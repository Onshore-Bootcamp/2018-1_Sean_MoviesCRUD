using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace MoviesCRUD_MVC
{
    public class LoggerPL
    {
        private readonly string LogPath = ConfigurationManager.AppSettings["LogPathPL"];

        public void Log(string level,
                        string source,
                        string target,
                        string message,
                        string stackTrace = null)
        {
            StreamWriter writer = null;

            try
            {
                string timeStamp = DateTime.Now.ToString();
                writer = new StreamWriter(LogPath, true);
                writer.WriteLine("[{0}] -- {1} -- {2} -- {3} -- {4}", timeStamp, level, source, target, message);
                writer.WriteLine();
               
                if (stackTrace != null)
                {
                    writer.WriteLine(stackTrace);
                }
                writer.WriteLine();
                writer.WriteLine();

            }
            catch (Exception)
            {

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