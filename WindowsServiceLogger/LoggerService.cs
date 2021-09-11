using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Configuration;
using System.Timers;
using System.IO;

namespace WindowsServiceLogger
{
    public partial class LoggerService : ServiceBase
    {
        private Timer timer;

        public LoggerService()
        {
            InitializeComponent();
            eventLog.Log = "WindowsLoggerService";
            eventLog.Source = "WindowsLoggerServiceSource";
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer
            {
                Interval = (Convert.ToInt32(ConfigurationManager.AppSettings["TimeInterval"]) * 1000)
            };
            timer.Elapsed += new ElapsedEventHandler(onElapsedTime);
            eventLog.WriteEntry("Service started successfully", EventLogEntryType.Information);
            textLog($"Service started at {DateTime.Now}");
            timer.Start();
        }

        private void onElapsedTime(object sender, ElapsedEventArgs e)
        {
            try
            {
                timer.Stop();

                //Any operation or API call can be made here.

                textLog($"Service called at {DateTime.Now}");
            }
            catch (Exception ex)
            {
                eventLog.WriteEntry($"\n Exception Message: {ex.Message} \n StackTrace: {ex.StackTrace} \n Inner Exception: {ex.InnerException}", EventLogEntryType.Error);
            }
            finally
            {
                timer.Start();
            }
        }

        protected override void OnStop()
        {
            textLog($"Service stopped at {DateTime.Now}");
            eventLog.WriteEntry("Service stopped", EventLogEntryType.Information);
        }

        private void textLog(string message)
        {
            string logFolderPath;
            string logFilePath;

            #if !DEBUG
            {
                logFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ServiceLogs");
            }
            #else
            {
                logFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ServiceLogs");
            }
            #endif


            if (!Directory.Exists(logFolderPath))
            {
                Directory.CreateDirectory(logFolderPath);
            }

            logFilePath = Path.Combine(logFolderPath, $"log_{DateTime.Now.ToString("dd_MMM_yyyy")}.txt");
            if (!File.Exists(logFilePath))
            {
                using (StreamWriter sw = File.CreateText(logFilePath))
                {
                    sw.WriteLine("================================================================================");
                    sw.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(logFilePath))
                {
                    sw.WriteLine("================================================================================");
                    sw.WriteLine(message);
                }
            }
        }

        internal void OnDebug()
        {
            OnStart(null);
        }
    }
}
