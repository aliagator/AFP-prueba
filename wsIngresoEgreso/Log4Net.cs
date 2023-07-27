using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsIntegracionContable
{
    public class Log4Net
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void WriteLogInfo(string name, string msj)
        {
            Log.Info("Info :" + name + " " + msj);
        }

        public void WriteLogError(string name, string msj)
        {
            Log.Info("Error :" + name + " " + msj);
        }

        public void WriteLogExeption(string name, Exception ex)
        {
            Log.Info("Exeption :" + name + " " + ex);
        }
    }
}
