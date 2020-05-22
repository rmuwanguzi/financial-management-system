namespace MTOMS
{
    partial class TrialBalanceForm
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
            this.paneltop = new DevComponents.DotNetBar.PanelEx();
            this.panelmain = new DevComponents.DotNetBar.PanelEx();
            this.panelinnerpanel = new DevComponents.DotNetBar.PanelEx();
            this.buttonrefresh = new DevComponents.DotNetBar.ButtonX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.comboyear = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dateTimePicker2 = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.dateTimePicker1 = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.checkdate = new System.Windows.Forms.CheckBox();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.backworker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.iGrid1 = new TenTec.Windows.iGridLib.iGrid();
            this.tabTrialBalance = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel3 = new DevComponents.DotNetBar.TabControlPanel();
            this.iGridBS = new TenTec.Windows.iGridLib.iGrid();
            this.tabBS = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.iGridPL = new TenTec.Windows.iGridLib.iGrid();
            this.tabPL = new DevComponents.DotNetBar.TabItem(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.printStatementToolStripMenuItemSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.paneltop.SuspendLayout();
            this.panelinnerpanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePicker2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePicker1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iGrid1)).BeginInit();
            this.tabControlPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iGridBS)).BeginInit();
            this.tabControlPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iGridPL)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // paneltop
            // 
            this.paneltop.CanvasColor = System.Drawing.SystemColors.Control;
            this.paneltop.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.paneltop.Controls.Add(this.panelmain);
            this.paneltop.Controls.Add(this.panelinnerpanel);
            this.paneltop.Dock = System.Windows.Forms.DockStyle.Top;
            this.paneltop.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paneltop.Location = new System.Drawing.Point(0, 0);
            this.paneltop.Name = "paneltop";
            this.paneltop.Size = new System.Drawing.Size(968, 104);
            this.paneltop.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.paneltop.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.paneltop.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.paneltop.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.paneltop.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.paneltop.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.paneltop.Style.GradientAngle = 90;
            this.paneltop.TabIndex = 36;
            this.paneltop.SizeChanged += new System.EventHandler(this.paneltop_SizeChanged);
            this.paneltop.Click += new System.EventHandler(this.paneltop_Click);
            // 
            // panelmain
            // 
            this.panelmain.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelmain.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelmain.Location = new System.Drawing.Point(16, 70);
            this.panelmain.Name = "panelmain";
            this.panelmain.Size = new System.Drawing.Size(935, 31);
            this.panelmain.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelmain.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelmain.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelmain.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelmain.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelmain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelmain.Style.GradientAngle = 90;
            this.panelmain.TabIndex = 11;
            this.panelmain.Click += new System.EventHandler(this.panelmain_Click);
            // 
            // panelinnerpanel
            // 
            this.panelinnerpanel.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelinnerpanel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.panelinnerpanel.Controls.Add(this.buttonrefresh);
            this.panelinnerpanel.Controls.Add(this.labelX7);
            this.panelinnerpanel.Controls.Add(this.comboyear);
            this.panelinnerpanel.Controls.Add(this.panel1);
            this.panelinnerpanel.Location = new System.Drawing.Point(139, 4);
            this.panelinnerpanel.Name = "panelinnerpanel";
            this.panelinnerpanel.Size = new System.Drawing.Size(719, 63);
            this.panelinnerpanel.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelinnerpanel.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelinnerpanel.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelinnerpanel.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelinnerpanel.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelinnerpanel.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelinnerpanel.Style.GradientAngle = 90;
            this.panelinnerpanel.TabIndex = 10;
            // 
            // buttonrefresh
            // 
            this.buttonrefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonrefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonrefresh.Enabled = false;
            this.buttonrefresh.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonrefresh.ForeColor = System.Drawing.Color.Indigo;
            this.buttonrefresh.Location = new System.Drawing.Point(352, 4);
            this.buttonrefresh.Name = "buttonrefresh";
            this.buttonrefresh.Size = new System.Drawing.Size(97, 23);
            this.buttonrefresh.TabIndex = 7;
            this.buttonrefresh.Text = "Refresh";
            this.buttonrefresh.Click += new System.EventHandler(this.buttonrefresh_Click);
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.Class = "";
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(226, 7);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(32, 16);
            this.labelX7.TabIndex = 6;
            this.labelX7.Text = "Year";
            // 
            // comboyear
            // 
            this.comboyear.DisplayMember = "Text";
            this.comboyear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboyear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboyear.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboyear.FormattingEnabled = true;
            this.comboyear.ItemHeight = 16;
            this.comboyear.Location = new System.Drawing.Point(259, 4);
            this.comboyear.Name = "comboyear";
            this.comboyear.Size = new System.Drawing.Size(87, 22);
            this.comboyear.TabIndex = 2;
            this.comboyear.SelectedIndexChanged += new System.EventHandler(this.comboyear_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Controls.Add(this.dateTimePicker2);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.checkdate);
            this.panel1.Controls.Add(this.labelX2);
            this.panel1.Controls.Add(this.labelX3);
            this.panel1.Location = new System.Drawing.Point(4, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(712, 28);
            this.panel1.TabIndex = 0;
            // 
            // dateTimePicker2
            // 
            // 
            // 
            // 
            this.dateTimePicker2.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dateTimePicker2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimePicker2.ButtonDropDown.Visible = true;
            this.dateTimePicker2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker2.Format = DevComponents.Editors.eDateTimePickerFormat.Long;
            this.dateTimePicker2.IsPopupCalendarOpen = false;
            this.dateTimePicker2.Location = new System.Drawing.Point(413, 3);
            // 
            // 
            // 
            this.dateTimePicker2.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateTimePicker2.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dateTimePicker2.MonthCalendar.BackgroundStyle.Class = "";
            this.dateTimePicker2.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimePicker2.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dateTimePicker2.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dateTimePicker2.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dateTimePicker2.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dateTimePicker2.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dateTimePicker2.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dateTimePicker2.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dateTimePicker2.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dateTimePicker2.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimePicker2.MonthCalendar.DisplayMonth = new System.DateTime(2011, 5, 1, 0, 0, 0, 0);
            this.dateTimePicker2.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dateTimePicker2.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateTimePicker2.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dateTimePicker2.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dateTimePicker2.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dateTimePicker2.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dateTimePicker2.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimePicker2.MonthCalendar.TodayButtonVisible = true;
            this.dateTimePicker2.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(270, 22);
            this.dateTimePicker2.TabIndex = 14;
            this.dateTimePicker2.Value = new System.DateTime(2011, 5, 24, 0, 0, 0, 0);
            // 
            // dateTimePicker1
            // 
            // 
            // 
            // 
            this.dateTimePicker1.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dateTimePicker1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimePicker1.ButtonDropDown.Visible = true;
            this.dateTimePicker1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = DevComponents.Editors.eDateTimePickerFormat.Long;
            this.dateTimePicker1.IsPopupCalendarOpen = false;
            this.dateTimePicker1.Location = new System.Drawing.Point(52, 3);
            // 
            // 
            // 
            this.dateTimePicker1.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateTimePicker1.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dateTimePicker1.MonthCalendar.BackgroundStyle.Class = "";
            this.dateTimePicker1.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimePicker1.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dateTimePicker1.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dateTimePicker1.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dateTimePicker1.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dateTimePicker1.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dateTimePicker1.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dateTimePicker1.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dateTimePicker1.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dateTimePicker1.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimePicker1.MonthCalendar.DisplayMonth = new System.DateTime(2011, 5, 1, 0, 0, 0, 0);
            this.dateTimePicker1.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dateTimePicker1.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateTimePicker1.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dateTimePicker1.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dateTimePicker1.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dateTimePicker1.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dateTimePicker1.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimePicker1.MonthCalendar.TodayButtonVisible = true;
            this.dateTimePicker1.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(290, 22);
            this.dateTimePicker1.TabIndex = 13;
            this.dateTimePicker1.Value = new System.DateTime(2011, 5, 24, 0, 0, 0, 0);
            // 
            // checkdate
            // 
            this.checkdate.AutoSize = true;
            this.checkdate.Enabled = false;
            this.checkdate.Location = new System.Drawing.Point(684, 9);
            this.checkdate.Name = "checkdate";
            this.checkdate.Size = new System.Drawing.Size(15, 14);
            this.checkdate.TabIndex = 8;
            this.checkdate.UseVisualStyleBackColor = true;
            this.checkdate.CheckedChanged += new System.EventHandler(this.checkdate_CheckedChanged);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.ForeColor = System.Drawing.Color.Maroon;
            this.labelX2.Location = new System.Drawing.Point(376, 6);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(32, 16);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "End";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.ForeColor = System.Drawing.Color.Maroon;
            this.labelX3.Location = new System.Drawing.Point(6, 3);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(42, 19);
            this.labelX3.TabIndex = 3;
            this.labelX3.Text = "Start";
            // 
            // backworker1
            // 
            this.backworker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backworker1_DoWork);
            this.backworker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backworker1_RunWorkerCompleted);
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
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
            this.tabControl1.Controls.Add(this.tabControlPanel2);
            this.tabControl1.Controls.Add(this.tabControlPanel1);
            this.tabControl1.Controls.Add(this.tabControlPanel3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 104);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.tabControl1.SelectedTabIndex = 1;
            this.tabControl1.Size = new System.Drawing.Size(968, 395);
            this.tabControl1.Style = DevComponents.DotNetBar.eTabStripStyle.Office2007Document;
            this.tabControl1.TabIndex = 37;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl1.Tabs.Add(this.tabTrialBalance);
            this.tabControl1.Tabs.Add(this.tabPL);
            this.tabControl1.Tabs.Add(this.tabBS);
            this.tabControl1.Text = "tabControl1";
            this.tabControl1.SelectedTabChanged += new DevComponents.DotNetBar.TabStrip.SelectedTabChangedEventHandler(this.tabControl1_SelectedTabChanged);
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.iGrid1);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 24);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(968, 371);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.WhiteSmoke;
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tabTrialBalance;
            this.tabControlPanel1.SizeChanged += new System.EventHandler(this.tabControlPanel1_SizeChanged);
            // 
            // iGrid1
            // 
            this.iGrid1.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGrid1.AutoHeightRowMode = TenTec.Windows.iGridLib.iGAutoHeightRowMode.Cells;
            this.iGrid1.AutoResizeCols = true;
            this.iGrid1.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.iGrid1.BorderStyle = TenTec.Windows.iGridLib.iGBorderStyle.Flat;
            this.iGrid1.CellCtrlBackColor = System.Drawing.Color.Empty;
            this.iGrid1.CellCtrlForeColor = System.Drawing.Color.Black;
            this.iGrid1.FocusRectColor1 = System.Drawing.SystemColors.ControlDarkDark;
            this.iGrid1.FocusRectColor2 = System.Drawing.SystemColors.ControlDarkDark;
            this.iGrid1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.iGrid1.GridLines.Mode = TenTec.Windows.iGridLib.iGGridLinesMode.Vertical;
            this.iGrid1.Header.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGrid1.Header.Height = 21;
            this.iGrid1.LevelIndent = 21;
            this.iGrid1.Location = new System.Drawing.Point(180, 10);
            this.iGrid1.Name = "iGrid1";
            this.iGrid1.RowHeader.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGrid1.RowHeader.Visible = true;
            this.iGrid1.RowMode = true;
            this.iGrid1.RowSelectionInCellMode = TenTec.Windows.iGridLib.iGRowSelectionInCellModeTypes.SingleRow;
            this.iGrid1.SelCellsBackColor = System.Drawing.Color.Moccasin;
            this.iGrid1.SelCellsBackColorNoFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.iGrid1.SelCellsForeColor = System.Drawing.Color.Navy;
            this.iGrid1.Size = new System.Drawing.Size(678, 349);
            this.iGrid1.TabIndex = 167;
            this.iGrid1.TreeCol = null;
            this.iGrid1.TreeLines.Color = System.Drawing.Color.Black;
            this.iGrid1.TreeLines.Visible = true;
            this.iGrid1.AfterRowStateChanged += new TenTec.Windows.iGridLib.iGAfterRowStateChangedEventHandler(this.iGrid1_AfterRowStateChanged);
            // 
            // tabTrialBalance
            // 
            this.tabTrialBalance.AttachedControl = this.tabControlPanel1;
            this.tabTrialBalance.Name = "tabTrialBalance";
            this.tabTrialBalance.Text = "Trial Balance";
            // 
            // tabControlPanel3
            // 
            this.tabControlPanel3.Controls.Add(this.iGridBS);
            this.tabControlPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel3.Location = new System.Drawing.Point(0, 24);
            this.tabControlPanel3.Name = "tabControlPanel3";
            this.tabControlPanel3.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel3.Size = new System.Drawing.Size(968, 371);
            this.tabControlPanel3.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tabControlPanel3.Style.BackColor2.Color = System.Drawing.Color.WhiteSmoke;
            this.tabControlPanel3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel3.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel3.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel3.Style.GradientAngle = 90;
            this.tabControlPanel3.TabIndex = 3;
            this.tabControlPanel3.TabItem = this.tabBS;
            this.tabControlPanel3.SizeChanged += new System.EventHandler(this.tabControlPanel3_SizeChanged);
            // 
            // iGridBS
            // 
            this.iGridBS.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGridBS.AutoHeightRowMode = TenTec.Windows.iGridLib.iGAutoHeightRowMode.Cells;
            this.iGridBS.AutoResizeCols = true;
            this.iGridBS.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.iGridBS.BorderStyle = TenTec.Windows.iGridLib.iGBorderStyle.Flat;
            this.iGridBS.CellCtrlBackColor = System.Drawing.Color.Empty;
            this.iGridBS.CellCtrlForeColor = System.Drawing.Color.Black;
            this.iGridBS.FocusRectColor1 = System.Drawing.SystemColors.ControlDarkDark;
            this.iGridBS.FocusRectColor2 = System.Drawing.SystemColors.ControlDarkDark;
            this.iGridBS.ForeColor = System.Drawing.SystemColors.WindowText;
            this.iGridBS.GridLines.Mode = TenTec.Windows.iGridLib.iGGridLinesMode.Vertical;
            this.iGridBS.Header.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGridBS.Header.Height = 21;
            this.iGridBS.LevelIndent = 21;
            this.iGridBS.Location = new System.Drawing.Point(145, 11);
            this.iGridBS.Name = "iGridBS";
            this.iGridBS.RowHeader.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGridBS.RowHeader.Visible = true;
            this.iGridBS.RowMode = true;
            this.iGridBS.RowSelectionInCellMode = TenTec.Windows.iGridLib.iGRowSelectionInCellModeTypes.SingleRow;
            this.iGridBS.SelCellsBackColor = System.Drawing.Color.Moccasin;
            this.iGridBS.SelCellsBackColorNoFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.iGridBS.SelCellsForeColor = System.Drawing.Color.Navy;
            this.iGridBS.Size = new System.Drawing.Size(779, 349);
            this.iGridBS.TabIndex = 169;
            this.iGridBS.TreeCol = null;
            this.iGridBS.TreeLines.Color = System.Drawing.Color.Black;
            this.iGridBS.TreeLines.Visible = true;
            this.iGridBS.AfterRowStateChanged += new TenTec.Windows.iGridLib.iGAfterRowStateChangedEventHandler(this.iGridBS_AfterRowStateChanged);
            // 
            // tabBS
            // 
            this.tabBS.AttachedControl = this.tabControlPanel3;
            this.tabBS.Name = "tabBS";
            this.tabBS.Text = "Balance Sheet";
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Controls.Add(this.iGridPL);
            this.tabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 24);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(968, 371);
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(253)))), ((int)(((byte)(254)))));
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.WhiteSmoke;
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(165)))), ((int)(((byte)(199)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.TabIndex = 2;
            this.tabControlPanel2.TabItem = this.tabPL;
            this.tabControlPanel2.SizeChanged += new System.EventHandler(this.tabControlPanel2_SizeChanged);
            // 
            // iGridPL
            // 
            this.iGridPL.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGridPL.AutoHeightRowMode = TenTec.Windows.iGridLib.iGAutoHeightRowMode.Cells;
            this.iGridPL.AutoResizeCols = true;
            this.iGridPL.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.iGridPL.BorderStyle = TenTec.Windows.iGridLib.iGBorderStyle.Flat;
            this.iGridPL.CellCtrlBackColor = System.Drawing.Color.Empty;
            this.iGridPL.CellCtrlForeColor = System.Drawing.Color.Black;
            this.iGridPL.ContextMenuStrip = this.contextMenuStrip1;
            this.iGridPL.FocusRectColor1 = System.Drawing.SystemColors.ControlDarkDark;
            this.iGridPL.FocusRectColor2 = System.Drawing.SystemColors.ControlDarkDark;
            this.iGridPL.ForeColor = System.Drawing.SystemColors.WindowText;
            this.iGridPL.GridLines.Mode = TenTec.Windows.iGridLib.iGGridLinesMode.Vertical;
            this.iGridPL.Header.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGridPL.Header.Height = 21;
            this.iGridPL.LevelIndent = 21;
            this.iGridPL.Location = new System.Drawing.Point(145, 11);
            this.iGridPL.Name = "iGridPL";
            this.iGridPL.RowHeader.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGridPL.RowHeader.Visible = true;
            this.iGridPL.RowMode = true;
            this.iGridPL.RowSelectionInCellMode = TenTec.Windows.iGridLib.iGRowSelectionInCellModeTypes.SingleRow;
            this.iGridPL.SelCellsBackColor = System.Drawing.Color.Moccasin;
            this.iGridPL.SelCellsBackColorNoFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.iGridPL.SelCellsForeColor = System.Drawing.Color.Navy;
            this.iGridPL.Size = new System.Drawing.Size(678, 349);
            this.iGridPL.TabIndex = 168;
            this.iGridPL.TreeCol = null;
            this.iGridPL.TreeLines.Color = System.Drawing.Color.Black;
            this.iGridPL.TreeLines.Visible = true;
            this.iGridPL.AfterRowStateChanged += new TenTec.Windows.iGridLib.iGAfterRowStateChangedEventHandler(this.iGridPL_AfterRowStateChanged);
            // 
            // tabPL
            // 
            this.tabPL.AttachedControl = this.tabControlPanel2;
            this.tabPL.Name = "tabPL";
            this.tabPL.Text = "Profit And Loss a/c";
            this.tabPL.TextColor = System.Drawing.Color.MidnightBlue;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printStatementToolStripMenuItemSummary,
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(298, 86);
            // 
            // printStatementToolStripMenuItemSummary
            // 
            this.printStatementToolStripMenuItemSummary.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printStatementToolStripMenuItemSummary.ForeColor = System.Drawing.Color.Indigo;
            this.printStatementToolStripMenuItemSummary.Image = global::MTOMS.Properties.Resources.Excel_icon;
            this.printStatementToolStripMenuItemSummary.Name = "printStatementToolStripMenuItemSummary";
            this.printStatementToolStripMenuItemSummary.Size = new System.Drawing.Size(297, 30);
            this.printStatementToolStripMenuItemSummary.Text = "Export To Excel Summary";
            this.printStatementToolStripMenuItemSummary.Click += new System.EventHandler(this.printStatementToolStripMenuItemSummary_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.Indigo;
            this.toolStripMenuItem1.Image = global::MTOMS.Properties.Resources.Excel_icon;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(297, 30);
            this.toolStripMenuItem1.Text = "Export To Excel Detail";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // TrialBalanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 499);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.paneltop);
            this.DoubleBuffered = true;
            this.Name = "TrialBalanceForm";
            this.Text = "TrailBalanceForm";
            this.Load += new System.EventHandler(this.TrailBalanceForm_Load);
            this.paneltop.ResumeLayout(false);
            this.panelinnerpanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePicker2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePicker1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iGrid1)).EndInit();
            this.tabControlPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iGridBS)).EndInit();
            this.tabControlPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iGridPL)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx paneltop;
        private DevComponents.DotNetBar.PanelEx panelinnerpanel;
        private DevComponents.DotNetBar.ButtonX buttonrefresh;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboyear;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dateTimePicker2;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dateTimePicker1;
        private System.Windows.Forms.CheckBox checkdate;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private System.ComponentModel.BackgroundWorker backworker1;
        private System.Windows.Forms.Timer timer1;
        private DevComponents.DotNetBar.TabControl tabControl1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem tabTrialBalance;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.TabItem tabPL;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel3;
        private DevComponents.DotNetBar.TabItem tabBS;
        private TenTec.Windows.iGridLib.iGrid iGrid1;
        private TenTec.Windows.iGridLib.iGrid iGridPL;
        private TenTec.Windows.iGridLib.iGrid iGridBS;
        private DevComponents.DotNetBar.PanelEx panelmain;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem printStatementToolStripMenuItemSummary;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}