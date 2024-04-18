namespace EpilLaserLab.Server.Dtos.Auths
{
    public class RegisterClientsDto : RegisterDto
    {
        public string Name { get; set; }
        public string Phone {  get; set; }

        public override int RoleId => 2;
    }

}
