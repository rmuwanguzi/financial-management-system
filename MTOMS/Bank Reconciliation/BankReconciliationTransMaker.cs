using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;
using SdaHelperManager.Security;
using SdaHelperManager;

namespace MTOMS
{
    public partial class BankReconciliationTransMaker : DevComponents.DotNetBar.Office2007Form
    {
        public BankReconciliationTransMaker()
        {
            InitializeComponent();
        }
        private iGDropDownList m_Banks = null;
        private iGDropDownList m_RecAccounts = null;
        private enum _process
        {
            form_loading = 0,
            after_save
        }
        _process m_process = _process.form_loading;
       
        DateTime? m_SelectedDate = null;
        private void ExpenseMaker_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
            CenterToScreen();
            InitIgridColumns();
            this.fGrid.AfterCommitEdit += fGrid_AfterCommitEdit;
            datam.ShowWaitForm();
            Application.DoEvents();
            datam.SecurityCheck();
            this.Tag = null;
            backworker.RunWorkerAsync();
        }

        

        void InitIgridColumns()
        {
            #region Adjust the grid's appearance
            // Set the flat appearance for the cells.
            fGrid.Appearance = iGControlPaintAppearance.StyleFlat;
            fGrid.UseXPStyles = false;
            fGrid.ScrollBarSettings.Appearance = iGControlPaintAppearance.StyleFlat;
            fGrid.FocusRect = false;
            #endregion
            iGCol myCol;
            //
            myCol = fGrid.Cols.Add("name", "Field Name");
            myCol.CellStyle.ReadOnly = iGBool.True;
            myCol.SortType = iGSortType.None;
            myCol.Width = 20;
            myCol.CellStyle.ForeColor = Color.DarkBlue;
            myCol.CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
            myCol.CellStyle.ReadOnly = iGBool.True;
            myCol.IncludeInSelect = false;
            myCol.CellStyle.BackColor = Color.WhiteSmoke;
            //
            myCol.AllowSizing = false;
            myCol = fGrid.Cols.Add("desc", "Description");
            myCol.Width = 150;
            //  myCol.AllowSizing = false;
            //
            // Add a special column which will store the category name.
            // This column will be used for grouping.
            fGrid.Cols.Add("category", string.Empty).Visible = false;
            // Add a special column which will store the default 
            // values for the properties.
            fGrid.Cols.Add("svalue", string.Empty).Visible = false;
            myCol = fGrid.Cols["svalue"];
            myCol.SortType = iGSortType.ByValue;
            fGrid.DefaultRow.Height = fGrid.GetPreferredRowHeight(true, false);
            fGrid.DefaultAutoGroupRow.Height = fGrid.DefaultRow.Height;
        }
        private void backworker_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (m_process)
            {
                case _process.form_loading:
                    {
                        datam.SystemInitializer();
                        using (var xd = new xing())
                        {
                            datam.GetBankAccounts(xd);
                            datam.InitBankReconciliation(xd);
                           
                            xd.CommitTransaction();
                        }
                        break;
                    }
                
            }
           
        }
      
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                using (var _fm = new Bank_Reconciliation.BankReconciliationAccountsMaker())
                {
                    _fm.ShowDialog();
                    if (_fm.Tag != null)
                    {
                        if (datam.DATA_BANK_RECONCILIATION_ACCOUNTS != null)
                        {
                            var nlist = from k in datam.DATA_BANK_RECONCILIATION_ACCOUNTS.Values
                                        orderby k.br_acc_name
                                        select k;
                                 m_RecAccounts.Items.Clear();
                                foreach (var k in nlist)
                                {
                                    m_RecAccounts.Items.Add(new fnn.iGComboItemEX()
                                        {
                                            Tag = k,
                                            Value = k.br_acc_name
                                        });
                                }
                            
                        }
                    }
                }
                fGrid.Focus();
                fGrid.Cells["exp_account", 1].Value = null;
                fGrid.Cells["exp_account", 1].AuxValue = null;
                fGrid.SetCurCell("exp_account", 1);
                return true;
            }
            if (keyData == Keys.Tab)
            {
                buttoncreate.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
         private void backworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch (m_process)
            {
                case _process.form_loading:
                    {
                        LoadMainGrid();
                       
                        LoadDropDownLists();
                        datam.HideWaitForm();
                        fGrid.Focus();
                        fGrid.SetCurCell("start_date", 1);
                        break;
                    }
                case _process.after_save:
                    {
                        if( this.Owner is BankReconcManager)
                        {
                            (this.Owner as BankReconcManager).CheckForUpdates();
                        }
                        break;
                    }
               
               
            }
            
        }
         private void LoadDropDownLists()
         {
             m_Banks = fnn.CreateCombo();
             m_RecAccounts = fnn.CreateCombo();
             fGrid.Rows["exp_account"].Cells[1].DropDownControl = m_RecAccounts;
                   
             //
             foreach(var k in datam.DATA_BANK_RECONCILIATION_ACCOUNTS.Values)
             {
                 m_RecAccounts.Items.Add(new fnn.iGComboItemEX()
                     {
                         Value = k.br_acc_name,
                         Tag = k
                     });
             }
             //
             foreach(var k in datam.DATA_BANK_ACCOUNTS.Values)
             {
                 m_Banks.Items.Add(new fnn.iGComboItemEX()
                 {
                     Value = k.BankAccountFullName,
                     Tag = k
                 });
             }

              fGrid.Rows["bank"].Cells[1].DropDownControl = m_Banks;
             
            
            
         }
         private iGRow CreateNewRow(string field, string rowkey, Type _type, int group_index, string group_name)
         {
             var _row = fGrid.Rows.Add();
             _row.Font = new Font("georgia", 14, FontStyle.Regular);
             _row.Cells["desc"].Font = new Font("arial", 14, FontStyle.Regular);
             _row.Cells["name"].Col.Width = 230;
             _row.Cells["name"].Value = field;
             _row.Cells["name"].TextAlign = iGContentAlignment.BottomRight;
             _row.Cells["desc"].ValueType = _type;
             _row.Cells["category"].Value = group_name;
             _row.Cells["svalue"].Value = group_index;
             _row.Key = rowkey;
             _row.AutoHeight();
             _row.Height += 2;
             return _row;
         }
         private void LoadMainGrid()
         {
             fGrid.BeginUpdate();
             iGRow _row = null;
             _row = fGrid.Rows.Add();
             _row.Height = 4;
             _row.BackColor = Color.Lavender;
             _row.Selectable = false;
             int group_index = 1;
             string group_name = "New Account Information";
            //
             var _date = new fnn.DropDownCalenderX();
             _date.start_date = new DateTime(2015, 1, 1);
             _date.end_date = sdata.CURR_DATE;
             _date.selected_date = sdata.CURR_DATE;
             _row = CreateNewRow("Date", "start_date", typeof(string), group_index, group_name);
             //
             _row.Cells["desc"].TypeFlags = iGCellTypeFlags.ComboPreferValue | iGCellTypeFlags.NoTextEdit | iGCellTypeFlags.HasEllipsisButton;
             _row.Cells["desc"].FormatString = "{0:D}";// {0:d} for short date string
             _row.Cells["desc"].DropDownControl = _date;
             _row.Cells["desc"].Value = null;
             //
              _row = CreateNewRow("Bank Name", "bank", typeof(string), group_index, group_name);
             _row = fGrid.Rows.Add();
             _row.Height = 4;
             _row.BackColor = Color.Lavender;
             _row.Selectable = false;
             
             _row = CreateNewRow("(+F1)  Account Name", "exp_account", typeof(string), group_index, group_name);
             _row = CreateNewRow("Account Type", "category", typeof(string), group_index, group_name);
             _row.Cells[1].Enabled = iGBool.False;
             //
            
             //
             _row = fGrid.Rows.Add();
             _row.Height = 3;
             _row.BackColor = Color.Lavender;
             _row.Selectable = false;
             //
             _row = CreateNewRow("Amount", "amount", typeof(decimal), group_index, group_name);
             _row.Cells[1].FormatString = "{0:N0}";
             //
             _row = fGrid.Rows.Add();
             _row.Height = 3;
             _row.BackColor = Color.Lavender;
             _row.Selectable = false;

             _row = CreateNewRow("Enter \n Particulars", "desc", typeof(string), group_index, group_name);
             _row.Height = 60;
             _row.Cells["desc"].TextFormatFlags = iGStringFormatFlags.WordWrap;
             _row.Cells["name"].TextAlign = iGContentAlignment.MiddleRight;
             //
             
             
             _row = fGrid.Rows.Add();
             _row.Height = 5;
             _row.BackColor = Color.Lavender;
             _row.Selectable = false;
             
              fGrid.EndUpdate();
         }

         private void buttonX2_Click(object sender, EventArgs e)
         {
             ClearGrid();
         }
       
         private void fGrid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
         {
             if (fGrid.Rows[e.RowIndex].Cells[e.ColIndex].DropDownControl != null && fGrid.Rows[e.RowIndex].Cells[e.ColIndex].AuxValue == null)
             {
                 fGrid.Rows[e.RowIndex].Cells[e.ColIndex].Value = null;
                 fGrid.SetCurCell(e.RowIndex, 1);
               
                 if (fGrid.Rows[e.RowIndex].Key == "exp_account")
                 {
                    fGrid.Rows["category"].Cells[1].Value = null;
                 }
                 return;
             }
             
             
             if (fGrid.Rows[e.RowIndex].Key == "exp_account" & fGrid.Rows[e.RowIndex].Cells[1].AuxValue!=null)
             {
                
                     var _exp = (fGrid.Rows[e.RowIndex].Cells[1].AuxValue as fnn.iGComboItemEX).Tag as ic.bank_reconc_accountC;
                     if (_exp != null)
                     {
                         fGrid.Rows["category"].Cells[1].Value = _exp.br_acc_type == em.bank_reconc_typeS.addition ? "Income" : "Expense";
                     }
                     else
                     {

                         fGrid.Rows["category"].Cells[1].Value = null;
                     }
                
             }
             if (e.RowIndex != fGrid.Rows.Count - 1)
             {
                 if (fGrid.Rows[e.RowIndex].Cells["desc"].Value == null) { return; }
                 for (int k = e.RowIndex + 1; k < fGrid.Rows.Count; k++)
                 {
                     if (fGrid.Rows[k].Type == iGRowType.AutoGroupRow)
                     {
                         fGrid.Rows[k].Expanded = true;
                         continue;
                     }
                     if (!fGrid.Rows[k].Visible) { continue; }
                     if (fGrid.Rows[k].Cells[1].Enabled == iGBool.False) { continue; }
                     if (string.IsNullOrEmpty(fGrid.Rows[k].Key)) { continue; }
                     if ((e.RowIndex + 3) < fGrid.Rows.Count)
                     {
                         fGrid.Rows[(e.RowIndex + 3)].EnsureVisible();
                     }
                     fGrid.SetCurCell(k, e.ColIndex);
                     break;
                 }
             }
         }
         private void ClearGrid()
         {

             if (checkBox1.Checked & fGrid.Rows["start_date"].Cells[1].Value != null)
             {
                 var _prev_date = System.Convert.ToDateTime(fGrid.Rows["start_date"].Cells[1].AuxValue);
                 var _drop_down = fGrid.Rows["start_date"].Cells[1].DropDownControl as fnn.DropDownCalenderX;
                 if (_drop_down != null)
                 {
                     _drop_down.selected_date = _prev_date;
                 }

             }
             foreach (var k in fGrid.Rows.Cast<iGRow>())
             {
                 k.Cells[1].Value = null;
                 k.Cells[1].AuxValue = null;
             }
           
             fGrid.Focus();
             buttoncreate.Enabled = true;
             buttoncancel.Enabled = true;
            
             m_SelectedDate = null;
            
             //fGrid.Rows["exp_account"].Cells[1].DropDownControl = null;
             //
           // wdata.TABLE_STAMP["accounts_current_balance_tb"];
             
             fGrid.SetCurCell("start_date", 1);
            
         }
         private void buttonX1_Click(object sender, EventArgs e)
         {
             fGrid.CommitEditCurCell();
             var _check = new string[] { "start_date","bank", "exp_account", "amount",};
             bool _roll_back = false;
             foreach (var k in _check)
             {
                 if (fGrid.Rows[k].Cells[1].Value == null)
                 {
                     MessageBox.Show("Important Field Left Blank",k);
                     fGrid.Focus();
                     fGrid.SetCurCell(k, 1);
                     return;
                 }
             }
             
             string _str = "Are You Sure You Want To Save This Record ??";
             if (!dbm.WarningMessage(_str, "Save Warning"))
             {
                 return;
             }
             buttoncreate.Enabled = false;
             buttoncancel.Enabled = false;
             fGrid.ReadOnly = true;
             ic.bank_reconc_transC _trans = new ic.bank_reconc_transC();
            ic.bank_reconc_accountC _account = (fGrid.Rows["exp_account"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag as ic.bank_reconc_accountC;
             ic.bankAccountC _bank = (fGrid.Rows["bank"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag as ic.bankAccountC;

             //
             _trans.amount = fGrid.Rows["amount"].Cells["desc"].Value.ToInt32();
             _trans.fs_date = System.Convert.ToDateTime(fGrid.Rows["start_date"].Cells[1].AuxValue);
             _trans.fs_id = fn.GetFSID(_trans.fs_date);
             _trans.status = em.voucher_statusS.valid;
             _trans.m_partition_id = string.Format("{0}{1}", _trans.fs_date.Year, _trans.fs_date.Month).ToInt32();
             _trans.br_acc_id = _account.br_acc_id;
             _trans.sys_account_id = _account.sys_account_id;
             _trans.bank_account_id = _bank.un_id;
            _trans.br_acc_type = _account.br_acc_type;
             //
             if (fGrid.Rows["desc"].Cells[1].Value != null)
             {
                 _trans.desc = fGrid.Rows["desc"].Cells[1].Text;
             }
                    
             using (var xd = new xing())
             {
                 #region Insert Master Payment
                 //
                 var _ts_id = accn.AccountsTransaction(xd, string.Format("Being Bank Reconciliation"), _trans.fs_date);
                _trans.trans_id = _ts_id;
                 _trans.un_id = xd.SingleInsertCommandTSPInt("acc_bank_reconc_trans_tb", new string[]
                 {
                    "br_acc_id",
                    "amount",
                    "transaction_id",
                    "fs_date",
                    "fs_id",
                    "m_partition_id",
                    "status",
                    "sys_account_id",
                    "description",
                    "br_acc_type_id",
                    "fs_time_stamp",
                    "lch_id",
                    "bank_account_id","pc_us_id"
                 }, new object[]
                 {
                     _trans.br_acc_id,
                     _trans.amount,
                     _trans.trans_id,
                     _trans.fs_date,
                     _trans.fs_id,
                     _trans.m_partition_id,
                     _trans.status.ToInt16(),
                     _trans.sys_account_id,
                     _trans.desc,
                     _trans.br_acc_type.ToInt16(),
                      0,
                    sdata.ChurchID,
                    _trans.bank_account_id,sdata.PC_US_ID
              });
                 #endregion
                 switch(_account.br_acc_type)
                {
                    case em.bank_reconc_typeS.addition:
                        {
                           // accn.JournalBook(xd, _trans.fs_date, em.j_sectionS.income, _ts_id, _account.sys_account_id, 0, 0);
                            break;
                        }
                    case em.bank_reconc_typeS.deduction:
                        {

                            break;
                        }
                }
                 xd.CommitTransaction();
             }
             fGrid.ReadOnly = false;
             ClearGrid();
            m_process = _process.after_save;
            backworker.RunWorkerAsync();
         }

         private void buttonItem2_Click(object sender, EventArgs e)
         {
             using (var _fm = new BankingMaker())
             {
                 _fm.ShowDialog();
             }
             ClearGrid();
         }

         private void buttonItem5_Click(object sender, EventArgs e)
         {
             using (var _fm = new BankWithDraw())
             {
                 _fm.ShowDialog();
             }
             ClearGrid();
         }

         private void checkBox1_CheckedChanged(object sender, EventArgs e)
         {

         }

         private void fGrid_Click(object sender, EventArgs e)
         {

         }
    }
}
