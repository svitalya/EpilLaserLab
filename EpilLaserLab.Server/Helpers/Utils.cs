namespace EpilLaserLab.Server.Helpers
{
    public static class Utils
    {
        public static DateOnly ToDateOnly(this DateTime datetime) => DateOnly.FromDateTime(datetime);

        public static DateTime NowDate()
        {
            DateOnly dateo = DateTime.Now.ToDateOnly();
            DateTime now = dateo.ToDateTime(new TimeOnly(0, 0, 0));
            return now;
        }

        public static int GetMinuts(this string inputStr)
        {
            string[] timeData = inputStr.Split(":");


            int hours, minuts;
            if (timeData.Length == 1)
            {
                hours = int.Parse(timeData[0]);
                minuts = 0;
            }
            else if(timeData.Length == 2)
            {
                hours = int.Parse(timeData[0]);
                minuts = int.Parse(timeData[1]);
            }
            else
            {
                throw new Exception("Utils.GetMinuts: неправильный формат");
            }


            return hours * 60 + minuts;
        }
    }
}
