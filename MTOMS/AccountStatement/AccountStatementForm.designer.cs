namespace MTOMS
{
    partial class AccountStatementForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountStatementForm));
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewChequeExpenseDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printStatementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabTrialBalance = new DevComponents.DotNetBar.TabItem(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.paneltop.SuspendLayout();
            this.panelinnerpanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePicker2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimePicker1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iGrid1)).BeginInit();
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
            this.paneltop.Size = new System.Drawing.Size(1027, 104);
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
            this.panelmain.Location = new System.Drawing.Point(105, 70);
            this.panelmain.Name = "panelmain";
            this.panelmain.Size = new System.Drawing.Size(773, 31);
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
            this.panelinnerpanel.Location = new System.Drawing.Point(105, 4);
            this.panelinnerpanel.Name = "panelinnerpanel";
            this.panelinnerpanel.Size = new System.Drawing.Size(773, 63);
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
            this.buttonrefresh.Location = new System.Drawing.Point(397, 3);
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
            this.labelX7.Location = new System.Drawing.Point(235, 6);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(72, 16);
            this.labelX7.TabIndex = 6;
            this.labelX7.Text = "Start Year";
            // 
            // comboyear
            // 
            this.comboyear.DisplayMember = "Text";
            this.comboyear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboyear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboyear.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboyear.FormattingEnabled = true;
            this.comboyear.ItemHeight = 16;
            this.comboyear.Location = new System.Drawing.Point(308, 4);
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
            this.panel1.Location = new System.Drawing.Point(13, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(757, 28);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // dateTimePicker2
            // 
            // 
            // 
            // 
            this.dateTimePicker2.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dateTimePicker2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimePicker2.ButtonClear.Visible = true;
            this.dateTimePicker2.ButtonDropDown.Visible = true;
            this.dateTimePicker2.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker2.Format = DevComponents.Editors.eDateTimePickerFormat.Long;
            this.dateTimePicker2.IsPopupCalendarOpen = false;
            this.dateTimePicker2.Location = new System.Drawing.Point(430, 3);
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
            this.dateTimePicker2.Size = new System.Drawing.Size(300, 25);
            this.dateTimePicker2.TabIndex = 14;
            this.dateTimePicker2.Value = new System.DateTime(2011, 5, 24, 0, 0, 0, 0);
            this.dateTimePicker2.Click += new System.EventHandler(this.dateTimePicker2_Click);
            // 
            // dateTimePicker1
            // 
            // 
            // 
            // 
            this.dateTimePicker1.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dateTimePicker1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimePicker1.ButtonDropDown.Visible = true;
            this.dateTimePicker1.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.dateTimePicker1.Size = new System.Drawing.Size(318, 25);
            this.dateTimePicker1.TabIndex = 13;
            this.dateTimePicker1.Value = new System.DateTime(2011, 5, 24, 0, 0, 0, 0);
            this.dateTimePicker1.Click += new System.EventHandler(this.dateTimePicker1_Click);
            // 
            // checkdate
            // 
            this.checkdate.AutoSize = true;
            this.checkdate.Enabled = false;
            this.checkdate.Location = new System.Drawing.Point(736, 9);
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
            this.labelX2.Location = new System.Drawing.Point(393, 6);
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
            this.tabControl1.Controls.Add(this.tabControlPanel1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 104);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.tabControl1.SelectedTabIndex = 1;
            this.tabControl1.Size = new System.Drawing.Size(1027, 555);
            this.tabControl1.Style = DevComponents.DotNetBar.eTabStripStyle.Office2007Document;
            this.tabControl1.TabIndex = 37;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl1.Tabs.Add(this.tabTrialBalance);
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
            this.tabControlPanel1.Size = new System.Drawing.Size(1027, 531);
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
            this.iGrid1.ContextMenuStrip = this.contextMenuStrip1;
            this.iGrid1.FocusRectColor1 = System.Drawing.SystemColors.ControlDarkDark;
            this.iGrid1.FocusRectColor2 = System.Drawing.SystemColors.ControlDarkDark;
            this.iGrid1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.iGrid1.GridLines.Horizontal = new TenTec.Windows.iGridLib.iGPenStyle(System.Drawing.Color.Gainsboro, 1, System.Drawing.Drawing2D.DashStyle.Solid);
            this.iGrid1.Header.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGrid1.Header.Height = 21;
            this.iGrid1.LevelIndent = 21;
            this.iGrid1.Location = new System.Drawing.Point(4, 4);
            this.iGrid1.Name = "iGrid1";
            this.iGrid1.RowHeader.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGrid1.RowHeader.Visible = true;
            this.iGrid1.RowMode = true;
            this.iGrid1.RowSelectionInCellMode = TenTec.Windows.iGridLib.iGRowSelectionInCellModeTypes.SingleRow;
            this.iGrid1.SelCellsBackColor = System.Drawing.Color.Moccasin;
            this.iGrid1.SelCellsBackColorNoFocus = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.iGrid1.SelCellsForeColor = System.Drawing.Color.Navy;
            this.iGrid1.Size = new System.Drawing.Size(1019, 515);
            this.iGrid1.TabIndex = 167;
            this.iGrid1.TreeCol = null;
            this.iGrid1.TreeLines.Color = System.Drawing.Color.Black;
            this.iGrid1.TreeLines.Visible = true;
            this.iGrid1.AfterRowStateChanged += new TenTec.Windows.iGridLib.iGAfterRowStateChangedEventHandler(this.iGrid1_AfterRowStateChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewChequeExpenseDetailsToolStripMenuItem,
            this.printStatementToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(330, 64);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // viewChequeExpenseDetailsToolStripMenuItem
            // 
            this.viewChequeExpenseDetailsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewChequeExpenseDetailsToolStripMenuItem.Name = "viewChequeExpenseDetailsToolStripMenuItem";
            this.viewChequeExpenseDetailsToolStripMenuItem.Size = new System.Drawing.Size(329, 30);
            this.viewChequeExpenseDetailsToolStripMenuItem.Text = "View Cheque Expense Details";
            this.viewChequeExpenseDetailsToolStripMenuItem.Click += new System.EventHandler(this.viewChequeExpenseDetailsToolStripMenuItem_Click);
            // 
            // printStatementToolStripMenuItem
            // 
            this.printStatementToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printStatementToolStripMenuItem.ForeColor = System.Drawing.Color.Indigo;
            this.printStatementToolStripMenuItem.Image = global::MTOMS.Properties.Resources.printer1;
            this.printStatementToolStripMenuItem.Name = "printStatementToolStripMenuItem";
            this.printStatementToolStripMenuItem.Size = new System.Drawing.Size(329, 30);
            this.printStatementToolStripMenuItem.Text = "Print Statement";
            this.printStatementToolStripMenuItem.Click += new System.EventHandler(this.printStatementToolStripMenuItem_Click);
            // 
            // tabTrialBalance
            // 
            this.tabTrialBalance.AttachedControl = this.tabControlPanel1;
            this.tabTrialBalance.Name = "tabTrialBalance";
            this.tabTrialBalance.Text = "General";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Addition .png");
            this.imageList1.Images.SetKeyName(1, "Minus.png");
            // 
            // AccountStatementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 659);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.paneltop);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountStatementForm";
            this.Text = "Account Statement Form";
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
        private TenTec.Windows.iGridLib.iGrid iGrid1;
        private DevComponents.DotNetBar.PanelEx panelmain;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem printStatementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewChequeExpenseDetailsToolStripMenuItem;
    }
}