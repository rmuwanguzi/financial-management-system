namespace MTOMS
{
    partial class MemberStatistics
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
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.iGrid1 = new TenTec.Windows.iGridLib.iGrid();
            this.fStyleGroup = new TenTec.Windows.iGridLib.iGCellStyleDesign();
            this.iGrid2 = new TenTec.Windows.iGridLib.iGrid();
            this.iGrid3 = new TenTec.Windows.iGridLib.iGrid();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iGrid2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iGrid3)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.buttonX1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(864, 47);
            this.panel1.TabIndex = 0;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX1.Location = new System.Drawing.Point(4, 5);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(135, 38);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 0;
            this.buttonX1.Text = "Refresh";
            // 
            // iGrid1
            // 
            this.iGrid1.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGrid1.AutoHeightRowMode = TenTec.Windows.iGridLib.iGAutoHeightRowMode.Cells;
            this.iGrid1.AutoResizeCols = true;
            this.iGrid1.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.iGrid1.BorderStyle = TenTec.Windows.iGridLib.iGBorderStyle.Flat;
            this.iGrid1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.iGrid1.GroupRowLevelStyles = new TenTec.Windows.iGridLib.iGCellStyle[] {
        ((TenTec.Windows.iGridLib.iGCellStyle)(this.fStyleGroup))};
            this.iGrid1.Header.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGrid1.Header.Height = 19;
            this.iGrid1.Header.Visible = false;
            this.iGrid1.Location = new System.Drawing.Point(25, 69);
            this.iGrid1.Name = "iGrid1";
            this.iGrid1.RowMode = true;
            this.iGrid1.RowSelectionInCellMode = TenTec.Windows.iGridLib.iGRowSelectionInCellModeTypes.SingleRow;
            this.iGrid1.SelRowsBackColor = System.Drawing.Color.PapayaWhip;
            this.iGrid1.SelRowsBackColorNoFocus = System.Drawing.Color.Transparent;
            this.iGrid1.SelRowsForeColor = System.Drawing.Color.Black;
            this.iGrid1.SilentValidation = true;
            this.iGrid1.Size = new System.Drawing.Size(261, 371);
            this.iGrid1.TabIndex = 163;
            this.iGrid1.TreeCol = null;
            this.iGrid1.TreeLines.Color = System.Drawing.SystemColors.WindowText;
            this.iGrid1.VScrollBar.Visibility = TenTec.Windows.iGridLib.iGScrollBarVisibility.Always;
            // 
            // fStyleGroup
            // 
            this.fStyleGroup.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.fStyleGroup.CustomDrawFlags = TenTec.Windows.iGridLib.iGCustomDrawFlags.None;
            this.fStyleGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fStyleGroup.ForeColor = System.Drawing.Color.Black;
            // 
            // iGrid2
            // 
            this.iGrid2.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGrid2.AutoHeightRowMode = TenTec.Windows.iGridLib.iGAutoHeightRowMode.Cells;
            this.iGrid2.AutoResizeCols = true;
            this.iGrid2.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.iGrid2.BorderStyle = TenTec.Windows.iGridLib.iGBorderStyle.Flat;
            this.iGrid2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.iGrid2.GroupRowLevelStyles = new TenTec.Windows.iGridLib.iGCellStyle[] {
        ((TenTec.Windows.iGridLib.iGCellStyle)(this.fStyleGroup))};
            this.iGrid2.Header.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGrid2.Header.Height = 19;
            this.iGrid2.Header.Visible = false;
            this.iGrid2.Location = new System.Drawing.Point(305, 69);
            this.iGrid2.Name = "iGrid2";
            this.iGrid2.RowMode = true;
            this.iGrid2.RowSelectionInCellMode = TenTec.Windows.iGridLib.iGRowSelectionInCellModeTypes.SingleRow;
            this.iGrid2.SelRowsBackColor = System.Drawing.Color.PapayaWhip;
            this.iGrid2.SelRowsBackColorNoFocus = System.Drawing.Color.Transparent;
            this.iGrid2.SelRowsForeColor = System.Drawing.Color.Black;
            this.iGrid2.SilentValidation = true;
            this.iGrid2.Size = new System.Drawing.Size(284, 364);
            this.iGrid2.TabIndex = 166;
            this.iGrid2.TreeCol = null;
            this.iGrid2.TreeLines.Color = System.Drawing.SystemColors.WindowText;
            this.iGrid2.VScrollBar.Visibility = TenTec.Windows.iGridLib.iGScrollBarVisibility.Always;
            // 
            // iGrid3
            // 
            this.iGrid3.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGrid3.AutoHeightRowMode = TenTec.Windows.iGridLib.iGAutoHeightRowMode.Cells;
            this.iGrid3.AutoResizeCols = true;
            this.iGrid3.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.iGrid3.BorderStyle = TenTec.Windows.iGridLib.iGBorderStyle.Flat;
            this.iGrid3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.iGrid3.GroupRowLevelStyles = new TenTec.Windows.iGridLib.iGCellStyle[] {
        ((TenTec.Windows.iGridLib.iGCellStyle)(this.fStyleGroup))};
            this.iGrid3.Header.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGrid3.Header.Height = 19;
            this.iGrid3.Header.Visible = false;
            this.iGrid3.HScrollBar.Visibility = TenTec.Windows.iGridLib.iGScrollBarVisibility.Hide;
            this.iGrid3.Location = new System.Drawing.Point(595, 69);
            this.iGrid3.Name = "iGrid3";
            this.iGrid3.RowMode = true;
            this.iGrid3.RowSelectionInCellMode = TenTec.Windows.iGridLib.iGRowSelectionInCellModeTypes.SingleRow;
            this.iGrid3.SelRowsBackColor = System.Drawing.Color.PapayaWhip;
            this.iGrid3.SelRowsBackColorNoFocus = System.Drawing.Color.Transparent;
            this.iGrid3.SelRowsForeColor = System.Drawing.Color.Black;
            this.iGrid3.SilentValidation = true;
            this.iGrid3.Size = new System.Drawing.Size(246, 364);
            this.iGrid3.TabIndex = 167;
            this.iGrid3.TreeCol = null;
            this.iGrid3.TreeLines.Color = System.Drawing.SystemColors.WindowText;
            this.iGrid3.VScrollBar.Visibility = TenTec.Windows.iGridLib.iGScrollBarVisibility.Always;
            // 
            // MemberStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 485);
            this.Controls.Add(this.iGrid3);
            this.Controls.Add(this.iGrid2);
            this.Controls.Add(this.iGrid1);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "MemberStatistics";
            this.Text = "MemberStatistics";
            this.Load += new System.EventHandler(this.MemberStatistics_Load);
            this.SizeChanged += new System.EventHandler(this.MemberStatistics_SizeChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iGrid2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iGrid3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private TenTec.Windows.iGridLib.iGrid iGrid1;
        private TenTec.Windows.iGridLib.iGrid iGrid2;
        private TenTec.Windows.iGridLib.iGrid iGrid3;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private TenTec.Windows.iGridLib.iGCellStyleDesign fStyleGroup;
    }
}