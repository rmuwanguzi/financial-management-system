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

namespace MTOMS.Pledge
{
    public partial class ViewPledgeMembers : Form
    {
        public ViewPledgeMembers()
        {
            InitializeComponent();
        }
        ic.pledgeSettingC m_pledge_settings { get; set; }
        private void ViewPledgeMembers_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            m_pledge_settings = this.Tag as ic.pledgeSettingC;
            InitializeGridColumnMain();
            backworker.RunWorkerAsync();

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
            grid_cols.Add("pledge", "Member\nName");
            grid_cols.Add("pledge_amount", "Pledged\nAmount");
            grid_cols.Add("paid", "Paid\nAmount");
            grid_cols.Add("balance", "Balance");
            grid_cols.Add("p_comp", "Comp\n%");
            grid_cols.Add("phone", "Phone No");
            iGCol myCol;
            foreach (var _grid in _grids)
            {
                foreach (var c in grid_cols)
                {
                    myCol = _grid.Cols.Add(c.Key, c.Value);
                    myCol.IncludeInSelect = true;
                    myCol.SortType = iGSortType.ByValue;
                    myCol.ColHdrStyle.BackColor = Color.AntiqueWhite;
                    myCol.ColHdrStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    myCol.ColHdrStyle.ForeColor = Color.DarkGreen;
                    myCol.ColHdrStyle.Font = new Font("georgia", 12, FontStyle.Bold);
                }
                //
                foreach (var c in new string[] { "paid", "pledge_amount", "balance" })
                {
                    _grid.Cols[c].CellStyle.FormatString = "{0:N0}";
                    _grid.Cols[c].CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
                }
                //
                _grid.Cols["pledge"].CellStyle.ForeColor = Color.DarkBlue;
                _grid.Cols["paid"].CellStyle.ForeColor = Color.DarkGreen;
                _grid.Cols["balance"].CellStyle.ForeColor = Color.Maroon;
                _grid.Cols["p_comp"].ColHdrStyle.TextAlign = iGContentAlignment.MiddleCenter;
                _grid.Cols["p_comp"].CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
                _grid.Cols["p_comp"].CellStyle.ForeColor = Color.Gray;
                _grid.Cols["phone"].CellStyle.ForeColor = Color.Gray;

                _grid.DefaultRow.Height = _grid.GetPreferredRowHeight(true, false);
                _grid.DefaultAutoGroupRow.Height = _grid.DefaultRow.Height;
                _grid.Cols.AutoWidth();
            }

            #endregion
        }
        private void backworker_DoWork(object sender, DoWorkEventArgs e)
        {
            datam.SystemInitializer();
            using (var xd = new xing())
            {
                datam.GetPledgeSettings(xd);
                datam.GetCurrentPledges(xd);
            }
           
        }
        private void LoadMainGrid()
        {

            fGrid.BeginUpdate();
            fGrid.Rows.Clear();
            
            iGRow _row = null;
            float _total = 0f;
            float _paid = 0f;
            float _percent = 0f;
            var _list = from j in datam.DATA_CURRENT_PLEDGES.Values
                        where j.pls_id == m_pledge_settings.pls_id & j.pledge_status != em.pledge_statusS.deleted
                        select j;
            foreach (var n in _list)
            {
                _row = fGrid.Rows.Add();
                _row.Font = new Font("verdana", 12, FontStyle.Regular);
                _row.ReadOnly = iGBool.True;
                _row.Cells["no"].Value = fGrid.Rows.Count;
                _row.Cells["pledge"].Value = n.source_name;
                _row.Cells["pledge_amount"].Value = n.amount_pledged;
                _row.Cells["paid"].Value = n.amount_paid;
                _row.Cells["phone"].Value = n.source_phone;
                if (n.balance > 0)
                {
                    _row.Cells["balance"].Value = n.balance;
                }
                if (n.amount_paid > 0)
                {
                    _total = n.amount_pledged;
                    _paid = n.amount_paid;
                    _percent = ((_paid / _total) * 100);
                    _row.Cells["p_comp"].Value = string.Format(" {0} %", Math.Round(_percent, 1));
                }
                else
                {
                    _row.Cells["p_comp"].Value = string.Format(" {0} %", 0);
                }
                _row.Tag = n;
                _row.Key = string.Format("{0}", n.pl_id);
                _row.AutoHeight();
                _row.Height += 2;

            }

            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
            fGrid.EndUpdate();

        }
        public void NewRecord(ic.pledgeC n)
        {

            fGrid.BeginUpdate();
          

            iGRow _row = null;
            float _total = 0f;
            float _paid = 0f;
            float _percent = 0f;

            _row = fGrid.Rows.Add();
            _row.Font = new Font("verdana", 12, FontStyle.Regular);
            _row.ReadOnly = iGBool.True;
            _row.Cells["no"].Value = fGrid.Rows.Count;
            _row.Cells["pledge"].Value = n.source_name;
            _row.Cells["pledge_amount"].Value = n.amount_pledged;
            _row.Cells["paid"].Value = n.amount_paid;
            _row.Cells["phone"].Value = n.source_phone;
            if (n.balance > 0)
            {
                _row.Cells["balance"].Value = n.balance;
            }
            if (n.amount_paid > 0)
            {
                _total = n.amount_pledged;
                _paid = n.amount_paid;
                _percent = ((_paid / _total) * 100);
                _row.Cells["p_comp"].Value = string.Format(" {0} %", Math.Round(_percent, 1));
            }
            else
            {
                _row.Cells["p_comp"].Value = string.Format(" {0} %", 0);
            }
            _row.Key = string.Format("{0}", n.pl_id);
            _row.AutoHeight();
            _row.Height += 2;
            _row.Tag = n;



            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
            fGrid.EndUpdate();
            if (this.Tag != null)
            {
                this.Tag = null;
            }
        }
        private void backworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label1.Text = string.IsNullOrEmpty(m_pledge_settings.pledge_name) ? datam.DATA_ACCOUNTS[m_pledge_settings.account_id].account_name : m_pledge_settings.pledge_name;
            LoadMainGrid();
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            using (var _fm = new PledgeMaker())
            {
                _fm.Owner = this;
                _fm.Tag = m_pledge_settings;
                _fm.ShowDialog();
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (fGrid.Rows.Count == 0)
            {
                e.Cancel = true;
                return;
            }
            if(fGrid.SelectedRows.Count==0)
            {
                e.Cancel = true;
            }
        }

