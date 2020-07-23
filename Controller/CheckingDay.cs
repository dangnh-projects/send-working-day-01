using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace send_working_day.Controller
{
    public class CheckingDay
    {
        public static bool Weekend()
        {
            bool weekend = false;
            DateTime today = DateTime.Today;
            if (today.DayOfWeek == DayOfWeek.Saturday || today.DayOfWeek == DayOfWeek.Sunday)
                weekend = true;
            else weekend = false;
            return weekend;
        }
    }
}
