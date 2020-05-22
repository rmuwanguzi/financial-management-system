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
using TenTec.Windows.iGridLib;

namespace MTOMS
{
    public partial class AccountStatementForm : DevComponents.DotNetBar.Office2007Form
    {
        public AccountStatementForm()
        {
            InitializeComponent();
        }
        int m_sel_year = 0;
        private int _fs_id_1 = 0;
        private int _fs_id_2 = 0;
        bool app_working = false;
        bool special_day = false;
        bool prev_year = false;
        int m_BF = 0;
        List<int> m_ParentExeceptions { get; set; }
        ic.accountC m_account { get; set; }
        DateTime? m_account_start_date { get; set; }
        int m_opening_balance = 0;
        string m_account_name = string.Empty;
        private void TrailBalanceForm_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            datam.SecurityCheck();
            m_account = this.Tag as ic.accountC;
            datam.ShowWaitForm();
            Application.DoEvents();
            app_working = true;
            dateTimePicker1.ValueChanged += new EventHandler(dateTimePicker1_ValueChanged);
            dateTimePicker2.ValueChanged += new EventHandler(dateTimePicker2_ValueChanged);
            dateTimePicker1.Value = dateTimePicker1.MinDate;
            dateTimePicker2.Value = dateTimePicker1.MinDate;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            app_working = false;
            comboyear.Enabled = false;
            panelmain.Text = m_account.account_name;
            InitializeGridColumnMain();
            m_ParentExeceptions = new List<int>();
            m_ParentExeceptions.Add(accn.GetAccountByAlias("CUC_PAYABLE").account_id);
            m_ParentExeceptions.Add(accn.GetAccountByAlias("UNBANKED").account_id);
            //if (m_ParentExeceptions.IndexOf(m_account.p_account_id) > -1)
            //{
            //    MessageBox.Show("This Operation Has Not Activated For This Account", "Pending Action");
            //    datam.HideWaitForm();
            //    this.Close();
            //    return;
            //}
            //if (m_account.account_id == accn.GetAccountByAlias("CUC_PAYABLE").account_id)
            //{
            //    MessageBox.Show("This Operation Has Not Activated For This Account", "Pending Action");
            //    datam.HideWaitForm();
            //    this.Close();
            //    return;
            //}
            backworker1.RunWorkerAsync();
            
        }
        private void InitializeGridColumnMain()
        {
            #region Columns To Display
            List<iGrid> _grids = new List<iGrid>()
            {
                iGrid1
            };
            Dictionary<string, string> grid_cols = new Dictionary<string, string>();
            grid_cols.Add("date", "Date");
            grid_cols.Add("desc", "Description");
            grid_cols.Add("account", "Account\nName");
            grid_cols.Add("ref_type", "Reference\nType");
            grid_cols.Add("ref_no", "Reference\nNo");
            //grid_cols.Add("dr", "Dr\nAmount");
            //grid_cols.Add("cr", "Cr\nAmount");
            //
            grid_cols.Add("dr", string.Empty);
            grid_cols.Add("cr", string.Empty);
            //
            grid_cols.Add("balance", "Balance");
            iGCol myCol;
            foreach (var _grid in _grids)
            {

                foreach (var c in grid_cols)
                {
                    myCol = _grid.Cols.Add(c.Key, c.Value);
                    myCol.IncludeInSelect = true;
                    myCol.SortType = iGSortType.None;
                    //  myCol.ColHdrStyle.BackColor = Color.AntiqueWhite;
                    myCol.ColHdrStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    myCol.ColHdrStyle.ForeColor = Color.Black;
                    myCol.ColHdrStyle.Font = new Font("georgia", 13, FontStyle.Regular);
                    myCol.AllowSizing = true;
                    myCol.Width = 40;
                    myCol.ColHdrStyle.BackColor = Color.Thistle;
                }
                _grid.Cols["dr"].ColHdrStyle.ImageList = imageList1;
                _grid.Cols["dr"].ColHdrStyle.ImageAlign = iGContentAlignment.MiddleCenter;
                _grid.Cols["dr"].ImageIndex = 0;
                //_grid.Cols["dr"].ColHdrStyle.Font = new Font("georgia", 20, FontStyle.Bold);
                _grid.Cols["dr"].ColHdrStyle.BackColor = Color.LightGreen;

                //
                _grid.Cols["cr"].ColHdrStyle.ImageList = imageList1;
                _grid.Cols["cr"].ColHdrStyle.ImageAlign = iGContentAlignment.MiddleCenter;
                _grid.Cols["cr"].ImageIndex = 1;
                _grid.Cols["cr"].ColHdrStyle.BackColor = Color.Maroon;


                _grid.Cols["desc"].Width = 300;
                _grid.Cols["desc"].AllowSizing = false;
                
                _grid.Cols["desc"].CellStyle.TextFormatFlags = iGStringFormatFlags.WordWrap;
                //
                _grid.Cols["ref_no"].Width = 150;
                _grid.Cols["ref_no"].AllowSizing = false;

                _grid.Cols["ref_no"].CellStyle.TextFormatFlags = iGStringFormatFlags.WordWrap;
               
                _grid.Cols["dr"].CellStyle.FormatString = "{0:N0}";
                _grid.Cols["dr"].CellStyle.ValueType = typeof(int);
                _grid.Cols["cr"].CellStyle.FormatString = "{0:N0}";
                _grid.Cols["cr"].CellStyle.ValueType = typeof(int);
                _grid.Cols["balance"].CellStyle.FormatString = "{0:N0}";
                _grid.Cols["balance"].CellStyle.ValueType = typeof(int);
                _grid.DefaultRow.Height = _grid.GetPreferredRowHeight(true, false);
                _grid.DefaultAutoGroupRow.Height = _grid.DefaultRow.Height;
               // _grid.Cols.AutoWidth();
            }

            #endregion
        }
        
