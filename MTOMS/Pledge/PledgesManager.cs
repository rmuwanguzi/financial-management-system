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
    public partial class PledgesManager : DevComponents.DotNetBar.Office2007Form
    {
        public PledgesManager()
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
        private void PledgesManager_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
            datam.SecurityCheck();
            this.VisibleChanged += PledgesManager_VisibleChanged;
            backworker.RunWorkerAsync();
        }


        void PledgesManager_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible & form_loaded)
            {
                CheckUpdates();
            }
        }
        private void InitializeGridColumnMain()
        {
            #region Columns To Display
            List<iGrid> _grids = new List<iGrid>()
            {
                fGrid
            };
            Dictionary<string, string> grid_cols = new Dictionary<string, string>();
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
        private void InitializeLeftGrid()
        {
            #region Columns To Display
            List<TenTec.Windows.iGridLib.iGrid> _grids = new List<iGrid>()
            {
                iGrid1
            };
            Dictionary<string, string> grid_cols = new Dictionary<string, string>();
            grid_cols.Add("pledge", "Pledge\nAccount");
            grid_cols.Add("pledge_amount", "Pledged\nAmount");
            grid_cols.Add("paid", "Paid\nAmount");
            grid_cols.Add("balance", "Balance");
            grid_cols.Add("p_comp", "Comp\n%");
            grid_cols.Add("mem_count", "Member\nCount");
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
        private void DrawLeftTree()
        {
            Application.DoEvents();
            iGrid1.BeginUpdate();
            iGrid1.Rows.Clear();
            fGrid.Rows.Clear();
            iGRow _row = null;
            var nlist = from k in datam.DATA_CURRENT_PLEDGE_SETTINGS.Values
                        where k.pledged_amount > 0
                        orderby k.pls_id
                        select k;
            float _total = 0f;
            float _paid = 0f;
            float _percent = 0f;
            iGRow parent_row = null;
            int mem_count = 0;
            foreach (var k in nlist)
            {
                _row = iGrid1.Rows.Add();
                _row.Font = new Font("verdana", 12, FontStyle.Bold);
                _row.BackColor = Color.WhiteSmoke;
                _row.ReadOnly = iGBool.True;
                _row.Cells["pledge"].Value = datam.DATA_ACCOUNTS[k.account_id].account_name;
                _row.Cells["pledge_amount"].Value = k.pledged_amount;
                _row.Cells["paid"].Value = k.paid_amount;
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
                mem_count = 0;
                var plist = from c in datam.DATA_CURRENT_PLEDGES.Values
                            where c.pls_id == k.pls_id
                            group c by c.cg_id into nw_gp
                            select new
                            {
                                cg_id = nw_gp.Key,
                                plegde_amount = nw_gp.Sum(p => p.amount_pledged),
                                paid_amount = nw_gp.Sum(p => p.amount_paid),
                                balance = nw_gp.Sum(p => p.balance),
                                mem_count = nw_gp.Select(u => new { _id = string.Format("{0}-{1}", u.source_type.ToByte(), u.source_id) }).Distinct().Count()
                            };
                parent_row = _row;
                parent_row.TreeButton = iGTreeButtonState.Visible;
                foreach (var p in plist)
                {
                    _row = iGrid1.Rows.Add();
                    _row.Font = new Font("verdana", 11, FontStyle.Regular);
                    _row.Level = 1;
                    _row.TreeButton = iGTreeButtonState.Hidden;
                    _row.ReadOnly = iGBool.True;
                    if (p.cg_id == 0)
                    {
                        _row.Cells["pledge"].Value = "General";
                        _row.Cells["pledge"].ForeColor = Color.Purple;
                    }
                    else
                    {
                        _row.Cells["pledge"].Value = datam.DATA_CHURCH_GROUPS[p.cg_id].cg_name;
                        _row.Cells["pledge"].ForeColor = Color.Purple;
                    }
                    _row.Cells["pledge_amount"].Value = p.plegde_amount;
                    _row.Cells["paid"].Value = p.paid_amount;
                    _row.Cells["mem_count"].Value = p.mem_count;
                    mem_count += p.mem_count;
                    if (p.balance > 0)
                    {
                        _row.Cells["balance"].Value = p.balance;
                    }
                    if (p.paid_amount > 0)
                    {
                        _total = p.plegde_amount;
                        _paid = p.paid_amount;
                        _percent = ((_paid / _total) * 100);
                        _row.Cells["p_comp"].Value = string.Format(" {0} %", Math.Round(_percent, 1));
                    }
                    else
                    {
                        _row.Cells["p_comp"].Value = string.Format(" {0} %", 0);
                    }
                    _row.Key = string.Format("{0}|{1}", k.pls_id, p.cg_id);
                    _row.Tag = k;
                    _row.Visible = true;
                    _row.AutoHeight();
                    _row.Height += 2;
                }
                parent_row.Cells["mem_count"].Value = mem_count;
              //  parent_row.Expanded = false;
                mem_count = 0;
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
                        InitializeGridColumnMain();
                        DrawLeftTree();
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
                        DrawLeftTree();
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
        private void buttonItem1_Click(object sender, EventArgs e)
        {
            using (var _fm = new PledgeMaker())
            {
                _fm.Owner = this;
                _fm.ShowDialog();
            }
        }
        private void iGrid1_CurRowChanged(object sender, EventArgs e)
        {
            if (iGrid1.CurRow == null)
            {
                fGrid.Rows.Clear(); return;
            }
           
            LoadList();
        }
        private void LoadList()
        {
            if (iGrid1.CurRow.Level == 0)
            {
                var nlist = from j in datam.DATA_CURRENT_PLEDGES.Values
                            where j.pls_id == iGrid1.CurCell.RowKey.ToInt32()
                            select j;
                LoadMainGrid(nlist);
            }
            else
            {
                var _split = iGrid1.CurCell.RowKey.Split(new char[] { '|' });
                if (_split != null && _split.Length == 2)
                {
                    var nlist = from j in datam.DATA_CURRENT_PLEDGES.Values
                                where j.pls_id == _split[0].ToInt32() &
                                j.cg_id==_split[1].ToInt32()
                                select j;
                    LoadMainGrid(nlist);
                }
                else
                {
                    fGrid.Rows.Clear();
                }
            }
         

        }
        private void LoadMainGrid(IEnumerable<ic.pledgeC> _list)
        {

            fGrid.BeginUpdate();
            fGrid.Rows.Clear();
            if (_list == null) { fGrid.EndUpdate(); return; }
            iGRow _row = null;
            float _total = 0f;
            float _paid = 0f;
            float _percent = 0f;
          
            foreach (var n in _list)
            {
                _row = fGrid.Rows.Add();
                _row.Font = new Font("verdana", 12, FontStyle.Regular);
                 _row.ReadOnly = iGBool.True;
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
                _row.Key = string.Format("P-{0}", n.pl_id);
                _row.AutoHeight();
                _row.Height += 2;
              
            }
           
            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
            fGrid.EndUpdate();

        }
        private void iGrid1_CellMouseUp(object sender, iGCellMouseUpEventArgs e)
        {
            iGrid1.SetCurCell(e.RowIndex, e.ColIndex);
        }

        private void fGrid_AfterContentsSorted(object sender, EventArgs e)
        {
            fGrid.BeginUpdate();
            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
            fGrid.EndUpdate();
        }

        private void exSplitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            fGrid.BeginUpdate();
            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
            fGrid.EndUpdate();
            //
            iGrid1.BeginUpdate();
            iGrid1.Cols.AutoWidth();
            iGrid1.AutoResizeCols = false;
            iGrid1.EndUpdate();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (fGrid.Rows.Count == 0)
            {
                e.Cancel = true;
            }
        }

        private void exportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xcel.xcel.IGridPayRoll(fGrid, string.Format("Pledges"), null, true, new string[] { "pledge_amount", "paid","balance" });
           
        }

        private void iGrid1_SizeChanged(object sender, EventArgs e)
        {
            fGrid.BeginUpdate();
            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
            fGrid.EndUpdate();
        }

        private void printGridToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
    
}
