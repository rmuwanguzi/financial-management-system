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
using SdaHelperManager.Security;


namespace MTOMS
{
    public partial class ExpensesManager : DevComponents.DotNetBar.Office2007Form
    {
        public ExpensesManager()
        {
            InitializeComponent();
        }
        bool app_working = false;
     
        int prev_txtbox_cnt = 0;
        iGDropDownList m_categories { get; set; }
        iGDropDownList m_Dept { get; set; }
       
        SortedList<string, string> m_GroupByList { get; set; }
        string m_group_col { get; set; }
        List<string> m_GroupedRows = new List<string>();
        private void MoveUp(int _index)
        {
            int _val = _index;
            _val--;
            var _col = fGrid.CurCell != null ? fGrid.CurCell.ColIndex : 0;
            while (_val > -1)
            {
                if (fGrid.Rows[_val].Visible)
                {
                    fGrid.CurCell = fGrid.Rows[_val].Cells[_col];
                    break;
                }
                _val--;
            }
        }
        private void MoveDown(int _index)
        {
            int _val = _index;
            _val++;
            var _col = fGrid.CurCell != null ? fGrid.CurCell.ColIndex : 0;
            while (_val < fGrid.Rows.Count)
            {
                if (fGrid.Rows[_val].Visible)
                {
                    fGrid.CurCell = fGrid.Rows[_val].Cells[_col];
                    break;
                }
                _val++;
            }
        }
        List<int> OtherGroups = new List<int>();
        private void MemSearchFilter()
        {
            string _ret = textBoxX1.Text.ToLower();
            if (prev_txtbox_cnt == 0)
            {
                foreach (var r in m_GroupedRows)
                {
                   // if (!fGrid.Rows[r].Expanded) { fGrid.Rows[r].Expanded = true; }
                }
            }
            prev_txtbox_cnt = _ret.Length;
            fGrid.BeginUpdate();
            try
            {
                for (int i = fGrid.Rows.Count - 1; i >= 0; i--)
                {
                    if (fGrid.Rows[i].Type != iGRowType.Normal) { continue; }
                    if (((string)fGrid.Cells[i, "colSearch"].Value).IndexOf(_ret) < 0)
                    {
                        fGrid.Rows[i].Visible = false;
                    }
                    else
                    {
                        fGrid.Rows[i].Visible = true;
                    }
                }
            }
            finally
            {
                fGrid.Cols.AutoWidth();
                fGrid.AutoResizeCols = false;
                fGrid.EndUpdate();
            }
            //  fGrid.SearchAsType.SearchText = _ret;
        }
        private void load_group_context_menus()
        {
            ToolStripMenuItem tool_item = null;
            foreach (var m in m_GroupByList.Keys)
            {
                tool_item = new ToolStripMenuItem();
                tool_item.Text = string.Format("{0}", m);
                tool_item.Font = groupByToolStripMenuItem.Font;
                tool_item.ForeColor = Color.Maroon;
                tool_item.Tag = m;
                tool_item.Click += new EventHandler(tool_item_Click);
                groupByToolStripMenuItem.DropDownItems.Add(tool_item);
                tool_item = null;
            }
        }
        private void filter_grid(string _value)
        {
            if (_value == null)
            {
                fGrid.BeginUpdate();
                foreach (iGRow r in fGrid.Rows)
                {
                    if (!r.Visible) { r.Visible = true; }
                }
                
                fGrid.EndUpdate();
                return;
            }
            fGrid.BeginUpdate();
            foreach (iGRow r in fGrid.Rows)
            {
                if (r.Type == iGRowType.AutoGroupRow)
                {
                    r.Visible = (r.RowTextCell.Text == _value) ? true : false;
                }
                else
                {
                    r.Visible = (r.Cells[m_group_col].Text == _value) ? true : false;
                }
            }
            fGrid.EndUpdate();
        }
        void tool_item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem sent_item = sender as ToolStripMenuItem;
            //
            combofilter.Items.Clear();
            combofilter.Items.Add("-----NONE----");
            filter_grid(null);
           
           
            //
            m_group_col = m_GroupByList[sent_item.Text];
            //OtherGroups.Clear();
            fGrid.GroupObject.Clear();
            fGrid.GroupObject.Add(m_group_col);
            //labelgroup.Text = string.Format("Grouped By : {0}", m_group_col);
            fGrid.Group();
            fGrid.PerformAction(iGActions.CollapseAll);
        }
        private void ItemsManager_Load(object sender, EventArgs e)
        {
            datam.SecurityCheck();
            this.VisibleChanged += new EventHandler(ItemsManager_VisibleChanged);
            fGrid.CurRowChanged += new EventHandler(fGrid_CurRowChanged);
            fGrid.VScrollBarCustomButtonClick += new iGScrollBarCustomButtonClickEventHandler(fGrid_VScrollBarCustomButtonClick);
            fGrid.AfterCommitEdit += new iGAfterCommitEditEventHandler(fGrid_AfterCommitEdit);
            fGrid.SizeChanged += new EventHandler(fGrid_SizeChanged);
            InitializeGridColumns();
            m_GroupByList = new SortedList<string, string>();
            m_GroupByList.Add("Department","dept");
            m_GroupByList.Add("Category","category");
           load_group_context_menus();
            backworker.RunWorkerAsync();
            Application.DoEvents();
                    
            
         }

