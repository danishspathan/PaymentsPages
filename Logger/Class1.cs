using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public sealed class Log : ILog
    {
        private Log()
        {
        }
        private static readonly Lazy<Log> instance = new Lazy<Log>(() => new Log());

        public static Log GetInstance
        {
            get
            {
                return instance.Value;
            }
        }

        public void LogException(string message)
        {

            var filePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            DateTime currentdate = DateTime.Today.Date;
            string filename = "ErrorLog_ " + currentdate.ToString("MM-dd-yyyy") + ".txt";
            string _path = Path.Combine(filePath, filename);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            DirectoryInfo dir = new DirectoryInfo(filePath);
            var file = dir.GetFiles("*", SearchOption.AllDirectories);
            if (file != null)
            {
                using (StreamWriter sw = new StreamWriter(_path, true))
                {
                  
                        sw.WriteLine(message);
                    
                }
            }
        }

    }
}
