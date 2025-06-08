using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using OpenHardwareMonitor.Hardware;

namespace Monitoring
{
    class Program
    {
        static bool LogCPU = false;
        static bool LogRAM = false;
        static bool LogDisk = false;
        static string LogFile = "log.txt";
        static int LogIntervalSeconds = 5; //по умолчанию каждые 5 секунд

        static void Main()
        {
            Console.Title = "Мониторинг ПК/Сервера";
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== МЕНЮ ====");
                Console.WriteLine("[1] Аппаратная часть");
                Console.WriteLine("[2] Мониторинг системы");
                Console.WriteLine("[3] Список процессов");
                Console.WriteLine("[4] SMART-диски");
                Console.WriteLine("[5] Настройки логирования");
                Console.WriteLine("[0] Выход");
                Console.Write("Выберите раздел: ");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        ShowHardwareInfo();
                        break;
                    case ConsoleKey.D2:
                        MonitorSystem();
                        break;
                    case ConsoleKey.D3:
                        ShowProcesses();
                        break;
                    case ConsoleKey.D4:
                        ShowSmartInfo();
                        break;
                    case ConsoleKey.D5:
                        SettingsMenu();
                        break;
                    case ConsoleKey.D0:
                        return;
                }
            }
        }

        static void ShowHardwareInfo()
        {
            Console.Clear();
            Console.WriteLine("== Аппаратная часть ==");

            var searcher = new System.Management.ManagementObjectSearcher("select * from Win32_Processor");
            foreach (var item in searcher.Get())
            {
                Console.WriteLine($"CPU: {item["Name"]}, Ядер: {item["NumberOfCores"]}");
            }

            var ramSearcher = new System.Management.ManagementObjectSearcher("select * from Win32_PhysicalMemory");
            ulong totalRam = 0;
            foreach (var item in ramSearcher.Get())
            {
                totalRam += (ulong)item["Capacity"];
            }
            Console.WriteLine($"RAM: {totalRam / 1024 / 1024 / 1024} GB");

            var diskSearcher = new System.Management.ManagementObjectSearcher("select * from Win32_LogicalDisk where DriveType=3");
            foreach (var d in diskSearcher.Get())
            {
                ulong size = (ulong)d["Size"];
                ulong free = (ulong)d["FreeSpace"];
                Console.WriteLine($"Диск {d["DeviceID"]}: {size / 1024 / 1024 / 1024} GB, Свободно: {free / 1024 / 1024 / 1024} GB");
            }

            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        static void MonitorSystem()
        {
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            var ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            Console.Clear();
            Console.WriteLine("== Мониторинг системы ==\n(Нажмите Esc для выхода)\n");

            while (true)
            {
                float cpu = cpuCounter.NextValue();
                float ram = ramCounter.NextValue();

                Console.SetCursorPosition(0, 3);
                Console.WriteLine($"Загрузка CPU:  {cpu,5:0.0} %   ");
                Console.WriteLine($"Свободная RAM: {ram,5} MB     ");

                if (LogCPU || LogRAM)
                {
                    using var sw = new StreamWriter(LogFile, true);
                    string logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";

                    if (LogCPU) logLine += $" | CPU: {cpu:0.0}%";
                    if (LogRAM) logLine += $" | RAM Free: {ram} MB";

                    sw.WriteLine(logLine);
                }

                Thread.Sleep(LogIntervalSeconds * 1000);

                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                    break;
            }
        }

        static void ShowProcesses()
        {
            const int pageSize = 10;
            int currentPage = 0;
            bool paused = false;
            Dictionary<int, TimeSpan> lastCpuTimes = new();
            DateTime lastTime = DateTime.Now;

            List<(string name, double cpu, double ram)> processInfoList = new();

            while (true)
            {
                if (!paused)
                {
                    var allProcesses = Process.GetProcesses();
                    var now = DateTime.Now;
                    double interval = (now - lastTime).TotalMilliseconds;
                    lastTime = now;

                    processInfoList.Clear();

                    foreach (var p in allProcesses)
                    {
                        try
                        {
                            TimeSpan prevCpu = lastCpuTimes.TryGetValue(p.Id, out var prev) ? prev : TimeSpan.Zero;
                            TimeSpan currCpu = p.TotalProcessorTime;
                            double cpuPercent = (currCpu - prevCpu).TotalMilliseconds / interval / Environment.ProcessorCount * 100;
                            double ramMB = p.WorkingSet64 / 1024.0 / 1024.0;
                            processInfoList.Add((p.ProcessName, cpuPercent, ramMB));
                            lastCpuTimes[p.Id] = currCpu;
                        }
                        catch { }
                    }

                    processInfoList.Sort((a, b) => b.cpu.CompareTo(a.cpu));

                    if (LogCPU || LogRAM)
                    {
                        using var sw = new StreamWriter(LogFile, true);
                        sw.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | Топ процессов:");
                        foreach (var (name, cpu, ram) in processInfoList.GetRange(0, Math.Min(5, processInfoList.Count)))
                        {
                            string line = $"  {name,-20} CPU: {cpu:0.0}%  RAM: {ram:0.0} MB";
                            sw.WriteLine(line);
                        }
                    }
                }

                int totalPages = (processInfoList.Count + pageSize - 1) / pageSize;
                currentPage = Math.Min(Math.Max(currentPage, 0), totalPages - 1);
                int start = currentPage * pageSize;
                int end = Math.Min(start + pageSize, processInfoList.Count);

                Console.Clear();
                Console.WriteLine($"== Процессы ({(paused ? "Пауза" : "Обновляется...")})  ← → страницы, P — пауза, Esc — выход ==\n");

                Console.WriteLine("{0,-30} {1,8} {2,8}", "Имя процесса", "CPU %", "RAM MB");
                Console.WriteLine(new string('-', 50));
                for (int i = start; i < end; i++)
                {
                    var (name, cpu, ram) = processInfoList[i];
                    Console.WriteLine("{0,-30} {1,8:0.0} {2,8:0.0}", name, cpu, ram);
                }

                Console.WriteLine($"\nСтраница {currentPage + 1}/{totalPages}");

                DateTime waitStart = DateTime.Now;

                while (true)
                {
                    if (!paused && (DateTime.Now - waitStart).TotalMilliseconds >= 2000)
                        break;

                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true).Key;
                        if (key == ConsoleKey.Escape) return;
                        if (key == ConsoleKey.P) { paused = !paused; break; }
                        if (paused && key == ConsoleKey.LeftArrow) { currentPage--; break; }
                        if (paused && key == ConsoleKey.RightArrow) { currentPage++; break; }
                    }

                    Thread.Sleep(LogIntervalSeconds * 1000);
                }
            }
        }

        static void ShowSmartInfo()
        {
            Console.Clear();
            Console.WriteLine("== SMART-информация о дисках ==\n");

            Computer computer = new Computer
            {
                HDDEnabled = true
            };
            computer.Open();
            computer.Accept(new HardwareVisitor());

            bool found = false;

            foreach (var hardware in computer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.HDD)
                {
                    Console.WriteLine($"[{hardware.Name}]");
                    hardware.Update();

                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                            Console.WriteLine($"Температура: {sensor.Value?.ToString("0.0")} °C");

                        if (sensor.SensorType == SensorType.Load)
                            Console.WriteLine($"Нагрузка: {sensor.Value?.ToString("0.0")} %");

                        if (sensor.SensorType == SensorType.Data)
                            Console.WriteLine($"{sensor.Name}: {sensor.Value?.ToString("0.0")} {sensor.SensorType}");
                    }

                    Console.WriteLine();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("SMART-информация недоступна или не обнаружены HDD/SSD.");
            }

            Console.WriteLine("\nНажмите любую клавишу для возврата...");
            Console.ReadKey();
        }


        static void SettingsMenu()
        {
            ConsoleKey key;
            do
            {
                Console.Clear();
                Console.WriteLine("== Настройки логирования ==");
                Console.WriteLine($"[1] CPU: {(LogCPU ? "ВКЛ" : "ВЫКЛ")}");
                Console.WriteLine($"[2] RAM: {(LogRAM ? "ВКЛ" : "ВЫКЛ")}");
                Console.WriteLine($"[3] Диски: {(LogDisk ? "ВКЛ" : "ВЫКЛ")}");
                Console.WriteLine($"[4] Интервал логирования: {LogIntervalSeconds} сек");
                Console.WriteLine("\n[0] Назад");
                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.D1: LogCPU = !LogCPU; break;
                    case ConsoleKey.D2: LogRAM = !LogRAM; break;
                    case ConsoleKey.D3: LogDisk = !LogDisk; break;
                    case ConsoleKey.D4:
                        Console.Clear();
                        Console.Write("Введите интервал логирования (сек): ");
                        string input = Console.ReadLine();
                        if (int.TryParse(input, out int interval) && interval > 0)
                        {
                            LogIntervalSeconds = interval;
                        }
                        else
                        {
                            Console.WriteLine("Неверный ввод. Используется предыдущее значение.");
                            Thread.Sleep(1000);
                        }
                        break;
                }
            } while (key != ConsoleKey.D0);
        }
        public class HardwareVisitor : IVisitor
        {
            public void VisitComputer(IComputer computer)
            {
                computer.Traverse(this);
            }

            public void VisitHardware(IHardware hardware)
            {
                hardware.Update();
                foreach (IHardware subHardware in hardware.SubHardware)
                    subHardware.Accept(this);
            }

            public void VisitSensor(ISensor sensor) { }

            public void VisitParameter(IParameter parameter) { }
        }

    }
}
