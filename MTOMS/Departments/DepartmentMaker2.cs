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

namespace MTOMS
{
    public partial class DepartmentMaker2 : DevComponents.DotNetBar.Office2007Form
    {
        public DepartmentMaker2()
        {
            InitializeComponent();
        }
        private DevComponents.AdvTree.Node curr_node = null;
        DevComponents.DotNetBar.ElementStyle _header_style = null;
        DevComponents.DotNetBar.ElementStyle _dept_style = null;
        DevComponents.DotNetBar.ElementStyle _sb_dept_style = null;
        DevComponents.DotNetBar.ElementStyle _sb_dept_style2 = null;
        int xx;
        bool is_edited = false;
        enum action_type
        {
            none = 1, create, update, delete
        }
        enum object_type
        {
            none = 1, GroupAccount, Department,SubDepartment
        }
        private action_type m_action = action_type.none;
        private object_type m_object = object_type.none;
        private void DepartMentMaker2_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            datam.SecurityCheck();
            this.FormClosing += new FormClosingEventHandler(DepartmentMaker2_FormClosing);
            textBox1.KeyPress+=new KeyPressEventHandler(textBox1_KeyPress);
            textBox1.TextChanged += new EventHandler(textBox1_TextChanged);
            advTree1.AfterNodeSelect += new DevComponents.AdvTree.AdvTreeNodeEventHandler(advTree1_AfterNodeSelect);
            _header_style = new DevComponents.DotNetBar.ElementStyle();
            _header_style.TextColor = Color.Maroon;
            _header_style.Font = new Font("georgia", 14, FontStyle.Regular);
            //
            _dept_style = new DevComponents.DotNetBar.ElementStyle();
           // _dept_style.TextColor = Color.Blue;
            _dept_style.TextColor = Color.FromArgb(64, 64, 64);
            _dept_style.Font = new Font("georgia", 13, FontStyle.Regular);
            //
            _sb_dept_style = new DevComponents.DotNetBar.ElementStyle();
            _sb_dept_style.TextColor = Color.Blue;
            _sb_dept_style.Font = new Font("verdana", 11, FontStyle.Regular);
            //
            _sb_dept_style2 = new DevComponents.DotNetBar.ElementStyle();
            _sb_dept_style2.TextColor = Color.Purple;
            _sb_dept_style2.Font = new Font("verdana", 10, FontStyle.Regular);
            datam.GetDepartments(null);
            LoadTree();
        }

