
namespace Baccarat_Client_Manager.Forms
{
    partial class mainManagerAdmin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainManagerAdmin));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.账户管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.账户配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.账户监管ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.server管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据备份ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.视频管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系统设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backUpFileDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.zhuangWinActualOdds = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.账户管理ToolStripMenuItem,
            this.server管理ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(999, 36);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 账户管理ToolStripMenuItem
            // 
            this.账户管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.账户配置ToolStripMenuItem,
            this.账户监管ToolStripMenuItem});
            this.账户管理ToolStripMenuItem.Name = "账户管理ToolStripMenuItem";
            this.账户管理ToolStripMenuItem.Size = new System.Drawing.Size(114, 32);
            this.账户管理ToolStripMenuItem.Text = "账户管理";
            // 
            // 账户配置ToolStripMenuItem
            // 
            this.账户配置ToolStripMenuItem.Name = "账户配置ToolStripMenuItem";
            this.账户配置ToolStripMenuItem.Size = new System.Drawing.Size(315, 40);
            this.账户配置ToolStripMenuItem.Text = "账户配置";
            this.账户配置ToolStripMenuItem.Click += new System.EventHandler(this.账户配置ToolStripMenuItem_Click);
            // 
            // 账户监管ToolStripMenuItem
            // 
            this.账户监管ToolStripMenuItem.Name = "账户监管ToolStripMenuItem";
            this.账户监管ToolStripMenuItem.Size = new System.Drawing.Size(315, 40);
            this.账户监管ToolStripMenuItem.Text = "账户监管";
            this.账户监管ToolStripMenuItem.Click += new System.EventHandler(this.账户监管ToolStripMenuItem_Click);
            // 
            // server管理ToolStripMenuItem
            // 
            this.server管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.数据备份ToolStripMenuItem,
            this.视频管理ToolStripMenuItem,
            this.系统设置ToolStripMenuItem});
            this.server管理ToolStripMenuItem.Name = "server管理ToolStripMenuItem";
            this.server管理ToolStripMenuItem.Size = new System.Drawing.Size(135, 32);
            this.server管理ToolStripMenuItem.Text = "Server管理";
            // 
            // 数据备份ToolStripMenuItem
            // 
            this.数据备份ToolStripMenuItem.Name = "数据备份ToolStripMenuItem";
            this.数据备份ToolStripMenuItem.Size = new System.Drawing.Size(213, 40);
            this.数据备份ToolStripMenuItem.Text = "数据备份";
            this.数据备份ToolStripMenuItem.Click += new System.EventHandler(this.数据备份ToolStripMenuItem_Click);
            // 
            // 视频管理ToolStripMenuItem
            // 
            this.视频管理ToolStripMenuItem.Name = "视频管理ToolStripMenuItem";
            this.视频管理ToolStripMenuItem.Size = new System.Drawing.Size(213, 40);
            this.视频管理ToolStripMenuItem.Text = "视频管理";
            this.视频管理ToolStripMenuItem.Click += new System.EventHandler(this.视频管理ToolStripMenuItem_Click);
            // 
            // 系统设置ToolStripMenuItem
            // 
            this.系统设置ToolStripMenuItem.Name = "系统设置ToolStripMenuItem";
            this.系统设置ToolStripMenuItem.Size = new System.Drawing.Size(213, 40);
            this.系统设置ToolStripMenuItem.Text = "系统设置";
            this.系统设置ToolStripMenuItem.Click += new System.EventHandler(this.系统设置ToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62.05534F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.94466F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 136F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 296F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.zhuangWinActualOdds, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(53, 70);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(803, 299);
            this.tableLayoutPanel1.TabIndex = 1;
            this.tableLayoutPanel1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 28);
            this.label2.TabIndex = 2;
            this.label2.Text = "label2";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(115, 152);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(62, 35);
            this.textBox2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 56);
            this.label1.TabIndex = 0;
            this.label1.Text = "庄胜实际赔率";
            // 
            // zhuangWinActualOdds
            // 
            this.zhuangWinActualOdds.Location = new System.Drawing.Point(115, 3);
            this.zhuangWinActualOdds.Name = "zhuangWinActualOdds";
            this.zhuangWinActualOdds.Size = new System.Drawing.Size(62, 35);
            this.zhuangWinActualOdds.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(183, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 56);
            this.label3.TabIndex = 4;
            this.label3.Text = "庄胜实际赔率";
            // 
            // mainManagerAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(999, 619);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("小米兰亭", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "mainManagerAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "mainManagerAdmin";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 账户管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 账户配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 账户监管ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem server管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数据备份ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 视频管理ToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog backUpFileDialog;
        private System.Windows.Forms.ToolStripMenuItem 系统设置ToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox zhuangWinActualOdds;
        private System.Windows.Forms.Label label3;
    }
}