namespace MTOMS.sms
{
    partial class ComposeSms
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
            this.loadingCircle1 = new MTOMS.LoadingCircle();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelmsgcnt2 = new System.Windows.Forms.Label();
            this.labelcharacter = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelPhoneCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxMsg = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSender = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.buttonsave = new DevComponents.DotNetBar.ButtonX();
            this.buttoncancel = new DevComponents.DotNetBar.ButtonX();
            this.backworker = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.loadingCircle1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.labelmsgcnt2);
            this.groupBox1.Controls.Add(this.labelcharacter);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.labelPhoneCount);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxMsg);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBoxSender);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(1, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(507, 353);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SMS Controller";
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.BackColor = System.Drawing.Color.DimGray;
            this.loadingCircle1.Color = System.Drawing.SystemColors.Control;
            this.loadingCircle1.InnerCircleRadius = 5;
            this.loadingCircle1.Location = new System.Drawing.Point(241, 157);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 12;
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(40, 29);
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.StylePreset = MTOMS.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.TabIndex = 13;
            this.loadingCircle1.Text = "loadingCircle1";
            this.loadingCircle1.Visible = false;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Maroon;
            this.label8.Location = new System.Drawing.Point(198, 329);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 21);
            this.label8.TabIndex = 12;
            this.label8.Text = "0";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Maroon;
            this.label7.Location = new System.Drawing.Point(198, 310);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(117, 21);
            this.label7.TabIndex = 11;
            this.label7.Text = "0";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Maroon;
            this.label5.Location = new System.Drawing.Point(199, 292);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 21);
            this.label5.TabIndex = 10;
            this.label5.Text = "0";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelmsgcnt2
            // 
            this.labelmsgcnt2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.labelmsgcnt2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelmsgcnt2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelmsgcnt2.Location = new System.Drawing.Point(426, 298);
            this.labelmsgcnt2.Name = "labelmsgcnt2";
            this.labelmsgcnt2.Size = new System.Drawing.Size(53, 21);
            this.labelmsgcnt2.TabIndex = 8;
            this.labelmsgcnt2.Text = "1";
            this.labelmsgcnt2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelcharacter
            // 
            this.labelcharacter.BackColor = System.Drawing.Color.WhiteSmoke;
            this.labelcharacter.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelcharacter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelcharacter.Location = new System.Drawing.Point(407, 320);
            this.labelcharacter.Name = "labelcharacter";
            this.labelcharacter.Size = new System.Drawing.Size(85, 21);
            this.labelcharacter.TabIndex = 9;
            this.labelcharacter.Text = "120/199";
            this.labelcharacter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Maroon;
            this.label6.Location = new System.Drawing.Point(74, 330);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 18);
            this.label6.TabIndex = 7;
            this.label6.Text = "Total SMS :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPhoneCount
            // 
            this.labelPhoneCount.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPhoneCount.ForeColor = System.Drawing.Color.Maroon;
            this.labelPhoneCount.Location = new System.Drawing.Point(66, 312);
            this.labelPhoneCount.Name = "labelPhoneCount";
            this.labelPhoneCount.Size = new System.Drawing.Size(133, 17);
            this.labelPhoneCount.TabIndex = 6;
            this.labelPhoneCount.Text = "Client Phone List :";
            this.labelPhoneCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Maroon;
            this.label4.Location = new System.Drawing.Point(69, 291);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 21);
            this.label4.TabIndex = 5;
            this.label4.Text = "Message Count :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxMsg
            // 
            // 
            // 
            // 
            this.textBoxMsg.Border.Class = "TextBoxBorder";
            this.textBoxMsg.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxMsg.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMsg.Location = new System.Drawing.Point(78, 56);
            this.textBoxMsg.MaxLength = 645;
            this.textBoxMsg.Multiline = true;
            this.textBoxMsg.Name = "textBoxMsg";
            this.textBoxMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMsg.Size = new System.Drawing.Size(422, 234);
            this.textBoxMsg.TabIndex = 4;
            this.textBoxMsg.Text = "mtoms sevices ltm ffff";
            this.textBoxMsg.TextChanged += new System.EventHandler(this.textBoxX2_TextChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Maroon;
            this.label3.Location = new System.Drawing.Point(312, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 21);
            this.label3.TabIndex = 3;
            this.label3.Text = "(Max 11 Characters)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(6, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Message";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(7, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Heading";
            // 
            // textBoxSender
            // 
            // 
            // 
            // 
            this.textBoxSender.Border.Class = "TextBoxBorder";
            this.textBoxSender.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxSender.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSender.Location = new System.Drawing.Point(78, 20);
            this.textBoxSender.MaxLength = 11;
            this.textBoxSender.Name = "textBoxSender";
            this.textBoxSender.Size = new System.Drawing.Size(232, 29);
            this.textBoxSender.TabIndex = 0;
            this.textBoxSender.Text = "mtoms sevices ltm ffff";
            this.textBoxSender.TextChanged += new System.EventHandler(this.textBoxSender_TextChanged);
            // 
            // buttonsave
            // 
            this.buttonsave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonsave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonsave.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonsave.ForeColor = System.Drawing.Color.Green;
            this.buttonsave.Location = new System.Drawing.Point(13, 364);
            this.buttonsave.Name = "buttonsave";
            this.buttonsave.Size = new System.Drawing.Size(241, 47);
            this.buttonsave.TabIndex = 221;
            this.buttonsave.Text = "Send SMS";
            this.buttonsave.Click += new System.EventHandler(this.buttonsave_Click);
            // 
            // buttoncancel
            // 
            this.buttoncancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttoncancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttoncancel.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttoncancel.ForeColor = System.Drawing.Color.Maroon;
            this.buttoncancel.Location = new System.Drawing.Point(255, 364);
            this.buttoncancel.Name = "buttoncancel";
            this.buttoncancel.Size = new System.Drawing.Size(241, 47);
            this.buttoncancel.TabIndex = 220;
            this.buttoncancel.Text = "Cancel";
            this.buttoncancel.Click += new System.EventHandler(this.buttoncancel_Click);
            // 
            // backworker
            // 
            this.backworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backworker_DoWork);
            this.backworker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backworker_RunWorkerCompleted);
            // 
            // ComposeSms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 415);
            this.Controls.Add(this.buttonsave);
            this.Controls.Add(this.buttoncancel);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ComposeSms";
            this.ShowInTaskbar = false;
            this.Text = "Compose Sms";
            this.Load += new System.EventHandler(this.ComposeSms_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxSender;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxMsg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelPhoneCount;
        private System.Windows.Forms.Label labelcharacter;
        private System.Windows.Forms.Label labelmsgcnt2;
        private DevComponents.DotNetBar.ButtonX buttonsave;
        private DevComponents.DotNetBar.ButtonX buttoncancel;
        private System.ComponentModel.BackgroundWorker backworker;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private MTOMS.LoadingCircle loadingCircle1;
    }
}