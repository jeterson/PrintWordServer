using PrintWordServerApp.Properties;
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
        private NotifyIcon trayIcon;
        MenuItem sair = new MenuItem("Sair");
        MenuItem parar = new MenuItem("Parar");
        MenuItem iniciar = new MenuItem("Inicar");

        public Form1()
        {
            InitializeComponent();
            btnStopServer.Enabled = false;
            txtPort.Text = Configuration.Singleton().Port.ToString();
            String ip = Configuration.Singleton().GetLocalIP();
            // String msg = "O servidor está acessivel na rede pelo IP: " + Configuration.Singleton().GetLocalIP();
            //MessageBox.Show(Configuration.Singleton().GetLocalIP());
            lblAcessivelRede.Text = Configuration.Singleton().IsRunningAdministrator() ? "O servidor está acessivel na rede pelo IP: " + ip : "O servidor não está acessível em rede";
            this.lblInfo.Text = "";

            this.sair.Click += Exit;
            this.parar.Click += Stop;
            this.iniciar.Click += Start;
            this.criarIconeBandeja();
           
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            Start(sender, e);
        }

        private void btnStopServer_Click(object sender, EventArgs e)
        {
            Stop(sender, e);
        }

        private void criarIconeBandeja()
        {
            parar.Enabled = Configuration.Singleton().IsServerListening();
            iniciar.Enabled = !Configuration.Singleton().IsServerListening();

            notifyIcon1.ContextMenu = new ContextMenu(new MenuItem[] {
                sair,
                iniciar, parar
            });
            notifyIcon1.Visible = true;
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Deseja realmente fechar? \r\nPara manter o servidor rodando em segundo plano, clique em Não. Para sair clique em Sim", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
                this.Hide();
            }
            
        }

        private void btnRunningBackground_Click(object sender, EventArgs e)
        {
            this.Hide();
            notifyIcon1.ShowBalloonTip(500);
            // this.criarIconeBandeja();
            

        }

        void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
             //notifyIcon1.Visible = false;
            this.Stop(sender, e); 
            this.Close(); 
            Application.Exit();
        }

        void Start(object sender, EventArgs e)
        {
            Configuration.Singleton().ConfigureAndStart();
            parar.Enabled = Configuration.Singleton().IsServerListening();
            iniciar.Enabled = !Configuration.Singleton().IsServerListening();

            if (Configuration.Singleton().IsServerListening())
            {
                btnStartServer.Enabled = false;
                btnStopServer.Enabled = true;
                txtPort.Enabled = false;
                lblInfo.Text = "Server has been started ";
            }
        }

        void Stop(object sender, EventArgs e)
        {            

            Configuration.Singleton().Stop();

            if (!Configuration.Singleton().IsServerListening())
            {
                btnStartServer.Enabled = true;
                btnStopServer.Enabled = false;
                txtPort.Enabled = true;
                lblInfo.Text = "Server has been stopped ";
            }
            parar.Enabled = Configuration.Singleton().IsServerListening();
            iniciar.Enabled = !Configuration.Singleton().IsServerListening();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
        }
    }
}
