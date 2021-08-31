using System;
using System.Collections.Generic;

#nullable disable

namespace WorkoutHunterV2.Models.DbModels
{
    public partial class CharacterItemSkill
    {
        public string Uid { get; set; }
        public string Items { get; set; }
        public string Skills { get; set; }
        public string ChaPic { get; set; }
        public int? Money { get; set; }
        public int? RawPoint { get; set; }
        public int? NowSkill{ get; set; }
        public int? NowItem { get; set; }

        public virtual UserInfo UidNavigation { get; set; }
    }
}
