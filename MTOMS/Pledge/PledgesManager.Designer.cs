namespace MTOMS.Pledge
{
    partial class PledgesManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PledgesManager));
            this.paneltotal = new DevComponents.DotNetBar.PanelEx();
            this.buttonAdd = new DevComponents.DotNetBar.ButtonX();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.tabControl1 = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.iGrid1 = new TenTec.Windows.iGridLib.iGrid();
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.tabItem2 = new DevComponents.DotNetBar.TabItem(this.components);
            this.exSplitter1 = new SdaHelperManager.ExSplitter();
            this.backworker = new System.ComponentModel.BackgroundWorker();
            this.fGrid = new TenTec.Windows.iGridLib.iGrid();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.printGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paneltotal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // paneltotal
            // 
            this.paneltotal.Controls.Add(this.buttonAdd);
            this.paneltotal.Dock = System.Windows.Forms.DockStyle.Top;
            this.paneltotal.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paneltotal.Location = new System.Drawing.Point(0, 0);
            this.paneltotal.Name = "paneltotal";
            this.paneltotal.Size = new System.Drawing.Size(1214, 39);
            this.paneltotal.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.paneltotal.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(161)))), ((int)(((byte)(220)))));
            this.paneltotal.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(56)))), ((int)(((byte)(148)))));
            this.paneltotal.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.paneltotal.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.paneltotal.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.paneltotal.Style.GradientAngle = 90;
            this.paneltotal.TabIndex = 6;
            // 
            // buttonAdd
            // 
            this.buttonAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonAdd.AutoExpandOnClick = true;
            this.buttonAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonAdd.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdd.ForeColor = System.Drawing.Color.DarkGreen;
            this.buttonAdd.Location = new System.Drawing.Point(2, 1);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(203, 36);
            this.buttonAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonAdd.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1,
            this.buttonItem3});
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.Text = "Tasks";
            // 
            // buttonItem1
            // 
            this.buttonItem1.GlobalItem = false;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "Make Pledge";
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // buttonItem3
            // 
            this.buttonItem3.GlobalItem = false;
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.Text = "Refresh";
            // 
            // tabControl1
            // 
            this.tabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.tabControl1.CanReorderTabs = true;
            this.tabControl1.ColorScheme.TabItemBackground = System.Drawing.Color.WhiteSmoke;
            this.tabControl1.ColorScheme.TabItemBackground2 = System.Drawing.Color.WhiteSmoke;
            this.tabControl1.ColorScheme.TabItemBackgroundColorBlend.AddRange(new DevComponents.DotNetBar.BackgroundColorBlend[] {
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(230)))), ((int)(((byte)(249))))), 0F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(220)))), ((int)(((byte)(248))))), 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(208)))), ((int)(((byte)(245))))), 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(229)))), ((int)(((byte)(247))))), 1F)});
            this.tabControl1.ColorScheme.TabItemHotBackgroundColorBlend.AddRange(new DevComponents.DotNetBar.BackgroundColorBlend[] {
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(253)))), ((int)(((byte)(235))))), 0F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(236)))), ((int)(((byte)(168))))), 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(218)))), ((int)(((byte)(89))))), 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(230)))), ((int)(((byte)(141))))), 1F)});
            this.tabControl1.ColorScheme.TabItemSelectedBackgroundColorBlend.AddRange(new DevComponents.DotNetBar.BackgroundColorBlend[] {
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.White, 0F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254))))), 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254))))), 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254))))), 1F)});
            this.tabControl1.ColorScheme.TabPanelBackground2 = System.Drawing.Color.WhiteSmoke;
            this.tabControl1.Controls.Add(this.tabControlPanel1);
            this.tabControl1.Controls.Add(this.tabControlPanel2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 39);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.tabControl1.SelectedTabIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(561, 449);
            this.tabControl1.Style = DevComponents.DotNetBar.eTabStripStyle.Office2007Document;
            this.tabControl1.TabIndex = 8;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl1.Tabs.Add(this.tabItem1);
            this.tabControl1.Tabs.Add(this.tabItem2);
            this.tabControl1.Text = "tabControl1";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.iGrid1);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 24);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(561, 425);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.WhiteSmoke;
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tabItem1;
            // 
            // iGrid1
            // 
            this.iGrid1.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGrid1.AutoHeightRowMode = TenTec.Windows.iGridLib.iGAutoHeightRowMode.Cells;
            this.iGrid1.AutoResizeCols = true;
            this.iGrid1.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.iGrid1.CellCtrlBackColor = System.Drawing.Color.Empty;
            this.iGrid1.CellCtrlForeColor = System.Drawing.Color.Black;
            this.iGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iGrid1.FocusRectColor1 = System.Drawing.SystemColors.ControlDarkDark;
            this.iGrid1.FocusRectColor2 = System.Drawing.SystemColors.ControlDarkDark;
            this.iGrid1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.iGrid1.GridLines.Mode = TenTec.Windows.iGridLib.iGGridLinesMode.Vertical;
            this.iGrid1.GridLines.Vertical = new TenTec.Windows.iGridLib.iGPenStyle(System.Drawing.Color.LightGray, 1, System.Drawing.Drawing2D.DashStyle.Solid);
            this.iGrid1.Header.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGrid1.Header.Height = 21;
            this.iGrid1.HScrollBar.Visibility = TenTec.Windows.iGridLib.iGScrollBarVisibility.Hide;
            this.iGrid1.LevelIndent = 21;
            this.iGrid1.Location = new System.Drawing.Point(1, 1);
            this.iGrid1.Name = "iGrid1";
            this.iGrid1.RowMode = true;
            this.iGrid1.RowSelectionInCellMode = TenTec.Windows.iGridLib.iGRowSelectionInCellModeTypes.SingleRow;
            this.iGrid1.SelCellsBackColor = System.Drawing.Color.Moccasin;
            this.iGrid1.SelCellsBackColorNoFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.iGrid1.SelCellsForeColor = System.Drawing.Color.Navy;
            this.iGrid1.Size = new System.Drawing.Size(559, 423);
            this.iGrid1.TabIndex = 167;
            this.iGrid1.TreeCol = null;
            this.iGrid1.TreeLines.Color = System.Drawing.Color.Black;
            this.iGrid1.TreeLines.Visible = true;
            this.iGrid1.CurRowChanged += new System.EventHandler(this.iGrid1_CurRowChanged);
            this.iGrid1.CellMouseUp += new TenTec.Windows.iGridLib.iGCellMouseUpEventHandler(this.iGrid1_CellMouseUp);
            this.iGrid1.SizeChanged += new System.EventHandler(this.iGrid1_SizeChanged);
            // 
            // tabItem1
            // 
            this.tabItem1.AttachedControl = this.tabControlPanel1;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "Current";
            this.tabItem1.TextColor = System.Drawing.Color.Green;
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 24);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(561, 425);
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.WhiteSmoke;
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.TabIndex = 2;
            this.tabControlPanel2.TabItem = this.tabItem2;
            // 
            // tabItem2
            // 
            this.tabItem2.AttachedControl = this.tabControlPanel2;
            this.tabItem2.Name = "tabItem2";
            this.tabItem2.Text = "Completed";
            this.tabItem2.TextColor = System.Drawing.Color.Maroon;
            // 
            // exSplitter1
            // 
            this.exSplitter1.Location = new System.Drawing.Point(561, 39);
            this.exSplitter1.Name = "exSplitter1";
            this.exSplitter1.Size = new System.Drawing.Size(7, 449);
            this.exSplitter1.TabIndex = 9;
            this.exSplitter1.TabStop = false;
            this.exSplitter1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.exSplitter1_SplitterMoved);
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
            this.fGrid.ContextMenuStrip = this.contextMenuStrip1;
            this.fGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fGrid.ForeColor = System.Drawing.SystemColors.WindowText;
            this.fGrid.FrozenArea.ColCount = 2;
            this.fGrid.GroupBox.BackColor = System.Drawing.Color.LightBlue;
            this.fGrid.Header.Height = 19;
            this.fGrid.Location = new System.Drawing.Point(568, 39);
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
            this.fGrid.Size = new System.Drawing.Size(646, 449);
            this.fGrid.TabIndex = 172;
            this.fGrid.TreeCol = null;
            this.fGrid.TreeLines.Color = System.Drawing.SystemColors.WindowText;
            this.fGrid.VScrollBar.CustomButtons.AddRange(new TenTec.Windows.iGridLib.iGScrollBarCustomButton[] {
            new TenTec.Windows.iGridLib.iGScrollBarCustomButton(TenTec.Windows.iGridLib.iGScrollBarCustomButtonAlign.Far, TenTec.Windows.iGridLib.iGActions.CollapseAll, -1, null, true, null),
            new TenTec.Windows.iGridLib.iGScrollBarCustomButton(TenTec.Windows.iGridLib.iGScrollBarCustomButtonAlign.Far, TenTec.Windows.iGridLib.iGActions.ExpandAll, -1, null, true, null)});
            this.fGrid.VScrollBar.Visibility = TenTec.Windows.iGridLib.iGScrollBarVisibility.Always;
            this.fGrid.AfterContentsSorted += new System.EventHandler(this.fGrid_AfterContentsSorted);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printGridToolStripMenuItem,
            this.exportToExcelToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(177, 70);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // printGridToolStripMenuItem
            // 
            this.printGridToolStripMenuItem.Checked = true;
            this.printGridToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.printGridToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printGridToolStripMenuItem.ForeColor = System.Drawing.Color.DarkMagenta;
            this.printGridToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printGridToolStripMenuItem.Image")));
            this.printGridToolStripMenuItem.Name = "printGridToolStripMenuItem";
            this.printGridToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.printGridToolStripMenuItem.Text = "Print Grid";
            this.printGridToolStripMenuItem.Visible = false;
            this.printGridToolStripMenuItem.Click += new System.EventHandler(this.printGridToolStripMenuItem_Click);
            // 
            // exportToExcelToolStripMenuItem
            // 
            this.exportToExcelToolStripMenuItem.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportToExcelToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.exportToExcelToolStripMenuItem.Name = "exportToExcelToolStripMenuItem";
            this.exportToExcelToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.exportToExcelToolStripMenuItem.Text = "Export To Excel";
            this.exportToExcelToolStripMenuItem.Click += new System.EventHandler(this.exportToExcelToolStripMenuItem_Click);
            // 
            // PledgesManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 488);
            this.Controls.Add(this.fGrid);
            this.Controls.Add(this.exSplitter1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.paneltotal);
            this.DoubleBuffered = true;
            this.Name = "PledgesManager";
            this.Text = "PledgesManager";
            this.Load += new System.EventHandler(this.PledgesManager_Load);
            this.paneltotal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx paneltotal;
        private DevComponents.DotNetBar.ButtonX buttonAdd;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        private DevComponents.DotNetBar.TabControl tabControl1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.TabItem tabItem2;
        private TenTec.Windows.iGridLib.iGrid iGrid1;
        private SdaHelperManager.ExSplitter exSplitter1;
        private System.ComponentModel.BackgroundWorker backworker;
        private TenTec.Windows.iGridLib.iGrid fGrid;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem printGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToExcelToolStripMenuItem;
    }
}