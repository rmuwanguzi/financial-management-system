using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SdaHelperManager;
using TenTec.Windows.iGridLib;
using SdaHelperManager.Security;
namespace MTOMS
{
    public partial class MonthlyTransferManager : DevComponents.DotNetBar.Office2007Form
    {
        public MonthlyTransferManager()
        {
            InitializeComponent();
        }
        int m_YEAR = 0;
        int m_MONTH = 0;
        int m_partition = 0;
         bool UPDATE_MAIN_GRID = false;
       private void InitializeLeftGrid()
        {
            #region Columns To Display
            List<iGrid> _grids = new List<iGrid>()
            {
                iGrid1
            };
           
            iGCol myCol;
            foreach (var _grid in _grids)
            {
                myCol = _grid.Cols.Add("item_name", "Month\nStatistics");
                myCol.IncludeInSelect = true;
                myCol.SortType = iGSortType.None;
                //  myCol.ColHdrStyle.BackColor = Color.AntiqueWhite;
                myCol.ColHdrStyle.TextAlign = iGContentAlignment.MiddleCenter;
                myCol.ColHdrStyle.ForeColor = Color.DarkGreen;
                myCol.ColHdrStyle.Font = new Font("georgia", 11, FontStyle.Bold);
                //
                Dictionary<string, string> _extra_cols = new Dictionary<string, string>();
                _extra_cols.Add("payments", "Payments");
                _extra_cols.Add("expenses", "Expenses");
              
                foreach (var f in _extra_cols)
                {
                    myCol = _grid.Cols.Add(f.Key, f.Value);
                    myCol.CellStyle.FormatString = "{0:N0}";
                    myCol.IncludeInSelect = true;
                    myCol.SortType = iGSortType.None;
                    //  myCol.ColHdrStyle.BackColor = Color.AntiqueWhite;
                    myCol.ColHdrStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    myCol.ColHdrStyle.Font = new Font("georgia", 12, FontStyle.Regular);
                    myCol.ColHdrStyle.ForeColor = Color.WhiteSmoke;
                    myCol.ColHdrStyle.BackColor = Color.Gray;
                    myCol.CellStyle.ForeColor = Color.DarkGray;
                    myCol.CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    myCol.Visible = false;
                  
                }
                //
                myCol = _grid.Cols.Add("total", "Transfer\nCount");
                myCol.CellStyle.FormatString = "{0:N0}";
                myCol.CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
               
                myCol.IncludeInSelect = true;
                myCol.SortType = iGSortType.None;
                //  myCol.ColHdrStyle.BackColor = Color.AntiqueWhite;
                myCol.ColHdrStyle.TextAlign = iGContentAlignment.MiddleCenter;
                myCol.ColHdrStyle.ForeColor = Color.DarkRed;
                myCol.ColHdrStyle.Font = new Font("georgia", 11, FontStyle.Bold);
               
               
                _grid.SearchAsType.Mode = iGSearchAsTypeMode.Filter;
                _grid.SearchAsType.MatchRule = iGMatchRule.Contains;
               
                
                _grid.DefaultRow.Height = _grid.GetPreferredRowHeight(true, false);
                _grid.DefaultAutoGroupRow.Height = _grid.DefaultRow.Height;
                _grid.Cols.AutoWidth();
            }

            #endregion
        }
        private void InitializeGridColumnMain()
        {
            #region Columns To Display
            List<iGrid> _grids = new List<iGrid>()
            {
                fGrid
            };
            Dictionary<string, string> grid_cols = new Dictionary<string, string>();
            grid_cols.Add("date", "Date");
            grid_cols.Add("from_account", "From");
            grid_cols.Add("to_account", "To");
            grid_cols.Add("amount", "Amount");
            grid_cols.Add("details", "Transfer Details");
            //grid_cols.Add("pay_mode", "Pay Mode");
            //grid_cols.Add("dept", "Department");
            //grid_cols.Add("source", "Source");
          
           iGCol myCol;
            foreach (var _grid in _grids)
            {

                foreach (var c in grid_cols)
                {
                    myCol = _grid.Cols.Add(c.Key, c.Value);
                    myCol.IncludeInSelect = true;
                    myCol.SortType = iGSortType.None;
                    //  myCol.ColHdrStyle.BackColor = Color.AntiqueWhite;
                    myCol.ColHdrStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    myCol.ColHdrStyle.ForeColor = Color.DarkGreen;
                    myCol.ColHdrStyle.Font = new Font("georgia", 13, FontStyle.Regular);
                   
                }
                _grid.Cols["date"].ColHdrStyle.ForeColor = Color.Maroon;
                _grid.Cols["date"].CellStyle.ForeColor = Color.DarkGray;
                _grid.Cols["from_account"].CellStyle.ForeColor = Color.Maroon;
                _grid.Cols["to_account"].CellStyle.ForeColor = Color.Blue;
                //
                _grid.Cols["amount"].CellStyle.FormatString = "{0:N0}";
                _grid.DefaultRow.Height = _grid.GetPreferredRowHeight(true, false);
                _grid.DefaultAutoGroupRow.Height = _grid.DefaultRow.Height;
                _grid.Cols.AutoWidth();
            }

            #endregion  
        }
        public void CheckForUpdates()
        {
            m_partition = string.Format("{0}{1}", m_YEAR, m_MONTH).ToInt32();
            if (datam.GetMonthCashTransfers(m_partition))
            {
               
                var nlist = from n in datam.DATA_MONTH_CASH_TRANSFERS[m_partition]
                            where n.Value.is_updated 
                            group n.Value by n.Value.fs_id into new_group
                            select new
                            {
                                fs_id=new_group.Key,
                                group_val=new_group
                            };
                if (nlist.Count() > 0)
                {
                    string _prev_key = null;
                    if (iGrid1.CurCell != null)
                    {
                        _prev_key = iGrid1.CurCell.Row.Key;
                    }
                    iGrid1.CurCell = null;
                    fGrid.Rows.Clear();
                    foreach (var t in nlist)
                    {
                        try
                        {
                            iGrid1.Rows[t.fs_id.ToString()].Cells["total"].Value = datam.DATA_MONTH_CASH_TRANSFERS[m_partition].Where(u => u.Value.fs_id == t.fs_id & u.Value.status == em.cashTransferStatus.valid).Count();
                            if (!iGrid1.Rows[t.fs_id.ToString()].Visible)
                            {
                                iGrid1.Rows[t.fs_id.ToString()].Visible = true;
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }

                    }
                    iGrid1.Rows[0].Cells["total"].Value = datam.DATA_MONTH_CASH_TRANSFERS[m_partition].Values.Where(j => j.status == em.cashTransferStatus.valid).Count();
                    iGrid1.Cols.AutoWidth();
                    iGrid1.AutoResizeCols = false;
                    if (!string.IsNullOrEmpty(_prev_key))
                    {
                        iGrid1.SetCurCell(_prev_key, 1);
                    }
                }
            }
        }
        private void ViewPaymentsN_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
            datam.SecurityCheck();
           
            labelCalender.Text = string.Empty;
            this.VisibleChanged += new EventHandler(ViewPaymentsN_VisibleChanged);
            fGrid.AfterContentsSorted+=new EventHandler(fGrid_AfterContentsSorted);
            fGrid.CurRowChanged += new EventHandler(fGrid_CurRowChanged);
          
            label_back.MouseDown += new MouseEventHandler(label_back_MouseDown);
            label_back.MouseUp += new MouseEventHandler(label_back_MouseUp);
            //
            label_forward.MouseDown += new MouseEventHandler(label_back_MouseDown);
            label_forward.MouseUp+=new MouseEventHandler(label_back_MouseUp);
            //
            //if (sdata.CURRENT_MENU != null)
            //{
            //    buttonAdd.Text = "+ Enter Expenses";
            //}
            backworker.RunWorkerAsync();
        }

