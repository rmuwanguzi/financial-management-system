using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;
using SdaHelperManager;
namespace MTOMS
{
    public partial class ViewSingleSabbathAccountDetails : DevComponents.DotNetBar.Office2007Form
    {
        public ViewSingleSabbathAccountDetails()
        {
            InitializeComponent();
        }
        int _account_id = 0;
        int _cg_id = 0;
        int _percent = 0;
        int _sab_fs_id = 0;
        int _year = 0;
        int _month = 0;
        private enum xoff_type
        {
            _none,
            singleSabbath,
            singleSabbathCombined,
            monthSingle,
            monthSingleCombined
        }
        private xoff_type m_xOffType = xoff_type._none;
        private void InitializeGridColumnMain()
        {
            #region Columns To Display
            List<iGrid> _grids = new List<iGrid>()
            {
                fGrid
            };
            Dictionary<string, string> grid_cols = new Dictionary<string, string>();
            grid_cols.Add("no", "No");
            grid_cols.Add("name", "Member Name");
            grid_cols.Add("amount", "Amount");
            grid_cols.Add("cg", "Cg");
            grid_cols.Add("rpt_no", "Receipt\nNo");
            grid_cols.Add("sab_date", "Sabbath\nDate");
            grid_cols.Add("account", "Account Name");
            iGCol myCol;
            foreach (var _grid in _grids)
            {

                foreach (var c in grid_cols)
                {
                    myCol = _grid.Cols.Add(c.Key, c.Value);
                    myCol.IncludeInSelect = true;
                    myCol.SortType = iGSortType.ByValue;
                    myCol.ColHdrStyle.BackColor = Color.Thistle;
                    //  myCol.ColHdrStyle.BackColor = Color.AntiqueWhite;
                    myCol.ColHdrStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    myCol.ColHdrStyle.ForeColor = Color.Black;
                    myCol.ColHdrStyle.Font = new Font("georgia", 13, FontStyle.Regular);

                }
                 _grid.Cols["amount"].CellStyle.FormatString = "{0:N0}";
                 _grid.Cols["amount"].CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
                 _grid.Cols["amount"].CellStyle.ForeColor = Color.DarkBlue;
                 foreach (var k in new string[] { "cg", "rpt_no", "sab_date","account" })
                 {
                     _grid.Cols[k].CellStyle.ForeColor = Color.Gray;
                 }
                _grid.DefaultRow.Height = _grid.GetPreferredRowHeight(true, false);
                _grid.DefaultAutoGroupRow.Height = _grid.DefaultRow.Height;
                _grid.Cols.AutoWidth();
            }

            #endregion
        }
        private void ViewSingleSabbathAccountDetails_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            InitializeGridColumnMain();
            var ret_array = this.Tag.ToString().Split(new char[] { '|' });
            if (ret_array != null && ret_array.Length != 0)
            {
                switch (ret_array[0].ToString())
                {
                    case "sof":
                        {
                            _account_id = ret_array[1].ToInt32();
                            _sab_fs_id = ret_array[2].ToInt32();
                            _cg_id = ret_array[3].ToInt32();
                            _percent = ret_array[4].ToInt32();
                            m_xOffType = xoff_type.singleSabbath;
                            break;
                        }
                    case "cmb_of":
                        {
                            _account_id = ret_array[1].ToInt32();
                            _sab_fs_id = ret_array[2].ToInt32();
                            _cg_id = ret_array[3].ToInt32();
                            _percent = ret_array[4].ToInt32();
                            m_xOffType = xoff_type.singleSabbathCombined;
                            break;
                        }
                    case "m_sof":
                        {
                            _account_id = ret_array[1].ToInt32();
                            _cg_id = ret_array[2].ToInt32();
                            _percent = ret_array[3].ToInt32();
                            _month = ret_array[5].ToInt32();
                            _year = ret_array[4].ToInt32();
                            m_xOffType = xoff_type.monthSingle;
                            break;
                        }
                    case "m_cmb_of":
                        {
                            _account_id = ret_array[1].ToInt32();
                            _cg_id = ret_array[2].ToInt32();
                            _percent = ret_array[3].ToInt32();
                            _month = ret_array[5].ToInt32();
                            _year = ret_array[4].ToInt32();
                            m_xOffType = xoff_type.monthSingleCombined;
                            break;
                        }
                }

            }
            if (_account_id == 0)
            {
                this.Close();
                return;
            }
            if (_cg_id <= 0)
            {
                fGrid.Cols["cg"].Visible = false;
            }
            else
            {
                var cg = datam.DATA_CHURCH_GROUPS[_cg_id];
                fGrid.Cols["cg"].Text = datam.DATA_CG_TYPES[cg.cg_type_id].cg_type_name;
            }
            backworker.RunWorkerAsync();
        }

