using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace BusinessLogic
{
    public class Log
    {
        private readonly bool _isLoggingActivated;
        private bool _loggingFileCreated;
        private readonly string _completeLogPath;

        public Log()
        {
            var logValue = ConfigurationManager.AppSettings.Get("log");
            var logPath = ConfigurationManager.AppSettings.Get("loggingPath");
            _isLoggingActivated = logValue != "false";
            if (_isLoggingActivated && !string.IsNullOrEmpty(logPath))
            {
                var fileName = BuildFileName();
                _completeLogPath = Path.Combine(logPath, fileName);
            }
        }

        public bool IsLoggingActivated()
        {
            return _isLoggingActivated;
        }

        public void ActivateLogging()
        {
            try
            {
                if (!_isLoggingActivated)
                {
                    string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    string configFile = Path.Combine(appPath, "App.config");
                    ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                    configFileMap.ExeConfigFilename = configFile;
                    Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap,
                        ConfigurationUserLevel.None);
                    config.AppSettings.Settings["log"].Value = "true";
                    config.Save();
                    Console.WriteLine("Logging is activated now");
                }

            }
            catch (Exception exception)
            {

                Console.WriteLine(exception.Message);
            }

        }

        public void DeActivateLogging()
        {
            try
            {
                if (_isLoggingActivated)
                {
                    string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    string configFile = Path.Combine(appPath, "App.config");
                    ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                    configFileMap.ExeConfigFilename = configFile;
                    Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap,
                        ConfigurationUserLevel.None);
                    config.AppSettings.Settings["log"].Value = "false";
                    config.Save();
                    Console.WriteLine("Logging is deactivated");
                }
            }
            catch (Exception exception)
            {

                Console.WriteLine(exception.Message);
            }

        }

        public void SetLoggingPath(string path)
        {
            try
            {
                if (!string.IsNullOrEmpty(path))
                {
                    string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    string configFile = Path.Combine(appPath, "App.config");
                    ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                    configFileMap.ExeConfigFilename = configFile;
                    Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap,
                        ConfigurationUserLevel.None);
                    config.AppSettings.Settings["loggingPath"].Value = path;
                    config.Save();
                    Console.WriteLine("New path is saved");
                }
            }
            catch (Exception exception)
            {

                Console.WriteLine(exception.Message);
            }

        }

        public void LogIt(string message)
        {
            try
            {
                File.AppendAllText(_completeLogPath,
                    string.Format("{0}    {1}", DateTime.UtcNow, message + Environment.NewLine));
            }
            catch (Exception exp)
            {

                Console.WriteLine(exp.Message);
            }

        }

        public void LogIt(Exception exception)
        {
            try
            {
                File.AppendAllText(_completeLogPath,
                    string.Format("{0}    {1}", DateTime.UtcNow, exception.Message + Environment.NewLine));
            }
            catch (Exception exp)
            {

                Console.WriteLine(exp.Message);
            }
        }

        private string BuildFileName()
        {
            string year = DateTime.UtcNow.Year.ToString();
            string month = DateTime.UtcNow.Month.ToString();
            string day = DateTime.UtcNow.Day.ToString();
            return "Log" + "-" + month + "-" + day + "-" + year + ".txt";
        }
    }
}
