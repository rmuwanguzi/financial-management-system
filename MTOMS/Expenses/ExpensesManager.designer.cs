namespace MTOMS
{
    partial class ExpensesManager
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
            this.label2 = new System.Windows.Forms.Label();
            this.combofilter = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.buttonAdd = new DevComponents.DotNetBar.ButtonX();
            this.btnadd = new DevComponents.DotNetBar.ButtonItem();
            this.BtnRefresh = new DevComponents.DotNetBar.ButtonItem();
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.fGrid = new TenTec.Windows.iGridLib.iGrid();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.printGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupByToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unGroupByToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.fStyleGroup = new TenTec.Windows.iGridLib.iGCellStyleDesign();
            this.iGCellStyleDesign1 = new TenTec.Windows.iGridLib.iGCellStyleDesign();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.backworker = new System.ComponentModel.BackgroundWorker();
            this.button1 = new System.Windows.Forms.Button();
            this.iGCellStyleDesign2 = new TenTec.Windows.iGridLib.iGCellStyleDesign();
            this.pn_dis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pn_dis
            // 
            this.pn_dis.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.pn_dis.Controls.Add(this.label2);
            this.pn_dis.Controls.Add(this.combofilter);
            this.pn_dis.Controls.Add(this.checkBox1);
            this.pn_dis.Controls.Add(this.buttonAdd);
            this.pn_dis.Controls.Add(this.textBoxX1);
            this.pn_dis.Dock = System.Windows.Forms.DockStyle.Top;
            this.pn_dis.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pn_dis.Location = new System.Drawing.Point(0, 0);
            this.pn_dis.Name = "pn_dis";
            this.pn_dis.Size = new System.Drawing.Size(1169, 42);
            this.pn_dis.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pn_dis.Style.BackColor1.Color = System.Drawing.Color.White;
            this.pn_dis.Style.BackColor2.Color = System.Drawing.Color.White;
            this.pn_dis.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pn_dis.Style.BorderColor.Color = System.Drawing.SystemColors.ControlDark;
            this.pn_dis.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pn_dis.Style.GradientAngle = 90;
            this.pn_dis.TabIndex = 8;
            this.pn_dis.Click += new System.EventHandler(this.pn_dis_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkRed;
            this.label2.Location = new System.Drawing.Point(614, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 22);
            this.label2.TabIndex = 174;
            this.label2.Text = "Filter";
            // 
            // combofilter
            // 
            this.combofilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combofilter.DropDownWidth = 400;
            this.combofilter.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combofilter.FormattingEnabled = true;
            this.combofilter.Location = new System.Drawing.Point(676, 7);
            this.combofilter.Name = "combofilter";
            this.combofilter.Size = new System.Drawing.Size(260, 27);
            this.combofilter.TabIndex = 173;
            this.combofilter.SelectedIndexChanged += new System.EventHandler(this.combofilter_SelectedIndexChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(421, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(170, 31);
            this.checkBox1.TabIndex = 170;
            this.checkBox1.Text = "EDIT Records";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // buttonAdd
            // 
            this.buttonAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonAdd.AutoExpandOnClick = true;
            this.buttonAdd.BackColor = System.Drawing.Color.White;
            this.buttonAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonAdd.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonAdd.Font = new System.Drawing.Font("Georgia", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdd.ForeColor = System.Drawing.Color.Black;
            this.buttonAdd.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.buttonAdd.Location = new System.Drawing.Point(0, 0);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(141, 42);
            this.buttonAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.buttonAdd.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnadd,
            this.BtnRefresh});
            this.buttonAdd.TabIndex = 169;
            this.buttonAdd.Text = "Tasks";
            this.buttonAdd.Tooltip = "New Order";
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // btnadd
            // 
            this.btnadd.ForeColor = System.Drawing.Color.Green;
            this.btnadd.GlobalItem = false;
            this.btnadd.ImageFixedSize = new System.Drawing.Size(20, 20);
            this.btnadd.Name = "btnadd";
            this.btnadd.Text = "Create New Expense Account";
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
            // textBoxX1
            // 
            // 
            // 
            // 
            this.textBoxX1.Border.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.textBoxX1.Border.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.textBoxX1.Border.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.textBoxX1.Border.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxX1.Location = new System.Drawing.Point(147, 8);
            this.textBoxX1.MaxLength = 20;
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.Size = new System.Drawing.Size(269, 27);
            this.textBoxX1.TabIndex = 168;
            this.textBoxX1.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty;
            this.textBoxX1.WatermarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.textBoxX1.WatermarkText = "Search For Account";
            this.textBoxX1.TextChanged += new System.EventHandler(this.textBoxX1_TextChanged);
            this.textBoxX1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxX1_KeyDown);
            // 
            // fGrid
            // 
            this.fGrid.AutoHeightRowMode = TenTec.Windows.iGridLib.iGAutoHeightRowMode.Cells;
            this.fGrid.AutoResizeCols = true;
            this.fGrid.BorderStyle = TenTec.Windows.iGridLib.iGBorderStyle.Flat;
            this.fGrid.ContextMenuStrip = this.contextMenuStrip1;
            this.fGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fGrid.ForeColor = System.Drawing.SystemColors.WindowText;
            this.fGrid.GroupBox.BackColor = System.Drawing.Color.White;
            this.fGrid.GroupRowLevelStyles = new TenTec.Windows.iGridLib.iGCellStyle[] {
        ((TenTec.Windows.iGridLib.iGCellStyle)(this.fStyleGroup))};
            this.fGrid.Header.Height = 21;
            this.fGrid.Location = new System.Drawing.Point(0, 42);
            this.fGrid.Name = "fGrid";
            this.fGrid.RowHeader.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.fGrid.RowHeader.Visible = true;
            this.fGrid.RowMode = true;
            this.fGrid.RowSelectionInCellMode = TenTec.Windows.iGridLib.iGRowSelectionInCellModeTypes.SingleRow;
            this.fGrid.SelCellsBackColor = System.Drawing.Color.Bisque;
            this.fGrid.SelCellsBackColorNoFocus = System.Drawing.Color.Linen;
            this.fGrid.SelCellsForeColor = System.Drawing.Color.Black;
            this.fGrid.SilentValidation = true;
            this.fGrid.Size = new System.Drawing.Size(1169, 434);
            this.fGrid.TabIndex = 21;
            this.fGrid.TreeCol = null;
            this.fGrid.TreeLines.Color = System.Drawing.SystemColors.WindowText;
            this.fGrid.VScrollBar.CustomButtons.AddRange(new TenTec.Windows.iGridLib.iGScrollBarCustomButton[] {
            new TenTec.Windows.iGridLib.iGScrollBarCustomButton(TenTec.Windows.iGridLib.iGScrollBarCustomButtonAlign.Far, TenTec.Windows.iGridLib.iGActions.ExpandAll, -1, "ExpandAll", true, null),
            new TenTec.Windows.iGridLib.iGScrollBarCustomButton(TenTec.Windows.iGridLib.iGScrollBarCustomButtonAlign.Far, TenTec.Windows.iGridLib.iGActions.CollapseAll, -1, "CollapseAll", true, null),
            new TenTec.Windows.iGridLib.iGScrollBarCustomButton(TenTec.Windows.iGridLib.iGScrollBarCustomButtonAlign.Far, TenTec.Windows.iGridLib.iGActions.None, -1, "Un Group", true, null)});
            this.fGrid.VScrollBar.Visibility = TenTec.Windows.iGridLib.iGScrollBarVisibility.Always;
            this.fGrid.AfterContentsGrouped += new System.EventHandler(this.fGrid_AfterContentsGrouped);
            this.fGrid.AfterContentsSorted += new System.EventHandler(this.fGrid_AfterContentsSorted);
            this.fGrid.AfterRowStateChanged += new TenTec.Windows.iGridLib.iGAfterRowStateChangedEventHandler(this.fGrid_AfterRowStateChanged);
            this.fGrid.AfterAutoGroupRowCreated += new TenTec.Windows.iGridLib.iGAfterAutoGroupRowCreatedEventHandler(this.fGrid_AfterAutoGroupRowCreated);
            this.fGrid.CellMouseUp += new TenTec.Windows.iGridLib.iGCellMouseUpEventHandler(this.fGrid_CellMouseUp);
            this.fGrid.CellClick += new TenTec.Windows.iGridLib.iGCellClickEventHandler(this.fGrid_CellClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.Color.White;
            this.contextMenuStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printGridToolStripMenuItem,
            this.groupByToolStripMenuItem,
            this.unGroupByToolStripMenuItem,
            this.ts_delete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(260, 122);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // printGridToolStripMenuItem
            // 
            this.printGridToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printGridToolStripMenuItem.ForeColor = System.Drawing.Color.MediumBlue;
            this.printGridToolStripMenuItem.Name = "printGridToolStripMenuItem";
            this.printGridToolStripMenuItem.Size = new System.Drawing.Size(232, 24);
            this.printGridToolStripMenuItem.Text = "Export To Excel";
            this.printGridToolStripMenuItem.Click += new System.EventHandler(this.printGridToolStripMenuItem_Click);
            // 
            // groupByToolStripMenuItem
            // 
            this.groupByToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupByToolStripMenuItem.Name = "groupByToolStripMenuItem";
            this.groupByToolStripMenuItem.Size = new System.Drawing.Size(232, 24);
            this.groupByToolStripMenuItem.Text = "Group By";
            this.groupByToolStripMenuItem.Click += new System.EventHandler(this.groupByToolStripMenuItem_Click);
            // 
            // unGroupByToolStripMenuItem
            // 
            this.unGroupByToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unGroupByToolStripMenuItem.ForeColor = System.Drawing.Color.DarkRed;
            this.unGroupByToolStripMenuItem.Name = "unGroupByToolStripMenuItem";
            this.unGroupByToolStripMenuItem.Size = new System.Drawing.Size(232, 24);
            this.unGroupByToolStripMenuItem.Text = "UnGroup By";
            this.unGroupByToolStripMenuItem.Click += new System.EventHandler(this.unGroupByToolStripMenuItem_Click);
            // 
            // ts_delete
            // 
            this.ts_delete.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ts_delete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ts_delete.Image = global::MTOMS.Properties.Resources.delete;
            this.ts_delete.Name = "ts_delete";
            this.ts_delete.Size = new System.Drawing.Size(259, 24);
            this.ts_delete.Text = "Delete Selected Expense";
            this.ts_delete.Click += new System.EventHandler(this.ts_delete_Click);
            // 
            // fStyleGroup
            // 
            this.fStyleGroup.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.fStyleGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fStyleGroup.ForeColor = System.Drawing.Color.Black;
            // 
            // iGCellStyleDesign1
            // 
            this.iGCellStyleDesign1.BackColor = System.Drawing.Color.Silver;
            this.iGCellStyleDesign1.ForeColor = System.Drawing.Color.Black;
            // 
            // buttonItem1
            // 
            this.buttonItem1.GlobalItem = false;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "buttonItem1";
            // 
            // backworker
            // 
            this.backworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backworker_DoWork);
            this.backworker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backworker_RunWorkerCompleted);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.IndianRed;
            this.button1.Location = new System.Drawing.Point(0, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(21, 38);
            this.button1.TabIndex = 22;
            this.button1.Text = "C";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ExpensesManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1169, 476);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fGrid);
            this.Controls.Add(this.pn_dis);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "ExpensesManager";
            this.Text = "Expenses Accounts Manager";
            this.Load += new System.EventHandler(this.ItemsManager_Load);
            this.pn_dis.ResumeLayout(false);
            this.pn_dis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fGrid)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx pn_dis;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private TenTec.Windows.iGridLib.iGrid fGrid;
        private DevComponents.DotNetBar.ButtonX buttonAdd;
        private DevComponents.DotNetBar.ButtonItem btnadd;
        private TenTec.Windows.iGridLib.iGCellStyleDesign iGCellStyleDesign1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ts_delete;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private System.Windows.Forms.ToolStripMenuItem printGridToolStripMenuItem;
        private DevComponents.DotNetBar.ButtonItem BtnRefresh;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.ComponentModel.BackgroundWorker backworker;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem groupByToolStripMenuItem;
        private TenTec.Windows.iGridLib.iGCellStyleDesign fStyleGroup;
        private TenTec.Windows.iGridLib.iGCellStyleDesign iGCellStyleDesign2;
        private System.Windows.Forms.ToolStripMenuItem unGroupByToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox combofilter;
    }
}