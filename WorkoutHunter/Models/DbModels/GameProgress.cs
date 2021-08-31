using System;
using System.Collections.Generic;

#nullable disable

namespace WorkoutHunterV2.Models.DbModels
{
    public partial class GameProgress
    {
        public string Uid { get; set; }
        public string StartTime { get; set; }
        public string SavePoint { get; set; }

        public virtual UserInfo UidNavigation { get; set; }
    }
}
