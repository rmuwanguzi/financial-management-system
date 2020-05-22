using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTOMS
{
   public partial class em
    {
       public enum acc_trans_typeS
       {
           offertory_payment = 0,
           opening_balance=1,
           bank_deposit=2,
           bank_withdraw=3,
           sabbath_loss=4,
           sabbath_gain=5,
           expense=6,
           expense_payment=7,
           accounts_adjustment=8
       }
    }
}
