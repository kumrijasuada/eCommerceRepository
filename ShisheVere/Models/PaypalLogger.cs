using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace ShisheVere.Models
{
    public class PaypalLogger
    {
        public static string LogDirectoryPath = Environment.CurrentDirectory;

        public static void Log (String messages)
        {
            try
            {
                StreamWriter strw = new StreamWriter(LogDirectoryPath + "\\PaypalError.log",true);
                strw.WriteLine(String.Format("{0} ",DateTime.Now.ToString("MM/dd/yyyy HH:MM:SS") + "--->" + messages));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}