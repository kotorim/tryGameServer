
namespace Baccarat_Client_Manager.Forms
{
    partial class fmManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmManager));
            this.userGrid = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.subMoneyBtn = new System.Windows.Forms.Button();
            this.addMoneyBtn = new System.Windows.Forms.Button();
            this.moneyEditTB = new System.Windows.Forms.TextBox();
            this.curMoneyText = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.addUserBtn = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coinNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coinEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.userControl = new System.Windows.Forms.DataGridViewButtonColumn();
            this.defaultPassword = new System.Windows.Forms.DataGridViewButtonColumn();
            this.getMes = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.userGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // userGrid
            // 
            this.userGrid.AllowUserToAddRows = false;
            this.userGrid.AllowUserToDeleteRows = false;
            this.userGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userGrid.ColumnHeadersHeight = 30;
            this.userGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.userName,
            this.coinNum,
            this.coinEdit,
            this.userControl,
            this.defaultPassword,
            this.getMes});
            this.userGrid.Enabled = false;
            this.userGrid.Location = new System.Drawing.Point(0, 0);
            this.userGrid.Name = "userGrid";
            this.userGrid.ReadOnly = true;
            this.userGrid.RowHeadersVisible = false;
            this.userGrid.RowHeadersWidth = 72;
            this.userGrid.RowTemplate.Height = 33;
            this.userGrid.Size = new System.Drawing.Size(1096, 565);
            this.userGrid.TabIndex = 0;
            this.userGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.userGrid_CellContentClick);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1096, 643);
            this.panel1.TabIndex = 1;
            this.panel1.Visible = false;
            this.panel1.VisibleChanged += new System.EventHandler(this.panel1_VisibleChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackgroundImage = global::Baccarat_Client_Manager.Properties.Resources.di;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.subMoneyBtn);
            this.panel2.Controls.Add(this.addMoneyBtn);
            this.panel2.Controls.Add(this.moneyEditTB);
            this.panel2.Controls.Add(this.curMoneyText);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(321, 121);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(419, 370);
            this.panel2.TabIndex = 0;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(160, 298);
            this.button3.Margin = new System.Windows.Forms.Padding(50);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(120, 49);
            this.button3.TabIndex = 9;
            this.button3.Text = "关闭";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // subMoneyBtn
            // 
            this.subMoneyBtn.BackgroundImage = global::Baccarat_Client_Manager.Properties.Resources.jian;
            this.subMoneyBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.subMoneyBtn.Location = new System.Drawing.Point(317, 210);
            this.subMoneyBtn.Margin = new System.Windows.Forms.Padding(30);
            this.subMoneyBtn.Name = "subMoneyBtn";
            this.subMoneyBtn.Size = new System.Drawing.Size(60, 60);
            this.subMoneyBtn.TabIndex = 7;
            this.subMoneyBtn.UseVisualStyleBackColor = true;
            this.subMoneyBtn.Click += new System.EventHandler(this.subMoneyBtn_Click);
            // 
            // addMoneyBtn
            // 
            this.addMoneyBtn.BackgroundImage = global::Baccarat_Client_Manager.Properties.Resources.jia;
            this.addMoneyBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.addMoneyBtn.Location = new System.Drawing.Point(317, 90);
            this.addMoneyBtn.Margin = new System.Windows.Forms.Padding(30);
            this.addMoneyBtn.Name = "addMoneyBtn";
            this.addMoneyBtn.Size = new System.Drawing.Size(60, 60);
            this.addMoneyBtn.TabIndex = 5;
            this.addMoneyBtn.UseVisualStyleBackColor = true;
            this.addMoneyBtn.Click += new System.EventHandler(this.addMoneyBtn_Click);
            // 
            // moneyEditTB
            // 
            this.moneyEditTB.BackColor = System.Drawing.Color.LightGray;
            this.moneyEditTB.Location = new System.Drawing.Point(50, 223);
            this.moneyEditTB.Margin = new System.Windows.Forms.Padding(30);
            this.moneyEditTB.Name = "moneyEditTB";
            this.moneyEditTB.Size = new System.Drawing.Size(230, 35);
            this.moneyEditTB.TabIndex = 4;
            this.moneyEditTB.Text = "100";
            this.moneyEditTB.TextChanged += new System.EventHandler(this.moneyEditTB_TextChanged);
            // 
            // curMoneyText
            // 
            this.curMoneyText.AutoSize = true;
            this.curMoneyText.Location = new System.Drawing.Point(45, 122);
            this.curMoneyText.Name = "curMoneyText";
            this.curMoneyText.Size = new System.Drawing.Size(152, 28);
            this.curMoneyText.TabIndex = 3;
            this.curMoneyText.Text = "9999999999";
            this.curMoneyText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 90);
            this.label1.Margin = new System.Windows.Forms.Padding(30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "当前代币数:";
            // 
            // addUserBtn
            // 
            this.addUserBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addUserBtn.BackColor = System.Drawing.Color.Salmon;
            this.addUserBtn.ForeColor = System.Drawing.Color.White;
            this.addUserBtn.Location = new System.Drawing.Point(0, 568);
            this.addUserBtn.Margin = new System.Windows.Forms.Padding(0);
            this.addUserBtn.Name = "addUserBtn";
            this.addUserBtn.Size = new System.Drawing.Size(1096, 75);
            this.addUserBtn.TabIndex = 1;
            this.addUserBtn.Text = "增加用户";
            this.addUserBtn.UseVisualStyleBackColor = false;
            this.addUserBtn.Click += new System.EventHandler(this.addUserBtn_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.userGrid);
            this.panel3.Controls.Add(this.addUserBtn);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1096, 643);
            this.panel3.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 178);
            this.label2.Margin = new System.Windows.Forms.Padding(30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 28);
            this.label2.TabIndex = 10;
            this.label2.Text = "代币编辑:";
            // 
            // id
            // 
            this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.id.HeaderText = "编号";
            this.id.MinimumWidth = 9;
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 95;
            // 
            // userName
            // 
            this.userName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.userName.HeaderText = "账户名";
            this.userName.MinimumWidth = 9;
            this.userName.Name = "userName";
            this.userName.ReadOnly = true;
            this.userName.Width = 116;
            // 
            // coinNum
            // 
            this.coinNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.coinNum.HeaderText = "代币数量";
            this.coinNum.MinimumWidth = 9;
            this.coinNum.Name = "coinNum";
            this.coinNum.ReadOnly = true;
            this.coinNum.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // coinEdit
            // 
            this.coinEdit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.coinEdit.HeaderText = "代币操作";
            this.coinEdit.MinimumWidth = 9;
            this.coinEdit.Name = "coinEdit";
            this.coinEdit.ReadOnly = true;
            this.coinEdit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.coinEdit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.coinEdit.Width = 137;
            // 
            // userControl
            // 
            this.userControl.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.userControl.HeaderText = "用户操作";
            this.userControl.MinimumWidth = 9;
            this.userControl.Name = "userControl";
            this.userControl.ReadOnly = true;
            this.userControl.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.userControl.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.userControl.Width = 137;
            // 
            // defaultPassword
            // 
            this.defaultPassword.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.defaultPassword.HeaderText = "还原初始密码";
            this.defaultPassword.MinimumWidth = 9;
            this.defaultPassword.Name = "defaultPassword";
            this.defaultPassword.ReadOnly = true;
            this.defaultPassword.Width = 144;
            // 
            // getMes
            // 
            this.getMes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.getMes.HeaderText = "查询";
            this.getMes.MinimumWidth = 9;
            this.getMes.Name = "getMes";
            this.getMes.ReadOnly = true;
            this.getMes.Width = 60;
            // 
            // fmManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1096, 643);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("小米兰亭", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fmManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "账户配置";
            this.Shown += new System.EventHandler(this.fmManager_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.userGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView userGrid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button subMoneyBtn;
        private System.Windows.Forms.Button addMoneyBtn;
        private System.Windows.Forms.TextBox moneyEditTB;
        private System.Windows.Forms.Label curMoneyText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addUserBtn;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn userName;
        private System.Windows.Forms.DataGridViewTextBoxColumn coinNum;
        private System.Windows.Forms.DataGridViewButtonColumn coinEdit;
        private System.Windows.Forms.DataGridViewButtonColumn userControl;
        private System.Windows.Forms.DataGridViewButtonColumn defaultPassword;
        private System.Windows.Forms.DataGridViewButtonColumn getMes;
    }
}