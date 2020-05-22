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

namespace MTOMS.Accounts
{
    public partial class DiscontinuedAccountListForm : Form
    {
        public DiscontinuedAccountListForm()
        {
            InitializeComponent();
        }

        private void DisabledAccountListForm_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            InitializeGridColumnMain();
            LoadGrid();
        }
        private void LoadGrid()
        {
            iGRow _row = null;
            var _nlist = (from d in datam.DATA_ACCOUNTS.Values
                         where d.account_status == em.account_statusS.DisContinued
                         select d);
            fGrid.BeginUpdate();
            foreach (var n in _nlist)
            {
               
                _row = fGrid.Rows.Add();
                _row.ReadOnly = iGBool.True;
                _row.Font = new Font("georgia", 12, FontStyle.Regular);
                _row.Cells["no"].Value = fGrid.Rows.Count;
                _row.Cells["account_name"].Value = datam.DATA_ACCOUNTS[n.account_id].account_name;
                
                 _row.Key = n.account_id.ToStringNullable();
                _row.Tag = n;
                _row.AutoHeight();
            }
            fGrid.AutoResizeCols = false;
            fGrid.Cols.AutoWidth();
            fGrid.EndUpdate();
        }
        private void InitializeGridColumnMain()
        {
            #region Columns To Display
            List<iGrid> _grids = new List<iGrid>()
            {
                fGrid
            };
            Dictionary<string, string> grid_cols = new Dictionary<string, string>();
            grid_cols.Add("no", "No");
            grid_cols.Add("account_name", "Account Name");
            iGCol myCol;
            foreach (var _grid in _grids)
            {
                _grid.BeginUpdate();
                foreach (var c in grid_cols)
                {
                    myCol = _grid.Cols.Add(c.Key, c.Value);
                    myCol.IncludeInSelect = true;
                    myCol.SortType = iGSortType.None;
                    myCol.ColHdrStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    myCol.ColHdrStyle.ForeColor = Color.DarkGreen;
                    myCol.ColHdrStyle.Font = new Font("georgia", 13, FontStyle.Regular);

                }
                _grid.DefaultRow.Height = _grid.GetPreferredRowHeight(true, false);
                _grid.DefaultAutoGroupRow.Height = _grid.DefaultRow.Height;
                _grid.Cols.AutoWidth();
                _grid.EndUpdate();
            }

            #endregion
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if(fGrid.SelectedRows.Count==0)
            {
                e.Cancel = true;
                return;
            }
        }

        private void reEnableAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string _str = "Are You Sure You Want To Re-Enable This Income Account ??";
            iGRow _row = fGrid.SelectedRows[0];
            var _obj = fGrid.SelectedRows[0].Tag as ic.accountC;
            if (_obj == null) { return; }
            using (var _fm = new SdaHelperManager.ConfirmWPwd())
            {
                _fm.ShowDialog();
                if (_fm.DialogResult != DialogResult.OK)
                {
                    return;
                }
            }
            if (!dbm.WarningMessage(_str, "Re-Enable Warning"))
            {
                return;
            }
            using (var xd = new xing())
            {
                xd.SingleUpdateCommandALL("accounts_tb", new string[]
                   {
                        "account_status_id",
                        "account_id"
                   }, new object[]
                   {
                    em.account_statusS.Enabled.ToByte(),
                    _obj.account_id
                   }, 1);
                
                _obj.account_status = em.account_statusS.Enabled;
                xd.CommitTransaction();
                sdata.ClearFormCache(em.fm.chart_of_accounts.ToInt16());
                sdata.ClearFormCache(em.fm.accounts_balances_manager.ToInt16());
                sdata.ClearFormCache(em.fm.analyze_offering_weekly.ToInt16());
                sdata.ClearFormCache(em.fm.offering_range_1.ToInt16());
                sdata.ClearFormCache(em.fm.sabbath_cash_statement.ToInt16());
                sdata.ClearFormCache(em.fm.quarter_cash.ToInt16());
            }
            (this.Owner as IncomeAccountsManager).EditRow(_obj);
            fGrid.Rows.RemoveAt(fGrid.Rows[_obj.account_id.ToString()].Index);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