        private void exportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xcel.xcel.IGridPayRoll(fGrid, string.Format("Pledges"), null, true, new string[] { "pledge_amount", "paid", "balance" });
        }

        private void fGrid_AfterContentsSorted(object sender, EventArgs e)
        {
            if(fGrid.Rows.Count>0)
            {
                fGrid.SetCurCell(0, 1);
            }
            fGrid.BeginUpdate();
            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
            fGrid.EndUpdate();
        }

        private void deleteMemberPlegdeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var _fm = new SdaHelperManager.ConfirmWPwd())
            {
                _fm.ShowDialog();
                if (_fm.DialogResult != DialogResult.OK)
                {
                    return;
                }
            }
            var _obj = fGrid.SelectedRows[0].Tag as ic.pledgeC;
            _obj.pledge_status = em.pledge_statusS.deleted;
            string _str = "Are You Sure You Want To Delete This Member Pledge ??";
            
            using (var xd = new xing())
            {
                var _d_obj = xd.ExecuteScalarObject("select pl_status from pledge_master_tb where pl_id=" + _obj.pl_id);
                if (_d_obj != null)
                {
                    if (_d_obj.ToInt16() == em.pledge_statusS.deleted.ToInt16())
                    {
                        MessageBox.Show("You Have Already Deleted This Member Pledge", "Already Deleted");
                        return;
                    }
                }
                if (!dbm.WarningMessage(_str, "Delete Warning"))
                {
                    return;
                }
                //
                xd.SingleUpdateCommandALL("pledge_master_tb", new string[] { "pl_status", "pl_id" }, new object[]
                {
                    em.pledge_statusS.deleted.ToInt16(),
                    _obj.pl_id
                }, 1);
                //
                xd.SingleUpdateCommandALL("pledge_payment_mvt_tb", new string[] { "status", "pl_id" }, new object[]
                {
                     0,
                    _obj.pl_id
                }, 1);
                //
                xd.SingleUpdateCommandALL("pledge_addition_tb", new string[] { "pl_status", "pl_id" }, new object[]
               {
                    em.pledge_statusS.deleted.ToInt16(),
                    _obj.pl_id
               }, 1);

                //
                accn.PledgeSummary(xd, (_obj.amount_pledged * -1), (_obj.amount_paid * -1), _obj.pls_id);
                //
                xd.CommitTransaction();
            }
            fGrid.Rows.RemoveAt(fGrid.Rows[_obj.pl_id.ToString()].Index);
            if (this.Tag != null)
            {
                this.Tag = null;
            }
        }
        private void UpdateRecord(ic.pledgeC n)
        {
            var _row = fGrid.Rows[n.pl_id.ToString()];
            float _total = 0f;
            float _paid = 0f;
            float _percent = 0f;
            _row.Font = new Font("verdana", 12, FontStyle.Regular);
            _row.ReadOnly = iGBool.True;
            _row.Cells["pledge"].Value = n.source_name;
            _row.Cells["pledge_amount"].Value = n.amount_pledged;
            _row.Cells["paid"].Value = n.amount_paid;
            if (n.balance > 0)
            {
                _row.Cells["balance"].Value = n.balance;
            }
            if (n.amount_paid > 0)
            {
                _total = n.amount_pledged;
                _paid = n.amount_paid;
                _percent = ((_paid / _total) * 100);
                _row.Cells["p_comp"].Value = string.Format(" {0} %", Math.Round(_percent, 1));
            }
            else
            {
                _row.Cells["p_comp"].Value = string.Format(" {0} %", 0);
            }
           
           
        }
        private void addAnotherPledgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var _obj = fGrid.SelectedRows[0].Tag as ic.pledgeC;
            if (m_pledge_settings.end_fs_id < sdata.CURR_FS.fs_id)
            {
                dbm.ErrorMessage("This Pledge Account Has Expired", "Expired Pledge Account");
                return;
            }
            using (var _fm = new AddToPledgeMaker())
            {
                _fm.Owner = this;
                _fm.Tag = _obj;
                _fm.ShowDialog();
                if (_fm.Tag == null)
                {
                    using (var xd = new xing())
                    {
                        datam.GetCurrentPledges(xd);
                        xd.CommitTransaction();
                    }
                    _obj = datam.DATA_CURRENT_PLEDGES[_obj.pl_id];
                    UpdateRecord(_obj);
                    if (this.Tag != null)
                    {
                        this.Tag = null;
                    }
                }
            }
        }
    }
}
