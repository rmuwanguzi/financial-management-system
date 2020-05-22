using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SdaHelperManager;
using DevComponents.DotNetBar.Controls;
using TenTec.Windows.iGridLib;
using System.Reflection;
using SdaHelperManager.Security;

namespace MTOMS
{
    public partial class MemberManager : DevComponents.DotNetBar.Office2007Form
    {
        public MemberManager()
        {
            InitializeComponent();
        }
       
        MemberMaker2 mem_maker = null;
        Image empty_picture = null;
        bool app_working = false;
        List<int> OtherGroups = new List<int>();
        int pic_hlp = -5;
        iGrid m_grid = null;
        Dictionary<string, bool> m_ExpandedGroup = new Dictionary<string, bool>();
        int prev_col_width = 2;
        ic.church_group_typeC m_default_cg_type { get; set; }
        iGDropDownList m_defaultChurchGroupsDropDown { get; set; }
        private void InitializeGridColumns()
        {
            #region Columns To Display
            string[] _cols = new string[] { "Code", "Member Name","G","Group Name" };
            iGCol myCol;
            var _grid = new iGrid[] { fGrid, dataaging, dataapostacy, datadeceased, datatransfered };
            foreach (var g in _grid)
            {
                foreach (var c in _cols)
                {
                    myCol = g.Cols.Add(c, c);
                    myCol.IncludeInSelect = true;
                    myCol.SortType = iGSortType.ByValue;
                    //  myCol.ColHdrStyle.BackColor = Color.AntiqueWhite;
                    myCol.ColHdrStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    myCol.ColHdrStyle.ForeColor = Color.DarkGreen;
                    myCol.ColHdrStyle.Font = new Font("arial", 10, FontStyle.Bold);
                    myCol.Width = 70;
                    myCol.AllowSizing = true;
                }
                g.Cols["Code"].SortType = iGSortType.ByAuxValue;
                g.Cols["Member Name"].SortType = iGSortType.ByAuxValue;
                myCol = g.Cols.Add("picture", string.Empty);
                myCol.Width = 102;
                myCol.AllowSizing = false;
                myCol = g.Cols.Add("colSearch", "colSearch");
                myCol.Width = 1;
                myCol.Visible = false;
                myCol = g.Cols.Add("mem_id", "mem_id");
                myCol.Width = 1;
                myCol.Visible = false;
                //
                if (g == fGrid)
                {
                    g.Cols["Group Name"].CellStyle.DropDownControl = m_defaultChurchGroupsDropDown;
                    // g.Cols["Group Name"].SortType = iGSortType.None;
                    if(m_default_cg_type!=null)
                    {
                        g.Cols["Group Name"].Text = m_default_cg_type.cg_type_name;
                    }
                }
                g.SearchAsType.Mode = iGSearchAsTypeMode.Filter;
                g.SearchAsType.MatchRule = iGMatchRule.Contains;
                g.SearchAsType.SearchCol = g.Cols["colSearch"];

                g.SearchAsType.AutoCancel = false;
                // g.SearchAsType.FilterKeepCurRow = true;
                // The following code lines force the grid
                // not to show the search window.
                g.SearchAsType.DisplayKeyboardHint = false;
                g.SearchAsType.DisplaySearchText = false;
                //
                g.DefaultRow.Height = g.GetPreferredRowHeight(true, false);
                g.DefaultAutoGroupRow.Height = g.DefaultRow.Height;
                g.Cols.AutoWidth();
            }
            #endregion
        }
        private void DisplayTotal()
        {
            paneltotal.Text = string.Empty;
            int cnt = 0;
            if (datam.DATA_MEMBER.Values.Count > 0)
            {
                if (tabControl1.SelectedTab == tabmen)
                {
                    cnt = (from j in datam.DATA_MEMBER.Values
                           where j.IsCurrentMember
                           select j.mem_id).Count();
                    paneltotal.Text = string.Format("Current Church Members = {0}", cnt);
                }
                if (tabControl1.SelectedTab == tabageing)
                {
                    cnt = (from j in datam.DATA_MEMBER.Values
                           where j.mem_status==em.xmem_status.Ageing
                           select j.mem_id).Count();
                    paneltotal.Text = string.Format("Ageing Church Members = {0}", cnt);
                }
                if (tabControl1.SelectedTab == tabApostacy)
                {
                    cnt = (from j in datam.DATA_MEMBER.Values
                           where j.mem_status==em.xmem_status.Apostacy
                           select j.mem_id).Count();
                    paneltotal.Text = string.Format("Apostacy Members = {0}", cnt);
                }
                if (tabControl1.SelectedTab == tabtransfer)
                {
                    cnt = (from j in datam.DATA_MEMBER.Values
                           where j.mem_status==em.xmem_status.Transfered
                           select j.mem_id).Count();
                    paneltotal.Text = string.Format("Transferred Church Members = {0}", cnt);
                }
                if (tabControl1.SelectedTab == tabdeceased)
                {
                    cnt = (from j in datam.DATA_MEMBER.Values
                           where j.mem_status==em.xmem_status.Deceased
                           select j.mem_id).Count();
                    paneltotal.Text = string.Format("Deceased Church Members = {0}", cnt);
                }
            }
        }
        void InitIgridColumns()
        {
            #region Adjust the grid's appearance

            // Set the flat appearance for the cells.
            fGridD.Appearance = iGControlPaintAppearance.StyleFlat;
            fGridD.UseXPStyles = false;
            // Set the flat appearance for the scroll bars.
            fGridD.ScrollBarSettings.Appearance = iGControlPaintAppearance.StyleFlat;
            // Hide the focus rectangle.
            fGridD.FocusRect = false;

            #endregion
            iGCol myCol;

            myCol = fGridD.Cols.Add("Name", "Field Name");
            myCol.CellStyle.ReadOnly = iGBool.True;
            myCol.SortType = iGSortType.None;
            myCol = fGridD.Cols.Add("Value", "Description");
            myCol.ColHdrStyle.TextFormatFlags = iGStringFormatFlags.FitBlackBox;
           // myCol.CellStyle.SingleClickEdit = iGBool.False;
            myCol.SortType = iGSortType.None;
           

            // Add a special column which will store the category name.
            // This column will be used for grouping.
            fGridD.Cols.Add("Category", string.Empty).Visible = false;
            fGridD.Cols.Add("mem_id", string.Empty).Visible = false;
            // Add a special column which will store the default 
            // values for the properties.
            fGridD.Cols.Add("SValue", string.Empty).Visible = false;
            myCol = fGridD.Cols["SValue"];
            myCol.SortType = iGSortType.ByValue;
            fGridD.DefaultRow.Height = fGridD.GetPreferredRowHeight(true, false);
            fGridD.DefaultAutoGroupRow.Height = fGridD.DefaultRow.Height;
        }
        private void ShowHideBirthInfo()
        {
            if (fGridD.Rows.Count > 0)
            {
                if (showageinfo.Checked)
                {
                    fGridD.Rows["Age"].Visible = true;
                   // fGridD.Rows["Age Group"].Visible = true;
                    fGridD.Rows["BirthDate"].Visible = true;
                    fGridD.Rows["Birth Year"].Visible = true;
                }
                else
                {
                    fGridD.Rows["Age"].Visible = false;
                   // fGridD.Rows["Age Group"].Visible = false;
                    fGridD.Rows["BirthDate"].Visible = false;
                    fGridD.Rows["Birth Year"].Visible = false;
                }
            }
        }
        private void FillIGridDetails(ic.memberC _m)
        {
            if (_m == null) { fGridD.Rows.Clear(); return; }
            //picture date
            if (_m.objPicture != null)
            {
                if (_m.objPicture.SmallPicture != null)
                {
                    if (_m.mem_title_id == 0)
                    {
                        pictureBox1.Image = fn.LabelPicture(_m.objPicture.SmallPicture, xso.xso.DATA_COMMON[_m.mem_title_id].item_name, _m.mem_name);
                    }
                    else
                    {
                        pictureBox1.Image = fn.LabelPicture(_m.objPicture.SmallPicture, string.Empty, _m.mem_name);
                    }
                }
            }
            iGRow _row = null;
            string group_name = null;
            fGridD.BeginUpdate();
            fGridD.Rows.Clear();
            //personal details
            group_name = "personal details";
            //if (!_m.IsCurrentMember)
            //{
                if (_m.objPicture != null)
                {
                    _row = AddProperty("Member Picture", string.Empty, group_name);
                    _row.Cells[0].TextAlign = iGContentAlignment.MiddleLeft;
                    _row.Cells["Value"].CustomDrawFlags |= iGCustomDrawFlags.Foreground;
                    _row.Cells["Value"].Value = _m.objPicture.SmallPicture;
                    _row.Cells["Value"].ValueType = typeof(Image);
                    _row.Cells["Value"].AuxValue = _m.objPicture.Small_PicBytes;
                    _row.Height = _m.objPicture.SmallPicture.Height;
                    //  _row.AutoHeight();
                }
            //}
                if (_m.mem_title_id > 0)
                {
                    AddProperty("Member Name", string.Format("{0} {1}", xso.xso.DATA_COMMON[_m.mem_title_id].item_name, _m.mem_name), group_name);
                }
                else
                {
                    AddProperty("Member Name", string.Format("{0}", _m.mem_name), group_name);
                }
            AddProperty("Other Names", _m.mem_o_name, group_name);
            AddProperty("Member-Code", _m.mem_code, group_name);
           // AddProperty("U-Code", _m.mem_u_code, group_name);
            if (!_m.IsCurrentMember)
            {
                
                switch (_m.mem_status)
                {
                    case em.xmem_status.Ageing:
                        {
                            _row = AddProperty("Status", "AGEING", group_name);
                            _row.Cells["Value"].ForeColor = Color.DarkBlue;
                            break;
                        }
                    case em.xmem_status.Transfered:
                        {
                            _row = AddProperty("Status", "TRANSFERED", group_name);
                            _row.Cells["Value"].ForeColor = Color.Green;
                            break;
                        }
                    case em.xmem_status.Apostacy:
                        {
                            _row = AddProperty("Status", "APOSTACY", group_name);
                            _row.Cells["Value"].ForeColor = Color.Maroon;
                            break;
                        }
                    case em.xmem_status.Deceased:
                        {
                            _row = AddProperty("Status", "DECEASED", group_name);
                            _row.Cells["Value"].ForeColor = Color.Red;
                            break;
                        }
                }
            }
            
            AddProperty("Gender", xso.xso.DATA_COMMON[_m.gender_id].item_name, group_name);
            if (_m.age_gp_id > 0)
            {
                AddProperty("Age Group", xso.xso.DATA_AGEGROUP[_m.age_gp_id].age_gp_name, group_name);
            }
            AddProperty("Age", _m.age.ToString(), group_name);
            AddProperty("BirthDate", _m.mem_birth_date==null ? null : _m.mem_birth_date.ToMyLongDate(), group_name);
            if (_m.mem_birth_date != null)
            {
                AddProperty("Birth Year", _m.birth_yr.ToString(), group_name);
            }
            else
            {
                AddProperty("Birth Year", null, group_name).Visible=false;
            }
            switch (_m.mem_type)
            {
                case em.MembershipType.none:
                    {
                        _row = AddProperty("MemberShip Type", "NONE", group_name);
                        break;
                    }
                case em.MembershipType.DualMemberShip:
                    {
                        _row = AddProperty("MemberShip Type", "Dual MemberShip", group_name);
                        break;
                    }
                case em.MembershipType.RegisteredChurchMember:
                    {
                        _row = AddProperty("MemberShip Type", "Registered Church Member", group_name);
                        break;
                    }
                case em.MembershipType.SabbathSchool:
                    {
                        _row = AddProperty("MemberShip Type", "Sabbath School", group_name);
                        break;
                    }
                case em.MembershipType.Temporary:
                    {
                        _row = AddProperty("MemberShip Type", "Temporary", group_name);
                        break;
                    }
            }
            if (_m.country_id > 0)
            {
                AddProperty("Nationality", xso.xso.DATA_COUNTRY[_m.country_id].country_name, group_name);
            }
            if (_m.baptismal_type_id > 0)
            {
                AddProperty("Is Baptised", xso.xso.DATA_COMMON[_m.baptismal_type_id].item_name, group_name);
            }
            if (_m.marital_type_id > 0)
            {
                AddProperty("Marital Status", xso.xso.DATA_COMMON[_m.marital_type_id].item_name, group_name);
            }
            AddProperty("No Of Children", _m.objChildrenCol == null ? "0" : _m.objChildrenCol.Count().ToString(), group_name);
            AddProperty("Phone1", _m.objAddress == null ? null : _m.objAddress.phone1, group_name);
            AddProperty("Phone2", _m.objAddress == null ? null : _m.objAddress.phone2, group_name);
            AddProperty("Email", _m.objAddress == null ? null : _m.objAddress.email, group_name);
            
            AddProperty("Previous Church", _m.prev_church, group_name);
            AddProperty("Previous Religion", _m.prev_religion, group_name);
            AddProperty("Education Level", xso.xso.DATA_COMMON[_m.educ_level_id].item_name, group_name);
            AddProperty("Employment Status", fnn.GetEmploymentStatus(_m.employment_status), group_name);

            if (_m.ChurchGroupCollection.Count > 0)
            {
                group_name = "church groups";
                foreach (var g in _m.ChurchGroupCollection.OrderBy(k => k.cg_type_id))
                {
                    AddProperty(datam.DATA_CG_TYPES[g.cg_type_id].cg_type_name, g.cg_name, group_name);
                }
            }
           
            if (_m.objOccupation != null)
            {
                group_name = "occupation details";
                if (_m.objOccupation.occ_type_id > 0)
                {
                    AddProperty("Occupation", xso.xso.DATA_OCCUPATION[_m.objOccupation.occ_type_id].item_name, group_name);
                }
                else
                {
                    AddProperty("Occupation", _m.objOccupation.occupation_other, group_name);
                }
                AddProperty("Work Place", _m.objOccupation.employer, group_name);
                AddProperty("Other Skills", _m.objOccupation.other_skills, group_name);
            }
            
            if (_m.objSpouse != null)
            {
                group_name = "spouse & marital details";
                AddProperty("Spouse Name", _m.objSpouse.spouse_name, group_name);
                AddProperty("Spouse Phone", _m.objSpouse.phone_no, group_name);
                AddProperty("Marriage Year", _m.objSpouse.marriage_year.ToString(), group_name);
                AddProperty("Marriage Date", _m.objSpouse.marriage_date == null ? null : _m.objSpouse.marriage_date.ToMyLongDate(),group_name);
                if (_m.objSpouse.spouse_id > 0)
                {
                    _row = fGridD.Rows["Spouse Name"];
                    _row.Cells["mem_id"].Value = _m.objSpouse.spouse_id;
                    _row.Cells["Value"].TypeFlags = iGCellTypeFlags.NoTextEdit | iGCellTypeFlags.HasEllipsisButton;
                    _row.Cells["Value"].ReadOnly = iGBool.False;
                    _row.Cells["Value"].SingleClickEdit = iGBool.False;
                    var _sp = datam.DATA_MEMBER[_m.objSpouse.spouse_id];
                    if (_sp != null && _sp.objPicture != null)
                    {
                        _row = AddProperty("Spouse Picture", string.Empty, group_name);
                        _row.Cells[0].TextAlign = iGContentAlignment.MiddleLeft;
                        _row.Cells["Value"].CustomDrawFlags |= iGCustomDrawFlags.Foreground;
                        var n_img = _sp.objPicture.SmallPicture;
                        var _img = fn.IMAGERESIZER(_sp.objPicture.SmallPicture, (n_img.Width *.75).ToFloat(), (n_img.Height * .75).ToFloat());
                        _row.Cells["Value"].Value = _img;
                        _row.Cells["Value"].ValueType = typeof(Image);
                        _row.Cells["Value"].AuxValue = _img.ToImageBytes();
                        _row.Height = (n_img.Height * .75).ToInt32();
                          }

                }
            }
            if (_m.objBaptism != null)
            {
                group_name = "baptism details";
                AddProperty("Baptism Year", _m.objBaptism.bapt_yr.ToString(), group_name);
                AddProperty("Baptism Date", _m.objBaptism.bapt_date == null ? null : _m.objBaptism.bapt_date.ToMyShortDate(), group_name);
                AddProperty("Baptism Place", _m.objBaptism.bapt_place, group_name);
                AddProperty("Pastor Name", _m.objBaptism.bapt_pastor, group_name);
                AddProperty("Country", _m.objBaptism.bapt_country, group_name);
                AddProperty("Church Name", _m.objBaptism.bapt_church, group_name);
            }
            if (_m.objChildrenCol != null)
            {
                byte j = 1;
                foreach (var n in _m.objChildrenCol)
                {
                    _row = AddProperty(j.ToString(), string.Format("{0}     [ {1} ]", n.child_name, n.birth_year), "Children");
                   _row.Cells[0].TextAlign = iGContentAlignment.TopRight;
                   if (n.child_mem_id > 0)
                   {
                       _row.Cells["mem_id"].Value = n.child_mem_id;
                       _row.Cells["Value"].TypeFlags = iGCellTypeFlags.NoTextEdit | iGCellTypeFlags.HasEllipsisButton;
                       _row.Cells["Value"].ReadOnly = iGBool.False;
                   }
                    j++;
                }
            }
            if (_m.objAddress != null)
            {
                AddProperty("Phy Address", _m.objAddress.phy_address, "address info");
                AddProperty("Postal Address", _m.objAddress.postal_address, "address info");
                AddProperty("Division/SubCounty", _m.objAddress.division, "address info");
                AddProperty("Village", _m.objAddress.village, "address info");

            }
            if (_m.DepartmentCollection.Count() > 0)
            {
                group_name = "departments";
                int _cnt = 1;
                foreach (var r in _m.DepartmentCollection)
                {
                    _row = AddProperty(_cnt.ToString(), string.Format("{0}", r.dept_name), group_name);
                    _row.Cells[0].TextAlign = iGContentAlignment.TopRight;
                    _cnt++;
                    fGridD.Rows[fGridD.Rows.Count - 1].Cells["Value"].Value = r.dept_name;
                }
            }
            if (_m.RolesCollection!=null && _m.RolesCollection.Count() > 0)
            {
                group_name = "church roles";
                int _cnt = 1;
                var role_list = from k in _m.RolesCollection
                                where k.Is_valid
                                orderby k.objRole.gp_type.ToByte(), k.objRole.gp_id
                                select k;
                foreach (var r in role_list)
                {
                    if(r.objRole.gp_type==em.role_gp_typeS.church_group)
                    {
                        _row = AddProperty(datam.DATA_CHURCH_GROUPS[r.objRole.gp_id].cg_name, string.Format("{0}", datam.DATA_ROLES[r.role_id].role_name), group_name);
                        _row.Cells[0].TextAlign = iGContentAlignment.TopRight;
                    }
                    else
                    {
                        _row = AddProperty(datam.DATA_DEPARTMENT[r.objRole.gp_id].dept_name + " Dept", string.Format("{0}", datam.DATA_ROLES[r.role_id].role_name), group_name);
                          _row.Cells[0].TextAlign = iGContentAlignment.TopRight;
                    }
                    
                    
                }
            }
            if (_m.objNok != null)
            {
                AddProperty("Name", _m.objNok.nok_name, "Next Of Kin");
                AddProperty("Phone", _m.objNok.nok_phone, "Next Of Kin");
                AddProperty("Email", _m.objNok.nok_email, "Next Of Kin");
                AddProperty("Phy Address", _m.objNok.nok_phy_address, "Next Of Kin");
            }
            if (_m.objNeighbourCol != null)
            {
                int _cnt = 1;
                foreach (var n in _m.objNeighbourCol)
                {
                    AddProperty(_cnt.ToString(), string.Format("{0}    [ {1} ]", n.neighbour_name, n.neigh_phone), "Neighbour");
                    _cnt++;
                }
            }
            if(_m.objParent!=null)
            {
                _row = AddProperty("Father", _m.objParent.father_name, "Parents");
                if (_m.objParent.father_mem_id > 0)
                {
                    _row.Cells["mem_id"].Value = _m.objParent.father_mem_id;
                    _row.Cells["Value"].TypeFlags = iGCellTypeFlags.NoTextEdit | iGCellTypeFlags.HasEllipsisButton;
                    _row.Cells["Value"].ReadOnly = iGBool.False;
                    _row.Cells["Value"].ImageAlign = iGContentAlignment.MiddleLeft;
                }
                _row = AddProperty("Mother", _m.objParent.mother_name, "Parents");
                if (_m.objParent.mother_mem_id > 0)
                {
                    _row.Cells["mem_id"].Value = _m.objParent.mother_mem_id;
                    _row.Cells["Value"].TypeFlags = iGCellTypeFlags.NoTextEdit | iGCellTypeFlags.HasEllipsisButton;
                    _row.Cells["Value"].ReadOnly = iGBool.False;
                }
                _row = AddProperty("Guardian", _m.objParent.guardian_name, "Parents");
                if (_m.objParent.guardian_mem_id> 0)
                {
                    _row.Cells["mem_id"].Value = _m.objParent.guardian_mem_id;
                    _row.Cells["Value"].TypeFlags = iGCellTypeFlags.NoTextEdit | iGCellTypeFlags.HasEllipsisButton;
                    _row.Cells["Value"].ReadOnly = iGBool.False;
                }
            }
            if (!_m.IsCurrentMember)
            {

                switch (_m.mem_status)
                {
                     case em.xmem_status.Transfered:
                        {
                            if(_m.objTransfer!=null)
                            {
                            group_name = "transfer info";
                            _row = AddProperty("Transfer Date", _m.objTransfer.transfer_date.ToMyShortDate(), group_name);
                            _row = AddProperty("New Church", _m.objTransfer.church_name, group_name);
                            _row = AddProperty("Reason", _m.objTransfer.reason, group_name);
                            _row = AddProperty("Comment", _m.objTransfer.comment, group_name);
                            }
                            break;
                        }
                    case em.xmem_status.Apostacy:
                        {
                            if (_m.objApostacy != null)
                            {
                                group_name = "apostacy info";
                                _row = AddProperty("Cancel Date", _m.objApostacy.apostacy_date.ToMyShortDate(), group_name);
                                _row = AddProperty("Reason", _m.objApostacy.reason, group_name);
                                _row = AddProperty("Comment", _m.objApostacy.comment, group_name);
                            }
                            break;
                        }
                    case em.xmem_status.Deceased:
                        {
                            if (_m.objDeath != null)
                            {
                                group_name = "deceased info";
                                _row = AddProperty("Died On", _m.objDeath.death_date.ToMyShortDate(), group_name);
                                _row = AddProperty("Cause Of Death", _m.objDeath.reason, group_name);
                                _row = AddProperty("Comment", _m.objDeath.comment, group_name);
                            }
                            break;
                        }
                }
            }
            SortAndGroup();
            fGridD.Cols.AutoWidth();
            fGridD.AutoResizeCols = false;
            if (fGridD.Cols[1].Width > prev_col_width )
            {
                prev_col_width = fGridD.Cols[1].Width;
            }
            fGridD.Cols[1].Width = prev_col_width;
            ShowHideBirthInfo();
            fGridD.EndUpdate();
            if (fGridD.HScrollBar.Visible)
            {
                prev_col_width = -5;
            }
           
        }
        void ResizeGridDetail()
        {
            fGridD.Cols.AutoWidth();
            fGridD.AutoResizeCols = false;
            if (fGridD.Cols[1].Width > prev_col_width)
            {
                prev_col_width = fGridD.Cols[1].Width;
            }
            fGridD.Cols[1].Width = prev_col_width;

        }
        void fGridD_CustomDrawCellForeground(object sender, iGCustomDrawCellEventArgs e)
        {

            Image cell_img = (Image)fGridD.Cells[e.RowIndex, e.ColIndex].Value;
            if (cell_img == null) { return; }//caused error the other time
            Region myOldClipRegion = e.Graphics.Clip;
            e.Graphics.SetClip(new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            try
            {
                e.Graphics.DrawImage(cell_img, new Rectangle(e.Bounds.X, e.Bounds.Y + (e.Bounds.Height - cell_img.Height) / 2, cell_img.Width, cell_img.Height));
            }
            finally
            {
                e.Graphics.Clip = myOldClipRegion;
            }

        }
        private iGRow AddProperty(string name, string value, string categoryName)
        {
            // Add a new row.
            iGRow myRow = fGridD.Rows.Add();
            myRow.Font = new Font("verdana", 11, FontStyle.Regular);
            // Fill the name and category fields.
            myRow.Cells["Name"].Value = name.ToProperCase();
            myRow.Cells["Category"].Value = categoryName;
            myRow.Cells["Value"].Value = value;
            myRow.Cells[1].ForeColor = Color.Black;
            myRow.Cells[0].ForeColor = Color.DarkGreen;
            myRow.Key = name;
            myRow.AutoHeight();
            // This method is invoked to determine which properties are in 
            // their default statate, and to highlight non-default properties.
            switch (categoryName.ToLower())
            {
                case "personal details":
                    {
                        myRow.Cells["SValue"].Value = 1;
                        break;
                    }
                case "church groups":
                    {
                        myRow.Cells["SValue"].Value = 2;
                        break;
                    }
                case "transfer info":
                    {

                        myRow.Cells["SValue"].Value = 3;
                        break;
                    }
                case "apostacy info":
                    {

                        myRow.Cells["SValue"].Value = 4;
                        break;
                    }
                case "deceased info":
                    {

                        myRow.Cells["SValue"].Value = 5;
                        break;
                    }
                case "baptism details":
                    {
                        myRow.Cells["SValue"].Value = 6;
                        break;
                    }
                case "spouse & marital details":
                    {
                        myRow.Cells["SValue"].Value = 7;
                        break;
                    }
                case "departments":
                    {
                        myRow.Cells["SValue"].Value = 8;
                        break;
                    }
                case "church roles":
                    {
                        myRow.Cells["SValue"].Value = 9;
                        break;
                    }
                case "occupation details":
                    {
                        myRow.Cells["SValue"].Value = 10;
                        break;
                    }
                case "address info":
                    {
                        myRow.Cells["SValue"].Value = 11;
                        break;
                    }
                      
                case "children":
                        {
                            myRow.Cells["SValue"].Value = 12;
                            break;
                        }
                
                case "next of kin":
                        {
                            myRow.Cells["SValue"].Value = 13;
                            break;
                        }
                case "parents":
                        {
                            myRow.Cells["SValue"].Value = 14;
                            break;
                        }
                case "neighbour":
                        {
                            myRow.Cells["SValue"].Value = 15;
                            break;
                        }
               

            }

            return myRow;
        }
        private void UpdateMember(ic.memberC n)
        {
            if (n == null) { return; }
            iGrid _grid = null;
            iGRow _row = null;
            if (n.IsCurrentMember)
            {
                _grid = fGrid;
                try
                {
                    _row = _grid.Rows[n.mem_code];
                }
                catch (Exception ex )
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            if (_row == null) { return; }
            _row.Cells["Code"].Value = n.mem_code;
            _row.Cells["Member Name"].Value = string.Format("{0} {1}", xso.xso.DATA_COMMON[n.mem_title_id].item_name, n.mem_name).ToProperCase();
            _row.Cells["G"].Value = string.Format("{0}", xso.xso.DATA_COMMON[n.gender_id].item_name);
           
            _row.Cells["colSearch"].Value = string.Format("{0} {1}", n.mem_code, n.mem_name).ToLower();
            _row.Height += 2;
            _row.Key = n.mem_code;
            _row.Tag = n;
            _grid.EndUpdate();
            _grid.Cols.AutoWidth();
            _grid.AutoResizeCols = false;
            clear_left_panel();
            _grid.CurCell = null;
            _grid.SetCurCell(n.mem_code, 0);
            
          
        }
        private void NewMember(ic.memberC n)
        {
                if (n == null) { return; }
                iGrid _grid = null;
                iGRow _row = null;
                ic.church_groupC _gp;
                if (n.IsCurrentMember)
                {
                    _grid = fGrid;
                    _row = _grid.Rows.Add();
                }
                else
                {
                    switch (n.mem_status)
                    {
                        case em.xmem_status.Ageing:
                            {
                                _grid = dataaging;
                                _row = _grid.Rows.Add();
                                _row.ForeColor = Color.DarkBlue;
                                break;
                            }
                        case em.xmem_status.Apostacy:
                            {
                                _grid = dataapostacy;
                                _row = _grid.Rows.Add();
                                _row.ForeColor = Color.Maroon;
                                break;
                            }
                        case em.xmem_status.Deceased:
                            {
                                _grid = datadeceased;
                                _row = _grid.Rows.Add();
                                _row.ForeColor = Color.Red;
                                break;
                            }
                        case em.xmem_status.Transfered:
                            {
                                _grid = datatransfered;
                                _row = _grid.Rows.Add();
                                _row.ForeColor = Color.Green;
                                break;
                            }
                    }
                }
                _row.Font = new Font("georgia", 12, FontStyle.Regular);
                _row.ReadOnly = iGBool.True;
                _row.Key = n.mem_code;
                _row.Cells["Code"].Value = n.mem_code;
                _row.Cells["Member Name"].Value = string.Format("{0} {1}", xso.xso.DATA_COMMON[n.mem_title_id].item_name, n.mem_name).ToProperCase();
                _row.Cells["G"].Value = string.Format("{0}", xso.xso.DATA_COMMON[n.gender_id].item_name);
               
                _row.Cells["colSearch"].Value = string.Format("{0} {1}", n.mem_code.ToLower(), n.mem_name.ToLower()).ToLower();
                _row.Height += 2;
                _row.Key = n.mem_code;
                _row.Tag = n;
            _grid.EndUpdate();
            _grid.Cols.AutoWidth();
            _grid.AutoResizeCols = false;
            _grid.CurCell = _grid.Rows[n.mem_code].Cells[0];
            DisplayTotal();
        }

        
        private void MemSearchFilter()
        {
            if (fGridD.Rows.Count > 0)
            {
                fGridD.Rows.Clear();
            }
            string _ret = textBoxX1.Text.ToLower();
            fGrid.BeginUpdate();
           
            try
            {
                for (int i = fGrid.Rows.Count - 1; i >= 0; i--)
                {
                    if (fGrid.Rows[i].Type != iGRowType.Normal) { continue; }
                    if (((string)fGrid.Cells[i, "colSearch"].Value).IndexOf(_ret) < 0)
                        fGrid.Rows[i].Visible = false;
                    else
                        fGrid.Rows[i].Visible = true;
                }
            }
            finally
            {
                fGrid.EndUpdate();
            }
         //  fGrid.SearchAsType.SearchText = _ret;
        }
        private void LoadMembers()
        {
            var _grid = new iGrid[] { fGrid, dataaging, dataapostacy, datadeceased, datatransfered };

            foreach (var g in _grid)
            {
                g.BeginUpdate();
             
            }

            if (datam.DATA_MEMBER == null || datam.DATA_MEMBER.Count == 0)
            {
                foreach (var g in _grid)
                {
                    g.EndUpdate();
                }
                return;
            }
            
           
            var nlist = from n in datam.DATA_MEMBER.Values
                        orderby n.mem_id
                        select n;
            iGRow _row = null;
            ic.church_groupC _gp = null;
            foreach (var n in nlist)
            {
                if (n.IsCurrentMember)
                {
                    _row = fGrid.Rows.Add();
                }
                else
                {
                    switch (n.mem_status)
                    {
                        case em.xmem_status.Ageing:
                            {
                                _row = dataaging.Rows.Add();
                                _row.ForeColor = Color.DarkBlue;
                                break;
                            }
                        case em.xmem_status.Apostacy:
                            {
                                _row = dataapostacy.Rows.Add();
                                _row.ForeColor = Color.Maroon;
                                break;
                            }
                        case em.xmem_status.Deceased:
                            {
                                _row = datadeceased.Rows.Add();
                                _row.ForeColor = Color.Red;
                                break;
                            }
                        case em.xmem_status.Transfered:
                            {
                                {
                                    _row = datatransfered.Rows.Add();
                                    _row.ForeColor = Color.Green;
                                    break;
                                }
                            }
                        case em.xmem_status.Deleted:
                            {
                                continue;
                                break;
                            }

                    }
                }
                _row.Font = new Font("georgia", 12, FontStyle.Regular);
                _row.ReadOnly = iGBool.True;
                _row.Cells["Code"].Value = n.mem_code;
                _row.Cells["Code"].AuxValue = n.mem_id;
                _row.Cells["mem_id"].Value = n.mem_id;
                if (n.mem_title_id > 0)
                {
                    _row.Cells["Member Name"].Value = string.Format("{0} {1}", xso.xso.DATA_COMMON[n.mem_title_id].item_name, n.mem_name).ToProperCase();
                }
                else
                {
                    _row.Cells["Member Name"].Value = string.Format("{0}", n.mem_name).ToProperCase();
                }
                _row.Cells["Member Name"].AuxValue = n.mem_name;
                _row.Cells["G"].Value = string.Format("{0}", xso.xso.DATA_COMMON[n.gender_id].item_name);
                //_gp = n.ChurchGroupCollection.Find(k => k.cg_type == m_default_type);
                //if (_gp != null)
                //{
                //     _row.Cells["Group Name"].Value = string.Format("{0}", _gp.cg_name);
                //}

                if (_row.Grid == fGrid)
                {
                    _row.Cells["Group Name"].ForeColor = Color.DarkBlue;
                    if (m_default_cg_type != null)
                    {
                        _gp = n.ChurchGroupCollection.Find(k => k.cg_type_id == m_default_cg_type.cg_type_id);
                        if (_gp != null)
                        {
                            _row.Cells["Group Name"].Value = string.Format("{0}", _gp.cg_name);
                            _row.Cells["Group Name"].ForeColor = Color.DarkBlue;
                        }
                    }
                }
                

                _row.Cells["colSearch"].Value = string.Format("{0} {1}", n.mem_code, n.mem_name).ToLower();
                _row.Key = n.mem_code;
                _row.AutoHeight();
                _row.Tag = n;
            }
            foreach (var g in _grid)
            {
                g.EndUpdate();
                g.Cols.AutoWidth();
                g.AutoResizeCols = false;
            }
           
            SortGrid1("mem_id");
            DisplayTotal();
        }
        private void SortGrid1(string col_name)
        {
            fGrid.SortObject.Clear();
            fGrid.SortObject.Add(col_name, iGSortOrder.Ascending);
            fGrid.Sort();
        }
        ic.memberC selected_mem = null;
        private void MemberManager_Load(object sender, EventArgs e)
        {
            this.Office2007ColorTable = DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.Silver;
            this.BackColor = Color.WhiteSmoke;
            datam.SecurityCheck();
            xso.xso.Intialize();
            datam.SystemInitializer();
            paneltotal.Text = string.Empty;
           
            tabControl1.SelectedTabIndex = 0;
            fGrid.CurRowChanged += new EventHandler(fGrid_CurRowChanged);
            fGridD.AfterAutoGroupRowCreated += new iGAfterAutoGroupRowCreatedEventHandler(fGridD_AfterAutoGroupRowCreated);
            fGridD.CustomDrawCellForeground+=new iGCustomDrawCellEventHandler(fGridD_CustomDrawCellForeground);
            fGridD.SizeChanged += new EventHandler(fGridD_SizeChanged);
            fGrid.AfterContentsSorted += new EventHandler(fGrid_AfterContentsSorted);
            this.VisibleChanged += new EventHandler(MemberManager_VisibleChanged);
            datadeceased.CurRowChanged += new EventHandler(fGrid_CurRowChanged);
            dataaging.CurRowChanged += new EventHandler(fGrid_CurRowChanged);
            datatransfered.CurRowChanged += new EventHandler(fGrid_CurRowChanged);
            dataapostacy.CurRowChanged += new EventHandler(fGrid_CurRowChanged);
            empty_picture = fn.CreateNoPictureImage();
            m_ExpandedGroup.Add("personal details", true);
            m_ExpandedGroup.Add("church groups", true);
            m_ExpandedGroup.Add("spouse & marital details",false);
            m_ExpandedGroup.Add("occupation details", false);
            m_ExpandedGroup.Add("address info",false);
            m_ExpandedGroup.Add("baptism details",false);
            m_ExpandedGroup.Add("children", false);
            m_ExpandedGroup.Add("next of kin", false);
            m_ExpandedGroup.Add("parents", false);
            m_ExpandedGroup.Add("neighbour", false);
            m_ExpandedGroup.Add("transfer info", false);
            m_ExpandedGroup.Add("apostacy info", false);
            m_ExpandedGroup.Add("deceased info", false);
            m_ExpandedGroup.Add("departments", false);
            m_ExpandedGroup.Add("region", false);
            m_ExpandedGroup.Add("church roles", false);
            if (datam.DATA_CG_TYPES != null)
            {
                m_default_cg_type = datam.DATA_CG_TYPES.Values.FirstOrDefault(k => k.is_default);
                if (m_default_cg_type != null)
                {
                    m_defaultChurchGroupsDropDown = fnn.CreateCombo();
                    m_defaultChurchGroupsDropDown.MaxVisibleRowCount = 6;
                    m_defaultChurchGroupsDropDown.SelItemForeColor = Color.DarkBlue;
                    var _cgs = (from h in datam.DATA_CHURCH_GROUPS.Values
                                where h.cg_type_id == m_default_cg_type.cg_type_id
                                select h).ToList();
                    foreach (var _t in _cgs)
                    {
                        m_defaultChurchGroupsDropDown.Items.Add(new fnn.iGComboItemEX()
                        {
                            Value = _t.cg_name,
                            Tag = _t
                        });
                    }
                    checkGroup.Visible = true;
                }
            }
            InitializeGridColumns();
            InitIgridColumns();
            load_group_context_menus();
            LoadMembers();
           
        }

        void fGrid_AfterContentsSorted(object sender, EventArgs e)
        {
            fGrid.SetCurCell(0, fGrid.CurCell == null ? 0 : fGrid.CurCell.Col.Index);
            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
        }
        private void load_group_context_menus()
        {
            ToolStripMenuItem tool_item = null;

            foreach (var m in m_ExpandedGroup)
            {
                tool_item = new ToolStripMenuItem();
                tool_item.Text = string.Format("{0}", m.Key);
              
                tool_item.ForeColor = Color.Maroon;
                tool_item.Tag = m;
                tool_item.Checked = true;
               
                tool_item.CheckOnClick = true;
                tool_item.Click += new EventHandler(tool_item_Click);
               
                tool_item = null;
            }
        }

        void tool_item_Click(object sender, EventArgs e)
        {
            
        }
        void fGridD_SizeChanged(object sender, EventArgs e)
        {
            ResizeGridDetail();
           pictureBox1.Left = (fGrid.Right - pictureBox1.Width) - 19;
        }
         void MemberManager_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                if (mem_maker != null && !mem_maker.IsDisposed)
                {

                    mem_maker.Close();
                    mem_maker.Dispose();
                }
                mem_maker = null;
                if (this.Visible & fGrid.Rows.Count > 0)
                {
                    if (tabControl1.SelectedTab == tabmen)
                    {
                        iGCell _cell = fGrid.CurCell;
                        fGrid.CurCell = null;
                        if (_cell != null)
                        {
                            fGrid.CurCell = _cell;
                        }
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }
        void fGridD_AfterAutoGroupRowCreated(object sender, iGAfterAutoGroupRowCreatedEventArgs e)
        {
            iGCell myGroupRowCell = fGridD.RowTextCol.Cells[e.AutoGroupRowIndex];
            myGroupRowCell.Row.AutoHeight();
            iGCell myFirstCellInGroup = fGridD.Cells[e.GroupedRowIndex, "Category"];
            myGroupRowCell.Value = string.Format("{0}", myFirstCellInGroup.Value.ToProperCase());
            myGroupRowCell.Row.RowTextCell.AuxValue = myFirstCellInGroup.Value;
            myGroupRowCell.Row.Key = string.Format("GP{0}", e.GroupedRowIndex);
            OtherGroups.Add(e.GroupedRowIndex - 1);
       
        }
        private void SortAndGroup()
        {
            fGridD.GroupObject.Clear();
            fGridD.SortObject.Clear();
            fGridD.GroupObject.Add("SValue");
            fGridD.SortObject.Add("SValue", iGSortOrder.Ascending);
            fGridD.Group();
        }
        private void CollapseOther()
        {
            if (fGridD.Rows.Count == 0) { return; }
            iGRow _r = null;
            foreach (var i in OtherGroups)
            {
                _r = fGridD.Rows[i];
              
                if (_r.Type == iGRowType.AutoGroupRow)
                {
                    _r.Expanded = m_ExpandedGroup[_r.RowTextCell.Text.ToLower()];
                }
            }
        }
        void fGrid_CurRowChanged(object sender, EventArgs e)
        {
            
            if (app_working)
            { return; }
            m_grid = sender as iGrid;
            if (m_grid.CurRow == null)
            {
                selected_mem = null; return;
            }
            OtherGroups.Clear();
            selected_mem = m_grid.CurRow.Tag as ic.memberC;
            label1.Text = m_grid.CurRow.Cells[1].Text;
            //if (_key_down)
            //{
            //    if (fGridD.Rows.Count >0)
            //    {
            //        clear_left_panel();
            //    }
            //    return;
            //}
            FillIGridDetails(selected_mem);
            CollapseOther();
            if (m_grid == fGrid)
            {
                MovePicture(fGrid.CurRow.Cells["picture"], selected_mem);
            }
         }
        private void MovePicture(iGCell _cell,ic.memberC _m)
        {
            if (_m == null || _m.objPicture == null || _m.objPicture.SmallPicture == null)
            {
                pictureBox1.Visible = false;
                pictureBox1.Image = null;
                return;
            }
            if (!pictureBox1.Visible) { api.LockWindowUpdate(pictureBox1.Handle); pictureBox1.Visible = true; }
            var _p = new Point(_cell.Bounds.Left, _cell.Bounds.Top);
            pictureBox1.Left = _p.X + 1;
            pic_hlp = (_p.Y + _cell.Bounds.Height) - 149;
            if (pic_hlp > -1)
            {
                pictureBox1.Top = pic_hlp;
            }
            else
            {
                pictureBox1.Top = _p.Y;
            }
            api.LockWindowUpdate(IntPtr.Zero);
        }
        #region member maker
        private void buttonAdd_Click(object sender, EventArgs e)
        {
           
        }
        void A_E_Maker_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (mem_maker != null && !mem_maker.IsDisposed)
                {
                    mem_maker.Dispose();
                }
                mem_maker = null;
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion
         public enum SentMsg
        {
            none = 1,
            Add_member,
             Update_member
        }
        public void FormCommunicate(object s_obj, SentMsg _msg, object Tag)
        {
            switch (_msg)
            {
                case SentMsg.Add_member:
                    {
                        NewMember(s_obj as ic.memberC);
                        break;
                    }
                case SentMsg.Update_member:
                    {
                        UpdateMember(s_obj as ic.memberC);
                        break;
                    }
            }
        }
      
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (fGrid.CurRow == null || fGrid.CurRow.Type != iGRowType.Normal)
            {
                e.Cancel = true;
            }
        }
        private void ts_edit_Click(object sender, EventArgs e)
        {
                if (selected_mem == null) { return; }
                int x = tabControl1.SelectedTabIndex;
              ////  MemberUpdater  mem_updater = new MemberUpdater();
              //  this.AddOwnedForm(mem_updater);
              //  fdata = new form_data();
              //  fdata.edit_object = selected_mem;
              //  fdata.ParentHandle = this.Handle;
              //  mem_updater.Tag = fdata;
              //  mem_updater.ShowDialog();
                tabControl1.SelectedTabIndex = x;
        }
        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            if (app_working) { return; }
            pictureBox1.Visible = false;
            if (tabControl1.SelectedTab != tabmen)
            {
                tabControl1.SelectedTab = tabmen;
                textBoxX1.Focus();
                Application.DoEvents();
            }
            MemSearchFilter();
            if (fGrid.Rows.Count == 0)
            {
                fGrid.CurRow = null;
            }
            else
            {
                MoveDown(-1);
            }
        }
        private void MoveUp(int _index)
        {
            int _val = _index;
            _val--;
            while (_val > -1)
            {
                if (fGrid.Rows[_val].Visible)
                {
                    fGrid.CurCell = fGrid.Rows[_val].Cells[0];
                    break;
                }
                _val--;
            }
        }
        private void MoveDown(int _index)
        {
            int _val = _index;
            _val++;
            while (_val < fGrid.Rows.Count)
            {
                if (fGrid.Rows[_val].Visible)
                {
                    fGrid.CurCell = null;
                    fGrid.CurCell = fGrid.Rows[_val].Cells[0];
                    if (fGridD.Rows.Count == 0)
                    {//rarely occurs,but does when one object is remaining in the grid
                        if (selected_mem != null)
                        {
                            label1.Text = fGrid.CurRow.Cells[1].Text;
                            FillIGridDetails(selected_mem);
                            CollapseOther();
                        }
                    }
                    break;
                }
                _val++;
            }
        }
        private void clear_left_panel()
        {
            pictureBox1.Image = null;
            pictureBox1.Visible = false;
            label1.Text = string.Empty;
            fGridD.Rows.Clear();
        }
        private void textBoxX1_KeyDown(object sender, KeyEventArgs e)
        {
            if (fGrid.CurRow == null) { return; }
            switch (e.KeyCode)
            {
                case (Keys.Up):
                    {
                        MoveUp(fGrid.CurRow.Index);
                        e.Handled = true;
                        break;
                    }
                case (Keys.Down):
                    {
                        MoveDown(fGrid.CurRow.Index);
                        e.Handled = true;
                        break;
                    }
                case Keys.Enter:
                    {
                        if (fGrid.CurRow != null)
                        {
                            app_working = true;
                            textBoxX1.Clear();
                            MemSearchFilter();
                            fGrid.CurRow.EnsureVisible();
                            if (selected_mem != null)
                            {
                                label1.Text = fGrid.CurRow.Cells[1].Text;
                                FillIGridDetails(selected_mem);
                                CollapseOther();
                                MovePicture(fGrid.CurRow.Cells["picture"], selected_mem);
                            }
                            app_working = false;
                        }
                        break;
                    }

            }
        }

       

        private void fGrid_AfterAutoGroupRowCreated(object sender, iGAfterAutoGroupRowCreatedEventArgs e)
        {
            if (pictureBox1.Visible)
            {
                pictureBox1.Visible = false;
            }
        }
        private void ts_delete_Click(object sender, EventArgs e)
        {
            if (!dbm.DeleteRecordWarning())
            {
                return;
            }
            if (!dbm.DeleteRecordWarning())
            {
                return;
            }
            var _mem = fGrid.CurRow.Tag as ic.memberC;
            _mem.mem_status_id = em.xmem_status.Deleted.ToInt16();
            var _row_index = fGrid.CurRow.Index;
            fGrid.Rows.RemoveAt(_row_index);
            dbh.SingleUpdateCommandALL("member_tb", new string[] { "mem_status_type_id", "pc_us_id", "mem_id" }, new object[] { em.xmem_status.Deleted.ToInt16(), datam.PC_US_ID, _mem.mem_id }, 1);
            DisplayTotal();
            clear_left_panel();
         }

        private void fGrid_SizeChanged(object sender, EventArgs e)
        {
            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
            pictureBox1.Left = (fGrid.Right - pictureBox1.Width) - 19;
        }

        private void transferedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var _fm = new TransferForm())
            {
                _fm.Owner = this;
                _fm.Tag = selected_mem;
                _fm.ShowDialog();

            }
            if (selected_mem.objTransfer != null)
            {
                NewMember(selected_mem);
                fGrid.Rows.RemoveAt(fGrid.CurRow.Index);
                clear_left_panel();
            }
           
            
        }

