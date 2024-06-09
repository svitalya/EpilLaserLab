// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EpilLaserLab.Server.Dtos.Dashboard
{
    public class NavItem
    {
        public string? Svg { get; set; }
        public string Text { get; set; } = string.Empty;

        public string? Link { get; set; }

        public object Params { get; set; }

        public List<NavItem>? Dropdown { get; set; }
    }
}
