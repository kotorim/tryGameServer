using Baccarat_Server.Forms;
using System;
using System.Windows.Forms;
using TouchSocket.Sockets;

namespace Baccarat_Server
{
    public partial class Baccarat_Server : Form
    {
        public Baccarat_Server()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            if (true == webHelper.connectDatabase("mongodb://" + dataBaseIP.Text + ":" + dataBasePort.Text))
            {
                webHelper.startService(new IPHost[] { new IPHost(serverIP.Text + ":" + serverPort.Text) });
                this.Hide();
                var form = new workingForm();
                form.FormClosed += new FormClosedEventHandler((a, b) =>
                {
                    this.Close();
                });
                form.Show();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            webHelper.disposeService();
        }
    }

}
