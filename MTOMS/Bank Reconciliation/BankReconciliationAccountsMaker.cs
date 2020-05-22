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
    public partial class BankReconciliationAccountsMaker : DevComponents.DotNetBar.Office2007Form
    {
        public BankReconciliationAccountsMaker()
        {
            InitializeComponent();
        }

        private void BankReconciliationAccountsMaker_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            this.Tag = null;
            backworker.RunWorkerAsync();
        }

        private void backworker_DoWork(object sender, DoWorkEventArgs e)
        {
            datam.SystemInitializer();
            using(var xd= new xing())
            {
                datam.InitBankReconciliation(xd);
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
           
            _row = CreateNewRow("Account Name", "account_name", typeof(string), group_index, group_name);
            
            //
            _row = CreateNewRow("Reconciliation Type", "account_type", typeof(string), group_index, group_name);
            var icombo = fnn.CreateCombo();
            foreach(var t in datam.DATA_ENUM_BANK_RECONCILIATION_TYPES)
            {
                icombo.Items.Add(new fnn.iGComboItemEX()
                    {
                        Value = t.Value,
                        ID = t.Key.ToInt16()
                    });
            }
            _row.Cells["desc"].DropDownControl = icombo;
            _row.Cells["desc"].Value = null;
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

            var _comp = new string[] { "account_name", "account_type", };
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
            string _str = "Are You Sure You Want To Create This Account ??";
            if (!dbm.WarningMessage(_str, "Save Warning"))
            {
                return;
            }
            ic.bank_reconc_accountC _acc = new ic.bank_reconc_accountC();
            _acc.br_acc_name = fGrid.Rows["account_name"].Cells[1].Text.ToProperCase();
            _acc.br_acc_type = (em.bank_reconc_typeS)(fGrid.Rows["account_type"].Cells[1].AuxValue as fnn.iGComboItemEX).ID;
            ic.accountC _sys_account = null;
            using (var xd = new xing())
            {
                switch (_acc.br_acc_type)
                {
                    case em.bank_reconc_typeS.deduction:
                        {
                            _sys_account = accn.CreateChildAccount(xd, accn.GetAccountByAlias("BANK_RECONCILIATION_EXPENSE").account_id, _acc.br_acc_name);
                            break;
                        }
                    case em.bank_reconc_typeS.addition:
                        {
                            _sys_account = accn.CreateChildAccount(xd, accn.GetAccountByAlias("BANK_RECONCILATION_INCOME").account_id, _acc.br_acc_name);
                            break;
                        }
                }
                _acc.sys_account_id = _sys_account.account_id;
                //
               _acc.br_acc_id= xd.SingleInsertCommandTSPInt("acc_bank_reconc_accounts_tb", new string[]
                    {
                        "br_acc_name",
                        "br_acc_type_id",
                        "sys_account_id",
                        "lch_id",
                        "fs_time_stamp",
                       
                    }, new object[]
            {
                _acc.br_acc_name,
                _acc.br_acc_type.ToInt16(),
                _acc.sys_account_id,
                sdata.ChurchID,
                0,
            }

            );
                if(datam.DATA_BANK_RECONCILIATION_ACCOUNTS==null)
                {
                    datam.DATA_BANK_RECONCILIATION_ACCOUNTS = new SortedList<int, ic.bank_reconc_accountC>();
                }
                datam.DATA_BANK_RECONCILIATION_ACCOUNTS.Add(_acc.br_acc_id, _acc);
                xd.CommitTransaction();
            }
            this.Tag = 1;
            sdata.ClearFormCache(em.fm.chart_of_accounts.ToInt16());
            ClearGrid();
        }
        private void ClearGrid()
        {
            foreach (var k in fGrid.Rows.Cast<iGRow>())
            {
                k.Cells[1].Value = null;
                k.Cells[1].AuxValue = null;
            }
            fGrid.Focus();
            fGrid.SetCurCell("account_name", 1);

        }
        private void buttoncancel_Click(object sender, EventArgs e)
        {
            ClearGrid();
        }

        private void fGrid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
        {

            if (fGrid.Rows[e.RowIndex].Cells["desc"].DropDownControl != null && fGrid.Rows[e.RowIndex].Cells["desc"].AuxValue == null)
            {
                fGrid.Rows[e.RowIndex].Cells["desc"].Value = null;
             
                return;
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
