using System;
using System.Collections.Generic;
using System.Linq;

namespace lab_system
{
    class Program
    {
        static void Main(string[] args)
        {
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

            
            for (int i = 0; i < 20; i++)
            {
                db.Add(Tree.AddOrder(16));
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
    

}
