using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OrquestraErrorLogParser
{
    public class DirectoryValidation
    {
        public bool DirectoryExists(string DirectoryPath)
        {
            return Directory.Exists(DirectoryPath);
        }

        public bool DirectoryHasFiles(string DirectoryPath)
        {
            string[] filenames = Directory.GetFiles(DirectoryPath, "*.txt");
            return (filenames.Length != 0);
        }
    }
}
