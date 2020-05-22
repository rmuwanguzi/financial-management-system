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
       public static SortedList<int, ic.accountPayableC> DATA_ACCOUNTS_PAYABLE { get; set; }
       public static bool GetAccountsPayable(xing xd)
       {
           if (DATA_ACCOUNTS_PAYABLE == null)
           {
               DATA_ACCOUNTS_PAYABLE = new SortedList<int, ic.accountPayableC>();
           }
           if(wdata.TABLE_STAMP==null)
           {
               wdata.TABLE_STAMP=new SortedList<string,long>();
           }
           string _table_name = "accounts_current_balance_tb";
           if (wdata.TABLE_STAMP.IndexOfKey(_table_name) == -1)
           {
               wdata.TABLE_STAMP.Add(_table_name, 0);
           }
           string _str = null;
           bool load_all = false;
           bool is_new = false;
           bool is_updated = false;
           var _xd = (xd == null) ? new xing() : xd;
           long _stamp = _xd.GetTimeStamp(_table_name);
               if (DATA_ACCOUNTS_PAYABLE.Keys.Count == 0)
               {
                   _str = string.Format("select * from " + _table_name + " where lch_id={0} and jsection_id={1} and balance <> 0 ", sdata.ChurchID, em.j_sectionS.creditor.ToInt16());
                   load_all = true;
                   is_new = true;
               }
               else
               {
                   if (_stamp == wdata.TABLE_STAMP[_table_name])
                   {
                      
                       return false;
                   }
                   _str = string.Format("select * from " + _table_name + " where lch_id={0} and fs_time_stamp > {1} and jsection_id={2}", sdata.ChurchID, wdata.TABLE_STAMP[_table_name], em.j_sectionS.creditor.ToInt16());
               }
               wdata.TABLE_STAMP[_table_name] = _stamp;
               using (var _dr = _xd.SelectCommand(_str))
               {
                   if (_dr == null) { return false; }
                   ic.accountPayableC _obj = null;
                   while (_dr.Read())
                   {
                       if (!is_updated) { is_updated = true; }
                       _obj = null;
                       if (load_all)
                       {
                           _obj = new ic.accountPayableC();
                       }
                       else
                       {
                           try
                           {
                               _obj = DATA_ACCOUNTS_PAYABLE[_dr["un_id"].ToInt32()];
                               is_new = false;
                           }
                           catch (Exception)
                           {
                               if (_obj == null)
                               {
                                   _obj = new ic.accountPayableC();
                                   is_new = true;
                               }
                           }
                       }
                       //
                       if (is_new)
                       {
                           _obj.account_id = _dr["account_id"].ToInt32();
                           _obj.un_id = _dr["un_id"].ToInt32();
                           DATA_ACCOUNTS_PAYABLE.Add(_obj.un_id, _obj);
                       }
                       _obj.balance = _dr["balance"].ToInt32();
                       _obj.is_updated = true;

                   }

               }
               if (xd == null)
               {
                   _xd.CommitTransaction();
                   _xd.Dispose();
                   _xd = null;
               }
           return is_updated;
       }
    }
}
