using System;

namespace send_working_day.Models
{
    public class User
    {
        public string UserName { set; get; }
        public int DepartmentId { set; get; }
        public string TelNumber { set; get; }
        public string Email { set; get; }
        public int UserId { set; get; }
        public int StartDate { set; get; }
        public int EndDate { set; get; }
    }
}
