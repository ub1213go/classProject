using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_system
{
    public class SysOrderPara
    {
        // 易貨收益
        public int BasicSingleIncome { get; set; }

        // 易貨分紅
        // 參數條件: lv1 < lv2 < lv3 < lv4
        // 條件範圍
        // lv1 <= x && x <= lv2 - 1
        // lv2 <= x && x <= lv3 - 1
        // lv3 <= x && x <= lv4
        // level 1
        public int BasicSingleBonuslv1 { get; set; }
        // level 2
        public int BasicSingleBonuslv2 { get; set; }
        // level 3
        public int BasicSingleBonuslv3 { get; set; }
        // level 4
        public int BasicSingleBonuslv4 { get; set; }
        // %參數
        public double BasicSingleBonuslv1_2Percent { get; set; }
        public double BasicSingleBonuslv2_3Percent { get; set; }
        public double BasicSingleBonuslv3_4Percent { get; set; }


        // 推薦收益
        public int BasicFamilyIncome { get; set; }

        // 推薦分紅
        // 參數條件: lv1 < lv2
        // level 1
        public int BasicFamilyBonuslv1 { get; set; }
        // level 2
        public int BasicFamilyBonuslv2 { get; set; }
        // %參數
        public double BasicFamilyBonuslv1_2Percent { get; set; }
        public double BasicFamilyBonuslv2_Percent { get; set; }

        // 營業額(月)
        public double CompanyBusiness { get; set; }
    }
}
