Imports Sonda.Net
Imports Sonda.Gestion.Adm.Sys
Imports Sonda.Gestion.Adm.Sys.CodeCompletion
Imports Microsoft.VisualBasic.Interaction
Imports System.Reflection
Imports Sonda.Net.Reports
Imports sysIntegracionContable
Imports wsIntegracionContable

Public MustInherit Class ineDetallePlanillaTesoreria
    Inherits Sonda.Net.Page.Body
    Protected _dpage As Page.DefaultPage

    Protected WithEvents fechaAcreditacion As Sonda.Net.Control.FechaAdv
    Protected WithEvents DataGridSnd2 As Sonda.Net.Control.DataGridSnd
    'Protected WithEvents btnConsultar As Sonda.Net.Control.Button
    'Protected WithEvents lblTimbreCaja As Sonda.Net.Control.Label
    'Protected WithEvents MsgBoxSnd1 As Sonda.Net.Control.MsgBoxSnd
    Dim FechaMEC As New Sonda.Gestion.Adm.WS.Soporte.wsFecha()
    'Protected WithEvents rblTipo As Sonda.Net.Control.RadioButtonList

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _dpage = Page

        Try
            If Not Page.IsPostBack Then
                fechaPlanilla.FechaBD = FechaMEC.wmahora()
            End If
        Catch ex As SondaException
            _dpage.SondaExceptionError(ex)
        Catch ex As System.Web.Services.Protocols.SoapException
            _dpage.SondaExceptionError(ex)
        Catch ex As Exception
            _dpage.SondaExceptionError(ex)
        End Try
    End Sub

    Protected Sub btnTest_Click(sender As Object, e As EventArgs)
        Dim info As New InformeRecaudacion()
        Dim dataAFP As DataSet
        Try
            dataAFP = info.DetallePlanillaTesoreria("1", Me.fechaPlanilla.Text)

        Catch ex As SondaException
            _dpage.SondaExceptionError(ex)
        Catch ex As System.Web.Services.Protocols.SoapException
            _dpage.SondaExceptionError(ex)
        Catch ex As Exception
            _dpage.SondaExceptionError(ex)
        End Try
    End Sub

    Private Sub DataGridSnd_DataBinding(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridSnd2.DataBinding


        DataGridSnd2.GetColumnByDataField("NOMBRE_BANCO").HeaderText = "NOMBRE BANCO"
        DataGridSnd2.GetColumnByDataField("NRO_CTA").HeaderText = "NUMERO CUENTA"
        DataGridSnd2.GetColumnByDataField("SALDO_INCIAL").HeaderText = "SALDO INCIAL"
        DataGridSnd2.GetColumnByDataField("RECAUDACION_COTIZACIONES").HeaderText = "RECAUDACION COTIZACIONES"
        DataGridSnd2.GetColumnByDataField("CONSIGNACIONES").HeaderText = "CONSIGNACIONES"
        DataGridSnd2.GetColumnByDataField("ABONO_MAL_EFECTUADO").HeaderText = "ABONO MAL EFECTUADO"
        DataGridSnd2.GetColumnByDataField("AJUSTE_CHEQUE_PROTESTADO").HeaderText = "AJUSTE CHEQUE PROTESTADO"
        DataGridSnd2.GetColumnByDataField("RECAUDACION_DIA_30").HeaderText = "RECAUDACION DIA 30"
        DataGridSnd2.GetColumnByDataField("REG_CARGO_BANCARIO").HeaderText = "REG CARGO BANCARIO"
        DataGridSnd2.GetColumnByDataField("DEV_PARTIDA_ERRONEA").HeaderText = "DEV PARTIDA ERRONEA"
        DataGridSnd2.GetColumnByDataField("ACLARACION_CARGO_BANCARIO").HeaderText = "ACLARACION CARGO BANCARIO"
        DataGridSnd2.GetColumnByDataField("CHEQUES_PROTESTADO").HeaderText = "CHEQUES PROTESTADO"
        DataGridSnd2.GetColumnByDataField("REGCARGO_MAL_EFECTUADO").HeaderText = "REGCARGO MAL EFECTUADO"
        DataGridSnd2.GetColumnByDataField("TRASPASO_RECAUDACION").HeaderText = "TRASPASO RECAUDACION"
        DataGridSnd2.GetColumnByDataField("CHEQUE_MAL_DIGITADO").HeaderText = "CHEQUE MAL DIGITADO"
        DataGridSnd2.GetColumnByDataField("CARGO_BANCARIO").HeaderText = "CARGO BANCARIO"
        DataGridSnd2.GetColumnByDataField("TRASFERENCIA_OTRO_BANCO").HeaderText = "TRASFERENCIA OTRO BANCO"
        DataGridSnd2.GetColumnByDataField("SALDO_FINAL").HeaderText = "SALDO FINAL"
        DataGridSnd2.GetColumnByDataField("ABONO_MAL_EFECTUADO_1").HeaderText = "ABONO MAL EFECTUADO_1"
        DataGridSnd2.GetColumnByDataField("ABONO_MAL_EFECTUADO_2").HeaderText = "ABONO MAL EFECTUADO_2"
        DataGridSnd2.GetColumnByDataField("PRESTACIONES_LABORALES").HeaderText = "PRESTACIONESLABORALES"
        DataGridSnd2.GetColumnByDataField("CIRCULAR_1411").HeaderText = "CIRCULAR 1411"
        DataGridSnd2.GetColumnByDataField("APORTES_CARGOS").HeaderText = "APORTES CARGOS"
        DataGridSnd2.GetColumnByDataField("ERRORESTUDIOJURIDICO_1").HeaderText = "ERRORES ESTUDIO JURIDICO 1"
        DataGridSnd2.GetColumnByDataField("ERRORESTUDIOJURIDICO_2").HeaderText = "ERRORES ESTUDIO JURIDICO 2"
        DataGridSnd2.GetColumnByDataField("FINANCIAMIENTO").HeaderText = "FINANCIAMIENTO"
        DataGridSnd2.GetColumnByDataField("VUELTO_PROTESTADO").HeaderText = "VUELTO PROTESTADO"



        Dim pesos_format As BoundColumn
        pesos_format = DataGridSnd2.GetColumnByDataField("SALDO_INCIAL") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("RECAUDACION_COTIZACIONES") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("CONSIGNACIONES") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("ABONO_MAL_EFECTUADO") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("AJUSTE_CHEQUE_PROTESTADO") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("RECAUDACION_DIA_30") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("REG_CARGO_BANCARIO") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("DEV_PARTIDA_ERRONEA") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("ACLARACION_CARGO_BANCARIO") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("CHEQUES_PROTESTADO") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("REGCARGO_MAL_EFECTUADO") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("TRASPASO_RECAUDACION") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("CHEQUE_MAL_DIGITADO") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("CARGO_BANCARIO") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("TRASFERENCIA_OTRO_BANCO") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("SALDO_FINAL") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("ABONO_MAL_EFECTUADO_1") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("ABONO_MAL_EFECTUADO_2") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("PRESTACIONES_LABORALES") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("CIRCULAR_1411") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("APORTES_CARGOS") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("ERRORESTUDIOJURIDICO_1") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("ERRORESTUDIOJURIDICO_2") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("FINANCIAMIENTO") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("VUELTO_PROTESTADO") : pesos_format.DataFormatString = "{0:c0}"



    End Sub

    Private Sub CargarGrilla()
        Dim info As New InformeRecaudacion()
        Dim dataAFP As DataSet
        Try
            dataAFP = info.DetallePlanillaTesoreria(Session("ID_ADM"), Me.fechaPlanilla.Text)

            If dataAFP.Tables(0).Rows.Count = 0 Then
                mensaje("El proceso no encontro registros")
            End If

            DataGridSnd2.DataSet = dataAFP
            DataGridSnd2.DataSet.AcceptChanges()

            If Not IsNothing(DataGridSnd2.DataSet) AndAlso DataGridSnd2.DataSet.Tables(0).Rows.Count > 0 Then
                DataGridSnd2.CurrentPageIndex = 0
            End If

        Catch ex As SondaException
            _dpage.SondaExceptionError(ex)
        Catch ex As System.Web.Services.Protocols.SoapException
            _dpage.SondaExceptionError(ex)
        Catch ex As Exception
            _dpage.SondaExceptionError(ex)
        End Try
    End Sub

    Private Sub mensaje(ByVal mensaje As String)
        Me.MsgBoxSnd1.show(mensaje, "Recaudacion", Control.Util.TipoDlg.OkDlg, )
    End Sub

    Protected Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click

        Dim log_ As New Log4Net
        Dim nameMethod As String = MethodBase.GetCurrentMethod().Name
        Try
            log_.WriteLogInfo(nameMethod, "Inicia consulta informe recaudacion")
            CargarGrilla()
        Catch ex As SondaException
            _dpage.SondaExceptionError(ex)
        Catch ex As System.Web.Services.Protocols.SoapException
            _dpage.SondaExceptionError(ex)
        Catch ex As Exception
            _dpage.SondaExceptionError(ex)
        End Try

    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Dim rpt As Report
        Try
            rpt = New Report("rpt\rptDetallePlanillaTesoreria.rpt", "Informe Carga Acreditación")

            Server_Transfer("default.aspx?url=PreImpresion")

        Catch ex As System.Threading.ThreadAbortException
            rpt = Nothing
        Catch ex As SondaException
            _dpage.SondaExceptionError(ex)
        Catch ex As System.Web.Services.Protocols.SoapException
            _dpage.SondaExceptionError(ex)
        Catch ex As Exception
            _dpage.SondaExceptionError(ex)
        End Try
    End Sub
End Class