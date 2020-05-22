namespace MTOMS
{
    partial class TransferForm
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
            this.buttonsave = new DevComponents.DotNetBar.ButtonX();
            this.fGrid = new TenTec.Windows.iGridLib.iGrid();
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // buttoncancel
            // 
            this.buttoncancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttoncancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttoncancel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttoncancel.ForeColor = System.Drawing.Color.Maroon;
            this.buttoncancel.Location = new System.Drawing.Point(284, 388);
            this.buttoncancel.Name = "buttoncancel";
            this.buttoncancel.Size = new System.Drawing.Size(167, 28);
            this.buttoncancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttoncancel.TabIndex = 173;
            this.buttoncancel.Text = "Cancel Changes";
            this.buttoncancel.Click += new System.EventHandler(this.buttoncancel_Click);
            // 
            // buttonsave
            // 
            this.buttonsave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonsave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonsave.Enabled = false;
            this.buttonsave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonsave.ForeColor = System.Drawing.Color.Green;
            this.buttonsave.Location = new System.Drawing.Point(115, 388);
            this.buttonsave.Name = "buttonsave";
            this.buttonsave.Size = new System.Drawing.Size(167, 28);
            this.buttonsave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonsave.TabIndex = 172;
            this.buttonsave.Text = "Save Changes";
            this.buttonsave.Click += new System.EventHandler(this.buttonsave_Click);
            // 
            // fGrid
            // 
            this.fGrid.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.fGrid.AutoHeightRowMode = TenTec.Windows.iGridLib.iGAutoHeightRowMode.Cells;
            this.fGrid.AutoResizeCols = true;
            this.fGrid.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.fGrid.BorderStyle = TenTec.Windows.iGridLib.iGBorderStyle.Flat;
            this.fGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.fGrid.ForeColor = System.Drawing.SystemColors.WindowText;
            this.fGrid.Header.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.fGrid.Header.Height = 19;
            this.fGrid.Header.Visible = false;
            this.fGrid.Location = new System.Drawing.Point(0, 0);
            this.fGrid.Name = "fGrid";
            this.fGrid.RowMode = true;
            this.fGrid.RowSelectionInCellMode = TenTec.Windows.iGridLib.iGRowSelectionInCellModeTypes.SingleRow;
            this.fGrid.SelCellsBackColor = System.Drawing.Color.Gainsboro;
            this.fGrid.SelCellsForeColor = System.Drawing.Color.Black;
            this.fGrid.SilentValidation = true;
            this.fGrid.Size = new System.Drawing.Size(567, 382);
            this.fGrid.TabIndex = 171;
            this.fGrid.TreeCol = null;
            this.fGrid.TreeLines.Color = System.Drawing.SystemColors.WindowText;
            this.fGrid.AfterCommitEdit += new TenTec.Windows.iGridLib.iGAfterCommitEditEventHandler(this.fGrid_AfterCommitEdit);
            this.fGrid.CustomDrawCellForeground += new TenTec.Windows.iGridLib.iGCustomDrawCellEventHandler(this.fGrid_CustomDrawCellForeground);
            this.fGrid.EllipsisButtonClick += new TenTec.Windows.iGridLib.iGEllipsisButtonClickEventHandler(this.fGrid_EllipsisButtonClick);
            // 
            // TransferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 418);
            this.Controls.Add(this.buttoncancel);
            this.Controls.Add(this.buttonsave);
            this.Controls.Add(this.fGrid);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransferForm";
            this.Text = "Member Transfer Form";
            this.Load += new System.EventHandler(this.TransferForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttoncancel;
        private DevComponents.DotNetBar.ButtonX buttonsave;
        private TenTec.Windows.iGridLib.iGrid fGrid;
    }
}