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
       public static SortedList<int, SortedList<int, ic.foreign_exchange_convC>> DATA_MONTH_FOREIGN_EXCHANGE { get; set; }
       public static bool GetMonthForeignExchange(int m_partition_id)
       {
           if (DATA_MONTH_FOREIGN_EXCHANGE == null)
           {
               DATA_MONTH_FOREIGN_EXCHANGE = new SortedList<int, SortedList<int, ic.foreign_exchange_convC>>();
           }
           if (DATA_MONTH_FOREIGN_EXCHANGE.Keys.IndexOf(m_partition_id) == -1)
           {
               DATA_MONTH_FOREIGN_EXCHANGE.Add(m_partition_id, new SortedList<int, ic.foreign_exchange_convC>());
           }
           if (wdata.DATA_MONTH_STAMP == null)
           {
               wdata.DATA_MONTH_STAMP = new SortedList<string, SortedList<int, long>>();
           }
          
           string _table_name = "acc_foreign_exchange_tb";
           if (wdata.DATA_MONTH_STAMP.IndexOfKey(_table_name) == -1)
           {
               wdata.DATA_MONTH_STAMP.Add(_table_name, new SortedList<int, long>());
               wdata.DATA_MONTH_STAMP[_table_name].Add(m_partition_id, 0);
           }
           string _str = null;
           bool load_all = false;
           bool is_new = false;
           bool is_updated = false;
           long _stamp = 0;
           using (var xd = new xing())
           {
               _stamp = xd.GetTimeStamp(_table_name);
               datam.FillForeignCurrency(xd);
               if (datam.DATA_MONTH_FOREIGN_EXCHANGE[m_partition_id].Keys.Count == 0)
               {
                   _str = string.Format("select * from " + _table_name + " where m_partition_id={0} and status={1}", m_partition_id, em.foreign_exch_statusS.valid.ToByte());
                   load_all = true;
                   is_new = true;
               }
               else
               {
                   if (_stamp == wdata.DATA_MONTH_STAMP[_table_name][m_partition_id])
                   {
                       xd.RollBackTransaction();
                       return false;
                   }
                   _str = string.Format("select * from " + _table_name + " where m_partition_id={0} and fs_time_stamp > {1}", m_partition_id, wdata.DATA_MONTH_STAMP[_table_name][m_partition_id]);
               }
               wdata.DATA_MONTH_STAMP[_table_name][m_partition_id] = _stamp;
               using (var _dr = xd.SelectCommand(_str))
               {
                   if (_dr == null) { return false; }
                   ic.foreign_exchange_convC _obj = null;
                   while (_dr.Read())
                   {
                       if (!is_updated) { is_updated = true; }
                       _obj = null;
                       if (load_all)
                       {
                           _obj = new ic.foreign_exchange_convC();
                       }
                       else
                       {
                           try
                           {
                               _obj = DATA_MONTH_FOREIGN_EXCHANGE[m_partition_id][_dr["un_id"].ToInt32()];
                               is_new = false;
                           }
                           catch (Exception)
                           {
                               if (_obj == null)
                               {
                                   _obj = new ic.foreign_exchange_convC();
                                   is_new = true;
                               }
                           }

                       }
                       //
                       if (is_new)
                       {
                           _obj.un_id = _dr["un_id"].ToInt32();
                           _obj.used_exch_rate = _dr.GetFloat("used_exch_rate");
                           _obj.exchanged_amount = _dr["exchanged_amount"].ToInt32();
                           _obj.curr_sys_amount = _dr["curr_sys_amount"].ToInt32();
                           _obj.curr_sys_exch_rate = _dr.GetFloat("curr_sys_exch_rate");
                           _obj.m_partition_id = _dr["m_partition_id"].ToInt32();
                           _obj.sys_account_id = _dr["sys_account_id"].ToInt32();
                           _obj.currency_id = _dr["currency_id"].ToInt32();
                           _obj.fs_id = _dr["fs_id"].ToInt32();
                           _obj.fs_date = _dr.GetDateTime("fs_date");
                           //
                       }
                       _obj.status = (em.foreign_exch_statusS)_dr["status"].ToInt16();
                       _obj.is_updated = true;
                       if (_dr["del_fs_date"] != null)
                       {
                           if (_obj.objDelInfo == null) { _obj.objDelInfo = new ic.deleteBaseC(); }
                           _obj.objDelInfo.del_fs_date = _dr.GetDateTime("del_fs_date");
                           _obj.objDelInfo.del_fs_id = fn.GetFSID(_obj.objDelInfo.del_fs_date.Value);
                           _obj.objDelInfo.del_fs_time = _dr["del_fs_time"].ToStringNullable();
                           _obj.objDelInfo.del_pc_us_id = _dr["del_pc_us_id"].ToInt32();
                       }
                       if (is_new)
                       {
                           DATA_MONTH_FOREIGN_EXCHANGE[m_partition_id].Add(_obj.un_id, _obj);
                       }
                   }
               }

               xd.CommitTransaction();
           }
           return is_updated;
       }
    }
}
