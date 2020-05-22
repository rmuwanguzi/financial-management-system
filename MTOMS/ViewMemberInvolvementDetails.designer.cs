namespace MTOMS
{
    partial class ViewMemberInvolvementDetails
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.printReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backworker = new System.ComponentModel.BackgroundWorker();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.combogender = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.label2 = new System.Windows.Forms.Label();
            this.switchButton1 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
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
            this.labelTopName.Size = new System.Drawing.Size(1078, 48);
            this.labelTopName.TabIndex = 2;
            this.labelTopName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fGrid
            // 
            this.fGrid.BorderStyle = TenTec.Windows.iGridLib.iGBorderStyle.None;
            this.fGrid.ContextMenuStrip = this.contextMenuStrip1;
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
            this.fGrid.Location = new System.Drawing.Point(0, 48);
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
            this.fGrid.Size = new System.Drawing.Size(1078, 540);
            this.fGrid.TabIndex = 13;
            this.fGrid.TreeCol = null;
            this.fGrid.TreeLines.Color = System.Drawing.SystemColors.WindowText;
            this.fGrid.VScrollBar.CustomButtons.AddRange(new TenTec.Windows.iGridLib.iGScrollBarCustomButton[] {
            new TenTec.Windows.iGridLib.iGScrollBarCustomButton(TenTec.Windows.iGridLib.iGScrollBarCustomButtonAlign.Far, TenTec.Windows.iGridLib.iGActions.CollapseAll, -1, "Collapse All", true, null),
            new TenTec.Windows.iGridLib.iGScrollBarCustomButton(TenTec.Windows.iGridLib.iGScrollBarCustomButtonAlign.Far, TenTec.Windows.iGridLib.iGActions.ExpandAll, -1, "Expand All", true, null)});
            this.fGrid.VScrollBar.Visibility = TenTec.Windows.iGridLib.iGScrollBarVisibility.Always;
            this.fGrid.AfterContentsSorted += new System.EventHandler(this.fGrid_AfterContentsSorted);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printReportToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(150, 26);
            // 
            // printReportToolStripMenuItem
            // 
            this.printReportToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printReportToolStripMenuItem.ForeColor = System.Drawing.Color.MediumBlue;
            this.printReportToolStripMenuItem.Image = global::MTOMS.Properties.Resources.printer1;
            this.printReportToolStripMenuItem.Name = "printReportToolStripMenuItem";
            this.printReportToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.printReportToolStripMenuItem.Text = "Print Report";
            this.printReportToolStripMenuItem.Click += new System.EventHandler(this.printReportToolStripMenuItem_Click);
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
            this.button1.Location = new System.Drawing.Point(4, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 26);
            this.button1.TabIndex = 170;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(42, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 36);
            this.label1.TabIndex = 171;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // combogender
            // 
            this.combogender.DisplayMember = "Text";
            this.combogender.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.combogender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combogender.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combogender.FormattingEnabled = true;
            this.combogender.ItemHeight = 16;
            this.combogender.Location = new System.Drawing.Point(987, 23);
            this.combogender.Name = "combogender";
            this.combogender.Size = new System.Drawing.Size(87, 22);
            this.combogender.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.combogender.TabIndex = 172;
            this.combogender.WatermarkText = "Gender";
            this.combogender.SelectedIndexChanged += new System.EventHandler(this.combogender_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Gold;
            this.label2.Location = new System.Drawing.Point(990, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 173;
            this.label2.Text = "Gender Filter";
            // 
            // switchButton1
            // 
            // 
            // 
            // 
            this.switchButton1.BackgroundStyle.Class = "";
            this.switchButton1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton1.Location = new System.Drawing.Point(904, 23);
            this.switchButton1.Name = "switchButton1";
            this.switchButton1.Size = new System.Drawing.Size(78, 22);
            this.switchButton1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton1.TabIndex = 174;
            this.switchButton1.ValueChanged += new System.EventHandler(this.switchButton1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Gold;
            this.label3.Location = new System.Drawing.Point(905, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 175;
            this.label3.Text = "Show Detail";
            // 
            // ViewMemberInvolvementDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 588);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.switchButton1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.combogender);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fGrid);
            this.Controls.Add(this.labelTopName);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewMemberInvolvementDetails";
            this.Text = "Member Involvement Details";
            this.Load += new System.EventHandler(this.ViewMemberInvolvementDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTopName;
        private TenTec.Windows.iGridLib.iGrid fGrid;
        private System.ComponentModel.BackgroundWorker backworker;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem printReportToolStripMenuItem;
        private DevComponents.DotNetBar.Controls.ComboBoxEx combogender;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton1;
        private System.Windows.Forms.Label label3;
    }
}