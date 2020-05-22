using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTOMS.ic
{
   public class bank_reconc_accountC
    {
       public int br_acc_id { get; set; }
       public string br_acc_name { get; set; }
       public em.bank_reconc_typeS br_acc_type { get; set; }
       public int sys_account_id { get; set; }
    }
}
