using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutHunterV2.Models.DbModels
{
    public partial class ForIndex
    {
        [Key]
        public string Uid { get; set; }
        public string Class { get; set; }
        public int? Strength { get; set; }
        public int? Vitality { get; set; }
        public int? Agility { get; set; }
        public string Items { get; set; }
        public string Skills { get; set; }
        public string ChaPic { get; set; }
        public int? Money { get; set; }
        public int? rawPoint { get; set; }
        public int? nowSkill { get; set; }

    }
}
