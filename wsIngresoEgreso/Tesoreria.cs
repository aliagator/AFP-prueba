using Sonda.Net.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.OleDb;

namespace wsIntegracionContable
{
    public class Tesoreria
    {
        OraConn dbc = new OraConn();
        public bool ProcesarArchivo(int idAdm, string nombreArchivo)
        {
            string respuesta = string.Empty;
            string linea;
            StreamWriter esc;
            bool result = false;
            try
            {
                //Implentacion de Log
                if (!File.Exists(nombreArchivo))
                    return result;

                if (!nombreArchivo.EndsWith("XLSX"))
                    return result;

                StreamReader lec = new StreamReader(nombreArchivo, System.Text.Encoding.Default);

                linea = lec.ReadLine();

                string[] valores = linea.Split(';');

                result = true;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public DataSet ImportarArchivoDataSet(string rutaserver, string strConn)
        {
            OleDbConnection conector = default(OleDbConnection);
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            try
            {
                conector = new OleDbConnection(strConn);
                conector.Open();

                OleDbCommand consulta = default(OleDbCommand);
                consulta = new OleDbCommand("select * from [Hoja1$]", conector);
                OleDbDataAdapter adaptador = new OleDbDataAdapter();
                adaptador.SelectCommand = consulta;

                adaptador.Fill(ds, "PlanillaTesoreria");
                conector.Close();

                ds1 = ConstruccionPlanillaExcelTesoreria(ds);
            }
            catch (Exception ex)
            {
                throw;
            }
            return ds1;
        }

        public DataSet ConstruccionPlanillaExcelTesoreria(DataSet ds)
        {
            DataTable tblPlanillaTesoreria;
            DataSet ds1 = new DataSet();
            //Primer grupo de bancos
            BcoRecaudacion bcoEstado_1 = new BcoRecaudacion();
            BcoRecaudacion bcoCredito_1 = new BcoRecaudacion();
            BcoRecaudacion bcoSantiago_1 = new BcoRecaudacion();
            BcoRecaudacion bcoChile_1 = new BcoRecaudacion();
            BcoRecaudacion bcoChile_2 = new BcoRecaudacion();
            BcoRecaudacion bcoChile_3 = new BcoRecaudacion();
            BcoRecaudacion bcoChile_4 = new BcoRecaudacion();
            BcoRecaudacion bcoSantander_1 = new BcoRecaudacion();
            BcoRecaudacion bcoSantiago_2 = new BcoRecaudacion();
            BcoRecaudacion bcoBBVA_1 = new BcoRecaudacion();
            BcoRecaudacion bcoEstado_2 = new BcoRecaudacion();
            BcoRecaudacion bcoSantander_2 = new BcoRecaudacion();
            BcoRecaudacion bcoSantander_3 = new BcoRecaudacion();
            BcoRecaudacion bcoChilePrevired_1 = new BcoRecaudacion();

            //Segundoo grupo de bancos
            BcoRecaudacion bcoBice_1 = new BcoRecaudacion();
            BcoRecaudacion bcoScotiabank_1 = new BcoRecaudacion();
            BcoRecaudacion bcoChileINP_1 = new BcoRecaudacion();
            BcoRecaudacion bcoChilePrevired_2 = new BcoRecaudacion();
            BcoRecaudacion bcoSantiago_3 = new BcoRecaudacion();
            BcoRecaudacion bcoBCI_1 = new BcoRecaudacion();
            BcoRecaudacion bcoEstado_3 = new BcoRecaudacion();
            BcoRecaudacion bcoBCI_2 = new BcoRecaudacion();
            BcoRecaudacion bcoBCI_3 = new BcoRecaudacion();
            BcoRecaudacion bcoItau_1 = new BcoRecaudacion();
            BcoRecaudacion bcoEstado_4 = new BcoRecaudacion();
            BcoRecaudacion bcoItauCorpBanca_1 = new BcoRecaudacion();
            BcoRecaudacion bcoSecurity_1 = new BcoRecaudacion();
            BcoRecaudacion bcoEstado_5 = new BcoRecaudacion();
            BcoRecaudacion bcoTotal = new BcoRecaudacion();

            List<BcoRecaudacion> listBco = new List<BcoRecaudacion>();
            try
            {
                tblPlanillaTesoreria = ds.Tables["PlanillaTesoreria"];
                int index = 0;

                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        #region Llenado de objetos
                        if (row[0].ToString().Equals("") && index.Equals(0)) //Columna O
                        {
                            index++;
                            continue;
                        }

                        if (row[0].ToString().Equals("BCO RECAUDACION") && index.Equals(1) || row[0].ToString().Equals("BCO RECAUDACION") && index.Equals(19)) //Columna B -- 19 lado B
                        {
                            //Nombres BCO
                            if (index.Equals(1))
                            {
                                #region Bancos 1 al 14
                                if (!row[1].ToString().Equals(""))
                                    bcoEstado_1.NombreBco = row[1].ToString();
                                if (!row[2].ToString().Equals(""))
                                    bcoCredito_1.NombreBco = row[2].ToString();
                                if (!row[3].ToString().Equals(""))
                                    bcoSantiago_1.NombreBco = row[3].ToString();
                                if (!row[4].ToString().Equals(""))
                                    bcoChile_1.NombreBco = row[4].ToString();
                                if (!row[5].ToString().Equals(""))
                                    bcoChile_2.NombreBco = row[5].ToString();
                                if (!row[6].ToString().Equals(""))
                                    bcoChile_3.NombreBco = row[6].ToString();
                                if (!row[7].ToString().Equals(""))
                                    bcoChile_4.NombreBco = row[7].ToString();
                                if (!row[8].ToString().Equals(""))
                                    bcoSantander_1.NombreBco = row[8].ToString();
                                if (!row[9].ToString().Equals(""))
                                    bcoSantiago_2.NombreBco = row[9].ToString();
                                if (!row[10].ToString().Equals(""))
                                    bcoBBVA_1.NombreBco = row[10].ToString();
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_2.NombreBco = row[11].ToString();
                                if (!row[12].ToString().Equals(""))
                                    bcoSantander_2.NombreBco = row[12].ToString();
                                if (!row[13].ToString().Equals(""))
                                    bcoSantander_3.NombreBco = row[13].ToString();
                                if (!row[14].ToString().Equals(""))
                                    bcoChilePrevired_1.NombreBco = row[14].ToString();
                                #endregion
                            }
                            else
                            {
                                #region Bancos 15 al 28
                                if (!row[1].ToString().Equals(""))
                                    bcoBice_1.NombreBco = row[1].ToString();
                                if (!row[2].ToString().Equals(""))
                                    bcoScotiabank_1.NombreBco = row[2].ToString();
                                if (!row[3].ToString().Equals(""))
                                    bcoChileINP_1.NombreBco = row[3].ToString();
                                if (!row[4].ToString().Equals(""))
                                    bcoChilePrevired_2.NombreBco = row[4].ToString();
                                if (!row[5].ToString().Equals(""))
                                    bcoSantiago_3.NombreBco = row[5].ToString();
                                if (!row[6].ToString().Equals(""))
                                    bcoBCI_1.NombreBco = row[6].ToString();
                                if (!row[7].ToString().Equals(""))
                                    bcoEstado_3.NombreBco = row[7].ToString();
                                if (!row[8].ToString().Equals(""))
                                    bcoBCI_2.NombreBco = row[8].ToString();
                                if (!row[9].ToString().Equals(""))
                                    bcoBCI_3.NombreBco = row[9].ToString();
                                if (!row[10].ToString().Equals(""))
                                    bcoItau_1.NombreBco = row[10].ToString();
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_4.NombreBco = row[11].ToString();
                                if (!row[12].ToString().Equals(""))
                                    bcoItauCorpBanca_1.NombreBco = row[12].ToString();
                                if (!row[13].ToString().Equals(""))
                                    bcoSecurity_1.NombreBco = row[13].ToString();
                                if (!row[14].ToString().Equals(""))
                                    bcoEstado_5.NombreBco = row[14].ToString();
                                if (!row[15].ToString().Equals(""))
                                    bcoTotal.NombreBco = row[15].ToString();
                                #endregion
                            }
                            index++;
                        }
                        if (row[0].ToString().Equals("") && index.Equals(2) || row[0].ToString().Equals("") && index.Equals(20)) //Columna C
                        {
                            //Nro Cuenta
                            if (index.Equals(2))
                            {
                                #region Bancos 1 al 14
                                if (!row[1].ToString().Equals(""))
                                    bcoEstado_1.NroCta = row[1].ToString();
                                if (!row[2].ToString().Equals(""))
                                    bcoCredito_1.NroCta = row[2].ToString();
                                if (!row[3].ToString().Equals(""))
                                    bcoSantiago_1.NroCta = row[3].ToString();
                                if (!row[4].ToString().Equals(""))
                                    bcoChile_1.NroCta = row[4].ToString();
                                if (!row[5].ToString().Equals(""))
                                    bcoChile_2.NroCta = row[5].ToString();
                                if (!row[6].ToString().Equals(""))
                                    bcoChile_3.NroCta = row[6].ToString();
                                if (!row[7].ToString().Equals(""))
                                    bcoChile_4.NroCta = row[7].ToString();
                                if (!row[8].ToString().Equals(""))
                                    bcoSantander_1.NroCta = row[8].ToString();
                                if (!row[9].ToString().Equals(""))
                                    bcoSantiago_2.NroCta = row[9].ToString();
                                if (!row[10].ToString().Equals(""))
                                    bcoBBVA_1.NroCta = row[10].ToString();
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_2.NroCta = row[11].ToString();
                                if (!row[12].ToString().Equals(""))
                                    bcoSantander_2.NroCta = row[12].ToString();
                                if (!row[13].ToString().Equals(""))
                                    bcoSantander_3.NroCta = row[13].ToString();
                                if (!row[14].ToString().Equals(""))
                                    bcoChilePrevired_1.NroCta = row[14].ToString();
                                #endregion
                            }
                            else
                            {
                                #region Bancos 15 al 28
                                if (!row[1].ToString().Equals(""))
                                    bcoBice_1.NroCta = row[1].ToString();
                                if (!row[2].ToString().Equals(""))
                                    bcoScotiabank_1.NroCta = row[2].ToString();
                                if (!row[3].ToString().Equals(""))
                                    bcoChileINP_1.NroCta = row[3].ToString();
                                if (!row[4].ToString().Equals(""))
                                    bcoChilePrevired_2.NroCta = row[4].ToString();
                                if (!row[5].ToString().Equals(""))
                                    bcoSantiago_3.NroCta = row[5].ToString();
                                if (!row[6].ToString().Equals(""))
                                    bcoBCI_1.NroCta = row[6].ToString();
                                if (!row[7].ToString().Equals(""))
                                    bcoEstado_3.NroCta = row[7].ToString();
                                if (!row[8].ToString().Equals(""))
                                    bcoBCI_2.NroCta = row[8].ToString();
                                if (!row[9].ToString().Equals(""))
                                    bcoBCI_3.NroCta = row[9].ToString();
                                if (!row[10].ToString().Equals(""))
                                    bcoItau_1.NroCta = row[10].ToString();
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_4.NroCta = row[11].ToString();
                                if (!row[12].ToString().Equals(""))
                                    bcoItauCorpBanca_1.NroCta = row[12].ToString();
                                if (!row[13].ToString().Equals(""))
                                    bcoSecurity_1.NroCta = row[13].ToString();
                                if (!row[14].ToString().Equals(""))
                                    bcoEstado_5.NroCta = row[14].ToString();
                                if (!row[15].ToString().Equals(""))
                                    bcoTotal.NroCta = row[15].ToString();
                                #endregion
                            }
                            index++;
                        }
                        if (row[0].ToString().Equals("SALDO INICIAL") && index.Equals(3) || row[0].ToString().Equals("SALDO INICIAL") && index.Equals(21)) //Columna C
                        {
                            //Saldo Inicial

                            if (index.Equals(3))
                            {
                                #region Bancos 1 al 14
                                if (!row[1].ToString().Equals(""))
                                    bcoEstado_1.SaldoIncial = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoCredito_1.SaldoIncial = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoSantiago_1.SaldoIncial = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChile_1.SaldoIncial = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoChile_2.SaldoIncial = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoChile_3.SaldoIncial = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoChile_4.SaldoIncial = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoSantander_1.SaldoIncial = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoSantiago_2.SaldoIncial = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoBBVA_1.SaldoIncial = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_2.SaldoIncial = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoSantander_2.SaldoIncial = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSantander_3.SaldoIncial = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoChilePrevired_1.SaldoIncial = Convert.ToInt32(row[14]);
                                #endregion
                            }
                            else
                            {
                                #region Bancos 15 al 28
                                if (!row[1].ToString().Equals(""))
                                    bcoBice_1.SaldoIncial = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoScotiabank_1.SaldoIncial = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoChileINP_1.SaldoIncial = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChilePrevired_2.SaldoIncial = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoSantiago_3.SaldoIncial = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoBCI_1.SaldoIncial = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoEstado_3.SaldoIncial = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoBCI_2.SaldoIncial = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoBCI_3.SaldoIncial = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoItau_1.SaldoIncial = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_4.SaldoIncial = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoItauCorpBanca_1.SaldoIncial = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSecurity_1.SaldoIncial = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoEstado_5.SaldoIncial = Convert.ToInt32(row[14]);
                                if (!row[15].ToString().Equals(""))
                                    bcoTotal.SaldoIncial = Convert.ToInt32(row[15]);
                                #endregion
                            }
                            index++;
                        }
                        if (row[0].ToString().Equals(" + RECAUDACION COTIZACIONES") && index.Equals(4) || row[0].ToString().Equals(" + RECAUDACION COTIZACIONES") && index.Equals(22)) //Columna C
                        {
                            //Recaudacion Cotizaciones

                            if (index.Equals(4))
                            {
                                #region Bancos 1 al 14
                                if (!row[1].ToString().Equals(""))
                                    bcoEstado_1.RecaudacionCotizaciones = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoCredito_1.RecaudacionCotizaciones = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoSantiago_1.RecaudacionCotizaciones = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChile_1.RecaudacionCotizaciones = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoChile_2.RecaudacionCotizaciones = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoChile_3.RecaudacionCotizaciones = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoChile_4.RecaudacionCotizaciones = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoSantander_1.RecaudacionCotizaciones = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoSantiago_2.RecaudacionCotizaciones = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoBBVA_1.RecaudacionCotizaciones = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_2.RecaudacionCotizaciones = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoSantander_2.RecaudacionCotizaciones = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSantander_3.RecaudacionCotizaciones = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoChilePrevired_1.RecaudacionCotizaciones = Convert.ToInt32(row[14]);
                                #endregion
                            }
                            else
                            {
                                #region Bancos 15 al 28
                                if (!row[1].ToString().Equals(""))
                                    bcoBice_1.RecaudacionCotizaciones = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoScotiabank_1.RecaudacionCotizaciones = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoChileINP_1.RecaudacionCotizaciones = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChilePrevired_2.RecaudacionCotizaciones = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoSantiago_3.RecaudacionCotizaciones = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoBCI_1.RecaudacionCotizaciones = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoEstado_3.RecaudacionCotizaciones = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoBCI_2.RecaudacionCotizaciones = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoBCI_3.RecaudacionCotizaciones = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoItau_1.RecaudacionCotizaciones = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_4.RecaudacionCotizaciones = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoItauCorpBanca_1.RecaudacionCotizaciones = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSecurity_1.RecaudacionCotizaciones = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoEstado_5.RecaudacionCotizaciones = Convert.ToInt32(row[14]);
                                if (!row[15].ToString().Equals(""))
                                    bcoTotal.RecaudacionCotizaciones = Convert.ToInt32(row[15]);
                                #endregion
                            }
                            index++;
                        }

                        if (row[0].ToString().Equals("CONSIGNACIONES") && index.Equals(5)) //Columna C
                        {
                            //Consignaciones


                            if (index.Equals(5))
                            {
                                #region Bancos 1 al 14
                                if (!row[1].ToString().Equals(""))
                                    bcoEstado_1.Consignaciones = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoCredito_1.Consignaciones = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoSantiago_1.Consignaciones = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChile_1.Consignaciones = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoChile_2.Consignaciones = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoChile_3.Consignaciones = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoChile_4.Consignaciones = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoSantander_1.Consignaciones = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoSantiago_2.Consignaciones = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoBBVA_1.Consignaciones = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_2.Consignaciones = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoSantander_2.Consignaciones = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSantander_3.Consignaciones = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoChilePrevired_1.Consignaciones = Convert.ToInt32(row[14]);
                                #endregion
                            }
                            index++;
                        }

                        if (row[0].ToString().Equals("ABONO MAL EFECTUADO") && index.Equals(6) ||
                            row[0].ToString().Equals("ABONO MAL EFECTUADO") && index.Equals(23) ||
                            row[0].ToString().Equals("ABONO MAL EFECTUADO") && index.Equals(24)) //Columna C
                        {
                            //Abono Mal Efectuado
                            if (index.Equals(6))
                            {
                                #region Bancos 1 al 14
                                if (!row[1].ToString().Equals(""))
                                    bcoEstado_1.AbonoMalEfectuado = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoCredito_1.AbonoMalEfectuado = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoSantiago_1.AbonoMalEfectuado = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChile_1.AbonoMalEfectuado = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoChile_2.AbonoMalEfectuado = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoChile_3.AbonoMalEfectuado = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoChile_4.AbonoMalEfectuado = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoSantander_1.AbonoMalEfectuado = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoSantiago_2.AbonoMalEfectuado = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoBBVA_1.AbonoMalEfectuado = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_2.AbonoMalEfectuado = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoSantander_2.AbonoMalEfectuado = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSantander_3.AbonoMalEfectuado = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoChilePrevired_1.AbonoMalEfectuado = Convert.ToInt32(row[14]);
                                #endregion
                            }
                            //Abono mal efectuado 1
                            if (index.Equals(23))
                            {
                                #region Bancos 15 al 28
                                if (!row[1].ToString().Equals(""))
                                    bcoBice_1.AbonoMalEfectuado_1 = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoScotiabank_1.AbonoMalEfectuado_1 = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoChileINP_1.AbonoMalEfectuado_1 = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChilePrevired_2.AbonoMalEfectuado_1 = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoSantiago_3.AbonoMalEfectuado_1 = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoBCI_1.AbonoMalEfectuado_1 = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoEstado_3.AbonoMalEfectuado_1 = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoBCI_2.AbonoMalEfectuado_1 = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoBCI_3.AbonoMalEfectuado_1 = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoItau_1.AbonoMalEfectuado_1 = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_4.AbonoMalEfectuado_1 = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoItauCorpBanca_1.AbonoMalEfectuado_1 = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSecurity_1.AbonoMalEfectuado_1 = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoEstado_5.AbonoMalEfectuado_1 = Convert.ToInt32(row[14]);
                                if (!row[15].ToString().Equals(""))
                                    bcoTotal.AbonoMalEfectuado_1 = Convert.ToInt32(row[15]);
                                #endregion
                            }
                            //Abono mal efectuado 2
                            if (index.Equals(24))
                            {
                                #region Bancos 15 al 28
                                if (!row[1].ToString().Equals(""))
                                    bcoBice_1.AbonoMalEfectuado_2 = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoScotiabank_1.AbonoMalEfectuado_2 = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoChileINP_1.AbonoMalEfectuado_2 = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChilePrevired_2.AbonoMalEfectuado_2 = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoSantiago_3.AbonoMalEfectuado_2 = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoBCI_1.AbonoMalEfectuado_2 = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoEstado_3.AbonoMalEfectuado_2 = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoBCI_2.AbonoMalEfectuado_2 = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoBCI_3.AbonoMalEfectuado_2 = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoItau_1.AbonoMalEfectuado_2 = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_4.AbonoMalEfectuado_2 = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoItauCorpBanca_1.AbonoMalEfectuado_2 = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSecurity_1.AbonoMalEfectuado_2 = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoEstado_5.AbonoMalEfectuado_2 = Convert.ToInt32(row[14]);
                                if (!row[15].ToString().Equals(""))
                                    bcoTotal.AbonoMalEfectuado_2 = Convert.ToInt32(row[15]);
                                #endregion
                            }
                            index++;
                        }

                        if (row[0].ToString().Equals("AJUSTE CHEQUE PROTESTADO") && index.Equals(7)) //Columna C
                        {
                            //Abono Mal Efectuado

                            if (index.Equals(7))
                            {
                                #region Bancos 1 al 14
                                if (!row[1].ToString().Equals(""))
                                    bcoEstado_1.AjusteChequeProtestado = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoCredito_1.AjusteChequeProtestado = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoSantiago_1.AjusteChequeProtestado = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChile_1.AjusteChequeProtestado = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoChile_2.AjusteChequeProtestado = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoChile_3.AjusteChequeProtestado = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoChile_4.AjusteChequeProtestado = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoSantander_1.AjusteChequeProtestado = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoSantiago_2.AjusteChequeProtestado = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoBBVA_1.AjusteChequeProtestado = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_2.AjusteChequeProtestado = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoSantander_2.AjusteChequeProtestado = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSantander_3.AjusteChequeProtestado = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoChilePrevired_1.AjusteChequeProtestado = Convert.ToInt32(row[14]);
                                #endregion
                            }
                            index++;
                        }

                        if (row[0].ToString().Equals(" + VXD RECAUDACION DIA 30") && index.Equals(8)) //Columna C
                        {
                            //VXD RECAUDACION DIA 30
                            if (index.Equals(8))
                            {
                                #region Bancos 1 al 14
                                if (!row[1].ToString().Equals(""))
                                    bcoEstado_1.VxdRecaudacionDia30 = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoCredito_1.VxdRecaudacionDia30 = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoSantiago_1.VxdRecaudacionDia30 = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChile_1.VxdRecaudacionDia30 = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoChile_2.VxdRecaudacionDia30 = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoChile_3.VxdRecaudacionDia30 = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoChile_4.VxdRecaudacionDia30 = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoSantander_1.VxdRecaudacionDia30 = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoSantiago_2.VxdRecaudacionDia30 = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoBBVA_1.VxdRecaudacionDia30 = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_2.VxdRecaudacionDia30 = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoSantander_2.VxdRecaudacionDia30 = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSantander_3.VxdRecaudacionDia30 = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoChilePrevired_1.VxdRecaudacionDia30 = Convert.ToInt32(row[14]);
                                #endregion
                            }
                            index++;
                        }

                        if (row[0].ToString().Equals("REG.CARGO BANCARIO") && index.Equals(9)) //Columna C
                        {
                            //Reg Cargo Bancario

                            if (index.Equals(9))
                            {
                                #region Bancos 1 al 14
                                if (!row[1].ToString().Equals(""))
                                    bcoEstado_1.RegCargoBancario = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoCredito_1.RegCargoBancario = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoSantiago_1.RegCargoBancario = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChile_1.RegCargoBancario = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoChile_2.RegCargoBancario = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoChile_3.RegCargoBancario = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoChile_4.RegCargoBancario = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoSantander_1.RegCargoBancario = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoSantiago_2.RegCargoBancario = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoBBVA_1.RegCargoBancario = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_2.RegCargoBancario = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoSantander_2.RegCargoBancario = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSantander_3.RegCargoBancario = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoChilePrevired_1.RegCargoBancario = Convert.ToInt32(row[14]);
                                #endregion
                            }
                            index++;
                        }

                        if (row[0].ToString().Equals("DEV.PARTIDA ERRONEA") && index.Equals(10)) //Columna C
                        {
                            //Dwv.Partida Erroneo

                            if (index.Equals(10))
                            {
                                #region Bancos 1 al 14
                                if (!row[1].ToString().Equals(""))
                                    bcoEstado_1.DevPartidaErronea = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoCredito_1.DevPartidaErronea = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoSantiago_1.DevPartidaErronea = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChile_1.DevPartidaErronea = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoChile_2.DevPartidaErronea = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoChile_3.DevPartidaErronea = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoChile_4.DevPartidaErronea = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoSantander_1.DevPartidaErronea = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoSantiago_2.DevPartidaErronea = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoBBVA_1.DevPartidaErronea = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_2.DevPartidaErronea = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoSantander_2.DevPartidaErronea = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSantander_3.DevPartidaErronea = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoChilePrevired_1.DevPartidaErronea = Convert.ToInt32(row[14]);
                                #endregion
                            }
                            index++;
                        }

                        if (row[0].ToString().Equals("ACLARACION CARGO BANCARIO") && index.Equals(11)) //Columna C
                        {
                            //Aclaracion Cargo Bancario

                            if (index.Equals(11))
                            {
                                #region Bancos 1 al 14
                                if (!row[1].ToString().Equals(""))
                                    bcoEstado_1.AclaracionCargoBancario = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoCredito_1.AclaracionCargoBancario = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoSantiago_1.AclaracionCargoBancario = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChile_1.AclaracionCargoBancario = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoChile_2.AclaracionCargoBancario = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoChile_3.AclaracionCargoBancario = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoChile_4.AclaracionCargoBancario = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoSantander_1.AclaracionCargoBancario = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoSantiago_2.AclaracionCargoBancario = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoBBVA_1.AclaracionCargoBancario = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_2.AclaracionCargoBancario = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoSantander_2.AclaracionCargoBancario = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSantander_3.AclaracionCargoBancario = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoChilePrevired_1.AclaracionCargoBancario = Convert.ToInt32(row[14]);
                                #endregion
                            }
                            index++;
                        }

                        if (row[0].ToString().Equals("CHEQUE PROTESTADO") && index.Equals(12) || row[0].ToString().Equals("CHEQUE PROTESTADO") && index.Equals(28)) //Columna C
                        {
                            //Cheque Protestado

                            if (index.Equals(12))
                            {
                                #region Bancos 1 al 14
                                if (!row[1].ToString().Equals(""))
                                    bcoEstado_1.ChequeProtestado = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoCredito_1.ChequeProtestado = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoSantiago_1.ChequeProtestado = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChile_1.ChequeProtestado = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoChile_2.ChequeProtestado = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoChile_3.ChequeProtestado = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoChile_4.ChequeProtestado = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoSantander_1.ChequeProtestado = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoSantiago_2.ChequeProtestado = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoBBVA_1.ChequeProtestado = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_2.ChequeProtestado = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoSantander_2.ChequeProtestado = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSantander_3.ChequeProtestado = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoChilePrevired_1.ChequeProtestado = Convert.ToInt32(row[14]);
                                #endregion
                            }
                            else
                            {
                                #region Bancos 15 al 28
                                if (!row[1].ToString().Equals(""))
                                    bcoBice_1.ChequeProtestado = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoScotiabank_1.ChequeProtestado = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoChileINP_1.ChequeProtestado = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChilePrevired_2.ChequeProtestado = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoSantiago_3.ChequeProtestado = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoBCI_1.ChequeProtestado = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoEstado_3.ChequeProtestado = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoBCI_2.ChequeProtestado = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoBCI_3.ChequeProtestado = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoItau_1.ChequeProtestado = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_4.ChequeProtestado = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoItauCorpBanca_1.ChequeProtestado = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSecurity_1.ChequeProtestado = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoEstado_5.ChequeProtestado = Convert.ToInt32(row[14]);
                                if (!row[15].ToString().Equals(""))
                                    bcoTotal.ChequeProtestado = Convert.ToInt32(row[15]);
                                #endregion
                            }
                            index++;
                        }

                        if (row[0].ToString().Equals("REG CARGO MAL EFECTUADO") && index.Equals(13)) //Columna C
                        {
                            //Reg Cargo Mal Efectuado                            
                            if (index.Equals(13))
                            {
                                #region Bancos 1 al 14
                                if (!row[1].ToString().Equals(""))
                                    bcoEstado_1.RegCargoMalEfectuado = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoCredito_1.RegCargoMalEfectuado = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoSantiago_1.RegCargoMalEfectuado = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChile_1.RegCargoMalEfectuado = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoChile_2.RegCargoMalEfectuado = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoChile_3.RegCargoMalEfectuado = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoChile_4.RegCargoMalEfectuado = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoSantander_1.RegCargoMalEfectuado = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoSantiago_2.RegCargoMalEfectuado = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoBBVA_1.RegCargoMalEfectuado = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_2.RegCargoMalEfectuado = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoSantander_2.RegCargoMalEfectuado = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSantander_3.RegCargoMalEfectuado = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoChilePrevired_1.RegCargoMalEfectuado = Convert.ToInt32(row[14]);
                                #endregion
                            }
                            index++;
                        }

                        if (row[0].ToString().Equals(" - TRASPASO DE RECAUDACION") && index.Equals(14) || row[0].ToString().Equals(" - TRASPASO DE RECAUDACION") && index.Equals(32)) //Columna C
                        {
                            //Traspaso de Recaudacion

                            if (index.Equals(14))
                            {
                                #region Bancos 1 al 14
                                if (!row[1].ToString().Equals(""))
                                    bcoEstado_1.TraspasoDeRecaudacion = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoCredito_1.TraspasoDeRecaudacion = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoSantiago_1.TraspasoDeRecaudacion = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChile_1.TraspasoDeRecaudacion = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoChile_2.TraspasoDeRecaudacion = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoChile_3.TraspasoDeRecaudacion = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoChile_4.TraspasoDeRecaudacion = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoSantander_1.TraspasoDeRecaudacion = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoSantiago_2.TraspasoDeRecaudacion = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoBBVA_1.TraspasoDeRecaudacion = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_2.TraspasoDeRecaudacion = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoSantander_2.TraspasoDeRecaudacion = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSantander_3.TraspasoDeRecaudacion = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoChilePrevired_1.TraspasoDeRecaudacion = Convert.ToInt32(row[14]);
                                #endregion
                            }
                            else
                            {
                                #region Bancos 15 al 28
                                if (!row[1].ToString().Equals(""))
                                    bcoBice_1.TraspasoDeRecaudacion = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoScotiabank_1.TraspasoDeRecaudacion = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoChileINP_1.TraspasoDeRecaudacion = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChilePrevired_2.TraspasoDeRecaudacion = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoSantiago_3.TraspasoDeRecaudacion = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoBCI_1.TraspasoDeRecaudacion = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoEstado_3.TraspasoDeRecaudacion = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoBCI_2.TraspasoDeRecaudacion = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoBCI_3.TraspasoDeRecaudacion = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoItau_1.TraspasoDeRecaudacion = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_4.TraspasoDeRecaudacion = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoItauCorpBanca_1.TraspasoDeRecaudacion = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSecurity_1.TraspasoDeRecaudacion = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoEstado_5.TraspasoDeRecaudacion = Convert.ToInt32(row[14]);
                                if (!row[15].ToString().Equals(""))
                                    bcoTotal.TraspasoDeRecaudacion = Convert.ToInt32(row[15]);

                                #endregion
                            }
                            index++;
                        }

                        if (row[0].ToString().Equals("CHEQUE MAL DIGITADO") && index.Equals(15)) //Columna C
                        {
                            //VXD RECAUDACION DIA 30

                            if (index.Equals(15))
                            {
                                #region Bancos 1 al 14
                                if (!row[1].ToString().Equals(""))
                                    bcoEstado_1.ChequeMalDigitado = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoCredito_1.ChequeMalDigitado = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoSantiago_1.ChequeMalDigitado = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChile_1.ChequeMalDigitado = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoChile_2.ChequeMalDigitado = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoChile_3.ChequeMalDigitado = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoChile_4.ChequeMalDigitado = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoSantander_1.ChequeMalDigitado = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoSantiago_2.ChequeMalDigitado = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoBBVA_1.ChequeMalDigitado = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_2.ChequeMalDigitado = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoSantander_2.ChequeMalDigitado = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSantander_3.ChequeMalDigitado = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoChilePrevired_1.ChequeMalDigitado = Convert.ToInt32(row[14]);
                                #endregion
                            }
                            index++;
                        }

                        if (row[0].ToString().Equals("CARGO BANCARIO") && index.Equals(16) || row[0].ToString().Equals("CARGO BANCARIO") && index.Equals(34)) //Columna C
                        {
                            //Cargo Banco

                            if (index.Equals(16))
                            {
                                #region Bancos 1 al 14
                                if (!row[1].ToString().Equals(""))
                                    bcoEstado_1.CargoBancario = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoCredito_1.CargoBancario = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoSantiago_1.CargoBancario = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChile_1.CargoBancario = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoChile_2.CargoBancario = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoChile_3.CargoBancario = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoChile_4.CargoBancario = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoSantander_1.CargoBancario = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoSantiago_2.CargoBancario = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoBBVA_1.CargoBancario = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_2.CargoBancario = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoSantander_2.CargoBancario = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSantander_3.CargoBancario = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoChilePrevired_1.CargoBancario = Convert.ToInt32(row[14]);
                                #endregion
                            }
                            else
                            {
                                #region Bancos 15 al 28
                                if (!row[1].ToString().Equals(""))
                                    bcoBice_1.CargoBancario = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoScotiabank_1.CargoBancario = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoChileINP_1.CargoBancario = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChilePrevired_2.CargoBancario = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoSantiago_3.CargoBancario = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoBCI_1.CargoBancario = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoEstado_3.CargoBancario = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoBCI_2.CargoBancario = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoBCI_3.CargoBancario = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoItau_1.CargoBancario = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_4.CargoBancario = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoItauCorpBanca_1.CargoBancario = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSecurity_1.CargoBancario = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoEstado_5.CargoBancario = Convert.ToInt32(row[14]);
                                if (!row[15].ToString().Equals(""))
                                    bcoTotal.CargoBancario = Convert.ToInt32(row[15]);
                                #endregion
                            }
                            index++;
                        }
                        if (row[0].ToString().Equals("TRANSFERENCIA OTRO BANCO ") && index.Equals(17) || row[0].ToString().Equals("TRANSFERENCIA OTRO BANCO ") && index.Equals(35)) //Columna C
                        {
                            //VXD RECAUDACION DIA 30

                            if (index.Equals(17))
                            {
                                #region Bancos 1 al 14
                                if (!row[1].ToString().Equals(""))
                                    bcoEstado_1.TrasferenciaOtroBanco = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoCredito_1.TrasferenciaOtroBanco = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoSantiago_1.TrasferenciaOtroBanco = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChile_1.TrasferenciaOtroBanco = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoChile_2.TrasferenciaOtroBanco = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoChile_3.TrasferenciaOtroBanco = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoChile_4.TrasferenciaOtroBanco = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoSantander_1.TrasferenciaOtroBanco = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoSantiago_2.TrasferenciaOtroBanco = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoBBVA_1.TrasferenciaOtroBanco = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_2.TrasferenciaOtroBanco = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoSantander_2.TrasferenciaOtroBanco = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSantander_3.TrasferenciaOtroBanco = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoChilePrevired_1.TrasferenciaOtroBanco = Convert.ToInt32(row[14]);
                                #endregion
                            }
                            else
                            {
                                #region Bancos 15 al 28
                                if (!row[1].ToString().Equals(""))
                                    bcoBice_1.TrasferenciaOtroBanco = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoScotiabank_1.TrasferenciaOtroBanco = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoChileINP_1.TrasferenciaOtroBanco = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChilePrevired_2.TrasferenciaOtroBanco = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoSantiago_3.TrasferenciaOtroBanco = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoBCI_1.TrasferenciaOtroBanco = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoEstado_3.TrasferenciaOtroBanco = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoBCI_2.TrasferenciaOtroBanco = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoBCI_3.TrasferenciaOtroBanco = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoItau_1.TrasferenciaOtroBanco = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_4.TrasferenciaOtroBanco = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoItauCorpBanca_1.TrasferenciaOtroBanco = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSecurity_1.TrasferenciaOtroBanco = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoEstado_5.TrasferenciaOtroBanco = Convert.ToInt32(row[14]);
                                if (!row[15].ToString().Equals(""))
                                    bcoTotal.TrasferenciaOtroBanco = Convert.ToInt32(row[15]);
                                #endregion
                            }
                            index++;
                        }
                        if (row[0].ToString().Equals("SALDO FINAL") && index.Equals(18) || row[0].ToString().Equals("SALDO FINAL") && index.Equals(36)) //Columna C
                        {
                            //VXD RECAUDACION DIA 30


                            if (index.Equals(18))
                            {
                                #region Bancos 1 al 14
                                if (!row[1].ToString().Equals(""))
                                    bcoEstado_1.SaldoFinal = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoCredito_1.SaldoFinal = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoSantiago_1.SaldoFinal = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChile_1.SaldoFinal = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoChile_2.SaldoFinal = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoChile_3.SaldoFinal = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoChile_4.SaldoFinal = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoSantander_1.SaldoFinal = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoSantiago_2.SaldoFinal = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoBBVA_1.SaldoFinal = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_2.SaldoFinal = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoSantander_2.SaldoFinal = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSantander_3.SaldoFinal = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoChilePrevired_1.SaldoFinal = Convert.ToInt32(row[14]);
                                #endregion
                            }
                            else
                            {
                                #region Bancos 15 al 28
                                if (!row[1].ToString().Equals(""))
                                    bcoBice_1.SaldoFinal = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoScotiabank_1.SaldoFinal = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoChileINP_1.SaldoFinal = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChilePrevired_2.SaldoFinal = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoSantiago_3.SaldoFinal = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoBCI_1.SaldoFinal = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoEstado_3.SaldoFinal = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoBCI_2.SaldoFinal = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoBCI_3.SaldoFinal = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoItau_1.SaldoFinal = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_4.SaldoFinal = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoItauCorpBanca_1.SaldoFinal = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSecurity_1.SaldoFinal = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoEstado_5.SaldoFinal = Convert.ToInt32(row[14]);
                                if (!row[15].ToString().Equals(""))
                                    bcoTotal.SaldoFinal = Convert.ToInt32(row[15]);
                                #endregion
                            }
                            index++;
                        }


                        if (row[0].ToString().Equals("PRESTACIONES LABORALES") && index.Equals(25)) //Columna C
                        {
                            #region Bancos 15 al 28
                            if (!row[1].ToString().Equals(""))
                                bcoBice_1.PrestacionesLaborales = Convert.ToInt32(row[1]);
                            if (!row[2].ToString().Equals(""))
                                bcoScotiabank_1.PrestacionesLaborales = Convert.ToInt32(row[2]);
                            if (!row[3].ToString().Equals(""))
                                bcoChileINP_1.PrestacionesLaborales = Convert.ToInt32(row[3]);
                            if (!row[4].ToString().Equals(""))
                                bcoChilePrevired_2.PrestacionesLaborales = Convert.ToInt32(row[4]);
                            if (!row[5].ToString().Equals(""))
                                bcoSantiago_3.PrestacionesLaborales = Convert.ToInt32(row[5]);
                            if (!row[6].ToString().Equals(""))
                                bcoBCI_1.PrestacionesLaborales = Convert.ToInt32(row[6]);
                            if (!row[7].ToString().Equals(""))
                                bcoEstado_3.PrestacionesLaborales = Convert.ToInt32(row[7]);
                            if (!row[8].ToString().Equals(""))
                                bcoBCI_2.PrestacionesLaborales = Convert.ToInt32(row[8]);
                            if (!row[9].ToString().Equals(""))
                                bcoBCI_3.PrestacionesLaborales = Convert.ToInt32(row[9]);
                            if (!row[10].ToString().Equals(""))
                                bcoItau_1.PrestacionesLaborales = Convert.ToInt32(row[10]);
                            if (!row[11].ToString().Equals(""))
                                bcoEstado_4.PrestacionesLaborales = Convert.ToInt32(row[11]);
                            if (!row[12].ToString().Equals(""))
                                bcoItauCorpBanca_1.PrestacionesLaborales = Convert.ToInt32(row[12]);
                            if (!row[13].ToString().Equals(""))
                                bcoSecurity_1.PrestacionesLaborales = Convert.ToInt32(row[13]);
                            if (!row[14].ToString().Equals(""))
                                bcoEstado_5.PrestacionesLaborales = Convert.ToInt32(row[14]);
                            if (!row[15].ToString().Equals(""))
                                bcoTotal.PrestacionesLaborales = Convert.ToInt32(row[15]);
                            #endregion
                            index++;
                        }

                        if (row[0].ToString().Equals("REV. DEV. CIRCULAR 1411") && index.Equals(26)) //Columna C
                        {
                            #region Bancos 15 al 28
                            if (!row[1].ToString().Equals(""))
                                bcoBice_1.RevDevCircular1411 = Convert.ToInt32(row[1]);
                            if (!row[2].ToString().Equals(""))
                                bcoScotiabank_1.RevDevCircular1411 = Convert.ToInt32(row[2]);
                            if (!row[3].ToString().Equals(""))
                                bcoChileINP_1.RevDevCircular1411 = Convert.ToInt32(row[3]);
                            if (!row[4].ToString().Equals(""))
                                bcoChilePrevired_2.RevDevCircular1411 = Convert.ToInt32(row[4]);
                            if (!row[5].ToString().Equals(""))
                                bcoSantiago_3.RevDevCircular1411 = Convert.ToInt32(row[5]);
                            if (!row[6].ToString().Equals(""))
                                bcoBCI_1.RevDevCircular1411 = Convert.ToInt32(row[6]);
                            if (!row[7].ToString().Equals(""))
                                bcoEstado_3.RevDevCircular1411 = Convert.ToInt32(row[7]);
                            if (!row[8].ToString().Equals(""))
                                bcoBCI_2.RevDevCircular1411 = Convert.ToInt32(row[8]);
                            if (!row[9].ToString().Equals(""))
                                bcoBCI_3.RevDevCircular1411 = Convert.ToInt32(row[9]);
                            if (!row[10].ToString().Equals(""))
                                bcoItau_1.RevDevCircular1411 = Convert.ToInt32(row[10]);
                            if (!row[11].ToString().Equals(""))
                                bcoEstado_4.RevDevCircular1411 = Convert.ToInt32(row[11]);
                            if (!row[12].ToString().Equals(""))
                                bcoItauCorpBanca_1.RevDevCircular1411 = Convert.ToInt32(row[12]);
                            if (!row[13].ToString().Equals(""))
                                bcoSecurity_1.RevDevCircular1411 = Convert.ToInt32(row[13]);
                            if (!row[14].ToString().Equals(""))
                                bcoEstado_5.RevDevCircular1411 = Convert.ToInt32(row[14]);
                            if (!row[15].ToString().Equals(""))
                                bcoTotal.RevDevCircular1411 = Convert.ToInt32(row[15]);
                            #endregion
                            index++;
                        }

                        if (row[0].ToString().Equals(" + APORTE DE CARGOS") && index.Equals(27)) //Columna C
                        {
                            #region Bancos 15 al 28
                            if (!row[1].ToString().Equals(""))
                                bcoBice_1.AportesCargos = Convert.ToInt32(row[1]);
                            if (!row[2].ToString().Equals(""))
                                bcoScotiabank_1.AportesCargos = Convert.ToInt32(row[2]);
                            if (!row[3].ToString().Equals(""))
                                bcoChileINP_1.AportesCargos = Convert.ToInt32(row[3]);
                            if (!row[4].ToString().Equals(""))
                                bcoChilePrevired_2.AportesCargos = Convert.ToInt32(row[4]);
                            if (!row[5].ToString().Equals(""))
                                bcoSantiago_3.AportesCargos = Convert.ToInt32(row[5]);
                            if (!row[6].ToString().Equals(""))
                                bcoBCI_1.AportesCargos = Convert.ToInt32(row[6]);
                            if (!row[7].ToString().Equals(""))
                                bcoEstado_3.AportesCargos = Convert.ToInt32(row[7]);
                            if (!row[8].ToString().Equals(""))
                                bcoBCI_2.AportesCargos = Convert.ToInt32(row[8]);
                            if (!row[9].ToString().Equals(""))
                                bcoBCI_3.AportesCargos = Convert.ToInt32(row[9]);
                            if (!row[10].ToString().Equals(""))
                                bcoItau_1.AportesCargos = Convert.ToInt32(row[10]);
                            if (!row[11].ToString().Equals(""))
                                bcoEstado_4.AportesCargos = Convert.ToInt32(row[11]);
                            if (!row[12].ToString().Equals(""))
                                bcoItauCorpBanca_1.AportesCargos = Convert.ToInt32(row[12]);
                            if (!row[13].ToString().Equals(""))
                                bcoSecurity_1.AportesCargos = Convert.ToInt32(row[13]);
                            if (!row[14].ToString().Equals(""))
                                bcoEstado_5.AportesCargos = Convert.ToInt32(row[14]);
                            if (!row[15].ToString().Equals(""))
                                bcoTotal.AportesCargos = Convert.ToInt32(row[15]);
                            #endregion
                            index++;
                        }

                        if (row[0].ToString().Equals("DEV. ERROR ESTUDIO JURIDICO") && index.Equals(29) || row[0].ToString().Equals("DEV. ERROR ESTUDIO JURIDICO") && index.Equals(30)) //Columna C
                        {
                            if (index.Equals(29))
                            {
                                #region Bancos 15 al 28
                                if (!row[1].ToString().Equals(""))
                                    bcoBice_1.DevErrorEstudioJuridico_1 = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoScotiabank_1.DevErrorEstudioJuridico_1 = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoChileINP_1.DevErrorEstudioJuridico_1 = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChilePrevired_2.DevErrorEstudioJuridico_1 = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoSantiago_3.DevErrorEstudioJuridico_1 = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoBCI_1.DevErrorEstudioJuridico_1 = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoEstado_3.DevErrorEstudioJuridico_1 = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoBCI_2.DevErrorEstudioJuridico_1 = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoBCI_3.DevErrorEstudioJuridico_1 = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoItau_1.DevErrorEstudioJuridico_1 = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_4.DevErrorEstudioJuridico_1 = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoItauCorpBanca_1.DevErrorEstudioJuridico_1 = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSecurity_1.DevErrorEstudioJuridico_1 = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoEstado_5.DevErrorEstudioJuridico_1 = Convert.ToInt32(row[14]);
                                if (!row[15].ToString().Equals(""))
                                    bcoTotal.DevErrorEstudioJuridico_1 = Convert.ToInt32(row[15]);
                                #endregion
                            }
                            else
                            {
                                #region Bancos 15 al 28
                                if (!row[1].ToString().Equals(""))
                                    bcoBice_1.DevErrorEstudioJuridico_2 = Convert.ToInt32(row[1]);
                                if (!row[2].ToString().Equals(""))
                                    bcoScotiabank_1.DevErrorEstudioJuridico_2 = Convert.ToInt32(row[2]);
                                if (!row[3].ToString().Equals(""))
                                    bcoChileINP_1.DevErrorEstudioJuridico_2 = Convert.ToInt32(row[3]);
                                if (!row[4].ToString().Equals(""))
                                    bcoChilePrevired_2.DevErrorEstudioJuridico_2 = Convert.ToInt32(row[4]);
                                if (!row[5].ToString().Equals(""))
                                    bcoSantiago_3.DevErrorEstudioJuridico_2 = Convert.ToInt32(row[5]);
                                if (!row[6].ToString().Equals(""))
                                    bcoBCI_1.DevErrorEstudioJuridico_2 = Convert.ToInt32(row[6]);
                                if (!row[7].ToString().Equals(""))
                                    bcoEstado_3.DevErrorEstudioJuridico_2 = Convert.ToInt32(row[7]);
                                if (!row[8].ToString().Equals(""))
                                    bcoBCI_2.DevErrorEstudioJuridico_2 = Convert.ToInt32(row[8]);
                                if (!row[9].ToString().Equals(""))
                                    bcoBCI_3.DevErrorEstudioJuridico_2 = Convert.ToInt32(row[9]);
                                if (!row[10].ToString().Equals(""))
                                    bcoItau_1.DevErrorEstudioJuridico_2 = Convert.ToInt32(row[10]);
                                if (!row[11].ToString().Equals(""))
                                    bcoEstado_4.DevErrorEstudioJuridico_2 = Convert.ToInt32(row[11]);
                                if (!row[12].ToString().Equals(""))
                                    bcoItauCorpBanca_1.DevErrorEstudioJuridico_2 = Convert.ToInt32(row[12]);
                                if (!row[13].ToString().Equals(""))
                                    bcoSecurity_1.DevErrorEstudioJuridico_2 = Convert.ToInt32(row[13]);
                                if (!row[14].ToString().Equals(""))
                                    bcoEstado_5.DevErrorEstudioJuridico_2 = Convert.ToInt32(row[14]);
                                if (!row[15].ToString().Equals(""))
                                    bcoTotal.DevErrorEstudioJuridico_2 = Convert.ToInt32(row[15]);
                                #endregion
                            }

                            index++;
                        }

                        if (row[0].ToString().Equals("FINANCIAMIENTO") && index.Equals(31)) //Columna C
                        {
                            #region Bancos 15 al 28
                            if (!row[1].ToString().Equals(""))
                                bcoBice_1.Financiamiento = Convert.ToInt32(row[1]);
                            if (!row[2].ToString().Equals(""))
                                bcoScotiabank_1.Financiamiento = Convert.ToInt32(row[2]);
                            if (!row[3].ToString().Equals(""))
                                bcoChileINP_1.Financiamiento = Convert.ToInt32(row[3]);
                            if (!row[4].ToString().Equals(""))
                                bcoChilePrevired_2.Financiamiento = Convert.ToInt32(row[4]);
                            if (!row[5].ToString().Equals(""))
                                bcoSantiago_3.Financiamiento = Convert.ToInt32(row[5]);
                            if (!row[6].ToString().Equals(""))
                                bcoBCI_1.Financiamiento = Convert.ToInt32(row[6]);
                            if (!row[7].ToString().Equals(""))
                                bcoEstado_3.Financiamiento = Convert.ToInt32(row[7]);
                            if (!row[8].ToString().Equals(""))
                                bcoBCI_2.Financiamiento = Convert.ToInt32(row[8]);
                            if (!row[9].ToString().Equals(""))
                                bcoBCI_3.Financiamiento = Convert.ToInt32(row[9]);
                            if (!row[10].ToString().Equals(""))
                                bcoItau_1.Financiamiento = Convert.ToInt32(row[10]);
                            if (!row[11].ToString().Equals(""))
                                bcoEstado_4.Financiamiento = Convert.ToInt32(row[11]);
                            if (!row[12].ToString().Equals(""))
                                bcoItauCorpBanca_1.Financiamiento = Convert.ToInt32(row[12]);
                            if (!row[13].ToString().Equals(""))
                                bcoSecurity_1.Financiamiento = Convert.ToInt32(row[13]);
                            if (!row[14].ToString().Equals(""))
                                bcoEstado_5.Financiamiento = Convert.ToInt32(row[14]);
                            if (!row[15].ToString().Equals(""))
                                bcoTotal.Financiamiento = Convert.ToInt32(row[15]);
                            #endregion
                            index++;
                        }

                        if (row[0].ToString().Equals("DOC. DEVUELTO PROTESTADO") && index.Equals(33)) //Columna C
                        {
                            #region Bancos 15 al 28
                            if (!row[1].ToString().Equals(""))
                                bcoBice_1.DocDevueltoProtestado = Convert.ToInt32(row[1]);
                            if (!row[2].ToString().Equals(""))
                                bcoScotiabank_1.DocDevueltoProtestado = Convert.ToInt32(row[2]);
                            if (!row[3].ToString().Equals(""))
                                bcoChileINP_1.DocDevueltoProtestado = Convert.ToInt32(row[3]);
                            if (!row[4].ToString().Equals(""))
                                bcoChilePrevired_2.DocDevueltoProtestado = Convert.ToInt32(row[4]);
                            if (!row[5].ToString().Equals(""))
                                bcoSantiago_3.DocDevueltoProtestado = Convert.ToInt32(row[5]);
                            if (!row[6].ToString().Equals(""))
                                bcoBCI_1.DocDevueltoProtestado = Convert.ToInt32(row[6]);
                            if (!row[7].ToString().Equals(""))
                                bcoEstado_3.DocDevueltoProtestado = Convert.ToInt32(row[7]);
                            if (!row[8].ToString().Equals(""))
                                bcoBCI_2.DocDevueltoProtestado = Convert.ToInt32(row[8]);
                            if (!row[9].ToString().Equals(""))
                                bcoBCI_3.DocDevueltoProtestado = Convert.ToInt32(row[9]);
                            if (!row[10].ToString().Equals(""))
                                bcoItau_1.DocDevueltoProtestado = Convert.ToInt32(row[10]);
                            if (!row[11].ToString().Equals(""))
                                bcoEstado_4.DocDevueltoProtestado = Convert.ToInt32(row[11]);
                            if (!row[12].ToString().Equals(""))
                                bcoItauCorpBanca_1.DocDevueltoProtestado = Convert.ToInt32(row[12]);
                            if (!row[13].ToString().Equals(""))
                                bcoSecurity_1.DocDevueltoProtestado = Convert.ToInt32(row[13]);
                            if (!row[14].ToString().Equals(""))
                                bcoEstado_5.DocDevueltoProtestado = Convert.ToInt32(row[14]);
                            if (!row[15].ToString().Equals(""))
                                bcoTotal.DocDevueltoProtestado = Convert.ToInt32(row[15]);
                            #endregion
                            index++;
                        }
                        #endregion
                    }
                }

                #region Carga Lista Bancos
                listBco.Add(bcoEstado_1);
                listBco.Add(bcoCredito_1);
                listBco.Add(bcoSantiago_1);
                listBco.Add(bcoChile_1);
                listBco.Add(bcoChile_2);
                listBco.Add(bcoChile_3);
                listBco.Add(bcoChile_4);
                listBco.Add(bcoSantander_1);
                listBco.Add(bcoSantiago_2);
                listBco.Add(bcoBBVA_1);
                listBco.Add(bcoEstado_2);
                listBco.Add(bcoSantander_2);
                listBco.Add(bcoSantander_3);
                listBco.Add(bcoChilePrevired_1);

                listBco.Add(bcoBice_1);
                listBco.Add(bcoScotiabank_1);
                listBco.Add(bcoChileINP_1);
                listBco.Add(bcoChilePrevired_2);
                listBco.Add(bcoSantiago_3);
                listBco.Add(bcoBCI_1);
                listBco.Add(bcoEstado_3);
                listBco.Add(bcoBCI_2);
                listBco.Add(bcoBCI_3);
                listBco.Add(bcoItau_1);
                listBco.Add(bcoEstado_4);
                listBco.Add(bcoItauCorpBanca_1);
                listBco.Add(bcoSecurity_1);
                listBco.Add(bcoEstado_5);
                listBco.Add(bcoTotal);
                #endregion

                ds1 = ProcesaListADataSet(listBco);
            }
            catch (Exception ex)
            {
                throw;
            }
            return ds1;
        }

