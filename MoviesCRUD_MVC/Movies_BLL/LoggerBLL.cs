using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Movies_BLL
{
    class LoggerBLL
    {
        private readonly string LogPath = ConfigurationManager.AppSettings["LogPathBLL"];

        public void Log(string level, string source, string target, string message, string stackTrace = null)
        {
            StreamWriter writer = null;
            try
            {
                string timeStamp = DateTime.Now.ToString();
                writer = new StreamWriter(LogPath, true);
                writer.WriteLine("[{0}] -- {1} -- {2} -- {3} -- {4}", timeStamp, level, source, message);
                if (stackTrace != null)
                {
                    writer.WriteLine(stackTrace);
                }
                writer.WriteLine();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                }
            }
        }
    }
}
