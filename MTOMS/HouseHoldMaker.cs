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
using SdaHelperManager.Security;
namespace MTOMS
{
    public partial class HouseHoldMaker : DevComponents.DotNetBar.Office2007Form
    {
        public HouseHoldMaker()
        {
            InitializeComponent();
        }
        private iGDropDownList m_Husbands = null;
        private iGDropDownList m_Wifes = null;
        private void HouseHoldMaker_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            InitIgridColumns();
            datam.ShowWaitForm();
            Application.DoEvents();
            datam.SecurityCheck();
            backworker.RunWorkerAsync();
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
        private void LoadLists()
        {
            m_Husbands = fnn.CreateCombo();
            m_Wifes = fnn.CreateCombo();
            var _husbands = from k in datam.DATA_MEMBER.Values
                            where k.IsCurrentMember & k.gender_id == (em.xgender.Male).ToByte() & (k.objSpouse == null || (k.objSpouse.couple_id == 0 & k.objSpouse.spouse_id == 0))
                            select k;
            //var _husbands = from k in datam.DATA_MEMBER.Values
            //                where k.IsCurrentMember & k.gender_id == (em.xgender.Male).ToByte() 
            //                select k;
            foreach (var d in _husbands)
            {
                m_Husbands.Items.Add(new fnn.iGComboItemEX()
                {
                    ID=d.mem_id,
                    Value=d.mem_name,
                    Tag=d
                });
            }
            fGrid.Rows["husband"].Cells["desc"].DropDownControl = m_Husbands;
            //
            var _wifes = from k in datam.DATA_MEMBER.Values
                         where k.IsCurrentMember & k.gender_id == (em.xgender.Female).ToByte() & (k.objSpouse == null || (k.objSpouse.couple_id == 0 & k.objSpouse.spouse_id == 0))
                         select k;
           //var _wifes = from k in datam.DATA_MEMBER.Values
           //             where k.IsCurrentMember & k.gender_id == (em.xgender.Female).ToByte() 
           //             select k;
            foreach (var d in _wifes)
            {
                m_Wifes.Items.Add(new fnn.iGComboItemEX()
                {
                    ID = d.mem_id,
                    Value = d.mem_name,
                    Tag=d
                });
            }
            fGrid.Rows["wife"].Cells["desc"].DropDownControl = m_Wifes;
            fGrid.Focus();
            fGrid.SetCurCell("husband", 1);
        }
        private void backworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadMainGrid();
            LoadLists();
           
