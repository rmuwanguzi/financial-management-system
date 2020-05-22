using SdaHelperManager;
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
    public partial class PledgesManagerB : DevComponents.DotNetBar.Office2007Form
    {
        public PledgesManagerB()
        {
            InitializeComponent();
        }
        bool form_loaded = false;
        enum _process
        {
            form_load,
            check_updates
        }
        _process m_process = _process.form_load;
        private void PledgesManagerB_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
            datam.SecurityCheck();
            this.VisibleChanged += PledgesManagerB_VisibleChanged;
            backworker.RunWorkerAsync();
        }


        void PledgesManagerB_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible & form_loaded)
            {
                CheckUpdates();
            }
        }
     
        private void InitializeLeftGrid()
        {
            #region Columns To Display
            List<TenTec.Windows.iGridLib.iGrid> _grids = new List<iGrid>()
            {
                iGrid1
            };
            Dictionary<string, string> grid_cols = new Dictionary<string, string>();
            grid_cols.Add("no", "No");
            grid_cols.Add("pledge_name", "Pledge\nName");
            grid_cols.Add("pledge", "Pledge\nAccount");
            grid_cols.Add("pledge_amount", "Pledged\nAmount");
            grid_cols.Add("paid", "Paid\nAmount");
            grid_cols.Add("balance", "Balance");
            grid_cols.Add("p_comp", "Comp\n%");
            grid_cols.Add("mem_count", "Member\nCount");
            grid_cols.Add("start_date", "Start\nDate");
            grid_cols.Add("end_date", "End\nDate");
            grid_cols.Add("status", "Status");
            iGCol myCol;
            foreach (var _grid in _grids)
            {
                foreach (var c in grid_cols)
                {
                    myCol = _grid.Cols.Add(c.Key, c.Value);
                    myCol.IncludeInSelect = true;
                    myCol.SortType = iGSortType.None;
                    myCol.ColHdrStyle.BackColor = Color.Thistle;
                    myCol.ColHdrStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    myCol.ColHdrStyle.ForeColor = Color.Black;
                    myCol.ColHdrStyle.Font = new Font("georgia", 13, FontStyle.Regular);
                }
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
                 _grid.Cols["mem_count"].CellStyle.ForeColor = Color.Gray;
                 _grid.Cols["mem_count"].ColHdrStyle.TextAlign = iGContentAlignment.MiddleCenter;
                 _grid.Cols["mem_count"].CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
                //
                _grid.DefaultRow.Height = _grid.GetPreferredRowHeight(true, false);
                _grid.DefaultAutoGroupRow.Height = _grid.DefaultRow.Height;
                _grid.Cols.AutoWidth();
            }

            #endregion
        }
        private void backworker_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (m_process)
            {
                case _process.form_load:
                    {
                        datam.SystemInitializer();
                        using (var xd = new xing())
                        {
                            datam.GetPledgeSettings(xd);
                            datam.GetCurrentPledges(xd);
                        }
                        break;
                    }
                case _process.check_updates:
                    {
                        using (var xd = new xing())
                        {
                            datam.GetPledgeSettings(xd);
                            datam.GetCurrentPledges(xd);
                            xd.CommitTransaction();
                        }
                        break;
                    }
            }
        }
        private void LoadList()
        {
            Application.DoEvents();
            iGrid1.BeginUpdate();
            iGrid1.Rows.Clear();
            iGRow _row = null;
            var nlist = from k in datam.DATA_CURRENT_PLEDGE_SETTINGS.Values
                        where k.status != em.pledge_setting_statusS.deleted
                        orderby k.pls_id
                        select k;
            float _total = 0f;
            float _paid = 0f;
            float _percent = 0f;
            iGRow parent_row = null;
           foreach (var k in nlist)
            {
                _row = iGrid1.Rows.Add();
                _row.Font = new Font("georgia", 12, FontStyle.Regular);
               _row.ReadOnly = iGBool.True;
                _row.Cells["no"].Value = iGrid1.Rows.Count;
                _row.Cells["pledge_name"].Value = k.pledge_name;
                _row.Cells["pledge"].Value = datam.DATA_ACCOUNTS[k.account_id].account_name;
                _row.Cells["pledge"].ForeColor = Color.Gray;
                if(k.status== em.pledge_setting_statusS.expired)
                {
                    _row.Font = new Font("georgia", 12, FontStyle.Italic | FontStyle.Regular);
                }
                if (string.IsNullOrEmpty(k.pledge_name))
                {
                    _row.Cells["pledge_name"].Value = _row.Cells["pledge"].Value;
                }
                _row.Cells["pledge_amount"].Value = k.pledged_amount;
                _row.Cells["paid"].Value = k.paid_amount;
                if (k.start_date != null)
                {
                    _row.Cells["start_date"].Value = k.start_date.Value.ToMyShortDate();
                }
                if (k.end_date != null)
                {
                    _row.Cells["end_date"].Value = k.end_date.Value.ToMyShortDate();
                }
                if (k.balance > 0)
                {
                    _row.Cells["balance"].Value = k.balance;
                }
                if (k.paid_amount > 0)
                {
                    _total = k.pledged_amount;
                    _paid = k.paid_amount;
                    _percent = ((_paid / _total) * 100);
                    _row.Cells["p_comp"].Value = string.Format(" {0} %", Math.Round(_percent, 1));
                }
                else
                {
                    _row.Cells["p_comp"].Value = string.Format(" {0} %", 0);
                }
                _row.Key = string.Format("{0}", k.pls_id);
                _row.Tag = k;
                _row.Visible = true;
                _row.AutoHeight();
                _row.Height += 2;
             
                var plist = ( from c in datam.DATA_CURRENT_PLEDGES.Values
                            where c.pls_id == k.pls_id & c.pledge_status!=em.pledge_statusS.deleted
                            group c by c.cg_id into nw_gp
                            select new
                            {
                                cg_id = nw_gp.Key,
                                plegde_amount = nw_gp.Sum(p => p.amount_pledged),
                                paid_amount = nw_gp.Sum(p => p.amount_paid),
                                balance = nw_gp.Sum(p => p.balance),
                                mem_count = nw_gp.Select(u => new { _id = string.Format("{0}-{1}", u.source_type.ToByte(), u.source_id) }).Distinct().Count()
                            }).ToList();
                parent_row = _row;
                parent_row.TreeButton = iGTreeButtonState.Hidden;
                parent_row.Cells["mem_count"].Value = plist.Sum(h => h.mem_count);
             
              
            }
            iGrid1.Cols.AutoWidth();
            iGrid1.AutoResizeCols = false;
            iGrid1.EndUpdate();
        }
        public void CheckUpdates()
        {
            m_process = _process.check_updates;
            backworker.RunWorkerAsync();
        }
        private void backworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch (m_process)
            {
                case _process.form_load:
                    {
                        InitializeLeftGrid();
                        LoadList();
                        form_loaded = true;
                        break;
                    }
                case _process.check_updates:
                    {
                        string _prev_key = null;
                        if (iGrid1.CurCell != null)
                        {
                            _prev_key = iGrid1.CurCell.RowKey;
                        }
                        LoadList();
                        if (!string.IsNullOrEmpty(_prev_key))
                        {
                            try
                            {
                                iGrid1.SetCurCell(_prev_key, 1);
                            }
                            catch (Exception)
                            {

                            }
                        }
                        break;
                    }
            }
        }
        public void NewRecord(ic.pledgeSettingC k)
        {
            Application.DoEvents();
            iGrid1.BeginUpdate();
     
            iGRow _row = null;

            float _total = 0f;
            float _paid = 0f;
            float _percent = 0f;
            iGRow parent_row = null;

            _row = iGrid1.Rows.Add();
            _row.Font = new Font("georgia", 12, FontStyle.Regular);
            _row.ReadOnly = iGBool.True;
            _row.Cells["no"].Value = iGrid1.Rows.Count;
            _row.Cells["pledge_name"].Value = k.pledge_name;
            _row.Cells["pledge"].Value = datam.DATA_ACCOUNTS[k.account_id].account_name;
            _row.Cells["pledge"].ForeColor = Color.Gray;
            if (string.IsNullOrEmpty(k.pledge_name))
            {
                _row.Cells["pledge_name"].Value = _row.Cells["pledge"].Value;
            }
            _row.Cells["pledge_amount"].Value = k.pledged_amount;
            _row.Cells["paid"].Value = k.paid_amount;
            if (k.start_date != null)
            {
                _row.Cells["start_date"].Value = k.start_date.Value.ToMyShortDate();
            }
            if (k.end_date != null)
            {
                _row.Cells["end_date"].Value = k.end_date.Value.ToMyShortDate();
            }
            if (k.balance > 0)
            {
                _row.Cells["balance"].Value = k.balance;
            }
            if (k.paid_amount > 0)
            {
                _total = k.pledged_amount;
                _paid = k.paid_amount;
                _percent = ((_paid / _total) * 100);
                _row.Cells["p_comp"].Value = string.Format(" {0} %", Math.Round(_percent, 1));
            }
            else
            {
                _row.Cells["p_comp"].Value = string.Format(" {0} %", 0);
            }
            _row.Key = string.Format("{0}", k.pls_id);
            _row.Tag = k;
            _row.Visible = true;
            _row.AutoHeight();
            _row.Height += 2;


            parent_row = _row;
            parent_row.TreeButton = iGTreeButtonState.Hidden;
            parent_row.Cells["mem_count"].Value = null;



            iGrid1.Cols.AutoWidth();
            iGrid1.AutoResizeCols = false;
            iGrid1.EndUpdate();
        }
        private void buttonItem1_Click(object sender, EventArgs e)
        {
            using (var _fm = new PledgeSettings())
            {
                _fm.Owner = this;
                _fm.ShowDialog();
            }
        }
        private void iGrid1_CurRowChanged(object sender, EventArgs e)
        {
           
        }
        private void iGrid1_CellMouseUp(object sender, iGCellMouseUpEventArgs e)
        {
            iGrid1.SetCurCell(e.RowIndex, e.ColIndex);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (iGrid1.SelectedRows.Count == 0)
            {
                e.Cancel = true;
                return;
            }
        }

        private void viewMemberPledgesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var _fm = new Pledge.ViewPledgeMembers())
            {
                _fm.Owner = this;
                _fm.Tag = iGrid1.SelectedRows[0].Tag;
                _fm.ShowDialog();
                if (_fm.Tag == null)
                {
                    CheckUpdates();
                }
            }
        }

        private void deletePledgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var _fm = new SdaHelperManager.ConfirmWPwd())
            {
                _fm.ShowDialog();
                if (_fm.DialogResult != DialogResult.OK)
                {
                    return;
                }
            }
            var _obj = iGrid1.SelectedRows[0].Tag as ic.pledgeSettingC;
            _obj.status = em.pledge_setting_statusS.deleted;
            string _str = "Are You Sure You Want To Delete This Pledge Account ??";
            if (!dbm.WarningMessage(_str, "Cancel Warning"))
            {
                return;
            }
            using (var xd = new xing())
            {
                xd.SingleUpdateCommandALL("pledge_settings_tb", new string[] { "status", "pls_id" }, new object[]
                {
                    em.pledge_setting_statusS.deleted.ToInt16(),
                    _obj.pls_id
                }, 1);
                //
                xd.SingleUpdateCommandALL("pledge_master_tb", new string[] { "pl_status", "pls_id" }, new object[]
                {
                    em.pledge_statusS.deleted.ToInt16(),
                    _obj.pls_id
                }, 1);
                //
                xd.SingleUpdateCommandALL("pledge_payment_mvt_tb", new string[] { "status", "pls_id" }, new object[]
                {
                     0,
                    _obj.pls_id
                }, 1);
                xd.CommitTransaction();
            }
            iGrid1.Rows.RemoveAt(iGrid1.Rows[_obj.pls_id.ToString()].Index);
        }
    }
    
}