        public DataSet ProcesaListADataSet(List<BcoRecaudacion> list)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                //Agrega Columnas
                dt.Columns.Add("NombreBco");
                dt.Columns.Add("NroCta");
                dt.Columns.Add("SaldoIncial");
                dt.Columns.Add("RecaudacionCotizaciones");
                dt.Columns.Add("Consignaciones");
                dt.Columns.Add("AbonoMalEfectuado");
                dt.Columns.Add("AjusteChequeProtestado");
                dt.Columns.Add("VxdRecaudacionDia30");
                dt.Columns.Add("RegCargoBancario");
                dt.Columns.Add("DevPartidaErronea");
                dt.Columns.Add("AclaracionCargoBancario");
                dt.Columns.Add("ChequesProtestado");
                dt.Columns.Add("RegCargoMalEfectuado");
                dt.Columns.Add("TraspasoDeRecaudacion");
                dt.Columns.Add("ChequeMalDigitado");
                dt.Columns.Add("CargoBancario");
                dt.Columns.Add("TrasferenciaOtroBanco");
                dt.Columns.Add("SaldoFinal");
                dt.Columns.Add("AbonoMalEfectuado_1");
                dt.Columns.Add("AbonoMalEfectuado_2");
                dt.Columns.Add("PrestacionesLaborales");
                dt.Columns.Add("RevDevCircular1411");
                dt.Columns.Add("AportesCargos");
                dt.Columns.Add("DevErrorEstudioJuridico_1");
                dt.Columns.Add("DevErrorEstudioJuridico_2");
                dt.Columns.Add("Financiamiento");
                dt.Columns.Add("DocDevueltoProtestado");

