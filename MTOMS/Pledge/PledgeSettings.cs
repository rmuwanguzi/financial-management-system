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
    public partial class PledgeSettings : DevComponents.DotNetBar.Office2007Form
    {
        public PledgeSettings()
        {
            InitializeComponent();
        }
        private iGDropDownList m_accounts = null;
        
        private void PledgeSettings_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
            CenterToScreen();
            InitIgridColumns();
            datam.ShowWaitForm();
            Application.DoEvents();
           // m_year = datam.CURR_DATE.Year;
            datam.SecurityCheck();
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
            _row.Cells["name"].Col.Width = 180;
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
      
        private void LoadMainGrid()
        {
            fGrid.BeginUpdate();
            iGRow _row = null;
            _row = fGrid.Rows.Add();
            _row.Height = 4;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            
            int group_index = 1;
            string group_name = "New Account Information";
            //
            _row = CreateNewRow("Pledge Name", "pledge_name", typeof(string), group_index, group_name);
            _row = fGrid.Rows.Add();
            _row.Height = 4;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            _row = CreateNewRow("Bind To Account", "account", typeof(string), group_index, group_name);
            _row.Cells[0].TextAlign = iGContentAlignment.MiddleRight;
            _row.Cells[1].Font = new Font("verdana", 15, FontStyle.Regular);
            _row.ForeColor = Color.Maroon;
            _row.Height += 4;
            //
            _row = fGrid.Rows.Add();
            _row.Height = 4;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            //
           // var _dropdown = new fnn.DropDownCalenderX();
           
            
            _row = CreateNewRow("Start Date", "start_date", typeof(string), group_index, group_name);
            _row.Cells["desc"].TypeFlags = iGCellTypeFlags.ComboPreferValue | iGCellTypeFlags.NoTextEdit;
            _row.Cells["desc"].FormatString = "{0:D}";// {0:d} for short date string
            _row.Cells["desc"].DropDownControl = new fnn.DropDownCalenderX();
            _row.Cells["desc"].ForeColor = Color.DarkBlue;
            _row.Cells["desc"].Value = null;
            _row.Cells["desc"].Enabled = iGBool.False;
             //
            _row = CreateNewRow("End Date", "end_date", typeof(string), group_index, group_name);
            _row.Cells["desc"].TypeFlags = iGCellTypeFlags.ComboPreferValue | iGCellTypeFlags.NoTextEdit;
            _row.Cells["desc"].FormatString = "{0:D}";// {0:d} for short date string
            _row.Cells["desc"].Value = null;
            _row.Cells["desc"].DropDownControl = new fnn.DropDownCalenderX();
            _row.Cells["desc"].Enabled = iGBool.False;
            //
            _row = fGrid.Rows.Add();
            _row.Height = 5;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            //
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
            fGrid.Rows["end_date"].Cells[1].Enabled = iGBool.False;
            fGrid.Rows["start_date"].Cells[1].Enabled = iGBool.False;
            //
            fGrid.EndUpdate();
            fGrid.Focus();
            fGrid.SetCurCell("account", 0);
            SendKeys.Send("{ENTER}");

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
        }
        private void backworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            using (var xd = new xing())
            {
                datam.fill_accounts(xd);
                datam.GetPledgeSettings(xd);
                datam.GetDepartments(xd);
                xd.CommitTransaction();
            }
           
            m_accounts = fnn.CreateCombo();
            //
            if (datam.DATA_ACCOUNTS != null )
            {
                //List<int> ValidIDS = (from s in datam.DATA_PLEDGE_SETTINGS.Values
                //                      where s.end_fs_id > sdata.CURR_FS.fs_id
                //                      select s.account_id).ToList();
                //if (ValidIDS != null && ValidIDS.Count > 0)
                //{
                    
                //}
                //else
                //{

                //}
                var nlist = accn.GetChildAccounts("OFFERTORY", em.account_typeS.ActualAccount);
                foreach (var k in nlist)
                {
                    m_accounts.Items.Add(new fnn.iGComboItemEX()
                    {
                        Tag = k,
                        Value = k.account_name
                    });
                }
                
            }
            LoadMainGrid();
            m_accounts.MaxVisibleRowCount = 8;
            fGrid.Rows["account"].Cells[1].DropDownControl = m_accounts;
            ClearGrid();
            datam.HideWaitForm();
            Application.DoEvents();
            fGrid.Focus();
            fGrid.SetCurCell("pledge_name", 1);

        }

        private void fGrid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
        {
            iGRow _row = fGrid.Rows[e.RowIndex];
            if (_row.Cells[1].DropDownControl != null && _row.Cells[1].AuxValue == null)
            {
                _row.Cells[1].Value = null;
               
                if (_row.Key == "account")
                {
                    fGrid.Rows["end_date"].Cells[1].Value = null;
                    fGrid.Rows["start_date"].Cells[1].Value = null;
                    //
                    fGrid.Rows["end_date"].Cells[1].Enabled = iGBool.False;
                    fGrid.Rows["start_date"].Cells[1].Enabled = iGBool.False;
                    //
                   
                }
                return;
            }
            if (_row.Key == "start_date")
            {
                object _date_obj = Convert.ToDateTime(fGrid.Rows["start_date"].Cells["desc"].AuxValue);
                if (_date_obj == null)
                {
                    _row.Cells["desc"].Value = sdata.CURR_DATE;
                    return;
                }
                fGrid.Rows["end_date"].Cells[1].Value = null;
                    fGrid.Rows["end_date"].Cells[1].AuxValue=null;
                var _date = Convert.ToDateTime(_date_obj);
                var _drop_down = fGrid.Rows["end_date"].Cells[1].DropDownControl as fnn.DropDownCalenderX;
                _drop_down.start_date = _date.AddDays(1);
                _drop_down.selected_date = _drop_down.start_date;
                _drop_down.end_date = _date.AddDays(366);
                //
                fGrid.Rows["end_date"].Cells[1].Enabled = iGBool.True;
            }
            //
            if (_row.Key == "account")
            {
                ic.accountC _acc = (fGrid.Rows["account"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag as ic.accountC;
                if (_acc != null)
                {
                    var _saccount = (from k in datam.DATA_CURRENT_PLEDGE_SETTINGS.Values
                                     where k.account_id == _acc.account_id & k.status == em.pledge_setting_statusS.valid
                                     orderby k.pls_id descending
                                     select k).FirstOrDefault();
                   var _drop_down=   fGrid.Rows["start_date"].Cells[1].DropDownControl  as fnn.DropDownCalenderX;
                    fGrid.Rows["start_date"].Cells[1].Value = null;
                    fGrid.Rows["start_date"].Cells[1].AuxValue = null;
                    if (_saccount != null)
                    {
                        _drop_down.start_date = _saccount.end_date.Value.AddDays(1);
                        _drop_down.selected_date = _drop_down.start_date;
                        _drop_down.end_date = _drop_down.start_date.Value.AddDays(365);
                    }
                    else
                    {
                        _drop_down.start_date = sdata.CURR_DATE.AddDays(-365);
                        _drop_down.selected_date = sdata.CURR_DATE;
                        _drop_down.end_date = sdata.CURR_DATE.AddDays(365);
                    }
                    fGrid.Rows["start_date"].Cells[1].Enabled = iGBool.True;
                    fGrid.Rows["end_date"].Cells[1].Value = null;
                    fGrid.Rows["end_date"].Cells[1].AuxValue = null;
                    //
                    fGrid.Rows["end_date"].Cells[1].Enabled = iGBool.False;
                   
                }
            }
            if (_row.Key == "end_date")
            {
                if (_row.Cells[1].Value != null)
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
            foreach (var k in fGrid.Rows.Cast<iGRow>())
            {
                k.Cells[1].Value = null;
                k.Cells[1].AuxValue = null;
            }
           //fGrid*/.Rows["start_date"].Cells[1].Value = sdata.CURR_DATE;
            fGrid.Focus();
            fGrid.SetCurCell("pledge_name", 1);
            fGrid.Rows["start_date"].Cells[1].Enabled = iGBool.False;
            fGrid.Rows["end_date"].Cells[1].Enabled = iGBool.False;

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
            var _comp = new string[] { "pledge_name", "start_date", "account", "end_date" };
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
            return true;
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                return;
            }
            string _str = "Are You Sure You Want To Save This Record ??";
            if (!dbm.WarningMessage(_str, "Save Warning"))
            {
                return;
            }
            ic.pledgeSettingC _pledge = new ic.pledgeSettingC();
            _pledge.pledge_name = fGrid.Rows["pledge_name"].Cells[1].Text.Trim();
            _pledge.pledge_name.ToProperCase();
            _pledge.start_date = System.Convert.ToDateTime(fGrid.Rows["start_date"].Cells[1].AuxValue);
            _pledge.start_fs_id = fn.GetFSID(_pledge.start_date.Value);
            _pledge.status = em.pledge_setting_statusS.valid;
            if (fGrid.Rows["end_date"].Cells[1].AuxValue != null)
            {
                _pledge.end_date = System.Convert.ToDateTime(fGrid.Rows["end_date"].Cells[1].AuxValue);
                _pledge.end_fs_id = fn.GetFSID(_pledge.end_date.Value);
            }
            ic.accountC account = (fGrid.Rows["account"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag as ic.accountC;
            if (account != null)
            {
                _pledge.account_id = account.account_id;
            }
            using (var xd = new xing())
            {
                _pledge.pls_id = xd.SingleInsertCommandTSPInt("pledge_settings_tb", new string[]
                  {
                    "account_id",
                    "start_date",
                    "start_fs_id",
                    "end_date",
                    "end_fs_id",
                    "fs_date",
                    "pc_us_id",
                    "exp_type",
                    "fs_time_stamp",
                    "lch_id",
                    "start_year","pledge_name"

                      }, new object[]
                      {
                        _pledge.account_id,
                        _pledge.start_date,
                        _pledge.start_fs_id,
                        _pledge.end_date,
                        _pledge.end_fs_id,
                        sdata.CURR_DATE,
                        sdata.PC_US_ID,
                        emm.export_type.insert.ToByte(),
                        0,
                        datam.LCH_ID,
                        _pledge.start_date.Value.Year,
                        _pledge.pledge_name
                          });
                xd.CommitTransaction();
            }
            if (this.Owner is Pledge.PledgesManagerB)
            {
                (this.Owner as Pledge.PledgesManagerB).NewRecord(_pledge);
            }
            buttonX2.PerformClick();
            
        }

        private void fGrid_Click(object sender, EventArgs e)
        {

        }
    }
}
