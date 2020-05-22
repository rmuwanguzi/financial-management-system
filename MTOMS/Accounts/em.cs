using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTOMS
{
   public partial class em
    {
       public enum account_d_categoryS
       {
           Asset=1,Liablity=2,Equity=3,Personal=4,PL=5
       }
       public enum account_typeS
       {
           ActualAccount=10,
           GroupAccount=11,
           SubGroupAccount=12,
           All
       }
       public enum account_extension_purposeS
       {
           none=-1,
           analysis_only,
           finance_and_analysis
       }
       public enum account_statusS
       {
           Enabled=1,Disabled=2,Expired=3,Deleted=4,DeActivated,DisContinued
       }
       public enum account_trans_PersonalAccType
       {
           supplier=3,
           customer,
           staff,
           director,
           shareholder
       }
       public enum acc_payable_trans_typeS
       {
           offering=0,
           invoice,
           payment,
           opening_balance
       }
       public enum postTypeS
       {
           none=0,
           cash,
           cash_accounts_payable
       }
        public enum account_d_typeS
       {
           CurrentAssets = 1,
           FixedAssets = 2,
           CurrentLiabilities = 3,
           LongTermLiabilities = 4,
           Expenses = 8,
           Equity = 9,
           Bank=12,
           Stock=11,
           Income=13,
           LCB_INCOME=14
       }
        public enum j_sectionS
        {
            cash=0,
            bank,
            income,
            expense,
            loss,
            debtor,
            creditor,
            open_bal_equity,
            unbanked,
            expense_accrued,
            prepaid_income,
            long_term_asset,
            adj_bal_equity,
            petty_cash,
            asset,
            lcb_income
        }
       public enum statementTypeS
       {

       }
       public enum CashTransferSourceType
       {
           none=-1,
           expense_cheque,
           account,
           
       }
       public enum CashTransferDestinationType
       {
           none=-1,
           un_banked_cash_account,
           bank,
           account
       }
        public enum CashBookSourceTable
        {
            off_accounts_tb=0,

        }
        public enum AccountOwnerTypeS
        {
            None=0,
            CUC = 11,
            CUC_CHURCH,
            CHURCH,
            DISTRICT,
            CHURCH_GROUP,
            DEPARTMENT,
            CHURCH_MEMBER,
            CHURCH_PROJECT,
            OTHER,
            CHURCH_GROUP_SHARED,
            DEPARTMENTS_SHARED,
            CHURCH_SUB_UNIT_PARENT,
            CHURCH_SUB_UNIT,
        }
       public enum account_trans_Type
       {
           Normal,System
       }
       public enum account_trans_Status
       {
           Normal=0,
           Cancelled
       }
       public enum account_payMode
       {
           Cash=12,
           Cheque,
           PsnAccount
       }
       public enum link_accTypes
       {
           none = -1,
           creditor = em.j_sectionS.creditor,
           expense_accrued = em.j_sectionS.expense_accrued
       }
       public enum account_statement_typeS
       {
           opening_balance=0,
           income_offering,
           expense_payment,
           expense,
           account_transfer_add,
           bank_deposit,
           bank_withdraw,
           bank_expense,
           account_transfer_minus
       }
       public enum account_AL_referenceTypeS
       {
           none=-1,
           receipt=0,
           voucher,
           bill,
           banking_slip,
           cheque,
           bank_transfer,
           account_transfer
           

       }
    }
}
