using System;

namespace send_working_day.Models
{
    public class WorkingDay
    {
        public int DateTime { set; get; }
        public int ReaderId { set; get; }
        public int EventId { set; get; }
        public int UserID { set; get; }
        public DateTime DayLog { set; get; }

    }
}
