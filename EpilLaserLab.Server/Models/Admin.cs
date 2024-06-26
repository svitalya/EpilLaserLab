﻿using System.Text.Json.Serialization;

namespace EpilLaserLab.Server.Models
{
    public class Admin
    {
        public int AdminId { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
