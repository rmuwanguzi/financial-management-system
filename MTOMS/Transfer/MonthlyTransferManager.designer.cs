namespace MTOMS
{
    partial class MonthlyTransferManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonthlyTransferManager));
            this.iGrid2Col0CellStyle = new TenTec.Windows.iGridLib.iGCellStyle(true);
            this.fimageList = new System.Windows.Forms.ImageList(this.components);
            this.iGrid2Col0ColHdrStyle = new TenTec.Windows.iGridLib.iGColHdrStyle(true);
            this.paneltotal = new DevComponents.DotNetBar.PanelEx();
            this.buttonAdd = new DevComponents.DotNetBar.ButtonX();
            this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.iGrid1 = new TenTec.Windows.iGridLib.iGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_forward = new System.Windows.Forms.Label();
            this.label_back = new System.Windows.Forms.Label();
            this.labelCalender = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.printTenantReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteExpenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backworker = new System.ComponentModel.BackgroundWorker();
            this.fGrid = new TenTec.Windows.iGridLib.iGrid();
            this.contextpayments = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.paneltotal.SuspendLayout();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iGrid1)).BeginInit();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).BeginInit();
            this.contextpayments.SuspendLayout();
            this.SuspendLayout();
            // 
            // iGrid2Col0CellStyle
            // 
            this.iGrid2Col0CellStyle.ImageList = this.fimageList;
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
            this.paneltotal.Controls.Add(this.buttonAdd);
            this.paneltotal.Dock = System.Windows.Forms.DockStyle.Top;
            this.paneltotal.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paneltotal.Location = new System.Drawing.Point(0, 0);
            this.paneltotal.Name = "paneltotal";
            this.paneltotal.Size = new System.Drawing.Size(1146, 39);
            this.paneltotal.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.paneltotal.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(161)))), ((int)(((byte)(220)))));
            this.paneltotal.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(56)))), ((int)(((byte)(148)))));
            this.paneltotal.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.paneltotal.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.paneltotal.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.paneltotal.Style.GradientAngle = 90;
            this.paneltotal.TabIndex = 5;
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
            this.buttonAdd.Size = new System.Drawing.Size(243, 36);
            this.buttonAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonAdd.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem4,
            this.buttonItem3});
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = "Tasks";
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonItem4
            // 
            this.buttonItem4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonItem4.GlobalItem = false;
            this.buttonItem4.Name = "buttonItem4";
            this.buttonItem4.Text = "Make Cash Transfer";
            this.buttonItem4.Click += new System.EventHandler(this.buttonItem4_Click);
            // 
            // buttonItem3
            // 
            this.buttonItem3.GlobalItem = false;
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.Text = "Refresh";
            this.buttonItem3.Click += new System.EventHandler(this.buttonItem3_Click_1);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.iGrid1);
            this.panelEx1.Controls.Add(this.panel1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelEx1.Location = new System.Drawing.Point(0, 39);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(327, 454);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 6;
            this.panelEx1.Text = "panelEx1";
            // 
            // iGrid1
            // 
            this.iGrid1.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGrid1.AutoHeightRowMode = TenTec.Windows.iGridLib.iGAutoHeightRowMode.Cells;
            this.iGrid1.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.iGrid1.CellCtrlBackColor = System.Drawing.Color.Empty;
            this.iGrid1.CellCtrlForeColor = System.Drawing.Color.Black;
            this.iGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iGrid1.FocusRectColor1 = System.Drawing.SystemColors.ControlDarkDark;
            this.iGrid1.FocusRectColor2 = System.Drawing.SystemColors.ControlDarkDark;
            this.iGrid1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.iGrid1.GridLines.Mode = TenTec.Windows.iGridLib.iGGridLinesMode.Vertical;
            this.iGrid1.Header.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGrid1.Header.Height = 19;
            this.iGrid1.HScrollBar.Visibility = TenTec.Windows.iGridLib.iGScrollBarVisibility.Hide;
            this.iGrid1.LevelIndent = 21;
            this.iGrid1.Location = new System.Drawing.Point(0, 41);
            this.iGrid1.Name = "iGrid1";
            this.iGrid1.RowMode = true;
            this.iGrid1.RowSelectionInCellMode = TenTec.Windows.iGridLib.iGRowSelectionInCellModeTypes.SingleRow;
            this.iGrid1.SelCellsBackColor = System.Drawing.Color.Moccasin;
            this.iGrid1.SelCellsBackColorNoFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.iGrid1.SelCellsForeColor = System.Drawing.Color.Navy;
            this.iGrid1.Size = new System.Drawing.Size(327, 413);
            this.iGrid1.TabIndex = 166;
            this.iGrid1.TreeCol = null;
            this.iGrid1.TreeLines.Color = System.Drawing.Color.Black;
            this.iGrid1.TreeLines.Visible = true;
            this.iGrid1.CurRowChanged += new System.EventHandler(this.iGrid1_CurRowChanged);
            this.iGrid1.Click += new System.EventHandler(this.iGrid1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_forward);
            this.panel1.Controls.Add(this.label_back);
            this.panel1.Controls.Add(this.labelCalender);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(327, 41);
            this.panel1.TabIndex = 0;
            // 
            // label_forward
            // 
            this.label_forward.BackColor = System.Drawing.Color.DimGray;
            this.label_forward.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_forward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_forward.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_forward.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label_forward.Image = global::MTOMS.Properties.Resources.MenuBar_ForwardFrame;
            this.label_forward.Location = new System.Drawing.Point(308, 1);
            this.label_forward.Name = "label_forward";
            this.label_forward.Size = new System.Drawing.Size(18, 38);
            this.label_forward.TabIndex = 3;
            this.label_forward.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_forward.Visible = false;
            this.label_forward.Click += new System.EventHandler(this.label_forward_Click);
            // 
            // label_back
            // 
            this.label_back.BackColor = System.Drawing.Color.DimGray;
            this.label_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_back.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_back.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label_back.Image = global::MTOMS.Properties.Resources.MenuBar_BackFrame;
            this.label_back.Location = new System.Drawing.Point(2, 1);
            this.label_back.Name = "label_back";
            this.label_back.Size = new System.Drawing.Size(18, 38);
            this.label_back.TabIndex = 2;
            this.label_back.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_back.Click += new System.EventHandler(this.label_back_Click);
            // 
            // labelCalender
            // 
            this.labelCalender.BackColor = System.Drawing.Color.DimGray;
            this.labelCalender.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelCalender.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCalender.ForeColor = System.Drawing.SystemColors.Control;
            this.labelCalender.Location = new System.Drawing.Point(0, 0);
            this.labelCalender.Name = "labelCalender";
            this.labelCalender.Size = new System.Drawing.Size(326, 41);
            this.labelCalender.TabIndex = 1;
            this.labelCalender.Text = "2010 December";
            this.labelCalender.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelCalender.Click += new System.EventHandler(this.labelCalender_Click);
            this.labelCalender.DoubleClick += new System.EventHandler(this.labelCalender_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printTenantReportToolStripMenuItem,
            this.deleteExpenseToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(217, 52);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening_1);
            // 
            // printTenantReportToolStripMenuItem
            // 
            this.printTenantReportToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printTenantReportToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.printTenantReportToolStripMenuItem.Image = global::MTOMS.Properties.Resources.printer;
            this.printTenantReportToolStripMenuItem.Name = "printTenantReportToolStripMenuItem";
            this.printTenantReportToolStripMenuItem.Size = new System.Drawing.Size(216, 24);
            this.printTenantReportToolStripMenuItem.Text = "Print Payment Report";
            this.printTenantReportToolStripMenuItem.Click += new System.EventHandler(this.printTenantReportToolStripMenuItem_Click);
            // 
            // deleteExpenseToolStripMenuItem
            // 
            this.deleteExpenseToolStripMenuItem.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteExpenseToolStripMenuItem.ForeColor = System.Drawing.Color.Maroon;
            this.deleteExpenseToolStripMenuItem.Name = "deleteExpenseToolStripMenuItem";
            this.deleteExpenseToolStripMenuItem.Size = new System.Drawing.Size(216, 24);
            this.deleteExpenseToolStripMenuItem.Text = "Delete Expense";
            this.deleteExpenseToolStripMenuItem.Click += new System.EventHandler(this.deleteExpenseToolStripMenuItem_Click);
            // 
            // backworker
            // 
            this.backworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backworker_DoWork);
            this.backworker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backworker_RunWorkerCompleted);
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
            this.fGrid.Location = new System.Drawing.Point(327, 39);
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
            this.fGrid.SilentValidation = true;
            this.fGrid.Size = new System.Drawing.Size(819, 454);
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
            // contextpayments
            // 
            this.contextpayments.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3});
            this.contextpayments.Name = "contextInvoice";
            this.contextpayments.Size = new System.Drawing.Size(167, 48);
            this.contextpayments.Opening += new System.ComponentModel.CancelEventHandler(this.contextpayments_Opening);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem3.ForeColor = System.Drawing.Color.Maroon;
            this.toolStripMenuItem3.Image = global::MTOMS.Properties.Resources.delete;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(166, 22);
            this.toolStripMenuItem3.Text = "Delete Transfer";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // MonthlyTransferManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 493);
            this.Controls.Add(this.fGrid);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.paneltotal);
            this.DoubleBuffered = true;
            this.Name = "MonthlyTransferManager";
            this.Text = "Cash Transfer Manager";
            this.Load += new System.EventHandler(this.ViewPaymentsN_Load);
            this.paneltotal.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iGrid1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).EndInit();
            this.contextpayments.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx paneltotal;
        private DevComponents.DotNetBar.ButtonX buttonAdd;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelCalender;
        private TenTec.Windows.iGridLib.iGrid iGrid1;
        private System.ComponentModel.BackgroundWorker backworker;
        private TenTec.Windows.iGridLib.iGrid fGrid;
        private System.Windows.Forms.ImageList fimageList;
        private TenTec.Windows.iGridLib.iGCellStyle iGrid2Col0CellStyle;
        private TenTec.Windows.iGridLib.iGColHdrStyle iGrid2Col0ColHdrStyle;
        private System.Windows.Forms.Label label_back;
        private System.Windows.Forms.Label label_forward;
        private System.Windows.Forms.ContextMenuStrip contextpayments;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem printTenantReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteExpenseToolStripMenuItem;
        private DevComponents.DotNetBar.ButtonItem buttonItem4;
    }
}