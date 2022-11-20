
namespace Baccarat_Client_Manager.Forms
{
    partial class userManager
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("用户列表");
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(userManager));
            this.nameTree = new System.Windows.Forms.TreeView();
            this.nameGrid = new System.Windows.Forms.DataGridView();
            this.username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.control = new System.Windows.Forms.DataGridViewButtonColumn();
            this.control2 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.contorl3 = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.nameGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // nameTree
            // 
            this.nameTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.nameTree.Font = new System.Drawing.Font("小米兰亭", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nameTree.ItemHeight = 31;
            this.nameTree.Location = new System.Drawing.Point(0, 0);
            this.nameTree.Name = "nameTree";
            treeNode1.Name = "super";
            treeNode1.Text = "用户列表";
            this.nameTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.nameTree.Size = new System.Drawing.Size(343, 678);
            this.nameTree.TabIndex = 0;
            this.nameTree.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.nameTree_AfterCollapse);
            this.nameTree.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.nameTree_AfterExpand);
            this.nameTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.nameTree_AfterSelect);
            // 
            // nameGrid
            // 
            this.nameGrid.AllowUserToAddRows = false;
            this.nameGrid.AllowUserToDeleteRows = false;
            this.nameGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.nameGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.nameGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.username,
            this.status,
            this.control,
            this.control2,
            this.contorl3});
            this.nameGrid.Location = new System.Drawing.Point(349, 0);
            this.nameGrid.Name = "nameGrid";
            this.nameGrid.ReadOnly = true;
            this.nameGrid.RowHeadersVisible = false;
            this.nameGrid.RowHeadersWidth = 72;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("小米兰亭", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nameGrid.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.nameGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("小米兰亭", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nameGrid.RowTemplate.Height = 36;
            this.nameGrid.RowTemplate.ReadOnly = true;
            this.nameGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.nameGrid.Size = new System.Drawing.Size(871, 678);
            this.nameGrid.TabIndex = 1;
            this.nameGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.nameGrid_CellContentClick);
            // 
            // username
            // 
            this.username.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.username.HeaderText = "id";
            this.username.MinimumWidth = 9;
            this.username.Name = "username";
            this.username.ReadOnly = true;
            this.username.Width = 72;
            // 
            // status
            // 
            this.status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.status.HeaderText = "状态";
            this.status.MinimumWidth = 9;
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Width = 95;
            // 
            // control
            // 
            this.control.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("小米兰亭", 11.14286F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.control.DefaultCellStyle = dataGridViewCellStyle1;
            this.control.HeaderText = "操作";
            this.control.MinimumWidth = 9;
            this.control.Name = "control";
            this.control.ReadOnly = true;
            this.control.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.control.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.control.Text = "";
            // 
            // control2
            // 
            this.control2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.control2.HeaderText = "";
            this.control2.MinimumWidth = 9;
            this.control2.Name = "control2";
            this.control2.ReadOnly = true;
            this.control2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.control2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // contorl3
            // 
            this.contorl3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.contorl3.HeaderText = "";
            this.contorl3.MinimumWidth = 9;
            this.contorl3.Name = "contorl3";
            this.contorl3.ReadOnly = true;
            this.contorl3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.contorl3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // userManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1220, 678);
            this.Controls.Add(this.nameGrid);
            this.Controls.Add(this.nameTree);
            this.Font = new System.Drawing.Font("小米兰亭", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "userManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "账户监管";
            ((System.ComponentModel.ISupportInitialize)(this.nameGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView nameTree;
        private System.Windows.Forms.DataGridView nameGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn username;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewButtonColumn control;
        private System.Windows.Forms.DataGridViewButtonColumn control2;
        private System.Windows.Forms.DataGridViewButtonColumn contorl3;
    }
}