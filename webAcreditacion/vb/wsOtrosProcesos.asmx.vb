Option Strict Off

Imports Sonda.Net.Trace
Imports System.Web.Services
Imports Sonda.Net.DB
Imports Sonda.Gestion.Adm.Sys.CodeCompletion
Imports Sonda.Gestion.Adm.Sys.IngresoEgreso
Imports Sonda.Net
Imports System.IO
Imports Sonda.Gestion.Adm.Sys.Soporte
Imports System.Threading

<WebService(Namespace:="http://tempuri.org/")> _
Public Class wsOtrosProcesos
    Inherits System.Web.Services.WebService
    Public gIdAdm As Integer
    Public gCodOrigenProceso As String
    Public gIdUsuarioProceso As String
    Public gNumeroId As Integer
    Public gTipoProceso As String
    Public gfuncion As String

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

    <WebMethod()> Public Sub cargaRptAcreditacion(ByVal idadm As Integer, ByVal numeroId As Integer, ByVal codOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal fecInicio As Object, ByVal fecFin As Object, ByVal estadoProceso As String)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            InformacionCuentas.reportes.cargarRptAcreditacion(dbc, idadm, codOrigenProceso, idUsuarioProceso, fecInicio, fecFin, numeroId, estadoProceso)
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Sub
    <WebMethod()> Public Sub cargaRptAcredAcum(ByVal idadm As Integer, ByVal numeroId As Integer, ByVal codOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal fecInicio As Object, ByVal fecFin As Object, ByVal estadoProceso As String, ByVal SeqInforme As Integer)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            InformacionCuentas.reportes.cargarRptAcredAcum(dbc, idadm, codOrigenProceso, idUsuarioProceso, fecInicio, fecFin, numeroId, estadoProceso, SeqInforme)
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Sub
    <WebMethod()> Public Function wmSeleccionProcesosRptAcred(ByVal idadm As Integer, ByVal numeroId As Integer, ByVal codOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal fecInicio As Object, ByVal fecFin As Object, ByVal lote As Long, ByVal caja As Long, ByVal estadoProceso As String) As Integer
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()
            wmSeleccionProcesosRptAcred = InformacionCuentas.reportes.seleccionProcesos(dbc, idadm, codOrigenProceso, idUsuarioProceso, fecInicio, fecFin, numeroId, lote, caja, estadoProceso)
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
    <WebMethod()> Public Sub wmEliminarSeleccionReportes(ByVal idadm As Integer, ByVal seqInforme As Integer)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()
            InformacionCuentas.reportes.eliminarTmpReportes(dbc, idadm, seqInforme)
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
    <WebMethod()> Public Function wmTraerSecuenciaRpt(ByVal idAdm As Integer) As Integer
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            wmTraerSecuenciaRpt = InformacionCuentas.reportes.traerSeqReporte(dbc, idAdm)
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function
    <WebMethod()> Public Sub wmCrearTmpReportes(ByVal idAdm As Integer, ByVal seqReporte As Integer, ByVal numeroId As Integer, ByVal USU As String, ByVal fun As String)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            InformacionCuentas.reportes.crearTmpReportes(dbc, idAdm, seqReporte, numeroId, USU, fun)
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Sub
    <WebMethod()> Public Function wmGetAppSetting(ByVal key As String) As String

        Try

            Return System.Configuration.ConfigurationSettings.AppSettings.Get(key)

        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)


        End Try
    End Function
    <WebMethod()> Public Function AcreditarProceso(ByVal idAdm As Integer, ByVal numeroId As Integer, ByVal tipoProceso As String) As String

        Dim dbc As OraConn
        Dim ds As DataSet
        Dim gfecValorCuota As Date

        Try

            AcreditarProceso = "OK"
            dbc = New OraConn()
            Dim ccAcr As ACR.ccControlAcreditacion
            gfecValorCuota = Sys.Kernel.Parametros.FechaAcreditacion.obtenerFechaValorCuota(dbc, 1, "ACR")

            Dim a As New WS.IngresoEgreso.INEProcesosAcr2()
            dbc.BeginTrans()

            ds = Sys.IngresoEgreso.ControlAcr.buscarProceso(dbc, idAdm, Nothing, Nothing, numeroId)
            If ds.Tables(0).Rows.Count > 0 Then
                ccAcr = New ACR.ccControlAcreditacion(ds.Tables(0).Rows(0))
                INEControlAcr.ProcesarAcreditacion(idAdm, ccAcr.codOrigenProceso, ccAcr.idUsuarioProceso, numeroId, tipoProceso, ccAcr.idUsuarioProceso, ccAcr.idUsuarioIngReg)
            End If

            dbc.Close()
            AcreditarProceso = "ok"

        Catch e As Exception
            AcreditarProceso = e.ToString

        End Try


        '''''''''Dim dbc As OraConn
        '''''''''Dim ds As DataSet
        '''''''''Dim gfecValorCuota As Date

        '''''''''Try

        '''''''''    AcreditarProceso = "OK"
        '''''''''    dbc = New OraConn()
        '''''''''    Dim ccAcr As ACR.ccControlAcreditacion

        '''''''''    'gfecValorCuota = Sys.Kernel.Parametros.FechaAcreditacion.obtenerFechaValorCuota(dbc, 1, "ACR")

        '''''''''    'Dim a As New WS.IngresoEgreso.INEProcesosAcr2()
        '''''''''    'ds = Transacciones.traer(dbc, 1, "RECAUDAC", "MCASTRO", 16555, 6112079)
        '''''''''    'a.rTrn = New ACR.ccTransacciones(ds)

        '''''''''    'a.CalcularNominales(20000, 0, 0, 0, 0, 0, 0)

        '''''''''    '----Acreditador
        '''''''''    '    ds = Sys.IngresoEgreso.ControlAcr.buscarProceso(dbc, idAdm, Nothing, Nothing, numeroId)
        '''''''''    '   If ds.Tables(0).Rows.Count > 0 Then
        '''''''''    '      ccAcr = New ACR.ccControlAcreditacion(ds.Tables(0).Rows(0))
        '''''''''    '     INEControlAcr.ProcesarAcreditacion(idAdm, ccAcr.codOrigenProceso, ccAcr.idUsuarioProceso, numeroId, tipoProceso, ccAcr.idUsuarioProceso, ccAcr.idUsuarioIngReg)
        '''''''''    'End If
        '''''''''    '-----fin acreditador

        '''''''''    '---copia archivo



        '''''''''    Dim indActualizacion As String = "N"
        '''''''''    'Dim dsArchivo As DataSet = Sys.IngresoEgreso.CambioFondo.cabecera.auditorMultifondos(dbc, idAdm, indActualizacion, seqProceso, seqEtapa)


        '''''''''    Dim utilCopiar As New FTPUtil.Copiador()
        '''''''''    Dim strFTPUtil As String = System.Configuration.ConfigurationSettings.AppSettings("STRING_FTP")

        '''''''''    Dim ip As String = traerString(strFTPUtil, ";", "=")
        '''''''''    Dim usuario As String = traerString(strFTPUtil, ";", "=")
        '''''''''    Dim password As String = traerString(strFTPUtil, ";", "=")
        '''''''''    Dim port As String = traerString(strFTPUtil, ";", "=")

        '''''''''    Dim pathOrigen As String = traerString(strFTPUtil, Nothing, "=")
        '''''''''    Dim pathDest As String = Sys.Soporte.Archivo.traer(dbc, idAdm, "ACRAUDMF").Tables(0).Rows(0).Item("RUTA") & "\"


        '''''''''    Dim errMsg As String

        '''''''''    If Not utilCopiar.Traer(ip, usuario, password, pathOrigen, port, "RegularizacionMultifondo_20080723.csv", pathDest, errMsg) Then
        '''''''''        AcreditarProceso = errMsg
        '''''''''    End If


        '''''''''    dbc.Close()



        '''''''''Catch e As Exception
        '''''''''    AcreditarProceso = e.ToString

        '''''''''End Try

    End Function

    Private Class nueva

        Private Sub escribirLogEnl(ByVal mensaje As String)
            Dim fechaStr As String = Now.Year.ToString.PadLeft(4, "0") & _
                                Now.Month.ToString.PadLeft(2, "0") & _
                                Now.Day.ToString.PadLeft(2, "0")

            Dim codArchivo As String = "LogSrvAcred"
            Dim ruta As String = Configuracion.pathControl
            If Not ruta.EndsWith("\") Then ruta &= "\"
            ruta = ruta & "LogSrvAcredENL_" & fechaStr & ".txt"

            Dim esc As New StreamWriter(ruta, True, System.Text.Encoding.Default)
            esc.WriteLine(fechaStr & " - " & mensaje)
            esc.Flush()
            esc.Close()
        End Sub

    End Class



    Private Function obtenerRutaArchivo() As String

        Dim codArchivo As String = "LogSrvAcred"
        Dim ruta As String
        Dim hoy As Date

        hoy = Now.Date
        'ruta = Archivo.traer(dbc, idadm, codArchivo).Tables(0).Rows(0).Item("RUTA")
        ruta = Configuracion.pathControl
        ruta &= "\" & codArchivo & "_"
        ruta &= hoy.Year.ToString.PadLeft(4, "0")
        ruta &= hoy.Month.ToString.PadLeft(2, "0")
        ruta &= hoy.Day.ToString.PadLeft(2, "0")
        ruta &= ".txt"
        obtenerRutaArchivo = ruta
    End Function



    Private Shared Sub agregarEvento(ByVal evento As String, ByVal ruta As String)
        Dim numArchivo As String
        numArchivo = AbrirArchivo(ruta)
        Print(numArchivo, Now & " - " & evento)
        Print(numArchivo, Chr(13) + Chr(10))
        FileClose(numArchivo)
    End Sub


    Private Shared Function AbrirArchivo(ByVal ruta As String) As Integer
        Dim bNoExiste As Boolean = True
        Dim numArchivo As Integer
        Dim version As String = "1.0.5"

        numArchivo = 1

        bNoExiste = IsNothing(Dir(ruta, FileAttribute.Archive))   ' = ""
        If bNoExiste Then
            FileOpen(numArchivo, ruta, OpenMode.Output, OpenAccess.Write)
            Print(numArchivo, Now & " - " & "El servicio para el dia " & Now.Date & " se ha establecido (version " & version & ")")
            Print(numArchivo, Chr(13) + Chr(10))

            Print(numArchivo, Now & " - " & "Identificación del acreditador : " & Configuracion.idAcreditador)
            Print(numArchivo, Chr(13) + Chr(10))

            If Configuracion.NroHilosAcreditacion = 1 Then
                Print(numArchivo, Now & " - " & "La acreditación paralela esta deshabilitada.")
            Else
                Print(numArchivo, Now & " - " & "La acreditación paralela esta habilitada con un máximo de " & Configuracion.NroHilosAcreditacion & " hilos.")
            End If
            Print(numArchivo, Chr(13) + Chr(10))


        Else
            FileOpen(numArchivo, ruta, OpenMode.Append, OpenAccess.ReadWrite)
        End If

        AbrirArchivo = numArchivo

    End Function
    Private Sub acreditar()

        INEControlAcr.ProcesarAcreditacion(gIdAdm, gCodOrigenProceso, gIdUsuarioProceso, gNumeroId, gTipoProceso, gIdUsuarioProceso, gfuncion)

    End Sub

    Public Class clsAcreditador

        Public gIdAdm As Integer
        Public gCodOrigenProceso As String
        Public gIdUsuarioProceso As String
        Public gNumeroId As Integer
        Public gTipoProceso As String
        Public gfuncion As String
        Public gRuta As String
        Public gseqProceso As Integer

        Public finalizado As Boolean = False

        Public Sub acreditar()
            'Dim hilo As Threading.Thread
            Try
                INEControlAcr.ProcesarAcreditacionBatch(gIdAdm, gCodOrigenProceso, gIdUsuarioProceso, gNumeroId, gTipoProceso, gIdUsuarioProceso, gfuncion)
                agregarEvento("     El proceso " & gNumeroId & " del origen " & gCodOrigenProceso & " ha finalizado.", gRuta)

            Catch ex As Exception
                agregarEvento("     El proceso " & gNumeroId & " del origen " & gCodOrigenProceso & " ha finalizado con error. " & ex.ToString, gRuta)
            Finally
                finalizado = True
            End Try
        End Sub
        Public Sub generarTotales()
            Dim dbc As New OraConn()
            Try
                gseqProceso = IIf(gTipoProceso = "AC", 1, 0)
                ResultadoAcred.TotalesAcred.sumarTotales(dbc, gIdAdm, gCodOrigenProceso, gIdUsuarioProceso, gNumeroId, gseqProceso, gIdUsuarioProceso, gfuncion)

                agregarEvento("     El proceso " & gNumeroId & " del origen " & gCodOrigenProceso & " tiene su informe de totales finalizado", gRuta)
            Catch ex As Exception
                agregarEvento("     El proceso " & gNumeroId & " del origen " & gCodOrigenProceso & " ha finalizado con error en su informe de totales. " & ex.ToString, gRuta)
            Finally
                finalizado = True
            End Try
        End Sub

    End Class

    <WebMethod()> Public Sub wmAcreditar(ByVal idadm As Integer)
        Dim dbc As OraConn
        Dim dsControl As DataSet
        Dim ds As DataSet
        Dim ccControl As ACR.ccControlAcreditacion
        Dim rCont As PAR.ccAcrControl
        Dim i As Integer
        Dim fun As String = "SrvAcreditacion"
        Dim tipoProceso, accion, ruta As String

        Dim hilo As Threading.Thread
        Dim hilos As New ArrayList()
        Dim datos As New Hashtable()

        Dim salir = False

        Dim hilosEnEjecucion As Integer = 0
        Dim horaRevision As Date

        Try

            ruta = obtenerRutaArchivo()
            'ruta = "C:\LOG_PROCESO.TXT"

            dbc = New OraConn()
            horaRevision = Sys.Soporte.Fecha.ahora(dbc)

            ds = ParametrosINE.Control.traer(dbc, idadm)
            If ds.Tables(0).Rows.Count = 0 Then
                Throw New SondaException(15013) 'No se ha en contrado el máximo de regitros permitido para la 
            End If

            rCont = New PAR.ccAcrControl(ds)

            If rCont.numMaxAcrTotal <= 0 Then
                If rCont.simulacionOffLine = "S" And rCont.numMaxAcrTotal <= 0 Then
                    'agregarEvento("El Acreditador se encuentra fuera de servicio (Simulación Permitida)", ruta)
                Else
                    agregarEvento("El Acreditador se encuentra fuera de servicio", ruta)
                    Exit Try
                End If
            End If

            Do
                hilosEnEjecucion = obtenerNroHilosEnEjecucion(hilos, datos, ruta)

                If hilosEnEjecucion >= Configuracion.NroHilosAcreditacion Then
                    System.Threading.Thread.Sleep(1000) '\ Espera 1 segundo antes de reiniciar el ciclo
                Else

                    Dim cant = Configuracion.NroHilosAcreditacion - hilosEnEjecucion

                    If rCont.simulacionOffLine = "S" And rCont.numMaxAcrTotal <= 0 Then
                        dsControl = traerProcesosEnEsperaOffLine(dbc, idadm, cant, horaRevision, fun)
                    Else
                        dsControl = traerProcesosEnEspera(dbc, idadm, cant, horaRevision, fun)
                    End If


                    If dsControl.Tables(0).Rows.Count = 0 Then '\ Se acabaron para la hora de revision, cambiamos
                        horaRevision = Sys.Soporte.Fecha.ahora(dbc) '\ la hora y revisamos si han llegado mas

                        If rCont.simulacionOffLine = "S" And rCont.numMaxAcrTotal <= 0 Then
                            dsControl = traerProcesosEnEsperaOffLine(dbc, idadm, cant, horaRevision, fun)
                        Else
                            dsControl = traerProcesosEnEspera(dbc, idadm, cant, horaRevision, fun)
                        End If
                    End If

                    If dsControl.Tables(0).Rows.Count = 0 Then
                        If hilosEnEjecucion = 0 Then

                            If rCont.simulacionOffLine = "S" And rCont.numMaxAcrTotal <= 0 Then
                                agregarEvento("El Acreditador se encuentra fuera de servicio (Simulación Permitida)", ruta)
                                Exit Do
                            Else
                                agregarEvento("No existen procesos en la cola y no quedan procesos en ejecución.", ruta)
                                Exit Do
                            End If

                        Else
                            '\\ no hay pendientes por acreditar pero quedan en ejecucion, espera 1 segundo
                            System.Threading.Thread.Sleep(1000) '\ Espera 1 segundo antes de reiniciar el ciclo
                        End If
                    Else
                        For i = 0 To dsControl.Tables(0).Rows.Count - 1

                            'TODO: \\ comprobar si se puede acreditar
                            '\\ sino grabar en el log

                            ccControl = New ACR.ccControlAcreditacion(dsControl.Tables(0).Rows(i))

                            'Dim key As String '\\ llave unica del proceso de acreditacion
                            'key = idadm.ToString & "-" & ccControl.codOrigenProceso.ToString & "-" & ccControl.idUsuarioProceso.ToString & "-" & ccControl.numeroId.ToString
                            'If procesados.IndexOf(key) < 0 Then

                            tipoProceso = IIf(ccControl.estadoProceso = "ES", "SIM", "ACR")
                            accion = IIf(ccControl.estadoProceso = "ES", "Simular", "Acreditar")
                            agregarEvento("El proceso " & ccControl.numeroId & " del origen " & ccControl.codOrigenProceso & " ha sido enviado a " & accion & " " & ccControl.totRegistrosCreados & " registros", ruta)

                            Dim aux As New clsAcreditador()
                            aux.gIdAdm = idadm
                            aux.gCodOrigenProceso = ccControl.codOrigenProceso
                            aux.gIdUsuarioProceso = ccControl.idUsuarioProceso
                            aux.gNumeroId = ccControl.numeroId
                            aux.gTipoProceso = tipoProceso
                            aux.gfuncion = fun
                            aux.gRuta = ruta

                            If Configuracion.NroHilosAcreditacion = 1 Then
                                aux.acreditar()
                                '\\se agregó estas lineas para ver si el proceso de acreditación se independiza de la generacion de su informe
                                'If aux.gCodOrigenProceso = "RECAUDAC" Then
                                '    agregarEvento("     El proceso " & aux.gNumeroId & " del origen " & gCodOrigenProceso & " va a generar sus totales", ruta)
                                '    hilo = New Thread(AddressOf aux.generarTotales)
                                '    hilos.Add(hilo)
                                '    datos.Add(hilo, aux)
                                '    hilo.Start()

                                '    hilosEnEjecucion = hilosEnEjecucion + 1
                                'End If
                                '\\se agregó estas lineas..
                            Else
                                hilo = New Thread(AddressOf aux.acreditar)
                                hilos.Add(hilo)
                                datos.Add(hilo, aux)
                                hilo.Start()

                                hilosEnEjecucion = hilosEnEjecucion + 1
                                'procesados.Add(key) '\\ agrega este proceso de acreditacion a la cola de procesados 
                                '\\ para evitar reprocesarlo en esta instancia del acreditador
                                '\\ en caso de que quedara pendiente por alguna cusa
                            End If

                            'End If
                            '\\ If hilosEnEjecucion = Configuracion.NroHilosAcreditacion Then Exit For
                        Next
                    End If
                End If
            Loop '\\ para consultar nuevamente la base hasta que no hayan nuevos que procesar

            'agregarEvento("Fin revisión de la cola de procesos de acreditación", ruta)

        Catch e As Exception
            Try
                agregarEvento("ERROR: " & e.ToString, ruta)
            Catch e2 As Exception
                Throw New Exception("No se pudo agregar evento en " & ruta & " Cuando se produjo el siguiente error: " & e.ToString)
            End Try
        Finally
            If Not IsNothing(dbc) Then dbc.Close()
        End Try

    End Sub

    Public Function obtenerNroHilosEnEjecucion(ByVal hilos As ArrayList, ByVal datos As Hashtable, ByVal ruta As String) As Integer
        Dim hilo As Thread
        Dim hilosEnEjecucion As Integer = 0

        If Configuracion.NroHilosAcreditacion > 1 Then
            '\\ solo es necesario si esta multihilo
            Dim hilosFinalizados As ArrayList = New ArrayList()

            For Each hilo In hilos
                If datos.Contains(hilo) Then
                    Dim aux As clsAcreditador = datos(hilo)
                    If aux.finalizado = True Then
                        hilosFinalizados.Add(hilo)
                    Else
                        hilosEnEjecucion = hilosEnEjecucion + 1
                    End If
                End If
            Next

            For Each hilo In hilosFinalizados
                '\\ Remueve los hilos de la coleccion
                If hilos.Contains(hilo) Then hilos.Remove(hilo)
            Next
            hilosFinalizados.Clear()
            Return hilosEnEjecucion
        Else
            Return 0
        End If

    End Function

    Public Function traerProcesosEnEspera(ByVal dbc As OraConn, ByVal idAdm As Integer, ByVal cant As Integer, ByVal horaRevision As Date, ByVal fun As String) As DataSet
        Dim dsControl As DataSet
        Dim cccontrol As ACR.ccControlAcreditacion
        dbc.BeginTrans()
        dsControl = ControlAcr.buscarProcesoEsperaCant(dbc, idAdm, cant, horaRevision, Configuracion.idAcreditador)

        'Dim dr As DataRow
        'For Each dr In dsControl.Tables(0).Rows
        '    cccontrol = New ACR.ccControlAcreditacion(dr)
        '    Dim ep As String = IIf(cccontrol.estadoProceso = "ES", "XS", "XA")
        '    ControlAcr.modEstadoProceso(dbc, idAdm, cccontrol.codOrigenProceso, cccontrol.idUsuarioProceso, cccontrol.numeroId, ep, cccontrol.idUsuarioProceso, fun)
        'Next

        dbc.Commit()
        traerProcesosEnEspera = dsControl

    End Function

    Public Function traerProcesosEnEsperaOffLine(ByVal dbc As OraConn, ByVal idAdm As Integer, ByVal cant As Integer, ByVal horaRevision As Date, ByVal fun As String) As DataSet
        Dim dsControl As DataSet
        Dim cccontrol As ACR.ccControlAcreditacion
        dbc.BeginTrans()
        dsControl = ControlAcr.buscarProcesoEsperaCantSim(dbc, idAdm, cant, horaRevision, Configuracion.idAcreditador)
        dbc.Commit()
        traerProcesosEnEsperaOffLine = dsControl

    End Function

    Private Shared Sub evento(ByVal LOG As Procesos.logEtapa, ByVal mensaje As String)
        If Not IsNothing(LOG) Then
            LOG.AddEvento(mensaje)
            LOG.Save()
        End If
    End Sub


    <WebMethod()> Public Function wmProcesoBatch(ByVal idAdm As Integer, ByVal seqProceso As Integer, ByVal seqEtapa As Integer, ByVal ds As DataSet, ByVal usu As String, ByVal fun As String)

        Dim log As Procesos.logEtapa
        Dim dbc As New OraConn()
        Dim ds2 As DataSet
        Dim cAcr As ACR.ccControlAcreditacion
        ''' para FTP
        Dim utilCopiar As FTPUtil.Copiador
        Dim errMsg As String
        Dim DirLocal As String
        Dim strFTPUtil As String = System.Configuration.ConfigurationSettings.AppSettings("STRING_FTP")
        Dim ip As String
        Dim usuario As String
        Dim password As String
        Dim port As String
        Dim pathDest As String
        Dim pathOrigen As String
        Dim Archivo As String
        Try


            'evento(log, "Inicio De Proceso")
            Select Case ds.Tables(0).Rows(0).Item("ID_ETAPA")
                Case "ETAPA1"

                    Dim fecDesde As Date = ds.Tables(0).Rows(0).Item("FEC_DESDE")
                    Dim fecHasta As Date = ds.Tables(0).Rows(0).Item("FEC_HASTA")



                    'evento(log, "Se inicia la etapa " & ds.Tables(0).Rows(0).Item("ID_ETAPA"))

                    'evento(log, "Generando el archivo ACRANEXO1 ...")

                    Sys.IngresoEgreso.Cotizaciones.IndCalidad.creacionAnexo1(dbc, idAdm, fecDesde, fecHasta, seqProceso, seqEtapa, usu, fun)

                    log = New Procesos.logEtapa(idAdm, seqProceso, seqEtapa)

                    evento(log, "Copiando archivo . . .")

                    utilCopiar = New FTPUtil.Copiador()

                    ip = traerString(strFTPUtil, ";", "=")
                    usuario = traerString(strFTPUtil, ";", "=")
                    password = traerString(strFTPUtil, ";", "=")
                    port = traerString(strFTPUtil, ";", "=")

                    pathOrigen = traerString(strFTPUtil, Nothing, "=")
                    pathDest = Sys.Soporte.Archivo.traer(dbc, idAdm, "ACRANEXO1").Tables(0).Rows(0).Item("RUTA") & "\"

                    evento(log, "Archivo correctamente generado en el servidor de datos " & pathOrigen)

                    evento(log, "Copiando el archivo generado en la ruta " & pathDest)

                    'Archivo = "Anexo1_" & Format(fecDesde, "yyyyMMdd") & Format(fecHasta, "yyyyMMdd") & ".csv"
                    Archivo = "Anexo1_" & CStr(seqProceso) & "_" & CStr(seqEtapa) & ".csv"

                    If utilCopiar.Traer(ip, usuario, password, pathOrigen, port, Archivo, pathDest, errMsg) = False Then
                        evento(log, errMsg)
                        log.estado = Procesos.Estado.ConError
                        evento(log, "Proceso ha finalizado con error")
                    End If

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
    Private Shared Function traerString(ByRef strWeb As String, ByVal str1 As String, ByVal str2 As String) As String
        'IP=172.16.0.15;Usuario=usrcopia;Password=copia2006;Port=21;Path=/base2/oracle/utl/bansand2
        'IP=172.16.0.15;Usuario=usrcopia;Password=copia2006;Port=21;Path=/home/oracle/OraHome_1/utl/bansande
        Dim pos As Integer
        Dim str As String

        If str1 = Nothing Then
            str = Mid(strWeb, InStr(strWeb, str2) + 1)
        Else
            pos = InStr(strWeb, str1)
            str = Mid(strWeb, 1, pos - 1)
            str = Mid(str, InStr(str, str2) + 1)
            strWeb = Mid(strWeb, pos + 1, strWeb.Length)
        End If
        traerString = str
    End Function

    <WebMethod()> Public Sub BuscaLotesPerdidos(ByVal idadm As Integer)
        'ByVal idadm As Integer, ByVal numeroId As Integer, ByVal codOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal fecInicio As Object, ByVal fecFin As Object, ByVal estadoProceso As String)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            ControlAcr.BuscaLotesPerdidos(dbc, idadm, 4, "PCI", "SONDA")

        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Sub

End Class


