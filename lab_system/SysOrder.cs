using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_system
{
    public class SysOrder
    {
        public SysBonusConfig para { get; set; }

        public int ID { get; set; } // 易貨單系統編號
        public int admin_id { get; set; } // 會員系統編號
        public string UID { get; set; } // 會員UID
        public int maxTime { get; set; } // 最大易貨次數
        public int nowTime { get; set; } // 目前易貨次數
        public bool isLive { get; set; } // 存活
        public int position { get; set; } // 位置
        public int stage { get; set; } // 層級
        public int queue { get; set; } // 順位
        public bool IsExpand { get; set; } // 是否展延
        public DateTime StarTime { get; set; } // 建立時間
        public DateTime EndTime { get; set; } // 到期時間
        public DateTime DeadTime { get; set; } // 死亡時間

        public int Head { get; set; } // 關聯位置
        public int Left { get; set; } // 關聯位置
        public int Right { get; set; } // 關聯位置

        public event EventHandler<MyEvent> OrderAddTime; // 有易貨次數變更時
        public event EventHandler<MyEvent> OrderFull; // 有易貨單達成完成2次易貨新增條件時

        public SysOrder(int pmaxTime, int pid = 0)
        {
            // 預設參數
            para.BasicSingleIncome = 200;


            IsExpand = false;
            StarTime = DateTime.Now;
            EndTime = DateTime.Now.AddMonths(6);
            ID = pid;
            nowTime = 0;
            maxTime = pmaxTime;
        }

        public AddTimeState AddTime()
        {
            if (isLive && nowTime + 1 <= maxTime)
            {
                nowTime++;
                if (nowTime == maxTime)
                {
                    isLive = false;
                    return AddTimeState.dead;
                }
                return AddTimeState.success;
            }
            return AddTimeState.fail;
        }
        public bool SetExpand()
        {
            if (isLive)
            {
                IsExpand = true;
                maxTime *= 2;
                return true;
            }
            return false;
        }
        public SysOrder AddOrder(int maxTime = 20, SysOrder OldOrder = null)
        {
            // 增加元素
            ID++;
            SysOrder Order;
            // 新增易貨單
            if (OldOrder == null)
            {
                ID++;
                Order = new SysOrder(maxTime, ID);
            }
            else
            {
                Order = OldOrder;
            }
            // 設定位置
            position = ID;

            // 雙軌制演算法
            // 層級
            stage = Convert.ToInt32(Math.Floor(Math.Log(Order.position, 2)));
            // 層級中順位
            queue = ID;
            int n = 1;
            for (int i = 0; i < stage; i++)
            {
                queue = queue - n;
                n *= 2;
            }

            // 計算head left right position
            Order.Head = Convert.ToInt32(Order.position - Math.Round(Convert.ToDouble(queue / 2)) - Math.Pow(2, stage - 1));
            Order.Left = (int)(Order.position + Math.Pow(2, stage)) + queue - 1;
            Order.Right = (int)(Order.position + Math.Pow(2, stage)) + queue;

            if (Order.position != 1)
            {
                // 觸發易貨次數
                OnOrderAddTime(new MyEvent(Order));
                // 觸發易貨單2次易貨新增條件
                if (Order.position % 2 == 1)
                {
                    OnOrderFull(new MyEvent(Order));
                }
            }

            return Order;
        }
        protected virtual void OnOrderAddTime(MyEvent e)
        {
            EventHandler<MyEvent> handler = OrderAddTime;
            if (handler != null)
                handler.Invoke(this, e);
        }
        protected virtual void OnOrderFull(MyEvent e)
        {
            EventHandler<MyEvent> handler = OrderFull;
            if (handler != null)
                handler.Invoke(this, e);
        }

        // V
        // 發放收益: 給予Reward值
        public int GiveReward()
        {
            return para.BasicSingleIncome;
        }
        // X
        // 分配易貨分紅
        // 參數1: 訂單數 = 會員本身這個月新增的訂單數
        // 回傳: 獎金
        public double GivePersonalBonus(MemberData m, double CompanyBusiness)
        {
            int OrderSum = m.MOrderNum;
            double money = 0;
            if (para.BasicSingleBonuslv1 <= OrderSum)
            {
                money += Math.Floor(CompanyBusiness * para.BasicSingleBonuslv1_2Percent * 100) / 100;
            }
            if (para.BasicSingleBonuslv2 <= OrderSum)
            {
                money += Math.Floor(CompanyBusiness * para.BasicSingleBonuslv2_3Percent * 100) / 100;
            }
            if (para.BasicSingleBonuslv3 <= OrderSum)
            {
                money += Math.Floor(CompanyBusiness * para.BasicSingleBonuslv3_Percent * 100) / 100;
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
            double Bonus = OrderSum * para.BasicFamilyIncome;
            // ==============================================
            // 昨日下線易貨單總數歸零 (待修改)
            foreach (var item in L)
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
            if (para.BasicFamilyBonuslv1 <= P)
            {
                money += P * para.BasicFamilyBonuslv1_2Percent;
            }
            if (para.BasicFamilyBonuslv2 <= P)
            {
                money += P * para.BasicFamilyBonuslv2_Percent;
            }
            return 0;
        }
    }
    public class MyEvent : EventArgs
    {
        public SysOrder Order { get; set; }
        public MyEvent(SysOrder pOrder)
        {
            Order = pOrder;
        }
    }
    public enum AddTimeState : byte
    {
        success = 0,
        dead = 1,
        fail = 2,
    }
}
