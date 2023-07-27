using Sonda.Net.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysIntegracionContable
{
    public class InePlanillaTesoreria
    {
        OraConn dbc = new OraConn();
        public bool CargarPlanillaTesoreria(DataSet ds, string idm, DateTime fecha)
        {
            bool resp = false;
            //object[] args = { IDM, fechaAcreditacion, nroLote };
            try
            {

                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        object[] args = { idm, fecha, row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(),
                            row[5].ToString(),row[6].ToString(),row[7].ToString(),row[8].ToString(),row[9].ToString(),row[10].ToString(),row[11].ToString(),
                            row[12].ToString(),row[13].ToString(),row[14].ToString(),row[15].ToString(),row[16].ToString(),row[17].ToString(),
                            row[18].ToString(),row[19].ToString(),row[20].ToString(),row[21].ToString(),row[22].ToString(),row[23].ToString(),
                            row[24].ToString(),row[25].ToString(),row[26].ToString(), "V",fecha,"Usuario Prueba",fecha,"Usuario Prueba","Usuario Prueba"
                        };

                        //Llamado al procedimiento
                        dbc.ExecProc("PITC_RECAUDA_RESUMEN_DETALLE.ITC_CARGA_PLANILLA_TESORERIA",
                        "VID_ADM,VFEC_ESTADO_REG,VNOMBRE_BANCO,VNRO_CTA,VSALDO_INCIAL,VRECAUDACION_COTIZACIONES," +
                        "VCONSIGNACIONES,VABONO_MAL_EFECTUADO,VAJUSTE_CHEQUE_PROTESTADO,VRECAUDACION_DIA_30," +
                        "VREG_CARGO_BANCARIO,VDEV_PARTIDA_ERRONEA,VACLARACION_CARGO_BANCARIO,VCHEQUES_PROTESTADO," +
                        "VREGCARGO_MAL_EFECTUADO,VTRASPASO_RECAUDACION,VCHEQUE_MAL_DIGITADO,VCARGO_BANCARIO," +
                        "VTRASFERENCIA_OTRO_BANCO,VSALDO_FINAL,VABONO_MAL_EFECTUADO_1,VABONO_MAL_EFECTUADO_2," +
                        "VPRESTACIONES_LABORALES,VCIRCULAR_1411,VAPORTES_CARGOS,VERRORESTUDIOJURIDICO_1," +
                        "VERRORESTUDIOJURIDICO_2,VFINANCIAMIENTO,VVUELTO_PROTESTADO,VESTADO_REG,VFEC_ING_REG," +
                        "VID_USUARIO_ING_REG,VFEC_ULT_MODIF_REG,VID_USUARIO_ULT_MODIF_REG,VID_FUNCION_ULT_MODIF_REG",
                        args, "in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in",
                        "int,date,string,string,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,int,string,date,string,date,string,string");
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return resp;
        }
    }
}
