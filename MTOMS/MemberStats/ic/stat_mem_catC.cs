using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTOMS.ic
{
  
   public class stat_mem_catC
    {
       public string cat_name { get; set; }
       public int cat_index { get; set; }
       public SortedList<int, ic.stat_mem_typeC> TypeCollection { get; set; }
       public bool is_optional { get; set; }
       public stat_mem_catC()
       {
           TypeCollection = new SortedList<int, stat_mem_typeC>();
           is_optional = false;
       }
    }
}
