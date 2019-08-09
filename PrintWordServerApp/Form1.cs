using ReplaceWordServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrintWordServerApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btnStopServer.Enabled = false;
            txtPort.Text = Configuration.Singleton().Port.ToString();
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            Configuration.Singleton().ConfigureAndStart();

            if (Configuration.Singleton().IsServerListening())
            {
                btnStartServer.Enabled = false;
                btnStopServer.Enabled = true;
                txtPort.Enabled = false;
                lblInfo.Text = "Server has been started ";
            }
        }

        private void btnStopServer_Click(object sender, EventArgs e)
        {
            Configuration.Singleton().Stop();

            if (!Configuration.Singleton().IsServerListening())
            {
                btnStartServer.Enabled = true;
                btnStopServer.Enabled = false;
                txtPort.Enabled = true;
                lblInfo.Text = "Server has been stopped ";
            }
        }

        private void txtPort_Leave(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtPort.Text))
            {
                try
                {
                    if (Convert.ToInt32(txtPort.Text) != Configuration.Singleton().Port)
                    {
                        Configuration.Singleton().Port = Convert.ToInt32(txtPort.Text);

                        lblInfo.Text = "Port has been changed to " + Configuration.Singleton().Port;
                    }
                }
                catch
                {
                    lblInfo.Text = "An error occurred in port number. Default port will be used " + Configuration.Singleton().Port;
                }
            }           
            
        }
    }
}
