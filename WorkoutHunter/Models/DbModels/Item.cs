using System;
using System.Collections.Generic;

#nullable disable

namespace WorkoutHunterV2.Models.DbModels
{
    public partial class Item
    {
        public int Iid { get; set; }
        public string ItemName { get; set; }
        public string ItemInfo { get; set; }
        public string ItemPic { get; set; }
        public string Buff { get; set; }
        public int? CostMoney { get; set; }
    }
}
