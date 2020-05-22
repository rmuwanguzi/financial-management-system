using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTOMS.ic
{
   public class batch_no_SettingC
    {
       public int un_id { get; set; }
       public int sab_fs_id { get; set; }
       public int entrant_id { get; set; }

       public string batch_no { get; set; }
       public int batch_count { get; set; }
       public int batch_total { get; set; }
       public int entrant_count { get; set; }
       public int entrant_total { get; set; }
       public override string ToString()
       {
           return this.batch_no;
       }
    }
}
