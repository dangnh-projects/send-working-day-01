using send_working_day.Controller;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace send_working_day
{
    class Program
    {
        static void Main(string[] args)
        {
            String thisprocessname = Process.GetCurrentProcess().ProcessName;

            if (Process.GetProcesses().Count(p => p.ProcessName == thisprocessname) > 1)
                return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormWorkingDay());
            // For Interval in Seconds 
            // This Scheduler will start at 11:10 and call after every 15 Seconds
            // IntervalInSeconds(start_hour, start_minute, seconds)
            // MyScheduler.IntervalInSeconds(11, 10, 15,
            // () =>
            // {
            //     Console.WriteLine("Second");
            //     Console.WriteLine("//here write the code that you want to schedule");
            // });

            // For Interval in Minutes 
            // This Scheduler will start at 22:00 and call after every 30 Minutes
            // IntervalInSeconds(start_hour, start_minute, minutes)
            // MyScheduler.IntervalInMinutes(22, 00, 30,
            // () =>
            // {

            //     Console.WriteLine("//here write the code that you want to schedule");
            // });

            // For Interval in Hours 
            // This Scheduler will start at 9:44 and call after every 1 Hour
            // IntervalInSeconds(start_hour, start_minute, hours)
            //Scheduler.IntervalInHours(9, 18, 1,
            //() =>
            //{
            //    //Console.WriteLine("Now is: " + DateTime.Now);
            //    DailyScheduler.DailySchedulerData();
            //});


            // For Interval in Seconds 
            // This Scheduler will start at 17:22 and call after every 3 Days
            // IntervalInSeconds(start_hour, start_minute, days)
            //Scheduler.IntervalInDays(9, 0, 1,
            //() =>
            //{
            //    DailyScheduler.DailySchedulerData(morning);
            //});

            //Scheduler.IntervalInDays(20, 0, 1,
            //() =>
            //{
            //    DailyScheduler.DailySchedulerData(!morning);
            //});

            // For Interval in Minutes 
            // This Scheduler will start at 22:00 and call after every 30 Minutes
            // IntervalInSeconds(start_hour, start_minute, minutes)
            //Scheduler.IntervalInMinutes(8, 40, 5,
            //() =>
            //{
            //    DailyScheduler.DailySchedulerData(morning);
            //});

            //Console.ReadLine();
        }
    }
}
