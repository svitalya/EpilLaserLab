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
    }
}
