namespace MTOMS
{
    partial class PendingChequesManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PendingChequesManager));
            this.fimageList = new System.Windows.Forms.ImageList(this.components);
            this.iGrid2Col0ColHdrStyle = new TenTec.Windows.iGridLib.iGColHdrStyle(true);
            this.paneltotal = new DevComponents.DotNetBar.PanelEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX41 = new DevComponents.DotNetBar.LabelX();
            this.buttonAdd = new DevComponents.DotNetBar.ButtonX();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.backworker = new System.ComponentModel.BackgroundWorker();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MakePaymentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transferChequeBalanceToUnBankedCashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.iGrid2Col0CellStyle = new TenTec.Windows.iGridLib.iGCellStyle(true);
            this.fGrid = new TenTec.Windows.iGridLib.iGrid();
            this.paneltotal.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // fimageList
            // 
            this.fimageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("fimageList.ImageStream")));
            this.fimageList.TransparentColor = System.Drawing.Color.Transparent;
            this.fimageList.Images.SetKeyName(0, "LetterType.gif");
            this.fimageList.Images.SetKeyName(1, "TypeNormal.gif");
            // 
            // iGrid2Col0ColHdrStyle
            // 
            this.iGrid2Col0ColHdrStyle.ImageList = this.fimageList;
            // 
            // paneltotal
            // 
            this.paneltotal.Controls.Add(this.labelX1);
            this.paneltotal.Controls.Add(this.textBoxX1);
            this.paneltotal.Controls.Add(this.labelX41);
            this.paneltotal.Controls.Add(this.buttonAdd);
            this.paneltotal.Dock = System.Windows.Forms.DockStyle.Top;
            this.paneltotal.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paneltotal.Location = new System.Drawing.Point(0, 0);
            this.paneltotal.Name = "paneltotal";
            this.paneltotal.Size = new System.Drawing.Size(1294, 39);
            this.paneltotal.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.paneltotal.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(161)))), ((int)(((byte)(220)))));
            this.paneltotal.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(56)))), ((int)(((byte)(148)))));
            this.paneltotal.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.paneltotal.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.paneltotal.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.paneltotal.Style.GradientAngle = 90;
            this.paneltotal.TabIndex = 5;
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.labelX1.Location = new System.Drawing.Point(621, 4);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(250, 32);
            this.labelX1.TabIndex = 171;
            // 
            // textBoxX1
            // 
            // 
            // 
            // 
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxX1.Location = new System.Drawing.Point(192, 7);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.Size = new System.Drawing.Size(189, 26);
            this.textBoxX1.TabIndex = 170;
            this.textBoxX1.WatermarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBoxX1.WatermarkText = "Cheque No";
            // 
            // labelX41
            // 
            this.labelX41.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX41.BackgroundStyle.Class = "";
            this.labelX41.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX41.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX41.ForeColor = System.Drawing.SystemColors.Control;
            this.labelX41.Location = new System.Drawing.Point(148, 7);
            this.labelX41.Name = "labelX41";
            this.labelX41.Size = new System.Drawing.Size(48, 24);
            this.labelX41.TabIndex = 169;
            this.labelX41.Text = "Search";
            // 
            // buttonAdd
            // 
            this.buttonAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonAdd.AutoExpandOnClick = true;
            this.buttonAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonAdd.Enabled = false;
            this.buttonAdd.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdd.ForeColor = System.Drawing.Color.DarkGreen;
            this.buttonAdd.Location = new System.Drawing.Point(2, 1);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(139, 36);
            this.buttonAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonAdd.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1,
            this.buttonItem2});
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = "Select Task";
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonItem1
            // 
            this.buttonItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "WithDraw Cash From Bank";
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // buttonItem2
            // 
            this.buttonItem2.Image = global::MTOMS.Properties.Resources.refresh;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Text = "Refresh List";
            this.buttonItem2.Click += new System.EventHandler(this.buttonItem2_Click);
            // 
            // backworker
            // 
            this.backworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backworker_DoWork);
            this.backworker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backworker_RunWorkerCompleted);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MakePaymentToolStripMenuItem,
            this.transferChequeBalanceToUnBankedCashToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(381, 86);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // MakePaymentToolStripMenuItem
            // 
            this.MakePaymentToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MakePaymentToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.MakePaymentToolStripMenuItem.Name = "MakePaymentToolStripMenuItem";
            this.MakePaymentToolStripMenuItem.Size = new System.Drawing.Size(380, 30);
            this.MakePaymentToolStripMenuItem.Text = "View Expenses On Selected Cheque";
            this.MakePaymentToolStripMenuItem.Click += new System.EventHandler(this.postToToolStripMenuItem_Click);
            // 
            // transferChequeBalanceToUnBankedCashToolStripMenuItem
            // 
            this.transferChequeBalanceToUnBankedCashToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.transferChequeBalanceToUnBankedCashToolStripMenuItem.ForeColor = System.Drawing.Color.Maroon;
            this.transferChequeBalanceToUnBankedCashToolStripMenuItem.Name = "transferChequeBalanceToUnBankedCashToolStripMenuItem";
            this.transferChequeBalanceToUnBankedCashToolStripMenuItem.Size = new System.Drawing.Size(380, 30);
            this.transferChequeBalanceToUnBankedCashToolStripMenuItem.Text = "Transfer Cheque Balance";
            this.transferChequeBalanceToUnBankedCashToolStripMenuItem.Visible = false;
            this.transferChequeBalanceToUnBankedCashToolStripMenuItem.Click += new System.EventHandler(this.transferChequeBalanceToUnBankedCashToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 40000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // iGrid2Col0CellStyle
            // 
            this.iGrid2Col0CellStyle.ImageList = this.fimageList;
            // 
            // fGrid
            // 
            this.fGrid.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.fGrid.AutoHeightRowMode = TenTec.Windows.iGridLib.iGAutoHeightRowMode.Cells;
            this.fGrid.AutoResizeCols = true;
            this.fGrid.BorderStyle = TenTec.Windows.iGridLib.iGBorderStyle.None;
            this.fGrid.ContextMenuStrip = this.contextMenuStrip1;
            this.fGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fGrid.ForeColor = System.Drawing.SystemColors.WindowText;
            this.fGrid.FrozenArea.ColCount = 2;
            this.fGrid.GroupBox.BackColor = System.Drawing.Color.LightBlue;
            this.fGrid.Header.Height = 19;
            this.fGrid.Location = new System.Drawing.Point(0, 39);
            this.fGrid.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.fGrid.Name = "fGrid";
            this.fGrid.PrefixGroupValues = false;
            this.fGrid.RowHeader.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.fGrid.RowHeader.Visible = true;
            this.fGrid.RowMode = true;
            this.fGrid.RowSelectionInCellMode = TenTec.Windows.iGridLib.iGRowSelectionInCellModeTypes.SingleRow;
            this.fGrid.RowTextStartColNear = 2;
            this.fGrid.SelCellsBackColor = System.Drawing.Color.Moccasin;
            this.fGrid.SelCellsBackColorNoFocus = System.Drawing.Color.PapayaWhip;
            this.fGrid.SelCellsForeColor = System.Drawing.Color.Black;
            this.fGrid.SilentValidation = true;
            this.fGrid.Size = new System.Drawing.Size(1294, 454);
            this.fGrid.TabIndex = 171;
            this.fGrid.TreeCol = null;
            this.fGrid.TreeLines.Color = System.Drawing.SystemColors.WindowText;
            this.fGrid.VScrollBar.CustomButtons.AddRange(new TenTec.Windows.iGridLib.iGScrollBarCustomButton[] {
            new TenTec.Windows.iGridLib.iGScrollBarCustomButton(TenTec.Windows.iGridLib.iGScrollBarCustomButtonAlign.Far, TenTec.Windows.iGridLib.iGActions.CollapseAll, -1, null, true, null),
            new TenTec.Windows.iGridLib.iGScrollBarCustomButton(TenTec.Windows.iGridLib.iGScrollBarCustomButtonAlign.Far, TenTec.Windows.iGridLib.iGActions.ExpandAll, -1, null, true, null)});
            this.fGrid.VScrollBar.Visibility = TenTec.Windows.iGridLib.iGScrollBarVisibility.Always;
            this.fGrid.CellMouseDown += new TenTec.Windows.iGridLib.iGCellMouseDownEventHandler(this.fGrid_CellMouseDown);
            this.fGrid.SizeChanged += new System.EventHandler(this.fGrid_SizeChanged);
            // 
            // PendingChequesManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1294, 493);
            this.Controls.Add(this.fGrid);
            this.Controls.Add(this.paneltotal);
            this.DoubleBuffered = true;
            this.Name = "PendingChequesManager";
            this.Text = "Pending Cheques Manager";
            this.Load += new System.EventHandler(this.FinanceChargesManager_Load);
            this.paneltotal.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx paneltotal;
        private DevComponents.DotNetBar.ButtonX buttonAdd;
        private System.ComponentModel.BackgroundWorker backworker;
        private System.Windows.Forms.ImageList fimageList;
        private TenTec.Windows.iGridLib.iGColHdrStyle iGrid2Col0ColHdrStyle;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MakePaymentToolStripMenuItem;
        private TenTec.Windows.iGridLib.iGCellStyle iGrid2Col0CellStyle;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private DevComponents.DotNetBar.LabelX labelX41;
        private TenTec.Windows.iGridLib.iGrid fGrid;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private System.Windows.Forms.ToolStripMenuItem transferChequeBalanceToUnBankedCashToolStripMenuItem;
    }
}