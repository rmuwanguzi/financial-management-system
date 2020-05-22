using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;
using SdaHelperManager.Security;
using SdaHelperManager;
namespace MTOMS
{
    public partial class AddToPledgeMaker : DevComponents.DotNetBar.Office2007Form
    {
        public AddToPledgeMaker()
        {
            InitializeComponent();
        }
       
      
        private int m_year = 0;
        private List<int> m_CurrentOffAccounts { get; set; }
        ic.pledgeSettingC m_pledge_settings { get; set; }
        ic.church_group_typeC m_default_church_gp_type { get; set; }
        ic.pledgeC m_pledge { get; set; }
        private void AddToPledgeMaker_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
            CenterToScreen();
            InitIgridColumns();
            datam.ShowWaitForm();
            Application.DoEvents();
            m_year = datam.CURR_DATE.Year;
            datam.SecurityCheck();
            m_pledge = this.Tag as ic.pledgeC;
            m_pledge_settings = datam.DATA_CURRENT_PLEDGE_SETTINGS[m_pledge.pls_id];


            backworker.RunWorkerAsync();
        }
        private void SortAndGroup()
        {
            fGrid.GroupObject.Clear();
            fGrid.SortObject.Clear();
            fGrid.GroupObject.Add("svalue");
            fGrid.SortObject.Add("svalue", iGSortOrder.Ascending);
            fGrid.Group();
        }
        private iGRow CreateNewRow(string field, string rowkey, Type _type, int group_index, string group_name)
        {
            var _row = fGrid.Rows.Add();
            _row.Font = new Font("georgia", 14, FontStyle.Regular);
            _row.Cells["desc"].Font = new Font("georgia", 15, FontStyle.Regular);
            _row.Cells["name"].Col.Width = 220;
            _row.Cells["name"].Value = field;
            _row.Cells["name"].TextAlign = iGContentAlignment.BottomRight;
            _row.Cells["desc"].ValueType = _type;
            _row.Cells["category"].Value = group_name;
            _row.Cells["svalue"].Value = group_index;
            _row.Key = rowkey;
            _row.AutoHeight();
            _row.Height += 2;
            return _row;
        }
      
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                buttonX1.PerformClick();
                return true;
            }
           
           
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void LoadMainGrid()
        {
            fGrid.BeginUpdate();
            iGRow _row = null;
            _row = fGrid.Rows.Add();
            _row.Height = 7;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            
            int group_index = 1;
            string group_name = "New Account Information";
            _row = CreateNewRow("Pledge Account", "pledge_acc", typeof(string), group_index, group_name);
            _row.Cells[0].TextAlign = iGContentAlignment.MiddleRight;
            _row.Cells[1].Font = new Font("verdana", 15, FontStyle.Regular);
            _row.Cells[1].Value = string.IsNullOrEmpty(m_pledge_settings.pledge_name) ? datam.DATA_ACCOUNTS[m_pledge_settings.account_id].account_name : m_pledge_settings.pledge_name;
            _row.Cells[1].Enabled = iGBool.False;
            _row.Height += 4;
            _row = fGrid.Rows.Add();
            _row.Height = 3;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;

            
            //
            _row = CreateNewRow(" Member Name", "owner", typeof(string), group_index, group_name);
            _row.Cells[0].TextAlign = iGContentAlignment.MiddleRight;
            _row.Cells[1].Font = new Font("verdana", 15, FontStyle.Regular);
            _row.Height += 4;
             switch(m_pledge.source_type)
            {
                case em.off_source_typeS.church_group:
                    {
                        _row.Cells[1].Value = datam.DATA_CHURCH_GROUPS[m_pledge.source_id].cg_name;
                        break;
                    }
                case em.off_source_typeS.church_member:
                    {
                        _row.Cells[1].Value = string.Format("{0} :: {1}", datam.DATA_MEMBER[m_pledge.source_id].mem_name, datam.DATA_MEMBER[m_pledge.source_id].mem_code);
                        break;
                    }
                case em.off_source_typeS.department:
                    {
                        _row.Cells[1].Value = datam.DATA_DEPARTMENT[m_pledge.source_id].dept_name;
                        break;
                    }
            }
            _row.Cells[1].Enabled = iGBool.False;
            //
            _row = CreateNewRow("Pledge Account", "account", typeof(string), group_index, group_name);
            _row.Cells[0].TextAlign = iGContentAlignment.MiddleRight;
            _row.Cells[1].Font = new Font("verdana", 15, FontStyle.Regular);
            _row.Cells[1].Value = datam.DATA_ACCOUNTS[m_pledge.account_id].account_name;
            _row.Height += 4;
            _row.Visible = true;
            _row.Cells[1].Enabled = iGBool.False;
            //
           
            //
            _row = CreateNewRow("Name", "other_name", typeof(string), group_index, group_name);
            _row.Cells[0].TextAlign = iGContentAlignment.MiddleRight;
            _row.Cells[1].Font = new Font("verdana", 15, FontStyle.Regular);
            _row.Height += 3;
            _row.Visible = false;
            //
            _row = CreateNewRow("Phone", "other_phone", typeof(string), group_index, group_name);
            _row.Cells[0].TextAlign = iGContentAlignment.MiddleRight;
            _row.Cells[1].Font = new Font("verdana", 15, FontStyle.Regular);
            _row.Height += 3;
            _row.Visible = false;
            //
            _row = fGrid.Rows.Add();
            _row.Height = 7;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            //
            _row = CreateNewRow("Added Pledge Amount", "pl_amount", typeof(decimal), group_index, group_name);
            _row.Cells[0].TextAlign = iGContentAlignment.MiddleRight;
            _row.Cells[1].Font = new Font("verdana", 15, FontStyle.Regular);
            _row.Cells[1].FormatString = "{0:N0}";
            _row.Height += 4;
            _row.ForeColor = Color.Maroon;
            //
            _row = fGrid.Rows.Add();
            _row.Height = 7;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            //
            _row = CreateNewRow("Church Group", "church_group", typeof(string), group_index, group_name);
            _row.Cells[0].TextAlign = iGContentAlignment.MiddleRight;
            _row.Cells[1].Font = new Font("verdana", 15, FontStyle.Regular);
            _row.Height += 4;
            _row.Cells[1].Enabled = iGBool.False;
            _row.Visible = false;
            _row = fGrid.Rows.Add();
            _row.Height = 7;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            _row.Visible = false;
            //
            var _dropdown = new fnn.DropDownCalenderX();
            _dropdown.start_date = m_pledge.fs_date;
            _dropdown.selected_date = sdata.CURR_DATE;
            _dropdown.end_date = sdata.CURR_DATE;
            _row = CreateNewRow("Added Pledge Date", "start_date", typeof(string), group_index, group_name);
            _row.Cells["desc"].TypeFlags = iGCellTypeFlags.ComboPreferValue | iGCellTypeFlags.NoTextEdit | iGCellTypeFlags.HasEllipsisButton;
            _row.Cells["desc"].FormatString = "{0:D}";// {0:d} for short date string
            _row.Cells["desc"].DropDownControl = _dropdown;
            _row.Cells["desc"].Value = null;
            //
            _row = CreateNewRow("Collect Date (Optional)", "collect_date", typeof(string), group_index, group_name);
            _row.Font = new Font("georgia", 13, FontStyle.Regular);
            _row.Cells["desc"].TypeFlags = iGCellTypeFlags.ComboPreferValue | iGCellTypeFlags.NoTextEdit | iGCellTypeFlags.HasEllipsisButton;
            _row.Cells["desc"].FormatString = "{0:D}";// {0:d} for short date string
            _row.Cells["desc"].Value = sdata.CURR_DATE;
            _row.Cells["desc"].DropDownControl = new fnn.DropDownCalendar();
            _row.Cells["name"].TypeFlags = iGCellTypeFlags.NoTextEdit | iGCellTypeFlags.HasEllipsisButton;
            _row.Cells["name"].ReadOnly = iGBool.False;
            _row.Cells["name"].ForeColor = Color.DarkSlateGray;
            _row.Cells["name"].Selectable = iGBool.False;
            _row.Cells["name"].TextAlign = iGContentAlignment.MiddleRight;
            _row.Cells[1].Enabled = iGBool.False;
            _row.Visible = false;
            
            fGrid.EndUpdate();
        }
        public void ClearGrid()
        {
            if (fGrid.Rows.Count == 0) { return; }
            var nlist = from t in fGrid.Rows.Cast<iGRow>().AsQueryable<iGRow>()
                        where !string.IsNullOrEmpty(t.Key)
                        select t;
            fGrid.BeginUpdate();
            foreach (var _rw in nlist)
            {
                _rw.Cells["desc"].Value = null;
                _rw.Cells["desc"].AuxValue = null;
            }
           // fGrid.Rows["start_date"].Cells[1].Value = sdata.CURR_DATE;
            //
            fGrid.EndUpdate();
            fGrid.Focus();
            fGrid.SetCurCell("owner", 1);
            fGrid.Rows["collect_date"].Cells[1].Enabled = iGBool.False;
            fGrid.Rows["pledge_acc"].Cells[1].Value = string.IsNullOrEmpty(m_pledge_settings.pledge_name) ? datam.DATA_ACCOUNTS[m_pledge_settings.account_id].account_name : m_pledge_settings.pledge_name;

        }
        void InitIgridColumns()
        {
            #region Adjust the grid's appearance
            // Set the flat appearance for the cells.
            fGrid.Appearance = iGControlPaintAppearance.StyleFlat;
            fGrid.UseXPStyles = false;
            fGrid.ScrollBarSettings.Appearance = iGControlPaintAppearance.StyleFlat;
            fGrid.FocusRect = false;
            #endregion
            iGCol myCol;
            //
            myCol = fGrid.Cols.Add("name", "Field Name");
            myCol.CellStyle.ReadOnly = iGBool.True;
            myCol.SortType = iGSortType.None;
            myCol.Width = 20;
            myCol.CellStyle.ForeColor = Color.DarkBlue;
            myCol.CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
            myCol.CellStyle.ReadOnly = iGBool.True;
            myCol.IncludeInSelect = false;
            myCol.CellStyle.BackColor = Color.WhiteSmoke;
            //
            myCol.AllowSizing = false;
            myCol = fGrid.Cols.Add("desc", "Description");
            myCol.Width = 150;
            //  myCol.AllowSizing = false;
            //
            // Add a special column which will store the category name.
            // This column will be used for grouping.
            fGrid.Cols.Add("category", string.Empty).Visible = false;
            // Add a special column which will store the default 
            // values for the properties.
            fGrid.Cols.Add("svalue", string.Empty).Visible = false;
            myCol = fGrid.Cols["svalue"];
            myCol.SortType = iGSortType.ByValue;
            fGrid.DefaultRow.Height = fGrid.GetPreferredRowHeight(true, false);
            fGrid.DefaultAutoGroupRow.Height = fGrid.DefaultRow.Height;
        }

        private void backworker_DoWork(object sender, DoWorkEventArgs e)
        {
            datam.SystemInitializer();
            datam.GetPendingPledges();
        }
       
      
        private void backworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var m_default_cg_type = datam.DATA_CG_TYPES.Values.FirstOrDefault(k => k.is_default);
            if(m_default_cg_type!=null)
            {

            }
            using (var xd = new xing())
            {
                datam.fill_accounts(xd);
                datam.GetPledgeSettings(xd);
                datam.GetDepartments(xd);
                xd.CommitTransaction();
            }
            
            LoadMainGrid();
          
           // ClearGrid();
            datam.HideWaitForm();
            Application.DoEvents();
            fGrid.Focus();
            fGrid.SetCurCell("pl_amount", 1);
          
        }

        private void fGrid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
        {
            iGRow _row = fGrid.Rows[e.RowIndex];
            if (_row.Cells[1].DropDownControl != null && _row.Cells[1].AuxValue == null)
            {
                _row.Cells[1].Value = null;
                if (_row.Key == "owner_type")
                {
                    fGrid.Rows["owner"].Cells[1].AuxValue = null;
                    fGrid.Rows["owner"].Cells[1].Value = null;
                    fGrid.Rows["owner"].Cells[1].DropDownControl = null;
                    fGrid.Rows["owner"].Visible = false;
                }
                if (_row.Key == "account_type")
                {
                    ClearGrid();
                }
                return;
            }
            if (_row.Key == "start_date")
            {
                if (_row.Cells[1].AuxValue != null)
                {
                    buttonX1.PerformClick();
                }
            }
             if (e.RowIndex != fGrid.Rows.Count - 1)
            {
                if (fGrid.Rows[e.RowIndex].Cells["desc"].Value == null) { return; }
                for (int k = e.RowIndex + 1; k < fGrid.Rows.Count; k++)
                {
                    if (fGrid.Rows[k].Type == iGRowType.AutoGroupRow)
                    {
                        fGrid.Rows[k].Expanded = true;
                        continue;
                    }
                    if (!fGrid.Rows[k].Visible) { continue; }
                    if (fGrid.Rows[k].Cells[1].Enabled==iGBool.False) { continue; }
                    if (string.IsNullOrEmpty(fGrid.Rows[k].Key)) { continue; }
                    if ((e.RowIndex + 3) < fGrid.Rows.Count)
                    {
                        fGrid.Rows[(e.RowIndex + 3)].EnsureVisible();
                    }
                    fGrid.SetCurCell(k, e.ColIndex);
                    break;
                }
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            foreach(var _k in new string[]
            {
                "pl_amount",
                "other_name",
                "other_phone",
                "church_group",
                "start_date",
                "collect_date"
            })
            {
                fGrid.Rows[_k].Cells[1].Value = null;
                fGrid.Rows[_k].Cells[1].AuxValue = null;
            }
           
            fGrid.Focus();
            fGrid.SetCurCell("pl_amount", 1);
            fGrid.Rows["collect_date"].Cells[1].Enabled = iGBool.False;
            fGrid.Rows["pledge_acc"].Cells[1].Value = string.IsNullOrEmpty(m_pledge_settings.pledge_name) ? datam.DATA_ACCOUNTS[m_pledge_settings.account_id].account_name : m_pledge_settings.pledge_name;
        }

        private void fGrid_EllipsisButtonClick(object sender, iGEllipsisButtonClickEventArgs e)
        {
            if (e.ColIndex == 0)
            {
                fGrid.Rows[e.RowIndex].Cells[1].Enabled = (fGrid.Rows[e.RowIndex].Cells[1].Enabled == iGBool.False) ? iGBool.True : iGBool.False;
                return;
            }
            if (fGrid.Rows[e.RowIndex].Key == "collect_date")
            {
                fGrid.Rows[e.RowIndex].Cells[1].Value = null;
                fGrid.Rows[e.RowIndex].Cells[1].AuxValue = null;
            }
        }
        private bool IsValid()
        {
            var _comp = new string[] { "start_date", "pl_amount" };
            foreach (var k in _comp)
            {
                if (fGrid.Rows[k].Cells[1].Value == null)
                {
                    MessageBox.Show("Important Field Left Blank", "Save Error");
                    fGrid.Focus();
                    fGrid.SetCurCell(k, 1);
                    return false;
                }
            }
            //ic.shared_off_item _acc_item = (fGrid.Rows["account"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag as ic.shared_off_item;
            //ic.off_source_item _source = (fGrid.Rows["owner"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag as ic.off_source_item;
            //if (_acc_item != null)
            //{
            //    var _pledge = (from k in datam.DATA_PENDING_PLEDGES.Values
            //                   where k.pls_id == _acc_item.pls_id
            //                   & k.source_id == _source.source_id & k.source_type.ToByte() == _source.source_type_id
            //                   select k).FirstOrDefault();
            //    if (_pledge != null)
            //    {
            //        DateTime? _date = System.Convert.ToDateTime(fGrid.Rows["start_date"].Cells[1].AuxValue);
            //        var _pls = datam.DATA_PLEDGE_SETTINGS[_pledge.pls_id];
            //        if (_date != null)
            //        {
            //            var _fs_id = fn.GetFSID(_date.Value);
            //            if (_pls.start_fs_id >= _fs_id & _fs_id <= _pls.end_fs_id)
            //            {
            //                MessageBox.Show("You Have Already Made This Pledge For The Selected Member", "Duplicate Pledge");

            //            }


            //        }
            //    }
            //}
            return true;
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                return;
            }
            string _str = "Are You Sure You Want To Save This Record ??";
            var _date = System.Convert.ToDateTime(fGrid.Rows["start_date"].Cells[1].AuxValue);
            int _amount= fGrid.Rows["pl_amount"].Cells[1].Value.ToInt32();
            using (var xd = new xing())
            {
                
                if (!dbm.WarningMessage(_str, "Save Warning"))
                {
                    return;
                }
                xd.SingleInsertCommandTSPInt("pledge_addition_tb", new string[]
                {
                    "account_id",
                    "source_type_id",
                    "source_id",
                    "added_pledge_amount",
                    "pls_id",
                    "pl_id",
                    "fs_date",
                    "fs_id",
                    "lch_id",
                    "fs_time_stamp","pl_status"
                }, new object[]
                {
                    m_pledge.account_id,
                    m_pledge.source_type.ToInt16(),
                    m_pledge.source_id,
                    _amount,
                    m_pledge.pls_id,
                    m_pledge.pl_id,
                    _date,
                    fn.GetFSID(_date),
                    datam.LCH_ID,
                    0,
                    em.pledge_setting_statusS.valid.ToInt16()

                });
                accn.PledgeSummary(xd, _amount, 0, m_pledge.pls_id);
                xd.SingleUpdateCommandALL("pledge_settings_tb", new string[]
                {
                    "paid_amount",
                    "pls_id"
                }, new object[]
                {
                    0,
                    m_pledge.pls_id
                }, 1);
                xd.UpdateFsTimeStamp("pledge_master_tb");
                _str = string.Format("update pledge_master_tb set added_pledge_amount=(added_pledge_amount + {0}),amount_pledged=(amount_pledged + {0}),amount_paid=0,fs_time_stamp={1},{2} where pl_id={3}",
                                    _amount, SQLH.UnixStamp, dbm.ETS, m_pledge.pl_id);
                xd.SingleUpdateCommand(_str);
                m_pledge.amount_paid = 0;
                m_pledge.added_pledge_amount += _amount;
                m_pledge.amount_pledged += _amount;
                SortedList<int, int> _vv_ids = new SortedList<int, int>();
                using (var _dr = xd.SelectCommand("select un_id,lch_id from pledge_payment_mvt_tb where pl_id=" + m_pledge.pl_id))
                {
                    while (_dr.Read())
                    {
                        _vv_ids.Add(_dr[0].ToInt32(), _dr[1].ToInt32());
                    }
                }
                foreach(var _d in _vv_ids)
                {
                    xd.SingleDeleteCommandExp("pledge_payment_mvt_tb", new string[] { "un_id", "lch_id" }, new int[] { _d.Key, _d.Value });
                }
                xd.CommitTransaction();
            }
            datam.GetPendingPledges();
            using (var xd = new xing())
            {
                var _pls = datam.DATA_CURRENT_PLEDGE_SETTINGS[m_pledge.pls_id];
                _str = string.Format("select amount,transaction_id,sab_date,off_id,pay_mode from off_accounts_tb where sab_fs_id between {0} and {1} and source_id={2} and source_type_id={3} and account_id={4} and receipt_status={5} order by sab_fs_id",
                   _pls.start_fs_id, _pls.end_fs_id, m_pledge.source_id, m_pledge.source_type.ToByte(), m_pledge.account_id, em.off_receipt_statusS.Valid.ToByte());
                //
                Correction.cr.cr_off_account _off = null;
                List<Correction.cr.cr_off_account> _list = null;
                using (var _dr = xd.SelectCommand(_str))
                {
                    while (_dr.Read())
                    {
                        if (_list == null)
                        {
                            _list = new List<Correction.cr.cr_off_account>();
                        }
                        _off = new Correction.cr.cr_off_account();
                        _off.amount = _dr["amount"].ToInt32();
                        _off.trans_id = _dr["transaction_id"].ToInt64();
                        _off.fs_date = _dr.GetDateTime("sab_date");
                        _off.off_id = _dr["off_id"].ToInt32();
                        _off.pay_mode = (em.off_paymodeS)_dr["pay_mode"].ToByte();
                        _list.Add(_off);
                        _off = null;
                    }
                }
                if (_list != null)
                {
                    #region old pledge payments
                    int ww_amount = 0;
                   
                    foreach (var p in _list)
                    {
                        ww_amount = p.amount;
                        if (m_pledge.balance <= ww_amount)
                        {
                            xd.SingleUpdateCommandALL("pledge_master_tb", new string[]
                           {
                                    "amount_paid",
                                    "pl_status",
                                    "fs_id",
                                    "pl_id",
                           }, new object[]
                           {
                                    m_pledge.amount_pledged,
                                    em.pledge_statusS.completed.ToByte(),
                                    m_pledge.fs_id,
                                    m_pledge.pl_id
                           }, 2);
                            ww_amount -= m_pledge.balance;

                            xd.SingleInsertCommandTSP("pledge_payment_mvt_tb", new string[]
                      {
                            "off_id","pl_id","account_id","fs_date","fs_id","amount","pay_mode","status","lch_id","lch_type_id","source_type_id","source_id","transaction_id","pl_fs_date","pl_fs_id","exp_type","fs_time_stamp","pls_id","cg_id"
                       }, new object[] { p.off_id, m_pledge.pl_id, m_pledge.account_id, p.fs_date, fn.GetFSID(p.fs_date), m_pledge.balance, p.pay_mode, 1, datam.LCH_ID, datam.LCH_TYPE_ID, m_pledge.source_type.ToByte(), m_pledge.source_id,
                                p.trans_id,m_pledge.fs_date,m_pledge.fs_id,33,0,m_pledge.pls_id,m_pledge.cg_id });
                            //
                            m_pledge.amount_paid += m_pledge.balance;
                            accn.PledgeSummary(xd, 0, m_pledge.balance, m_pledge.pls_id);
                        }
                        if (m_pledge.balance > ww_amount)
                        {
                            xd.UpdateFsTimeStamp("pledge_master_tb");
                            xd.InsertUpdateDelete(string.Format("update pledge_master_tb set amount_paid=(amount_paid + {0}),fs_time_stamp={1},{2} where pl_id={3}",
                                ww_amount, SQLH.UnixStamp, dbm.ETS, m_pledge.pl_id));
                            xd.SingleInsertCommandTSP("pledge_payment_mvt_tb", new string[]
                       {
                            "off_id","pl_id","account_id","fs_date","fs_id","amount","pay_mode","status","lch_id","lch_type_id","source_type_id","source_id","transaction_id","pl_fs_date","pl_fs_id","exp_type","fs_time_stamp","pls_id","cg_id"
                       }, new object[] { p.off_id, m_pledge.pl_id, m_pledge.account_id,  p.fs_date, fn.GetFSID(p.fs_date), ww_amount, p.pay_mode, 1, datam.LCH_ID, datam.LCH_TYPE_ID, m_pledge.source_type.ToByte(), m_pledge.source_id,
                                p.trans_id,m_pledge.fs_date,m_pledge.fs_id,33,0,m_pledge.pls_id,m_pledge.cg_id });
                            //
                            m_pledge.amount_paid += ww_amount;
                            accn.PledgeSummary(xd, 0, ww_amount, m_pledge.pls_id);
                            ww_amount = 0;
                        }
                    }
                    #endregion
                }
                xd.CommitTransaction();
            }
            datam.GetPendingPledges();
            buttonX2.PerformClick();
            this.Tag = null;
            this.Close();
        }
    }
}
