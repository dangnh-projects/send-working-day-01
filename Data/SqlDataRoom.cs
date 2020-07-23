using send_working_day.Actions;
using send_working_day.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace send_working_day.Data
{
    public class SqlDataRoom
    {
        public static List<Room> getRoomInf(string connectString)
        {
            List<Room> allRoom = new List<Room>();
            
            try
            {
                using (WorkingDayDBDataContext db = new WorkingDayDBDataContext(connectString))
                {
                    var queryAllRoom = from roomItem in db.TB_READERs
                                       select roomItem;

                    foreach (var room in queryAllRoom)
                    {
                        Room roomInf = new Room();
                        string nameRoomInf = "";
                        roomInf.ReaderId = room.nReaderIdn;
                        nameRoomInf = getNameRoom(room.nReaderIdn);
                        roomInf.Name = nameRoomInf;

                        allRoom.Add(roomInf);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Get rooms error!");
                Console.WriteLine(e.Message);

                WriteLog.WriteLogFile($"Get rooms error: {e.Message}", 0);
            }
            return allRoom;
        }
        public static string getNameRoom(int roomID)
        {
            string roomName = "";
            switch(roomID)
            {
                case 539572592:
                    roomName = "Floor 06 - Office";
                    break;
                case 539574093:
                    roomName = "Floor 06 - VIP01";
                    break;
                case 539574085:
                    roomName = "Floor 06 - VIP04";
                    break;
                case 539574091:
                    roomName = "Floor 06 - Office";
                    break;
                case 539548767:
                    roomName = "Floor 09 - VIP04";
                    break;
                case 539548764:
                    roomName = "Floor 09 - VIP03";
                    break;
                case 539548755:
                    roomName = "Floor 09 - Office";
                    break;
                case 539548766:
                    roomName = "Floor 09 - VIP01";
                    break;
                case 539548638:
                    roomName = "Floor 09 - VIP02";
                    break;
                case 539567604:
                    roomName = "Floor 09 - VIP04";
                    break;
                case 539551591:
                    roomName = "Floor 10 - Office";
                    break;
                case 539551581:
                    roomName = "Floor 10 - VIP01";
                    break;
                case 539551598:
                    roomName = "Floor 10 - VIP02";
                    break;
                case 539551597:
                    roomName = "Floor 10 - VIP03";
                    break;
                case 539551477:
                    roomName = "Floor 10 - VIP04";
                    break;
                case 539561881:
                    roomName = "Floor 10 - VIP05";
                    break;
                case 539561882:
                    roomName = "Floor 10 - VIP06";
                    break;
                case 539551595:
                case 539562799:
                case 539551602:
                case 539557750:
                    roomName = "Floor 11";
                    break;
            }
            return roomName;
        }
    }
}
