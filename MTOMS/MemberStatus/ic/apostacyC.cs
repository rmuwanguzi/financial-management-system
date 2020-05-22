using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTOMS.ic
{
   public class apostacyC
    {
        public int un_id { get; set; }
        public int mem_id { get; set; }
        public string reason { get; set; }
        public string comment { get; set; }
        public DateTime apostacy_date { get; set; }
        public int apostacy_fs_id { get; set; }
        public int pc_us_id { get; set; }
        public int lch_type_id { get; set; }
        public int lch_id { get; set; }
    }
}
