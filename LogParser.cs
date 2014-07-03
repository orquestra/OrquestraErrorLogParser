using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrquestraErrorLogParses
{
    public class LogParser
    {
        private string[] m_Files;
        private string FileHeader = null;

        public string[] Files
        {
            get
            {
                return m_Files;
            }
            set
            {
                m_Files = value;
            }
        }

        public LogParser()
        {
            // cabeçalho do arquivo
            FileHeader = "CodLog;Date;File;Error Message;Raw URL;Error in Path;Stack Trace;UserAgent;int" + System.Environment.NewLine;
        }

        public bool StartParse()
        {
            for (int i = 0; i < Files.Length; i++)
            {
            }
            return true;
        }

    }
}
