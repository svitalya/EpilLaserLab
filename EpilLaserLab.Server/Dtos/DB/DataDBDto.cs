namespace EpilLaserLab.Server.Dtos.DB;

public class DataDBDto
{
    public string Server { get; set; }
    public string Database { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public string Port { get; set; }

    public string ConnectString => $"Server={Server};Database={Database};User={User};Password={Password};Port={Port}";
}
