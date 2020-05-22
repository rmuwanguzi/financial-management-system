namespace MTOMS
{
    partial class ViewChequeExpenses
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonAdd = new DevComponents.DotNetBar.ButtonX();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.fGrid = new TenTec.Windows.iGridLib.iGrid();
            this.contextpayments = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.printReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.backworker = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).BeginInit();
            this.contextpayments.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1225, 44);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(89, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(918, 31);
            this.label1.TabIndex = 2;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonAdd
            // 
            this.buttonAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonAdd.AutoExpandOnClick = true;
            this.buttonAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonAdd.Enabled = false;
            this.buttonAdd.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdd.ForeColor = System.Drawing.Color.DarkGreen;
            this.buttonAdd.Location = new System.Drawing.Point(4, 4);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(79, 36);
            this.buttonAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonAdd.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1,
            this.buttonItem3});
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Tasks";
            // 
            // buttonItem1
            // 
            this.buttonItem1.GlobalItem = false;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "Make Expense";
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // buttonItem3
            // 
            this.buttonItem3.GlobalItem = false;
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.Text = "Refresh";
            // 
            // fGrid
            // 
            this.fGrid.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.fGrid.AutoHeightRowMode = TenTec.Windows.iGridLib.iGAutoHeightRowMode.Cells;
            this.fGrid.AutoResizeCols = true;
            this.fGrid.BorderStyle = TenTec.Windows.iGridLib.iGBorderStyle.None;
            this.fGrid.ContextMenuStrip = this.contextpayments;
            this.fGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fGrid.ForeColor = System.Drawing.SystemColors.WindowText;
            this.fGrid.FrozenArea.ColCount = 2;
            this.fGrid.GroupBox.BackColor = System.Drawing.Color.LightBlue;
            this.fGrid.Header.Height = 19;
            this.fGrid.HScrollBar.Visibility = TenTec.Windows.iGridLib.iGScrollBarVisibility.Always;
            this.fGrid.Location = new System.Drawing.Point(0, 44);
            this.fGrid.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.fGrid.Name = "fGrid";
            this.fGrid.PrefixGroupValues = false;
            this.fGrid.RowHeader.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.fGrid.RowHeader.Visible = true;
            this.fGrid.RowMode = true;
            this.fGrid.RowModeHasCurCell = true;
            this.fGrid.RowSelectionInCellMode = TenTec.Windows.iGridLib.iGRowSelectionInCellModeTypes.SingleRow;
            this.fGrid.RowTextStartColNear = 2;
            this.fGrid.SelCellsBackColor = System.Drawing.Color.Moccasin;
            this.fGrid.SelCellsBackColorNoFocus = System.Drawing.Color.PapayaWhip;
            this.fGrid.SelCellsForeColor = System.Drawing.Color.Black;
            this.fGrid.ShowControlsInAllCells = false;
            this.fGrid.SilentValidation = true;
            this.fGrid.Size = new System.Drawing.Size(1225, 515);
            this.fGrid.TabIndex = 172;
            this.fGrid.TreeCol = null;
            this.fGrid.TreeLines.Color = System.Drawing.SystemColors.WindowText;
            this.fGrid.VScrollBar.CustomButtons.AddRange(new TenTec.Windows.iGridLib.iGScrollBarCustomButton[] {
            new TenTec.Windows.iGridLib.iGScrollBarCustomButton(TenTec.Windows.iGridLib.iGScrollBarCustomButtonAlign.Far, TenTec.Windows.iGridLib.iGActions.CollapseAll, -1, null, true, null),
            new TenTec.Windows.iGridLib.iGScrollBarCustomButton(TenTec.Windows.iGridLib.iGScrollBarCustomButtonAlign.Far, TenTec.Windows.iGridLib.iGActions.ExpandAll, -1, null, true, null)});
            this.fGrid.VScrollBar.Visibility = TenTec.Windows.iGridLib.iGScrollBarVisibility.Always;
            // 
            // contextpayments
            // 
            this.contextpayments.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printReportToolStripMenuItem,
            this.toolStripMenuItem3});
            this.contextpayments.Name = "contextInvoice";
            this.contextpayments.Size = new System.Drawing.Size(167, 56);
            this.contextpayments.Opening += new System.ComponentModel.CancelEventHandler(this.contextpayments_Opening);
            // 
            // printReportToolStripMenuItem
            // 
            this.printReportToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printReportToolStripMenuItem.Image = global::MTOMS.Properties.Resources.printer1;
            this.printReportToolStripMenuItem.Name = "printReportToolStripMenuItem";
            this.printReportToolStripMenuItem.Size = new System.Drawing.Size(166, 26);
            this.printReportToolStripMenuItem.Text = "Print Report";
            this.printReportToolStripMenuItem.Click += new System.EventHandler(this.printReportToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem3.ForeColor = System.Drawing.Color.Maroon;
            this.toolStripMenuItem3.Image = global::MTOMS.Properties.Resources.delete;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(166, 26);
            this.toolStripMenuItem3.Text = "Delete Entry";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // backworker
            // 
            this.backworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backworker_DoWork);
            this.backworker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backworker_RunWorkerCompleted);
            // 
            // ViewChequeExpenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1225, 559);
            this.Controls.Add(this.fGrid);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewChequeExpenses";
            this.Text = "ViewChequeExpenses";
            this.Load += new System.EventHandler(this.ViewChequeExpenses_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).EndInit();
            this.contextpayments.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.ButtonX buttonAdd;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        private TenTec.Windows.iGridLib.iGrid fGrid;
        private System.ComponentModel.BackgroundWorker backworker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextpayments;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem printReportToolStripMenuItem;

    }
}