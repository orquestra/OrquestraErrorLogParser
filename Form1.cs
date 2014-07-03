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

namespace OrquestraErrorLogParses
{
    public partial class Form1 : Form
    {
        const string BEGIN_LOG = "Begin Log:";
        const string LOG_CODE = "LogCode:";
        const string RAW_URL = "Raw URL:";
        const string ERROR_IN_PATH = "Error in Path:";
        const string ERROR_MESSAGE = "Error Message:";

        const string STACK_TRACE_LINE1 = "   em SefaFlow.";

        const string STACK_TRACE_LINE2 = "   em WorkFlowBR.";
        const string STACK_TRACE_LINE2B = "   em WorkFlowEngineBR.";

        const string STACK_TRACE_LINE3 = "   em WorkFlowDB.";
        const string STACK_TRACE_LINE3B = "   em WorkFlowEngineDB";

        const string ERROR_TARGET_SITE = "Error Target Site:";
        const string NEWLINE = "------------------------";
        const string REQUEST_DATA = "Request:";
        const string USER_AGENT = "	User-agent:";

        public Form1()
        {
            string clipboard;
            InitializeComponent();

            this.btnParse.Enabled = false;

            clipboard = Clipboard.GetText();

            if (!string.IsNullOrEmpty(clipboard)) {

                this.txtFolder.Text = clipboard.Replace("\r", "").Replace("\n", "").ToString();

            }

            if (!string.IsNullOrEmpty(this.txtFolder.Text))
            {
                this.btnParse.Enabled = true;
            }

            /*
            this.txtFolder.Text += System.DateTime.Now.Year.ToString() +
                System.DateTime.Now.Month.ToString().PadLeft(2, '0') +
                System.DateTime.Now.Day.ToString().PadLeft(2, '0');
            */
        }

        private void btnParse_Click(object sender, EventArgs e)
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
                this.btnParse.Enabled = false;
                this.txtFolder.Enabled = false;

                this.txtDetails.Text = "";

                ParseFiles();

                this.lblResult.Text = "Concluído";

