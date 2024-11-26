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

        private const int MaxProcessesToDisplay = 20; // Лимит отображаемых процессов
        private Dictionary<string, float> cpuCache = new Dictionary<string, float>(); // Кеш для CPU

        // Настройки логирования
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
            // Настройка ListView
            processListView = new ListView
            {
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Dock = DockStyle.Fill
            };

            // Добавление колонок
            processListView.Columns.Add("Имя процесса", 200);
            processListView.Columns.Add("Память (MB)", 100);
            processListView.Columns.Add("CPU (%)", 100);

            // Добавление контекстного меню
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Завершить процесс", null, (s, e) => KillSelectedProcess());
            processListView.ContextMenuStrip = contextMenu;

            // Добавление ListView на вкладку
            processesTabPage.Controls.Add(processListView);

            // Таймер для обновления списка процессов (RAM + общий список)
            processUpdateTimer = new Timer { Interval = 5000 };
            processUpdateTimer.Tick += (s, e) => UpdateProcesses();
            processUpdateTimer.Start();

            // Таймер для обновления CPU-метрик
            cpuUpdateTimer = new Timer { Interval = 10000 };
            cpuUpdateTimer.Tick += (s, e) => UpdateCpuMetrics();
            cpuUpdateTimer.Start();
        }

        private void UpdateCpuMetrics()
        {
            try
            {
                foreach (ListViewItem item in processListView.Items)
                {
                    string processName = item.Text;

                    float cpuUsage = GetProcessCpuUsage(processName);

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
                        Memory = p.WorkingSet64 / (1024f * 1024f) // Перевод в MB
                    })
                    .OrderByDescending(p => p.Memory) // Сортировка по RAM
                    .Take(MaxProcessesToDisplay) 
                    .ToList();

                // Очистка списка
                processListView.Items.Clear();

                // Добавление процессов в ListView
                foreach (var process in allProcesses)
                {
                    var item = new ListViewItem(process.Name); // Имя процесса
                    item.SubItems.Add(process.Memory.ToString("F2")); // Использование памяти

                    float cpuUsage = cpuCache.ContainsKey(process.Name) ? cpuCache[process.Name] : 0f;
                    item.SubItems.Add(cpuUsage.ToString("F2")); // CPU

                    processListView.Items.Add(item);
                }

                // Обновление CPU в фоновом режиме
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
            // Обновление использования CPU
            float cpuUsage = cpuCounter.NextValue();
            cpuUsageLabel.Text = $"CPU Usage: {cpuUsage:F2}%";
            cpuChart.ChartAreas[0].AxisY.Maximum = 100;
            cpuChart.ChartAreas[0].AxisY.Minimum = 0;
            cpuChart.Series[0].ChartType = SeriesChartType.Spline;
            cpuChart.Series[0].Points.AddY(cpuUsage);

            // Обновление использования RAM
            float availableRam = ramCounter.NextValue();
            float totalRam = GetTotalPhysicalMemory();
            float usedRam = totalRam - availableRam;

            ramUsageLabel.Text = $"Used RAM: {usedRam:F2} MB / {totalRam:F2} MB";
            ramChart.ChartAreas[0].AxisY.Maximum = 16000;
            ramChart.ChartAreas[0].AxisY.Minimum = 0;
            ramChart.Series[0].ChartType = SeriesChartType.Spline;
            ramChart.Series[0].Points.AddY(usedRam);

            // Логирование
            if (logCpu || logRam)
            {
                LogMetrics(cpuUsage, usedRam, totalRam);
            }

            // Обновление списка процессов
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
                        return $"{obj["Name"]} - {obj["AdapterRAM"]} Bytes";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error retrieving GPU info: {ex.Message}";
            }
            return "GPU info not available.";
        }

        private void LoadProcesses()
        {
            // Получаем текущие процессы
            var currentProcesses = Process.GetProcesses()
                .Select(p => new
                {
                    Name = p.ProcessName,
                    Memory = p.WorkingSet64 / (1024f * 1024f), // Перевод в MB
                    Cpu = GetProcessCpuUsage(p.ProcessName)
                })
                .OrderByDescending(p => p.Memory) // Сортировка по RAM (по убыванию)
                .ToList();

            processListView.Items.Clear();

            foreach (var process in currentProcesses)
            {
                var item = new ListViewItem(process.Name); // Имя процесса
                item.SubItems.Add(process.Memory.ToString("F2")); // Использование памяти
                item.SubItems.Add(process.Cpu.ToString("F2")); // Использование CPU
                processListView.Items.Add(item); // Добавление элемента
            }
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
        private void KillSelectedProcess()
        {
            if (processListView.SelectedItems.Count > 0)
            {
                var selectedItem = processListView.SelectedItems[0];
                string processName = selectedItem.Text; // Имя процесса

                try
                {
                    // Завершаем все процессы с этим именем
                    foreach (var process in Process.GetProcessesByName(processName))
                    {
                        process.Kill();
                    }
                    MessageBox.Show($"Процессы с именем {processName} завершены.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при завершении процесса: {ex.Message}");
                }
            }
        }

        private void InitializeSettingsTab()
        {
            // Установка текста выбранного файла по умолчанию
            selectedFileLabel.Text = $"{Path.GetFileName(logFilePath)}";

            // Событие для CheckBox логирования CPU
            logCpuCheckBox.CheckedChanged += (s, e) => logCpu = logCpuCheckBox.Checked;

            // Событие для CheckBox логирования RAM
            logRamCheckBox.CheckedChanged += (s, e) => logRam = logRamCheckBox.Checked;

            // Событие для кнопки выбора файла
            browseButton.Click += (s, e) =>
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "Text Files|*.txt",
                    Title = "Select or Create Log File",
                    CheckFileExists = false,
                    InitialDirectory = AppDomain.CurrentDomain.BaseDirectory, // Открыть текущую директорию по умолчанию
                    FileName = "system_metrics_log.txt" // Установить имя по умолчанию
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
        private void LogMetrics(float cpuUsage, float usedRam, float totalRam)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\t";
                    if (logCpu)
                    {
                        logEntry += $"CPU Usage: {cpuUsage:F2}%\t";
                    }
                    if (logRam)
                    {
                        logEntry += $"Used RAM: {usedRam:F2} MB / {totalRam:F2} MB";
                    }
                    writer.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }

    }
}
