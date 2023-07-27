Imports System.Web.Services
Imports Sonda.Net
Imports Sonda.Net.DB
Imports Sonda.Gestion.Adm.Sys.IngresoEgreso
<WebService(Namespace := "http://tempuri.org/")> _
Public Class wsINEParametros
    Inherits System.Web.Services.WebService

#Region " Web Services Designer Generated Code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Web Services Designer.
        InitializeComponent()

        'Add your own initialization code after the InitializeComponent() call

    End Sub

    'Required by the Web Services Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Web Services Designer
    'It can be modified using the Web Services Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
    End Sub

    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        'CODEGEN: This procedure is required by the Web Services Designer
        'Do not modify it using the code editor.
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

#End Region

    
   
    <WebMethod()> Public Function wmTraerFechaAcreditacion(ByVal idAdm As Integer) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()

            wmTraerFechaAcreditacion = Sys.Kernel.Parametros.FechaAcreditacion.traerFechaAcreditacion(dbc, idAdm, "ACR")

        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function
    <WebMethod()> Public Function wmObtenerFechaValorCuota(ByVal idAdm As Integer) As Date
        Dim dbc As OraConn
        Try
            dbc = New OraConn()

            wmObtenerFechaValorCuota = Sys.Kernel.Parametros.FechaAcreditacion.obtenerFechaValorCuota(dbc, idAdm, "ACR")

        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function
    <WebMethod()> Public Function wmtraerOrigenProceso(ByVal idAdm As Integer, ByVal codOrigenProceso As String) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            wmtraerOrigenProceso = ParametrosINE.OrigenProceso.traer(dbc, idAdm, codOrigenProceso)

            dbc.Commit()
        Catch e As SondaException
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try

    End Function
    <WebMethod()> Public Function wmBuscarOrigenProceso(ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal descripcion As String) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            wmBuscarOrigenProceso = ParametrosINE.OrigenProceso.buscar(dbc, idAdm, codOrigenProceso, descripcion)

            dbc.Commit()
        Catch e As SondaException
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function
    <WebMethod()> Public Function wmBuscarEstadoAcreditacion(ByVal idAdm As Integer, ByVal codEstadoAcreditacion As String, ByVal descripcion As String) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            wmBuscarEstadoAcreditacion = ParametrosINE.EstadoAcreditacion.buscar(dbc, idAdm, codEstadoAcreditacion, descripcion)

            dbc.Commit()
        Catch e As SondaException
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function
End Class
