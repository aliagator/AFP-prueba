Imports System.Web.Services
Imports Sonda.Gestion.Adm.Sys.CodeCompletion
Imports Sonda.Net
Imports Sonda.Net.DB
Imports Sonda.Gestion.Adm.Sys.IngresoEgreso
Imports Sonda.Gestion.Adm.Sys.Kernel
Imports Sonda.Gestion.Adm.Sys.Soporte
Imports Sonda.Gestion.Adm.WS

<WebService(Namespace:="http://tempuri.org/")> _
Public Class wsINEVector
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


    <WebMethod()> Public Function wmbuscar(ByVal idAdm As Integer, ByVal idCliente As Integer, ByVal tipoVector As String, ByVal tipoProducto As String, ByVal tipoFondo As String, ByVal seqMes As Object, ByVal codAdmOrigen As Object) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            'wmbuscar = Vector.buscar(dbc, idAdm, idCliente, tipoVector, tipoProducto, tipoFondo, seqMes, codAdmOrigen)

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
    <WebMethod()> Public Function wmbuscarPorEstado(ByVal idAdm As Integer, ByVal idCliente As Integer, ByVal tipoVector As String, ByVal tipoProducto As String, ByVal tipoFondo As String, ByVal seqMes As Integer, ByVal codAdmOrigen As Integer, ByVal estadoVector As String) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            'wmbuscarPorEstado = Vector.buscarPorEstado(dbc, idAdm, idCliente, tipoVector, tipoProducto, tipoFondo, seqMes, codAdmOrigen, estadoVector)

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

    <WebMethod()> Public Sub wmcrear(ByVal idAdm As Integer, ByVal idCliente As Integer, ByVal tipoVector As String, ByVal tipoProducto As String, ByVal tipoFondo As String, ByVal categoria As String, ByVal seqMes As Integer, ByVal idPersona As String, ByVal perCaja As Date, ByVal estadoVector As String, ByVal codAdmOrigen As Integer, ByVal indTipoOrigen As String, ByVal valMlAbo As Decimal, ByVal valMlCar As Decimal, ByVal valMlRen As Decimal, ByVal valMlTotal As Decimal, ByVal valCuoAbo As Decimal, ByVal valCuoCar As Decimal, ByVal valCuoRen As Decimal, ByVal valCuoTotal As Decimal, ByVal indLey As Integer, ByVal fecValCuo As Date, ByVal valMlValCuo As Decimal, ByVal usu As String, ByVal fun As String)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            'Vector.crear(dbc, idAdm, idCliente, tipoVector, tipoProducto, tipoFondo, seqMes, codAdmOrigen, categoria, perCaja, estadoVector, indTipoOrigen, valMlAbo, valMlCar, valMlRen, valMlTotal, valCuoAbo, valCuoCar, valCuoRen, valCuoTotal, indLey, fecValCuo, valMlValCuo, usu, fun)

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
    End Sub
    <WebMethod()> Public Sub wmeliminar(ByVal idAdm As Integer, ByVal idCliente As Integer, ByVal tipoVector As String, ByVal tipoProducto As String, ByVal tipoFondo As String, ByVal seqMes As Integer, ByVal codAdmOrigen As Integer)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            'Vector.eliminar(dbc, idAdm, idCliente, tipoVector, tipoProducto, tipoFondo, seqMes, codAdmOrigen)

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
    End Sub
    <WebMethod()> Public Sub wmincrementarSeqMes(ByVal idAdm As Integer, ByVal idCliente As Integer, ByVal tipoVector As String, ByVal tipoProducto As String, ByVal tipoFondo As String, ByVal seqMes As Integer, ByVal usu As String, ByVal fun As String)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            'Vector.incrementarSeqMes(dbc, idAdm, idCliente, tipoVector, tipoProducto, tipoFondo, seqMes, usu, fun)

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
    End Sub
    <WebMethod()> Public Sub wmmodEstado(ByVal idAdm As Integer, ByVal idCliente As Integer, ByVal tipoVector As String, ByVal tipoProducto As String, ByVal tipoFondo As String, ByVal seqMes As Integer, ByVal codAdmOrigen As Integer, ByVal estadoReg As String, ByVal usu As String, ByVal fun As String)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            'Vector.modEstado(dbc, idAdm, idCliente, tipoVector, tipoProducto, tipoFondo, seqMes, codAdmOrigen, estadoReg, usu, fun)

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
    End Sub
    <WebMethod()> Public Sub wmmodificar(ByVal idAdm As Integer, ByVal idCliente As Integer, ByVal tipoVector As String, ByVal tipoProducto As String, ByVal tipoFondo As String, ByVal categoria As String, ByVal seqMes As Integer, ByVal perCaja As Date, ByVal estadoVector As String, ByVal codAdmOrigen As Integer, ByVal indTipoOrigen As String, ByVal valMlAbo As Decimal, ByVal valMlCar As Decimal, ByVal valMlRen As Decimal, ByVal valMlTotal As Decimal, ByVal valCuoAbo As Decimal, ByVal valCuoCar As Decimal, ByVal valCuoRen As Decimal, ByVal valCuoTotal As Decimal, ByVal indLey As Integer, ByVal fecValCuo As Date, ByVal valMlValCuo As Decimal, ByVal usu As String, ByVal fun As String)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            'Vector.modificar(dbc, idAdm, idCliente, tipoVector, tipoProducto, tipoFondo, seqMes, codAdmOrigen, categoria, perCaja, estadoVector, indTipoOrigen, valMlAbo, valMlCar, valMlRen, valMlTotal, valCuoAbo, valCuoCar, valCuoRen, valCuoTotal, indLey, fecValCuo, valMlValCuo, usu, fun)

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
    End Sub
    <WebMethod()> Public Function wmobtenerTopePeriodo(ByVal idAdm As Integer, ByVal fechaCaja As Date) As Date
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            'wmobtenerTopePeriodo = Vector.obtenerTopePeriodo(dbc, idAdm, fechaCaja)

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

    <WebMethod()> Public Function wmtraer(ByVal idAdm As Integer, ByVal idCliente As Integer, ByVal tipoVector As String, ByVal tipoProducto As String, ByVal tipoFondo As String, ByVal seqMes As Integer, ByVal codAdmOrigen As Integer) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            'wmtraer = Vector.traer(dbc, idAdm, idCliente, tipoVector, tipoProducto, tipoFondo, seqMes, codAdmOrigen)

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


    <WebMethod()> Public Function wmProcesoBatch(ByVal idAdm As Integer, ByVal seqProceso As Integer, ByVal seqEtapa As Integer, ByVal ds As DataSet, ByVal usu As String, ByVal fun As String)
        Dim i As Integer
        Dim fecDesde, fechasta As Date
        Dim dbc As New OraConn()
        Try

            dbc.BeginTrans()

            fechasta = ds.Tables(0).Rows(0).Item("FEC_HASTA")
            Vector.generacionMasiva(dbc, idAdm, fechasta, seqProceso, seqEtapa, usu, fun)

            'If ds.Tables(0).Rows(0).Item("webMetodo") = "wmActualizacionMensual" Then

            '    Vector.actualizarVector(dbc, idAdm, ds.Tables(0).Rows(0).Item("perCaja"), seqProceso, seqEtapa, usu, fun)

            'ElseIf ds.Tables(0).Rows(0).Item("webMetodo") = "wmCalcularRentabilidad" Then

            '    Vector.rentabilidadVector(dbc, idAdm, ds.Tables(0).Rows(0).Item("fecValorCuota"), seqProceso, seqEtapa, usu, fun)

            'End If

            dbc.Commit()
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
            dbc.Rollback()
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
            dbc.Rollback()
        Finally
            dbc.Close()
        End Try

    End Function



End Class