        void DepartmentMaker2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (is_edited)
            {
                this.Tag = 1;
            }
        }

        void advTree1_AfterNodeSelect(object sender, DevComponents.AdvTree.AdvTreeNodeEventArgs e)
        {
            curr_node = e.Node;
            if (curr_node == null) { return; }
          
            if (curr_node.Level == 0)
            {
               
                return;
            }
            if (curr_node.Tag != null)
            {
              
            }
        }

        void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Length == 0)
            {
                buttonadd.Enabled = false;
                return;
            }
            if (m_action == action_type.create)
            {
                if (textBox1.TextLength == 0)
                {
                    buttonadd.Enabled = false;
                    return;
                }

                if (buttonadd.Enabled == false) { buttonadd.Enabled = true; }
            }
            if (m_action == action_type.update)
            {
                if (textBox1.TextLength == 0)
                {
                    buttonadd.Enabled = false;
                    return;
                }
                if (textBox1.TextLength > 0 && textBox1.Text != curr_node.Text)
                {
                    if (buttonadd.Enabled == false)
                    {
                        buttonadd.Enabled = true;

                    }
                }
                if (textBox1.Text == curr_node.Text)
                { if (buttonadd.Enabled == true) { buttonadd.Enabled = false; } }

            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.ToByte() == (byte)Keys.Enter & buttonadd.Enabled)
            {
                buttonadd.PerformClick();
            }
        }
        private void LoadTree()
        {
            DevComponents.AdvTree.Node _nd = null;
            foreach (var c in datam.DEPARTMENT_INNER_COLLECTION)
            {
                _nd = new DevComponents.AdvTree.Node();
                _nd.Text = c.Value;
                _nd.Tag = c.Key;
                _nd.Name = string.Format("MDEPT{0}", c.Key);
                _nd.Style = _dept_style;
                _nd.ContextMenu = contextMenuFolder;
                advTree1.Nodes.Add(_nd); _nd = null;

            }
            var nlist = from k in datam.DATA_DEPARTMENT.Values
                        where k.is_visible & k.dept_type==em.dept_typeS.main
                        group k by k.parent_id into new_group
                        select new
                        {
                            _key = new_group.Key,
                            gp_data = new_group
                        };
            DevComponents.AdvTree.Node _parent = null;
            foreach (var k in nlist)
            {
                 _parent = null;
                _parent = advTree1.FindNodeByName(string.Format("MDEPT{0}", k._key));
                foreach (var sd in k.gp_data)
                {
                    _nd = new DevComponents.AdvTree.Node();
                    _nd.Text = sd.dept_name;
                    _nd.Tag = sd;
                    _nd.Name = string.Format("DEPT{0}", sd.dept_id);
                    _nd.Style = _sb_dept_style;
                    if (sd.dept_id != -5000)
                    {
                        _nd.ContextMenu = contextMenuFile;
                    }
                    _parent.Nodes.Add(_nd); _nd = null;
                 }
                if (_parent != null)
                {
                    _parent.Expand();
                }
            }
            nlist = from k in datam.DATA_DEPARTMENT.Values
                    where k.is_visible & k.dept_type == em.dept_typeS.sub
                    group k by k.parent_id into new_group
                    select new
                    {
                        _key = new_group.Key,
                        gp_data = new_group
                    };
            foreach (var k in nlist)
            {
                _parent = null;
                _parent = advTree1.FindNodeByName(string.Format("DEPT{0}", k._key));
                foreach (var sd in k.gp_data)
                {
                    _nd = new DevComponents.AdvTree.Node();
                    _nd.Text = sd.dept_name;
                    _nd.Tag = sd;
                    _nd.Name = string.Format("DEPT{0}", sd.dept_id);
                    _nd.Style = _sb_dept_style2;
                    _nd.ContextMenu = contextMenuFile;
                    _parent.Nodes.Add(_nd); _nd = null;
                }
                if (_parent != null)
                {
                    _parent.Expand();
                }
            }

        }
        private void RecursiveFill(DevComponents.AdvTree.Node _p_node, int parent_id)
        {
            var nlist = from k in datam.DATA_DEPARTMENT.Values
                        where k.parent_id == parent_id
                        orderby k.index
                        select k;
            DevComponents.AdvTree.Node _nd = null;
            foreach (var k in nlist)
            {
                _nd = new DevComponents.AdvTree.Node();
                _nd.Text = k.dept_name;
                _nd.Style = _sb_dept_style;
                _nd.ContextMenu = contextMenuFile;
                _nd.Tag = k;
                _p_node.Nodes.Add(_nd);
                _nd = null;
            }

        }
        public void showpanel(DevComponents.AdvTree.Node tnode)
        {

            if (m_action == action_type.update)
            {
                panel2.Top = (tnode.Bounds.Bottom + advTree1.NodeSpacing + tnode.Bounds.Height) - 5;
            }
            else
            {
                panel2.Top = (tnode.Bounds.Bottom + advTree1.NodeSpacing + tnode.Bounds.Height) + 8;
            }

            panel2.Left = advTree1.Left + (tnode.Bounds.Left-1);
            panel2.Visible = true;
            panel2.Enabled = true;
            textBox1.Clear();
            textBox1.Focus();
            buttonadd.Enabled = false;
            switch (m_action)
            {
                case action_type.create:
                    {
                        buttonadd.Text = "Save";
                        break;
                    }
                case action_type.update:
                    {
                        textBox1.Text = curr_node.Text;
                        textBox1.SelectionStart = curr_node.Text.Length;
                        textBox1.SelectionLength = curr_node.Text.Length - 1;

                        buttonadd.Text = "Edit";
                        break;
                    }
            }
            advTree1.Enabled = false;
        }
        public void CreateDummyNode()
        {
            DevComponents.AdvTree.Node temp_node = new DevComponents.AdvTree.Node();
            temp_node.Text = "_" + xx++ + "_" + (char)190;
            curr_node.Nodes.Add(temp_node);
            advTree1.SelectedNode = temp_node;
            temp_node.Style = _sb_dept_style;
            showpanel(advTree1.SelectedNode);
            curr_node = advTree1.SelectedNode;
        }
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            m_action = action_type.create;
            m_object = object_type.Department;
            CreateDummyNode();
        }

        private void buttonclose_Click(object sender, EventArgs e)
        {
            switch (m_action)
            {
                case action_type.create:
                    {
                        advTree1.SelectedNode.Remove();
                        break;
                    }
                case action_type.update:
                    {
                        break;
                    }
            }
            panel2.Visible = false;
            advTree1.Enabled = true;
            m_action = action_type.none;
        }

        private void buttonadd_Click(object sender, EventArgs e)
        {
            if (m_action == action_type.create)
            {
                #region Insert Region
                ic.departmentC _dept = new MTOMS.ic.departmentC();
                _dept.dept_name = textBox1.Text.Trim().ToProperCase();
                if (curr_node.Level == 1)
                {
                    _dept.parent_id = curr_node.Parent.Tag == null ? 0 : curr_node.Parent.Tag.ToInt32();
                }
                if (curr_node.Level == 2)
                {
                    _dept.parent_id = curr_node.Parent.Tag == null ? 0 : (curr_node.Parent.Tag as ic.departmentC).dept_id;
                }
                _dept.is_visible = true;
                _dept.level = curr_node.Level;
                _dept.index = curr_node.Index;
                string[] _cols = new string[]
           {
               "exp_type",
               "is_visible",
               "lch_id",
               "fs_time_stamp",
               "dept_name",
               "parent_id","s_level","s_index"
           };
                object[] _row = new object[]
                {
                    emm.export_type.insert.ToByte(),
                    1,
                   sdata.ChurchID,
                   0,
                   _dept.dept_name,
                   _dept.parent_id,_dept.level,_dept.index
                };
                using (var xd = new xing())
                {
                   
                    if (datam.DuplicateDepartmentName(_dept.dept_name, xd))
                    {
                        MessageBox.Show("The Department Name You Have Entered Already Exists", "Duplicate Department Name");
                        buttonclose.PerformClick();
                        return;
                    }
                    _dept.dept_id = xd.SingleInsertCommandTSPInt("dept_master_tb_ns", _cols, _row);
                    curr_node.Tag = _dept;
                    curr_node.Text = _dept.dept_name;
                    curr_node.Name = string.Format("DEPT{0}", _dept.dept_id);
                    if (_dept.parent_id > 0)
                    {
                        _dept.expense_sys_account_id = accn.CreateChildGroupAccount(xd, datam.DATA_DEPARTMENT[_dept.parent_id].expense_sys_account_id, _dept.dept_name).account_id;
                        _dept.cr_sys_account_id = datam.DATA_DEPARTMENT[_dept.parent_id].cr_sys_account_id;
                        _dept.income_sys_account_id = datam.DATA_DEPARTMENT[_dept.parent_id].income_sys_account_id;
                    }
                    else
                    {
                        _dept.expense_sys_account_id = accn.CreateChildGroupAccount(xd, -2386, _dept.dept_name).account_id;
                        _dept.income_sys_account_id = accn.CreateChildGroupAccount(xd, -2370, _dept.dept_name).account_id;
                        _dept.cr_sys_account_id = accn.CreateChildGroupAccount(xd, -2369, _dept.dept_name).account_id;
                        xd.UpdateFsTimeStamp("accounts_tb");
                        xd.SingleUpdateCommand(string.Format("update accounts_tb set link_id={0},{1},fs_time_stamp={2} where account_id in ({3},{4},{5})", _dept.expense_sys_account_id,
                            dbm.ETS, SQLH.UnixStamp, _dept.expense_sys_account_id, _dept.income_sys_account_id, _dept.cr_sys_account_id));

                    }
                    //
                    xd.SingleUpdateCommandALL("dept_master_tb_ns", new string[]
                             {
                                 "sys_account_id",
                                 "inc_sys_account_id",
                                 "cr_sys_account_id",
                                 "dept_id"
                             }, new object[] { _dept.expense_sys_account_id,_dept.income_sys_account_id,_dept.cr_sys_account_id, _dept.dept_id }, 1);
                    //
                    ic.expense_accountC _exp = new ic.expense_accountC();
                    _exp.dept_id = _dept.dept_id;
                    _exp.dept_parent_id = _dept.parent_id;
                    _exp.dept_sys_account_id = _dept.expense_sys_account_id;
                    _exp.exp_acc_name = string.Format("GE :: {0}", _dept.dept_name);
                    _exp.exp_acc_status = em.exp_acc_statusS.valid;
                    _exp.exp_acc_type = em.exp_acc_typeS.system_department;
                    datam.DATA_DEPARTMENT.Add(_dept.dept_id, _dept);
                    accn.CreateExpenseAccount(_exp, xd);
                    if (_dept.parent_id < 0)
                    {
                        #region
                        ic.accountC _inc_dept = new MTOMS.ic.accountC();
                        _inc_dept.account_name = string.Format("GI :: {0}", _dept.dept_name);
                        _inc_dept.account_status = em.account_statusS.Enabled;
                        _inc_dept.opening_balance = 0;
                        _inc_dept.start_date = sdata.CURR_DATE;
                        _inc_dept.owner_type = em.AccountOwnerTypeS.DEPARTMENT;
                        _inc_dept.owner_id = _dept.dept_id;
                        _inc_dept.owner_name = _dept.dept_name;
                        _inc_dept.PostType = em.postTypeS.cash_accounts_payable;

                        ic.accountC parent_account = datam.DATA_ACCOUNTS[_dept.income_sys_account_id];
                        _inc_dept.account_dept_type = parent_account.account_dept_type;
                        _inc_dept.account_dept_category = parent_account.account_dept_category;
                        _inc_dept.account_status = em.account_statusS.Enabled;
                        _inc_dept.account_type = em.account_typeS.ActualAccount;
                        _inc_dept.p_account_id = parent_account.account_id;
                        _inc_dept.a_level = (parent_account.a_level + 1).ToInt16();
                        _inc_dept.a_index = ((datam.DATA_ACCOUNTS.Values.Count(l => l.p_account_id == parent_account.account_id & l.account_type == em.account_typeS.ActualAccount)) + 1).ToInt16();
                         _cols = new string[]
                {
                    "acc_d_cat_id",
                    "acc_d_type_id",
                    "account_name",
                    "account_type_id",
                    "account_alias",
                    "a_level",
                    "a_index",
                    "p_account_id",
                    "exp_type",
                    "fs_time_stamp",
                    "edate",
                    "pc_us_id",
                    "account_status_id",
                    "search_alias",
                    "is_sys_account",
                    "post_type_id",
                    "owner_type_id",
                    "owner_id",
                    "owner_name",
                    "start_date",
                    "end_date","opening_balance",
                    "account_short_name","account_code","description","lch_id","ex_cg_type_ids","accounts_ext_purpose"
                    
                };

                 xd.UpdateFsTimeStamp("accounts_tb");
                _row = new object[]
                {
                    _inc_dept.account_dept_category.ToByte(),
                    _inc_dept.account_dept_type.ToByte(),
                    _inc_dept.account_name,
                    _inc_dept.account_type.ToByte(),
                    _inc_dept.account_alias,
                    _inc_dept.a_level,
                    _inc_dept.a_index,
                    _inc_dept.p_account_id,
                    emm.export_type.insert.ToByte(),
                    0,
                    datam.CURR_DATE,
                    datam.PC_US_ID,
                    _inc_dept.account_status.ToByte(),
                    null,//search_alias
                    0, // is_sys_account
                    _inc_dept.PostType.ToByte(),//post_type
                    _inc_dept.owner_type.ToByte(),//owner_type
                    _inc_dept.owner_id,//owner_id
                    _inc_dept.owner_name,//owner name
                    _inc_dept.start_date,
                    _inc_dept.end_date,//end_date
                    _inc_dept.opening_balance,//opening balance
                    _inc_dept.account_short_name,//account_short_name
                    _inc_dept.account_code,
                    _inc_dept.description,//description
                    sdata.ChurchID,null,_inc_dept.extension_purpose.ToByte()
                };
                _inc_dept.account_id = xd.SingleInsertCommandTSPInt("accounts_tb", _cols, _row);
                xd.UpdateFsTimeStamp("accounts_tb");
                xd.SingleUpdateCommand(string.Format("update accounts_tb set link_id={0},{1},fs_time_stamp={2} where account_id in ({3},{4})", _dept.expense_sys_account_id,
                    dbm.ETS, SQLH.UnixStamp, _inc_dept.account_id, _exp.sys_account_id));

                        if (datam.DATA_ACCOUNTS != null)
                        {
                            try
                            {
                                datam.DATA_ACCOUNTS.Add(_inc_dept.account_id, _inc_dept);
                            }
                            catch (Exception)
                            {

                            }
                        }


                        #endregion
                    }
                    switch (curr_node.Level)
                    {
                        case 1:
                            {
                                curr_node.Style = _sb_dept_style;
                                break;
                            }
                        case 2:
                            {
                                curr_node.Style = _sb_dept_style2;
                                break;
                            }
                    }
                    curr_node.ContextMenu = contextMenuFile;
                    xd.CommitTransaction();
                }
                #endregion
              
                _dept = null;
                m_action = action_type.none;
                if (!is_edited) { is_edited = true; }
                sdata.ClearFormCache(em.fm.expense_account_settings.ToInt16());
                sdata.ClearFormCache(em.fm.chart_of_accounts.ToInt16());
                //
                sdata.ClearFormCache(em.fm.income_accounts_settings.ToInt16());
                sdata.ClearFormCache(em.fm.creditors.ToInt16());
                buttonclose.PerformClick();
            }
            if (m_action == action_type.update)
            {
                if (curr_node != null && curr_node.Tag != null)
                {
                    var _dept = curr_node.Tag as ic.departmentC;
                    if (_dept != null)
                    {
                        using (var xd = new xing())
                        {
                            if (datam.DuplicateDepartmentName(textBox1.Text.Trim().ToProperCase(), xd, _dept.dept_id))
                            {
                                MessageBox.Show("The Department Name You Have Entered Already Exists", "Duplicate Department Name");
                                buttonclose.PerformClick();
                                return;
                            }
                            xd.SingleUpdateCommandALL("dept_master_tb_ns", new string[] { "dept_name", "dept_id" }, new object[] { textBox1.Text.Trim().ToProperCase(), _dept.dept_id }, 1);
                            xd.SingleUpdateCommandALL("accounts_tb", new string[] { "account_name", "account_id" }, new object[] { textBox1.Text.Trim().ToProperCase(), _dept.expense_sys_account_id }, 1);
                            xd.CommitTransaction();
                        }
                        _dept.dept_name = textBox1.Text.Trim().ToProperCase();
                        curr_node.Text = _dept.dept_name;
                        if (!is_edited) { is_edited = true; }
                        sdata.ClearFormCache(em.fm.expense_account_settings.ToInt16());
                        sdata.ClearFormCache(em.fm.chart_of_accounts.ToInt16());
                        buttonclose.PerformClick();
                    }
                }

            }
        }

        private void editAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_action = action_type.update;
            m_object = object_type.GroupAccount;
            showpanel(advTree1.SelectedNode);
        }

        private void deleteClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var _dept = curr_node.Tag as ic.departmentC;
            string _str = "Are You Sure You Want To The Delete The Selected Department";
            
            using (var xd = new xing())
            {
                datam.InitAccount(xd);
                if (xd.ExecuteScalarInt(string.Format("select TOP 1 count(dept_id) from dept_member_tb where dept_id={0}", _dept.dept_id)) > 0)
                {
                    dbm.ErrorMessage("You Cannot Delete This Department,It Has References In Other Tables", "Delete Failure");
                    return;
                }
                //
                if (xd.ExecuteScalarInt(string.Format("select TOP 1 count(dept_id) from acc_expense_trans_tb where dept_id={0}", _dept.dept_id)) > 0)
                {
                    dbm.ErrorMessage("You Cannot Delete This Department,It Has References In Other Tables", "Delete Failure");
                    return;
                }
                if (xd.ExecuteScalarInt(string.Format("select TOP 1 count(dept_id) from acc_expense_accounts_tb where dept_id={0}", _dept.dept_id)) > 1)
                {
                    dbm.ErrorMessage("You Cannot Delete This Department,It Has References In Other Tables", "Delete Failure");
                    return;
                }
                List<int> _child_accounts = new List<int>();
                if (_dept.parent_id > 0)
                {
                    if (_dept.cr_sys_account_id > 0)
                    {
                        string _int_str = null;
                        var _ac1 = accn.GetChildAccounts(_dept.cr_sys_account_id, em.account_typeS.ActualAccount);
                        foreach (var _p in _ac1)
                        {
                            _child_accounts.Add(_p.account_id);
                            if (string.IsNullOrEmpty(_int_str))
                            {
                                _int_str = "(" + _p.account_id.ToStringNullable();
                            }
                            else
                            {
                                _int_str += string.Format(",{0}", _p.account_id);
                            }

                        }
                        if (!string.IsNullOrEmpty(_int_str))
                        {
                            _int_str += ")";
                            if (xd.ExecuteScalarInt(string.Format("select count(account_id) from journal_year_tb where account_id in {0}", _int_str)) > 0)
                            {
                                dbm.ErrorMessage("You Cannot Delete This Department,It Has References In Other Tables", "Delete Failure");
                                return;
                            }
                        }
                    }
                    if (_dept.expense_sys_account_id > 0)
                    {
                        string _int_str = null;
                        var _ac1 = accn.GetChildAccounts(_dept.expense_sys_account_id, em.account_typeS.ActualAccount);
                        foreach (var _p in _ac1)
                        {
                            _child_accounts.Add(_p.account_id);
                            if (string.IsNullOrEmpty(_int_str))
                            {
                                _int_str = "(" + _p.account_id.ToStringNullable();
                            }
                            else
                            {
                                _int_str += string.Format(",{0}", _p.account_id);
                            }

                        }
                        if (!string.IsNullOrEmpty(_int_str))
                        {
                            _int_str += ")";
                            if (xd.ExecuteScalarInt(string.Format("select count(account_id) from journal_year_tb where account_id in {0}", _int_str)) > 0)
                            {
                                dbm.ErrorMessage("You Cannot Delete This Department,It Has References In Other Tables", "Delete Failure");
                                return;
                            }
                        }
                    }
                    if (_dept.income_sys_account_id > 0)
                    {
                        string _int_str = null;
                        var _ac1 = accn.GetChildAccounts(_dept.income_sys_account_id, em.account_typeS.ActualAccount);
                        foreach (var _p in _ac1)
                        {
                            _child_accounts.Add(_p.account_id);
                            if (string.IsNullOrEmpty(_int_str))
                            {
                                _int_str = "(" + _p.account_id.ToStringNullable();
                            }
                            else
                            {
                                _int_str += string.Format(",{0}", _p.account_id);
                            }

                        }
                        if (!string.IsNullOrEmpty(_int_str))
                        {
                            _int_str += ")";
                            if (xd.ExecuteScalarInt(string.Format("select count(account_id) from journal_year_tb where account_id in {0}", _int_str)) > 0)
                            {
                                dbm.ErrorMessage("You Cannot Delete This Department,It Has References In Other Tables", "Delete Failure");
                                return;
                            }
                        }
                    }
                }
                            
                if (!dbm.WarningMessage(_str, "Delete Warning"))
                {
                    return;
                }
                //
                xd.SingleDeleteCommandExp("dept_master_tb_ns", new string[]
                             {
                                 "dept_id"
                             }, new int[] { _dept.dept_id });
                //
                xd.SingleDeleteCommandExp("acc_expense_accounts_tb", new string[]
                             {
                                 "dept_id"
                             }, new int[] { _dept.dept_id });
                //
                List<int> _keys = datam.DATA_EXPENSE_ACCOUNTS.Values.Where(d => d.dept_id == _dept.dept_id).Select(k => k.exp_acc_id).ToList();
                foreach (var s in _keys)
                {
                    datam.DATA_EXPENSE_ACCOUNTS.Remove(s);
                }
                //
                _child_accounts.Add(_dept.income_sys_account_id);
                _child_accounts.Add(_dept.expense_sys_account_id);
                _child_accounts.Add(_dept.cr_sys_account_id);

                foreach(var _id in _child_accounts)
                {
                    xd.SingleDeleteCommandExp("accounts_tb", new string[]
                             {
                                 "account_id"
                             }, new int[] { _id });
                    datam.DATA_ACCOUNTS.Remove(_id);
                }
                _keys = datam.DATA_ACCOUNTS.Values.Where(d => (d.account_id == _dept.expense_sys_account_id | d.account_id == _dept.income_sys_account_id | d.account_id == _dept.cr_sys_account_id)).Select(k => k.account_id).ToList();
                foreach (var s in _keys)
                {
                    datam.DATA_ACCOUNTS.Remove(s);
                }
                xd.CommitTransaction();

            }
            if (!is_edited) { is_edited = true; }
            datam.DATA_DEPARTMENT.Remove(_dept.dept_id);
            advTree1.SelectedNode.Remove();
            sdata.ClearFormCache(em.fm.expense_account_settings.ToInt16());
            sdata.ClearFormCache(em.fm.chart_of_accounts.ToInt16());
            sdata.ClearFormCache(em.fm.income_accounts_settings.ToInt16());
            sdata.ClearFormCache(em.fm.creditors.ToInt16());
        }

        private void createSubDepartmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_action = action_type.create;
            m_object = object_type.Department;
            CreateDummyNode();
        }

        private void contextMenuFile_Opening(object sender, CancelEventArgs e)
        {
            if (curr_node != null)
            {
                deleteClientToolStripMenuItem.Visible = curr_node.Nodes.Count > 0 ? false : true;
                createSubDepartmentToolStripMenuItem.Visible = curr_node.Level > 1 ? false : true;
            }
        }

        private void contextMenuFolder_Opening(object sender, CancelEventArgs e)
        {

        }

        private void createSubDepartmentToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            m_action = action_type.create;
            m_object = object_type.SubDepartment;
            CreateDummyNode();
        }
    }
}
