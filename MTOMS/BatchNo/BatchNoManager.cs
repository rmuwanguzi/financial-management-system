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
    public partial class BatchNoManager : Form
    {
        public BatchNoManager()
        {
            InitializeComponent();
        }
        fs_class m_sabbath { get; set; }
        bool IsLoaded = false;
        private void BatchNoManager_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            InitializeGridColumns();
            backworker.RunWorkerAsync();
        }

        private void InitializeGridColumns()
        {
            #region Columns To Display
            Dictionary<string, string> Cols = new Dictionary<string, string>();
            Cols.Add("batch_no", "Batch\nNo");
            Cols.Add("batch_count", "Count");
            Cols.Add("batch_total", "Total");

            Cols.Add("entrant_count", "Count");
            Cols.Add("entrant_total", "Total");
          
            iGCol myCol;
            foreach (var c in Cols)
            {
                myCol = fGrid.Cols.Add(c.Key, c.Value);
                myCol.IncludeInSelect = true;
                myCol.SortType = iGSortType.None;
                myCol.ShowWhenGrouped = true;
                myCol.ColHdrStyle.TextAlign = iGContentAlignment.MiddleCenter;
                myCol.ColHdrStyle.ForeColor = Color.Black;
                myCol.ColHdrStyle.BackColor = Color.Thistle;
                myCol.ColHdrStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
                myCol.Width = 70;
                myCol.AllowSizing = true;
            }
            fGrid.Header.Rows.Add();
          
            //
            foreach (var f in new string[] { "batch_no"})
            {
                fGrid.Header.Cells[0, f].SpanRows = 2;
            }

            foreach (var k in new string[] { "batch_count", "batch_total", "entrant_count", "entrant_total" })
            {
                fGrid.Cols[k].ColHdrStyle.BackColor = Color.LightGray;
                fGrid.Cols[k].CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
            }
            //
            foreach (var k in new string[] { "batch_no" })
            {
                fGrid.Cols[k].CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
            }
            //
            foreach (var k in new string[] { "batch_count", "batch_total" })
            {
                fGrid.Cols[k].ColHdrStyle.ForeColor = Color.Maroon;
            }

            fGrid.Header.Cells[1, "batch_count"].SpanCols = 2;
            fGrid.Header.Cells[1, "batch_count"].Value = "Actual Cash Settings";
            fGrid.Header.Cells[1, "batch_count"].ForeColor = Color.Blue;
            fGrid.Header.Cells[1, "batch_count"].BackColor = Color.Thistle;
            //
            fGrid.Header.Cells[1, "entrant_count"].SpanCols = 2;
            fGrid.Header.Cells[1, "entrant_count"].Value = "MTOMS Entries";
            fGrid.Header.Cells[1, "entrant_count"].ForeColor = Color.Blue;

            fGrid.Header.Cells[1, "entrant_count"].BackColor = Color.Thistle;
            //

            //
            
           



            fGrid.DefaultRow.Height = fGrid.GetPreferredRowHeight(true, false);
            fGrid.DefaultAutoGroupRow.Height = fGrid.DefaultRow.Height;
            fGrid.Cols.AutoWidth();
            #endregion
        }
        public void CheckForUpdates()
        {
            backworker.RunWorkerAsync();
        }
        private void LoadSabbaths()
        {
           
            if (datam.CURR_FS == null || datam.DATA_FS == null || datam.DATA_FS.Keys.Count == 0)
            {
                return;
            }
            comboDate.Items.Clear();
            comboDate.DataSource = null;
            List<fs_class> _list = new List<fs_class>();
            IEnumerable<fs_class> new_list = null;
            new_list = from r in datam.DATA_FS[datam.CURR_FS.fs_year].Values
                       where r.fs_id <= (datam.CURR_FS.fs_id) & r.fs_week_dayname_no == 6
                       orderby r.fs_id descending
                       select r;

            foreach (var t in new_list)
            {
                _list.Add(t);
                comboDate.Items.Add(t);
                if (_list.Count > 5) { break; }
            }
            if (_list.Count < 5)
            {
                new_list = from r in datam.DATA_FS[datam.CURR_FS.fs_year - 1].Values
                           where r.fs_id <= datam.CURR_FS.fs_id & r.fs_week_dayname_no == 6
                           orderby r.fs_id descending
                           select r;
                int _cnt = _list.Count;
                foreach (var t in new_list)
                {

                    // _list.Add(t);
                    comboDate.Items.Add(t);
                    if (_cnt > 5) { break; }
                    _cnt++;
                }
            }
           // comboDate.DataSource = _list;
            comboDate.DisplayMember = "fs_date_long";
            comboDate.ValueMember = "fs_date_long";
        }
        private void LoadData()
        {
            iGRow _row = null;
            iGRow _parent = null;
            fGrid.BeginUpdate();
            fGrid.Rows.Clear();
            var gp_nlist = from k in datam.DATA_BATCH_NO_SETTINGS[m_sabbath.fs_id].Values
                          group k by k.entrant_id into nw_gp
                          select new
                          {
                              entrant_id = nw_gp.Key,
                              gp_data = nw_gp
                          };
            foreach (var _obj in gp_nlist)
            {
                if (!sdata.HasMenuRight(7, false))
                {
                    if (_obj.entrant_id != sdata.PC_US_ID)
                    {
                        continue;
                    }
                }
                _row = fGrid.Rows.Add();
                _row.Font = new Font("georgia", 12, FontStyle.Bold);
                _row.ReadOnly = iGBool.True;
                try
                {
                    _row.Cells["batch_no"].Value = sdata.GetUserName(_obj.entrant_id);
                }
                catch (Exception)
                {
                    _row.Cells["batch_no"].Value = null;
                }
                _row.Cells["batch_no"].ForeColor = Color.DarkBlue;
                _row.AutoHeight();
                //
                _parent = _row;
                //
                _parent.Level = 0;
                            
                foreach (var k in _obj.gp_data)
                {
                    #region
                   
                    _row = fGrid.Rows.Add();
                    _row.Font = new Font("georgia", 12, FontStyle.Regular);
                    _row.ReadOnly = iGBool.True;
                    _row.ForeColor = Color.Purple;
                    //
                    _row.Cells["batch_no"].Value = k.batch_no;
                    _row.Cells["batch_count"].Value = k.batch_count;
                    _row.Cells["batch_count"].TextAlign = iGContentAlignment.MiddleCenter;
                    _row.Cells["batch_total"].Value = k.batch_total;
                    _row.Cells["batch_total"].FormatString="{0:N0}";
                    _row.Cells["batch_total"].ValueType=typeof(int);
                    _row.Cells["batch_total"].TextAlign = iGContentAlignment.MiddleCenter;
                    //
                    _row.Cells["entrant_count"].Value = k.entrant_count;
                    _row.Cells["entrant_count"].TextAlign = iGContentAlignment.MiddleCenter;
                    _row.Cells["entrant_count"].ForeColor = Color.Black;
                    _row.Cells["entrant_total"].Value = k.entrant_total;
                    _row.Cells["entrant_total"].FormatString="{0:N0}";
                    _row.Cells["entrant_total"].ValueType=typeof(int);
                    _row.Cells["entrant_total"].ForeColor = Color.Black;
                    _row.Cells["entrant_total"].TextAlign = iGContentAlignment.MiddleCenter;
                    //
                     _row.Tag = k;
                     _row.Key = k.un_id.ToStringNullable();
                    _row.AutoHeight();
                    _row.Height += 2;
                    _row.TreeButton = iGTreeButtonState.Hidden;
                    _row.Level = 1;
                     #endregion
                }
               _parent.TreeButton = iGTreeButtonState.Visible;
                            
            }
            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
            fGrid.EndUpdate();
        }
        private void comboDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            fGrid.Rows.Clear();
            buttonAdd.Enabled = false;
            m_sabbath = null;
            if (comboDate.SelectedIndex == -1)
            {
                return;
            }
            //
            Application.DoEvents();
            datam.ShowWaitForm("Loading Records Please Wait...");
            m_sabbath = comboDate.SelectedItem as fs_class;
            if (m_sabbath != null)
            {
                using (var xd = new xing())
                {
                    datam.fill_batch_no_sabbath(xd, m_sabbath.fs_id);
                    xd.CommitTransaction();
                }
                LoadData();
                buttonAdd.Enabled = false;
                //if (m_sabbath.fs_id == sdata.CURR_FS.fs_id)
                //{
                //    if (sdata.HasMenuRight(6, false))
                //    {
                //        buttonAdd.Enabled = true;
                //    }
                //}
                if (comboDate.SelectedIndex == 0)
                {
                    if (sdata.HasMenuRight(6, false))
                    {
                        buttonAdd.Enabled = true;
                    }
                }
            }
            datam.HideWaitForm();
        }

        private void backworker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!IsLoaded)
            {
                datam.SystemInitializer();
            }
            else
            {
                using (var xd = new xing())
                {
                    datam.fill_batch_no_sabbath(xd, m_sabbath.fs_id);
                    xd.CommitTransaction();
                }
            }
        }

        private void backworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!IsLoaded)
            {
                IsLoaded = true;
                LoadSabbaths();
                if (comboDate.Items.Count > 0)
                {
                    comboDate.Enabled = true;
                    comboDate.SelectedIndex = 0;
                    if (!sdata.HasMenuRight(5, false))
                    {
                        comboDate.Enabled = false;
                    }
                }
            }
            else
            {
                LoadData();
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            using(var _fm= new BatchNoMaker())
            {
                _fm.Owner = this;
                _fm.Tag = m_sabbath;
                _fm.ShowDialog();
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            CheckForUpdates();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if(fGrid.SelectedRows.Count==0)
            {
                e.Cancel = true;
                return;
            }
            if (fGrid.SelectedRows[0].Level == 0)
            {
                e.Cancel = true;
                return;
            }
            ic.batch_no_SettingC _obj = fGrid.SelectedRows[0].Tag as ic.batch_no_SettingC;
            if (_obj.entrant_count != 0 & _obj.entrant_total != 0)
            {
                e.Cancel = true;
            }
        }

        private void deleteRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string _str = "Are You Sure You Want To Delete This Entry";
            if (!dbm.WarningMessage(_str, "Delete Warning"))
            {
                return;
            }
            ic.batch_no_SettingC _obj = fGrid.SelectedRows[0].Tag as ic.batch_no_SettingC;
            if (_obj != null)
            {
                using(var xd= new xing())
                {
                    if(xd.SingleUpdateCommand(string.Format("delete from off_batch_no_settings_tb where un_id={0} and entrant_count=0", _obj.un_id)))
                    {
                        xd.CommitTransaction();
                        fGrid.Rows.RemoveAt(fGrid.Rows[_obj.un_id.ToString()].Index);
                    }
                    else
                    {
                        MessageBox.Show("This Batch No Is Already Attached To Some Receipts,Cancel Them In Order To Proceed", "Delete Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }
    }
}
