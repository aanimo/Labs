using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PCMonitoringApp
{
    public partial class MainForm : Form
    {
        private Timer updateTimer;
        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;
        private ListView processListView;
        private Timer processUpdateTimer;
        private Timer cpuUpdateTimer;

        private const int MaxProcessesToDisplay = 20;
        private Dictionary<string, float> cpuCache = new Dictionary<string, float>();

        private bool logCpu = false;
        private bool logRam = false;
        private string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "system_metrics_log.txt");

        public MainForm()
        {
            InitializeComponent();
            InitializePerformanceCounters();
            SetupTimer();
            LoadHardwareInfo();
            InitializeSettingsTab();
            InitializeProcessTab();
        }

        private void InitializeProcessTab()
        {
            processListView = new ListView
            {
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Dock = DockStyle.Fill
            };

            processListView.Columns.Add("Имя процесса", 200);
            processListView.Columns.Add("Память (MB)", 100);
            processListView.Columns.Add("CPU (%)", 100);

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Завершить процесс", null, (s, e) => KillSelectedProcess());
            processListView.ContextMenuStrip = contextMenu;

            processesTabPage.Controls.Add(processListView);

            processUpdateTimer = new Timer { Interval = 5000 };
            processUpdateTimer.Tick += (s, e) => UpdateProcesses();
            processUpdateTimer.Start();

            cpuUpdateTimer = new Timer { Interval = 10000 };
            cpuUpdateTimer.Tick += (s, e) => UpdateCpuMetrics();
            cpuUpdateTimer.Start();
        }

        private async void UpdateCpuMetrics()
        {
            try
            {
                foreach (ListViewItem item in processListView.Items)
                {
                    string processName = item.Text;

                    float cpuUsage = await Task.Run(() => GetProcessCpuUsage(processName));

                    lock (cpuCache)
                    {
                        cpuCache[processName] = cpuUsage;
                    }

                    Invoke(new Action(() =>
                    {
                        item.SubItems[2].Text = cpuUsage.ToString("F2");
                    }));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при обновлении CPU метрик: {ex.Message}");
            }
        }


        private async void UpdateProcesses()
        {
            try
            {
                var allProcesses = Process.GetProcesses()
                    .Select(p => new
                    {
                        Name = p.ProcessName,
                        Memory = p.WorkingSet64 / (1024f * 1024f)
                    })
                    .OrderByDescending(p => p.Memory)
                    .Take(MaxProcessesToDisplay)
                    .ToList();

                processListView.Items.Clear();

                foreach (var process in allProcesses)
                {
                    var item = new ListViewItem(process.Name);
                    item.SubItems.Add(process.Memory.ToString("F2"));

                    float cpuUsage = cpuCache.ContainsKey(process.Name) ? cpuCache[process.Name] : 0f;
                    item.SubItems.Add(cpuUsage.ToString("F2"));

                    processListView.Items.Add(item);
                }

                await Task.Run(UpdateCpuMetrics);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при обновлении процессов: {ex.Message}");
            }
        }

        private void InitializePerformanceCounters()
        {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        private void SetupTimer()
        {
            updateTimer = new Timer { Interval = 1000 };
            updateTimer.Tick += UpdateSystemInfo;
            updateTimer.Start();
        }

        private void UpdateSystemInfo(object sender, EventArgs e)
        {
            float cpuUsage = cpuCounter.NextValue();
            cpuUsageLabel.Text = $"CPU Usage: {cpuUsage:F2}%";
            cpuChart.ChartAreas[0].AxisY.Maximum = 100;
            cpuChart.ChartAreas[0].AxisY.Minimum = 0;
            cpuChart.Series[0].ChartType = SeriesChartType.Spline;
            cpuChart.Series[0].Points.AddY(cpuUsage);

            float availableRam = ramCounter.NextValue();
            float totalRam = GetTotalPhysicalMemory();
            float usedRam = totalRam - availableRam;

            ramUsageLabel.Text = $"Used RAM: {usedRam:F2} MB / {totalRam:F2} MB";
            ramChart.ChartAreas[0].AxisY.Maximum = 16000;
            ramChart.ChartAreas[0].AxisY.Minimum = 0;
            ramChart.Series[0].ChartType = SeriesChartType.Spline;
            ramChart.Series[0].Points.AddY(usedRam);

            if (logCpu || logRam)
            {
                LogMetrics(cpuUsage, usedRam, totalRam);
            }

            LoadProcesses();
        }

        private void LoadHardwareInfo()
        {
            cpuInfoLabel.Text = GetCpuInfo();
            ramInfoLabel.Text = $"Total RAM: {GetTotalPhysicalMemory()} MB";
            hddInfoLabel.Text = GetHddInfo();
            gpuInfoLabel.Text = GetGpuInfo();
        }

        private string GetCpuInfo()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("select * from Win32_Processor"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        return $"{obj["Name"]} - {obj["NumberOfCores"]} Cores @ {obj["MaxClockSpeed"]} MHz";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error retrieving CPU info: {ex.Message}";
            }
            return "CPU info not available.";
        }

        private float GetTotalPhysicalMemory()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("select * from Win32_ComputerSystem"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        if (obj["TotalPhysicalMemory"] != null)
                        {
                            return Convert.ToSingle(obj["TotalPhysicalMemory"]) / (1024 * 1024);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving total physical memory: {ex.Message}");
            }
            return 0;
        }

        private string GetHddInfo()
        {
            try
            {
                string hddInfo = string.Empty;
                using (var searcher = new ManagementObjectSearcher("select * from Win32_LogicalDisk where DriveType=3"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        hddInfo += $"{obj["DeviceID"]}: {Convert.ToDouble(obj["Size"]) / (1024 * 1024 * 1024):F2} GB Total, " +
                                   $"{Convert.ToDouble(obj["FreeSpace"]) / (1024 * 1024 * 1024):F2} GB Free\n";
                    }
                }
                return hddInfo;
            }
            catch (Exception ex)
            {
                return $"Error retrieving HDD info: {ex.Message}";
            }
        }

        private string GetGpuInfo()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("select * from Win32_VideoController"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        return $"{obj["Name"]} - {obj["AdapterRAM"]}";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error retrieving GPU info: {ex.Message}";
            }
            return "GPU info not available.";
        }

        private void KillSelectedProcess()
        {
            try
            {
                if (processListView.SelectedItems.Count > 0)
                {
                    string processName = processListView.SelectedItems[0].Text;
                    var processes = Process.GetProcessesByName(processName);
                    foreach (var process in processes)
                    {
                        process.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error terminating process: {ex.Message}");
            }
        }

        private void LogMetrics(float cpuUsage, float usedRam, float totalRam)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(logFilePath, true))
                {
                    sw.WriteLine($"{DateTime.Now} - CPU: {cpuUsage:F2}% - RAM Used: {usedRam:F2} MB / {totalRam:F2} MB");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error logging metrics: {ex.Message}");
            }
        }
        private void InitializeSettingsTab()
        {
            selectedFileLabel.Text = $"{Path.GetFileName(logFilePath)}";

            logCpuCheckBox.CheckedChanged += (s, e) => logCpu = logCpuCheckBox.Checked;

            logRamCheckBox.CheckedChanged += (s, e) => logRam = logRamCheckBox.Checked;

            browseButton.Click += (s, e) =>
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Text Files|*.txt",
                    Title = "Select or Create Log File",
                    CheckFileExists = false,
                    InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                    FileName = "system_metrics_log.txt"
                })
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        logFilePath = openFileDialog.FileName;
                        selectedFileLabel.Text = $"{Path.GetFileName(logFilePath)}";
                    }
                }
            };
        }

        private float GetProcessCpuUsage(string processName)
        {
            try
            {
                using (PerformanceCounter pc = new PerformanceCounter("Process", "% Processor Time", processName, true))
                {
                    pc.NextValue();
                    System.Threading.Thread.Sleep(100);
                    return pc.NextValue() / Environment.ProcessorCount;
                }
            }
            catch
            {
                return 0f;
            }
        }


        private async void LoadProcesses()
        {
            try
            {
                var currentProcesses = await Task.Run(() =>
                    Process.GetProcesses()
                    .Select(p => new
                    {
                        Name = p.ProcessName,
                        Memory = p.WorkingSet64 / (1024f * 1024f),
                        Cpu = GetProcessCpuUsage(p.ProcessName)
                    })
                    .OrderByDescending(p => p.Memory)
                    .ToList());

                processListView.Items.Clear();

                foreach (var process in currentProcesses)
                {
                    var item = new ListViewItem(process.Name); // Имя процесса
                    item.SubItems.Add(process.Memory.ToString("F2")); // Использование памяти
                    item.SubItems.Add(process.Cpu.ToString("F2")); // Использование CPU
                    processListView.Items.Add(item); // Добавление элемента
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка при загрузке процессов: {ex.Message}");
            }
        }

    }
}
