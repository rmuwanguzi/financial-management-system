namespace MTOMS
{
    partial class ExtendAccountMaker
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
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.fGrid = new TenTec.Windows.iGridLib.iGrid();
            this.backworker = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX2.ForeColor = System.Drawing.Color.Maroon;
            this.buttonX2.Location = new System.Drawing.Point(335, 506);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(181, 36);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 24;
            this.buttonX2.Text = "Reset";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX1.ForeColor = System.Drawing.Color.Green;
            this.buttonX1.Location = new System.Drawing.Point(152, 506);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(181, 36);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 23;
            this.buttonX1.Text = "Extend Account";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
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
            this.fGrid.Location = new System.Drawing.Point(0, 0);
            this.fGrid.Name = "fGrid";
            this.fGrid.RowMode = true;
            this.fGrid.RowSelectionInCellMode = TenTec.Windows.iGridLib.iGRowSelectionInCellModeTypes.SingleRow;
            this.fGrid.SelCellsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.fGrid.SelCellsForeColor = System.Drawing.Color.Black;
            this.fGrid.SilentValidation = true;
            this.fGrid.Size = new System.Drawing.Size(674, 500);
            this.fGrid.TabIndex = 25;
            this.fGrid.TreeCol = null;
            this.fGrid.TreeLines.Color = System.Drawing.SystemColors.WindowText;
            this.fGrid.EllipsisButtonClick += new TenTec.Windows.iGridLib.iGEllipsisButtonClickEventHandler(this.fGrid_EllipsisButtonClick);
            this.fGrid.AfterCommitEdit += new TenTec.Windows.iGridLib.iGAfterCommitEditEventHandler(this.fGrid_AfterCommitEdit);
            this.fGrid.Click += new System.EventHandler(this.fGrid_Click);
            // 
            // backworker
            // 
            this.backworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backworker_DoWork);
            this.backworker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backworker_RunWorkerCompleted);
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            // 
            // ExtendAccountMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 544);
            this.Controls.Add(this.fGrid);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExtendAccountMaker";
            this.Text = "Extend Account Maker";
            this.Load += new System.EventHandler(this.ExtendAccountMaker_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private TenTec.Windows.iGridLib.iGrid fGrid;
        private System.ComponentModel.BackgroundWorker backworker;
        private System.Windows.Forms.Timer timer1;
    }
}