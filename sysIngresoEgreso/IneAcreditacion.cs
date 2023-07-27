using Sonda.Net.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysIntegracionContable
{
    public class IneAcreditacion
    {
        OraConn dbc = new OraConn();
        public void CargaAcreditacion(string IDM, DateTime fechaAcreditacion, int nroLote)
        {
            object[] args = { IDM, fechaAcreditacion, nroLote};
            try
            {
                dbc.ExecProc("PITC_RECAUDA_RESUMEN_DETALLE.ITC_CARGA_ACRED",
                    "VID_ADM,VFECHA_ACREDITACION,VNUMERO_ID_LOTE,THISCURSOR",
                    args, "in,in,in,out", "int,date,int,cursor");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
