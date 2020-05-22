using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTOMS
{
   public static class xTension
    {
       public static int CustomAgeGroup(this ic.memberC value,int yr)
       {
           if (value == null | yr == 0) { return 0; }
           int mem_age = (yr - value.birth_yr);
           var nlist = (xso.xso.DATA_AGEGROUP.Values.Where(o => mem_age >= o.start_age && mem_age <= o.end_age)).FirstOrDefault();
           if (nlist != null)
           {
               return nlist.age_gp_id;
           }
           return 0;
       }
    }
}
