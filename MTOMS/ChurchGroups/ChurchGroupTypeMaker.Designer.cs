namespace MTOMS.ChurchGroups
{
    partial class ChurchGroupTypeMaker
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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.iGridCategory = new TenTec.Windows.iGridLib.iGrid();
            this.backworker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.iGridCategory)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DimGray;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(551, 36);
            this.label1.TabIndex = 5;
            this.label1.Text = "Church Group Types";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LightGray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(2, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(20, 27);
            this.button1.TabIndex = 21;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // iGridCategory
            // 
            this.iGridCategory.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGridCategory.AutoHeightRowMode = TenTec.Windows.iGridLib.iGAutoHeightRowMode.Cells;
            this.iGridCategory.AutoResizeCols = true;
            this.iGridCategory.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.iGridCategory.BorderStyle = TenTec.Windows.iGridLib.iGBorderStyle.Flat;
            this.iGridCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iGridCategory.ForeColor = System.Drawing.SystemColors.WindowText;
            this.iGridCategory.Header.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGridCategory.Header.Height = 19;
            this.iGridCategory.Location = new System.Drawing.Point(0, 36);
            this.iGridCategory.Name = "iGridCategory";
            this.iGridCategory.RowHeader.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGridCategory.RowHeader.Visible = true;
            this.iGridCategory.RowModeHasCurCell = true;
            this.iGridCategory.SelCellsBackColor = System.Drawing.Color.Moccasin;
            this.iGridCategory.SelCellsBackColorNoFocus = System.Drawing.Color.RosyBrown;
            this.iGridCategory.SelCellsForeColor = System.Drawing.Color.Black;
            this.iGridCategory.SilentValidation = true;
            this.iGridCategory.Size = new System.Drawing.Size(551, 470);
            this.iGridCategory.TabIndex = 20;
            this.iGridCategory.TreeCol = null;
            this.iGridCategory.TreeLines.Color = System.Drawing.SystemColors.WindowText;
            this.iGridCategory.VScrollBar.CustomButtons.AddRange(new TenTec.Windows.iGridLib.iGScrollBarCustomButton[] {
            new TenTec.Windows.iGridLib.iGScrollBarCustomButton(TenTec.Windows.iGridLib.iGScrollBarCustomButtonAlign.Near, TenTec.Windows.iGridLib.iGActions.SelectAllCells, -1, null, true, null),
            new TenTec.Windows.iGridLib.iGScrollBarCustomButton(TenTec.Windows.iGridLib.iGScrollBarCustomButtonAlign.Near, TenTec.Windows.iGridLib.iGActions.DeselectAllCells, -1, null, true, null)});
            this.iGridCategory.VScrollBar.Visibility = TenTec.Windows.iGridLib.iGScrollBarVisibility.Always;
            // 
            // ChurchGroupTypeMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 506);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.iGridCategory);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChurchGroupTypeMaker";
            this.Text = "ChurchGroupTypeMaker";
            this.Load += new System.EventHandler(this.ChurchGroupTypeMaker_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iGridCategory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private TenTec.Windows.iGridLib.iGrid iGridCategory;
        private System.ComponentModel.BackgroundWorker backworker;
    }
}