        void ViewPaymentsN_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible )
            {
                if (iGrid1.CurCell != null)
                {
                    UPDATE_MAIN_GRID = true;
                    CheckForUpdates();
                }
            }
           
           
        }
        private void LoadList()
        {
            if (iGrid1.CurRow.Level == 0)
            {
                var nlist = from j in datam.DATA_MONTH_CASH_TRANSFERS[string.Format("{0}{1}", m_YEAR, m_MONTH).ToInt32()]
                            where j.Value.status != em.cashTransferStatus.deleted
                            select j.Value;
               
                LoadMainGrid(nlist);

            }
            else
            {
                var nlist = from j in datam.DATA_MONTH_CASH_TRANSFERS[string.Format("{0}{1}", m_YEAR, m_MONTH).ToInt32()]
                            where j.Value.fs_id == iGrid1.CurRow.Key.ToInt32() & j.Value.status != em.cashTransferStatus.deleted
                            select j.Value;
                //if (UPDATE_MAIN_GRID)
                //{
                //    iGrid1.CurRow.Cells[1].Value = nlist.Sum(k => k.exp_amount).ToNumberDisplayFormat();
                //}
                LoadMainGrid(nlist);
            }
            

        }
        void fGrid_CurRowChanged(object sender, EventArgs e)
        {
            if (fGrid.CurRow == null)
            {
              return;
            }
         }
        private void backworker_DoWork(object sender, DoWorkEventArgs e)
        {
            datam.SystemInitializer();
        }
        private void backworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            InitializeLeftGrid();
            InitializeGridColumnMain();
            labelCalender.Text = string.Format("{0} {1}", sdata.CURR_DATE.Month.ToMonthName(), sdata.CURR_DATE.Year);
            labelCalender.Tag = sdata.CURR_DATE;
            DrawLeftTree();
            buttonAdd.Enabled = true;
         }
        private void button1_Click(object sender, EventArgs e)
         {
            
         }
        private void DrawLeftTree()
        {
            if (labelCalender.Tag == null)
            {
                return;
            }
           
            Application.DoEvents();
            var _dt = Convert.ToDateTime(labelCalender.Tag);
            m_YEAR = _dt.Year;
            m_MONTH = _dt.Month;
            int new_val = string.Format("{0}{1}", m_YEAR, m_MONTH).ToInt32();
            iGrid1.BeginUpdate();
            iGrid1.Rows.Clear();
            fGrid.Rows.Clear();
            if (!datam.GetMonthCashTransfers(new_val))
            {
              
            }
            var _days = DateTime.DaysInMonth(m_YEAR, m_MONTH);
            iGRow _row = null;
            #region draw_month header
            _row = iGrid1.Rows.Add();
            _row.Font = new Font("georgia", 12, FontStyle.Bold);
            _row.ForeColor = Color.Blue;
            _row.Cells["payments"].ForeColor = Color.FromArgb(64, 64, 64);
            _row.Cells["expenses"].ForeColor = Color.FromArgb(64, 64, 64);
            _row.Cells["total"].ForeColor = Color.Maroon;
            _row.Cells[0].Value = datam.MONTHS[m_MONTH - 1];
            _row.TreeButton = iGTreeButtonState.Visible;
            _row.Level = 0;
            _row.ReadOnly = iGBool.True;
            _row.Height += 2;
            _row.Key = m_MONTH.ToStringNullable();
            _row.AutoHeight();
            #endregion
            _row = iGrid1.Rows.Add();
            _row.Height = 7;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            _row.Level = 1;
            _row.TreeButton = iGTreeButtonState.Hidden;
            _row.Key = "sddd";
            #region draw month details
             DateTime? _last_date = new DateTime(m_YEAR, m_MONTH, _days);
            int start_fs_id = fn.GetFSID(_last_date.Value);
            for (int j = _days; j > 0; j--)
            {
                _row = iGrid1.Rows.Add();
                _row.ReadOnly = iGBool.True;
                _row.Level = 1;
                _row.TreeButton = iGTreeButtonState.Hidden;
                _row.Font = new Font("georgia", 12, FontStyle.Regular);
                _row.ForeColor = Color.Maroon;
                //_row.Cells["payments"].ForeColor = Color.FromArgb(64, 64, 64);
                //_row.Cells["expenses"].ForeColor = Color.FromArgb(64, 64, 64);
                _row.Cells[0].Value = j.ToNthDay();
                _row.Cells[0].AuxValue = j;
                _row.AutoHeight();
                _row.Height += 2;
                _row.Key = start_fs_id.ToStringNullable();
                _row.Visible = false;
                _row.Tag = new DateTime(m_YEAR, m_MONTH, j).ToMyLongDate();
               
                start_fs_id--;
            }
            #endregion
            if (datam.DATA_MONTH_CASH_TRANSFERS[new_val].Keys.Count > 0)
            {
                var nlist = from n in datam.DATA_MONTH_CASH_TRANSFERS[new_val]
                            where n.Value.status==em.cashTransferStatus.valid
                            orderby n.Value.fs_id descending
                            group n by n.Value.fs_id
                                into new_group
                                select new
                                {
                                    fs_id = new_group.Key,
                                    total_entries = new_group.Count(),
                                };
                if (nlist.Count() > 0)
                {
                    iGrid1.Rows[m_MONTH.ToString()].Cells["total"].Value = nlist.Sum(k => k.total_entries);
                    if ((nlist.Max(j => j.fs_id) != sdata.CURR_FS.fs_id) & new_val == string.Format("{0}{1}", sdata.CURR_DATE.Year, sdata.CURR_DATE.Month).ToInt32())
                    {
                        iGrid1.Rows[sdata.CURR_FS.fs_id.ToString()].Cells["total"].Value = null;
                        iGrid1.Rows[sdata.CURR_FS.fs_id.ToString()].Visible = true;
                    }
                    foreach (var d in nlist)
                    {
                        iGrid1.Rows[d.fs_id.ToString()].Cells["total"].Value = d.total_entries;
                        iGrid1.Rows[d.fs_id.ToString()].Visible = true;
                    }
                    if (new_val == string.Format("{0}{1}", sdata.CURR_DATE.Year, sdata.CURR_DATE.Month).ToInt32())
                    {
                        iGrid1.SetCurCell(sdata.CURR_FS.fs_id.ToString(), 0);
                    }
                }
            }
            else
            {
                iGrid1.Rows[m_MONTH.ToString()].Cells["total"].Value = null;
                if (m_YEAR == sdata.CURR_DATE.Year & m_MONTH == sdata.CURR_DATE.Month)
                {
                    iGrid1.Rows[sdata.CURR_FS.fs_id.ToString()].Cells["total"].Value = null;
                    iGrid1.Rows[sdata.CURR_FS.fs_id.ToString()].Visible = true;
                }
            }
          
            iGrid1.Cols.AutoWidth();
            iGrid1.AutoResizeCols = false;
            iGrid1.EndUpdate();
            
        }
        private void LoadMainGrid(IEnumerable<ic.cash_transferC> _list)
        {
            fGrid.BeginUpdate();
            fGrid.Rows.Clear();
            if (_list == null) { fGrid.EndUpdate(); return; }
            iGRow _row = null;

            ic.accountC _acc = null;
                foreach (var n in _list)
                {
                   
                    _row = fGrid.Rows.Add();
                    _row.ReadOnly = iGBool.True;
                    _row.Font = new Font("georgia", 12, FontStyle.Regular);
                    _row.Cells["date"].Value = n.fs_date.Value.ToMyShortDate();
                    _acc = datam.DATA_ACCOUNTS[n.source_id];
                    _row.Cells["from_account"].Value = _acc.account_name;
                    //
                    _acc = datam.DATA_ACCOUNTS[n.destination_id];
                    _row.Cells["to_account"].Value = _acc.account_name;
                    if (_acc.account_id == -2355)
                    {
                        _row.Cells["to_account"].Value = "LCB";
                    }
                    //
                    _row.Cells["amount"].Value = n.amount;
                    _row.Cells["amount"].TextAlign = iGContentAlignment.MiddleCenter;
                    _row.Cells["amount"].ForeColor = Color.DarkBlue;
                    //
                    _row.Cells["details"].Value = n.transfer_reason;
                   //
                   
                    _row.Key = n.un_id.ToStringNullable();
                    _row.Tag = n;
                    _row.AutoHeight();
                
                    n.is_updated = false;
                }
            
            fGrid.EndUpdate();
            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
        }
        private void fGrid_AfterContentsSorted(object sender, EventArgs e)
        {

        }
        private void iGrid1_Click(object sender, EventArgs e)
        {

        }
        private void iGrid1_CurRowChanged(object sender, EventArgs e)
        {
            if (iGrid1.CurRow == null)
            {
               fGrid.Rows.Clear(); return;
            }
            if (iGrid1.CurCell != null && iGrid1.CurCell.Row.Tag != null)
            {
                paneltotal.Text = iGrid1.CurCell.Row.Tag.ToStringNullable();
            }
            else
            {
                paneltotal.Text = string.Empty;
            }
            LoadList();
        }
         private void labelCalender_DoubleClick(object sender, EventArgs e)
        {
            var _dt = sdata.CURR_DATE;
            labelCalender.Text = string.Format("{0} {1}", _dt.Month.ToMonthName(), _dt.Year);
            labelCalender.Tag = _dt;
            if (_dt.Year == sdata.CURR_DATE.Year)
            {
                if (_dt.Month >= sdata.CURR_DATE.Month)
                {
                    label_forward.Visible = false;
                }
            }
            iGrid1.Rows.Clear();
            fGrid.Rows.Clear();
            DrawLeftTree();
        }
        private void label_forward_Click(object sender, EventArgs e)
        {
            var _dt = fnn.ScrollMonthControl(System.Convert.ToDateTime(labelCalender.Tag), 0);
            labelCalender.Text = string.Format("{0} {1}", _dt.Month.ToMonthName(), _dt.Year);
            labelCalender.Tag = _dt;
            if (label_forward.Visible)
            {
                if (_dt.Year == sdata.CURR_DATE.Year)
                {
                    if (_dt.Month >= sdata.CURR_DATE.Month)
                    {
                        label_forward.Visible = false;
                    }
                }
              }
            iGrid1.Rows.Clear();
            fGrid.Rows.Clear();
            DrawLeftTree();
        }
        void label_back_MouseUp(object sender, MouseEventArgs e)
        {
            (sender as Control).BackColor = Color.DimGray;
        }
        void label_back_MouseDown(object sender, MouseEventArgs e)
        {
            (sender as Control).BackColor = Color.Red;
        }
        private void label_back_Click(object sender, EventArgs e)
        {
            var _dt = fnn.ScrollMonthControl(System.Convert.ToDateTime(labelCalender.Tag), 1);
            labelCalender.Text = string.Format("{0} {1}", _dt.Month.ToMonthName(), _dt.Year);
            labelCalender.Tag = _dt;
            if (!label_forward.Visible)
            {
                label_forward.Visible = true;
            }
            iGrid1.Rows.Clear();
            fGrid.Rows.Clear();
            DrawLeftTree();
        }

        private void labelCalender_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            if (labelCalender.Tag == null) { return; }
            string _key = null;
            if (iGrid1.SelectedRows.Count > 0)
            {
                _key = iGrid1.SelectedRows[0].Key;
            }
            DrawLeftTree();
            if (!string.IsNullOrEmpty(_key))
            {
                iGrid1.Focus();
                iGrid1.SetCurCell(_key, 1);
            }
        }

        private void fGrid_SizeChanged(object sender, EventArgs e)
        {
            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            CheckForUpdates();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            
            //timer1.Enabled = false;
            //Application.DoEvents();
            //buttonAdd.Enabled = false;
            //CheckForUpdates();
            //buttonAdd.Enabled = true;
            //timer1.Enabled = true;
          
        }

        private void contextpayments_Opening(object sender, CancelEventArgs e)
        {
            if(fGrid.SelectedRows.Count==0)
            {
                e.Cancel = true;
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

            string _str = "Are You Sure You Want To Delete This Transfer";
            if (!dbm.WarningMessage(_str, "Delete Transfer Warning"))
            {
                return;
            }
            var m_partition = string.Format("{0}{1}", m_YEAR, m_MONTH).ToInt32();
            var _obj = fGrid.SelectedRows[0].Tag as ic.cash_transferC;
            _obj.status = em.cashTransferStatus.deleted;
            if (_obj != null)
            {
                using (var xd = new xing())
                {
                    long _trans_id = xd.ExecuteScalarInt64(string.Format("select transaction_id from acc_cash_transfer_tb where un_id={0} and status={1}", _obj.un_id, em.cashTransferStatus.valid.ToByte()));
                    if (_trans_id > 0)
                    {
                        _str = string.Format("update acc_cash_transfer_tb set status={0},{1},fs_time_stamp={2} where un_id={3} and status={4}", em.cashTransferStatus.deleted.ToByte(), dbm.ETS, SQLH.UnixStamp, _obj.un_id, em.cashTransferStatus.valid.ToByte());
                        xd.UpdateFsTimeStamp("acc_cash_transfer_tb");
                        if (xd.SingleUpdateCommand(_str))
                        {
                             _str = string.Format("select source_id, destination_id, amount from acc_cash_transfer_tb where transaction_id={0}", _trans_id);
                            int _source_id = 0;
                            int _destination_id = 0;
                            int _amount = 0;
                            using (var _dr = xd.SelectCommand(_str))
                            {
                                while (_dr.Read())
                                {
                                    _source_id = _dr["source_id"].ToInt32();
                                    _destination_id = _dr["destination_id"].ToInt32();
                                    _amount = _dr["amount"].ToInt32();
                                }
                                //
                            }
                            #region 
                            if (accn.GetAccountBaseParent(_source_id).search_alias == "WITHDRAWN_CHEQUES")
                            {
                                //cheque stuff;
                                int wdr_id = xd.ExecuteScalarInt(string.Format("select wdr_id from acc_bank_withdraw_tb where sys_account_id={0}", _source_id));
                                if (wdr_id > 0)
                                {
                                    xd.UpdateFsTimeStamp("acc_bank_withdraw_tb");
                                    var _st = string.Format("update acc_bank_withdraw_tb set cheque_balance=(cheque_balance+{0}),{1},fs_time_stamp={2} where wdr_id={3}", _amount,
                                         dbm.ETS, SQLH.UnixStamp, wdr_id);
                                    xd.SingleUpdateCommand(_st);
                                }
                                if (accn.GetAccountBaseParent(_destination_id).search_alias == "WITHDRAWN_CHEQUES")
                                {
                                    xd.UpdateFsTimeStamp("acc_bank_withdraw_tb");
                                    wdr_id = xd.ExecuteScalarInt(string.Format("select wdr_id from acc_bank_withdraw_tb where sys_account_id={0}", _destination_id));
                                    if (wdr_id > 0)
                                    {
                                        xd.UpdateFsTimeStamp("acc_bank_withdraw_tb");
                                        var _st = string.Format("update acc_bank_withdraw_tb set cheque_balance=(cheque_balance-{0}),{1},fs_time_stamp={2} where wdr_id={3}", _amount,
                                             dbm.ETS, SQLH.UnixStamp, wdr_id);
                                        xd.SingleUpdateCommand(_st);
                                    }
                                }

                            }
                            #endregion
                            accn.DeleteJournal(_trans_id, xd);
                            fGrid.Rows.RemoveAt(fGrid.Rows[_obj.un_id.ToString()].Index);
                            var _delete_id = xd.IDCtrlGet("CASH_TRANSFER_DELETE_ID");
                            xd.SingleUpdateCommandALL("acc_cash_transfer_tb", new string[]
                                {
                                    "delete_id",
                                    "delete_fs_date",
                                    "delete_pc_us_id",
                                    "un_id"
                                }, new object[]
                                {
                                
                                    _delete_id,
                                    sdata.CURR_DATE,
                                    sdata.PC_US_ID,
                                    _obj.un_id

                                }, 1);
                            xd.CommitTransaction();
                            
                        }
                    }
                    else
                    {
                        MessageBox.Show("This Transfer Has Already Been Deleted", "Delete Failure");
                    }

                }
                 CheckForUpdates();
            }
         
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
           
            
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            //if (string.Format("{0}{1}", m_YEAR, m_MONTH).ToInt32() == string.Format("{0}{1}", sdata.CURR_DATE.Year, sdata.CURR_DATE.Month).ToInt32())
            //{
            //    try
            //    {
            //        iGrid1.SetCurCell(sdata.CURR_FS.fs_id.ToString(), 1);
            //    }
            //    catch (Exception)
            //    {

            //    }
            //}
            //using (var kd = new MakeExpensePro())
            //{
            //    kd.Owner = this;
            //    kd.ShowDialog();
            //}
           // CheckForUpdates();
         }
        public void ChangeDate(int fs_id)
        {
            try
            {
                iGrid1.SetCurCell(fs_id.ToString(), 1);
                if (!iGrid1.Rows[fs_id.ToString()].Visible) { iGrid1.Rows[fs_id.ToString()].Visible = true; }
            }
            catch (Exception ex)
            {

            }
        }
        private void buttonItem3_Click_1(object sender, EventArgs e)
        {
            Application.DoEvents();
            buttonAdd.Enabled = false;
            CheckForUpdates();
            buttonAdd.Enabled = true;
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening_1(object sender, CancelEventArgs e)
        {
            if (iGrid1.CurCell == null)
            {
                e.Cancel = true;
            }
        }
        private void printTenantReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (iGrid1.CurCell.Row.Level == 0)
            //{
            //    fnn.PrintIGridNormal(fGrid, string.Format("Expenses For {0} {1} :: {2}", datam.MONTHS[m_MONTH], m_YEAR, iGrid1.CurCell.Row.Cells[1].Text), null);
            //}
            //else
            //{
            //    fnn.PrintIGridNormal(fGrid, string.Format("Expenses For {0} {1} {2} :: {3}", iGrid1.CurCell.Row.Cells[0].Text, datam.MONTHS[m_MONTH], m_YEAR, iGrid1.CurCell.Row.Cells[1].Text), null);

            //}
        }

        private void printReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void buttonItem2_Click_1(object sender, EventArgs e)
        {
            if (string.Format("{0}{1}", m_YEAR, m_MONTH).ToInt32() == string.Format("{0}{1}", sdata.CURR_DATE.Year, sdata.CURR_DATE.Month).ToInt32())
            {
                try
                {
                    iGrid1.SetCurCell(sdata.CURR_FS.fs_id.ToString(), 1);
                }
                catch (Exception)
                {

                }
            }
           
        }

        private void fGrid_CellMouseDown(object sender, iGCellMouseDownEventArgs e)
        {
            fGrid.SetCurCell(e.RowIndex, e.ColIndex);
        }

        private void deleteExpenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem2_Click_2(object sender, EventArgs e)
        {
            //if (string.Format("{0}{1}", m_YEAR, m_MONTH).ToInt32() == string.Format("{0}{1}", sdata.CURR_DATE.Year, sdata.CURR_DATE.Month).ToInt32())
            //{
            //    try
            //    {
            //        iGrid1.SetCurCell(sdata.CURR_FS.fs_id.ToString(), 1);
            //    }
            //    catch (Exception)
            //    {

            //    }
            //}
            //using (var kd = new MakeExpenseBulk())
            //{
            //    kd.Owner = this;
            //    kd.ShowDialog();
            //}
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            using(var _fm= new AccountsTransferMaker())
            {
                _fm.Owner = this;
                _fm.ShowDialog();
                if (_fm.Tag != null)
                {
                    CheckForUpdates();
                }
            }
        }

       
       
    }
}
