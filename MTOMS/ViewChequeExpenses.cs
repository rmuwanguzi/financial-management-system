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
    public partial class ViewChequeExpenses : DevComponents.DotNetBar.Office2007Form
    {
        public ViewChequeExpenses()
        {
            InitializeComponent();
        }
        ic.bankWithDrawC m_WithDraw { get; set; }
        List<ic.expense_transC> m_Expenses { get; set; }
        private enum _process
        {
            _formload=0,
            checkforupdates=1
        }
        _process m_process = _process._formload;
        private void InitializeGridColumnMain()
        {
            #region Columns To Display
            List<iGrid> _grids = new List<iGrid>()
            {
                fGrid
            };
            Dictionary<string, string> grid_cols = new Dictionary<string, string>();
            grid_cols.Add("sys_no", "Sys-No");
            grid_cols.Add("date", "Expense\nDate");
            grid_cols.Add("voucher_no", "Voucher No");
            grid_cols.Add("expense", "Expense Type");
            grid_cols.Add("expense_amount", "Amount");
            grid_cols.Add("received_by", "Received By");
            grid_cols.Add("details", "details");
            grid_cols.Add("pay_mode", "Pay Mode");
            grid_cols.Add("dept", "Department");
            grid_cols.Add("source", "Source");

            iGCol myCol;
            foreach (var _grid in _grids)
            {
                _grid.BeginUpdate();
                foreach (var c in grid_cols)
                {
                    myCol = _grid.Cols.Add(c.Key, c.Value);
                    myCol.IncludeInSelect = true;
                    myCol.SortType = iGSortType.None;
                    myCol.ColHdrStyle.TextAlign = iGContentAlignment.MiddleCenter;
                    myCol.ColHdrStyle.ForeColor = Color.DarkGreen;
                    myCol.ColHdrStyle.Font = new Font("georgia", 13, FontStyle.Regular);

                }
                _grid.Cols.Add("fs_id", "fs_id").Visible = false;
                _grid.Cols["sys_no"].ColHdrStyle.ForeColor = Color.Maroon;
                _grid.Cols["sys_no"].CellStyle.ForeColor = Color.DarkGray;
                _grid.Cols["sys_no"].Visible = false;
                _grid.Cols["expense_amount"].CellStyle.FormatString = "{0:N0}";
                _grid.Cols["source"].Visible = false;
                foreach(var _t in new string[] { "voucher_no","expense","expense_amount","details","pay_mode","received_by"})
                {
                    _grid.Cols[_t].SortType = iGSortType.ByValue;
                }
                _grid.DefaultRow.Height = _grid.GetPreferredRowHeight(true, false);
                _grid.DefaultAutoGroupRow.Height = _grid.DefaultRow.Height;
                _grid.Cols.AutoWidth();
                _grid.EndUpdate();
            }

            #endregion
        }
        private void DisplayText()
        {
            label1.Text = string.Format("Cheque No :: {0}     WithDraw Date :: {1}    Cheque Balance :: {2}", string.Format("{0} {1}", m_WithDraw.cheque_no, m_WithDraw.cheque_alias), m_WithDraw.fs_date.ToMyShortDate(), m_WithDraw.cheque_balance.ToNumberDisplayFormat());
        }
        private void ViewChequeExpenses_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.WhiteSmoke;
            CenterToScreen();
            datam.SecurityCheck();
            m_WithDraw = this.Tag as ic.bankWithDrawC;
            if (m_WithDraw == null)
            {
                this.Close();
                return;
            }
            InitializeGridColumnMain();
            datam.ShowWaitForm("Loading Data, Please Wait...");
            backworker.RunWorkerAsync();
        }
        private void backworker_DoWork(object sender, DoWorkEventArgs e)
        {
            datam.SystemInitializer();
            using(var xd= new xing())
            {
                datam.InitExpenses(xd);
                datam.GetBankAccounts(xd);
                datam.UpdateWithDrawnCheque(m_WithDraw);
                m_Expenses = datam.GetChequeExpenses(xd, m_WithDraw.wdr_id);
                xd.CommitTransaction();
            }
        }
        private void backworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoadMainGrid(m_Expenses);
            buttonAdd.Enabled = true;
            DisplayText();
            datam.HideWaitForm();
        }
        public void CheckForUpdates()
        {
            backworker.RunWorkerAsync();
           
        }
        private void LoadMainGrid(IEnumerable<ic.expense_transC> _list)
        {
            fGrid.BeginUpdate();
            fGrid.Rows.Clear();
            if (_list == null) { fGrid.EndUpdate(); return; }
            iGRow _row = null;
            foreach (var n in _list)
            {
                _row = fGrid.Rows.Add();
                _row.ReadOnly = iGBool.True;
                _row.Font = new Font("georgia", 12, FontStyle.Regular);
               // _row.Cells["sys_no"].Value = "V-" + n.voucher_id;
                _row.Cells["date"].Value = n.exp_date.Value.ToMyShortDate();
                _row.Cells["fs_id"].Value = fn.GetFSID(n.exp_date.Value);
                _row.Cells["voucher_no"].Value = n.voucher_no;
                _row.Cells["expense_amount"].Value = n.exp_amount;
                _row.Cells["expense_amount"].TextAlign = iGContentAlignment.MiddleCenter;
                _row.Cells["expense_amount"].ForeColor = Color.DarkBlue;
                _row.Cells["expense"].Value = n.objExpenseAccount.exp_acc_name;
                _row.Cells["details"].Value = n.exp_details;
                _row.Cells["dept"].Value = datam.DATA_DEPARTMENT[n.dept_id].dept_name;
                _row.Cells["pay_mode"].Value = fnn.GetExpensePayModeString(n.pay_mode);
                switch (n.source_type)
                {
                    case em.exp_inc_src_typeS.bank:
                        {
                            _row.Cells["source"].Value = "Bank";
                            break;
                        }
                    case em.exp_inc_src_typeS.petty_cash_cheque:
                        {
                            _row.Cells["source"].Value = "Petty Cash";
                            break;
                        }
                    case em.exp_inc_src_typeS.unbanked_cash:
                        {
                            _row.Cells["source"].Value = "Source";
                            _row.ForeColor = Color.Red;
                            break;
                        }
                }
                _row.Cells["received_by"].Value = n.received_by;
                _row.Key = n.un_id.ToStringNullable();
                _row.Tag = n;
                _row.AutoHeight();

                n.is_updated = false;
            }
            
                using (var xd = new xing())
                {
                    string _str = string.Format("select * from acc_cash_transfer_tb where (source_id={0} or destination_id={0}) and status={1} and source_type_id={2} and destination_type_id={3}",
                        m_WithDraw.sys_account_id, em.cashTransferStatus.valid.ToInt16(), em.CashTransferSourceType.account.ToInt16(),em.CashTransferDestinationType.account.ToInt16());
                    using (var _dr = xd.SelectCommand(_str))
                    {
                        while (_dr.Read())
                        {
                            _row = fGrid.Rows.Add();
                            _row.ReadOnly = iGBool.True;
                            _row.Font = new Font("georgia", 12, FontStyle.Regular);
                            _row.ForeColor = _dr["source_id"].ToInt32() == m_WithDraw.sys_account_id ? Color.Maroon : Color.Green;
                            _row.Cells["date"].Value = _dr.GetDateTime("fs_date").ToMyShortDate();
                            _row.Cells["fs_id"].Value = fn.GetFSID(_dr.GetDateTime("fs_date"));
                            _row.Cells["expense_amount"].Value = _dr["amount"].ToInt32();
                            _row.Cells["expense_amount"].TextAlign = iGContentAlignment.MiddleCenter;
                            _row.Cells["expense_amount"].ForeColor =  _row.ForeColor;
                            _row.Cells["expense"].Value = "TRANSFER";
                            _row.Cells["expense"].TextAlign = iGContentAlignment.MiddleCenter;
                            if (_dr["source_id"].ToInt32() == m_WithDraw.sys_account_id)
                            {
                                _row.Cells["details"].Value = string.Format(" Cash Transfer To {0}", datam.DATA_ACCOUNTS[_dr["destination_id"].ToInt32()].account_name);
                            }
                            else
                            {
                                _row.Cells["expense"].Value = "Extra INCOME";
                                _row.Cells["expense_amount"].ForeColor = Color.Green;
                                _row.Cells["details"].Value = string.Format("Cash Received From {0}", datam.DATA_ACCOUNTS[_dr["source_id"].ToInt32()].account_name);
                                _row.Cells["details"].AuxValue = 1;
                            }
                            _row.AutoHeight();
                        }
                    }
                    xd.CommitTransaction();
                }
            
            fGrid.Cols.AutoWidth();
            fGrid.AutoResizeCols = false;
            fGrid.EndUpdate();
          
        }
        private void buttonItem1_Click(object sender, EventArgs e)
        {
            using(var _fm= new MakeCheckExpense())
            {
                _fm.Tag = m_WithDraw;
                _fm.Owner = this;
                _fm.ShowDialog();
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
          

            string _str = "Are You Sure You Want To Delete This Voucher";
            if (!dbm.WarningMessage(_str, "Delete Voucher Warning"))
            {
                return;
            }
            var _obj = fGrid.SelectedRows[0].Tag as ic.expense_transC;
            
            var m_partition = string.Format("{0}{1}", _obj.exp_date.Value.Year, _obj.exp_date.Value.Month).ToInt32();
            datam.GetMonthExpenses(m_partition);
            var nlist = from k in datam.DATA_MONTH_EXPENSES[m_partition].Values
                        where k.voucher_id == _obj.voucher_id & k.voucher_status == em.voucher_statusS.valid
                        select k;
            
            if (_obj != null)
            {

                using (var xd = new xing())
                {
                    
                    long _trans_id = xd.ExecuteScalarInt64(string.Format("select transaction_id from acc_expense_trans_tb where un_id={0} and voucher_status={1}", _obj.un_id, em.voucher_statusS.valid.ToByte()));
                    if (_trans_id > 0)
                    {
                        _str = string.Format("update acc_expense_vouchers_tb set status={0},{1},fs_time_stamp={2} where voucher_id={3} and status={4}", em.voucher_statusS.cancelled.ToByte(), dbm.ETS, SQLH.UnixStamp, _obj.voucher_id, em.voucher_statusS.valid.ToByte());
                        xd.UpdateFsTimeStamp("acc_expense_vouchers_tb");
                        if (xd.SingleUpdateCommand(_str))
                        {
                            _str = string.Format("select * from journal_tb where transaction_id in ({0})",
                                string.Format("select transaction_id from acc_expense_trans_tb where un_id={0} and voucher_status={1}", _obj.un_id, em.voucher_statusS.valid.ToByte()));
                            accn.DeleteJournal(_str, xd);
                            //

                            foreach (var _expense in nlist)
                            {
                                _expense.voucher_status = em.voucher_statusS.cancelled;
                                xd.SingleUpdateCommandALL("acc_expense_trans_tb", new string[]
                     {
                         "voucher_status",
                         "un_id"
                     }, new object[]
                     {
                         em.voucher_statusS.cancelled.ToByte(),
                         _expense.un_id
                     }, 1);
                                //
                                xd.SingleUpdateCommandALL("acc_expense_trans_child_tb", new string[]
                     {
                         "voucher_status",
                         "trans_id"
                     }, new object[]
                     {
                         em.voucher_statusS.cancelled.ToByte(),
                         _expense.un_id
                     }, 1);
                                if (datam.DATA_EXPENSE_ACCOUNTS[_expense.exp_acc_id].exp_acc_type == em.exp_acc_typeS.system_offertory_payment |
                                    datam.DATA_EXPENSE_ACCOUNTS[_expense.exp_acc_id].exp_acc_type == em.exp_acc_typeS.trust_fund)
                                {
                                    accn.DeleteExpenseAccountsPayable(xd, _trans_id);
                                }
                                if (!string.IsNullOrEmpty(_expense.w_dr_data))
                                {
                                    var _cheques = _expense.w_dr_data.Split(new char[] { ',' });
                                    xd.UpdateFsTimeStamp("acc_bank_withdraw_tb");
                                    string _st = null;
                                    foreach (var t in _cheques)
                                    {
                                        var _val = t.Split(new char[] { ':' });
                                        if (xd.ExecuteScalarInt(string.Format("select transfer_id from acc_bank_withdraw_tb where wdr_id={0}", _val[0].ToInt32()))>0)
                                        {
                                            MessageBox.Show("You Have Already Closed This Cheque, This Operation Cannot Be Carried Out", "Closed Cheque Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            xd.RollBackTransaction();
                                            return;
                                        }
                                        _st = string.Format("update acc_bank_withdraw_tb set cheque_balance=(cheque_balance+{0}),{1},fs_time_stamp={2} where wdr_id={3}", _val[1],
                                         dbm.ETS, SQLH.UnixStamp, _val[0]);
                                        xd.InsertUpdateDelete(_st);
                                    }
                                }

                                //
                                fnn.ExecuteDeleteBase(xd, new ic.deleteBaseC()
                                {
                                    del_fs_date = sdata.CURR_DATE,
                                    del_fs_id = sdata.CURR_FS.fs_id,
                                    del_fs_time = DateTime.Now.ToShortTimeString(),
                                    del_pc_us_id = sdata.PC_US_ID
                                }, "acc_expense_trans_tb", "un_id", _expense.un_id);
                                //
                                if (!string.IsNullOrEmpty(_expense.cheque_no))
                                {
                                    ic.bankAccountC _bank = datam.DATA_BANK_ACCOUNTS.Values.Where(y => y.sys_account_id == _expense.source_account_id).FirstOrDefault();
                                    xd.SingleUpdateCommandALL("acc_usedbank_cheques_tb", new string[]
                           {
                               "status",
                               "bank_account_id",
                               "cheque_no"
                           }, new object[]
                           {
                               1,
                               _bank.un_id,
                               _expense.cheque_no
                           }, 2);

                                }
                                fGrid.Rows.RemoveAt(fGrid.Rows[_expense.un_id.ToString()].Index);
                            }

                            xd.CommitTransaction();
                        }
                    }
                    else
                    {
                        MessageBox.Show("This Expense Record Has Already Been Deleted", "Delete Failure");
                    }

                }
                CheckForUpdates();
            }
        }

        private void contextpayments_Opening(object sender, CancelEventArgs e)
        {
            if (fGrid.SelectedRows.Count == 0)
            {
                e.Cancel = true;
                return;
            }
            if (fGrid.SelectedRows[0].Tag==null)
            {
                e.Cancel = true;
                return;
            }
            if (Owner is AccountStatementForm | m_WithDraw.transfer_id > 0)
            {
                toolStripMenuItem3.Visible = false;
                return;
            }
        }

        private void printReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string _str = "Are You Sure You Want To Print This Report";
            if (!dbm.WarningMessage(_str, "Print Warning"))
            {
                return;
            }

            fnn.PrintChequeExpenseReport(fGrid, string.Empty, null, m_WithDraw);
        }
    }
}
