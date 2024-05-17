namespace EpilLaserLab.Server.Helpers
{
    public class DBConnectData
    {
        // Server=localhost;Database=epillaserlab;User=root;Password=qwerty;Port=3306
        public static string Server = "127.0.0.1";
        public static string Database = "epillaserlab";
        public static string User = "root";
        public static string Password = "qwerty";
        public static string Port = "3306";

        public static string ConnectString => $"Server={Server};Database={Database};User={User};Password={Password};Port={Port}";
    }
}
