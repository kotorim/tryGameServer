
namespace Baccarat_Client_Manager.Forms
{
    partial class fileUpload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fileUpload));
            this.mainVideoGrid = new System.Windows.Forms.DataGridView();
            this.fileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.showEach = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.prevMain = new System.Windows.Forms.Button();
            this.nextMain = new System.Windows.Forms.Button();
            this.uploadMainBtn = new System.Windows.Forms.Button();
            this.jsonFile = new System.Windows.Forms.OpenFileDialog();
            this.videoFile = new System.Windows.Forms.OpenFileDialog();
            this.uploadNapBtn = new System.Windows.Forms.Button();
            this.napVideoGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nextNap = new System.Windows.Forms.Button();
            this.prevNap = new System.Windows.Forms.Button();
            this.message = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.mainVideoGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.napVideoGrid)).BeginInit();
            this.message.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainVideoGrid
            // 
            this.mainVideoGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mainVideoGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fileName});
            this.mainVideoGrid.Location = new System.Drawing.Point(50, 50);
            this.mainVideoGrid.Margin = new System.Windows.Forms.Padding(25);
            this.mainVideoGrid.Name = "mainVideoGrid";
            this.mainVideoGrid.RowHeadersWidth = 72;
            this.mainVideoGrid.RowTemplate.Height = 33;
            this.mainVideoGrid.Size = new System.Drawing.Size(462, 434);
            this.mainVideoGrid.TabIndex = 0;
            // 
            // fileName
            // 
            this.fileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fileName.HeaderText = "文件名";
            this.fileName.MinimumWidth = 9;
            this.fileName.Name = "fileName";
            this.fileName.ReadOnly = true;
            // 
            // showEach
            // 
            this.showEach.Font = new System.Drawing.Font("小米兰亭", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.showEach.FormattingEnabled = true;
            this.showEach.Items.AddRange(new object[] {
            "10个",
            "20个",
            "50个",
            "100个"});
            this.showEach.Location = new System.Drawing.Point(472, 579);
            this.showEach.Name = "showEach";
            this.showEach.Size = new System.Drawing.Size(121, 36);
            this.showEach.TabIndex = 2;
            this.showEach.SelectedIndexChanged += new System.EventHandler(this.showEach_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("小米兰亭", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(271, 582);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 28);
            this.label1.TabIndex = 3;
            this.label1.Text = "每页显示个数：";
            // 
            // prevMain
            // 
            this.prevMain.Font = new System.Drawing.Font("小米兰亭", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.prevMain.Location = new System.Drawing.Point(513, 50);
            this.prevMain.Name = "prevMain";
            this.prevMain.Size = new System.Drawing.Size(80, 172);
            this.prevMain.TabIndex = 4;
            this.prevMain.Text = "上\r\n一\r\n页";
            this.prevMain.UseVisualStyleBackColor = true;
            this.prevMain.Click += new System.EventHandler(this.prevPage_Click);
            // 
            // nextMain
            // 
            this.nextMain.Font = new System.Drawing.Font("小米兰亭", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nextMain.Location = new System.Drawing.Point(513, 315);
            this.nextMain.Name = "nextMain";
            this.nextMain.Size = new System.Drawing.Size(80, 169);
            this.nextMain.TabIndex = 5;
            this.nextMain.Text = "下\r\n一\r\n页";
            this.nextMain.UseVisualStyleBackColor = true;
            this.nextMain.Click += new System.EventHandler(this.nextPage_Click);
            // 
            // uploadMainBtn
            // 
            this.uploadMainBtn.Font = new System.Drawing.Font("小米兰亭", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uploadMainBtn.Location = new System.Drawing.Point(733, 512);
            this.uploadMainBtn.Name = "uploadMainBtn";
            this.uploadMainBtn.Size = new System.Drawing.Size(227, 103);
            this.uploadMainBtn.TabIndex = 6;
            this.uploadMainBtn.Text = "上传对局视频";
            this.uploadMainBtn.UseVisualStyleBackColor = true;
            this.uploadMainBtn.Click += new System.EventHandler(this.uploadButton_Click);
            // 
            // jsonFile
            // 
            this.jsonFile.Filter = "*.json|*.json";
            this.jsonFile.Multiselect = true;
            this.jsonFile.Title = "选择json文件";
            // 
            // videoFile
            // 
            this.videoFile.Filter = "*.mp4|*.mp4";
            this.videoFile.Multiselect = true;
            this.videoFile.Title = "选择视频文件";
            // 
            // uploadNapBtn
            // 
            this.uploadNapBtn.Font = new System.Drawing.Font("小米兰亭", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uploadNapBtn.Location = new System.Drawing.Point(1049, 512);
            this.uploadNapBtn.Name = "uploadNapBtn";
            this.uploadNapBtn.Size = new System.Drawing.Size(227, 103);
            this.uploadNapBtn.TabIndex = 8;
            this.uploadNapBtn.Text = "上传间歇视频";
            this.uploadNapBtn.UseVisualStyleBackColor = true;
            this.uploadNapBtn.Click += new System.EventHandler(this.uploadNapBtn_Click);
            // 
            // napVideoGrid
            // 
            this.napVideoGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.napVideoGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
            this.napVideoGrid.Location = new System.Drawing.Point(814, 50);
            this.napVideoGrid.Margin = new System.Windows.Forms.Padding(25);
            this.napVideoGrid.Name = "napVideoGrid";
            this.napVideoGrid.RowHeadersWidth = 72;
            this.napVideoGrid.RowTemplate.Height = 33;
            this.napVideoGrid.Size = new System.Drawing.Size(462, 434);
            this.napVideoGrid.TabIndex = 9;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "文件名";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 9;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // nextNap
            // 
            this.nextNap.Font = new System.Drawing.Font("小米兰亭", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nextNap.Location = new System.Drawing.Point(733, 315);
            this.nextNap.Name = "nextNap";
            this.nextNap.Size = new System.Drawing.Size(80, 169);
            this.nextNap.TabIndex = 11;
            this.nextNap.Text = "下\r\n一\r\n页";
            this.nextNap.UseVisualStyleBackColor = true;
            this.nextNap.Click += new System.EventHandler(this.nextNap_Click);
            // 
            // prevNap
            // 
            this.prevNap.Font = new System.Drawing.Font("小米兰亭", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.prevNap.Location = new System.Drawing.Point(733, 50);
            this.prevNap.Name = "prevNap";
            this.prevNap.Size = new System.Drawing.Size(80, 172);
            this.prevNap.TabIndex = 10;
            this.prevNap.Text = "上\r\n一\r\n页";
            this.prevNap.UseVisualStyleBackColor = true;
            this.prevNap.Click += new System.EventHandler(this.prevNap_Click);
            // 
            // message
            // 
            this.message.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.message.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.message.Location = new System.Drawing.Point(0, 647);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(1326, 37);
            this.message.TabIndex = 12;
            this.message.Text = "aaa";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 28);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Image = global::Baccarat_Client_Manager.Properties.Resources._018e615abc4276a8012062e3c4ce132;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(46, 28);
            this.toolStripStatusLabel2.Text = " ";
            // 
            // fileUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(168F, 168F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1326, 684);
            this.Controls.Add(this.message);
            this.Controls.Add(this.nextNap);
            this.Controls.Add(this.prevNap);
            this.Controls.Add(this.napVideoGrid);
            this.Controls.Add(this.uploadNapBtn);
            this.Controls.Add(this.uploadMainBtn);
            this.Controls.Add(this.nextMain);
            this.Controls.Add(this.prevMain);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.showEach);
            this.Controls.Add(this.mainVideoGrid);
            this.Font = new System.Drawing.Font("小米兰亭", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "fileUpload";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "视频管理";
            ((System.ComponentModel.ISupportInitialize)(this.mainVideoGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.napVideoGrid)).EndInit();
            this.message.ResumeLayout(false);
            this.message.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView mainVideoGrid;
        private System.Windows.Forms.ComboBox showEach;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button prevMain;
        private System.Windows.Forms.Button nextMain;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileName;
        private System.Windows.Forms.Button uploadMainBtn;
        private System.Windows.Forms.OpenFileDialog jsonFile;
        private System.Windows.Forms.OpenFileDialog videoFile;
        private System.Windows.Forms.Button uploadNapBtn;
        private System.Windows.Forms.DataGridView napVideoGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.Button nextNap;
        private System.Windows.Forms.Button prevNap;
        private System.Windows.Forms.StatusStrip message;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    }
}