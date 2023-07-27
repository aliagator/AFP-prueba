Option Explicit On 
Option Strict Off

Imports Sonda.Net.DB
Imports Sonda.Net
Imports Sonda.Gestion.Adm.Sys.IngresoEgreso
Imports Sonda.Gestion.Adm.Sys.CodeCompletion
Imports Sonda.Gestion.Adm.Sys.AdministracionClientes


Public Class INEPrimasSeguros

    Public Shared Sub DeterminarMontoPrima(ByVal dbc As OraConn, ByVal idAdm As Integer, ByVal idCliente As Integer, ByVal tipoCliente As String, ByVal indFuturoFinProducto As String, ByVal codAdmOrigen As Decimal, ByVal codAdmDestino As Decimal, ByVal codAdmActual As Decimal, ByVal perCotiza As Date, ByVal fecAfiliacionAdm As Date, ByVal fecAfiliacionSis As Date, ByVal tipoProducto As String, ByVal tipoOrigenProducto As String, ByVal tipoFinProducto As String, ByVal valValorCuota As Decimal, ByVal valMlAdicional As Decimal, ByVal tasaComision As Decimal, ByVal sexo As String, ByRef valMlMontoPrima As Decimal, ByRef valCuoMontoPrima As Decimal, ByRef codDestinoPrima As String, ByRef codInstitucionPrima As Decimal, ByRef TasaPrima As Decimal, ByRef codError As Decimal)

        Dim ds As DataSet
        Dim rPri As PAR.ccAcrSeguro
        Dim idAdmSeguro As Integer = 0
        ' Dim tasaComision As Decimal
        Dim perPrima As Date
        Dim TasaPrimaval As Decimal

        valMlMontoPrima = 0 : valCuoMontoPrima = 0

        Select Case tipoCliente

            Case "DA", "DN"

                perPrima = fecAfiliacionAdm.AddMonths(-1)

                If perPrima.Year = perCotiza.Year And _
                   perPrima.Month = perCotiza.Month And _
                   (tipoOrigenProducto = "PFTI" Or tipoOrigenProducto = "TID") Then 'Es su primera cotizacion y biene por traspaso normal

                    If IsNothing(codAdmOrigen) Or codAdmOrigen = 0 Then


                        idAdmSeguro = 0 'Es nuevo en el sistema

                    Else

                        idAdmSeguro = codAdmOrigen
                        codDestinoPrima = "A"

                    End If

                End If

            Case "I", "IS"

                If indFuturoFinProducto = "S" Then

                    If IsNothing(codAdmDestino) Or codAdmDestino = 0 Then

                        idAdmSeguro = 0

                    Else

                        idAdmSeguro = codAdmDestino
                        codDestinoPrima = "N"

                    End If

                End If

        End Select

        If idAdmSeguro = 0 Or idAdmSeguro = codAdmActual Then

            idAdmSeguro = codAdmActual
            codDestinoPrima = "P"
        Else
            Dim dsAux As DataSet

            dsAux = Sys.Soporte.Parametro.traerGlobal(dbc, "PAR_ACR_ADM_PRIMAS", New Object() {idAdm, idAdmSeguro})
            If dsAux.Tables(0).Rows.Count > 0 Then
                If IsDBNull(dsAux.Tables(0).Rows(0).Item("COD_ADM_PRIMA")) Then
                    idAdmSeguro = codAdmActual
                    codDestinoPrima = "P"
                Else
                    idAdmSeguro = dsAux.Tables(0).Rows(0).Item("COD_ADM_PRIMA")
                End If

                If idAdmSeguro = 0 Or idAdmSeguro = codAdmActual Then
                    idAdmSeguro = codAdmActual
                    codDestinoPrima = "P"
                End If
            Else
                idAdmSeguro = codAdmActual
                codDestinoPrima = "P"
            End If
        End If




        If codDestinoPrima <> "P" Then
            DeterminaTasaComision(dbc, idAdm, perCotiza, tipoProducto, "PRE1", tipoCliente, idAdmSeguro, "POR", tasaComision, codError)
            If codError <> 0 Then Exit Sub

            If tasaComision = 0 Then
                valMlMontoPrima = 0
                valCuoMontoPrima = 0
                Exit Sub
            End If
        End If


        If Not INEProcesosAcr2.codAdmF = Nothing Then
            ds = PrimasCiasSeguro.traerUltimoValor(dbc, idAdm, INEProcesosAcr2.codAdmF, perCotiza, tipoProducto, tipoCliente, "PRI", "M")
            INEProcesosAcr2.codAdmF = Nothing
        Else
            ds = PrimasCiasSeguro.traerUltimoValor(dbc, idAdm, idAdmSeguro, perCotiza, tipoProducto, tipoCliente, "PRI", "M")
        End If

        If ds.Tables(0).Rows.Count > 0 Then
            rPri = New PAR.ccAcrSeguro(ds)
            If codDestinoPrima = "P" Then
                codInstitucionPrima = rPri.idCompSeguro
            Else
                codInstitucionPrima = idAdmSeguro
            End If

            Select Case rPri.tipoValorSeguro
                Case "TAS"
                    TasaPrima = rPri.valMonto
                    TasaPrimaval = rPri.valMonto / tasaComision
                Case "POR"
                    TasaPrima = Mat.Redondear(rPri.valMonto / 100, 2)
                    TasaPrimaval = Mat.Redondear(rPri.valMonto / (tasaComision * 100), 7)
            End Select
        End If

        If TasaPrimaval = 0 Then
            valMlMontoPrima = 0
            valCuoMontoPrima = 0
        End If

        valMlMontoPrima = Mat.Redondear(TasaPrimaval * valMlAdicional)
        valCuoMontoPrima = Mat.Redondear(valMlMontoPrima / valValorCuota, 2)

        If valCuoMontoPrima = 0 Then
            valMlMontoPrima = 0
        End If

    End Sub


    Public Shared Sub DeterminarMontoPrimaSIS(ByVal dbc As OraConn, _
                                              ByVal idAdm As Integer, _
                                              ByVal idCliente As Integer, _
                                              ByVal tipoCliente As String, _
                                              ByVal codAdmOrigen As Decimal, _
                                              ByVal codAdmDestino As Decimal, _
                                              ByVal codAdmActual As Decimal, _
                                              ByVal perCotiza As Date, _
                                              ByVal tipoProducto As String, _
                                              ByVal valValorCuota As Decimal, _
                                              ByVal valMlrentaImponibleSis As Decimal, _
                                              ByVal sexo As String, _
                                              ByRef valMlMontoPrima As Decimal, _
                                              ByRef valCuoMontoPrima As Decimal, _
                                              ByRef codDestinoPrima As String, _
                                              ByRef codInstitucionPrima As Decimal, _
                                              ByRef TasaPrima As Decimal, _
                                              ByRef codError As Decimal)

        Dim ds As DataSet
        Dim rPri As PAR.ccAcrSeguro
        valMlMontoPrima = 0 : valCuoMontoPrima = 0

        ds = PrimasCiasSeguro.traerUltimoValor(dbc, idAdm, idCliente, codAdmActual, perCotiza, tipoProducto, tipoCliente, "SIS", sexo)
        If codError <> 0 Then Exit Sub

        If ds.Tables(0).Rows.Count > 0 Then
            rPri = New PAR.ccAcrSeguro(ds)
            TasaPrima = rPri.valMonto
        End If

        If TasaPrima = 0 Then
            valMlMontoPrima = 0
            valCuoMontoPrima = 0
        Else
            valMlMontoPrima = Mat.Redondear((TasaPrima / 100) * valMlrentaImponibleSis)
            valCuoMontoPrima = Mat.Redondear(valMlMontoPrima / valValorCuota, 2)
        End If

        'lfc: se comenta -CA-2009080201    -20/08/2009
        'If valCuoMontoPrima = 0 Then
        '    valMlMontoPrima = 0
        'End If

        'SIS// sin transferencia
        codDestinoPrima = "S"
        codInstitucionPrima = 0

    End Sub



    Public Shared Sub Reliquidacion(ByVal dbc As OraConn, ByVal idAdm As Integer, ByVal rTrn As ACR.ccTransacciones, ByVal rCli As AAA.ccClientes, ByVal usu As String, ByVal fun As String, ByRef valMlPrima As Decimal, ByVal valCuoPrima As Decimal, ByRef gcodErrorIgnorar As Integer)


        Dim ds As DataSet
        Dim rPri As ACR.ccPrimasCiasSeg
        Dim blExiste As Boolean

        valMlPrima = 0
        valCuoPrima = 0
        gcodErrorIgnorar = 0

        ds = PrimasCiasSeguro.traer(dbc, idAdm, rTrn.tipoFondoDestinoCal, rTrn.perContable, rTrn.valIdInstPagoPrimCal, rTrn.idPersona, rTrn.seqRegistro, rTrn.perCotizacion)
        If ds.Tables(0).Rows.Count > 0 Then
            rPri = New ACR.ccPrimasCiasSeg(ds)
            blExiste = True
            If Not IsNothing(rPri.tipoPago) Then
                Exit Sub
            End If
        Else
            blExiste = False
            rPri = New ACR.ccPrimasCiasSeg(ds.Tables(0).NewRow)
        End If


        DeterminarMontoPrima(dbc, idAdm, rTrn.idCliente, rTrn.tipoCliente, "S", rCli.codAdmOrigen, rCli.codAdmDestino, rCli.codAdmActual, rTrn.perCotizacion, rCli.fecAfiliacionAdm, rCli.fecAfiliacionSistema, rTrn.tipoProducto, Nothing, Nothing, rTrn.valMlCuotaComision, rTrn.valMlAdicionalCal + rTrn.valMlAdicionalInteresCal + rTrn.valMlAdicionalReajusteCal, rTrn.tasaAdicional, rTrn.sexo, rPri.valMlPrimaSeguro, valCuoPrima, rPri.tipoPago, rPri.codInstFinanciera, rPri.porcPrimaSeguro, gcodErrorIgnorar)

        If gcodErrorIgnorar <> 0 Then
            Exit Sub
        End If

        If rPri.valMlPrimaSeguro <= 0 Then
            Exit Sub
        End If

        Select Case rPri.tipoPago

            Case "P"
                rPri.codMvto = Nothing
                rPri.tipoPago = Nothing
                Exit Sub

            Case "A"
                rPri.codMvto = 120804 'dependiente a administradora antigua
                rPri.tipoPago = 1
                Exit Sub

            Case "N"
                rPri.codMvto = 120805 'independiente hacia administradora nueva
                rPri.tipoPago = 2

        End Select

        rPri.codOrigenProceso = "RELPRIMA"
        rPri.fecOperacion = rTrn.fecAcreditacion
        rPri.idAdmCobroAdicional = rCli.codAdmActual
        rPri.idEmpleador = rTrn.idEmpleador
        rPri.idPersona = rTrn.idPersona
        rPri.indDerechoSeguro = "S"
        rPri.perCotiza = rTrn.perCotizacion
        rPri.perProceso = rTrn.perContable
        rPri.porcPrimaSeguro = Mat.Redondear(rPri.porcPrimaSeguro * 100, 2)
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
        rPri.valMlRentaImponible = rTrn.valMlRentaImponible
        rPri.porcAdicional = rTrn.tasaAdicional

        'SIS//
        rPri.sexo = rTrn.sexo
        rPri.fecAcreditacion = rTrn.fecAcreditacion
        rPri.valMlPrimaInteres = rTrn.valMlPrimaSisInteres
        rPri.valCuoPrimaReajuste = rTrn.valMlPrimaSisReajuste
        rPri.valCuoPrimaSeguro = rTrn.valCuoPrimaSis
        rPri.valCuoPrimaInteres = rTrn.valCuoPrimaSisInteres
        rPri.valCuoPrimaReajuste = rTrn.valCuoPrimaSisReajuste



        If rTrn.valIndPagoPrimCal = "A" Or rTrn.valIndPagoPrimCal = "N" Then

            valMlPrima = rPri.valMlPrimaSeguro
            PrimasCiasSeguro.crear(dbc, idAdm, rPri.tipoFondo, rPri.perProceso, rPri.codInstFinanciera, rPri.idPersona, rPri.seqMovimiento, "ABO", rPri.perCotiza, rPri.tipoTrabajador, rPri.indDerechoSeguro, rPri.codOrigenProceso, rPri.fecOperacion, rPri.tipoPago, rPri.valMlCco, rPri.valCuoCco, rPri.valMlComisionFija, rPri.valMlRentaImponible, rPri.valMlAdicional, rPri.valMlAdicionalInteres, rPri.valMlAdicionalReajuste, rPri.valMlPrimaSeguro, rPri.idAdmCobroAdicional, rPri.codMvto, rPri.porcPrimaSeguro, rPri.porcAdicional, rPri.idEmpleador, usu, fun, rPri.sexo, rPri.fecAcreditacion, rPri.valMlPrimaInteres, rPri.valCuoPrimaReajuste, rPri.valCuoPrimaSeguro, rPri.valCuoPrimaInteres, rPri.valCuoPrimaReajuste)

        End If

    End Sub
    Private Shared Sub DeterminaTasaComision(ByVal dbc As OraConn, ByVal idAdm As Integer, ByVal perCotiza As Date, ByVal tipoProducto As String, ByVal tipoComision As String, ByVal tipoCliente As String, ByVal idAdmSeguro As Integer, ByVal tipoCobroComision As String, ByRef comisionPorcentual As Decimal, ByRef coderror As Integer)
        Dim ds As DataSet
        Dim rComPor As PAR.ccAcrComisiones

        ds = Comisiones.traerUltimoValor(dbc, idAdm, perCotiza, tipoProducto, tipoComision, tipoCliente, idAdmSeguro, tipoCobroComision)

        If ds.Tables(0).Rows.Count = 0 Then
            coderror = 15338 '"No existe comision para periodo

        Else
            rComPor = New PAR.ccAcrComisiones(ds)

            Select Case rComPor.tipoValorComision

                Case "ML", "UF"
                    coderror = 15339 '"Tipo valor comision incorrecto

                Case "TAS" : comisionPorcentual = rComPor.valMonto

                Case "POR" : comisionPorcentual = Mat.Redondear(rComPor.valMonto / 100, 9)

                Case Else
                    coderror = 15339 '"Tipo valor comision incorrecto
            End Select
        End If
    End Sub
    Public Shared Sub rebajaPrimaCiaSeg(ByVal dbc As OraConn, ByVal idAdm As Integer, ByVal rCli As AAA.ccClientes, ByVal rTrn As ACR.ccTransacciones, ByVal codAdmActual As Decimal, ByVal usu As String, ByVal fun As String)

        Dim ds As DataSet
        Dim rPri As PAR.ccAcrSeguro
        Dim rCom As PAR.ccAcrComisiones
        Dim rPrima As ACR.ccPrimasCiasSeg

        Dim tipoSeguro As String = "SIS"
        Dim sexo As String = rTrn.sexo

        If rTrn.perCotizacion < New Date(2009, 7, 1).Date Then
            tipoSeguro = "PRIM"
            sexo = "M"
        End If



        ds = PrimasCiasSeguro.traerUltimoValor(dbc, idAdm, codAdmActual, rTrn.perCotizacion, rTrn.tipoProducto, rTrn.tipoCliente, tipoSeguro, sexo)
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        Else
            rPri = New PAR.ccAcrSeguro(ds)
        End If

        ds = Comisiones.traerUltimoValorComPorc(dbc, idAdm, rTrn.perCotizacion, rTrn.tipoProducto, "PRE1", rTrn.tipoCliente, rCli.sexo, rCli.fecNacimiento, codAdmActual, "POR", codAdmActual)
        If ds.Tables(0).Rows.Count = 0 Then
            Exit Sub
        Else
            rCom = New PAR.ccAcrComisiones(ds)
        End If

        If rCom.valMonto = 0 Then
            Exit Sub
        End If

        rPrima = New ACR.ccPrimasCiasSeg(PrimasCiasSeguro.traer(dbc, -1, Nothing, Nothing, 0, Nothing, 0, Nothing).Tables(0).NewRow)

        rPrima.porcPrimaSeguro = Mat.Redondear(rPri.valMonto / rCom.valMonto, 7)

        If rPrima.porcPrimaSeguro = 0 Then
            Exit Sub
        End If

        rPrima.valMlPrimaSeguro = Mat.Redondear(rPrima.porcPrimaSeguro * rTrn.valMlMvto)
        PrimasCiasSeguro.crear(dbc, idAdm, rTrn.tipoFondoDestinoCal, rTrn.perContable, rPri.idCompSeguro, rTrn.idPersona, rTrn.seqRegistro, "CAR", rTrn.perCotizacion, rTrn.tipoCliente, "S", rTrn.codOrigenProceso, rTrn.fecOperacion, 0, rTrn.valMlMvtoCal, rTrn.valCuoMvtoCal, rTrn.valMlComisFija, rTrn.valMlRentaImponible, rTrn.valMlMvto, 0, 0, rPrima.valMlPrimaSeguro, codAdmActual, "120506", rPrima.porcPrimaSeguro, rCom.valMonto, rTrn.idEmpleador, usu, fun, rTrn.sexo, rTrn.fecAcreditacion, rTrn.valMlPrimaSisInteres, rTrn.valMlPrimaSisReajuste, rTrn.valCuoPrimaSis, rTrn.valCuoPrimaSisInteres, rTrn.valCuoPrimaSisReajuste)

    End Sub

    'SIS// calcula el factor para obtener nuevos monto interes-reajuste(CAL) de la prima calculada
    Public Shared Sub calcularIntReaPrimaSis(ByVal valMlPrimaSis As Decimal, ByVal valMlPrimaSisCal As Decimal, ByVal valMlPrimaSisInteres As Decimal, ByVal valMlPrimaSisReajuste As Decimal, ByRef valMlPrimaSisInteresCal As Decimal, ByRef valMlPrimaSisReajusteCal As Decimal)
        Dim factor As Decimal

        If valMlPrimaSisCal = 0 Or valMlPrimaSis = 0 Then
            valMlPrimaSisInteresCal = 0
            valMlPrimaSisReajusteCal = 0
        Else
            factor = (valMlPrimaSisCal / valMlPrimaSis)

            valMlPrimaSisInteresCal = Math.Round(valMlPrimaSisInteres * factor, 0)
            valMlPrimaSisReajusteCal = Math.Round(valMlPrimaSisReajuste * factor, 0)
        End If
    End Sub

End Class
