Option Strict Off

Imports Sonda.Gestion.Adm.Sys.CodeCompletion

Imports Sonda.Net.DB
Imports Sonda.Net
Imports System.IO
Public Class INERegistros

    Public Class transacciones
        Public rtrn As ACR.ccTransacciones
        Public Sub New(ByVal s As String, ByVal ds As DataSet)
            Dim row() As Object

            row = s.Split(";")

            rtrn = New ACR.ccTransacciones(ds.Tables(0).NewRow)

            rtrn.codOrigenProceso = IIf(row(0) = "", Nothing, row(0))
            rtrn.usuarioProceso = IIf(row(1) = "", Nothing, row(1))
            rtrn.numeroId = IIf(row(2) = "", Nothing, row(2))
            rtrn.seqRegistro = IIf(row(3) = "", Nothing, row(3))
            rtrn.idPersona = IIf(row(4) = "", Nothing, row(4))
            rtrn.idCliente = IIf(row(5) = "", Nothing, row(5))
            rtrn.apPaterno = IIf(row(6) = "", Nothing, row(6))
            rtrn.apMaterno = IIf(row(7) = "", Nothing, row(7))
            rtrn.nombre = IIf(row(8) = "", Nothing, row(8))
            rtrn.nombreAdicional = IIf(row(9) = "", Nothing, row(9))
            rtrn.codSoundex = IIf(row(10) = "", Nothing, row(10))
            rtrn.perCotizacion = IIf(row(11) = "", Nothing, row(11))
            rtrn.numReferenciaOrigen1 = IIf(row(12) = "", Nothing, row(12))
            rtrn.numReferenciaOrigen2 = IIf(row(13) = "", Nothing, row(13))
            rtrn.numReferenciaOrigen3 = IIf(row(14) = "", Nothing, row(14))
            rtrn.numReferenciaOrigen4 = IIf(row(15) = "", Nothing, row(15))
            rtrn.numReferenciaOrigen5 = IIf(row(16) = "", Nothing, row(16))
            rtrn.numReferenciaOrigen6 = IIf(row(17) = "", Nothing, row(17))
            rtrn.tipoRemuneracion = IIf(row(18) = "", Nothing, row(18))
            rtrn.tipoPago = IIf(row(19) = "", Nothing, row(19))
            rtrn.tipoPlanilla = IIf(row(20) = "", Nothing, row(20))
            rtrn.tipoEntidadPagadora = IIf(row(21) = "", Nothing, row(21))
            rtrn.tipoCliente = IIf(row(22) = "", Nothing, row(22))
            rtrn.fecInicioGratificacion = IIf(row(23) = "", Nothing, row(23))
            rtrn.fecFinGratificacion = IIf(row(24) = "", Nothing, row(24))
            rtrn.numPeriodosCai = IIf(row(25) = "", Nothing, row(25))
            rtrn.fecOperacion = IIf(row(26) = "", Nothing, row(26))
            rtrn.fecOperacionAdmOrigen = IIf(row(27) = "", Nothing, row(27))
            rtrn.fecDeposito = IIf(row(28) = "", Nothing, row(28))
            rtrn.idEmpleador = IIf(row(29) = "", Nothing, row(29))
            rtrn.folioConvenio = IIf(row(30) = "", Nothing, row(30))
            rtrn.idAlternativoDoc = IIf(row(31) = "", Nothing, row(31))
            rtrn.numCuotasPactadas = IIf(row(32) = "", Nothing, row(32))
            rtrn.numCuotasPagadas = IIf(row(33) = "", Nothing, row(33))
            rtrn.valMlRentaImponible = IIf(row(34) = "", Nothing, row(34))
            rtrn.tipoProducto = IIf(row(35) = "", Nothing, row(35))
            rtrn.tipoFondoOrigen = IIf(row(36) = "", Nothing, row(36))
            rtrn.tipoFondoDestino = IIf(row(37) = "", Nothing, row(37))
            rtrn.categoria = IIf(row(38) = "", Nothing, row(38))
            rtrn.tipoImputacion = IIf(row(39) = "", Nothing, row(39))
            rtrn.codOrigenTransaccion = IIf(row(40) = "", Nothing, row(40))
            rtrn.codDestinoTransaccion = IIf(row(41) = "", Nothing, row(41))
            rtrn.codDestinoTransaccionCal = IIf(row(42) = "", Nothing, row(42))
            rtrn.codOrigenRecaudacion = IIf(row(43) = "", Nothing, row(43))
            rtrn.seqMvtoOrigen = IIf(row(44) = "", Nothing, row(44))
            rtrn.seqMvtoDestino = IIf(row(45) = "", Nothing, row(45))
            rtrn.codOrigenMvto = IIf(row(46) = "", Nothing, row(46))
            rtrn.codMvto = IIf(row(47) = "", Nothing, row(47))
            rtrn.codMvtoAdi = IIf(row(48) = "", Nothing, row(48))
            rtrn.codMvtoIntreaCue = IIf(row(49) = "", Nothing, row(49))
            rtrn.codMvtoIntreaAdi = IIf(row(50) = "", Nothing, row(50))
            rtrn.codMvtoComPor = IIf(row(51) = "", Nothing, row(51))
            rtrn.codMvtoComFij = IIf(row(52) = "", Nothing, row(52))
            rtrn.idInstOrigen = IIf(row(53) = "", Nothing, row(53))
            rtrn.idInstDestino = IIf(row(54) = "", Nothing, row(54))
            rtrn.codCausalRezago = IIf(row(55) = "", Nothing, row(55))
            rtrn.codCausalRezagoCal = IIf(row(56) = "", Nothing, row(56))
            rtrn.codCausalAjuste = IIf(row(57) = "", Nothing, row(57))
            rtrn.fecValorCuotaVal = IIf(row(58) = "", Nothing, row(58))
            rtrn.valMlValorCuotaVal = IIf(row(59) = "", Nothing, row(59))
            rtrn.perContable = IIf(row(60) = "", Nothing, row(60))
            rtrn.fecAcreditacion = IIf(row(61) = "", Nothing, row(61))
            rtrn.fecValorCuota = IIf(row(62) = "", Nothing, row(62))
            rtrn.valMlValorCuota = IIf(row(63) = "", Nothing, row(63))
            rtrn.fecValorUfExceso = IIf(row(64) = "", Nothing, row(64))
            rtrn.fecValorCuotaCaja = IIf(row(65) = "", Nothing, row(65))
            rtrn.valMlValorCuotaCaja = IIf(row(66) = "", Nothing, row(66))
            rtrn.porcInstSalud = IIf(row(67) = "", Nothing, row(67))
            rtrn.valUfInstSalud = IIf(row(68) = "", Nothing, row(68))
            rtrn.tasaCotizacion = IIf(row(69) = "", Nothing, row(69))
            rtrn.tasaAdicional = IIf(row(70) = "", Nothing, row(70))
            rtrn.tasaInteres = IIf(row(71) = "", Nothing, row(71))
            rtrn.tasaReajuste = IIf(row(72) = "", Nothing, row(72))
            rtrn.indMontoPagado = IIf(row(73) = "", Nothing, row(73))
            rtrn.valMlMvto = IIf(row(74) = "", Nothing, row(74))
            rtrn.valMlReajuste = IIf(row(75) = "", Nothing, row(75))
            rtrn.valMlInteres = IIf(row(76) = "", Nothing, row(76))
            rtrn.valCuoMvto = IIf(row(77) = "", Nothing, row(77))
            rtrn.valCuoReajuste = IIf(row(78) = "", Nothing, row(78))
            rtrn.valCuoInteres = IIf(row(79) = "", Nothing, row(79))
            rtrn.valMlAdicional = IIf(row(80) = "", Nothing, row(80))
            rtrn.valMlAdicionalReajuste = IIf(row(81) = "", Nothing, row(81))
            rtrn.valMlAdicionalInteres = IIf(row(82) = "", Nothing, row(82))
            rtrn.valCuoAdicional = IIf(row(83) = "", Nothing, row(83))
            rtrn.valCuoAdicionalReajuste = IIf(row(84) = "", Nothing, row(84))
            rtrn.valCuoAdicionalInteres = IIf(row(85) = "", Nothing, row(85))
            rtrn.tipoComisionPorcentual = IIf(row(86) = "", Nothing, row(86))
            rtrn.valMlComisPorcentual = IIf(row(87) = "", Nothing, row(87))
            rtrn.valCuoComisPorcentual = IIf(row(88) = "", Nothing, row(88))
            rtrn.valMlCuotaComision = IIf(row(89) = "", Nothing, row(89))
            rtrn.tipoComisionFija = IIf(row(90) = "", Nothing, row(90))
            rtrn.valMlComisFija = IIf(row(91) = "", Nothing, row(91))
            rtrn.valCuoComisFija = IIf(row(92) = "", Nothing, row(92))
            rtrn.tipoImputacionAdm = IIf(row(93) = "", Nothing, row(93))
            rtrn.valMlAporteAdm = IIf(row(94) = "", Nothing, row(94))
            rtrn.valCuoAporteAdm = IIf(row(95) = "", Nothing, row(95))
            rtrn.idInstSalud = IIf(row(96) = "", Nothing, row(96))
            rtrn.valMlSalud = IIf(row(97) = "", Nothing, row(97))
            rtrn.valCuoSalud = IIf(row(98) = "", Nothing, row(98))
            rtrn.valCuoTransferencia = IIf(row(99) = "", Nothing, row(99))
            rtrn.valMlTransferencia = IIf(row(100) = "", Nothing, row(100))
            rtrn.valMlExcesoLinea = IIf(row(101) = "", Nothing, row(101))
            rtrn.valCuoExcesoLinea = IIf(row(102) = "", Nothing, row(102))
            rtrn.tipoFondoDestinoCal = IIf(row(103) = "", Nothing, row(103))
            rtrn.valMlPatrFrecCal = IIf(row(104) = "", Nothing, row(104))
            rtrn.valCuoPatrFrecCal = IIf(row(105) = "", Nothing, row(105))
            rtrn.valCuoPatrFrecActCal = IIf(row(106) = "", Nothing, row(106))
            rtrn.valMlPatrFrecActCal = IIf(row(107) = "", Nothing, row(107))
            rtrn.valMlPatrFdesCal = IIf(row(108) = "", Nothing, row(108))
            rtrn.valCuoPatrFdesCal = IIf(row(109) = "", Nothing, row(109))
            rtrn.valMlMvtoCal = IIf(row(110) = "", Nothing, row(110))
            rtrn.valMlReajusteCal = IIf(row(111) = "", Nothing, row(111))
            rtrn.valMlInteresCal = IIf(row(112) = "", Nothing, row(112))
            rtrn.valCuoMvtoCal = IIf(row(113) = "", Nothing, row(113))
            rtrn.valCuoReajusteCal = IIf(row(114) = "", Nothing, row(114))
            rtrn.valCuoInteresCal = IIf(row(115) = "", Nothing, row(115))
            rtrn.valMlAdicionalCal = IIf(row(116) = "", Nothing, row(116))
            rtrn.valMlAdicionalInteresCal = IIf(row(117) = "", Nothing, row(117))
            rtrn.valMlAdicionalReajusteCal = IIf(row(118) = "", Nothing, row(118))
            rtrn.valCuoAdicionalCal = IIf(row(119) = "", Nothing, row(119))
            rtrn.valCuoAdicionalReajusteCal = IIf(row(120) = "", Nothing, row(120))
            rtrn.valCuoAdicionalInteresCal = IIf(row(121) = "", Nothing, row(121))
            rtrn.valMlComisPorcentualCal = IIf(row(122) = "", Nothing, row(122))
            rtrn.valCuoComisPorcentualCal = IIf(row(123) = "", Nothing, row(123))
            rtrn.seqComisionPorcentual = IIf(row(124) = "", Nothing, row(124))
            rtrn.valMlComisFijaCal = IIf(row(125) = "", Nothing, row(125))
            rtrn.valCuoComisFijaCal = IIf(row(126) = "", Nothing, row(126))
            rtrn.seqComisionFija = IIf(row(127) = "", Nothing, row(127))
            rtrn.codDestinoExcesoTopeCal = IIf(row(128) = "", Nothing, row(128))
            rtrn.seqDestinoExcesoTopeCal = IIf(row(129) = "", Nothing, row(129))
            rtrn.codMvtoExcesoTopeCal = IIf(row(130) = "", Nothing, row(130))
            rtrn.valMlExcesoTopeCal = IIf(row(131) = "", Nothing, row(131))
            rtrn.valCuoExcesoTopeCal = IIf(row(132) = "", Nothing, row(132))
            rtrn.codDestinoExcesoLineaCal = IIf(row(133) = "", Nothing, row(133))
            rtrn.seqDestinoExcesoLineaCal = IIf(row(134) = "", Nothing, row(134))
            rtrn.codMvtoExcesoLineaCal = IIf(row(135) = "", Nothing, row(135))
            rtrn.valMlExcesoLineaCal = IIf(row(136) = "", Nothing, row(136))
            rtrn.valCuoExcesoLineaCal = IIf(row(137) = "", Nothing, row(137))
            rtrn.valMlTransferenciaCal = IIf(row(138) = "", Nothing, row(138))
            rtrn.valCuoTransferenciaCal = IIf(row(139) = "", Nothing, row(139))
            rtrn.seqDestinoTrfCal = IIf(row(140) = "", Nothing, row(140))
            rtrn.valMlPrimaCal = IIf(row(141) = "", Nothing, row(141))
            rtrn.valMlIntPrimaCal = IIf(row(142) = "", Nothing, row(142))
            rtrn.valMlReaPrimaCal = IIf(row(143) = "", Nothing, row(143))
            rtrn.valCuoPrimaCal = IIf(row(144) = "", Nothing, row(144))
            rtrn.valIndPagoPrimCal = IIf(row(145) = "", Nothing, row(145))
            rtrn.valIdInstPagoPrimCal = IIf(row(146) = "", Nothing, row(146))
            rtrn.valMlCompensCal = IIf(row(147) = "", Nothing, row(147))
            rtrn.valCuoCompensCal = IIf(row(148) = "", Nothing, row(148))
            rtrn.seqDestinoCompenCal = IIf(row(149) = "", Nothing, row(149))
            rtrn.valMlAjusteDecimalCal = IIf(row(150) = "", Nothing, row(150))
            rtrn.valCuoAjusteDecimalCal = IIf(row(151) = "", Nothing, row(151))
            rtrn.numSaldo = IIf(row(152) = "", Nothing, row(152))
            rtrn.valMlSaldo = IIf(row(153) = "", Nothing, row(153))
            rtrn.valCuoSaldo = IIf(row(154) = "", Nothing, row(154))
            rtrn.seqMvtoSaldoAnterior = IIf(row(155) = "", Nothing, row(155))
            rtrn.valCuoSaldoAnterior = IIf(row(156) = "", Nothing, row(156))
            rtrn.valMlSaldoAnterior = IIf(row(157) = "", Nothing, row(157))
            rtrn.numRetiros = IIf(row(158) = "", Nothing, row(158))
            rtrn.codAjusteMovimiento = IIf(row(159) = "", Nothing, row(159))
            rtrn.indInsistenciaAcr = IIf(row(160) = "", Nothing, row(160))
            rtrn.indCierreProducto = IIf(row(161) = "", Nothing, row(161))
            rtrn.indMvtoVisibleCartola = IIf(row(162) = "", Nothing, row(162))
            rtrn.perCuatrimestre = IIf(row(163) = "", Nothing, row(163))
            rtrn.numDictamen = IIf(row(164) = "", Nothing, row(164))
            rtrn.codTrabajoPesado = IIf(row(165) = "", Nothing, row(165))
            rtrn.puestoTrabajoPesado = IIf(row(166) = "", Nothing, row(166))
            rtrn.indCobranza = IIf(row(167) = "", Nothing, row(167))
            rtrn.codError = IIf(row(168) = "", Nothing, row(168))
            rtrn.estadoTransaccion = IIf(row(169) = "", Nothing, row(169))
            rtrn.estadoReg = IIf(row(170) = "", Nothing, row(170))
            rtrn.fecEstadoReg = IIf(row(171) = "", Nothing, row(171))
            rtrn.fecIngReg = IIf(row(172) = "", Nothing, row(172))
            rtrn.idUsuarioIngReg = IIf(row(173) = "", Nothing, row(173))
            rtrn.fecUltModifReg = IIf(row(174) = "", Nothing, row(174))
            rtrn.idUsuarioUltModifReg = IIf(row(175) = "", Nothing, row(175))
            rtrn.idFuncionUltModifReg = IIf(row(176) = "", Nothing, row(176))



        End Sub

    End Class
    Public Class tiposProductos
        Public rtrn As AAA.ccTiposProducto
        Public Sub New(ByVal s As String, ByVal ds As DataSet)
            Dim row() As Object

            row = s.Split(";")

            rtrn = New AAA.ccTiposProducto(ds.Tables(0).NewRow)
            rtrn.idCliente = IIf(row(0) = "", Nothing, row(0))
            rtrn.numTipoProducto = IIf(row(1) = "", Nothing, row(1))
            rtrn.tipoProducto = IIf(row(2) = "", Nothing, row(2))
            rtrn.fecAperturaTipoProducto = IIf(row(3) = "", Nothing, row(3))
            rtrn.fecCierreTipoProducto = IIf(row(4) = "", Nothing, row(4))
            rtrn.fecOrigen = IIf(row(5) = "", Nothing, row(5))
            rtrn.codInstitucionOrigen = IIf(row(6) = "", Nothing, row(6))
            rtrn.codInstitucionDestino = IIf(row(7) = "", Nothing, row(7))
            rtrn.codInstitucionFusion = IIf(row(8) = "", Nothing, row(8))
            rtrn.perPrimerPago = IIf(row(9) = "", Nothing, row(9))
            rtrn.fecUltimoPago = IIf(row(10) = "", Nothing, row(10))
            rtrn.indCotizante = IIf(row(11) = "", Nothing, row(11))
            rtrn.seqDireccion = IIf(row(12) = "", Nothing, row(12))
            rtrn.indEnvioCartola = IIf(row(13) = "", Nothing, row(13))
            rtrn.tipoCartola = IIf(row(14) = "", Nothing, row(14))
            rtrn.numSolicitudAut = IIf(row(15) = "", Nothing, row(15))
            rtrn.numSolicitudUltModif = IIf(row(16) = "", Nothing, row(16))
            rtrn.tipoOrigenProducto = IIf(row(17) = "", Nothing, row(17))
            rtrn.indFuturoFinProducto = IIf(row(18) = "", Nothing, row(18))
            rtrn.tipoFinProducto = IIf(row(19) = "", Nothing, row(19))
            rtrn.fecFinProducto = IIf(row(20) = "", Nothing, row(20))
            rtrn.tipoFondoRecaudacion = IIf(row(21) = "", Nothing, row(21))
            rtrn.valMlSaldoEmbargo = IIf(row(22) = "", Nothing, row(22))
            rtrn.fecEmbargo = IIf(row(23) = "", Nothing, row(23))
            rtrn.codRegTributario = IIf(row(24) = "", Nothing, row(24))
            rtrn.fecRegTributario = IIf(row(25) = "", Nothing, row(25))
            rtrn.tipoEleccionFondos = IIf(row(26) = "", Nothing, row(26))
            rtrn.fecEleccionFondos = IIf(row(27) = "", Nothing, row(27))
            rtrn.estadoProducto = IIf(row(28) = "", Nothing, row(28))
            rtrn.fecEstadoProducto = IIf(row(29) = "", Nothing, row(29))
            rtrn.estadoReg = IIf(row(30) = "", Nothing, row(30))



        End Sub

    End Class
    Public Class Productos
        Public rtrn As AAA.ccProductos
        Public Sub New(ByVal s As String, ByVal ds As DataSet)
            Dim row() As Object

            row = s.Split(";")

            rtrn = New AAA.ccProductos(ds.Tables(0).NewRow)

            rtrn.idCliente = IIf(row(0) = "", Nothing, row(0))
            rtrn.numProducto = IIf(row(1) = "", Nothing, row(1))
            rtrn.tipoProducto = IIf(row(2) = "", Nothing, row(2))
            rtrn.tipoFondo = IIf(row(3) = "", Nothing, row(3))
            rtrn.numTipoProducto = IIf(row(4) = "", Nothing, row(4))
            rtrn.indBloqueo = IIf(row(5) = "", Nothing, row(5))
            rtrn.codBloqueo = IIf(row(6) = "", Nothing, row(6))
            rtrn.fecInicioBloqueo = IIf(row(7) = "", Nothing, row(7))
            rtrn.fecFinBloqueo = IIf(row(8) = "", Nothing, row(8))
            rtrn.fecAperturaProducto = IIf(row(9) = "", Nothing, row(9))
            rtrn.fecCierreProducto = IIf(row(10) = "", Nothing, row(10))
            rtrn.numSolicitudAut = IIf(row(11) = "", Nothing, row(11))
            rtrn.numSolicitudUltModif = IIf(row(12) = "", Nothing, row(12))
            rtrn.tipoCuenta = IIf(row(13) = "", Nothing, row(13))
            rtrn.estadoReg = IIf(row(14) = "", Nothing, row(14))


        End Sub

    End Class
    Public Class saldos
        Public rtrn As AAA.ccSaldos
        Public Sub New(ByVal s As String, ByVal ds As DataSet)
            Dim row() As Object

            row = s.Split(";")

            rtrn = New AAA.ccSaldos(ds.Tables(0).NewRow)
            rtrn.idCliente = IIf(row(0) = "", Nothing, row(0))
            rtrn.numSaldo = IIf(row(1) = "", Nothing, row(1))
            rtrn.categoria = IIf(row(2) = "", Nothing, row(2))
            rtrn.tipoProducto = IIf(row(3) = "", Nothing, row(3))
            rtrn.tipoFondo = IIf(row(4) = "", Nothing, row(4))
            rtrn.numTipoProducto = IIf(row(5) = "", Nothing, row(5))
            rtrn.numProducto = IIf(row(6) = "", Nothing, row(6))
            rtrn.numSolicitudAut = IIf(row(7) = "", Nothing, row(7))
            rtrn.valMlSaldo = IIf(row(8) = "", Nothing, row(8))
            rtrn.valCuoSaldo = IIf(row(9) = "", Nothing, row(9))
            rtrn.valUtmSaldo = IIf(row(10) = "", Nothing, row(10))
            rtrn.fecValorCuota = IIf(row(11) = "", Nothing, row(11))
            rtrn.seqUltMvto = IIf(row(12) = "", Nothing, row(12))
            rtrn.codRegTributario = IIf(row(13) = "", Nothing, row(13))
            rtrn.fecAperturaSaldo = IIf(row(14) = "", Nothing, row(14))
            rtrn.fecCierreSaldo = IIf(row(15) = "", Nothing, row(15))
            rtrn.estadoSaldo = IIf(row(16) = "", Nothing, row(16))
            rtrn.estadoReg = IIf(row(17) = "", Nothing, row(17))



        End Sub

    End Class
    Public Class controlRentas
        Public rtrn As ACR.ccControlRentas
        Public Sub New(ByVal s As String, ByVal ds As DataSet)
            Dim row() As Object

            row = s.Split(";")

            rtrn = New ACR.ccControlRentas(ds.Tables(0).NewRow)
            rtrn.idCliente = IIf(row(0) = "", Nothing, row(0))
            rtrn.perCotizado = IIf(row(1) = "", Nothing, row(1))
            rtrn.tipoProducto = IIf(row(2) = "", Nothing, row(2))
            rtrn.tipoCotizacion = IIf(row(3) = "", Nothing, row(3))
            rtrn.idPersona = IIf(row(4) = "", Nothing, row(4))
            rtrn.valMlRentaAcum = IIf(row(5) = "", Nothing, row(5))
            rtrn.valUfRentaAcum = IIf(row(6) = "", Nothing, row(6))
            rtrn.valMlComisPorcentual = IIf(row(7) = "", Nothing, row(7))
            rtrn.numComsionFija = IIf(row(8) = "", Nothing, row(8))
            rtrn.seqMvtoComisFija = IIf(row(9) = "", Nothing, row(9))
            rtrn.valMlComisFija = IIf(row(10) = "", Nothing, row(10))
            rtrn.valCuoComisFija = IIf(row(11) = "", Nothing, row(11))



        End Sub

    End Class

End Class
