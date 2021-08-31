using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutHunterV2.Models.Home
{
    public class MyCache
    {
        // UID
        public string U { get; set; }
        // 混合的密碼
        public string C { get; set; }
        // salt
        public byte[] K { get; set; }
        // 密碼
        public string Password { get; set; }
    }
}
