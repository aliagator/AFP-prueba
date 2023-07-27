Imports Sonda.Net
Imports Sonda.Gestion.Adm.Sys
Imports Sonda.Gestion.Adm.Sys.CodeCompletion
Imports Microsoft.VisualBasic.Interaction
Imports System.Reflection
Imports Sonda.Net.Reports
Imports sysIntegracionContable
Imports wsIntegracionContable

Public MustInherit Class ineAcreditacionDiario
    Inherits Sonda.Net.Page.Body
    Protected _dpage As Page.DefaultPage

    Protected WithEvents fechaAcreditacion As Sonda.Net.Control.FechaAdv
    Protected WithEvents DataGridSnd2 As Sonda.Net.Control.DataGridSnd
    'Protected WithEvents btnConsultar As Sonda.Net.Control.Button
    Protected WithEvents lbl_Usuario As Sonda.Net.Control.Label
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
                Me.lbl_Usuario.Text = Session("ID_ADM")
                Me.lblHora.Text = FechaMEC.wmahora().ToString("HH:mm:ss")

                fechaAcreditacion.FechaBD = FechaMEC.wmahora()
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
            dataAFP = info.InformeResumenDiario("1", "01-01-2022")

        Catch ex As SondaException
            _dpage.SondaExceptionError(ex)
        Catch ex As System.Web.Services.Protocols.SoapException
            _dpage.SondaExceptionError(ex)
        Catch ex As Exception
            _dpage.SondaExceptionError(ex)
        End Try
    End Sub

    Private Sub DataGridSnd_DataBinding(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridSnd2.DataBinding


        DataGridSnd2.GetColumnByDataField("NUMERO_ID").HeaderText = "N° Lote Acreditado"
        DataGridSnd2.GetColumnByDataField("NUM_REF_ORIGEN1").HeaderText = "Folio Lote Recuadación"
        DataGridSnd2.GetColumnByDataField("TOTAL_RECAUDACION").HeaderText = "Total Recuadación"
        DataGridSnd2.GetColumnByDataField("TOT_REGISTROS_CREADOS").HeaderText = "N° Transacciones Acreditadas"
        DataGridSnd2.GetColumnByDataField("ID_USUARIO_PROCESO").HeaderText = "Usuario"
        DataGridSnd2.GetColumnByDataField("CARGADO").HeaderText = "Estado Carga"

        Dim pesos_format As BoundColumn
        pesos_format = DataGridSnd2.GetColumnByDataField("TOTAL_RECAUDACION") : pesos_format.DataFormatString = "{0:c0}"


    End Sub

    Private Sub CargarGrilla()
        Dim info As New InformeRecaudacion()
        Dim dataAFP As DataSet
        Try
            dataAFP = info.InformeResumenDiario(Session("ID_ADM"), Me.fechaAcreditacion.Text)

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

            rpt.AddParameter("VID_ADM", ContextoSesion("ID_ADM"))
            rpt.AddParameter("VFECHA_DIARIA", Me.fechaAcreditacion.Text)

            parametrosSalida = New Object() {rpt}
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