        private void deceasedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var _fm = new DeathForm())
            {
                _fm.Owner = this;
                _fm.Tag = selected_mem;
                _fm.ShowDialog();

            }
            if (selected_mem.objDeath != null)
            {
                NewMember(selected_mem);
                fGrid.Rows.RemoveAt(fGrid.CurRow.Index);
                clear_left_panel();
            }
          

        }

        private void apostacyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var _fm = new ApostacyForm())
            {
                _fm.Owner = this;
                _fm.Tag = selected_mem;
                _fm.ShowDialog();

            }
            if (selected_mem.objApostacy != null)
            {
                NewMember(selected_mem);
                fGrid.Rows.RemoveAt(fGrid.CurRow.Index);
                clear_left_panel();
            }
           
        }
        private void fGrid_CellMouseDown(object sender, iGCellMouseDownEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                fGrid.SetCurCell(e.RowIndex, e.ColIndex);
            }
        }

        private void fGrid_VScrollBarValueChanged(object sender, EventArgs e)
        {
            if (pictureBox1.Visible) { pictureBox1.Visible = false; }
        }

        private void tabControl1_SelectedTabChanged(object sender, DevComponents.DotNetBar.TabStripTabChangedEventArgs e)
        {
            fGridD.Rows.Clear();
            label1.Text = string.Empty;
            var _grid = new iGrid[] { fGrid, dataaging, dataapostacy, datadeceased, datatransfered };
            foreach (var j in _grid)
            {
                if (j.CurCell != null) { j.CurCell = null; }
            }
            DisplayTotal();
        }

        private void fGridD_TextBoxKeyDown(object sender, iGTextBoxKeyDownEventArgs e)
        {
            e.DoDefault = false;
        }

        private void fGridD_TextBoxKeyPress(object sender, iGTextBoxKeyPressEventArgs e)
        {
            e.DoDefault = false;
        }

        private void alwaysToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fGridD_AfterRowStateChanged(object sender, iGAfterRowStateChangedEventArgs e)
        {
            if (e.Expanded)
            {

                m_ExpandedGroup[fGridD.Rows[e.RowIndex].RowTextCell.Text.ToLower()] = true;
                ResizeGridDetail();
               
            }
            else
            {
                m_ExpandedGroup[fGridD.Rows[e.RowIndex].RowTextCell.Text.ToLower()] = false;
            }
           
        }

        private void fGridD_EllipsisButtonClick(object sender, iGEllipsisButtonClickEventArgs e)
        {
            using (var _fm = new MemberDetail())
            {
                _fm.Tag = datam.DATA_MEMBER[fGridD.Rows[e.RowIndex].Cells["mem_id"].Value.ToInt32()];
                _fm.ShowDialog();
            }
        }

        private void memberPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void editMemberPICTUREToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void ageingMememberToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
         private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string str = "Are You Sure You Want To Remove Member From This Section ??";
            if (!dbm.WarningMessage(str, "Delete Warning"))
            {
                return;
            }
            var _g = contextMenuStrip3.SourceControl as iGrid;
            if (_g != null)
            {
                var _row = _g.CurRow;
                if (_row != null)
                {
                    ic.memberC _mem = _row.Tag as ic.memberC;
                    using (xing xd = new xing())
                    {
                        switch (_mem.mem_status)
                        {
                            case em.xmem_status.Apostacy:
                                {
                                    xd.SingleUpdateCommandALL("member_tb", new string[] { "mem_status_type_id", "pc_us_id", "mem_id" }, new object[] { em.xmem_status.Normal.ToByte(), datam.PC_US_ID, _mem.mem_id }, 1);
                                    if (_mem.objApostacy != null)
                                    {
                                        xd.InsertUpdateDelete("delete from apostacy_tb where mem_id=" + _mem.mem_id);
                                        _mem.objApostacy = null;
                                    }
                                    break;
                                }
                            case em.xmem_status.Deceased:
                                {
                                    xd.SingleUpdateCommandALL("member_tb", new string[] { "mem_status_type_id", "pc_us_id", "mem_id" }, new object[] { em.xmem_status.Normal.ToByte(), datam.PC_US_ID, _mem.mem_id }, 1);
                                    if (_mem.objDeath != null)
                                    {
                                        xd.InsertUpdateDelete("delete from death_tb where mem_id=" + _mem.mem_id);
                                        _mem.objDeath = null;
                                    }
                                    break;
                                }
                            case em.xmem_status.Transfered:
                                {
                                    xd.SingleUpdateCommandALL("member_tb", new string[] { "mem_status_type_id", "pc_us_id", "mem_id" }, new object[] { em.xmem_status.Normal.ToByte(), datam.PC_US_ID, _mem.mem_id }, 1);
                                    if (_mem.objTransfer != null)
                                    {
                                        xd.InsertUpdateDelete("delete from transfer_tb where mem_id=" + _mem.mem_id);
                                        _mem.objTransfer = null;
                                    }
                                    break;
                                }
                        }
                        xd.CommitTransaction();
                        _mem.mem_status_id = em.xmem_status.Normal.ToByte();
                    }
                    _g.Rows.RemoveAt(_row.Index);
                    NewMember(_mem);
                }
            }
        }

         private void contextMenuStrip3_Opening(object sender, CancelEventArgs e)
         {
             
         }

         private void fGridD_CustomDrawCellBackground(object sender, iGCustomDrawCellEventArgs e)
         {
           
         }

         private void buttonItem1_Click(object sender, EventArgs e)
         {
             if (mem_maker == null)
             {
                 mem_maker = new MemberMaker2();
                 this.AddOwnedForm(mem_maker);
                 mem_maker.FormClosing -= new FormClosingEventHandler(A_E_Maker_FormClosing);
                 mem_maker.FormClosing += new FormClosingEventHandler(A_E_Maker_FormClosing);
                 mem_maker.Show();
                 mem_maker.BringToFront();
             }
             else
             {
                 if (mem_maker.WindowState == FormWindowState.Minimized)
                 {
                     mem_maker.WindowState = FormWindowState.Normal;
                 }
                 mem_maker.BringToFront();
             }
         }

         private void buttonItem3_Click(object sender, EventArgs e)
         {
             using (var _fm = new BulkPicEditor())
             {
                 _fm.Owner = this;
                 _fm.ShowDialog();
             }
           
         }

         private void buttonItem2_Click(object sender, EventArgs e)
         {
             using (var _fm = new MemberUpd())
             {
                 _fm.Owner = this;
                 _fm.ShowDialog();
             }
            
         }

         private void editMemberDataToolStripMenuItem_Click(object sender, EventArgs e)
         {
             using (var _fm = new MemberMakerTempUpdater())
             {
                 _fm.Tag = selected_mem;
                 _fm.Owner = this;
                 _fm.ShowDialog();
             }
            UpdateMember(selected_mem);
         }
        
         private void fGrid_KeyDown(object sender, KeyEventArgs e)
         {
             
         }

        
         private void fGrid_KeyUp(object sender, KeyEventArgs e)
         {
            
            // FillIGridDetails(selected_mem);
            // CollapseOther();
            //// MovePicture(fGrid.CurRow.Cells["picture"], selected_mem);
         }

         private void printMemberSummaToolStripMenuItem_Click(object sender, EventArgs e)
         {

             fnn.PrintIGridNormal(fGrid, "Member Summary Report", null);
             
                 
                
         }

         private void toolStripMenuItem2_Click(object sender, EventArgs e)
         {

         }

         private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
         {
             if (fGrid.SelectedRows.Count == 0)
             {
                 e.Cancel = true;
                 return;
             }
         }

         private void pirntReportToolStripMenuItem_Click(object sender, EventArgs e)
         {
           // var _mem = fGrid.SelectedRows[0].Tag as ic.memberC;
            fnn.PrintIGridGrouped(fGridD, fGridD.Rows["Member Name"].Cells[1].Text, null, "SValue");
         }

         private void fGridD_AfterContentsGrouped(object sender, EventArgs e)
         {
             fGridD.Rows[OtherGroups.First()].Expanded = true;
         }

         private void showageinfo_CheckedChanged(object sender, EventArgs e)
         {
             ShowHideBirthInfo();           
         }

         private void houseHold_Click(object sender, EventArgs e)
         {
             using (var _fm = new HouseHoldMaker())
             {
                 _fm.Owner = this;
                 _fm.ShowDialog();
             }
         }

        private void fGrid_ColHdrDoubleClick(object sender, iGColHdrDoubleClickEventArgs e)
        {
            //switch (fGrid.Cols[e.ColIndex].Key)
            //{
            //    case "Group Name":
            //        {
            //            foreach(var _cell in fGrid.Cols[e.ColIndex].Cells.Cast<iGCell>())
            //            {
            //                _cell.ReadOnly = iGBool.False;
            //            }
            //          //  fGrid.Cols[e.RowIndex].CellStyle.ReadOnly = iGBool.False;
            //            break;
            //        }
            //}
        }

        private void checkGroup_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var _cell in fGrid.Cols["Group Name"].Cells.Cast<iGCell>())
            {
                if (checkGroup.Checked)
                {
                    _cell.ReadOnly = iGBool.False;
                }
                else
                {
                    _cell.ReadOnly = iGBool.True;
                }
            }
        }

        private void fGrid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
        {
            switch(fGrid.Cols[e.ColIndex].Key)
            {
                case "Group Name":
                    {
                        if (fGrid.Rows[e.RowIndex].Cells[e.ColIndex].AuxValue == null)
                        {
                            fGrid.Rows[e.RowIndex].Cells[e.ColIndex].Value = null;
                            fGrid.Focus();
                            fGrid.SetCurCell(e.RowIndex, e.ColIndex);
                            //
                            var _mem = fGrid.Rows[e.RowIndex].Tag as ic.memberC;
                            var _prev_cg = _mem.ChurchGroupCollection.Find(h => h.cg_type_id == m_default_cg_type.cg_type_id);
                            if (_prev_cg != null)
                            {
                                _mem.ChurchGroupCollection.Remove(_prev_cg);
                            }
                            using (var xd = new xing())
                            {
                                xd.SingleDeleteCommandExp("church_group_members_tb", new string[]
                                 {
                                        "mem_id",
                                        "cg_type_id"
                                 }, new int[]
                                 {
                                        _mem.mem_id,
                                        m_default_cg_type.cg_type_id
                                  });
                                xd.CommitTransaction();
                            }
                            FillIGridDetails(_mem);
                            return;
                        }
                        else
                        {
                            var _cg = (fGrid.Rows[e.RowIndex].Cells[e.ColIndex].AuxValue as fnn.iGComboItemEX).Tag as ic.church_groupC;
                            if (_cg != null)
                            {
                                bool _inserted = false;
                                using (var xd = new xing())
                                {
                                    var _mem = fGrid.Rows[e.RowIndex].Tag as ic.memberC;
                                    var _prev_cg = _mem.ChurchGroupCollection.Find(h => h.cg_type_id == m_default_cg_type.cg_type_id);
                                    if (_prev_cg != null)
                                    {
                                        _mem.ChurchGroupCollection.Remove(_prev_cg);
                                    }
                                    else
                                    {
                                        #region insert
                                      var _result_id=  xd.SingleInsertCommandIgnore("church_group_members_tb", new string[]
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
                        _cg.cg_id,
                        _cg.cg_type_id,
                        1,
                            datam.CURR_DATE.Date,
                            null,33,
                            datam.LCH_ID,
                            datam.LCH_TYPE_ID,0//time_stamp
                                                     });
                                        _inserted = _result_id == 0 ? false : true;
                                        #endregion
                                      
                                    }
                                    _mem.ChurchGroupCollection.Add(_cg);
                                    if (!_inserted)
                                    {
                                        xd.SingleUpdateCommandALL("church_group_members_tb", new string[]
                                        {
                                        "cg_id",
                                        "mem_id",
                                        "cg_type_id"
                                        }, new object[]
                                        {
                                        _cg.cg_id,
                                        _mem.mem_id,
                                        _cg.cg_type_id

                                        }, 2);
                                    }
                                    xd.CommitTransaction();
                                }
                                var _current_index = fGrid.Rows[e.RowIndex].Index;
                                if (_current_index < (fGrid.Rows.Count - 1))
                                {
                                    _current_index++;
                                }
                                for (int j = _current_index; j < fGrid.Rows.Count; j++)
                                {
                                    if (fGrid.Rows[j].Cells[e.ColIndex].Value == null)
                                    {
                                        fGrid.Focus();
                                        fGrid.SetCurCell(j, e.ColIndex);
                                        break;
                                    }
                                }
                                fGrid.Cols[e.ColIndex].AutoWidth();
                            }
                        }

                        sdata.ClearFormCache(em.fm.analyze_offering_range.ToInt16());
                        sdata.ClearFormCache(em.fm.church_groups.ToInt16());
                        break;
                    }

            }
        }

        private void buttonMapMember_Click(object sender, EventArgs e)
        {
            if (!sdata.HasMenuRight(4, true))
            {
                return;
            }
            using (var _fm = new MapDuplicateMembersMaker())
            {
                _fm.Owner = this;
                _fm.ShowDialog();
                if (_fm.Tag != null)
                {
                    sdata.FORCED_SHUTDOWN = true;
                    System.Environment.Exit(0);
                }
            }
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            string _str = "Are You Sure You Want To Perform The Above Operation ??";
            if (!dbm.WarningMessage(_str, "Map Warning"))
            {
                return;
            }
            datam.ShowWaitForm("Mapping Duplicates,Pliz Wait..");
            Application.DoEvents();
            using (var _xd = new xing())
            {
                fnn.CorrectDuplicateNames(_xd);
                _xd.CommitTransaction();
            }
            using (var _xd = new xing())
            {
                _xd.InsertUpdateDelete("UPDATE member_tb AS m,off_main_tb AS o SET o.`source_name`=m.`mem_name` WHERE o.`source_type_id`=11 AND o.`source_id`=m.`mem_id`");
                _xd.CommitTransaction();
            }
            datam.HideWaitForm();
            sdata.FORCED_SHUTDOWN = true;
            System.Environment.Exit(0);
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            using (var _fm = new BackUp.TransferToMySqlForm())
            {
                _fm.Owner = this;
                _fm.ShowDialog();
            }
        }
    }
}