        private void backworker_DoWork(object sender, DoWorkEventArgs e)
        {
            datam.SystemInitializer();
            using (var _xd = new xing())
            {
                datam.fill_accounts(_xd);
                _xd.CommitTransaction();
            }
        }
         private void backworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string _str=null;
            bool isSingleRecord = false;
            switch (m_xOffType)
            {
                case xoff_type.singleSabbath:
                    {
                        isSingleRecord = true;
                        #region single_sabbath
                        if (_cg_id > -1)
                        {
                            _str = string.Format("select m.off_id,m.source_name,m.sab_date,m.receipt_status,c.amount,c.cg_id,c.account_id,c.un_id from off_main_tb as m,off_accounts_tb as c" +
                                         " where m.sab_fs_id={0} and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={1} and c.receipt_status=1 and cg_id={2} order by c.cg_id", _sab_fs_id, _account_id, _cg_id);
                           
                        }
                        else
                        {
                            _str = string.Format("select m.off_id,m.source_name,m.sab_date,m.receipt_status,c.amount,c.cg_id,c.account_id,c.un_id from off_main_tb as m,off_accounts_tb as c" +
                                        " where m.sab_fs_id={0} and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={1} and c.receipt_status=1 order by c.cg_id ", _sab_fs_id, _account_id);
                        }
                        #endregion
                        break;
                    }
                case xoff_type.singleSabbathCombined:
                    {
                        isSingleRecord = true;
                        #region single_sabbath_combined
                        var _accounts = accn.GetChildAccounts("COMBINED_OFFERING", em.account_typeS.ActualAccount).Select(l => l.account_id).ToList();
                        string _in_str = "(";
                        foreach(var k in _accounts)
                        {
                            _in_str += (_in_str.Length == 1) ? string.Format("{0}", k) : string.Format(",{0}", k);
                        }
                        _in_str += ")";
                        _str = string.Format("select m.off_id,m.source_name,m.sab_date,m.receipt_status,c.amount,c.cg_id,c.account_id,c.un_id from off_main_tb as m,off_accounts_tb as c" +
                                         " where m.sab_fs_id={0} and m.off_id=c.off_id and m.receipt_status=1 and c.account_id in {1} and c.receipt_status=1 order by c.account_id ", _sab_fs_id, _in_str);
                        #endregion
                        break;
                    }
                case xoff_type.monthSingle:
                    {
                        var _fs_ids = fn.GetFsIDMonth(_month, _year);
                        #region month_single_sabbath
                        if (_cg_id > -1)
                        {
                            _str = string.Format("select m.off_id,m.source_name,m.sab_date,m.receipt_status,c.amount,c.cg_id,c.account_id,c.un_id from off_main_tb as m,off_accounts_tb as c" +
                                         " where m.sab_fs_id between {0} and {1} and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={2} and c.receipt_status=1 and cg_id={3} order by m.sab_fs_id, c.source_type_id,c.source_id,c.cg_id", _fs_ids[0], _fs_ids[1], _account_id, _cg_id);
                        }
                        else
                        {
                            _str = string.Format("select m.off_id,m.source_name,m.sab_date,m.receipt_status,c.amount,c.cg_id,c.account_id,c.un_id from off_main_tb as m,off_accounts_tb as c" +
                                        " where m.sab_fs_id between {0} and {1}  and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={2} and c.receipt_status=1 order by m.sab_fs_id, c.source_type_id,c.source_id ", _fs_ids[0], _fs_ids[1], _account_id);
                        }
                        #endregion
                        break;
                    }
                case xoff_type.monthSingleCombined:
                    {
                        #region month_single_combined
                        var _fs_ids = fn.GetFsIDMonth(_month, _year);
                        var _accounts = accn.GetChildAccounts("COMBINED_OFFERING", em.account_typeS.ActualAccount).Select(l => l.account_id).ToList();
                        string _in_str = "(";
                        foreach (var k in _accounts)
                        {
                            _in_str += (_in_str.Length == 1) ? string.Format("{0}", k) : string.Format(",{0}", k);
                        }
                        _in_str += ")";
                        _str = string.Format("select m.off_id,m.source_name,m.sab_date,m.receipt_status,c.amount,c.cg_id,c.account_id,c.un_id from off_main_tb as m,off_accounts_tb as c" +
                                         " where m.sab_fs_id between {0} and {1} and m.off_id=c.off_id and m.receipt_status=1 and c.account_id in {2} and  c.receipt_status=1 order by m.sab_fs_id, c.source_type_id,c.source_id,c.account_id ", _fs_ids[0], _fs_ids[1], _in_str);
                        #endregion
                        break;
                    }
            }
            datam.ShowWaitForm("Loading Data, Please Wait");
            Application.DoEvents();
            fGrid.BeginUpdate();
            fGrid.Rows.Clear();
            using (var xd = new xing())
            {
                using (var _dr = xd.SelectCommand(_str))
                {
                    iGRow _row = null;
                    DateTime? _date = null;
                    int cg_id = 0;
                    int _total = 0;
                    int _acc_id = 0;
                    while (_dr.Read())
                    {
                        _row = fGrid.Rows.Add();
                        _row.Font = new Font("georgia", 12, FontStyle.Regular);
                         _row.ReadOnly = iGBool.True;
                        _row.Cells["no"].Value = fGrid.Rows.Count;
                        _row.Cells["name"].Value = _dr["source_name"].ToStringNullable();
                      //  _row.Cells["amount"].Value = _percent == 0 ? _dr["amount"].ToInt32() : ((_percent * _dr["amount"].ToInt32()) / 100);
                        _row.Cells["amount"].Value = _dr["amount"].ToInt32();
                        _row.Cells["rpt_no"].Value = "R-" + _dr["off_id"].ToStringNullable();
                        _total += _row.Cells["amount"].Value.ToInt32();
                        //
                        _date = _dr.GetDateTime("sab_date");
                        _row.Cells["sab_date"].Value = _date.Value.ToMyShortDate();
                        cg_id = _dr["cg_id"].ToInt32();
                        _acc_id = _dr["account_id"].ToInt32();
                        if (cg_id > 0)
                        {
                            _row.Cells["cg"].Value = datam.DATA_CHURCH_GROUPS[cg_id].cg_name;
                            _row.Cells["account"].Value = string.Format("{0} :: {1}", datam.DATA_ACCOUNTS[_acc_id].account_name, datam.DATA_CHURCH_GROUPS[cg_id].cg_name);
                            if (!_row.Cells["cg"].Col.Visible)
                            {
                                try
                                {
                                    _row.Cells["cg"].Col.Text = datam.DATA_CG_TYPES[datam.DATA_CHURCH_GROUPS[cg_id].cg_type_id].cg_type_name;
                                    _row.Cells["cg"].Col.Visible = true;
                                }
                                catch (Exception)
                                {
                                    
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                _row.Cells["account"].Value = datam.DATA_ACCOUNTS[_acc_id].account_name;
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Halla");
                            }
                        }
                        _row.Key = _dr["un_id"].ToStringNullable();
                        _row.TextAlign = iGContentAlignment.MiddleCenter;
                        _row.AutoHeight();
                        _row.Height += 1;
                    }
                    switch (m_xOffType)
                    {
                        case xoff_type.singleSabbath:
                            {
                                if (_cg_id == 0)
                                {
                                    labelTopName.Text = string.Format("{0} General Report for  {1}  :::  {2}", datam.DATA_ACCOUNTS[_account_id].account_name, _date.Value.ToMyShortDate(), _total.ToNumberDisplayFormat());
                                    break;
                                }
                                if (_cg_id < 0)
                                {
                                    labelTopName.Text = string.Format("{0}  Report for  {1}  :::  {2}", datam.DATA_ACCOUNTS[_account_id].account_name, _date.Value.ToMyShortDate(), _total.ToNumberDisplayFormat());
                                    break;
                                }
                                if (_cg_id > 0)
                                {
                                    labelTopName.Text = string.Format("{0}  Report for  {1}  :::  {2}", string.Format("{0} :: {1}", datam.DATA_ACCOUNTS[_account_id].account_name, datam.DATA_CHURCH_GROUPS[_cg_id].cg_name), _date.Value.ToMyShortDate(), _total.ToNumberDisplayFormat());
                                }
                                break;
                            }
                        case xoff_type.singleSabbathCombined:
                            {
                                labelTopName.Text = string.Format("Combined Offerings  Report for  {0}  :::  {1}", _date.Value.ToMyShortDate(), _total.ToNumberDisplayFormat());
                                break;
                            }
                        case xoff_type.monthSingle:
                            {
                                if (_cg_id == 0)
                                {
                                    labelTopName.Text = string.Format("{0} General Report for  {1}  :::  {2}", datam.DATA_ACCOUNTS[_account_id].account_name, string.Format("{0} {1}", datam.MONTHS[_month - 1], _date.Value.Year), _total.ToNumberDisplayFormat());
                                    break;
                                }
                                if (_cg_id < 0)
                                {
                                    labelTopName.Text = string.Format("{0}  Report for  {1}  :::  {2}", datam.DATA_ACCOUNTS[_account_id].account_name, string.Format("{0} {1}", datam.MONTHS[_month - 1], _date.Value.Year), _total.ToNumberDisplayFormat());
                                    break;
                                }
                                if (_cg_id > 0)
                                {
                                    labelTopName.Text = string.Format("{0}  Report for  {1}  :::  {2}", string.Format("{0} :: {1}", datam.DATA_ACCOUNTS[_account_id].account_name, datam.DATA_CHURCH_GROUPS[_cg_id].cg_name), string.Format("{0} {1}", datam.MONTHS[_month - 1], _date.Value.Year), _total.ToNumberDisplayFormat());
                                } break;
                            }
                        case xoff_type.monthSingleCombined:
                            {
                                labelTopName.Text = string.Format("Combined Offerings  Report for  {0}  :::  {1}", string.Format("{0} {1}", datam.MONTHS[_month - 1], _date.Value.Year), _total.ToNumberDisplayFormat());
                                break;

                            }
                    }
                   
                }
            }
            if (fGrid.SortObject.Count == 0)
            {
                fGrid.SortObject.Add("amount", iGSortOrder.Descending);
                fGrid.Sort();
            }
            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
            fGrid.EndUpdate();
            datam.HideWaitForm();
        }

