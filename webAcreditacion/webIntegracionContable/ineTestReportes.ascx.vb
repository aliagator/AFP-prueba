Imports Sonda.Net
Imports Sonda.Gestion.Adm.Sys
Imports Sonda.Gestion.Adm.Sys.CodeCompletion
Imports sysIntegracionContable
Imports Microsoft.VisualBasic.Interaction
Imports System.Reflection
Imports Sonda.Net.Reports
Imports wsIntegracionContable

Public MustInherit Class ineTestReportes
    Inherits Sonda.Net.Page.Body
    Protected _dpage As Page.DefaultPage
    Protected WithEvents Label1 As Sonda.Net.Control.Label
    Protected WithEvents fechaIniCaja As Sonda.Net.Control.FechaAdv
    Protected WithEvents fechaFinCaja As Sonda.Net.Control.FechaAdv
    Protected WithEvents fechaIniRecaudacion As Sonda.Net.Control.FechaAdv
    Protected WithEvents fechaFinRecaudacion As Sonda.Net.Control.FechaAdv
    Protected WithEvents DataGridSnd1 As Sonda.Net.Control.DataGridSnd
    Protected WithEvents btnConsultar As Sonda.Net.Control.Button
    Protected WithEvents lblTimbreCaja As Sonda.Net.Control.Label
    Protected WithEvents lblFechaAcre As Sonda.Net.Control.Label
    Protected WithEvents MsgBoxSnd1 As Sonda.Net.Control.MsgBoxSnd
    Dim FechaMEC As New Sonda.Gestion.Adm.WS.Soporte.wsFecha()
    Protected WithEvents rblTipo As Sonda.Net.Control.RadioButtonList

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
                fechaIniCaja.FechaBD = FechaMEC.wmahora()
                fechaFinCaja.FechaBD = FechaMEC.wmahora()
                fechaIniRecaudacion.FechaBD = FechaMEC.wmahora()
                fechaFinRecaudacion.FechaBD = FechaMEC.wmahora()
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
            dataAFP = info.InformeResumen("1", "01-02-2022", "28-02-2022", "03-02-2022", "03-02-2022")

        Catch ex As SondaException
            _dpage.SondaExceptionError(ex)
        Catch ex As System.Web.Services.Protocols.SoapException
            _dpage.SondaExceptionError(ex)
        Catch ex As Exception
            _dpage.SondaExceptionError(ex)
        End Try
    End Sub

    Private Sub DataGridSnd1_DataBinding(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridSnd1.DataBinding

        DataGridSnd1.GetColumnByDataField("ID_ADM").Visible = False
        DataGridSnd1.GetColumnByDataField("PATRIMONIO").HeaderText = "Patrimonio"
        DataGridSnd1.GetColumnByDataField("REG_REZAGOS").HeaderText = "Reg. Rezagos"
        DataGridSnd1.GetColumnByDataField("FONDO_A").HeaderText = "Fondo A"
        DataGridSnd1.GetColumnByDataField("FONDO_B").HeaderText = "Fondo B"
        DataGridSnd1.GetColumnByDataField("FONDO_C").HeaderText = "Fondo C"
        DataGridSnd1.GetColumnByDataField("FONDO_D").HeaderText = "Fondo D"
        DataGridSnd1.GetColumnByDataField("FONDO_E").HeaderText = "Fondo E"
        DataGridSnd1.GetColumnByDataField("TOTAL").HeaderText = "Total"
    End Sub

    Private Sub CargarGrilla()
        Dim info As New InformeRecaudacion()
        Dim dataAFP As DataSet
        Try
            dataAFP = info.InformeResumen(Session("ID_ADM"), Me.fechaIniCaja.Text,
                                          Me.fechaFinCaja.Text,
                                          Me.fechaIniRecaudacion.Text,
                                          Me.fechaFinRecaudacion.Text)

            If dataAFP.Tables(0).Rows.Count = 0 Then
                mensaje("El proceso no encontro registros")
            End If

            DataGridSnd1.DataSet = dataAFP
            DataGridSnd1.DataSet.AcceptChanges()

            If Not IsNothing(DataGridSnd1.DataSet) AndAlso DataGridSnd1.DataSet.Tables(0).Rows.Count > 0 Then
                DataGridSnd1.CurrentPageIndex = 0
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

        log_.WriteLogInfo(nameMethod, "Inicia consulta informe recaudacion")

        If Me.rblTipo.SelectedValue = "1" Then
            log_.WriteLogInfo(nameMethod, "Carga de grilla iniciada")
            CargarGrilla()
        End If
        If Me.rblTipo.SelectedValue = "2" Then
            log_.WriteLogInfo(nameMethod, "")
        End If
        If Me.rblTipo.SelectedValue = "3" Then
            log_.WriteLogInfo(nameMethod, "")
        End If
        If Me.rblTipo.SelectedValue = "4" Then
            log_.WriteLogInfo(nameMethod, "")
        End If
        If Me.rblTipo.SelectedValue = "5" Then
            log_.WriteLogInfo(nameMethod, "")
        End If
        If Me.rblTipo.SelectedValue = "6" Then
            log_.WriteLogInfo(nameMethod, "")
        End If

    End Sub

    Private Sub btnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
        Dim rpt As Report
        Try
            rpt = New Report("IngresoEgreso\rptReporteRecaudacion.rpt", "Procesos de reporte de recaudacion")

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