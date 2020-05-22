using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SdaHelperManager;
using TenTec.Windows.iGridLib;
using SdaHelperManager.Security;
namespace MTOMS
{
    public partial class PendingChequesManager : DevComponents.DotNetBar.Office2007Form
    {
        public PendingChequesManager()
        {
            InitializeComponent();
        }
        private ic.bankWithDrawC m_widthDraw = null;
        bool FORM_LOADED = false;
        private long m_stamp = 0;
        private void InitializeGridColumnMain()
        {
            #region Columns To Display
            List<iGrid> _grids = new List<iGrid>()
            {
                fGrid
            };
            Dictionary<string, string> grid_cols = new Dictionary<string, string>();
            grid_cols.Add("date", "WithDraw\nDate");
            grid_cols.Add("cheque_no", "Cheque No");
            grid_cols.Add("alias", "Purpose");
            grid_cols.Add("cheque_amount", "WithDrawn\nAmount");
            grid_cols.Add("spent_amount", "Spent\nAmount");
            grid_cols.Add("balance", "Cheque\nBalance");
           
            grid_cols.Add("colsearch", "ColSearch");
            iGCol myCol;
            foreach (var _grid in _grids)
            {

                foreach (var c in grid_cols)
                {
                    myCol = _grid.Cols.Add(c.Key, c.Value);
                    myCol.IncludeInSelect = true;
                    myCol.SortType = iGSortType.None;
                    //  myCol.ColHdrStyle.BackColor = Color.AntiqueWhite;
                    myCol.ColHdrStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    myCol.ColHdrStyle.ForeColor = Color.DarkGreen;
                    myCol.ColHdrStyle.Font = new Font("georgia", 13, FontStyle.Regular);
                   
                }
                _grid.Cols["colsearch"].Visible = false;
            
                _grid.Cols["date"].Visible = true;
                _grid.Cols["cheque_no"].CellStyle.ForeColor = Color.Maroon;
                _grid.Cols["cheque_no"].CellStyle.TextAlign = iGContentAlignment.MiddleCenter;
                foreach (var k in new string[]
                {
                    "spent_amount"
                }
                )
                {
                    _grid.Cols[k].CellStyle.ForeColor = Color.Gray;
                }
              
                _grid.DefaultRow.Height = _grid.GetPreferredRowHeight(true, false);
                _grid.DefaultAutoGroupRow.Height = _grid.DefaultRow.Height;
                _grid.Cols.AutoWidth();
            }

            #endregion  
        }
        public void CheckForUpdates()
        {
            Application.DoEvents();
            using(var xd=new xing())
            {
                datam.InitExpenses(xd);
                xd.CommitTransaction();
            }
            datam.GetPendingWithDrawnCheques();
            if (fGrid.Rows.Count > 0)
            {
                if (datam.DATA_PENDING_WITHDRAWN_CHEQUES.Values.Count(j => j.is_updated) == 0)
                {
                    return;
                }
            }
            UpdateGrid();
            var _sum = (from k in datam.DATA_PENDING_WITHDRAWN_CHEQUES.Values
                        where k.status == em.account_trans_Status.Normal & k.cheque_balance > 0
                        select k.cheque_balance).Sum();
            labelX1.Text = string.Empty;
            labelX1.Text = string.Format("Total :: {0}", _sum.ToNumberDisplayFormat());
              
     }
        private void FinanceChargesManager_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
            this.VisibleChanged += new EventHandler(FinanceChargesManager_VisibleChanged);
            datam.SecurityCheck();
            fGrid.CurRowChanged += new EventHandler(fGrid_CurRowChanged);
            textBoxX1.KeyDown+=new KeyEventHandler(textBoxX1_KeyDown);
            textBoxX1.TextChanged+=new EventHandler(textBoxX1_TextChanged);
            backworker.RunWorkerAsync();
        }
        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {

            MemSearchFilter();
            if (fGrid.Rows.Count == 0)
            {
                fGrid.CurRow = null;
            }
            else
            {
                MoveDown(-1);
            }
        }
        private void MoveUp(int _index)
        {
            int _val = _index;
            _val--;
            while (_val > -1)
            {
                if (fGrid.Rows[_val].Visible)
                {
                    fGrid.CurCell = fGrid.Rows[_val].Cells[0];
                    break;
                }
                _val--;
            }
        }
        private void MoveDown(int _index)
        {
            int _val = _index;
            _val++;
            while (_val < fGrid.Rows.Count)
            {
                if (fGrid.Rows[_val].Visible)
                {
                    fGrid.CurCell = null;
                    fGrid.CurCell = fGrid.Rows[_val].Cells[0];
                    break;
                }
                _val++;
            }
        }
        private void MemSearchFilter()
        {

            string _ret = textBoxX1.Text.ToLower();
            fGrid.BeginUpdate();

            try
            {
                for (int i = fGrid.Rows.Count - 1; i >= 0; i--)
                {
                    if (fGrid.Rows[i].Type != iGRowType.Normal) { continue; }
                    if (((string)fGrid.Cells[i, "colsearch"].Value).IndexOf(_ret) < 0)
                        fGrid.Rows[i].Visible = false;
                    else
                        fGrid.Rows[i].Visible = true;
                }
            }
            finally
            {
                fGrid.EndUpdate();
            }
            
        }
        private void textBoxX1_KeyDown(object sender, KeyEventArgs e)
        {
            if (fGrid.CurRow == null) { return; }
            switch (e.KeyCode)
            {
                case (Keys.Up):
                    {
                        MoveUp(fGrid.CurRow.Index);
                        e.Handled = true;
                        break;
                    }
                case (Keys.Down):
                    {
                        MoveDown(fGrid.CurRow.Index);
                        e.Handled = true;
                        break;
                    }
                case Keys.Enter:
                    {
                        if (fGrid.CurRow != null)
                        {
                            var _prev_key = fGrid.CurRow.Key;
                            textBoxX1.Clear();
                            fGrid.SetCurCell(_prev_key, 0);
                         }
                        break;
                    }

            }
        }
        void FinanceChargesManager_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible &FORM_LOADED)
            {
                CheckForUpdates();
               
            }
            
            if (!this.Visible)
            {
                timer1.Enabled = false;
            }
        }
        void UpdateGrid()
        {
            IEnumerable<ic.bankWithDrawC> _withdraws = null;
            bool load_all = false;
            if (fGrid.Rows.Count == 0)
            {
                _withdraws = from n in datam.DATA_PENDING_WITHDRAWN_CHEQUES.Values
                             where n.status==em.account_trans_Status.Normal & n.cheque_balance>0
                             orderby n.fs_id descending
                             select n;
                load_all = true;
            }
            else
            {
                _withdraws = from n in datam.DATA_PENDING_WITHDRAWN_CHEQUES.Values
                             where n.is_updated
                             orderby n.fs_id descending
                             select n;
               
            }
            fGrid.Enabled = false;
            fGrid.BeginUpdate();
            iGRow _row = null;
            List<string> To_Delete = new List<string>();
            bool _update = false;
            foreach (var n in _withdraws)
            {
                _update = false;
                n.is_updated = false;
               
                if (load_all)
                {
                    _row = fGrid.Rows.Add();
                }
                else
                {
                    try
                    {
                        _row = fGrid.Rows[n.wdr_id.ToString()];
                        _update = true;
                        if (n.status== em.account_trans_Status.Cancelled)
                        {
                                                     
                            if (To_Delete.IndexOf(n.wdr_id.ToString()) == -1)
                            {
                                To_Delete.Add(n.wdr_id.ToString());
                            }
                            continue;
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        if (n.status == em.account_trans_Status.Cancelled)
                        {
                            continue;
                        }
                        _row = fGrid.Rows.Add();
                        _update = false;
                    }
                }
                if (!_update)
                {
                    _row.Key = n.wdr_id.ToStringNullable();
                    _row.Font = new Font("georgia", 12, FontStyle.Regular);
                    _row.ReadOnly = iGBool.True;
                    _row.Tag = n;
                }
                _row.Cells["date"].Value = n.fs_date.ToMyShortDate();
                _row.Cells["cheque_no"].Value = n.cheque_no;
                _row.Cells["alias"].Value = n.cheque_alias;
                _row.Cells["cheque_amount"].Value = n.WithDrawnAmount;
                _row.Cells["cheque_amount"].FormatString = "{0:N0}";
                _row.Cells["spent_amount"].Value = (n.WithDrawnAmount - n.cheque_balance);
                _row.Cells["spent_amount"].FormatString = "{0:N0}";
                _row.Cells["balance"].Value = n.cheque_balance;
                _row.Cells["balance"].FormatString = "{0:N0}";
               ///
                _row.Cells["colsearch"].Value = string.Format("{0}", _row.Cells["cheque_no"].Text.ToString()).ToLower();
                _row.AutoHeight();
                _row.Height += 2;
           }
            if (To_Delete.Count > 0)
            {
                foreach (var k in To_Delete)
                {
                    fGrid.Rows.RemoveAt(fGrid.Rows[k].Index);
                }
            }
            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
            fGrid.EndUpdate();
            fGrid.Enabled = true;
            if (!timer1.Enabled) { timer1.Enabled = true; }
        }
        void fGrid_CurRowChanged(object sender, EventArgs e)
        {
            if (fGrid.CurRow == null)
            {
              return;
            }
         }
        private void backworker_DoWork(object sender, DoWorkEventArgs e)
        {
            datam.SystemInitializer();
          
        }
        private void backworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            datam.ShowWaitForm();
            Application.DoEvents();
             InitializeGridColumnMain();
             timer1.Enabled = true;
             CheckForUpdates();
             buttonAdd.Enabled = true;
             if (fGrid.Rows.Count == 0)
             {
                 UpdateGrid();
             }
             FORM_LOADED = true;
             datam.HideWaitForm();
        }
        private void fGrid_SizeChanged(object sender, EventArgs e)
        {
            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            CheckForUpdates();
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
           

        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
         {
             
             buttonAdd.PerformClick();
         }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
         {
             if (fGrid.SelectedRows.Count == 0)
             {
                 e.Cancel = true; return;
             }
           
           
             
         }
        
        private void postToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(var _fm = new ViewChequeExpenses())
            {
                _fm.Tag = fGrid.SelectedRows[0].Tag;
                _fm.Owner = this;
                _fm.ShowDialog();
            }
            CheckForUpdates();
        }

        private void makePaymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
          
        }

        private void fGrid_CellMouseDown(object sender, iGCellMouseDownEventArgs e)
        {
            fGrid.SetCurCell(e.RowIndex, e.ColIndex);
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            using (var _fm = new BankWithDraw())
            {
                _fm.Owner = this;
                _fm.ShowDialog();
            }

        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Application.DoEvents();
            CheckForUpdates();
            timer1.Enabled = true;
        }

        private void transferChequeBalanceToUnBankedCashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            ic.bankWithDrawC _obj = fGrid.SelectedRows[0].Tag as ic.bankWithDrawC;
            string _str = string.Format("Are You Sure You Want To Transfer The Cheque Balance Of {0} To UNBANKED Account ??", _obj.cheque_balance.ToNumberDisplayFormat());
            if(!dbm.WarningMessage(_str,"Transfer Warrning"))
            {
                return;
            }
            _str = string.Format("update acc_bank_withdraw_tb set cheque_balance=(cheque_balance-{0}),{1},fs_time_stamp={2} where wdr_id={3} and cheque_balance={4} and transfer_id=0", _obj.cheque_balance,
                         dbm.ETS, SQLH.UnixStamp, _obj.wdr_id, _obj.cheque_balance);
            using (var xd = new xing())
            {
                xd.UpdateFsTimeStamp("acc_bank_withdraw_tb");
                if (!xd.SingleUpdateCommand(_str))
                {
                    MessageBox.Show("You Need To ReEnter This Entry To Prevent Double Expense Entry", "Multi User Double Entry Error");
                    xd.RollBackTransaction();
                    return;
                }
                else
                {
                    string[] _cols = new string[]
                    {
                        "source_type_id",
                        "source_id",
                        "transaction_id",
                        "destination_type_id",
                        "destination_id",
                        "amount",
                        "fs_date",
                        "fs_id",
                        "fs_time_stamp",
                        "lch_id"
                    };
                    var _ts_id = accn.AccountsTransaction(xd, string.Format("Being Transfer Of Cheque Balance for {0} To UnBanked Cash", _obj.cheque_no), sdata.CURR_DATE);
                   var _ret_id= xd.SingleInsertCommandTSPInt("acc_cash_transfer_tb", _cols, new object[]
                        {
                            em.CashTransferSourceType.expense_cheque.ToInt16(),
                            _obj.wdr_id,
                            _ts_id,
                            em.CashTransferDestinationType.un_banked_cash_account,
                            accn.GetAccountByAlias("UNBANKED_CASH").account_id,
                            _obj.cheque_balance,
                            sdata.CURR_DATE,
                            sdata.CURR_FS.fs_id,
                            0,
                            datam.LCH_ID
                        });
                   xd.SingleUpdateCommandALL("acc_bank_withdraw_tb", new string[]
                        {
                          "transfer_id",
                          "wdr_id"
                        }, new object[]
                        {
                            _ret_id,
                            _obj.wdr_id
                        }, 1);
                    //
                   accn.JournalBook(xd, sdata.CURR_DATE, em.j_sectionS.cash, _ts_id, accn.GetAccountByAlias("CASH_ACCOUNT").account_id, 0, _obj.cheque_balance);
                   accn.JournalBook(xd, sdata.CURR_DATE, em.j_sectionS.cash, _ts_id, accn.GetAccountByAlias("UNBANKED_CASH").account_id, _obj.cheque_balance, 0);

                }
                xd.CommitTransaction();
            }
            //
           
            Application.DoEvents();
            CheckForUpdates();
            timer1.Enabled = true;

        }

       
              
    }
}
