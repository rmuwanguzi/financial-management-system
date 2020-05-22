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
    public partial class ChildrenMaker : DevComponents.DotNetBar.Office2007Form
    {
        public ChildrenMaker()
        {
            InitializeComponent();
        }
        ic.memberC _mem_obj = null;
        private void ChildrenMaker_Load(object sender, EventArgs e)
        {
            xso.xso.Intialize();
            datam.SecurityCheck();
            datam.SystemInitializer();
            CenterToScreen();
           
            combochild.SelectedIndexChanged += new EventHandler(combochild_SelectedIndexChanged);
            combochild.SelectionChangeCommitted += new EventHandler(combochild_SelectionChangeCommitted);
            if (this.Tag == null)
            {
                _mem_obj = new MTOMS.ic.memberC();
            }
            else
            {
                _mem_obj = this.Tag as ic.memberC;
            }
            LoadMembers();
            combochild.Focus();
        }
        void combochild_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (combochild.SelectedIndex > -1)
            {
                txt_child_name.Clear();
                txt_birth_year.Clear();
                txt_child_name.Enabled = false;
                txt_birth_year.Enabled = false;
                var _mem = (combochild.SelectedItem as cbItemx).AttachedObject as ic.memberC;
                txt_child_name.Text = _mem.mem_name;
                txt_birth_year.Text = _mem.birth_yr.ToStringNullable();
            }   
        }
        void LoadMembers()
        {
            if (datam.DATA_MEMBER == null) { return; }
            //load father
            combochild.ClearSearchCollection();
            combochild.ResetControl();
            combochild.DataSource = null;
            var flist = from n in datam.DATA_MEMBER.Values
                       where n.age < (_mem_obj.age + 15)
                       select n;
            foreach (var m in flist)
            {
                combochild.AddToSearchCollection(new cbItemx()
                {
                    AttachedObject = m,
                    display_name = m.mem_name,
                    item_id = m.mem_id,
                    search_string = m.mem_name
                });
            }
            combochild.SortSearchCollection(ComboX.sort_type.DisplayASC);
           
        }
        void combochild_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combochild.SelectedIndex == -1)
            {
                picturechild.Image = null;
                txt_child_name.Clear();
                txt_birth_year.Clear();
                txt_child_name.Enabled = true;
                txt_birth_year.Enabled = true;
            }
            else
            {
                var _mem = (combochild.SelectedItem as cbItemx).AttachedObject as ic.memberC;
                if (_mem != null)
                {
                    txt_child_name.Text = _mem.mem_name;
                    if (_mem.objPicture != null)
                    {
                        picturechild.Image = fn.IMAGERESIZER(_mem.objPicture.SmallPicture, (0.75 * 102).ToFloat(), (0.75 * 119).ToFloat());
                    }
                    txt_birth_year.Text = _mem.birth_yr.ToStringNullable();
                }

            }
        }
        private void buttonsave_Click(object sender, EventArgs e)
        {
            if (txt_birth_year.Text.Trim().Length == 0 & txt_child_name.Text.Trim().Length == 0)
            {
                MessageBox.Show("Some Fields Have Not Been Filled In", "Missing Fields");
                return;
            }
            if(comboGender.SelectedIndex==-1)
            {
                MessageBox.Show("Please Enter The Gender", "Missing Fields");
                comboGender.Focus();
                return;
            }
            ic.children obj = new MTOMS.ic.children();
            obj.child_name = txt_child_name.Text.Trim();
            obj.birth_year = txt_birth_year.Text.ToInt32();
            obj.gender_id = comboGender.SelectedIndex == 0 ? 1 : 0;
            if (combochild.SelectedIndex > -1)
            {
                var _mem = (combochild.SelectedItem as cbItemx).AttachedObject as ic.memberC;
                if (_mem != null)
                {
                    obj.child_mem_id = _mem.mem_id;
                }
            }
            if (this.Owner.GetType() == typeof(MemberMaker2))
            {
                (this.Owner as MemberMaker2).FormCommunicate(obj, MemberMaker2.SentMsg.Add_children, null);
            }
           
            txt_birth_year.Clear();
            txt_child_name.Clear();
            combochild.ResetControl();
            combochild.SelectedIndex = -1;
            combochild.Focus();
        }

        private void buttoncanc_Click(object sender, EventArgs e)
        {
            txt_birth_year.Clear();
            txt_child_name.Clear();
            combochild.ResetControl();
            combochild.SelectedIndex = -1;
            combochild.Focus();
        }

       
        
    }
}
