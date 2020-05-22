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
    public partial class DailyExpensesManager : DevComponents.DotNetBar.Office2007Form
    {
        public DailyExpensesManager()
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
                  
                }
                //
                myCol = _grid.Cols.Add("total", "Total\nExpenditure");
                myCol.CellStyle.FormatString = "{0:N0}";
               
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
            grid_cols.Add("sys_no", "Sys-No");
            grid_cols.Add("voucher_no", "Voucher No");
            grid_cols.Add("expense", "Account Name");
            grid_cols.Add("expense_amount", "Amount");
            grid_cols.Add("details", "details");
            grid_cols.Add("pay_mode", "Pay Mode");
            grid_cols.Add("dept", "Department");
            grid_cols.Add("source", "Source");
          
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
                _grid.Cols["sys_no"].ColHdrStyle.ForeColor = Color.Maroon;
                _grid.Cols["sys_no"].CellStyle.ForeColor = Color.DarkGray;
                _grid.Cols["sys_no"].Visible = false;
                _grid.Cols["expense_amount"].CellStyle.FormatString = "{0:N0}";
                _grid.DefaultRow.Height = _grid.GetPreferredRowHeight(true, false);
                _grid.DefaultAutoGroupRow.Height = _grid.DefaultRow.Height;
                _grid.Cols.AutoWidth();
            }

            #endregion  
        }
        public void CheckForUpdates(bool reloadGrid)
        {
            m_partition = string.Format("{0}{1}", m_YEAR, m_MONTH).ToInt32();
            if (datam.GetMonthExpenses(m_partition))
            {
               
                var nlist = from n in datam.DATA_MONTH_EXPENSES[m_partition]
                            where n.Value.is_updated 
                            group n.Value by n.Value.exp_fs_id into new_group
                            select new
                            {
                                pay_fs_id=new_group.Key,
                                group_val=new_group
                            };
                if (nlist.Count() > 0)
                {
                    string _prev_key = null;
                    if (iGrid1.CurCell != null)
                    {
                        _prev_key = iGrid1.CurCell.Row.Key;
                    }
                    if (reloadGrid)
                    {
                        iGrid1.CurCell = null;
                        fGrid.Rows.Clear();
                    }
                    foreach (var t in nlist)
                    {
                        try
                        {
                            iGrid1.Rows[t.pay_fs_id.ToString()].Cells["total"].Value = datam.DATA_MONTH_EXPENSES[m_partition].Where(u => u.Value.exp_fs_id == t.pay_fs_id & u.Value.voucher_status != em.voucher_statusS.cancelled).Sum(o => o.Value.exp_amount);
                            iGrid1.Rows[t.pay_fs_id.ToString()].Cells["payments"].Value = datam.DATA_MONTH_EXPENSES[m_partition].Where(u => u.Value.exp_fs_id == t.pay_fs_id & u.Value.voucher_status != em.voucher_statusS.cancelled & !u.Value.objExpenseAccount.isActualExpense).Sum(o => o.Value.exp_amount);
                            iGrid1.Rows[t.pay_fs_id.ToString()].Cells["expenses"].Value = datam.DATA_MONTH_EXPENSES[m_partition].Where(u => u.Value.exp_fs_id == t.pay_fs_id & u.Value.voucher_status != em.voucher_statusS.cancelled & u.Value.objExpenseAccount.isActualExpense).Sum(o => o.Value.exp_amount);
                            if (!iGrid1.Rows[t.pay_fs_id.ToString()].Visible)
                            {
                                iGrid1.Rows[t.pay_fs_id.ToString()].Visible = true;
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }

                    }
                    iGrid1.Rows[0].Cells["total"].Value = datam.DATA_MONTH_EXPENSES[m_partition].Values.Where(j => j.voucher_status != em.voucher_statusS.cancelled).Sum(e => e.exp_amount);
                    iGrid1.Rows[0].Cells["payments"].Value = datam.DATA_MONTH_EXPENSES[m_partition].Values.Where(j => j.voucher_status != em.voucher_statusS.cancelled & !j.objExpenseAccount.isActualExpense).Sum(e => e.exp_amount);
                    iGrid1.Rows[0].Cells["expenses"].Value = datam.DATA_MONTH_EXPENSES[m_partition].Values.Where(j => j.voucher_status != em.voucher_statusS.cancelled & j.objExpenseAccount.isActualExpense).Sum(e => e.exp_amount);
                    iGrid1.Cols.AutoWidth();
                    iGrid1.AutoResizeCols = false;
                    if (reloadGrid)
                    {
                        if (!string.IsNullOrEmpty(_prev_key))
                        {
                            iGrid1.SetCurCell(_prev_key, 1);
                        }
                    }
                }
            }
        }
        public void CheckForUpdates()
        {
            m_partition = string.Format("{0}{1}", m_YEAR, m_MONTH).ToInt32();
            if (datam.GetMonthExpenses(m_partition))
            {

                var nlist = from n in datam.DATA_MONTH_EXPENSES[m_partition]
                            where n.Value.is_updated
                            group n.Value by n.Value.exp_fs_id into new_group
                            select new
                            {
                                pay_fs_id = new_group.Key,
                                group_val = new_group
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
                            iGrid1.Rows[t.pay_fs_id.ToString()].Cells["total"].Value = datam.DATA_MONTH_EXPENSES[m_partition].Where(u => u.Value.exp_fs_id == t.pay_fs_id & u.Value.voucher_status != em.voucher_statusS.cancelled).Sum(o => o.Value.exp_amount);
                            iGrid1.Rows[t.pay_fs_id.ToString()].Cells["payments"].Value = datam.DATA_MONTH_EXPENSES[m_partition].Where(u => u.Value.exp_fs_id == t.pay_fs_id & u.Value.voucher_status != em.voucher_statusS.cancelled & !u.Value.objExpenseAccount.isActualExpense).Sum(o => o.Value.exp_amount);
                            iGrid1.Rows[t.pay_fs_id.ToString()].Cells["expenses"].Value = datam.DATA_MONTH_EXPENSES[m_partition].Where(u => u.Value.exp_fs_id == t.pay_fs_id & u.Value.voucher_status != em.voucher_statusS.cancelled & u.Value.objExpenseAccount.isActualExpense).Sum(o => o.Value.exp_amount);
                            if (!iGrid1.Rows[t.pay_fs_id.ToString()].Visible)
                            {
                                iGrid1.Rows[t.pay_fs_id.ToString()].Visible = true;
                            }
                        }
                        catch (Exception)
                        {
                            continue;
                        }

                    }
                    iGrid1.Rows[0].Cells["total"].Value = datam.DATA_MONTH_EXPENSES[m_partition].Values.Where(j => j.voucher_status != em.voucher_statusS.cancelled).Sum(e => e.exp_amount);
                    iGrid1.Rows[0].Cells["payments"].Value = datam.DATA_MONTH_EXPENSES[m_partition].Values.Where(j => j.voucher_status != em.voucher_statusS.cancelled & !j.objExpenseAccount.isActualExpense).Sum(e => e.exp_amount);
                    iGrid1.Rows[0].Cells["expenses"].Value = datam.DATA_MONTH_EXPENSES[m_partition].Values.Where(j => j.voucher_status != em.voucher_statusS.cancelled & j.objExpenseAccount.isActualExpense).Sum(e => e.exp_amount);
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
                var nlist = from j in datam.DATA_MONTH_EXPENSES[string.Format("{0}{1}", m_YEAR, m_MONTH).ToInt32()]
                            where j.Value.voucher_status != em.voucher_statusS.cancelled
                            select j.Value;
                //if (UPDATE_MAIN_GRID)
                //{
                //    iGrid1.CurRow.Cells[1].Value = nlist.Sum(k => k.exp_amount).ToNumberDisplayFormat();
                //}
                LoadMainGrid(nlist);

            }
            else
            {
                var nlist = from j in datam.DATA_MONTH_EXPENSES[string.Format("{0}{1}", m_YEAR, m_MONTH).ToInt32()]
                            where j.Value.exp_fs_id == iGrid1.CurRow.Key.ToInt32() & j.Value.voucher_status != em.voucher_statusS.cancelled
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
            if (!datam.GetMonthExpenses(new_val))
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
                _row.Cells["payments"].ForeColor = Color.FromArgb(64, 64, 64);
                _row.Cells["expenses"].ForeColor = Color.FromArgb(64, 64, 64);
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
            if (datam.DATA_MONTH_EXPENSES[new_val].Keys.Count > 0)
            {
                var nlist = from n in datam.DATA_MONTH_EXPENSES[new_val]
                            where n.Value.voucher_status==em.voucher_statusS.valid
                            orderby n.Value.exp_fs_id descending
                            group n by n.Value.exp_fs_id
                                into new_group
                                select new
                                {
                                    fs_id = new_group.Key,
                                    total_entries = new_group.Count(),
                                    item_obj = new_group.FirstOrDefault(),
                                    expenses_amount=new_group.Where(p=>p.Value.objExpenseAccount.isActualExpense).Sum(k=>k.Value.exp_amount),
                                    payments_amount=new_group.Where(p=>!p.Value.objExpenseAccount.isActualExpense).Sum(k=>k.Value.exp_amount),
                                    total_amount = new_group.Sum(k => k.Value.exp_amount)
                                };
                if (nlist.Count() > 0)
                {
                    iGrid1.Rows[m_MONTH.ToString()].Cells["total"].Value = nlist.Sum(k => k.total_amount);
                    iGrid1.Rows[m_MONTH.ToString()].Cells["payments"].Value = nlist.Sum(k => k.payments_amount);
                    iGrid1.Rows[m_MONTH.ToString()].Cells["expenses"].Value = nlist.Sum(k => k.expenses_amount);
                    if ((nlist.Max(j => j.fs_id) != sdata.CURR_FS.fs_id) & new_val == string.Format("{0}{1}", sdata.CURR_DATE.Year, sdata.CURR_DATE.Month).ToInt32())
                    {
                        iGrid1.Rows[sdata.CURR_FS.fs_id.ToString()].Cells[1].Value = null;
                        iGrid1.Rows[sdata.CURR_FS.fs_id.ToString()].Cells[2].Value = null;
                        iGrid1.Rows[sdata.CURR_FS.fs_id.ToString()].Cells[3].Value = null;
                        iGrid1.Rows[sdata.CURR_FS.fs_id.ToString()].Visible = true;
                    }
                    foreach (var d in nlist)
                    {
                        iGrid1.Rows[d.fs_id.ToString()].Cells["total"].Value = d.total_amount.ToNumberDisplayFormat();
                        iGrid1.Rows[d.fs_id.ToString()].Cells["expenses"].Value =d.expenses_amount.ToNumberDisplayFormat();
                        iGrid1.Rows[d.fs_id.ToString()].Cells["payments"].Value = d.payments_amount.ToNumberDisplayFormat();
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
                iGrid1.Rows[m_MONTH.ToString()].Cells[1].Value = null;
                iGrid1.Rows[m_MONTH.ToString()].Cells[2].Value = null;
                iGrid1.Rows[m_MONTH.ToString()].Cells[3].Value = null;
                if (m_YEAR == sdata.CURR_DATE.Year & m_MONTH == sdata.CURR_DATE.Month)
                {
                    iGrid1.Rows[sdata.CURR_FS.fs_id.ToString()].Cells[1].Value = null;
                    iGrid1.Rows[sdata.CURR_FS.fs_id.ToString()].Cells[2].Value = null;
                    iGrid1.Rows[sdata.CURR_FS.fs_id.ToString()].Cells[3].Value = null;
                    iGrid1.Rows[sdata.CURR_FS.fs_id.ToString()].Visible = true;
                }
            }
          
            iGrid1.Cols.AutoWidth();
            iGrid1.AutoResizeCols = false;
            iGrid1.EndUpdate();
            
        }
        private void LoadMainGrid(IEnumerable<ic.expense_transC> _list)
        {
            fGrid.BeginUpdate();
            fGrid.Rows.Clear();
            if (_list == null) { fGrid.EndUpdate(); return; }
            iGRow _row = null;

            //using (var xd = new xing())
            //{
            var _vlist = (from k in _list
                          group k by k.voucher_id into new_gp
                          select new
                          {
                              voucher_id = new_gp.Key,
                              voucher_count = new_gp.Count(),
                              voucher_data = new_gp,
                              voucher_amount = new_gp.Sum(j => j.exp_amount),
                              voucher_no = new_gp.FirstOrDefault().voucher_no,
                              first_obj = new_gp.FirstOrDefault()
                          }).OrderBy(g => g.voucher_count).ToList();
            bool is_child_row = false;
            iGRow _parent_row = null;
            foreach(var _v in _vlist)
            {
                is_child_row = false;
                _parent_row = null;
                if (_v.voucher_count > 1)
                {
                    _row = fGrid.Rows.Add();
                    _row.ReadOnly = iGBool.True;
                    _row.Font = new Font("georgia", 12, FontStyle.Italic| FontStyle.Bold);
                    _row.Cells["sys_no"].Value = "V-" + _v.voucher_id;
                    _row.Cells["voucher_no"].Value = _v.voucher_no;
                    _row.Cells["voucher_no"].AuxValue = 1;
                    _row.Cells["expense_amount"].Value = _v.voucher_amount;
                    _row.ForeColor = Color.Maroon;
                    _row.BackColor = Color.GhostWhite;
                    _row.Key = string.Format("gp{0}", _v.first_obj.voucher_id.ToString());
                    _row.Tag = _v.voucher_id;
                    _row.Level = 0;
                    _row.TreeButton = iGTreeButtonState.Visible;
                    is_child_row = true;
                    _parent_row = _row;
                    _row.AutoHeight();

                }
                foreach(var n in _v.voucher_data)
                {
                    _row = fGrid.Rows.Add();
                    if(is_child_row)
                    {
                        _row.Level = 1;
                        _row.TreeButton = iGTreeButtonState.Hidden;
                    }
                    else
                    {
                        _row.Level = 0;
                        _row.TreeButton = iGTreeButtonState.Absent;
                    }
                    _row.ReadOnly = iGBool.True;
                    _row.Font = new Font("georgia", 12, FontStyle.Regular);
                    _row.Cells["sys_no"].Value = "V-" + n.voucher_id;
                    _row.Cells["voucher_no"].Value = n.voucher_no;
                    _row.Cells["voucher_no"].AuxValue = 3;
                    if (!is_child_row)
                    {
                        _row.Cells["voucher_no"].Font = new Font("georgia", 12, FontStyle.Italic | FontStyle.Bold);
                        _row.Cells["voucher_no"].Value = string.Format("   {0}", n.voucher_no);
                        _row.Cells["voucher_no"].AuxValue = 2;
                    }
                    _row.Cells["expense_amount"].Value = n.exp_amount;
                    _row.Cells["expense_amount"].TextAlign = iGContentAlignment.MiddleCenter;
                    _row.Cells["expense_amount"].ForeColor = Color.DarkBlue;
                    _row.Cells["expense"].Value = n.objExpenseAccount.exp_acc_name;
                    _row.Cells["details"].Value = n.exp_details;
                    _row.Cells["dept"].Value = datam.DATA_DEPARTMENT[n.dept_id].dept_name;
                    _row.Cells["pay_mode"].Value = fnn.GetExpensePayModeString(n.pay_mode);
                    switch (n.source_type)
                    {
                        case em.exp_inc_src_typeS.bank:
                            {
                                _row.Cells["source"].Value = "Bank";
                                break;
                            }
                        case em.exp_inc_src_typeS.petty_cash_cheque:
                            {
                                _row.Cells["source"].Value = "Cheque";

                                break;
                            }
                        case em.exp_inc_src_typeS.unbanked_cash:
                            {
                                _row.Cells["source"].Value = "Source";
                                _row.ForeColor = Color.Red;
                                break;
                            }
                    }

                    _row.Key = n.un_id.ToStringNullable();
                    _row.Tag = n;
                    _row.AutoHeight();

                    n.is_updated = false;
                }
                if(_parent_row!=null)
                {
                    _parent_row.Expanded = false;
                }

            }
                //foreach (var n in _list)
                //{
                //    _row = fGrid.Rows.Add();
                //    _row.ReadOnly = iGBool.True;
                //    _row.Font = new Font("georgia", 12, FontStyle.Regular);
                //    _row.Cells["sys_no"].Value = "V-" + n.voucher_id;
                //    _row.Cells["voucher_no"].Value = n.voucher_no;
                //    _row.Cells["expense_amount"].Value = n.exp_amount;
                //    _row.Cells["expense_amount"].TextAlign = iGContentAlignment.MiddleCenter;
                //    _row.Cells["expense_amount"].ForeColor = Color.DarkBlue;
                //    _row.Cells["expense"].Value = n.objExpenseAccount.exp_acc_name;
                //    _row.Cells["details"].Value = n.exp_details;
                //    _row.Cells["dept"].Value = datam.DATA_DEPARTMENT[n.dept_id].dept_name;
                //    _row.Cells["pay_mode"].Value = fnn.GetExpensePayModeString(n.pay_mode);
                //    switch(n.source_type)
                //    {
                //        case em.exp_inc_src_typeS.bank:
                //            {
                //                _row.Cells["source"].Value = "Bank";
                //                break;
                //            }
                //        case em.exp_inc_src_typeS.petty_cash_cheque:
                //            {
                //                _row.Cells["source"].Value = "Cheque";
                               
                //                break;
                //            }
                //        case em.exp_inc_src_typeS.unbanked_cash:
                //            {
                //                _row.Cells["source"].Value = "Source";
                //                _row.ForeColor = Color.Red;
                //                break;
                //            }
                //    }
                   
                //    _row.Key = n.un_id.ToStringNullable();
                //    _row.Tag = n;
                //    _row.AutoHeight();
                
                //    n.is_updated = false;
                //}
            //}
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
            iGRow _row = fGrid.SelectedRows[0];
            if(_row!=null)
            {
                switch(_row.Cells["voucher_no"].AuxValue.ToInt32())
                {
                    case 1:
                        {
                            toolStripdeleteExpense.Visible = false;
                            toolStripDeleteBulkExpense.Visible = true;
                            deleteSubExpenseToolStripMenuItem.Visible = false;
                            addExpenseToolStripMenuItem.Visible = true;
                            break;
                        }
                    case 2:
                        {
                            toolStripdeleteExpense.Visible = true;
                            toolStripDeleteBulkExpense.Visible = false;
                            deleteSubExpenseToolStripMenuItem.Visible = false;
                            addExpenseToolStripMenuItem.Visible = false;
                            break;
                        }
                    case 3:
                        {
                            toolStripdeleteExpense.Visible = false;
                            toolStripDeleteBulkExpense.Visible = false;
                            deleteSubExpenseToolStripMenuItem.Visible = true;
                            addExpenseToolStripMenuItem.Visible = false;
                            break;
                        }
                }
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
           
          
            var  m_partition = string.Format("{0}{1}", m_YEAR, m_MONTH).ToInt32();
            var _obj = fGrid.SelectedRows[0].Tag as ic.expense_transC;
            var nlist = from k in datam.DATA_MONTH_EXPENSES[m_partition].Values
                        where k.voucher_id == _obj.voucher_id & k.voucher_status == em.voucher_statusS.valid
                        select k;
            if (_obj != null)
            {
                using (var xd = new xing())
                {
                    long _trans_id = xd.ExecuteScalarInt64(string.Format("select transaction_id from acc_expense_trans_tb where un_id={0} and voucher_status={1}", _obj.un_id, em.voucher_statusS.valid.ToByte()));
                    if (_trans_id> 0)
                    {
                        string _str = "Are You Sure You Want To Delete This Voucher";
                        if (!dbm.WarningMessage(_str, "Delete Voucher Warning"))
                        {
                            return;
                        }
                        datam.ShowWaitForm("Deleting Record Please Wait..");
                        Application.DoEvents();
                        _str = string.Format("update acc_expense_vouchers_tb set status={0},{1},fs_time_stamp={2} where voucher_id={3} and status={4}", em.voucher_statusS.cancelled.ToByte(), dbm.ETS, SQLH.UnixStamp, _obj.voucher_id, em.voucher_statusS.valid.ToByte());
                        xd.UpdateFsTimeStamp("acc_expense_vouchers_tb");
                        if (xd.SingleUpdateCommand(_str))
                        {
                            _str = string.Format("select * from journal_tb where transaction_id in ({0})",
                                string.Format("select transaction_id from acc_expense_trans_tb where un_id={0} and voucher_status={1}", _obj.un_id, em.voucher_statusS.valid.ToByte()));
                            accn.DeleteJournal(_str, xd);
                            //
                            foreach (var _expense in nlist)
                            {
                                if (_expense.source_type == em.exp_inc_src_typeS.petty_cash_cheque)
                                {
                                    if (!string.IsNullOrEmpty(_expense.w_dr_data))
                                    {

                                    }
                                }
                                if (!string.IsNullOrEmpty(_expense.w_dr_data))
                                {
                                    var _cheques = _expense.w_dr_data.Split(new char[] { ',' });
                                    xd.UpdateFsTimeStamp("acc_bank_withdraw_tb");
                                    string _st = null;
                                    foreach (var t in _cheques)
                                    {
                                        
                                        var _val = t.Split(new char[] { ':' });
                                        if (xd.ExecuteScalarInt(string.Format("select transfer_id from acc_bank_withdraw_tb where wdr_id={0}", _val[0].ToInt32())) > 0)
                                        {
                                            MessageBox.Show("You Have Already Closed This Cheque, This Operation Cannot Be Carried Out", "Closed Cheque Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            xd.RollBackTransaction();
                                            return;
                                        }

                                        _st = string.Format("update acc_bank_withdraw_tb set cheque_balance=(cheque_balance+{0}),{1},fs_time_stamp={2} where wdr_id={3}", _val[1],
                                         dbm.ETS, SQLH.UnixStamp, _val[0]);
                                        xd.InsertUpdateDelete(_st);
                                    }
                                }

                                //
                                _expense.voucher_status = em.voucher_statusS.cancelled;
                                xd.SingleUpdateCommandALL("acc_expense_trans_tb", new string[]
                     {
                         "voucher_status",
                         "un_id"
                     }, new object[]
                     {
                         em.voucher_statusS.cancelled.ToByte(),
                         _expense.un_id
                     }, 1);
                     //
                                xd.SingleUpdateCommandALL("acc_expense_trans_child_tb", new string[]
                     {
                         "voucher_status",
                         "trans_id"
                     }, new object[]
                     {
                         em.voucher_statusS.cancelled.ToByte(),
                         _expense.un_id
                     }, 1);
                                //
                                fnn.ExecuteDeleteBase(xd, new ic.deleteBaseC()
                                {
                                    del_fs_date = sdata.CURR_DATE,
                                    del_fs_id = sdata.CURR_FS.fs_id,
                                    del_fs_time = DateTime.Now.ToShortTimeString(),
                                    del_pc_us_id = sdata.PC_US_ID
                                }, "acc_expense_trans_tb", "un_id", _expense.un_id);
                                //
                                if (!string.IsNullOrEmpty(_expense.cheque_no))
                                {
                                    ic.bankAccountC _bank = datam.DATA_BANK_ACCOUNTS.Values.Where(y => y.sys_account_id == _expense.source_account_id).FirstOrDefault();
                                    xd.SingleUpdateCommandALL("acc_usedbank_cheques_tb", new string[]
                           {
                               "status",
                               "bank_account_id",
                               "cheque_no"
                           }, new object[]
                           {
                               1,
                               _bank.un_id,
                               _expense.cheque_no
                           }, 2);

                                }
                                fGrid.Rows.RemoveAt(fGrid.Rows[_expense.un_id.ToString()].Index);
                            }

                            xd.CommitTransaction();
                        }
                        datam.HideWaitForm();
                    }
                    else
                    {
                        MessageBox.Show("This Expense Record Has Already Been Deleted", "Delete Failure");
                    }
                   
                }
               
                CheckForUpdates(false);
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
            using (var kd = new MakeExpensePro())
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

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            using(var _fm= new PayCreditor())
            {
                _fm.Owner = this;
                _fm.ShowDialog();
            }
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            using (var _fm = new MakeExpense())
            {
                _fm.Owner = this;
                _fm.ShowDialog();
            }
        }

        private void fGrid_AfterRowStateChanged(object sender, iGAfterRowStateChangedEventArgs e)
        {
            fGrid.EndUpdate();
            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
        }

        private void toolStripDeleteBulkExpense_Click(object sender, EventArgs e)
        {
           
            iGRow _row = fGrid.SelectedRows[0];
            var m_partition = string.Format("{0}{1}", m_YEAR, m_MONTH).ToInt32();
            var _obj = (from k in datam.DATA_MONTH_EXPENSES[m_partition].Values
                       where k.voucher_id == _row.Tag.ToInt32() & k.voucher_status == em.voucher_statusS.valid
                       select k).FirstOrDefault();
            if (_obj == null)
            {
                fGrid.Rows.RemoveAt(fGrid.Rows[_row.Key].Index);
                return;
            }
            
            var nlist = from k in datam.DATA_MONTH_EXPENSES[m_partition].Values
                        where k.voucher_id == _obj.voucher_id & k.voucher_status == em.voucher_statusS.valid
                        select k;
            if (_obj != null)
            {
                using (var xd = new xing())
                {
                    long _trans_id = xd.ExecuteScalarInt64(string.Format("select transaction_id from acc_expense_trans_tb where un_id={0} and voucher_status={1}", _obj.un_id, em.voucher_statusS.valid.ToByte()));
                    if (_trans_id > 0)
                    {
                        string _str = "Are You Sure You Want To Delete This Bulk Voucher";
                        if (!dbm.WarningMessage(_str, "Delete Voucher Warning"))
                        {
                            return;
                        }
                        datam.ShowWaitForm("Deleting Records Please Wait..");
                        Application.DoEvents();
                        _str = string.Format("update acc_expense_vouchers_tb set status={0},{1},fs_time_stamp={2} where voucher_id={3} and status={4}", em.voucher_statusS.cancelled.ToByte(), dbm.ETS, SQLH.UnixStamp, _obj.voucher_id, em.voucher_statusS.valid.ToByte());
                        xd.UpdateFsTimeStamp("acc_expense_vouchers_tb");
                        if (xd.SingleUpdateCommand(_str))
                        {
                            _str = string.Format("select * from journal_tb where transaction_id in ({0})",
                                string.Format("select transaction_id from acc_expense_trans_tb where un_id={0} and voucher_status={1}", _obj.un_id, em.voucher_statusS.valid.ToByte()));
                            accn.DeleteJournal(_str, xd);
                            //
                            foreach (var _expense in nlist)
                            {
                                if (_expense.source_type == em.exp_inc_src_typeS.petty_cash_cheque)
                                {
                                    if (!string.IsNullOrEmpty(_expense.w_dr_data))
                                    {

                                    }
                                }
                                if (!string.IsNullOrEmpty(_expense.w_dr_data))
                                {
                                    var _cheques = _expense.w_dr_data.Split(new char[] { ',' });
                                    xd.UpdateFsTimeStamp("acc_bank_withdraw_tb");
                                    string _st = null;
                                    foreach (var t in _cheques)
                                    {

                                        var _val = t.Split(new char[] { ':' });
                                        if (xd.ExecuteScalarInt(string.Format("select transfer_id from acc_bank_withdraw_tb where wdr_id={0}", _val[0].ToInt32())) > 0)
                                        {
                                            MessageBox.Show("You Have Already Closed This Cheque, This Operation Cannot Be Carried Out", "Closed Cheque Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            xd.RollBackTransaction();
                                            return;
                                        }

                                        _st = string.Format("update acc_bank_withdraw_tb set cheque_balance=(cheque_balance+{0}),{1},fs_time_stamp={2} where wdr_id={3}", _val[1],
                                         dbm.ETS, SQLH.UnixStamp, _val[0]);
                                        xd.InsertUpdateDelete(_st);
                                    }
                                }

                                //
                                _expense.voucher_status = em.voucher_statusS.cancelled;
                                xd.SingleUpdateCommandALL("acc_expense_trans_tb", new string[]
                     {
                         "voucher_status",
                         "un_id"
                     }, new object[]
                     {
                         em.voucher_statusS.cancelled.ToByte(),
                         _expense.un_id
                     }, 1);
                                //
                     xd.SingleUpdateCommandALL("acc_expense_trans_child_tb", new string[]
                     {
                         "voucher_status",
                         "trans_id"
                     }, new object[]
                     {
                         em.voucher_statusS.cancelled.ToByte(),
                         _expense.un_id
                     }, 1);
                                //
                                fnn.ExecuteDeleteBase(xd, new ic.deleteBaseC()
                                {
                                    del_fs_date = sdata.CURR_DATE,
                                    del_fs_id = sdata.CURR_FS.fs_id,
                                    del_fs_time = DateTime.Now.ToShortTimeString(),
                                    del_pc_us_id = sdata.PC_US_ID
                                }, "acc_expense_trans_tb", "un_id", _expense.un_id);
                                //
                                if (!string.IsNullOrEmpty(_expense.cheque_no))
                                {
                                    ic.bankAccountC _bank = datam.DATA_BANK_ACCOUNTS.Values.Where(y => y.sys_account_id == _expense.source_account_id).FirstOrDefault();
                                    xd.SingleUpdateCommandALL("acc_usedbank_cheques_tb", new string[]
                           {
                               "status",
                               "bank_account_id",
                               "cheque_no"
                           }, new object[]
                           {
                               1,
                               _bank.un_id,
                               _expense.cheque_no
                           }, 2);

                                }
                                fGrid.Rows.RemoveAt(fGrid.Rows[_expense.un_id.ToString()].Index);
                            }

                            xd.CommitTransaction();
                            fGrid.Rows.RemoveAt(fGrid.Rows[_row.Key].Index);
                        }

                        datam.HideWaitForm();

                    }
                    else
                    {
                        MessageBox.Show("This Expense Record Has Already Been Deleted", "Delete Failure");
                    }

                }

                CheckForUpdates(false);
            }
        }

        private void deleteSubExpenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iGRow _row = fGrid.SelectedRows[0];
            var _obj = _row.Tag as ic.expense_transC;
            using (var xd = new xing())
            {
                if (xd.ExecuteScalarInt(string.Format("select voucher_status from acc_expense_trans_tb where un_id={0}", _obj.un_id)) == 0)
                {
                    string _str = "Are You Sure You Want To Delete This Sub Expense ??";
                    if (!dbm.WarningMessage(_str, "Delete Voucher Warning"))
                    {
                        return;
                    }
                    datam.ShowWaitForm("Deleting Record Please Wait..");
                    Application.DoEvents();
                    #region withdraws
                    if (_obj.source_type == em.exp_inc_src_typeS.petty_cash_cheque)
                    {
                        if (!string.IsNullOrEmpty(_obj.w_dr_data))
                        {

                        }
                    }
                    if (!string.IsNullOrEmpty(_obj.w_dr_data))
                    {
                        var _cheques = _obj.w_dr_data.Split(new char[] { ',' });
                        xd.UpdateFsTimeStamp("acc_bank_withdraw_tb");
                        string _st = null;
                        foreach (var t in _cheques)
                        {
                            var _val = t.Split(new char[] { ':' });
                            if (xd.ExecuteScalarInt(string.Format("select transfer_id from acc_bank_withdraw_tb where wdr_id={0}", _val[0].ToInt32())) > 0)
                            {
                                MessageBox.Show("You Have Already Closed This Cheque, This Operation Cannot Be Carried Out", "Closed Cheque Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                xd.RollBackTransaction();
                                return;
                            }

                            _st = string.Format("update acc_bank_withdraw_tb set cheque_balance=(cheque_balance + {0}),{1},fs_time_stamp={2} where wdr_id={3}", _val[1],
                             dbm.ETS, SQLH.UnixStamp, _val[0]);
                            xd.InsertUpdateDelete(_st);
                        }
                    }
                    //
                    _obj.voucher_status = em.voucher_statusS.cancelled;
                    xd.SingleUpdateCommandALL("acc_expense_trans_tb", new string[]
         {
                         "voucher_status",
                         "un_id"
         }, new object[]
         {
                         em.voucher_statusS.cancelled.ToByte(),
                         _obj.un_id
         }, 1);
                    //
                    xd.SingleUpdateCommandALL("acc_expense_trans_child_tb", new string[]
                    {
                         "voucher_status",
                         "trans_id"
                    }, new object[]
                    {
                         em.voucher_statusS.cancelled.ToByte(),
                         _obj.un_id
                    }, 1);
                    //
                    fnn.ExecuteDeleteBase(xd, new ic.deleteBaseC()
                    {
                        del_fs_date = sdata.CURR_DATE,
                        del_fs_id = sdata.CURR_FS.fs_id,
                        del_fs_time = DateTime.Now.ToShortTimeString(),
                        del_pc_us_id = sdata.PC_US_ID
                    }, "acc_expense_trans_tb", "un_id", _obj.un_id);
                    //
                    if (!string.IsNullOrEmpty(_obj.cheque_no))
                    {
                        ic.bankAccountC _bank = datam.DATA_BANK_ACCOUNTS.Values.Where(y => y.sys_account_id == _obj.source_account_id).FirstOrDefault();
                        xd.SingleUpdateCommandALL("acc_usedbank_cheques_tb", new string[]
               {
                               "status",
                               "bank_account_id",
                               "cheque_no"
               }, new object[]
               {
                               1,
                               _bank.un_id,
                               _obj.cheque_no
               }, 2);

                    }
                   
                    switch (_obj.pay_mode)
                    {
                        case em.voucher_Paymode.cash:
                            {
                                accn.JournalBook(xd, _obj.exp_date.Value, em.j_sectionS.cash, _obj.transaction_id, _obj.source_account_id, 0, (_obj.exp_amount * -1));
                                break;
                            }
                        case em.voucher_Paymode.bank_transfer:
                        case em.voucher_Paymode.cheque:
                            {
                                accn.JournalBook(xd, _obj.exp_date.Value, em.j_sectionS.bank, _obj.transaction_id, _obj.source_account_id, 0, (_obj.exp_amount * -1));
                                break;
                            }
                    }
                    switch (_obj.objExpenseAccount.exp_acc_type)
                    {
                        case em.exp_acc_typeS.system_offertory_payment:
                            {
                                accn.JournalBook(xd, _obj.exp_date.Value, em.j_sectionS.creditor, _obj.transaction_id, _obj.objExpenseAccount.cr_account_id, (_obj.exp_amount * -1), 0);
                                break;

                            }
                        default:
                            {
                                accn.JournalBook(xd, _obj.exp_date.Value, em.j_sectionS.expense, _obj.transaction_id, _obj.objExpenseAccount.sys_account_id, (_obj.exp_amount * -1), 0);
                                break;
                            }
                    }
                    #endregion
                    xd.CommitTransaction();
                    iGRow parentRow = fGrid.Rows[string.Format("gp{0}", _obj.voucher_id)];
                    // parentRow.
                    var m_partition = string.Format("{0}{1}", m_YEAR, m_MONTH).ToInt32();
                    var _sum = (from k in datam.DATA_MONTH_EXPENSES[m_partition].Values
                                where k.voucher_id == _obj.voucher_id & k.voucher_status == em.voucher_statusS.valid
                                select k).Sum(h => h.exp_amount);
                    parentRow.Cells["expense_amount"].Value = _sum.ToNumberDisplayFormat();
                    parentRow.Cells["expense_amount"].Col.AutoWidth();
                    fGrid.Rows.RemoveAt(fGrid.Rows[_obj.un_id.ToString()].Index);
                    datam.HideWaitForm();
                }
                else
                {
                    MessageBox.Show("You Have Already Deleted This Expense", "Duplicate Action Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
              
            }
            CheckForUpdates(false);
        }

        private void addExpenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            iGRow _row = fGrid.SelectedRows[0];
            var m_partition = string.Format("{0}{1}", m_YEAR, m_MONTH).ToInt32();
            var _obj = (from k in datam.DATA_MONTH_EXPENSES[m_partition].Values
                        where k.voucher_id == _row.Tag.ToInt32() & k.voucher_status == em.voucher_statusS.valid
                        select k).FirstOrDefault();
            if (_obj != null)
            {
                using (var _fm = new MakeAdditionalExpense())
                {
                    _fm.Owner = this;
                    _fm.Tag = _obj;
                    _fm.ShowDialog();
                }
                
            }
        }
    }
}
