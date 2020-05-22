using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdaHelperManager;
namespace MTOMS.ic
{
   public  class foreign_exchange_convC
    {
       public int un_id { get; set; }
       public DateTime fs_date { get; set; }
       public int fs_id { get; set; }
       public int currency_id { get; set; }
       public int sys_account_id { get; set; }
       public float curr_sys_exch_rate { get; set; }
       public int curr_sys_amount { get; set; }
       public int curr_foreign_currency
       {
           get
           {
               return (curr_sys_amount / curr_sys_exch_rate).ToInt32();
           }
       }
       public int converted_ug_amount
       {
           get
           {
               return (exchanged_amount * used_exch_rate).ToInt32();

           }
       }
       public float used_exch_rate { get; set; }
       public int exchanged_amount
       {
           get;
           set;
       }
       private int _diff
       {
           get
           {
               return (converted_ug_amount - (exchanged_amount * curr_sys_exch_rate).ToInt32());
           }
       }
       public int exch_gain
       {
           get
           {
               if (_diff > 0)
               {
                   return _diff;
               }
               return 0;
           }
       }
       public int exch_loss
       {
           get
           {
               if (_diff < 0)
               {
                   return Math.Abs(_diff);
               }
               return 0;
           }
       }
       public em.foreign_exch_statusS status { get; set; }
       public int m_partition_id { get; set; }
       public bool is_updated { get; set; }
       public int pc_us_id { get; set; }
       public ic.deleteBaseC objDelInfo { get; set; }
       public long transaction_id { get; set; }
    }
}
