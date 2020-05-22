using SdaHelperManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTOMS
{
   public partial class accn
    {
       public static List<ic.account_AL_statementC> DATA_ASSET_LIABILITY_STATEMENT { get; set; }
       public static void Get_AssetLiabilityAccountStatement(xing xd, int start_fs_id, int end_fs_id, int account_id,string _account_name)
       {
           datam.InitAccount(xd);
           datam.LoadCrExpOffItems(xd);
           ic.accountC PrimaryAccount = datam.DATA_ACCOUNTS[account_id];
          
           switch(PrimaryAccount.account_dept_category)
           {
               case em.account_d_categoryS.Liablity:
                   {
                       var _cr_inc_item = datam.DATA_CR_EXP_OFFITEMS.Values.Where(k => k.gen_account_id == account_id & k._type == em.link_accTypes.creditor).FirstOrDefault();
                       if (_cr_inc_item == null)
                       {
                           return;
                       }
                       
                       int cg_id = _cr_inc_item.cg_id;
                       int _income_acc_id = _cr_inc_item.account_id;
                       ic.accountC _parent = datam.DATA_ACCOUNTS[_income_acc_id].p_account_id == 0 ? null : datam.DATA_ACCOUNTS[datam.DATA_ACCOUNTS[_income_acc_id].p_account_id];
                       if (_parent != null && _parent.account_id == -2360)
                       {
                           ic.church_sub_unitC _unit = (from c in datam.DATA_CHURCH_SUB_UNIT.Values
                                                        where c.sys_gp_account_id == _income_acc_id
                                                        select c).FirstOrDefault();
                           if (_unit != null)
                           {
                               int _expense_acc_id = datam.DATA_CR_EXP_OFFITEMS.Values.Where(k => k.link_id == _cr_inc_item.link_id & k._type == em.link_accTypes.expense_accrued).FirstOrDefault().gen_account_id;
                                //
                                Get_OffAccountsStatement(xd, start_fs_id, end_fs_id, _unit, _account_name);
                               Get_CreditorExpenseStatement(xd, start_fs_id, end_fs_id, _expense_acc_id);
                               Get_AccountTransfers(xd, start_fs_id, end_fs_id, account_id);
                               //
                           }
                       }
                       else
                       {
                           int _expense_acc_id = datam.DATA_CR_EXP_OFFITEMS.Values.Where(k => k.link_id == _cr_inc_item.link_id & k._type == em.link_accTypes.expense_accrued).FirstOrDefault().gen_account_id;
                            //
                            var _income_account = datam.DATA_ACCOUNTS[_income_acc_id];
                            if (_income_account.owner_type == em.AccountOwnerTypeS.CHURCH_GROUP_SHARED | _income_account.account_id == -2435)
                            {
                                cg_id = _cr_inc_item.cg_id;
                            }
                            else
                            {
                                cg_id = -1;
                            }
                            Get_OffAccountsStatement(xd, start_fs_id, end_fs_id, _income_acc_id, cg_id, _account_name);
                           Get_CreditorExpenseStatement(xd, start_fs_id, end_fs_id, _expense_acc_id);
                           Get_AccountTransfers(xd, start_fs_id, end_fs_id, account_id);
                           //
                       }
                       break;
                   }
               case em.account_d_categoryS.Asset:
                   {
                       ic.accountC _account = datam.DATA_ACCOUNTS[account_id];
                       switch(_account.p_account_id)
                       {
                           case -2499://accn.GetAccountByAlias("BANK_ACCOUNT").account_id:
                               {
                                   datam.GetBankAccounts(xd);
                                   ic.bankAccountC _bank = (from k in datam.DATA_BANK_ACCOUNTS.Values
                                                               where k.sys_account_id == account_id
                                                               select k).FirstOrDefault();
                                   if (_bank != null)
                                   {
                                       Get_BankingStatement(xd, start_fs_id, end_fs_id, _bank.un_id);
                                       Get_BankingWithDrawStatement(xd, start_fs_id, end_fs_id, _bank.un_id);
                                       Get_BankExpenseStatement(xd, start_fs_id, end_fs_id, account_id);
                                   }
                                   break;
                               }
                           case -2404:// accn.GetAccountByAlias("UNBANKED").account_id:
                               {

                                   break;
                               }
                       }
                       
                       break;
                   }
           }
       }
       private static void Get_AccountTransfers(xing xd, int start_fs_id, int end_fs_id, int account_id)
       {
           string _str = string.Format("select m.un_id, m.source_id, m.destination_id,m.amount,m.transfer_reason,m.fs_date,m.fs_id from acc_cash_transfer_tb as m where m.fs_id between {0} and {1} and" +
               "(m.source_id={2} or m.destination_id={2}) and m.destination_type_id={3} and m.source_type_id={4} " +
                "and m.status={5}", start_fs_id, end_fs_id, account_id, em.CashTransferDestinationType.account.ToInt16(), em.CashTransferSourceType.account.ToInt16(), em.cashTransferStatus.valid.ToInt16());
           ic.account_AL_statementC _obj = null;
           int _amount=0;
           using (var _dr = xd.SelectCommand(_str))
           {
               while (_dr.Read())
               {
                   _obj = new ic.account_AL_statementC();
                   _amount = _dr["amount"].ToInt32();
                  
                   if (account_id == _dr["source_id"].ToInt16())
                   {
                       if (_dr["destination_id"].ToInt32() == accn.GetAccountByAlias("LCB_CREDITOR_REDUCTION_INCOME").account_id)
                       {
                           _obj.description = string.Format("CASH Transfer To LCB");

                       }
                       else
                       {
                           _obj.description = string.Format("CASH Transfer To {0}", datam.DATA_ACCOUNTS[_dr["destination_id"].ToInt32()].account_name);
                       }
                       _obj.statement_type = em.account_statement_typeS.account_transfer_minus;
                       _obj.cr_amount = _amount;
                   }
                   else
                   {
                       _obj.description = string.Format("CASH Transfer From {0}", datam.DATA_ACCOUNTS[_dr["source_id"].ToInt32()].account_name);
                       _obj.statement_type = em.account_statement_typeS.account_transfer_add;
                       _obj.dr_amount = _amount;
                   }
                   _obj.fs_date = _dr.GetDateTime("fs_date");
                   _obj.fs_id = _dr["fs_id"].ToInt32();
                   _obj.reference_no = string.Format("TFR-{0}", _dr["un_id"].ToInt32());
                   _obj.reference_type = em.account_AL_referenceTypeS.account_transfer;
                   
                   accn.DATA_ASSET_LIABILITY_STATEMENT.Add(_obj);
               }
           }
       }
       private static void Get_OffAccountsStatement(xing xd,int start_fs_id, int end_fs_id , int account_id, int cg_id,string _account_name)
       {
            string _str = null;
            if (cg_id < 0)
            {
                _str = string.Format("select m.amount,m.fs_id,m.fs_date,c.source_name,c.off_id from off_accounts_tb as m,off_main_tb as c where m.fs_id between {0} and {1} and m.account_id={2} and m.off_id=c.off_id and m.receipt_status=1", start_fs_id, end_fs_id, account_id);
            }
            else
            {
                _str = string.Format("select m.amount,m.fs_id,m.fs_date,c.source_name,c.off_id from off_accounts_tb as m,off_main_tb as c where m.fs_id between {0} and {1} and m.account_id={2} and m.cg_id={3} and m.off_id=c.off_id and m.receipt_status=1", start_fs_id, end_fs_id, account_id, cg_id);
            }
            ic.account_AL_statementC _obj = null;
           using(var _dr= xd.SelectCommand(_str))
           {
               while(_dr.Read())
               {
                   _obj = new ic.account_AL_statementC();
                   _obj.dr_amount = _dr["amount"].ToInt32();
                   _obj.description = _dr["source_name"].ToStringNullable();
                   _obj.fs_date = _dr.GetDateTime("fs_date");
                   _obj.fs_id = _dr["fs_id"].ToInt32();
                   _obj.reference_no = string.Format("R-{0}", _dr["off_id"].ToInt32());
                   _obj.reference_type = em.account_AL_referenceTypeS.receipt;
                   _obj.statement_type = em.account_statement_typeS.income_offering;
                    _obj.account_name = _account_name;
                   accn.DATA_ASSET_LIABILITY_STATEMENT.Add(_obj);
               }
           }
       }
       private static void Get_BankingStatement(xing xd,int start_fs_id, int end_fs_id , int account_id)
       {
           string _str = string.Format("select * from acc_bank_deposit_tb  where fs_id between {0} and {1} and bank_account_id={2} and status=0", start_fs_id, end_fs_id, account_id);
           ic.account_AL_statementC _obj = null;
           using (var _dr = xd.SelectCommand(_str))
           {
               while (_dr.Read())
               {
                   _obj = new ic.account_AL_statementC();
                   _obj.dr_amount = _dr["deposited_amount"].ToInt32();
                   _obj.description = "CASH TO BANK";
                   _obj.fs_date = _dr.GetDateTime("fs_date");
                   _obj.fs_id = _dr["fs_id"].ToInt32();
                   _obj.reference_no = string.Format("{0}", _dr["voucher_no"].ToString());
                   _obj.reference_type = em.account_AL_referenceTypeS.banking_slip;
                   _obj.statement_type = em.account_statement_typeS.bank_deposit;
                 
                   accn.DATA_ASSET_LIABILITY_STATEMENT.Add(_obj);
               }
           }
       }
       private static void Get_BankingWithDrawStatement(xing xd,int start_fs_id, int end_fs_id , int account_id)
       {
           string _str = string.Format("select * from acc_bank_withdraw_tb  where fs_id between {0} and {1} and bank_account_id={2} and status=0", start_fs_id, end_fs_id, account_id);
           ic.account_AL_statementC _obj = null;
           using (var _dr = xd.SelectCommand(_str))
           {
               while (_dr.Read())
               {
                   _obj = new ic.account_AL_statementC();
                   _obj.cr_amount = _dr["w_amount"].ToInt32();
                   if (_dr["cheque_alias"] != null)
                   {
                       _obj.description = string.Format("WithDraw {0}",_dr["cheque_alias"].ToString());
                   }
                   else
                   {
                       _obj.description = string.Format("Cheque WithDraw");
                   }
                   _obj.fs_date = _dr.GetDateTime("fs_date");
                   _obj.fs_id = _dr["fs_id"].ToInt32();
                   _obj.reference_no = string.Format("{0}", _dr["cheque_no"].ToString());
                   _obj.reference_type = em.account_AL_referenceTypeS.cheque;
                   _obj.statement_type = em.account_statement_typeS.bank_withdraw;
                   _obj.record_id = _dr["wdr_id"].ToInt32();
                   accn.DATA_ASSET_LIABILITY_STATEMENT.Add(_obj);
               }
           }
       }
       private static void Get_CreditorExpenseStatement(xing xd,int start_fs_id, int end_fs_id , int account_id)
       {
           string _str = string.Format("select voucher_id,voucher_no,exp_fs_id,exp_date,exp_details,exp_amount from acc_expense_trans_tb where exp_fs_id between {0} and {1} and sys_account_id={2} and voucher_status=0", start_fs_id, end_fs_id, account_id);
           ic.account_AL_statementC _obj = null;
           using (var _dr = xd.SelectCommand(_str))
           {
               while (_dr.Read())
               {
                   _obj = new ic.account_AL_statementC();
                   _obj.cr_amount = _dr["exp_amount"].ToInt32();
                   _obj.description = _dr["exp_details"].ToStringNullable();
                   _obj.fs_date = _dr.GetDateTime("exp_date");
                   _obj.fs_id = _dr["exp_fs_id"].ToInt32();
                   _obj.reference_no = string.Format("{0}", _dr["voucher_no"].ToString());
                   _obj.reference_type = em.account_AL_referenceTypeS.voucher;
                   _obj.statement_type = em.account_statement_typeS.expense_payment;
                   accn.DATA_ASSET_LIABILITY_STATEMENT.Add(_obj);
               }
           }
       }
       private static void Get_BankExpenseStatement(xing xd,int start_fs_id, int end_fs_id , int account_id)
       {
           string _str = string.Format("select voucher_id,voucher_no,exp_fs_id,exp_date,exp_details,exp_amount,pay_mode,cheque_no from acc_expense_trans_tb where exp_fs_id between {0} and {1} and source_account_id={2} and source_type_id=3 and voucher_status=0", start_fs_id, end_fs_id, account_id);
           ic.account_AL_statementC _obj = null;
           using (var _dr = xd.SelectCommand(_str))
           {
               while (_dr.Read())
               {
                   _obj = new ic.account_AL_statementC();
                   _obj.cr_amount = _dr["exp_amount"].ToInt32();
                   _obj.description = _dr["exp_details"].ToStringNullable();
                   _obj.fs_date = _dr.GetDateTime("exp_date");
                   _obj.fs_id = _dr["exp_fs_id"].ToInt32();
                   //
                   switch((em.voucher_Paymode)_dr["pay_mode"].ToInt32())
                   {
                       case em.voucher_Paymode.cheque:
                           {
                               _obj.reference_no = string.Format("{0}", _dr["cheque_no"].ToString());
                               _obj.reference_type = em.account_AL_referenceTypeS.cheque;
                               break;
                           }
                       case em.voucher_Paymode.bank_transfer:
                           {
                               _obj.reference_no = string.Format("{0}", _dr["voucher_no"].ToString());
                               _obj.reference_type = em.account_AL_referenceTypeS.bank_transfer;
                               break;
                           }
                       default:
                           {
                               _obj.reference_no = string.Format("{0}", _dr["voucher_no"].ToString());
                               _obj.reference_type = em.account_AL_referenceTypeS.voucher;
                               break;
                           }
                   }
                   _obj.statement_type = em.account_statement_typeS.bank_expense;
                   accn.DATA_ASSET_LIABILITY_STATEMENT.Add(_obj);
               }
           }
       }
       private static void Get_OffAccountsStatement(xing xd,int start_fs_id, int end_fs_id, ic.church_sub_unitC _unit,string _account_name )
       {
           string _str = string.Format("select source_name,source_type_id,source_id,fs_date,fs_id,off_id,receipt_total,transaction_id from off_main_tb where fs_id between {0} and {1} and source_type_id={2} and source_id={3} and receipt_status=1", start_fs_id, end_fs_id, em.off_source_typeS.church_sub_unit.ToInt16(), _unit.sb_unit_id);
           ic.account_AL_statementC _obj = null;
           List<ic.account_AL_statementC> _temp_list = new List<ic.account_AL_statementC>();
           using (var _dr = xd.SelectCommand(_str))
           {
               while (_dr.Read())
               {
                   _obj = new ic.account_AL_statementC();
                   _obj.dr_amount = 0;
                   _obj.description = _dr["source_name"].ToStringNullable();
                   _obj.fs_date = _dr.GetDateTime("fs_date");
                   _obj.fs_id = _dr["fs_id"].ToInt32();
                   _obj.reference_no = string.Format("R-{0}", _dr["off_id"].ToInt32());
                   _obj.reference_type = em.account_AL_referenceTypeS.receipt;
                   _obj.statement_type = em.account_statement_typeS.income_offering;
                   _obj.transaction_id = _dr["transaction_id"].ToInt64();
                    _obj.account_name = _account_name;
                   _temp_list.Add(_obj);
                   _obj = null;
                 
               }
           }
           var _cr_account_id = accn.GetCrExpLinkAccountID(em.link_accTypes.creditor, datam.DATA_ACCOUNTS[_unit.sys_gp_account_id], xd);
           foreach(var k in _temp_list)
           {
               k.dr_amount = xd.ExecuteScalarInt(string.Format("select sum(cr) as _sm from journal_tb where transaction_id={0} and account_id={1}", k.transaction_id, _cr_account_id));
               if (k.dr_amount > 0)
               {
                   accn.DATA_ASSET_LIABILITY_STATEMENT.Add(k);
               }
           }
       }
    }
}
