using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdaHelperManager;
using SdaHelperManager.Security;

namespace MTOMS
{
   public partial class datam
    {
       public static SortedList<int, ic.church_sub_unitC> DATA_CHURCH_SUB_UNIT = null;
       public static void FillChurchSubUnits(xing xd)
       {
           if (DATA_CHURCH_SUB_UNIT == null)
           {
               DATA_CHURCH_SUB_UNIT = new SortedList<int, MTOMS.ic.church_sub_unitC>();
           }
           bool is_new = false;
           bool load_all = false;
           string _str = string.Empty;
           if (wdata.TABLE_STAMP == null)
           {
               wdata.TABLE_STAMP = new SortedList<string, long>();
           }
           string _table_name = "church_sub_unit_tb";
           if (wdata.TABLE_STAMP.IndexOfKey(_table_name) == -1)
           {
               wdata.TABLE_STAMP.Add(_table_name, 0);
           }
           var _stamp = xd.GetTimeStamp(_table_name);
           if (DATA_CHURCH_SUB_UNIT.Keys.Count == 0)
           {
               _str = "select * from church_sub_unit_tb";
               load_all = true;
           }
           else
           {

               if (_stamp == wdata.TABLE_STAMP[_table_name])
               {
                   return;
               }
               _str = string.Format("select * from church_sub_unit_tb where fs_time_stamp > {0}", wdata.TABLE_STAMP[_table_name]);
           }
           wdata.TABLE_STAMP[_table_name] = _stamp;
           ic.church_sub_unitC _obj = null;
           #region database fill
           using (var _dr = xd.SelectCommand(_str))
           {
               while (_dr.Read())
               {
                   _obj = null;
                   if (load_all)
                   {
                       _obj = new MTOMS.ic.church_sub_unitC();
                       is_new = true;
                   }
                   else
                   {
                       try
                       {
                           _obj = datam.DATA_CHURCH_SUB_UNIT[_dr["sb_unit_id"].ToInt32()];
                           is_new = false;
                       }
                       catch (Exception ex)
                       {
                           if (_obj == null)
                           {
                               _obj = new MTOMS.ic.church_sub_unitC();
                               is_new = true;
                           }
                       }
                   }
                   if (is_new)
                   {
                       _obj.sb_unit_id = _dr["sb_unit_id"].ToInt32();
                       _obj.sb_unit_category = (em.sb_unit_categoryS)_dr["sb_unit_cat_id"].ToInt32();
                       _obj.sb_unit_name = _dr["sb_unit_name"].ToStringNullable();
                       _obj.sys_gp_account_id = _dr["sys_gp_account_id"].ToInt32();
                       datam.DATA_CHURCH_SUB_UNIT.Add(_obj.sb_unit_id, _obj);
                   }
                   _obj.sb_unit_name = _dr["sb_unit_name"].ToStringNullable();

               }
               _dr.Close(); _dr.Dispose();
           }


           #endregion
       }
    }
}
