namespace MTOMS
{
    partial class BatchNoManager
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
            this.pn_dis = new DevComponents.DotNetBar.PanelEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.comboDate = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.buttonAdd = new DevComponents.DotNetBar.ButtonX();
            this.btnadd = new DevComponents.DotNetBar.ButtonItem();
            this.BtnRefresh = new DevComponents.DotNetBar.ButtonItem();
            this.fGrid = new TenTec.Windows.iGridLib.iGrid();
            this.backworker = new System.ComponentModel.BackgroundWorker();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pn_dis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pn_dis
            // 
            this.pn_dis.CanvasColor = System.Drawing.Color.WhiteSmoke;
            this.pn_dis.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Windows7;
            this.pn_dis.Controls.Add(this.labelX1);
            this.pn_dis.Controls.Add(this.comboDate);
            this.pn_dis.Controls.Add(this.buttonAdd);
            this.pn_dis.Dock = System.Windows.Forms.DockStyle.Top;
            this.pn_dis.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pn_dis.Location = new System.Drawing.Point(0, 0);
            this.pn_dis.Name = "pn_dis";
            this.pn_dis.Size = new System.Drawing.Size(657, 42);
            this.pn_dis.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pn_dis.Style.BackColor1.Color = System.Drawing.Color.LightGray;
            this.pn_dis.Style.BackColor2.Color = System.Drawing.Color.Beige;
            this.pn_dis.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pn_dis.Style.BorderColor.Color = System.Drawing.SystemColors.ControlDark;
            this.pn_dis.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pn_dis.Style.GradientAngle = 90;
            this.pn_dis.TabIndex = 9;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.Location = new System.Drawing.Point(96, 11);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(102, 21);
            this.labelX1.TabIndex = 171;
            this.labelX1.Text = "Sabbath Date";
            // 
            // comboDate
            // 
            this.comboDate.DisplayMember = "Text";
            this.comboDate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDate.Enabled = false;
            this.comboDate.Font = new System.Drawing.Font("Verdana", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboDate.ForeColor = System.Drawing.Color.Green;
            this.comboDate.FormattingEnabled = true;
            this.comboDate.ItemHeight = 22;
            this.comboDate.Location = new System.Drawing.Point(199, 8);
            this.comboDate.Name = "comboDate";
            this.comboDate.Size = new System.Drawing.Size(227, 28);
            this.comboDate.TabIndex = 170;
            this.comboDate.SelectedIndexChanged += new System.EventHandler(this.comboDate_SelectedIndexChanged);
            // 
            // buttonAdd
            // 
            this.buttonAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonAdd.AutoExpandOnClick = true;
            this.buttonAdd.BackColor = System.Drawing.Color.White;
            this.buttonAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonAdd.Enabled = false;
            this.buttonAdd.Font = new System.Drawing.Font("Georgia", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdd.ForeColor = System.Drawing.Color.Black;
            this.buttonAdd.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.buttonAdd.Location = new System.Drawing.Point(0, 0);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(90, 42);
            this.buttonAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.buttonAdd.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnadd,
            this.BtnRefresh});
            this.buttonAdd.TabIndex = 169;
            this.buttonAdd.Text = "Tasks";
            this.buttonAdd.Tooltip = "New Order";
            // 
            // btnadd
            // 
            this.btnadd.ForeColor = System.Drawing.Color.Green;
            this.btnadd.GlobalItem = false;
            this.btnadd.ImageFixedSize = new System.Drawing.Size(20, 20);
            this.btnadd.Name = "btnadd";
            this.btnadd.Text = "Create Batch Settings";
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.GlobalItem = false;
            this.BtnRefresh.Image = global::MTOMS.Properties.Resources.refresh;
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Text = "Refresh";
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // fGrid
            // 
            this.fGrid.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.fGrid.AutoHeightRowMode = TenTec.Windows.iGridLib.iGAutoHeightRowMode.Cells;
            this.fGrid.AutoResizeCols = true;
            this.fGrid.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.fGrid.BorderStyle = TenTec.Windows.iGridLib.iGBorderStyle.Flat;
            this.fGrid.ContextMenuStrip = this.contextMenuStrip1;
            this.fGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fGrid.ForeColor = System.Drawing.SystemColors.WindowText;
            this.fGrid.GridLines.GroupRows = new TenTec.Windows.iGridLib.iGPenStyle(System.Drawing.Color.Gainsboro, 1, System.Drawing.Drawing2D.DashStyle.Solid);
            this.fGrid.GridLines.HorizontalExtended = new TenTec.Windows.iGridLib.iGPenStyle(System.Drawing.SystemColors.ControlDarkDark, 1, System.Drawing.Drawing2D.DashStyle.Solid);
            this.fGrid.GridLines.Mode = TenTec.Windows.iGridLib.iGGridLinesMode.Vertical;
            this.fGrid.GridLines.VerticalExtended = new TenTec.Windows.iGridLib.iGPenStyle(System.Drawing.SystemColors.ControlDarkDark, 1, System.Drawing.Drawing2D.DashStyle.Solid);
            this.fGrid.GroupBox.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.fGrid.Header.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.fGrid.Header.Height = 19;
            this.fGrid.Location = new System.Drawing.Point(0, 42);
            this.fGrid.Name = "fGrid";
            this.fGrid.RowHeader.Visible = true;
            this.fGrid.RowMode = true;
            this.fGrid.RowSelectionInCellMode = TenTec.Windows.iGridLib.iGRowSelectionInCellModeTypes.SingleRow;
            this.fGrid.SelCellsBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.fGrid.SelCellsForeColor = System.Drawing.Color.Black;
            this.fGrid.SilentValidation = true;
            this.fGrid.Size = new System.Drawing.Size(657, 491);
            this.fGrid.TabIndex = 26;
            this.fGrid.TreeCol = null;
            this.fGrid.TreeLines.Color = System.Drawing.SystemColors.WindowText;
            this.fGrid.TreeLines.Visible = true;
            this.fGrid.VScrollBar.CustomButtons.AddRange(new TenTec.Windows.iGridLib.iGScrollBarCustomButton[] {
            new TenTec.Windows.iGridLib.iGScrollBarCustomButton(TenTec.Windows.iGridLib.iGScrollBarCustomButtonAlign.Far, TenTec.Windows.iGridLib.iGActions.ExpandAll, -1, null, true, null),
            new TenTec.Windows.iGridLib.iGScrollBarCustomButton(TenTec.Windows.iGridLib.iGScrollBarCustomButtonAlign.Far, TenTec.Windows.iGridLib.iGActions.CollapseAll, -1, null, true, null)});
            this.fGrid.VScrollBar.Visibility = TenTec.Windows.iGridLib.iGScrollBarVisibility.Always;
            // 
            // backworker
            // 
            this.backworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backworker_DoWork);
            this.backworker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backworker_RunWorkerCompleted);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteRecordToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(183, 52);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // deleteRecordToolStripMenuItem
            // 
            this.deleteRecordToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteRecordToolStripMenuItem.ForeColor = System.Drawing.Color.Red;
            this.deleteRecordToolStripMenuItem.Name = "deleteRecordToolStripMenuItem";
            this.deleteRecordToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.deleteRecordToolStripMenuItem.Text = "Delete Record";
            this.deleteRecordToolStripMenuItem.Click += new System.EventHandler(this.deleteRecordToolStripMenuItem_Click);
            // 
            // BatchNoManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 533);
            this.Controls.Add(this.fGrid);
            this.Controls.Add(this.pn_dis);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BatchNoManager";
            this.Text = "Batch-No Manager";
            this.Load += new System.EventHandler(this.BatchNoManager_Load);
            this.pn_dis.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx pn_dis;
        private DevComponents.DotNetBar.ButtonX buttonAdd;
        private DevComponents.DotNetBar.ButtonItem btnadd;
        private DevComponents.DotNetBar.ButtonItem BtnRefresh;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboDate;
        private TenTec.Windows.iGridLib.iGrid fGrid;
        private System.ComponentModel.BackgroundWorker backworker;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem deleteRecordToolStripMenuItem;
    }
}