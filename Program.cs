using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SiqiDemo1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           
           /*string dataDir = AppDomain.CurrentDomain.BaseDirectory;
            
            if (dataDir.EndsWith(@"\bin\debug\") || dataDir.EndsWith(@"\bin\release\"))
            { 
                dataDir = System.IO.Directory.GetParent(dataDir).Parent.Parent.FullName;
                AppDomain.CurrentDomain.SetData("DataDirectory",dataDir);
            }

            MessageBox.Show(dataDir);
           
            */

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
