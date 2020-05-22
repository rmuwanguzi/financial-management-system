using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdaHelperManager;
using SdaHelperManager.Security;
using System.Windows.Forms;

using XPrintTypesPluggin;
namespace MTOMS
{
   partial class datam
    {
       public static Dictionary<int, Dictionary<int, fs_class>> DATA_FS = new Dictionary<int, Dictionary<int, fs_class>>();
       public static SortedList<em.StampTables, long> DATA_STAMP_STORE = null;
       public static ic.accountC CR_PAYMENT_PARENT_ACCOUNT { get; set; }
       public static ic.accountC CR_EXPENSE_PARENT_ACCOUNT { get; set; }
       public static string RECEIPT_PRINTER { get; set; }
       private static WaitForm wait_form = new WaitForm();
       public static void ShowWaitForm()
       {
           if (!wait_form.Visible)
           {
               wait_form.ShowText("Loading Data, Please Wait...");
               wait_form.Show();
               wait_form.TopMost = true;
           }
       }
       public static void ShowWaitForm(string disp_name)
       {
           if (!wait_form.Visible)
           {
               wait_form.ShowText(disp_name);
               wait_form.Show();
               wait_form.TopMost = true;
           }
       }
       public static void HideWaitForm()
       {
           if (wait_form.Visible)
           {
               wait_form.Hide();
           }
       }
       public static int LCH_ID { get; set; }
       public static int LCH_TYPE_ID { get; set; }
       public static string[] MONTHS = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        public static string[] MONTHS_SHORT_NAMES = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec" };
        public static string[] QUARTERS = new string[] { "Quarter I", "Quarter II", "Quarter III", "Quarter IV" };
       public static void fill_data_fs(int year)
       {
           if (DATA_FS == null)
           {
               DATA_FS = new Dictionary<int, Dictionary<int, fs_class>>();
           }
           if (DATA_FS.Keys.Contains(year))
           {
               return;
           }
           DATA_FS.Add(year, fn.CreateYearFsDetails(year));
           int prev_year = (year - 1);
           if (DATA_FS.Keys.Contains(prev_year))
           {
               return;
           }
           DATA_FS.Add(prev_year, fn.CreateYearFsDetails(prev_year));
       }
       public static xso.ic.cregion CHURCH { get; set; }
       public static void FillPcUserDetails()
       {
           if (sdata.PCU == null)
           {
               PC_US_ID = -2010;
               PC_US_NAME = "DEVELOPER";
           }
           if (sdata.PCU != null && sdata.PCU.is_admin)
           {
               PC_US_ID = -1964;
               PC_US_NAME = "ADMIN";
           }
           if (sdata.PCU != null && !sdata.PCU.is_admin)
           {
               PC_US_ID = sdata.PCU.user_id.ToInt32();
               PC_US_NAME = sdata.PCU.user_name;
           }
       }
       public static void FillChurchLicence()
       {
           xso.xso.Intialize();
           if (!sdata.LicenceCreated())
           {
               System.Environment.Exit(0);
               return;
           }
            try
           {
               datam.CHURCH = xso.xso.DATA_REGION[sdata.ChurchID];
               datam.LCH_ID = CHURCH.item_id;
               datam.LCH_TYPE_ID = CHURCH.item_type.ToByte();

           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
               System.Environment.Exit(0);
           }
       }
       public static fs_class CURR_FS = null;
       public static fs_class L_SABBATH = null;
       public static DateTime CURR_DATE = DateTime.MinValue;
       public static bool is_init = false;
       public static int PC_US_ID = 0;
       public static string PC_US_NAME = null;
       public static bool security_complete = false;
       public static void SecurityCheck()
       {
           if (!security_complete)
           {
               security_complete = true;
               xso.xso.Intialize();
               sdata.LoadLicenseFile();
                string _db_path = System.IO.Path.Combine(Application.StartupPath, "mtoms_demo_db.vdb5");
                // dbm.SetUpConnection(_db_path, "123");
                dbm.SetUpConnection("mtoms_demo_db", "@_mtoms_123@");
                // datap.InitPrinterData();
                //   dbm.SetUpConnection("PVT/sm_connector.xml");

            }
        }
       public static void SystemInitializer()
       {
           if (is_init) { return; }
           try
           {
               is_init = true;
               if (CURR_FS == null)
               {
                   CURR_DATE = fn.GetServerDate();
                   CURR_FS = fn.GetFsDetails(CURR_DATE, false);
               }
               if (CURR_FS != null)
               {
                   fill_data_fs(CURR_FS.fs_year);
               }
               if (DATA_SABBATH_RECEIPT == null)
               {
                   DATA_SABBATH_RECEIPT = new SortedList<int, List<MTOMS.ic.off_receipt>>();
               }
               DATA_STAMP_STORE = new SortedList<em.StampTables, long>();
               GetLastSabbath();
               FillPcUserDetails();
              
              
               xing xd = new xing();
               datam.FillChurchSubUnits(xd);
               datam.MemberInit(ref xd);
               fill_church_group_types(xd);
               fill_church_groups(xd);
               datam.InitDesignation(xd);
               datam.MemberStatusInit(ref xd);
               datam.InitAccount(xd);
               datam.InitExpenses(xd);
               datam.DepartmentInit(ref xd);
               datam.GetSystemDefaultValues(xd);
              xd.CommitTransaction();
               xd.Dispose();
            

           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
               System.Environment.Exit(0);
           }
       }
           
    }
}
