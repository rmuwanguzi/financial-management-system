using MTOMS.ic;
using SdaHelperManager;
using SdaHelperManager.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

namespace MTOMS.Bank_Reconciliation
{
    public partial class MergeAccountsMaker : DevComponents.DotNetBar.Office2007Form
    {
        public MergeAccountsMaker()
        {
            InitializeComponent();
        }
        ic.accountC m_group_account { get; set; }
        private void MergeAccountsMaker_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            m_group_account = this.Tag as ic.accountC;
            if (m_group_account == null)
            {
                return;
                this.Close();
            }
            backworker.RunWorkerAsync();
        }

        private void backworker_DoWork(object sender, DoWorkEventArgs e)
        {
            datam.SystemInitializer();
            using(var xd= new xing())
            {
                datam.InitAccount(xd);
                datam.InitExpenses(xd);
            }
        }

        private void backworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            InitIgridColumns();
            LoadMainGrid();
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
            _row = CreateNewRow("Merge From", "account_from", typeof(string), group_index, group_name);
            var nlist = accn.GetChildAccounts(m_group_account.account_id, em.account_typeS.ActualAccount);

            var icombo = fnn.CreateCombo();
            int _balance = 0;
            int _opening_balance = 0;
            if (nlist != null)
            {
                using (var xd = new xing())
                {
                    foreach (var k in nlist)
                    {
                        if (k.account_status == em.account_statusS.DeActivated)
                        {
                            continue;
                        }
                        _balance = accn.GetFs_AccountBalance(xd, sdata.CURR_FS.fs_id, k.account_id);
                        if (_balance < 0)
                        {
                            continue;
                        }
                        _opening_balance = fnn.GetAccountOpeningBalance(xd, k.account_id);
                        if (_opening_balance > 0)
                        {
                            continue;
                        }
                        icombo.Items.Add(new fnn.iGComboItemEX()
                            {
                                Value = k.account_name,
                                Tag = k
                            });
                    }
                }
                _row.Cells[1].DropDownControl = icombo;

            }
            _row.Cells[1].Value = null;
            //
            _row = CreateNewRow("Merge To", "account_to", typeof(string), group_index, group_name);
            _row.Cells[1].Enabled = iGBool.False;
            
            //
            _row = fGrid.Rows.Add();
            _row.Height = 4;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            //
           
            

