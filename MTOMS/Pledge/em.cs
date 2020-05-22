using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTOMS

{
   public partial class em
    {
       public enum pledge_statusS
       {
           pending=0,
           completed,
           deleted,
           expired
       }
        public enum pledge_setting_statusS
        {
            none=-1,
            valid,
            deleted,
            expired
           
        }
        public enum pledge_modeS
       {
           normal=0,
           mobile_phone
       }
    }
}
