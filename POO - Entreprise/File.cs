using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO___Entreprise
{
    class File
    {
        public String FileName;
        public String Path;

        public File(String filename)
        {
            this.FileName = filename;
            // All files are in the folder Data
            this.Path = @"Data\" + this.FileName; ;
        }

        private string[] LoadFile()
        {
            string[] lines = System.IO.File.ReadAllLines(this.Path);
            Console.WriteLine("[FILE] - File Loaded");
            return lines;
        }

        public string[] Load
        {
            get { return this.LoadFile(); }
        }

        public void SaveFile(String text)
        {
            System.IO.File.WriteAllText(this.Path, text);
        }
    }
}
