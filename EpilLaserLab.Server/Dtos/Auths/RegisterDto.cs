namespace EpilLaserLab.Server.Dtos.Auths
{
    public abstract class RegisterDto
    {
        public string Login {  get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }   

        public abstract int RoleId { get;}
    }
}
 