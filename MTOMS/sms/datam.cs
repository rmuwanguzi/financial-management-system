using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTOMS.sms;
using SdaHelperManager;
using System.Globalization;
namespace MTOMS
{
   partial class datam
    {
        public static SortedList<int, SortedList<int, List<sms.smsTransC>>> DATA_SMS_TRANSACTION = null;
        public static SortedList<int, List<sms.smsContactC>> DATA_SMS_CONTACT = null;
        public static int LAST_SMS_TRANS_ID = 0;
        public static int SMS_ACCOUNT_ID = 0;
        public static IEnumerable<sms.smsTransC> GetMonthSMSTransaction(int yr, int month)
        {
            if (yr == 0 | month == 0) { return null; }
            if (DATA_SMS_TRANSACTION == null)
            {
                DATA_SMS_TRANSACTION = new SortedList<int, SortedList<int, List<smsTransC>>>();
            }
            if (DATA_SMS_TRANSACTION.Keys.IndexOf(yr) == -1)
            {
                DATA_SMS_TRANSACTION.Add(yr, new SortedList<int, List<smsTransC>>());
                DATA_SMS_TRANSACTION[yr].Add(month, new List<smsTransC>());
            }
            if (DATA_SMS_TRANSACTION[yr][month].Count>0)
            {
                return DATA_SMS_TRANSACTION[yr][month];
            }
            var _month_fs = fn.GetFsIDMonth(month, yr);
            string _str = string.Format("select * from sms_trans_tb where partition_id between {0} and {1}", _month_fs[0], _month_fs[1]);
            sms.smsTransC _obj = null;
            using (var _dr = dbm.SelectCommand(_str))
            {
                if (_dr == null) { return null; }
                while (_dr.Read())
                {
                    try
                    {
                        _obj = new smsTransC();
                        _obj.trans_fs_id = _dr["trans_fs_id"].ToInt32();
                        _obj.fs_obj = datam.DATA_FS[yr][_obj.trans_fs_id];
                        _obj.trans_id = _dr["trans_id"].ToInt32();
                        _obj.trans_time = _dr["trans_time"].ToStringNullable();
                        _obj.heading = _dr["heading"].ToStringNullable();
                        _obj.max_char_per_msg = _dr["max_char_per_msg"].ToInt32();
                        _obj.message_string = _dr["message"].ToStringNullable();
                        _obj.message_count = _dr["message_count"].ToInt32();
                        _obj.no_of_characters = _dr["no_of_characters"].ToInt32();
                        _obj.pc_us_id = _dr["pc_us_id"].ToInt32();
                        _obj.phone_count = _dr["phone_count"].ToInt32();
                        _obj.phone_string = _dr["phone_string"].ToStringNullable();
                        _obj.client_name = _dr["client_name"].ToStringNullable();
                        _obj.client_id = _dr["client_id"].ToInt32();
                        _obj.client_sms_cp = _dr["client_sms_cp"].ToFloat();
                        _obj.admin_sms_cp = _dr["admin_sms_cp"].ToFloat();
                        _obj.sms_type = (em.sms_type)_dr["sms_type"].ToByte();
                        _obj.trans_date = _dr["trans_date"] == null ? null : _dr.GetDateTime("trans_date").ToString(new CultureInfo("en-US"));
                        _obj.WasScheduled = _dr["sms_type"].ToByte() == 0 ? false : true;
                        if (LAST_SMS_TRANS_ID < _obj.trans_id) { LAST_SMS_TRANS_ID = _obj.trans_id; }
                        DATA_SMS_TRANSACTION[yr][month].Add(_obj); _obj = null;
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
                return DATA_SMS_TRANSACTION[yr][month];
            }
           
        }
        public static SortedDictionary<int, sms.smsfileSys> DATA_SMS_FILE_SYSTEM = null;
        public static void LoadSmsFileSystem()
        {
            if (DATA_SMS_FILE_SYSTEM == null)
            {
                DATA_SMS_FILE_SYSTEM = new SortedDictionary<int, smsfileSys>();
            }
            if (DATA_SMS_FILE_SYSTEM.Count == 0)
            {
                string _str = "select * from sms_fsys_tb";
                sms.smsfileSys _obj = null;
                using (var _dr = dbm.SelectCommand(_str))
                {
                    if (_dr == null) { return; }
                    while (_dr.Read())
                    {
                        _obj = new smsfileSys();
                        _obj.fsys_id = _dr["fsys_id"].ToInt32();
                        _obj.fsys_name = _dr["fsys_name"].ToStringNullable();
                        _obj.drive_id = _dr["drive_id"].ToInt16();
                        _obj.account_id = _dr["account_id"].ToInt32();
                        _obj.fsys_type = (em.fsys_typeS)_dr["fsys_type_id"].ToByte();
                        _obj.level = _dr["f_level"].ToInt32();
                        _obj.index = _dr["f_index"].ToInt32();
                        _obj.parent_fsys_id = _dr["p_fsys_id"].ToInt32();
                        DATA_SMS_FILE_SYSTEM.Add(_obj.fsys_id,_obj);
                        _obj = null;
                    }
                }
                LoadSMSClients();
                LoadSMSContacts();
            }
        }
        public static void LoadSMSContacts()
        {
            if (DATA_SMS_CONTACT == null)
            {
                DATA_SMS_CONTACT = new SortedList<int, List<smsContactC>>();
            }
            if (DATA_SMS_CONTACT.Count == 0)
            {
                string _str = "select * from sms_contact_tb order by client_id";
                sms.smsContactC _obj = null;
                using (var _dr = dbm.SelectCommand(_str))
                {
                    if (_dr == null) { return; }
                    int prev_client_id = -5;
                    while (_dr.Read())
                    {
                        _obj = new smsContactC();
                        _obj.uno = _dr["uno"].ToInt32();
                        _obj.client_id = _dr["client_id"].ToInt32();
                        if (prev_client_id != _obj.client_id)
                        {
                            prev_client_id = _obj.client_id;
                            DATA_SMS_CONTACT.Add(prev_client_id, new List<smsContactC>());
                        }
                        _obj.name = _dr["c_name"].ToStringNullable();
                        _obj.partition_id = _dr["partition_id"].ToInt32();
                        _obj.phone1 = _dr["c_phone"].ToStringNullable();
                        DATA_SMS_CONTACT[prev_client_id].Add(_obj);
                        _obj = null;
                    }
                }
            }
        }
        public static void LoadSMSClients()
        {
           
                string _str = "select * from sms_client_tb";
                sms.smsClient _obj = null;
                using (var _dr = dbm.SelectCommand(_str))
                {
                    if (_dr == null) { return; }
                    while (_dr.Read())
                    {
                        _obj = new smsClient();
                        _obj.account_id= _dr["account_id"].ToInt32();
                        _obj.client_id = _dr["client_id"].ToInt32();
                        _obj.client_name = _dr["client_name"].ToStringNullable();
                        _obj.email = _dr["client_email"].ToStringNullable();
                        _obj.client_phone = _dr["client_phone"].ToStringNullable();
                        _obj.client_sms_cp = _dr["client_sms_cp"].ToFloat();
                        _obj.default_header = _dr["d_header"].ToStringNullable();
                        _obj.default_msg = _dr["d_message"].ToStringNullable();
                        _obj.fsys_id = _dr["fsys_id"].ToInt32();
                        _obj.start_date = _dr["start_date"] == null ? null : _dr.GetDateTime("start_date").ToString(new CultureInfo("en-US"));
                        _obj.start_date = _dr["end_date"] == null ? null : _dr.GetDateTime("end_date").ToString(new CultureInfo("en-US"));
                        DATA_SMS_FILE_SYSTEM[_obj.fsys_id].sms_client_obj = _obj;
                        _obj = null;
                    }
                }
            
        }
        public static int GetSmsDemoCount()
        {
            string _str = "select s_count from sms_count_tb";
            return dbm.ExecuteScalarInt(_str);
           
        }

    }
}
