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
namespace MTOMS.BatchNo
{
    public partial class BatchNoSelector : Form
    {
        public BatchNoSelector()
        {
            InitializeComponent();
        }
        int sab_fs_id = 0;
        private void BatchNoSelector_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            sab_fs_id=this.Tag.ToInt32();
            var nlist = from k in datam.DATA_BATCH_NO_SETTINGS[sab_fs_id].Values
                        where k.entrant_id == sdata.PC_US_ID & k.batch_count != k.entrant_count
                        select k;
            foreach(var k in nlist)
            {
                comboBoxEx1.Items.Add(k);
            }
            this.Tag = null;
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxEx1.SelectedIndex<0)
            {
                buttonX1.Enabled = false;
            }
            buttonX1.Enabled = true;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Tag = comboBoxEx1.SelectedItem;
            this.Close();
        }
    }
}
