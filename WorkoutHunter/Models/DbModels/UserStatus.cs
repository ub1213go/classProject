using System;
using System.Collections.Generic;

#nullable disable

namespace WorkoutHunterV2.Models.DbModels
{
    public partial class UserStatus
    {
        public string Uid { get; set; }
        public int? Strength { get; set; }
        public int? Vitality { get; set; }
        public int? Agility { get; set; }

        public virtual UserInfo UidNavigation { get; set; }
    }
}
