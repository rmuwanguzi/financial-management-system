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
    public partial class Mem_Displine : DevComponents.DotNetBar.Office2007Form
    {
        public Mem_Displine()
        {
            InitializeComponent();
        }
        ic.memberC selected_mem_obj = null;
        int selected_start_fs_id = 0;
        int selected_end_fs_id = 0;
        DateTime selected_start_date;
        DateTime selected_end_date;
        cbItemx m_sel_item = null;
        ic.displineCategory selected_cat = null;
        public void load_members()
        {
            cbx_member.ClearSearchCollection();
            if (datam.DATA_MEMBER == null || datam.DATA_MEMBER.Count == 0) { return; }
            foreach (ic.memberC obj in datam.DATA_MEMBER.Values)
            {
                cbx_member.AddToSearchCollection(new cbItemx()
                {
                    AttachedObject = obj,
                    display_name = obj.mem_name,
                    frequency = 0,
                    item_id = obj.mem_id,
                    search_string = obj.mem_name
                });
            }
        }
        public void load_cat()
        {
            cbx_cat.ClearSearchCollection();

            if (xso.xso.DATA_DISPLINE_CATEGORY == null || xso.xso.DATA_DISPLINE_CATEGORY.Count == 0) { return; }
            foreach (var obj in xso.xso.DATA_DISPLINE_CATEGORY.Values)
            {
                cbx_cat.AddToSearchCollection(new cbItemx()
                {
                    AttachedObject = obj,
                    display_name = obj.cat_name,
                    frequency = 0,
                    item_id = obj.cat_id,
                    search_string = obj.cat_name
                });
            }

        }
        public bool save_Mem_displine(ic.displineC obj, ref xing xd)
        {
            if (xd == null) { return false; }
            if (obj == null) { return false; }
            String[] tb_col = null;
            object[] _row = null;
            tb_col = new String[] 
                        {
                           "mem_id","cat_id","reason","start_date","end_date",
                            "action","edate","exp_type","pc_us_id","pc_us_name",
                            "lch_id","lch_type_id"    
                        };
            _row = new object[] 
                    {
                #region 
                obj.mem_id,
                obj.cat_id,
                obj.reason,
                obj.start_date,
                obj.end_date,
                obj.action,
                obj.e_date,
                emm.export_type.insert,
                obj.pc_us_id,
                obj.pc_us_name,
                obj.lch_id,
                obj.lch_type_id,
                #endregion
                    };
            xd.SingleInsertCommand("discpline_tb", tb_col, _row);
            return true;
        }
        private void Mem_Displine_Load(object sender, EventArgs e)
        {
            load_members();
            load_cat();
            dateTimeInput2.MaxDate = datam.CURR_DATE;
            dateTimeInput1.MaxDate = datam.CURR_DATE;
            cbx_member.Select(); cbx_member.Focus();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbx_cat.ResetControl();
            cbx_member.ResetControl();
            txt_action.Clear();
            txt_details.Clear();
            btnSave.Enabled = false;
            cbx_member.Select(); cbx_member.Focus();
        }
        public void clear_control()
        {
            btnSave.Enabled = false;
            if (selected_mem_obj == null) { return; }
            if (selected_cat == null) { return; }
            if (txt_action.Text.Trim().Length == 0) { return; }
            if (cbx_cat.Text.Trim().Length == 0) { return; }
            if (cbx_member.Text.Trim().Length == 0) { return; }
            btnSave.Enabled = true;
        }
        private void cbx_member_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_member.SelectedIndex != -1)
            {
                m_sel_item = null;
                selected_mem_obj = null;
            }

        }
        private void cbx_member_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbx_member.SelectedIndex == -1) { return; }
            m_sel_item = cbx_member.SelectedItem as cbItemx;
            if (m_sel_item == null) { return; }
            selected_mem_obj = m_sel_item.AttachedObject as ic.memberC;
            clear_control();
            if (selected_mem_obj != null) { cbx_cat.Select(); cbx_cat.Focus(); }
        }
        private void cbx_cat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_cat.SelectedIndex != -1)
            {
                m_sel_item = null;
                selected_cat = null;
            }
        }
        private void cbx_cat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbx_cat.SelectedIndex == -1) { return; }
            m_sel_item = cbx_cat.SelectedItem as cbItemx;
            if (m_sel_item == null) { return; }
            selected_cat = m_sel_item.AttachedObject as ic.displineCategory;
            clear_control();
            if (selected_cat != null) { txt_details.Select(); txt_details.Focus(); }
        }
        private void txt_action_TextChanged(object sender, EventArgs e)
        {
            clear_control();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!dbm.InsertToDatabaseWarning()) { return; }
            ic.displineC obj = new MTOMS.ic.displineC();
            obj.cat_id = selected_cat.cat_id;
            obj.mem_id = selected_mem_obj.mem_id;
            if (txt_details.Text.Trim().Length != 0)
            {
                obj.reason = txt_details.Text.Trim();
            }
            if (txt_action.Text.Trim().Length != 0)
            {
                obj.action = txt_action.Text.Trim();
            }
            obj.start_date = selected_start_date;
            obj.start_fs_id = selected_start_fs_id;
            obj.end_date = selected_end_date;
            obj.end_fs_id = selected_end_fs_id;
            obj.e_date = datam.CURR_DATE;
            obj.pc_us_id = datam.PC_US_ID;
            obj.pc_us_name = datam.PC_US_NAME;
            obj.lch_id = datam.LCH_ID;
            obj.lch_type_id = datam.LCH_TYPE_ID;
            xing xd = new xing();
            save_Mem_displine(obj, ref xd);
            xd.CommitTransaction();
            dbm.RecordInsertedMessage();
            btnCancel.PerformClick();
        }
        private void dateTimeInput1_ValueChanged(object sender, EventArgs e)
        {
            selected_start_date = dateTimeInput1.Value;
            selected_start_fs_id = fn.GetFSID(selected_start_date);
            dateTimeInput2.MinDate = selected_start_date.AddDays(1);
            dateTimeInput2.Value = selected_start_date.AddDays(1);
            dateTimeInput2.Select(); dateTimeInput2.Focus();
        }
        private void dateTimeInput2_ValueChanged(object sender, EventArgs e)
        {
            selected_end_date = dateTimeInput2.Value;
            selected_end_fs_id = fn.GetFSID(selected_end_date);
            btnSave.Select(); btnSave.Focus();
        }
        private void cbx_member_TextChanged(object sender, EventArgs e)
        {
            if (cbx_member.Text.Trim().Length == 0) { clear_control(); }
        }
        private void cbx_cat_TextChanged(object sender, EventArgs e)
        {
            if (cbx_cat.Text.Trim().Length == 0) { clear_control(); }
        }
    }
}
