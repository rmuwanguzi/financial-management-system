using MTOMS.ic;
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
    public partial class AccountsTransferMaker : DevComponents.DotNetBar.Office2007Form
    {
        public AccountsTransferMaker()
        {
            InitializeComponent();
        }
        ic.accountC m_account { get; set; }
        private iGDropDownList m_SourceAccounts = null;
        private iGDropDownList m_DestinationAccounts = null;
        private DateTime? m_SelectedDate { get; set; }
        private List<string> m_ChequeList = new List<string>();
        private void AccountsTransferMaker_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            //m_group_account = this.Tag as ic.accountC;
            //if (m_group_account == null)
            //{
            //    return;
            //    this.Close();
            //}
            fGrid.AfterCommitEdit += fGrid_AfterCommitEdit;
            backworker.RunWorkerAsync();
        }
        private void backworker_DoWork(object sender, DoWorkEventArgs e)
        {
            datam.SystemInitializer();
            using (var xd = new xing())
            {
                datam.InitAccount(xd);
            }
        }
        private void backworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            InitIgridColumns();
            LoadMainGrid();
            fGrid.Focus();
            fGrid.SetCurCell("transfer_date", 1);
        }
        private void FilterDestinationAccounts(ic.accountC _source)
        {

            if (m_DestinationAccounts == null || m_DestinationAccounts.Items.Count == 0 )
            {
                return;
            }
            if (_source == null)
            {
                foreach (var k in m_DestinationAccounts.Items.Cast<fnn.iGComboItemEX>())
                {
                    k.Visible = true;
                }
                return;
            }
            else
            {
                ic.accountC _base_parent = accn.GetAccountBaseParent(_source.account_id);
                if (_base_parent != null)
                {
                    switch(_base_parent.search_alias)
                    {
                        case "WITHDRAWN_CHEQUES":
                            {
                                foreach (var k in m_DestinationAccounts.Items.Cast<fnn.iGComboItemEX>())
                                {
                                    if (m_ChequeList.IndexOf(k.Value.ToString()) > -1)
                                    {
                                        k.Visible = true;
                                    }
                                    else
                                    {
                                        k.Visible = false;
                                    }
                                    if (k.Value.ToString() == _source.account_name)
                                    {
                                        k.Visible = false;
                                    }
                                }
                                break;
                            }
                        case "ACC_PAYABLE":
                            {
                                foreach (var k in m_DestinationAccounts.Items.Cast<fnn.iGComboItemEX>())
                                {
                                    if (m_ChequeList.IndexOf(k.Value.ToString()) > -1)
                                    {
                                        k.Visible = false;
                                    }
                                    else
                                    {
                                        k.Visible = true;
                                    }
                                    if (k.Value.ToString() == _source.account_name)
                                    {
                                        k.Visible = false;
                                    }
                                }
                                break;
                            }
                    }
                }
            }
            //


        }
        private void LoadMainGrid()
        {
            fGrid.BeginUpdate();
            iGRow _row = null;
            _row = fGrid.Rows.Add();
            _row.Height = 4;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            int group_index = 1;
            string group_name = "New Account Information";
            //
            var _date = new fnn.DropDownCalenderX();
            _date.start_date = new DateTime(2015, 1, 1);
           // _date.start_date = sdata.CURR_DATE;
            _date.end_date = sdata.CURR_DATE;
            _date.selected_date = sdata.CURR_DATE;
            //
            _row = CreateNewRow("Transfer Date", "transfer_date", typeof(string), group_index, group_name);
            _row.Cells["desc"].TypeFlags = iGCellTypeFlags.ComboPreferValue | iGCellTypeFlags.NoTextEdit | iGCellTypeFlags.HasEllipsisButton;
            _row.Cells["desc"].FormatString = "{0:D}";// {0:d} for short date string
            _row.Cells["desc"].DropDownControl = _date;
            _row.Cells["desc"].Value = null;
            //


            //
            _row = fGrid.Rows.Add();
            _row.Height = 4;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            //
            _row = CreateNewRow("From Account", "account_from", typeof(string), group_index, group_name);
            _row.Cells[1].ForeColor = Color.DarkBlue;
            _row.Cells[0].ForeColor = Color.Maroon;
            m_SourceAccounts = fnn.CreateCombo();
            m_SourceAccounts.MaxVisibleRowCount = 6;
            _row.Cells[1].DropDownControl = m_SourceAccounts;
            _row.Cells[1].Value = null;
            _row.Cells[1].Enabled = iGBool.False;
            //
            _row = CreateNewRow("Account Balance", "account_balance", typeof(decimal), group_index, group_name);
            _row.Cells[1].Enabled = iGBool.False;
            _row.Cells[0].ForeColor = Color.Gray;
            _row.Cells[1].FormatString = "{0:N0}";
            //
            _row = fGrid.Rows.Add();
            _row.Height = 4;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            _row.Height += 2;
            //
            _row = CreateNewRow("To Account", "account_to", typeof(string), group_index, group_name);
            _row.Cells[1].ForeColor = Color.DarkBlue;
            _row.Cells[0].ForeColor = Color.Green;
            m_DestinationAccounts = fnn.CreateCombo();
            m_DestinationAccounts.MaxVisibleRowCount = 6;
            _row.Cells[1].DropDownControl = m_DestinationAccounts;
            _row.Cells[1].Value = null;
            _row.Cells[1].Enabled = iGBool.False;
            //
            _row = CreateNewRow("Transfered Amount", "transfer_amount", typeof(decimal), group_index, group_name);
            _row.Cells[1].Enabled = iGBool.False;
            _row.Cells[1].FormatString = "{0:N0}";
            //
            _row = fGrid.Rows.Add();
            _row.Height = 4;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            _row.Height += 2;
            //
            _row = CreateNewRow("Transfer \n Reason", "reason", typeof(string), group_index, group_name);
            _row.Height = 60;
            _row.Cells["desc"].TextFormatFlags = iGStringFormatFlags.WordWrap;
            _row.Cells["name"].TextAlign = iGContentAlignment.MiddleRight;
            //
            _row = fGrid.Rows.Add();
            _row.Height = 3;
            _row.BackColor = Color.Lavender;
            _row.Selectable = false;
            //


            fGrid.EndUpdate();
        }
        private iGRow CreateNewRow(string field, string rowkey, Type _type, int group_index, string group_name)
        {
            var _row = fGrid.Rows.Add();
            _row.Font = new Font("georgia", 14, FontStyle.Regular);
            _row.Cells["desc"].Font = new Font("arial", 14, FontStyle.Regular);
            _row.Cells["name"].Col.Width = 180;
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
        private bool IsValid()
        {

            var _comp = new string[] { "account_from", "account_to","transfer_amount","reason" };
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
            string _str = "Are You Sure You Want To Perform This Operation ??";
            if (!dbm.WarningMessage(_str, "Save Warning"))
            {
                return;
            }
            ic.accountC _from = (fGrid.Rows["account_from"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag as ic.accountC;
            ic.accountC _to = (fGrid.Rows["account_to"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag as ic.accountC;
            //
            using (var xd = new xing())
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
                        "lch_id","transfer_reason","edate","pc_us_id","m_partition_id"
                    };
                var _ts_id = accn.AccountsTransaction(xd, string.Format("Being Transfer Of Account Balance from {0} To {1}", _from.account_name, _to.account_name), sdata.CURR_DATE);
                var _amount = fGrid.Rows["transfer_amount"].Cells[1].Value.ToInt32();
                var _ret_id = xd.SingleInsertCommandTSPInt("acc_cash_transfer_tb", _cols, new object[]
                        {
                            em.CashTransferSourceType.account,
                            _from.account_id,
                            _ts_id,
                            em.CashTransferDestinationType.account,
                            _to.account_id,
                            _amount,
                            m_SelectedDate,
                            fn.GetFSID(m_SelectedDate.Value),
                            0,
                            datam.LCH_ID,
                         fGrid.Rows["reason"].Cells[1].Text,
                         sdata.CURR_DATE,
                         sdata.PC_US_ID,
                         string.Format("{0}{1}",m_SelectedDate.Value.Year,m_SelectedDate.Value.Month).ToInt32()
                        });

                em.j_sectionS? source_j_section = null;
                em.j_sectionS? dest_j_section = null;
                int source_dr=0;
                int source_cr=0;
                int dest_dr=0;
                int dest_cr=0;
                switch(_from.account_dept_category)
                {
                     case em.account_d_categoryS.Liablity:
                        {
                            source_j_section = em.j_sectionS.creditor;
                            source_dr = _amount;
                            break;
                        }
                    case em.account_d_categoryS.Asset:
                        {
                            source_j_section=em.j_sectionS.asset;
                            source_cr = _amount;
                            break;
                        }
                    default:
                        {
                            MessageBox.Show("You Have Performed An Invalid Operation", "Invalid Operation");
                            return;
                        }
                }
                switch (_to.account_dept_category)
                {
                    case em.account_d_categoryS.Asset:
                        {
                            dest_j_section = em.j_sectionS.asset;
                            dest_dr = _amount;
                            break;
                        }
                    case em.account_d_categoryS.Liablity:
                        {
                            dest_j_section = em.j_sectionS.creditor;
                            dest_cr = _amount;
                            break;
                        }
                    case em.account_d_categoryS.PL:
                        {
                            dest_j_section= em.j_sectionS.income;
                            dest_cr = _amount;
                            break;
                        }
                }
                ///
                accn.JournalBook(xd, m_SelectedDate.Value, source_j_section.Value, _ts_id, _from.account_id, source_dr, source_cr);
                accn.JournalBook(xd, m_SelectedDate.Value, dest_j_section.Value, _ts_id, _to.account_id, dest_dr, dest_cr);
                //
                if (accn.GetAccountBaseParent(_from.account_id).search_alias == "WITHDRAWN_CHEQUES")
                {
                    //cheque stuff;
                    int wdr_id = xd.ExecuteScalarInt(string.Format("select wdr_id from acc_bank_withdraw_tb where sys_account_id={0}", _from.account_id));
                    if (wdr_id > 0)
                    {
                        xd.UpdateFsTimeStamp("acc_bank_withdraw_tb");
                      var _st = string.Format("update acc_bank_withdraw_tb set cheque_balance=(cheque_balance-{0}),{1},fs_time_stamp={2} where wdr_id={3}", _amount,
                           dbm.ETS, SQLH.UnixStamp, wdr_id);
                      xd.SingleUpdateCommand(_st);
                    }
                    if (accn.GetAccountBaseParent(_to.account_id).search_alias == "WITHDRAWN_CHEQUES")
                    {
                        xd.UpdateFsTimeStamp("acc_bank_withdraw_tb");
                        wdr_id = xd.ExecuteScalarInt(string.Format("select wdr_id from acc_bank_withdraw_tb where sys_account_id={0}", _to.account_id));
                        if (wdr_id > 0)
                        {
                            xd.UpdateFsTimeStamp("acc_bank_withdraw_tb");
                            var _st = string.Format("update acc_bank_withdraw_tb set cheque_balance=(cheque_balance+{0}),{1},fs_time_stamp={2} where wdr_id={3}", _amount,
                                 dbm.ETS, SQLH.UnixStamp, wdr_id);
                            xd.SingleUpdateCommand(_st);
                        }
                    }

                }
                xd.CommitTransaction();
            }
            
            ClearGrid();
            this.Tag = 1;
            this.Close();
                
        }
        private void ClearGrid()
        {
            foreach (var k in fGrid.Rows.Cast<iGRow>())
            {
                k.Cells[1].Value = null;
                k.Cells[1].AuxValue = null;
                k.Cells[1].Enabled = iGBool.False;
            }
            FilterDestinationAccounts(null);
            fGrid.Rows["transfer_date"].Cells[1].Enabled = iGBool.True;
            fGrid.Focus();
            fGrid.SetCurCell("transfer_date", 1);
        }
        private void buttoncancel_Click(object sender, EventArgs e)
        {
            ClearGrid();
        }
        private void fGrid_Click(object sender, EventArgs e)
        {

        }
        private void FillAccountsByDate(DateTime? start_date)
        {
            m_SourceAccounts.Items.Clear();
            m_DestinationAccounts.Items.Clear();
            using (var xd = new xing())
            {
                foreach (var _alias in new string[]
                    {
                        "LC_OFFERTORY_PAYABLE",
                        "DISTRICT_PAYABLE",
                        "CHURCH_SUBUNITS_PAYABLE",
                        "WITHDRAWN_CHEQUES"
                    })
                {
                    var clist = accn.GetChildAccounts(accn.GetAccountByAlias(_alias).account_id, em.account_typeS.ActualAccount);
                    m_ChequeList.Clear();
                    foreach (var k in clist)
                    {
                        switch(_alias)
                        {
                            case "LC_OFFERTORY_PAYABLE":
                            case "DISTRICT_PAYABLE":
                            case "CHURCH_SUBUNITS_PAYABLE":
                                {
                                    if (accn.GetFs_AccountBalance(xd, fn.GetFSID(start_date.Value), k.account_id) > 0)
                                    {
                                        m_SourceAccounts.Items.Add(new fnn.iGComboItemEX()
                                        {
                                            Value = k.account_name,
                                            Tag = k
                                        });
                                    }
                                    m_DestinationAccounts.Items.Add(new fnn.iGComboItemEX()
                                    {
                                        Value = k.account_name,
                                        Tag = k
                                    });
                                    break;
                                }
                            case "WITHDRAWN_CHEQUES":
                                {
                                    if (accn.GetFs_AccountBalance(xd, fn.GetFSID(sdata.CURR_DATE), k.account_id) > 0)
                                    {
                                        m_SourceAccounts.Items.Add(new fnn.iGComboItemEX()
                                        {
                                            Value = k.account_name,
                                            Tag = k
                                        });
                                    }
                                    m_DestinationAccounts.Items.Add(new fnn.iGComboItemEX()
                                    {
                                        Value = k.account_name,
                                        Tag = k
                                    });
                                    m_ChequeList.Add(k.account_name);
                                    break;
                                }
                        }
                       
                    }
                }
                ic.accountC _lcb = accn.GetAccountByAlias("LCB_CREDITOR_REDUCTION_INCOME");
                m_DestinationAccounts.Items.Add(new fnn.iGComboItemEX()
                {
                    Value = "LCB",
                    Tag = _lcb
                });
                xd.CommitTransaction();
            }
            
        }
        private void fGrid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
        {
             if (fGrid.Rows[e.RowIndex].Cells["desc"].DropDownControl != null && fGrid.Rows[e.RowIndex].Cells["desc"].AuxValue == null)
            {
                fGrid.Rows[e.RowIndex].Cells["desc"].Value = null;
                for (int y = (e.RowIndex +1); y < fGrid.Rows.Count; y++)
                {
                    fGrid.Rows[y].Cells["desc"].Value = null;
                    fGrid.Rows[y].Cells["desc"].AuxValue = null;
                    if (fGrid.Rows[y].Cells["desc"].Selectable==iGBool.True)
                    {
                        fGrid.Rows[y].Cells["desc"].Enabled = iGBool.False;
                    }
                }
                if (fGrid.Rows[e.RowIndex].Key == "account_from")
                {
                    FilterDestinationAccounts(null);
                }
                return;
            }
            switch(fGrid.Rows[e.RowIndex].Key)
            {
                case "transfer_date":
                    {
                        m_SelectedDate = System.Convert.ToDateTime(fGrid.Rows["transfer_date"].Cells[1].AuxValue);
                        for (int y = (e.RowIndex + 1); y < fGrid.Rows.Count; y++)
                        {
                            fGrid.Rows[y].Cells["desc"].Value = null;
                            fGrid.Rows[y].Cells["desc"].AuxValue = null;
                            if (fGrid.Rows[y].Cells["desc"].Selectable == iGBool.True)
                            {
                                fGrid.Rows[y].Cells["desc"].Enabled = iGBool.False;
                            }
                        }
                        FillAccountsByDate(m_SelectedDate);
                        fGrid.Rows["account_from"].Cells[1].Enabled = iGBool.True;
                        //
                        break;
                    }
                case "account_from":
                    {
                        ic.accountC _from = (fGrid.Rows["account_from"].Cells[1].AuxValue as fnn.iGComboItemEX).Tag as ic.accountC;
                        
                        for (int y = (e.RowIndex + 1); y < fGrid.Rows.Count; y++)
                        {
                            fGrid.Rows[y].Cells["desc"].Value = null;
                            fGrid.Rows[y].Cells["desc"].AuxValue = null;
                            if (fGrid.Rows[y].Cells["desc"].Selectable == iGBool.True)
                            {
                                fGrid.Rows[y].Cells["desc"].Enabled = iGBool.False;
                            }
                        }
                        using (var xd = new xing())
                        {
                            fGrid.Rows["account_balance"].Cells[1].Value = accn.GetFs_AccountBalance(xd, fn.GetFSID(m_SelectedDate.Value), _from.account_id);
                        }
                        fGrid.Rows["account_to"].Cells[1].Enabled = iGBool.True;
                        FilterDestinationAccounts(_from);
                        break;
                    }
                case "account_to":
                    {
                        for (int y = (e.RowIndex + 1); y < fGrid.Rows.Count; y++)
                        {
                            fGrid.Rows[y].Cells["desc"].Value = null;
                            fGrid.Rows[y].Cells["desc"].AuxValue = null;
                            if (fGrid.Rows[y].Cells["desc"].Selectable == iGBool.True)
                            {
                                fGrid.Rows[y].Cells["desc"].Enabled = iGBool.False;
                            }
                        }
                        fGrid.Rows["transfer_amount"].Cells[1].Enabled = iGBool.True;
                        fGrid.Rows["reason"].Cells[1].Enabled = iGBool.True;
                        break;
                    }
                case "transfer_amount":
                    {
                        if ((fGrid.Rows[e.RowIndex].Cells[1].Value.ToInt32() <= 0) | fGrid.Rows[e.RowIndex].Cells[1].Value.ToInt32() > fGrid.Rows["account_balance"].Cells[1].Value.ToInt32())
                        {
                            MessageBox.Show("You Have Entered An Invalid Amount", "Invalid Amount", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            fGrid.Focus();
                            fGrid.Rows[e.RowIndex].Cells[1].Value = null;
                            fGrid.SetCurCell(e.RowIndex, 1);
                            return;
                        }
                        break;
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
    }
}
