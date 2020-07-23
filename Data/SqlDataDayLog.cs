using send_working_day.Models;
using send_working_day.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using send_working_day.Actions;

namespace send_working_day.Data
{
    public class SqlDataDayLog
    {
        public static List<WorkingDay> getDayLog(string connectString, int eventId)
        {
			//Unix Timestamp Today 00:00 AM
			int unixTimestampStart = (Int32)(DateTime.Today.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
			//Unix Timestamp Today 24:00 PM
			int unixTimestampEnd = unixTimestampStart + 86400;
            List<WorkingDay> workingsLog = new List<WorkingDay>();

			try
			{
				using (WorkingDayDBDataContext db = new WorkingDayDBDataContext(connectString))
                {
                    var queryDayLog = from dayLog in db.TB_EVENT_LOGs
                                      where dayLog.nUserID > 0
                                        && (dayLog.nEventIdn == 23 || (30 < dayLog.nEventIdn && dayLog.nEventIdn < 63) || dayLog.nEventIdn == 71)
                                        && unixTimestampStart < dayLog.nDateTime && dayLog.nDateTime < unixTimestampEnd
                                      select dayLog;
					foreach(var dayInf in queryDayLog)
					{
                        int timeItem = dayInf.nDateTime;
                        DateTime dateCheck = ConvertDatetime.UnixTimeStampToDateTime(timeItem);

                        WorkingDay workingDay = new WorkingDay();
                        workingDay.DateTime = dayInf.nDateTime;
                        workingDay.ReaderId = dayInf.nReaderIdn;
                        workingDay.EventId = dayInf.nEventIdn;
                        workingDay.UserID = dayInf.nUserID;
                        workingDay.DayLog = dateCheck;

                        workingsLog.Add(workingDay);
                    }
                }
				}
				catch (Exception e)
				{
					Console.WriteLine("Get day log error!");
					Console.WriteLine(e.Message);

                    WriteLog.WriteLogFile($"Get day log error: {e.Message}", 0);
				}
			return workingsLog;
		}
		
	}
}
