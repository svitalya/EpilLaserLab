namespace EpilLaserLab.Server.Helpers
{
    public static class Utils
    {
        public static DateOnly ToDateOnly(this DateTime datetime) => DateOnly.FromDateTime(datetime);
    }
}