                this.btnParse.Text = "Processar logs";
                this.btnParse.Enabled = true;
                this.txtFolder.Enabled = true;
            }
        }

        private void ParseFiles()
        {
            string[] filenames = Directory.GetFiles(this.txtFolder.Text, "*.txt");

            //string resultfull = "CodLog;Date;File;Error Message;Raw URL;Error in Path;Stack Trace;Request;UserAgent;int" + System.Environment.NewLine;
            string resultfull = "CodLog;Date;File;Error Message;Raw URL;Error in Path;Stack Trace;UserAgent;int" + System.Environment.NewLine;
            //filenames.Length;

            this.txtDetails.Text += "Iniciando processamento..." + System.Environment.NewLine;
            this.progressBar1.Visible = true;
            this.progressBar1.Minimum = 1;
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = this.progressBar1.Minimum;

            decimal step = 100 / filenames.Length;

            this.progressBar1.Step = Convert.ToInt32(Math.Round(step, 0));

            string fullFilePath = this.txtFolder.Text + "\\result_" + GetTimeStamp() + ".csv";

            System.IO.File.WriteAllText(fullFilePath, resultfull);

            resultfull = "";

            foreach (string filename in filenames)
            {
                //bool isRequestData = false;
                string line = "";
                string logcode = "";
                string beginlog = "";
                string rawUrl = "";
                string errorInPath = "";
                string stack1 = "";
                string stack2 = "";
                string stack3 = "";
                string errormessage = "";
                string result = "";
                //string request = "";
                string userAgent = "";

                System.IO.StreamReader file = new System.IO.StreamReader(filename);

                this.txtDetails.Text += "Processando..." + filename + System.Environment.NewLine;

                this.lblResult.Text = "Processando arquivo: " + filename;
                this.lblResult.Visible = true;

                while ((line = file.ReadLine()) != null)
                {

                    if ((line.StartsWith(NEWLINE)))
                    {
                        //isRequestData = false;


                        result = logcode;
                        result += ";" + beginlog;
                        result += ";" + filename.Trim();
                        result += ";" + errormessage.Trim();
                        result += ";" + rawUrl.Trim();
                        result += ";" + errorInPath.Trim();
                        result += ";" + stack1.Trim() + "------" + stack2.ToString() + "------" + stack3.Trim();
                        //result += ";\"" + request + "\"";
                        result += ";" + userAgent;
                        result += ";1";

                        if (!string.IsNullOrEmpty(result))
                        {
                            resultfull += result + Environment.NewLine;
                        }


                        stack1 = "";
                        stack2 = "";
                        stack3 = "";
                        beginlog = "";
                        errormessage = "";
                        //request = "";
                        userAgent = "";
                    }


                    if ((line.StartsWith(BEGIN_LOG)))
                    {
                        beginlog = line.Replace(BEGIN_LOG, "").Trim();
                    }

                    if ((line.StartsWith(LOG_CODE)))
                    {

                        logcode = line.Replace(LOG_CODE, "").Trim();
                    }

                    if ((line.StartsWith(RAW_URL)))
                    {
                        rawUrl = line.Replace(RAW_URL, "").Trim();
                    }

                    if ((line.StartsWith(ERROR_IN_PATH)))
                    {
                        errorInPath = line.Replace(ERROR_IN_PATH, "").Trim();
                    }

                    if (line.StartsWith(ERROR_MESSAGE))
                    {
                        errormessage = line.Replace(ERROR_MESSAGE, "").Replace(";", "").ToString();

                        if (errormessage.Contains("para realizar a tarefa. Por favor, consulte o administrador do sistema."))
                        {
                            errormessage = errormessage.Substring(errormessage.IndexOf("N"));
                        }

                        if (errormessage.Contains("(Process ID"))
                        {
                            errormessage = System.Text.RegularExpressions.Regex.Replace(errormessage, "Process ID [0-9]*", "");
                        }
                        if (errormessage.Contains("(ID do processo"))
                        {
                            errormessage = System.Text.RegularExpressions.Regex.Replace(errormessage, "ID do processo [0-9]*", "");
                        }

                    }

                    if (line.StartsWith(STACK_TRACE_LINE1) && string.IsNullOrEmpty(stack1))
                    {
                        stack1 = line.Replace(";", "").ToString();
                    }

                    if ((line.StartsWith(STACK_TRACE_LINE2) || line.StartsWith(STACK_TRACE_LINE2B)) && string.IsNullOrEmpty(stack2))
                    {
                        stack2 = line.Replace(";", "").ToString();
                    }

                    if (line.StartsWith(STACK_TRACE_LINE3) && string.IsNullOrEmpty(stack3))
                    {
                        stack3 = line.Replace(";", "").ToString();
                    }

                    if (line.StartsWith(USER_AGENT))
                    {
                        userAgent = line.Replace(";", "").ToString().Trim();
                    }

                    /*
                    if ((line.StartsWith(REQUEST_DATA) || isRequestData) && (line.IndexOf("__VIEWSTATE") == -1 && line.IndexOf("__EVENTVALIDATION") == -1))
                    {
                        if (line.StartsWith(REQUEST_DATA))
                        {
                            request = "";
                        }
                        isRequestData = true;
                        request += line.Replace(";", "").ToString() + System.Environment.NewLine;
                    }
                    */

                }
                file.Close();

                // append no aruqivo
                System.IO.File.AppendAllText(fullFilePath, resultfull);
                resultfull = "";

                this.progressBar1.PerformStep();

                Thread.Sleep(100);
            }

            this.progressBar1.Value = this.progressBar1.Maximum;
            this.txtDetails.Text += "Fim do processamento." + System.Environment.NewLine;

            //System.IO.File.AppendAllText(this.txtFolder.Text + "\\result_" + GetTimeStamp() + ".csv", resultfull);
            
        }

        private string GetTimeStamp() {
            string timestamp = System.DateTime.Now.Year.ToString() +
                System.DateTime.Now.Month.ToString().PadLeft(2, '0') +
                System.DateTime.Now.Day.ToString().PadLeft(2, '0') + "_" +
                System.DateTime.Now.Hour.ToString().PadLeft(2, '0') +
                System.DateTime.Now.Minute.ToString().PadLeft(2, '0') +
                System.DateTime.Now.Second.ToString().PadLeft(2, '0');
            return timestamp;
        }

        private void txtFolder_TextChanged(object sender, EventArgs e)
        {
            this.btnParse.Enabled = true;
            this.lblResult.Text = "";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
