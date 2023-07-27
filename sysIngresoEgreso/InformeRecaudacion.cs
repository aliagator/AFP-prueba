using Sonda.Net.DB;
using Sonda.Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysIntegracionContable
{
    public class InformeRecaudacion
    {
        OraConn dbc = new OraConn();
        public DataSet InformeResumen(string IDM, DateTime fechaInCaja, DateTime fechaFinCaja, DateTime FechaIniAcre, DateTime FechaFinAcre)
        {
            DataSet dsRetorno = new DataSet();
            object[] args = { IDM, fechaInCaja, fechaFinCaja, FechaIniAcre, FechaFinAcre };
            try
            {
                dsRetorno = dbc.ExecProc("PITC_RECAUDA_RESUMEN_DETALLE.ITC_INFORME_RESUMEN",
                    "VID_ADM,VFECHA_INI_CAJA,VFECHA_FIN_CAJA,VFECHA_INI_ACRE,VFECHA_FIN_ACRE,THISCURSOR",
                    args, "in,in,in,in,in,out", "int,date,date,date,date,cursor");
            }
            catch (Exception ex)
            {
                throw;
            }
            return dsRetorno;
        }

        public DataSet InformeResumenDiario(string IDM, DateTime fechaInCaja)
        {
            DataSet dsRetorno = new DataSet();
            object[] args = { IDM, fechaInCaja };
            try
            {
                dsRetorno = dbc.ExecProc("PITC_RECAUDA_RESUMEN_DETALLE.ITC_PROCESO_DIARIO_ACREDITA",
                    "VID_ADM,VFECHA_DIARIA,THISCURSOR",
                    args, "in,in,out", "int,date,cursor");
            }
            catch (Exception ex)
            {
                throw;
            }
            return dsRetorno;
        }

        public DataSet InsertaPlanillaExcel(int IDM, string Nombre_Banco, string Numero_Cuenta, int Saldo_Inicial, int Recauda_Cotiza,
                                            DateTime Fecha_Registro, int Abono_Mal_Efec, int Prestaciones_Laborales, int Circular_1411,
                                            int Aporte_Cargos, int Cheque_Protestado, int Error_Juridico, int Financiamiento,
                                            int Traspaso_Recaudacion, int Doc_Devuelto, int Cargo_Bancario, int Transferecia_Otros,
                                            int Saldo_Final, string Estado_Reg, DateTime Fecha_Ingreso_Registro, string Usuario_Ingreso_Reg,
                                            DateTime Fecha_Ultima_Modificacion, string Usuario_Ultima_Modificacion, string Funcion_Ult_Modificacion)


        {
            DataSet dsRetorno = new DataSet();
            object[] args = { IDM,Nombre_Banco,Numero_Cuenta,Saldo_Inicial,Recauda_Cotiza,Fecha_Registro,
                              Abono_Mal_Efec,Prestaciones_Laborales,Circular_1411,Aporte_Cargos,Cheque_Protestado,Error_Juridico,
                              Financiamiento,Traspaso_Recaudacion,Doc_Devuelto,Cargo_Bancario,Transferecia_Otros,Saldo_Final,
                              Estado_Reg,Fecha_Ingreso_Registro,Usuario_Ingreso_Reg,Fecha_Ultima_Modificacion,Usuario_Ultima_Modificacion,
                              Funcion_Ult_Modificacion};
            try
            {
                dsRetorno = dbc.ExecProc("PITC_RECAUDA_RESUMEN_DETALLE.ITC_CARGA_PLANILLA_TESORERIA",
                    "VID_ADM,VBANCO_RECAUDACION,VNUM_CUENTA_BANCO,VSALDO_INICIAL,VRECAUDACION_COTIZACIONES," +
                    "VFEC_ESTADO_REG,VABONO_MAL_EFECTUADO,VPRESTACIONES_LABORALES,VREV_DEV_CIRCULAR_1411," +
                    "VAPORTE_CARGOS,VCHEQUE_PROTESTADO,VDEV_ERROR_JURIDICO,VFINANCIAMIENTO," +
                    "VTRASPASO_RECAUDACION,VDOC_DEVUELTO_PROTESTADO,VCARGO_BANCARIO,VTRANSFERENCIA_OTRO_BANCO," +
                    "VSALDO_FINAL,VESTADO_REG,VFEC_ING_REG,VID_USUARIO_ING_REG," +
                    "VFEC_ULT_MODIF_REG,VID_USUARIO_ULT_MODIF_REG,VID_FUNCION_ULT_MODIF_REG",

                    args, "in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in,in",
                          "int,string,string,int,int,date,int,int,int,int,int,int," +
                          "int,int,int,int,int,int,string,date,string,date,string,string");

            }
            catch (Exception ex)
            {
                throw;
            }
            return dsRetorno;
        }


        public DataSet LotesAcreditadosRecaudacion(string IDM, DateTime FechaContable)
        {
            DataSet dsRetorno = new DataSet();
            object[] args = { IDM, FechaContable };
            try
            {
                dsRetorno = dbc.ExecProc("PITC_RECAUDA_RESUMEN_DETALLE.ITC_INFORME_LOTES_ACREDITADOS",
                    "VID_ADM,VFECHA_CONTABLE,THISCURSOR",
                    args, "in,in,out", "int,date,cursor");
            }
            catch (Exception ex)
            {
                throw;
            }
            return dsRetorno;
        }



        public DataSet DetallePlanillaTesoreria(string IDM, DateTime fechaInCaja)
        {
            DataSet dsRetorno = new DataSet();
            object[] args = { IDM, fechaInCaja };
            try
            {
                dsRetorno = dbc.ExecProc("PITC_RECAUDA_RESUMEN_DETALLE.ITC_CARGA_DETALLE_TESORERIA",
                    "VID_ADM,VFECHA_DIARIA,THISCURSOR",
                    args, "in,in,out", "int,date,cursor");
            }
            catch (Exception ex)
            {
                throw;
            }
            return dsRetorno;
        }

    }
}
