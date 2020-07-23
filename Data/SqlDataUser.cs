using send_working_day.Actions;
using send_working_day.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace send_working_day.Data
{
    public class SqlDataUser
    {
        public static List<User> getUser(string connectString, int unixTimestampEnd)
        {
            //Get Emails Send
            List<User> allUser = new List<User>();
            List<string> allEmail = new List<string>();
            string _email = ConfigurationManager.AppSettings["emails"];
            string[] _emails = _email.Split(';');
            foreach (string emailItem in _emails)
            {
                allEmail.Add(emailItem);
            }
            //Get Emails Not Send
            List<string> allMailx = new List<string>();
            string mailx = ConfigurationManager.AppSettings["emailx"];
            string[] listMailx = mailx.Split(';');
            foreach (string mailxItem in listMailx)
            {
                allMailx.Add(mailxItem);
            }
            //Get Departments
            List<int> allDepartment = new List<int>();
            string _department = ConfigurationManager.AppSettings["Departments"];
            string[] _departments = _department.Split(';');
            foreach (string departmentItem in _departments)
            {
                allDepartment.Add(Convert.ToInt32(departmentItem));
            }

            try
			{
                using (WorkingDayDBDataContext db = new WorkingDayDBDataContext(connectString))
                {
                    var queryAllUser = from userItem in db.TB_USERs
                                       where userItem.nEndDate > unixTimestampEnd
                                            //&& userItem.nDepartmentIdn == 1 //CNTT //&& userItem.sUserID == "1452"
                                            && !(allMailx.Contains(userItem.sEmail))
                                            && !(allMailx.Contains("All"))
                                            && (allEmail.Contains(userItem.sEmail) || allEmail.Contains("All"))
                                            && (allDepartment.Contains(userItem.nDepartmentIdn) || allDepartment.Contains(0))
                                            && userItem.sEmail != "" 
                                            && userItem.sEmail != null
                                       select userItem;

                    foreach (var user in queryAllUser)
                    {
                        User userInf = new User();
                        userInf.UserName = user.sUserName;
                        userInf.DepartmentId = user.nDepartmentIdn;
                        userInf.TelNumber = user.sTelNumber;
                        userInf.Email = user.sEmail;
                        userInf.UserId = Convert.ToInt32(user.sUserID);
                        userInf.StartDate = user.nStartDate;
                        userInf.EndDate = user.nEndDate;

                        allUser.Add(userInf);
                    }
                }
                
            }
			catch (Exception e)
			{
				Console.WriteLine("Get users error!");
				Console.WriteLine(e.Message);

                WriteLog.WriteLogFile($"Get users error: {e.Message}", 0);
            }
			return allUser;
		}
    }
}
