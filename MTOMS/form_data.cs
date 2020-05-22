using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTOMS
{
  public class form_data 
    {
        public IntPtr ParentHandle
        {
            get;
            set;
        }
        public object edit_object
        {
            get;
            set;
        }
        public object Tag { get; set; }
          
    }
}
