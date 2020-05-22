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
    public partial class ParentsMaker : DevComponents.DotNetBar.Office2007Form
    {
        public ParentsMaker()
        {
            InitializeComponent();
        }
        ic.memberC _mem_obj = null;
        private void ParentsMaker_Load(object sender, EventArgs e)
        {
            xso.xso.Intialize();
            datam.SecurityCheck();
            CenterToScreen();
            datam.SystemInitializer();
            combomother.SelectedIndexChanged += new EventHandler(combomother_SelectedIndexChanged);
            combomother.SelectionChangeCommitted += new EventHandler(combomother_SelectionChangeCommitted);
            //
            combofather.SelectedIndexChanged += new EventHandler(combofather_SelectedIndexChanged);
            combofather.SelectionChangeCommitted += new EventHandler(combofather_SelectionChangeCommitted);
            //
            comboguardian.SelectedIndexChanged += new EventHandler(comboguardian_SelectedIndexChanged);
            comboguardian.SelectionChangeCommitted += new EventHandler(comboguardian_SelectionChangeCommitted);
            buttonsave.Enabled = false;
            buttoncanc.PerformClick();
            if (this.Tag == null)
            {
                _mem_obj = new MTOMS.ic.memberC();
            }
            else
            {
                _mem_obj = this.Tag as ic.memberC;
            }
            LoadMembers();
        }
        void comboguardian_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboguardian.SelectedIndex > -1)
            {
                textguardian.Clear();
                textphoneguardian.Clear();
                textphoneguardian.Enabled = false;
                textguardian.Enabled = false;
                var _mem = (comboguardian.SelectedItem as cbItemx).AttachedObject as ic.memberC;
                textguardian.Text = _mem.mem_name;
                if (_mem.objAddress != null)
                {
                    textphoneguardian.Text = _mem.objAddress.phone1;
                }
            }   
        }
        void comboguardian_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboguardian.SelectedIndex == -1)
            {
                pictureguardian.Image = null;
                textguardian.Clear();
                textphoneguardian.Clear();
                textphoneguardian.Enabled = true;
                textguardian.Enabled = true;
            }
            else
            {
                var _mem = (comboguardian.SelectedItem as cbItemx).AttachedObject as ic.memberC;
                textguardian.Text = _mem.mem_name;
                if (_mem.objAddress != null)
                {
                    textphoneguardian.Text = _mem.objAddress.phone1;
                }
                if (_mem.objPicture != null)
                {
                    pictureguardian.Image = fn.IMAGERESIZER(_mem.objPicture.SmallPicture, (0.75 * 102).ToFloat(), (0.75 * 119).ToFloat());
                }

            }
        }
        void combofather_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (combofather.SelectedIndex > -1)
            {
                textfather.Clear();
                textphonefather.Clear();
                textphonefather.Enabled = false;
                textfather.Enabled = false;
                var _mem = (combofather.SelectedItem as cbItemx).AttachedObject as ic.memberC;
                textfather.Text = _mem.mem_name;
                if (_mem.objAddress != null)
                {
                    textphonefather.Text = _mem.objAddress.phone1;
                }
            }
        }
        void combofather_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combofather.SelectedIndex == -1)
            {
                picturefather.Image = null;
                textfather.Clear();
                textphonefather.Clear();
                textphonefather.Enabled = true;
                textfather.Enabled = true;
            }
            else
            {
                var _mem = (combofather.SelectedItem as cbItemx).AttachedObject as ic.memberC;
                textfather.Text = _mem.mem_name;
                if (_mem.objAddress != null)
                {
                    textphonefather.Text = _mem.objAddress.phone1;
                }
                if (_mem.objPicture != null)
                {
                    picturefather.Image = fn.IMAGERESIZER(_mem.objPicture.SmallPicture, (0.75 * 102).ToFloat(), (0.75 * 119).ToFloat());
                }

            }
        }
        void combomother_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (combomother.SelectedIndex > -1)
            {
                textmother.Clear();
                textphonemother.Clear();
                textphonemother.Enabled = false;
                textmother.Enabled = false;
                var _mem = (combomother.SelectedItem as cbItemx).AttachedObject as ic.memberC;
                textmother.Text = _mem.mem_name;
                if (_mem.objAddress != null)
                {
                    textphonemother.Text = _mem.objAddress.phone1;
                }
            }
        }
        void combomother_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combomother.SelectedIndex == -1)
            {
                picturemother.Image = null;
                textmother.Clear();
                textphonemother.Clear();
                textphonemother.Enabled = true;
                textmother.Enabled = true;
            }
            else
            {
                var _mem = (combomother.SelectedItem as cbItemx).AttachedObject as ic.memberC;
                textmother.Text = _mem.mem_name;
                if (_mem.objAddress != null)
                {
                    textphonemother.Text = _mem.objAddress.phone1;
                }
                if (_mem.objPicture != null)
                {
                    picturemother.Image = fn.IMAGERESIZER(_mem.objPicture.SmallPicture, (0.75 * 102).ToFloat(), (0.75 * 119).ToFloat());
                }

            }
        }
        void LoadMembers()
        {
            if (datam.DATA_MEMBER == null) { return; }
            //load father
            combofather.ClearSearchCollection();
            combofather.ResetControl();
            combofather.DataSource = null;
            var flist = from n in datam.DATA_MEMBER.Values
                        where n.gender_id == em.xgender.Male.ToByte() &
                        n.age > (_mem_obj.age + 15)
                        select n;
            foreach (var m in flist)
            {
                combofather.AddToSearchCollection(new cbItemx()
                {
                    AttachedObject = m,
                    display_name = m.mem_name,
                    item_id = m.mem_id,
                    search_string = m.mem_name
                });
            }
            combofather.SortSearchCollection(ComboX.sort_type.DisplayASC);
            //load mother
            combomother.ClearSearchCollection();
            combomother.ResetControl();
            combomother.DataSource = null;
            var mlist = from n in datam.DATA_MEMBER.Values
                        where n.gender_id == em.xgender.Female.ToByte() &
                        n.age > (_mem_obj.age + 15)
                        select n;
            foreach (var m in mlist)
            {
                combomother.AddToSearchCollection(new cbItemx()
                {
                    AttachedObject = m,
                    display_name = m.mem_name,
                    item_id = m.mem_id,
                    search_string = m.mem_name
                });
            }
            combomother.SortSearchCollection(ComboX.sort_type.DisplayASC);
            //load guardian
            comboguardian.ClearSearchCollection();
            comboguardian.ResetControl();
            comboguardian.DataSource = null;
            var glist= from n in datam.DATA_MEMBER.Values
                        where n.age > _mem_obj.age
                        select n;
            foreach (var m in glist)
            {
                comboguardian.AddToSearchCollection(new cbItemx()
                {
                    AttachedObject = m,
                    display_name = m.mem_name,
                    item_id = m.mem_id,
                    search_string = m.mem_name
                });
            }

        }
        private void buttonsave_Click(object sender, EventArgs e)
        {
            ic.children obj = new MTOMS.ic.children();
            obj.child_name = textfather.Text.Trim();
            obj.birth_year = textphonefather.Text.ToInt32();
            if (this.Owner.GetType() == typeof(MemberMaker2))
            {
                (this.Owner as MemberMaker2).FormCommunicate(obj, MemberMaker2.SentMsg.Add_children, null);
            }
            
            textphonefather.Clear();
            buttonsave.Enabled = false;
            textfather.Clear();
            textfather.Select();textfather.Focus();
        }
        private void buttoncanc_Click(object sender, EventArgs e)
        {

            textfather.Clear();
            textmother.Clear();
            textguardian.Clear();
            //
            textphoneguardian.Clear();
            textphonemother.Clear();
            textphoneguardian.Clear();
            //
            combofather.ResetControl();
            combofather.SelectedIndex = -1;

            combomother.ResetControl();
            combomother.SelectedIndex = -1;

            comboguardian.ResetControl();
            comboguardian.SelectedIndex = -1;

            combofather.Focus();
        }

       
    }
}