         private void exportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
         {
             string _str = "Are You Sure You Want To Print This Data ?";
             if (!dbm.WarningMessage(_str, "Print Warning"))
             {
                 return;
             }
             fnn.PrintIGridNormal(fGrid, labelTopName.Text, new string[] { "no" });


         }

         private void button1_Click(object sender, EventArgs e)
         {
             string _str = "Are You Sure You Want To Print This Data ?";
             if (!dbm.WarningMessage(_str, "Print Warning"))
             {
                 return;
             }
             fnn.PrintIGridNormal(fGrid, labelTopName.Text, new string[] { "no" });
             //
            // fnn.PrintIGridNormal(fGrid, labelTopName.Text, null);
         }

         private void fGrid_AfterContentsSorted(object sender, EventArgs e)
         {
             fGrid.BeginUpdate();
             for (var t = 1; t <= fGrid.Rows.Count; t++)
             {
                 fGrid.Rows[t - 1].Cells["no"].Value = t;
             }
            
             fGrid.Cols.AutoWidth();
             fGrid.AutoResizeCols = false;
             fGrid.EndUpdate();  
             //
             if (fGrid.Rows.Count > 0)
             {
                 fGrid.SetCurCell(0, 0);
             }
         }

        private void contextMenuStripReplace_Opening(object sender, CancelEventArgs e)
        {
            if (fGrid.SelectedRows.Count == 0)
            {
                e.Cancel = true;
                return;
            }
            if (fGrid.SelectedRows[0].Tag != null)
            {
                e.Cancel = true;
                return;
            }
            
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (var _fm = new OfferingIncomeAccountSabbathReplacer())
            {
                var _key = fGrid.SelectedRows[0].Key;
                _fm.Tag = _key;
                _fm.Owner = this;
                _fm.ShowDialog();
                if (_fm.Tag != null)
                {
                    fGrid.Rows.RemoveAt(fGrid.Rows[_key].Index);
                }
            }
        }
    }
}
