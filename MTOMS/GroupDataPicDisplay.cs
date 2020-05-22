using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;
using SdaHelperManager;
namespace MTOMS
{
    public partial class GroupDataPicDisplay : DevComponents.DotNetBar.Office2007Form
    {
        public GroupDataPicDisplay()
        {
            InitializeComponent();
        }
        ic.GroupDataPicDisplay view_data = null;
        int m_ret_cols = 0;
        private void GroupDataPicDisplay_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            view_data = this.Tag as ic.GroupDataPicDisplay;
            if (view_data == null)
            {
                this.Close();
                return;
            }
            label1.Text = view_data.display_title;
            Application.DoEvents();
            iGrid1.CustomDrawCellForeground += new iGCustomDrawCellEventHandler(iGrid1_CustomDrawCellForeground);
            for (int j = 0; j < 30; j++)
            {
                var _col = iGrid1.Cols.Add(string.Format("col{0}", j), string.Format("col{0}", j));
                iGCellStyle myStyle = _col.CellStyle;
                myStyle.CustomDrawFlags |= iGCustomDrawFlags.Foreground;
                myStyle.ReadOnly = iGBool.True;
                _col.AllowSizing = false;
            }
            m_ret_cols = CalculteColumns();
            LoadPictureGrid();
        }
        private int CalculteColumns()
        {
            int g_size = (iGrid1.Width - (iGrid1.VScrollBar.Width + iGrid1.RowHeader.Width));
            if (g_size < 103) { return 103; }
            int ret_columns = (g_size / 103);
            return ret_columns;

        }
        void iGrid1_CustomDrawCellForeground(object sender, iGCustomDrawCellEventArgs e)
        {
            Image cell_img = (Image)iGrid1.Cells[e.RowIndex, e.ColIndex].Value;
            if (cell_img == null) { return; }//caused error the other time
            Region myOldClipRegion = e.Graphics.Clip;
            e.Graphics.SetClip(new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            try
            {
                e.Graphics.DrawImage(cell_img, new Rectangle(e.Bounds.X, e.Bounds.Y + (e.Bounds.Height - cell_img.Height) / 2, e.Bounds.Width, cell_img.Height));
            }
            finally
            {
                e.Graphics.Clip = myOldClipRegion;
            }
        }
        void LoadPictureGrid()
        {
            try
            {
                if (view_data.Data == null || view_data.Data.Count() == 0) { return; }
             int   record_count = 0;
                iGRow _row = null;
                iGrid1.BeginUpdate();
                iGrid1.Rows.Clear();
                var _nlist = (from v in view_data.Data
                              orderby v.HasPic descending, v.gender_id
                              select v);

                int y = 0;
                for (int j = 0; j < 30; j++)
                {
                    iGrid1.Cols[j].Visible = false;
                }
                record_count = _nlist.Count();
                bool _passed_first = false;
                ic.memberC _mem = null;
                iGCell _cell = null;

                while (y < _nlist.Count())
                {
                    _row = iGrid1.Rows.Add();
                    _row.Height = 150;
                    for (int j = 0; j < m_ret_cols; j++)
                    {
                        if (y == record_count) { break; }
                        _mem = _nlist.ElementAt(y);
                        if (_mem == null) { continue; }
                        if (!_passed_first)
                        {
                            if (!iGrid1.Cols[j].Visible)
                            {
                                iGrid1.Cols[j].Visible = true;
                            }
                        }
                        _cell = _row.Cells[j];
                        if (_mem.objPicture != null)
                        {
                            _cell.CustomDrawFlags |= iGCustomDrawFlags.Foreground;
                            _cell.Value = _mem.objPicture.SmallLabelPicture;
                            _cell.AuxValue = _mem.mem_id;
                            iGrid1.Cols[j].Width = 103;
                        }
                        else
                        {
                            _cell.CustomDrawFlags = iGCustomDrawFlags.None;
                            _cell.TextFormatFlags = iGStringFormatFlags.WordWrap;
                            _cell.TextAlign = iGContentAlignment.MiddleCenter;
                            if (_mem.mem_title_id > 0)
                            {
                                _cell.Value = string.Format("{0} {1}", xso.xso.DATA_COMMON[_mem.mem_title_id].item_name, _mem.mem_name).ToProperCase();
                            }
                            else
                            {
                                _cell.Value = string.Format("{0}", _mem.mem_name).ToProperCase();
                            }
                            _cell.AuxValue = _mem.mem_id;
                            _cell.BackColor = Color.Gainsboro;
                            iGrid1.Cols[j].Width = 103;

                        }
                        y++;
                    }
                    _passed_first = true;
                }
                iGrid1.AutoResizeCols = false;
                iGrid1.EndUpdate();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
