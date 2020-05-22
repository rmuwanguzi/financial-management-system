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
       public static SortedList<int, ic.bank_reconc_accountC> DATA_BANK_RECONCILIATION_ACCOUNTS { get; set; }
       public static SortedList<em.bank_reconc_typeS, string> DATA_ENUM_BANK_RECONCILIATION_TYPES { get; set; }
       public static void InitBankReconciliation(xing xd)
       {
           if (datam.DATA_ENUM_BANK_RECONCILIATION_TYPES == null)
           {
               datam.DATA_ENUM_BANK_RECONCILIATION_TYPES = new SortedList<em.bank_reconc_typeS, string>();
               datam.DATA_ENUM_BANK_RECONCILIATION_TYPES.Add(em.bank_reconc_typeS.addition, "Addition");
               datam.DATA_ENUM_BANK_RECONCILIATION_TYPES.Add(em.bank_reconc_typeS.deduction, "Deduction");
              // datam.DATA_ENUM_BANK_RECONCILIATION_TYPES.Add(em.bank_reconc_account_typeS.transfer, "Transfer");
           }
            fill_Bank_Reconciliation_Accounts(xd);
            GetBankAccounts(xd);
       }
       public static bool fill_Bank_Reconciliation_Accounts(xing xd)
       {
          // if(datam.)
         //  InitBankReconciliation(xd);
           if (datam.DATA_BANK_RECONCILIATION_ACCOUNTS == null)
           {
               datam.DATA_BANK_RECONCILIATION_ACCOUNTS = new SortedList<int, ic.bank_reconc_accountC>();
           }
           string _table_name = "acc_bank_reconc_accounts_tb";
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
           if (DATA_BANK_RECONCILIATION_ACCOUNTS.Keys.Count == 0)
           {
               _str = "select * from " + _table_name;
               load_all = true;
           }
           else
           {
               if (wdata.TABLE_STAMP[_table_name] == _stamp)
               {
                   return false;
               }
               _str = string.Format("select * from " + _table_name + " where fs_time_stamp > {0}", wdata.TABLE_STAMP[_table_name]);
           }
           wdata.TABLE_STAMP[_table_name] = _stamp;
           ic.bank_reconc_accountC _obj = null;
         
           using (var _dr = xd.SelectCommand(_str))
           {
               while (_dr.Read())
               {
                   _obj = null;
                   if (load_all)
                   {
                       _obj = new ic.bank_reconc_accountC();
                       is_new = true;
                   }
                   else
                   {
                       try
                       {
                           _obj = datam.DATA_BANK_RECONCILIATION_ACCOUNTS[_dr["br_acc_id"].ToInt32()];
                           is_new = false;
                       }
                       catch (Exception ex)
                       {
                           if (_obj == null)
                           {
                               _obj = new ic.bank_reconc_accountC();
                               is_new = true;
                           }
                       }
                   }
                   if (is_new)
                   {
                       _obj.br_acc_id = _dr["br_acc_id"].ToInt32();
                       _obj.sys_account_id = _dr["sys_account_id"].ToInt32();
                       _obj.br_acc_name = _dr["br_acc_name"].ToStringNullable();
                       _obj.br_acc_type = (em.bank_reconc_typeS)_dr["br_acc_type_id"].ToInt16();
                       datam.DATA_BANK_RECONCILIATION_ACCOUNTS.Add(_obj.br_acc_id, _obj);
                   }
                  
               }
               _dr.Close();
           }
           return true;
       }
          //
        public static SortedList<int, SortedList<int, ic.bank_reconc_transC>> DATA_MONTH_BANK_RECONCILIATION { get; set; }
        public static bool GetMonthBankReconcilition(int m_partition_id)
        {
            if (DATA_MONTH_BANK_RECONCILIATION == null)
            {
                DATA_MONTH_BANK_RECONCILIATION = new SortedList<int, SortedList<int, ic.bank_reconc_transC>>();
             
            }
            if (DATA_MONTH_BANK_RECONCILIATION.Keys.IndexOf(m_partition_id) == -1)
            {
                DATA_MONTH_BANK_RECONCILIATION.Add(m_partition_id, new SortedList<int, ic.bank_reconc_transC>());
            }
            if (wdata.DATA_MONTH_STAMP == null)
            {
                wdata.DATA_MONTH_STAMP = new SortedList<string, SortedList<int, long>>();
            }

            string _table_name = "acc_bank_reconc_trans_tb";
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
                if (datam.DATA_MONTH_BANK_RECONCILIATION[m_partition_id].Keys.Count == 0)
                {
                    _str = string.Format("select * from " + _table_name + " where m_partition_id={0} and status={1}", m_partition_id, em.voucher_statusS.valid.ToByte());
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
                    ic.bank_reconc_transC _obj = null;
                    while (_dr.Read())
                    {
                        if (!is_updated) { is_updated = true; }
                        _obj = null;
                        if (load_all)
                        {
                            _obj = new ic.bank_reconc_transC();
                        }
                        else
                        {
                            try
                            {
                                _obj = DATA_MONTH_BANK_RECONCILIATION[m_partition_id][_dr["un_id"].ToInt32()];
                                is_new = false;
                            }
                            catch (Exception)
                            {
                                if (_obj == null)
                                {
                                    _obj = new ic.bank_reconc_transC();
                                    is_new = true;
                                }
                            }

                        }
                        //
                        if (is_new)
                        {
                            _obj.un_id = _dr["un_id"].ToInt32();
                            _obj.br_acc_id = _dr["br_acc_id"].ToInt32();
                            _obj.br_acc_type = (em.bank_reconc_typeS)_dr["br_acc_type_id"].ToInt32();
                            _obj.bank_account_id = _dr["bank_account_id"].ToInt32();
                            _obj.amount = _dr["amount"].ToInt32();
                            _obj.desc = _dr["description"].ToStringNullable();
                            _obj.status = (em.voucher_statusS)_dr["status"].ToInt16();
                            _obj.m_partition_id = _dr["m_partition_id"].ToInt32();
                            _obj.sys_account_id = _dr["sys_account_id"].ToInt32();
                            _obj.fs_id = _dr["fs_id"].ToInt32();
                            _obj.fs_date = _dr.GetDateTime("fs_date");
                            //
                            _obj.objReconcAccount = datam.DATA_BANK_RECONCILIATION_ACCOUNTS[_obj.br_acc_id];
                            _obj.objBankAccount = datam.DATA_BANK_ACCOUNTS[_obj.bank_account_id];
                        }
                        _obj.status = (em.voucher_statusS)_dr["status"].ToInt16();
                        _obj.is_updated = true;
                        //if (_dr["del_fs_date"] != null)
                        //{
                        //    //if (_obj.objDelInfo == null) { _obj.objDelInfo = new ic.deleteBaseC(); }
                        //    //_obj.objDelInfo.del_fs_date = _dr.GetDateTime("del_fs_date");
                        //    //_obj.objDelInfo.del_fs_id = fn.GetFSID(_obj.objDelInfo.del_fs_date.Value);
                        //    //_obj.objDelInfo.del_fs_time = _dr["del_fs_time"].ToString()Nullable()();
                        //    //_obj.objDelInfo.del_pc_us_id = _dr["del_pc_us_id"].ToInt32();
                        //}
                        if (is_new)
                        {
                            DATA_MONTH_BANK_RECONCILIATION[m_partition_id].Add(_obj.un_id, _obj);
                        }
                    }
                }

                xd.CommitTransaction();
            }
            return is_updated;
        }

    }
}
