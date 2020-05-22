namespace MTOMS.Members
{
    partial class MemberPicView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemberPicView));
            this.iGrid1 = new TenTec.Windows.iGridLib.iGrid();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.printGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.iGrid1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // iGrid1
            // 
            this.iGrid1.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGrid1.AutoHeightRowMode = TenTec.Windows.iGridLib.iGAutoHeightRowMode.Cells;
            this.iGrid1.AutoResizeCols = true;
            this.iGrid1.BorderStyle = TenTec.Windows.iGridLib.iGBorderStyle.None;
            this.iGrid1.ContextMenuStrip = this.contextMenuStrip1;
            this.iGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.iGrid1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.iGrid1.Header.Appearance = TenTec.Windows.iGridLib.iGControlPaintAppearance.StyleFlat;
            this.iGrid1.Header.Height = 19;
            this.iGrid1.Header.Visible = false;
            this.iGrid1.Location = new System.Drawing.Point(0, 0);
            this.iGrid1.Name = "iGrid1";
            this.iGrid1.RowHeader.Visible = true;
            this.iGrid1.RowHeader.Width = 5;
            this.iGrid1.RowResizeMode = TenTec.Windows.iGridLib.iGRowResizeMode.NotAllowed;
            this.iGrid1.RowSelectionInCellMode = TenTec.Windows.iGridLib.iGRowSelectionInCellModeTypes.SingleRow;
            this.iGrid1.SilentValidation = true;
            this.iGrid1.Size = new System.Drawing.Size(748, 469);
            this.iGrid1.TabIndex = 12;
            this.iGrid1.TreeCol = null;
            this.iGrid1.TreeLines.Color = System.Drawing.SystemColors.WindowText;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printGridToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(129, 26);
            // 
            // printGridToolStripMenuItem
            // 
            this.printGridToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.printGridToolStripMenuItem.ForeColor = System.Drawing.Color.DarkMagenta;
            this.printGridToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printGridToolStripMenuItem.Image")));
            this.printGridToolStripMenuItem.Name = "printGridToolStripMenuItem";
            this.printGridToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.printGridToolStripMenuItem.Text = "Print Grid";
            this.printGridToolStripMenuItem.Click += new System.EventHandler(this.printGridToolStripMenuItem_Click);
            // 
            // MemberPicView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 469);
            this.Controls.Add(this.iGrid1);
            this.DoubleBuffered = true;
            this.Name = "MemberPicView";
            this.Text = "MemberPicView";
            this.Load += new System.EventHandler(this.MemberPicView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iGrid1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TenTec.Windows.iGridLib.iGrid iGrid1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem printGridToolStripMenuItem;
    }
}