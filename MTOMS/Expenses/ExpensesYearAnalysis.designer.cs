namespace MTOMS
{
    partial class ExpensesYearAnalysis
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
            TenTec.Windows.iGridLib.iGRowPattern iGRowPattern2 = new TenTec.Windows.iGridLib.iGRowPattern();
            TenTec.Windows.iGridLib.iGCellPattern iGCellPattern2 = new TenTec.Windows.iGridLib.iGCellPattern();
            TenTec.Windows.iGridLib.iGRowPattern iGRowPattern3 = new TenTec.Windows.iGridLib.iGRowPattern();
            TenTec.Windows.iGridLib.iGCellPattern iGCellPattern3 = new TenTec.Windows.iGridLib.iGCellPattern();
            this.paneltop = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxEx1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupboxctrl = new System.Windows.Forms.GroupBox();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.comboyear = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.fStyleGroup = new TenTec.Windows.iGridLib.iGCellStyleDesign();
            this.fStyleName = new TenTec.Windows.iGridLib.iGCellStyleDesign();
            this.tabControl1 = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.fGrid = new TenTec.Windows.iGridLib.iGrid();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exportToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel3 = new DevComponents.DotNetBar.TabControlPanel();
            this.fGridnormal = new TenTec.Windows.iGridLib.iGrid();
            this.tabItem3 = new DevComponents.DotNetBar.TabItem(this.components);
            this.paneltop.SuspendLayout();
            this.groupboxctrl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControlPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fGridnormal)).BeginInit();
            this.SuspendLayout();
            // 
            // paneltop
            // 
            this.paneltop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.paneltop.Controls.Add(this.label1);
            this.paneltop.Controls.Add(this.comboBoxEx1);
            this.paneltop.Controls.Add(this.checkBox1);
            this.paneltop.Controls.Add(this.groupboxctrl);
            this.paneltop.Dock = System.Windows.Forms.DockStyle.Top;
            this.paneltop.Location = new System.Drawing.Point(0, 0);
            this.paneltop.Name = "paneltop";
            this.paneltop.Size = new System.Drawing.Size(991, 50);
            this.paneltop.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(450, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 14);
            this.label1.TabIndex = 14;
            this.label1.Text = "Show";
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.DisplayMember = "Text";
            this.comboBoxEx1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEx1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEx1.Enabled = false;
            this.comboBoxEx1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxEx1.ForeColor = System.Drawing.Color.Maroon;
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.ItemHeight = 20;
            this.comboBoxEx1.Location = new System.Drawing.Point(496, 14);
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.Size = new System.Drawing.Size(273, 26);
            this.comboBoxEx1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxEx1.TabIndex = 13;
            this.comboBoxEx1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEx1_SelectedIndexChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.Color.Purple;
            this.checkBox1.Location = new System.Drawing.Point(222, 18);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(190, 20);
            this.checkBox1.TabIndex = 12;
            this.checkBox1.Text = "Hide Sub Departments\r\n";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // groupboxctrl
            // 
            this.groupboxctrl.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupboxctrl.Controls.Add(this.buttonX1);
            this.groupboxctrl.Controls.Add(this.comboyear);
            this.groupboxctrl.Location = new System.Drawing.Point(5, 4);
            this.groupboxctrl.Name = "groupboxctrl";
            this.groupboxctrl.Size = new System.Drawing.Size(206, 41);
            this.groupboxctrl.TabIndex = 0;
            this.groupboxctrl.TabStop = false;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Enabled = false;
            this.buttonX1.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX1.Image = global::MTOMS.Properties.Resources.refresh;
            this.buttonX1.Location = new System.Drawing.Point(105, 11);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(94, 25);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 6;
            this.buttonX1.Text = "Refresh";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // comboyear
            // 
            this.comboyear.DisplayMember = "Text";
            this.comboyear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboyear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboyear.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboyear.FormattingEnabled = true;
            this.comboyear.ItemHeight = 17;
            this.comboyear.Location = new System.Drawing.Point(6, 11);
            this.comboyear.Name = "comboyear";
            this.comboyear.Size = new System.Drawing.Size(91, 23);
            this.comboyear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboyear.TabIndex = 0;
            this.comboyear.SelectedIndexChanged += new System.EventHandler(this.comboyear_SelectedIndexChanged);
            // 
            // fStyleGroup
            // 
            this.fStyleGroup.BackColor = System.Drawing.Color.Tan;
            this.fStyleGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fStyleGroup.ForeColor = System.Drawing.Color.Blue;
            // 
            // fStyleName
            // 
            this.fStyleName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.fStyleName.ReadOnly = TenTec.Windows.iGridLib.iGBool.True;
            this.fStyleName.TextAlign = TenTec.Windows.iGridLib.iGContentAlignment.MiddleLeft;
            // 
            // tabControl1
            // 
            this.tabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.tabControl1.CanReorderTabs = true;
            this.tabControl1.Controls.Add(this.tabControlPanel1);
            this.tabControl1.Controls.Add(this.tabControlPanel3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 50);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.SelectedTabIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(991, 388);
            this.tabControl1.Style = DevComponents.DotNetBar.eTabStripStyle.Office2007Document;
            this.tabControl1.TabIndex = 2;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl1.Tabs.Add(this.tabItem1);
            this.tabControl1.Tabs.Add(this.tabItem3);
            this.tabControl1.Text = "tabControl1";
            this.tabControl1.SelectedTabChanged += new DevComponents.DotNetBar.TabStrip.SelectedTabChangedEventHandler(this.tabControl1_SelectedTabChanged);
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.fGrid);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 24);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(991, 364);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(188)))), ((int)(((byte)(227)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tabItem1;
            // 
            // fGrid
            // 
            this.fGrid.BorderStyle = TenTec.Windows.iGridLib.iGBorderStyle.None;
            this.fGrid.ContextMenuStrip = this.contextMenuStrip1;
            this.fGrid.DefaultCol.AllowMoving = false;
            this.fGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fGrid.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fGrid.ForeColor = System.Drawing.SystemColors.WindowText;
            this.fGrid.FrozenArea.ColCount = 1;
            this.fGrid.FrozenArea.ColsEdge = new TenTec.Windows.iGridLib.iGPenStyle(System.Drawing.SystemColors.ControlDark, 1, System.Drawing.Drawing2D.DashStyle.Solid);
            this.fGrid.GridLines.Horizontal = new TenTec.Windows.iGridLib.iGPenStyle(System.Drawing.Color.Gainsboro, 1, System.Drawing.Drawing2D.DashStyle.Solid);
            this.fGrid.GridLines.Vertical = new TenTec.Windows.iGridLib.iGPenStyle(System.Drawing.Color.Gainsboro, 1, System.Drawing.Drawing2D.DashStyle.Solid);
            this.fGrid.GroupRowLevelStyles = new TenTec.Windows.iGridLib.iGCellStyle[] {
        ((TenTec.Windows.iGridLib.iGCellStyle)(this.fStyleGroup))};
            this.fGrid.Header.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.fGrid.Header.Height = 22;
            this.fGrid.Header.HGridLinesStyle = new TenTec.Windows.iGridLib.iGPenStyle(System.Drawing.SystemColors.ControlDark, 1, System.Drawing.Drawing2D.DashStyle.Solid);
            this.fGrid.Location = new System.Drawing.Point(1, 1);
            this.fGrid.Name = "fGrid";
            this.fGrid.RowHeader.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.fGrid.RowHeader.Visible = true;
            this.fGrid.RowMode = true;
            this.fGrid.RowResizeMode = TenTec.Windows.iGridLib.iGRowResizeMode.NotAllowed;
            this.fGrid.Rows.AddRange(new TenTec.Windows.iGridLib.iGRowPattern[] {
            iGRowPattern2}, new TenTec.Windows.iGridLib.iGCellPattern[] {
            iGCellPattern2});
            this.fGrid.RowSelectionInCellMode = TenTec.Windows.iGridLib.iGRowSelectionInCellModeTypes.SingleRow;
            this.fGrid.RowTextStartColNear = 1;
            this.fGrid.ShowControlsInAllCells = false;
            this.fGrid.SilentValidation = true;
            this.fGrid.Size = new System.Drawing.Size(989, 362);
            this.fGrid.TabIndex = 10;
            this.fGrid.TreeCol = null;
            this.fGrid.TreeLines.Color = System.Drawing.SystemColors.WindowText;
            this.fGrid.VScrollBar.CustomButtons.AddRange(new TenTec.Windows.iGridLib.iGScrollBarCustomButton[] {
            new TenTec.Windows.iGridLib.iGScrollBarCustomButton(TenTec.Windows.iGridLib.iGScrollBarCustomButtonAlign.Far, TenTec.Windows.iGridLib.iGActions.CollapseAll, -1, "Collapse All", true, null),
            new TenTec.Windows.iGridLib.iGScrollBarCustomButton(TenTec.Windows.iGridLib.iGScrollBarCustomButtonAlign.Far, TenTec.Windows.iGridLib.iGActions.ExpandAll, -1, "Expand All", true, null)});
            this.fGrid.VScrollBar.Visibility = TenTec.Windows.iGridLib.iGScrollBarVisibility.Always;
            this.fGrid.BeforeRowStateChanged += new TenTec.Windows.iGridLib.iGBeforeRowStateChangedEventHandler(this.fGrid_BeforeRowStateChanged);
            this.fGrid.EllipsisButtonClick += new TenTec.Windows.iGridLib.iGEllipsisButtonClickEventHandler(this.fGrid_EllipsisButtonClick);
            this.fGrid.RequestEdit += new TenTec.Windows.iGridLib.iGRequestEditEventHandler(this.fGrid_RequestEdit);
            this.fGrid.DrawEllipsisButtonBackground += new TenTec.Windows.iGridLib.iGDrawEllipsisButtonEventHandler(this.fGrid_DrawEllipsisButtonBackground);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToExcelToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(190, 30);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // exportToExcelToolStripMenuItem
            // 
            this.exportToExcelToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportToExcelToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.exportToExcelToolStripMenuItem.Image = global::MTOMS.Properties.Resources.Excel_icon;
            this.exportToExcelToolStripMenuItem.Name = "exportToExcelToolStripMenuItem";
            this.exportToExcelToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.exportToExcelToolStripMenuItem.Text = "Export To Excel";
            this.exportToExcelToolStripMenuItem.Click += new System.EventHandler(this.exportToExcelToolStripMenuItem_Click);
            // 
            // tabItem1
            // 
            this.tabItem1.AttachedControl = this.tabControlPanel1;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "Month Summary";
            // 
            // tabControlPanel3
            // 
            this.tabControlPanel3.Controls.Add(this.fGridnormal);
            this.tabControlPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel3.Location = new System.Drawing.Point(0, 24);
            this.tabControlPanel3.Name = "tabControlPanel3";
            this.tabControlPanel3.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel3.Size = new System.Drawing.Size(991, 364);
            this.tabControlPanel3.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tabControlPanel3.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(188)))), ((int)(((byte)(227)))));
            this.tabControlPanel3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel3.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel3.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel3.Style.GradientAngle = 90;
            this.tabControlPanel3.TabIndex = 3;
            this.tabControlPanel3.TabItem = this.tabItem3;
            // 
            // fGridnormal
            // 
            this.fGridnormal.BorderStyle = TenTec.Windows.iGridLib.iGBorderStyle.None;
            this.fGridnormal.ContextMenuStrip = this.contextMenuStrip1;
            this.fGridnormal.DefaultCol.AllowMoving = false;
            this.fGridnormal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fGridnormal.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fGridnormal.ForeColor = System.Drawing.SystemColors.WindowText;
            this.fGridnormal.FrozenArea.ColCount = 1;
            this.fGridnormal.FrozenArea.ColsEdge = new TenTec.Windows.iGridLib.iGPenStyle(System.Drawing.SystemColors.ControlDark, 1, System.Drawing.Drawing2D.DashStyle.Solid);
            this.fGridnormal.GridLines.Horizontal = new TenTec.Windows.iGridLib.iGPenStyle(System.Drawing.Color.Gainsboro, 1, System.Drawing.Drawing2D.DashStyle.Solid);
            this.fGridnormal.GridLines.Vertical = new TenTec.Windows.iGridLib.iGPenStyle(System.Drawing.Color.Gainsboro, 1, System.Drawing.Drawing2D.DashStyle.Solid);
            this.fGridnormal.GroupRowLevelStyles = new TenTec.Windows.iGridLib.iGCellStyle[] {
        ((TenTec.Windows.iGridLib.iGCellStyle)(this.fStyleGroup))};
            this.fGridnormal.Header.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.fGridnormal.Header.Height = 22;
            this.fGridnormal.Header.HGridLinesStyle = new TenTec.Windows.iGridLib.iGPenStyle(System.Drawing.SystemColors.ControlDark, 1, System.Drawing.Drawing2D.DashStyle.Solid);
            this.fGridnormal.Location = new System.Drawing.Point(1, 1);
            this.fGridnormal.Name = "fGridnormal";
            this.fGridnormal.RowHeader.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.fGridnormal.RowHeader.Visible = true;
            this.fGridnormal.RowMode = true;
            this.fGridnormal.RowResizeMode = TenTec.Windows.iGridLib.iGRowResizeMode.NotAllowed;
            this.fGridnormal.Rows.AddRange(new TenTec.Windows.iGridLib.iGRowPattern[] {
            iGRowPattern3}, new TenTec.Windows.iGridLib.iGCellPattern[] {
            iGCellPattern3});
            this.fGridnormal.RowSelectionInCellMode = TenTec.Windows.iGridLib.iGRowSelectionInCellModeTypes.SingleRow;
            this.fGridnormal.RowTextStartColNear = 1;
            this.fGridnormal.ShowControlsInAllCells = false;
            this.fGridnormal.SilentValidation = true;
            this.fGridnormal.Size = new System.Drawing.Size(989, 362);
            this.fGridnormal.TabIndex = 11;
            this.fGridnormal.TreeCol = null;
            this.fGridnormal.TreeLines.Color = System.Drawing.SystemColors.WindowText;
            this.fGridnormal.VScrollBar.CustomButtons.AddRange(new TenTec.Windows.iGridLib.iGScrollBarCustomButton[] {
            new TenTec.Windows.iGridLib.iGScrollBarCustomButton(TenTec.Windows.iGridLib.iGScrollBarCustomButtonAlign.Far, TenTec.Windows.iGridLib.iGActions.CollapseAll, -1, "Collapse All", true, null),
            new TenTec.Windows.iGridLib.iGScrollBarCustomButton(TenTec.Windows.iGridLib.iGScrollBarCustomButtonAlign.Far, TenTec.Windows.iGridLib.iGActions.ExpandAll, -1, "Expand All", true, null)});
            this.fGridnormal.VScrollBar.Visibility = TenTec.Windows.iGridLib.iGScrollBarVisibility.Always;
            this.fGridnormal.EllipsisButtonClick += new TenTec.Windows.iGridLib.iGEllipsisButtonClickEventHandler(this.fGridnormal_EllipsisButtonClick);
            this.fGridnormal.RequestEdit += new TenTec.Windows.iGridLib.iGRequestEditEventHandler(this.fGridnormal_RequestEdit);
            this.fGridnormal.DrawEllipsisButtonBackground += new TenTec.Windows.iGridLib.iGDrawEllipsisButtonEventHandler(this.fGridnormal_DrawEllipsisButtonBackground);
            // 
            // tabItem3
            // 
            this.tabItem3.AttachedControl = this.tabControlPanel3;
            this.tabItem3.Name = "tabItem3";
            this.tabItem3.Text = "Quarter Summary";
            // 
            // ExpensesYearAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 438);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.paneltop);
            this.DoubleBuffered = true;
            this.Name = "ExpensesYearAnalysis";
            this.Text = "ExpensesYearAnalysis";
            this.Load += new System.EventHandler(this.ExpensesYearAnalysis_Load);
            this.paneltop.ResumeLayout(false);
            this.paneltop.PerformLayout();
            this.groupboxctrl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControlPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fGridnormal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel paneltop;
        private System.Windows.Forms.GroupBox groupboxctrl;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboyear;
        private TenTec.Windows.iGridLib.iGCellStyleDesign fStyleGroup;
        private TenTec.Windows.iGridLib.iGCellStyleDesign fStyleName;
        private DevComponents.DotNetBar.TabControl tabControl1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private TenTec.Windows.iGridLib.iGrid fGrid;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel3;
        private TenTec.Windows.iGridLib.iGrid fGridnormal;
        private DevComponents.DotNetBar.TabItem tabItem3;
        private System.Windows.Forms.ToolStripMenuItem exportToExcelToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxEx1;
        private System.Windows.Forms.Label label1;
    }
}