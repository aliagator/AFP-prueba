Imports Sonda.Net
Imports Sonda.Gestion.Adm.Sys
Imports Sonda.Gestion.Adm.Sys.CodeCompletion
Imports sysIntegracionContable
Imports Microsoft.VisualBasic.Interaction
Imports System.Reflection
Imports Sonda.Net.Reports
Imports System.IO
Imports System.Data.OleDb
Imports wsIntegracionContable

Public MustInherit Class inePlanillaTesoreria
    Inherits Sonda.Net.Page.Body
    Protected _dpage As Page.DefaultPage
    Protected WithEvents Label1 As Sonda.Net.Control.Label
    Protected WithEvents DataGridSnd1 As Sonda.Net.Control.DataGridSnd
    Protected WithEvents lblTimbreCaja As Sonda.Net.Control.Label
    Protected WithEvents MsgBoxSnd1 As Sonda.Net.Control.MsgBoxSnd
    Protected WithEvents lblFecha As Sonda.Net.Control.Label
    Dim FechaMEC As New Sonda.Gestion.Adm.WS.Soporte.wsFecha()
    Protected WithEvents fileCargar As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnImportarArchivo As Sonda.Net.Control.Button


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

    Dim nombApp As String = "Carga Planilla Tesoreria"
    Dim wsSoporte As New Sonda.Gestion.Adm.WS.Soporte.wsARCArchivo()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _dpage = Page

        Try
            If Not Page.IsPostBack Then
                lblFecha.Text = Date.Now.ToString("dd/MM/yyyy")
            End If
        Catch ex As SondaException
            _dpage.SondaExceptionError(ex)
        Catch ex As System.Web.Services.Protocols.SoapException
            _dpage.SondaExceptionError(ex)
        Catch ex As Exception
            _dpage.SondaExceptionError(ex)
        End Try
    End Sub

    Private Sub DataGridSnd1_DataBinding(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataGridSnd1.DataBinding

        'DataGridSnd1.GetColumnByDataField("ID_ADM").Visible = False
        'DataGridSnd1.GetColumnByDataField("PATRIMONIO").HeaderText = "Banco Recaudacion"
        'DataGridSnd1.GetColumnByDataField("REG_REZAGOS").HeaderText = "Cuenta Corriente"
        'DataGridSnd1.GetColumnByDataField("FONDO_A").HeaderText = "Concepto"
        'DataGridSnd1.GetColumnByDataField("FONDO_B").HeaderText = "Total Rec"
    End Sub

    Private Sub btnImportarArchivo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportarArchivo.Click
        Dim mantTesoreria As New Tesoreria()
        Dim archivo, ext As String
        Dim numproceso As Long
        Dim dsRuta As New DataSet()
        Dim rutaServer As String = Nothing
        Dim dsDatosArchivo As DataSet
        Dim listBco As New List(Of BcoRecaudacion)
        Try
            If fileCargar.PostedFile.FileName = Nothing Then
                Me.MsgBoxSnd1.show("Debe especificar un archivo", "Carga Archivo", Net.Control.Util.TipoDlg.OkDlg)
                Exit Sub
            Else
                archivo = fileCargar.PostedFile.FileName
                ext = Path.GetExtension(archivo).ToUpper
            End If

            If ext <> ".XLS" And ext <> ".XLSX" Then
                MsgBoxSnd1.show("Archivo Incorrecto. Por favor seleccione un archivo .xls o .xlsx", "Carga Archivo", Net.Control.Util.TipoDlg.OkDlg)
                Exit Sub
            End If

            '**Grabar archivo en ruta servidor**
            archivo = Path.GetFileName(archivo)
            archivo = Path.GetFileNameWithoutExtension(archivo) + DateTime.Now.ToString("yyyyMMddHHmmss")
            archivo = archivo + ext

            rutaServer = "D:\File\PlanillaTesoreria"
            If Not rutaServer.EndsWith("\") Then rutaServer = rutaServer & "\"

            rutaServer = rutaServer & archivo 'c:\aaa\archivo.csv
            fileCargar.PostedFile.SaveAs(rutaServer)
            '**Fin Grabar archivo en ruta servidor**

            Dim strConn = System.Configuration.ConfigurationSettings.AppSettings("PROVIDER_EXCEL") & " data source= " & rutaServer & ";Extended properties=""Excel 12.0;hdr=yes;imex=1"""

            dsDatosArchivo = mantTesoreria.ImportarArchivoDataSet(rutaServer, strConn)

            DataGridSnd1.DataSet = dsDatosArchivo
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
        Me.MsgBoxSnd1.show(mensaje, "Planilla Tesoreria", Control.Util.TipoDlg.OkDlg, )
    End Sub

    Protected Sub btnAbrirPlanilla_Click(sender As Object, e As EventArgs)
        Dim log_ As New Log4Net
        Dim nameMethod As String = MethodBase.GetCurrentMethod().Name

        log_.WriteLogInfo(nameMethod, "Inicia carga planilla Tesoreria")
    End Sub

    Protected Sub btnGrabarPlanilla_Click(sender As Object, e As EventArgs)
        Dim mantTesoreria As New PlanillaTesoreria()
        Try

            Dim resp As Boolean = mantTesoreria.CargarPlanillaTesoreria(DataGridSnd1.DataSet, Session("ID_ADM"), lblFecha.Text)

            If resp Then
                MsgBoxSnd1.show("Archivo Procesado con Exito", "Carga Archivo", Net.Control.Util.TipoDlg.OkDlg)
            Else
                MsgBoxSnd1.show("Carga con errores", "Carga Archivo", Net.Control.Util.TipoDlg.OkDlg)
            End If
        Catch ex As SondaException
            _dpage.SondaExceptionError(ex)
        Catch ex As System.Web.Services.Protocols.SoapException
            _dpage.SondaExceptionError(ex)
        Catch ex As Exception
            _dpage.SondaExceptionError(ex)
        End Try
    End Sub

    Protected Sub btnInformeCargaPlanilla_Click(sender As Object, e As EventArgs)

    End Sub

    Private Function GetDataExcel(ByVal fileName As String, ByVal source As String) As DataSet
        If FUN = "MANUAL" Then
            Try

                Dim cnn As New OleDbConnection(
                                                    "Provider=Microsoft.Jet.OLEDB.4.0;" &
                                                    "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;';" &
                                                    "Data Source=" & fileName)
                Dim sql As String =
                 String.Format("SELECT * FROM [{0}]", source)
                Dim da As New OleDbDataAdapter(sql, cnn)
                Dim ds As New DataSet()
                da.Fill(ds)
                ds.GetXml()
                Return ds
            Catch ex As Exception
                Throw
            End Try
        Else
            Dim oConn As OleDb.OleDbConnection
            Try
                oConn = New OleDb.OleDbConnection()
                oConn.ConnectionString = System.Configuration.ConfigurationSettings.AppSettings("PROVIDER_EXCEL") & " data source= " & fileName & ";Extended properties=""Excel 12.0;hdr=yes;imex=1"""
                oConn.Open()
                Dim oCmd As New OleDb.OleDbCommand()
                oCmd.Connection = oConn
                oCmd.CommandType = CommandType.Text
                oCmd.CommandText = "SELECT * FROM [Hoja1$]"
                oCmd.ExecuteNonQuery()
                Dim oAdapter As New OleDb.OleDbDataAdapter(oCmd)
                Dim ds As New DataSet()
                oAdapter.Fill(ds)
                Return ds
            Catch ex As Exception
                Dim sm As New SondaExceptionManager(ex)
            Finally
                If Not IsNothing(oConn) Then oConn.Close()
            End Try
        End If
    End Function
End Class