using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace send_working_day.Utils
{
    public class ConvertDatetime
    {
        public static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            origin = origin.AddSeconds(unixTimeStamp);
            return origin;
        }
    }
}
