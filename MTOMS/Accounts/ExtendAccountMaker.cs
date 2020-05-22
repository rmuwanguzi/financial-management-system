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
    public partial class ExtendAccountMaker : DevComponents.DotNetBar.Office2007Form
    {
        public ExtendAccountMaker()
        {
            InitializeComponent();
        }
        private iGDropDownList m_Deparments = null;
        private iGDropDownList m_church_groups = null;
        private iGDropDownList m_members = null;
        private iGDropDownList m_CG_Shared = null;
        SortedDictionary<string, em.AccountOwnerTypeS> m_OWNER_Types { get; set; }
        ic.accountC m_account = null;
        int[] m_Range = new int[2];
        private int m_StartExtendedCG = 0;
        private void ExtendAccountMaker_Load(object sender, EventArgs e)
        {
            CenterToScreen();
         

            
            m_account = this.Tag as ic.accountC;
            if (m_account == null)
            {
                this.Close();
                return;
            }
            if (fnn.IsSabbathGainAccount(m_account.account_id))
            {
                MessageBox.Show("You Cannot Extend This Account", "Extend Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if(m_account.owner_type==em.AccountOwnerTypeS.CHURCH_GROUP)
            {
                MessageBox.Show("You Cannot Extend This Account Because Its Owner Is A CHURCH GROUP", "Extend Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if (m_account.owner_type == em.AccountOwnerTypeS.CHURCH_GROUP_SHARED)
            {
                MessageBox.Show("You Cannot Extend This Account Because Its Owner Is A CHURCH GROUP SHARED", "Extend Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            datam.ShowWaitForm();
            Application.DoEvents();
            InitIgridColumns();
            datam.SecurityCheck();
            backworker.RunWorkerAsync();
           
        }
         private void LoadGroupNames()
         {
             //if (datam.DATA_ACCOUNT_GROUPS == null)
             //{
             //    datam.DATA_ACCOUNT_GROUPS = new SortedList<int, string>();
             //    datam.DATA_ACCOUNT_GROUPS.Add(12, "BUILDING");
             //    datam.DATA_ACCOUNT_GROUPS.Add(13, "FUNDRAISING");
             //    datam.DATA_ACCOUNT_GROUPS.Add(14, "WEDDING");
             //    datam.DATA_ACCOUNT_GROUPS.Add(15, "CAMP MEETING");
             //    datam.DATA_ACCOUNT_GROUPS.Add(16, "EFFORT");
             //    datam.DATA_ACCOUNT_GROUPS.Add(17, "CHURCH EX INCOME");
                 

             //}
         }
         private void LoadDropDownLists()
         {
             m_Deparments = fnn.CreateCombo();
             m_church_groups = fnn.CreateCombo();
             m_members = fnn.CreateCombo();
             m_CG_Shared = fnn.CreateCombo();
             m_OWNER_Types = new SortedDictionary<string, em.AccountOwnerTypeS>();
              m_OWNER_Types.Add("CHURCH/LCB", em.AccountOwnerTypeS.CHURCH);
             m_OWNER_Types.Add("CHURCH_PROJECT", em.AccountOwnerTypeS.CHURCH_PROJECT);
             m_OWNER_Types.Add("DISTRICT", em.AccountOwnerTypeS.DISTRICT);
             m_OWNER_Types.Add("CHURCH_GROUP", em.AccountOwnerTypeS.CHURCH_GROUP);
             m_OWNER_Types.Add("DEPARTMENT", em.AccountOwnerTypeS.DEPARTMENT);
             m_OWNER_Types.Add("CHURCH_MEMBER", em.AccountOwnerTypeS.CHURCH_MEMBER);
             m_OWNER_Types.Add("OTHER", em.AccountOwnerTypeS.OTHER);
             m_OWNER_Types.Add("CUC", em.AccountOwnerTypeS.CUC);
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
                 ic.church_group_typeC _cg_type=null;
                 foreach (var k in dlist)
                 {
                     _cg_type=datam.DATA_CG_TYPES[k];
                     m_CG_Shared.Items.Add(new fnn.iGComboItemEX()
                     {
                         ID = _cg_type.cg_type_id,
                         Value = string.Format("{0} Shared",_cg_type.cg_type_name),
                         ForeColor=Color.DarkBlue
                         
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
         protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
         {
             if (keyData == Keys.Tab)
             {
                
                 if (!fGrid.IsEditing & (fGrid.CurCell != null && (fGrid.CurCell.ColIndex == 0 & fGrid.CurCell.RowIndex == fGrid.Rows.Count - 1)))
                 {
                     buttonX1.PerformClick();
                     return true;
                 }
             }
             if (keyData==Keys.F1 & (fGrid.Rows.Count > 0 & fGrid.Rows["owner"].Tag != null))
             {
                 switch ((em.AccountOwnerTypeS)fGrid.Rows["owner"].Tag.ToByte())
                 {
                     case em.AccountOwnerTypeS.CHURCH_GROUP:
                         {
                             using (var _fm = new ChurchGroups.ChurchGroupMaker())
                             {
                                 _fm.ShowDialog();
                                 if (_fm.Tag != null)
                                 {
                                     if (datam.DATA_CHURCH_GROUPS != null)
                                     {
                                         m_church_groups.Items.Clear();
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
                                         m_CG_Shared.Items.Clear();
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
                                 }
                             }
                             fGrid.Focus();
                             fGrid.SetCurCell("owner", 1);
                             break;
                         }
                     case em.AccountOwnerTypeS.DEPARTMENT:
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
                                         m_Deparments.Items.Clear();
                                         foreach (var k in nlist)
                                         {
                                             m_Deparments.Items.Add(new fnn.iGComboItemEX()
                                             {
                                                 ID = k.dept_id,
                                                 Value = k.dept_name
                                             });
                                         }
                                     }
                                 }
                             }
                             fGrid.Focus();
                             fGrid.SetCurCell("owner", 1);
                             break;
                         }

                 }
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
             iGDropDownList icombo = null;
             int group_index = 1;
             string group_name = "New Account Information";
          
            
             int _indxx = 0;
            
             //
             _row = CreateNewRow("Account Name", "account_name", typeof(string), group_index, group_name);
             _row = CreateNewRow("Account Status", "account_status", typeof(string), group_index, group_name);
             icombo = fnn.CreateCombo();
             foreach (var k in new string[] { "Enabled", "Disabled" })
             {
                 icombo.Items.Add(new fnn.iGComboItemEX()
                 {
                     Value = k
                 });
             }
             _row.Cells[1].DropDownControl = icombo;
             _row.Cells[1].Value = null;
             //
             _row = CreateNewRow("Account Code", "account_code", typeof(string), group_index, group_name);
             _row.Visible = false;
             //
             _row = CreateNewRow("(Opt) Account Category", "account_category", typeof(string), group_index, group_name);
             //
             if (datam.DATA_ACCOUNTS != null && datam.DATA_ACCOUNTS.Keys.Count > 0)
             {
                  var nlist = accn.GetChildAccounts("LC_OFFERTORY_ACCOUNTS", em.account_typeS.SubGroupAccount);
                 icombo = fnn.CreateCombo();
                 foreach (var k in nlist)
                 {
                     icombo.Items.Add(new fnn.iGComboItemEX()
                     {
                         Value = k.account_name,
                         Tag = k
                     });
                 }
                 _row.Cells[1].DropDownControl = icombo;
             }
             //
             _row = CreateNewRow("Account Group Name", "group_name", typeof(string), group_index, group_name);
             _row.Visible = false;
             //
             
             //
             _row = CreateNewRow("Account Owner Type", "owner_type", typeof(string), group_index, group_name);
             icombo = fnn.CreateCombo();
             _indxx = 13;
            
             foreach (var k in m_OWNER_Types.Keys)
             {
                 icombo.Items.Add(new fnn.iGComboItemEX()
                 {
                     Value = k,
                     Tag=k
                 });
                 _indxx++;
             }
             _row.Cells["desc"].DropDownControl = icombo;
             //
             _row = CreateNewRow("(F1)  Account Owner", "owner", typeof(string), group_index, group_name);
             _row.Visible = false;
             //
             _row = CreateNewRow("Owner Name", "owner_name", typeof(string), group_index, group_name);
             _row.Visible = false;
             //

            
             //
             _row = CreateNewRow("Owner Is A Creditor ?", "post_type", typeof(string), group_index, group_name);
             icombo = fnn.CreateCombo();
             _indxx = 1;
             foreach (var k in new string[] { "Yes", "No"})
             {
                 icombo.Items.Add(new fnn.iGComboItemEX()
                 {
                     Value = k,
                     Tag = _indxx
                 });
                 _indxx++;
             }
             _row.Cells["desc"].DropDownControl = icombo;
           //
             _row = CreateNewRow("Opening Balance", "sbalance", typeof(decimal), group_index, group_name);
             _row.Cells[1].FormatString = "{0:N0}";
             _row.Visible = false;
             //
             _row = fGrid.Rows.Add();
             _row.Height = 5;
             _row.BackColor = Color.Lavender;
             _row.Selectable = false;
             //
             _row = CreateNewRow("Start Date", "start_date", typeof(string), group_index, group_name);
             _row.Cells["desc"].TypeFlags = iGCellTypeFlags.ComboPreferValue | iGCellTypeFlags.NoTextEdit | iGCellTypeFlags.HasEllipsisButton;
             _row.Cells["desc"].FormatString = "{0:D}";// {0:d} for short date string
             _row.Cells["desc"].Value = null;
             _row.Cells["desc"].DropDownControl = null;
             _row.Visible = false;
             //
             _row = CreateNewRow("(If Temporal) End Date", "end_date", typeof(string), group_index, group_name);
             _row.Cells["desc"].TypeFlags = iGCellTypeFlags.ComboPreferValue | iGCellTypeFlags.NoTextEdit | iGCellTypeFlags.HasEllipsisButton;
             _row.Cells["desc"].FormatString = "{0:D}";// {0:d} for short date string
             _row.Cells["desc"].Value = null;
             _row.Cells["desc"].DropDownControl = null;
             _row.Enabled = iGBool.False;
             _row.Visible = false;
             //
             //_row = fGrid.Rows.Add();
             //_row.Height = 5;
             //_row.BackColor = Color.Lavender;
             //_row.Selectable = false;
             _row = CreateNewRow("Description", "description", typeof(string), group_index, group_name);
             _row.Height = 60;
             _row.Cells["desc"].TextFormatFlags = iGStringFormatFlags.WordWrap;
             _row.Cells["name"].TextAlign = iGContentAlignment.MiddleRight;
             _row.Visible = false;
             //
           
             //
             _row = CreateNewRow("Extend Account To", null, typeof(string), group_index, group_name);
             _row.BackColor = Color.FromArgb(64, 64, 64);
             _row.Selectable = false;
             _row.ForeColor = Color.WhiteSmoke;
             _row.Cells[1].Value = "Extend This Account To";
             _row.Cells[0].Value = "Tick Box";
             _row.Cells[0].TextAlign = iGContentAlignment.MiddleRight;
             _row.Cells[0].BackColor = Color.WhiteSmoke;

             //
             _row = fGrid.Rows.Add();
             _row.Height = 5;
             _row.BackColor = Color.Lavender;
             _row.Selectable = false;
             //
             var cg_type_list = (from k in datam.DATA_CHURCH_GROUPS.Values
                                 select k.cg_type_id).Distinct().ToList();
             bool _created = false;
             if (cg_type_list.Count() > 0 & !fnn.IsCombinedOffering(m_account.account_id))
             {
                 ic.church_group_typeC _cg_type=null;
                 string _last_char=null;
                 string _disp_name=null;
                 foreach (var t in cg_type_list)
                 {
                     _row = CreateNewRow(string.Empty, string.Format("{0}{1}", "cg_type_", t), typeof(string), group_index, group_name);
                     _row.Cells[0].Type = iGCellType.Check;
                     _row.Cells[0].ImageAlign = iGContentAlignment.MiddleRight;
                     _cg_type = datam.DATA_CG_TYPES[t];
                     _last_char = _cg_type.cg_type_name.Substring(_cg_type.cg_type_name.Length - 1);
                     _disp_name = _cg_type.cg_type_name.Remove(_cg_type.cg_type_name.Length - 1);
                     _row.Cells[1].Value = _last_char.ToLower() == "y" ? string.Format("{0}{1}", _disp_name, "ies") : _cg_type.cg_type_name;
                     _row.Cells[0].ReadOnly = iGBool.False;
                     _row.Cells[0].Enabled = iGBool.True;
                     _row.Cells[1].ReadOnly = iGBool.True;
                    // _row.Selectable = false;
                     _row.Cells[0].Selectable = iGBool.True;
                     _row.Cells[1].Selectable = iGBool.False;
                     _row.Cells[1].ForeColor = Color.Blue;
                   
                                        //
                    _row.Cells[0].Value = null;
                    _row.Cells[0].Selectable = iGBool.True;
                    _row.Cells[0].SingleClickEdit = iGBool.True;
                    _row.Tag = datam.DATA_CG_TYPES[t];
                     if(!_created)
                     {
                         _created = true;
                         m_Range[0] = _row.Index;
                     }
                     if(m_StartExtendedCG==0)
                     {
                         m_StartExtendedCG = _row.Index;
                     }
                 }
             }
             m_Range[1] = _row.Index;

             _row = fGrid.Rows.Add();
             _row.Height = 5;
             _row.BackColor = Color.Lavender;
             _row.Selectable = false;
             //
             _row = CreateNewRow("Extend Account To", null, typeof(string), group_index, group_name);
             _row.BackColor = Color.FromArgb(64, 64, 64);
             _row.Selectable = false;
             _row.ForeColor = Color.WhiteSmoke;
             _row.Cells[1].Value = "Purpose For Extending Account";
             _row.Cells[0].Value = null;
             _row.Cells[0].BackColor = Color.WhiteSmoke;
              //
             _row = fGrid.Rows.Add();
             _row.Height = 5;
             _row.BackColor = Color.Lavender;
             _row.Selectable = false;
            
             foreach (var t in datam.DATA_ENUM_ACCOUNT_EXTENSION)
             {
                 if (t.Key == em.account_extension_purposeS.none) { continue; }
                 _row = CreateNewRow(string.Empty, string.Format("{0}{1}", "p_", t.Key.ToByte()), typeof(string), group_index, group_name);
                 _row.Cells[0].Type = iGCellType.Check;
                 _row.Cells[0].ImageAlign = iGContentAlignment.MiddleRight;

                 _row.Cells[1].Value = t.Value;
                 _row.Cells[0].ReadOnly = iGBool.False;
                 _row.Cells[0].Enabled = iGBool.True;
                 _row.Cells[1].ReadOnly = iGBool.True;
                 _row.Cells[1].Selectable = iGBool.False;
                 _row.Cells[1].ForeColor = Color.Blue;

                 //
                 _row.Cells[0].Value = null;
                 _row.Cells[0].Selectable = iGBool.True;
                 _row.Cells[0].SingleClickEdit = iGBool.True;
                 _row.Cells[0].Enabled = _created ? iGBool.True : iGBool.False;

             }
             _row = fGrid.Rows.Add();
             _row.Height = 5;
             _row.BackColor = Color.Lavender;
             _row.Selectable = false;
            
             fGrid.EndUpdate();
             if (!_created)
             {
                 datam.HideWaitForm();
                 if (cg_type_list.Count() == 0)
                 {
                     MessageBox.Show("You Cannot Extend This Account Because No Church Groups Have Been Created", "Extend Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     this.Close();
                     return;
                 }
                 MessageBox.Show("You Cannot Extend This Account", "Extend Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 this.Close();
                 return;
             }
            
         }
         private void DisableExtending(bool _val)
         {
             return;
             if (m_StartExtendedCG == 0) { return; }
             for (int j = m_StartExtendedCG; j < fGrid.Rows.Count; j++)
             {
                 try
                 {
                     if (fGrid.Rows[j].Tag != null)
                     {
                         if (fGrid.Rows[j].Tag.GetType() == typeof(ic.church_group_typeC))
                         {
                             fGrid.Rows[j].Cells[0].Enabled = _val ? iGBool.False : iGBool.True;
                             fGrid.Rows[j].Cells[0].Value = null;
                             fGrid.Rows[j].Cells[0].BackColor = Color.Empty;
                         }
                     }
                 }
                 catch (Exception)
                 {
                     continue; 
                    
                 }
             }
         }
         public void FillAccountDetails(ic.accountC _acc)
         {
             fGrid.Rows["account_name"].Cells[1].Value = _acc.account_name;
             fGrid.Rows["account_category"].Cells[1].Value = datam.DATA_ACCOUNTS[_acc.p_account_id].account_name;
             fGrid.Rows["owner_type"].Cells[1].Value = (from s in m_OWNER_Types
                                                        where s.Value == _acc.owner_type
                                                        select s.Key).FirstOrDefault();
             //
             #region owner type
             switch (_acc.owner_type)
             {
                 case em.AccountOwnerTypeS.CHURCH:
                     {
                         fGrid.Rows["owner"].Cells[1].Value = string.IsNullOrEmpty(_acc.owner_name) ? "CHURCH" : _acc.owner_name;
                         fGrid.Rows["owner"].Visible = false;
                         fGrid.Rows["sbalance"].Cells[1].Value = 0;
                         fGrid.Rows["sbalance"].Visible = false;
                         DisableExtending(false);
                         break;
                     }
                 case em.AccountOwnerTypeS.CHURCH_GROUP:
                     {
                         fGrid.Rows["owner"].Visible = true;
                         fGrid.Rows["owner"].Cells[1].DropDownControl = m_church_groups;
                         fGrid.Rows["owner"].Tag = em.AccountOwnerTypeS.CHURCH_GROUP;
                         fGrid.Rows["owner"].Cells[1].Value = string.IsNullOrEmpty(_acc.owner_name) ? string.Empty : _acc.owner_name;
                         DisableExtending(true);
                         break;
                     }
                 case em.AccountOwnerTypeS.CHURCH_GROUP_SHARED:
                     {
                         fGrid.Rows["owner"].Visible = true;
                         fGrid.Rows["owner"].Cells[1].DropDownControl = m_CG_Shared;
                         fGrid.Rows["owner"].Tag = em.AccountOwnerTypeS.CHURCH_GROUP_SHARED;
                         fGrid.Rows["owner"].Cells[1].Value = string.IsNullOrEmpty(_acc.owner_name) ? string.Empty : _acc.owner_name;
                         DisableExtending(true);
                         break;
                     }
                 case em.AccountOwnerTypeS.CHURCH_MEMBER:
                     {
                         fGrid.Rows["owner"].Visible = true;
                         fGrid.Rows["owner"].Cells[1].DropDownControl = m_members;
                         fGrid.Rows["owner"].Tag = em.AccountOwnerTypeS.CHURCH_MEMBER;
                         fGrid.Rows["owner"].Cells[1].Value = string.IsNullOrEmpty(_acc.owner_name) ? string.Empty : _acc.owner_name;
                         DisableExtending(true);
                         break;
                     }
                 case em.AccountOwnerTypeS.CUC:
                     {
                         fGrid.Rows["owner"].Cells[1].Value = string.IsNullOrEmpty(_acc.owner_name) ? "CUC" : _acc.owner_name;
                         fGrid.Rows["owner"].Visible = false;
                         fGrid.Rows["sbalance"].Cells[1].Value = 0;
                         fGrid.Rows["sbalance"].Visible = false;
                         break;
                     }
                 case em.AccountOwnerTypeS.DEPARTMENT:
                     {
                         fGrid.Rows["owner"].Visible = true;
                         fGrid.Rows["owner"].Cells[1].DropDownControl = m_Deparments;
                         fGrid.Rows["owner"].Tag = em.AccountOwnerTypeS.DEPARTMENT;
                         fGrid.Rows["owner"].Cells[1].Value = string.IsNullOrEmpty(_acc.owner_name) ? null : _acc.owner_name;
                         DisableExtending(true);
                         break;
                     }
                 case em.AccountOwnerTypeS.DISTRICT:
                     {
                         fGrid.Rows["owner"].Cells[1].Value = string.IsNullOrEmpty(_acc.owner_name) ? "District" : _acc.owner_name;
                         fGrid.Rows["owner"].Visible = false;
                         fGrid.Rows["sbalance"].Cells[1].Value = 0;
                         fGrid.Rows["sbalance"].Visible = false;
                         break;
                     }
                 default:
                     {
                         fGrid.Rows["owner"].Cells[1].Value = string.IsNullOrEmpty(_acc.owner_name) ? string.Empty : _acc.owner_name;
                         break;
                     }
             }
             #endregion
             fGrid.Rows["post_type"].Cells[1].Value = _acc.PostType == em.postTypeS.cash_accounts_payable ? "Yes" : "No";
             fGrid.Rows["sbalance"].Visible = false;
             fGrid.Rows["sbalance"].Cells[1].Value = _acc.opening_balance;
             fGrid.Rows["sbalance"].Cells[1].FormatString = "{0:N0}";
             var start_date = new fnn.DropDownCalenderX();
             start_date.start_date = sdata.CURR_DATE.AddDays(-360);
             start_date.end_date = _acc.start_date;
             start_date.selected_date = _acc.start_date;
             //
             fGrid.Rows["start_date"].Cells[1].DropDownControl = start_date;
             fGrid.Rows["start_date"].Cells[1].Value = _acc.start_date;
             //
             var _end_date = new fnn.DropDownCalenderX();
             _end_date.start_date = _acc.start_date.AddDays(1);
             if (_acc.last_offertory_date == null)
             {
                 if (_acc.account_status == em.account_statusS.Expired)
                 {
                     _end_date.start_date = _acc.end_date;
                 }
                 _end_date.end_date = sdata.CURR_DATE.AddDays(360);
                 _end_date.selected_date = _end_date.start_date;
             }
             else
             {
                 _end_date.start_date = _acc.last_offertory_date.Value.AddDays(1);
                 if (_acc.account_status == em.account_statusS.Expired)
                 {
                     _end_date.start_date = _acc.end_date;
                 }
                 _end_date.end_date = sdata.CURR_DATE.AddDays(360);
                 _end_date.selected_date = _end_date.start_date;
             }
             fGrid.Rows["end_date"].Cells[1].DropDownControl = _end_date;
             fGrid.Rows["end_date"].Cells[1].Value = _acc.end_date;
             fGrid.Rows["end_date"].Cells[1].Enabled = iGBool.True;
             //
             fGrid.Rows["description"].Cells[1].Value = _acc.description;
             if (_acc.isExtended)
             {
                 foreach (var k in _acc.ExtCgTypeIds)
                 {
                     fGrid.Rows[string.Format("{0}{1}", "cg_type_", k)].Cells[0].Value = 1;
                     fGrid.Rows[string.Format("{0}{1}", "cg_type_", k)].Cells[1].BackColor = Color.LightGreen;
                 }
             }
             var _disable = new string[]
             {
                 "owner_type",
                 "owner",
                 "owner_name",
                 "post_type",
                 "sbalance",
                 "start_date","end_date","account_name","account_category","account_status"
                
             };
             switch (_acc.account_status)
             {
                 case em.account_statusS.Deleted:
                 case em.account_statusS.Disabled:
                 case em.account_statusS.Expired:
                     {
                         fGrid.Rows["account_status"].Cells[1].Value = "Disabled";
                         break;
                     }
                 case em.account_statusS.Enabled:
                     {
                         fGrid.Rows["account_status"].Cells[1].Value = "Enabled";
                         break;
                     }
             }
             foreach (var k in _disable)
             {
                 fGrid.Rows[k].Cells[1].Enabled = iGBool.False;
                 fGrid.Rows[k].Visible = false;
             }
             _disable = new string[]
                 {
                   
                     "account_name","account_category","account_status","owner_type"
                 };
             foreach (var k in _disable)
             {
                 fGrid.Rows[k].Cells[1].Enabled = iGBool.False;
                 fGrid.Rows[k].Visible = true;
             }
             if (_acc.extension_purpose != em.account_extension_purposeS.none)
             {
                 fGrid.Rows[string.Format("p_{0}", _acc.extension_purpose.ToByte())].Cells[1].BackColor = Color.LightGreen;
                 fGrid.Rows[string.Format("p_{0}", _acc.extension_purpose.ToByte())].Cells[0].Value = 1;
                 fGrid.Rows[string.Format("p_{0}", _acc.extension_purpose.ToByte())].Cells[0].Enabled = iGBool.True;
             }
             else
             {
                 iGRow _row = null;
                 if (fnn.IsMainOffering(_acc.account_id))
                 {
                     if (_acc.account_id != -2435)
                     {
                         //_row = fGrid.Rows[string.Format("p_{0}", em.account_extension_purposeS.analysis_only.ToByte())];
                         //_row.Cells[1].BackColor = Color.LightGreen;
                         ////_row.Cells[0].Value = 1;
                         ////_row.Cells[0].Enabled = iGBool.False;
                         ////_row.Selectable = false;
                         //
                         _row = fGrid.Rows[string.Format("p_{0}", em.account_extension_purposeS.finance_and_analysis.ToByte())];
                         _row.Cells[0].Value = 0;
                         _row.Cells[1].BackColor = Color.Empty;
                         _row.Cells[0].Enabled = iGBool.False;
                         _row.Cells[1].Enabled = iGBool.False;
                         _row.Cells[0].AuxValue = 1;
                         _row.Selectable = false;
                         //
                     }
                     else
                     {
                         //lunch settings
                     }
                 }
                 else
                 {
                     if (_acc.PostType == em.postTypeS.cash)
                     {
                         _row = fGrid.Rows[string.Format("p_{0}", em.account_extension_purposeS.finance_and_analysis.ToByte())];
                         _row.Cells[0].Value = 0;
                         _row.Cells[1].BackColor = Color.Empty;
                         _row.Cells[0].Enabled = iGBool.False;
                         _row.Cells[0].AuxValue = 1;
                         _row.Cells[1].Enabled = iGBool.False;
                         _row.Selectable = false;
                     }

                 }
             }
         }
         
         public void ClearGrid()
         {
             if (fGrid.Rows.Count == 0) { return; }
             var nlist = from t in fGrid.Rows.Cast<iGRow>().AsQueryable<iGRow>()
                         where !string.IsNullOrEmpty(t.Key)
                         select t;
             fGrid.BeginUpdate();
             ic.church_group_typeC _cg_type = null;
             foreach (var _rw in nlist)
             {
                 _rw.Cells["desc"].Value = null;
                 _rw.Cells["desc"].AuxValue = null;
                 if (_rw.Tag != null)
                 {
                     if (_rw.Tag.GetType() == typeof(ic.church_group_typeC))
                     {
                         _cg_type = _rw.Tag as ic.church_group_typeC;
                        var _last_char = _cg_type.cg_type_name.Substring(_cg_type.cg_type_name.Length - 1);
                        var _disp_name = _cg_type.cg_type_name.Remove(_cg_type.cg_type_name.Length - 1);
                         _rw.Cells[1].Value = _last_char.ToLower() == "y" ? string.Format("{0}{1}", _disp_name, "ies") : _cg_type.cg_type_name;
                         _rw.Cells[0].Value = null;
                         _rw.Cells[0].BackColor = Color.Empty;
                         _rw.Cells[1].BackColor = Color.Empty;
                     }
                 }
             }
             foreach(var k in datam.DATA_ENUM_ACCOUNT_EXTENSION)
             {
                 fGrid.Rows[string.Format("p_{0}", k.Key.ToByte())].Cells[1].Value = k.Value;
                 fGrid.Rows[string.Format("p_{0}", k.Key.ToByte())].Cells[0].Value = 0;
                 fGrid.Rows[string.Format("p_{0}", k.Key.ToByte())].Cells[1].BackColor = Color.Empty;
                 fGrid.Rows[string.Format("p_{0}", k.Key.ToByte())].Cells[0].Enabled = iGBool.False;
             }
             fGrid.Rows["owner"].Visible = false;
             fGrid.Rows["owner_name"].Visible = false;
             fGrid.Rows["owner"].Cells[1].DropDownControl = null;
             fGrid.Rows["owner_name"].Visible = false;
             fGrid.Rows["owner"].Tag = null;
             //
             fGrid.Rows["end_date"].Enabled = iGBool.False;
             fGrid.EndUpdate();
             fGrid.Focus();
             if (!m_account.is_sys_account)
             {
                 fGrid.SetCurCell("account_name", 1);
             }
             else
             {
                 fGrid.SetCurCell("description", 1);
             }
            
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
             myCol.CellStyle.Selectable = iGBool.False;
           //  myCol.IncludeInSelect = false;
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
                 datam.GetDepartments(xd);
                 xd.CommitTransaction();
             }
             LoadDropDownLists();
             LoadGroupNames();
             LoadMainGrid();
             ClearGrid();
             FillAccountDetails(m_account);
             datam.HideWaitForm();
             fGrid.Focus();
              if(m_account.is_sys_account)
              {
             fGrid.SetCurCell("description", 1);
              }
              else
              {
                   fGrid.SetCurCell("account_name", 1);
              }
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
                  if (_row.Key == "account_category")
                  {
                      fGrid.Rows["group_name"].Cells[1].Value = null;
                  }
                  return;
              }
              if (_row.Key == "post_type")
              {
                  if (_row.Cells[1].Text.ToLower() == "yes")
                  {
                      if (fGrid.Rows["owner"].Cells[1].AuxValue == null)
                      {
                          if (!fGrid.Rows["sbalance"].Visible)
                          {
                              fGrid.Rows["sbalance"].Visible = true;
                          }
                      }
                  }
                  else
                  {
                      fGrid.Rows["sbalance"].Cells[1].Value = 0;
                      fGrid.Rows["sbalance"].Visible = false;
                  }
              }
              if (_row.Key == "account_name")
              {
                  using (var xd = new xing())
                  {
                      if (datam.DuplicateAccountName(_row.Cells[1].Text, xd, m_account.account_id))
                      {
                          MessageBox.Show("The Account Name You Have Entered Already Exists", "Duplicate Account Name");
                          _row.Cells[1].Value = m_account.account_name;
                          return;
                      }
                      else
                      {
                          _row.Cells[1].Value = _row.Cells[1].Text.ToProperCase();
                      }
                  }
              }
              if (_row.Key == "account_category")
              {
                  fGrid.Rows["group_name"].Cells[1].Value = fGrid.Rows["account_category"].Cells[1].Value;
              }
              if (_row.Key == "owner_type")
              {
                  var _val = (fGrid.Rows["owner_type"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag.ToStringNullable();
                  fGrid.Rows["owner"].Cells[1].AuxValue = null;
                  fGrid.Rows["owner"].Cells[1].Value = null;
                  fGrid.Rows["owner"].Cells[1].DropDownControl = null;
                  fGrid.Rows["owner"].Visible = true;
                  fGrid.Rows["sbalance"].Cells[1].Value = null;
                  fGrid.Rows["sbalance"].Visible = true;
                  fGrid.Rows["owner"].Tag = null;
                   switch (m_OWNER_Types[_val])
                  {
                      case em.AccountOwnerTypeS.CUC:case em.AccountOwnerTypeS.CUC_CHURCH:case em.AccountOwnerTypeS.CHURCH:case em.AccountOwnerTypeS.DISTRICT:
                          {
                              fGrid.Rows["owner"].Visible = false;
                              fGrid.Rows["sbalance"].Cells[1].Value = 0;
                              fGrid.Rows["sbalance"].Visible = false;
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
                              break;
                          }
                     case em.AccountOwnerTypeS.CHURCH_GROUP_SHARED:
                          {
                              fGrid.Rows["owner"].Visible = true;
                              fGrid.Rows["owner"].Cells[1].DropDownControl = m_CG_Shared;
                              //
                              fGrid.Rows["sbalance"].Cells[1].Value = 0;
                              fGrid.Rows["sbalance"].Visible = false;
                              break;
                          }
                  }
              }
              if (_row.Tag != null)
              {
                  if (_row.Tag.GetType() == typeof(ic.church_group_typeC))
                  {
                      _row.Cells[1].BackColor = (_row.Cells[0].Value.ToByte()) == 0 ? Color.Empty : Color.LightGreen;
                      fGrid.SetCurCell("post_type", 1);
                      if (!ChurchGroupChecked())
                      {
                          foreach (var k in datam.DATA_ENUM_ACCOUNT_EXTENSION)
                          {
                              fGrid.Rows[string.Format("p_{0}", k.Key.ToByte())].Cells[1].Value = k.Value;
                              fGrid.Rows[string.Format("p_{0}", k.Key.ToByte())].Cells[0].Value = 0;
                              fGrid.Rows[string.Format("p_{0}", k.Key.ToByte())].Cells[1].BackColor = Color.Empty;
                              _row.Cells[0].AuxValue = 1;
                              if(fGrid.Rows[string.Format("p_{0}", k.Key.ToByte())].Cells[0].AuxValue==null)
                              {
                                  fGrid.Rows[string.Format("p_{0}", k.Key.ToByte())].Cells[0].Enabled = iGBool.False;
                              }
                          }
                      }
                      else
                      {
                          foreach (var k in datam.DATA_ENUM_ACCOUNT_EXTENSION)
                          {
                             if (fGrid.Rows[string.Format("p_{0}", k.Key.ToByte())].Cells[0].AuxValue == null)
                              {
                                  fGrid.Rows[string.Format("p_{0}", k.Key.ToByte())].Cells[0].Enabled = iGBool.True; ;
                              }
                          }
                      }
                      return;
                  }
              }
              if (_row.Key == "p_0")
              {
                  _row.Cells[1].BackColor = (_row.Cells[0].Value.ToByte()) == 0 ? Color.Empty : Color.LightGreen;
                  fGrid.Rows["p_1"].Cells[1].BackColor = Color.Empty;
                  fGrid.Rows["p_1"].Cells[0].Value = null;
                  fGrid.SetCurCell("post_type", 1);
                  return;
              }
              if (_row.Key == "p_1")
              {
                  _row.Cells[1].BackColor = (_row.Cells[0].Value.ToByte()) == 0 ? Color.Empty : Color.LightGreen;
                  fGrid.Rows["p_0"].Cells[1].BackColor = Color.Empty;
                  fGrid.Rows["p_0"].Cells[0].Value = null;
                  fGrid.SetCurCell("post_type", 1);
                  return;
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
                      if (fGrid.Rows[k].Tag != null)
                      {
                          fGrid.SetCurCell(k, 0);
                          break;
                      }
                      if (!fGrid.Rows[k].Selectable) { continue; }
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
         private bool ChurchGroupChecked()
         {
             for (int j = m_Range[0]; j <= m_Range[1]; j++)
             {
                 if(fGrid.Rows[j].Cells[0].Value.ToInt16()==1)
                 {
                     return true;
                 }
             }
             return false;
         }
          private bool CheckPassed()
          {
              var _cols = new string[] { "account_name","account_status","start_date","sbalance","post_type" };
              foreach (var s in _cols)
              {
                  if (fGrid.Rows[s].Cells[1].Value == null)
                  {
                      MessageBox.Show("Important Field Left Blank", "Save Error");
                      fGrid.Focus();
                      fGrid.SetCurCell(s, 1);
                      return false;
                  }
              }
              if (ChurchGroupChecked())
              {
                  var _keys= new string[]{"p_0","p_1"};
                  var nlist = (from k in fGrid.Rows.Cast<iGRow>()
                               where _keys.Contains(k.Key) & k.Cells[0].Value.ToInt16() == 1
                               select k.Key).Count();
                  if (nlist == 0)
                  {
                      MessageBox.Show("Please Select The Purpose For Extending The Acccount", "Purpose Error");
                      return false;
                  }
              }
              return true;

          }
          private void buttonX1_Click(object sender, EventArgs e)
          {
              if (!CheckPassed()) { return; }
              string str = "Are You Sure You Want To Update This Account";
              if (!dbm.WarningMessage(str, "Create Account Warning"))
              {
                  return;
              }
              datam.ShowWaitForm("Updating Database, Please wait..");
              Application.DoEvents();
              ic.accountC _acc = m_account;
              _acc.extension_purpose = em.account_extension_purposeS.none;
              if (fGrid.Rows["p_0"].Cells[0].Value.ToInt16() == 1)
              {
                  _acc.extension_purpose = em.account_extension_purposeS.analysis_only;
              }
              if (fGrid.Rows["p_1"].Cells[0].Value.ToInt16() == 1)
              {
                  _acc.extension_purpose = em.account_extension_purposeS.finance_and_analysis;
              }
             var extended_cgs_ids = (from g in fGrid.Rows.Cast<iGRow>()
                                      where g.Tag != null & g.Cells[0].Value.ToByte() == 1
                                      select (g.Tag as ic.church_group_typeC).cg_type_id);
             string _ext_string = null;
              if (_acc.ExtCgTypeIds != null)
              {
                  _acc.ExtCgTypeIds.Clear();
              }
              if (extended_cgs_ids.Count() > 0)
              {
                  foreach (var y in extended_cgs_ids)
                  {
                      if (string.IsNullOrEmpty(_ext_string))
                      {
                          _ext_string = y.ToStringNullable();
                      }
                      else
                      {
                          _ext_string += "," + y.ToStringNullable();
                      }
                  }
                  _acc.ExtCgTypeIds = extended_cgs_ids.ToList();
              }
              string[] _cols = new string[]
                {
                    "ex_cg_type_ids",
                    "accounts_ext_purpose",
                    "account_id"
                };
                  using (var xd = new xing())
                  {
                      xd.UpdateFsTimeStamp("accounts_tb");
                      xd.SingleUpdateCommandALL("accounts_tb",
                          _cols,
                          new object[]
                      {
                          _ext_string,
                          _acc.extension_purpose,
                          _acc.account_id
                      }, 1);
                      xd.CommitTransaction();
                  }
                  if (this.Owner != null && this.Owner is IncomeAccountsManager)
                  {
                      (this.Owner as IncomeAccountsManager).EditRow(_acc);
                      this.Close();
                  }
                  else
                  {
                      this.Tag = _acc;
                      this.Close();
                  }
                  datam.HideWaitForm();
              
          }

          private void buttonX2_Click(object sender, EventArgs e)
          {
              ClearGrid();
              FillAccountDetails(m_account);
          }

          private void fGrid_Click(object sender, EventArgs e)
          {

          }

          private void fGrid_EllipsisButtonClick(object sender, iGEllipsisButtonClickEventArgs e)
          {
              if (fGrid.Rows[e.RowIndex].Key == "start_date")
              {

                  fGrid.Rows[e.RowIndex].Cells[1].Value = m_account.start_date;
                  fGrid.Rows[e.RowIndex].Cells[1].AuxValue = m_account.start_date;
              }
              if (fGrid.Rows[e.RowIndex].Key == "end_date")
              {
                  var _str = "Are You Sure You Want To Edit The End Date ??";
                  if(!dbm.WarningMessage(_str,"Edit End Date"))
                  {
                      return;
                  }
                  fGrid.Rows[e.RowIndex].Cells[1].Value = null;
                  fGrid.Rows[e.RowIndex].Cells[1].AuxValue = null;
              }
          }
    }
}
