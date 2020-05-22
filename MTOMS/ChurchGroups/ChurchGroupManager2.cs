using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SdaHelperManager;
using System.Runtime.InteropServices;
using TenTec.Windows.iGridLib;
using System.Reflection;
using SdaHelperManager.Security;


namespace MTOMS
{
    public partial class ChurchGroupManager2 : DevComponents.DotNetBar.Office2007Form
    {
        public ChurchGroupManager2()
        {
            InitializeComponent();
        }
        
        ic.RoleC m_prev_role = null;
        SortedList<int, iGDropDownList> m_LDATA = null;
        
     
        private void UpdateRoles(ic.church_groupC _gp)
        {
            if (_gp == null) { return; }
            var nlist = from k in m_LDATA[_gp.cg_id].Items.Cast<fnn.iGComboItemEX>()
                        where k.Visible
                        select k;
            ic.RoleC _role = null;
            List<int> _ids = new List<int>();
            foreach (var k in nlist)
            {
                _role = k.Tag as ic.RoleC;
                if (datam.DATA_ROLES.Keys.IndexOf(_role.role_id) == -1 )
                {
                    k.Visible = false; continue;
                }
                k.Value = _role.role_name;
                k.Visible = true;
                _ids.Add(_role.role_id);
            }
            var new_list = from k in datam.DATA_ROLES.Values
                           where k.gp_type == em.role_gp_typeS.church_group & k.gp_id == _gp.cg_id
                           & _ids.IndexOf(k.role_id) == -1
                           select k;
            foreach (var r in new_list)
            {
                m_LDATA[_gp.cg_id].Items.Add(new fnn.iGComboItemEX()
                {
                    Tag = r,
                    Value = r.role_name,
                });
            }
        }
        private void ChurchGroupManager_Load(object sender, EventArgs e)
        {
            datam.SecurityCheck();
            datam.SystemInitializer();
            InitIgridColumns();
            LoadMainGrid();
            InitIgridColumnsTwo();
            this.VisibleChanged += new EventHandler(ChurchGroupManager_VisibleChanged);
            buttonX2.Visible = false;
            buttonR.Visible = false;
            m_LDATA = new SortedList<int, iGDropDownList>();
        }
        void ChurchGroupManager_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                UpdateGrid();
            }
        }
        private void position_button_controller1()
        {
            if (fGrid2.Rows.Count > 0)
            {
                iGCell _cell = fGrid2.Rows["othersxxx"].Cells["mem"];
                if (_cell != null)
                {
                    buttonX2.Left = fGrid2.Left + (_cell.Bounds.Left + _cell.Bounds.Width - buttonX2.Width);
                    buttonX2.Top = fGrid2.Top + _cell.Bounds.Top + 1;
                }
            }
            if (fGrid2.Rows.Count > 0)
            {
                iGCell _cell = fGrid2.Rows["othersxxx"].Cells["desig"];
                if (_cell != null)
                {
                    buttonR.Left = fGrid2.Left + (_cell.Bounds.Left + _cell.Bounds.Width - buttonR.Width);
                    buttonR.Top = fGrid2.Top + _cell.Bounds.Top + 1;
                }
            }
           // if (!buttonX2.Visible) { buttonX2.Visible = true; }
        }
        ic.church_groupC m_cgroup = null;
        public void NewGroup(ic.church_groupC obj)
        {
            var  _row = fGrid.Rows.Add();
            _row.Font = new Font("verdana", 10, FontStyle.Bold);
            _row.Cells["gp_name"].Col.Width = 220;
            _row.Cells["mem_cnt"].Col.Width = 100;
            _row.Cells["gp_name"].Value = obj.cg_name;
            _row.Cells["gp_name"].TextAlign = iGContentAlignment.BottomRight;
            _row.Cells["mem_cnt"].ValueType = typeof(int);
            _row.Key = obj.cg_id.ToStringNullable();
            _row.AutoHeight();
            _row.Cells["type_id"].Value = obj.cg_type_id;
            _row.Cells["mem_cnt"].Value = obj.MemberCount;
            _row.Tag = obj;
            fGrid.SortObject.Add("type_id");
            fGrid.Sort();
            fGrid.GroupObject.Add("type_id");
            fGrid.Group();
            fGrid.PerformAction(iGActions.CollapseAll);
           
            var _rw = GetGroupRow(_row, obj.cg_type_id);
            if (_rw != null)
            {
                _rw.Expanded = true;
            }
            fGrid.SetCurCell(obj.cg_id.ToString(), 0);
            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
            fGrid.EndUpdate();
          
        }
        public iGRow GetGroupRow(iGRow _child,int _key)
        {
            return (from j in fGrid.Rows.Cast<iGRow>()
                        where  j.Type == iGRowType.AutoGroupRow & j.RowTextCell.AuxValue.ToByte()==_key
                        select j).FirstOrDefault();
        }
        #region IgridOne
        public class iGComboItemEX : iGDropDownListItem
        {
            public object Tag { get; set; }
        }
        private iGDropDownList CreateCombo()
        {
            iGDropDownList icombo = new iGDropDownList();
            icombo.SelectedItemChanged += new iGSelectedItemChangedEventHandler(icombo_SelectedItemChanged);
            icombo.MaxVisibleRowCount = 15;
            icombo.SearchAsType.MatchRule = iGMatchRule.Contains;
            icombo.SearchAsType.AutoCompleteMode = iGSearchAsTypeMode.Filter;
            icombo.SelItemBackColor = Color.Lavender;
            return icombo;
        }
        void icombo_SelectedItemChanged(object sender, iGSelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                fGrid.CurCell.AuxValue = null;
            }
        }
        void InitIgridColumns()
        {
            #region Adjust the grid's appearance
            // Set the flat appearance for the cells.
            fGrid.Header.Visible = true;
            fGrid.SelectionMode = iGSelectionMode.One;
            fGrid.Appearance = iGControlPaintAppearance.StyleFlat;
            fGrid.UseXPStyles = false;
            fGrid.ScrollBarSettings.Appearance = iGControlPaintAppearance.StyleFlat;
            fGrid.FocusRect = false;
            #endregion
            iGCol myCol;
            //
            myCol = fGrid.Cols.Add("gp_name", "Group Name");
            myCol.CellStyle.ReadOnly = iGBool.True;
            myCol.ColHdrStyle.TextAlign = iGContentAlignment.MiddleCenter;
            myCol.SortType = iGSortType.None;
            myCol.Width = 300;
            myCol.CellStyle.ForeColor = Color.DarkBlue;
            myCol.ColHdrStyle.ForeColor = Color.DarkGreen;
            myCol.ColHdrStyle.Font = new Font("arial", 10, FontStyle.Bold);
            myCol.CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
            myCol.CellStyle.ReadOnly = iGBool.True;
         
            myCol.IncludeInSelect = true;
            //
            myCol.AllowSizing = false;
            myCol = fGrid.Cols.Add("mem_cnt", "M Count");
            myCol.Width = 150;
            myCol.CellStyle.ReadOnly = iGBool.True;
            myCol.CellStyle.TextAlign = iGContentAlignment.MiddleLeft;
            myCol.ColHdrStyle.TextFormatFlags = iGStringFormatFlags.WordWrap;
            myCol.ColHdrStyle.ForeColor = Color.DarkGreen;
            myCol.ColHdrStyle.Font = new Font("arial", 10, FontStyle.Bold);
            myCol.SortType = iGSortType.None;
            myCol.AllowSizing = false;
            myCol.CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
            fGrid.Cols.Add("type_id","type_id").Visible = false;
          //  fGrid.Cols["type_id"].CellStyle.ValueType = typeof(int);
            //  myCol.AllowSizing = false;
            //
            // Add a special column which will store the category name.
            // This column will be used for grouping.
            // fGrid.Cols.Add("category", string.Empty).Visible = false;
            // Add a special column which will store the default 
            // values for the properties.
            // fGrid.Cols.Add("svalue", string.Empty).Visible = false;
            // myCol = fGrid.Cols["svalue"];
            //
           fGrid.Cols["type_id"].SortType = iGSortType.ByValue;
            fGrid.DefaultRow.Height = fGrid.GetPreferredRowHeight(true, false);
            fGrid.DefaultAutoGroupRow.Height = fGrid.DefaultRow.Height;
        }
        private void UpdateGrid()
        {
            if (fGrid.Rows.Count == 0) { return; }
            if (datam.DATA_CHURCH_GROUPS == null) { return; }
            if (datam.DATA_CHURCH_GROUPS.Keys.Count == 0) { return; }
            fGrid.BeginUpdate();
            foreach (var obj in datam.DATA_CHURCH_GROUPS.Values)
            {
                if (fGrid.Rows[obj.cg_id.ToString()].Cells["mem_cnt"].Value.ToInt32() != obj.MemberCount)
                {
                    fGrid.Rows[obj.cg_id.ToString()].Cells["mem_cnt"].Value = obj.MemberCount;
                }
                if (fGrid.Rows[obj.cg_id.ToString()].Selected)
                {
                    LoadMainGrid2(obj);
                }
            }
            fGrid.EndUpdate();
        }
        private void LoadMainGrid()
        {
            iGRow _row = null;
            if (datam.DATA_CHURCH_GROUPS == null) { return; }
            if (datam.DATA_CHURCH_GROUPS.Keys.Count == 0) { return; }
            fGrid.BeginUpdate();
            foreach (var obj in datam.DATA_CHURCH_GROUPS.Values)
            {
               
                _row = fGrid.Rows.Add();
                _row.Font = new Font("verdana", 10, FontStyle.Bold);
                _row.Cells["gp_name"].Col.Width = 220;
                _row.Cells["mem_cnt"].Col.Width = 100;
                _row.Cells["gp_name"].Value = obj.cg_name;
                _row.Cells["gp_name"].TextAlign = iGContentAlignment.BottomRight;
                _row.Cells["mem_cnt"].ValueType = typeof(int);
                _row.Key = obj.cg_id.ToStringNullable();
                _row.AutoHeight();
                _row.Cells["type_id"].Value = obj.cg_type_id;
                _row.Cells["mem_cnt"].Value = obj.MemberCount;
                _row.Tag = obj;
               
            }
            fGrid.SortObject.Add("type_id");
            fGrid.Sort();
            fGrid.GroupObject.Add("type_id");
            fGrid.Group();
            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
            fGrid.PerformAction(iGActions.CollapseAll);
            if (fGrid.Rows.Count > 0)
            {
                fGrid.Rows[0].Expanded = true;
            }
            fGrid.EndUpdate();

        }
        public void count_Rows()
        {
            int count = 1;
            foreach (iGRow row in fGrid2.Rows)
            {
                if (row.Index > fGrid2.Rows["othersxxx"].Index && row.Cells[0].AuxValue == null)
                {
                    row.Cells[0].Value = count;
                    row.Cells[0].TextAlign = iGContentAlignment.MiddleRight;
                    count += 1;
                }
            }
        }
        private void fGrid_CurRowChanged(object sender, EventArgs e)
        {
            //check if pastor is selected
            if(fGrid.CurCell==null || fGrid.CurCell.Row.Type==iGRowType.AutoGroupRow)
            {
                label1.Text=string.Empty;
                fGrid2.Rows.Clear();
                m_cgroup = null;
                buttonX1.Visible = false;
                buttonX2.Visible = false;
                buttonR.Visible = false;
                return;
            }
            label1.Text = fGrid.CurRow.Cells[0].Value.ToStringNullable();
            m_cgroup = fGrid.CurRow.Tag as ic.church_groupC;
            buttonX1.Visible = false;
            buttonX2.Visible = false;
            buttonR.Visible = false;
            LoadMainGrid2(m_cgroup);
        }
        #endregion
        #region IgridTwo
        void InitIgridColumnsTwo()
        {
            #region Adjust the grid's appearance
            // Set the flat appearance for the cells.
          
            fGrid2.Appearance = iGControlPaintAppearance.StyleFlat;
            fGrid2.UseXPStyles = false;
            fGrid2.ScrollBarSettings.Appearance = iGControlPaintAppearance.StyleFlat;
            fGrid2.FocusRect = false;
            #endregion
            iGCol myCol;
            //
            myCol = fGrid2.Cols.Add("type", "Roles");
            myCol.CellStyle.ReadOnly = iGBool.True;
            myCol.SortType = iGSortType.None;
            myCol.CellStyle.ForeColor = Color.Maroon;
            myCol.CellStyle.BackColor = Color.WhiteSmoke;
            myCol.CellStyle.ReadOnly = iGBool.True;
            myCol.AllowSizing = false;
           
            myCol = fGrid2.Cols.Add("mem", "Member");
            myCol.AllowSizing = false;
            myCol = fGrid2.Cols.Add("_pic","D");
            myCol.AllowSizing = false;
            myCol.CellStyle.Selectable = iGBool.False;
            //
            myCol = fGrid2.Cols.Add("desig", "Roles/Designation");
            myCol.CellStyle.ReadOnly = iGBool.True;
            myCol.SortType = iGSortType.None;
            myCol.CellStyle.ForeColor = Color.DarkBlue;
            myCol.AllowSizing = false;
            //
            fGrid2.DefaultRow.Height = fGrid2.GetPreferredRowHeight(true, false);
            fGrid2.DefaultAutoGroupRow.Height = fGrid2.DefaultRow.Height;
        }
        private iGRow CreateNewRow2()
        {
            var _row = fGrid2.Rows.Add();
            _row.Font = new Font("verdana", 10, FontStyle.Bold);
            _row.Cells["type"].Col.Width = 80;
            _row.Cells["mem"].Col.Width = 270;
            _row.Cells["desig"].Col.Width = 290;
            _row.AutoHeight();
            _row.Height += 2;
            return _row;

        }
        public int get_rowCount()
        {
            int cout = 0;
            foreach (iGRow row in fGrid2.Rows)
            {
                if (row.Index > fGrid2.Rows["others"].Index)
                {
                    cout += 1;
                }
            }
            return cout;

        }
        private void LoadMainGrid2(ic.church_groupC _gp)
        {
            if (m_LDATA.Keys.IndexOf(_gp.cg_id) == -1)
            {
                m_LDATA.Add(_gp.cg_id, new iGDropDownList());
                m_LDATA[_gp.cg_id] = fnn.CreateCombo();
                m_LDATA[_gp.cg_id].MaxVisibleRowCount = 5;
                var nlist = from k in datam.DATA_ROLES.Values
                            where k.gp_type == em.role_gp_typeS.church_group &
                            k.gp_id == _gp.cg_id
                            select k;
                foreach (var n in nlist)
                {
                    m_LDATA[_gp.cg_id].Items.Add(new fnn.iGComboItemEX()
                    {
                        Text = n.role_name,
                        Value = n.role_name,
                        Tag = n
                    });
                }
            }
            m_prev_role = null;
            fGrid2.Cols["_pic"].Width = 20;
            fGrid2.Rows.Clear();
            buttonX1.Visible = false;
            buttonX2.Visible = false;
            buttonR.Visible = false;
            if (_gp == null) { return; }
            iGRow _row = null;
            _row = CreateNewRow2();
            _row.Key = "othersxxx";
             _row.ForeColor = Color.Blue;
            _row.Cells["mem"].Value = "Member Names";
            _row.Cells["mem"].TextAlign = iGContentAlignment.MiddleCenter;
            _row.BackColor = Color.Lavender;
            _row.ReadOnly = iGBool.False;
            _row.Selectable = false;
            _row.Cells["type"].Value = null;
            _row.Cells["type"].TextAlign = iGContentAlignment.MiddleRight;
            //
            _row.Cells["desig"].Value = "Member Role(s)";
            _row.Cells["desig"].TextAlign = iGContentAlignment.MiddleCenter;
           // fGrid2.Rows["othersxxx"].Cells["_pic"].TypeFlags = iGCellTypeFlags.HasEllipsisButton;
            
            buttonX1.Visible = true;
            buttonX2.Visible = true;
            buttonR.Visible = true;
            if (_gp == null) { fGrid2.EndUpdate(); return; }
            int _cnt = 0;
            ic.RoleC _role = null;
            if (_gp.MemberCount > 0)
            {
                foreach (var r in _gp.MemberEnumerable.OrderBy(l => l.mem_name))
                {
                    _cnt++;
                    _row = fGrid2.Rows.Add();
                    _row.ReadOnly = iGBool.True;
                    _row.Font = new Font("verdana", 10, FontStyle.Bold);
                    _row.Cells["type"].Col.Width = 30;
                    _row.Cells["type"].Value = _cnt;
                    _row.Cells["type"].TextAlign = iGContentAlignment.MiddleCenter;
                    _row.Cells["mem"].ValueType = typeof(string);
                    _row.Tag = r.mem_id.ToStringNullable();
                    _row.Cells["mem"].Value = r.mem_name;
                    _row.Cells["_pic"].TypeFlags = iGCellTypeFlags.HasEllipsisButton;
                    _row.Cells["_pic"].ReadOnly = iGBool.False;
                    //
                    _role = r.RolesCollection.Where(k => k.Is_valid & k.objRole.gp_type == em.role_gp_typeS.church_group & k.objRole.gp_id == _gp.cg_id ).Select(k => k.objRole).FirstOrDefault();
                    _row.Cells["desig"].DropDownControl = m_LDATA[_gp.cg_id];
                    _row.Cells["desig"].Value = null;
                    if (_role != null)
                    {
                        _row.Cells["desig"].Value = _role.role_name;
                    }
                    //
                    _row.Cells["desig"].ReadOnly = iGBool.False;
                    _row.Cells["desig"].TypeFlags = iGCellTypeFlags.HideComboButton;
                    _row.Key = r.mem_id.ToStringNullable();
                    _row.AutoHeight();
                   
                }
            }
            fGrid2.Cols.AutoWidth();
            fGrid2.AutoResizeCols = false;
            fGrid2.EndUpdate();
            position_button_controller();
           
           
        }
        #endregion
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
       
        private void fGrid2_EllipsisButtonClick(object sender, iGEllipsisButtonClickEventArgs e)
        {
          
        }
        private void fGrid_AfterAutoGroupRowCreated(object sender, iGAfterAutoGroupRowCreatedEventArgs e)
        {
            iGrid _grid = sender as iGrid;
            iGCell myGroupRowCell = _grid.RowTextCol.Cells[e.AutoGroupRowIndex];
            myGroupRowCell.Row.AutoHeight();
            ic.church_groupC _gp = _grid.Rows[e.GroupedRowIndex].Tag as ic.church_groupC;
              string _disp_str=null;
            var _cg_type = datam.DATA_CG_TYPES[_gp.cg_type_id];
            if (_gp != null)
            {
                _disp_str = _cg_type.cg_type_name;
            }
            myGroupRowCell.Value = _cg_type.is_default ? string.Format("{0} (Default)", _disp_str) : _disp_str;
            myGroupRowCell.AuxValue = _gp.cg_type_id;
            myGroupRowCell.Row.Tag = _cg_type;
            myGroupRowCell.Row.Key = string.Format("gp-{0}", _cg_type.cg_type_id);

        }
        private void tabItem1_Click(object sender, EventArgs e)
        {
            using (var _fm = new ChurchGroups.ChurchGroupMaker())
            {
                _fm.Owner = this;
                _fm.ShowDialog();
            }
        }
        private void contextMenuStrip1_Opening_1(object sender, CancelEventArgs e)
        {
            if (fGrid.Rows.Count == 0)
            {
                e.Cancel = true;
                return;
            }
            if (fGrid.SelectedRows.Count == 0)
            {
                e.Cancel = true; return;
            }
            if (fGrid.SelectedRows[0].Type==iGRowType.AutoGroupRow)
            {
                toolStripSetAsDefault.Visible = false;
                deleteGroupToolStripMenuItem.Visible = false;
                var _cg_type = fGrid.SelectedRows[0].Tag as ic.church_group_typeC;
                if(_cg_type!=null)
                {
                    toolStripSetAsDefault.Visible = !_cg_type.is_default;
                }
                return;
            }
            else
            {
                deleteGroupToolStripMenuItem.Visible = true;
            }
            //if (fGrid.SelectedRows[0].Type != iGRowType.Normal)
            //{
            //    e.Cancel = true; return;
            //}
            if (fGrid.SelectedRows[0].Cells[1].Value.ToInt16()>0)
            {
                e.Cancel = true; return;
            }
        }

        private void deleteGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string _str = "Are You Sure You Want To Delete The Selected Group";
            var _c = fGrid.CurRow.Tag as ic.church_groupC;
            if (_c != null)
            {
                using (var xd = new xing())
                {
                    if (xd.ExecuteScalarInt("select count(uno) from church_group_members_tb where cg_id=" + _c.cg_id) > 0)
                    {
                        MessageBox.Show("This Record Has References In Other Tables Which will Cause The System To Become Unstable", "Delete Failure");
                        return;
                    }
                    if (xd.ExecuteScalarInt(string.Format("select TOP 1 count(account_id) from accounts_tb where owner_type_id={0} and owner_id={1}", em.AccountOwnerTypeS.CHURCH_GROUP.ToByte(),_c.cg_id)) > 0)
                    {
                        dbm.ErrorMessage("You Cannot Delete This Church Group,It Has References In Other Tables", "Delete Failure");
                        return;
                    }
                    if (xd.ExecuteScalarInt("select count(un_id) from off_accounts_tb where cg_id=" + _c.cg_id) > 0)
                    {
                        MessageBox.Show("This Record Has References In Other Tables Which will Cause The System To Become Unstable", "Delete Failure");
                        return;
                    }
                    if (_c.sys_account_id > 0)
                    {
                        var _child_accounts = accn.GetChildAccounts(_c.sys_account_id, em.account_typeS.All).Select(d => d.account_id).ToList();
                        _child_accounts.Add(_c.sys_account_id);
                        foreach (var k in _child_accounts)
                        {
                            xd.SingleDeleteCommandExp("accounts_tb", new string[]
                             {
                                 "account_id"
                             }, new int[] { k });
                            datam.DATA_ACCOUNTS.Remove(k);
                        }
                    }
                    sdata.ClearFormCache(em.fm.expense_account_settings.ToInt16());
                    sdata.ClearFormCache(em.fm.chart_of_accounts.ToInt16());
                    sdata.ClearFormCache(em.fm.income_accounts_settings.ToInt16());
                    sdata.ClearFormCache(em.fm.creditors.ToInt16());
                    if (!dbm.WarningMessage(_str, "Delete Warning"))
                    {
                        return;
                    }
                    dbm.InsertUpdateDelete("delete from church_group_tb where cg_id=" + _c.cg_id);
                    datam.DATA_CHURCH_GROUPS.Remove(_c.cg_id);
                    fGrid.Rows.RemoveAt(fGrid.CurRow.Index);
                }
            }
        }

        private void fGrid2_AfterCommitEdit_1(object sender, iGAfterCommitEditEventArgs e)
        {
            if (string.IsNullOrEmpty(fGrid2.Rows[e.RowIndex].Key))
            {
                #region new member
                if(e.ColIndex==1)
                {
                    if (fGrid2.Rows[e.RowIndex].Cells[1].AuxValue != null)
                    {
                        var _mem = (fGrid2.Rows[e.RowIndex].Cells[1].AuxValue as fnn.iGComboItemEX).Tag as ic.memberC;
                        _mem.ChurchGroupCollection.Add(m_cgroup);
                        fGrid2.Rows[e.RowIndex].Tag = _mem.mem_id.ToStringNullable();
                        fGrid2.Rows[e.RowIndex].Key = _mem.mem_id.ToStringNullable();
                        dbh.SingleInsertCommandTSP("church_group_members_tb", new string[]
                    {
                         "mem_id",
                        "cg_id",
                        "cg_type_id",
                        "mem_status",
                        "start_date",
                        "end_date",
                        "exp_type",
                        "lch_id",
                        "lch_type_id",
                        "fs_time_stamp"
                    },
                        new object[]
                    {
                        _mem.mem_id,
                        m_cgroup.cg_id,
                        m_cgroup.cg_type_id,
                        1,
                            datam.CURR_DATE.Date,
                            null,33,
                            datam.LCH_ID,
                            datam.LCH_TYPE_ID,0//time_stamp
                     });
                        fGrid2.Rows[e.RowIndex].ReadOnly = iGBool.True;
                        fGrid2.Rows[e.RowIndex].Cells[1].DropDownControl = null;
                        fGrid2.Rows[e.RowIndex].Cells["_pic"].TypeFlags = iGCellTypeFlags.HasEllipsisButton;
                        fGrid2.Rows[e.RowIndex].Cells["_pic"].ReadOnly = iGBool.False;
                        fGrid2.Rows[e.RowIndex].Tag = _mem.mem_id;
                        fGrid2.Rows[e.RowIndex].Cells["desig"].DropDownControl = m_LDATA[m_cgroup.cg_id];
                        fGrid2.Rows[e.RowIndex].Cells["desig"].ReadOnly = iGBool.False;
                        fGrid2.Rows[e.RowIndex].Cells["desig"].TypeFlags = iGCellTypeFlags.HideComboButton;
                        count_Rows();
                        fGrid.Rows[m_cgroup.cg_id.ToString()].Cells[1].Value = m_cgroup.MemberCount;
                        buttonX1.PerformClick();
                    }
                    else
                    {
                        fGrid2.Rows[e.RowIndex].Cells[1].AuxValue = null;
                        fGrid2.Rows[e.RowIndex].Cells[1].Value = null;
                    }
                    
                }
                
                #endregion
                return;
            }
            if (e.ColIndex == 3)
            {
                if (fGrid2.Rows[e.RowIndex].Cells[3].AuxValue != null)
                {
                    var _role = (fGrid2.Rows[e.RowIndex].Cells[3].AuxValue as fnn.iGComboItemEX).Tag as ic.RoleC;
                    var _mem = datam.DATA_MEMBER[fGrid2.Rows[e.RowIndex].Key.ToInt32()];
                          if (_role != null & _mem != null)
                          {
                              using (var xd = new xing())
                              {

                                  #region previous role
                                  if (m_prev_role != null)
                                  {
                                      //delete previous role

                                      if (_mem != null)
                                      {
                                          var mem_role = (from c in _mem.RolesCollection
                                                          where c.Is_valid & c.role_id == m_prev_role.role_id
                                                          select c).FirstOrDefault();
                                          if (mem_role != null)
                                          {
                                              mem_role.Is_valid = false;

                                              if (sdata.CURR_DATE.Subtract(mem_role.start_date).Days > 30)
                                              {

                                                  xd.SingleUpdateCommandETS("roles_mem_tb", new string[]
                                    {
                                        "is_valid",
                                        "end_date",
                                        "end_fs_id",
                                        "un_id",
                                        
                                    }, new object[]
                                    {
                                        0,
                                        sdata.CURR_DATE,
                                        sdata.CURR_FS.fs_id,
                                        mem_role.un_id
                                    }, 1);

                                              }
                                              else
                                              {
                                                  xd.SingleDeleteCommandExp("roles_mem_tb", new string[] { "un_id", "lch_id" }, new int[] { mem_role.un_id, sdata.ChurchID });
                                                  _mem.RolesCollection.Remove(mem_role);
                                              }

                                          }
                                      }
                                      m_prev_role = null;
                                  }
                                  #endregion
                                  //
                                  ic.MemRoleC _mem_role = new ic.MemRoleC();
                                  _mem_role.start_date = sdata.CURR_DATE;
                                  _mem_role.start_fs_id = sdata.CURR_FS.fs_id;
                                  _mem_role.objRole = _role;
                                  _mem_role.Is_valid = true;
                                  _mem_role.role_id = _role.role_id;
                                  _mem_role.mem_id = _mem.mem_id;
                                _mem_role.un_id= xd.SingleInsertCommandInt("roles_mem_tb", new string[]
                                  {
                                      "mem_id",
                                      "role_id",
                                      "start_date",
                                      "end_date",
                                      "start_fs_id",
                                      "end_fs_id",
                                      "is_valid",
                                      "exp_type",
                                      "lch_id",
                                   }, new object[]
                                   {
                                       _mem_role.mem_id,
                                       _mem_role.role_id,
                                       _mem_role.start_date,
                                       null,//end date
                                       _mem_role.start_fs_id,
                                       _mem_role.end_fs_id,
                                       1,
                                       33,sdata.ChurchID
                                        });
                                xd.CommitTransaction();
                                _mem.RolesCollection.Add(_mem_role);
                                _mem_role = null;
                              }
                          }
                          return;
                }
                if (fGrid2.Rows[e.RowIndex].Cells[3].AuxValue == null)
                {
                    using (var xd = new xing())
                    {
                        var _mem = datam.DATA_MEMBER[fGrid2.Rows[e.RowIndex].Key.ToInt32()];
                        #region previous role
                        if (m_prev_role != null & _mem!=null)
                        {
                            var mem_role = (from c in _mem.RolesCollection
                                                where c.Is_valid & c.role_id == m_prev_role.role_id
                                                select c).FirstOrDefault();
                                if (mem_role != null)
                                {
                                    mem_role.Is_valid = false;

                                    if (sdata.CURR_DATE.Subtract(mem_role.start_date).Days > 30)
                                    {

                                        xd.SingleUpdateCommandETS("roles_mem_tb", new string[]
                                    {
                                        "is_valid",
                                        "end_date",
                                        "end_fs_id",
                                        "un_id",
                                        
                                    }, new object[]
                                    {
                                        0,
                                        sdata.CURR_DATE,
                                        sdata.CURR_FS.fs_id,
                                        mem_role.un_id
                                    }, 1);

                                    }
                                    else
                                    {
                                        xd.SingleDeleteCommandExp("roles_mem_tb", new string[] { "un_id", "lch_id" }, new int[] { mem_role.un_id, sdata.ChurchID });
                                        _mem.RolesCollection.Remove(mem_role);
                                    }
                                    xd.CommitTransaction();
                                }
                            
                            m_prev_role = null;
                        }
                        #endregion
                    }
                    fGrid2.Rows[e.RowIndex].Cells[3].Value = null;
                }
            }
           
        }

        private void fGrid2_EllipsisButtonClick_1(object sender, iGEllipsisButtonClickEventArgs e)
        {
            
            fGrid2.SetCurCell(e.RowIndex, 0);
            if (fGrid2.Rows[e.RowIndex].Tag == null) { return; }
            using (var _fm = new MemberDetail())
            {
                _fm.Tag = datam.DATA_MEMBER[fGrid2.Rows[e.RowIndex].Tag.ToInt32()];
                _fm.ShowDialog();
            }
        }

        private void fGrid_AfterContentsGrouped(object sender, EventArgs e)
        {

        }

        private void fGrid_AfterRowStateChanged(object sender, iGAfterRowStateChangedEventArgs e)
        {
            if (e.Expanded)
            {

                fGrid.Cols.AutoWidth();
                fGrid.AutoResizeCols = false;
                fGrid.EndUpdate();
            }
           
        }
        private void position_button_controller()
        {
            if (fGrid2.Rows.Count > 0)
            {
                iGCell _cell = fGrid2.Rows["othersxxx"].Cells["type"];
                if (_cell != null)
                {
                    buttonX1.Left = fGrid2.Left + _cell.Bounds.Left;
                    buttonX1.Top = fGrid2.Top + _cell.Bounds.Top + 1;
                    buttonX1.Width = _cell.Bounds.Width;

                }
            }
            position_button_controller1();
        }
        private void fGrid2_SizeChanged(object sender, EventArgs e)
        {
            position_button_controller();
        }
         private void buttonX1_Click(object sender, EventArgs e)
        {
            var _index = fGrid2.Rows["othersxxx"].Index;
            var _col_width = fGrid2.Cols[0].Width;
            if (fGrid2.Rows.Cast<iGRow>().Where(i => i.Index > _index).Count(j => j.Cells[1].Value == null) > 0)
            {
                fGrid2.Focus();
                fGrid2.SetCurCell(fGrid2.Rows.Count - 1, 1);
                SendKeys.Send("{ENTER}");
                
                return;
            }
            fGrid2.BeginUpdate();
            iGRow _row = CreateNewRow2();
            var icombo = fnn.CreateCombo();
            var nlist = from t in datam.DATA_MEMBER.Values
                        where t.IsCurrentMember &
                        (t.ChurchGroupCollection.Count(k => k.cg_type_id == m_cgroup.cg_type_id) == 0)
                        select t;
            foreach (var m in nlist)
            {
                icombo.Items.Add(new fnn.iGComboItemEX()
                {
                    Tag = m,
                    Text = m.mem_name,
                    Value = m.mem_name
                });
            }
            _row.Cells[1].DropDownControl = icombo;
            fGrid2.Focus();
            fGrid2.SetCurCell(fGrid2.Rows.Count - 1, 1);
            SendKeys.Send("{ENTER}");
            fGrid2.Cols[0].Width = _col_width;
            fGrid2.EndUpdate();
          
        }

        private void fGrid_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {
            ts_delete.Visible = false;
            if (fGrid2.CurRow == null) { return; }
            if (fGrid2.CurRow.Index > fGrid2.Rows["othersxxx"].Index & !string.IsNullOrEmpty(fGrid2.CurRow.Key) & fGrid2.CurRow.Cells[1].Value != null)
            {
                ts_delete.Visible = true;
            }
        }

        private void ts_delete_Click(object sender, EventArgs e)
        {
            string _str = "Are You Sure You Want To Remove The Selected Member ??";
            if (!dbm.WarningMessage(_str, "Remove Warning"))
            {
                return;
            }
            int mem_id = fGrid2.SelectedRows[0].Key.ToInt32();
            var _mem = datam.DATA_MEMBER[mem_id];
            _mem.ChurchGroupCollection.Remove(m_cgroup);
             using (var xd = new xing())
            {
                xd.SingleDeleteCommandExp("church_group_members_tb", new string[]
                {
                    "mem_id",
                    "cg_id"
                }, new int[]
                {
                   _mem.mem_id,
                    m_cgroup.cg_id
                });
                
                xd.CommitTransaction();
            }
            iGRow _row = fGrid2.SelectedRows[0];
            fGrid2.Rows.RemoveAt(_row.Index);
            fGrid.Rows[m_cgroup.cg_id.ToString()].Cells[1].Value = m_cgroup.MemberCount;
            //
            var _index = fGrid2.Rows["othersxxx"].Index;
            if (fGrid2.Rows.Cast<iGRow>().Where(i => i.Index > _index).Count(j => j.Cells[1].Value == null) > 0)
            {
                fGrid2.Rows.RemoveAt(fGrid2.Rows.Count - 1);
            }
            count_Rows();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (m_cgroup != null)
            {
                using (var _fm = new GroupDataPicDisplay())
                {
                    _fm.Tag = new ic.GroupDataPicDisplay()
                    {
                        display_title = string.Format("{0} Members", m_cgroup.cg_name),
                        Data = m_cgroup == null ? null : m_cgroup.MemberEnumerable
                    };
                    _fm.ShowDialog();
                }
                return;
            }
        }

        private void buttonR_Click(object sender, EventArgs e)
        {
            if (m_cgroup != null)
            {
                using (var _fm = new DesignationMaker())
                {
                    _fm.Tag = new int[] { em.role_gp_typeS.church_group.ToByte(), m_cgroup.cg_id };
                    _fm.ShowDialog();
                }
                UpdateRoles(m_cgroup);
            }
        }

        private void fGrid2_BeforeCommitEdit(object sender, iGBeforeCommitEditEventArgs e)
        {
            if (e.ColIndex == 3)
            {
                if (fGrid2.Rows[e.RowIndex].Cells[e.ColIndex].AuxValue == null)
                {
                    m_prev_role = null;
                    return;
                }
                m_prev_role = (fGrid2.Rows[e.RowIndex].Cells[e.ColIndex].AuxValue as fnn.iGComboItemEX).Tag as ic.RoleC;
            }
        }

        private void toolStripSetAsDefault_Click(object sender, EventArgs e)
        {
            var _row = fGrid.SelectedRows[0];
            var _cg_type = _row.Tag as ic.church_group_typeC;
            if (_cg_type != null)
            {
                using (var _xd = new xing())
                {
                    var _prev_default_type = datam.DATA_CG_TYPES.Values.FirstOrDefault(j => j.is_default);
                    if (_prev_default_type != null)
                    {
                        _prev_default_type.is_default = false;
                        //
                        _xd.SingleUpdateCommandALL("church_group_types_tb", new string[]
                   {
                        "is_default",
                        "cg_type_id"
                   }, new object[]
                   {
                        _prev_default_type.is_default.ToBooleanByte(),
                        _prev_default_type.cg_type_id
                   }, 1);

                        datam.DATA_CG_TYPES[_prev_default_type.cg_type_id].is_default = false;
                        fGrid.Rows[string.Format("gp-{0}",_prev_default_type.cg_type_id)].RowTextCell.Value = _prev_default_type.cg_type_name;

                    }
                    _cg_type.is_default = true;
                    _xd.SingleUpdateCommandALL("church_group_types_tb", new string[]
                    {
                        "is_default",
                        "cg_type_id"
                    }, new object[]
                    {
                        _cg_type.is_default.ToBooleanByte(),
                        _cg_type.cg_type_id
                    }, 1);
                    //
                    datam.DATA_CG_TYPES[_cg_type.cg_type_id].is_default = true;
                    _row.RowTextCell.Value = string.Format("{0} (Default)", _cg_type.cg_type_name);
                    _xd.CommitTransaction();
                }

                sdata.ClearFormCache(em.fm.membershi.ToInt16());
                sdata.ClearFormCache(em.fm.view_church_members.ToInt16());
            }
        }
    }
}
