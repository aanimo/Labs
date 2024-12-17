using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.IO;
using System.Text;

namespace LoggingSystem
{
    /// <summary>
    /// Уровни логирования
    /// </summary>
    public enum LogLevel
    {
        TRACE,
        DEBUG,
        INFO,
        WARNING,
        ERROR
    }

    /// <summary>
    /// Класс для логирования сообщений с поддержкой различных уровней, мест вывода и форматов.
    /// </summary>
    public class Logger
    {
        private static readonly Lazy<Logger> _instance = new(() => new Logger());
        private readonly object _lock = new();
        private LogLevel _logLevel = LogLevel.INFO;
        private string _logFileName = $"{AppDomain.CurrentDomain.FriendlyName}.log";
        private bool _logToConsole = true;
        private bool _logToFile = false;
        private string _customTemplate = "{t} | {L} -> {m}";

        private readonly ConcurrentDictionary<string, StreamWriter> _fileWriters = new();

        private Logger() { }

        /// <summary>
        /// Получение экземпляра Logger (паттерн Singleton).
        /// </summary>
        public static Logger Instance => _instance.Value;

        /// <summary>
        /// Настройка логгера с выбором уровня логирования, мест вывода и имени файла.
        /// </summary>
        public void Configure(LogLevel level, bool logToConsole, bool logToFile, string logFileName = null)
        {
            lock (_lock)
            {
                _logLevel = level;
                _logToConsole = logToConsole;
                _logToFile = logToFile;
                if (!string.IsNullOrWhiteSpace(logFileName))
                {
                    _logFileName = logFileName;
                }
            }
        }

        /// <summary>
        /// Установка пользовательского шаблона для форматирования лог-сообщений.
        /// </summary>
        public void SetCustomTemplate(string template)
        {
            _customTemplate = template;
        }

        /// <summary>
        /// Запись лог-сообщения с указанием уровня и дополнительных параметров.
        /// </summary>
        public void Log(LogLevel level, string message, string filePath = "", int lineNumber = 0)
        {
            if (level < _logLevel)
                return;

            string logMessage = FormatMessage(level, message, filePath, lineNumber);
            WriteLog(logMessage, level);
        }

        /// <summary>
        /// Запись форматированного сообщения в лог с передачей нескольких параметров.
        /// </summary>
        public void Log(LogLevel level, string message, params object[] args)
        {
            string formattedMessage = string.Format(CultureInfo.InvariantCulture, message, args);
            Log(level, formattedMessage);
        }

        /// <summary>
        /// Форматирование сообщения для логирования.
        /// </summary>
        private string FormatMessage(LogLevel level, string message, string filePath, int lineNumber)
        {
            string timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            string fileName = string.IsNullOrWhiteSpace(filePath) ? "" : Path.GetFileName(filePath);
            string formatted = _customTemplate
                .Replace("{t}", timeStamp)
                .Replace("{L}", level.ToString())
                .Replace("{f}", fileName)
                .Replace("{n}", lineNumber.ToString())
                .Replace("{m}", message);
            return formatted;
        }

        /// <summary>
        /// Запись лог-сообщения в консоль и/или файл.
        /// </summary>
        private void WriteLog(string message, LogLevel level)
        {
            lock (_lock)
            {
                if (_logToConsole)
                {
                    Console.WriteLine(message);
                }

                if (_logToFile)
                {
                    string uniqueFileName = $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}_{_logFileName}";
                    if (!_fileWriters.ContainsKey(uniqueFileName))
                    {
                        _fileWriters[uniqueFileName] = new StreamWriter(File.Open(uniqueFileName, FileMode.Append, FileAccess.Write, FileShare.Read), Encoding.UTF8)
                        {
                            AutoFlush = true
                        };
                    }
                    _fileWriters[uniqueFileName].WriteLine(message);
                }
            }
        }

        /// <summary>
        /// Закрытие всех потоков записи в файлы.
        /// </summary>
        public void Close()
        {
            foreach (var writer in _fileWriters.Values)
            {
                writer.Close();
            }
        }
    }

    /// <summary>
    /// Макросы для удобного логирования.
    /// </summary>
    public static class LogMacros
    {
        public static void LOGT(string message) => Logger.Instance.Log(LogLevel.TRACE, message);
        public static void LOGD(string message) => Logger.Instance.Log(LogLevel.DEBUG, message);
        public static void LOGI(string message) => Logger.Instance.Log(LogLevel.INFO, message);
        public static void LOGW(string message) => Logger.Instance.Log(LogLevel.WARNING, message);
        public static void LOGE(string message) => Logger.Instance.Log(LogLevel.ERROR, message);
    }
}
