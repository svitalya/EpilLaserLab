using EpilLaserLab.Server.Dtos.DB;

namespace EpilLaserLab.Server.Helpers
{
    public static class DBConnectData
    {
        // Server=localhost;Database=epillaserlab;User=root;Password=qwerty;Port=3306
        public static string Server = "127.0.0.1";
        public static string Database = "epillaserlab";
        public static string User = "root";
        public static string Password = "qwerty";
        public static string Port = "3306";

        public static string ConnectString => Data.ConnectString;

        public static DataDBDto Data
        {
            get
            {
                return new()
                {
                    Server = Server,
                    Database = Database,
                    User = User,
                    Password = Password,
                    Port = Port,
                };
            }

            set
            {
                Server = value.Server;
                Database = value.Database;
                User = value.User;
                Password = value.Password;
                Port = value.Port;
            }
        }
    }
}
