namespace MTOMS
{
    partial class SabbathBulkPrinter
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
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.comboprinter = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboPaper = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.buttonstart = new DevComponents.DotNetBar.ButtonX();
            this.labelcnt = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.combodate = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.backworker = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.comboprinter);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.comboPaper);
            this.groupBox1.Controls.Add(this.buttonX2);
            this.groupBox1.Controls.Add(this.buttonstart);
            this.groupBox1.Controls.Add(this.labelcnt);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.combodate);
            this.groupBox1.Location = new System.Drawing.Point(3, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 196);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Blue;
            this.button1.Location = new System.Drawing.Point(351, 99);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 21);
            this.button1.TabIndex = 12;
            this.button1.Text = "PS";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Printer Name";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboprinter
            // 
            this.comboprinter.DisplayMember = "Text";
            this.comboprinter.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboprinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboprinter.Enabled = false;
            this.comboprinter.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboprinter.FormattingEnabled = true;
            this.comboprinter.ItemHeight = 16;
            this.comboprinter.Location = new System.Drawing.Point(88, 98);
            this.comboprinter.Name = "comboprinter";
            this.comboprinter.Size = new System.Drawing.Size(262, 22);
            this.comboprinter.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboprinter.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Paper Type";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(89, 128);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(206, 18);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Print Preview Before Printing";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // comboPaper
            // 
            this.comboPaper.DisplayMember = "Text";
            this.comboPaper.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboPaper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPaper.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboPaper.FormattingEnabled = true;
            this.comboPaper.ItemHeight = 16;
            this.comboPaper.Location = new System.Drawing.Point(88, 73);
            this.comboPaper.Name = "comboPaper";
            this.comboPaper.Size = new System.Drawing.Size(149, 22);
            this.comboPaper.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboPaper.TabIndex = 7;
            this.comboPaper.SelectedIndexChanged += new System.EventHandler(this.comboPaper_SelectedIndexChanged);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX2.ForeColor = System.Drawing.Color.Maroon;
            this.buttonX2.Location = new System.Drawing.Point(190, 154);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(151, 35);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 5;
            this.buttonX2.Text = "Reset";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // buttonstart
            // 
            this.buttonstart.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonstart.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonstart.Enabled = false;
            this.buttonstart.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonstart.ForeColor = System.Drawing.Color.Green;
            this.buttonstart.Location = new System.Drawing.Point(36, 154);
            this.buttonstart.Name = "buttonstart";
            this.buttonstart.Size = new System.Drawing.Size(151, 35);
            this.buttonstart.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonstart.TabIndex = 4;
            this.buttonstart.Text = "Start Print Job";
            this.buttonstart.Click += new System.EventHandler(this.buttonstart_Click);
            // 
            // labelcnt
            // 
            this.labelcnt.BackColor = System.Drawing.Color.LightGray;
            this.labelcnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelcnt.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelcnt.Location = new System.Drawing.Point(88, 42);
            this.labelcnt.Name = "labelcnt";
            this.labelcnt.Size = new System.Drawing.Size(61, 27);
            this.labelcnt.TabIndex = 3;
            this.labelcnt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "No Receipts";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sabbath Date";
            // 
            // combodate
            // 
            this.combodate.DisplayMember = "Text";
            this.combodate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combodate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combodate.Enabled = false;
            this.combodate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combodate.FormattingEnabled = true;
            this.combodate.ItemHeight = 16;
            this.combodate.Location = new System.Drawing.Point(87, 16);
            this.combodate.Name = "combodate";
            this.combodate.Size = new System.Drawing.Size(287, 22);
            this.combodate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.combodate.TabIndex = 0;
            this.combodate.SelectionChangeCommitted += new System.EventHandler(this.combodate_SelectionChangeCommitted);
            this.combodate.SelectedIndexChanged += new System.EventHandler(this.combodate_SelectedIndexChanged);
            // 
            // backworker
            // 
            this.backworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backworker_DoWork);
            this.backworker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backworker_RunWorkerCompleted);
            // 
            // SabbathBulkPrinter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 205);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SabbathBulkPrinter";
            this.Text = "Sabbath Receipt Printer (Bulk)";
            this.Load += new System.EventHandler(this.SabbathBulkPrinter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx combodate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelcnt;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.ButtonX buttonstart;
        private System.ComponentModel.BackgroundWorker backworker;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboPaper;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboprinter;
        private System.Windows.Forms.Button button1;
    }
}