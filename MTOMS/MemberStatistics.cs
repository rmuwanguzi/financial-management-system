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
    public partial class MemberStatistics : DevComponents.DotNetBar.Office2007Form
    {
        public MemberStatistics()
        {
            InitializeComponent();
        }
        iGrid[] m_grids = null;
        private void MemberStatistics_Load(object sender, EventArgs e)
        {
            this.Office2007ColorTable = DevComponents.DotNetBar.Rendering.eOffice2007ColorScheme.Silver;
            this.BackColor = Color.WhiteSmoke;
            datam.SecurityCheck();
            xso.xso.Intialize();
            datam.SystemInitializer();
            m_grids = new iGrid[] { iGrid1, iGrid2, iGrid3 };
            InitIgridColumns();
             foreach (var g in m_grids)
             {
                 g.AfterAutoGroupRowCreated += new iGAfterAutoGroupRowCreatedEventHandler(g_AfterAutoGroupRowCreated);
             }
            ResizePanels();
            Application.DoEvents();
            FillGrid();
        }
        void g_AfterAutoGroupRowCreated(object sender, iGAfterAutoGroupRowCreatedEventArgs e)
        {
            iGrid objfgrid = sender as iGrid;
            if (objfgrid == null) { return; }
            iGCell myGroupRowCell = objfgrid.RowTextCol.Cells[e.AutoGroupRowIndex];
            myGroupRowCell.Row.AutoHeight();
            myGroupRowCell.Row.Height = myGroupRowCell.Row.Height + 3;
            myGroupRowCell.Row.Selectable = false;
            iGCell myFirstCellInGroup = objfgrid.Cells[e.GroupedRowIndex, "category"];
            myGroupRowCell.Row.Key = string.Format("GP{0}", objfgrid.Cells[e.GroupedRowIndex, "svalue"].Value.ToInt32());
            myFirstCellInGroup.TextAlign = iGContentAlignment.MiddleCenter;
            myGroupRowCell.Value = string.Format("{0}", myFirstCellInGroup.Value.ToString());

        }
        void InitIgridColumns()
        {
           
            // Set the flat appearance for the cells.
            
            iGCol myCol;
            foreach (var _grid in m_grids)
            {
                #region Adjust the grid's appearance
                _grid.Appearance = iGControlPaintAppearance.StyleFlat;
                _grid.UseXPStyles = false;
                _grid.ScrollBarSettings.Appearance = iGControlPaintAppearance.StyleFlat;
                _grid.FocusRect = false;
               #endregion
                //
                myCol = _grid.Cols.Add("name", "Field Name");
                myCol.CellStyle.ReadOnly = iGBool.True;
                myCol.SortType = iGSortType.None;
                myCol = _grid.Cols.Add("value", "Description");
                myCol.SortType = iGSortType.None;
                myCol.AllowSizing = false;
               // myCol = _grid.Cols.Add("desc", "Description");
              
                _grid.Cols.Add("category", string.Empty).Visible = false;
                _grid.Cols.Add("svalue", string.Empty).Visible = false;
                myCol = _grid.Cols["svalue"];
                myCol.SortType = iGSortType.ByValue;
                _grid.DefaultRow.Height = _grid.GetPreferredRowHeight(true, false);
                _grid.DefaultAutoGroupRow.Height = _grid.DefaultRow.Height;
            }
        }
        private void ResizePanels()
        {
            if (m_grids == null) { return; }
            var _client_width = this.ClientSize.Width;
            int _spacing = 5;
            _client_width -= (_spacing * 4);
            var _grid_width = (_client_width / 3);
            int _left = _spacing;
            int _height = this.ClientSize.Height - (panel1.Bottom + (_spacing * 2));
             foreach (var t in m_grids)
            {
                t.Left = _left;
                t.Width = _grid_width;
                _left = (t.Left + _grid_width + _spacing);
                t.Top = (panel1.Bottom + _spacing);
                t.Height = _height;
            }
             foreach (var g in m_grids)
             {
                // g.Cols[0].Width = ((3 * g.ClientSize.Width) / 4)-5;
                 g.Cols[1].AutoWidth();
                 g.Cols[0].Width = (g.ClientSize.Width-40) - g.Cols[1].Width;
                g.AutoResizeCols = false;
               
             }
        }
        private void MemberStatistics_SizeChanged(object sender, EventArgs e)
        {
            ResizePanels();
        }
        private void SortAndGroup()
        {
           
            foreach (var _g in m_grids)
            {
                _g.GroupObject.Clear();
                _g.SortObject.Clear();
                _g.GroupObject.Add("svalue");
                _g.SortObject.Add("svalue", iGSortOrder.Ascending);
                _g.Group();
            }
        }
        private void ClearAllGrids()
        {
            foreach (var g in m_grids)
            {
                g.Rows.Clear();
            }
        }
        private void FillGrid()
        {
            ClearAllGrids();
            if (datam.DATA_MEM_STATS == null) { return; }
            byte _index = 0;
            byte _counter = 0;
            iGrid _grid = null;
            iGRow _row = null;
            foreach (var g in m_grids)
            {

                g.BeginUpdate();
            }
            foreach (var c in datam.DATA_MEM_STATS.Values.OrderBy(j=>j.cat_index))
            {
                _grid = m_grids[_index];
                foreach (var t in c.TypeCollection.Values)
                {
                    if (c.is_optional & t.count == 0) { continue; }
                    _row = _grid.Rows.Add();
                    _row.ReadOnly = iGBool.True;
                    _row.Font = new Font("verdana", 11, FontStyle.Regular);
                    _row.Cells["name"].Value = t.type_name;
                    _row.Cells["category"].Value = c.cat_name;
                    _row.Cells["value"].Value = t.count == 0 ? null : t.count.ToStringNullable();
                    _row.Cells["svalue"].Value = c.cat_index;
                    _row.Cells[1].ForeColor = Color.Black;
                    _row.Cells[0].ForeColor = Color.DarkGreen;
                    _row.AutoHeight();
                }
                _counter++;
                if(_counter==5 & _index!=2)
                {
                    _index++;
                    _counter=0;
                }
            }
            SortAndGroup();
            foreach (var g in m_grids)
            {
                g.Cols[0].Width = ((3 * g.ClientSize.Width) / 4);
                g.Cols[1].AutoWidth();
                g.AutoResizeCols = false;
                g.EndUpdate();
            }
        }
       
    }
}