                foreach (var item in list)
                {
                    //Nueva row
                    DataRow row = dt.NewRow();

                    //Asigna dato
                    row["NombreBco"] = item.NombreBco;
                    row["NroCta"] = item.NroCta;
                    row["SaldoIncial"] = item.SaldoIncial;
                    row["RecaudacionCotizaciones"] = item.RecaudacionCotizaciones;
                    row["Consignaciones"] = item.Consignaciones;
                    row["AbonoMalEfectuado_1"] = item.AbonoMalEfectuado_1;
                    row["AjusteChequeProtestado"] = item.AjusteChequeProtestado;
                    row["VxdRecaudacionDia30"] = item.VxdRecaudacionDia30;
                    row["DevPartidaErronea"] = item.DevPartidaErronea;
                    row["AclaracionCargoBancario"] = item.AclaracionCargoBancario;
                    row["RegCargoBancario"] = item.RegCargoBancario;
                    row["TraspasoDeRecaudacion"] = item.TraspasoDeRecaudacion;
                    row["ChequeMalDigitado"] = item.ChequeMalDigitado;
                    row["CargoBancario"] = item.CargoBancario;
                    row["TrasferenciaOtroBanco"] = item.TrasferenciaOtroBanco;
                    row["SaldoFinal"] = item.SaldoFinal;
                    row["RegCargoMalEfectuado"] = item.RegCargoMalEfectuado;
                    row["AbonoMalEfectuado_2"] = item.AbonoMalEfectuado_2;
                    row["PrestacionesLaborales"] = item.PrestacionesLaborales;
                    row["RevDevCircular1411"] = item.RevDevCircular1411;
                    row["AportesCargos"] = item.AportesCargos;
                    row["ChequesProtestado"] = item.ChequeProtestado;
                    row["DevErrorEstudioJuridico_1"] = item.DevErrorEstudioJuridico_1;
                    row["DevErrorEstudioJuridico_2"] = item.DevErrorEstudioJuridico_2;
                    row["Financiamiento"] = item.Financiamiento;
                    row["DocDevueltoProtestado"] = item.DocDevueltoProtestado;
                    row["AbonoMalEfectuado"] = item.AbonoMalEfectuado;

                    dt.Rows.Add(row);

                }
                ds.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                throw;
            }
            return ds;
        }

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
