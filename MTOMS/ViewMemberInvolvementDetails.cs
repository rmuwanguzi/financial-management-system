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
    public partial class ViewMemberInvolvementDetails : DevComponents.DotNetBar.Office2007Form
    {
        public ViewMemberInvolvementDetails()
        {
            InitializeComponent();
        }
        int _account_id = 0;
        int _cg_id = 0;
        int _percent = 0;
        int _sab_fs_id = 0;
        int _year = 0;
        int _month = 0;
        em.off_source_typeS m_source_type { get; set; }
        string m_ReportPeriod { get; set; }
        string m_ReportTotal { get; set; }
        string m_ReportMemberCount { get; set; }
        string m_ReportName { get; set; }
        string m_SourceTypeName { get; set; }
        private enum xoff_type
        {
            _none,
            singleSabbath,
            singleSabbathCombined,
            monthSingle,
            monthSingleCombined,
            quarterSingle,
            range
        }
        private em.xgender m_gender { get; set; }
        private xoff_type m_xOffType = xoff_type._none;
        private bool m_IsRange { get; set; }
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
            grid_cols.Add("freq", "Freq");
            grid_cols.Add("gender", "G");
            grid_cols.Add("amount", "Total\nAmount");
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
                 foreach (var k in new string[] { "cg", "rpt_no", "sab_date","account","gender" })
                 {
                     _grid.Cols[k].CellStyle.ForeColor = Color.Gray;
                 }
                _grid.Cols["sab_date"].SortType = iGSortType.ByAuxValue;
                
                _grid.DefaultRow.Height = _grid.GetPreferredRowHeight(true, false);
                _grid.DefaultAutoGroupRow.Height = _grid.DefaultRow.Height;
                _grid.Cols.AutoWidth();
            }

            #endregion
        }
        private void ViewMemberInvolvementDetails_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            //
            m_gender = em.xgender.none;
            //
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
                            m_source_type=(em.off_source_typeS)ret_array[6].ToInt32();
                            break;
                        }
                    case "q_sof":
                        {
                            _account_id = ret_array[1].ToInt32();
                            _cg_id = ret_array[2].ToInt32();
                            _percent = ret_array[3].ToInt32();
                            _month = ret_array[5].ToInt32();
                            _year = ret_array[4].ToInt32();
                            m_xOffType = xoff_type.quarterSingle;
                            m_source_type = (em.off_source_typeS)ret_array[6].ToInt32();
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
                    case "r_sof":
                        {
                            _account_id = ret_array[1].ToInt32();
                            _cg_id = ret_array[2].ToInt32();
                            _percent = ret_array[3].ToInt32();
                            _month = ret_array[5].ToInt32();
                            _year = ret_array[4].ToInt32();
                            m_xOffType = xoff_type.range;
                            m_source_type = (em.off_source_typeS)ret_array[6].ToInt32();
                            m_IsRange = true;
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
            combogender.SelectedIndex = -1;
            foreach(var _k in new string[] { "All", "Male","Female"})
            {
                combogender.Items.Add(_k);
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
            switch(m_source_type)
            {
                case em.off_source_typeS.church_member:
                    {
                        m_SourceTypeName = "Registered Church Members";
                        break;
                    }
                case em.off_source_typeS.unregistered:
                    {
                        m_SourceTypeName = "UNRegistered Church Members";
                        break;
                    }
                case em.off_source_typeS.none:
                    {
                        m_SourceTypeName = string.Empty;
                        break;
                    }
            }
            if (!switchButton1.Value)
            {
                switch (m_xOffType)
                {
                    case xoff_type.singleSabbath:
                        {
                            #region single_sabbath
                            if (_cg_id > -1)
                            {
                                _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,c.amount,c.cg_id,c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                             " where m.sab_fs_id={0} and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={1} and c.receipt_status=1 and cg_id={2} order by c.cg_id", _sab_fs_id, _account_id, _cg_id);
                            }
                            else
                            {
                                _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,c.amount,c.cg_id,c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                            " where m.sab_fs_id={0} and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={1} and c.receipt_status=1 order by c.cg_id ", _sab_fs_id, _account_id);
                            }
                            #endregion
                            break;
                        }
                    case xoff_type.singleSabbathCombined:
                        {
                            #region single_sabbath_combined
                            var _accounts = accn.GetChildAccounts("COMBINED_OFFERING", em.account_typeS.ActualAccount).Select(l => l.account_id).ToList();
                            string _in_str = "(";
                            foreach (var k in _accounts)
                            {
                                _in_str += (_in_str.Length == 1) ? string.Format("{0}", k) : string.Format(",{0}", k);
                            }
                            _in_str += ")";
                            _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,c.amount,c.cg_id,c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                             " where m.sab_fs_id={0} and m.off_id=c.off_id and m.receipt_status=1 and c.account_id in {1} and c.receipt_status=1 order by c.account_id ", _sab_fs_id, _in_str);
                            #endregion
                            break;
                        }
                    case xoff_type.monthSingle:
                        {
                            fGrid.Cols["rpt_no"].Visible = false;
                            fGrid.Cols["sab_date"].Visible = false;
                            var _fs_ids = fn.GetFsIDMonth(_month, _year);
                            #region month_single_sabbath
                            if (m_source_type != em.off_source_typeS.none)
                            {
                                if (_cg_id > -1)
                                {
                                    _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,sum(c.amount) as _sum,count(c.un_id) as _freq, c.cg_id,c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                                 " where m.sab_fs_id between {0} and {1} and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={2} and c.cg_id={3} and m.source_type_id={4} group by m.source_type_id,m.source_id,m.source_type_id,c.cg_id", _fs_ids[0], _fs_ids[1], _account_id, _cg_id, m_source_type.ToInt32());
                                }
                                else
                                {
                                    _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,sum(c.amount) as _sum,count(c.un_id) as _freq,c.cg_id,c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                                " where m.sab_fs_id between {0} and {1}  and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={2} and c.source_type_id={3} group by c.source_type_id,c.source_id", _fs_ids[0], _fs_ids[1], _account_id, m_source_type.ToInt32());
                                }
                            }
                            else
                            {
                                if (_cg_id > -1)
                                {
                                    _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,sum(c.amount) as _sum,count(c.un_id) as _freq, c.cg_id,c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                                 " where m.sab_fs_id between {0} and {1} and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={2} and c.cg_id={3} group by m.source_type_id,m.source_id,m.source_type_id,c.cg_id", _fs_ids[0], _fs_ids[1], _account_id, _cg_id);
                                }
                                else
                                {
                                    _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,sum(c.amount) as _sum,count(c.un_id) as _freq,c.cg_id,c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                                " where m.sab_fs_id between {0} and {1}  and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={2} group by c.source_type_id,c.source_id", _fs_ids[0], _fs_ids[1], _account_id);
                                }
                            }
                            #endregion
                            break;
                        }
                    case xoff_type.range:
                        {
                            fGrid.Cols["rpt_no"].Visible = false;
                            fGrid.Cols["sab_date"].Visible = false;
                            var _fs_ids = (this.Owner as OfferingYearMemberActivity).GetRangeFsIds();
                            #region range
                            if (m_source_type != em.off_source_typeS.none)
                            {
                                if (_cg_id > -1)
                                {
                                    _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,sum(c.amount) as _sum,count(c.un_id) as _freq, c.cg_id,c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                                 " where m.sab_fs_id between {0} and {1} and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={2} and c.cg_id={3} and m.source_type_id={4} group by m.source_type_id,m.source_id,m.source_type_id,c.cg_id", _fs_ids[0], _fs_ids[1], _account_id, _cg_id, m_source_type.ToInt32());
                                }
                                else
                                {
                                    _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,sum(c.amount) as _sum,count(c.un_id) as _freq,c.cg_id,c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                                " where m.sab_fs_id between {0} and {1}  and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={2} and c.source_type_id={3} group by c.source_type_id,c.source_id", _fs_ids[0], _fs_ids[1], _account_id, m_source_type.ToInt32());
                                }
                            }
                            else
                            {
                                if (_cg_id > -1)
                                {
                                    _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,sum(c.amount) as _sum,count(c.un_id) as _freq, c.cg_id,c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                                 " where m.sab_fs_id between {0} and {1} and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={2} and c.cg_id={3} group by m.source_type_id,m.source_id,m.source_type_id,c.cg_id", _fs_ids[0], _fs_ids[1], _account_id, _cg_id);
                                }
                                else
                                {
                                    _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,sum(c.amount) as _sum,count(c.un_id) as _freq,c.cg_id,c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                                " where m.sab_fs_id between {0} and {1}  and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={2} group by c.source_type_id,c.source_id", _fs_ids[0], _fs_ids[1], _account_id);
                                }
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
                            _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,c.amount,c.cg_id,c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                             " where m.sab_fs_id between {0} and {1} and m.off_id=c.off_id and m.receipt_status=1 and c.account_id in {2} and  c.receipt_status=1 order by m.sab_fs_id, c.source_type_id,c.source_id,c.account_id ", _fs_ids[0], _fs_ids[1], _in_str);
                            #endregion
                            break;
                        }
                }
            }
            else
            {
                switch (m_xOffType)
                {
                    case xoff_type.singleSabbath:
                        {
                            fGrid.Cols["rpt_no"].Visible = true;
                            fGrid.Cols["sab_date"].Visible = true;
                            #region single_sabbath
                            if (_cg_id > -1)
                            {
                                _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,c.amount,c.cg_id,c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                             " where m.sab_fs_id={0} and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={1} and c.receipt_status=1 and cg_id={2} order by c.cg_id", _sab_fs_id, _account_id, _cg_id);
                            }
                            else
                            {
                                _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,c.amount,c.cg_id,c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                            " where m.sab_fs_id={0} and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={1} and c.receipt_status=1 order by c.cg_id ", _sab_fs_id, _account_id);
                            }
                            #endregion
                            break;
                        }
                    case xoff_type.singleSabbathCombined:
                        {
                            fGrid.Cols["rpt_no"].Visible = true;
                            fGrid.Cols["sab_date"].Visible = true;
                            #region single_sabbath_combined
                            var _accounts = accn.GetChildAccounts("COMBINED_OFFERING", em.account_typeS.ActualAccount).Select(l => l.account_id).ToList();
                            string _in_str = "(";
                            foreach (var k in _accounts)
                            {
                                _in_str += (_in_str.Length == 1) ? string.Format("{0}", k) : string.Format(",{0}", k);
                            }
                            _in_str += ")";
                            _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,c.amount,c.cg_id,c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                             " where m.sab_fs_id={0} and m.off_id=c.off_id and m.receipt_status=1 and c.account_id in {1} and c.receipt_status=1 order by c.account_id ", _sab_fs_id, _in_str);
                            #endregion
                            break;
                        }
                    case xoff_type.monthSingle:
                        {
                            fGrid.Cols["rpt_no"].Visible = true;
                            fGrid.Cols["sab_date"].Visible = true;
                            var _fs_ids = fn.GetFsIDMonth(_month, _year);
                            #region month_single_sabbath
                            if (m_source_type != em.off_source_typeS.none)
                            {
                                if (_cg_id > -1)
                                {
                                    _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,c.amount,c.cg_id, c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                                 " where m.sab_fs_id between {0} and {1} and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={2} and c.cg_id={3} and m.source_type_id={4}", _fs_ids[0], _fs_ids[1], _account_id, _cg_id, m_source_type.ToInt32());
                                }
                                else
                                {
                                    _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,c.amount,c.cg_id,c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                                " where m.sab_fs_id between {0} and {1}  and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={2} and c.source_type_id={3}", _fs_ids[0], _fs_ids[1], _account_id, m_source_type.ToInt32());
                                }
                            }
                            else
                            {
                                if (_cg_id > -1)
                                {
                                    _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,c.amount, c.cg_id,c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                                 " where m.sab_fs_id between {0} and {1} and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={2} and c.cg_id={3}", _fs_ids[0], _fs_ids[1], _account_id, _cg_id);
                                }
                                else
                                {
                                    _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,c.amount,c.cg_id,c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                                " where m.sab_fs_id between {0} and {1}  and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={2}", _fs_ids[0], _fs_ids[1], _account_id);
                                }
                            }
                            #endregion
                            break;
                        }
                    case xoff_type.range:
                        {
                            fGrid.Cols["rpt_no"].Visible = true;
                            fGrid.Cols["sab_date"].Visible = true;
                            var _fs_ids = (this.Owner as OfferingYearMemberActivity).GetRangeFsIds();
                            #region range
                            if (m_source_type != em.off_source_typeS.none)
                            {
                                if (_cg_id > -1)
                                {
                                    _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,c.amount, c.cg_id,c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                                 " where m.sab_fs_id between {0} and {1} and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={2} and c.cg_id={3} and m.source_type_id={4}", _fs_ids[0], _fs_ids[1], _account_id, _cg_id, m_source_type.ToInt32());
                                }
                                else
                                {
                                    _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,c.amount,c.cg_id,c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                                " where m.sab_fs_id between {0} and {1}  and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={2} and c.source_type_id={3}", _fs_ids[0], _fs_ids[1], _account_id, m_source_type.ToInt32());
                                }
                            }
                            else
                            {
                                if (_cg_id > -1)
                                {
                                    _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,c.amount, c.cg_id,c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                                 " where m.sab_fs_id between {0} and {1} and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={2} and c.cg_id={3}", _fs_ids[0], _fs_ids[1], _account_id, _cg_id);
                                }
                                else
                                {
                                    _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,c.amount,c.cg_id,c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                                " where m.sab_fs_id between {0} and {1}  and m.off_id=c.off_id and m.receipt_status=1 and c.account_id={2}", _fs_ids[0], _fs_ids[1], _account_id);
                                }
                            }
                            #endregion
                            break;
                        }
                    case xoff_type.monthSingleCombined:
                        {
                            #region month_single_combined
                            fGrid.Cols["rpt_no"].Visible = true;
                            fGrid.Cols["sab_date"].Visible = true;
                            var _fs_ids = fn.GetFsIDMonth(_month, _year);
                            var _accounts = accn.GetChildAccounts("COMBINED_OFFERING", em.account_typeS.ActualAccount).Select(l => l.account_id).ToList();
                            string _in_str = "(";
                            foreach (var k in _accounts)
                            {
                                _in_str += (_in_str.Length == 1) ? string.Format("{0}", k) : string.Format(",{0}", k);
                            }
                            _in_str += ")";
                            _str = string.Format("select m.off_id,m.source_name,m.source_id,m.source_type_id,m.sab_date,m.receipt_status,c.amount,c.cg_id,c.account_id from off_main_tb as m,off_accounts_tb as c" +
                                             " where m.sab_fs_id between {0} and {1} and m.off_id=c.off_id and m.receipt_status=1 and c.account_id in {2} and  c.receipt_status=1 order by m.sab_fs_id, c.source_type_id,c.source_id,c.account_id ", _fs_ids[0], _fs_ids[1], _in_str);
                            #endregion
                            break;
                        }
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
                    int _source_id = 0;
                    int _gender_id = 0;
                    em.off_source_typeS m_source_type = em.off_source_typeS.none;
                    while (_dr.Read())
                    {
                      
                        _row = fGrid.Rows.Add();
                        m_source_type = (em.off_source_typeS)_dr["source_type_id"].ToInt32();
                        _source_id = _dr["source_id"].ToInt32();
                        if (m_source_type == em.off_source_typeS.church_member)
                        {
                            if (datam.DATA_MEMBER.IndexOfKey(_source_id) > -1)
                            {
                                _gender_id = datam.DATA_MEMBER[_source_id].gender_id;
                                _row.Cells["gender"].AuxValue = _gender_id;
                                _row.Cells["gender"].Value = ((em.xgender)_gender_id) == em.xgender.Male ? "M" : "F";
                            }
                        }
                        _row.Font = new Font("georgia", 12, FontStyle.Regular);
                         _row.ReadOnly = iGBool.True;
                        _row.Cells["no"].Value = fGrid.Rows.Count;
                        _row.Cells["name"].Value = _dr["source_name"].ToStringNullable();
                        if (!switchButton1.Value)
                        {
                            _row.Cells["amount"].Value = _dr["_sum"].ToInt32();
                            _row.Cells["freq"].Value = _dr["_freq"].ToInt32();
                        }
                        else
                        {
                            _row.Cells["amount"].Value = _dr["amount"].ToInt32();
                            _date = _dr.GetDateTime("sab_date");
                            _row.Cells["sab_date"].Value = _date.Value.ToMyLongDate();
                            _row.Cells["sab_date"].AuxValue = fn.GetFSID(_date.Value);
                            _row.Cells["rpt_no"].Value = "R-" + _dr["off_id"].ToStringNullable();


                        }
                     
                        _total += _row.Cells["amount"].Value.ToInt32();
                        //
                      
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
                                    //
                                    labelTopName.Tag = string.Format("{0} General Report for  {1}  :::  #total", datam.DATA_ACCOUNTS[_account_id].account_name, _date.Value.ToMyShortDate());
                                    break;
                                }
                                if (_cg_id < 0)
                                {
                                    labelTopName.Text = string.Format("{0}  Report for  {1}  :::  {2}", datam.DATA_ACCOUNTS[_account_id].account_name, _date.Value.ToMyShortDate(), _total.ToNumberDisplayFormat());
                                    //
                                    labelTopName.Tag = string.Format("{0}  Report for  {1}  :::  #total", datam.DATA_ACCOUNTS[_account_id].account_name, _date.Value.ToMyShortDate());
                                    break;
                                }
                                if (_cg_id > 0)
                                {
                                    labelTopName.Text = string.Format("{0}  Report for  {1}  :::  {2}", string.Format("{0} :: {1}", datam.DATA_ACCOUNTS[_account_id].account_name, datam.DATA_CHURCH_GROUPS[_cg_id].cg_name), _date.Value.ToMyShortDate(), _total.ToNumberDisplayFormat());
                                    //
                                    labelTopName.Tag = string.Format("{0}  Report for  {1}  :::  {2}", string.Format("{0} :: #total", datam.DATA_ACCOUNTS[_account_id].account_name, datam.DATA_CHURCH_GROUPS[_cg_id].cg_name), _date.Value.ToMyShortDate());
                                }
                                break;
                            }
                        case xoff_type.singleSabbathCombined:
                            {
                                labelTopName.Text = string.Format("Combined Offerings  Report for  {0}  :::  {1}", _date.Value.ToMyShortDate(), _total.ToNumberDisplayFormat());
                                //
                                labelTopName.Tag = string.Format("Combined Offerings  Report for  {0}  :::  #total", _date.Value.ToMyShortDate());
                                break;
                            }
                        case xoff_type.monthSingle:
                            {
                                m_ReportPeriod = string.Format("{0} {1}", datam.MONTHS[_month - 1], _year);
                                m_ReportTotal = _total.ToNumberDisplayFormat();
                                m_ReportMemberCount = fGrid.Rows.Count.ToStringNullable();
                                if (_cg_id == 0)
                                {
                                    m_ReportName = string.Format("{0} General Report", datam.DATA_ACCOUNTS[_account_id].account_name);
                                    labelTopName.Text = string.Format("{0} General Report for  {1}  :::  {2}", datam.DATA_ACCOUNTS[_account_id].account_name, string.Format("{0} {1}", datam.MONTHS[_month - 1], _year), _total.ToNumberDisplayFormat());
                                    //
                                    labelTopName.Tag = string.Format("{0} General Report for  {1}  :::  #total", datam.DATA_ACCOUNTS[_account_id].account_name, string.Format("{0} {1}", datam.MONTHS[_month - 1], _year));
                                    break;
                                }
                                if (_cg_id < 0)
                                {
                                    m_ReportName = string.Format("{0} Report", datam.DATA_ACCOUNTS[_account_id].account_name);
                                    labelTopName.Text = string.Format("{0} Report for  {1}  :::  {2}", datam.DATA_ACCOUNTS[_account_id].account_name, string.Format("{0} {1}", datam.MONTHS[_month - 1], _year), _total.ToNumberDisplayFormat());
                                    //
                                    labelTopName.Tag = string.Format("{0} Report for  {1}  :::  #total", datam.DATA_ACCOUNTS[_account_id].account_name, string.Format("{0} {1}", datam.MONTHS[_month - 1], _year));
                                    break;
                                }
                                if (_cg_id > 0)
                                {
                                    m_ReportName = string.Format("{0} Report  ::: {1}", datam.DATA_ACCOUNTS[_account_id].account_name, datam.DATA_CHURCH_GROUPS[_cg_id].cg_name);
                                    labelTopName.Text = string.Format("{0}  Report for  {1}  :::  {2}", string.Format("{0} :: {1}", datam.DATA_ACCOUNTS[_account_id].account_name, datam.DATA_CHURCH_GROUPS[_cg_id].cg_name), string.Format("{0} {1}", datam.MONTHS[_month - 1], _year), _total.ToNumberDisplayFormat());
                                    //
                                    labelTopName.Tag = string.Format("{0}  Report for  {1}  :::  {2}", string.Format("{0} :: #total", datam.DATA_ACCOUNTS[_account_id].account_name, datam.DATA_CHURCH_GROUPS[_cg_id].cg_name), string.Format("{0} {1}", datam.MONTHS[_month - 1], _year));
                                }
                                
                                break;
                          }
                        case xoff_type.range:
                            {
                                var _dates = (this.Owner as OfferingYearMemberActivity).GetRangeFsDates();
                                if (_dates[1] == null)
                                {
                                    m_ReportPeriod = string.Format("{0}", System.Convert.ToDateTime(_dates[0]).ToShortDateString());
                                }
                                else
                                {
                                    m_ReportPeriod = string.Format("{0}  to  {1}", System.Convert.ToDateTime(_dates[0]).ToShortDateString(), System.Convert.ToDateTime(_dates[1]).ToShortDateString());
                                }
                                m_ReportTotal = _total.ToNumberDisplayFormat();
                                m_ReportMemberCount = fGrid.Rows.Count.ToStringNullable();
                              var _alias = string.Format("{0} to {1}", System.Convert.ToDateTime(_dates[0]).ToShortDateString(), System.Convert.ToDateTime(_dates[1]).ToShortDateString());
                                if (_cg_id == 0)
                                {
                                    m_ReportName = string.Format("{0} General Report", datam.DATA_ACCOUNTS[_account_id].account_name);
                                    labelTopName.Text = string.Format("{0} General Report from  {1}  :::  {2}", datam.DATA_ACCOUNTS[_account_id].account_name, _alias, _total.ToNumberDisplayFormat());
                                    //
                                    labelTopName.Tag = string.Format("{0} General Report from  {1}  :::  #total", datam.DATA_ACCOUNTS[_account_id].account_name, _alias);
                                    break;
                                }
                                if (_cg_id < 0)
                                {
                                    m_ReportName = string.Format("{0} Report", datam.DATA_ACCOUNTS[_account_id].account_name);
                                    labelTopName.Text = string.Format("{0} Report from  {1}  :::  {2}", datam.DATA_ACCOUNTS[_account_id].account_name, _alias, _total.ToNumberDisplayFormat());
                                    //
                                    labelTopName.Tag = string.Format("{0} Report from  {1}  :::  #total", datam.DATA_ACCOUNTS[_account_id].account_name, _alias);
                                    break;
                                }
                                if (_cg_id > 0)
                                {
                                    m_ReportName = string.Format("{0} Report  ::: {1}", datam.DATA_ACCOUNTS[_account_id].account_name, datam.DATA_CHURCH_GROUPS[_cg_id].cg_name);
                                    labelTopName.Text = string.Format("{0}  Report from  {1}  :::  {2}", string.Format("{0} :: {1}", datam.DATA_ACCOUNTS[_account_id].account_name, datam.DATA_CHURCH_GROUPS[_cg_id].cg_name), _alias, _total.ToNumberDisplayFormat());
                                    //
                                    labelTopName.Tag = string.Format("{0}  Report from  {1}  :::  #total", string.Format("{0} :: {1}", datam.DATA_ACCOUNTS[_account_id].account_name, datam.DATA_CHURCH_GROUPS[_cg_id].cg_name), _alias);
                                }

                                break;
                            }
                        case xoff_type.monthSingleCombined:
                            {
                                labelTopName.Text = string.Format("Combined Offerings  Report for  {0}  :::  #total", string.Format("{0} {1}", datam.MONTHS[_month - 1], _date.Value.Year));
                                break;

                            }
                    }
                    if (!string.IsNullOrEmpty(m_SourceTypeName))
                    {
                        m_ReportName += " for " + m_SourceTypeName;
                    }
                    label1.Text = fGrid.Rows.Count.ToStringNullable();

                }
            }
            fGrid.SortObject.Clear();
            
            if(switchButton1.Value)
            {
                fGrid.SortObject.Add("sab_date", iGSortOrder.Ascending);
                fGrid.Sort();
            }
            else
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
            fnn.PrintIGridNormalOffertory(fGrid, m_gender == em.xgender.none ? string.Format(m_ReportName) : m_gender == em.xgender.Male ? string.Format("(MALE) {0}", m_ReportName) : string.Format("FEMALE {0}", m_ReportName), new string[] { "no" }, m_ReportPeriod, m_ReportTotal, m_ReportMemberCount);
            //
            // fnn.PrintIGridNormal(fGrid, labelTopName.Text, null);
        }

        private void fGrid_AfterContentsSorted(object sender, EventArgs e)
        {
            fGrid.BeginUpdate();
            var _rows = fGrid.Rows.Cast<iGRow>().Where(j => j.Visible).ToList();
            int _count = 1;
            foreach (var _rw in _rows)
            {
                _rw.Cells["no"].Value = _count;
                _count++;
            }
            //for (var t = 1; t <= _rows.Count; t++)
            //{
            //    fGrid.Rows[t - 1].Cells["no"].Value = t;
            //}

            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
            fGrid.EndUpdate();
            //
            if (fGrid.Rows.Count > 0)
            {
                fGrid.SetCurCell(0, 0);
            }
        }

        private void printReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.PerformClick();
            //string _str = "Are You Sure You Want To Print This Data ?";
            //if (!dbm.WarningMessage(_str, "Print Warning"))
            //{
            //    return;
            //}
            //fnn.PrintIGridNormalOffertory(fGrid, m_gender == em.xgender.none ? string.Format(m_ReportName) : m_gender == em.xgender.Male ? string.Format("{0} ({1})", m_ReportName, "Male") : string.Format("{0} ({1})", m_ReportName, "Female"), new string[] { "no" }, m_ReportPeriod, m_ReportTotal, m_ReportMemberCount);
        }
        private bool app_working = false;
        private void combogender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(app_working)
            {
                return;
            }
            switch (combogender.SelectedIndex)
            {
                case -1:
                    {
                        m_gender = em.xgender.none;
                        break;
                    }
                case 0:
                    {
                        m_gender = em.xgender.none;
                        break;
                    }
                case 1:
                    {
                        m_gender = em.xgender.Male;
                        break;
                    }
                case 2:
                    {
                        m_gender = em.xgender.Female;
                        break;
                    }
            }
            int _count = 0;
            int _amount = 0;
            foreach (var _rw in fGrid.Rows.Cast<iGRow>())
            {
                if (m_gender == em.xgender.none)
                {
                    _rw.Visible = true;
                    _rw.Cells["no"].Value = (_count + 1);
                    _amount += (_rw.Cells["amount"].Value.ToInt32());
                    _count++;
                    continue;
                }
                //
                if (_rw.Cells["gender"].AuxValue == null)
                {
                    _rw.Visible = false;
                    continue;
                }
                //
                _rw.Visible = _rw.Cells["gender"].AuxValue.ToInt16() == m_gender.ToInt16() ? true : false;
                if (_rw.Visible)
                {
                    _rw.Cells["no"].Value = (_count + 1);
                    _count++;
                    _amount += (_rw.Cells["amount"].Value.ToInt32());
                }
                //
            }
            m_ReportMemberCount = _count.ToStringNullable();
            if (labelTopName.Tag != null)
            {
                labelTopName.Text = labelTopName.Tag.ToStringNullable();
                labelTopName.Text = labelTopName.Text.Replace("#total", _amount.ToNumberDisplayFormat());
            }
            
            m_ReportTotal = _amount.ToNumberDisplayFormat();
            label1.Text = m_ReportMemberCount;
        }

        private void switchButton1_ValueChanged(object sender, EventArgs e)
        {
            app_working = true;
            //
            combogender.SelectedIndex = -1;

            app_working = false;
            if(switchButton1.Value)
            {
                fGrid.Cols["rpt_no"].Visible = true;
                fGrid.Cols["sab_date"].Visible = true;
                //
                fGrid.Cols["freq"].Visible = false;
            }
            else
            {
                fGrid.Cols["rpt_no"].Visible = false;
                fGrid.Cols["sab_date"].Visible = false;
                //
                fGrid.Cols["freq"].Visible = true;
            }
            backworker.RunWorkerAsync();

        }
    }
}
