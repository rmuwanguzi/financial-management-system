using SdaHelperManager.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SdaHelperManager;
namespace MTOMS
{
    public partial class MonthSelector : DevComponents.DotNetBar.Office2007Form
    {
        public MonthSelector()
        {
            InitializeComponent();
        }

        private void MonthSelector_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            LoadYears();
        }
        private void LoadYears()
        {
            comboBoxYear.Items.Clear();
            for (int j = sdata.CURR_DATE.Year; j >= 2014; j--)
            {
                comboBoxYear.Items.Add(j);
            }
        }

        private void comboBoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _yr = comboBoxYear.SelectedItem.ToInt32();
            if(_yr==sdata.CURR_DATE.Year)
            {

            }
        }
    }
}
