using SdaHelperManager;
using SdaHelperManager.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

namespace MTOMS.Expenses
{
    public partial class ExpenseAccountMaker : DevComponents.DotNetBar.Office2007Form
    {
        public ExpenseAccountMaker()
        {
            InitializeComponent();
        }
        enum bk_process
        {
            none = 1,
            formload,
            get_id
        }
        bk_process m_process = bk_process.none;
        SortedDictionary<string, em.AccountOwnerTypeS> m_OWNER_Types { get; set; }
        private iGDropDownList m_Deparments = null;
        private iGDropDownList m_church_groups = null;
        private iGDropDownList m_members = null;
        private iGDropDownList m_CG_Shared = null;
        private void ExpenseAccountMaker_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            datam.SecurityCheck();
            InitIgridColumns();
            InitializeGridPhoneColumns();
            m_process = bk_process.formload;
            //
            m_OWNER_Types = new SortedDictionary<string, em.AccountOwnerTypeS>();
          //  m_OWNER_Types.Add("CUC", em.AccountOwnerTypeS.CUC);
            m_OWNER_Types.Add("CHURCH", em.AccountOwnerTypeS.CHURCH);
            m_OWNER_Types.Add("DISTRICT", em.AccountOwnerTypeS.DISTRICT);
            m_OWNER_Types.Add("CHURCH_GROUP", em.AccountOwnerTypeS.CHURCH_GROUP);
            m_OWNER_Types.Add("DEPARTMENT", em.AccountOwnerTypeS.DEPARTMENT);
            m_OWNER_Types.Add("CHURCH_MEMBER", em.AccountOwnerTypeS.CHURCH_MEMBER);
            m_OWNER_Types.Add("OTHER", em.AccountOwnerTypeS.OTHER);
            m_OWNER_Types.Add("CHURCH_GROUP_SHARED", em.AccountOwnerTypeS.CHURCH_GROUP_SHARED);
            fGrid.AfterCommitEdit+=fGrid_AfterCommitEdit;
            //
            iGridCategory.AfterCommitEdit += new iGAfterCommitEditEventHandler(iGridCategory_AfterCommitEdit);
            iGridCategory.TextBoxKeyDown += new iGTextBoxKeyDownEventHandler(iGridCategory_TextBoxKeyDown);
            //
           
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
            switch (m_process)
            {
                case bk_process.formload:
                    {
                        datam.SystemInitializer();
                        using (var xd = new xing())
                        {
                            datam.GetDepartments(xd);
                            datam.InitExpenses(xd);
                            xd.CommitTransaction();
                        }
                        break;
                    }

            }
        }
         private void LoadDropDownLists()
         {
             m_Deparments = fnn.CreateCombo();
             m_church_groups = fnn.CreateCombo();
             m_members = fnn.CreateCombo();
             m_CG_Shared = fnn.CreateCombo();
             if (datam.DATA_CHURCH_GROUPS != null)
             {
                 foreach (var k in datam.DATA_CHURCH_GROUPS.Values)
                 {
                     m_church_groups.Items.Add(new fnn.iGComboItemEX()
                     {
                         ID = k.cg_id,
                         Value = string.Format("{0} :: {1}", datam.DATA_CG_TYPES[k.cg_type_id].cg_type_name, k.cg_name)
                     });
                 }
                 //
                 var dlist = (from l in datam.DATA_CHURCH_GROUPS.Values
                              select l.cg_type_id).Distinct().ToList();
                 ic.church_group_typeC _cg_type = null;
                 foreach (var k in dlist)
                 {
                     _cg_type = datam.DATA_CG_TYPES[k];
                     m_CG_Shared.Items.Add(new fnn.iGComboItemEX()
                     {
                         ID = _cg_type.cg_type_id,
                         Value = string.Format("{0} Shared", _cg_type.cg_type_name),
                         ForeColor = Color.DarkBlue

                     });
                 }
             }
             //
             if (datam.DATA_DEPARTMENT != null)
             {
                 var nlist = from k in datam.DATA_DEPARTMENT.Values
                             where k.dept_type == em.dept_typeS.main
                             orderby k.dept_name
                             select k;
                 foreach (var k in nlist)
                 {
                     m_Deparments.Items.Add(new fnn.iGComboItemEX()
                     {
                         ID = k.dept_id,
                         Value = k.dept_name
                     });
                 }
             }
             //
             if (datam.DATA_MEMBER != null)
             {
                 var nlist = from k in datam.DATA_MEMBER.Values
                             where k.IsCurrentMember
                             select k;
                 foreach (var k in nlist)
                 {
                     m_members.Items.Add(new fnn.iGComboItemEX()
                     {
                         ID = k.mem_id,
                         Value = string.Format("{0}-{1}", k.mem_name, k.mem_code)
                     });
                 }
             }
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
            _row.Font = new Font("verdana", 10, FontStyle.Bold);
            _row.Cells["name"].Col.Width = 200;
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
            string group_name = "Expense Information";
            //
            _row = CreateNewRow("Expense Account Name", "account", typeof(string), group_index, group_name);
            _row.Cells[0].TextAlign = iGContentAlignment.MiddleRight;
            _row.Cells[1].TextAlign = iGContentAlignment.MiddleLeft;
           //
           
            //item code
            _row = CreateNewRow("+(F1)  Department", "department", typeof(string), group_index, group_name);
            _row.Cells[0].TextAlign = iGContentAlignment.MiddleRight;
            _row.Cells[1].TextAlign = iGContentAlignment.MiddleLeft;
            //
            icombo = fnn.CreateCombo();
            var slist = from k in datam.DATA_DEPARTMENT.Values
                         select k;
            foreach (var c in slist)
            {
                icombo.Items.Add(new fnn.iGComboItemEX()
                {
                    Value = c.dept_name,
                    Text = c.dept_name,
                    Tag = c
                });
            }
            icombo.SelItemBackColor = Color.Bisque;
            _row.Cells["desc"].DropDownControl = icombo;
            _row.Cells["desc"].Value = null;
            //
            _row = CreateNewRow("Expense Category", "item_cat", typeof(string), group_index, group_name);
            _row.Cells[0].TextAlign = iGContentAlignment.MiddleRight;
            _row.Cells[1].TextAlign = iGContentAlignment.MiddleLeft;
            //
            icombo = fnn.CreateCombo();
            foreach (var c in datam.DATA_EXPENSE_CATEGORY.Values)
            {
                icombo.Items.Add(new fnn.iGComboItemEX()
                {
                    Value = c.exp_cat_name,
                    Text = c.exp_cat_name,
                    Tag = c
                });
            }
            icombo.SelItemBackColor = Color.Bisque;
            _row.Cells["desc"].DropDownControl = icombo;
            _row.Cells["desc"].Value = null;
           
           
            //
            _row = fGrid.Rows.Add();
            _row.Height = 5;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            _row = CreateNewRow("Owner Type", "owner_type", typeof(string), group_index, group_name);
            icombo = fnn.CreateCombo();
           

            foreach (var k in m_OWNER_Types.Keys)
            {
                icombo.Items.Add(new fnn.iGComboItemEX()
                {
                    Value = k,
                    Tag = k
                });
              
            }
            _row.Cells["desc"].DropDownControl = icombo;
            //
            _row = CreateNewRow("Owner Name", "owner", typeof(string), group_index, group_name);
            _row.Visible = false;
            //
            _row = fGrid.Rows.Add();
            _row.Height = 5;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            //
            fGrid.EndUpdate();
            var nlist = from r in fGrid.Rows.Cast<iGRow>()
                        where !string.IsNullOrEmpty(r.Key)
                        select r;
            foreach (var t in nlist)
            {
                t.AutoHeight();
                t.Height += 5;
                t.Cells[0].TextAlign = iGContentAlignment.MiddleRight;
                t.Cells[1].TextAlign = iGContentAlignment.MiddleLeft;
            }
            fGrid.SetCurCell(fGrid.Rows["account"].Index, 1);
            fGrid.Select(); fGrid.Focus();
        }
      
