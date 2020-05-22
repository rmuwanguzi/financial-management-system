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
    public partial class NeighbourMaker : DevComponents.DotNetBar.Office2007Form
    {
        public NeighbourMaker()
        {
            InitializeComponent();
        }
        form_data fdata = null;

        private void NeighbourMaker_Load(object sender, EventArgs e)
        {
            xso.xso.Intialize();
            datam.SecurityCheck();
            fdata = new form_data();
            CenterToScreen();
            buttonsave.Enabled = false;
            txt_neigh_name.Select(); txt_neigh_name.Focus();

        }
        private void buttonsave_Click(object sender, EventArgs e)
        {
            ic.neighbour obj = new MTOMS.ic.neighbour();
            obj.neighbour_name = txt_neigh_name.Text.Trim();
            obj.neigh_phone = txt_neigh_phone.Text.ToStringNullable();
            if (this.Owner.GetType() == typeof(MemberMaker2))
            {
                (this.Owner as MemberMaker2).FormCommunicate(obj, MemberMaker2.SentMsg.Add_neighbour, null);
            }
            //else
            //{
            //    (this.Owner as MemberUpdater).FormCommunicate(obj, MemberUpdater.SentMsg.Add_neighbour, null);
            //} txt_neigh_phone.Clear();
            buttonsave.Enabled = false;
            txt_neigh_name.Clear();
            txt_neigh_name.Select(); txt_neigh_name.Focus();
        }

        private void txt_neigh_name_TextChanged(object sender, EventArgs e)
        {
            if (txt_neigh_name.Text.Trim().Length == 0)
            {
                buttonsave.Enabled = false;
            }
            else
            {

                buttonsave.Enabled = true;
            }
        }

        private void txt_neigh_phone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonsave.PerformClick();
            }
        }
    }
}
