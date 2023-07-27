Option Explicit On 
Option Strict Off
Imports Sonda.Gestion.Adm.Sys.Soporte
Imports Sonda.Gestion.Adm.Sys.IngresoEgreso
Imports Sonda.Gestion.Adm.Sys
Imports Sonda.Gestion.Adm.Sys.CodeCompletion
Imports Sonda.Gestion.Adm.Sys.CodeCompletion.PAR
Imports Sonda.Gestion.Adm.Sys.CodeCompletion.ACR
Imports Sonda.Gestion.Adm.Sys.IngresoalFondo
Imports Sonda.Net.DB
Imports Sonda.Net
Imports System.Threading
Imports Sonda.Gestion.Adm.Sys.sysRetirosSap.AcreditaRetiro3
Imports System.Runtime.Serialization
Imports System.IO
Imports System.Configuration

Public Structure paramDatos
    Dim idAdm As Integer
    Dim NumeroId As Integer
    Dim TipoProceso As String
    Dim CodOrigenProceso As String
    Dim idUsuarioProceso As String
    Dim dsHebra As DataSet
    Dim idHebra As Integer
    Dim usu As String
    Dim fun As String
    Dim LOG As Procesos.logEtapa
    Dim seqSolSolicitud As Integer
    Dim seqSolEtapa As Integer
    Dim SeqLog As Integer
    Dim SeqLog2 As Integer
    Dim CantHebras As Integer

    Public Sub New(ByVal idAdm As Integer, ByVal NumeroId As Integer, ByVal TipoProceso As String, ByVal CodOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal ds As DataSet, ByVal usu As String, ByVal fun As String, Optional ByVal seqSolSolicitud As Integer = 0, Optional ByVal seqSolEtapa As Integer = 0)
        Me.idAdm = idAdm
        Me.NumeroId = NumeroId
        Me.TipoProceso = TipoProceso
        Me.CodOrigenProceso = CodOrigenProceso
        Me.idUsuarioProceso = idUsuarioProceso
        Me.dsHebra = ds
        Me.usu = usu
        Me.fun = fun
        Me.seqSolSolicitud = seqSolSolicitud
        Me.seqSolEtapa = seqSolEtapa
    End Sub
End Structure

