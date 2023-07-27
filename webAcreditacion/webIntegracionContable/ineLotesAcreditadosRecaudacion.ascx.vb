Imports Sonda.Net
Imports Sonda.Gestion.Adm.Sys
Imports Sonda.Gestion.Adm.Sys.CodeCompletion
Imports sysIntegracionContable
Imports Microsoft.VisualBasic.Interaction
Imports System.Reflection
Imports Sonda.Net.Reports
Imports wsIntegracionContable

Public MustInherit Class ineLotesAcreditadosRecaudacion
    Inherits Sonda.Net.Page.Body
    Protected _dpage As Page.DefaultPage

    Protected WithEvents fechaLotes As Sonda.Net.Control.FechaAdv
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
                fechaLotes.FechaBD = FechaMEC.wmahora()
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
            dataAFP = info.DetallePlanillaTesoreria("1", Me.fechaLotes.Text)

        Catch ex As SondaException
            _dpage.SondaExceptionError(ex)
        Catch ex As System.Web.Services.Protocols.SoapException
            _dpage.SondaExceptionError(ex)
        Catch ex As Exception
            _dpage.SondaExceptionError(ex)
        End Try
    End Sub

    Private Sub DataGridSnd_DataBinding(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridSnd2.DataBinding



        DataGridSnd2.GetColumnByDataField("TIPO_LOTE").HeaderText = "Tipo de Lote"
        DataGridSnd2.GetColumnByDataField("NUM_REF_ORIGEN2").HeaderText = "Lote"
        'DataGridSnd2.GetColumnByDataField("Validación").HeaderText = "Validación"
        DataGridSnd2.GetColumnByDataField("LOTE_CIERRE").HeaderText = "Entrada"
        DataGridSnd2.GetColumnByDataField("LOTE_CIERRE").HeaderText = "Total depósitos Fondo "
        'DataGridSnd2.GetColumnByDataField("Validación 1").HeaderText = "Validación 1"
        DataGridSnd2.GetColumnByDataField("RECAUDACION_SOBRANTE").HeaderText = "Sobrante"
        DataGridSnd2.GetColumnByDataField("RECAUDACION_DOC_INC").HeaderText = "Doc Inc"
        DataGridSnd2.GetColumnByDataField("RECAUDACION_CAI").HeaderText = "Excesos CAI"
        DataGridSnd2.GetColumnByDataField("RECAUDACION_COSTAS").HeaderText = "Costas"
        DataGridSnd2.GetColumnByDataField("RECAUDACION_DESC_POS").HeaderText = "Desc +"
        DataGridSnd2.GetColumnByDataField("RECAUDACION_DESC_NEG").HeaderText = "Desc -"
        DataGridSnd2.GetColumnByDataField("TOTAL_DETALLE_CAJA_ML").HeaderText = "i.a-Detalle por Caja (Pesos)"
        DataGridSnd2.GetColumnByDataField("TOTAL_DETALLE").HeaderText = "Total"
        'DataGridSnd2.GetColumnByDataField("Validación 2").HeaderText = "Validación 2"
        DataGridSnd2.GetColumnByDataField("TOTAL_ENVIO_ACR").HeaderText = "Total Envío ACR "
        DataGridSnd2.GetColumnByDataField("TOTAL_ENVIO_ACR").HeaderText = "i.a-Detalle por Caja (Pesos)"
        'DataGridSnd2.GetColumnByDataField("Validación 3").HeaderText = "Validación 3"
        DataGridSnd2.GetColumnByDataField("LOTE_SALUD_FONASA").HeaderText = "7% Salud Fonasa"
        DataGridSnd2.GetColumnByDataField("LOTE_ENTRADA_FDC").HeaderText = "Entrada FDC"
        DataGridSnd2.GetColumnByDataField("LOTE_SOBRANTE_FDC").HeaderText = "Sobrante FDC "
        DataGridSnd2.GetColumnByDataField("LOTE_DOC_INC").HeaderText = "Doc.Inc. FDC"
        DataGridSnd2.GetColumnByDataField("TOTAL_LOTE").HeaderText = "TOTAL"
        DataGridSnd2.GetColumnByDataField("RECAUDACION_FONDO_C").HeaderText = "Cuentas personales Fondo C"
        DataGridSnd2.GetColumnByDataField("RECAUDACION_OTROS_FONDOS").HeaderText = "Cuentas personales otros Fondos"
        DataGridSnd2.GetColumnByDataField("RECAUDACION_REZAGO").HeaderText = "Rezagos"
        DataGridSnd2.GetColumnByDataField("RECAUDACION_TRANSFERENCIA").HeaderText = "Transferencias"
        DataGridSnd2.GetColumnByDataField("TOTAL_RACAUDACION").HeaderText = "Total"
        'DataGridSnd2.GetColumnByDataField("Validación 4").HeaderText = "Validación 4"
        DataGridSnd2.GetColumnByDataField("CTA_PERSONALES_OTROS").HeaderText = "Cuentas personales otros Fondos"
        DataGridSnd2.GetColumnByDataField("ENTRADA_RENT_OTROS").HeaderText = "Entrada (Rentabilidad otros Fondos)"
        'DataGridSnd2.GetColumnByDataField("Validación 5").HeaderText = "Validación 5"
        DataGridSnd2.GetColumnByDataField("TOTAL_PESOS_RENT").HeaderText = "Total pesos (Rentabilidad otros Fondos)"
        DataGridSnd2.GetColumnByDataField("ENTRADA_REPA").HeaderText = "Entrada (REPA Fondo destino)"
        DataGridSnd2.GetColumnByDataField("ENTRADA_ACREDITA").HeaderText = "Entrada (Acreditación Fondo Destino)"
        'DataGridSnd2.GetColumnByDataField("Validación 6").HeaderText = "Validación 6"
        DataGridSnd2.GetColumnByDataField("A_ENTRADA_ML_C").HeaderText = "A Entrada desde C"
        DataGridSnd2.GetColumnByDataField("B_ENTRADA_ML_C").HeaderText = "B Entrada desde C"
        DataGridSnd2.GetColumnByDataField("D_ENTRADA_ML_C").HeaderText = "D Entrada desde C"
        DataGridSnd2.GetColumnByDataField("E_ENTRADA_ML_C").HeaderText = "E Entrada desde C"
        DataGridSnd2.GetColumnByDataField("TOTAL_ENTRADA").HeaderText = "TOTAL"
        'DataGridSnd2.GetColumnByDataField("Validación 7").HeaderText = "Validación 7"
        DataGridSnd2.GetColumnByDataField("A_ENTRADA_CUO_C").HeaderText = "A Entrada desde C"
        DataGridSnd2.GetColumnByDataField("B_ENTRADA_CUO_C").HeaderText = "B Entrada desde C"
        DataGridSnd2.GetColumnByDataField("C_ENTRADA_CUO_C").HeaderText = "C Entrada desde C"
        DataGridSnd2.GetColumnByDataField("D_ENTRADA_CUO_C").HeaderText = "D Entrada desde C"
        DataGridSnd2.GetColumnByDataField("E_ENTRADA_CUO_C").HeaderText = "E Entrada desde C"
        DataGridSnd2.GetColumnByDataField("TOTAL_CUOTA").HeaderText = "TOTAL"
        DataGridSnd2.GetColumnByDataField("TOTAL_CUOTA_TOTAL").HeaderText = "TOTAL INFORME"
        'DataGridSnd2.GetColumnByDataField("Validación 8").HeaderText = "Validación 8"
        DataGridSnd2.GetColumnByDataField("REPA_FONDO_ML_A").HeaderText = "Repa Fondo A"
        DataGridSnd2.GetColumnByDataField("REPA_FONDO_ML_B").HeaderText = "Repa Fondo B"
        DataGridSnd2.GetColumnByDataField("REPA_FONDO_ML_D").HeaderText = "Repa Fondo D"
        DataGridSnd2.GetColumnByDataField("REPA_FONDO_ML_E").HeaderText = "Repa Fondo E"
        DataGridSnd2.GetColumnByDataField("TOTAL_REPA").HeaderText = "TOTAL REPA "
        DataGridSnd2.GetColumnByDataField("ACRE_FONDO_A").HeaderText = "Acreditacion Fondo destino Fondo A"
        DataGridSnd2.GetColumnByDataField("ACRE_FONDO_B").HeaderText = "Acreditacion Fondo destino Fondo B"
        DataGridSnd2.GetColumnByDataField("ACRE_FONDO_D").HeaderText = "Acreditacion Fondo destino Fondo D"
        DataGridSnd2.GetColumnByDataField("ACRE_FONDO_E").HeaderText = "Acreditacion Fondo destino Fondo E"
        DataGridSnd2.GetColumnByDataField("TOTAL_FONDO").HeaderText = "TOTAL ACREDITACIÓN"
        'DataGridSnd2.GetColumnByDataField("Validación 9").HeaderText = "Validación 9"
        DataGridSnd2.GetColumnByDataField("REPA_FONDO_CUO_A").HeaderText = "Repa Fondo A"
        DataGridSnd2.GetColumnByDataField("REPA_FONDO_CUO_B").HeaderText = "Repa Fondo B"
        DataGridSnd2.GetColumnByDataField("REPA_FONDO_CUO_D").HeaderText = "Repa Fondo D"
        DataGridSnd2.GetColumnByDataField("REPA_FONDO_CUO_E").HeaderText = "Repa Fondo E"
        DataGridSnd2.GetColumnByDataField("FONDO_CUOTA").HeaderText = "TOTAL REPA"
        DataGridSnd2.GetColumnByDataField("ACRE_DESTINO_FONDO_A").HeaderText = "Acreditacion Fondo destino Fondo A"
        DataGridSnd2.GetColumnByDataField("ACRE_DESTINO_FONDO_B").HeaderText = "Acreditacion Fondo destino Fondo B"
        DataGridSnd2.GetColumnByDataField("ACRE_DESTINO_FONDO_D").HeaderText = "Acreditacion Fondo destino Fondo D"
        DataGridSnd2.GetColumnByDataField("ACRE_DESTINO_FONDO_E").HeaderText = "Acreditacion Fondo destino Fondo E"
        DataGridSnd2.GetColumnByDataField("DESTINO").HeaderText = "TOTAL ACREDITACIÓN"
        DataGridSnd2.GetColumnByDataField("AJUSTE_DECIMAL_A").HeaderText = "Ajuste Decimal Fondo A"
        DataGridSnd2.GetColumnByDataField("AJUSTE_DECIMAL_B").HeaderText = "Ajuste Decimal Fondo B"
        DataGridSnd2.GetColumnByDataField("AJUSTE_DECIMAL_D").HeaderText = "Ajuste Decimal Fondo D"
        DataGridSnd2.GetColumnByDataField("AJUSTE_DECIMAL_E").HeaderText = "Ajuste Decimal Fondo E"
        DataGridSnd2.GetColumnByDataField("TOTAL_DECIMAL").HeaderText = "TOTAL AJUSTE DECIMAL"
        'DataGridSnd2.GetColumnByDataField("Validación 10").HeaderText = "Validación 10"
        DataGridSnd2.GetColumnByDataField("COMISIONES_FONDO_A").HeaderText = "FONDO A"
        DataGridSnd2.GetColumnByDataField("COMISIONES_FONDO_B").HeaderText = "FONDO B"
        DataGridSnd2.GetColumnByDataField("COMISIONES_FONDO_C").HeaderText = "FONDO C"
        DataGridSnd2.GetColumnByDataField("COMISIONES_FONDO_D").HeaderText = "FONDO D"
        DataGridSnd2.GetColumnByDataField("COMISIONES_FONDO_E").HeaderText = "FONDO E"
        DataGridSnd2.GetColumnByDataField("TOTAL_COMISION").HeaderText = "TOTAL"


        Dim pesos_format As BoundColumn

        pesos_format = DataGridSnd2.GetColumnByDataField("LOTE_CIERRE") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("LOTE_CIERRE") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("RECAUDACION_SOBRANTE") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("RECAUDACION_DOC_INC") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("RECAUDACION_CAI") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("RECAUDACION_COSTAS") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("RECAUDACION_DESC_POS") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("RECAUDACION_DESC_NEG") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("TOTAL_DETALLE_CAJA_ML") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("TOTAL_DETALLE") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("TOTAL_ENVIO_ACR") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("TOTAL_ENVIO_ACR") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("LOTE_SALUD_FONASA") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("LOTE_ENTRADA_FDC") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("LOTE_SOBRANTE_FDC") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("LOTE_DOC_INC") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("TOTAL_LOTE") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("RECAUDACION_FONDO_C") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("RECAUDACION_OTROS_FONDOS") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("RECAUDACION_REZAGO") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("RECAUDACION_TRANSFERENCIA") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("TOTAL_RACAUDACION") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("CTA_PERSONALES_OTROS") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("ENTRADA_RENT_OTROS") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("TOTAL_PESOS_RENT") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("ENTRADA_REPA") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("ENTRADA_ACREDITA") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("A_ENTRADA_ML_C") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("B_ENTRADA_ML_C") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("D_ENTRADA_ML_C") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("E_ENTRADA_ML_C") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("TOTAL_ENTRADA") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("A_ENTRADA_CUO_C") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("B_ENTRADA_CUO_C") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("C_ENTRADA_CUO_C") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("D_ENTRADA_CUO_C") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("E_ENTRADA_CUO_C") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("TOTAL_CUOTA") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("TOTAL_CUOTA_TOTAL") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("REPA_FONDO_ML_A") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("REPA_FONDO_ML_B") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("REPA_FONDO_ML_D") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("REPA_FONDO_ML_E") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("TOTAL_REPA") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("ACRE_FONDO_A") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("ACRE_FONDO_B") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("ACRE_FONDO_D") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("ACRE_FONDO_E") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("TOTAL_FONDO") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("REPA_FONDO_CUO_A") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("REPA_FONDO_CUO_B") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("REPA_FONDO_CUO_D") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("REPA_FONDO_CUO_E") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("FONDO_CUOTA") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("ACRE_DESTINO_FONDO_A") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("ACRE_DESTINO_FONDO_B") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("ACRE_DESTINO_FONDO_D") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("ACRE_DESTINO_FONDO_E") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("DESTINO") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("AJUSTE_DECIMAL_A") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("AJUSTE_DECIMAL_B") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("AJUSTE_DECIMAL_D") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("AJUSTE_DECIMAL_E") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("TOTAL_DECIMAL") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("COMISIONES_FONDO_A") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("COMISIONES_FONDO_B") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("COMISIONES_FONDO_C") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("COMISIONES_FONDO_D") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("COMISIONES_FONDO_E") : pesos_format.DataFormatString = "{0:c0}"
        pesos_format = DataGridSnd2.GetColumnByDataField("TOTAL_COMISION") : pesos_format.DataFormatString = "{0:c0}"




    End Sub

    Private Sub CargarGrilla()
        Dim info As New InformeRecaudacion()
        Dim dataAFP As DataSet
        Try
            dataAFP = info.LotesAcreditadosRecaudacion(Session("ID_ADM"), Me.fechaLotes.Text)

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
            rpt = New Report("rpt\rptReporteRecaudacionDiaria.rpt", "Informe Carga Acreditación")

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