using SdaHelperManager;
using SdaHelperManager.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTOMS
{
   public static partial class datam
    {
       public static SortedList<int, SortedList<int, ic.batch_no_SettingC>> DATA_BATCH_NO_SETTINGS { get; set; }
       public static bool fill_batch_no_sabbath(xing xd, int sab_fs_id)
       {
           if (DATA_BATCH_NO_SETTINGS == null)
           {
               DATA_BATCH_NO_SETTINGS = new SortedList<int, SortedList<int, ic.batch_no_SettingC>>();
           }
           if(sab_fs_id==0)
           {
               return false;
           }
           if(DATA_BATCH_NO_SETTINGS.IndexOfKey(sab_fs_id)==-1)
           {
               DATA_BATCH_NO_SETTINGS.Add(sab_fs_id, new SortedList<int, ic.batch_no_SettingC>());
           }
           string _table_name = "off_batch_no_settings_tb";
           if (wdata.TABLE_STAMP == null)
           {
               wdata.TABLE_STAMP = new SortedList<string, long>();
           }
           if (wdata.TABLE_STAMP.IndexOfKey(_table_name) == -1)
           {
               wdata.TABLE_STAMP.Add(_table_name, 0);
           }
           bool is_new = false;
           bool load_all = false;
           string _str = string.Empty;
           long _stamp = xd.GetTimeStamp(_table_name);
           if (DATA_BATCH_NO_SETTINGS[sab_fs_id].Keys.Count == 0)
           {
               _str = string.Format("select * from {0} where sab_fs_id={1}", _table_name, sab_fs_id);
               load_all = true;
           }
           else
           {
               if (wdata.TABLE_STAMP[_table_name] == _stamp)
               {
                    return false;
               }
               _str = string.Format("select * from {0} where sab_fs_id={1} and fs_time_stamp > {2}", _table_name, sab_fs_id, wdata.TABLE_STAMP[_table_name]);
               load_all = false;
           }
           wdata.TABLE_STAMP[_table_name] = _stamp;
           ic.batch_no_SettingC _obj = null;
           #region database fill
           using (var _dr = xd.SelectCommand(_str))
           {
               while (_dr.Read())
               {
                   _obj = null;
                   if (load_all)
                   {
                       _obj = new ic.batch_no_SettingC();
                       is_new = true;
                   }
                   else
                   {
                       try
                       {
                           _obj = datam.DATA_BATCH_NO_SETTINGS[sab_fs_id][_dr["un_id"].ToInt32()];
                           is_new = false;
                       }
                       catch (Exception ex)
                       {
                           if (_obj == null)
                           {
                               _obj = new ic.batch_no_SettingC();
                               is_new = true;
                           }
                       }
                   }
                   if (is_new)
                   {
                       _obj.un_id = _dr["un_id"].ToInt32();
                       _obj.entrant_id = _dr["entrant_id"].ToInt32();
                       _obj.sab_fs_id = _dr["sab_fs_id"].ToInt32();
                       _obj.batch_count = _dr["batch_count"].ToInt32();
                       _obj.batch_no = _dr["batch_no"].ToStringNullable();
                       _obj.batch_total = _dr["batch_total"].ToInt32();

                       datam.DATA_BATCH_NO_SETTINGS[sab_fs_id].Add(_obj.un_id, _obj);
                   }
                   //
                   _obj.entrant_count = _dr["entrant_count"].ToInt32();
                   _obj.entrant_total = _dr["entrant_total"].ToInt32();
                   
               }
               _dr.Close(); _dr.Dispose();
           }
           return true;

           #endregion
       }
    }
}
