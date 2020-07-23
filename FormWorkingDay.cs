using send_working_day.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace send_working_day
{
    public partial class FormWorkingDay : Form
    {
        public FormWorkingDay()
        {
            InitializeComponent();
        }

        private void FormWorkingDay_Load(object sender, EventArgs e)
        {
            Form form = (Form)sender;
            form.ShowInTaskbar = false;
            form.Opacity = 0;

            bool morning = true;

            int morningHour = Convert.ToInt32(ConfigurationManager.AppSettings["morningHour"]);
            int morningMinute = Convert.ToInt32(ConfigurationManager.AppSettings["morningMinute"]);
            int nightHour = Convert.ToInt32(ConfigurationManager.AppSettings["nightHour"]);
            int nightMinute = Convert.ToInt32(ConfigurationManager.AppSettings["nightMinute"]);
            double days = Convert.ToDouble(ConfigurationManager.AppSettings["days"]);

            Scheduler.IntervalInDays(morningHour, morningMinute, days,
            () =>
            {
                DailyScheduler.DailySchedulerData(morning);
            });


            Scheduler.IntervalInDays(nightHour, nightMinute, days,
            () =>
            {
                DailyScheduler.DailySchedulerData(!morning);
            });

            //DailyScheduler.DailySchedulerData(morning);
            //DailyScheduler.DailySchedulerData(!morning);
        }
    }
}
