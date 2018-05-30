using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PDBundleModPatcher
{
    public class log
    {
        
        private StreamWriter logstream; //A private internal stream
        private String filelogname = "./console.log";
        
        public log()
        {
            try
            {
                if (StaticStorage.settings.WriteConsole)
                    logstream = new StreamWriter(filelogname, true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString(), "Error");
            }
        }

        public log(String filename)
        {
            try
            {
                if (StaticStorage.settings.WriteConsole)
                    logstream = new StreamWriter(filename, true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString(), "Error");
            }
        }

        ~log()
        {
            try
            {
                this.close();
            }
            catch (Exception)
            { 

            }
        }

        public void close()
        {
            if (logstream != null)
            {
                logstream.Close();
            }
        }
        
        public string WriteLine(string line, bool timestamp = false)
        {
            StringBuilder sb = new StringBuilder();
            if (timestamp)
            {
                sb.Append("[" + DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + "] ");
            }
            sb.Append(line);
            sb.Append("\r\n");
            
            Console.Write(sb.ToString());
            if (StaticStorage.settings.WriteConsole && logstream != null)
            {
                logstream.Write(sb.ToString());
                logstream.Flush();
            }
            
            return sb.ToString();
        }
    }
}