using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace OrquestraErrorLogParser
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            string clipboard;
            InitializeComponent();

            this.btnParse.Enabled = false;
            this.btnCancel.Enabled = false;

            clipboard = Clipboard.GetText();

            if (!string.IsNullOrEmpty(clipboard)) {

                this.txtFolder.Text = clipboard.Replace("\r", "").Replace("\n", "").ToString();

            }

            if (!string.IsNullOrEmpty(this.txtFolder.Text))
            {
                this.btnParse.Enabled = true;
            }

            txtEncryptionKey.Text = OrquestraErrorLogParser.Encryption.AES_DEFAULT_KEY;

            /*
            this.txtFolder.Text += System.DateTime.Now.Year.ToString() +
                System.DateTime.Now.Month.ToString().PadLeft(2, '0') +
                System.DateTime.Now.Day.ToString().PadLeft(2, '0');
            */

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            ValidateFormAndParseFiles();
        }

        private void ValidateFormAndParseFiles()
        {
            DirectoryValidation validator = new DirectoryValidation();

            if (string.IsNullOrEmpty(this.txtFolder.Text))
            {
                this.lblResult.Text = "Diretório não informado";
            }
            else if (!validator.DirectoryExists(this.txtFolder.Text))
            {
                this.lblResult.Text = "Diretório não existe";
            }
            else if (!validator.DirectoryHasFiles(this.txtFolder.Text))
            {
                this.lblResult.Text = "Diretório não contém arquivos .txt";
            }
            else
            {
                this.btnParse.Text = "Processando";
                this.lblResult.Text = "Processando";
                this.btnParse.Enabled = false;
                this.txtFolder.Enabled = false;
                this.btnCancel.Enabled = true;

                this.txtDetails.Text = "";

                ParseFiles();

            }
        }

        private void ParseFiles()
        {
            this.txtDetails.Text += "Iniciando processamento..." + System.Environment.NewLine;
            this.progressBar1.Visible = true;
            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = this.progressBar1.Minimum;

            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
            }

        }

        private void txtFolder_TextChanged(object sender, EventArgs e)
        {
            this.btnParse.Enabled = true;
            this.lblResult.Text = "";
        }
        
        private void txtFolder_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ValidateFormAndParseFiles();
            }
        }

        private void txtEncryptionKey_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ValidateFormAndParseFiles();
            }
        }

        private void rbDes_CheckedChanged(object sender, EventArgs e)
        {
            txtEncryptionKey.Text = OrquestraErrorLogParser.Encryption.DES_DEFAULT_KEY;
        }

        private void rbAES_CheckedChanged(object sender, EventArgs e)
        {
            txtEncryptionKey.Text = OrquestraErrorLogParser.Encryption.AES_DEFAULT_KEY;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                LogParser logParser = new LogParser(this.txtFolder.Text, 
                    (rbAES.Checked ? "AES" : "DES"), 
                    txtEncryptionKey.Text, 
                    worker, 
                    e);
                logParser.ParseFiles();
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string userState = string.Empty;

            if (e.UserState != null)
            {
                userState = (string)e.UserState;
                this.txtDetails.Text += userState;
            }

            this.progressBar1.Value = e.ProgressPercentage;


        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // atualizar status
            this.progressBar1.Value = this.progressBar1.Maximum;
            this.txtDetails.Text += "Fim do processamento." + System.Environment.NewLine;
            this.lblResult.Text = "Concluído";

            // atualizar visibilidade do label
            this.lblResult.Visible = true;

            // atualizar situacao dos botoes
            this.btnCancel.Enabled = false;
            this.btnParse.Enabled = true;
            this.txtFolder.Enabled = true;

            // atualizar texto dos botoes
            this.btnParse.Text = "Processar logs";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Cancel the asynchronous operation.
            this.backgroundWorker1.CancelAsync();

            // Disable the Cancel button.
            this.btnCancel.Enabled = false;
            this.btnParse.Enabled = true;
        }
    }
}
