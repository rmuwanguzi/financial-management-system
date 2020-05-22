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
       public static SortedList<int, ic.expense_catC> DATA_EXPENSE_CATEGORY = null;
       public static SortedList<int, ic.expense_accountC> DATA_EXPENSE_ACCOUNTS = null;
       public static SortedList<int, SortedList<int, ic.expense_transC>> DATA_MONTH_EXPENSES = null;
       public static SortedList<int, SortedList<int, ic.expense_transC>> DATA_YEAR_EXPENSES = null;
       public static bool GetMonthExpenses(int m_partition_id)
       {
           if (DATA_MONTH_EXPENSES == null)
           {
               DATA_MONTH_EXPENSES = new SortedList<int, SortedList<int, ic.expense_transC>>();
           }
           if (DATA_MONTH_EXPENSES.Keys.IndexOf(m_partition_id) == -1)
           {
               DATA_MONTH_EXPENSES.Add(m_partition_id, new SortedList<int, ic.expense_transC>());
           }
           if (wdata.DATA_MONTH_STAMP == null)
           {
               wdata.DATA_MONTH_STAMP = new SortedList<string, SortedList<int, long>>();
           }
           string _table_name = "acc_expense_trans_tb";
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
               if (datam.DATA_MONTH_EXPENSES[m_partition_id].Keys.Count == 0)
               {
                   _str = string.Format("select * from " + _table_name + " where m_partition_id={0} and voucher_status={1}", m_partition_id, em.voucher_statusS.valid.ToByte());
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
                   ic.expense_transC _obj = null;
                   while (_dr.Read())
                   {
                       if (!is_updated) { is_updated = true; }
                       _obj = null;
                       if (load_all)
                       {
                           _obj = new ic.expense_transC();
                       }
                       else
                       {
                           try
                           {
                               _obj = datam.DATA_MONTH_EXPENSES[m_partition_id][_dr["un_id"].ToInt32()];
                               is_new = false;
                           }
                           catch (Exception)
                           {
                               if (_obj == null)
                               {
                                   _obj = new ic.expense_transC();
                                   is_new = true;
                               }
                           }

                       }
                       //
                       if (is_new)
                       {
                           _obj.un_id = _dr["un_id"].ToInt32();
                           _obj.voucher_no = _dr["voucher_no"].ToStringNullable();
                           _obj.voucher_id = _dr["voucher_id"].ToInt32();
                           _obj.exp_acc_id = _dr["exp_acc_id"].ToInt32();
                           _obj.exp_amount = _dr["exp_amount"].ToInt32();
                           _obj.exp_details = _dr["exp_details"].ToStringNullable();
                           _obj.exp_date = _dr.GetDateTime("exp_date");
                           _obj.exp_fs_id = _dr["exp_fs_id"].ToInt32();
                           _obj.pc_us_id = _dr["pc_us_id"].ToInt32();
                           _obj.exp_cat_id = _dr["exp_cat_id"].ToInt32();
                           _obj.dept_id = _dr["dept_id"].ToInt32();
                           _obj.dept_parent_id = _dr["dept_parent_id"].ToInt32();
                           _obj.pay_mode = (em.voucher_Paymode)_dr["pay_mode"].ToByte();
                           _obj.cheque_no = _dr["cheque_no"].ToStringNullable();
                           _obj.source_type =(em.exp_inc_src_typeS) _dr["source_type_id"].ToByte();
                           _obj.source_account_id = _dr["source_account_id"].ToInt32();
                           _obj.sys_account_id = _dr["sys_account_id"].ToInt32();
                           _obj.source_id = _dr["source_id"].ToInt32();
                           if (_dr["w_dr_data"] != null)
                           {
                               _obj.w_dr_data = _dr["w_dr_data"].ToStringNullable();
                           }
                           if (_dr["tf_string"] != null)
                           {
                               _obj.tf_string = _dr["tf_string"].ToStringNullable();
                           }
                           //
                           try
                           {
                               _obj.objExpenseAccount = datam.DATA_EXPENSE_ACCOUNTS[_obj.exp_acc_id];
                           }
                           catch (Exception)
                           {
                           
                           }
                           //
                       }
                       _obj.voucher_status = (em.voucher_statusS)_dr["voucher_status"].ToInt16();
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
                           datam.DATA_MONTH_EXPENSES[m_partition_id].Add(_obj.un_id, _obj);
                       }
                   }
               }

               xd.CommitTransaction();
           }
           return is_updated;
       }
       public static bool GetYearExpenses(int year, xing xd)
       {
           if (DATA_YEAR_EXPENSES == null)
           {
               DATA_YEAR_EXPENSES = new SortedList<int, SortedList<int, ic.expense_transC>>();
           }
           if (DATA_YEAR_EXPENSES.Keys.IndexOf(year) == -1)
           {
               DATA_YEAR_EXPENSES.Add(year, new SortedList<int, ic.expense_transC>());
           }
           if (wdata.DATA_MONTH_STAMP == null)
           {
               wdata.DATA_MONTH_STAMP = new SortedList<string, SortedList<int, long>>();
           }
           string _table_name = "acc_expense_trans_tb";
           string _key = string.Format("{0}{1}", _table_name, year);
           if (wdata.TABLE_STAMP.IndexOfKey(_key) == -1)
           {
               wdata.TABLE_STAMP.Add(_key, 0);
              
           }
           string _str = null;
           bool load_all = false;
           bool is_new = false;
           bool is_updated = false;
           long _stamp = 0;
          
               _stamp = xd.GetTimeStamp(_table_name);
               if (datam.DATA_YEAR_EXPENSES[year].Keys.Count == 0)
               {
                   _str = string.Format("select * from " + _table_name + " where YEAR(exp_date)={0} and voucher_status={1}", year, em.voucher_statusS.valid.ToByte());
                   load_all = true;
                   is_new = true;
               }
               else
               {
                   if (_stamp == wdata.TABLE_STAMP[_key])
                   {
                       xd.RollBackTransaction();
                       return false;
                   }
                   _str = string.Format("select * from " + _table_name + " where YEAR(exp_date)={0} and fs_time_stamp > {1}", year, wdata.TABLE_STAMP[_key]);
               }
               wdata.TABLE_STAMP[_key] = _stamp;
               using (var _dr = xd.SelectCommand(_str))
               {
                   if (_dr == null) { return false; }
                   ic.expense_transC _obj = null;
                   while (_dr.Read())
                   {
                       if (!is_updated) { is_updated = true; }
                       _obj = null;
                       if (load_all)
                       {
                           _obj = new ic.expense_transC();
                       }
                       else
                       {
                           try
                           {
                               _obj = datam.DATA_YEAR_EXPENSES[year][_dr["un_id"].ToInt32()];
                               is_new = false;
                           }
                           catch (Exception)
                           {
                               if (_obj == null)
                               {
                                   _obj = new ic.expense_transC();
                                   is_new = true;
                               }
                           }

                       }
                       //
                       if (is_new)
                       {
                           _obj.un_id = _dr["un_id"].ToInt32();
                           _obj.voucher_no = _dr["voucher_no"].ToStringNullable();
                           _obj.voucher_id = _dr["voucher_id"].ToInt32();
                           _obj.exp_acc_id = _dr["exp_acc_id"].ToInt32();
                           _obj.exp_amount = _dr["exp_amount"].ToInt32();
                           _obj.exp_details = _dr["exp_details"].ToStringNullable();
                           _obj.exp_date = _dr.GetDateTime("exp_date");
                           _obj.exp_fs_id = _dr["exp_fs_id"].ToInt32();
                           _obj.pc_us_id = _dr["pc_us_id"].ToInt32();
                           _obj.exp_cat_id = _dr["exp_cat_id"].ToInt32();
                           _obj.dept_id = _dr["dept_id"].ToInt32();
                           _obj.dept_parent_id = _dr["dept_parent_id"].ToInt32();
                           _obj.pay_mode = (em.voucher_Paymode)_dr["pay_mode"].ToByte();
                           _obj.cheque_no = _dr["cheque_no"].ToStringNullable();
                           _obj.source_type = (em.exp_inc_src_typeS)_dr["source_type_id"].ToByte();
                           _obj.source_account_id = _dr["source_account_id"].ToInt32();
                           _obj.sys_account_id = _dr["sys_account_id"].ToInt32();
                           _obj.source_id = _dr["source_id"].ToInt32();
                           try
                           {
                               _obj.objExpenseAccount = datam.DATA_EXPENSE_ACCOUNTS[_obj.exp_acc_id];
                           }
                           catch (Exception)
                           {

                           }
                           //
                       }
                       _obj.voucher_status = (em.voucher_statusS)_dr["voucher_status"].ToInt16();
                       _obj.is_updated = true;
                       if (is_new)
                       {
                           datam.DATA_YEAR_EXPENSES[year].Add(_obj.un_id, _obj);
                       }
                   }
               }

            
           return is_updated;
       }
       public static List<ic.expense_transC> GetChequeExpenses(xing xd, int wdr_id)
       {
           List<ic.expense_transC> _list = new List<ic.expense_transC>();
           string _str = null;
           _str = string.Format("select * from acc_expense_trans_tb where source_type_id={0} and source_id={1} and voucher_status={2}", em.exp_inc_src_typeS.petty_cash_cheque.ToByte(), wdr_id, em.voucher_statusS.valid.ToByte());
           using (var _dr = xd.SelectCommand(_str))
           {
               if (_dr == null) { return null; }
               ic.expense_transC _obj = null;
               while (_dr.Read())
               {
                   _obj = new ic.expense_transC();
                   _obj.un_id = _dr["un_id"].ToInt32();
                   _obj.voucher_no = _dr["voucher_no"].ToStringNullable();
                   _obj.voucher_id = _dr["voucher_id"].ToInt32();
                   _obj.exp_acc_id = _dr["exp_acc_id"].ToInt32();
                   _obj.exp_amount = _dr["exp_amount"].ToInt32();
                   _obj.exp_details = _dr["exp_details"].ToStringNullable();
                   _obj.exp_date = _dr.GetDateTime("exp_date");
                   _obj.exp_fs_id = _dr["exp_fs_id"].ToInt32();
                   _obj.pc_us_id = _dr["pc_us_id"].ToInt32();
                   _obj.exp_cat_id = _dr["exp_cat_id"].ToInt32();
                   _obj.dept_id = _dr["dept_id"].ToInt32();
                   _obj.dept_parent_id = _dr["dept_parent_id"].ToInt32();
                   _obj.pay_mode = (em.voucher_Paymode)_dr["pay_mode"].ToByte();
                   _obj.cheque_no = _dr["cheque_no"].ToStringNullable();
                   _obj.source_type = (em.exp_inc_src_typeS)_dr["source_type_id"].ToByte();
                   _obj.source_account_id = _dr["source_account_id"].ToInt32();
                   _obj.sys_account_id = _dr["sys_account_id"].ToInt32();
                   _obj.source_id = _dr["source_id"].ToInt32();
                   if (_dr["w_dr_data"] != null)
                   {
                       _obj.w_dr_data = _dr["w_dr_data"].ToStringNullable();
                   }
                   if (_dr["tf_string"] != null)
                   {
                       _obj.tf_string = _dr["tf_string"].ToStringNullable();
                   }
                   //
                   try
                   {
                       _obj.objExpenseAccount = datam.DATA_EXPENSE_ACCOUNTS[_obj.exp_acc_id];
                   }
                   catch (Exception)
                   {

                   }
                   _obj.voucher_status = (em.voucher_statusS)_dr["voucher_status"].ToInt16();
                   _obj.received_by = _dr["received_by"].ToStringNullable();
                   _obj.is_updated = true;
                   _list.Add(_obj);
                   _obj = null;
               }
           }
           return _list;
       }
       public static List<ic.expense_transC> GetExpensesByID(xing xd, int _exp_account_id, int _fs_id_1, int _fs_id_2)
       {
           List<ic.expense_transC> _list = new List<ic.expense_transC>();
           string _str = null;
           _str = string.Format("select * from acc_expense_trans_tb where sys_account_id={0} and exp_fs_id between {1} and {2} and voucher_status={3}", _exp_account_id, _fs_id_1, _fs_id_2, em.voucher_statusS.valid.ToByte());
           using (var _dr = xd.SelectCommand(_str))
           {
               if (_dr == null) { return null; }
               ic.expense_transC _obj = null;
               while (_dr.Read())
               {
                   _obj = new ic.expense_transC();
                   _obj.un_id = _dr["un_id"].ToInt32();
                   _obj.voucher_no = _dr["voucher_no"].ToStringNullable();
                   _obj.voucher_id = _dr["voucher_id"].ToInt32();
                   _obj.exp_acc_id = _dr["exp_acc_id"].ToInt32();
                   _obj.exp_amount = _dr["exp_amount"].ToInt32();
                   _obj.exp_details = _dr["exp_details"].ToStringNullable();
                   _obj.exp_date = _dr.GetDateTime("exp_date");
                   _obj.exp_fs_id = _dr["exp_fs_id"].ToInt32();
                   _obj.pc_us_id = _dr["pc_us_id"].ToInt32();
                   _obj.exp_cat_id = _dr["exp_cat_id"].ToInt32();
                   _obj.dept_id = _dr["dept_id"].ToInt32();
                   _obj.dept_parent_id = _dr["dept_parent_id"].ToInt32();
                   _obj.pay_mode = (em.voucher_Paymode)_dr["pay_mode"].ToByte();
                   _obj.cheque_no = _dr["cheque_no"].ToStringNullable();
                   _obj.source_type = (em.exp_inc_src_typeS)_dr["source_type_id"].ToByte();
                   _obj.source_account_id = _dr["source_account_id"].ToInt32();
                   _obj.sys_account_id = _dr["sys_account_id"].ToInt32();
                   _obj.source_id = _dr["source_id"].ToInt32();
                   if (_dr["w_dr_data"] != null)
                   {
                       _obj.w_dr_data = _dr["w_dr_data"].ToStringNullable();
                   }
                   if (_dr["tf_string"] != null)
                   {
                       _obj.tf_string = _dr["tf_string"].ToStringNullable();
                   }
                   //
                   try
                   {
                       _obj.objExpenseAccount = datam.DATA_EXPENSE_ACCOUNTS[_obj.exp_acc_id];
                   }
                   catch (Exception)
                   {

                   }
                   _obj.voucher_status = (em.voucher_statusS)_dr["voucher_status"].ToInt16();
                   _obj.received_by = _dr["received_by"].ToStringNullable();
                   _obj.is_updated = true;
                   _list.Add(_obj);
                   _obj = null;
               }
           }
           return _list;
       }
       public static void FillExpenseAccounts(xing xd)
       {
           if (DATA_EXPENSE_ACCOUNTS == null)
           {
               DATA_EXPENSE_ACCOUNTS = new SortedList<int, ic.expense_accountC>();
           }
           string _table_name="acc_expense_accounts_tb";
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
           if (DATA_EXPENSE_ACCOUNTS.Keys.Count == 0)
           {
               _str = "select * from acc_expense_accounts_tb";
               load_all = true;
           }
           else
           {
               if (wdata.TABLE_STAMP[_table_name] == _stamp)
               {
                   return;
               }
               _str = string.Format("select * from acc_expense_accounts_tb where fs_time_stamp > {0}", wdata.TABLE_STAMP[_table_name]);
               load_all = false;
           }
           wdata.TABLE_STAMP[_table_name] = _stamp;
           ic.expense_accountC _obj = null;
           #region database fill
           using (var _dr = xd.SelectCommand(_str))
           {
               while (_dr.Read())
               {
                   _obj = null;
                   if (load_all)
                   {
                       _obj = new ic.expense_accountC();
                       is_new = true;
                   }
                   else
                   {
                       try
                       {
                           _obj = datam.DATA_EXPENSE_ACCOUNTS[_dr["exp_acc_id"].ToInt32()];
                           is_new = false;
                       }
                       catch (Exception ex)
                       {
                           if (_obj == null)
                           {
                               _obj = new ic.expense_accountC();
                               is_new = true;
                           }
                       }
                   }
                   if (is_new)
                   {
                       _obj.exp_acc_id = _dr["exp_acc_id"].ToInt32();
                    
                       _obj.exp_acc_type = (em.exp_acc_typeS)_dr["exp_acc_type_id"].ToByte();
                       datam.DATA_EXPENSE_ACCOUNTS.Add(_obj.exp_acc_id, _obj);
                   }
                   //
                   _obj.sys_account_id = _dr["sys_account_id"].ToInt32();
                   _obj.cr_account_id = _dr["cr_account_id"].ToInt32();
                   _obj.inc_cg_id = _dr["cr_cg_id"].ToInt32();
                   _obj.cr_un_id = _dr["cr_un_id"].ToInt32();
                   //
                   _obj.inc_sys_account_id = _dr["inc_sys_account_id"].ToInt32();
                   _obj.cuc_account_id = _dr["cuc_account_id"].ToInt32();
                   _obj.exp_acc_name = _dr["exp_acc_name"].ToStringNullable();
                   _obj.exp_cat_id = _dr["exp_cat_id"].ToInt32();
                    if (datam.DATA_EXPENSE_CATEGORY.IndexOfKey(_obj.exp_cat_id) > -1)
                    {
                        _obj.objCategory = (_obj.exp_cat_id == 0) ? null : datam.DATA_EXPENSE_CATEGORY[_obj.exp_cat_id];
                    }
                 
                   _obj.exp_acc_status = (em.exp_acc_statusS)_dr["exp_acc_status"].ToByte();
                   _obj.dept_id = _dr["dept_id"].ToInt32();
                   _obj.dept_parent_id = _dr["dept_parent_id"].ToInt32();
                   _obj.dept_sys_account_id = _dr["dept_sys_account_id"].ToInt32();
                   _obj.objDepartment = (_obj.dept_id == 0) ? null : datam.DATA_DEPARTMENT[_obj.dept_id];
                   _obj.is_updated = true;
               }
               _dr.Close(); _dr.Dispose();
           }
          

           #endregion
       }
       public static void FillExpenseCategories(xing xd)
       {
           if (DATA_EXPENSE_CATEGORY == null)
           {
               DATA_EXPENSE_CATEGORY = new SortedList<int, ic.expense_catC>();
           }
           string _table_name = "acc_expense_cat_tb";
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
           if (DATA_EXPENSE_CATEGORY.Keys.Count == 0)
           {
               _str = "select * from " + _table_name;
               load_all = true;
           }
           else
           {

               if (wdata.TABLE_STAMP[_table_name] == _stamp)
               {
                   return;
               }
               _str = string.Format("select * from " + _table_name + " where fs_time_stamp > {0}", wdata.TABLE_STAMP[_table_name]);
           }
           wdata.TABLE_STAMP[_table_name] = _stamp;
           ic.expense_catC _obj = null;
           #region database fill
           using (var _dr = xd.SelectCommand(_str))
           {
               while (_dr.Read())
               {
                   _obj = null;
                   if (load_all)
                   {
                       _obj = new ic.expense_catC();
                       is_new = true;
                   }
                   else
                   {
                       try
                       {
                           _obj = datam.DATA_EXPENSE_CATEGORY[_dr["exp_cat_id"].ToInt32()];
                           is_new = false;
                       }
                       catch (Exception ex)
                       {
                           if (_obj == null)
                           {
                               _obj = new ic.expense_catC();
                               is_new = true;
                           }
                       }
                   }
                   if (is_new)
                   {
                       _obj.exp_cat_id = _dr["exp_cat_id"].ToInt32();
                      datam.DATA_EXPENSE_CATEGORY.Add(_obj.exp_cat_id, _obj);
                   }
                   _obj.exp_cat_name = _dr["exp_cat_name"].ToStringNullable();
                }
               _dr.Close(); _dr.Dispose();
           }


           #endregion
       }
       public static void InitExpenses(xing xd)
       {
           datam.GetDepartments(xd);
           FillExpenseCategories(xd);
           FillExpenseAccounts(xd);
       }
    }
}
