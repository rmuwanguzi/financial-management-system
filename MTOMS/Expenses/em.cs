using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTOMS
{
   public partial class em
    {
       public enum voucher_statusS
       {
           valid=0,
           cancelled=1,
           replaced
       }
       public enum voucher_Paymode
       {
           cash,
           cheque,
           bank_transfer,
          
       }
       public enum exp_inc_src_typeS
       {
           petty_cash=0,
           petty_cash_cheque,
           unbanked_cash,
           bank
       }
       public enum exp_acc_statusS
       {
           none=-1,
           valid,
           invalid,
           deleted

       }
       public enum exp_acc_typeS
       {
           none=-1,
           system_department,
           system_offertory_payment,
           user_defined,
           user_defined_shared,
           trust_fund
       }
    }
}
