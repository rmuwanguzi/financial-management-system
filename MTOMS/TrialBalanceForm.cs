using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SdaHelperManager;
using SdaHelperManager.Security;
using TenTec.Windows.iGridLib;

namespace MTOMS
{
    public partial class TrialBalanceForm : DevComponents.DotNetBar.Office2007Form
    {
        public TrialBalanceForm()
        {
            InitializeComponent();
        }
        int m_sel_year = 0;
        private int _fs_id_1 = 0;
        private int _fs_id_2 = 0;
        bool app_working = false;
        bool special_day = false;
        bool prev_year = false;
        private void TrailBalanceForm_Load(object sender, EventArgs e)
        {
            datam.SecurityCheck();
            datam.ShowWaitForm();
            Application.DoEvents();
            app_working = true;
            dateTimePicker1.ValueChanged += new EventHandler(dateTimePicker1_ValueChanged);
            dateTimePicker2.ValueChanged += new EventHandler(dateTimePicker2_ValueChanged);
            dateTimePicker1.Value = dateTimePicker1.MinDate;
            dateTimePicker2.Value = dateTimePicker1.MinDate;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            app_working = false;
            comboyear.Enabled = false;
            backworker1.RunWorkerAsync();
            
        }
        private void InitializeGridColumnMain()
        {
            #region Columns To Display
            List<iGrid> _grids = new List<iGrid>()
            {
                iGrid1
            };
            Dictionary<string, string> grid_cols = new Dictionary<string, string>();
            grid_cols.Add("account", "Account Name");
            grid_cols.Add("dr", "Dr\nAmount");
            grid_cols.Add("cr", "Cr\nAmount");
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
                    myCol.AllowSizing = true;
                }
                _grid.Cols["account"].Width = (iGrid1.Width / 2) + 20;
                _grid.Cols["dr"].Width = (((iGrid1.Width - _grid.Cols["account"].Width) / 2) - iGrid1.RowHeader.Width) - 2;
                _grid.Cols["cr"].Width = _grid.Cols["dr"].Width;
                _grid.Cols["dr"].CellStyle.FormatString = "{0:N0}";
                _grid.Cols["cr"].CellStyle.FormatString = "{0:N0}";
                _grid.DefaultRow.Height = _grid.GetPreferredRowHeight(true, false);
                _grid.DefaultAutoGroupRow.Height = _grid.DefaultRow.Height;
               // _grid.Cols.AutoWidth();
            }

