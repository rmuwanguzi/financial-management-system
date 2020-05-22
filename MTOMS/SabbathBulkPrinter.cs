using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SdaHelperManager;
using XPrintTypesPluggin;

namespace MTOMS
{
    public partial class SabbathBulkPrinter : DevComponents.DotNetBar.Office2007Form
    {
        public SabbathBulkPrinter()
        {
            InitializeComponent();
        }
        private List<int> m_SabIDS = new List<int>();
        public SortedList<int, IEnumerable<ic.off_receipt>> m_DATA = null;
        private enum _operation
        {
            formload,
            sendingemail
        }
        _operation m_operation = _operation.formload;
        fs_class m_sabbath = null;
       // private MTOMS.Print.A4PrintManager print_document = null;

        bool _is_working = false;
        private void SabbathBulkPrinter_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            datam.SecurityCheck();
            this.BackColor = Color.WhiteSmoke;
            m_operation = _operation.formload;
            m_DATA = new SortedList<int, IEnumerable<MTOMS.ic.off_receipt>>();
            this.FormClosing += new FormClosingEventHandler(SabbathBulkPrinter_FormClosing);
            comboPaper.Items.Add("A4 Paper");
            comboPaper.Items.Add("Small Paper");
            backworker.RunWorkerAsync();
           
          
        }
        void SabbathBulkPrinter_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_is_working)
            {
                e.Cancel = true;
            }
        }
        private void GetPendingSabbathIds()
        {
            string _str = null;
            _str = "select distinct sab_fs_id from offering_master_tb order by sab_fs_id desc";
            using (var _dr = dbm.SelectCommand(_str))
            {
                if (_dr == null ) { return; }
                while (_dr.Read())
                {
                    m_SabIDS.Add(_dr[0].ToInt32());
                }
            }
            if (m_SabIDS.Count == 0)
            {
                combodate.Items.Clear();
                combodate.SelectedIndex = -1;
                combodate.Enabled = false;
            }
            else
            {
                //do the loading
                int _yr = 0;
                List<fs_class> _fs_list = new List<fs_class>();
                foreach (var t in m_SabIDS)
                {
                    _yr = t.ToString().Substring(0, 4).ToInt32();
                    try
                    {
                        _fs_list.Add(datam.DATA_FS[_yr][t]);
                    }
                    catch (Exception ex)
                    {
                        datam.fill_data_fs(_yr);
                        _fs_list.Add(datam.DATA_FS[_yr][t]);
                    }
                }
                //
                combodate.DataSource = _fs_list;
                combodate.DisplayMember = "fs_date_long";
                combodate.ValueMember = "fs_date_long";
                if (combodate.Items.Count > 0)
                {
                    combodate.SelectedIndex = -1;
                    combodate.Enabled = true;
                }
            }

        }
        private void backworker_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (m_operation)
            {
                case _operation.formload:
                    {
                        datam.SystemInitializer();   
                        break;
                    }
                case _operation.sendingemail:
                    {
                      //  e.Result = SendReceipts();
                        break;
                    }
            }
        }
        private void backworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch (m_operation)
            {
                case _operation.formload:
                    {
                       
                        GetPendingSabbathIds();
                        GetInstalledPrinters();
                         break;
                    }
                case _operation.sendingemail:
                    {
                        bool success = (bool)e.Result;
                         _is_working = false;
                        buttonstart.Enabled = true;
                      break;
                    }

            }
        }
        private void combodate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            labelcnt.Text = string.Empty;
            m_sabbath = combodate.SelectedItem as fs_class;
            if (m_sabbath == null) { return; }
            //fill datasource
            if (!m_DATA.Keys.Contains(m_sabbath.fs_id))
            {
                m_DATA.Add(m_sabbath.fs_id, null);
                datam.UpdateSabbathPool(m_sabbath.fs_id, m_sabbath.fs_id, m_sabbath.fs_year);
                m_DATA[m_sabbath.fs_id] = from r in datam.SabbathQuery(m_sabbath.fs_id, m_sabbath.fs_id)
                                          where r.receipt_status == em.off_receipt_statusS.Valid 
                                          select r;
                //load from the database
            }
            int _result = m_DATA[m_sabbath.fs_id].Count();
            labelcnt.Text = _result.ToString();
            if (_result == 0)
            {
                buttonstart.Enabled = false;
                return;
            }
         }
        private void buttonstart_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (comboPaper.SelectedIndex == 0)
                {
                    PrintPreviewDialog pdlg = new PrintPreviewDialog();
                    print_document = new MTOMS.Print.A4PrintManager();
                    pdlg.Document = print_document;
                    print_document.OFFCollection = m_DATA[m_sabbath.fs_id].ToList<ic.off_receipt>();
                    pdlg.WindowState = FormWindowState.Maximized;
                    pdlg.PrintPreviewControl.Zoom = 1;
                    pdlg.ShowDialog();
                }
            }
        }
        private void buttonX2_Click(object sender, EventArgs e)
       {
           comboPaper.SelectedIndex = -1;
           checkBox1.Checked = false;
           comboprinter.SelectedIndex = -1;
           m_sabbath = null;
           _is_working = false;
           labelcnt.Text = string.Empty;
           combodate.SelectedIndex = -1;
       }
        private void combodate_SelectedIndexChanged(object sender, EventArgs e)
        {
           

        }
        private void comboPaper_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboPaper.SelectedIndex < 0)
            {
                buttonstart.Enabled = false;
                return;
            }
            if (comboPaper.SelectedIndex > -1 )
            {
                buttonstart.Enabled = true;
                if (comboPaper.SelectedIndex == 0)
                { 
                    //a4
                    var _str = (from t in datap.PrinterTypes
                                where t.Key == datap.printer_type.A4Printer
                                select t.Value);
                    foreach (var n in _str)
                    {
                        comboprinter.SelectedItem = n;
                    }
                }
                else
                {
                    var _str = (from t in datap.PrinterTypes
                                where t.Key == datap.printer_type.ReceiptPrinter
                                select t.Value);
                    foreach (var n in _str)
                    {
                        comboprinter.SelectedItem = n;
                    }

                }
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox1.ForeColor = Color.Green;
            }
            else
            {
                checkBox1.ForeColor = Color.Black;
            }
        }
        private void GetInstalledPrinters()
        {
            short sel_index = -1;
            #region load installed printers
            //load printers
            comboprinter.Items.Clear();
            String pkInstalledPrinters;
            for (short i = 0; i < System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count; i++)
            {
                pkInstalledPrinters = System.Drawing.Printing.PrinterSettings.InstalledPrinters[i];
                //if (pkInstalledPrinters == print_document.PageSettingsReadOnly.PrinterSettings.PrinterName.ToString())
                //{
                //    default_printer_name = print_document.PageSettingsReadOnly.PrinterSettings.PrinterName.ToString();
                //    sel_index = i;
                //}
                comboprinter.Items.Add(pkInstalledPrinters);
            }
            comboprinter.SelectedIndex = sel_index;
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (XPrintTypesPluggin.PrintTypeManager _fm = new XPrintTypesPluggin.PrintTypeManager())
            {
                _fm.ShowDialog();
            }
        }
    }
}
