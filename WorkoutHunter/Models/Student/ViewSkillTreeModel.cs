using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutHunterV2.Models.DbModels;

namespace WorkoutHunterV2.Models.Student
{
    public class ViewSkillTreeModel
    {
        public string Rank { get; set; }
        public IQueryable<Skill> Skills { get; set; }
        public int? rawPoint { get; set; }
        public int? Strength { get; set; }
        public int? Vitality { get; set; }
        public int? Agility { get; set; }
        public int? nowSkill { get; set; }
        public List<int> SkillList{ get; set; }
    }
}
