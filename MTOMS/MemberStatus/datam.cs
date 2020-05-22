using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdaHelperManager;

using System.Windows.Forms;

namespace MTOMS
{
   partial class datam
    {
        public static SortedList<int, ic.apostacyC> DATA_APOSTACY = null;
        public static SortedList<int, ic.deathC> DATA_DEATH = null;
        public static SortedList<int, ic.displineC> DATA_DISPLINE = null;
        public static SortedList<int, ic.transferC> DATA_TRANSFER = null;
        private static void fill_mem_apostacy(ref xing xd)
        {
            string _str = "select * from apostacy_tb";
            try
            {
                if (DATA_APOSTACY == null) { DATA_APOSTACY = new SortedList<int, MTOMS.ic.apostacyC>(); }
                if (DATA_APOSTACY.Keys.Count > 0) { return; }
                using (var _dr = xd.SelectCommand(_str))
                {
                    ic.apostacyC obj = null;
                    while (_dr.Read())
                    {
                        obj = new MTOMS.ic.apostacyC();
                        obj.mem_id = _dr["mem_id"].ToInt32();
                        obj.un_id = _dr["un_id"].ToInt32();
                        obj.reason = _dr["reason"] == null ? string.Empty : _dr["reason"].ToStringNullable();
                        obj.comment = _dr["comment"] == null ? string.Empty : _dr["comment"].ToStringNullable();
                        obj.apostacy_date = _dr.GetMySqlDateTime("apos_date").Value;
                        obj.apostacy_fs_id = fn.GetFSID(obj.apostacy_date);
                        obj.pc_us_id = _dr["pc_us_id"].ToInt32();
                        if (_dr["lch_id"] != null) { obj.lch_id = _dr["lch_id"].ToInt32(); }
                        if (_dr["lch_type_id"] != null) { obj.lch_type_id = _dr["lch_type_id"].ToInt32(); }
                        DATA_APOSTACY.Add(obj.un_id, obj);
                        datam.DATA_MEMBER[obj.mem_id].objApostacy = obj;
                    }
                    _dr.Close(); _dr.Dispose();

                }
            }
            catch ( VistaDB.Diagnostic.VistaDBException ex)
            {
                MessageBox.Show(ex.Message);
                throw new Exception("Data Loading Failed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static void fill_mem_death(ref xing xd)
        {
            string _str = "select * from death_tb";
            try
            {
                if (DATA_DEATH == null) { DATA_DEATH = new SortedList<int, MTOMS.ic.deathC>(); }
                if (DATA_DEATH.Keys.Count > 0) { return; }
                using (var _dr = xd.SelectCommand(_str))
                {
                    ic.deathC obj = null;
                    while (_dr.Read())
                    {
                        obj = new MTOMS.ic.deathC();
                        obj.un_id = _dr["un_id"].ToInt32();
                        obj.mem_id = _dr["mem_id"].ToInt32();
                        obj.reason = _dr["death_cause"] == null ? string.Empty : _dr["death_cause"].ToStringNullable();
                        obj.comment = _dr["comment"] == null ? string.Empty : _dr["comment"].ToStringNullable();
                        obj.death_date = _dr.GetMySqlDateTime("death_date").Value;
                        obj.death_fs_id = fn.GetFSID(obj.death_date);
                        obj.pc_us_id = _dr["pc_us_id"].ToInt32();
                        if (_dr["lch_id"] != null) { obj.lch_id = _dr["lch_id"].ToInt32(); }
                        if (_dr["lch_type_id"] != null) { obj.lch_type_id = _dr["lch_type_id"].ToInt32(); }
                        DATA_DEATH.Add(obj.un_id, obj);
                        datam.DATA_MEMBER[obj.mem_id].objDeath = obj;
                    }
                    _dr.Close(); _dr.Dispose();

                }
            }
            catch ( VistaDB.Diagnostic.VistaDBException ex)
            {
                MessageBox.Show(ex.Message);
                throw new Exception("Data Loading Failed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static void fill_mem_displine(ref xing xd)
        {
            string _str = "select * from discpline_tb";
            try
            {
                if (DATA_DISPLINE == null) { DATA_DISPLINE = new SortedList<int, MTOMS.ic.displineC>(); }
                if (DATA_DISPLINE.Keys.Count > 0) { return; }
                using (var _dr = xd.SelectCommand(_str))
                {
                    ic.displineC obj = null;
                    while (_dr.Read())
                    {
                        obj = new MTOMS.ic.displineC();
                        obj.un_id = _dr["un_id"].ToInt32();
                        obj.mem_id = _dr["mem_id"].ToInt32();
                        //obj.cat_id = _dr["cat_id"].ToInt32();
                        obj.reason = _dr["reason"] == null ? string.Empty : _dr["reason"].ToStringNullable();
                        obj.start_date = Convert.ToDateTime(_dr["start_date"]);
                        obj.end_date = Convert.ToDateTime(_dr["end_date"]);
                        //obj.action = _dr["action"] == null ? string.Empty : _dr["action"].ToStringNullable();
                        //obj.e_date = Convert.ToDateTime(_dr["edate"]);
                        obj.pc_us_id = _dr["pc_us_id"].ToInt32();
                        //obj.pc_us_name = _dr["pc_us_name"].ToStringNullable();
                        if (_dr["lch_id"] != null) { obj.lch_id = _dr["lch_id"].ToInt32(); }
                        if (_dr["lch_type_id"] != null) { obj.lch_type_id = _dr["lch_type_id"].ToInt32(); }
                        DATA_DISPLINE.Add(obj.un_id, obj);
                    }
                    _dr.Close(); _dr.Dispose();

                }
            }
            catch ( VistaDB.Diagnostic.VistaDBException ex)
            {
                MessageBox.Show(ex.Message);
                throw new Exception("Data Loading Failed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static void fill_mem_transfer(ref xing xd)
        {
            string _str = "select * from transfer_tb";
            try
            {
                if (DATA_TRANSFER == null) { DATA_TRANSFER = new SortedList<int, MTOMS.ic.transferC>(); }
                if (DATA_TRANSFER.Keys.Count > 0) { return; }
                using (var _dr = xd.SelectCommand(_str))
                {
                    ic.transferC obj = null;
                    while (_dr.Read())
                    {
                        obj = new MTOMS.ic.transferC();
                        obj.un_id = _dr["un_id"].ToInt32();
                        obj.mem_id = _dr["mem_id"].ToInt32();
                        obj.reason = _dr["reason"] == null ? string.Empty : _dr["reason"].ToStringNullable();
                        obj.comment = _dr["comment"] == null ? string.Empty : _dr["comment"].ToStringNullable();
                        obj.transfer_date = _dr.GetMySqlDateTime("transfer_date").Value;
                        obj.transfer_fs_id = fn.GetFSID(obj.transfer_date);
                        obj.pc_us_id = _dr["pc_us_id"].ToInt32();
                        if (_dr["lch_id"] != null) { obj.lch_id = _dr["lch_id"].ToInt32(); }
                        if (_dr["lch_type_id"] != null) { obj.lch_type_id = _dr["lch_type_id"].ToInt32(); }
                        DATA_TRANSFER.Add(obj.un_id, obj);
                        datam.DATA_MEMBER[obj.mem_id].objTransfer = obj;
                    }
                    _dr.Close(); _dr.Dispose();

                }
            }
            catch ( VistaDB.Diagnostic.VistaDBException ex)
            {
                MessageBox.Show(ex.Message);
                throw new Exception("Data Loading Failed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void MemberStatusInit(ref xing xd)
        {
            if (xd == null) { return; }
           
            //
            fill_mem_apostacy(ref xd);
            fill_mem_death(ref xd);
            fill_mem_transfer(ref xd);
            fill_mem_displine(ref xd);
            //
            
        }


       
    }
}
