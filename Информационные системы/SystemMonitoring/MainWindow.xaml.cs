using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Windows;
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;

namespace SystemMonitor
{
    public partial class MainWindow : Window
    {
        // Данные для графиков
        public SeriesCollection CpuSeries { get; set; }
        public SeriesCollection MemorySeries { get; set; }

        private DispatcherTimer timer;
        private List<double> cpuValues;
        private List<double> memoryValues;

        public SeriesCollection DiskUsageSeries { get; set; }
        public SeriesCollection GpuUsageSeries { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            LoadSystemInfo();
            LoadProcessList();

            // Инициализация данных для графиков
            CpuSeries = new SeriesCollection();
            MemorySeries = new SeriesCollection();
            DiskUsageSeries = new SeriesCollection();
            GpuUsageSeries = new SeriesCollection();
            cpuValues = new List<double>();
            memoryValues = new List<double>();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += Timer_Tick;
            timer.Start();

            // Привязка данных к графикам
            DataContext = this;

            // Инициализация дисков и GPU
            UpdateDiskUsage();
            UpdateGpuUsage();
        }

        private void UpdateDiskUsage()
        {
            DiskUsageSeries.Clear();
            var diskInfo = new ManagementObjectSearcher("select * from Win32_LogicalDisk where DriveType=3");
            foreach (var item in diskInfo.Get())
            {
                long freeSpace = Convert.ToInt64(item["FreeSpace"]) / (1024 * 1024);
                long totalSize = Convert.ToInt64(item["Size"]) / (1024 * 1024);
                double usagePercent = (1 - (double)freeSpace / totalSize) * 100;

                DiskUsageSeries.Add(new PieSeries
                {
                    Title = $"{item["DeviceID"]}: {usagePercent:F1}%",
                    Values = new ChartValues<double> { usagePercent, 100 - usagePercent },
                    DataLabels = true
                });
            }
        }

        private void UpdateGpuUsage()
        {
            GpuUsageSeries.Clear();
            var gpuInfo = new ManagementObjectSearcher("select * from Win32_VideoController");
            foreach (var item in gpuInfo.Get())
            {
                GpuUsageSeries.Add(new PieSeries
                {
                    Title = $"{item["Name"]}",
                    Values = new ChartValues<double> { /* Ваши данные GPU */ },
                    DataLabels = true
                });
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateCpuUsage();
            UpdateMemoryUsage();
            UpdateProcessList();
        }

        private void UpdateCpuUsage()
        {
            var cpuUsage = GetCpuLoad();
            cpuValues.Add(cpuUsage);
            if (cpuValues.Count > 10) cpuValues.RemoveAt(0); // Ограничиваем количество значений

            CpuSeries.Clear();
            CpuSeries.Add(new LineSeries
            {
                Values = new ChartValues<double>(cpuValues),
                Title = "Загрузка CPU (%)"
            });
        }

        private void UpdateMemoryUsage()
        {
            var memoryUsage = GetMemoryUsage();
            memoryValues.Add(memoryUsage);
            if (memoryValues.Count > 10) memoryValues.RemoveAt(0); // Ограничиваем количество значений

            MemorySeries.Clear();
            MemorySeries.Add(new LineSeries
            {
                Values = new ChartValues<double>(memoryValues),
                Title = "Использование памяти (MB)"
            });
        }

        private double GetCpuLoad()
        {
            // Получаем загрузку CPU
            var counter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            return counter.NextValue();
        }

        private double GetMemoryUsage()
        {
            // Получаем используемую память
            var ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            float availableMemory = ramCounter.NextValue();
            var totalMemory = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory / (1024 * 1024);
            return totalMemory - availableMemory; // Используемая память в MB
        }

        private void LoadSystemInfo()
        {
            // Получение информации о процессоре
            var cpuInfo = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (var item in cpuInfo.Get())
            {
                CpuInfo.Text += $"Модель: {item["Name"]}, Ядер: {item["NumberOfCores"]}, Частота: {item["CurrentClockSpeed"]} MHz\n";
            }

            // Получение информации об оперативной памяти
            var ramInfo = new ManagementObjectSearcher("select * from Win32_ComputerSystem");
            foreach (var item in ramInfo.Get())
            {
                long totalMemory = Convert.ToInt64(item["TotalPhysicalMemory"]) / (1024 * 1024);
                RamInfo.Text = $"Общий объём: {totalMemory} MB";
            }

            // Получение информации о диске
            var diskInfo = new ManagementObjectSearcher("select * from Win32_LogicalDisk where DriveType=3");
            foreach (var item in diskInfo.Get())
            {
                long totalSize = Convert.ToInt64(item["Size"]) / (1024 * 1024);
                long freeSpace = Convert.ToInt64(item["FreeSpace"]) / (1024 * 1024);
                HddInfo.Text += $"Диск {item["DeviceID"]}: Общий объём: {totalSize} MB, Свободное пространство: {freeSpace} MB\n";
            }

            // Получение информации о графическом процессоре
            var gpuInfo = new ManagementObjectSearcher("select * from Win32_VideoController");
            foreach (var item in gpuInfo.Get())
            {
                GpuInfo.Text = $"GPU: {item["Name"]}, Memory: {item["AdapterRAM"]} bytes";
            }
        }

        private void LoadProcessList()
        {
            var processes = Process.GetProcesses()
                .Select(p => new
                {
                    ProcessName = p.ProcessName,
                    MemoryUsage = Math.Round((double)p.PrivateMemorySize64 / (1024 * 1024), 2),
                    CpuLoad = GetCpuUsage(p)
                }).OrderByDescending(p => p.MemoryUsage).ToList();

            ProcessesListView.ItemsSource = processes;
        }

        private double GetCpuUsage(Process process)
        {
            // Получить нагрузку на процессор для данного процесса
            var counter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
            return counter.NextValue();
        }

        private void UpdateProcessList()
        {
            // Обновление списка процессов
            LoadProcessList();
        }
    }
}
