using System;
using System.Diagnostics;
using System.Management;
using System.Windows.Forms;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Monitoring
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer updateTimer;
        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;
        private CancellationTokenSource cancellationTokenSource; // Для отмены задач
        private int processUpdateCounter = 0;
        private bool sortByMemoryAscending = false;
        private bool sortByCpuUsageAscending = false;
        private List<ProcessInfo> cachedProcesses = new List<ProcessInfo>();
        private Dictionary<int, PerformanceCounter> cpuCounters = new Dictionary<int, PerformanceCounter>();

        public Form1()
        {
            InitializeComponent();
            InitializeListView();
            InitializeMonitoring();
            InitializeUpdateTimer();
            lvProcesses.ColumnClick += LvProcesses_ColumnClick;
        }

        private void InitializeListView()
        {
            lvProcesses.View = View.Details;
            lvProcesses.Columns.Clear();
            lvProcesses.Columns.Add("Имя процесса", 150, HorizontalAlignment.Left);
            lvProcesses.Columns.Add("PID", 50, HorizontalAlignment.Left);
            lvProcesses.Columns.Add("Используемая память", 100, HorizontalAlignment.Left);
            lvProcesses.Columns.Add("Загрузка CPU", 100, HorizontalAlignment.Left);
        }

        private void InitializeUpdateTimer()
        {
            updateTimer = new System.Windows.Forms.Timer();
            updateTimer.Interval = 2000;
            updateTimer.Tick += async (s, e) => await UpdateDataAsync();
            updateTimer.Start();
        }

        private async Task UpdateDataAsync()
        {
            // Проверяем, отменена ли операция
            if (cancellationTokenSource.IsCancellationRequested)
                return;

            var updateCpuTask = Task.Run(() => UpdateCPUInfo());
            var updateRamTask = Task.Run(() => UpdateRAMInfo());
            var updateDiskTask = Task.Run(() => UpdateDiskInfo());
            var updateProcessTask = Task.Run(() => UpdateProcessListAsync());

            await Task.WhenAll(updateCpuTask, updateRamTask, updateDiskTask, updateProcessTask);
        }

        private async Task UpdateProcessListAsync()
        {
            var currentProcesses = Process.GetProcesses().ToList();
            var updatedProcesses = new List<ProcessInfo>();

            foreach (var process in currentProcesses)
            {
                var cpuUsage = GetCpuUsage(process);
                updatedProcesses.Add(new ProcessInfo
                {
                    ProcessName = process.ProcessName,
                    Pid = process.Id,
                    TotalMemory = process.WorkingSet64 / (1024 * 1024),
                    CPUUsage = cpuUsage
                });
            }

            foreach (var updatedProcess in updatedProcesses)
            {
                var cachedProcess = cachedProcesses.FirstOrDefault(p => p.Pid == updatedProcess.Pid);
                if (cachedProcess == null ||
                    cachedProcess.TotalMemory != updatedProcess.TotalMemory ||
                    cachedProcess.CPUUsage != updatedProcess.CPUUsage)
                {
                    UpdateProcessInListView(updatedProcess);
                }
            }

            cachedProcesses = updatedProcesses;
        }

        private async Task InitializeMonitoring()
        {
            cancellationTokenSource = new CancellationTokenSource();
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            UpdateStaticHardwareInfo();
            await UpdateProcessListAsync();
        }

        private void UpdateStaticHardwareInfo()
        {
            UpdateCPUInfo();
            UpdateRAMInfo();
            UpdateDiskInfo();
            UpdateGPUInfo();
        }

        private void UpdateCPUInfo()
        {
            var searcher = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject obj in searcher.Get())
            {
                UpdateLabelThreadSafe(lblCPUName, "Модель: " + obj["Name"].ToString());
                UpdateLabelThreadSafe(lblCPUCores, "Ядра: " + obj["NumberOfCores"].ToString());
                UpdateLabelThreadSafe(lblCPUFrequency, "Частота: " + obj["MaxClockSpeed"].ToString() + " MHz");
            }

            float cpuLoad = cpuCounter.NextValue();
            UpdateLabelThreadSafe(lblCPULoad, "Загрузка CPU: " + cpuLoad.ToString("0.00") + " %");
            UpdateProgressBarThreadSafe(pbCpu, cpuLoad); // Обновляем прогресс-бар
        }

        private void UpdateRAMInfo()
        {
            var totalMemory = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;
            var freeMemory = ramCounter.NextValue();
            var usedMemory = (totalMemory / (1024 * 1024)) - freeMemory;

            UpdateLabelThreadSafe(lblRAMTotal, "Общий объем: " + (totalMemory / (1024 * 1024)).ToString("0") + " MB");
            UpdateLabelThreadSafe(lblRAMUsed, "Использовано: " + usedMemory.ToString("0") + " MB");
            UpdateLabelThreadSafe(lblRAMFree, "Свободно: " + freeMemory.ToString("0") + " MB");

            // Обновляем прогресс-бар для RAM
            float ramUsagePercentage = (usedMemory / (float)(totalMemory / (1024 * 1024))) * 100;
            UpdateProgressBarThreadSafe(pbRam, ramUsagePercentage);
        }

        private void UpdateDiskInfo()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            var diskInfoBuilder = new System.Text.StringBuilder();
            float totalDiskSpace = 0;
            float totalUsedSpace = 0;

            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    long totalSpace = drive.TotalSize / (1024 * 1024 * 1024); // В ГБ
                    long freeSpace = drive.TotalFreeSpace / (1024 * 1024 * 1024); // В ГБ
                    long usedSpace = totalSpace - freeSpace;

                    diskInfoBuilder.AppendLine($"Диск {drive.Name} - Общий объем: {totalSpace} GB, Свободно: {freeSpace} GB, Использовано: {usedSpace} GB");
                    totalDiskSpace += totalSpace;
                    totalUsedSpace += usedSpace;
                }
            }

            UpdateLabelThreadSafe(lblDiskInfo, diskInfoBuilder.ToString());

            // Обновляем прогресс-бар для диска
            float diskUsagePercentage = (totalUsedSpace / totalDiskSpace) * 100;
            UpdateProgressBarThreadSafe(pbDisk, diskUsagePercentage);
        }

        private void UpdateProgressBarThreadSafe(ProgressBar progressBar, float value)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(new Action(() => UpdateProgressBarThreadSafe(progressBar, value)));
            }
            else
            {
                // Ограничиваем значение от 0 до 100
                value = value < 0 ? 0 : (value > 100 ? 100 : value);
                progressBar.Value = (int)value; // Устанавливаем значение прогресс-бара
            }
        }




        private void UpdateGPUInfo()
        {
            var searcher = new ManagementObjectSearcher("select * from Win32_VideoController");
            foreach (ManagementObject obj in searcher.Get())
            {
                UpdateLabelThreadSafe(lblGPUName, "Графический процессор: " + obj["Name"].ToString());
                UpdateLabelThreadSafe(lblGPUMemory, "Память: " + (Convert.ToUInt64(obj["AdapterRAM"]) / (1024 * 1024)).ToString("0") + " MB");
            }
        }

        private void UpdateProcessInListView(ProcessInfo process)
        {
            if (lvProcesses.InvokeRequired)
            {
                lvProcesses.Invoke(new Action(() => UpdateProcessInListView(process)));
            }
            else
            {
                var existingItem = lvProcesses.Items.Cast<ListViewItem>()
                    .FirstOrDefault(item => int.Parse(item.SubItems[1].Text) == process.Pid);

                if (existingItem != null)
                {
                    existingItem.SubItems[2].Text = process.TotalMemory.ToString("0") + " MB";
                    existingItem.SubItems[3].Text = process.CPUUsage.ToString("0.00") + " %";
                }
                else
                {
                    var item = new ListViewItem(new[] {
                        process.ProcessName,
                        process.Pid.ToString(),
                        process.TotalMemory.ToString("0") + " MB",
                        process.CPUUsage.ToString("0.00") + " %"
                    });
                    lvProcesses.Items.Add(item);
                }
            }
        }

        private float GetCpuUsage(Process process)
        {
            try
            {
                if (!cpuCounters.ContainsKey(process.Id))
                {
                    cpuCounters[process.Id] = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
                }
                return cpuCounters[process.Id].NextValue();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Ошибка получения CPU Usage для процесса {process.ProcessName}: {ex.Message}");
                return 0; // Возвращаем 0, если произошла ошибка
            }
        }


        private void LvProcesses_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            switch (e.Column)
            {
                case 2:
                    sortByMemoryAscending = !sortByMemoryAscending;
                    var sortedByMemory = lvProcesses.Items.Cast<ListViewItem>()
                        .OrderBy(item => int.Parse(item.SubItems[2].Text.Split(' ')[0])).ToList();

                    if (!sortByMemoryAscending)
                        sortedByMemory.Reverse();

                    lvProcesses.Items.Clear();
                    lvProcesses.Items.AddRange(sortedByMemory.ToArray());
                    break;
                case 3:
                    sortByCpuUsageAscending = !sortByCpuUsageAscending;
                    var sortedByCpu = lvProcesses.Items.Cast<ListViewItem>()
                        .OrderBy(item => float.Parse(item.SubItems[3].Text.Split(' ')[0])).ToList();

                    if (!sortByCpuUsageAscending)
                        sortedByCpu.Reverse();

                    lvProcesses.Items.Clear();
                    lvProcesses.Items.AddRange(sortedByCpu.ToArray());
                    break;
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            cancellationTokenSource.Cancel(); // Отменяем все операции
            updateTimer.Stop();
            cpuCounter.Dispose();
            ramCounter.Dispose();
            foreach (var cpuCounter in cpuCounters.Values)
            {
                cpuCounter.Dispose();
            }

            base.OnFormClosed(e);
        }

        private void UpdateLabelThreadSafe(Label label, string text)
        {
            if (label.InvokeRequired)
            {
                label.Invoke(new Action(() => label.Text = text));
            }
            else
            {
                label.Text = text;
            }
        }
    }

    public class ProcessInfo
    {
        public string ProcessName { get; set; }
        public int Pid { get; set; }
        public long TotalMemory { get; set; }
        public float CPUUsage { get; set; }
    }
}
