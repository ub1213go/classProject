using System;
using System.Collections.Generic;

#nullable disable

namespace WorkoutHunterV2.Models.DbModels
{
    public partial class UserInfo
    {
        public string Uid { get; set; }
        public string Email { get; set; }
        public string PassWord { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string SignDate { get; set; }
        public string Class { get; set; }
        public string Salt { get; set; }
        public string PT { get; set; }
        public string userPic { get; set; }

        public virtual CharacterItemSkill CharacterItemSkill { get; set; }
        public virtual GameProgress GameProgress { get; set; }
        public virtual UserStatus UserStatus { get; set; }
    }
}
