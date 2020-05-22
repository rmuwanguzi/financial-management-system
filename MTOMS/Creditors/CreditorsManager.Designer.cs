namespace MTOMS
{
    partial class CreditorsManager
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
            this.paneltotal = new DevComponents.DotNetBar.PanelEx();
            this.buttonAdd = new DevComponents.DotNetBar.ButtonX();
            this.fGrid = new TenTec.Windows.iGridLib.iGrid();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editMemberDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backworker = new System.ComponentModel.BackgroundWorker();
            this.fStyleGroup = new TenTec.Windows.iGridLib.iGCellStyleDesign();
            this.paneltotal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // paneltotal
            // 
            this.paneltotal.CanvasColor = System.Drawing.SystemColors.Control;
            this.paneltotal.Controls.Add(this.buttonAdd);
            this.paneltotal.Dock = System.Windows.Forms.DockStyle.Top;
            this.paneltotal.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paneltotal.Location = new System.Drawing.Point(0, 0);
            this.paneltotal.Name = "paneltotal";
            this.paneltotal.Size = new System.Drawing.Size(1105, 51);
            this.paneltotal.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.paneltotal.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.paneltotal.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.paneltotal.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.paneltotal.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.paneltotal.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.paneltotal.Style.GradientAngle = 90;
            this.paneltotal.TabIndex = 4;
            // 
            // buttonAdd
            // 
            this.buttonAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonAdd.AutoExpandOnClick = true;
            this.buttonAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonAdd.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdd.ForeColor = System.Drawing.Color.DarkGreen;
            this.buttonAdd.Location = new System.Drawing.Point(3, 3);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(120, 44);
            this.buttonAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "Creditors";
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // fGrid
            // 
            this.fGrid.BorderStyle = TenTec.Windows.iGridLib.iGBorderStyle.None;
            this.fGrid.ContextMenuStrip = this.contextMenuStrip1;
            this.fGrid.DefaultCol.AllowMoving = false;
            this.fGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fGrid.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fGrid.ForeColor = System.Drawing.SystemColors.WindowText;
            this.fGrid.FrozenArea.ColCount = 3;
            this.fGrid.FrozenArea.ColsEdge = new TenTec.Windows.iGridLib.iGPenStyle(System.Drawing.Color.DarkGray, 1, System.Drawing.Drawing2D.DashStyle.Solid);
            this.fGrid.GroupRowLevelStyles = new TenTec.Windows.iGridLib.iGCellStyle[] {
        ((TenTec.Windows.iGridLib.iGCellStyle)(this.fStyleGroup))};
            this.fGrid.Header.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.fGrid.Header.Height = 27;
            this.fGrid.Header.HGridLinesStyle = new TenTec.Windows.iGridLib.iGPenStyle(System.Drawing.SystemColors.ControlDark, 1, System.Drawing.Drawing2D.DashStyle.Solid);
            this.fGrid.Location = new System.Drawing.Point(0, 51);
            this.fGrid.Name = "fGrid";
            this.fGrid.RowHeader.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.fGrid.RowHeader.Visible = true;
            this.fGrid.RowMode = true;
            this.fGrid.RowResizeMode = TenTec.Windows.iGridLib.iGRowResizeMode.NotAllowed;
            this.fGrid.RowSelectionInCellMode = TenTec.Windows.iGridLib.iGRowSelectionInCellModeTypes.SingleRow;
            this.fGrid.RowTextStartColNear = 3;
            this.fGrid.SilentValidation = true;
            this.fGrid.Size = new System.Drawing.Size(1105, 419);
            this.fGrid.TabIndex = 11;
            this.fGrid.TreeCol = null;
            this.fGrid.TreeLines.Color = System.Drawing.SystemColors.WindowText;
            this.fGrid.VScrollBar.CustomButtons.AddRange(new TenTec.Windows.iGridLib.iGScrollBarCustomButton[] {
            new TenTec.Windows.iGridLib.iGScrollBarCustomButton(TenTec.Windows.iGridLib.iGScrollBarCustomButtonAlign.Near, TenTec.Windows.iGridLib.iGActions.CollapseAll, -1, "Collapse All", true, null),
            new TenTec.Windows.iGridLib.iGScrollBarCustomButton(TenTec.Windows.iGridLib.iGScrollBarCustomButtonAlign.Near, TenTec.Windows.iGridLib.iGActions.ExpandAll, -1, "Expand All", true, null)});
            this.fGrid.VScrollBar.Visibility = TenTec.Windows.iGridLib.iGScrollBarVisibility.Always;
            this.fGrid.AfterContentsSorted += new System.EventHandler(this.fGrid_AfterContentsSorted);
            this.fGrid.AfterAutoGroupRowCreated += new TenTec.Windows.iGridLib.iGAfterAutoGroupRowCreatedEventHandler(this.fGrid_AfterAutoGroupRowCreated);
            this.fGrid.CellMouseDown += new TenTec.Windows.iGridLib.iGCellMouseDownEventHandler(this.fGrid_CellMouseDown);
            this.fGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.fGrid_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editMemberDataToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(133, 26);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // editMemberDataToolStripMenuItem
            // 
            this.editMemberDataToolStripMenuItem.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editMemberDataToolStripMenuItem.ForeColor = System.Drawing.Color.Blue;
            this.editMemberDataToolStripMenuItem.Image = global::MTOMS.Properties.Resources.FlagYellow;
            this.editMemberDataToolStripMenuItem.Name = "editMemberDataToolStripMenuItem";
            this.editMemberDataToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.editMemberDataToolStripMenuItem.Text = "Pay Out";
            this.editMemberDataToolStripMenuItem.Click += new System.EventHandler(this.editMemberDataToolStripMenuItem_Click);
            // 
            // backworker
            // 
            this.backworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backworker_DoWork);
            this.backworker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backworker_RunWorkerCompleted);
            // 
            // fStyleGroup
            // 
            this.fStyleGroup.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.fStyleGroup.CustomDrawFlags = TenTec.Windows.iGridLib.iGCustomDrawFlags.None;
            this.fStyleGroup.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fStyleGroup.ForeColor = System.Drawing.Color.Maroon;
            this.fStyleGroup.TextAlign = TenTec.Windows.iGridLib.iGContentAlignment.MiddleLeft;
            // 
            // CreditorsManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 470);
            this.Controls.Add(this.fGrid);
            this.Controls.Add(this.paneltotal);
            this.DoubleBuffered = true;
            this.Name = "CreditorsManager";
            this.Text = "CreditorsManager";
            this.Load += new System.EventHandler(this.CreditorsManager_Load);
            this.paneltotal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx paneltotal;
        private DevComponents.DotNetBar.ButtonX buttonAdd;
        private TenTec.Windows.iGridLib.iGrid fGrid;
        private System.ComponentModel.BackgroundWorker backworker;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editMemberDataToolStripMenuItem;
        private TenTec.Windows.iGridLib.iGCellStyleDesign fStyleGroup;

    }
}