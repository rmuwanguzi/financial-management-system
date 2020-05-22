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
    public partial class MakeExpense : DevComponents.DotNetBar.Office2007Form
    {
        public MakeExpense()
        {
            InitializeComponent();
        }
        private iGDropDownList m_Deparments = null;
        private iGDropDownList m_Expenses = null;
        private enum _process
        {
            form_loading = 0,
            get_id = 1,
            update_after_insert,
            check_duplicate_voucher,
            cheeck_duplicate_cheque
        }
        _process m_process = _process.form_loading;
        string m_voucher_no = null;
        ic.expIncSrcTypeC m_SourceType = null;
        string m_cheque_no = null;
        long m_LastCreditorStamp = 0;
        DateTime? m_selected_date = null;
        private void ExpenseMaker_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
            CenterToScreen();
            InitIgridColumns();
            datam.ShowWaitForm();
            Application.DoEvents();
            datam.SecurityCheck();
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
                            datam.fill_accounts(xd);
                            datam.GetDepartments(xd);
                            datam.InitExpenses(xd);
                            datam.GetAccountsPayable(xd);
                            m_LastCreditorStamp = wdata.TABLE_STAMP["accounts_current_balance_tb"];
                            datam.GetBankAccounts(xd);
                            xd.CommitTransaction();
                        }
                        break;
                    }
                case _process.check_duplicate_voucher:
                    {
                        using (var xd = new xing())
                        {
                            if (accn.IsDuplicateVoucherNo(m_voucher_no, xd))
                            {
                                m_voucher_no = null;
                            }
                            xd.CommitTransaction();
                        }
                        break;
                    }
                case _process.cheeck_duplicate_cheque:
                    {
                        using (var xd = new xing())
                        {
                            var _bank_account = datam.DATA_BANK_ACCOUNTS.Values.Where(y => y.sys_account_id == m_SourceType.account_id).FirstOrDefault();
                            if (accn.IsDuplicateChequeNo(_bank_account, m_cheque_no, xd))
                            {
                                m_cheque_no = null;
                            }
                            xd.CommitTransaction();
                        }
                        break;
                    }
            }
           
        }
        private List<ic.expIncSrcTypeC> GetExpenseIncSourceTypes(DateTime _date)
        {
            List<ic.expIncSrcTypeC> _types = new List<ic.expIncSrcTypeC>();
            var b_list = accn.GetChildAccounts("BANK_ACCOUNT", em.account_typeS.ActualAccount).Select(k => k);
            int _amount = 0;
            int _fs_id = fn.GetFSID(_date);
            using (var xd = new xing())
            {
                foreach (var k in b_list)
                {
                    _amount = accn.GetFs_AccountBalance(xd, _fs_id, k.account_id);
                    if (_amount > 0)
                    {
                        _types.Add(new ic.expIncSrcTypeC()
                        {
                            source_type = em.exp_inc_src_typeS.bank,
                            account_id = k.account_id,
                            source_id=k.account_id,
                            balance = _amount,
                            name = k.account_name
                        });
                    }
                }
                //
                var _acc = accn.GetAccountByAlias("UNBANKED_CASH");
                if (_acc != null)
                {
                    _amount = accn.GetFs_AccountBalance(xd, _fs_id, _acc.account_id);
                    if (_amount > 0)
                    {
                        _types.Add(new ic.expIncSrcTypeC()
                        {
                            source_type = em.exp_inc_src_typeS.unbanked_cash,
                            account_id = _acc.account_id,
                            source_id=_acc.account_id,
                            balance = _amount,
                            name = "UnBanked Cash (SOURCE)"
                        });
                    }
                }
                //
                //_acc = accn.GetAccountByAlias("CASH_ACCOUNT");
                //if (_acc != null)
                //{
                //    _amount = accn.GetFS_Cash(xd, _fs_id);
                //    if (_amount > 0)
                //    {
                //        _types.Add(new ic.expIncSrcTypeC()
                //        {
                //            source_type = em.exp_inc_src_typeS.petty_cash,
                //            account_id = _acc.account_id,
                //            source_id = _acc.account_id,
                //            balance = _amount,
                //            name = "Petty Cash"
                //        });
                //    }
                //}
                var _list = accn.GetCurrentBankWithDraws(xd, _fs_id);
                foreach (var c in _list)
                {
                    _types.Add(new ic.expIncSrcTypeC()
                    {
                        source_type = em.exp_inc_src_typeS.petty_cash_cheque,
                        source_id = c.wdr_id,
                        account_id = c.sys_account_id,
                        //account_id = accn.GetAccountByAlias("CASH_ACCOUNT").account_id,
                        balance = c.cheque_balance,
                        name = string.Format("Cheque {0} {1}", c.cheque_no, string.IsNullOrEmpty(c.cheque_alias) ? string.Empty : string.Format("({0})", c.cheque_alias))

                    });
                }
                xd.CommitTransaction();
            }
            return _types;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                using (var _fm = new Expenses.ExpenseAccountMaker())
                {
                    _fm.ShowDialog();
                    if (_fm.Tag != null)
                    {
                        if (datam.DATA_EXPENSE_ACCOUNTS != null)
                        {
                            var nlist = from k in datam.DATA_EXPENSE_ACCOUNTS.Values
                                        where k.exp_acc_status==em.exp_acc_statusS.valid
                                        orderby k.exp_acc_name
                                        select k;
                                 m_Expenses.Items.Clear();
                                foreach (var k in nlist)
                                {
                                    #region load_expense_types
                                    switch (k.exp_acc_type)
                                    {
                                        case em.exp_acc_typeS.user_defined:
                                        case em.exp_acc_typeS.user_defined_shared:
                                            {
                                                m_Expenses.Items.Add(new fnn.iGComboItemEX()
                                                {
                                                    Tag = k,
                                                    Value = k.exp_acc_name,
                                                });
                                                break;
                                            }
                                        case em.exp_acc_typeS.system_department:
                                            {
                                                m_Expenses.Items.Add(new fnn.iGComboItemEX()
                                                {
                                                    Tag = k,
                                                    Value = k.exp_acc_name,
                                                    ForeColor = Color.Maroon
                                                });
                                                break;
                                            }
                                        case em.exp_acc_typeS.system_offertory_payment:
                                            {
                                                m_Expenses.Items.Add(new fnn.iGComboItemEX()
                                                {
                                                    Tag = k,
                                                    Value = string.Format("CR :: {0}", k.exp_acc_name),
                                                    ForeColor = Color.Blue
                                                });
                                                break;
                                            }
                                    } 
                                    #endregion
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
                case _process.update_after_insert:
                    {
                        wdata.TABLE_STAMP["accounts_current_balance_tb"] = m_LastCreditorStamp;
                        (this.Owner as DailyExpensesManager).CheckForUpdates();
                        break;
                    }
                case _process.check_duplicate_voucher:
                    {
                        if (string.IsNullOrEmpty(m_voucher_no))
                        {
                            MessageBox.Show("You Have Entered a Duplicate Voucher No", "Duplicate Voucher No");
                            fGrid.Focus();
                            fGrid.Rows["voucher"].Cells[1].Enabled = iGBool.True;
                            fGrid.Rows["voucher"].Cells[1].Value = null;
                            fGrid.SetCurCell("voucher", 1);
                            return;
                        }
                        fGrid.Rows["voucher"].Cells[1].Enabled = iGBool.True;
                        m_voucher_no = null;
                        break;
                    }
                case _process.cheeck_duplicate_cheque:
                    {
                        if (string.IsNullOrEmpty(m_cheque_no))
                        {
                            MessageBox.Show("You Have Already Used This Cheque No", "Duplicate Cheque No");
                            fGrid.Focus();
                            fGrid.Rows["cheque_no"].Cells[1].Enabled = iGBool.True;
                            fGrid.Rows["cheque_no"].Cells[1].Value = null;
                            fGrid.SetCurCell("cheque_no", 1);
                            return;
                        }
                        fGrid.Rows["cheque_no"].Cells[1].Enabled = iGBool.True;
                        m_voucher_no = null;
                        break;
                    }
            }
            
        }
        private void FilterCreditors(DateTime? filter_date)
        {
            var nlist = from k in m_Expenses.Items.Cast<fnn.iGComboItemEX>()
                        where k.ID > 0
                        select k;
            int _fs_id = fn.GetFSID(filter_date.Value);
            using (var xd = new xing())
            {
                foreach (var _v in nlist)
                {
                    _v.Visible = accn.GetFs_AccountBalance(xd, _fs_id, _v.ID) <= 0 ? false : true;
                }
                xd.CommitTransaction();
            }
            fGrid.Rows["exp_account"].Cells[1].Enabled = iGBool.True;
            fGrid.Rows["exp_account"].Cells[1].Value = null;
            fGrid.Rows["exp_account"].Cells[1].AuxValue = null;
        }
         private void LoadDropDownLists()
         {
             m_Deparments = fnn.CreateCombo();
             m_Expenses = fnn.CreateCombo();
                     
             //
             if (datam.DATA_EXPENSE_ACCOUNTS != null)
             {
                 var nlist = from k in datam.DATA_EXPENSE_ACCOUNTS.Values
                             where k.exp_acc_status == em.exp_acc_statusS.valid
                             select k;
                 foreach (var k in nlist)
                 {
                     switch (k.exp_acc_type)
                     {
                         case em.exp_acc_typeS.user_defined:
                         case em.exp_acc_typeS.user_defined_shared:
                             {
                                 m_Expenses.Items.Add(new fnn.iGComboItemEX()
                               {
                                   Tag = k,
                                   Value = k.exp_acc_name,

                               });
                                 break;
                             }
                         case em.exp_acc_typeS.system_department:
                             {
                                 m_Expenses.Items.Add(new fnn.iGComboItemEX()
                                 {
                                     Tag = k,
                                     Value = k.exp_acc_name,
                                     ForeColor = Color.Maroon
                                 });
                                 break;
                             }
                         //case em.exp_acc_typeS.system_offertory_payment:
                         //case em.exp_acc_typeS.trust_fund:
                         //    {
                         //        if (!accn.IsTrustFundExpense(k.cr_account_id))
                         //        {
                         //            m_Expenses.Items.Add(new fnn.iGComboItemEX()
                         //            {
                         //                Tag = k,
                         //                Value = string.Format(k.exp_acc_name),
                         //                ForeColor = Color.Blue,
                         //                ID = k.cr_account_id
                         //            });
                         //        }
                         //        break;
                         //    }
                     }
                 }
                 fGrid.Rows["exp_account"].Cells[1].DropDownControl = m_Expenses;
                // fGrid.Rows["exp_account"].Cells[1].Enabled = iGBool.False;
             }
            
            
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
             _row = CreateNewRow("Expense Date", "start_date", typeof(string), group_index, group_name);
             //
             _row.Cells["desc"].TypeFlags = iGCellTypeFlags.ComboPreferValue | iGCellTypeFlags.NoTextEdit | iGCellTypeFlags.HasEllipsisButton;
             _row.Cells["desc"].FormatString = "{0:D}";// {0:d} for short date string
             _row.Cells["desc"].DropDownControl = _date;
             _row.Cells["desc"].Value = null;
             //
             _row = CreateNewRow("Voucher No", "voucher", typeof(string), group_index, group_name);
             _row.Cells[1].ForeColor = Color.DeepPink;
            if (datam.DATA_SYSTEM_SETTINGS[em.SystemDefaultValuesS.show_expense_invoice_no] == 0)
            {
                _row.Visible = false;
            }
            //
            _row = fGrid.Rows.Add();
             _row.Height = 4;
             _row.BackColor = Color.Lavender;
             _row.Selectable = false;
             //
             _row = CreateNewRow("Source Of Income", "inc_source", typeof(string), group_index, group_name);
             var icombo = fnn.CreateCombo();
             //using (var xd = new xing())
             //{
             //    var _list = accn.GetCurrentBankWithDraws(xd);
             //    foreach (var k in _list)
             //    {
             //        icombo.Items.Add(new fnn.iGComboItemEX()
             //        {
             //            Value=k.cheque_no,
             //            Text=k.cheque_no,
             //            Tag=k
             //        });
             //    }
             //    xd.CommitTransaction();
             //}
             _row.Cells[1].DropDownControl = icombo;
             _row.Cells[1].Value = null;
             _row.Cells[1].Enabled = iGBool.False;
             //
             _row = CreateNewRow("Balance", "cheque_balance", typeof(int), group_index, group_name);
             _row.Cells[1].Enabled = iGBool.False;
             _row.Cells[1].FormatString = "{0:N0}";
             //

           
             //
             _row = CreateNewRow("Payment Mode", "pay_mode", typeof(string), group_index, group_name);
             icombo = fnn.CreateCombo();
             foreach (var k in new string[] { "Cheque", "Bank Transfer" })
             {
                 icombo.Items.Add(new fnn.iGComboItemEX()
                 {
                     Value = k
                 });
             }
             _row.Cells[1].DropDownControl = icombo;
             _row.Cells[1].Value = null;
             _row.Visible = false;
             //
             _row = CreateNewRow("Cheque No", "cheque_no", typeof(string), group_index, group_name);
             _row.Visible = false;
             _row = fGrid.Rows.Add();
             _row.Height = 5;
             _row.BackColor = Color.Lavender;
             _row.Selectable = false;
             _row.Cells[1].ForeColor = Color.Purple;
             //
            
           
             //
             _row = CreateNewRow("(+F1)  Account Name", "exp_account", typeof(string), group_index, group_name);
             _row = CreateNewRow("Department", "department", typeof(string), group_index, group_name);
             _row.Cells[1].Enabled = iGBool.False;
             //
             _row = CreateNewRow("Category", "category", typeof(string), group_index, group_name);
             _row.Cells[1].Enabled = iGBool.False;
             //
             _row = CreateNewRow("CR Balance", "cr_balance", typeof(int), group_index, group_name);
             _row.Cells[1].Enabled = iGBool.False;
             _row.Visible = false;
             _row.Cells[1].FormatString = "{0:N0}";
             //
             _row = fGrid.Rows.Add();
             _row.Height = 3;
             _row.BackColor = Color.Lavender;
             _row.Selectable = false;
             //
             _row = CreateNewRow("Paid Amount", "amount", typeof(decimal), group_index, group_name);
             _row.Cells[0].ForeColor = Color.Maroon;
             _row.Cells[1].FormatString = "{0:N0}";
             //
             _row = fGrid.Rows.Add();
             _row.Height = 3;
             _row.BackColor = Color.Lavender;
             _row.Selectable = false;
             _row = CreateNewRow("Received By", "received_by", typeof(string), group_index, group_name);
             //
             _row = CreateNewRow("Enter \n Particulars", "desc", typeof(string), group_index, group_name);
             _row.Height = 60;
             _row.Cells["desc"].TextFormatFlags = iGStringFormatFlags.WordWrap;
             _row.Cells["name"].TextAlign = iGContentAlignment.MiddleRight;
             //
             
             //
            
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
                     fGrid.Rows["department"].Cells[1].Value = null;
                     fGrid.Rows["category"].Cells[1].Value = null;
                     fGrid.Rows["cr_balance"].Cells[1].Value = null;
                     fGrid.Rows["cr_balance"].Visible = false;
                 }
                 if (fGrid.Rows[e.RowIndex].Key == "inc_source")
                 {
                     for (int y = e.RowIndex + 1; y < fGrid.Rows.Count; y++)
                     {
                         fGrid.Rows[y].Cells[1].Value = null;
                         fGrid.Rows[y].Cells[1].AuxValue = null;
                     }
                 }
                 return;
             }
             if (fGrid.Rows[e.RowIndex].Key == "voucher")
             {
                 if (fGrid.Rows[e.RowIndex].Cells[1].Value != null)
                 {
                     m_process = _process.check_duplicate_voucher;
                     m_voucher_no = fGrid.Rows[e.RowIndex].Cells[1].Text.Trim();
                     fGrid.Rows["voucher"].Cells[1].Enabled = iGBool.False;
                     backworker.RunWorkerAsync();
                 }
             }
             if (fGrid.Rows[e.RowIndex].Key == "start_date")
             {
                 var _date = System.Convert.ToDateTime(fGrid.Rows[e.RowIndex].Cells[1].AuxValue);
                 var _list = GetExpenseIncSourceTypes(_date);
                 m_selected_date = _date;
               //  FilterCreditors(m_selected_date);
                 var icombo = fGrid.Rows["inc_source"].Cells[1].DropDownControl as iGDropDownList;
                 fGrid.Rows["inc_source"].Cells[1].Value = null;
                 fGrid.Rows["inc_source"].Cells[1].AuxValue = null;
                 fGrid.Rows["inc_source"].Cells[1].Enabled = iGBool.False;
                 for (int y = e.RowIndex + 1; y < fGrid.Rows.Count; y++)
                 {
                     fGrid.Rows[y].Cells[1].Value = null;
                     fGrid.Rows[y].Cells[1].AuxValue = null;
                 }
                 fGrid.Rows["pay_mode"].Visible = false;
                 fGrid.Rows["cheque_no"].Visible = false;
                 if (icombo != null)
                 {
                     icombo.Items.Clear();
                     foreach (var k in _list)
                     {
                         icombo.Items.Add(new fnn.iGComboItemEX()
                         {
                             Tag = k,
                             Value = k.name
                         });
                     }
                     fGrid.Rows["inc_source"].Cells[1].Value = null;
                     fGrid.Rows["inc_source"].Cells[1].AuxValue = null;
                     fGrid.Rows["inc_source"].Cells[1].Enabled = iGBool.True;
                 }

                 if (fGrid.Rows[e.RowIndex].Key == "pay_mode")
                 {
                     fGrid.Rows["cheque_no"].Visible = false;
                     fGrid.Rows["cheque_no"].Cells[1].Value = null;
                     fGrid.Rows["cheque_no"].Cells[1].AuxValue = null;
                 }


             }
             if (fGrid.Rows[e.RowIndex].Key == "pay_mode")
             {
                 fGrid.Rows["cheque_no"].Cells[1].Value = null;
                 fGrid.Rows["cheque_no"].Cells[1].AuxValue = null;
                 if (fGrid.Rows[e.RowIndex].Cells[1].Text.ToLower() == "cheque")
                 {
                     fGrid.Rows["cheque_no"].Visible = true;
                 }
                 else
                 {
                     fGrid.Rows["cheque_no"].Visible = false;
                 }
             }
             if (fGrid.Rows[e.RowIndex].Key == "inc_source")
             {
                 var _source = (fGrid.Rows["inc_source"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag as ic.expIncSrcTypeC;
                 if (_source != null)
                 {
                     m_SourceType = _source;
                     fGrid.Rows["cheque_balance"].Cells[1].Value = _source.balance;
                     switch (_source.source_type)
                     {
                         case em.exp_inc_src_typeS.bank:
                             {
                                 fGrid.Rows["pay_mode"].Visible = true;
                                 fGrid.Rows["pay_mode"].Cells[1].Value = null;
                                 fGrid.Rows["pay_mode"].Cells[1].AuxValue = null;
                                 //
                                 fGrid.Rows["cheque_no"].Visible = false;
                                 fGrid.Rows["cheque_no"].Cells[1].Value = null;
                                 fGrid.Rows["cheque_no"].Cells[1].AuxValue = null;
                                 //
                                 break;
                             }
                         default:
                             {
                                 fGrid.Rows["pay_mode"].Visible = false;
                                 fGrid.Rows["pay_mode"].Cells[1].Value = null;
                                 fGrid.Rows["pay_mode"].Cells[1].AuxValue = null;
                                 //
                                 fGrid.Rows["cheque_no"].Visible = false;
                                 fGrid.Rows["cheque_no"].Cells[1].Value = null;
                                 fGrid.Rows["cheque_no"].Cells[1].AuxValue = null;
                                 break;
                             }
                     }
                 }
                 else
                 {
                     m_SourceType = null;
                     for (int y = e.RowIndex + 1; y < fGrid.Rows.Count; y++)
                     {
                         fGrid.Rows[y].Cells[1].Value = null;
                         fGrid.Rows[y].Cells[1].AuxValue = null;
                     }
                     fGrid.Rows["pay_mode"].Visible = false;
                     fGrid.Rows["cheque_no"].Visible = false;
                 }
             }
            
             if (fGrid.Rows[e.RowIndex].Key == "amount")
             {
                 if (fGrid.Rows["cheque_balance"].Cells[1].Value.ToInt32() < fGrid.Rows["amount"].Cells[1].Value.ToInt32())
                 {
                     MessageBox.Show("You Have Entered An Amount Which Is Higher Than The Cheque Balance", "Invalid Value");
                     fGrid.Focus();
                     fGrid.Rows["amount"].Cells[1].Value = null;
                     fGrid.Rows["amount"].Cells[1].Selected = true;
                     return;
                 }
                 if (fGrid.Rows["cr_balance"].Visible & (fGrid.Rows["amount"].Cells[1].Value.ToInt32() > fGrid.Rows["cr_balance"].Cells[1].Value.ToInt32()))
                 {
                     MessageBox.Show("You Have Entered An Amount Which Is Higher Than The CR Balance", "Invalid Value");
                     fGrid.Focus();
                     fGrid.Rows["amount"].Cells[1].Value = null;
                     fGrid.Rows["amount"].Cells[1].Selected = true;
                     return;
                 }
             }
             if (fGrid.Rows[e.RowIndex].Key == "cheque_no")
             {
                 if (fGrid.Rows[e.RowIndex].Cells[1].Value != null)
                 {
                     m_process = _process.cheeck_duplicate_cheque;
                     m_cheque_no = fGrid.Rows[e.RowIndex].Cells[1].Text.Trim();
                     fGrid.Rows["cheque_no"].Cells[1].Enabled = iGBool.False;
                     backworker.RunWorkerAsync();
                 }
             }
             if (fGrid.Rows[e.RowIndex].Key == "exp_account")
             {
                 using (var xd = new xing())
                 {
                     var _exp = (fGrid.Rows[e.RowIndex].Cells[1].AuxValue as fnn.iGComboItemEX).Tag as ic.expense_accountC;
                     if (_exp != null)
                     {
                         fGrid.Rows["department"].Cells[1].Value = datam.DATA_DEPARTMENT[_exp.dept_id].dept_name;
                         fGrid.Rows["category"].Cells[1].Value = _exp.exp_cat_id == 0 ? string.Empty : datam.DATA_EXPENSE_CATEGORY[_exp.exp_cat_id].exp_cat_name;
                         if (_exp.exp_acc_type == em.exp_acc_typeS.system_offertory_payment)
                         {
                             fGrid.Rows["cr_balance"].Cells[1].Value = accn.GetFs_AccountBalance(xd, fn.GetFSID(m_selected_date.Value), _exp.cr_account_id);// datam.DATA_ACCOUNTS_PAYABLE.Values.Where(p => p.account_id == _exp.cr_account_id).Select(b => b.balance).Sum();
                             fGrid.Rows["cr_balance"].Visible = true;
                         }
                         else
                         {
                             fGrid.Rows["cr_balance"].Cells[1].Value = null;
                             fGrid.Rows["cr_balance"].Visible = false;
                             if (_exp.exp_acc_type == em.exp_acc_typeS.trust_fund)
                             {
                                 var _trust_fund_account = accn.GetAccountByAlias("CUC_PAYABLE").account_id;
                                 fGrid.Rows["cr_balance"].Cells[1].Value = accn.GetFs_AccountBalance(xd, fn.GetFSID(m_selected_date.Value), _trust_fund_account);  //datam.DATA_ACCOUNTS_PAYABLE.Values.Where(p => p.account_id == _trust_fund_account).Select(b => b.balance).Sum();
                                 fGrid.Rows["cr_balance"].Visible = true;
                             }
                         }
                     }
                     else
                     {
                         fGrid.Rows["department"].Cells[1].Value = null;
                         fGrid.Rows["category"].Cells[1].Value = null;
                         fGrid.Rows["cr_balance"].Cells[1].Value = null;
                         fGrid.Rows["cr_balance"].Visible = false;
                     }
                     xd.CommitTransaction();
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
             m_selected_date = null;
             fGrid.Focus();
             buttoncreate.Enabled = true;
             buttoncancel.Enabled = true;
             fGrid.Rows["pay_mode"].Visible = false;
             fGrid.Rows["cheque_no"].Visible = false;
             //
             fGrid.Rows["cr_balance"].Visible = false;
             //
           // wdata.TABLE_STAMP["accounts_current_balance_tb"];
             fGrid.Rows["exp_account"].Cells[1].Enabled = iGBool.False;
             fGrid.SetCurCell("start_date", 1);
            
         }
         private void buttonX1_Click(object sender, EventArgs e)
         {
             fGrid.CommitEditCurCell();
             var _check = new string[] { "start_date","inc_source", "voucher", "exp_account", "amount",};
             bool _roll_back = false;
             foreach (var k in _check)
             {
                if (k == "voucher")
                {
                    if (datam.DATA_SYSTEM_SETTINGS[em.SystemDefaultValuesS.show_expense_invoice_no] == 0)
                    {
                        continue;
                    }
                }
                if (fGrid.Rows[k].Cells[1].Value == null)
                 {
                     MessageBox.Show("Important Field Left Blank",k);
                     fGrid.Focus();
                     fGrid.SetCurCell(k, 1);
                     return;
                 }
             }
             if (fGrid.Rows["pay_mode"].Visible & fGrid.Rows["pay_mode"].Cells[1].Value == null)
             {
                 MessageBox.Show("Important Field Left Blank", "Payment Mode");
                 fGrid.Focus();
                 fGrid.SetCurCell("pay_mode", 1);
                 return;
             }
             if (fGrid.Rows["cheque_no"].Visible & fGrid.Rows["cheque_no"].Cells[1].Value == null)
             {
                 MessageBox.Show("Important Field Left Blank", "Cheque No");
                 fGrid.Focus();
                 fGrid.SetCurCell("cheque_no", 1);
                 return;
             }
             string _str = "Are You Sure You Want To Save This Expense ??";
             if (!dbm.WarningMessage(_str, "Save Warning"))
             {
                 return;
             }
             buttoncreate.Enabled = false;
             buttoncancel.Enabled = false;
             fGrid.ReadOnly = true;
             ic.expense_transC _exp_item = new ic.expense_transC();
             ic.expense_accountC _exp_account = null;
             //
             _exp_item.exp_amount = fGrid.Rows["amount"].Cells["desc"].Value.ToInt32();
            if (fGrid.Rows["voucher"].Cells[1].Value != null)
            {
                _exp_item.voucher_no = fGrid.Rows["voucher"].Cells[1].Text.Trim();
            }
             if (fGrid.Rows["start_date"].Cells[1].AuxValue != null)
             {
                 _exp_item.exp_date = System.Convert.ToDateTime(fGrid.Rows["start_date"].Cells[1].AuxValue);
                 _exp_item.exp_fs_id = fn.GetFSID(_exp_item.exp_date.Value);
             }
             else
             {
                 _exp_item.exp_date = sdata.CURR_DATE;
                 _exp_item.exp_fs_id = sdata.CURR_FS.fs_id;
             }
             _exp_item.voucher_status = em.voucher_statusS.valid;
             _exp_account = (fGrid.Rows["exp_account"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag as ic.expense_accountC;
             //
             _exp_item.exp_cat_id = _exp_account.exp_cat_id;
              _exp_item.dept_id = _exp_account.dept_id;
              _exp_item.dept_parent_id = _exp_account.dept_parent_id;
              _exp_item.objExpenseAccount = _exp_account;
             //
             _exp_item.exp_acc_id = _exp_account.exp_acc_id;
             if (fGrid.Rows["desc"].Cells[1].Value != null)
             {
                 _exp_item.exp_details = fGrid.Rows["desc"].Cells[1].Text;
             }
             if (fGrid.Rows["cheque_no"].Cells[1].Value != null)
             {
                 _exp_item.cheque_no = fGrid.Rows["cheque_no"].Cells[1].Text;
             }
             //
             _exp_item.received_by = fGrid.Rows["received_by"].Cells[1].Text;
             //
             if (!string.IsNullOrEmpty(_exp_item.received_by))
             {
                 _exp_item.received_by = _exp_item.received_by.ToProperCase();
             }
             //
             var _source = (fGrid.Rows["inc_source"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag as ic.expIncSrcTypeC;
             _exp_item.source_account_id = _source.account_id;
             _exp_item.source_id = _source.source_id;
             _exp_item.source_type = _source.source_type;
             switch (_source.source_type)
             {
                 case em.exp_inc_src_typeS.bank:
                     {
                         _exp_item.pay_mode = fGrid.Rows["pay_mode"].Cells[1].Text.ToLower() == "cheque" ? em.voucher_Paymode.cheque : em.voucher_Paymode.bank_transfer;
                         break;
                     }
                 default:
                     {
                         _exp_item.pay_mode = em.voucher_Paymode.cash;
                         break;
                     }
             }
             using (var xd = new xing())
             {
                 xd.UpdateFsTimeStamp("acc_expense_trans_tb");
                 #region Insert Master Payment
                 //
                 _exp_item.sys_account_id = _exp_account.sys_account_id;
                 var _ts_id = accn.AccountsTransaction(xd, string.Format("Being Payment for {0}", datam.DATA_ACCOUNTS[_exp_item.sys_account_id].account_name), _exp_item.exp_date.Value);
                 //
                 string _wdr_data = null;
                  #region withdraws
                 if (_exp_item.source_type == em.exp_inc_src_typeS.petty_cash_cheque)
                 {
                     xd.UpdateFsTimeStamp("acc_bank_withdraw_tb");
                     string _st = string.Format("update acc_bank_withdraw_tb set cheque_balance=(cheque_balance-{0}),{1},fs_time_stamp={2} where wdr_id={3} and cheque_balance={4}", _exp_item.exp_amount,
                         dbm.ETS, SQLH.UnixStamp, _exp_item.source_id, _source.balance);
                    if(! xd.SingleUpdateCommand(_st))
                    {
                        _roll_back = true;
                        MessageBox.Show("You Need To ReEnter This Entry To Prevent Double Expense Entry", "Multi User Double Entry Error");
                        xd.RollBackTransaction();
                        return;
                    }
                     _wdr_data  = string.Format("{0}:{1}:{2}", _exp_item.source_id, _exp_item.exp_amount,_source.balance);
                   
                 }
                 if (_exp_item.source_type == em.exp_inc_src_typeS.petty_cash)
                 {
                     var _wlist = accn.GetCurrentBankWithDraws(xd, _exp_item.exp_fs_id);
                     int _exp_amt = _exp_item.exp_amount;
                     var _cb_amount = 0;
                     int _amount = 0;
                     string _st = null;
                     xd.UpdateFsTimeStamp("acc_bank_withdraw_tb");
                     foreach (var k in _wlist.OrderBy(r => r.wdr_id))
                     {
                         _cb_amount = k.cheque_balance;
                         if (_exp_amt > _cb_amount)
                         {
                             _amount = _cb_amount;
                         }
                         if (_exp_amt < _cb_amount)
                         {
                             _amount = _exp_amt;
                         }
                         if (_exp_amt == _cb_amount)
                         {
                             _amount = _cb_amount;
                         }
                         _st = string.Format("update acc_bank_withdraw_tb set cheque_balance=(cheque_balance-{0}),{1},fs_time_stamp={2} where wdr_id={3} and cheque_balance={4}", _amount,
                             dbm.ETS, SQLH.UnixStamp, k.wdr_id, k.cheque_balance);
                         if (xd.SingleUpdateCommand(_st))
                         {
                             if (string.IsNullOrEmpty(_wdr_data))
                             {
                                 _wdr_data = string.Format("{0}:{1}:{2}", k.wdr_id, _amount,k.cheque_no);
                             }
                             else
                             {
                                 _wdr_data += string.Format(",{0}:{1}:{2}", k.wdr_id, _amount, k.cheque_no);
                             }
                         }
                         else
                         {
                            
                         }
                         _exp_amt -= _amount;
                         _amount = 0;
                     }
                 }
                 #endregion

                 _exp_item.un_id = xd.SingleInsertCommandTSPInt("acc_expense_trans_tb", new string[]
                 {
                    "exp_acc_id",
                    "voucher_id",
                    "voucher_no",
                    "voucher_status",
                    "exp_date",
                    "exp_fs_id",
                    "exp_amount",
                    "exp_details",
                    "exp_type",
                    "fs_time_stamp",
                    "fs_date",
                    "fs_id",
                    "m_partition_id",
                    "app_station_id",
                    "pc_us_id","transaction_id","dept_id","dept_parent_id","exp_cat_id",
                    "pay_mode","cheque_no","sys_account_id","source_account_id","source_type_id","source_balance","w_dr_data","source_id","received_by"
                 }, new object[]
                 {
                     _exp_account.exp_acc_id,
                     _exp_item.voucher_id,
                     _exp_item.voucher_no,
                     _exp_item.voucher_status.ToByte(),
                     _exp_item.exp_date,
                     _exp_item.exp_fs_id,
                     _exp_item.exp_amount,
                     _exp_item.exp_details,
                     33,
                     0,
                     sdata.CURR_DATE,
                     sdata.CURR_FS.fs_id,
                     string.Format("{0}{1}",_exp_item.exp_date.Value.Year,_exp_item.exp_date.Value.Month).ToInt32(),
                     sdata.App_station_id,
                     sdata.PC_US_ID,_ts_id,_exp_item.dept_id,_exp_item.dept_parent_id,
                     _exp_item.exp_cat_id,_exp_item.pay_mode.ToByte(),_exp_item.cheque_no,_exp_account.sys_account_id,
                     _exp_item.source_account_id,_exp_item.source_type.ToByte(),_source.balance,_wdr_data,_source.source_id,_exp_item.received_by
              });
                 #endregion
                 if (!string.IsNullOrEmpty(_exp_item.cheque_no))
                 {
                     ic.bankAccountC _bank = datam.DATA_BANK_ACCOUNTS.Values.Where(y => y.sys_account_id == m_SourceType.account_id).FirstOrDefault();
                     xd.SingleInsertCommandTSPInt("acc_usedbank_cheques_tb", new string[]
                {
                    "bank_account_id",
                    "cheque_no",
                    "fs_time_stamp",
                    "lch_id",
                    "bank_id",
                }, new object[]
                {
                    _bank.un_id,
                    _exp_item.cheque_no,
                    0,
                    sdata.ChurchID,
                    _bank.bank_id
                });
                 }
                 switch (_exp_item.pay_mode)
                 {
                     case em.voucher_Paymode.cash:
                         {
                             accn.JournalBook(xd, _exp_item.exp_date.Value, em.j_sectionS.cash, _ts_id, _source.account_id, 0, _exp_item.exp_amount);
                             break;
                         }
                     case em.voucher_Paymode.bank_transfer:
                     case em.voucher_Paymode.cheque:
                         {
                             accn.JournalBook(xd, _exp_item.exp_date.Value, em.j_sectionS.bank, _ts_id, _source.account_id, 0, _exp_item.exp_amount);
                             break;
                         }
                 }
                 switch (_exp_account.exp_acc_type)
                 {
                     case em.exp_acc_typeS.system_offertory_payment:
                         {
                             accn.JournalBook(xd, _exp_item.exp_date.Value, em.j_sectionS.creditor, _ts_id, _exp_account.cr_account_id, _exp_item.exp_amount, 0);

                             //var _balance = accn.GetFs_AccountBalance(xd, _exp_item.exp_fs_id, _exp_account.cr_account_id);
                             //if (_balance >= _exp_item.exp_amount)
                             //{
                             //    accn.JournalBook(xd, _exp_item.exp_date.Value, em.j_sectionS.creditor, _ts_id, _exp_account.cr_account_id, _exp_item.exp_amount, 0);
                             //}
                             //else
                             //{
                             //    accn.JournalBook(xd, _exp_item.exp_date.Value, em.j_sectionS.creditor, _ts_id, _exp_account.cr_account_id, _balance, 0);
                             //    _balance = (_exp_item.exp_amount - _balance);
                             //    //now this is a church expense
                             //  //  accn.JournalBook(xd, _exp_item.exp_date.Value, em.j_sectionS.expense, _ts_id, _exp_account.sys_account_id, _balance, 0);

                             //}
                             break;
                         }
                     case em.exp_acc_typeS.trust_fund:
                         {
                             datam.GetAccountsPayable(xd);
                             var _cr_accounts = accn.GetChildAccounts("CUC_PAYABLE", em.account_typeS.ActualAccount);
                             var _cr_ids = _cr_accounts.Select(p => p.account_id).ToList();
                             var nlist = datam.DATA_ACCOUNTS_PAYABLE.Values.Where(p => _cr_ids.IndexOf(p.account_id) > -1).OrderByDescending(p => p.balance).Select(s => s);
                             int WORK_VALUE = _exp_item.exp_amount;
                             int paid_amount = 0;
                             string TF_STRING = null;
                             foreach (var k in nlist)
                             {
                                 paid_amount = 0;
                                 if (WORK_VALUE > 0)
                                 {
                                     if (k.balance >= WORK_VALUE)
                                     {
                                         paid_amount = WORK_VALUE;
                                         WORK_VALUE = 0;
                                     }
                                     else
                                     {
                                         paid_amount = k.balance;
                                         WORK_VALUE -= paid_amount;
                                     }
                                     accn.JournalBook(xd, _exp_item.exp_date.Value, em.j_sectionS.creditor, _ts_id, k.account_id, paid_amount, 0);
                                     if (string.IsNullOrEmpty(TF_STRING))
                                     {
                                         TF_STRING = string.Format("{0}:{1}", paid_amount, k.account_id);
                                     }
                                     else
                                     {
                                         TF_STRING += string.Format(",{0}:{1}", paid_amount, k.account_id);
                                     }
                                     xd.SingleInsertCommandIgnore("acc_expense_trans_child_tb", new string[]
                                               {
                                                   "trans_id",
                                                   "voucher_id",
                                                   "voucher_status",
                                                   "fs_time_stamp",
                                                   "app_station_id",
                                                   "transaction_id",
                                                   "exp_amount",
                                                   "cr_account_id",
                                                   "exp_date"
                                                 
                                               }, new object[]
                                               {
                                                   _exp_item.un_id,
                                                   _exp_item.voucher_id,
                                                   _exp_item.voucher_status.ToInt16(),
                                                   0,
                                                   datam.LCH_ID,
                                                   _exp_item.transaction_id,
                                                   paid_amount,
                                                   k.account_id,
                                                   _exp_item.exp_date
                                                                                               
                                               });
                                 }
                                 else
                                 {
                                     break;
                                 }
                             }
                             if (!string.IsNullOrEmpty(TF_STRING))
                             {
                                 xd.SingleUpdateCommandALL("acc_expense_trans_tb", new string[]
                                     {
                                      "tf_string",
                                      "un_id"
                                     }, new object[]
                                     {
                                        TF_STRING,
                                        _exp_item.un_id
                                     }, 1);
                             }
                             break;
                         }
                     default:
                         {
                             accn.JournalBook(xd, _exp_item.exp_date.Value, em.j_sectionS.expense, _ts_id, _exp_account.sys_account_id, _exp_item.exp_amount, 0);
                             break;
                         }
                 }
                //
                if (string.IsNullOrEmpty(_exp_item.voucher_no))
                {
                    _exp_item.voucher_no = "EXP_" + (_exp_item.un_id * -1).ToStringNullable();
                }

                _exp_item.voucher_id = xd.SingleInsertCommandTSPInt("acc_expense_vouchers_tb",
                     new string[]
                     {
                         "voucher_no",
                         "status",
                         "transaction_id",
                         "lch_id","fs_time_stamp"
                      },
                     new object[]
                     {
                         _exp_item.voucher_no,
                         _exp_item.voucher_status,
                         _ts_id,
                         sdata.ChurchID,0
                     });
                    xd.SingleUpdateCommandALL("acc_expense_trans_tb",
                        new string[]
                        {
                         "voucher_id",
                         "un_id"
                        }, new object[] { _exp_item.voucher_id, _exp_item.un_id }, 1);
                    //
                

                 xd.CommitTransaction();
             }
             fGrid.ReadOnly = false;
             m_process = _process.update_after_insert;
             backworker.RunWorkerAsync();
             ClearGrid();
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
    }
}
