using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wsIntegracionContable
{
    public class BcoRecaudacion
    {
        public string NombreBco { get; set; }
        public string NroCta { get; set; }
        public int SaldoIncial { get; set; }
        public int RecaudacionCotizaciones { get; set; }
        public int Consignaciones { get; set; }
        public int AbonoMalEfectuado { get; set; }
        public int AjusteChequeProtestado { get; set; }
        public int VxdRecaudacionDia30 { get; set; }
        public int RegCargoBancario { get; set; }
        public int DevPartidaErronea { get; set; }
        public int AclaracionCargoBancario { get; set; }
        public int ChequeProtestado { get; set; }
        public int RegCargoMalEfectuado { get; set; }
        public int TraspasoDeRecaudacion { get; set; }
        public int ChequeMalDigitado { get; set; }
        public int CargoBancario { get; set; }
        public int TrasferenciaOtroBanco { get; set; }
        public int SaldoFinal { get; set; }
        public int AbonoMalEfectuado_1 { get; set; }
        public int AbonoMalEfectuado_2 { get; set; }
        public int PrestacionesLaborales { get; set; }
        public int RevDevCircular1411 { get; set; }
        public int AportesCargos { get; set; }
        public int DevErrorEstudioJuridico_1 { get; set; }
        public int DevErrorEstudioJuridico_2 { get; set; }
        public int Financiamiento { get; set; }
        public int DocDevueltoProtestado { get; set; }
        public int Total { get; set; }

        public BcoRecaudacion()
        {
            NombreBco = string.Empty;
            NroCta = string.Empty;
            SaldoIncial = 0;
            RecaudacionCotizaciones = 0;
            Consignaciones = 0;
            AbonoMalEfectuado = 0;
            AjusteChequeProtestado = 0;
            VxdRecaudacionDia30 = 0;
            RegCargoBancario = 0;
            DevPartidaErronea = 0;
            AclaracionCargoBancario = 0;
            RegCargoMalEfectuado = 0;
            TraspasoDeRecaudacion = 0;
            ChequeMalDigitado = 0;
            CargoBancario = 0;
            TrasferenciaOtroBanco = 0;
            SaldoFinal = 0;
            AbonoMalEfectuado_1 = 0;
            AbonoMalEfectuado_2 = 0;
            PrestacionesLaborales = 0;
            RevDevCircular1411 = 0;
            AportesCargos = 0;
            ChequeProtestado = 0;
            DevErrorEstudioJuridico_1 = 0;
            DevErrorEstudioJuridico_2 = 0;
            Financiamiento = 0;
            DocDevueltoProtestado = 0;
            Total = 0;
        }
    }
}
