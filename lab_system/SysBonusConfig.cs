using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_system
{
    public class SysBonusConfig
    {
        public int BasicSingleIncome { get; set; }
        public int BasicSingleBonuslv1 { get; set; }
        public int BasicSingleBonuslv2 { get; set; }
        public int BasicSingleBonuslv3 { get; set; }
        public double BasicSingleBonuslv1_2Percent { get; set; }
        public double BasicSingleBonuslv2_3Percent { get; set; }
        public double BasicSingleBonuslv3_Percent { get; set; }
        public int BasicFamilyIncome { get; set; }
        public int BasicFamilyBonuslv1 { get; set; }
        public int BasicFamilyBonuslv2 { get; set; }
        public double BasicFamilyBonuslv1_2Percent { get; set; }
        public double BasicFamilyBonuslv2_Percent { get; set; }
    }
}
