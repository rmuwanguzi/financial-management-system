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
    public partial class BankReconcManager : DevComponents.DotNetBar.Office2007Form
    {
        public BankReconcManager()
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
            Dictionary<string, string> grid_cols = new Dictionary<string, string>();
            grid_cols.Add("month_stat", "Month\nStatistics");
            grid_cols.Add("reconc_add", string.Empty);
            grid_cols.Add("reconc_deduct", string.Empty);
           //
            foreach (var _grid in _grids)
            {
                foreach(var k in grid_cols)
                {
                    myCol = _grid.Cols.Add(k.Key, k.Value);
                    myCol.IncludeInSelect = true;
                    myCol.SortType = iGSortType.None;
                    myCol.ColHdrStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    myCol.ColHdrStyle.ForeColor = Color.DarkGreen;
                    myCol.ColHdrStyle.BackColor = Color.Thistle;
                    myCol.ColHdrStyle.Font = new Font("verdana", 10, FontStyle.Bold);
                }
                foreach (var k in new string[] { "reconc_add", "reconc_deduct" })
                {
                    _grid.Cols[k].CellStyle.FormatString = "{0:N0}";
                    _grid.Cols[k].CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
                }
                _grid.Cols["reconc_add"].ColHdrStyle.BackColor = Color.LightGreen;
               _grid.Cols["reconc_add"].ColHdrStyle.ImageAlign = iGContentAlignment.MiddleCenter;
                _grid.Cols["reconc_add"].ColHdrStyle.ImageList = imageList1;
                _grid.Cols["reconc_add"].ImageIndex = 0;
                _grid.Cols["reconc_add"].AllowSizing = false;
                _grid.Cols["reconc_add"].Width = 100;

                //
                _grid.Cols["reconc_deduct"].ColHdrStyle.BackColor = Color.Maroon;
                _grid.Cols["reconc_deduct"].ColHdrStyle.ImageAlign = iGContentAlignment.MiddleCenter;
                _grid.Cols["reconc_deduct"].ColHdrStyle.ImageList = imageList1;
                _grid.Cols["reconc_deduct"].ImageIndex = 1;
                _grid.Cols["reconc_deduct"].AllowSizing = false;
                _grid.Cols["reconc_deduct"].Width = 100;


                _grid.Cols["reconc_deduct"].ColHdrStyle.ForeColor = Color.DarkRed;
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
            grid_cols.Add("no", "No");
            grid_cols.Add("date", "Date");
            grid_cols.Add("bank", "Bank Name");
            grid_cols.Add("rc_account", "Account\nName");
            grid_cols.Add("rc_type", "Reconc\nType");
            grid_cols.Add("rc_amount", "Amount");
            grid_cols.Add("desc", "Description");
            grid_cols.Add("pc_us_id", "Entrant");
          
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
              
               _grid.DefaultRow.Height = _grid.GetPreferredRowHeight(true, false);
               _grid.DefaultAutoGroupRow.Height = _grid.DefaultRow.Height;
               _grid.Cols.AutoWidth();
               foreach (var k in new string[] { "rc_amount" })
               {
                   _grid.Cols[k].CellStyle.FormatString = "{0:N0}";
               }
           }
            
            #endregion  
        }
        public void CheckForUpdates()
        {
            m_partition = string.Format("{0}{1}", m_YEAR, m_MONTH).ToInt32();
            if (datam.GetMonthBankReconcilition(m_partition))
            {
               
                var nlist = from n in datam.DATA_MONTH_BANK_RECONCILIATION[m_partition]
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

                            iGrid1.Rows[t.fs_id.ToString()].Cells["reconc_add"].Value = datam.DATA_MONTH_BANK_RECONCILIATION[m_partition].Where(u => u.Value.fs_id == t.fs_id & u.Value.status != em.voucher_statusS.cancelled & u.Value.br_acc_type == em.bank_reconc_typeS.addition).Sum(o => o.Value.amount);
                            iGrid1.Rows[t.fs_id.ToString()].Cells["reconc_deduct"].Value = datam.DATA_MONTH_BANK_RECONCILIATION[m_partition].Where(u => u.Value.fs_id == t.fs_id & u.Value.status != em.voucher_statusS.cancelled & u.Value.br_acc_type == em.bank_reconc_typeS.deduction).Sum(o => o.Value.amount);

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
                    iGrid1.Rows[0].Cells["reconc_add"].Value = datam.DATA_MONTH_BANK_RECONCILIATION[m_partition].Values.Where(j => j.status != em.voucher_statusS.cancelled & j.br_acc_type == em.bank_reconc_typeS.addition).Sum(e => e.amount);
                    iGrid1.Rows[0].Cells["reconc_deduct"].Value = datam.DATA_MONTH_BANK_RECONCILIATION[m_partition].Values.Where(j => j.status != em.voucher_statusS.cancelled & j.br_acc_type == em.bank_reconc_typeS.deduction).Sum(e => e.amount);
                    //
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
                var nlist = from j in datam.DATA_MONTH_BANK_RECONCILIATION[string.Format("{0}{1}", m_YEAR, m_MONTH).ToInt32()]
                            where j.Value.status == em.voucher_statusS.valid
                            select j.Value;
                if (UPDATE_MAIN_GRID)
                {
                    iGrid1.CurRow.Cells["reconc_add"].Value = nlist.Where(f => f.br_acc_type == em.bank_reconc_typeS.addition).Sum(k => k.amount).ToNumberDisplayFormat();
                    iGrid1.CurRow.Cells["reconc_deduct"].Value = nlist.Where(f => f.br_acc_type == em.bank_reconc_typeS.deduction).Sum(k => k.amount).ToNumberDisplayFormat();
                }
                LoadMainGrid(nlist);

            }
            else
            {
                var nlist = from j in datam.DATA_MONTH_BANK_RECONCILIATION[string.Format("{0}{1}", m_YEAR, m_MONTH).ToInt32()]
                            where j.Value.fs_id == iGrid1.CurRow.Key.ToInt32() & j.Value.status == em.voucher_statusS.valid
                            select j.Value;
                if (UPDATE_MAIN_GRID)
                {
                    iGrid1.CurRow.Cells["reconc_add"].Value = nlist.Where(f => f.br_acc_type == em.bank_reconc_typeS.addition).Sum(k => k.amount).ToNumberDisplayFormat();
                    iGrid1.CurRow.Cells["reconc_deduct"].Value = nlist.Where(f => f.br_acc_type == em.bank_reconc_typeS.deduction).Sum(k => k.amount).ToNumberDisplayFormat();
                }
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
            using (var xd = new xing())
            {
                datam.InitBankReconciliation(xd);
                xd.CommitTransaction();
            }
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
            if (!datam.GetMonthBankReconcilition(new_val))
            {
              
            }
            var _days = DateTime.DaysInMonth(m_YEAR, m_MONTH);
            iGRow _row = null;
            #region draw_month header
            _row = iGrid1.Rows.Add();
            _row.Font = new Font("georgia", 13, FontStyle.Regular);
            _row.ForeColor = Color.Blue;
            _row.Cells[0].Value = datam.MONTHS[m_MONTH - 1];
            _row.TreeButton = iGTreeButtonState.Visible;
            _row.Level = 0;
            _row.ReadOnly = iGBool.True;
            _row.Height += 2;
            _row.Key = m_MONTH.ToStringNullable();
            _row.AutoHeight();
            #endregion
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
            if (datam.DATA_MONTH_BANK_RECONCILIATION[new_val].Keys.Count > 0)
            {
                var nlist = from n in datam.DATA_MONTH_BANK_RECONCILIATION[new_val]
                            where n.Value.status == em.voucher_statusS.valid
                            orderby n.Value.fs_id descending
                            group n by n.Value.fs_id
                                into new_group
                                select new
                                {
                                    fs_id = new_group.Key,
                                    total_deduct = new_group.Where(y=>y.Value.br_acc_type==em.bank_reconc_typeS.deduction).Sum(h=>h.Value.amount),
                                    total_addition = new_group.Where(y => y.Value.br_acc_type == em.bank_reconc_typeS.addition).Sum(h => h.Value.amount),
                                };
                if (nlist.Count() > 0)
                {
                    iGrid1.Rows[m_MONTH.ToString()].Cells["reconc_add"].Value = nlist.Sum(k => k.total_addition);
                    iGrid1.Rows[m_MONTH.ToString()].Cells["reconc_deduct"].Value = nlist.Sum(k => k.total_deduct);
                    if ((nlist.Max(j => j.fs_id) != sdata.CURR_FS.fs_id) & new_val == string.Format("{0}{1}", sdata.CURR_DATE.Year, sdata.CURR_DATE.Month).ToInt32())
                    {
                        iGrid1.Rows[sdata.CURR_FS.fs_id.ToString()].Cells["reconc_add"].Value = null;
                        iGrid1.Rows[sdata.CURR_FS.fs_id.ToString()].Cells["reconc_deduct"].Value = null;
                        iGrid1.Rows[sdata.CURR_FS.fs_id.ToString()].Visible = true;
                    }
                    foreach (var d in nlist)
                    {
                        iGrid1.Rows[d.fs_id.ToString()].Cells["reconc_add"].Value = d.total_addition;
                        iGrid1.Rows[d.fs_id.ToString()].Cells["reconc_deduct"].Value = d.total_deduct;
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
                iGrid1.Rows[m_MONTH.ToString()].Cells["reconc_add"].Value = null;
                iGrid1.Rows[m_MONTH.ToString()].Cells["reconc_deduct"].Value = null;
                if (m_YEAR == sdata.CURR_DATE.Year & m_MONTH == sdata.CURR_DATE.Month)
                {
                    iGrid1.Rows[sdata.CURR_FS.fs_id.ToString()].Cells["reconc_add"].Value = null;
                    iGrid1.Rows[sdata.CURR_FS.fs_id.ToString()].Cells["reconc_deduct"].Value = null;
                    iGrid1.Rows[sdata.CURR_FS.fs_id.ToString()].Visible = true;
                }
            }
            iGrid1.EndUpdate();
            iGrid1.Cols.AutoWidth();
            iGrid1.AutoResizeCols = false;
            
        }
        private void LoadMainGrid(IEnumerable<ic.bank_reconc_transC> _list)
        {
            fGrid.BeginUpdate();
            fGrid.Rows.Clear();
            if (_list == null) { fGrid.EndUpdate(); return; }
            iGRow _row = null;
          
           
                foreach (var n in _list)
                {
                    _row = fGrid.Rows.Add();
                    _row.ReadOnly = iGBool.True;
                    _row.Font = new Font("georgia", 12, FontStyle.Regular);
                    _row.Cells["no"].Value = fGrid.Rows.Count;
                    _row.Cells["date"].Value = n.fs_date.ToMyShortDate();
                    _row.Cells["rc_type"].Value = n.br_acc_type == em.bank_reconc_typeS.addition ? "+" : "-";
                    _row.Cells["rc_type"].TextAlign = iGContentAlignment.MiddleCenter;
                    _row.Cells["rc_amount"].Value = n.amount;
                    _row.Cells["rc_amount"].TextAlign = iGContentAlignment.MiddleCenter;
                    _row.Cells["bank"].Value = n.objBankAccount.BankName;
                
                    //
                    _row.Cells["rc_account"].Value = n.objReconcAccount.br_acc_name;
                    _row.Cells["rc_account"].ForeColor = Color.DarkBlue;
                    _row.Cells["desc"].Value = n.desc;
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

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

            string _str = "Are You Sure You Want To Delete This Entry";
            if (!dbm.WarningMessage(_str, "Delete Entry Warning"))
            {
                return;
            }
            var m_partition = string.Format("{0}{1}", m_YEAR, m_MONTH).ToInt32();
            var _obj = fGrid.SelectedRows[0].Tag as ic.bank_reconc_transC;
            if (_obj != null)
            {
                using (var xd = new xing())
                {
                    long _trans_id = xd.ExecuteScalarInt64(string.Format("select transaction_id from acc_foreign_exchange_tb where un_id={0} and status={1}", _obj.un_id, em.voucher_statusS.valid.ToByte()));
                    if (_trans_id > 0)
                    {
                        accn.DeleteJournal(_trans_id, xd);
                        //
                        xd.SingleUpdateCommandALL("acc_bank_reconc_trans_tb",
                               new string[]
                            {
                                "status",
                                "un_id",
                            }, new object[]
                            {
                                em.voucher_statusS.cancelled.ToByte(),
                                _obj.un_id
                            }, 1);
                        //
                        //fnn.ExecuteDeleteBase(xd, new ic.deleteBaseC()
                        //{
                        //    del_fs_date = sdata.CURR_DATE,
                        //    del_fs_id = sdata.CURR_FS.fs_id,
                        //    del_fs_time = DateTime.Now.ToShortTimeString(),
                        //    del_pc_us_id = sdata.PC_US_ID
                        //}, "acc_foreign_exchange_tb", "un_id", _obj.un_id);
                        //
                        xd.CommitTransaction();
                        fGrid.Rows.RemoveAt(fGrid.Rows[_obj.un_id.ToString()].Index);

                    }
                    else
                    {
                        MessageBox.Show("This Record Has Already Been Cancelled", "Cancel Failure");
                    }
                }
                 CheckForUpdates();
            }

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (sdata.CURRENT_MENU == null)
            {
                return;
            }
            if (!sdata.CURRENT_MENU.ContainsRight(2))
            {
                MessageBox.Show("You Are Not Authorized To Print Receipts", "Security Control");
                return;
            }
            string _str = "Are You Sure You Want To Print This Receipt";
            if (!dbm.WarningMessage(_str, "Print Receipt Warning"))
            {
                return;
            }
            
        }

        private void buttonItem1_Click(object sender, EventArgs e)
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
            using (var kd = new BankReconciliationTransMaker())
            {
                kd.Owner = this;
                kd.ShowDialog();
            }
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
            if (iGrid1.CurCell.Row.Level == 0)
            {
                fnn.PrintIGridNormal(fGrid, string.Format("Expenses For {0} {1} :: {2}", datam.MONTHS[m_MONTH], m_YEAR, iGrid1.CurCell.Row.Cells[1].Text), null);
            }
            else
            {
                fnn.PrintIGridNormal(fGrid, string.Format("Expenses For {0} {1} {2} :: {3}", iGrid1.CurCell.Row.Cells[0].Text, datam.MONTHS[m_MONTH], m_YEAR, iGrid1.CurCell.Row.Cells[1].Text), null);

            }
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
            using (var kd = new MakeExpenseBulk())
            {
                kd.Owner = this;
                kd.ShowDialog();
            }
        }
       
    }
}