            #endregion
        }
        private void InitializeColumnsPL()
        {
            #region Columns To Display
            List<iGrid> _grids = new List<iGrid>()
            {
                iGridPL
            };
            Dictionary<string, string> grid_cols = new Dictionary<string, string>();
            grid_cols.Add("account", "Account Name");
            grid_cols.Add("amount", "Amount");
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
                    myCol.AllowSizing = true;
                }
                _grid.Cols["account"].Width = (iGrid1.Width / 2) + 20;
                _grid.Cols["amount"].Width = (((iGrid1.Width - _grid.Cols["account"].Width) / 2) - iGrid1.RowHeader.Width) - 2;
                _grid.Cols["amount"].CellStyle.FormatString = "{0:N0}";
                _grid.DefaultRow.Height = _grid.GetPreferredRowHeight(true, false);
                _grid.DefaultAutoGroupRow.Height = _grid.DefaultRow.Height;
                // _grid.Cols.AutoWidth();
            }

            #endregion
        }
        private void InitializeColumnsBS()
        {
            #region Columns To Display
            List<iGrid> _grids = new List<iGrid>()
            {
                iGridBS
            };
            Dictionary<string, string> grid_cols = new Dictionary<string, string>();
            grid_cols.Add("account", "Particulars");
            grid_cols.Add("asset", "Dr\nAmount");
            grid_cols.Add("lb", "Cr\nAmount");
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
                    myCol.AllowSizing = true;
                }
                _grid.Cols["account"].Width = (iGrid1.Width / 2) + 20;
                _grid.Cols["asset"].Width = (((iGrid1.Width - _grid.Cols["account"].Width) / 2) - iGrid1.RowHeader.Width) - 2;
                _grid.Cols["asset"].Width += 10;
                _grid.Cols["lb"].Width = _grid.Cols["asset"].Width;
                _grid.Cols["asset"].CellStyle.FormatString = "{0:N0}";
                _grid.Cols["lb"].CellStyle.FormatString = "{0:N0}";
                _grid.DefaultRow.Height = _grid.GetPreferredRowHeight(true, false);
                _grid.DefaultAutoGroupRow.Height = _grid.DefaultRow.Height;
                // _grid.Cols.AutoWidth();
            }

            #endregion

        }
        private void LoadYears()
        {
            comboyear.Items.Clear();
            for (int i = fn.GetServerDate().Year; i >= 2010; i--)
            {
                comboyear.Items.Add(i.ToString());
            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            if (app_working) { return; }
            app_working = true;
            if (special_day)
            {
                dateTimePicker2.MaxDate = dateTimePicker1.MaxDate;
                dateTimePicker2.MinDate = dateTimePicker1.Value;
            }
            else
            {
                dateTimePicker2.MaxDate = sdata.CURR_DATE;
                dateTimePicker2.MinDate = dateTimePicker1.Value;

            }

            if (!prev_year & checkdate.Checked == false)
            {
                dateTimePicker2.Value = dateTimePicker1.Value;
            }
            Application.DoEvents();
            _fs_id_1 = fn.GetFSID(dateTimePicker1.Value);
            _fs_id_2 = fn.GetFSID(dateTimePicker2.Value);
            if (_fs_id_1 > _fs_id_2)
            {
                app_working = true;
                dateTimePicker2.Value = dateTimePicker1.Value;
                _fs_id_2 = _fs_id_1;
                app_working = false;
            }
            if (timer1.Enabled) { timer1.Enabled = false; }
            timer1.Enabled = true;

            app_working = false;
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (app_working) { return; }
            Application.DoEvents();
            _fs_id_1 = fn.GetFSID(dateTimePicker1.Value);
            _fs_id_2 = fn.GetFSID(dateTimePicker2.Value);
            if (timer1.Enabled) { timer1.Enabled = false; }
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
             clear_grids();
             CreateData(_fs_id_1, _fs_id_2);
            if (!dateTimePicker1.Enabled)
            {
                dateTimePicker1.Enabled = true;
            }
            buttonrefresh.Enabled = true;
        }
        private void clear_grids()
        {
            iGrid1.Rows.Clear();
            iGridPL.Rows.Clear();
            iGridBS.Rows.Clear();
        }
        private void CreateData(int _fs_id_1, int _fs_id_2)
        {
            if (tabControl1.SelectedTab == tabTrialBalance & iGrid1.Rows.Count==0)
            {
                int _hlp_val = 0;
                using (var xd = new xing())
                {
                     #region trial balance
                    datam.fill_accounts(xd);
                    iGrid1.BeginUpdate();
                    var _list = accn.GetTrialBalanceData(xd, _fs_id_1, _fs_id_2);
                     if (_list != null)
                    {
                        iGRow _row = null;
                        var nlist = from k in _list
                                    orderby k.j_section.ToByte()
                                    group k by k.j_section into nw_gp
                                    select new
                                    {
                                        section = nw_gp.Key,
                                        _data = nw_gp
                                    };
                        int _sum = 0;
                        foreach (var k in nlist)
                        {
                            _row = iGrid1.Rows.Add();
                            _row.Font = new Font("georgia", 13, FontStyle.Regular);
                            _row.ForeColor = Color.DarkBlue;
                            _row.Key = string.Format("S{0}", k.section.ToByte());
                            _row.TreeButton = iGTreeButtonState.Visible;
                            _row.Level = 0;
                            _row.ReadOnly = iGBool.True;
                            _row.AutoHeight();
                            _sum = k._data.Sum(d => d.FinValue);
                            if (_sum > 0)
                            {
                                _row.Cells["dr"].AuxValue = _sum;
                            }
                            else
                            {
                                _row.Cells["cr"].AuxValue = Math.Abs(_sum);
                            }
                            #region GetSectionName
                            switch (k.section)
                            {
                                case em.j_sectionS.cash:
                                    {
                                        _row.Cells["account"].Value = "Cash";
                                        break;
                                    }
                                case em.j_sectionS.bank:
                                    {
                                        _row.Cells["account"].Value = "Bank";
                                        break;
                                    }
                                case em.j_sectionS.creditor:
                                    {
                                        _row.Cells["account"].Value = "Creditors";
                                        break;
                                    }
                                case em.j_sectionS.debtor:
                                    {

                                        _row.Cells["account"].Value = "Debtors";
                                        break;
                                    }
                                case em.j_sectionS.open_bal_equity:
                                    {
                                        _row.Cells["account"].Value = "Equity";
                                        break;
                                    }
                                case em.j_sectionS.expense:
                                    {
                                        _row.Cells["account"].Value = "Expenses (Accrued)";
                                        break;
                                    }
                                case em.j_sectionS.income:
                                    {
                                        _row.Cells["account"].Value = "Income";
                                        break;
                                    }
                                case em.j_sectionS.loss:
                                    {
                                        _row.Cells["account"].Value = "Losses";
                                        break;
                                    }
                            }
                            #endregion
                            foreach (var c in k._data)
                            {
                                _hlp_val = Math.Abs(c.FinValue);
                                if (_hlp_val == 0) { continue; }
                                _row = iGrid1.Rows.Add();
                                _row.ReadOnly = iGBool.True;
                                _row.Level = 1;
                                _row.TreeButton = iGTreeButtonState.Hidden;
                                _row.Font = new Font("georgia", 12, FontStyle.Regular);
                                _row.ForeColor = Color.FromArgb(64, 64, 64);
                                _row.Cells["account"].Value = datam.DATA_ACCOUNTS[c.account_id].account_name;
                                _row.AutoHeight();
                                if (c.FinValue > 0)
                                {
                                    _row.Cells["dr"].Value = c.FinValue;
                                }
                                else
                                {
                                    _row.Cells["cr"].Value = Math.Abs(c.FinValue);
                                }
                            }
                        }
                        #region summary
                        _row = iGrid1.Rows.Add();
                        _row.Selectable = false;
                        _row.Height = 5;
                        _row.BackColor = Color.DarkGray;
                        _row = iGrid1.Rows.Add();
                        _row.Font = new Font("Verdana", 10, FontStyle.Bold);
                        _row.ReadOnly = iGBool.True;
                        _row.Cells["account"].Value = null;
                        _row.Cells["dr"].TextAlign = iGContentAlignment.MiddleCenter;
                        _row.Cells["dr"].Value = null;
                        //
                        _row.Cells["account"].Value = null;
                        _row.Cells["cr"].TextAlign = iGContentAlignment.MiddleCenter;
                        _row.Cells["cr"].Value = null;
                        _row.ForeColor = Color.Maroon;
                        _row.Height += 8;
                        _row.Key = "total";
                        _row = iGrid1.Rows.Add();
                        _row.Selectable = false;
                        _row.Height = 5;
                        _row.BackColor = Color.DarkGray;

                        #endregion
                        iGrid1.Rows["total"].Cells["dr"].Value = (from k in iGrid1.Cols["dr"].Cells.Cast<iGCell>()
                                                                  where k.Value != null
                                                                  select k.Value.ToInt32()).Sum();
                        iGrid1.Rows["total"].Cells["cr"].Value = (from k in iGrid1.Cols["cr"].Cells.Cast<iGCell>()
                                                                  where k.Value != null
                                                                  select k.Value.ToInt32()).Sum();
                       
                        iGrid1.PerformAction(iGActions.CollapseAll);
                        var nres = from k in iGrid1.Rows.Cast<iGRow>()
                                   where k.TreeButton == iGTreeButtonState.Visible
                                   select k;
                        foreach (var k in nres)
                        {
                            k.Cells["dr"].Value = k.Cells["dr"].AuxValue;
                            k.Cells["cr"].Value = k.Cells["cr"].AuxValue;
                        }
                        iGrid1.Cols["dr"].CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
                        iGrid1.Cols["cr"].CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    }
                     iGrid1.EndUpdate();
                    #endregion
                } 
           }
           if (tabControl1.SelectedTab == tabPL & iGridPL.Rows.Count==0)
           {
               using (var xd = new xing())
               {
                   #region profit and loss a/c
                   iGridPL.BeginUpdate();
                   iGridPL.Cols["amount"].CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
                   var _list = accn.GetProfitAndLossData(xd, _fs_id_1, _fs_id_2);
                   bool expenses_drawn = false;
                   int _hlp_val = 0;
                   if (_list != null)
                   {
                       iGRow _row = null;
                       var nlist = from k in _list
                                   orderby k.d_type.ToByte(),k.j_section.ToByte()
                                   group k by k.d_type into nw_gp
                                   select new
                                   {
                                       section = nw_gp.Key,
                                       _data = nw_gp
                                   };
                       int _sum = 0;
                       foreach (var k in nlist.OrderByDescending(j=>j.section.ToByte()))
                       {
                           if (k.section == em.account_d_typeS.Income)
                           {
                               #region income section
                               var olist = from s in k._data
                                           group s by fnn.IsOffertory(s.account_id)
                                               into nw_gp
                                               select new
                                               {
                                                   is_main = (nw_gp.Key == true) ? 0 : 1,
                                                   nw_data = nw_gp
                                               };
                               foreach (var v in olist.OrderBy(p => p.is_main))
                               {
                                   if (v.is_main == 0)
                                   {
                                       _row = iGridPL.Rows.Add();
                                       _row.Font = new Font("georgia", 13, FontStyle.Regular);
                                       _row.ForeColor = Color.DarkBlue;
                                       _row.Key = string.Format("S{0}", k.section.ToByte());
                                       _row.TreeButton = iGTreeButtonState.Visible;
                                       _row.Level = 0;
                                       _row.ReadOnly = iGBool.True;
                                       _row.Cells["account"].Value = "Income From Offertory";
                                       _row.Cells["amount"].AuxValue = Math.Abs(v.nw_data.Sum(g => g.FinValue));
                                       _row.AutoHeight();
                                       _sum += (_row.Cells["amount"].AuxValue).ToInt32();
                                   }
                                   else
                                   {
                                       _row = iGridPL.Rows.Add();
                                       _row.Font = new Font("georgia", 13, FontStyle.Regular);
                                       _row.ForeColor = Color.DarkBlue;
                                       _row.Key = string.Format("S{0}", k.section.ToByte());
                                       _row.TreeButton = iGTreeButtonState.Visible;
                                       _row.Level = 0;
                                       _row.ReadOnly = iGBool.True;
                                       _row.Cells["amount"].AuxValue = Math.Abs(v.nw_data.Sum(g => g.FinValue));
                                       _row.Cells["account"].Value = "Other Income";
                                       _row.AutoHeight();
                                       _sum += (_row.Cells["amount"].AuxValue).ToInt32();
                                   }
                                   foreach (var c in v.nw_data)
                                   {
                                       _hlp_val = Math.Abs(c.FinValue);
                                       if (_hlp_val == 0) { continue; }
                                       _row = iGridPL.Rows.Add();
                                       _row.ReadOnly = iGBool.True;
                                       _row.Level = 1;
                                       _row.TreeButton = iGTreeButtonState.Hidden;
                                       _row.Font = new Font("georgia", 12, FontStyle.Regular);
                                       _row.ForeColor = Color.FromArgb(64, 64, 64);
                                       _row.Cells["account"].Value = datam.DATA_ACCOUNTS[c.account_id].account_name;
                                       _row.AutoHeight();
                                       _row.Cells["amount"].Value = _hlp_val;
                                   }
                               } 
                               #endregion
                           }
                           else
                           {
                               #region expense section
                               var olist = from s in k._data
                                           where (s.j_section==em.j_sectionS.expense_accrued | s.j_section== em.j_sectionS.expense)
                                           group s by s.j_section
                                               into nw_gp
                                               select new
                                               {
                                                   is_main = (nw_gp.Key==em.j_sectionS.expense_accrued) ? 0 : 1,
                                                   nw_data = nw_gp
                                               };
                               foreach (var v in olist.OrderBy(p => p.is_main))
                               {
                                   if (v.is_main == 0)
                                   {
                                       _row = iGridPL.Rows.Add();
                                       _row.Font = new Font("georgia", 13, FontStyle.Regular);
                                       _row.ForeColor = Color.Maroon;
                                       _row.Key = string.Format("SE{0}", k.section.ToByte());
                                       _row.TreeButton = iGTreeButtonState.Visible;
                                       _row.Level = 0;
                                       _row.ReadOnly = iGBool.True;
                                       _row.Cells["account"].Value = "Less Church Creditors";
                                       _row.TextAlign = iGContentAlignment.MiddleLeft;
                                       _row.AutoHeight();
                                       _row.Cells["amount"].AuxValue = Math.Abs(v.nw_data.Sum(g => g.FinValue));
                                       _sum -= (_row.Cells["amount"].AuxValue).ToInt32();

                                   }
                                   else
                                   {
                                       //draw gross profit at this point
                                       #region summary
                                       _row = iGridPL.Rows.Add();
                                       _row.Selectable = false;
                                       _row.Height = 2;
                                       _row.BackColor = Color.DarkGray;
                                       _row = iGridPL.Rows.Add();
                                       _row.Font = new Font("Verdana", 10, FontStyle.Bold);
                                       _row.ReadOnly = iGBool.True;
                                       _row.Cells["account"].Value = _sum >= 0 ? "Gross Income" : "Gross Loss";
                                       _row.Cells["account"].TextAlign = iGContentAlignment.MiddleRight;
                                      
                                       _row.Cells["amount"].Value = _sum;
                                       _row.ForeColor = _sum >= 0 ? Color.DarkGreen : Color.DarkRed;
                                       //

                                       _row.Height += 8;
                                       _row.Key = "gross_profit";
                                       _row = iGridPL.Rows.Add();
                                       _row.Selectable = false;
                                       _row.Height = 2;
                                       _row.BackColor = Color.DarkGray;

                                       #endregion
                                       //////
                                       _row = iGridPL.Rows.Add();
                                       _row.Font = new Font("georgia", 13, FontStyle.Regular);
                                       _row.ForeColor = Color.Maroon;
                                       _row.Key = string.Format("SE{0}", k.section.ToByte());
                                       _row.TreeButton = iGTreeButtonState.Visible;
                                       _row.TextAlign = iGContentAlignment.MiddleLeft;
                                       _row.Level = 0;
                                       _row.ReadOnly = iGBool.True;
                                       _row.Key = "cexpense";
                                       _row.Cells["account"].Value = "Less Church Expenses/Losses";
                                       _row.Cells["amount"].AuxValue = Math.Abs(v.nw_data.Sum(g => g.FinValue));
                                       _sum -= (_row.Cells["amount"].AuxValue).ToInt32();
                                       _row.AutoHeight();
                                       expenses_drawn = true;
                                     //  _row.Expanded = false;
                                   }
                                   foreach (var c in v.nw_data)
                                   {
                                       _hlp_val = Math.Abs(c.FinValue);
                                       if (_hlp_val == 0) { continue; }
                                       _row = iGridPL.Rows.Add();
                                       _row.ReadOnly = iGBool.True;
                                       _row.Level = 1;
                                       _row.TreeButton = iGTreeButtonState.Hidden;
                                       _row.Font = new Font("georgia", 12, FontStyle.Regular);
                                       _row.ForeColor = Color.FromArgb(64, 64, 64);
                                       _row.Cells["account"].Value = v.is_main == 0 ? string.Format("{0} :: {1}", "CR", datam.DATA_ACCOUNTS[c.account_id].account_name) : datam.DATA_ACCOUNTS[c.account_id].account_name;
                                       _row.AutoHeight();
                                       _row.Cells["amount"].Value = _hlp_val;
                                   }
                                  
                               }
                               #endregion
                           }
                       }
                       #region summary
                       _row = iGridPL.Rows.Add();
                       _row.Selectable = false;
                       _row.Height = 5;
                       _row.BackColor = Color.DarkGray;
                       _row = iGridPL.Rows.Add();
                       _row.Font = new Font("Verdana", 10, FontStyle.Bold);
                       _row.ReadOnly = iGBool.True;
                       _row.Cells["account"].Value = _sum >= 0 ? "Net Income" : "Net Loss";
                       _row.Cells["account"].TextAlign = iGContentAlignment.MiddleRight;
                      
                       _row.Cells["amount"].Value = _sum;
                       //
                       _row.ForeColor = _sum >= 0 ? Color.DarkGreen : Color.DarkRed;
                     
                       _row.Height += 8;
                       _row.Key = "net_profit";
                       _row = iGridPL.Rows.Add();
                       _row.Selectable = false;
                       _row.Height = 5;
                       _row.BackColor = Color.DarkGray;

                       #endregion
                     
                   }
                   iGridPL.EndUpdate();
                   if (expenses_drawn)
                   {
                       iGridPL.Rows["cexpense"].Expanded = false;
                       iGridPL.Rows["cexpense"].Cells["amount"].Value = iGridPL.Rows["cexpense"].Cells["amount"].AuxValue;
                       iGridPL.Rows["cexpense"].Cells["amount"].TextAlign = iGContentAlignment.MiddleCenter;
                   }
                   #endregion
               } 
           }
           if (tabControl1.SelectedTab == tabBS & iGridBS.Rows.Count == 0)
           {
               iGRow _row = null;
               using (var xd = new xing())
               {
                   #region balance sheet
                   iGridBS.BeginUpdate();
                   iGridBS.Cols["asset"].CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
                   iGridBS.Cols["lb"].CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
                   int w_fs_id = _fs_id_1 > _fs_id_2 ? _fs_id_1 : _fs_id_2;
                   int _helper = 0;
                    #region main asset heading
                   _row = iGridBS.Rows.Add();
                   _row.Font = new Font("georgia", 13, FontStyle.Regular);
                   _row.ForeColor = Color.Maroon;
                   _row.TreeButton = iGTreeButtonState.Visible;
                   _row.Level = 0;
                   _row.ReadOnly = iGBool.True;
                   _row.Cells["account"].Value = "Assets";
                   _row.Cells["asset"].AuxValue = null;
                   _row.Key = "asset";
                   _row.AutoHeight();
                   #region cash in bank
                   _row = iGridBS.Rows.Add();
                   _row.ReadOnly = iGBool.True;
                   _row.Level = 1;
                   _row.TreeButton = iGTreeButtonState.Hidden;
                   _row.Font = new Font("georgia", 12, FontStyle.Regular);
                   _row.ForeColor = Color.Maroon;
                   _row.TreeButton = iGTreeButtonState.Visible;
                   _row.Cells["account"].Value = "Bank";
                   _row.Key = "bank";
                   _row.AutoHeight();
                   //
                   var b_list = accn.GetChildAccounts("BANK_ACCOUNT", em.account_typeS.ActualAccount).Select(k => k.account_id).ToList();
                   int _banked = 0;
                   foreach (var k in b_list)
                   {
                       if (datam.DATA_ACCOUNTS[k].account_status != em.account_statusS.Enabled) { continue; }
                       _row = iGridBS.Rows.Add();
                       _row.ReadOnly = iGBool.True;
                       _row.Level = 2;
                       _row.TreeButton = iGTreeButtonState.Hidden;
                       _row.Font = new Font("georgia", 12, FontStyle.Regular);
                       _row.ForeColor = Color.FromArgb(64, 64, 64);
                       _row.Cells["account"].Value = datam.DATA_ACCOUNTS[k].account_name;
                       _row.AutoHeight();
                       _row.Cells["asset"].Value = accn.GetFs_AccountBalance(xd, w_fs_id, k);
                       _banked += (_row.Cells["asset"].Value.ToInt32());
                   }
                   iGridBS.Rows["bank"].Cells["asset"].AuxValue = _banked;
                   _helper += _banked;
                   #endregion
                   #region unbanked
                   _row = iGridBS.Rows.Add();
                   _row.Font = new Font("georgia", 13, FontStyle.Regular);
                   _row.ForeColor = Color.Maroon;
                   _row.TreeButton = iGTreeButtonState.Visible;
                   _row.Level = 1;
                   _row.ReadOnly = iGBool.True;
                   _row.Cells["account"].Value = "UnBanked";
                   _row.Cells["asset"].AuxValue = null;
                   _row.Key = "unbanked";
                   _row.AutoHeight();
                   //
                   var _list = accn.GetChildAccounts("UNBANKED", em.account_typeS.All).Where(s => s.p_account_id != -2395).Select(k => k.account_id).ToList();
                   int _unbanked = 0;
                   int _unconverted = 0;
                   int _hlp_1 = 0;
                   float _conv_rate = 0f;
                   //
                  // sdata.HasDatabaseDependency()
                   foreach (var k in _list)
                   {
                        _row = iGridBS.Rows.Add();
                       _row.ReadOnly = iGBool.True;
                       _row.Level = 2;
                       _row.TreeButton = iGTreeButtonState.Hidden;
                       _row.Font = new Font("georgia", 12, FontStyle.Regular);
                       _row.ForeColor = Color.FromArgb(64, 64, 64);
                       _row.Cells["account"].Value = datam.DATA_ACCOUNTS[k].account_name;
                       if (k == -2395)
                       {
                           _row.ForeColor = Color.Maroon;
                           _row.TreeButton = iGTreeButtonState.Visible;
                           _row.Cells["asset"].AuxValue = null;
                           _row.Cells["asset"].Value = null;
                           _row.Key = "un_converted";
                           _row.AutoHeight();
                           var _currency_list = accn.GetChildAccounts("UNBANKED_FOREIGN", em.account_typeS.ActualAccount).Select(f => f.account_id).ToList();
                           _unconverted = 0;
                           foreach(var c in _currency_list)
                           {
                               _row = iGridBS.Rows.Add();
                               _row.ReadOnly = iGBool.True;
                               _row.Level = 3;
                               _row.TreeButton = iGTreeButtonState.Hidden;
                               _row.Font = new Font("georgia", 12, FontStyle.Regular);
                               _row.ForeColor = Color.FromArgb(64, 64, 64);
                               //
                               _hlp_1 = accn.GetFs_AccountBalance(xd, w_fs_id, c);
                               _conv_rate = accn.GetFS_ConversionRate(xd, w_fs_id, c);
                               _row.Cells["asset"].Value = _hlp_1;
                               //
                               if (_conv_rate > _hlp_1)
                               {
                                   _row.Cells["account"].Value = datam.DATA_ACCOUNTS[c].account_name;
                               }
                               else
                               {
                                   _row.Cells["account"].Value = string.Format("{0}  [ {1} ]", datam.DATA_ACCOUNTS[c].account_name, (_hlp_1 / _conv_rate).ToInt32());
                               }
                               _row.AutoHeight();
                               _unconverted += (_row.Cells["asset"].Value.ToInt32());
                           }
                           iGridBS.Rows["un_converted"].Cells["asset"].AuxValue = _unconverted;
                           _unbanked += _unconverted;
                       }
                       else
                       {
                           _row.Cells["asset"].Value = accn.GetFs_AccountBalance(xd, w_fs_id, k);
                           _row.AutoHeight();
                           _unbanked += (_row.Cells["asset"].Value.ToInt32());
                       }
                   }
                   iGridBS.Rows["unbanked"].Cells["asset"].AuxValue = _unbanked;
                   _helper += _unbanked;
                   #endregion
                   #region borrowed unbanked
                   //_row = iGridBS.Rows.Add();
                   //_row.ReadOnly = iGBool.True;
                   //_row.Level = 1;
                   //_row.TreeButton = iGTreeButtonState.Hidden;
                   //_row.Font = new Font("georgia", 12, FontStyle.Regular);
                   //_row.ForeColor = Color.Black;
                   //_row.Cells["account"].Value = "Borrowed From UnBanked";
                   //_row.Cells["asset"].Value = accn.GetFS_UnBanked(xd, w_fs_id, datam.GetAccountByAlias("BORROWED_UNBANKED").account_id);
                   //_row.AutoHeight();
                   //_helper += _row.Cells["asset"].Value.ToInt32();
                   #endregion
                   #region cash a/c
                   _row = iGridBS.Rows.Add();
                   _row.ReadOnly = iGBool.True;
                   _row.Level = 1;
                   _row.TreeButton = iGTreeButtonState.Hidden;
                   _row.Font = new Font("georgia", 12, FontStyle.Regular);
                   _row.ForeColor = Color.Black;
                   _row.Cells["account"].Value = "Pending Cheques";
                   _row.Cells["asset"].Value = accn.GetFs_AccountBalance(xd, w_fs_id, accn.GetAccountByAlias("WITHDRAWN_CHEQUES").account_id);
                 //  _row.Cells["asset"].Value = accn.GetFs_AccountBalance(xd, w_fs_id, -2500);
                   _row.AutoHeight();
                   _helper += _row.Cells["asset"].Value.ToInt32();
                   #endregion
                   iGridBS.Rows["asset"].Cells["asset"].AuxValue = _helper;
                   #endregion
                   _helper = 0;
                     #region main liability heading
                   _row = iGridBS.Rows.Add();
                   _row.Font = new Font("georgia", 13, FontStyle.Regular);
                   _row.ForeColor = Color.Maroon;
                   _row.TreeButton = iGTreeButtonState.Visible;
                   _row.Level = 0;
                   _row.ReadOnly = iGBool.True;
                   _row.Cells["account"].Value = "Liabilities";
                   _row.Cells["asset"].AuxValue = null;
                   _row.Key = "liability";
                   _row.AutoHeight();
                   #region creditors
                   _row = iGridBS.Rows.Add();
                   _row.ReadOnly = iGBool.True;
                   _row.Level = 1;
                   _row.TreeButton = iGTreeButtonState.Visible;
                   _row.Font = new Font("georgia", 12, FontStyle.Regular);
                   _row.ForeColor = Color.Maroon;
                   _row.Cells["account"].Value = "Creditors";
                   _row.Key = "creditor";
                   _row.AutoHeight();
                   _row.Cells["asset"].Value = null;
                   #endregion
                   #region sub creditors
                   var cr_list = accn.GetChildAccounts("ACC_PAYABLE", em.account_typeS.SubGroupAccount ).Select(k => k).ToList();
                   _helper = 0;
                   ic.accountC _parent = accn.GetAccountByAlias("ACC_PAYABLE");
                   foreach (var k in cr_list)
                   {
                       if (k.p_account_id != _parent.account_id) { continue; }
                       _row = iGridBS.Rows.Add();
                       _row.ReadOnly = iGBool.True;
                       _row.Level = 2;
                       _row.TreeButton = iGTreeButtonState.Hidden;
                       _row.Font = new Font("georgia", 12, FontStyle.Regular);
                       _row.ForeColor = Color.FromArgb(64, 64, 64);
                       _row.Cells["account"].Value = k.account_name;
                       _row.AutoHeight();
                       _row.Cells["lb"].Value = accn.GetFs_AccountBalance(xd, w_fs_id, k.account_id);
                       _helper += (_row.Cells["lb"].Value.ToInt32());
                   }
                   iGridBS.Rows["creditor"].Cells["lb"].AuxValue = _helper;
                   iGridBS.Rows["liability"].Cells["lb"].AuxValue = _helper;
                   #endregion
                   #endregion
                   #region equity
                   _helper = 0;
                   _row = iGridBS.Rows.Add();
                   _row.Font = new Font("georgia", 13, FontStyle.Regular);
                   _row.ForeColor = Color.DarkBlue;
                   _row.TreeButton = iGTreeButtonState.Visible;
                   _row.Level = 0;
                   _row.ReadOnly = iGBool.True;
                   _row.Cells["account"].Value = "Equity";
                   _row.Cells["asset"].AuxValue = null;
                   _row.Key = "equity";
                   _row.AutoHeight();
                   #region net profit
                   _row = iGridBS.Rows.Add();
                   _row.ReadOnly = iGBool.True;
                   _row.Level = 1;
                   _row.TreeButton = iGTreeButtonState.Hidden;
                   _row.Font = new Font("georgia", 12, FontStyle.Regular);
                   _row.ForeColor = Color.FromArgb(64, 64, 64);
                   _row.AutoHeight();
                   _helper = accn.GetFS_PL(xd, w_fs_id);
                   if(_helper<0)
                   {
                       _row.Cells["account"].Value = "Net Loss";
                       _row.ForeColor = Color.Red;
                       _row.Cells["asset"].Value = Math.Abs(_helper);
                       iGridBS.Rows["equity"].Cells["asset"].AuxValue = Math.Abs(_helper);
                   }
                   else
                   {
                       _row.Cells["account"].Value = "Net Gain";
                       _row.Cells["lb"].Value = _helper;
                       iGridBS.Rows["equity"].Cells["lb"].AuxValue = _helper;
                   }
                   #endregion
                   #region equity accounts
                   var nlist = accn.GetFS_Equity(xd, w_fs_id);
                   if (nlist != null)
                   {
                       foreach (var k in nlist)
                       {
                           _row = iGridBS.Rows.Add();
                           _row.ReadOnly = iGBool.True;
                           _row.Level = 1;
                           _row.TreeButton = iGTreeButtonState.Hidden;
                           _row.Font = new Font("georgia", 12, FontStyle.Regular);
                           _row.ForeColor = Color.FromArgb(64, 64, 64);
                           _row.Cells["account"].Value = datam.DATA_ACCOUNTS[k.account_id].account_name;
                           _row.AutoHeight();
                           _row.Cells["lb"].Value = k.cr;
                           _row.Cells["asset"].Value = k.dr;
                           ///
                           iGridBS.Rows["equity"].Cells["asset"].AuxValue = (iGridBS.Rows["equity"].Cells["asset"].AuxValue.ToInt32() + k.dr);
                           iGridBS.Rows["equity"].Cells["lb"].AuxValue = (iGridBS.Rows["equity"].Cells["lb"].AuxValue.ToInt32() + k.cr);
                           ///
                        }
                   }
                   #endregion
                   #endregion
                   iGridBS.EndUpdate();
                   #endregion
                   xd.CommitTransaction();
               }
               #region summary
               _row = iGridBS.Rows.Add();
               _row.Selectable = false;
               _row.Height = 5;
               _row.BackColor = Color.DarkGray;
               _row = iGridBS.Rows.Add();
               _row.Font = new Font("Verdana", 10, FontStyle.Bold);
               _row.ReadOnly = iGBool.True;
               _row.Cells["account"].Value = null;
               _row.Cells["asset"].TextAlign = iGContentAlignment.MiddleCenter;
               _row.Cells["asset"].Value = null;
               //
               _row.Cells["account"].Value = null;
               _row.Cells["lb"].TextAlign = iGContentAlignment.MiddleCenter;
               _row.Cells["lb"].Value = null;
               _row.ForeColor = Color.Maroon;
               _row.Height += 8;
               _row.Key = "total";
               _row = iGridBS.Rows.Add();
               _row.Selectable = false;
               _row.Height = 5;
               _row.BackColor = Color.DarkGray;

               #endregion

               iGridBS.Rows["total"].Cells["asset"].Value = (from k in iGridBS.Cols["asset"].Cells.Cast<iGCell>()
                                                         where k.Value != null
                                                         select k.Value.ToInt32()).Sum();
               iGridBS.Rows["total"].Cells["lb"].Value = (from k in iGridBS.Cols["lb"].Cells.Cast<iGCell>()
                                                         where k.Value != null
                                                         select k.Value.ToInt32()).Sum();
           }
        }
        private void comboyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboyear.SelectedIndex == -1)
            {
                app_working = true;
                dateTimePicker1.Value = DateTime.MinValue;
                dateTimePicker2.Value = DateTime.MinValue;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                checkdate.Checked = false;
                checkdate.Enabled = false;
                app_working = false;
                _fs_id_2 = 0;
                _fs_id_1 = 0;

                return;
            }
            Application.DoEvents();
            m_sel_year = comboyear.SelectedItem.ToInt16();
            app_working = true;
            checkdate.Checked = false;
            checkdate.Enabled = false;
            app_working = false;
            dateTimePicker2.Enabled = false;
            app_working = true;
            if (m_sel_year != sdata.CURR_DATE.Year)
            {
                dateTimePicker1.MinDate = new DateTime(m_sel_year, 1, 1);
                dateTimePicker1.MaxDate = new DateTime(m_sel_year, 12, 31);
                dateTimePicker1.Value = dateTimePicker1.MinDate;

                dateTimePicker2.MinDate = new DateTime(m_sel_year, 1, 1);
                dateTimePicker2.MaxDate = new DateTime(m_sel_year, 12, 31);
                dateTimePicker2.Value = dateTimePicker1.MinDate;
                prev_year = true;
            }
            else
            {
                prev_year = false;
                DateTime lower_date = new DateTime(m_sel_year, 1, 1);
                if (sdata.CURR_FS.fs_id == fn.GetFSID(lower_date))
                {
                    dateTimePicker1.MaxDate = sdata.CURR_DATE.AddDays(1);
                    dateTimePicker1.MinDate = new DateTime(m_sel_year, 1, 1);
                    dateTimePicker1.Value = dateTimePicker1.MinDate;
                    //
                    dateTimePicker2.MaxDate = dateTimePicker1.MaxDate;
                    dateTimePicker2.MinDate = dateTimePicker1.Value;
                    dateTimePicker2.Value = dateTimePicker1.Value;

                    special_day = true;
                }
                else
                {
                    dateTimePicker1.MaxDate = sdata.CURR_DATE;
                    dateTimePicker1.MinDate = new DateTime(m_sel_year, 1, 1);
                    dateTimePicker1.Value = dateTimePicker1.MaxDate;

                    dateTimePicker2.MaxDate = dateTimePicker1.MaxDate;
                    dateTimePicker2.MinDate = dateTimePicker1.Value;
                    dateTimePicker2.Value = dateTimePicker1.Value;
                    special_day = false;
                }

            }
            app_working = false;
            dateTimePicker1.Enabled = false;
            if (!dateTimePicker1.Enabled)
            {
              checkdate.Enabled = true;
            }
            if (!checkdate.Enabled)
            {
                checkdate.Enabled = true;
            }
            _fs_id_1 = fn.GetFSID(dateTimePicker1.Value);
            _fs_id_2 = fn.GetFSID(dateTimePicker2.Value);
            if (timer1.Enabled) { timer1.Enabled = false; }
            timer1.Enabled = true;
        }

        private void buttonrefresh_Click(object sender, EventArgs e)
        {
            buttonrefresh.Enabled = false;
            if (!timer1.Enabled)
            {
                timer1.Enabled = true;
            }
        }

        private void paneltop_SizeChanged(object sender, EventArgs e)
        {
            if (panelinnerpanel.Width > paneltop.Width) { return; }
            panelinnerpanel.Left = (paneltop.Width - panelinnerpanel.Width) / 2;
            panelmain.Left = (paneltop.Width - panelmain.Width) / 2;
        }

        private void backworker1_DoWork(object sender, DoWorkEventArgs e)
        {
            datam.SystemInitializer();
        }

        private void backworker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            InitializeGridColumnMain();
            InitializeColumnsPL();
            InitializeColumnsBS();
            LoadYears();
            comboyear.Enabled = true;
            datam.HideWaitForm();
        }

        private void checkdate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkdate.Checked)
            {
                dateTimePicker2.Enabled = true;
            }
            else
            {
                app_working = true;
                dateTimePicker2.Value = dateTimePicker1.Value;
                dateTimePicker2.Enabled = false;
                _fs_id_1 = fn.GetFSID(dateTimePicker1.Value);
                _fs_id_2 = fn.GetFSID(dateTimePicker2.Value);
                timer1.Start();
                app_working = false;
            }
        }

        private void tabControlPanel1_SizeChanged(object sender, EventArgs e)
        {
            iGrid1.Left = (tabControlPanel1.Width - iGrid1.Width) / 2 + tabControlPanel1.Left;
            iGrid1.Top = 10;
            iGrid1.Height = tabControlPanel1.Height - 20;
        }

        private void iGrid1_AfterRowStateChanged(object sender, iGAfterRowStateChangedEventArgs e)
        {
            if (iGrid1.Rows[e.RowIndex].Level == 0)
            {
                if (iGrid1.Rows[e.RowIndex].Expanded)
                {

                    iGrid1.Rows[e.RowIndex].Cells["dr"].Value = null;
                    iGrid1.Rows[e.RowIndex].Cells["cr"].Value = null;
                }
                else
                {
                    iGrid1.Rows[e.RowIndex].Cells["dr"].Value = iGrid1.Rows[e.RowIndex].Cells["dr"].AuxValue;
                    iGrid1.Rows[e.RowIndex].Cells["cr"].Value = iGrid1.Rows[e.RowIndex].Cells["cr"].AuxValue;
                }
            }
        }
        private void tabControlPanel2_SizeChanged(object sender, EventArgs e)
        {
            iGridPL.Left = (tabControlPanel2.Width - iGridPL.Width) / 2 + tabControlPanel2.Left;
            iGridPL.Top = 10;
            iGridPL.Height = tabControlPanel2.Height - 20;
        }

        private void iGridPL_AfterRowStateChanged(object sender, iGAfterRowStateChangedEventArgs e)
        {
            if (iGridPL.Rows[e.RowIndex].Level == 0)
            {
                if (iGridPL.Rows[e.RowIndex].Expanded)
                {
                    iGridPL.Rows[e.RowIndex].Cells["amount"].Value = null;
                }
                else
                {
                    iGridPL.Rows[e.RowIndex].Cells["amount"].Value = iGridPL.Rows[e.RowIndex].Cells["amount"].AuxValue;
                    iGridPL.Rows[e.RowIndex].Cells["amount"].TextAlign = iGContentAlignment.MiddleCenter;
                }
            }
        }
        private void panelmain_Click(object sender, EventArgs e)
        {

        }

        private void tabControlPanel3_SizeChanged(object sender, EventArgs e)
        {
            iGridBS.Left = (tabControlPanel3.Width - iGridBS.Width) / 2 + tabControlPanel3.Left;
            iGridBS.Top = 10;
            iGridBS.Height = tabControlPanel3.Height - 20;
        }

        private void iGridBS_AfterRowStateChanged(object sender, iGAfterRowStateChangedEventArgs e)
        {
            if (iGridBS.Rows[e.RowIndex].Expanded)
            {

                iGridBS.Rows[e.RowIndex].Cells["asset"].Value = null;
                iGridBS.Rows[e.RowIndex].Cells["lb"].Value = null;
            }
            else
            {
                iGridBS.Rows[e.RowIndex].Cells["asset"].Value = iGridBS.Rows[e.RowIndex].Cells["asset"].AuxValue;
                iGridBS.Rows[e.RowIndex].Cells["lb"].Value = iGridBS.Rows[e.RowIndex].Cells["lb"].AuxValue;
            }
        }

        private void tabControl1_SelectedTabChanged(object sender, DevComponents.DotNetBar.TabStripTabChangedEventArgs e)
        {
            if (_fs_id_1 > 0)
            {
                CreateData(_fs_id_1, _fs_id_2);
            }
        }

        private void paneltop_Click(object sender, EventArgs e)
        {

        }

        private void printStatementToolStripMenuItemSummary_Click(object sender, EventArgs e)
        {
            var _grid = contextMenuStrip1.SourceControl as iGrid;
            var _digit_cols = new string[] { "amount" };
            string _disp_name = string.Empty;
            // _disp_name = string.Format("System Account Balance As At {0}", dateTimeInput1.Value.ToMyLongDate());
            xcel.xcel.IGridLCBStatement(_grid, "sheet1", null, true, _digit_cols, true);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var _grid = contextMenuStrip1.SourceControl as iGrid;
            var _digit_cols = new string[] { "amount" };
            string _disp_name = string.Empty;
            // _disp_name = string.Format("System Account Balance As At {0}", dateTimeInput1.Value.ToMyLongDate());
            xcel.xcel.IGridLCBStatement(_grid, "sheet1", null, true, _digit_cols, true);
        }

        
    }
}
