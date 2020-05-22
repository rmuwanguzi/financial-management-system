namespace MTOMS
{
    partial class WorkManager
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
            this.dockContainerItem1 = new DevComponents.DotNetBar.DockContainerItem();
            this.backworker1 = new System.ComponentModel.BackgroundWorker();
            this.collapsibleSplitter1 = new NJFLib.Controls.CollapsibleSplitter();
            this.treeListView1 = new SdaHelperManager.TreeList.TreeListView();
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dockContainerItem1
            // 
            this.dockContainerItem1.Name = "dockContainerItem1";
            this.dockContainerItem1.Text = "dockContainerItem1";
            // 
            // backworker1
            // 
            this.backworker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backworker1_DoWork);
            this.backworker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backworker1_RunWorkerCompleted);
            // 
            // collapsibleSplitter1
            // 
            this.collapsibleSplitter1.AnimationDelay = 20;
            this.collapsibleSplitter1.AnimationStep = 20;
            this.collapsibleSplitter1.BackColor = System.Drawing.Color.Gray;
            this.collapsibleSplitter1.BorderStyle3D = System.Windows.Forms.Border3DStyle.Flat;
            this.collapsibleSplitter1.ControlToHide = this.treeListView1;
            this.collapsibleSplitter1.ExpandParentForm = false;
            this.collapsibleSplitter1.Location = new System.Drawing.Point(301, 0);
            this.collapsibleSplitter1.Name = "collapsibleSplitter1";
            this.collapsibleSplitter1.TabIndex = 30;
            this.collapsibleSplitter1.TabStop = false;
            this.collapsibleSplitter1.UseAnimations = false;
            this.collapsibleSplitter1.VisualStyle = NJFLib.Controls.VisualStyles.Mozilla;
            // 
            // treeListView1
            // 
            this.treeListView1.BackColor = System.Drawing.Color.White;
            this.treeListView1.ColumnsOptions.HeaderHeight = 30;
            this.treeListView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeListView1.Enabled = false;
            this.treeListView1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeListView1.Images = null;
            this.treeListView1.Location = new System.Drawing.Point(0, 0);
            this.treeListView1.MultiSelect = false;
            this.treeListView1.Name = "treeListView1";
            this.treeListView1.RowOptions.ItemHeight = 26;
            this.treeListView1.RowOptions.ShowHeader = false;
            this.treeListView1.Size = new System.Drawing.Size(301, 462);
            this.treeListView1.TabIndex = 2;
            this.treeListView1.Text = "TreeListView1";
            this.treeListView1.ViewOptions.ShowCheckBox = false;
            this.treeListView1.Visible = false;
            this.treeListView1.SizeChanged += new System.EventHandler(this.treeListView1_SizeChanged);
            this.treeListView1.NodeCheckedEvent += new SdaHelperManager.TreeList.TreeListView.NodeCheckedEventHandler(this.treeListView1_NodeCheckedEvent);
            // 
            // WorkManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(968, 462);
            this.Controls.Add(this.collapsibleSplitter1);
            this.Controls.Add(this.treeListView1);
            this.DoubleBuffered = true;
            this.IsMdiContainer = true;
            this.Name = "WorkManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.WorkManager_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WorkManager_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.DockContainerItem dockContainerItem1;
        internal SdaHelperManager.TreeList.TreeListView treeListView1;
        private System.ComponentModel.BackgroundWorker backworker1;
        private NJFLib.Controls.CollapsibleSplitter collapsibleSplitter1;

    }
}