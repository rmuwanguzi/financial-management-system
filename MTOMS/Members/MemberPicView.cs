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

namespace MTOMS.Members
{
    public partial class MemberPicView : DevComponents.DotNetBar.Office2007Form
    {
        public MemberPicView()
        {
            InitializeComponent();
        }
        void LoadGrid(int cols)
        {
            iGRow _row = null;
            iGrid1.BeginUpdate();
            iGrid1.Rows.Clear();
            var _nlist = (from h in datam.DATA_MEMBER.Values
                          where h.objPicture != null
                          select h).ToList();
            int y = 0;
            while (y < _nlist.Count)
            {
                _row = iGrid1.Rows.Add();
                _row.Height = 150;
                for (int j = 0; j < cols; j++)
                {
                    if (y == _nlist.Count) { break; }
                    if (!iGrid1.Cols[j].Visible)
                    {
                        iGrid1.Cols[j].Visible = true;
                    }
                    _row.Cells[j].Value = _nlist[y].objPicture.SmallLabelPicture;
                    iGrid1.Cols[j].Width = 102;
                    y++;
                }
            }
            iGrid1.AutoResizeCols = false;
            iGrid1.EndUpdate();
          
        }
        private void MemberPicView_Load(object sender, EventArgs e)
        {
            datam.SecurityCheck();
            datam.SystemInitializer();
            iGrid1.CustomDrawCellForeground += new iGCustomDrawCellEventHandler(iGrid1_CustomDrawCellForeground);
            iGrid1.ClientSizeChanged += new EventHandler(iGrid1_ClientSizeChanged);
            for (int j = 0; j < 30; j++)
            {
                var _col = iGrid1.Cols.Add(string.Format("col{0}", j), string.Format("col{0}", j));
                iGCellStyle myStyle = _col.CellStyle;
                myStyle.CustomDrawFlags |= iGCustomDrawFlags.Foreground;
                myStyle.ReadOnly = iGBool.True;
                _col.AllowSizing = false;
             }
                         
        }
        void iGrid1_ClientSizeChanged(object sender, EventArgs e)
        {
            CalculteColumns();
        }
        private void CalculteColumns()
        {
            int g_size = (iGrid1.Width - (iGrid1.VScrollBar.Width + iGrid1.RowHeader.Width));
            if (g_size < 103) { return; }
            int ret_columns = (g_size / 103);
            for (int j = ret_columns; j < 30; j++)
            {
                iGrid1.Cols[j].Visible = false;
            }
            LoadGrid(ret_columns);
            
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
        private void printGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IGridPrint.NormalPrint("General Report", iGrid1, false);
        }
         
    }
}
