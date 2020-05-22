namespace MTOMS
{
    partial class NeighbourMaker
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
            this.buttoncanc = new DevComponents.DotNetBar.ButtonX();
            this.buttonsave = new DevComponents.DotNetBar.ButtonX();
            this.txt_neigh_phone = new SdaHelperManager.CustomTextBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txt_neigh_name = new SdaHelperManager.CustomTextBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.buttoncanc);
            this.groupBox1.Controls.Add(this.buttonsave);
            this.groupBox1.Controls.Add(this.txt_neigh_phone);
            this.groupBox1.Controls.Add(this.labelX2);
            this.groupBox1.Controls.Add(this.txt_neigh_name);
            this.groupBox1.Controls.Add(this.labelX1);
            this.groupBox1.Location = new System.Drawing.Point(1, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 106);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // buttoncanc
            // 
            this.buttoncanc.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttoncanc.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttoncanc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttoncanc.ForeColor = System.Drawing.Color.Maroon;
            this.buttoncanc.Location = new System.Drawing.Point(185, 69);
            this.buttoncanc.Name = "buttoncanc";
            this.buttoncanc.Size = new System.Drawing.Size(140, 33);
            this.buttoncanc.TabIndex = 95;
            this.buttoncanc.Text = "Cancel";
            // 
            // buttonsave
            // 
            this.buttonsave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonsave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonsave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonsave.ForeColor = System.Drawing.Color.ForestGreen;
            this.buttonsave.Location = new System.Drawing.Point(33, 69);
            this.buttonsave.Name = "buttonsave";
            this.buttonsave.Size = new System.Drawing.Size(150, 33);
            this.buttonsave.TabIndex = 94;
            this.buttonsave.Text = "Add To List";
            this.buttonsave.Click += new System.EventHandler(this.buttonsave_Click);
            // 
            // txt_neigh_phone
            // 
            this.txt_neigh_phone.AllowCOPY = false;
            this.txt_neigh_phone.AllowCUT = false;
            this.txt_neigh_phone.AllowDotCharacter = false;
            this.txt_neigh_phone.AllowEnterCharacter = false;
            this.txt_neigh_phone.AllowPASTE = false;
            this.txt_neigh_phone.AllowSpaceCharacter = false;
            // 
            // 
            // 
            this.txt_neigh_phone.Border.Class = "TextBoxBorder";
            this.txt_neigh_phone.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_neigh_phone.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_neigh_phone.Location = new System.Drawing.Point(70, 39);
            this.txt_neigh_phone.MaxLength = 10;
            this.txt_neigh_phone.Name = "txt_neigh_phone";
            this.txt_neigh_phone.ShowDefaultContextMenu = true;
            this.txt_neigh_phone.Size = new System.Drawing.Size(187, 21);
            this.txt_neigh_phone.TabIndex = 3;
            this.txt_neigh_phone.TextBoxType = SdaHelperManager.CustomTextBoxEx.m_TextBoxType.AlphabetAndDigitsOnly;
            this.txt_neigh_phone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_neigh_phone_KeyDown);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.Location = new System.Drawing.Point(25, 43);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(42, 12);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "Phone";
            // 
            // txt_neigh_name
            // 
            this.txt_neigh_name.AllowCOPY = false;
            this.txt_neigh_name.AllowCUT = false;
            this.txt_neigh_name.AllowDotCharacter = false;
            this.txt_neigh_name.AllowEnterCharacter = false;
            this.txt_neigh_name.AllowPASTE = false;
            this.txt_neigh_name.AllowSpaceCharacter = false;
            // 
            // 
            // 
            this.txt_neigh_name.Border.Class = "TextBoxBorder";
            this.txt_neigh_name.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_neigh_name.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_neigh_name.Location = new System.Drawing.Point(70, 16);
            this.txt_neigh_name.Name = "txt_neigh_name";
            this.txt_neigh_name.ShowDefaultContextMenu = true;
            this.txt_neigh_name.Size = new System.Drawing.Size(280, 21);
            this.txt_neigh_name.TabIndex = 1;
            this.txt_neigh_name.TextBoxType = SdaHelperManager.CustomTextBoxEx.m_TextBoxType.All;
            this.txt_neigh_name.TextChanged += new System.EventHandler(this.txt_neigh_name_TextChanged);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.Location = new System.Drawing.Point(15, 21);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(52, 12);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "N Name";
            // 
            // NeighbourMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 110);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NeighbourMaker";
            this.Text = "Neighbour Details";
            this.Load += new System.EventHandler(this.NeighbourMaker_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private SdaHelperManager.CustomTextBoxEx txt_neigh_phone;
        private DevComponents.DotNetBar.LabelX labelX2;
        private SdaHelperManager.CustomTextBoxEx txt_neigh_name;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX buttoncanc;
        private DevComponents.DotNetBar.ButtonX buttonsave;
    }
}