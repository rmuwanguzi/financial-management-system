using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SdaHelperManager;

namespace MTOMS.sms
{
    public partial class ComposeSms : DevComponents.DotNetBar.Office2007Form
    {
        public ComposeSms()
        {
            InitializeComponent();
        }
         const int SMS_MAX = 159;
         int m_helper_int = 0;
         int m_msg_count = 0;
         int m_character = 0;
         int m_phone_count = 0;
         const int DEMO_MAX = 100;
         sms.smsClient m_client = null;
         string m_PhoneString = null;
         string m_HEADER = null;
         string m_MESSAGE = null;
         private enum _process
         {
             form_loading,
             send_sms,
             test_mode
         }
         _process m_process = _process.form_loading;
         private enum _sms_status
         {
             success=0,
             insufficientcredit=108,
             invalid_user=106,
             invalid_request=105,
             unknown=-1,
             none=-2
         }
         _sms_status m_status = _sms_status.none;
         private void ComposeSms_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            m_client = (sms.smsClient)this.Tag;
            if (m_client == null) { this.Close(); return; }
            buttoncancel.PerformClick();
            backworker.RunWorkerAsync();
            this.FormClosing += new FormClosingEventHandler(ComposeSms_FormClosing);
        }
         void ComposeSms_FormClosing(object sender, FormClosingEventArgs e)
         {
             if (loadingCircle1.Visible)
             {
                 e.Cancel = true;
             }
         }
         private string GetPhoneString()
         {
             if (m_client.PhoneCollection == null) { return string.Empty; }
             string _phone_list = null;
             foreach (var j in m_client.PhoneCollection)
             {
                 if (_phone_list == null)
                 {
                     _phone_list = j.phone1.ToUgSMS();
                 }
                 else
                 {
                     _phone_list += "," + j.phone1.ToUgSMS();
                 }
             }
             return _phone_list;
         }
         private void textBoxX2_TextChanged(object sender, EventArgs e)
         {
             m_helper_int = textBoxMsg.Text.Length;
             if (m_helper_int > 0)
             {
                 m_character = m_helper_int % SMS_MAX;
                 m_msg_count = (m_helper_int / SMS_MAX);
                 m_msg_count++;
                 if (m_character == 0) { m_character = 159; m_msg_count--; }
                 labelcharacter.Text = string.Format("{0}/159", m_character);
                 labelmsgcnt2.Text = m_msg_count.ToStringNullable();
                 if (textBoxSender.Text.Trim().Length > 0)
                 {
                     if (!buttonsave.Enabled) { buttonsave.Enabled = true; }
                 }
                 label5.Text = m_msg_count.ToStringNullable();
                 label8.Text = (m_msg_count * m_phone_count).ToStringNullable();
             }
             else
             {
                 if (buttonsave.Enabled) { buttonsave.Enabled = false; }
                 labelcharacter.Text = string.Format("0/159");
                 labelmsgcnt2.Text = "0";
                 m_msg_count = 0;
                 m_character = 0;
                 label5.Text = "0";
                 label8.Text = "0";
             }
         }
         private void buttoncancel_Click(object sender, EventArgs e)
         {
             textBoxSender.Clear();
             textBoxMsg.Clear();
             textBoxSender.Focus();
             m_MESSAGE = null;
             m_HEADER = null;
             if (m_client != null & !string.IsNullOrEmpty(m_client.default_header))
             {
                 textBoxSender.Text = m_client.default_header;
                 textBoxMsg.Focus();
             }

         }
         private void textBoxSender_TextChanged(object sender, EventArgs e)
         {
             m_helper_int = textBoxSender.Text.Trim().Length;
             if (m_helper_int > 0)
             {
                 if (textBoxMsg.Text.Trim().Length > 0)
                 {
                     if (!buttonsave.Enabled) { buttonsave.Enabled = true; }
                 }
             }
             else
             {
                 if (buttonsave.Enabled) { buttonsave.Enabled = false; }
                
             }
         }
         private void buttonsave_Click(object sender, EventArgs e)
         {
             MessageBox.Show("This Feature Is Currently Disabled On Your System");
             return;
             string _str = null;
             _str = "Are You Sure You Want To Send This Message ??";
             if (!dbm.WarningMessage(_str, "Send SMS Warning"))
             {
                 return;
             }
             textBoxMsg.Enabled = false;
             textBoxSender.Enabled = false;
             buttonsave.Enabled = false;
             m_process = _process.send_sms;
            // m_process = _process.test_mode;
             loadingCircle1.Active = true;
             loadingCircle1.Visible = true;
             m_PhoneString = null;
             m_HEADER = textBoxSender.Text.Trim();
             m_MESSAGE = textBoxMsg.Text.Trim();
             backworker.RunWorkerAsync();
         }
         private void backworker_DoWork(object sender, DoWorkEventArgs e)
         {
             switch (m_process)
             {
                 case _process.form_loading:
                     {
                         if (m_client != null)
                         {
                             m_phone_count = m_client.PhoneCollection.Count();
                         }
                         break;
                     }
                 case _process.send_sms:
                     {
                         int demo_count = datam.GetSmsDemoCount();
                         if (demo_count >= DEMO_MAX)
                         {
                             MessageBox.Show("Your SMS Demo Version Has Expired");
                             return;
                         }
                         else
                         {
                             //if (m_phone_count > demo_count)
                             //{
                             //    if (m_phone_count > DEMO_MAX)
                             //    {
                             //        m_phone_count = DEMO_MAX - demo_count;
                             //    }
                             //    else
                             //    {
                             //        m_phone_count = m_phone_count - demo_count;
                             //    }
                             //    MessageBox.Show(string.Format("Your SMS Demo Version Has Almost Over,Only {0} SMS Will Be Sent", m_phone_count));
                             //    m_PhoneCollection = m_PhoneCollection.Take(m_phone_count);
                             //}
                            // m_phone_count = 2;
                         }
                         m_phone_count = 2;
                         #region Webservice Settings
                         //
                         Chilkat.HttpRequest req = new Chilkat.HttpRequest();
                         Chilkat.Http http = new Chilkat.Http();
                         bool success;
                         //Any string unlocks the component for the 1st 30-days.
                         success = http.UnlockComponent("30277129240");
                         if (success != true)
                         {
                             MessageBox.Show("Invalid Use Of The Chilkat Library");
                             return;
                         }
                         //  Build an HTTP POST Request:
                         req.UsePost();
                         req.Path = "/api.php";
                         req.AddParam("username", "muwanguzi.ronald@gmail.com");
                         req.AddParam("password", "prpc2qb7");
                         req.AddParam("from", m_HEADER);
                         req.AddParam("message", m_MESSAGE);
                        // req.AddParam("recipients", "256772332619,256772508360");
                         req.AddParam("recipients", "256701871684");
                        
                         m_PhoneString = GetPhoneString();
                       //  req.AddParam("recipients", m_PhoneString);
                         #region compression
                         //MessageBox.Show(m_PhoneString.Length.ToStringNullable();
                         //Chilkat.Gzip gzip = new Chilkat.Gzip();
                         ////  Any string unlocks the component for the 1st 30-days.
                         //success = gzip.UnlockComponent("ZIP12345678_4F507D55AD1G");
                         //if (success != true)
                         //{
                         //    MessageBox.Show(gzip.LastErrorText);
                         //    return;
                         //}
                         //string cs;
                         //cs = gzip.DeflateStringENC(m_PhoneString, "windows-1252", "base64");
                         //MessageBox.Show(cs.Length.ToStringNullable();
                         //return;
                         #endregion
                         //req.AddParam("recipients", m_PhoneString);
                         req.AddParam("type", "normal");
                         req.AddParam("token", "c4ca4238a0b923820dcc509a6f75849b");
                         //  Send the HTTP POST and get the response.  Note: This is a blocking call.
                         //  The method does not return until the full HTTP response is received.
                         string domain;
                         int port;
                         bool ssl;
                         domain = "http://208.111.47.244";
                         port = 80;
                         ssl = false;
                         Chilkat.HttpResponse resp = null;
                         resp = http.SynchronousRequest(domain, port, ssl, req);
                         if (resp == null)
                         {
                            // MessageBox.Show(http.LastErrorText);
                             MessageBox.Show("There Seems To Be A Problem With Your Internet Connection ,SMS Has Not Been Sent", "SMS Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                         }
                         else
                         {
                             var _res = resp.BodyStr.Trim();
                             string error_str = null;
                             if (!string.IsNullOrEmpty(_res))
                             {
                                 int _ret = -1;
                                 if (_res.Split(new char[] { ':' }).Length > 1)
                                 {
                                     _res = _res.Split(new char[] { ':' })[0];
                                 }
                                 int.TryParse(_res, out _ret);
                                 if (_ret >=0)
                                 {
                                     switch ((_sms_status)_ret)
                                     {
                                         case _sms_status.success:
                                             {
                                                 m_status = _sms_status.success;
                                                 break;
                                             }
                                         case _sms_status.insufficientcredit:
                                             {
                                                 error_str = "You Do Not Have Sufficient Credit On Your Account,Please Contact Smart IT Solutions";
                                                 m_status = _sms_status.insufficientcredit;
                                                 break;
                                             }
                                         case _sms_status.invalid_request:
                                             {
                                                 error_str = "The Http Request You Made Is Not Valid";
                                                 m_status = _sms_status.invalid_request;
                                                 break;
                                             }
                                         case _sms_status.invalid_user:
                                             {
                                                 m_status = _sms_status.invalid_user;
                                                 error_str = "Invalid UserName Or Password";
                                                 break;
                                             }
                                         default:
                                             {
                                                 error_str = "Unknown Error,Please Contact Smart IT Solutions";
                                                 m_status = _sms_status.unknown;
                                                 break;
                                             }

                                     }

                                 }
                             }
                             if (!string.IsNullOrEmpty(error_str))
                             {
                                 dbm.ErrorMessage(error_str, "SMS Send ERROR");
                             }
                         }
                         http.Dispose(); 
                         #endregion
                         break;
                     }
                 case _process.test_mode:
                     {
                         m_status = _sms_status.success;
                         break;
                     }

             }
         }
         private void backworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
         {
             switch (m_process)
             {
                 case _process.form_loading:
                     {
                         label7.Text = m_phone_count.ToStringNullable();
                         if (string.IsNullOrEmpty(m_client.default_header))
                         {
                             textBoxSender.Focus();
                         }
                         else
                         {
                             textBoxMsg.Focus();
                         }
                         break;
                     }
                 case _process.send_sms:
                     {
                         loadingCircle1.Visible = false;
                         loadingCircle1.Active = false;
                         if (m_status == _sms_status.success)
                         {
                            //insert into the database
                             InsertRecordIntoDB();
                             this.Close();
                         }
                         textBoxMsg.Enabled = true;
                         textBoxSender.Enabled = true;
                         buttonsave.Enabled = true;
                         break;
                     }
                 case _process.test_mode:
                     {
                         loadingCircle1.Visible = false;
                         loadingCircle1.Active = false;
                         if (m_status == _sms_status.success)
                         {
                             //insert into the database
                             InsertRecordIntoDB();
                             this.Close();
                         }
                         textBoxMsg.Enabled = true;
                         textBoxSender.Enabled = true;
                         buttonsave.Enabled = true;
                         break;

                     }

             }
         }
         private void InsertRecordIntoDB()
         {
             #region Insert Into The Database
             sms.smsTransC _trans = new smsTransC();
             _trans.fs_obj = datam.CURR_FS;
             _trans.heading = m_HEADER;
             _trans.max_char_per_msg = SMS_MAX;
             _trans.message_count = m_msg_count;
             _trans.message_string = m_MESSAGE;
             _trans.no_of_characters = _trans.message_string.Length;
             _trans.pc_us_id = datam.PC_US_ID;
             _trans.phone_count = m_phone_count;
             _trans.phone_string = m_PhoneString;
             _trans.sms_type = em.sms_type.company;
             _trans.trans_date = datam.CURR_DATE.ToShortDateString();
             _trans.trans_fs_id = datam.CURR_FS.fs_id;
             _trans.client_sms_cp = m_client.client_sms_cp;
             _trans.trans_time = datam.CURR_DATE.ToShortTimeString();
             _trans.client_name = m_client.client_name;
             _trans.client_id = m_client.client_id;
             //_trans.admin_account_id = -1964;
             _trans.account_id = -1964;
             string[] _cols = new string[]
                           {
                               "trans_year",
                               "trans_date",
                               "trans_time",
                               "trans_fs_id",
                               "account_id",
                               "client_name",
                               "client_id",
                               "heading",
                               "message",
                               "max_char_per_msg",
                               "message_count",
                               "no_of_characters",
                               "phone_string",
                               "phone_count",
                               "admin_sms_cp",
                               "account_sms_cp",
                               "client_sms_cp",
                               "sms_type",
                               "partition_id",
                               "admin_account_id",
                               "account_type_id",
                               "comp_cat_id",
                               "user_type_id",
                               "pc_us_id"
                           };
             var _row = new object[]
                             {
                                 datam.CURR_DATE.Year,
                                 datam.CURR_DATE.Date,
                                 _trans.trans_time,
                                 _trans.trans_fs_id,
                                 _trans.account_id,
                                 _trans.client_name,
                                 _trans.client_id,
                                 _trans.heading,
                                 _trans.message_string,
                                 _trans.max_char_per_msg,
                                 _trans.message_count,
                                 _trans.no_of_characters,
                                   null,
                                 _trans.phone_count,
                                 _trans.admin_sms_cp,
                                 _trans.account_sms_cp,
                                 _trans.client_sms_cp,
                                 _trans.sms_type.ToByte(),
                                 _trans.trans_fs_id,//partition_id
                                 _trans.admin_account_id,
                                 12,//account_type_id
                                 13,//comp_cat_id,
                                 0,//user type _id
                                 datam.PC_US_ID
                             };
             using (var xd = new xing())
             {
                 _trans.trans_id = xd.SingleInsertCommandInt("sms_trans_tb", _cols, _row);
                 xd.InsertUpdateDelete(string.Format("update sms_count_tb set s_count=(s_count+{0})", m_phone_count));
                 xd.CommitTransaction();
             }
             datam.DATA_SMS_TRANSACTION[datam.CURR_DATE.Year][datam.CURR_DATE.Month].Add(_trans);
             datam.LAST_SMS_TRANS_ID = _trans.trans_id;
             (this.Owner.Owner as sms.SmsManager).NewTransaction(_trans); _trans = null;
             #endregion
         }
        
    }
}