        private void LoadYears()
        {
            comboyear.Items.Clear();
            for (int i = fn.GetServerDate().Year; i >= 2010; i--)
            {
                comboyear.Items.Add(i.ToString());
            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            if (app_working) { return; }
            app_working = true;
            if (special_day)
            {
                dateTimePicker2.MaxDate = dateTimePicker1.MaxDate;
                dateTimePicker2.MinDate = dateTimePicker1.Value;
            }
            else
            {
                dateTimePicker2.MaxDate = sdata.CURR_DATE;
                dateTimePicker2.MinDate = dateTimePicker1.Value;

            }

            if (!prev_year & checkdate.Checked == false)
            {
                dateTimePicker2.Value = dateTimePicker1.Value;
            }
            Application.DoEvents();
            _fs_id_1 = fn.GetFSID(dateTimePicker1.Value);
            _fs_id_2 = fn.GetFSID(dateTimePicker2.Value);
            if (_fs_id_1 > _fs_id_2)
            {
                app_working = true;
                dateTimePicker2.Value = dateTimePicker1.Value;
                _fs_id_2 = _fs_id_1;
                app_working = false;
            }
            if (timer1.Enabled) { timer1.Enabled = false; }
            timer1.Enabled = true;

            app_working = false;
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (app_working) { return; }
            Application.DoEvents();
            _fs_id_1 = fn.GetFSID(dateTimePicker1.Value);
            _fs_id_2 = fn.GetFSID(dateTimePicker2.Value);
            if (timer1.Enabled) { timer1.Enabled = false; }
            timer1.Enabled = true;
        }
        private string GetReferenceTypeName(em.account_AL_referenceTypeS _type)
        {
            switch(_type)
            {
                case em.account_AL_referenceTypeS.receipt:
                    {
                        return "RECEIPT";
                    }
                case em.account_AL_referenceTypeS.voucher:
                    {
                        return "VOUCHER";
                    }
                case em.account_AL_referenceTypeS.cheque:
                    {
                        return "CHEQUE";
                    }
                case em.account_AL_referenceTypeS.banking_slip:
                    {
                        return "BANK SLIP";
                    }
                case em.account_AL_referenceTypeS.bank_transfer:
                    {
                        return "BANK TRANSFER";
                    }
                case em.account_AL_referenceTypeS.account_transfer:
                    {
                        return "CASH TRANSFER";
                    }
            }
            return _type.ToStringNullable();
        }
        private void LoadGrid()
        {
            if (accn.DATA_ASSET_LIABILITY_STATEMENT == null )
            {
                return;
            }
            var nlist = from k in accn.DATA_ASSET_LIABILITY_STATEMENT
                        orderby k.fs_id
                        select k;
            iGrid1.BeginUpdate();
            iGRow _row=null;
            int _balance = 0;
            //
            _row = iGrid1.Rows.Add();
            _row.ReadOnly = iGBool.True;
            _row.Font = new Font("verdana", 11, FontStyle.Regular);
            _row.Cells["date"].Value = dateTimePicker1.Value.ToMyShortDate();
            _row.Cells["desc"].Value = "Balance B/F";
            _row.Cells["desc"].Font = new Font("verdana", 11, FontStyle.Bold);
            _row.Cells["desc"].Col.Width = 200;
            _row.Cells["desc"].TextAlign = iGContentAlignment.MiddleCenter;
            _row.TextAlign = iGContentAlignment.MiddleCenter;

            if (m_BF >= 0)
            {
                 _row.Cells["dr"].Value = m_BF;
            }
            else
            {
                  _row.Cells["cr"].Value = (m_BF * -1);
            }

            _balance += m_BF;
           
            _row.Cells["balance"].Value = _balance;
            _row.Cells["balance"].ForeColor = Color.Blue;
            _row.Cells["balance"].TextAlign = iGContentAlignment.MiddleCenter;
            _row.Cells["balance"].BackColor = Color.AntiqueWhite;
            _row.Cells["balance"].ForeColor = Color.Blue;
            _row.AutoHeight();
            //
            _row = iGrid1.Rows.Add();
            _row.Height = 5;
            _row.BackColor = Color.FromArgb(64, 64, 64);
            _row.Selectable = false;
            _row.Key = "fixed_row";

            foreach (var k in nlist)
            {
                _row = iGrid1.Rows.Add();
                _row.ReadOnly = iGBool.True;
                _row.Font = new Font("verdana", 11, FontStyle.Regular);
                _row.Cells["date"].Value = k.fs_date.ToMyLongDate();
                _row.Cells["desc"].Value = k.description;
                _row.Cells["desc"].Col.Width = 200;
                _row.Cells["ref_type"].Value = GetReferenceTypeName(k.reference_type);
                _row.Cells["ref_type"].ForeColor = Color.Gray;
                _row.Cells["ref_no"].Value = k.reference_no;
                _row.Cells["account"].Value = k.account_name;
                _row.TextAlign = iGContentAlignment.MiddleCenter;
                _row.Cells["ref_no"].ForeColor = Color.Gray;
                if (k.dr_amount > 0)
                {
                    _row.Cells["dr"].Value = k.dr_amount;
                    _balance += k.dr_amount;
                }
                if (k.cr_amount > 0)
                {
                    _row.Cells["cr"].Value = k.cr_amount;
                    _row.ForeColor = Color.Maroon;
                    _balance -= k.cr_amount;
                }
                _row.Cells["balance"].Value = _balance;
                _row.Cells["balance"].ForeColor = Color.Blue;
                _row.Cells["balance"].TextAlign = iGContentAlignment.MiddleCenter;
                _row.Cells["balance"].BackColor = Color.AntiqueWhite;
                _row.Cells["balance"].ForeColor = Color.Blue;
                _row.Tag = k;
                _row.AutoHeight();
            }
            iGrid1.AutoResizeCols = false;
            iGrid1.Cols.AutoWidth();
            //
            iGrid1.Cols["desc"].Width = 300;
            iGrid1.Rows.AutoHeight();
            iGrid1.Rows["fixed_row"].Height = 5;
            iGrid1.EndUpdate();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
             clear_grids();
             datam.ShowWaitForm("Loading Data, Please Wait");
             Application.DoEvents();
             using (var xd = new xing())
             {
                 if (m_account_start_date != null && _fs_id_1 <= fn.GetFSID(m_account_start_date.Value))
                 {
                      m_BF = m_opening_balance;
                    //if (_fs_id_1 == fn.GetFSID(m_account_start_date.Value))
                    //{
                    //    m_BF = accn.GetFs_AccountBalance(xd, fn.GetFSID(dateTimePicker1.Value), m_account.account_id);
                    //}
                    //else
                    //{
                    //    m_BF = 0;
                    //}
                 }
                 else
                 {
                     m_BF = accn.GetFs_AccountBalance(xd, fn.GetFSID(dateTimePicker1.Value.AddDays(-1)), m_account.account_id);
                 }
                if (accn.DATA_ASSET_LIABILITY_STATEMENT == null)
                {
                   accn.DATA_ASSET_LIABILITY_STATEMENT = new List<ic.account_AL_statementC>();
                }
                if (m_account.account_type == em.account_typeS.ActualAccount)
                {
                    iGrid1.Cols["account"].Visible = false;
                    accn.DATA_ASSET_LIABILITY_STATEMENT.Clear();
                    m_account_name = string.Empty;
                    accn.Get_AssetLiabilityAccountStatement(xd, _fs_id_1, _fs_id_2, m_account.account_id, string.Empty);
                }
                else
                {
                    iGrid1.Cols["account"].Visible = true;
                    var _accounts = accn.GetChildAccounts(m_account.account_id, em.account_typeS.ActualAccount);
                    accn.DATA_ASSET_LIABILITY_STATEMENT.Clear();
                    foreach (var _acc in _accounts)
                    {
                        m_account_name = _acc.account_name;
                        accn.Get_AssetLiabilityAccountStatement(xd, _fs_id_1, _fs_id_2, _acc.account_id, m_account_name);
                    }

                }
                 xd.CommitTransaction();
             }
             LoadGrid();
             datam.HideWaitForm();
            if (!dateTimePicker1.Enabled)
            {
                dateTimePicker1.Enabled = true;
            }
            buttonrefresh.Enabled = true;
        }
        private void clear_grids()
        {
            iGrid1.Rows.Clear();
          
        }
        
        private void comboyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboyear.SelectedIndex == -1)
            {
                app_working = true;
                dateTimePicker1.Value = DateTime.MinValue;
                dateTimePicker2.Value = DateTime.MinValue;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
                checkdate.Checked = false;
                checkdate.Enabled = false;
                app_working = false;
                _fs_id_2 = 0;
                _fs_id_1 = 0;

                return;
            }
            Application.DoEvents();
            m_sel_year = comboyear.SelectedItem.ToInt16();
            app_working = true;
            checkdate.Checked = false;
            checkdate.Enabled = false;
            app_working = false;
            dateTimePicker2.Enabled = false;
            app_working = true;
            if (m_sel_year != sdata.CURR_DATE.Year)
            {
                dateTimePicker1.MinDate = new DateTime(m_sel_year, 1, 1);
                dateTimePicker1.MaxDate = new DateTime(m_sel_year, 12, 31);
                dateTimePicker1.Value = dateTimePicker1.MinDate;

                //dateTimePicker2.MinDate = new DateTime(m_sel_year, 1, 1);
                //dateTimePicker2.MaxDate = new DateTime(m_sel_year, 12, 31);
                //
                dateTimePicker2.MinDate = dateTimePicker1.MinDate;
                dateTimePicker2.MaxDate = sdata.CURR_DATE;


                dateTimePicker2.Value = dateTimePicker1.MinDate;
                prev_year = true;
            }
            else
            {
                prev_year = false;
                DateTime lower_date = new DateTime(m_sel_year, 1, 1);
                if (sdata.CURR_FS.fs_id == fn.GetFSID(lower_date))
                {
                    dateTimePicker1.MaxDate = sdata.CURR_DATE.AddDays(1);
                    dateTimePicker1.MinDate = new DateTime(m_sel_year, 1, 1);
                    dateTimePicker1.Value = dateTimePicker1.MinDate;
                    //
                    dateTimePicker2.MaxDate = dateTimePicker1.MaxDate;
                    dateTimePicker2.MinDate = dateTimePicker1.Value;
                    dateTimePicker2.Value = dateTimePicker1.Value;

                    special_day = true;
                }
                else
                {
                    dateTimePicker1.MaxDate = sdata.CURR_DATE;
                    dateTimePicker1.MinDate = new DateTime(m_sel_year, 1, 1);
                    dateTimePicker1.Value = dateTimePicker1.MaxDate;

                    dateTimePicker2.MaxDate = dateTimePicker1.MaxDate;
                    dateTimePicker2.MinDate = dateTimePicker1.Value;
                    dateTimePicker2.Value = dateTimePicker1.Value;
                    special_day = false;
                }

            }
            app_working = false;
            dateTimePicker1.Enabled = false;
            if (!dateTimePicker1.Enabled)
            {
              checkdate.Enabled = true;
            }
            if (!checkdate.Enabled)
            {
                checkdate.Enabled = true;
            }
            _fs_id_1 = fn.GetFSID(dateTimePicker1.Value);
            _fs_id_2 = fn.GetFSID(dateTimePicker2.Value);
            if (timer1.Enabled) { timer1.Enabled = false; }
            timer1.Enabled = true;
        }

        private void buttonrefresh_Click(object sender, EventArgs e)
        {
            buttonrefresh.Enabled = false;
            if (!timer1.Enabled)
            {
                timer1.Enabled = true;
            }
        }

        private void paneltop_SizeChanged(object sender, EventArgs e)
        {
            if (panelinnerpanel.Width > paneltop.Width) { return; }
            panelinnerpanel.Left = (paneltop.Width - panelinnerpanel.Width) / 2;
            panelmain.Left = (paneltop.Width - panelmain.Width) / 2;
        }

        private void backworker1_DoWork(object sender, DoWorkEventArgs e)
        {
            datam.SystemInitializer();
            using(var xd= new xing())
            {
                m_opening_balance = fnn.GetAccountOpeningBalance(xd, m_account.account_id);
                m_account_start_date = fnn.GetAccountStartDate(xd, m_account.account_id);
                xd.CommitTransaction();
            }
        }

        private void backworker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           // InitializeGridColumnMain();
          
            LoadYears();
            comboyear.Enabled = true;
            datam.HideWaitForm();
        }

        private void checkdate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkdate.Checked)
            {
                dateTimePicker2.Enabled = true;
            }
            else
            {
                app_working = true;
                dateTimePicker2.Value = dateTimePicker1.Value;
                dateTimePicker2.Enabled = false;
                _fs_id_1 = fn.GetFSID(dateTimePicker1.Value);
                _fs_id_2 = fn.GetFSID(dateTimePicker2.Value);
                timer1.Start();
                app_working = false;
            }
        }

        private void tabControlPanel1_SizeChanged(object sender, EventArgs e)
        {
            iGrid1.Left = (tabControlPanel1.Width - iGrid1.Width) / 2 + tabControlPanel1.Left;
            iGrid1.Top = 10;
            iGrid1.Height = tabControlPanel1.Height - 20;
        }

        private void iGrid1_AfterRowStateChanged(object sender, iGAfterRowStateChangedEventArgs e)
        {
            if (iGrid1.Rows[e.RowIndex].Level == 0)
            {
                if (iGrid1.Rows[e.RowIndex].Expanded)
                {

                    iGrid1.Rows[e.RowIndex].Cells["dr"].Value = null;
                    iGrid1.Rows[e.RowIndex].Cells["cr"].Value = null;
                }
                else
                {
                    iGrid1.Rows[e.RowIndex].Cells["dr"].Value = iGrid1.Rows[e.RowIndex].Cells["dr"].AuxValue;
                    iGrid1.Rows[e.RowIndex].Cells["cr"].Value = iGrid1.Rows[e.RowIndex].Cells["cr"].AuxValue;
                }
            }
        }
        private void tabControlPanel2_SizeChanged(object sender, EventArgs e)
        {
           
        }

        private void iGridPL_AfterRowStateChanged(object sender, iGAfterRowStateChangedEventArgs e)
        {
           
        }
        private void panelmain_Click(object sender, EventArgs e)
        {

        }

        private void tabControlPanel3_SizeChanged(object sender, EventArgs e)
        {
            
        }

        private void iGridBS_AfterRowStateChanged(object sender, iGAfterRowStateChangedEventArgs e)
        {
            
        }

        private void tabControl1_SelectedTabChanged(object sender, DevComponents.DotNetBar.TabStripTabChangedEventArgs e)
        {
            if (_fs_id_1 > 0)
            {
               // CreateData(_fs_id_1, _fs_id_2);
            }
        }

        private void paneltop_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if(iGrid1.SelectedRows.Count==0)
            {
                e.Cancel = true;
                return;
            }
            viewChequeExpenseDetailsToolStripMenuItem.Visible = false;
            ic.account_AL_statementC _obj = iGrid1.SelectedRows[0].Tag as ic.account_AL_statementC;
            if (_obj != null)
            {
                if(_obj.statement_type==em.account_statement_typeS.bank_withdraw)
                {
                    viewChequeExpenseDetailsToolStripMenuItem.Visible = true;
                }
            }
        }

        private void viewChequeExpenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ic.account_AL_statementC _obj = iGrid1.SelectedRows[0].Tag as ic.account_AL_statementC;
            if (_obj != null)
            {
                if (_obj.statement_type == em.account_statement_typeS.bank_withdraw)
                {
                    if (_obj.record_id > 0)
                    {
                        ic.bankWithDrawC _withDraw = datam.GetWithDrawnCheque(_obj.record_id);
                        using (var _fm = new ViewChequeExpenses())
                        {
                            _fm.Tag = _withDraw;
                            _fm.Owner = this;
                            _fm.ShowDialog();
                        }
                    }
                }
            }
        }

        private void printStatementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string _str = "Are You Sure You Want To Print This Statement";
            if (!dbm.WarningMessage(_str, "Print Warning"))
            {
                return;
            }
            string _period=string.Format("{0}  To  {1}",dateTimePicker1.Value.ToLongDateString(),dateTimePicker2.Value.ToLongDateString());
            fnn.PrintAccountStatement(iGrid1, string.Empty, null, m_account, _period);
        }

        private void dateTimePicker1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
