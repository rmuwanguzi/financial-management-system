namespace MTOMS
{
    partial class ChildrenMaker
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.combochild = new SdaHelperManager.ComboX();
            this.picturechild = new System.Windows.Forms.PictureBox();
            this.buttoncanc = new DevComponents.DotNetBar.ButtonX();
            this.buttonsave = new DevComponents.DotNetBar.ButtonX();
            this.txt_birth_year = new SdaHelperManager.CustomTextBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txt_child_name = new SdaHelperManager.CustomTextBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.comboGender = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picturechild)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.comboGender);
            this.groupBox1.Controls.Add(this.labelX4);
            this.groupBox1.Controls.Add(this.labelX3);
            this.groupBox1.Controls.Add(this.combochild);
            this.groupBox1.Controls.Add(this.picturechild);
            this.groupBox1.Controls.Add(this.buttoncanc);
            this.groupBox1.Controls.Add(this.buttonsave);
            this.groupBox1.Controls.Add(this.txt_birth_year);
            this.groupBox1.Controls.Add(this.labelX2);
            this.groupBox1.Controls.Add(this.txt_child_name);
            this.groupBox1.Controls.Add(this.labelX1);
            this.groupBox1.Location = new System.Drawing.Point(1, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(432, 157);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.ForeColor = System.Drawing.Color.Blue;
            this.labelX3.Location = new System.Drawing.Point(84, 19);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(51, 12);
            this.labelX3.TabIndex = 223;
            this.labelX3.Text = "SYSTEM";
            // 
            // combochild
            // 
            this.combochild.DisplayMember = "Text";
            this.combochild.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combochild.DropDownHeight = 180;
            this.combochild.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combochild.FormattingEnabled = true;
            this.combochild.IntegralHeight = false;
            this.combochild.ItemHeight = 16;
            this.combochild.Location = new System.Drawing.Point(138, 12);
            this.combochild.MaxDropDownItems = 15;
            this.combochild.Name = "combochild";
            this.combochild.Size = new System.Drawing.Size(280, 22);
            this.combochild.TabIndex = 0;
            this.combochild.ValueMember = "display_name";
            // 
            // picturechild
            // 
            this.picturechild.BackColor = System.Drawing.SystemColors.ControlText;
            this.picturechild.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picturechild.Location = new System.Drawing.Point(7, 21);
            this.picturechild.Name = "picturechild";
            this.picturechild.Size = new System.Drawing.Size(72, 91);
            this.picturechild.TabIndex = 96;
            this.picturechild.TabStop = false;
            // 
            // buttoncanc
            // 
            this.buttoncanc.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttoncanc.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttoncanc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttoncanc.ForeColor = System.Drawing.Color.Maroon;
            this.buttoncanc.Location = new System.Drawing.Point(241, 116);
            this.buttoncanc.Name = "buttoncanc";
            this.buttoncanc.Size = new System.Drawing.Size(140, 33);
            this.buttoncanc.TabIndex = 4;
            this.buttoncanc.Text = "Cancel";
            this.buttoncanc.Click += new System.EventHandler(this.buttoncanc_Click);
            // 
            // buttonsave
            // 
            this.buttonsave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonsave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonsave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonsave.ForeColor = System.Drawing.Color.ForestGreen;
            this.buttonsave.Location = new System.Drawing.Point(89, 116);
            this.buttonsave.Name = "buttonsave";
            this.buttonsave.Size = new System.Drawing.Size(150, 33);
            this.buttonsave.TabIndex = 3;
            this.buttonsave.Text = "Add To List";
            this.buttonsave.Click += new System.EventHandler(this.buttonsave_Click);
            // 
            // txt_birth_year
            // 
            this.txt_birth_year.AllowCOPY = false;
            this.txt_birth_year.AllowCUT = false;
            this.txt_birth_year.AllowDotCharacter = false;
            this.txt_birth_year.AllowEnterCharacter = false;
            this.txt_birth_year.AllowPASTE = false;
            this.txt_birth_year.AllowSpaceCharacter = false;
            // 
            // 
            // 
            this.txt_birth_year.Border.Class = "TextBoxBorder";
            this.txt_birth_year.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_birth_year.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_birth_year.Location = new System.Drawing.Point(168, 62);
            this.txt_birth_year.MaxLength = 4;
            this.txt_birth_year.Name = "txt_birth_year";
            this.txt_birth_year.ShowDefaultContextMenu = true;
            this.txt_birth_year.Size = new System.Drawing.Size(66, 21);
            this.txt_birth_year.TabIndex = 2;
            this.txt_birth_year.TextBoxType = SdaHelperManager.CustomTextBoxEx.m_TextBoxType.NumberOnly;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.Location = new System.Drawing.Point(100, 68);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(66, 12);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "Birth Year";
            // 
            // txt_child_name
            // 
            this.txt_child_name.AllowCOPY = false;
            this.txt_child_name.AllowCUT = false;
            this.txt_child_name.AllowDotCharacter = false;
            this.txt_child_name.AllowEnterCharacter = false;
            this.txt_child_name.AllowPASTE = false;
            this.txt_child_name.AllowSpaceCharacter = false;
            // 
            // 
            // 
            this.txt_child_name.Border.Class = "TextBoxBorder";
            this.txt_child_name.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_child_name.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_child_name.Location = new System.Drawing.Point(138, 37);
            this.txt_child_name.Name = "txt_child_name";
            this.txt_child_name.ShowDefaultContextMenu = true;
            this.txt_child_name.Size = new System.Drawing.Size(280, 21);
            this.txt_child_name.TabIndex = 1;
            this.txt_child_name.TextBoxType = SdaHelperManager.CustomTextBoxEx.m_TextBoxType.All;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.Location = new System.Drawing.Point(99, 41);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(39, 12);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "Name";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX4.Location = new System.Drawing.Point(117, 89);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(48, 12);
            this.labelX4.TabIndex = 224;
            this.labelX4.Text = "Gender";
            // 
            // comboGender
            // 
            this.comboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboGender.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboGender.FormattingEnabled = true;
            this.comboGender.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.comboGender.Location = new System.Drawing.Point(168, 86);
            this.comboGender.Name = "comboGender";
            this.comboGender.Size = new System.Drawing.Size(87, 22);
            this.comboGender.TabIndex = 225;
            // 
            // ChildrenMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 168);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChildrenMaker";
            this.Text = "Children Details";
            this.Load += new System.EventHandler(this.ChildrenMaker_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picturechild)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private SdaHelperManager.CustomTextBoxEx txt_birth_year;
        private DevComponents.DotNetBar.LabelX labelX2;
        private SdaHelperManager.CustomTextBoxEx txt_child_name;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX buttoncanc;
        private DevComponents.DotNetBar.ButtonX buttonsave;
        internal System.Windows.Forms.PictureBox picturechild;
        private SdaHelperManager.ComboX combochild;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private System.Windows.Forms.ComboBox comboGender;
    }
}