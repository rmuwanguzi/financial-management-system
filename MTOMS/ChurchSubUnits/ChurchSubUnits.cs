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
    public partial class ChurchSubUnits : DevComponents.DotNetBar.Office2007Form
    {
        public ChurchSubUnits()
        {
            InitializeComponent();
        }
        private DevComponents.AdvTree.Node curr_node = null;
        DevComponents.DotNetBar.ElementStyle _header_style = null;
        DevComponents.DotNetBar.ElementStyle _dept_style = null;
        DevComponents.DotNetBar.ElementStyle _sb_dept_style = null;
        int xx;
        bool is_edited = false;
        enum action_type
        {
            none = 1, create, update, delete
        }
        enum object_type
        {
            none = 1, GroupAccount, Account
        }
        private action_type m_action = action_type.none;
        private object_type m_object = object_type.none;
        private SortedList<int, string> m_categories = null;
        private void ChurchSubUnits_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            datam.SecurityCheck();
            datam.SystemInitializer();
            this.FormClosing += new FormClosingEventHandler(ChurchSubUnits_FormClosing);
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
            m_categories = new SortedList<int, string>();
            m_categories.Add(1, "Sabbath School");
            m_categories.Add(2, "Company");
            m_categories.Add(3, "Hospital");
            m_categories.Add(4, "Clinic");
            m_categories.Add(5, "Primary School");
            m_categories.Add(6, "Secondary School");
            m_categories.Add(7, "Insititution");
            m_categories.Add(8, "University");
            LoadTree();
        }

        void ChurchSubUnits_FormClosing(object sender, FormClosingEventArgs e)
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
            foreach (var c in m_categories)
            {
                _nd = new DevComponents.AdvTree.Node();
                _nd.Text = c.Value;
                _nd.Tag = c.Key;
                _nd.Name = string.Format("MDEPT{0}", c.Key);
                _nd.Style = _header_style;
                _nd.ContextMenu = contextMenuFolder;
                advTree1.Nodes.Add(_nd); _nd = null;

            }
            var nlist = from k in datam.DATA_CHURCH_SUB_UNIT.Values
                        group k by k.sb_unit_category.ToByte() into new_group
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
                    _nd.Text = sd.sb_unit_name;
                    _nd.Tag = sd;
                    _nd.Name = string.Format("DEPT{0}", sd.sb_unit_id);
                    _nd.Style = _sb_dept_style;
                    _nd.ContextMenu = contextMenuFile;
                    _parent.Nodes.Add(_nd); _nd = null;
                 }
                if (_parent != null)
                {
                    _parent.Expand();
                }
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
            m_object = object_type.Account;
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
                ic.church_sub_unitC _unit = new MTOMS.ic.church_sub_unitC();
                _unit.sb_unit_name = textBox1.Text.Trim().ToProperCase();
                _unit.sb_unit_category = (em.sb_unit_categoryS)curr_node.Parent.Tag.ToByte();
                string[] _cols = new string[]
           {
               "sb_unit_name",
               "sb_unit_cat_id",
               "exp_type",
               "fs_time_stamp",
               "lch_id",
               
           };
                object[] _row = new object[]
                {
                    _unit.sb_unit_name,
                    _unit.sb_unit_category.ToByte(),
                    33,
                    0,
                   sdata.App_station_id,
                   };
                using (var xd = new xing())
                {
                    //if (datam.DuplicateDepartmentName(_unit.dept_name, xd))
                    //{
                    //    MessageBox.Show("The Department Name You Have Entered Already Exists", "Duplicate Department Name");
                    //    buttonclose.PerformClick();
                    //    return;
                    //}
                    _unit.sb_unit_id = xd.SingleInsertCommandTSPInt("church_sub_unit_tb", _cols, _row);
                    curr_node.Tag = _unit;
                    curr_node.Text = _unit.sb_unit_name;
                    curr_node.Name = string.Format("DEPT{0}", _unit.sb_unit_id);
                    switch (curr_node.Level)
                    {
                        case 1:
                            {
                                curr_node.Style = _sb_dept_style;
                                break;
                            }
                        case 2:
                            {
                                curr_node.Style = _sb_dept_style;
                                break;
                            }
                    }
                    curr_node.ContextMenu = contextMenuFile;
                    accn.CreateSubUnitCrExpAccount(_unit, xd);
                    xd.CommitTransaction();
                }
                #endregion
                 datam.DATA_CHURCH_SUB_UNIT.Add(_unit.sb_unit_id, _unit);
                _unit = null;
                m_action = action_type.none;
                if (!is_edited) { is_edited = true; }
                buttonclose.PerformClick();
            }
            if (m_action == action_type.update)
            {
                if (curr_node != null && curr_node.Tag != null)
                {
                    var _unit = curr_node.Tag as ic.church_sub_unitC;
                    if (_unit != null)
                    {
                        using (var xd = new xing())
                        {
                            //if (datam.DuplicateDepartmentName(textBox1.Text.Trim().ToProperCase(), xd, _dept.dept_id))
                            //{
                            //    MessageBox.Show("The Department Name You Have Entered Already Exists", "Duplicate Department Name");
                            //    buttonclose.PerformClick();
                            //    return;
                            //}
                            xd.SingleUpdateCommandALL("church_sub_unit_tb", new string[] { "sb_unit_name", "sb_unit_id" }, new object[] { textBox1.Text.Trim().ToProperCase(), _unit.sb_unit_id }, 1);
                            xd.CommitTransaction();
                        }
                        _unit.sb_unit_name = textBox1.Text.Trim().ToProperCase();
                        curr_node.Text = _unit.sb_unit_name;
                        if (!is_edited) { is_edited = true; }
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
            MessageBox.Show("You Cannot Perform This Operation At The Moment", "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
            var _unit = curr_node.Tag as ic.church_sub_unitC;
            string _str = "Are You Sure You Want To The Delete The Selected Expense Account";
            
            using (var xd = new xing())
            {
                //if (xd.ExecuteScalarInt(string.Format("select count(dept_id) from dept_member_tb where dept_id={0} limit 1", _dept.dept_id)) > 0)
                //{
                //    dbm.ErrorMessage("You Cannot Delete This Department,It Has References In Other Tables", "Delete Failure");
                //    return;
                //}
                if (!dbm.WarningMessage(_str, "Delete Warning"))
                {
                    return;
                }
                xd.SingleDeleteCommandExp("church_sub_unit_tb", new string[]
                             {
                                 "sb_unit_id"
                             }, new int[] { _unit.sb_unit_id });
                xd.CommitTransaction();

            }
            if (!is_edited) { is_edited = true; }
            datam.DATA_CHURCH_SUB_UNIT.Remove(_unit.sb_unit_id);
            advTree1.SelectedNode.Remove();             
        }

        private void createSubDepartmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_action = action_type.create;
            m_object = object_type.Account;
            CreateDummyNode();
        }

        private void contextMenuFile_Opening(object sender, CancelEventArgs e)
        {
            if (curr_node != null)
            {
                deleteClientToolStripMenuItem.Visible = curr_node.Nodes.Count > 0 ? false : true;
                
            }
        }

        private void contextMenuFolder_Opening(object sender, CancelEventArgs e)
        {
            toolStripMenuItem2.Text = "Create " + advTree1.SelectedNode.Text;
        }
    }
}
