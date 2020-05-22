using SdaHelperManager;
using SdaHelperManager.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

namespace MTOMS
{
    public partial class ForeignExchangeMaker : DevComponents.DotNetBar.Office2007Form
    {
        public ForeignExchangeMaker()
        {
            InitializeComponent();
        }
        private enum _process
        {
            none=-1,
            form_load,
            after_insert
        }
        _process m_process = _process.form_load;
        private void ForeignExchangeMaker_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
            CenterToScreen();
            InitIgridColumns();
            datam.ShowWaitForm();
            Application.DoEvents();
            datam.SecurityCheck();
            backworker.RunWorkerAsync();
        }
        void InitIgridColumns()
        {
            #region Adjust the grid's appearance
            // Set the flat appearance for the cells.
            fGrid.Appearance = iGControlPaintAppearance.StyleFlat;
            fGrid.UseXPStyles = false;
            fGrid.ScrollBarSettings.Appearance = iGControlPaintAppearance.StyleFlat;
            fGrid.FocusRect = false;
            #endregion
            iGCol myCol;
            //
            myCol = fGrid.Cols.Add("name", "Field Name");
            myCol.CellStyle.ReadOnly = iGBool.True;
            myCol.SortType = iGSortType.None;
            myCol.Width = 20;
            myCol.CellStyle.ForeColor = Color.DarkBlue;
            myCol.CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
            myCol.CellStyle.ReadOnly = iGBool.True;
            myCol.IncludeInSelect = false;
            myCol.CellStyle.BackColor = Color.WhiteSmoke;
            //
            myCol.AllowSizing = false;
            myCol = fGrid.Cols.Add("desc", "Description");
            myCol.Width = 150;
            //  myCol.AllowSizing = false;
            //
            // Add a special column which will store the category name.
            // This column will be used for grouping.
            fGrid.Cols.Add("category", string.Empty).Visible = false;
            // Add a special column which will store the default 
            // values for the properties.
            fGrid.Cols.Add("svalue", string.Empty).Visible = false;
            myCol = fGrid.Cols["svalue"];
            myCol.SortType = iGSortType.ByValue;
            fGrid.DefaultRow.Height = fGrid.GetPreferredRowHeight(true, false);
            fGrid.DefaultAutoGroupRow.Height = fGrid.DefaultRow.Height;
        }
        private void backworker_DoWork(object sender, DoWorkEventArgs e)
        {
            switch(m_process)
            {
                case _process.form_load:
                    {
                        datam.SystemInitializer();
                        using (var xd = new xing())
                        {
                            datam.FillForeignCurrency(xd);
                            xd.CommitTransaction();
                        }
                        break;
                    }
                case _process.after_insert:
                    {
                        break;
                    }
            }
          
        }
        private void backworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            switch (m_process)
            {
                case _process.form_load:
                    {
                        LoadMainGrid();
                        fGrid.SetCurCell("currency", 1);
                        datam.HideWaitForm();
                        break;
                    }
                case _process.after_insert:
                    {
                        if(this.Owner is ForeignExchangeManager)
                        {
                            (this.Owner as ForeignExchangeManager).CheckForUpdates();
                        }
                        break;
                    }
            }
        
        }
        private void LoadMainGrid()
        {
            fGrid.BeginUpdate();
            iGRow _row = null;
            _row = fGrid.Rows.Add();
            _row.Height = 5;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            int group_index = 1;
            string group_name = "New Account Information";
            //
            _row = CreateNewRow("Select Currency", "currency", typeof(string), group_index, group_name);
            _row.Cells[0].ForeColor = Color.Maroon;
            var _nlist = accn.GetChildAccounts("UNBANKED_FOREIGN", em.account_typeS.ActualAccount);
            var icombo = fnn.CreateCombo();
            foreach(var k in _nlist)
            {
                icombo.Items.Add(new fnn.iGComboItemEX()
                    {
                        Value = k.account_name,
                        Tag = k.account_id
                    });
            }
            _row.Cells[1].DropDownControl = icombo;
            _row.Cells[1].Value = null;
            //
            var _date = new fnn.DropDownCalenderX();
            _date.start_date = sdata.CURR_DATE;
            _date.end_date = sdata.CURR_DATE;
            _date.selected_date = sdata.CURR_DATE;
            _row = CreateNewRow("Exchange Date", "date", typeof(string), group_index, group_name);
            _row.Cells["desc"].TypeFlags = iGCellTypeFlags.ComboPreferValue | iGCellTypeFlags.NoTextEdit | iGCellTypeFlags.HasEllipsisButton;
            _row.Cells["desc"].FormatString = "{0:D}";// {0:d} for short date string
            _row.Cells["desc"].DropDownControl = _date;
            _row.Cells["desc"].Value = null;
            _row.Cells["desc"].Enabled = iGBool.False;
            //
            _row = fGrid.Rows.Add();
            _row.Height = 5;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            //
            _row = CreateNewRow("Current Amount", "curr_amount", typeof(string), group_index, group_name);
            _row.Cells[1].FormatString = "{0:N0}";
            _row.Cells[1].Enabled = iGBool.False;
            _row = CreateNewRow("Current Exch Rate", "curr_exch_rate", typeof(float), group_index, group_name);
            _row.Cells[1].Enabled = iGBool.False;
            //
            _row = fGrid.Rows.Add();
            _row.Height = 5;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            _row = CreateNewRow("Amount Exchanged", "exch_amount", typeof(int), group_index, group_name);
            _row.Cells[0].ForeColor = Color.Maroon;
            _row = CreateNewRow("Exchange Rate", "exch_rate", typeof(float), group_index, group_name);
            _row.Cells[0].ForeColor = Color.Maroon;
            //
            _row = fGrid.Rows.Add();
            _row.Height = 3;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;

            _row = CreateNewRow("Ugx Amount", "ug_amount", typeof(float), group_index, group_name);
            _row.Cells[1].FormatString = "{0:N0}";
            _row.Cells[1].ReadOnly = iGBool.True;
            _row.Cells[1].ForeColor = Color.DarkBlue;
             //
            _row = fGrid.Rows.Add();
            _row.Height = 3;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
              //
            _row = CreateNewRow("Exchange Gain", "gain", typeof(int), group_index, group_name);
            _row.Cells[1].FormatString = "{0:N0}";
            _row.Cells[1].Enabled = iGBool.False;
            _row.Cells[0].ForeColor = Color.DarkGreen;
            _row = CreateNewRow("Exchange Loss", "loss", typeof(int), group_index, group_name);
            _row.Cells[1].FormatString = "{0:N0}";
            _row.Cells[1].Enabled = iGBool.False;
            _row.Cells[0].ForeColor = Color.Red;
            //
            
            _row = fGrid.Rows.Add();
            _row.Height = 4;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            //
           
           

            fGrid.EndUpdate();
        }
        private iGRow CreateNewRow(string field, string rowkey, Type _type, int group_index, string group_name)
        {
            var _row = fGrid.Rows.Add();
            _row.Font = new Font("georgia", 14, FontStyle.Regular);
            _row.Cells["desc"].Font = new Font("arial", 14, FontStyle.Regular);
            _row.Cells["name"].Col.Width = 270;
            _row.Cells["name"].Value = field;
            _row.Cells["name"].TextAlign = iGContentAlignment.BottomRight;
            _row.Cells["desc"].ValueType = _type;
            _row.Cells["category"].Value = group_name;
            _row.Cells["svalue"].Value = group_index;
            _row.Key = rowkey;
            _row.AutoHeight();
            _row.Height += 2;
            return _row;
        }
        private void CalculateGainLoss()
        {

        }
        private void fGrid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
        {

            var _key= fGrid.Rows[e.RowIndex].Key;
             switch(_key)
             {
                 case "date":
                     {
                         DateTime? _date = System.Convert.ToDateTime(fGrid.Rows[e.RowIndex].Cells[1].Value);
                         fGrid.Rows["curr_amount"].Cells[1].Value = null;
                         fGrid.Rows["curr_exch_rate"].Cells[1].Value = null;
                         using (var xd = new xing())
                         {
                             int _acc_id = (fGrid.Rows["currency"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag.ToInt32();
                             float _exch_rate = accn.GetFS_ConversionRate(xd, fn.GetFSID(_date.Value), _acc_id);
                             int _acc_bal = accn.GetFS_ForeignCurrencyBalance(xd, fn.GetFSID(_date.Value), _acc_id);
                             fGrid.Rows["curr_exch_rate"].Cells[1].Value = _exch_rate;
                             if (_acc_bal > 0)
                             {
                                 fGrid.Rows["curr_amount"].Cells[1].Value = _acc_bal;
                             }
                             xd.CommitTransaction();
                         }
                         break;
                     }
                 case "currency":
                     {
                         if (fGrid.Rows[e.RowIndex].Cells[1].Value == null)
                         {
                             fGrid.Rows["curr_amount"].Cells[1].Value = null;
                             fGrid.Rows["curr_exch_rate"].Cells[1].Value = null;
                             //
                             fGrid.Rows["exch_amount"].Cells[1].Value = null;
                             fGrid.Rows["exch_rate"].Cells[1].Value = null;
                             fGrid.Rows["ug_amount"].Cells[1].Value = null;
                             fGrid.Rows["gain"].Cells[1].Value = null;
                             fGrid.Rows["loss"].Cells[1].Value = null;
                             fGrid.Rows["date"].Cells[1].Value = null;
                             fGrid.Rows["date"].Cells[1].AuxValue = null;
                             fGrid.Rows["date"].Cells[1].Enabled = iGBool.False;
                             fGrid.Rows["exch_amount"].Cells[0].Value = "Amount Exchanged";
                         }
                         else
                         {
                             fGrid.Rows["curr_amount"].Cells[1].Value = null;
                             fGrid.Rows["curr_exch_rate"].Cells[1].Value = null;
                             //
                             fGrid.Rows["exch_amount"].Cells[1].Value = null;
                             fGrid.Rows["exch_rate"].Cells[1].Value = null;
                             fGrid.Rows["ug_amount"].Cells[1].Value = null;
                             fGrid.Rows["gain"].Cells[1].Value = null;
                             fGrid.Rows["loss"].Cells[1].Value = null;
                             fGrid.Rows["date"].Cells[1].Value = null;
                             fGrid.Rows["date"].Cells[1].AuxValue = null;
                             fGrid.Rows["date"].Cells[1].Enabled = iGBool.True;
                             fGrid.Rows["exch_amount"].Cells[0].Value = string.Format("{0} Exchanged", fGrid.Rows["currency"].Cells[1].Text);
                             //
                             using (var xd = new xing())
                             {
                                 int _acc_id = (fGrid.Rows["currency"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag.ToInt32();
                                 object _obj = xd.ExecuteScalarObject(string.Format("select last_exchange_date from acc_foreign_currency_tb where sys_account_id={0}", _acc_id));
                                 if (_obj == null)
                                 {
                                     _obj = xd.ExecuteScalarObject(string.Format("select TOP 1 fs_date from acc_foreign_currency_mvt_tb where sys_account_id={0} order by fs_id", _acc_id));
                                 }
                                 DateTime? _start_date = System.Convert.ToDateTime(_obj);
                                 var _date = new fnn.DropDownCalenderX();
                                 if (_start_date != null)
                                 {
                                     _date.start_date = _start_date.Value;
                                     _date.end_date = sdata.CURR_DATE;
                                     _date.selected_date = sdata.CURR_DATE;
                                 }
                                 else
                                 {
                                     _date.start_date = sdata.CURR_DATE;
                                     _date.end_date = sdata.CURR_DATE;
                                     _date.selected_date = sdata.CURR_DATE;
                                 }
                                 fGrid.Rows["date"].Cells["desc"].DropDownControl = _date;

                             }
                         }
                         break;
                     }
                 case "exch_amount":
                 case "exch_rate":
                     {
                         fGrid.Rows["gain"].Cells[1].Value = null;
                         fGrid.Rows["loss"].Cells[1].Value = null;
                         fGrid.Rows["ug_amount"].Cells[1].Value = null;
                         if (_key == "exch_amount")
                         {
                             if ((fGrid.Rows["exch_amount"].Cells[1].Value.ToInt32() > fGrid.Rows["curr_amount"].Cells[1].Value.ToInt32()) | (fGrid.Rows["exch_amount"].Cells[1].Value.ToInt32() <= 0))
                             {
                                 MessageBox.Show("The Amount You Have Entered INVALID", "Invalid Value Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                 fGrid.Focus();
                                 fGrid.Rows[e.RowIndex].Cells[1].Value = null;
                                 fGrid.Rows[e.RowIndex].Cells[1].Selected = true;
                                 fGrid.Rows["exch_rate"].Cells[1].Value = null;
                                 return;
                             }
                         }
                         if (_key == "exch_rate")
                         {
                             int _c = (fGrid.Rows["exch_rate"].Cells[1].Value.ToFloat() - fGrid.Rows["curr_exch_rate"].Cells[1].Value.ToFloat()).ToInt32();
                             if (Math.Abs(_c) > (fGrid.Rows["curr_exch_rate"].Cells[1].Value.ToFloat() * 0.75).ToInt32())
                             {
                                 MessageBox.Show("The Exchange Rate You Entered Is Probably Wrong", "Wrong Exchange Rate Error");
                                 fGrid.Focus();
                                 fGrid.Rows[e.RowIndex].Cells[1].Value = null;
                                 fGrid.Rows[e.RowIndex].Cells[1].Selected = true;
                                 return;
                             }
                         }
                        
                         if (fGrid.Rows["exch_amount"].Cells[1].Value != null & fGrid.Rows["exch_rate"].Cells[1].Value != null)
                         {
                             fGrid.Rows["ug_amount"].Cells[1].Value = (fGrid.Rows["exch_amount"].Cells[1].Value.ToInt32() * fGrid.Rows["exch_rate"].Cells[1].Value.ToFloat()).ToInt32();
                             var _diff = (fGrid.Rows["ug_amount"].Cells[1].Value.ToInt32() - ((fGrid.Rows["exch_amount"].Cells[1].Value.ToInt32() * fGrid.Rows["curr_exch_rate"].Cells[1].Value.ToFloat()).ToInt32()));
                             if (_diff > 0)
                             {
                                 fGrid.Rows["gain"].Cells[1].Value = _diff;
                             }
                             else
                             {
                                 if (_diff != 0)
                                 {
                                     fGrid.Rows["loss"].Cells[1].Value = Math.Abs(_diff);
                                 }
                             }
                         }
                        
                         break;
                     }
               
               
            }
             if (e.RowIndex != fGrid.Rows.Count - 1)
             {
                 if (fGrid.Rows[e.RowIndex].Cells["desc"].Value == null) { return; }
                 for (int k = e.RowIndex + 1; k < fGrid.Rows.Count; k++)
                 {
                     if (fGrid.Rows[k].Type == iGRowType.AutoGroupRow)
                     {
                         fGrid.Rows[k].Expanded = true;
                         continue;
                     }
                     if (!fGrid.Rows[k].Visible) { continue; }
                     if (fGrid.Rows[k].Cells[1].Enabled == iGBool.False) { continue; }
                     if (fGrid.Rows[k].Cells[1].ReadOnly == iGBool.True) { continue; }
                     if (string.IsNullOrEmpty(fGrid.Rows[k].Key)) { continue; }
                     if ((e.RowIndex + 3) < fGrid.Rows.Count)
                     {
                         fGrid.Rows[(e.RowIndex + 3)].EnsureVisible();
                     }
                     fGrid.SetCurCell(k, e.ColIndex);
                     break;
                 }
             }
            
        }
        private bool CanSave()
        {
            foreach (var k in new string[] { "exch_amount", "exch_rate" })
            {
                if (fGrid.Rows[k].Cells[1].Value == null)
                {
                    MessageBox.Show("Important Field Left Blank", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    fGrid.Focus();
                    fGrid.SetCurCell(k, 1);
                    return false;
                }
            }
            return true;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F5)
            {
                buttoncreate.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void buttoncreate_Click(object sender, EventArgs e)
        {
            if (fGrid.CurCell != null)
            {
                fGrid.CommitEditCurCell();
            }
            if (!CanSave())
            {
                return;
            }
            ic.foreign_exchange_convC _c = new ic.foreign_exchange_convC();
            _c.curr_sys_exch_rate = fGrid.Rows["curr_exch_rate"].Cells[1].Value.ToFloat();
            _c.curr_sys_amount = fGrid.Rows["curr_amount"].Cells[1].AuxValue.ToInt32();
            _c.sys_account_id = (fGrid.Rows["currency"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag.ToInt32();
            _c.currency_id = datam.DATA_FOREIGN_CURRENCY.Values.Where(d => d.sys_account_id == _c.sys_account_id).FirstOrDefault().fr_currency_id;
            _c.fs_date = System.Convert.ToDateTime(fGrid.Rows["date"].Cells[1].Value);
            _c.fs_id = fn.GetFSID(_c.fs_date);
            _c.used_exch_rate = fGrid.Rows["exch_rate"].Cells[1].Value.ToFloat();
            _c.exchanged_amount = fGrid.Rows["exch_amount"].Cells[1].Value.ToInt32();
            _c.m_partition_id = string.Format("{0}{1}", _c.fs_date.Year, _c.fs_date.Month).ToInt32();
            _c.pc_us_id = sdata.PC_US_ID;
            _c.status = em.foreign_exch_statusS.valid;
            //
            string _str = "Are You Sure You Want To Save This Record ??";
            if (!dbm.WarningMessage(_str, "Save Warning"))
            {
                return;
            }
            using (var xd = new xing())
            {
                var _ts_id = accn.AccountsTransaction(xd, string.Format("Foreign Exchange for {0}", datam.DATA_FOREIGN_CURRENCY[_c.currency_id].fr_currency_name), _c.fs_date);
              _c.un_id = xd.SingleInsertCommandTSPInt("acc_foreign_exchange_tb", new string[]
                    {
                        "fs_date",
                        "fs_id",
                        "currency_id",
                        "sys_account_id",
                        "curr_sys_exch_rate",
                        "curr_sys_amount",
                        "used_exch_rate",
                        "exchanged_amount",
                        "status",
                        "m_partition_id",
                        "pc_us_id",
                        "fs_time_stamp",
                        "lch_id",
                        "transaction_id",
                        "edate"
                    }, new object[]
                    {
                        _c.fs_date,
                        _c.fs_id,
                        _c.currency_id,
                        _c.sys_account_id,
                        _c.curr_sys_exch_rate,
                        _c.curr_sys_amount,
                        _c.used_exch_rate,
                        _c.exchanged_amount,
                        _c.status.ToByte(),
                        _c.m_partition_id,
                        _c.pc_us_id,
                        0,
                        datam.LCH_ID,
                        _ts_id,
                        sdata.CURR_DATE
                    });

                //
                int _val = _c.exch_loss == 0 ? (_c.exchanged_amount * _c.curr_sys_exch_rate).ToInt32() : _c.converted_ug_amount;
                accn.JournalBook(xd, _c.fs_date, em.j_sectionS.unbanked, _ts_id, accn.GetAccountByAlias("UNBANKED_CASH").account_id, _val, 0);
                accn.JournalBook(xd, _c.fs_date, em.j_sectionS.unbanked, _ts_id, _c.sys_account_id, 0, _val);
                //
                if (_c.exch_loss > 0)
                {
                    accn.JournalBook(xd, _c.fs_date, em.j_sectionS.unbanked, _ts_id, _c.sys_account_id, 0, _c.exch_loss);
                    accn.JournalBook(xd, _c.fs_date, em.j_sectionS.loss, _ts_id, accn.GetAccountByAlias("FOREIGN_EXCHANGE_LOSS").account_id, _c.exch_loss, 0);
                }
                if (_c.exch_gain > 0)
                {
                    accn.JournalBook(xd, _c.fs_date, em.j_sectionS.unbanked, _ts_id, accn.GetAccountByAlias("UNBANKED_CASH").account_id, _c.exch_gain, 0);
                    accn.JournalBook(xd, _c.fs_date, em.j_sectionS.income, _ts_id, accn.GetAccountByAlias("FOREIGN_EXCHANGE_GAIN").account_id, 0, _c.exch_gain);
                }
                accn.ForeignCurrencyMVT(xd, datam.DATA_FOREIGN_CURRENCY[_c.currency_id], 0, _c.exchanged_amount, _c.fs_date);
                //
                xd.SingleUpdateCommandALL("acc_foreign_currency_tb",
                    new string[]
                    {
                        "last_exchange_date",
                        "last_exchange_id",
                        "fr_currency_id",
                        "lch_id"
                    }, new object[]
                    {
                        _c.fs_date,
                        _c.un_id,
                        _c.currency_id,
                        datam.LCH_ID
                    }, 2);
                //
                xd.CommitTransaction();

            }
            m_process = _process.after_insert;
            backworker.RunWorkerAsync();
            ClearGrid();
        }
        private void ClearGrid()
        {
            foreach (var k in fGrid.Rows.Cast<iGRow>())
            {
                k.Cells[1].Value = null;
                k.Cells[1].AuxValue = null;
            }
            fGrid.Rows["exch_amount"].Cells[0].Value = "Amount Exchanged";
            fGrid.Focus();
            fGrid.SetCurCell("currency", 1);

        }
        private void buttoncancel_Click(object sender, EventArgs e)
        {
            ClearGrid();
        }
    }
}
