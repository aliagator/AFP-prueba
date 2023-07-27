Option Explicit On 
Option Strict Off

Imports System.Web.Services
Imports Sonda.Net.DB
Imports Sonda.Gestion.Adm.Sys.CodeCompletion
Imports Sonda.Gestion.Adm.Sys.IngresoEgreso
Imports Sonda.Net

<WebService(Namespace:="http://tempuri.org/")> _
Public Class wsINEInformacionCuentas
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

    <WebMethod()> Public Function wmbuscarAfiemp(ByVal idAdm As Integer, ByVal idCliente As Integer, ByVal idEmpleador As String, ByVal tipoProducto As String, ByVal perCotizaDesde As Date, ByVal perCotizaHasta As Date, ByVal tipoSolicitante As String) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            wmbuscarAfiemp = InformacionCuentas.buscarAfiEmp(dbc, idAdm, idCliente, idEmpleador, tipoProducto, perCotizaDesde, perCotizaHasta, tipoSolicitante)

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
    <WebMethod()> Public Function wmbuscarAjustemasivo(ByVal idAdm As Integer, ByVal idCliente As Integer, ByVal numPlanilla As Integer, ByVal perCotiza As Date, ByVal idEmpleador As String) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            wmbuscarAjustemasivo = InformacionCuentas.buscarAjusteMasivo(dbc, idAdm, idCliente, numPlanilla, perCotiza, idEmpleador)

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
    <WebMethod()> Public Function wmbuscarSaldos(ByVal idAdm As Integer, ByVal idCliente As Integer, ByVal numSaldo As Integer, ByVal seqMvto As Integer) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            wmbuscarSaldos = InformacionCuentas.buscarSaldo(dbc, idAdm, idCliente, numSaldo, seqMvto)

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
    <WebMethod()> Public Function wmtraerSaldos(ByVal idAdm As Integer, ByVal idCliente As Integer, ByVal numSaldo As Integer, ByVal seqMvto As Long) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            wmtraerSaldos = InformacionCuentas.traerSaldos(dbc, idAdm, idCliente, numSaldo, seqMvto)

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
    <WebMethod()> Public Function wmObtenerProductosVigentesTProd(ByVal idAdm As Integer, ByVal idCliente As Integer, ByVal tipoProducto As String) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            wmObtenerProductosVigentesTProd = InformacionCliente.buscarProdVigTprod(dbc, idAdm, idCliente, tipoProducto)

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
