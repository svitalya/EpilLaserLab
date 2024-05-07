namespace EpilLaserLab.Server.Dtos.Auths
{
    public class UserInfoDto
    {
        public int UserId { get; set; }
        public string Login { get; set; } 

        public string Email { get; set; }

        public string Role { get; set; }

        public string RoleName { get; set; }
    }
}
