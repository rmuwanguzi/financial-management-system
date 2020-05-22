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
using SdaHelperManager.Security;
namespace MTOMS
{
    public partial class CreditorsManager : DevComponents.DotNetBar.Office2007Form
    {
        public CreditorsManager()
        {
            InitializeComponent();
        }
        SortedList<string, int> m_GroupTotal = null;
        bool FORM_LOADED = false;
        long m_LastStamp = 0;
        private void CreditorsManager_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
            this.VisibleChanged += new EventHandler(CreditorsManager_VisibleChanged);
            InitializeGridColumnMain();
            datam.ShowWaitForm();
            Application.DoEvents();
            datam.SecurityCheck();
            m_GroupTotal = new SortedList<string, int>();
            backworker.RunWorkerAsync();
        }
         void CreditorsManager_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (FORM_LOADED)
                {
                    CheckUpdates();
                }
            }
                   
        }
         private void backworker_DoWork(object sender, DoWorkEventArgs e)
        {
            datam.SystemInitializer();
            using (var xd = new xing())
            {
                datam.GetAccountsPayable(xd);
                m_LastStamp = wdata.TABLE_STAMP["accounts_current_balance_tb"];
                xd.CommitTransaction();
            }
        }
         private void backworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           // datam.GetAccounts();
          
            LoadGrid();
           
            FORM_LOADED = true;
            datam.HideWaitForm();
        }
         
         private void LoadGrid()
         {
             var nlist = from k in datam.DATA_ACCOUNTS_PAYABLE.Values
                         where k.balance !=0 & k.objAccount.account_type== em.account_typeS.ActualAccount
                         orderby k.balance descending
                         select k;
             iGRow _row = null;
             fGrid.BeginUpdate();
             ic.accountC _acc = null;
             foreach (var r in nlist)
             {
                 _row = fGrid.Rows.Add();
                 _row.Font = new Font("georgia", 12, FontStyle.Regular);
                 _row.ReadOnly = iGBool.True;
                 _acc = datam.DATA_ACCOUNTS[r.account_id];
                 if (r.account_id < 0)
                 {
                     _row.Cells["owner"].Value = "CUC";
                     _row.Cells["svalue"].Value = _row.Cells["owner"].Value;
                 }
                 _row.Cells["account"].Value = _acc.account_name;
                 _row.Cells["balance"].Value = r.balance;
                 _row.Cells["balance"].Font = new Font("verdana", 11, FontStyle.Regular);
                 _row.Cells["balance"].ForeColor = Color.DarkBlue;
                 _row.Cells["acc_category"].Value = datam.DATA_ACCOUNTS[_acc.p_account_id].account_name;
                // _row.Cells["owner"].Value = _acc.owner_name;
                 switch (_acc.owner_type)
                 {
                     case em.AccountOwnerTypeS.CUC:
                         {
                             _row.Cells["owner"].Value = "CUC";
                             _row.Cells["svalue"].Value = _row.Cells["owner"].Value;
                             _row.Cells["s_id"].Value = 1;
                             break;
                         }
                     case em.AccountOwnerTypeS.CHURCH:
                         {
                             _row.Cells["owner"].Value = "Church";
                             _row.Cells["svalue"].Value = _row.Cells["owner"].Value;
                             _row.Cells["s_id"].Value = 3;
                             break;
                         }
                     case em.AccountOwnerTypeS.CHURCH_GROUP:
                         {
                             try
                             {
                                 _row.Cells["owner"].Value = datam.DATA_CHURCH_GROUPS[_acc.owner_id].cg_name;
                             }
                             catch (Exception)
                             {
                                 _row.Cells["owner"].Value = "Church Group";
                             }
                             _row.Cells["svalue"].Value = _row.Cells["owner"].Value;
                             _row.Cells["s_id"].Value = 5;
                             break;
                         }
                     case em.AccountOwnerTypeS.CHURCH_MEMBER:
                         {
                             try
                             {
                                 _row.Cells["owner"].Value = datam.DATA_MEMBER[_acc.owner_id].mem_name;
                             }
                             catch (Exception)
                             {
                                 _row.Cells["owner"].Value = "Church Member";
                             }
                             _row.Cells["svalue"].Value = "Church Members";
                             _row.Cells["s_id"].Value = 7;
                             break;
                         }
                     case em.AccountOwnerTypeS.DEPARTMENT:
                         {
                             try
                             {
                                 _row.Cells["owner"].Value = datam.DATA_DEPARTMENT[_acc.owner_id].dept_name;
                                 if (_row.Cells["owner"].Text.IndexOf("department") == -1)
                                 {
                                     _row.Cells["owner"].Value = _row.Cells["owner"].Text + " Department";
                                 }
                             }
                             catch (Exception)
                             {
                                 _row.Cells["owner"].Value = "Department";
                             }
                             _row.Cells["svalue"].Value = _row.Cells["owner"].Value;
                             _row.Cells["s_id"].Value = 4;
                           
                             break;
                         }
                     case em.AccountOwnerTypeS.DISTRICT:
                         {
                             _row.Cells["owner"].Value = "District";
                             _row.Cells["svalue"].Value = _row.Cells["owner"].Value;
                             _row.Cells["s_id"].Value = 2;
                             break;
                         }
                     case em.AccountOwnerTypeS.OTHER:
                         {
                             _row.Cells["owner"].Value = _acc.owner_name;
                             _row.Cells["svalue"].Value = "Others";
                             _row.Cells["s_id"].Value = 6;
                             break;
                         }
                    
                 }
                 if (r.objAccount.owner_id > 0)
                 {
                     try
                     {
                         var _cg = datam.DATA_CHURCH_GROUPS[r.objAccount.owner_id];
                         if (_cg != null)
                         {
                             _row.Cells["account"].Value = string.Format("{0} :: {1}", _acc.account_name, _cg.cg_name);
                             _row.Cells["owner"].Value = _cg.cg_name;
                             _row.Cells["svalue"].Value = _row.Cells["owner"].Value;
                             _row.Cells["s_id"].Value = 5;
                         }
                     }
                     catch (Exception ex)
                     {

                     }
                 }
                 _row.AutoHeight();
                 _row.Key = string.Format("{0}{1}", r.account_id.ToString(), r.objAccount.owner_id);
                 _row.Tag = r;
                 r.is_updated = false;
             }
             ShowTotalCreditors();
             fGrid.Cols.AutoWidth();
             fGrid.AutoResizeCols = false;
             var Gtotal = from k in fGrid.Rows.Cast<iGRow>()
                          group k by k.Cells["svalue"].Text
                              into nw_gp
                              select new
                              {
                                  gp_name = nw_gp.Key,
                                  gp_total = nw_gp.Sum(p => p.Cells["balance"].Value.ToInt32())
                              };
             m_GroupTotal.Clear();
             foreach (var t in Gtotal)
             {
                 m_GroupTotal.Add(t.gp_name, t.gp_total);
             }
             if (fGrid.GroupObject.Count == 0)
             {
                 fGrid.GroupObject.Add("svalue");
             }
             fGrid.Group();
             fGrid.PerformAction(iGActions.CollapseAll);
             fGrid.EndUpdate();
         }
         private void CheckUpdates()
         {
             using (var _xd = new xing())
             {
                 datam.InitAccount(_xd);
                 _xd.CommitTransaction();
             }
             wdata.TABLE_STAMP["accounts_current_balance_tb"] = m_LastStamp;
             if (datam.GetAccountsPayable(null))
             {
                 m_LastStamp = wdata.TABLE_STAMP["accounts_current_balance_tb"];
                 var nlist = from k in datam.DATA_ACCOUNTS_PAYABLE.Values
                             where k.is_updated & k.objAccount.account_type==em.account_typeS.ActualAccount
                             select k;
                 if (nlist.Count() == 0) { return; }
                 var _selected_cell = fGrid.CurCell;
                 var ex_list = fGrid.Rows.Cast<iGRow>().Where(t => t.Type == iGRowType.AutoGroupRow & t.Expanded).Select(p => p.Key).ToList();
                 iGRow _row = null;
                 fGrid.BeginUpdate();
                 fGrid.GroupObject.Clear();
                 fGrid.Group();
                 ic.accountC _acc = null;
                 foreach (var r in nlist)
                 {
                     try
                     {
                         _row = fGrid.Rows[string.Format("{0}{1}", r.account_id.ToString(), r.objAccount.owner_id)];
                         _acc = datam.DATA_ACCOUNTS[r.account_id];
                         _row.Cells["balance"].Value = r.balance;
                     }
                     catch (Exception ex)
                     {
                         #region new_row
                         _row = fGrid.Rows.Add();
                         _row.Font = new Font("georgia", 12, FontStyle.Regular);
                         _row.ReadOnly = iGBool.True;
                         _acc = datam.DATA_ACCOUNTS[r.account_id];
                         if (r.account_id < 0)
                         {
                             _row.Cells["owner"].Value = "CUC";
                             _row.Cells["svalue"].Value = _row.Cells["owner"].Value;
                         }
                         _row.Cells["account"].Value = _acc.account_name;
                         _row.Cells["balance"].Value = r.balance;
                         _row.Cells["balance"].Font = new Font("verdana", 11, FontStyle.Regular);
                         _row.Cells["balance"].ForeColor = Color.DarkBlue;
                         _row.Cells["acc_category"].Value = datam.DATA_ACCOUNTS[_acc.p_account_id].account_name;
                         // _row.Cells["owner"].Value = _acc.owner_name;
                         switch (_acc.owner_type)
                         {
                             case em.AccountOwnerTypeS.CUC:
                                 {
                                     _row.Cells["owner"].Value = "CUC";
                                     _row.Cells["svalue"].Value = _row.Cells["owner"].Value;
                                     _row.Cells["s_id"].Value = 1;
                                     break;
                                 }
                             case em.AccountOwnerTypeS.CHURCH:
                                 {
                                     _row.Cells["owner"].Value = "Church";
                                     _row.Cells["svalue"].Value = _row.Cells["owner"].Value;
                                     _row.Cells["s_id"].Value = 3;
                                     break;
                                 }
                             case em.AccountOwnerTypeS.CHURCH_GROUP:
                                 {
                                     try
                                     {
                                         _row.Cells["owner"].Value = datam.DATA_CHURCH_GROUPS[_acc.owner_id].cg_name;
                                     }
                                     catch (Exception)
                                     {
                                         _row.Cells["owner"].Value = "Church Group";
                                     }
                                     _row.Cells["svalue"].Value = _row.Cells["owner"].Value;
                                     _row.Cells["s_id"].Value = 5;
                                     break;
                                 }
                             case em.AccountOwnerTypeS.CHURCH_MEMBER:
                                 {
                                     try
                                     {
                                         _row.Cells["owner"].Value = datam.DATA_MEMBER[_acc.owner_id].mem_name;
                                     }
                                     catch (Exception)
                                     {
                                         _row.Cells["owner"].Value = "Church Member";
                                     }
                                     _row.Cells["svalue"].Value = "Church Members";
                                     _row.Cells["s_id"].Value = 7;
                                     break;
                                 }
                             case em.AccountOwnerTypeS.DEPARTMENT:
                                 {
                                     try
                                     {
                                         _row.Cells["owner"].Value = datam.DATA_DEPARTMENT[_acc.owner_id].dept_name;
                                         if (_row.Cells["owner"].Text.IndexOf("department") == -1)
                                         {
                                             _row.Cells["owner"].Value = _row.Cells["owner"].Text + " Department";
                                         }
                                     }
                                     catch (Exception)
                                     {
                                         _row.Cells["owner"].Value = "Department";
                                     }
                                     _row.Cells["svalue"].Value = _row.Cells["owner"].Value;
                                     _row.Cells["s_id"].Value = 4;

                                     break;
                                 }
                             case em.AccountOwnerTypeS.DISTRICT:
                                 {
                                     _row.Cells["owner"].Value = "District";
                                     _row.Cells["svalue"].Value = _row.Cells["owner"].Value;
                                     _row.Cells["s_id"].Value = 2;
                                     break;
                                 }
                             case em.AccountOwnerTypeS.OTHER:
                                 {
                                     _row.Cells["owner"].Value = _acc.owner_name;
                                     _row.Cells["svalue"].Value = "Others";
                                     _row.Cells["s_id"].Value = 6;
                                     break;
                                 }
                             case em.AccountOwnerTypeS.CHURCH_GROUP_SHARED:
                                 {
                                     if (r.objAccount.owner_id == 0)
                                     {
                                         _row.Cells["owner"].Value = "Church";
                                         _row.Cells["s_id"].Value = 3;
                                     }
                                     else
                                     {
                                         try
                                         {
                                             var _cg = datam.DATA_CHURCH_GROUPS[r.objAccount.owner_id];
                                             if (_cg != null)
                                             {
                                                 _row.Cells["account"].Value = string.Format("{0} :: {1}", _acc.account_name, _cg.cg_name);
                                                 _row.Cells["owner"].Value = _cg.cg_name;
                                                 _row.Cells["s_id"].Value = 5;
                                             }

                                         }
                                         catch (Exception er)
                                         {

                                         }
                                     }
                                     _row.Cells["svalue"].Value = _row.Cells["owner"].Value;
                                     break;
                                 }
                         }
                         #endregion
                         _row.AutoHeight();
                         _row.Key = string.Format("{0}{1}", r.account_id.ToString(), r.objAccount.owner_id);
                         _row.Tag = r;
                     }
                     r.is_updated = false;
                 }
                 ShowTotalCreditors();
                 fGrid.Cols.AutoWidth();
                 fGrid.AutoResizeCols = false;
                 var Gtotal = from k in fGrid.Rows.Cast<iGRow>()
                              group k by k.Cells["svalue"].Text
                                  into nw_gp
                                  select new
                                  {
                                      gp_name = nw_gp.Key,
                                      gp_total = nw_gp.Sum(p => p.Cells["balance"].Value.ToInt32())
                                  };
                 m_GroupTotal.Clear();
                 foreach (var t in Gtotal)
                 {
                     m_GroupTotal.Add(t.gp_name, t.gp_total);
                 }
                 if (fGrid.GroupObject.Count == 0)
                 {
                     fGrid.GroupObject.Add("svalue");
                 }
                 fGrid.Group();
                 fGrid.PerformAction(iGActions.CollapseAll);
                 foreach (var y in ex_list)
                 {
                     try
                     {
                         fGrid.Rows[y].Expanded = true;
                     }
                     catch (Exception ex)
                     {
                      
                     }
                 }
                 fGrid.EndUpdate();
             }
         }
         private void ShowTotalCreditors()
         {
             var _totall = datam.DATA_ACCOUNTS_PAYABLE.Values.Where(f => f.objAccount.account_type == em.account_typeS.ActualAccount).Sum(k => k.balance);
             paneltotal.Text = "Total Credit Balance  = " + _totall.ToNumberDisplayFormat();
          
         }
        private void InitializeGridColumnMain()
        {
            #region Columns To Display
            List<iGrid> _grids = new List<iGrid>()
            {
                fGrid
            };
            Dictionary<string, string> grid_cols = new Dictionary<string, string>();
            grid_cols.Add("account", "Account Name");
            grid_cols.Add("balance", "Credit\nBalance");
            grid_cols.Add("acc_category", "Category");
            grid_cols.Add("owner", "Owner");
            iGCol myCol;
            foreach (var _grid in _grids)
            {

                foreach (var c in grid_cols)
                {
                    myCol = _grid.Cols.Add(c.Key, c.Value);
                    myCol.IncludeInSelect = true;
                    myCol.SortType = iGSortType.ByValue;
                    myCol.ShowWhenGrouped = true;
                    //  myCol.ColHdrStyle.BackColor = Color.AntiqueWhite;
                    myCol.ColHdrStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    myCol.ColHdrStyle.ForeColor = Color.DarkGreen;
                    myCol.ColHdrStyle.Font = new Font("georgia", 13, FontStyle.Regular);

                }
               
                 _grid.Cols["balance"].CellStyle.FormatString = "{0:N0}";
                 _grid.Cols["balance"].CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
                 _grid.Cols["balance"].CellStyle.BackColor = Color.WhiteSmoke;
                 _grid.Cols.Add("svalue", "svalue").Visible = false;
                 _grid.Cols.Add("s_id", "s_id").Visible = false;
                 _grid.Cols["s_id"].CellStyle.ValueType = typeof(int);
                _grid.DefaultRow.Height = _grid.GetPreferredRowHeight(true, false);
                _grid.DefaultAutoGroupRow.Height = _grid.DefaultRow.Height;
                _grid.Cols.AutoWidth();
            }

            #endregion
        }
       

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            CheckUpdates();
        }

        private void fGrid_MouseDown(object sender, MouseEventArgs e)
        {
            
            
        }

        private void fGrid_CellMouseDown(object sender, iGCellMouseDownEventArgs e)
        {
            if (fGrid.CurCell != fGrid.Cells[e.RowIndex, e.ColIndex])
            {
                fGrid.SetCurCell(e.RowIndex, e.ColIndex);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (fGrid.SelectedRows.Count == 0)
            {
                e.Cancel = true;
                return;
            }
            if(fGrid.SelectedRows[0].Type==iGRowType.AutoGroupRow)
            {
                e.Cancel = true;
                return;
            }
        }

        private void editMemberDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //using (var _fm = new CreditPaymentsMaker())
            //{
            //    _fm.Tag = fGrid.CurCell.Row.Tag;
            //    _fm.ShowDialog();

            //}
            CheckUpdates();
        }

        private void fGrid_AfterContentsSorted(object sender, EventArgs e)
        {
            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
        }

        private void fGrid_AfterAutoGroupRowCreated(object sender, iGAfterAutoGroupRowCreatedEventArgs e)
        {
            iGCell myGroupRowCell = fGrid.RowTextCol.Cells[e.AutoGroupRowIndex];
            myGroupRowCell.Row.AutoHeight();
            iGCell myFirstCellInGroup = fGrid.Cells[e.GroupedRowIndex, "svalue"];
            myGroupRowCell.Value = string.Format("{0}   ::  {1}", myFirstCellInGroup.Value.ToProperCase(), m_GroupTotal[myFirstCellInGroup.Text].ToNumberDisplayFormat());
            myGroupRowCell.Row.Key = myFirstCellInGroup.Text;
            myGroupRowCell.Row.RowTextCell.Row.Key = myFirstCellInGroup.Text;
            myGroupRowCell.Row.TextAlign = iGContentAlignment.MiddleLeft;
          
           
        }
    }
}
