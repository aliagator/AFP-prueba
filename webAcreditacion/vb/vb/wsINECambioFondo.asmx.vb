Imports System.Web.Services
Imports Sonda.Net.DB
Imports Sonda.Gestion.Adm.Sys.CodeCompletion

Imports Sonda.Gestion.Adm.Sys.IngresoEgreso
Imports Sonda.Gestion.Adm.Sys.Soporte
Imports Sonda.Net


<WebService(Namespace:="http://tempuri.org/")> _
Public Class wsINECambioFondo
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

    <WebMethod()> Public Sub wmCrearConSecuencia(ByVal idAdm As Integer, ByVal numSolicitudAut As Integer, ByVal idCliente As Integer, ByVal tipoProducto As String, ByVal tipoFondoDestino As String, ByVal tipoFondoOrigen As String, ByVal porcDistribucionDestino As Decimal, ByVal porcDistribucionReal As Decimal, ByVal porcRecaudacion As Decimal, ByVal numCambio As Integer, ByVal tipoDistribucion As String, ByVal fecCambio As Date, ByVal tipoCuenta As Integer, ByVal fecProceso As Date, ByVal numeroId As Integer, ByVal fecAcreditacion As Date, ByVal fecEnvAcreditacion As Date, ByVal estadoCambio As String, ByVal ajustePeriodo As Object, ByVal usu As String, ByVal fun As String)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()
            Sys.IngresoEgreso.CambioFondo.cabecera.crearConSecuencia(dbc, idAdm, numSolicitudAut, idCliente, tipoProducto, tipoFondoDestino, tipoFondoOrigen, porcDistribucionDestino, porcDistribucionReal, porcRecaudacion, numCambio, tipoDistribucion, fecCambio, tipoCuenta, fecProceso, numeroId, fecAcreditacion, fecEnvAcreditacion, estadoCambio, ajustePeriodo, usu, fun)
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

    <WebMethod()> Public Function wmObtenerSecuenciaSolicitud(ByVal idAdm As Integer) As Integer
        Dim dbc As OraConn
        Try
            dbc = New OraConn()

            wmObtenerSecuenciaSolicitud = Sys.IngresoEgreso.CambioFondo.cabecera.obtenerSeqSolicitud(dbc, idAdm)

        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function

    <WebMethod()> Public Function buscarDistintoNumeroId(ByVal idAdm As Integer, ByVal numeroId As Integer, ByVal periodo As Date) As DataSet
        Dim obj As New Sys.IngresoEgreso.CambioFondo()
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            buscarDistintoNumeroId = obj.cabecera.buscarDistiNumeroId(dbc, idAdm, numeroId, periodo)
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function

    <WebMethod()> Public Function wmModEstadoSolicitud(ByVal idAdm As Integer, ByVal fecCambio As Date, ByVal idCliente As Integer, ByVal tipoDistribucion As String, ByVal numSolicitudAut As Integer, ByVal estadoCambio As String, ByVal numeroId As Integer, ByVal usu As String, ByVal fun As String) As Integer
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            wmModEstadoSolicitud = CambioFondo.cabecera.modEstadoCambio(dbc, idAdm, fecCambio, idCliente, tipoDistribucion, numSolicitudAut, estadoCambio, numeroId, usu, fun)
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function

    <WebMethod()> Public Function wmTraerCabecera(ByVal idAdm As Integer, ByVal seqCambio As Integer) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            wmTraerCabecera = CambioFondo.cabecera.traer(dbc, idAdm, seqCambio)
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function

    <WebMethod()> Public Function wmbuscarPorSolicitud(ByVal idAdm As Integer, ByVal numSolicitudAut As Integer) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            wmbuscarPorSolicitud = CambioFondo.cabecera.buscarPorSolicitud(dbc, idAdm, numSolicitudAut)
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function

    <WebMethod()> Public Function wmModificarCabecera(ByVal idAdm As Integer, ByVal seqCambio As Integer, ByVal numSolicitudAut As Integer, ByVal idcliente As Integer, ByVal tipoProducto As String, ByVal tipoFondoDestino As String, ByVal tipoFondoOrigen As String, ByVal porcDistribucionDestino As Decimal, ByVal porcDistribucionReal As Decimal, ByVal numCambio As Integer, ByVal tipoDistribucion As String, ByVal fecCambio As Date, ByVal tipoCuenta As Integer, ByVal fecProceso As Date, ByVal numeroId As Integer, ByVal fecAcreditacion As Date, ByVal fecEnvAcreditacion As Date, ByVal estadoCambio As String, ByVal USU As String, ByVal fun As String)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            CambioFondo.cabecera.modificar(dbc, idAdm, seqCambio, numSolicitudAut, idcliente, tipoProducto, tipoFondoDestino, tipoFondoOrigen, porcDistribucionDestino, porcDistribucionReal, numCambio, tipoDistribucion, fecCambio, tipoCuenta, fecProceso, numeroId, fecAcreditacion, fecEnvAcreditacion, estadoCambio, USU, fun)
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function

    <WebMethod()> Public Function wmEnviarAcreditacion(ByVal idAdm As Integer, ByVal fecProceso As Date, ByVal tipoCambio As String, ByVal numSolicitud As Decimal, ByVal usu As String, ByVal fun As String, ByVal log As Procesos.logEtapa)

        enviarAcreditacion(idAdm, fecProceso, tipoCambio, numSolicitud, usu, fun, Nothing)
    End Function

    <WebMethod()> Public Function wmEnviarAcreditacion2()
        enviarAcreditacion(1, New Date(2008, 12, 18), "01000", 91739225, "LFC", "LFC", Nothing) ' FRAG 230909 AJUPER
        'enviarAcreditacion(1, New Date(2008, 12, 2), "01000", 91739155, "RCZ", "RCZ", Nothing)
        'enviarAcreditacion(idAdm, fecProceso, tipoCambio, numSolicitud, usu, fun, Nothing)
    End Function

    Public Shared Function enviarAcreditacion(ByVal idAdm As Integer, ByVal fecProceso As Date, ByVal tipoCambio As String, ByVal numSolicitud As Decimal, ByVal usu As String, ByVal fun As String, Optional ByVal log As Procesos.logEtapa = Nothing)
        Dim dsCf, Solicitud, ds As DataSet
        Dim dbc As OraConn
        Dim i, codError, totRegCreados, cantSolic, cantSolicERR As Integer
        Dim numeroId As Integer
        Dim V(4) As Decimal
        Dim hoy, fecCreacion, fecCierre, fecAcreditacion, fecValorCuota As Date
        Dim ccControl As ACR.ccControlAcreditacion
        Dim contAcr As New INEControlAcr()
        Dim estadoSolicitud As String
        Dim cp As Sys.Soporte.Procesos
        Dim numproceso As Integer
        Dim numSolicitudActual As Integer
        Dim tipoSolicitud As String
        Dim dsSolicitud As DataSet
        Dim ccAdmSolicitud As Sys.CodeCompletion.ADM.ccSolicitudes
        Dim IdClienteActual As String

        Dim tipoDistribucion As String

        Try
            dbc = New OraConn()

            cantSolic = 0
            cantSolicERR = 0
            codError = 0
            If IsNothing(log) Then numproceso = 0
            hoy = Sys.Soporte.Fecha.ahora(dbc)
            'se elimina tipocambio para excedente fondo A  ********

            Select Case tipoCambio

                Case "10000" : tipoSolicitud = "cambio de fondo normal"

                Case "01000" : tipoSolicitud = "distribución de fondos" 'añade CFR

                Case "00100" : tipoSolicitud = "asignación etarea"

                Case "00010" : tipoSolicitud = "Excedente"

                Case "00001" : tipoSolicitud = "ajuste periodico" ' FRAG 230909 AJUPER

                    ' leo: implementarlo en el web// si fuese aparte
                    ' Case "10100" : tipoSolicitud = "cambio fondo recaudador"

            End Select

            'TRAE SOLICITUDES UNICAS//COMBOBOX
            Solicitud = CambioFondo.cabecera.buscarDistSolicitudes(dbc, idAdm, fecProceso, tipoCambio, numSolicitud)

            If Solicitud.Tables(0).Rows.Count = 0 Then

                evento(log, "No se encontraron solicitudes para acreditar")

                log.estado = Procesos.Estado.Exitoso

                Exit Function

            Else

                evento(log, "Seleccion de " & Solicitud.Tables(0).Rows.Count & " solicitud(es) de " & tipoSolicitud & " con fecha de cambio anterior o igual a " & fecProceso)

            End If

            fecAcreditacion = Sys.Kernel.Parametros.FechaAcreditacion.obtenerFechaAcreditacion(dbc, idAdm, "ACR")
            fecValorCuota = Sys.Kernel.Parametros.FechaAcreditacion.obtenerFechaValorCuota(dbc, idAdm, "ACR")
            evento(log, "************************************")
            evento(log, "Parámetros: " & tipoSolicitud)
            evento(log, " Fec.Acreditacion: " & fecAcreditacion.ToShortDateString)
            evento(log, " Fec.Valor Cuota : " & fecValorCuota.ToShortDateString)
            evento(log, "************************************")
            evento(log, "")

            contAcr.crearProcesoAcreditacion(idAdm, "CAMBFOND", usu, numproceso, 0, "MSI", usu, fun, numeroId, fecCreacion, codError)
            'numeroId = 398389

            'PARA CADA SOLICITUD

            For i = 0 To Solicitud.Tables(0).Rows.Count - 1

                codError = 0
                'TRAE SOICITUDE ACR_MOV.... //NO HAY DETALLE

                dsCf = CambioFondo.cabecera.buscarPorSolicitud(dbc, idAdm, Solicitud.Tables(0).Rows(i).Item("NUM_SOLICITUD_AUT"))

                If dsCf.Tables(0).Rows.Count > 0 Then
                    dbc.BeginTrans()
                    numSolicitudActual = Solicitud.Tables(0).Rows(i).Item("NUM_SOLICITUD_AUT")
                    IdClienteActual = Solicitud.Tables(0).Rows(i).Item("ID_CLIENTE")

                    ''' agregado por IRM 09-06-2006
                    If Solicitud.Tables(0).Rows(i).Item("TIPO_DISTRIBUCION") = "CFN" Or Solicitud.Tables(0).Rows(i).Item("TIPO_DISTRIBUCION") = "CFD" Or Solicitud.Tables(0).Rows(i).Item("TIPO_DISTRIBUCION") = "CFR" Or Solicitud.Tables(0).Rows(i).Item("TIPO_DISTRIBUCION") = "CFE" Then ' FRAG 230909 AJUPER
                        dsSolicitud = Adm.Sys.AdministracionClientes.Solicitud.traer(dbc, idAdm, numSolicitudActual)
                        If dsSolicitud.Tables(0).Rows.Count > 0 Then
                            ccAdmSolicitud = New Sys.CodeCompletion.ADM.ccSolicitudes(dsSolicitud)
                            Sys.Soporte.Etapa.crear(dbc, idAdm, ccAdmSolicitud.seqSolicitudSol, IIf(Solicitud.Tables(0).Rows(i).Item("TIPO_DISTRIBUCION") = "CFN", "SCFE03", "SDFE03"), usu, fecAcreditacion, 0, "T", usu, fun)
                        Else
                            evento(log, "No se encontro registro en ADM_SOLICITUDES")
                            evento(log, "Solicitud número " & numSolicitudActual & "- Id_Cliente (" & IdClienteActual & ") ha sido ignorada")
                            codError = 1
                        End If
                    End If

                    If codError = 0 Then

                        CambioFondo.Detalle.eliminarSolicitud(dbc, idAdm, Solicitud.Tables(0).Rows(i).Item("NUM_SOLICITUD_AUT"))
                        'obtener tipo, proceso masivo o sólo una solicitud

                        If tipoDistribucion Is Nothing Or tipoDistribucion = "CFR" Then

                            tipoDistribucion = Solicitud.Tables(0).Rows(i).Item("TIPO_DISTRIBUCION")

                        End If

                        Select Case Solicitud.Tables(0).Rows(i).Item("TIPO_DISTRIBUCION")

                            Case "CFN" : mCambioFondo(dbc, idAdm, dsCf, fecValorCuota, fecAcreditacion, numeroId, usu, fun, log, codError)
                            Case "CFA" : mAsignacionFondo(dbc, idAdm, dsCf, fecValorCuota, fecAcreditacion, numeroId, usu, fun, log, codError)
                            Case "CFD", "CFT", "CFR", "CFE", "CFP" : mDistribucion(dbc, idAdm, dsCf, fecValorCuota, fecAcreditacion, numeroId, usu, fun, log, codError) 'FRAG 230909 AJUPER

                            Case Else

                                evento(log, "Tipo de distribucion no válido")
                                If Not IsNothing(log) Then
                                    log.estado = Procesos.Estado.ConError
                                End If
                                dbc.Rollback()
                                Exit Function

                        End Select
                    End If

                    cantSolic = i + 1

                    If codError <> 0 Then
                        cantSolicERR += 1
                        dbc.Rollback()
                        codError = 0
                    Else
                        dbc.Commit()
                    End If
                    If cantSolic Mod 500 = 0 Then
                        evento(log, "Se ha valorizado " & cantSolic & " solicitudes ")
                    End If
                End If
            Next i

            evento(log, "Se ha valorizado " & cantSolic & " solicitudes ")

            'If tipoCambio = "010" Or tipoCambio = "101" Then
            If tipoCambio = "01000" Or tipoCambio = "10100" Or tipoCambio = "00001" Then  ' FRAG 230909 AJUPER
                resumenValorizacion(dbc, idAdm, numeroId, "CFR", log)
            End If

            'If tipoCambio = "100" Or tipoCambio = "010" Or tipoCambio = "001" Or tipoCambio = "101" Then
            If tipoCambio = "10000" Or tipoCambio = "01000" Or tipoCambio = "00100" Or tipoCambio = "10100" Or tipoCambio = "00001" Then ' FRAG 230909 AJUPER
                If cantSolicERR > 0 Then
                    evento(log, "Revision de Administracion de Solicitudes (" & cantSolicERR & " rechazadas)")
                    Sys.AdministracionClientes.Solicitud.actualizaAcreditSolicitud(dbc, idAdm, fecProceso, numeroId, usu, fun)
                End If
            End If

            contAcr.CerrarProcesoAcreditacion(idAdm, "CAMBFOND", usu, numeroId, usu, fun, totRegCreados, fecCierre, codError)

            If totRegCreados = 0 And tipoDistribucion <> "CFR" Then
                evento(log, "No se encontraron registros para enviar a acreditar...")
                Exit Function
            End If

            'LFC:// 07-10 solo distribuciones
            If tipoDistribucion = "CFD" Or tipoDistribucion = "CFP" Then
                Sonda.Gestion.Adm.Sys.IngresoEgreso.Transacciones.modificarIndCierreProd(dbc, idAdm, "CAMBFOND", numeroId, Nothing)
            End If

            ' leo: se agrega la validacion ya que el CFR no genera transacciones 
            ' evento(log, "Se crearon " & totRegCreados & " transacciones para simular en el proceso de acreditación Nº " & numeroId)
            If tipoDistribucion = "CFR" Then
                contAcr.eliminarProcesoAcreditacion(idAdm, "CAMBFOND", usu, numeroId)
                ControlAcr.modEstadoProceso(dbc, idAdm, "CAMBFOND", usu, numeroId, "NU", usu, fun)
                evento(log, "No se crearon transacciones...")
                evento(log, "Finalizando Proceso de Cambio fondo Recaudador...")
            Else
                evento(log, "Se crearon " & totRegCreados & " transacciones para simular en el proceso de acreditación Nº " & numeroId)
            End If

            If Not IsNothing(log) Then
                log.estado = Procesos.Estado.Exitoso
            End If
        Catch e As SondaException
            evento(log, "Error en la solicitud " & numSolicitudActual)
            If Not IsNothing(log) Then
                log.estado = Procesos.Estado.ConError
            End If
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            evento(log, "Error en la solicitud " & numSolicitudActual)
            If Not IsNothing(log) Then
                log.estado = Procesos.Estado.ConError
            End If
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()

        End Try
    End Function

    'muestra log con resumen de las solicitudes CFN - CFR

    Public Shared Sub resumenValorizacion(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal numeroId As Integer, ByVal tipo_distribucion As String, ByRef log As Procesos.logEtapa)
        Dim ds As New DataSet()
        Dim i As Integer
        Dim TD As String
        Dim SolCFR As Integer
        Dim SolCFN As Integer
        Dim SolRE As Integer

        ds = CambioFondo.Detalle.resumenValorizacion(dbc, idAdm, numeroId, tipo_distribucion)
        If ds.Tables(0).Rows.Count = 0 Then Exit Sub

        evento(log, "************************************")
        evento(log, "Sol.Cambio Fondo Recaudador... ")
        evento(log, "Acreditadas: " & ds.Tables(0).Rows(0).Item("total_sol"))
        evento(log, "Digitadas  : " & ds.Tables(0).Rows(1).Item("total_sol"))
        evento(log, "Error      : " & ds.Tables(0).Rows(2).Item("total_sol"))
        evento(log, "************************************")

    End Sub


    Private Shared Sub mAsignacionFondo(ByVal dbc As OraConn, ByVal idAdm As Integer, ByVal dsCF As DataSet, ByVal fecValorCuota As Date, ByVal fecAcreditacion As Date, ByVal numeroId As Integer, ByVal usu As String, ByVal fun As String, ByRef log As Procesos.logEtapa, ByRef codError As Integer)

        Dim ccf As ACR.ccMovCambiosFondos
        Dim cDet As ACR.ccDetCambiosFondos
        Dim rCLI As AAA.ccClientes
        Dim tipoCliente As String
        Dim tipoComision As String
        Dim codMvtoComis As String
        Dim dsAux, dsSal As DataSet
        Dim i, j, num As Integer
        Dim seqRegistro As Integer
        Dim codMvtoABO, codMvtoCAR As String
        Dim ProdVig(4) As Boolean
        Dim indCierreProducto As String
        Dim numSol, idCliente As Integer
        Dim fecCambio As Date

        '--.--cn2
        'Dim subCategoria As Long = 0
        'Dim codRegTributario As String = Nothing
        Dim tipoRezago As Integer = 0
        Dim valMlMontoNominal As Decimal = 0



        codError = 0
        numSol = dsCF.Tables(0).Rows(0).Item("NUM_SOLICITUD_AUT")
        idCliente = dsCF.Tables(0).Rows(0).Item("ID_CLIENTE")
        fecCambio = dsCF.Tables(0).Rows(0).Item("FEC_CAMBIO")

        dsAux = Sys.Kernel.Cliente.traer(dbc, idAdm, dsCF.Tables(0).Rows(0).Item("ID_CLIENTE"))
        If dsAux.Tables(0).Rows.Count = 0 Then
            codError = ignorarSolicitud(dbc, idAdm, log, "Cliente no identificado : " & idCliente, Nothing, codError, ccf, numeroId, usu, fun)
            Exit Sub
        End If
        rCLI = New AAA.ccClientes(dsAux)
        tipoCliente = IIf(rCLI.tipoRegPrevisional = "A", "AFP", "APV")


        For i = 0 To dsCF.Tables(0).Rows.Count - 1

            ccf = New ACR.ccMovCambiosFondos(dsCF.Tables(0).Rows(i))

            DeterminaCodMovimiento(ccf.tipoProducto, "CFA", codMvtoABO, codMvtoCAR)

            Select Case ccf.tipoProducto
                Case "CCV" : tipoComision = "PAD4" : codMvtoComis = "420516"
                Case "CDC" : tipoComision = "PAD5" : codMvtoComis = "520516"
                Case "CAV" : tipoComision = "PAD2" : codMvtoComis = "220516"

                Case Else : tipoComision = Nothing : codMvtoComis = Nothing
            End Select

            'traemos todos los productos vigentes para el tipo de producto
            dsSal = InformacionCliente.buscarSaldoVigProd(dbc, idAdm, ccf.idCliente, ccf.tipoProducto, Nothing)

            If dsSal.Tables(0).Rows.Count = 0 Then

                codError = ignorarSolicitud(dbc, idAdm, log, "Cliente(1) : " & idCliente & " no tiene saldo vigente " & ccf.tipoProducto & " , " & ccf.tipoFondoOrigen, Nothing, codError, ccf, numeroId, usu, fun)
                Exit Sub
            End If

            CargarProductosVigentes(dsSal, ProdVig, num)

            If Not ProductoVigente(ProdVig, ccf.tipoFondoOrigen) Then
                codError = ignorarSolicitud(dbc, idAdm, log, "Cliente : " & idCliente & " no tiene " & ccf.tipoProducto & " fondo " & ccf.tipoFondoOrigen & " vigente", Nothing, codError, ccf, numeroId, usu, fun)
                Exit Sub ' fondo origen no esta vigente
            End If

            '--->>>>>>---MVC---CA---1639861---27-02-19---
            If ccf.porcDistribucionReal < 100 Then
                If quedaraConMasDeDosFondos(ProdVig, ccf.tipoFondoDestino) Then
                    codError = ignorarSolicitud(dbc, idAdm, log, "Afiliado quedaria con mas de dos fondos (2), se pretendía mover al fondo " & ccf.tipoFondoDestino, Nothing, codError, ccf, numeroId, usu, fun)
                    Exit Sub
                End If
            End If
            '---<<<<<<---MVC---CA---1639861---27-02-19---

            If ccf.tipoFondoOrigen = ccf.tipoFondoDestino Then 'se aprueba sin generar transacciones
                ccf.numeroId = numeroId
                'num = CambioFondo.cabecera.modEstadoCambio(dbc, idAdm, ccf.fecCambio, ccf.idCliente, ccf.tipoDistribucion, numSol, "AC", numeroId, usu, fun) 'APROBADA
            Else

                'valorizamos la el movimiento de cambio de fondo
                codError = CambioFondo.Detalle.valorizarCambiosFondos(dbc, idAdm, ccf.seqCambio, fecValorCuota, tipoCliente, fecAcreditacion, usu, fun)
                If codError <> 0 Then
                    codError = ignorarSolicitud(dbc, idAdm, log, "Ha ocurrido el siguiente error en la valorización: " & codError, Nothing, codError, ccf, numeroId, usu, fun)
                    Exit Sub ' error al valorizar
                End If
                dsAux = CambioFondo.Detalle.buscarPorSecuencia(dbc, idAdm, ccf.seqCambio)

                'Para cada detalle (categoría) se crean las transacciones
                For j = 0 To dsAux.Tables(0).Rows.Count - 1
                    If ccf.tipoFondoOrigen = ccf.tipoFondoDestino Then
                        num = CambioFondo.cabecera.modEstadoCambio(dbc, idAdm, ccf.fecCambio, ccf.idCliente, ccf.tipoDistribucion, numSol, "AC", numeroId, usu, fun) 'APROBADA
                    End If
                    cDet = New ACR.ccDetCambiosFondos(dsAux.Tables(0).Rows(j))
                    indCierreProducto = "N"

                    'CARGO EN EL ORIGEN
                    If (j = dsAux.Tables(0).Rows.Count - 1) Then
                        'If cDet.porcDistribucionDestino = 100 Then
                        If ccf.porcDistribucionReal = 100 Then
                            indCierreProducto = "C"
                            AbrirCerrarProducto(ProdVig, ccf.tipoFondoOrigen, "C")
                        End If
                    End If

                    '--.--cn2
                    INEControlAcr.crearTransacciones(dbc, idAdm, "CAMBFOND", usu, numeroId, rCLI.idPersona, ccf.idCliente, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, ccf.numSolicitudAut, 0, 0, 0, 0, 0, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, ccf.seqCambio, 0, 0, Nothing, ccf.tipoProducto, cDet.tipoFondoOrigen, cDet.tipoFondoOrigen, cDet.categoria, cDet.subCategoria, IIf(cDet.codRegTributario = "X", Nothing, cDet.codRegTributario), "CAR", "CTA", "CTA", Nothing, ccf.seqCambio, Nothing, "CAMBFOND", codMvtoCAR, Nothing, Nothing, Nothing, codMvtoComis, Nothing, Nothing, Nothing, Nothing, tipoRezago, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, Nothing, Nothing, valMlMontoNominal, cDet.valMlFondoOrigen, Nothing, Nothing, cDet.valCuoFondoOrigen, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tipoComision, cDet.valMlComision, cDet.valCuoComision, 0, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, "CUO", "N", indCierreProducto, "S", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, 0, 0, 0, 0, 0, 0, usu, fun, seqRegistro, codError)
                    If codError <> 0 Then
                        codError = ignorarSolicitud(dbc, idAdm, log, "Ha ocurrido el siguiente error en la creación de las transacciones", Nothing, codError, ccf, numeroId, usu, fun)
                        Exit Sub
                    End If
                    indCierreProducto = "N"

                    'ABONO EN EL DESTINO
                    If (j = 0) And Not ProductoVigente(ProdVig, ccf.tipoFondoDestino) Then
                        indCierreProducto = "A"
                        AbrirCerrarProducto(ProdVig, ccf.tipoFondoDestino, "A")
                    End If

                    '--.--cn2
                    INEControlAcr.crearTransacciones(dbc, idAdm, "CAMBFOND", usu, numeroId, rCLI.idPersona, ccf.idCliente, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, ccf.numSolicitudAut, 0, 0, 0, 0, cDet.subCategoria, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, ccf.seqCambio, 0, 0, Nothing, ccf.tipoProducto, cDet.tipoFondoOrigen, ccf.tipoFondoDestino, cDet.categoria, cDet.subCategoria, IIf(cDet.codRegTributario = "X", Nothing, cDet.codRegTributario), "ABO", "CTA", "CTA", Nothing, ccf.seqCambio, Nothing, "CAMBFOND", codMvtoABO, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tipoRezago, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, Nothing, Nothing, valMlMontoNominal, cDet.valMlFondoDestino, Nothing, Nothing, cDet.valCuoFondoDestino, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, "CUO", "N", indCierreProducto, "S", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, 0, 0, 0, 0, 0, 0, usu, fun, seqRegistro, codError)
                    If codError <> 0 Then
                        codError = ignorarSolicitud(dbc, idAdm, log, "Ha ocurrido el siguiente error en la creación de las transacciones", Nothing, codError, ccf, numeroId, usu, fun)
                        Exit Sub
                    End If

                Next j

                ccf.estadoCambio = "EN"
                ccf.numeroId = numeroId
                ccf.fecEnvAcreditacion = Fecha.ahora(dbc)
                CambioFondo.cabecera.modificar(dbc, idAdm, ccf.seqCambio, ccf.numSolicitudAut, ccf.idCliente, ccf.tipoProducto, ccf.tipoFondoDestino, ccf.tipoFondoOrigen, ccf.porcDistribucionDestino, ccf.porcDistribucionReal, ccf.numCambio, ccf.tipoDistribucion, ccf.fecCambio, ccf.tipoCuenta, ccf.fecProceso, numeroId, ccf.fecAcreditacion, ccf.fecEnvAcreditacion, ccf.estadoCambio, usu, fun)
            End If

        Next i

    End Sub



    Private Shared Sub cambioFondoSinSaldo(ByVal dbc As OraConn, ByVal idAdm As Integer, ByVal ccf As ACR.ccMovCambiosFondos, ByVal fecAcreditacion As Date, ByVal numeroId As Integer, ByVal numDetalle As Integer, ByVal usu As String, ByVal fun As String)
        Dim dsP As New DataSet()
        Dim rPr As AAA.ccProductos
        Dim newProducto As Long

        dsP = InformacionCliente.traerProductoVig(dbc, idAdm, ccf.idCliente, ccf.tipoProducto, ccf.tipoFondoOrigen)
        rPr = New AAA.ccProductos(dsP.Tables(0).Rows(0))
        InformacionCliente.cerrarProducto(dbc, idAdm, ccf.idCliente, rPr.numProducto, ccf.tipoProducto, "N", fecAcreditacion, Nothing, usu, fun)
        'nuevo producto
        newProducto = Sys.Kernel.Producto.crearConSecuencia(dbc, idAdm, rPr.idCliente, rPr.tipoProducto, ccf.tipoFondoDestino, rPr.numTipoProducto, "N", Nothing, Nothing, Nothing, fecAcreditacion, Nothing, ccf.numSolicitudAut, 0, ccf.tipoCuenta, usu, fun)



        Dim dsD As New DataSet()
        Dim rDi As AAA.ccDistribuciones
        dsD = Sys.Kernel.Producto.traerDistribucionPorNumero(dbc, idAdm, ccf.idCliente, rPr.numProducto, ccf.tipoProducto, ccf.tipoFondoOrigen, "V")
        rDi = New AAA.ccDistribuciones(dsD.Tables(0).Rows(0))
        InformacionCliente.cerrarDistribucion(dbc, idAdm, ccf.idCliente, rPr.numProducto, rDi.seqDistribucion, usu, fun)
        'nueva distribucion
        InformacionCliente.crearDistribucion(dbc, idAdm, ccf.idCliente, newProducto, ccf.tipoProducto, ccf.tipoFondoDestino, ccf.tipoFondoOrigen, Nothing, rPr.numTipoProducto, ccf.porcDistribucionDestino, ccf.porcRecaudacion, ccf.numSolicitudAut, fecAcreditacion, ccf.tipoDistribucion, fecAcreditacion, ccf.tipoCuenta, usu, fun)


        If numDetalle = 1 Then
            CambioFondo.cabecera.modificarEstadoSolicitud(dbc, idAdm, ccf.numSolicitudAut, "AP", usu, fun)
            CambioFondo.cabecera.modEstadoCambio(dbc, idAdm, ccf.fecCambio, ccf.idCliente, ccf.tipoDistribucion, ccf.numSolicitudAut, "AC", numeroId, usu, fun)
        Else
            CambioFondo.cabecera.modEstadoCambioDet(dbc, idAdm, ccf.numSolicitudAut, ccf.seqCambio, "AC", numeroId, fecAcreditacion, usu, fun)
        End If

    End Sub


    Private Shared Sub mCambioFondo(ByVal dbc As OraConn, ByVal idAdm As Integer, ByVal dsCF As DataSet, ByVal fecValorCuota As Date, ByVal fecAcreditacion As Date, ByVal numeroId As Integer, ByVal usu As String, ByVal fun As String, ByRef log As Procesos.logEtapa, ByRef codError As Integer)

        Dim ccf As ACR.ccMovCambiosFondos
        Dim cDet As ACR.ccDetCambiosFondos
        Dim rCLI As AAA.ccClientes
        Dim tipoCliente As String
        Dim tipoComision As String
        Dim codMvtoComis As String
        Dim dsAux, dsSal As DataSet
        Dim i, j, num, k As Integer
        Dim seqRegistro As Integer
        Dim codMvtoABO, codMvtoCAR As String
        Dim ProdVig(4) As Boolean
        Dim indCierreProducto As String
        Dim numSol, idCliente As Integer
        Dim fecCambio As Date
        Dim transacciones() As ACR.ccTransacciones
        Dim dstr As DataSet = Sys.IngresoEgreso.Transacciones.traer(dbc, -1, Nothing, Nothing, -1, -1)
        Dim cargosTipoProducto As Decimal = 0
        Dim seAcredita As Boolean
        Dim verifcuenta As Boolean

        '--.--cn2
        Dim tipoRezago As Integer = 0
        Dim valMlMontoNominal As Decimal = 0


        k = 0
        codError = 0
        numSol = dsCF.Tables(0).Rows(0).Item("NUM_SOLICITUD_AUT")
        idCliente = dsCF.Tables(0).Rows(0).Item("ID_CLIENTE")
        fecCambio = dsCF.Tables(0).Rows(0).Item("FEC_CAMBIO")
        seAcredita = False
        verifcuenta = True

        dsAux = Sys.Kernel.Cliente.traer(dbc, idAdm, dsCF.Tables(0).Rows(0).Item("ID_CLIENTE"))
        If dsAux.Tables(0).Rows.Count = 0 Then
            codError = ignorarSolicitud(dbc, idAdm, log, "Cliente no identificado : " & idCliente, Nothing, codError, ccf, numeroId, usu, fun)
            Exit Sub
        End If

        rCLI = New AAA.ccClientes(dsAux)
        tipoCliente = IIf(rCLI.tipoRegPrevisional = "A", "AFP", "APV")


        For i = 0 To dsCF.Tables(0).Rows.Count - 1

            ccf = New ACR.ccMovCambiosFondos(dsCF.Tables(0).Rows(i))

            DeterminaCodMovimiento(ccf.tipoProducto, "CFN", codMvtoABO, codMvtoCAR)
            Select Case ccf.tipoProducto
                Case "CCV" : tipoComision = "PAD4" : codMvtoComis = "420516"
                Case "CDC" : tipoComision = "PAD5" : codMvtoComis = "520516"
                Case "CAV" : tipoComision = "PAD2" : codMvtoComis = "220516"

                Case Else : tipoComision = Nothing : codMvtoComis = Nothing
            End Select

            'traemos todos los productos vigentes para el tipo de producto
            'aaa_saldos
            dsSal = InformacionCliente.buscarSaldoVigProd(dbc, idAdm, ccf.idCliente, ccf.tipoProducto, Nothing)

            'If dsSal.Tables(0).Rows.Count = 0 Then

            '    '###############################################################################################
            '    If ccf.tipoProducto = "CAI" Or ccf.tipoProducto = "CAV" Then
            '        cambioFondoSinSaldo(dbc, idAdm, ccf, fecAcreditacion, numeroId, dsCF.Tables(0).Rows.Count, usu, fun)
            '        evento(log, "      Actualizando Solicitud(Sin Saldo) Número: " & ccf.numSolicitudAut & "  Producto: " & ccf.tipoProducto)
            '        GoTo siguiente
            '    Else
            '        codError = ignorarSolicitud(dbc, idAdm, log, "Cliente(2) : " & idCliente & " no tiene saldo vigente " & ccf.tipoProducto & " , " & ccf.tipoFondoOrigen, Nothing, codError, ccf, numeroId, usu, fun)
            '        Exit Sub
            '    End If
            'End If


            If dsSal.Tables(0).Rows.Count = 0 Then

                '###############################################################################################

                'PARA PLANVITAL, TODAS LAS CUENTAS CON SALDO CERO---OK
                Dim gcodAdministradora As Integer
                gcodAdministradora = ParametrosINE.ParametrosGenerales.codigoAdministradora(dbc, idAdm)

                If gcodAdministradora = 1032 Or gcodAdministradora = 1035 Then
                    cambioFondoSinSaldo(dbc, idAdm, ccf, fecAcreditacion, numeroId, dsCF.Tables(0).Rows.Count, usu, fun)
                    evento(log, "      Actualizando Solicitud(Sin Saldo) Número: " & ccf.numSolicitudAut & "  Producto: " & ccf.tipoProducto)
                    GoTo siguiente

                Else

                    If ccf.tipoProducto = "CAI" Or ccf.tipoProducto = "CAV" Then
                        cambioFondoSinSaldo(dbc, idAdm, ccf, fecAcreditacion, numeroId, dsCF.Tables(0).Rows.Count, usu, fun)
                        evento(log, "      Actualizando Solicitud(Sin Saldo) Número: " & ccf.numSolicitudAut & "  Producto: " & ccf.tipoProducto)
                        GoTo siguiente
                    Else
                        codError = ignorarSolicitud(dbc, idAdm, log, "Cliente(2) : " & idCliente & " no tiene saldo vigente " & ccf.tipoProducto & " , " & ccf.tipoFondoOrigen, Nothing, codError, ccf, numeroId, usu, fun)
                        Exit Sub
                    End If


                End If

            End If

            CargarProductosVigentes(dsSal, ProdVig, num)

            If Not ProductoVigente(ProdVig, ccf.tipoFondoOrigen) Then
                codError = ignorarSolicitud(dbc, idAdm, log, "Cliente : " & idCliente & " no tiene " & ccf.tipoProducto & " fondo " & ccf.tipoFondoOrigen & " vigente", Nothing, codError, ccf, numeroId, usu, fun)
                Exit Sub ' fondo origen no esta vigente
            End If

            'Agregado por KPM 12/03/2007 
            If ccf.tipoFondoOrigen = ccf.tipoFondoDestino Then 'se aprueba sin generar transacciones
                ccf.numeroId = numeroId
                InformacionCliente.modTipoEleccionFondo(dbc, idAdm, ccf.idCliente, ccf.tipoProducto, "M", usu, fun)
                'num = CambioFondo.cabecera.modEstadoCambio(dbc, idAdm, ccf.fecCambio, ccf.idCliente, ccf.tipoDistribucion, numSol, "AC", numeroId, usu, fun) 'APROBADA
                verifcuenta = False
            Else
                seAcredita = True
                'valorizamos la el movimiento de cambio de fondo
                codError = CambioFondo.Detalle.valorizarCambiosFondos(dbc, idAdm, ccf.seqCambio, fecValorCuota, tipoCliente, fecAcreditacion, usu, fun)
                If codError <> 0 Then
                    codError = ignorarSolicitud(dbc, idAdm, log, "Ha ocurrido el siguiente error en la valorización: " & codError, Nothing, codError, ccf, numeroId, usu, fun)
                    Exit Sub
                End If
                dsAux = CambioFondo.Detalle.buscarPorSecuencia(dbc, idAdm, ccf.seqCambio)

                'Para cada detalle (categoría) se crean las transacciones
                For j = 0 To dsAux.Tables(0).Rows.Count - 1
                    'If ccf.tipoFondoOrigen = ccf.tipoFondoDestino Then
                    'num = CambioFondo.cabecera.modEstadoCambio(dbc, idAdm, ccf.fecCambio, ccf.idCliente, ccf.tipoDistribucion, numSol, "AC", numeroId, usu, fun) 'APROBADA
                    'End If
                    cDet = New ACR.ccDetCambiosFondos(dsAux.Tables(0).Rows(j))
                    indCierreProducto = "N"

                    'CARGO EN EL ORIGEN
                    If (j = dsAux.Tables(0).Rows.Count - 1) Then
                        indCierreProducto = "C"
                        AbrirCerrarProducto(ProdVig, ccf.tipoFondoOrigen, "C")
                    End If
                    '--.--cn2

                    INEControlAcr.crearTransacciones(dbc, idAdm, "CAMBFOND", usu, numeroId, rCLI.idPersona, ccf.idCliente, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, ccf.numSolicitudAut, 0, 0, 0, 0, 0, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, ccf.seqCambio, 0, 0, Nothing, ccf.tipoProducto, cDet.tipoFondoOrigen, cDet.tipoFondoOrigen, cDet.categoria, cDet.subCategoria, IIf(cDet.codRegTributario = "X", Nothing, cDet.codRegTributario), "CAR", "CTA", "CTA", Nothing, ccf.seqCambio, Nothing, "CAMBFOND", codMvtoCAR, Nothing, Nothing, Nothing, codMvtoComis, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, Nothing, Nothing, valMlMontoNominal, cDet.valMlFondoOrigen, Nothing, Nothing, cDet.valCuoFondoOrigen, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tipoComision, cDet.valMlComision, cDet.valCuoComision, 0, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, "CUO", "N", indCierreProducto, "S", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, 0, 0, 0, 0, 0, 0, usu, fun, seqRegistro, codError)
                    If codError <> 0 Then
                        codError = ignorarSolicitud(dbc, idAdm, log, "Ha ocurrido el siguiente error en la creación de las transacciones", Nothing, codError, ccf, numeroId, usu, fun)
                        Exit Sub
                    End If

                    cargosTipoProducto += cDet.valCuoFondoOrigen
                    indCierreProducto = "N"

                    If (j = 0) Then
                        indCierreProducto = "A"
                    End If

                    ReDim Preserve transacciones(k)

                    transacciones(k) = New ACR.ccTransacciones(dstr.Tables(0).NewRow)

                    With transacciones(k)
                        .usuarioProceso = usu
                        .idCliente = ccf.idCliente
                        .numReferenciaOrigen1 = ccf.numSolicitudAut
                        .idAlternativoDoc = ccf.seqCambio
                        .tipoProducto = ccf.tipoProducto
                        .tipoFondoOrigen = cDet.tipoFondoOrigen
                        .tipoFondoDestino = ccf.tipoFondoDestino
                        .categoria = cDet.categoria
                        .subCategoria = cDet.subCategoria
                        .codRegTributario = IIf(cDet.codRegTributario = "X", Nothing, cDet.codRegTributario)
                        .seqMvtoOrigen = ccf.seqCambio
                        .valMlMvto = cDet.valMlFondoDestino
                        .valCuoMvto = cDet.valCuoFondoDestino
                        .indCierreProducto = indCierreProducto
                        .codMvto = codMvtoABO
                    End With
                    k += 1
                Next j
            End If
            If verifcuenta Then
                If i = dsCF.Tables(0).Rows.Count - 1 Then
                    If cargosTipoProducto = 0 Then
                        If (ccf.tipoProducto <> "CAI" And ccf.tipoProducto <> "CAV") Or (ccf.tipoProducto = "CAI" And rCLI.tipoRegPrevisional <> "A") Then
                            codError = ignorarSolicitud(dbc, idAdm, log, "Ha ocurrido el siguiente error en la creación de las transacciones", "Cliente no tiene saldo en la cuenta  : " & ccf.tipoProducto, codError, ccf, numeroId, usu, fun)
                            Exit Sub
                        End If
                    End If
                    cargosTipoProducto = 0
                Else
                    If ccf.tipoProducto <> dsCF.Tables(0).Rows(i + 1).Item("TIPO_PRODUCTO") Then
                        If cargosTipoProducto = 0 Then
                            If (ccf.tipoProducto <> "CAI" And ccf.tipoProducto <> "CAV" And ccf.tipoProducto <> "CCV") Or (ccf.tipoProducto = "CAI" And rCLI.tipoRegPrevisional <> "A") Then
                                codError = ignorarSolicitud(dbc, idAdm, log, "Ha ocurrido el siguiente error en la creación de las transacciones", "Cliente no tiene saldo en la cuenta  : " & ccf.tipoProducto, codError, ccf, numeroId, usu, fun)
                                Exit Sub
                            End If
                        End If
                        cargosTipoProducto = 0
                    End If
                End If
                ccf.estadoCambio = "EN"
                ccf.numeroId = numeroId
                ccf.fecEnvAcreditacion = Fecha.ahora(dbc)
                CambioFondo.cabecera.modificar(dbc, idAdm, ccf.seqCambio, ccf.numSolicitudAut, ccf.idCliente, ccf.tipoProducto, ccf.tipoFondoDestino, ccf.tipoFondoOrigen, ccf.porcDistribucionDestino, ccf.porcDistribucionReal, ccf.numCambio, ccf.tipoDistribucion, ccf.fecCambio, ccf.tipoCuenta, ccf.fecProceso, numeroId, ccf.fecAcreditacion, ccf.fecEnvAcreditacion, ccf.estadoCambio, usu, fun)
            End If
            'Agregado por KPM 12/03/2007
            If Not seAcredita Then
                ccf.estadoCambio = "AC"
                ccf.fecEnvAcreditacion = Fecha.ahora(dbc)
                CambioFondo.cabecera.modificarEstadoSolicitud(dbc, idAdm, ccf.numSolicitudAut, "AP", usu, fun)
                CambioFondo.cabecera.modificar(dbc, idAdm, ccf.seqCambio, ccf.numSolicitudAut, ccf.idCliente, ccf.tipoProducto, ccf.tipoFondoDestino, ccf.tipoFondoOrigen, ccf.porcDistribucionDestino, ccf.porcDistribucionReal, ccf.numCambio, ccf.tipoDistribucion, ccf.fecCambio, ccf.tipoCuenta, ccf.fecProceso, numeroId, ccf.fecAcreditacion, ccf.fecEnvAcreditacion, ccf.estadoCambio, usu, fun)

            End If
siguiente:

        Next i

        'ahora se realizan los abonos guardados en el arreglo
        If IsNothing(transacciones) Then
            Exit Sub
        End If
        For j = 0 To UBound(transacciones)
            '--.--cn2
            INEControlAcr.crearTransacciones(dbc, idAdm, "CAMBFOND", usu, numeroId, rCLI.idPersona, transacciones(j).idCliente, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, transacciones(j).numReferenciaOrigen1, 0, 0, 0, 0, 0, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, transacciones(j).idAlternativoDoc, 0, 0, Nothing, transacciones(j).tipoProducto, transacciones(j).tipoFondoOrigen, transacciones(j).tipoFondoDestino, transacciones(j).categoria, transacciones(j).subCategoria, transacciones(j).codRegTributario, "ABO", "CTA", "CTA", Nothing, transacciones(j).seqMvtoOrigen, Nothing, "CAMBFOND", transacciones(j).codMvto, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tipoRezago, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, Nothing, Nothing, valMlMontoNominal, transacciones(j).valMlMvto, Nothing, Nothing, transacciones(j).valCuoMvto, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, "CUO", "N", transacciones(j).indCierreProducto, "S", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, 0, 0, 0, 0, 0, 0, usu, fun, seqRegistro, codError)
            If codError <> 0 Then
                codError = ignorarSolicitud(dbc, idAdm, log, "Ha ocurrido el siguiente error en la creación de las transacciones", Nothing, codError, ccf, numeroId, usu, fun)
                Exit Sub
            End If
        Next


    End Sub

    Private Shared Sub mDistribucion(ByVal dbc As OraConn, ByVal idAdm As Integer, ByVal dsCF As DataSet, ByVal fecValorCuota As Date, ByVal fecAcreditacion As Date, ByVal numeroId As Integer, ByVal usu As String, ByVal fun As String, ByVal log As Procesos.logEtapa, ByRef codError As Integer)
        Dim ccf As ACR.ccMovCambiosFondos
        Dim cDet As ACR.ccDetCambiosFondos
        Dim prodAnterior As String
        Dim rCLI As AAA.ccClientes
        Dim tipoCliente As String
        Dim tipoComision As String
        Dim codMvtoComis As String
        Dim dsAux, dsSal As DataSet
        Dim i, j, num, k As Integer
        Dim seqRegistro As Integer
        Dim codMvtoABO, codMvtoCAR As String
        Dim indCierreProducto As String
        Dim ProdVig(4) As Boolean

        Dim numSol, idCliente As Integer
        Dim fecCambio As Date
        Dim tipoDistribucion As String
        Dim transacciones() As ACR.ccTransacciones
        Dim cargosTipoProducto As Decimal

        '--.--cn2
        Dim tipoRezago As Integer = 0
        Dim valMlMontoNominal As Decimal = 0

        '--.--
        Dim saveIndCierreProducto As Boolean

        Dim dstr As DataSet = Sys.IngresoEgreso.Transacciones.traer(dbc, -1, Nothing, Nothing, -1, -1)

        Dim numSaldo As Long = 0 'lfc: 15-09-09; modificar el estado reg del saldos al finalizar la solicitud


        codError = 0
        numSol = dsCF.Tables(0).Rows(0).Item("NUM_SOLICITUD_AUT")
        idCliente = dsCF.Tables(0).Rows(0).Item("ID_CLIENTE")
        fecCambio = dsCF.Tables(0).Rows(0).Item("FEC_CAMBIO")
        tipoDistribucion = dsCF.Tables(0).Rows(0).Item("TIPO_DISTRIBUCION")

        dsAux = Sys.Kernel.Cliente.traer(dbc, idAdm, dsCF.Tables(0).Rows(0).Item("ID_CLIENTE"))
        If dsAux.Tables(0).Rows.Count = 0 Then
            codError = ignorarSolicitud(dbc, idAdm, log, "Cliente no identificado : " & idCliente, Nothing, codError, ccf, numeroId, usu, fun)
            Exit Sub
        End If

        rCLI = New AAA.ccClientes(dsAux)
        tipoCliente = IIf(rCLI.tipoRegPrevisional = "A", "AFP", "APV")
        prodAnterior = Nothing : k = 0


        For i = 0 To dsCF.Tables(0).Rows.Count - 1

            ccf = New ACR.ccMovCambiosFondos(dsCF.Tables(0).Rows(i))

            If ccf.tipoProducto <> prodAnterior Then

                'lfc: 15-09-09 modificar estado saldo ---numSaldo
                If numSaldo <> 0 Then
                    InformacionCliente.modificarEstadoSaldo(dbc, idAdm, ccf.idCliente, numSaldo, "V", usu, fun)
                    numSaldo = 0
                End If


                prodAnterior = ccf.tipoProducto
                cargosTipoProducto = 0
                DeterminaCodMovimiento(ccf.tipoProducto, tipoDistribucion, codMvtoABO, codMvtoCAR)

                Select Case ccf.tipoProducto
                    Case "CCV" : tipoComision = "PAD4" : codMvtoComis = "420516"
                    Case "CDC" : tipoComision = "PAD5" : codMvtoComis = "520516"
                    Case "CAV" : tipoComision = "PAD2" : codMvtoComis = "220516"
                    Case Else : tipoComision = Nothing : codMvtoComis = Nothing
                End Select

                'lfc: 15-09-09, no traer producto r100 creado, num_solicitud_aut
                'dsSal = InformacionCliente.buscarSaldoVigProd(dbc, idAdm, ccf.idCliente, ccf.tipoProducto, Nothing)

                dsSal = InformacionCliente.buscarSaldoVigProdSol(dbc, idAdm, ccf.idCliente, ccf.tipoProducto, Nothing, ccf.numSolicitudAut)


                If dsSal.Tables(0).Rows.Count = 0 Then
                    codError = ignorarSolicitud(dbc, idAdm, log, "Cliente : " & idCliente & " no tiene saldo vigente para el tipo producto " & ccf.tipoProducto, Nothing, codError, ccf, numeroId, usu, fun)
                    Exit Sub
                End If

                CargarProductosVigentes(dsSal, ProdVig, num)

            End If

            If ccf.porcDistribucionDestino = 100 Then
                If ProductoVigente(ProdVig, ccf.tipoFondoDestino) And num = 1 Then
                    'aprobar la solicitud
                    modificaDistribucion(dbc, idAdm, ccf, usu, fun)
                    CambioFondo.cabecera.modificarEstadoSolicitud(dbc, idAdm, ccf.numSolicitudAut, "AP", usu, fun)
                    'CambioFondo.cabecera.modEstadoCambio(dbc, idAdm, ccf.fecCambio, ccf.idCliente, ccf.tipoDistribucion, ccf.numSolicitudAut, "AC", numeroId, usu, fun)
                    ccf.estadoCambio = "AC"
                    ccf.numeroId = numeroId
                    ccf.fecEnvAcreditacion = Fecha.ahora(dbc).ToShortDateString
                    CambioFondo.cabecera.modificar(dbc, idAdm, ccf.seqCambio, ccf.numSolicitudAut, ccf.idCliente, ccf.tipoProducto, ccf.tipoFondoDestino, ccf.tipoFondoOrigen, ccf.porcDistribucionDestino, ccf.porcDistribucionReal, ccf.numCambio, ccf.tipoDistribucion, ccf.fecCambio, ccf.tipoCuenta, ccf.fecProceso, ccf.numeroId, ccf.fecAcreditacion, ccf.fecEnvAcreditacion, ccf.estadoCambio, usu, fun)
                    GoTo siguiente
                End If
            End If

            If ccf.porcDistribucionDestino = 0 Then
                If Not ProductoVigente(ProdVig, ccf.tipoFondoDestino) And num = 1 Then
                    'crear el producto y el saldo con las mismas categorias y regimen del otro saldo abierto

                    'lfc:15-09-09
                    numSaldo = CrearProducto(dbc, idAdm, ccf, dsSal, usu, fun)


                    ccf.estadoCambio = "AC"
                    ccf.numeroId = numeroId
                    ccf.fecEnvAcreditacion = Fecha.ahora(dbc).ToShortDateString
                    CambioFondo.cabecera.modificar(dbc, idAdm, ccf.seqCambio, ccf.numSolicitudAut, ccf.idCliente, ccf.tipoProducto, ccf.tipoFondoDestino, ccf.tipoFondoOrigen, ccf.porcDistribucionDestino, ccf.porcDistribucionReal, ccf.numCambio, ccf.tipoDistribucion, ccf.fecCambio, ccf.tipoCuenta, ccf.fecProceso, ccf.numeroId, ccf.fecAcreditacion, ccf.fecEnvAcreditacion, ccf.estadoCambio, usu, fun)
                    GoTo siguiente
                    'leo: solicitudes de cambio fondo recaudador
                ElseIf tipoDistribucion = "CFR" Then
                    modificaDistribucion(dbc, idAdm, ccf, usu, fun)
                    CambioFondo.cabecera.modificarEstadoSolicitud(dbc, idAdm, ccf.numSolicitudAut, "AP", usu, fun)
                    'CambioFondo.cabecera.modEstadoCambio(dbc, idAdm, ccf.fecCambio, ccf.idCliente, ccf.tipoDistribucion, ccf.numSolicitudAut, "AC", numeroId, usu, fun)
                    ccf.estadoCambio = "AC"
                    ccf.numeroId = numeroId '-- se debe pasar el numeroId para identificar las solicitudes
                    ccf.fecEnvAcreditacion = Nothing '--.--Fecha.ahora(dbc).ToShortDateString
                    CambioFondo.cabecera.modificar(dbc, idAdm, ccf.seqCambio, ccf.numSolicitudAut, ccf.idCliente, ccf.tipoProducto, ccf.tipoFondoDestino, ccf.tipoFondoOrigen, ccf.porcDistribucionDestino, ccf.porcDistribucionReal, ccf.numCambio, ccf.tipoDistribucion, ccf.fecCambio, ccf.tipoCuenta, ccf.fecProceso, ccf.numeroId, ccf.fecAcreditacion, ccf.fecEnvAcreditacion, ccf.estadoCambio, usu, fun)
                    GoTo siguiente

                    'LFC: 01-10-2009
                ElseIf tipoDistribucion = "CFD" And ccf.porcDistribucionReal = 0 And ccf.porcRecaudacion > 0 Then
                    'crear el producto y el saldo con las mismas categorias y regimen del otro saldo abierto
                    numSaldo = CrearProducto(dbc, idAdm, ccf, dsSal, usu, fun)

                    ccf.estadoCambio = "AC"
                    ccf.numeroId = numeroId
                    ccf.fecEnvAcreditacion = Fecha.ahora(dbc).ToShortDateString
                    CambioFondo.cabecera.modificar(dbc, idAdm, ccf.seqCambio, ccf.numSolicitudAut, ccf.idCliente, ccf.tipoProducto, ccf.tipoFondoDestino, ccf.tipoFondoOrigen, ccf.porcDistribucionDestino, ccf.porcDistribucionReal, ccf.numCambio, ccf.tipoDistribucion, ccf.fecCambio, ccf.tipoCuenta, ccf.fecProceso, ccf.numeroId, ccf.fecAcreditacion, ccf.fecEnvAcreditacion, ccf.estadoCambio, usu, fun)
                    GoTo siguiente
                Else
                    If ccf.porcRecaudacion = 0 Then
                        codError = ignorarSolicitud(dbc, idAdm, log, "Distribucion 0% Fondo Incorrecto", Nothing, codError, ccf, numeroId, usu, fun)
                        Exit Sub
                    End If
                End If
            End If

            'valorizamos la el movimiento de cambio de fondo
            codError = CambioFondo.Detalle.valorizarCambiosFondos(dbc, idAdm, ccf.seqCambio, fecValorCuota, tipoCliente, fecAcreditacion, usu, fun)

            If codError <> 0 Then
                codError = ignorarSolicitud(dbc, idAdm, log, "Ha ocurrido el siguiente error en la valorización: " & codError, Nothing, codError, ccf, numeroId, usu, fun)
                Exit Sub
            End If


            'buscar las solicitudes con diferentes fondos//lfc:11-09-09
            Dim filas() As DataRow = dsCF.Tables(0).Select("tipo_producto='" & ccf.tipoProducto & "'")
            Dim numFondosSol As Integer = filas.Length


            'TRAE DETALLE DE LA SOLICITUD// ACR_DET
            dsAux = CambioFondo.Detalle.buscarPorSecuencia(dbc, idAdm, ccf.seqCambio)
            'Para cada SubSaldo se realizan todos los cargos de la solicitud    
            For j = 0 To dsAux.Tables(0).Rows.Count - 1
                indCierreProducto = "N"

                cDet = New ACR.ccDetCambiosFondos(dsAux.Tables(0).Rows(j))
                saveIndCierreProducto = False

                'Dim OBJ As Object
                'OBJ = cDet.seqCambio
                'OBJ = cDet.tipoFondoOrigen
                'OBJ = cDet.categoria
                'OBJ = ccf.tipoProducto


                If i = dsCF.Tables(0).Rows.Count - 1 Then
                    saveIndCierreProducto = True
                Else

                    If numFondosSol = 2 And i = 0 Then
                        Try
                            Dim ccf2 As ACR.ccMovCambiosFondos
                            ccf2 = New ACR.ccMovCambiosFondos(dsCF.Tables(0).Rows(i + 1))
                            If ccf2.porcDistribucionDestino = 0 And ccf2.porcDistribucionReal = 0 And ccf2.porcRecaudacion > 0 And (ccf.tipoDistribucion = "CFD" Or ccf.tipoDistribucion = "CFP") Then
                                saveIndCierreProducto = True
                            End If
                        Catch : End Try

                    End If
                End If
                '********************************************************************************


                'If i = dsCF.Tables(0).Rows.Count - 1 Then    'antes lfc://26-06-2009
                If saveIndCierreProducto Then
                    If j = dsAux.Tables(0).Rows.Count - 1 Then
                        indCierreProducto = "C"
                    Else
                        If cDet.tipoFondoOrigen <> dsAux.Tables(0).Rows(j + 1).Item("TIPO_FONDO_ORIGEN") Then
                            indCierreProducto = "C"
                        End If
                    End If

                Else
                    If ccf.tipoProducto <> dsCF.Tables(0).Rows(i + 1).Item("TIPO_PRODUCTO") Then
                        If j = dsAux.Tables(0).Rows.Count - 1 Then
                            indCierreProducto = "C"
                        Else
                            If cDet.tipoFondoOrigen <> dsAux.Tables(0).Rows(j + 1).Item("TIPO_FONDO_ORIGEN") Then
                                indCierreProducto = "C"
                            End If

                        End If
                        ' End If


                    Else ' LFC://01/10/2009------------->>>>>>>>>>>>>>>>>>
                        If ccf.tipoDistribucion = "CFD" Then
                            Try
                                If ccf.tipoProducto = dsCF.Tables(0).Rows(i + 1).Item("TIPO_PRODUCTO") And cDet.tipoFondoOrigen <> dsAux.Tables(0).Rows(j + 1).Item("TIPO_FONDO_ORIGEN") Then
                                    indCierreProducto = "C"
                                End If
                            Catch
                                'indCierreProducto = "C"
                            End Try
                        End If
                    End If
                    '------<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                End If

                '--.--cn2
                INEControlAcr.crearTransacciones(dbc, idAdm, "CAMBFOND", usu, numeroId, rCLI.idPersona, ccf.idCliente, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, ccf.numSolicitudAut, 0, 0, 0, 0, 0, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, ccf.seqCambio, 0, 0, Nothing, ccf.tipoProducto, cDet.tipoFondoOrigen, cDet.tipoFondoOrigen, cDet.categoria, cDet.subCategoria, IIf(cDet.codRegTributario = "X", Nothing, cDet.codRegTributario), "CAR", "CTA", "CTA", Nothing, ccf.seqCambio, Nothing, "CAMBFOND", codMvtoCAR, Nothing, Nothing, Nothing, codMvtoComis, Nothing, Nothing, Nothing, Nothing, tipoRezago, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, Nothing, Nothing, valMlMontoNominal, cDet.valMlFondoOrigen, Nothing, Nothing, cDet.valCuoFondoOrigen, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, tipoComision, cDet.valMlComision, cDet.valCuoComision, 0, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, "CUO", "N", indCierreProducto, "S", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, 0, 0, 0, 0, 0, 0, usu, fun, seqRegistro, codError)

                cargosTipoProducto += cDet.valCuoFondoOrigen

                If codError <> 0 Then

                    'num = CambioFondo.cabecera.modEstadoCambio(dbc, idAdm, ccf.fecCambio, ccf.idCliente, ccf.tipoDistribucion, numSol, "RE", numeroId, usu, fun) 'RECHAZADA
                    codError = ignorarSolicitud(dbc, idAdm, log, "Ha ocurrido el siguiente error en la creación de las transacciones", Nothing, codError, ccf, numeroId, usu, fun)
                    Exit Sub
                End If

                If (j = 0) Then
                    indCierreProducto = "A"
                Else
                    indCierreProducto = "N"
                End If

                ReDim Preserve transacciones(k)

                transacciones(k) = New ACR.ccTransacciones(dstr.Tables(0).NewRow)

                With transacciones(k)
                    .usuarioProceso = usu
                    .idCliente = ccf.idCliente
                    .numReferenciaOrigen1 = ccf.numSolicitudAut
                    .idAlternativoDoc = ccf.seqCambio
                    .tipoProducto = ccf.tipoProducto
                    .tipoFondoOrigen = cDet.tipoFondoOrigen
                    .tipoFondoDestino = ccf.tipoFondoDestino
                    .categoria = cDet.categoria

                    .subCategoria = cDet.subCategoria
                    .codRegTributario = IIf(cDet.codRegTributario = "X", Nothing, cDet.codRegTributario)

                    .seqMvtoOrigen = ccf.seqCambio
                    .valMlMvto = cDet.valMlFondoDestino
                    .valCuoMvto = cDet.valCuoFondoDestino
                    .indCierreProducto = indCierreProducto
                    .codMvto = codMvtoABO
                End With
                k += 1
            Next j

            'lfc: 15-09-09 modificar estado saldo ---numSaldo
            If numSaldo <> 0 Then
                InformacionCliente.modificarEstadoSaldo(dbc, idAdm, ccf.idCliente, numSaldo, "V", usu, fun)
                numSaldo = 0
            End If


            ccf.estadoCambio = "EN"
            ccf.numeroId = numeroId
            ccf.fecEnvAcreditacion = Fecha.ahora(dbc).ToShortDateString
            CambioFondo.cabecera.modificar(dbc, idAdm, ccf.seqCambio, ccf.numSolicitudAut, ccf.idCliente, ccf.tipoProducto, ccf.tipoFondoDestino, ccf.tipoFondoOrigen, ccf.porcDistribucionDestino, ccf.porcDistribucionReal, ccf.numCambio, ccf.tipoDistribucion, ccf.fecCambio, ccf.tipoCuenta, ccf.fecProceso, ccf.numeroId, ccf.fecAcreditacion, ccf.fecEnvAcreditacion, ccf.estadoCambio, usu, fun)

            If i = dsCF.Tables(0).Rows.Count - 1 Then
                If cargosTipoProducto = 0 Then
                    If (ccf.tipoProducto <> "CAI" And ccf.tipoProducto <> "CAV") Or (ccf.tipoProducto = "CAI" And rCLI.tipoRegPrevisional <> "A") Then
                        codError = ignorarSolicitud(dbc, idAdm, log, "Ha ocurrido el siguiente error en la creación de las transacciones", "Cliente no tiene saldo en la cuenta  : " & ccf.tipoProducto, codError, ccf, numeroId, usu, fun)
                        Exit Sub
                    End If
                End If

            Else
                If ccf.tipoProducto <> dsCF.Tables(0).Rows(i + 1).Item("TIPO_PRODUCTO") Then
                    If cargosTipoProducto = 0 Then
                        If (ccf.tipoProducto <> "CAI" And ccf.tipoProducto <> "CAV") Or (ccf.tipoProducto = "CAI" And rCLI.tipoRegPrevisional <> "A") Then
                            codError = ignorarSolicitud(dbc, idAdm, log, "Ha ocurrido el siguiente error en la creación de las transacciones", "Cliente no tiene saldo en la cuenta  : " & ccf.tipoProducto, codError, ccf, numeroId, usu, fun)
                            Exit Sub
                        End If
                    End If
                End If
            End If
siguiente:

            'lfc: 15-09-09 modificar estado saldo ---numSaldo
            If numSaldo <> 0 Then
                InformacionCliente.modificarEstadoSaldo(dbc, idAdm, ccf.idCliente, numSaldo, "V", usu, fun)
                numSaldo = 0
            End If

        Next i
        'ahora se realizan los abonos guardados en el arreglo
        If Not IsNothing(transacciones) Then
            For j = 0 To UBound(transacciones)
                INEControlAcr.crearTransacciones(dbc, idAdm, "CAMBFOND", usu, numeroId, rCLI.idPersona, transacciones(j).idCliente, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, transacciones(j).numReferenciaOrigen1, 0, 0, 0, 0, 0, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, transacciones(j).idAlternativoDoc, 0, 0, Nothing, transacciones(j).tipoProducto, transacciones(j).tipoFondoOrigen, transacciones(j).tipoFondoDestino, transacciones(j).categoria, transacciones(j).subCategoria, transacciones(j).codRegTributario, "ABO", "CTA", "CTA", Nothing, transacciones(j).seqMvtoOrigen, Nothing, "CAMBFOND", transacciones(j).codMvto, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, transacciones(j).tipoRezago, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, Nothing, Nothing, transacciones(j).valMlMontoNominal, transacciones(j).valMlMvto, Nothing, Nothing, transacciones(j).valCuoMvto, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, "CUO", "N", transacciones(j).indCierreProducto, "S", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, 0, 0, 0, 0, 0, 0, usu, fun, seqRegistro, codError)
                If codError <> 0 Then
                    num = ignorarSolicitud(dbc, idAdm, log, "Ha ocurrido el siguiente error en la creación de las transacciones", Nothing, codError, ccf, numeroId, usu, fun)
                    Exit Sub
                End If
            Next
        End If
    End Sub

    Private Shared Sub AcumulaDestino(ByRef dt As DataTable, ByVal Categoria As String, ByVal monto As Decimal)
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            If dt.Rows(i).Item("CATEGORIA") = Categoria Then
                dt.Rows(i).Item("VAL_ML_MONTO") = dt.Rows(i).Item("VAL_ML_MONTO") + monto
                Exit Sub
            End If
        Next
        dt.Rows.Add(New Object() {Categoria, monto})
    End Sub

    Private Shared Sub AcumulaOrigen(ByRef dt As DataTable, ByVal Fondo As String, ByVal CuoMonto As Decimal)
        Dim i As Integer
        For i = 0 To dt.Rows.Count - 1
            If dt.Rows(i).Item("FONDO") = Fondo Then
                dt.Rows(i).Item("VAL_CUO_MONTO") = dt.Rows(i).Item("VAL_CUO_MONTO") + CuoMonto
                Exit Sub
            End If
        Next
        dt.Rows.Add(New Object() {Fondo, CuoMonto})
    End Sub

    Private Shared Function ProductoVigente(ByVal P() As Boolean, ByVal fondo As String) As Boolean
        Select Case fondo
            Case "A" : ProductoVigente = P(0)
            Case "B" : ProductoVigente = P(1)
            Case "C" : ProductoVigente = P(2)
            Case "D" : ProductoVigente = P(3)
            Case "E" : ProductoVigente = P(4)
        End Select
    End Function


    Private Shared Function CrearProducto(ByVal dbc As OraConn, ByVal idAdm As Integer, ByVal cf As ACR.ccMovCambiosFondos, ByVal dsSal As DataSet, ByVal usu As String, ByVal fun As String) As Long
        Dim ds As DataSet
        Dim rTpr As AAA.ccTiposProducto
        Dim numProducto As Integer
        Dim dr As DataRow
        Dim rSal As AAA.ccSaldos

        'lfc: 15-09-09 --- obtener num_saldo para cambiar estado   //de sub a function para modificar el estado del saldo
        Dim numSaldo As Long


        ' ds = Sys.Kernel.TipoProducto.traerTipoProducto(dbc, idAdm, cf.idCliente, cf.tipoProducto, "V")
        ds = Sys.Kernel.TipoProducto.traerTipoProductoVigente(dbc, idAdm, cf.idCliente, cf.tipoProducto)
        If ds.Tables(0).Rows.Count = 0 Then
            rTpr = New AAA.ccTiposProducto(ds.Tables(0).NewRow)
        Else
            rTpr = New AAA.ccTiposProducto(ds.Tables(0).Rows(0))
        End If
        numProducto = Sys.Kernel.Producto.crearConSecuencia(dbc, idAdm, cf.idCliente, cf.tipoProducto, cf.tipoFondoDestino, rTpr.numTipoProducto, "N", Nothing, Nothing, Nothing, Sys.Soporte.Fecha.ahora(dbc).ToShortDateString, Nothing, cf.numSolicitudAut, 0, cf.tipoCuenta, usu, fun)
        For Each dr In dsSal.Tables(0).Rows
            rSal = New AAA.ccSaldos(dr)
            If rSal.estadoReg = "V" And rSal.tipoProducto = cf.tipoProducto And rSal.tipoFondo <> cf.tipoFondoDestino Then
                numSaldo = Sys.Kernel.Producto.crearSaldoConSecuencia(dbc, idAdm, cf.idCliente, rSal.categoria, rSal.subCategoria, cf.tipoProducto, cf.tipoFondoDestino, rTpr.numTipoProducto, numProducto, cf.numSolicitudAut, 0, 0, 0, Nothing, 0, rSal.codRegTributario, Sys.Soporte.Fecha.ahora(dbc).ToShortDateString, Nothing, "V", Nothing, usu, fun)

                InformacionCliente.modificarEstadoSaldo(dbc, idAdm, cf.idCliente, numSaldo, "N", usu, fun)

                CrearProducto = numSaldo
            End If
        Next
        Sys.Kernel.Producto.crearDistribucion(dbc, idAdm, cf.idCliente, numProducto, 1, cf.tipoProducto, cf.tipoFondoDestino, Nothing, Nothing, rTpr.numTipoProducto, cf.porcDistribucionDestino, cf.porcRecaudacion, cf.numSolicitudAut, Sys.Soporte.Fecha.ahora(dbc).ToShortDateString, Nothing, cf.tipoDistribucion, Nothing, cf.tipoCuenta, usu, fun)
    End Function

    Private Shared Sub modificaDistribucion(ByVal dbc As OraConn, ByVal idAdm As Integer, ByVal cf As ACR.ccMovCambiosFondos, ByVal usu As String, ByVal fun As String)
        Dim ds As DataSet
        Dim rTpr As AAA.ccTiposProducto
        Dim rPro As AAA.ccProductos
        Dim rDis As AAA.ccDistribuciones

        ds = InformacionCliente.traerTipoProductoVig(dbc, idAdm, cf.idCliente, cf.tipoProducto)

        If ds.Tables(0).Rows.Count = 0 Then
            rTpr = New AAA.ccTiposProducto(ds.Tables(0).NewRow)
        Else
            rTpr = New AAA.ccTiposProducto(ds.Tables(0).Rows(0))
        End If

        If cf.tipoCuenta = 1 Then
            rTpr.tipoFondoRecaudacion = cf.tipoFondoDestino
        End If

        rTpr.numSolicitudUltModif = cf.numSolicitudAut
        rTpr.tipoEleccionFondos = "M"
        Sys.Kernel.TipoProducto.modificar(dbc, idAdm, rTpr.idCliente, rTpr.numTipoProducto, rTpr.tipoProducto, rTpr.fecAperturaTipoProducto, rTpr.fecCierreTipoProducto, rTpr.fecOrigen, rTpr.codInstitucionOrigen, rTpr.codInstitucionDestino, rTpr.codInstitucionFusion, rTpr.perPrimerPago, rTpr.fecUltimoPago, rTpr.indCotizante, rTpr.seqDireccion, rTpr.indEnvioCartola, rTpr.tipoCartola, rTpr.numSolicitudAut, rTpr.numSolicitudUltModif, rTpr.tipoOrigenProducto, rTpr.indFuturoFinProducto, rTpr.tipoFinProducto, rTpr.fecFinProducto, rTpr.tipoFondoRecaudacion, rTpr.valMlSaldoEmbargo, rTpr.fecEmbargo, rTpr.codRegTributario, rTpr.fecRegTributario, rTpr.tipoEleccionFondos, rTpr.fecEleccionFondos, rTpr.estadoProducto, rTpr.fecEstadoProducto, usu, fun)
        ds = InformacionCliente.traerProductoVig(dbc, idAdm, cf.idCliente, cf.tipoProducto, cf.tipoFondoDestino)

        If ds.Tables(0).Rows.Count = 0 Then
            rPro = New AAA.ccProductos(ds.Tables(0).NewRow)
        Else
            rPro = New AAA.ccProductos(ds.Tables(0).Rows(0))
        End If

        rPro.tipoCuenta = cf.tipoCuenta
        rPro.numSolicitudUltModif = cf.numSolicitudAut
        rPro.indBloqueo = "N"
        rPro.codBloqueo = Nothing
        rPro.fecInicioBloqueo = Nothing

        Sys.Kernel.Producto.modificar(dbc, idAdm, rPro.idCliente, rPro.numProducto, rPro.tipoProducto, rPro.tipoFondo, rPro.numTipoProducto, rPro.indBloqueo, rPro.codBloqueo, rPro.fecInicioBloqueo, rPro.fecFinBloqueo, rPro.fecAperturaProducto, rPro.fecCierreProducto, rPro.numSolicitudAut, rPro.numSolicitudUltModif, rPro.tipoCuenta, usu, fun)

        ds = InformacionCliente.traerDistribucion(dbc, idAdm, cf.idCliente, rPro.numProducto)
        If ds.Tables(0).Rows.Count = 0 Then
            rDis = New AAA.ccDistribuciones(ds.Tables(0).NewRow)
        Else
            rDis = New AAA.ccDistribuciones(ds.Tables(0).Rows(0))
        End If

        rDis.tipoCuenta = cf.tipoCuenta

        'leo: se agrega validacion, el % se informa en cero, se debe conservar el original
        'rDis.porcDistribucion = cf.porcDistribucionDestino
        If cf.tipoDistribucion = "CFR" Then
            rDis.porcDistribucion = rDis.porcDistribucion
        Else
            rDis.porcDistribucion = cf.porcDistribucionDestino
        End If

        rDis.tipoDistribucion = cf.tipoDistribucion
        rDis.numSolicitud = cf.numSolicitudAut
        '--.-- porcentaje recaudación
        rDis.porcDistribucionReca = cf.porcRecaudacion
        Sys.Kernel.Producto.modificarDistribucion(dbc, idAdm, rDis.idCliente, rDis.numProducto, rDis.seqDistribucion, rDis.tipoProducto, rDis.tipoFondo, rDis.tipoFondoAnterior, rDis.tipoFondoAnterior2, rDis.numTipoProducto, rDis.porcDistribucion, rDis.porcDistribucionReca, rDis.numSolicitud, rDis.fecAperturaDistribucion, rDis.fecCierreDistribucion, rDis.tipoDistribucion, rDis.fecConvenioDistribucion, rDis.tipoCuenta, usu, fun)

    End Sub

    Private Shared Sub CargarProductosVigentes(ByVal ds As DataSet, ByRef P() As Boolean, ByRef num As Integer)
        Dim i As Integer
        Dim cSal As AAA.ccSaldos
        For i = 0 To 4
            P(i) = False
        Next
        num = 0
        For i = 0 To ds.Tables(0).Rows.Count - 1
            cSal = New AAA.ccSaldos(ds.Tables(0).Rows(i))
            Select Case cSal.tipoFondo
                Case "A" : If Not P(0) Then num = num + 1 : P(0) = True
                Case "B" : If Not P(1) Then num = num + 1 : P(1) = True
                Case "C" : If Not P(2) Then num = num + 1 : P(2) = True
                Case "D" : If Not P(3) Then num = num + 1 : P(3) = True
                Case "E" : If Not P(4) Then num = num + 1 : P(4) = True
            End Select
        Next
    End Sub
    Private Shared Function quedaraConMasDeDosFondos(ByVal P() As Boolean, ByVal tipoFondoDestino As String) As Boolean
        Dim num As Integer = 0
        Dim i As Integer
        Dim Q(4) As Boolean

        Q(0) = tipoFondoDestino = "A" Or P(0)
        Q(1) = tipoFondoDestino = "B" Or P(1)
        Q(2) = tipoFondoDestino = "C" Or P(2)
        Q(3) = tipoFondoDestino = "D" Or P(3)
        Q(4) = tipoFondoDestino = "E" Or P(4)

        For i = 0 To 4
            If Q(i) Then
                num += 1
            End If
        Next
        Return (num > 2)

    End Function

    Private Shared Sub AbrirCerrarProducto(ByRef P() As Boolean, ByVal fondo As String, ByVal accion As String)
        Select Case fondo
            Case "A" : P(0) = (accion = "A")
            Case "B" : P(1) = (accion = "A")
            Case "C" : P(2) = (accion = "A")
            Case "D" : P(3) = (accion = "A")
            Case "E" : P(4) = (accion = "A")
        End Select
    End Sub

    Private Shared Sub DeterminaCodMovimiento(ByVal tipoProducto As String, ByVal tipoDistribucion As String, ByRef codMvtoAbo As String, ByRef codMvtoCar As String)

        Select Case tipoProducto
            Case "CCO" : codMvtoAbo = "110" : codMvtoCar = "120"
            Case "CAV" : codMvtoAbo = "210" : codMvtoCar = "220"
            Case "CAI" : codMvtoAbo = "310" : codMvtoCar = "320"
            Case "CCV" : codMvtoAbo = "410" : codMvtoCar = "420"
            Case "CDC" : codMvtoAbo = "510" : codMvtoCar = "520"
            Case "CAF" : codMvtoAbo = "610" : codMvtoCar = "620"
            Case "CVC" : codMvtoAbo = "710" : codMvtoCar = "720"
        End Select

        Select Case tipoDistribucion
            Case "CFA" : codMvtoAbo = codMvtoAbo & "903" : codMvtoCar = codMvtoCar & "902"
            Case "CFN" : codMvtoAbo = codMvtoAbo & "900" : codMvtoCar = codMvtoCar & "901"
            Case "CFD" : codMvtoAbo = codMvtoAbo & "905" : codMvtoCar = codMvtoCar & "904"
            Case "CFE" : codMvtoAbo = codMvtoAbo & "905" : codMvtoCar = codMvtoCar & "904"
            Case "CFT" : codMvtoAbo = codMvtoAbo & "905" : codMvtoCar = codMvtoCar & "905"
            Case "CFP" : codMvtoAbo = codMvtoAbo & "905" : codMvtoCar = codMvtoCar & "904" ' FRAG 230909 AJUPER
        End Select
    End Sub
    <WebMethod()> Public Function wmProcesoBatch(ByVal idAdm As Integer, ByVal seqProceso As Integer, ByVal seqEtapa As Integer, ByVal ds As DataSet, ByVal usu As String, ByVal fun As String)

        Dim log As New Procesos.logEtapa()
        Dim i As Integer
        Dim tipoProceso As String
        Dim fecProceso As Date
        Dim numSolicitud As Integer
        Dim tipoCambio As String
        Dim indActualizacion As String
        Dim dsArchivo As DataSet
        Dim dbc As New OraConn()

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
            Select Case ds.Tables(0).Rows(0).Item("ID_ETAPA")
                Case "ETAPA1"

                    log = New Procesos.logEtapa(idAdm, seqProceso, seqEtapa)
                    Dim ds2 As DataSet
                    Dim cCon As ACR.ccControlAcreditacion

                    log = New Procesos.logEtapa(idAdm, seqProceso, seqEtapa)
                    evento(log, "Inicio de proceso")
                    log.FecHoraInicio = Now
                    evento(log, "Se inicia la etapa " & ds.Tables(0).Rows(0).Item("ID_ETAPA"))

                    ds2 = ControlAcr.buscarProcesoSimulacion(dbc, idAdm, "CAMBFOND", Nothing)
                    dbc.BeginTrans()

                    If ds2.Tables(0).Rows.Count = 0 Then
                        evento(log, " ")
                    End If

                    For i = 0 To ds2.Tables(0).Rows.Count - 1
                        cCon = New ACR.ccControlAcreditacion(ds2.Tables(0).Rows(i))
                        ControlAcr.modEstadoProceso(dbc, idAdm, cCon.codOrigenProceso, cCon.idUsuarioProceso, cCon.numeroId, "NU", usu, fun)
                        evento(log, "Simulación número " & cCon.numeroId & " en estado " & ds2.Tables(0).Rows(i).Item("ESTADO_PROCESO") & " ha sido anulada")
                    Next
                    dbc.Commit()
                    fecProceso = ds.Tables(0).Rows(0).Item("FEC_PROCESO")
                    numSolicitud = ds.Tables(0).Rows(0).Item("NUM_SOLICITUD")
                    tipoCambio = ds.Tables(0).Rows(0).Item("TIPO_CAMBIO")

                    If tipoCambio = "00001" Then
                        Dim dsErr As New DataSet()
                        dsErr = CreaSolAjustePeridico(idAdm, fecProceso, usu, fun, log)
                        If dsErr.Tables(0).Rows.Count > 0 Then
                            Dim err As String
                            err = IIf(IsDBNull(dsErr.Tables(0).Rows(0).Item(0)), Nothing, dsErr.Tables(0).Rows(0).Item(0))
                            If Not err Is Nothing Then Throw New Exception(err)
                        End If
                    End If

                    enviarAcreditacion(idAdm, fecProceso, tipoCambio, numSolicitud, usu, fun, log)

                Case "GENARCHIVO"

                    Dim periodo As Date = ds.Tables(0).Rows(0).Item("PER_PROCESO")
                    Dim mensaje As String

                    log = New Procesos.logEtapa(idAdm, seqProceso, seqEtapa)
                    evento(log, "Inicio de proceso")
                    log.FecHoraInicio = Now
                    evento(log, "Se inicia la etapa " & ds.Tables(0).Rows(0).Item("ID_ETAPA"))
                    evento(log, "Generación de archivos para el periodo " & periodo.Month & "/" & periodo.Year)
                    evento(log, "Movimientos Internos de Fondos (Anexos 12.G, 12.H, 12.I)")
                    evento(log, "Generando el archivo 123" & Format(periodo, "yyyyMM") & ".txt")
                    GenerarArchivo(dbc, idAdm, periodo, "CFN", mensaje, log)
                    evento(log, mensaje)

                    'Comentado por JCO 23/04/2009, segun oficio 373
                    'evento(log, "Generando el archivo CT07" & Format(periodo, "yyMM") & " - Cambios de fondo ...")
                    'GenerarArchivo(dbc, idAdm, periodo, "CFN", mensaje, log)
                    'evento(log, mensaje)
                    'evento(log, "Generando el archivo CT08" & Format(periodo, "yyMM") & " - Asignación de fondo ...")
                    'GenerarArchivo(dbc, idAdm, periodo, "CFA", mensaje, log)
                    'evento(log, mensaje)
                    'evento(log, "Generando el archivo CT09" & Format(periodo, "yyMM") & " - Distribuciones ...")
                    'GenerarArchivo(dbc, idAdm, periodo, "CFD", mensaje, log)
                    'evento(log, mensaje)

                    utilCopiar = New FTPUtil.Copiador()

                    ip = traerString(strFTPUtil, ";", "=")
                    usuario = traerString(strFTPUtil, ";", "=")
                    password = traerString(strFTPUtil, ";", "=")
                    port = traerString(strFTPUtil, ";", "=")

                    pathOrigen = traerString(strFTPUtil, Nothing, "=")
                    pathDest = Sys.Soporte.Archivo.traer(dbc, idAdm, "ACRCT07").Tables(0).Rows(0).Item("RUTA") & "\"

                    evento(log, "Generando el archivo anexo13CambioFondo" & Format(periodo, "yyyyMM") & " ...")

                    'crea anexo 13
                    CambioFondo.Archivos.archivoAnexo13(dbc, idAdm, periodo)
                    evento(log, "Archivo correctamente generado en el servidor de datos " & pathOrigen)
                    evento(log, "Copiando el archivo generado en la ruta " & pathDest)

                    'copiar archivo anexo13_CF.....
                    Archivo = "anexo13_CF" & Format(periodo, "yyyyMM") & ".txt"
                    If utilCopiar.Traer(ip, usuario, password, pathOrigen, port, Archivo, pathDest, errMsg) = False Then
                        evento(log, errMsg)
                        log.estado = Procesos.Estado.ConError
                        evento(log, "Proceso ha finalizado con error")
                    End If

                    'copiar archivo anexo13_DF.....
                    Archivo = "anexo13_DF" & Format(periodo, "yyyyMM") & ".txt"
                    If utilCopiar.Traer(ip, usuario, password, pathOrigen, port, Archivo, pathDest, errMsg) = False Then
                        evento(log, errMsg)
                        log.estado = Procesos.Estado.ConError
                        evento(log, "Proceso ha finalizado con error")
                    End If

                Case "ETAREA1"

                    fecProceso = ds.Tables(0).Rows(0).Item("PER_PROCESO")
                    tipoProceso = ds.Tables(0).Rows(0).Item("TIPO_PROCESO")
                    extracionEtarea(dbc, idAdm, fecProceso, tipoProceso, seqProceso, seqEtapa, usu, fun)

                Case "AUDITMFS"

                    log = New Procesos.logEtapa(idAdm, seqProceso, seqEtapa)
                    evento(log, "Inicio de proceso")
                    log.FecHoraInicio = Now
                    evento(log, "Se inicia la etapa " & ds.Tables(0).Rows(0).Item("ID_ETAPA") & " " & indActualizacion)

                    dbc.BeginTrans()

                    indActualizacion = ds.Tables(0).Rows(0).Item("TIPO_CAMBIO")

                    dsArchivo = Sys.IngresoEgreso.CambioFondo.cabecera.auditorMultifondos(dbc, idAdm, indActualizacion, seqProceso, seqEtapa)
                    dbc.Commit()

                    utilCopiar = New FTPUtil.Copiador()

                    ip = traerString(strFTPUtil, ";", "=")
                    usuario = traerString(strFTPUtil, ";", "=")
                    password = traerString(strFTPUtil, ";", "=")
                    port = traerString(strFTPUtil, ";", "=")

                    pathOrigen = traerString(strFTPUtil, Nothing, "=")
                    pathDest = Sys.Soporte.Archivo.traer(dbc, idAdm, "ACRAUDMF").Tables(0).Rows(0).Item("RUTA") & "\"

                    evento(log, "Archivo correctamente generado en el servidor de datos " & pathOrigen)

                    evento(log, "Copiando el archivo generado en la ruta " & pathDest)

                    evento(log, "Nombre Archivo " & dsArchivo.Tables(0).Rows(0).Item("NOMBRE_ARCHIVO"))

                    If Not utilCopiar.Traer(ip, usuario, password, pathOrigen, port, dsArchivo.Tables(0).Rows(0).Item("NOMBRE_ARCHIVO"), pathDest, errMsg) Then
                        evento(log, errMsg)
                        log.estado = Procesos.Estado.ConError
                        evento(log, "Proceso ha finalizado con error")
                    End If

                    evento(log, "Proceso Terminado " & ds.Tables(0).Rows(0).Item("ID_ETAPA"))

            End Select

            If Not IsNothing(log) And Not IsNothing(errMsg) Then
                log.estado = Procesos.Estado.Exitoso
                evento(log, "finalizacion exitosa del proceso")
                log.fecHoraFin = Now

            End If

        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)

        Finally
            dbc.Close()
        End Try

    End Function

    'FRAG 230909
    Public Shared Function CreaSolAjustePeridico(ByVal idAdm As Integer, ByVal fecProceso As Date, ByVal usu As String, ByVal fun As String, Optional ByVal log As Procesos.logEtapa = Nothing) As DataSet
        Dim dbc As OraConn
        Dim ds As DataSet

        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            CreaSolAjustePeridico = CambioFondo.cabecera.NewSolAjustePeridico(dbc, idAdm, fecProceso, usu, fun)


        Catch ex As SondaException
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(ex)
        Catch ex As Exception
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(ex)
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


    <WebMethod()> Public Function buscarDistSolicitudes(ByVal idAdm As Integer, ByVal fecProceso As Date, ByVal tipoCambio As String, ByVal numSolicitudAut As Integer) As DataSet
        Dim dbc As New OraConn()
        Try
            buscarDistSolicitudes = CambioFondo.cabecera.buscarDistSolicitudes(dbc, idAdm, fecProceso, tipoCambio, numSolicitudAut)
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function

    <WebMethod()> Public Function buscarDistSolicitudesM(ByVal idAdm As Integer, ByVal fecProceso As Date, ByVal tipoCambio As String, ByVal numSolicitudAut As Integer) As DataSet
        Dim dbc As New OraConn()
        Try
            buscarDistSolicitudesM = CambioFondo.cabecera.buscarDistSolicitudesM(dbc, idAdm, fecProceso, tipoCambio, numSolicitudAut)
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function

    <WebMethod()> Public Function buscarDistSolicitudesRut(ByVal idAdm As Integer, ByVal fecProceso As Date, ByVal tipoCambio As String, ByVal idPersona As String) As DataSet
        Dim dbc As New OraConn()
        Try
            buscarDistSolicitudesRut = CambioFondo.cabecera.buscarDistSolicitudesRut(dbc, idAdm, fecProceso, tipoCambio, idPersona)
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

    Private Shared Function ignorarSolicitud(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal LOG As Procesos.logEtapa, ByVal mensaje1 As String, ByVal mensaje2 As String, ByVal SError As Integer, ByVal ccf As ACR.ccMovCambiosFondos, ByVal numeroId As Integer, ByVal usu As String, ByVal fun As String) As Integer
        dbc.Rollback()
        If Not IsNothing(mensaje1) Then
            evento(LOG, mensaje1)
        End If
        If Not IsNothing(mensaje2) Then
            evento(LOG, mensaje2)
        End If
        If SError > 0 Then
            evento(LOG, ControlAcr.LogAcreditacion.obtenerSondaException(dbc, SError))
        End If

        evento(LOG, "Solicitud número " & ccf.numSolicitudAut & "- Id_Cliente (" & ccf.idCliente & ") ha sido ignorada")
        ignorarSolicitud = Sys.IngresoEgreso.CambioFondo.cabecera.modEstadoCambio(dbc, idAdm, ccf.fecCambio, ccf.idCliente, ccf.tipoDistribucion, ccf.numSolicitudAut, "RE", numeroId, usu, fun) 'RECHAZADA

        ''' agregado por IRM 09-06-2006
        If ccf.tipoDistribucion = "CFN" Or ccf.tipoDistribucion = "CFD" Then ' FRAG 230909 AJUPER
            Dim dsSolicitud As DataSet
            dsSolicitud = Adm.Sys.AdministracionClientes.Solicitud.traer(dbc, idAdm, ccf.numSolicitudAut)
            Dim ccAdmSolicitud As New Sys.CodeCompletion.ADM.ccSolicitudes(dsSolicitud)
            Sys.Soporte.Etapa.crear(dbc, idAdm, ccAdmSolicitud.seqSolicitudSol, IIf(ccf.tipoDistribucion = "CFN", "SCFE04", "SDFE04"), usu, ccf.fecCambio, 0, "T", usu, fun)
        End If
    End Function

    Public Shared Sub GenerarArchivo(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal periodo As Date, ByVal tipoDistribucion As String, ByRef mensaje As String, Optional ByRef LOG As Procesos.logEtapa = Nothing)
        Dim ds As DataSet
        Dim ruta As String
        Dim linea As String
        Dim i As Integer
        Dim origen, destino As String
        Dim montoNeto As Decimal
        Dim signoMontoNeto As String

        mensaje = Nothing

        'Comentado por JCO, segun oficio 373
        'ds = CambioFondo.Archivos.archivoAnexo12RegId(dbc, idAdm, periodo, tipoDistribucion)
        'If ds.Tables(0).Rows(0).Item("CANTIDAD") = 0 Then
        '    mensaje = "No se encontraron registros para generar el archivo"
        '    Exit Sub
        'End If

        ruta = abrirArchivo(dbc, idAdm, periodo, tipoDistribucion, 1)

        'linea = String.Empty
        'linea &= ds.Tables(0).Rows(0).Item("COD_ADM").ToString.PadRight(2, " ")
        'linea &= ds.Tables(0).Rows(0).Item("MES_TRASPASO").ToString.PadRight(6, " ")
        'linea &= ds.Tables(0).Rows(0).Item("CANTIDAD").ToString.PadLeft(7, "0")
        'linea = linea.PadRight(128, " ")
        ''Insertar linea
        'PrintLine(1, linea)

        'ds = CambioFondo.Archivos.archivoAnexo12RegDatos(dbc, idAdm, periodo, tipoDistribucion)
        'For i = 0 To ds.Tables(0).Rows.Count - 1
        '    linea = String.Empty

        '    If ds.Tables(0).Rows(i).Item("TIPO_FONDO_ORIGEN") = "Z" Then
        '        ds.Tables(0).Rows(i).Item("TIPO_FONDO_ORIGEN") = "#"
        '    End If
        '    If ds.Tables(0).Rows(i).Item("TIPO_FONDO_DESTINO") = "Z" Then
        '        ds.Tables(0).Rows(i).Item("TIPO_FONDO_DESTINO") = "#"
        '    End If

        '    linea &= ds.Tables(0).Rows(i).Item("TIPO_FONDO_ORIGEN")
        '    linea &= ds.Tables(0).Rows(i).Item("TIPO_FONDO_DESTINO")

        '    linea &= ds.Tables(0).Rows(i).Item("CANTIDAD_CCO").ToString.PadLeft(7, "0")
        '    linea &= ds.Tables(0).Rows(i).Item("VAL_ML_MONTO_CCO").ToString.PadLeft(14, "0")

        '    linea &= ds.Tables(0).Rows(i).Item("CANTIDAD_CCV").ToString.PadLeft(7, "0")
        '    linea &= ds.Tables(0).Rows(i).Item("VAL_ML_MONTO_CCV").ToString.PadLeft(14, "0")

        '    linea &= ds.Tables(0).Rows(i).Item("CANTIDAD_CDC").ToString.PadLeft(7, "0")
        '    linea &= ds.Tables(0).Rows(i).Item("VAL_ML_MONTO_CDC").ToString.PadLeft(14, "0")

        '    linea &= ds.Tables(0).Rows(i).Item("CANTIDAD_CAV").ToString.PadLeft(7, "0")
        '    linea &= ds.Tables(0).Rows(i).Item("VAL_ML_MONTO_CAV").ToString.PadLeft(14, "0")

        '    linea &= ds.Tables(0).Rows(i).Item("CANTIDAD_CAI").ToString.PadLeft(7, "0")
        '    linea &= ds.Tables(0).Rows(i).Item("VAL_ML_MONTO_CAI").ToString.PadLeft(14, "0")

        '    linea &= ds.Tables(0).Rows(i).Item("CANTIDAD_TODOS").ToString.PadLeft(7, "0")
        '    linea &= ds.Tables(0).Rows(i).Item("VAL_ML_MONTO_TODOS").ToString.PadLeft(14, "0")

        '    'Insertar linea
        '    PrintLine(1, linea)
        'Next

        'agregado por JCO 23/04/2009, segun oficio 373
        ds = CambioFondo.Archivos.archivoAnexo12RegDatos(dbc, idAdm, periodo, Nothing)
        For i = 0 To ds.Tables(0).Rows.Count - 1
            linea = String.Empty

            linea &= ds.Tables(0).Rows(i).Item("COD_ADM_ALFA").ToString.PadLeft(2, "0")
            linea &= ds.Tables(0).Rows(i).Item("MES_TRASPASO").ToString.PadLeft(6, "0")
            linea &= ds.Tables(0).Rows(i).Item("TIPO_OPERACION").ToString.PadLeft(1, "0")
            linea &= ds.Tables(0).Rows(i).Item("TIPO_CUENTA").ToString.PadLeft(1, "0")
            linea &= ds.Tables(0).Rows(i).Item("TIPO_FONDO_ORIGEN").ToString.PadLeft(1, "0")
            linea &= ds.Tables(0).Rows(i).Item("TIPO_FONDO_DESTINO").ToString.PadLeft(1, "0")
            linea &= ds.Tables(0).Rows(i).Item("TOTAL_CTAS").ToString.PadLeft(9, "0")
            linea &= ds.Tables(0).Rows(i).Item("TOT_ML_FONDO").ToString.PadLeft(15, "0")

            'Insertar linea
            PrintLine(1, linea)
        Next

        'ds = CambioFondo.Archivos.archivoAnexo12RegNetos(dbc, idAdm, periodo, tipoDistribucion)
        'For i = 0 To ds.Tables(0).Rows.Count - 1
        '    linea = "X"
        '    linea &= ds.Tables(0).Rows(i).Item("TIPO_FONDO")
        '    montoNeto = ds.Tables(0).Rows(i).Item("VAL_ML_MONTO_NETO")
        '    If montoNeto >= 0 Then
        '        signoMontoNeto = "+"
        '    Else
        '        signoMontoNeto = "-"
        '        montoNeto = Decimal.Negate(montoNeto)
        '    End If
        '    linea &= montoNeto.ToString.PadLeft(14, "0")
        '    linea &= signoMontoNeto

        '    linea = linea.PadRight(128, " ")
        '    'Insertar linea
        '    PrintLine(1, linea)
        'Next
        mensaje = "Archivo correctamente generado en la ruta " & ruta
        FileClose(1)

    End Sub
    Private Shared Function abrirArchivo(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal periodo As Date, ByVal tipoDistribucion As String, ByVal numFile As Integer) As String

        Dim Path, Archivo, sArchivo, fileName As String

        Select Case tipoDistribucion
            Case "CFN" : fileName = "CT07"
            Case "CFA" : fileName = "CT08"
            Case "CFD" : fileName = "CT09"
        End Select

        Path = Sys.Soporte.Archivo.traer(dbc, idAdm, "ACR" & fileName).Tables(0).Rows(0).Item("RUTA")
        Archivo = "\" & "123" & Format(periodo, "yyyyMM") & ".TXT"
        sArchivo = Path & Archivo

        'Borra el Arhcivo si Existe
        If Dir(sArchivo) <> "" Then
            Kill(sArchivo)
        End If
        FileOpen(numFile, sArchivo, OpenMode.Output, OpenAccess.Write)
        abrirArchivo = Path

    End Function


    Private Sub extracionEtarea(ByVal dbc As OraConn, ByVal idAdm As Integer, ByVal fecProceso As Date, ByVal tipoProceso As String, ByVal seqProceso As Integer, ByVal seqEtapa As Integer, ByVal usu As String, ByVal fun As String)
        Try
            dbc.BeginTrans()
            CambioFondo.cabecera.asignacionFondoEtareo(dbc, idAdm, fecProceso, tipoProceso, seqProceso, seqEtapa, usu, fun)
            dbc.Commit()
        Catch e As SondaException
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(e)
        End Try
    End Sub


    <WebMethod()> _
    Public Function wmGenerarAnexosCambioFondo(ByVal idAdm As Integer, ByVal fecDesde As Date, ByVal fecHasta As Date, ByVal nomAfp As String) As String
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            wmGenerarAnexosCambioFondo = INECambioFondoAnexo.generarAnexos(dbc, idAdm, fecDesde, fecHasta, nomAfp)

        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function


    <WebMethod()> _
    Public Function wmBuscarMontosAnexos(ByVal idAdm As Integer, ByVal fecProceso As Date) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            wmBuscarMontosAnexos = CambioFondo.Archivos.buscarMontosAnexo(dbc, idAdm, fecProceso)

        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function


    <WebMethod()> _
        Public Sub wmModificarMontosAnexos(ByVal idAdm As Integer, ByVal ds As DataSet, ByVal usu As String, ByVal fun As String)
        Dim dbc As OraConn
        Dim i As Integer
        Dim fecProceso As Date
        Dim valMonto As Long
        Dim numReg As Long
        Dim tipoFondo As String

        Try
            dbc = New OraConn()
            dbc.BeginTrans()
            If ds.Tables(0).Rows.Count = 0 Then Exit Sub
            For i = 0 To ds.Tables(0).Rows.Count - 1
                fecProceso = ds.Tables(0).Rows(i).Item("fec_proceso")
                tipoFondo = ds.Tables(0).Rows(i).Item("tipo_fondo")
                valMonto = ds.Tables(0).Rows(i).Item("val_ml_monto")
                numReg = ds.Tables(0).Rows(i).Item("num_registros")

                CambioFondo.Archivos.modificarMontosAnexo(dbc, idAdm, fecProceso, tipoFondo, valMonto, numReg, usu, fun)
            Next i

            dbc.Commit()
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Sub

    <WebMethod()> _
            Public Function wmCalcularViernesAnterior(ByVal idAdm As Integer, ByVal fecProceso As Date) As Date
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            wmCalcularViernesAnterior = INECambioFondoAnexo.calcularViernesAnterior(dbc, fecProceso)

        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function

    <WebMethod()> _
        Public Function wmBuscarSolicitudesIgnoradas(ByVal idAdm As Integer, ByVal codSolicitud As String, ByVal fecProceso As Date) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            wmBuscarSolicitudesIgnoradas = CambioFondo.Detalle.buscarSolicitudesIgnoradas(dbc, idAdm, codSolicitud, fecProceso)

        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function

    <WebMethod()> _
       Public Sub wmTestDistribucion(ByVal id_adm As Integer)

        Dim ds As New DataSet()
        ds.Tables.Add(0)
        ds.AcceptChanges()
        ds.Tables(0).Columns.Add("FEC_PROCESO", GetType(Date))
        ds.Tables(0).Columns.Add("NUM_SOLICITUD", GetType(Integer))
        ds.Tables(0).Columns.Add("TIPO_CAMBIO", GetType(String))
        ds.Tables(0).Columns.Add("ID_ETAPA", GetType(String))
        ds.AcceptChanges()

        'Case "10000" : tipoSolicitud = "cambio de fondo normal"
        'Case "01000" : tipoSolicitud = "distribución de fondos" 'añade CFR
        'Case "00100" : tipoSolicitud = "asignación etarea"
        'Case "00001" : tipoSolicitud = "ajueste periódico"

        'ds.Tables(0).Rows.Add(New Object() {"22/09/2009", 92759371, "010", "ETAPA1"})   'NUM_SOL_PRUEBA:4670541
        ds.Tables(0).Rows.Add(New Object() {"15/10/2009", 0, "00001", "ETAPA1"})   ' FRAG 230909 AJUPER
        wmProcesoBatch(1, 226863, 1, ds, "usu", "fun")
    End Sub



End Class
