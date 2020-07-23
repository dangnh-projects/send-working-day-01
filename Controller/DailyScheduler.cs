using send_working_day.Actions;
using send_working_day.Data;
using send_working_day.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;

namespace send_working_day.Controller
{
    public class DailyScheduler
    {
        public static void DailySchedulerData(bool date_session)
        {
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            string connectString = ConfigurationManager.ConnectionStrings["send_working_day.Properties.Settings.BioStarConnectionString"].ToString();
            
            bool weekend = CheckingDay.Weekend();
            if (!weekend)
            {
                try {
                    int eventId = 55;
                    int unixTimestampEnd = (Int32)(DateTime.Today.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    //Unix Timestamp Today 24:00 PM
                    unixTimestampEnd = unixTimestampEnd + 86400;
                    List<User> allUser = SqlDataUser.getUser(connectString, unixTimestampEnd);
                    List<WorkingDay> workingDays = SqlDataDayLog.getDayLog(connectString, eventId);
                    List<Room> listRoom = SqlDataRoom.getRoomInf(connectString);

                    if (allUser.Count > 0)
                    {
                        foreach (User userItem in allUser)
                        {
                            //List<WorkingDay> workingDays = SqlDataDayLog.getDayLog(connectString, eventId, userItem);
                            //List<Room> listRoom = SqlDataRoom.getRoomInf(connectString);
                            List<WorkingDay> userDays = workingDays.Where(e => e.UserID == userItem.UserId).ToList();

                            if (date_session == SessionDay.morning)
                            {
                                //Has data morning
                                if(userDays.Count > 0)
                                {
                                    WorkingDay dayIdMin = userDays.FirstOrDefault(e => e.DateTime == userDays.Min(ex => ex.DateTime));

                                    string mindate = dayIdMin.DayLog.ToString("hh:mm tt", CultureInfo.InvariantCulture);
                                    string maxdate = null;
                                    string nowDate = DateTime.Today.ToString("dd/MM/yyyy");

                                    string nameRoomIn = listRoom.FirstOrDefault(e => e.ReaderId == dayIdMin.ReaderId).Name;

                                    SendMail.SendMailDaily(date_session, mindate, maxdate, nowDate, userItem, nameRoomIn, "");
                                }
                                //Hasn't data morning
                                else
                                {
                                    string mindate = null;
                                    string maxdate = null;
                                    string nowDate = DateTime.Today.ToString("dd/MM/yyyy");

                                    SendMail.SendMailDaily(date_session, mindate, maxdate, nowDate, userItem, "", "");
                                }
                            }    
                            else if(date_session == SessionDay.night)
                            {
                                //has data night
                                if (userDays.Count > 1)
                                {
                                    WorkingDay dayIdMin = userDays.FirstOrDefault(e => e.DateTime == userDays.Min(ex => ex.DateTime));
                                    WorkingDay dayIdMax = userDays.FirstOrDefault(e => e.DateTime == userDays.Max(ex => ex.DateTime));

                                    string mindate = dayIdMin.DayLog.ToString("hh:mm tt", CultureInfo.InvariantCulture);
                                    string maxdate = dayIdMax.DayLog.ToString("hh:mm tt", CultureInfo.InvariantCulture);
                                    string nowDate = DateTime.Today.ToString("dd/MM/yyyy");

                                    string nameRoomIn = listRoom.FirstOrDefault(e => e.ReaderId == dayIdMin.ReaderId).Name;
                                    string nameRoomOut = listRoom.FirstOrDefault(e => e.ReaderId == dayIdMax.ReaderId).Name;

                                    SendMail.SendMailDaily(date_session, mindate, maxdate, nowDate, userItem, nameRoomIn, nameRoomOut);
                                }
                                //has data night one
                                else if (userDays.Count == 1)
                                {
                                    WorkingDay dayIdMin = userDays.FirstOrDefault();

                                    string mindate = dayIdMin.DayLog.ToString("hh:mm tt", CultureInfo.InvariantCulture);
                                    string maxdate = null;
                                    string nowDate = DateTime.Today.ToString("dd/MM/yyyy");

                                    string nameRoomIn = listRoom.FirstOrDefault(e => e.ReaderId == dayIdMin.ReaderId).Name;

                                    SendMail.SendMailDaily(date_session, mindate, maxdate, nowDate, userItem, nameRoomIn, "");
                                }
                                //hasn't data night
                                else 
                                {
                                    string mindate = null;
                                    string maxdate = null;
                                    string nowDate = DateTime.Today.ToString("dd/MM/yyyy");

                                    SendMail.SendMailDaily(date_session, mindate, maxdate, nowDate, userItem, "", "");
                                }
                            }    
                            
                        }
                    }    
                }
                catch (Exception e)
                {
                    Console.WriteLine("Data send error!!!");
                    Console.WriteLine(e.Message);

                    WriteLog.WriteLogFile($"Data send error: {e.Message}",0);
                }
            }
        }
    }
}
