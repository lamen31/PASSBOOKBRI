using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace PASSBOOKBRI.Data
{
    public class PrintServer
    {
        public const string param_printserverA4_exe = "C:\\printserver\\printserverapplication.exe";
        public const string param_printservercoba_exe = "C:\\printserver\\printservercobaapplication.exe";
        public const string param_printserverpassbook_exe = "C:\\printserver\\printserverpassbookapplication.exe";
        public const string param_printserverthermal_exe = "C:\\printserver\\printserverthermalapplication.exe";

        private Process process = new Process();

        public void Printserver(string strnamaprinter)
        {
            string workingdirectory = string.Empty;
            //Process process = new Process();
            switch (strnamaprinter)
            {
                case "Brother HL-L2360D series":
                    {
                        workingdirectory = Path.GetDirectoryName(param_printserverA4_exe);
                        process.StartInfo.FileName = param_printserverA4_exe;
                        break;
                    }
                case "EPSON L3150 Series":
                    {
                        workingdirectory = Path.GetDirectoryName(param_printservercoba_exe);
                        process.StartInfo.FileName = param_printservercoba_exe;
                        break;
                    }
                case "PsiPR-OLI":
                    {
                        workingdirectory = Path.GetDirectoryName(param_printserverpassbook_exe);
                        process.StartInfo.FileName = param_printserverpassbook_exe;
                        break;
                    }
                case "BT-T080(U) 1":
                    {
                        workingdirectory = Path.GetDirectoryName(param_printserverthermal_exe);
                        process.StartInfo.FileName = param_printserverthermal_exe;
                        break;
                    }
            }
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.WorkingDirectory = workingdirectory;
            process.Start();
            process.WaitForExit();
            if (process.HasExited)
            {
                process.Close();
                process.Dispose();
            }
        }
    }
}
