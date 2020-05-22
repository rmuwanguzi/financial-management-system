using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SdaHelperManager;
using SdaHelperManager.Security;
namespace MTOMS
{
   public partial class datam
    {
       public static SortedList<int, ic.pledgeC> DATA_PENDING_PLEDGES = null;
       public static SortedList<int, ic.pledgeSettingC> DATA_CURRENT_PLEDGE_SETTINGS { get; set; }
       public static SortedList<int, ic.pledgeC> DATA_CURRENT_PLEDGES { get; set; }
       private static long Last_Pending_pledge_Stamp = 0;
        public static void GetPledgeSettings(xing xd)
        {
            if (DATA_CURRENT_PLEDGE_SETTINGS == null)
            {
                DATA_CURRENT_PLEDGE_SETTINGS = new SortedList<int, ic.pledgeSettingC>();
            }
            string _table_name = "pledge_settings_tb";
            if (wdata.TABLE_STAMP == null)
            {
                wdata.TABLE_STAMP = new SortedList<string, long>();
            }

            if (wdata.TABLE_STAMP.IndexOfKey(_table_name) == -1)
            {
                wdata.TABLE_STAMP.Add(_table_name, 0);
            }
            //
            bool is_new = false;
            int _hlp_id = 0;
            var _xd = xd == null ? new xing() : xd;

            string _str = string.Empty;
            bool load_all = false;
            var _temp_val = _xd.GetTimeStamp(_table_name);
            if (DATA_CURRENT_PLEDGE_SETTINGS.Keys.Count == 0)
            {
                _str = "select * from " + _table_name + " where status=0";
                is_new = true;
                load_all = true;
            }
            else
            {
                if (wdata.TABLE_STAMP[_table_name] == _temp_val)
                {
                    return;
                }
                _str = string.Format("select * from " + _table_name + " where fs_time_stamp > {0}", wdata.TABLE_STAMP[_table_name]);
            }
            wdata.TABLE_STAMP[_table_name] = _temp_val; ;
            List<int> m_upd_ids = new List<int>();
            using (var _dr = _xd.SelectCommand(_str))
            {
                if (_dr == null) { return; }
                ic.pledgeSettingC _obj = null;
                while (_dr.Read())
                {
                    _obj = null;
                    if (load_all)
                    {
                        _obj = new ic.pledgeSettingC();
                        is_new = true;
                    }
                    else
                    {
                        try
                        {
                            _obj = datam.DATA_CURRENT_PLEDGE_SETTINGS[_dr["pls_id"].ToInt32()];
                            is_new = false;
                        }
                        catch (Exception ex)
                        {
                            if (_obj == null)
                            {
                                _obj = new ic.pledgeSettingC();
                                is_new = true;
                            }
                        }
                    }
                    if (is_new)
                    {
                        _obj.account_id = _dr["account_id"].ToInt32();

                        _obj.pls_id = _dr["pls_id"].ToInt32();

                        DATA_CURRENT_PLEDGE_SETTINGS.Add(_obj.pls_id, _obj);
                    }
                    _obj.pls_id = _dr["pls_id"].ToInt32();
                    _hlp_id = _dr["status"].ToInt16();
                    _obj.status = (em.pledge_setting_statusS)_dr["status"].ToInt16();
                    _obj.start_date = _dr.GetDateTime("start_date");
                    _obj.end_date = _dr.GetDateTime("end_date");
                    _obj.end_fs_id = fn.GetFSID(_obj.end_date.Value);
                    _obj.start_fs_id = fn.GetFSID(_obj.start_date.Value);
                    _obj.pledge_name = _dr["pledge_name"].ToStringNullable();
                    _obj.paid_amount = _dr["paid_amount"].ToInt32();
                    _obj.pledged_amount = _dr["amount_pledged"].ToInt32();
                    if (datam.CURR_FS.fs_id > _obj.end_fs_id & _obj.status == em.pledge_setting_statusS.valid)
                    {
                        m_upd_ids.Add(_obj.pls_id);
                        _obj.status = em.pledge_setting_statusS.expired;
                    }
                }
            }
            if (m_upd_ids.Count > 0)
            {
                foreach (var _t in m_upd_ids)
                {
                    _xd.SingleUpdateCommandALL("pledge_settings_tb", new string[] { "status", "pls_id" }, new object[]
                {
                    em.pledge_setting_statusS.expired.ToInt16(),
                    _t
                }, 1);
                    //
                    xd.SingleUpdateCommandALL("pledge_master_tb", new string[] { "pl_status", "pls_id" }, new object[]
                    {
                    em.pledge_statusS.expired.ToInt16(),
                    _t
                    }, 1);
                    //
                    xd.SingleUpdateCommandALL("pledge_payment_mvt_tb", new string[] { "status", "pls_id" }, new object[]
                    {
                     2,//expired
                    _t
                    }, 1);
                }
            }
            if (xd == null)
            {
                _xd.CommitTransaction();
            }


        }
       public static void GetCurrentPledges(xing xd)
       {
           if (DATA_CURRENT_PLEDGES == null)
           {
               DATA_CURRENT_PLEDGES = new SortedList<int, ic.pledgeC>();
           }
           string _table_name = "pledge_master_tb";
           if (wdata.TABLE_STAMP == null)
           {
               wdata.TABLE_STAMP = new SortedList<string, long>();
           }

           if (wdata.TABLE_STAMP.IndexOfKey(_table_name) == -1)
           {
               wdata.TABLE_STAMP.Add(_table_name, 0);
           }
           bool is_new = false;
           var _xd = xd == null ? new xing() : xd;
           string _str = string.Empty;
           bool load_all = false;
           var _temp_val = _xd.GetTimeStamp(_table_name);
           if (DATA_CURRENT_PLEDGES.Keys.Count == 0)
           {
               _str = "select * from " + _table_name;
               is_new = true;
               load_all = true;
           }
           else
           {
               if (wdata.TABLE_STAMP[_table_name] == _temp_val)
               {
                   return;
               }
               _str = string.Format("select * from " + _table_name + " where fs_time_stamp > {0}", wdata.TABLE_STAMP[_table_name]);
           }
           wdata.TABLE_STAMP[_table_name] = _temp_val;
           using (var _dr = xd.SelectCommand(_str))
           {
               if (_dr == null) { return; }

               ic.pledgeC _pledge = null;
               while (_dr.Read())
               {
                   _pledge = null;
                   if (load_all)
                   {
                       _pledge = new MTOMS.ic.pledgeC();
                       is_new = true;
                   }
                   else
                   {
                       try
                       {
                           _pledge = datam.DATA_CURRENT_PLEDGES[_dr["pl_id"].ToInt32()];
                           is_new = false;
                       }
                       catch (Exception ex)
                       {
                           if (_pledge == null)
                           {
                               _pledge = new MTOMS.ic.pledgeC();
                               is_new = true;
                           }
                       }
                   }
                   if (is_new)
                   {
                       _pledge.pl_id = _dr["pl_id"].ToInt32();
                       _pledge.account_id = _dr["account_id"].ToInt32();
                       _pledge.source_type = (em.off_source_typeS)_dr["source_type_id"].ToByte();
                       _pledge.source_id = _dr["source_id"].ToInt32();
                       _pledge.source_name = _dr["source_name"].ToStringNullable();
                       _pledge.source_phone = _dr["source_phone"].ToStringNullable();
                       _pledge.amount_pledged = _dr["amount_pledged"].ToInt32();
                       _pledge.pls_id = _dr["pls_id"].ToInt32();
                       _pledge.cg_id = _dr["cg_id"].ToInt32();
                       _pledge.pledge_mode = (em.pledge_modeS)_dr["pl_mode"].ToByte();
                       //
                       if (_dr["fs_date"] != null)
                       {
                           _pledge.fs_date = _dr.GetDateTime("fs_date");
                           _pledge.fs_id = _dr["fs_id"].ToInt32();
                       }
                       if (_dr["collect_date"] != null)
                       {
                           _pledge.fs_date = _dr.GetDateTime("collect_date");
                       }
                       DATA_CURRENT_PLEDGES.Add(_pledge.pl_id, _pledge);
                   }
                   _pledge.amount_paid = _dr["amount_paid"].ToInt32();
                   _pledge.pledge_status = (em.pledge_statusS)_dr["pl_status"].ToByte();
                   _pledge.added_pledge_amount = _dr["added_pledge_amount"].ToInt32();
               }
           }
           if (xd == null)
           {
               _xd.CommitTransaction();
           }

       }
       public static void GetPendingPledges()
       {
           if (DATA_PENDING_PLEDGES == null)
           {
               DATA_PENDING_PLEDGES = new SortedList<int, MTOMS.ic.pledgeC>();
           }
           bool is_new = false;
           using (var xd = new xing())
           {
               string _str = string.Empty;
               bool load_all = false;
               var _temp_val = xd.GetTimeStamp("pledge_master_tb");
               if (DATA_PENDING_PLEDGES.Keys.Count == 0)
               {
                   _str = "select * from pledge_master_tb where pl_status=0";
                   is_new = true;
                   load_all = true;
               }
               else
               {
                    if (Last_Pending_pledge_Stamp == _temp_val)
                   {
                       return;
                   }
                   _str = string.Format("select * from pledge_master_tb where fs_time_stamp > {0}", Last_Pending_pledge_Stamp);
               }
               Last_Pending_pledge_Stamp = _temp_val; ;
               using (var _dr = xd.SelectCommand(_str))
               {
                   if (_dr == null) { return; }

                   ic.pledgeC _pledge = null;
                   while (_dr.Read())
                   {
                       _pledge = null;
                       if (load_all)
                       {
                           _pledge = new MTOMS.ic.pledgeC();
                           is_new = true;
                       }
                       else
                       {
                           try
                           {
                               _pledge = datam.DATA_PENDING_PLEDGES[_dr["pl_id"].ToInt32()];
                               is_new = false;
                           }
                           catch (Exception ex)
                           {
                               if (_pledge == null)
                               {
                                   _pledge = new MTOMS.ic.pledgeC();
                                   is_new = true;
                               }
                           }
                       }
                       if (is_new)
                       {
                           _pledge.pl_id = _dr["pl_id"].ToInt32();
                           _pledge.account_id = _dr["account_id"].ToInt32();
                           _pledge.source_type = (em.off_source_typeS)_dr["source_type_id"].ToByte();
                           _pledge.source_id = _dr["source_id"].ToInt32();
                           _pledge.source_name = _dr["source_name"].ToStringNullable();
                           _pledge.source_phone = _dr["source_phone"].ToStringNullable();
                           _pledge.amount_pledged = _dr["amount_pledged"].ToInt32();
                           _pledge.pls_id = _dr["pls_id"].ToInt32();
                           _pledge.cg_id = _dr["cg_id"].ToInt32();
                           _pledge.pledge_mode = (em.pledge_modeS)_dr["pl_mode"].ToByte();
                           //
                           if (_dr["fs_date"] != null)
                           {
                               _pledge.fs_date = _dr.GetDateTime("fs_date");
                               _pledge.fs_id = _dr["fs_id"].ToInt32();
                           }
                           if (_dr["collect_date"] != null)
                           {
                               _pledge.fs_date = _dr.GetDateTime("collect_date");
                           }
                           DATA_PENDING_PLEDGES.Add(_pledge.pl_id, _pledge);
                       }
                       _pledge.amount_paid = _dr["amount_paid"].ToInt32();
                       _pledge.pledge_status = (em.pledge_statusS)_dr["pl_status"].ToInt16();
                        _pledge.added_pledge_amount = _dr["added_pledge_amount"].ToInt32();
                   }
               }

               xd.CommitTransaction();
           }
       }
    }
}
