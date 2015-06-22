using System;
using System.Collections.Generic;


namespace DairyFarm.Service
{
    public class Util
    {
        public static ICollection<string> Hours
        {
            get
            {
                return new List<string>
                {
                    "23:30","23:00","22:30","22:00","21:30","21:00","20:30","20:00", 
                    "19:30","19:00","18:30","18:00","17:30","17:00","16:30","16:00",
                    "15:30","15:00","14:30","14:00","13:30","13:00","12:30","12:00",
                    "11:30","11:00","10:30","10:00","09:30","09:00","08:30","08:00",
                    "07:30","07:00","06:30","06:00","05:30","05:00","04:30","03:00",
                    "02:30","02:00","01:30","01:00","0:30","00:00"
                };
            }
        }

        public static int GetRank(string sex, DateTime date)
        {
            var age =(( DateTime.Now.Year - date.Year) * 12) + DateTime.Now.Month - date.Month;
            if (age <= 6)
            {
                return 0;
            }else if(age >6 && age<=12)
            {
                 return 1;
            }
            else if ((age > 12 && age <= 24) || (age > 24 && sex == "M"))
            {
                return 2;
            }else if(age> 24)
            {
                return 4;
            }
            return 100;
        }
    }
}