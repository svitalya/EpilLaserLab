﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EpilLaserLab.Server.Models
{
    public class Role
    {
        public int RoleId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public uint LevelAccess { get; set; }

        [JsonIgnore]
        public ICollection<User> Users { get; set; } = [];
    }
}
