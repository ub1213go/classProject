using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_system
{
    class SysBonus
    {
        // 獎金資格
        // OrderBonus = 1, 2, 3
        // 易貨分紅 lv1
        // 易貨分紅 lv2
        // 易貨分紅 lv3
        // PeopleBonus = 1, 2
        // 推薦分紅 lv1
        // 推薦分紅 lv2
        SysOrderPara par { get; set; }

        public SysBonus(SysOrderPara p)
        {
            par = p;
        }
        // V
        // 發放收益: 給予Reward值
        public int GiveReward()
        {
            return par.BasicSingleIncome;
        }
        // X
        // 分配易貨分紅
        // 參數1: 訂單數 = 會員本身這個月新增的訂單數
        // 回傳: 獎金
        public double GivePersonalBonus(MemberData m)
        {
            int OrderSum = m.MOrderNum;
            double money = 0;
            if (par.BasicSingleBonuslv1 <= OrderSum && OrderSum <= par.BasicSingleBonuslv4)
            {
                money += Math.Floor(par.CompanyBusiness * par.BasicSingleBonuslv1_2Percent * 100) / 100;
            }
            if (par.BasicSingleBonuslv2 <= OrderSum && OrderSum <= par.BasicSingleBonuslv4)
            {
                money += Math.Floor(par.CompanyBusiness * par.BasicSingleBonuslv2_3Percent * 100) / 100;
            }
            if (par.BasicSingleBonuslv3 <= OrderSum && OrderSum <= par.BasicSingleBonuslv4)
            {
                money += Math.Floor(par.CompanyBusiness * par.BasicSingleBonuslv3_4Percent * 100) / 100;
            }
            return money;
        }
        // V
        // 發放推薦收益
        // 描述: 根據會員的下線總單數，發放獎金
        // 參數: 會員物件
        // 回傳: 獎金
        // 操作: 取得會員物件的今日總單數，計算獎金，計算完畢，使其歸零回存資料庫
        public double GiveFamilyReward(MemberData m)
        {
            // ==============================================
            // 取得下線昨日易貨單總數 (待修改)
            List<MemberData> L = new List<MemberData>();
            L.Add(new MemberData() { DOrderNum = 5 });
            L.Add(new MemberData() { DOrderNum = 3 });
            L.Add(new MemberData() { DOrderNum = 6 });
            // ==============================================
            int OrderSum = L.Sum(p => p.DOrderNum);
            double Bonus = OrderSum * par.BasicFamilyIncome;
            // ==============================================
            // 昨日下線易貨單總數歸零 (待修改)
            foreach(var item in L)
            {
                item.DOrderNum = 0;
            }
            // ==============================================
            return Bonus;
        }
        // X
        // 分配推薦分紅
        // 描述: 根據會員有活單的下線人數，發放獎金
        // 參數: 會員清單
        // 回傳: 獎金
        // 操作: 取得會員物件的下線且有活單的人數，計算獎金
        public int GiveFamilyBonus()
        {
            // ==============================================
            // 查詢會員的下線且有活單的人數
            int P = 10;
            // ==============================================
            double money = 0;
            if(par.BasicFamilyBonuslv1 <= P)
            {
                money += P * par.BasicFamilyBonuslv1_2Percent;
            }
            if(par.BasicFamilyBonuslv2 <= P)
            {
                money += P * par.BasicFamilyBonuslv2_Percent;
            }
            return 0;
        }
    }
    
}
