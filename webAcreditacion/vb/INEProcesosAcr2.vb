Option Strict Off

Imports Sonda.Gestion.Adm.Sys
Imports Sonda.Gestion.Adm.Sys.IngresoEgreso
Imports Sonda.Gestion.Adm.Sys.Soporte
Imports Sonda.Gestion.Adm.Sys.Kernel
Imports Sonda.Gestion.Adm.Sys.sysAportesPendientes
Imports Sonda.Gestion.Adm.Sys.IngresoalFondo
Imports Sonda.Gestion.Adm.Sys.Cobranzas
Imports Sonda.Gestion.Adm.Sys.CodeCompletion.ACR
Imports Sonda.Gestion.Adm.Sys.CodeCompletion.APA
Imports Sonda.Gestion.Adm.Sys.CodeCompletion.AAA
Imports Sonda.Gestion.Adm.Sys.CodeCompletion.PAR
Imports Sonda.Gestion.Adm.Sys.CodeCompletion.EGR
Imports Sonda.Gestion.Adm.Sys.CodeCompletion.AIF
Imports Sonda.Gestion.Adm.WS
'Imports Sonda.Gestion.Adm.bl.blRetirosSap
Imports Sonda.Net.DB
Imports Sonda.Net
Imports System.IO


Public Class INEProcesosAcr2
    Dim dbc As OraConn

    Dim gFecInicioSistema As Date = New Date(1981, 5, 1)
    Dim parTipCom As New INEParametrosDS.ParametroGeneral("PAR_ACR_TIPO_COMISION")
    Dim parCodMvto As New INEParametrosDS.ParametroGeneral("PAR_ACR_MVTO_ACREDITACION")
    Dim parCodMvtoRez As New INEParametrosDS.ParametroGeneral("PAR_ACR_MVTO_REZAGO")
    Dim parTipoPlanilla As New INEParametrosDS.ParametroGeneral("PAR_ING_TIPOS_PLANILLAS")

    Dim parValorUf As New INEParametrosDS.ParametroGeneral("PAR_VALOR_UF")
    Dim parComision As New INEParametrosDS.Comision()
    'Dim parParametrosAcred As New INEParametrosDS.ParametroGeneral("PAR_ACR_PARAMETROS_ACRED")

    Dim clsAux As INETotalesAcr.Auxiliar
    Dim clsConta As WS.IngresoEgresoConta.auxiliarContabilidad.Contabilidad
    Dim clsSal As INESaldo
    Dim sMov As INEMovimiento

    Dim II As Integer
    Dim gNoExiste As Boolean
    Dim gExisteConRen As Boolean

    Dim gExesoTope As Boolean
    Dim gExesoTopeSis As Boolean

    Dim gExisteConRenSIS As Boolean
    Dim gCrearConRenSis As Boolean


    Dim gCrearConRen As Boolean
    Dim gExisteEncabezado As Boolean

    Dim dsTrnCur As DataSet
    Dim dsValRez As DataSet
    Dim dsAux As DataSet
    Dim dsAuxSIS As DataSet
    Dim dsMov As DataSet

    'Dim dsTP, dsP, dsS, dsCR As DataSet


    Dim rRez As ccRezagos
    'Dim rMov As ccSaldosMovimientos
    Dim rTrn As ccTransacciones
    Dim rTrf As ccTransfApv
    Dim rCli As ccClientes
    Dim rTPr As ccTiposProducto
    Dim rPro As ccProductos
    Dim rSal As ccSaldos
    Dim rDis As ccDistribuciones
    Dim rCam As ccMovCambiosFondos
    Dim rProEmp As ccTiposProductoEmpleadores

    Dim rConRen As ccControlRentas
    Dim rConRenSIS As ccControlRentas
    Dim rTipComPor As ccAcrTipoComision
    Dim rTipComFij As ccAcrTipoComision
    Dim rComPor As ccAcrComisiones
    Dim rComFij As ccAcrComisiones
    Dim rPri As ccPrimasCiasSeg
    Dim rMovAcr As ccAcrMvtoAcreditacion
    Dim rMovAcr2 As ccAcrMvtoAcreditacion
    Dim rCauRez As ccApaCausalrezago

    Dim rFecAcr As ccFechaAcreditacion
    Dim rCab As ccEncabezadoAcred
    Dim rTot As ccTotalesAcred
    Dim rCuo As ccCuotasAcred
    Dim rDet As ccDetalleAcred
    Dim rComis As ccComisiones
    Dim rOriPro As ccAcrOrigenProceso

    Dim rAuxSal As ccAuxiliarSaldos
    Dim rAuxAboCar As ccAuxiliarAbocar



    'Dim aTpr As INERegistros.tiposProductos
    'Dim aPro As INERegistros.Productos
    'Dim aSal As INERegistros.saldos
    'Dim aConRen As INERegistros.controlRentas

    Dim blExcesosIndep As Boolean
    Dim blAdicionalAntiguo As Boolean

    Dim blIgnorar As Boolean
    Dim blIgnorarCliente As Boolean = False
    Dim blIgnorarProceso As Boolean = False
    Dim blAcreditarARezago As Boolean
    Dim blErrorFatal As Boolean
    Dim gEstadoError As String
    Dim sqlError As Long

    Dim gcausalRezago As String
    Dim gSeqLog As Integer

    Dim gfecUFRenta As Date
    Dim gvalorUF As Decimal
    Dim gcomisionPorcentual As Decimal
    Dim gcomisionFija As Decimal

    Dim gidAdm As Integer
    Dim gcodOrigenProceso As String
    Dim gidUsuarioProceso As String
    Dim gnumeroId As Long
    Dim gseqProceso As Decimal
    Dim gnumOrigenRef As Decimal
    Dim gtipoProceso As String
    Dim gIdHebra As Integer
    Dim gValDif As Decimal

    Dim gvalMlCuotaDestinoA As Double
    Dim gvalMlCuotaDestinoB As Double
    Dim gvalMlCuotaDestinoC As Double
    Dim gvalMlCuotaDestinoD As Double
    Dim gvalMlCuotaDestinoE As Double

    Dim gdbc As OraConn

    Dim gEsConvenio As Boolean


    Dim gvalMlCuotaAct As Decimal

    Dim gfecAcreditacion As Date
    Dim gfecValorCuota As Date
    Dim gfecValorCuotaAct As Date

    Dim gperContable As Date
    Dim gperCuatrimestre As Date
    Dim gfuncion As String
    Dim gcodCausalRezagoCal As String
    Dim gcodErrorIgnorar As Integer

    Dim gTotRegistrosIgnorados As Integer
    Dim gTotRegistrosSimulados As Integer
    Dim gTotRegistrosAcreditados As Integer

    Dim gRegistros As Integer
    Dim gRegistrosIgnorados As Integer
    Dim gRegistrosEnviados As Integer
    Dim gRegistrosCalculados As Integer
    Dim gRegistrosAcreditados As Integer
    Dim gRegistrosAjustes As Integer
    Dim gRegistrosCompen As Integer

    Dim gvalMlIgnorados As Decimal
    Dim gvalCuoIgnorados As Decimal

    Dim gvalMlMvto As Decimal
    Dim gvalMlAdicional As Decimal
    Dim gvalMlExceso As Decimal
    Dim gvalMlExcesoEmpl As Decimal
    Dim gvalMlComisiones As Decimal

    Dim gPerContableSis As Date   '---------------SIS
    Dim gvalMlPrimaSis As Decimal '---------------SIS
    Dim gvalCuoPrimaSis As Decimal '--------------SIS
    Dim gvalMlPrimaSisCal As Decimal '------------SIS
    Dim gvalCuoPrimaSisCal As Decimal '-----------SIS

    Dim gvalCuoMvto As Decimal
    Dim gvalCuoAdicional As Decimal
    Dim gvalCuoExceso As Decimal
    Dim gvalCuoExcesoEmpl As Decimal
    Dim gvalCuoComisiones As Decimal

    Dim gvalMlMvtoCal As Decimal
    Dim gvalMlAdicionalCal As Decimal
    Dim gvalMlExcesoCal As Decimal
    Dim gvalMlExcesoEmplCal As Decimal
    Dim gvalMlComisionesCal As Decimal
    Dim gvalMlPatrFrecCal As Decimal
    Dim gvalMlPatrFrecActCal As Decimal
    Dim gvalMlPatrFdesCal As Decimal
    Dim gvalMlPrimaCal As Decimal

    Dim gvalMlTransferenciaCal As Decimal
    Dim gvalCuoTransferenciaCal As Decimal


    Dim gvalMlCompenCarCal As Decimal
    Dim gvalMlCompenAboCal As Decimal

    Dim gvalCuoMvtoCal As Decimal
    Dim gvalCuoAdicionalCal As Decimal
    Dim gvalCuoExcesoCal As Decimal
    Dim gvalCuoExcesoEmplCal As Decimal
    Dim gvalCuoComisionesCal As Decimal
    Dim gvalCuoPatrFrecCal As Decimal
    Dim gvalCuoPatrFrecActCal As Decimal
    Dim gvalCuoPatrFdesCal As Decimal
    Dim gvalCuoPrimaCal As Decimal

    Dim gvalCuoAjuDecCal As Decimal

    Dim gvalCuoCompenCarCal As Decimal
    Dim gvalCuoCompenAboCal As Decimal

    Dim gvalMlAbonosCtaCal As Decimal
    Dim gvalMlCargosCtaCal As Decimal
    Dim gvalCuoAbonosCtaCal As Decimal
    Dim gvalCuoCargosCtaCal As Decimal

    Dim gvalMlAbonosCtaAcr As Decimal
    Dim gvalMlCargosCtaAcr As Decimal
    Dim gvalCuoAbonosCtaAcr As Decimal
    Dim gvalCuoCargosCtaAcr As Decimal

    Dim gvalMlCargosComAcr As Decimal
    Dim gvalCuoCargosComAcr As Decimal

    Dim gvalMlCompenCarAcr As Decimal
    Dim gvalMlCompenAboAcr As Decimal
    Dim gvalCuoCompenCarAcr As Decimal
    Dim gvalCuoCompenAboAcr As Decimal

    Dim gvalMlTransferenciaAcr As Decimal
    Dim gvalCuoTransferenciaAcr As Decimal

    Dim gvalMlCargosCambFond As Decimal
    Dim gvalMlAbonosCambFond As Decimal
    Dim gvalCuoCargosCambFond As Decimal
    Dim gvalCuoAbonosCambFond As Decimal

    Dim gnumTransCambFond As Integer
    Dim gcodAdministradora As Integer

    Dim gvalCuoImpuestoCal As Decimal
    Dim gvalMlImpuestoCal As Decimal
    Dim blPermiteAcreditacionParcial As Boolean
    Dim gIdClienteAnterior As Integer = -1
    Dim gAcreditadorExcedido As Boolean = False
    Dim gvalMlPatrDistFondoC As Decimal = 0
    Dim gAdicionalSeTransfiere As Boolean
    Dim gDescripcionCausalRezago As String
    Dim commitParcial As Boolean = False
    Dim gtipoFondoDestinoOriginal As String
    Dim m As Long
    Friend Shared codAdmF As Object

    Dim gUsuarioEjecProc As String

    'rez antiguos
    Dim g_valMlAdicional As Decimal = 0
    Dim g_valMlAdicionalInt As Decimal = 0
    Dim g_valMlAdicionalRea As Decimal = 0

    'ajuste Decimal a comision
    Dim g_valMlAjusteDec As Decimal = 0
    Dim g_valCuoAjusteDec As Decimal = 0
    Dim ValAjusteCom As Decimal

    Dim gBuscaError As String = "PCI"

    Dim gEnLinea As Boolean = False
    Dim blSoloSISAboCar As Boolean = False
    Dim blRecalculaInteres As Boolean = False

    Dim blGenExcesoEnLinea As Boolean = False ' os:9075964 - controla el exceso por tope "en linea" para PLV
    Dim gcontrolRentasEnLinea As String
    Dim valMlRIMCotExcesoGen As Decimal = 0
    Dim valMlRIMSISExcesoGen As Decimal = 0
    Dim blGenExcesos As Boolean = False

    Dim blValorizaCuotaFdoDest As Boolean = True

    Dim blSaldoNegativo As Boolean = False ' casos especiales, saldo negativo luego de aplicar rentabilidad negativa

    Dim blRentabilidadRez As Boolean = False 'OS-10410740 --rentabilidad del rezago separada en la trs

    ' lfc: ca-1214034  expasis en TGR 20-05-2019
    Dim blExpasisTGR As Boolean = False

    'transaferencia de comision----
    'Dim gValMlComisionTRF As Decimal
    'Dim gValCuoComisionTRF As Decimal

    '--LFC: CA-3927246 - NO COMISION PAGOS TGR
    Dim blNoComisionTGR As Boolean = True

    'lfc: NCG-255 
    Dim blNocional As Boolean = True ' trs que afecta el saldo nocional

	'lfc: NCG-264 - 25/04/2020
	Dim blCongelaSaldo As Boolean = True

    Public Function AcredRecaudac(ByRef dbc As OraConn, _
                                  ByVal idAdm As Integer, _
                                  ByVal codOrigenProceso As String, _
                                  ByVal idUsuarioProceso As String, _
                                  ByVal numeroId As Long, _
                                  ByVal tipoProceso As String, _
                                  ByVal IdHebra As Integer, _
                                  ByVal LOG As Procesos.logEtapa, _
                                  ByVal FecAcreditacion As Date, _
                                  ByVal FecValorCuota As Date, _
                                  ByVal PerCuatrimestre As Date, _
                                  ByVal PerContable As Date, _
                                  ByVal PerContableSis As Date, _
                                  ByVal SeqProceso As Decimal, _
                                  ByVal ValMlCuotaDestinoA As Decimal, _
                                  ByVal ValMlCuotaDestinoB As Decimal, _
                                  ByVal ValMlCuotaDestinoC As Decimal, _
                                  ByVal ValMlCuotaDestinoD As Decimal, _
                                  ByVal ValMlCuotaDestinoE As Decimal, _
                                  ByVal PermiteAcreditacionParcial As Boolean) As Long


        Dim iniciarConexion As Boolean = True
        Dim dsRtrn As New DataSet()
        Dim error_ape As Integer
        Dim seq_log As Integer

        Try

            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gseqProceso = 0
            gtipoProceso = tipoProceso
            gfuncion = "AcredRecaudac"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()

            'EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            ''MSC: Agregar este bloque de codigo en este mismo punto
            'If tipoProceso <> "AC" Then
            '    evento(LOG, "Hebra " & IdHebra & " - Se inicia proceso apertura de transacciones")

            '    ' apertura transacciones
            '    dsRtrn = Transacciones.aperturaTransaccion(gdbc,gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId, gtipoProceso)
            '    error_ape = dsRtrn.Tables(0).Rows(0).Item("verror_ape")
            '    seq_log = dsRtrn.Tables(0).Rows(0).Item("vseq_log")

            '    If error_ape <> 0 Then
            '        blErrorFatal = True
            '        GenerarLog("E", 99999, Nothing, IdHebra, 0, Nothing, Nothing)
            '        evento(LOG, "Hebra " & IdHebra & " - Error fatal en apertura de transacciones")
            '        Throw New SondaException(99999) 'No existen registros para acreditar
            '    End If

            '    evento(LOG, "Hebra " & IdHebra & " - Proceso apertura de transacciones finalizado")

            '    gSeqLog = seq_log  ' se asigna variable obtenida de la llamada del procedimiento

            'End If
            ''FIN MSC 

            evento(LOG, "Hebra " & IdHebra & " - Se inicia el proceso de acreditacion")

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc, idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)

            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)
            gdbc.Rollback()


            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'evento(LOG, "Hebra " & IdHebra & " - No se encontraron registros para acreditar")
                'Throw New SondaException(15307) 'No existen registros para acreditar

            Else
                'dbc.BeginTrans()
                'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
                'ValoresAcreditacion()
                gfecAcreditacion = FecAcreditacion
                gfecValorCuota = FecValorCuota
                gperCuatrimestre = PerCuatrimestre
                gperContable = PerContable
                gPerContableSis = PerContableSis
                gseqProceso = SeqProceso
                gvalMlCuotaDestinoA = ValMlCuotaDestinoA
                gvalMlCuotaDestinoB = ValMlCuotaDestinoB
                gvalMlCuotaDestinoC = ValMlCuotaDestinoC
                gvalMlCuotaDestinoD = ValMlCuotaDestinoD
                gvalMlCuotaDestinoE = ValMlCuotaDestinoE
                blPermiteAcreditacionParcial = PermiteAcreditacionParcial

                'dbc.Commit()

                If Not blPermiteAcreditacionParcial Then
                    gdbc.BeginTrans()
                End If

                For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1

                    commitParcial = commitParcial Or (II + 1) Mod 1000 = 0

                    rTrn = Nothing
                    rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                    If rTrn.seqRegistro = 1129848620 Then
                        rTrn.seqRegistro = 1129848620
                    End If


                    If blPermiteAcreditacionParcial And iniciarConexion Then 'rTrn.idCliente <> gIdClienteAnterior Then
                        gdbc.BeginTrans()
                        iniciarConexion = False
                    End If

                    If rTrn.tipoEntidadPagadora = "V" And (rTrn.tipoPago = 2 Or rTrn.tipoPago = 3) And _
                       rTrn.tipoProducto = "CCO" And rTrn.valMlExcesoLinea > 0 Then 'Afil. Independiente Voluntario(V) y Atrasado(2) y Solo Obligatoria.

                        blExcesosIndep = True
                    Else
                        blExcesosIndep = False
                    End If


                    If rTrn.codMvtoAdi = "110358" And gcodAdministradora = 1032 Then 'Solo para Planvital se excluyen Adic. Antiguo.OS-7079391. 09/03/2015. OS-7243919 01/04/2016
                        blAdicionalAntiguo = True
                    Else
                        blAdicionalAntiguo = False
                    End If

                    'OS-7243919. PCI La siguiente linea es para no considerar Adicional Antiguo. Cuando se Apruebe 
                    '                se debe eliminar
                    blAdicionalAntiguo = False

                    LimpiarDatos()
                    LlenaValoresIniciales()

                    'MSC: Estas lineas se comentan                                
                    'ValorCuotaCaja()
                    'CalcularPatrimonioFechaOperacion()
                    'fin MSC
                    DeterminaMontoInstitucionSalud()

                    g_valCuoAjusteDec = 0
                    g_valMlAjusteDec = 0

                    '--.--If (rTrn.tipoProducto = "CCV" Or rTrn.tipoProducto = "CDC") And rTrn.codDestinoTransaccion <> "REZ" Then
                    If rTrn.codDestinoTransaccion <> "REZ" Then

                        DeterminarTransferencia()

                        If rTrn.valMlTransferenciaCal > 0 And rTrn.codDestinoTransaccion <> "TRF" Then
                            AcreditaTransferencia() 'viene cotizacion y transferencia
                        End If

                    End If

                    If rTrn.codDestinoTransaccionCal = "REZ" Then
                        blAcreditarARezago = True
                    End If

                    ValidarDatosBasicos()

                    If rTrn.codDestinoTransaccion <> "REZ" Then

                        '-->>>lfc:19/05 --se crean cuentas CCV saldo=0 y regTrib null
                        If Not blAcreditarARezago And Not blIgnorar Then

                            gcausalRezago = LeerDatosCliente()

                            If Not blAcreditarARezago And Not blIgnorar Then
                                ValidarParaAcreditacion()
                            End If
                        End If
                    End If

                    ''CAVCAI
                    If rTrn.tipoProducto = "CAV" And rTrn.codDestinoTransaccionCal = "CTA" And IsNothing(Trim(rTrn.categoria)) Then
                        'Sin reg.Tributario, es decir sin Categoria. OS-4427354
                        rTrn.codCausalRezagoCal = "14"
                        blAcreditarARezago = True
                    End If


                    Select Case True
                        Case blIgnorar

                        Case rTrn.codDestinoTransaccion = "TRF"
                            AcreditaTransferencia() 'vien solo transferencia sin cotizacion

                        Case blAcreditarARezago
                            descripcionCausalRez()
                            AcreditaRezago()

                        Case rTrn.codDestinoTransaccion = "REZ"
                            blAcreditarARezago = True
                            AcreditaRezago()

                        Case rMovAcr.tipoMvto = "DEC"
                            AcreditaDeclaraciones()

                        Case blExcesosIndep
                            AcreditaExcesosIndepend()

                        Case rMovAcr.indCotizacion = "S"

                            If rTrn.perCotizacion >= New Date(2009, 7, 1).Date And (rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF") Then
                                AcreditaCotREC2()   '-----------------------PRIMA SIS---------------------
                            Else
                                AcreditaCotREC()
                            End If


                        Case rMovAcr.tipoMvto = "PER"

                        Case Else
                            blIgnorar = True
                            rTrn.codError = 15308 'Tipo movimiento no fue reconocido
                            GenerarLog("A", 15308, "Hebra " & IdHebra & " - Tipo movimiento: " & rMovAcr.tipoMvto, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                    End Select

                    ValidaFondoDestinoInicial()

                    Select Case True
                        Case blIgnorar
                            IgnorarRegistroTrn()

                        Case Else
                            'actualizacion aux comisiones
                            ModificarRegistroTrn() 'aqui se actualizan los totales

                            If blPermiteAcreditacionParcial And ultimaTrnCliente() Then

                                If commitParcial Then

                                    commitParcial = False
                                    iniciarConexion = True
                                    blIgnorar = Not RegistroContable()

                                    If blIgnorar Then
                                        IgnorarRegistroTrn() 'Rollback
                                    Else
                                        gdbc.Commit()
                                        determinaEstadoError()

                                    End If

                                    GenerarLog("I", 0, "Hebra " & IdHebra & " - Se han procesado " & (II + 1).ToString & " de " & dsTrnCur.Tables(0).Rows.Count & " transacciones", IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                                    'forzarGrarbageCollector()

                                End If
                            End If

                            Select Case gtipoProceso
                                Case "SI"
                                    gTotRegistrosSimulados = gTotRegistrosSimulados + 1
                                Case "AC"
                                    gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
                            End Select


                    End Select

                    '--.--lfc:09/04/09 llamada va en el cierre diario
                    'debe ir comentado desde SIS--------------------DON
                    ''''''If gtipoProceso = "AC" And Not blIgnorar Then

                    ''''''    Sys.Cobranzas.Pago.acreditarEnCobranza(gdbc,gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId, rTrn.seqRegistro, gidUsuarioProceso, gfuncion)

                    ''''''End If

                Next II


                'AcredRecaudac = TotalesControlAcreditacion() ' MARCA LA TRANSACCION COMO ACREDITADA


                'If gTotRegistrosIgnorados > 0 Then
                '    GenerarLog("A", 0, "Hebra " & IdHebra & " - Existen registros ignorados", 9, Nothing, Nothing)
                'Else
                '    If gtipoProceso = "AC" Then
                'Lotes.modEstado(gdbc,gidAdm, rTrn.numReferenciaOrigen5, Nothing, "T", gidUsuarioProceso, gfuncion)
                '    End If
                'End If


            End If

        Catch e As SondaException
            Dim mensaje As String
            If Not IsNothing(gdbc) Then gdbc.Rollback()
            AcredRecaudac = 15310
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - " & e.codigo & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim mensaje As String
            If Not IsNothing(gdbc) Then gdbc.Rollback()
            AcredRecaudac = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()
        End Try

    End Function

    Public Function AcredExterna(ByRef dbc As OraConn, _
                                  ByVal idAdm As Integer, _
                                  ByVal codOrigenProceso As String, _
                                  ByVal idUsuarioProceso As String, _
                                  ByVal numeroId As Long, _
                                  ByVal tipoProceso As String, _
                                  ByVal IdHebra As Integer, _
                                  ByVal LOG As Procesos.logEtapa, _
                                  ByVal FecAcreditacion As Date, _
                                  ByVal FecValorCuota As Date, _
                                  ByVal PerCuatrimestre As Date, _
                                  ByVal PerContable As Date, _
                                  ByVal PerContableSis As Date, _
                                  ByVal SeqProceso As Decimal, _
                                  ByVal ValMlCuotaDestinoA As Decimal, _
                                  ByVal ValMlCuotaDestinoB As Decimal, _
                                  ByVal ValMlCuotaDestinoC As Decimal, _
                                  ByVal ValMlCuotaDestinoD As Decimal, _
                                  ByVal ValMlCuotaDestinoE As Decimal, _
                                  ByVal PermiteAcreditacionParcial As Boolean) As Long


        Dim iniciarConexion As Boolean = True
        Dim dsRtrn As New DataSet()
        Dim error_ape As Integer
        Dim seq_log As Integer

        Try

            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gseqProceso = 0
            gtipoProceso = tipoProceso
            gfuncion = "AcredRecaudac"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()

            'EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            ''MSC: Agregar este bloque de codigo en este mismo punto
            'If tipoProceso <> "AC" Then
            '    evento(LOG, "Hebra " & IdHebra & " - Se inicia proceso apertura de transacciones")

            '    ' apertura transacciones
            '    dsRtrn = Transacciones.aperturaTransaccion(gdbc,gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId, gtipoProceso)
            '    error_ape = dsRtrn.Tables(0).Rows(0).Item("verror_ape")
            '    seq_log = dsRtrn.Tables(0).Rows(0).Item("vseq_log")

            '    If error_ape <> 0 Then
            '        blErrorFatal = True
            '        GenerarLog("E", 99999, Nothing, IdHebra, 0, Nothing, Nothing)
            '        evento(LOG, "Hebra " & IdHebra & " - Error fatal en apertura de transacciones")
            '        Throw New SondaException(99999) 'No existen registros para acreditar
            '    End If

            '    evento(LOG, "Hebra " & IdHebra & " - Proceso apertura de transacciones finalizado")

            '    gSeqLog = seq_log  ' se asigna variable obtenida de la llamada del procedimiento

            'End If
            ''FIN MSC 

            evento(LOG, "Hebra " & IdHebra & " - Se inicia el proceso de acreditacion")

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc, idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)

            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)
            gdbc.Rollback()


            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'evento(LOG, "Hebra " & IdHebra & " - No se encontraron registros para acreditar")
                'Throw New SondaException(15307) 'No existen registros para acreditar

            Else
                'dbc.BeginTrans()
                'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
                'ValoresAcreditacion()
                gfecAcreditacion = FecAcreditacion
                gfecValorCuota = FecValorCuota
                gperCuatrimestre = PerCuatrimestre
                gperContable = PerContable
                gPerContableSis = PerContableSis
                gseqProceso = SeqProceso
                gvalMlCuotaDestinoA = ValMlCuotaDestinoA
                gvalMlCuotaDestinoB = ValMlCuotaDestinoB
                gvalMlCuotaDestinoC = ValMlCuotaDestinoC
                gvalMlCuotaDestinoD = ValMlCuotaDestinoD
                gvalMlCuotaDestinoE = ValMlCuotaDestinoE
                blPermiteAcreditacionParcial = PermiteAcreditacionParcial

                'dbc.Commit()

                If Not blPermiteAcreditacionParcial Then
                    gdbc.BeginTrans()
                End If

                For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1

                    commitParcial = commitParcial Or (II + 1) Mod 1000 = 0

                    rTrn = Nothing
                    rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                    If rTrn.seqRegistro = 1074391247 Then
                        rTrn.seqRegistro = 1074391247
                    End If

                    If rTrn.seqRegistro = 1074391308 Then
                        rTrn.seqRegistro = 1074391308
                    End If


                    If blPermiteAcreditacionParcial And iniciarConexion Then 'rTrn.idCliente <> gIdClienteAnterior Then
                        gdbc.BeginTrans()
                        iniciarConexion = False
                    End If

                    If rTrn.tipoEntidadPagadora = "V" And (rTrn.tipoPago = 2 Or rTrn.tipoPago = 3) And _
                       rTrn.tipoProducto = "CCO" And rTrn.valMlExcesoLinea > 0 Then 'Afil. Independiente Voluntario(V) y Atrasado(2) y Solo Obligatoria.
                        blExcesosIndep = True
                    Else
                        blExcesosIndep = False
                    End If

                    LimpiarDatos()
                    LlenaValoresIniciales()

                    DeterminaMontoInstitucionSalud()

                    g_valCuoAjusteDec = 0
                    g_valMlAjusteDec = 0

                    If rTrn.codDestinoTransaccion <> "REZ" Then

                        DeterminarTransferencia()

                        If rTrn.valMlTransferenciaCal > 0 And rTrn.codDestinoTransaccion <> "TRF" Then
                            AcreditaTransferencia() 'viene cotizacion y transferencia
                        End If

                    End If

                    If rTrn.codDestinoTransaccionCal = "REZ" Then
                        blAcreditarARezago = True
                    End If

                    ValidarDatosBasicos()

                    If rTrn.codDestinoTransaccion <> "REZ" Then

                        '-->>>lfc:19/05 --se crean cuentas CCV saldo=0 y regTrib null
                        If Not blAcreditarARezago And Not blIgnorar Then

                            gcausalRezago = LeerDatosCliente()

                            If Not blAcreditarARezago And Not blIgnorar Then
                                ValidarParaAcreditacion()
                            End If
                        End If
                    End If

                    ''CAVCAI
                    If rTrn.tipoProducto = "CAV" And rTrn.codDestinoTransaccionCal = "CTA" And IsNothing(Trim(rTrn.categoria)) Then
                        'Sin reg.Tributario, es decir sin Categoria. OS-4427354
                        rTrn.codCausalRezagoCal = "14"
                        blAcreditarARezago = True
                    End If

                    Select Case True
                        Case blIgnorar

                        Case rTrn.codDestinoTransaccion = "TRF"
                            AcreditaTransferencia() 'vien solo transferencia sin cotizacion

                        Case blAcreditarARezago
                            descripcionCausalRez()
                            AcreditaRezago()

                        Case rTrn.codDestinoTransaccion = "REZ"
                            blAcreditarARezago = True
                            AcreditaRezago()

                        Case rMovAcr.tipoMvto = "DEC"
                            AcreditaDeclaraciones()

                        Case blExcesosIndep
                            AcreditaExcesosIndepend()

                        Case rTrn.codOrigenProceso = "TRAINCHP" And rMovAcr.tipoMvto = "TIC"
                            AcreditaCotREC3()


                        Case rTrn.codOrigenProceso = "ACREXAFC"
                            AcreditaCotREC3()

                        Case rMovAcr.indCotizacion = "S"

                            AcreditaCotREC3()

                        Case Else
                            blIgnorar = True
                            rTrn.codError = 15308 'Tipo movimiento no fue reconocido
                            GenerarLog("A", 15308, "Hebra " & IdHebra & " - Tipo movimiento: " & rMovAcr.tipoMvto, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                    End Select

                    ValidaFondoDestinoInicial()

                    Select Case True
                        Case blIgnorar
                            IgnorarRegistroTrn()

                        Case Else
                            'actualizacion aux comisiones
                            ModificarRegistroTrn() 'aqui se actualizan los totales

                            If blPermiteAcreditacionParcial And ultimaTrnCliente() Then

                                If commitParcial Then

                                    commitParcial = False
                                    iniciarConexion = True
                                    blIgnorar = Not RegistroContable()

                                    If blIgnorar Then
                                        IgnorarRegistroTrn() 'Rollback
                                    Else
                                        gdbc.Commit()
                                        determinaEstadoError()

                                    End If

                                    GenerarLog("I", 0, "Hebra " & IdHebra & " - Se han procesado " & (II + 1).ToString & " de " & dsTrnCur.Tables(0).Rows.Count & " transacciones", IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                                End If
                            End If

                            Select Case gtipoProceso
                                Case "SI"
                                    gTotRegistrosSimulados = gTotRegistrosSimulados + 1
                                Case "AC"
                                    gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
                            End Select


                    End Select

                Next II

            End If

        Catch e As SondaException
            Dim mensaje As String
            If Not IsNothing(gdbc) Then gdbc.Rollback()
            AcredExterna = e.codigo
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim mensaje As String
            If Not IsNothing(gdbc) Then gdbc.Rollback()
            AcredExterna = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()
        End Try

    End Function

    Public Function AcredDnpCobranzas(ByRef dbc As OraConn, _
                                      ByVal idAdm As Integer, _
                                      ByVal codOrigenProceso As String, _
                                      ByVal idUsuarioProceso As String, _
                                      ByVal numeroId As Long, _
                                      ByVal tipoProceso As String, _
                                      ByVal IdHebra As Integer, _
                                      ByVal LOG As Procesos.logEtapa, _
                                      ByVal FecAcreditacion As Date, _
                                      ByVal FecValorCuota As Date, _
                                      ByVal PerCuatrimestre As Date, _
                                      ByVal PerContable As Date, _
                                      ByVal PerContableSis As Date, _
                                      ByVal SeqProceso As Decimal, _
                                      ByVal ValMlCuotaDestinoA As Decimal, _
                                      ByVal ValMlCuotaDestinoB As Decimal, _
                                      ByVal ValMlCuotaDestinoC As Decimal, _
                                      ByVal ValMlCuotaDestinoD As Decimal, _
                                      ByVal ValMlCuotaDestinoE As Decimal, _
                                      ByVal PermiteAcreditacionParcial As Boolean) As Long
        Try

            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gseqProceso = 0
            gtipoProceso = tipoProceso
            gfuncion = "AcredDnpAjuste"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()

            evento(LOG, "Hebra " & IdHebra & " - Se inicia el proceso de acreditacion")

            EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc,idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)
            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)
            gdbc.Rollback()


            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'evento(LOG, "Hebra " & IdHebra & " - No se encontraron registros para acreditar")
                'Throw New SondaException(15307) 'No existen registros para acreditar

            Else
                gdbc.BeginTrans()
                'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
                'ValoresAcreditacion()
                gfecAcreditacion = FecAcreditacion
                gfecValorCuota = FecValorCuota
                gperCuatrimestre = PerCuatrimestre
                gperContable = PerContable
                gPerContableSis = PerContableSis
                gseqProceso = SeqProceso
                gvalMlCuotaDestinoA = ValMlCuotaDestinoA
                gvalMlCuotaDestinoB = ValMlCuotaDestinoB
                gvalMlCuotaDestinoC = ValMlCuotaDestinoC
                gvalMlCuotaDestinoD = ValMlCuotaDestinoD
                gvalMlCuotaDestinoE = ValMlCuotaDestinoE
                blPermiteAcreditacionParcial = PermiteAcreditacionParcial

                gdbc.Commit()

                If Not blPermiteAcreditacionParcial Then
                    gdbc.BeginTrans()
                End If

                For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1

                    rTrn = Nothing
                    rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                    If blPermiteAcreditacionParcial And rTrn.idCliente <> gIdClienteAnterior Then
                        gdbc.BeginTrans()
                    End If

                    LimpiarDatos()
                    LlenaValoresIniciales()
                    ValorCuotaCaja()

                    CalcularPatrimonioFechaOperacion()

                    '--.-- CN2 If (rTrn.tipoProducto = "CCV" Or rTrn.tipoProducto = "CDC") And rTrn.codDestinoTransaccion <> "REZ" Then
                    If rTrn.codDestinoTransaccion <> "REZ" Then
                        DeterminarTransferencia()

                        If rTrn.valMlTransferenciaCal > 0 And rTrn.codDestinoTransaccion <> "TRF" Then
                            AcreditaTransferencia() 'viene cotizacion y transferencia
                        End If

                    End If

                    ValidarDatosBasicos()

                    If rTrn.codDestinoTransaccion <> "REZ" Then


                        If Not blAcreditarARezago And Not blIgnorar Then

                            gcausalRezago = LeerDatosCliente()

                            If Not blAcreditarARezago And Not blIgnorar Then
                                ValidarParaAcreditacion()
                            End If

                        End If
                    End If

                    Select Case True

                        Case blIgnorar


                        Case blAcreditarARezago
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("E", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rTrn.codDestinoTransaccion = "REZ"
                            blAcreditarARezago = True
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("E", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rMovAcr.tipoMvto = "DEC"
                            AcreditaDeclaraciones()


                        Case Else
                            blIgnorar = True
                            rTrn.codError = 15308 'Tipo movimiento no fue reconocido
                            GenerarLog("A", 15308, "Hebra " & IdHebra & " - Tipo movimiento: " & rMovAcr.tipoMvto, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                    End Select

                    Select Case True
                        Case blIgnorar
                            IgnorarRegistroTrn()

                        Case Else
                            ModificarRegistroTrn() 'aqui se actualizan los totales

                            If blPermiteAcreditacionParcial And ultimaTrnCliente() Then

                                blIgnorar = Not RegistroContable()

                                If blIgnorar Then
                                    IgnorarRegistroTrn() 'Rollback
                                Else
                                    gdbc.Commit()
                                    determinaEstadoError()
                                End If

                            End If

                            Select Case gtipoProceso
                                Case "SI"
                                    gTotRegistrosSimulados = gTotRegistrosSimulados + 1
                                Case "AC"
                                    gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
                            End Select


                    End Select


                    If (II + 1) Mod 1000 = 0 Then
                        GenerarLog("I", 0, "Hebra " & IdHebra & " - Se han procesado " & (II + 1).ToString & " de " & dsTrnCur.Tables(0).Rows.Count & " transacciones", IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                    End If

                Next II

                'AcredDnpCobranzas = TotalesControlAcreditacion() ' MARCA LA TRANSACCION COMO ACREDITADA


                'If gTotRegistrosIgnorados > 0 Then
                'GenerarLog("A", 0, "Hebra " & IdHebra & " - Existen registros ignorados", 9, Nothing, Nothing)
                'End If


            End If

        Catch e As SondaException
            Dim mensaje As String
            If Not IsNothing(gdbc) Then gdbc.Rollback()
            AcredDnpCobranzas = e.codigo
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim mensaje As String
            If Not IsNothing(gdbc) Then gdbc.Rollback()
            AcredDnpCobranzas = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()
        End Try

    End Function

    Public Function AcredBono(ByRef dbc As OraConn, _
                              ByVal idAdm As Integer, _
                              ByVal codOrigenProceso As String, _
                              ByVal idUsuarioProceso As String, _
                              ByVal numeroId As Long, _
                              ByVal tipoProceso As String, _
                              ByVal IdHebra As Integer, _
                              ByVal LOG As Procesos.logEtapa, _
                              ByVal FecAcreditacion As Date, _
                              ByVal FecValorCuota As Date, _
                              ByVal PerCuatrimestre As Date, _
                              ByVal PerContable As Date, _
                              ByVal PerContableSis As Date, _
                              ByVal SeqProceso As Decimal, _
                              ByVal ValMlCuotaDestinoA As Decimal, _
                              ByVal ValMlCuotaDestinoB As Decimal, _
                              ByVal ValMlCuotaDestinoC As Decimal, _
                              ByVal ValMlCuotaDestinoD As Decimal, _
                              ByVal ValMlCuotaDestinoE As Decimal, _
                              ByVal PermiteAcreditacionParcial As Boolean) As Long

        Try

            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gtipoProceso = tipoProceso
            gfuncion = "AcredBono"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()

            EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc,idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)
            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)
            gdbc.Rollback()

            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'Throw New SondaException(15307) '"No existen registros para acreditar
            Else
                gdbc.BeginTrans()
                'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
                'ValoresAcreditacion()
                gfecAcreditacion = FecAcreditacion
                gfecValorCuota = FecValorCuota
                gperCuatrimestre = PerCuatrimestre
                gperContable = PerContable
                gPerContableSis = PerContableSis
                gseqProceso = SeqProceso
                gvalMlCuotaDestinoA = ValMlCuotaDestinoA
                gvalMlCuotaDestinoB = ValMlCuotaDestinoB
                gvalMlCuotaDestinoC = ValMlCuotaDestinoC
                gvalMlCuotaDestinoD = ValMlCuotaDestinoD
                gvalMlCuotaDestinoE = ValMlCuotaDestinoE
                blPermiteAcreditacionParcial = PermiteAcreditacionParcial

                gdbc.Commit()

                If Not blPermiteAcreditacionParcial Then
                    gdbc.BeginTrans()
                End If

                For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1

                    rTrn = Nothing
                    rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                    If blPermiteAcreditacionParcial And rTrn.idCliente <> gIdClienteAnterior Then
                        gdbc.BeginTrans()

                    End If

                    LimpiarDatos()
                    LlenaValoresIniciales()

                    'Se mueve fecha acreditacion a fecha operacion para que no rezague por causal 1D
                    rTrn.fecOperacion = gfecAcreditacion

                    ValidarDatosBasicos()

                    If Not blAcreditarARezago And Not blIgnorar Then
                        If Not blAcreditarARezago And Not blIgnorar Then

                            gcausalRezago = LeerDatosCliente()

                            If Not blAcreditarARezago And Not blIgnorar Then
                                ValidarParaAcreditacion()
                            End If

                        End If
                    End If

                    Select Case True

                        Case blIgnorar

                        Case blAcreditarARezago
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rTrn.codDestinoTransaccion = "REZ"
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rMovAcr.tipoMvto = "BON"
                            AcreditaBono()

                        Case Else
                            blIgnorar = True
                            rTrn.codError = 15308 'Tipo movimiento no fue reconocido
                            GenerarLog("A", 15308, "Hebra " & IdHebra & " - Tipo movimiento: " & rMovAcr.tipoMvto, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                    End Select

                    Select Case True

                        Case blIgnorar
                            IgnorarRegistroTrn()
                            'modificar bono con error

                        Case Else
                            'modificar bono con exito
                            ModificarRegistroTrn() 'aqui se actualizan los totales

                            If blPermiteAcreditacionParcial And ultimaTrnCliente() Then

                                blIgnorar = Not RegistroContable()

                                If blIgnorar Then
                                    IgnorarRegistroTrn() 'Rollback
                                Else
                                    gdbc.Commit()
                                    determinaEstadoError()
                                End If

                            End If

                            Select Case gtipoProceso
                                Case "SI"
                                    gTotRegistrosSimulados = gTotRegistrosSimulados + 1
                                Case "AC"
                                    gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
                            End Select


                    End Select

                Next II


                'AcredBono = TotalesControlAcreditacion()

                'If gTotRegistrosIgnorados > 0 Then

                '    GenerarLog("A", 0, "Hebra " & IdHebra & " - Existen registros ignorados", 9, Nothing, Nothing)
                'End If

            End If

        Catch e As SondaException
            Dim mensaje As String
            gdbc.Rollback()
            AcredBono = e.codigo
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim mensaje As String
            gdbc.Rollback()
            AcredBono = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()

        End Try

    End Function

    Public Function AcredRetiro(ByRef dbc As OraConn, _
                                ByVal idAdm As Integer, _
                                ByVal codOrigenProceso As String, _
                                ByVal idUsuarioProceso As String, _
                                ByVal numeroId As Long, _
                                ByVal tipoProceso As String, _
                                ByVal IdHebra As Integer, _
                                ByVal LOG As Procesos.logEtapa, _
                                ByVal FecAcreditacion As Date, _
                                ByVal FecValorCuota As Date, _
                                ByVal PerCuatrimestre As Date, _
                                ByVal PerContable As Date, _
                                ByVal PerContableSis As Date, _
                                ByVal SeqProceso As Decimal, _
                                ByVal ValMlCuotaDestinoA As Decimal, _
                                ByVal ValMlCuotaDestinoB As Decimal, _
                                ByVal ValMlCuotaDestinoC As Decimal, _
                                ByVal ValMlCuotaDestinoD As Decimal, _
                                ByVal ValMlCuotaDestinoE As Decimal, _
                                ByVal PermiteAcreditacionParcial As Boolean) As Long

        Try
            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gtipoProceso = tipoProceso
            gfuncion = "AcredRetiro"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()

            'EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc,idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)
            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)
            gdbc.Rollback()

            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'Throw New SondaException(15307) '"No existen registros para acreditar
            Else
                gdbc.BeginTrans()
                'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
                'ValoresAcreditacion()
                gfecAcreditacion = FecAcreditacion
                gfecValorCuota = FecValorCuota
                gperCuatrimestre = PerCuatrimestre
                gperContable = PerContable
                gPerContableSis = PerContableSis
                gseqProceso = SeqProceso
                gvalMlCuotaDestinoA = ValMlCuotaDestinoA
                gvalMlCuotaDestinoB = ValMlCuotaDestinoB
                gvalMlCuotaDestinoC = ValMlCuotaDestinoC
                gvalMlCuotaDestinoD = ValMlCuotaDestinoD
                gvalMlCuotaDestinoE = ValMlCuotaDestinoE
                blPermiteAcreditacionParcial = PermiteAcreditacionParcial

                'If Not gExisteEncabezado Then
                '    CrearEncabezadoAcred()
                'End If
                gdbc.Commit()

                If Not blPermiteAcreditacionParcial Then
                    gdbc.BeginTrans()
                End If

                For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1

                    rTrn = Nothing
                    rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                    If blPermiteAcreditacionParcial And rTrn.idCliente <> gIdClienteAnterior Then
                        gdbc.BeginTrans()

                    End If

                    LimpiarDatos()
                    LlenaValoresIniciales()
                    ValidarDatosBasicos()

                    If Not blAcreditarARezago And Not blIgnorar Then

                        gcausalRezago = LeerDatosCliente()

                        If Not blAcreditarARezago And Not blIgnorar Then
                            ValidarParaAcreditacion()
                        End If

                    End If

                    Select Case True

                        Case blIgnorar

                        Case blAcreditarARezago
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rTrn.codDestinoTransaccion = "REZ"
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga 
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rMovAcr.tipoMvto = "RET"
                            AcreditaRetiro()

                        Case rMovAcr.tipoMvto = "IMP"
                            AcreditaImpuesto()

                        Case rMovAcr.tipoMvto = "RRET"
                            AcreditaRetiro()

                        Case rMovAcr.tipoMvto = "COT"
                            'AcreditaCotMandato()

                            'OS:6732254 - 23/08/2016 - CONTROL SIS PARA COTIZACIONES DEL ORIGEN
                            If rTrn.codOrigenProceso = "RETPAGPE" And rTrn.perCotizacion >= New Date(2009, 7, 1).Date And (rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF") Then
                                Me.AcreditaCotAjusteSIS()
                            Else
                                AcreditaCotAjuste()
                            End If

                        Case rMovAcr.tipoMvto = "DEV"
                            AcreditaRetiro()

                        Case rMovAcr.tipoMvto = "AJU"
                            AcreditaAjuste()

                        Case rMovAcr.tipoMvto = "COM"
                            'Solicitado por Manuel Avalos Para soporte de Comision Adm. Saldo.
                            AcreditaComision()

                        Case Else
                            blIgnorar = True
                            rTrn.codError = 15308 'Tipo movimiento no fue reconocido
                            GenerarLog("A", 15308, "Hebra " & IdHebra & " - Tipo movimiento: " & rMovAcr.tipoMvto, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                    End Select




                    Select Case True

                        Case blIgnorar
                            IgnorarRegistroTrn()
                            'modificar retiro con error

                        Case Else
                            'modificar retiro con exito si y solo si contabiliza

                            ModificarRegistroTrn() 'aqui se actualizan los totales

                            If blPermiteAcreditacionParcial And ultimaTrnCliente() Then

                                blIgnorar = Not RegistroContable()

                                If blIgnorar Then
                                    IgnorarRegistroTrn() 'Rollback
                                Else
                                    gdbc.Commit()
                                    determinaEstadoError()
                                End If

                            End If

                            Select Case gtipoProceso
                                Case "SI"
                                    gTotRegistrosSimulados = gTotRegistrosSimulados + 1
                                Case "AC"
                                    gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
                            End Select


                    End Select

                Next II


                'lfc: OS-10304432- Mejoras lote de retiros
				''solo en la simulacion
				'If gtipoProceso = "SI" And gcodAdministradora = 1033 Then
				'    If gcodOrigenProceso = "RETCAVAD" Or gcodOrigenProceso = "RETCAIFO" Or gcodOrigenProceso = "RETCCVAD" Then
				'        Transacciones.excluirPorSolicitudRet(dbc, gidAdm, gnumeroId, gcodOrigenProceso, idUsuarioProceso, "excTrsXSol")
				'    End If
				'End If



                'AcredRetiro = TotalesControlAcreditacion()

                'If gTotRegistrosIgnorados > 0 Then

                '    GenerarLog("A", 0, "Hebra " & IdHebra & " - Existen registros ignorados", 9, Nothing, Nothing)
                'End If

                End If

        Catch e As SondaException
            Dim mensaje As String
            gdbc.Rollback()
            AcredRetiro = e.codigo
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim mensaje As String
            gdbc.Rollback()
            AcredRetiro = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()

        End Try

    End Function


    Public Function AcredDevExcesos(ByRef dbc As OraConn, _
                                    ByVal idAdm As Integer, _
                                    ByVal codOrigenProceso As String, _
                                    ByVal idUsuarioProceso As String, _
                                    ByVal numeroId As Long, _
                                    ByVal tipoProceso As String, _
                                    ByVal IdHebra As Integer, _
                                    ByVal LOG As Procesos.logEtapa, _
                                    ByVal FecAcreditacion As Date, _
                                    ByVal FecValorCuota As Date, _
                                    ByVal PerCuatrimestre As Date, _
                                    ByVal PerContable As Date, _
                                    ByVal PerContableSis As Date, _
                                    ByVal SeqProceso As Decimal, _
                                    ByVal ValMlCuotaDestinoA As Decimal, _
                                    ByVal ValMlCuotaDestinoB As Decimal, _
                                    ByVal ValMlCuotaDestinoC As Decimal, _
                                    ByVal ValMlCuotaDestinoD As Decimal, _
                                    ByVal ValMlCuotaDestinoE As Decimal, _
                                    ByVal PermiteAcreditacionParcial As Boolean) As Long

        Try
            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gtipoProceso = tipoProceso
            gfuncion = "AcredDevExcesos"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()

            EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc,idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)
            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)
            gdbc.Rollback()

            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'Throw New SondaException(15307) '"No existen registros para acreditar
            Else
                gdbc.BeginTrans()
                'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
                'ValoresAcreditacion()
                gfecAcreditacion = FecAcreditacion
                gfecValorCuota = FecValorCuota
                gperCuatrimestre = PerCuatrimestre
                gperContable = PerContable
                gPerContableSis = PerContableSis
                gseqProceso = SeqProceso
                gvalMlCuotaDestinoA = ValMlCuotaDestinoA
                gvalMlCuotaDestinoB = ValMlCuotaDestinoB
                gvalMlCuotaDestinoC = ValMlCuotaDestinoC
                gvalMlCuotaDestinoD = ValMlCuotaDestinoD
                gvalMlCuotaDestinoE = ValMlCuotaDestinoE
                blPermiteAcreditacionParcial = PermiteAcreditacionParcial

                'If Not gExisteEncabezado Then
                '    CrearEncabezadoAcred()
                'End If

                gdbc.Commit()

                If Not blPermiteAcreditacionParcial Then
                    gdbc.BeginTrans()
                End If

                For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1

                    rTrn = Nothing
                    rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                    '''''actualizar datos antes de error en la transaccion......
                    ''''If gtipoProceso <> "AC" Then
                    ''''    rTrn.fecAcreditacion = gfecAcreditacion
                    ''''    rTrn.fecValorCuota = gfecValorCuota
                    ''''    rTrn.perContable = gperContable

                    ''''    rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()

                    ''''    Select Case rTrn.tipoFondoDestino
                    ''''        Case "A" : rTrn.valMlValorCuota = gvalMlCuotaDestinoA
                    ''''        Case "B" : rTrn.valMlValorCuota = gvalMlCuotaDestinoB
                    ''''        Case "C" : rTrn.valMlValorCuota = gvalMlCuotaDestinoC
                    ''''        Case "D" : rTrn.valMlValorCuota = gvalMlCuotaDestinoD
                    ''''        Case "E" : rTrn.valMlValorCuota = gvalMlCuotaDestinoE
                    ''''    End Select
                    ''''    rTrn.numReferenciaOrigen6 = rTrn.valCuoMvto * 100 'guardar el valor informado
                    ''''    rTrn.valCuoMvto = Mat.Redondear(rTrn.valMlMvto / rTrn.valMlValorCuota, 2)
                    ''''End If


                    If blPermiteAcreditacionParcial And rTrn.idCliente <> gIdClienteAnterior Then
                        gdbc.BeginTrans()

                    End If

                    LimpiarDatos()
                    LlenaValoresIniciales()
                    ValidarDatosBasicos()

                    If Not blAcreditarARezago And Not blIgnorar Then

                        gcausalRezago = LeerDatosCliente()

                        If Not blAcreditarARezago And Not blIgnorar Then
                            ValidarParaAcreditacion()
                        End If

                    End If

                    Select Case True

                        Case blIgnorar

                        Case blAcreditarARezago
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rTrn.codDestinoTransaccion = "REZ"
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rMovAcr.tipoMvto = "DEV"
                            AcreditaDevExcesos()

                        Case rMovAcr.tipoMvto = "RCOM"
                            AcreditaRevComision()

                        Case rMovAcr.tipoMvto = "RPRI"
                            AcreditaRevPrima()
                        Case Else
                            blIgnorar = True
                            rTrn.codError = 15308 'Tipo movimiento no fue reconocido
                            GenerarLog("A", 15308, "Hebra " & IdHebra & " - Tipo movimiento: " & rMovAcr.tipoMvto, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                    End Select

                    Select Case True

                        Case blIgnorar

                            IgnorarRegistroTrn()
                            'modificar solicitud con error

                        Case Else
                            ModificarRegistroTrn() 'aqui se actualizan los totales

                            If blPermiteAcreditacionParcial And ultimaTrnCliente() Then

                                blIgnorar = Not RegistroContable()

                                If blIgnorar Then
                                    IgnorarRegistroTrn() 'Rollback
                                Else
                                    gdbc.Commit()
                                    determinaEstadoError()
                                End If

                            End If

                            Select Case gtipoProceso
                                Case "SI"
                                    gTotRegistrosSimulados = gTotRegistrosSimulados + 1
                                Case "AC"
                                    gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
                            End Select


                    End Select

                Next II


                'AcredDevExcesos = TotalesControlAcreditacion()

                'If gTotRegistrosIgnorados > 0 Then

                '    GenerarLog("A", 0, "Hebra " & IdHebra & " - Existen registros ignorados", 9, Nothing, Nothing)
                'End If

            End If


        Catch e As SondaException
            Dim mensaje As String
            gdbc.Rollback()
            AcredDevExcesos = e.codigo
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim mensaje As String
            gdbc.Rollback()
            AcredDevExcesos = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()

        End Try

    End Function

    Public Function AcredReintegroDevExcesos(ByRef dbc As OraConn, _
                                       ByVal idAdm As Integer, _
                                       ByVal codOrigenProceso As String, _
                                       ByVal idUsuarioProceso As String, _
                                       ByVal numeroId As Long, _
                                       ByVal tipoProceso As String, _
                                       ByVal IdHebra As Integer, _
                                       ByVal LOG As Procesos.logEtapa, _
                                       ByVal FecAcreditacion As Date, _
                                       ByVal FecValorCuota As Date, _
                                       ByVal PerCuatrimestre As Date, _
                                       ByVal PerContable As Date, _
                                       ByVal PerContableSis As Date, _
                                       ByVal SeqProceso As Decimal, _
                                       ByVal ValMlCuotaDestinoA As Decimal, _
                                       ByVal ValMlCuotaDestinoB As Decimal, _
                                       ByVal ValMlCuotaDestinoC As Decimal, _
                                       ByVal ValMlCuotaDestinoD As Decimal, _
                                       ByVal ValMlCuotaDestinoE As Decimal, _
                                       ByVal PermiteAcreditacionParcial As Boolean) As Long

        Try
            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gtipoProceso = tipoProceso
            gfuncion = "AcredDevExcesos"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()

            EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc,idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)
            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)
            gdbc.Rollback()

            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'Throw New SondaException(15307) '"No existen registros para acreditar
            Else
                gdbc.BeginTrans()
                'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
                'ValoresAcreditacion()
                gfecAcreditacion = FecAcreditacion
                gfecValorCuota = FecValorCuota
                gperCuatrimestre = PerCuatrimestre
                gperContable = PerContable
                gPerContableSis = PerContableSis
                gseqProceso = SeqProceso
                gvalMlCuotaDestinoA = ValMlCuotaDestinoA
                gvalMlCuotaDestinoB = ValMlCuotaDestinoB
                gvalMlCuotaDestinoC = ValMlCuotaDestinoC
                gvalMlCuotaDestinoD = ValMlCuotaDestinoD
                gvalMlCuotaDestinoE = ValMlCuotaDestinoE
                blPermiteAcreditacionParcial = PermiteAcreditacionParcial

                'If Not gExisteEncabezado Then
                '    CrearEncabezadoAcred()
                'End If
                gdbc.Commit()

                If Not blPermiteAcreditacionParcial Then
                    gdbc.BeginTrans()
                End If
                For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1

                    rTrn = Nothing
                    rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                    If blPermiteAcreditacionParcial And rTrn.idCliente <> gIdClienteAnterior Then
                        gdbc.BeginTrans()

                    End If

                    LimpiarDatos()
                    LlenaValoresIniciales()

                    ValidarDatosBasicos()
                    If Not blAcreditarARezago And Not blIgnorar Then

                        gcausalRezago = LeerDatosCliente()

                        If Not blAcreditarARezago And Not blIgnorar Then
                            ValidarParaAcreditacion()
                        End If

                    End If

                    Select Case True

                        Case blIgnorar

                        Case blAcreditarARezago
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rTrn.codDestinoTransaccion = "REZ"
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)


                        Case rMovAcr.tipoMvto = "EXC"
                            AcreditaDevExcesos()

                        Case Else
                            blIgnorar = True
                            rTrn.codError = 15308 'Tipo movimiento no fue reconocido
                            GenerarLog("A", 15308, "Hebra " & IdHebra & " - Tipo movimiento: " & rMovAcr.tipoMvto, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                    End Select

                    Select Case True

                        Case blIgnorar

                            IgnorarRegistroTrn()
                            'modificar solicitud con error

                        Case Else
                            ModificarRegistroTrn() 'aqui se actualizan los totales

                            If blPermiteAcreditacionParcial And ultimaTrnCliente() Then

                                blIgnorar = Not RegistroContable()

                                If blIgnorar Then
                                    IgnorarRegistroTrn() 'Rollback
                                Else
                                    gdbc.Commit()
                                    determinaEstadoError()
                                End If

                            End If

                            Select Case gtipoProceso
                                Case "SI"
                                    gTotRegistrosSimulados = gTotRegistrosSimulados + 1
                                Case "AC"
                                    gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
                            End Select


                    End Select

                Next II

                'ReintegroCuentas.actualizarCaducidad(gdbc,gidAdm, gcodOrigenProceso, gnumeroId, gidUsuarioProceso, gfuncion)


                'AcredReintegroDevExcesos = TotalesControlAcreditacion()

                'If gTotRegistrosIgnorados > 0 Then

                '    GenerarLog("A", 0, "Hebra " & IdHebra & " - Existen registros ignorados", 9, Nothing, Nothing)
                'End If

            End If


        Catch e As SondaException
            Dim mensaje As String
            gdbc.Rollback()
            AcredReintegroDevExcesos = e.codigo
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim mensaje As String
            gdbc.Rollback()
            AcredReintegroDevExcesos = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()

        End Try

    End Function
    Public Function AcredReintegroCheque(ByRef dbc As OraConn, _
                                         ByVal idAdm As Integer, _
                                          ByVal codOrigenProceso As String, _
                                          ByVal idUsuarioProceso As String, _
                                          ByVal numeroId As Long, _
                                          ByVal tipoProceso As String, _
                                          ByVal IdHebra As Integer, _
                                          ByVal LOG As Procesos.logEtapa, _
                                          ByVal FecAcreditacion As Date, _
                                          ByVal FecValorCuota As Date, _
                                          ByVal PerCuatrimestre As Date, _
                                          ByVal PerContable As Date, _
                                          ByVal PerContableSis As Date, _
                                          ByVal SeqProceso As Decimal, _
                                          ByVal ValMlCuotaDestinoA As Decimal, _
                                          ByVal ValMlCuotaDestinoB As Decimal, _
                                          ByVal ValMlCuotaDestinoC As Decimal, _
                                          ByVal ValMlCuotaDestinoD As Decimal, _
                                          ByVal ValMlCuotaDestinoE As Decimal, _
                                          ByVal PermiteAcreditacionParcial As Boolean) As Long

        Try
            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gtipoProceso = tipoProceso
            gfuncion = "AcredReinCheque"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()

            EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc,idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)
            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)
            gdbc.Rollback()

            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'Throw New SondaException(15307) '"No existen registros para acreditar
            Else
                gdbc.BeginTrans()
                'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
                'ValoresAcreditacion()
                gfecAcreditacion = FecAcreditacion
                gfecValorCuota = FecValorCuota
                gperCuatrimestre = PerCuatrimestre
                gperContable = PerContable
                gPerContableSis = PerContableSis
                gseqProceso = SeqProceso
                gvalMlCuotaDestinoA = ValMlCuotaDestinoA
                gvalMlCuotaDestinoB = ValMlCuotaDestinoB
                gvalMlCuotaDestinoC = ValMlCuotaDestinoC
                gvalMlCuotaDestinoD = ValMlCuotaDestinoD
                gvalMlCuotaDestinoE = ValMlCuotaDestinoE
                blPermiteAcreditacionParcial = PermiteAcreditacionParcial

                'If Not gExisteEncabezado Then
                '    CrearEncabezadoAcred()
                'End If
                gdbc.Commit()

                If Not blPermiteAcreditacionParcial Then
                    gdbc.BeginTrans()
                End If
                For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1

                    rTrn = Nothing
                    rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                    If blPermiteAcreditacionParcial And rTrn.idCliente <> gIdClienteAnterior Then
                        gdbc.BeginTrans()

                    End If

                    LimpiarDatos()
                    If (rTrn.tipoImputacion = "ABO" And rTrn.codDestinoTransaccion = "CTA") Or _
                       (rTrn.tipoImputacion = "ABO" And rTrn.codDestinoTransaccion = "REZ") Then
                        LlenaValoresIniciales()

                        ValidarDatosBasicos()
                        If Not blAcreditarARezago And Not blIgnorar Then

                            gcausalRezago = LeerDatosCliente()

                            If Not blAcreditarARezago And Not blIgnorar Then
                                ValidarParaAcreditacion()
                            End If

                        End If

                        Select Case True

                            Case blIgnorar

                            Case blAcreditarARezago
                                AcreditaRezagoAjuste()

                            Case rTrn.codDestinoTransaccion = "REZ"
                                AcreditaRezagoAjuste()

                            Case rMovAcr.tipoMvto = "RDEV" 'Pagos en exceso 
                                AcreditaDevExcesos()

                            Case rMovAcr.tipoMvto = "RRET" 'Retiros
                                AcreditaDevExcesos()

                            Case rMovAcr.tipoMvto = "RPPE" 'Pago de beneficios
                                AcreditaDevExcesos()

                            Case Else
                                blIgnorar = True
                                rTrn.codError = 15308 'Tipo movimiento no fue reconocido
                                GenerarLog("A", 15308, "Hebra " & IdHebra & " - Tipo movimiento: " & rMovAcr.tipoMvto, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                        End Select

                        Select Case True

                            Case blIgnorar

                                IgnorarRegistroTrn()
                                'modificar solicitud con error

                            Case Else
                                ModificarRegistroTrn() 'aqui se actualizan los totales

                                If blPermiteAcreditacionParcial And ultimaTrnCliente() Then

                                    blIgnorar = Not RegistroContable()

                                    If blIgnorar Then
                                        IgnorarRegistroTrn() 'Rollback
                                    Else
                                        gdbc.Commit()
                                        determinaEstadoError()
                                    End If

                                End If

                                Select Case gtipoProceso
                                    Case "SI"
                                        gTotRegistrosSimulados = gTotRegistrosSimulados + 1
                                    Case "AC"
                                        gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
                                End Select


                        End Select
                    ElseIf (rTrn.codDestinoTransaccion = "RND") Then

                        AcreditaAjustesEspeciales()

                    End If

                Next II

                'ReintegroCuentas.actualizarCaducidad(gdbc,gidAdm, gcodOrigenProceso, gnumeroId, gidUsuarioProceso, gfuncion)

                'FALTA LA MODIFICACION DE LOS ESTADOS DEL CHEQUE

                AcredReintegroCheque = TotalesControlAcreditacion()

                'If gTotRegistrosIgnorados > 0 Then

                '    GenerarLog("A", 0, "Hebra " & IdHebra & " - Existen registros ignorados", 9, Nothing, Nothing)
                'End If

            End If


        Catch e As SondaException
            Dim mensaje As String
            gdbc.Rollback()
            AcredReintegroCheque = e.codigo
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim mensaje As String
            gdbc.Rollback()
            AcredReintegroCheque = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()

        End Try

    End Function
    Public Function AcredComisionSaldo(ByRef dbc As OraConn, _
                                       ByVal idAdm As Integer, _
                                       ByVal codOrigenProceso As String, _
                                       ByVal idUsuarioProceso As String, _
                                       ByVal numeroId As Long, _
                                       ByVal tipoProceso As String, _
                                       ByVal IdHebra As Integer, _
                                       ByVal LOG As Procesos.logEtapa, _
                                       ByVal FecAcreditacion As Date, _
                                       ByVal FecValorCuota As Date, _
                                       ByVal PerCuatrimestre As Date, _
                                       ByVal PerContable As Date, _
                                       ByVal PerContableSis As Date, _
                                       ByVal SeqProceso As Decimal, _
                                       ByVal ValMlCuotaDestinoA As Decimal, _
                                       ByVal ValMlCuotaDestinoB As Decimal, _
                                       ByVal ValMlCuotaDestinoC As Decimal, _
                                       ByVal ValMlCuotaDestinoD As Decimal, _
                                       ByVal ValMlCuotaDestinoE As Decimal, _
                                       ByVal PermiteAcreditacionParcial As Boolean) As Long

        Try
            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gtipoProceso = tipoProceso
            gfuncion = "AcredComisSaldo"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()

            evento(LOG, "Hebra " & IdHebra & " - Se comienzan a acreditar las Transacciones")
            EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc,idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)
            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)

            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'Throw New SondaException(15307) '"No existen registros para acreditar
            Else

                'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
                'ValoresAcreditacion()
                gfecAcreditacion = FecAcreditacion
                gfecValorCuota = FecValorCuota
                gperCuatrimestre = PerCuatrimestre
                gperContable = PerContable
                gPerContableSis = PerContableSis
                gseqProceso = SeqProceso
                gvalMlCuotaDestinoA = ValMlCuotaDestinoA
                gvalMlCuotaDestinoB = ValMlCuotaDestinoB
                gvalMlCuotaDestinoC = ValMlCuotaDestinoC
                gvalMlCuotaDestinoD = ValMlCuotaDestinoD
                gvalMlCuotaDestinoE = ValMlCuotaDestinoE
                blPermiteAcreditacionParcial = PermiteAcreditacionParcial

                'If Not gExisteEncabezado Then
                '    CrearEncabezadoAcred()
                'End If
                gdbc.Commit()

                If Not blPermiteAcreditacionParcial Then
                    gdbc.BeginTrans()
                End If

                For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1

                    rTrn = Nothing
                    rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                    If rTrn.seqRegistro = 1 Then
                        rTrn.seqRegistro = 1
                    End If

                    If blPermiteAcreditacionParcial And rTrn.idCliente <> gIdClienteAnterior Then
                        gdbc.BeginTrans()
                    End If

                    LimpiarDatos()
                    LlenaValoresIniciales()

                    'Se mueve fecha acreditacion a fecha operacion para que no rezague por causal 1D
                    rTrn.fecOperacion = gfecAcreditacion

                    ValidarDatosBasicos()

                    If Not blAcreditarARezago And Not blIgnorar Then

                        gcausalRezago = LeerDatosCliente()

                        If Not blAcreditarARezago And Not blIgnorar Then
                            ValidarParaAcreditacion()
                        End If

                    End If

                    Select Case True

                        Case blIgnorar

                        Case blAcreditarARezago
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rTrn.codDestinoTransaccion = "REZ"
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rMovAcr.tipoMvto = "COM"
                            AcreditaComision()

                        Case rMovAcr.tipoMvto = "RCOM"
                            AcreditaComision()

                        Case Else
                            blIgnorar = True
                            rTrn.codError = 15308 'Tipo movimiento no fue reconocido
                            GenerarLog("A", 15308, "Hebra " & IdHebra & " - Tipo movimiento: " & rMovAcr.tipoMvto, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                    End Select

                    Select Case True

                        Case blIgnorar

                            IgnorarRegistroTrn()

                            'modificar comision con error
                            Comisiones.ComisionAdmSaldo.modEstadoAcreditacion(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.tipoFondoDestino, rTrn.categoria, rTrn.subCategoria, rTrn.codRegTributario, rTrn.perCotizacion, "ER", gidUsuarioProceso, gfuncion)
                        Case Else

                            ModificarRegistroTrn() 'aqui se actualizan los totales

                            If blPermiteAcreditacionParcial And ultimaTrnCliente() Then

                                blIgnorar = Not RegistroContable()

                                If blIgnorar Then
                                    IgnorarRegistroTrn() 'Rollback
                                Else
                                    gdbc.Commit()
                                    determinaEstadoError()
                                End If

                            End If

                            Select Case gtipoProceso
                                Case "SI"
                                    gTotRegistrosSimulados = gTotRegistrosSimulados + 1
                                Case "AC"
                                    gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
                            End Select



                    End Select
                    If II + 1 Mod 1000 = 0 Then
                        evento(LOG, "Hebra " & IdHebra & " - Se han procesado " & II + 1 & " registros")
                    End If
                Next II

                evento(LOG, "Hebra " & IdHebra & " - Se procesaron " & II & " registros")


                'AcredComisionSaldo = TotalesControlAcreditacion()

                'If gTotRegistrosIgnorados > 0 Then
                '    GenerarLog("A", 0, "Hebra " & IdHebra & " - Existen registros ignorados", 9, Nothing, Nothing)
                '    evento(LOG, "Hebra " & IdHebra & " - Existen registros ignorados")
                'End If
            End If

        Catch e As SondaException
            Dim mensaje As String
            gdbc.Rollback()
            AcredComisionSaldo = e.codigo
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim mensaje As String
            gdbc.Rollback()
            AcredComisionSaldo = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()

        End Try

    End Function

    Public Function AcredGeneracionExcesos(ByRef dbc As OraConn, _
                                       ByVal idAdm As Integer, _
                                       ByVal codOrigenProceso As String, _
                                       ByVal idUsuarioProceso As String, _
                                       ByVal numeroId As Long, _
                                       ByVal tipoProceso As String, _
                                       ByVal IdHebra As Integer, _
                                       ByVal LOG As Procesos.logEtapa, _
                                  ByVal FecAcreditacion As Date, _
                                  ByVal FecValorCuota As Date, _
                                  ByVal PerCuatrimestre As Date, _
                                  ByVal PerContable As Date, _
                                  ByVal PerContableSis As Date, _
                                  ByVal SeqProceso As Decimal, _
                                  ByVal ValMlCuotaDestinoA As Decimal, _
                                  ByVal ValMlCuotaDestinoB As Decimal, _
                                  ByVal ValMlCuotaDestinoC As Decimal, _
                                  ByVal ValMlCuotaDestinoD As Decimal, _
                                  ByVal ValMlCuotaDestinoE As Decimal, _
                                  ByVal PermiteAcreditacionParcial As Boolean) As Long

        Dim dsRtrn As New DataSet()
        Dim error_ape As Integer
        Dim seq_log As Integer

        Try
            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gtipoProceso = tipoProceso
            gfuncion = "AcredGenExcesos"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()
            '''''''''MSC: Agregar este bloque de codigo en este mismo punto
            ''''''''If tipoProceso <> "AC" Then
            ''''''''    evento(LOG, "Se inicia proceso apertura de transacciones")

            ''''''''    ' apertura transacciones
            ''''''''    dsRtrn = Transacciones.aperturaTransaccion(gdbc,gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId, gtipoProceso)
            ''''''''    error_ape = dsRtrn.Tables(0).Rows(0).Item("verror_ape")
            ''''''''    seq_log = dsRtrn.Tables(0).Rows(0).Item("vseq_log")

            ''''''''    If error_ape <> 0 Then
            ''''''''        blErrorFatal = True
            ''''''''        GenerarLog("E", 99999, Nothing, 0, Nothing, Nothing)
            ''''''''        evento(LOG, "Error fatal en apertura de transacciones")
            ''''''''        Throw New SondaException(99999) 'No existen registros para acreditar
            ''''''''    End If
            ''''''''    evento(LOG, "Proceso apertura de transacciones finalizado")
            ''''''''    gSeqLog = seq_log  ' se asigna variable obtenida de la llamada del procedimiento
            ''''''''End If
            '''''''''FIN MSC 


            evento(LOG, "Hebra " & IdHebra & " - Se comienzan a acreditar las Transacciones")
            EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc,idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)
            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)

            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'Throw New SondaException(15307) '"No existen registros para acreditar
            Else

                'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
                'ValoresAcreditacion()
                gfecAcreditacion = FecAcreditacion
                gfecValorCuota = FecValorCuota
                gperCuatrimestre = PerCuatrimestre
                gperContable = PerContable
                gPerContableSis = PerContableSis
                gseqProceso = SeqProceso
                gvalMlCuotaDestinoA = ValMlCuotaDestinoA
                gvalMlCuotaDestinoB = ValMlCuotaDestinoB
                gvalMlCuotaDestinoC = ValMlCuotaDestinoC
                gvalMlCuotaDestinoD = ValMlCuotaDestinoD
                gvalMlCuotaDestinoE = ValMlCuotaDestinoE
                blPermiteAcreditacionParcial = PermiteAcreditacionParcial

                'If Not gExisteEncabezado Then
                '    CrearEncabezadoAcred()
                'End If
                gdbc.Commit()

                If Not blPermiteAcreditacionParcial Then
                    gdbc.BeginTrans()
                End If

                For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1

                    rTrn = Nothing
                    rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                    If blPermiteAcreditacionParcial And rTrn.idCliente <> gIdClienteAnterior Then
                        gdbc.BeginTrans()
                    End If

                    LimpiarDatos()
                    LlenaValoresIniciales()

                    'Se mueve fecha acreditacion a fecha operacion para que no rezague por causal 1D
                    rTrn.fecOperacion = gfecAcreditacion

                    ValidarDatosBasicos()

                    If Not blAcreditarARezago And Not blIgnorar Then
                        gcausalRezago = LeerDatosCliente()
                        If Not blAcreditarARezago And Not blIgnorar Then
                            ValidarParaAcreditacion()
                        End If
                    End If

                    Select Case True
                        Case blIgnorar
                        Case blAcreditarARezago
                            'lfc:22-03-2018 - modificacion por exceso empleador a rezagos
                            If rTrn.codCausalRezago = "31" And rTrn.codDestinoTransaccion = "REZ" Then

                                rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste + _
                                                        rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste + _
                                                        rTrn.valMlPrimaSis + rTrn.valMlPrimaSisInteres + rTrn.valMlPrimaSisReajuste + _
                                                        rTrn.valMlExcesoLinea + rTrn.valMlExcesoEmpl

                                rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto + rTrn.valCuoInteres + rTrn.valCuoReajuste + _
                                                        rTrn.valCuoAdicional + rTrn.valCuoAdicionalInteres + rTrn.valCuoAdicionalReajuste + _
                                                        rTrn.valCuoPrimaSis + rTrn.valCuoPrimaSisInteres + rTrn.valCuoPrimaSisReajuste + _
                                                        rTrn.valCuoExcesoLinea + rTrn.valCuoExcesoEmpl
                                Me.AcreditaRezago()
                            Else
                                blIgnorar = True
                                rTrn.codError = 15309 'Movimiento se rezaga
                                GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                            End If
                        Case rTrn.codDestinoTransaccion = "REZ"
                            'lfc:22-03-2018 - modificacion por exceso empleador a rezagos
                            If rTrn.codCausalRezago = "31" And rTrn.codDestinoTransaccion = "REZ" Then
                                rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste + _
                                                        rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste + _
                                                        rTrn.valMlPrimaSis + rTrn.valMlPrimaSisInteres + rTrn.valMlPrimaSisReajuste + _
                                                        rTrn.valMlExcesoLinea + rTrn.valMlExcesoEmpl

                                rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto + rTrn.valCuoInteres + rTrn.valCuoReajuste + _
                                                        rTrn.valCuoAdicional + rTrn.valCuoAdicionalInteres + rTrn.valCuoAdicionalReajuste + _
                                                        rTrn.valCuoPrimaSis + rTrn.valCuoPrimaSisInteres + rTrn.valCuoPrimaSisReajuste + _
                                                        rTrn.valCuoExcesoLinea + rTrn.valCuoExcesoEmpl

                                Me.AcreditaRezago()
                            Else
                                blIgnorar = True
                                rTrn.codError = 15309 'Movimiento se rezaga
                                GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                            End If
                        Case rMovAcr.tipoMvto = "RCOT"
                            AcreditaCotCargo()
                        Case rMovAcr.tipoMvto = "EXC"
                            AcreditaDevExcesos()
                        Case rMovAcr.tipoMvto = "RPRI"
                            AcreditaPrimas()
                        Case rMovAcr.tipoMvto = "RCOM"
                            AcreditaComision()
                        Case rMovAcr.tipoMvto = "DEV" 'lfc:08-05-2018 OS-9706059 
                            AcreditaDevExcesos()
                        Case Else
                            blIgnorar = True
                            rTrn.codError = 15308 'Tipo movimiento no fue reconocido
                            GenerarLog("A", 15308, "Hebra " & IdHebra & " - Tipo movimiento: " & rMovAcr.tipoMvto, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                    End Select

                    Select Case True
                        Case blIgnorar
                            IgnorarRegistroTrn()

                            'modificar comision con error
                            'sysExcesos.modificarEstadoExceso(gdbc, gidAdm, rTrn.numReferenciaOrigen1, rTrn.perCotizacion, rTrn.idCliente, rTrn.idEmpleador, rTrn.numReferenciaOrigen2, "ER", gidUsuarioProceso, gfuncion)
                            Sys.IngresoEgreso.sysExcesos.modificarEstadoExceso(gdbc, gidAdm, rTrn.numReferenciaOrigen1, rTrn.perCotizacion, rTrn.idCliente, rTrn.idEmpleador, rTrn.numReferenciaOrigen2, "ER", gidUsuarioProceso, gfuncion)


                        Case Else

                            'PCI 06/11/2013
                            If ultimaTrnCliente() Then
                                If CuentaSobregirada() Then
                                    IgnorarRegistroTrn()
                                End If

                            End If


                            ModificarRegistroTrn() 'aqui se actualizan los totales
                            If blPermiteAcreditacionParcial And ultimaTrnCliente() Then
                                blIgnorar = Not RegistroContable()
                                If blIgnorar Then
                                    IgnorarRegistroTrn() 'Rollback
                                Else
                                    gdbc.Commit()
                                    determinaEstadoError()
                                End If
                            End If
                            Select Case gtipoProceso
                                Case "SI"
                                    gTotRegistrosSimulados = gTotRegistrosSimulados + 1
                                    'modificar comision con error
                                    'sysExcesos.modificarEstadoExceso(gdbc, gidAdm, rTrn.numReferenciaOrigen1, rTrn.perCotizacion, rTrn.idCliente, rTrn.idEmpleador, rTrn.numReferenciaOrigen2, "SI", gidUsuarioProceso, gfuncion)
                                    Sys.IngresoEgreso.sysExcesos.modificarEstadoExceso(gdbc, gidAdm, rTrn.numReferenciaOrigen1, rTrn.perCotizacion, rTrn.idCliente, rTrn.idEmpleador, rTrn.numReferenciaOrigen2, "SI", gidUsuarioProceso, gfuncion)

                                Case "AC"
                                    'modificar comision con error
                                    'sysExcesos.modificarEstadoExceso(gdbc, gidAdm, rTrn.numReferenciaOrigen1, rTrn.perCotizacion, rTrn.idCliente, rTrn.idEmpleador, rTrn.numReferenciaOrigen2, "AC", gidUsuarioProceso, gfuncion)
                                    Sys.IngresoEgreso.sysExcesos.modificarEstadoExceso(gdbc, gidAdm, rTrn.numReferenciaOrigen1, rTrn.perCotizacion, rTrn.idCliente, rTrn.idEmpleador, rTrn.numReferenciaOrigen2, "AC", gidUsuarioProceso, gfuncion)

                                    gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
                            End Select
                    End Select
                    If II + 1 Mod 1000 = 0 Then
                        evento(LOG, "Hebra " & IdHebra & " - Se han procesado " & II + 1 & " registros")
                    End If
                Next II
                evento(LOG, "Hebra " & IdHebra & " - Se procesaron " & II & " registros")
                'AcredGeneracionExcesos = TotalesControlAcreditacion()

                'If gTotRegistrosIgnorados > 0 Then
                '    GenerarLog("A", 0, "Hebra " & IdHebra & " - Existen registros ignorados", 9, Nothing, Nothing)
                '    evento(LOG, "Hebra " & IdHebra & " - Existen registros ignorados")
                'End If
            End If
        Catch e As SondaException
            Dim mensaje As String
            gdbc.Rollback()
            AcredGeneracionExcesos = e.codigo
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim mensaje As String
            gdbc.Rollback()
            AcredGeneracionExcesos = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()
        End Try
    End Function
    Public Function AcredIngresoAPV(ByRef dbc As OraConn, _
                                    ByVal idAdm As Integer, _
                                   ByVal codOrigenProceso As String, _
                                   ByVal idUsuarioProceso As String, _
                                   ByVal numeroId As Long, _
                                   ByVal tipoProceso As String, _
                                   ByVal IdHebra As String, _
                                   ByVal LOG As Procesos.logEtapa, _
                                   ByVal FecAcreditacion As Date, _
                                   ByVal FecValorCuota As Date, _
                                   ByVal PerCuatrimestre As Date, _
                                   ByVal PerContable As Date, _
                                   ByVal PerContableSis As Date, _
                                   ByVal SeqProceso As Decimal, _
                                   ByVal ValMlCuotaDestinoA As Decimal, _
                                   ByVal ValMlCuotaDestinoB As Decimal, _
                                   ByVal ValMlCuotaDestinoC As Decimal, _
                                   ByVal ValMlCuotaDestinoD As Decimal, _
                                   ByVal ValMlCuotaDestinoE As Decimal, _
                                   ByVal PermiteAcreditacionParcial As Boolean) As Long

        Try
            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gtipoProceso = tipoProceso
            gfuncion = "AcredIngresoAPV"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()

            EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc,idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)
            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)
            gdbc.Rollback()

            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'Throw New SondaException(15307) '"No existen registros para acreditar
            Else
                gdbc.BeginTrans()
                'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
                'ValoresAcreditacion()
                gfecAcreditacion = FecAcreditacion
                gfecValorCuota = FecValorCuota
                gperCuatrimestre = PerCuatrimestre
                gperContable = PerContable
                gPerContableSis = PerContableSis
                gseqProceso = SeqProceso
                gvalMlCuotaDestinoA = ValMlCuotaDestinoA
                gvalMlCuotaDestinoB = ValMlCuotaDestinoB
                gvalMlCuotaDestinoC = ValMlCuotaDestinoC
                gvalMlCuotaDestinoD = ValMlCuotaDestinoD
                gvalMlCuotaDestinoE = ValMlCuotaDestinoE
                blPermiteAcreditacionParcial = PermiteAcreditacionParcial

                'If Not gExisteEncabezado Then
                '    CrearEncabezadoAcred()
                'End If
                gdbc.Commit()

                If Not blPermiteAcreditacionParcial Then
                    gdbc.BeginTrans()
                End If

                For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1

                    rTrn = Nothing
                    rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                    If blPermiteAcreditacionParcial And rTrn.idCliente <> gIdClienteAnterior Then
                        gdbc.BeginTrans()

                    End If

                    LimpiarDatos()
                    LlenaValoresIniciales()

                    ValidarDatosBasicos()
                    'ValorCuotaCaja()

                    If Not blAcreditarARezago And Not blIgnorar Then

                        gcausalRezago = LeerDatosCliente()

                        If Not blAcreditarARezago And Not blIgnorar Then
                            ValidarParaAcreditacion()
                        End If

                    End If

                    Select Case True

                        Case blIgnorar

                        Case blAcreditarARezago
                            ' AcreditaRezago()
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rTrn.codDestinoTransaccion = "REZ"
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rMovAcr.tipoMvto = "TIC"
                            AcreditaSaldoIngreso()

						Case rMovAcr.tipoMvto = "AJU"				   'lfc: 26-01-2021 - 7080923 añade soporte para mvtos de cargos en lotes de Ingreso APV
							Me.AcreditaAjuste()

						Case Else
							blIgnorar = True
							rTrn.codError = 15308							'Tipo movimiento no fue reconocido
							GenerarLog("A", 15308, "Hebra " & IdHebra & " - Tipo movimiento: " & rMovAcr.tipoMvto, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
					End Select

                    Select Case True

                        Case blIgnorar

                            IgnorarRegistroTrn()
                            'modificar solicitud ingreso con error
                        Case Else
                            ModificarRegistroTrn() 'aqui se actualizan los totales

                            If blPermiteAcreditacionParcial And ultimaTrnCliente() Then

                                blIgnorar = Not RegistroContable()

                                If blIgnorar Then
                                    IgnorarRegistroTrn() 'Rollback
                                Else
                                    gdbc.Commit()
                                    determinaEstadoError()
                                End If

                            End If

                            Select Case gtipoProceso
                                Case "SI"
                                    gTotRegistrosSimulados = gTotRegistrosSimulados + 1
                                Case "AC"
                                    gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
                            End Select


                    End Select

                Next II


                'AcredIngresoAPV = TotalesControlAcreditacion()

                'If gTotRegistrosIgnorados > 0 Then

                '    GenerarLog("A", 0, "Hebra " & IdHebra & " - Existen registros ignorados", 9, Nothing, Nothing)
                'End If

            End If

        Catch e As SondaException
            Dim mensaje As String
            gdbc.Rollback()
            AcredIngresoAPV = e.codigo
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim mensaje As String
            gdbc.Rollback()
            AcredIngresoAPV = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()

        End Try

    End Function


    Public Function AcredIngresoTRF(ByRef dbc As OraConn, _
                                    ByVal idAdm As Integer, _
                                    ByVal codOrigenProceso As String, _
                                    ByVal idUsuarioProceso As String, _
                                    ByVal numeroId As Long, _
                                    ByVal tipoProceso As String, _
                                    ByVal IdHebra As Integer, _
                                    ByVal LOG As Procesos.logEtapa, _
                                    ByVal FecAcreditacion As Date, _
                                    ByVal FecValorCuota As Date, _
                                    ByVal PerCuatrimestre As Date, _
                                    ByVal PerContable As Date, _
                                    ByVal PerContableSis As Date, _
                                    ByVal SeqProceso As Decimal, _
                                    ByVal ValMlCuotaDestinoA As Decimal, _
                                    ByVal ValMlCuotaDestinoB As Decimal, _
                                    ByVal ValMlCuotaDestinoC As Decimal, _
                                    ByVal ValMlCuotaDestinoD As Decimal, _
                                    ByVal ValMlCuotaDestinoE As Decimal, _
                                    ByVal PermiteAcreditacionParcial As Boolean) As Long

        Try
            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gtipoProceso = tipoProceso
            gfuncion = "AcredIngresoTRF"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()

            EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc,idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)
            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)
            gdbc.Rollback()

            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'Throw New SondaException(15307) '"No existen registros para acreditar
            Else
                gdbc.BeginTrans()
                'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
                'ValoresAcreditacion()
                gfecAcreditacion = FecAcreditacion
                gfecValorCuota = FecValorCuota
                gperCuatrimestre = PerCuatrimestre
                gperContable = PerContable
                gPerContableSis = PerContableSis
                gseqProceso = SeqProceso
                gvalMlCuotaDestinoA = ValMlCuotaDestinoA
                gvalMlCuotaDestinoB = ValMlCuotaDestinoB
                gvalMlCuotaDestinoC = ValMlCuotaDestinoC
                gvalMlCuotaDestinoD = ValMlCuotaDestinoD
                gvalMlCuotaDestinoE = ValMlCuotaDestinoE
                blPermiteAcreditacionParcial = PermiteAcreditacionParcial

                'If Not gExisteEncabezado Then
                '    CrearEncabezadoAcred()
                'End If
                gdbc.Commit()

                If Not blPermiteAcreditacionParcial Then
                    gdbc.BeginTrans()
                End If

                For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1

                    rTrn = Nothing
                    rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                    If blPermiteAcreditacionParcial And rTrn.idCliente <> gIdClienteAnterior Then
                        gdbc.BeginTrans()

                    End If

                    LimpiarDatos()
                    LlenaValoresIniciales()

                    ValidarDatosBasicos()
                    'ValorCuotaCaja()

                    If Not blAcreditarARezago And Not blIgnorar Then

                        gcausalRezago = LeerDatosCliente()

                        If Not blAcreditarARezago And Not blIgnorar Then
                            ValidarParaAcreditacion()
                        End If

                    End If

                    Select Case True

                        Case blIgnorar

                        Case blAcreditarARezago
                            AcreditaRezago()
                            'blIgnorar = True
                            'rTrn.codError = 15309 'Movimiento se rezaga
                            'GenerarLog("A", 15309, "Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rTrn.codDestinoTransaccion = "REZ"
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rMovAcr.tipoMvto = "TIC"
                            AcreditaSaldoIngreso()

                            'Case rMovAcr.tipoMvto = "COT"
                            '    AcreditaSaldoIngreso()

                        Case Else
                            blIgnorar = True
                            rTrn.codError = 15308 'Tipo movimiento no fue reconocido
                            GenerarLog("A", 15308, "Hebra " & IdHebra & " - Tipo movimiento: " & rMovAcr.tipoMvto, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                    End Select

                    Select Case True

                        Case blIgnorar

                            IgnorarRegistroTrn()
                            'modificar solicitud ingreso con error

                        Case Else
                            ModificarRegistroTrn() 'aqui se actualizan los totales

                            If blPermiteAcreditacionParcial And ultimaTrnCliente() Then

                                blIgnorar = Not RegistroContable()

                                If blIgnorar Then
                                    IgnorarRegistroTrn() 'Rollback
                                Else
                                    gdbc.Commit()
                                    determinaEstadoError()
                                End If

                            End If

                            Select Case gtipoProceso
                                Case "SI"
                                    gTotRegistrosSimulados = gTotRegistrosSimulados + 1
                                Case "AC"
                                    gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
                            End Select


                    End Select

                Next II


                'AcredIngresoTRF = TotalesControlAcreditacion()

                'If gTotRegistrosIgnorados > 0 Then

                '    GenerarLog("A", 0, "Hebra " & IdHebra & " - Existen registros ignorados", 9, Nothing, Nothing)
                'End If

            End If

        Catch e As SondaException
            Dim mensaje As String
            gdbc.Rollback()
            AcredIngresoTRF = e.codigo
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim mensaje As String
            gdbc.Rollback()
            AcredIngresoTRF = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()

        End Try

    End Function


    Public Function AcredIngresoCTA(ByRef dbc As OraConn, _
                                    ByVal idAdm As Integer, _
                                    ByVal codOrigenProceso As String, _
                                    ByVal idUsuarioProceso As String, _
                                    ByVal numeroId As Long, _
                                    ByVal tipoProceso As String, _
                                    ByVal IdHebra As Integer, _
                                    ByVal LOG As Procesos.logEtapa, _
                                    ByVal FecAcreditacion As Date, _
                                    ByVal FecValorCuota As Date, _
                                    ByVal PerCuatrimestre As Date, _
                                    ByVal PerContable As Date, _
                                    ByVal PerContableSis As Date, _
                                    ByVal SeqProceso As Decimal, _
                                    ByVal ValMlCuotaDestinoA As Decimal, _
                                    ByVal ValMlCuotaDestinoB As Decimal, _
                                    ByVal ValMlCuotaDestinoC As Decimal, _
                                    ByVal ValMlCuotaDestinoD As Decimal, _
                                    ByVal ValMlCuotaDestinoE As Decimal, _
                                    ByVal PermiteAcreditacionParcial As Boolean) As Long

        Try
            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gtipoProceso = tipoProceso
            gfuncion = "AcredIngresoCTA"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()


            evento(LOG, "Hebra " & IdHebra & " - Se comienzan a acreditar las Transacciones")

            EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc,idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)
            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)
            gdbc.Rollback()

            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'Throw New SondaException(15307) '"No existen registros para acreditar
            Else
                gdbc.BeginTrans()
                'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
                'ValoresAcreditacion()
                gfecAcreditacion = FecAcreditacion
                gfecValorCuota = FecValorCuota
                gperCuatrimestre = PerCuatrimestre
                gperContable = PerContable
                gPerContableSis = PerContableSis
                gseqProceso = SeqProceso
                gvalMlCuotaDestinoA = ValMlCuotaDestinoA
                gvalMlCuotaDestinoB = ValMlCuotaDestinoB
                gvalMlCuotaDestinoC = ValMlCuotaDestinoC
                gvalMlCuotaDestinoD = ValMlCuotaDestinoD
                gvalMlCuotaDestinoE = ValMlCuotaDestinoE
                blPermiteAcreditacionParcial = PermiteAcreditacionParcial

                'If Not gExisteEncabezado Then
                '    CrearEncabezadoAcred()
                'End If
                gdbc.Commit()

                If Not blPermiteAcreditacionParcial Then
                    gdbc.BeginTrans()
                End If

                For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1

                    rTrn = Nothing
                    rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                    If blPermiteAcreditacionParcial And rTrn.idCliente <> gIdClienteAnterior Then
                        gdbc.BeginTrans()

                    End If

                    LimpiarDatos()
                    LlenaValoresIniciales()

                    ValidarDatosBasicos()
                    'ValorCuotaCaja()

                    If Not blAcreditarARezago And Not blIgnorar Then

                        gcausalRezago = LeerDatosCliente()

                        If Not blAcreditarARezago And Not blIgnorar Then
                            ValidarParaAcreditacion()
                        End If

                    End If

                    Select Case True

                        Case blIgnorar

                        Case blAcreditarARezago
                            'AcreditaRezago()
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rTrn.codDestinoTransaccion = "REZ"
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rMovAcr.tipoMvto = "TIC"
                            AcreditaSaldoIngreso()

                        Case Else
                            blIgnorar = True
                            rTrn.codError = 15308 'Tipo movimiento no fue reconocido
                            GenerarLog("A", 15308, "Hebra " & IdHebra & " - Tipo movimiento: " & rMovAcr.tipoMvto, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                    End Select

                    Select Case True

                        Case blIgnorar

                            IgnorarRegistroTrn()
                            'modificar solicitud ingreso con error

                        Case Else
                            'modificar solicitud ingreso con exito

                            ModificarRegistroTrn() 'aqui se actualizan los totales

                            If blPermiteAcreditacionParcial And ultimaTrnCliente() Then

                                blIgnorar = Not RegistroContable()

                                If blIgnorar Then
                                    IgnorarRegistroTrn() 'Rollback
                                Else
                                    gdbc.Commit()
                                    determinaEstadoError()
                                End If

                            End If

                            Select Case gtipoProceso
                                Case "SI"
                                    gTotRegistrosSimulados = gTotRegistrosSimulados + 1
                                Case "AC"
                                    gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
                            End Select


                    End Select
                    If II > 0 And II Mod 100 = 0 Then
                        evento(LOG, "Hebra " & IdHebra & " - Se han procesado " & II + 1 & " registros")
                    End If
                Next II

                evento(LOG, "Hebra " & IdHebra & " - Se procesaron " & II & " registros")


                'AcredIngresoCTA = TotalesControlAcreditacion()

                'If gTotRegistrosIgnorados > 0 Then

                '    GenerarLog("A", 0, "Hebra " & IdHebra & " - Existen registros ignorados", 9, Nothing, Nothing)
                'End If

            End If

        Catch e As SondaException
            Dim mensaje As String
            gdbc.Rollback()
            AcredIngresoCTA = e.codigo
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim mensaje As String
            gdbc.Rollback()
            AcredIngresoCTA = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()

        End Try

    End Function



    Public Function AcredIngresoREZ(ByVal idAdm As Integer, _
                                    ByVal codOrigenProceso As String, _
                                    ByVal idUsuarioProceso As String, _
                                    ByVal numeroId As Long, _
                                    ByVal tipoProceso As String, _
                                    ByVal IdHebra As Integer, _
                                    ByVal LOG As Procesos.logEtapa, _
                                    ByVal FecAcreditacion As Date, _
                                    ByVal FecValorCuota As Date, _
                                    ByVal PerCuatrimestre As Date, _
                                    ByVal PerContable As Date, _
                                    ByVal PerContableSis As Date, _
                                    ByVal SeqProceso As Decimal, _
                                    ByVal ValMlCuotaDestinoA As Decimal, _
                                    ByVal ValMlCuotaDestinoB As Decimal, _
                                    ByVal ValMlCuotaDestinoC As Decimal, _
                                    ByVal ValMlCuotaDestinoD As Decimal, _
                                    ByVal ValMlCuotaDestinoE As Decimal, _
                                    ByVal PermiteAcreditacionParcial As Boolean) As Long

        Try
            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gtipoProceso = tipoProceso
            gfuncion = "AcredIngresoREZ"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()

            EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc,idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)
            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)
            gdbc.Rollback()

            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'Throw New SondaException(15307) '"No existen registros para acreditar
            Else
                gdbc.BeginTrans()
                'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
                'ValoresAcreditacion()
                gfecAcreditacion = FecAcreditacion
                gfecValorCuota = FecValorCuota
                gperCuatrimestre = PerCuatrimestre
                gperContable = PerContable
                gPerContableSis = PerContableSis
                gseqProceso = SeqProceso
                gvalMlCuotaDestinoA = ValMlCuotaDestinoA
                gvalMlCuotaDestinoB = ValMlCuotaDestinoB
                gvalMlCuotaDestinoC = ValMlCuotaDestinoC
                gvalMlCuotaDestinoD = ValMlCuotaDestinoD
                gvalMlCuotaDestinoE = ValMlCuotaDestinoE
                blPermiteAcreditacionParcial = PermiteAcreditacionParcial

                'If Not gExisteEncabezado Then
                '    CrearEncabezadoAcred()
                'End If
                gdbc.Commit()


                If Not blPermiteAcreditacionParcial Then
                    gdbc.BeginTrans()
                End If

                For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1

                    rTrn = Nothing
                    rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                    If blPermiteAcreditacionParcial And rTrn.idCliente <> gIdClienteAnterior Then
                        gdbc.BeginTrans()

                    End If

                    LimpiarDatos()

                    LlenaValoresIniciales()
                    ValorCuotaCaja()

                    '--.-- CN2 If (rTrn.tipoProducto = "CCV" Or rTrn.tipoProducto = "CDC") And rTrn.codDestinoTransaccion <> "REZ" Then
                    If rTrn.codDestinoTransaccion <> "REZ" Then
                        DeterminarTransferencia()

                    End If

                    ValidarDatosBasicos()


                    If Not blAcreditarARezago And Not blIgnorar Then

                        gcausalRezago = LeerDatosCliente()

                        If Not blAcreditarARezago And Not blIgnorar Then
                            ValidarParaAcreditacion()
                        End If

                    End If

                    Select Case True

                        Case blIgnorar

                        Case rTrn.codDestinoTransaccion = "TRF"
                            AcreditaTransferencia() 'vien solo transferencia sin cotizacion

                        Case blAcreditarARezago
                            rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste + _
                                                    rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste + _
                                                    rTrn.valMlExcesoLinea

                            AcreditaRezago()

                        Case rTrn.codDestinoTransaccion = "REZ"

                            If rTrn.codCausalRezago = 25 Then

                                rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste + _
                                                        rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste + _
                                                        rTrn.valMlExcesoLinea

                                AcreditaRezago()
                            Else
                                blIgnorar = True
                                rTrn.codError = 15309 'Movimiento se rezaga
                                GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                            End If
                        Case rMovAcr.indCotizacion = "S"
                            AcreditaCotREZ()

                        Case Else
                            blIgnorar = True
                            rTrn.codError = 15308 'Tipo movimiento no fue reconocido
                            GenerarLog("A", 15308, "Hebra " & IdHebra & " - Tipo movimiento: " & rMovAcr.tipoMvto, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                    End Select

                    Select Case True

                        Case blIgnorar

                            IgnorarRegistroTrn()
                            'modificar solicitud ingreso con error

                        Case Else
                            'modificar solicitud ingreso con exito

                            ModificarRegistroTrn() 'aqui se actualizan los totales

                            If blPermiteAcreditacionParcial And ultimaTrnCliente() Then

                                blIgnorar = Not RegistroContable()

                                If blIgnorar Then
                                    IgnorarRegistroTrn() 'Rollback
                                Else
                                    gdbc.Commit()
                                    determinaEstadoError()
                                End If

                            End If

                            Select Case gtipoProceso
                                Case "SI"
                                    gTotRegistrosSimulados = gTotRegistrosSimulados + 1
                                Case "AC"
                                    gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
                            End Select


                    End Select

                Next II



                'AcredIngresoREZ = TotalesControlAcreditacion()

                'If gTotRegistrosIgnorados > 0 Then

                '    GenerarLog("A", 0, "Hebra " & IdHebra & " - Existen registros ignorados", 9, Nothing, Nothing)
                'End If

            End If

        Catch e As SondaException
            Dim mensaje As String
            gdbc.Rollback()
            AcredIngresoREZ = e.codigo
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim mensaje As String
            gdbc.Rollback()
            AcredIngresoREZ = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()

        End Try

    End Function
    Public Function AcredIngresoPAG(ByVal idAdm As Integer, _
                                        ByVal codOrigenProceso As String, _
                                        ByVal idUsuarioProceso As String, _
                                        ByVal numeroId As Long, _
                                        ByVal tipoProceso As String, _
                                        ByVal IdHebra As Integer, _
                                        ByVal LOG As Procesos.logEtapa, _
                                        ByVal FecAcreditacion As Date, _
                                        ByVal FecValorCuota As Date, _
                                        ByVal PerCuatrimestre As Date, _
                                        ByVal PerContable As Date, _
                                        ByVal PerContableSis As Date, _
                                        ByVal SeqProceso As Decimal, _
                                        ByVal ValMlCuotaDestinoA As Decimal, _
                                        ByVal ValMlCuotaDestinoB As Decimal, _
                                        ByVal ValMlCuotaDestinoC As Decimal, _
                                        ByVal ValMlCuotaDestinoD As Decimal, _
                                        ByVal ValMlCuotaDestinoE As Decimal, _
                                        ByVal PermiteAcreditacionParcial As Boolean) As Long

        Try
            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gtipoProceso = tipoProceso
            gfuncion = "AcredIngresoPAG"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()

            EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc,idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, idhebra)
            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)
            gdbc.Rollback()

            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'Throw New SondaException(15307) '"No existen registros para acreditar
            Else
                gdbc.BeginTrans()
                'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
                'ValoresAcreditacion()
                gfecAcreditacion = FecAcreditacion
                gfecValorCuota = FecValorCuota
                gperCuatrimestre = PerCuatrimestre
                gperContable = PerContable
                gPerContableSis = PerContableSis
                gseqProceso = SeqProceso
                gvalMlCuotaDestinoA = ValMlCuotaDestinoA
                gvalMlCuotaDestinoB = ValMlCuotaDestinoB
                gvalMlCuotaDestinoC = ValMlCuotaDestinoC
                gvalMlCuotaDestinoD = ValMlCuotaDestinoD
                gvalMlCuotaDestinoE = ValMlCuotaDestinoE
                blPermiteAcreditacionParcial = PermiteAcreditacionParcial

                'If Not gExisteEncabezado Then
                '    CrearEncabezadoAcred()
                'End If
                gdbc.Commit()


                If Not blPermiteAcreditacionParcial Then
                    gdbc.BeginTrans()
                End If

                For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1

                    rTrn = Nothing
                    rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                    If blPermiteAcreditacionParcial And rTrn.idCliente <> gIdClienteAnterior Then
                        gdbc.BeginTrans()

                    End If


                    LimpiarDatos()

                    LlenaValoresIniciales()

                    ValidarDatosBasicos()
                    ValorCuotaCaja()

                    If rTrn.codDestinoTransaccion = "TRF" Then
                        DeterminarTransferencia()
                    End If

                    If rTrn.codDestinoTransaccion <> "REZ" Then

                        If Not blAcreditarARezago And Not blIgnorar Then

                            gcausalRezago = LeerDatosCliente()

                            If Not blAcreditarARezago And Not blIgnorar Then
                                ValidarParaAcreditacion()
                            End If

                        End If
                    Else
                        'If rTrn.idCliente = 0 Then
                        '    gIdClienteAnterior = -1
                        'Else
                        '    gIdClienteAnterior = rTrn.idCliente
                        'End If
                        gIdClienteAnterior = -1

                    End If

                    Select Case True

                        Case blIgnorar

                        Case blAcreditarARezago

                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("E", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rTrn.codDestinoTransaccion = "REZ"
                            AcreditaRezagoAjuste()

                        Case rTrn.codDestinoTransaccion = "TRF" And rTrn.tipoImputacion = "ABO"
                            AcreditaTransferencia()

                        Case rMovAcr.indCotizacion = "S"
                            'AcreditaCotREZ()
                            AcreditaCotAjuste()

                        Case rMovAcr.tipoMvto = "AJU"
                            AcreditaOtrosMovs()

                        Case rMovAcr.tipoMvto = "TIC"
                            AcreditaSaldoIngreso()



                        Case Else
                            blIgnorar = True
                            rTrn.codError = 15308 'Tipo movimiento no fue reconocido
                            GenerarLog("A", 15308, "Hebra " & IdHebra & " - Tipo movimiento: " & rMovAcr.tipoMvto, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                    End Select

                    Select Case True

                        Case blIgnorar

                            IgnorarRegistroTrn()
                            'modificar solicitud ingreso con error

                        Case Else
                            'modificar solicitud ingreso con exito
                            acreditaAjusteDecimalInformado()

                            ModificarRegistroTrn() 'aqui se actualizan los totales

                            If blPermiteAcreditacionParcial And ultimaTrnCliente() Then

                                blIgnorar = Not RegistroContable()

                                If blIgnorar Then
                                    IgnorarRegistroTrn() 'Rollback
                                Else
                                    gdbc.Commit()
                                    determinaEstadoError()
                                End If

                            End If

                            Select Case gtipoProceso
                                Case "SI"
                                    gTotRegistrosSimulados = gTotRegistrosSimulados + 1
                                Case "AC"
                                    gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
                            End Select


                    End Select

                Next II



                'AcredIngresoPAG = TotalesControlAcreditacion()

                'If gTotRegistrosIgnorados > 0 Then

                '    GenerarLog("A", 0, "Hebra " & IdHebra & " - Existen registros ignorados", 9, Nothing, Nothing)
                'End If

            End If

        Catch e As SondaException
            Dim mensaje As String
            gdbc.Rollback()
            AcredIngresoPAG = e.codigo
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim mensaje As String
            gdbc.Rollback()
            AcredIngresoPAG = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()

        End Try

    End Function
    Public Function AcredIngresoPAG_new(ByRef dbc As OraConn, _
                                        ByVal idAdm As Integer, _
                                         ByVal codOrigenProceso As String, _
                                         ByVal idUsuarioProceso As String, _
                                         ByVal numeroId As Long, _
                                         ByVal tipoProceso As String, _
                                         ByVal IdHebra As String, _
                                         ByVal LOG As Procesos.logEtapa, _
                                         ByVal FecAcreditacion As Date, _
                                         ByVal FecValorCuota As Date, _
                                         ByVal PerCuatrimestre As Date, _
                                         ByVal PerContable As Date, _
                                         ByVal PerContableSis As Date, _
                                         ByVal SeqProceso As Decimal, _
                                         ByVal ValMlCuotaDestinoA As Decimal, _
                                         ByVal ValMlCuotaDestinoB As Decimal, _
                                         ByVal ValMlCuotaDestinoC As Decimal, _
                                         ByVal ValMlCuotaDestinoD As Decimal, _
                                         ByVal ValMlCuotaDestinoE As Decimal, _
                                         ByVal PermiteAcreditacionParcial As Boolean) As Long

        Dim dsRtrn As New DataSet()
        Dim error_ape As Integer
        Dim seq_log As Integer


        Try
            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gtipoProceso = tipoProceso
            gfuncion = "AcredIngresoPAG"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()

            '''':lfc 18-02-09
            ''''modifcar estado lote al inicio proceso
            'EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            ''MSC: Agregar este bloque de codigo en este mismo punto
            'If tipoProceso <> "AC" Then
            '    evento(LOG, "Hebra " & IdHebra & " - Se inicia proceso apertura de transacciones")

            '    ' apertura transacciones
            '    dsRtrn = Transacciones.aperturaTransaccion(gdbc,gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId, gtipoProceso)
            '    error_ape = dsRtrn.Tables(0).Rows(0).Item("verror_ape")
            '    seq_log = dsRtrn.Tables(0).Rows(0).Item("vseq_log")

            '    If error_ape <> 0 Then
            '        'Throw New SondaException(99999) 'Error fatal en apertura de transacciones

            '        blErrorFatal = True
            '        GenerarLog("E", 99999, Nothing, IdHebra, 0, Nothing, Nothing)
            '        evento(LOG, "Error fatal en apertura de transacciones")
            '        Throw New SondaException(99999) 'No existen registros para acreditar
            '    End If

            '    evento(LOG, "Hebra " & IdHebra & " - Proceso apertura de transacciones finalizado")

            '    gSeqLog = seq_log  ' se asigna variable obtenida de la llamada del procedimiento

            'End If
            ''FIN MSC 




            ' EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc,idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)
            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)
            gdbc.Rollback()

            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'Throw New SondaException(15307) '"No existen registros para acreditar
            End If

            gdbc.BeginTrans()
            'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
            'ValoresAcreditacion()
            gfecAcreditacion = FecAcreditacion
            gfecValorCuota = FecValorCuota
            gperCuatrimestre = PerCuatrimestre
            gperContable = PerContable
            gPerContableSis = PerContableSis
            gseqProceso = SeqProceso
            gvalMlCuotaDestinoA = ValMlCuotaDestinoA
            gvalMlCuotaDestinoB = ValMlCuotaDestinoB
            gvalMlCuotaDestinoC = ValMlCuotaDestinoC
            gvalMlCuotaDestinoD = ValMlCuotaDestinoD
            gvalMlCuotaDestinoE = ValMlCuotaDestinoE
            blPermiteAcreditacionParcial = PermiteAcreditacionParcial


            'If Not gExisteEncabezado Then
            '    CrearEncabezadoAcred()
            'End If
            EliminarIngresoRezagos()
            gdbc.Commit()

            If Not blPermiteAcreditacionParcial Then
                gdbc.BeginTrans()
            End If


            For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1

                rTrn = Nothing
                rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                If blPermiteAcreditacionParcial And rTrn.idCliente <> gIdClienteAnterior Then
                    gdbc.BeginTrans()
                End If

                If rTrn.seqRegistro = 1275484503 Then
                    rTrn.seqRegistro = 1275484503
                End If

                If rTrn.seqRegistro = 1275484569 Then
                    rTrn.seqRegistro = 1275484569
                End If

                If rTrn.seqRegistro = 1275484570 Then
                    rTrn.seqRegistro = 1275484570
                End If

                If rTrn.seqRegistro = 1275484577 Then
                    rTrn.seqRegistro = 1275484577
                End If

                LimpiarDatos()
                LlenaValoresIniciales()

                If rTrn.codDestinoTransaccionCal = "REZ" Then
                    blAcreditarARezago = True
                End If

                ValidarDatosBasicos()
                ValorCuotaCaja()

                If rTrn.codDestinoTransaccion = "TRF" Then
                    DeterminarTransferencia()
                End If

                'If rTrn.codDestinoTransaccion <> "REZ" Then
                If Not blAcreditarARezago And Not blIgnorar Then

                    gcausalRezago = LeerDatosCliente()

                    If Not blAcreditarARezago And Not blIgnorar Then
                        ValidarParaAcreditacion()
                    End If
                End If


                Select Case True

                    Case blIgnorar

                    Case blAcreditarARezago
                        descripcionCausalRez()

                        If rTrn.perCotizacion >= New Date(2009, 7, 1).Date And (rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF") Then
                            AcreditaRezagoIngreso2()
                        Else
                            AcreditaRezagoIngreso()
                        End If

                    Case rTrn.codDestinoTransaccionCal = "REZ"
                        If rTrn.perCotizacion >= New Date(2009, 7, 1).Date And (rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF") Then
                            AcreditaRezagoIngreso2()
                        Else
                            AcreditaRezagoIngreso()
                        End If

                    Case rTrn.codDestinoTransaccion = "TRF" And rTrn.tipoImputacion = "ABO"
                        AcreditaTransferencia()

                    Case rMovAcr.indCotizacion = "S"
                        If rTrn.perCotizacion >= New Date(2009, 7, 1).Date And (rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF") Then
                            'AcreditaCotRezNominal2()
                            AcreditaCotREC2()
                        Else
                            AcreditaCotRezNominal()
                        End If

                    Case rMovAcr.tipoMvto = "TIC"

                        AcreditaCotRezNominal()

                    Case rMovAcr.tipoMvto = "AJU" 'OS-10981406, procesa codigos mvto: 110197 - liq bono por hijo

                        AcreditaCotRezNominal()

                    Case Else
                        blIgnorar = True
                        rTrn.codError = 15308 'Tipo movimiento no fue reconocido
                        GenerarLog("A", 15308, "Hebra " & IdHebra & " - Tipo movimiento: " & rMovAcr.tipoMvto, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                End Select

                Select Case True

                    Case blIgnorar

                        IgnorarRegistroTrn()
                        'modificar solicitud ingreso con error

                    Case Else
                        'modificar solicitud ingreso con exito
                        'acreditaAjusteDecimalInformado()
                        CrearIngresoRezagos()
                        ModificarRegistroTrn() 'aqui se actualizan los totales

                        If blPermiteAcreditacionParcial And ultimaTrnCliente() Then

                            blIgnorar = Not RegistroContable()

                            If blIgnorar Then
                                IgnorarRegistroTrn() 'Rollback
                            Else
                                gdbc.Commit()
                                determinaEstadoError()
                            End If

                        End If

                        Select Case gtipoProceso
                            Case "SI"
                                gTotRegistrosSimulados = gTotRegistrosSimulados + 1
                            Case "AC"
                                gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
                        End Select


                End Select

            Next II



            'AcredIngresoPAG_new = TotalesControlAcreditacion()

            'If gTotRegistrosIgnorados > 0 Then

            '    GenerarLog("A", 0, "Hebra " & IdHebra & " - Existen registros ignorados", 9, Nothing, Nothing)
            'End If


        Catch e As SondaException
            Dim mensaje As String
            gdbc.Rollback()
            AcredIngresoPAG_new = e.codigo
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim mensaje As String
            gdbc.Rollback()
            AcredIngresoPAG_new = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()

        End Try

    End Function

    Public Function AcredEgresoAPV(ByRef dbc As OraConn, _
                                   ByVal idAdm As Integer, _
                                   ByVal codOrigenProceso As String, _
                                   ByVal idUsuarioProceso As String, _
                                   ByVal numeroId As Long, _
                                   ByVal tipoProceso As String, _
                                   ByVal IdHebra As Integer, _
                                   ByVal LOG As Procesos.logEtapa, _
                                   ByVal FecAcreditacion As Date, _
                                   ByVal FecValorCuota As Date, _
                                   ByVal PerCuatrimestre As Date, _
                                   ByVal PerContable As Date, _
                                   ByVal PerContableSis As Date, _
                                   ByVal SeqProceso As Decimal, _
                                   ByVal ValMlCuotaDestinoA As Decimal, _
                                   ByVal ValMlCuotaDestinoB As Decimal, _
                                   ByVal ValMlCuotaDestinoC As Decimal, _
                                   ByVal ValMlCuotaDestinoD As Decimal, _
                                   ByVal ValMlCuotaDestinoE As Decimal, _
                                   ByVal PermiteAcreditacionParcial As Boolean) As Long

        Try
            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gtipoProceso = tipoProceso
            gfuncion = "AcredEgresoAPV"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()

            EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc,idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)
            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)
            gdbc.Rollback()

            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'Throw New SondaException(15307) '"No existen registros para acreditar
            Else
                gdbc.BeginTrans()
                'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
                'ValoresAcreditacion()
                gfecAcreditacion = FecAcreditacion
                gfecValorCuota = FecValorCuota
                gperCuatrimestre = PerCuatrimestre
                gperContable = PerContable
                gPerContableSis = PerContableSis
                gseqProceso = SeqProceso
                gvalMlCuotaDestinoA = ValMlCuotaDestinoA
                gvalMlCuotaDestinoB = ValMlCuotaDestinoB
                gvalMlCuotaDestinoC = ValMlCuotaDestinoC
                gvalMlCuotaDestinoD = ValMlCuotaDestinoD
                gvalMlCuotaDestinoE = ValMlCuotaDestinoE
                blPermiteAcreditacionParcial = PermiteAcreditacionParcial

                'If Not gExisteEncabezado Then
                '    CrearEncabezadoAcred()
                'End If
                gdbc.Commit()

                If Not blPermiteAcreditacionParcial Then
                    gdbc.BeginTrans()
                End If

                For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1

                    rTrn = Nothing
                    rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                    If blPermiteAcreditacionParcial And rTrn.idCliente <> gIdClienteAnterior Then
                        gdbc.BeginTrans()

                    End If

                    LimpiarDatos()
                    LlenaValoresIniciales()

                    ValidarDatosBasicos()
                    If Not blAcreditarARezago And Not blIgnorar Then

                        gcausalRezago = LeerDatosCliente()

                        If Not blAcreditarARezago And Not blIgnorar Then
                            ValidarParaAcreditacion()
                        End If

                    End If

                    Select Case True

                        Case blIgnorar

                        Case blAcreditarARezago
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rTrn.codDestinoTransaccion = "REZ"
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rMovAcr.tipoMvto = "TEC"
                            AcreditaSaldoEgresoAPV()

                        Case rMovAcr.tipoMvto = "COM"
                            'Solicitado por Paulina Lagos Para soporte de Comision Adm. Saldo.
                            AcreditaComision()

                        Case Else
                            blIgnorar = True
                            rTrn.codError = 15308 'Tipo movimiento no fue reconocido
                            GenerarLog("A", 15308, "Hebra " & IdHebra & " - Tipo movimiento: " & rMovAcr.tipoMvto, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                    End Select

                    Select Case True

                        Case blIgnorar

                            IgnorarRegistroTrn()
                            'modificar solicitud egreso con error

                        Case Else
                            'modificar solicitud egreso con exito

                            ModificarRegistroTrn() 'aqui se actualizan los totales

                            If blPermiteAcreditacionParcial And ultimaTrnCliente() Then

                                blIgnorar = Not RegistroContable()

                                If blIgnorar Then
                                    IgnorarRegistroTrn() 'Rollback
                                Else
                                    gdbc.Commit()
                                    determinaEstadoError()
                                End If

                            End If

                            Select Case gtipoProceso
                                Case "SI"
                                    gTotRegistrosSimulados = gTotRegistrosSimulados + 1
                                Case "AC"
                                    gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
                            End Select


                    End Select

                Next II



                'AcredEgresoAPV = TotalesControlAcreditacion()

                'If gTotRegistrosIgnorados > 0 Then

                '    GenerarLog("A", 0, "Hebra " & IdHebra & " - Existen registros ignorados", 9, Nothing, Nothing)
                'End If

            End If

        Catch e As SondaException
            Dim mensaje As String
            gdbc.Rollback()
            AcredEgresoAPV = e.codigo
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim mensaje As String
            gdbc.Rollback()
            AcredEgresoAPV = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()

        End Try

    End Function



    Public Function AcredEgresoCTA(ByRef dbc As OraConn, _
                                   ByVal idAdm As Integer, _
                                   ByVal codOrigenProceso As String, _
                                   ByVal idUsuarioProceso As String, _
                                   ByVal numeroId As Long, _
                                   ByVal tipoProceso As String, _
                                   ByVal IdHebra As Integer, _
                                   ByVal LOG As Procesos.logEtapa, _
                                   ByVal FecAcreditacion As Date, _
                                   ByVal FecValorCuota As Date, _
                                   ByVal PerCuatrimestre As Date, _
                                   ByVal PerContable As Date, _
                                   ByVal PerContableSis As Date, _
                                   ByVal SeqProceso As Decimal, _
                                   ByVal ValMlCuotaDestinoA As Decimal, _
                                   ByVal ValMlCuotaDestinoB As Decimal, _
                                   ByVal ValMlCuotaDestinoC As Decimal, _
                                   ByVal ValMlCuotaDestinoD As Decimal, _
                                   ByVal ValMlCuotaDestinoE As Decimal, _
                                   ByVal PermiteAcreditacionParcial As Boolean) As Long

        Try
            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gtipoProceso = tipoProceso
            gfuncion = "AcredEgresoCTA"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()

            EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc,idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)
            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)
            gdbc.Rollback()

            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'Throw New SondaException(15307) '"No existen registros para acreditar
            Else
                gdbc.BeginTrans()
                'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
                'ValoresAcreditacion()
                gfecAcreditacion = FecAcreditacion
                gfecValorCuota = FecValorCuota
                gperCuatrimestre = PerCuatrimestre
                gperContable = PerContable
                gPerContableSis = PerContableSis
                gseqProceso = SeqProceso
                gvalMlCuotaDestinoA = ValMlCuotaDestinoA
                gvalMlCuotaDestinoB = ValMlCuotaDestinoB
                gvalMlCuotaDestinoC = ValMlCuotaDestinoC
                gvalMlCuotaDestinoD = ValMlCuotaDestinoD
                gvalMlCuotaDestinoE = ValMlCuotaDestinoE
                blPermiteAcreditacionParcial = PermiteAcreditacionParcial


                gdbc.Commit()

                If Not blPermiteAcreditacionParcial Then
                    gdbc.BeginTrans()
                End If

                For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1

                    rTrn = Nothing
                    rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                    If blPermiteAcreditacionParcial And rTrn.idCliente <> gIdClienteAnterior Then
                        gdbc.BeginTrans()

                    End If

                    LimpiarDatos()
                    LlenaValoresIniciales()

                    ValidarDatosBasicos()
                    If Not blAcreditarARezago And Not blIgnorar Then

                        gcausalRezago = LeerDatosCliente()

                        If Not blAcreditarARezago And Not blIgnorar Then
                            ValidarParaAcreditacion()
                        End If

                    End If

                    Select Case True

                        Case blIgnorar

                        Case blAcreditarARezago
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rTrn.codDestinoTransaccion = "REZ"
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rMovAcr.tipoMvto = "TEC"
                            AcreditaSaldoEgresoCTA()

                            'Case rMovAcr.tipoMvto = "TES"
                            '    AcreditaSaldoEgresoAPV()

                        Case Else
                            blIgnorar = True
                            rTrn.codError = 15308 'Tipo movimiento no fue reconocido
                            GenerarLog("A", 15308, "Hebra " & IdHebra & " - Tipo movimiento: " & rMovAcr.tipoMvto, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                    End Select

                    Select Case True

                        Case blIgnorar

                            IgnorarRegistroTrn()

                            'modificar solicitud egreso con error

                        Case Else
                            'modificar solicitud egreso con exito

                            ModificarRegistroTrn() 'aqui se actualizan los totales

                            If blPermiteAcreditacionParcial And ultimaTrnCliente() Then

                                blIgnorar = Not RegistroContable()

                                If blIgnorar Then
                                    IgnorarRegistroTrn() 'Rollback
                                Else
                                    gdbc.Commit()
                                    determinaEstadoError()
                                End If

                            End If

                            Select Case gtipoProceso
                                Case "SI"
                                    gTotRegistrosSimulados = gTotRegistrosSimulados + 1
                                Case "AC"
                                    gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
                            End Select


                    End Select

                    If (II + 1 Mod 100) = 0 Then
                        evento(LOG, "Hebra " & IdHebra & " - Se han procesado " & II + 1 & " registros ")
                    End If

                Next II

                evento(LOG, "Hebra " & IdHebra & " - Se procesaron " & II & " registros ")


                'AcredEgresoCTA = TotalesControlAcreditacion()


                'If gTotRegistrosIgnorados > 0 Then

                '    GenerarLog("A", 0, "Hebra " & IdHebra & " - Existen registros ignorados", 9, Nothing, Nothing)
                'End If

            End If

        Catch e As SondaException
            Dim mensaje As String
            gdbc.Rollback()
            AcredEgresoCTA = e.codigo
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim mensaje As String
            gdbc.Rollback()
            AcredEgresoCTA = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()

        End Try

    End Function

    Public Function AcredRevEgresoCTA(ByRef dbc As OraConn, _
                                      ByVal idAdm As Integer, _
                                       ByVal codOrigenProceso As String, _
                                       ByVal idUsuarioProceso As String, _
                                       ByVal numeroId As Long, _
                                       ByVal tipoProceso As String, _
                                       ByVal IdHebra As Integer, _
                                       ByVal LOG As Procesos.logEtapa, _
                                       ByVal FecAcreditacion As Date, _
                                       ByVal FecValorCuota As Date, _
                                       ByVal PerCuatrimestre As Date, _
                                       ByVal PerContable As Date, _
                                       ByVal PerContableSis As Date, _
                                       ByVal SeqProceso As Decimal, _
                                       ByVal ValMlCuotaDestinoA As Decimal, _
                                       ByVal ValMlCuotaDestinoB As Decimal, _
                                       ByVal ValMlCuotaDestinoC As Decimal, _
                                       ByVal ValMlCuotaDestinoD As Decimal, _
                                       ByVal ValMlCuotaDestinoE As Decimal, _
                                       ByVal PermiteAcreditacionParcial As Boolean) As Long

        Try
            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gtipoProceso = tipoProceso
            gfuncion = "AcredRevEgrCTA"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()

            EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc,idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)
            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)
            gdbc.Rollback()

            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'Throw New SondaException(15307) '"No existen registros para acreditar
            Else
                gdbc.BeginTrans()
                'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
                'ValoresAcreditacion()
                gfecAcreditacion = FecAcreditacion
                gfecValorCuota = FecValorCuota
                gperCuatrimestre = PerCuatrimestre
                gperContable = PerContable
                gPerContableSis = PerContableSis
                gseqProceso = SeqProceso
                gvalMlCuotaDestinoA = ValMlCuotaDestinoA
                gvalMlCuotaDestinoB = ValMlCuotaDestinoB
                gvalMlCuotaDestinoC = ValMlCuotaDestinoC
                gvalMlCuotaDestinoD = ValMlCuotaDestinoD
                gvalMlCuotaDestinoE = ValMlCuotaDestinoE
                blPermiteAcreditacionParcial = PermiteAcreditacionParcial

                gdbc.Commit()

                If Not blPermiteAcreditacionParcial Then
                    gdbc.BeginTrans()
                End If

                For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1

                    rTrn = Nothing
                    rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                    If blPermiteAcreditacionParcial And rTrn.idCliente <> gIdClienteAnterior Then
                        gdbc.BeginTrans()

                    End If

                    If rTrn.seqRegistro = 1225520467 Then
                        rTrn.seqRegistro = 1225520467
                    End If

                    If rTrn.seqRegistro = 1171123116 Then
                        rTrn.seqRegistro = 1171123116
                    End If

                    LimpiarDatos()
                    LlenaValoresIniciales()
                    ValidarDatosBasicos()

                    If Not blAcreditarARezago And Not blIgnorar Then
                        ReversarCierreProducto()
                        gcausalRezago = LeerDatosCliente()

                        If Not blAcreditarARezago And Not blIgnorar Then
                            ValidarParaAcreditacion()
                        End If

                    End If

                    Select Case True

                        Case blIgnorar

                        Case blAcreditarARezago
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rTrn.codDestinoTransaccion = "REZ"
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rMovAcr.tipoMvto = "RTEC"
                            AcreditaRevSaldoEgresoCTA()

                        Case Else
                            blIgnorar = True
                            rTrn.codError = 15308 'Tipo movimiento no fue reconocido
                            GenerarLog("A", 15308, "Hebra " & IdHebra & " - Tipo movimiento: " & rMovAcr.tipoMvto, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                    End Select

                    Select Case True

                        Case blIgnorar

                            IgnorarRegistroTrn()

                        Case Else
                            'modificar solicitud egreso con exito

                            ModificarRegistroTrn() 'aqui se actualizan los totales

                            If blPermiteAcreditacionParcial And ultimaTrnCliente() Then

                                blIgnorar = Not RegistroContable()

                                If blIgnorar Then
                                    IgnorarRegistroTrn() 'Rollback
                                Else
                                    gdbc.Commit()
                                    determinaEstadoError()
                                End If

                            End If

                            Select Case gtipoProceso
                                Case "SI"
                                    gTotRegistrosSimulados = gTotRegistrosSimulados + 1
                                Case "AC"
                                    gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
                            End Select


                    End Select

                Next II


                'AcredRevEgresoCTA = TotalesControlAcreditacion()


                'If gTotRegistrosIgnorados > 0 Then

                '    GenerarLog("A", 0, "Hebra " & IdHebra & " - Existen registros ignorados", 9, Nothing, Nothing)
                'End If

            End If

        Catch e As SondaException
            Dim mensaje As String
            gdbc.Rollback()
            AcredRevEgresoCTA = e.codigo
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim mensaje As String
            gdbc.Rollback()
            AcredRevEgresoCTA = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()

        End Try

    End Function


    Public Function AcredPagoPens(ByRef dbc As OraConn, _
                                  ByVal idAdm As Integer, _
                                  ByVal codOrigenProceso As String, _
                                  ByVal idUsuarioProceso As String, _
                                  ByVal numeroId As Long, _
                                  ByVal tipoProceso As String, _
                                  ByVal IdHebra As Integer, _
                                  ByVal LOG As Procesos.logEtapa, _
                                  ByVal FecAcreditacion As Date, _
                                  ByVal FecValorCuota As Date, _
                                  ByVal PerCuatrimestre As Date, _
                                  ByVal PerContable As Date, _
                                  ByVal PerContableSis As Date, _
                                  ByVal SeqProceso As Decimal, _
                                  ByVal ValMlCuotaDestinoA As Decimal, _
                                  ByVal ValMlCuotaDestinoB As Decimal, _
                                  ByVal ValMlCuotaDestinoC As Decimal, _
                                  ByVal ValMlCuotaDestinoD As Decimal, _
                                  ByVal ValMlCuotaDestinoE As Decimal, _
                                  ByVal PermiteAcreditacionParcial As Boolean) As Long

        '--:AP------------------->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>----------------------
        Dim clsConta2 As WS.IngresoEgresoConta.auxiliarContabilidad.Contabilidad2
        Dim gValMlMontoP As Decimal = 0
        '--<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<------------------------
        Try

            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gtipoProceso = tipoProceso
            gfuncion = "AcredPagoPens"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()

            'EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc,idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)
            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)
            gdbc.Rollback()

            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'Throw New SondaException(15307) '"No existen registros para acreditar
            Else
                gdbc.BeginTrans()
                'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
                'ValoresAcreditacion()
                gfecAcreditacion = FecAcreditacion
                gfecValorCuota = FecValorCuota
                gperCuatrimestre = PerCuatrimestre
                gperContable = PerContable
                gPerContableSis = PerContableSis
                gseqProceso = SeqProceso
                gvalMlCuotaDestinoA = ValMlCuotaDestinoA
                gvalMlCuotaDestinoB = ValMlCuotaDestinoB
                gvalMlCuotaDestinoC = ValMlCuotaDestinoC
                gvalMlCuotaDestinoD = ValMlCuotaDestinoD
                gvalMlCuotaDestinoE = ValMlCuotaDestinoE
                blPermiteAcreditacionParcial = PermiteAcreditacionParcial

                'If Not gExisteEncabezado Then
                '    CrearEncabezadoAcred()
                'End If
                gdbc.Commit()

                If Not blPermiteAcreditacionParcial Then
                    gdbc.BeginTrans()
                End If

                '--:AP------------------->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>-------------------------
                clsConta2 = New WS.IngresoEgresoConta.auxiliarContabilidad.Contabilidad2("BEN005")

                For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1

                    rTrn = Nothing
                    rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                    If blPermiteAcreditacionParcial And rTrn.idCliente <> gIdClienteAnterior Then
                        gdbc.BeginTrans()

                    End If

                    LimpiarDatos()
                    LlenaValoresIniciales()
                    ValidarDatosBasicos()

                    If Not blAcreditarARezago And Not blIgnorar Then

                        gcausalRezago = LeerDatosCliente()

                        If Not blAcreditarARezago And Not blIgnorar Then
                            ValidarParaAcreditacion()
                        End If

                    End If

                    Select Case True

                        Case blIgnorar

                        Case blAcreditarARezago
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rTrn.codDestinoTransaccion = "REZ"
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rMovAcr.tipoMvto = "PPE"
                            AcreditaPensiones()

                            '--:AP------->>>>>>>>>>>>>>>>>>>>>>>
                            gValMlMontoP += rTrn.valMlPatrFrecCal

                        Case rMovAcr.tipoMvto = "RPPE" And gcodAdministradora = 1033 'AP 05/02/2016 OS-6015235 Pagos Caducos.

                            AcreditaPensiones()

                        Case rMovAcr.tipoMvto = "IMP"
                            AcreditaImpuesto()

                        Case rMovAcr.tipoMvto = "NOC" And blNocional
                            ' AcreditaNocional()

                            'rSal.valMlSaldo = Mat.Redondear(rSal.valCuoSaldoNocional * rTrn.valMlValorCuota, 0)
                            'rSal.valCuoSaldo = rSal.valCuoSaldoNocional

                            If rTrn.valMlMvtoNoc + rTrn.valCuoMvtoNoc + rTrn.valMlComisionNoc + rTrn.valCuoComisionNoc = 0 Then
                                rTrn.valMlMvtoNoc = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste
                                rTrn.valCuoMvtoNoc = rTrn.valCuoMvto + rTrn.valCuoInteres + rTrn.valCuoReajuste

                                rTrn.valMlComisionNoc = rTrn.valMlComisPorcentual
                                rTrn.valCuoComisionNoc = rTrn.valCuoComisPorcentual

                                rTrn.valMlMvto = 0
                                rTrn.valMlInteres = 0
                                rTrn.valMlReajuste = 0

                                rTrn.valCuoMvto = 0
                                rTrn.valCuoInteres = 0
                                rTrn.valCuoReajuste = 0
                                rTrn.valMlComisPorcentual = 0
                                rTrn.valCuoComisPorcentual = 0

                            End If


                            AcreditaPensiones()

                        Case rTrn.numReferenciaOrigen1 = 1 And rTrn.codOrigenProceso = "PAGOPENS"
                            AcreditaPensiones()

                        Case Else
                            blIgnorar = True
                            rTrn.codError = 15308 'Tipo movimiento no fue reconocido
                            GenerarLog("A", 15308, "Hebra " & IdHebra & " - Tipo movimiento: " & rMovAcr.tipoMvto, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                    End Select

                    Select Case True

                        Case blIgnorar

                            IgnorarRegistroTrn()
                            'modificar pago pension con error

                        Case Else
                            'modificar pago pension con exito

                            ModificarRegistroTrn() 'aqui se actualizan los totales

                            If blPermiteAcreditacionParcial And ultimaTrnCliente() Then
                                'AP: AERR
                                '--:AP-------->>>>>>>>>>>>>>>>>>>>>>>>>>
                                If gcodAdministradora = 1032 Then
                                    blIgnorar = Not RegistroContable(clsConta2)
                                Else
                                    blIgnorar = Not RegistroContable()
                                End If
                                '----------<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


                                If blIgnorar Then
                                    IgnorarRegistroTrn() 'Rollback
                                Else
                                    gdbc.Commit()
                                    determinaEstadoError()
                                End If

                            End If

                            Select Case gtipoProceso
                                Case "SI"
                                    gTotRegistrosSimulados = gTotRegistrosSimulados + 1
                                Case "AC"
                                    gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
                            End Select


                    End Select

                Next II

                '--:AP------------------->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>-----
                If gcodAdministradora = 1032 Then contabilizarCollectionPagoPensiones(clsConta2, gValMlMontoP)
                '-----------------------------------------------<<<<<<<<<<<<-

                'AcredPagoPens = TotalesControlAcreditacion()

                'If gTotRegistrosIgnorados > 0 Then

                '    GenerarLog("A", 0, "Hebra " & IdHebra & " - Existen registros ignorados", 9, Nothing, Nothing)
                'End If

            End If

        Catch e As SondaException
            Dim mensaje As String
            gdbc.Rollback()
            AcredPagoPens = e.codigo
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim mensaje As String
            gdbc.Rollback()
            AcredPagoPens = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()

        End Try

    End Function



    Public Function AcredPagoOtrBen(ByRef dbc As OraConn, _
                                    ByVal idAdm As Integer, _
                                    ByVal codOrigenProceso As String, _
                                    ByVal idUsuarioProceso As String, _
                                    ByVal numeroId As Long, _
                                    ByVal tipoProceso As String, _
                                    ByVal IdHebra As Integer, _
                                    ByVal LOG As Procesos.logEtapa, _
                                    ByVal FecAcreditacion As Date, _
                                    ByVal FecValorCuota As Date, _
                                    ByVal PerCuatrimestre As Date, _
                                    ByVal PerContable As Date, _
                                    ByVal PerContableSis As Date, _
                                    ByVal SeqProceso As Decimal, _
                                    ByVal ValMlCuotaDestinoA As Decimal, _
                                    ByVal ValMlCuotaDestinoB As Decimal, _
                                    ByVal ValMlCuotaDestinoC As Decimal, _
                                    ByVal ValMlCuotaDestinoD As Decimal, _
                                    ByVal ValMlCuotaDestinoE As Decimal, _
                                    ByVal PermiteAcreditacionParcial As Boolean) As Long

        Try
            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gtipoProceso = tipoProceso
            gfuncion = "AcredPagoOtrBen"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()

            EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc,idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)
            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)
            gdbc.Rollback()

            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'Throw New SondaException(15307) '"No existen registros para acreditar
            Else
                gdbc.BeginTrans()
                'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
                'ValoresAcreditacion()
                gfecAcreditacion = FecAcreditacion
                gfecValorCuota = FecValorCuota
                gperCuatrimestre = PerCuatrimestre
                gperContable = PerContable
                gPerContableSis = PerContableSis
                gseqProceso = SeqProceso
                gvalMlCuotaDestinoA = ValMlCuotaDestinoA
                gvalMlCuotaDestinoB = ValMlCuotaDestinoB
                gvalMlCuotaDestinoC = ValMlCuotaDestinoC
                gvalMlCuotaDestinoD = ValMlCuotaDestinoD
                gvalMlCuotaDestinoE = ValMlCuotaDestinoE
                blPermiteAcreditacionParcial = PermiteAcreditacionParcial

                'If Not gExisteEncabezado Then
                '    CrearEncabezadoAcred()
                'End If
                gdbc.Commit()

                If Not blPermiteAcreditacionParcial Then
                    gdbc.BeginTrans()
                End If

                For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1

                    rTrn = Nothing
                    rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                    If blPermiteAcreditacionParcial And rTrn.idCliente <> gIdClienteAnterior Then
                        gdbc.BeginTrans()

                    End If

                    LimpiarDatos()
                    LlenaValoresIniciales()
                    ValidarDatosBasicos()

                    If Not blAcreditarARezago And Not blIgnorar Then

						gcausalRezago = LeerDatosCliente()

						If rTrn.numeroId = 478201 Then
							GenerarLog("A", 15309, "Hebra " & IdHebra & " Causal de rezagos : " & rTrn.codCausalRezagoCal & " - " & gcausalRezago & "=" & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
						End If

						If Not blAcreditarARezago And Not blIgnorar Then
							ValidarParaAcreditacion()
						End If

					End If

					Select Case True

						Case blIgnorar

						Case blAcreditarARezago
							blIgnorar = True
							rTrn.codError = 15309					  'Movimiento se rezaga
							GenerarLog("A", 15309, "Hebra " & IdHebra & " a) Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

						Case rTrn.codDestinoTransaccion = "REZ"
							blIgnorar = True
							rTrn.codError = 15309					  'Movimiento se rezaga
							GenerarLog("A", 15309, "Hebra " & IdHebra & " b)Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

						Case rMovAcr.tipoMvto = "PPE"
							AcreditaPensiones()

						Case rMovAcr.tipoMvto = "IMP"
							AcreditaImpuesto()

						Case rMovAcr.tipoMvto = "NOC" And blNocional						 'lfc:07-04-2021 - añade por enfermos terminales
							' AcreditaNocional()

							'rSal.valMlSaldo = Mat.Redondear(rSal.valCuoSaldoNocional * rTrn.valMlValorCuota, 0)
							'rSal.valCuoSaldo = rSal.valCuoSaldoNocional

							If rTrn.valMlMvtoNoc + rTrn.valCuoMvtoNoc + rTrn.valMlComisionNoc + rTrn.valCuoComisionNoc = 0 Then
								rTrn.valMlMvtoNoc = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste
								rTrn.valCuoMvtoNoc = rTrn.valCuoMvto + rTrn.valCuoInteres + rTrn.valCuoReajuste

								rTrn.valMlComisionNoc = rTrn.valMlComisPorcentual
								rTrn.valCuoComisionNoc = rTrn.valCuoComisPorcentual

								rTrn.valMlMvto = 0
								rTrn.valMlInteres = 0
								rTrn.valMlReajuste = 0

								rTrn.valCuoMvto = 0
								rTrn.valCuoInteres = 0
								rTrn.valCuoReajuste = 0
								rTrn.valMlComisPorcentual = 0
								rTrn.valCuoComisPorcentual = 0

							End If


							AcreditaPensiones()


						Case Else
							blIgnorar = True
							rTrn.codError = 15308							'Tipo movimiento no fue reconocido
							GenerarLog("A", 15308, "Hebra " & IdHebra & " - Tipo movimiento: " & rMovAcr.tipoMvto, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

					End Select

					Select Case True

						Case blIgnorar

							IgnorarRegistroTrn()
							'modificar pago otros beneficios con error

						Case Else
							'modificar pago otros beneficios con exito

							ModificarRegistroTrn()					  'aqui se actualizan los totales

							If blPermiteAcreditacionParcial And ultimaTrnCliente() Then

								blIgnorar = Not RegistroContable()

								If blIgnorar Then
									IgnorarRegistroTrn()							'Rollback
								Else
									gdbc.Commit()
									determinaEstadoError()
								End If

							End If

							Select Case gtipoProceso
								Case "SI"
									gTotRegistrosSimulados = gTotRegistrosSimulados + 1
								Case "AC"
									gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
							End Select


					End Select

				Next II


                'AcredPagoOtrBen = TotalesControlAcreditacion()

                'If gTotRegistrosIgnorados > 0 Then

                '    GenerarLog("A", 0, "Hebra " & IdHebra & " - Existen registros ignorados", 9, Nothing, Nothing)
                'End If

            End If

        Catch e As SondaException
            Dim mensaje As String
            gdbc.Rollback()
            AcredPagoOtrBen = e.codigo
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim mensaje As String
            gdbc.Rollback()
            AcredPagoOtrBen = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()

        End Try

    End Function



    Public Function AcredRecupRezagos(ByRef dbc As OraConn, _
                                      ByVal idAdm As Integer, _
                                      ByVal codOrigenProceso As String, _
                                      ByVal idUsuarioProceso As String, _
                                      ByVal numeroId As Long, _
                                      ByVal tipoProceso As String, _
                                      ByVal IdHebra As Integer, _
                                      ByVal LOG As Procesos.logEtapa, _
                                      ByVal FecAcreditacion As Date, _
                                      ByVal FecValorCuota As Date, _
                                      ByVal PerCuatrimestre As Date, _
                                      ByVal PerContable As Date, _
                                      ByVal PerContableSis As Date, _
                                      ByVal SeqProceso As Decimal, _
                                      ByVal ValMlCuotaDestinoA As Decimal, _
                                      ByVal ValMlCuotaDestinoB As Decimal, _
                                      ByVal ValMlCuotaDestinoC As Decimal, _
                                      ByVal ValMlCuotaDestinoD As Decimal, _
                                      ByVal ValMlCuotaDestinoE As Decimal, _
                                      ByVal PermiteAcreditacionParcial As Boolean) As Long

        Dim dsRtrn As New DataSet()
        Dim error_ape As Integer
        Dim seq_log As Integer

        Try
            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gtipoProceso = tipoProceso
            gfuncion = "AcredRecupREZ"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()


            '''':lfc 18-02-09
            ''''modifcar estado lote al inicio proceso
            'EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            ''MSC: Agregar este bloque de codigo en este mismo punto
            'If tipoProceso <> "AC" Then
            '    evento(LOG, "Hebra " & IdHebra & " - Se inicia proceso apertura de transacciones")

            '    ' apertura transacciones
            '    dsRtrn = Transacciones.aperturaTransaccion(gdbc,gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId, gtipoProceso)
            '    error_ape = dsRtrn.Tables(0).Rows(0).Item("verror_ape")
            '    seq_log = dsRtrn.Tables(0).Rows(0).Item("vseq_log")

            '    If error_ape <> 0 Then
            '        'Throw New SondaException(99999) 'Error fatal en apertura de transacciones

            '        blErrorFatal = True
            '        GenerarLog("E", 20509, Nothing, IdHebra, 0, Nothing, Nothing)
            '        evento(LOG, "Hebra " & IdHebra & " - Error fatal en apertura de transacciones")
            '        Throw New SondaException(20509) 'No existen registros para acreditar
            '    End If

            '    evento(LOG, "Hebra " & IdHebra & " - Proceso apertura de transacciones finalizado")

            '    gSeqLog = seq_log  ' se asigna variable obtenida de la llamada del procedimiento

            'End If
            ''FIN MSC

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc, idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)
            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)
            gdbc.Rollback()

            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'Throw New SondaException(15307) '"No existen registros para acreditar
            Else
                gdbc.BeginTrans()

                'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
                'ValoresAcreditacion()
                gfecAcreditacion = FecAcreditacion
                gfecValorCuota = FecValorCuota
                gperCuatrimestre = PerCuatrimestre
                gperContable = PerContable
                gPerContableSis = PerContableSis
                gseqProceso = SeqProceso
                gvalMlCuotaDestinoA = ValMlCuotaDestinoA
                gvalMlCuotaDestinoB = ValMlCuotaDestinoB
                gvalMlCuotaDestinoC = ValMlCuotaDestinoC
                gvalMlCuotaDestinoD = ValMlCuotaDestinoD
                gvalMlCuotaDestinoE = ValMlCuotaDestinoE
                blPermiteAcreditacionParcial = PermiteAcreditacionParcial

                EliminarIngresoRezagos()
                'If Not gExisteEncabezado Then
                '    CrearEncabezadoAcred()
                'End If
                gdbc.Commit()

                If Not blPermiteAcreditacionParcial Then
                    gdbc.BeginTrans()
                End If

                For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1


                    ValAjusteCom = 0

                    rTrn = Nothing
                    rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                    If blPermiteAcreditacionParcial And rTrn.idCliente <> gIdClienteAnterior Then
                        gdbc.BeginTrans()
                    End If

                    If rTrn.valCuoAjusteDecimalCal <> 0 And rTrn.tipoProducto = "CCO" Then
                        Dim a As New Integer()
                        a = 0
                    End If

                    'Verifica Exceso de Independiente
                    If rTrn.tipoEntidadPagadora = "V" And (rTrn.tipoPago = 2 Or rTrn.tipoPago = 3) And _
                       rTrn.tipoProducto = "CCO" And rTrn.valMlExcesoLinea > 0 Then 'Afil. Independiente Voluntario(V) y Atrasado(2) y Solo Obligatoria.
                        blExcesosIndep = True
                    Else
                        blExcesosIndep = False
                    End If

                    If rTrn.codMvtoAdi = "110358" And gcodAdministradora = 1032 Then 'Solo para Planvital se excluyen Adic. Antiguo.OS-7079391. 09/03/2015. OS-7243919 01/04/2016
                        blAdicionalAntiguo = True
                    Else
                        blAdicionalAntiguo = False
                    End If

                    'OS-7243919. PCI La siguiente linea es para no considerar Adicional Antiguo. Cuando se Apruebe 
                    '                se debe eliminar
                    blAdicionalAntiguo = False




                    If rTrn.seqRegistro = 50140903 Then
                        rTrn.seqRegistro = 50140903
                    End If

                    If rTrn.seqRegistro = 50140913 Then
                        rTrn.seqRegistro = 50140913
                    End If

                    g_valCuoAjusteDec = 0
                    g_valMlAjusteDec = 0


                    LimpiarDatos()
                    LlenaValoresIniciales()
                    ValorCuotaCaja()

                    If rTrn.codDestinoTransaccionCal = "REZ" Then
                        blAcreditarARezago = True
                    End If

                    ValidarDatosBasicos()

                    If Not blAcreditarARezago And Not blIgnorar Then

                        gcausalRezago = LeerDatosCliente()

                        If Not blAcreditarARezago And Not blIgnorar Then
                            ValidarParaAcreditacion()
                        End If

                    End If

                    ''CAVCAI
                    If rTrn.tipoProducto = "CAV" And rTrn.codDestinoTransaccionCal = "CTA" And IsNothing(Trim(rTrn.categoria)) Then
                        'Sin reg.Tributario, es decir sin Categoria. OS-4427354
                        rTrn.codCausalRezagoCal = "14"
                        blAcreditarARezago = True
                    End If

                    Select Case True

                        Case blIgnorar

                        Case blAcreditarARezago

                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga

                            'If Not rTrn.codCausalRezagoCal Is Nothing Then
                            '    dsAux = Parametro.traerGlobal(gdbc,"PAR_APA_CAUSALREZAGO", New Object() {gidAdm, rTrn.codCausalRezagoCal})
                            '    If dsAux.Tables(0).Rows.Count > 0 Then gDescripcionCausalRezago = IIf(IsDBNull(dsAux.Tables(0).Rows(0).Item("DESCRIPCION")), "SiNdESC", dsAux.Tables(0).Rows(0).Item("DESCRIPCION"))
                            'End If
                            descripcionCausalRez()
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rTrn.codDestinoTransaccion = "REZ"
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga

                            'If Not rTrn.codCausalRezagoCal Is Nothing Then
                            '    dsAux = Parametro.traerGlobal(gdbc,"PAR_APA_CAUSALREZAGO", New Object() {gidAdm, rTrn.codCausalRezagoCal})
                            '    If dsAux.Tables(0).Rows.Count > 0 Then gDescripcionCausalRezago = IIf(IsDBNull(dsAux.Tables(0).Rows(0).Item("DESCRIPCION")), "SiNdESC-2", dsAux.Tables(0).Rows(0).Item("DESCRIPCION"))
                            'End If
                            descripcionCausalRez()
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rMovAcr.tipoMvto = "DEC"
                            AcreditaDeclaraciones()

                        Case blExcesosIndep
                            AcreditaExcesosIndepend()


                        Case rMovAcr.indCotizacion = "S"
                            'AcreditaCotRecupRez()

                            If rTrn.perCotizacion >= New Date(2009, 7, 1).Date And (rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF") Then
                                'AcreditaCotRezNominal2()
                                AcreditaCotREC2()
                            Else
                                AcreditaCotRezNominal()
                            End If

                        Case rMovAcr.tipoMvto = "TIC"
                            AcreditaSaldoIngreso()


                        Case Else 'rMovAcr.tipoMvto <> "COT"
                            'If rTrn.perCotizacion >= New Date(2009, 7, 1).Date And (rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF") Then
                            '    AcreditaCotREC2()
                            'Else
                            AcreditaOtrosRecupRez()
                            'End If

                            'Case Else
                            'blIgnorar = True
                            'rTrn.codError = 15308 'Tipo movimiento no fue reconocido
                            'GenerarLog("A", 15308, "Tipo movimiento: " & rMovAcr.tipoMvto, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                    End Select

                    Select Case True

                        Case blIgnorar

                            IgnorarRegistroTrn()
                            DevolverRezagoAVigente() 'lfc: 14-03-2018 no desmarcar rezagos cuando se acredite.

                            'modificar rezago con error

                        Case Else
                            'modificar rezago con exito

                            ModificarRegistroTrn() 'aqui se actualizan los totales

                            If blPermiteAcreditacionParcial And ultimaTrnCliente() Then

                                blIgnorar = Not RegistroContable()

                                If blIgnorar Then
                                    IgnorarRegistroTrn() 'Rollback
                                    DevolverRezagoAVigente()
                                Else
                                    gdbc.Commit()
                                    determinaEstadoError()
                                End If

                            End If

                            Select Case gtipoProceso
                                Case "SI"
                                    gTotRegistrosSimulados = gTotRegistrosSimulados + 1
                                Case "AC"
                                    gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
                            End Select


                    End Select

                Next II


                'AcredRecupRezagos = TotalesControlAcreditacion()

            End If

        Catch e As SondaException
            Dim mensaje As String
            gdbc.Rollback()
            AcredRecupRezagos = e.codigo
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim mensaje As String
            gdbc.Rollback()
            AcredRecupRezagos = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            'dbc.Close()

        End Try

    End Function


    Public Function AcredAjuste(ByRef dbc As OraConn, _
                                ByVal idAdm As Integer, _
                                ByVal codOrigenProceso As String, _
                                ByVal idUsuarioProceso As String, _
                                ByVal numeroId As Long, _
                                ByVal tipoProceso As String, _
                                ByVal IdHebra As Integer, _
                                ByVal LOG As Procesos.logEtapa, _
                                ByVal usu As String, _
                                ByVal FecAcreditacion As Date, _
                                ByVal FecValorCuota As Date, _
                                ByVal PerCuatrimestre As Date, _
                                ByVal PerContable As Date, _
                                ByVal PerContableSis As Date, _
                                ByVal SeqProceso As Decimal, _
                                ByVal ValMlCuotaDestinoA As Decimal, _
                                ByVal ValMlCuotaDestinoB As Decimal, _
                                ByVal ValMlCuotaDestinoC As Decimal, _
                                ByVal ValMlCuotaDestinoD As Decimal, _
                                ByVal ValMlCuotaDestinoE As Decimal, _
                                ByVal PermiteAcreditacionParcial As Boolean) As Long

        Try
            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gtipoProceso = tipoProceso
            gfuncion = "AcredAjuste"
            gIdHebra = IdHebra

            '--.--
            If codOrigenProceso = "COMPECON" Then
                gfuncion = "AcredCompEco"

            ElseIf codOrigenProceso = "DEVTECEX" Then      ':lfc -tecnico extranjero
                gfuncion = "AcredDevTecEx"

            ElseIf codOrigenProceso = "AEXDVSTJ" Then      ':PCI - DEV. SUBSIDIO TRABAJADOR JOVEN
                gfuncion = "AcredDevSTJ"

            ElseIf codOrigenProceso = "COBPRIMA" Then      ':lfc -cargo prima sis caf
                gfuncion = "AcredCobSisCaf"

			ElseIf codOrigenProceso = "ACRTGRCO" Then      '<<-- lfc:// comision TGR - ca-4048436--
				gfuncion = "AcredComTGR"

			ElseIf codOrigenProceso = "ACRFREZZ" Then
				gfuncion = "AcredSalFrezz"				  '<<-- lfc:// ncg-264

			ElseIf codOrigenProceso = "ACRTOOBL" Then			 'lfc:RTO10%
				gfuncion = "AcredRTO10"

			ElseIf codOrigenProceso = "ACRTRAFC" Then			 'ltraspaso de saldo AFC
				gfuncion = "AcredTrfAFC"

			ElseIf codOrigenProceso = "ACRTOPRO" Then			 'Provision 2Retiro
				gfuncion = "AcredProvision"

			ElseIf codOrigenProceso = "BONO200K" Then			 'Bono 200mil
				gfuncion = "AcredBono200K"

			End If

			gUsuarioEjecProc = usu

			gdbc = dbc

			IniProceso()

			'EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

			gdbc.BeginTrans()
			dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc, idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)
			rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)
			gdbc.Rollback()

			If dsTrnCur.Tables(0).Rows.Count = 0 Then
				'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
				'no contengan registros a Procesar.

				'blErrorFatal = True
				'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
				'Throw New SondaException(15307) '"No existen registros para acreditar
			Else
				gdbc.BeginTrans()
				'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
				'ValoresAcreditacion()
				gfecAcreditacion = FecAcreditacion
				gfecValorCuota = FecValorCuota
				gperCuatrimestre = PerCuatrimestre
				gperContable = PerContable
				gPerContableSis = PerContableSis
				gseqProceso = SeqProceso
				gvalMlCuotaDestinoA = ValMlCuotaDestinoA
				gvalMlCuotaDestinoB = ValMlCuotaDestinoB
				gvalMlCuotaDestinoC = ValMlCuotaDestinoC
				gvalMlCuotaDestinoD = ValMlCuotaDestinoD
				gvalMlCuotaDestinoE = ValMlCuotaDestinoE
				blPermiteAcreditacionParcial = PermiteAcreditacionParcial

				'If Not gExisteEncabezado Then
				'    CrearEncabezadoAcred()
				'End If
				gdbc.Commit()

				If Not blPermiteAcreditacionParcial Then
					gdbc.BeginTrans()
				End If

				For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1

					rTrn = Nothing
					rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

					If blPermiteAcreditacionParcial And rTrn.idCliente <> gIdClienteAnterior Then
						gdbc.BeginTrans()
					End If

					If rTrn.seqRegistro = 1275156806 Then
						rTrn.seqRegistro = 1275156806
					End If

					If rTrn.seqRegistro = 1074529768 Then
						rTrn.seqRegistro = 1074529768
					End If

					If rTrn.seqRegistro = 1074529771 Then
						rTrn.seqRegistro = 1074529771
					End If

					LimpiarDatos()

					If (rTrn.tipoImputacion = "ABO" And rTrn.codDestinoTransaccion = "CTA") Or _
					   (rTrn.tipoImputacion = "ABO" And rTrn.codDestinoTransaccion = "REZ") Or _
					   (rTrn.tipoImputacion = "ABO" And rTrn.codDestinoTransaccion = "TRF") Or _
					   (rTrn.tipoImputacion = "CAR" And rTrn.codOrigenTransaccion = "CTA") Or _
					   (rTrn.tipoImputacion = "CAR" And rTrn.codOrigenTransaccion = "REZ") Or _
					   (rTrn.tipoImputacion = "CAR" And rTrn.codOrigenTransaccion = "TRF") Then


						LlenaValoresIniciales()


						'DSZ: 07-02-2017 - FECHA CUOTA Y VAL CUOTA SIN VALORES PARA AJUSTES OS:9356049
						If gtipoProceso <> "AC" And rTrn.codDestinoTransaccion = "CTA" And (rTrn.codOrigenProceso = "AJUMASIV" Or rTrn.codOrigenProceso = "AJUSELEC") Then
							If rTrn.fecValorCuota Is Nothing Then
								rTrn.fecValorCuota = FecValorCuota
								If rTrn.valMlValorCuota = 0 Or rTrn.valMlValorCuota Is Nothing Then
									Select Case rTrn.tipoFondoDestino
										Case "A" : rTrn.valMlValorCuota = ValMlCuotaDestinoA
										Case "B" : rTrn.valMlValorCuota = ValMlCuotaDestinoB
										Case "C" : rTrn.valMlValorCuota = ValMlCuotaDestinoC
										Case "D" : rTrn.valMlValorCuota = ValMlCuotaDestinoD
										Case "E" : rTrn.valMlValorCuota = ValMlCuotaDestinoE
										Case Else
									End Select
								End If
							End If
						End If


						ValidarDatosBasicos()


						If rTrn.codDestinoTransaccion = "TRF" Then
							DeterminarTransferencia()
						End If


						If (rTrn.tipoImputacion = "ABO" And rTrn.codDestinoTransaccion <> "REZ") Or _
						 (rTrn.tipoImputacion = "CAR" And rTrn.codOrigenTransaccion <> "REZ" And rTrn.codOrigenTransaccion <> "TRF") Then


							If Not blAcreditarARezago And Not blIgnorar Then

								gcausalRezago = LeerDatosCliente()

								If Not blAcreditarARezago And Not blIgnorar Then
									ValidarParaAcreditacion()
								End If

							End If

							If rTrn.codDestinoTransaccion = "CTA" And rTrn.codDestinoTransaccionCal = "REZ" Then
								blIgnorar = True
								rTrn.codError = 15309								  ' Movimiento se rezaga                               
								GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
							End If
						Else
							If rTrn.idCliente = 0 Then
								gIdClienteAnterior = -1
							Else
								'INI OS-7629132 05/08/2015. Reinicia Instancia cuando previamente hay CARGO de REZAGO.
								If gcodOrigenProceso = "AJUMASIV" Then
									If IsNothing(clsSal) Then
										clsSal = New INESaldo()
										clsSal.clear()
									End If
								End If
								'FIN OS-7629132 05/08/2015. Reinicia Instancia cuando previamente hay CARGO de REZAGO.

								gIdClienteAnterior = rTrn.idCliente

							End If
						End If


						'crear funcion que asigne los valores cuotas a la transaccion.
						'--cargar valor cuota
						'--asignar valor a la trs

						Select Case True

							Case blIgnorar

								'    ':lfc -tecnico extranjero
								'Case codOrigenProceso = "DEVTECEX"
								'    AcreditaOtrosMovs()

							Case blAcreditarARezago
								rTrn.codError = 15309								  ' Movimiento se rezaga
								GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
								blIgnorar = True

								'lfc:// comision TGR - ca-4048436-->>>
							Case codOrigenProceso = "ACRTGRCO"

								If rTrn.codOrigenTransaccion <> "CTA" Or Not rTrn.codCausalRezagoCal Is Nothing Then
									rTrn.codError = 15309									 ' Movimiento se rezaga
									GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
									blIgnorar = True
								Else
									AcreditaComision()

								End If
								''--<<<---lfc:// comision TGR - ca-4048436--

							Case rTrn.codOrigenProceso = "ACRFREZZ"							' ncg-264


							Case rTrn.codOrigenProceso = "ACRTOOBL"							'lfc:RTO10%

								AcreditaCotAjuste()

							Case rTrn.codOrigenProceso = "ACRTOPRO"							'lfc:2retiro - provision
								AcreditaCotAjuste()

							Case rTrn.codOrigenProceso = "BONO200K"							'bono 200 mil
								AcreditaCotAjuste()

							Case rTrn.codDestinoTransaccion = "TRF" And rTrn.tipoImputacion = "ABO"
								AcreditaTransferencia()

							Case rTrn.codOrigenTransaccion = "TRF" And rTrn.tipoImputacion = "CAR"
								AcreditaTransferenciaCargo()

							Case rTrn.codDestinoTransaccion = "REZ" And rTrn.tipoImputacion = "ABO"
								AcreditaRezagoAjuste()

							Case rTrn.codOrigenTransaccion = "REZ" And rTrn.tipoImputacion = "CAR"
								AcreditaRezagoAjuste()

							Case rMovAcr.indCotizacion = "S" And codOrigenProceso <> "ACRTGRCO"
								If rTrn.numReferenciaOrigen6 = 20080701 And rTrn.perCotizacion >= New Date(2009, 7, 1).Date And (rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF") Then
									AcreditaCotAjusteSIS()
								Else
									AcreditaCotAjuste()
								End If

							Case rMovAcr.tipoMvto = "RCOT"
								AcreditaCotCargo()

							Case rMovAcr.tipoMvto = "DEC"
								AcreditaDeclaraciones()

							Case rMovAcr.tipoMvto = "COM" And codOrigenProceso <> "ACRTGRCO"
								AcreditaComision()

							Case rMovAcr.tipoMvto = "RCOM" And codOrigenProceso <> "ACRTGRCO"
								AcreditaComision()

							Case codOrigenProceso = "COBPRIMA"							 'lfc:// cargo prima sis caf
								AcreditaCargoSIS()


							Case rMovAcr.tipoMvto = "PRI"
								AcreditaPrimas()

							Case rMovAcr.tipoMvto = "RPRI"
								AcreditaPrimas()

							Case Else
								AcreditaOtrosMovs()

						End Select

					Else					  'transacciones de cuentas especiales ADM, RND, FDO, etc.

						AcreditaAjustesEspeciales()

					End If

					Select Case True

						Case blIgnorar

							IgnorarRegistroTrn()
							'modificar ajuste con error
							'Case ultimaTrnCliente()

							'modificar ajuste con error

						Case Else
							acreditaAjusteDecimalInformado()
							'modificar ajuste con exito
							If ultimaTrnCliente() Then

								If CuentaSobregirada() Then
									IgnorarRegistroTrn()
								End If

							End If

							If Not blIgnorar Then

								ModificarRegistroTrn()								  'aqui se actualizan los totales

								':lfc -tecnico extranjero
								If codOrigenProceso = "DEVTECEX" And gtipoProceso = "AC" Then
									Sys.TecnicoExtranjero.sysTecnicoExtranjero.modificaEstadoAcreditacion(gdbc, idAdm, rTrn.numReferenciaOrigen1, rTrn.numReferenciaOrigen2, gidUsuarioProceso, gfuncion)
								End If

								If blPermiteAcreditacionParcial And ultimaTrnCliente() Then

									blIgnorar = Not RegistroContable()

									If blIgnorar Then
										IgnorarRegistroTrn()										'Rollback
									Else
										gdbc.Commit()
										determinaEstadoError()
									End If

								End If

								Select Case gtipoProceso
									Case "SI"
										gTotRegistrosSimulados = gTotRegistrosSimulados + 1
									Case "AC"
										gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
								End Select
							End If

					End Select

				Next II

				'':lfc -tecnico extranjero
				'If gtipoProceso = "AC" And codOrigenProceso <> "DEVTECEX" Then
				'    Sys.sysAjustes.Ajustes.actualizarAjusteAcred(gdbc,gidAdm, gnumeroId, gidUsuarioProceso, gfuncion)
				'End If

				'AcredAjuste = TotalesControlAcreditacion()

				'If gTotRegistrosIgnorados > 0 Then

				'    GenerarLog("A", 0, "Hebra " & IdHebra & " - Existen registros ignorados", 9, Nothing, Nothing)
				'End If

			End If

		Catch e As SondaException
            Dim mensaje As String
            gdbc.Rollback()
            AcredAjuste = e.codigo
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim mensaje As String
            gdbc.Rollback()
            AcredAjuste = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()

        End Try

    End Function



    Public Function AcredCambioFondo(ByRef dbc As OraConn, _
                                     ByVal idAdm As Integer, _
                                     ByVal codOrigenProceso As String, _
                                     ByVal idUsuarioProceso As String, _
                                     ByVal numeroId As Long, _
                                     ByVal tipoProceso As String, _
                                     ByVal IdHebra As Integer, _
                                     ByVal LOG As Procesos.logEtapa, _
                                     ByVal FecAcreditacion As Date, _
                                     ByVal FecValorCuota As Date, _
                                     ByVal PerCuatrimestre As Date, _
                                     ByVal PerContable As Date, _
                                     ByVal PerContableSis As Date, _
                                     ByVal SeqProceso As Decimal, _
                                     ByVal ValMlCuotaDestinoA As Decimal, _
                                     ByVal ValMlCuotaDestinoB As Decimal, _
                                     ByVal ValMlCuotaDestinoC As Decimal, _
                                     ByVal ValMlCuotaDestinoD As Decimal, _
                                     ByVal ValMlCuotaDestinoE As Decimal, _
                                     ByVal PermiteAcreditacionParcial As Boolean) As Long

        Try
            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gtipoProceso = tipoProceso
            gfuncion = "AcredCambioFondo"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()

            EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc, idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)
            'dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc, idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, 2)
            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)
            gdbc.Rollback()

            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'Throw New SondaException(15307) '"No existen registros para acreditar
            Else
                gdbc.BeginTrans()
                'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
                'ValoresAcreditacion()
                gfecAcreditacion = FecAcreditacion
                gfecValorCuota = FecValorCuota
                gperCuatrimestre = PerCuatrimestre
                gperContable = PerContable
                gPerContableSis = PerContableSis
                gseqProceso = SeqProceso
                gvalMlCuotaDestinoA = ValMlCuotaDestinoA
                gvalMlCuotaDestinoB = ValMlCuotaDestinoB
                gvalMlCuotaDestinoC = ValMlCuotaDestinoC
                gvalMlCuotaDestinoD = ValMlCuotaDestinoD
                gvalMlCuotaDestinoE = ValMlCuotaDestinoE
                blPermiteAcreditacionParcial = PermiteAcreditacionParcial

                'If Not gExisteEncabezado Then
                '    CrearEncabezadoAcred()
                'End If
                gdbc.Commit()

                If Not blPermiteAcreditacionParcial Then
                    gdbc.BeginTrans()
                End If

                For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1


                    rTrn = Nothing
                    rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                    If blPermiteAcreditacionParcial And rTrn.idCliente <> gIdClienteAnterior Then
                        gdbc.BeginTrans()

                    End If


                    LimpiarDatos()
                    LlenaValoresIniciales()


                    'Se mueve fecha acreditacion a fecha operacion para que no rezague por causal 1D
                    rTrn.fecOperacion = gfecAcreditacion

                    ValidarDatosBasicos()
                    LeerMovCambioFondo()

                    If Not blAcreditarARezago And Not blIgnorar Then

                        gcausalRezago = LeerDatosCliente()

                        If Not blAcreditarARezago And Not blIgnorar Then
                            ValidarParaAcreditacion()
                        End If

                    End If

                    Select Case True

                        Case blIgnorar

                        Case blAcreditarARezago
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rTrn.codDestinoTransaccion = "REZ"
                            blIgnorar = True
                            rTrn.codError = 15309 'Movimiento se rezaga
                            GenerarLog("A", 15309, "Hebra " & IdHebra & " - Causal: " & rTrn.codCausalRezagoCal & " - " & gDescripcionCausalRezago, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                        Case rMovAcr.tipoMvto = "TIF"
                            AcreditaCambioFondoAbono()

                        Case rMovAcr.tipoMvto = "TEF"
                            AcreditaCambioFondoCargo()

                        Case rMovAcr.tipoMvto = "COM"
                            'Solicitado por Manuel Avalos Para soporte de Comision Adm. Saldo.
                            AcreditaComision()

                        Case Else
                            blIgnorar = True
                            rTrn.codError = 15308 'Tipo movimiento no fue reconocido
                            GenerarLog("A", 15308, "Hebra " & IdHebra & " - Tipo movimiento: " & Trim(rMovAcr.tipoMvto), IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)


                    End Select


                    Select Case True

                        Case blIgnorar

                            IgnorarRegistroTrn()
                            IgnorarCambioFondo()


                        Case Else
                            'modificar ajuste con exito si los cargos del cliente son iguales a los abonos (en pesos)
                            AplicaCambioFondo()

                            If blIgnorar Then

                                IgnorarRegistroTrn()
                                IgnorarCambioFondo()

                            Else

                                ModificarRegistroTrn() 'aqui se actualizan los totales

                                If blPermiteAcreditacionParcial And ultimaTrnCliente() Then

                                    ValidaCambioFondo() 'valida: totalCargos = totalAbonos

                                    If Not blIgnorar Then
                                        RegistroContable()
                                    End If

                                    If blIgnorar Then

                                        IgnorarRegistroTrn() 'Rollback
                                        IgnorarCambioFondo()

									Else

										'--lfc: 03/07/2020 - 7361685 -- actualizar sol de retiros --->>>>---
										If gtipoProceso = "AC" And Me.gcodAdministradora = 1033 Then
											Try
												'GenerarLog("I", 0, "Inicia Act.Sol.Retiros", IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
												sysRefExternas.retiros.actualizaRetirosCdf(dbc, gidAdm, rTrn.idCliente, gnumeroId, gcodOrigenProceso, gidUsuarioProceso, gfuncion)
												'GenerarLog("I", 0, "Fin Act.Sol.Retiros", IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
											Catch ex As Exception
												'	GenerarLog("I", 0, "Error Act.Sol.Retiros:" & ex.Message, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
											End Try
											'---<<<<-----------------
										End If

										gdbc.Commit()
										determinaEstadoError()
									End If

								End If

                                Select Case gtipoProceso
                                    Case "SI"
                                        gTotRegistrosSimulados = gTotRegistrosSimulados + 1
                                    Case "AC"
                                        gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
                                End Select

                            End If


                    End Select

                    If ((II + 1) Mod 1000) = 0 Then
                        evento(LOG, "Hebra " & IdHebra & " - Se han procesado " & II + 1 & " registros")
                    End If

                Next II

                evento(LOG, "Hebra " & IdHebra & " - Se procesaron " & II & " registros")

                If Not blPermiteAcreditacionParcial Then

                    ValidaCambioFondo()

                End If

                'AcredCambioFondo = TotalesControlAcreditacion()

                'If gTotRegistrosIgnorados > 0 Then

                '    GenerarLog("A", 0, "Hebra " & IdHebra & " - Existen registros ignorados", gTotRegistrosIgnorados, Nothing, Nothing)
                'End If


                'LFC:23/03/2009 - ACTUALIZACION SOLO PARA ACRED


            End If

        Catch e As SondaException
            Dim mensaje As String
            gdbc.Rollback()
            AcredCambioFondo = e.codigo
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim mensaje As String
            gdbc.Rollback()
            AcredCambioFondo = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()

        End Try

    End Function

    Public Function AcredExTGR(ByRef dbc As OraConn, _
                                  ByVal idAdm As Integer, _
                                  ByVal codOrigenProceso As String, _
                                  ByVal idUsuarioProceso As String, _
                                  ByVal numeroId As Long, _
                                  ByVal tipoProceso As String, _
                                  ByVal IdHebra As Integer, _
                                  ByVal LOG As Procesos.logEtapa, _
                                  ByVal FecAcreditacion As Date, _
                                  ByVal FecValorCuota As Date, _
                                  ByVal PerCuatrimestre As Date, _
                                  ByVal PerContable As Date, _
                                  ByVal PerContableSis As Date, _
                                  ByVal SeqProceso As Decimal, _
                                  ByVal ValMlCuotaDestinoA As Decimal, _
                                  ByVal ValMlCuotaDestinoB As Decimal, _
                                  ByVal ValMlCuotaDestinoC As Decimal, _
                                  ByVal ValMlCuotaDestinoD As Decimal, _
                                  ByVal ValMlCuotaDestinoE As Decimal, _
                                  ByVal PermiteAcreditacionParcial As Boolean) As Long


        Dim iniciarConexion As Boolean = True
        Dim dsRtrn As New DataSet()
        Dim error_ape As Integer
        Dim seq_log As Integer

        Try

            gidAdm = idAdm
            gcodOrigenProceso = codOrigenProceso
            gidUsuarioProceso = idUsuarioProceso
            gnumeroId = numeroId
            gseqProceso = 0
            gtipoProceso = tipoProceso
            gfuncion = "AcredRecaudac"
            gIdHebra = IdHebra

            gdbc = dbc

            IniProceso()

            'EstadoAcreditacion(DeterminaEstadoAcreditacion(gtipoProceso))

            ''MSC: Agregar este bloque de codigo en este mismo punto
            'If tipoProceso <> "AC" Then
            '    evento(LOG, "Hebra " & IdHebra & " - Se inicia proceso apertura de transacciones")

            '    ' apertura transacciones
            '    dsRtrn = Transacciones.aperturaTransaccion(gdbc,gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId, gtipoProceso)
            '    error_ape = dsRtrn.Tables(0).Rows(0).Item("verror_ape")
            '    seq_log = dsRtrn.Tables(0).Rows(0).Item("vseq_log")

            '    If error_ape <> 0 Then
            '        blErrorFatal = True
            '        GenerarLog("E", 99999, Nothing, IdHebra, 0, Nothing, Nothing)
            '        evento(LOG, "Hebra " & IdHebra & " - Error fatal en apertura de transacciones")
            '        Throw New SondaException(99999) 'No existen registros para acreditar
            '    End If

            '    evento(LOG, "Hebra " & IdHebra & " - Proceso apertura de transacciones finalizado")

            '    gSeqLog = seq_log  ' se asigna variable obtenida de la llamada del procedimiento

            'End If
            ''FIN MSC 

            evento(LOG, "Hebra " & IdHebra & " - Se inicia el proceso de acreditacion")

            gdbc.BeginTrans()
            dsTrnCur = Transacciones.buscarConEstadoHeb(gdbc, idAdm, gcodOrigenProceso, idUsuarioProceso, numeroId, gtipoProceso, IdHebra)

            rTrn = New ccTransacciones(dsTrnCur.Tables(0).NewRow)
            gdbc.Rollback()


            If dsTrnCur.Tables(0).Rows.Count = 0 Then
                'Se deja en Comentario, ya que para los reprocesos es posible que algunas hebras
                'no contengan registros a Procesar.

                'blErrorFatal = True
                'GenerarLog("E", 15307, Nothing, IdHebra, 0, Nothing, Nothing)
                'evento(LOG, "Hebra " & IdHebra & " - No se encontraron registros para acreditar")
                'Throw New SondaException(15307) 'No existen registros para acreditar

            Else
                'dbc.BeginTrans()
                'Comentario por procesamiento en HEBRAS. Se definen variables pasadas por Parametros
                'ValoresAcreditacion()
                gfecAcreditacion = FecAcreditacion
                gfecValorCuota = FecValorCuota
                gperCuatrimestre = PerCuatrimestre
                gperContable = PerContable
                gPerContableSis = PerContableSis
                gseqProceso = SeqProceso
                gvalMlCuotaDestinoA = ValMlCuotaDestinoA
                gvalMlCuotaDestinoB = ValMlCuotaDestinoB
                gvalMlCuotaDestinoC = ValMlCuotaDestinoC
                gvalMlCuotaDestinoD = ValMlCuotaDestinoD
                gvalMlCuotaDestinoE = ValMlCuotaDestinoE
                blPermiteAcreditacionParcial = PermiteAcreditacionParcial

                'dbc.Commit()

                If Not blPermiteAcreditacionParcial Then
                    gdbc.BeginTrans()
                End If

                For II = 0 To dsTrnCur.Tables(0).Rows.Count - 1

                    commitParcial = commitParcial Or (II + 1) Mod 1000 = 0

                    rTrn = Nothing
                    rTrn = New ccTransacciones(dsTrnCur.Tables(0).Rows(II))

                    If rTrn.seqRegistro = 26145328 Then
                        rTrn.seqRegistro = 26145328
                    End If

                    If rTrn.seqRegistro = 1165948570 Then
                        rTrn.seqRegistro = 1165948570
                    End If

                    If rTrn.seqRegistro = 1165948564 Then
                        rTrn.seqRegistro = 1165948564
                    End If

                    If blPermiteAcreditacionParcial And iniciarConexion Then 'rTrn.idCliente <> gIdClienteAnterior Then
                        gdbc.BeginTrans()
                        iniciarConexion = False
                    End If

                    If rTrn.tipoEntidadPagadora = "V" And (rTrn.tipoPago = 2 Or rTrn.tipoPago = 3) And _
                       rTrn.tipoProducto = "CCO" And rTrn.valMlExcesoLinea > 0 Then 'Afil. Independiente Voluntario(V) y Atrasado(2) y Solo Obligatoria.
                        blExcesosIndep = True
                    Else
                        blExcesosIndep = False
                    End If

                    LimpiarDatos()
                    LlenaValoresIniciales()

                    'MSC: Estas lineas se comentan                                
                    'ValorCuotaCaja()
                    'CalcularPatrimonioFechaOperacion()
                    'fin MSC
                    DeterminaMontoInstitucionSalud()

                    g_valCuoAjusteDec = 0
                    g_valMlAjusteDec = 0

                    '--.--If (rTrn.tipoProducto = "CCV" Or rTrn.tipoProducto = "CDC") And rTrn.codDestinoTransaccion <> "REZ" Then
                    If rTrn.codDestinoTransaccion <> "REZ" Then

                        DeterminarTransferencia()

                        If rTrn.valMlTransferenciaCal > 0 And rTrn.codDestinoTransaccion <> "TRF" Then
                            AcreditaTransferencia() 'viene cotizacion y transferencia
                        End If

                    End If

                    If rTrn.codDestinoTransaccionCal = "REZ" Then
                        blAcreditarARezago = True
                    End If

                    ValidarDatosBasicos()

                    If rTrn.codDestinoTransaccion <> "REZ" Then

                        '-->>>lfc:19/05 --se crean cuentas CCV saldo=0 y regTrib null
                        If Not blAcreditarARezago And Not blIgnorar Then

                            gcausalRezago = LeerDatosCliente()

                            If Not blAcreditarARezago And Not blIgnorar Then
                                ValidarParaAcreditacion()
                            End If
                        End If
                    End If

                    ''CAVCAI
                    If rTrn.tipoProducto = "CAV" And rTrn.codDestinoTransaccionCal = "CTA" And IsNothing(Trim(rTrn.categoria)) Then
                        'Sin reg.Tributario, es decir sin Categoria. OS-4427354
                        rTrn.codCausalRezagoCal = "14"
                        blAcreditarARezago = True
                    End If

                    Select Case True
                        Case blIgnorar

                        Case rTrn.codDestinoTransaccion = "TRF"
                            AcreditaTransferencia() 'vien solo transferencia sin cotizacion

                        Case blAcreditarARezago
                            descripcionCausalRez()
                            AcreditaRezago()

                        Case rTrn.codDestinoTransaccion = "REZ"
                            blAcreditarARezago = True
                            AcreditaRezago()

                        Case rMovAcr.tipoMvto = "DEC"
                            AcreditaDeclaraciones()

                        Case blExcesosIndep
                            AcreditaExcesosIndepend()

                        Case rMovAcr.indCotizacion = "S"

                            If rTrn.perCotizacion >= New Date(2009, 7, 1).Date And (rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF") Then
                                AcreditaCotREC5()   '-----------------------PRIMA SIS---------------------
                            Else
                                AcreditaCotREC4()
                            End If


                        Case rMovAcr.tipoMvto = "PER"

                        Case Else
                            blIgnorar = True
                            rTrn.codError = 15308 'Tipo movimiento no fue reconocido
                            GenerarLog("A", 15308, "Hebra " & IdHebra & " - Tipo movimiento: " & rMovAcr.tipoMvto, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                    End Select

                    ValidaFondoDestinoInicial()

                    Select Case True
                        Case blIgnorar
                            IgnorarRegistroTrn()

                        Case Else
                            'actualizacion aux comisiones
                            ModificarRegistroTrn() 'aqui se actualizan los totales

                            If blPermiteAcreditacionParcial And ultimaTrnCliente() Then

                                If commitParcial Then

                                    commitParcial = False
                                    iniciarConexion = True
                                    blIgnorar = Not RegistroContable()

                                    If blIgnorar Then
                                        IgnorarRegistroTrn() 'Rollback
                                    Else
                                        gdbc.Commit()
                                        determinaEstadoError()

                                    End If

                                    GenerarLog("I", 0, "Hebra " & IdHebra & " - Se han procesado " & (II + 1).ToString & " de " & dsTrnCur.Tables(0).Rows.Count & " transacciones", IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

                                    'forzarGrarbageCollector()

                                End If
                            End If

                            Select Case gtipoProceso
                                Case "SI"
                                    gTotRegistrosSimulados = gTotRegistrosSimulados + 1
                                Case "AC"
                                    gTotRegistrosAcreditados = gTotRegistrosAcreditados + 1
                            End Select


                    End Select

                    '--.--lfc:09/04/09 llamada va en el cierre diario
                    'debe ir comentado desde SIS--------------------DON
                    ''''''If gtipoProceso = "AC" And Not blIgnorar Then

                    ''''''    Sys.Cobranzas.Pago.acreditarEnCobranza(gdbc,gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId, rTrn.seqRegistro, gidUsuarioProceso, gfuncion)

                    ''''''End If

                Next II


                'AcredRecaudac = TotalesControlAcreditacion() ' MARCA LA TRANSACCION COMO ACREDITADA


                'If gTotRegistrosIgnorados > 0 Then
                '    GenerarLog("A", 0, "Hebra " & IdHebra & " - Existen registros ignorados", 9, Nothing, Nothing)
                'Else
                '    If gtipoProceso = "AC" Then
                'Lotes.modEstado(gdbc,gidAdm, rTrn.numReferenciaOrigen5, Nothing, "T", gidUsuarioProceso, gfuncion)
                '    End If
                'End If


            End If

        Catch e As SondaException
            Dim mensaje As String
            If Not IsNothing(gdbc) Then gdbc.Rollback()
            AcredExTGR = 15310
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - " & e.codigo & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)

        Catch e As Exception
            Dim mensaje As String
            If Not IsNothing(gdbc) Then gdbc.Rollback()
            AcredExTGR = 15310 ' Error en acreditacion
            EstadoAcreditacion(gEstadoError)
            mensaje = "Hebra " & IdHebra & " - Error en la Acreditacion : " & e.ToString
            GenerarLog("E", 15310, mensaje, IdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            Dim sm As New SondaExceptionManager(e)
        Finally
            CerrarLog()
            If Not gEnLinea Then gdbc.Close()
        End Try

    End Function


    Private Sub AcreditaRezago() 'ABONOS A REZAGO

        'Verifica que sea Convenio
        If rTrn.folioConvenio > 0 And rTrn.numCuotasPactadas > 0 And rTrn.numCuotasPagadas > 0 Then
            gEsConvenio = True
        End If

        rTrn.codDestinoTransaccionCal = "REZ"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()


        If IsNothing(rTrn.codCausalRezagoCal) Then
            rTrn.codCausalRezagoCal = rTrn.codCausalRezago
        End If

        gRegistrosCalculados = gRegistrosCalculados + 1
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        rTrn.codAjusteMovimiento = "CUO"

        rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal

        If rTrn.codOrigenProceso = "TRAINREZ" Or rTrn.codOrigenProceso = "TRAINRZC" Or rTrn.codOrigenProceso = "TRAIPAGN" Then
            rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto
            rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
        Else
            rTrn.valCuoPatrFrecActCal = Mat.Redondear(rTrn.valMlPatrFrecActCal / rTrn.valMlValorCuota, 2)
        End If

        rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal

        'rTrn.valMlMvtoCal = rTrn.valMlMvto + rTrn.valMlExcesoLinea
        rTrn.valMlMvtoCal = rTrn.valMlMvto + rTrn.valMlExcesoLinea + rTrn.valMlExcesoEmpl

        rTrn.valMlInteresCal = rTrn.valMlInteres
        rTrn.valMlReajusteCal = rTrn.valMlReajuste
        rTrn.valMlAdicionalCal = rTrn.valMlAdicional
        rTrn.valMlAdicionalInteresCal = rTrn.valMlAdicionalInteres
        rTrn.valMlAdicionalReajusteCal = rTrn.valMlAdicionalReajuste


        'SIS//
        rTrn.valMlPrimaSisCal = rTrn.valMlPrimaSis
        rTrn.valMlPrimaSisInteresCal = rTrn.valMlPrimaSisInteres
        rTrn.valMlPrimaSisReajusteCal = rTrn.valMlPrimaSisReajuste


        'SIS// no se debe calcular ajuste decimal
        '''If rTrn.tipoProducto = "CCO" And rMovAcr.indCotizacion = "S" Then
        ''AjusteDecimalAbonosRezagos(rTrn.valMlValorCuotaCaja)
        '''End If
        'rTrn.valCuoMvtoCal = rTrn.valCuoPatrFdesCal - rTrn.valCuoTransferenciaCal - rTrn.valCuoAjusteDecimalCal

        rTrn.valCuoMvtoCal = rTrn.valCuoPatrFdesCal - rTrn.valCuoTransferenciaCal

        TrnARez()
        CrearRezagos()

        SumarTotales()

        'CrearControlRecaudacionREC()

    End Sub

    Private Sub AcreditaRezagoIngreso() 'ABONOS A REZAGO

        rTrn.codDestinoTransaccionCal = "REZ"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()


        If IsNothing(rTrn.codCausalRezagoCal) Then
            rTrn.codCausalRezagoCal = rTrn.codCausalRezago
        End If

        gRegistrosCalculados = gRegistrosCalculados + 1
        'CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)
        rTrn.valMlValorCuota = rTrn.valMlValorCuotaCaja
        rTrn.fecValorCuota = rTrn.fecValorCuotaCaja

        rTrn.codAjusteMovimiento = "CUO"


        rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste + _
                                rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste + _
                                rTrn.valMlExcesoLinea

        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto + rTrn.valCuoInteres + rTrn.valCuoReajuste + _
                                 rTrn.valCuoAdicional + rTrn.valCuoAdicionalInteres + rTrn.valCuoAdicionalReajuste + _
                                 rTrn.valCuoExcesoLinea

        'If blRentabilidadRez Then
        '    rTrn.valMlPatrFrecCal += rTrn.valMlAporteAdm
        '    rTrn.valCuoPatrFrecCal += rTrn.valCuoAporteAdm
        'End If


        rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
        rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal

        rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal



        rTrn.valMlMvtoCal = rTrn.valMlMvto + rTrn.valMlExcesoLinea
        rTrn.valMlInteresCal = rTrn.valMlInteres
        rTrn.valMlReajusteCal = rTrn.valMlReajuste
        rTrn.valMlAdicionalCal = rTrn.valMlAdicional
        rTrn.valMlAdicionalInteresCal = rTrn.valMlAdicionalInteres
        rTrn.valMlAdicionalReajusteCal = rTrn.valMlAdicionalReajuste

        'SIS// no se debe calcular ajuste decimal
        ''''If rTrn.tipoProducto = "CCO" And rMovAcr.indCotizacion = "S" Then
        ''''    AjusteDecimalAbonosRezagos(rTrn.valMlValorCuotaCaja)
        ''''End If

        rTrn.valCuoMvtoCal = rTrn.valCuoPatrFdesCal - rTrn.valCuoTransferenciaCal

        'SIS//
        rTrn.valMlPrimaSisCal = rTrn.valMlPrimaSis
        rTrn.valMlPrimaSisInteresCal = rTrn.valMlPrimaSisInteres
        rTrn.valMlPrimaSisReajusteCal = rTrn.valMlPrimaSisReajuste


        TrnARez()
        CrearRezagos()

        SumarTotales()

        'CrearControlRecaudacionREC()

    End Sub

    Private Sub AcreditaRezagoIngreso2() 'ABONOS A REZAGO

        rTrn.codDestinoTransaccionCal = "REZ"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()

        If IsNothing(rTrn.codCausalRezagoCal) Then rTrn.codCausalRezagoCal = rTrn.codCausalRezago

        gRegistrosCalculados = gRegistrosCalculados + 1
        rTrn.valMlValorCuota = rTrn.valMlValorCuotaCaja
        rTrn.fecValorCuota = rTrn.fecValorCuotaCaja

        rTrn.codAjusteMovimiento = "CUO"

        rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste + _
                        rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste + _
                        rTrn.valMlPrimaSis + rTrn.valMlPrimaSisInteres + rTrn.valMlPrimaSisReajuste + _
                        rTrn.valMlExcesoLinea

            rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto + rTrn.valCuoInteres + rTrn.valCuoReajuste + _
                                     rTrn.valCuoAdicional + rTrn.valCuoAdicionalInteres + rTrn.valCuoAdicionalReajuste + _
                                     rTrn.valCuoPrimaSis + rTrn.valCuoPrimaSisInteres + rTrn.valCuoPrimaSisReajuste + _
                                     rTrn.valCuoExcesoLinea

        'If blRentabilidadRez Then
        '    rTrn.valMlPatrFrecCal += rTrn.valMlAporteAdm
        '    rTrn.valCuoPatrFrecCal += rTrn.valCuoAporteAdm
        'End If


        rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
        rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal

        rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal

        rTrn.valMlMvtoCal = rTrn.valMlMvto + rTrn.valMlExcesoLinea + rTrn.valMlAporteAdm

        rTrn.valMlInteresCal = rTrn.valMlInteres
        rTrn.valMlReajusteCal = rTrn.valMlReajuste
        rTrn.valMlAdicionalCal = rTrn.valMlAdicional
        rTrn.valMlAdicionalInteresCal = rTrn.valMlAdicionalInteres
        rTrn.valMlAdicionalReajusteCal = rTrn.valMlAdicionalReajuste

        rTrn.valCuoMvtoCal = rTrn.valCuoPatrFdesCal - rTrn.valCuoTransferenciaCal

        'SIS//
        rTrn.valMlPrimaSisCal = rTrn.valMlPrimaSis
        rTrn.valMlPrimaSisInteresCal = rTrn.valMlPrimaSisInteres
        rTrn.valMlPrimaSisReajusteCal = rTrn.valMlPrimaSisReajuste


        TrnARez()
        CrearRezagos()

        SumarTotales()
    End Sub



    Private Sub AcreditaRezagoAjuste()


        rTrn.codAjusteMovimiento = "CUO"

        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino() 'PCI-05/08/2016 OS:9090452


        'LFC: 27/07/2010--. OS_3068291 (pli)
        'rTrn.valMlPatrFrecCal = rTrn.valMlMvto
        'rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto
        rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste + _
                                rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste + _
                                rTrn.valMlPrimaSis + rTrn.valMlPrimaSisInteres + rTrn.valMlPrimaSisReajuste + _
                                rTrn.valMlExcesoLinea

        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto + rTrn.valCuoInteres + rTrn.valCuoReajuste + _
                                 rTrn.valCuoAdicional + rTrn.valCuoAdicionalInteres + rTrn.valCuoAdicionalReajuste + _
                                 rTrn.valCuoPrimaSis + rTrn.valCuoPrimaSisInteres + rTrn.valCuoPrimaSisReajuste + _
                                 rTrn.valCuoExcesoLinea



        rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
        rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal


        rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal

        ' rTrn.valMlMvtoCal = rTrn.valMlMvto
        rTrn.valMlMvtoCal = rTrn.valMlMvto + rTrn.valMlExcesoLinea

        'PCI REGULARIZA SITUACION SOLICITADA POR MODELO
        'rTrn.valMlExcesoLinea = 0
        'rTrn.valCuoExcesoLinea = 0

        rTrn.valMlInteresCal = rTrn.valMlInteres
        rTrn.valMlReajusteCal = rTrn.valMlReajuste
        rTrn.valMlAdicionalCal = rTrn.valMlAdicional
        rTrn.valMlAdicionalInteresCal = rTrn.valMlAdicionalInteres
        rTrn.valMlAdicionalReajusteCal = rTrn.valMlAdicionalReajuste

        'SIS//
        rTrn.valMlPrimaSisCal = rTrn.valMlPrimaSis
        rTrn.valMlPrimaSisInteresCal = rTrn.valMlPrimaSisInteres
        rTrn.valMlPrimaSisReajusteCal = rTrn.valMlPrimaSisReajuste


        'rTrn.valCuoMvtoCal = rTrn.valCuoMvto
        rTrn.valCuoMvtoCal = rTrn.valCuoPatrFrecCal

        If rTrn.tipoImputacion = "ABO" Then
            rTrn.codDestinoTransaccionCal = "REZ"
            rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
            If IsNothing(rTrn.codCausalRezagoCal) Then
                rTrn.codCausalRezagoCal = rTrn.codCausalRezago
            End If
            TrnARez()
            CrearRezagos()
        Else
            RezagoAHistorico()
        End If

        SumarTotales()

    End Sub

    Private Sub AcreditaBono()

        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
        'CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        rTrn.valMlPatrFrecCal = rTrn.valMlMvto
        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto
        gRegistrosEnviados = gRegistrosEnviados + 1

        rTrn.valMlPatrFrecActCal = rTrn.valMlMvto
        rTrn.valCuoPatrFrecActCal = Mat.Redondear(rTrn.valMlMvto / rTrn.valMlValorCuota, 2)

        rTrn.valMlPatrFdesCal = rTrn.valMlMvto
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
        gRegistrosCalculados = gRegistrosCalculados + 1

        rTrn.valMlMvtoCal = rTrn.valMlMvto
        rTrn.valCuoMvtoCal = rTrn.valCuoPatrFdesCal

        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()

        'AplicaVector()
        CalcularSaldo()
        If rTrn.tipoImputacion = "CAR" And rTrn.tipoProducto = "CAI" Then
            CerrarSaldo()
        End If


    End Sub
    Private Sub AcreditaCollect()

        rTrn.codDestinoTransaccionCal = "CTA"

        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        rTrn.valMlPatrFrecCal = rTrn.valMlMvto
        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto
        gRegistrosEnviados = gRegistrosEnviados + 1

        rTrn.valMlPatrFrecActCal = rTrn.valMlMvto
        rTrn.valCuoPatrFrecActCal = Mat.Redondear(rTrn.valMlMvto / rTrn.valMlValorCuota, 2)

        rTrn.valMlPatrFdesCal = rTrn.valMlMvto
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
        gRegistrosCalculados = gRegistrosCalculados + 1

        rTrn.valMlMvtoCal = rTrn.valMlMvto
        rTrn.valCuoMvtoCal = rTrn.valCuoPatrFdesCal

        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()

        CalcularSaldo()



    End Sub

    Private Sub AcreditaRetiro()

        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
        'CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlComisFija + rTrn.valMlComisPorcentual
        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto + rTrn.valCuoComisFija + rTrn.valCuoComisPorcentual
        gRegistrosEnviados = gRegistrosEnviados + 1

        rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
        rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal

        rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
        gRegistrosCalculados = gRegistrosCalculados + 1

        rTrn.valMlMvtoCal = rTrn.valMlMvto
        rTrn.valCuoMvtoCal = rTrn.valCuoMvto

        rTrn.valMlComisFijaCal = rTrn.valMlComisFija
        rTrn.valCuoComisFijaCal = rTrn.valCuoComisFija

        rTrn.valMlComisPorcentualCal = rTrn.valMlComisPorcentual
        rTrn.valCuoComisPorcentualCal = rTrn.valCuoComisPorcentual

        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()
        'AplicaVector()
        CalcularSaldo()
        If rTrn.codOrigenProceso = "RETCAIFO" Then
            CerrarSaldo()
        End If





    End Sub
    Private Sub AcreditaAjuste()
        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        rTrn.valMlPatrFrecCal = rTrn.valMlMvto
        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto
        gRegistrosEnviados = gRegistrosEnviados + 1

        rTrn.valMlPatrFrecActCal = rTrn.valMlMvto
        rTrn.valCuoPatrFrecActCal = Mat.Redondear(rTrn.valMlMvto / rTrn.valMlValorCuota, 2)

        rTrn.valMlPatrFdesCal = rTrn.valMlMvto
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
        gRegistrosCalculados = gRegistrosCalculados + 1

        rTrn.valMlMvtoCal = rTrn.valMlMvto
        rTrn.valCuoMvtoCal = rTrn.valCuoPatrFdesCal

        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()

        'AplicaVector()
        CalcularSaldo()


    End Sub
    Private Sub AcreditaCotMandato()
        Dim valMlTotNominal As Decimal
        Dim valCuoTotNominal As Decimal

        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        rTrn.valMlPatrFrecCal = rTrn.valMlMvto
        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto
        gRegistrosEnviados = gRegistrosEnviados + 1

        'rTrn.valMlPatrFrecActCal = rTrn.valMlMvto
        'rTrn.valCuoPatrFrecActCal = Mat.Redondear(rTrn.valMlMvto / rTrn.valMlValorCuota, 2)

        rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste + _
                                rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste + _
                                rTrn.valMlExcesoLinea

        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto + rTrn.valCuoInteres + rTrn.valCuoReajuste + _
                                rTrn.valCuoAdicional + rTrn.valCuoAdicionalInteres + rTrn.valCuoAdicionalReajuste + _
                                rTrn.valCuoExcesoLinea

        rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
        rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal

        rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal

        gRegistrosCalculados = gRegistrosCalculados + 1

        'nuevo

        valMlTotNominal = rTrn.valMlPatrFrecCal
        valCuoTotNominal = rTrn.valCuoPatrFrecCal


        CalcularNominalesValorizado(valMlTotNominal, _
                                    valCuoTotNominal, _
                                    rTrn.valMlMvto, _
                                    rTrn.valMlInteres, _
                                    rTrn.valMlReajuste, _
                                    rTrn.valMlAdicional, _
                                    rTrn.valMlAdicionalInteres, _
                                    rTrn.valMlAdicionalReajuste, _
                                    rTrn.valMlExcesoLinea)



        TraerControlRentas()
        CalcularExcesos()
        CalcularComisionPorcentual()
        DeterminaMontoInstitucionSalud()

        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()

        CrearSaldosMovimientos()
        realizarAbonosExcesos()
        CalcularComisionFija()
        'ActualizaCotTrabajosPesados()
        'AplicaVector()
        CalcularSaldo()


        '--OS:9075964 - nuevas validaciones PLV
        If blGenExcesoEnLinea And gcodAdministradora = 1032 Then
            CrearControlRenta()
        End If

        ' ActualizaRentas()

    End Sub
    Private Sub AcreditaImpuesto()

        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
        'CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlComisFija + rTrn.valMlComisPorcentual
        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto + rTrn.valCuoComisFija + rTrn.valCuoComisPorcentual
        gRegistrosEnviados = gRegistrosEnviados + 1

        rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
        rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal

        rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
        gRegistrosCalculados = gRegistrosCalculados + 1

        rTrn.valMlMvtoCal = rTrn.valMlMvto
        rTrn.valCuoMvtoCal = rTrn.valCuoMvto


        gvalMlImpuestoCal = rTrn.valMlMvtoCal
        gvalCuoImpuestoCal = rTrn.valCuoMvtoCal

        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()

        'AplicaVector()
        CalcularSaldo()


    End Sub


    Private Sub AcreditaDevExcesos()

        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()

        '--.-- modificar valores informados//CA-2009010118
        If gtipoProceso <> "AC" Then
            rTrn.fecAcreditacion = gfecAcreditacion
            rTrn.fecValorCuota = gfecValorCuota
            rTrn.perContable = gperContable
        End If

        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        '--.-- calcular el monto cuotas, con nueva val_ml_valor_cuota//CA-2009010118
        If gtipoProceso <> "AC" Then
            rTrn.numReferenciaOrigen6 = rTrn.valCuoMvto * 100 'guardar el valor informado
            rTrn.valCuoMvto = Mat.Redondear(rTrn.valMlMvto / rTrn.valMlValorCuota, 2)
        End If


        If rTrn.codMvto = "111851" Or rTrn.codMvto = "111852" Then
            'Reversas de Primas y Comision por Excesos
            If rTrn.codMvto = "111851" Then
                AcreditaComision()
            ElseIf rTrn.codMvto = "111852" Then
                AcreditaPrimas()
            End If

        Else
            ' Reversa de Cotizacion por Excesos
            rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlComisFija + rTrn.valMlComisPorcentual
            rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto + rTrn.valCuoComisFija + rTrn.valCuoComisPorcentual
            gRegistrosEnviados = gRegistrosEnviados + 1

            rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
            rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal

            rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
            rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
            gRegistrosCalculados = gRegistrosCalculados + 1

            rTrn.valMlMvtoCal = rTrn.valMlMvto
            rTrn.valCuoMvtoCal = rTrn.valCuoMvto

            rTrn.valMlComisFijaCal = rTrn.valMlComisFija
            rTrn.valCuoComisFijaCal = rTrn.valCuoComisFija

            rTrn.valMlComisPorcentualCal = rTrn.valMlComisPorcentual
            rTrn.valCuoComisPorcentualCal = rTrn.valCuoComisPorcentual

            SumarTotales()
            TrnAMov()
            CrearSaldosMovimientos()

            'AplicaVector()
            CalcularSaldo()

        End If





    End Sub


    Private Sub AcreditaCargoSIS()

        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        rTrn.valMlPatrFrecCal = rTrn.valMlMvto
        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto
        gRegistrosEnviados = gRegistrosEnviados + 1

        rTrn.valMlPatrFrecActCal = rTrn.valMlMvto
        rTrn.valCuoPatrFrecActCal = rTrn.valCuoMvto

        rTrn.tipoComisionPorcentual = rMovAcr.tipoComisionPorcentual

        rTrn.valMlPatrFdesCal = rTrn.valMlMvto
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
        gRegistrosCalculados = gRegistrosCalculados + 1

        rTrn.valMlMvtoCal = rTrn.valMlMvto
        rTrn.valCuoMvtoCal = rTrn.valCuoPatrFdesCal

        If (rTrn.valCuoMvtoCal + rTrn.valMlMvtoCal) > 0 Then
            rTrn.valIdInstPagoPrimCal = gcodAdministradora
            rPri.codInstFinanciera = gcodAdministradora
            rPri.codOrigenProceso = rTrn.codOrigenProceso
            rPri.fecOperacion = rTrn.fecAcreditacion
            rPri.idAdmCobroAdicional = gcodAdministradora
            rPri.idEmpleador = rTrn.idEmpleador
            rPri.idPersona = rTrn.idPersona
            rPri.indDerechoSeguro = "S"
            rPri.perCotiza = rTrn.perCotizacion
            rPri.perProceso = rTrn.perContable
            rPri.porcPrimaSeguro = Mat.Redondear(rTrn.tasaPrima * 100, 2)
            rPri.seqMovimiento = rTrn.seqRegistro
            rPri.tipoFondo = rTrn.tipoFondoDestinoCal
            rPri.tipoPago = rTrn.tipoPago
            rPri.tipoTrabajador = rTrn.tipoCliente
            rPri.valCuoCco = rTrn.valCuoMvtoCal
            rPri.valMlAdicional = 0
            rPri.valMlAdicionalInteres = 0
            rPri.valMlAdicionalReajuste = 0
            rPri.valMlCco = rTrn.valMlMvtoCal
            rPri.valMlComisionFija = 0
            rPri.valMlPrimaSeguro = rTrn.valMlMvtoCal
            rPri.valMlRentaImponible = rTrn.valMlRentaImponibleSis

            rPri.porcAdicional = 0 'Mat.Redondear(rTrn.tasaAdicional * 100, 2)

            'SIS//
            rPri.sexo = rTrn.sexo
            rPri.fecAcreditacion = rTrn.fecAcreditacion
            rPri.valMlPrimaInteres = 0
            rPri.valCuoPrimaInteres = 0
            rPri.valCuoPrimaReajuste = 0
            rPri.valMlPrimaReajuste = 0

            If rTrn.codOrigenProceso = "COBPRIMA" And rPri.codMvto Is Nothing Then
                'No esta definiendo Cod Mvto en la tabla (ACR_PRIMAS_CIAS_SEG)
                rPri.codMvto = rTrn.codMvto
            End If

            If gtipoProceso = "AC" Then
                PrimasCiasSeguro.crear(gdbc, gidAdm, rTrn.tipoFondoDestinoCal, rTrn.perContable, rPri.codInstFinanciera, rTrn.idPersona, rTrn.seqRegistro, "ABO", rTrn.perCotizacion, rTrn.tipoCliente, "S", rTrn.codOrigenProceso, rTrn.fecOperacion, rPri.tipoPago, rTrn.valMlMvtoCal, rTrn.valCuoMvtoCal, rTrn.valMlComisFija, rTrn.valMlRentaImponible, rPri.valMlAdicional, rPri.valMlAdicionalInteres, rPri.valMlAdicionalReajuste, rPri.valMlPrimaSeguro, rPri.idAdmCobroAdicional, rPri.codMvto, rPri.porcPrimaSeguro, rPri.porcAdicional, rTrn.idEmpleador, gidUsuarioProceso, gfuncion, rPri.sexo, rPri.fecAcreditacion, rPri.valMlPrimaInteres, rPri.valMlPrimaReajuste, rPri.valCuoPrimaSeguro, rPri.valCuoPrimaInteres, rPri.valCuoPrimaReajuste)
            End If

            TrnAMov()
            CrearSaldosMovimientos()

            'AplicaVector()
            CalcularSaldo()

        Else
            GenerarLog("A", 15343, Nothing, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
        End If


    End Sub




    Private Sub AcreditaComision()

        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        rTrn.valMlPatrFrecCal = rTrn.valMlMvto
        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto
        gRegistrosEnviados = gRegistrosEnviados + 1

        rTrn.valMlPatrFrecActCal = rTrn.valMlMvto
        rTrn.valCuoPatrFrecActCal = rTrn.valCuoMvto

        rTrn.tipoComisionPorcentual = rMovAcr.tipoComisionPorcentual

        rTrn.valMlPatrFdesCal = rTrn.valMlMvto
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
        gRegistrosCalculados = gRegistrosCalculados + 1

        rTrn.valMlMvtoCal = rTrn.valMlMvto
        rTrn.valCuoMvtoCal = rTrn.valCuoPatrFdesCal

        If (rTrn.valMlMvtoCal + rTrn.valCuoMvtoCal) > 0 Then

            If rTrn.codOrigenProceso = "ACRTGRCO" Then

               	'gAdicionalSeTransfiere = determinarTransferenciaAdicional()
                If rTrn.codMvto = "120703" Then
                    gAdicionalSeTransfiere = True
                Else
                    gAdicionalSeTransfiere = False
                End If


                If Not gAdicionalSeTransfiere Then

                    If rTrn.codMvto = "120703" Or rTrn.codMvto = "123106" Then
                        rTrn.tipoComisionPorcentual = "PRE1"
                        rTrn.codMvto = "123106"
                    End If
                Else

                    If rTrn.codMvto = "120703" Or rTrn.codMvto = "123106" Then
                        rTrn.tipoComisionPorcentual = "COMD"
                        rTrn.codMvto = "120703"
                    End If

                    If gtipoProceso = "AC" Then
                        PrimasCiasSeguro.crear(gdbc, _
                                                    gidAdm, _
                                                    rTrn.tipoFondoDestinoCal, _
                                                    rTrn.perContable, _
                                                    rTrn.idInstDestino, _
                                                    rTrn.idPersona, _
                                                    rTrn.seqRegistro, _
                                                    "ABO", _
                                                    rTrn.perCotizacion, _
                                                    rTrn.tipoCliente, _
                                                    "S", _
                                                    rTrn.codOrigenProceso, _
                                                    rTrn.fecOperacion, _
                                                    "4", _
                                                    rTrn.valMlMvtoCal, _
                                                    rTrn.valCuoMvtoCal, _
                                                    rTrn.valMlComisFija, _
                                                    rTrn.valMlRentaImponible, _
                                                    rTrn.valMlMvtoCal, _
                                                    0, _
                                                    0, _
                                                    0, _
                                                    rTrn.idInstDestino, _
                                                    "120703", _
                                                    0, _
                                                    Mat.Redondear(rTrn.tasaAdicional * 100, 2), _
                                                    rTrn.idEmpleador, _
                                                    gidUsuarioProceso, _
                                                    gfuncion, _
                                                    rTrn.sexo, _
                                                    rTrn.fecAcreditacion, _
                                                    0, _
                                                    0, _
                                                    0, _
                                                    0, _
                                                    0)
                    End If '--fin ac
                    End If '--fin se transfiere
            End If '--fin proceso cobro de comision tgr

            SumarTotalesComision()
            TrnAMov()
            CrearSaldosMovimientos()

            'AplicaVector()
            CalcularSaldo()

        Else
            GenerarLog("A", 15343, Nothing, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
        End If


    End Sub
    Private Sub AcreditaRevComision()

        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()

        '--.-- modificar valores informados//CA-2009010118
        If gtipoProceso <> "AC" Then
            rTrn.fecAcreditacion = gfecAcreditacion
            rTrn.fecValorCuota = gfecValorCuota
            rTrn.perContable = gperContable
        End If

        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        '--.-- calcular el monto cuotas, con nueva val_ml_valor_cuota//CA-2009010118
        If gtipoProceso <> "AC" Then
            rTrn.numReferenciaOrigen6 = rTrn.valCuoMvto * 100 'guardar el valor informado
            rTrn.valCuoMvto = Mat.Redondear(rTrn.valMlMvto / rTrn.valMlValorCuota, 2)
        End If

        rTrn.valMlPatrFrecCal = rTrn.valMlMvto
        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto
        gRegistrosEnviados = gRegistrosEnviados + 1

        rTrn.valMlPatrFrecActCal = rTrn.valMlMvto

        rTrn.valCuoPatrFrecActCal = rTrn.valCuoMvto

        rTrn.valMlPatrFdesCal = rTrn.valMlMvto
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
        gRegistrosCalculados = gRegistrosCalculados + 1

        rTrn.valMlMvtoCal = rTrn.valMlMvto
        rTrn.valCuoMvtoCal = rTrn.valCuoPatrFdesCal

        If (rTrn.valMlMvtoCal + rTrn.valCuoMvtoCal) > 0 Then
            SumarTotalesComision()
            TrnAMov()
            CrearSaldosMovimientos()

            'AplicaVector()
            CalcularSaldo()

        Else
            GenerarLog("A", 15343, Nothing, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
        End If


    End Sub

    Private Sub AcreditaRevPrima()
        Dim iImputacion As String

        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()

        '--.-- modificar valores informados//CA-2009010118
        If gtipoProceso <> "AC" Then
            rTrn.fecAcreditacion = gfecAcreditacion
            rTrn.fecValorCuota = gfecValorCuota
            rTrn.perContable = gperContable
        End If

        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        '--.-- calcular el monto cuotas, con nueva val_ml_valor_cuota//CA-2009010118
        If gtipoProceso <> "AC" Then
            rTrn.numReferenciaOrigen6 = rTrn.valCuoMvto * 100 'guardar el valor informado
            rTrn.valCuoMvto = Mat.Redondear(rTrn.valMlMvto / rTrn.valMlValorCuota, 2)
        End If

        rTrn.valMlPatrFrecCal = rTrn.valMlMvto
        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto
        gRegistrosEnviados = gRegistrosEnviados + 1

        rTrn.valMlPatrFrecActCal = rTrn.valMlMvto

        rTrn.valCuoPatrFrecActCal = rTrn.valCuoMvto

        rTrn.valMlPatrFdesCal = rTrn.valMlMvto
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
        gRegistrosCalculados = gRegistrosCalculados + 1

        rTrn.valMlMvtoCal = rTrn.valMlMvto
        rTrn.valCuoMvtoCal = rTrn.valCuoPatrFdesCal




        'Ini PCI 13/01/2012

        If rTrn.valCuoMvtoCal > 0 Or rTrn.valMlMvtoCal > 0 Then

            rTrn.valIdInstPagoPrimCal = gcodAdministradora
            rPri.codInstFinanciera = gcodAdministradora

            rPri.codOrigenProceso = rTrn.codOrigenProceso
            rPri.fecOperacion = rTrn.fecAcreditacion
            rPri.idAdmCobroAdicional = gcodAdministradora
            rPri.idEmpleador = rTrn.idEmpleador
            rPri.idPersona = rTrn.idPersona
            rPri.indDerechoSeguro = "S"
            rPri.perCotiza = rTrn.perCotizacion
            rPri.perProceso = rTrn.perContable
            rPri.porcPrimaSeguro = 0
            rPri.seqMovimiento = rTrn.seqRegistro
            rPri.tipoFondo = rTrn.tipoFondoDestinoCal
            rPri.tipoPago = rTrn.tipoPago
            rPri.tipoTrabajador = rTrn.tipoCliente
            rPri.valCuoCco = rTrn.valCuoMvtoCal
            rPri.valMlAdicional = 0
            rPri.valMlAdicionalInteres = 0
            rPri.valMlAdicionalReajuste = 0
            rPri.valMlCco = rTrn.valMlMvtoCal
            rPri.valMlComisionFija = 0
            rPri.valMlPrimaSeguro = rTrn.valMlMvtoCal
            rPri.valMlRentaImponible = rTrn.valMlRentaImponibleSis


            rPri.porcAdicional = 0 'Mat.Redondear(rTrn.tasaAdicional * 100, 2)

            'SIS//
            rPri.sexo = rTrn.sexo
            rPri.fecAcreditacion = rTrn.fecAcreditacion
            rPri.valMlPrimaInteres = 0
            rPri.valCuoPrimaInteres = 0
            rPri.valCuoPrimaReajuste = 0
            rPri.valMlPrimaReajuste = 0

            'Dev. Exc. Empl. por codigo 110806

            If (rTrn.tipoProducto = "CAF" Or rTrn.tipoProducto = "CCO") And _
            (rMovAcr.tipoMvto = "PRI" Or rMovAcr.tipoMvto = "RPRI") And _
            (gcodOrigenProceso = "AJUMASIV" Or gcodOrigenProceso = "AJUSELEC" Or _
             gcodOrigenProceso = "DEVEXCAF" Or gcodOrigenProceso = "DEVEXCEM") Then
                If rMovAcr.tipoMvto = "PRI" Then iImputacion = "ABO"
                If rMovAcr.tipoMvto = "RPRI" Then iImputacion = "CAR"

                If gtipoProceso = "AC" Then
                    PrimasCiasSeguro.crear(gdbc, gidAdm, rTrn.tipoFondoDestinoCal, rTrn.perContable, _
                       rPri.codInstFinanciera, _
                       rTrn.idPersona, rTrn.seqRegistro, _
                       iImputacion, _
                       rTrn.perCotizacion, rTrn.tipoCliente, "S", _
                       rTrn.codOrigenProceso, rTrn.fecOperacion, _
                       rPri.tipoPago, _
                       rTrn.valMlMvtoCal, rTrn.valCuoMvtoCal, rTrn.valMlComisFija, rTrn.valMlRentaImponible, _
                       rPri.valMlAdicional, _
                       rPri.valMlAdicionalInteres, _
                       rPri.valMlAdicionalReajuste, _
                       rPri.valMlPrimaSeguro, _
                       rPri.idAdmCobroAdicional, _
                       rPri.codMvto, _
                       rPri.porcPrimaSeguro, _
                       rPri.porcAdicional, _
                       rTrn.idEmpleador, gidUsuarioProceso, gfuncion, _
                       rPri.sexo, _
                       rPri.fecAcreditacion, _
                       rPri.valMlPrimaInteres, _
                       rPri.valMlPrimaReajuste, _
                       rPri.valCuoPrimaSeguro, _
                       rPri.valCuoPrimaInteres, _
                       rPri.valCuoPrimaReajuste)

                End If
            End If
            TrnAMov()
            CrearSaldosMovimientos()

            'AplicaVector()
            CalcularSaldo()
        Else
            GenerarLog("A", 15343, Nothing, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
        End If

        'Fin PCI 13/01/2012


        ' ORIGINAL 13/01/2012
        '
        'If rTrn.valCuoMvtoCal > 0 Then
        '    'SumarTotalesComision()
        '    TrnAMov()
        '    CrearSaldosMovimientos()

        '    'AplicaVector()
        '    CalcularSaldo()

        'Else
        '    GenerarLog("A", 15343, Nothing, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
        'End If


    End Sub


    'lfc:// 02-11-2009 CA-2009090317 
    '''''Private Sub AcreditaPrima()

    '''''    rTrn.codDestinoTransaccionCal = "CTA"
    '''''    rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
    '''''    CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

    '''''    rTrn.valMlPatrFrecCal = rTrn.valMlMvto
    '''''    rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto
    '''''    gRegistrosEnviados = gRegistrosEnviados + 1

    '''''    rTrn.valMlPatrFrecActCal = rTrn.valMlMvto
    '''''    rTrn.valCuoPatrFrecActCal = rTrn.valCuoMvto

    '''''    'rTrn.tipoComisionPorcentual = rMovAcr.tipoComisionPorcentual

    '''''    rTrn.valMlPatrFdesCal = rTrn.valMlMvto
    '''''    rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
    '''''    gRegistrosCalculados = gRegistrosCalculados + 1

    '''''    rTrn.valMlMvtoCal = rTrn.valMlMvto
    '''''    rTrn.valMlInteresCal = rTrn.valMlInteres
    '''''    rTrn.valMlReajusteCal = rTrn.valMlReajuste

    '''''    rTrn.valCuoMvtoCal = rTrn.valCuoPatrFdesCal
    '''''    rTrn.valCuoInteresCal = rTrn.valCuoInteres
    '''''    rTrn.valCuoReajusteCal = rTrn.valCuoReajuste


    '''''    If rTrn.valCuoMvtoCal > 0 Then
    '''''        SumarTotalesComision()
    '''''        TrnAMov()
    '''''        CrearSaldosMovimientos()

    '''''        'AplicaVector()
    '''''        CalcularSaldo()

    '''''    Else
    '''''        GenerarLog("A", 15343, Nothing, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
    '''''    End If


    '''''End Sub



    Private Sub acreditaAjusteDecimalInformado()
        rTrn.valCuoAjusteDecimalCal = rTrn.valCuoAporteAdm
    End Sub
    Private Sub AcreditaSaldoIngreso()

        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()




        'CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)


        rTrn.valMlPatrFrecCal = rTrn.valMlMvto
        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto
        gRegistrosEnviados = gRegistrosEnviados + 1

        rTrn.valMlPatrFrecActCal = rTrn.valMlMvto
        rTrn.valCuoPatrFrecActCal = rTrn.valCuoMvto 'mat.Redondear(rTrn.valMlMvto / rTrn.valMlValorCuota, 2)

        rTrn.valMlPatrFdesCal = rTrn.valMlMvto
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
        gRegistrosCalculados = gRegistrosCalculados + 1

        rTrn.valMlMvtoCal = rTrn.valMlMvto
        rTrn.valCuoMvtoCal = rTrn.valCuoPatrFdesCal

        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()

        'AplicaVector()
        CalcularSaldo()

        If gcodOrigenProceso = "REREZSEL" Or gcodOrigenProceso = "REREZMAS" Or gcodOrigenProceso = "REREZCON" Then
            If rMovAcr.tipoMvto = "TIC" Then
                RezagoAHistorico()
            End If
        End If



    End Sub



    Private Sub AcreditaSaldoEgresoAPV()

        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        rTrn.valMlPatrFrecCal = rTrn.valMlMvto
        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto
        gRegistrosEnviados = gRegistrosEnviados + 1

        rTrn.valMlPatrFrecActCal = rTrn.valMlMvto
        rTrn.valCuoPatrFrecActCal = rTrn.valCuoMvto

        rTrn.valMlPatrFdesCal = rTrn.valMlMvto
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
        gRegistrosCalculados = gRegistrosCalculados + 1

        rTrn.valMlMvtoCal = rTrn.valMlMvto
        rTrn.valCuoMvtoCal = rTrn.valCuoPatrFdesCal

        rTrn.valMlComisPorcentualCal = rTrn.valMlComisPorcentual
        rTrn.valCuoComisPorcentualCal = rTrn.valCuoComisPorcentual

        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()

        'AplicaVector()
        CalcularSaldo()

        CerrarProducto()


    End Sub



    Private Sub AcreditaSaldoEgresoCTA()

        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        rTrn.valMlPatrFrecCal = Mat.Redondear(rTrn.valCuoMvto * rTrn.valMlValorCuota)
        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto
        gRegistrosEnviados = gRegistrosEnviados + 1

        rTrn.valMlPatrFrecActCal = rTrn.valMlMvto
        rTrn.valCuoPatrFrecActCal = rTrn.valCuoMvto

        rTrn.valMlPatrFdesCal = rTrn.valMlMvto
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
        gRegistrosCalculados = gRegistrosCalculados + 1

        rTrn.valMlMvtoCal = rTrn.valMlMvto
        rTrn.valCuoMvtoCal = rTrn.valCuoPatrFdesCal

        rTrn.valMlComisPorcentualCal = rTrn.valMlComisPorcentual
        rTrn.valCuoComisPorcentualCal = rTrn.valCuoComisPorcentual

        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()

        'AplicaVector()
        CalcularSaldo()

        CerrarProducto()



    End Sub
    Private Sub AcreditaRevSaldoEgresoCTA()

        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        rTrn.valMlPatrFrecCal = Mat.Redondear(rTrn.valCuoMvto * rTrn.valMlValorCuota)
        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto
        gRegistrosEnviados = gRegistrosEnviados + 1

        rTrn.valMlPatrFrecActCal = rTrn.valMlMvto
        rTrn.valCuoPatrFrecActCal = rTrn.valCuoMvto

        rTrn.valMlPatrFdesCal = rTrn.valMlMvto
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
        gRegistrosCalculados = gRegistrosCalculados + 1

        rTrn.valMlMvtoCal = rTrn.valMlMvto
        rTrn.valCuoMvtoCal = rTrn.valCuoPatrFdesCal

        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()

        'AplicaVector()
        CalcularSaldo()



    End Sub


    Private Sub AcreditaPensiones()

        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        rTrn.valMlPatrFrecCal = rTrn.valMlMvto
        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto
        gRegistrosEnviados = gRegistrosEnviados + 1

        rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
        rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal

        rTrn.valMlPatrFdesCal = rTrn.valMlMvto
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
        gRegistrosCalculados = gRegistrosCalculados + 1

        rTrn.valMlMvtoCal = rTrn.valMlMvto
        rTrn.valCuoMvtoCal = rTrn.valCuoPatrFdesCal


        rTrn.valMlComisFijaCal = rTrn.valMlComisFija
        rTrn.valCuoComisFijaCal = rTrn.valCuoComisFija

        rTrn.valMlComisPorcentualCal = rTrn.valMlComisPorcentual
        rTrn.valCuoComisPorcentualCal = rTrn.valCuoComisPorcentual

        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()

        'AplicaVector()
        CalcularSaldo()


    End Sub


    Private Sub AcreditaCambioFondoCargo()

        'CFA=Asignación, CFN=Cambio Fondo, CFD=Distribución, CFT=Traspaso Futuro

        rTrn.codDestinoTransaccionCal = "CTA"
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlComisPorcentual
        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto + rTrn.valCuoComisPorcentual

        gRegistrosEnviados += 1

        'PCI OS-6219995. 03/07/2014.
        'rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
        'rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal

        'rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
        'rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal

        rTrn.valMlPatrFrecActCal = rTrn.valMlMvto
        rTrn.valCuoPatrFrecActCal = rTrn.valCuoMvto

        rTrn.valMlPatrFdesCal = rTrn.valMlMvto
        rTrn.valCuoPatrFdesCal = rTrn.valCuoMvto

        gRegistrosCalculados += 1

        rTrn.valCuoMvtoCal = rTrn.valCuoPatrFdesCal
        rTrn.valMlMvtoCal = rTrn.valMlPatrFdesCal

        rTrn.valMlComisPorcentualCal = rTrn.valMlComisPorcentual
        rTrn.valCuoComisPorcentualCal = rTrn.valCuoComisPorcentual

        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()

        ''AplicaVector()
        CalcularSaldo()


    End Sub


    Private Sub AcreditaCambioFondoAbono()



        rTrn.codDestinoTransaccionCal = "CTA"
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        rTrn.valMlPatrFrecCal = rTrn.valMlMvto
        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto
        gRegistrosEnviados = gRegistrosEnviados + 1

        rTrn.valMlPatrFrecActCal = rTrn.valMlMvto
        rTrn.valCuoPatrFrecActCal = rTrn.valCuoMvto

        rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal

        gRegistrosCalculados = gRegistrosCalculados + 1

        rTrn.valCuoMvtoCal = rTrn.valCuoPatrFdesCal
        rTrn.valMlMvtoCal = rTrn.valMlPatrFdesCal


        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()

        ''AplicaVector()
        CalcularSaldo()



    End Sub

    Private Sub AcreditaTransferencia()
        If blIgnorar Then
            Exit Sub
        End If

        If rTrn.codDestinoTransaccion = "TRF" Then
            rTrn.codDestinoTransaccionCal = rTrn.codDestinoTransaccion

            rTrn.valMlMvtoCal = rTrn.valMlMvto
            rTrn.valCuoMvtoCal = rTrn.valCuoMvto
            rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()

            If rTrn.codOrigenProceso = "RECAUDAC" Then 'Or rTrn.codOrigenProceso = "TRAINREZ" Or rTrn.codOrigenProceso = "TRAINRZC" Or rTrn.codOrigenProceso = "TRAIPAGN" Then
                rTrn.valCuoMvtoCal = Mat.Redondear(rTrn.valMlMvtoCal / rTrn.valMlValorCuotaCaja, 2)
            End If

        End If
        gRegistrosEnviados = gRegistrosEnviados + 1
        gRegistrosCalculados = gRegistrosCalculados + 1

        TrnATrf()
        CrearTransferencia()
        'SumarTotales()

    End Sub

    Private Sub AcreditaTransferenciaCargo()

        If blIgnorar Then
            Exit Sub
        End If

        If rTrn.codOrigenTransaccion = "TRF" Then
            rTrn.codDestinoTransaccionCal = rTrn.codOrigenTransaccion

            dsAux = Transferencia.Egresos.traer(gdbc,gidAdm, rTrn.seqMvtoOrigen)
            If dsAux.Tables(0).Rows.Count = 0 Then
                blIgnorar = True
                GenerarLog("E", 0, "Hebra " & gIdHebra & " - La transferencia no se encuentra en estado pendiente", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                Exit Sub
            End If

            rTrf = New ccTransfApv(dsAux)

            If rTrf.estadoTransferencia <> "P" Then
                blIgnorar = True
                GenerarLog("E", 0, "Hebra " & gIdHebra & " - La transferencia no se encuentra en estado pendiente", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                Exit Sub
            End If

            rTrn.valCuoMvtoCal = rTrf.valCuoTransferenciaRec + rTrf.valCuoInteres + rTrf.valCuoReajuste
            rTrn.valMlMvtoCal = Mat.Redondear(rTrn.valCuoMvtoCal * gvalMlCuotaDestinoC, 0)

            rTrf.estadoTransferencia = "R"
            rTrf.fecTransferencia = rTrn.fecAcreditacion
            rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()

        End If
        gRegistrosEnviados = gRegistrosEnviados + 1
        gRegistrosCalculados = gRegistrosCalculados + 1


        modificarTransferencia()


    End Sub


    Private Sub AcreditaDeclaraciones()

        gRegistrosEnviados = gRegistrosEnviados + 1
        gRegistrosCalculados = gRegistrosCalculados + 1
        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()

        If rMovAcr.indCotizacion = "S" Then

            CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

            TrnAMov()
            CrearSaldosMovimientos()
            CalcularSaldo()
        End If

        RezagoAHistorico()

    End Sub
    Private Sub AcreditaAjustesEspeciales()

        gRegistrosEnviados = gRegistrosEnviados + 1
        gRegistrosCalculados = gRegistrosCalculados + 1

        rTrn.codDestinoTransaccionCal = rTrn.codDestinoTransaccion
        ' rTrn.tipoProducto = rTrn.codDestinoTransaccionCal
        ' rTrn.categoria = rTrn.codDestinoTransaccionCal

        If rTrn.tipoImputacion = "ABO" Then
            rTrn.tipoFondoDestinoCal = rTrn.tipoFondoDestino
        Else
            rTrn.tipoFondoDestinoCal = rTrn.tipoFondoOrigen
        End If


        rTrn.valMlMvtoCal = rTrn.valMlMvto
        rTrn.valCuoMvtoCal = rTrn.valCuoMvto
        rTrn.valMlPatrFrecCal = rTrn.valMlMvtoCal
        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvtoCal
        rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
        rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal

        rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal

    End Sub


    Private Sub AcreditaCotREC()


        rTrn.codDestinoTransaccionCal = "CTA"
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        'Calculadas
        gRegistrosCalculados = gRegistrosCalculados + 1
        If rTrn.tipoFondoDestinoCal = "C" Then
            rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
            rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal

            rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
            rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal

        Else
            gvalMlPatrDistFondoC += rTrn.valMlPatrFrecCal - rTrn.valMlTransferenciaCal

            If (rTrn.tipoProducto = "CCV" Or rTrn.tipoProducto = "CDC") And rTrn.valCuoPatrFrecCal = 0 Then
                'Oficio 3021. OS-2789587
                rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
                rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
                rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal '- rTrn.valMlTransferenciaCal
                rTrn.valCuoPatrFdesCal = Mat.Redondear(rTrn.valMlPatrFdesCal / rTrn.valMlValorCuota, 2)
            Else
                rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
                rTrn.valMlPatrFrecActCal = Mat.Redondear(rTrn.valCuoPatrFrecCal * gvalMlCuotaDestinoC, 0)
                rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal '- rTrn.valMlTransferenciaCal
                rTrn.valCuoPatrFdesCal = Mat.Redondear(rTrn.valMlPatrFdesCal / rTrn.valMlValorCuota, 2)
            End If
        End If

        'primaSIS --CalcularNominales2------------------

        'Acreditadas
        CalcularNominales(rTrn.valMlMvto, _
                          rTrn.valMlInteres, _
                          rTrn.valMlReajuste, _
                          rTrn.valMlAdicional, _
                          rTrn.valMlAdicionalInteres, _
                          rTrn.valMlAdicionalReajuste, _
                          rTrn.valMlExcesoLinea, _
                          rTrn.valMlExcesoEmpl)

        TraerControlRentas()
        CalcularExcesos()
        CalcularComisionPorcentual()

        'INI - OS-7194707 PCI 08/04/2015 Ajustes Decimales para cuando se realizan Cargos y no Hay SALDO.
        VerificaAjustesDecimal(gValDif, rTrn.valCuoMvtoCal, rTrn.valCuoAdicionalCal, rTrn.valCuoAdicionalInteresCal, rTrn.valCuoAdicionalReajusteCal, rTrn.valCuoPrimaSisCal, rTrn.valCuoCompensCal)

        rTrn.valCuoAjusteDecimalCal = gValDif
        rTrn.valMlAjusteDecimalCal = 0
        'FIN - OS-7194707 PCI 08/04/2015 Ajustes Decimales para cuando se realizan Cargos y no Hay SALDO.

        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()
        realizarAbonosExcesos()
        realizarAbonosCargosCompensasion()
        realizarCargoTransfPrimaComision()
        CalcularComisionFija()
        'ActualizaCotTrabajosPesados()


        'AplicaVector()
        CalcularSaldo()

        '--OS:9075964 - nuevas validaciones PLV
        If blGenExcesoEnLinea And gcodAdministradora = 1032 Then
            CrearControlRenta()
        End If

        'CrearControlRecaudacionREC()
        'ActualizaRentas()


    End Sub

    'SIS//
    Private Sub AcreditaCotREC2()

        'lfc:9255512(MoD) 9350933(CAP)-->>> PAGO DE SOLO SIS se añade cargo al auxiliar 10-11-2016
        blSoloSISAboCar = False
        If (rTrn.codOrigenProceso = "TRAIPAGN" Or _
            rTrn.codOrigenProceso = "REREZSEL" Or _
            rTrn.codOrigenProceso = "REREZMAS") _
            And (rMovAcr.tipoMvto = "CTP"  Or rTrn.tipoRezago = 38 )And gtipoProceso <> "AC" _
            And (rTrn.valMlPrimaSis + rTrn.valMlPrimaSisInteres + rTrn.valMlPrimaSisReajuste) = 0 _
            And (rTrn.valCuoPrimaSis + rTrn.valCuoPrimaSisInteres + rTrn.valCuoPrimaSisReajuste) = 0 Then

            'se elimina porque debe ser para todos, lfc:20-02-2017
            'And (gcodAdministradora = 1034 Or gcodAdministradora = 1033) _

            blSoloSISAboCar = True
            'If rTrn.valMlPrimaSis = 0 _
            'And rTrn.valMlPrimaSisInteres = 0 _
            'And rTrn.valMlPrimaSisReajuste = 0 Then

            rTrn.valMlRentaImponibleSis = rTrn.valMlRentaImponible
            rTrn.codMvtoPrim = rTrn.codMvto

            rTrn.valMlPrimaSis += rTrn.valMlMvto
            rTrn.valMlPrimaSisInteres += rTrn.valMlInteres
            rTrn.valMlPrimaSisReajuste += rTrn.valMlReajuste

            rTrn.valCuoPrimaSis += rTrn.valCuoMvto
            rTrn.valCuoPrimaSisInteres += rTrn.valCuoInteres
            rTrn.valCuoPrimaSisReajuste += rTrn.valCuoReajuste

            rTrn.valMlMvto = 0
            rTrn.valMlInteres = 0
            rTrn.valMlReajuste = 0
            rTrn.valCuoMvto = 0
            rTrn.valCuoInteres = 0
            rTrn.valCuoReajuste = 0

            'End If

        End If '--<<<----

        rTrn.codDestinoTransaccionCal = "CTA"
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        'Calculadas
        gRegistrosCalculados = gRegistrosCalculados + 1
        If rTrn.tipoFondoDestinoCal = "C" Then
            rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
            rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
            rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
            rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
        Else
            gvalMlPatrDistFondoC += rTrn.valMlPatrFrecCal - rTrn.valMlTransferenciaCal
            If (rTrn.tipoProducto = "CCV" Or rTrn.tipoProducto = "CDC") And rTrn.valCuoPatrFrecCal = 0 Then
                'Oficio 3021. OS-2789587
                rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
                rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
                rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal '- rTrn.valMlTransferenciaCal
                rTrn.valCuoPatrFdesCal = Mat.Redondear(rTrn.valMlPatrFdesCal / rTrn.valMlValorCuota, 2)
            Else
                rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
                rTrn.valMlPatrFrecActCal = Mat.Redondear(rTrn.valCuoPatrFrecCal * gvalMlCuotaDestinoC, 0)
                rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal '- rTrn.valMlTransferenciaCal
                rTrn.valCuoPatrFdesCal = Mat.Redondear(rTrn.valMlPatrFdesCal / rTrn.valMlValorCuota, 2)
			End If



			'lfc: 08-09-2021 - montos menores no son patrimonizados por no generar cuotas -->>>---
			If rTrn.tipoImputacion = "ABO" Then
				If rTrn.valMlPatrFrecCal > 0 And rTrn.valCuoPatrFrecCal = 0 And rTrn.valMlPatrFrecActCal = 0 And rTrn.valCuoPatrFrecActCal = 0 Then
					rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal

					If rTrn.valMlPatrFdesCal = 0 And rTrn.valCuoPatrFdesCal = 0 Then
						rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecCal
					End If
				End If
			End If
			'--<<<----


		End If
		'acreditadas

		' Solo para Recuperacion de Rezagos se Calcula AJUSTE DECIMAL
		If rTrn.codOrigenProceso = "REREZSEL" Or rTrn.codOrigenProceso = "REREZMAS" Then
			'CalcularNominales4(rTrn.valMlMvto, _
			'                  rTrn.valMlInteres, _
			'                  rTrn.valMlReajuste, _
			'                  rTrn.valMlAdicional, _
			'                  rTrn.valMlAdicionalInteres, _
			'                  rTrn.valMlAdicionalReajuste, _
			'                  rTrn.valMlExcesoLinea, _
			'                  rTrn.valMlPrimaSis, _
			'                  rTrn.valMlPrimaSisInteres, _
			'                  rTrn.valMlPrimaSisReajuste)

			' OS-5598016  Se agrega Exc Empleador 


			CalcularNominales4(rTrn.valMlMvto, _
							  rTrn.valMlInteres, _
							  rTrn.valMlReajuste, _
							  rTrn.valMlAdicional, _
							  rTrn.valMlAdicionalInteres, _
							  rTrn.valMlAdicionalReajuste, _
							  rTrn.valMlExcesoLinea, _
							  rTrn.valMlPrimaSis, _
							  rTrn.valMlPrimaSisInteres, _
							  rTrn.valMlPrimaSisReajuste, _
							  rTrn.valMlExcesoEmpl, _
							  gValDif)

		Else
			'CalcularNominales2(rTrn.valMlMvto, _
			'      rTrn.valMlInteres, _
			'      rTrn.valMlReajuste, _
			'      rTrn.valMlAdicional, _
			'      rTrn.valMlAdicionalInteres, _
			'      rTrn.valMlAdicionalReajuste, _
			'      rTrn.valMlExcesoLinea, _
			'       rTrn.valMlPrimaSis, _
			'      rTrn.valMlPrimaSisInteres, _
			'      rTrn.valMlPrimaSisReajuste)

			' OS-5598016  Se agrega Exc Empleador 
			CalcularNominales2(rTrn.valMlMvto, _
				  rTrn.valMlInteres, _
				  rTrn.valMlReajuste, _
				  rTrn.valMlAdicional, _
				  rTrn.valMlAdicionalInteres, _
				  rTrn.valMlAdicionalReajuste, _
				  rTrn.valMlExcesoLinea, _
				   rTrn.valMlPrimaSis, _
				  rTrn.valMlPrimaSisInteres, _
				  rTrn.valMlPrimaSisReajuste, _
				  rTrn.valMlExcesoEmpl)
		End If

		TraerControlRentas()		  '--------------------------------ok
		' call arriba para todos los origenes TraerControlRentasSIS() '------ok


		'Solo se debe calcular Excesos para cotizaciones que no sean INDEPENDIENTES. PCI. 15/05/2012.
		'Ni TGR. PCI. 14/05/2013
		If (rTrn.tipoEntidadPagadora = "V" Or rTrn.tipoEntidadPagadora = "O" Or rTrn.tipoEntidadPagadora = "A") Or _
			(rTrn.tipoRezago = 35 Or rTrn.tipoRezago = 36 Or rTrn.tipoRezago = 39) Then
		Else
			CalcularExcesos()		  '------------------------------------ok
		End If


		CalcularComisionPorcentual2()	   '---------------rev



		blSaldoNegativo = False
		'LFC: 20-10-2017 OS-10385099 - saldos negativos en transacciones con total negativo y el afiliado no tiene saldo suficiente para cubrir -->>>--
		Dim totTrs As Decimal
		If gcodAdministradora = 1033 Then
			If rTrn.tipoImputacion = "ABO" And rTrn.codDestinoTransaccionCal = "CTA" And rTrn.valCuoPatrFdesCal > 0 And rTrn.valCuoCompensCal < 0 Then
				totTrs = rTrn.valCuoPatrFdesCal - (rTrn.valCuoComisPorcentualCal + rTrn.valCuoComisFijaCal + rTrn.valCuoPrimaCal)
				If totTrs < 0 Then			 'el cargo es mayor al abono
					If Math.Abs(totTrs) > rSal.valCuoSaldo Then
						blSaldoNegativo = True
						If rTrn.valCuoAjusteDecimalCal >= 0 Then
							rTrn.valCuoAjusteDecimalCal -= totTrs
						Else
							rTrn.valCuoAjusteDecimalCal += totTrs
						End If
					End If
				End If
			End If
		End If
		'--<<<----<<<----
		'End If

		'modificacion por OS:8984519 - CON ajuste decimal en recuperaciones de rezagos
		VerificaAjustesDecimal(gValDif, rTrn.valCuoMvtoCal, rTrn.valCuoAdicionalCal, rTrn.valCuoAdicionalInteresCal, rTrn.valCuoAdicionalReajusteCal, rTrn.valCuoPrimaSisCal, rTrn.valCuoCompensCal)

		SumarTotales()	   '------------------------------rev
		TrnAMov()		'----------------------------------rev
		CrearSaldosMovimientos()	   '--------------------rev

		realizarCargoPrimaSis()	   '---------------------REV --new items

		realizarAbonosExcesos()	   '---------------------rev

		realizarAbonosExcesosEmpl()

		realizarAbonosCargosCompensasion()

		'NEW..COMISIONES---->>>>>>>>>>>>>>>>>>>>>>
		'sólo periodos SIS 'lfc:22/10/2009 - CASOS ESPECIALES
		realizarCargoTransfComision()

		CalcularComisionFija()
		CalcularSaldo()

		'para rezagos //validacion origen_proc...
		RezagoAHistorico()

		'--OS:9075964 - nuevas validaciones PLV
		If blGenExcesoEnLinea And gcodAdministradora = 1032 Then
			CrearControlRenta()
		End If

    End Sub

    Private Sub AcreditaCotREC3()


        rTrn.codDestinoTransaccionCal = "CTA"
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()

        'MODELO. Verifica Fechas de Acreditacion para Patrimonizacion. OS-5875552. 09/05/2014.
        'If rTrn.fecOperacion = rTrn.fecAcreditacion And rTrn.codOrigenProceso = "ACREXSTJ" Then
        If rTrn.fecOperacion = gfecValorCuota And rTrn.codOrigenProceso = "ACREXSTJ" Then
            'OS-7249875. Solo para PLV. 27/04/2015
            If rTrn.valMlPatrFrecCal > 0 And rTrn.valCuoPatrFrecCal = 0 And gcodAdministradora = 1032 Then
                rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
                rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
                rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
                rTrn.valCuoPatrFdesCal = Mat.Redondear(rTrn.valMlPatrFdesCal / rTrn.valMlValorCuota, 2)
                'rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
            Else
                rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
                rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
                rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
                rTrn.valCuoPatrFdesCal = Mat.Redondear(rTrn.valMlPatrFdesCal / rTrn.valMlValorCuota, 2)
            End If
        Else
            'Calculadas
            gRegistrosCalculados = gRegistrosCalculados + 1
            If rTrn.tipoFondoDestinoCal = "C" Then
                rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
                rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
                rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
                rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
            Else
                gvalMlPatrDistFondoC += rTrn.valMlPatrFrecCal - rTrn.valMlTransferenciaCal
                If (rTrn.tipoProducto = "CCV" Or rTrn.tipoProducto = "CDC") And rTrn.valCuoPatrFrecCal = 0 Then
                    'Oficio 3021. OS-2789587
                    rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
                    rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
                    rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal '- rTrn.valMlTransferenciaCal
                    rTrn.valCuoPatrFdesCal = Mat.Redondear(rTrn.valMlPatrFdesCal / rTrn.valMlValorCuota, 2)
                Else
                    'OS-7249875. Solo para PLV. 27/04/2015
                    If (rTrn.codOrigenProceso = "ACREXSTJ" Or rTrn.codOrigenProceso = "ACREXAFC") And _
                    rTrn.valMlPatrFrecCal > 0 And rTrn.valCuoPatrFrecCal = 0 And gcodAdministradora = 1032 Then
                        rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
                        rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
                        rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
                        rTrn.valCuoPatrFdesCal = Mat.Redondear(rTrn.valMlPatrFdesCal / rTrn.valMlValorCuota, 2)
                        'rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
                    Else
                        rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
                        rTrn.valMlPatrFrecActCal = Mat.Redondear(rTrn.valCuoPatrFrecCal * gvalMlCuotaDestinoC, 0)
                        rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal '- rTrn.valMlTransferenciaCal
                        rTrn.valCuoPatrFdesCal = Mat.Redondear(rTrn.valMlPatrFdesCal / rTrn.valMlValorCuota, 2)
                    End If
                End If
            End If
        End If

        ''Calculadas
        'gRegistrosCalculados = gRegistrosCalculados + 1
        'If rTrn.tipoFondoDestinoCal = "C" Then
        '    rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
        '    rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
        '    rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
        '    rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
        'Else
        '    gvalMlPatrDistFondoC += rTrn.valMlPatrFrecCal - rTrn.valMlTransferenciaCal
        '    If (rTrn.tipoProducto = "CCV" Or rTrn.tipoProducto = "CDC") And rTrn.valCuoPatrFrecCal = 0 Then
        '        'Oficio 3021. OS-2789587
        '        rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
        '        rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
        '        rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal '- rTrn.valMlTransferenciaCal
        '        rTrn.valCuoPatrFdesCal = Mat.Redondear(rTrn.valMlPatrFdesCal / rTrn.valMlValorCuota, 2)
        '    Else
        '        rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
        '        rTrn.valMlPatrFrecActCal = Mat.Redondear(rTrn.valCuoPatrFrecCal * gvalMlCuotaDestinoC, 0)
        '        rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal '- rTrn.valMlTransferenciaCal
        '        rTrn.valCuoPatrFdesCal = Mat.Redondear(rTrn.valMlPatrFdesCal / rTrn.valMlValorCuota, 2)
        '    End If
        'End If
        ''Acreditadas

        If rTrn.codOrigenProceso = "ACREXIPS" Then

            CalcularNominales6(rTrn.valMlMvto, _
                  rTrn.valMlInteres, _
                  rTrn.valMlReajuste, _
                  rTrn.valMlAdicional, _
                  rTrn.valMlAdicionalInteres, _
                  rTrn.valMlAdicionalReajuste, _
                  rTrn.valMlExcesoLinea, _
                  rTrn.valMlPrimaSis, _
                  rTrn.valMlPrimaSisInteres, _
                  rTrn.valMlPrimaSisReajuste)

        Else

            CalcularNominales(rTrn.valMlMvto, _
                              rTrn.valMlInteres, _
                              rTrn.valMlReajuste, _
                              rTrn.valMlAdicional, _
                              rTrn.valMlAdicionalInteres, _
                              rTrn.valMlAdicionalReajuste, _
                              rTrn.valMlExcesoLinea, _
                              rTrn.valMlExcesoEmpl)
        End If

        CalcularCargoPrima()

        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()

        'Prima Sis
        realizarCargoPrimaSis()

        'Excesos
        realizarAbonosExcesos()

        'Rentabilidad
        realizarAbonosCargosCompensasion()

        CalcularSaldo()

    End Sub





    Private Sub AcreditaCotREC4()


        rTrn.codDestinoTransaccionCal = "CTA"
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        'Calculadas
        gRegistrosCalculados = gRegistrosCalculados + 1
        If rTrn.tipoFondoDestinoCal = "C" Then
            rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
            rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal

            rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
            rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal

        Else
            gvalMlPatrDistFondoC += rTrn.valMlPatrFrecCal - rTrn.valMlTransferenciaCal

            If (rTrn.tipoProducto = "CCV" Or rTrn.tipoProducto = "CDC") And rTrn.valCuoPatrFrecCal = 0 Then
                'Oficio 3021. OS-2789587
                rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
                rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
                rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal '- rTrn.valMlTransferenciaCal
                rTrn.valCuoPatrFdesCal = Mat.Redondear(rTrn.valMlPatrFdesCal / rTrn.valMlValorCuota, 2)
            Else
                rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
                rTrn.valMlPatrFrecActCal = Mat.Redondear(rTrn.valCuoPatrFrecCal * gvalMlCuotaDestinoC, 0)
                rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal '- rTrn.valMlTransferenciaCal
                rTrn.valCuoPatrFdesCal = Mat.Redondear(rTrn.valMlPatrFdesCal / rTrn.valMlValorCuota, 2)
            End If
        End If

        'primaSIS --CalcularNominales2------------------

        'Acreditadas
        CalcularNominales7(rTrn.valMlMvto, _
                          rTrn.valMlInteres, _
                          rTrn.valMlReajuste, _
                          rTrn.valMlAdicional, _
                          rTrn.valMlAdicionalInteres, _
                          rTrn.valMlAdicionalReajuste, _
                          rTrn.valMlExcesoLinea)

        TraerControlRentas()
        CalcularExcesos()
        CalcularComisionPorcentual()
        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()
        realizarAbonosExcesos()
        realizarAbonosCargosCompensasion()
        realizarCargoTransfPrimaComision()
        CalcularComisionFija()
        'ActualizaCotTrabajosPesados()


        'AplicaVector()
        CalcularSaldo()

        '--OS:9075964 - nuevas validaciones PLV
        If blGenExcesoEnLinea And gcodAdministradora = 1032 Then
            CrearControlRenta()
        End If

        'CrearControlRecaudacionREC()
        'ActualizaRentas()


    End Sub

    'SIS//
    Private Sub AcreditaCotREC5()

        ' lfc: ca-1214034  expasis en TGR 20-05-2019
        If Me.gcodAdministradora = 1034 And rCli.codEstadoAfiliado = 7 And rTrn.valMlExcesoEmpl > 0 And rTrn.valMlPrimaSis = 0 And rTrn.codOrigenProceso = "ACREXTGR" Then
            blExpasisTGR = True
        Else
            blExpasisTGR = False
        End If


        rTrn.codDestinoTransaccionCal = "CTA"
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        'Calculadas
        gRegistrosCalculados = gRegistrosCalculados + 1
        If rTrn.tipoFondoDestinoCal = "C" Then
            rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
            rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
            rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
            rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
        Else
            gvalMlPatrDistFondoC += rTrn.valMlPatrFrecCal - rTrn.valMlTransferenciaCal
            If (rTrn.tipoProducto = "CCV" Or rTrn.tipoProducto = "CDC") And rTrn.valCuoPatrFrecCal = 0 Then
                'Oficio 3021. OS-2789587
                rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
                rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
                rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal '- rTrn.valMlTransferenciaCal
                rTrn.valCuoPatrFdesCal = Mat.Redondear(rTrn.valMlPatrFdesCal / rTrn.valMlValorCuota, 2)
            Else
                rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
                rTrn.valMlPatrFrecActCal = Mat.Redondear(rTrn.valCuoPatrFrecCal * gvalMlCuotaDestinoC, 0)
                rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal '- rTrn.valMlTransferenciaCal
                rTrn.valCuoPatrFdesCal = Mat.Redondear(rTrn.valMlPatrFdesCal / rTrn.valMlValorCuota, 2)
            End If
        End If
        'acreditadas

        ' Solo para Recuperacion de Rezagos se Calcula AJUSTE DECIMAL
        If rTrn.codOrigenProceso = "REREZSEL" Or rTrn.codOrigenProceso = "REREZMAS" Then
            CalcularNominales8(rTrn.valMlMvto, _
                              rTrn.valMlInteres, _
                              rTrn.valMlReajuste, _
                              rTrn.valMlAdicional, _
                              rTrn.valMlAdicionalInteres, _
                              rTrn.valMlAdicionalReajuste, _
                              rTrn.valMlExcesoLinea, _
                              rTrn.valMlPrimaSis, _
                              rTrn.valMlPrimaSisInteres, _
                              rTrn.valMlPrimaSisReajuste)

        Else
		' lfc: ca-1214034  expasis en TGR 20-05-2019
            CalcularNominales9(rTrn.valMlMvto, _
                  rTrn.valMlInteres, _
                  rTrn.valMlReajuste, _
                  rTrn.valMlAdicional, _
                  rTrn.valMlAdicionalInteres, _
                  rTrn.valMlAdicionalReajuste, _
                  rTrn.valMlExcesoLinea, _
                   rTrn.valMlPrimaSis, _
                  rTrn.valMlPrimaSisInteres, _
                  rTrn.valMlPrimaSisReajuste, _
                  rTrn.valMlExcesoEmpl)
        End If

        TraerControlRentas()    '--------------------------------ok
        ' call arriba para todos los origenes TraerControlRentasSIS() '------ok

        'Solo se debe calcular Excesos para cotizaciones que no sean INDEPENDIENTES. PCI. 15/05/2012.
        'Ni TGR. PCI. 14/05/2013
        If (rTrn.tipoEntidadPagadora = "V" Or rTrn.tipoEntidadPagadora = "O" Or rTrn.tipoEntidadPagadora = "A") Or _
            rTrn.codOrigenProceso = "ACREXTGR" Then

            ' lfc: ca-1214034  expasis en TGR 20-05-2019
            If blExpasisTGR Then
                gvalMlExcesoCal = rTrn.valMlExcesoEmplCal
            End If
            '---<<<<---
        Else
            CalcularExcesos() '------------------------------------ok
        End If

        CalcularComisionPorcentual2() '---------------rev
        SumarTotales() '------------------------------rev
        TrnAMov()  '----------------------------------rev
        CrearSaldosMovimientos() '--------------------rev

        realizarCargoPrimaSis() '---------------------REV --new items

        realizarAbonosExcesos() '---------------------rev

        ' lfc: ca-1214034  expasis en TGR 20-05-2019
        If blExpasisTGR Then Me.realizarAbonosExcesosEmpl()

        realizarAbonosCargosCompensasion()

        'NEW..COMISIONES---->>>>>>>>>>>>>>>>>>>>>>
        'sólo periodos SIS 'lfc:22/10/2009 - CASOS ESPECIALES
        realizarCargoTransfComision()

        CalcularComisionFija()
        CalcularSaldo()

        'para rezagos //validacion origen_proc...
        RezagoAHistorico()

        '--OS:9075964 - nuevas validaciones PLV
        If blGenExcesoEnLinea And gcodAdministradora = 1032 Then
            CrearControlRenta()
        End If

    End Sub








    Private Sub AcreditaCotREZ()
        Dim valMlTotNominal As Decimal
        Dim valCuoTotNominal As Decimal

        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)


        rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste + _
                              rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste + _
                              rTrn.valMlExcesoLinea

        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto + rTrn.valCuoInteres + rTrn.valCuoReajuste + _
                                 rTrn.valCuoAdicional + rTrn.valCuoAdicionalInteres + rTrn.valCuoAdicionalReajuste + _
                                 rTrn.valCuoExcesoLinea



        gRegistrosEnviados = gRegistrosEnviados + 1


        'Calculadas
        gRegistrosCalculados = gRegistrosCalculados + 1

        If rTrn.tipoFondoDestinoCal = "C" Then

            rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
            rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal

            rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
            rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal

            valMlTotNominal = rTrn.valMlPatrFrecCal
            valCuoTotNominal = rTrn.valCuoPatrFrecCal

        Else

            rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
            rTrn.valMlPatrFrecActCal = Mat.Redondear(rTrn.valCuoPatrFrecCal * gvalMlCuotaDestinoC, 0)

            rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
            rTrn.valCuoPatrFdesCal = Mat.Redondear(rTrn.valMlPatrFdesCal / rTrn.valMlValorCuota, 2)

            valMlTotNominal = rTrn.valMlPatrFrecCal
            valCuoTotNominal = Mat.Redondear(valMlTotNominal / rTrn.valMlValorCuota, 2)
        End If



        CalcularNominalesValorizado(valMlTotNominal, _
                            valCuoTotNominal, _
                            rTrn.valMlMvto, _
                            rTrn.valMlInteres, _
                            rTrn.valMlReajuste, _
                            rTrn.valMlAdicional, _
                            rTrn.valMlAdicionalInteres, _
                            rTrn.valMlAdicionalReajuste, _
                            rTrn.valMlExcesoLinea)
        TraerControlRentas()
        'CalcularExcesos()
        CalcularComisionPorcentual()
        DeterminaMontoInstitucionSalud()
        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()
        'realizarAbonosExcesos()
        realizarAbonosCargosCompensasion()
        realizarCargoTransfPrimaComision()
        CalcularComisionFija()
        'ActualizaCotTrabajosPesados()

        'AplicaVector()
        CalcularSaldo()


        '--OS:9075964 - nuevas validaciones PLV
        If blGenExcesoEnLinea And gcodAdministradora = 1032 Then
            CrearControlRenta()
        End If

        'If rTrn.codOrigenMvto = "RECAUDAC" Then
        '    CrearControlRecaudacionREZ()
        'End If

        'ActualizaRentas()

    End Sub

    Private Sub AcreditaCotRezNominal()
        Dim valMlTraspasado As Decimal

        '--.-->>>>lfc:15/04/09:rezagos antiguos/descuadrados-------------------------------------------------
        Dim fecRezagoAntiguo As Date = New Date(1988, 1, 1).Date
        Dim fechaCaja As Date
        If rTrn.codOrigenMvto = "RECAUDAC" Then
            fechaCaja = rTrn.fecOperacion
        Else
            fechaCaja = rTrn.fecOperacionAdmOrigen
        End If
        If fechaCaja < fecRezagoAntiguo Then
            g_valMlAdicional = rTrn.valMlAdicional
            g_valMlAdicionalInt = rTrn.valMlAdicionalInteres
            g_valMlAdicionalRea = rTrn.valMlAdicionalReajuste

            rTrn.valMlAdicional = 0
            rTrn.valMlAdicionalInteres = 0
            rTrn.valMlAdicionalReajuste = 0
        End If
        '--.--<<<<<'--.--------------------------------------------------------------------------------------


        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste + _
                              rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste + _
                              rTrn.valMlExcesoLinea

        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto + rTrn.valCuoInteres + rTrn.valCuoReajuste + _
                                 rTrn.valCuoAdicional + rTrn.valCuoAdicionalInteres + rTrn.valCuoAdicionalReajuste + _
                                 rTrn.valCuoExcesoLinea

        gRegistrosEnviados = gRegistrosEnviados + 1


        'Calculadas
        gRegistrosCalculados = gRegistrosCalculados + 1

        If rTrn.tipoFondoDestinoCal = "C" Then

            rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
            rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal

            rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
            rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal

        Else
            If (rTrn.tipoProducto = "CCV" Or rTrn.tipoProducto = "CDC") And rTrn.valCuoPatrFrecCal = 0 Then
                'Oficio 3021. OS-2789587
                rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
                rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
                rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal '- rTrn.valMlTransferenciaCal
                rTrn.valCuoPatrFdesCal = Mat.Redondear(rTrn.valMlPatrFdesCal / rTrn.valMlValorCuota, 2)
            Else
				rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal

				'-->> LFC 6901817 error en la rentabilidad cuando no se generan cuotas --
				'--rTrn.valMlPatrFrecActCal = Mat.Redondear(rTrn.valCuoPatrFrecCal * gvalMlCuotaDestinoC, 0)

				If rTrn.valCuoPatrFrecActCal = 0 And rTrn.valMlPatrFrecCal > 0 Then
					rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
				Else
					rTrn.valMlPatrFrecActCal = Mat.Redondear(rTrn.valCuoPatrFrecCal * gvalMlCuotaDestinoC, 0)
				End If


				rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
				rTrn.valCuoPatrFdesCal = Mat.Redondear(rTrn.valMlPatrFdesCal / rTrn.valMlValorCuota, 2)

            End If

        End If

        valMlTraspasado = rTrn.valMlPatrFrecCal

        If rTrn.valMlMontoNominal = 0 Then
            rTrn.valMlMontoNominal = rTrn.valMlPatrFrecCal
        End If

        If fechaCaja < fecRezagoAntiguo And fechaCaja >= New Date(1981, 5, 1) Then
            'OS- 9961839 - FBA – 05/2018– INI
            AperturaMontosAbonos(rTrn.valMlMontoNominal, valMlTraspasado, rMovAcr, rTrn)
            'OS- 9961839 - FBA – 05/2018– FIN

        Else
            'Solo Calcula Ajuste Decimal para Origen REREZMAS y REREZSEL
            If rTrn.codOrigenProceso = "REREZMAS" Or rTrn.codOrigenProceso = "REREZSEL" Then
                AperturaMontosAbonos2(rTrn.valMlMontoNominal, valMlTraspasado, rMovAcr, rTrn)
            Else
                AperturaMontosAbonos(rTrn.valMlMontoNominal, valMlTraspasado, rMovAcr, rTrn)
            End If
        End If

        TraerControlRentas()
        CalcularComisionPorcentual()

        '--.-->>>>lfc:15/04/09:rezagos antiguos/descuadrados-------------------------------------------------
        If fechaCaja < fecRezagoAntiguo And fechaCaja >= New Date(1981, 5, 1) Then
            rTrn.valMlAdicional = g_valMlAdicional
            rTrn.valMlAdicionalInteres = g_valMlAdicionalInt
            rTrn.valMlAdicionalReajuste = g_valMlAdicionalRea

            rTrn.valMlAdicionalCal = g_valMlAdicional
            rTrn.valMlAdicionalInteresCal = g_valMlAdicionalInt
            rTrn.valMlAdicionalReajusteCal = g_valMlAdicionalRea

            g_valMlAdicional = 0
            g_valMlAdicionalInt = 0
            g_valMlAdicionalRea = 0
        End If
        '--.--<<<<<'--.- -------------------------------------------------------------------------------------

        'OS-7079391. 09/03/2015. OS-7243919 01/04/2016
        If blAdicionalAntiguo Then
            rTrn.valMlAdicionalCal = 0
            rTrn.valMlAdicionalInteresCal = 0
            rTrn.valMlAdicionalReajusteCal = 0
        End If

        DeterminaMontoInstitucionSalud()

        If (rTrn.codOrigenProceso = "REREZSEL" Or rTrn.codOrigenProceso = "REREZMAS") And gcodAdministradora = 1032 Then
            VerificaAjustesDecimal(gValDif, rTrn.valCuoMvtoCal, rTrn.valCuoAdicionalCal, rTrn.valCuoAdicionalInteresCal, rTrn.valCuoAdicionalReajusteCal, rTrn.valCuoPrimaSisCal, rTrn.valCuoCompensCal)
        End If

        'verifica el saldo del afiliado y si es cargo

        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()
        realizarAbonosExcesos()
        realizarAbonosCargosCompensasion()
        realizarCargoTransfPrimaComision()
        CalcularComisionFija()
        'ActualizaCotTrabajosPesados()

        'AplicaVector()
        CalcularSaldo()

        RezagoAHistorico()

        '--OS:9075964 - nuevas validaciones PLV
        If blGenExcesoEnLinea And gcodAdministradora = 1032 Then
            CrearControlRenta()
        End If

        'If rTrn.codOrigenMvto = "RECAUDAC" Then
        '    CrearControlRecaudacionREZ()
        'End If

        'ActualizaRentas()
    End Sub

    Private Sub AcreditaOtrosRecupRez()

        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        'Enviadas
        rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste + _
                             rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste + _
                             rTrn.valMlExcesoLinea

        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto + rTrn.valCuoInteres + rTrn.valCuoReajuste + _
                                 rTrn.valCuoAdicional + rTrn.valCuoAdicionalInteres + rTrn.valCuoAdicionalReajuste + _
                                 rTrn.valCuoExcesoLinea



        gRegistrosEnviados += 1


        'Calculadas
        gRegistrosCalculados += 1

        If rTrn.tipoFondoDestinoCal = "C" Then
            rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
            rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal

            rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
            rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal

        Else
            rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
            rTrn.valMlPatrFrecActCal = Mat.Redondear(rTrn.valCuoPatrFrecCal * gvalMlCuotaDestinoC, 0)

            rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
            rTrn.valCuoPatrFdesCal = Mat.Redondear(rTrn.valMlPatrFdesCal / rTrn.valMlValorCuota, 2)
        End If

        'Acreditadas
        CalcularNominales(rTrn.valMlMvto, _
                          rTrn.valMlInteres, _
                          rTrn.valMlReajuste, _
                          rTrn.valMlAdicional, _
                          rTrn.valMlAdicionalInteres, _
                          rTrn.valMlAdicionalReajuste, _
                          rTrn.valMlExcesoLinea, _
                          rTrn.valMlExcesoEmpl)
     


            TraerControlRentas()
            CalcularExcesos()
            CalcularComisionPorcentual()
        DeterminaMontoInstitucionSalud()

        If (rTrn.codOrigenProceso = "REREZSEL" Or rTrn.codOrigenProceso = "REREZMAS") And gcodAdministradora = 1032 Then
            VerificaAjustesDecimal(gValDif, rTrn.valCuoMvtoCal, rTrn.valCuoAdicionalCal, rTrn.valCuoAdicionalInteresCal, rTrn.valCuoAdicionalReajusteCal, rTrn.valCuoPrimaSisCal, rTrn.valCuoCompensCal)
        End If


        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()
        realizarAbonosExcesos()
        realizarAbonosCargosCompensasion()
        realizarCargoTransfPrimaComision()
        CalcularComisionFija()
        'AplicaVector()
        CalcularSaldo()
        'ActualizaCotTrabajosPesados()

        '--OS:9075964 - nuevas validaciones PLV
        If blGenExcesoEnLinea And gcodAdministradora = 1032 Then
            CrearControlRenta()
        End If

        'If rTrn.codOrigenMvto = "RECAUDAC" Then
        '    CrearControlRecaudacionREZ()
        'End If

        RezagoAHistorico()
        ' ActualizaRentas()

    End Sub

    '''''SIS//
    ''''Private Sub AcreditaOtrosRecupRez2()

    ''''    rTrn.codDestinoTransaccionCal = "CTA"
    ''''    rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
    ''''    CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

    ''''    'Enviadas
    ''''    rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste + _
    ''''                         rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste + _
    ''''                         rTrn.valMlExcesoLinea + _
    ''''                         rTrn.valMlPrimaSis + rTrn.valMlPrimaSisInteres + rTrn.valMlPrimaSisReajuste   'SIS//se añaden

    ''''    rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto + rTrn.valCuoInteres + rTrn.valCuoReajuste + _
    ''''                             rTrn.valCuoAdicional + rTrn.valCuoAdicionalInteres + rTrn.valCuoAdicionalReajuste + _
    ''''                             rTrn.valCuoExcesoLinea + _
    ''''                             rTrn.valCuoPrimaSis + rTrn.valCuoPrimaSisInteres + rTrn.valCuoPrimaSisReajuste  'SIS//se añaden

    ''''    gRegistrosEnviados += 1
    ''''    'Calculadas
    ''''    gRegistrosCalculados += 1

    ''''    If rTrn.tipoFondoDestinoCal = "C" Then
    ''''        rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
    ''''        rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal

    ''''        rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
    ''''        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal

    ''''    Else
    ''''        rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
    ''''        rTrn.valMlPatrFrecActCal = Mat.Redondear(rTrn.valCuoPatrFrecCal * gvalMlCuotaDestinoC, 0)

    ''''        rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
    ''''        rTrn.valCuoPatrFdesCal = Mat.Redondear(rTrn.valMlPatrFdesCal / rTrn.valMlValorCuota, 2)
    ''''    End If

    ''''    'Acreditadas
    ''''    'SIS// CalcularNominales-->>>CalcularNominales2
    ''''    CalcularNominales2(rTrn.valMlMvto, _
    ''''                      rTrn.valMlInteres, _
    ''''                      rTrn.valMlReajuste, _
    ''''                      rTrn.valMlAdicional, _
    ''''                      rTrn.valMlAdicionalInteres, _
    ''''                      rTrn.valMlAdicionalReajuste, _
    ''''                      rTrn.valMlExcesoLinea, _
    ''''                      rTrn.valMlPrimaSis, _
    ''''                      rTrn.valMlPrimaSisInteres, _
    ''''                      rTrn.valMlPrimaSisReajuste)



    ''''    TraerControlRentas()
    ''''    CalcularExcesos()

    ''''    'SIS// CalcularComisionPorcentual-->>>CalcularComisionPorcentual2
    ''''    'CalcularComisionPorcentual()
    ''''    CalcularComisionPorcentual2()

    ''''    DeterminaMontoInstitucionSalud()   ' solo rezagos

    ''''    SumarTotales() '------------------------------rev-sis
    ''''    TrnAMov()      '------------------------------rev-sis
    ''''    CrearSaldosMovimientos()   '------------------rev-sis

    ''''    realizarCargoPrimaSis() '---------------------REV --new items

    ''''    realizarAbonosExcesos() '---------------------REV --new items

    ''''    realizarAbonosCargosCompensasion()

    ''''    'SIS// sin transferencia de prima
    ''''    'realizarCargoTransfPrimaComision()

    ''''    CalcularComisionFija()
    ''''    CalcularSaldo()

    ''''    If gCrearConRen Then
    ''''        CrearControlRenta()
    ''''    End If

    ''''    RezagoAHistorico()
    ''''End Sub


    Private Sub AcreditaCotRecupRez()

        Dim valMlTotNominal As Decimal
        Dim valCuoTotNominal As Decimal

        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)


        If rTrn.fecOperacion >= "01-01-1988" Then
            rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste + _
                            rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste + _
                            rTrn.valMlExcesoLinea
        Else
            rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste + _
                                                                   rTrn.valMlExcesoLinea
        End If


        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto + rTrn.valCuoInteres + rTrn.valCuoReajuste + _
                                 rTrn.valCuoAdicional + rTrn.valCuoAdicionalInteres + rTrn.valCuoAdicionalReajuste + _
                                 rTrn.valCuoExcesoLinea



        gRegistrosEnviados = gRegistrosEnviados + 1




        'Calculadas
        gRegistrosCalculados = gRegistrosCalculados + 1

        If rTrn.tipoFondoDestinoCal = "C" Then

            rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
            rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal

            rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
            rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal

            valMlTotNominal = rTrn.valMlPatrFrecCal
            valCuoTotNominal = rTrn.valCuoPatrFrecCal

        Else

            rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
            rTrn.valMlPatrFrecActCal = Mat.Redondear(rTrn.valCuoPatrFrecCal * gvalMlCuotaDestinoC, 0)

            rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
            rTrn.valCuoPatrFdesCal = Mat.Redondear(rTrn.valMlPatrFdesCal / rTrn.valMlValorCuota, 2)

            valMlTotNominal = rTrn.valMlPatrFrecCal
            valCuoTotNominal = Mat.Redondear(valMlTotNominal / rTrn.valMlValorCuota, 2)
        End If



        CalcularNominalesValorizado(valMlTotNominal, _
                            valCuoTotNominal, _
                            rTrn.valMlMvto, _
                            rTrn.valMlInteres, _
                            rTrn.valMlReajuste, _
                            rTrn.valMlAdicional, _
                            rTrn.valMlAdicionalInteres, _
                            rTrn.valMlAdicionalReajuste, _
                            rTrn.valMlExcesoLinea)


        If blIgnorar Then
            Exit Sub
        End If


        TraerControlRentas()
        CalcularExcesos()
        CalcularComisionPorcentual()
        DeterminaMontoInstitucionSalud()
        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()
        realizarAbonosExcesos()
        realizarAbonosCargosCompensasion()
        realizarCargoTransfPrimaComision()
        CalcularComisionFija()
        'ActualizaCotTrabajosPesados()
        CalcularSaldo()

        '--OS:9075964 - nuevas validaciones PLV
        If blGenExcesoEnLinea And gcodAdministradora = 1032 Then
            CrearControlRenta()
        End If

        'If rTrn.codOrigenMvto = "RECAUDAC" Then
        '    CrearControlRecaudacionREZ()
        'End If

        RezagoAHistorico()


    End Sub


    Private Sub AcreditaCotAjuste()

        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)


        'Enviadas
        rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste + _
                                rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste + _
                                rTrn.valMlExcesoLinea

        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto + rTrn.valCuoInteres + rTrn.valCuoReajuste + _
                                 rTrn.valCuoAdicional + rTrn.valCuoAdicionalInteres + rTrn.valCuoAdicionalReajuste + _
                                 rTrn.valCuoExcesoLinea

        gRegistrosEnviados = gRegistrosEnviados + 1


        'Calculadas
        gRegistrosCalculados = gRegistrosCalculados + 1

        rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
        rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal

        rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal

        rTrn.valMlMvtoCal = rTrn.valMlMvto
        rTrn.valMlInteresCal = rTrn.valMlInteres
        rTrn.valMlReajusteCal = rTrn.valMlReajuste
        rTrn.valMlAdicionalCal = rTrn.valMlAdicional
        rTrn.valMlAdicionalInteresCal = rTrn.valMlAdicionalInteres
        rTrn.valMlAdicionalReajusteCal = rTrn.valMlAdicionalReajuste

        rTrn.valMlComisFijaCal = rTrn.valMlComisFija
        rTrn.valMlComisPorcentualCal = rTrn.valMlComisPorcentual

        '06/01/2012 No se estaban considerando los excesos para las cotizaciones antiguas.
        'rTrn.valMlExcesoLinea = rTrn.valMlExcesoLinea
        rTrn.valMlExcesoTopeCal = rTrn.valMlExcesoLinea

        rTrn.valCuoMvtoCal = rTrn.valCuoMvto
        rTrn.valCuoInteresCal = rTrn.valCuoInteres
        rTrn.valCuoReajusteCal = rTrn.valCuoReajuste
        rTrn.valCuoAdicionalCal = rTrn.valCuoAdicional
        rTrn.valCuoAdicionalInteresCal = rTrn.valCuoAdicionalInteres
        rTrn.valCuoAdicionalReajusteCal = rTrn.valCuoAdicionalReajuste

        rTrn.valCuoComisFijaCal = rTrn.valCuoComisFija
        rTrn.valCuoComisPorcentualCal = rTrn.valCuoComisPorcentual

        '06/01/2012 No se estaban considerando los excesos para las cotizaciones antiguas.
        'rTrn.valCuoExcesoLinea = rTrn.valCuoExcesoLinea
        rTrn.valCuoExcesoTopeCal = rTrn.valCuoExcesoLinea

        TraerControlRentas()
        'En este caso la determinacion de exceso calculado provoca el recálculo de los valores informados en cuota

        'CalcularExcesos()
        DeterminaPunterosPlanilla()
        CalcularComisionPorcentual()
        DeterminaMontoInstitucionSalud()
        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()
        realizarAbonosExcesos()
        realizarAbonosCargosCompensasion()
        realizarCargoTransfPrimaComision()
        CalcularComisionFija()
        'AplicaVector()
        ActualizaMovimientos()
        CalcularSaldo()
        'ActualizaCotTrabajosPesados()

        If gCrearConRen Then
            rConRen.valMlRentaAcum = rTrn.valMlRentaImponible
            'rConRen.valUfRentaAcum = Mat.Redondear(rConRen.valMlRentaAcum / gvalorUF, 2)

            'Controla Error por Division por 0. PCI 23/01/2012
            If gvalorUF = 0 Then
                blIgnorar = True
                rTrn.codError = 7440 'Valor UF en CERO.
                GenerarLog("A", 7440, "1.- Valor UF en CERO. Producto: " & rTrn.tipoProducto & ", Per Cotizacion:" & rTrn.perCotizacion, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                Exit Sub
            Else
                rConRen.valUfRentaAcum = Mat.Redondear(rConRen.valMlRentaAcum / gvalorUF, 2)
            End If

            '--OS:9075964 - nuevas validaciones PLV
            If blGenExcesoEnLinea And gcodAdministradora = 1032 Then
                CrearControlRenta()
            End If

        End If

    End Sub

    Private Sub AcreditaCotAjusteSIS()

        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        'Enviadas
        rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste + _
                                rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste + _
                                rTrn.valMlPrimaSis + rTrn.valMlPrimaSisInteres + rTrn.valMlPrimaSisReajuste + _
                                rTrn.valMlExcesoLinea

        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto + rTrn.valCuoInteres + rTrn.valCuoReajuste + _
                                 rTrn.valCuoAdicional + rTrn.valCuoAdicionalInteres + rTrn.valCuoAdicionalReajuste + _
                                 rTrn.valCuoPrimaSis + rTrn.valCuoPrimaSisInteres + rTrn.valCuoPrimaSisReajuste + _
                                 rTrn.valCuoExcesoLinea

        gRegistrosEnviados = gRegistrosEnviados + 1


        'Calculadas
        gRegistrosCalculados = gRegistrosCalculados + 1

        rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
        rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal

        rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal

        rTrn.valMlMvtoCal = rTrn.valMlMvto
        rTrn.valMlInteresCal = rTrn.valMlInteres
        rTrn.valMlReajusteCal = rTrn.valMlReajuste
        rTrn.valMlAdicionalCal = rTrn.valMlAdicional
        rTrn.valMlAdicionalInteresCal = rTrn.valMlAdicionalInteres
        rTrn.valMlAdicionalReajusteCal = rTrn.valMlAdicionalReajuste

        rTrn.valMlComisFijaCal = rTrn.valMlComisFija
        rTrn.valMlComisPorcentualCal = rTrn.valMlComisPorcentual

        'rTrn.valMlExcesoLinea = rTrn.valMlExcesoLinea
        rTrn.valMlExcesoTopeCal = rTrn.valMlExcesoLinea


        rTrn.valCuoMvtoCal = rTrn.valCuoMvto
        rTrn.valCuoInteresCal = rTrn.valCuoInteres
        rTrn.valCuoReajusteCal = rTrn.valCuoReajuste
        rTrn.valCuoAdicionalCal = rTrn.valCuoAdicional
        rTrn.valCuoAdicionalInteresCal = rTrn.valCuoAdicionalInteres
        rTrn.valCuoAdicionalReajusteCal = rTrn.valCuoAdicionalReajuste

        rTrn.valCuoComisFijaCal = rTrn.valCuoComisFija
        rTrn.valCuoComisPorcentualCal = rTrn.valCuoComisPorcentual

        'rTrn.valCuoExcesoLinea = rTrn.valCuoExcesoLinea
        rTrn.valCuoExcesoTopeCal = rTrn.valCuoExcesoLinea

        'prima SIS -------------------------------------------------------------
        rTrn.valMlPrimaSisCal = rTrn.valMlPrimaSis
        rTrn.valMlPrimaSisInteresCal = rTrn.valMlPrimaSisInteres
        rTrn.valMlPrimaSisReajusteCal = rTrn.valMlPrimaSisReajuste
        rTrn.valCuoPrimaSisCal = rTrn.valCuoPrimaSis
        rTrn.valCuoPrimaSisInteresCal = rTrn.valCuoPrimaSisInteres
        rTrn.valCuoPrimaSisReajusteCal = rTrn.valCuoPrimaSisReajuste



        TraerControlRentas()
        
        'CalcularExcesos()
        DeterminaPunterosPlanilla()


        CalcularComisionPorcentual2() '---------------rev

        DeterminaMontoInstitucionSalud()

        SumarTotales()

        TrnAMov()

        CrearSaldosMovimientos()

        realizarCargoPrimaSis() '---------------------REV --new items

        realizarAbonosExcesos()
        realizarAbonosCargosCompensasion()

        'PCI
        'Genera Linea de Comision en ACR_SALDOS_MOVIMIENTOS cuando es COm. Descoord. 17/01/2012
        realizarCargoTransfPrimaComision()

        'PCI
        'Genera Linea de Comision en ACR_SALDOS_MOVIMIENTOS cuando es COm. Descoord. 09/04/2013
        realizarCargoTransfComision()

        CalcularComisionFija()
        'AplicaVector()
        ActualizaMovimientos()

        CalcularSaldo()
        'ActualizaCotTrabajosPesados()

        If gCrearConRen Then
            rConRen.valMlRentaAcum = rTrn.valMlRentaImponible
            'rConRen.valUfRentaAcum = Mat.Redondear(rConRen.valMlRentaAcum / gvalorUF, 2)

            'Controla Error por Division por 0. PCI 23/01/2012
            If gvalorUF = 0 Then
                blIgnorar = True
                rTrn.codError = 7440 'Valor UF en CERO.
                GenerarLog("A", 7440, "2.- Valor UF en CERO. Producto: " & rTrn.tipoProducto & ", Per Cotizacion:" & rTrn.perCotizacion, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                Exit Sub
            Else
                rConRen.valUfRentaAcum = Mat.Redondear(rConRen.valMlRentaAcum / gvalorUF, 2)
            End If

            '--OS:9075964 - nuevas validaciones PLV
            If blGenExcesoEnLinea And gcodAdministradora = 1032 Then
                CrearControlRenta()
            End If

        End If

    End Sub





    Private Sub AcreditaOtrosMovs()
        Dim iImputacion As String

        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)


        'Enviadas
        rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste + _
                                rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste + _
                                rTrn.valMlExcesoLinea

        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto + rTrn.valCuoInteres + rTrn.valCuoReajuste + _
                                 rTrn.valCuoAdicional + rTrn.valCuoAdicionalInteres + rTrn.valCuoAdicionalReajuste + _
                                 rTrn.valCuoExcesoLinea

        gRegistrosEnviados = gRegistrosEnviados + 1


        'Calculadas
        gRegistrosCalculados = gRegistrosCalculados + 1

        rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
        rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal

        rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal

        rTrn.valMlMvtoCal = rTrn.valMlMvto
        rTrn.valMlInteresCal = rTrn.valMlInteres
        rTrn.valMlReajusteCal = rTrn.valMlReajuste
        rTrn.valMlAdicionalCal = rTrn.valMlAdicional
        rTrn.valMlAdicionalInteresCal = rTrn.valMlAdicionalInteres
        rTrn.valMlAdicionalReajusteCal = rTrn.valMlAdicionalReajuste

        rTrn.valMlComisFijaCal = rTrn.valMlComisFija
        rTrn.valMlComisPorcentualCal = rTrn.valMlComisPorcentual


        rTrn.valMlExcesoLinea = rTrn.valMlExcesoLinea


        rTrn.valCuoMvtoCal = rTrn.valCuoMvto
        rTrn.valCuoInteresCal = rTrn.valCuoInteres
        rTrn.valCuoReajusteCal = rTrn.valCuoReajuste
        rTrn.valCuoAdicionalCal = rTrn.valCuoAdicional
        rTrn.valCuoAdicionalInteresCal = rTrn.valCuoAdicionalInteres
        rTrn.valCuoAdicionalReajusteCal = rTrn.valCuoAdicionalReajuste

        rTrn.valCuoComisFijaCal = rTrn.valCuoComisFijaCal
        rTrn.valCuoComisPorcentualCal = rTrn.valCuoComisPorcentualCal

        rTrn.valCuoExcesoLinea = rTrn.valCuoExcesoLinea

        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()

        'AplicaVector()
        ActualizaMovimientos()
        CalcularSaldo()

    End Sub

    'rutina para Ajuste de Prima y Rev Prima------------------------------lfc://04-11-2009
    Private Sub AcreditaPrimas()
        Dim iImputacion As String


        'No se permiten Ajustes de Prima o Reversa de Prima con Periodos de Cotizacion menor a Jul/2009.OS-5835345 13/02/2014.
        If Not (rTrn.perCotizacion >= New Date(2009, 7, 1).Date And _
           (rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF")) Then

            blIgnorar = True
            rTrn.codError = 20518 'Valor de Campo PER_COTIZACION es inválido
            GenerarLog("A", 20518, "Hebra " & gIdHebra & " - Periodo Cotizacion < 07/2009 ", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

            Exit Sub
        End If

        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)


        'Enviadas
        rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste + _
                                rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste + _
                                rTrn.valMlExcesoLinea

        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto + rTrn.valCuoInteres + rTrn.valCuoReajuste + _
                                 rTrn.valCuoAdicional + rTrn.valCuoAdicionalInteres + rTrn.valCuoAdicionalReajuste + _
                                 rTrn.valCuoExcesoLinea

        gRegistrosEnviados = gRegistrosEnviados + 1


        'Calculadas
        gRegistrosCalculados = gRegistrosCalculados + 1

        rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
        rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal

        rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal

        rTrn.valMlMvtoCal = rTrn.valMlMvto
        rTrn.valMlInteresCal = rTrn.valMlInteres
        rTrn.valMlReajusteCal = rTrn.valMlReajuste
        rTrn.valMlAdicionalCal = rTrn.valMlAdicional
        rTrn.valMlAdicionalInteresCal = rTrn.valMlAdicionalInteres
        rTrn.valMlAdicionalReajusteCal = rTrn.valMlAdicionalReajuste

        rTrn.valMlComisFijaCal = rTrn.valMlComisFija
        rTrn.valMlComisPorcentualCal = rTrn.valMlComisPorcentual


        rTrn.valMlExcesoLinea = rTrn.valMlExcesoLinea


        rTrn.valCuoMvtoCal = rTrn.valCuoMvto
        rTrn.valCuoInteresCal = rTrn.valCuoInteres
        rTrn.valCuoReajusteCal = rTrn.valCuoReajuste
        rTrn.valCuoAdicionalCal = rTrn.valCuoAdicional
        rTrn.valCuoAdicionalInteresCal = rTrn.valCuoAdicionalInteres
        rTrn.valCuoAdicionalReajusteCal = rTrn.valCuoAdicionalReajuste

        rTrn.valCuoComisFijaCal = rTrn.valCuoComisFijaCal
        rTrn.valCuoComisPorcentualCal = rTrn.valCuoComisPorcentualCal

        rTrn.valCuoExcesoLinea = rTrn.valCuoExcesoLinea

        If rTrn.valCuoMvtoCal > 0 Or rTrn.valMlMvtoCal > 0 Then

            Dim gValMlPrima, gValCuoPrima As Decimal
            Dim gInstitucion As Integer

            rTrn.sexo = IIf(rTrn.sexo Is Nothing, rCli.sexo, rTrn.sexo)
            INEPrimasSeguros.DeterminarMontoPrimaSIS(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoCliente, rCli.codAdmOrigen, rCli.codAdmDestino, gcodAdministradora, rTrn.perCotizacion, rTrn.tipoProducto, rTrn.valMlValorCuota, rTrn.valMlRentaImponibleSis, rTrn.sexo, gValMlPrima, gValCuoPrima, rTrn.valIndPagoPrimCal, gInstitucion, rTrn.tasaPrimaCal, gcodErrorIgnorar)

            rTrn.valIdInstPagoPrimCal = gcodAdministradora
            rPri.codInstFinanciera = gcodAdministradora
            rPri.codOrigenProceso = rTrn.codOrigenProceso
            rPri.fecOperacion = rTrn.fecAcreditacion
            rPri.idAdmCobroAdicional = gcodAdministradora
            rPri.idEmpleador = rTrn.idEmpleador
            rPri.idPersona = rTrn.idPersona
            rPri.indDerechoSeguro = "S"
            rPri.perCotiza = rTrn.perCotizacion
            rPri.perProceso = rTrn.perContable
            rPri.porcPrimaSeguro = Mat.Redondear(rTrn.tasaPrimaCal * 100, 2)
            rPri.seqMovimiento = rTrn.seqRegistro
            rPri.tipoFondo = rTrn.tipoFondoDestinoCal
            rPri.tipoPago = rTrn.tipoPago
            rPri.tipoTrabajador = rTrn.tipoCliente
            rPri.valCuoCco = rTrn.valCuoMvtoCal
            rPri.valMlAdicional = 0
            rPri.valMlAdicionalInteres = 0
            rPri.valMlAdicionalReajuste = 0
            rPri.valMlCco = rTrn.valMlMvtoCal
            rPri.valMlComisionFija = 0
            rPri.valMlPrimaSeguro = rTrn.valMlMvtoCal
            rPri.valMlRentaImponible = rTrn.valMlRentaImponibleSis

            rPri.porcAdicional = 0 'Mat.Redondear(rTrn.tasaAdicional * 100, 2)

            'SIS//
            rPri.sexo = rTrn.sexo
            rPri.fecAcreditacion = rTrn.fecAcreditacion
            rPri.valMlPrimaInteres = 0
            rPri.valCuoPrimaInteres = 0
            rPri.valCuoPrimaReajuste = 0
            rPri.valMlPrimaReajuste = 0


            If (rTrn.tipoProducto = "CAF" Or rTrn.tipoProducto = "CCO") And _
            (rMovAcr.tipoMvto = "PRI" Or rMovAcr.tipoMvto = "RPRI" Or rMovAcr.tipoMvto = "EXC") And _
            (gcodOrigenProceso = "AJUMASIV" Or gcodOrigenProceso = "AJUSELEC" Or gcodOrigenProceso = "GEXCESOS") Then

                If rMovAcr.tipoMvto = "PRI" Then iImputacion = "ABO"
                If rMovAcr.tipoMvto = "RPRI" Then iImputacion = "CAR"
                If rTrn.codMvto = "111852" Then iImputacion = "CAR" 'Reversa Prima Por GEXCESOS

                If gtipoProceso = "AC" Then
                    PrimasCiasSeguro.crear(gdbc, gidAdm, rTrn.tipoFondoDestinoCal, rTrn.perContable, _
                       rPri.codInstFinanciera, _
                       rTrn.idPersona, rTrn.seqRegistro, _
                       iImputacion, _
                       rTrn.perCotizacion, rTrn.tipoCliente, "S", _
                       rTrn.codOrigenProceso, rTrn.fecOperacion, _
                       rPri.tipoPago, _
                       rTrn.valMlMvtoCal, rTrn.valCuoMvtoCal, rTrn.valMlComisFija, rTrn.valMlRentaImponible, _
                       rPri.valMlAdicional, _
                       rPri.valMlAdicionalInteres, _
                       rPri.valMlAdicionalReajuste, _
                       rPri.valMlPrimaSeguro, _
                       rPri.idAdmCobroAdicional, _
                       rPri.codMvto, _
                       rPri.porcPrimaSeguro, _
                       rPri.porcAdicional, _
                       rTrn.idEmpleador, gidUsuarioProceso, gfuncion, _
                       rPri.sexo, _
                       rPri.fecAcreditacion, _
                       rPri.valMlPrimaInteres, _
                       rPri.valMlPrimaReajuste, _
                       rPri.valCuoPrimaSeguro, _
                       rPri.valCuoPrimaInteres, _
                       rPri.valCuoPrimaReajuste)

                End If
            End If
        End If

        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()
        'AplicaVector()
        ActualizaMovimientos()
        CalcularSaldo()
    End Sub



    Private Sub AcreditaCotCargo()

        rTrn.codDestinoTransaccionCal = "CTA"
        rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
        CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

        rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste + _
                                rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste
        gRegistrosEnviados = gRegistrosEnviados + 1


        rTrn.valCuoPatrFrecCal = rTrn.valCuoMvto + rTrn.valCuoInteres + rTrn.valCuoReajuste + _
                                rTrn.valCuoAdicional + rTrn.valCuoAdicionalInteres + rTrn.valCuoAdicionalReajuste


        rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
        rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal

        rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecCal
        rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
        gRegistrosCalculados = gRegistrosCalculados + 1


        rTrn.valMlMvtoCal = rTrn.valMlMvto
        rTrn.valCuoMvtoCal = rTrn.valCuoMvto

        rTrn.valMlInteresCal = rTrn.valMlInteres
        rTrn.valCuoInteresCal = rTrn.valCuoInteres

        rTrn.valMlReajusteCal = rTrn.valMlReajuste
        rTrn.valCuoReajusteCal = rTrn.valCuoReajuste

        rTrn.valMlAdicionalCal = rTrn.valMlAdicional
        rTrn.valCuoAdicionalCal = rTrn.valCuoAdicional

        rTrn.valMlAdicionalInteresCal = rTrn.valMlAdicionalInteres
        rTrn.valCuoAdicionalInteresCal = rTrn.valCuoAdicionalInteres

        rTrn.valMlAdicionalReajusteCal = rTrn.valMlAdicionalReajuste
        rTrn.valCuoAdicionalReajusteCal = rTrn.valCuoAdicionalReajuste

        CargoControlRenta()
        SumarTotales()
        TrnAMov()
        CrearSaldosMovimientos()


        'AplicaVector()
        CalcularSaldo()


    End Sub


    Private Sub CrearCuotaAcred(ByVal tipoFondo As String, ByVal valMlCuota As Decimal)
        Dim ds As DataSet

        If gtipoProceso = "AC" Then

            ds = ResultadoAcred.CuotasAcred.traer(gdbc,gidAdm, gnumeroId, 0, tipoFondo)
            If ds.Tables(0).Rows.Count > 0 Then
                If ds.Tables(0).Rows(0).Item("VAL_ML_VALOR_CUOTA") <> valMlCuota Then
                    Throw New SondaException(15015) ' Los parametros utilizados en la simulacion difieren de los de la acreditación
                End If
            End If

        End If

        ResultadoAcred.CuotasAcred.crear(gdbc,gidAdm, gnumeroId, gseqProceso, tipoFondo, valMlCuota, gidUsuarioProceso, gfuncion)

    End Sub
    Private Sub CrearIngresoRezagos()
        Dim perProceso As Date
        Dim indApertura As String
        Dim valMlGanancia, valCuoGanancia, valMlPerdida, valCuoPerdida As Decimal
        Dim seqTraspaso As Long

        perProceso = rTrn.perContable
        seqTraspaso = rTrn.numReferenciaOrigen1
        If IsNothing(rMovAcr.codMvtoAdi) Then
            indApertura = 1
        Else
            indApertura = 2
        End If
        If rTrn.valMlCompensCal > 0 Then
            valMlGanancia = rTrn.valMlCompensCal
            valCuoGanancia = rTrn.valCuoCompensCal
        Else
            valMlPerdida = Decimal.Negate(rTrn.valMlCompensCal)
            valCuoPerdida = Decimal.Negate(rTrn.valCuoCompensCal)
        End If
        If gcodOrigenProceso = "TRAINREZ" Or gcodOrigenProceso = "TRAINRZC" Or gcodOrigenProceso = "TRAIPAGN" Then

            Sys.IngresoEgreso.Cotizaciones.PagoDirecto.crearIngresoRezago(gdbc,gidAdm, _
                                                                          rTrn.numeroId, _
                                                                          seqTraspaso, _
                                                                          perProceso, _
                                                                          rTrn.idInstOrigen, _
                                                                          rTrn.codOrigenProceso, _
                                                                          rTrn.idCliente, _
                                                                          rTrn.valMlMontoNominal, _
                                rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste + rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste + rTrn.valMlPrimaSis + rTrn.valMlPrimaSisInteres + rTrn.valMlPrimaSisReajuste + rTrn.valMlExcesoLinea, _
                                                                          rTrn.valMlRentaImponible, _
                                                                          rTrn.tipoPago, _
                                                                          rTrn.tipoProducto, _
                                                                          indApertura, _
                                                                          rTrn.tasaCotizacion, _
                                                                          rTrn.tasaAdicional, _
                                                                          rTrn.valMlValorCuotaCaja, _
                                                                          rTrn.valMlValorCuota, _
                                                                          rTrn.tipoFondoDestinoCal, _
                                                                          rTrn.valMlValorCuota, _
                                                                          rTrn.valMlPatrFdesCal, _
                                                                          rTrn.valCuoPatrFrecCal, _
                                                                          rTrn.valCuoPatrFdesCal, _
                                                                          rTrn.valMlMvtoCal + rTrn.valMlInteresCal + rTrn.valMlReajusteCal + rTrn.valMlExcesoTopeCal, _
                                                                           rTrn.valMlAdicionalCal + rTrn.valMlAdicionalInteresCal + rTrn.valMlAdicionalReajusteCal + rTrn.valMlPrimaSisCal + rTrn.valMlPrimaSisInteresCal + rTrn.valMlPrimaSisReajusteCal, _
                                                                           rTrn.valCuoMvtoCal + rTrn.valCuoInteresCal + rTrn.valCuoReajusteCal + rTrn.valCuoExcesoTopeCal, _
                                                                           rTrn.valCuoAdicionalCal + rTrn.valCuoAdicionalInteresCal + rTrn.valCuoAdicionalReajusteCal + rTrn.valCuoPrimaSisCal + rTrn.valCuoPrimaSisInteresCal + rTrn.valCuoPrimaSisReajusteCal, _
                                                                          valMlGanancia, _
                                                                          valCuoGanancia, _
                                                                          valMlPerdida, _
                                                                          valCuoPerdida, _
                                                                          rTrn.valCuoAjusteDecimalCal, _
                                                                          rTrn.codDestinoTransaccionCal, _
                                                                          0, _
                                                                          gfecAcreditacion, gtipoProceso, gtipoProceso, gidUsuarioProceso, gfuncion)
        End If

    End Sub
    Private Sub EliminarIngresoRezagos()

        If gcodOrigenProceso = "TRAINREZ" Or gcodOrigenProceso = "TRAINRZC" Or gcodOrigenProceso = "TRAIPAGN" Then
            Sys.IngresoEgreso.Cotizaciones.PagoDirecto.eliminaProceso(gdbc,gidAdm, gnumeroId)
        End If

    End Sub
    Private Sub SumarTotales()


        gvalMlMvto = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste
        gvalMlAdicional = rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste

        'SIS//
        gvalMlPrimaSis = rTrn.valMlPrimaSis + rTrn.valMlPrimaSisInteres + rTrn.valMlPrimaSisReajuste

        gvalMlExceso = rTrn.valMlExcesoLinea

        'OS-5598016 Exceso Empleador
        gvalMlExcesoEmpl = rTrn.valMlExcesoEmpl

        gvalMlComisiones = rTrn.valMlComisFija + rTrn.valMlComisPorcentual

        gvalCuoMvto = rTrn.valCuoMvto + rTrn.valCuoInteres + rTrn.valCuoReajuste
        gvalCuoAdicional = rTrn.valCuoAdicional + rTrn.valCuoAdicionalInteres + rTrn.valCuoAdicionalReajuste

        'SIS//
        gvalCuoPrimaSis = rTrn.valCuoPrimaSis + rTrn.valCuoPrimaSisInteres + rTrn.valCuoPrimaSisReajuste

        gvalCuoExceso = rTrn.valCuoExcesoLinea

        'OS-5598016 Exceso Empleador
        gvalCuoExcesoEmpl = rTrn.valCuoExcesoEmpl

        gvalCuoComisiones = rTrn.valCuoComisFija + rTrn.valCuoComisPorcentual


        gvalMlMvtoCal = rTrn.valMlMvtoCal + rTrn.valMlInteresCal + rTrn.valMlReajusteCal
        gvalMlAdicionalCal = rTrn.valMlAdicionalCal + rTrn.valMlAdicionalInteresCal + rTrn.valMlAdicionalReajusteCal

        'SIS//
        gvalMlPrimaSisCal = rTrn.valMlPrimaSisCal + rTrn.valMlPrimaSisInteresCal + rTrn.valMlPrimaSisReajusteCal

        gvalMlExcesoCal = rTrn.valMlExcesoTopeCal

        'OS-5598016 Exceso Empleador
        gvalMlExcesoEmplCal = rTrn.valMlExcesoEmplCal

        gvalMlComisionesCal = rTrn.valMlComisFijaCal + rTrn.valMlComisPorcentualCal

        gvalCuoMvtoCal = rTrn.valCuoMvtoCal + rTrn.valCuoInteresCal + rTrn.valCuoReajusteCal
        gvalCuoAdicionalCal = rTrn.valCuoAdicionalCal + rTrn.valCuoAdicionalInteresCal + rTrn.valCuoAdicionalReajusteCal

        'SIS//
        gvalCuoPrimaSisCal = rTrn.valCuoPrimaSisCal + rTrn.valCuoPrimaSisInteresCal + rTrn.valCuoPrimaSisReajusteCal

        gvalCuoExcesoCal = rTrn.valCuoExcesoTopeCal

        'OS-5598016 Exceso Empleador
        gvalCuoExcesoEmplCal = rTrn.valCuoExcesoEmplCal

        gvalCuoComisionesCal = rTrn.valCuoComisFijaCal + rTrn.valCuoComisPorcentualCal

        gvalMlPatrFrecCal = rTrn.valMlPatrFrecCal
        gvalMlPatrFrecActCal = rTrn.valMlPatrFrecActCal

        'SIS//
        'If rTrn.perCotizacion >= New Date(2009, 7, 1).Date And (rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF") Then
        gvalMlPrimaCal = rTrn.valMlPrimaSisCal + rTrn.valMlPrimaSisInteresCal + rTrn.valMlPrimaSisReajusteCal
        'Else
        '    gvalMlPrimaCal = rTrn.valMlPrimaCal + rTrn.valMlIntPrimaCal + rTrn.valMlReaPrimaCal
        ' End If

        gvalCuoPatrFrecCal = rTrn.valCuoPatrFrecCal
        gvalCuoPatrFrecActCal = rTrn.valCuoPatrFrecActCal
        gvalCuoPrimaCal = rTrn.valCuoPrimaCal

        gvalMlPatrFdesCal = rTrn.valMlPatrFdesCal
        gvalCuoPatrFdesCal = rTrn.valCuoPatrFdesCal

        gvalMlTransferenciaCal = rTrn.valMlTransferenciaCal
        gvalCuoTransferenciaCal = rTrn.valCuoTransferenciaCal


        gvalCuoAjuDecCal = rTrn.valCuoAjusteDecimalCal
        If gvalCuoAjuDecCal <> 0 Then
            gRegistrosAjustes = 1
        End If

        If rTrn.valMlCompensCal > 0 Then
            gRegistrosCompen = gRegistrosCompen + 1
            gvalMlCompenAboCal = rTrn.valMlCompensCal
            gvalCuoCompenAboCal = rTrn.valCuoCompensCal
        Else
            gRegistrosCompen = gRegistrosCompen + 1
            gvalMlCompenCarCal = rTrn.valMlCompensCal * -1
            gvalCuoCompenCarCal = rTrn.valCuoCompensCal * -1
        End If


        If rTrn.tipoImputacion = "ABO" Then

            'SIS//
            If rTrn.perCotizacion >= New Date(2009, 7, 1).Date And (rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF") Then
                'gvalMlAbonosCtaCal = gvalMlMvtoCal + gvalMlAdicionalCal + gvalMlExcesoCal + gvalMlPrimaSisCal
                'gvalCuoAbonosCtaCal = gvalCuoMvtoCal + gvalCuoAdicionalCal + gvalCuoExcesoCal + gvalCuoAjuDecCal + gvalCuoPrimaSisCal

                'OS-5598016 Exceso Empleador
                gvalMlAbonosCtaCal = gvalMlMvtoCal + gvalMlAdicionalCal + gvalMlExcesoCal + gvalMlPrimaSisCal + gvalMlExcesoEmplCal
                gvalCuoAbonosCtaCal = gvalCuoMvtoCal + gvalCuoAdicionalCal + gvalCuoExcesoCal + gvalCuoAjuDecCal + gvalCuoPrimaSisCal + gvalCuoExcesoEmplCal


                gvalCuoCargosCtaCal = gvalCuoComisionesCal + gvalCuoPrimaCal ' DECOMENTAR + gvalCuoPrimaSisCal
                gvalMlCargosCtaCal = gvalMlComisionesCal + gvalMlPrimaCal 'DESCOMENTAR + gvalMlPrimaSisCal
            Else
                'gvalMlAbonosCtaCal = gvalMlMvtoCal + gvalMlAdicionalCal + gvalMlExcesoCal
                'gvalCuoAbonosCtaCal = gvalCuoMvtoCal + gvalCuoAdicionalCal + gvalCuoExcesoCal + gvalCuoAjuDecCal

                'OS-5598016 Exceso Empleador
                gvalMlAbonosCtaCal = gvalMlMvtoCal + gvalMlAdicionalCal + gvalMlExcesoCal + gvalMlExcesoEmplCal
                gvalCuoAbonosCtaCal = gvalCuoMvtoCal + gvalCuoAdicionalCal + gvalCuoExcesoCal + gvalCuoAjuDecCal + gvalCuoExcesoEmplCal

                gvalCuoCargosCtaCal = gvalCuoComisionesCal + gvalCuoPrimaCal
                gvalMlCargosCtaCal = gvalMlComisionesCal + gvalMlPrimaCal
            End If

        Else

            'OS-5598016 Exceso Empleador
            'gvalMlCargosCtaCal = gvalMlMvtoCal + gvalMlAdicionalCal + gvalMlExcesoCal + gvalMlComisionesCal
            'gvalCuoCargosCtaCal = gvalCuoMvtoCal + gvalCuoAdicionalCal + gvalCuoExcesoCal + gvalCuoComisionesCal

            gvalMlCargosCtaCal = gvalMlMvtoCal + gvalMlAdicionalCal + gvalMlExcesoCal + gvalMlComisionesCal + gvalMlExcesoEmplCal
            gvalCuoCargosCtaCal = gvalCuoMvtoCal + gvalCuoAdicionalCal + gvalCuoExcesoCal + gvalCuoComisionesCal + gvalCuoExcesoEmplCal

        End If

        If gvalMlCompenAboCal > 0 Then
            gvalMlAbonosCtaCal += gvalMlCompenAboCal
            gvalCuoAbonosCtaCal += gvalCuoCompenAboCal
        End If

        If gvalMlCompenCarCal > 0 Then
            gvalMlCargosCtaCal += gvalMlCompenCarCal
            gvalCuoCargosCtaCal += gvalCuoCompenCarCal
        End If

    End Sub

    Private Sub SumarTotalesAuxiliar()

        Dim mlAbonos, mlCargos, cuoAbonos, cuoCargos As Decimal

        If gtipoProceso <> "AC" Then
            Exit Sub
        End If


        '-----------------------------------------------------
        'Cuentas personales
        '-----------------------------------------------------

        If rTrn.codDestinoTransaccionCal = "CTA" Then

            If rTrn.tipoImputacion = "ABO" Then

                mlAbonos = rTrn.valMlMvtoCal + rTrn.valMlInteresCal + rTrn.valMlReajusteCal + _
                           rTrn.valMlAdicionalCal + rTrn.valMlAdicionalInteresCal + rTrn.valMlAdicionalReajusteCal + _
                           rTrn.valMlExcesoTopeCal + _
                       rTrn.valMlPrimaSisCal + rTrn.valMlPrimaSisInteresCal + rTrn.valMlPrimaSisReajusteCal  'SIS//


                cuoAbonos = rTrn.valCuoMvtoCal + rTrn.valCuoInteresCal + rTrn.valCuoReajusteCal + _
                            rTrn.valCuoAdicionalCal + rTrn.valCuoAdicionalInteresCal + rTrn.valCuoAdicionalReajusteCal + _
                            rTrn.valCuoExcesoTopeCal + _
                        rTrn.valCuoPrimaSisCal + rTrn.valCuoPrimaSisInteresCal + rTrn.valCuoPrimaSisReajusteCal 'SIS//

                mlCargos = rTrn.valMlComisFijaCal + rTrn.valMlComisPorcentualCal
                cuoCargos = rTrn.valCuoComisFijaCal + rTrn.valCuoComisPorcentualCal

                If rTrn.valIndPagoPrimCal = "A" Or rTrn.valIndPagoPrimCal = "N" Or rTrn.valIndPagoPrimCal = "S" Then
                    mlCargos += rTrn.valMlPrimaCal
                    cuoCargos += rTrn.valCuoPrimaCal
                End If

                'DESCOMENTAR, LOS CARGOS POR COMISION YA EXISTEN 
                'If gAdicionalSeTransfiere And (rTrn.codMvtoComPor = "120703" Or rTrn.codMvtoComPor = "620703") Then ' solo esta condicion new
                '    mlCargos += rTrn.valMlComisPorcentualCal
                '    cuoCargos += rTrn.valCuoComisPorcentualCal
                'End If



                If rTrn.valMlCompensCal > 0 Then

                    mlAbonos += rTrn.valMlCompensCal
                    cuoAbonos += rTrn.valCuoCompensCal

                ElseIf rTrn.valMlCompensCal < 0 Then

                    mlCargos += Decimal.Negate(rTrn.valMlCompensCal)
                    cuoCargos += Decimal.Negate(rTrn.valCuoCompensCal)

                End If


            Else 'cargos
                mlAbonos = 0
                cuoAbonos = 0
                mlCargos = rTrn.valMlMvtoCal + rTrn.valMlInteresCal + rTrn.valMlReajusteCal + _
                           rTrn.valMlAdicionalCal + rTrn.valMlAdicionalInteresCal + rTrn.valMlAdicionalReajusteCal + _
                           rTrn.valMlExcesoTopeCal + _
                           rTrn.valMlComisFijaCal + rTrn.valMlComisPorcentualCal + _
                       rTrn.valMlPrimaSisCal + rTrn.valMlPrimaSisInteresCal + rTrn.valMlPrimaSisReajusteCal  'SIS//

                cuoCargos = rTrn.valCuoMvtoCal + rTrn.valCuoInteresCal + rTrn.valCuoReajusteCal + _
                            rTrn.valCuoAdicionalCal + rTrn.valCuoAdicionalInteresCal + rTrn.valCuoAdicionalReajusteCal + _
                            rTrn.valCuoExcesoTopeCal + _
                            rTrn.valCuoComisFijaCal + rTrn.valCuoComisPorcentualCal + _
                       rTrn.valCuoPrimaSisCal + rTrn.valCuoPrimaSisInteresCal + rTrn.valCuoPrimaSisReajusteCal  'SIS//


            End If
            clsAux.Add(gidAdm, "SCUP", rTrn.tipoProducto, rTrn.tipoFondoDestinoCal, mlAbonos, cuoAbonos, mlCargos, cuoCargos)

        End If
        '-----------------------------------------------------
        'Rezagos
        '-----------------------------------------------------
        If rTrn.codDestinoTransaccionCal = "REZ" Then

            mlAbonos = rTrn.valMlMvtoCal + rTrn.valMlInteresCal + rTrn.valMlReajusteCal + _
                          rTrn.valMlAdicionalCal + rTrn.valMlAdicionalInteresCal + rTrn.valMlAdicionalReajusteCal + _
                          rTrn.valMlExcesoLineaCal + rTrn.valMlExcesoTopeCal + _
                       rTrn.valMlPrimaSisCal + rTrn.valMlPrimaSisInteresCal + rTrn.valMlPrimaSisReajusteCal  'SIS//

            cuoAbonos = rTrn.valCuoMvtoCal + rTrn.valCuoInteresCal + rTrn.valCuoReajusteCal + _
                        rTrn.valCuoAdicionalCal + rTrn.valCuoAdicionalInteresCal + rTrn.valCuoAdicionalReajusteCal + _
                        rTrn.valCuoExcesoLineaCal + rTrn.valCuoExcesoTopeCal + _
                      rTrn.valCuoPrimaSisCal + rTrn.valCuoPrimaSisInteresCal + rTrn.valCuoPrimaSisReajusteCal  'SIS//

            mlCargos = 0
            cuoCargos = 0
            clsAux.Add(gidAdm, "REZA", rTrn.tipoProducto, rTrn.tipoFondoDestinoCal, mlAbonos, cuoAbonos, mlCargos, cuoCargos)

        ElseIf rTrn.codOrigenTransaccion = "REZ" Then

            Select Case rTrn.codOrigenProceso

                Case "TRAINREZ", "TRAINRZC", "TRAIPAGN"

                Case "REREZMAS", "REREZSEL", "REREZCON", "AJUSELEC", "AJUMASIV"
                    mlAbonos = 0
                    cuoAbonos = 0

                    'INI Linea AGREGADA PCI
                    'rTrn.valMlExcesoLineaCal + rTrn.valMlExcesoTopeCal + _
                    'rTrn.valCuoExcesoLineaCal + rTrn.valCuoExcesoTopeCal + _
                    'FIN Linea AGREGADA PCI

                    mlCargos = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste + _
                               rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste + _
                               rTrn.valMlPrimaSis + rTrn.valMlPrimaSisInteres + rTrn.valMlPrimaSisReajuste  'SIS//

                    cuoCargos = rTrn.valCuoMvto + rTrn.valCuoInteres + rTrn.valCuoReajuste + _
                                rTrn.valCuoAdicional + rTrn.valCuoAdicionalInteres + rTrn.valCuoAdicionalReajuste + _
                                rTrn.valCuoPrimaSis + rTrn.valCuoPrimaSisInteres + rTrn.valCuoPrimaSisReajuste  'SIS//

                    clsAux.Add(gidAdm, "REZA", rTrn.tipoProducto, "C", mlAbonos, cuoAbonos, mlCargos, cuoCargos)
            End Select

        End If

        '-----------------------------------------------------
        'Transferencias
        '-----------------------------------------------------
        If rTrn.tipoImputacion = "ABO" Then
            If rTrn.codDestinoTransaccionCal = "CTA" And rTrn.valMlTransferenciaCal + rTrn.valCuoTransferenciaCal > 0 Then
                mlAbonos = rTrn.valMlTransferenciaCal
                cuoAbonos = rTrn.valCuoTransferenciaCal
                mlCargos = 0
                cuoCargos = 0
                clsAux.Add(gidAdm, "TRAN", rTrn.tipoProducto, "C", mlAbonos, cuoAbonos, mlCargos, cuoCargos)
            End If

            If rTrn.codDestinoTransaccionCal = "REZ" And rTrn.valMlTransferenciaCal + rTrn.valCuoTransferenciaCal > 0 Then
                mlAbonos = rTrn.valMlTransferenciaCal
                cuoAbonos = rTrn.valCuoTransferenciaCal
                mlCargos = 0
                cuoCargos = 0
                clsAux.Add(gidAdm, "TRAN", rTrn.tipoProducto, "C", mlAbonos, cuoAbonos, mlCargos, cuoCargos)
            End If

            If rTrn.codDestinoTransaccionCal = "TRF" Then

                mlAbonos = rTrn.valMlTransferenciaCal
                cuoAbonos = rTrn.valCuoTransferenciaCal
                mlCargos = 0
                cuoCargos = 0
                clsAux.Add(gidAdm, "TRAN", rTrn.tipoProducto, "C", mlAbonos, cuoAbonos, mlCargos, cuoCargos)

            End If
        Else
            If rTrn.codDestinoTransaccionCal = "TRF" Then

                mlAbonos = 0
                cuoAbonos = 0
                mlCargos = rTrn.valMlMvtoCal
                cuoCargos = rTrn.valCuoMvtoCal
                clsAux.Add(gidAdm, "TRAN", rTrn.tipoProducto, "C", mlAbonos, cuoAbonos, mlCargos, cuoCargos)

            End If
        End If
        '-----------------------------------------------------
        'Comisiones
        '-----------------------------------------------------

        mlAbonos = 0 : cuoAbonos = 0 : mlCargos = 0 : cuoCargos = 0

        Select Case rMovAcr.tipoMvto

            Case "COM"
                mlAbonos = rTrn.valMlMvto
                cuoAbonos = rTrn.valCuoMvto

            Case "RCOM"
                mlCargos = rTrn.valMlMvto
                cuoCargos = rTrn.valCuoMvto

            Case Else
                'lfc://18/11/2009 --'DESCOMENTAR,   'antes SIS, la comision quedaba en Cero

                If gAdicionalSeTransfiere And (rTrn.codMvtoComPor = "120703" Or rTrn.codMvtoComPor = "620703") Then ' solo esta condicion new
                    If rTrn.perCotizacion >= New Date(2009, 7, 1).Date Then
                        mlAbonos = rTrn.valMlComisFijaCal + rTrn.valMlComisPorcentualCal
                        cuoAbonos = rTrn.valCuoComisFijaCal + rTrn.valCuoComisPorcentualCal
                    Else
                        mlAbonos = rTrn.valMlComisFijaCal
                        cuoAbonos = rTrn.valCuoComisFijaCal
                    End If
                Else
                    mlAbonos = rTrn.valMlComisFijaCal + rTrn.valMlComisPorcentualCal
                    cuoAbonos = rTrn.valCuoComisFijaCal + rTrn.valCuoComisPorcentualCal
                End If

                'mlAbonos = rTrn.valMlComisFijaCal + rTrn.valMlComisPorcentualCal
                'cuoAbonos = rTrn.valCuoComisFijaCal + rTrn.valCuoComisPorcentualCal

                If mlAbonos + cuoAbonos = 0 Then
                    mlAbonos = rTrn.valMlPrimaCal
                    cuoAbonos = rTrn.valCuoPrimaCal
                End If
        End Select

        clsAux.Add(gidAdm, "COMI", rTrn.tipoProducto, rTrn.tipoFondoDestinoCal, mlAbonos, cuoAbonos, mlCargos, cuoCargos)


    End Sub

    Private Sub SumarTotalesComision()

        gvalCuoPatrFrecCal = rTrn.valCuoPatrFrecCal
        gvalMlPatrFrecCal = rTrn.valMlPatrFrecCal

        gvalCuoPatrFdesCal = rTrn.valCuoPatrFdesCal
        gvalMlPatrFdesCal = rTrn.valMlPatrFdesCal

        gvalMlComisiones = rTrn.valMlMvtoCal
        gvalCuoComisiones = rTrn.valCuoMvtoCal

        gvalMlComisionesCal = rTrn.valMlMvtoCal
        gvalCuoComisionesCal = rTrn.valCuoMvtoCal

        If rTrn.tipoImputacion = "ABO" Then

            gvalMlAbonosCtaCal = gvalMlComisiones
            gvalCuoAbonosCtaCal = gvalCuoComisiones

        Else

            gvalMlCargosCtaCal = gvalMlComisiones
            gvalCuoCargosCtaCal = gvalCuoComisiones

        End If


    End Sub


    Private Sub CrearEncabezadoAcred()
        Dim ds As DataSet
        If gtipoProceso = "AC" Then
            ds = ResultadoAcred.EncabezadoAcred.traer(gdbc,gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId, 0)
            If ds.Tables(0).Rows.Count > 0 Then

                If CDate(ds.Tables(0).Rows(0).Item("FEC_ACREDITACION")) <> gfecAcreditacion Then
                    Throw New SondaException(15015) ' Los parametros utilizados en la simulacion difieren de los de la acreditación
                End If

                If CDate(ds.Tables(0).Rows(0).Item("FEC_VALOR_CUOTA")) <> gfecValorCuota Then
                    Throw New SondaException(15015) ' Los parametros utilizados en la simulacion difieren de los de la acreditación
                End If

            End If
        End If
        gseqProceso = ResultadoAcred.EncabezadoAcred.crearConSecuencia(gdbc,gidAdm, gcodOrigenProceso, gidUsuarioProceso, _
                                  gnumeroId, gtipoProceso, Now, gnumOrigenRef, gfecAcreditacion, _
                                  gperContable, gfecValorCuota, gidUsuarioProceso, gfuncion)


    End Sub




    Private Sub IngresarAuxiliarComisiones()

        If rTrn.valMlComisFijaCal > 0 Then

            rComis.tipoImputacion = "ABO"
            rComis.seqComision = 0
            rComis.idCliente = rTrn.idCliente
            rComis.idPersona = rTrn.idPersona
            rComis.idEmpleador = rTrn.idEmpleador
            rComis.codOrigenProceso = gcodOrigenProceso
            rComis.perCotizacion = rTrn.perCotizacion
            rComis.tipoProducto = rTrn.tipoProducto
            rComis.tipoFondo = rTrn.tipoFondoDestinoCal
            rComis.categoria = rTrn.categoria
            rComis.perContable = rTrn.perContable
            rComis.fecAcreditacion = rTrn.fecAcreditacion
            rComis.tipoComision = rTrn.tipoComisionFija
            rComis.valMlComision = rTrn.valMlComisFijaCal
            rComis.valCuoComision = rTrn.valCuoComisFijaCal
            rComis.numMvtoAsociado = rTrn.seqMvtoDestino
            rComis.tipoAgrupProceso = "ADM"
            rComis.tipoAgrupComision = rTipComFij.tipoAgrupComision
            rComis.codCausalAjuste = rTrn.codCausalAjuste

            If gtipoProceso = "AC" Then
                rTrn.seqComisionFija = Auxiliares.Comisiones.crearConSeq(gdbc,gidAdm, rComis.seqComision, rComis.idCliente, _
                                                  rComis.idPersona, rComis.idEmpleador, rComis.codOrigenProceso, _
                                                  rComis.perCotizacion, rComis.tipoProducto, rComis.tipoFondo, _
                                                  rComis.categoria, rComis.tipoImputacion, rComis.perContable, _
                                                  rComis.fecAcreditacion, rComis.tipoComision, rComis.valMlComision, _
                                                  rComis.valCuoComision, rComis.numMvtoAsociado, rComis.tipoAgrupProceso, _
                                                  rComis.tipoAgrupComision, rComis.codCausalAjuste, gidUsuarioProceso, gfuncion)
            End If
        End If

        If Not gAdicionalSeTransfiere Then

            If rTrn.valMlComisPorcentualCal > 0 Then

                rComis.tipoImputacion = "ABO"
                rComis.seqComision = 0
                rComis.idCliente = rTrn.idCliente
                rComis.idPersona = rTrn.idPersona
                rComis.idEmpleador = rTrn.idEmpleador
                rComis.codOrigenProceso = gcodOrigenProceso
                rComis.perCotizacion = rTrn.perCotizacion
                rComis.tipoProducto = rTrn.tipoProducto
                rComis.tipoFondo = rTrn.tipoFondoDestinoCal
                rComis.categoria = rTrn.categoria
                rComis.perContable = rTrn.perContable
                rComis.fecAcreditacion = rTrn.fecAcreditacion
                rComis.tipoComision = rTrn.tipoComisionPorcentual
                rComis.valMlComision = rTrn.valMlComisPorcentualCal '- rTrn.valMlPrimaCal (ye fue restada la prima)
                rComis.valCuoComision = rTrn.valCuoComisPorcentualCal '- rTrn.valCuoPrimaCal
                rComis.numMvtoAsociado = rTrn.seqMvtoDestino
                rComis.tipoAgrupProceso = "ADM"
                rComis.tipoAgrupComision = rTipComPor.tipoAgrupComision
                rComis.codCausalAjuste = rTrn.codCausalAjuste

                If gtipoProceso = "AC" Then
                    rTrn.seqComisionPorcentual = Auxiliares.Comisiones.crearConSeq(gdbc,gidAdm, rComis.seqComision, rComis.idCliente, _
                                                rComis.idPersona, rComis.idEmpleador, rComis.codOrigenProceso, _
                                                rComis.perCotizacion, rComis.tipoProducto, rComis.tipoFondo, _
                                                rComis.categoria, rComis.tipoImputacion, rComis.perContable, _
                                                rComis.fecAcreditacion, rComis.tipoComision, rComis.valMlComision, _
                                                rComis.valCuoComision, rComis.numMvtoAsociado, rComis.tipoAgrupProceso, _
                                                rComis.tipoAgrupComision, rComis.codCausalAjuste, gidUsuarioProceso, gfuncion)

                End If

            End If '  If rTrn.valMlComisPorcentualCal > 0 Then
        Else 'COMISION DESCORDINADA SE PAGA A OTRA ADMINISTRADORA

            If rTrn.valMlPrimaCal + rTrn.valCuoPrimaCal > 0 And rTrn.valMlComisPorcentualCal + rTrn.valCuoComisPorcentualCal = 0 Then
                rComis.tipoImputacion = "ABO"
                rComis.seqComision = 0
                rComis.idCliente = rTrn.idCliente
                rComis.idPersona = rTrn.idPersona
                rComis.idEmpleador = rTrn.idEmpleador
                rComis.codOrigenProceso = gcodOrigenProceso
                rComis.perCotizacion = rTrn.perCotizacion
                rComis.tipoProducto = rTrn.tipoProducto
                rComis.tipoFondo = rTrn.tipoFondoDestinoCal
                rComis.categoria = rTrn.categoria
                rComis.perContable = rTrn.perContable
                rComis.fecAcreditacion = rTrn.fecAcreditacion
                rComis.tipoComision = rTrn.tipoComisionPorcentual
                rComis.valMlComision = rTrn.valMlPrimaCal
                rComis.valCuoComision = rTrn.valCuoPrimaCal
                rComis.numMvtoAsociado = rTrn.seqMvtoDestino
                rComis.tipoAgrupProceso = "TRF"
                rComis.tipoAgrupComision = rTipComPor.tipoAgrupComision
                rComis.codCausalAjuste = rTrn.codCausalAjuste

                If gtipoProceso = "AC" Then
                    rTrn.seqComisionPorcentual = Auxiliares.Comisiones.crearConSeq(gdbc,gidAdm, rComis.seqComision, rComis.idCliente, _
                                                rComis.idPersona, rComis.idEmpleador, rComis.codOrigenProceso, _
                                                rComis.perCotizacion, rComis.tipoProducto, rComis.tipoFondo, _
                                                rComis.categoria, rComis.tipoImputacion, rComis.perContable, _
                                                rComis.fecAcreditacion, rComis.tipoComision, rComis.valMlComision, _
                                                rComis.valCuoComision, rComis.numMvtoAsociado, rComis.tipoAgrupProceso, _
                                                rComis.tipoAgrupComision, rComis.codCausalAjuste, gidUsuarioProceso, gfuncion)

                End If
            End If
        End If

        If IsNothing(rTrn.codMvto) Then
            Exit Sub
        End If

        'reversa de comision
        If rMovAcr.tipoMvto = "RCOM" Then
            rComis.tipoImputacion = "CAR"

            rComis.seqComision = 0
            rComis.idCliente = rTrn.idCliente
            rComis.idPersona = rTrn.idPersona
            rComis.idEmpleador = rTrn.idEmpleador
            rComis.codOrigenProceso = gcodOrigenProceso
            rComis.perCotizacion = rTrn.perCotizacion
            rComis.tipoProducto = rTrn.tipoProducto
            rComis.tipoFondo = rTrn.tipoFondoDestinoCal
            rComis.categoria = rTrn.categoria
            rComis.perContable = rTrn.perContable
            rComis.fecAcreditacion = rTrn.fecAcreditacion
            rComis.tipoComision = "PRE1" 'rTrn.tipoComisionPorcentual
            rComis.valMlComision = rTrn.valMlMvtoCal
            rComis.valCuoComision = rTrn.valCuoMvtoCal
            rComis.numMvtoAsociado = rTrn.seqMvtoDestino
            rComis.tipoAgrupProceso = "ADM"
            rComis.tipoAgrupComision = rTipComPor.tipoAgrupComision
            rComis.codCausalAjuste = rTrn.codCausalAjuste

            If gtipoProceso = "AC" Then
                rTrn.seqComisionPorcentual = Auxiliares.Comisiones.crearConSeq(gdbc,gidAdm, rComis.seqComision, rComis.idCliente, _
                                            rComis.idPersona, rComis.idEmpleador, rComis.codOrigenProceso, _
                                            rComis.perCotizacion, rComis.tipoProducto, rComis.tipoFondo, _
                                            rComis.categoria, rComis.tipoImputacion, rComis.perContable, _
                                            rComis.fecAcreditacion, rComis.tipoComision, rComis.valMlComision, _
                                            rComis.valCuoComision, rComis.numMvtoAsociado, rComis.tipoAgrupProceso, _
                                            rComis.tipoAgrupComision, rComis.codCausalAjuste, gidUsuarioProceso, gfuncion)


                If rTrn.codMvto = "110506" Then
                    INEPrimasSeguros.rebajaPrimaCiaSeg(gdbc,gidAdm, rCli, rTrn, Me.gcodAdministradora, gidUsuarioProceso, gfuncion)
                End If
            End If



        End If

        'Movimientos de comision
        If rMovAcr.tipoMvto = "COM" Then

            rComis.tipoImputacion = "ABO"
            rComis.seqComision = 0
            rComis.idCliente = rTrn.idCliente
            rComis.idPersona = rTrn.idPersona
            rComis.idEmpleador = rTrn.idEmpleador
            rComis.codOrigenProceso = gcodOrigenProceso
            rComis.perCotizacion = rTrn.perCotizacion
            rComis.tipoProducto = rTrn.tipoProducto
            rComis.tipoFondo = rTrn.tipoFondoDestinoCal
            rComis.categoria = rTrn.categoria
            rComis.perContable = rTrn.perContable
            rComis.fecAcreditacion = rTrn.fecAcreditacion

            rComis.tipoComision = rTrn.tipoComisionPorcentual

            rComis.valMlComision = rTrn.valMlMvtoCal
            rComis.valCuoComision = rTrn.valCuoMvtoCal
            rComis.numMvtoAsociado = rTrn.seqMvtoDestino
            rComis.tipoAgrupProceso = "ADM"

            Dim OBJ As Object
            OBJ = rTipComPor.tipoAgrupComision

            rComis.tipoAgrupComision = rTipComPor.tipoAgrupComision



            rComis.codCausalAjuste = rTrn.codCausalAjuste

            If gtipoProceso = "AC" Then

                rTrn.seqComisionPorcentual = Auxiliares.Comisiones.crearConSeq(gdbc,gidAdm, rComis.seqComision, rComis.idCliente, _
                            rComis.idPersona, rComis.idEmpleador, rComis.codOrigenProceso, _
                            rComis.perCotizacion, rComis.tipoProducto, rComis.tipoFondo, _
                            rComis.categoria, rComis.tipoImputacion, rComis.perContable, _
                            rComis.fecAcreditacion, rComis.tipoComision, rComis.valMlComision, _
                            rComis.valCuoComision, rComis.numMvtoAsociado, rComis.tipoAgrupProceso, _
                            rComis.tipoAgrupComision, rComis.codCausalAjuste, gidUsuarioProceso, gfuncion)
            End If

        End If



    End Sub



    'SIS// lfc:30-09-09
    Private Sub IngresarAuxiliarComisiones2()


        If rTrn.valMlComisFijaCal > 0 Then
            rComis.tipoImputacion = "ABO"
            rComis.seqComision = 0
            rComis.idCliente = rTrn.idCliente
            rComis.idPersona = rTrn.idPersona
            rComis.idEmpleador = rTrn.idEmpleador
            rComis.codOrigenProceso = gcodOrigenProceso
            rComis.perCotizacion = rTrn.perCotizacion
            rComis.tipoProducto = rTrn.tipoProducto
            rComis.tipoFondo = rTrn.tipoFondoDestinoCal
            rComis.categoria = rTrn.categoria
            rComis.perContable = rTrn.perContable
            rComis.fecAcreditacion = rTrn.fecAcreditacion
            rComis.tipoComision = rTrn.tipoComisionFija
            rComis.valMlComision = rTrn.valMlComisFijaCal
            rComis.valCuoComision = rTrn.valCuoComisFijaCal
            rComis.numMvtoAsociado = rTrn.seqMvtoDestino
            rComis.tipoAgrupProceso = "ADM"
            rComis.tipoAgrupComision = rTipComFij.tipoAgrupComision
            rComis.codCausalAjuste = rTrn.codCausalAjuste
            If gtipoProceso = "AC" Then
                rTrn.seqComisionFija = Auxiliares.Comisiones.crearConSeq(gdbc,gidAdm, rComis.seqComision, rComis.idCliente, _
                                                  rComis.idPersona, rComis.idEmpleador, rComis.codOrigenProceso, _
                                                  rComis.perCotizacion, rComis.tipoProducto, rComis.tipoFondo, _
                                                  rComis.categoria, rComis.tipoImputacion, rComis.perContable, _
                                                  rComis.fecAcreditacion, rComis.tipoComision, rComis.valMlComision, _
                                                  rComis.valCuoComision, rComis.numMvtoAsociado, rComis.tipoAgrupProceso, _
                                                  rComis.tipoAgrupComision, rComis.codCausalAjuste, gidUsuarioProceso, gfuncion)
            End If
        End If

        If Not gAdicionalSeTransfiere Then

            If rTrn.valMlComisPorcentualCal > 0 Then
                rComis.tipoImputacion = "ABO"
                rComis.seqComision = 0
                rComis.idCliente = rTrn.idCliente
                rComis.idPersona = rTrn.idPersona
                rComis.idEmpleador = rTrn.idEmpleador
                rComis.codOrigenProceso = gcodOrigenProceso
                rComis.perCotizacion = rTrn.perCotizacion
                rComis.tipoProducto = rTrn.tipoProducto
                rComis.tipoFondo = rTrn.tipoFondoDestinoCal
                rComis.categoria = rTrn.categoria
                rComis.perContable = rTrn.perContable
                rComis.fecAcreditacion = rTrn.fecAcreditacion
                rComis.tipoComision = rTrn.tipoComisionPorcentual
                rComis.valMlComision = rTrn.valMlComisPorcentualCal '- rTrn.valMlPrimaCal (ye fue restada la prima)
                rComis.valCuoComision = rTrn.valCuoComisPorcentualCal '- rTrn.valCuoPrimaCal
                rComis.numMvtoAsociado = rTrn.seqMvtoDestino
                rComis.tipoAgrupProceso = "ADM"
                rComis.tipoAgrupComision = rTipComPor.tipoAgrupComision
                rComis.codCausalAjuste = rTrn.codCausalAjuste

                If gtipoProceso = "AC" Then
                    rTrn.seqComisionPorcentual = Auxiliares.Comisiones.crearConSeq(gdbc,gidAdm, rComis.seqComision, rComis.idCliente, _
                                                rComis.idPersona, rComis.idEmpleador, rComis.codOrigenProceso, _
                                                rComis.perCotizacion, rComis.tipoProducto, rComis.tipoFondo, _
                                                rComis.categoria, rComis.tipoImputacion, rComis.perContable, _
                                                rComis.fecAcreditacion, rComis.tipoComision, rComis.valMlComision, _
                                                rComis.valCuoComision, rComis.numMvtoAsociado, rComis.tipoAgrupProceso, _
                                                rComis.tipoAgrupComision, rComis.codCausalAjuste, gidUsuarioProceso, gfuncion)

                End If

            End If '  If rTrn.valMlComisPorcentualCal > 0 Then
        Else 'COMISION DESCORDINADA SE PAGA A OTRA ADMINISTRADORA

            'If rTrn.valMlPrimaCal + rTrn.valCuoPrimaCal > 0 And rTrn.valMlComisPorcentualCal + rTrn.valCuoComisPorcentualCal = 0 Then
            If rTrn.valMlComisPorcentualCal > 0 Or rTrn.valCuoComisPorcentualCal > 0 Or rTrn.codOrigenProceso = "ACRTGRCO" Then
                rComis.tipoImputacion = "ABO"
                rComis.seqComision = 0
                rComis.idCliente = rTrn.idCliente
                rComis.idPersona = rTrn.idPersona
                rComis.idEmpleador = rTrn.idEmpleador
                rComis.codOrigenProceso = gcodOrigenProceso
                rComis.perCotizacion = rTrn.perCotizacion
                rComis.tipoProducto = rTrn.tipoProducto
                rComis.tipoFondo = rTrn.tipoFondoDestinoCal
                rComis.categoria = rTrn.categoria
                rComis.perContable = rTrn.perContable
                rComis.fecAcreditacion = rTrn.fecAcreditacion

                If rTrn.tipoProducto = "CCO" Then
                    rComis.tipoComision = "PRE1"
                ElseIf rTrn.tipoProducto = "CAF" Then
                    rComis.tipoComision = "PRE6"
                Else
                    rComis.tipoComision = rTrn.tipoComisionPorcentual
                End If

                'lfc:añade OS-9975581 - 31/08/2017
                If gcodAdministradora = 1034 Or gcodAdministradora = 1035 Then
                    rComis.tipoComision = rTrn.tipoComisionPorcentual 'comd
                Else

                End If

                If rTrn.codOrigenProceso = "ACRTGRCO" Then
                    rComis.valMlComision = rTrn.valMlMvtoCal
                    rComis.valCuoComision = rTrn.valCuoMvtoCal
                Else
                    rComis.valMlComision = rTrn.valMlComisPorcentualCal
                    rComis.valCuoComision = rTrn.valCuoComisPorcentualCal
                End If

                'lfc:añade OS-9975581 - 31/08/2017
                If gcodAdministradora = 1034 Or gcodAdministradora = 1035 Then
                    rComis.numMvtoAsociado = rTrn.seqDestinoTrfCal
                Else
                    rComis.numMvtoAsociado = rTrn.seqMvtoDestino
                End If


                rComis.tipoAgrupProceso = "TRF"
                rComis.tipoAgrupComision = rTipComPor.tipoAgrupComision
                rComis.codCausalAjuste = rTrn.codCausalAjuste

                If gtipoProceso = "AC" Then
                    rTrn.seqComisionPorcentual = Auxiliares.Comisiones.crearConSeq(gdbc, gidAdm, rComis.seqComision, rComis.idCliente, _
                                                rComis.idPersona, rComis.idEmpleador, rComis.codOrigenProceso, _
                                                rComis.perCotizacion, rComis.tipoProducto, rComis.tipoFondo, _
                                                rComis.categoria, rComis.tipoImputacion, rComis.perContable, _
                                                rComis.fecAcreditacion, rComis.tipoComision, rComis.valMlComision, _
                                                rComis.valCuoComision, rComis.numMvtoAsociado, rComis.tipoAgrupProceso, _
                                                rComis.tipoAgrupComision, rComis.codCausalAjuste, gidUsuarioProceso, gfuncion)

                End If
            End If
            End If

        If IsNothing(rTrn.codMvto) Then
            Exit Sub
        End If

        'reversa de comision
        If rMovAcr.tipoMvto = "RCOM" Then
            rComis.tipoImputacion = "CAR"

            rComis.seqComision = 0
            rComis.idCliente = rTrn.idCliente
            rComis.idPersona = rTrn.idPersona
            rComis.idEmpleador = rTrn.idEmpleador
            rComis.codOrigenProceso = gcodOrigenProceso
            rComis.perCotizacion = rTrn.perCotizacion
            rComis.tipoProducto = rTrn.tipoProducto
            rComis.tipoFondo = rTrn.tipoFondoDestinoCal
            rComis.categoria = rTrn.categoria
            rComis.perContable = rTrn.perContable
            rComis.fecAcreditacion = rTrn.fecAcreditacion
            rComis.tipoComision = "PRE1" 'rTrn.tipoComisionPorcentual
            rComis.valMlComision = rTrn.valMlMvtoCal
            rComis.valCuoComision = rTrn.valCuoMvtoCal
            rComis.numMvtoAsociado = rTrn.seqMvtoDestino
            rComis.tipoAgrupProceso = "ADM"
            rComis.tipoAgrupComision = rTipComPor.tipoAgrupComision
            rComis.codCausalAjuste = rTrn.codCausalAjuste

            If gtipoProceso = "AC" Then
                rTrn.seqComisionPorcentual = Auxiliares.Comisiones.crearConSeq(gdbc,gidAdm, rComis.seqComision, rComis.idCliente, _
                                            rComis.idPersona, rComis.idEmpleador, rComis.codOrigenProceso, _
                                            rComis.perCotizacion, rComis.tipoProducto, rComis.tipoFondo, _
                                            rComis.categoria, rComis.tipoImputacion, rComis.perContable, _
                                            rComis.fecAcreditacion, rComis.tipoComision, rComis.valMlComision, _
                                            rComis.valCuoComision, rComis.numMvtoAsociado, rComis.tipoAgrupProceso, _
                                            rComis.tipoAgrupComision, rComis.codCausalAjuste, gidUsuarioProceso, gfuncion)

                'Se agrega condicion por periodo. MOD-2012050012 OS-4335303 PCI. Solicicitado por PAC.
                If rTrn.perCotizacion < New Date(2009, 7, 1).Date Then
                    If rTrn.codMvto = "110506" Then
                        INEPrimasSeguros.rebajaPrimaCiaSeg(gdbc, gidAdm, rCli, rTrn, Me.gcodAdministradora, gidUsuarioProceso, gfuncion)
                    End If
                End If
            End If
        End If

        'Reversa de comision desde Generador de Excesos
        If rTrn.codOrigenProceso = "GEXCESOS" And rTrn.codMvto = "111851" Then
            rComis.tipoImputacion = "CAR"

            rComis.seqComision = 0
            rComis.idCliente = rTrn.idCliente
            rComis.idPersona = rTrn.idPersona
            rComis.idEmpleador = rTrn.idEmpleador
            rComis.codOrigenProceso = gcodOrigenProceso
            rComis.perCotizacion = rTrn.perCotizacion
            rComis.tipoProducto = rTrn.tipoProducto
            rComis.tipoFondo = rTrn.tipoFondoDestinoCal
            rComis.categoria = rTrn.categoria
            rComis.perContable = rTrn.perContable
            rComis.fecAcreditacion = rTrn.fecAcreditacion
            rComis.tipoComision = "PRE1" 'rTrn.tipoComisionPorcentual
            rComis.valMlComision = rTrn.valMlMvtoCal
            rComis.valCuoComision = rTrn.valCuoMvtoCal
            rComis.numMvtoAsociado = rTrn.seqMvtoDestino
            rComis.tipoAgrupProceso = "ADM"
            rComis.tipoAgrupComision = rTipComPor.tipoAgrupComision
            rComis.codCausalAjuste = rTrn.codCausalAjuste

            If gtipoProceso = "AC" Then
                rTrn.seqComisionPorcentual = Auxiliares.Comisiones.crearConSeq(gdbc, gidAdm, rComis.seqComision, rComis.idCliente, _
                                            rComis.idPersona, rComis.idEmpleador, rComis.codOrigenProceso, _
                                            rComis.perCotizacion, rComis.tipoProducto, rComis.tipoFondo, _
                                            rComis.categoria, rComis.tipoImputacion, rComis.perContable, _
                                            rComis.fecAcreditacion, rComis.tipoComision, rComis.valMlComision, _
                                            rComis.valCuoComision, rComis.numMvtoAsociado, rComis.tipoAgrupProceso, _
                                            rComis.tipoAgrupComision, rComis.codCausalAjuste, gidUsuarioProceso, gfuncion)

                'Se agrega condicion por periodo. MOD-2012050012 OS-4335303 PCI. Solicicitado por PAC.
                If rTrn.perCotizacion < New Date(2009, 7, 1).Date Then
                    If rTrn.codMvto = "110506" Then
                        INEPrimasSeguros.rebajaPrimaCiaSeg(gdbc, gidAdm, rCli, rTrn, Me.gcodAdministradora, gidUsuarioProceso, gfuncion)
                    End If
                End If
            End If

        End If

        'Movimientos de comision
        If rMovAcr.tipoMvto = "COM" Then

            rComis.tipoImputacion = "ABO"
            rComis.seqComision = 0
            rComis.idCliente = rTrn.idCliente
            rComis.idPersona = rTrn.idPersona
            rComis.idEmpleador = rTrn.idEmpleador
            rComis.codOrigenProceso = gcodOrigenProceso
            rComis.perCotizacion = rTrn.perCotizacion
            rComis.tipoProducto = rTrn.tipoProducto
            rComis.tipoFondo = rTrn.tipoFondoDestinoCal
            rComis.categoria = rTrn.categoria
            rComis.perContable = rTrn.perContable
            rComis.fecAcreditacion = rTrn.fecAcreditacion

            rComis.tipoComision = rTrn.tipoComisionPorcentual

            rComis.valMlComision = rTrn.valMlMvtoCal
            rComis.valCuoComision = rTrn.valCuoMvtoCal
            rComis.numMvtoAsociado = rTrn.seqMvtoDestino
            rComis.tipoAgrupProceso = "ADM"
            rComis.tipoAgrupComision = rTipComPor.tipoAgrupComision
            rComis.codCausalAjuste = rTrn.codCausalAjuste


            If gtipoProceso = "AC" Then

                rTrn.seqComisionPorcentual = Auxiliares.Comisiones.crearConSeq(gdbc, gidAdm, rComis.seqComision, rComis.idCliente, _
                                            rComis.idPersona, rComis.idEmpleador, rComis.codOrigenProceso, _
                                            rComis.perCotizacion, rComis.tipoProducto, rComis.tipoFondo, _
                                            rComis.categoria, rComis.tipoImputacion, rComis.perContable, _
                                            rComis.fecAcreditacion, rComis.tipoComision, rComis.valMlComision, _
                                            rComis.valCuoComision, rComis.numMvtoAsociado, rComis.tipoAgrupProceso, _
                                            rComis.tipoAgrupComision, rComis.codCausalAjuste, gidUsuarioProceso, gfuncion)
            End If

        End If
    End Sub


    Private Sub ValoresAcreditacion()

        Dim numDias As Integer
        Dim i As Integer


        gfecAcreditacion = Sys.Kernel.Parametros.FechaAcreditacion.obtenerFechaAcreditacion(gdbc, gidAdm, "ACR")
        gfecValorCuota = Sys.Kernel.Parametros.FechaAcreditacion.obtenerFechaValorCuota(gdbc, gidAdm, "ACR")
        gperCuatrimestre = ParametrosINE.PeriodoCuatrimestral.traer(gdbc,gidAdm).Tables(0).Rows(0).Item("PER_CUATRIMESTRE")

        If gfecValorCuota = Nothing Then
            blErrorFatal = True
            Throw New SondaException(15313) '"Fecha valor cuota es nulo
        End If

        dsAux = ParametrosINE.PeriodoContable.traer(gdbc,gidAdm)
        If dsAux.Tables(0).Rows.Count = 0 Then
            blErrorFatal = True
            Throw New SondaException(15314) '"No existe periodo contable
        Else
            gperContable = dsAux.Tables(0).Rows(0).Item("PER_CONTABLE")
            'SIS//--para comprar con cta CAF pago adelantado
            gPerContableSis = dsAux.Tables(0).Rows(0).Item("PER_CONTABLE")
        End If
        ' NUEVO
        CrearEncabezadoAcred()

        dsAux = ResultadoAcred.EncabezadoAcred.traer(gdbc,gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId, gseqProceso)
        If dsAux.Tables(0).Rows.Count > 0 Then
            rCab = New ccEncabezadoAcred(dsAux)
            ValoresAcreditacion1() 'crea los valores cuota para el proceso
            gExisteEncabezado = True
        Else

            blErrorFatal = True
            Throw New SondaException(15315) '"Reproceso acreditacion difiere con valores iniciales

        End If

        rOriPro = Nothing
        dsAux = ParametrosINE.OrigenProceso.traer(gdbc,gidAdm, gcodOrigenProceso)
        If dsAux.Tables(0).Rows.Count = 0 Then
            blErrorFatal = True
            GenerarLog("E", 15341, "Origen Proceso: " & Trim(gcodOrigenProceso), gIdHebra, 0, Nothing, 0)
            Throw New SondaException(15341) '"No existe origen proceso

        End If
        rOriPro = New ccAcrOrigenProceso(dsAux)
        blPermiteAcreditacionParcial = rOriPro.indAcreditacion = "S"
    End Sub



    Private Sub ValoresAcreditacion1()

        Dim i As Integer


        dsAux = ParametrosINE.ValorCuota.obtenerValorCuota(gdbc,gidAdm, gfecValorCuota, Nothing)
        'dsAux = Parametro.obtenerValorCuotasFondos(gdbc, gidAdm, gfecValorCuota, Nothing, 0, 1)
        If dsAux.Tables(0).Rows.Count < 5 Then
            blErrorFatal = True
            Throw New SondaException(15316) '"No existen todos los valores cuotas
        End If
        For i = 0 To dsAux.Tables(0).Rows.Count - 1
            Select Case dsAux.Tables(0).Rows(i).Item("TIPO_FONDO")
                Case "A"
                    gvalMlCuotaDestinoA = dsAux.Tables(0).Rows(i).Item("VAL_CUOTA")
                    CrearCuotaAcred("A", gvalMlCuotaDestinoA)

                Case "B"
                    gvalMlCuotaDestinoB = dsAux.Tables(0).Rows(i).Item("VAL_CUOTA")
                    CrearCuotaAcred("B", gvalMlCuotaDestinoB)

                Case "C"
                    gvalMlCuotaDestinoC = dsAux.Tables(0).Rows(i).Item("VAL_CUOTA")
                    CrearCuotaAcred("C", gvalMlCuotaDestinoC)

                Case "D"
                    gvalMlCuotaDestinoD = dsAux.Tables(0).Rows(i).Item("VAL_CUOTA")
                    CrearCuotaAcred("D", gvalMlCuotaDestinoD)

                Case "E"
                    gvalMlCuotaDestinoE = dsAux.Tables(0).Rows(i).Item("VAL_CUOTA")
                    CrearCuotaAcred("E", gvalMlCuotaDestinoE)

            End Select
        Next


    End Sub


    Private Sub ValoresAcreditacion2()

        Dim i As Integer


        dsAux = ResultadoAcred.CuotasAcred.buscar(gdbc,gidAdm, gnumeroId, gseqProceso, Nothing)
        If dsAux.Tables(0).Rows.Count < 5 Then
            blErrorFatal = True
            Throw New SondaException(15316) '"No existen todos los valores cuota
        End If

        For i = 0 To dsAux.Tables(0).Rows.Count - 1

            Select Case dsAux.Tables(0).Rows(i).Item("TIPO_FONDO")

                Case "A" : gvalMlCuotaDestinoA = dsAux.Tables(0).Rows(i).Item("VAL_ML_VALOR_CUOTA")
                Case "B" : gvalMlCuotaDestinoB = dsAux.Tables(0).Rows(i).Item("VAL_ML_VALOR_CUOTA")
                Case "C" : gvalMlCuotaDestinoC = dsAux.Tables(0).Rows(i).Item("VAL_ML_VALOR_CUOTA")
                Case "D" : gvalMlCuotaDestinoD = dsAux.Tables(0).Rows(i).Item("VAL_ML_VALOR_CUOTA")
                Case "E" : gvalMlCuotaDestinoE = dsAux.Tables(0).Rows(i).Item("VAL_ML_VALOR_CUOTA")

            End Select
        Next


    End Sub


    Private Sub ValorCuotaCaja()
        Dim fecNormativa As New Date(2008, 11, 1)

        Dim leerValores As Boolean = True

        If rTrn.codOrigenTransaccion = "REZ" Then
            Exit Sub
        End If
        If II > 0 Then
            leerValores = (rTrn.fecOperacion <> dsTrnCur.Tables(0).Rows(II - 1).Item("FEC_OPERACION"))
        End If

        leerValores = (rTrn.valMlValorCuotaCaja = Nothing) Or (rTrn.valMlValorCuotaCaja = 0)
        If Not leerValores Then
            Exit Sub
        End If

        If rTrn.codOrigenTransaccion <> "REZ" Then
            '--.-- cn2
            If rTrn.fecOperacion < fecNormativa Then

                If rTrn.codOrigenRecaudacion = "RECF" Or rTrn.codOrigenRecaudacion = "SDOF" Or rTrn.codOrigenRecaudacion = "CDOF" Or rTrn.codOrigenRecaudacion = "SOBF" Then
                    rTrn.fecValorCuotaCaja = Fecha.contarhabiles(gdbc, rTrn.fecOperacion, 2)
                Else
                    rTrn.fecValorCuotaCaja = Fecha.contarhabiles(gdbc, rTrn.fecOperacion, 3)
                End If
            Else
                rTrn.fecValorCuotaCaja = Fecha.contarhabiles(gdbc, rTrn.fecOperacion, 1)
            End If
        End If


        If IsNothing(rTrn.fecValorCuotaCaja) Or rTrn.fecValorCuotaCaja < gFecInicioSistema Then
            blErrorFatal = True
            blIgnorar = True
            GenerarLog("E", 15317, "Hebra " & gIdHebra & " - Fecha valor cuota patrimonizacion (caja) es nulo", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
        End If

        'dsAux = Parametro.obtenerValorCuotasFondos(gdbc,gidAdm, rTrn.fecValorCuotaCaja, "C", 0, 1)
        dsAux = ParametrosINE.ValorCuota.obtenerValorCuota(gdbc, gidAdm, rTrn.fecValorCuotaCaja, "C")

        If dsAux.Tables(0).Rows.Count = 0 Then
            blErrorFatal = True
            blIgnorar = True
            GenerarLog("E", 15318, "Hebra " & gIdHebra & " - No existe valor cuota para fecha caja: " & rTrn.fecValorCuotaCaja, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            rTrn.valMlValorCuotaCaja = 1
        Else
            rTrn.valMlValorCuotaCaja = dsAux.Tables(0).Rows(0).Item("VAL_CUOTA")

        End If

    End Sub


    Private Sub CuotaDestino(ByVal tipoFondo As String, ByRef valorCuota As Decimal, ByRef fecValorCuota As Date)


        Select Case gcodOrigenProceso
            'Acreditacion Externa 20/08/2012
            'Case "RECAUDAC", "REREZSEL", "REREZMAS", "REREZCON", "TRAINREZ", "TRAINRZC", "TRAIPAGN"
        Case "RECAUDAC", "REREZSEL", "REREZMAS", "REREZCON", "TRAINREZ", "TRAINRZC", "TRAIPAGN", "ACREXIPS", "ACREXSTJ", "ACREXTBF", "ACREXTGR", "AEXDVSTJ", "ACREXAFC"
                Select Case rTrn.codDestinoTransaccionCal
                    Case "REZ"
                        valorCuota = rTrn.valMlValorCuotaCaja
                        fecValorCuota = rTrn.fecValorCuotaCaja
                    Case "TRF"
                        valorCuota = rTrn.valMlValorCuotaCaja
                        fecValorCuota = rTrn.fecValorCuotaCaja
                    Case Else
                        Select Case tipoFondo
                            Case "A" : valorCuota = gvalMlCuotaDestinoA : fecValorCuota = gfecValorCuota
                            Case "B" : valorCuota = gvalMlCuotaDestinoB : fecValorCuota = gfecValorCuota
                            Case "D" : valorCuota = gvalMlCuotaDestinoD : fecValorCuota = gfecValorCuota
                            Case "E" : valorCuota = gvalMlCuotaDestinoE : fecValorCuota = gfecValorCuota
                            Case "C" ' valorCuota = rTrn.valMlValorCuotaCaja : fecValorCuota = rTrn.fecValorCuotaCaja

                                If rTrn.codOrigenTransaccion = "REZ" Then 'Valor cuota informado
                                    valorCuota = rTrn.valMlValorCuota : fecValorCuota = rTrn.fecValorCuota
                                Else
                                    valorCuota = rTrn.valMlValorCuotaCaja : fecValorCuota = rTrn.fecValorCuotaCaja
                                End If

                        End Select
                End Select

			Case "AJUSELEC", "AJUMASIV", "BEFACAJU", "ACRTOPRO"		   'Valor cuota informado
				valorCuota = rTrn.valMlValorCuota : fecValorCuota = rTrn.fecValorCuota

				''''''    'leo: CA-2009010118 -- se actualizan los valores informados
				''''''Case "DEVEXCAF", "DEVEXCEM"
				''''''    rTrn.fecAcreditacion = gfecAcreditacion
				''''''    rTrn.fecValorCuota = gfecValorCuota
				''''''    rTrn.perContable = gperContable

				''''''    Select Case tipoFondo
				''''''        Case "A" : valorCuota = gvalMlCuotaDestinoA
				''''''        Case "B" : valorCuota = gvalMlCuotaDestinoB
				''''''        Case "C" : valorCuota = gvalMlCuotaDestinoC
				''''''        Case "D" : valorCuota = gvalMlCuotaDestinoD
				''''''        Case "E" : valorCuota = gvalMlCuotaDestinoE
				''''''    End Select

			Case "COBPRIMA"

				valorCuota = rTrn.valMlValorCuota : fecValorCuota = rTrn.fecValorCuota

			Case Else
				If gcodOrigenProceso = "COBPRIMA" And gcodAdministradora = 1032 Then
					'Se deja Fecha y Valor Cuota tal como viene. Solo para PLANVITAL
					valorCuota = rTrn.valMlValorCuota : fecValorCuota = rTrn.fecValorCuota
				Else
					Select Case tipoFondo
						Case "A" : valorCuota = gvalMlCuotaDestinoA
						Case "B" : valorCuota = gvalMlCuotaDestinoB
						Case "C" : valorCuota = gvalMlCuotaDestinoC
						Case "D" : valorCuota = gvalMlCuotaDestinoD
						Case "E" : valorCuota = gvalMlCuotaDestinoE
					End Select
				End If
		End Select
        Select Case tipoFondo
            Case "A" : rTrn.valMlCuotaComision = gvalMlCuotaDestinoA
            Case "B" : rTrn.valMlCuotaComision = gvalMlCuotaDestinoB
            Case "C" : rTrn.valMlCuotaComision = gvalMlCuotaDestinoC
            Case "D" : rTrn.valMlCuotaComision = gvalMlCuotaDestinoD
            Case "E" : rTrn.valMlCuotaComision = gvalMlCuotaDestinoE
        End Select

    End Sub

    Private Sub TraerControlRentas()
        Dim tipoCotizacion As String = Nothing
        gcontrolRentasEnLinea = Nothing

        gExisteConRen = False




        gExisteConRenSis = False


        '--.--OS:1753303-->>>>
        If rMovAcr.tipoMvto = "COP" Then
            tipoCotizacion = "COP"            

        ElseIf rMovAcr.tipoMvto = "COS" Then
            tipoCotizacion = "COT"
            gcontrolRentasEnLinea = tipoCotizacion
            gExisteConRen = True

        ElseIf rMovAcr.tipoMvto = "COT" Then
            tipoCotizacion = "COT"
            gcontrolRentasEnLinea = tipoCotizacion
            gExisteConRen = True

        ElseIf rMovAcr.tipoMvto = "COG" And gcodAdministradora = 1032 Then  'solicitud de planvital lfc:29/12/2017 
            Dim fecGratifIni, fecGratifFin, perCotizacion As Date
            Try
                fecGratifIni = rTrn.fecInicioGratificacion
                fecGratifFin = rTrn.fecFinGratificacion
                perCotizacion = rTrn.perCotizacion
                If perCotizacion.Year = fecGratifIni.Year And perCotizacion.Month = fecGratifIni.Month Then
                    If fecGratifIni.Day = 1 And fecGratifIni >= New Date(1981, 1, 1) Then
                        'If fecGratifFin.Day = Date.DaysInMonth(fecGratifIni.Year, fecGratifIni.Month) Then
                        If fecGratifFin.Year = fecGratifIni.Year And fecGratifFin.Month = fecGratifIni.Month Then
                            tipoCotizacion = "COT"
                            gcontrolRentasEnLinea = tipoCotizacion
                            gExisteConRen = True
                        End If
                    End If
                End If
            Catch : End Try
            If tipoCotizacion Is Nothing Then
                tipoCotizacion = "COG"
                gcontrolRentasEnLinea = Nothing ' gratificaciones por mas de un periodo no acumulan rentas

            End If

        Else '--COG
            tipoCotizacion = "COG"
            gcontrolRentasEnLinea = Nothing ' gratificaciones por mas de un periodo no acumulan rentas
        End If
        '--<<<<

        If gExisteConRen Then
            dsAux = Cotizaciones.ControlRentas.traerControlSim(gdbc, gidAdm, rTrn.idCliente, rTrn.perCotizacion, rTrn.tipoProducto, tipoCotizacion)
            If dsAux.Tables(0).Rows.Count = 0 Then
                rConRen = New ccControlRentas(dsAux.Tables(0).NewRow)
            Else
                rConRen = New ccControlRentas(dsAux)
            End If
        Else
            'LFC: 22-05 traer estructura
            dsAux = Cotizaciones.ControlRentas.traerControlSim(gdbc, gidAdm, rTrn.idCliente, rTrn.perCotizacion, rTrn.tipoProducto, Nothing)
            If dsAux.Tables(0).Rows.Count = 0 Then
                rConRen = New ccControlRentas(dsAux.Tables(0).NewRow)
            Else
                rConRen = New ccControlRentas(dsAux)
            End If
        End If

        TraerControlRentasSIS()
    End Sub

    Private Sub TraerControlRentasSIS()
        gExisteConRenSIS = False

        If rTrn.valMlPrimaSis + rTrn.valMlPrimaSisInteres + rTrn.valMlPrimaSisReajuste > 0 Or (rMovAcr.tipoMvto = "CTP" Or rMovAcr.tipoMvto = "CTG") Then
            'verifica si hay pago de SIS
            gExisteConRenSIS = True
            dsAuxSIS = Cotizaciones.ControlRentas.traerControlSim(gdbc, gidAdm, rTrn.idCliente, rTrn.perCotizacion, rTrn.tipoProducto, "SIS")

            If dsAuxSIS.Tables(0).Rows.Count = 0 Then
                rConRenSIS = New ccControlRentas(dsAuxSIS.Tables(0).NewRow)
            Else
                rConRenSIS = New ccControlRentas(dsAuxSIS)
            End If
        Else
            'LFC: 22-05 traer estructura
            dsAuxSIS = Cotizaciones.ControlRentas.traerControlSim(gdbc, gidAdm, rTrn.idCliente, rTrn.perCotizacion, rTrn.tipoProducto, Nothing)
            If dsAuxSIS.Tables(0).Rows.Count = 0 Then
                rConRenSIS = New ccControlRentas(dsAuxSIS.Tables(0).NewRow)
            Else
                rConRenSIS = New ccControlRentas(dsAuxSIS)
            End If
        End If
    End Sub



    Private Sub CalcularExcesos()

        Dim rTope As ccAcrParametrosAcred

        Dim topeImponiblePes As Decimal
        Dim imponibleAcumulado As Decimal
        Dim diferenciaImponible As Decimal
        Dim imponibleACotizar As Decimal
        Dim fec As String
        Dim MontoCotizacion As Decimal
        Dim limiteExcesoUF As Decimal = 0
        Dim topeImponibleUF As Decimal = 0
        topeImponiblePes = 0
        imponibleAcumulado = 0
        diferenciaImponible = 0
        imponibleACotizar = 0
        gCrearConRen = False


        'SIS// 
        Dim imponibleAcumuladoSis As Decimal = 0
        Dim diferenciaImponibleSis As Decimal = 0
        Dim imponibleACotizarSis As Decimal = 0

        gExesoTope = False
        gExesoTopeSis = False

        valMlRIMCotExcesoGen = 0
        valMlRIMSISExcesoGen = 0

        ''''''''LFC: SIN CONTROL RENTA: -->>>> excesos- aun no se enceuntra en produccion
        '''''''If (rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF") And gcodAdministradora = 1033 Then
        '''''''    gNoExisteConRen = True
        '''''''    Exit Sub
        '''''''End If
        '--<<<<


        '--OS:9075964 - nuevas validaciones PLV
        If blGenExcesoEnLinea And gcodAdministradora = 1032 Then

            If rTrn.tipoProducto <> "CCO" Then 'solo CCO
                gExisteConRen = False
                Exit Sub
            End If

            If rMovAcr.tipoMvto = "COP" Then 'NO considera Trabajo Pesado
                gExisteConRen = False
                Exit Sub
            End If

            If rMovAcr.tipoMvto = "COG" Then 'gratificacion de solo un periodo
                Try
                    Dim fecGratifIni, fecGratifFin, perCotizacion As Date

                    Try
                        gExisteConRen = False

                        fecGratifIni = rTrn.fecInicioGratificacion
                        fecGratifFin = rTrn.fecFinGratificacion
                        perCotizacion = rTrn.perCotizacion
                        If perCotizacion.Year = fecGratifIni.Year And perCotizacion.Month = fecGratifIni.Month Then
                            If fecGratifIni.Day = 1 And fecGratifIni >= New Date(1981, 1, 1) Then
                                If fecGratifFin.Year = fecGratifIni.Year And fecGratifFin.Month = fecGratifIni.Month Then
                                    gExisteConRen = True
                                Else
                                    gExisteConRen = False
                                    Exit Sub
                                End If
                            End If
                        Else
                            gExisteConRen = False
                            Exit Sub
                        End If
                    Catch
                        gExisteConRen = False
                        Exit Sub
                    End Try
                Catch
                    gExisteConRen = False
                    Exit Sub
                End Try
            End If
        Else
            ' otras administradoras
            '--.--OS:1753303-->>>>
            If rMovAcr.tipoMvto = "COG" Or rMovAcr.tipoMvto = "COS" Then
                gExisteConRen = False
                Exit Sub
            End If
            '--<<<<
            '--OS:2035792 -->>>>>
            'sin exceso para CAV y CCV .deposito directo
            If rTrn.codMvto = 410290 Or rTrn.codMvto = 210290 Or _
               rTrn.codMvto = 610290 Then 'lfc: 15/10/09 CA-2009100123  CAF-depos.direct
                gExisteConRen = False
                Exit Sub
            End If
            '--<<<<
            Exit Sub
        End If

        If CDate(rTrn.perCotizacion) > rTrn.fecAcreditacion Or rTrn.perCotizacion < gFecInicioSistema Or rTrn.tipoPago = 3 Then
            '''Adelantados no Tienen Excesos OS-6475538. PCI 18/08/2014
            Exit Sub
        End If


        'tope para el tipo de producto
        'dsAux = parParametrosAcred.traer(gdbc,New Object() {"VID_ADM", "VTIPO_PRODUCTO"}, New Object() {gidAdm, rTrn.tipoProducto}, New Object() {"INTEGER", "STRING"})
        dsAux = Sys.IngresoEgreso.Rentas.ControlRentas.traerRentaTopeUf(gdbc, gidAdm, rTrn.tipoProducto, rTrn.perCotizacion)
        If dsAux.Tables(0).Rows.Count = 0 Then
            Exit Sub
        Else
            rTope = New ccAcrParametrosAcred(dsAux)
            limiteExcesoUF = dsAux.Tables(0).Rows(0).Item("VAL_UF_LIMITE_EXCESO")
            topeImponibleUF = dsAux.Tables(0).Rows(0).Item("VAL_UF_RENTA_IMPONIBLE_TOPE")
        End If

        If rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF" Then

            topeImponiblePes = Math.Round(topeImponibleUF * gvalorUF, 0)
            '--.--os: 1607884 

            If gcodAdministradora = 1032 And blGenExcesoEnLinea Then
                If gExisteConRen Then
                    imponibleAcumulado = rConRen.valMlRentaAcum + rTrn.valMlRentaImponible
                Else
                    imponibleAcumulado = rTrn.valMlRentaImponible
                End If

                If gExisteConRenSIS Then
                    imponibleAcumuladoSis = rConRenSIS.valMlRentaAcum + rTrn.valMlRentaImponibleSis
                Else
                    imponibleAcumuladoSis = rTrn.valMlRentaImponibleSis
                End If

            Else
                imponibleAcumulado = rConRen.valMlRentaAcum + rTrn.valMlRentaImponible
                imponibleAcumuladoSis = rConRenSIS.valMlRentaAcum + rTrn.valMlRentaImponibleSis
            End If



            'De acuerdo a lo solicitado por Ruben Araya y Jorge Jorquera, la renta
            'acumulada para otras cotizaciones no se utiliza para determinación del
            'exceso. solo el exceso que produce la cotización por si sola
            'segun mail del 23/08/2006 . RCZ

            'imponibleAcumulado = rTrn.valMlRentaImponible


            '***CA-2009080313*********************--------------------->>>>>>>>>>>>>>>>>>>
            ' nueva definición..... 
            'sin calculo de excesos para CCO---
            'Ayer se realizó reunión con Marcelo Schliebener y se cambió la definición de la generación de excesos 
            'por tope UF 60. Esta contempla que no se generen excesos para pagos por más de un empleador. 
            'Además se definió que sólo se deben generar excesos por tope UF 60 para el caso en que se 
            'declare una renta imponible mayor al tope y cuya cotización pagada tenga relación con dicha renta. 
            If rTrn.tipoProducto = "CCO" Then

                If gcodAdministradora = 1032 And blGenExcesoEnLinea Then
                    'PARA PLANVITAL SE CONTROLA EL EXCESO EN LINEA
                Else

                    imponibleAcumulado = rTrn.valMlRentaImponible
                    imponibleAcumuladoSis = rTrn.valMlRentaImponibleSis

                End If
                'solo para Modelo, verificar si los montos informados se enceuntran por el tope,
                ' estos ya serían calculado por la Recaudacion, pero la renta que viene es la informada no la calculada
                If (gcodAdministradora = 1034 Or gcodAdministradora = 1035) And _
                (rTrn.codOrigenProceso = "RECAUDAC" Or rTrn.codOrigenProceso = "REREZMAS" Or rTrn.codOrigenProceso = "REREZSEL") Then
                    Dim valorCot As Decimal

                    valorCot = Mat.Redondear(topeImponiblePes * rTrn.tasaCotizacion)
                    valorCot = valorCot - rTrn.valMlMvto

                    If Math.Abs(valorCot) <= 500 Then
                        blRecalculaInteres = False ' no cambiarán los interes, asumir lo informado
                        Exit Sub
                    End If

                End If


            End If   '***********************************---------------------<<<<<<<<<<<<<

            diferenciaImponible = imponibleAcumulado - topeImponiblePes
            diferenciaImponibleSis = imponibleAcumuladoSis - topeImponiblePes

            If diferenciaImponible > 0 Or diferenciaImponibleSis > 0 Then
                If diferenciaImponible > 0 Then
                    imponibleACotizar = rTrn.valMlRentaImponible - diferenciaImponible

                    If imponibleACotizar <= 0 Then
                        valMlRIMCotExcesoGen = rTrn.valMlRentaImponible
                    Else
                        valMlRIMCotExcesoGen = diferenciaImponible
                    End If

                    gExesoTope = True
                End If

                If diferenciaImponibleSis > 0 Then
                    imponibleACotizarSis = rTrn.valMlRentaImponibleSis - diferenciaImponibleSis
                    If imponibleACotizarSis <= 0 Then
                        valMlRIMSISExcesoGen = rTrn.valMlRentaImponible
                    Else
                        valMlRIMSISExcesoGen = diferenciaImponibleSis
                    End If

                    gExesoTopeSis = True
                End If

                If imponibleACotizar < 0 Then imponibleACotizar = 0

                If imponibleACotizarSis < 0 Then imponibleACotizarSis = 0


                If diferenciaImponible >= 0 Or diferenciaImponibleSis >= 0 Then
                    'SIS--- modificacion de la rutina----
                    CalcularExcesosGen(imponibleACotizar, imponibleACotizarSis, limiteExcesoUF)

                    If gcodAdministradora = 1032 And blGenExcesoEnLinea Then
                        rConRen.valMlRentaAcum = rConRen.valMlRentaAcum + rTrn.valMlRentaImponible
                        rConRenSIS.valMlRentaAcum = rConRenSIS.valMlRentaAcum + rTrn.valMlRentaImponibleSis
                    Else
                        rConRen.valMlRentaAcum = rConRen.valMlRentaAcum + imponibleACotizar
                        rConRenSIS.valMlRentaAcum = rConRenSIS.valMlRentaAcum + imponibleACotizarSis
                    End If


                    'Controla Error por Division por 0. PCI 23/01/2012
                    If gvalorUF = 0 Then
                        blIgnorar = True
                        rTrn.codError = 7440 'Valor UF en CERO.
                        GenerarLog("A", 7440, "3.- Valor UF en CERO. Producto: " & rTrn.tipoProducto & ", Per Cotizacion:" & rTrn.perCotizacion, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                        Exit Sub
                    End If
                    gCrearConRen = True

                    rConRen.valUfRentaAcum = Mat.Redondear(rConRen.valMlRentaAcum / gvalorUF, 2)
                    rConRenSIS.valUfRentaAcum = Mat.Redondear(rConRenSIS.valMlRentaAcum / gvalorUF, 2)

                End If

            Else
                rConRen.valMlRentaAcum = rConRen.valMlRentaAcum + rTrn.valMlRentaImponible
                rConRenSIS.valMlRentaAcum = rConRenSIS.valMlRentaAcum + rTrn.valMlRentaImponibleSis

                'Controla Error por Division por 0. PCI 23/01/2012
                If gvalorUF = 0 Then
                    blIgnorar = True
                    rTrn.codError = 7440 'Valor UF en CERO.
                    GenerarLog("A", 7440, "4.- Valor UF en CERO. Producto: " & rTrn.tipoProducto & ", Per Cotizacion:" & rTrn.perCotizacion, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                    Exit Sub
                End If

                gCrearConRen = True
                rConRen.valUfRentaAcum = Mat.Redondear(rConRen.valMlRentaAcum / gvalorUF, 2)
                rConRenSIS.valUfRentaAcum = Mat.Redondear(rConRenSIS.valMlRentaAcum / gvalorUF, 2)

            End If

        ElseIf rTrn.tipoProducto = "CCV" Then
            '---------------------------------------------------------------------------
            Dim rentaUf As Decimal
            Dim excesoUf As Decimal = 0


            MontoCotizacion = rTrn.valMlPatrFrecCal - rTrn.valMlTransferenciaCal
            'rentaUf = rConRen.valUfRentaAcum + Mat.Redondear(MontoCotizacion / gvalorUF, 2)

            'Controla Error por Division por 0. PCI 23/01/2012
            If gvalorUF = 0 Then
                blIgnorar = True
                rTrn.codError = 7440 'Valor UF en CERO.
                GenerarLog("A", 7440, "5.- Valor UF en CERO. Producto: " & rTrn.tipoProducto & ", Per Cotizacion:" & rTrn.perCotizacion, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                Exit Sub
            Else
                rentaUf = rConRen.valUfRentaAcum + Mat.Redondear(MontoCotizacion / gvalorUF, 2)
            End If


            gCrearConRen = True

            If rentaUf > rTope.valUfRentaImponibleTope Then

                CalcularExcesosGen(MontoCotizacion, 0, rTope.valUfLimiteExceso)

            End If

            excesoUf = Mat.Redondear(rTrn.valMlExcesoTopeCal / gvalorUF, 2)

            If excesoUf > 0 Then

                rConRen.valUfRentaAcum = rTope.valUfRentaImponibleTope
                rConRen.valMlRentaAcum += rTrn.valMlMvtoCal + rTrn.valMlReajusteCal + rTrn.valMlInteresCal

            Else

                rConRen.valUfRentaAcum += rentaUf
                rConRen.valMlRentaAcum += MontoCotizacion
            End If

        End If

    End Sub





    Private Sub CalcularExcesosGen(ByVal ImponibleACotizar As Decimal, ByVal ImponibleACotizarSis As Decimal, ByVal limiteExcesoUF As Decimal)

        Dim TasaInt As Decimal = 0
        Dim TasaRea As Decimal = 0

        'montos con la nueva renta
        Dim valMlCot As Decimal = 0
        Dim valCuoCot As Decimal = 0
        Dim valMlInt As Decimal = 0
        Dim valCuoInt As Decimal = 0
        Dim valMlRea As Decimal = 0
        Dim valCuoRea As Decimal = 0
        Dim valMlAdi As Decimal = 0
        Dim valCuoAdi As Decimal = 0
        Dim valMlAdiInt As Decimal = 0
        Dim valCuoAdiInt As Decimal = 0
        Dim valMlAdiRea As Decimal = 0
        Dim valCuoAdiRea As Decimal = 0

        ' diff entre el informado y el calculado con la nueva renta
        Dim valMlCotDif As Decimal = 0
        Dim valMlAdiDif As Decimal = 0
        Dim valMlIntDif As Decimal = 0
        Dim valMlReaDif As Decimal = 0
        Dim valMlAdiintDif As Decimal = 0
        Dim valMlAdireaDif As Decimal = 0
        Dim valUfExceso As Decimal = 0
        Dim valMLExceso As Decimal = 0
        Dim valMLExcesoEmpl As Decimal = 0
        Dim valMlPrima As Decimal = 0
        Dim valMlPrimaSisInt As Decimal = 0
        Dim valMlPrimaSisRea As Decimal = 0
        Dim valMlPrimaIntDif As Decimal = 0
        Dim valMlPrimaReaDif As Decimal = 0
        Dim valMlPrimaDif As Decimal = 0


        Dim valMlExcesoGenerado As Decimal = 0
        Dim valMlAdicionalCal As Decimal = rTrn.valMlAdicionalCal


        If rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF" Then

            'INI PCI OS-6741433. 16/12/2014
            If rTrn.tasaInteres = 0 And rTrn.tasaReajuste = 0 And _
               (rTrn.codOrigenProceso = "RECAUDAC" Or rTrn.codOrigenProceso = "REREZMAS" Or rTrn.codOrigenProceso = "REREZSEL") And _
               rTrn.valMlInteres > 0 Then

				' dsAux = Sys.IngresoEgreso.ParametrosINE.Tasas.traer(gdbc, gidAdm, rTrn.perCotizacion, rTrn.fecOperacion)
				'28/08/2020 - EJS - CA-6911422 - (PLI-6849249,MOD-2020050053,UNO-6907607) Calculo Intereses Ley proteccion del empleo -->>>-- INI
				'05/10/2020 - EJS - CA-7597812 - Incluye Planillas tipo 45 Pago de Convenios por Ley de Protección al empleo
				If rTrn.tipoPlanilla = 42 Or rTrn.tipoPlanilla = 43 Or rTrn.tipoPlanilla = 44 Or rTrn.tipoPlanilla = 45 Then
					dsAux = Sys.IngresoEgreso.ParametrosINE.Tasas.traer_ley_21227(gdbc, gidAdm, rTrn.perCotizacion, rTrn.fecOperacion)
				Else
					dsAux = Sys.IngresoEgreso.ParametrosINE.Tasas.traer(gdbc, gidAdm, rTrn.perCotizacion, rTrn.fecOperacion)
				End If


				If dsAux.Tables(0).Rows.Count > 0 Then
					rTrn.tasaInteres = dsAux.Tables(0).Rows(0).Item("TASA_INTERES")
					rTrn.tasaReajuste = dsAux.Tables(0).Rows(0).Item("TASA_REAJUSTE")

					'lfc:05-2017 - interes y reajuste elevado
					If rTrn.tasaInteres > 0 Then rTrn.tasaInteres = Mat.Redondear(rTrn.tasaInteres / 100, 5)
					If rTrn.tasaReajuste > 0 Then rTrn.tasaReajuste = Mat.Redondear(rTrn.tasaReajuste / 100, 5)

					blRecalculaInteres = True				   'lfc:recalcular por la tasa de interes


					'05/10/2020 - EJS - CA-7597812 - Incluye Planillas tipo 45 Pago de Convenios por Ley de Protección al empleo ->>>- INI
					If rTrn.tipoPlanilla = 45 Then
						blRecalculaInteres = False						 ' no cambiarán los interes, asumir lo informado
					End If
					'05/10/2020 - EJS - CA-7597812 - Incluye Planillas tipo 45 Pago de Convenios por Ley de Protección al empleo -<<<- FIN

				End If
			End If
            'FIN PCI OS-6741433. 16/12/2014

            If gExesoTope Then
                valMlCot = Mat.Redondear(ImponibleACotizar * rTrn.tasaCotizacion, 0)
                valMlInt = Mat.Redondear(valMlCot * rTrn.tasaInteres, 0)
                valMlRea = Mat.Redondear(valMlCot * rTrn.tasaReajuste, 0)
            Else
                valMlCot = rTrn.valMlMvtoCal
                valMlInt = rTrn.valMlInteresCal
                valMlRea = rTrn.valMlReajusteCal
            End If

            If rTrn.valCuoAdicionalCal > 0 And gExesoTope Then
                valMlAdi = Mat.Redondear(ImponibleACotizar * rTrn.tasaAdicional, 0)
            Else
                valMlAdi = rTrn.valMlAdicionalCal
                valMlAdiInt = rTrn.valMlAdicionalInteresCal
                valMlAdiRea = rTrn.valMlAdicionalReajusteCal
            End If

            ''SIS//
            If rTrn.valCuoPrimaSisCal > 0 And gExesoTopeSis Then
                valMlPrima = Mat.Redondear(ImponibleACotizarSis * rTrn.tasaPrima, 0)
                valMlPrimaSisInt = Mat.Redondear(valMlPrima * rTrn.tasaInteres, 0)    'TASA INTERES PRIMA
                valMlPrimaSisRea = Mat.Redondear(valMlPrima * rTrn.tasaReajuste, 0)   'TASA REAJUSTE PRIMA
            Else
                valMlPrima = rTrn.valMlPrimaSisCal
                valMlPrimaSisInt = rTrn.valMlPrimaSisInteresCal
                valMlPrimaSisRea = rTrn.valMlPrimaSisReajusteCal
            End If

        Else
            valMlCot = ImponibleACotizar
        End If


        'LFC:14-05-2018, VALORES NEGATIVOS CUANDO PAGA DE MENOS
        'COTIZACION
        If valMlCot > rTrn.valMlMvtoCal Then valMlCot = rTrn.valMlMvtoCal
        If valMlInt > rTrn.valMlInteresCal Then valMlInt = rTrn.valMlInteresCal
        If valMlRea > rTrn.valMlReajuste Then valMlRea = rTrn.valMlReajuste

        valMlCotDif = Math.Abs(rTrn.valMlMvtoCal - valMlCot)
        valMlIntDif = Math.Abs(rTrn.valMlInteresCal - valMlInt)
        valMlReaDif = Math.Abs(rTrn.valMlReajuste - valMlRea)

        valMlExcesoGenerado = valMlCotDif + valMlIntDif + valMlReaDif

        'ADICIONAL
        If valMlAdi > rTrn.valMlAdicionalCal Then
            'sumar al adicional el exceso hasta completar

            If valMlAdi >= valMlExcesoGenerado + rTrn.valMlAdicionalCal Then
                rTrn.valMlAdicionalCal += valMlExcesoGenerado
                valMlAdi = rTrn.valMlAdicionalCal
                valMlExcesoGenerado = 0
                valMlCotDif = 0
                valMlIntDif = 0
                valMlReaDif = 0
            Else
                ' si lo que debio pagar es menor a la suma del adicional + exceso
                valMlExcesoGenerado = valMlExcesoGenerado - Math.Abs(valMlAdi - rTrn.valMlAdicionalCal)
                rTrn.valMlAdicionalCal = valMlAdi
                valMlCotDif = valMlExcesoGenerado
                valMlIntDif = 0
                valMlReaDif = 0
            End If
            ''comento 30-05-2018 valMlAdi = rTrn.valMlAdicionalCal

        End If

        If valMlAdiInt > rTrn.valMlAdicionalInteresCal Then valMlAdiInt = rTrn.valMlAdicionalInteresCal
        If valMlAdiRea > rTrn.valMlAdicionalReajuste Then valMlAdiRea = rTrn.valMlAdicionalReajuste
        valMlAdiDif = Math.Abs(rTrn.valMlAdicionalCal - valMlAdi)
        valMlAdiintDif = Math.Abs(rTrn.valMlAdicionalInteresCal - valMlAdiInt)
        valMlAdireaDif = Math.Abs(rTrn.valMlAdicionalReajuste - valMlAdiRea)

        'SIS//
        If valMlPrima > rTrn.valMlPrimaSisCal Then valMlPrima = rTrn.valMlPrimaSisCal
        If valMlPrimaSisInt > rTrn.valMlPrimaSisInteresCal Then valMlPrimaSisInt = rTrn.valMlPrimaSisInteresCal
        If valMlPrimaSisRea > rTrn.valMlPrimaSisReajusteCal Then valMlPrimaSisRea = rTrn.valMlPrimaSisReajusteCal
        valMlPrimaDif = Math.Abs(rTrn.valMlPrimaSisCal - valMlPrima)
        valMlPrimaIntDif = Math.Abs(rTrn.valMlPrimaSisInteresCal - valMlPrimaSisInt)
        valMlPrimaReaDif = Math.Abs(rTrn.valMlPrimaSisReajusteCal - valMlPrimaSisRea)



        If rTrn.perCotizacion >= New Date(2009, 7, 1).Date And (rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF") Then

            If gcodAdministradora = 1032 And blGenExcesoEnLinea Then  'LFC:EXCESO EN LINEA PARA PLV
                valMLExceso = valMlCotDif + valMlAdiDif + valMlIntDif + valMlReaDif + valMlAdiintDif + valMlAdireaDif + rTrn.valMlExcesoLinea 'lfc:09-08-2018 sumar el exceso en linea al exceso calculado
                valMLExcesoEmpl = valMlPrimaDif + valMlPrimaIntDif + valMlPrimaReaDif

            Else
                valMLExceso = valMlCotDif + valMlAdiDif + valMlIntDif + valMlReaDif + _
                                                                 valMlAdiintDif + valMlAdireaDif + _
                                                                 valMlPrimaDif + valMlPrimaIntDif + valMlPrimaReaDif '- se agrega SIS
            End If




        Else
            valMLExceso = valMlCotDif + valMlAdiDif + valMlIntDif + valMlReaDif + _
                               valMlAdiintDif + valMlAdireaDif
        End If

        'OS-6406480. PCI 29/07/2014. Se verifica existencia de Valro UF. Si no existe deja exceso en 0.
        'Cuando las transacciones tienen Cod. MVtos 410290, 410291, 210290, 210291, 610290, 610291 El valor UF es 0.
        If valMLExceso > 0 Then
            'Controla Error por Division por 0. PCI 23/01/2012
            If gvalorUF = 0 Then
                blIgnorar = True
                rTrn.codError = 7440 'Valor UF en CERO.
                GenerarLog("A", 7440, "4.- Valor UF en CERO. Producto: " & rTrn.tipoProducto & ", Per Cotizacion:" & rTrn.perCotizacion, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                Exit Sub
            Else
                valUfExceso = Mat.Redondear(valMLExceso / gvalorUF, 2)
            End If
        End If

        'valUfExceso = Mat.Redondear(valMLExceso / gvalorUF, 2)

        If valUfExceso <= limiteExcesoUF Then
            valMLExceso = 0
            gExesoTope = False
            valMlCot = rTrn.valMlMvto
            valMlInt = rTrn.valMlInteres
            valMlRea = rTrn.valMlReajuste
            valMlAdi = rTrn.valMlAdicional

            rTrn.valMlAdicionalCal = valMlAdicionalCal

            valMlAdiInt = rTrn.valMlAdicionalInteres
            valMlAdiRea = rTrn.valMlAdicionalReajuste
            valMLExceso = rTrn.valMlExcesoLinea

            If valMLExcesoEmpl = 0 Then
                gExesoTopeSis = False
                Exit Sub
            End If
        End If


        If gcodAdministradora = 1032 And blGenExcesoEnLinea Then 'LFC:EXCESO EN LINEA PARA PLV
            valMLExcesoEmpl = valMLExcesoEmpl + rTrn.valMlExcesoEmpl
        Else
            valMLExcesoEmpl = rTrn.valMlExcesoEmpl
        End If



        'SIS//
        If rTrn.perCotizacion >= New Date(2009, 7, 1).Date And (rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF") Then

            If rTrn.tipoProducto = "CAF" Then
                valMlCot += valMLExceso
                valMLExceso = 0
            End If

            CalcularNominales2(valMlCot, _
                            valMlInt, _
                            valMlRea, _
                            valMlAdi, _
                            valMlAdiInt, _
                            valMlAdiRea, _
                            valMLExceso, _
                                    valMlPrima, valMlPrimaSisInt, valMlPrimaSisRea, valMLExcesoEmpl)
        Else
            CalcularNominales(valMlCot, _
                               valMlInt, _
                               valMlRea, _
                               valMlAdi, _
                               valMlAdiInt, _
                               valMlAdiRea, _
                               valMLExceso, valMLExcesoEmpl)
        End If


        If rTrn.valMlExcesoTopeCal < 0 Or rTrn.valCuoExcesoTopeCal < 0 Then
            blErrorFatal = True
            blIgnorar = True
            GenerarLog("E", 15337, "Hebra " & gIdHebra & " - Valores excesos negativos", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            'Throw New SondaException(15337) 'Valores excesos negativos
        End If

    End Sub

    Private Sub CalcularSaldo()
        Dim i As Integer
        Dim montos As Decimal

        Dim gValMlComisionTrf As Decimal = 0
        Dim gValCuoComisionTrf As Decimal = 0


        rTrn.valMlSaldo = sMov.valMlSaldoFinal
        rTrn.valCuoSaldo = sMov.valCuoSaldoFinal
        rTrn.valMlSaldoAnterior = rSal.valMlSaldo
        rTrn.valCuoSaldoAnterior = rSal.valCuoSaldo


        For i = 0 To sMov.count - 1

            If gtipoProceso = "AC" Then

                If IsNothing(sMov.item(i).mov.perCotizacion) Then
                    sMov.item(i).mov.perCotizacion = sMov.item(i).mov.perContable
                End If
                If IsNothing(sMov.item(i).mov.fecOperacion) Then
                    sMov.item(i).mov.fecOperacion = sMov.item(i).mov.fecAcreditacion
                End If
                If IsNothing(sMov.item(i).mov.numReferenciaOrigen5) Then
                    sMov.item(i).mov.numReferenciaOrigen5 = rTrn.numeroId
                End If
                If (sMov.item(i).mov.numReferenciaOrigen5 = 0) Then
                    sMov.item(i).mov.numReferenciaOrigen5 = rTrn.numeroId
                End If

                gValMlComisionTrf = sMov.item(i).mov.valMlComisionPorcentual
                gValCuoComisionTrf = sMov.item(i).mov.valCuoComisionPorcentual
                ''''periodos nuevos, transf de comision
                If rTrn.perCotizacion >= New Date(2009, 7, 1).Date And (rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF") And _
                   gAdicionalSeTransfiere And (rTrn.codMvtoComPor = "120703" Or rTrn.codMvtoComPor = "620703") Then

                    If sMov.item(i).tipoMvto = 0 Then

                        gValMlComisionTrf = 0
                        gValCuoComisionTrf = 0

                    End If
                End If

                montos = 0
                montos = sMov.item(i).mov.valMlMvto + sMov.item(i).mov.valMlMvtoInteres + sMov.item(i).mov.valMlMvtoReajuste + _
                       sMov.item(i).mov.valCuoMvto + sMov.item(i).mov.valCuoMvtoInteres + sMov.item(i).mov.valCuoMvtoReajuste + _
                       sMov.item(i).mov.valMlAdicional + sMov.item(i).mov.valMlAdicionalInteres + sMov.item(i).mov.valMlAdicionalReajuste + _
                       sMov.item(i).mov.valCuoAdicional + sMov.item(i).mov.valCuoAdicionalInteres + sMov.item(i).mov.valCuoAdicionalReajuste + _
                       sMov.item(i).mov.valMlComisionPorcentual + sMov.item(i).mov.valMlComisionFija + _
                       sMov.item(i).mov.valCuoComisionPorcentual + sMov.item(i).mov.valCuoComisionFija

                'lfc://11-11-2009 ----CA-2009090197--------------->>>>>>>>>>>>>>>>>> insert monto=0
                ' lfc:// actualizacion por CA-2009120076, traspaso egreso cta, genera mvto con monto=0
                '--LFC:10-11-2016 SE AÑADE TRAIPAGN para pagos solo SIS
                If (sMov.item(i).mov.tipoProducto = "CAF" Or sMov.item(i).mov.tipoProducto = "CCO") And sMov.item(i).mov.perCotizacion >= New Date(2009, 7, 1).Date _
                   And (sMov.item(i).mov.codOrigenProceso = "RECAUDAC" _
                   Or sMov.item(i).mov.codOrigenProceso = "REREZSEL" _
                   Or sMov.item(i).mov.codOrigenProceso = "REREZMAS" _
                   Or sMov.item(i).mov.codOrigenProceso = "TRAIPAGN") _
                   And montos = 0 _
                   And Not PlanillaDnp() Then 'par_ing_tipos_planilla. origen=2

                    sMov.item(i).mov.seqMvto = 0

                Else
                    'Independientes III
                    dsAux = ParametrosINE.MvtoAcreditacion.traer(gdbc, gidAdm, sMov.item(i).mov.codMvto)
                    'dsAux = parCodMvto.traer(gdbc, New Object() {"VID_ADM", "VCOD_MVTO"}, New Object() {gidAdm, rTrn.codMvto}, New Object() {"INTEGER", "STRING"})

                    If dsAux.Tables(0).Rows.Count = 0 Then
                        rMovAcr2 = Nothing
                        rMovAcr2 = New ccAcrMvtoAcreditacion(dsAux.Tables(0).NewRow)
                    Else
                        rMovAcr2 = Nothing
                        rMovAcr2 = New ccAcrMvtoAcreditacion(dsAux)
                    End If

                    If (sMov.item(i).mov.tipoProducto = "CCO" And sMov.item(i).mov.tipoImputacion = "ABO" And _
                       (rMovAcr2.tipoMvto = "COT" Or rMovAcr2.tipoMvto = "CTP") And _
                       ((sMov.item(i).mov.codOrigenProceso = "RECAUDAC" And sMov.item(i).mov.tipoPlanilla = 40) Or _
                       ((sMov.item(i).mov.codOrigenProceso = "REREZMAS" Or sMov.item(i).mov.codOrigenProceso = "REREZSEL" Or _
                        sMov.item(i).mov.codOrigenProceso = "TRAINREZ" Or sMov.item(i).mov.codOrigenProceso = "TRAIPAGN" Or _
                        sMov.item(i).mov.codOrigenProceso = "ACREXTGR") And (rTrn.tipoRezago = 35 Or rTrn.tipoRezago = 36 Or rTrn.tipoRezago = 39)))) Then

						sMov.item(i).mov.seqMvto = SaldosMovimientos.crearMvtoTgr(gdbc, gidAdm, sMov.item(i).mov.idCliente, sMov.item(i).mov.numSaldo, _
														  sMov.item(i).mov.seqRegistroTransaccion, sMov.item(i).mov.tipoProducto, sMov.item(i).mov.tipoFondo, _
														  sMov.item(i).mov.perCotizacion, sMov.item(i).mov.tipoImputacion, sMov.item(i).mov.tipoRemuneracion, _
														  sMov.item(i).mov.tipoPago, sMov.item(i).mov.tipoPlanilla, sMov.item(i).mov.tipoEntPagadora, _
														  sMov.item(i).mov.numReferenciaOrigen1, sMov.item(i).mov.numReferenciaOrigen2, sMov.item(i).mov.numReferenciaOrigen3, _
														  sMov.item(i).mov.numReferenciaOrigen4, sMov.item(i).mov.numReferenciaOrigen5, sMov.item(i).mov.numReferenciaOrigen6, sMov.item(i).mov.fecOperacion, sMov.item(i).mov.idEmpleador, sMov.item(i).mov.folioConvenio, _
														  sMov.item(i).mov.numCuotasPactadas, sMov.item(i).mov.numCuotasPagadas, sMov.item(i).mov.idAlternativoDoc, sMov.item(i).mov.valMlRentaImponible, _
														  sMov.item(i).mov.fecAcreditacion, sMov.item(i).mov.perContable, sMov.item(i).mov.seqMvtoOrigen, sMov.item(i).mov.codOrigenMvto, _
														  sMov.item(i).mov.codOrigenProceso, sMov.item(i).mov.codMvto, sMov.item(i).mov.codMvtoAdi, sMov.item(i).mov.codMvtoIntreaCue, _
														  sMov.item(i).mov.codMvtoIntreaAdi, sMov.item(i).mov.codMvtoComPor, sMov.item(i).mov.codMvtoComFij, sMov.item(i).mov.idAdmOrigen, _
														  sMov.item(i).mov.fecValorCuota, sMov.item(i).mov.valMlValorCuota, sMov.item(i).mov.valMlMvto, sMov.item(i).mov.valMlMvtoInteres, _
														  sMov.item(i).mov.valMlMvtoReajuste, sMov.item(i).mov.valCuoMvto, sMov.item(i).mov.valCuoMvtoInteres, sMov.item(i).mov.valCuoMvtoReajuste, _
														  sMov.item(i).mov.valMlAdicional, sMov.item(i).mov.valMlAdicionalInteres, sMov.item(i).mov.valMlAdicionalReajuste, _
														  sMov.item(i).mov.valCuoAdicional, sMov.item(i).mov.valCuoAdicionalInteres, sMov.item(i).mov.valCuoAdicionalReajuste, _
														  gValMlComisionTrf, _
														  gValCuoComisionTrf, _
														  sMov.item(i).mov.valMlComisionFija, sMov.item(i).mov.valCuoComisionFija, sMov.item(i).mov.valMlCuotaComision, sMov.item(i).mov.valMlSaldo, sMov.item(i).mov.valCuoSaldo, sMov.item(i).mov.numDictamen, _
														  sMov.item(i).mov.indMvtoVisibleCartola, sMov.item(i).mov.perCuatrimestre, gidUsuarioProceso, gfuncion, sMov.item(i).mov.tipoOrigenDigitacion, sMov.item(i).mov.claseCotizante, sMov.item(i).mov.codActividadEconomica)


                    Else
						sMov.item(i).mov.seqMvto = SaldosMovimientos.crearConSeq(gdbc, gidAdm, sMov.item(i).mov.idCliente, sMov.item(i).mov.numSaldo, _
							 sMov.item(i).mov.seqRegistroTransaccion, sMov.item(i).mov.tipoProducto, sMov.item(i).mov.tipoFondo, _
							  sMov.item(i).mov.perCotizacion, sMov.item(i).mov.tipoImputacion, sMov.item(i).mov.tipoRemuneracion, _
							  sMov.item(i).mov.tipoPago, sMov.item(i).mov.tipoPlanilla, sMov.item(i).mov.tipoEntPagadora, _
							  sMov.item(i).mov.numReferenciaOrigen1, sMov.item(i).mov.numReferenciaOrigen2, sMov.item(i).mov.numReferenciaOrigen3, _
							  sMov.item(i).mov.numReferenciaOrigen4, sMov.item(i).mov.numReferenciaOrigen5, sMov.item(i).mov.numReferenciaOrigen6, sMov.item(i).mov.fecOperacion, sMov.item(i).mov.idEmpleador, sMov.item(i).mov.folioConvenio, _
							  sMov.item(i).mov.numCuotasPactadas, sMov.item(i).mov.numCuotasPagadas, sMov.item(i).mov.idAlternativoDoc, sMov.item(i).mov.valMlRentaImponible, _
							  sMov.item(i).mov.fecAcreditacion, sMov.item(i).mov.perContable, sMov.item(i).mov.seqMvtoOrigen, sMov.item(i).mov.codOrigenMvto, _
							  sMov.item(i).mov.codOrigenProceso, sMov.item(i).mov.codMvto, sMov.item(i).mov.codMvtoAdi, sMov.item(i).mov.codMvtoIntreaCue, _
							  sMov.item(i).mov.codMvtoIntreaAdi, sMov.item(i).mov.codMvtoComPor, sMov.item(i).mov.codMvtoComFij, sMov.item(i).mov.idAdmOrigen, _
							  sMov.item(i).mov.fecValorCuota, sMov.item(i).mov.valMlValorCuota, sMov.item(i).mov.valMlMvto, sMov.item(i).mov.valMlMvtoInteres, _
							  sMov.item(i).mov.valMlMvtoReajuste, sMov.item(i).mov.valCuoMvto, sMov.item(i).mov.valCuoMvtoInteres, sMov.item(i).mov.valCuoMvtoReajuste, _
							  sMov.item(i).mov.valMlAdicional, sMov.item(i).mov.valMlAdicionalInteres, sMov.item(i).mov.valMlAdicionalReajuste, _
							  sMov.item(i).mov.valCuoAdicional, sMov.item(i).mov.valCuoAdicionalInteres, sMov.item(i).mov.valCuoAdicionalReajuste, _
							  gValMlComisionTrf, _
							  gValCuoComisionTrf, _
							  sMov.item(i).mov.valMlComisionFija, sMov.item(i).mov.valCuoComisionFija, sMov.item(i).mov.valMlCuotaComision, sMov.item(i).mov.valMlSaldo, sMov.item(i).mov.valCuoSaldo, sMov.item(i).mov.numDictamen, _
							  sMov.item(i).mov.indMvtoVisibleCartola, sMov.item(i).mov.perCuatrimestre, gidUsuarioProceso, gfuncion, sMov.item(i).mov.tipoOrigenDigitacion, sMov.item(i).mov.claseCotizante, sMov.item(i).mov.codActividadEconomica)

                    End If
                End If


                'End If
                Select Case sMov.item(i).tipoMvto
                    Case 0 : rTrn.seqMvtoDestino = sMov.item(i).mov.seqMvto
                    Case 1 : rTrn.seqDestinoExcesoTopeCal = sMov.item(i).mov.seqMvto
                    Case 2 : rTrn.seqDestinoCompenCal = sMov.item(i).mov.seqMvto
                    Case 3 : rTrn.seqDestinoTrfCal = sMov.item(i).mov.seqMvto
                    Case 4 : rTrn.seqPrima = sMov.item(i).mov.seqMvto 'SIS//-- secuencia acr_saldos_mvtos.
                    Case 5 : rTrn.seqExcesoEmpl = sMov.item(i).mov.seqMvto
                End Select
            End If
            clsSal.imputar(rSal.numSaldo, sMov.item(i).valCuoAbonos, "ABO")
            clsSal.imputar(rSal.numSaldo, sMov.item(i).valCuoCargo, "CAR")

        Next


        '--.-- COMENTAR...para DEBUG
        Dim OBJ As Object
        OBJ = rSal.seqUltMvto
        OBJ = rTrn.seqRegistro

        If rSal.seqUltMvto = rTrn.seqRegistro Then
            Throw New Exception("Hebra " & gIdHebra & " - Transaccion ya se encuentra acreditada")
        End If

        'NCG-255
        If gtipoProceso = "AC" And rMovAcr.tipoMvto <> "NOC" Then
            InformacionCliente.modificarSaldo(gdbc, gidAdm, rTrn.idCliente, rTrn.numSaldo, rTrn.valMlSaldo, rTrn.valCuoSaldo, rSal.valUtmSaldo, rTrn.seqRegistro, gidUsuarioProceso, gfuncion)
        End If


        sMov.clear()
    End Sub


    Private Sub CalcularSaldoMov(Optional ByVal rentabilidad As Boolean = False)
        Dim i As Integer

        i = sMov.count - 1

        gvalMlAbonosCtaAcr = 0
        gvalCuoAbonosCtaAcr = 0
        gvalMlCargosCtaAcr = 0
        gvalCuoCargosCtaAcr = 0

        If sMov.item(i).mov.tipoImputacion = "ABO" Then

            gvalMlAbonosCtaAcr = sMov.item(i).mov.valMlMvto + sMov.item(i).mov.valMlMvtoInteres + sMov.item(i).mov.valMlMvtoReajuste + _
                                 sMov.item(i).mov.valMlAdicional + sMov.item(i).mov.valMlAdicionalInteres + sMov.item(i).mov.valMlAdicionalReajuste

            gvalCuoAbonosCtaAcr = sMov.item(i).mov.valCuoMvto + sMov.item(i).mov.valCuoMvtoInteres + sMov.item(i).mov.valCuoMvtoReajuste + _
                                  sMov.item(i).mov.valCuoAdicional + sMov.item(i).mov.valCuoAdicionalInteres + sMov.item(i).mov.valCuoAdicionalReajuste

            gvalMlCargosCtaAcr = sMov.item(i).mov.valMlComisionFija + sMov.item(i).mov.valMlComisionPorcentual

            gvalCuoCargosCtaAcr = sMov.item(i).mov.valCuoComisionFija + sMov.item(i).mov.valCuoComisionPorcentual

            If rentabilidad Then
                If blSaldoNegativo Then
                    If rTrn.valCuoAjusteDecimalCal <> 0 Then
                        gvalCuoAbonosCtaAcr += rTrn.valCuoAjusteDecimalCal
                    End If
                End If
            End If

        Else

            gvalMlCargosCtaAcr = sMov.item(i).mov.valMlMvto + sMov.item(i).mov.valMlMvtoInteres + sMov.item(i).mov.valMlMvtoReajuste + _
                                  sMov.item(i).mov.valMlAdicional + sMov.item(i).mov.valMlAdicionalInteres + sMov.item(i).mov.valMlAdicionalReajuste + _
                                  sMov.item(i).mov.valMlComisionFija + sMov.item(i).mov.valMlComisionPorcentual

            gvalCuoCargosCtaAcr = sMov.item(i).mov.valCuoMvto + sMov.item(i).mov.valCuoMvtoInteres + sMov.item(i).mov.valCuoMvtoReajuste + _
                                   sMov.item(i).mov.valCuoAdicional + sMov.item(i).mov.valCuoAdicionalInteres + sMov.item(i).mov.valCuoAdicionalReajuste + _
                                   sMov.item(i).mov.valCuoComisionFija + sMov.item(i).mov.valCuoComisionPorcentual

        End If

        If rentabilidad Then
            If blSaldoNegativo Then
                If rTrn.valCuoAjusteDecimalCal > 0 Then
                    gvalCuoAbonosCtaAcr += rTrn.valCuoAjusteDecimalCal
                ElseIf rTrn.valCuoAjusteDecimalCal < 0 Then
                    gvalCuoCargosCtaAcr += rTrn.valCuoAjusteDecimalCal
                End If
                blSaldoNegativo = False
            End If
        End If


        If rMovAcr.tipoMvto = "NOC" Then

        Else

            If gvalCuoAbonosCtaAcr + gvalMlAbonosCtaAcr > 0 Then
                sMov.Abonar(i, gvalMlAbonosCtaAcr, gvalCuoAbonosCtaAcr)
            End If

            If gvalCuoCargosCtaAcr + gvalMlCargosCtaAcr > 0 Then
                sMov.Cargar(i, gvalMlCargosCtaAcr, gvalCuoCargosCtaAcr)
            End If

        End If
        'sMov.item(i).mov.valMlSaldo = rSal.valMlSaldo + gvalMlAbonosCtaAcr - gvalMlCargosCtaAcr
        'sMov.item(i).mov.valCuoSaldo = rSal.valCuoSaldo + gvalCuoAbonosCtaAcr - gvalCuoCargosCtaAcr

    End Sub

    Private Sub CalcularPatrimonioFechaOperacion()

        gRegistrosEnviados = gRegistrosEnviados + 1

        If blIgnorar Then
            Exit Sub
        End If
        rTrn.valMlPatrFrecCal = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste + _
                        rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste + _
                        rTrn.valMlExcesoLinea '+ rTrn.valMlTransferencia

        rTrn.valCuoPatrFrecCal = Mat.Redondear(rTrn.valMlPatrFrecCal / rTrn.valMlValorCuotaCaja, 2)


    End Sub

    Private Sub CalcularNominales(ByVal valMlMvto As Decimal, _
                                  ByVal valMlInteres As Decimal, _
                                  ByVal valMlReajuste As Decimal, _
                                  ByVal valMlAdicional As Decimal, _
                                  ByVal valMlAdicionalInteres As Decimal, _
                                  ByVal valMlAdicionalReajuste As Decimal, _
                                  ByVal valMlExceso As Decimal, _
                                  ByVal valMlExcesoEmp As Decimal)

        Dim valDif As Decimal
        'Dim valMLDif As Decimal
        'Dim valCuoDif As Decimal

        Dim valMlCotIntRea As Decimal
        Dim valCuoCotIntRea As Decimal

        Dim valMlAdiIntRea As Decimal
        Dim valCuoAdiIntRea As Decimal

        Dim valMlExc As Decimal
        Dim valCuoExc As Decimal

        'OS-5598016 Exceso Empleador
        Dim valMlExcEmpl As Decimal
        Dim valCuoExcEmpl As Decimal

        Dim valMlCot As Decimal
        Dim valCuoCot As Decimal

        Dim valMlInt As Decimal
        Dim valCuoInt As Decimal

        Dim valMlRea As Decimal
        Dim valCuoRea As Decimal

        Dim valMlAdi As Decimal
        Dim valCuoAdi As Decimal

        Dim valMlAdiInt As Decimal
        Dim valCuoAdiInt As Decimal

        Dim valMlAdiRea As Decimal
        Dim valCuoAdiRea As Decimal

        Dim valCuoTotNominal As Decimal
        Dim valMlTotNominal As Decimal


        valMlTotNominal = rTrn.valMlPatrFrecCal '- rTrn.valMlTransferenciaCal

        If rTrn.codOrigenTransaccion = "REZ" Then
            valCuoTotNominal = rTrn.valCuoPatrFrecCal
        Else
            valCuoTotNominal = Mat.Redondear(valMlTotNominal / rTrn.valMlValorCuota, 2)
        End If

        'conceptos sumados fondo, adicional y exceso

        valMlCotIntRea = valMlMvto + valMlInteres + valMlReajuste
        valCuoCotIntRea = Mat.Redondear(valMlCotIntRea / rTrn.valMlValorCuota, 2)

        valMlAdiIntRea = valMlAdicional + valMlAdicionalInteres + valMlAdicionalReajuste

        'Adicional Antiguo. OS-7079391. OS-7243919 01/04/2016
        If blAdicionalAntiguo Then
            valMlAdiIntRea = 0
        Else
            valMlAdiIntRea = valMlAdicional + valMlAdicionalInteres + valMlAdicionalReajuste
        End If

        valCuoAdiIntRea = Mat.Redondear(valMlAdiIntRea / rTrn.valMlValorCuota, 2)

        valMlExc = valMlExceso
        valCuoExc = Mat.Redondear(valMlExc / rTrn.valMlValorCuota, 2)

        'OS-5598016 Exceso Empleador
        valMlExcEmpl = valMlExcesoEmp
        valCuoExcEmpl = Mat.Redondear(valMlExcEmpl / rTrn.valMlValorCuota, 2)

        'conceptos individuales fondo, adicional,interes, reajuste, exceso, etc

        valMlCot = valMlMvto
        valCuoCot = Mat.Redondear(valMlCot / rTrn.valMlValorCuota, 2)

        valMlInt = valMlInteres
        valCuoInt = Mat.Redondear(valCuoCotIntRea * rTrn.tasaInteres, 2)

        valMlRea = valMlReajuste
        valCuoRea = valCuoCotIntRea - (valCuoCot + valCuoInt)

        valMlAdi = valMlAdicional

        'Adicional Antiguo. OS-7079391. OS-7243919 01/04/2016
        If blAdicionalAntiguo Then
            valMlAdi = 0
        Else
            valMlAdi = valMlAdicional
        End If

        valCuoAdi = Mat.Redondear(valMlAdi / rTrn.valMlValorCuota, 2)

        valMlAdiInt = valMlAdicionalInteres
        valCuoAdiInt = Mat.Redondear(valCuoAdiIntRea * rTrn.tasaInteres, 2)

        valMlAdiRea = valMlAdicionalReajuste
        valCuoAdiRea = valCuoAdiIntRea - (valCuoAdi + valCuoAdiInt)




        rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - rTrn.valMlPatrFrecCal

        If rTrn.valMlCompensCal > 0 Then

            rTrn.valCuoCompensCal = Mat.Redondear((valMlCotIntRea + rTrn.valMlCompensCal) / rTrn.valMlValorCuota, 2) - valCuoCotIntRea

            'OS-8071348. Se agrega nuevo Calculo de Rentabilidad en cuotas para ACREXAFC. Problemas con Aproximaciones.
        If rTrn.codOrigenProceso = "ACREXAFC" And gcodAdministradora = 1034 Then
            If (valCuoCotIntRea + (Mat.Redondear(rTrn.valMlCompensCal / rTrn.valMlValorCuota, 2))) <> rTrn.valCuoPatrFdesCal Then
                rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFdesCal - rTrn.valCuoCompensCal
                rTrn.valCuoCompensCal = 0
            End If
        End If
        Else
        rTrn.valCuoCompensCal = Mat.Redondear(rTrn.valMlCompensCal / rTrn.valMlValorCuota, 2)
        'Segun BA BA-2007050232 del 24/05/2007
        'No se aplicarán perdidas si la cotizacion es insuficiente para generar abonos

        If rTrn.valCuoPatrFdesCal = 0 And rTrn.codOrigenProceso <> "ACREXSTJ" Then
            rTrn.valCuoCompensCal = 0
        Else
            If rTrn.codOrigenProceso = "ACREXSTJ" Or rTrn.codOrigenProceso = "ACREXAFC" Then
                rTrn.valCuoCompensCal = Mat.Redondear((valMlCotIntRea + rTrn.valMlCompensCal) / rTrn.valMlValorCuota, 2) - valCuoCotIntRea

                'OS-8071348. Se agrega nuevo Calculo de Rentabilidad en cuotas para ACREXAFC. Problemas con Aproximaciones.
                If rTrn.codOrigenProceso = "ACREXAFC" Then
                    If (valCuoCotIntRea + (Mat.Redondear(rTrn.valMlCompensCal / rTrn.valMlValorCuota, 2))) <> rTrn.valCuoPatrFdesCal Then
                        If rTrn.valCuoCompensCal < 0 Then
                            'INI - PCI- 07/04/2016 - OS-8520229 - Rentabilidad Negativa
                            rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFdesCal + (rTrn.valCuoCompensCal * -1)
                            'FIN - PCI- 07/04/2016 - OS-8520229 - Rentabilidad Negativa
                        Else
                            rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFdesCal + rTrn.valCuoCompensCal
                        End If
                        rTrn.valCuoCompensCal = 0
                    End If
                End If

            End If
        End If

        'lfc//20-11-2009 ca-2009070092-planillas complementarias generan monto negativo
        ' cuando se paga adicional (comision) y la rentabilidad es negativa
        'rentabilidad negativa
        If rTrn.valCuoCompensCal < 0 Then  ' solo rentabilidad negativa        

            If valCuoCotIntRea = 0 And valCuoAdiIntRea > 0 And valCuoExc = 0 _
               And (rTrn.valCuoCompensCal * -1) > rSal.valCuoSaldo And rSal.valCuoSaldo >= 0 Then
                rTrn.valCuoCompensCal = rSal.valCuoSaldo * -1
                rTrn.valMlCompensCal = Mat.Redondear(rTrn.valCuoCompensCal * rTrn.valMlValorCuota, 0)
            End If

        End If

        End If

        valMlExc += rTrn.valMlExcesoTopeCal
        valCuoExc += rTrn.valCuoExcesoTopeCal


        'valDif = rTrn.valCuoPatrFdesCal - (valCuoCotIntRea + valCuoAdiIntRea + valCuoExc + rTrn.valCuoCompensCal)
        'OS-5598016 Se agrega Exceso Empleador
        valDif = rTrn.valCuoPatrFdesCal - (valCuoCotIntRea + valCuoAdiIntRea + valCuoExc + rTrn.valCuoCompensCal + valCuoExcEmpl)

        'OS-7194707 PCI 08/04/2015 Ajustes Decimales para cuando se realizan Cargos y no Hay SALDO.
        gValDif = valDif

        rTrn.valCuoAjusteDecimalCal = 0 '--valDif


        rTrn.valMlAjusteDecimalCal = 0

        rTrn.valMlMvtoCal = valMlCot
        rTrn.valCuoMvtoCal = valCuoCot

        rTrn.valMlInteresCal = valMlInt
        rTrn.valCuoInteresCal = valCuoInt

        rTrn.valMlReajusteCal = valMlRea
        rTrn.valCuoReajusteCal = valCuoRea

        rTrn.valMlAdicionalCal = valMlAdi
        rTrn.valCuoAdicionalCal = valCuoAdi

        'Se agrega AJUSTE DECIMAL a ADICIONAL, Solo en CUOTAS.15/10/2010
        'ajustesDecimalAComisionPorc()
        'rTrn.valCuoAdicionalCal = valCuoAdi - g_valCuoAjusteDec
        'PCI
        'rTrn.valCuoAjusteDecimalCal = 0

        rTrn.valMlAdicionalInteresCal = valMlAdiInt
        rTrn.valCuoAdicionalInteresCal = valCuoAdiInt

        rTrn.valMlAdicionalReajusteCal = valMlAdiRea
        rTrn.valCuoAdicionalReajusteCal = valCuoAdiRea

        rTrn.valMlExcesoTopeCal = valMlExc
        rTrn.valCuoExcesoTopeCal = valCuoExc

        'OS-5598016 Se agrega Exceso Empleador
        rTrn.valMlExcesoEmplCal = valMlExcEmpl
        rTrn.valCuoExcesoEmplCal = valCuoExcEmpl

    End Sub

    'lfc:03/2017 nueva funcion que no utiliza la tasa de interes
    Private Function valorizarIntRea(ByVal concepto As String, ByVal totCuotasIntRea As Decimal, ByRef valCuoInteres As Decimal, ByVal valCuoReajuste As Decimal)
        Dim interes, reajuste As Decimal
        Dim totCuotas As Decimal

        totCuotasIntRea = 0 : valCuoInteres = 0

        Select Case concepto
            Case "MOV" : interes = rTrn.valMlInteres : reajuste = rTrn.valMlReajuste : totCuotas = totCuotasIntRea - rTrn.valCuoMvtoCal
            Case "ADI" : interes = rTrn.valMlAdicionalInteres : reajuste = rTrn.valMlAdicionalReajuste : totCuotas = totCuotasIntRea - rTrn.valCuoAdicionalCal
            Case "SIS" : interes = rTrn.valMlPrimaSisInteres : reajuste = rTrn.valMlPrimaSisReajuste : totCuotas = totCuotasIntRea - rTrn.valCuoPrimaSisCal
        End Select

        If interes = 0 Then
            valCuoInteres = 0
        Else
            valCuoInteres = Mat.Redondear(interes / rTrn.valMlValorCuota, 2)

            If reajuste = 0 Then ' si no existe reajuste, entonces el interes se lleva las cuotas
                valCuoInteres = totCuotas
            End If
        End If

        If reajuste = 0 Then
            valCuoReajuste = 0
        Else
            valCuoReajuste = Mat.Redondear(reajuste / rTrn.valMlValorCuota, 2)

            If interes = 0 Then ' si no existe interes, entonces el reajuste se lleva las cuotas
                valCuoReajuste = totCuotas
            Else
                'si existen ambos, entonces se ajustara el reajuste para no exceder el monto sumado
                If totCuotas <> (valCuoInteres + valCuoReajuste) Then
                    valCuoReajuste = totCuotas - valCuoInteres
                    valCuoReajuste = Math.Abs(valCuoReajuste)
                End If
            End If
        End If
    End Function

    Function verificaTasaInteres()
        'Dim totCuotas As Decimal
        'Dim cuoInteres As Decimal
        'Dim cuoIntCal As Decimal
        ''---cotizacion
        'Try

        '    If Not blTasaIntAjustada Then Exit Function


        '    If gtipoProceso = "AC" Or rTrn.codDestinoTransaccionCal <> "CTA" Or _
        '        rTrn.tasaInteres = 0 Or rTrn.tasaInteres Is Nothing Or _
        '        (rTrn.valMlInteres + rTrn.valMlAdicionalInteres + rTrn.valMlPrimaSisInteres) = 0 Then Exit Function

        '    'cotizacion
        '    If rTrn.valMlInteres > 0 Then
        '        totCuotas = Mat.Redondear((rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste) / rTrn.valMlValorCuota, 2)
        '        cuoInteres = Mat.Redondear(rTrn.valMlInteres / rTrn.valMlValorCuota, 2)
        '        cuoIntCal = Mat.Redondear(totCuotas * rTrn.tasaInteres, 2)

        '        If (cuoIntCal - 0.05) >= cuoInteres Then
        '            rTrn.tasaInteres = Mat.Redondear(rTrn.tasaInteres / 100, 5)  'ajustamos la tasa de interes
        '            blTasaIntAjustada = True
        '            Exit Function
        '        End If
        '    End If
        '    'adicional
        '    If rTrn.valMlAdicionalInteres > 0 Then
        '        totCuotas = Mat.Redondear((rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste) / rTrn.valMlValorCuota, 2)
        '        cuoInteres = Mat.Redondear(rTrn.valMlAdicionalInteres / rTrn.valMlValorCuota, 2)
        '        cuoIntCal = Mat.Redondear(totCuotas * rTrn.tasaInteres, 2)

        '        If (cuoIntCal - 0.05) >= cuoInteres Then
        '            rTrn.tasaInteres = Mat.Redondear(rTrn.tasaInteres / 100, 5)  'ajustamos la tasa de interes
        '            blTasaIntAjustada = True
        '            Exit Function
        '        End If
        '    End If

        '    'sis
        '    If rTrn.valMlPrimaSisInteres > 0 Then
        '        totCuotas = Mat.Redondear((rTrn.valMlPrimaSis + rTrn.valMlPrimaSisInteres + rTrn.valMlPrimaSisReajuste) / rTrn.valMlValorCuota, 2)
        '        cuoInteres = Mat.Redondear(rTrn.valMlPrimaSisInteres / rTrn.valMlValorCuota, 2)
        '        cuoIntCal = Mat.Redondear(totCuotas * rTrn.tasaInteres, 2)

        '        If (cuoIntCal - 0.05) >= cuoInteres Then
        '            rTrn.tasaInteres = Mat.Redondear(rTrn.tasaInteres / 100, 5)  'ajustamos la tasa de interes
        '            blTasaIntAjustada = True
        '            Exit Function
        '        End If
        '    End If
        'Catch : End Try
    End Function


    Private Sub verifica_interes_reajuste(ByRef totCuoInteresReajuste As Decimal, ByRef valMlReajuste As Decimal, ByRef valCuoInt As Decimal, ByRef valCuoRea As Decimal)
        'LFC: AÑADE TASA INTERES
        'If gcodAdministradora = 1034 Then
        If valCuoRea < 0 And valMlReajuste = 0 And (valCuoInt + valCuoRea) >= 0 Then
            valCuoInt = valCuoInt + valCuoRea
            valCuoRea = 0
        End If
        'End If
    End Sub

    '//SIS-- duplica procedimiento
    Private Sub CalcularNominales2(ByVal valMlMvto As Decimal, ByVal valMlInteres As Decimal, ByVal valMlReajuste As Decimal, ByVal valMlAdicional As Decimal, ByVal valMlAdicionalInteres As Decimal, ByVal valMlAdicionalReajuste As Decimal, ByVal valMlExceso As Decimal, ByVal valMlPrima As Decimal, ByVal valMlPrimaInteres As Decimal, ByVal valMlPrimaReajuste As Decimal, ByVal valMlExcesoEmp As Decimal)
        Dim valDif As Decimal
        'Dim valCuoDif As Decimal
        'Dim valMlDif As Decimal
        Dim valMlCotIntRea As Decimal
        Dim valCuoCotIntRea As Decimal
        Dim valMlAdiIntRea As Decimal
        Dim valCuoAdiIntRea As Decimal
        Dim valMlExc As Decimal
        Dim valCuoExc As Decimal
        Dim valMlCot As Decimal
        Dim valCuoCot As Decimal
        Dim valMlInt As Decimal
        Dim valCuoInt As Decimal
        Dim valMlRea As Decimal
        Dim valCuoRea As Decimal
        Dim valMlAdi As Decimal
        Dim valCuoAdi As Decimal
        Dim valMlAdiInt As Decimal
        Dim valCuoAdiInt As Decimal
        Dim valMlAdiRea As Decimal
        Dim valCuoAdiRea As Decimal
        Dim valCuoTotNominal As Decimal
        Dim valMlTotNominal As Decimal

        'OS-5598016 Exceso Empleador
        Dim valMlExcEmpl As Decimal
        Dim valCuoExcEmpl As Decimal

        'SIS//
        Dim valMlPrimaIntRea As Decimal
        Dim valCuoPrimaIntRea As Decimal
        Dim valMlPrim As Decimal
        Dim valCuoPrim As Decimal
        Dim valMlPrimInt As Decimal
        Dim valCuoPrimInt As Decimal
        Dim valMlPrimRea As Decimal
        Dim valCuoPrimRea As Decimal

        Dim valmlRentabilidadRez As Decimal = 0
        Dim valCuoRentabilidadRez As Decimal = 0
        Dim valMlNominalRentaRez As Decimal = 0


        ''LFC: 13/09/2010 - OS_3175470 // 
        'If rTrn.codOrigenProceso = "TRAIPAGN" Or rTrn.codOrigenProceso = "TRAIPAGC" Or rTrn.codOrigenProceso = "TRAINREZ" Then
        '    valMlTotNominal = rTrn.valMlMontoNominal
        'Else
            valMlTotNominal = rTrn.valMlPatrFrecCal
        'End If

        If (Me.blRentabilidadRez And rTrn.codDestinoTransaccionCal = "CTA") And (rTrn.codOrigenProceso = "TRAIPAGN" Or rTrn.codOrigenProceso = "TRAINRZC" Or rTrn.codOrigenProceso = "TRAIPAGC" Or rTrn.codOrigenProceso = "TRAINREZ") Then
            'aplicar rentabilidad
        Else
            blRentabilidadRez = False
        End If



        If rTrn.codOrigenTransaccion = "REZ" Then
            valCuoTotNominal = rTrn.valCuoPatrFrecCal
        Else
            valCuoTotNominal = Mat.Redondear(valMlTotNominal / rTrn.valMlValorCuota, 2)
        End If

        'conceptos sumados fondo, adicional y exceso
        valMlCotIntRea = valMlMvto + valMlInteres + valMlReajuste
        valCuoCotIntRea = Mat.Redondear(valMlCotIntRea / rTrn.valMlValorCuota, 2)

        valMlAdiIntRea = valMlAdicional + valMlAdicionalInteres + valMlAdicionalReajuste
        valCuoAdiIntRea = Mat.Redondear(valMlAdiIntRea / rTrn.valMlValorCuota, 2)

        valMlExc = valMlExceso
        valCuoExc = Mat.Redondear(valMlExc / rTrn.valMlValorCuota, 2)

        'OS-5598016 Exceso Empleador
        valMlExcEmpl = valMlExcesoEmp
        valCuoExcEmpl = Mat.Redondear(valMlExcEmpl / rTrn.valMlValorCuota, 2)

        'SIS//             ' montos parametros de sis informado
        valMlPrimaIntRea = valMlPrima + valMlPrimaInteres + valMlPrimaReajuste
        valCuoPrimaIntRea = Mat.Redondear(valMlPrimaIntRea / rTrn.valMlValorCuota, 2)


        'If blRentabilidadRez Then
        '    'valoriza al fondo de destino
        '    valorizarRentabilidadRez(valmlRentabilidadRez, valCuoRentabilidadRez)
        'End If


        If gcodAdministradora = 1032 And blValorizaCuotaFdoDest Then

            'solo PLV***************************************************************************************************************************
            'conceptos individuales fondo, adicional,interes, reajuste, exceso, etc
            valMlCot = valMlMvto
            valCuoCot = Mat.Redondear(valMlCot / rTrn.valMlValorCuota, 2)
            valMlInt = valMlInteres
            valCuoInt = Mat.Redondear(valMlInt / rTrn.valMlValorCuota, 2)
            valMlRea = valMlReajuste
            valCuoRea = Mat.Redondear(valMlRea / rTrn.valMlValorCuota, 2)
            'LFC: verifica si el reajuste està con monto negativo por el calculo con la tasa de interes
            verifica_interes_reajuste(valCuoCotIntRea, valMlRea, valCuoInt, valCuoRea)

            valMlAdi = valMlAdicional
            valCuoAdi = Mat.Redondear(valMlAdi / rTrn.valMlValorCuota, 2)
            valMlAdiInt = valMlAdicionalInteres
            valCuoAdiInt = Mat.Redondear(valMlAdiInt / rTrn.valMlValorCuota, 2)
            valMlAdiRea = valMlAdicionalReajuste
            valCuoAdiRea = Mat.Redondear(valMlAdiRea / rTrn.valMlValorCuota, 2)
            'LFC: verifica si el reajuste està con monto negativo por el calculo con la tasa de interes
            verifica_interes_reajuste(valCuoAdiIntRea, valMlAdiRea, valCuoAdiInt, valCuoAdiRea)

            'primaSIS
            valMlPrim = valMlPrima
            valCuoPrim = Mat.Redondear(valMlPrim / rTrn.valMlValorCuota, 2)
            valMlPrimInt = valMlPrimaInteres
            valCuoPrimInt = Mat.Redondear(valMlPrimInt / rTrn.valMlValorCuota, 2)
            valMlPrimRea = valMlPrimaReajuste
            valCuoPrimRea = Mat.Redondear(valMlPrimRea / rTrn.valMlValorCuota, 2)
            'LFC: verifica si el reajuste està con monto negativo por el calculo con la tasa de interes
            verifica_interes_reajuste(valCuoPrimaIntRea, valMlPrimRea, valCuoPrimInt, valCuoPrimRea)


            'LFC: 13/09/2010 - OS_3175470 
            If rTrn.codOrigenProceso = "TRAIPAGN" Or rTrn.codOrigenProceso = "TRAIPAGC" Or rTrn.codOrigenProceso = "TRAINREZ" Then
                If rTrn.tipoFondoDestinoCal = "C" Then
                    rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
                Else
                    'rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - rTrn.valMlMontoNominal
                    rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
                End If
            Else
                rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - rTrn.valMlPatrFrecCal
            End If


            rTrn.valCuoCompensCal = Mat.Redondear(rTrn.valMlCompensCal / rTrn.valMlValorCuota, 2)

            'If blRentabilidadRez Then
            '    rTrn.valMlCompensCal += valmlRentabilidadRez
            '    rTrn.valCuoCompensCal += valCuoRentabilidadRez
            'End If


            valDif = rTrn.valCuoPatrFdesCal - (valCuoCot + valCuoInt + valCuoRea + valCuoAdi + valCuoAdiInt + valCuoAdiRea + valCuoPrim + valCuoPrimInt + valCuoPrimRea + valCuoExcEmpl + valCuoExc + rTrn.valCuoCompensCal)

            rTrn.valCuoAjusteDecimalCal = valDif
            rTrn.valMlAjusteDecimalCal = 0

        Else

            If Me.blRentabilidadRez Then
                valmlRentabilidadRez = rTrn.valMlPatrFrecCal - rTrn.valMlMontoNominal

                'se debe restar simpre el monto de la rentabilidad del otro fondo a la obl
                ' en la rentabilidad si se debe sumar
                If valMlMvto - valmlRentabilidadRez >= 0 Then
                    valMlMvto = valMlMvto - valmlRentabilidadRez
                Else
                    valmlRentabilidadRez = 0
                End If
            End If


            'conceptos individuales fondo, adicional,interes, reajuste, exceso, etc
            valMlCot = valMlMvto
            valCuoCot = Mat.Redondear(valMlCot / rTrn.valMlValorCuota, 2)

            valMlInt = valMlInteres
            valCuoInt = Mat.Redondear(valCuoCotIntRea * rTrn.tasaInteres, 2)


            valMlRea = valMlReajuste
            valCuoRea = valCuoCotIntRea - (valCuoCot + valCuoInt)

            'LFC: verifica si el reajuste està con monto negativo por el calculo con la tasa de interes
            verifica_interes_reajuste(valCuoCotIntRea, valMlRea, valCuoInt, valCuoRea)

            valMlAdi = valMlAdicional
            valCuoAdi = Mat.Redondear(valMlAdi / rTrn.valMlValorCuota, 2)

            valMlAdiInt = valMlAdicionalInteres
            valCuoAdiInt = Mat.Redondear(valCuoAdiIntRea * rTrn.tasaInteres, 2)

            valMlAdiRea = valMlAdicionalReajuste
            valCuoAdiRea = valCuoAdiIntRea - (valCuoAdi + valCuoAdiInt)

            'LFC: verifica si el reajuste està con monto negativo por el calculo con la tasa de interes
            verifica_interes_reajuste(valCuoAdiIntRea, valMlAdiRea, valCuoAdiInt, valCuoAdiRea)


            'LFC: 13/09/2010 - OS_3175470 
            If rTrn.codOrigenProceso = "TRAIPAGN" Or rTrn.codOrigenProceso = "TRAIPAGC" Or rTrn.codOrigenProceso = "TRAINREZ" Then
                If rTrn.tipoFondoDestinoCal = "C" Then
                    If Me.blRentabilidadRez Then
                        rTrn.valMlCompensCal = valmlRentabilidadRez
                    Else
                        rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
                    End If
                Else
                    If Me.blRentabilidadRez Then
                        rTrn.valMlCompensCal = (rTrn.valMlPatrFrecActCal - valMlTotNominal) + valmlRentabilidadRez
                    Else
                        'rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - rTrn.valMlMontoNominal
                        rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
                    End If

                End If
            Else
                rTrn.valMlCompensCal = (rTrn.valMlPatrFrecActCal - rTrn.valMlPatrFrecCal) + valmlRentabilidadRez
            End If

            'primaSIS
            valMlPrim = valMlPrima
            valCuoPrim = Mat.Redondear(valMlPrim / rTrn.valMlValorCuota, 2)
            valMlPrimInt = valMlPrimaInteres

            valCuoPrimInt = Mat.Redondear(valMlPrimaIntRea * rTrn.tasaInteres, 2)


            valMlPrimRea = valMlPrimaReajuste
            valCuoPrimRea = valCuoPrimaIntRea - (valCuoPrim + valCuoPrimInt)

            'LFC: verifica si el reajuste està con monto negativo por el calculo con la tasa de interes
            verifica_interes_reajuste(valCuoPrimaIntRea, valMlPrimRea, valCuoPrimInt, valCuoPrimRea)


            'If blRentabilidadRez Then
            '    rTrn.valMlCompensCal += valmlRentabilidadRez
            '    rTrn.valCuoCompensCal += valCuoRentabilidadRez
            'End If

            If rTrn.valMlCompensCal > 0 Then
                rTrn.valCuoCompensCal = Mat.Redondear((valMlCotIntRea + rTrn.valMlCompensCal) / rTrn.valMlValorCuota, 2) - valCuoCotIntRea
            Else
                rTrn.valCuoCompensCal = Mat.Redondear(rTrn.valMlCompensCal / rTrn.valMlValorCuota, 2)
                'Segun BA BA-2007050232 del 24/05/2007
                'No se aplicarán perdidas si la cotizacion es insuficiente para generar abonos
                If rTrn.valCuoPatrFdesCal = 0 Then
                    rTrn.valCuoCompensCal = 0
                End If

                '-------------->>>>>>>>>>>>>>>>-----------------------
                'lfc//20-11-2009 ca-2009070092-planillas complementarias generan monto negativo
                ' cuando se paga adicional (comision) y la rentabilidad es negativa
                'rentabilidad negativa
                If rTrn.valCuoCompensCal < 0 Then  ' solo rentabilidad negativa        
					If rTrn.sexo = "Fx" And (valCuoPrim + valCuoPrimInt + valCuoPrimRea) > 0 Then
					Else
						If valCuoCotIntRea = 0 And valCuoAdiIntRea > 0 And valCuoExc = 0 And (rTrn.valCuoCompensCal * -1) > rSal.valCuoSaldo Then
							If rSal.valCuoSaldo > 0 Then							'--OS:6491232  --27/08/2014
								rTrn.valCuoCompensCal = rSal.valCuoSaldo * -1
								rTrn.valMlCompensCal = Mat.Redondear(rTrn.valCuoCompensCal * rTrn.valMlValorCuota, 0)
							Else
								rTrn.valCuoCompensCal = 0
								rTrn.valMlCompensCal = 0
							End If
						End If
					End If
				End If
            End If

            valMlExc += rTrn.valMlExcesoTopeCal
            valCuoExc += rTrn.valCuoExcesoTopeCal
            '--------------------------------------------------------------------------------------------------------+  sis

            'lfc:27/07/2010-------ajuste decimal: os_2626661
            '------------------>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

            'OS-5598016 Se agrega Exceso Empleador
            valDif = rTrn.valCuoPatrFdesCal - (valCuoCotIntRea + valCuoAdiIntRea + valCuoExc + rTrn.valCuoCompensCal + valCuoPrimaIntRea + valCuoExcEmpl)
            'valDif = rTrn.valCuoPatrFdesCal - (valCuoCotIntRea + valCuoAdiIntRea + valCuoExc + rTrn.valCuoCompensCal + valCuoPrimaIntRea)

            'valCuoDif = rTrn.valCuoPatrFdesCal - (valCuoCotIntRea + valCuoAdiIntRea + valCuoExc + rTrn.valCuoCompensCal + valCuoPrimaIntRea)
            'valMlDif = rTrn.valMlPatrFdesCal - (valMlCotIntRea + valMlAdiIntRea + valMlExc + rTrn.valMlCompensCal + valMlPrimaIntRea)

            rTrn.valCuoAjusteDecimalCal = valDif
            rTrn.valMlAjusteDecimalCal = 0
            '-----------------<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            'ajustesDecimalAComisionPorc()

        End If

        rTrn.valMlMvtoCal = valMlCot
        rTrn.valCuoMvtoCal = valCuoCot

        rTrn.valMlInteresCal = valMlInt
        rTrn.valCuoInteresCal = valCuoInt

        rTrn.valMlReajusteCal = valMlRea
        rTrn.valCuoReajusteCal = valCuoRea

        rTrn.valMlAdicionalCal = valMlAdi
        rTrn.valCuoAdicionalCal = valCuoAdi

        'Se agrega AJUSTE DECIMAL a ADICIONAL, Solo en CUOTAS.15/10/2010
        ''Se vuelve atras con el AJUSTE DECIMAL. 09/11/2010 PCI
        'ajustesDecimalAComisionPorc()
        'rTrn.valCuoAdicionalCal = valCuoAdi - g_valCuoAjusteDec

        'PCI
        'rTrn.valCuoAjusteDecimalCal = 0

        rTrn.valMlAdicionalInteresCal = valMlAdiInt
        rTrn.valCuoAdicionalInteresCal = valCuoAdiInt

        rTrn.valMlAdicionalReajusteCal = valMlAdiRea
        rTrn.valCuoAdicionalReajusteCal = valCuoAdiRea

        rTrn.valMlExcesoTopeCal = valMlExc
        rTrn.valCuoExcesoTopeCal = valCuoExc

        'OS-5598016 Se agrega Exceso Empleador
        rTrn.valMlExcesoEmplCal = valMlExcEmpl
        rTrn.valCuoExcesoEmplCal = valCuoExcEmpl

        'primaSIS

        rTrn.valMlPrimaSisCal = valMlPrim
        rTrn.valCuoPrimaSisCal = valCuoPrim
        rTrn.valMlPrimaSisInteresCal = valMlPrimInt
        rTrn.valCuoPrimaSisInteresCal = valCuoPrimInt
        rTrn.valMlPrimaSisReajusteCal = valMlPrimRea
        rTrn.valCuoPrimaSisReajusteCal = valCuoPrimRea

        If rTrn.valMlReajusteCal = 0 And rTrn.valCuoReajusteCal <> 0 Then
            If rTrn.valCuoReajusteCal > 0 Then
                rTrn.valCuoInteresCal += rTrn.valCuoReajusteCal
                rTrn.valCuoReajusteCal = 0
            Else
                If rTrn.valCuoInteresCal + rTrn.valCuoReajusteCal > 0 Then
                    rTrn.valCuoInteresCal += rTrn.valCuoReajusteCal
                    rTrn.valCuoReajusteCal = 0
                End If
            End If
        End If

        If rTrn.valMlAdicionalReajusteCal = 0 And rTrn.valCuoAdicionalReajusteCal <> 0 Then
            If rTrn.valCuoAdicionalReajusteCal > 0 Then
                rTrn.valCuoAdicionalInteresCal += rTrn.valCuoAdicionalReajusteCal
                rTrn.valCuoAdicionalReajusteCal = 0
            Else
                If rTrn.valCuoAdicionalInteresCal + rTrn.valCuoAdicionalReajusteCal > 0 Then
                    rTrn.valCuoAdicionalInteresCal += rTrn.valCuoAdicionalReajusteCal
                    rTrn.valCuoAdicionalReajusteCal = 0
                End If
            End If
        End If

        If rTrn.valMlPrimaSisReajusteCal = 0 And rTrn.valCuoPrimaSisReajusteCal <> 0 Then
            If rTrn.valCuoPrimaSisReajusteCal > 0 Then
                rTrn.valCuoPrimaSisInteresCal += rTrn.valCuoPrimaSisReajusteCal
                rTrn.valCuoPrimaSisReajusteCal = 0
            Else
                If rTrn.valCuoPrimaSisInteresCal + rTrn.valCuoPrimaSisReajusteCal > 0 Then
                    rTrn.valCuoPrimaSisInteresCal += rTrn.valCuoPrimaSisReajusteCal
                    rTrn.valCuoPrimaSisReajusteCal = 0
                End If
            End If
        End If

    End Sub

    Private Sub CalcularNominales5(ByVal valMlMvto As Decimal, _
                                  ByVal valMlInteres As Decimal, _
                                  ByVal valMlReajuste As Decimal, _
                                  ByVal valMlAdicional As Decimal, _
                                  ByVal valMlAdicionalInteres As Decimal, _
                                  ByVal valMlAdicionalReajuste As Decimal, _
                                  ByVal valMlExceso As Decimal)

        Dim valDif As Decimal
        Dim valDifUni As Decimal
        'Dim valMLDif As Decimal
        'Dim valCuoDif As Decimal

        Dim valMlCotIntRea As Decimal
        Dim valCuoCotIntRea As Decimal

        Dim valMlAdiIntRea As Decimal
        Dim valCuoAdiIntRea As Decimal

        Dim valMlExc As Decimal
        Dim valCuoExc As Decimal

        Dim valMlCot As Decimal
        Dim valCuoCot As Decimal

        Dim valMlInt As Decimal
        Dim valCuoInt As Decimal

        Dim valMlRea As Decimal
        Dim valCuoRea As Decimal

        Dim valMlAdi As Decimal
        Dim valCuoAdi As Decimal

        Dim valMlAdiInt As Decimal
        Dim valCuoAdiInt As Decimal

        Dim valMlAdiRea As Decimal
        Dim valCuoAdiRea As Decimal

        Dim valCuoTotNominal As Decimal
        Dim valMlTotNominal As Decimal

        valMlTotNominal = rTrn.valMlPatrFrecCal '- rTrn.valMlTransferenciaCal

        If rTrn.codOrigenTransaccion = "REZ" Then
            valCuoTotNominal = rTrn.valCuoPatrFrecCal
        Else
            'valCuoTotNominal = Mat.Redondear(valMlTotNominal / rTrn.valMlValorCuota, 2)
            valCuoTotNominal = (valMlTotNominal / rTrn.valMlValorCuota)
        End If

        'conceptos sumados fondo, adicional y exceso

        valMlCotIntRea = valMlMvto + valMlInteres + valMlReajuste
        'valCuoCotIntRea = Mat.Redondear(valMlCotIntRea / rTrn.valMlValorCuota, 2)
        valCuoCotIntRea = (valMlCotIntRea / rTrn.valMlValorCuota)

        valMlAdiIntRea = valMlAdicional + valMlAdicionalInteres + valMlAdicionalReajuste
        'valCuoAdiIntRea = Mat.Redondear(valMlAdiIntRea / rTrn.valMlValorCuota, 2)
        valCuoAdiIntRea = (valMlAdiIntRea / rTrn.valMlValorCuota)

        valMlExc = valMlExceso
        'valCuoExc = Mat.Redondear(valMlExc / rTrn.valMlValorCuota, 2)
        valCuoExc = (valMlExc / rTrn.valMlValorCuota)

        'conceptos individuales fondo, adicional,interes, reajuste, exceso, etc

        valMlCot = valMlMvto
        'valCuoCot = Mat.Redondear(valMlCot / rTrn.valMlValorCuota, 2)
        valCuoCot = (valMlCot / rTrn.valMlValorCuota)

        valMlInt = valMlInteres
        'valCuoInt = Mat.Redondear(valCuoCotIntRea * rTrn.tasaInteres, 2)
        'valCuoInt = Mat.Redondear(valCuoCotIntRea * (rTrn.tasaInteres / 100), 2)
        valCuoInt = (valCuoCotIntRea * (rTrn.tasaInteres / 100))

        valMlRea = valMlReajuste
        valCuoRea = valCuoCotIntRea - (valCuoCot + valCuoInt)

        valMlAdi = valMlAdicional
        'valCuoAdi = Mat.Redondear(valMlAdi / rTrn.valMlValorCuota, 2)
        valCuoAdi = (valMlAdi / rTrn.valMlValorCuota)

        valMlAdiInt = valMlAdicionalInteres
        'valCuoAdiInt = Mat.Redondear(valCuoAdiIntRea * rTrn.tasaInteres, 2)
        'valCuoAdiInt = Mat.Redondear(valCuoAdiIntRea * (rTrn.tasaInteres / 100), 2)
        valCuoAdiInt = (valCuoAdiIntRea * (rTrn.tasaInteres / 100))

        valMlAdiRea = valMlAdicionalReajuste
        valCuoAdiRea = valCuoAdiIntRea - (valCuoAdi + valCuoAdiInt)




        rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - rTrn.valMlPatrFrecCal

        If rTrn.valMlCompensCal > 0 Then
            'rTrn.valCuoCompensCal = Mat.Redondear((valMlCotIntRea + rTrn.valMlCompensCal) / rTrn.valMlValorCuota, 2) - valCuoCotIntRea
            rTrn.valCuoCompensCal = ((valMlCotIntRea + rTrn.valMlCompensCal) / rTrn.valMlValorCuota) - valCuoCotIntRea
        Else
            'rTrn.valCuoCompensCal = Mat.Redondear(rTrn.valMlCompensCal / rTrn.valMlValorCuota, 2)
            rTrn.valCuoCompensCal = (rTrn.valMlCompensCal / rTrn.valMlValorCuota)
            'Segun BA BA-2007050232 del 24/05/2007
            'No se aplicarán perdidas si la cotizacion es insuficiente para generar abonos
            If rTrn.valCuoPatrFdesCal = 0 Then
                rTrn.valCuoCompensCal = 0
            End If

            'lfc//20-11-2009 ca-2009070092-planillas complementarias generan monto negativo
            ' cuando se paga adicional (comision) y la rentabilidad es negativa
            'rentabilidad negativa
            If rTrn.valCuoCompensCal < 0 Then  ' solo rentabilidad negativa        

                If valCuoCotIntRea = 0 And valCuoAdiIntRea > 0 And valCuoExc = 0 _
                   And (rTrn.valCuoCompensCal * -1) > rSal.valCuoSaldo And rSal.valCuoSaldo >= 0 Then
                    rTrn.valCuoCompensCal = rSal.valCuoSaldo * -1
                    rTrn.valMlCompensCal = Mat.Redondear(rTrn.valCuoCompensCal * rTrn.valMlValorCuota, 0)
                End If
            End If

        End If

        valMlExc += rTrn.valMlExcesoTopeCal
        valCuoExc += rTrn.valCuoExcesoTopeCal





        '--------------------------------------------------------------------------------------------------------+  sis
        'Verifica Diferencias entre Valores Individuales y Acumulados ya que tambien genera Ajuste Decimal
        valDifUni = Mat.Redondear(valCuoCotIntRea, 2) - (Mat.Redondear(valCuoCot, 2) + Mat.Redondear(valCuoInt, 2) + Mat.Redondear(valCuoRea, 2))
        If valDifUni > 0 Then
            If valMlInt > 0 Then
                valCuoInt += valDifUni
            ElseIf valMlRea > 0 Then
                valCuoRea += valDifUni
            End If
        End If

        valDifUni = Mat.Redondear(valCuoAdiIntRea, 2) - (Mat.Redondear(valCuoAdi, 2) + Mat.Redondear(valCuoAdiInt, 2) + Mat.Redondear(valCuoAdiRea, 2))
        If valDifUni > 0 Then
            If valMlAdiInt > 0 Then
                valCuoAdiInt += valDifUni
            ElseIf valMlAdiRea > 0 Then
                valCuoAdiRea += valDifUni
            End If
        End If

        'valDif = rTrn.valCuoPatrFdesCal - (valCuoCotIntRea + valCuoAdiIntRea + valCuoExc + rTrn.valCuoCompensCal)
        valDif = rTrn.valCuoPatrFdesCal - (Mat.Redondear(valCuoCot, 2) + Mat.Redondear(valCuoInt, 2) + Mat.Redondear(valCuoRea, 2) + Mat.Redondear(valCuoAdi, 2) + Mat.Redondear(valCuoAdiInt, 2) + Mat.Redondear(valCuoAdiRea, 2) + Mat.Redondear(valCuoExc, 2) + Mat.Redondear(rTrn.valCuoCompensCal, 2))

        rTrn.valMlAjusteDecimalCal = 0
        '-----------------<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        VerificaAjustesDecimal(valDif, valCuoCot, valCuoAdi, valCuoAdiInt, valCuoAdiRea, 0, rTrn.valCuoCompensCal)


        'If rTrn.codCausalAjuste = 4 And valDif > 0 Then 'Transaccion Aperturada
        '    valCuoCotIntRea += valDif
        '    valMlCot += valDif
        '    valDif = 0
        'End If

        'rTrn.valCuoAjusteDecimalCal = valDif
        rTrn.valMlAjusteDecimalCal = 0

        rTrn.valMlMvtoCal = valMlCot
        rTrn.valCuoMvtoCal = Mat.Redondear(valCuoCot, 2)

        rTrn.valMlInteresCal = valMlInt
        rTrn.valCuoInteresCal = Mat.Redondear(valCuoInt, 2)

        rTrn.valMlReajusteCal = valMlRea
        rTrn.valCuoReajusteCal = Mat.Redondear(valCuoRea, 2)

        rTrn.valMlAdicionalCal = valMlAdi
        rTrn.valCuoAdicionalCal = Mat.Redondear(valCuoAdi, 2)

        'Se agrega AJUSTE DECIMAL a ADICIONAL, Solo en CUOTAS.15/10/2010
        'ajustesDecimalAComisionPorc()
        'rTrn.valCuoAdicionalCal = valCuoAdi - g_valCuoAjusteDec
        'PCI
        'rTrn.valCuoAjusteDecimalCal = 0

        rTrn.valMlAdicionalInteresCal = valMlAdiInt
        rTrn.valCuoAdicionalInteresCal = Mat.Redondear(valCuoAdiInt, 2)

        rTrn.valMlAdicionalReajusteCal = valMlAdiRea
        rTrn.valCuoAdicionalReajusteCal = Mat.Redondear(valCuoAdiRea, 2)

        rTrn.valMlExcesoTopeCal = valMlExc
        rTrn.valCuoExcesoTopeCal = Mat.Redondear(valCuoExc, 2)

        'rTrn.valMlCompensCal = Mat.Redondear(rTrn.valMlCompensCal, 0)
        rTrn.valCuoCompensCal = Mat.Redondear(rTrn.valCuoCompensCal, 2)

    End Sub

    '//SIS-- duplica procedimiento
    Private Sub CalcularNominales4(ByVal valMlMvto As Decimal, ByVal valMlInteres As Decimal, ByVal valMlReajuste As Decimal, ByVal valMlAdicional As Decimal, ByVal valMlAdicionalInteres As Decimal, ByVal valMlAdicionalReajuste As Decimal, ByVal valMlExceso As Decimal, ByVal valMlPrima As Decimal, ByVal valMlPrimaInteres As Decimal, ByVal valMlPrimaReajuste As Decimal, ByVal valMlExcesoEmp As Decimal, ByRef valDif As Decimal)
        'Dim valDif As Decimal
        Dim valDifUni As Decimal
        'Dim valCuoDif As Decimal
        'Dim valMlDif As Decimal
        Dim valMlCotIntRea As Decimal
        Dim valCuoCotIntRea As Decimal
        Dim valMlAdiIntRea As Decimal
        Dim valCuoAdiIntRea As Decimal
        Dim valMlExc As Decimal
        Dim valCuoExc As Decimal
        Dim valMlCot As Decimal
        Dim valCuoCot As Decimal
        Dim valMlInt As Decimal
        Dim valCuoInt As Decimal
        Dim valMlRea As Decimal
        Dim valCuoRea As Decimal
        Dim valMlAdi As Decimal
        Dim valCuoAdi As Decimal
        Dim valMlAdiInt As Decimal
        Dim valCuoAdiInt As Decimal
        Dim valMlAdiRea As Decimal
        Dim valCuoAdiRea As Decimal
        Dim valCuoTotNominal As Decimal
        Dim valMlTotNominal As Decimal

        'OS-5598016 Exceso Empleador
        Dim valMlExcEmpl As Decimal
        Dim valCuoExcEmpl As Decimal

        'SIS//
        Dim valMlPrimaIntRea As Decimal
        Dim valCuoPrimaIntRea As Decimal
        Dim valMlPrim As Decimal
        Dim valCuoPrim As Decimal
        Dim valMlPrimInt As Decimal
        Dim valCuoPrimInt As Decimal
        Dim valMlPrimRea As Decimal
        Dim valCuoPrimRea As Decimal

        Dim valmlRentabilidadRez As Decimal = 0
        Dim valCuoRentabilidadRez As Decimal = 0


        If rTrn.codOrigenMvto = "TRAIPAGN" Or rTrn.codOrigenMvto = "TRAIPAGC" Or rTrn.codOrigenMvto = "TRAINREZ" Then
            If rTrn.tipoFondoDestinoCal = "C" Then
                valMlTotNominal = rTrn.valMlPatrFrecCal
            Else
                valMlTotNominal = rTrn.valMlPatrFrecCal
            End If
        Else
            valMlTotNominal = rTrn.valMlPatrFrecCal
        End If


        If rTrn.codOrigenTransaccion = "REZ" Then
            If rTrn.codOrigenMvto = "TRAIPAGN" Or rTrn.codOrigenMvto = "TRAIPAGC" Or rTrn.codOrigenMvto = "TRAINREZ" Then
                If rTrn.tipoFondoDestinoCal = "C" Then
                    valCuoTotNominal = rTrn.valCuoPatrFrecCal
                Else
                    valCuoTotNominal = (valMlTotNominal / rTrn.valMlValorCuota)
                End If
            Else
                valCuoTotNominal = rTrn.valCuoPatrFrecCal
            End If
        Else
            'PCI No se utiliza Redondeo hasta el final.
            'valCuoTotNominal = Mat.Redondear(valMlTotNominal / rTrn.valMlValorCuota, 2)
            valCuoTotNominal = (valMlTotNominal / rTrn.valMlValorCuota)
        End If

        'conceptos sumados fondo, adicional y exceso
        valMlCotIntRea = valMlMvto + valMlInteres + valMlReajuste
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoCotIntRea = Mat.Redondear(valMlCotIntRea / rTrn.valMlValorCuota, 2)
        valCuoCotIntRea = (valMlCotIntRea / rTrn.valMlValorCuota)

        valMlAdiIntRea = valMlAdicional + valMlAdicionalInteres + valMlAdicionalReajuste
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoAdiIntRea = Mat.Redondear(valMlAdiIntRea / rTrn.valMlValorCuota, 2)
        valCuoAdiIntRea = (valMlAdiIntRea / rTrn.valMlValorCuota)

        valMlExc = valMlExceso
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoExc = Mat.Redondear(valMlExc / rTrn.valMlValorCuota, 2)
        valCuoExc = (valMlExc / rTrn.valMlValorCuota)

        'OS-5598016 Exceso Empleador
        valMlExcEmpl = valMlExcesoEmp
        valCuoExcEmpl = (valMlExcEmpl / rTrn.valMlValorCuota)

        'SIS//             ' montos parametros de sis informado
        valMlPrimaIntRea = valMlPrima + valMlPrimaInteres + valMlPrimaReajuste
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoPrimaIntRea = Mat.Redondear(valMlPrimaIntRea / rTrn.valMlValorCuota, 2)
        valCuoPrimaIntRea = (valMlPrimaIntRea / rTrn.valMlValorCuota)

        'If blRentabilidadRez Then            'valoriza al fondo de destino
        '    valorizarRentabilidadRez(valmlRentabilidadRez, valCuoRentabilidadRez)
        'End If

        If gcodAdministradora = 1032 And blValorizaCuotaFdoDest Then

            'solo PLV***************************************************************************************************************************
            'conceptos individuales fondo, adicional,interes, reajuste, exceso, etc
            valMlCot = valMlMvto
            valCuoCot = Mat.Redondear(valMlCot / rTrn.valMlValorCuota, 2)
            valMlInt = valMlInteres
            valCuoInt = Mat.Redondear(valMlInt / rTrn.valMlValorCuota, 2)
            valMlRea = valMlReajuste
            valCuoRea = Mat.Redondear(valMlRea / rTrn.valMlValorCuota, 2)
            'LFC: verifica si el reajuste està con monto negativo por el calculo con la tasa de interes
            verifica_interes_reajuste(valCuoCotIntRea, valMlRea, valCuoInt, valCuoRea)

            valMlAdi = valMlAdicional
            valCuoAdi = Mat.Redondear(valMlAdi / rTrn.valMlValorCuota, 2)
            valMlAdiInt = valMlAdicionalInteres
            valCuoAdiInt = Mat.Redondear(valMlAdiInt / rTrn.valMlValorCuota, 2)
            valMlAdiRea = valMlAdicionalReajuste
            valCuoAdiRea = Mat.Redondear(valMlAdiRea / rTrn.valMlValorCuota, 2)
            'LFC: verifica si el reajuste està con monto negativo por el calculo con la tasa de interes
            verifica_interes_reajuste(valCuoAdiIntRea, valMlAdiRea, valCuoAdiInt, valCuoAdiRea)

            'primaSIS
            valMlPrim = valMlPrima
            valCuoPrim = Mat.Redondear(valMlPrim / rTrn.valMlValorCuota, 2)
            valMlPrimInt = valMlPrimaInteres
            valCuoPrimInt = Mat.Redondear(valMlPrimInt / rTrn.valMlValorCuota, 2)
            valMlPrimRea = valMlPrimaReajuste
            valCuoPrimRea = Mat.Redondear(valMlPrimRea / rTrn.valMlValorCuota, 2)




            'LFC: verifica si el reajuste està con monto negativo por el calculo con la tasa de interes
            verifica_interes_reajuste(valCuoPrimaIntRea, valMlPrimRea, valCuoPrimInt, valCuoPrimRea)


            'LFC: 13/09/2010 - OS_3175470 
            If rTrn.codOrigenProceso = "TRAIPAGN" Or rTrn.codOrigenProceso = "TRAIPAGC" Or rTrn.codOrigenProceso = "TRAINREZ" Then
                If rTrn.tipoFondoDestinoCal = "C" Then
                    rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
                Else
                    'rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - rTrn.valMlMontoNominal
                    rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
                End If
            Else
                rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - rTrn.valMlPatrFrecCal
            End If


            rTrn.valCuoCompensCal = Mat.Redondear(rTrn.valMlCompensCal / rTrn.valMlValorCuota, 2)

            'If blRentabilidadRez Then
            '    rTrn.valMlCompensCal += valmlRentabilidadRez
            '    rTrn.valCuoCompensCal += valCuoRentabilidadRez
            'End If

            valDif = rTrn.valCuoPatrFdesCal - (valCuoCot + valCuoInt + valCuoRea _
                        + valCuoAdi + valCuoAdiInt + valCuoAdiRea _
                        + valCuoPrim + valCuoPrimInt + valCuoPrimRea _
                        + valCuoExcEmpl + valCuoExc _
                        + rTrn.valCuoCompensCal)

            rTrn.valCuoAjusteDecimalCal = Mat.Redondear(valDif, 2)
            rTrn.valMlAjusteDecimalCal = 0



            rTrn.valMlMvtoCal = valMlCot
            rTrn.valCuoMvtoCal = Mat.Redondear(valCuoCot, 2)

            rTrn.valMlInteresCal = valMlInt
            rTrn.valCuoInteresCal = Mat.Redondear(valCuoInt, 2)

            rTrn.valMlReajusteCal = valMlRea
            rTrn.valCuoReajusteCal = Mat.Redondear(valCuoRea, 2)

            rTrn.valMlAdicionalCal = valMlAdi
            rTrn.valCuoAdicionalCal = Mat.Redondear(valCuoAdi, 2)

            'Se agrega AJUSTE DECIMAL a ADICIONAL, Solo en CUOTAS.15/10/2010
            ''Se vuelve atras con el AJUSTE DECIMAL. 09/11/2010 PCI
            'ajustesDecimalAComisionPorc()
            'rTrn.valCuoAdicionalCal = valCuoAdi - g_valCuoAjusteDec

            'PCI
            'rTrn.valCuoAjusteDecimalCal = 0

            rTrn.valMlAdicionalInteresCal = valMlAdiInt
            rTrn.valCuoAdicionalInteresCal = Mat.Redondear(valCuoAdiInt, 2)

            rTrn.valMlAdicionalReajusteCal = valMlAdiRea
            rTrn.valCuoAdicionalReajusteCal = Mat.Redondear(valCuoAdiRea, 2)

            rTrn.valMlExcesoTopeCal = valMlExc
            rTrn.valCuoExcesoTopeCal = Mat.Redondear(valCuoExc, 2)

            'OS-5598016 Se agrega Exceso Empleador
            rTrn.valMlExcesoEmplCal = valMlExcEmpl
            rTrn.valCuoExcesoEmplCal = Mat.Redondear(valCuoExcEmpl, 2)

            'primaSIS

            rTrn.valMlPrimaSisCal = valMlPrim
            rTrn.valCuoPrimaSisCal = Mat.Redondear(valCuoPrim, 2)
            rTrn.valMlPrimaSisInteresCal = valMlPrimInt
            rTrn.valCuoPrimaSisInteresCal = Mat.Redondear(valCuoPrimInt, 2)
            rTrn.valMlPrimaSisReajusteCal = valMlPrimRea
            rTrn.valCuoPrimaSisReajusteCal = Mat.Redondear(valCuoPrimRea, 2)


        Else


            'conceptos individuales fondo, adicional,interes, reajuste, exceso, etc
            valMlCot = valMlMvto
            'PCI No se utiliza Redondeo hasta el final.
            'valCuoCot = Mat.Redondear(valMlCot / rTrn.valMlValorCuota, 2)
            valCuoCot = (valMlCot / rTrn.valMlValorCuota)


            'Nuevo

            valMlInt = valMlInteres
            'PCI No se utiliza Redondeo hasta el final.
            'valCuoInt = Mat.Redondear(valCuoCotIntRea * rTrn.tasaInteres, 2)
            'valCuoInt = (valCuoCotIntRea * rTrn.tasaInteres)
            valCuoInt = (valCuoCotIntRea * (rTrn.tasaInteres / 100))

            valMlRea = valMlReajuste
            valCuoRea = valCuoCotIntRea - (valCuoCot + valCuoInt)

            valMlAdi = valMlAdicional
            'PCI No se utiliza Redondeo hasta el final.
            'valCuoAdi = Mat.Redondear(valMlAdi / rTrn.valMlValorCuota, 2)
            valCuoAdi = (valMlAdi / rTrn.valMlValorCuota)

            valMlAdiInt = valMlAdicionalInteres
            'PCI No se utiliza Redondeo hasta el final.
            'valCuoAdiInt = Mat.Redondear(valCuoAdiIntRea * rTrn.tasaInteres, 2)
            'valCuoAdiInt = (valCuoAdiIntRea * rTrn.tasaInteres)
            valCuoAdiInt = (valCuoAdiIntRea * (rTrn.tasaInteres / 100))


            valMlAdiRea = valMlAdicionalReajuste
            valCuoAdiRea = valCuoAdiIntRea - (valCuoAdi + valCuoAdiInt)


            'LFC: 13/09/2010 - OS_3175470 
            If rTrn.codOrigenProceso = "TRAIPAGN" Or rTrn.codOrigenProceso = "TRAIPAGC" Or rTrn.codOrigenProceso = "TRAINREZ" Then
                If rTrn.tipoFondoDestinoCal = "C" Then
                    rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
                Else
                    'rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - rTrn.valMlMontoNominal
                    rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
                End If
            Else
                If rTrn.codOrigenMvto = "TRAIPAGN" Or rTrn.codOrigenMvto = "TRAIPAGC" Or rTrn.codOrigenMvto = "TRAINREZ" Then
                    rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
                Else
                    rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - rTrn.valMlPatrFrecCal
                End If
            End If

            'primaSIS
            valMlPrim = valMlPrima
            'PCI No se utiliza Redondeo hasta el final.
            'valCuoPrim = Mat.Redondear(valMlPrim / rTrn.valMlValorCuota, 2)
            valCuoPrim = (valMlPrim / rTrn.valMlValorCuota)

            valMlPrimInt = valMlPrimaInteres
            'PCI No se utiliza Redondeo hasta el final.
            'valCuoPrimInt = Mat.Redondear(valMlPrimaIntRea * rTrn.tasaInteres, 2)
            'valCuoPrimInt = (valMlPrimaIntRea * rTrn.tasaInteres)
            valCuoPrimInt = (valCuoPrimaIntRea * (rTrn.tasaInteres / 100))

            valMlPrimRea = valMlPrimaReajuste
            valCuoPrimRea = valCuoPrimaIntRea - (valCuoPrim + valCuoPrimInt)

            'If blRentabilidadRez Then
            '    rTrn.valMlCompensCal += valmlRentabilidadRez
            '    rTrn.valCuoCompensCal += valCuoRentabilidadRez
            'End If

            If rTrn.valMlCompensCal > 0 Then
                'PCI No se utiliza Redondeo hasta el final.
                'rTrn.valCuoCompensCal = Mat.Redondear((valMlCotIntRea + rTrn.valMlCompensCal) / rTrn.valMlValorCuota, 2) - valCuoCotIntRea
                rTrn.valCuoCompensCal = ((valMlCotIntRea + rTrn.valMlCompensCal) / rTrn.valMlValorCuota) - valCuoCotIntRea

                If Mat.Redondear(rTrn.valCuoCompensCal, 2) = 0 Then
                    rTrn.valCuoCompensCal = 0
                End If
            Else
                'PCI No se utiliza Redondeo hasta el final.
                'rTrn.valCuoCompensCal = Mat.Redondear(rTrn.valMlCompensCal / rTrn.valMlValorCuota, 2)
                rTrn.valCuoCompensCal = (rTrn.valMlCompensCal / rTrn.valMlValorCuota)
                'Segun BA BA-2007050232 del 24/05/2007
                'No se aplicarán perdidas si la cotizacion es insuficiente para generar abonos
                If rTrn.valCuoPatrFdesCal = 0 Then
                    rTrn.valCuoCompensCal = 0
                End If

                '-------------->>>>>>>>>>>>>>>>-----------------------
                'lfc//20-11-2009 ca-2009070092-planillas complementarias generan monto negativo
                ' cuando se paga adicional (comision) y la rentabilidad es negativa
                'rentabilidad negativa
                If rTrn.valCuoCompensCal < 0 Then  ' solo rentabilidad negativa        

					If rTrn.sexo = "Fx" And (valCuoPrim + valCuoPrimInt + valCuoPrimRea) > 0 Then
					Else
						If valCuoCotIntRea = 0 And valCuoAdiIntRea > 0 And valCuoExc = 0 And (rTrn.valCuoCompensCal * -1) > rSal.valCuoSaldo And rSal.valCuoSaldo > 0 Then






							rTrn.valCuoCompensCal = rSal.valCuoSaldo * -1

							'PCI No se utiliza Redondeo hasta el final.
							'rTrn.valMlCompensCal = Mat.Redondear(rTrn.valCuoCompensCal * rTrn.valMlValorCuota, 0)
							rTrn.valMlCompensCal = (rTrn.valCuoCompensCal * rTrn.valMlValorCuota)
						End If
					End If

				End If			   '------<<<<<<<<<<<<<<<<<<<<<<<<<<<---------------------
            End If

            valMlExc += rTrn.valMlExcesoTopeCal
            valCuoExc += rTrn.valCuoExcesoTopeCal

            '--------------------------------------------------------------------------------------------------------+  sis
            'Verifica Diferencias entre Valores Individuales y Acumulados ya que tambien genera Ajuste Decimal
            valDifUni = Mat.Redondear(valCuoCotIntRea, 2) - (Mat.Redondear(valCuoCot, 2) + Mat.Redondear(valCuoInt, 2) + Mat.Redondear(valCuoRea, 2))
            If valDifUni > 0 Then
                If valMlInt > 0 Then
                    valCuoInt += valDifUni
                ElseIf valMlRea > 0 Then
                    valCuoRea += valDifUni
                End If
            End If

            valDifUni = Mat.Redondear(valCuoAdiIntRea, 2) - (Mat.Redondear(valCuoAdi, 2) + Mat.Redondear(valCuoAdiInt, 2) + Mat.Redondear(valCuoAdiRea, 2))
            If valDifUni > 0 Then
                If valMlAdiInt > 0 Then
                    valCuoAdiInt += valDifUni
                ElseIf valMlAdiRea > 0 Then
                    valCuoAdiRea += valDifUni
                End If
            End If

            valDifUni = Mat.Redondear(valCuoPrimaIntRea, 2) - (Mat.Redondear(valCuoPrim, 2) + Mat.Redondear(valCuoPrimInt, 2) + Mat.Redondear(valCuoPrimRea, 2))
            If valDifUni > 0 Then
                If valMlPrimInt > 0 Then
                    valCuoPrimInt += valDifUni
                ElseIf valMlPrimRea > 0 Then
                    valCuoPrimRea += valDifUni
                End If
            End If

            'lfc:27/07/2010-------ajuste decimal: os_2626661
            '------------------>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            'PCI No se utiliza Redondeo hasta el final.
            'valDif = rTrn.valCuoPatrFdesCal - (valCuoCotIntRea + valCuoAdiIntRea + valCuoExc + rTrn.valCuoCompensCal + valCuoPrimaIntRea)
            'valDif = rTrn.valCuoPatrFdesCal - Mat.Redondear((valCuoCotIntRea + valCuoAdiIntRea + valCuoExc + rTrn.valCuoCompensCal + valCuoPrimaIntRea), 2)

            'valDif = rTrn.valCuoPatrFdesCal - (Mat.Redondear(valCuoCotIntRea, 2) + Mat.Redondear(valCuoAdiIntRea, 2) + Mat.Redondear(valCuoExc, 2) + Mat.Redondear(rTrn.valCuoCompensCal, 2) + Mat.Redondear(valCuoPrimaIntRea, 2))
            valDif = rTrn.valCuoPatrFdesCal - (Mat.Redondear(valCuoCot, 2) + Mat.Redondear(valCuoInt, 2) + Mat.Redondear(valCuoRea, 2) + Mat.Redondear(valCuoAdi, 2) + Mat.Redondear(valCuoAdiInt, 2) + Mat.Redondear(valCuoAdiRea, 2) + Mat.Redondear(valCuoExc, 2) + Mat.Redondear(rTrn.valCuoCompensCal, 2) + Mat.Redondear(valCuoPrim, 2) + Mat.Redondear(valCuoPrimInt, 2) + Mat.Redondear(valCuoPrimRea, 2) + Mat.Redondear(valCuoExcEmpl, 2))

            'valCuoDif = rTrn.valCuoPatrFdesCal - (valCuoCotIntRea + valCuoAdiIntRea + valCuoExc + rTrn.valCuoCompensCal + valCuoPrimaIntRea)
            'valMlDif = rTrn.valMlPatrFdesCal - (valMlCotIntRea + valMlAdiIntRea + valMlExc + rTrn.valMlCompensCal + valMlPrimaIntRea)


            rTrn.valMlAjusteDecimalCal = 0
            'modificacion por OS:8984519 - CON ajuste decimal en recuperaciones de rezagos
            If (rTrn.codOrigenProceso = "REREZMAS" Or rTrn.codOrigenProceso = "REREZSEL") And gcodAdministradora = 1032 Then
                rTrn.valCuoAjusteDecimalCal = valDif
                valDif = 0
            Else
                'rTrn.valCuoAjusteDecimalCal = valDif
            End If




            '-----------------<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            'PCI 10/06/2014
            'VerificaAjustesDecimal(valDif, valCuoCot, valCuoAdi, valCuoPrim, rTrn.valCuoCompensCal)

            rTrn.valMlMvtoCal = valMlCot
            'PCI No se utiliza Redondeo hasta el final.
            'rTrn.valCuoMvtoCal = valCuoCot
            rTrn.valCuoMvtoCal = Mat.Redondear(valCuoCot, 2)

            rTrn.valMlInteresCal = valMlInt
            'PCI No se utiliza Redondeo hasta el final.
            'rTrn.valCuoInteresCal = valCuoInt
            rTrn.valCuoInteresCal = Mat.Redondear(valCuoInt, 2)

            rTrn.valMlReajusteCal = valMlRea
            'PCI No se utiliza Redondeo hasta el final.
            'rTrn.valCuoReajusteCal = valCuoRea
            rTrn.valCuoReajusteCal = Mat.Redondear(valCuoRea, 2)


            rTrn.valMlAdicionalCal = valMlAdi
            'PCI No se utiliza Redondeo hasta el final.
            'rTrn.valCuoAdicionalCal = valCuoAdi
            rTrn.valCuoAdicionalCal = Mat.Redondear(valCuoAdi, 2)

            'Se agrega AJUSTE DECIMAL a ADICIONAL, Solo en CUOTAS.15/10/2010
            ''Se vuelve atras con el AJUSTE DECIMAL. 09/11/2010 PCI
            'ajustesDecimalAComisionPorc()
            'rTrn.valCuoAdicionalCal = valCuoAdi - g_valCuoAjusteDec

            'PCI
            'rTrn.valCuoAjusteDecimalCal = 0

            rTrn.valMlAdicionalInteresCal = valMlAdiInt
            'PCI No se utiliza Redondeo hasta el final.
            'rTrn.valCuoAdicionalInteresCal = valCuoAdiInt
            rTrn.valCuoAdicionalInteresCal = Mat.Redondear(valCuoAdiInt, 2)

            rTrn.valMlAdicionalReajusteCal = valMlAdiRea
            'PCI No se utiliza Redondeo hasta el final.
            'rTrn.valCuoAdicionalReajusteCal = valCuoAdiRea
            rTrn.valCuoAdicionalReajusteCal = Mat.Redondear(valCuoAdiRea, 2)

            rTrn.valMlExcesoTopeCal = valMlExc
            'PCI No se utiliza Redondeo hasta el final.
            'rTrn.valCuoExcesoTopeCal = valCuoExc
            rTrn.valCuoExcesoTopeCal = Mat.Redondear(valCuoExc, 2)

            'OS-5598016 Se agrega Exceso Empleador
            rTrn.valMlExcesoEmplCal = valMlExcEmpl
            rTrn.valCuoExcesoEmplCal = Mat.Redondear(valCuoExcEmpl, 2)

            'primaSIS

            rTrn.valCuoCompensCal = Mat.Redondear(rTrn.valCuoCompensCal, 2)

            rTrn.valMlPrimaSisCal = valMlPrim
            'PCI No se utiliza Redondeo hasta el final.
            'rTrn.valCuoPrimaSisCal = valCuoPrim
            rTrn.valCuoPrimaSisCal = Mat.Redondear(valCuoPrim, 2)
            rTrn.valMlPrimaSisInteresCal = valMlPrimInt
            'PCI No se utiliza Redondeo hasta el final.
            'rTrn.valCuoPrimaSisInteresCal = valCuoPrimInt
            rTrn.valCuoPrimaSisInteresCal = Mat.Redondear(valCuoPrimInt, 2)
            rTrn.valMlPrimaSisReajusteCal = valMlPrimRea
            'PCI No se utiliza Redondeo hasta el final.
            'rTrn.valCuoPrimaSisReajusteCal = valCuoPrimRea
            rTrn.valCuoPrimaSisReajusteCal = Mat.Redondear(valCuoPrimRea, 2)


        End If

    End Sub

    Private Sub CalcularNominales3(ByVal valMlMvto As Decimal, ByVal valMlInteres As Decimal, ByVal valMlReajuste As Decimal, ByVal valMlAdicional As Decimal, ByVal valMlAdicionalInteres As Decimal, ByVal valMlAdicionalReajuste As Decimal, ByVal valMlExceso As Decimal, ByVal valMlPrima As Decimal, ByVal valMlPrimaInteres As Decimal, ByVal valMlPrimaReajuste As Decimal)
        Dim valDif As Decimal
        Dim valDifUni As Decimal
        'Dim valCuoDif As Decimal
        'Dim valMlDif As Decimal
        Dim valMlCotIntRea As Decimal
        Dim valCuoCotIntRea As Decimal
        Dim valMlAdiIntRea As Decimal
        Dim valCuoAdiIntRea As Decimal
        Dim valMlExc As Decimal
        Dim valCuoExc As Decimal
        Dim valMlCot As Decimal
        Dim valCuoCot As Decimal
        Dim valMlInt As Decimal
        Dim valCuoInt As Decimal
        Dim valMlRea As Decimal
        Dim valCuoRea As Decimal
        Dim valMlAdi As Decimal
        Dim valCuoAdi As Decimal
        Dim valMlAdiInt As Decimal
        Dim valCuoAdiInt As Decimal
        Dim valMlAdiRea As Decimal
        Dim valCuoAdiRea As Decimal
        Dim valCuoTotNominal As Decimal
        Dim valMlTotNominal As Decimal

        'SIS//
        Dim valMlPrimaIntRea As Decimal
        Dim valCuoPrimaIntRea As Decimal
        Dim valMlPrim As Decimal
        Dim valCuoPrim As Decimal
        Dim valMlPrimInt As Decimal
        Dim valCuoPrimInt As Decimal
        Dim valMlPrimRea As Decimal
        Dim valCuoPrimRea As Decimal


        'LFC: 13/09/2010 - OS_3175470 // 
        'If gcodAdministradora = 1033 Then
        'valMlTotNominal = rTrn.valMlMontoNominal
        'Else
        valMlTotNominal = rTrn.valMlPatrFrecCal '- rTrn.valMlTransferenciaCal
        'End If
        ''If rTrn.codOrigenProceso = "TRAIPAGN" Then
        ''    valMlTotNominal = rTrn.valMlMontoNominal
        ''Else
        'valMlTotNominal = rTrn.valMlPatrFrecCal '- rTrn.valMlTransferenciaCal
        ''End If


        If rTrn.codOrigenTransaccion = "REZ" Then
            valCuoTotNominal = rTrn.valCuoPatrFrecCal
        Else
            'PCI No se utiliza Redondeo hasta el final.
            'valCuoTotNominal = Mat.Redondear(valMlTotNominal / rTrn.valMlValorCuota, 2)
            valCuoTotNominal = (valMlTotNominal / rTrn.valMlValorCuota)
        End If

        'conceptos sumados fondo, adicional y exceso
        valMlCotIntRea = valMlMvto + valMlInteres + valMlReajuste
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoCotIntRea = Mat.Redondear(valMlCotIntRea / rTrn.valMlValorCuota, 2)
        valCuoCotIntRea = (valMlCotIntRea / rTrn.valMlValorCuota)

        valMlAdiIntRea = valMlAdicional + valMlAdicionalInteres + valMlAdicionalReajuste
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoAdiIntRea = Mat.Redondear(valMlAdiIntRea / rTrn.valMlValorCuota, 2)
        valCuoAdiIntRea = (valMlAdiIntRea / rTrn.valMlValorCuota)

        valMlExc = valMlExceso
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoExc = Mat.Redondear(valMlExc / rTrn.valMlValorCuota, 2)
        valCuoExc = (valMlExc / rTrn.valMlValorCuota)

        'SIS//             ' montos parametros de sis informado
        valMlPrimaIntRea = valMlPrima + valMlPrimaInteres + valMlPrimaReajuste
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoPrimaIntRea = Mat.Redondear(valMlPrimaIntRea / rTrn.valMlValorCuota, 2)
        valCuoPrimaIntRea = (valMlPrimaIntRea / rTrn.valMlValorCuota)

        'conceptos individuales fondo, adicional,interes, reajuste, exceso, etc
        valMlCot = valMlMvto
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoCot = Mat.Redondear(valMlCot / rTrn.valMlValorCuota, 2)
        valCuoCot = (valMlCot / rTrn.valMlValorCuota)

        valMlInt = valMlInteres
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoInt = Mat.Redondear(valCuoCotIntRea * rTrn.tasaInteres, 2)
        valCuoInt = (valCuoCotIntRea * rTrn.tasaInteres)

        valMlRea = valMlReajuste
        valCuoRea = valCuoCotIntRea - (valCuoCot + valCuoInt)

        valMlAdi = valMlAdicional
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoAdi = Mat.Redondear(valMlAdi / rTrn.valMlValorCuota, 2)
        valCuoAdi = (valMlAdi / rTrn.valMlValorCuota)

        valMlAdiInt = valMlAdicionalInteres
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoAdiInt = Mat.Redondear(valCuoAdiIntRea * rTrn.tasaInteres, 2)
        valCuoAdiInt = (valCuoAdiIntRea * rTrn.tasaInteres)

        valMlAdiRea = valMlAdicionalReajuste
        valCuoAdiRea = valCuoAdiIntRea - (valCuoAdi + valCuoAdiInt)


        'LFC: 13/09/2010 - OS_3175470 
        If rTrn.codOrigenProceso = "TRAIPAGN" Then
            If rTrn.tipoFondoDestinoCal = "C" Then
                rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
            Else
                'rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - rTrn.valMlMontoNominal
                rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
            End If
        Else
            rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - rTrn.valMlPatrFrecCal
        End If

        'primaSIS
        valMlPrim = valMlPrima
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoPrim = Mat.Redondear(valMlPrim / rTrn.valMlValorCuota, 2)
        valCuoPrim = (valMlPrim / rTrn.valMlValorCuota)

        valMlPrimInt = valMlPrimaInteres
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoPrimInt = Mat.Redondear(valMlPrimaIntRea * rTrn.tasaInteres, 2)
        valCuoPrimInt = (valMlPrimaIntRea * rTrn.tasaInteres)

        valMlPrimRea = valMlPrimaReajuste
        valCuoPrimRea = valCuoPrimaIntRea - (valCuoPrim + valCuoPrimInt)

        If rTrn.valMlCompensCal > 0 Then
            'PCI No se utiliza Redondeo hasta el final.
            'rTrn.valCuoCompensCal = Mat.Redondear((valMlCotIntRea + rTrn.valMlCompensCal) / rTrn.valMlValorCuota, 2) - valCuoCotIntRea
            rTrn.valCuoCompensCal = ((valMlCotIntRea + rTrn.valMlCompensCal) / rTrn.valMlValorCuota) - valCuoCotIntRea
        Else
            'PCI No se utiliza Redondeo hasta el final.
            'rTrn.valCuoCompensCal = Mat.Redondear(rTrn.valMlCompensCal / rTrn.valMlValorCuota, 2)
            rTrn.valCuoCompensCal = (rTrn.valMlCompensCal / rTrn.valMlValorCuota)
            'Segun BA BA-2007050232 del 24/05/2007
            'No se aplicarán perdidas si la cotizacion es insuficiente para generar abonos
            If rTrn.valCuoPatrFdesCal = 0 Then
                rTrn.valCuoCompensCal = 0
            End If

            '-------------->>>>>>>>>>>>>>>>-----------------------
            'lfc//20-11-2009 ca-2009070092-planillas complementarias generan monto negativo
            ' cuando se paga adicional (comision) y la rentabilidad es negativa
            'rentabilidad negativa
            If rTrn.valCuoCompensCal < 0 Then  ' solo rentabilidad negativa        

				If rTrn.sexo = "Fx" And (valCuoPrim + valCuoPrimInt + valCuoPrimRea) > 0 Then
				Else
					If valCuoCotIntRea = 0 And valCuoAdiIntRea > 0 And valCuoExc = 0 And (rTrn.valCuoCompensCal * -1) > rSal.valCuoSaldo And rSal.valCuoSaldo > 0 Then
						rTrn.valCuoCompensCal = rSal.valCuoSaldo * -1

						'PCI No se utiliza Redondeo hasta el final.
						'rTrn.valMlCompensCal = Mat.Redondear(rTrn.valCuoCompensCal * rTrn.valMlValorCuota, 0)
						rTrn.valMlCompensCal = (rTrn.valCuoCompensCal * rTrn.valMlValorCuota)
					End If
				End If
			End If			'------<<<<<<<<<<<<<<<<<<<<<<<<<<<---------------------
        End If

        valMlExc += rTrn.valMlExcesoTopeCal
        valCuoExc += rTrn.valCuoExcesoTopeCal

        '--------------------------------------------------------------------------------------------------------+  sis
        'Verifica Diferencias entre Valores Individuales y Acumulados ya que tambien genera Ajuste Decimal
        valDifUni = Mat.Redondear(valCuoCotIntRea, 2) - (Mat.Redondear(valCuoCot, 2) + Mat.Redondear(valCuoInt, 2) + Mat.Redondear(valCuoRea, 2))
        If valDifUni > 0 Then
            If valMlInt > 0 Then
                valCuoInt += valDifUni
            ElseIf valMlRea > 0 Then
                valCuoRea += valDifUni
            End If
        End If

        valDifUni = Mat.Redondear(valCuoAdiIntRea, 2) - (Mat.Redondear(valCuoAdi, 2) + Mat.Redondear(valCuoAdiInt, 2) + Mat.Redondear(valCuoAdiRea, 2))
        If valDifUni > 0 Then
            If valMlAdiInt > 0 Then
                valCuoAdiInt += valDifUni
            ElseIf valMlAdiRea > 0 Then
                valCuoAdiRea += valDifUni
            End If
        End If

        valDifUni = Mat.Redondear(valCuoPrimaIntRea, 2) - (Mat.Redondear(valCuoPrim, 2) + Mat.Redondear(valCuoPrimInt, 2) + Mat.Redondear(valCuoPrimRea, 2))
        If valDifUni > 0 Then
            If valMlPrimInt > 0 Then
                valCuoPrimInt += valDifUni
            ElseIf valMlPrimRea > 0 Then
                valCuoPrimRea += valDifUni
            End If
        End If

        'lfc:27/07/2010-------ajuste decimal: os_2626661
        '------------------>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        'PCI No se utiliza Redondeo hasta el final.
        'valDif = rTrn.valCuoPatrFdesCal - (valCuoCotIntRea + valCuoAdiIntRea + valCuoExc + rTrn.valCuoCompensCal + valCuoPrimaIntRea)
        'valDif = rTrn.valCuoPatrFdesCal - Mat.Redondear((valCuoCotIntRea + valCuoAdiIntRea + valCuoExc + rTrn.valCuoCompensCal + valCuoPrimaIntRea), 2)

        'valDif = rTrn.valCuoPatrFdesCal - (Mat.Redondear(valCuoCotIntRea, 2) + Mat.Redondear(valCuoAdiIntRea, 2) + Mat.Redondear(valCuoExc, 2) + Mat.Redondear(rTrn.valCuoCompensCal, 2) + Mat.Redondear(valCuoPrimaIntRea, 2))
        valDif = rTrn.valCuoPatrFdesCal - (Mat.Redondear(valCuoCot, 2) + Mat.Redondear(valCuoInt, 2) + Mat.Redondear(valCuoRea, 2) + Mat.Redondear(valCuoAdi, 2) + Mat.Redondear(valCuoAdiInt, 2) + Mat.Redondear(valCuoAdiRea, 2) + Mat.Redondear(valCuoExc, 2) + Mat.Redondear(rTrn.valCuoCompensCal, 2) + Mat.Redondear(valCuoPrim, 2) + Mat.Redondear(valCuoPrimInt, 2) + Mat.Redondear(valCuoPrimRea, 2))

        'valCuoDif = rTrn.valCuoPatrFdesCal - (valCuoCotIntRea + valCuoAdiIntRea + valCuoExc + rTrn.valCuoCompensCal + valCuoPrimaIntRea)
        'valMlDif = rTrn.valMlPatrFdesCal - (valMlCotIntRea + valMlAdiIntRea + valMlExc + rTrn.valMlCompensCal + valMlPrimaIntRea)

        'rTrn.valCuoAjusteDecimalCal = valDif
        rTrn.valMlAjusteDecimalCal = 0
        '-----------------<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        VerificaAjustesDecimal(valDif, valCuoCot, valCuoAdi, valCuoAdiInt, valCuoAdiRea, valCuoPrim, rTrn.valCuoCompensCal)

        rTrn.valMlMvtoCal = valMlCot
        'PCI No se utiliza Redondeo hasta el final.
        'rTrn.valCuoMvtoCal = valCuoCot
        rTrn.valCuoMvtoCal = Mat.Redondear(valCuoCot, 2)

        rTrn.valMlInteresCal = valMlInt
        'PCI No se utiliza Redondeo hasta el final.
        'rTrn.valCuoInteresCal = valCuoInt
        rTrn.valCuoInteresCal = Mat.Redondear(valCuoInt, 2)

        rTrn.valMlReajusteCal = valMlRea
        'PCI No se utiliza Redondeo hasta el final.
        'rTrn.valCuoReajusteCal = valCuoRea
        rTrn.valCuoReajusteCal = Mat.Redondear(valCuoRea, 2)


        rTrn.valMlAdicionalCal = valMlAdi
        'PCI No se utiliza Redondeo hasta el final.
        'rTrn.valCuoAdicionalCal = valCuoAdi
        rTrn.valCuoAdicionalCal = Mat.Redondear(valCuoAdi, 2)

        'Se agrega AJUSTE DECIMAL a ADICIONAL, Solo en CUOTAS.15/10/2010
        ''Se vuelve atras con el AJUSTE DECIMAL. 09/11/2010 PCI
        'ajustesDecimalAComisionPorc()
        'rTrn.valCuoAdicionalCal = valCuoAdi - g_valCuoAjusteDec

        'PCI
        'rTrn.valCuoAjusteDecimalCal = 0

        rTrn.valMlAdicionalInteresCal = valMlAdiInt
        'PCI No se utiliza Redondeo hasta el final.
        'rTrn.valCuoAdicionalInteresCal = valCuoAdiInt
        rTrn.valCuoAdicionalInteresCal = Mat.Redondear(valCuoAdiInt, 2)

        rTrn.valMlAdicionalReajusteCal = valMlAdiRea
        'PCI No se utiliza Redondeo hasta el final.
        'rTrn.valCuoAdicionalReajusteCal = valCuoAdiRea
        rTrn.valCuoAdicionalReajusteCal = Mat.Redondear(valCuoAdiRea, 2)

        rTrn.valMlExcesoTopeCal = valMlExc
        'PCI No se utiliza Redondeo hasta el final.
        'rTrn.valCuoExcesoTopeCal = valCuoExc
        rTrn.valCuoExcesoTopeCal = Mat.Redondear(valCuoExc, 2)

        'primaSIS

        rTrn.valCuoCompensCal = Mat.Redondear(rTrn.valCuoCompensCal, 2)

        rTrn.valMlPrimaSisCal = valMlPrim
        'PCI No se utiliza Redondeo hasta el final.
        'rTrn.valCuoPrimaSisCal = valCuoPrim
        rTrn.valCuoPrimaSisCal = Mat.Redondear(valCuoPrim, 2)
        rTrn.valMlPrimaSisInteresCal = valMlPrimInt
        'PCI No se utiliza Redondeo hasta el final.
        'rTrn.valCuoPrimaSisInteresCal = valCuoPrimInt
        rTrn.valCuoPrimaSisInteresCal = Mat.Redondear(valCuoPrimInt, 2)
        rTrn.valMlPrimaSisReajusteCal = valMlPrimRea
        'PCI No se utiliza Redondeo hasta el final.
        'rTrn.valCuoPrimaSisReajusteCal = valCuoPrimRea
        rTrn.valCuoPrimaSisReajusteCal = Mat.Redondear(valCuoPrimRea, 2)
    End Sub

    Private Sub CalcularNominales6(ByVal valMlMvto As Decimal, ByVal valMlInteres As Decimal, ByVal valMlReajuste As Decimal, ByVal valMlAdicional As Decimal, ByVal valMlAdicionalInteres As Decimal, ByVal valMlAdicionalReajuste As Decimal, ByVal valMlExceso As Decimal, ByVal valMlPrima As Decimal, ByVal valMlPrimaInteres As Decimal, ByVal valMlPrimaReajuste As Decimal)
        Dim valDif As Decimal
        'Dim valCuoDif As Decimal
        'Dim valMlDif As Decimal
        Dim valMlCotIntRea As Decimal
        Dim valCuoCotIntRea As Decimal
        Dim valMlAdiIntRea As Decimal
        Dim valCuoAdiIntRea As Decimal
        Dim valMlExc As Decimal
        Dim valCuoExc As Decimal
        Dim valMlCot As Decimal
        Dim valCuoCot As Decimal
        Dim valMlInt As Decimal
        Dim valCuoInt As Decimal
        Dim valMlRea As Decimal
        Dim valCuoRea As Decimal
        Dim valMlAdi As Decimal
        Dim valCuoAdi As Decimal
        Dim valMlAdiInt As Decimal
        Dim valCuoAdiInt As Decimal
        Dim valMlAdiRea As Decimal
        Dim valCuoAdiRea As Decimal
        Dim valCuoTotNominal As Decimal
        Dim valMlTotNominal As Decimal

        'SIS//
        Dim valMlPrimaIntRea As Decimal
        Dim valCuoPrimaIntRea As Decimal
        Dim valMlPrim As Decimal
        Dim valCuoPrim As Decimal
        Dim valMlPrimInt As Decimal
        Dim valCuoPrimInt As Decimal
        Dim valMlPrimRea As Decimal
        Dim valCuoPrimRea As Decimal

        Dim gfecha19 As Date
        Dim valCuo19 As Decimal
        Dim valCuoFinales As Decimal
        Dim valCuoComisiones As Decimal
        Dim valMF As Decimal
        Dim valMC As Decimal
        Dim valR1 As Decimal
        Dim valR2 As Decimal

        Dim DifExcesoRentab As Decimal

        ''LFC: 13/09/2010 - OS_3175470 // 
        'If rTrn.codOrigenProceso = "TRAIPAGN" Or rTrn.codOrigenProceso = "TRAIPAGC" Or rTrn.codOrigenProceso = "TRAINREZ" Then
        '    valMlTotNominal = rTrn.valMlMontoNominal
        'Else
        '    valMlTotNominal = rTrn.valMlPatrFrecCal
        'End If

        'SNDLFUENTES---
        'valMlTotNominal = rTrn.valMlPatrFrecCal
        valMlTotNominal = rTrn.valMlMontoNominal

        If rTrn.codOrigenTransaccion = "REZ" Then
            valCuoTotNominal = rTrn.valCuoPatrFrecCal
        Else
            valCuoTotNominal = Mat.Redondear(valMlTotNominal / rTrn.valMlValorCuota, 2)
        End If

        'conceptos sumados fondo, adicional y exceso
        valMlCotIntRea = valMlMvto + valMlInteres + valMlReajuste
        valCuoCotIntRea = Mat.Redondear(valMlCotIntRea / rTrn.valMlValorCuota, 2)

        valMlAdiIntRea = valMlAdicional + valMlAdicionalInteres + valMlAdicionalReajuste
        valCuoAdiIntRea = Mat.Redondear(valMlAdiIntRea / rTrn.valMlValorCuota, 2)

        'Se comentan lineas de Excesos ya que se esta Duplicando. PCI 24/10/2013.
        'valMlExc = valMlExceso
        'valCuoExc = Mat.Redondear(valMlExc / rTrn.valMlValorCuota, 2)

        'SIS//             ' montos parametros de sis informado
        valMlPrimaIntRea = valMlPrima + valMlPrimaInteres + valMlPrimaReajuste
        valCuoPrimaIntRea = Mat.Redondear(valMlPrimaIntRea / rTrn.valMlValorCuota, 2)


        'conceptos individuales fondo, adicional,interes, reajuste, exceso, etc
        valMlCot = valMlMvto
        valCuoCot = Mat.Redondear(valMlCot / rTrn.valMlValorCuota, 2)

        valMlInt = valMlInteres
        valCuoInt = Mat.Redondear(valCuoCotIntRea * rTrn.tasaInteres, 2)

        valMlRea = valMlReajuste
        valCuoRea = valCuoCotIntRea - (valCuoCot + valCuoInt)

        valMlAdi = valMlAdicional
        valCuoAdi = Mat.Redondear(valMlAdi / rTrn.valMlValorCuota, 2)

        valMlAdiInt = valMlAdicionalInteres
        valCuoAdiInt = Mat.Redondear(valCuoAdiIntRea * rTrn.tasaInteres, 2)

        valMlAdiRea = valMlAdicionalReajuste
        valCuoAdiRea = valCuoAdiIntRea - (valCuoAdi + valCuoAdiInt)


        'primaSIS
        valMlPrim = valMlPrima
        valCuoPrim = Mat.Redondear(valMlPrim / rTrn.valMlValorCuota, 2)

        valMlPrimInt = valMlPrimaInteres
        valCuoPrimInt = Mat.Redondear(valMlPrimaIntRea * rTrn.tasaInteres, 2)

        valMlPrimRea = valMlPrimaReajuste
        valCuoPrimRea = valCuoPrimaIntRea - (valCuoPrim + valCuoPrimInt)

        'Se modifica calculo de Transacciones para Acreditacion IPS
        'OS-5296346 10/09/2013
        DifExcesoRentab = valMlTotNominal - (valMlCotIntRea + valMlAdiIntRea + valMlPrimaIntRea)
        If DifExcesoRentab > 0 Then
            'Verifica Rentabilidad
            If ((Mat.Redondear(((valCuoCotIntRea + valCuoAdiIntRea + valCuoPrimaIntRea) * gvalMlCuotaDestinoC), 0)) - _
               (valMlCotIntRea + valMlAdiIntRea + valMlPrimaIntRea)) > 0 Then

                rTrn.valMlCompensCal = ((Mat.Redondear(((valCuoCotIntRea + valCuoAdiIntRea + valCuoPrimaIntRea) * gvalMlCuotaDestinoC), 0)) - (valMlCotIntRea + valMlAdiIntRea + valMlPrimaIntRea))
            Else
                rTrn.valMlCompensCal = 0
            End If

            'Verifica Excesos
            rTrn.valMlExcesoTopeCal = valMlTotNominal - (valMlCotIntRea + valMlAdiIntRea + valMlPrimaIntRea + rTrn.valMlCompensCal)

        Else
            rTrn.valMlCompensCal = 0
            rTrn.valMlExcesoTopeCal = 0
        End If


        ''SNDLFUENTES - //CONTROL RENTA
        ' TraerControlRentas()

        ' CalcularComisionFija()
        ' valCuoComisiones = Mat.Redondear(rTrn.valMlComisFijaCal / valCuo19, 2)
        ' 'valCuoComisiones = Mat.Redondear(rTrn.valMlComisPorcentual / rTrn.valMlValorCuota, 2)

        ' valMF = Mat.Redondear(valCuoFinales * rTrn.valMlValorCuotaVal)

        ' valMC = Mat.Redondear(valCuoComisiones * rTrn.valMlValorCuotaVal)

        ' valR1 = valMF - (valMlCot + valMC)

        ' 'SNDLFUENTES - valor no asignado
        ' 'valR2 = rTrn.valMlMontoNominal - (valMlCot + rTrn.valMlComisPorcentualCal)
        ' valR2 = rTrn.valMlMontoNominal - (valMlCot + rTrn.valMlComisPorcentual)

        ' If (valR2 - valR1) < 0 Then
        '     rTrn.valMlCompensCal = valR2
        ' ElseIf (valR2 - valR1) > 0 Then
        '     rTrn.valMlCompensCal = valR1
        ' End If

        ' If rTrn.valMlCompensCal < 0 Then
        '     'rTrn.valMlCompensCal = (rTrn.valMlCompensCal * -1)
        '     rTrn.valMlCompensCal = 0
        ' End If

        rTrn.valCuoCompensCal = Mat.Redondear(rTrn.valMlCompensCal / rTrn.valMlValorCuota, 2)

        'If (valR2 - valR1) > 0 Then
        '    rTrn.valMlExcesoTopeCal = (valR2 - valR1)
        'ElseIf (valR2 - valR1) <= 0 Then
        '    rTrn.valMlExcesoTopeCal = 0
        'End If

        rTrn.valCuoExcesoTopeCal = Mat.Redondear(rTrn.valMlExcesoTopeCal / rTrn.valMlValorCuota, 2)

        valMlExc += rTrn.valMlExcesoTopeCal
        valCuoExc += rTrn.valCuoExcesoTopeCal

        rTrn.valCuoAjusteDecimalCal = valDif
        rTrn.valMlAjusteDecimalCal = 0
        '-----------------<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        'ajustesDecimalAComisionPorc()

        rTrn.valMlMvtoCal = valMlCot
        rTrn.valCuoMvtoCal = valCuoCot

        rTrn.valMlInteresCal = valMlInt
        rTrn.valCuoInteresCal = valCuoInt

        rTrn.valMlReajusteCal = valMlRea
        rTrn.valCuoReajusteCal = valCuoRea


        rTrn.valMlAdicionalCal = valMlAdi
        rTrn.valCuoAdicionalCal = valCuoAdi

        'Se agrega AJUSTE DECIMAL a ADICIONAL, Solo en CUOTAS.15/10/2010
        ''Se vuelve atras con el AJUSTE DECIMAL. 09/11/2010 PCI
        'ajustesDecimalAComisionPorc()
        'rTrn.valCuoAdicionalCal = valCuoAdi - g_valCuoAjusteDec

        'PCI
        'rTrn.valCuoAjusteDecimalCal = 0


        rTrn.valMlAdicionalInteresCal = valMlAdiInt
        rTrn.valCuoAdicionalInteresCal = valCuoAdiInt

        rTrn.valMlAdicionalReajusteCal = valMlAdiRea
        rTrn.valCuoAdicionalReajusteCal = valCuoAdiRea

        'SNDLFUENTES // se deben realizar los calculos con los valores cuota actuales
        rTrn.valMlComisPorcentualCal = rTrn.valMlComisPorcentual
        rTrn.valCuoComisPorcentualCal = rTrn.valCuoComisPorcentual
        'rTrn.valMlComisPorcentualCal = rTrn.valMlComisPorcentual
        'rTrn.valCuoComisPorcentualCal = valCuoComisiones

        rTrn.valMlExcesoTopeCal = valMlExc
        rTrn.valCuoExcesoTopeCal = valCuoExc

        'primaSIS

        rTrn.valMlPrimaSisCal = valMlPrim
        rTrn.valCuoPrimaSisCal = valCuoPrim
        rTrn.valMlPrimaSisInteresCal = valMlPrimInt
        rTrn.valCuoPrimaSisInteresCal = valCuoPrimInt
        rTrn.valMlPrimaSisReajusteCal = valMlPrimRea
        rTrn.valCuoPrimaSisReajusteCal = valCuoPrimRea
    End Sub

    Private Sub CalcularNominales7(ByVal valMlMvto As Decimal, _
                                   ByVal valMlInteres As Decimal, _
                                   ByVal valMlReajuste As Decimal, _
                                   ByVal valMlAdicional As Decimal, _
                                   ByVal valMlAdicionalInteres As Decimal, _
                                   ByVal valMlAdicionalReajuste As Decimal, _
                                   ByVal valMlExceso As Decimal)

        Dim valDif As Decimal
        'Dim valMLDif As Decimal
        'Dim valCuoDif As Decimal

        Dim valMlCotIntRea As Decimal
        Dim valCuoCotIntRea As Decimal

        Dim valMlAdiIntRea As Decimal
        Dim valCuoAdiIntRea As Decimal

        Dim valMlExc As Decimal
        Dim valCuoExc As Decimal

        Dim valMlCot As Decimal
        Dim valCuoCot As Decimal

        Dim valMlInt As Decimal
        Dim valCuoInt As Decimal

        Dim valMlRea As Decimal
        Dim valCuoRea As Decimal

        Dim valMlAdi As Decimal
        Dim valCuoAdi As Decimal

        Dim valMlAdiInt As Decimal
        Dim valCuoAdiInt As Decimal

        Dim valMlAdiRea As Decimal
        Dim valCuoAdiRea As Decimal

        Dim valCuoTotNominal As Decimal
        Dim valMlTotNominal As Decimal

        valMlTotNominal = rTrn.valMlPatrFrecCal '- rTrn.valMlTransferenciaCal

        If rTrn.codOrigenTransaccion = "REZ" Then
            valCuoTotNominal = rTrn.valCuoPatrFrecCal
        Else
            valCuoTotNominal = Mat.Redondear(valMlTotNominal / rTrn.valMlValorCuota, 2)
        End If

        'conceptos sumados fondo, adicional y exceso

        valMlCotIntRea = valMlMvto + valMlInteres + valMlReajuste
        valCuoCotIntRea = Mat.Redondear(valMlCotIntRea / rTrn.valMlValorCuota, 2)

        valMlAdiIntRea = valMlAdicional + valMlAdicionalInteres + valMlAdicionalReajuste
        valCuoAdiIntRea = Mat.Redondear(valMlAdiIntRea / rTrn.valMlValorCuota, 2)

        valMlExc = valMlExceso
        valCuoExc = Mat.Redondear(valMlExc / rTrn.valMlValorCuota, 2)

        'conceptos individuales fondo, adicional,interes, reajuste, exceso, etc

        valMlCot = valMlMvto
        valCuoCot = Mat.Redondear(valMlCot / rTrn.valMlValorCuota, 2)

        valMlInt = valMlInteres
        valCuoInt = Mat.Redondear(valCuoCotIntRea * rTrn.tasaInteres, 2)

        valMlRea = valMlReajuste
        valCuoRea = valCuoCotIntRea - (valCuoCot + valCuoInt)

        valMlAdi = valMlAdicional
        valCuoAdi = Mat.Redondear(valMlAdi / rTrn.valMlValorCuota, 2)

        valMlAdiInt = valMlAdicionalInteres
        valCuoAdiInt = Mat.Redondear(valCuoAdiIntRea * rTrn.tasaInteres, 2)

        valMlAdiRea = valMlAdicionalReajuste
        valCuoAdiRea = valCuoAdiIntRea - (valCuoAdi + valCuoAdiInt)




        rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - rTrn.valMlPatrFrecCal

        If rTrn.valMlCompensCal > 0 Then

            rTrn.valCuoCompensCal = Mat.Redondear((valMlCotIntRea + rTrn.valMlCompensCal) / rTrn.valMlValorCuota, 2) - valCuoCotIntRea
        Else
            rTrn.valCuoCompensCal = Mat.Redondear(rTrn.valMlCompensCal / rTrn.valMlValorCuota, 2)
            'Segun BA BA-2007050232 del 24/05/2007
            'No se aplicarán perdidas si la cotizacion es insuficiente para generar abonos
            If rTrn.valCuoPatrFdesCal = 0 Then
                rTrn.valCuoCompensCal = 0
            End If

            'lfc//20-11-2009 ca-2009070092-planillas complementarias generan monto negativo
            ' cuando se paga adicional (comision) y la rentabilidad es negativa
            'rentabilidad negativa
            If rTrn.valCuoCompensCal < 0 Then  ' solo rentabilidad negativa        

                If valCuoCotIntRea = 0 And valCuoAdiIntRea > 0 And valCuoExc = 0 _
                   And (rTrn.valCuoCompensCal * -1) > rSal.valCuoSaldo And rSal.valCuoSaldo >= 0 Then
                    rTrn.valCuoCompensCal = rSal.valCuoSaldo * -1
                    rTrn.valMlCompensCal = Mat.Redondear(rTrn.valCuoCompensCal * rTrn.valMlValorCuota, 0)
                End If
            End If

        End If

        valMlExc += rTrn.valMlExcesoTopeCal
        valCuoExc += rTrn.valCuoExcesoTopeCal


        valDif = rTrn.valCuoPatrFdesCal - (valCuoCotIntRea + valCuoAdiIntRea + valCuoExc + rTrn.valCuoCompensCal)


        rTrn.valCuoAjusteDecimalCal = valDif
        rTrn.valMlAjusteDecimalCal = 0

        rTrn.valMlMvtoCal = valMlCot
        rTrn.valCuoMvtoCal = valCuoCot

        rTrn.valMlInteresCal = valMlInt
        rTrn.valCuoInteresCal = valCuoInt

        rTrn.valMlReajusteCal = valMlRea
        rTrn.valCuoReajusteCal = valCuoRea

        rTrn.valMlAdicionalCal = valMlAdi
        rTrn.valCuoAdicionalCal = valCuoAdi

        'Se agrega AJUSTE DECIMAL a ADICIONAL, Solo en CUOTAS.15/10/2010
        'ajustesDecimalAComisionPorc()
        'rTrn.valCuoAdicionalCal = valCuoAdi - g_valCuoAjusteDec
        'PCI
        'rTrn.valCuoAjusteDecimalCal = 0

        rTrn.valMlAdicionalInteresCal = valMlAdiInt
        rTrn.valCuoAdicionalInteresCal = valCuoAdiInt

        rTrn.valMlAdicionalReajusteCal = valMlAdiRea
        rTrn.valCuoAdicionalReajusteCal = valCuoAdiRea

        rTrn.valMlExcesoTopeCal = valMlExc
        rTrn.valCuoExcesoTopeCal = valCuoExc


    End Sub

    '//SIS-- duplica procedimiento
    ' lfc: ca-1214034  expasis en TGR 20-05-2019, añade parametro
    Private Sub CalcularNominales9(ByVal valMlMvto As Decimal, ByVal valMlInteres As Decimal, ByVal valMlReajuste As Decimal, ByVal valMlAdicional As Decimal, ByVal valMlAdicionalInteres As Decimal, ByVal valMlAdicionalReajuste As Decimal, ByVal valMlExceso As Decimal, ByVal valMlPrima As Decimal, ByVal valMlPrimaInteres As Decimal, ByVal valMlPrimaReajuste As Decimal, ByVal valMlExcesoEmp As Decimal)
        Dim valDif As Decimal
        'Dim valCuoDif As Decimal
        'Dim valMlDif As Decimal
        Dim valMlCotIntRea As Decimal
        Dim valCuoCotIntRea As Decimal
        Dim valMlAdiIntRea As Decimal
        Dim valCuoAdiIntRea As Decimal
        Dim valMlExc As Decimal
        Dim valCuoExc As Decimal
        Dim valMlCot As Decimal
        Dim valCuoCot As Decimal
        Dim valMlInt As Decimal
        Dim valCuoInt As Decimal
        Dim valMlRea As Decimal
        Dim valCuoRea As Decimal
        Dim valMlAdi As Decimal
        Dim valCuoAdi As Decimal
        Dim valMlAdiInt As Decimal
        Dim valCuoAdiInt As Decimal
        Dim valMlAdiRea As Decimal
        Dim valCuoAdiRea As Decimal
        Dim valCuoTotNominal As Decimal
        Dim valMlTotNominal As Decimal

        'SIS//
        Dim valMlPrimaIntRea As Decimal
        Dim valCuoPrimaIntRea As Decimal
        Dim valMlPrim As Decimal
        Dim valCuoPrim As Decimal
        Dim valMlPrimInt As Decimal
        Dim valCuoPrimInt As Decimal
        Dim valMlPrimRea As Decimal
        Dim valCuoPrimRea As Decimal

        ' lfc: ca-1214034  expasis en TGR 20-05-2019
        Dim valMlExcEmpl As Decimal = 0
        Dim valCuoExcEmpl As Decimal = 0


        ''LFC: 13/09/2010 - OS_3175470 // 
        'If rTrn.codOrigenProceso = "TRAIPAGN" Or rTrn.codOrigenProceso = "TRAIPAGC" Or rTrn.codOrigenProceso = "TRAINREZ" Then
        '    valMlTotNominal = rTrn.valMlMontoNominal
        'Else
        '    valMlTotNominal = rTrn.valMlPatrFrecCal
        'End If

        valMlTotNominal = rTrn.valMlPatrFrecCal

        If rTrn.codOrigenTransaccion = "REZ" Then
            valCuoTotNominal = rTrn.valCuoPatrFrecCal
        Else
            valCuoTotNominal = Mat.Redondear(valMlTotNominal / rTrn.valMlValorCuota, 2)
        End If

        'conceptos sumados fondo, adicional y exceso
        valMlCotIntRea = valMlMvto + valMlInteres + valMlReajuste
        valCuoCotIntRea = Mat.Redondear(valMlCotIntRea / rTrn.valMlValorCuota, 2)

        valMlAdiIntRea = valMlAdicional + valMlAdicionalInteres + valMlAdicionalReajuste
        valCuoAdiIntRea = Mat.Redondear(valMlAdiIntRea / rTrn.valMlValorCuota, 2)

        valMlExc = valMlExceso
        valCuoExc = Mat.Redondear(valMlExc / rTrn.valMlValorCuota, 2)

        'SIS//             ' montos parametros de sis informado
        valMlPrimaIntRea = valMlPrima + valMlPrimaInteres + valMlPrimaReajuste
        valCuoPrimaIntRea = Mat.Redondear(valMlPrimaIntRea / rTrn.valMlValorCuota, 2)


        'conceptos individuales fondo, adicional,interes, reajuste, exceso, etc
        valMlCot = valMlMvto
        valCuoCot = Mat.Redondear(valMlCot / rTrn.valMlValorCuota, 2)

        valMlInt = valMlInteres
        valCuoInt = Mat.Redondear(valCuoCotIntRea * rTrn.tasaInteres, 2)

        valMlRea = valMlReajuste
        valCuoRea = valCuoCotIntRea - (valCuoCot + valCuoInt)

        valMlAdi = valMlAdicional
        valCuoAdi = Mat.Redondear(valMlAdi / rTrn.valMlValorCuota, 2)

        valMlAdiInt = valMlAdicionalInteres
        valCuoAdiInt = Mat.Redondear(valCuoAdiIntRea * rTrn.tasaInteres, 2)

        valMlAdiRea = valMlAdicionalReajuste
        valCuoAdiRea = valCuoAdiIntRea - (valCuoAdi + valCuoAdiInt)


        'LFC: 13/09/2010 - OS_3175470 
        If rTrn.codOrigenProceso = "TRAIPAGN" Or rTrn.codOrigenProceso = "TRAIPAGC" Or rTrn.codOrigenProceso = "TRAINREZ" Then
            If rTrn.tipoFondoDestinoCal = "C" Then
                rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
            Else
                'rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - rTrn.valMlMontoNominal
                rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
            End If
        Else
            rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - rTrn.valMlPatrFrecCal
        End If

        'primaSIS
        valMlPrim = valMlPrima
        valCuoPrim = Mat.Redondear(valMlPrim / rTrn.valMlValorCuota, 2)
        valMlPrimInt = valMlPrimaInteres
        valCuoPrimInt = Mat.Redondear(valMlPrimaIntRea * rTrn.tasaInteres, 2)
        valMlPrimRea = valMlPrimaReajuste
        valCuoPrimRea = valCuoPrimaIntRea - (valCuoPrim + valCuoPrimInt)

        If rTrn.valMlCompensCal > 0 Then
            rTrn.valCuoCompensCal = Mat.Redondear((valMlCotIntRea + rTrn.valMlCompensCal) / rTrn.valMlValorCuota, 2) - valCuoCotIntRea
        Else
            rTrn.valCuoCompensCal = Mat.Redondear(rTrn.valMlCompensCal / rTrn.valMlValorCuota, 2)
            'Segun BA BA-2007050232 del 24/05/2007
            'No se aplicarán perdidas si la cotizacion es insuficiente para generar abonos
            If rTrn.valCuoPatrFdesCal = 0 Then
                rTrn.valCuoCompensCal = 0
            End If

            '-------------->>>>>>>>>>>>>>>>-----------------------
            'lfc//20-11-2009 ca-2009070092-planillas complementarias generan monto negativo
            ' cuando se paga adicional (comision) y la rentabilidad es negativa
            'rentabilidad negativa
            If rTrn.valCuoCompensCal < 0 Then  ' solo rentabilidad negativa        

				If rTrn.sexo = "Fx" And (valCuoPrim + valCuoPrimInt + valCuoPrimRea) > 0 Then
				Else
					If valCuoCotIntRea = 0 And valCuoAdiIntRea > 0 And valCuoExc = 0 And (rTrn.valCuoCompensCal * -1) > rSal.valCuoSaldo And rSal.valCuoSaldo > 0 Then
						rTrn.valCuoCompensCal = rSal.valCuoSaldo * -1
						rTrn.valMlCompensCal = Mat.Redondear(rTrn.valCuoCompensCal * rTrn.valMlValorCuota, 0)
					End If
				End If
			End If			'------<<<<<<<<<<<<<<<<<<<<<<<<<<<---------------------
        End If

        valMlExc += rTrn.valMlExcesoTopeCal
        valCuoExc += rTrn.valCuoExcesoTopeCal
        '--------------------------------------------------------------------------------------------------------+  sis

        ' lfc: ca-1214034  expasis en TGR 20-05-2019
        If blExpasisTGR Then
            valMlExcEmpl = valMlExcesoEmp
            valCuoExcEmpl = Math.Round(valMlExcEmpl / rTrn.valMlValorCuota, 2)
        End If
        '--<<<---

        'lfc:27/07/2010-------ajuste decimal: os_2626661
        '------------------>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        ' lfc: ca-1214034  expasis en TGR 20-05-2019
        valDif = rTrn.valCuoPatrFdesCal - (valCuoCotIntRea + valCuoAdiIntRea + valCuoExc + valCuoExcEmpl + rTrn.valCuoCompensCal + valCuoPrimaIntRea)





        'valCuoDif = rTrn.valCuoPatrFdesCal - (valCuoCotIntRea + valCuoAdiIntRea + valCuoExc + rTrn.valCuoCompensCal + valCuoPrimaIntRea)
        'valMlDif = rTrn.valMlPatrFdesCal - (valMlCotIntRea + valMlAdiIntRea + valMlExc + rTrn.valMlCompensCal + valMlPrimaIntRea)

        rTrn.valCuoAjusteDecimalCal = valDif
        rTrn.valMlAjusteDecimalCal = 0
        '-----------------<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        'ajustesDecimalAComisionPorc()

        rTrn.valMlMvtoCal = valMlCot
        rTrn.valCuoMvtoCal = valCuoCot

        rTrn.valMlInteresCal = valMlInt
        rTrn.valCuoInteresCal = valCuoInt

        rTrn.valMlReajusteCal = valMlRea
        rTrn.valCuoReajusteCal = valCuoRea


        rTrn.valMlAdicionalCal = valMlAdi
        rTrn.valCuoAdicionalCal = valCuoAdi

        'Se agrega AJUSTE DECIMAL a ADICIONAL, Solo en CUOTAS.15/10/2010
        ''Se vuelve atras con el AJUSTE DECIMAL. 09/11/2010 PCI
        'ajustesDecimalAComisionPorc()
        'rTrn.valCuoAdicionalCal = valCuoAdi - g_valCuoAjusteDec

        'PCI
        'rTrn.valCuoAjusteDecimalCal = 0


        rTrn.valMlAdicionalInteresCal = valMlAdiInt
        rTrn.valCuoAdicionalInteresCal = valCuoAdiInt

        rTrn.valMlAdicionalReajusteCal = valMlAdiRea
        rTrn.valCuoAdicionalReajusteCal = valCuoAdiRea

        rTrn.valMlExcesoTopeCal = valMlExc
        rTrn.valCuoExcesoTopeCal = valCuoExc

        ' lfc: ca-1214034  expasis en TGR 20-05-2019
        If blExpasisTGR Then
            rTrn.valMlExcesoEmplCal = valMlExcEmpl
            rTrn.valCuoExcesoEmplCal = Mat.Redondear(valCuoExcEmpl, 2)
        End If
        '--<<<----

        'primaSIS

        rTrn.valMlPrimaSisCal = valMlPrim
        rTrn.valCuoPrimaSisCal = valCuoPrim
        rTrn.valMlPrimaSisInteresCal = valMlPrimInt
        rTrn.valCuoPrimaSisInteresCal = valCuoPrimInt
        rTrn.valMlPrimaSisReajusteCal = valMlPrimRea
        rTrn.valCuoPrimaSisReajusteCal = valCuoPrimRea
    End Sub


    '//SIS-- duplica procedimiento
    Private Sub CalcularNominales8(ByVal valMlMvto As Decimal, ByVal valMlInteres As Decimal, ByVal valMlReajuste As Decimal, ByVal valMlAdicional As Decimal, ByVal valMlAdicionalInteres As Decimal, ByVal valMlAdicionalReajuste As Decimal, ByVal valMlExceso As Decimal, ByVal valMlPrima As Decimal, ByVal valMlPrimaInteres As Decimal, ByVal valMlPrimaReajuste As Decimal)
        Dim valDif As Decimal
        Dim valDifUni As Decimal
        'Dim valCuoDif As Decimal
        'Dim valMlDif As Decimal
        Dim valMlCotIntRea As Decimal
        Dim valCuoCotIntRea As Decimal
        Dim valMlAdiIntRea As Decimal
        Dim valCuoAdiIntRea As Decimal
        Dim valMlExc As Decimal
        Dim valCuoExc As Decimal
        Dim valMlCot As Decimal
        Dim valCuoCot As Decimal
        Dim valMlInt As Decimal
        Dim valCuoInt As Decimal
        Dim valMlRea As Decimal
        Dim valCuoRea As Decimal
        Dim valMlAdi As Decimal
        Dim valCuoAdi As Decimal
        Dim valMlAdiInt As Decimal
        Dim valCuoAdiInt As Decimal
        Dim valMlAdiRea As Decimal
        Dim valCuoAdiRea As Decimal
        Dim valCuoTotNominal As Decimal
        Dim valMlTotNominal As Decimal

        'SIS//
        Dim valMlPrimaIntRea As Decimal
        Dim valCuoPrimaIntRea As Decimal
        Dim valMlPrim As Decimal
        Dim valCuoPrim As Decimal
        Dim valMlPrimInt As Decimal
        Dim valCuoPrimInt As Decimal
        Dim valMlPrimRea As Decimal
        Dim valCuoPrimRea As Decimal

        If rTrn.codOrigenMvto = "TRAIPAGN" Or rTrn.codOrigenMvto = "TRAIPAGC" Or rTrn.codOrigenMvto = "TRAINREZ" Then
            If rTrn.tipoFondoDestinoCal = "C" Then
                valMlTotNominal = rTrn.valMlPatrFrecCal
            Else
                'If gcodAdministradora = 1034 Then
                '    valMlTotNominal = rTrn.valMlPatrFrecCal
                'Else
                '    valMlTotNominal = rTrn.valMlMontoNominal
                'End If
                'PCI 06/09/2012

                valMlTotNominal = rTrn.valMlPatrFrecCal
            End If
        Else
            valMlTotNominal = rTrn.valMlPatrFrecCal
        End If

        ''If rTrn.codOrigenProceso = "TRAIPAGN" Then
        ''    valMlTotNominal = rTrn.valMlMontoNominal
        ''Else
        'valMlTotNominal = rTrn.valMlPatrFrecCal '- rTrn.valMlTransferenciaCal
        ''End If


        If rTrn.codOrigenTransaccion = "REZ" Then
            If rTrn.codOrigenMvto = "TRAIPAGN" Or rTrn.codOrigenMvto = "TRAIPAGC" Or rTrn.codOrigenMvto = "TRAINREZ" Then
                If rTrn.tipoFondoDestinoCal = "C" Then
                    valCuoTotNominal = rTrn.valCuoPatrFrecCal
                Else
                    valCuoTotNominal = (valMlTotNominal / rTrn.valMlValorCuota)
                End If
            Else
                valCuoTotNominal = rTrn.valCuoPatrFrecCal
            End If
        Else
            'PCI No se utiliza Redondeo hasta el final.
            'valCuoTotNominal = Mat.Redondear(valMlTotNominal / rTrn.valMlValorCuota, 2)
            valCuoTotNominal = (valMlTotNominal / rTrn.valMlValorCuota)
        End If

        'conceptos sumados fondo, adicional y exceso
        valMlCotIntRea = valMlMvto + valMlInteres + valMlReajuste
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoCotIntRea = Mat.Redondear(valMlCotIntRea / rTrn.valMlValorCuota, 2)
        valCuoCotIntRea = (valMlCotIntRea / rTrn.valMlValorCuota)

        valMlAdiIntRea = valMlAdicional + valMlAdicionalInteres + valMlAdicionalReajuste
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoAdiIntRea = Mat.Redondear(valMlAdiIntRea / rTrn.valMlValorCuota, 2)
        valCuoAdiIntRea = (valMlAdiIntRea / rTrn.valMlValorCuota)

        valMlExc = valMlExceso
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoExc = Mat.Redondear(valMlExc / rTrn.valMlValorCuota, 2)
        valCuoExc = (valMlExc / rTrn.valMlValorCuota)

        'SIS//             ' montos parametros de sis informado
        valMlPrimaIntRea = valMlPrima + valMlPrimaInteres + valMlPrimaReajuste
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoPrimaIntRea = Mat.Redondear(valMlPrimaIntRea / rTrn.valMlValorCuota, 2)
        valCuoPrimaIntRea = (valMlPrimaIntRea / rTrn.valMlValorCuota)

        'conceptos individuales fondo, adicional,interes, reajuste, exceso, etc
        valMlCot = valMlMvto
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoCot = Mat.Redondear(valMlCot / rTrn.valMlValorCuota, 2)
        valCuoCot = (valMlCot / rTrn.valMlValorCuota)


        'Nuevo

        valMlInt = valMlInteres
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoInt = Mat.Redondear(valCuoCotIntRea * rTrn.tasaInteres, 2)
        'valCuoInt = (valCuoCotIntRea * rTrn.tasaInteres)
        valCuoInt = (valCuoCotIntRea * (rTrn.tasaInteres / 100))

        valMlRea = valMlReajuste
        valCuoRea = valCuoCotIntRea - (valCuoCot + valCuoInt)

        valMlAdi = valMlAdicional
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoAdi = Mat.Redondear(valMlAdi / rTrn.valMlValorCuota, 2)
        valCuoAdi = (valMlAdi / rTrn.valMlValorCuota)

        valMlAdiInt = valMlAdicionalInteres
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoAdiInt = Mat.Redondear(valCuoAdiIntRea * rTrn.tasaInteres, 2)
        'valCuoAdiInt = (valCuoAdiIntRea * rTrn.tasaInteres)
        valCuoAdiInt = (valCuoAdiIntRea * (rTrn.tasaInteres / 100))


        valMlAdiRea = valMlAdicionalReajuste
        valCuoAdiRea = valCuoAdiIntRea - (valCuoAdi + valCuoAdiInt)


        'LFC: 13/09/2010 - OS_3175470 
        If rTrn.codOrigenProceso = "TRAIPAGN" Or rTrn.codOrigenProceso = "TRAIPAGC" Or rTrn.codOrigenProceso = "TRAINREZ" Then
            If rTrn.tipoFondoDestinoCal = "C" Then
                rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
            Else
                'rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - rTrn.valMlMontoNominal
                rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
            End If
        Else
            If rTrn.codOrigenMvto = "TRAIPAGN" Or rTrn.codOrigenMvto = "TRAIPAGC" Or rTrn.codOrigenMvto = "TRAINREZ" Then
                rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
            Else
                rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - rTrn.valMlPatrFrecCal
            End If
        End If

        'primaSIS
        valMlPrim = valMlPrima
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoPrim = Mat.Redondear(valMlPrim / rTrn.valMlValorCuota, 2)
        valCuoPrim = (valMlPrim / rTrn.valMlValorCuota)

        valMlPrimInt = valMlPrimaInteres
        'PCI No se utiliza Redondeo hasta el final.
        'valCuoPrimInt = Mat.Redondear(valMlPrimaIntRea * rTrn.tasaInteres, 2)
        'valCuoPrimInt = (valMlPrimaIntRea * rTrn.tasaInteres)
        valCuoPrimInt = (valCuoPrimaIntRea * (rTrn.tasaInteres / 100))

        valMlPrimRea = valMlPrimaReajuste
        valCuoPrimRea = valCuoPrimaIntRea - (valCuoPrim + valCuoPrimInt)

        If rTrn.valMlCompensCal > 0 Then
            'PCI No se utiliza Redondeo hasta el final.
            'rTrn.valCuoCompensCal = Mat.Redondear((valMlCotIntRea + rTrn.valMlCompensCal) / rTrn.valMlValorCuota, 2) - valCuoCotIntRea
            rTrn.valCuoCompensCal = ((valMlCotIntRea + rTrn.valMlCompensCal) / rTrn.valMlValorCuota) - valCuoCotIntRea

            If Mat.Redondear(rTrn.valCuoCompensCal, 2) = 0 Then
                rTrn.valCuoCompensCal = 0
            End If
        Else
            'PCI No se utiliza Redondeo hasta el final.
            'rTrn.valCuoCompensCal = Mat.Redondear(rTrn.valMlCompensCal / rTrn.valMlValorCuota, 2)
            rTrn.valCuoCompensCal = (rTrn.valMlCompensCal / rTrn.valMlValorCuota)
            'Segun BA BA-2007050232 del 24/05/2007
            'No se aplicarán perdidas si la cotizacion es insuficiente para generar abonos
            If rTrn.valCuoPatrFdesCal = 0 Then
                rTrn.valCuoCompensCal = 0
            End If

            '-------------->>>>>>>>>>>>>>>>-----------------------
            'lfc//20-11-2009 ca-2009070092-planillas complementarias generan monto negativo
            ' cuando se paga adicional (comision) y la rentabilidad es negativa
            'rentabilidad negativa
            If rTrn.valCuoCompensCal < 0 Then  ' solo rentabilidad negativa        

				If rTrn.sexo = "Fx" And (valCuoPrim + valCuoPrimInt + valCuoPrimRea) > 0 Then
				Else
					If valCuoCotIntRea = 0 And valCuoAdiIntRea > 0 And valCuoExc = 0 And (rTrn.valCuoCompensCal * -1) > rSal.valCuoSaldo And rSal.valCuoSaldo > 0 Then
						rTrn.valCuoCompensCal = rSal.valCuoSaldo * -1

						'PCI No se utiliza Redondeo hasta el final.
						'rTrn.valMlCompensCal = Mat.Redondear(rTrn.valCuoCompensCal * rTrn.valMlValorCuota, 0)
						rTrn.valMlCompensCal = (rTrn.valCuoCompensCal * rTrn.valMlValorCuota)
					End If
				End If
			End If			'------<<<<<<<<<<<<<<<<<<<<<<<<<<<---------------------
        End If

        valMlExc += rTrn.valMlExcesoTopeCal
        valCuoExc += rTrn.valCuoExcesoTopeCal

        '--------------------------------------------------------------------------------------------------------+  sis
        'Verifica Diferencias entre Valores Individuales y Acumulados ya que tambien genera Ajuste Decimal
        valDifUni = Mat.Redondear(valCuoCotIntRea, 2) - (Mat.Redondear(valCuoCot, 2) + Mat.Redondear(valCuoInt, 2) + Mat.Redondear(valCuoRea, 2))
        If valDifUni > 0 Then
            If valMlInt > 0 Then
                valCuoInt += valDifUni
            ElseIf valMlRea > 0 Then
                valCuoRea += valDifUni
            End If
        End If

        valDifUni = Mat.Redondear(valCuoAdiIntRea, 2) - (Mat.Redondear(valCuoAdi, 2) + Mat.Redondear(valCuoAdiInt, 2) + Mat.Redondear(valCuoAdiRea, 2))
        If valDifUni > 0 Then
            If valMlAdiInt > 0 Then
                valCuoAdiInt += valDifUni
            ElseIf valMlAdiRea > 0 Then
                valCuoAdiRea += valDifUni
            End If
        End If

        valDifUni = Mat.Redondear(valCuoPrimaIntRea, 2) - (Mat.Redondear(valCuoPrim, 2) + Mat.Redondear(valCuoPrimInt, 2) + Mat.Redondear(valCuoPrimRea, 2))
        If valDifUni > 0 Then
            If valMlPrimInt > 0 Then
                valCuoPrimInt += valDifUni
            ElseIf valMlPrimRea > 0 Then
                valCuoPrimRea += valDifUni
            End If
        End If

        'lfc:27/07/2010-------ajuste decimal: os_2626661
        '------------------>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        'PCI No se utiliza Redondeo hasta el final.
        'valDif = rTrn.valCuoPatrFdesCal - (valCuoCotIntRea + valCuoAdiIntRea + valCuoExc + rTrn.valCuoCompensCal + valCuoPrimaIntRea)
        'valDif = rTrn.valCuoPatrFdesCal - Mat.Redondear((valCuoCotIntRea + valCuoAdiIntRea + valCuoExc + rTrn.valCuoCompensCal + valCuoPrimaIntRea), 2)

        'valDif = rTrn.valCuoPatrFdesCal - (Mat.Redondear(valCuoCotIntRea, 2) + Mat.Redondear(valCuoAdiIntRea, 2) + Mat.Redondear(valCuoExc, 2) + Mat.Redondear(rTrn.valCuoCompensCal, 2) + Mat.Redondear(valCuoPrimaIntRea, 2))
        valDif = rTrn.valCuoPatrFdesCal - (Mat.Redondear(valCuoCot, 2) + Mat.Redondear(valCuoInt, 2) + Mat.Redondear(valCuoRea, 2) + Mat.Redondear(valCuoAdi, 2) + Mat.Redondear(valCuoAdiInt, 2) + Mat.Redondear(valCuoAdiRea, 2) + Mat.Redondear(valCuoExc, 2) + Mat.Redondear(rTrn.valCuoCompensCal, 2) + Mat.Redondear(valCuoPrim, 2) + Mat.Redondear(valCuoPrimInt, 2) + Mat.Redondear(valCuoPrimRea, 2))

        'valCuoDif = rTrn.valCuoPatrFdesCal - (valCuoCotIntRea + valCuoAdiIntRea + valCuoExc + rTrn.valCuoCompensCal + valCuoPrimaIntRea)
        'valMlDif = rTrn.valMlPatrFdesCal - (valMlCotIntRea + valMlAdiIntRea + valMlExc + rTrn.valMlCompensCal + valMlPrimaIntRea)

        'rTrn.valCuoAjusteDecimalCal = valDif
        rTrn.valMlAjusteDecimalCal = 0
        '-----------------<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        VerificaAjustesDecimal(valDif, valCuoCot, valCuoAdi, valCuoAdiInt, valCuoAdiRea, valCuoPrim, rTrn.valCuoCompensCal)

        rTrn.valMlMvtoCal = valMlCot
        'PCI No se utiliza Redondeo hasta el final.
        'rTrn.valCuoMvtoCal = valCuoCot
        rTrn.valCuoMvtoCal = Mat.Redondear(valCuoCot, 2)

        rTrn.valMlInteresCal = valMlInt
        'PCI No se utiliza Redondeo hasta el final.
        'rTrn.valCuoInteresCal = valCuoInt
        rTrn.valCuoInteresCal = Mat.Redondear(valCuoInt, 2)

        rTrn.valMlReajusteCal = valMlRea
        'PCI No se utiliza Redondeo hasta el final.
        'rTrn.valCuoReajusteCal = valCuoRea
        rTrn.valCuoReajusteCal = Mat.Redondear(valCuoRea, 2)


        rTrn.valMlAdicionalCal = valMlAdi
        'PCI No se utiliza Redondeo hasta el final.
        'rTrn.valCuoAdicionalCal = valCuoAdi
        rTrn.valCuoAdicionalCal = Mat.Redondear(valCuoAdi, 2)

        'Se agrega AJUSTE DECIMAL a ADICIONAL, Solo en CUOTAS.15/10/2010
        ''Se vuelve atras con el AJUSTE DECIMAL. 09/11/2010 PCI
        'ajustesDecimalAComisionPorc()
        'rTrn.valCuoAdicionalCal = valCuoAdi - g_valCuoAjusteDec

        'PCI
        'rTrn.valCuoAjusteDecimalCal = 0

        rTrn.valMlAdicionalInteresCal = valMlAdiInt
        'PCI No se utiliza Redondeo hasta el final.
        'rTrn.valCuoAdicionalInteresCal = valCuoAdiInt
        rTrn.valCuoAdicionalInteresCal = Mat.Redondear(valCuoAdiInt, 2)

        rTrn.valMlAdicionalReajusteCal = valMlAdiRea
        'PCI No se utiliza Redondeo hasta el final.
        'rTrn.valCuoAdicionalReajusteCal = valCuoAdiRea
        rTrn.valCuoAdicionalReajusteCal = Mat.Redondear(valCuoAdiRea, 2)

        rTrn.valMlExcesoTopeCal = valMlExc
        'PCI No se utiliza Redondeo hasta el final.
        'rTrn.valCuoExcesoTopeCal = valCuoExc
        rTrn.valCuoExcesoTopeCal = Mat.Redondear(valCuoExc, 2)

        'primaSIS

        rTrn.valCuoCompensCal = Mat.Redondear(rTrn.valCuoCompensCal, 2)

        rTrn.valMlPrimaSisCal = valMlPrim
        'PCI No se utiliza Redondeo hasta el final.
        'rTrn.valCuoPrimaSisCal = valCuoPrim
        rTrn.valCuoPrimaSisCal = Mat.Redondear(valCuoPrim, 2)
        rTrn.valMlPrimaSisInteresCal = valMlPrimInt
        'PCI No se utiliza Redondeo hasta el final.
        'rTrn.valCuoPrimaSisInteresCal = valCuoPrimInt
        rTrn.valCuoPrimaSisInteresCal = Mat.Redondear(valCuoPrimInt, 2)
        rTrn.valMlPrimaSisReajusteCal = valMlPrimRea
        'PCI No se utiliza Redondeo hasta el final.
        'rTrn.valCuoPrimaSisReajusteCal = valCuoPrimRea
        rTrn.valCuoPrimaSisReajusteCal = Mat.Redondear(valCuoPrimRea, 2)
    End Sub



    'Private Sub CalcularNominales_old(ByVal valMlMvto As Decimal, _
    '                             ByVal valMlInteres As Decimal, _
    '                             ByVal valMlReajuste As Decimal, _
    '                             ByVal valMlAdicional As Decimal, _
    '                             ByVal valMlAdicionalInteres As Decimal, _
    '                             ByVal valMlAdicionalReajuste As Decimal, _
    '                             ByVal valMlExceso As Decimal)


    '    Dim valDif As Decimal

    '    Dim valMlCotIntRea As Decimal
    '    Dim valCuoCotIntRea As Decimal

    '    Dim valMlAdiIntRea As Decimal
    '    Dim valCuoAdiIntRea As Decimal

    '    Dim valMlExc As Decimal
    '    Dim valCuoExc As Decimal

    '    Dim valMlCot As Decimal
    '    Dim valCuoCot As Decimal

    '    Dim valMlInt As Decimal
    '    Dim valCuoInt As Decimal

    '    Dim valMlRea As Decimal
    '    Dim valCuoRea As Decimal

    '    Dim valMlAdi As Decimal
    '    Dim valCuoAdi As Decimal

    '    Dim valMlAdiInt As Decimal
    '    Dim valCuoAdiInt As Decimal

    '    Dim valMlAdiRea As Decimal
    '    Dim valCuoAdiRea As Decimal

    '    Dim valCuoTotNominal As Decimal
    '    Dim valMlTotNominal As Decimal


    '    valMlTotNominal = rTrn.valMlPatrFrecCal - rTrn.valMlTransferenciaCal

    '    If rTrn.codOrigenTransaccion = "REZ" Then
    '        valCuoTotNominal = rTrn.valCuoPatrFrecCal
    '    Else
    '        valCuoTotNominal = Mat.Redondear(valMlTotNominal / rTrn.valMlValorCuota, 2)
    '    End If

    '    'conceptos sumados fondo, adicional y exceso

    '    valMlCotIntRea = valMlMvto + valMlInteres + valMlReajuste
    '    valCuoCotIntRea = Mat.Redondear(valMlCotIntRea / rTrn.valMlValorCuota, 2)

    '    valMlAdiIntRea = valMlAdicional + valMlAdicionalInteres + valMlAdicionalReajuste
    '    valCuoAdiIntRea = Mat.Redondear(valMlAdiIntRea / rTrn.valMlValorCuota, 2)

    '    valMlExc = valMlExceso
    '    valCuoExc = Mat.Redondear(valMlExc / rTrn.valMlValorCuota, 2)

    '    'conceptos individuales fondo, adicional,interes, reajuste, exceso, etc

    '    valMlCot = valMlMvto
    '    valCuoCot = Mat.Redondear(valMlCot / rTrn.valMlValorCuota, 2)

    '    valMlInt = valMlInteres
    '    valCuoInt = Mat.Redondear(valCuoCotIntRea * rTrn.tasaInteres, 2)

    '    valMlRea = valMlReajuste
    '    valCuoRea = valCuoCotIntRea - (valCuoCot + valCuoInt)

    '    valMlAdi = valMlAdicional
    '    valCuoAdi = Mat.Redondear(valMlAdi / rTrn.valMlValorCuota, 2)

    '    valMlAdiInt = valMlAdicionalInteres
    '    valCuoAdiInt = Mat.Redondear(valCuoAdiIntRea * rTrn.tasaInteres, 2)

    '    valMlAdiRea = valMlAdicionalReajuste
    '    valCuoAdiRea = valCuoAdiIntRea - (valCuoAdi + valCuoAdiInt)


    '    valDif = valCuoTotNominal - (valCuoCotIntRea + valCuoAdiIntRea + valCuoExc)


    '    rTrn.valCuoAjusteDecimalCal = valDif
    '    rTrn.valMlAjusteDecimalCal = 0

    '    rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - rTrn.valMlPatrFrecCal
    '    rTrn.valCuoCompensCal = Mat.Redondear(rTrn.valMlCompensCal / rTrn.valMlValorCuota, 2)



    '    rTrn.valMlMvtoCal = valMlCot
    '    rTrn.valCuoMvtoCal = valCuoCot

    '    rTrn.valMlInteresCal = valMlInt
    '    rTrn.valCuoInteresCal = valCuoInt

    '    rTrn.valMlReajusteCal = valMlRea
    '    rTrn.valCuoReajusteCal = valCuoRea

    '    rTrn.valMlAdicionalCal = valMlAdi
    '    rTrn.valCuoAdicionalCal = valCuoAdi

    '    rTrn.valMlAdicionalInteresCal = valMlAdiInt
    '    rTrn.valCuoAdicionalInteresCal = valCuoAdiInt

    '    rTrn.valMlAdicionalReajusteCal = valMlAdiRea
    '    rTrn.valCuoAdicionalReajusteCal = valCuoAdiRea

    '    rTrn.valMlExcesoTopeCal += valMlExc
    '    rTrn.valCuoExcesoTopeCal += valCuoExc


    'End Sub
    Private Sub Actualizar(ByVal valCuoRezago As Decimal, _
                            ByRef valMlMvto As Decimal, _
                           ByRef valMlInteres As Decimal, _
                           ByRef valMlReajuste As Decimal, _
                           ByRef valMlAdicional As Decimal, _
                           ByRef valMlAdicionalInteres As Decimal, _
                           ByRef valMlAdicionalReajuste As Decimal, _
                           ByRef valMlExceso As Decimal)

        Dim valCuoMvto As Decimal
        Dim valCuoInteres As Decimal
        Dim valCuoReajuste As Decimal
        Dim valCuoAdicional As Decimal
        Dim valCuoAdicionalInteres As Decimal
        Dim valCuoAdicionalReajuste As Decimal
        Dim valCuoExceso As Decimal

        valCuoMvto = Mat.Redondear(valMlMvto / valCuoRezago, 2)
        valMlMvto = Mat.Redondear(valCuoMvto * gvalMlCuotaDestinoC, 0)

        valCuoInteres = Mat.Redondear(valMlInteres / valCuoRezago, 2)
        valMlInteres = Mat.Redondear(valCuoInteres * gvalMlCuotaDestinoC, 0)

        valCuoReajuste = Mat.Redondear(valMlReajuste / valCuoRezago, 2)
        valMlReajuste = Mat.Redondear(valCuoReajuste * gvalMlCuotaDestinoC, 0)

        valCuoAdicional = Mat.Redondear(valMlAdicional / valCuoRezago, 2)
        valMlAdicional = Mat.Redondear(valCuoAdicional * gvalMlCuotaDestinoC, 0)

        valCuoAdicionalInteres = Mat.Redondear(valMlAdicionalInteres / valCuoRezago, 2)
        valMlAdicionalInteres = Mat.Redondear(valCuoAdicionalInteres * gvalMlCuotaDestinoC, 0)

        valCuoAdicionalReajuste = Mat.Redondear(valMlAdicionalReajuste / valCuoRezago, 2)
        valMlAdicionalReajuste = Mat.Redondear(valCuoAdicionalReajuste * gvalMlCuotaDestinoC, 0)

        valCuoExceso = Mat.Redondear(valMlExceso / valCuoRezago, 2)
        valMlExceso = Mat.Redondear(valCuoExceso * gvalMlCuotaDestinoC, 0)

    End Sub
    Private Sub CalcularNominalesValorizado(ByVal valMlTotNominal As Decimal, _
                                            ByVal valCuoTotNominal As Decimal, _
                                            ByVal valMlMvto As Decimal, _
                                            ByVal valMlInteres As Decimal, _
                                            ByVal valMlReajuste As Decimal, _
                                            ByVal valMlAdicional As Decimal, _
                                            ByVal valMlAdicionalInteres As Decimal, _
                                            ByVal valMlAdicionalReajuste As Decimal, _
                                            ByVal valMlExceso As Decimal)

        Dim valDif As Decimal
        'Dim valCuoDif As Decimal
        'Dim valMlDif As Decimal

        Dim valMlCotIntRea As Decimal
        Dim valCuoCotIntRea As Decimal

        Dim valMlAdiIntRea As Decimal
        Dim valCuoAdiIntRea As Decimal

        Dim valMlExc As Decimal
        Dim valCuoExc As Decimal

        Dim valMlCot As Decimal
        Dim valCuoCot As Decimal

        Dim valMlInt As Decimal
        Dim valCuoInt As Decimal

        Dim valMlRea As Decimal
        Dim valCuoRea As Decimal

        Dim valMlAdi As Decimal
        Dim valCuoAdi As Decimal

        Dim valMlAdiInt As Decimal
        Dim valCuoAdiInt As Decimal

        Dim valMlAdiRea As Decimal
        Dim valCuoAdiRea As Decimal
        Dim valorCuotaRezago As Decimal
        Dim fecRezagoAntiguo As Date = New Date(1988, 1, 1).Date
        Dim fecOperacion As Date

        fecOperacion = rTrn.fecOperacion
        valorCuotaRezago = rTrn.valMlValorCuotaCaja


        valMlCotIntRea = valMlMvto + valMlInteres + valMlReajuste
        valCuoCotIntRea = Mat.Redondear(valMlCotIntRea / rTrn.valMlValorCuota, 2)

        If fecOperacion >= fecRezagoAntiguo Then  'rezago anterior no debe abonar el adicional a la cuenta
            valMlAdiIntRea = valMlAdicional + valMlAdicionalInteres + valMlAdicionalReajuste
            valCuoAdiIntRea = Mat.Redondear(valMlAdiIntRea / rTrn.valMlValorCuota, 2)
        End If

        valMlExc = valMlExceso
        valCuoExc = Mat.Redondear(valMlExc / rTrn.valMlValorCuota, 2)

        valMlCot = valMlMvto
        valCuoCot = Mat.Redondear(valMlCot / rTrn.valMlValorCuota, 2)

        valMlInt = valMlInteres
        valCuoInt = Mat.Redondear(valCuoCotIntRea * rTrn.tasaInteres, 2)

        valMlRea = valMlReajuste
        valCuoRea = valCuoCotIntRea - (valCuoCot + valCuoInt)

        If fecOperacion >= fecRezagoAntiguo Then 'rezago anterior no debe abonar el adicional a la cuenta

            valMlAdi = valMlAdicional
            valCuoAdi = Mat.Redondear(valMlAdi / rTrn.valMlValorCuota, 2)

            valMlAdiInt = valMlAdicionalInteres
            valCuoAdiInt = Mat.Redondear(valCuoAdiIntRea * rTrn.tasaInteres, 2)

            valMlAdiRea = valMlAdicionalReajuste
            valCuoAdiRea = valCuoAdiIntRea - (valCuoAdi + valCuoAdiInt)

        End If

        rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - rTrn.valMlPatrFrecCal

        If rTrn.valMlCompensCal > 0 Then

            rTrn.valCuoCompensCal = Mat.Redondear((valMlCotIntRea + rTrn.valMlCompensCal) / rTrn.valMlValorCuota, 2) - valCuoCotIntRea
        Else
            rTrn.valCuoCompensCal = Mat.Redondear(rTrn.valMlCompensCal / rTrn.valMlValorCuota, 2)
        End If


        'lfc:27/07/2010-------ajuste decimal: os_2626661
        '------------------>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        valDif = rTrn.valCuoPatrFdesCal - (valCuoCotIntRea + valCuoAdiIntRea + valCuoExc + rTrn.valCuoCompensCal)
        'valCuoDif = rTrn.valCuoPatrFdesCal - (valCuoCotIntRea + valCuoAdiIntRea + valCuoExc + rTrn.valCuoCompensCal)
        'valMlDif = rTrn.valMlPatrFdesCal - (valMlCotIntRea + valMlAdiIntRea + valMlExc + rTrn.valMlCompensCal)

        If rTrn.tipoFondoDestinoCal = "C" Then
            valCuoCot += valDif
            valDif = 0
        End If
        'If rTrn.tipoFondoDestinoCal = "C" Then
        '    valCuoCot += valCuoDif
        '    valCuoDif = 0
        'End If


        If Math.Abs(valDif) > 0.01 Then
            GenerarLog("E", 0, "Hebra " & gIdHebra & " - El rezago está descuadrado", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            blIgnorar = True
            Exit Sub
        End If

        rTrn.valCuoAjusteDecimalCal = valDif

        'ajustesDecimalAComisionPorc()

        rTrn.valMlAjusteDecimalCal = 0


        rTrn.valMlMvtoCal = valMlCot
        rTrn.valCuoMvtoCal = valCuoCot

        rTrn.valMlInteresCal = valMlInt
        rTrn.valCuoInteresCal = valCuoInt

        rTrn.valMlReajusteCal = valMlRea
        rTrn.valCuoReajusteCal = valCuoRea


        rTrn.valMlAdicionalCal = valMlAdi
        rTrn.valCuoAdicionalCal = valCuoAdi

        'Se agrega AJUSTE DECIMAL a ADICIONAL, Solo en CUOTAS.15/10/2010
        'rTrn.valCuoAdicionalCal = valCuoAdi
        ''Se vuelve atras de AJUSTE DECIMAL 09/11/2010
        'ajustesDecimalAComisionPorc()
        'rTrn.valCuoAdicionalCal = valCuoAdi - g_valCuoAjusteDec

        'PCI
        'rTrn.valCuoAjusteDecimalCal = 0


        rTrn.valMlAdicionalInteresCal = valMlAdiInt
        rTrn.valCuoAdicionalInteresCal = valCuoAdiInt

        rTrn.valMlAdicionalReajusteCal = valMlAdiRea
        rTrn.valCuoAdicionalReajusteCal = valCuoAdiRea


        rTrn.valMlExcesoTopeCal += valMlExc
        rTrn.valCuoExcesoTopeCal += valCuoExc


    End Sub
    'Private Sub CalcularNominalesIngresoRezago(ByVal valMlNominal As Decimal, _
    '                                           ByVal valMlTraspasado As Decimal)


    '    Dim valMlMontoPorDistribuir As Decimal
    '    Dim fecRezagoAntiguo As Date = New Date(1988, 1, 1).Date
    '    Dim rTope As ccAcrParametrosAcred
    '    Dim cuotasFondo As Decimal
    '    Dim cuotasAdicional As Decimal
    '    Dim fechaCaja As Date

    '    'rTrn.valMlCompensCal = valMlTraspasado - valMlNominal
    '    rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlNominal
    '    valMlMontoPorDistribuir = valMlNominal

    '    rTrn.valMlMvtoCal = 0
    '    rTrn.valMlInteresCal = 0
    '    rTrn.valMlReajusteCal = 0
    '    rTrn.valMlAdicionalCal = 0
    '    rTrn.valMlAdicionalInteresCal = 0
    '    rTrn.valMlAdicionalReajusteCal = 0

    '    If rTrn.codOrigenMvto = "RECAUDAC" Then
    '        fechaCaja = rTrn.fecOperacion
    '    Else
    '        fechaCaja = rTrn.fecOperacionAdmOrigen
    '    End If

    '    '1.- Montos calculados (lo que debió pagar)
    '    If rTrn.tipoProducto = "CCO" Then
    '        rTrn.valMlMvtoCal = Mat.Redondear(rTrn.valMlRentaImponible * rTrn.tasaCotizacion)
    '        If Not IsNothing(rMovAcr.codMvtoAdi) Then
    '            rTrn.valMlAdicionalCal = Mat.Redondear(rTrn.valMlRentaImponible * rTrn.tasaAdicional)
    '            rTrn.codMvtoAdi = rMovAcr.codMvtoAdi
    '        End If
    '    Else
    '        rTrn.valMlMvtoCal = valMlMontoPorDistribuir
    '    End If

    '    If rTrn.tipoPago = 2 Then

    '        If Not IsNothing(rMovAcr.codMvtoIntreaCue) Then
    '            rTrn.valMlInteresCal = Sys.Servicios.CSer_Clientes.calculoInteres(gdbc,gidAdm, rTrn.perCotizacion, fechaCaja, rTrn.valMlMvtoCal)
    '            rTrn.valMlReajusteCal = Sys.Servicios.CSer_Clientes.calculoReajuste(gdbc,gidAdm, rTrn.perCotizacion, fechaCaja, rTrn.valMlMvtoCal)
    '            rTrn.codMvtoIntreaCue = rMovAcr.codMvtoIntreaCue
    '        End If
    '        If Not IsNothing(rMovAcr.codMvtoIntreaAdi) Then
    '            rTrn.valMlAdicionalInteresCal = Sys.Servicios.CSer_Clientes.calculoInteres(gdbc,gidAdm, rTrn.perCotizacion, fechaCaja, rTrn.valMlAdicionalCal)
    '            rTrn.valMlAdicionalReajusteCal = Sys.Servicios.CSer_Clientes.calculoReajuste(gdbc,gidAdm, rTrn.perCotizacion, fechaCaja, rTrn.valMlAdicionalCal)
    '            rTrn.codMvtoIntreaAdi = rMovAcr.codMvtoIntreaAdi
    '        End If

    '    End If
    '    '2.- Distribucion de lo pagado

    '    'Fondo
    '    If valMlMontoPorDistribuir > 0 And rTrn.valMlMvtoCal > 0 Then
    '        If rTrn.valMlMvtoCal > valMlMontoPorDistribuir Then
    '            rTrn.valMlMvtoCal = valMlMontoPorDistribuir
    '        End If
    '    End If
    '    valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlMvtoCal
    '    'Interes
    '    If valMlMontoPorDistribuir > 0 And rTrn.valMlInteresCal > 0 Then
    '        If rTrn.valMlInteresCal > valMlMontoPorDistribuir Then
    '            rTrn.valMlInteresCal = valMlMontoPorDistribuir
    '        End If
    '    Else
    '        rTrn.valMlInteresCal = 0
    '    End If
    '    valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlInteresCal
    '    'Reajustes
    '    If valMlMontoPorDistribuir > 0 And rTrn.valMlReajusteCal > 0 Then
    '        If rTrn.valMlReajusteCal > valMlMontoPorDistribuir Then
    '            rTrn.valMlReajusteCal = valMlMontoPorDistribuir
    '        End If
    '        valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlReajusteCal
    '    Else
    '        rTrn.valMlReajusteCal = 0
    '    End If
    '    'Adicional
    '    If valMlMontoPorDistribuir > 0 And rTrn.valMlAdicionalCal > 0 Then
    '        If rTrn.valMlAdicionalCal > valMlMontoPorDistribuir Then
    '            rTrn.valMlAdicionalCal = valMlMontoPorDistribuir
    '        End If
    '    Else
    '        rTrn.valMlAdicionalCal = 0
    '    End If
    '    valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlAdicionalCal
    '    'Interes Adicional
    '    If valMlMontoPorDistribuir > 0 And rTrn.valMlAdicionalInteresCal > 0 Then
    '        If rTrn.valMlAdicionalInteresCal > valMlMontoPorDistribuir Then
    '            rTrn.valMlAdicionalInteresCal = valMlMontoPorDistribuir
    '        End If
    '    Else
    '        rTrn.valMlAdicionalInteresCal = 0
    '    End If
    '    valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlAdicionalInteresCal
    '    'Reajustes Adicional
    '    If valMlMontoPorDistribuir > 0 And rTrn.valMlAdicionalReajusteCal > 0 Then
    '        If rTrn.valMlAdicionalReajusteCal > valMlMontoPorDistribuir Then
    '            rTrn.valMlAdicionalReajusteCal = valMlMontoPorDistribuir
    '        End If
    '    Else
    '        rTrn.valMlAdicionalReajusteCal = 0
    '    End If
    '    valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlAdicionalReajusteCal

    '    'Exceso
    '    If valMlMontoPorDistribuir > 0 Then
    '        rTrn.valMlExcesoTopeCal = valMlMontoPorDistribuir
    '        'tope para el tipo de producto
    '        dsAux = parParametrosAcred.traer(gdbc,New Object() {"VID_ADM", "VTIPO_PRODUCTO"}, New Object() {gidAdm, rTrn.tipoProducto}, New Object() {"INTEGER", "STRING"})

    '        If dsAux.Tables(0).Rows.Count > 0 Then
    '            rTope = New ccAcrParametrosAcred(dsAux)
    '            If rTrn.valMlExcesoTopeCal < Mat.Redondear(rTope.valUfLimiteExceso * gvalorUF) Then
    '                rTrn.valMlMvtoCal += rTrn.valMlExcesoTopeCal
    '                rTrn.valMlExcesoTopeCal = 0
    '            End If
    '        End If
    '    End If

    '    '3.- Conversión a cuotas

    '    cuotasFondo = Mat.Redondear((rTrn.valMlMvtoCal + rTrn.valMlInteresCal + rTrn.valMlReajusteCal) / rTrn.valMlValorCuota, 2)
    '    cuotasAdicional = Mat.Redondear((rTrn.valMlAdicionalCal + rTrn.valMlAdicionalInteresCal + rTrn.valMlAdicionalReajusteCal) / rTrn.valMlValorCuota, 2)
    '    If fechaCaja < fecRezagoAntiguo Then
    '        cuotasAdicional = 0
    '    End If
    '    'Fondo
    '    rTrn.valCuoMvtoCal = Mat.Redondear(rTrn.valMlMvtoCal / rTrn.valMlValorCuota, 2)
    '    rTrn.valCuoInteresCal = Mat.Redondear(rTrn.valMlInteresCal / rTrn.valMlValorCuota, 2)
    '    rTrn.valCuoReajusteCal = Mat.Redondear(rTrn.valMlReajusteCal / rTrn.valMlValorCuota, 2)

    '    If cuotasFondo <> rTrn.valCuoMvtoCal + rTrn.valCuoInteresCal + rTrn.valCuoReajusteCal Then
    '        If rTrn.valCuoInteresCal > 0 Then
    '            rTrn.valCuoInteresCal += cuotasFondo - (rTrn.valCuoMvtoCal + rTrn.valCuoInteresCal + rTrn.valCuoReajusteCal)
    '        Else
    '            rTrn.valCuoReajusteCal += cuotasFondo - (rTrn.valCuoMvtoCal + rTrn.valCuoInteresCal + rTrn.valCuoReajusteCal)
    '        End If
    '    End If
    '    'Adicional
    '    If cuotasAdicional > 0 Then
    '        rTrn.valCuoAdicionalCal = Mat.Redondear(rTrn.valMlAdicionalCal / rTrn.valMlValorCuota, 2)
    '        rTrn.valCuoAdicionalInteresCal = Mat.Redondear(rTrn.valMlAdicionalInteresCal / rTrn.valMlValorCuota, 2)
    '        rTrn.valCuoAdicionalReajusteCal = Mat.Redondear(rTrn.valMlAdicionalReajusteCal / rTrn.valMlValorCuota, 2)
    '    End If
    '    If cuotasAdicional <> rTrn.valCuoAdicionalCal + rTrn.valCuoAdicionalInteresCal + rTrn.valCuoAdicionalReajusteCal Then
    '        If rTrn.valCuoInteresCal > 0 Then
    '            rTrn.valCuoAdicionalInteresCal += cuotasAdicional - (rTrn.valCuoAdicionalCal + rTrn.valCuoAdicionalInteresCal + rTrn.valCuoAdicionalReajusteCal)
    '        Else
    '            rTrn.valCuoAdicionalReajusteCal += cuotasAdicional - (rTrn.valCuoAdicionalCal + rTrn.valCuoAdicionalInteresCal + rTrn.valCuoAdicionalReajusteCal)
    '        End If
    '    End If
    '    'Exceso
    '    rTrn.valCuoExcesoTopeCal = Mat.Redondear(rTrn.valMlExcesoTopeCal / rTrn.valMlValorCuota, 2)
    '    'Ganancia o Perdida
    '    rTrn.valCuoCompensCal = Mat.Redondear(rTrn.valMlCompensCal / rTrn.valMlValorCuota, 2)
    '    'Ajuste decimal obligatorio
    '    rTrn.valCuoAjusteDecimalCal = rTrn.valCuoPatrFdesCal - (cuotasFondo + rTrn.valCuoExcesoTopeCal + rTrn.valCuoCompensCal + cuotasAdicional)

    'End Sub

    Public Sub AperturaMontosAbonos(ByVal valMlNominal As Decimal, _
                                    ByVal valMlTraspasado As Decimal, _
                                    ByVal rMovAcr As ccAcrMvtoAcreditacion, _
                                    ByRef rTrn As ccTransacciones)


        Dim valMlMontoPorDistribuir As Decimal
        Dim fecRezagoAntiguo As Date = New Date(1988, 1, 1).Date
        Dim rTope As ccAcrParametrosAcred
        Dim cuotasFondo As Decimal
        Dim cuotasAdicional As Decimal
        Dim fechaCaja As Date
        Dim tasaCotizacion As Decimal = 0
        Dim valMlTotNominal As Decimal = 0

        Dim blBonoPorHijo As Boolean = False ' lfc: 07-08-2018 110197-LIQUIDACION BONO POR HIJO, sin calculo de rentaImponible

        If rTrn.codMvto = "110197" Then
            blBonoPorHijo = True
        End If

        'Se agrega condicion para TRAIPAGN
        valMlTotNominal = rTrn.valMlPatrFrecCal '- rTrn.valMlTransferenciaCal
        If rTrn.codOrigenProceso = "TRAIPAGN" Or rTrn.codOrigenProceso = "TRAINREZ" Then
            If rTrn.tipoFondoDestinoCal = "C" Then
                rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
                valMlMontoPorDistribuir = valMlTotNominal
            Else
                'rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - rTrn.valMlMontoNominal
                'rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal

                'rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlNominal
                'valMlMontoPorDistribuir = valMlNominal

                ''Ultimo(PCI)
                'rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
                'valMlMontoPorDistribuir = valMlTotNominal

                If gcodAdministradora = 1034 Or gcodAdministradora = 1035 Then
                    rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
                    valMlMontoPorDistribuir = valMlTotNominal
                Else
                    rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlNominal
                    valMlMontoPorDistribuir = valMlNominal

                    'rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
                    'valMlMontoPorDistribuir = valMlTotNominal
                End If
            End If
        Else
            If gcodAdministradora = 1034 Or gcodAdministradora = 1035 Then
                rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
                valMlMontoPorDistribuir = valMlTotNominal
            Else
                'rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
                'valMlMontoPorDistribuir = valMlTotNominal

                'CAMBIADO PARA PRUEBA 21/10/2011 LUIS
                rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlNominal
                valMlMontoPorDistribuir = valMlNominal
            End If


            ''rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - rTrn.valMlPatrFrecCal
            ''Se modifican las siguientes dos lineas a pedido de MODELO.
            ''rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlNominal
            ''valMlMontoPorDistribuir = valMlNominal

            'Ultimo PCI
            'rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
            'valMlMontoPorDistribuir = valMlTotNominal
        End If

        'valMlMontoPorDistribuir = valMlNominal

        rTrn.valMlMvtoCal = 0
        rTrn.valMlInteresCal = 0
        rTrn.valMlReajusteCal = 0
        rTrn.valMlAdicionalCal = 0
        rTrn.valMlAdicionalInteresCal = 0
        rTrn.valMlAdicionalReajusteCal = 0

        If rTrn.codOrigenMvto = "RECAUDAC" Then
            fechaCaja = rTrn.fecOperacion
        Else
            fechaCaja = rTrn.fecOperacionAdmOrigen
        End If

        '1.- Montos calculados (lo que debió pagar)
        If rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF" Then  'LFC://PLI-20090802

            If blBonoPorHijo Then
                rTrn.valMlMvtoCal = valMlMontoPorDistribuir
            Else

                tasaCotizacion = rTrn.tasaCotizacion
                If rMovAcr.tipoMvto = "COP" Then
                    tasaCotizacion = Mat.Redondear(rMovAcr.tasaTrabajoPesado / 100, 2)
                End If
                rTrn.valMlMvtoCal = Mat.Redondear(rTrn.valMlRentaImponible * tasaCotizacion)

                If Not IsNothing(rMovAcr.codMvtoAdi) Then
                    rTrn.valMlAdicionalCal = Mat.Redondear(rTrn.valMlRentaImponible * rTrn.tasaAdicional)

                    If blAdicionalAntiguo Then 'Se excluye Adicional. OS-7079391. OS-7243919 01/04/2016
                        rTrn.valMlAdicionalCal = 0
                    Else
                        rTrn.valMlAdicionalCal = Mat.Redondear(rTrn.valMlRentaImponible * rTrn.tasaAdicional)
                    End If

                    'rTrn.valMlAdicionalCal = Mat.Redondear(rTrn.valMlRentaImponible * rTrn.tasaAdicional)
                    rTrn.codMvtoAdi = rMovAcr.codMvtoAdi
                End If



                '***********************************************************************************
                If fechaCaja < fecRezagoAntiguo And fechaCaja >= New Date(1981, 5, 1) Then  ' no aperturar la cotizacion 
                    rTrn.valMlMvtoCal = valMlMontoPorDistribuir
                End If
                '***********************************************************************************
            End If
        Else
            rTrn.valMlMvtoCal = valMlMontoPorDistribuir
        End If

        If rTrn.tipoPago = 2 Then

            If Not IsNothing(rMovAcr.codMvtoIntreaCue) Then
                rTrn.valMlInteresCal = Sys.Servicios.CSer_Clientes.calculoInteres(gdbc, gidAdm, rTrn.perCotizacion, fechaCaja, rTrn.valMlMvtoCal)
                rTrn.valMlReajusteCal = Sys.Servicios.CSer_Clientes.calculoReajuste(gdbc, gidAdm, rTrn.perCotizacion, fechaCaja, rTrn.valMlMvtoCal)
                rTrn.codMvtoIntreaCue = rMovAcr.codMvtoIntreaCue
            End If
            If Not IsNothing(rMovAcr.codMvtoIntreaAdi) Then
                rTrn.valMlAdicionalInteresCal = Sys.Servicios.CSer_Clientes.calculoInteres(gdbc, gidAdm, rTrn.perCotizacion, fechaCaja, rTrn.valMlAdicionalCal)
                rTrn.valMlAdicionalReajusteCal = Sys.Servicios.CSer_Clientes.calculoReajuste(gdbc, gidAdm, rTrn.perCotizacion, fechaCaja, rTrn.valMlAdicionalCal)
                rTrn.codMvtoIntreaAdi = rMovAcr.codMvtoIntreaAdi
            End If

        End If

        '2.- Distribucion de lo pagado

        'Fondo
        If valMlMontoPorDistribuir > 0 And rTrn.valMlMvtoCal > 0 Then
            If rTrn.valMlMvtoCal > valMlMontoPorDistribuir Then
                rTrn.valMlMvtoCal = valMlMontoPorDistribuir
            End If
        Else
            rTrn.valMlMvtoCal = 0
        End If
        valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlMvtoCal

        'Interes
        If valMlMontoPorDistribuir > 0 And rTrn.valMlInteresCal > 0 Then
            If rTrn.valMlInteresCal > valMlMontoPorDistribuir Then
                rTrn.valMlInteresCal = valMlMontoPorDistribuir
            End If
        Else
            rTrn.valMlInteresCal = 0
        End If
        valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlInteresCal

        'Reajustes
        If valMlMontoPorDistribuir > 0 And rTrn.valMlReajusteCal > 0 Then
            If rTrn.valMlReajusteCal > valMlMontoPorDistribuir Then
                rTrn.valMlReajusteCal = valMlMontoPorDistribuir
            End If
            valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlReajusteCal
        Else
            rTrn.valMlReajusteCal = 0
        End If

        'Adicional
        If valMlMontoPorDistribuir > 0 And rTrn.valMlAdicionalCal > 0 Then
            If rTrn.valMlAdicionalCal > valMlMontoPorDistribuir Then
                rTrn.valMlAdicionalCal = valMlMontoPorDistribuir
            End If
        Else
            rTrn.valMlAdicionalCal = 0
        End If
        valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlAdicionalCal
        'Interes Adicional
        If valMlMontoPorDistribuir > 0 And rTrn.valMlAdicionalInteresCal > 0 Then
            If rTrn.valMlAdicionalInteresCal > valMlMontoPorDistribuir Then
                rTrn.valMlAdicionalInteresCal = valMlMontoPorDistribuir
            End If
        Else
            rTrn.valMlAdicionalInteresCal = 0
        End If
        valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlAdicionalInteresCal
        'Reajustes Adicional
        If valMlMontoPorDistribuir > 0 And rTrn.valMlAdicionalReajusteCal > 0 Then
            If rTrn.valMlAdicionalReajusteCal > valMlMontoPorDistribuir Then
                rTrn.valMlAdicionalReajusteCal = valMlMontoPorDistribuir
            End If
        Else
            rTrn.valMlAdicionalReajusteCal = 0
        End If
        valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlAdicionalReajusteCal

        If rTrn.valMlInteresCal < 0 Or rTrn.valMlReajusteCal < 0 Or rTrn.valMlAdicionalInteresCal < 0 Or _
           rTrn.valMlAdicionalReajusteCal < 0 Then
            GenerarLog("E", 0, "Hebra " & gIdHebra & " - Interes o Reajuste Negativo ", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            blIgnorar = True
        End If

        'Exceso
        If valMlMontoPorDistribuir > 0 Then
            rTrn.valMlExcesoTopeCal = valMlMontoPorDistribuir
            'tope para el tipo de producto
            'dsAux = parParametrosAcred.traer(dbc, New Object() {"VID_ADM", "VTIPO_PRODUCTO"}, New Object() {gidAdm, rTrn.tipoProducto}, New Object() {"INTEGER", "STRING"})

            dsAux = Sys.IngresoEgreso.Rentas.ControlRentas.traerRentaTopeUf(gdbc, gidAdm, rTrn.tipoProducto, rTrn.perCotizacion)

            If dsAux.Tables(0).Rows.Count > 0 Then
                rTope = New ccAcrParametrosAcred(dsAux)
                If rTrn.valMlExcesoTopeCal < Mat.Redondear(rTope.valUfLimiteExceso * gvalorUF) Then
                    rTrn.valMlMvtoCal += rTrn.valMlExcesoTopeCal
                    rTrn.valMlExcesoTopeCal = 0
                End If
            End If
        End If

        '3.- Conversión a cuotas

        cuotasFondo = Mat.Redondear((rTrn.valMlMvtoCal + rTrn.valMlInteresCal + rTrn.valMlReajusteCal) / rTrn.valMlValorCuota, 2)
        cuotasAdicional = Mat.Redondear((rTrn.valMlAdicionalCal + rTrn.valMlAdicionalInteresCal + rTrn.valMlAdicionalReajusteCal) / rTrn.valMlValorCuota, 2)


        If fechaCaja < fecRezagoAntiguo And fechaCaja >= New Date(1981, 5, 1) Then
            cuotasAdicional = 0
        End If


        'Fondo
        rTrn.valCuoMvtoCal = Mat.Redondear(rTrn.valMlMvtoCal / rTrn.valMlValorCuota, 2)
        rTrn.valCuoInteresCal = Mat.Redondear(rTrn.valMlInteresCal / rTrn.valMlValorCuota, 2)
        rTrn.valCuoReajusteCal = Mat.Redondear(rTrn.valMlReajusteCal / rTrn.valMlValorCuota, 2)

        If cuotasFondo <> rTrn.valCuoMvtoCal + rTrn.valCuoInteresCal + rTrn.valCuoReajusteCal Then
            If rTrn.valCuoInteresCal > 0 Then
                rTrn.valCuoInteresCal += cuotasFondo - (rTrn.valCuoMvtoCal + rTrn.valCuoInteresCal + rTrn.valCuoReajusteCal)
            Else
                rTrn.valCuoReajusteCal += cuotasFondo - (rTrn.valCuoMvtoCal + rTrn.valCuoInteresCal + rTrn.valCuoReajusteCal)
            End If
        End If
        'Adicional
        If cuotasAdicional > 0 Then
            rTrn.valCuoAdicionalCal = Mat.Redondear(rTrn.valMlAdicionalCal / rTrn.valMlValorCuota, 2)
            rTrn.valCuoAdicionalInteresCal = Mat.Redondear(rTrn.valMlAdicionalInteresCal / rTrn.valMlValorCuota, 2)
            rTrn.valCuoAdicionalReajusteCal = Mat.Redondear(rTrn.valMlAdicionalReajusteCal / rTrn.valMlValorCuota, 2)
        End If
        If cuotasAdicional <> rTrn.valCuoAdicionalCal + rTrn.valCuoAdicionalInteresCal + rTrn.valCuoAdicionalReajusteCal Then
            If rTrn.valCuoInteresCal > 0 Then
                rTrn.valCuoAdicionalInteresCal += cuotasAdicional - (rTrn.valCuoAdicionalCal + rTrn.valCuoAdicionalInteresCal + rTrn.valCuoAdicionalReajusteCal)
            Else
                rTrn.valCuoAdicionalReajusteCal += cuotasAdicional - (rTrn.valCuoAdicionalCal + rTrn.valCuoAdicionalInteresCal + rTrn.valCuoAdicionalReajusteCal)
            End If
        End If
        'Exceso
        rTrn.valCuoExcesoTopeCal = Mat.Redondear(rTrn.valMlExcesoTopeCal / rTrn.valMlValorCuota, 2)
        'Ganancia o Perdida
        rTrn.valCuoCompensCal = Mat.Redondear(rTrn.valMlCompensCal / rTrn.valMlValorCuota, 2)
        'Ajuste decimal obligatorio

        'Verifica Cuotas de Compensacion menor a 0,01 cuotas pero ML > 0
        'Dim ValCuoCompen As Decimal = 0
        'If rTrn.valMlCompensCal > 0 And rTrn.valCuoCompensCal = 0 Then
        '    ValCuoCompen = rTrn.valMlCompensCal / rTrn.valMlValorCuota
        'Else
        '    ValCuoCompen = rTrn.valCuoCompensCal
        'End If

        'rTrn.valCuoAjusteDecimalCal = rTrn.valCuoPatrFdesCal - (cuotasFondo + rTrn.valCuoExcesoTopeCal + ValCuoCompen + cuotasAdicional)
        rTrn.valCuoAjusteDecimalCal = rTrn.valCuoPatrFdesCal - (cuotasFondo + rTrn.valCuoExcesoTopeCal + rTrn.valCuoCompensCal + cuotasAdicional)

        If Math.Abs(rTrn.valCuoAjusteDecimalCal) > 0.02 Then
            GenerarLog("E", 0, "Hebra " & gIdHebra & " - El rezago está descuadrado(" & Str(rTrn.valCuoAjusteDecimalCal) & ")", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            'GenerarLog("E", 0, "El rezago está descuadrado(" & Str(rTrn.valCuoAjusteDecimalCal) & ")", rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            blIgnorar = True
        End If
        'PCI()

        ''Se vuelve atras AJUSTE DECIMAL. 09/11/2010 PCI
        'Me.ajustesDecimalAComisionPorc()

        'rTrn.valCuoAdicionalCal = rTrn.valCuoAdicionalCal - g_valCuoAjusteDec
        g_valCuoAjusteDec = 0

    End Sub


    Public Sub AperturaMontosAbonos2(ByVal valMlNominal As Decimal, _
                                    ByVal valMlTraspasado As Decimal, _
                                    ByVal rMovAcr As ccAcrMvtoAcreditacion, _
                                    ByRef rTrn As ccTransacciones)

        Dim valDif As Decimal
        Dim valDifUni As Decimal

        Dim valMlMontoPorDistribuir As Decimal
        Dim fecRezagoAntiguo As Date = New Date(1988, 1, 1).Date
        Dim rTope As ccAcrParametrosAcred
        Dim cuotasFondo As Decimal
        Dim cuotasAdicional As Decimal
        Dim fechaCaja As Date
        Dim tasaCotizacion As Decimal = 0
        Dim valMlTotNominal As Decimal = 0

        'Se agrega condicion para TRAIPAGN
        valMlTotNominal = rTrn.valMlPatrFrecCal '- rTrn.valMlTransferenciaCal
        If rTrn.codOrigenMvto = "TRAIPAGN" Or rTrn.codOrigenMvto = "TRAINREZ" Then
            If rTrn.tipoFondoDestinoCal = "C" Then
                rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
                valMlMontoPorDistribuir = valMlTotNominal
            Else
                'rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - rTrn.valMlMontoNominal
                'rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal

                If gcodAdministradora = 1034 Or gcodAdministradora = 1035 Then
                    rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
                    valMlMontoPorDistribuir = valMlTotNominal
                Else
                    rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlNominal
                    valMlMontoPorDistribuir = valMlNominal
                End If
                'rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
                'valMlMontoPorDistribuir = valMlTotNominal

            End If
        Else
            'rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - rTrn.valMlPatrFrecCal

            'Se modifican las siguientes dos lineas a pedido de MODELO.
            'rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlNominal
            'valMlMontoPorDistribuir = valMlNominal
            rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlTotNominal
            valMlMontoPorDistribuir = valMlTotNominal
        End If

        'valMlMontoPorDistribuir = valMlNominal

        rTrn.valMlMvtoCal = 0
        rTrn.valMlInteresCal = 0
        rTrn.valMlReajusteCal = 0
        rTrn.valMlAdicionalCal = 0
        rTrn.valMlAdicionalInteresCal = 0
        rTrn.valMlAdicionalReajusteCal = 0

        If rTrn.codOrigenMvto = "RECAUDAC" Then
            fechaCaja = rTrn.fecOperacion
        Else
            fechaCaja = rTrn.fecOperacionAdmOrigen
        End If

        '1.- Montos calculados (lo que debió pagar)
        If rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF" Then  'LFC://PLI-2009080142

            tasaCotizacion = rTrn.tasaCotizacion
            If rMovAcr.tipoMvto = "COP" Then
                tasaCotizacion = Mat.Redondear(rMovAcr.tasaTrabajoPesado / 100, 2)
            End If
            rTrn.valMlMvtoCal = Mat.Redondear(rTrn.valMlRentaImponible * tasaCotizacion)

            If Not IsNothing(rMovAcr.codMvtoAdi) Then
                If blAdicionalAntiguo Then 'Se excluye Adicional. OS-7079391. OS-7243919 01/04/2016
                    rTrn.valMlAdicionalCal = 0
                Else
                    rTrn.valMlAdicionalCal = Mat.Redondear(rTrn.valMlRentaImponible * rTrn.tasaAdicional)
                End If

                'rTrn.valMlAdicionalCal = Mat.Redondear(rTrn.valMlRentaImponible * rTrn.tasaAdicional)
                rTrn.codMvtoAdi = rMovAcr.codMvtoAdi
            End If

            '***********************************************************************************
            If fechaCaja < fecRezagoAntiguo And fechaCaja >= New Date(1981, 5, 1) Then ' no aperturar la cotizacion 
                rTrn.valMlMvtoCal = valMlMontoPorDistribuir
            End If
            '***********************************************************************************

        Else
            rTrn.valMlMvtoCal = valMlMontoPorDistribuir
        End If

        If rTrn.tipoPago = 2 Then

            If Not IsNothing(rMovAcr.codMvtoIntreaCue) Then
                rTrn.valMlInteresCal = Sys.Servicios.CSer_Clientes.calculoInteres(gdbc, gidAdm, rTrn.perCotizacion, fechaCaja, rTrn.valMlMvtoCal)
                rTrn.valMlReajusteCal = Sys.Servicios.CSer_Clientes.calculoReajuste(gdbc, gidAdm, rTrn.perCotizacion, fechaCaja, rTrn.valMlMvtoCal)
                rTrn.codMvtoIntreaCue = rMovAcr.codMvtoIntreaCue
            End If
            If Not IsNothing(rMovAcr.codMvtoIntreaAdi) Then
                rTrn.valMlAdicionalInteresCal = Sys.Servicios.CSer_Clientes.calculoInteres(gdbc, gidAdm, rTrn.perCotizacion, fechaCaja, rTrn.valMlAdicionalCal)
                rTrn.valMlAdicionalReajusteCal = Sys.Servicios.CSer_Clientes.calculoReajuste(gdbc, gidAdm, rTrn.perCotizacion, fechaCaja, rTrn.valMlAdicionalCal)
                rTrn.codMvtoIntreaAdi = rMovAcr.codMvtoIntreaAdi
            End If

        End If

        '2.- Distribucion de lo pagado

        'Fondo
        If valMlMontoPorDistribuir > 0 And rTrn.valMlMvtoCal > 0 Then
            If rTrn.valMlMvtoCal > valMlMontoPorDistribuir Then
                rTrn.valMlMvtoCal = valMlMontoPorDistribuir
            End If
        Else
            rTrn.valMlMvtoCal = 0
        End If
        valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlMvtoCal

        'Interes
        If valMlMontoPorDistribuir > 0 And rTrn.valMlInteresCal > 0 Then
            If rTrn.valMlInteresCal > valMlMontoPorDistribuir Then
                rTrn.valMlInteresCal = valMlMontoPorDistribuir
            End If
        Else
            rTrn.valMlInteresCal = 0
        End If
        valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlInteresCal

        'Reajustes
        If valMlMontoPorDistribuir > 0 And rTrn.valMlReajusteCal > 0 Then
            If rTrn.valMlReajusteCal > valMlMontoPorDistribuir Then
                rTrn.valMlReajusteCal = valMlMontoPorDistribuir
            End If
            valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlReajusteCal
        Else
            rTrn.valMlReajusteCal = 0
        End If

        'Adicional
        If valMlMontoPorDistribuir > 0 And rTrn.valMlAdicionalCal > 0 Then
            If rTrn.valMlAdicionalCal > valMlMontoPorDistribuir Then
                rTrn.valMlAdicionalCal = valMlMontoPorDistribuir
            End If
        Else
            rTrn.valMlAdicionalCal = 0
        End If
        valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlAdicionalCal
        'Interes Adicional
        If valMlMontoPorDistribuir > 0 And rTrn.valMlAdicionalInteresCal > 0 Then
            If rTrn.valMlAdicionalInteresCal > valMlMontoPorDistribuir Then
                rTrn.valMlAdicionalInteresCal = valMlMontoPorDistribuir
            End If
        Else
            rTrn.valMlAdicionalInteresCal = 0
        End If
        valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlAdicionalInteresCal
        'Reajustes Adicional
        If valMlMontoPorDistribuir > 0 And rTrn.valMlAdicionalReajusteCal > 0 Then
            If rTrn.valMlAdicionalReajusteCal > valMlMontoPorDistribuir Then
                rTrn.valMlAdicionalReajusteCal = valMlMontoPorDistribuir
            End If
        Else
            rTrn.valMlAdicionalReajusteCal = 0
        End If
        valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlAdicionalReajusteCal

        If rTrn.valMlInteresCal < 0 Or rTrn.valMlReajusteCal < 0 Or rTrn.valMlAdicionalInteresCal < 0 Or _
           rTrn.valMlAdicionalReajusteCal < 0 Then
            GenerarLog("E", 0, "Hebra " & gIdHebra & " - Interes o Reajuste Negativo ", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
            blIgnorar = True
        End If

        'Exceso
        If valMlMontoPorDistribuir > 0 Then
            rTrn.valMlExcesoTopeCal = valMlMontoPorDistribuir
            'tope para el tipo de producto
            'dsAux = parParametrosAcred.traer(gdbc,New Object() {"VID_ADM", "VTIPO_PRODUCTO"}, New Object() {gidAdm, rTrn.tipoProducto}, New Object() {"INTEGER", "STRING"})
            dsAux = Sys.IngresoEgreso.Rentas.ControlRentas.traerRentaTopeUf(gdbc, gidAdm, rTrn.tipoProducto, rTrn.perCotizacion)

            If dsAux.Tables(0).Rows.Count > 0 Then
                rTope = New ccAcrParametrosAcred(dsAux)
                If rTrn.valMlExcesoTopeCal < Mat.Redondear(rTope.valUfLimiteExceso * gvalorUF) Then
                    rTrn.valMlMvtoCal += rTrn.valMlExcesoTopeCal
                    rTrn.valMlExcesoTopeCal = 0
                End If
            End If
        End If

        '3.- Conversión a cuotas
        cuotasFondo = ((rTrn.valMlMvtoCal + rTrn.valMlInteresCal + rTrn.valMlReajusteCal) / rTrn.valMlValorCuota)
        cuotasAdicional = ((rTrn.valMlAdicionalCal + rTrn.valMlAdicionalInteresCal + rTrn.valMlAdicionalReajusteCal) / rTrn.valMlValorCuota)

        If fechaCaja < fecRezagoAntiguo And fechaCaja >= New Date(1981, 5, 1) Then
            cuotasAdicional = 0
        End If

        'Fondo
        rTrn.valCuoMvtoCal = (rTrn.valMlMvtoCal / rTrn.valMlValorCuota)
        rTrn.valCuoInteresCal = (rTrn.valMlInteresCal / rTrn.valMlValorCuota)
        rTrn.valCuoReajusteCal = (rTrn.valMlReajusteCal / rTrn.valMlValorCuota)

        If cuotasFondo <> rTrn.valCuoMvtoCal + rTrn.valCuoInteresCal + rTrn.valCuoReajusteCal Then
            If rTrn.valCuoInteresCal > 0 Then
                rTrn.valCuoInteresCal += cuotasFondo - (rTrn.valCuoMvtoCal + rTrn.valCuoInteresCal + rTrn.valCuoReajusteCal)
            Else
                rTrn.valCuoReajusteCal += cuotasFondo - (rTrn.valCuoMvtoCal + rTrn.valCuoInteresCal + rTrn.valCuoReajusteCal)
            End If
        End If
        'Adicional
        If cuotasAdicional > 0 Then
            rTrn.valCuoAdicionalCal = (rTrn.valMlAdicionalCal / rTrn.valMlValorCuota)
            rTrn.valCuoAdicionalInteresCal = (rTrn.valMlAdicionalInteresCal / rTrn.valMlValorCuota)
            rTrn.valCuoAdicionalReajusteCal = (rTrn.valMlAdicionalReajusteCal / rTrn.valMlValorCuota)
        End If
        If cuotasAdicional <> rTrn.valCuoAdicionalCal + rTrn.valCuoAdicionalInteresCal + rTrn.valCuoAdicionalReajusteCal Then
            If rTrn.valCuoInteresCal > 0 Then
                rTrn.valCuoAdicionalInteresCal += cuotasAdicional - (rTrn.valCuoAdicionalCal + rTrn.valCuoAdicionalInteresCal + rTrn.valCuoAdicionalReajusteCal)
            Else
                rTrn.valCuoAdicionalReajusteCal += cuotasAdicional - (rTrn.valCuoAdicionalCal + rTrn.valCuoAdicionalInteresCal + rTrn.valCuoAdicionalReajusteCal)
            End If
        End If
        'Exceso
        rTrn.valCuoExcesoTopeCal = (rTrn.valMlExcesoTopeCal / rTrn.valMlValorCuota)
        'Ganancia o Perdida
        rTrn.valCuoCompensCal = (rTrn.valMlCompensCal / rTrn.valMlValorCuota)

        'Ajuste decimal obligatorio

        'Verifica Cuotas de Compensacion menor a 0,01 cuotas pero ML > 0
        'Dim ValCuoCompen As Decimal = 0
        'If rTrn.valMlCompensCal > 0 And rTrn.valCuoCompensCal = 0 Then
        '    ValCuoCompen = rTrn.valMlCompensCal / rTrn.valMlValorCuota
        'Else
        '    ValCuoCompen = rTrn.valCuoCompensCal
        'End If




        '--------------------------------------------------------------------------------------------------------+  sis
        'Verifica Diferencias entre Valores Individuales y Acumulados ya que tambien genera Ajuste Decimal
        valDifUni = Mat.Redondear(cuotasFondo, 2) - (Mat.Redondear(rTrn.valCuoMvtoCal, 2) + Mat.Redondear(rTrn.valCuoInteresCal, 2) + Mat.Redondear(rTrn.valCuoReajusteCal, 2))
        If valDifUni > 0 Then
            If rTrn.valMlInteresCal > 0 Then
                rTrn.valCuoInteresCal += valDifUni
            ElseIf rTrn.valMlReajusteCal > 0 Then
                rTrn.valCuoReajusteCal += valDifUni
            End If
        End If

        valDifUni = Mat.Redondear(cuotasAdicional, 2) - (Mat.Redondear(rTrn.valCuoAdicionalCal, 2) + Mat.Redondear(rTrn.valCuoAdicionalInteresCal, 2) + Mat.Redondear(rTrn.valCuoAdicionalReajusteCal, 2))
        If valDifUni > 0 Then
            If rTrn.valMlAdicionalInteresCal > 0 Then
                rTrn.valCuoAdicionalInteresCal += valDifUni
            ElseIf rTrn.valMlAdicionalReajusteCal > 0 Then
                rTrn.valCuoAdicionalReajusteCal += valDifUni
            End If
        End If

        g_valCuoAjusteDec = 0
        If Me.gcodAdministradora = 1032 Then

            rTrn.valCuoMvtoCal = Mat.Redondear(rTrn.valCuoMvtoCal, 2)
            rTrn.valCuoInteresCal = Mat.Redondear(rTrn.valCuoInteresCal, 2)
            rTrn.valCuoReajusteCal = Mat.Redondear(rTrn.valCuoReajusteCal, 2)
            rTrn.valCuoAdicionalCal = Mat.Redondear(rTrn.valCuoAdicionalCal, 2)
            rTrn.valCuoAdicionalInteresCal = Mat.Redondear(rTrn.valCuoAdicionalInteresCal, 2)
            rTrn.valCuoAdicionalReajusteCal = Mat.Redondear(rTrn.valCuoAdicionalReajusteCal, 2)
            rTrn.valCuoExcesoTopeCal = Mat.Redondear(rTrn.valCuoExcesoTopeCal, 2)
            rTrn.valCuoCompensCal = Mat.Redondear(rTrn.valCuoCompensCal, 2)

            rTrn.valCuoAjusteDecimalCal = rTrn.valCuoPatrFdesCal - (rTrn.valCuoMvtoCal + rTrn.valCuoInteresCal + rTrn.valCuoReajusteCal + rTrn.valCuoAdicionalCal + rTrn.valCuoAdicionalInteresCal + rTrn.valCuoAdicionalReajusteCal + rTrn.valCuoExcesoTopeCal + rTrn.valCuoCompensCal)

            If Math.Abs(rTrn.valCuoAjusteDecimalCal) > 0.02 Then
                GenerarLog("E", 0, "Hebra " & gIdHebra & " - El rezago está descuadrado(" & Str(rTrn.valCuoAjusteDecimalCal) & ")", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                blIgnorar = True
            End If


        Else
            rTrn.valCuoAjusteDecimalCal = rTrn.valCuoPatrFdesCal - (Mat.Redondear(rTrn.valCuoMvtoCal, 2) + Mat.Redondear(rTrn.valCuoInteresCal, 2) + Mat.Redondear(rTrn.valCuoReajusteCal, 2) + Mat.Redondear(rTrn.valCuoExcesoTopeCal, 2) + Mat.Redondear(rTrn.valCuoCompensCal, 2) + Mat.Redondear(rTrn.valCuoAdicionalCal, 2) + Mat.Redondear(rTrn.valCuoAdicionalInteresCal, 2) + Mat.Redondear(rTrn.valCuoAdicionalReajusteCal, 2))

            If Math.Abs(rTrn.valCuoAjusteDecimalCal) > 0.02 Then
                GenerarLog("E", 0, "Hebra " & gIdHebra & " - El rezago está descuadrado(" & Str(rTrn.valCuoAjusteDecimalCal) & ")", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                blIgnorar = True
            End If


            VerificaAjustesDecimal(rTrn.valCuoAjusteDecimalCal, rTrn.valCuoMvtoCal, rTrn.valCuoAdicionalCal, rTrn.valCuoAdicionalInteresCal, rTrn.valCuoAdicionalReajusteCal, rTrn.valCuoPrimaSisCal, rTrn.valCuoCompensCal)

            rTrn.valCuoAjusteDecimalCal = 0

            rTrn.valCuoMvtoCal = Mat.Redondear(rTrn.valCuoMvtoCal, 2)
            rTrn.valCuoInteresCal = Mat.Redondear(rTrn.valCuoInteresCal, 2)
            rTrn.valCuoReajusteCal = Mat.Redondear(rTrn.valCuoReajusteCal, 2)
            rTrn.valCuoAdicionalCal = Mat.Redondear(rTrn.valCuoAdicionalCal, 2)
            rTrn.valCuoAdicionalInteresCal = Mat.Redondear(rTrn.valCuoAdicionalInteresCal, 2)
            rTrn.valCuoAdicionalReajusteCal = Mat.Redondear(rTrn.valCuoAdicionalReajusteCal, 2)
            rTrn.valCuoExcesoTopeCal = Mat.Redondear(rTrn.valCuoExcesoTopeCal, 2)
            rTrn.valCuoCompensCal = Mat.Redondear(rTrn.valCuoCompensCal, 2)
            rTrn.valCuoAjusteDecimalCal = Mat.Redondear(rTrn.valCuoAjusteDecimalCal, 2)

        End If

    End Sub

    '''''Public Sub AperturaMontosAbonos2(ByVal valMlNominal As Decimal, _
    '''''                                ByVal valMlTraspasado As Decimal, _
    '''''                                ByVal rMovAcr As ccAcrMvtoAcreditacion, _
    '''''                                ByRef rTrn As ccTransacciones)


    '''''    Dim valMlMontoPorDistribuir As Decimal
    '''''    'Dim fecRezagoAntiguo As Date = New Date(1988, 1, 1).Date
    '''''    Dim rTope As ccAcrParametrosAcred
    '''''    Dim cuotasFondo As Decimal
    '''''    Dim cuotasAdicional As Decimal
    '''''    Dim fechaCaja As Date
    '''''    ' Dim tasaCotizacion As Decimal = 0

    '''''    rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - valMlNominal
    '''''    valMlMontoPorDistribuir = valMlNominal

    '''''    rTrn.valMlMvtoCal = 0
    '''''    rTrn.valMlInteresCal = 0
    '''''    rTrn.valMlReajusteCal = 0
    '''''    rTrn.valMlAdicionalCal = 0
    '''''    rTrn.valMlAdicionalInteresCal = 0
    '''''    rTrn.valMlAdicionalReajusteCal = 0

    '''''    If rTrn.codOrigenMvto = "RECAUDAC" Then
    '''''        fechaCaja = rTrn.fecOperacion
    '''''    Else
    '''''        fechaCaja = rTrn.fecOperacionAdmOrigen
    '''''    End If



    '''''    '1.- Montos calculados (lo que debió pagar)
    '''''    If (rTrn.tipoProducto = "CCO") Or (rTrn.tipoProducto = "CAF") Then



    '''''        'tasaCotizacion = rTrn.tasaCotizacion
    '''''        'If rMovAcr.tipoMvto = "COP" Then
    '''''        '    tasaCotizacion = Mat.Redondear(rMovAcr.tasaTrabajoPesado / 100, 2)
    '''''        'End If
    '''''        'rTrn.valMlMvtoCal = Mat.Redondear(rTrn.valMlRentaImponible * tasaCotizacion)

    '''''        'SIS//

    '''''        rTrn.valMlMvtoCal = rTrn.valMlMvto
    '''''        rTrn.valMlAdicionalCal = rTrn.valMlAdicional

    '''''        '''''''If Not IsNothing(rMovAcr.codMvtoAdi) Then
    '''''        '''''''    rTrn.valMlAdicionalCal = Mat.Redondear(rTrn.valMlRentaImponible * rTrn.tasaAdicional)
    '''''        '''''''    rTrn.codMvtoAdi = rMovAcr.codMvtoAdi
    '''''        '''''''End If
    '''''    Else
    '''''        rTrn.valMlMvtoCal = valMlMontoPorDistribuir
    '''''    End If



    '''''    If rTrn.tipoPago = 2 Then

    '''''        If Not IsNothing(rMovAcr.codMvtoIntreaCue) Then
    '''''            rTrn.valMlInteresCal = Sys.Servicios.CSer_Clientes.calculoInteres(gdbc,gidAdm, rTrn.perCotizacion, fechaCaja, rTrn.valMlMvtoCal)
    '''''            rTrn.valMlReajusteCal = Sys.Servicios.CSer_Clientes.calculoReajuste(gdbc,gidAdm, rTrn.perCotizacion, fechaCaja, rTrn.valMlMvtoCal)
    '''''            rTrn.codMvtoIntreaCue = rMovAcr.codMvtoIntreaCue
    '''''        End If
    '''''        If Not IsNothing(rMovAcr.codMvtoIntreaAdi) Then
    '''''            rTrn.valMlAdicionalInteresCal = Sys.Servicios.CSer_Clientes.calculoInteres(gdbc,gidAdm, rTrn.perCotizacion, fechaCaja, rTrn.valMlAdicionalCal)
    '''''            rTrn.valMlAdicionalReajusteCal = Sys.Servicios.CSer_Clientes.calculoReajuste(gdbc,gidAdm, rTrn.perCotizacion, fechaCaja, rTrn.valMlAdicionalCal)
    '''''            rTrn.codMvtoIntreaAdi = rMovAcr.codMvtoIntreaAdi
    '''''        End If

    '''''    End If

    '''''    '2.- Distribucion de lo pagado

    '''''    'Fondo
    '''''    If valMlMontoPorDistribuir > 0 And rTrn.valMlMvtoCal > 0 Then
    '''''        If rTrn.valMlMvtoCal > valMlMontoPorDistribuir Then
    '''''            rTrn.valMlMvtoCal = valMlMontoPorDistribuir
    '''''        End If
    '''''    Else
    '''''        rTrn.valMlMvtoCal = 0
    '''''    End If
    '''''    valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlMvtoCal

    '''''    'Interes
    '''''    If valMlMontoPorDistribuir > 0 And rTrn.valMlInteresCal > 0 Then
    '''''        If rTrn.valMlInteresCal > valMlMontoPorDistribuir Then
    '''''            rTrn.valMlInteresCal = valMlMontoPorDistribuir
    '''''        End If
    '''''    Else
    '''''        rTrn.valMlInteresCal = 0
    '''''    End If
    '''''    valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlInteresCal

    '''''    'Reajustes
    '''''    If valMlMontoPorDistribuir > 0 And rTrn.valMlReajusteCal > 0 Then
    '''''        If rTrn.valMlReajusteCal > valMlMontoPorDistribuir Then
    '''''            rTrn.valMlReajusteCal = valMlMontoPorDistribuir
    '''''        End If
    '''''        valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlReajusteCal
    '''''    Else
    '''''        rTrn.valMlReajusteCal = 0
    '''''    End If

    '''''    'Adicional
    '''''    If valMlMontoPorDistribuir > 0 And rTrn.valMlAdicionalCal > 0 Then
    '''''        If rTrn.valMlAdicionalCal > valMlMontoPorDistribuir Then
    '''''            rTrn.valMlAdicionalCal = valMlMontoPorDistribuir
    '''''        End If
    '''''    Else
    '''''        rTrn.valMlAdicionalCal = 0
    '''''    End If
    '''''    valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlAdicionalCal
    '''''    'Interes Adicional
    '''''    If valMlMontoPorDistribuir > 0 And rTrn.valMlAdicionalInteresCal > 0 Then
    '''''        If rTrn.valMlAdicionalInteresCal > valMlMontoPorDistribuir Then
    '''''            rTrn.valMlAdicionalInteresCal = valMlMontoPorDistribuir
    '''''        End If
    '''''    Else
    '''''        rTrn.valMlAdicionalInteresCal = 0
    '''''    End If
    '''''    valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlAdicionalInteresCal
    '''''    'Reajustes Adicional
    '''''    If valMlMontoPorDistribuir > 0 And rTrn.valMlAdicionalReajusteCal > 0 Then
    '''''        If rTrn.valMlAdicionalReajusteCal > valMlMontoPorDistribuir Then
    '''''            rTrn.valMlAdicionalReajusteCal = valMlMontoPorDistribuir
    '''''        End If
    '''''    Else
    '''''        rTrn.valMlAdicionalReajusteCal = 0
    '''''    End If
    '''''    valMlMontoPorDistribuir = valMlMontoPorDistribuir - rTrn.valMlAdicionalReajusteCal

    '''''    'Exceso
    '''''    If valMlMontoPorDistribuir > 0 Then
    '''''        rTrn.valMlExcesoTopeCal = valMlMontoPorDistribuir
    '''''        'tope para el tipo de producto
    '''''        dsAux = parParametrosAcred.traer(gdbc,New Object() {"VID_ADM", "VTIPO_PRODUCTO"}, New Object() {gidAdm, rTrn.tipoProducto}, New Object() {"INTEGER", "STRING"})

    '''''        If dsAux.Tables(0).Rows.Count > 0 Then
    '''''            rTope = New ccAcrParametrosAcred(dsAux)
    '''''            If rTrn.valMlExcesoTopeCal < Mat.Redondear(rTope.valUfLimiteExceso * gvalorUF) Then
    '''''                rTrn.valMlMvtoCal += rTrn.valMlExcesoTopeCal
    '''''                rTrn.valMlExcesoTopeCal = 0
    '''''            End If
    '''''        End If
    '''''    End If

    '''''    If rTrn.valMlPrimaSis > 0 Then
    '''''        rTrn.valMlPrimaSisCal = rTrn.valMlPrimaSis
    '''''        rTrn.valCuoPrimaSisCal = Mat.Redondear(rTrn.valMlPrimaSisCal / rTrn.valMlValorCuota, 2)
    '''''    End If


    '''''    '3.- Conversión a cuotas

    '''''    cuotasFondo = Mat.Redondear((rTrn.valMlMvtoCal + rTrn.valMlInteresCal + rTrn.valMlReajusteCal) / rTrn.valMlValorCuota, 2)
    '''''    cuotasAdicional = Mat.Redondear((rTrn.valMlAdicionalCal + rTrn.valMlAdicionalInteresCal + rTrn.valMlAdicionalReajusteCal) / rTrn.valMlValorCuota, 2)

    '''''    'Fondo
    '''''    rTrn.valCuoMvtoCal = Mat.Redondear(rTrn.valMlMvtoCal / rTrn.valMlValorCuota, 2)
    '''''    rTrn.valCuoInteresCal = Mat.Redondear(rTrn.valMlInteresCal / rTrn.valMlValorCuota, 2)
    '''''    rTrn.valCuoReajusteCal = Mat.Redondear(rTrn.valMlReajusteCal / rTrn.valMlValorCuota, 2)

    '''''    If cuotasFondo <> rTrn.valCuoMvtoCal + rTrn.valCuoInteresCal + rTrn.valCuoReajusteCal Then
    '''''        If rTrn.valCuoInteresCal > 0 Then
    '''''            rTrn.valCuoInteresCal += cuotasFondo - (rTrn.valCuoMvtoCal + rTrn.valCuoInteresCal + rTrn.valCuoReajusteCal)
    '''''        Else
    '''''            rTrn.valCuoReajusteCal += cuotasFondo - (rTrn.valCuoMvtoCal + rTrn.valCuoInteresCal + rTrn.valCuoReajusteCal)
    '''''        End If
    '''''    End If
    '''''    'Adicional
    '''''    If cuotasAdicional > 0 Then
    '''''        rTrn.valCuoAdicionalCal = Mat.Redondear(rTrn.valMlAdicionalCal / rTrn.valMlValorCuota, 2)
    '''''        rTrn.valCuoAdicionalInteresCal = Mat.Redondear(rTrn.valMlAdicionalInteresCal / rTrn.valMlValorCuota, 2)
    '''''        rTrn.valCuoAdicionalReajusteCal = Mat.Redondear(rTrn.valMlAdicionalReajusteCal / rTrn.valMlValorCuota, 2)
    '''''    End If
    '''''    If cuotasAdicional <> rTrn.valCuoAdicionalCal + rTrn.valCuoAdicionalInteresCal + rTrn.valCuoAdicionalReajusteCal Then
    '''''        If rTrn.valCuoInteresCal > 0 Then
    '''''            rTrn.valCuoAdicionalInteresCal += cuotasAdicional - (rTrn.valCuoAdicionalCal + rTrn.valCuoAdicionalInteresCal + rTrn.valCuoAdicionalReajusteCal)
    '''''        Else
    '''''            rTrn.valCuoAdicionalReajusteCal += cuotasAdicional - (rTrn.valCuoAdicionalCal + rTrn.valCuoAdicionalInteresCal + rTrn.valCuoAdicionalReajusteCal)
    '''''        End If
    '''''    End If
    '''''    'Exceso
    '''''    rTrn.valCuoExcesoTopeCal = Mat.Redondear(rTrn.valMlExcesoTopeCal / rTrn.valMlValorCuota, 2)
    '''''    'Ganancia o Perdida
    '''''    rTrn.valCuoCompensCal = Mat.Redondear(rTrn.valMlCompensCal / rTrn.valMlValorCuota, 2)
    '''''    'Ajuste decimal obligatorio
    '''''    rTrn.valCuoAjusteDecimalCal = rTrn.valCuoPatrFdesCal - (cuotasFondo + rTrn.valCuoExcesoTopeCal + rTrn.valCuoCompensCal + cuotasAdicional)

    '''''    If Math.Abs(rTrn.valCuoAjusteDecimalCal) > 0.02 Then
    '''''        GenerarLog("E", 0, "El rezago está descuadrado(" & Str(rTrn.valCuoAjusteDecimalCal) & ")", rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
    '''''        blIgnorar = True
    '''''    End If

    '''''End Sub



    Private Sub AjusteDecimalRecupRezagos(ByVal valMlValorCuota As Decimal)
        If rMovAcr.tipoMvto = "COP" Then
            Exit Sub
        End If

        If rTrn.fecOperacion < "01-10-2002" Then 'antes de 1220
            Dim valDif As Decimal
            'Dim valCuoDif As Decimal
            'Dim valMlDif As Decimal

            Dim valMlCotIntRea As Decimal
            Dim valCuoCotIntRea As Decimal

            Dim valMlAdiIntRea As Decimal
            Dim valCuoAdiIntRea As Decimal

            Dim valMlExc As Decimal
            Dim valCuoExc As Decimal

            Dim valMlCot As Decimal
            Dim valCuoCot As Decimal

            Dim valMlInt As Decimal
            Dim valCuoInt As Decimal

            Dim valMlRea As Decimal
            Dim valCuoRea As Decimal

            Dim valMlAdi As Decimal
            Dim valCuoAdi As Decimal

            Dim valMlAdiInt As Decimal
            Dim valCuoAdiInt As Decimal

            Dim valMlAdiRea As Decimal
            Dim valCuoAdiRea As Decimal

            Dim valCuoTotNominal As Decimal
            Dim valMlTotNominal As Decimal


            valMlTotNominal = rTrn.valMlPatrFrecCal - rTrn.valMlTransferenciaCal
            valCuoTotNominal = Mat.Redondear(valMlTotNominal / valMlValorCuota, 2)

            valMlCotIntRea = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste
            valCuoCotIntRea = Mat.Redondear(valMlCotIntRea / valMlValorCuota, 2)

            valMlAdiIntRea = rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste
            valCuoAdiIntRea = Mat.Redondear(valMlAdiIntRea / valMlValorCuota, 2)

            valMlExc = rTrn.valMlExcesoLinea
            valCuoExc = Mat.Redondear(valMlExc / valMlValorCuota, 2)

            valMlCot = rTrn.valMlMvto
            valCuoCot = Mat.Redondear(valMlCot / valMlValorCuota, 2)

            valMlInt = rTrn.valMlInteres
            valCuoInt = Mat.Redondear(valCuoCotIntRea * rTrn.tasaInteres, 2)

            valMlRea = rTrn.valMlReajuste
            valCuoRea = valCuoCotIntRea - (valCuoCot + valCuoInt)

            valMlAdi = rTrn.valMlAdicional
            valCuoAdi = Mat.Redondear(valMlAdi / valMlValorCuota, 2)

            valMlAdiInt = rTrn.valMlAdicionalInteres
            valCuoAdiInt = Mat.Redondear(valCuoAdiIntRea * rTrn.tasaInteres, 2)

            valMlAdiRea = rTrn.valMlAdicionalReajuste
            valCuoAdiRea = valCuoAdiIntRea - (valCuoAdi + valCuoAdiInt)

            'lfc:27/07/2010-------ajuste decimal: os_2626661
            '------------------>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            valDif = valCuoTotNominal - (valCuoCotIntRea + valCuoAdiIntRea + valCuoExc)
            'valCuoDif = valCuoTotNominal - (valCuoCotIntRea + valCuoAdiIntRea + valCuoExc)
            'valMlDif = valMlTotNominal - (valMlCotIntRea + valMlAdiIntRea + valMlExc)

            rTrn.valCuoAporteAdm = valDif



        End If

    End Sub

    Private Sub AjusteDecimalAbonosRezagos(ByVal valMlValorCuota As Decimal)

        Dim fecInicio1220 As New Date(2002, 10, 1)

        If rTrn.fecOperacion >= fecInicio1220.Date Then  'despues de 1220

            Dim valDif As Decimal

            Dim valMlCotIntRea As Decimal
            Dim valCuoCotIntRea As Decimal

            Dim valMlAdiIntRea As Decimal
            Dim valCuoAdiIntRea As Decimal

            Dim valMlExc As Decimal
            Dim valCuoExc As Decimal

            Dim valMlCot As Decimal
            Dim valCuoCot As Decimal

            Dim valMlInt As Decimal
            Dim valCuoInt As Decimal

            Dim valMlRea As Decimal
            Dim valCuoRea As Decimal

            Dim valMlAdi As Decimal
            Dim valCuoAdi As Decimal

            Dim valMlAdiInt As Decimal
            Dim valCuoAdiInt As Decimal

            Dim valMlAdiRea As Decimal
            Dim valCuoAdiRea As Decimal

            Dim valCuoTotNominal As Decimal
            Dim valMlTotNominal As Decimal


            valMlTotNominal = rTrn.valMlPatrFrecCal - rTrn.valMlTransferenciaCal
            valCuoTotNominal = Mat.Redondear(valMlTotNominal / valMlValorCuota, 2)

            valMlCotIntRea = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste
            valCuoCotIntRea = Mat.Redondear(valMlCotIntRea / valMlValorCuota, 2)

            valMlAdiIntRea = rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste
            valCuoAdiIntRea = Mat.Redondear(valMlAdiIntRea / valMlValorCuota, 2)

            'SIS//----
            Dim valMlPrimaIntRea, valCuoPrimaIntRea As Decimal
            valMlPrimaIntRea = rTrn.valMlPrimaSis + rTrn.valMlPrimaSisInteres + rTrn.valMlPrimaSisReajuste
            valCuoPrimaIntRea = Mat.Redondear(valMlPrimaIntRea / valMlValorCuota, 2)


            valMlExc = rTrn.valMlExcesoLinea
            valCuoExc = Mat.Redondear(valMlExc / valMlValorCuota, 2)

            valMlCot = rTrn.valMlMvto
            valCuoCot = Mat.Redondear(valMlCot / valMlValorCuota, 2)

            valMlInt = rTrn.valMlInteres
            valCuoInt = Mat.Redondear(valCuoCotIntRea * rTrn.tasaInteres, 2)

            valMlRea = rTrn.valMlReajuste
            valCuoRea = valCuoCotIntRea - (valCuoCot + valCuoInt)

            valMlAdi = rTrn.valMlAdicional
            valCuoAdi = Mat.Redondear(valMlAdi / valMlValorCuota, 2)

            valMlAdiInt = rTrn.valMlAdicionalInteres
            valCuoAdiInt = Mat.Redondear(valCuoAdiIntRea * rTrn.tasaInteres, 2)

            valMlAdiRea = rTrn.valMlAdicionalReajuste
            valCuoAdiRea = valCuoAdiIntRea - (valCuoAdi + valCuoAdiInt)


            valDif = valCuoTotNominal - (valCuoCotIntRea + valCuoAdiIntRea + valCuoExc)

            'Se vuel atras AJUSTE DECIMAL. 09/11/2010 PCI
            'rTrn.valCuoAjusteDecimalCal = valDif
            'Me.ajustesDecimalAComisionPorc()


        End If

    End Sub

    Private Sub DeterminarComision()

        Dim tipoCliente As String
        Dim blCotizacion As Boolean


        blCotizacion = rMovAcr.indCotizacion = "S"
        tipoCliente = rTrn.tipoCliente

        'If IsNothing(rMovAcr.tipoComisionPorcentual) Then
        '    If IsNothing(rTipComPor) Then

        dsAux = parTipCom.traer(gdbc, New Object() {"VID_ADM", "VTIPO_COMISION"}, New Object() {gidAdm, rMovAcr.tipoComisionPorcentual}, New Object() {"INTEGER", "STRING"})

        '    End If
        'Else
        '    dsAux = parTipCom.traer(gdbc,New Object() {"VID_ADM", "VTIPO_COMISION"}, New Object() {gidAdm, rMovAcr.tipoComisionPorcentual}, New Object() {"INTEGER", "STRING"})
        'End If 

        If dsAux.Tables(0).Rows.Count = 0 Then
            gcomisionPorcentual = 0
            rTipComPor = Nothing
            rTipComPor = New ccAcrTipoComision(dsAux.Tables(0).NewRow)
        Else

            rTipComPor = Nothing
            rTipComPor = New ccAcrTipoComision(dsAux)

            rTrn.tipoComisionPorcentual = rTipComPor.tipoComision

            If rTrn.tasaAdicional > 0 Then
                gcomisionPorcentual = rTrn.tasaAdicional
            End If

            If blCotizacion And gcomisionPorcentual = 0 Then

                If IsNothing(tipoCliente) Then
                    blIgnorar = True
                    GenerarLog("A", 0, "Hebra " & gIdHebra & " - Tipo de cliente no informado", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                    Exit Sub
                End If

                dsAux = Comisiones.traerUltimoValorComPorc(gdbc, gidAdm, rTrn.perCotizacion, rTrn.tipoProducto, rTipComPor.tipoComision, tipoCliente, rCli.sexo, rCli.fecNacimiento, rCli.codAdmFusion, "POR", gcodAdministradora)
                'dsAux = parComision.traer(gdbc,gidAdm, rTrn.perCotizacion, rTrn.tipoProducto, rTipComPor.tipoComision, tipoCliente, gcodAdministradora, "POR")
                If dsAux.Tables(0).Rows.Count = 0 Then
                    blErrorFatal = True
                    GenerarLog("E", 15338, "Hebra " & gIdHebra & " - Com. Porcentual: " & rTipComPor.tipoComision & " Periodo: " & CDate(rTrn.perCotizacion).Month & "/" & CDate(rTrn.perCotizacion).Year & " Tipo cliente: " & tipoCliente & " Administradora " & gcodAdministradora, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                    blIgnorar = True
                    'Throw New SondaException(15338) '"No existe comision para periodo

                Else

                    codAdmF = IIf(IsDBNull(dsAux.Tables(0).Rows(0).Item("ID_ADM_COMISION")), Nothing, dsAux.Tables(0).Rows(0).Item("ID_ADM_COMISION"))
                    rComPor = Nothing
                    rComPor = New ccAcrComisiones(dsAux)
                    Select Case rComPor.tipoValorComision

                        Case "ML"
                            blErrorFatal = True
                            GenerarLog("E", 15339, "Hebra " & gIdHebra & " - Tipo valor comision: ML", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                            blIgnorar = True
                            'Throw New SondaException(15339) '"Tipo valor comision incorrecto

                        Case "UF"
                            blErrorFatal = True
                            GenerarLog("E", 15339, "Hebra " & gIdHebra & " - Tipo valor comision: UF", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                            blIgnorar = True
                            'Throw New SondaException(15339) '"Tipo valor comision incorrecto

                        Case "TAS" : gcomisionPorcentual = rComPor.valMonto

                        Case "POR" : gcomisionPorcentual = Mat.Redondear(rComPor.valMonto / 100, 9)

                        Case Else
                            GenerarLog("E", 15339, "Hebra " & gIdHebra & " - Tipo valor comision no reconocido", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                            blIgnorar = True
                            'Throw New SondaException(15339) '"Tipo valor comision incorrecto
                    End Select
                End If
            End If
        End If

        'If IsNothing(rMovAcr.tipoComisionFija) Then
        '    If IsNothing(rTipComFij) Then
        '        dsAux = parTipCom.traer(gdbc,New Object() {"VID_ADM", "VTIPO_COMISION"}, New Object() {gidAdm, rMovAcr.tipoComisionFija}, New Object() {"INTEGER", "STRING"})
        '    End If
        'Else

        dsAux = parTipCom.traer(gdbc, New Object() {"VID_ADM", "VTIPO_COMISION"}, New Object() {gidAdm, rMovAcr.tipoComisionFija}, New Object() {"INTEGER", "STRING"})

        'End If

        'dsAux = ParametrosINE.TipoComision.traer(gdbc,gidAdm, rMovAcr.tipoComisionFija)


        If dsAux.Tables(0).Rows.Count = 0 Then
            gcomisionFija = 0
            rTipComFij = Nothing
            rTipComFij = New ccAcrTipoComision(dsAux.Tables(0).NewRow)
        Else
            rTipComFij = Nothing
            rTipComFij = New ccAcrTipoComision(dsAux)

            'If rMovAcr.tipoMvto = "COT" Then

            If IsNothing(tipoCliente) Then
                blIgnorar = True
                GenerarLog("A", 0, "Hebra " & gIdHebra & " - Tipo de cliente no informado", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                Exit Sub
            End If

            dsAux = Comisiones.traerUltimoValorComPorc(gdbc, gidAdm, rTrn.perCotizacion, rTrn.tipoProducto, rTipComFij.tipoComision, tipoCliente, rCli.sexo, rCli.fecNacimiento, rCli.codAdmFusion, "FIJ", gcodAdministradora)
            'dsAux = parComision.traer(gdbc,gidAdm, rTrn.perCotizacion, rTrn.tipoProducto, rTipComFij.tipoComision, tipoCliente, gcodAdministradora, "FIJ")

            If dsAux.Tables(0).Rows.Count = 0 Then
                'blErrorFatal = True
                'GenerarLog("E", 15338, "Com. Fija: " & rTipComFij.tipoComision & " Periodo: " & CDate(rTrn.perCotizacion).Month & "/" & CDate(rTrn.perCotizacion).Year & " Tipo cliente: " & tipoCliente & " Administradora " & gcodAdministradora, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                'blIgnorar = True
                gcomisionFija = 0
            Else
                codAdmF = IIf(IsDBNull(dsAux.Tables(0).Rows(0).Item("ID_ADM_COMISION")), Nothing, dsAux.Tables(0).Rows(0).Item("ID_ADM_COMISION"))
                rComFij = Nothing
                rComFij = New ccAcrComisiones(dsAux)
                Select Case rComFij.tipoValorComision

                    Case "ML" : gcomisionFija = rComFij.valMonto

                    Case "UF" : gcomisionFija = Mat.Redondear(rComFij.valMonto * gvalorUF, 0)

                    Case "FIJ"

                        'Adicional Antiguo sin Calculode comisiones. OS-7079391. 19/03/2015. OS-7243919 01/04/2016
                        If blAdicionalAntiguo Then
                            gcomisionFija = 0
                        Else
                            gcomisionFija = rComFij.valMonto
                        End If

                        'gcomisionFija = rComFij.valMonto

                    Case "TAS"
                        blErrorFatal = True
                        GenerarLog("E", 15339, "Hebra " & gIdHebra & " - Tipo valor comision: TAS", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                        blIgnorar = True
                        'Throw New SondaException(15339) '"Tipo valor comision incorrecto

                    Case "POR"
                        blErrorFatal = True
                        GenerarLog("E", 15339, "Hebra " & gIdHebra & " - Tipo valor comision: POR", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                        blIgnorar = True
                        'Throw New SondaException(15339) '"Tipo valor comision incorrecto

                    Case Else
                        blErrorFatal = True
                        GenerarLog("E", 15339, "Hebra " & gIdHebra & " - Tipo valor comision no reconocido", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
                        blIgnorar = True
                        'Throw New SondaException(15339) '"Tipo valor comision incorrecto

                End Select
            End If
            'End If
        End If


    End Sub


    Private Sub DeterminarTransferencia()
        If blIgnorar Then
            Exit Sub
        End If
        If rTrn.codDestinoTransaccion = "TRF" Then
            rTrn.valMlTransferenciaCal = rTrn.valMlMvto

            If rTrn.codOrigenProceso = "RECAUDAC" Then 'Or rTrn.codOrigenProceso = "TRAINREZ" Or rTrn.codOrigenProceso = "TRAINRZC" Or rTrn.codOrigenProceso = "TRAIPAGN" Then
                rTrn.valCuoTransferenciaCal = Mat.Redondear(rTrn.valMlMvto / rTrn.valMlValorCuotaCaja, 2)
            Else
                rTrn.valCuoTransferenciaCal = rTrn.valCuoMvto
            End If

        Else
            rTrn.valMlTransferenciaCal = rTrn.valMlTransferencia
            rTrn.valCuoTransferenciaCal = Mat.Redondear(rTrn.valMlTransferencia / rTrn.valMlValorCuotaCaja, 2)
        End If

    End Sub


    Private Function DeterminaEstadoAcreditacion(ByVal tipoProceso As String) As String


        Select Case tipoProceso
            Case "SI"
                DeterminaEstadoAcreditacion = "PS"
            Case "AC"
                DeterminaEstadoAcreditacion = "PA"
        End Select


    End Function


    Private Sub CrearControlRenta()

        If gExisteConRen Then
            rConRen.idCliente = rTrn.idCliente
            rConRen.idPersona = rTrn.idPersona
            rConRen.perCotizado = rTrn.perCotizacion
            rConRen.tipoProducto = rTrn.tipoProducto

            rConRen.valMlComisPorcentual = rConRen.valMlComisPorcentual + rTrn.valMlComisPorcentualCal
            rConRen.valMlComisFija = rConRen.valMlComisFija + rTrn.valMlComisFijaCal
            rConRen.valCuoComisFija = rConRen.valCuoComisFija + rTrn.valCuoComisFijaCal
            rConRen.numComsionFija = rConRen.numComsionFija + 1
            rConRen.seqMvtoComisFija = rTrn.seqMvtoDestino


            If gtipoProceso = "AC" Then
                Cotizaciones.ControlRentas.crear(gdbc, gidAdm, rConRen.idCliente, rConRen.perCotizado, _
                                                rConRen.tipoProducto, gcontrolRentasEnLinea, rConRen.idPersona, rConRen.valMlRentaAcum, _
                                                rConRen.valUfRentaAcum, rConRen.valMlComisPorcentual, rConRen.numComsionFija, _
                                                rConRen.seqMvtoComisFija, rConRen.valMlComisFija, rConRen.valCuoComisFija, _
                                                gidUsuarioProceso, gfuncion)
            ElseIf gtipoProceso = "SI" Then
                Cotizaciones.ControlRentas.crearControlSim(gdbc, gidAdm, rConRen.idCliente, rConRen.perCotizado, _
                                                rConRen.tipoProducto, gcontrolRentasEnLinea, rConRen.idPersona, rConRen.valMlRentaAcum, _
                                                rConRen.valUfRentaAcum, rConRen.valMlComisPorcentual, rConRen.numComsionFija, _
                                                rConRen.seqMvtoComisFija, rConRen.valMlComisFija, rConRen.valCuoComisFija, _
                                                gidUsuarioProceso, gfuncion)
            End If
        End If

        'SIS//
        If gExisteConRenSIS Then
            If rTrn.perCotizacion >= New Date(2009, 7, 1).Date And (rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF") Then
                Dim tipoCotizacion As String

                rConRenSIS.idCliente = rTrn.idCliente
                rConRenSIS.idPersona = rTrn.idPersona
                rConRenSIS.perCotizado = rTrn.perCotizacion
                rConRenSIS.tipoProducto = rTrn.tipoProducto
                tipoCotizacion = "SIS"

                If gtipoProceso = "AC" Then
                    Cotizaciones.ControlRentas.crear(gdbc, gidAdm, rConRenSIS.idCliente, rConRenSIS.perCotizado, _
                                                    rConRenSIS.tipoProducto, tipoCotizacion, rConRenSIS.idPersona, rConRenSIS.valMlRentaAcum, _
                                                    rConRenSIS.valUfRentaAcum, rConRenSIS.valMlComisPorcentual, rConRenSIS.numComsionFija, _
                                                    rConRenSIS.seqMvtoComisFija, rConRenSIS.valMlComisFija, rConRenSIS.valCuoComisFija, _
                                                    gidUsuarioProceso, gfuncion)
                ElseIf gtipoProceso = "SI" Then
                    Cotizaciones.ControlRentas.crearControlSim(gdbc, gidAdm, rConRenSIS.idCliente, rConRenSIS.perCotizado, _
                                                  rConRenSIS.tipoProducto, tipoCotizacion, rConRenSIS.idPersona, rConRenSIS.valMlRentaAcum, _
                                                  rConRenSIS.valUfRentaAcum, rConRenSIS.valMlComisPorcentual, rConRenSIS.numComsionFija, _
                                                  rConRenSIS.seqMvtoComisFija, rConRenSIS.valMlComisFija, rConRenSIS.valCuoComisFija, _
                                                  gidUsuarioProceso, gfuncion)
                End If

            End If
        End If

    End Sub

	Private Sub DeterminaPunterosPlanilla()
		Dim p As DataSet
		Dim s As String

		If rTrn.seqMvtoOrigen > 0 And rTrn.codDestinoTransaccion = "CTA" Then

			p = Sys.sysAportesPendientes.Rezagos.traerFolios(gdbc, gidAdm, rTrn.seqMvtoOrigen)

			If p.Tables(0).Rows.Count = 0 Or rTrn.codOrigenProceso = "ACRTOOBL" Or rTrn.codOrigenProceso = "ACRTOPRO" Then			 'retiro10%
				Exit Sub
			End If

			rTrn.numReferenciaOrigen6 = p.Tables(0).Rows(0).Item("FOLIO_PLANILLA")
			rTrn.numReferenciaOrigen2 = p.Tables(0).Rows(0).Item("FOLIO_CAJA")
			rTrn.numReferenciaOrigen5 = p.Tables(0).Rows(0).Item("FOLIO_LOTE")
		End If
	End Sub

	Private Sub CargoControlRenta()

		Dim valMontoUF As Decimal
		Dim tipoCotizacion As String
		Try

			gExisteConRen = False

			If CDate(rTrn.perCotizacion) > rTrn.fecAcreditacion Or rTrn.perCotizacion < gFecInicioSistema Then
				'gvalorUF = 0 'se rezagará por pago anticipado
				Exit Sub
			End If

			'--OS:2035792 -->>>>>
			'sin exceso para CAV y CCV .deposito directo //no necesario valor uf
			If rTrn.codMvto = 420290 Or rTrn.codMvto = 420291 Or _
			rTrn.codMvto = 220290 Or rTrn.codMvto = 220291 Or _
			rTrn.codMvto = 620290 Or rTrn.codMvto = 620291 Then			 'lfc: 15/10/09 CA-2009100123
				'gvalorUF = 0
				Exit Sub
			End If

			'--.--OS:1753303-->>>>
			If rMovAcr.tipoMvto = "RCOS" Then
				tipoCotizacion = "COT"
				gExisteConRen = True

			ElseIf rMovAcr.tipoMvto = "RCOT" Then
				tipoCotizacion = "COT"
				gExisteConRen = True

			ElseIf rMovAcr.tipoMvto = "RCTP" Then
				tipoCotizacion = "SIS"
				gExisteConRen = True
			Else
				Exit Sub
			End If


			dsAux = Cotizaciones.ControlRentas.traer(gdbc, gidAdm, rTrn.idCliente, rTrn.perCotizacion, rTrn.tipoProducto, tipoCotizacion)
			If dsAux.Tables(0).Rows.Count = 0 Then
				rConRen = New ccControlRentas(dsAux.Tables(0).NewRow)
				Exit Sub
			Else
				gExisteConRen = True
				rConRen = New ccControlRentas(dsAux)
			End If

			'Controla Error por Division por 0. PCI 23/01/2012
			If gvalorUF = 0 Then
				blIgnorar = True
				rTrn.codError = 7440				'Valor UF en CERO.
				GenerarLog("A", 7440, "6.- Valor UF en CERO. Producto: " & rTrn.tipoProducto & ", Per Cotizacion:" & rTrn.perCotizacion, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
				Exit Sub
			Else
				valMontoUF = Mat.Redondear(rTrn.valMlRentaImponible / gvalorUF, 0)
			End If

			rConRen.valMlRentaAcum = rConRen.valMlRentaAcum - rTrn.valMlRentaImponible
			rConRen.valUfRentaAcum = rConRen.valUfRentaAcum - valMontoUF

			If gtipoProceso = "AC" Then
				Cotizaciones.ControlRentas.modificar(gdbc, gidAdm, rConRen.idCliente, rConRen.perCotizado, _
						 rConRen.tipoProducto, tipoCotizacion, rConRen.idPersona, rConRen.valMlRentaAcum, _
						 rConRen.valUfRentaAcum, rConRen.valMlComisPorcentual, rConRen.numComsionFija, _
						 rConRen.seqMvtoComisFija, rConRen.valMlComisFija, rConRen.valCuoComisFija, _
						 gidUsuarioProceso, gfuncion)
			End If

		Catch : End Try
	End Sub

	Private Function determinarTransferenciaAdicional()
		Dim indVigencia As String

		indVigencia = "S"
		' Modelo Vuelve atras con lo realizado en Mayo.
		'OS-7390053. MOD-2015060005. 03/06/2015.
		If CDate(rTrn.perCotizacion) <= CDate(rCli.fecAfiliacionAdm).AddMonths(-2) Then
			'verifico si estuvo vigente en otra vida en la afp
			indVigencia = InformacionCliente.verificaVigenciaPeriodo(gdbc, gidAdm, rTrn.idCliente, rTrn.perCotizacion)
		End If

		'If gcodAdministradora = 1034 Then
		'    'Modelo cambia busqueda de Comisiones Descoordinadas a Afiliacion - 1 mes. 
		'    'OS-7153357. MOD-2015030062. 04/05/2015
		'    If CDate(rTrn.perCotizacion) <= CDate(rCli.fecAfiliacionAdm).AddMonths(-1) Then
		'        'verifico si estubo vigente en otra vida en la afp
		'        'indVigencia = "N"
		'        indVigencia = InformacionCliente.verificaVigenciaPeriodo(gdbc, gidAdm, rTrn.idCliente, rTrn.perCotizacion)
		'    End If
		'Else
		'    If CDate(rTrn.perCotizacion) <= CDate(rCli.fecAfiliacionAdm).AddMonths(-2) Then
		'        'verifico si estubo vigente en otra vida en la afp
		'        indVigencia = InformacionCliente.verificaVigenciaPeriodo(gdbc, gidAdm, rTrn.idCliente, rTrn.perCotizacion)
		'    End If
		'End If


		determinarTransferenciaAdicional = (indVigencia = "N")
	End Function



	Private Sub CalcularComisionPorcentual()

		Dim codAdmDestino As Integer

		gAdicionalSeTransfiere = False
		rTrn.valMlPrimaCal = 0


		If (rTrn.valMlAdicionalCal + rTrn.valCuoAdicionalCal = 0) Or gcomisionPorcentual = 0 Then
			rTrn.valMlComisPorcentualCal = 0
			rTrn.valCuoComisPorcentualCal = 0
			Exit Sub
		End If


		gAdicionalSeTransfiere = determinarTransferenciaAdicional()

		If gAdicionalSeTransfiere Then
			dsAux = Sys.Soporte.Parametro.traerGlobal(gdbc, "PAR_ACR_ADM_PRIMAS", New Object() {gidAdm, rCli.codAdmOrigen})
			If dsAux.Tables(0).Rows.Count > 0 Then
				If IsDBNull(dsAux.Tables(0).Rows(0).Item("COD_ADM_PRIMA")) Then
					gAdicionalSeTransfiere = False
				Else
					codAdmDestino = dsAux.Tables(0).Rows(0).Item("COD_ADM_PRIMA")
				End If
			Else
				gAdicionalSeTransfiere = False
			End If
		End If

		If codAdmDestino = gcodAdministradora Then
			gAdicionalSeTransfiere = False
		End If

		If Not gAdicionalSeTransfiere Then

			DeterminarPrimasCiasSeg()

			If rPri.valMlPrimaSeguro > 0 Then

				If gtipoProceso = "AC" Then
					PrimasCiasSeguro.crear(gdbc, gidAdm, rTrn.tipoFondoDestinoCal, rTrn.perContable, rPri.codInstFinanciera, rTrn.idPersona, rTrn.seqRegistro, "ABO", rTrn.perCotizacion, rTrn.tipoCliente, "S", rTrn.codOrigenProceso, rTrn.fecOperacion, rPri.tipoPago, rTrn.valMlMvtoCal, rTrn.valCuoMvtoCal, rTrn.valMlComisFija, rTrn.valMlRentaImponible, rPri.valMlAdicional, rPri.valMlAdicionalInteres, rPri.valMlAdicionalReajuste, rPri.valMlPrimaSeguro, rPri.idAdmCobroAdicional, rPri.codMvto, rPri.porcPrimaSeguro, rPri.porcAdicional, rTrn.idEmpleador, gidUsuarioProceso, gfuncion, rPri.sexo, rPri.fecAcreditacion, rPri.valMlPrimaInteres, rPri.valMlPrimaReajuste, rPri.valCuoPrimaSeguro, rPri.valCuoPrimaInteres, rPri.valCuoPrimaReajuste)
				End If

				If rTrn.valIndPagoPrimCal = "A" Or rTrn.valIndPagoPrimCal = "N" Then
					'Generar movimiento de cargo si la prima debe ser transferida
					rTrn.valMlComisPorcentualCal = rTrn.valMlAdicionalCal + rTrn.valMlAdicionalInteresCal + rTrn.valMlAdicionalReajusteCal - rTrn.valMlPrimaCal
				Else
					rTrn.valIndPagoPrimCal = "P"
					rTrn.valMlComisPorcentualCal = rTrn.valMlAdicionalCal + rTrn.valMlAdicionalInteresCal + rTrn.valMlAdicionalReajusteCal
				End If

			Else

				If gtipoProceso = "AC" And rTrn.tipoCliente = "PE" Then
					rPri.tipoPago = Nothing
					PrimasCiasSeguro.crear(gdbc, gidAdm, rTrn.tipoFondoDestinoCal, rTrn.perContable, rPri.codInstFinanciera, rTrn.idPersona, rTrn.seqRegistro, "ABO", rTrn.perCotizacion, rTrn.tipoCliente, "S", rTrn.codOrigenProceso, rTrn.fecOperacion, rPri.tipoPago, rTrn.valMlMvtoCal, rTrn.valCuoMvtoCal, rTrn.valMlComisFija, rTrn.valMlRentaImponible, rPri.valMlAdicional, rPri.valMlAdicionalInteres, rPri.valMlAdicionalReajuste, rPri.valMlPrimaSeguro, rPri.idAdmCobroAdicional, rPri.codMvto, rPri.porcPrimaSeguro, rPri.porcAdicional, rTrn.idEmpleador, gidUsuarioProceso, gfuncion, rPri.sexo, rPri.fecAcreditacion, rPri.valMlPrimaInteres, rPri.valMlPrimaReajuste, rPri.valCuoPrimaSeguro, rPri.valCuoPrimaInteres, rPri.valCuoPrimaReajuste)
				End If

				rTrn.valMlComisPorcentualCal = rTrn.valMlAdicionalCal + rTrn.valMlAdicionalInteresCal + rTrn.valMlAdicionalReajusteCal

			End If

		Else		  'El adicional completo se transfiere a la ADM_ORIGEN
			rTrn.valMlPrimaCal = rTrn.valMlAdicionalCal + rTrn.valMlAdicionalInteresCal + rTrn.valMlAdicionalReajusteCal
			rTrn.valCuoPrimaCal = rTrn.valCuoAdicionalCal + rTrn.valCuoAdicionalInteresCal + rTrn.valCuoAdicionalReajusteCal
			rTrn.valIndPagoPrimCal = "A"
			rTrn.valMlComisPorcentualCal = 0
			rTrn.valCuoComisPorcentualCal = 0
			rPri.codMvto = "120703"
			rPri.tipoPago = 4

			If gtipoProceso = "AC" Then

				PrimasCiasSeguro.crear(gdbc, gidAdm, rTrn.tipoFondoDestinoCal, rTrn.perContable, codAdmDestino, rTrn.idPersona, rTrn.seqRegistro, "ABO", rTrn.perCotizacion, rTrn.tipoCliente, "S", rTrn.codOrigenProceso, rTrn.fecOperacion, rPri.tipoPago, rTrn.valMlMvtoCal, rTrn.valCuoMvtoCal, rTrn.valMlComisFija, rTrn.valMlRentaImponible, rTrn.valMlAdicionalCal, rTrn.valMlAdicionalInteresCal, rTrn.valMlAdicionalReajusteCal, 0, codAdmDestino, 120703, 0, Mat.Redondear(rTrn.tasaAdicional * 100, 2), rTrn.idEmpleador, gidUsuarioProceso, gfuncion, rPri.sexo, rPri.fecAcreditacion, rPri.valMlPrimaInteres, rPri.valMlPrimaReajuste, rPri.valCuoPrimaSeguro, rPri.valCuoPrimaInteres, rPri.valCuoPrimaReajuste)

			End If

		End If

		rTrn.valCuoComisPorcentualCal = Mat.Redondear(rTrn.valMlComisPorcentualCal / rTrn.valMlCuotaComision, 2)

		If rTrn.codOrigenProceso = "REREZMAS" Or rTrn.codOrigenProceso = "REREZSEL" Then
			' Solo para RECUPERACION DE REZAGOS SE GENERA AJUSTE DECIMAL
			If rTrn.valCuoComisPorcentualCal > 0 Then
				If ValAjusteCom <> 0 Then
					'Existio un Ajuste en Adicional. Se agrega a Comisiones.
					If rTrn.valCuoComisPorcentualCal > 0 Then
						rTrn.valCuoComisPorcentualCal += ValAjusteCom
						rTrn.valCuoComisPorcentualCal = Mat.Redondear(rTrn.valCuoComisPorcentualCal, 2)
					End If
				End If
			Else

			End If
		End If

		'ajuste_decimal
		'Se elimina AJUSTE a COMISION. Debe ir a ADICIONAL. 15/10/2010
		'rTrn.valCuoComisPorcentualCal = rTrn.valCuoComisPorcentualCal + g_valCuoAjusteDec
		'rTrn.valMlComisPorcentualCal = rTrn.valMlComisPorcentualCal + g_valMlAjusteDec



		If rTrn.tasaAdicional = gcomisionPorcentual Then

		Else

			blIgnorar = True
			rTrn.codError = 15319			 'Tasa adicional informada no coincide con registrada
			GenerarLog("A", 15319, "Hebra " & gIdHebra & " - Tasas: " & rTrn.tasaAdicional & " <> " & gcomisionPorcentual, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

		End If

		If (rTrn.valMlComisPorcentualCal + rTrn.valCuoComisPorcentualCal) > 0 Then
			rTrn.tipoComisionPorcentual = rMovAcr.tipoComisionPorcentual			 ' rComPor.tipoComision
			rTrn.codMvtoComPor = rTipComPor.codMvtoComision
		End If



	End Sub

	'SIS//
	Private Sub CalcularComisionPorcentual2()

		Dim codAdmDestino As Integer
		Dim codComisionTrf As String

		gAdicionalSeTransfiere = False
		rTrn.valMlPrimaCal = 0

		'gValMlComisionTRF = 0
		'gValCuoComisionTRF = 0


		'Verifica Cargo Prima para TGR

		'Verifica INDEPENDIENTES
		If (rTrn.tipoEntidadPagadora = "V" Or rTrn.tipoEntidadPagadora = "O" Or rTrn.tipoEntidadPagadora = "A") And rTrn.tipoPago = 3 Then
			If rTrn.codOrigenProceso = "RECAUDAC" Then
				'Se calcula Prima para periodos que se pagaron en fecha que que se acreditan meses despues,
				'quedando meses con pago adelantado, pero con periodo contable de pago posterior
				If rTrn.perCotizacion < DateAdd(DateInterval.Month, -1, gperContable) Then
					DeterminarPrimasCiasSeg2()
				Else
					rPri.valMlPrimaSeguro = 0
				End If
			Else
				'Se calcula Prima para periodos que se pagaron en fecha que que se acreditan meses despues,
				'quedando meses con pago adelantado, pero con periodo contable de pago posterior
				If rTrn.perCotizacion < DateAdd(DateInterval.Month, -1, gperContable) Then
					DeterminarPrimasCiasSeg2()
				Else
					rPri.valMlPrimaSeguro = 0
				End If
			End If
		Else
			'-- INI

			If rTrn.codOrigenProceso = "RECAUDAC" Then

				'modelo--------
				If gcodAdministradora = 1034 Or gcodAdministradora = 1035 Then
					If rTrn.tipoProducto = "CAF" And rTrn.tipoPago = 3 And rTrn.perCotizacion > gPerContableSis Then
						Try : rPri.valMlPrimaSeguro = 0 : Catch : End Try 'error en insert al auxiliar de primas  : lfc//05/05/2010--
						'sin cobro de prima para CAF pago adelantado----------------------
					Else
						DeterminarPrimasCiasSeg2()
					End If

				Else
					If rTrn.tipoProducto = "CAF" And rTrn.tipoPago = 3 Then					' DON: rTrn.perCotizacion >= gPerContableSis 
						Try : rPri.valMlPrimaSeguro = 0 : Catch : End Try 'error en insert al auxiliar de primas  : lfc//05/05/2010--
						'sin cobro de prima para CAF pago adelantado----------------------
					Else
						DeterminarPrimasCiasSeg2()
					End If
				End If
			Else
				'modelo--------
				'If gidAdm = 1034 Then
				If gcodAdministradora = 1034 Or gcodAdministradora = 1035 Then				'Cambiado el 15/10/2010
					If rTrn.tipoProducto = "CAF" And rTrn.perCotizacion > gPerContableSis Then
						Try : rPri.valMlPrimaSeguro = 0 : Catch : End Try 'error en insert al auxiliar de primas  : lfc//05/05/2010--
						'sin cobro de prima para CAF pago adelantado----------------------
					Else
						DeterminarPrimasCiasSeg2()
					End If
				Else
					If rTrn.tipoProducto = "CAF" And rTrn.perCotizacion >= gPerContableSis Then
						Try : rPri.valMlPrimaSeguro = 0 : Catch : End Try 'error en insert al auxiliar de primas  : lfc//05/05/2010--
						'sin cobro de prima para CAF pago adelantado----------------------
					Else
						DeterminarPrimasCiasSeg2()
					End If
				End If
			End If
			'-- FIN
		End If

		'PCI OS-8304810. 07/03/2016. Se agregan Intereses y Reajustes a IF.
		'If ((rTrn.valMlAdicionalCal + rTrn.valCuoAdicionalCal) = 0) Or gcomisionPorcentual = 0 Then

		If ((rTrn.valMlAdicionalCal + rTrn.valMlAdicionalInteresCal + rTrn.valMlAdicionalReajusteCal) + _
		 (rTrn.valCuoAdicionalCal + rTrn.valCuoAdicionalInteresCal + rTrn.valCuoAdicionalReajusteCal) = 0) Or _
		 gcomisionPorcentual = 0 Then

			rTrn.valMlComisPorcentualCal = 0
			rTrn.valCuoComisPorcentualCal = 0

			'SIS// antes de salir crear aux prima
			If gtipoProceso = "AC" And rPri.valMlPrimaSeguro > 0 Then

				'OS-6475336 Apertura de Primas en Proc. ACR_SALDOS_MOVIMIENTOS para TGR. SOLO PARA CAPITAL
				' lfc: 29-06-2018 -OS:11273223 - se añade PLV ya que la prima se crear en la generacion de mvtos tgr




				'If gcodAdministradora = 1033 Or gcodAdministradora = 1034 Or gcodAdministradora = 1032 Then
				If Not (rTrn.tipoProducto = "CCO" And rTrn.tipoImputacion = "ABO" And _
				   ((rTrn.codOrigenProceso = "RECAUDAC" And rTrn.tipoPlanilla = 40) Or _
				   ((rTrn.codOrigenProceso = "REREZMAS" Or rTrn.codOrigenProceso = "REREZSEL" Or _
				  rTrn.codOrigenProceso = "TRAINREZ" Or rTrn.codOrigenProceso = "TRAIPAGN" Or _
				  rTrn.codOrigenProceso = "ACREXTGR") And (rTrn.tipoRezago = 35 Or rTrn.tipoRezago = 36 Or rTrn.tipoRezago = 39)))) Then

					PrimasCiasSeguro.crear(gdbc, gidAdm, rTrn.tipoFondoDestinoCal, rTrn.perContable, gcodAdministradora, rTrn.idPersona, rTrn.seqRegistro, "ABO", rTrn.perCotizacion, rTrn.tipoCliente, "S", rTrn.codOrigenProceso, rTrn.fecOperacion, rPri.tipoPago, rTrn.valMlMvtoCal, rTrn.valCuoMvtoCal, rTrn.valMlComisFija, rTrn.valMlRentaImponible, rTrn.valMlAdicionalCal, rTrn.valMlAdicionalInteresCal, rTrn.valMlAdicionalReajusteCal, rPri.valMlPrimaSeguro, gcodAdministradora, 120806, 0, Mat.Redondear(rTrn.tasaAdicional * 100, 2), rTrn.idEmpleador, gidUsuarioProceso, gfuncion, rPri.sexo, rPri.fecAcreditacion, rPri.valMlPrimaInteres, rPri.valMlPrimaReajuste, rPri.valCuoPrimaSeguro, rPri.valCuoPrimaInteres, rPri.valCuoPrimaReajuste)


				End If







			End If

			Exit Sub
		End If


		gAdicionalSeTransfiere = determinarTransferenciaAdicional()

		If gAdicionalSeTransfiere Then
			dsAux = Sys.Soporte.Parametro.traerGlobal(gdbc, "PAR_ACR_ADM_PRIMAS", New Object() {gidAdm, rCli.codAdmOrigen})
			If dsAux.Tables(0).Rows.Count > 0 Then
				If IsDBNull(dsAux.Tables(0).Rows(0).Item("COD_ADM_PRIMA")) Then
					gAdicionalSeTransfiere = False
				Else
					codAdmDestino = dsAux.Tables(0).Rows(0).Item("COD_ADM_PRIMA")
				End If
			Else
				gAdicionalSeTransfiere = False
			End If
		End If

		If codAdmDestino = gcodAdministradora Then
			gAdicionalSeTransfiere = False
		End If



		rTrn.valMlComisPorcentualCal = rTrn.valMlAdicionalCal + rTrn.valMlAdicionalInteresCal + rTrn.valMlAdicionalReajusteCal

		If gtipoProceso = "AC" And rPri.valMlPrimaSeguro > 0 Then

			''OS-6475336 Apertura de Primas en Proc. ACR_SALDOS_MOVIMIENTOS para TGR. SOLO PARA CAPITAL
			'If gcodAdministradora = 1033 Or gcodAdministradora = 1034 Or gcodAdministradora = 1032 Then
			If Not (rTrn.tipoProducto = "CCO" And rTrn.tipoImputacion = "ABO" And _
			   ((rTrn.codOrigenProceso = "RECAUDAC" And rTrn.tipoPlanilla = 40) Or _
			   ((rTrn.codOrigenProceso = "REREZMAS" Or rTrn.codOrigenProceso = "REREZSEL" Or _
			  rTrn.codOrigenProceso = "TRAINREZ" Or rTrn.codOrigenProceso = "TRAIPAGN" Or _
			  rTrn.codOrigenProceso = "ACREXTGR") And (rTrn.tipoRezago = 35 Or rTrn.tipoRezago = 36 Or rTrn.tipoRezago = 39)))) Then

				PrimasCiasSeguro.crear(gdbc, gidAdm, rTrn.tipoFondoDestinoCal, rTrn.perContable, gcodAdministradora, rTrn.idPersona, rTrn.seqRegistro, "ABO", rTrn.perCotizacion, rTrn.tipoCliente, "S", rTrn.codOrigenProceso, rTrn.fecOperacion, rPri.tipoPago, rTrn.valMlMvtoCal, rTrn.valCuoMvtoCal, rTrn.valMlComisFija, rTrn.valMlRentaImponible, rTrn.valMlAdicionalCal, rTrn.valMlAdicionalInteresCal, rTrn.valMlAdicionalReajusteCal, rPri.valMlPrimaSeguro, gcodAdministradora, 120806, 0, Mat.Redondear(rTrn.tasaAdicional * 100, 2), rTrn.idEmpleador, gidUsuarioProceso, gfuncion, rPri.sexo, rPri.fecAcreditacion, rPri.valMlPrimaInteres, rPri.valMlPrimaReajuste, rPri.valCuoPrimaSeguro, rPri.valCuoPrimaInteres, rPri.valCuoPrimaReajuste)




				' End If
				'Else
				'   PrimasCiasSeguro.crear(gdbc, gidAdm, rTrn.tipoFondoDestinoCal, rTrn.perContable, gcodAdministradora, rTrn.idPersona, rTrn.seqRegistro, "ABO", rTrn.perCotizacion, rTrn.tipoCliente, "S", rTrn.codOrigenProceso, rTrn.fecOperacion, rPri.tipoPago, rTrn.valMlMvtoCal, rTrn.valCuoMvtoCal, rTrn.valMlComisFija, rTrn.valMlRentaImponible, rTrn.valMlAdicionalCal, rTrn.valMlAdicionalInteresCal, rTrn.valMlAdicionalReajusteCal, rPri.valMlPrimaSeguro, gcodAdministradora, 120806, 0, Mat.Redondear(rTrn.tasaAdicional * 100, 2), rTrn.idEmpleador, gidUsuarioProceso, gfuncion, rPri.sexo, rPri.fecAcreditacion, rPri.valMlPrimaInteres, rPri.valMlPrimaReajuste, rPri.valCuoPrimaSeguro, rPri.valCuoPrimaInteres, rPri.valCuoPrimaReajuste)

			End If

		End If

		'rTrn.valCuoComisPorcentualCal = Mat.Redondear(rTrn.valMlComisPorcentualCal / rTrn.valMlCuotaComision, 2)

		'INI OS-7706828. PCI 07/04/2016. Igualar Adicional y Comisiones
		If gcodAdministradora = 1032 And rTrn.codOrigenProceso = "AJUMASIV" Then
			rTrn.valCuoComisPorcentualCal = rTrn.valCuoAdicionalCal + rTrn.valCuoAdicionalInteresCal + rTrn.valCuoAdicionalReajusteCal
		Else
			rTrn.valCuoComisPorcentualCal = Mat.Redondear(rTrn.valMlComisPorcentualCal / rTrn.valMlCuotaComision, 2)
		End If
		'FIN OS-7706828. PCI 07/04/2016. 



		If rTrn.codOrigenProceso = "REREZMAS" Or rTrn.codOrigenProceso = "REREZSEL" Then
			' Solo para RECUPERACION DE REZAGOS SE GENERA AJUSTE DECIMAL
			If rTrn.valCuoComisPorcentualCal > 0 Then
				If ValAjusteCom <> 0 Then
					'Existio un Ajuste en Adicional. Se agrega a Comisiones.
					If rTrn.valCuoComisPorcentualCal > 0 Then
						rTrn.valCuoComisPorcentualCal += ValAjusteCom
						rTrn.valCuoComisPorcentualCal = Mat.Redondear(rTrn.valCuoComisPorcentualCal, 2)
					End If
				End If
			Else

			End If
		End If

		'ajuste_decimal
		'Se elimina AJUSTE a COMISION. Debe ir a ADICIONAL. 15/10/2010
		'rTrn.valCuoComisPorcentualCal = rTrn.valCuoComisPorcentualCal + g_valCuoAjusteDec
		'rTrn.valMlComisPorcentualCal = rTrn.valMlComisPorcentualCal + g_valMlAjusteDec


		If rTrn.tasaAdicional = gcomisionPorcentual Then

		Else
			blIgnorar = True
			rTrn.codError = 15319			 'Tasa adicional informada no coincide con registrada
			GenerarLog("A", 15319, "Hebra " & gIdHebra & " - Tasas: " & rTrn.tasaAdicional & " <> " & gcomisionPorcentual, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

		End If

		'NEW..COMISIONES---->>>>>>>>>>>>>>>>>>>>>>
		'sólo periodos SIS 'lfc:22/10/2009 - CASOS ESPECIALES
		If gAdicionalSeTransfiere And codAdmDestino <> gcodAdministradora And (rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF") Then
			Select Case rTrn.tipoProducto
				Case "CCO" : codComisionTrf = "120703"
				Case "CAF" : codComisionTrf = "620703"
			End Select

			rTrn.tipoComisionPorcentual = "COMD"
			rTrn.codMvtoComPor = codComisionTrf
			' se graba en acr_primas_cias_seg, el monto de la comision descordinada COMD
			'rTrn.valMlPrimaCal = rTrn.valMlAdicionalCal + rTrn.valMlAdicionalInteresCal + rTrn.valMlAdicionalReajusteCal
			'rTrn.valCuoPrimaCal = rTrn.valCuoAdicionalCal + rTrn.valCuoAdicionalInteresCal + rTrn.valCuoAdicionalReajusteCal
			'rTrn.valIndPagoPrimCal = "A"
			'rTrn.valMlComisPorcentualCal = 0
			'rTrn.valCuoComisPorcentualCal = 0
			' rPri.codMvto = "120703"
			' rPri.tipoPago = 4

			If gtipoProceso = "AC" Then

				PrimasCiasSeguro.crear(gdbc, gidAdm, _
						rTrn.tipoFondoDestinoCal, _
						rTrn.perContable, _
					  codAdmDestino, _
						rTrn.idPersona, _
						rTrn.seqRegistro, _
						"ABO", _
					  rTrn.perCotizacion, _
						rTrn.tipoCliente, _
					  "S", _
					  rTrn.codOrigenProceso, _
					  rTrn.fecOperacion, _
					  "4", _
					  rTrn.valMlMvtoCal, _
					  rTrn.valCuoMvtoCal, _
					  rTrn.valMlComisFija, _
					  rTrn.valMlRentaImponible, _
					  rTrn.valMlComisPorcentualCal, _
					  0, _
					  0, _
					  0, _
					  codAdmDestino, _
					   rTrn.codMvtoComPor, _
					  0, _
					  Mat.Redondear(rTrn.tasaAdicional * 100, 2), _
					  rTrn.idEmpleador, _
						gidUsuarioProceso, _
					  "FUN_COMD", _
						rTrn.sexo, _
						rTrn.fecAcreditacion, _
						0, _
						0, _
						0, _
						0, _
						0)

			End If



		Else		  '----------------<<<<<<<<<<<<<<<<<<<<<<<<<<<<


			If (rTrn.valMlComisPorcentualCal + rTrn.valCuoComisPorcentualCal) > 0 Then
				rTrn.tipoComisionPorcentual = rMovAcr.tipoComisionPorcentual				' rComPor.tipoComision
				rTrn.codMvtoComPor = rTipComPor.codMvtoComision
			End If

		End If


		'--LFC: CA-3927246 - NO COMISION PAGOS TGR
		If blNoComisionTGR Then
			If rTrn.tipoProducto = "CCO" And rTrn.tipoImputacion = "ABO" And rTrn.perCotizacion >= New Date(2019, 5, 1) Then

				If (rTrn.codOrigenProceso = "RECAUDAC" And rTrn.tipoPlanilla = 40) Or _
				   ((rTrn.codOrigenProceso = "REREZMAS" Or rTrn.codOrigenProceso = "REREZSEL" Or _
					rTrn.codOrigenProceso = "TRAINREZ" Or rTrn.codOrigenProceso = "TRAIPAGN" Or rTrn.codOrigenProceso = "ACREXTGR") _
				   And (rTrn.tipoRezago = 35 Or rTrn.tipoRezago = 36 Or rTrn.tipoRezago = 39)) Then
					blNoComisionTGR = True

					rTrn.valMlComisPorcentualCal = 0
					rTrn.valCuoComisPorcentualCal = 0
					rTrn.codMvtoComPor = Nothing

				End If
			End If
		End If

	End Sub


	'SIS//
	Private Sub CalcularCargoPrima()

		Dim codAdmDestino As Integer
		Dim codComisionTrf As String

		gAdicionalSeTransfiere = False
		rTrn.valMlPrimaCal = 0

		'gValMlComisionTRF = 0
		'gValCuoComisionTRF = 0


		'Verifica Cargo Prima para TGR

		'Verifica INDEPENDIENTES
		If (rTrn.tipoEntidadPagadora = "V" Or rTrn.tipoEntidadPagadora = "O" Or rTrn.tipoEntidadPagadora = "A") And rTrn.tipoPago = 3 Then
			If rTrn.codOrigenProceso = "RECAUDAC" Then
				'Se calcula Prima para periodos que se pagaron en fecha que que se acreditan meses despues,
				'quedando meses con pago adelantado, pero con periodo contable de pago posterior
				If rTrn.perCotizacion < DateAdd(DateInterval.Month, -1, gperContable) Then
					DeterminarPrimasCiasSeg2()
				Else
					rPri.valMlPrimaSeguro = 0
				End If
			Else
				'Se calcula Prima para periodos que se pagaron en fecha que que se acreditan meses despues,
				'quedando meses con pago adelantado, pero con periodo contable de pago posterior
				If rTrn.perCotizacion < DateAdd(DateInterval.Month, -1, gperContable) Then
					DeterminarPrimasCiasSeg2()
				Else
					rPri.valMlPrimaSeguro = 0
				End If
			End If
		Else
			'-- INI

			If rTrn.codOrigenProceso = "RECAUDAC" Then

				'modelo--------
				If gcodAdministradora = 1034 Or gcodAdministradora = 1035 Then
					If rTrn.tipoProducto = "CAF" And rTrn.tipoPago = 3 And rTrn.perCotizacion > gPerContableSis Then
						Try : rPri.valMlPrimaSeguro = 0 : Catch : End Try 'error en insert al auxiliar de primas  : lfc//05/05/2010--
						'sin cobro de prima para CAF pago adelantado----------------------
					Else
						DeterminarPrimasCiasSeg2()
					End If

				Else
					If rTrn.tipoProducto = "CAF" And rTrn.tipoPago = 3 Then					' DON: rTrn.perCotizacion >= gPerContableSis 
						Try : rPri.valMlPrimaSeguro = 0 : Catch : End Try 'error en insert al auxiliar de primas  : lfc//05/05/2010--
						'sin cobro de prima para CAF pago adelantado----------------------
					Else
						DeterminarPrimasCiasSeg2()
					End If
				End If
			Else
				'modelo--------
				'If gidAdm = 1034 Then
				If gcodAdministradora = 1034 Or gcodAdministradora = 1035 Then				'Cambiado el 15/10/2010
					If rTrn.tipoProducto = "CAF" And rTrn.perCotizacion > gPerContableSis Then
						Try : rPri.valMlPrimaSeguro = 0 : Catch : End Try 'error en insert al auxiliar de primas  : lfc//05/05/2010--
						'sin cobro de prima para CAF pago adelantado----------------------
					Else
						DeterminarPrimasCiasSeg2()
					End If
				Else
					If rTrn.tipoProducto = "CAF" And rTrn.perCotizacion >= gPerContableSis Then
						Try : rPri.valMlPrimaSeguro = 0 : Catch : End Try 'error en insert al auxiliar de primas  : lfc//05/05/2010--
						'sin cobro de prima para CAF pago adelantado----------------------
					Else
						DeterminarPrimasCiasSeg2()
					End If
				End If
			End If
			'-- FIN
		End If

		If (rTrn.valMlAdicionalCal + rTrn.valCuoAdicionalCal = 0) Or gcomisionPorcentual = 0 Then
			rTrn.valMlComisPorcentualCal = 0
			rTrn.valCuoComisPorcentualCal = 0

			'SIS// antes de salir crear aux prima
			If gtipoProceso = "AC" And rPri.valMlPrimaSeguro > 0 Then
				PrimasCiasSeguro.crear(gdbc, gidAdm, rTrn.tipoFondoDestinoCal, rTrn.perContable, gcodAdministradora, rTrn.idPersona, rTrn.seqRegistro, "ABO", rTrn.perCotizacion, rTrn.tipoCliente, "S", rTrn.codOrigenProceso, rTrn.fecOperacion, rPri.tipoPago, rTrn.valMlMvtoCal, rTrn.valCuoMvtoCal, rTrn.valMlComisFija, rTrn.valMlRentaImponible, rTrn.valMlAdicionalCal, rTrn.valMlAdicionalInteresCal, rTrn.valMlAdicionalReajusteCal, rPri.valMlPrimaSeguro, gcodAdministradora, 120806, 0, Mat.Redondear(rTrn.tasaAdicional * 100, 2), rTrn.idEmpleador, gidUsuarioProceso, gfuncion, rPri.sexo, rPri.fecAcreditacion, rPri.valMlPrimaInteres, rPri.valMlPrimaReajuste, rPri.valCuoPrimaSeguro, rPri.valCuoPrimaInteres, rPri.valCuoPrimaReajuste)
				'BACKUP PrimasCiasSeguro.crear(gdbc,gidAdm, rTrn.tipoFondoDestinoCal, rTrn.perContable, codAdmDestino, rTrn.idPersona, rTrn.seqRegistro, "ABO", rTrn.perCotizacion, rTrn.tipoCliente, "S", rTrn.codOrigenProceso, rTrn.fecOperacion, rPri.tipoPago, rTrn.valMlMvtoCal, rTrn.valCuoMvtoCal, rTrn.valMlComisFija, rTrn.valMlRentaImponible, rTrn.valMlAdicionalCal, rTrn.valMlAdicionalInteresCal, rTrn.valMlAdicionalReajusteCal, rPri.valMlPrimaSeguro, codAdmDestino, 120806, 0, Mat.Redondear(rTrn.tasaAdicional * 100, 2), rTrn.idEmpleador, gidUsuarioProceso, gfuncion, rPri.sexo, rPri.fecAcreditacion, rPri.valMlPrimaInteres, rPri.valMlPrimaReajuste, rPri.valCuoPrimaSeguro, rPri.valCuoPrimaInteres, rPri.valCuoPrimaReajuste)
			End If

			Exit Sub
		End If

	End Sub



	Private Sub realizarCargoTransfPrimaComision()

		If rTrn.valIndPagoPrimCal = "A" Or rTrn.valIndPagoPrimCal = "N" Then
			'Generar movimiento de cargo si la prima debe ser transferida
			If rTrn.valMlPrimaCal + rTrn.valMlPrimaCal = 0 Then
				Exit Sub
			End If

			TrnAMovPrima("CAR")
			sMov.item(sMov.count - 1).mov.valMlMvto = rTrn.valMlPrimaCal
			sMov.item(sMov.count - 1).mov.valCuoMvto = rTrn.valCuoPrimaCal
			sMov.item(sMov.count - 1).mov.codMvto = rPri.codMvto
			CrearSaldosMovimientos()

		End If

	End Sub

	'NEW..COMISIONES---->>>>>>>>>>>>>>>>>>>>>>
	'sólo periodos SIS 'lfc:22/10/2009 - CASOS ESPECIALES
	Private Sub realizarCargoTransfComision()

		If gAdicionalSeTransfiere And (rTrn.codMvtoComPor = "120703" Or rTrn.codMvtoComPor = "620703") Then		  ' solo esta condicion new
			'Generar movimiento de cargo si la prima debe ser transferida
			If rTrn.valMlComisPorcentualCal = 0 And rTrn.valCuoComisPorcentualCal = 0 Then
				Exit Sub
			End If

			TrnAMovPrima("CAR")
			sMov.item(sMov.count - 1).mov.valMlMvto = rTrn.valMlComisPorcentualCal
			sMov.item(sMov.count - 1).mov.valCuoMvto = rTrn.valCuoComisPorcentualCal
			sMov.item(sMov.count - 1).mov.codMvto = rTrn.codMvtoComPor
			'CrearSaldosMovimientos()
			CrearSaldosMovimientosTrnComis()
		End If

	End Sub



	Private Sub realizarCargoPrimaSis()
		If (gvalMlPrimaSisCal + gvalCuoPrimaSisCal) > 0 Then

			rTrn.codMvtoPrimCar = CodMvtoPrimaSis(rTrn.tipoProducto)
			TrnAMovSis("ABO")
			CrearSaldosMovimientos()
		End If
	End Sub


	Private Sub realizarAbonosExcesos()
		If gvalMlExcesoCal > 0 Then

			rTrn.codDestinoExcesoTopeCal = "CTA"
			rTrn.codMvtoExcesoTopeCal = CodMvtoExc(rTrn.tipoProducto)
			TrnAMovExc()
			CrearSaldosMovimientos()
			'rTrn.seqDestinoExcesoTopeCal = rMov.seqMvto
		End If
	End Sub

	Private Sub realizarAbonosExcesosEmpl()
		If gvalMlExcesoEmplCal > 0 Then

			rTrn.codMvtoExcEmpl = CodMvtoExcEmp(rTrn.tipoProducto)
			TrnAMovExcEmpl()
			CrearSaldosMovimientos()
			'rTrn.seqDestinoExcesoTopeCal = rMov.seqMvto
		End If
	End Sub

	Private Sub realizarAbonosCargosCompensasion()
		If gvalMlCompenAboCal > 0 Then


			TrnAMovCompen("ABO")
			sMov.item(sMov.count - 1).mov.valMlMvto = gvalMlCompenAboCal
			sMov.item(sMov.count - 1).mov.valCuoMvto = gvalCuoCompenAboCal
			CrearSaldosMovimientos()
			'rTrn.seqDestinoCompenCal = rMov.seqMvto

		End If

		If gvalMlCompenCarCal > 0 Then

			TrnAMovCompen("CAR")
			sMov.item(sMov.count - 1).mov.valMlMvto = gvalMlCompenCarCal
			sMov.item(sMov.count - 1).mov.valCuoMvto = gvalCuoCompenCarCal
			If blSaldoNegativo Then
				CrearSaldosMovimientos(True)
			Else
				CrearSaldosMovimientos()
			End If
			'rTrn.seqDestinoCompenCal = rMov.seqMvto

		End If
	End Sub

	Private Sub CalcularComisionFija()
		Dim mlComision As Decimal
		Dim cuoComision As Decimal

		If gExisteConRen Then
			rConRen.numComsionFija = 0

		End If

		If rConRen.numComsionFija > 0 Then
			rTrn.tipoComisionFija = Nothing
			rTrn.codMvtoComFij = Nothing
			rTrn.valMlComisFijaCal = 0
			rTrn.valCuoComisFijaCal = 0
			Exit Sub
		End If

		If rTrn.valMlMvto + rTrn.valMlAdicional <= 0 Then
			rTrn.valMlComisFijaCal = 0
			rTrn.valCuoComisFijaCal = 0
			Exit Sub
		End If
		'No se cobra comision fija si es un adicional es descordinado
		If gAdicionalSeTransfiere Then
			rTrn.valMlComisFijaCal = 0
			rTrn.valCuoComisFijaCal = 0
			Exit Sub
		End If

		mlComision = gcomisionFija
		cuoComision = Mat.Redondear(mlComision / rTrn.valMlCuotaComision, 2)		  ' comision en cuotas

		If sMov Is Nothing Then
			'SNDLFUENTES, CC DE SALDOS NOTHING PARA ACREXIPS
		Else
			If cuoComision > sMov.valCuoSaldoFinal Then			 'se produce sobregiro
				mlComision = Mat.Redondear(sMov.valCuoSaldoFinal * rTrn.valMlCuotaComision, 0)
				cuoComision = sMov.valCuoSaldoFinal
				If mlComision > gcomisionFija Then
					mlComision = gcomisionFija
				End If
			End If
		End If

		rTrn.valMlComisFijaCal = mlComision
		rTrn.valCuoComisFijaCal = cuoComision


		If rTrn.valCuoComisFijaCal > 0 Then
			rTrn.tipoComisionFija = rTipComFij.tipoComision
			rTrn.codMvtoComFij = rTipComFij.codMvtoComision
			sMov.item(0).mov.codMvtoComFij = rTrn.codMvtoComFij
			sMov.item(0).mov.valMlComisionFija = rTrn.valMlComisFijaCal
			sMov.item(0).mov.valCuoComisionFija = rTrn.valCuoComisFijaCal
			sMov.Cargar(0, rTrn.valMlComisFijaCal, rTrn.valCuoComisFijaCal)
			gCrearConRen = True
		Else
			rTrn.tipoComisionFija = Nothing
			rTrn.codMvtoComFij = Nothing
			rTrn.valMlComisFijaCal = 0
			rTrn.valCuoComisFijaCal = 0
		End If


	End Sub

	Private Sub DeterminarPrimasCiasSeg()

		Dim gValMlPrima, gValCuoPrima, gInstitucion, gTasaPrima As Decimal
		Dim mensaje As String

		gValMlPrima = 0
		gValCuoPrima = 0
		gInstitucion = 0
		gTasaPrima = 0
		rTrn.valMlPrimaCal = 0
		rTrn.valCuoPrimaCal = 0
		rPri.valMlPrimaSeguro = 0
		rPri.tipoPago = Nothing
		rPri.codInstFinanciera = 0


		INEPrimasSeguros.DeterminarMontoPrima(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoCliente, rTPr.indFuturoFinProducto, rCli.codAdmOrigen, rCli.codAdmDestino, gcodAdministradora, rTrn.perCotizacion, rCli.fecAfiliacionAdm, rCli.fecAfiliacionSistema, rTrn.tipoProducto, rTPr.tipoOrigenProducto, rTPr.tipoFinProducto, rTrn.valMlValorCuota, rTrn.valMlAdicionalCal + rTrn.valMlAdicionalInteresCal + rTrn.valMlAdicionalReajusteCal, rTrn.tasaAdicional, rTrn.sexo, gValMlPrima, gValCuoPrima, rTrn.valIndPagoPrimCal, gInstitucion, gTasaPrima, gcodErrorIgnorar)

		If gcodErrorIgnorar <> 0 Then
			blIgnorar = True
			GenerarLog("E", gcodErrorIgnorar, "Hebra " & gIdHebra & " - Error al determinar monto de la prima", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
			gcodErrorIgnorar = 0
			Exit Sub
		End If

		rTrn.valMlPrimaCal = gValMlPrima
		rTrn.valCuoPrimaCal = gValCuoPrima

		'lfc:cargo mayor al abono
		If rTrn.valMlPrimaCal > 0 And rTrn.perCotizacion > New Date(2009, 7, 1) Then
			If rTrn.valMlPrimaSisCal + rTrn.valMlPrimaSisInteresCal + rTrn.valMlPrimaSisReajusteCal > 0 Then
				If rTrn.valMlPrimaCal > (rTrn.valMlPrimaSisCal + rTrn.valMlPrimaSisInteresCal + rTrn.valMlPrimaSisReajusteCal) Then
					rTrn.valMlPrimaCal = rTrn.valMlPrimaSisCal + rTrn.valMlPrimaSisInteresCal + rTrn.valMlPrimaSisReajusteCal
				End If
			Else

			End If
		End If

		If gValMlPrima <= 0 Then
			rTrn.valMlPrimaCal = 0
			rTrn.valCuoPrimaCal = 0
			'Exit Sub
		End If
		If IsNothing(rTrn.valIndPagoPrimCal) Then
			rTrn.valMlPrimaCal = 0
			rTrn.valCuoPrimaCal = 0
			Exit Sub
		End If
		Select Case rTrn.valIndPagoPrimCal

			Case "P"
				rPri.codMvto = Nothing
				rPri.tipoPago = Nothing

			Case "A"
				rPri.codMvto = 120804				'dependiente a administradora antigua
				rPri.tipoPago = 1

			Case "N"
				rPri.codMvto = 120805				'independiente hacia administradora nueva
				rPri.tipoPago = 2

		End Select

		rTrn.valIdInstPagoPrimCal = gInstitucion

		rPri.codInstFinanciera = gInstitucion
		rPri.codOrigenProceso = rTrn.codOrigenProceso
		rPri.fecOperacion = rTrn.fecAcreditacion
		rPri.idAdmCobroAdicional = gcodAdministradora
		rPri.idEmpleador = rTrn.idEmpleador
		rPri.idPersona = rTrn.idPersona
		rPri.indDerechoSeguro = "S"
		rPri.perCotiza = rTrn.perCotizacion
		rPri.perProceso = rTrn.perContable
		rPri.porcPrimaSeguro = Mat.Redondear(gTasaPrima * 100, 2)
		rPri.seqMovimiento = rTrn.seqRegistro
		rPri.tipoFondo = rTrn.tipoFondoDestinoCal
		'rPri.tipoPago = rTrn.tipoPago
		rPri.tipoTrabajador = rTrn.tipoCliente
		rPri.valCuoCco = rTrn.valCuoMvtoCal
		rPri.valMlAdicional = rTrn.valMlAdicionalCal
		rPri.valMlAdicionalInteres = rTrn.valMlAdicionalInteresCal
		rPri.valMlAdicionalReajuste = rTrn.valCuoAdicionalReajusteCal
		rPri.valMlCco = rTrn.valMlMvtoCal
		rPri.valMlComisionFija = rTrn.valMlComisFijaCal
		rPri.valMlPrimaSeguro = rTrn.valMlPrimaCal
		rPri.valMlRentaImponible = rTrn.valMlRentaImponible

		rPri.porcAdicional = Mat.Redondear(rTrn.tasaAdicional * 100, 2)

		'SIS//
		rPri.sexo = rTrn.sexo
		rPri.fecAcreditacion = rTrn.fecAcreditacion
		rPri.valMlPrimaInteres = 0
		rPri.valCuoPrimaInteres = 0
		rPri.valCuoPrimaReajuste = 0
		rPri.valMlPrimaReajuste = 0

	End Sub

	Private Sub DeterminarPrimasCiasSeg2()

		Dim gValMlPrima, gValCuoPrima, gInstitucion, gTasaPrima As Decimal
		Dim gDifPeriodos As Integer
		Dim TipoCliente As String
		' Dim mensaje As String


		'lfc 15/06 OS:8891128
		Dim valMlMontoTotalSIS As Decimal

		gValMlPrima = 0
		gValCuoPrima = 0
		gInstitucion = 0
		gTasaPrima = 0
		rTrn.valMlPrimaCal = 0
		rTrn.valCuoPrimaCal = 0
		rPri.valMlPrimaSeguro = 0
		rPri.tipoPago = Nothing
		rPri.codInstFinanciera = 0



		If rTrn.codOrigenProceso = "ACREXTGR" Or (rTrn.codOrigenProceso = "RECAUDAC" And rTrn.tipoPlanilla = 40) Or _
		   (rTrn.tipoRezago = 35 Or rTrn.tipoRezago = 36 Or rTrn.tipoRezago = 39) Then

			'lfc 15/06 OS:8891128
			valMlMontoTotalSIS = rTrn.valMlPrimaSisCal + rTrn.valMlPrimaSisInteresCal + rTrn.valMlPrimaSisReajusteCal

			Dim TasaPrima As Decimal = 0
			Dim ds As DataSet

			'Se agrega codigo de Tasa Prima ya que solo existia en RECAUDACION. 11/09/2015. OS-7783367.
			If rTrn.tipoProducto = "CCO" And rTrn.tipoCliente = "AV" Then
				If rTrn.tipoEntidadPagadora = "A" Or rTrn.tipoEntidadPagadora = "V" Or rTrn.tipoEntidadPagadora = "O" Then
					TipoCliente = "I"
				Else
					TipoCliente = "DN"
				End If
			Else
				TipoCliente = rTrn.tipoCliente
			End If

			'ds = PrimasCiasSeguro.traerUltimoValor(gdbc, gidAdm, gcodAdministradora, rTrn.perCotizacion, rTrn.tipoProducto, rTrn.tipoCliente, "SIS", rTrn.sexo)

			Try
				'lfc:modifica 20/09/2016 OS:8262782
				ds = PrimasCiasSeguro.traerUltimoValor(gdbc, gidAdm, rCli.idCliente, gcodAdministradora, rTrn.perCotizacion, rTrn.tipoProducto, TipoCliente, "SIS", rTrn.sexo)
			Catch
				ds = PrimasCiasSeguro.traerUltimoValor(gdbc, gidAdm, gcodAdministradora, rTrn.perCotizacion, rTrn.tipoProducto, rTrn.tipoCliente, "SIS", rTrn.sexo)
			End Try
			If ds.Tables(0).Rows.Count > 0 Then
				TasaPrima = IIf(IsDBNull(ds.Tables(0).Rows(0).Item("VAL_MONTO")), 0, ds.Tables(0).Rows(0).Item("VAL_MONTO"))
			End If

			'20130620

			If rTrn.perCotizacion < rTrn.perContable Then
				gDifPeriodos = CInt(DateDiff(DateInterval.Month, rTrn.perCotizacion, rTrn.perContable))

				If rTrn.cantPeriodosSis < gDifPeriodos Then
					gDifPeriodos = rTrn.cantPeriodosSis
				End If


				If rTrn.cantPeriodosSis <= gDifPeriodos Then

					'lfc 15/06 OS:8891128 , comenta
					'rTrn.valMlPrimaCal = rTrn.valMlPrimaSisCal
					rTrn.valMlPrimaCal = valMlMontoTotalSIS

				Else
					'lfc 15/06 OS:8891128 , comenta
					' rTrn.valMlPrimaCal = Int((rTrn.valMlPrimaSisCal / rTrn.cantPeriodosSis) * CInt(DateDiff(DateInterval.Month, rTrn.perCotizacion, rTrn.perContable)))
					rTrn.valMlPrimaCal = Int((valMlMontoTotalSIS / rTrn.cantPeriodosSis) * gDifPeriodos)
				End If

				If rTrn.sexo = "F" Then
					If rTrn.tasaPrima > 0 Then
						rTrn.valMlPrimaCal = Mat.Redondear((((TasaPrima / 100) / rTrn.tasaPrima) * rTrn.valMlPrimaCal))

					Else
						rTrn.valMlPrimaCal = 0
					End If
				Else
					'lfc:04/10/2016 OS:9163322 optimizacion del cobro de prima SIS para hombres, monto exacto al valor abonado
					If rTrn.valMlPrimaCal > 0 And rTrn.cantPeriodosSis > gDifPeriodos Then
						Try
							If gcodAdministradora = 1034 Or gcodAdministradora = 1035 Then
								Dim valMontoCargo As Decimal = Int(Int(valMlMontoTotalSIS / rTrn.cantPeriodosSis) * gDifPeriodos)
								If valMontoCargo >= 0 And valMontoCargo <= valMlMontoTotalSIS Then
									rTrn.valMlPrimaCal = valMontoCargo
								End If
							End If
						Catch : End Try
					End If
				End If

				gValMlPrima = rTrn.valMlPrimaCal
			Else
				rTrn.valMlPrimaCal = 0
			End If

			rTrn.valCuoPrimaCal = Mat.Redondear(rTrn.valMlPrimaCal / rTrn.valMlValorCuota, 2)
			gValCuoPrima = Mat.Redondear(gValMlPrima / rTrn.valMlValorCuota, 2)
			rTrn.valIndPagoPrimCal = "S"

			'fin TGR, recaudac planilla 40, tipo rezago 35 y 36
		Else

			'Se agrega codigo de Tasa Prima ya que solo existia en RECAUDACION. 11/09/2015. OS-7783367.
			If rTrn.tipoProducto = "CCO" And rTrn.tipoCliente = "AV" Then
				If rTrn.tipoEntidadPagadora = "A" Or rTrn.tipoEntidadPagadora = "V" Or rTrn.tipoEntidadPagadora = "O" Then
					TipoCliente = "I"
				Else
					TipoCliente = "DN"
				End If
			Else
				TipoCliente = rTrn.tipoCliente
			End If


			'SIS//se pasa la renta imponible sis---info desde reca
			INEPrimasSeguros.DeterminarMontoPrimaSIS(gdbc, gidAdm, rTrn.idCliente, TipoCliente, rCli.codAdmOrigen, rCli.codAdmDestino, gcodAdministradora, rTrn.perCotizacion, rTrn.tipoProducto, rTrn.valMlValorCuota, rTrn.valMlRentaImponibleSis, rTrn.sexo, gValMlPrima, gValCuoPrima, rTrn.valIndPagoPrimCal, gInstitucion, gTasaPrima, gcodErrorIgnorar)

			If gcodErrorIgnorar <> 0 Then
				blIgnorar = True
				GenerarLog("E", gcodErrorIgnorar, "Hebra " & gIdHebra & " - Error al determinar monto de la prima", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
				gcodErrorIgnorar = 0
				Exit Sub
			End If

			rTrn.valIdInstPagoPrimCal = 0



			'lfc 15/06 OS:8891128 , mueve if... para que afecte a las trs que sean distinctas de pago TGR, planilla 40 o tiporezago 35 y3 6
			If gcodAdministradora = 1034 Or gcodAdministradora = 1035 Then
				'SIS// no calcular mas de lo informado


				'If gValMlPrima > rTrn.valMlPrimaSis Then
				If gValMlPrima > rTrn.valMlPrimaSisCal Then
					rTrn.valMlPrimaCal = rTrn.valMlPrimaSisCal + rTrn.valMlPrimaSisInteresCal + rTrn.valMlPrimaSisReajusteCal
					rTrn.valCuoPrimaCal = Mat.Redondear(rTrn.valMlPrimaCal / rTrn.valMlCuotaComision, 2)
				Else
					If rTrn.sexo = "F" Then
						Dim intCal, reaCal As Decimal
						'SIS//calcular nuevo int-rea monto prima calculado
						INEPrimasSeguros.calcularIntReaPrimaSis(rTrn.valMlPrimaSis, gValMlPrima, rTrn.valMlPrimaSisInteres, rTrn.valMlPrimaSisReajuste, intCal, reaCal)
						'calculo + factor de interes y reajuste
						rTrn.valMlPrimaCal = gValMlPrima + intCal + reaCal
						rTrn.valCuoPrimaCal = Mat.Redondear(rTrn.valMlPrimaCal / rTrn.valMlCuotaComision, 2)
					Else
						'OS-8016503 Se excluye IF ya que no esta considerando INT y REAJ para Sexo M en el cargo de PRIMA para planillas 40
						'If rTrn.codOrigenProceso = "ACREXTGR" Or (rTrn.codOrigenProceso = "RECAUDAC" And rTrn.tipoPlanilla = 40) Or (rTrn.tipoRezago = "35" Or rTrn.tipoRezago = "36") Then 'Se grega Rezagos ya que no estaban

						'Else
						rTrn.valMlPrimaCal = rTrn.valMlPrimaSisCal + rTrn.valMlPrimaSisInteresCal + rTrn.valMlPrimaSisReajusteCal
						'End If
						rTrn.valCuoPrimaCal = Mat.Redondear(rTrn.valMlPrimaCal / rTrn.valMlCuotaComision, 2)
					End If
				End If
			Else
				'SIS// no calcular mas de lo informado
				If gValMlPrima > rTrn.valMlPrimaSisCal Then
					rTrn.valMlPrimaCal = rTrn.valMlPrimaSisCal + rTrn.valMlPrimaSisInteresCal + rTrn.valMlPrimaSisReajusteCal
					rTrn.valCuoPrimaCal = Mat.Redondear(rTrn.valMlPrimaCal / rTrn.valMlValorCuota, 2)
				Else
					If rTrn.sexo = "F" Then
						Dim intCal, reaCal As Decimal
						'SIS//calcular nuevo int-rea monto prima calculado
						INEPrimasSeguros.calcularIntReaPrimaSis(rTrn.valMlPrimaSis, gValMlPrima, rTrn.valMlPrimaSisInteres, rTrn.valMlPrimaSisReajuste, intCal, reaCal)
						'calculo + factor de interes y reajuste
						rTrn.valMlPrimaCal = gValMlPrima + intCal + reaCal
						rTrn.valCuoPrimaCal = Mat.Redondear(rTrn.valMlPrimaCal / rTrn.valMlValorCuota, 2)
					Else
						'OS-8016503 Se excluye IF ya que no esta considerando INT y REAJ para Sexo M en el cargo de PRIMA para planillas 40
						'If rTrn.codOrigenProceso = "ACREXTGR" Or (rTrn.codOrigenProceso = "RECAUDAC" And rTrn.tipoPlanilla = 40) Or (rTrn.tipoRezago = "35" Or rTrn.tipoRezago = "36") Then 'Se grega Rezagos ya que no estaban
						'Else
						rTrn.valMlPrimaCal = rTrn.valMlPrimaSisCal + rTrn.valMlPrimaSisInteresCal + rTrn.valMlPrimaSisReajusteCal
						'End If
						'lfc:comenta 27-04-2018 - 
						'rTrn.valCuoPrimaCal = Mat.Redondear(rTrn.valMlPrimaCal / rTrn.valMlValorCuota, 2)
						rTrn.valCuoPrimaCal = rTrn.valCuoPrimaSisCal + rTrn.valCuoPrimaSisInteresCal + rTrn.valCuoPrimaSisReajusteCal
					End If
				End If
			End If

		End If


		If gValMlPrima <= 0 Then
			rTrn.valMlPrimaCal = 0
			rTrn.valCuoPrimaCal = 0
		End If

		If IsNothing(rTrn.valIndPagoPrimCal) Then
			rTrn.valMlPrimaCal = 0
			rTrn.valCuoPrimaCal = 0
			Exit Sub
		End If

		If rTrn.codOrigenProceso = "AJUMASIV" And rTrn.valCuoPrimaCal <> (rTrn.valCuoPrimaSisCal + rTrn.valCuoPrimaSisInteresCal + rTrn.valCuoPrimaSisReajusteCal) And rTrn.sexo = "M" And gcodAdministradora = 1033 Then
			'Calculo de Diferencia aplica solo al MASCULINO(Rutina de RECAUDACION), en caso de FEMENINO, el valor
			'informado es con tasa MASCULINA y el Cargo de la Prima es con Tasa FEMENINA.

			'OS-3500316 Diferencia de Calculo entre Cargo Prima y Prima Informada. PAC. 28/02/2011
			Dim DifAjus As Decimal
			DifAjus = rTrn.valCuoPrimaCal - (rTrn.valCuoPrimaSisCal + rTrn.valCuoPrimaSisInteresCal + rTrn.valCuoPrimaSisReajusteCal)
			If Math.Abs(DifAjus) > 0.01 Then
				blIgnorar = True
				GenerarLog("E", gcodErrorIgnorar, "Hebra " & gIdHebra & " - Cargo de Prima Calculado <> de Prima Informada", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
				gcodErrorIgnorar = 0
				Exit Sub
			End If
			rTrn.valCuoPrimaCal = (rTrn.valCuoPrimaSisCal + rTrn.valCuoPrimaSisInteresCal + rTrn.valCuoPrimaSisReajusteCal)
		End If


		If rTrn.tipoProducto = "CCO" Then
			rPri.codMvto = 120806
			rPri.tipoPago = Nothing
		ElseIf rTrn.tipoProducto = "CAF" Then
			rPri.codMvto = 620806
			rPri.tipoPago = Nothing
		Else
			rPri.codMvto = Nothing
			rPri.tipoPago = Nothing
		End If

		'SIS//--no hay transferencia
		'rTrn.valIdInstPagoPrimCal = gInstitucion 
		'rPri.codInstFinanciera = gInstitucion

		rPri.codInstFinanciera = 0


		rPri.codOrigenProceso = rTrn.codOrigenProceso
		rPri.fecOperacion = rTrn.fecAcreditacion
		rPri.idAdmCobroAdicional = gcodAdministradora
		rPri.idEmpleador = rTrn.idEmpleador
		rPri.idPersona = rTrn.idPersona
		rPri.indDerechoSeguro = "S"
		rPri.perCotiza = rTrn.perCotizacion
		rPri.perProceso = rTrn.perContable
		rPri.porcPrimaSeguro = Mat.Redondear(gTasaPrima * 100, 2)
		rPri.seqMovimiento = rTrn.seqRegistro
		rPri.tipoFondo = rTrn.tipoFondoDestinoCal
		rPri.tipoTrabajador = rTrn.tipoCliente
		rPri.valCuoCco = rTrn.valCuoMvtoCal
		rPri.valMlAdicional = rTrn.valMlAdicionalCal
		rPri.valMlAdicionalInteres = rTrn.valMlAdicionalInteresCal
		rPri.valMlAdicionalReajuste = rTrn.valCuoAdicionalReajusteCal

		rPri.valMlCco = rTrn.valMlMvtoCal
		rPri.valMlComisionFija = rTrn.valMlComisFijaCal

		rPri.valMlPrimaSeguro = rTrn.valMlPrimaCal
		rPri.valCuoPrimaSeguro = rTrn.valCuoPrimaCal
		rPri.valMlRentaImponible = rTrn.valMlRentaImponibleSis
		rPri.porcAdicional = Mat.Redondear(rTrn.tasaAdicional * 100, 2)

		'SIS//
		rPri.sexo = rTrn.sexo
		rPri.fecAcreditacion = rTrn.fecAcreditacion
		rPri.valMlPrimaInteres = 0
		rPri.valCuoPrimaInteres = 0
		rPri.valCuoPrimaReajuste = 0
		rPri.valMlPrimaReajuste = 0
		rTrn.tasaPrimaCal = gTasaPrima
	End Sub


	Private Sub DeterminaMontoInstitucionSalud()
		If rTrn.valMlSalud + rTrn.valCuoSalud > 0 Then
			If gtipoProceso = "AC" Then
				' lfc : os:2949437 - error en recaudacion distribuida--14/06/2010
				'se utilizará el campo val_cuo_salud para grabar la renta imponible full..
				'--antesCompaniasSalud.crear(gdbc,gidAdm, rTrn.seqRegistro, rTrn.codOrigenProceso, rTrn.idPersona, rTrn.perCotizacion, rTrn.idInstSalud, rTrn.perContable, rTrn.fecAcreditacion, rTrn.valMlRentaImponible, rTrn.tipoCliente, rTrn.valMlSalud, 0, rTrn.idEmpleador, rTrn.numReferenciaOrigen1, rTrn.numeroId, rTrn.valUfInstSalud, rTrn.porcInstSalud, gidUsuarioProceso, gfuncion)
				CompaniasSalud.crear(gdbc, gidAdm, rTrn.seqRegistro, rTrn.codOrigenProceso, rTrn.idPersona, rTrn.perCotizacion, rTrn.idInstSalud, rTrn.perContable, rTrn.fecAcreditacion, Mat.Redondear(IIf(rTrn.valCuoSalud > 0, rTrn.valCuoSalud, rTrn.valMlRentaImponible), 0), rTrn.tipoCliente, rTrn.valMlSalud, 0, rTrn.idEmpleador, rTrn.numReferenciaOrigen1, rTrn.numeroId, rTrn.valUfInstSalud, rTrn.porcInstSalud, gidUsuarioProceso, gfuncion)
			End If
		End If
	End Sub

	Private Function DeterminaFondoDestino() As String
		Dim fondoDestino As String
		Select Case True
			Case blAcreditarARezago
				fondoDestino = "C"

			Case rTrn.codDestinoTransaccionCal = "TRF"
				fondoDestino = "C"

			Case rTrn.codDestinoTransaccion = "REZ"
				fondoDestino = "C"

			Case rTrn.codDestinoTransaccionCal = "REZ"
				fondoDestino = "C"

			Case rTrn.codOrigenProceso = "RECAUDAC"
				'MSC: 
				'fondoDestino = rTPr.tipoFondoRecaudacion
				fondoDestino = rTrn.tipoFondoDestino
				'FIN MSC

			Case rTrn.codOrigenProceso = "COLLECT"
				fondoDestino = rTPr.tipoFondoRecaudacion

			Case rTrn.codOrigenProceso = "DECCOBR"
				fondoDestino = rTPr.tipoFondoRecaudacion

			Case rTrn.codOrigenProceso = "TRAINREZ"
				'MSC: 
				' fondoDestino = rTPr.tipoFondoRecaudacion
				fondoDestino = rTrn.tipoFondoDestino
				'FIN MSC
			Case rTrn.codOrigenProceso = "TRAINRZC"
				'MSC: 
				' fondoDestino = rTPr.tipoFondoRecaudacion
				fondoDestino = rTrn.tipoFondoDestino
				'FIN MSC
			Case rTrn.codOrigenProceso = "TRAIPAGN"
				'MSC:
				'fondoDestino = rTPr.tipoFondoRecaudacion
				fondoDestino = rTrn.tipoFondoDestino
				' FIN MSC
			Case rTrn.codOrigenProceso = "TRAINAPV"

				'LFC://02/11/2009 --- el origen, calcula el fondo de Destino, y abre el monto de acuerdo
				' a la distribucion del afiliado, segun actividad (OS) creada desde CA-2008120024  
				' COMENTAR Y DESCOMENTAR
				'fondoDestino = rTPr.tipoFondoRecaudacion
				fondoDestino = rTrn.tipoFondoDestino

			Case rTrn.codOrigenProceso = "TRAINTRA"
				fondoDestino = rTPr.tipoFondoRecaudacion

			Case rTrn.codOrigenProceso = "REREZMAS"
				'--.--fondoDestino = rTPr.tipoFondoRecaudacion
				fondoDestino = rTrn.tipoFondoDestino

			Case rTrn.codOrigenProceso = "REREZSEL"
				'--.--fondoDestino = rTPr.tipoFondoRecaudacion
				fondoDestino = rTrn.tipoFondoDestino

			Case rTrn.codOrigenProceso = "REREZCON"
				'--.--fondoDestino = rTPr.tipoFondoRecaudacion
				fondoDestino = rTrn.tipoFondoDestino

				'Acreditacion Externa. 20/08/2012
			Case rTrn.codOrigenProceso = "ACREXIPS" Or rTrn.codOrigenProceso = "ACREXSTJ" Or rTrn.codOrigenProceso = "ACREXTBF" Or rTrn.codOrigenProceso = "ACREXAFC"
				fondoDestino = rTrn.tipoFondoDestino

			Case Else
				If rTrn.tipoImputacion = "ABO" Then
					fondoDestino = rTrn.tipoFondoDestino
				Else
					fondoDestino = rTrn.tipoFondoOrigen
				End If

		End Select
		If IsNothing(fondoDestino) Then

			blIgnorar = True
			rTrn.codError = 15310			 'Error en la Acreditacion
			GenerarLog("E", 15310, "Hebra " & gIdHebra & " - No se pudo determinar Tipo Fondo Destino", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

		End If
		If fondoDestino <> "A" And fondoDestino <> "B" And fondoDestino <> "C" And fondoDestino <> "D" And fondoDestino <> "E" Then

			blIgnorar = True
			rTrn.codError = 15310			 'Error en la Acreditacion
			GenerarLog("E", 15310, "Hebra " & gIdHebra & " - Tipo Fondo Destino incorrecto " & fondoDestino, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

		End If
		'If gtipoProceso = "AC" Then
		'    If Not IsNothing(rTrn.tipoFondoDestinoCal) Then
		'        If rTrn.tipoFondoDestinoCal <> fondoDestino Then
		'            blIgnorar = True
		'            blErrorFatal = True
		'            rTrn.codError = 15310 'Error en la Acreditacion
		'            GenerarLog("E", 15310, "Tipo de fondo destino difiere con el de la simulación ", rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
		'            GenerarLog("E", 15310, "Simulación :" & rTrn.tipoFondoDestinoCal & " Acreditación: " & fondoDestino, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
		'        End If
		'    End If
		'End If
		DeterminaFondoDestino = fondoDestino


	End Function

	Private Sub ValidaFondoDestinoInicial()
		If Not IsNothing(gtipoFondoDestinoOriginal) And gtipoProceso = "AC" Then
			If gtipoFondoDestinoOriginal <> rTrn.tipoFondoDestinoCal Then
				blIgnorar = True
				blErrorFatal = True

				rTrn.codError = 15310				'Error en la Acreditacion
				GenerarLog("E", 15310, "Hebra " & gIdHebra & " - Tipo de fondo destino difiere con el de la simulación ", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
				GenerarLog("E", 15310, "Hebra " & gIdHebra & " - Simulación :" & gtipoFondoDestinoOriginal & " Acreditación: " & rTrn.tipoFondoDestinoCal, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
			End If
		End If

	End Sub
	Private Sub ValidarParaAcreditacion()

		If (rTrn.valMlAdicional + rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste) > 0 Or _
		   (rTrn.valCuoAdicional + rTrn.valCuoAdicionalInteres + rTrn.valCuoAdicionalReajuste) > 0 Then
			If rTrn.codMvtoAdi = Nothing And Not IsNothing(rMovAcr.codMvtoAdi) Then
				rTrn.codError = 15321				'Codigo adicional no informado
				GenerarLog("A", 15321, "Hebra " & gIdHebra & " - " & ControlAcr.LogAcreditacion.obtenerSondaException(gdbc, 15321), gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
				blIgnorar = True
				Exit Sub

			Else

				'SIS//
				If rTrn.perCotizacion >= New Date(2009, 7, 1).Date And (rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF") Then
					If rTrn.codMvtoAdi <> rMovAcr.codMvtoComis Then
						rTrn.codError = 15322						 'Codigo adicional incorrecto
						GenerarLog("A", 15322, "Hebra " & gIdHebra & " - Codigo Comision: " & Trim(rTrn.codMvtoAdi) & ", " & Trim(rMovAcr.codMvtoComis), gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
						blIgnorar = True
						Exit Sub
					End If



					If (rTrn.valMlPrimaSis + rTrn.valMlPrimaSisInteres + rTrn.valMlPrimaSisReajuste) > 0 Then
						If rTrn.codMvtoPrim = Nothing Then
							rTrn.codError = 20511							'Codigo PRIMA incorrecto
							GenerarLog("A", 20511, "Hebra " & gIdHebra & " - Codigo Prima Vacio ", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
							blIgnorar = True
							Exit Sub

						End If


						If rTrn.codMvtoPrim <> rMovAcr.codMvtoPrim Then
							rTrn.codError = 20511							'Codigo PRIMA incorrecto
							GenerarLog("A", 20511, "Hebra " & gIdHebra & " - Codigo Prima: " & Trim(rTrn.codMvtoPrim) & ", " & Trim(rMovAcr.codMvtoPrim), gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
							blIgnorar = True
							Exit Sub
						End If

					End If
				Else

					If rTrn.codMvtoAdi <> rMovAcr.codMvtoAdi Then
						rTrn.codError = 15322						 'Codigo adicional incorrecto
						GenerarLog("A", 15322, "Hebra " & gIdHebra & " - Codigo Adicional: " & Trim(rTrn.codMvtoAdi) & ", " & Trim(rMovAcr.codMvtoAdi), gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
						blIgnorar = True
						Exit Sub
					End If
				End If

			End If
		End If

		If (rTrn.codDestinoTransaccion = "CTA" And rTrn.tipoImputacion = "ABO") Or (rTrn.codOrigenTransaccion = "CTA" And rTrn.tipoImputacion = "CAR") Then


			ValidarRezago()

			Select Case True

				Case dsValRez.Tables(0).Rows.Count = 0
					blErrorFatal = True
					blIgnorar = True
					GenerarLog("E", 15331, "Hebra " & gIdHebra & " - Error al validar rezagos", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
					'Throw New SondaException(15331) '"Error al validar rezagos

				Case dsValRez.Tables(0).Rows(0).Item("CODE_ERROR") <> 0
					blErrorFatal = True
					blIgnorar = True
					GenerarLog("E", 15331, "Hebra " & gIdHebra & " - Codigo error validacion: " & dsValRez.Tables(0).Rows(0).Item("CODE_ERROR"), gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
					'Throw New SondaException(15331) '"Error al validar rezagos

				Case IsDBNull(dsValRez.Tables(0).Rows(0).Item("COD_CAUSAL_REZAGO"))

					blAcreditarARezago = False
					rTrn.codCausalRezagoCal = Nothing

				Case Else

					Try
						Dim causalRez As String = dsValRez.Tables(0).Rows(0).Item("COD_CAUSAL_REZAGO")


						If causalRez.Length > 1 Then
							rTrn.codCausalRezagoCal = dsValRez.Tables(0).Rows(0).Item("COD_CAUSAL_REZAGO")

							blAcreditarARezago = True
						End If

					Catch
						rTrn.codCausalRezagoCal = dsValRez.Tables(0).Rows(0).Item("COD_CAUSAL_REZAGO")

						blAcreditarARezago = True
					End Try

			End Select

		End If

		If IsNothing(rTrn.codCausalRezagoCal) Then
			If Not IsNothing(gcausalRezago) Then

				blAcreditarARezago = True
				rTrn.codCausalRezagoCal = gcausalRezago

				GenerarLog("A", 0, "Hebra " & gIdHebra & " - a Rezagos, causal: " & gcausalRezago, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)


				Select Case gcausalRezago

					Case "20" : GenerarLog("A", 0, "Hebra " & gIdHebra & " - Tipo producto no vigente: " & Trim(rTrn.tipoProducto), gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
					Case "21" : GenerarLog("A", 0, "Hebra " & gIdHebra & " - Producto no vigente: " & Trim(rTrn.tipoProducto) & ", " & Trim(rTrn.tipoFondoDestinoCal), gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
					Case "22" : GenerarLog("A", 0, "Hebra " & gIdHebra & " - Saldo no vigente: " & Trim(rTrn.tipoProducto) & ", " & Trim(rTrn.tipoFondoDestinoCal) & ", " & Trim(rTrn.categoria), gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
					Case "23" : GenerarLog("A", 0, "Hebra " & gIdHebra & " - Distribucion no vigente: " & rPro.numProducto, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
					Case Else
						dsAux = Parametro.traerGlobal(gdbc, "PAR_APA_CAUSALREZAGO", New Object() {gidAdm, rTrn.codCausalRezagoCal})
						If dsAux.Tables(0).Rows.Count > 0 Then
							gDescripcionCausalRezago = dsAux.Tables(0).Rows(0).Item("DESCRIPCION")
						Else
							gDescripcionCausalRezago = "Causal indeterminada " & rTrn.codCausalRezagoCal
						End If
				End Select
			Else
				gDescripcionCausalRezago = Nothing
			End If

		Else
			GenerarLog("A", 0, "Hebra " & gIdHebra & " - destino Rezagos, causal: " & gcausalRezago, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

			Select Case gcausalRezago

				Case "20" : GenerarLog("A", 0, "Hebra " & gIdHebra & " - Tipo producto no vigente: " & Trim(rTrn.tipoProducto), gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
				Case "21" : GenerarLog("A", 0, "Hebra " & gIdHebra & " - Producto no vigente: " & Trim(rTrn.tipoProducto) & ", " & Trim(rTrn.tipoFondoDestinoCal), gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
				Case "22" : GenerarLog("A", 0, "Hebra " & gIdHebra & " - Saldo no vigente: " & Trim(rTrn.tipoProducto) & ", " & Trim(rTrn.tipoFondoDestinoCal) & ", " & Trim(rTrn.categoria), gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
				Case "23" : GenerarLog("A", 0, "Hebra " & gIdHebra & " - Distribucion no vigente: " & rPro.numProducto, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
				Case Else
					dsAux = Parametro.traerGlobal(gdbc, "PAR_APA_CAUSALREZAGO", New Object() {gidAdm, rTrn.codCausalRezagoCal})
					If dsAux.Tables(0).Rows.Count > 0 Then
						gDescripcionCausalRezago = dsAux.Tables(0).Rows(0).Item("DESCRIPCION")
					Else
						gDescripcionCausalRezago = Nothing
						gDescripcionCausalRezago = "Indeterminada " & rTrn.codCausalRezagoCal
					End If
			End Select

		End If

		If blAcreditarARezago Then
			Exit Sub
		End If

		DeterminarComision()

		If (rTrn.valMlComisFija + rTrn.valCuoComisFija) > 0 Then

			Select Case True
				Case rTrn.codMvtoComFij = Nothing
					rTrn.codError = 15323					  'Codigo comision fija no informado
					GenerarLog("A", 15323, Nothing, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
					blIgnorar = True
					Exit Sub

				Case rTrn.codMvtoComFij <> rTipComFij.codMvtoComision
					rTrn.codError = 15324					  'Codigo comision fija incorrecto
					GenerarLog("A", 15324, Nothing, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
					blIgnorar = True
					Exit Sub

				Case rTrn.tipoComisionFija = Nothing
					rTrn.codError = 15325					  'Tipo comision fija no informado
					GenerarLog("A", 15325, Nothing, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
					blIgnorar = True
					Exit Sub

				Case rTrn.tipoComisionFija <> rMovAcr.tipoComisionFija
					rTrn.codError = 15326					  'Tipo comision fija incorrecto
					GenerarLog("A", 15326, "Hebra " & gIdHebra & " - Tipo comision fija: " & Trim(rTrn.tipoComisionFija) & ", " & Trim(rMovAcr.tipoComisionFija), gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
					blIgnorar = True
					Exit Sub

			End Select
		End If


		If (rTrn.valMlComisPorcentual + rTrn.valCuoComisPorcentual) > 0 Then

			If IsNothing(rTrn.codMvtoComPor) Then
				rTrn.codMvtoComPor = rTipComPor.codMvtoComision
			End If
			Select Case True
				Case rTrn.codMvtoComPor = Nothing
					rTrn.codError = 15327					  'Codigo comision porcentual no informado
					GenerarLog("A", 15327, Nothing, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
					blIgnorar = True
					Exit Sub

				Case rTrn.codMvtoComPor <> rTipComPor.codMvtoComision
					rTrn.codError = 15328					  'Codigo comision porcentual incorrecto
					GenerarLog("A", 15328, Nothing, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
					blIgnorar = True
					Exit Sub

				Case rTrn.tipoComisionPorcentual = Nothing
					rTrn.codError = 15329					  'Tipo comision porcentual no informado
					GenerarLog("A", 15329, Nothing, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
					blIgnorar = True
					Exit Sub

				Case rTrn.tipoComisionPorcentual <> rMovAcr.tipoComisionPorcentual
					rTrn.codError = 15330					  'Tipo comision porcentual incorrecto
					GenerarLog("A", 15330, "Hebra " & gIdHebra & " - Tipo comision porcentual: " & Trim(rTrn.tipoComisionFija) & ", " & Trim(rMovAcr.tipoComisionFija), gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
					blIgnorar = True
					Exit Sub

			End Select

			'OS-5598016 se agregan Atributos en ACR_TRANSACCIONES de Exceo Empleador.
			If (rTrn.valMlExcesoEmpl + rTrn.valCuoExcesoEmpl) > 0 Then

				Select Case True
					Case rTrn.codMvtoExcEmpl = Nothing
						rTrn.codError = 15333
						GenerarLog("A", 15333, Nothing, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
						blIgnorar = True
						Exit Sub

				End Select
			End If
		End If
	End Sub


	Private Sub ValidarDatosBasicos()

		If (rTrn.codDestinoTransaccion = "TRF" Or rTrn.codDestinoTransaccion = "REZ") And _
		 rTrn.tipoImputacion = "CAR" Then
			blErrorFatal = True
			blIgnorar = True
			GenerarLog("E", 15332, "Hebra " & gIdHebra & " - Cargo a transferencia o rezago", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
			'Throw New SondaException(15332) '"Cargo a transferencia o rezago
		End If

		If rTrn.codMvto = Nothing And rTrn.codDestinoTransaccion <> "TRF" And rTrn.codOrigenTransaccion <> "TRF" Then
			blErrorFatal = True
			blIgnorar = True
			GenerarLog("E", 15332, "Hebra " & gIdHebra & " - Codigo movimiento no informado", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
			'Throw New SondaException(15333) '"Codigo movimiento no informado
		End If

		If (Mid(rTrn.codMvto, 2, 1) = "1" And rTrn.tipoImputacion <> "ABO") Or _
		 (Mid(rTrn.codMvto, 2, 1) = "2" And rTrn.tipoImputacion <> "CAR") Then
			blErrorFatal = True
			blIgnorar = True
			GenerarLog("E", 15334, "Hebra " & gIdHebra & " - CodigoMov/TipoImp: " & Trim(rTrn.codMvto) & ", " & Trim(rTrn.tipoImputacion), gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
			'Throw New SondaException(15334) '"Cargo/Abono incorrecto con codigo mvto
		End If

		dsAux = ParametrosINE.MvtoAcreditacion.traer(gdbc, gidAdm, rTrn.codMvto)
		dsAux = parCodMvto.traer(gdbc, New Object() {"VID_ADM", "VCOD_MVTO"}, New Object() {gidAdm, rTrn.codMvto}, New Object() {"INTEGER", "STRING"})

		If dsAux.Tables(0).Rows.Count = 0 Then
			rMovAcr = Nothing
			rMovAcr = New ccAcrMvtoAcreditacion(dsAux.Tables(0).NewRow)
			If rTrn.codDestinoTransaccion <> "TRF" And rTrn.codOrigenTransaccion <> "TRF" Then
				rTrn.codError = 15320				'No existe codigo movimiento
				GenerarLog("A", 15320, "Hebra " & gIdHebra & " - Codigo: " & Trim(rTrn.codMvto), gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
				blIgnorar = True
				Exit Sub
			End If
		Else
			rMovAcr = Nothing
			rMovAcr = New ccAcrMvtoAcreditacion(dsAux)
		End If

		If rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF" Then
			If rMovAcr.indNocional = "S" Then Me.blNocional = True
		End If

	End Sub


	Private Sub IgnorarRegistroTrn()
		blIgnorarCliente = True
		If Not IsNothing(sMov) Then
			sMov.clear()
		End If
		If Not IsNothing(clsAux) Then
			clsAux.Clear()
		End If
		If blPermiteAcreditacionParcial Then
			blIgnorarCliente = True
			gdbc.Rollback()
		End If
		Transacciones.modCausalRezagoConCommit(gdbc, gidAdm, rTrn.codOrigenProceso, gidUsuarioProceso, rTrn.numeroId, rTrn.seqRegistro, rTrn.codCausalRezagoCal, gidUsuarioProceso, gfuncion)
		Transacciones.modEstadoTransConCommit(gdbc, gidAdm, rTrn.codOrigenProceso, gidUsuarioProceso, rTrn.numeroId, rTrn.seqRegistro, "ER", rTrn.codError, gidUsuarioProceso, gfuncion)
		'SIS//
		gvalMlIgnorados = gvalMlMvto + gvalMlAdicional + gvalMlExceso + gvalMlComisiones + gvalMlPrimaSis
		gvalCuoIgnorados = gvalCuoMvto + gvalCuoAdicional + gvalCuoExceso + gvalCuoComisiones + gvalCuoPrimaSis

		gRegistrosIgnorados += 1
		gTotRegistrosIgnorados += 1

		'CrearTotalesAcredIgnorados()


	End Sub


	Private Sub ModificarRegistroTrn()


		rTrn.estadoTransaccion = gtipoProceso

		'lfc:// error comisiones
		If rTrn.perCotizacion >= New Date(2009, 7, 1).Date And (rTrn.tipoProducto = "CCO" Or rTrn.tipoProducto = "CAF") Then

			'solo trf de comisiones// para prima (SIS) sin trf
			IngresarAuxiliarComisiones2()

		Else
			IngresarAuxiliarComisiones()
		End If

		If gAdicionalSeTransfiere Then
			rTrn.indAdicTransf = 1
		Else
			rTrn.indAdicTransf = 0
		End If

		'rTrn.indAdicTransf = gAdicionalSeTransfiere

		Transacciones.modificar(gdbc, gidAdm, _
		  rTrn.codOrigenProceso, _
		  rTrn.usuarioProceso, _
		  rTrn.numeroId, _
		  rTrn.seqRegistro, _
		  rTrn.idPersona, _
		  rTrn.idCliente, _
		  rTrn.apPaterno, _
		  rTrn.apMaterno, _
		  rTrn.nombre, _
		  rTrn.nombreAdicional, _
		  rTrn.codSoundex, _
		  rTrn.perCotizacion, _
		  rTrn.numReferenciaOrigen1, _
		  rTrn.numReferenciaOrigen2, _
		  rTrn.numReferenciaOrigen3, _
		  rTrn.numReferenciaOrigen4, _
		  rTrn.numReferenciaOrigen5, _
		  rTrn.numReferenciaOrigen6, _
		  rTrn.tipoRemuneracion, _
		  rTrn.tipoPago, _
		  rTrn.tipoPlanilla, _
		  rTrn.tipoEntidadPagadora, _
		  rTrn.tipoCliente, _
		  rTrn.fecInicioGratificacion, _
		  rTrn.fecFinGratificacion, _
		  rTrn.numPeriodosCai, _
		  rTrn.fecOperacion, _
		  rTrn.fecOperacionAdmOrigen, _
		  rTrn.fecDeposito, _
		  rTrn.idEmpleador, _
		  rTrn.folioConvenio, _
		  rTrn.idAlternativoDoc, _
		  rTrn.numCuotasPactadas, _
		  rTrn.numCuotasPagadas, _
		  rTrn.valMlRentaImponible, _
		  rTrn.tipoProducto, _
		  rTrn.tipoFondoOrigen, _
		  rTrn.tipoFondoDestino, _
		  rTrn.categoria, _
		  rTrn.subCategoria, _
		  rTrn.tipoImputacion, _
		  rTrn.codOrigenTransaccion, _
		  rTrn.codDestinoTransaccion, _
		  rTrn.codDestinoTransaccionCal, _
		  rTrn.codOrigenRecaudacion, _
		  rTrn.seqMvtoOrigen, _
		  rTrn.seqMvtoDestino, _
		  rTrn.codOrigenMvto, _
		  rTrn.codMvto, _
		  rTrn.codMvtoAdi, _
		  rTrn.codMvtoIntreaCue, _
		  rTrn.codMvtoIntreaAdi, _
		  rTrn.codMvtoComPor, _
		  rTrn.codMvtoComFij, _
		  rTrn.idInstOrigen, _
		  rTrn.idInstDestino, _
		  rTrn.codCausalRezago, _
		  rTrn.codCausalRezagoCal, _
		  rTrn.tipoRezago, _
		  rTrn.codCausalAjuste, _
		  rTrn.fecValorCuotaVal, _
		  rTrn.valMlValorCuotaVal, _
		  rTrn.perContable, _
		  rTrn.fecAcreditacion, _
		  rTrn.fecValorCuota, _
		  rTrn.valMlValorCuota, _
		  rTrn.fecValorUfExceso, _
		  rTrn.fecValorCuotaCaja, _
		  rTrn.valMlValorCuotaCaja, _
		  rTrn.porcInstSalud, _
		  rTrn.valUfInstSalud, _
		  rTrn.tasaCotizacion, _
		  rTrn.tasaAdicional, _
		  rTrn.tasaInteres, _
		  rTrn.tasaReajuste, _
		  rTrn.indMontoPagado, _
		  rTrn.valMlMontoNominal, _
		  rTrn.valMlMvto, _
		  rTrn.valMlReajuste, _
		  rTrn.valMlInteres, _
		  rTrn.valCuoMvto, _
		  rTrn.valCuoReajuste, _
		  rTrn.valCuoInteres, _
		  rTrn.valMlAdicional, _
		  rTrn.valMlAdicionalReajuste, _
		  rTrn.valMlAdicionalInteres, _
		  rTrn.valCuoAdicional, _
		  rTrn.valCuoAdicionalReajuste, _
		  rTrn.valCuoAdicionalInteres, _
		  rTrn.tipoComisionPorcentual, _
		  rTrn.valMlComisPorcentual, _
		  rTrn.valCuoComisPorcentual, _
		  rTrn.valMlCuotaComision, _
		  rTrn.tipoComisionFija, _
		  rTrn.valMlComisFija, _
		  rTrn.valCuoComisFija, _
		  rTrn.tipoImputacionAdm, _
		  rTrn.valMlAporteAdm, _
		  rTrn.valCuoAporteAdm, _
		  rTrn.idInstSalud, _
		  rTrn.valMlSalud, _
		  rTrn.valCuoSalud, _
		  rTrn.valCuoTransferencia, _
		  rTrn.valMlTransferencia, _
		  rTrn.valMlExcesoLinea, _
		  rTrn.valCuoExcesoLinea, _
		  rTrn.tipoFondoDestinoCal, _
		  rTrn.valMlPatrFrecCal, _
		  rTrn.valCuoPatrFrecCal, _
		  rTrn.valCuoPatrFrecActCal, _
		  rTrn.valMlPatrFrecActCal, _
		  rTrn.valMlPatrFdesCal, _
		  rTrn.valCuoPatrFdesCal, _
		  rTrn.valMlMvtoCal, _
		  rTrn.valMlReajusteCal, _
		  rTrn.valMlInteresCal, _
		  rTrn.valCuoMvtoCal, _
		  rTrn.valCuoReajusteCal, _
		  rTrn.valCuoInteresCal, _
		  rTrn.valMlAdicionalCal, _
		  rTrn.valMlAdicionalInteresCal, _
		  rTrn.valMlAdicionalReajusteCal, _
		  rTrn.valCuoAdicionalCal, _
		  rTrn.valCuoAdicionalReajusteCal, _
		  rTrn.valCuoAdicionalInteresCal, _
		  rTrn.valMlComisPorcentualCal, _
		  rTrn.valCuoComisPorcentualCal, _
		  rTrn.seqComisionPorcentual, _
		  rTrn.valMlComisFijaCal, _
		  rTrn.valCuoComisFijaCal, _
		  rTrn.seqComisionFija, _
		  rTrn.codDestinoExcesoTopeCal, _
		  rTrn.seqDestinoExcesoTopeCal, _
		  rTrn.codMvtoExcesoTopeCal, _
		  rTrn.valMlExcesoTopeCal, _
		  rTrn.valCuoExcesoTopeCal, _
		  rTrn.codDestinoExcesoLineaCal, _
		  rTrn.seqDestinoExcesoLineaCal, _
		  rTrn.codMvtoExcesoLineaCal, _
		  rTrn.valMlExcesoLineaCal, _
		  rTrn.valCuoExcesoLineaCal, _
		  rTrn.valMlTransferenciaCal, _
		  rTrn.valCuoTransferenciaCal, _
		  rTrn.seqDestinoTrfCal, _
		  rTrn.valMlPrimaCal, _
		  rTrn.valMlIntPrimaCal, _
		  rTrn.valMlReaPrimaCal, _
		  rTrn.valCuoPrimaCal, _
		  rTrn.valIndPagoPrimCal, _
		  rTrn.valIdInstPagoPrimCal, _
		  rTrn.valMlCompensCal, _
		  rTrn.valCuoCompensCal, _
		  rTrn.seqDestinoCompenCal, _
		  rTrn.valMlAjusteDecimalCal, _
		  rTrn.valCuoAjusteDecimalCal, _
		  rTrn.numSaldo, _
		  rTrn.valMlSaldo, _
		  rTrn.valCuoSaldo, _
		  rTrn.seqMvtoSaldoAnterior, _
		  rTrn.valCuoSaldoAnterior, _
		  rTrn.valMlSaldoAnterior, _
		  rTrn.numRetiros, _
		  rTrn.codAjusteMovimiento, _
		  rTrn.indInsistenciaAcr, _
		  rTrn.indCierreProducto, _
		  rTrn.indMvtoVisibleCartola, _
		  rTrn.perCuatrimestre, _
		  rTrn.numDictamen, _
		  rTrn.codTrabajoPesado, _
		  rTrn.puestoTrabajoPesado, _
		  rTrn.indCobranza, _
		  rTrn.codError, _
		  rTrn.estadoTransaccion, _
		  gidUsuarioProceso, gfuncion, _
		  rTrn.sexo, rTrn.codMvtoPrim, rTrn.codMvtoPrimCar, rTrn.codMvtoIntreaPrim, rTrn.tasaPrima, rTrn.valMlPrimaSis, rTrn.valMlPrimaSisReajuste, _
		  rTrn.valMlPrimaSisInteres, rTrn.valCuoPrimaSis, rTrn.valCuoPrimaSisReajuste, rTrn.valCuoPrimaSisInteres, _
		  rTrn.valMlPrimaSisCal, rTrn.valMlPrimaSisReajusteCal, rTrn.valMlPrimaSisInteresCal, rTrn.valCuoPrimaSisCal, _
		  rTrn.valCuoPrimaSisReajusteCal, rTrn.valCuoPrimaSisInteresCal, rTrn.tasaPrimaCal, rTrn.seqPrima, rTrn.valMlRentaImponibleSis, rTrn.indAdicTransf, rTrn.codMvtoExcEmpl, rTrn.valMlExcesoEmpl, rTrn.valCuoExcesoEmpl, rTrn.valMlExcesoEmplCal, rTrn.valCuoExcesoEmplCal, rTrn.seqExcesoEmpl, rTrn.seqExcesoLinea, rTrn.tipoOrigenDigitacion, _
		   rTrn.valMlMvtoNoc, rTrn.valCuoMvtoNoc, rTrn.valMlComisionNoc, rTrn.valCuoComisionNoc)

		If gtipoProceso = "AC" Then

			'Modifica Tipo Trabajador. OS-5839109 21/02/2014
			If (rTrn.codOrigenProceso = "REREZSEL" Or rTrn.codOrigenProceso = "REREZMAS") And _
			 rTrn.codOrigenMvto = "ACREXTGR" And _
			 rTrn.tipoCliente = "IS" Then

				'Se cambia Tipo Trabajador en AAA_CLIENTES
				InformacionCliente.modTipoTrabajador(gdbc, gidAdm, rTrn.idCliente, "I", gidUsuarioProceso, gfuncion)

			End If
		End If

	End Sub



	Private Sub CerrarProducto()



		Dim indCierreTipoProducto As String

		If rTrn.codOrigenProceso = "TRAEGCTA" Or rTrn.codOrigenProceso = "TRAEGDES" Or _
		   rTrn.codOrigenProceso = "TRAEGAPV" Or rTrn.codOrigenProceso = "TRAEGEXT" Or _
		   rTrn.codOrigenProceso = "TRAEGCHP" Then

			indCierreTipoProducto = "S"
		Else

			indCierreTipoProducto = "N"
		End If

		If gtipoProceso = "AC" And rTrn.indCierreProducto = "C" Then

			If rTrn.valCuoSaldo > 0 Then

				blIgnorar = True
				rTrn.codError = 15335				'Saldo no es cero
				GenerarLog("A", 15335, Nothing, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
				Exit Sub

			End If

			If rTrn.codOrigenProceso = "RETCAIFO" Then

				InformacionCliente.cerrarSaldo(gdbc, gidAdm, rTrn.idCliente, rSal.numSaldo, gfecAcreditacion, gidUsuarioProceso, gfuncion)

			Else
				InformacionCliente.cerrarSaldosProducto(gdbc, gidAdm, rSal.idCliente, rSal.numProducto, gidUsuarioProceso, gfuncion)

				InformacionCliente.cerrarProducto(gdbc, gidAdm, rSal.idCliente, rSal.numProducto, rSal.tipoProducto, indCierreTipoProducto, gfecAcreditacion, rCli.codAdmDestino, gidUsuarioProceso, gfuncion)

				InformacionCliente.cerrarDistribucion(gdbc, gidAdm, rTrn.idCliente, rPro.numProducto, rDis.seqDistribucion, gidUsuarioProceso, gfuncion)

			End If


		End If

	End Sub
	Private Sub CerrarSaldo()

		If gtipoProceso = "AC" And rTrn.indCierreProducto = "C" And rSal.valCuoSaldo = 0 Then

			InformacionCliente.cerrarSaldo(gdbc, gidAdm, rTrn.idCliente, rSal.numSaldo, gfecAcreditacion, gidUsuarioProceso, gfuncion)
		End If

	End Sub
	Private Sub ReversarCierreProducto()
		Dim codError As Decimal
		Dim mensaje As String
		codError = InformacionCliente.reversarCierreCuenta(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.tipoFondoDestino, rTrn.categoria, gidUsuarioProceso, gfuncion)
		If codError <> 0 Then
			blIgnorar = True
			rTrn.codError = codError
			mensaje = "Hebra " & gIdHebra & " - " & rTrn.tipoProducto & ", " & rTrn.tipoFondoDestino & ", " & rTrn.categoria
			GenerarLog("E", codError, mensaje, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
			Exit Sub
		End If

	End Sub


	Private Sub RezagoAHistorico()

		'dsAux = Sys.IngresoEgreso.Rezagos.traer(gdbc,gidAdm, rTrn.seqMvtoOrigen)

		If rTrn.codOrigenProceso <> "REREZMAS" And _
		 rTrn.codOrigenProceso <> "REREZSEL" And _
		 rTrn.codOrigenProceso <> "REREZCON" Then
			If rTrn.codOrigenTransaccion <> "REZ" Or rTrn.tipoImputacion <> "CAR" Then
				Exit Sub
			End If
		End If

		If gtipoProceso = "AC" Then

			rTrn.codError = Sys.IngresoEgreso.Rezagos.cargoRezago(gdbc, gidAdm, rTrn.seqMvtoOrigen, gcodOrigenProceso, gperContable, gfecAcreditacion, gidUsuarioProceso, gfuncion)
		End If

		If rTrn.codError <> 0 And rTrn.codCausalAjuste <> "4" Then
			blIgnorar = True
			GenerarLog("A", rTrn.codError, "Hebra " & gIdHebra & " - Codigo error: " & rTrn.codError, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
		End If


	End Sub

	Private Sub DevolverRezagoAVigente()

		If gtipoProceso = "AC" Then
			'Planvital, no requiere la desmarca en las recuperaciones de rezagos
			If gcodAdministradora <> 1032 Then
				rTrn.codError = Sys.IngresoEgreso.Rezagos.retornarAPendiente(gdbc, gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId, rTrn.idCliente, gidUsuarioProceso, gfuncion)
			End If


		End If

		If rTrn.codError <> 0 Then
			blIgnorar = True
			GenerarLog("A", rTrn.codError, "Hebra " & gIdHebra & " - Codigo error: " & rTrn.codError, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
		End If
	End Sub

	Private Sub ValidarRezago()
		'dbc = New OraConn()
		'gidAdm = 1
		'Dim codcausal As String
		Dim NumRef As Long

		'INI OS-7826172. PCI 29/01/2016. Solo para Capital Pagos Anticipados. Se pasa Num Solicitud.
		If gcodAdministradora = 1033 And (rTrn.codOrigenProceso = "TRAINCAV" Or rTrn.codOrigenProceso = "TRAINAPV") Then
			NumRef = rTrn.numReferenciaOrigen2
		Else
			NumRef = rTrn.numReferenciaOrigen6
		End If


		dsValRez = ControlAcr.validarRezagos(gdbc, gidAdm, rTrn.codOrigenProceso, _
				  rTrn.idPersona, _
				  rTrn.idCliente, _
				  rTrn.perCotizacion, _
				  rTrn.tipoRemuneracion, _
				  rTrn.tipoPago, _
				  rTrn.tipoPlanilla, _
				  rTrn.tipoEntidadPagadora, _
				  rTrn.fecInicioGratificacion, _
				  rTrn.fecFinGratificacion, _
				  rTrn.fecOperacion, _
				  rTrn.fecOperacionAdmOrigen, _
				  rTrn.idEmpleador, _
				  rTrn.valMlRentaImponible, _
				  rTrn.tipoProducto, _
				  rTrn.tipoFondoOrigen, _
				  rTrn.tipoFondoDestinoCal, _
				  rTrn.categoria, _
				  rTrn.subCategoria, _
				  rTrn.codRegTributario, _
				  rTrn.numSaldo, _
				  rTrn.tipoImputacion, _
				  rTrn.codMvto, _
				  rTrn.codMvtoAdi, _
				  rTrn.codMvtoIntreaCue, _
				  rTrn.codMvtoIntreaAdi, _
				  rTrn.codMvtoComPor, _
				  rTrn.codMvtoComFij, _
				  rTrn.valMlMvto, _
				  rTrn.valMlReajuste, _
				  rTrn.valMlInteres, _
				  rTrn.valCuoMvto, _
				  rTrn.valCuoReajuste, _
				  rTrn.valCuoInteres, _
				  rTrn.valMlAdicional, _
				  rTrn.valMlAdicionalReajuste, _
				  rTrn.valMlAdicionalInteres, _
				  rTrn.valCuoAdicional, _
				  rTrn.valCuoAdicionalReajuste, _
				  rTrn.valCuoAdicionalInteres, _
				  rTrn.valMlComisFija, _
				  rTrn.valMlComisPorcentual, _
				  rTrn.valCuoComisFija, _
				  rTrn.valCuoComisPorcentual, _
				  gfecAcreditacion, _
				  rTrn.tasaCotizacion, _
				  rTrn.puestoTrabajoPesado, _
				  rTrn.codOrigenMvto, _
				  gidUsuarioProceso, _
				  gfuncion, _
				  rTrn.sexo, _
				  rTrn.numReferenciaOrigen1, _
				  NumRef)

		'If IsDBNull(dsValRez.Tables(0).Rows(0).Item("COD_CAUSAL_REZAGO")) Then
		'    codcausal = 0
		'Else
		'    codcausal = dsValRez.Tables(0).Rows(0).Item("COD_CAUSAL_REZAGO")
		'End If

	End Sub

	Private Sub TrnARez()

		rRez.folioConvenio = 0
		If rTrn.codOrigenProceso = "RECAUDAC" Then
			rRez.numPlanilla = rTrn.numReferenciaOrigen1
			rRez.folioPlanilla = rTrn.numReferenciaOrigen6
			rRez.numPagina = rTrn.numReferenciaOrigen3
			rRez.seqLinea = rTrn.numReferenciaOrigen4

			rRez.codAdmOrigen = gcodAdministradora
		Else
			rRez.numPlanilla = 0
			rRez.folioPlanilla = 0
			rRez.numPagina = 0
			rRez.seqLinea = 0
			rRez.codAdmOrigen = rTrn.idInstOrigen
		End If

		dsAux = parCodMvtoRez.traer(gdbc, New Object() {"VID_ADM", "VCOD_MVTO"}, New Object() {gidAdm, rTrn.codMvto}, New Object() {"INTEGER", "STRING"})

		If dsAux.Tables(0).Rows.Count = 0 Then
			rRez.codMvto = rTrn.codMvto
			rRez.codMvtoPrim = rTrn.codMvtoPrim
			rRez.codMvtoIntreaPrim = rTrn.codMvtoIntreaPrim
		Else

			rRez.codMvto = dsAux.Tables(0).Rows(0).Item("COD_MVTO_REZAGO")

			Dim dsMovAcr As New DataSet()
			dsMovAcr = parCodMvto.traer(gdbc, New Object() {"VID_ADM", "VCOD_MVTO"}, New Object() {gidAdm, rRez.codMvto}, New Object() {"INTEGER", "STRING"})

			If dsMovAcr.Tables(0).Rows.Count > 0 Then
				Dim rCodMvto As ccAcrMvtoAcreditacion
				rCodMvto = New ccAcrMvtoAcreditacion(dsMovAcr)
				rRez.codMvtoPrim = rCodMvto.codMvtoPrim
				rRez.codMvtoIntreaPrim = rCodMvto.codMvtoIntreaPrim
			Else
				rRez.codMvtoPrim = rTrn.codMvtoPrim
				rRez.codMvtoIntreaPrim = rTrn.codMvtoIntreaPrim
			End If
		End If

		rRez.numRezago = rTrn.seqMvtoDestino
		rRez.perCotiza = rTrn.perCotizacion
		rRez.tipoProducto = rTrn.tipoProducto
		rRez.tipoFondo = rTrn.tipoFondoDestinoCal

		rRez.tipoPago = rTrn.tipoPago
		rRez.tipoPlanilla = rTrn.tipoPlanilla
		rRez.tipoRemuneracion = rTrn.tipoRemuneracion
		rRez.tipoEntidadPagadora = rTrn.tipoEntidadPagadora
		rRez.fecOperacion = rTrn.fecOperacion
		rRez.fecInicioGratificacion = rTrn.fecInicioGratificacion
		rRez.fecFinGratificacion = rTrn.fecFinGratificacion
		rRez.idCliente = rTrn.idCliente
		rRez.idEmpleadorOri = rTrn.idEmpleador
		rRez.idPersonaOri = rTrn.idPersona
		rRez.apPaternoOri = rTrn.apPaterno
		rRez.apMaternoOri = rTrn.apMaterno
		rRez.nombreOri = rTrn.nombre
		rRez.nombreAdicionalOri = rTrn.nombreAdicional
		rRez.codigoSoundex = rTrn.codSoundex
		rRez.idEmpleador = rTrn.idEmpleador
		rRez.idPersona = rTrn.idPersona
		rRez.apPaterno = rTrn.apPaterno
		rRez.apMaterno = rTrn.apMaterno
		rRez.nombre = rTrn.nombre
		rRez.nombreAdicional = rTrn.nombreAdicional

		'OS-5681735 Se Agrega Rta Imponible Cuando es 0, se toma Rta Imponible SIS.
		'rRez.valMlRentaImponible = rTrn.valMlRentaImponible
		If rTrn.codOrigenMvto = "RECAUDAC" Then
			If rTrn.valMlRentaImponible = 0 And rTrn.valMlRentaImponibleSis > 0 Then			 'Solo Prima
				rRez.valMlRentaImponible = rTrn.valMlRentaImponibleSis
			ElseIf rTrn.valMlRentaImponible > 0 And rTrn.valMlRentaImponibleSis > 0 Then			 'Cot + Prima
				rRez.valMlRentaImponible = rTrn.valMlRentaImponible
			ElseIf rTrn.valMlRentaImponible > 0 And rTrn.valMlRentaImponibleSis = 0 Then			 'CobPrima o Cot
				rRez.valMlRentaImponible = rTrn.valMlRentaImponible
			End If
		Else
			rRez.valMlRentaImponible = rTrn.valMlRentaImponible
		End If

		rRez.valMlMontoNominal = rTrn.valMlMontoNominal
		rRez.valMlMonto = rTrn.valMlMvtoCal

		rRez.valMlInteres = rTrn.valMlInteresCal
		rRez.valMlReajuste = rTrn.valMlReajusteCal

		rRez.valCuoMonto = rTrn.valCuoMvtoCal


		'If blRentabilidadRez Then
		'    rRez.valMlMonto += rTrn.valMlAporteAdm
		'    rRez.valCuoMonto += rTrn.valCuoAporteAdm
		'End If



		rRez.valCuoInteres = rTrn.valCuoInteresCal
		rRez.valCuoReajuste = rTrn.valCuoReajusteCal
		rRez.valMlAdicional = rTrn.valMlAdicionalCal
		rRez.valMlAdicionalInteres = rTrn.valMlAdicionalInteresCal
		rRez.valMlAdicionalReajuste = rTrn.valMlAdicionalReajusteCal
		rRez.valCuoAdicional = rTrn.valCuoAdicionalCal
		rRez.valCuoAdicionalInteres = rTrn.valCuoAdicionalInteresCal
		rRez.valCuoAdicionalReajuste = rTrn.valCuoAdicionalReajusteCal

		rRez.fecValorCuota = rTrn.fecValorCuota
		rRez.valMlValorCuota = rTrn.valMlValorCuota

		rRez.codOrigenMvto = rTrn.codOrigenMvto
		rRez.codOrigenProceso = rTrn.codOrigenProceso
		rRez.fecContableRezago = rTrn.fecAcreditacion
		rRez.fecOperacionAdmOrigen = rTrn.fecOperacionAdmOrigen
		rRez.perContableRezago = rTrn.perContable
		rRez.codCausalRezago = rTrn.codCausalRezagoCal
		rRez.codCausalOriginal = rTrn.codCausalRezagoCal

		rRez.numDictamenOri = rTrn.numDictamen
		rRez.fecNotificacion = Nothing
		rRez.codTrabajoPesado = rTrn.codTrabajoPesado
		rRez.puestoTrabajoPesado = rTrn.puestoTrabajoPesado
		rRez.tasaTrabajoPesadoOri = 0
		If rMovAcr.tipoMvto = "COP" Then
			rRez.tasaTrabajoPesado = rTrn.tasaCotizacion
		Else
			rRez.tasaTrabajoPesado = 0
		End If
		rRez.estadoRezago = "V"
		rRez.fecEstadoRezago = gfecAcreditacion
		rRez.estadoReg = Nothing
		rRez.fecEstadoReg = Nothing
		rRez.idUsuarioIngReg = gidUsuarioProceso
		rRez.fecIngReg = Nothing
		rRez.idUsuarioUltModifReg = gidUsuarioProceso
		rRez.fecUltModifReg = Nothing
		rRez.idFuncionUltModifReg = gfuncion

		'--.--lfc - cn2
		'PCI 'Se marca en REZAGOS reg. Tributario de la CAV. 30/05/2012. OS-4377528
		'rRez.indRegimenTributario = rTrn.codRegTributario
		'CAVCAI
		If rTrn.tipoProducto = "CAV" Then

			If rTrn.categoria = "GENERAL" Then
				rRez.indRegimenTributario = "A"
			ElseIf rTrn.categoria = "OPCIONAL" Then
				rRez.indRegimenTributario = "B"
			ElseIf Not IsNothing(rTrn.categoria) Then
				rRez.indRegimenTributario = "B"
			End If
		Else
			rRez.indRegimenTributario = rTrn.codRegTributario
		End If
		'PCI FIN

		'CAV. NCG 133 19/06/2015.
		If rTrn.tipoProducto = "CAV" Then
			Dim ds As DataSet
			ds = ParametrosINE.ParametrosGenerales.BuscaRegTribut(gdbc, gidAdm, rTrn.tipoProducto, rTrn.categoria)
			If ds.Tables(0).Rows.Count > 0 Then
				rRez.indRegimenTributario = IIf(IsDBNull(ds.Tables(0).Rows(0).Item("COD_REG_TRIBUTARIO")), "RTCAV2", ds.Tables(0).Rows(0).Item("COD_REG_TRIBUTARIO"))
			Else
				rRez.indRegimenTributario = "RTCAV2"
			End If
		Else
			rRez.indRegimenTributario = rTrn.codRegTributario
		End If
		'CAV. NCG 133 19/06/2015.

		rRez.numContrato = rTrn.subCategoria
		rRez.categoria = rTrn.categoria

		If gEsConvenio Then
			' Define Atributos cuando es CONVENIO
			rRez.folioConvenio = rTrn.folioConvenio
			rRez.numCuotasPactadas = rTrn.numCuotasPactadas
			rRez.numCuotasPagadas = rTrn.numCuotasPagadas
		End If


		'SIS//
		'rRez.codMvtoPrim = rTrn.codMvtoPrim -- ESTE VALOR SE CALCULA
		'rRez.codMvtoIntreaPrim = rTrn.codMvtoIntreaPrim --ESTE VALOR SE CALCULA
		rRez.sexo = rTrn.sexo
		rRez.valMlRentaImponibleSis = rTrn.valMlRentaImponibleSis

		rRez.valMlPrimaseguro = rTrn.valMlPrimaSisCal
		rRez.valMlPrimaseguroInt = rTrn.valMlPrimaSisInteresCal
		rRez.valMlPrimaseguroRea = rTrn.valMlPrimaSisReajusteCal
		rRez.tasaPrima = rTrn.tasaPrima
		rRez.valCuoPrimaseguro = rTrn.valCuoPrimaSisCal		  ' no valorizado-->>>
		rRez.valCuoPrimaseguroInt = rTrn.valCuoPrimaSisInteresCal
		rRez.valCuoPrimaseguroRea = rTrn.valCuoPrimaSisReajusteCal
		'rRez.valMlExcesoAfi = rTrn.valMlExcesoLinea
		rRez.valMlExcesoAfi = 0
		rRez.valMlExcesoEmp = 0

		rRez.tipoOrigenDigitacion = rTrn.tipoOrigenDigitacion

		'--SOLICITADO mUT
		If Not (rTrn.tipoRezago Is Nothing) And rTrn.codOrigenProceso = "TRAIPAGN" Then
			rRez.tipoRezago = rTrn.tipoRezago
			Exit Sub
		End If
		'--.-- CA-2008120252 --13-03-09
		dsAux = Sys.IngresoEgreso.Rezagos.buscarAfiliadoTipoRez(gdbc, gidAdm, rTrn.idPersona)

		Dim fecOperacionCal As Date = rRez.fecOperacion		  'modificacion MUT 23/02/2010
		If rTrn.codOrigenProceso = "TRAINREZ" Or _
		   rTrn.codOrigenProceso = "TRAINRZC" Or _
		   rTrn.codOrigenProceso = "TRAIPAGN" Or _
		   rTrn.codOrigenProceso = "TRAIPAGC" Then
			fecOperacionCal = rTrn.fecOperacionAdmOrigen
		End If

		Dim OBJ As Object

		OBJ = rTrn.codOrigenProceso
		OBJ = rTrn.tipoRezago

		'cambios TGR-nueva estrcutura
		If rTrn.codOrigenProceso = "ACREXTGR" And rTrn.tipoRezago = "39" Then
			rRez.tipoRezago = rTrn.tipoRezago
		Else

			'OS-7601976 y OS-7911929. Normativa 145 Solo para MODELO y CAPITAL
			If dsAux.Tables(0).Rows.Count = 0 Then

				rRez.tipoRezago = Sys.IngresoEgreso.Rezagos.calcularTipoRezago(gdbc, gidAdm, _
					 rTrn.idCliente, _
					 Nothing, _
					 Nothing, _
					 rTrn.perCotizacion, _
					 fecOperacionCal, _
					 rRez.tasaTrabajoPesado, _
					 rRez.codMvto, _
					 rRez.numPlanilla, _
					 rRez.numPagina, _
					 rRez.seqLinea, _
					 rTrn.tipoPlanilla, _
					 rRez.tipoProducto, _
					  rRez.valMlAdicional, _
					  rRez.valMlAdicionalInteres, _
					  rRez.valMlAdicionalReajuste, _
				rRez.valMlMonto, _
				rRez.valMlInteres, _
				rRez.valMlReajuste, _
				rRez.valMlPrimaseguro, _
				rRez.valMlPrimaseguroInt, _
				rRez.valMlPrimaseguroRea, _
				rRez.valMlComision, _
				rRez.valCuoAdicional, _
				rRez.valCuoAdicionalInteres, _
				rRez.valCuoAdicionalReajuste, _
				rRez.valCuoMonto, _
				rRez.valCuoInteres, _
				rRez.valCuoReajuste, _
				rRez.valCuoPrimaseguro, _
				rRez.valCuoPrimaseguroInt, _
				rRez.valCuoPrimaseguroRea, _
				rRez.valCuoComision, _
				   rRez.codCausalRezago)
			Else
				rCli = New ccClientes(dsAux.Tables(0))
				'Dim aux As New DataSet()
				'aux = Sys.IngresoEgreso.Rezagos.calcularTipoRezago(gdbc, gidAdm, _
				rRez.tipoRezago = Sys.IngresoEgreso.Rezagos.calcularTipoRezago(gdbc, gidAdm, _
					  rCli.idCliente, _
					  rCli.fecEstadoAfiliado, _
					  rCli.fecAfiliacionAdm, _
					  rTrn.perCotizacion, _
					  fecOperacionCal, _
					  rRez.tasaTrabajoPesado, _
					  rRez.codMvto, _
					  rRez.numPlanilla, _
					  rRez.numPagina, _
					  rRez.seqLinea, _
					  rTrn.tipoPlanilla, _
					  rRez.tipoProducto, _
					  rRez.valMlAdicional, _
					  rRez.valMlAdicionalInteres, _
					  rRez.valMlAdicionalReajuste, _
				 rRez.valMlMonto, _
				rRez.valMlInteres, _
				rRez.valMlReajuste, _
				rRez.valMlPrimaseguro, _
				rRez.valMlPrimaseguroInt, _
				rRez.valMlPrimaseguroRea, _
				rRez.valMlComision, _
				rRez.valCuoAdicional, _
				rRez.valCuoAdicionalInteres, _
				rRez.valCuoAdicionalReajuste, _
				rRez.valCuoMonto, _
				rRez.valCuoInteres, _
				rRez.valCuoReajuste, _
				rRez.valCuoPrimaseguro, _
				rRez.valCuoPrimaseguroInt, _
				rRez.valCuoPrimaseguroRea, _
				rRez.valCuoComision, _
				   rRez.codCausalRezago)

				rRez.sexo = rCli.sexo
			End If
		End If

		''Solo para PLV
		'If dsAux.Tables(0).Rows.Count = 0 Then
		'    rRez.tipoRezago = Sys.IngresoEgreso.Rezagos.calcularTipoRezago(gdbc, gidAdm, _
		'                                                                  rTrn.idCliente, _
		'                                                                  Nothing, _
		'                                                                  Nothing, _
		'                                                                  rTrn.perCotizacion, _
		'                                                                  fecOperacionCal, _
		'                                                                  rRez.tasaTrabajoPesado, _
		'                                                                  rRez.codMvto, _
		'                                                                  rRez.numPlanilla, _
		'                                                                  rRez.numPagina, _
		'                                                                  rRez.seqLinea, _
		'                                                                  rTrn.tipoPlanilla, _
		'                                                                  rRez.tipoProducto, _
		'                                                                  rRez.valCuoAdicional, _
		'                                                                  rRez.valCuoAdicionalInteres, _
		'                                                                  rRez.valCuoAdicionalReajuste, _
		'                                                                  rRez.valCuoMonto, _
		'                                                                  rRez.valCuoInteres, _
		'                                                                  rRez.valCuoReajuste, _
		'                                                                  rRez.codCausalRezago)
		'Else
		'    rCli = New ccClientes(dsAux.Tables(0))
		'    rRez.tipoRezago = Sys.IngresoEgreso.Rezagos.calcularTipoRezago(gdbc, gidAdm, _
		'                                                                   rCli.idCliente, _
		'                                                                   rCli.fecEstadoAfiliado, _
		'                                                                   rCli.fecAfiliacionAdm, _
		'                                                                   rTrn.perCotizacion, _
		'                                                                   fecOperacionCal, _
		'                                                                   rRez.tasaTrabajoPesado, _
		'                                                                   rRez.codMvto, _
		'                                                                   rRez.numPlanilla, _
		'                                                                   rRez.numPagina, _
		'                                                                   rRez.seqLinea, _
		'                                                                   rTrn.tipoPlanilla, _
		'                                                                   rRez.tipoProducto, _
		'                                                                   rRez.valCuoAdicional, _
		'                                                                   rRez.valCuoAdicionalInteres, _
		'                                                                   rRez.valCuoAdicionalReajuste, _
		'                                                                   rRez.valCuoMonto, _
		'                                                                   rRez.valCuoInteres, _
		'                                                                   rRez.valCuoReajuste, _
		'                                                                   rRez.codCausalRezago)

		'    rRez.sexo = rCli.sexo
		'End If

		If rRez.tipoRezago Is Nothing Or rRez.tipoRezago = 0 Then rRez.tipoRezago = rTrn.tipoRezago

		'NCG 145. 28/08/2015.
		If rTrn.codOrigenProceso = "ACREXAFC" Then
			rRez.tipoRezago = 37
		End If

		'RRS - 10/05/2020 - CA-6570158 - CA-6674175 - CA-6766871 - CA-6677076 - Nuevo Tipo rezago 40
		If (rTrn.codOrigenProceso = "RECAUDAC" And rTrn.idPersona = rRez.idEmpleador _
		 And rRez.tipoEntidadPagadora = "V" And rTrn.perCotizacion >= "01/01/2020" _
		 And rRez.tipoProducto = "CCO") Then

			Select Case rTrn.tipoPago
				Case 1
					rTrn.codMvto = 111401
				Case 2
					rTrn.codMvto = 111409
				Case Else
					rTrn.codMvto = 111405
			End Select
			rRez.tipoRezago = "40"

		End If


		'RRS - 10/05/2020 - CA-6570158 - CA-6674175 - CA-6766871 - CA-6677076 - Nuevo Tipo rezago 40
		If (rTrn.codOrigenProceso = "RECAUDAC" And rTrn.idPersona = rRez.idEmpleador _
		 And rRez.tipoEntidadPagadora = "V" And rTrn.perCotizacion >= "01/01/2020" _
		 And rRez.tipoProducto = "CCO") Then
			rTrn.codMvto = 110101
			rRez.tipoRezago = "40"
		End If


		rTrn.tipoRezago = rRez.tipoRezago
	End Sub

	Private Sub TrnAMov()
		Dim i As Integer

		sMov = New INEMovimiento(rSal.valMlSaldo, rSal.valCuoSaldo)

		If rTrn.indMvtoVisibleCartola = Nothing Or rTrn.indMvtoVisibleCartola = Space(1) Then
			rTrn.indMvtoVisibleCartola = "S"
		End If

		sMov.add(dsMov, rTrn.tipoImputacion, 0)
		i = sMov.count - 1

		sMov.item(i).mov.idCliente = rTrn.idCliente
		sMov.item(i).mov.numSaldo = rTrn.numSaldo
		sMov.item(i).mov.seqMvto = rTrn.seqMvtoDestino
		sMov.item(i).mov.seqRegistroTransaccion = rTrn.seqRegistro
		sMov.item(i).mov.tipoProducto = rTrn.tipoProducto
		sMov.item(i).mov.tipoFondo = rTrn.tipoFondoDestinoCal
		sMov.item(i).mov.perCotizacion = rTrn.perCotizacion
		sMov.item(i).mov.tipoRemuneracion = rTrn.tipoRemuneracion
		sMov.item(i).mov.tipoPago = rTrn.tipoPago
		sMov.item(i).mov.tipoPlanilla = rTrn.tipoPlanilla
		sMov.item(i).mov.tipoEntPagadora = rTrn.tipoEntidadPagadora
		sMov.item(i).mov.numReferenciaOrigen1 = rTrn.numReferenciaOrigen1
		sMov.item(i).mov.numReferenciaOrigen2 = rTrn.numReferenciaOrigen2
		sMov.item(i).mov.numReferenciaOrigen3 = rTrn.numReferenciaOrigen3
		sMov.item(i).mov.numReferenciaOrigen4 = rTrn.numReferenciaOrigen4
		sMov.item(i).mov.numReferenciaOrigen5 = rTrn.numReferenciaOrigen5
		sMov.item(i).mov.numReferenciaOrigen6 = rTrn.numReferenciaOrigen6
		sMov.item(i).mov.fecOperacion = rTrn.fecOperacion
		sMov.item(i).mov.idEmpleador = rTrn.idEmpleador

		'OS-5180479 Cuando Origen es RECAUDAC se asume Rta Imp. Sis si no existe 
		' se toma rta Imponible Sola. 20/08/2013.

		'OS-5381224 Se agrega renta Impon. para COBPRIMA.

		If rTrn.codOrigenMvto = "RECAUDAC" Or rTrn.codOrigenMvto = "COBPRIMA" Then
			If rTrn.valMlRentaImponible = 0 And rTrn.valMlRentaImponibleSis > 0 Then			 'Solo Prima
				sMov.item(i).mov.valMlRentaImponible = rTrn.valMlRentaImponibleSis
			ElseIf rTrn.valMlRentaImponible > 0 And rTrn.valMlRentaImponibleSis > 0 Then			 'Cot + Prima
				sMov.item(i).mov.valMlRentaImponible = rTrn.valMlRentaImponible
			ElseIf rTrn.valMlRentaImponible > 0 And rTrn.valMlRentaImponibleSis = 0 Then			 'CobPrima o Cot
				sMov.item(i).mov.valMlRentaImponible = rTrn.valMlRentaImponible
			End If
		Else
			sMov.item(i).mov.valMlRentaImponible = rTrn.valMlRentaImponible
		End If

		If gcodAdministradora = 1032 And blGenExcesoEnLinea And valMlRIMCotExcesoGen > 0 And gExesoTope Then
			If sMov.item(i).mov.valMlRentaImponible - valMlRIMCotExcesoGen >= 0 Then
				sMov.item(i).mov.valMlRentaImponible = rTrn.valMlRentaImponible - valMlRIMCotExcesoGen
			ElseIf sMov.item(i).mov.valMlRentaImponible - valMlRIMCotExcesoGen < 0 Then
				sMov.item(i).mov.valMlRentaImponible = valMlRIMCotExcesoGen - rTrn.valMlRentaImponible
			End If
		End If

		'sMov.item(i).mov.valMlRentaImponible = rTrn.valMlRentaImponible
		sMov.item(i).mov.fecAcreditacion = rTrn.fecAcreditacion
		sMov.item(i).mov.perContable = rTrn.perContable
		sMov.item(i).mov.seqMvtoOrigen = rTrn.seqMvtoOrigen
		sMov.item(i).mov.codOrigenMvto = rTrn.codOrigenMvto
		sMov.item(i).mov.codOrigenProceso = rTrn.codOrigenProceso
		sMov.item(i).mov.codMvto = rTrn.codMvto
		sMov.item(i).mov.codMvtoAdi = rTrn.codMvtoAdi
		sMov.item(i).mov.codMvtoIntreaCue = rTrn.codMvtoIntreaCue
		sMov.item(i).mov.codMvtoIntreaAdi = rTrn.codMvtoIntreaAdi
		sMov.item(i).mov.codMvtoComPor = rTrn.codMvtoComPor
		sMov.item(i).mov.codMvtoComFij = rTrn.codMvtoComFij
		'sMov.item(i).mov.idAdmOrigen = rTrn.idInstOrigen

		'OS-8857288 - KRB - 10/06/2016 - Guardar el código de la AFP Destino en la tabla ACR_SALDOS_MOVIMIENTOS en el Traspaso Egreso por Canje  --->>>>>>--- INICIO
		If (rTrn.codOrigenProceso = "TRAEGCTA" Or _
		 rTrn.codOrigenProceso = "TRAEGCAV" Or _
		 rTrn.codOrigenProceso = "TRAEPAGN" Or _
		 rTrn.codOrigenProceso = "TRAEGCHP" Or _
		 rTrn.codOrigenProceso = "TRAEGDES") Then
			sMov.item(i).mov.idAdmOrigen = rTrn.idInstDestino
		Else
			sMov.item(i).mov.idAdmOrigen = rTrn.idInstOrigen
		End If
		'OS-8857288 - KRB - 10/06/2016 ---<<<<<<--- FIN

		sMov.item(i).mov.fecValorCuota = rTrn.fecValorCuota
		sMov.item(i).mov.valMlValorCuota = rTrn.valMlValorCuota

		If rMovAcr.tipoMvto = "NOC" Then		'rmovacr.tipomvto="NOC"
			sMov.item(i).mov.valMlMvto = rTrn.valMlMvtoNoc
			sMov.item(i).mov.valCuoMvto = rTrn.valCuoMvtoNoc

			sMov.item(i).mov.valMlComisionPorcentual = rTrn.valMlComisionNoc
			sMov.item(i).mov.valCuoComisionPorcentual = rTrn.valCuoComisionNoc
		Else


			sMov.item(i).mov.valMlMvto = rTrn.valMlMvtoCal
			sMov.item(i).mov.valMlMvtoInteres = rTrn.valMlInteresCal
			sMov.item(i).mov.valMlMvtoReajuste = rTrn.valMlReajusteCal
			sMov.item(i).mov.valCuoMvto = rTrn.valCuoMvtoCal
			sMov.item(i).mov.valCuoMvtoInteres = rTrn.valCuoInteresCal
			sMov.item(i).mov.valCuoMvtoReajuste = rTrn.valCuoReajusteCal
			sMov.item(i).mov.valMlAdicional = rTrn.valMlAdicionalCal
			sMov.item(i).mov.valMlAdicionalInteres = rTrn.valMlAdicionalInteresCal
			sMov.item(i).mov.valMlAdicionalReajuste = rTrn.valMlAdicionalReajusteCal
			sMov.item(i).mov.valCuoAdicional = rTrn.valCuoAdicionalCal
			sMov.item(i).mov.valCuoAdicionalInteres = rTrn.valCuoAdicionalInteresCal
			sMov.item(i).mov.valCuoAdicionalReajuste = rTrn.valCuoAdicionalReajusteCal

			sMov.item(i).mov.valMlComisionPorcentual = rTrn.valMlComisPorcentualCal
			sMov.item(i).mov.valCuoComisionPorcentual = rTrn.valCuoComisPorcentualCal

		End If

		sMov.item(i).mov.valMlCuotaComision = rTrn.valMlCuotaComision

		sMov.item(i).mov.valMlComisionFija = rTrn.valMlComisFijaCal
		sMov.item(i).mov.valCuoComisionFija = rTrn.valCuoComisFijaCal

		'sMov.item(i).mov.valMlSaldo = rTrn.valMlSaldo
		'sMov.item(i).mov.valCuoSaldo = rTrn.valCuoSaldo

		sMov.item(i).mov.numDictamen = rTrn.numDictamen
		sMov.item(i).mov.indMvtoVisibleCartola = rTrn.indMvtoVisibleCartola
		sMov.item(i).mov.perCuatrimestre = rTrn.perCuatrimestre
		sMov.item(i).mov.idUsuarioIngReg = gidUsuarioProceso
		sMov.item(i).mov.idFuncionUltModifReg = gidUsuarioProceso
		sMov.item(i).mov.tipoOrigenDigitacion = rTrn.tipoOrigenDigitacion

		'lfc: 17-05-2021 - estadistica
		sMov.item(i).mov.claseCotizante = rTrn.claseCotizante
		sMov.item(i).mov.codActividadEconomica = rTrn.codActividadEconomica

	End Sub


	Private Sub TrnAMovSis(ByVal tipoImputacion As String)
		Dim i As Integer

		' sMov.add(dsMov, rTrn.tipoImputacion, 1)  --- de debe realizar ABO
		sMov.add(dsMov, tipoImputacion, 4)
		i = sMov.count - 1

		sMov.item(i).mov.idCliente = rTrn.idCliente
		sMov.item(i).mov.numSaldo = rTrn.numSaldo
		sMov.item(i).mov.seqMvto = 0
		sMov.item(i).mov.seqRegistroTransaccion = rTrn.seqRegistro
		sMov.item(i).mov.tipoProducto = rTrn.tipoProducto
		sMov.item(i).mov.tipoFondo = rTrn.tipoFondoDestinoCal
		sMov.item(i).mov.perCotizacion = rTrn.perCotizacion
		sMov.item(i).mov.tipoRemuneracion = rTrn.tipoRemuneracion
		sMov.item(i).mov.tipoPago = rTrn.tipoPago
		sMov.item(i).mov.tipoPlanilla = rTrn.tipoPlanilla
		sMov.item(i).mov.tipoEntPagadora = rTrn.tipoEntidadPagadora
		sMov.item(i).mov.numReferenciaOrigen1 = rTrn.numReferenciaOrigen1
		sMov.item(i).mov.numReferenciaOrigen2 = rTrn.numReferenciaOrigen2
		sMov.item(i).mov.numReferenciaOrigen3 = rTrn.numReferenciaOrigen3
		sMov.item(i).mov.numReferenciaOrigen4 = rTrn.numReferenciaOrigen4
		sMov.item(i).mov.numReferenciaOrigen5 = rTrn.numReferenciaOrigen5
		sMov.item(i).mov.numReferenciaOrigen6 = rTrn.numReferenciaOrigen6

		'fecha Operacion = fec Acreditacion. OS-3798863 MOD-2011070125
		sMov.item(i).mov.fecOperacion = rTrn.fecOperacion
		'If gcodAdministradora = 1034 And (gcodOrigenProceso = "REREZSEL" Or gcodOrigenProceso = "REREZMAS" Or gcodOrigenProceso = "REREZCON") Then
		'    sMov.item(i).mov.fecOperacion = rTrn.fecAcreditacion
		'Else
		'    sMov.item(i).mov.fecOperacion = rTrn.fecOperacion
		'End If

		sMov.item(i).mov.idEmpleador = rTrn.idEmpleador

		'Cuando se paga solo Prima se debe asignar ValMlRentaImponiblie ya que ValMlRentaImponible = 0 OS-4235417
		If (rTrn.valMlMvtoCal + rTrn.valMlReajusteCal + rTrn.valMlInteresCal + rTrn.valMlAdicionalCal + rTrn.valMlAdicionalInteresCal + rTrn.valMlAdicionalReajusteCal) = 0 Then
			sMov.item(i).mov.valMlRentaImponible = rTrn.valMlRentaImponibleSis
		Else
			If rTrn.codOrigenProceso = "RECAUDAC" Then
				sMov.item(i).mov.valMlRentaImponible = rTrn.valMlRentaImponibleSis
			ElseIf rTrn.codOrigenProceso = "RECAUDAC" Or rTrn.codOrigenProceso = "REREZMAS" Or rTrn.codOrigenProceso = "REREZSEL" Then
				sMov.item(i).mov.valMlRentaImponible = rTrn.valMlRentaImponibleSis
			Else
				'lfc:cambios TGR 04-07-2017
				If rTrn.codOrigenProceso = "ACREXTGR" Or (rTrn.tipoRezago = 35 Or rTrn.tipoRezago = 36 Or rTrn.tipoRezago = 39) Then
					sMov.item(i).mov.valMlRentaImponible = rTrn.valMlRentaImponibleSis
				Else
					sMov.item(i).mov.valMlRentaImponible = rTrn.valMlRentaImponible
				End If

			End If
		End If

		If gcodAdministradora = 1032 And blGenExcesoEnLinea And valMlRIMSISExcesoGen > 0 And gExesoTopeSis Then
			If sMov.item(i).mov.valMlRentaImponible - valMlRIMSISExcesoGen >= 0 Then
				'LFC:COMENTA 15-03 sMov.item(i).mov.valMlRentaImponible = sMov.item(i).mov.valMlRentaImponible - valMlRIMSISExcesoGen
				sMov.item(i).mov.valMlRentaImponible = rTrn.valMlRentaImponibleSis - valMlRIMSISExcesoGen
			ElseIf sMov.item(i).mov.valMlRentaImponible - valMlRIMSISExcesoGen < 0 Then
				'LFC:COMENTA 15-03 sMov.item(i).mov.valMlRentaImponible = valMlRIMSISExcesoGen - sMov.item(i).mov.valMlRentaImponible
				sMov.item(i).mov.valMlRentaImponible = valMlRIMSISExcesoGen - rTrn.valMlRentaImponibleSis
			End If
		End If


		sMov.item(i).mov.fecAcreditacion = rTrn.fecAcreditacion
		sMov.item(i).mov.perContable = rTrn.perContable
		sMov.item(i).mov.seqMvtoOrigen = rTrn.seqMvtoOrigen
		sMov.item(i).mov.codOrigenMvto = rTrn.codOrigenMvto
		sMov.item(i).mov.codOrigenProceso = rTrn.codOrigenProceso

		sMov.item(i).mov.codMvto = rTrn.codMvtoPrim
		sMov.item(i).mov.codMvtoAdi = Nothing
		'sMov.item(i).mov.codMvtoIntreaCue = Nothing        10/01/2012 PCI
		sMov.item(i).mov.codMvtoIntreaCue = rTrn.codMvtoIntreaPrim
		sMov.item(i).mov.codMvtoIntreaAdi = Nothing




		'modelo--------
		If gcodAdministradora = 1034 Or gcodAdministradora = 1035 Then
			If rTrn.tipoProducto = "CAF" And rTrn.tipoPago = 3 And rTrn.perCotizacion > gPerContableSis Then
				sMov.item(i).mov.codMvtoComPor = Nothing				'sin combro de prima
			Else
				sMov.item(i).mov.codMvtoComPor = rTrn.codMvtoPrimCar
			End If
		Else
			If rTrn.tipoProducto = "CAF" And rTrn.perCotizacion >= gPerContableSis Then
				sMov.item(i).mov.codMvtoComPor = Nothing				'sin combro de prima
			Else
				sMov.item(i).mov.codMvtoComPor = rTrn.codMvtoPrimCar
			End If
		End If




		sMov.item(i).mov.codMvtoComFij = Nothing

		sMov.item(i).mov.idAdmOrigen = 0

		sMov.item(i).mov.fecValorCuota = rTrn.fecValorCuota
		sMov.item(i).mov.valMlValorCuota = rTrn.valMlValorCuota

		'SIS//
		'Valor cuota es T-2 OS-3798863 MOD-2011070125
		'sMov.item(i).mov.valMlCuotaComision = rTrn.valMlValorCuota
		If gcodAdministradora = 1034 Or gcodAdministradora = 1035 Then
			sMov.item(i).mov.valMlCuotaComision = rTrn.valMlCuotaComision
		Else
			sMov.item(i).mov.valMlCuotaComision = rTrn.valMlValorCuota
		End If

		sMov.item(i).mov.valMlMvto = rTrn.valMlPrimaSisCal
		sMov.item(i).mov.valMlMvtoInteres = rTrn.valMlPrimaSisInteresCal
		sMov.item(i).mov.valMlMvtoReajuste = rTrn.valMlPrimaSisReajusteCal
		sMov.item(i).mov.valCuoMvto = rTrn.valCuoPrimaSisCal
		sMov.item(i).mov.valCuoMvtoInteres = rTrn.valCuoPrimaSisInteresCal
		sMov.item(i).mov.valCuoMvtoReajuste = rTrn.valCuoPrimaSisReajusteCal

		sMov.item(i).mov.valMlAdicional = 0
		sMov.item(i).mov.valMlAdicionalInteres = 0
		sMov.item(i).mov.valMlAdicionalReajuste = 0
		sMov.item(i).mov.valCuoAdicional = 0
		sMov.item(i).mov.valCuoAdicionalInteres = 0
		sMov.item(i).mov.valCuoAdicionalReajuste = 0
		sMov.item(i).mov.valMlComisionPorcentual = rTrn.valMlPrimaCal
		sMov.item(i).mov.valCuoComisionPorcentual = rTrn.valCuoPrimaCal
		sMov.item(i).mov.valMlComisionFija = 0
		sMov.item(i).mov.valCuoComisionFija = 0
		sMov.item(i).mov.numDictamen = 0
		sMov.item(i).mov.indMvtoVisibleCartola = rTrn.indMvtoVisibleCartola
		sMov.item(i).mov.perCuatrimestre = rTrn.perCuatrimestre
		sMov.item(i).mov.idUsuarioIngReg = gidUsuarioProceso
		sMov.item(i).mov.idFuncionUltModifReg = gidUsuarioProceso
		sMov.item(i).mov.tipoOrigenDigitacion = rTrn.tipoOrigenDigitacion


		'lfc: 17-05-2021 - estadistica
		sMov.item(i).mov.claseCotizante = rTrn.claseCotizante
		sMov.item(i).mov.codActividadEconomica = rTrn.codActividadEconomica

	End Sub


	Private Sub TrnAMovExc()
		Dim i As Integer

		sMov.add(dsMov, rTrn.tipoImputacion, 1)
		i = sMov.count - 1

		sMov.item(i).mov.idCliente = rTrn.idCliente
		sMov.item(i).mov.numSaldo = rTrn.numSaldo
		sMov.item(i).mov.seqMvto = 0
		sMov.item(i).mov.seqRegistroTransaccion = rTrn.seqRegistro
		sMov.item(i).mov.tipoProducto = rTrn.tipoProducto
		sMov.item(i).mov.tipoFondo = rTrn.tipoFondoDestinoCal
		sMov.item(i).mov.perCotizacion = rTrn.perCotizacion
		sMov.item(i).mov.tipoRemuneracion = rTrn.tipoRemuneracion
		sMov.item(i).mov.tipoPago = rTrn.tipoPago
		sMov.item(i).mov.tipoPlanilla = rTrn.tipoPlanilla
		sMov.item(i).mov.tipoEntPagadora = rTrn.tipoEntidadPagadora
		sMov.item(i).mov.numReferenciaOrigen1 = rTrn.numReferenciaOrigen1
		sMov.item(i).mov.numReferenciaOrigen2 = rTrn.numReferenciaOrigen2
		sMov.item(i).mov.numReferenciaOrigen3 = rTrn.numReferenciaOrigen3
		sMov.item(i).mov.numReferenciaOrigen4 = rTrn.numReferenciaOrigen4
		sMov.item(i).mov.numReferenciaOrigen5 = rTrn.numReferenciaOrigen5
		sMov.item(i).mov.numReferenciaOrigen6 = rTrn.numReferenciaOrigen6
		sMov.item(i).mov.fecOperacion = rTrn.fecOperacion
		sMov.item(i).mov.idEmpleador = rTrn.idEmpleador

		sMov.item(i).mov.valMlRentaImponible = rTrn.valMlRentaImponible
		If gcodAdministradora = 1032 And blGenExcesoEnLinea And valMlRIMCotExcesoGen > 0 And gExesoTope Then
			sMov.item(i).mov.valMlRentaImponible = valMlRIMCotExcesoGen
		End If

		sMov.item(i).mov.fecAcreditacion = rTrn.fecAcreditacion
		sMov.item(i).mov.perContable = rTrn.perContable
		sMov.item(i).mov.seqMvtoOrigen = rTrn.seqMvtoOrigen
		sMov.item(i).mov.codOrigenMvto = rTrn.codOrigenMvto
		sMov.item(i).mov.codOrigenProceso = rTrn.codOrigenProceso

		sMov.item(i).mov.codMvto = rTrn.codMvtoExcesoTopeCal
		sMov.item(i).mov.codMvtoAdi = Nothing
		sMov.item(i).mov.codMvtoIntreaCue = Nothing
		sMov.item(i).mov.codMvtoIntreaAdi = Nothing
		sMov.item(i).mov.codMvtoComPor = Nothing
		sMov.item(i).mov.codMvtoComFij = Nothing

		sMov.item(i).mov.idAdmOrigen = 0

		sMov.item(i).mov.fecValorCuota = rTrn.fecValorCuota
		sMov.item(i).mov.valMlValorCuota = rTrn.valMlValorCuota

		sMov.item(i).mov.valMlMvto = gvalMlExcesoCal
		sMov.item(i).mov.valMlMvtoInteres = 0
		sMov.item(i).mov.valMlMvtoReajuste = 0
		sMov.item(i).mov.valCuoMvto = gvalCuoExcesoCal
		sMov.item(i).mov.valCuoMvtoInteres = 0
		sMov.item(i).mov.valCuoMvtoReajuste = 0
		sMov.item(i).mov.valMlAdicional = 0
		sMov.item(i).mov.valMlAdicionalInteres = 0
		sMov.item(i).mov.valMlAdicionalReajuste = 0
		sMov.item(i).mov.valCuoAdicional = 0
		sMov.item(i).mov.valCuoAdicionalInteres = 0
		sMov.item(i).mov.valCuoAdicionalReajuste = 0
		sMov.item(i).mov.valMlComisionPorcentual = 0
		sMov.item(i).mov.valCuoComisionPorcentual = 0
		sMov.item(i).mov.valMlComisionFija = 0
		sMov.item(i).mov.valCuoComisionFija = 0

		'sMov.item(i).mov.valMlSaldo = rTrn.valMlSaldo
		'sMov.item(i).mov.valCuoSaldo = rTrn.valCuoSaldo

		sMov.item(i).mov.numDictamen = 0
		sMov.item(i).mov.indMvtoVisibleCartola = rTrn.indMvtoVisibleCartola
		sMov.item(i).mov.perCuatrimestre = rTrn.perCuatrimestre
		sMov.item(i).mov.idUsuarioIngReg = gidUsuarioProceso
		sMov.item(i).mov.idFuncionUltModifReg = gidUsuarioProceso
		sMov.item(i).mov.tipoOrigenDigitacion = rTrn.tipoOrigenDigitacion

		'lfc: 17-05-2021 - estadistica
		sMov.item(i).mov.claseCotizante = rTrn.claseCotizante
		sMov.item(i).mov.codActividadEconomica = rTrn.codActividadEconomica


	End Sub

	Private Sub TrnAMovExcEmpl()
		Dim i As Integer


		sMov.add(dsMov, rTrn.tipoImputacion, 5)
		i = sMov.count - 1

		sMov.item(i).mov.idCliente = rTrn.idCliente
		sMov.item(i).mov.numSaldo = rTrn.numSaldo
		sMov.item(i).mov.seqMvto = 0
		sMov.item(i).mov.seqRegistroTransaccion = rTrn.seqRegistro
		sMov.item(i).mov.tipoProducto = rTrn.tipoProducto
		sMov.item(i).mov.tipoFondo = rTrn.tipoFondoDestinoCal
		sMov.item(i).mov.perCotizacion = rTrn.perCotizacion
		sMov.item(i).mov.tipoRemuneracion = rTrn.tipoRemuneracion
		sMov.item(i).mov.tipoPago = rTrn.tipoPago
		sMov.item(i).mov.tipoPlanilla = rTrn.tipoPlanilla
		sMov.item(i).mov.tipoEntPagadora = rTrn.tipoEntidadPagadora
		sMov.item(i).mov.numReferenciaOrigen1 = rTrn.numReferenciaOrigen1
		sMov.item(i).mov.numReferenciaOrigen2 = rTrn.numReferenciaOrigen2
		sMov.item(i).mov.numReferenciaOrigen3 = rTrn.numReferenciaOrigen3
		sMov.item(i).mov.numReferenciaOrigen4 = rTrn.numReferenciaOrigen4
		sMov.item(i).mov.numReferenciaOrigen5 = rTrn.numReferenciaOrigen5
		sMov.item(i).mov.numReferenciaOrigen6 = rTrn.numReferenciaOrigen6
		sMov.item(i).mov.fecOperacion = rTrn.fecOperacion
		sMov.item(i).mov.idEmpleador = rTrn.idEmpleador

		sMov.item(i).mov.valMlRentaImponible = rTrn.valMlRentaImponible
		If gcodAdministradora = 1032 And blGenExcesoEnLinea And valMlRIMSISExcesoGen > 0 And gExesoTopeSis Then
			sMov.item(i).mov.valMlRentaImponible = valMlRIMSISExcesoGen
		End If

		sMov.item(i).mov.fecAcreditacion = rTrn.fecAcreditacion
		sMov.item(i).mov.perContable = rTrn.perContable
		sMov.item(i).mov.seqMvtoOrigen = rTrn.seqMvtoOrigen
		sMov.item(i).mov.codOrigenMvto = rTrn.codOrigenMvto
		sMov.item(i).mov.codOrigenProceso = rTrn.codOrigenProceso

		sMov.item(i).mov.codMvto = rTrn.codMvtoExcEmpl
		sMov.item(i).mov.codMvtoAdi = Nothing
		sMov.item(i).mov.codMvtoIntreaCue = Nothing
		sMov.item(i).mov.codMvtoIntreaAdi = Nothing
		sMov.item(i).mov.codMvtoComPor = Nothing
		sMov.item(i).mov.codMvtoComFij = Nothing

		sMov.item(i).mov.idAdmOrigen = 0

		sMov.item(i).mov.fecValorCuota = rTrn.fecValorCuota
		sMov.item(i).mov.valMlValorCuota = rTrn.valMlValorCuota

		sMov.item(i).mov.valMlMvto = gvalMlExcesoEmplCal
		sMov.item(i).mov.valMlMvtoInteres = 0
		sMov.item(i).mov.valMlMvtoReajuste = 0
		sMov.item(i).mov.valCuoMvto = gvalCuoExcesoEmplCal
		sMov.item(i).mov.valCuoMvtoInteres = 0
		sMov.item(i).mov.valCuoMvtoReajuste = 0
		sMov.item(i).mov.valMlAdicional = 0
		sMov.item(i).mov.valMlAdicionalInteres = 0
		sMov.item(i).mov.valMlAdicionalReajuste = 0
		sMov.item(i).mov.valCuoAdicional = 0
		sMov.item(i).mov.valCuoAdicionalInteres = 0
		sMov.item(i).mov.valCuoAdicionalReajuste = 0
		sMov.item(i).mov.valMlComisionPorcentual = 0
		sMov.item(i).mov.valCuoComisionPorcentual = 0
		sMov.item(i).mov.valMlComisionFija = 0
		sMov.item(i).mov.valCuoComisionFija = 0

		'sMov.item(i).mov.valMlSaldo = rTrn.valMlSaldo
		'sMov.item(i).mov.valCuoSaldo = rTrn.valCuoSaldo

		sMov.item(i).mov.numDictamen = 0
		sMov.item(i).mov.indMvtoVisibleCartola = rTrn.indMvtoVisibleCartola
		sMov.item(i).mov.perCuatrimestre = rTrn.perCuatrimestre
		sMov.item(i).mov.idUsuarioIngReg = gidUsuarioProceso
		sMov.item(i).mov.idFuncionUltModifReg = gidUsuarioProceso
		sMov.item(i).mov.tipoOrigenDigitacion = rTrn.tipoOrigenDigitacion

		'lfc: 17-05-2021 - estadistica
		sMov.item(i).mov.claseCotizante = rTrn.claseCotizante
		sMov.item(i).mov.codActividadEconomica = rTrn.codActividadEconomica

	End Sub


	Private Sub TrnAMovCompen(ByVal tipoImputacion As String)

		Dim i As Integer

		sMov.add(dsMov, tipoImputacion, 2)
		i = sMov.count - 1

		sMov.item(i).mov.idCliente = rTrn.idCliente
		sMov.item(i).mov.numSaldo = rTrn.numSaldo
		sMov.item(i).mov.seqMvto = 0
		sMov.item(i).mov.seqRegistroTransaccion = rTrn.seqRegistro
		sMov.item(i).mov.tipoProducto = rTrn.tipoProducto
		sMov.item(i).mov.tipoFondo = rTrn.tipoFondoDestinoCal
		sMov.item(i).mov.perCotizacion = rTrn.perCotizacion
		sMov.item(i).mov.tipoImputacion = tipoImputacion
		sMov.item(i).mov.tipoRemuneracion = rTrn.tipoRemuneracion
		sMov.item(i).mov.tipoPago = rTrn.tipoPago
		sMov.item(i).mov.tipoPlanilla = rTrn.tipoPlanilla
		sMov.item(i).mov.tipoEntPagadora = rTrn.tipoEntidadPagadora
		sMov.item(i).mov.numReferenciaOrigen1 = rTrn.numReferenciaOrigen1
		sMov.item(i).mov.numReferenciaOrigen2 = rTrn.numReferenciaOrigen2
		sMov.item(i).mov.numReferenciaOrigen3 = rTrn.numReferenciaOrigen3
		sMov.item(i).mov.numReferenciaOrigen4 = rTrn.numReferenciaOrigen4
		sMov.item(i).mov.numReferenciaOrigen5 = rTrn.numReferenciaOrigen5
		sMov.item(i).mov.numReferenciaOrigen6 = rTrn.numReferenciaOrigen6
		sMov.item(i).mov.fecOperacion = rTrn.fecOperacion
		sMov.item(i).mov.idEmpleador = rTrn.idEmpleador

		'Cuando se paga solo Prima se debe asignar ValMlRentaImponiblie ya que ValMlRentaImponible = 0 OS-4235417
		'sMov.item(i).mov.valMlRentaImponible = rTrn.valMlRentaImponible
		If (rTrn.valMlMvtoCal + rTrn.valMlReajusteCal + rTrn.valMlInteresCal + rTrn.valMlAdicionalCal + rTrn.valMlAdicionalInteresCal + rTrn.valMlAdicionalReajusteCal) = 0 Then
			sMov.item(i).mov.valMlRentaImponible = rTrn.valMlRentaImponibleSis
		Else
			sMov.item(i).mov.valMlRentaImponible = rTrn.valMlRentaImponible
		End If

		If gcodAdministradora = 1032 And blGenExcesoEnLinea And valMlRIMCotExcesoGen > 0 Then

			If sMov.item(i).mov.valMlRentaImponible - valMlRIMCotExcesoGen > 0 Then
				sMov.item(i).mov.valMlRentaImponible = Math.Abs(rTrn.valMlRentaImponible - valMlRIMCotExcesoGen)
			ElseIf sMov.item(i).mov.valMlRentaImponible - valMlRIMCotExcesoGen < 0 Then
				sMov.item(i).mov.valMlRentaImponible = Math.Abs(valMlRIMCotExcesoGen - rTrn.valMlRentaImponible)


				'ElseIf sMov.item(i).mov.valMlRentaImponible - valMlRIMCotExcesoGen = 0 Then
				'    sMov.item(i).mov.valMlRentaImponible = rTrn.valMlRentaImponible
			End If
		End If

		sMov.item(i).mov.fecAcreditacion = rTrn.fecAcreditacion
		sMov.item(i).mov.perContable = rTrn.perContable
		sMov.item(i).mov.seqMvtoOrigen = rTrn.seqMvtoOrigen
		sMov.item(i).mov.codOrigenMvto = rTrn.codOrigenMvto
		sMov.item(i).mov.codOrigenProceso = rTrn.codOrigenProceso

		sMov.item(i).mov.codMvto = CodMvtoCompen(rTrn.tipoProducto, tipoImputacion)

		sMov.item(i).mov.codMvtoAdi = Nothing
		sMov.item(i).mov.codMvtoIntreaCue = Nothing
		sMov.item(i).mov.codMvtoIntreaAdi = Nothing
		sMov.item(i).mov.codMvtoComPor = Nothing
		sMov.item(i).mov.codMvtoComFij = Nothing

		sMov.item(i).mov.idAdmOrigen = 0

		sMov.item(i).mov.fecValorCuota = rTrn.fecValorCuota
		sMov.item(i).mov.valMlValorCuota = rTrn.valMlValorCuota

		sMov.item(i).mov.valMlMvtoInteres = 0
		sMov.item(i).mov.valMlMvtoReajuste = 0
		sMov.item(i).mov.valCuoMvtoInteres = 0
		sMov.item(i).mov.valCuoMvtoReajuste = 0
		sMov.item(i).mov.valMlAdicional = 0
		sMov.item(i).mov.valMlAdicionalInteres = 0
		sMov.item(i).mov.valMlAdicionalReajuste = 0
		sMov.item(i).mov.valCuoAdicional = 0
		sMov.item(i).mov.valCuoAdicionalInteres = 0
		sMov.item(i).mov.valCuoAdicionalReajuste = 0
		sMov.item(i).mov.valMlComisionPorcentual = 0
		sMov.item(i).mov.valCuoComisionPorcentual = 0
		sMov.item(i).mov.valMlComisionFija = 0
		sMov.item(i).mov.valCuoComisionFija = 0

		'sMov.item(i).mov.valMlSaldo = rTrn.valMlSaldo
		'sMov.item(i).mov.valCuoSaldo = rTrn.valCuoSaldo

		sMov.item(i).mov.numDictamen = 0
		sMov.item(i).mov.indMvtoVisibleCartola = rTrn.indMvtoVisibleCartola
		sMov.item(i).mov.perCuatrimestre = rTrn.perCuatrimestre
		sMov.item(i).mov.idUsuarioIngReg = gidUsuarioProceso
		sMov.item(i).mov.idFuncionUltModifReg = gidUsuarioProceso
		sMov.item(i).mov.tipoOrigenDigitacion = rTrn.tipoOrigenDigitacion

		'lfc: 17-05-2021 - estadistica
		sMov.item(i).mov.claseCotizante = rTrn.claseCotizante
		sMov.item(i).mov.codActividadEconomica = rTrn.codActividadEconomica

	End Sub

	Private Sub TrnAMovPrima(ByVal tipoImputacion As String)


		Dim i As Integer

		sMov.add(dsMov, tipoImputacion, 3)
		i = sMov.count - 1


		sMov.item(i).mov.idCliente = rTrn.idCliente
		sMov.item(i).mov.numSaldo = rTrn.numSaldo
		sMov.item(i).mov.seqMvto = 0
		sMov.item(i).mov.seqRegistroTransaccion = rTrn.seqRegistro
		sMov.item(i).mov.tipoProducto = rTrn.tipoProducto
		sMov.item(i).mov.tipoFondo = rTrn.tipoFondoDestinoCal
		sMov.item(i).mov.perCotizacion = rTrn.perCotizacion
		sMov.item(i).mov.tipoImputacion = tipoImputacion
		sMov.item(i).mov.tipoRemuneracion = rTrn.tipoRemuneracion
		sMov.item(i).mov.tipoPago = rTrn.tipoPago
		sMov.item(i).mov.tipoPlanilla = rTrn.tipoPlanilla
		sMov.item(i).mov.tipoEntPagadora = rTrn.tipoEntidadPagadora
		sMov.item(i).mov.numReferenciaOrigen1 = rTrn.numReferenciaOrigen1
		sMov.item(i).mov.numReferenciaOrigen2 = rTrn.numReferenciaOrigen2
		sMov.item(i).mov.numReferenciaOrigen3 = rTrn.numReferenciaOrigen3
		sMov.item(i).mov.numReferenciaOrigen4 = rTrn.numReferenciaOrigen4
		sMov.item(i).mov.numReferenciaOrigen5 = rTrn.numReferenciaOrigen5
		sMov.item(i).mov.numReferenciaOrigen6 = rTrn.numReferenciaOrigen6
		sMov.item(i).mov.fecOperacion = rTrn.fecOperacion
		sMov.item(i).mov.idEmpleador = rTrn.idEmpleador
		sMov.item(i).mov.valMlRentaImponible = rTrn.valMlRentaImponible
		sMov.item(i).mov.fecAcreditacion = rTrn.fecAcreditacion
		sMov.item(i).mov.perContable = rTrn.perContable
		sMov.item(i).mov.seqMvtoOrigen = rTrn.seqMvtoOrigen
		sMov.item(i).mov.codOrigenMvto = rTrn.codOrigenMvto
		sMov.item(i).mov.codOrigenProceso = rTrn.codOrigenProceso

		sMov.item(i).mov.codMvtoAdi = Nothing
		sMov.item(i).mov.codMvtoIntreaCue = Nothing
		sMov.item(i).mov.codMvtoIntreaAdi = Nothing
		sMov.item(i).mov.codMvtoComPor = Nothing
		sMov.item(i).mov.codMvtoComFij = Nothing


		sMov.item(i).mov.fecValorCuota = rTrn.fecValorCuota
		sMov.item(i).mov.valMlValorCuota = rTrn.valMlValorCuota

		sMov.item(i).mov.valMlMvtoInteres = 0
		sMov.item(i).mov.valMlMvtoReajuste = 0
		sMov.item(i).mov.valCuoMvtoInteres = 0
		sMov.item(i).mov.valCuoMvtoReajuste = 0
		sMov.item(i).mov.valMlAdicional = 0
		sMov.item(i).mov.valMlAdicionalInteres = 0
		sMov.item(i).mov.valMlAdicionalReajuste = 0
		sMov.item(i).mov.valCuoAdicional = 0
		sMov.item(i).mov.valCuoAdicionalInteres = 0
		sMov.item(i).mov.valCuoAdicionalReajuste = 0
		sMov.item(i).mov.valMlComisionPorcentual = 0
		sMov.item(i).mov.valCuoComisionPorcentual = 0
		sMov.item(i).mov.valMlComisionFija = 0
		sMov.item(i).mov.valCuoComisionFija = 0

		'sMov.item(i).mov.valMlSaldo = rTrn.valMlSaldo
		'sMov.item(i).mov.valCuoSaldo = rTrn.valCuoSaldo

		sMov.item(i).mov.numDictamen = 0
		sMov.item(i).mov.indMvtoVisibleCartola = rTrn.indMvtoVisibleCartola
		sMov.item(i).mov.perCuatrimestre = rTrn.perCuatrimestre
		sMov.item(i).mov.idUsuarioIngReg = gidUsuarioProceso
		sMov.item(i).mov.idFuncionUltModifReg = gidUsuarioProceso
		sMov.item(i).mov.tipoOrigenDigitacion = rTrn.tipoOrigenDigitacion

		'lfc: 17-05-2021 - estadistica
		sMov.item(i).mov.claseCotizante = rTrn.claseCotizante
		sMov.item(i).mov.codActividadEconomica = rTrn.codActividadEconomica

	End Sub

	Private Sub TrnATrf()

		If rTrn.codOrigenMvto = "RECAUDAC" Then
			rTrf.numPlanilla = rTrn.numReferenciaOrigen1
			rTrf.numCaja = rTrn.numReferenciaOrigen2
			rTrf.numPagina = rTrn.numReferenciaOrigen3
			rTrf.seqLinea = rTrn.numReferenciaOrigen4
		Else

			rTrf.numPlanilla = 0
			rTrf.numCaja = 0
			rTrf.numPagina = 0
			rTrf.seqLinea = 0
		End If
		rTrf.numRefOrigen = rTrn.seqRegistro

		rTrf.seqMvto = 0
		rTrf.tipoProducto = rTrn.tipoProducto
		rTrf.tipoFondo = "C"		  'rTrn.tipoFondoDestinoCal
		rTrf.perCotiza = rTrn.perCotizacion
		rTrf.tipoPago = rTrn.tipoPago
		rTrf.fecOperacion = rTrn.fecOperacion
		rTrf.idCliente = rTrn.idCliente
		rTrf.idEmpleador = rTrn.idEmpleador
		rTrf.idPersona = rTrn.idPersona
		rTrf.apPaterno = rTrn.apPaterno
		rTrf.apMaterno = rTrn.apMaterno
		rTrf.nombre = rTrn.nombre
		rTrf.nombreAdicional = rTrn.nombreAdicional
		rTrf.valMlRentaImponible = rTrn.valMlRentaImponible
		rTrf.fecValorCuota = rTrn.fecValorCuotaCaja
		rTrf.valMlValorCuota = rTrn.valMlValorCuotaCaja
		rTrf.valMlTransferencia = 0
		rTrf.valCuoTransferencia = 0
		rTrf.valMlComisionApv = 0

		rTrf.valMlTransferenciaRec = rTrn.valMlTransferenciaCal
		rTrf.valMlInteres = rTrn.valMlInteres
		rTrf.valMlReajuste = rTrn.valMlReajuste

		rTrf.valCuoTransferenciaRec = rTrn.valCuoTransferenciaCal
		rTrf.valCuoInteres = rTrn.valCuoInteres
		rTrf.valCuoReajuste = rTrn.valCuoReajuste

		rTrf.numSolicitudTransf = 0
		rTrf.fecAcreditacion = rTrn.fecAcreditacion
		rTrf.fecAcreditacionTransf = Nothing
		rTrf.perContable = rTrn.perContable
		rTrf.perContableTransf = Nothing
		rTrf.codMvto = 99999
		rTrf.codAdmDestino = rTrn.idInstDestino
		rTrf.estadoTransferencia = "P"
		rTrf.fecTransferencia = Nothing
		rTrf.perProceso = Nothing


	End Sub

	Private Function LeerDatosCliente() As String
		Dim sub_catgoria_saldo_cai As Long
		Dim dsPactos As New DataSet()

		If rTrn.codOrigenProceso = "CAMBFOND" And rTrn.tipoImputacion = "ABO" Then
			LeerDatosClienteCambioFondo()
			Exit Function
		End If

		'CA-2009080033 //26-10-2009 LFC: monto Ml no informado
		If rTrn.codOrigenProceso = "RETCAVAD" Or rTrn.codOrigenProceso = "RETCAVFO" Or rTrn.codOrigenProceso = "RETCDCAD" Or rTrn.codOrigenProceso = "RETCDCFO" Or rTrn.codOrigenProceso = "RETCCVAD" Or rTrn.codOrigenProceso = "RETCCVFO" Or rTrn.codOrigenProceso = "RETCAIFO" Or rTrn.codOrigenProceso = "RETPAGPE" Or rTrn.codOrigenProceso = "RETEXDIP" Or rTrn.codOrigenProceso = "RETCVCAD" Or rTrn.codOrigenProceso = "RETCVCFO" Then
			If (rTrn.valMlComisPorcentual Is Nothing Or rTrn.valMlComisPorcentual = 0) And rTrn.valCuoComisPorcentual > 0 Then
				GenerarLog("E", 0, "Hebra " & gIdHebra & " - Monto Comision($) no Informado ", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
				blIgnorar = True
				Exit Function
			End If
		End If


		'Leo registro clientes for update
		If rTrn.idCliente <> gIdClienteAnterior Then

			clsSal = New INESaldo()
			clsSal.clear()

			If rTrn.idCliente = 0 Then
				gIdClienteAnterior = -1
			Else
				gIdClienteAnterior = rTrn.idCliente
			End If

			blIgnorarCliente = False
			dsAux = InformacionCliente.traercliupdate(gdbc, gidAdm, rTrn.idCliente, gnumeroId, gidUsuarioProceso, gfuncion)

			If dsAux.Tables(0).Rows.Count = 0 Then
				blAcreditarARezago = True
				LeerDatosCliente = "11"
				rTrn.codCausalRezagoCal = "11"
				Exit Function
			Else
				rCli = New ccClientes(dsAux)
				InformacionCliente.crearBloqueoCliente(gdbc, gidAdm, rTrn.idCliente, gnumeroId, gidUsuarioProceso, gfuncion)
			End If
		Else
			If blIgnorarCliente Then
				blIgnorar = True

				Exit Function

			End If
		End If


		If rTrn.codDestinoTransaccion = "TRF" Then
			Exit Function
		End If

		'Leo tipo producto vigente
		Dim indVerificaCuenta As String
		Dim fechaCaja As Date
		dsAux = InformacionCliente.traerTipoProductoVig(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto)
		'dsAux = InformacionCliente.traerTipoProductoVigNew(gdbc,gidAdm, rTrn.idCliente, rTrn.tipoProducto)
		If dsAux.Tables(0).Rows.Count = 0 Then

			If (rMovAcr.indCotizacion = "S" And rTrn.tipoImputacion = "ABO") Or _
			   (rTrn.tipoProducto = "CAF" And rTrn.tipoImputacion = "ABO") Then

				If rTrn.indInsistenciaAcr = "S" Then
					indVerificaCuenta = "A"
				Else
					indVerificaCuenta = "N"
				End If

				fechaCaja = IIf(IsNothing(rTrn.fecOperacionAdmOrigen), rTrn.fecOperacion, rTrn.fecOperacionAdmOrigen)

				'lfc://10-12 -- implementacion SUB_CATEGORIA saldos CAI
				gcausalRezago = InformacionCliente.creacionCuentasCotiz(gdbc, _
				  gidAdm, _
				  rTrn.idCliente, _
				  rTrn.tipoProducto, _
				  rTrn.tipoFondoDestino, _
				  rTrn.categoria, _
				  rCli.tipoRegPrevisional, _
				  rCli.tipoTrabajador, _
				  rCli.sexo, _
				  rCli.fecNacimiento, _
				  gfecAcreditacion, _
				  fechaCaja, _
				  rTrn.perCotizacion, _
				  rTrn.idEmpleador, _
				  rTrn.tipoEntidadPagadora, _
				  rTrn.tipoPlanilla, _
				  indVerificaCuenta, _
				  rTrn.numReferenciaOrigen6, _
				  rCli.fecAfiliacionAdm, _
				  gidUsuarioProceso, _
				  gfuncion)

				dsAux = InformacionCliente.traerTipoProductoVig(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto)
				'dsAux = InformacionCliente.traerTipoProductoVigNew(gdbc,gidAdm, rTrn.idCliente, rTrn.tipoProducto)
				If dsAux.Tables(0).Rows.Count = 0 Then
					rTPr = Nothing
					rTPr = New ccTiposProducto(dsAux.Tables(0).NewRow)
					LeerDatosCliente = gcausalRezago					  'Tipo producto no vigente
					rTrn.codDestinoTransaccionCal = "REZ"
					rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
					Exit Function
				Else
					rTPr = Nothing
					rTPr = New ccTiposProducto(dsAux)
					rCli.fecAfiliacionAdm = rTrn.perCotizacion
					rCli.fecAfiliacionSistema = rTrn.perCotizacion
					rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
				End If
			Else
				rTPr = Nothing
				rTPr = New ccTiposProducto(dsAux.Tables(0).NewRow)
				LeerDatosCliente = "20"				'Tipo producto no vigente
				rTrn.codDestinoTransaccionCal = "REZ"
				rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
				Exit Function
			End If
		Else
			rTPr = Nothing
			rTPr = New ccTiposProducto(dsAux)
			'aTpr = New INERegistros.tiposProductos(dsAux.Tables(0).Rows(0).Item(0), Me.dsTP)
			'rTPr = aTpr.rtrn
			rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
		End If


		If rTrn.codDestinoTransaccion = "REZ" Then
			rTrn.codDestinoTransaccionCal = rTrn.codDestinoTransaccion
			rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
			Exit Function
		End If



		dsAux = InformacionCliente.traerProductoVig(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.tipoFondoDestinoCal)
		'dsAux = InformacionCliente.traerProductoVigNew(gdbc,gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.tipoFondoDestinoCal)
		If dsAux.Tables(0).Rows.Count = 0 Then
			LeerDatosCliente = "21"
			Exit Function
		Else
			rPro = Nothing
			rPro = New ccProductos(dsAux)
			'aPro = New INERegistros.Productos(dsAux.Tables(0).Rows(0).Item(0), dsP)
			'rPro = aPro.rtrn
			Select Case rPro.codBloqueo
				Case "BL2"
					If gcodAdministradora <> 1032 Then					  ' OS:9705406 LFC:añade cod_administradora
						If rTrn.codOrigenProceso <> "TRAEGCTA" And rTrn.codOrigenProceso <> "TRAEGDES" Then
							LeerDatosCliente = "1J"
							GenerarLog("E", 0, "Hebra " & gIdHebra & " - Cuenta Bloqueada ", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
							'Exit Function SE COMENTA PARA QUE SIGA LEYENDO EL SALDO, SI NO LO LEE 
							'SE PRODUCE UNA FALLA AL VALIDAR REZAGOS, usa el num saldo para determinar sobregiro
						End If
					End If
				Case "BL8"
					If rTrn.codOrigenProceso <> "CAMBFOND" Then
						'  LeerDatosCliente = "1"
						GenerarLog("E", 0, "Hebra " & gIdHebra & " - Cuenta Bloqueada por Cambio Fondo ", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
						blIgnorar = True
						Exit Function
					End If
				Case "BL7"
					If gcodAdministradora = 1034 Or gcodAdministradora = 1035 Then					  'Modelo si requiere bloqueo de Movimientos 'RECLAMO CIRCULAR 650

						blAcreditarARezago = True
						LeerDatosCliente = "11"
						rTrn.codCausalRezagoCal = "11"
						Exit Function

					End If
				Case "BL9"				'NCG-264
					If blCongelaSaldo Then

						'If rTrn.tipoImputacion = "CAR" And rTrn.codOrigenProceso <> "AJUMASIV" And codigoMvtoNcg264() Then
						If validaCodbloqueo("BL9") Then
							GenerarLog("E", 0, "Hebra " & gIdHebra & " - Cuenta Congelada (NCG-264)", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
							blIgnorar = True
							Exit Function
						End If

					End If

			End Select
		End If

		'INI OS-7062884. 20/01/2016. PCI. Cotizaciones SALDORETENIDO. Si cumple Condiciones Categoria cambia a SALDORETENIDO
		If gcodAdministradora = 1033 And (rTrn.codOrigenProceso = "RECAUDAC" Or rTrn.codOrigenProceso = "TRAINREZ" Or rTrn.codOrigenProceso = "REREZMAS" Or rTrn.codOrigenProceso = "REREZSEL") Then
			If rTrn.tipoProducto = "CCO" And rTrn.categoria = "NORMAL" Then
				dsAux = Sys.IngresoEgreso.InformacionCliente.acreditaSaldosRetenidos(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.tipoFondoDestinoCal, rTrn.perCotizacion)
				If dsAux.Tables(0).Rows.Count > 0 Then
					Dim IndEstado As String
					IndEstado = IIf(IsDBNull(dsAux.Tables(0).Rows(0).Item("INDSALIDA")), "N", dsAux.Tables(0).Rows(0).Item("INDSALIDA"))
					If IndEstado = "S" Then
						rTrn.categoria = "SALDORETENIDO"
					End If
				End If
			End If
		End If
		'FIN OS-7062884. 20/01/2016. PCI. Cotizaciones SALDORETENIDO.

		'Leo saldo vigente
		Select Case True

			Case gcodOrigenProceso = "RETCAIFO"
				'dsAux = Sys.Kernel.Producto.traerSaldo(gdbc,gidAdm, rTrn.idCliente, rTrn.numReferenciaOrigen3)
				If rTrn.subCategoria > 0 Then
					'lfc:// 14-12 -- implementacion SUB_CATEGORIA saldo CAI
					dsAux = Sys.IngresoEgreso.InformacionCliente.traerSaldoCaiCategoria(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.tipoFondoOrigen, rTrn.categoria, rTrn.subCategoria)
				Else
					dsAux = Sys.IngresoEgreso.InformacionCliente.traerSaldoCaiNumSaldo(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.tipoFondoOrigen, rTrn.numReferenciaOrigen3)
				End If



			Case rTrn.tipoProducto = "CAI" And rTrn.tipoImputacion = "CAR" And (gcodOrigenProceso = "AJUSELEC" Or gcodOrigenProceso = "AJUMASIV")
				If rTrn.subCategoria > 0 Then
					'lfc:// 14-12 -- implementacion SUB_CATEGORIA saldo CAI
					dsAux = Sys.IngresoEgreso.InformacionCliente.traerSaldoCaiCategoria(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.tipoFondoOrigen, rTrn.categoria, rTrn.subCategoria)
				Else
					dsAux = Sys.IngresoEgreso.InformacionCliente.traerSaldoCaiNumSaldo(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.tipoFondoOrigen, rTrn.numReferenciaOrigen3)
				End If

			Case rTrn.tipoProducto = "CAI" And rTrn.tipoImputacion = "ABO"
				'dsAux = InformacionCliente.traerSaldoCai(gdbc,gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.tipoFondoDestinoCal, rTrn.categoria, rTrn.perCotizacion)
				'lfc:// 14-12 -- implementacion SUB_CATEGORIA saldo CAI // buscar sub_categoria en Pactos
				If rTrn.subCategoria Is Nothing Or rTrn.subCategoria = 0 Then
					If rTrn.codOrigenProceso <> "CAMBFOND" Then
						'PCI 11/07/2010. Si es CAMBFON no debe buscar
						dsPactos = InformacionCliente.verificaContratoIndemnizNew(gdbc, gidAdm, rTrn.idCliente, rCli.tipoTrabajador, rTrn.tipoProducto, rTPr.numTipoProducto, rTrn.subCategoria, rTrn.categoria, rTrn.perCotizacion, gidUsuarioProceso, gfuncion)
						If dsPactos.Tables(0).Rows.Count = 0 Then
							LeerDatosCliente = "84"							'Num contrato invalido
							Exit Function
						End If
						If IsDBNull(dsPactos.Tables(0).Rows(0).Item("COD_CAUSAL_REZAGO")) Then

						Else
							If dsPactos.Tables(0).Rows(0).Item("COD_CAUSAL_REZAGO") = 0 Then
								rTrn.subCategoria = IIf(IsDBNull(dsPactos.Tables(0).Rows(0).Item("SUB_CATEGORIA")), rTrn.subCategoria, dsPactos.Tables(0).Rows(0).Item("SUB_CATEGORIA"))
								'rSal.fecAperturaSaldo = IIf(IsDBNull(dsPactos.Tables(0).Rows(0).Item("FEC_APERTURA_SALDO")), rTrn.perCotizacion, dsPactos.Tables(0).Rows(0).Item("SUB_CATEGORIA"))
							Else
								LeerDatosCliente = CStr(dsPactos.Tables(0).Rows(0).Item("COD_CAUSAL_REZAGO"))								  'Num contrato invalido
								Exit Function
							End If
						End If
					End If
				End If
				dsAux = Sys.IngresoEgreso.InformacionCliente.traerSaldoCaiCategoria(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.tipoFondoDestinoCal, rTrn.categoria, rTrn.subCategoria)

				'--.-->>>LFC:09/04/2009 -- para cambiofondo cai traer saldo por el num_saldo - SUBCATEGORIA
			Case rTrn.tipoProducto = "CAI" And rTrn.tipoImputacion = "CAR" And (gcodOrigenProceso = "CAMBFOND" Or gcodOrigenProceso = "TRAEGCTA")
				'dsAux = Kernel.Producto.traerSaldo(gdbc,gidAdm, rTrn.idCliente, rTrn.subCategoria)
				'dsAux = Sys.IngresoEgreso.InformacionCliente.traerSaldoCaiNumSaldo(gdbc,gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.tipoFondoOrigen, rTrn.subCategoria)

				'lfc:// 10-12 implementacion SUB_CATEGORIA saldos CAI
				dsAux = Sys.IngresoEgreso.InformacionCliente.traerSaldoCaiCategoria(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.tipoFondoOrigen, rTrn.categoria, rTrn.subCategoria)


				'--.--lfc - cn2
			Case rTrn.tipoProducto = "CVC"			  'TRAERSALDOCVC(P1,P2..PN, RTRN.SUBCATEGORIA, REGIMEN)
				If rTrn.codRegTributario Is Nothing Or rTrn.codRegTributario = "" Then
					LeerDatosCliente = "85"					  'Regimen tributario error
					Exit Function
				ElseIf rTrn.subCategoria <= 0 Then
					LeerDatosCliente = "84"					  'Num contrato invalido
					Exit Function
				Else
					dsAux = InformacionCliente.traerSaldoApv(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.tipoFondoDestinoCal, rTrn.categoria, rTrn.subCategoria, rTrn.codRegTributario)
				End If
				'--.--lfc - cn2
			Case rTrn.tipoProducto = "CCV"			  'TRAERSALDOCVC(P1,P2..PN,  REGIMEN)
				If rTrn.codRegTributario Is Nothing Or rTrn.codRegTributario = "" Then
					LeerDatosCliente = "85"					  'Regimen tributario error
					Exit Function
				Else
					dsAux = InformacionCliente.traerSaldoApv(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.tipoFondoDestinoCal, rTrn.categoria, rTrn.subCategoria, rTrn.codRegTributario)
				End If

			Case Else

				If gtipoProceso = "SI" Then
					dsAux = InformacionCliente.traerSaldoVig(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.tipoFondoDestinoCal, rTrn.categoria, rTPr.numTipoProducto)
				ElseIf gtipoProceso = "AC" Then
					dsAux = InformacionCliente.traerSaldoVigAcr(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.tipoFondoDestinoCal, rTrn.categoria, rTPr.numTipoProducto)
				End If

		End Select


		If dsAux.Tables(0).Rows.Count = 0 Then
			rSal = New ccSaldos(dsAux.Tables(0).NewRow)

			If rTrn.tipoImputacion <> "ABO" Then
				LeerDatosCliente = "22"				'Saldo no vigente
				Exit Function
			End If

			If rTrn.tipoProducto = "CAI" Then


				'rSal.fecAperturaSaldo = InformacionCliente.verificaContratoIndemniz(gdbc,gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTPr.numTipoProducto, rTrn.categoria, rTrn.perCotizacion, gidUsuarioProceso, gfuncion)
				'lfc: 21-12-09, implementacion sub-categoria saldo CAI
				If rTrn.codOrigenProceso <> "CAMBFOND" Then
					'PCI 11/07/2010. Si es CAMBFOND no debe buscar Contrato
					dsPactos = InformacionCliente.verificaContratoIndemnizNew(gdbc, gidAdm, rTrn.idCliente, rCli.tipoTrabajador, rTrn.tipoProducto, rTPr.numTipoProducto, rTrn.subCategoria, rTrn.categoria, rTrn.perCotizacion, gidUsuarioProceso, gfuncion)
					If dsPactos.Tables(0).Rows.Count = 0 Then
						LeerDatosCliente = "84"						 'Num contrato invalido
						Exit Function
					End If
					If IsDBNull(dsPactos.Tables(0).Rows(0).Item("COD_CAUSAL_REZAGO")) Then

					Else
						If dsPactos.Tables(0).Rows(0).Item("COD_CAUSAL_REZAGO") = 0 Then
							rTrn.subCategoria = IIf(IsDBNull(dsPactos.Tables(0).Rows(0).Item("SUB_CATEGORIA")), rTrn.subCategoria, dsPactos.Tables(0).Rows(0).Item("SUB_CATEGORIA"))
							rSal.fecAperturaSaldo = IIf(IsDBNull(dsPactos.Tables(0).Rows(0).Item("FEC_APERTURA_SALDO")), rTrn.perCotizacion, dsPactos.Tables(0).Rows(0).Item("FEC_APERTURA_SALDO"))
						Else
							LeerDatosCliente = CStr(dsPactos.Tables(0).Rows(0).Item("COD_CAUSAL_REZAGO"))							'Num contrato invalido
							Exit Function
						End If
					End If

				End If

				If rSal.fecAperturaSaldo = rTrn.perCotizacion Then
					rSal.estadoSaldo = "L"
				Else
					rSal.estadoSaldo = "R"
				End If
			Else
				rSal.fecAperturaSaldo = gfecAcreditacion
			End If

			CrearSaldo()
		Else
			rSal = Nothing
			rSal = New ccSaldos(dsAux)
			'aSal = New INERegistros.saldos(dsAux.Tables(0).Rows(0).Item(0), dsS)
			'rSal = aSal.rtrn
			If rSal.estadoReg <> "V" And rTrn.tipoImputacion = "ABO" And rTrn.tipoProducto = "CAI" Then
				'rSal.fecAperturaSaldo = InformacionCliente.verificaContratoIndemniz(gdbc,gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTPr.numTipoProducto, rTrn.categoria, rTrn.perCotizacion, gidUsuarioProceso, gfuncion)
				'lfc: 21-12-09, implementacion sub-categoria saldo CAI
				If rTrn.codOrigenProceso <> "CAMBFOND" Then
					'PCI 11/07/2010 si es CAMBFOND no debe buscar Contrato
					dsPactos = InformacionCliente.verificaContratoIndemnizNew(gdbc, gidAdm, rTrn.idCliente, rCli.tipoTrabajador, rTrn.tipoProducto, rTPr.numTipoProducto, rTrn.subCategoria, rTrn.categoria, rTrn.perCotizacion, gidUsuarioProceso, gfuncion)
					If dsPactos.Tables(0).Rows.Count = 0 Then
						LeerDatosCliente = "84"						 'Num contrato invalido
						Exit Function
					End If
					If IsDBNull(dsPactos.Tables(0).Rows(0).Item("COD_CAUSAL_REZAGO")) Then

					Else
						If dsPactos.Tables(0).Rows(0).Item("COD_CAUSAL_REZAGO") = 0 Then
							rTrn.subCategoria = IIf(IsDBNull(dsPactos.Tables(0).Rows(0).Item("SUB_CATEGORIA")), rTrn.subCategoria, dsPactos.Tables(0).Rows(0).Item("SUB_CATEGORIA"))
							rSal.fecAperturaSaldo = IIf(IsDBNull(dsPactos.Tables(0).Rows(0).Item("FEC_APERTURA_SALDO")), rTrn.perCotizacion, dsPactos.Tables(0).Rows(0).Item("SUB_CATEGORIA"))
						Else
							LeerDatosCliente = CStr(dsPactos.Tables(0).Rows(0).Item("COD_CAUSAL_REZAGO"))							'Num contrato invalido
							Exit Function
						End If
					End If
				End If


				If rSal.fecAperturaSaldo = rTrn.perCotizacion Then
					rSal.estadoSaldo = "L"
				Else
					rSal.estadoSaldo = "R"
				End If
				CrearSaldo()
			End If
		End If

		rTrn.numSaldo = rSal.numSaldo

		clsSal.add(rSal.numSaldo, rSal.valCuoSaldo)

		If rTrn.codOrigenProceso = "CAMBFOND" Or rTrn.codOrigenProceso = "TRAEGCTA" Or rTrn.codOrigenProceso = "TRAEGDES" Or rTrn.codOrigenProceso = "TRAEGEXT" Or rTrn.codOrigenProceso = "TRAEGCHP" Then

			dsAux = InformacionCliente.traerDistribucion(gdbc, gidAdm, rTrn.idCliente, rPro.numProducto)
			If dsAux.Tables(0).Rows.Count = 0 Then
				LeerDatosCliente = "23"				'Distribucion no vigente
				Exit Function
			Else
				rDis = Nothing
				rDis = New ccDistribuciones(dsAux)
			End If

		End If

		'lfc: cap-1621994 plv.1255493 - sobregiro, saldo cero y rentabilidad negativa
		'If gcodAdministradora = 1032 Or gcodAdministradora = 1033 Then
		'    If gcodOrigenProceso = "RECAUDAC" Or gcodOrigenProceso = "REREZSEL" And gcodOrigenProceso = "REREZMAS" Then
		'        If rTrn.tipoImputacion = "ABO" And rTrn.codDestinoTransaccion = "CTA" And rTrn.tipoFondoDestinoCal <> "C" Then
		'            If rTrn.tipoRezago = 38 And rSal.valCuoSaldo = 0 Then
		'                Try
		'                    Dim valMontoRentabilidad As Decimal = Mat.Redondear(Mat.Redondear(rTrn.valCuoPatrFrecCal * gvalMlCuotaDestinoC, 0) - rTrn.valMlPatrFrecCal, 2)
		'                    If valMontoRentabilidad < 0 Then
		'                        ' If Mat.Redondear(valMontoRentabilidad / rTrn.valMlValorCuota, 2) < 0 Then
		'                        LeerDatosCliente = "87" 'cliente inconsistente
		'                        Exit Function
		'                        '  End If
		'                    End If
		'                Catch ex1 As Exception
		'                    Dim aa As String = ex1.Message
		'                End Try

		'            End If
		'        End If
		'    End If
		'End If

	End Function

	Private Function LeerDatosClienteCambioFondo() As String

		'Leo registro clientes for update
		If rTrn.idCliente <> gIdClienteAnterior Then

			clsSal = New INESaldo()
			clsSal.clear()

			If rTrn.idCliente = 0 Then
				gIdClienteAnterior = -1
			Else
				gIdClienteAnterior = rTrn.idCliente
			End If
			blIgnorarCliente = False

			dsAux = InformacionCliente.traercliupdate(gdbc, gidAdm, rTrn.idCliente, gnumeroId, gidUsuarioProceso, gfuncion)
			If dsAux.Tables(0).Rows.Count = 0 Then
				blAcreditarARezago = True
				rTrn.codCausalRezagoCal = "11"
				Exit Function
			Else
				rCli = Nothing
				rCli = New ccClientes(dsAux)
				InformacionCliente.crearBloqueoCliente(gdbc, gidAdm, rTrn.idCliente, gnumeroId, gidUsuarioProceso, gfuncion)
			End If
		Else
			If blIgnorarCliente Then
				blIgnorar = True
			End If
		End If

		'Leo tipo producto vigente
		dsAux = InformacionCliente.traerTipoProductoVig(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto)
		If dsAux.Tables(0).Rows.Count = 0 Then
			rTPr = Nothing
			rTPr = New ccTiposProducto(dsAux.Tables(0).NewRow)
			blAcreditarARezago = True
			rTrn.codCausalRezagoCal = "20"			 'Tipo producto no vigente
			GenerarLog("A", 0, "Hebra " & gIdHebra & " - Tipo producto no vigente: " & Trim(rTrn.tipoProducto), gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
			Exit Function
		Else
			rTPr = Nothing
			rTPr = New ccTiposProducto(dsAux)
			rTrn.tipoFondoDestinoCal = DeterminaFondoDestino()
		End If


		'Leo producto vigente destino
		dsAux = InformacionCliente.traerProductoVig(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.tipoFondoDestinoCal)
		If dsAux.Tables(0).Rows.Count = 0 Then
			rPro = Nothing
			rPro = New ccProductos(dsAux.Tables(0).NewRow)
			If Not blIgnorar Then
				CrearProducto()
			End If
		Else
			rPro = Nothing
			rPro = New ccProductos(dsAux)
		End If



		'LFC:26-03-09-- hacer distincion para apv, por reg_tributario, subcategoria
		If rTrn.tipoProducto = "CCV" Or rTrn.tipoProducto = "CVC" Then
			dsAux = InformacionCliente.traerSaldoVigApv(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.tipoFondoDestinoCal, rTrn.categoria, rTrn.subCategoria, rTrn.codRegTributario, rTPr.numTipoProducto)

		ElseIf rTrn.tipoProducto = "CAI" And rTrn.tipoImputacion = "ABO" Then
			'dsAux = Kernel.Producto.traerSaldo(gdbc,-1, -1, -1)    'siempre crear un nuevo saldo para cai

			'Se comenta esta linea ya que no deberia ir a buscar con numero de solicitud. Genera Duplicado.
			'''LFC://04/01/10 IMPLEMENTACION SUB_CATEGORIA SALDO CAI, NO DUPLICAR SALDOS EN AAA_SALDOS
			'dsAux = InformacionCliente.traerSaldoCaiSinBloqueo(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.tipoFondoDestinoCal, rTrn.categoria, rTrn.subCategoria, rTrn.numReferenciaOrigen1)

			dsAux = Sys.IngresoEgreso.InformacionCliente.traerSaldoCaiCategoria(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.tipoFondoDestinoCal, rTrn.categoria, rTrn.subCategoria)

		Else
			'Leo saldo vigente destino
			dsAux = InformacionCliente.traerSaldoVig(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.tipoFondoDestinoCal, rTrn.categoria, rTPr.numTipoProducto)
		End If


		'>>>>>>>>>>>>>>---- antes
		'Leo saldo vigente destino
		''''''''dsAux = InformacionCliente.traerSaldoVig(gdbc,gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.tipoFondoDestinoCal, rTrn.categoria, rTPr.numTipoProducto)
		'<<<<<<<<<<<<<<<<<<<<<<<<----------------------

		If dsAux.Tables(0).Rows.Count = 0 Then
			rSal = Nothing
			rSal = New ccSaldos(dsAux.Tables(0).NewRow)
			If Not blIgnorar Then
				If rTrn.tipoProducto = "CAI" Then
					'rSal.fecAperturaSaldo = InformacionCliente.verificaContratoIndemniz(gdbc,gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTPr.numTipoProducto, rTrn.categoria, rTrn.perCotizacion, gidUsuarioProceso, gfuncion)
					'lfc: 21-12-09, implementacion sub-categoria saldo CAI
					If rTrn.codOrigenProceso <> "CAMBFOND" Then
						'PCI :11/07/2010. Para CAMBFOND no debe buscar encontrato
						Dim dsPactos As New DataSet()
						dsPactos = InformacionCliente.verificaContratoIndemnizNew(gdbc, gidAdm, rTrn.idCliente, rCli.tipoTrabajador, rTrn.tipoProducto, rTPr.numTipoProducto, rTrn.subCategoria, rTrn.categoria, rTrn.perCotizacion, gidUsuarioProceso, gfuncion)
						If dsPactos.Tables(0).Rows.Count = 0 Then
							blAcreditarARezago = True
							rTrn.codCausalRezagoCal = "84"
							Exit Function
						End If
						If IsDBNull(dsPactos.Tables(0).Rows(0).Item("COD_CAUSAL_REZAGO")) Then

						Else
							If dsPactos.Tables(0).Rows(0).Item("COD_CAUSAL_REZAGO") = 0 Then
								rTrn.subCategoria = IIf(IsDBNull(dsPactos.Tables(0).Rows(0).Item("SUB_CATEGORIA")), rTrn.subCategoria, dsPactos.Tables(0).Rows(0).Item("SUB_CATEGORIA"))
								rSal.fecAperturaSaldo = IIf(IsDBNull(dsPactos.Tables(0).Rows(0).Item("FEC_APERTURA_SALDO")), rTrn.fecValorCuota, dsPactos.Tables(0).Rows(0).Item("FEC_APERTURA_SALDO"))
							Else
								blAcreditarARezago = True
								rTrn.codCausalRezagoCal = CStr(dsPactos.Tables(0).Rows(0).Item("COD_CAUSAL_REZAGO"))								  'Num contrato invalido
								Exit Function

							End If
						End If

						If rSal.fecAperturaSaldo = rTrn.perCotizacion Then
							rSal.estadoSaldo = "L"
						Else
							rSal.estadoSaldo = "R"
						End If

					End If
				Else
					rSal.fecAperturaSaldo = gfecAcreditacion
				End If

				CrearSaldo()
			End If

		Else
			rSal = Nothing
			rSal = New ccSaldos(dsAux)
		End If

		rTrn.numSaldo = rSal.numSaldo

		clsSal.add(rSal.numSaldo, rSal.valCuoSaldo)

		'Leo distribucion destino
		dsAux = InformacionCliente.traerDistribucion(gdbc, gidAdm, rTrn.idCliente, rPro.numProducto)
		If dsAux.Tables(0).Rows.Count = 0 Then

			rDis = Nothing
			rDis = New ccDistribuciones(dsAux.Tables(0).NewRow)
			If Not blIgnorar Then
				CrearDistribucion()				' FONDO DESTINO
			End If
			rDis.porcDistribucion = rCam.porcDistribucionDestino

			''''''--.-- % recauda
			'''''rDis.porcDistribucionReca = rCam.porcRecaudacion

		Else
			rDis = Nothing
			rDis = New ccDistribuciones(dsAux)
			If rCam.tipoDistribucion = "CFN" Then
				modificarDistibucionDestino()
			End If
		End If


	End Function

	Private Sub LeerMovCambioFondo()

		'Leo registro acr_mov_cambios_fondos por secuencia
		dsAux = CambioFondo.cabecera.traer(gdbc, gidAdm, rTrn.seqMvtoOrigen)
		If dsAux.Tables(0).Rows.Count = 0 Then
			blIgnorar = True
			rTrn.codError = 15344
			GenerarLog("A", 15344, "Hebra " & gIdHebra & " - Secuencia: " & rTrn.seqMvtoOrigen, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
			rCam = Nothing
			rCam = New ccMovCambiosFondos(dsAux.Tables(0).NewRow)
		Else
			rCam = Nothing
			rCam = New ccMovCambiosFondos(dsAux)

			If rCam.estadoCambio = "RE" Then
				blIgnorar = True
				blIgnorarCliente = True

				GenerarLog("A", 2605, "Hebra " & gIdHebra & " - Solicitud con estado REchazada, Secuencia: " & rTrn.seqMvtoOrigen, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

				'rTrn.codError = 2605
				'GenerarLog("A", 2605, "Hebra " & gIdHebra & " - Estado Cambio Fondo RECHAZADO, Secuencia: " & rTrn.seqMvtoOrigen, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

			End If
		End If


	End Sub

	Private Sub LlenaValoresIniciales()

		rTrn.fecAcreditacion = gfecAcreditacion
		rTrn.perContable = gperContable
		gtipoFondoDestinoOriginal = rTrn.tipoFondoDestinoCal

		Select Case True

			Case gcodOrigenProceso = "LIQBONNO" Or _
			  gcodOrigenProceso = "LIQBONEX" Or _
			  gcodOrigenProceso = "TRAINCTA" Or _
			  gcodOrigenProceso = "AJUSELEC" Or _
			  gcodOrigenProceso = "AJUMASIV" Or _
			  gcodOrigenProceso = "RETCCVAD" Or _
			  gcodOrigenProceso = "RETCAIFO" Or _
			  gcodOrigenProceso = "RETCAVAD" Or _
			  gcodOrigenProceso = "RETCAVFO" Or _
			  gcodOrigenProceso = "TRAINAPV" Or _
			  gcodOrigenProceso = "TRAINEXT" Or _
			  gcodOrigenProceso = "BEFACAJU" Or _
			 gcodOrigenProceso = "COBPRIMA"

				'se añade cobprima, porque PLV valorizará con fechas ya definidas


			Case rTrn.codOrigenTransaccion = "REZ"

				If IsNothing(rTrn.fecValorCuotaCaja) Then
					rTrn.fecValorCuotaCaja = rTrn.fecValorCuota
					rTrn.valMlValorCuotaCaja = rTrn.valMlValorCuota
				End If

				rTrn.fecValorCuota = gfecValorCuota

			Case Else

				rTrn.fecValorCuota = gfecValorCuota

		End Select

		'rTrn.perCuatrimestre = gperCuatrimestre

		rTrn.idFuncionUltModifReg = gfuncion
		rTrn.idUsuarioUltModifReg = gidUsuarioProceso

		gcomisionPorcentual = 0
		gcomisionFija = 0


		If IsNothing(rTrn.perCotizacion) Then
			gvalorUF = 0
			Exit Sub
		End If

		If CDate(rTrn.perCotizacion) > rTrn.fecAcreditacion Or rTrn.perCotizacion < gFecInicioSistema Or rTrn.tipoPago = 3 Then
			gvalorUF = 0			 'se rezagará por pago anticipado
			Exit Sub
		End If

		'--OS:2035792 -->>>>>
		'sin exceso para CAV y CCV .deposito directo //no necesario valor uf
		If rTrn.codMvto = 410290 Or rTrn.codMvto = 410291 Or _
		   rTrn.codMvto = 210290 Or rTrn.codMvto = 210291 Or _
		   rTrn.codMvto = 610290 Or rTrn.codMvto = 610291 Then		  'lfc: 15/10/09 CA-2009100123
			gvalorUF = 0
			Exit Sub
		End If
		'---<<<<<<<<<<----------------------------------------------------


		gfecUFRenta = gfecUFRenta.DaysInMonth(Year(rTrn.perCotizacion), Month(rTrn.perCotizacion)) & "/" & Month(rTrn.perCotizacion).ToString.PadLeft(2, "0") & "/" & Year(rTrn.perCotizacion)

		dsAux = parValorUf.traer(gdbc, New Object() {"VFEC_VALOR"}, New Object() {gfecUFRenta}, New Object() {"DATE"})


		If dsAux.Tables(0).Rows.Count = 0 Then

			'lfc:05/05/09 no considerar ajustes   -----------------------------------------------
			Dim dsMvtoAcred As New DataSet()
			Dim rMovAcred As ccAcrMvtoAcreditacion
			dsMvtoAcred = parCodMvto.traer(gdbc, New Object() {"VID_ADM", "VCOD_MVTO"}, New Object() {gidAdm, rTrn.codMvto}, New Object() {"INTEGER", "STRING"})
			If dsMvtoAcred.Tables(0).Rows.Count > 0 Then
				rMovAcred = New ccAcrMvtoAcreditacion(dsMvtoAcred.Tables(0))
				If rMovAcred.tipoMvto <> "COT" Then
					gvalorUF = 0
					Exit Sub
				End If
			End If
			'---------------------------------------------



			blErrorFatal = True
			GenerarLog("E", 15340, "Hebra " & gIdHebra & " - Fecha: " & gfecUFRenta.Date, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
			blIgnorar = True
			'Throw New SondaException(15340) '"No existe valor UF
			gvalorUF = 0
		Else
			gvalorUF = dsAux.Tables(0).Rows(0).Item("VAL_UF")
		End If

	End Sub

	Private Sub CrearSaldosMovimientos(Optional ByVal rentabilidad As Boolean = False)
		'0 = Movimiento principal
		'1 = Abono por exceso
		'2 = Abono o Cargo por compensacion
		'3 = cargo por transferencia de prima o comision

		Dim blCotizacion As Boolean

		CalcularSaldoMov(rentabilidad)

		blCotizacion = rMovAcr.tipoMvto = "COT" Or rMovAcr.tipoMvto = "COP" Or _
		   rMovAcr.tipoMvto = "COS" Or rMovAcr.tipoMvto = "COG"


		gRegistrosAcreditados = gRegistrosAcreditados + 1

	End Sub

	Private Sub CrearSaldosMovimientosTrnComis()
		'0 = Movimiento principal
		'1 = Abono por exceso
		'2 = Abono o Cargo por compensacion
		'3 = cargo por transferencia de prima o comision

		Dim blCotizacion As Boolean

		'SE DUPLICA EL CARGO POR COMISIONES, EN EL MONTO DEL MVTO Y LUEGO COMO TRANSFERENCIA.
		' LA SIGUIENTE RUTINA REALIZA EL CALCULO, ESTA RUTINA SÓLO ES LLAMADA POR LOS PERIODOS POSTERIORES 
		' A JULIO DE 2009....
		' DESCOMENTAR 
		'CalcularSaldoMov()

		blCotizacion = rMovAcr.tipoMvto = "COT" Or rMovAcr.tipoMvto = "COP" Or _
		   rMovAcr.tipoMvto = "COS" Or rMovAcr.tipoMvto = "COG"


		gRegistrosAcreditados = gRegistrosAcreditados + 1

	End Sub

	Private Sub CrearRezagos()

		If gtipoProceso = "AC" And (rMovAcr.tipoMvto <> "DEC" Or rMovAcr.indCotizacion = "S") Then
			'Solo admite DEC si indCotizacion="S" (caso de las DNP) dejando fuera los Movimientos de personal


			' lfc: 04/03/2010 - os_2701495 CA-2010010106 --- Las DNP no van a rezagos--------------------------
			If rTrn.codMvto = "110253" Or _
			   rTrn.codMvto = "210253" Or _
			   rTrn.codMvto = "310253" Or _
			   rTrn.codMvto = "410253" Or _
			   rTrn.codMvto = "510253" Or _
			   rTrn.codMvto = "610253" Or _
			   rTrn.codMvto = "710253" Then
				rTrn.seqMvtoDestino = 0
			Else

				rRez.numRezago = Sys.IngresoEgreso.Rezagos.crearconseq(gdbc, gidAdm, rRez.numPlanilla, _
				  rRez.folioPlanilla, rRez.folioConvenio, rRez.numPagina, rRez.seqLinea, _
				  rRez.perCotiza, rRez.tipoProducto, rRez.tipoFondo, _
				  rRez.codMvto, rRez.tipoPago, rRez.tipoPlanilla, _
				  rRez.tipoRemuneracion, rRez.tipoEntidadPagadora, _
				  rRez.fecOperacion, rRez.fecInicioGratificacion, _
				  rRez.fecFinGratificacion, rRez.idCliente, rRez.idEmpleadorOri, _
				  rRez.idPersonaOri, rRez.apPaternoOri, rRez.apMaternoOri, _
				  rRez.nombreOri, rRez.nombreAdicionalOri, rRez.codigoSoundex, _
				  rRez.idEmpleador, rRez.idPersona, rRez.apPaterno, rRez.apMaterno, _
				  rRez.nombre, rRez.nombreAdicional, rRez.valMlRentaImponible, _
				  rRez.valMlMontoNominal, rRez.valMlMonto, rRez.valMlInteres, rRez.valMlReajuste, _
				  rRez.valCuoMonto, rRez.valCuoInteres, rRez.valCuoReajuste, _
				  rRez.valMlAdicional, rRez.valMlAdicionalInteres, _
				  rRez.valMlAdicionalReajuste, rRez.valCuoAdicional, _
				  rRez.valCuoAdicionalInteres, rRez.valCuoAdicionalReajuste, _
				  rRez.fecValorCuota, rRez.valMlValorCuota, rRez.numCuotasPactadas, rRez.numCuotasPagadas, _
				  rRez.codOrigenMvto, rRez.codOrigenProceso, rRez.fecContableRezago, _
				  rRez.fecOperacionAdmOrigen, rRez.perContableRezago, _
				  rRez.tipoRezago, rRez.codCausalRezago, rRez.codCausalOriginal, rRez.fecReclasificacion, _
				  rRez.valCuoAjusteDecimal, rRez.codAdmOrigen, rRez.numDictamenOri, _
				  rRez.fecNotificacion, rRez.codTrabajoPesado, _
				  rRez.puestoTrabajoPesado, rRez.tasaTrabajoPesadoOri, _
				  rRez.tasaTrabajoPesado, rRez.estadoRezago, _
				  rRez.fecEstadoRezago, rRez.indRegimenTributario, rRez.numContrato, rRez.categoria, _
				  rRez.codMvtoPrim, rRez.codMvtoIntreaPrim, rRez.sexo, rRez.tasaPrima, rRez.valMlRentaImponibleSis, rRez.valMlPrimaseguro, rRez.valMlPrimaseguroInt, rRez.valMlPrimaseguroRea, rRez.valCuoPrimaseguro, rRez.valCuoPrimaseguroInt, rRez.valCuoPrimaseguroRea, rRez.valMlExcesoAfi, rRez.valMlExcesoEmp, _
				  gidUsuarioProceso, gfuncion, rRez.tipoOrigenDigitacion)

				rTrn.seqMvtoDestino = rRez.numRezago

			End If

		End If


		gvalMlAbonosCtaAcr += rRez.valMlMonto + rRez.valMlInteres + rRez.valMlReajuste + _
		 rRez.valMlAdicional + rRez.valMlAdicionalInteres + rRez.valMlAdicionalReajuste + _
		   rRez.valMlPrimaseguro + rRez.valMlPrimaseguroInt + rRez.valMlPrimaseguroRea		  'SIS//

		gvalCuoAbonosCtaAcr += rRez.valCuoMonto + rRez.valCuoInteres + rRez.valCuoReajuste + _
		  rRez.valCuoAdicional + rRez.valCuoAdicionalInteres + rRez.valCuoAdicionalReajuste + _
		   rRez.valCuoPrimaseguro + rRez.valCuoPrimaseguroInt + rRez.valCuoPrimaseguroRea		  'SIS//

		gRegistrosAcreditados = gRegistrosAcreditados + 1

	End Sub


	Private Sub modificarTransferencia()
		If gtipoProceso = "AC" Then
			Transferencia.Egresos.modEstadoTransferencia(gdbc, gidAdm, rTrf.seqMvto, rTrf.estadoTransferencia, rTrf.fecTransferencia, gidUsuarioProceso, gfuncion)
		End If
	End Sub

	Private Sub CrearTransferencia()
		Dim ds As DataSet
		Dim i As Integer
		Dim rTrf2 As ccTransferencias

		If gtipoProceso = "AC" Then

			ds = Transferencia.Recaudacion.buscar(gdbc, gidAdm, rTrn.codOrigenProceso, rTrn.usuarioProceso, rTrn.numeroId, rTrn.seqRegistro, Nothing)

			For i = 0 To ds.Tables(0).Rows.Count - 1

				rTrf2 = New ccTransferencias(ds.Tables(0).Rows(i))

				rTrf.valMlTransferenciaRec = rTrf2.valMlTransferencia
				rTrf.valMlInteres = rTrf2.valMlInteres
				rTrf.valMlReajuste = rTrf2.valMlReajuste
				rTrf.valCuoInteres = 0
				rTrf.valCuoReajuste = 0

				If rTrn.codOrigenProceso = "RECAUDAC" Then				'Or rTrn.codOrigenProceso = "TRAINREZ" Or rTrn.codOrigenProceso = "TRAINRZC" Or rTrn.codOrigenProceso = "TRAIPAGN" Then
					rTrf.valCuoTransferenciaRec = Mat.Redondear(rTrf2.valMlTransferencia / rTrn.valMlValorCuotaCaja, 2)
					If rTrn.valCuoPatrFrecCal > rTrf.valCuoTransferenciaRec Then
						rTrf.valCuoInteres = Mat.Redondear(rTrf2.valMlInteres / rTrn.valMlValorCuotaCaja, 2)
						If rTrf.valCuoTransferenciaRec + rTrf.valCuoInteres > rTrn.valCuoPatrFrecCal Then
							rTrf.valCuoInteres = rTrn.valCuoPatrFrecCal - rTrf.valCuoTransferenciaRec
						Else
							rTrf.valCuoReajuste = Mat.Redondear(rTrf2.valMlReajuste / rTrn.valMlValorCuotaCaja, 2)
							If rTrf.valCuoTransferenciaRec + rTrf.valCuoInteres + rTrf.valCuoReajuste > rTrn.valCuoPatrFrecCal Then
								rTrf.valCuoReajuste = rTrn.valCuoPatrFrecCal - rTrf.valCuoTransferenciaRec - rTrf.valCuoInteres
							End If
						End If
					End If
					If rTrn.valCuoPatrFrecCal - (rTrf.valCuoTransferenciaRec + rTrf.valCuoInteres + rTrf.valCuoReajuste) > 0 Then

						rTrf.valCuoTransferenciaRec += rTrn.valCuoPatrFrecCal - (rTrf.valCuoTransferenciaRec + rTrf.valCuoInteres + rTrf.valCuoReajuste)
					End If

				Else
					rTrf.valCuoTransferenciaRec = rTrf2.valCuoTransferencia
					rTrf.valCuoInteres = rTrf2.valCuoInteres
					rTrf.valCuoReajuste = rTrf2.valCuoReajuste
				End If

				rTrf.codAdmDestino = rTrf2.codAdmDestino
				rTrf.numRefOrigen = rTrn.seqRegistro

				rTrf.seqMvto = Transferencia.Egresos.crearconseq(gdbc, _
				  gidAdm, rTrf.seqMvto, rTrf.numRefOrigen, rTrf.tipoProducto, rTrf.tipoFondo, rTrf.perCotiza, rTrf.numPlanilla, _
				  rTrf.tipoPago, rTrf.numCaja, rTrf.numPagina, rTrf.seqLinea, rTrf.fecOperacion, _
				  rTrf.idCliente, rTrf.idEmpleador, rTrf.idPersona, rTrf.apPaterno, rTrf.apMaterno, _
				  rTrf.nombre, rTrf.nombreAdicional, rTrf.valMlRentaImponible, rTrf.fecValorCuota, _
				  rTrf.valMlValorCuota, rTrf.valMlTransferencia, rTrf.valCuoTransferencia, _
				  rTrf.valMlComisionApv, rTrf.valMlTransferenciaRec, rTrf.valCuoTransferenciaRec, _
				  rTrf.valMlInteres, rTrf.valCuoInteres, rTrf.valMlReajuste, rTrf.valCuoReajuste, _
				  rTrf.numSolicitudTransf, rTrf.fecAcreditacion, rTrf.fecAcreditacionTransf, _
				  rTrf.perContable, rTrf.perContableTransf, rTrf.codMvto, rTrf.codAdmDestino, _
				  rTrf.estadoTransferencia, rTrf.fecTransferencia, rTrf.perProceso, gidUsuarioProceso, gfuncion)

				rTrn.seqDestinoTrfCal = rTrf.seqMvto

			Next
		End If

		gRegistrosAcreditados = gRegistrosAcreditados + 1

		gvalMlTransferenciaAcr += rTrf.valMlTransferenciaRec + rTrf.valMlInteres + rTrf.valMlReajuste
		gvalCuoTransferenciaAcr += rTrf.valCuoTransferenciaRec + rTrf.valCuoInteres + rTrf.valCuoReajuste


	End Sub

	Private Sub CrearProducto()



		If gtipoProceso <> "AC" Then
			Exit Sub
		End If


		rPro.numProducto = Sys.Kernel.Producto.crearConSecuencia(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto, _
		  rTrn.tipoFondoDestinoCal, rTPr.numTipoProducto, "N", Nothing, Nothing, _
		   Nothing, gfecAcreditacion, Nothing, rTrn.numReferenciaOrigen1, 0, rCam.tipoCuenta, _
		  gidUsuarioProceso, gfuncion)


	End Sub

	Private Sub CrearSaldo()
		'modificar la subcategoria,cambiofondo,cai
		'Dim subCategoria As Integer

		If gtipoProceso <> "AC" And rTrn.codOrigenProceso = "CAMBFOND" Then
			Exit Sub
		End If

		' subCategoria = rTrn.subCategoria

		'if rTrn.codOrigenProceso = "CAMBFOND" And rTrn.tipoProducto = "CAI" Then subCategoria = 0

		rSal.numSaldo = Sys.Kernel.Producto.crearSaldoConSecuencia(gdbc, _
		  gidAdm, _
		  rTrn.idCliente, _
		  rTrn.categoria, _
		  rTrn.subCategoria, _
		  rTrn.tipoProducto, _
		  rTrn.tipoFondoDestinoCal, _
		  rTPr.numTipoProducto, _
		  rPro.numProducto, _
		  rTrn.numReferenciaOrigen1, _
		  0, _
		  0, _
		  0, _
		  gfecAcreditacion, _
		  0, _
		  rTrn.codRegTributario, _
		  rSal.fecAperturaSaldo, _
		  Nothing, _
		  rSal.estadoSaldo, _
		  Nothing, _
		  gidUsuarioProceso, _
		  gfuncion)
	End Sub

	Private Sub CrearDistribucion()
		If gtipoProceso = "AC" Then

			If rCam.tipoDistribucion = "CFA" Then
				Select Case rCam.porcDistribucionReal
					Case 20 : rDis.porcDistribucion = 20
					Case 25 : rDis.porcDistribucion = 40
					Case 33 : rDis.porcDistribucion = 60
					Case 50 : rDis.porcDistribucion = 80
					Case 100 : rDis.porcDistribucion = 100
				End Select
				rDis.tipoDistribucion = "AS"

			Else
				rDis.porcDistribucion = rCam.porcDistribucionDestino
				rDis.tipoDistribucion = rCam.tipoDistribucion
				rDis.porcDistribucionReca = rCam.porcRecaudacion

			End If

			'se modifica creacion ---antes:rDis.porcDistribucionReca  ---ahora:rCam.porcRecaudacion
			rDis.seqDistribucion = InformacionCliente.crearDistribucion(gdbc, gidAdm, rTrn.idCliente, rPro.numProducto, _
			 rTrn.tipoProducto, rTrn.tipoFondoDestinoCal, _
			 rCam.tipoFondoOrigen, rDis.tipoFondoAnterior, rTPr.numTipoProducto, _
			 rDis.porcDistribucion, rCam.porcRecaudacion, rCam.numSolicitudAut, _
			 gfecAcreditacion, rDis.tipoDistribucion, rCam.fecCambio, _
			 rCam.tipoCuenta, gidUsuarioProceso, gfuncion)


		End If

	End Sub

	Private Sub modificarDistibucionOrigen()
		Dim tipoCuenta As Integer


		If gtipoProceso = "AC" And (rCam.tipoDistribucion = "CFA" Or rCam.tipoDistribucion = "CFN") Then

			tipoCuenta = 2
			'rDis.porcDistribucion = rDis.porcDistribucion - rCam.porcDistribucionDestino

			Select Case rCam.porcDistribucionReal
				Case 20 : rDis.porcDistribucion = 80
				Case 25 : rDis.porcDistribucion = 60
				Case 33 : rDis.porcDistribucion = 40
				Case 50 : rDis.porcDistribucion = 20
				Case 100 : rDis.porcDistribucion = 0
			End Select

			If rCam.tipoDistribucion = "CFA" Then
				rDis.tipoDistribucion = "AS"

			Else
				rDis.tipoDistribucion = rCam.tipoDistribucion
			End If

			'se modifica creacion ---antes:rDis.porcDistribucionReca  ---ahora:rCam.porcRecaudacion
			Kernel.Producto.modificarDistribucion(gdbc, gidAdm, rDis.idCliente, rDis.numProducto, rDis.seqDistribucion, rDis.tipoProducto, rDis.tipoFondo, rDis.tipoFondoAnterior, rDis.tipoFondoAnterior2, rDis.numTipoProducto, rDis.porcDistribucion, rCam.porcRecaudacion, rCam.numSolicitudAut, gfecAcreditacion, Nothing, rDis.tipoDistribucion, rCam.fecIngReg, tipoCuenta, gidUsuarioProceso, gfuncion)

			InformacionCliente.modProdTipoCuenta(gdbc, gidAdm, rTrn.idCliente, rPro.numProducto, tipoCuenta, gidUsuarioProceso, gfuncion)

		End If

	End Sub

	Private Sub modificarDistibucionDestino()


		If gtipoProceso <> "AC" Then
			Exit Sub
		End If

		If rCam.tipoDistribucion = "CFA" Then
			rDis.tipoCuenta = 1
			InformacionCliente.modProdTipoCuenta(gdbc, gidAdm, rTrn.idCliente, rPro.numProducto, rDis.tipoCuenta, gidUsuarioProceso, gfuncion)
			rDis.tipoDistribucion = "AS"

			Select Case rCam.porcDistribucionReal
				Case 20 : rDis.porcDistribucion = 20
				Case 25 : rDis.porcDistribucion = 40
				Case 33 : rDis.porcDistribucion = 60
				Case 50 : rDis.porcDistribucion = 80
				Case 100 : rDis.porcDistribucion = 100
			End Select
		Else
			rDis.tipoDistribucion = rCam.tipoDistribucion
		End If


		'se modifica creacion ---antes:rDis.porcDistribucionReca  ---ahora:rCam.porcRecaudacion
		Kernel.Producto.modificarDistribucion(gdbc, gidAdm, rDis.idCliente, rDis.numProducto, rDis.seqDistribucion, rDis.tipoProducto, rDis.tipoFondo, rTrn.tipoFondoOrigen, rDis.tipoFondoAnterior, rDis.numTipoProducto, rDis.porcDistribucion, rCam.porcRecaudacion, rCam.numSolicitudAut, gfecAcreditacion, Nothing, rDis.tipoDistribucion, rCam.fecIngReg, rDis.tipoCuenta, gidUsuarioProceso, gfuncion)

	End Sub

	Private Sub ActualizarFondoRecaudacion()

		If gtipoProceso = "AC" Then


			If rCam.tipoDistribucion <> "CFA" And rCam.tipoDistribucion <> "CFI" Then

				InformacionCliente.modProdTipoCuenta(gdbc, gidAdm, rTrn.idCliente, rPro.numProducto, _
				  rCam.tipoCuenta, gidUsuarioProceso, gfuncion)
				InformacionCliente.modDistrTipoCuenta(gdbc, gidAdm, rTrn.idCliente, rPro.numProducto, _
				  rDis.seqDistribucion, rCam.tipoCuenta, gidUsuarioProceso, _
				  gfuncion)

				rTPr.tipoEleccionFondos = "M"


			Else
				'OJO AVISAR A MANUEL AVALOS DE ESTE CODIGO. PCI 
				rTPr.tipoEleccionFondos = "A"
			End If

			If rCam.tipoCuenta = 1 Then

				rTPr.fecEleccionFondos = CDate(rCam.fecIngReg).Date
				rTPr.tipoFondoRecaudacion = rCam.tipoFondoDestino
				Kernel.TipoProducto.modificar(gdbc, gidAdm, rTPr.idCliente, rTPr.numTipoProducto, rTPr.tipoProducto, rTPr.fecAperturaTipoProducto, rTPr.fecCierreTipoProducto, rTPr.fecOrigen, rTPr.codInstitucionOrigen, rTPr.codInstitucionDestino, rTPr.codInstitucionFusion, rTPr.perPrimerPago, rTPr.fecUltimoPago, rTPr.indCotizante, rTPr.seqDireccion, rTPr.indEnvioCartola, rTPr.tipoCartola, rTPr.numSolicitudAut, rCam.numSolicitudAut, rTPr.tipoOrigenProducto, rTPr.indFuturoFinProducto, rTPr.tipoFinProducto, rTPr.fecFinProducto, rTPr.tipoFondoRecaudacion, rTPr.valMlSaldoEmbargo, rTPr.fecEmbargo, rTPr.codRegTributario, rTPr.fecRegTributario, rTPr.tipoEleccionFondos, rTPr.fecEleccionFondos, rTPr.estadoProducto, rTPr.fecEstadoProducto, gidUsuarioProceso, gfuncion)

			End If

		End If


	End Sub



	Private Sub ActualizaMovimientos()

		If gtipoProceso = "AC" Then
			If rTrn.codOrigenTransaccion = "CTA" And rTrn.codDestinoTransaccionCal = "CTA" Then
				If rTrn.seqMvtoOrigen > 0 Then

					SaldosMovimientos.modRelacionOrigen(gdbc, gidAdm, rTrn.idCliente, rTrn.seqMvtoOrigen, sMov.item(0).mov.seqMvto, gidUsuarioProceso, gfuncion)

					If rTrn.indMvtoVisibleCartola = "N" Then
						SaldosMovimientos.modVisible(gdbc, gidAdm, rTrn.idCliente, rTrn.seqMvtoOrigen, "N", gidUsuarioProceso, gfuncion)
					End If
				End If
			Else
				If rTrn.codOrigenTransaccion = "REZ" Then
					If rTrn.seqMvtoOrigen > 0 Then
						Sys.IngresoEgreso.Rezagos.cargoRezago(gdbc, gidAdm, rTrn.seqMvtoOrigen, gcodOrigenProceso, gperContable, gfecAcreditacion, gidUsuarioProceso, gfuncion)
					End If
				End If
			End If
		End If

	End Sub
	'Private Sub ActualizaRentas()

	'    Dim tipoCotizacion As String
	'    Dim tipoOrigen As String = "ACR"
	'    Dim ds As DataSet

	'    If IsNothing(rTrn.idEmpleador) Then
	'        blIgnorar = True
	'        rTrn.codError = 15349
	'        GenerarLog("A", rTrn.codError, ControlAcr.LogAcreditacion.obtenerSondaException(gdbc,rTrn.codError), rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
	'        Exit Sub
	'    End If

	'    If rMovAcr.tipoMvto = "DEC" Then

	'        Select Case rTrn.tipoPlanilla

	'            Case 27, 5 : tipoCotizacion = "DES"

	'            Case 17 : tipoCotizacion = "DEP"

	'            Case Else : tipoCotizacion = "DEC"

	'        End Select
	'    Else
	'        tipoCotizacion = rMovAcr.tipoMvto
	'    End If

	'    Select Case rMovAcr.tipoMvto

	'        Case "COT"
	'            Cotizaciones.Rentas.sumar(gdbc,gidAdm, rTrn.idCliente, rTrn.idEmpleador, rTrn.perCotizacion, rTrn.tipoProducto, tipoCotizacion, tipoOrigen, rTrn.valMlRentaImponible, rTrn.valMlPatrFrecActCal, rTrn.valMlValorCuota, gidUsuarioProceso, gfuncion)

	'        Case "COP", "COS", "DEC", "COG"
	'            Cotizaciones.Rentas.sumarCotizaciones(gdbc,gidAdm, rTrn.idCliente, rTrn.idEmpleador, rTrn.perCotizacion, rTrn.tipoProducto, tipoCotizacion, tipoOrigen, rTrn.valMlRentaImponible, rTrn.valMlPatrFrecActCal, rTrn.valMlValorCuota, gidUsuarioProceso, gfuncion)

	'        Case "RCOT"
	'            Cotizaciones.Rentas.restar(gdbc,gidAdm, rTrn.idCliente, rTrn.idEmpleador, rTrn.perCotizacion, rTrn.tipoProducto, tipoCotizacion, tipoOrigen, rTrn.valMlRentaImponible, rTrn.valMlPatrFrecActCal, rTrn.valMlValorCuota, gidUsuarioProceso, gfuncion)

	'        Case "RCOP", "RCOS", "RCOG", "RDEC"
	'            Cotizaciones.Rentas.eliminar(gdbc,gidAdm, rTrn.idCliente, rTrn.idEmpleador, rTrn.perCotizacion, rTrn.tipoProducto, tipoCotizacion)

	'    End Select

	'End Sub

	Private Sub IniProceso()


		blErrorFatal = False
		gEstadoError = "ER"


		gTotRegistrosIgnorados = 0
		gTotRegistrosSimulados = 0
		gTotRegistrosAcreditados = 0

		'dbc = New OraConn()

		IniciarLog()

		clsAux = New INETotalesAcr.Auxiliar(gdbc)
		' clsConta = New INETotalesAcr.Contabilidad(gdbc)
		clsConta = New WS.IngresoEgresoConta.auxiliarContabilidad.Contabilidad(gdbc)

		gcodAdministradora = ParametrosINE.ParametrosGenerales.codigoAdministradora(gdbc, gidAdm)

		'rMov = Nothing
		dsMov = SaldosMovimientos.traer(gdbc, -1, Nothing, Nothing, Nothing)
		'rMov = New ccSaldosMovimientos(dsMov.Tables(0).NewRow)

		rTrf = Nothing
		dsAux = Transferencia.Egresos.traer(gdbc, -1, Nothing)
		rTrf = New ccTransfApv(dsAux.Tables(0).NewRow)

		rRez = Nothing
		dsAux = Sys.IngresoEgreso.Rezagos.traer(gdbc, -1, Nothing)
		rRez = New ccRezagos(dsAux.Tables(0).NewRow)

		rCab = Nothing
		dsAux = ResultadoAcred.EncabezadoAcred.traer(gdbc, -1, Nothing, Nothing, -1, -1)
		rCab = New ccEncabezadoAcred(dsAux.Tables(0).NewRow)

		rComis = Nothing
		dsAux = Auxiliares.Comisiones.traer(gdbc, -1, Nothing)
		rComis = New ccComisiones(dsAux.Tables(0).NewRow)

		rPri = Nothing
		dsAux = PrimasCiasSeguro.traer(gdbc, -1, Nothing, Nothing, -1, Nothing, -1, Nothing)
		rPri = New ccPrimasCiasSeg(dsAux.Tables(0).NewRow)

		'dsTP = InformacionCliente.traerTipoProductoVig(gdbc,-1, Nothing, Nothing)
		'dsP = InformacionCliente.traerProductoVig(gdbc,-1, Nothing, Nothing, Nothing)
		'dsS = InformacionCliente.traerSaldoVig(gdbc,-1, Nothing, Nothing, Nothing, Nothing, Nothing)
		'dsCR = Cotizaciones.ControlRentas.traer(gdbc,-1, Nothing, Nothing, Nothing, Nothing)

		Me.blNocional = False

	End Sub


	Private Function CodMvtoPrimaSis(ByVal tipoProducto As String) As String

		Select Case tipoProducto

			Case "CCO" : Return "120806"
				'Case "CCV" : Return "0"
				'Case "CDC" : Return "0"
				'Case "CAV" : Return "0"
				'Case "CAI" : Return "0"
			Case "CAF" : Return "620806"
				'Case "CVC" : Return "0"
			Case Else
				blErrorFatal = True
				GenerarLog("E", 15342, "Hebra " & gIdHebra & " - Producto: " & Trim(rTrn.tipoProducto) & " prima no valida", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
				blIgnorar = True
				'Throw New SondaException(15342) 'Tipo producto invalido
		End Select


	End Function

	Private Function CodMvtoExcEmp(ByVal tipoProducto As String) As String

		If blExcesosIndep Then

			CodMvtoExcEmp = "110864"
			' lfc: ca-1214034  expasis en TGR 20-05-2019
		ElseIf blExpasisTGR Then
			CodMvtoExcEmp = "110863"
		Else
			Select Case tipoProducto

				Case "CCO" : CodMvtoExcEmp = "110857"
				Case "CCV" : CodMvtoExcEmp = "410857"
				Case "CDC" : CodMvtoExcEmp = "510857"
				Case "CAV" : CodMvtoExcEmp = "210857"
				Case "CAI" : CodMvtoExcEmp = "310857"
				Case "CAF" : CodMvtoExcEmp = "610857"
				Case "CVC" : CodMvtoExcEmp = "710857"
				Case Else
					blErrorFatal = True
					GenerarLog("E", 15342, "Hebra " & gIdHebra & " Codigo exceso Emp Invalido - " & Trim(rTrn.tipoProducto), gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
					blIgnorar = True
					'Throw New SondaException(15342) 'Tipo producto invalido

			End Select
		End If

	End Function


	Private Function CodMvtoExc(ByVal tipoProducto As String) As String

		If blExcesosIndep Then

			CodMvtoExc = "110864"

		Else
			Select Case tipoProducto

				Case "CCO" : CodMvtoExc = "110850"
				Case "CCV" : CodMvtoExc = "410850"
				Case "CDC" : CodMvtoExc = "510850"
				Case "CAV" : CodMvtoExc = "210850"
				Case "CAI" : CodMvtoExc = "310850"
				Case "CAF" : CodMvtoExc = "610850"
				Case "CVC" : CodMvtoExc = "710850"
				Case Else
					blErrorFatal = True
					GenerarLog("E", 15342, "Hebra " & gIdHebra & " - Producto: " & Trim(rTrn.tipoProducto), gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
					blIgnorar = True
					'Throw New SondaException(15342) 'Tipo producto invalido

			End Select
		End If

	End Function


	Private Function CodMvtoCompen(ByVal tipoProducto As String, ByVal tipoImputacion As String) As String


		Select Case tipoProducto
			Case "CCO" : CodMvtoCompen = IIf(tipoImputacion = "ABO", "110974", 120976)
			Case "CCV" : CodMvtoCompen = IIf(tipoImputacion = "ABO", "410974", 420976)
			Case "CDC" : CodMvtoCompen = IIf(tipoImputacion = "ABO", "510974", 520976)
			Case "CAV" : CodMvtoCompen = IIf(tipoImputacion = "ABO", "210974", 220976)
			Case "CAI" : CodMvtoCompen = IIf(tipoImputacion = "ABO", "310974", 320976)
			Case "CAF" : CodMvtoCompen = IIf(tipoImputacion = "ABO", "610974", 620976)
			Case "CVC" : CodMvtoCompen = IIf(tipoImputacion = "ABO", "710974", 720976)
			Case Else
				blErrorFatal = True
				GenerarLog("E", 15342, "Hebra " & gIdHebra & " - Producto: " & Trim(rTrn.tipoProducto), gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
				blIgnorar = True
				'Throw New SondaException(15342) 'Tipo producto invalido
		End Select
		'SNDLFUENTES -codigo especial para acred externa A.P.
		If gcodOrigenProceso = "ACREXIPS" Then
			CodMvtoCompen = IIf(tipoImputacion = "ABO", "110965", "110965")
		End If

	End Function


	Private Sub LimpiarDatos()


		gcausalRezago = Nothing
		gValDif = 0		  ' --> lfc:31/08/2017 añade, peor error de arrastre
		'--.--rTrn.codDestinoTransaccionCal = Nothing
		rTrn.seqMvtoDestino = 0
		'--.--rTrn.codCausalRezagoCal = Nothing
		rTrn.perContable = Nothing
		rTrn.fecAcreditacion = Nothing
		'rTrn.fecValorCuota = Nothing
		'rTrn.valMlValorCuota = 0


		'Acreditacion Externa 20/08/2012.
		'If rTrn.codOrigenProceso <> "RECAUDAC" And rTrn.codOrigenProceso <> "REREZMAS" And _
		'   rTrn.codOrigenProceso <> "REREZSEL" And rTrn.codOrigenProceso <> "REREZCON" And _
		'   rTrn.codOrigenProceso <> "TRAINREZ" And rTrn.codOrigenProceso <> "TRAIPAGN" And _
		'   rTrn.codOrigenProceso <> "TRAIPAGC" And rTrn.codOrigenProceso <> "TRAINRZC" Then

		If rTrn.codOrigenProceso = "RECAUDAC" Or rTrn.codOrigenProceso = "REREZMAS" Or _
		   rTrn.codOrigenProceso = "REREZSEL" Or rTrn.codOrigenProceso = "TRAIPAGN" Then
			rTrn.tasaInteres = 0
		End If

		If rTrn.codOrigenProceso <> "RECAUDAC" And rTrn.codOrigenProceso <> "REREZMAS" And _
		 rTrn.codOrigenProceso <> "REREZSEL" And rTrn.codOrigenProceso <> "REREZCON" And _
		 rTrn.codOrigenProceso <> "TRAINREZ" And rTrn.codOrigenProceso <> "TRAIPAGN" And _
		 rTrn.codOrigenProceso <> "TRAIPAGC" And rTrn.codOrigenProceso <> "TRAINRZC" And _
		 rTrn.codOrigenProceso <> "ACREXIPS" And rTrn.codOrigenProceso <> "ACREXSTJ" And _
		 rTrn.codOrigenProceso <> "ACREXTBF" And rTrn.codOrigenProceso <> "ACREXTGR" And _
		 rTrn.codOrigenProceso <> "AEXDVSTJ" And rTrn.codOrigenProceso <> "ACREXAFC" And _
		 rTrn.codOrigenProceso <> "TRAINCHP" Then

			rTrn.valMlPatrFrecCal = 0
			rTrn.valCuoPatrFrecCal = 0
			rTrn.codDestinoTransaccionCal = Nothing
			rTrn.codCausalRezagoCal = Nothing
		End If
		'FIN MSC

		rTrn.valCuoPatrFrecActCal = 0
		rTrn.valMlPatrFrecActCal = 0
		rTrn.valMlPatrFdesCal = 0
		rTrn.valCuoPatrFdesCal = 0
		rTrn.valMlMvtoCal = 0
		rTrn.valMlReajusteCal = 0
		rTrn.valMlInteresCal = 0
		rTrn.valCuoMvtoCal = 0
		rTrn.valCuoReajusteCal = 0
		rTrn.valCuoInteresCal = 0
		rTrn.valMlAdicionalCal = 0
		rTrn.valMlAdicionalInteresCal = 0
		rTrn.valMlAdicionalReajusteCal = 0
		rTrn.valCuoAdicionalCal = 0
		rTrn.valCuoAdicionalReajusteCal = 0
		rTrn.valCuoAdicionalInteresCal = 0
		rTrn.valMlComisPorcentualCal = 0
		rTrn.valCuoComisPorcentualCal = 0
		rTrn.valMlComisFijaCal = 0
		rTrn.valCuoComisFijaCal = 0
		rTrn.codDestinoExcesoTopeCal = Nothing
		rTrn.seqDestinoExcesoTopeCal = 0
		rTrn.codMvtoExcesoTopeCal = Nothing
		rTrn.valMlExcesoTopeCal = 0
		rTrn.valCuoExcesoTopeCal = 0
		rTrn.codDestinoExcesoLineaCal = Nothing
		rTrn.seqDestinoExcesoLineaCal = 0
		rTrn.codMvtoExcesoLineaCal = Nothing
		rTrn.valMlExcesoLineaCal = 0
		rTrn.valCuoExcesoLineaCal = 0

		rTrn.valMlExcesoEmplCal = 0
		rTrn.valCuoExcesoEmplCal = 0
		rTrn.seqExcesoEmpl = 0
		rTrn.seqExcesoLinea = 0


		rTrn.valMlTransferenciaCal = 0
		rTrn.valCuoTransferenciaCal = 0

		rTrn.valMlPrimaCal = 0
		rTrn.valMlIntPrimaCal = 0
		rTrn.valMlReaPrimaCal = 0
		rTrn.valCuoPrimaCal = 0
		rTrn.valIndPagoPrimCal = 0
		rTrn.valIdInstPagoPrimCal = 0
		rTrn.valMlCompensCal = 0
		rTrn.valCuoCompensCal = 0
		rTrn.valMlAjusteDecimalCal = 0
		rTrn.valCuoAjusteDecimalCal = 0
		rTrn.numSaldo = 0
		rTrn.valMlSaldo = 0
		rTrn.valCuoSaldo = 0
		rTrn.perCuatrimestre = Nothing
		rTrn.codError = 0
		'rTrn.estadoTransaccion = gtipoProceso

		'SIS//
		rTrn.valMlPrimaSisCal = 0
		rTrn.valMlPrimaSisInteresCal = 0
		rTrn.valMlPrimaSisReajusteCal = 0
		rTrn.valCuoPrimaSisCal = 0
		rTrn.valCuoPrimaSisInteresCal = 0
		rTrn.valCuoPrimaSisReajusteCal = 0
		'rTrn.codMvtoIntreaPrim = Nothing
		rTrn.codMvtoPrimCar = Nothing
		rTrn.tasaPrimaCal = 0


		gRegistros = 0
		gRegistrosIgnorados = 0
		gRegistrosEnviados = 0
		gRegistrosCalculados = 0
		gRegistrosAcreditados = 0
		gRegistrosAjustes = 0
		gRegistrosCompen = 0

		gvalMlIgnorados = 0
		gvalCuoIgnorados = 0

		gvalMlMvto = 0
		gvalMlAdicional = 0
		gvalMlExceso = 0
		gvalMlExcesoEmpl = 0
		gvalMlComisiones = 0
		gvalCuoMvto = 0
		gvalCuoAdicional = 0
		gvalCuoExceso = 0
		gvalCuoExcesoEmpl = 0
		gvalCuoComisiones = 0

		gvalMlMvtoCal = 0
		gvalMlAdicionalCal = 0
		gvalMlExcesoCal = 0
		gvalMlExcesoEmplCal = 0
		gvalMlComisionesCal = 0
		gvalMlPatrFrecCal = 0
		gvalMlPatrFrecActCal = 0
		gvalMlPatrFdesCal = 0
		gvalMlPrimaCal = 0


		gvalMlCompenCarCal = 0
		gvalMlCompenAboCal = 0

		gvalCuoMvtoCal = 0
		gvalCuoAdicionalCal = 0
		gvalCuoExcesoCal = 0
		gvalCuoExcesoEmplCal = 0
		gvalCuoComisionesCal = 0
		gvalCuoPatrFrecCal = 0
		gvalCuoPatrFrecActCal = 0
		gvalCuoPatrFdesCal = 0
		gvalCuoPrimaCal = 0

		gvalCuoAjuDecCal = 0

		gvalCuoCompenCarCal = 0
		gvalCuoCompenAboCal = 0
		gvalMlCompenCarCal = 0
		gvalMlCompenAboCal = 0

		gvalCuoCompenCarAcr = 0
		gvalCuoCompenAboAcr = 0
		gvalMlCompenCarAcr = 0
		gvalMlCompenAboAcr = 0

		gvalMlTransferenciaCal = 0
		gvalCuoTransferenciaCal = 0

		gvalMlAbonosCtaCal = 0
		gvalMlCargosCtaCal = 0
		gvalCuoAbonosCtaCal = 0
		gvalCuoCargosCtaCal = 0

		gvalMlCargosComAcr = 0
		gvalCuoCargosComAcr = 0

		gvalMlAbonosCtaAcr = 0
		gvalMlCargosCtaAcr = 0
		gvalCuoAbonosCtaAcr = 0
		gvalCuoCargosCtaAcr = 0

		gvalMlImpuestoCal = 0
		gvalCuoImpuestoCal = 0

		gcomisionPorcentual = 0
		gcomisionFija = 0

		blIgnorar = False
		blAcreditarARezago = False
		gcodErrorIgnorar = 0

		gEsConvenio = False

		blGenExcesoEnLinea = False
		If gcodAdministradora = 1032 And (gcodOrigenProceso = "REREZCON" _
		 Or gcodOrigenProceso = "RECAUDAC" _
		 Or gcodOrigenProceso = "REREZMAS" _
		 Or gcodOrigenProceso = "REREZSEL" _
		 Or gcodOrigenProceso = "TRAINREZ" _
		 Or gcodOrigenProceso = "TRAIPAGN" _
		 Or gcodOrigenProceso = "TRAIPAGC" _
		 Or gcodOrigenProceso = "TRAINRZC") Then
			blGenExcesoEnLinea = True
		End If
		valMlRIMCotExcesoGen = 0
		valMlRIMSISExcesoGen = 0
		blGenExcesos = False

		If Me.blRentabilidadRez Then
			Me.blRentabilidadRez = False
			If gcodAdministradora = 1033 And (gcodOrigenProceso = "TRAINREZ" _
			 Or gcodOrigenProceso = "TRAIPAGN" _
			 Or gcodOrigenProceso = "TRAIPAGC" _
			 Or gcodOrigenProceso = "TRAINRZC") Then
				Me.blRentabilidadRez = True
			End If
		End If
	End Sub


	Private Sub EstadoAcreditacion(ByVal estadoAcreditacion As String)
		ControlAcr.modEstadoCacrConCommit(gdbc, gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId, estadoAcreditacion, gfecAcreditacion, gidUsuarioProceso, gfuncion)
	End Sub


	Private Sub IniciarLog()

		Dim version As String
		Dim fecha As String
		Dim dsControl As New DataSet()
		Dim enLinea As String = Nothing
		version = "4.0.0.*"
		fecha = "01-08-2011"
		gSeqLog = 1


		'ControlAcr.LogAcreditacion.eliminarTodos(gdbc,gidAdm, gnumeroId)
		Try
			dsControl = ControlAcr.buscarProceso(gdbc, gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId)
			If dsControl.Tables(0).Rows.Count = 0 Then
				Throw New SondaException(15006)				'"Proceso de acreditación no ha sido creado")
			End If

			enLinea = dsControl.Tables(0).Rows(0).Item("TIPO_ACREDITACION_CREADO")
			If enLinea = "ENL" Then
				gEnLinea = True
			Else
				gEnLinea = False
			End If
		Catch : End Try

		If gEnLinea Then
			ControlAcr.LogAcreditacion.crearLogHeb(gdbc, gidAdm, gnumeroId, gIdHebra, gSeqLog, "I", 0, "Iniciando procesamiento. --", 0, Nothing, Nothing, gidUsuarioProceso, gfuncion)
		Else
			ControlAcr.LogAcreditacion.crearLogHeb(gdbc, gidAdm, gnumeroId, gIdHebra, gSeqLog, "I", 0, "" & gIdHebra & " - Inicio Proceso (Versión acreditador " & version & "  " & fecha & ")..", 0, Nothing, Nothing, gidUsuarioProceso, gfuncion)
		End If

		'--LFC: 17/03/2009: usuario que envia a acreditar no corresponde al usuario del lote, (funcionalidad: ACRMADMINSTRA)
		'-- mejora sólo para detectar en PLI        
		If gtipoProceso = "AC" And (gcodOrigenProceso = "AJUSELEC" Or gcodOrigenProceso = "AJUMASIV") And gUsuarioEjecProc <> gidUsuarioProceso Then
			GenerarLog("I", 0, "Hebra " & gIdHebra & " - Usuario Proceso: " & gidUsuarioProceso.ToLower & "  Usuario Acreditacion: " & gUsuarioEjecProc.ToLower, gIdHebra, Nothing, Nothing, Nothing)
		End If
	End Sub

	Private Sub CerrarLog()


		gSeqLog = gSeqLog + 1
		ControlAcr.LogAcreditacion.crearLogHeb(gdbc, gidAdm, gnumeroId, gIdHebra, gSeqLog, "I", 0, gIdHebra & " - Termino Proceso", 0, Nothing, Nothing, gidUsuarioProceso, gfuncion)


	End Sub


	Private Sub GenerarLog(ByVal tipoMensaje As String, ByVal codMensaje As Integer, ByVal mensaje As String, ByVal IdHebra As Integer, ByVal seqTransaccion As Object, ByVal idPersona As Object, ByVal idCliente As Object)

		Dim i As Integer = 0
		Dim largoTrama As Integer = 200
		Dim maxLargo As Integer = 6000
		Dim gidPersona As String

		If IsNothing(seqTransaccion) Then seqTransaccion = 0
		If IsNothing(idCliente) Then idCliente = 0
		If IsNothing(idPersona) Then idPersona = Nothing

		If IsNothing(mensaje) Then
			mensaje = "Detalle del error no especificado"
		End If

		Dim j As Integer = mensaje.Length
		If j > maxLargo Then j = maxLargo

		gidPersona = idPersona

		'If rTrn.codOrigenProceso = "REREZSEL" Or rTrn.codOrigenProceso = "REREZMAS" Or rTrn.codOrigenProceso = "REREZCON" Then
		'    gidPersona = CStr(rTrn.seqMvtoOrigen)
		'Else
		'    gidPersona = idPersona
		'End If

		While i < j
			gSeqLog = gSeqLog + 1
			If j - i < largoTrama Then
				ControlAcr.LogAcreditacion.crearLogHeb(gdbc, gidAdm, gnumeroId, IdHebra, gSeqLog, tipoMensaje, codMensaje, mensaje.Substring(i, j - i), seqTransaccion, gidPersona, idCliente, gidUsuarioProceso, gfuncion)
				j = i
			Else
				ControlAcr.LogAcreditacion.crearLogHeb(gdbc, gidAdm, gnumeroId, IdHebra, gSeqLog, tipoMensaje, codMensaje, mensaje.Substring(i, largoTrama), seqTransaccion, gidPersona, idCliente, gidUsuarioProceso, gfuncion)
				i = i + largoTrama
			End If

		End While

	End Sub


	Private Function TotalesControlAcreditacion() As Integer


		If gTotRegistrosIgnorados > 0 Then

			If Not blPermiteAcreditacionParcial Then			 ' ERROR
				gdbc.Rollback()
				ControlAcr.modTotCacrConCommit(gdbc, gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId, gseqProceso, Nothing, Nothing, 0, gEstadoError, gidUsuarioProceso, gfuncion)
				TotalesControlAcreditacion = 1
			Else

				If gtipoProceso = "SI" Then				'SIMULADO PARCIAL
					ControlAcr.modTotCacrConCommit(gdbc, gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId, gseqProceso, gfecAcreditacion, Nothing, gTotRegistrosSimulados, "SP", gidUsuarioProceso, gfuncion)
				Else				' ACREDITADO PARCIAL
					ControlAcr.modTotCacrConCommit(gdbc, gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId, gseqProceso, gfecAcreditacion, Nothing, gTotRegistrosAcreditados, "AP", gidUsuarioProceso, gfuncion)
				End If
				TotalesControlAcreditacion = 0

			End If

		Else

			If Not blPermiteAcreditacionParcial Then
				blIgnorar = Not RegistroContable()

				If blIgnorar Then
					gTotRegistrosIgnorados = gTotRegistrosAcreditados
					gdbc.Rollback()
					ControlAcr.modTotCacrConCommit(gdbc, gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId, gseqProceso, Nothing, Nothing, 0, gEstadoError, gidUsuarioProceso, gfuncion)
					TotalesControlAcreditacion = 1
				Else
					gdbc.Commit()
					gEstadoError = gtipoProceso
					If gtipoProceso = "SI" Then					  'SIMULADO 

						ControlAcr.modTotCacrConCommit(gdbc, gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId, gseqProceso, gfecAcreditacion, Nothing, gTotRegistrosSimulados, "SI", gidUsuarioProceso, gfuncion)
					Else					  ' ACREDITADO 
						ControlAcr.modTotCacrConCommit(gdbc, gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId, gseqProceso, gfecAcreditacion, Nothing, gTotRegistrosAcreditados, "AC", gidUsuarioProceso, gfuncion)
					End If
					TotalesControlAcreditacion = 0
				End If
			Else
				If gtipoProceso = "SI" Then				'SIMULADO 

					ControlAcr.modTotCacrConCommit(gdbc, gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId, gseqProceso, gfecAcreditacion, Nothing, gTotRegistrosSimulados, "SI", gidUsuarioProceso, gfuncion)
				Else				' ACREDITADO 
					ControlAcr.modTotCacrConCommit(gdbc, gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId, gseqProceso, gfecAcreditacion, Nothing, gTotRegistrosAcreditados, "AC", gidUsuarioProceso, gfuncion)
				End If
				TotalesControlAcreditacion = 0
			End If


		End If

		ResultadoAcred.TotalesAcred.sumarTotales(gdbc, gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId, gseqProceso, gidUsuarioProceso, gfuncion)
	End Function





	Private Sub AplicaCambioFondo()

		If gtipoProceso = "ER" Then
			gtipoProceso = "ER"
		End If

		If blIgnorar Then

			gTotRegistrosIgnorados += 1 : Exit Sub

		End If

		If rTrn.tipoImputacion = "ABO" Then

			gvalMlAbonosCambFond += rTrn.valMlMvto			  'gvalMlAbonosCtaCal

			gvalCuoAbonosCambFond += rTrn.valCuoMvto			 'gvalCuoAbonosCtaCal


			ActualizarFondoRecaudacion()

			If rCam.tipoDistribucion = "CFA" Then

				If II = dsTrnCur.Tables(0).Rows.Count - 1 Then
					modificarDistibucionDestino()

				Else
					If dsTrnCur.Tables(0).Rows(II + 1).Item("ID_CLIENTE") <> rTrn.idCliente Or _
					 dsTrnCur.Tables(0).Rows(II + 1).Item("TIPO_PRODUCTO") <> rTrn.tipoProducto Or _
					 dsTrnCur.Tables(0).Rows(II + 1).Item("TIPO_FONDO_DESTINO") <> rTrn.tipoFondoDestino Then
						modificarDistibucionDestino()

					End If
				End If
			End If
		Else
			'Se excluye comision para CAMBFOND. Manuel Avalos.
			If rMovAcr.tipoMvto = "COM" Then
				Exit Sub
			End If

			gvalMlCargosCambFond += rTrn.valMlMvto			 'gvalMlCargosCtaCal

			gvalCuoCargosCambFond += rTrn.valCuoMvto			 'gvalCuoCargosCtaCal

			If rTrn.indCierreProducto = "C" Then
				CerrarProducto()
			End If

			If rCam.tipoDistribucion = "CFA" Then

				If dsTrnCur.Tables(0).Rows.Count = II Then
					modificarDistibucionOrigen()
				Else
					If dsTrnCur.Tables(0).Rows(II + 1).Item("ID_CLIENTE") <> rTrn.idCliente Or _
					   dsTrnCur.Tables(0).Rows(II + 1).Item("TIPO_PRODUCTO") <> rTrn.tipoProducto Or _
					   dsTrnCur.Tables(0).Rows(II + 1).Item("TIPO_FONDO_DESTINO") <> rTrn.tipoFondoDestino Then
						modificarDistibucionOrigen()
					End If
				End If

			End If

		End If


		gnumTransCambFond += 1

		Try
			CambioFondo.cabecera.modEstadoCambio(gdbc, gidAdm, rCam.fecCambio, rCam.idCliente, rTrn.tipoProducto, rCam.tipoDistribucion, rCam.numSolicitudAut, gtipoProceso, gnumeroId, gidUsuarioProceso, gfuncion)
		Catch
			CambioFondo.cabecera.modEstadoCambio(gdbc, gidAdm, rCam.fecCambio, rCam.idCliente, rCam.tipoDistribucion, rCam.numSolicitudAut, gtipoProceso, gnumeroId, gidUsuarioProceso, gfuncion)
		End Try
	End Sub
	Private Sub ValidaCambioFondo()

		If (gvalMlAbonosCambFond <> gvalMlCargosCambFond) Or blIgnorar Then

			gTotRegistrosIgnorados = gnumTransCambFond
			blIgnorar = True
			blIgnorarCliente = True
			IgnorarCambioFondo()
			If (gvalMlAbonosCambFond <> gvalMlCargosCambFond) Then
				rTrn.codError = 15310
				GenerarLog("E", rTrn.codError, "Hebra " & gIdHebra & " - El cambio de fondo está descuadrado", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

			End If


		ElseIf gtipoProceso = "AC" Then

			'gcodErrorIgnorar = Vector.cambiarDeFondo(gdbc,gidAdm, rCam.numSolicitudAut, gidUsuarioProceso, gfuncion)
			If gcodErrorIgnorar > 0 Then
				'blIgnorar = True
				rTrn.codError = gcodErrorIgnorar
				GenerarLog("A", rTrn.codError, "Hebra " & gIdHebra & " - " & ControlAcr.LogAcreditacion.obtenerSondaException(gdbc, rTrn.codError), gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

			End If
		End If

		gvalMlAbonosCambFond = 0
		gvalMlCargosCambFond = 0

	End Sub

	Private Sub IgnorarCambioFondo()

		gvalMlAbonosCambFond = 0
		gvalMlCargosCambFond = 1

		If gtipoProceso = "AC" Then
			'Prueba de Error PACI
			'GenerarLog("A", 0, "Hebra " & gIdHebra & " - Transaccion Ignorada. RE", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
			'Fin Prueba de Error PACI

			CambioFondo.cabecera.modEstadoCambio(gdbc, gidAdm, rCam.fecCambio, rCam.idCliente, rCam.tipoDistribucion, rCam.numSolicitudAut, "RE", gnumeroId, gidUsuarioProceso, gfuncion)

		End If

	End Sub
	Private Function CuentaSobregirada() As Boolean
		CuentaSobregirada = False
		If Not IsNothing(clsSal) Then
			If clsSal.sobregirado() Then
				GenerarLog("E", 0, "Hebra " & gIdHebra & " - Se ha determinado un sobregiro compuesto por " & clsSal.count & " transacciones - " & rTrn.tipoProducto & " - " & rTrn.tipoFondoDestinoCal & " - " & rTrn.categoria & " - " & clsSal.ValorSobregiro, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
				'GenerarLog("E", 0, "Hebra " & gIdHebra & " - Se ha determinado un sobregiro compuesto por " & clsSal.count & " transacciones ", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
				blIgnorar = True
				CuentaSobregirada = True
				clsSal.clear()
				Exit Function
			End If
			clsSal.clear()
		End If

	End Function
	Private Function RegistroContable(Optional ByRef clsConta2 As WS.IngresoEgresoConta.auxiliarContabilidad.Contabilidad2 = Nothing) As Boolean

		Dim ds As DataSet
		Dim dsCon As DataSet
		Dim NumOpeContab As Integer
		Dim FecOpeContab As Date
		Dim IndExito As Boolean = True
		Dim idCliente As Integer
		Dim idPersona As String
		Dim gNumRefContable As Long
		Dim tipoOperacion As String
		Dim indSimulacion As String

		RegistroContable = True

		idCliente = IIf(blPermiteAcreditacionParcial, rTrn.idCliente, 0)
		idPersona = IIf(blPermiteAcreditacionParcial, rTrn.idPersona, "0")

		If CuentaSobregirada() Then
			GenerarLog("E", 0, "Hebra " & gIdHebra & " - Se ha determinado un sobregiro compuesto por " & clsSal.count & " transacciones", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
			RegistroContable = False
			Exit Function
		End If

		If gtipoProceso <> "AC" Then
			Exit Function
		End If

		RegistroContable = IndExito
		'
		'clsAux.Insert(gdbc, gidAdm, gfecAcreditacion, gcodOrigenProceso, gperContable, gidUsuarioProceso, gfuncion)
		'clsAux.Clear()


		''''INICIO DE CONTABILIZACION NO CORRE. PCI 12/08/2011
		'ds = clsConta.ds

		'If ds.Tables(0).Rows.Count = 0 Then
		'    Exit Function
		'End If

		'tipoOperacion = Nothing

		'Select Case gcodOrigenProceso

		'    Case "RECAUDAC"

		'        If rMovAcr.tipoMvto = "DEC" Then Exit Function

		'        Select Case rTrn.codOrigenRecaudacion

		'            Case "RECA" : tipoOperacion = "REC005"
		'            Case "CDOC" : tipoOperacion = "REC012"
		'            Case "SDOC" : tipoOperacion = "REC022"
		'            Case "SOB" : tipoOperacion = "REC023"

		'        End Select
		'        gNumRefContable = rTrn.idAlternativoDoc


		'    Case "RETCCVAD"
		'        tipoOperacion = "RET001"
		'        gNumRefContable = rTrn.numReferenciaOrigen1 'Solic retiro

		'    Case "RETCCVFO"
		'        tipoOperacion = "RET002"
		'        gNumRefContable = rTrn.numReferenciaOrigen1 'Solic retiro

		'    Case "RETCDCAD"
		'        tipoOperacion = "RET014"
		'        gNumRefContable = rTrn.numReferenciaOrigen1 'Solic retiro

		'    Case "RETCDCFO"
		'        tipoOperacion = "RET006"
		'        gNumRefContable = rTrn.numReferenciaOrigen1 'Solic retiro
		'    Case "RETCAVFO"

		'        tipoOperacion = "RET005"
		'        gNumRefContable = rTrn.numReferenciaOrigen1 'Solic retiro

		'    Case "RETCAIFO"

		'        tipoOperacion = "RET003"
		'        gNumRefContable = rTrn.numReferenciaOrigen1 'Solic retiro

		'    Case "RETCAVAD"
		'        tipoOperacion = "RET008"
		'        gNumRefContable = rTrn.numReferenciaOrigen1 'Solic retiro

		'    Case "LIQBONNO", "LIQBONEX"

		'        tipoOperacion = "BON001"
		'        gNumRefContable = rTrn.numReferenciaOrigen1 'SBR 

		'    Case "REREZMAS", "REREZSEL"

		'        tipoOperacion = "REZ010"
		'        gNumRefContable = rTrn.seqMvtoOrigen 'Numero de rezago

		'    Case "DEVEXCAF"

		'        tipoOperacion = "REZ001"
		'        gNumRefContable = Sys.IngresoEgreso.Contabilidad.ultimaRefOrigen(gdbc, gidAdm, tipoOperacion, gidUsuarioProceso, gfuncion)

		'    Case "PAGOPENS"
		'        tipoOperacion = "BEN011"

		'    Case "PAGOTRBE"
		'        tipoOperacion = "BEN021"

		'    Case "CAMBFOND"

		'        If rCam.tipoDistribucion = "CFA" Then
		'            tipoOperacion = "CAM002"
		'        Else
		'            tipoOperacion = "CAM001"
		'        End If

		'        gNumRefContable = rTrn.numReferenciaOrigen1 'Solic Cambio fondo 

		'End Select

		''MUESTRA EL DATASET EN UN ARCHIVO XML
		''Dim filename As String = "C:\dsCONTA_" & tipoOperacion & ".xml"
		''Dim myFileStreamSchema As New System.IO.FileStream(filename, System.IO.FileMode.Create)
		''Dim MyXmlTextWriter As New System.Xml.XmlTextWriter(myFileStreamSchema, System.Text.Encoding.Unicode)
		''ds.WriteXml(MyXmlTextWriter)
		''MyXmlTextWriter.Close()

		'If IsNothing(tipoOperacion) Then
		'    Exit Function
		'End If

		'dsAux = Sys.Soporte.Parametro.traerGlobal(gdbc, "PAR_ACR_ORIGEN_CONTABILIDAD", New Object() {gidAdm, gcodOrigenProceso, tipoOperacion})

		'If dsAux.Tables(0).Rows.Count = 0 Then
		'    Exit Function
		'End If

		'If dsAux.Tables(0).Rows(0).Item("IND_CONTABILIDAD") <> "S" Then
		'    Exit Function
		'End If

		'indSimulacion = dsAux.Tables(0).Rows(0).Item("IND_SIMULACION")

		'If gcodOrigenProceso <> "PAGOPENS" And gcodOrigenProceso <> "PAGOTRBE" Then


		'    dsCon = Contabilidad.CTBComun.ProcesoContable(gdbc, gidAdm, tipoOperacion, gNumRefContable, indSimulacion, ds, gfecAcreditacion, gidUsuarioProceso, gfuncion)

		'    If dsCon.Tables(0).Rows.Count = 0 Then
		'        GenerarLog("E", 0, "Hebra " & gIdHebra & " - No fué exitosa la contabilización del proceso", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
		'        IndExito = False
		'    Else
		'        IndExito = IndExito And dsCon.Tables(0).Rows(0).Item("IND_EXITO")
		'        If Not dsCon.Tables(0).Rows(0).Item("IND_EXITO") Then
		'            GenerarLog("E", 0, "Hebra " & gIdHebra & " - CONTA: " & dsCon.Tables(0).Rows(0).Item("ERROR"), gIdHebra, rTrn.seqRegistro, idPersona, idCliente)
		'        Else
		'            gNumRefContable = Sys.IngresoEgreso.Contabilidad.ultimaRefOrigen(gdbc, gidAdm, tipoOperacion, gidUsuarioProceso, gfuncion)
		'            Sys.IngresoEgreso.Contabilidad.relacionContable(gdbc, gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId, idCliente, tipoOperacion, gNumRefContable, gfecAcreditacion, gidUsuarioProceso, gfuncion)
		'        End If
		'    End If
		'Else 'Contabilidad de Pago de beneficios

		'    Dim tipoFondo As String
		'    Dim dsBen As DataSet = ds.Clone
		'    Dim dsFon As DataSet
		'    Dim i As Integer
		'    Dim tipoOpePorFondo As String
		'    Dim dr As DataRow()
		'    Dim r As DataRow

		'    dsFon = Sys.Soporte.Parametro.buscarGlobal(gdbc, "PAR_TIPOS_FONDOS", New Object() {gidAdm, Nothing, Nothing})

		'    For i = 0 To dsFon.Tables(0).Rows.Count - 1
		'        dsBen.Tables(0).Rows.Clear()
		'        tipoFondo = dsFon.Tables(0).Rows(i).Item("TIPO_FONDO")
		'        dr = ds.Tables(0).Select("TIPO_FONDO= '" & tipoFondo & "'")
		'        For Each r In dr
		'            dsBen.Tables(0).Rows.Add(r.ItemArray)
		'        Next
		'        Select Case tipoFondo
		'            Case "A" : tipoOpePorFondo = IIf(gcodOrigenProceso = "PAGOPENS", "BEN011", "BEN021")
		'            Case "B" : tipoOpePorFondo = IIf(gcodOrigenProceso = "PAGOPENS", "BEN012", "BEN022")
		'            Case "C" : tipoOpePorFondo = IIf(gcodOrigenProceso = "PAGOPENS", "BEN013", "BEN023")
		'            Case "D" : tipoOpePorFondo = IIf(gcodOrigenProceso = "PAGOPENS", "BEN014", "BEN024")
		'            Case "E" : tipoOpePorFondo = IIf(gcodOrigenProceso = "PAGOPENS", "BEN015", "BEN025")
		'            Case Else : tipoOpePorFondo = Nothing
		'        End Select

		'        If dsBen.Tables(0).Rows.Count > 0 And Not IsNothing(tipoOpePorFondo) Then

		'            dsCon = Contabilidad.CTBComun.ProcesoContable(gdbc, gidAdm, tipoOpePorFondo, gNumRefContable, indSimulacion, dsBen, gfecAcreditacion, gidUsuarioProceso, gfuncion)

		'            If dsCon.Tables(0).Rows.Count = 0 Then
		'                GenerarLog("E", 0, "Hebra " & gIdHebra & " - No fué exitosa la contabilización del proceso", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
		'                IndExito = False
		'            Else
		'                IndExito = IndExito And dsCon.Tables(0).Rows(0).Item("IND_EXITO")
		'                If Not dsCon.Tables(0).Rows(0).Item("IND_EXITO") Then
		'                    GenerarLog("E", 0, "Hebra " & gIdHebra & " - CONTA: " & dsCon.Tables(0).Rows(0).Item("ERROR"), gIdHebra, rTrn.seqRegistro, idPersona, idCliente)
		'                Else
		'                    gNumRefContable = Sys.IngresoEgreso.Contabilidad.ultimaRefOrigen(gdbc, gidAdm, tipoOperacion, gidUsuarioProceso, gfuncion)
		'                    Sys.IngresoEgreso.Contabilidad.relacionContable(gdbc, gidAdm, gcodOrigenProceso, gidUsuarioProceso, gnumeroId, idCliente, tipoOperacion, gNumRefContable, gfecAcreditacion, gidUsuarioProceso, gfuncion)
		'                End If
		'            End If
		'        End If

		'    Next

		'End If

		''--:AP------------------->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
		'If gcodOrigenProceso = "PAGOPENS" And gcodAdministradora = 1032 Then
		'    If clsConta2.ds.Tables(0).Rows.Count > 0 Then
		'        Dim mensaje As String
		'        Dim seqComp As Integer

		'        clsConta2.PostingVouncher(gidAdm, gfecAcreditacion, gidUsuarioProceso, gfuncion, seqComp, mensaje)
		'        If Not IsNothing(mensaje) Then
		'            GenerarLog("E", 0, "Hebra " & gIdHebra & " - Error Contable: " & mensaje, gIdHebra, rTrn.seqRegistro, idPersona, idCliente)
		'            IndExito = True
		'        End If
		'    End If
		'End If
		''-----------------------<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<---------------------------------
		' clsConta.Clear()
		''''FIN   INICIO DE CONTABILIZACION NO CORRE. PCI 12/08/2011

		'RegistroContable = False

	End Function

	Private Function ultimaTrnCliente() As Boolean
		If dsTrnCur.Tables(0).Rows.Count - 1 = II Then
			ultimaTrnCliente = True
			commitParcial = True

		Else
			If IsDBNull(dsTrnCur.Tables(0).Rows(II + 1).Item("ID_CLIENTE")) Then
				ultimaTrnCliente = True
			Else
				ultimaTrnCliente = (dsTrnCur.Tables(0).Rows(II + 1).Item("ID_CLIENTE") <> rTrn.idCliente) Or dsTrnCur.Tables(0).Rows(II + 1).Item("ID_CLIENTE") = 0
			End If

		End If
	End Function


	Private Function verificaTopeAcreditacion() As Boolean

		verificaTopeAcreditacion = ControlAcr.actualizarTopeAcred(gdbc, gidAdm, gcodOrigenProceso, gfecAcreditacion, rTrn.valMlPatrFrecCal - rTrn.valMlTransferencia, gvalCuoAbonosCtaAcr - gvalCuoCargosCtaAcr) = "S"

	End Function

	Private Sub evento(ByVal LOG As Procesos.logEtapa, ByVal mensaje As String)
		If Not IsNothing(LOG) Then
			LOG.AddEvento("ACR: " & mensaje)
			LOG.Save()
		End If
	End Sub
	Private Sub determinaEstadoError()
		gEstadoError = IIf(gtipoProceso = "AC", "AP", "SP")

	End Sub
	Private Sub forzarGrarbageCollector()

		Dim p As New System.Diagnostics.Process()
		Dim vm As Integer
		Dim pm As Integer
		Dim m As Long

		'\\ Forces garbage collection of all generations.
		m = GC.GetTotalMemory(True)
		vm = p.GetCurrentProcess.VirtualMemorySize
		pm = p.GetCurrentProcess.PrivateMemorySize
		GenerarLog("I", 0, "Hebra " & gIdHebra & " - Haciendo Garbage Collection " & "Mem = " & m & " - " & "VMS = " & vm & " - " & "PMS = " & pm, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
		GC.Collect()
		'\\ Suspends the current thread until the thread processing the queue of finalizers has emptied that queue.  
		m = GC.GetTotalMemory(True)
		vm = p.GetCurrentProcess.VirtualMemorySize
		pm = p.GetCurrentProcess.PrivateMemorySize
		GenerarLog("I", 0, "Hebra " & gIdHebra & " - Haciendo Wait para Garbage Collection " & "Mem = " & m & " - " & "VMS = " & vm & " - " & "PMS = " & pm, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
		GC.WaitForPendingFinalizers()

		m = GC.GetTotalMemory(True)
		vm = p.GetCurrentProcess.VirtualMemorySize
		pm = p.GetCurrentProcess.PrivateMemorySize
		GenerarLog("I", 0, "Hebra " & gIdHebra & " - Fin Garbage Collection " & "Mem = " & m & " - " & "VMS = " & vm & " - " & "PMS = " & pm, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)

	End Sub


	Private Sub valorizaTrn()
		Dim fecAcr As Date
		Dim fecCuota As Date
		Dim fecCuatr As Date
		Dim i As Integer
		Dim cuoA, cuoB, cuoC, cuoD, cuoE As Decimal

		fecAcr = Sys.Kernel.Parametros.FechaAcreditacion.obtenerFechaAcreditacion(gdbc, gidAdm, "ACR")
		fecCuota = Sys.Kernel.Parametros.FechaAcreditacion.obtenerFechaValorCuota(gdbc, gidAdm, "ACR")
		fecCuatr = ParametrosINE.PeriodoCuatrimestral.traer(gdbc, gidAdm).Tables(0).Rows(0).Item("PER_CUATRIMESTRE")

		dsAux = ParametrosINE.ValorCuota.obtenerValorCuota(gdbc, gidAdm, fecCuota, Nothing)

		If dsAux.Tables(0).Rows.Count < 5 Then Exit Sub

		For i = 0 To dsAux.Tables(0).Rows.Count - 1
			Select Case dsAux.Tables(0).Rows(i).Item("TIPO_FONDO")
				Case "A" : cuoA = dsAux.Tables(0).Rows(i).Item("VAL_CUOTA")
				Case "B" : cuoB = dsAux.Tables(0).Rows(i).Item("VAL_CUOTA")
				Case "C" : cuoC = dsAux.Tables(0).Rows(i).Item("VAL_CUOTA")
				Case "D" : cuoD = dsAux.Tables(0).Rows(i).Item("VAL_CUOTA")
				Case "E" : cuoE = dsAux.Tables(0).Rows(i).Item("VAL_CUOTA")
			End Select
		Next i
		dsAux = Nothing

	End Sub

	'--solo pensiones//Planvital--AP.
	Private Sub contabilizarCollectionPagoPensiones(ByRef clsConta As WS.IngresoEgresoConta.auxiliarContabilidad.Contabilidad2, ByVal valMlMonto As Decimal)
		If rTrn.codDestinoTransaccionCal <> "REZ" Then
			clsConta.Add(gidAdm, "BEN0005", "ML", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, valMlMonto)
			clsConta.Add(gidAdm, "BEN0006", "ML", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, valMlMonto)
		Else
			clsConta.Add(gidAdm, "BEN0005", "ML", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, valMlMonto)
			clsConta.Add(gidAdm, "BEN0007", "ML", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, valMlMonto)
		End If
	End Sub
	Private Function RegistroContablePensiones(ByRef clsConta As WS.IngresoEgresoConta.auxiliarContabilidad.Contabilidad2) As Boolean
		Dim dsCon As DataSet
		Dim NumOpeContab As Integer
		Dim FecOpeContab As Date
		Dim IndExito As Boolean = True
		Dim idCliente As Integer
		Dim idPersona As String
		Dim gNumRefContable As Integer
		Dim indSimulacion As String
		Dim FecAcredita As Date

		RegistroContablePensiones = True

		idCliente = IIf(blPermiteAcreditacionParcial, rTrn.idCliente, 0)
		idPersona = IIf(blPermiteAcreditacionParcial, rTrn.idPersona, "0")
		FecAcredita = gfecAcreditacion

		If clsSal.sobregirado() Then
			GenerarLog("E", 0, "Hebra " & gIdHebra & " - Se ha determinado un sobregiro compuesto por " & clsSal.count & " transacciones - " & rTrn.tipoProducto & " - " & rTrn.tipoFondoDestinoCal & " - " & rTrn.categoria & " - " & clsSal.ValorSobregiro, gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
			'GenerarLog("E", 0, "Hebra " & gIdHebra & " - Se ha determinado un sobregiro compuesto por " & clsSal.count & " transacciones", gIdHebra, rTrn.seqRegistro, rTrn.idPersona, rTrn.idCliente)
			RegistroContablePensiones = False
			clsSal.clear()
			Exit Function
		End If
		clsSal.clear()

		If gtipoProceso <> "AC" Then
			Exit Function
		End If

		clsAux.Insert(gdbc, gidAdm, gfecAcreditacion, gcodOrigenProceso, gperContable, gidUsuarioProceso, gfuncion)
		clsAux.Clear()

		'--:AP------------------->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
		If gcodOrigenProceso = "PAGOPENS" Then
			If clsConta.ds.Tables(0).Rows.Count > 0 Then
				Dim mensaje As String
				Dim seqComp As Integer

				clsConta.PostingVouncher(gidAdm, FecAcredita, gidUsuarioProceso, gfuncion, seqComp, mensaje)
				If Not IsNothing(mensaje) Then
					GenerarLog("E", 0, "Hebra " & gIdHebra & " - Error Contable: " & mensaje, gIdHebra, rTrn.seqRegistro, idPersona, idCliente)
					IndExito = True
				End If
			End If
		End If
		'--<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

		RegistroContablePensiones = IndExito

	End Function

	Private Function PlanillaDnp() As Boolean
		Dim ds As New DataSet()
		Try
			PlanillaDnp = False
			ds = parTipoPlanilla.traer(gdbc, New Object() {"VID_ADM", "VTIPO_PLANILLA"}, New Object() {gidAdm, rTrn.tipoPlanilla}, New Object() {"INTEGER", "INTEGER"})
			If ds.Tables(0).Rows.Count > 0 Then
				If ds.Tables(0).Rows(0).Item("ORIGEN") = 2 Then
					Return True
				End If
			End If
		Catch ex As Exception
			Return False
		End Try
	End Function


	Private Sub descripcionCausalRez()

		Select Case rTrn.codCausalRezagoCal
			Case "21a"
				rTrn.codCausalRezagoCal = "21"
				gDescripcionCausalRezago = "Mas de dos productos vigentes"

			Case "21b"
				rTrn.codCausalRezagoCal = "21"
				gDescripcionCausalRezago = "Sin cuentas vigentes"

			Case "21c"
				rTrn.codCausalRezagoCal = "21"
				gDescripcionCausalRezago = "Suma de porcentajes erroneos en DISTRIBUCION"

			Case "21d"
				rTrn.codCausalRezagoCal = "21"
				gDescripcionCausalRezago = "Fondo nulo para porcentaje mayor que cero"

			Case "21e"
				rTrn.codCausalRezagoCal = "21"
				gDescripcionCausalRezago = "Error al crear producto"

			Case "21f"
				rTrn.codCausalRezagoCal = "21"
				gDescripcionCausalRezago = "Sin Prod,mov.no es Cotizacion"

			Case Else

		End Select

	End Sub

	Private Sub ajustesDecimalAComisionPorc()
		g_valMlAjusteDec = 0
		g_valCuoAjusteDec = 0

		'Se Instala en TEST de MODELO y PLANVITAL. 
		'If rTrn.tipoFondoDestinoCal = "C" Then
		If (gcodAdministradora <> 1034 And rTrn.tipoFondoDestinoCal = "C") Then
			If rTrn.codOrigenProceso = "REREZSEL" Or rTrn.codOrigenProceso = "REREZMAS" Or rTrn.codOrigenProceso = "REREZCON" Then
				If rTrn.valCuoAjusteDecimalCal = 0 Then Exit Sub

				If rTrn.valCuoAjusteDecimalCal > 0 Then
					If rTrn.valCuoAdicionalCal - rTrn.valCuoAjusteDecimalCal < 0 Then Exit Sub

					'If rTrn.valMlAjusteDecimalCal = 0 Then rTrn.valMlAjusteDecimalCal = Mat.Redondear(rTrn.valCuoAjusteDecimalCal * rTrn.valMlValorCuota, 0)

					g_valCuoAjusteDec = rTrn.valCuoAjusteDecimalCal
					'g_valMlAjusteDec = rTrn.valMlAjusteDecimalCal
					'If rTrn.valMlAjusteDecimalCal = 0 Then
					'    g_valMlAjusteDec = Mat.Redondear(g_valCuoAjusteDec * rTrn.valMlValorCuota, 0)
					'Else
					'    g_valMlAjusteDec = rTrn.valMlAjusteDecimalCal
					'End If

				Else
					If rTrn.valCuoAdicionalCal + rTrn.valCuoAjusteDecimalCal < 0 Then Exit Sub


					'If rTrn.valMlAjusteDecimalCal = 0 Then rTrn.valMlAjusteDecimalCal = Mat.Redondear(rTrn.valCuoAjusteDecimalCal * rTrn.valMlValorCuota, 0)

					g_valCuoAjusteDec = rTrn.valCuoAjusteDecimalCal
					'g_valMlAjusteDec = rTrn.valMlAjusteDecimalCal
					'If rTrn.valMlAjusteDecimalCal = 0 Then
					'    g_valMlAjusteDec = Mat.Redondear(g_valCuoAjusteDec * rTrn.valMlValorCuota, 0)
					'Else
					'    g_valMlAjusteDec = rTrn.valMlAjusteDecimalCal
					'End If

				End If
				rTrn.valCuoAjusteDecimalCal = 0
				rTrn.valMlAjusteDecimalCal = 0
			End If
			''''''''ElseIf (gcodAdministradora = 1034) Then
			''''''''    If rTrn.codOrigenProceso = "REREZSEL" Or rTrn.codOrigenProceso = "REREZMAS" Or _
			''''''''       rTrn.codOrigenProceso = "REREZCON" Or rTrn.codOrigenProceso = "RECAUDAC" Then
			''''''''        If rTrn.valCuoAjusteDecimalCal = 0 Then Exit Sub

			''''''''        If rTrn.valCuoAjusteDecimalCal > 0 Then
			''''''''            If rTrn.valCuoAdicionalCal - rTrn.valCuoAjusteDecimalCal < 0 Then Exit Sub

			''''''''            'rTrn.valCuoAdicionalCal = rTrn.valCuoAdicionalCal - rTrn.valCuoAjusteDecimalCal

			''''''''            If rTrn.valMlAjusteDecimalCal = 0 Then rTrn.valMlAjusteDecimalCal = Mat.Redondear(rTrn.valCuoAjusteDecimalCal * rTrn.valMlValorCuota, 0)

			''''''''            'rTrn.valMlAdicionalCal = rTrn.valMlAdicionalCal - rTrn.valMlAjusteDecimalCal

			''''''''            g_valCuoAjusteDec = rTrn.valCuoAjusteDecimalCal
			''''''''            g_valMlAjusteDec = rTrn.valMlAjusteDecimalCal
			''''''''            'g_valMlAjusteDec = 0


			''''''''        Else
			''''''''            If rTrn.valCuoAdicionalCal + rTrn.valCuoAjusteDecimalCal < 0 Then Exit Sub

			''''''''            'rTrn.valCuoAdicionalCal = rTrn.valCuoAdicionalCal + rTrn.valCuoAjusteDecimalCal
			''''''''            If rTrn.valMlAjusteDecimalCal = 0 Then rTrn.valMlAjusteDecimalCal = Mat.Redondear(rTrn.valCuoAjusteDecimalCal * rTrn.valMlValorCuota, 0)
			''''''''            'rTrn.valMlAdicionalCal = rTrn.valMlAdicionalCal + rTrn.valMlAjusteDecimalCal

			''''''''            g_valCuoAjusteDec = rTrn.valCuoAjusteDecimalCal
			''''''''            g_valMlAjusteDec = rTrn.valMlAjusteDecimalCal
			''''''''            'g_valMlAjusteDec = 0

			''''''''        End If
			''''''''        rTrn.valCuoAjusteDecimalCal = 0
			''''''''        rTrn.valMlAjusteDecimalCal = 0
			''''''''    End If
		End If


	End Sub

	Private Sub VerificaAjustesDecimal(ByVal valdif As Decimal, ByRef valCuoCot As Decimal, ByRef valCuoAdi As Decimal, ByRef valCuoAdiInt As Decimal, ByRef valCuoAdiRea As Decimal, ByRef valCuoPrim As Decimal, ByRef valRentab As Decimal)
		Dim ValUltDig1 As Integer
		Dim ValUltDig2 As Integer

		'para planvital no entrar
		If Me.gcodAdministradora = 1032 Then Exit Sub

		'lfc:17/08/2016 OS:8984519 - con ajuste decimal en las recuperaciones de rezagos
		If (rTrn.codOrigenProceso = "REREZSEL" Or rTrn.codOrigenProceso = "REREZMAS") And gcodAdministradora = 1032 Then
			If rTrn.valCuoAjusteDecimalCal = 0 And valdif <> 0 Then
				rTrn.valCuoAjusteDecimalCal = valdif
				rTrn.valMlAjusteDecimalCal = 0
				gValDif = 0
				Exit Sub
			End If
		End If


		If valdif > 0 Then
			'Si Ajuste Decimal es Positivo se va a la Cotizacion
			valCuoCot += valdif
			valdif = 0
		ElseIf valdif < 0 Then

			'Si Ajuste Decimal es Negativo se va a la Comision, si no existe a la Prima y si no existe 
			'a la cotizacion.
			If rTrn.valCuoComisPorcentualCal > 0 And (rTrn.valCuoComisPorcentualCal + valdif) >= 0 Then

				rTrn.valCuoComisPorcentualCal += valdif

				If valCuoAdi > 0 And (valCuoAdi + valdif) >= 0 Then
					'valCuoAdi += -0.005
					valCuoAdi += valdif
				Else
					If valCuoAdiInt > 0 And (valCuoAdiInt + valdif) >= 0 Then
						valCuoAdiInt += valdif
					Else
						If valCuoAdiRea > 0 And (valCuoAdiRea + valdif) >= 0 Then
							valCuoAdiInt += valdif
						End If
					End If
				End If
				valdif = 0
			ElseIf valCuoPrim > 0 And (valCuoPrim + valdif) >= 0 Then
				'valCuoPrim += -0.005
				valCuoPrim += valdif
				valdif = 0
			ElseIf valCuoCot > 0 And (valCuoCot + valdif) >= 0 Then
				'valCuoCot += -0.005
				valCuoCot += valdif
				valdif = 0
			End If
		End If
		gValDif = valdif

	End Sub


	Private Sub AcreditaExcesosIndepend()
		Dim valDif As Decimal

		rTrn.codDestinoTransaccionCal = "CTA"
		CuotaDestino(rTrn.tipoFondoDestinoCal, rTrn.valMlValorCuota, rTrn.fecValorCuota)

		'Calculadas
		gRegistrosCalculados = gRegistrosCalculados + 1
		If rTrn.tipoFondoDestinoCal = "C" Then
			rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
			rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
			rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal
			rTrn.valCuoPatrFdesCal = rTrn.valCuoPatrFrecActCal
		Else
			gvalMlPatrDistFondoC += rTrn.valMlPatrFrecCal - rTrn.valMlTransferenciaCal
			If (rTrn.tipoProducto = "CCV" Or rTrn.tipoProducto = "CDC") And rTrn.valCuoPatrFrecCal = 0 Then
				'Oficio 3021. OS-2789587
				rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
				rTrn.valMlPatrFrecActCal = rTrn.valMlPatrFrecCal
				rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal				'- rTrn.valMlTransferenciaCal
				rTrn.valCuoPatrFdesCal = Mat.Redondear(rTrn.valMlPatrFdesCal / rTrn.valMlValorCuota, 2)
			Else
				rTrn.valCuoPatrFrecActCal = rTrn.valCuoPatrFrecCal
				rTrn.valMlPatrFrecActCal = Mat.Redondear(rTrn.valCuoPatrFrecCal * gvalMlCuotaDestinoC, 0)
				rTrn.valMlPatrFdesCal = rTrn.valMlPatrFrecActCal				'- rTrn.valMlTransferenciaCal
				rTrn.valCuoPatrFdesCal = Mat.Redondear(rTrn.valMlPatrFdesCal / rTrn.valMlValorCuota, 2)
			End If
		End If

		rTrn.valMlExcesoTopeCal = rTrn.valMlExcesoLinea
		rTrn.valCuoExcesoTopeCal = Mat.Redondear(rTrn.valMlExcesoLinea / rTrn.valMlValorCuota, 2)

		rTrn.valMlCompensCal = rTrn.valMlPatrFrecActCal - rTrn.valMlPatrFrecCal
		rTrn.valCuoCompensCal = Mat.Redondear(rTrn.valMlCompensCal / rTrn.valMlValorCuota, 2)


		'Verifica Calculo de Ajuste Decimal.
		valDif = rTrn.valCuoPatrFdesCal - (rTrn.valCuoExcesoTopeCal + rTrn.valCuoCompensCal)

		rTrn.valCuoAjusteDecimalCal = valDif
		rTrn.valMlAjusteDecimalCal = 0


		'rTrn.valMlExcesoTopeCal = rTrn.valMlMvto + rTrn.valMlInteres + rTrn.valMlReajuste + rTrn.valMlAdicional + _
		'       rTrn.valMlAdicionalInteres + rTrn.valMlAdicionalReajuste + rTrn.valMlPrimaSis + _
		'       rTrn.valMlPrimaSisInteres + rTrn.valMlPrimaSisReajuste

		'rTrn.valCuoExcesoTopeCal = rTrn.valCuoMvto + rTrn.valCuoInteres + rTrn.valCuoReajuste + rTrn.valCuoAdicional + _
		'       rTrn.valCuoAdicionalInteres + rTrn.valCuoAdicionalReajuste + rTrn.valCuoPrimaSis + _
		'       rTrn.valCuoPrimaSisInteres + rTrn.valCuoPrimaSisReajuste

		'Private Sub realizarAbonosExcesos()

		gvalMlExcesoCal = rTrn.valMlExcesoTopeCal
		gvalCuoExcesoCal = rTrn.valCuoExcesoTopeCal

		sMov = New INEMovimiento(rSal.valMlSaldo, rSal.valCuoSaldo)

		SumarTotales()

		realizarAbonosExcesos()
		realizarAbonosCargosCompensasion()

		CalcularSaldo()

		If gcodOrigenProceso = "REREZSEL" Or gcodOrigenProceso = "REREZMAS" Or gcodOrigenProceso = "REREZCON" Then
			RezagoAHistorico()
		End If

	End Sub

	'Private Sub VerificaAjustesDecimal(ByVal valdif As Decimal, ByRef valCuoCot As Decimal, ByRef valCuoAdi As Decimal, ByRef valCuoPrim As Decimal, ByRef valRentab As Decimal)
	'    Dim ValUltDig1 As Integer
	'    Dim ValUltDig2 As Integer
	'    If valdif > 0 Then
	'        'CStr(valdif).IndexOf(",", 0, 3)
	'        'Tiene Ajuste decimal
	'        If valCuoAdi > 0 And valCuoPrim > 0 Then
	'            ValUltDig1 = Mid(CStr(Int(valCuoAdi * 1000)), CStr(Int(valCuoAdi * 1000)).Length)
	'            ValUltDig2 = Mid(CStr(Int(valCuoPrim * 1000)), CStr(Int(valCuoPrim * 1000)).Length)
	'            If ValUltDig1 < 5 And ValUltDig2 < 5 Then
	'                If (5 - ValUltDig1) < (5 - ValUltDig2) Then
	'                    'Prima
	'                    valCuoPrim = (Int(valCuoPrim * 1000) + (5 - ValUltDig2)) / 1000
	'                Else
	'                    'Adicional
	'                    valCuoAdi = (Int(valCuoAdi * 1000) + (5 - ValUltDig1)) / 1000
	'                    ValAjusteCom = 0.005
	'                End If
	'            ElseIf ValUltDig1 < 5 And ValUltDig2 >= 5 Then
	'                'Se agrega a Adicional
	'                valCuoAdi = (Int(valCuoAdi * 1000) + (5 - ValUltDig1)) / 1000
	'                ValAjusteCom = 0.005
	'            ElseIf ValUltDig1 >= 5 And ValUltDig2 < 5 Then
	'                'Se agrega a Prima
	'                valCuoPrim = (Int(valCuoPrim * 1000) + (5 - ValUltDig2)) / 1000
	'            ElseIf ValUltDig1 >= 5 And ValUltDig2 >= 5 Then
	'                'Se agrega a Adicional
	'                valCuoAdi = (Int(valCuoAdi * 100) + 2) / 100
	'                ValAjusteCom = 0.01
	'            End If
	'        ElseIf valCuoCot > 0 And valCuoAdi > 0 And valCuoPrim = 0 Then
	'            ValUltDig1 = Mid(CStr(Int(valCuoCot * 1000)), CStr(Int(valCuoCot * 1000)).Length)
	'            ValUltDig2 = Mid(CStr(Int(valCuoAdi * 1000)), CStr(Int(valCuoAdi * 1000)).Length)
	'            If ValUltDig1 < 5 And ValUltDig2 < 5 Then
	'                If (5 - ValUltDig1) < (5 - ValUltDig2) Then
	'                    'Adicional
	'                    valCuoAdi = (Int(valCuoAdi * 1000) + (5 - ValUltDig2)) / 1000
	'                    ValAjusteCom = 0.005
	'                Else
	'                    'Cotiza
	'                    valCuoCot = (Int(valCuoCot * 1000) + (5 - ValUltDig1)) / 1000
	'                End If
	'            ElseIf ValUltDig1 < 5 And ValUltDig2 >= 5 Then
	'                'Se agrega a Cotizacion
	'                valCuoCot = (Int(valCuoCot * 1000) + (5 - ValUltDig1)) / 1000
	'            ElseIf ValUltDig1 >= 5 And ValUltDig2 < 5 Then
	'                'Se agrega a Adicional
	'                valCuoAdi = (Int(valCuoAdi * 1000) + (5 - ValUltDig2)) / 1000
	'                ValAjusteCom = 0.005
	'            End If
	'        ElseIf valCuoCot > 0 And valCuoPrim > 0 And valCuoAdi = 0 Then
	'            ValUltDig1 = Mid(CStr(Int(valCuoCot * 1000)), CStr(Int(valCuoCot * 1000)).Length)
	'            ValUltDig2 = Mid(CStr(Int(valCuoPrim * 1000)), CStr(Int(valCuoPrim * 1000)).Length)
	'            If ValUltDig1 < 5 And ValUltDig2 < 5 Then
	'                If (5 - ValUltDig1) < (5 - ValUltDig2) Then
	'                    'Prima
	'                    valCuoPrim = (Int(valCuoPrim * 1000) + (5 - ValUltDig2)) / 1000
	'                Else
	'                    'Adicional
	'                    valCuoCot = (Int(valCuoCot * 1000) + (5 - ValUltDig1)) / 1000
	'                End If
	'            ElseIf ValUltDig1 < 5 And ValUltDig2 >= 5 Then
	'                'Se agrega a Adicional
	'                valCuoCot = (Int(valCuoCot * 1000) + (5 - ValUltDig1)) / 1000
	'            ElseIf ValUltDig1 >= 5 And ValUltDig2 < 5 Then
	'                'Se agrega a Prima
	'                valCuoPrim = (Int(valCuoPrim * 1000) + (5 - ValUltDig2)) / 1000
	'            End If
	'        ElseIf valCuoCot > 0 And valCuoPrim = 0 And valCuoAdi = 0 Then
	'            ValUltDig1 = Mid(CStr(Int(valCuoCot * 1000)), CStr(Int(valCuoCot * 1000)).Length)
	'            valCuoCot = (Int(valCuoCot * 1000) + (5 - ValUltDig1)) / 1000
	'        End If
	'    ElseIf valdif < 0 Then
	'        'Ajuste Negativo. Lo asume la rentabilidad.
	'        If valRentab > 0 Then
	'            If rTrn.tipoFondoDestinoCal = "A" Then
	'                rTrn.tipoFondoDestinoCal = "A"
	'            End If
	'            valRentab += valdif
	'        Else
	'            If valCuoAdi > 0 And valCuoPrim > 0 Then
	'                ValUltDig1 = Mid(CStr(Int(valCuoAdi * 1000)), CStr(Int(valCuoAdi * 1000)).Length) + 1
	'                ValUltDig2 = Mid(CStr(Int(valCuoPrim * 1000)), CStr(Int(valCuoPrim * 1000)).Length) + 1
	'                If ValUltDig1 > 5 And ValUltDig2 > 5 Then
	'                    If (5 - ValUltDig1) > (5 - ValUltDig2) Then
	'                        'Prima
	'                        valCuoPrim = (Int(valCuoPrim * 1000) + (5 - ValUltDig2)) / 1000
	'                    Else
	'                        'Adicional
	'                        valCuoAdi = (Int(valCuoAdi * 1000) + (5 - ValUltDig1)) / 1000
	'                        ValAjusteCom = -0.005
	'                    End If
	'                ElseIf ValUltDig1 > 5 And ValUltDig2 <= 5 Then
	'                    'Se agrega a Adicional
	'                    valCuoAdi = (Int(valCuoAdi * 1000) + (5 - ValUltDig1)) / 1000
	'                    ValAjusteCom = -0.005
	'                ElseIf ValUltDig1 <= 5 And ValUltDig2 > 5 Then
	'                    'Se agrega a Prima
	'                    valCuoPrim = (Int(valCuoPrim * 1000) + (5 - ValUltDig2)) / 1000
	'                ElseIf ValUltDig1 <= 5 And ValUltDig2 <= 5 Then 'Nuevo
	'                    If (5 + ValUltDig1) > (5 + ValUltDig2) Then
	'                        'Prima
	'                        valCuoPrim = (Int(valCuoPrim * 1000) - (5 + ValUltDig2)) / 1000
	'                    Else
	'                        'Adicional
	'                        valCuoAdi = (Int(valCuoAdi * 1000) - (5 + ValUltDig1)) / 1000
	'                        ValAjusteCom = -0.005
	'                    End If 'Fin NUevo
	'                End If
	'            ElseIf valCuoCot > 0 And valCuoAdi > 0 And valCuoPrim = 0 Then
	'                ValUltDig1 = Mid(CStr(Int(valCuoCot * 1000)), CStr(Int(valCuoCot * 1000)).Length) + 1
	'                ValUltDig2 = Mid(CStr(Int(valCuoAdi * 1000)), CStr(Int(valCuoAdi * 1000)).Length) + 1
	'                If ValUltDig1 > 5 And ValUltDig2 > 5 Then
	'                    If (5 - ValUltDig1) > (5 - ValUltDig2) Then
	'                        'Adicional
	'                        valCuoAdi = (Int(valCuoAdi * 1000) + (5 - ValUltDig2)) / 1000
	'                        ValAjusteCom = -0.005
	'                    Else
	'                        'Cotiza
	'                        valCuoCot = (Int(valCuoCot * 1000) + (5 - ValUltDig1)) / 1000
	'                    End If
	'                ElseIf ValUltDig1 > 5 And ValUltDig2 <= 5 Then
	'                    'Se agrega a Cotizacion
	'                    valCuoCot = (Int(valCuoCot * 1000) + (5 - ValUltDig1)) / 1000
	'                ElseIf ValUltDig1 <= 5 And ValUltDig2 > 5 Then
	'                    'Se agrega a Adicional
	'                    valCuoAdi = (Int(valCuoAdi * 1000) + (5 - ValUltDig2)) / 1000
	'                    ValAjusteCom = -0.005
	'                End If
	'            ElseIf valCuoCot > 0 And valCuoPrim > 0 And valCuoAdi = 0 Then
	'                ValUltDig1 = Mid(CStr(Int(valCuoCot * 1000)), CStr(Int(valCuoCot * 1000)).Length) + 1
	'                ValUltDig2 = Mid(CStr(Int(valCuoPrim * 1000)), CStr(Int(valCuoPrim * 1000)).Length) + 1
	'                If ValUltDig1 > 5 And ValUltDig2 > 5 Then
	'                    If (5 - ValUltDig1) > (5 - ValUltDig2) Then
	'                        'Prima
	'                        valCuoPrim = (Int(valCuoPrim * 1000) + (5 - ValUltDig2)) / 1000
	'                    Else
	'                        'Adicional
	'                        valCuoCot = (Int(valCuoCot * 1000) + (5 - ValUltDig1)) / 1000
	'                    End If
	'                ElseIf ValUltDig1 > 5 And ValUltDig2 <= 5 Then
	'                    'Se agrega a Adicional
	'                    valCuoCot = (Int(valCuoCot * 1000) + (5 - ValUltDig1)) / 1000
	'                ElseIf ValUltDig1 <= 5 And ValUltDig2 > 5 Then
	'                    'Se agrega a Prima
	'                    valCuoPrim = (Int(valCuoPrim * 1000) + (5 - ValUltDig2)) / 1000
	'                End If
	'            ElseIf valCuoCot > 0 And valCuoPrim = 0 And valCuoAdi = 0 Then
	'                ValUltDig1 = Mid(CStr(Int(valCuoCot * 1000)), CStr(Int(valCuoCot * 1000)).Length)
	'                valCuoCot = (Int(valCuoCot * 1000) + (5 - (ValUltDig1 + 1))) / 1000
	'            End If
	'        End If
	'    End If
	'End Sub


	Private Function crearExcesoEmpleador()
		rRez.folioConvenio = 0

		If rTrn.codOrigenProceso = "RECAUDAC" Then
			rRez.numPlanilla = rTrn.numReferenciaOrigen1
			rRez.folioPlanilla = rTrn.numReferenciaOrigen6
			rRez.numPagina = rTrn.numReferenciaOrigen3
			rRez.seqLinea = rTrn.numReferenciaOrigen4
			rRez.codAdmOrigen = gcodAdministradora
		Else
			rRez.numPlanilla = 0
			rRez.folioPlanilla = 0
			rRez.numPagina = 0
			rRez.seqLinea = 0
			rRez.codAdmOrigen = rTrn.idInstOrigen
		End If

		Select Case rTrn.tipoProducto
			Case "CCO" : rRez.codMvto = "110851"
			Case "CAV" : rRez.codMvto = "210851"
			Case "CAI" : rRez.codMvto = "310851"
			Case "CCV" : rRez.codMvto = "410851"
			Case "CDC" : rRez.codMvto = "510851"
			Case "CAF" : rRez.codMvto = "610851"
			Case "CVC" : rRez.codMvto = "710851"
		End Select
		rRez.codMvtoPrim = Nothing
		rRez.codMvtoIntreaPrim = Nothing

		rRez.numRezago = 0
		rRez.perCotiza = rTrn.perCotizacion
		rRez.tipoProducto = rTrn.tipoProducto
		rRez.tipoFondo = "C"

		rRez.tipoPago = rTrn.tipoPago
		rRez.tipoPlanilla = rTrn.tipoPlanilla
		rRez.tipoRemuneracion = rTrn.tipoRemuneracion
		rRez.tipoEntidadPagadora = rTrn.tipoEntidadPagadora
		rRez.fecOperacion = rTrn.fecOperacion
		rRez.fecInicioGratificacion = Nothing
		rRez.fecFinGratificacion = Nothing
		rRez.idCliente = rTrn.idCliente
		rRez.idEmpleadorOri = rTrn.idEmpleador
		rRez.idPersonaOri = rTrn.idPersona
		rRez.apPaternoOri = rTrn.apPaterno
		rRez.apMaternoOri = rTrn.apMaterno
		rRez.nombreOri = rTrn.nombre
		rRez.nombreAdicionalOri = rTrn.nombreAdicional
		rRez.codigoSoundex = rTrn.codSoundex
		rRez.idEmpleador = rTrn.idEmpleador
		rRez.idPersona = rTrn.idPersona
		rRez.apPaterno = rTrn.apPaterno
		rRez.apMaterno = rTrn.apMaterno
		rRez.nombre = rTrn.nombre
		rRez.nombreAdicional = rTrn.nombreAdicional

		rRez.valMlRentaImponible = valMlRIMSISExcesoGen

		rRez.valMlMontoNominal = rTrn.valMlExcesoEmplCal
		rRez.valMlMonto = rTrn.valMlExcesoEmplCal
		rRez.valMlInteres = 0
		rRez.valMlReajuste = 0
		rRez.valCuoMonto = rTrn.valCuoExcesoEmplCal
		rRez.valCuoInteres = 0
		rRez.valCuoReajuste = 0
		rRez.valMlAdicional = 0
		rRez.valMlAdicionalInteres = 0
		rRez.valMlAdicionalReajuste = 0
		rRez.valCuoAdicional = 0
		rRez.valCuoAdicionalInteres = 0
		rRez.valCuoAdicionalReajuste = 0

		rRez.fecValorCuota = rTrn.fecValorCuota
		rRez.valMlValorCuota = rTrn.valMlValorCuota

		rRez.codOrigenMvto = rTrn.codOrigenMvto
		rRez.codOrigenProceso = rTrn.codOrigenProceso
		rRez.fecContableRezago = rTrn.fecAcreditacion
		rRez.fecOperacionAdmOrigen = rTrn.fecOperacionAdmOrigen
		rRez.perContableRezago = rTrn.perContable
		rRez.codCausalRezago = "31"
		rRez.codCausalOriginal = "31"

		rRez.numDictamenOri = rTrn.numDictamen
		rRez.fecNotificacion = Nothing
		rRez.codTrabajoPesado = Nothing
		rRez.puestoTrabajoPesado = Nothing
		rRez.tasaTrabajoPesadoOri = 0
		rRez.tasaTrabajoPesado = 0
		rRez.estadoRezago = "V"
		rRez.fecEstadoRezago = gfecAcreditacion
		rRez.estadoReg = Nothing
		rRez.fecEstadoReg = Nothing
		rRez.idUsuarioIngReg = gidUsuarioProceso
		rRez.fecIngReg = Nothing
		rRez.idUsuarioUltModifReg = gidUsuarioProceso
		rRez.fecUltModifReg = Nothing
		rRez.idFuncionUltModifReg = gfuncion

		If rTrn.tipoProducto = "CAV" Then
			If rTrn.categoria = "GENERAL" Then
				rRez.indRegimenTributario = "A"
			ElseIf rTrn.categoria = "OPCIONAL" Then
				rRez.indRegimenTributario = "B"
			ElseIf Not IsNothing(rTrn.categoria) Then
				rRez.indRegimenTributario = "B"
			End If
		Else
			rRez.indRegimenTributario = rTrn.codRegTributario
		End If

		If rTrn.tipoProducto = "CAV" Then
			Dim ds As DataSet
			ds = ParametrosINE.ParametrosGenerales.BuscaRegTribut(gdbc, gidAdm, rTrn.tipoProducto, rTrn.categoria)
			If ds.Tables(0).Rows.Count > 0 Then
				rRez.indRegimenTributario = IIf(IsDBNull(ds.Tables(0).Rows(0).Item("COD_REG_TRIBUTARIO")), "RTCAV2", ds.Tables(0).Rows(0).Item("COD_REG_TRIBUTARIO"))
			Else
				rRez.indRegimenTributario = "RTCAV2"
			End If
		Else
			rRez.indRegimenTributario = rTrn.codRegTributario
		End If
		'CAV. NCG 133 19/06/2015.

		rRez.numContrato = rTrn.subCategoria
		rRez.categoria = rTrn.categoria

		If gEsConvenio Then
			' Define Atributos cuando es CONVENIO
			rRez.folioConvenio = rTrn.folioConvenio
			rRez.numCuotasPactadas = rTrn.numCuotasPactadas
			rRez.numCuotasPagadas = rTrn.numCuotasPagadas
		End If


		'SIS//
		'rRez.codMvtoPrim = rTrn.codMvtoPrim -- ESTE VALOR SE CALCULA
		'rRez.codMvtoIntreaPrim = rTrn.codMvtoIntreaPrim --ESTE VALOR SE CALCULA
		rRez.sexo = rTrn.sexo
		rRez.valMlRentaImponibleSis = valMlRIMSISExcesoGen

		rRez.valMlPrimaseguro = 0
		rRez.valMlPrimaseguroInt = 0
		rRez.valMlPrimaseguroRea = 0
		rRez.tasaPrima = 0
		rRez.valCuoPrimaseguro = 0
		rRez.valCuoPrimaseguroInt = 0
		rRez.valCuoPrimaseguroRea = 0
		'rRez.valMlExcesoAfi = rTrn.valMlExcesoLinea
		rRez.valMlExcesoAfi = 0
		rRez.valMlExcesoEmp = 0

		rRez.tipoOrigenDigitacion = rTrn.tipoOrigenDigitacion

		'--SOLICITADO mUT
		If Not (rTrn.tipoRezago Is Nothing) And rTrn.codOrigenProceso = "TRAIPAGN" Then
			rRez.tipoRezago = rTrn.tipoRezago
			Exit Function
		End If
		'--.-- CA-2008120252 --13-03-09
		dsAux = Sys.IngresoEgreso.Rezagos.buscarAfiliadoTipoRez(gdbc, gidAdm, rTrn.idPersona)

		Dim fecOperacionCal As Date = rRez.fecOperacion		  'modificacion MUT 23/02/2010
		If rTrn.codOrigenProceso = "TRAINREZ" Or _
		   rTrn.codOrigenProceso = "TRAINRZC" Or _
		   rTrn.codOrigenProceso = "TRAIPAGN" Or _
		   rTrn.codOrigenProceso = "TRAIPAGC" Then
			fecOperacionCal = rTrn.fecOperacionAdmOrigen
		End If

		Dim OBJ As Object

		OBJ = rTrn.codOrigenProceso
		OBJ = rTrn.tipoRezago

		rCli = New ccClientes(dsAux.Tables(0))
		'Dim aux As New DataSet()
		'aux = Sys.IngresoEgreso.Rezagos.calcularTipoRezago(gdbc, gidAdm, _
		rRez.tipoRezago = Sys.IngresoEgreso.Rezagos.calcularTipoRezago(gdbc, gidAdm, _
		   rCli.idCliente, _
		   rCli.fecEstadoAfiliado, _
		   rCli.fecAfiliacionAdm, _
		   rTrn.perCotizacion, _
		   fecOperacionCal, _
		   rRez.tasaTrabajoPesado, _
		   rRez.codMvto, _
		   rRez.numPlanilla, _
		   rRez.numPagina, _
		   rRez.seqLinea, _
		   rTrn.tipoPlanilla, _
		   rRez.tipoProducto, _
		   rRez.valMlAdicional, _
		   rRez.valMlAdicionalInteres, _
		   rRez.valMlAdicionalReajuste, _
		 rRez.valMlMonto, _
		 rRez.valMlInteres, _
		 rRez.valMlReajuste, _
		 rRez.valMlPrimaseguro, _
		 rRez.valMlPrimaseguroInt, _
		 rRez.valMlPrimaseguroRea, _
		 rRez.valMlComision, _
		 rRez.valCuoAdicional, _
		 rRez.valCuoAdicionalInteres, _
		 rRez.valCuoAdicionalReajuste, _
		 rRez.valCuoMonto, _
		 rRez.valCuoInteres, _
		 rRez.valCuoReajuste, _
		 rRez.valCuoPrimaseguro, _
		 rRez.valCuoPrimaseguroInt, _
		 rRez.valCuoPrimaseguroRea, _
		 rRez.valCuoComision, _
		 rRez.codCausalRezago)

		rRez.sexo = rCli.sexo


		If rRez.tipoRezago Is Nothing Or rRez.tipoRezago = 0 Then rRez.tipoRezago = rTrn.tipoRezago

		'NCG 145. 28/08/2015.
		If rTrn.codOrigenProceso = "ACREXAFC" Then
			rRez.tipoRezago = 37
		End If

		rTrn.tipoRezago = rRez.tipoRezago

		If gtipoProceso = "AC" Then
			rRez.numRezago = Sys.IngresoEgreso.Rezagos.crearconseq(gdbc, gidAdm, rRez.numPlanilla, _
			   rRez.folioPlanilla, rRez.folioConvenio, rRez.numPagina, rRez.seqLinea, _
			   rRez.perCotiza, rRez.tipoProducto, rRez.tipoFondo, _
			   rRez.codMvto, rRez.tipoPago, rRez.tipoPlanilla, _
			   rRez.tipoRemuneracion, rRez.tipoEntidadPagadora, _
			   rRez.fecOperacion, rRez.fecInicioGratificacion, _
			   rRez.fecFinGratificacion, rRez.idCliente, rRez.idEmpleadorOri, _
			   rRez.idPersonaOri, rRez.apPaternoOri, rRez.apMaternoOri, _
			   rRez.nombreOri, rRez.nombreAdicionalOri, rRez.codigoSoundex, _
			   rRez.idEmpleador, rRez.idPersona, rRez.apPaterno, rRez.apMaterno, _
			   rRez.nombre, rRez.nombreAdicional, rRez.valMlRentaImponible, _
			   rRez.valMlMontoNominal, rRez.valMlMonto, rRez.valMlInteres, rRez.valMlReajuste, _
			   rRez.valCuoMonto, rRez.valCuoInteres, rRez.valCuoReajuste, _
			   rRez.valMlAdicional, rRez.valMlAdicionalInteres, _
			   rRez.valMlAdicionalReajuste, rRez.valCuoAdicional, _
			   rRez.valCuoAdicionalInteres, rRez.valCuoAdicionalReajuste, _
			   rRez.fecValorCuota, rRez.valMlValorCuota, rRez.numCuotasPactadas, rRez.numCuotasPagadas, _
			   rRez.codOrigenMvto, rRez.codOrigenProceso, rRez.fecContableRezago, _
			   rRez.fecOperacionAdmOrigen, rRez.perContableRezago, _
			   rRez.tipoRezago, rRez.codCausalRezago, rRez.codCausalOriginal, rRez.fecReclasificacion, _
			   rRez.valCuoAjusteDecimal, rRez.codAdmOrigen, rRez.numDictamenOri, _
			   rRez.fecNotificacion, rRez.codTrabajoPesado, _
			   rRez.puestoTrabajoPesado, rRez.tasaTrabajoPesadoOri, _
			   rRez.tasaTrabajoPesado, rRez.estadoRezago, _
			   rRez.fecEstadoRezago, rRez.indRegimenTributario, rRez.numContrato, rRez.categoria, _
			   rRez.codMvtoPrim, rRez.codMvtoIntreaPrim, rRez.sexo, rRez.tasaPrima, rRez.valMlRentaImponibleSis, rRez.valMlPrimaseguro, rRez.valMlPrimaseguroInt, rRez.valMlPrimaseguroRea, rRez.valCuoPrimaseguro, rRez.valCuoPrimaseguroInt, rRez.valCuoPrimaseguroRea, rRez.valMlExcesoAfi, rRez.valMlExcesoEmp, _
			   gidUsuarioProceso, gfuncion, rRez.tipoOrigenDigitacion)
		End If
		Return rRez.numRezago
	End Function


	Private Function gratificacionUnPeriodo() As Boolean
		Try
			gratificacionUnPeriodo = False
			Dim fechaIni As Date = CDate(rTrn.fecInicioGratificacion)
			Dim fechaFin As Date = CDate(rTrn.fecFinGratificacion)
			If fechaIni = New Date(fechaIni.Year, fechaIni.Month, 1) And _
			 fechaFin = New Date(fechaIni.Year, fechaIni.Month, Date.DaysInMonth(fechaIni.Year, fechaIni.Month)) Then
				'gratificacion de un periodo
				gratificacionUnPeriodo = True
			Else
				gratificacionUnPeriodo = True
			End If
		Catch
			gratificacionUnPeriodo = True
		End Try
	End Function

	Private Sub valorizarRentabilidadRez(ByRef valMlValorizaRentaRez As Decimal, ByRef valCuoValorizaRentaRez As Decimal)
		'monto nominla de rentabilidad en el otro fondo
		Dim pesos As Decimal = rTrn.valMlAporteAdm
		Dim cuotas As Decimal = rTrn.valCuoAporteAdm
		If cuotas = 0 Then
			cuotas = Mat.Redondear(pesos / rTrn.valMlValorCuotaCaja, 2)
		End If

		If rTrn.tipoFondoDestinoCal <> "C" Then
			pesos = Mat.Redondear(cuotas * rTrn.valMlValorCuotaVal, 0)
			cuotas = Mat.Redondear(pesos / rTrn.valMlValorCuota, 2)
		End If
		valMlValorizaRentaRez = pesos
		valCuoValorizaRentaRez = cuotas
	End Sub


	Private Function codigoMvtoNcg264() As Boolean
		codigoMvtoNcg264 = False
		Dim existe As Integer
		existe = Sys.IngresoEgreso.sysRefExternas.Nominal.validaCodmvto(gdbc, gidAdm, rTrn.codMvto)
		If existe >= 1 Then Return True
	End Function

	Private Function validaCodbloqueo(ByVal codBloqueo As String) As Boolean
		validaCodbloqueo = False
		Dim existe As Integer
		existe = Sys.IngresoEgreso.sysRefExternas.Nominal.validaCodbloqueo(gdbc, gidAdm, rTrn.idCliente, rTrn.tipoProducto, rTrn.codOrigenProceso, rTrn.seqRegistro, rTrn.tipoImputacion, codBloqueo)
		If existe >= 1 Then Return True
	End Function

End Class
