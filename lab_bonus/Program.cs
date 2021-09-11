using System;

namespace lab_bonus
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // 資料庫存取
            SysOrderPara sysp = new SysOrderPara()
            {
                BasicSingleIncome = 200,

                BasicSingleBonuslv1 = 11,
                BasicSingleBonuslv2 = 31,
                BasicSingleBonuslv3 = 61,
                BasicSingleBonuslv4 = 100,

                BasicSingleBonuslv1_2Percent = 1.0 / 100,
                BasicSingleBonuslv2_3Percent = 1.0 / 100,
                BasicSingleBonuslv3_4Percent = 1.0 / 100,

                BasicFamilyIncome = 200,

                BasicFamilyBonuslv1 = 31,
                BasicFamilyBonuslv2 = 61,

                BasicFamilyBonuslv1_2Percent = 0.5 / 100,
                BasicFamilyBonuslv2_Percent = 0.5 / 100,

                CompanyBusiness = 10035552.34,
            };
            BonusSystem T = new BonusSystem(sysp);
            Console.WriteLine(T.GivePersonalBonus(32));
        }
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

        // 獎金system
        public class BonusSystem 
        {
            SysOrderPara par { get; set; }

            public BonusSystem(SysOrderPara p)
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
            // 易貨分紅
            // 參數: 會員
            // 回傳: 獎金
            public double GivePersonalBonus(int OrderSum)
            {
                double money = 0;
                if(par.BasicSingleBonuslv1 <= OrderSum && OrderSum <= par.BasicSingleBonuslv4)
                {
                    money += Math.Floor(par.CompanyBusiness * par.BasicSingleBonuslv1_2Percent * 100) / 100;
                }
                if(par.BasicSingleBonuslv2 <= OrderSum && OrderSum <= par.BasicSingleBonuslv4)
                {
                    money += Math.Floor(par.CompanyBusiness * par.BasicSingleBonuslv2_3Percent * 100) / 100;
                }
                if(par.BasicSingleBonuslv3 <= OrderSum && OrderSum <= par.BasicSingleBonuslv4)
                {
                    money += Math.Floor(par.CompanyBusiness * par.BasicSingleBonuslv3_4Percent * 100) / 100;
                }
                return money;
            }
            // 發放推薦收益
            public int GiveFamilyReward(int OrderSum)
            {

                return 0;
            }
            // 發放推薦分紅(規則尚未決定)
            public int GiveFamilyBonus()
            {

                return 0;
            }

        }
    }
}
