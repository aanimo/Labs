using System;

namespace LoggingSystem
{
    class LoggerTest
    {
        static void Main(string[] args)
        {
            Logger.Instance.Configure(LogLevel.DEBUG, logToConsole: true, logToFile: true, logFileName: "app.log");

            Logger.Instance.SetCustomTemplate("{t} | {L} -> {f}:{n} -> {m}");

            Logger.Instance.Log(LogLevel.TRACE, "Сообщение трассировки", "LoggerTest.cs", 12);
            Logger.Instance.Log(LogLevel.DEBUG, "Отладка приложения", "LoggerTest.cs", 13);
            Logger.Instance.Log(LogLevel.INFO, "Информационное сообщение", "LoggerTest.cs", 14);
            Logger.Instance.Log(LogLevel.WARNING, "Предупреждающее сообщение на {0}", "line 42", "LoggerTest.cs", 15);
            Logger.Instance.Log(LogLevel.ERROR, "Ошибка произошла в {0} at line {1}", "LoggerTest.cs", 25);

            LogMacros.LOGT("Лог трассировки с макросом");
            LogMacros.LOGD("Отладочный лог с макросом");
            LogMacros.LOGI("Информационный лог с макросом");
            LogMacros.LOGW("Предупреждающий лог с макросом");
            LogMacros.LOGE("Ошибка в логе с макросом");

            Logger.Instance.Close();
        }
    }
}
