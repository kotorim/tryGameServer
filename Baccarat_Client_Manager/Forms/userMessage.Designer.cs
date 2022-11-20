
namespace Baccarat_Client_Manager.Forms
{
    partial class userMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(userMessage));
            this.grid = new System.Windows.Forms.DataGridView();
            this.editTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.editType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.editContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.subedMoney = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.money = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.editTime,
            this.editType,
            this.editContent});
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersVisible = false;
            this.grid.RowHeadersWidth = 72;
            this.grid.RowTemplate.Height = 33;
            this.grid.Size = new System.Drawing.Size(1078, 923);
            this.grid.TabIndex = 0;
            // 
            // editTime
            // 
            this.editTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.editTime.HeaderText = "操作时间";
            this.editTime.MinimumWidth = 9;
            this.editTime.Name = "editTime";
            this.editTime.ReadOnly = true;
            this.editTime.Width = 137;
            // 
            // editType
            // 
            this.editType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.editType.HeaderText = "操作类型";
            this.editType.MinimumWidth = 9;
            this.editType.Name = "editType";
            this.editType.ReadOnly = true;
            this.editType.Width = 137;
            // 
            // editContent
            // 
            this.editContent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.editContent.HeaderText = "操作内容";
            this.editContent.MinimumWidth = 9;
            this.editContent.Name = "editContent";
            this.editContent.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.subedMoney);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.money);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 929);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1078, 79);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(870, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(208, 73);
            this.button1.TabIndex = 5;
            this.button1.Text = "纯管理员扣分明细";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // subedMoney
            // 
            this.subedMoney.AutoSize = true;
            this.subedMoney.Location = new System.Drawing.Point(235, 42);
            this.subedMoney.Name = "subedMoney";
            this.subedMoney.Size = new System.Drawing.Size(77, 28);
            this.subedMoney.TabIndex = 4;
            this.subedMoney.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(217, 28);
            this.label3.TabIndex = 3;
            this.label3.Text = "当前已扣除代币总额：";
            // 
            // money
            // 
            this.money.AutoSize = true;
            this.money.Location = new System.Drawing.Point(235, 6);
            this.money.Name = "money";
            this.money.Size = new System.Drawing.Size(77, 28);
            this.money.TabIndex = 1;
            this.money.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前代币总额：";
            // 
            // userMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1078, 1008);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grid);
            this.Font = new System.Drawing.Font("小米兰亭", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "userMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户信息";
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label money;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn editTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn editType;
        private System.Windows.Forms.DataGridViewTextBoxColumn editContent;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label subedMoney;
        private System.Windows.Forms.Label label3;
    }
}