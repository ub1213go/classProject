using System;
using System.Collections.Generic;

namespace lab_system
{
    class Program
    {
        static void Main(string[] args)
        {
            // Positions 為 這張易貨單ID在雙軌制度的易貨單的所有位置資訊
            // Position 為 雙軌制度的易貨單以左到右上到下的順序
            // Head 為 易貨單的上線Position
            // Left 為 易貨單的左邊下線Position
            // Live 為 易貨單是否還存活
            // ID 為 COrder系統編號，可以對照會員編號

            // ==================使用方式=============================
            // 先建立一個COrderTree
            // 建構子:
            //      參數number為放入資料庫最後的Position，意思是最後一筆易貨單的Position
            //      如果抓不到Position，則不需要放參數
            // 
            // 使用Tree.AddOrder();建立新的易貨單並回傳這筆易貨單的物件
            // ======================================================

            // OrderAddTime為易貨單增加易貨次數時的事件
            // OrderFull為易貨單符合完成2次易貨次數時增加關聯性與發生的事件

            int youMoneyBag = 0;
            Console.Write("你的ID是: ");
            string youID = Console.ReadLine();
            int youIDInt = Convert.ToInt32(youID);

            COrderTree Tree = new COrderTree();
            // 假設是資料庫
            List<COrder> db = new List<COrder>();
            Tree.OrderAddTime += delegate (object sender, MyEvent e)
            {
                // 模擬從資料庫取出e的Head易貨單
                COrder o = db.Find(x => x.GetLastOrderPosition().Position == e.Order.GetLastOrderPosition().Head);
                // 死亡
                AddTimeState s = o.AddTime();
                if (s == AddTimeState.success || s == AddTimeState.dead)
                {
                    Console.WriteLine($"ID {o.ID} 收入+200 & nowTime: {o.nowTime}");
                    if(youIDInt == o.ID)
                        youMoneyBag += 200;
                    Console.WriteLine();
                    if (s == AddTimeState.dead)
                    {
                        o.DeadTime = DateTime.Now;
                        Console.WriteLine($"ID {o.ID} 結束");
                        Console.WriteLine($"結束時間 {o.DeadTime}");
                        Console.WriteLine();
                    }
                }
                else if (s == AddTimeState.fail)
                {
                    // 異常處理
                    Console.WriteLine("異常");
                    Console.WriteLine();
                }

            };

            // 完成兩次易貨條件
            Tree.OrderFull += delegate (object sender, MyEvent e)
            {
                COrder o = db.Find(x => x.GetLastOrderPosition().Position == e.Order.GetLastOrderPosition().Head);
                // 如果e的Head物件是live的狀態，新增Head易貨單關聯性
                if (o.isLive)
                {
                    o.Positions.Add(new CPosition());
                    Tree.AddOrder(OldOrder: o);
                }
            };

            
            for (int i = 0; i < 15; i++)
            {
                db.Add(Tree.AddOrder(3));
            }

            COrder youOrder = db.Find(p => p.ID == youIDInt);
            if (youOrder != null)
            {
                Console.WriteLine($"總共收入: {youMoneyBag}");
                Console.WriteLine("Live: " + youOrder.isLive);
            }
            else
            {
                Console.WriteLine($"找不到 {youIDInt} 號");
            }

        }

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
