using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using SdaHelperManager;
using System.Windows.Forms;
using SdaHelperManager.TreeList;
using SdaHelperManager.Security;
using System.Linq;

using System.Reflection;
using System.Diagnostics;

namespace MTOMS
{
    public partial class WorkManager : DevComponents.DotNetBar.Office2007Form
    {
        public WorkManager()
        {
            InitializeComponent();
        }
        private DevComponents.DotNetBar.Office2007Form mform;
        private menu_item _helper_menu=null;
        private bool m_ShowLocalBackUpWarning = false;
        private bool m_BackUpDialogWarningShown = false;
        #region auto backup requirements
        private enum _process
        {
            formload = 0,
            perform_auto_backup
        }
        _process m_process = _process.formload;
        Timer m_Timer = null;
        short auto_backup_count = 0;
       
        #endregion
        private void WorkManager_Load(object sender, EventArgs e)
        {
            this.Office2007ColorTable = DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.Silver;
            this.Text = "Checking Licence File....";
            sdata.OnLicenceChecked += new sdata.LicenceCheckedEventHandler(sdata_OnLicenceChecked);
           


            datam.SecurityCheck();
            Application.DoEvents();
        }
        private void CreatePrivateDir()
        {
            string _path = System.IO.Path.Combine(Application.StartupPath, "PVT");
            if (!System.IO.Directory.Exists(_path))
            {
                System.IO.Directory.CreateDirectory(_path);
            }
        }
        void sdata_OnLicenceChecked(object sender, sdata.LicenceCheckedEventArg e)
        {
            if (!e.IsValid) { this.Close(); return; }
             datam.FillChurchLicence();
             Assembly m_assembly;
             m_assembly = Assembly.GetExecutingAssembly();
             FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(m_assembly.Location);
             string version = fvi.ProductVersion;
             this.Text = string.Format("MTOMS DEMO Version {0}  Licensed To  {1}   {2}   {3}", version, datam.CHURCH.item_name, datam.CHURCH.code, xso.xso.DATA_REGION[datam.CHURCH.ds_id].item_name);
             CreatePrivateDir();
             //if (!sdata.IsDeveloper)
             //{
             //     dbm.SetUpConnection("PVT/sm_connector.xml");
             //}
             //else
             //{
             //      dbm.SetUpConnection("PVT/sm_connector.xml");
             //   // dbm.SetUpConnection("mtoms_bunga_db", "mysql");
             //   // dbm.SetUpConnection("mtoms_kabalagala_db", "mysql");
             //  //  dbm.SetUpConnection("mtoms_db_church", "mysql");
             // // dbm.SetUpConnection("mtoms_db_five", "mysql");
             //  //dbm.SetUpConnection("PVT/sm_connector.xml");
             //}
             sdata.OnSystemUpgradedEvent += sdata_OnSystemUpgradedEvent;
             LGM();
        }
        void sdata_OnSystemUpgradedEvent(object sender, sdata.SystemUpGradedXEventArg e)
        {
            bool restart_application = false;
            int _version_id = -1;
           // sdata.CreateTablePartitionMySqlForFsID()
                


            using (var xd = new xing())
            {
                
                if(e._oldversion>0)
                {
                    datam.ShowWaitForm("Updating System, Please Wait..."); 
                }
                else
                {
                    datam.ShowWaitForm("Starting System, Please Wait...");
                }
                Application.DoEvents();
                if (e._oldversion == 0)
                {
                    using (var _xd = new xing())
                    {
                        Assembly m_assembly;
                        m_assembly = Assembly.GetExecutingAssembly();
                        var l_stream = m_assembly.GetManifestResourceStream("MTOMS.ChartOfAccounts.xml");
                        fnn.UpdateMtomsChartOfAccounts(l_stream, _xd);
                        _xd.CommitTransaction();
                    }
                    xd.SingleUpdateCommandALL("accounts_tb", new string[]
                        {
                            "account_status_id",
                            "account_id"
                        }, new object[]
                        {
                         em.account_statusS.Disabled,
                         -2487
                        }, 1);
                    datam.InitAccount(xd);
                    accn.CreateDefaultTreasuryDepartment(xd);
                    //
                    var _acc_list = accn.GetChildAccounts("CUC_STANDARD", em.account_typeS.ActualAccount);
                    foreach(var k in _acc_list)
                    {
                        accn.CreateIncomeCRAccount(xd, k);
                    }
                    foreach(var k in datam.DATA_ACCOUNTS.Values)
                    {
                        k.fs_time_stamp = 0;
                    }
                    #region New Exe
                    xd.SingleInsertCommandOnDuplicate("system_upgraded_tb", new string[] { "cversion_id", "un_id" }, new object[]
                {
                    sdata.APP_VERSION_NO,
                    1
                }, string.Format("ON DUPLICATE KEY UPDATE cversion_id={0}", sdata.APP_VERSION_NO));
                    xd.CommitTransaction();
                    datam.HideWaitForm();
                    return; 
                    #endregion
                }
                
               
                PLL:
                 datam.HideWaitForm();
                 xd.SingleInsertCommandOnDuplicate("system_upgraded_tb", new string[] { "cversion_id", "un_id" }, new object[]
                {
                   _version_id==-1? sdata.APP_VERSION_NO:_version_id,
                    1
                }, string.Format("ON DUPLICATE KEY UPDATE cversion_id={0}", _version_id == -1 ? sdata.APP_VERSION_NO : _version_id));
                 xd.CommitTransaction();
                
             }
            if (restart_application)
            {
                MessageBox.Show("You Need To Restart This Application", "Restart Application", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                System.Environment.Exit(0);
               // Application.Restart();
            }
        }
        private void close_previous_form(ref DevComponents.DotNetBar.Office2007Form fm)
        {
            if(fm!=null)
            {
                fm.Close();
                fm.Dispose();
                mform = null;
            }
        }
        private void treeListView1_NodeCheckedEvent(object sender, TreeNodeCheckEventArgs e)
        {
            try
              {
                  treeListView1.Enabled = false;
	          if (e.CheckedNode.Tag != null)
	            {
                    _helper_menu = e.CheckedNode.Tag as SdaHelperManager.menu_item;
                   sdata.CURRENT_MENU = null;
                    if (mform != null)
                    {
                        mform.Hide();
                        mform = null;
                    }
                    if (_helper_menu != null && _helper_menu.FormObject != null && _helper_menu.FormObject.IsDisposed)
                    {
                        _helper_menu.FormObject = null;
                    }
                    Application.DoEvents();
                    if (_helper_menu.FormObject == null)
                    {
                        #region  Has Inner Form Object
                        switch ((em.fm)_helper_menu.m_id)
                        {
                            
                            case em.fm.change_password:
                                {
                                    mform = new ChangePwd();
                                    break;
                                }
                            case em.fm.enter_offering:
                                {
                                    mform = new OfferingManager();
                                    break;
                                }
                            case em.fm.membershi:
                                {
                                    mform = new MemberManager();
                                    break;
                                }
                            case em.fm.group_policy:
                                {
                                    mform = new group_policy();
                                    break;
                                }
                            case em.fm.church_groups:
                                {
                                    mform = new ChurchGroupManager2();
                                    break;
                                }
                            case em.fm.view_church_members:
                                {
                                    mform = new MemberViewerManager();
                                    break;
                                }
                           
                           case em.fm.log_off:
                                {
                                    if (!dbm.WarningMessage("Are You Sure You Want To Log Off", "Log Off Warning"))
                                    {
                                        treeListView1.Enabled = true;
                                        treeListView1.Focus();
                                        return;
                                    }
                                    Application.DoEvents();
                                        Application.DoEvents();
                                        treeListView1.Nodes.Clear();
                                        treeListView1.Columns.Clear();
                                        treeListView1.Visible = false;
                                        sdata.ClearFormCache(null);
                                        sdata.PCU = null;
                                        sdata.CURRENT_MENU = null;
                                        LGM();
                                        datam.FillPcUserDetails();
                                        break;
                                }
                            case em.fm.analyze_offering:
                                {
                                    //mform = new OfferingSabAnalysis();
                                    mform = new OffAnalysis();
                                    break;
                                }
                            case em.fm.sabbath_cash_statement:
                                {
                                    mform = new OfferingSabAnalysis();
                                    break;
                                }
                            case em.fm.analyze_offering_weekly:
                                {
                                    mform = new OfferingMonthAnalysis(); break;
                                }
                            //case em.fm.analyze_offering_range:
                            //    {

                            //        mform = new OffertoryRangeAnalysisA(); break;
                            //    }
                            case em.fm.banking_section:
                                {
                                    mform = new BankingManager();
                                    break;
                                }
                           
                            case em.fm.deparments_manager:
                                {
                                    mform = new DepartmentManager2();
                                    break;

                                }
                            case em.fm.cash_account:
                                {
                                    mform = new CashAccountManager();
                                    break;
                                }
                            case em.fm.sms_mananger:
                                {
                                    mform = new sms.SmsManager(); break;
                                }
                            case em.fm.chart_of_accounts:
                                {
                                    mform = new COAccounts(); break;
                                }
                            case em.fm.incomes_manager:
                                {
                                    mform = new OfferingManager(); break;
                                }
                            case em.fm.quarter_cash:
                                {
                                    mform = new OfferingYearAnalysis(); break;
                                }
                            case em.fm.expenses_manager:
                                {
                                    mform = new DailyExpensesManager(); break;
                                }
                            case em.fm.pledges_manager:
                                {
                                    mform = new Pledge.PledgesManagerB(); break;
                                }
                            case em.fm.creditors:
                                {
                                    mform = new CreditorsManager(); break;
                                }
                            case em.fm.member_statistics:
                                {
                                    mform = new MemberStatistics(); break;
                                }
                            case em.fm.church_sub_units:
                                {
                                    mform = new ChurchSubUnits(); break;
                                }
                           
                            case em.fm.expense_account_settings:
                                {
                                    mform = new ExpensesManager();
                                    break;
                                }
                            case em.fm.view_accounts:
                                {
                                    mform = new TrialBalanceForm(); break;
                                }
                            case em.fm.income_accounts_settings:
                                {
                                    mform = new IncomeAccountsManager(); break;
                                }
                            case em.fm.offering_range_1:
                                {
                                    mform = new OffertoryRangeAnalysisA(); break;
                                }
                            case em.fm.foreign_exch_manager:
                                {
                                    mform = new ForeignExchangeManager(); break;
                                }
                            case em.fm.pending_cheques:
                                {
                                    mform = new PendingChequesManager(); break;
                                }
                            case em.fm.expenses_analysis_year:
                                {
                                    mform = new ExpensesYearAnalysis(); break;
                                }
                            case em.fm.accounts_balances_manager:
                                {
                                    mform = new ClosingBalance.AcccountsBalancesManager();
                                    break;
                                }
                            case em.fm.transfers_manager:
                                {
                                    mform = new MonthlyTransferManager();
                                    break;
                                }
                            case em.fm.lcb_periodic_statement:
                                {
                                    mform = new LCBRangeStatement();
                                    break;
                                }
                            case em.fm.bank_reconciliation_manager:
                                {
                                    mform = new BankReconcManager();
                                    break;
                                }
                            case em.fm.member_activity_analysis:
                                {
                                    mform = new OfferingYearMemberActivity();
                                    break;
                                }
                            case em.fm.system_default_settings:
                                {
                                    mform = new PaymentSettings();
                                    break;
                                }
                                
                        }
                        #endregion
                    }
                    else
                    {
                        mform = _helper_menu.FormObject;
                    }
	                if (mform == null)
	                {
                        treeListView1.Enabled = true;
                        treeListView1.Focus();
	                   return;
	                }
                    if (_helper_menu.FormObject == null)
                    {
                        _helper_menu.FormObject = mform;
                    }
                    sdata.CURRENT_MENU = _helper_menu;
	                mform.MdiParent = this;
                    mform.MinimizeBox = false;
                    mform.MaximizeBox = false;
                    if (sdata.CURRENT_MENU.f_mode == emm.form_mode.full)
                    {
                        mform.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                        mform.Size = this.ClientSize;
                        mform.Dock = DockStyle.Fill;
                    }
                    else
                    {
                        mform.Left = (this.ClientSize.Width - mform.Width) / 2;
                        mform.FormClosing -= new FormClosingEventHandler(mform_FormClosing);
                        mform.FormClosing += new FormClosingEventHandler(mform_FormClosing);
                    }
                   // this.Text = string.Format("Work Manager-(SPN)-{0}", mform.Text);
	                mform.Show();
                    treeListView1.Enabled = true;
                    treeListView1.Focus();
	           }
 }
 catch (System.Exception ex)
 {
    MessageBox.Show(ex.Message);
     treeListView1.Enabled = true;
 }
        }
        void mform_FormClosing(object sender, FormClosingEventArgs e)
        {
            treeListView1.LoadedNode = null;
           
        }
        private void LGM()
        {
            if (treeListView1.Visible) { treeListView1.Visible = false; }
           
            Application.DoEvents();
            sdata.PARENT = this;
            sdata.AppDisplayName = "Central Uganda Conference";
            sdata.DB_SCHEMER_FILE_PATH = "MTOMS.mtoms_db_five.dbx";
            LoginManager lg = new LoginManager();
            Application.DoEvents();
          
            DialogResult rs = lg.ShowDialog();
            if (rs != System.Windows.Forms.DialogResult.OK)
            {
                if (lg.Tag == null)
                {
                    lg.Dispose();
                    System.Environment.Exit(1);
                }
                sdata.PCU = (pcuser)lg.Tag;
                lg.Dispose();
            }
            SdaHelperManager.Security.sdata.ProcessMenuFile("MTOMS.mtoms_MenuF.dat");
            SdaHelperManager.Security.sdata.LoadUserMenu(ref treeListView1);
            treeListView1.Enabled = true;
            backworker1.RunWorkerAsync();
        }
        private void backworker1_DoWork(object sender, DoWorkEventArgs e)
        {
            switch (m_process)
            {
                case _process.formload:
                    {
                        datam.SystemInitializer();
                                              
                        SdaHelperManager.menu_item _bk_menu = sdata.PCU.MENUCollection.Find(n => n.m_id == em.fm.local_backup_manager.ToInt16());
                        if (_bk_menu == null || !_bk_menu.ContainsRight(0))
                        {
                            m_BackUpDialogWarningShown = false;
                          //  MessageBox.Show("stage 1");
                        }
                        else
                        {
                            if(_bk_menu.ContainsRight(0))
                            {
                              //  MessageBox.Show("stage 2");
                                var _last_localbackup_date = dbm.GetLastBackUpDate();
                                if (_last_localbackup_date != null)
                                {
                                    if (sdata.CURR_DATE.Subtract(_last_localbackup_date.Value).Days > 6)
                                    {
                                        m_ShowLocalBackUpWarning = true;
                                     //   MessageBox.Show("stage 3");
                                    }
                                    else
                                    {
                                       // MessageBox.Show("stage 4");
                                    }
                                }
                                else
                                {
                                    m_ShowLocalBackUpWarning = true;
                                  //  MessageBox.Show("stage 5");
                                }
                            }
                            else
                            {
                              //  MessageBox.Show("stage 6");
                            }
                          
                        }
                        break;
                    }
               
            }
        }
        private void backworker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch (m_process)
            {
                case _process.formload:
                    {
                        treeListView1.Visible = true;
                        
                        Application.DoEvents();
                       
                        break;
                    }
               
            }
        }

        //void _fm_OnSavedTenantReceipt(object sender, MuwaEstates.SavedTenantReceiptEventArgs e)
        //{
        //    var _val = MuwaEstates.fnn.DeleteTenantReceipt(e.Receipt.receipt_id);
        //}
             
        void m_Timer_Tick(object sender, EventArgs e)
        {
            m_Timer.Stop();
            if (auto_backup_count > 3)
            {


                 return;
            }//tried three times in thirty minutes and failed
            m_process = _process.perform_auto_backup;
            backworker1.RunWorkerAsync();
         }
        private void treeListView1_SizeChanged(object sender, EventArgs e)
        {
            if (treeListView1.Columns.Count < 1)
            {
                return;
            }
            treeListView1.ResizeColumn(treeListView1.Width - 2, treeListView1.Columns[0]);
        }
        private void WorkManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            string str = "Are You Sure You Want To Close This Application";
            if(!dbm.WarningMessage(str,"Application Close Warning"))
            {
                e.Cancel = true;
            }
          

        }
    }
}
