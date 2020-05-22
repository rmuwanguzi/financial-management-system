using SdaHelperManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;

namespace MTOMS.ChurchGroups
{
    public partial class ChurchGroupTypeMaker : DevComponents.DotNetBar.Office2007Form
    {
        public ChurchGroupTypeMaker()
        {
            InitializeComponent();
        }
       
        private void ChurchGroupTypeMaker_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            datam.SecurityCheck();
            using (var xd = new xing())
            {
                datam.fill_church_group_types(xd);
                xd.CommitTransaction();
            }
            InitializeGridPhoneColumns();
       
            //
            iGridCategory.AfterCommitEdit += new iGAfterCommitEditEventHandler(iGridCategory_AfterCommitEdit);
            iGridCategory.TextBoxKeyDown += new iGTextBoxKeyDownEventHandler(iGridCategory_TextBoxKeyDown);
            //
            LoadCategoryGrid();
        }
      
        private void InitializeGridPhoneColumns()
        {
            #region Columns To Display
            List<iGrid> _grids = new List<iGrid>()
            {
                iGridCategory
            };
            string[] _cols = new string[] 
            { 
               #region MyRegion
		        "Phone No", 
                "X"
                #endregion
            };
            iGCol myCol = null;
            foreach (var _grid in _grids)
            {

                foreach (var c in _cols)
                {
                    myCol = _grid.Cols.Add(c, c);
                    myCol.IncludeInSelect = true;
                    myCol.SortType = iGSortType.None;

                    myCol.ColHdrStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    myCol.ColHdrStyle.ForeColor = Color.DarkGreen;
                    myCol.ColHdrStyle.Font = new Font("arial", 10, FontStyle.Bold);
                    // myCol.Width = 70;
                    myCol.AllowSizing = false;
                }

                if (_grid == iGridCategory)
                {
                    _grid.Cols[0].Text = "Church Group Types";
                }

                _grid.Cols[1].Visible = false;
                //
                _grid.Cols["X"].CellStyle.ValueType = typeof(string);
                _grid.Cols["X"].ColHdrStyle.ForeColor = Color.Maroon;
                _grid.Cols["X"].CellStyle.TypeFlags = iGCellTypeFlags.NoTextEdit | iGCellTypeFlags.HasEllipsisButton;
                _grid.Cols["X"].AllowSizing = false;
                _grid.Cols["X"].CellStyle.Selectable = iGBool.False;
                _grid.Cols[1].Visible = false;
                //
                _grid.DefaultRow.Height = _grid.GetPreferredRowHeight(true, false);
                _grid.DefaultAutoGroupRow.Height = _grid.DefaultRow.Height;
                _grid.Cols.AutoWidth();
            }

            #endregion
        }
        private void LoadCategoryGrid()
        {

            iGRow _row = null;
            iGridCategory.BeginUpdate();
            foreach (var r in datam.DATA_CG_TYPES.Values)
            {
                _row = iGridCategory.Rows.Add();
                _row.Cells[0].Col.Width = 290;
                _row.Cells[1].Col.Width = 22;
                _row.Font = new Font("verdana", 11, FontStyle.Regular);
                _row.ForeColor = Color.DarkBlue;
                _row.AutoHeight();
                _row.Height += 1;
                _row.Tag = r;
                _row.Cells[0].Value = r.cg_type_name;
            }
            iGridCategory.EndUpdate();

        }
        void iGridCategory_TextBoxKeyDown(object sender, iGTextBoxKeyDownEventArgs e)
        {
            var _grid = sender as iGrid;
            if (e.KeyValue == Keys.Down | e.KeyValue == Keys.Up)
            {
                _grid.CommitEditCurCell();
            }
        }
        void iGridCategory_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
        {
            iGrid _grid = sender as iGrid;
            if (_grid.Rows[e.RowIndex].Cells[0].Value != null)
            {
                _grid.Rows[e.RowIndex].Cells[0].Value = _grid.Rows[e.RowIndex].Cells[0].Value.ToProperCase();
            }
            if (sender == iGridCategory)
            {
                #region Item Category
                if (_grid.Rows[e.RowIndex].Cells[0].Value == null)
                {
                    if (_grid.Rows[e.RowIndex].Tag != null)
                    {
                        var _cg = _grid.Rows[e.RowIndex].Tag as ic.church_group_typeC;
                        using (var xd = new xing())
                        {

                            if (xd.ExecuteScalarInt("select TOP 1 cg_id from church_group_tb where cg_type_id=" + _cg.cg_type_id ) == 0)
                            {
                                xd.SingleDeleteCommandExp("church_group_types_tb", new string[] { "cg_type_id" }, new int[] { _cg.cg_type_id }); xd.CommitTransaction();
                            }
                            else
                            {
                                _grid.Rows[e.RowIndex].Cells[0].Value = _cg.cg_type_name;
                                dbm.ErrorMessage("There Are Some Church Groups Which Are Attached To This Church Group Type", "Delete Operation Will Be Cancelled");
                                return;
                            }
                        }
                        
                        _grid.Rows[e.RowIndex].Tag = null;
                        datam.DATA_CG_TYPES.Remove(_cg.cg_type_id);
                        _grid.Rows.RemoveAt(e.RowIndex);
                        this.Tag = 1;
                    }
                    return;
                }
                else
                {
                    if (_grid.Rows[e.RowIndex].Tag == null)
                    {
                        //insert
                        using (var xd = new xing())
                        {
                            var ret_val = xd.ExecuteScalerInt(new string[] { "cg_type_name" }, string.Format("select count(cg_type_id) as cnt from church_group_types_tb where lower(cg_type_name)=@cg_type_name"), new object[] { _grid.Rows[e.RowIndex].Cells[0].Text.ToLower() });
                            if (ret_val > 0)
                            {
                                MessageBox.Show("You Have Already Entered This Item", "Duplicate Item Entry");
                                _grid.Rows[e.RowIndex].Cells[0].Value = null;
                                return;
                            }
                        }
                        var _cg = new ic.church_group_typeC();
                        using (var xd = new xing())
                        {
                            _cg.cg_type_name = _grid.Rows[e.RowIndex].Cells[0].Text.ToProperCase();
                            _cg.cg_type_id = xd.SingleInsertCommandTSPInt("church_group_types_tb", new string[] { "cg_type_name", "fs_time_stamp", "lch_id" },
                             new object[] { _cg.cg_type_name, 0, datam.LCH_ID });
                            xd.CommitTransaction();
                        }
                        _grid.Rows[e.RowIndex].Tag = _cg;
                        datam.DATA_CG_TYPES.Add(_cg.cg_type_id, _cg);
                        this.Tag = 1;
                    }
                    else
                    {
                        var _cg = _grid.Rows[e.RowIndex].Tag as ic.church_group_typeC;
                        _cg.cg_type_name = _grid.Rows[e.RowIndex].Cells[0].Value.ToStringNullable();
                        using (var xd = new xing())
                        {
                            xd.SingleUpdateCommandALL("church_group_types_tb", new string[] { "cg_type_name", "cg_type_id" }, new object[] { _cg.cg_type_name, _cg.cg_type_id }, 1);
                            xd.CommitTransaction();
                        }
                        this.Tag = 1;
                        //update
                    }
                    Application.DoEvents();
                    using (var xd = new xing())
                    {
                        datam.fill_church_group_types(xd);
                        xd.CommitTransaction();
                    }
                }
                #endregion
            }
            if (((e.RowIndex + 1) == _grid.Rows.Count))
            {
                AddRowX(_grid);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddRowX(iGridCategory);
        }
        private void AddRowX(iGrid _grid)
        {
            if (_grid.Rows.Cast<iGRow>().Count(j => j.Cells[0].Value == null) > 0)
            {
                _grid.Focus();
                var _cell = _grid.Rows.Cast<iGRow>().Where(j => j.Cells[0].Value == null).Select(v => v.Cells[0]).FirstOrDefault();
                if (_cell != null)
                {
                    _cell.Selected = true;
                }
                return;
            }
            iGRow _row = null;
            _grid.Focus();
            _row = _grid.Rows.Add();
            _row.Cells[0].Col.Width = 290;
            _row.Cells[1].Col.Width = 22;
            _row.Font = new Font("verdana", 11, FontStyle.Regular);
            _row.ForeColor = Color.DarkBlue;
            _row.AutoHeight();
            _row.Height += 1;
            _grid.SetCurCell(_grid.Rows.Count - 1, 0);
            SendKeys.Send("{ENTER}");
            _grid.AutoResizeCols = false;
        }
    }
}