Public Class INEControlAcr
    Dim gidAdm As Integer

    Public Shared gSeqLog2 As Integer

    Public Shared Function obtenerCuotaAcreditacion(ByVal idAdm As Integer, ByVal tipoFondo As String) As DataSet
        Dim ParINE As New wsINEParametros()
        Dim fecCuota As Object
        Dim fecAcreditacion As Object
        Dim NumDias As Integer
        Dim dsFechaACR As DataSet
        Dim dbc As OraConn
        Dim ccFecAcr As PAR.ccFechaAcreditacion
        Try
            dbc = New OraConn()
            dbc.BeginTrans()
            dsFechaACR = ParINE.wmTraerFechaAcreditacion(idAdm)
            If dsFechaACR.Tables(0).Rows.Count = 0 Then
                Throw New SondaException(15002) '"No se ha encontrado fecha de acreditación")
            Else
                ccFecAcr = New PAR.ccFechaAcreditacion(dsFechaACR)
                fecAcreditacion = ccFecAcr.fecAcreditacion
                NumDias = ccFecAcr.numDiasValorCuota
                NumDias = -1 * (NumDias + 1)

                fecCuota = Sys.Soporte.Fecha.contarhabiles(dbc, fecAcreditacion, NumDias)
                obtenerCuotaAcreditacion = Sys.Soporte.Parametro.obtenerValorCuotasFondos(dbc, idAdm, fecCuota, tipoFondo, 0, 1)
                dbc.Commit()
            End If
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

    Public Shared Sub crearProcesoAcreditacion(ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal numRefOrigen1 As Long, ByVal numRefOrigen2 As Long, ByVal tipoAcreditacion As String, ByVal usu As String, ByVal fun As String, ByRef numeroId As Integer, ByRef fecCreacion As Object, ByRef codError As Integer)
        Dim dbc As OraConn
        Dim ds As DataSet
        Try
            fecCreacion = Now : numeroId = -1 : codError = 15008 'no se pudo crear

            dbc = New OraConn()
            dbc.BeginTrans()

            ds = New DataSet()
            ds = ControlAcr.crearProcesoAcreditacion(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numRefOrigen1, numRefOrigen2, tipoAcreditacion, usu, fun)


            codError = Val(ds.Tables(0).Rows(0).Item("CODE_ERROR"))
            If codError = 0 Then
                numeroId = ds.Tables(0).Rows(0).Item("NUMERO_ID") ' EXITOSO
                fecCreacion = ds.Tables(0).Rows(0).Item("FEC_CREACION")
            End If

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

    Public Shared Sub crearProcesoAcreditacion(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal numRefOrigen1 As Long, ByVal numRefOrigen2 As Long, ByVal tipoAcreditacion As String, ByVal usu As String, ByVal fun As String, ByRef numeroId As Integer, ByRef fecCreacion As Object, ByRef codError As Integer)
        Dim ds As DataSet
        Try
            fecCreacion = Now : numeroId = -1 : codError = 15008 'no se pudo crear


            ds = New DataSet()
            ds = ControlAcr.crearProcesoAcreditacion(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numRefOrigen1, numRefOrigen2, tipoAcreditacion, usu, fun)


            codError = Val(ds.Tables(0).Rows(0).Item("CODE_ERROR"))
            If codError = 0 Then
                numeroId = ds.Tables(0).Rows(0).Item("NUMERO_ID") ' EXITOSO
                fecCreacion = ds.Tables(0).Rows(0).Item("FEC_CREACION")
            End If

        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        End Try
    End Sub


    '--.-- modif agrega campos subCategoria,codregTributario--
    ''--.-- cn2
    Public Shared Sub crearTransacciones(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal usuarioProceso As String, ByVal numeroId As Integer, ByVal idPersona As String, ByVal idCliente As Integer, ByVal apPaterno As String, ByVal apMaterno As String, ByVal nombre As String, ByVal nombreAdicional As String, ByVal codSoundex As String, ByVal perCotizacion As Object, ByVal numReferenciaOrigen1 As Long, ByVal numReferenciaOrigen2 As Long, ByVal numReferenciaOrigen3 As Integer, ByVal numReferenciaOrigen4 As Integer, ByVal numReferenciaOrigen5 As Long, ByVal numReferenciaOrigen6 As Long, ByVal tipoRemuneracion As String, ByVal tipoPago As String, ByVal tipoPlanilla As String, ByVal tipoEntidadPagadora As String, ByVal tipoCliente As String, ByVal fecInicioGratificacion As Object, ByVal fecFinGratificacion As Object, ByVal numPeriodosCai As Integer, ByVal fecOperacion As Object, ByVal fecOperacionAdmOrigen As Object, ByVal fecDeposito As Object, ByVal idEmpleador As String, ByVal folioConvenio As Integer, ByVal idAlternativoDoc As Integer, ByVal numCuotasPactadas As Integer, ByVal numCuotasPagadas As Integer, ByVal valMlRentaImponible As Object, ByVal tipoProducto As String, ByVal tipoFondoOrigen As String, ByVal tipoFondoDestino As String, ByVal categoria As String, ByVal subCategoria As Long, ByVal codRegTributario As String, ByVal tipoImputacion As String, ByVal codOrigenTransaccion As String, ByVal codDestinoTransaccion As String, ByVal codOrigenRecaudacion As String, ByVal seqMvtoOrigen As Long, ByVal seqMvtoDestino As Integer, ByVal codOrigenMvto As String, ByVal codMvto As String, ByVal codMvtoAdi As String, ByVal codMvtoIntreaCue As String, ByVal codMvtoIntreaAdi As String, ByVal codMvtoComPor As String, ByVal codMvtoComFij As String, ByVal idInstOrigen As Integer, ByVal idInstDestino As Integer, ByVal codCausalRezago As String, ByVal tipoRezago As Integer, ByVal codCausalAjuste As String, ByVal fecValorCuotaVal As Object, ByVal valMlValorCuotaVal As Object, ByVal perContable As Object, ByVal fecAcreditacion As Object, ByVal fecValorCuota As Object, ByVal valMlValorCuota As Object, ByVal porcInstSalud As Object, ByVal valUfInstSalud As Object, ByVal tasaCotizacion As Object, ByVal tasaAdicional As Object, ByVal tasaInteres As Object, ByVal tasaReajuste As Object, ByVal indMontoPagado As String, ByVal valMlMontoNominal As Object, ByVal valMlMvto As Object, ByVal valMlReajuste As Object, ByVal valMlInteres As Object, ByVal valCuoMvto As Object, ByVal valCuoReajuste As Object, ByVal valCuoInteres As Object, ByVal valMlAdicional As Object, ByVal valMlAdicionalReajuste As Object, ByVal valMlAdicionalInteres As Object, ByVal valCuoAdicional As Object, ByVal valCuoAdicionalReajuste As Object, ByVal valCuoAdicionalInteres As Object, ByVal tipoComisionPorcentual As String, ByVal valMlComisPorcentual As Object, ByVal valCuoComisPorcentual As Object, ByVal valMlCuotaComision As Object, ByVal tipoComisionFija As String, ByVal valMlComisFija As Object, ByVal valCuoComisFija As Object, ByVal tipoImputacionAdm As String, ByVal valMlAporteAdm As Object, ByVal valCuoAporteAdm As Object, ByVal idInstSalud As Integer, ByVal valMlSalud As Object, ByVal valCuoSalud As Object, ByVal valCuoTransferencia As Object, ByVal valMlTransferencia As Object, ByVal valMlExcesoLinea As Object, ByVal valCuoExcesoLinea As Object, ByVal numRetiros As Integer, ByVal codAjusteMovimiento As String, ByVal indInsistenciaAcr As String, ByVal indCierreProducto As String, ByVal indMvtoVisibleCartola As String, ByVal perCuatrimestre As Object, ByVal numDictamen As String, ByVal puestoTrabajoPesado As String, ByVal sexo As String, ByVal codMvtoPrim As String, ByVal codMvtoIntreaPrim As String, ByVal tasaPrima As Decimal, ByVal valMlPrimaSis As Decimal, ByVal valMlPrimaSisReajuste As Decimal, ByVal valMlPrimaSisInteres As Decimal, ByVal valCuoPrimaSis As Decimal, ByVal valCuoPrimaSisReajuste As Decimal, ByVal valCuoPrimaSisInteres As Decimal, ByVal valMlRentaImponibleSis As Decimal, ByVal usu As String, ByVal fun As String, ByRef seqRegistro As Integer, ByRef CodError As Integer)

        Dim ds As DataSet
        Try
            seqRegistro = 0 : CodError = 15200 ' no fue posible crear en acr_transacciones

            ds = ControlAcr.crearTransacciones(dbc, idAdm, codOrigenProceso, usuarioProceso, numeroId, idPersona, idCliente, apPaterno, apMaterno, nombre, nombreAdicional, codSoundex, perCotizacion, numReferenciaOrigen1, numReferenciaOrigen2, numReferenciaOrigen3, numReferenciaOrigen4, numReferenciaOrigen5, numReferenciaOrigen6, tipoRemuneracion, tipoPago, tipoPlanilla, tipoEntidadPagadora, tipoCliente, fecInicioGratificacion, fecFinGratificacion, numPeriodosCai, fecOperacion, fecOperacionAdmOrigen, fecDeposito, idEmpleador, folioConvenio, idAlternativoDoc, numCuotasPactadas, numCuotasPagadas, valMlRentaImponible, tipoProducto, tipoFondoOrigen, tipoFondoDestino, categoria, subCategoria, codRegTributario, tipoImputacion, codOrigenTransaccion, codDestinoTransaccion, codOrigenRecaudacion, seqMvtoOrigen, seqMvtoDestino, codOrigenMvto, codMvto, codMvtoAdi, codMvtoIntreaCue, codMvtoIntreaAdi, codMvtoComPor, codMvtoComFij, idInstOrigen, idInstDestino, codCausalRezago, tipoRezago, codCausalAjuste, fecValorCuotaVal, valMlValorCuotaVal, perContable, fecAcreditacion, fecValorCuota, valMlValorCuota, porcInstSalud, valUfInstSalud, tasaCotizacion, tasaAdicional, tasaInteres, tasaReajuste, indMontoPagado, valMlMontoNominal, valMlMvto, valMlReajuste, valMlInteres, valCuoMvto, valCuoReajuste, valCuoInteres, valMlAdicional, valMlAdicionalReajuste, valMlAdicionalInteres, valCuoAdicional, valCuoAdicionalReajuste, valCuoAdicionalInteres, tipoComisionPorcentual, valMlComisPorcentual, valCuoComisPorcentual, valMlCuotaComision, tipoComisionFija, valMlComisFija, valCuoComisFija, tipoImputacionAdm, valMlAporteAdm, valCuoAporteAdm, idInstSalud, valMlSalud, valCuoSalud, valCuoTransferencia, valMlTransferencia, valMlExcesoLinea, valCuoExcesoLinea, numRetiros, codAjusteMovimiento, indInsistenciaAcr, indCierreProducto, indMvtoVisibleCartola, perCuatrimestre, numDictamen, puestoTrabajoPesado, sexo, codMvtoPrim, codMvtoIntreaPrim, tasaPrima, valMlPrimaSis, valMlPrimaSisReajuste, valMlPrimaSisInteres, valCuoPrimaSis, valCuoPrimaSisReajuste, valCuoPrimaSisInteres, valMlRentaImponibleSis, usu, fun)


            If ds.Tables(0).Rows.Count <> 0 Then
                CodError = Val(ds.Tables(0).Rows(0).Item("COD_ERROR"))
                If CodError = 0 Then
                    seqRegistro = Val(ds.Tables(0).Rows(0).Item("SEQ_REGISTRO")) ' sin error
                Else
                    seqRegistro = 0
                End If
            Else
                CodError = 15200 ' no fue posible crear en acr_transacciones
                seqRegistro = 0
            End If

        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)

        End Try
    End Sub

    Public Shared Sub CerrarProcesoAcreditacion(ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal numeroId As Integer, ByVal usu As String, ByVal fun As String, ByRef totRegCreados As Integer, ByRef fecCierre As Object, ByRef CodError As Integer)
        Dim dbc As OraConn
        Dim ds As DataSet

        fecCierre = Now : CodError = 15009 'no se pudo cerrar
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            ds = ControlAcr.cerrarProcesoAcreditacion(dbc, _
                                                   idAdm, _
                                                   codOrigenProceso, _
                                                   idUsuarioProceso, _
                                                   numeroId, usu, fun)


            CodError = ds.Tables(0).Rows(0).Item("CODE_ERROR")

            If CodError = 0 Then   ' exitoso
                totRegCreados = ds.Tables(0).Rows(0).Item("TOT_REGISTROS_CREADOS")
                fecCierre = ds.Tables(0).Rows(0).Item("FEC_CIERRE")
            End If


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

    Public Shared Sub CerrarProcesoAcreditacion(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal numeroId As Integer, ByVal usu As String, ByVal fun As String, ByRef totRegCreados As Integer, ByRef fecCierre As Object, ByRef CodError As Integer)
        Dim ds As DataSet

        fecCierre = Now : CodError = 15009 'no se pudo cerrar
        Try

            ds = ControlAcr.cerrarProcesoAcreditacion(dbc, _
                                                   idAdm, _
                                                   codOrigenProceso, _
                                                   idUsuarioProceso, _
                                                   numeroId, usu, fun)


            CodError = ds.Tables(0).Rows(0).Item("CODE_ERROR")

            If CodError = 0 Then   ' exitoso
                totRegCreados = ds.Tables(0).Rows(0).Item("TOT_REGISTROS_CREADOS")
                fecCierre = ds.Tables(0).Rows(0).Item("FEC_CIERRE")
            End If

        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        End Try
    End Sub


    Public Shared Sub ProcesarAcreditacion(ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal numeroId As Integer, ByVal tipoProceso As String, ByVal usu As String, ByVal fun As String, Optional ByVal LOG As Procesos.logEtapa = Nothing, Optional ByVal seqSolSolicitud As Integer = 0, Optional ByVal seqSolEtapa As Integer = 0)
        Dim dbc As OraConn

        Dim ParOriPro As ParametrosINE.OrigenProceso
        Dim ParControl As ParametrosINE.Control

        Dim dsOrigen As DataSet
        Dim dsControl As DataSet
        Dim dsParControl As DataSet

        Dim gCantHebras As Integer

        Dim maximoParaOrigen As Integer = 0
        Dim maximoTodosOrigenes As Integer = 0
        Dim numTrnProcesando As Integer = 0
        Dim numProProcesando As Integer = 0
        Dim regAProcesar As Integer = 0
        Dim strEstadoACR As String
        Dim strEstadoEspera As String
        Dim i As Integer
        Dim enLinea As Boolean

        Dim ccControlAcr As ACR.ccControlAcreditacion   'tabla ACR_CONTROL_ACREDITACION
        Dim ccParControl As PAR.ccAcrControl            'tabla PAR_ACR_CONTROL
        Dim ccOrigen As PAR.ccAcrOrigenProceso          'tabla PAR_ACR_ORIGEN_PROCESO

        'Dim hilo As Thread

        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            Dim dsHebras As DataSet
            evento(LOG, "Calculando Hebras necesarias para la ejecucion del proceso...")
            dsHebras = HebrasProceso.procCalculaHebrasAcr(dbc, idAdm, numeroId, Left(tipoProceso, 2), "ACR", usu, fun)
            If dsHebras.Tables(0).Rows.Count > 0 Then
                evento(LOG, "Cantidad de Hebras Calculadas: " & dsHebras.Tables(0).Rows.Count)
                gCantHebras = dsHebras.Tables(0).Rows(0).Item("CANT_HEBRAS")
            End If

            evento(LOG, "El control de acreditacion va a realizar algunas validaciones previas a la acreditacion")
            '-------------------------------------------------------------------
            '1. Validaciones generales
            '-------------------------------------------------------------------

            '1.1 verificar que  el tipo de proceso sea valido

            If tipoProceso <> "SIM" And tipoProceso <> "ACR" Then
                Throw New SondaException(15012) '"Tipo proceso erroneo
            End If

            '1.2 verificar que el origen de proceso exista

            dsOrigen = ParOriPro.traer(dbc, idAdm, codOrigenProceso)
            If dsOrigen.Tables(0).Rows.Count = 0 Then 'existe el origen de proceso
                Throw New SondaException(15005) 'Origen de proceso desconocido"
            End If
            ccOrigen = New PAR.ccAcrOrigenProceso(dsOrigen)

            '1.3 verificar que exista el proceso para el usuario y numero_id

            dsControl = ControlAcr.buscarProceso(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId)
            If dsControl.Tables(0).Rows.Count = 0 Then
                Throw New SondaException(15006) '"Proceso de acreditación no ha sido creado")
            End If
            ccControlAcr = New ACR.ccControlAcreditacion(dsControl)



            '1.4 verificar que el proceso esté cerrado (en estado "PE"=Pendiente o "ES"= Espera)
            If tipoProceso = "SIM" Then

                If Not (ccControlAcr.estadoProceso = "PE" Or _
                        ccControlAcr.estadoProceso = "SI" Or _
                        ccControlAcr.estadoProceso = "SP") Then

                    Throw New SondaException(15010)

                End If
            Else
                If Not (ccControlAcr.estadoProceso = "PE" Or _
                        ccControlAcr.estadoProceso = "SI" Or _
                        ccControlAcr.estadoProceso = "SP" Or _
                        ccControlAcr.estadoProceso = "AP") Then

                    Throw New SondaException(15010)
                End If
            End If

            strEstadoEspera = IIf(tipoProceso = "SIM", "ES", "EA")


            '--------------------------------------------------------------------------------
            '5. Validacion de Procesos Excluyentes
            '--------------------------------------------------------------------------------

            '5.1 obtenemos la cantidad maxima de transacciones para todos los origenes

            If ControlAcr.determinaProcesosExcluy(dbc, idAdm, codOrigenProceso) = "S" Then
                ControlAcr.modEstadoProceso(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, strEstadoEspera, usu, fun)
                dbc.Commit()
                evento(LOG, "Existe un proceso excluyente en ejecución")
                evento(LOG, "El proceso ha quedado en espera")
                Exit Try
            End If
            '---------------------------------------------------------------------------------

            dsParControl = ParControl.traer(dbc, idAdm)
            If dsParControl.Tables(0).Rows.Count = 0 Then
                Throw New SondaException(15013) 'No se ha en contrado el máximo de regitros permitido para la 
                'acreditación
            End If
            ccParControl = New PAR.ccAcrControl(dsParControl)


            regAProcesar = ccControlAcr.totRegistrosCreados





            '-- lfc:// comision TGR - ca-4048436-->>
            'lfc: 20/05/2049 BXH - 1622722 verificar que la fecha de acreditacionsea igual a la de parametros
            If codOrigenProceso = "ACREXBXH" Or codOrigenProceso = "ACRTGRCO" Then
                If ControlAcr.verificaLoteValorizado(dbc, idAdm, numeroId, Nothing, codOrigenProceso, "S", usu, fun) > 0 Then
                    evento(LOG, "No es posible procesar lote NúmeroID: " & numeroId & ", fue valorizado con fecha anterior y será anulado")
                    dbc.Commit()
                    Exit Try
                End If
            End If
            '--<<< lfc:// comision TGR - ca-4048436--


            enLinea = (ccControlAcr.tipoAcreditacionCreado = "ENL") And (regAProcesar <= ccParControl.numMaxAcrEnl)

            'lfc:16/08/2016 - verifica lotes sin conexion y con estado en proceso
            Transacciones.verificaLotesEnLinea(dbc, idAdm)

            If Not enLinea Then
                ControlAcr.modEstadoProceso(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, strEstadoEspera, usu, fun)
                dbc.Commit()
                evento(LOG, "El proceso se ejecutara modo batch")
                Exit Try
            End If

            '-------------------------------------------------------------------------
            '//         aquí se gatilla la acreditación para este proceso en linea   //
            '-------------------------------------------------------------------------

            ''''''Proceso de Hebras para Procesos EN LINEA
            '''''Dim dsHebra As DataSet
            ''''''Dim hilos() As HebrasAcr
            '''''Dim j As Integer

            '''''evento(LOG, "Iniciando Hebras...")
            '''''dsHebra = HebrasProceso.procTraerHebrasAcr(dbc, idAdm, numeroId, Left(tipoProceso, 2))

            '''''If dsHebra.Tables(0).Rows.Count > 0 Then
            '''''    'ReDim hilos(dsHebra.Tables(0).Rows.Count - 1)
            '''''    'For j = 0 To dsHebra.Tables(0).Rows.Count - 1
            '''''    '    hilos(j) = New HebrasAcr(idAdm, numeroId, tipoProceso, codOrigenProceso, idUsuarioProceso, dsHebra, dsHebra.Tables(0).Rows(j).Item("ID_HEBRA"), usu, fun, LOG, seqSolSolicitud, seqSolEtapa)
            '''''    'Next

            '''''    HebrasAcr.Supervisor(dbc, LOG, dsHebra, New paramDatos(idAdm, numeroId, tipoProceso, codOrigenProceso, idUsuarioProceso, dsHebra, usu, fun, seqSolSolicitud, seqSolEtapa), gCantHebras, "ENL")
            '''''    'HebrasAcr.Supervisor(dbc, LOG, dsHebra, New paramDatos(idAdm, numeroId, tipoProceso, codOrigenProceso, idUsuarioProceso, dsHebra, usu, fun, seqSolSolicitud, seqSolEtapa), gCantHebras, "EN")
            '''''    'log.estado = Procesos.Estado.Exitoso
            '''''End If


            HebrasAcr.SupervisorEnl(dbc, LOG, New paramDatos(idAdm, numeroId, tipoProceso, codOrigenProceso, idUsuarioProceso, Nothing, usu, fun, seqSolSolicitud, seqSolEtapa))

            'acreditar(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, usu, fun, LOG, seqSolSolicitud, seqSolEtapa)
            If Not enLinea Then
                InformacionCliente.eliminarBloqueoCliente(dbc, idAdm, 0, numeroId)
            End If

            dbc.Commit()

        Catch e As SondaException

            dbc.Rollback()

            ''''Error de Parametros previo a partir Servicio.
            '''' Se debe dejar el lote en estado. Si SIMULA se deja en SP. Si Acredita se deja como SI.
            If Left(tipoProceso, 2) = "SI" Then
                ControlAcr.modEstadoProceso(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, "SP", usu, fun)
                dbc.Commit()
            ElseIf Left(tipoProceso, 2) = "AC" Then
                ControlAcr.modEstadoProceso(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, "AP", usu, fun)
                dbc.Commit()
            End If

            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception

            dbc.Rollback()

            '''' Se debe dejar el lote en estado. Si SIMULA se deja en SP. Si Acredita se deja como SI.
            If Left(tipoProceso, 2) = "SI" Then
                ControlAcr.modEstadoProceso(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, "SP", usu, fun)
                dbc.Commit()
            ElseIf Left(tipoProceso, 2) = "AC" Then

                ControlAcr.modEstadoProceso(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, "AP", usu, fun)
                dbc.Commit()
            End If

            evento(LOG, "ERROR: INEControlAcr.ProcesarAcreditacionBatch " & e.ToString)

            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Sub

    Public Shared Function ProcesarAcreditacionBatch(ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal numeroId As Integer, ByVal tipoProceso As String, ByVal usu As String, ByVal fun As String, Optional ByVal LOG As Procesos.logEtapa = Nothing, Optional ByVal seqSolSolicitud As Integer = 0, Optional ByVal seqSolEtapa As Integer = 0) As Integer
        Dim dbc As OraConn

        Dim ParOriPro As ParametrosINE.OrigenProceso
        Dim ParControl As ParametrosINE.Control

        Dim dsOrigen As DataSet
        Dim dsControl As DataSet
        Dim dsParControl As DataSet

        Dim gCantHebras As Integer

        Dim maximoParaOrigen As Integer = 0
        Dim maximoTodosOrigenes As Integer = 0
        Dim numTrnProcesando As Integer = 0
        Dim numProProcesando As Integer = 0
        Dim regAProcesar As Integer = 0
        Dim strEstadoACR As String
        Dim strEstadoEspera As String
        Dim i As Integer
        Dim enLinea As Boolean

        Dim ccControlAcr As ACR.ccControlAcreditacion   'tabla ACR_CONTROL_ACREDITACION
        Dim ccParControl As PAR.ccAcrControl            'tabla PAR_ACR_CONTROL
        Dim ccOrigen As PAR.ccAcrOrigenProceso          'tabla PAR_ACR_ORIGEN_PROCESO

        'Dim hilo As Thread


        Try

            Try
                dbc = New OraConn()
            Catch iex As SondaException
                If Not IsNothing(iex.InnerException) Then
                    Dim ex2 As Exception = iex.InnerException
                    If TypeOf ex2 Is SerializationException Then
                        dbc = New OraConn()
                    Else
                        Throw
                    End If
                End If
            End Try

            dbc.BeginTrans()

            'jobPL-->lfc-->>>
            Dim indProcesoPorJob As String = "N"
            If codOrigenProceso = "RECAUDAC" Then
                Try
                    indProcesoPorJob = Sys.IngresoEgreso.ControlAcr.Acreditacion.procesoEnJob(dbc, idAdm, codOrigenProceso)
                Catch
                    indProcesoPorJob = "N"
                End Try

            End If

            If indProcesoPorJob = "S" Then
                evento(LOG, "Procesamiento por JOB")
            Else
                Dim dsHebras As DataSet
                evento(LOG, "Calculando Hebras necesarias para la ejecucion del proceso...")
                dsHebras = HebrasProceso.procCalculaHebrasAcr(dbc, idAdm, numeroId, Left(tipoProceso, 2), "ACR", usu, fun)
                If dsHebras.Tables(0).Rows.Count > 0 Then
                    evento(LOG, "Cantidad de Hebras Calculadas: " & dsHebras.Tables(0).Rows.Count)
                    gCantHebras = dsHebras.Tables(0).Rows(0).Item("CANT_HEBRAS")
                End If

            End If
            evento(LOG, "El control de acreditacion va a realizar algunas validaciones previas a la acreditacion")
            '-------------------------------------------------------------------
            '1. Validaciones generales
            '-------------------------------------------------------------------

            '1.1 verificar que  el tipo de proceso sea valido

            If tipoProceso <> "SIM" And tipoProceso <> "ACR" Then
                Throw New SondaException(15012) '"Tipo proceso erroneo
            End If


            '1.2 verificar que el origen de proceso exista

            dsOrigen = ParOriPro.traer(dbc, idAdm, codOrigenProceso)
            If dsOrigen.Tables(0).Rows.Count = 0 Then 'existe el origen de proceso
                Throw New SondaException(15005) 'Origen de proceso desconocido"
            End If
            ccOrigen = New PAR.ccAcrOrigenProceso(dsOrigen)

            '1.3 verificar que exista el proceso para el usuario y numero_id

            dsControl = ControlAcr.buscarProceso(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId)
            If dsControl.Tables(0).Rows.Count = 0 Then
                Throw New SondaException(15006) '"Proceso de acreditación no ha sido creado"
            End If
            ccControlAcr = New ACR.ccControlAcreditacion(dsControl)

            '1.4 verificar que el proceso esté en  Espera
            strEstadoEspera = IIf(tipoProceso = "SIM", "ES", "EA")
            'strEstadoEspera = IIf(tipoProceso = "SIM", "XS", "XA")

            If Not (ccControlAcr.estadoProceso = strEstadoEspera) Then
                Throw New SondaException(15010)
            End If

            'strEstadoEspera = IIf(strEstadoEspera = "XS", "ES", "EA")

            '--------------------------------------------------------------------------------
            '3. Validacion del máximo de pesos para acreditar segun ORIGEN DE PROCESO y FECHA
            '--------------------------------------------------------------------------------

            'If codOrigenProceso = "RECAUDAC" Then
            '    Dim fecAcreditacion As Date
            '    Dim indBloqueo As String
            '    fecAcreditacion = Kernel.Parametros.FechaAcreditacion.obtenerFechaAcreditacion(dbc, idAdm, "ACR")
            '    indBloqueo = ControlAcr.verificarTopeAcred(dbc, idAdm, codOrigenProceso, fecAcreditacion)
            '    If indBloqueo = "S" Then
            '        ControlAcr.modEstadoProceso(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, strEstadoEspera, usu, fun)
            '        dbc.Commit()
            '        evento(LOG, "Proceso supera tope permitido para la acreditación o tope no se ha establecido para la fecha de acreditación: " & codOrigenProceso)
            '        evento(LOG, "El proceso ha quedado en espera")
            '        Exit Try
            '        'Throw New SondaException(15014) 
            '    End If
            'End If

            '---------------------------------------------------------------------------------

            dsParControl = ParControl.traer(dbc, idAdm)
            If dsParControl.Tables(0).Rows.Count = 0 Then
                Throw New SondaException(15013) 'No se ha en contrado el máximo de regitros permitido para la 
                'acreditación
            End If
            ccParControl = New PAR.ccAcrControl(dsParControl)

            regAProcesar = ccControlAcr.totRegistrosCreados

            enLinea = (ccControlAcr.tipoAcreditacionCreado = "ENL") And (regAProcesar <= ccParControl.numMaxAcrEnl)



            If ccParControl.numMaxAcrEnl = 0 And ccParControl.numMaxAcrTotal = 0 And ccParControl.simulacionOffLine = "S" Then
                ' permitir simulacion
                If tipoProceso <> "SIM" Then

                    ControlAcr.modEstadoProceso(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, "SP", usu, fun)
                    dbc.Commit()
                    evento(LOG, "La acreditación no está permitida, El estado del lote será modificado")
                    evento(LOG, "El proceso ha quedado en espera")

                    Throw New SondaException(15012) '"Tipo proceso erroneo
                End If

            End If

            If Not enLinea Then
                '--------------------------------------------------------------------------------
                '3. Validacion de numero de registros contra el máximo para el ORIGEN DE PROCESO
                '--------------------------------------------------------------------------------


                '3.1 obtenemos la cantidad maxima de procesos para el origen de proceso

                maximoParaOrigen = ccOrigen.numMaximoAcr

                '3.2 obtenemos la suma de todos procesos en estado "Procesando" para ese origen de proceso

                numProProcesando = ControlAcr.contarProcEnProceso(dbc, idAdm, codOrigenProceso)

                '3.2 si la suma supera el maximo de registros por origen dejamos el proceso en espara
                If numProProcesando + 1 > maximoParaOrigen Then

                    If ccParControl.numMaxAcrEnl = 0 And ccParControl.numMaxAcrTotal = 0 And ccParControl.simulacionOffLine = "S" Then
                        ' permitir simulacion
                    Else

                        ControlAcr.modEstadoProceso(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, strEstadoEspera, usu, fun)
                        dbc.Commit()
                        evento(LOG, "El proceso supera el numero máximo de transacciones para acreditar simultaneamente para el origen del proceso " & codOrigenProceso)
                        evento(LOG, "El proceso ha quedado en espera")
                        Exit Try

                    End If
                End If


                '--------------------------------------------------------------------------------
                '4. Validacion de numero de registros contra el máximo para TODOS LOS ORIGENES
                '--------------------------------------------------------------------------------

                '4.1 obtenemos la cantidad maxima de transacciones para todos los origenes

                maximoTodosOrigenes = ccParControl.numMaxAcrTotal

                '4.2 obtenemos la suma de todos las transacciones en estado "Procesando" para todos los origenes
                numTrnProcesando = ControlAcr.contarTrnEnProceso(dbc, idAdm, Nothing)


                '4.3 si la suma supera el maximo de registros  dejamos el proceso en espara
                If regAProcesar + numTrnProcesando > maximoTodosOrigenes Then

                    If ccParControl.numMaxAcrEnl = 0 And ccParControl.numMaxAcrTotal = 0 And ccParControl.simulacionOffLine = "S" Then
                        ' permitir simulacion
                    Else


                        ControlAcr.modEstadoProceso(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, strEstadoEspera, usu, fun)
                        dbc.Commit()
                        evento(LOG, "El proceso supera el numero máximo de registros para acreditar simultaneamente")
                        evento(LOG, "El proceso ha quedado en espera")
                        Exit Try
                    End If
                End If

                '--------------------------------------------------------------------------------
                '5. Validacion de Procesos Excluyentes
                '--------------------------------------------------------------------------------

                '5.1 obtenemos la cantidad maxima de transacciones para todos los origenes

                If ControlAcr.determinaProcesosExcluy(dbc, idAdm, codOrigenProceso) = "S" Then
                    ControlAcr.modEstadoProceso(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, strEstadoEspera, usu, fun)
                    dbc.Commit()
                    evento(LOG, "Existe proceso excluyente en ejecución")
                    evento(LOG, "El proceso ha quedado en espera")
                    Exit Try
                End If

                '-------------------------------------------------------------------------
                '//         aquí se gatilla la acreditación para este proceso           //
                '-------------------------------------------------------------------------

            End If

            'lfc:16/08/2016 - verifica lotes sin conexion y con estado en proceso
            Transacciones.verificaLotesEnLinea(dbc, idAdm)


            ' ---/*-----jobPL-->lfc-->>>
            If indProcesoPorJob = "S" Then
                acreditar(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, -100, usu, fun, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, 0, 0, 0, 0, False, LOG, seqSolSolicitud, seqSolEtapa, 0)

            Else

                If Not enLinea Then

                    'Proceso de Hebras para Procesos EN LINEA
                    Dim dsHebra As DataSet
                    'Dim hilos() As HebrasAcr
                    Dim j As Integer

                    evento(LOG, "Iniciando Hebras...")
                    dsHebra = HebrasProceso.procTraerHebrasAcr(dbc, idAdm, numeroId, Left(tipoProceso, 2))

                    If dsHebra.Tables(0).Rows.Count > 0 Then
                        'ReDim hilos(dsHebra.Tables(0).Rows.Count - 1)
                        'For j = 0 To dsHebra.Tables(0).Rows.Count - 1
                        '    hilos(j) = New HebrasAcr(idAdm, numeroId, tipoProceso, codOrigenProceso, idUsuarioProceso, dsHebra, dsHebra.Tables(0).Rows(j).Item("ID_HEBRA"), usu, fun, LOG, seqSolSolicitud, seqSolEtapa)
                        'Next

                        HebrasAcr.Supervisor(dbc, LOG, dsHebra, New paramDatos(idAdm, numeroId, tipoProceso, codOrigenProceso, idUsuarioProceso, dsHebra, usu, fun, seqSolSolicitud, seqSolEtapa), gCantHebras, "BAT")
                        'log.estado = Procesos.Estado.Exitoso
                    End If

                Else

                    HebrasAcr.SupervisorEnl(dbc, LOG, New paramDatos(idAdm, numeroId, tipoProceso, codOrigenProceso, idUsuarioProceso, Nothing, usu, fun, seqSolSolicitud, seqSolEtapa))
                End If

            End If

            'acreditar(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, 0, usu, fun, LOG, seqSolSolicitud, seqSolEtapa)
            If Not enLinea Then
                InformacionCliente.eliminarBloqueoCliente(dbc, idAdm, 0, numeroId)
            End If
            dbc.Commit()

        Catch e As SondaException


            dbc.Rollback()

            ''''Error de Parametros previo a partir Servicio.
            '''' Se debe dejar el lote en estado. Si SIMULA se deja en SP. Si Acredita se deja como SI.
            If Left(tipoProceso, 2) = "SI" Then
                ControlAcr.modEstadoProceso(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, "SP", usu, fun)
                dbc.Commit()
            ElseIf Left(tipoProceso, 2) = "AC" Then

                ControlAcr.modEstadoProceso(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, "AP", usu, fun)
                dbc.Commit()
            End If

            Dim sm As New SondaExceptionManager(e)


        Catch e As Exception

            If Not IsNothing(dbc) Then dbc.Rollback()

            '''' Se debe dejar el lote en estado. Si SIMULA se deja en SP. Si Acredita se deja como SI.
            If Left(tipoProceso, 2) = "SI" Then
                ControlAcr.modEstadoProceso(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, "SP", usu, fun)
                dbc.Commit()
            ElseIf Left(tipoProceso, 2) = "AC" Then

                ControlAcr.modEstadoProceso(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, "AP", usu, fun)
                dbc.Commit()
            End If

            evento(LOG, "ERROR: INEControlAcr.ProcesarAcreditacionBatch " & e.ToString)

            Dim sm As New SondaExceptionManager(e)
        Finally
            If Not IsNothing(dbc) Then dbc.Close()
        End Try

    End Function

    Private Shared Sub acreditar(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal numeroId As Integer, ByVal tipoProceso As String, ByVal IhHebra As Integer, ByVal usu As String, ByVal fun As String, _
                                 ByVal FecAcreditacion As Date, ByVal FecValorCuota As Date, ByVal PerCuatrimestre As Date, ByVal PerContable As Date, ByVal PerContableSis As Date, ByVal SeqProceso As Decimal, ByVal ValMlCuotaDestinoA As Decimal, ByVal ValMlCuotaDestinoB As Decimal, ByVal ValMlCuotaDestinoC As Decimal, ByVal ValMlCuotaDestinoD As Decimal, ByVal ValMlCuotaDestinoE As Decimal, ByVal PermiteAcreditacionParcial As Boolean, _
                                 Optional ByVal LOG As Procesos.logEtapa = Nothing, Optional ByVal seqSolSolicitud As Integer = 0, Optional ByVal seqSolEtapa As Integer = 0, Optional ByRef SeqLog As Integer = 0)

        ' Dim metodo As New INEProcesosAcr()
        Dim metodo2 As New INEProcesosAcr2()

        Dim estado As Integer

        tipoProceso = tipoProceso.Substring(0, 2)

        Select Case codOrigenProceso

            Case "RECAUDAC"

                If IhHebra = -100 Then
                    'version JOB
                    Sys.IngresoEgreso.ControlAcr.Acreditacion.acredita(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, usu, fun)

                Else

                    estado = metodo2.AcredRecaudac(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)

                End If
            Case "ACREXTGR"

                estado = metodo2.AcredExTGR(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)

            Case "ACREXIPS", "ACREXSTJ", "ACREXTBF", "ACREXAFC", "TRAINCHP"

                estado = metodo2.AcredExterna(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)

            Case "DECCOBR"

                estado = metodo2.AcredDnpCobranzas(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)

				'--.-- CN2 -- SE AGREGAN LOS 2 ORIGEN "RETCVCAD", "RETCVCFO" - ACREDTACION CVC
				'ca-10097736  añade "R10EXADM", "R10EXFDO
			Case "RETCAVAD", "RETCAVFO", "RETCDCAD", "RETCDCFO", "RETCCVAD", "RETCCVFO", "RETCAIFO", "RETPAGPE", "RETEXDIP", "RETCVCAD", "RETCVCFO", "RET10FDO", "RET10ADM", "R10EXADM", "R10EXFDO"			  ' RETIRO10 10% SE AÑADEN NUEVOS ORIGENES DE PROCESO

				estado = metodo2.AcredRetiro(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)
				'actualizarSolicitud(dbc, idAdm, seqSolSolicitud, seqSolEtapa, IIf(estado = 0, "OK", "ER"), usu, fun)

				''If estado = 0 Then
				''Sys.Retiros.AcreditaRetiro.actualEstAcrSol(dbc, idAdm, numeroId, usu, fun)
				''End If

				'If tipoProceso = "AC" Then 'os_2701515 -                   
				'    Sonda.Gestion.Adm.Sys.sysRetirosSap.AcreditaRetiro3.actualEstAcrSol(dbc, idAdm, numeroId, usu, fun)
				'End If

			Case "RETPAGCO"

				estado = metodo2.AcredRetiro(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)
				'actualizarSolicitud(dbc, idAdm, seqSolSolicitud, seqSolEtapa, IIf(estado = 0, "OK", "ER"), usu, fun)

			Case "TRAEGAPV", "TREGAPVC", "TRAEGCAV"

				estado = metodo2.AcredEgresoAPV(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)
				'Sys.SysEgresosdelFondo.TraspasoAPV.actRegAcrSolicitudes(dbc, idAdm, numeroId)

			Case "TRAEGCTA", "TRAEGCHP"

				estado = metodo2.AcredEgresoCTA(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)
				'If tipoProceso = "AC" Then
				'    Dim mensaje As String
				'    mensaje = Sys.SysEgresosdelFondo.CierreCuentas.actMovFinalizaSolicitudes(dbc, idAdm, numeroId, usu, fun)
				'    evento(LOG, mensaje)
				'    mensaje = Sys.sysAdministracionInfFondo.CierreOperacional.CierreTributarioTraspasoEgreso(dbc, idAdm, usu, fun)
				'    evento(LOG, mensaje)

				'End If

			Case "TRAEGEXT"

				estado = metodo2.AcredEgresoCTA(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)
				'If estado = 0 And tipoProceso = "AC" Then
				'    Sys.TraspasoExtranjero.TraspasoExtranjero.acreditacionTrasExt(dbc, idAdm, numeroId)
				'End If

			Case "TRAEGDES"

				estado = metodo2.AcredEgresoCTA(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)
				'If tipoProceso = "AC" Then
				'    Dim mensaje As String
				'    evento(LOG, "Se va ha actualizar solicitudes")
				'    mensaje = Sys.SysEgresosdelFondo.DESAFILIACION.actualizarProcesoAcr(dbc, idAdm, numeroId, codOrigenProceso, idUsuarioProceso, usu, fun)
				'    evento(LOG, mensaje)
				'    evento(LOG, "Solicitudes han sido actualizadas")
				'End If

			Case "RTREGCTA", "RTREGDES"

				estado = metodo2.AcredRevEgresoCTA(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)
				'If tipoProceso = "AC" Then
				'    Dim mensaje As String
				'    evento(LOG, "Se va ha actualizar solicitudes")
				'    mensaje = Sys.SysEgresosdelFondo.CierreCuentas.actMovFinalizaSolicitudes(dbc, idAdm, numeroId, usu, fun)
				'    evento(LOG, mensaje)
				'    evento(LOG, "Solicitudes han sido actualizadas")
				'End If


			Case "TRAINAPV", "TRAINCAV"

				estado = metodo2.AcredIngresoAPV(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)
				'actualizarSolicitud(dbc, idAdm, seqSolSolicitud, seqSolEtapa, IIf(estado = 0, "OK", "ER"), usu, fun)

				''PENDIENTE:  pasar a produccion con MBELTRAN.
				'If tipoProceso = "AC" Then
				'    evento(LOG, "Actualizando Solicitudes...")
				'    Try
				'        Sonda.Gestion.Adm.Sys.IngresoalFondo.ProcesosSaldosApv.modEstadoSolicitud(dbc, idAdm, numeroId, usu, fun)
				'        evento(LOG, "Las Solicitudes han sido actualizadas")
				'    Catch : End Try
				'End If


			Case "TRAINCTA"

				estado = metodo2.AcredIngresoCTA(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)
				'actualizarSolicitud(dbc, idAdm, seqSolSolicitud, seqSolEtapa, IIf(estado = 0, "OK", "ER"), usu, fun)

				'If tipoProceso = "AC" Then
				'    Dim mensaje As String
				'    evento(LOG, "Se va ha actualizar solicitudes")
				'    Try
				'        mensaje = Sonda.Gestion.Adm.Sys.IngresoalFondo.ProcesosTrasal.actualizarSolAcreditadas(dbc, idAdm, numeroId, usu, fun)
				'        evento(LOG, mensaje)
				'        evento(LOG, "Solicitudes han sido actualizadas")
				'    Catch : End Try
				'End If


			Case "TRAINEXT"

				estado = metodo2.AcredIngresoCTA(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)
				'If estado = 0 And tipoProceso = "AC" Then
				'    Sys.TraspasoExtranjero.TraspasoExtranjero.acreditacionTrasExt(dbc, idAdm, numeroId)
				'End If


			Case "TRAINTRA", "TRAINPRV"

				estado = metodo2.AcredIngresoTRF(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)
				'actualizarSolicitud(dbc, idAdm, seqSolSolicitud, seqSolEtapa, IIf(estado = 0, "OK", "ER"), usu, fun)

			Case "TRAINREZ", "TRAIPAGN", "TRAIPAGC", "TRAINRZC"			 'Pago directo, Circ. 1317

				estado = metodo2.AcredIngresoPAG_new(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)
				'actualizarSolicitud(dbc, idAdm, seqSolSolicitud, seqSolEtapa, IIf(estado = 0, "OK", "ER"), usu, fun)


			Case "AJUSELEC", "AJUMASIV", "COMPECON", "BEFACAJU", "GEXCAMAS", "ACREXBXH", "ACRTGRCO", "ACRFREZZ", "ACRTOOBL", "ACRTRAFC", "ACRTOPRO", "BONO200K"
				'<<-- lfc:// comision TGR - ca-4048436--

				estado = metodo2.AcredAjuste(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, usu, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)

			Case "DEVEXCAF", "DEVEXCEM"

				estado = metodo2.AcredDevExcesos(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)

				'If estado = 0 Then
				'    Sys.sysAportesPendientes.Excesos.actualizarSolicitudAcred(dbc, idAdm, numeroId, usu, fun)
				'End If

			Case "RDEVEXAF", "RDEVEXEM"

				estado = metodo2.AcredReintegroDevExcesos(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)

			Case "REINCHEQ"
				estado = metodo2.AcredReintegroCheque(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)

			Case "LIQBONEX", "LIQBONNO" : estado = metodo2.AcredBono(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)


			Case "PAGOPENS"

				estado = metodo2.AcredPagoPens(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)

			Case "PAGOTRBE"

				estado = metodo2.AcredPagoOtrBen(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)

			Case "REREZSEL", "REREZMAS", "REREZCON"

				estado = metodo2.AcredRecupRezagos(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)

			Case "COMADMSA"

				estado = metodo2.AcredComisionSaldo(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)

			Case "GEXCESOS"
				estado = metodo2.AcredGeneracionExcesos(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)

			Case "CAMBFOND"

				estado = metodo2.AcredCambioFondo(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)

			Case "DEVTECEX"
				estado = metodo2.AcredAjuste(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, usu, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)

			Case "AEXDVSTJ"
				estado = metodo2.AcredAjuste(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, usu, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)

			Case "COBPRIMA"
				estado = metodo2.AcredAjuste(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, usu, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)

			Case "COLLECT"

				'estado = metodo.AcredCollect(idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, LOG)
		End Select


    End Sub

    Private Shared Sub actualizarSolicitud(ByVal dbc As OraConn, ByVal idadm As Integer, ByVal seqSolicitud As Integer, ByVal seqEtapa As Integer, ByVal estado As String, ByVal usu As String, ByVal fun As String)

        If seqSolicitud > 0 And seqEtapa > 0 Then
            Sys.Soporte.Solicitud.modEstadoSol(dbc, idadm, seqSolicitud, estado, usu, fun)
            Sys.Soporte.Etapa.modEstadoEta(dbc, idadm, seqSolicitud, seqEtapa, estado, usu, fun)
        End If
    End Sub

    Public Shared Sub eliminarProcesoAcreditacion(ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal numeroId As Integer)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            ControlAcr.eliminarProcesoAcreditacion(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId)

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

    Private Shared Sub evento(ByVal log As Procesos.logEtapa, ByVal mensaje As String)
        If Not IsNothing(log) Then
            log.AddEvento("ACR: " & mensaje)
            log.Save()
        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub


    Public Class HebrasAcr

        Public Shared gvalMlCuotaDestinoA As Double
        Public Shared gvalMlCuotaDestinoB As Double
        Public Shared gvalMlCuotaDestinoC As Double
        Public Shared gvalMlCuotaDestinoD As Double
        Public Shared gvalMlCuotaDestinoE As Double
        Public Shared gfecAcreditacion As Date
        Public Shared gfecValorCuota As Date
        Public Shared gperContable As Date
        Public Shared gperCuatrimestre As Date
        Public Shared gPerContableSis As Date    '---------------SIS
        Public Shared blPermiteAcreditacionParcial As Boolean
        Public Shared gExisteEncabezado As Boolean
        Public Shared gseqProceso As Decimal
        Public Shared blErrorFatal As Boolean
        Public Shared gSeqLog As Integer

        Dim dsAux As DataSet
        Dim rOriPro As ccAcrOrigenProceso
        Dim rCab As ccEncabezadoAcred

        Public Shared gtipoproceso As String
        Public Shared gnumeroid As Integer
        Public Shared gidadm As Integer
        Dim dbc As OraConn
        Public Shared gidUsuarioProceso As String
        Public Shared gfuncion As String
        Public Shared gcodOrigenProceso As String
        Dim gnumOrigenRef As Decimal

        Dim gTotRegistrosIgnorados As Integer
        Dim gTotRegistrosSimulados As Integer
        Dim gTotRegistrosAcreditados As Integer
        Dim blIgnorar As Boolean
        Dim gEstadoError As String
        Dim gEstadoLote As String
        'Dim gSeqLog As Integer

        Private Enum EstadoHebra
            Creada
            Ejecucion
            Finalizada
            FinError
            SinMemoria
        End Enum

        'Variable pública estática para mantener una cuenta de hilos en ejecución 
        '(hilos que están corriendo el sub)
        Public Shared m_CountThreads As Long = 0

        Public Datos As paramDatos
        Public m_Hilo As Thread
        Public m_StopSignal As Boolean = False    'Variable para manejar la condición de parada
        Public excep As Exception

        Private estHebra As HebrasAcr.EstadoHebra
        Private informada As Boolean = False

        'Sub llamado para cancelar la hebra
        Public Sub [Stop]()
            m_StopSignal = True
            estHebra = EstadoHebra.FinError
        End Sub

        Public Sub New(ByVal idAdm As Integer, ByVal NumeroId As Integer, ByVal TipoProceso As String, ByVal CodOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal ds As DataSet, ByVal idHebra As Integer, ByVal usu As String, ByVal fun As String, ByVal LOG As Procesos.logEtapa, Optional ByVal seqSolSolicitud As Integer = 0, Optional ByVal seqSolEtapa As Integer = 0, Optional ByVal CantHebras As Integer = 0)
            Datos.idAdm = idAdm
            Datos.NumeroId = NumeroId
            Datos.TipoProceso = TipoProceso
            Datos.CodOrigenProceso = CodOrigenProceso
            Datos.idUsuarioProceso = idUsuarioProceso
            Datos.dsHebra = ds
            Datos.idHebra = idHebra
            Datos.usu = usu
            Datos.fun = fun
            Datos.LOG = LOG
            Datos.seqSolSolicitud = seqSolSolicitud
            Datos.seqSolEtapa = seqSolEtapa
            Datos.CantHebras = CantHebras
            estHebra = EstadoHebra.Creada

            Me.m_Hilo = New Thread(AddressOf Me.EjecutaAcreditacion)
            Me.m_Hilo.Start()

            'Thread.Sleep(2000) '2seg.
        End Sub

        Public Sub New()

        End Sub

        'Este sub será ejecutado por los hilos
        Public Sub EjecutaAcreditacion()
            Dim dbc As OraConn
            System.Threading.Interlocked.Increment(m_CountThreads) 'Incremento la cantidad de hilos en ejecución

            Try
                'control de ejecucion
                If m_CountThreads > Datos.CantHebras Then
                    estHebra = EstadoHebra.SinMemoria
                    Thread.Sleep(1000) '1seg.
                    Exit Try
                End If


                dbc = New OraConn()

                GenerarLog(dbc, "I", 0, "****************************************************************", Nothing, Nothing, Nothing)

                estHebra = EstadoHebra.Ejecucion

                acreditar(dbc, Datos.idAdm, Datos.CodOrigenProceso, Datos.idUsuarioProceso, Datos.NumeroId, Datos.TipoProceso, Datos.idHebra, Datos.usu, Datos.fun, gfecAcreditacion, gfecValorCuota, _
                          gperCuatrimestre, gperContable, gPerContableSis, gseqProceso, gvalMlCuotaDestinoA, gvalMlCuotaDestinoB, gvalMlCuotaDestinoC, gvalMlCuotaDestinoD, gvalMlCuotaDestinoE, blPermiteAcreditacionParcial, Datos.LOG, 0, 0)

                ' prueba modelo kuntur
                'Dim prueba As New PruebaAcrMod()
                'prueba.acreditar(Me, dbc, Datos.idAdm, Datos.CodOrigenProceso, Datos.idUsuarioProceso, Datos.NumeroId, Datos.TipoProceso, Datos.idHebra, Datos.usu, Datos.fun, gfecAcreditacion, gfecValorCuota, _
                '          gperCuatrimestre, gperContable, gPerContableSis, gseqProceso, gvalMlCuotaDestinoA, gvalMlCuotaDestinoB, gvalMlCuotaDestinoC, gvalMlCuotaDestinoD, gvalMlCuotaDestinoE, blPermiteAcreditacionParcial, Datos.LOG, 0, 0)
                ' fim prueba

                estHebra = EstadoHebra.Finalizada

            Catch out As OutOfMemoryException
                Me.Stop()
                excep = New Exception(out.ToString, out.InnerException)
                estHebra = EstadoHebra.SinMemoria
            Catch ex As SondaException
                Me.Stop()
                excep = New Exception(ex.ToString, ex.InnerException)
                estHebra = EstadoHebra.FinError
            Catch e As Exception
                Me.Stop()
                excep = New Exception(e.ToString, e.InnerException)
                estHebra = EstadoHebra.FinError
            Finally
                If Not IsNothing(dbc) Then dbc.Close()
                'Decremento la cantidad de hilos en ejecución
                System.Threading.Interlocked.Decrement(m_CountThreads)
            End Try
        End Sub

        'Espera que finalicen todos los hilos del arreglo y luego dispara el evento de fin
        'También se encarga de abortar los hilos en caso de que se reciba una señal de parada
        Public Shared Sub Supervisor(ByRef dbc As OraConn, ByRef log As Procesos.logEtapa, ByVal dtHebras As DataSet, ByVal strDatos As paramDatos, ByVal gCantHebras As Integer, ByVal TipProc As String)
            Dim i, j As Integer
            Dim ds As DataSet

            Dim mHilos() As HebrasAcr

            Dim x As New HebrasAcr()
            'Dim Heb As New HebrasAcr()

            gidadm = strDatos.idAdm
            gnumeroid = strDatos.NumeroId
            gidUsuarioProceso = strDatos.idUsuarioProceso
            gcodOrigenProceso = strDatos.CodOrigenProceso
            gfuncion = strDatos.fun

            x.IniciarLog(dbc, strDatos.idAdm, strDatos.idUsuarioProceso, strDatos.NumeroId, strDatos.usu, strDatos.fun, log)

            'GenerarLog("E", 15307, Nothing, 0, Nothing, Nothing)

            '--LFC: 17/03/2009: usuario que envia a acreditar no corresponde al usuario del lote, (funcionalidad: ACRMADMINSTRA)
            '-- mejora sólo para detectar en PLI        
            'If Left(strDatos.TipoProceso, 2) = "AC" And (strDatos.CodOrigenProceso = "AJUSELEC" Or gcodOrigenProceso = "AJUMASIV") And strDatos.usu.ToLower <> strDatos.idUsuarioProceso.ToLower Then
            If (strDatos.CodOrigenProceso = "AJUSELEC" Or gcodOrigenProceso = "AJUMASIV") And strDatos.usu.ToLower <> strDatos.idUsuarioProceso.ToLower Then
                x.GenerarLog(dbc, "I", 0, "Usuario Proceso: " & strDatos.idUsuarioProceso.ToLower & "  Usuario Acreditacion: " & strDatos.usu.ToLower, Nothing, Nothing, Nothing)
            End If

            x.GenerarLog(dbc, "I", 0, "****************************************************************", Nothing, Nothing, Nothing)
            x.GenerarLog(dbc, "I", 0, "*********************Parametros**********************************", Nothing, Nothing, Nothing)
            x.GenerarLog(dbc, "I", 0, "Parámetros - Cantidad de Hebras Iniciadas: " & dtHebras.Tables(0).Rows.Count, Nothing, Nothing, Nothing)
            x.GenerarLog(dbc, "I", 0, "****************************************************************", Nothing, Nothing, Nothing)

            x.GenerarLog(dbc, "I", 0, "Parametros Iniciales...", Nothing, Nothing, Nothing)

            x.ValoresAcreditacion(dbc, strDatos.idAdm, strDatos.CodOrigenProceso, strDatos.idUsuarioProceso, strDatos.NumeroId, Left(strDatos.TipoProceso, 2), strDatos.usu, strDatos.fun, log)

            x.AperturaTransa(dbc)

            If TipProc = "ENL" Then
                ' En Linea....

                ReDim mHilos(dtHebras.Tables(0).Rows.Count - 1)

                For j = 0 To dtHebras.Tables(0).Rows.Count - 1
                    mHilos(j) = New HebrasAcr(strDatos.idAdm, strDatos.NumeroId, strDatos.TipoProceso, strDatos.CodOrigenProceso, strDatos.idUsuarioProceso, strDatos.dsHebra, dtHebras.Tables(0).Rows(j).Item("ID_HEBRA"), strDatos.usu, strDatos.fun, strDatos.LOG, strDatos.seqSolSolicitud, strDatos.seqSolEtapa, gCantHebras)
                Next

                'Thread.Sleep(5000) '5 Segs.
                'Thread.Sleep(600000) '10 Min
                Thread.Sleep(3000) '3 Segs.
                Do
                    'j indica si existen hebras vivas y en ejecucion.
                    j = 0
                    'evento(log, "----------- Hebras en Ejecucion: " & mHilos(0).m_CountThreads.ToString & " -----------")

                    For i = 0 To dtHebras.Tables(0).Rows.Count - 1
                        If Not IsNothing(mHilos(i).m_Hilo) AndAlso mHilos(i).m_Hilo.IsAlive() Then
                            j = 1
                            If mHilos(i).m_StopSignal Then
                                mHilos(i).m_Hilo.Abort()
                                mHilos(i).m_Hilo.Join()
                                mHilos(i).estHebra = EstadoHebra.FinError
                            End If

                            ds = HebrasProceso.procTraerHebrasInfoAcr(dbc, mHilos(i).Datos.idAdm, mHilos(i).Datos.NumeroId, gtipoproceso)
                            evento(log, "Hebra Número: " & mHilos(i).Datos.idHebra & ".")
                            evento(log, "-----------------------------------")

                        ElseIf mHilos(i).estHebra = EstadoHebra.FinError And Not mHilos(i).excep Is Nothing Then
                            If Not mHilos(i).informada Then
                                x.GenerarLog(dbc, "I", 0, "Hebra Número: " & mHilos(i).Datos.idHebra & " - Finalizada con Error.", Nothing, Nothing, Nothing)
                                x.GenerarLog(dbc, "I", 0, "Descripcion del Error: " & mHilos(i).excep.ToString, Nothing, Nothing, Nothing)
                                x.GenerarLog(dbc, "I", 0, "****************************************************************", Nothing, Nothing, Nothing)
                                mHilos(i).informada = True
                            End If
                        ElseIf Not mHilos(i).excep Is Nothing Then
                            If Not mHilos(i).informada Then
                                x.GenerarLog(dbc, "I", 0, "Hebra Número: " & mHilos(i).Datos.idHebra & " - Descripcion del Error: " & mHilos(i).excep.ToString, Nothing, Nothing, Nothing)
                                x.GenerarLog(dbc, "I", 0, "****************************************************************", Nothing, Nothing, Nothing)
                                mHilos(i).informada = True
                            End If
                            'ElseIf mHilos(i).estHebra = EstadoHebra.SinMemoria And mHilos(i).m_CountThreads < 4 Then
                        ElseIf mHilos(i).estHebra = EstadoHebra.SinMemoria And mHilos(i).m_CountThreads < gCantHebras Then
                            j = 1
                            x.GenerarLog(dbc, "I", 0, "Hebra Número: " & mHilos(i).Datos.idHebra & ". En Espera...", Nothing, Nothing, Nothing)
                            x.GenerarLog(dbc, "I", 0, "Intentando Iniciar... ", Nothing, Nothing, Nothing)
                            x.GenerarLog(dbc, "I", 0, "****************************************************************", Nothing, Nothing, Nothing)

                            Dim aux As paramDatos = mHilos(i).Datos
                            mHilos(i) = Nothing
                            mHilos(i) = New HebrasAcr(aux.idAdm, _
                                                        aux.NumeroId, _
                                                        aux.TipoProceso, _
                                                        aux.CodOrigenProceso, _
                                                        aux.idUsuarioProceso, _
                                                        aux.dsHebra, _
                                                        aux.idHebra, _
                                                        aux.usu, _
                                                        aux.fun, _
                                                        aux.LOG, _
                                                        aux.seqSolSolicitud, _
                                                        aux.seqSolEtapa)

                        Else
                            If Not mHilos(i).informada Then
                                If mHilos(i).estHebra = EstadoHebra.Finalizada Then
                                    x.GenerarLog(dbc, "I", 0, "Hebra Número: " & mHilos(i).Datos.idHebra & ". Finalizada", Nothing, Nothing, Nothing)
                                ElseIf mHilos(i).estHebra = EstadoHebra.SinMemoria Then
                                    x.GenerarLog(dbc, "I", 0, "Hebra Número: " & mHilos(i).Datos.idHebra & ". En Espera... ", Nothing, Nothing, Nothing)
                                Else
                                    If mHilos(i).excep Is Nothing Then
                                        x.GenerarLog(dbc, "I", 0, "Hebra Número: " & mHilos(i).Datos.idHebra & ". Finalizada", Nothing, Nothing, Nothing)
                                    Else
                                        x.GenerarLog(dbc, "I", 0, "Hebra Número: " & mHilos(i).Datos.idHebra & ". Finalizada con Error", Nothing, Nothing, Nothing)
                                        x.GenerarLog(dbc, "I", 0, "Descripcion del Error: " & mHilos(i).excep.ToString, Nothing, Nothing, Nothing)
                                    End If
                                End If
                                x.GenerarLog(dbc, "I", 0, "****************************************************************", Nothing, Nothing, Nothing)
                                mHilos(i).informada = True
                            End If
                        End If
                    Next
                Loop While j = 1


                'Dim dbcx As OraConn
                'dbcx = New OraConn()

                'acreditar(dbcx, strDatos.idAdm, strDatos.CodOrigenProceso, strDatos.idUsuarioProceso, strDatos.NumeroId, strDatos.TipoProceso, gCantHebras, strDatos.usu, strDatos.fun, gfecAcreditacion, gfecValorCuota, _
                '          gperCuatrimestre, gperContable, gPerContableSis, gseqProceso, gvalMlCuotaDestinoA, gvalMlCuotaDestinoB, gvalMlCuotaDestinoC, gvalMlCuotaDestinoD, gvalMlCuotaDestinoE, blPermiteAcreditacionParcial, strDatos.LOG, 0, 0)


            Else
                ' Proceso Batch

                ReDim mHilos(dtHebras.Tables(0).Rows.Count - 1)

                For j = 0 To dtHebras.Tables(0).Rows.Count - 1
                    mHilos(j) = New HebrasAcr(strDatos.idAdm, strDatos.NumeroId, strDatos.TipoProceso, strDatos.CodOrigenProceso, strDatos.idUsuarioProceso, strDatos.dsHebra, dtHebras.Tables(0).Rows(j).Item("ID_HEBRA"), strDatos.usu, strDatos.fun, strDatos.LOG, strDatos.seqSolSolicitud, strDatos.seqSolEtapa, gCantHebras)
                Next

                Thread.Sleep(20000) '20 Segs.
                'Thread.Sleep(30000) '30 Segs.
                'Thread.Sleep(60000) '1Min.
                'Thread.Sleep(600000) '10min.
                Do
                    'j indica si existen hebras vivas y en ejecucion.
                    j = 0
                    'evento(log, "----------- Hebras en Ejecucion: " & mHilos(0).m_CountThreads.ToString & " -----------")

                    For i = 0 To dtHebras.Tables(0).Rows.Count - 1
                        If Not IsNothing(mHilos(i).m_Hilo) AndAlso mHilos(i).m_Hilo.IsAlive() Then
                            j = 1
                            If mHilos(i).m_StopSignal Then
                                mHilos(i).m_Hilo.Abort()
                                mHilos(i).m_Hilo.Join()
                                mHilos(i).estHebra = EstadoHebra.FinError
                            End If

                            ds = HebrasProceso.procTraerHebrasInfoAcr(dbc, mHilos(i).Datos.idAdm, mHilos(i).Datos.NumeroId, gtipoproceso)
                            evento(log, "Hebra Número: " & mHilos(i).Datos.idHebra & ".")
                            evento(log, "-----------------------------------")

                        ElseIf mHilos(i).estHebra = EstadoHebra.FinError And Not mHilos(i).excep Is Nothing Then
                            If Not mHilos(i).informada Then
                                x.GenerarLog(dbc, "I", 0, "Hebra Número: " & mHilos(i).Datos.idHebra & " - Finalizada con Error.", Nothing, Nothing, Nothing)
                                x.GenerarLog(dbc, "I", 0, "Descripcion del Error: " & mHilos(i).excep.ToString, Nothing, Nothing, Nothing)
                                x.GenerarLog(dbc, "I", 0, "****************************************************************", Nothing, Nothing, Nothing)
                                mHilos(i).informada = True
                            End If
                        ElseIf Not mHilos(i).excep Is Nothing Then
                            If Not mHilos(i).informada Then
                                x.GenerarLog(dbc, "I", 0, "Hebra Número: " & mHilos(i).Datos.idHebra & " - Descripcion del Error: " & mHilos(i).excep.ToString, Nothing, Nothing, Nothing)
                                x.GenerarLog(dbc, "I", 0, "****************************************************************", Nothing, Nothing, Nothing)
                                mHilos(i).informada = True
                            End If
                            'ElseIf mHilos(i).estHebra = EstadoHebra.SinMemoria And mHilos(i).m_CountThreads < 4 Then
                        ElseIf mHilos(i).estHebra = EstadoHebra.SinMemoria And mHilos(i).m_CountThreads < gCantHebras Then
                            j = 1
                            x.GenerarLog(dbc, "I", 0, "Hebra Número: " & mHilos(i).Datos.idHebra & ". En Espera...", Nothing, Nothing, Nothing)
                            x.GenerarLog(dbc, "I", 0, "Intentando Iniciar... ", Nothing, Nothing, Nothing)
                            x.GenerarLog(dbc, "I", 0, "****************************************************************", Nothing, Nothing, Nothing)

                            Dim aux As paramDatos = mHilos(i).Datos
                            mHilos(i) = Nothing
                            mHilos(i) = New HebrasAcr(aux.idAdm, _
                                                        aux.NumeroId, _
                                                        aux.TipoProceso, _
                                                        aux.CodOrigenProceso, _
                                                        aux.idUsuarioProceso, _
                                                        aux.dsHebra, _
                                                        aux.idHebra, _
                                                        aux.usu, _
                                                        aux.fun, _
                                                        aux.LOG, _
                                                        aux.seqSolSolicitud, _
                                                        aux.seqSolEtapa)

                        Else
                            If Not mHilos(i).informada Then
                                If mHilos(i).estHebra = EstadoHebra.Finalizada Then
                                    x.GenerarLog(dbc, "I", 0, "Hebra Número: " & mHilos(i).Datos.idHebra & ". Finalizada", Nothing, Nothing, Nothing)
                                ElseIf mHilos(i).estHebra = EstadoHebra.SinMemoria Then
                                    x.GenerarLog(dbc, "I", 0, "Hebra Número: " & mHilos(i).Datos.idHebra & ". En Espera... ", Nothing, Nothing, Nothing)
                                Else
                                    If mHilos(i).excep Is Nothing Then
                                        x.GenerarLog(dbc, "I", 0, "Hebra Número: " & mHilos(i).Datos.idHebra & ". Finalizada", Nothing, Nothing, Nothing)
                                    Else
                                        x.GenerarLog(dbc, "I", 0, "Hebra Número: " & mHilos(i).Datos.idHebra & ". Finalizada con Error", Nothing, Nothing, Nothing)
                                        x.GenerarLog(dbc, "I", 0, "Descripcion del Error: " & mHilos(i).excep.ToString, Nothing, Nothing, Nothing)
                                    End If
                                End If
                                x.GenerarLog(dbc, "I", 0, "****************************************************************", Nothing, Nothing, Nothing)
                                mHilos(i).informada = True
                            End If
                        End If
                    Next
                Loop While j = 1

            End If

            x.GenerarLog(dbc, "I", 0, "****************************************************************", Nothing, Nothing, Nothing)
            x.GenerarLog(dbc, "I", 0, "Totaliza Hebras ************************************************", Nothing, Nothing, Nothing)

            Dim EstadoLote As String

            'Busca Registros IGNORADOS en el Total del LOTE.
            Dim DsBuscaIgn As DataSet
            Dim TotIgn As Integer = 0
            DsBuscaIgn = Transacciones.cuentaEstadosTrn(dbc, strDatos.idAdm, strDatos.CodOrigenProceso, strDatos.idUsuarioProceso, strDatos.NumeroId, "ER")
            If DsBuscaIgn.Tables(0).Rows.Count() > 0 Then
                TotIgn = DsBuscaIgn.Tables(0).Rows(0).Item("VTOTALESTADO")
            End If

            'Busca Registros ACREDITADOS en el Total del LOTE.
            Dim DsBuscaAcr As DataSet
            Dim TotAcr As Integer = 0
            DsBuscaAcr = Transacciones.cuentaEstadosTrn(dbc, strDatos.idAdm, strDatos.CodOrigenProceso, strDatos.idUsuarioProceso, strDatos.NumeroId, "AC")
            If DsBuscaAcr.Tables(0).Rows.Count() > 0 Then
                TotAcr = DsBuscaAcr.Tables(0).Rows(0).Item("VTOTALESTADO")
            End If

            'Busca Registros SIMULADOS en el Total del LOTE.
            Dim DsBuscaSim As DataSet
            Dim TotSim As Integer = 0
            DsBuscaSim = Transacciones.cuentaEstadosTrn(dbc, strDatos.idAdm, strDatos.CodOrigenProceso, strDatos.idUsuarioProceso, strDatos.NumeroId, "SI")
            If DsBuscaSim.Tables(0).Rows.Count() > 0 Then
                TotSim = DsBuscaSim.Tables(0).Rows(0).Item("VTOTALESTADO")
            End If


            x.GenerarLog(dbc, "I", 0, "Modifica Estados Finales ************************************************", Nothing, Nothing, Nothing)
            If TotIgn > 0 Then
                evento(log, "- Existen registros ignorados")
            Else
                If TotSim = 0 Then
                    If strDatos.CodOrigenProceso = "RECAUDAC" Then
                        'Solo para RECAUDACION
                        If Left(strDatos.TipoProceso, 2) = "AC" Then
                            Dim DsControlAcr As DataSet
                            Dim NumRef As Integer
                            DsControlAcr = ControlAcr.buscarProceso(dbc, strDatos.idAdm, strDatos.CodOrigenProceso, strDatos.idUsuarioProceso, strDatos.NumeroId)
                            If DsControlAcr.Tables(0).Rows.Count() > 0 Then
                                NumRef = DsControlAcr.Tables(0).Rows(0).Item("NUM_REF_ORIGEN1")
                            End If
                            Lotes.modEstado(dbc, strDatos.idAdm, NumRef, Nothing, "T", strDatos.idUsuarioProceso, strDatos.fun)
                        End If
                    End If
                End If

            End If

            If strDatos.CodOrigenProceso = "RDEVEXAF" Or strDatos.CodOrigenProceso = "RDEVEXEM" Then
                ReintegroCuentas.actualizarCaducidad(dbc, strDatos.idAdm, strDatos.CodOrigenProceso, strDatos.NumeroId, strDatos.idUsuarioProceso, strDatos.fun)
            End If

            ':lfc -tecnico extranjero

            If gtipoproceso = "AC" And (strDatos.CodOrigenProceso = "AJUSELEC" Or _
                                        strDatos.CodOrigenProceso = "AJUMASIV" Or _
                                        strDatos.CodOrigenProceso = "COMPECON" Or _
                                        strDatos.CodOrigenProceso = "BEFACAJU" Or _
                                        strDatos.CodOrigenProceso = "COBPRIMA") Then

                If strDatos.CodOrigenProceso = "COMPECON" Then
                    'solo para 1033, validacion en el package
                    Try
                        sysRefExternas.OCE.modificaSolicitudAcr(dbc, gidadm, gnumeroid, gtipoproceso, gidUsuarioProceso, gfuncion)
                    Catch : End Try
                End If

                Sys.sysAjustes.Ajustes.actualizarAjusteAcred(dbc, gidadm, gnumeroid, gidUsuarioProceso, gfuncion)

                Try
                    Dim nuevaSeqLog As Integer
                    nuevaSeqLog = ControlAcr.LogAcreditacion.obtenerSeqLog(dbc, gidadm, gnumeroid, -1)
                    If nuevaSeqLog > 0 And gSeqLog < nuevaSeqLog Then
                        gSeqLog = nuevaSeqLog
                    End If
                Catch

                End Try
            End If

            x.GenerarLog(dbc, "I", 0, "Fin Modifica Estados Finales ********************************************", Nothing, Nothing, Nothing)

            Dim estado As Integer
            estado = x.TotalesControlAcreditacion(dbc, TotIgn, TotAcr, TotSim, EstadoLote) ' MARCA LA TRANSACCION COMO ACREDITADA

            x.GenerarLog(dbc, "I", 0, "Finaliza Totaliza Hebras ************************************************", Nothing, Nothing, Nothing)

            Dim acrExt As New Sys.AcreditacionExterna.sysProcesos()
            Dim acrExt2 As New Sys.AcreditacionExterna.sysDevolucionStj()
            Dim acrExt3 As New Sys.AcreditacionExterna.sysAportesAFC()

            Select Case strDatos.CodOrigenProceso
                Case "TRAINPRV"
                    'If Left(strDatos.TipoProceso, 2) = "AC" Then
                    '    Sonda.Gestion.Adm.sys.IngresoalFondo.sysCargaArchivoTSPrevired.MarcarEncAcreditados(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)
                    'End If

                Case "ACREXTGR"
                    x.GenerarLog(dbc, "I", 0, "Actualizacion Auxiliar TGR. ", Nothing, Nothing, Nothing)
                    Try

                        Dim DsControlAcrTGR As DataSet
                        Dim AnoTrib As Integer = 0
                        Dim NumProceso As Integer = 0
                        DsControlAcrTGR = ControlAcr.buscarProceso(dbc, strDatos.idAdm, strDatos.CodOrigenProceso, strDatos.idUsuarioProceso, strDatos.NumeroId)
                        If DsControlAcrTGR.Tables(0).Rows.Count() > 0 Then
                            AnoTrib = IIf(IsDBNull(DsControlAcrTGR.Tables(0).Rows(0).Item("NUM_REF_ORIGEN2")), 0, DsControlAcrTGR.Tables(0).Rows(0).Item("NUM_REF_ORIGEN2"))
                            NumProceso = IIf(IsDBNull(DsControlAcrTGR.Tables(0).Rows(0).Item("NUM_REF_ORIGEN1")), 0, DsControlAcrTGR.Tables(0).Rows(0).Item("NUM_REF_ORIGEN1"))
                        End If

                        acrExt.actualizarTgrAcreditado(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.CodOrigenProceso, Left(strDatos.TipoProceso, 2), AnoTrib, NumProceso, strDatos.usu, strDatos.fun)

                    Catch ex As Exception
                        evento(log, ex.Message)
                    End Try

                Case "ACREXIPS", "ACREXSTJ", "ACREXTBF", "AEXDVSTJ"

                    Dim codErr As Integer = 0

                    If strDatos.CodOrigenProceso = "AEXDVSTJ" Then
                        codErr = acrExt2.actualizarRezagos(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)
                    Else
                        codErr = acrExt.modificarEstadoAcreditado(dbc, strDatos.idAdm, strDatos.NumeroId, EstadoLote, strDatos.usu, strDatos.fun)
                    End If

                    If codErr <> 0 Then
                        evento(log, "Error al Modificar AEX_PROCESOS. " & codErr)
                        'sólo mostrar mensaje de error en la actualizacion de estado, 
                    End If

					'ca-10097736  añade "R10EXADM", "R10EXFDO
				Case "RETCAVAD", "RETCAVFO", "RETCDCAD", "RETCDCFO", "RETCCVAD", "RETCCVFO", "RETCAIFO", "RETEXDIP", "RETCVCAD", "RETCVCFO", "RETPAGPE", "RET10FDO", "RET10ADM", "R10EXADM", "R10EXFDO"				  ' RETIRO10 10% SE AÑADEN NUEVOS ORIGENES DE PROCESO
					' se quita "RETPAGPE" porque genera error al realziar estas actualizaciones

					actualizarSolicitud(dbc, strDatos.idAdm, strDatos.seqSolSolicitud, strDatos.seqSolEtapa, IIf(estado = 0, "OK", "ER"), strDatos.usu, strDatos.fun)


					If Left(strDatos.TipoProceso, 2) = "AC" Then				   'os_2701515 -                   
						Sonda.Gestion.Adm.Sys.sysRetirosSap.AcreditaRetiro3.actualEstAcrSol(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)
					End If

				Case "RETPAGCO"

					actualizarSolicitud(dbc, strDatos.idAdm, strDatos.seqSolSolicitud, strDatos.seqSolEtapa, IIf(estado = 0, "OK", "ER"), strDatos.usu, strDatos.fun)

				Case "TRAEGAPV"

					Sys.SysEgresosdelFondo.TraspasoAPV.actRegAcrSolicitudes(dbc, strDatos.idAdm, strDatos.NumeroId)

				Case "TRAEGCTA"
					If Left(strDatos.TipoProceso, 2) = "AC" Then
						Dim mensaje As String
						mensaje = Sys.SysEgresosdelFondo.CierreCuentas.actMovFinalizaSolicitudes(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)
						evento(log, mensaje)
						'mensaje = Sys.sysAdministracionInfFondo.CierreOperacional.CierreTributarioTraspasoEgreso(dbc, strDatos.idAdm, strDatos.usu, strDatos.fun)
						'evento(log, mensaje)

					End If
				Case "TRAEGCHP"
					If Left(strDatos.TipoProceso, 2) = "AC" Then
						Dim mensaje As String

						'OJO. Se deshabilita linea siguiente ´para Catalogacion en MOD yPLV. esto es solo para CAP.
						Sys.TraspasoChilePeru.sysTraspasoChilePeru.finalizaAcreditacion(dbc, strDatos.idAdm, strDatos.NumeroId, gtipoproceso, strDatos.usu, strDatos.fun)




						'evento(log, mensaje)
						'mensaje = Sys.sysAdministracionInfFondo.CierreOperacional.CierreTributarioTraspasoEgreso(dbc, strDatos.idAdm, strDatos.usu, strDatos.fun)
						'evento(log, mensaje)

					End If
				Case "TRAEGCAV"
					If Left(strDatos.TipoProceso, 2) = "AC" Then
						Sys.SolicitudesCAV.TraspasoCAV.actRegAcrSolicitudes(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)
					End If

				Case "TRAEGEXT"

					'estado = metodo2.AcredEgresoCTA(idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)
					If estado = 0 And Left(strDatos.TipoProceso, 2) = "AC" Then
						Sys.TraspasoExtranjero.TraspasoExtranjero.acreditacionTrasExt(dbc, strDatos.idAdm, strDatos.NumeroId)
					End If

				Case "TRAEGDES"

					'estado = metodo2.AcredEgresoCTA(idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)
					If Left(strDatos.TipoProceso, 2) = "AC" Then
						Dim mensaje As String
						evento(log, "Se va ha actualizar solicitudes")
						mensaje = Sys.SysEgresosdelFondo.DESAFILIACION.actualizarProcesoAcr(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.CodOrigenProceso, strDatos.idUsuarioProceso, strDatos.usu, strDatos.fun)
						evento(log, mensaje)
						evento(log, "Solicitudes han sido actualizadas")
					End If

				Case "RTREGCTA", "RTREGDES"

					'estado = metodo2.AcredRevEgresoCTA(idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)
					If Left(strDatos.TipoProceso, 2) = "AC" Then
						Dim mensaje As String
						evento(log, "Se va ha actualizar solicitudes")
						mensaje = Sys.SysEgresosdelFondo.CierreCuentas.actMovFinalizaSolicitudes(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)
						evento(log, mensaje)
						evento(log, "Solicitudes han sido actualizadas")
					End If

				Case "TRAINAPV"

					actualizarSolicitud(dbc, strDatos.idAdm, strDatos.seqSolSolicitud, strDatos.seqSolEtapa, IIf(estado = 0, "OK", "ER"), strDatos.usu, strDatos.fun)

					'PENDIENTE:  pasar a produccion con MBELTRAN.
					If Left(strDatos.TipoProceso, 2) = "AC" Then
						evento(log, "Actualizando Solicitudes...")
						Try
							Sonda.Gestion.Adm.Sys.IngresoalFondo.ProcesosSaldosApv.modEstadoSolicitud(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)
							evento(log, "Las Solicitudes han sido actualizadas")
						Catch : End Try
					End If

				Case "TRAINCTA"

					actualizarSolicitud(dbc, strDatos.idAdm, strDatos.seqSolSolicitud, strDatos.seqSolEtapa, IIf(estado = 0, "OK", "ER"), strDatos.usu, strDatos.fun)

					If Left(strDatos.TipoProceso, 2) = "AC" Then
						Dim mensaje As String
						evento(log, "Se va ha actualizar solicitudes")
						Try
							mensaje = Sonda.Gestion.Adm.Sys.IngresoalFondo.ProcesosTrasal.actualizarSolAcreditadas(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)
							evento(log, mensaje)
							evento(log, "Solicitudes han sido actualizadas")
						Catch : End Try
					End If


				Case "TRAINEXT"

					'estado = metodo2.AcredIngresoCTA(idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)
					If estado = 0 And Left(strDatos.TipoProceso, 2) = "AC" Then
						Sys.TraspasoExtranjero.TraspasoExtranjero.acreditacionTrasExt(dbc, strDatos.idAdm, strDatos.NumeroId)
					End If


				Case "TRAINTRA"

					actualizarSolicitud(dbc, strDatos.idAdm, strDatos.seqSolSolicitud, strDatos.seqSolEtapa, IIf(estado = 0, "OK", "ER"), strDatos.usu, strDatos.fun)

				Case "TRAINREZ", "TRAIPAGN", "TRAIPAGC", "TRAINRZC"				'Pago directo, Circ. 1317

					actualizarSolicitud(dbc, strDatos.idAdm, strDatos.seqSolSolicitud, strDatos.seqSolEtapa, IIf(estado = 0, "OK", "ER"), strDatos.usu, strDatos.fun)

				Case "DEVEXCAF", "DEVEXCEM"

					'estado = metodo2.AcredDevExcesos(idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, log, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)

					If estado = 0 Then
						'Sys.sysAportesPendientes.Excesos.actualizarSolicitudAcred(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)
						Sys.sysExcesos.Excesos.actualizarSolicitudAcred(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)
					End If

				Case "CAMBFOND"
					If gtipoproceso = "AC" Then
						dbc.BeginTrans()

						Dim DsTipoDistrib As DataSet
						Dim TipDist As String

						'Busca TipoDistribucion en ACR_MOV_CAMBIOS_FONDOS por Numero_ID



						DsTipoDistrib = Sys.IngresoEgreso.CambioFondo.cabecera.buscar_acred(dbc, strDatos.idAdm, strDatos.NumeroId)
						If DsTipoDistrib.Tables(0).Rows.Count() > 0 Then
							TipDist = DsTipoDistrib.Tables(0).Rows(0).Item("TIPO_DISTRIBUCION")
						End If

						' JCP - OS 7874915 - FEC.21/10.2016 INI 
						Dim codAdm As Integer
						Dim dsTipoDesTD As DataSet
						codAdm = ParametrosINE.ParametrosGenerales.codigoAdministradora(dbc, strDatos.idAdm)

						If codAdm = 1032 Or codAdm = 1034 Or codAdm = 1035 Then
							dsTipoDesTD = CambioFondo.cabecera.traeTipoDistribucionTd(dbc, strDatos.idAdm, strDatos.NumeroId)
							If dsTipoDesTD.Tables(0).Rows.Count() > 0 Then
								If dsTipoDesTD.Tables(0).Rows(0).Item("TIPO_CAMBIO") = "TD" Then
									TipDist = "TD"
								End If
							End If
						End If

						' JCP - OS 7874915 - FEC.21/10.2016 FIN

						'lfc://02-12-2009--- solicitudes acreditadas con error en las trn's ----------------------->>>>>>>>
						'                actualizar acr_mov_cambios_fondos// con respecto a trn's acreditadas
						Sys.IngresoEgreso.CambioFondo.cabecera.actualizarEstadoCambio(dbc, strDatos.idAdm, strDatos.NumeroId, TipDist, gtipoproceso, strDatos.usu, strDatos.fun)

						'                actulizar adm_solicitudes// con respecto a acr_mov_cambios_fondos
						Sys.IngresoEgreso.CambioFondo.cabecera.actualizaSolAcreditadas(dbc, strDatos.idAdm, strDatos.NumeroId, gtipoproceso, strDatos.usu, strDatos.fun)

						'                actualizar porc_dsitribucion en aaa_distribuciones// con respecto a acr_mov
						Sys.IngresoEgreso.CambioFondo.cabecera.actualizaDistSolAcr(dbc, strDatos.idAdm, strDatos.NumeroId, TipDist, strDatos.usu, strDatos.fun)

						dbc.Commit()
						x.GenerarLog(dbc, "I", 0, " Actualizacion de Solicitudes.", Nothing, Nothing, Nothing)

						'--<<<<<<<<<<<<<<<<<<<<<<-------------------------------------------------------------------------

						'''''''lfc:25-11---- error solicitudes con mas detalle CFR='AC', distribucion con error=sol=AP---
						'''''''Sys.AdministracionClientes.Solicitud.actualizaAcreditSolicitud(gdbc,gidAdm, rCam.fecProceso, gnumeroId, gidUsuarioProceso, gfuncion)
						''''''Sys.IngresoEgreso.CambioFondo.cabecera.actualizaSolAcreditadas(gdbc,gidAdm, numeroId, gtipoProceso, gidUsuarioProceso, gfuncion)

						''''''If rCam.tipoDistribucion = "CFA" Then ' actualizar acr_mov, sólo para el proceso etareo
						''''''    Sys.IngresoEgreso.CambioFondo.cabecera.actualizarEstadoCambio(gdbc,gidAdm, numeroId, rCam.tipoDistribucion, gtipoProceso, "ACTUALIZANDO_SOL", gfuncion)
						''''''    gdbc.Commit()
						''''''End If

						'''''''actualiza aaa_distribuciones//cfn,cfa,cfd: todas las distribuciones con 1 reg//cfa: calcula el %reca,tipoCta desde el %distribucion----
						''''''Sys.IngresoEgreso.CambioFondo.cabecera.actualizaDistSolAcr(gdbc,gidAdm, gnumeroId, rCam.tipoDistribucion, "ACTUALIZANDO_DIS", gfuncion)

						''''''dbc.Commit()
						''''''GenerarLog("I", 0, "Actualizacion de  Solicitudes", 0, Nothing, Nothing)

						'retiros sap Tognola---- no produccion
						''''''Try
						''''''    Dim dsSap As New DataSet()
						''''''    Dim msgSap As String

						''''''    GenerarLog("I", 0, "Revision y Actualizacion Sol. Retiros pendientes", 0, Nothing, Nothing)
						''''''    Dim blRetirosSap As New blRetirosLiquidacion()

						''''''    dsSap = blRetirosSap.wmbuscaCambioFondos(idAdm, numeroId, "AC", gidUsuarioProceso, gfuncion)

						''''''    If dsSap.Tables(0).Rows.Count > 0 Then
						''''''        If dsSap.Tables(0).Rows(0).Item("VCOD_ERROR") <> 0 Then
						''''''            msgSap = IIf(IsDBNull(dsSap.Tables(0).Rows(0).Item("VMSG_ERROR")), "Error al actualizar Sol. Retiros", dsSap.Tables(0).Rows(0).Item("VMSG_ERROR"))
						''''''            GenerarLog("I", 0, Mid(msgSap, 1, 20), 0, Nothing, Nothing)
						''''''        End If
						''''''    End If
						''''''Catch ex_sap As Exception
						''''''    GenerarLog("I", 0, "Error: " & ex_sap.Message, 0, Nothing, Nothing)

						''''''End Try


					End If

				Case "AJUMASIV"				'OS:7109030 - LFC - 30/06/2015, actualizar estado Liberacion Factor de Ajuste
					'A la espera de definicion por LFC. 18/08/2015.
					'Sys.Liquidacion.liberacionFA.actualizarEstadoAcred(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)

					Try					  '-->>>lfc:RTO10%
						sysRefExternas.RETIRO10.actualizaProvAcred(dbc, strDatos.idAdm, strDatos.NumeroId, gtipoproceso, strDatos.usu, strDatos.fun)
						x.GenerarLog(dbc, "I", 0, "Actualizacion Estado de Solicitudes", Nothing, Nothing, Nothing)
					Catch : End Try


				Case "ACREXAFC"
					'NCG 145. 25/08/2015.
					acrExt3.actualizaAcred(dbc, strDatos.idAdm, strDatos.NumeroId, gtipoproceso, strDatos.usu, strDatos.fun)

					x.GenerarLog(dbc, "I", 0, "Actualizacion Estado AFC. ", Nothing, Nothing, Nothing)

				Case "TRAINCAV"
					'OS-7826035
					If Left(strDatos.TipoProceso, 2) = "AC" Then
						Dim DsControlAcr As DataSet
						Dim NumRef As Integer
						Dim NumRef2 As Integer
						Dim FecOperacion As Date

						'PCI OS-7826035 Se comenta. Solo es para CAPITAL, para PLV y MOD no van estas lineas.
						DsControlAcr = ControlAcr.buscarProceso(dbc, strDatos.idAdm, strDatos.CodOrigenProceso, strDatos.idUsuarioProceso, strDatos.NumeroId)
						If DsControlAcr.Tables(0).Rows.Count() > 0 Then
							NumRef = DsControlAcr.Tables(0).Rows(0).Item("NUM_REF_ORIGEN1")
							NumRef2 = DsControlAcr.Tables(0).Rows(0).Item("NUM_REF_ORIGEN2")
							FecOperacion = New Date(Mid(NumRef2, 1, 4), Mid(NumRef2, 5, 2), Mid(NumRef2, 7, 2))

							Sys.SolicitudesCAV.sysTraspasoIngresoCAV.actualizaSolCav(dbc, strDatos.idAdm, FecOperacion, NumRef, "ACR", strDatos.usu, strDatos.fun)


						End If

						x.GenerarLog(dbc, "I", 0, "Actualizacion Estado CAV. ", Nothing, Nothing, Nothing)
					End If

				Case "PAGOPENS"
					If Left(strDatos.TipoProceso, 2) = "AC" Then
						Dim cantReg As Integer
						Try
							'actualiza registros de ajustes predefinidos
							'cantReg = Sys.sysAjustes.ajustesPredefinidos.ajustesUsuario.estadoAcred(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)
							cantReg = sysRefExternas.Ajustes.estadoAcred(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)

							If cantReg > 0 Then
								x.GenerarLog(dbc, "I", 0, "Actualizacion Estado Ajuste", Nothing, Nothing, Nothing)
							End If
						Catch : End Try
					End If
				Case "GEXCAMAS"
					Try
						'call rutina de JGT para la devolucion de excesos                        
						Sonda.Gestion.Adm.Sys.IngresoEgreso.sysExcesos.actualizaEstadoExcMas(dbc, strDatos.idAdm, strDatos.NumeroId, gtipoproceso, strDatos.usu, strDatos.fun)
					Catch : End Try
					'lfc:// comision TGR - ca-4048436-->>>
				Case "ACRTGRCO"
					'NCG 145. 25/08/2015.
					Try
						sysRefExternas.ComisionTGR.modificarEstadoComisionTGR(dbc, strDatos.idAdm, strDatos.NumeroId, gtipoproceso, strDatos.usu, strDatos.fun)
						x.GenerarLog(dbc, "I", 0, "Actualizacion Estado Comision TGR. ", Nothing, Nothing, Nothing)
					Catch ex As Exception
						x.GenerarLog(dbc, "I", 0, "nOK Estado Comision TGR. " & EX.Message, Nothing, Nothing, Nothing)
					End Try
					'<<-- lfc:// comision TGR - ca-4048436--


				Case "ACRTOOBL"				'lfc:RTO10%
					Try
						x.GenerarLog(dbc, "I", 0, "Actualizacion Estado de Solicitudes", Nothing, Nothing, Nothing)
						sysRefExternas.RETIRO10.actualizarValorAcred(dbc, strDatos.idAdm, strDatos.NumeroId, gtipoproceso, strDatos.usu, strDatos.fun)
					Catch ex As Exception
						x.GenerarLog(dbc, "I", 0, "Error Estado Sol: " & EX.Message, Nothing, Nothing, Nothing)
					End Try

				Case "ACRTRAFC"				  ' traspaso de saldo AFC
					Try
						x.GenerarLog(dbc, "I", 0, "Actualizacion Estados Traspaso saldo AFC", Nothing, Nothing, Nothing)
						sysRefExternas.traspasoSaldoAFC.actualizaEstadoAcred(dbc, strDatos.idAdm, strDatos.NumeroId, gtipoproceso, strDatos.usu, strDatos.fun)
					Catch ex As Exception
						x.GenerarLog(dbc, "I", 0, "Error: " & EX.Message, Nothing, Nothing, Nothing)
					End Try

				Case "ACRTOPRO"				'2retiro			
					Try
						sysRefExternas.RETIRO10.actualizaProvAcred(dbc, strDatos.idAdm, strDatos.NumeroId, gtipoproceso, strDatos.usu, strDatos.fun)
						x.GenerarLog(dbc, "I", 0, "Actualizacion Estado de Solicitudes", Nothing, Nothing, Nothing)
					Catch : End Try

				Case "BONO200K"				'bono 200 mil			
					Try
						'sysRefExternas.RETIRO10.actualizaProvAcred(dbc, strDatos.idAdm, strDatos.NumeroId, gtipoproceso, strDatos.usu, strDatos.fun)
						'x.GenerarLog(dbc, "I", 0, "Actualizacion Estado de Solicitudes", Nothing, Nothing, Nothing)
					Catch : End Try

			End Select

			If TipProc <> "ENL" Then
				' Solo para Batch
				For i = 0 To dtHebras.Tables(0).Rows.Count - 1
					If mHilos(i).excep Is Nothing Then
						evento(log, "Hebra Número: " & mHilos(i).Datos.idHebra & ". Finalizada")
					Else
						evento(log, "Hebra Número: " & mHilos(i).Datos.idHebra & ". Finalizada con Error.")
						evento(log, "Descripcion del Error: " & mHilos(i).excep.ToString)
					End If
				Next
			End If

			evento(log, "*****************************************************************")
			x.GenerarLog(dbc, "I", 0, "Fin Proceso Acreditacion ************************************************", Nothing, Nothing, Nothing)
        End Sub


        'lfc: optimizacion
        Public Shared Sub SupervisorEnl(ByRef dbc As OraConn, ByRef log As Procesos.logEtapa, ByVal strDatos As paramDatos)
            Dim i, j As Integer
            Dim ds As DataSet
            'Dim mHilos() As HebrasAcr
            Dim acr As New HebrasAcr()

            gidadm = strDatos.idAdm
            gnumeroid = strDatos.NumeroId
            gidUsuarioProceso = strDatos.idUsuarioProceso
            gcodOrigenProceso = strDatos.CodOrigenProceso
            gfuncion = strDatos.fun

            escribirLogEnl("Iniciando " & IIf(gtipoproceso = "AC", "Acreditación", "Simulación") & " del lote nº " & gnumeroid)

            'lfc:comenta x.IniciarLog(dbc, strDatos.idAdm, strDatos.idUsuarioProceso, strDatos.NumeroId, strDatos.usu, strDatos.fun, log)
            acr.IniciarLog(dbc, strDatos.idAdm, strDatos.idUsuarioProceso, strDatos.NumeroId, strDatos.usu, strDatos.fun, log, True)


            'acr.GenerarLog(dbc, "I", 0, "Parametros Iniciales", Nothing, Nothing, Nothing)
            acr.ValoresAcreditacion(dbc, strDatos.idAdm, strDatos.CodOrigenProceso, strDatos.idUsuarioProceso, strDatos.NumeroId, Left(strDatos.TipoProceso, 2), strDatos.usu, strDatos.fun, log)

            acr.AperturaTransa(dbc)

            acreditar(dbc, _
                      gidadm, _
                      gcodOrigenProceso, _
                      gidUsuarioProceso, _
                      gnumeroid, _
                      gtipoproceso, _
                       1, _
                      gidUsuarioProceso, _
                      gfuncion, _
                      gfecAcreditacion, _
                      gfecValorCuota, _
                      gperCuatrimestre, _
                      gperContable, _
                      gPerContableSis, _
                      0, _
                      gvalMlCuotaDestinoA, _
                      gvalMlCuotaDestinoB, _
                      gvalMlCuotaDestinoC, _
                      gvalMlCuotaDestinoD, _
                      gvalMlCuotaDestinoE, _
                      blPermiteAcreditacionParcial, _
                      log, 0, 0, 0)

            acr.GenerarLog(dbc, "I", 0, "Finalizando proceso", Nothing, Nothing, Nothing)


            Dim EstadoLote As String
            Dim totIgn As Long = 0
            Dim totAcr As Long = 0
            Dim totSim As Long = 0
            Dim totPen As Long = 0
            Dim totLote As Long = 0

            cantidadTrsLote(dbc, strDatos.idAdm, strDatos.CodOrigenProceso, strDatos.idUsuarioProceso, strDatos.NumeroId, totSim, totAcr, totIgn, totPen, totLote)

            ''''''Busca Registros IGNORADOS en el Total del LOTE.
            '''''Dim DsBuscaIgn As DataSet

            '''''DsBuscaIgn = Transacciones.cuentaEstadosTrn(dbc, strDatos.idAdm, strDatos.CodOrigenProceso, strDatos.idUsuarioProceso, strDatos.NumeroId, "ER")
            '''''If DsBuscaIgn.Tables(0).Rows.Count() > 0 Then
            '''''    TotIgn = DsBuscaIgn.Tables(0).Rows(0).Item("VTOTALESTADO")
            '''''End If

            ''''''Busca Registros ACREDITADOS en el Total del LOTE.
            '''''Dim DsBuscaAcr As DataSet

            '''''DsBuscaAcr = Transacciones.cuentaEstadosTrn(dbc, strDatos.idAdm, strDatos.CodOrigenProceso, strDatos.idUsuarioProceso, strDatos.NumeroId, "AC")
            '''''If DsBuscaAcr.Tables(0).Rows.Count() > 0 Then
            '''''    TotAcr = DsBuscaAcr.Tables(0).Rows(0).Item("VTOTALESTADO")
            '''''End If

            ''''''Busca Registros SIMULADOS en el Total del LOTE.
            '''''Dim DsBuscaSim As DataSet

            '''''DsBuscaSim = Transacciones.cuentaEstadosTrn(dbc, strDatos.idAdm, strDatos.CodOrigenProceso, strDatos.idUsuarioProceso, strDatos.NumeroId, "SI")
            '''''If DsBuscaSim.Tables(0).Rows.Count() > 0 Then
            '''''    TotSim = DsBuscaSim.Tables(0).Rows(0).Item("VTOTALESTADO")
            '''''End If

            acr.GenerarLog(dbc, "I", 0, "Actualizando estados", Nothing, Nothing, Nothing)


            If (totIgn + totPen) > 0 Then
                evento(log, "- Existen registros ignorados")
                acr.GenerarLog(dbc, "A", 0, "Existen registros ignorados", Nothing, Nothing, Nothing)

            Else
                If totSim = 0 Then
                    If strDatos.CodOrigenProceso = "RECAUDAC" And gtipoproceso = "AC" Then
                        Dim DsControlAcr As DataSet
                        Dim NumRef As Integer
                        DsControlAcr = ControlAcr.buscarProceso(dbc, strDatos.idAdm, strDatos.CodOrigenProceso, strDatos.idUsuarioProceso, strDatos.NumeroId)
                        If DsControlAcr.Tables(0).Rows.Count() > 0 Then
                            NumRef = DsControlAcr.Tables(0).Rows(0).Item("NUM_REF_ORIGEN1")
                        End If
                        Lotes.modEstado(dbc, strDatos.idAdm, NumRef, Nothing, "T", strDatos.idUsuarioProceso, strDatos.fun)
                    End If
                End If

            End If

            If strDatos.CodOrigenProceso = "RDEVEXAF" Or strDatos.CodOrigenProceso = "RDEVEXEM" Then
                ReintegroCuentas.actualizarCaducidad(dbc, strDatos.idAdm, strDatos.CodOrigenProceso, strDatos.NumeroId, strDatos.idUsuarioProceso, strDatos.fun)
            End If

            ':lfc -tecnico extranjero

            If gtipoproceso = "AC" And (strDatos.CodOrigenProceso = "AJUSELEC" Or _
                                        strDatos.CodOrigenProceso = "AJUMASIV" Or _
                                        strDatos.CodOrigenProceso = "COMPECON" Or _
                                        strDatos.CodOrigenProceso = "BEFACAJU" Or _
                                        strDatos.CodOrigenProceso = "COBPRIMA") Then

                If strDatos.CodOrigenProceso = "COMPECON" Then
                    'solo para 1033, validacion en el package
                    Try
                        sysRefExternas.OCE.modificaSolicitudAcr(dbc, gidadm, gnumeroid, gtipoproceso, gidUsuarioProceso, gfuncion)
                    Catch : End Try
                End If

				Sys.sysAjustes.Ajustes.actualizarAjusteAcred(dbc, gidadm, gnumeroid, gidUsuarioProceso, gfuncion)

				Try
					sysRefExternas.Nominal.actualizarEstadoProcesoAcr(dbc, gidadm, gnumeroid, gidUsuarioProceso, gfuncion)
				Catch
					acr.GenerarLog(dbc, "I", 0, "Error actualiza solicitudes BEN", Nothing, Nothing, Nothing)
				End Try


            End If

			acr.GenerarLog(dbc, "I", 0, "Generando data para informes", Nothing, Nothing, Nothing)

			Dim estado As Integer
			estado = acr.TotalesControlAcreditacion(dbc, (totIgn + totPen), totAcr, totSim, EstadoLote)		   ' MARCA LA TRANSACCION COMO ACREDITADA


			Select Case strDatos.CodOrigenProceso
				Case "TRAINPRV"
					'If Left(strDatos.TipoProceso, 2) = "AC" Then
					'    Sonda.Gestion.Adm.sys.IngresoalFondo.sysCargaArchivoTSPrevired.MarcarEncAcreditados(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)
					'End If

				Case "ACREXTGR"
					acr.GenerarLog(dbc, "I", 0, "Actualizacion Auxiliar TGR. ", Nothing, Nothing, Nothing)
					Try

						Dim DsControlAcrTGR As DataSet
						Dim AnoTrib As Integer = 0
						Dim NumProceso As Integer = 0
						DsControlAcrTGR = ControlAcr.buscarProceso(dbc, strDatos.idAdm, strDatos.CodOrigenProceso, strDatos.idUsuarioProceso, strDatos.NumeroId)
						If DsControlAcrTGR.Tables(0).Rows.Count() > 0 Then
							AnoTrib = IIf(IsDBNull(DsControlAcrTGR.Tables(0).Rows(0).Item("NUM_REF_ORIGEN2")), 0, DsControlAcrTGR.Tables(0).Rows(0).Item("NUM_REF_ORIGEN2"))
							NumProceso = IIf(IsDBNull(DsControlAcrTGR.Tables(0).Rows(0).Item("NUM_REF_ORIGEN1")), 0, DsControlAcrTGR.Tables(0).Rows(0).Item("NUM_REF_ORIGEN1"))
						End If

						Dim acrExt As New Sys.AcreditacionExterna.sysProcesos()
						acrExt.actualizarTgrAcreditado(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.CodOrigenProceso, Left(strDatos.TipoProceso, 2), AnoTrib, NumProceso, strDatos.usu, strDatos.fun)

					Catch ex As Exception
						evento(log, ex.Message)
					End Try

				Case "ACREXIPS", "ACREXSTJ", "ACREXTBF", "AEXDVSTJ"
					Dim acrExt2 As New Sys.AcreditacionExterna.sysDevolucionStj()
					Dim acrExt As New Sys.AcreditacionExterna.sysProcesos()
					Dim codErr As Integer = 0

					If strDatos.CodOrigenProceso = "AEXDVSTJ" Then
						codErr = acrExt2.actualizarRezagos(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)
					Else
						codErr = acrExt.modificarEstadoAcreditado(dbc, strDatos.idAdm, strDatos.NumeroId, EstadoLote, strDatos.usu, strDatos.fun)
					End If

					If codErr <> 0 Then
						evento(log, "Error al Modificar AEX_PROCESOS. " & codErr)
						'sólo mostrar mensaje de error en la actualizacion de estado, 
					End If

					'ca-10097736  añade "R10EXADM", "R10EXFDO
				Case "RETCAVAD", "RETCAVFO", "RETCDCAD", "RETCDCFO", "RETCCVAD", "RETCCVFO", "RETCAIFO", "RETEXDIP", "RETCVCAD", "RETCVCFO", "RETPAGPE", "RET10FDO", "RET10ADM", "R10EXADM", "R10EXFDO"				 ' RETIRO10 10% SE AÑADEN NUEVOS ORIGENES DE PROCESO
					' se quita "RETPAGPE" porque genera error al realziar estas actualizaciones

					actualizarSolicitud(dbc, strDatos.idAdm, strDatos.seqSolSolicitud, strDatos.seqSolEtapa, IIf(estado = 0, "OK", "ER"), strDatos.usu, strDatos.fun)


					If gtipoproceso = "AC" Then					  'os_2701515 -                   
						Sonda.Gestion.Adm.Sys.sysRetirosSap.AcreditaRetiro3.actualEstAcrSol(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)
					End If

				Case "RETPAGCO"

					actualizarSolicitud(dbc, strDatos.idAdm, strDatos.seqSolSolicitud, strDatos.seqSolEtapa, IIf(estado = 0, "OK", "ER"), strDatos.usu, strDatos.fun)

				Case "TRAEGAPV"

					Sys.SysEgresosdelFondo.TraspasoAPV.actRegAcrSolicitudes(dbc, strDatos.idAdm, strDatos.NumeroId)

				Case "TRAEGCTA"
					If gtipoproceso = "AC" Then
						Dim mensaje As String
						mensaje = Sys.SysEgresosdelFondo.CierreCuentas.actMovFinalizaSolicitudes(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)
						evento(log, mensaje)

					End If
				Case "TRAEGCHP"
					If gtipoproceso = "AC" Then
						Sys.TraspasoChilePeru.sysTraspasoChilePeru.finalizaAcreditacion(dbc, strDatos.idAdm, strDatos.NumeroId, gtipoproceso, strDatos.usu, strDatos.fun)
					End If
				Case "TRAEGCAV"
					If gtipoproceso = "AC" Then
						Sys.SolicitudesCAV.TraspasoCAV.actRegAcrSolicitudes(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)
					End If

				Case "TRAEGEXT"

					'estado = metodo2.AcredEgresoCTA(idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)
					If estado = 0 And gtipoproceso = "AC" Then
						Sys.TraspasoExtranjero.TraspasoExtranjero.acreditacionTrasExt(dbc, strDatos.idAdm, strDatos.NumeroId)
					End If

				Case "TRAEGDES"

					'estado = metodo2.AcredEgresoCTA(idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)
					If gtipoproceso = "AC" Then
						Dim mensaje As String
						evento(log, "Se va ha actualizar solicitudes")
						mensaje = Sys.SysEgresosdelFondo.DESAFILIACION.actualizarProcesoAcr(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.CodOrigenProceso, strDatos.idUsuarioProceso, strDatos.usu, strDatos.fun)
						evento(log, mensaje)
						evento(log, "Solicitudes han sido actualizadas")
					End If

				Case "RTREGCTA", "RTREGDES"

					'estado = metodo2.AcredRevEgresoCTA(idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)
					If gtipoproceso = "AC" Then
						Dim mensaje As String
						evento(log, "Se va ha actualizar solicitudes")
						mensaje = Sys.SysEgresosdelFondo.CierreCuentas.actMovFinalizaSolicitudes(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)
						evento(log, mensaje)
						evento(log, "Solicitudes han sido actualizadas")
					End If

				Case "TRAINAPV"

					actualizarSolicitud(dbc, strDatos.idAdm, strDatos.seqSolSolicitud, strDatos.seqSolEtapa, IIf(estado = 0, "OK", "ER"), strDatos.usu, strDatos.fun)

					'PENDIENTE:  pasar a produccion con MBELTRAN.
					If gtipoproceso = "AC" Then
						evento(log, "Actualizando Solicitudes...")
						Try
							Sonda.Gestion.Adm.Sys.IngresoalFondo.ProcesosSaldosApv.modEstadoSolicitud(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)
							evento(log, "Las Solicitudes han sido actualizadas")
						Catch : End Try
					End If

				Case "TRAINCTA"

					actualizarSolicitud(dbc, strDatos.idAdm, strDatos.seqSolSolicitud, strDatos.seqSolEtapa, IIf(estado = 0, "OK", "ER"), strDatos.usu, strDatos.fun)

					If gtipoproceso = "AC" Then
						Dim mensaje As String
						evento(log, "Se va ha actualizar solicitudes")
						Try
							mensaje = Sonda.Gestion.Adm.Sys.IngresoalFondo.ProcesosTrasal.actualizarSolAcreditadas(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)
							evento(log, mensaje)
							evento(log, "Solicitudes han sido actualizadas")
						Catch : End Try
					End If


				Case "TRAINEXT"

					'estado = metodo2.AcredIngresoCTA(idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, IhHebra, LOG, FecAcreditacion, FecValorCuota, PerCuatrimestre, PerContable, PerContableSis, SeqProceso, ValMlCuotaDestinoA, ValMlCuotaDestinoB, ValMlCuotaDestinoC, ValMlCuotaDestinoD, ValMlCuotaDestinoE, PermiteAcreditacionParcial)
					If estado = 0 And gtipoproceso = "AC" Then
						Sys.TraspasoExtranjero.TraspasoExtranjero.acreditacionTrasExt(dbc, strDatos.idAdm, strDatos.NumeroId)
					End If


				Case "TRAINTRA"

					actualizarSolicitud(dbc, strDatos.idAdm, strDatos.seqSolSolicitud, strDatos.seqSolEtapa, IIf(estado = 0, "OK", "ER"), strDatos.usu, strDatos.fun)

				Case "TRAINREZ", "TRAIPAGN", "TRAIPAGC", "TRAINRZC"				'Pago directo, Circ. 1317

					actualizarSolicitud(dbc, strDatos.idAdm, strDatos.seqSolSolicitud, strDatos.seqSolEtapa, IIf(estado = 0, "OK", "ER"), strDatos.usu, strDatos.fun)

				Case "DEVEXCAF", "DEVEXCEM"
					If estado = 0 Then
						'Sys.sysAportesPendientes.Excesos.actualizarSolicitudAcred(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)
						Sys.sysExcesos.Excesos.actualizarSolicitudAcred(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)
					End If

				Case "CAMBFOND"
					If gtipoproceso = "AC" Then
						dbc.BeginTrans()

						Dim DsTipoDistrib As DataSet
						Dim TipDist As String

						'Busca TipoDistribucion en ACR_MOV_CAMBIOS_FONDOS por Numero_ID
						DsTipoDistrib = Sys.IngresoEgreso.CambioFondo.cabecera.buscar_acred(dbc, strDatos.idAdm, strDatos.NumeroId)
						If DsTipoDistrib.Tables(0).Rows.Count() > 0 Then
							TipDist = DsTipoDistrib.Tables(0).Rows(0).Item("TIPO_DISTRIBUCION")
						End If


						'lfc://02-12-2009--- solicitudes acreditadas con error en las trn's ----------------------->>>>>>>>
						'                actualizar acr_mov_cambios_fondos// con respecto a trn's acreditadas
						Sys.IngresoEgreso.CambioFondo.cabecera.actualizarEstadoCambio(dbc, strDatos.idAdm, strDatos.NumeroId, TipDist, gtipoproceso, strDatos.usu, strDatos.fun)

						'                actulizar adm_solicitudes// con respecto a acr_mov_cambios_fondos
						Sys.IngresoEgreso.CambioFondo.cabecera.actualizaSolAcreditadas(dbc, strDatos.idAdm, strDatos.NumeroId, gtipoproceso, strDatos.usu, strDatos.fun)

						'                actualizar porc_dsitribucion en aaa_distribuciones// con respecto a acr_mov
						Sys.IngresoEgreso.CambioFondo.cabecera.actualizaDistSolAcr(dbc, strDatos.idAdm, strDatos.NumeroId, TipDist, strDatos.usu, strDatos.fun)

						'dbc.Commit()
						acr.GenerarLog(dbc, "I", 0, " Actualizacion de Solicitudes.", Nothing, Nothing, Nothing)

					End If

				Case "AJUMASIV"				'OS:7109030 - LFC - 30/06/2015, actualizar estado Liberacion Factor de Ajuste
					'A la espera de definicion por LFC. 18/08/2015.
					'Sys.Liquidacion.liberacionFA.actualizarEstadoAcred(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)

					Try					  '-->>>lfc:RTO10%
						sysRefExternas.RETIRO10.actualizaProvAcred(dbc, strDatos.idAdm, strDatos.NumeroId, gtipoproceso, strDatos.usu, strDatos.fun)
						acr.GenerarLog(dbc, "I", 0, "Actualizacion Estado de Solicitudes", Nothing, Nothing, Nothing)
					Catch : End Try


				Case "PAGOPENS"
					If Left(strDatos.TipoProceso, 2) = "AC" Then
						Dim cantReg As Integer
						Try
							cantReg = sysRefExternas.Ajustes.estadoAcred(dbc, strDatos.idAdm, strDatos.NumeroId, strDatos.usu, strDatos.fun)
							If cantReg > 0 Then
								acr.GenerarLog(dbc, "I", 0, "Actualizacion Estado Ajuste", Nothing, Nothing, Nothing)
							End If
						Catch : End Try
					End If


				Case "ACREXAFC"
					Dim acrExt3 As New Sys.AcreditacionExterna.sysAportesAFC()
					acrExt3.actualizaAcred(dbc, strDatos.idAdm, strDatos.NumeroId, gtipoproceso, strDatos.usu, strDatos.fun)

					acr.GenerarLog(dbc, "I", 0, "Actualizacion Estado AFC. ", Nothing, Nothing, Nothing)

				Case "TRAINCAV"
					'OS-7826035
					If gtipoproceso = "AC" Then
						Dim DsControlAcr As DataSet
						Dim NumRef As Integer
						Dim NumRef2 As Integer
						Dim FecOperacion As Date

						'PCI OS-7826035 Se comenta. Solo es para CAPITAL, para PLV y MOD no van estas lineas.
						DsControlAcr = ControlAcr.buscarProceso(dbc, strDatos.idAdm, strDatos.CodOrigenProceso, strDatos.idUsuarioProceso, strDatos.NumeroId)
						If DsControlAcr.Tables(0).Rows.Count() > 0 Then
							NumRef = DsControlAcr.Tables(0).Rows(0).Item("NUM_REF_ORIGEN1")
							NumRef2 = DsControlAcr.Tables(0).Rows(0).Item("NUM_REF_ORIGEN2")
							FecOperacion = New Date(Mid(NumRef2, 1, 4), Mid(NumRef2, 5, 2), Mid(NumRef2, 7, 2))

							Sys.SolicitudesCAV.sysTraspasoIngresoCAV.actualizaSolCav(dbc, strDatos.idAdm, FecOperacion, NumRef, "ACR", strDatos.usu, strDatos.fun)


						End If

						acr.GenerarLog(dbc, "I", 0, "Actualizacion Estado CAV. ", Nothing, Nothing, Nothing)
					End If

					'lfc:// comision TGR - ca-4048436-->>>
				Case "ACRTGRCO"
					'NCG 145. 25/08/2015.
					Try
						sysRefExternas.ComisionTGR.modificarEstadoComisionTGR(dbc, strDatos.idAdm, strDatos.NumeroId, gtipoproceso, strDatos.usu, strDatos.fun)
						acr.GenerarLog(dbc, "I", 0, "Actualizacion Estado Comision TGR. ", Nothing, Nothing, Nothing)
					Catch ex As Exception
						acr.GenerarLog(dbc, "I", 0, "nOK Estado Comision TGR. " & EX.Message, Nothing, Nothing, Nothing)
					End Try
					'<<-- lfc:// comision TGR - ca-4048436--

				Case "ACRTOOBL"				'lfc:RTO10%
					Try
						acr.GenerarLog(dbc, "I", 0, "Actualizacion Estado de Solicitudes", Nothing, Nothing, Nothing)
						sysRefExternas.RETIRO10.actualizarValorAcred(dbc, strDatos.idAdm, strDatos.NumeroId, gtipoproceso, strDatos.usu, strDatos.fun)
					Catch ex As Exception
						acr.GenerarLog(dbc, "I", 0, "Error Estado Sol: " & EX.Message, Nothing, Nothing, Nothing)
					End Try

				Case "ACRTRAFC"				  ' traspaso de saldo AFC
					Try
						acr.GenerarLog(dbc, "I", 0, "Actualizacion Estados Traspaso saldo AFC", Nothing, Nothing, Nothing)
						sysRefExternas.traspasoSaldoAFC.actualizaEstadoAcred(dbc, strDatos.idAdm, strDatos.NumeroId, gtipoproceso, strDatos.usu, strDatos.fun)
					Catch ex As Exception
						acr.GenerarLog(dbc, "I", 0, "Error: " & EX.Message, Nothing, Nothing, Nothing)
					End Try

				Case "ACRTOPRO"				'2retiro			
					Try
						sysRefExternas.RETIRO10.actualizaProvAcred(dbc, strDatos.idAdm, strDatos.NumeroId, gtipoproceso, strDatos.usu, strDatos.fun)
						acr.GenerarLog(dbc, "I", 0, "Actualizacion Estado de Solicitudes", Nothing, Nothing, Nothing)
					Catch : End Try

				Case "BONO200K"				'bono 200 mil			
					Try
						'sysRefExternas.RETIRO10.actualizaProvAcred(dbc, strDatos.idAdm, strDatos.NumeroId, gtipoproceso, strDatos.usu, strDatos.fun)
						'acr.GenerarLog(dbc, "I", 0, "Actualizacion Estado de Solicitudes", Nothing, Nothing, Nothing)
					Catch : End Try

			End Select

			acr.GenerarLog(dbc, "I", 0, "Fin Proceso " & IIf(gtipoproceso = "AC", "Acreditación", "Simulación"), Nothing, Nothing, Nothing)
			escribirLogEnl("Se ha " & IIf(gtipoproceso = "AC", "Acreditado", "Simulado") & " lote nº " & gnumeroid & " con " & totLote.ToString & "transacciones")
			dbc.Commit()

        End Sub

        'crear archivo de log para procesos en linea
        Public Shared Sub escribirLogEnl(ByVal mensaje As String)
            'se comenta porque no hay acceso desde el web\ a archivos\
            ''Try
            ''    Dim fechaStr As String = Now.Year.ToString.PadLeft(4, "0") & _
            ''                        Now.Month.ToString.PadLeft(2, "0") & _
            ''                        Now.Day.ToString.PadLeft(2, "0")
            ''    Dim horaStr As String = Now.Hour.ToString.PadLeft(2, "0") & _
            ''                            Now.Minute.ToString.PadLeft(2, "0") & _
            ''                            Now.Second.ToString.PadLeft(2, "0") & _
            ''                            Now.Millisecond.ToString.PadLeft(4, "0")

            ''    Dim codArchivo As String = "LogSrvAcred"
            ''    Dim ruta As String = Configuracion.pathControl
            ''    If Not ruta.EndsWith("\") Then ruta &= "\"
            ''    ruta = ruta & "LogSrvAcredENL_" & fechaStr & ".txt"

            ''    Dim esc As New StreamWriter(ruta, True, System.Text.Encoding.Default)
            ''    esc.WriteLine(horaStr & " - " & mensaje)
            ''    esc.Flush()
            ''    esc.Close()
            ''Catch : End Try
        End Sub

        Public Shared Sub cantidadTrsLote(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal numeroId As Long, ByRef numSI As Integer, ByRef numAC As Integer, ByRef numER As Integer, ByRef numPE As Integer, ByRef numTotTrs As Integer)
            Dim dsNumTrs As New DataSet()
            Dim i As Integer
            Dim numTrs As Integer
            numSI = 0 : numAC = 0 : numER = 0 : numPE = 0 : numTotTrs = 0
            dsNumTrs = Transacciones.cuentaEstadosTrn(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, "T")
            With dsNumTrs.Tables(0)
                For i = 0 To .Rows.Count - 1
                    numTrs = IIf(IsDBNull(.Rows(i).Item("VTOTALESTADO")), 0, .Rows(i).Item("VTOTALESTADO"))
                    Select Case .Rows(i).Item("ESTADO_TRANSACCION")
                        Case "AC" : numAC = numTrs
                        Case "SI" : numSI = numTrs
                        Case "ER" : numER = numTrs
                        Case "PE" : numPE = numTrs
                    End Select
                Next i
            End With
            numTotTrs = numAC + numSI + numER + numPE
        End Sub


        Public Sub ValoresAcreditacion(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal numeroId As Integer, ByVal tipoProceso As String, ByVal usu As String, ByVal fun As String, Optional ByVal LOG As Procesos.logEtapa = Nothing)

            Dim numDias As Integer
            Dim i As Integer

            gfecAcreditacion = Sys.Kernel.Parametros.FechaAcreditacion.obtenerFechaAcreditacion(dbc, idAdm, "ACR")
            gfecValorCuota = Sys.Kernel.Parametros.FechaAcreditacion.obtenerFechaValorCuota(dbc, idAdm, "ACR")
            gperCuatrimestre = ParametrosINE.PeriodoCuatrimestral.traer(dbc, idAdm).Tables(0).Rows(0).Item("PER_CUATRIMESTRE")

            gtipoproceso = Left(tipoProceso, 2)
            gnumeroid = numeroId
            gidadm = idAdm
            gidUsuarioProceso = idUsuarioProceso
            gfuncion = fun
            gcodOrigenProceso = codOrigenProceso

            If gfecValorCuota = Nothing Then
                blErrorFatal = True
                GenerarLog(dbc, "I", 15313, "****************************************************************", Nothing, Nothing, Nothing)
                Throw New SondaException(15313) '"Fecha valor cuota es nulo
            End If

            dsAux = ParametrosINE.PeriodoContable.traer(dbc, idAdm)
            If dsAux.Tables(0).Rows.Count = 0 Then
                blErrorFatal = True
                GenerarLog(dbc, "I", 15314, "****************************************************************", Nothing, Nothing, Nothing)
                Throw New SondaException(15314) '"No existe periodo contable
            Else
                gperContable = dsAux.Tables(0).Rows(0).Item("PER_CONTABLE")
                'SIS//--para comprar con cta CAF pago adelantado
                gPerContableSis = dsAux.Tables(0).Rows(0).Item("PER_CONTABLE")
            End If
            ' NUEVO
            CrearEncabezadoAcred(dbc)

            dsAux = ResultadoAcred.EncabezadoAcred.traer(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, gseqProceso)
            If dsAux.Tables(0).Rows.Count > 0 Then
                rCab = New ccEncabezadoAcred(dsAux)
                ValoresAcreditacion1(dbc, idAdm, codOrigenProceso, idUsuarioProceso, numeroId, tipoProceso, usu, fun)
                gExisteEncabezado = True
            Else
                blErrorFatal = True
                GenerarLog(dbc, "I", 15315, "****************************************************************", Nothing, Nothing, Nothing)
                Throw New SondaException(15315) '"Reproceso acreditacion difiere con valores iniciales 

            End If

            rOriPro = Nothing
            dsAux = ParametrosINE.OrigenProceso.traer(dbc, idAdm, codOrigenProceso)
            If dsAux.Tables(0).Rows.Count = 0 Then
                blErrorFatal = True
                evento(LOG, "E - 15341 - Origen Proceso: " & Trim(codOrigenProceso))
                GenerarLog(dbc, "I", 15341, "****************************************************************", Nothing, Nothing, Nothing)
                Throw New SondaException(15341) '"No existe origen proceso

            End If
            rOriPro = New ccAcrOrigenProceso(dsAux)
            blPermiteAcreditacionParcial = rOriPro.indAcreditacion = "S"
        End Sub

        Public Sub ValoresAcreditacion1(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal idUsuarioProceso As String, ByVal numeroId As Integer, ByVal tipoProceso As String, ByVal usu As String, ByVal fun As String)

            Dim i As Integer

            dsAux = ParametrosINE.ValorCuota.obtenerValorCuota(dbc, idAdm, gfecValorCuota, Nothing)
            'dsAux = Parametro.obtenerValorCuotasFondos(dbc, gidAdm, gfecValorCuota, Nothing, 0, 1)
            If dsAux.Tables(0).Rows.Count < 5 Then
                blErrorFatal = True
                GenerarLog(dbc, "I", 15316, "****************************************************************", Nothing, Nothing, Nothing)
                Throw New SondaException(15316) '"No existen todos los valores cuotas
            End If
            For i = 0 To dsAux.Tables(0).Rows.Count - 1
                Select Case dsAux.Tables(0).Rows(i).Item("TIPO_FONDO")
                    Case "A"
                        gvalMlCuotaDestinoA = dsAux.Tables(0).Rows(i).Item("VAL_CUOTA")
                        CrearCuotaAcred(dbc, "A", gvalMlCuotaDestinoA)

                    Case "B"
                        gvalMlCuotaDestinoB = dsAux.Tables(0).Rows(i).Item("VAL_CUOTA")
                        CrearCuotaAcred(dbc, "B", gvalMlCuotaDestinoB)

                    Case "C"
                        gvalMlCuotaDestinoC = dsAux.Tables(0).Rows(i).Item("VAL_CUOTA")
                        CrearCuotaAcred(dbc, "C", gvalMlCuotaDestinoC)

                    Case "D"
                        gvalMlCuotaDestinoD = dsAux.Tables(0).Rows(i).Item("VAL_CUOTA")
                        CrearCuotaAcred(dbc, "D", gvalMlCuotaDestinoD)

                    Case "E"
                        gvalMlCuotaDestinoE = dsAux.Tables(0).Rows(i).Item("VAL_CUOTA")
                        CrearCuotaAcred(dbc, "E", gvalMlCuotaDestinoE)

                End Select
            Next
        End Sub

        Public Sub CrearCuotaAcred(ByRef dbc As OraConn, ByVal tipoFondo As String, ByVal valMlCuota As Decimal)
            Dim ds As DataSet

            If gtipoproceso = "AC" Then

                ds = ResultadoAcred.CuotasAcred.traer(dbc, gidadm, gnumeroid, 0, tipoFondo)
                If ds.Tables(0).Rows.Count > 0 Then
                    If CDec(ds.Tables(0).Rows(0).Item("VAL_ML_VALOR_CUOTA")) <> valMlCuota Then
                        GenerarLog(dbc, "I", 15015, "****************************************************************1", Nothing, Nothing, Nothing)
                        Throw New SondaException(15015) ' Los parametros utilizados en la simulacion difieren de los de la acreditación
                    End If
                End If
            End If
            ResultadoAcred.CuotasAcred.crear(dbc, gidadm, gnumeroid, gseqProceso, tipoFondo, valMlCuota, gidUsuarioProceso, gfuncion)
        End Sub


        Private Sub CrearEncabezadoAcred(ByRef dbc As OraConn)
            Dim ds As DataSet
            If gtipoproceso = "AC" Then
                ds = ResultadoAcred.EncabezadoAcred.traer(dbc, gidadm, gcodOrigenProceso, gidUsuarioProceso, gnumeroid, 0)
                If ds.Tables(0).Rows.Count > 0 Then
                    If CDate(ds.Tables(0).Rows(0).Item("FEC_ACREDITACION")) <> gfecAcreditacion Then
                        blErrorFatal = True
                        GenerarLog(dbc, "I", 15015, "****************************************************************2", Nothing, Nothing, Nothing)
                        Throw New SondaException(15015) ' Los parametros utilizados en la simulacion n de los de la acreditación
                    End If

                    If CDate(ds.Tables(0).Rows(0).Item("FEC_VALOR_CUOTA")) <> gfecValorCuota Then
                        blErrorFatal = True
                        GenerarLog(dbc, "I", 15015, "****************************************************************3", Nothing, Nothing, Nothing)
                        Throw New SondaException(15015) ' Los parametros utilizados en la simulacion n de los de la acreditación
                    End If

                End If
            End If
            gseqProceso = ResultadoAcred.EncabezadoAcred.crearConSecuencia(dbc, gidadm, gcodOrigenProceso, gidUsuarioProceso, _
                                      gnumeroid, Left(gtipoproceso, 2), Now, gnumOrigenRef, gfecAcreditacion, _
                                      gperContable, gfecValorCuota, gidUsuarioProceso, gfuncion)

            'PCI
            dbc.Commit()
        End Sub

        Private Function TotalesControlAcreditacion(ByRef dbc As OraConn, ByVal TotIgn As Integer, ByVal TotAcr As Integer, ByVal TotSim As Integer, ByRef EstadoLote As String) As Integer
            gEstadoError = "ER"

            Try
                'Carga Tabla ACR_TOTALES_ACRED
                ResultadoAcred.TotalesAcred.sumarTotales(dbc, gidadm, gcodOrigenProceso, gidUsuarioProceso, gnumeroid, gseqProceso, gidUsuarioProceso, gfuncion)

            Catch e As Exception
                GenerarLog(dbc, "E", 0, e.ToString, Nothing, Nothing, Nothing)
                If Not IsNothing(dbc) Then dbc.Rollback()
                'Dim sm As New SondaExceptionManager(e)
                'TotalesControlAcreditacion = 1
                'blPermiteAcreditacionParcial = False
            End Try

            If TotIgn > 0 Then

                If Not blPermiteAcreditacionParcial Then 'ERROR
                    dbc.Rollback()
                    ControlAcr.modTotCacrConCommit(dbc, gidadm, gcodOrigenProceso, gidUsuarioProceso, gnumeroid, gseqProceso, Nothing, Nothing, 0, gEstadoError, gidUsuarioProceso, gfuncion)
                    EstadoLote = gEstadoError
                    TotalesControlAcreditacion = 1
                Else

                    If gtipoproceso = "SI" Then 'SIMULADO PARCIAL
                        ControlAcr.modTotCacrConCommit(dbc, gidadm, gcodOrigenProceso, gidUsuarioProceso, gnumeroid, gseqProceso, gfecAcreditacion, Nothing, TotSim, "SP", gidUsuarioProceso, gfuncion)
                        EstadoLote = "SP"
                    Else ' ACREDITADO PARCIAL
                        'Carga Tablas AUXILIARES para Transacciones AC
                        Transacciones.cargaAuxiliaresAbocarSaldos(dbc, gidadm, gcodOrigenProceso, gidUsuarioProceso, gnumeroid, gidUsuarioProceso, gfuncion)

                        ControlAcr.modTotCacrConCommit(dbc, gidadm, gcodOrigenProceso, gidUsuarioProceso, gnumeroid, gseqProceso, gfecAcreditacion, Nothing, TotAcr, "AP", gidUsuarioProceso, gfuncion)
                        EstadoLote = "AP"
                    End If
                    TotalesControlAcreditacion = 0
                End If
            Else

                If Not blPermiteAcreditacionParcial Then
                    'blIgnorar = Not RegistroContable()
                    blIgnorar = False
                    If blIgnorar Then
                        gTotRegistrosIgnorados = TotAcr
                        dbc.Rollback()
                        ControlAcr.modTotCacrConCommit(dbc, gidadm, gcodOrigenProceso, gidUsuarioProceso, gnumeroid, gseqProceso, Nothing, Nothing, 0, gEstadoError, gidUsuarioProceso, gfuncion)
                        EstadoLote = gEstadoError
                        TotalesControlAcreditacion = 1
                    Else
                        dbc.Commit()
                        gEstadoError = gtipoproceso
                        If gtipoproceso = "SI" Then 'SIMULADO 

                            ControlAcr.modTotCacrConCommit(dbc, gidadm, gcodOrigenProceso, gidUsuarioProceso, gnumeroid, gseqProceso, gfecAcreditacion, Nothing, TotSim, "SI", gidUsuarioProceso, gfuncion)
                            EstadoLote = "SI"
                        Else
                            If TotSim = 0 Then ' Al Acreditar Verifica que no existan Transacciones en estado SI
                                'Carga Tablas AUXILIARES para Transacciones AC
                                Transacciones.cargaAuxiliaresAbocarSaldos(dbc, gidadm, gcodOrigenProceso, gidUsuarioProceso, gnumeroid, gidUsuarioProceso, gfuncion)

                                ' ACREDITADO 
                                ControlAcr.modTotCacrConCommit(dbc, gidadm, gcodOrigenProceso, gidUsuarioProceso, gnumeroid, gseqProceso, gfecAcreditacion, Nothing, TotAcr, "AC", gidUsuarioProceso, gfuncion)
                                EstadoLote = "AC"
                            Else
                                'Carga Tablas AUXILIARES para Transacciones AC
                                Transacciones.cargaAuxiliaresAbocarSaldos(dbc, gidadm, gcodOrigenProceso, gidUsuarioProceso, gnumeroid, gidUsuarioProceso, gfuncion)

                                'Si existen transacciones en estado SI apesar de que se cambiara estado a AC, 
                                'se dejara en estado AP
                                ControlAcr.modTotCacrConCommit(dbc, gidadm, gcodOrigenProceso, gidUsuarioProceso, gnumeroid, gseqProceso, gfecAcreditacion, Nothing, TotAcr, "AP", gidUsuarioProceso, gfuncion)
                                EstadoLote = "AP"
                            End If
                        End If
                        TotalesControlAcreditacion = 0
                    End If
                Else
                    If gtipoproceso = "SI" Then 'SIMULADO 

                        ControlAcr.modTotCacrConCommit(dbc, gidadm, gcodOrigenProceso, gidUsuarioProceso, gnumeroid, gseqProceso, gfecAcreditacion, Nothing, TotSim, "SI", gidUsuarioProceso, gfuncion)
                        EstadoLote = "SI"
                    Else
                        If TotSim = 0 Then ' Al Acreditar Verifica que no existan Transacciones en estado SI
                            'Carga Tablas AUXILIARES para Transacciones AC
                            Transacciones.cargaAuxiliaresAbocarSaldos(dbc, gidadm, gcodOrigenProceso, gidUsuarioProceso, gnumeroid, gidUsuarioProceso, gfuncion)

                            ' ACREDITADO 
                            ControlAcr.modTotCacrConCommit(dbc, gidadm, gcodOrigenProceso, gidUsuarioProceso, gnumeroid, gseqProceso, gfecAcreditacion, Nothing, TotAcr, "AC", gidUsuarioProceso, gfuncion)
                            EstadoLote = "AC"
                        Else
                            'Carga Tablas AUXILIARES para Transacciones AC
                            Transacciones.cargaAuxiliaresAbocarSaldos(dbc, gidadm, gcodOrigenProceso, gidUsuarioProceso, gnumeroid, gidUsuarioProceso, gfuncion)

                            'Si existen transacciones en estado SI apesar de que se cambiara estado a AC, 
                            'se dejara en estado AP
                            ControlAcr.modTotCacrConCommit(dbc, gidadm, gcodOrigenProceso, gidUsuarioProceso, gnumeroid, gseqProceso, gfecAcreditacion, Nothing, TotAcr, "AP", gidUsuarioProceso, gfuncion)
                            EstadoLote = "AP"
                        End If

                    End If
                    TotalesControlAcreditacion = 0
                End If
            End If
        End Function

        Private Sub IniciarLog(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal idUsuarioProceso As String, ByVal numeroId As Integer, ByVal usu As String, ByVal fun As String, Optional ByRef LOG As Procesos.logEtapa = Nothing, Optional ByVal pEnLinea As Boolean = False)

            Dim version As String
            Dim fecha As String


            If pEnLinea Then
                version = "4.0.1.*"
                fecha = "11-08-2016"
                gSeqLog = 1
            Else
                version = "4.0.0.*"
                fecha = "01-08-2011"
                gSeqLog = 1

            End If


            ControlAcr.LogAcreditacion.eliminarTodos(dbc, idAdm, numeroId)

            ControlAcr.LogAcreditacion.crearLogHeb(dbc, idAdm, numeroId, 0, gSeqLog, "I", 0, "Inicio Proceso (Versión acreditador " & version & "  " & fecha & ")", 0, Nothing, Nothing, idUsuarioProceso, fun)

            If gtipoproceso = "AC" And usu.ToUpper <> gidUsuarioProceso.ToUpper Then

                GenerarLog(dbc, "I", 0, "Usuario Proceso: " & gidUsuarioProceso.ToLower & "  Usuario Acreditacion: " & usu.ToLower, Nothing, Nothing, Nothing)

            End If

        End Sub

        Public Sub GenerarLog(ByRef dbc As OraConn, ByVal tipoMensaje As String, ByVal codMensaje As Integer, ByVal mensaje As String, ByVal seqTransaccion As Object, ByVal idPersona As Object, ByVal idCliente As Object)

            Dim i As Integer = 0
            Dim largoTrama As Integer = 200
            Dim maxLargo As Integer = 6000

            If IsNothing(seqTransaccion) Then seqTransaccion = 0
            If IsNothing(idCliente) Then idCliente = 0
            If IsNothing(idPersona) Then idPersona = Nothing

            If IsNothing(mensaje) Then
                mensaje = "Detalle del error no especificado"
            End If

            Dim j As Integer = mensaje.Length
            If j > maxLargo Then j = maxLargo

            While i < j
                gSeqLog = gSeqLog + 1
                If j - i < largoTrama Then
                    ControlAcr.LogAcreditacion.crearLogHeb(dbc, gidadm, gnumeroid, 0, gSeqLog, tipoMensaje, codMensaje, mensaje.Substring(i, j - i), seqTransaccion, idPersona, idCliente, gidUsuarioProceso, gfuncion)
                    j = i
                Else
                    ControlAcr.LogAcreditacion.crearLogHeb(dbc, gidadm, gnumeroid, 0, gSeqLog, tipoMensaje, codMensaje, mensaje.Substring(i, largoTrama), seqTransaccion, idPersona, idCliente, gidUsuarioProceso, gfuncion)
                    i = i + largoTrama
                End If
            End While
        End Sub


        Public Sub AperturaTransa(ByRef dbc As OraConn)
            Dim dsRtrn As New DataSet()
            Dim error_ape As Integer
            Dim seq_log As Integer

            EstadoAcreditacion(dbc, DeterminaEstadoAcreditacion(gtipoproceso))

            If gcodOrigenProceso = "RECAUDAC" Or gcodOrigenProceso = "TRAINREZ" Or _
               gcodOrigenProceso = "TRAIPAGN" Or gcodOrigenProceso = "TRAIPAGC" Or _
               gcodOrigenProceso = "TRAINRZC" Or gcodOrigenProceso = "REREZSEL" Or _
               gcodOrigenProceso = "ACREXIPS" Or gcodOrigenProceso = "ACREXSTJ" Or _
               gcodOrigenProceso = "ACREXTBF" Or _
               gcodOrigenProceso = "ACREXAFC" Or _
               gcodOrigenProceso = "TRAINCHP" Or _
               gcodOrigenProceso = "REREZMAS" Or gcodOrigenProceso = "REREZCON" Or _
               gcodOrigenProceso = "ACREXTGR" Or gcodOrigenProceso = "AEXDVSTJ" Then

                'MSC: Agregar este bloque de codigo en este mismo punto
                If gtipoproceso <> "AC" Then
                    GenerarLog(dbc, "I", 0, "Se inicia proceso apertura de transacciones", Nothing, Nothing, Nothing)

                    ' apertura transacciones
                    dsRtrn = Transacciones.aperturaTransaccion(dbc, gidadm, gcodOrigenProceso, gidUsuarioProceso, gnumeroid, gtipoproceso)
                    error_ape = dsRtrn.Tables(0).Rows(0).Item("verror_ape")
                    seq_log = dsRtrn.Tables(0).Rows(0).Item("vseq_log")

                    gSeqLog = seq_log  ' se asigna variable obtenida de la llamada del procedimiento

                    If error_ape <> 0 Then
                        blErrorFatal = True
                        GenerarLog(dbc, "E", 99999, "Error fatal en apertura de transacciones", 0, Nothing, Nothing)
                        'evento(LOG, "Hebra " & IdHebra & " - Error fatal en apertura de transacciones")

                        'INI - Se espera a catalogar en PRODUCCION. OS-7147818.

                        'Verifica Tipo de Proceso para Modificar Estado Lote.
                        If gtipoproceso = "SI" Then
                            EstadoAcreditacion(dbc, "SP")
                        Else
                            EstadoAcreditacion(dbc, "AP")
                        End If

                        'FIN - Se espera a catalogar en PRODUCCION. OS-7147818.

                        Throw New SondaException(99999) 'No existen registros para acreditar
                    End If

                    GenerarLog(dbc, "I", 0, "Proceso apertura de transacciones finalizado", Nothing, Nothing, Nothing)


                    'Buscar en ACR_TRANSACCIONES IND_ADIC_TRANSF = 0 (todos) no se a realizado el ordenamiento,
                    'Se realiza el ordenamiento. si IND_ADIC_TRANSF = 1 ya se realizo y se salta.


                    GenerarLog(dbc, "I", 0, "Finaliza Ornadenamiento Previo.", Nothing, Nothing, Nothing)

                End If
                'FIN MSC 

            End If
        End Sub

        Private Sub EstadoAcreditacion(ByRef dbc As OraConn, ByVal estadoAcreditacion As String)
            ControlAcr.modEstadoCacrConCommit(dbc, gidadm, gcodOrigenProceso, gidUsuarioProceso, gnumeroid, estadoAcreditacion, gfecAcreditacion, gidUsuarioProceso, gfuncion)
        End Sub

        Private Function DeterminaEstadoAcreditacion(ByVal tipoProceso As String) As String
            Select Case tipoProceso
                Case "SI"
                    DeterminaEstadoAcreditacion = "PS"
                Case "AC"
                    DeterminaEstadoAcreditacion = "PA"
            End Select
        End Function
    End Class

End Class
