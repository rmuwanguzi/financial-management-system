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
    public partial class COAccounts : DevComponents.DotNetBar.Office2007Form
    {
        public COAccounts()
        {
            InitializeComponent();
        }
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
        private DevComponents.AdvTree.Node curr_node = null;
        DevComponents.DotNetBar.ElementStyle _folder_style = null;
        DevComponents.DotNetBar.ElementStyle _file_style = null;
        DevComponents.DotNetBar.ElementStyle _disabled = null;
        int xx;
        ic.accountC m_account = null;
        List<int> LCB_ACCOUNT { get; set; }
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
            myCol.SortType = iGSortType.None;
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
        public void CreateDummyNode()
        {
            DevComponents.AdvTree.Node temp_node = new DevComponents.AdvTree.Node();
            temp_node.Text = "_" + xx++ + "_" + (char)190;
            curr_node.Nodes.Add(temp_node);
            advTree1.SelectedNode = temp_node;
            labelclient.Text = string.Empty;
            showpanel(advTree1.SelectedNode);
            curr_node = advTree1.SelectedNode;
        }
        public void showpanel(DevComponents.AdvTree.Node tnode)
        {

            panel2.Top = advTree1.Top + tnode.Bounds.Bottom + advTree1.NodeSpacing + 4;
            panel2.Left = advTree1.Left + tnode.Bounds.Left;
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
        private void COAccounts_Load(object sender, EventArgs e)
        {
            datam.SecurityCheck();
            datam.SystemInitializer();
            _folder_style = new DevComponents.DotNetBar.ElementStyle();
            _folder_style.TextColor = Color.Blue;
            //
            _file_style = new DevComponents.DotNetBar.ElementStyle();
            _file_style.TextColor = Color.Black;
            //
            _disabled = new DevComponents.DotNetBar.ElementStyle();
            _disabled.TextColor = Color.Gray;
            _file_style.Font = new Font("verdana", 11, FontStyle.Regular);
            _folder_style.Font = new Font("verdana", 11, FontStyle.Regular);
            //
            LCB_ACCOUNT = accn.GetChildAccounts("LCB", em.account_typeS.ActualAccount).Select(k => k.account_id).ToList();
            //
            InitIgridColumns();
            LoadMainGrid();
            //
            LoadLeftTree();
            fGrid.ReadOnly = true;
        }
        private iGRow CreateNewRow(string field, string rowkey, Type _type, int group_index, string group_name)
        {
            var _row = fGrid.Rows.Add();
            _row.Font = new Font("verdana", 10, FontStyle.Bold);
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
            clear_grid_data();
            iGRow _row = null;
            iGDropDownList icombo = null;
            int group_index = 1;
            string group_name = "Personal Information";
            _row = CreateNewRow("Account Alias", "alias", typeof(string), group_index, group_name);
            _row = CreateNewRow("Account ID", "account_id", typeof(string), group_index, group_name);
            _row.Enabled = iGBool.False;
            _row = CreateNewRow("Account Status", "account_status", typeof(string), group_index, group_name);
            icombo = CreateCombo();
            icombo.Items.Add(new fnn.iGComboItemEX()
            {
                Tag = 1,
                Text = "Enabled",
                Value = "Enabled"
            });
            icombo.Items.Add(new fnn.iGComboItemEX()
            {
                Tag = 0,
                Text = "Disabled",
                Value = "Disabled"
            });
            _row.Cells["desc"].DropDownControl = icombo;
            _row.Cells["desc"].Value = null;
            _row.Enabled = iGBool.False;
            // is system account
            _row = CreateNewRow("Is System Account", "is_sys_account", typeof(string), group_index, group_name);
            icombo = CreateCombo();
            icombo.Items.Add(new fnn.iGComboItemEX()
            {
                Tag = 1,
                Text = "Yes",
                Value = "Yes"
            });
            icombo.Items.Add(new fnn.iGComboItemEX()
            {
                Tag = 0,
                Text = "No",
                Value = "No"
            });
            _row.Cells["desc"].DropDownControl = icombo;
            _row.Cells["desc"].Value = null;
            _row.Enabled = iGBool.False;
            //
            _row = CreateNewRow("Map ID", "map_id", typeof(int), group_index, group_name);
            fGrid.EndUpdate();
                    
        }
        private iGDropDownList CreateCombo()
        {
            iGDropDownList icombo = new iGDropDownList();
            icombo.MaxVisibleRowCount = 15;
            icombo.SearchAsType.MatchRule = iGMatchRule.Contains;
            icombo.SearchAsType.AutoCompleteMode = iGSearchAsTypeMode.Filter;
            icombo.SelItemBackColor = Color.Moccasin;
            icombo.SelItemForeColor = Color.DarkBlue;
            return icombo;
        }
        private void clear_grid_data()
        {
            foreach (iGRow _r in fGrid.Rows)
            {
                _r.Cells["desc"].Value = null;
                _r.Cells["desc"].AuxValue = null;
            }
        }
        private void LoadAccountDetail(ic.accountC _acc)
        {
             clear_grid_data();
            if (_acc == null) { return; }
            fGrid.Rows["alias"].Cells["desc"].Value = _acc.search_alias; fGrid.Rows["alias"].AutoHeight();
            fGrid.Rows["account_id"].Cells["desc"].Value = _acc.account_id.ToStringNullable(); fGrid.Rows["account_id"].AutoHeight();
            fGrid.Rows["account_status"].Cells["desc"].Value = _acc.account_status == em.account_statusS.Enabled ? "Enabled" : "Disabled";
            fGrid.Rows["is_sys_account"].Cells["desc"].Value = _acc.is_sys_account ? "Yes" : "No";

        }
        private void LoadLeftTree()
        {
            if (datam.DATA_ACCOUNTS_D_SECTION == null && datam.DATA_ACCOUNTS_D_SECTION.Keys.Count == 0)
            {
                return;
            }
            advTree1.BeginUpdate();
            DevComponents.DotNetBar.ElementStyle _section_style = new DevComponents.DotNetBar.ElementStyle();
            _section_style.TextColor = Color.Maroon;
            _section_style.Font = new Font("verdana", 12, FontStyle.Regular);
            DevComponents.AdvTree.Node _section = null;
            var slist = from s in datam.DATA_ACCOUNTS_D_SECTION.Values
                        orderby s.disp_index
                        select s;
            foreach (var t in slist)
            {
                _section = new DevComponents.AdvTree.Node();
                _section.Text = t.acc_d_type_name;
                _section.Tag = t;
                _section.Name = string.Format("Section{0}", t.acc_d_type_id);
                _section.ContextMenu = contextMenuDrive;
                _section.Style = _section_style;
                advTree1.Nodes.Add(_section);
                _section = null;
            }
            if (datam.DATA_ACCOUNTS == null || datam.DATA_ACCOUNTS.Count == 0)
            {
                advTree1.EndUpdate();
                return;
            }
            var nlist = from n in datam.DATA_ACCOUNTS.Values
                        orderby n.p_account_id, n.account_id
                        group n by n.account_dept_type.ToByte()
                            into new_group
                            select new
                            {
                                section_id = new_group.Key,
                                section_collection = new_group
                            };
            DevComponents.AdvTree.Node _parent = null;
            DevComponents.AdvTree.Node _nd = null;
            foreach (var n in nlist)
            {
                _section = advTree1.FindNodeByName(string.Format("Section{0}", n.section_id.ToByte()));
                var llist = from j in n.section_collection
                            orderby j.a_level, j.account_id
                            select j;
                foreach (var t in llist)
                {
                    try
                    {
                        _nd = new DevComponents.AdvTree.Node();
                        _nd.Text = t.account_name;
                        _nd.Tag = t;
                        _nd.Name = string.Format("Account{0}", t.account_id);
                        if (t.a_level == 1)
                        {
                            _section.Nodes.Add(_nd);
                        }
                        else
                        {
                            
                            _parent = advTree1.FindNodeByName(string.Format("Account{0}", t.p_account_id));
                            if (_parent != null)
                            {
                                _parent.Nodes.Add(_nd);
                            }
                        }
                        if (t.account_type == em.account_typeS.ActualAccount)
                        {
                            _nd.ContextMenu = contextMenuFile;
                            if (t.account_status == em.account_statusS.Disabled)
                            {
                                _nd.Style = _disabled;
                            }
                            if (t.account_status == em.account_statusS.Enabled)
                            {
                                _nd.Style = _file_style;
                            }

                        }
                        else
                        {
                            //  _nd.Image = MUTTICO.Properties.Resources.Folder;
                            _nd.ContextMenu = contextMenuFolder;
                            _nd.Style = _folder_style;

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }

            }
            //
            #region fill other_income_payable
            //datam.GetAccountsPayable(null);
            //var tlist = from n in datam.DATA_ACCOUNTS_PAYABLE.Values
            //            where n.objAccount.account_status == em.account_statusS.Enabled
            //            group n by n.account_id into new_gp
            //            select new
            //                {
            //                    acc = datam.DATA_ACCOUNTS[new_gp.Key],
            //                    gp_id = new_gp.FirstOrDefault().gp_id
            //                };
            //DevComponents.DotNetBar.ElementStyle _tt = new DevComponents.DotNetBar.ElementStyle();
            //_tt.TextColor = Color.Purple;
            //_tt.Font = new Font("verdana", 12, FontStyle.Regular);
            //foreach (var k in tlist)
            //{
            //    _nd = new DevComponents.AdvTree.Node();
            //    _nd.Text = k.acc.account_name;
            //    _nd.Style = _tt;
            //    _nd.Name = string.Format("PAYABLE{0}", k.acc.account_id);
            //    advTree1.FindNodeByName(string.Format("Account{0}", k.gp_id)).Nodes.Add(_nd);
            //    _nd = null;
                
            //}
            #endregion
            advTree1.EndUpdate();
        }
        
         private void createFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_action = action_type.create;
            m_object = object_type.GroupAccount;
            CreateDummyNode();
        }

         private void createSMSFileToolStripMenuItem_Click(object sender, EventArgs e)
         {
            
             m_action = action_type.create;
             m_object = object_type.Account;
             CreateDummyNode();
         }

         private void toolStripMenuItem2_Click(object sender, EventArgs e)
         {
             m_action = action_type.create;
             m_object = object_type.Account;
             CreateDummyNode();
         }

         private void contextMenuFolder_Opening(object sender, CancelEventArgs e)
         {
             if (advTree1.SelectedNode == null || advTree1.SelectedNode.Tag == null)
             {
                 e.Cancel = true; return;
             }
             ic.accountC _acc = advTree1.SelectedNode.Tag as ic.accountC;
             if (_acc != null)
             {
                 if (_acc.account_dept_type != em.account_d_typeS.Expenses)
                 {
                     e.Cancel = true;
                 }
             }
             if (curr_node != null)
             {
                 toolStripMenuItem1.Visible = curr_node.Level == 2 ? false : true;
             }
             deleteToolStripMenuItem.Visible = curr_node.Nodes.Count == 0 ? true : false;
         }

         private void textBox1_TextChanged(object sender, EventArgs e)
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
         private ic.account_d_typeC GetRoot(DevComponents.AdvTree.Node _nd)
         {
             DevComponents.AdvTree.Node _p = _nd;
             while (_p.Level != 0)
             {
                 _p = _p.Parent;
             }
             return _p.Tag as ic.account_d_typeC;
         }
         private void buttonadd_Click(object sender, EventArgs e)
         {
             if (m_action == action_type.create)
             {
                 #region Insert Region
                 ic.accountC _account = new MTOMS.ic.accountC();
                 _account.account_status = em.account_statusS.Enabled;
                 _account.a_index = curr_node.Index.ToInt16();
                 _account.a_level = curr_node.Level.ToInt16();
                 int gp_id = 0;
                 var _sect = GetRoot(curr_node);
                 _account.account_dept_category = (em.account_d_categoryS)_sect.acc_d_cat_id;
                 _account.account_dept_type = (em.account_d_typeS)_sect.acc_d_type_id;
                 if (curr_node.Level > 1)
                 {
                     _account.p_account_id = curr_node.Parent == null ? 0 : (curr_node.Parent.Tag as ic.accountC).account_id;
                 }
                 using (var xd = new xing())
                 {
                     switch (m_object)
                     {
                         case object_type.GroupAccount:
                             {
                                 #region Add New Types
                                 _account.account_type = curr_node.Level == 1 ? em.account_typeS.GroupAccount : em.account_typeS.SubGroupAccount;
                                 curr_node.Text = textBox1.Text.Trim().ToProperCase();
                                 _account.account_name = curr_node.Text;
                                 curr_node.Style = _folder_style;
                                 curr_node.ContextMenu = contextMenuFolder;
                                 //  curr_node.Image = MUTTICO.Properties.Resources.Folder;
                                 m_action = action_type.none;
                                 if (_account.account_type == em.account_typeS.SubGroupAccount)
                                 {
                                     gp_id = (curr_node.Parent.Tag as ic.accountC).account_id;
                                 }
                                 break;
                                 #endregion
                             }

                         case object_type.Account:
                             {
                                 if (datam.DuplicateAccountName(textBox1.Text.Trim().ToProperCase(), xd))
                                 {
                                     MessageBox.Show("The Account Name You Have Entered Already Exists", "Duplicate Account Name");
                                     buttonclose.PerformClick();
                                     return;
                                 }
                                 #region Add New Types
                                 _account.account_type = em.account_typeS.ActualAccount;
                                 curr_node.Text = textBox1.Text.Trim().ToProperCase();
                                 _account.account_name = curr_node.Text;
                                 curr_node.Style = _folder_style;
                                 curr_node.ContextMenu = contextMenuFile;
                                 // curr_node.Image = MUTTICO.Properties.Resources.email;
                                 m_action = action_type.none;
                                 switch (curr_node.Level)
                                 {
                                     case 2:
                                     case 3:
                                         {
                                             gp_id = (curr_node.Parent.Tag as ic.accountC).account_id;
                                             break;
                                         }

                                 }
                                 break;
                                 #endregion
                             }
                     }
                     string[] _cols = new string[]
                {
                    "account_id",
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
                    "account_short_name","account_code","description","lch_id"
                    
                };
                     object[] _row = new object[]
                {
                    _account.account_id=xd.IDCtrlGet("acc_id_xxx",2013,-500),
                    _account.account_dept_category.ToByte(),
                    _account.account_dept_type.ToByte(),
                    _account.account_name,
                    _account.account_type.ToByte(),
                    _account.account_alias,
                    _account.a_level,
                    _account.a_index,
                    _account.p_account_id=gp_id,
                    emm.export_type.insert.ToByte(),
                    0,
                    datam.CURR_DATE,
                    datam.PC_US_ID,
                    _account.account_status.ToByte(),
                    null,//search_alias
                    0, // is_sys_account
                    0,//post_type
                    0,//owner_type
                    0,//owner_id
                    null,//owner name
                    sdata.CURR_DATE,
                    null,
                    0,//opening balance
                    null,//account_short_name
                    null,// account_code
                    null,//description
                    sdata.App_station_id
                };
                     xd.SingleInsertCommandInt("accounts_tb", _cols, _row);
                     xd.IDCtrlDelete("acc_id_xxx");
                     curr_node.Name = string.Format("Account{0}", _account.account_id);
                     xd.CommitTransaction();
                 }
                 #endregion
                 curr_node.Tag = _account;
                 datam.DATA_ACCOUNTS.Add(_account.account_id, _account);
                 labelclient.Text = _account.account_name;
                 if (_account.account_type == em.account_typeS.ActualAccount)
                 {
                     curr_node.Style = _file_style;
                 }
                 else
                 {
                     curr_node.Style = _folder_style;
                 }

                 _account = null;
                 buttonclose.PerformClick();
             }
             if (m_action == action_type.update)
             {
                 if (curr_node != null && curr_node.Tag != null)
                 {
                     var _folder = curr_node.Tag as ic.accountC;
                     if (_folder != null)
                     {
                         using (var xd = new xing())
                         {
                             if (datam.DuplicateAccountName(textBox1.Text.Trim().ToProperCase(),xd,_folder.account_id))
                             {
                                 MessageBox.Show("The Account Name You Have Entered Already Exists", "Duplicate Account Name");
                                 buttonclose.PerformClick();
                                 return;
                             }
                             xd.SingleUpdateCommandALL("accounts_tb", new string[] { "account_name", "account_id" }, new object[] { textBox1.Text.Trim().ToProperCase(), _folder.account_id }, 1);
                             xd.CommitTransaction();
                         }
                         _folder.account_name = textBox1.Text.Trim().ToProperCase();
                         curr_node.Text = _folder.account_name;
                         buttonclose.PerformClick();
                     }
                 }

             }
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

         private void advTree1_AfterNodeSelect(object sender, DevComponents.AdvTree.AdvTreeNodeEventArgs e)
         {
             curr_node = e.Node;
             if (curr_node == null) { return; }
             labelclient.Text = curr_node == null ? string.Empty : curr_node.Text;
             if (curr_node.Level == 0)
             {
                 label3.Text = string.Empty;
                 m_account = null;
                 clear_grid_data();
                 fGrid.Enabled=false;
                 return;
             }
             if (curr_node.Tag != null)
             {
                 if (!fGrid.Enabled) { fGrid.Enabled = true; }
                 m_account = curr_node.Tag as ic.accountC;
                 LoadAccountDetail(m_account);
                 label3.Text = curr_node.Text;
             }
         }
         private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
         {
             if (e.KeyChar.ToByte() == (byte)Keys.Enter & buttonadd.Enabled)
             {
                 buttonadd.PerformClick();
             }
         }
         private void toolStripMenuItem1_Click(object sender, EventArgs e)
         {
             m_action = action_type.create;
             m_object = object_type.GroupAccount;
             CreateDummyNode();
         }

         private void deleteClientToolStripMenuItem_Click(object sender, EventArgs e)
         {
             if (curr_node != null)
             {
                 if ((advTree1.SelectedNode.Tag != null && (advTree1.SelectedNode.Tag as ic.accountC).is_sys_account))
                 {
                     MessageBox.Show("Cannot Edit This Account Because It Is A System Account", "System Account Error");
                     return;
                 }
                 var _account = curr_node.Tag as ic.accountC;
                 if (_account != null)
                 {
                     using (var xd = new xing())
                     {
                         if (_account.account_dept_type == em.account_d_typeS.Income)
                         {
                             if (xd.ExecuteScalarInt(string.Format("select count(account_id) from off_accounts_tb where account_id={0}", _account.account_id))> 0)
                             {
                                 string _error = "You Cannot Delete This Account Since It Has References In Some Tables, Disable It Instead";
                                 dbm.ErrorMessage(_error, "Delete Failure");
                                 return;
                             }
                             if (xd.ExecuteScalarInt(string.Format("select count(account_id) from pledge_master_tb where account_id={0}", _account.account_id)) > 0)
                             {
                                 string _error = "You Cannot Delete This Account Since It Has References In Some Tables, Disable It Instead";
                                 dbm.ErrorMessage(_error, "Delete Failure");
                                 return;
                             }
                             string _str = "Are You Sure You Want To The Delete The Selected Account";
                             if (!dbm.WarningMessage(_str, "Delete Warning"))
                             {
                                 return;
                             }
                             xd.SingleDeleteCommandExp("accounts_tb", new string[]
                             {
                                 "account_id"
                             }, new int[] { _account.account_id });
                             if (_account.PostType == em.postTypeS.cash_accounts_payable)
                             {
                                 xd.SingleDeleteCommandExp("accounts_payable_tb", new string[]
                             {
                                 "account_id"
                             }, new int[] { _account.account_id });
                             }
                             if (_account.PostType == em.postTypeS.cash_accounts_payable)
                             {
                                 datam.DATA_ACCOUNTS_PAYABLE.Remove(_account.account_id);
                                 var _node = advTree1.FindNodeByName(string.Format("PAYABLE{0}", _account.account_id));
                                 if (_node != null)
                                 {
                                     _node.Remove();
                                 }
                             }
                             //xd.SingleUpdateCommandALL("accounts_tb", new string[]
                             //{
                             //    "account_status",
                             //    "account_id",
                             //}, new object[] { em.account_statusS.Deleted.ToByte(), _account.account_id }, 1);
                             ////
                             //xd.SingleUpdateCommandALL("accounts_tb", new string[]
                             //{
                             //    "account_status",
                             //    "account_id",
                             //}, new object[] { em.account_statusS.Deleted.ToByte(), _account.account_id }, 1);
                         }
                         if (_account.account_dept_type == em.account_d_typeS.Expenses)
                         {
                             string _str = "Are You Sure You Want To The Delete The Selected Expense Account";
                             if (!dbm.WarningMessage(_str, "Delete Warning"))
                             {
                                 return;
                             }
                             xd.SingleDeleteCommandExp("accounts_tb", new string[]
                             {
                                 "account_id"
                             }, new int[] { _account.account_id });
                         }
                         datam.DATA_ACCOUNTS.Remove(_account.account_id);
                         advTree1.SelectedNode.Remove();
                         xd.CommitTransaction();
                     }
                 }
             }
         }

         private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
         {
             if (curr_node != null)
             {
                 if ((advTree1.SelectedNode.Tag != null && (advTree1.SelectedNode.Tag as ic.accountC).is_sys_account))
                 {
                     MessageBox.Show("Cannot Edit This Account Because It Is A System Account", "System Account Error");
                     return;
                 }
                 var _account = curr_node.Tag as ic.accountC;
                 if (_account != null)
                 {
                     string _str = "Are You Sure You Want To The Delete The Selected Account";
                     if (!dbm.WarningMessage(_str, "Delete Warning"))
                     {
                         return;
                     }
                     using (var xd = new xing())
                     {
                         xd.InsertUpdateDelete(string.Format("delete from accounts_tb where account_id={0}", _account.account_id));
                         datam.DATA_ACCOUNTS.Remove(_account.account_id);
                         advTree1.SelectedNode.Remove();
                         xd.CommitTransaction();
                     }
                 }
             }
         }
         private void CheckIfSystemAccount()
         {

         }
         private void editToolStripMenuItem_Click(object sender, EventArgs e)
         {
             if((advTree1.SelectedNode.Tag!=null && (advTree1.SelectedNode.Tag as ic.accountC).is_sys_account))
             {
                 MessageBox.Show("Cannot Edit This Account Because It Is A System Account", "System Account Error");
                 return;
             }
             m_action = action_type.update;
             m_object = object_type.GroupAccount;
             showpanel(advTree1.SelectedNode);
         }

         private void editAccountToolStripMenuItem_Click(object sender, EventArgs e)
         {
             if ((advTree1.SelectedNode.Tag != null && (advTree1.SelectedNode.Tag as ic.accountC).is_sys_account))
             {
                 MessageBox.Show("Cannot Edit This Account Because It Is A System Account", "System Account Error");
                 return;
             }
             m_action = action_type.update;
             m_object = object_type.Account;
             showpanel(advTree1.SelectedNode);
         }
         private void advTree1_Click(object sender, EventArgs e)
         {

         }
         private void CreateRow(DevComponents.AdvTree.Node nd)
         {
            
             
         }
         private void Recursive(DevComponents.AdvTree.Node _nd)
         {
             
             ic.accountC _acc = _nd.Tag as ic.accountC;
             iGRow _row = null;
             foreach (var t in _nd.Nodes.Cast<DevComponents.AdvTree.Node>())
             {
                 _row= fGrid.Rows.Add();
                 _row.Level = t.Level - 1;
                 _row.Font = new Font("georgia", 12, FontStyle.Regular);
                 _row.ReadOnly = iGBool.True;
                 _row.Cells["Account Name"].Value = t.Text;
                 _row.Cells["Alias"].Value = null;
                 _row.Cells["section_index"].Value = _acc.account_dept_type.ToByte();
                 _row.Cells["section_name"].Value = _acc.account_dept_type.ToStringNullable();
                 _row.Key = _acc.account_id.ToStringNullable();
                 _row.AutoHeight();
                 if (t.Nodes.Count == 0) { continue; }
                 _row.TreeButton = iGTreeButtonState.Visible;
                 Recursive(t);
             }
         }
         private void buttonrefresh_Click(object sender, EventArgs e)
         {
             //if (advTree1.Nodes.Count == 0)
             //{
             //    return;
             //}
             //fGrid.Rows.Clear();
             //ic.accountC _acc = null;
             //iGRow _row = null;
             //int prev_sect_id = -5;
             //foreach (var t in advTree1.Nodes.Cast<DevComponents.AdvTree.Node>())
             //{
             //    foreach (var x in t.Nodes.Cast<DevComponents.AdvTree.Node>())
             //    {
             //         _acc= x.Tag as ic.accountC;       
             //        if (prev_sect_id != _acc.account_section.ToByte())
             //        {
             //            _row = fGrid.Rows.Add();
             //            _row.Type = iGRowType.ManualGroupRow;
             //            _row.Level = 0;
             //            _row.RowTextCell.Value = "Gina";
             //            prev_sect_id = _acc.account_section.ToByte();
             //        }
             //            _row = fGrid.Rows.Add();
             //            _row.Level = x.Level - 1;
             //            _row.Font = new Font("georgia", 12, FontStyle.Regular);
             //            _row.ReadOnly = iGBool.True;
             //            _row.Cells["Account Name"].Value = _acc.account_name;
             //            _row.Cells["Alias"].Value = null;
             //            _row.Cells["section_index"].Value = _acc.account_section.ToByte();
             //            _row.Cells["section_name"].Value = _acc.account_section.ToStringNullable();
             //            _row.Key = _acc.account_id.ToStringNullable();
             //            _row.AutoHeight();
             //            if (x.Nodes.Count == 0) { continue; }
             //            _row.TreeButton = iGTreeButtonState.Visible;
             //            Recursive(x);
                   
             //    }
             //}
             ////fGrid.GroupObject.Clear();
             ////fGrid.GroupObject.Add("section_index");
             
             ////fGrid.Group();
         }

         private void fGrid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
         {
             string _key = fGrid.Rows[e.RowIndex].Key;
             if (fGrid.Rows[e.RowIndex].Cells["desc"].DropDownControl == null & fGrid.Rows[e.RowIndex].Cells["desc"].Value != null & fGrid.Rows[e.RowIndex].Cells["desc"].ValueType==typeof(string))
             {
                 fGrid.Rows[e.RowIndex].Cells["desc"].Value = fGrid.Rows[e.RowIndex].Cells["desc"].Value.ToString().Trim().ToUpper();
             }
             if (!string.IsNullOrEmpty(_key))
             {
                 switch (_key)
                 {
                     case "alias":
                         {
                             var _alias = fGrid.Rows["alias"].Cells["desc"].Value;
                              if (datam.DATA_ACCOUNTS.Values.Where(p => p.account_id != m_account.account_id & !string.IsNullOrEmpty(p.search_alias)).Count(k => k.search_alias.ToLower() == _alias.ToString().ToLower()) > 0)
                                 {
                                     MessageBox.Show(string.Format("Alias {0} Already Exists", _alias));
                                     return;
                                 }   
                             dbh.SingleUpdateCommand("accounts_tb", new string[] { "alias", "account_id" }, new object[] { fGrid.Rows[_key].Cells["desc"].Value, m_account.account_id }, 1);
                             m_account.search_alias = fGrid.Rows[_key].Cells["desc"].Value == null ? null : fGrid.Rows[_key].Cells["desc"].Value.ToStringNullable();
                             fGrid.Rows["alias"].AutoHeight();
                             break;
                         }
                     case "map_id":
                         {
                             var _map_id = fGrid.Rows["map_id"].Cells["desc"].Value == null ? 0 : fGrid.Rows["map_id"].Cells["desc"].Value.ToInt32();
                             dbh.SingleUpdateCommand("accounts_tb", new string[] { "map_id", "account_id" }, new object[] { _map_id, m_account.account_id }, 1);
                            
                             fGrid.Rows["map_id"].AutoHeight();
                             break;
                         }
                 }

             }
         }

         private void tabControl1_Click(object sender, EventArgs e)
         {

         }

         private void contextMenuDrive_Opening(object sender, CancelEventArgs e)
         {
             if (advTree1.SelectedNode == null || advTree1.SelectedNode.Tag==null)
             {
                 e.Cancel = true; return;
             }
             if (advTree1.SelectedNode.Index < 3)
             {
                 e.Cancel = true;
             }
             if (advTree1.SelectedNode.Index == 3)
             {
                 createSMSFileToolStripMenuItem.Visible = false;
                 createFolderToolStripMenuItem.Visible = false;
                 createINCOMEAccountToolStripMenuItem.Visible = true;
             }
             else
             {
                 createSMSFileToolStripMenuItem.Visible = true;
                 createFolderToolStripMenuItem.Visible = true;
                 createINCOMEAccountToolStripMenuItem.Visible = false;
             }

         }

         private void contextMenuFile_Opening(object sender, CancelEventArgs e)
         {
             if (advTree1.SelectedNode == null || advTree1.SelectedNode.Tag == null)
             {
                 e.Cancel = true; return;
             }
             ic.accountC _acc = advTree1.SelectedNode.Tag as ic.accountC;
             if (_acc != null)
             {
                 e.Cancel = true;

                 //if (_acc.is_sys_account)
                 //{
                 //    e.Cancel = true;
                 //}
                 if (_acc.is_sys_account & LCB_ACCOUNT.IndexOf(_acc.account_id) > -1)
                 {
                     e.Cancel = false;
                 }
                 disableAccountToolStripMenuItem.Text = m_account.account_status == em.account_statusS.Enabled ? "Disable Account" : "Enable Account";
                 if (LCB_ACCOUNT.IndexOf(_acc.account_id) > -1)
                 {
                     linkToChurchGroupSharedToolStripMenuItem.Visible = true;
                 }
                 else
                 {
                     linkToChurchGroupSharedToolStripMenuItem.Visible = false;
                 }

             }
         }
         public void NewIncomeAccount(ic.accountC _inc)
         {
             if (_inc == null) { return; }
             var Parent_node = advTree1.FindNodeByName(string.Format("Account{0}", _inc.p_account_id));
             if (Parent_node != null)
             {
                 DevComponents.AdvTree.Node _nd = new DevComponents.AdvTree.Node();
                 _nd.ContextMenu = contextMenuFile;
                 _nd.Text = _inc.account_name;
                 _nd.Tag = _inc;
                 _nd.Name = string.Format("Account{0}", _inc.account_id);
                 if (_inc.account_status == em.account_statusS.Disabled)
                 {
                     _nd.Style = _disabled;
                 }
                 if (_inc.account_status == em.account_statusS.Enabled)
                 {
                     _nd.Style = _file_style;
                 }
                 Parent_node.Nodes.Add(_nd); _nd = null;
                 Parent_node.Expand();
             }
             if (_inc.PostType == em.postTypeS.cash_accounts_payable)
             {
                 #region fill other_income_payable
                 datam.GetAccountsPayable(null);
                 var _gp = datam.GetAccountByAlias("OIPAYABLE");
                 if (_gp != null)
                 {
                     var parent_node = advTree1.FindNodeByName(string.Format("Account{0}", _gp.account_id));
                     DevComponents.DotNetBar.ElementStyle _tt = new DevComponents.DotNetBar.ElementStyle();
                     _tt.TextColor = Color.DeepPink;
                     _tt.Font = new Font("verdana", 12, FontStyle.Regular);
                   DevComponents.AdvTree.Node _nd = new DevComponents.AdvTree.Node();
                     _nd.Text = _inc.account_name;
                     _nd.Style = _tt;
                     _nd.Name = string.Format("PAYABLE{0}", _inc.account_id);
                     parent_node.Nodes.Add(_nd);
                 }
                 #endregion
             }

         }
         private void createINCOMEAccountToolStripMenuItem_Click(object sender, EventArgs e)
         {
             if (curr_node != null && curr_node.Level == 0 & curr_node.Index == 3)
             {
                 advTree1.BeginUpdate();
                 advTree1.CollapseAll();
                 advTree1.Nodes[3].Expand();
                 //
                 var _gp_acc = datam.GetAccountByAlias("INCOME_OTHERS");
                 if (_gp_acc != null)
                 {
                     var _node = advTree1.FindNodeByName(string.Format("Account{0}", _gp_acc.account_id));
                     if (_node != null)
                     {
                         _node.CollapseAll();
                         _node.Expand();
                     }
                 }
                 advTree1.EndUpdate();
                 using (var _fm = new IncomeAccountsMaker())
                 {
                     _fm.Owner = this;
                     _fm.ShowDialog();
                     _fm.Dispose();
                 }

             }
         }

         private void toolStripMenuItem3_Click(object sender, EventArgs e)
         {

         }

         private void disableAccountToolStripMenuItem_Click(object sender, EventArgs e)
         {
             if(m_account!=null)
             {
                 string _str = string.Format("Are You Sure You want To {0} This Account ??", m_account.account_status == em.account_statusS.Enabled ? "Disable" : "Enable");
                 if (!dbm.WarningMessage(_str, "Update Warning"))
                 {
                     return;
                 }
                 m_account.account_status = m_account.account_status == em.account_statusS.Enabled ? em.account_statusS.Disabled : em.account_statusS.Enabled;
                 using (var xd = new xing())
                 {
                     xd.SingleUpdateCommandALL("accounts_tb",
                         new string[] { "account_status_id", "account_id" },
                         new object[] { m_account.account_status.ToByte(), m_account.account_id },
                         1);
                     xd.CommitTransaction();
                 }
                 LoadAccountDetail(m_account);
             }
         }

         
    }
}
