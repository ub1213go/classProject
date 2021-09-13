using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_system
{
    class MemberData
    {
        public int MOrderNum { get; set; } // 訂單數(月)
        public int DOrderNum { get; set; } // 訂單數(日)
        public bool Survive { get; set; } // 是否有活單
    }
}
