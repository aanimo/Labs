using System;
using System.Diagnostics;
using System.IO;
using System.Management;

public class SystemInfo
{
    public string GetCPUInfo()
    {
        string cpuInfo = "";
        var searcher = new ManagementObjectSearcher("select Name, NumberOfCores, CurrentClockSpeed from Win32_Processor");
        foreach (var item in searcher.Get())
        {
            cpuInfo += $"Модель: {item["Name"]}, Ядер: {item["NumberOfCores"]}, Частота: {item["CurrentClockSpeed"]} MHz\n";
        }
        return cpuInfo;
    }

    public string GetRAMInfo()
    {
        var searcher = new ManagementObjectSearcher("select Capacity, FreePhysicalMemory from Win32_ComputerSystem");
        foreach (var item in searcher.Get())
        {
            long totalRAM = Convert.ToInt64(item["Capacity"]) / (1024 * 1024);
            long freeRAM = Convert.ToInt64(item["FreePhysicalMemory"]);
            long usedRAM = totalRAM - (freeRAM / 1024);
            return $"Общий объём RAM: {totalRAM} MB, Используемый объём: {usedRAM} MB, Свободный объём: {freeRAM / 1024} MB\n";
        }
        return "";
    }

    public string GetDiskInfo()
    {
        string diskInfo = "";
        foreach (var drive in DriveInfo.GetDrives())
        {
            if (drive.IsReady)
            {
                diskInfo += $"Диск: {drive.Name}, Общий объём: {drive.TotalSize / (1024 * 1024 * 1024)} GB, " +
                            $"Используемое пространство: {drive.TotalSize - drive.AvailableFreeSpace / (1024 * 1024 * 1024)} GB, " +
                            $"Свободное пространство: {drive.AvailableFreeSpace / (1024 * 1024 * 1024)} GB\n";
            }
        }
        return diskInfo;
    }

    public string GetGPUInfo()
    {
        string gpuInfo = "";
        var searcher = new ManagementObjectSearcher("select Name from Win32_VideoController");
        foreach (var item in searcher.Get())
        {
            gpuInfo += $"Модель GPU: {item["Name"]}\n";
        }
        return gpuInfo;
    }
}
