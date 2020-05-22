namespace MTOMS
{
    partial class AccountsTransferMaker
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
            this.buttoncancel = new DevComponents.DotNetBar.ButtonX();
            this.buttoncreate = new DevComponents.DotNetBar.ButtonX();
            this.backworker = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.fGrid = new TenTec.Windows.iGridLib.iGrid();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // buttoncancel
            // 
            this.buttoncancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttoncancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttoncancel.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttoncancel.ForeColor = System.Drawing.Color.Maroon;
            this.buttoncancel.Location = new System.Drawing.Point(326, 363);
            this.buttoncancel.Name = "buttoncancel";
            this.buttoncancel.Size = new System.Drawing.Size(181, 36);
            this.buttoncancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttoncancel.TabIndex = 36;
            this.buttoncancel.Text = "Cancel";
            this.buttoncancel.Click += new System.EventHandler(this.buttoncancel_Click);
            // 
            // buttoncreate
            // 
            this.buttoncreate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttoncreate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttoncreate.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttoncreate.ForeColor = System.Drawing.Color.Green;
            this.buttoncreate.Location = new System.Drawing.Point(143, 363);
            this.buttoncreate.Name = "buttoncreate";
            this.buttoncreate.Size = new System.Drawing.Size(181, 36);
            this.buttoncreate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttoncreate.TabIndex = 35;
            this.buttoncreate.Text = "Transfer";
            this.buttoncreate.Click += new System.EventHandler(this.buttoncreate_Click);
            // 
            // backworker
            // 
            this.backworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backworker_DoWork);
            this.backworker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backworker_RunWorkerCompleted);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(651, 40);
            this.panel1.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(649, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = " Account Transfer Maker";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fGrid
            // 
            this.fGrid.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.fGrid.AutoHeightRowMode = TenTec.Windows.iGridLib.iGAutoHeightRowMode.Cells;
            this.fGrid.AutoResizeCols = true;
            this.fGrid.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.fGrid.BorderStyle = TenTec.Windows.iGridLib.iGBorderStyle.Flat;
            this.fGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.fGrid.EllipsisButtonGlyph = global::MTOMS.Properties.Resources.delete;
            this.fGrid.ForeColor = System.Drawing.SystemColors.WindowText;
            this.fGrid.Header.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.fGrid.Header.Height = 19;
            this.fGrid.Header.Visible = false;
            this.fGrid.Location = new System.Drawing.Point(0, 40);
            this.fGrid.Name = "fGrid";
            this.fGrid.RowMode = true;
            this.fGrid.RowSelectionInCellMode = TenTec.Windows.iGridLib.iGRowSelectionInCellModeTypes.SingleRow;
            this.fGrid.SelCellsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.fGrid.SelCellsForeColor = System.Drawing.Color.Black;
            this.fGrid.SilentValidation = true;
            this.fGrid.Size = new System.Drawing.Size(651, 317);
            this.fGrid.TabIndex = 39;
            this.fGrid.TreeCol = null;
            this.fGrid.TreeLines.Color = System.Drawing.SystemColors.WindowText;
            // 
            // AccountsTransferMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 403);
            this.Controls.Add(this.fGrid);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttoncancel);
            this.Controls.Add(this.buttoncreate);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountsTransferMaker";
            this.Text = "Accounts Transfer Maker";
            this.Load += new System.EventHandler(this.AccountsTransferMaker_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttoncancel;
        private DevComponents.DotNetBar.ButtonX buttoncreate;
        private System.ComponentModel.BackgroundWorker backworker;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private TenTec.Windows.iGridLib.iGrid fGrid;
    }
}