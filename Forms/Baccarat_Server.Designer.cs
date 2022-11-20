
namespace Baccarat_Server
{
    partial class Baccarat_Server
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Baccarat_Server));
            this.serverPort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.serverIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataBasePort = new System.Windows.Forms.TextBox();
            this.Start = new System.Windows.Forms.Button();
            this.dataBaseIP = new System.Windows.Forms.TextBox();
            this.startPanel = new System.Windows.Forms.Panel();
            this.startPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // serverPort
            // 
            this.serverPort.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverPort.Font = new System.Drawing.Font("微软雅黑", 10.71429F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.serverPort.Location = new System.Drawing.Point(190, 65);
            this.serverPort.Margin = new System.Windows.Forms.Padding(4);
            this.serverPort.MaxLength = 15;
            this.serverPort.Name = "serverPort";
            this.serverPort.Size = new System.Drawing.Size(249, 40);
            this.serverPort.TabIndex = 3;
            this.serverPort.Text = "9998";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.71429F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(14, 108);
            this.label4.Margin = new System.Windows.Forms.Padding(10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 35);
            this.label4.TabIndex = 5;
            this.label4.Text = "数据库ip：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.71429F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(14, 68);
            this.label2.Margin = new System.Windows.Forms.Padding(10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 35);
            this.label2.TabIndex = 2;
            this.label2.Text = "服务器端口：";
            // 
            // serverIP
            // 
            this.serverIP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.serverIP.Font = new System.Drawing.Font("微软雅黑", 10.71429F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.serverIP.Location = new System.Drawing.Point(190, 25);
            this.serverIP.Margin = new System.Windows.Forms.Padding(4);
            this.serverIP.MaxLength = 15;
            this.serverIP.Name = "serverIP";
            this.serverIP.Size = new System.Drawing.Size(249, 40);
            this.serverIP.TabIndex = 1;
            this.serverIP.Text = "0.0.0.0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.71429F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(14, 148);
            this.label3.Margin = new System.Windows.Forms.Padding(10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 35);
            this.label3.TabIndex = 7;
            this.label3.Text = "数据库端口：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.71429F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(14, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器ip：";
            // 
            // dataBasePort
            // 
            this.dataBasePort.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataBasePort.Font = new System.Drawing.Font("微软雅黑", 10.71429F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataBasePort.Location = new System.Drawing.Point(190, 145);
            this.dataBasePort.Margin = new System.Windows.Forms.Padding(4);
            this.dataBasePort.MaxLength = 15;
            this.dataBasePort.Name = "dataBasePort";
            this.dataBasePort.Size = new System.Drawing.Size(249, 40);
            this.dataBasePort.TabIndex = 8;
            this.dataBasePort.Text = "27017";
            // 
            // Start
            // 
            this.Start.BackColor = System.Drawing.Color.Salmon;
            this.Start.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Start.Font = new System.Drawing.Font("微软雅黑", 10.71429F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Start.Location = new System.Drawing.Point(0, 209);
            this.Start.Margin = new System.Windows.Forms.Padding(4);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(456, 68);
            this.Start.TabIndex = 4;
            this.Start.Text = "开     始";
            this.Start.UseVisualStyleBackColor = false;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // dataBaseIP
            // 
            this.dataBaseIP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataBaseIP.Font = new System.Drawing.Font("微软雅黑", 10.71429F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataBaseIP.Location = new System.Drawing.Point(190, 105);
            this.dataBaseIP.Margin = new System.Windows.Forms.Padding(4);
            this.dataBaseIP.MaxLength = 15;
            this.dataBaseIP.Name = "dataBaseIP";
            this.dataBaseIP.Size = new System.Drawing.Size(249, 40);
            this.dataBaseIP.TabIndex = 6;
            this.dataBaseIP.Text = "127.0.0.1";
            // 
            // startPanel
            // 
            this.startPanel.BackColor = System.Drawing.Color.White;
            this.startPanel.Controls.Add(this.dataBaseIP);
            this.startPanel.Controls.Add(this.Start);
            this.startPanel.Controls.Add(this.dataBasePort);
            this.startPanel.Controls.Add(this.label1);
            this.startPanel.Controls.Add(this.label3);
            this.startPanel.Controls.Add(this.serverIP);
            this.startPanel.Controls.Add(this.label2);
            this.startPanel.Controls.Add(this.label4);
            this.startPanel.Controls.Add(this.serverPort);
            this.startPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startPanel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.startPanel.Location = new System.Drawing.Point(0, 0);
            this.startPanel.Margin = new System.Windows.Forms.Padding(4);
            this.startPanel.Name = "startPanel";
            this.startPanel.Size = new System.Drawing.Size(456, 277);
            this.startPanel.TabIndex = 9;
            // 
            // Baccarat_Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(456, 277);
            this.Controls.Add(this.startPanel);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Baccarat_Server";
            this.Text = "Baccarat_Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.startPanel.ResumeLayout(false);
            this.startPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox serverPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox serverIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox dataBasePort;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.TextBox dataBaseIP;
        private System.Windows.Forms.Panel startPanel;
    }
}

