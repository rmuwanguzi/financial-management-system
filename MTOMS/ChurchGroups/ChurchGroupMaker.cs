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
namespace MTOMS.ChurchGroups
{
    public partial class ChurchGroupMaker : DevComponents.DotNetBar.Office2007Form
    {
        public ChurchGroupMaker()
        {
            InitializeComponent();
        }
        bool is_edited = false;
        private void ChurchGroupMaker_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            this.FormClosing += ChurchGroupMaker_FormClosing;
            using (var xd = new xing())
            {
                datam.fill_church_group_types(xd);
                datam.fill_church_groups(xd);
                xd.CommitTransaction();
            }
            foreach (var k in datam.DATA_CG_TYPES.Values)
            {
                comboBoxEx1.Items.Add(k);
            }
           
        }

        void ChurchGroupMaker_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (is_edited)
            {
                this.Tag = 1;
            }
        }
         private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxEx1.SelectedIndex==-1)
            {
                textBoxSender.Clear();
                textBoxSender.Enabled=false;
            }
        }

        private void buttoncancel_Click(object sender, EventArgs e)
        {
            comboBoxEx1.SelectedIndex = -1;
        }
         private void comboBoxEx1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!textBoxSender.Enabled)
            {
               textBoxSender.Enabled = true;
            }
            textBoxSender.Focus();
        }
       private void textBoxSender_TextChanged(object sender, EventArgs e)
         {
             if (textBoxSender.Text.Trim().Length == 0)
             {
                 buttonsave.Enabled = false;
             }
             else
             {
                 if (!buttonsave.Enabled) { buttonsave.Enabled = true; }
             }
         }

       private void buttonsave_Click(object sender, EventArgs e)
       {
           #region get type
           ic.church_group_typeC _type = comboBoxEx1.SelectedItem as ic.church_group_typeC;
           //em.church_group_typeS _type = em.church_group_typeS.Family;
           //switch (comboBoxEx1.SelectedIndex)
           //{
           //    case 0:
           //        {
           //            _type = em.church_group_typeS.Family; break;
           //        }
           //    case 1:
           //        {
           //            _type = em.church_group_typeS.City; break;
           //        }
           //    case 2:
           //        {
           //            _type = em.church_group_typeS.LessonClass; break;
           //        }
           //    case 3:
           //        {
           //            _type = em.church_group_typeS.BuildingClass; break;
           //        }
           //    case 4:
           //        {
           //            _type = em.church_group_typeS.FellowShipGroup; break;
           //        }
           //    case 5:
           //        {
           //            _type = em.church_group_typeS.PrayerGroup; break;
           //        }
           //    case 6:
           //        {
           //            _type = em.church_group_typeS.CookingClass; break;
           //        }
           //}

           #endregion
           string _str = textBoxSender.Text.Trim();
           if (datam.DATA_CHURCH_GROUPS != null)
           {
               var cnt = (from v in datam.DATA_CHURCH_GROUPS.Values
                          where v.cg_type_id == _type.cg_type_id & v.cg_name.ToLower() == _str.ToLower()
                          select v).Count();
               if (cnt > 0)
               {
                   _str = "A Group Name With The Same Name Already Exists";
                   dbm.ErrorMessage(_str, "Duplicate Group Name");
                   return;
               }
           }
           ic.church_groupC _c = new MTOMS.ic.church_groupC();
           _c.cg_type_id = _type.cg_type_id;
           _c.cg_name = _str.ToProperCase();
           string[] _cols = new string[]
           {
               "cg_name",
               "cg_type_id",
               "exp_type",
               "lch_id",
               "lch_type_id",
               "fs_time_stamp"
           };
           object[] _row = new object[]
           {
            _c.cg_name,
            _c.cg_type_id,
            33,datam.LCH_ID,datam.LCH_TYPE_ID,0
            };
           using(var xd = new xing())
           {
               if (xd.ExecuteScalarInt(string.Format("select count(cg_type_id) as cnt from church_group_tb where cg_type_id={0}", _c.cg_type_id)) == 0)
               {
                   _type.sys_account_id = accn.CreateChildGroupAccount(xd, -2368, _type.cg_type_name).account_id;
                   xd.SingleUpdateCommandALL("church_group_types_tb", new string[]
                       {
                           "sys_account_id",
                           "cg_type_id"
                       }, new object[]
                       {
                           _type.sys_account_id,
                           _type.cg_type_id

                       }, 1);
               }
               if (_type.sys_account_id == 0)
               {
                   _type.sys_account_id = xd.ExecuteScalarInt("select sys_account_id from church_group_types_tb where cg_type_id=" + _type.cg_type_id);
               }
               _c.cg_id = xd.SingleInsertCommandInt("church_group_tb", _cols, _row);
               _c.sys_account_id = accn.CreateChildGroupAccount(xd, _type.sys_account_id, _c.cg_name).account_id;
               xd.SingleUpdateCommandALL("church_group_tb", new string[]
                       {
                           "sys_account_id",
                           "cg_id"
                       }, new object[]
                       {
                           _c.sys_account_id,
                           _c.cg_id
                       }, 1);
               //create general expense account for this 
               
               xd.CommitTransaction();
           }
           if (datam.DATA_CHURCH_GROUPS == null) { datam.DATA_CHURCH_GROUPS = new SortedList<int, MTOMS.ic.church_groupC>(); }
           datam.DATA_CHURCH_GROUPS.Add(_c.cg_id, _c);
           if (this.Owner is ChurchGroupManager2)
           {
               (this.Owner as ChurchGroupManager2).NewGroup(_c);
           }
          
           sdata.ClearFormCache(em.fm.chart_of_accounts.ToInt16());
           sdata.ClearFormCache(em.fm.income_accounts_settings.ToInt16());
           sdata.ClearFormCache(em.fm.creditors.ToInt16());
           is_edited = true;
           textBoxSender.Clear();
           textBoxSender.Focus();
       }
       private void textBoxSender_KeyPress(object sender, KeyPressEventArgs e)
       {
           if(textBoxSender.TextLength>0 & (e.KeyChar.ToByte()==Keys.Enter.ToByte()))
           {
               buttonsave.PerformClick();
           }
       }

       private void buttonX1_Click(object sender, EventArgs e)
       {
           using (var _fm = new ChurchGroupTypeMaker())
           {
               _fm.ShowDialog();
               if (_fm.Tag != null)
               {
                   comboBoxEx1.SelectedIndex = -1;
                   comboBoxEx1.Items.Clear();
                   foreach (var k in datam.DATA_CG_TYPES.Values)
                   {
                       comboBoxEx1.Items.Add(k);
                   }
               }
           }
       }

       private void buttonX2_Click(object sender, EventArgs e)
       {
           using (var xd = new xing())
           {
               datam.fill_church_group_types(xd);
               xd.CommitTransaction();
           }
           comboBoxEx1.SelectedIndex = -1;
           comboBoxEx1.Items.Clear();
           foreach (var k in datam.DATA_CG_TYPES.Values)
           {
               comboBoxEx1.Items.Add(k);
           }
       }
    }
}
