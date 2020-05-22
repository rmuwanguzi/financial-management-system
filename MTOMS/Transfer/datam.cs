using SdaHelperManager.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdaHelperManager;
namespace MTOMS
{
   public partial class datam
    {
       public static SortedList<int, SortedList<int, ic.cash_transferC>> DATA_MONTH_CASH_TRANSFERS { get; set; }
       public static bool GetMonthCashTransfers(int m_partition_id)
       {
           if (DATA_MONTH_CASH_TRANSFERS == null)
           {
               DATA_MONTH_CASH_TRANSFERS = new SortedList<int, SortedList<int, ic.cash_transferC>>();
           }
           if (DATA_MONTH_CASH_TRANSFERS.Keys.IndexOf(m_partition_id) == -1)
           {
               DATA_MONTH_CASH_TRANSFERS.Add(m_partition_id, new SortedList<int, ic.cash_transferC>());
           }
           if (wdata.DATA_MONTH_STAMP == null)
           {
               wdata.DATA_MONTH_STAMP = new SortedList<string, SortedList<int, long>>();
           }
           string _table_name = "acc_cash_transfer_tb";
           string _key = "_key34" + _table_name;
           if (wdata.DATA_MONTH_STAMP.IndexOfKey(_key) == -1)
           {
               wdata.DATA_MONTH_STAMP.Add(_key, new SortedList<int, long>());
               wdata.DATA_MONTH_STAMP[_key].Add(m_partition_id, 0);
           }
           string _str = null;
           bool load_all = false;
           bool is_new = false;
           bool is_updated = false;
           long _stamp = 0;
           using (var xd = new xing())
           {
               _stamp = xd.GetTimeStamp(_table_name);
               if (datam.DATA_MONTH_CASH_TRANSFERS[m_partition_id].Keys.Count == 0)
               {
                   _str = string.Format("select * from " + _table_name + " where m_partition_id={0} and status={1}", m_partition_id, em.cashTransferStatus.valid.ToInt16());
                   load_all = true;
                   is_new = true;
               }
               else
               {
                   if (_stamp == wdata.DATA_MONTH_STAMP[_key][m_partition_id])
                   {
                       xd.RollBackTransaction();
                       return false;
                   }
                   _str = string.Format("select * from " + _table_name + " where m_partition_id={0} and fs_time_stamp > {1}", m_partition_id, wdata.DATA_MONTH_STAMP[_key][m_partition_id]);
               }
               wdata.DATA_MONTH_STAMP[_key][m_partition_id] = _stamp;
               using (var _dr = xd.SelectCommand(_str))
               {
                   if (_dr == null) { return false; }
                   ic.cash_transferC _obj = null;
                   while (_dr.Read())
                   {
                       if (!is_updated) { is_updated = true; }
                       _obj = null;
                       if (load_all)
                       {
                           _obj = new ic.cash_transferC();
                       }
                       else
                       {
                           try
                           {
                               _obj = datam.DATA_MONTH_CASH_TRANSFERS[m_partition_id][_dr["un_id"].ToInt32()];
                               is_new = false;
                           }
                           catch (Exception)
                           {
                               if (_obj == null)
                               {
                                   _obj = new ic.cash_transferC();
                                   is_new = true;
                               }
                           }

                       }
                       //
                       if (is_new)
                       {
                           _obj.un_id = _dr["un_id"].ToInt32();
                          
                           _obj.pc_us_id = _dr["pc_us_id"].ToInt32();
                           _obj.amount = _dr["amount"].ToInt32();
                           _obj.destination_id = _dr["destination_id"].ToInt32();
                           _obj.destination_type_id = _dr["destination_type_id"].ToInt32();
                           //
                           _obj.fs_date = _dr.GetDateTime("fs_date");
                           _obj.fs_id = fn.GetFSID(_obj.fs_date.Value);
                           _obj.source_id = _dr["source_id"].ToInt32();
                           _obj.source_type_id = _dr["source_type_id"].ToInt32();
                           _obj.transfer_reason = _dr["transfer_reason"].ToStringNullable();
                           _obj.pc_us_id = _dr["pc_us_id"].ToInt32();
                       
                        }
                       _obj.status = (em.cashTransferStatus)_dr["status"].ToInt16();
                       _obj.is_updated = true;
                       if (_dr["delete_fs_date"] != null)
                       {
                           if (_obj.objDelInfo == null) { _obj.objDelInfo = new ic.deleteBaseC(); }
                           _obj.objDelInfo.del_fs_date = _dr.GetDateTime("delete_fs_date");
                           _obj.objDelInfo.del_fs_id = fn.GetFSID(_obj.objDelInfo.del_fs_date.Value);
                          // _obj.objDelInfo.del_fs_time = _dr["del_fs_time"].ToStringNullable();
                         //  _obj.objDelInfo.del_pc_us_id = _dr["del_pc_us_id"].ToInt32();
                       }
                       if (is_new)
                       {
                           datam.DATA_MONTH_CASH_TRANSFERS[m_partition_id].Add(_obj.un_id, _obj);
                       }
                   }
               }

               xd.CommitTransaction();
           }
           return is_updated;
       }
       
    }
}
