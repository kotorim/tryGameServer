
namespace Baccarat_Server.Forms
{
    partial class workingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(workingForm));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.channelList = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playingVideo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isMain = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tickTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.重置全部userObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全体新增kvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastResponse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.channelList)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // channelList
            // 
            this.channelList.AllowUserToAddRows = false;
            this.channelList.AllowUserToDeleteRows = false;
            this.channelList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.channelList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.playingVideo,
            this.isMain,
            this.tickTime});
            this.channelList.Location = new System.Drawing.Point(12, 94);
            this.channelList.Name = "channelList";
            this.channelList.ReadOnly = true;
            this.channelList.RowHeadersWidth = 72;
            this.channelList.RowTemplate.Height = 33;
            this.channelList.Size = new System.Drawing.Size(701, 543);
            this.channelList.TabIndex = 0;
            // 
            // id
            // 
            this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.id.HeaderText = "id";
            this.id.MinimumWidth = 9;
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 72;
            // 
            // playingVideo
            // 
            this.playingVideo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.playingVideo.HeaderText = "正在播放的视频";
            this.playingVideo.MinimumWidth = 9;
            this.playingVideo.Name = "playingVideo";
            this.playingVideo.ReadOnly = true;
            // 
            // isMain
            // 
            this.isMain.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.isMain.HeaderText = "视频类型";
            this.isMain.MinimumWidth = 9;
            this.isMain.Name = "isMain";
            this.isMain.ReadOnly = true;
            this.isMain.Width = 137;
            // 
            // tickTime
            // 
            this.tickTime.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.tickTime.HeaderText = "播放时间";
            this.tickTime.MinimumWidth = 9;
            this.tickTime.Name = "tickTime";
            this.tickTime.ReadOnly = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status,
            this.toolStripSplitButton1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 649);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1351, 36);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(0, 27);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.重置全部userObjectToolStripMenuItem,
            this.全体新增kvToolStripMenuItem});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(52, 32);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            // 
            // 重置全部userObjectToolStripMenuItem
            // 
            this.重置全部userObjectToolStripMenuItem.Name = "重置全部userObjectToolStripMenuItem";
            this.重置全部userObjectToolStripMenuItem.Size = new System.Drawing.Size(323, 40);
            this.重置全部userObjectToolStripMenuItem.Text = "重置全部userObject";
            this.重置全部userObjectToolStripMenuItem.Click += new System.EventHandler(this.重置全部userObjectToolStripMenuItem_Click);
            // 
            // 全体新增kvToolStripMenuItem
            // 
            this.全体新增kvToolStripMenuItem.Name = "全体新增kvToolStripMenuItem";
            this.全体新增kvToolStripMenuItem.Size = new System.Drawing.Size(323, 40);
            this.全体新增kvToolStripMenuItem.Text = "全体新增kv";
            this.全体新增kvToolStripMenuItem.Click += new System.EventHandler(this.全体新增kvToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ip,
            this.lastResponse});
            this.dataGridView1.Location = new System.Drawing.Point(720, 94);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 72;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(619, 543);
            this.dataGridView1.TabIndex = 2;
            // 
            // ip
            // 
            this.ip.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ip.HeaderText = "ip地址";
            this.ip.MinimumWidth = 9;
            this.ip.Name = "ip";
            this.ip.ReadOnly = true;
            this.ip.Width = 114;
            // 
            // lastResponse
            // 
            this.lastResponse.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.lastResponse.HeaderText = "上次响应内容";
            this.lastResponse.MinimumWidth = 9;
            this.lastResponse.Name = "lastResponse";
            this.lastResponse.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 28);
            this.label1.TabIndex = 3;
            this.label1.Text = "频道信息:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(715, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 28);
            this.label2.TabIndex = 4;
            this.label2.Text = "传输信息:";
            // 
            // workingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1351, 685);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.channelList);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "workingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "workingForm";
            ((System.ComponentModel.ISupportInitialize)(this.channelList)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridView channelList;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn playingVideo;
        private System.Windows.Forms.DataGridViewTextBoxColumn isMain;
        private System.Windows.Forms.DataGridViewTextBoxColumn tickTime;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastResponse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem 重置全部userObjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全体新增kvToolStripMenuItem;
    }
}