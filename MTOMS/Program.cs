using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MTOMS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");//DATE FORMATS
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");//LANGUAGE
            //CULTURE SET FOR GLOBAL PROJECT
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool ok;
            System.Threading.Mutex m = new System.Threading.Mutex(true, "mtomss_84_pentocostal", out ok);
            if (!ok)
            {
                MessageBox.Show("Another instance is already running.");
                return;
            }
           // Application.Run(new BulkPicEditor());   // or whatever was there
          //  Application.Run(new Church_DeptManager());
    //     Application.Run(new OfferingManager());
         // Application.Run(new oo_manager());
        //   Application.Run(new WorkManagerX());
        Application.Run(new WorkManager());
     // Application.Run(new MemberStatistics());
        //   Application.Run(new sms.SmsManager());
      //   Application.Run(new ChurchGroupManager());
     //   Application.Run(new DBUpdater());
        //   Application.Run(new OffAnalysis());
//      Application.Run(new DeparmentMaker());
      // Application.Run(new DeptMasterManager());
   //   Application.Run(new FormTransfer());
  // Application.Run(new IncomeAccountsMaker());
     //  Application.Run(new COAccounts());
 //   Application.Run(new OffMakerN());
         //   Application.Run(new PledgeMaker());
        //    Application.Run(new DepartmentMaker2());
          //  Application.Run(new HouseHoldMaker());
         //  Application.Run(new ChurchGroupManager2());
          //  Application.Run(new DesignationMaker());
        //    Application.Run(new DepartmentManager2());
  //  Application.Run(new ChurchSubUnits());
 //  Application.Run(new ExpenseCategories());
            GC.KeepAlive(m);   
        }
 
    }
}
