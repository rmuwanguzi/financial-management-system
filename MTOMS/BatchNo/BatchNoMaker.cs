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
    public partial class BatchNoMaker : DevComponents.DotNetBar.Office2007Form
    {
        public BatchNoMaker()
        {
            InitializeComponent();
        }
        private enum _process
        {
            form_loading = 0,
           update_after_insert,
          
        }
        _process m_process = _process.form_loading;
        List<int> m_DataEntrants { get; set; }
        fs_class m_sabbath = null;
        private void BankMaker_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
            CenterToScreen();
            m_sabbath = this.Tag as fs_class;
            if(m_sabbath==null)
            {
                this.Close();
                return;
            }
            InitIgridColumns();
            datam.ShowWaitForm();
            Application.DoEvents();
            datam.SecurityCheck();
            m_DataEntrants = new List<int>();
            m_process = _process.form_loading;
            backworker.RunWorkerAsync();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            
            if (keyData == Keys.Tab)
            {
                if (fGrid.Rows.Count > 0 )
                {
                    if (buttoncreate.Enabled)
                    {
                        buttoncreate.PerformClick();
                    }
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
          
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
            switch (m_process)
            {
                case _process.form_loading:
                    {
                        datam.SystemInitializer();
                        using(var xd= new xing())
                        {
                            using(var _dr= xd.SelectCommand("select us_id from pcuser_tb"))
                            {
                                while(_dr.Read())
                                {
                                    m_DataEntrants.Add(_dr[0].ToInt32());
                                }
                            }
                            xd.CommitTransaction();
                        }
                       
                        break;
                    }
                case _process.update_after_insert:
                    {
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
                      
                        LoadMainGrid();
                        datam.HideWaitForm();
                        fGrid.Focus();
                        fGrid.SetCurCell("data_entrant", 1);
                        break;
                    }
                case _process.update_after_insert:
                    {
                        //if (this.Owner is TransPaymentsManager)
                        //{
                        //    (this.Owner as TransPaymentsManager).CheckForUpdates();
                        //}
                        break;
                    }
            }
        }
        private void LoadMainGrid()
        {
            fGrid.BeginUpdate();
            iGRow _row = null;
            _row = fGrid.Rows.Add();
            _row.Height = 7;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            int group_index = 1;
            string group_name = "New Account Information";
            //
            _row = CreateNewRow("Data Entrant ", "data_entrant", typeof(string), group_index, group_name);
            _row.Cells[1].ForeColor = Color.DarkBlue;
            var icombo = fnn.CreateCombo();
            foreach (var b in m_DataEntrants)
            {
                try
                {
                    icombo.Items.Add(new fnn.iGComboItemEX()
                           {
                               Value = sdata.GetUserName(b),
                               Tag = b,
                               ID = b
                           });
                }
                catch (Exception)
                {
                     continue;
                }
            }
            _row.Cells[1].DropDownControl = icombo;
            _row.Cells[1].Value = null;
            _row = fGrid.Rows.Add();
            _row.Height = 5;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            _row = CreateNewRow("Batch No", "batch_no", typeof(string), group_index, group_name);
            _row = CreateNewRow("Batch Record Count", "batch_count", typeof(string), group_index, group_name);
            _row = CreateNewRow("Batch Total (Ugx)", "batch_total", typeof(decimal), group_index, group_name);
            _row.Cells[1].FormatString = "{0:N0}";
            _row = fGrid.Rows.Add();
            _row.Height = 5;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
       
           
            
            fGrid.EndUpdate();
        }
        private iGRow CreateNewRow(string field, string rowkey, Type _type, int group_index, string group_name)
        {
            var _row = fGrid.Rows.Add();
            _row.Font = new Font("georgia", 14, FontStyle.Regular);
            _row.Cells["desc"].Font = new Font("arial", 14, FontStyle.Regular);
            _row.Cells["name"].Col.Width = 230;
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

        private void fGrid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
        {
            if (fGrid.Rows[e.RowIndex].Cells["desc"].DropDownControl != null && fGrid.Rows[e.RowIndex].Cells["desc"].AuxValue == null)
            {
                fGrid.Rows[e.RowIndex].Cells["desc"].Value = null;
                return;
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

        private void buttoncancel_Click(object sender, EventArgs e)
        {
            ClearGrid();
        }
        private void ClearGrid()
        {
            foreach (var k in fGrid.Rows.Cast<iGRow>())
            {
                k.Cells[1].Value = null;
                k.Cells[1].AuxValue = null;
            }
            fGrid.Focus();
            fGrid.SetCurCell("data_entrant", 1);

        }
        private bool IsValid()
        {
           
            var _comp = new string[] { "data_entrant", "batch_no","batch_count", "batch_total" };
            foreach (var k in _comp)
            {
                if (fGrid.Rows[k].Cells[1].Value == null)
                {
                    MessageBox.Show("Important Field Left Blank", "Save Error");
                    fGrid.Focus();
                    fGrid.SetCurCell(k, 1);
                    return false;
                }
            }
            return true;
        }
        private void buttoncreate_Click(object sender, EventArgs e)
        {
            if (fGrid.CurCell != null)
            {
                fGrid.CommitEditCurCell();
            }
            if (!IsValid())
            {
                return;
            }
            string _str = "Are You Sure You Want To Save This Record ??";
            if (!dbm.WarningMessage(_str, "Save Warning"))
            {
                return;
            }
            //
            ic.batch_no_SettingC _obj = new ic.batch_no_SettingC();
            _obj.batch_count = fGrid.Rows["batch_count"].Cells[1].Value.ToInt32();
            _obj.batch_no = fGrid.Rows["batch_no"].Cells[1].Text.ToUpper();
            _obj.batch_total = fGrid.Rows["batch_total"].Cells[1].Value.ToInt32();
            _obj.entrant_id = (fGrid.Rows["data_entrant"].Cells[1].AuxValue as fnn.iGComboItemEX).ID;
            _obj.sab_fs_id = m_sabbath.fs_id;
            
            using (var xd = new xing())
            {
                var _ret_val = xd.SingleInsertCommandIgnore("off_batch_no_settings_tb",
                        new string[]
                              {
                                  "batch_no",
                                  "batch_count",
                                  "fs_time_stamp",
                                  "batch_total",
                                  "entrant_id",
                                  "lch_id",
                                  "sab_fs_id"
                              }, new object[]
                              {
                                 _obj.batch_no,
                                 _obj.batch_count,
                                 0,
                                 _obj.batch_total,
                                 _obj.entrant_id,
                                  datam.LCH_ID,
                                  _obj.sab_fs_id
                                });
                if (_ret_val == 0)
                {
                    MessageBox.Show("You Have Already Entered This BATCH NUMBER", "DUPLICATE BATCH NO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    fGrid.Focus();
                    fGrid.SetCurCell("batch_no", 1);
                    fGrid.Rows["batch_no"].Cells[1].Value = null;
                    return;
                }
                xd.CommitTransaction();
            }
            (this.Owner as BatchNoManager).CheckForUpdates();
            ClearGrid();
           
        }
    }
}
