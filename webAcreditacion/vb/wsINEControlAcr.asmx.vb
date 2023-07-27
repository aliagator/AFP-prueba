Option Explicit On 
Option Strict Off

Imports System.Web.Services
Imports Sonda.Net.DB
Imports Sonda.Gestion.Adm.Sys.CodeCompletion
Imports Sonda.Gestion.Adm.Sys.IngresoEgreso
Imports Sonda.Net
Imports System.IO
Imports System.Threading
Imports System.Configuration

<WebService(Namespace:="http://tempuri.org/")> _
Public Class wsINEControlAcr
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


    <WebMethod()> Public Function wmbuscarProceso(ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal numLote As Long, ByVal folioCaja As Long, ByVal Proposito As String) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            If numLote = 0 Then
                If Proposito = "A" Then 'acreditacion
                    wmbuscarProceso = ControlAcr.buscarProcesoAcreditacion(dbc, idAdm, codOrigenProceso, idUsuarioProceso)
                Else                    'simulacion
                    wmbuscarProceso = ControlAcr.buscarProcesoSimulacion(dbc, idAdm, codOrigenProceso, idUsuarioProceso)

                End If
            Else
                If Proposito = "A" Then 'acreditacion
                    wmbuscarProceso = ControlAcr.buscarLoteAcreditacion(dbc, idAdm, codOrigenProceso, folioCaja, numLote)
                Else                    'simulacion
                    wmbuscarProceso = ControlAcr.buscarLoteSimulacion(dbc, idAdm, codOrigenProceso, folioCaja, numLote)
                End If
            End If
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function
    <WebMethod()> Public Function wmbuscarProcesoCola(ByVal idAdm As Integer) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            wmbuscarProcesoCola = Sys.IngresoEgreso.ControlAcr.buscarProcesoEsperaCant(dbc, idAdm, 100, Now, 1)
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function
    <WebMethod()> Public Sub wmmodificarFechaCola(ByVal idAdm As Integer, ByVal fecUltModifReg As Date, ByVal numeroId As Integer)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()
            Sys.IngresoEgreso.ControlAcr.modFechaProceso(dbc, idAdm, fecUltModifReg, numeroId)
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
    <WebMethod()> Public Function wmbuscarLotesPendientes(ByVal idAdm As Integer, ByVal codOrigenProceso As String) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            wmbuscarLotesPendientes = ControlAcr.buscarLotesPendientes(dbc, idAdm, codOrigenProceso)

        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function
    <WebMethod()> Public Function wmbuscarProcesosVariosParametros(ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal numeroId As Integer, ByVal numRefOrigen1 As Long, ByVal numRefOrigen2 As Long, ByVal fecInicio As Object, ByVal fecFin As Object, ByVal estadoAcreditacion As String) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()

            wmbuscarProcesosVariosParametros = ControlAcr.buscarProcesoVariosParam(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, numRefOrigen1, numRefOrigen2, fecInicio, fecFin, estadoAcreditacion)


        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function
    <WebMethod()> Public Function wmbuscarLotesPorFecha(ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal fecDesde As Date, ByVal fecHasta As Date, ByVal estadoAcreditacion As String) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()

            wmbuscarLotesPorFecha = ControlAcr.buscarLotesPorFecha(dbc, idAdm, codOrigenProceso, fecDesde, fecHasta, estadoAcreditacion)


        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function
    <WebMethod()> Public Function wmbuscarCajasPorLote(ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal numLote As Long, ByVal estadoAcreditacion As String) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()

            wmbuscarCajasPorLote = ControlAcr.buscarCajasPorLote(dbc, idAdm, codOrigenProceso, numLote, estadoAcreditacion)


        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function

    <WebMethod()> Public Function wmbuscarRetirosPorLote(ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal numLote As Long, ByVal estadoAcreditacion As String) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            wmbuscarRetirosPorLote = ControlAcr.buscarRetirosPorLote(dbc, _
                                                                     idAdm, _
                                                                     codOrigenProceso, _
                                                                     numLote, _
                                                                     estadoAcreditacion)
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function

    <WebMethod()> Public Function wmObtenerCuotaAcreditacion(ByVal idAdm As Integer, ByVal TipoFondo As String) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            wmObtenerCuotaAcreditacion = INEControlAcr.obtenerCuotaAcreditacion(idAdm, TipoFondo)
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function
    <WebMethod()> Public Function wmBuscarDistintosUsuarios(ByVal idAdm As Integer, ByVal codOrigenProceso As String) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            wmBuscarDistintosUsuarios = ControlAcr.buscarDistintosUsuarios(dbc, idAdm, codOrigenProceso)
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function
    <WebMethod()> Public Sub wmProcesarAcreditacion(ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal numeroId As Integer, ByVal tipoProceso As String, ByVal usu As String, ByVal fun As String)
        Dim obj As New INEControlAcr()
        Try
            obj.ProcesarAcreditacion(idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, usu, fun)
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally

        End Try
    End Sub

    Public Function verificaEnvioAProcesar(ByVal idAdm As Integer, ByVal numeroId As Integer, ByVal tipoProceso As String) As String
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            verificaEnvioAProcesar = Transacciones.verificaEnvioAProcesar(dbc, idAdm, numeroId, tipoProceso)

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



    Public Sub verificaAcredEnLinea(ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal numeroId As Integer, ByRef enLinea As Boolean, ByVal estadoFinal As String)
        Try



        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally

        End Try
    End Sub

    <WebMethod()> Public Sub wmModEstadoProcAcre(ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal numeroId As Integer, ByVal estadoAcreditacion As String, ByVal usu As String, ByVal fun As String)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()
            ControlAcr.modEstadoProceso(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, estadoAcreditacion, usu, fun)
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
    End Sub
    <WebMethod()> Public Sub wmModEstadoProcAcreAnterior(ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal numeroId As Integer, ByVal estadoAnterior As String, ByVal estadoAcreditacion As String, ByVal usu As String, ByVal fun As String)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()
            ControlAcr.modEstadoProcesoAnt(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, estadoAnterior, estadoAcreditacion, usu, fun)
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
    End Sub

    <WebMethod()> Public Sub wmModEstadoRetirosPorLote(ByVal idAdm As Integer, _
                                                       ByVal CodAgenciaIng As Integer, _
                                                       ByVal numSolicitud As Integer, _
                                                       ByVal tipoSolicitud As String, _
                                                       ByVal usu As String, _
                                                       ByVal fun As String)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()
            ControlAcr.modEstadoRetirosporlote(dbc, _
                                               idAdm, _
                                               CodAgenciaIng, _
                                               numSolicitud, _
                                               tipoSolicitud, _
                                               usu, _
                                               fun)
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
    End Sub

    <WebMethod()> Public Sub wmAumentarPrioridad(ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal numeroId As Integer, ByVal usu As String, ByVal fun As String)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()
            ControlAcr.aumentarPrioridadProceso(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, usu, fun)
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
    End Sub
    <WebMethod()> Public Function wmverificaProcesoExistente(ByVal idAdm As Integer, ByVal numeroId As Integer) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()
            wmverificaProcesoExistente = ControlAcr.verificaProcesoExistente(dbc, idAdm, numeroId)
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
    <WebMethod()> Public Function wmtraerTransacciones(ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal usuarioProceso As String, ByVal numeroId As Integer, ByVal seqRegistro As Integer) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()

            wmtraerTransacciones = Sys.IngresoEgreso.Transacciones.traer(dbc, idAdm, codOrigenProceso, usuarioProceso, numeroId, seqRegistro)

        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function
    <WebMethod()> Public Sub wmMsjErr(ByVal idAdm As Integer, ByVal descripcion As String, ByVal usu As String, ByVal fun As String)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()
            Rezagos.msjerr(dbc, idAdm, descripcion, usu, fun)

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
    End Sub

    <WebMethod()> Public Sub crearUsuarioProceso(ByVal idAdm As Integer, ByVal tipoProceso As String, ByVal numeroId As Integer, ByVal idUsuarioProceso As String, ByVal usu As String, ByVal fun As String)
        Dim dbc As OraConn
        Dim ds As DataSet
        Try

            dbc = New OraConn()
            dbc.BeginTrans()

            ControlAcr.crearUsuarioProceso(dbc, idAdm, tipoProceso, numeroId, idUsuarioProceso, usu, fun)

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

    'CA-1460317 - 2018/11 - FBA - MOD - INI
    <WebMethod()> Public Function wmtraerTiposLotes(ByVal idAdm As Integer) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()

            wmtraerTiposLotes = ControlAcr.traerTiposLotes(dbc, idAdm)

        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function
    'CA-1460317 - 2018/11 - FBA - MOD - FIN

    'lfc: 20/05/2049 BXH - 1622722 verificar que la fecha de acreditacionsea igual a la de parametros
    Public Function verificaLoteValorizado(ByVal idAdm As Integer, ByVal numeroId As Long, ByVal fecAcreditacion As Object, ByVal codOrigenProceso As String, ByVal indAnulacion As String, ByVal usu As String, ByVal fun As String) As Boolean
        Dim dbc As OraConn
        Dim existeLote As Integer

        Try
            verificaLoteValorizado = False
            dbc = New OraConn()

            existeLote = ControlAcr.verificaLoteValorizado(dbc, idAdm, numeroId, fecAcreditacion, codOrigenProceso, indAnulacion, usu, fun)
            If existeLote > 0 Then verificaLoteValorizado = True

        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function


    '>>>>>> MVC - CA-5697663- 15/01/2020
    <WebMethod()> Public Function wmbuscarTransaccionesNocionales(ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal usuarioProceso As String, ByVal numeroId As Integer, ByVal seqRegistro As Integer) As Integer
        Dim dbc As OraConn
        Try
            dbc = New OraConn()

            wmbuscarTransaccionesNocionales = Sys.IngresoEgreso.Transacciones.cantidadTransNocionales(dbc, idAdm, codOrigenProceso, usuarioProceso, numeroId)

        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function

    '<<<<<< MVC - CA-5697663- 15/01/2020
End Class