            datam.HideWaitForm();
        }
        private bool CheckKukoos()
        {
            var _husband_id = fGrid.Rows["husband"].Cells["desc"].AuxValue == null ? 0 : (fGrid.Rows["husband"].Cells["desc"].AuxValue as fnn.iGComboItemEX).ID;
            var _wife_id = fGrid.Rows["wife"].Cells["desc"].AuxValue == null ? 0 : (fGrid.Rows["wife"].Cells["desc"].AuxValue as fnn.iGComboItemEX).ID;
            if (_husband_id > 0 & _wife_id > 0)
            {
                var _husband = datam.DATA_MEMBER[_husband_id];
                if (_husband != null)
                {
                    if (_husband.objSpouse != null)
                    {
                        var _wife = datam.DATA_MEMBER[_wife_id];
                        if (_wife.objSpouse != null)
                        {
                            if (_husband.objSpouse.marriage_fs_id > 0 & _wife.objSpouse.marriage_fs_id > 0)
                            {
                                if (_husband.objSpouse.marriage_fs_id != _wife.objSpouse.marriage_fs_id)
                                {
                                    MessageBox.Show("The Marriage Dates Of The Husband And Wife Do Not Match", "Wrong Couple Error");
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }
        private iGRow CreateNewRow(string field, string rowkey, Type _type, int group_index, string group_name)
        {
            var _row = fGrid.Rows.Add();
            _row.Font = new Font("georgia", 14, FontStyle.Regular);
            _row.Cells["desc"].Font = new Font("arial", 14, FontStyle.Regular);
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
        private void LoadMainGrid()
        {
            fGrid.BeginUpdate();
            iGRow _row = null;
            _row = fGrid.Rows.Add();
            _row.Height = 7;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            iGDropDownList icombo = null;
            int group_index = 1;
            string group_name = "New Account Information";
            _row = CreateNewRow("Husband's Name", "husband", typeof(string), group_index, group_name);
            _row.Cells[1].TypeFlags = iGCellTypeFlags.HasEllipsisButton;
            _row.Height += 2;
            _row = CreateNewRow("Wife's Name", "wife", typeof(string), group_index, group_name);
            _row.Cells[1].TypeFlags = iGCellTypeFlags.HasEllipsisButton;
            _row.Height += 2;
            //
            _row = fGrid.Rows.Add();
            _row.Height = 7;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            //
            _row = CreateNewRow("HouseHold Name", "household", typeof(string), group_index, group_name); _row.Height += 2;
            _row.Cells[1].ForeColor = Color.Maroon;
            //
            _row = fGrid.Rows.Add();
            _row.Height = 7;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            //
            group_name = "Spouse And Marriage Details";
            CreateNewRow("Year Of Marriage", "marr_yr", typeof(Int32), group_index, group_name);
            _row = CreateNewRow("Date Of Marriage", "marr_date", typeof(Int32), group_index, group_name);
            _row.Cells["desc"].TypeFlags = iGCellTypeFlags.ComboPreferValue | iGCellTypeFlags.NoTextEdit | iGCellTypeFlags.HasEllipsisButton;
            _row.Cells["desc"].FormatString = "{0:D}";// {0:d} for short date string
            _row.Cells["desc"].Value = null;// DateTime.Parse("1/1/2010");
            _row.Cells["desc"].DropDownControl = new fnn.DropDownCalendar();
            _row.Cells["desc"].ReadOnly = iGBool.True;
            _row = CreateNewRow("Church Name", "marr_church", typeof(string), group_index, group_name);
            #region MyRegion
            icombo = fnn.CreateCombo();
            icombo.SearchAsType.MatchRule = iGMatchRule.StartsWith;
            icombo.MaxVisibleRowCount = 12;
            var clist = from r in xso.xso.DATA_REGION.Values
                        where r.item_type==xso.em.cregion_type.church & r.item_id > 0
                        select r;
            foreach (var f in clist)
            {
                icombo.Items.Add(new fnn.iGComboItemEX()
                {
                    Text = f.item_name,
                    Value = f.item_name,
                    Tag = f
                });
            }
            _row.Cells["desc"].DropDownControl = icombo;
            _row.Cells["desc"].Value = null;
            #endregion

            _row = CreateNewRow("Pastor's Name", "marr_pastor", typeof(string), group_index, group_name);
            fGrid.EndUpdate();
        }

        private void fGrid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
        {
            iGRow _row = fGrid.Rows[e.RowIndex];
            if (_row.Cells[1].DropDownControl != null && _row.Cells[1].AuxValue == null)
            {
                if (_row.Key != "marr_church")
                {
                    _row.Cells[1].Value = null;
                    return;
                }
               
            }
            if (_row.Key == "household")
            {
                if (_row.Cells[1].Value != null)
                {
                    _row.Cells[1].Value = _row.Cells[1].Value.ToProperCase();
                }
            }
            if (_row.Key == "husband" | _row.Key=="wife")
            {
                if (!CheckKukoos())
                {
                    return;
                }
                if (fGrid.Rows["husband"].Cells[1].Value != null & fGrid.Rows["wife"].Cells[1].Value != null)
                {
                    var _husband = (fGrid.Rows["husband"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag as ic.memberC;
                    var _wife = (fGrid.Rows["wife"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag as ic.memberC;
                    if (_husband.objSpouse != null)
                    {
                        if (_husband.objSpouse.marriage_year > 0)
                        {
                            fGrid.Rows["marr_yr"].Cells["desc"].Value = _husband.objSpouse.marriage_year;
                        }
                        if (_husband.objSpouse.marriage_date != null & fGrid.Rows["marr_yr"].Cells["desc"].Value != null)
                        {
                            int marr_yr = fGrid.Rows["marr_yr"].Cells["desc"].Value.ToInt32();
                            fGrid.Rows["marr_date"].Cells["name"].AuxValue = marr_yr;
                            fGrid.Rows["marr_date"].Cells["desc"].ReadOnly = iGBool.False;
                            fGrid.Rows["marr_date"].Cells["desc"].Value = _husband.objSpouse.marriage_date;
                        }
                        if (!string.IsNullOrEmpty(_husband.objSpouse.church))
                        {
                            fGrid.Rows["marr_church"].Cells["desc"].Value = _husband.objSpouse.church;
                        }
                        if (!string.IsNullOrEmpty(_husband.objSpouse.pastor))
                        {
                            fGrid.Rows["marr_pastor"].Cells["desc"].Value = _husband.objSpouse.pastor;
                        }
                    }
                    if (_wife.objSpouse!= null)
                    {
                        if (fGrid.Rows["marr_yr"].Cells["desc"].Value==null & _wife.objSpouse.marriage_year > 0)
                        {
                            fGrid.Rows["marr_yr"].Cells["desc"].Value = _wife.objSpouse.marriage_year;
                        }
                        if (fGrid.Rows["marr_date"].Cells["desc"].Value==null & _wife.objSpouse.marriage_date != null & fGrid.Rows["marr_yr"].Cells["desc"].Value != null)
                        {
                            int marr_yr = fGrid.Rows["marr_yr"].Cells["desc"].Value.ToInt32();
                            fGrid.Rows["marr_date"].Cells["name"].AuxValue = marr_yr;
                            fGrid.Rows["marr_date"].Cells["desc"].ReadOnly = iGBool.False;
                            fGrid.Rows["marr_date"].Cells["desc"].Value = _wife.objSpouse.marriage_date;
                        }
                        if (fGrid.Rows["marr_church"].Cells["desc"].Value==null & !string.IsNullOrEmpty(_wife.objSpouse.church))
                        {
                            fGrid.Rows["marr_church"].Cells["desc"].Value = _wife.objSpouse.church;
                        }
                        if ( fGrid.Rows["marr_pastor"].Cells["desc"].Value==null & !string.IsNullOrEmpty(_wife.objSpouse.pastor))
                        {
                            fGrid.Rows["marr_pastor"].Cells["desc"].Value = _wife.objSpouse.pastor;
                        }
                    }
                }
                else
                {
                    foreach (var t in new string[] { "marr_yr", "marr_date", "marr_church", "marr_pastor" })
                    {
                        fGrid.Rows[t].Cells[1].Value = null;
                        fGrid.Rows["marr_date"].Cells["desc"].ReadOnly = iGBool.True;
                    }
                }
            }
            if (_row.Key == "marr_yr")
            {
                #region MyRegion
                if (fGrid.Rows["marr_yr"].Cells["desc"].Value != null && (fGrid.Rows[e.RowIndex].Cells["desc"].Value.ToInt32() >= 1900 & fGrid.Rows[e.RowIndex].Cells["desc"].Value.ToInt32() <= sdata.CURR_DATE.Year))
                {
                    int marr_yr = fGrid.Rows["marr_yr"].Cells["desc"].Value.ToInt32();
                    fGrid.Rows["marr_date"].Cells["name"].AuxValue = marr_yr;
                    fGrid.Rows["marr_date"].Cells["desc"].ReadOnly = iGBool.False;
                }
                else
                {
                    fGrid.Rows["marr_yr"].Cells["desc"].Value = null;
                    fGrid.Rows["marr_date"].Cells["desc"].Value = null;
                    fGrid.Rows["marr_date"].Cells["desc"].ReadOnly = iGBool.True;
                    fGrid.Rows["marr_date"].Cells["name"].AuxValue = null;

                }
                #endregion
                
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

        private void buttonX1_Click(object sender, EventArgs e)
        {
            var _strs = new string[] { "husband", "wife", "household","marr_yr" };
            foreach (var k in _strs)
            {
                if (fGrid.Rows[k].Cells[1].Value == null)
                {
                    MessageBox.Show("Important Field Left Blank", "Invalid Field");
                    fGrid.Focus();
                    fGrid.SetCurCell(k, 1);
                    return;
                }
            }
           string _str = "Are You Sure You Want To Create The Above HouseHold ??";
           if (!dbm.WarningMessage(_str, "Create Warning"))
           {
               return;
           }
           
            
            var _husband = (fGrid.Rows["husband"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag as ic.memberC;
            var _wife = (fGrid.Rows["wife"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag as ic.memberC;
            ic.coupleC _couple = new MTOMS.ic.coupleC();
            _couple.couple_name = fGrid.Rows["household"].Cells[1].Text;
            _couple.female_mem_id = _wife.mem_id;
            _couple.male_mem_id = _husband.mem_id;
            _couple.is_valid = true;
            _couple.lch_id = datam.LCH_ID;
            _couple.lch_type_id = datam.LCH_TYPE_ID;

            using (var xd = new xing())
            {
                SaveCouple(xd, _couple);
                if (_husband.objSpouse == null)
                {
                     //
                    _husband.objSpouse = new ic.spouseC();
                    _husband.objSpouse.lch_id = datam.LCH_ID;
                    _husband.objSpouse.lch_type_id = datam.LCH_TYPE_ID;
                   //
                    _husband.objSpouse.mem_id = _husband.mem_id;
                    _husband.objSpouse.spouse_id = _wife.mem_id;
                    _husband.objSpouse.couple_id = _couple.couple_id;
                    //
                    Save_spouse(xd, _husband);
                    //
                 }
                //
                _husband.objSpouse.mem_id = _husband.mem_id;
                _husband.objSpouse.spouse_id = _wife.mem_id;
                _husband.objSpouse.couple_id = _couple.couple_id;
                _husband.objSpouse.spouse_name = _wife.mem_name;
                if (_wife.objAddress != null)
                {
                    _husband.objSpouse.phone_no = _wife.objAddress.phone1;
                }
                //
                _husband.objSpouse.church = string.IsNullOrEmpty(_husband.objSpouse.church) ? fGrid.Rows["marr_church"].Cells[1].Text : null;
                _husband.objSpouse.pastor = string.IsNullOrEmpty(_husband.objSpouse.pastor) ? fGrid.Rows["marr_pastor"].Cells[1].Text : null;
                _husband.objSpouse.spouse_id = _wife.mem_id;
                _husband.objSpouse.mem_id = _husband.mem_id;
                if (fGrid.Rows["marr_date"].Cells[1].Value != null & _husband.objSpouse.marriage_date == null)
                {
                    _husband.objSpouse.marriage_date = System.Convert.ToDateTime(fGrid.Rows["marr_date"].Cells[1].Value);
                    _husband.objSpouse.marriage_fs_id = fn.GetFSID(_husband.objSpouse.marriage_date.Value);
                }
               _husband.objSpouse.marriage_year = _husband.objSpouse.marriage_year == 0 ? fGrid.Rows["marr_yr"].Cells["desc"].Value.ToInt32() : 0;
               Update_Spouse(xd, _husband);
                //
                if (_wife.objSpouse == null)
                {
                    _wife.objSpouse = new ic.spouseC();
                    _wife.objSpouse.lch_id = datam.LCH_ID;
                    _wife.objSpouse.lch_type_id = datam.LCH_TYPE_ID;
                    //
                    _wife.objSpouse.mem_id = _wife.mem_id;
                    _wife.objSpouse.spouse_id = _husband.mem_id;
                    _wife.objSpouse.spouse_name = _husband.mem_name;
                    _wife.objSpouse.couple_id = _couple.couple_id;
                 
                    Save_spouse(xd, _wife);
                    //
                 }
                _wife.objSpouse.mem_id = _wife.mem_id;
                _wife.objSpouse.spouse_id = _husband.mem_id;
                _wife.objSpouse.spouse_name = _husband.mem_name;
                if (_husband.objAddress != null)
                {
                    _wife.objSpouse.phone_no = _husband.objAddress.phone1;
                }
                _wife.objSpouse.couple_id = _couple.couple_id;
                _wife.objSpouse.church = string.IsNullOrEmpty(_wife.objSpouse.church) ? fGrid.Rows["marr_church"].Cells[1].Text : null;
                _wife.objSpouse.pastor = string.IsNullOrEmpty(_wife.objSpouse.pastor) ? fGrid.Rows["marr_pastor"].Cells[1].Text : null;
                _wife.objSpouse.spouse_id = _husband.mem_id;
                _wife.objSpouse.mem_id = _wife.mem_id;
                if (fGrid.Rows["marr_date"].Cells[1].Value != null & _wife.objSpouse.marriage_date == null)
                {
                    _wife.objSpouse.marriage_date = System.Convert.ToDateTime(fGrid.Rows["marr_date"].Cells[1].Value);
                    _wife.objSpouse.marriage_fs_id = fn.GetFSID(_wife.objSpouse.marriage_date.Value);
                }
                _wife.objSpouse.marriage_year = _wife.objSpouse.marriage_year == 0 ? fGrid.Rows["marr_yr"].Cells["desc"].Value.ToInt32() : 0;
                Update_Spouse(xd, _wife);
                //
                xd.CommitTransaction();
            }
            (fGrid.Rows["husband"].Cells[1].AuxValue as fnn.iGComboItemEX).Visible = false;
            (fGrid.Rows["wife"].Cells[1].AuxValue as fnn.iGComboItemEX).Visible = false;
            buttonX2.PerformClick();
            
            
        }
        private void SaveCouple(xing xd, ic.coupleC _couple)
        {
           var tb_col = new string[]
                {  "couple_name",
                    "male_mem_id",
                    "female_mem_id",
                    "is_valid",
                    "exp_type",
                    "pc_us_id",
                    "pc_us_name",
                    "edate",
                    "lch_id",
                    "lch_type_id",
                    "fs_time_stamp"
                 };
           
            _couple.is_valid = true;
            var _row = new object[]
                    {
                    _couple.couple_name,
                    _couple.male_mem_id,
                    _couple.female_mem_id,
                     1,
                    emm.export_type.insert.ToByte(),
                    datam.PC_US_ID,
                    datam.PC_US_NAME,
                    datam.CURR_DATE,
                   _couple.lch_id= datam.LCH_ID,
                   _couple.lch_type_id=datam.LCH_TYPE_ID,
                   0//fs_time_stamp
                    };
            _couple.couple_id = xd.SingleInsertCommandTSPInt("couple_tb", tb_col, _row);
            datam.DATA_COUPLE.Add(_couple.couple_id, _couple);
        }
        private void Update_Spouse(xing xd, ic.memberC mem_obj)
        {
            ic.spouseC objSpouse = mem_obj.objSpouse;
            string[] tb_col = null;
            object[] _row = null;
            tb_col = new string[]
            {
            "mem_id",
            "spouse_id",
            "spouse_name",
            "phone_no",
            "marriage_year",
            "marriage_date",
            "marriage_fs_id",
            "couple_id",
            "pastor_name",
            "church_name",
            "church_id","un_id"
           
            };
            _row = new object[]
            {
            objSpouse.mem_id,
            objSpouse.spouse_id,
            objSpouse.spouse_name,
            objSpouse.phone_no,
            objSpouse.marriage_year,
            objSpouse.marriage_date,
            objSpouse.marriage_fs_id,
            objSpouse.couple_id,
            objSpouse.pastor,
            objSpouse.church,
            0,objSpouse.un_id
           };
            xd.SingleUpdateCommandALL("ng_spouse_tb", tb_col, _row, 1);
                           
        }
        public void Save_spouse(xing xd, ic.memberC mem_obj)
        {
            ic.spouseC objSpouse = mem_obj.objSpouse;
           
            string[] tb_col = null;
            object[] _row = null;
            tb_col = new string[]
            {
            "mem_id",
            "spouse_id",
            "spouse_name",
            "phone_no",
            "marriage_year",
            "marriage_date",
            "marriage_fs_id",
            "couple_id",
            "exp_type",
            "pastor_name",
            "pastor_id",
            "church_name",
            "church_id",
            "lch_id",
            "lch_type_id",
            "fs_time_stamp"
            };
            _row = new object[]
            {
            objSpouse.mem_id,
            objSpouse.spouse_id,
            objSpouse.spouse_name,
            objSpouse.phone_no,
            objSpouse.marriage_year,
            objSpouse.marriage_date,
            objSpouse.marriage_fs_id,
            objSpouse.couple_id,
            emm.export_type.insert.ToByte(),
            objSpouse.pastor,
            0,
            objSpouse.church,
            0,
            datam.LCH_ID,
            datam.LCH_TYPE_ID,
            0//fs_time_stamp
            };
            objSpouse.un_id = xd.SingleInsertCommandTSPInt("ng_spouse_tb", tb_col, _row);
            
                  
        }
        private void fGrid_EllipsisButtonClick(object sender, iGEllipsisButtonClickEventArgs e)
        {
            var _row = fGrid.Rows[e.RowIndex];
            if (string.IsNullOrEmpty(_row.Key)) { return; }
            if (_row.Cells[1].Value == null) { return; }
            if (_row.Key == "husband" | _row.Key=="wife")
            {
                using (var _fm = new MemberDetail())
                {
                    _fm.Owner = this;
                    _fm.Tag = (_row.Cells[1].AuxValue as fnn.iGComboItemEX).Tag;
                    _fm.ShowDialog();
                }
            }
            else
            {
               
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            foreach (var k in fGrid.Rows.Cast<iGRow>())
            {
                k.Cells[1].Value = null;
                k.Cells[1].AuxValue = null;
            }
            fGrid.Rows["marr_date"].Cells["desc"].ReadOnly = iGBool.True;
            fGrid.Focus();
            fGrid.SetCurCell("husband", 1);
            
        }
    }
}
