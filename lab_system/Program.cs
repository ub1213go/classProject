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

            // ==================使用方式=============================
            // 先建立一個COrderTree
            // 建構子參數number為放入資料庫最後的position
            // 使用Tree.AddOrder();建立新的易貨單並回傳這筆易貨單的物件
            // ======================================================

            // OrderAdd為新增易貨單時的事件
            // OrderAddTime為易貨單增加易貨次數時的事件
            // OrderFull為易貨單符合完成2次易貨次數時增加關聯性與發生的事件

            Console.WriteLine("Hello World!");
            COrderTree Tree = new COrderTree(0);
            // 假設是資料庫
            List<COrder> db = new List<COrder>();
            Tree.OrderAdd += delegate (object sender, MyEvent e)
            {
                db.Add(e.Order);
            };
            // sender = Tree & e = head position
            Tree.OrderAddTime += delegate (object sender, MyEvent e)
            {
                COrder o = db.Find(x => x.GetLastOrderPosition().Position == e.Order.GetLastOrderPosition().Head);
                // 增加次數
                o.AddTime();
                // 發錢的事件
                Console.WriteLine($"ID {o.id} 收入+200 & nowTime: {o.nowTime}");
                Console.WriteLine();
            };
            Tree.OrderFull += delegate (object sender, MyEvent e)
            {
                // 如果e的Head物件是live的狀態，新增Head易貨單關聯性
                if (db[e.Order.GetLastOrderPosition().Head - 1].live)
                {
                    db[e.Order.GetLastOrderPosition().Head - 1].Positions.Add(new CPosition());
                    Tree.AddOrder(OldOrder: db[e.Order.GetLastOrderPosition().Head - 1]);
                }
            };
            Tree.AddOrder();
            Tree.AddOrder();
            Tree.AddOrder();
            Tree.AddOrder();
            Tree.AddOrder();
            Tree.AddOrder();
            Tree.AddOrder();
            Tree.AddOrder();
            Tree.AddOrder();
        }

    }
    // 易貨單
    public class COrder
    {
        // 系統易貨單編號
        public int id;
        // 易貨次數上限
        public int maxTime;
        public int nowTime = 0;
        // 存活
        public bool live = true;
        public List<CPosition> Positions { get; set; } = new List<CPosition>();

        public COrder(int pmaxTime, int pid = 0)
        {
            id = pid;
            maxTime = pmaxTime;
        }
        
        public CPosition GetLastOrderPosition()
        {
            if(Positions.Count == 0)
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
        public bool AddTime()
        {
            if(nowTime + 1 >= maxTime)
            {
                nowTime = maxTime;
                live = false;
                return false;
            }
            nowTime++;
            return true;
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
        public int id;
        // 易貨單總數
        public int Number { get; set; }
        // 有易貨次數變更時
        public event EventHandler<MyEvent> OrderAddTime;
        // 有易貨單達成完成2次易貨新增條件時
        public event EventHandler<MyEvent> OrderFull;
        // 新增易貨單時
        public event EventHandler<MyEvent> OrderAdd;
        public COrderTree(int number)
        {
            Number = number;
            id = number;
        }
        public COrder AddOrder(int maxTime = 20, COrder OldOrder = null)
        {
            // 增加元素
            Number++;
            COrder Order;
            // 新增易貨單
            if (OldOrder == null)
            {
                id++;
                Order = new COrder(maxTime, id);
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

            // 輸出
            Console.WriteLine($"ID: {Order.id}");
            Console.WriteLine($"Stage: {stage + 1}");
            Console.WriteLine($"Queue: {queue}");
            Console.WriteLine($"Head: {p.Head}");
            Console.WriteLine($"Left: {p.Left}");
            Console.WriteLine($"Right: {p.Right}");
            Console.WriteLine($"maxTime: {Order.maxTime}");
            Console.WriteLine($"nowTime: {Order.nowTime}");
            if (Order.Positions.Count != 0)
                Console.WriteLine($"Position: {Order.Positions[Order.Positions.Count - 1].Position}");
            Console.WriteLine($"Live: {Order.live}");
            Console.WriteLine();

            // 事件
            // ==========================================================================
            OnOrderAdd(new MyEvent(Order));
            if (p.Position != 1)
            {
                // 觸發易貨次數
                OnOrderAddTime(new MyEvent(Order));
                // 觸發易貨單2次易貨新增條件
                if(p.Position % 2 == 1)
                {
                    OnOrderFull(new MyEvent(Order));
                }
            }
            // ==========================================================================

            return Order;
        }
        // 是否存在
        public bool IsExist(int position)
        {
            return position <= Number;
        }
        protected virtual void OnOrderAddTime(MyEvent e)
        {
            EventHandler<MyEvent> handler = OrderAddTime;
            if(handler != null)
                handler.Invoke(this, e);
        }
        protected virtual void OnOrderFull(MyEvent e)
        {
            EventHandler<MyEvent> handler = OrderFull;
            if (handler != null)
                handler.Invoke(this, e);
        }
        protected virtual void OnOrderAdd(MyEvent e)
        {
            EventHandler<MyEvent> handler = OrderAdd;
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

}
