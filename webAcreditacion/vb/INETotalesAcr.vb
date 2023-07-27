Imports Sonda.Net.DB
Imports Sonda.Gestion.Adm.Sys.IngresoEgreso
Imports Sonda.Gestion.Adm.Sys.CodeCompletion

Public Class INETotalesAcr
    Public Class Auxiliar

        Public ds As DataSet

        Public Sub New(ByRef dbc As OraConn)
            ds = Auxiliares.AbonosCargos.traer(dbc, -1, Nothing, Nothing, Nothing, Nothing, Nothing)
        End Sub

        Public Sub Add(ByVal idAdm As Integer, ByVal codAuxiliar As String, ByVal tipoProducto As String, ByVal tipoFondo As String, ByVal valMlAbonos As Decimal, ByVal valCuoAbonos As Decimal, ByVal valMlCargos As Decimal, ByVal valcuoCargos As Decimal)
            Dim dr() As DataRow
            Dim SQL As String
            If ds.Tables(0).Rows.Count = 0 Then
                ds.Tables(0).Rows.Add(New Object() {codAuxiliar, Nothing, tipoProducto, tipoFondo, Nothing, Nothing, valMlAbonos, valMlCargos, valCuoAbonos, valcuoCargos, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing})
            Else
                SQL = "COD_AUXILIAR='" & codAuxiliar & "' AND TIPO_PRODUCTO='" & tipoProducto & "' AND TIPO_FONDO='" & tipoFondo & "'"
                dr = ds.Tables(0).Select(SQL)
                If UBound(dr) < 0 Then
                    ds.Tables(0).Rows.Add(New Object() {codAuxiliar, Nothing, tipoProducto, tipoFondo, Nothing, Nothing, valMlAbonos, valMlCargos, valCuoAbonos, valcuoCargos, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing})
                Else
                    dr(0).Item("VAL_ML_ABONOS") += valMlAbonos
                    dr(0).Item("VAL_ML_CARGOS") += valMlCargos
                    dr(0).Item("VAL_CUO_ABONOS") += valCuoAbonos
                    dr(0).Item("VAL_CUO_CARGOS") += valcuoCargos
                End If
                ds.AcceptChanges()
            End If

        End Sub
        Public Sub Clear()
            ds.Clear()
        End Sub
        Public Function Count() As Integer
            Count = ds.Tables(0).Rows.Count
        End Function
        Public Sub Insert(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal fecCierre As Date, ByVal codOrigenProceso As String, ByVal perContable As Date, ByVal usu As String, ByVal fun As String)
            Dim rAux As ACR.ccAuxiliarAbocar
            Dim i As Integer
            For i = 0 To Count() - 1
                rAux = New ACR.ccAuxiliarAbocar(ds.Tables(0).Rows(i))

                If rAux.valMlAbonos + rAux.valCuoAbonos + rAux.valMlCargos + rAux.valCuoCargos > 0 Then
                    Auxiliares.AbonosCargos.sumar(dbc, idAdm, rAux.codAuxiliar, fecCierre, rAux.tipoProducto, rAux.tipoFondo, codOrigenProceso, perContable, rAux.valMlAbonos, rAux.valMlCargos, rAux.valCuoAbonos, rAux.valCuoCargos, usu, fun)
                End If

                If rAux.valMlAbonos + rAux.valCuoAbonos > 0 Then
                    Auxiliares.Saldos.imputar(dbc, idAdm, rAux.codAuxiliar, fecCierre, rAux.tipoProducto, rAux.tipoFondo, perContable, rAux.valMlAbonos, rAux.valCuoAbonos, "ABO", usu, fun)
                End If

                If rAux.valMlCargos + rAux.valCuoCargos > 0 Then
                    Auxiliares.Saldos.imputar(dbc, idAdm, rAux.codAuxiliar, fecCierre, rAux.tipoProducto, rAux.tipoFondo, perContable, rAux.valMlCargos, rAux.valCuoCargos, "CAR", usu, fun)
                End If

            Next

        End Sub
    End Class
    Public Class Contabilidad
        Public ds As New DataSet()
        Public Sub New(ByRef dbc As OraConn)
            Dim T As New DataTable()
            ds.Tables.Add(T)
            ds.Tables(0).Columns.Add("TIPO_VARIABLE", GetType(String))
            ds.Tables(0).Columns.Add("TIPO_MONEDA", GetType(String))
            ds.Tables(0).Columns.Add("TIPO_FONDO", GetType(String))
            ds.Tables(0).Columns.Add("TIPO_PRODUCTO", GetType(String))
            ds.Tables(0).Columns.Add("TIPO_COMISION", GetType(String))
            ds.Tables(0).Columns.Add("INST_FINANCIERA", GetType(String))
            ds.Tables(0).Columns.Add("REF_PAGO_BEN", GetType(String))
            ds.Tables(0).Columns.Add("TIPO_ANALISIS", GetType(String))
            ds.Tables(0).Columns.Add("NUM_ANALISIS", GetType(String))
            ds.Tables(0).Columns.Add("MONTO", GetType(Decimal))
            ds.Tables(0).Columns.Add("BENEFICIARIO", GetType(String))
        End Sub
        Private Sub Add(ByVal idAdm As Integer, ByVal tipoVariable As String, ByVal tipoMoneda As String, ByVal tipoFondo As String, ByVal tipoProducto As String, ByVal tipoComision As String, ByVal instFinanciera As String, ByVal refPagoBen As String, ByVal numAnalisis As String, ByVal beficiario As String, ByVal valMonto As Decimal)
            Dim dr() As DataRow
            Dim SQL As String
            Dim Moneda As String = IIf(tipoMoneda = "ML", "001", "005")

            'If valMonto <= 0 Then
            '    Exit Sub
            'End If
            If ds.Tables(0).Rows.Count = 0 Then
                ds.Tables(0).Rows.Add(New Object() {tipoVariable, Moneda, tipoFondo, tipoProducto, tipoComision, instFinanciera, refPagoBen, Nothing, numAnalisis, valMonto, beficiario})
            Else


                SQL = "TIPO_VARIABLE= '" & tipoVariable & "'"

                If Not IsNothing(tipoFondo) Then
                    SQL &= " AND TIPO_FONDO= '" & tipoFondo & "'"
                End If

                If Not IsNothing(tipoProducto) Then
                    SQL &= " AND TIPO_PRODUCTO= '" & tipoProducto & "'"
                End If

                If Not IsNothing(tipoComision) Then
                    SQL &= " AND TIPO_COMISION= '" & tipoComision & "'"
                End If

                If Not IsNothing(instFinanciera) Then
                    SQL &= " AND INST_FINANCIERA= '" & instFinanciera & "'"
                End If

                If Not IsNothing(refPagoBen) Then
                    SQL &= " AND REF_PAGO_BEN= '" & refPagoBen & "'"
                End If

                If Not IsNothing(numAnalisis) Then
                    SQL &= " AND NUM_ANALISIS= '" & numAnalisis & "'"
                End If

                If Not IsNothing(beficiario) Then
                    SQL &= " AND BENEFICIARIO= '" & beficiario & "'"
                End If


                dr = ds.Tables(0).Select(SQL)
                If UBound(dr) < 0 Then
                    ds.Tables(0).Rows.Add(New Object() {tipoVariable, Moneda, tipoFondo, tipoProducto, tipoComision, instFinanciera, refPagoBen, Nothing, numAnalisis, valMonto, beficiario})
                Else
                    dr(0).Item("MONTO") += valMonto
                End If
                ds.AcceptChanges()
            End If

        End Sub
        Public Sub Clear()
            ds.Clear()
        End Sub
        Public Function Count() As Integer
            Count = ds.Tables(0).Rows.Count
        End Function
        Public Sub ingresar(ByVal idAdm As Integer, ByVal rtrn As ACR.ccTransacciones, ByVal tipoMovAcr As String)

            Select Case rtrn.codOrigenProceso
                Case "RECAUDAC"
                    ContabRecaudac(idAdm, rtrn)

                Case "COLLECT"
                    ContabCollect(idAdm, rtrn)

                Case "RETCAVAD", "RETCCVAD", "RETCCVFO", "RETCAVFO", "RETCAIFO"
                    ContabRetiros(idAdm, rtrn, tipoMovAcr)

                Case "REREZMAS", "REREZSEL"
                    ContabRecupReza(idAdm, rtrn)

                Case "DEVEXCAF"
                    ContabDevExcesos(idAdm, rtrn)

                Case "LIQBONNO", "LIQBONEX"
                    ContabBono(idAdm, rtrn)

                Case "CAMBFOND"
                    ContabCambFondo(idAdm, rtrn)

            End Select


        End Sub

        Private Sub ContabRecaudac(ByVal idAdm As Integer, ByVal rtrn As ACR.ccTransacciones)

            Dim Monto As Decimal

            Monto = rtrn.valMlMvto + rtrn.valMlInteres + rtrn.valMlReajuste + _
                    rtrn.valMlAdicional + rtrn.valMlAdicionalReajuste + rtrn.valMlAdicionalInteres + _
                    rtrn.valMlExcesoLinea

            Add(idAdm, "REC0040", "ML", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
            Add(idAdm, "REC0050", "ML", Nothing, rtrn.tipoProducto, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
            'Add(idAdm, "REC0050", "ML", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)

            Monto = rtrn.valCuoPatrFrecCal
            Add(idAdm, "REC1050", "CUO", Nothing, rtrn.tipoProducto, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
            'Add(idAdm, "REC1050", "CUO", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
            If rtrn.codDestinoTransaccionCal = "CTA" Or rtrn.codDestinoTransaccionCal = "REZ" Then

                Monto = rtrn.valMlPatrFdesCal
                Add(idAdm, "REC0090", "ML", rtrn.tipoFondoDestinoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
                Monto = rtrn.valCuoPatrFdesCal
                Add(idAdm, "REC1090", "CUO", rtrn.tipoFondoDestinoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)

            End If
            'If rtrn.valCuoAjusteDecimalCal <> 0 Then
            '    If rtrn.valCuoAjusteDecimalCal > 0 Then
            '        Monto = 0
            '        Add(idAdm, "REC0111", "ML", rtrn.tipoFondoDestinoCal, rtrn.tipoProducto, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
            '        Monto = rtrn.valCuoAjusteDecimalCal
            '        Add(idAdm, "REC1111", "CUO", rtrn.tipoFondoDestinoCal, rtrn.tipoProducto, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
            '    Else
            '        Monto = 0
            '        Add(idAdm, "REC0112", "ML", rtrn.tipoFondoDestinoCal, rtrn.tipoProducto, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
            '        Monto = rtrn.valCuoAjusteDecimalCal * -1
            '        Add(idAdm, "REC1112", "CUO", rtrn.tipoFondoDestinoCal, rtrn.tipoProducto, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
            '    End If

            'End If

            If rtrn.codDestinoTransaccionCal = "CTA" Then

                Monto = rtrn.valMlPatrFdesCal
                Add(idAdm, "REC0110", "ML", rtrn.tipoFondoDestinoCal, rtrn.tipoProducto, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
                Monto = rtrn.valCuoPatrFdesCal
                Add(idAdm, "REC1110", "CUO", rtrn.tipoFondoDestinoCal, rtrn.tipoProducto, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)

            End If

            If rtrn.codDestinoTransaccionCal = "REZ" Then
                Monto = rtrn.valMlPatrFdesCal
                Add(idAdm, "REC0120", "ML", Nothing, rtrn.tipoProducto, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
                Monto = rtrn.valCuoPatrFdesCal
                Add(idAdm, "REC1120", "CUO", Nothing, rtrn.tipoProducto, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
            End If

            If rtrn.codDestinoTransaccionCal = "CTA" Then
                Monto = rtrn.valCuoPatrFdesCal
                Add(idAdm, "REC1100", "CUO", rtrn.tipoFondoDestinoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
            End If

            Monto = rtrn.valMlComisFijaCal
            Add(idAdm, "REC0150", "ML", rtrn.tipoFondoDestinoCal, Nothing, rtrn.tipoComisionFija, Nothing, Nothing, Nothing, Nothing, Monto)
            Monto = rtrn.valCuoComisFijaCal
            Add(idAdm, "REC1150", "CUO", rtrn.tipoFondoDestinoCal, Nothing, rtrn.tipoComisionFija, Nothing, Nothing, Nothing, Nothing, Monto)

            Monto = rtrn.valMlComisPorcentualCal
            Add(idAdm, "REC0150", "ML", rtrn.tipoFondoDestinoCal, Nothing, rtrn.tipoComisionPorcentual, Nothing, Nothing, Nothing, Nothing, Monto)
            Monto = rtrn.valCuoComisPorcentualCal
            Add(idAdm, "REC1150", "CUO", rtrn.tipoFondoDestinoCal, Nothing, rtrn.tipoComisionPorcentual, Nothing, Nothing, Nothing, Nothing, Monto)

            If rtrn.valIndPagoPrimCal <> "P" Then

                Monto = rtrn.valMlPrimaCal + rtrn.valMlIntPrimaCal + rtrn.valMlReaPrimaCal
                Add(idAdm, "REC0160", "ML", rtrn.tipoFondoDestinoCal, Nothing, rtrn.tipoComisionPorcentual, Nothing, Nothing, Nothing, Nothing, Monto)
                Monto = rtrn.valCuoPrimaCal
                Add(idAdm, "REC1160", "CUO", rtrn.tipoFondoDestinoCal, Nothing, rtrn.tipoComisionPorcentual, Nothing, Nothing, Nothing, Nothing, Monto)

            End If

        End Sub
        Private Sub ContabRetiros(ByVal idAdm As Integer, ByVal rtrn As ACR.ccTransacciones, ByVal tipoMovAcr As String)

            Dim Monto As Decimal

            If tipoMovAcr = "RET" Or tipoMovAcr = "IMP" Then

                Monto = rtrn.valMlMvto + rtrn.valMlComisPorcentualCal
                Add(idAdm, "RET0010", "ML", rtrn.tipoFondoDestinoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)

                Monto = rtrn.valCuoMvto + rtrn.valCuoComisPorcentualCal
                Add(idAdm, "RET1010", "CUO", rtrn.tipoFondoDestinoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)

                If tipoMovAcr = "RET" Then

                    Monto = rtrn.valMlMvto
                    Add(idAdm, "RET0020", "ML", rtrn.tipoFondoDestinoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)

                    Monto = rtrn.valMlComisPorcentualCal
                    Add(idAdm, "RET0040", "ML", rtrn.tipoFondoDestinoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)

                ElseIf tipoMovAcr = "IMP" Then

                    Monto = rtrn.valMlMvto
                    Add(idAdm, "RET0030", "ML", rtrn.tipoFondoDestinoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)

                End If
            End If

        End Sub
        Private Sub ContabRecupReza(ByVal idAdm As Integer, ByVal rtrn As ACR.ccTransacciones)

            Dim Monto As Decimal

            If rtrn.valCuoPatrFrecActCal + rtrn.valMlPatrFrecActCal > 0 Then
                Monto = rtrn.valMlPatrFrecActCal
                Add(idAdm, "REZ0510", "ML", Nothing, rtrn.tipoProducto, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
                Monto = rtrn.valCuoPatrFrecActCal
                Add(idAdm, "REZ1510", "CUO", Nothing, rtrn.tipoProducto, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
            End If
            If rtrn.valMlPatrFrecActCal + rtrn.valCuoPatrFrecActCal > 0 Then
                Monto = rtrn.valMlPatrFrecActCal
                Add(idAdm, "REZ0520", "ML", rtrn.tipoFondoDestinoCal, rtrn.tipoProducto, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
                Monto = rtrn.valCuoPatrFrecActCal
                Add(idAdm, "REZ1520", "CUO", rtrn.tipoFondoDestinoCal, rtrn.tipoProducto, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
            End If
            If rtrn.valMlPatrFrecActCal + rtrn.valCuoPatrFrecActCal > 0 Then
                Monto = rtrn.valMlPatrFrecActCal
                Add(idAdm, "REZ0530", "ML", rtrn.tipoFondoDestinoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
                Monto = rtrn.valCuoPatrFrecActCal
                Add(idAdm, "REZ1530", "CUO", rtrn.tipoFondoDestinoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)

                Monto = rtrn.valCuoPatrFdesCal
                Add(idAdm, "REZ1540", "CUO", rtrn.tipoFondoDestinoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
            End If
            If rtrn.valMlComisFijaCal + rtrn.valCuoComisFijaCal > 0 Then
                Monto = rtrn.valMlComisFijaCal
                Add(idAdm, "REZ0550", "ML", rtrn.tipoFondoDestinoCal, Nothing, rtrn.tipoComisionFija, Nothing, Nothing, Nothing, Nothing, Monto)
                Monto = rtrn.valCuoComisFijaCal
                Add(idAdm, "REZ1550", "CUO", rtrn.tipoFondoDestinoCal, Nothing, rtrn.tipoComisionFija, Nothing, Nothing, Nothing, Nothing, Monto)
            End If

            If rtrn.valMlComisPorcentualCal + rtrn.valCuoComisPorcentualCal > 0 Then
                Monto = rtrn.valMlComisPorcentualCal
                Add(idAdm, "REZ0550", "ML", rtrn.tipoFondoDestinoCal, Nothing, rtrn.tipoComisionPorcentual, Nothing, Nothing, Nothing, Nothing, Monto)
                Monto = rtrn.valCuoComisPorcentualCal
                Add(idAdm, "REZ1550", "CUO", rtrn.tipoFondoDestinoCal, Nothing, rtrn.tipoComisionPorcentual, Nothing, Nothing, Nothing, Nothing, Monto)

            End If
            If rtrn.valIndPagoPrimCal = "A" Or rtrn.valIndPagoPrimCal = "N" Then
                If rtrn.valMlPrimaCal + rtrn.valMlIntPrimaCal + rtrn.valMlReaPrimaCal + rtrn.valCuoPrimaCal > 0 Then
                    Monto = rtrn.valMlPrimaCal + rtrn.valMlIntPrimaCal + rtrn.valMlReaPrimaCal
                    Add(idAdm, "REZ0560", "ML", rtrn.tipoFondoDestinoCal, Nothing, rtrn.tipoComisionPorcentual, Nothing, Nothing, Nothing, "Adm. destino", Monto)
                    Monto = rtrn.valCuoPrimaCal
                    Add(idAdm, "REZ1560", "CUO", rtrn.tipoFondoDestinoCal, Nothing, rtrn.tipoComisionPorcentual, Nothing, Nothing, Nothing, Nothing, Monto)
                End If
            End If

        End Sub
        Private Sub ContabDevExcesos(ByVal idAdm As Integer, ByVal rtrn As ACR.ccTransacciones)

            Dim Monto As Decimal
            Dim beficiario As String = rtrn.nombre & " " & rtrn.apPaterno & " " & rtrn.apMaterno
            If rtrn.valMlPatrFrecCal + rtrn.valCuoPatrFrecCal > 0 Then
                Monto = rtrn.valMlPatrFrecCal
                Add(idAdm, "REZ0020", "ML", rtrn.tipoFondoDestinoCal, rtrn.tipoProducto, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
                Monto = rtrn.valCuoPatrFrecCal
                Add(idAdm, "REZ1020", "CUO", rtrn.tipoFondoDestinoCal, rtrn.tipoProducto, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
            End If
            Monto = rtrn.valMlPatrFrecCal
            Add(idAdm, "REZ0120", "ML", rtrn.tipoFondoDestinoCal, Nothing, Nothing, Nothing, Nothing, Nothing, beficiario, Monto)

        End Sub
        Private Sub ContabBono(ByVal idAdm As Integer, ByVal rtrn As ACR.ccTransacciones)

            Dim Monto As Decimal

            Monto = rtrn.valMlPatrFdesCal
            Add(idAdm, "BON0010", "ML", rtrn.tipoFondoDestinoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)

            Monto = rtrn.valCuoPatrFdesCal
            Add(idAdm, "BON1010", "CUO", rtrn.tipoFondoDestinoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)


        End Sub
        Private Sub ContabCollect(ByVal idAdm As Integer, ByVal rtrn As ACR.ccTransacciones)

            Dim Monto As Decimal

            Monto = rtrn.valMlMvtoCal + rtrn.valMlAdicionalCal
            Add(idAdm, "COL0003", "ML", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)

            If rtrn.codDestinoTransaccion = "CTA" Then
                Monto = rtrn.valMlMvtoCal + rtrn.valMlAdicionalCal
                Add(idAdm, "COL0005", "ML", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
                If rtrn.tipoProducto = "CCO" Then
                    Monto = rtrn.valMlMvtoCal + rtrn.valMlAdicionalCal
                    Add(idAdm, "COL0010", "ML", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
                    Monto = rtrn.valCuoMvtoCal + rtrn.valCuoAdicionalCal
                    Add(idAdm, "COL1010", "CUO", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
                Else
                    Monto = rtrn.valMlMvtoCal + rtrn.valMlAdicionalCal
                    Add(idAdm, "COL0011", "ML", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
                    Monto = rtrn.valCuoMvtoCal + rtrn.valCuoAdicionalCal
                    Add(idAdm, "COL1011", "CUO", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
                End If
            Else
                Monto = rtrn.valMlMvtoCal + rtrn.valMlAdicionalCal
                Add(idAdm, "COL0008", "ML", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
            End If


        End Sub
        Private Sub ContabCambFondo(ByVal idAdm As Integer, ByVal rtrn As ACR.ccTransacciones)

            Dim Monto As Decimal

            If rtrn.tipoImputacion = "CAR" Then

                Monto = rtrn.valMlMvto
                Add(idAdm, "CAM0010", "ML", rtrn.tipoFondoDestinoCal, rtrn.tipoProducto, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
                Monto = rtrn.valCuoMvto
                Add(idAdm, "CAM1010", "CUO", rtrn.tipoFondoDestinoCal, rtrn.tipoProducto, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)

                Monto = rtrn.valMlMvto
                Add(idAdm, "CAM0020", "ML", rtrn.tipoFondoDestinoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
                Monto = rtrn.valCuoMvto
                Add(idAdm, "CAM1020", "CUO", rtrn.tipoFondoDestinoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)

            Else


                Monto = rtrn.valMlMvto
                Add(idAdm, "CAM0030", "ML", rtrn.tipoFondoDestinoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)

                Monto = rtrn.valMlMvto
                Add(idAdm, "CAM0040", "ML", rtrn.tipoFondoDestinoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
                Monto = rtrn.valCuoMvto
                Add(idAdm, "CAM1040", "CUO", rtrn.tipoFondoDestinoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)

                Monto = rtrn.valMlMvto
                Add(idAdm, "CAM0050", "ML", rtrn.tipoFondoDestinoCal, rtrn.tipoProducto, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
                Monto = rtrn.valCuoMvto
                Add(idAdm, "CAM1050", "CUO", rtrn.tipoFondoDestinoCal, rtrn.tipoProducto, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)

                'If rtrn.tipoImputacion = "ABO" Then

                '    If rtrn.tipoFondoOrigen = rtrn.tipoFondoDestinoCal Then
                '        Monto = rtrn.valMlMvto
                '        Add(idAdm, "CAM0060", "ML", rtrn.tipoFondoDestinoCal, rtrn.tipoProducto, Nothing, Nothing, Nothing, Nothing, Nothing, Monto)
                '    End If

                'End If

            End If


        End Sub
    End Class

End Class