        private void InitializeGridPhoneColumns()
        {
            #region Columns To Display
            List<iGrid> _grids = new List<iGrid>()
            {
                iGridCategory
            };
            string[] _cols = new string[] 
            { 
               #region MyRegion
		        "Phone No", 
                "X"
                #endregion
            };
            iGCol myCol = null;
            foreach (var _grid in _grids)
            {

                foreach (var c in _cols)
                {
                    myCol = _grid.Cols.Add(c, c);
                    myCol.IncludeInSelect = true;
                    myCol.SortType = iGSortType.None;

                    myCol.ColHdrStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    myCol.ColHdrStyle.ForeColor = Color.DarkGreen;
                    myCol.ColHdrStyle.Font = new Font("arial", 10, FontStyle.Bold);
                    // myCol.Width = 70;
                    myCol.AllowSizing = false;
                }
               
                if (_grid == iGridCategory)
                {
                    _grid.Cols[0].Text = "Expense Categories";
                }
               
                _grid.Cols[1].Visible = false;
                //
                _grid.Cols["X"].CellStyle.ValueType = typeof(string);
                _grid.Cols["X"].ColHdrStyle.ForeColor = Color.Maroon;
                _grid.Cols["X"].CellStyle.TypeFlags = iGCellTypeFlags.NoTextEdit | iGCellTypeFlags.HasEllipsisButton;
                _grid.Cols["X"].AllowSizing = false;
                _grid.Cols["X"].CellStyle.Selectable = iGBool.False;
                _grid.Cols[1].Visible = false;
                //
                _grid.DefaultRow.Height = _grid.GetPreferredRowHeight(true, false);
                _grid.DefaultAutoGroupRow.Height = _grid.DefaultRow.Height;
                _grid.Cols.AutoWidth();
            }

            #endregion
        }
        private void backworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch (m_process)
            {
                case bk_process.formload:
                    {
                        LoadDropDownLists();
                        LoadMainGrid();
                        LoadCategoryGrid();
                        break;
                    }

            }
        }
        private void AddRowX(iGrid _grid)
        {
            if (_grid.Rows.Cast<iGRow>().Count(j => j.Cells[0].Value == null) > 0)
            {
                _grid.Focus();
                var _cell = _grid.Rows.Cast<iGRow>().Where(j => j.Cells[0].Value == null).Select(v=>v.Cells[0]).FirstOrDefault();
                if (_cell != null)
                {
                    _cell.Selected = true;
                }
                return;
            }
            iGRow _row = null;
            _grid.Focus();
            _row = _grid.Rows.Add();
            _row.Cells[0].Col.Width = 290;
            _row.Cells[1].Col.Width = 22;
            _row.Font = new Font("verdana", 11, FontStyle.Regular);
            _row.ForeColor = Color.DarkBlue;
            _row.AutoHeight();
            _row.Height += 1;
            _grid.SetCurCell(_grid.Rows.Count - 1, 0);
            SendKeys.Send("{ENTER}");
            _grid.AutoResizeCols = false;
        }
        
        private void LoadCategoryGrid()
        {

            iGRow _row = null;
            iGridCategory.BeginUpdate();
            foreach (var r in datam.DATA_EXPENSE_CATEGORY.Values)
            {
                _row = iGridCategory.Rows.Add();
                _row.Cells[0].Col.Width = 290;
                _row.Cells[1].Col.Width = 22;
                _row.Font = new Font("verdana", 11, FontStyle.Regular);
                _row.ForeColor = Color.DarkBlue;
                _row.AutoHeight();
                _row.Height += 1;
                _row.Tag = r;
                _row.Cells[0].Value = r.exp_cat_name;
            }
            iGridCategory.EndUpdate();

        }
        void iGridCategory_TextBoxKeyDown(object sender, iGTextBoxKeyDownEventArgs e)
        {
            var _grid = sender as iGrid;
            if (e.KeyValue == Keys.Down | e.KeyValue == Keys.Up)
            {
                _grid.CommitEditCurCell();
            }

        }
        private void fGrid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
        {
            if (fGrid.Rows[e.RowIndex].Cells["desc"].DropDownControl != null && fGrid.Rows[e.RowIndex].Cells["desc"].AuxValue == null)
            {
                fGrid.Rows[e.RowIndex].Cells["desc"].Value = null;
                if (fGrid.Rows[e.RowIndex].Key == "owner_type")
                {
                    fGrid.Rows["owner"].Cells[1].Value = null;
                    fGrid.Rows["owner"].Cells[1].AuxValue = null;
                    fGrid.Rows["owner"].Visible = false;
                }
                return;
            }
            if (fGrid.Rows[e.RowIndex].Cells["desc"].DropDownControl == null && fGrid.Rows[e.RowIndex].Cells["desc"].Value != null)
            {
                if (fGrid.Rows[e.RowIndex].Cells["desc"].ValueType == typeof(string))
                {
                    fGrid.Rows[e.RowIndex].Cells["desc"].Value = fGrid.Rows[e.RowIndex].Cells["desc"].Value.ToString().ToProperCase();
                }
                if (fGrid.Rows[e.RowIndex].Key == "owner_type")
                {
                    fGrid.Rows["owner"].Cells[1].Value = null;
                    fGrid.Rows["owner"].Cells[1].AuxValue = null;
                    fGrid.Rows["owner"].Visible = false;
                }

            }
            if (fGrid.Rows[e.RowIndex].Key == "owner_type")
            {
                var _val = (fGrid.Rows["owner_type"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag.ToStringNullable();
                fGrid.Rows["owner"].Cells[1].AuxValue = null;
                fGrid.Rows["owner"].Cells[1].Value = null;
                fGrid.Rows["owner"].Cells[1].DropDownControl = null;
                fGrid.Rows["owner"].Visible = true;
                fGrid.Rows["owner"].Tag = null;

                switch (m_OWNER_Types[_val])
                {
                    case em.AccountOwnerTypeS.CUC:
                    case em.AccountOwnerTypeS.DISTRICT:
                    case em.AccountOwnerTypeS.CHURCH:
                        {
                            fGrid.Rows["owner"].Visible = false;
                            break;
                        }
                    case em.AccountOwnerTypeS.DEPARTMENT:
                        {
                            fGrid.Rows["owner"].Visible = true;
                            fGrid.Rows["owner"].Cells[1].DropDownControl = m_Deparments;
                            fGrid.Rows["owner"].Tag = em.AccountOwnerTypeS.DEPARTMENT;
                            break;
                        }
                    case em.AccountOwnerTypeS.CHURCH_GROUP:
                        {
                            fGrid.Rows["owner"].Visible = true;
                            fGrid.Rows["owner"].Cells[1].DropDownControl = m_church_groups;
                            fGrid.Rows["owner"].Tag = em.AccountOwnerTypeS.CHURCH_GROUP;

                            break;
                        }
                    case em.AccountOwnerTypeS.CHURCH_MEMBER:
                        {
                            fGrid.Rows["owner"].Visible = true;
                            fGrid.Rows["owner"].Cells[1].DropDownControl = m_members;
                            fGrid.Rows["owner"].Tag = em.AccountOwnerTypeS.CHURCH_MEMBER;

                            break;
                        }
                    case em.AccountOwnerTypeS.CHURCH_GROUP_SHARED:
                        {
                            fGrid.Rows["owner"].Visible = true;
                            fGrid.Rows["owner"].Cells[1].DropDownControl = m_CG_Shared;
                            //
                            break;
                        }
                    case em.AccountOwnerTypeS.OTHER:
                        {

                            break;
                        }
                }
            }
            //
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
                    if (fGrid.Rows[k].Cells[1].Enabled == iGBool.False) { continue; }
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
        void iGridCategory_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
        {
            iGrid _grid = sender as iGrid;
            if (_grid.Rows[e.RowIndex].Cells[0].Value != null)
            {
                _grid.Rows[e.RowIndex].Cells[0].Value = _grid.Rows[e.RowIndex].Cells[0].Value.ToProperCase();
            }
            if (sender == iGridCategory)
            {
                #region Item Category
                if (_grid.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    if (_grid.Rows[e.RowIndex].Tag != null)
                    {
                        var _category = _grid.Rows[e.RowIndex].Tag as ic.expense_catC;
                        using (var xd = new xing())
                        {

                            if (xd.ExecuteScalarInt("select TOP 1 exp_acc_id from acc_expense_accounts_tb where exp_cat_id=" + _category.exp_cat_id ) == 0)
                            {
                                xd.SingleDeleteCommandExp("acc_expense_cat_tb", new string[] { "exp_cat_id" }, new int[] { _category.exp_cat_id }); xd.CommitTransaction();
                            }
                            else
                            {
                                _grid.Rows[e.RowIndex].Cells[0].Value = _category.exp_cat_name;
                                dbm.ErrorMessage("There Are Some Expense Accounts Which Are Attached To This Category", "Delete Operation Will Be Cancelled");
                                return;
                            }
                        }
                        var _dp_item = (from r in (fGrid.Rows["item_cat"].Cells["desc"].DropDownControl as iGDropDownList).Items.Cast<fnn.iGComboItemEX>()
                                        where r.Tag == _category
                                        select r).FirstOrDefault();
                        if (_dp_item != null)
                        {
                            (fGrid.Rows["item_cat"].Cells["desc"].DropDownControl as iGDropDownList).Items.Remove(_dp_item);
                            fGrid.Rows["item_cat"].Cells["desc"].AuxValue = null;
                            fGrid.Rows["item_cat"].Cells["desc"].Value = null;
                        }
                        _grid.Rows[e.RowIndex].Tag = null;
                        datam.DATA_EXPENSE_CATEGORY.Remove(_category.exp_cat_id);
                        _grid.Rows.RemoveAt(e.RowIndex);
                    }
                    return;
                }
                else
                {
                    if (_grid.Rows[e.RowIndex].Tag == null)
                    {
                        //insert
                        if (datam.DATA_EXPENSE_CATEGORY.Values.FirstOrDefault(k => k.exp_cat_name.ToLower() == _grid.Rows[e.RowIndex].Cells[0].Text.ToLower()) != null)
                        {
                            dbm.ErrorMessage("Duplicate Entry Found", "Duplicate Error");
                            _grid.Rows[e.RowIndex].Cells[0].Value = null;
                            return;
                        }
                        var _category = new ic.expense_catC();
                        using (var xd = new xing())
                        {
                            _category.exp_cat_name = _grid.Rows[e.RowIndex].Cells[0].Text.ToProperCase();
                            _category.exp_cat_id = xd.SingleInsertCommandTSPInt("acc_expense_cat_tb", new string[] { "exp_cat_name", "exp_type", "fs_time_stamp", "lch_id" },
                             new object[] { _category.exp_cat_name, emm.export_type.insert.ToByte(), 0, datam.LCH_ID });
                            xd.CommitTransaction();
                        }
                        _grid.Rows[e.RowIndex].Tag = _category;
                        datam.DATA_EXPENSE_CATEGORY.Add(_category.exp_cat_id, _category);
                        (fGrid.Rows["item_cat"].Cells["desc"].DropDownControl as iGDropDownList).Items.Add(new fnn.iGComboItemEX { Tag = _category, Text = _category.exp_cat_name, Value = _category.exp_cat_name });
                    }
                    else
                    {
                        var _category = _grid.Rows[e.RowIndex].Tag as ic.expense_catC;
                        _category.exp_cat_name = _grid.Rows[e.RowIndex].Cells[0].Value.ToStringNullable();
                        using (var xd = new xing())
                        {
                            xd.SingleUpdateCommandALL("acc_expense_cat_tb", new string[] { "exp_cat_name", "exp_cat_id" }, new object[] { _category.exp_cat_name, _category.exp_cat_id }, 1);
                            xd.CommitTransaction();
                        }
                        //update
                    }
                }
                #endregion
            }
            if (((e.RowIndex + 1) == _grid.Rows.Count))
            {
                AddRowX(_grid);
            }
        }
        void fGridCategory_TextBoxKeyDown(object sender, iGTextBoxKeyDownEventArgs e)
        {
            var _grid = sender as iGrid;
            if (e.KeyValue == Keys.Down | e.KeyValue == Keys.Up)
            {
                _grid.CommitEditCurCell();
            }

        }
        private void buttoncancel_Click(object sender, EventArgs e)
        {
            foreach (var j in fGrid.Rows.Cast<iGRow>())
            {
                j.Cells["desc"].Value = null;
                j.Cells["desc"].AuxValue = null;
            }
            fGrid.Rows["item_cat"].Cells[1].AuxValue = null;
            fGrid.Rows["item_cat"].Cells[1].Value = null;
            fGrid.Rows["owner"].Visible = false;
            //
            fGrid.SetCurCell("account", 1);
            fGrid.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddRowX(iGridCategory);
        }
        private bool IsOkay()
        {

            if (fGrid.Rows["account"].Cells["desc"].Value == null)
            {
                dbm.ErrorMessage("Please Enter An Account Name", "Save Error");
                fGrid.Focus();
                fGrid.SetCurCell("account", 1);
                return false;
            }
            if (fGrid.Rows["department"].Cells["desc"].Value == null)
            {
                dbm.ErrorMessage("Please A Department", "Save Error");
                fGrid.Focus();
                fGrid.SetCurCell("department", 1);
                return false;
            }
            return true;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                using (var _fm = new DepartmentMaker2())
                {
                    _fm.ShowDialog();
                    if (_fm.Tag != null)
                    {
                        if (datam.DATA_DEPARTMENT != null)
                        {
                            var nlist = from k in datam.DATA_DEPARTMENT.Values
                                        orderby k.dept_name
                                        select k;
                            var _list = fGrid.Rows["department"].Cells[1].DropDownControl as iGDropDownList;
                            if (_list != null)
                            {
                                _list.Items.Clear();
                                foreach (var k in nlist)
                                {
                                    _list.Items.Add(new fnn.iGComboItemEX()
                                    {
                                        ID = k.dept_id,
                                        Value = k.dept_name,
                                        Tag=k
                                    });
                                }
                            }
                        }
                    }
                }
                fGrid.Focus();
                fGrid.Cells["department", 1].Value = null;
                fGrid.Cells["department", 1].AuxValue = null;
                fGrid.SetCurCell("department", 1);
                return true;
            }
            if (keyData == Keys.F5)
            {
                buttonsave.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void buttonsave_Click(object sender, EventArgs e)
        {
            if (!IsOkay()) { return; }
            using (var xd = new xing())
            {
                var ret_val = xd.ExecuteScalerInt(new string[] { "exp_acc_name" }, string.Format("select count(exp_acc_id) as cnt from acc_expense_accounts_tb where lower(exp_acc_name)=@exp_acc_name"), new object[] { fGrid.Rows["account"].Cells["desc"].Text.Trim().ToLower() });
                if (ret_val > 0)
                {
                    MessageBox.Show("You Have Already Entered This Expense Account", "Duplicate Item Entry");
                    return;
                }
                xd.CommitTransaction();
            }
            if (!dbm.WarningMessage("Are You Sure You Want To Create This Expense Account ??", "Save Warning"))
            {
                return;
            }
            ic.expense_accountC _exp = new ic.expense_accountC();
            _exp.exp_acc_status = em.exp_acc_statusS.valid;
            _exp.exp_acc_name = fGrid.Rows["account"].Cells["desc"].Text.ToProperCase();
            _exp.exp_acc_type = em.exp_acc_typeS.user_defined;
            if(fGrid.Rows["item_cat"].Cells["desc"].AuxValue!=null)
            {
                var _cat = (fGrid.Rows["item_cat"].Cells["desc"].AuxValue as fnn.iGComboItemEX).Tag as ic.expense_catC;
                 _exp.exp_cat_id = _cat.exp_cat_id;
                 _exp.objCategory = _cat;
            }
            if (fGrid.Rows["department"].Cells["desc"].AuxValue != null)
            {
                var _dept = ((fGrid.Rows["department"].Cells["desc"].AuxValue as fnn.iGComboItemEX).Tag as ic.departmentC);
                _exp.dept_id = _dept.dept_id;
                _exp.dept_parent_id = _dept.parent_id;
                _exp.dept_sys_account_id = _dept.expense_sys_account_id;
                _exp.objDepartment = _dept;
            }
            using(var xd=new xing())
            {
                accn.CreateExpenseAccount(_exp, xd);
                xd.CommitTransaction();
            }
            sdata.ClearFormCache(em.fm.chart_of_accounts.ToInt16());
            if (this.Owner is ExpensesManager)
            {
                (this.Owner as ExpensesManager).CreateNewRow(_exp, true);
            }
            this.Tag = 1;
            buttoncancel.PerformClick();
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {

        }

        private void iGridCategory_Click(object sender, EventArgs e)
        {

        }
    }
}
