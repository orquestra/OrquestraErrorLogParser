using System;
using System.IO;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace OrquestraErrorLogParser
{
    public class LogParser
    {
        const string CSV_FILE_HEADER = "CodLog;Date;File;Error Message;Raw URL;Error in Path;JS;Stack Trace;Request;UserAgent;int";
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
        Encoding DEFAULT_ENCODING_UTF8 = Encoding.UTF8;

        private string _LogDirectory;
        private string _EncryptionAlgorithm;
        private string _EncryptionKey;
        BackgroundWorker _BackgroundWorker;
        DoWorkEventArgs _DoWorkEventArgs;
        //public event EventHandler ProgressUpdate;

        public LogParser(string LogDirectory, string EncryptionAlgorithm, string EncryptionKey, BackgroundWorker worker, DoWorkEventArgs e)
        {
            _LogDirectory = LogDirectory;
            _EncryptionAlgorithm = EncryptionAlgorithm;
            _EncryptionKey = EncryptionKey;
            _BackgroundWorker = worker;
            _DoWorkEventArgs = e;
        }

        public void ParseFiles()
        {
            ParseFiles(_LogDirectory, _EncryptionAlgorithm, _EncryptionKey);
        }

        public void ParseFiles(string LogDirectory, string EncryptionAlgorithm, string EncryptionKey)
        {
            // nomes dos arquivos que serao lidos
            string[] filenames = Directory.GetFiles(LogDirectory, "*.txt");
            string resultfull = string.Empty;

            // variaveis para reportar progresso
            int iStep;
            decimal dStep;
            int processedFilesCount;
            string userState;

            // variavel do caminho do arquivo a ser gerado
            string fullFilePath;

            // variaveis do arquivo de escrita
            FileStream wfs;
            StreamWriter sw;

            // variaveis de cada linha do arquivo a ser processado
            bool isRequestData = false;
            string line = string.Empty;
            string logcode = string.Empty;
            string beginlog = string.Empty;
            string rawUrl = string.Empty;
            string errorInPath = string.Empty;
            string stack1 = string.Empty;
            string stack2 = string.Empty;
            string stack3 = string.Empty;
            string errormessage = string.Empty;
            string result = string.Empty;
            string request = string.Empty;
            string userAgent = string.Empty;
            string lineDecrypted = string.Empty;

            // reportar progresso
            iStep = 1;
            dStep = (100 / filenames.Length);
            dStep = Math.Ceiling(dStep);
            iStep = Convert.ToInt32(dStep);
            processedFilesCount = 0;
            userState = string.Empty;

            // nome do arquivo que sera gerado
            fullFilePath = LogDirectory + "\\result_" + GetTimeStamp() + ".csv";

            wfs = File.Open(fullFilePath, FileMode.CreateNew);
            sw = new StreamWriter(wfs, DEFAULT_ENCODING_UTF8);
            sw.WriteLine(CSV_FILE_HEADER);

            foreach (string filename in filenames)
            {
                isRequestData = false;
                line = string.Empty;
                logcode = string.Empty;
                beginlog = string.Empty;
                rawUrl = string.Empty;
                errorInPath = string.Empty;
                stack1 = string.Empty;
                stack2 = string.Empty;
                stack3 = string.Empty;
                errormessage = string.Empty;
                result = string.Empty;
                request = string.Empty;
                userAgent = string.Empty;
                lineDecrypted = string.Empty;

                FileStream rfs = File.Open(filename, FileMode.Open);
                StreamReader file = new StreamReader(rfs, DEFAULT_ENCODING_UTF8);

                userState = "Processando arquivo: " + filename + Environment.NewLine;

                _BackgroundWorker.ReportProgress(processedFilesCount, userState);

                while ((line = file.ReadLine()) != null)
                {

                    if (_BackgroundWorker.CancellationPending)
                    {
                        _DoWorkEventArgs.Cancel = true;
                    }

                    if ((line.StartsWith(NEWLINE)))
                    {
                        // mudou de um registro para outro

                        isRequestData = false;

                        result = logcode;
                        result += ";" + beginlog;
                        result += ";" + GetFileNameOnly(filename.Trim());
                        result += ";" + errormessage.Trim();
                        result += ";" + rawUrl.Trim();
                        result += ";" + errorInPath.Trim();

                        // erro interno ou JS
                        result += ";" + (string.IsNullOrEmpty(rawUrl) && string.IsNullOrEmpty(errorInPath) ? "JS" : "Outro");

                        result += ";" + stack1.Trim() + "------" + stack2.ToString() + "------" + stack3.Trim();
                        result += ";\"" + request + "\"";
                        result += ";" + userAgent;
                        result += ";1";

                        if (!string.IsNullOrEmpty(result))
                        {
                            // obtem todo o resultado da concatenacao de uma linha para gravar no arquivo
                            resultfull += result;

                            sw.WriteLine(resultfull);

                            resultfull = string.Empty;
                        }

                        stack1 = "";
                        stack2 = "";
                        stack3 = "";
                        beginlog = "";
                        errormessage = "";
                        request = "";
                        userAgent = "";

                        // zerar variaveis 
                        rawUrl = errorInPath = string.Empty;
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
                            errormessage = Regex.Replace(errormessage, "Process ID [0-9]*", "");
                        }
                        if (errormessage.Contains("(ID do processo"))
                        {
                            errormessage = Regex.Replace(errormessage, "ID do processo [0-9]*", "");
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
                        userAgent = line.Replace(";", "").Replace("User-agent: ", "").Trim();
                    }

                    if ((line.StartsWith(REQUEST_DATA) || isRequestData) && (line.IndexOf("__VIEWSTATE") == -1 && line.IndexOf("__EVENTVALIDATION") == -1))
                    {
                        if (line.StartsWith(REQUEST_DATA))
                        {
                            request = "";
                        }
                        else
                        {
                            //request += line.Replace(";", "").ToString() + System.Environment.NewLine;


                            try
                            {
                                //lineDecrypted = Orquestra.Util.Encryption.Dec(line, txtEncryptionKey.Text);

                                lineDecrypted = OrquestraErrorLogParser.Encryption.Dec(EncryptionAlgorithm, line, EncryptionKey).Trim();
                            }
                            catch (System.Security.Cryptography.CryptographicException)
                            {
                                lineDecrypted = null;
                            }

                            if (lineDecrypted != null)
                            {
                                request += lineDecrypted.Replace("\"", "'");
                            }
                            else
                            {
                                request += line;
                            }
                        }
                        isRequestData = true;
                    }

                } // while readline

                file.Close();

                // acabou de processar um arquivo de entrada
                // deve gravar todo o conteudo no arquivo de saida
                //System.IO.File.AppendAllText(fullFilePath, resultfull);

                sw.Write(resultfull);

                resultfull = string.Empty;

                // atualizar o progresso na UI
                processedFilesCount += iStep;

                _BackgroundWorker.ReportProgress(processedFilesCount);

            } // foreach file

            sw.Close();
            
        }

        private string GetTimeStamp()
        {
            string timestamp = DateTime.Now.Year.ToString() +
                DateTime.Now.Month.ToString().PadLeft(2, '0') +
                DateTime.Now.Day.ToString().PadLeft(2, '0') + "_" +
                DateTime.Now.Hour.ToString().PadLeft(2, '0') +
                DateTime.Now.Minute.ToString().PadLeft(2, '0') +
                DateTime.Now.Second.ToString().PadLeft(2, '0');
            return timestamp;
        }

        private string GetFileNameOnly(string FilePathAndName)
        {
            string filename = string.Empty;

            if (!string.IsNullOrEmpty(FilePathAndName))
            {
                filename = new FileInfo(FilePathAndName).Name;
            }

            return filename;
        }

    }
}
