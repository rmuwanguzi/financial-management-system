namespace MTOMS
{
    partial class ViewSingleSabbathAccountDetails
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
            TenTec.Windows.iGridLib.iGRowPattern iGRowPattern1 = new TenTec.Windows.iGridLib.iGRowPattern();
            TenTec.Windows.iGridLib.iGCellPattern iGCellPattern1 = new TenTec.Windows.iGridLib.iGCellPattern();
            this.labelTopName = new System.Windows.Forms.Label();
            this.fGrid = new TenTec.Windows.iGridLib.iGrid();
            this.backworker = new System.ComponentModel.BackgroundWorker();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStripReplace = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).BeginInit();
            this.contextMenuStripReplace.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelTopName
            // 
            this.labelTopName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelTopName.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelTopName.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTopName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelTopName.Location = new System.Drawing.Point(0, 0);
            this.labelTopName.Name = "labelTopName";
            this.labelTopName.Size = new System.Drawing.Size(936, 37);
            this.labelTopName.TabIndex = 2;
            this.labelTopName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fGrid
            // 
            this.fGrid.BorderStyle = TenTec.Windows.iGridLib.iGBorderStyle.None;
            this.fGrid.ContextMenuStrip = this.contextMenuStripReplace;
            this.fGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fGrid.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fGrid.ForeColor = System.Drawing.SystemColors.WindowText;
            this.fGrid.FrozenArea.ColCount = 1;
            this.fGrid.FrozenArea.ColsEdge = new TenTec.Windows.iGridLib.iGPenStyle(System.Drawing.SystemColors.ControlDark, 1, System.Drawing.Drawing2D.DashStyle.Solid);
            this.fGrid.GridLines.Horizontal = new TenTec.Windows.iGridLib.iGPenStyle(System.Drawing.Color.Gainsboro, 1, System.Drawing.Drawing2D.DashStyle.Solid);
            this.fGrid.GridLines.Vertical = new TenTec.Windows.iGridLib.iGPenStyle(System.Drawing.Color.Gainsboro, 1, System.Drawing.Drawing2D.DashStyle.Solid);
            this.fGrid.Header.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.fGrid.Header.Height = 22;
            this.fGrid.Header.HGridLinesStyle = new TenTec.Windows.iGridLib.iGPenStyle(System.Drawing.SystemColors.ControlDark, 1, System.Drawing.Drawing2D.DashStyle.Solid);
            this.fGrid.Location = new System.Drawing.Point(0, 37);
            this.fGrid.Name = "fGrid";
            this.fGrid.RowHeader.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.fGrid.RowHeader.Visible = true;
            this.fGrid.RowMode = true;
            this.fGrid.RowResizeMode = TenTec.Windows.iGridLib.iGRowResizeMode.NotAllowed;
            this.fGrid.Rows.AddRange(new TenTec.Windows.iGridLib.iGRowPattern[] {
            iGRowPattern1}, new TenTec.Windows.iGridLib.iGCellPattern[] {
            iGCellPattern1});
            this.fGrid.RowSelectionInCellMode = TenTec.Windows.iGridLib.iGRowSelectionInCellModeTypes.SingleRow;
            this.fGrid.RowTextStartColNear = 1;
            this.fGrid.ShowControlsInAllCells = false;
            this.fGrid.SilentValidation = true;
            this.fGrid.Size = new System.Drawing.Size(936, 551);
            this.fGrid.TabIndex = 13;
            this.fGrid.TreeCol = null;
            this.fGrid.TreeLines.Color = System.Drawing.SystemColors.WindowText;
            this.fGrid.VScrollBar.CustomButtons.AddRange(new TenTec.Windows.iGridLib.iGScrollBarCustomButton[] {
            new TenTec.Windows.iGridLib.iGScrollBarCustomButton(TenTec.Windows.iGridLib.iGScrollBarCustomButtonAlign.Far, TenTec.Windows.iGridLib.iGActions.CollapseAll, -1, "Collapse All", true, null),
            new TenTec.Windows.iGridLib.iGScrollBarCustomButton(TenTec.Windows.iGridLib.iGScrollBarCustomButtonAlign.Far, TenTec.Windows.iGridLib.iGActions.ExpandAll, -1, "Expand All", true, null)});
            this.fGrid.VScrollBar.Visibility = TenTec.Windows.iGridLib.iGScrollBarVisibility.Always;
            this.fGrid.AfterContentsSorted += new System.EventHandler(this.fGrid_AfterContentsSorted);
            // 
            // backworker
            // 
            this.backworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backworker_DoWork);
            this.backworker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backworker_RunWorkerCompleted);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.LightGray;
            this.button1.Image = global::MTOMS.Properties.Resources.printer1;
            this.button1.Location = new System.Drawing.Point(4, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 26);
            this.button1.TabIndex = 170;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // contextMenuStripReplace
            // 
            this.contextMenuStripReplace.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2});
            this.contextMenuStripReplace.Name = "contextMenuStrip1";
            this.contextMenuStripReplace.Size = new System.Drawing.Size(190, 50);
            this.contextMenuStripReplace.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripReplace_Opening);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem2.ForeColor = System.Drawing.Color.Blue;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(189, 24);
            this.toolStripMenuItem2.Text = "Replace Account";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // ViewSingleSabbathAccountDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 588);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fGrid);
            this.Controls.Add(this.labelTopName);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewSingleSabbathAccountDetails";
            this.Text = "Income Report";
            this.Load += new System.EventHandler(this.ViewSingleSabbathAccountDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).EndInit();
            this.contextMenuStripReplace.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelTopName;
        private TenTec.Windows.iGridLib.iGrid fGrid;
        private System.ComponentModel.BackgroundWorker backworker;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripReplace;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    }
}