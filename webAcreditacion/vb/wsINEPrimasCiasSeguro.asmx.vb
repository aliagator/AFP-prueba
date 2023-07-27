Imports System.Web.Services
Imports Sonda.Net.DB
Imports Sonda.Gestion.Adm.Sys.CodeCompletion

Imports Sonda.Gestion.Adm.Sys.IngresoEgreso
Imports Sonda.Gestion.Adm.Sys.Soporte
Imports Sonda.Net

<WebService(Namespace:="http://tempuri.org/")> _
Public Class wsINEPrimasCiasSeguro
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
    '-----------------------------------------------------------------------
    ' Archivo generado automáticamente.
    ' Ruta Archivo  : C:\TEMP\sysIngresoEgreso.dll_705547
    ' Full Namespace: Sonda.Gestion.Adm.Sys.IngresoEgreso.PrimasCiasSeguro
    '
    ' Fecha: 15/12/2003 11:44:39
    ' Generador de código implementado por Luis Lillo Armijo.
    ' Versión del generador: 1.0.735 (Beta)
    '-----------------------------------------------------------------------


    <WebMethod()> Public Function wmbuscar(ByVal idAdm As Integer, ByVal tipoFondo As String, ByVal perProceso As Date, ByVal codInstFinanciera As Integer, ByVal idPersona As String, ByVal seqMovimiento As Integer, ByVal perCotiza As Date) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            wmbuscar = PrimasCiasSeguro.buscar(dbc, idAdm, tipoFondo, perProceso, codInstFinanciera, idPersona, seqMovimiento, perCotiza)

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
    <WebMethod()> Public Sub wmcrear(ByVal idAdm As Integer, ByVal tipoFondo As String, ByVal perProceso As Date, ByVal codInstFinanciera As Integer, ByVal idPersona As String, ByVal seqMovimiento As Integer, ByVal tipoImputacion As String, ByVal perCotiza As Date, ByVal tipoTrabajador As String, ByVal indDerechoSeguro As String, ByVal codOrigenProceso As String, ByVal fecOperacion As Date, ByVal tipoPago As String, ByVal valMlCco As Decimal, ByVal valCuoCco As Decimal, ByVal valMlComisionFija As Decimal, ByVal valMlRentaImponible As Decimal, ByVal valMlAdicional As Decimal, ByVal valMlAdicionalInteres As Decimal, ByVal valMlAdicionalReajuste As Decimal, ByVal valMlPrimaSeguro As Decimal, ByVal idAdmCobroAdicional As Integer, ByVal codMvto As String, ByVal porcPrimaSeguro As Decimal, ByVal porcAdicional As Decimal, ByVal idEmpleador As String, ByVal usu As String, ByVal fun As String, ByVal sexo As String, ByVal fecAcreditacion As Object, ByVal valMlPrimaInteres As Decimal, ByVal valMlPrimaReajuste As Decimal, ByVal valCuoPrimaSeguro As Decimal, ByVal valCuoPrimaInteres As Decimal, ByVal valCuoPrimaReajuste As Decimal)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            PrimasCiasSeguro.crear(dbc, idAdm, tipoFondo, perProceso, codInstFinanciera, idPersona, seqMovimiento, tipoImputacion, perCotiza, tipoTrabajador, indDerechoSeguro, codOrigenProceso, fecOperacion, tipoPago, valMlCco, valCuoCco, valMlComisionFija, valMlRentaImponible, valMlAdicional, valMlAdicionalInteres, valMlAdicionalReajuste, valMlPrimaSeguro, idAdmCobroAdicional, codMvto, porcPrimaSeguro, porcAdicional, idEmpleador, usu, fun, sexo, fecAcreditacion, valMlPrimaInteres, valMlPrimaReajuste, valCuoPrimaSeguro, valCuoPrimaInteres, valCuoPrimaReajuste)

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
    <WebMethod()> Public Sub wmeliminar(ByVal idAdm As Integer, ByVal tipoFondo As String, ByVal perProceso As Date, ByVal codInstFinanciera As Integer, ByVal idPersona As String, ByVal seqMovimiento As Integer, ByVal perCotiza As Date)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            PrimasCiasSeguro.eliminar(dbc, idAdm, tipoFondo, perProceso, codInstFinanciera, idPersona, seqMovimiento, perCotiza)

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
    <WebMethod()> Public Sub wmmodEstado(ByVal idAdm As Integer, ByVal tipoFondo As String, ByVal perProceso As Date, ByVal codInstFinanciera As Integer, ByVal idPersona As String, ByVal seqMovimiento As Integer, ByVal perCotiza As Date, ByVal estadoReg As String, ByVal usu As String, ByVal fun As String)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            PrimasCiasSeguro.modEstado(dbc, idAdm, tipoFondo, perProceso, codInstFinanciera, idPersona, seqMovimiento, perCotiza, estadoReg, usu, fun)

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
    <WebMethod()> Public Sub wmmodificar(ByVal idAdm As Integer, ByVal tipoFondo As String, ByVal perProceso As Date, ByVal codInstFinanciera As Integer, ByVal idPersona As String, ByVal seqMovimiento As Integer, ByVal tipoImputacion As String, ByVal perCotiza As Date, ByVal tipoTrabajador As String, ByVal indDerechoSeguro As String, ByVal codOrigenProceso As String, ByVal fecOperacion As Date, ByVal tipoPago As String, ByVal valMlCco As Decimal, ByVal valCuoCco As Decimal, ByVal valMlComisionFija As Decimal, ByVal valMlRentaImponible As Decimal, ByVal valMlAdicional As Decimal, ByVal valMlAdicionalInteres As Decimal, ByVal valMlAdicionalReajuste As Decimal, ByVal valMlPrimaSeguro As Decimal, ByVal idAdmCobroAdicional As Integer, ByVal codMvto As String, ByVal porcPrimaSeguro As Decimal, ByVal porcAdicional As Decimal, ByVal idEmpleador As String, ByVal usu As String, ByVal fun As String, ByVal sexo As String, ByVal fecAcreditacion As Object, ByVal valMlPrimaInteres As Decimal, ByVal valMlPrimaReajuste As Decimal, ByVal valCuoPrimaSeguro As Decimal, ByVal valCuoPrimaInteres As Decimal, ByVal valCuoPrimaReajuste As Decimal)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            PrimasCiasSeguro.modificar(dbc, idAdm, tipoFondo, perProceso, codInstFinanciera, idPersona, seqMovimiento, tipoImputacion, perCotiza, tipoTrabajador, indDerechoSeguro, codOrigenProceso, fecOperacion, tipoPago, valMlCco, valCuoCco, valMlComisionFija, valMlRentaImponible, valMlAdicional, valMlAdicionalInteres, valMlAdicionalReajuste, valMlPrimaSeguro, idAdmCobroAdicional, codMvto, porcPrimaSeguro, porcAdicional, idEmpleador, usu, fun, sexo, fecAcreditacion, valMlPrimaInteres, valMlPrimaReajuste, valCuoPrimaSeguro, valCuoPrimaInteres, valCuoPrimaReajuste)

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
    <WebMethod()> Public Function wmobtenerAdmDestino(ByVal idAdm As Integer, ByVal idCliente As Integer, ByVal perCotiza As Date) As Integer
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            wmobtenerAdmDestino = PrimasCiasSeguro.obtenerAdmDestino(dbc, idAdm, idCliente, perCotiza)

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
    <WebMethod()> Public Function wmobtenerAdmOrigen(ByVal idAdm As Integer, ByVal idPersona As String, ByVal perCotiza As Date) As Integer
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            wmobtenerAdmOrigen = PrimasCiasSeguro.obtenerAdmOrigen(dbc, idAdm, idPersona, perCotiza)

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
    <WebMethod()> Public Function wmtraer(ByVal idAdm As Integer, ByVal tipoFondo As String, ByVal perProceso As Date, ByVal codInstFinanciera As Integer, ByVal idPersona As String, ByVal seqMovimiento As Integer, ByVal perCotiza As Date) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            wmtraer = PrimasCiasSeguro.traer(dbc, idAdm, tipoFondo, perProceso, codInstFinanciera, idPersona, seqMovimiento, perCotiza)

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
    <WebMethod()> Public Function wmtraerUltimoValor(ByVal idAdm As Integer, ByVal idAdmSeguro As Integer, ByVal perSeguro As Date, ByVal tipoProducto As String, ByVal tipoCliente As String, ByVal tipoSeguro As String, ByVal sexo As String) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            wmtraerUltimoValor = PrimasCiasSeguro.traerUltimoValor(dbc, idAdm, idAdmSeguro, perSeguro, tipoProducto, tipoCliente, tipoSeguro, sexo)

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

    <WebMethod()> Public Function wmBuscarPorRut(ByVal idAdm As Integer, ByVal idPersona As String) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            'dbc.BeginTrans()

            wmBuscarPorRut = PrimasCiasSeguro.buscarPorRut(dbc, idAdm, idPersona)


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

    'lfc://16/10 -cargo prima CAF
    <WebMethod()> Public Function wmCargoPrimaSisCaf(ByVal idAdm As Integer, ByVal perProceso As Date, ByVal codAdm As Integer, ByVal tipoProceso As String, ByVal seqProceso As Integer, ByVal seqEtapa As Integer, ByVal usu As String, ByVal fun As String) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            wmCargoPrimaSisCaf = PrimasCiasSeguro.cargoPrimaSisCaf(dbc, idAdm, perProceso, codAdm, tipoProceso, seqProceso, seqEtapa, usu, fun)

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
        Dim log As New Procesos.logEtapa()
        Dim dbc As New OraConn()
        Dim perProceso As Date
        Dim codAdm As Integer
        Dim dsR As New DataSet()
        Try

            'log = New Procesos.logEtapa(idAdm, seqProceso, seqEtapa)
            'evento(log, "Inicio de proceso")
            'log.FecHoraInicio = Now

            Select Case ds.Tables(0).Rows(0).Item("ID_ETAPA")
                Case "ETAPA1"
                    'evento(log, "Se inicia la etapa " & ds.Tables(0).Rows(0).Item("ID_ETAPA"))
                    'evento(log, "")
                    'evento(log, "Recatando Parámetros")
                    perProceso = ds.Tables(0).Rows(0).Item("FEC_PROCESO")
                    codAdm = ds.Tables(0).Rows(0).Item("COD_ADM")
                    'evento(log, "Fecha Proceso: " & perProceso.ToShortDateString)
                    'evento(log, "")


                    dbc.BeginTrans()
                    dsR = PrimasCiasSeguro.cargoPrimaSisCaf(dbc, idAdm, perProceso, codAdm, "BATCH", seqProceso, seqEtapa, usu, fun)


                    If dsR.Tables(0).Rows.Count = 0 Then

                        log = New Procesos.logEtapa(idAdm, seqProceso, seqEtapa)
                        log.estado = Procesos.Estado.ConError
                        evento(log, "Error en el Proceso")
                        log.fecHoraFin = Now
                        log.Save()
                        Throw New Exception("Error en el proceso")
                    End If

                    Dim numeroId, gcodError As Integer

                    gcodError = dsR.Tables(0).Rows(0).Item("COD_ERROR")
                    numeroId = dsR.Tables(0).Rows(0).Item("NUMERO_ID")

                    If gcodError = 15307 Then


                    End If

                    If gcodError <> 0 Then Throw New SondaException(gcodError)

                    ' evento(log, "Se crea lote con numero id: " & numeroId.ToString)

                    'log.estado = Procesos.Estado.Exitoso
                    'evento(log, "finalizacion exitosa del proceso")
                    'log.fecHoraFin = Now
                    'log.Save()

                    dbc.Commit()


                Case Else
                    log = New Procesos.logEtapa(idAdm, seqProceso, seqEtapa)
                    log.estado = Procesos.Estado.Exitoso
                    evento(log, "No hay etapa para ejecutar")
                    evento(log, "finalizacion exitosa del proceso")
                    log.fecHoraFin = Now
                    log.Save()
            End Select

           


        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try

    End Function

    Private Shared Sub evento(ByVal LOG As Procesos.logEtapa, ByVal mensaje As String)
        If Not IsNothing(LOG) Then
            LOG.AddEvento(mensaje)
            LOG.Save()
        End If
    End Sub


    'OS- 10340031 - FBA – 01/2018– INI 
    <WebMethod()> Public Function traeTiposInformesAuditor(ByVal idAdm As Integer) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            traeTiposInformesAuditor = PrimasCiasSeguro.traeTiposInformesAuditor(dbc, idAdm)

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
    'OS- 10340031 - FBA – 01/2018 – FIN


End Class
