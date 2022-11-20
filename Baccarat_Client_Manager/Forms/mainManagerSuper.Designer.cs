
namespace Baccarat_Client_Manager.Forms
{
    partial class mainManagerSuper
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainManagerSuper));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.菜单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_ResetAdminPassword = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_ResetAdminPassword = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_ResetAdminPassword = new System.Windows.Forms.Button();
            this.newPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.reNewPassword = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.panel_ResetAdminPassword.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.菜单ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(968, 41);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 菜单ToolStripMenuItem
            // 
            this.菜单ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_ResetAdminPassword});
            this.菜单ToolStripMenuItem.Name = "菜单ToolStripMenuItem";
            this.菜单ToolStripMenuItem.Size = new System.Drawing.Size(81, 37);
            this.菜单ToolStripMenuItem.Text = "菜单";
            // 
            // menu_ResetAdminPassword
            // 
            this.menu_ResetAdminPassword.Name = "menu_ResetAdminPassword";
            this.menu_ResetAdminPassword.Size = new System.Drawing.Size(303, 42);
            this.menu_ResetAdminPassword.Text = "修改控制端密码";
            this.menu_ResetAdminPassword.Click += new System.EventHandler(this.resetAdminPassword_Click);
            // 
            // panel_ResetAdminPassword
            // 
            this.panel_ResetAdminPassword.Controls.Add(this.panel1);
            this.panel_ResetAdminPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ResetAdminPassword.Location = new System.Drawing.Point(0, 41);
            this.panel_ResetAdminPassword.Name = "panel_ResetAdminPassword";
            this.panel_ResetAdminPassword.Padding = new System.Windows.Forms.Padding(250, 100, 250, 100);
            this.panel_ResetAdminPassword.Size = new System.Drawing.Size(968, 553);
            this.panel_ResetAdminPassword.TabIndex = 1;
            this.panel_ResetAdminPassword.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_ResetAdminPassword);
            this.panel1.Controls.Add(this.newPassword);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.reNewPassword);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(250, 100);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(40, 0, 40, 0);
            this.panel1.Size = new System.Drawing.Size(468, 353);
            this.panel1.TabIndex = 3;
            // 
            // btn_ResetAdminPassword
            // 
            this.btn_ResetAdminPassword.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_ResetAdminPassword.Location = new System.Drawing.Point(40, 296);
            this.btn_ResetAdminPassword.Name = "btn_ResetAdminPassword";
            this.btn_ResetAdminPassword.Size = new System.Drawing.Size(388, 57);
            this.btn_ResetAdminPassword.TabIndex = 4;
            this.btn_ResetAdminPassword.Text = "确认修改";
            this.btn_ResetAdminPassword.UseVisualStyleBackColor = true;
            this.btn_ResetAdminPassword.Click += new System.EventHandler(this.btn_ResetAdminPassword_Click);
            // 
            // newPassword
            // 
            this.newPassword.Location = new System.Drawing.Point(150, 25);
            this.newPassword.Margin = new System.Windows.Forms.Padding(10);
            this.newPassword.Name = "newPassword";
            this.newPassword.Size = new System.Drawing.Size(268, 35);
            this.newPassword.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 155);
            this.label1.Margin = new System.Windows.Forms.Padding(10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "重复输入:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 28);
            this.label2.TabIndex = 1;
            this.label2.Text = "新密码:";
            // 
            // reNewPassword
            // 
            this.reNewPassword.Location = new System.Drawing.Point(150, 152);
            this.reNewPassword.Name = "reNewPassword";
            this.reNewPassword.Size = new System.Drawing.Size(268, 35);
            this.reNewPassword.TabIndex = 2;
            // 
            // mainManagerSuper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(968, 594);
            this.Controls.Add(this.panel_ResetAdminPassword);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "mainManagerSuper";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "mainManagerSuper";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel_ResetAdminPassword.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 菜单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_ResetAdminPassword;
        private System.Windows.Forms.Panel panel_ResetAdminPassword;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_ResetAdminPassword;
        private System.Windows.Forms.TextBox newPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox reNewPassword;
    }
}