            fGrid.EndUpdate();
        }
        private iGRow CreateNewRow(string field, string rowkey, Type _type, int group_index, string group_name)
        {
            var _row = fGrid.Rows.Add();
            _row.Font = new Font("georgia", 14, FontStyle.Regular);
            _row.Cells["desc"].Font = new Font("arial", 14, FontStyle.Regular);
            _row.Cells["name"].Col.Width = 180;
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
        private bool IsValid()
        {

            var _comp = new string[] { "account_from", "account_to", };
            foreach (var k in _comp)
            {
                if (fGrid.Rows[k].Cells[1].Value == null)
                {
                    MessageBox.Show("Important Field Left Blank", "Save Error");
                    fGrid.Focus();
                    fGrid.SetCurCell(k, 1);
                    return false;
                }
            }
            return true;
        }
        private void buttoncreate_Click(object sender, EventArgs e)
        {
            if (fGrid.CurCell != null)
            {
                fGrid.CommitEditCurCell();
            }
            if (!IsValid())
            {
                return;
            }
            string _str = "Are You Sure You Want To Perform This Operation ??";
            if (!dbm.WarningMessage(_str, "Save Warning"))
            {
                return;
            }
            ic.accountC _from = (fGrid.Rows["account_from"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag as ic.accountC;
            ic.accountC _to = (fGrid.Rows["account_to"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag as ic.accountC;
            //
            using (var xd = new xing())
            {
                string[] _cols = new string[]
                    {
                        "source_type_id",
                        "source_id",
                        "transaction_id",
                        "destination_type_id",
                        "destination_id",
                        "amount",
                        "fs_date",
                        "fs_id",
                        "fs_time_stamp",
                        "lch_id"
                    };
                var _ts_id = accn.AccountsTransaction(xd, string.Format("Being Transfer Of Account Balance for {0} To {1}", _from.account_name, _to.account_name), sdata.CURR_DATE);
                var _amount = accn.GetFs_AccountBalance(xd, sdata.CURR_FS.fs_id, _from.account_id);
                var _ret_id = xd.SingleInsertCommandTSPInt("acc_cash_transfer_tb", _cols, new object[]
                        {
                            em.CashTransferSourceType.account,
                            _from.account_id,
                            _ts_id,
                            em.CashTransferDestinationType.account,
                            _to.account_id,
                            _amount,
                            sdata.CURR_DATE,
                            sdata.CURR_FS.fs_id,
                            0,
                            datam.LCH_ID
                        });

                //
                switch (m_group_account.account_dept_category)
                {
                    case em.account_d_categoryS.Liablity:
                        {
                            accn.JournalBook(xd, sdata.CURR_DATE, em.j_sectionS.creditor, _ts_id, _from.account_id, _amount, 0);
                            accn.JournalBook(xd, sdata.CURR_DATE, em.j_sectionS.creditor, _ts_id, _to.account_id, 0, _amount);
                            //
                            _from.account_status = em.account_statusS.DeActivated;
                            datam.LoadCrExpOffItems(xd);
                            var to_exp_link_obj = datam.DATA_CR_EXP_OFFITEMS.Values.Where(k => k.gen_account_id == _to.account_id & k._type == em.link_accTypes.creditor).FirstOrDefault();
                            var to_exp_sys_account_id = 0;
                            ic.expense_accountC to_expense_account = null;
                            if (to_exp_link_obj != null)
                            {
                                to_exp_sys_account_id = datam.DATA_CR_EXP_OFFITEMS.Values.Where(k => k.link_id == to_exp_link_obj.link_id & k._type == em.link_accTypes.expense_accrued).FirstOrDefault().gen_account_id;
                                to_expense_account = datam.DATA_EXPENSE_ACCOUNTS.Values.Where(p => p.sys_account_id == to_exp_sys_account_id).FirstOrDefault();
                            }
                            #region From
                            var from_cr_inc_item = datam.DATA_CR_EXP_OFFITEMS.Values.Where(k => k.gen_account_id == _from.account_id & k._type == em.link_accTypes.creditor).FirstOrDefault();
                            if (from_cr_inc_item != null && from_cr_inc_item.cg_id == 0)
                            {
                                xd.SingleUpdateCommandALL("accounts_tb", new string[]
                                  {
                                      "account_status_id",
                                      "account_id"
                                  }, new object[]
                                  {
                                  em.account_statusS.DeActivated.ToInt16(),
                                  from_cr_inc_item.account_id
                                  
                                  }, 1);
                                //
                                xd.SingleUpdateCommandALL("accounts_tb", new string[]
                                  {
                                      "account_status_id",
                                      "account_id"
                                  }, new object[]
                                  {
                                  em.account_statusS.DeActivated.ToInt16(),
                                  _from.account_id
                                  
                                  }, 1);
                                //
                                var _exp_setting = datam.DATA_CR_EXP_OFFITEMS.Values.Where(k => k.link_id == from_cr_inc_item.link_id & k._type == em.link_accTypes.expense_accrued).FirstOrDefault();
                                if (_exp_setting != null)
                                {
                                    ic.expense_accountC from_exp_account = (from k in datam.DATA_EXPENSE_ACCOUNTS.Values
                                                                    where k.sys_account_id == _exp_setting.gen_account_id
                                                                    select k).FirstOrDefault();
                                    if (from_exp_account != null)
                                    {
                                        if (xd.ExecuteScalarInt(string.Format("select count(un_id) as cnt from acc_expense_trans_tb where exp_acc_id={0}", from_exp_account.exp_acc_id)) > 0)
                                        {
                                            if (to_expense_account != null)
                                            {
                                                string _upd_str = string.Format("update acc_expense_trans_tb set exp_cat_id={0},dept_id={1},dept_parent_id={2},exp_acc_id={3},sys_account_id={4},{5},fs_time_stamp={6} where exp_acc_id={7}",
                                                    to_expense_account.exp_cat_id, to_expense_account.dept_id, to_expense_account.dept_parent_id, to_expense_account.exp_acc_id, to_expense_account.sys_account_id, dbm.ETS, SQLH.UnixStamp, from_exp_account.exp_acc_id);
                                                xd.SingleUpdateCommand(_upd_str);
                                            }
                                        }
                                        from_exp_account.exp_acc_status = em.exp_acc_statusS.invalid;
                                        xd.SingleUpdateCommandALL("acc_expense_accounts_tb", new string[]
                                            {
                                                "exp_acc_status",
                                                "exp_acc_id"
                                            }, new object[]
                                            {
                                                from_exp_account.exp_acc_status.ToInt16(),
                                                from_exp_account.exp_acc_id
                                            }, 1);
                                    }
                                }
                            }
                            #endregion
                            //
                            break;

                        }
                }
                xd.CommitTransaction();
            }
            
            ClearGrid();
            this.Close();
                
        }
        private void ClearGrid()
        {
            foreach (var k in fGrid.Rows.Cast<iGRow>())
            {
                k.Cells[1].Value = null;
                k.Cells[1].AuxValue = null;
            }
            fGrid.Focus();
            fGrid.SetCurCell("account_from", 1);

        }
        private void buttoncancel_Click(object sender, EventArgs e)
        {
            ClearGrid();
        }

        private void fGrid_Click(object sender, EventArgs e)
        {

        }

        private void fGrid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
        {
             if (fGrid.Rows[e.RowIndex].Cells["desc"].DropDownControl != null && fGrid.Rows[e.RowIndex].Cells["desc"].AuxValue == null)
            {
                fGrid.Rows[e.RowIndex].Cells["desc"].Value = null;
                if (fGrid.Rows[e.RowIndex].Key == "account_from")
                {
                    fGrid.Rows["account_to"].Cells[1].Value = null;
                    fGrid.Rows["account_to"].Cells[1].AuxValue = null;
                    fGrid.Rows["account_to"].Cells[1].Enabled = iGBool.False;
                }
                return;
            }
             if (fGrid.Rows[e.RowIndex].Key == "account_from")
             {
                 fGrid.Rows["account_to"].Cells[1].Value = null;
                 fGrid.Rows["account_to"].Cells[1].AuxValue = null;
                 fGrid.Rows["account_to"].Cells[1].Enabled = iGBool.False;
                 var icombo = fGrid.Rows["account_to"].Cells[1].DropDownControl as iGDropDownList;
                 if (icombo == null)
                 {
                     icombo = fnn.CreateCombo();
                     fGrid.Rows["account_to"].Cells[1].DropDownControl = icombo;
                 }
                 ic.accountC _from = (fGrid.Rows["account_from"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag as ic.accountC;
                 icombo.Items.Clear();
                 var nlist = accn.GetChildAccounts(m_group_account.account_id, em.account_typeS.ActualAccount);

                 if (nlist != null)
                 {
                     foreach (var k in nlist)
                     {
                         if (k.account_status == em.account_statusS.DeActivated)
                         {
                             continue;
                         }
                         if (k.account_id == _from.account_id)
                         {
                             continue;
                         }
                         icombo.Items.Add(new fnn.iGComboItemEX()
                             {
                                 Value = k.account_name,
                                 Tag = k
                             });
                     }
                 }
                 fGrid.Rows["account_to"].Cells[1].Enabled = iGBool.True;
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
                     if (fGrid.Rows[k].Cells[1].ReadOnly == iGBool.True) { continue; }
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
    }
}
