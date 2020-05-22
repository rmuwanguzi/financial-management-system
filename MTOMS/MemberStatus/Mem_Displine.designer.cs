namespace MTOMS
{
    partial class Mem_Displine
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateTimeInput2 = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.dateTimeInput1 = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txt_action = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txt_details = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.cbx_cat = new SdaHelperManager.ComboX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.cbx_member = new SdaHelperManager.ComboX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeInput2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeInput1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(456, 336);
            this.panel1.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Location = new System.Drawing.Point(233, 308);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(185, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(42, 308);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(185, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateTimeInput2);
            this.groupBox1.Controls.Add(this.labelX6);
            this.groupBox1.Controls.Add(this.dateTimeInput1);
            this.groupBox1.Controls.Add(this.labelX5);
            this.groupBox1.Controls.Add(this.txt_action);
            this.groupBox1.Controls.Add(this.labelX2);
            this.groupBox1.Controls.Add(this.txt_details);
            this.groupBox1.Controls.Add(this.labelX4);
            this.groupBox1.Controls.Add(this.cbx_cat);
            this.groupBox1.Controls.Add(this.labelX3);
            this.groupBox1.Controls.Add(this.cbx_member);
            this.groupBox1.Controls.Add(this.labelX1);
            this.groupBox1.Location = new System.Drawing.Point(8, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(441, 293);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "9";
            // 
            // dateTimeInput2
            // 
            // 
            // 
            // 
            this.dateTimeInput2.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dateTimeInput2.ButtonDropDown.Visible = true;
            this.dateTimeInput2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeInput2.Format = DevComponents.Editors.eDateTimePickerFormat.Long;
            this.dateTimeInput2.Location = new System.Drawing.Point(119, 264);
            // 
            // 
            // 
            this.dateTimeInput2.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateTimeInput2.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.dateTimeInput2.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dateTimeInput2.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dateTimeInput2.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dateTimeInput2.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dateTimeInput2.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dateTimeInput2.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dateTimeInput2.MonthCalendar.DisplayMonth = new System.DateTime(2011, 8, 1, 0, 0, 0, 0);
            this.dateTimeInput2.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dateTimeInput2.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateTimeInput2.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dateTimeInput2.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dateTimeInput2.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dateTimeInput2.MonthCalendar.TodayButtonVisible = true;
            this.dateTimeInput2.Name = "dateTimeInput2";
            this.dateTimeInput2.Size = new System.Drawing.Size(271, 22);
            this.dateTimeInput2.TabIndex = 6;
            this.dateTimeInput2.Value = new System.DateTime(2012, 3, 30, 15, 36, 55, 0);
            this.dateTimeInput2.ValueChanged += new System.EventHandler(this.dateTimeInput2_ValueChanged);
            // 
            // labelX6
            // 
            this.labelX6.Location = new System.Drawing.Point(59, 264);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(54, 22);
            this.labelX6.TabIndex = 12;
            this.labelX6.Text = "End";
            this.labelX6.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // dateTimeInput1
            // 
            // 
            // 
            // 
            this.dateTimeInput1.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dateTimeInput1.ButtonDropDown.Visible = true;
            this.dateTimeInput1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimeInput1.Format = DevComponents.Editors.eDateTimePickerFormat.Long;
            this.dateTimeInput1.Location = new System.Drawing.Point(119, 238);
            // 
            // 
            // 
            this.dateTimeInput1.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateTimeInput1.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.dateTimeInput1.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dateTimeInput1.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dateTimeInput1.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dateTimeInput1.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dateTimeInput1.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dateTimeInput1.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dateTimeInput1.MonthCalendar.DisplayMonth = new System.DateTime(2011, 8, 1, 0, 0, 0, 0);
            this.dateTimeInput1.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dateTimeInput1.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateTimeInput1.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dateTimeInput1.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dateTimeInput1.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dateTimeInput1.MonthCalendar.TodayButtonVisible = true;
            this.dateTimeInput1.Name = "dateTimeInput1";
            this.dateTimeInput1.Size = new System.Drawing.Size(271, 22);
            this.dateTimeInput1.TabIndex = 5;
            this.dateTimeInput1.Value = new System.DateTime(2012, 3, 30, 15, 36, 55, 0);
            this.dateTimeInput1.ValueChanged += new System.EventHandler(this.dateTimeInput1_ValueChanged);
            // 
            // labelX5
            // 
            this.labelX5.Location = new System.Drawing.Point(59, 238);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(54, 22);
            this.labelX5.TabIndex = 10;
            this.labelX5.Text = "Start";
            this.labelX5.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // txt_action
            // 
            // 
            // 
            // 
            this.txt_action.Border.Class = "TextBoxBorder";
            this.txt_action.Location = new System.Drawing.Point(119, 167);
            this.txt_action.Multiline = true;
            this.txt_action.Name = "txt_action";
            this.txt_action.Size = new System.Drawing.Size(271, 67);
            this.txt_action.TabIndex = 4;
            this.txt_action.TextChanged += new System.EventHandler(this.txt_action_TextChanged);
            // 
            // labelX2
            // 
            this.labelX2.Location = new System.Drawing.Point(207, 149);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(98, 14);
            this.labelX2.TabIndex = 8;
            this.labelX2.Text = "Action Taken";
            // 
            // txt_details
            // 
            // 
            // 
            // 
            this.txt_details.Border.Class = "TextBoxBorder";
            this.txt_details.Location = new System.Drawing.Point(119, 79);
            this.txt_details.Multiline = true;
            this.txt_details.Name = "txt_details";
            this.txt_details.Size = new System.Drawing.Size(271, 67);
            this.txt_details.TabIndex = 3;
            // 
            // labelX4
            // 
            this.labelX4.Location = new System.Drawing.Point(207, 61);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(98, 14);
            this.labelX4.TabIndex = 6;
            this.labelX4.Text = "Other Details";
            // 
            // cbx_cat
            // 
            this.cbx_cat.DisplayMember = "Text";
            this.cbx_cat.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbx_cat.DropDownHeight = 180;
            this.cbx_cat.FormattingEnabled = true;
            this.cbx_cat.IntegralHeight = false;
            this.cbx_cat.ItemHeight = 15;
            this.cbx_cat.Location = new System.Drawing.Point(119, 38);
            this.cbx_cat.MaxDropDownItems = 15;
            this.cbx_cat.Name = "cbx_cat";
            this.cbx_cat.Size = new System.Drawing.Size(271, 21);
            this.cbx_cat.TabIndex = 2;
            this.cbx_cat.ValueMember = "display_name";
            this.cbx_cat.SelectionChangeCommitted += new System.EventHandler(this.cbx_cat_SelectionChangeCommitted);
            this.cbx_cat.SelectedIndexChanged += new System.EventHandler(this.cbx_cat_SelectedIndexChanged);
            this.cbx_cat.TextChanged += new System.EventHandler(this.cbx_cat_TextChanged);
            // 
            // labelX3
            // 
            this.labelX3.Location = new System.Drawing.Point(45, 38);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(68, 21);
            this.labelX3.TabIndex = 4;
            this.labelX3.Text = "Category";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // cbx_member
            // 
            this.cbx_member.DisplayMember = "Text";
            this.cbx_member.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbx_member.DropDownHeight = 180;
            this.cbx_member.FormattingEnabled = true;
            this.cbx_member.IntegralHeight = false;
            this.cbx_member.ItemHeight = 15;
            this.cbx_member.Location = new System.Drawing.Point(119, 13);
            this.cbx_member.MaxDropDownItems = 15;
            this.cbx_member.Name = "cbx_member";
            this.cbx_member.Size = new System.Drawing.Size(271, 21);
            this.cbx_member.TabIndex = 1;
            this.cbx_member.ValueMember = "display_name";
            this.cbx_member.SelectionChangeCommitted += new System.EventHandler(this.cbx_member_SelectionChangeCommitted);
            this.cbx_member.SelectedIndexChanged += new System.EventHandler(this.cbx_member_SelectedIndexChanged);
            this.cbx_member.TextChanged += new System.EventHandler(this.cbx_member_TextChanged);
            // 
            // labelX1
            // 
            this.labelX1.Location = new System.Drawing.Point(59, 13);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(54, 21);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "Member";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // Mem_Displine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 336);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Mem_Displine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Member Displine";
            this.Load += new System.EventHandler(this.Mem_Displine_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeInput2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeInput1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_details;
        private DevComponents.DotNetBar.LabelX labelX4;
        private SdaHelperManager.ComboX cbx_cat;
        private DevComponents.DotNetBar.LabelX labelX3;
        private SdaHelperManager.ComboX cbx_member;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_action;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dateTimeInput2;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dateTimeInput1;
        private DevComponents.DotNetBar.LabelX labelX5;
    }
}