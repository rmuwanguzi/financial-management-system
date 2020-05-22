using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTOMS.ic
{
   public class bank_reconc_transC
    {
       public int un_id { get; set; }
       public int br_acc_id { get; set; }
       public ic.bank_reconc_accountC objReconcAccount { get; set; }
        public ic.bankAccountC objBankAccount { get; set; }
       public int amount { get; set; }
       public long trans_id { get; set; }
       public int fs_id { get; set; }
       public DateTime fs_date { get; set; }
       public int m_partition_id { get; set; }
       public em.voucher_statusS status { get; set; }
       public int sys_account_id { get; set; }
       public string desc { get; set; }
       public em.bank_reconc_typeS br_acc_type { get; set; }
       public int bank_account_id { get; set; }
        public bool is_updated { get; set; }

    }
}
