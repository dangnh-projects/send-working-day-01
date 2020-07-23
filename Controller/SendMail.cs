//using MailKit.Net.Smtp;
using MailKit.Net.Smtp;
using MimeKit;
using send_working_day.Actions;
using send_working_day.Models;
using System;
using System.Net;
using System.Text;

namespace send_working_day.Controller
{
    public class SendMail
    {
        public static void SendMailDaily(bool date_session, string mindate, string maxdate, string nowDate, User user, string nameRoomIn, string nameRoomOut)
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            try
            {
                string bodyMessage = "";
                var message = new MimeMessage();

                if (date_session == SessionDay.morning)
                {
                    bodyMessage = Morning(bodyMessage, mindate, nowDate, user.UserName, nameRoomIn, nameRoomOut);
                    if (mindate != null)
                        message.Subject = $"Daily check fingerprint notifications in {nowDate}";
                    else
                        message.Subject = $"Missing check fingerprint in {nowDate}";
                }

                else if (date_session == SessionDay.night)
                {
                    bodyMessage = Night(bodyMessage, mindate, maxdate, nowDate, user.UserName, nameRoomIn, nameRoomOut);
                    if (mindate == null || maxdate == null)
                        message.Subject = $"Missing check fingerprint in {nowDate}";
                    else
                        message.Subject = $"Daily check fingerprint notifications in {nowDate}";
                }

                message.From.Add(new MailboxAddress("Time Keeper", "timekeeper@nhg.vn"));
                message.To.Add(new MailboxAddress(user.UserName, user.Email));
                //message.To.Add(new MailboxAddress(user.UserName, user.Email));
                //message.Subject = $"You missing check fingerprint in {nowDate}";

                message.Body = new TextPart("plain")
                {
                    Text = bodyMessage
                };
                Console.WriteLine(bodyMessage);
                WriteLog.WriteLogFile(bodyMessage, 0);

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp-mail.outlook.com", 587, false);

                    //Note: only needed if the SMTP server requires authentication
                    client.Authenticate("timekeeper@nhg.vn", "vOPWFiV54Wof06UvOKLA");
                    //client.Authenticate("dangnh@nhg.vn", "Santazero1");

                    client.Send(message);
                    client.Disconnect(true);

                    WriteLog.WriteLogFile($"Sending Success!", 1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Sending mail error!!!");
                Console.WriteLine(e.Message);

                WriteLog.WriteLogFile($"Sending mail error: {e.Message}", 0);
            }
		}
        public static string Morning(string bodyMessage, string mindate, string nowDate, string userName, string nameRoomIn, string nameRoomOut)
        {
            try
            {
                var bodyText = new StringBuilder();
                bodyText.Append($"Good morning {userName},\r\n\r\n");
                if(mindate != null) bodyText.Append($"You check-in at {mindate} ");
                else bodyText.Append($"Please check-in, you missing check fingerprint ");
                bodyText.Append($"in {nowDate}. \r\n\r\n");
                if(nameRoomIn != "" && nameRoomOut != "") bodyText.Append($"Location In-Out: {nameRoomIn} - {nameRoomOut}. \r\n\r\n");
                else if (nameRoomIn != "") bodyText.Append($"Location: {nameRoomIn} \r\n\r\n");
                bodyText.Append($"Thank you and have a nice day!\r\n\r\n");
                bodyMessage = bodyText.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("Morning mail error!!!");
                Console.WriteLine(e.Message);

                WriteLog.WriteLogFile($"Morning mail error: {e.Message}", 0);
            }
            return bodyMessage;
        }

        public static string Night(string bodyMessage, string mindate, string maxdate, string nowDate, string userName, string nameRoomIn, string nameRoomOut)
        {
            try {
                var bodyText = new StringBuilder();
                bodyText.Append($"Good evening {userName},\r\n\r\n");
                if (mindate != null && maxdate != null)
                {
                    bodyText.Append($"You check-in at {mindate} ");
                    bodyText.Append($"and check-out at {maxdate} ");
                }
                else if (mindate != null && maxdate == null) 
                    bodyText.Append($"You missing onetime check, you check at {mindate} ");
                else bodyText.Append($"You missing check fingerprint ");
                bodyText.Append($"in {nowDate}. \r\n\r\n");
                if (nameRoomIn != "" && nameRoomOut != "")
                {
                    if(nameRoomIn == nameRoomOut) bodyText.Append($"Location In-Out: {nameRoomIn}. \r\n\r\n");
                    else bodyText.Append($"Location In-Out: {nameRoomIn} <> {nameRoomOut}. \r\n\r\n");
                }
                else if (nameRoomIn != "") bodyText.Append($"Location: {nameRoomIn} \r\n\r\n");
                bodyText.Append($"Thank you!\r\n\r\n");
                bodyMessage = bodyText.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("Night mail error!!!");
                Console.WriteLine(e.Message);

                WriteLog.WriteLogFile($"Night mail error: {e.Message}", 0);
            }
            return bodyMessage;
        }
    }
}