        void fGrid_SizeChanged(object sender, EventArgs e)
        {
            if (fGrid.Rows.Count > 0)
            {
                fGrid.Cols.AutoWidth();
                fGrid.AutoResizeCols = false;
                fGrid.EndUpdate();
            }
        }

        void fGrid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
        {
            var _cell = fGrid.Cells[e.RowIndex, e.ColIndex];
            var _exp = fGrid.Rows[e.RowIndex].Tag as ic.expense_accountC;
            string _edit_col = null;
            object _edit_val = null;
            switch (_cell.Col.Key)
            {
                case "exp_name":
                    {
                        if (!string.IsNullOrEmpty(_cell.Text))
                        {
                            using (var xd = new xing())
                            {
                                var ret_val = xd.ExecuteScalerInt(new string[] { "exp_acc_name", "exp_acc_id" }, string.Format("select count(exp_acc_id) as cnt from acc_expense_accounts_tb where lower(exp_acc_name)=@exp_acc_name and exp_acc_id<>@exp_acc_id"), new object[] { _cell.Text.Trim().ToLower(), _exp.exp_acc_id });
                                if (ret_val > 0)
                                {
                                    MessageBox.Show("You Have Already Entered This Expense Account Name", "Duplicate Item Entry");
                                    return;
                                }
                                xd.CommitTransaction();
                            }
                        }
                        _exp.exp_acc_name = string.IsNullOrEmpty(_cell.Text) ? _exp.exp_acc_name : _cell.Text.ToProperCase();
                        _edit_col = "exp_acc_name";
                        _edit_val = _exp.exp_acc_name;
                        break;
                    }
                      
                case "dept":
                    {
                        if (_cell.AuxValue != null)
                        {
                            var _dept = ((_cell.AuxValue as fnn.iGComboItemEX).Tag as ic.departmentC);
                            _exp.dept_id = _dept.dept_id;
                            _exp.dept_parent_id = _dept.parent_id;
                            _exp.dept_sys_account_id = _dept.expense_sys_account_id;
                        }
                        else
                        {
                            _cell.Value = _exp.objDepartment.dept_name;
                            return;
                        }
                        _edit_col = "dept_id";
                        _edit_val = _exp.dept_id;
                        break;
                    }
               
                case "category":
                    {
                        _exp.exp_cat_id = (_cell.AuxValue == null) ? 0 : ((_cell.AuxValue as fnn.iGComboItemEX).Tag as ic.expense_catC).exp_cat_id;
                        if (_cell.AuxValue == null)
                        {
                            _cell.Value = null;
                        }
                        if (_exp.exp_cat_id == 0)
                        {
                            _exp.objCategory = null;
                        }
                        else
                        {
                            _exp.objCategory = datam.DATA_EXPENSE_CATEGORY[_exp.exp_cat_id];
                        }
                        _edit_col = "exp_cat_id";
                        _edit_val = _exp.exp_cat_id;
                        sdata.ClearFormCache(em.fm.lcb_periodic_statement.ToInt16());
                        break;
                    }
               
               
               
            }
            using (var xd = new xing())
            {
                if (_edit_col.ToLower() != "dept_id")
                {
                    if (_edit_col == "exp_name")
                    {
                        xd.SingleUpdateCommandALL("acc_expense_accounts_tb", new string[]
                      {
                    _edit_col,"exp_acc_id"},
                            new object[]
                    {
                        _edit_val,
                        _exp.exp_acc_id
                    }, 1);
                        xd.SingleUpdateCommandALL("accounts_tb", new string[] { "account_name", "account_id" }, new object[] { _edit_val.ToProperCase(), _exp.sys_account_id }, 1);
                    }
                    else
                    {
                        xd.SingleUpdateCommandALL("acc_expense_accounts_tb", new string[]
                      {
                    _edit_col,"exp_acc_id"},
                                                  new object[]
                    {
                        _edit_val,
                        _exp.exp_acc_id
                    }, 1);
                    }
                }
                else
                {
                    xd.SingleUpdateCommandALL("acc_expense_accounts_tb", new string[]
                {
                    _edit_col,"dept_parent_id","dept_sys_account_id","exp_acc_id"},
                      new object[]
                    {
                        _edit_val,
                        _exp.dept_parent_id,
                        _exp.dept_sys_account_id,
                        _exp.exp_acc_id
                    }, 1);
                    //
                    xd.SingleUpdateCommandALL("accounts_tb", new string[] { "p_account_id", "account_id" }, new object[] { _exp.dept_sys_account_id, _exp.sys_account_id }, 1);
                }
                xd.CommitTransaction();
            }
           
            EditRow(_exp);
            fGrid.Cols[e.ColIndex].AutoWidth();
        }
        private void UpdateGridWithDBChanges()
        {
            var nlist = from r in datam.DATA_EXPENSE_ACCOUNTS.Values
                        where r.is_updated & r.isActualExpense
                        select r;
            if (nlist.Count() == 0) { return; }
            fGrid.BeginUpdate();
            var _keys = fGrid.Rows.Cast<iGRow>().Where(k => (k.Type == iGRowType.Normal & !string.IsNullOrEmpty(k.Key))).Select(k => k.Key).ToArray<string>();
            iGRow _row=null;
            List<string> _del_keys = new List<string>();
            foreach (var _obj in nlist)
            {
                if (!_keys.Contains(_obj.exp_acc_id.ToString()))
                {
                    if (_obj.exp_acc_status == em.exp_acc_statusS.valid)
                    {
                        CreateNewRow(_obj, false);
                    }
                    continue;
                }
                else
                {
                    if(_obj.exp_acc_status != em.exp_acc_statusS.valid)
                    {
                        _del_keys.Add(_obj.exp_acc_id.ToString());
                    }
                }
                //
                _row = fGrid.Rows[_obj.exp_acc_id.ToString()];
                _row.ReadOnly = iGBool.True;
                _row.Cells["exp_name"].Value = _obj.exp_acc_name.ToProperCase();
                if (_obj.objCategory != null)
                {
                    _row.Cells["category"].Value = _obj.objCategory.exp_cat_name;
                }
                if (_obj.dept_id > 0)
                {
                    _row.Cells["dept"].Value = datam.DATA_DEPARTMENT[_obj.dept_id].dept_name;
                }
                _row.Cells["exp_type"].Value = fnn.GetExpenseTypeName(_obj.exp_acc_type);
                _row.Cells["colSearch"].Value = string.Format("{0} {1} {2} {3}", _obj.exp_acc_name.ToLower(), _row.Cells["exp_type"].Text, _row.Cells["dept"].Text, _row.Cells["category"].Text).ToLower(); ;
              
                _obj.is_updated = false;
            }
            foreach(var j in _del_keys)
            {
                try
                {
                    fGrid.Rows.RemoveAt(fGrid.Rows[j].Index);
                }
                catch(Exception ex)
                {
                    continue;
                }
            }
            List<string> m_grouped = new List<string>();
            foreach (var r in m_GroupedRows)
            {
                try
                {
                    if (fGrid.Rows[r].Expanded) { m_grouped.Add(r); }
                }
                catch (Exception)
                {
                                      
                }
            }
            m_GroupedRows.Clear();
            fGrid.Group();
            foreach (var r in m_GroupedRows)
            {
                try
                {
                    if (m_grouped.IndexOf(r) == -1 & fGrid.Rows[r].Expanded) { fGrid.Rows[r].Expanded = false; }
                }
                catch (Exception)
                {
                  
                }
            }
            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
            fGrid.EndUpdate();
            
           
        }
        void ItemsManager_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible & this.Tag == null) { this.Tag = 1; }
            if (this.Visible & this.Tag !=null)
            {
                using (var _xd = new xing())
                {
                    datam.GetDepartments(_xd);
                    datam.InitExpenses(_xd);
                    _xd.CommitTransaction();
                }
                UpdateGridWithDBChanges();
            }
        }
        #region fgrid
        void fGrid_CurRowChanged(object sender, EventArgs e)
        {
            //if (fGrid.CurRow == null) { return; }
            //if (fGrid.CurRow.Tag == null) { return; }
            //ic.MenuC obj = fGrid.CurRow.Tag as ic.MenuC;
            //if (obj == null) { return; }
            //load_items(obj);
            //lbl_title.Text = string.Empty;
            //lbl_title.Text = string.Format("{0}", fGrid.CurRow.Cells[0].Text, fGrid.CurRow.Cells[1].Text);
        }
        void fGrid_VScrollBarCustomButtonClick(object sender, iGScrollBarCustomButtonClickEventArgs e)
        {
            iGrid fgrid = sender as iGrid;
            if (fgrid == null || fgrid.Rows.Count == 0) { return; }

            if (e.Index == 0)
            {
                //group and expandall
                if (fgrid.GroupObject.Count == 0) { SortAndGroup(fgrid); }
            }
            else if (e.Index == 1)
            {
                //Group and collapse all
                if (fgrid.GroupObject.Count == 0) { SortAndGroup(fgrid); }
            }
            else if (e.Index == 2)
            {
                //UnGroup
             
            }

        }
      
        private void InitializeGridColumns()
        {
            #region Columns To Display
            Dictionary<string, string> Cols = new Dictionary<string, string>();
            Cols.Add("no", "No");
            Cols.Add("exp_name", "Expense Name");
            Cols.Add("exp_type", "Expense Type");
            Cols.Add("dept", "Department");
            Cols.Add("category", "Category");
          
           
          
            iGCol myCol;
            foreach (var c in Cols)
            {
                myCol = fGrid.Cols.Add(c.Key,c.Value);
                myCol.IncludeInSelect = true;
                myCol.SortType = iGSortType.ByValue;
                myCol.ShowWhenGrouped = true;
                myCol.ColHdrStyle.TextAlign = iGContentAlignment.MiddleCenter;
                myCol.ColHdrStyle.ForeColor = Color.Black;
                myCol.ColHdrStyle.BackColor = Color.Thistle;
                myCol.ColHdrStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
                myCol.Width = 70;
                myCol.AllowSizing = true;
            }
           
            fGrid.Cols["exp_type"].CellStyle.Enabled = iGBool.False;
          
            myCol = fGrid.Cols.Add("colSearch", string.Empty);
          
            myCol.Width = 1;
            myCol.Visible = false;
            //
            fGrid.SearchAsType.Mode = iGSearchAsTypeMode.Filter;
            fGrid.SearchAsType.MatchRule = iGMatchRule.Contains;
            fGrid.SearchAsType.SearchCol = fGrid.Cols["colSearch"];

            fGrid.SearchAsType.AutoCancel = false;
            // fGrid.SearchAsType.FilterKeepCurRow = true;
            // The following code lines force the grid
            // not to show the search window.
            fGrid.SearchAsType.DisplayKeyboardHint = false;
            fGrid.SearchAsType.DisplaySearchText = false;
            //
            // Add a special column which will store the category name.
            // This column will be used for grouping.
            fGrid.Cols.Add("section", string.Empty).Visible = false;
            // Add a special column which will store the default 
            // values for the properties.
            fGrid.Cols.Add("svalue", string.Empty).Visible = false;
            myCol = fGrid.Cols["svalue"];
            myCol.SortType = iGSortType.ByValue;



            fGrid.DefaultRow.Height = fGrid.GetPreferredRowHeight(true, false);
            fGrid.DefaultAutoGroupRow.Height = fGrid.DefaultRow.Height;
            fGrid.Cols.AutoWidth();
            #endregion
        }

        public void CreateNewRow(ic.expense_accountC _obj, bool update_mode)
        {
          
            iGRow _row = null;
            fGrid.BeginUpdate();
            _row = fGrid.Rows.Add();
            _row.Font = new Font("georgia", 12, FontStyle.Regular);
            _row.TextAlign = iGContentAlignment.BottomLeft;
            _row.ReadOnly = iGBool.True;
            _row.Cells["exp_name"].Value = _obj.exp_acc_name.ToProperCase();
            if (_obj.objCategory != null)
            {
                _row.Cells["category"].Value = _obj.objCategory.exp_cat_name;
            }
            if ((_obj.dept_id > 0 | _obj.dept_id==-5000))
            {
                _row.Cells["dept"].Value = datam.DATA_DEPARTMENT[_obj.dept_id].dept_name;
            }
            _row.Cells["exp_type"].Value = fnn.GetExpenseTypeName(_obj.exp_acc_type);
            _row.Cells["exp_type"].AuxValue = _obj.exp_acc_type.ToByte();
            _row.Cells["colSearch"].Value = string.Format("{0} {1} {2} {3}", _obj.exp_acc_name.ToLower(), _row.Cells["exp_type"].Text,_row.Cells["dept"].Text,_row.Cells["category"].Text).ToLower(); ;
            _row.Tag = _obj;
            _row.Key = _obj.exp_acc_id.ToStringNullable();
            _row.AutoHeight();
            _row.Height += 2;
            fGrid.EndUpdate();
        }
        public void EditRow(ic.expense_accountC _obj)
        {
            if (_obj == null) { return; }
            fGrid.BeginUpdate();
            iGRow _row = null;
            try
            {
                _row = fGrid.Rows[_obj.exp_acc_id.ToString()];
            }
            catch (Exception)
            {

                 CreateNewRow(_obj,true);
                 fGrid.EndUpdate();
                 return;
             }
            _row.Cells["exp_name"].Value = _obj.exp_acc_name.ToProperCase();
            if (_obj.objCategory != null)
            {
                _row.Cells["category"].Value = _obj.objCategory.exp_cat_name;
            }
            if (_obj.dept_id > 0)
            {
                _row.Cells["dept"].Value = datam.DATA_DEPARTMENT[_obj.dept_id].dept_name;
            }
            _row.Cells["exp_type"].Value = fnn.GetExpenseTypeName(_obj.exp_acc_type);
            _row.Cells["colSearch"].Value = string.Format("{0} {1} {2} {3}", _obj.exp_acc_name.ToLower(), _row.Cells["exp_type"].Text, _row.Cells["dept"].Text, _row.Cells["category"].Text).ToLower(); ;
         
            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
            fGrid.EndUpdate();

        }
        public void expand_row(int section_id)
        {
            if (section_id == -1) { return; }
            var glist = fGrid.Rows.Cast<iGRow>().Where(g => g.Type == iGRowType.AutoGroupRow);
            foreach (var r in glist)
            {
                r.Expanded = false;
            }
            try
            {
                fGrid.Rows[string.Format("GP{0}", section_id)].Expanded = true;
            }
            catch (Exception ex)
            {

            }

        }
        public void load_items()
        {
           
            fGrid.Rows.Clear();
          
            if (datam.DATA_EXPENSE_ACCOUNTS == null || datam.DATA_EXPENSE_ACCOUNTS.Count == 0) { return; }
            iGRow _row = null;
            fGrid.BeginUpdate();
            var nlist = from s in datam.DATA_EXPENSE_ACCOUNTS.Values
                        where s.exp_acc_status == em.exp_acc_statusS.valid & s.isActualExpense
                        select s;
            foreach (var _obj in nlist)
            {
                _row = fGrid.Rows.Add();
                _row.Font = new Font("georgia", 12, FontStyle.Regular);
                _row.TextAlign = iGContentAlignment.BottomLeft;
                _row.ReadOnly = iGBool.True;
                _row.Cells["exp_name"].Value = _obj.exp_acc_name.ToProperCase();
                if (_obj.objCategory != null)
                {
                    _row.Cells["category"].Value = _obj.objCategory.exp_cat_name;
                }
                if (_obj.dept_id > 0 | _obj.dept_id==-5000)
                {
                    _row.Cells["dept"].Value = datam.DATA_DEPARTMENT[_obj.dept_id].dept_name;
                }
                _row.Cells["exp_type"].Value = fnn.GetExpenseTypeName(_obj.exp_acc_type);
                _row.Cells["exp_type"].AuxValue = _obj.exp_acc_type.ToByte();
                _row.Cells["colSearch"].Value = string.Format("{0} {1} {2} {3}", _obj.exp_acc_name.ToLower(), _row.Cells["exp_type"].Text, _row.Cells["dept"].Text, _row.Cells["category"].Text).ToLower(); ;
                _row.Tag = _obj;
                _row.Key = _obj.exp_acc_id.ToStringNullable();
                _row.AutoHeight();
                _row.Height += 2;
                           
            }
            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
            fGrid.EndUpdate();
           
        }
        private void SortAndGroup(iGrid fGrid)
        {
            fGrid.BeginUpdate();
            fGrid.GroupObject.Clear();
            fGrid.SortObject.Clear();
            fGrid.GroupObject.Add("svalue");
            fGrid.SortObject.Add("svalue", iGSortOrder.Ascending);
            fGrid.Group();
            fGrid.EndUpdate();
        }
        
        #endregion
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

                            app_working = false;
                        }
                        break;
                    }

            }
        }
        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            if (app_working) { return; }
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
        private void btnadd_Click(object sender, EventArgs e)
        {
            
            //if (sdata.CURRENT_MENU == null || !sdata.CURRENT_MENU.ContainsRight(0))
            //{
            //    MessageBox.Show("You Are Not Authorized To Carry Out This Operation", "Limited Rights Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            using (var _fm = new Expenses.ExpenseAccountMaker())
            {
                _fm.Owner = this;
                _fm.ShowDialog();
            }
            if (checkBox1.Checked)
            {
                checkBox1.Checked = false;
            }
        }
       
        private void fGrid_AfterContentsSorted(object sender, EventArgs e)
        {
            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
            if (fGrid.Rows.Count > 0)
            {
                fGrid.SetCurCell(0, 0);
            }
        }
         private void ts_delete_Click(object sender, EventArgs e)
        {
            using (var _fm = new SdaHelperManager.ConfirmWPwd())
            {
                _fm.ShowDialog();
                if (_fm.DialogResult != DialogResult.OK)
                {
                    return;
                }
            }
            string _str = "Are You Sure You Want To Delete The Expense Account ??";
              iGRow _row = fGrid.SelectedRows[0];
              var _obj = fGrid.SelectedRows[0].Tag as ic.expense_accountC;
            if (_obj == null) { return; }
            if (dbm.ExecuteScalarInt(string.Format("select TOP 1 exp_acc_id from acc_expense_trans_tb where exp_acc_id={0}", _obj.exp_acc_id)) > 0)
            {
                MessageBox.Show("Cannot Delete This Expense Account Since Has Important Attachements To Other Records Already", "Delete Failure");
                return;
            }
            if (!dbm.WarningMessage(_str, "Delete Warning"))
            {
                return;
            }
            using (var xd = new xing())
            {
                xd.SingleDeleteCommandExp("acc_expense_accounts_tb", new string[]
                {
                    "exp_acc_id","lch_id"
                }, new int[] { _obj.exp_acc_id,datam.LCH_ID });
               
                _row.Tag = null;
                fGrid.Rows.RemoveAt(_row.Index);
                datam.DATA_EXPENSE_ACCOUNTS.Remove(_obj.exp_acc_id);
                //
                xd.SingleDeleteCommandExp("accounts_tb", new string[]
                {
                    "account_id",
                }, new int[] { _obj.sys_account_id});
                //
                datam.DATA_ACCOUNTS.Remove(_obj.sys_account_id);
                 xd.CommitTransaction();
                 sdata.ClearFormCache(em.fm.chart_of_accounts.ToInt16());
            }
        }
         private void LoadEditCombos()
         {
             if (m_categories == null)
             {
                 m_categories = fnn.CreateCombo();
                 m_categories.MaxVisibleRowCount = 5;
             }
            
             if (m_Dept == null)
             {
                 m_Dept = fnn.CreateCombo();
                 m_Dept.MaxVisibleRowCount = 5;
             }
           
             if (m_categories.Items.Count != datam.DATA_EXPENSE_CATEGORY.Keys.Count)
             {
                 m_categories.Items.Clear();
                 foreach (var k in datam.DATA_EXPENSE_CATEGORY.Values)
                 {
                     m_categories.Items.Add(new fnn.iGComboItemEX()
                     {
                         Value = k.exp_cat_name,
                         Text = k.exp_cat_name,
                         Tag = k
                     });
                 }
             }
             if (m_Dept.Items.Count != datam.DATA_DEPARTMENT.Keys.Count)
             {
                 m_Dept.Items.Clear();
                 foreach (var k in datam.DATA_DEPARTMENT.Values)
                 {
                     m_Dept.Items.Add(new fnn.iGComboItemEX()
                     {
                         Value = k.dept_name,
                         Text = k.dept_name,
                         Tag = k
                     });
                 }
             }
         }
         public void CollaspseInvalidSection(int section_id)
         {

         }
         private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
         {
             if (fGrid.SelectedRows.Count == 0 || fGrid.SelectedRows[0].Type==iGRowType.AutoGroupRow)
             {
                 e.Cancel = true;
                 return;
             }
             var _exp = fGrid.SelectedRows[0].Tag as ic.expense_accountC;
             if (_exp != null)
             {
                 ts_delete.Visible = _exp.IsSystemAccount ? false : true;
             }
            
         }

        

         private void printGridToolStripMenuItem_Click(object sender, EventArgs e)
         {
           
         }

         private void fGrid_AfterContentsGrouped(object sender, EventArgs e)
         {
             fGrid.Cols.AutoWidth();
             fGrid.AutoResizeCols = false;
         }

         private void fGrid_AfterRowStateChanged(object sender, iGAfterRowStateChangedEventArgs e)
         {
             fGrid.Cols.AutoWidth();
             fGrid.AutoResizeCols = false;
         }

         private void BtnRefresh_Click(object sender, EventArgs e)
         {
             using (var _xd = new xing())
             {
                 datam.GetDepartments(_xd);
                 datam.InitExpenses(_xd);
                 _xd.CommitTransaction();
             }
             UpdateGridWithDBChanges();
         }
         private void checkBox1_CheckedChanged(object sender, EventArgs e)
         {
             //filter_grid(null);
             //fGrid.GroupObject.Clear();
             //fGrid.Group();
             //combofilter.SelectedIndex = -1;
             //combofilter.Items.Clear();
             if (checkBox1.Checked)
             {
                 checkBox1.Text = "Disable Editing";
                 checkBox1.ForeColor = Color.Maroon;
                 Application.DoEvents();
                 LoadEditCombos();
                 var _edit_cols = new string[] { "exp_name", "dept","category" };
                 fGrid.BeginUpdate();
                 fGrid.Cols["category"].CellStyle.DropDownControl = m_categories;
                 fGrid.Cols["dept"].CellStyle.DropDownControl = m_Dept;
                 foreach (var r in fGrid.Rows.Cast<iGRow>())
                 {
                     foreach (var c in _edit_cols)
                     {
                         r.Cells[c].ReadOnly = iGBool.False;
                         switch ((em.exp_acc_typeS)r.Cells["exp_type"].AuxValue.ToByte())
                         {
                             case em.exp_acc_typeS.system_department:
                                 {
                                    if (c == "dept")
                                    {
                                        r.Cells[c].ReadOnly = iGBool.True;
                                        r.Cells[c].Enabled = iGBool.False;
                                    }
                                    break;
                                 }
                             case em.exp_acc_typeS.system_offertory_payment:
                             case em.exp_acc_typeS.trust_fund:
                                 {
                                     if (c == "exp_name")
                                     {
                                         r.Cells[c].ReadOnly = iGBool.True;
                                         r.Cells[c].Enabled = iGBool.False;
                                     }
                                     break;
                                 }
                         }
                     }
                   
                 }
                 fGrid.RowMode = false;
                 fGrid.Cols.AutoWidth();
                 fGrid.AutoResizeCols = false;
                 fGrid.EndUpdate();
             }
             else
             {
                 checkBox1.Text = "Edit Records";
                 checkBox1.ForeColor = Color.Green;
                 fGrid.BeginUpdate();
                 fGrid.Cols["category"].CellStyle.DropDownControl = null;
                 fGrid.Cols["dept"].CellStyle.DropDownControl = null;
                 var _edit_cols = new string[] { "exp_name", "dept", "category" }; foreach (var r in fGrid.Rows.Cast<iGRow>())
                 {
                     foreach (var c in _edit_cols)
                     {
                         r.Cells[c].ReadOnly = iGBool.True;
                         r.Cells[c].Enabled = iGBool.True;
                     }
                 }
                 fGrid.RowMode = true;
                 fGrid.Cols.AutoWidth();
                 fGrid.AutoResizeCols = false;
                 fGrid.EndUpdate();
             }
         }

         
         

         private void backworker_DoWork(object sender, DoWorkEventArgs e)
         {
             datam.SystemInitializer();
             using (var _xd = new xing())
             {
                 datam.GetDepartments(_xd);
                 datam.InitExpenses(_xd);
                 _xd.CommitTransaction();
             }
           
         }

         private void backworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
         {
             datam.ShowWaitForm();
             Application.DoEvents();
             load_items();
             datam.HideWaitForm();
            
         }
      

         private void buttonAdd_Click(object sender, EventArgs e)
         {

         }

         private void buttonItem4_Click(object sender, EventArgs e)
         {
             using (var _fm = new DefaultReceiptPrinter())
             {
                 _fm.ShowDialog();
             }
         }

         private void fGrid_CellClick(object sender, iGCellClickEventArgs e)
         {
            
         }

         private void fGrid_CellMouseUp(object sender, iGCellMouseUpEventArgs e)
         {
             if (fGrid.Rows.Count > 0)
             {
                 if (fGrid.CurCell == null || fGrid.CurCell.Row.Index != e.RowIndex)
                 {
                     fGrid.SetCurCell(e.RowIndex, e.ColIndex);
                 }
             }
         }

         private void pn_dis_Click(object sender, EventArgs e)
         {

         }
         private void button1_Click(object sender, EventArgs e)
         {
             using (var _fm = new ColFilter())
             {
                 _fm.Tag = fGrid;
                 _fm.ShowDialog();
             }
         }
         private void groupByToolStripMenuItem_Click(object sender, EventArgs e)
         {

         }

        

         private void unGroupByToolStripMenuItem_Click(object sender, EventArgs e)
         {
             combofilter.Items.Clear();
             //labelgroup.Text = "Grouped By : NONE";
             filter_grid(null);
             m_group_col = null;
             OtherGroups.Clear();
             fGrid.GroupObject.Clear();
             fGrid.Group();
             //if (!textBoxX1.Enabled)
             //{
             //    textBoxX1.Enabled = true;
             //}
         }
         private void CollapseOther()
         {
             return;
             if (fGrid.Rows.Count == 0) { return; }
             iGRow _r = null;
             int count = 0;
             fGrid.BeginUpdate();
             foreach (var i in OtherGroups)
             {
                 count = 0;
                 _r = fGrid.Rows[i];
                 if (_r.Type == iGRowType.AutoGroupRow)
                 {
                     count = GetGroupRowCount(m_group_col, _r.RowTextCell.Text);
                     _r.Expanded = false;
                     _r.RowTextCell.Value = string.Format("{0} = {1}", _r.RowTextCell.Text, count);
                 }
             }
             fGrid.EndUpdate();
         }
         private int GetGroupRowCount(string _col, string value)
         {
             var o = (from j in fGrid.Rows.Cast<iGRow>()
                      where j.Visible & j.Cells[_col].Text == value
                      select j).Count();
             return o;
         }
         private void fGrid_AfterAutoGroupRowCreated(object sender, iGAfterAutoGroupRowCreatedEventArgs e)
         {
             iGrid objfgrid = sender as iGrid;
             if (objfgrid == null) { return; }
             iGCell myGroupRowCell = objfgrid.RowTextCol.Cells[e.AutoGroupRowIndex];
             myGroupRowCell.Row.AutoHeight();
             myGroupRowCell.Row.Height = myGroupRowCell.Row.Height + 4;
             iGCell myFirstCellInGroup = objfgrid.Cells[e.GroupedRowIndex, m_group_col];
             myGroupRowCell.Row.Key = string.Format("GP{0}", objfgrid.Cells[e.GroupedRowIndex, "svalue"].Value.ToInt32());
             myGroupRowCell.Row.TextAlign = iGContentAlignment.MiddleLeft;
             myFirstCellInGroup.TextAlign = iGContentAlignment.MiddleCenter;
             myGroupRowCell.Value = string.Format("{0}", myFirstCellInGroup.Value.ToProperCase());
             combofilter.Items.Add(myGroupRowCell.Text);
             m_GroupedRows.Add(myGroupRowCell.Row.Key);
             OtherGroups.Add(e.GroupedRowIndex - 1);
         }

         private void combofilter_SelectedIndexChanged(object sender, EventArgs e)
         {
             if (combofilter.SelectedIndex == 0 | combofilter.SelectedIndex == -1)
             {
                 filter_grid(null);
                 CollapseOther();
                 return;
             }

             filter_grid(combofilter.SelectedItem.ToString());
             ExpandSingle();
         }
         private void ExpandSingle()
         {
             if (fGrid.Rows.Count == 0) { return; }
             iGRow _r = null;
             fGrid.BeginUpdate();
             foreach (var i in OtherGroups)
             {

                 _r = fGrid.Rows[i];
                 if (_r.Type == iGRowType.AutoGroupRow & _r.RowTextCell.Text == combofilter.SelectedItem.ToString())
                 {
                     _r.Expanded = true;
                 }
             }
             ResizeGrid();
             fGrid.EndUpdate();

         }
         private void ResizeGrid()
         {
             fGrid.Cols.AutoWidth();
             fGrid.AutoResizeCols = false;
         }
        
        
        
       
    }
}
