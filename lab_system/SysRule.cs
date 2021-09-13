using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_system
{
    class SysRule
    {
        
    }
    // 易貨單
    public class COrder
    {
        // 系統易貨單編號
        public int ID;
        // 最大易貨次數
        public int maxTime;
        // 目前易貨次數
        public int nowTime;
        // 存活
        public bool isLive = true;
        // 所有位置
        public List<CPosition> Positions { get; set; } = new List<CPosition>();
        // 建立時間
        public DateTime StarTime;
        // 到期時間
        public DateTime EndTime;
        // 是否展延
        public bool IsExpand;
        // 結束時間
        public DateTime DeadTime;

        public COrder(int pmaxTime, int pid = 0)
        {
            IsExpand = false;
            StarTime = DateTime.Now;
            EndTime = DateTime.Now.AddMonths(6);
            ID = pid;
            nowTime = 0;
            maxTime = pmaxTime;
        }

        public CPosition GetLastOrderPosition()
        {
            if (Positions.Count == 0)
            {
                Positions.Add(new CPosition());
            }
            return Positions[Positions.Count - 1];
        }
        public CPosition GetOrderPosition(int n)
        {
            if (n > Positions.Count - 1) return null;
            return Positions[n];
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
    }
    // 位置
    public class CPosition
    {
        // 易貨單位置
        public int Position { get; set; }
        // 關聯
        public int Head { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }
    }
    // 易貨單樹
    public class COrderTree
    {
        public int ID;
        // 易貨單總數
        public int Number { get; set; }
        // 有易貨次數變更時
        public event EventHandler<MyEvent> OrderAddTime;
        // 有易貨單達成完成2次易貨新增條件時
        public event EventHandler<MyEvent> OrderFull;
        public COrderTree(int number = 0)
        {
            Number = number;
            ID = number;
        }
        public COrder AddOrder(int maxTime = 20, COrder OldOrder = null)
        {
            // 增加元素
            Number++;
            COrder Order;
            // 新增易貨單
            if (OldOrder == null)
            {
                ID++;
                Order = new COrder(maxTime, ID);
            }
            else
            {
                Order = OldOrder;
            }
            var p = Order.GetLastOrderPosition();
            // 設定位置
            p.Position = Number;

            // 雙軌制演算法
            // ==========================================================================
            // 層級
            int stage = Convert.ToInt32(Math.Floor(Math.Log(p.Position, 2)));
            // 層級中順位
            int queue = Number;
            int n = 1;
            for (int i = 0; i < stage; i++)
            {
                queue = queue - n;
                n *= 2;
            }
            // 計算head left right position
            p.Head = Convert.ToInt32(p.Position - Math.Round(Convert.ToDouble(queue / 2)) - Math.Pow(2, stage - 1));
            p.Left = (int)(p.Position + Math.Pow(2, stage)) + queue - 1;
            p.Right = (int)(p.Position + Math.Pow(2, stage)) + queue;
            // ==========================================================================

            // 測試Test
            // ==========================================================================
            Console.WriteLine($"ID: {Order.ID}");
            Console.WriteLine($"Stage: {stage + 1}");
            Console.WriteLine($"Queue: {queue}");
            Console.WriteLine($"Head: {p.Head}");
            Console.WriteLine($"Left: {p.Left}");
            Console.WriteLine($"Right: {p.Right}");
            Console.WriteLine($"maxTime: {Order.maxTime}");
            Console.WriteLine($"nowTime: {Order.nowTime}");
            if (Order.Positions.Count != 0)
                Console.WriteLine($"Position: {p.Position}");
            Console.WriteLine($"Live: {Order.isLive}");
            Console.WriteLine();
            // ==========================================================================

            // 事件
            // ==========================================================================
            if (p.Position != 1)
            {
                // 觸發易貨次數
                OnOrderAddTime(new MyEvent(Order));
                // 觸發易貨單2次易貨新增條件
                if (p.Position % 2 == 1)
                {
                    OnOrderFull(new MyEvent(Order));
                }
            }
            // ==========================================================================

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
    }
    public class MyEvent : EventArgs
    {
        public COrder Order { get; set; }
        public MyEvent(COrder pOrder)
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
