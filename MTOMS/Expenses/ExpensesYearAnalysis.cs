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
using System.Drawing.Drawing2D;
using SdaHelperManager.Security;

namespace MTOMS
{
    public partial class ExpensesYearAnalysis : DevComponents.DotNetBar.Office2007Form
    {
        public ExpensesYearAnalysis()
        {
            InitializeComponent();
        }
        private int m_month = -5;
        private long m_last_time_stamp;
        private bool tab1_loaded = false;
        private bool tab2_loaded = false;
       
        private List<int> IsExtended { get; set; }
        int m_YEAR = 0;
        private enum _show_type
        {
            payments_expenses,
            expenses_only,
            payments_only
        }
        private _show_type m_showType = _show_type.expenses_only;
        private Dictionary<string, _show_type> m_SHOW_TYPES { get; set; }
        private bool app_working = false;
        private void ExpensesYearAnalysis_Load(object sender, EventArgs e)
        {
            datam.SecurityCheck();
            xso.xso.Intialize();
            datam.SystemInitializer();
            fGrid.Rows.Clear();
            fGridnormal.Rows.Clear();
            LoadYears();
            InitializeTab1GridColumns();
            InitializeTab2GridColumns();
            
            
            using(var xd= new xing())
            {
                datam.InitExpenses(xd);
                xd.CommitTransaction();
            }
            m_SHOW_TYPES = new Dictionary<string, _show_type>();
            m_SHOW_TYPES.Add("Expenses Only", _show_type.expenses_only);
            //m_SHOW_TYPES.Add("CR Payments Only", _show_type.payments_only);
            //m_SHOW_TYPES.Add("Expenses & CR Payments", _show_type.payments_expenses);
            app_working = true;
            foreach(var k in m_SHOW_TYPES.Keys)
            {
                comboBoxEx1.Items.Add(k);
            }
            comboBoxEx1.SelectedIndex = 0;
            app_working = false;
            // CreateDefaultRowsDepartment();
            CreateDefaultRowsCategory();
            this.VisibleChanged += new EventHandler(ExpensesYearAnalysis_VisibleChanged);
        }
        
