﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceLogger
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            #if !DEBUG
            {
            
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new LoggerService()
                };
                ServiceBase.Run(ServicesToRun);
            
            }
            #else
            {
                {
                    new LoggerService().OnDebug();
                    System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
                }
            }
            #endif
        }
    }
}
