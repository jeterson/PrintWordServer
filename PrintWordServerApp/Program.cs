using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrintWordServerApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            string nomeProcesso = Process.GetCurrentProcess().ProcessName;

            //Busca os processos com este nome que estão em execução
            Process[] processos = Process.GetProcessesByName(nomeProcesso);

            //Se já houver um aberto
            if (processos.Length > 1)
            {
                //Mostra mensagem de erro e finaliza
                MessageBox.Show("O aplicativo já está aberto Verifique na barra e tarefas ou na bandeja de notificações no lado inferior direito.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();                
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }


           
        }
    }
}