        void ExpensesYearAnalysis_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible & comboyear.SelectedIndex > -1)
            {
                CheckForUpdates();
            }
        }
        private void InitializeTab1GridColumns()
        {
            #region Columns To Display
            string[] _cols = new string[] { "Description", "Jan", "Feb", "Mar", "April", "May", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec", "Year\nTotal" };
            iGCol myCol;
            int y = 1;
            var _grids = new object[] { fGrid };
            foreach (var g in _grids)
            {
                iGrid _grid = (iGrid)g;
                y = 1;
                foreach (var c in _cols)
                {
                    if (y > 1 & y < 14)
                    {
                        myCol = _grid.Cols.Add((y - 1).ToString(), string.Format("{0}", c));
                        myCol.AllowSizing = true;
                        myCol.CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
                        myCol.CellStyle.FormatString = "{0:N0}";
                        myCol.CellStyle.ValueType = typeof(int);
                        myCol.Width = 100;
                    }
                    else
                    {
                        if (y == 14)
                        {
                            myCol = _grid.Cols.Add("Total", string.Format("{0}", c));
                            myCol.AllowSizing = true;
                            myCol.CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
                            myCol.CellStyle.FormatString = "{0:N0}";
                            myCol.CellStyle.ValueType = typeof(int);
                            myCol.Width = 100;
                            continue;
                        }
                        myCol = _grid.Cols.Add(c, c);
                        myCol.AllowSizing = true;
                    }
                    myCol.IncludeInSelect = true;
                    myCol.SortType = iGSortType.None;
                    //


                    myCol.ColHdrStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    myCol.ColHdrStyle.ForeColor = Color.Red;
                    myCol.ColHdrStyle.Font = new Font("verdana", 9, FontStyle.Bold);

                    y++;

                }
                _grid.Cols["Total"].CellStyle.FormatString = "{0:N0}";
                _grid.Cols["Total"].CellStyle.BackColor = Color.WhiteSmoke;
                _grid.Cols.Add("Category", string.Empty).Visible = false;
                _grid.Cols.Add("SValue", string.Empty).Visible = false;
                _grid.Cols.Add("xsection", string.Empty).Visible = false;
                _grid.Cols.Add("parent_id", string.Empty).Visible = false;
                _grid.Cols.Add("has_children", string.Empty).Visible = false;
                myCol = _grid.Cols["SValue"];
                myCol.SortType = iGSortType.ByValue;

                _grid.Header.Rows.Add();

                _grid.Header.Cells[1, 1].SpanCols = 12;
                _grid.Header.Cells[1, 1].Value = "Monthly Departmental Expenses";
                _grid.Header.Cells[1, 1].ForeColor = Color.Blue;

                _grid.DefaultRow.Height = _grid.GetPreferredRowHeight(true, false);
                _grid.DefaultAutoGroupRow.Height = _grid.DefaultRow.Height;
                _grid.Cols.AutoWidth();


            }
            #endregion
        }
        private string BreakString(string _str)
        {
            var _ret = _str.Split(new char[] { (char)Keys.Space });
            if (_ret.Length < 2) { return _str; }
            string n_str = null;
            n_str = string.Format("{0}\n{1}", _ret[0], _ret[1]);
            if (_ret.Length > 2)
            {
                n_str += " " + _ret[2];
            }
            return n_str;
        }
        private void InitializeTab2GridColumns()
        {
            #region Columns To Display
            string[] _cols = new string[] { "Description", "1st Quarter", "2nd Quarter", "3rd Quarter", "4th Quarter", "Year Total" };
            iGCol myCol;
            int y = 1;
            var _grids = new object[] { fGridnormal };
            foreach (var g in _grids)
            {
                iGrid _grid = (iGrid)g;
                y = 1;
                foreach (var c in _cols)
                {
                    if (y > 1 & y < 6)
                    {
                        myCol = _grid.Cols.Add((y - 1).ToString(), BreakString(c));
                        myCol.AllowSizing = true;
                        myCol.CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
                        myCol.CellStyle.FormatString = "{0:N0}";
                        myCol.CellStyle.ValueType = typeof(int);
                        myCol.Width = 100;
                    }
                    else
                    {
                        if (y == 6)
                        {
                            myCol = _grid.Cols.Add("Total", BreakString(c));
                            myCol.AllowSizing = true;
                            myCol.CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
                            myCol.CellStyle.ValueType = typeof(decimal);
                            myCol.CellStyle.FormatString = "{0:N0}";
                            myCol.Width = 100;
                            myCol.CellStyle.ValueType = typeof(int);
                            continue;
                        }
                        myCol = _grid.Cols.Add(c, c);
                        myCol.AllowSizing = true;
                    }
                    myCol.IncludeInSelect = true;
                    myCol.SortType = iGSortType.None;
                    //


                    myCol.ColHdrStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    myCol.ColHdrStyle.ForeColor = Color.Red;
                    myCol.ColHdrStyle.Font = new Font("verdana", 9, FontStyle.Bold);

                    y++;

                }
                _grid.Cols["Total"].CellStyle.FormatString = "{0:N0}";
                _grid.Cols["Total"].CellStyle.BackColor = Color.WhiteSmoke;
                _grid.Cols.Add("Category", string.Empty).Visible = false;
                _grid.Cols.Add("SValue", string.Empty).Visible = false;
                _grid.Cols.Add("xsection", string.Empty).Visible = false;
                _grid.Cols.Add("parent_id", string.Empty).Visible = false;
                _grid.Cols.Add("has_children", string.Empty).Visible = false;
                myCol = _grid.Cols["SValue"];
                myCol.SortType = iGSortType.ByValue;

                _grid.Header.Rows.Add();

                _grid.Header.Cells[1, 1].SpanCols = 4;
                _grid.Header.Cells[1, 1].Value = "Quarterly Departmental Expenses";
                _grid.Header.Cells[1, 1].ForeColor = Color.Blue;

                _grid.DefaultRow.Height = _grid.GetPreferredRowHeight(true, false);
                _grid.DefaultAutoGroupRow.Height = _grid.DefaultRow.Height;
                _grid.Cols.AutoWidth();


            }
            #endregion

        }
        private void ClearGridCells(iGrid grid)
        {
            m_last_time_stamp = 0;
            if (grid == null)
            {
                foreach (var gr in new iGrid[] { fGrid, fGridnormal })
                {
                    var cell_list = from c in gr.Cols.Cast<iGCol>()
                                    where c.Index > 0 & c.Visible
                                    from k in c.Cells.Cast<iGCell>()
                                    where k.Value != null
                                    select k;
                    foreach (var k in cell_list)
                    {
                        k.Value = null;
                        k.AuxValue = null;
                        if (k.ValueType == null)
                        {
                            k.ValueType = typeof(int);
                        }
                        if (k.Row.Level > 0)
                        {
                            k.Row.Visible = false;
                        }
                    }
                }
            }
            else
            {
                var cell_list = from c in grid.Cols.Cast<iGCol>()
                                where c.Index > 0 & c.Visible
                                from k in c.Cells.Cast<iGCell>()
                                where k.Value != null
                                select k;
                foreach (var k in cell_list)
                {
                    k.Value = null;
                    k.AuxValue = null;
                    if (k.ValueType == null)
                    {
                        k.ValueType = typeof(int);
                    }
                    if (k.Row.Level > 0)
                    {
                        k.Row.Visible = false;
                    }
                }
            }
        }
        private void CheckForUpdates()
        {
            if (m_YEAR > 0)
            {
                using (var xd = new xing())
                {
                    if (datam.GetYearExpenses(m_YEAR, xd))
                    {
                        if (tab1_loaded)
                        {
                           
                            tab1_loaded = false;
                        }
                        if (tab2_loaded)
                        {
                            tab2_loaded = false;
                        }
                          LoadTabs();
                    }
                    else
                    {
                        if (tab1_loaded)
                        {
                            fGrid.Focus();
                            return;
                        }
                        if (tab2_loaded)
                        {
                            fGridnormal.Focus();
                            return;
                        }
                    }
                }
            }
        }
        private void LoadQuarterData()
        {
            IEnumerable<ic.expense_transC> _list = null;
            switch (m_showType)
            {
                case _show_type.payments_expenses:
                    {
                        _list = from e in datam.DATA_YEAR_EXPENSES[m_YEAR].Values
                                where e.voucher_status == em.voucher_statusS.valid
                                select e;
                        break;
                    }
                case _show_type.expenses_only:
                    {
                        _list = from e in datam.DATA_YEAR_EXPENSES[m_YEAR].Values
                                where e.voucher_status == em.voucher_statusS.valid & e.IsExpense
                                select e;
                        break;
                    }
                case _show_type.payments_only:
                    {
                        _list = from e in datam.DATA_YEAR_EXPENSES[m_YEAR].Values
                                where e.voucher_status == em.voucher_statusS.valid & e.IsPayment
                                select e;
                        break;
                    }
            }
         
            iGCell _cell = null;
            iGRow _row = null;
            int amount = 0;
            int month_id = 0;
            var _grid = fGridnormal;
            iGCell _cell_parent = null;
            _grid.BeginUpdate();
            foreach (var _exp in _list)
            {
                _cell = _grid.Rows[_exp.exp_acc_id.ToString()].Cells[_exp.quarter_id];
                _cell.TypeFlags = iGCellTypeFlags.HasEllipsisButton;
                _cell.ReadOnly = iGBool.False;
                _cell.Row.Visible = true;
                if (_cell.Row.Level == 0)
                {
                    _cell.Value = (_cell.Value.ToInt32() + _exp.exp_amount);
                    if (_cell.AuxValue == null)
                    {
                        _cell.AuxValue = string.Format("{0}|{1}|{2}|{3}|{4}|{5}", "qe", 0, _exp.exp_cat_id, m_YEAR, _exp.quarter_id, m_showType.ToByte());
                    }
                }
                else
                {
                    _cell.Value = (_cell.Value.ToInt32() + _exp.exp_amount);
                    if (_cell.AuxValue == null)
                    {
                        _cell.AuxValue = string.Format("{0}|{1}|{2}|{3}|{4}|{5}", "qe", 1, _exp.exp_acc_id, m_YEAR, _exp.quarter_id, m_showType.ToByte());
                    }
                    //
                    _cell_parent = _grid.Rows[_cell.Row.Cells["parent_id"].Text].Cells[_cell.ColIndex];
                    _cell_parent.Value = (_cell_parent.Value.ToInt32() + _exp.exp_amount);
                    if (_cell_parent.AuxValue == null)
                    {
                        _cell_parent.AuxValue = string.Format("{0}|{1}|{2}|{3}|{4}|{5}", "qe", 0, _exp.exp_cat_id, m_YEAR, _exp.quarter_id, m_showType.ToByte());
                    }
                    //

                }
            }
            #region calculate grand_total
            var gnd_tt = from n in _grid.Cols.Cast<iGCol>()
                         where n.Index > 0 & n.Index < 5
                         from c in n.Cells.Cast<iGCell>()
                         where (c.Row.Index < _grid.Rows["gt"].Index & c.Row.Level == 0)
                         & c.Value != null
                         group c by c.ColKey into ngroup
                         select new
                         {
                             col_key = ngroup.Key,
                             xsum = ngroup.Sum(j => j.Value.ToInt32())
                         };
            foreach (var t in gnd_tt)
            {
                _grid.Rows["gt"].Cells[t.col_key].Value = t.xsum;
            }
            #endregion
            #region calculate row totals
            var clist = from n in _grid.Rows.Cast<iGRow>()
                        where !string.IsNullOrEmpty(n.Key)
                        from c in n.Cells.Cast<iGCell>()
                        where (c.ColIndex > 0 & c.ColIndex < 5) & c.Value != null
                        group c by c.RowKey into ngroup
                        select new
                        {
                            row_key = ngroup.Key,
                            xsum = ngroup.Sum(j => j.Value.ToInt32())
                        };
            foreach (var t in clist)
            {
                _grid.Rows[t.row_key].Cells["total"].Value = t.xsum;
            }
            #endregion

            _grid.Cols.AutoWidth();
            _grid.EndUpdate();
            _grid.EndUpdate();
        }
        private void LoadMonthData()
        {
          
            IEnumerable<ic.expense_transC> _list = null;
            switch (m_showType)
            {
                case _show_type.payments_expenses:
                    {
                        _list = from e in datam.DATA_YEAR_EXPENSES[m_YEAR].Values
                                where e.voucher_status == em.voucher_statusS.valid
                                select e;
                        break;
                    }
                case _show_type.expenses_only:
                    {
                        _list = from e in datam.DATA_YEAR_EXPENSES[m_YEAR].Values
                                where e.voucher_status == em.voucher_statusS.valid & e.IsExpense
                                select e;
                        break;
                    }
                case _show_type.payments_only:
                    {
                        _list = from e in datam.DATA_YEAR_EXPENSES[m_YEAR].Values
                                where e.voucher_status == em.voucher_statusS.valid & e.IsPayment
                                select e;
                        break;
                    }
            }
           
            iGCell _cell = null;
            iGRow _row = null;
            int amount = 0;
            int month_id = 0;
            var _grid = fGrid;
            iGCell _cell_parent = null;
            _grid.BeginUpdate();
            foreach (var _exp in _list)
            {
                _cell = _grid.Rows[_exp.exp_acc_id.ToString()].Cells[_exp.exp_date.Value.Month];
                _cell.TypeFlags = iGCellTypeFlags.HasEllipsisButton;
                _cell.ReadOnly = iGBool.False;
                _cell.Row.Visible = true;
                if (_cell.Row.Level == 0)
                {
                    _cell.Value = (_cell.Value.ToInt32() + _exp.exp_amount);
                    if (_cell.AuxValue == null)
                    {
                        _cell.AuxValue = string.Format("{0}|{1}|{2}|{3}|{4}|{5}", "me", 0, _exp.exp_cat_id, m_YEAR, _exp.exp_date.Value.Month,m_showType.ToByte());
                    }
                }
                else
                {
                    _cell.Value = (_cell.Value.ToInt32() + _exp.exp_amount);
                    if (_cell.AuxValue == null)
                    {
                        _cell.AuxValue = string.Format("{0}|{1}|{2}|{3}|{4}|{5}", "me", 1, _exp.exp_acc_id, m_YEAR, _exp.exp_date.Value.Month,m_showType.ToByte());
                    }
                    //
                    _cell_parent = _grid.Rows[_cell.Row.Cells["parent_id"].Text].Cells[_cell.ColIndex];
                    _cell_parent.Value = (_cell_parent.Value.ToInt32() + _exp.exp_amount);
                    if (_cell_parent.AuxValue == null)
                    {
                        _cell_parent.AuxValue = string.Format("{0}|{1}|{2}|{3}|{4}|{5}", "me", _exp.exp_cat_id, _cell_parent.Row.Key.ToInt32(), m_YEAR, _exp.exp_date.Value.Month, m_showType.ToByte());
                    }
                    //
                    
                }
            }
            #region calculate grand_total
            var gnd_tt = from n in _grid.Cols.Cast<iGCol>()
                         where n.Index > 0 & n.Index < 13
                         from c in n.Cells.Cast<iGCell>()
                         where (c.Row.Index < _grid.Rows["gt"].Index & c.Row.Level==0)
                         & c.Value != null
                         group c by c.ColKey into ngroup
                         select new
                         {
                             col_key = ngroup.Key,
                             xsum = ngroup.Sum(j => j.Value.ToInt32())
                         };
            foreach (var t in gnd_tt)
            {
                _grid.Rows["gt"].Cells[t.col_key].Value = t.xsum;
            }
            #endregion
            #region calculate row totals
            var clist = from n in _grid.Rows.Cast<iGRow>()
                        where !string.IsNullOrEmpty(n.Key)
                        from c in n.Cells.Cast<iGCell>()
                        where (c.ColIndex > 0 & c.ColIndex < 13) & c.Value != null
                        group c by c.RowKey into ngroup
                        select new
                        {
                            row_key = ngroup.Key,
                            xsum = ngroup.Sum(j => j.Value.ToInt32())
                        };
            foreach (var t in clist)
            {
                _grid.Rows[t.row_key].Cells["total"].Value = t.xsum;
            }
            #endregion

            _grid.Cols.AutoWidth();
            _grid.EndUpdate();
            _grid.EndUpdate();
        }
    
        private void CreateDefaultRowsCategory()
        {
            iGRow _row = null;
            iGRow _parent_row = null;
            bool _has_children = false;
            IEnumerable<ic.expense_accountC> clist = null;
            foreach (var _grid in new iGrid[] { fGrid, fGridnormal })
            {
                _grid.BeginUpdate();
                _grid.Rows.Clear();
                _row = _grid.Rows.Add();
                _row.Selectable = false;
                _row.Height = 5;
                _row.BackColor = Color.Lavender;
                _row.Key = "div11";
                var elist = (from k in datam.DATA_EXPENSE_ACCOUNTS.Values
                             where k.objCategory != null && k.objCategory.exp_cat_id > 0
                             select k.objCategory).Distinct();
            
                foreach (var d in elist)
                {
                    _row = _grid.Rows.Add();
                    _row.Font = new Font("arial narrow", 13, FontStyle.Bold);
                    _row.ReadOnly = iGBool.True;
                    _row.Cells["Description"].Value = d.exp_cat_name;
                    _row.Key = string.Format("C{0}", d.exp_cat_id.ToString());
                    _row.Cells["SValue"].Value = 1;
                    _row.ForeColor = Color.Maroon;
                    //
                     clist = (from c in datam.DATA_EXPENSE_ACCOUNTS.Values
                                 where c.exp_cat_id==d.exp_cat_id
                                 select c).Distinct();
                    _parent_row = _row;
                    _has_children = false;
                    _row.AutoHeight();
                    foreach (var c in clist)
                    {
                        _has_children = true;
                        _row = _grid.Rows.Add();
                        _row.ReadOnly = iGBool.True;
                        _row.Level = 1;
                        _row.ReadOnly = iGBool.True;
                        _row.Cells["Description"].Value = c.exp_acc_name;
                        _row.Key = c.exp_acc_id.ToStringNullable();
                        _row.TreeButton = iGTreeButtonState.Hidden;
                        _row.Font = new Font("arial narrow", 12, FontStyle.Bold);
                        _row.ForeColor = Color.DarkBlue;
                        _row.Cells["SValue"].Value = 2;
                        _row.Cells["parent_id"].Value = _parent_row.Key;
                        _row.Expanded = false;
                        _row.AutoHeight();
                       // _row.Visible = false;
                    }
                    if (_has_children)
                    {
                        _parent_row.TreeButton = iGTreeButtonState.Visible;
                        _parent_row.Expanded = false;
                        _parent_row.Cells["has_children"].Value = 1;
                    }
                }
                #region no group
                _row = _grid.Rows.Add();
                _row.Font = new Font("arial narrow", 13, FontStyle.Bold);
                _row.ReadOnly = iGBool.True;
                _row.Cells["Description"].Value = "NO Category";
                _row.Key = "C0";
                _row.Cells["SValue"].Value = 1;
                _row.ForeColor = Color.Maroon;
                //
                 clist = (from c in datam.DATA_EXPENSE_ACCOUNTS.Values
                             where c.exp_cat_id == 0
                             select c).Distinct();
                _parent_row = _row;
                _has_children = false;
                _row.AutoHeight();
                foreach (var c in clist)
                {
                    _has_children = true;
                    _row = _grid.Rows.Add();
                    _row.ReadOnly = iGBool.True;
                    _row.Level = 1;
                    _row.ReadOnly = iGBool.True;
                    _row.Cells["Description"].Value = c.exp_acc_name;
                    _row.Key = c.exp_acc_id.ToStringNullable();
                    _row.TreeButton = iGTreeButtonState.Hidden;
                    _row.Font = new Font("arial narrow", 12, FontStyle.Bold);
                    _row.ForeColor = Color.DarkBlue;
                    _row.Cells["SValue"].Value = 2;
                    _row.Cells["parent_id"].Value = _parent_row.Key;
                    _row.Expanded = false;
                    _row.AutoHeight();
                    //  _row.Visible = false;
                }
                if (_has_children)
                {
                    _parent_row.TreeButton = iGTreeButtonState.Visible;
                    _parent_row.Expanded = false;
                    _parent_row.Cells["has_children"].Value = 1;
                }
                #endregion
                _row = _grid.Rows.Add();
                _row.Selectable = false;
                _row.Height = 5;
                _row.BackColor = Color.Lavender;
                _row.Key = "div4";
                //
                _row = _grid.Rows.Add();
                _row.Font = new Font("arial narrow", 13, FontStyle.Bold);
                _row.ReadOnly = iGBool.True;
                if (_grid == fGrid)
                {
                    _row.Cells["Description"].Value = "Month Total";
                }
                else
                {
                    _row.Cells["Description"].Value = "Quarter Total";
                }

                _row.Cells["Description"].TextAlign = iGContentAlignment.MiddleCenter;
                _row.ForeColor = Color.Yellow;
                _row.BackColor = Color.FromArgb(64, 64, 64);
                _row.Key = "gt";
                _row.AutoHeight();
                _row.Height += 3;
                //add the sum row
                _grid.TreeLines.Visible = true;
                _grid.Cols.AutoWidth();
                _grid.AutoResizeCols = false;
                var plist = from c in _grid.Cols["Description"].Cells.Cast<iGCell>()
                            where c.Value != null
                            select c;
                foreach (var k in plist)
                {
                    k.TextAlign = iGContentAlignment.BottomLeft;
                    //if (k.Row.Level == 0)
                    //{
                    //    k.Font = new Font("verdana", 9, FontStyle.Bold);
                    //}
                    //else
                    //{
                    //    k.Font = new Font("verdana", 9, FontStyle.Regular);
                    //}
                }
                _grid.Rows["gt"].Cells["description"].TextAlign = iGContentAlignment.BottomRight;
                _grid.EndUpdate();
            }
        }
        private void CreateDefaultRowsDepartment()
        {
            iGRow _row = null;
            iGRow _parent_row = null;
            bool _has_children = false;
            foreach (var _grid in new iGrid[] { fGrid, fGridnormal })
            {
                _grid.BeginUpdate();
                _grid.Rows.Clear();
                 _row = _grid.Rows.Add();
                _row.Selectable = false;
                _row.Height = 5;
                _row.BackColor = Color.Lavender;
                _row.Key = "div11";
                var elist = (from k in datam.DATA_EXPENSE_ACCOUNTS.Values
                             where k.objDepartment.level == 1
                             select k.objDepartment).Distinct();
                foreach (var d in elist)
                {
                    _row = _grid.Rows.Add();
                    _row.Font = new Font("arial narrow", 13, FontStyle.Bold);
                    _row.ReadOnly = iGBool.True;
                    _row.Cells["Description"].Value = d.dept_name;
                    _row.Key = d.dept_id.ToStringNullable();
                    _row.Cells["SValue"].Value = 1;
                    //
                    var clist = (from c in datam.DATA_EXPENSE_ACCOUNTS.Values
                                where c.objDepartment.parent_id == d.dept_id
                                select c.objDepartment).Distinct();
                    _parent_row = _row;
                    _has_children = false;
                    _row.AutoHeight();
                    foreach(var c in clist)
                    {
                        _has_children = true;
                        _row = _grid.Rows.Add();
                        _row.ReadOnly = iGBool.True;
                        _row.Level = 1;
                        _row.ReadOnly = iGBool.True;
                        _row.Cells["Description"].Value = c.dept_name;
                        _row.Key = c.dept_id.ToStringNullable();
                        _row.TreeButton = iGTreeButtonState.Hidden;
                        _row.Font = new Font("arial narrow", 12, FontStyle.Bold);
                        _row.ForeColor = Color.Blue;
                        _row.Cells["SValue"].Value = 2;
                        _row.Cells["parent_id"].Value = _parent_row.Key;
                        _row.Expanded = false;
                        _row.AutoHeight();
                    }
                    if (_has_children)
                    {
                        _parent_row.TreeButton = iGTreeButtonState.Visible;
                        _parent_row.Expanded = false;
                        _parent_row.Cells["has_children"].Value = 1;
                    }
                }
               
                _row = _grid.Rows.Add();
                _row.Selectable = false;
                _row.Height = 5;
                _row.BackColor = Color.Lavender;
                _row.Key = "div4";
                //
                _row = _grid.Rows.Add();
                _row.Font = new Font("arial narrow", 13, FontStyle.Bold);
                _row.ReadOnly = iGBool.True;
                if (_grid == fGrid)
                {
                    _row.Cells["Description"].Value = "Month Total";
                }
                else
                {
                    _row.Cells["Description"].Value = "Quarter Total";
                }

                _row.Cells["Description"].TextAlign = iGContentAlignment.MiddleCenter;
                _row.ForeColor = Color.Yellow;
                _row.BackColor = Color.FromArgb(64, 64, 64);
                _row.Key = "gt";
                _row.AutoHeight();
                _row.Height += 3;
                //add the sum row
                _grid.TreeLines.Visible = true;
                _grid.Cols.AutoWidth();
                _grid.AutoResizeCols = false;
                var plist = from c in _grid.Cols["Description"].Cells.Cast<iGCell>()
                            where c.Value != null
                            select c;
                foreach (var k in plist)
                {
                    k.TextAlign = iGContentAlignment.BottomLeft;
                    //if (k.Row.Level == 0)
                    //{
                    //    k.Font = new Font("verdana", 9, FontStyle.Bold);
                    //}
                    //else
                    //{
                    //    k.Font = new Font("verdana", 9, FontStyle.Regular);
                    //}
                }
                _grid.Rows["gt"].Cells["description"].TextAlign = iGContentAlignment.BottomRight;
                _grid.EndUpdate();
            }
            
        }
       
        private void SortAndGroup()
        {
            fGrid.GroupObject.Clear();
            fGrid.SortObject.Clear();
            fGrid.GroupObject.Add("SValue");
            fGrid.VScrollBar.Visibility = iGScrollBarVisibility.Always;
            fGrid.SortObject.Add("SValue", iGSortOrder.Ascending);
            fGrid.Group();
        }
        private void LoadYears()
        {
            for (int j = datam.CURR_FS.fs_year; j > 2010; j--)
            {
                comboyear.Items.Add(j.ToString());
            }
           // comboyear.SelectedIndex = 0;
        }
        private void comboyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboyear.SelectedIndex==-1)
            {
                m_YEAR = -1;
                ClearGridCells(null);
                buttonX1.Enabled = false;
                comboBoxEx1.Enabled = false;
                return;
            }
            
            if(!buttonX1.Enabled)
            {
                buttonX1.Enabled = true;
                comboBoxEx1.Enabled = true;
            }
            comboyear.Enabled = false;
            Application.DoEvents();
           
            m_YEAR = comboyear.SelectedItem.ToInt32();
            //combomonth.SelectedIndex = -1;
            tab2_loaded = false;
            tab1_loaded = false;
            ClearGridCells(null);
            LoadTabs();
            comboyear.Enabled = true;
            
        }
        private void combomonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            //m_month = combomonth.SelectedIndex + 1;
            //var _new_list = from r in datam.DATA_FS[comboyear.SelectedItem.ToInt32()].Values
            //                where r.fs_month == combomonth.SelectedIndex + 1
            //                & r.fs_week_dayname_no == 6
            //                orderby r.fs_id ascending
            //                select r;
            // int y = 1;
            // var _grids = new object[] { fGrid, fGridnormal };
            // foreach (var _g in _grids)
            // {
            //     iGrid _grid = (iGrid)_g;
            //     y = 1;
            //     foreach (var c in _new_list)
            //     {
            //         //_grid.Cols[string.Format("Week {0}", y)].Text = string.Format("Week {0} \n{1}", y, c.fs_date.ToMyShortDate());
            //         //_grid.Cols[string.Format("Week {0}", y)].Width = 160;
            //         //_grid.Cols[string.Format("Week {0}", y)].Visible = true;
            //         _grid.Cols[y].Text = string.Format("Week {0} \n{1}", y, c.fs_date.ToMyShortDate());
            //         _grid.Cols[y].Width = 160;
            //         _grid.Cols[y].Visible = true;
            //         _grid.Cols[y].Key = c.fs_id.ToStringNullable();
            //         y++;
            //     }
            //     if (y == 5)
            //     {
            //         _grid.Cols[5].Visible = false;
            //     }
            //     else
            //     {
            //         _grid.Cols[5].Visible = true;
            //     }
            //     _grid.Cols.AutoWidth();
            // }
            // LoadTabs();
        }
        private void LoadTabs()
        {
            if (comboyear.SelectedItem == null | comboyear.SelectedIndex==-1) { return; }
            datam.ShowWaitForm("Loading Data,Please Wait");
            comboyear.Enabled = false;
            Application.DoEvents();
            using(var xd= new xing())
            {
                datam.GetYearExpenses(comboyear.SelectedItem.ToString().ToInt32(), xd);
                xd.CommitTransaction();
            }
            if (tabControl1.SelectedTab == tabItem1 & !tab1_loaded )
            {
                tab1_loaded = true;
                ClearGridCells(fGrid);
                LoadMonthData();
               
            }
          
            if (tabControl1.SelectedTab == tabItem3 & !tab2_loaded)
            {
                tab2_loaded = true;
                ClearGridCells(fGridnormal);
                LoadQuarterData();
                
            }
            comboyear.Enabled = true;
            datam.HideWaitForm();
        }
        
        private void buttonX1_Click(object sender, EventArgs e)
        {
            CheckForUpdates();
        }
        private void tabControl1_SelectedTabChanged(object sender, DevComponents.DotNetBar.TabStripTabChangedEventArgs e)
         {
             LoadTabs();
         }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if(comboyear.SelectedIndex==-1)
            {
                e.Cancel = true;
                return;
            }
        }
        private void printGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckForUpdates();
            if (tabControl1.SelectedTab == tabItem1)
            {
                var nlist = from r in fGrid.Rows.Cast<iGRow>()
                            where (r.Level == 0 & r.Cells["SValue"].Value != null)
                            from c in r.Cells.Cast<iGCell>()
                            where (c.ColIndex > 0 & c.ColIndex < 13)
                            & c.Col.Visible orderby c.ColIndex
                            select new
                            {
                                month_index = c.Col.Key.ToInt16(),
                                month_value = c.Value == null ? 0 : c.Value.ToInt32(),
                                section = r.Cells["SValue"].Value.ToInt16(),
                                item = r.Cells["Description"].Value.ToString(),
                           };
                DataTable htable = new DataTable();
                htable.TableName = "HeaderData";
                htable.Columns.Add("ChurchName", typeof(string));
                htable.Columns.Add("LData", typeof(string));
                var _row = htable.NewRow();
                _row[0] = datam.CHURCH.item_name;
                _row[1] = string.Format("{0}", comboyear.SelectedItem.ToString());
                htable.Rows.Add(_row);
                using (var _report = new FastReport.Report())
                {
                    _report.Load("month_cash_statement.frx");
                    _report.RegisterData(nlist, "MonthOffering");
                    _report.RegisterData(htable, "HeaderData");
                    // _report.Prepare();
                  //  _report.PrintSettings.PageRange = FastReport.PageRange.All;
                  //   _report.Design();
                    _report.Show();
                }
            }
            else
            {
                var nlist = from r in fGridnormal.Rows.Cast<iGRow>()
                            where (r.Level == 0 & r.Cells["SValue"].Value != null)
                            from c in r.Cells.Cast<iGCell>()
                            where (c.ColIndex > 0 & c.ColIndex < 5)
                            & c.Col.Visible 
                            select new
                            {
                                week_name = c.Col.Text.ToString(),
                                week_value = c.Value == null ? 0 : c.Value.ToInt32(),
                                section = r.Cells["SValue"].Value.ToInt16(),
                                item = r.Cells["Description"].Value.ToString()
                            };
                DataTable htable = new DataTable();
                htable.TableName = "HeaderData";
                htable.Columns.Add("ChurchName", typeof(string));
                htable.Columns.Add("LData", typeof(string));
                var _row = htable.NewRow();
                _row[0] = datam.CHURCH.item_name;
                _row[1] = string.Format("{0}", comboyear.SelectedItem.ToString());

                using (var _report = new FastReport.Report())
                {
                    FastReport.Utils.Config.PreviewSettings.Buttons = FastReport.PreviewButtons.Close
                      | FastReport.PreviewButtons.PageSetup | FastReport.PreviewButtons.Zoom;
                    FastReport.Utils.Config.UIStyle = FastReport.Utils.UIStyle.VistaGlass;
                    _report.Load("quarter_cash_statement.frx");
                 
                    _report.RegisterData(nlist, "MonthOffering");
                    _report.RegisterData(htable, "HeaderData");
                    // _report.Prepare();
                 //   _report.PrintSettings.PageRange = FastReport.PageRange.All;
                   //  _report.Design();
                 _report.Show();
                }
            }
        }
        private void printGrid2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
           
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {

        }

        private void fGrid_DrawEllipsisButtonBackground(object sender, iGDrawEllipsisButtonEventArgs e)
        {
            #region Determine the colors of the background
            Color myColor1, myColor2;
            switch (e.State)
            {
                case iGControlState.Pressed:
                    myColor1 = SystemColors.ControlDark;
                    myColor2 = SystemColors.ControlLightLight;
                    break;
                case iGControlState.Hot:
                    myColor1 = SystemColors.ControlLightLight;
                    myColor2 = Color.Pink;
                    break;
                default:
                    myColor1 = Color.LightGreen;
                    myColor2 = Color.LightGreen;
                    break;
            }
            #endregion

            #region Draw the background
            LinearGradientBrush myBrush = new LinearGradientBrush(e.Bounds, myColor1, myColor2, 45);
            e.Graphics.FillRectangle(myBrush, e.Bounds);
            e.Graphics.DrawRectangle(SystemPens.ControlDark, e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
            #endregion

            #region Notify the grid that the background has been drawn and there is no need to draw it
            e.DoDefault = false;
            #endregion
        }

        private void fGrid_RequestEdit(object sender, iGRequestEditEventArgs e)
        {
            e.DoDefault = false;
        }

        private void fGridnormal_RequestEdit(object sender, iGRequestEditEventArgs e)
        {
            e.DoDefault = false;
        }

        private void fGrid_EllipsisButtonClick(object sender, iGEllipsisButtonClickEventArgs e)
        {
            var _cell = fGrid.Cells[e.RowIndex, e.ColIndex];
            if (_cell.AuxValue != null)
            {
                using (var _fm = new ViewQMYExpenses())
                {
                    _fm.Tag = _cell.AuxValue;
                    _fm.ShowDialog();
                }
            }
        }

        private void exportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var _grid = contextMenuStrip1.SourceControl as iGrid;
            var _digit_cols = (from c in _grid.Cols.Cast<iGCol>()
                               where c.CellStyle.ValueType == typeof(int) & c.Visible
                               select c.Key).ToArray();
            string _disp_name=string.Empty;
            if(tabControl1.SelectedTab==tabItem1)
            {
                _disp_name=string.Format("{0} Monthly Departmental Expenses Report",comboyear.SelectedItem.ToInt32());
            }
            else
            {
                 _disp_name=string.Format("{0} Quarterly Departmental Expenses Report",comboyear.SelectedItem.ToInt32());
            }
            xcel.xcel.IGridMonthQuarterCashStatement(_grid, _disp_name, null, true, _digit_cols);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           // checkBox1.Text = checkBox1.Checked ? "UnCheck To Show Extended Sub Accounts" : "Hide Extended Sub Accounts";
            //foreach (var _grid in new iGrid[] { fGrid, fGridnormal })
            //{
            //    _grid.BeginUpdate();
            //    ic.accountC _acc = null;
            //    iGRow _row = null;
            //    foreach (var k in IsExtended)
            //    {
            //        try
            //        {
            //            _row = _grid.Rows[string.Format("AC{0}", k)];
            //            _acc = datam.DATA_ACCOUNTS[k];
            //            _row.Expanded = false;
            //            _row.TreeButton = checkBox1.Checked ? iGTreeButtonState.Absent : iGTreeButtonState.Visible;
            //        }
            //        catch (Exception ex)
            //        {
            //            continue;
            //        }
            //        //
            //        if ((_row.Index + 1) >= _grid.Rows.Count - 1) { break; }
            //        for (int j = (_row.Index + 1); j < _grid.Rows.Count; j++)
            //        {
            //            if (_grid.Rows[j].Level == _row.Level)
            //            {
            //                break;
            //            }
            //            _grid.Rows[j].Visible = checkBox1.Checked ? false : true;
            //        }
            //    }
            //    _grid.EndUpdate();
            //}
           
        }

        private void fGrid_BeforeRowStateChanged(object sender, iGBeforeRowStateChangedEventArgs e)
        {
            if(tabControl1.SelectedTab==tabItem1)
            {
               // fGrid.Cols["Description"].AutoWidth();
               
            }
        }

        private void fGridnormal_DrawEllipsisButtonBackground(object sender, iGDrawEllipsisButtonEventArgs e)
        {
            #region Determine the colors of the background
            Color myColor1, myColor2;
            switch (e.State)
            {
                case iGControlState.Pressed:
                    myColor1 = SystemColors.ControlDark;
                    myColor2 = SystemColors.ControlLightLight;
                    break;
                case iGControlState.Hot:
                    myColor1 = SystemColors.ControlLightLight;
                    myColor2 = Color.Pink;
                    break;
                default:
                    myColor1 = Color.LightGreen;
                    myColor2 = Color.LightGreen;
                    break;
            }
            #endregion

            #region Draw the background
            LinearGradientBrush myBrush = new LinearGradientBrush(e.Bounds, myColor1, myColor2, 45);
            e.Graphics.FillRectangle(myBrush, e.Bounds);
            e.Graphics.DrawRectangle(SystemPens.ControlDark, e.Bounds.X, e.Bounds.Y, e.Bounds.Width - 1, e.Bounds.Height - 1);
            #endregion

            #region Notify the grid that the background has been drawn and there is no need to draw it
            e.DoDefault = false;
            #endregion
        }

        private void fGridnormal_EllipsisButtonClick(object sender, iGEllipsisButtonClickEventArgs e)
        {
            var _cell = fGridnormal.Cells[e.RowIndex, e.ColIndex];
            if (_cell.AuxValue != null)
            {
                using (var _fm = new ViewQMYExpenses())
                {
                    _fm.Tag = _cell.AuxValue;
                    _fm.ShowDialog();
                }
            }
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            checkBox1.Text = checkBox1.Checked ? "UnCheck To Show Sub Departments" : "Hide Sub Departments";

            foreach (var _grid in new iGrid[] { fGrid, fGridnormal })
            {
                _grid.BeginUpdate();
                ic.accountC _acc = null;
                iGRow _row = null;
                var _row_list = from _r in _grid.Rows.Cast<iGRow>()
                                where _r.Level == 0 & _r.Cells["has_children"].Value != null
                                select _r;
                foreach (var k in _row_list)
                {
                    try
                    {
                        _row = k;
                        _row.Expanded = false;
                        _row.TreeButton = checkBox1.Checked ? iGTreeButtonState.Absent : iGTreeButtonState.Visible;
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                    //
                    if ((_row.Index + 1) >= _grid.Rows.Count - 1) { break; }
                    for (int j = (_row.Index + 1); j < _grid.Rows.Count; j++)
                    {
                        if (_grid.Rows[j].Level == _row.Level)
                        {
                            break;
                        }
                        _grid.Rows[j].Visible = checkBox1.Checked ? false : true;
                    }
                }
                _grid.EndUpdate();
            }
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (app_working) { return; }
            m_showType = m_SHOW_TYPES[comboBoxEx1.SelectedItem.ToString()];
            tab1_loaded = false;
            tab2_loaded = false;
            LoadTabs();
       }
    }
}
