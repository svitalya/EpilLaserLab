using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EpilLaserLab.Server.Models;

public class User
{

    public int UserId { get; set; }

    [Required]
    [MaxLength(64)]
    public string Login {  get; set; } = string.Empty;

    [Required]
    [JsonIgnore]
    public string PasswordHash { get; set; } = string.Empty;

    public string? Email { get; set; }

    [Required]
    public int RoleId { get; set; }

    public virtual Role? Role { get; set; }

}
