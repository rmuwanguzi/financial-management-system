﻿using System;
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
    public partial class TransferForm : DevComponents.DotNetBar.Office2007Form
    {
        public TransferForm()
        {
            InitializeComponent();
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

        }
        ic.memberC mem_obj = null;
        string[] _canc = new string[] { "transfer_date", "church", "reason","comment" };
        private iGDropDownList CreateCombo()
        {
            iGDropDownList icombo = new iGDropDownList();
            //icombo.SelectedItemChanged += new iGSelectedItemChangedEventHandler(icombo_SelectedItemChanged);
            icombo.MaxVisibleRowCount = 15;
            icombo.SearchAsType.MatchRule = iGMatchRule.Contains;
            icombo.SearchAsType.AutoCompleteMode = iGSearchAsTypeMode.Filter;
            icombo.SelItemBackColor = Color.Lavender;
            return icombo;
        }
        private iGRow CreateNewRow(string field, string rowkey, Type _type, int group_index, string group_name)
        {
            var _row = fGrid.Rows.Add();
            _row.Font = new Font("verdana", 13, FontStyle.Regular);
            _row.Cells["name"].Col.Width = 180;
            _row.Cells["name"].Value = field;
            _row.Cells["name"].TextAlign = iGContentAlignment.BottomRight;
            _row.Cells["desc"].ValueType = _type;
            _row.Key = rowkey;
            _row.AutoHeight();
            return _row;

        }
        private void TransferForm_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            mem_obj = this.Tag as ic.memberC;
            this.BackColor = Color.WhiteSmoke;
            datam.SecurityCheck();
            datam.SystemInitializer();
            InitIgridColumns();
            LoadMainGrid();
            Application.DoEvents();
            fGrid.SetCurCell("transfer_date", 1);
        }
        private void LoadMainGrid()
        {
            try
            {
                fGrid.BeginUpdate();
                iGRow _row = null;
                iGDropDownList icombo = null;
                #region Personal Information
                int group_index = 1;
                string group_name = "Member Information";
                //current year
                _row = CreateNewRow(null, null, typeof(string), group_index, group_name);
                _row.Selectable = false;
                _row.Height = 5;
                _row.BackColor = Color.Lavender;
                _row = CreateNewRow("Member Name", "mem_name", typeof(string), group_index, group_name);
                _row.ReadOnly = iGBool.True;
                _row.Cells["desc"].Value = mem_obj != null ? mem_obj.mem_name : string.Empty;
                //
                _row.Cells["desc"].Value = mem_obj != null ? mem_obj.mem_name : string.Empty;
                _row = CreateNewRow("Member Code", "mem_code", typeof(string), group_index, group_name);
                _row.Cells[1].Enabled = iGBool.False;
                _row.ReadOnly = iGBool.True;
                _row.Cells["desc"].Value = mem_obj != null ? mem_obj.mem_code : string.Empty;

                //
                if (mem_obj.ChurchGroupCollection != null && mem_obj.ChurchGroupCollection.Count > 0)
                {
                    _row = CreateNewRow("Group Name", "fg_group", typeof(string), group_index, group_name);
                    _row.ReadOnly = iGBool.True;
                    _row.Cells[1].Enabled = iGBool.False;
                   // _row.Cells["desc"].Value = mem_obj != null ? datam.DATA_FELLOWSHIPGROUPS[mem_obj.fg_id].fg_name : string.Empty;
                }
                //
                _row = CreateNewRow("Member Picture", "mem_pic", typeof(object), group_index, group_name);
                _row.Cells["desc"].ReadOnly = iGBool.True;
                _row.Cells["name"].TypeFlags = iGCellTypeFlags.NoTextEdit;
                _row.Cells["name"].Selectable = iGBool.False;
                _row.Cells["name"].TextAlign = iGContentAlignment.MiddleRight;
                _row.Cells["desc"].CustomDrawFlags |= iGCustomDrawFlags.Foreground;
                if (mem_obj != null && mem_obj.objPicture != null)
                {
                    var _img = fn.IMAGERESIZER(mem_obj.objPicture.SmallPicture, (mem_obj.objPicture.SmallPicture.Width * .75).ToFloat(), (mem_obj.objPicture.SmallPicture.Height * .75).ToFloat());
                    _row.Cells["desc"].Value = _img;
                    fGrid.Rows["mem_pic"].Height = 90;
                }
               
                //
                _row = CreateNewRow(null, null, typeof(string), group_index, group_name);
                _row.Selectable = false;
                _row.Height = 5;
                _row.BackColor = Color.Lavender;
                _row = CreateNewRow("* Transfer Date", "transfer_date", typeof(Int32), group_index, group_name);
                _row.Cells["desc"].TypeFlags = iGCellTypeFlags.ComboPreferValue | iGCellTypeFlags.NoTextEdit | iGCellTypeFlags.HasEllipsisButton;
                _row.Cells["desc"].FormatString = "{0:D}";// {0:d} for short date string
                _row.Cells["desc"].Value = null;// DateTime.Parse("1/1/2010");
                _row.Cells["desc"].DropDownControl = new fnn.DropDownCalendar();
                _row.Cells["name"].ForeColor = Color.Green;
                //
                _row = CreateNewRow("New Church Name", "church", typeof(string), group_index, group_name);
                var ch_list = from n in xso.xso.DATA_REGION.Values
                              where (n.item_type == xso.em.cregion_type.church |
                              n.item_type == xso.em.cregion_type.church_company |
                              n.item_type == xso.em.cregion_type.sabbath_school)
                              select n;
                icombo = CreateCombo();
                icombo.SelItemBackColor = Color.Lavender;
                icombo.SearchAsType.MatchRule = iGMatchRule.StartsWith;
                icombo.MaxVisibleRowCount = 7;
                foreach (var f in ch_list)
                {
                    icombo.Items.Add(new fnn.iGComboItemEX()
                    {
                        Text = f.item_name,
                        Value = f.item_name,
                        Tag = f
                    });
                }
                _row.Cells["desc"].DropDownControl = icombo;
                _row.Cells["desc"].Value = null;
                //
                _row = CreateNewRow("Reason", "reason", typeof(string), group_index, group_name);
                _row.Height = 60;
                _row.Cells["desc"].TextFormatFlags = iGStringFormatFlags.WordWrap;
                _row.Cells["name"].TextAlign = iGContentAlignment.MiddleCenter;
               //
                _row = CreateNewRow("Comment", "comment", typeof(string), group_index, group_name);
                _row.Height = 60;
                _row.Cells["desc"].TextFormatFlags = iGStringFormatFlags.WordWrap;
                _row.Cells["name"].TextAlign = iGContentAlignment.MiddleCenter;

                #endregion
                fGrid.EndUpdate();
            }
            catch (Exception ed)
            {
                MessageBox.Show(ed.Message);
            }

        }
        private void fGrid_CustomDrawCellForeground(object sender, iGCustomDrawCellEventArgs e)
        {
            Image cell_img = (Image)fGrid.Cells[e.RowIndex, e.ColIndex].Value;
            if (cell_img == null) { return; }//caused error the other time
            Region myOldClipRegion = e.Graphics.Clip;
            e.Graphics.SetClip(new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            try
            {
                e.Graphics.DrawImage(cell_img, new Rectangle(e.Bounds.X, e.Bounds.Y + (e.Bounds.Height - cell_img.Height) / 2, cell_img.Width, cell_img.Height));
            }
            finally
            {
                e.Graphics.Clip = myOldClipRegion;
            }
        }
        private void fGrid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
         {
             switch (fGrid.Rows[e.RowIndex].Key)
             {
                 case "transfer_date":
                     {
                         if (fGrid.Rows[e.RowIndex].Cells["desc"].AuxValue != null)
                         {
                             buttonsave.Enabled = true;
                             return;
                         }
                         else
                         {
                             buttonsave.Enabled = false;
                             return;
                         }
                         
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
                     fGrid.SetCurCell(k, e.ColIndex);
                     break;
                 }
             }
         }
        private void fGrid_EllipsisButtonClick(object sender, iGEllipsisButtonClickEventArgs e)
         {
             var cv = fGrid.Rows[e.RowIndex];
           if (string.IsNullOrEmpty(cv.Key)) { return; }
           
               if (cv.Cells[1].Value == null) { return; }
               string _str = "Are You Sure You Want To Cancel The Date";
               if (!dbm.WarningMessage(_str, "Cancel Warning"))
               {
                   return;
               }
               cv.Cells[1].Value = null;
               cv.Cells[1].AuxValue = null;
               buttonsave.Enabled = false;
         }
        private void buttoncancel_Click(object sender, EventArgs e)
         {
             if (fGrid.Rows.Count == 0) { return; }
             foreach (var t in _canc)
             {
                 fGrid.Rows[t].Cells["desc"].AuxValue = null;
                 fGrid.Rows[t].Cells["desc"].Value = null;
             }
             buttonsave.Enabled = false;
         }
        private void buttonsave_Click(object sender, EventArgs e)
         {
             string _str = "Are You Sure You Want To Transfer The Selected Member ??";
             if (!dbm.WarningMessage(_str, "Transfer Warning"))
             {
                 return;
             }
             ic.transferC _tc = new MTOMS.ic.transferC();
             _tc.church_name=fnn.GetIGridCellValue(fGrid,"church","desc");
             if(fGrid.Rows["church"].Cells["desc"].AuxValue!=null)
             {
                 xso.ic.cregion _r = (fGrid.Rows["church"].Cells["desc"].AuxValue as fnn.iGComboItemEX).Tag as xso.ic.cregion;
                 _tc.church_id = _r.item_id;
                 _tc.church_name = _r.item_name;
             }
             string[] _cols = new string[]
             {
              "mem_id",
              "cat_id",
              "reason",
              "comment",
              "transfer_date",
              "transfer_fs_id",
              "nchurch_name",
              "church_id",
              "edate",
              "pc_us_id",
              "exp_type",
              "lch_id",
              "lch_type_id"

             };
             object[] _row = new object[]
             {
                 _tc.mem_id= mem_obj.mem_id,
                  0,
                 _tc.reason=fnn.GetIGridCellValue(fGrid,"reason","desc"),
                 _tc.comment=fnn.GetIGridCellValue(fGrid,"comment","desc"),
                 _tc.transfer_date=Convert.ToDateTime(fGrid.Rows["transfer_date"].Cells["desc"].AuxValue),
                 _tc.transfer_fs_id=fn.GetFSID(_tc.transfer_date),
                 _tc.church_name,
                 _tc.church_id,
                 datam.CURR_DATE,
                 datam.PC_US_ID,
                 emm.export_type.insert.ToByte(),
                 _tc.lch_id= datam.LCH_ID,
                _tc.lch_type_id= datam.LCH_TYPE_ID

             };
             using (xing xd = new xing())
             {
                 _tc.un_id = xd.SingleInsertCommandInt("transfer_tb", _cols, _row);
                 xd.InsertUpdateDelete(string.Format("update member_tb set mem_status_type_id={0},{1},pc_us_id={2} where mem_id={3}", em.xmem_status.Transfered.ToByte(), dbm.ETS, datam.PC_US_ID, mem_obj.mem_id));
                 xd.CommitTransaction();
             }
             datam.DATA_TRANSFER.Add(_tc.un_id, _tc);
             mem_obj.mem_status_id = em.xmem_status.Transfered.ToByte();
             mem_obj.objTransfer = _tc;
             this.Close();
          }
    }
}
