Option Explicit On 
Option Strict Off

Imports Sonda.Gestion.Adm.Sys.IngresoEgreso
Imports Sonda.Gestion.Adm.Sys
Imports Sonda.Gestion.Adm.Sys.Soporte
Imports Sonda.Gestion.Adm.Sys.CodeCompletion
Imports Sonda.Net.DB
Imports Sonda.Net

Public Class INEProcesosVector

    'Public Shared Sub RealizarAbonos(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal IdCliente As Integer, ByVal fecCaja As Date, ByVal tipoProducto As String, ByVal tipoFondo As String, ByVal montoPesos As Decimal, ByVal montoCuotas As Decimal, ByVal valCuotaIni As Integer, ByVal fecValCuotaIni As Date, ByVal usu As String, ByVal fun As String, ByRef codeError As Integer)
    '    Dim Vec As Vector
    '    Dim seqMes As Integer
    '    Dim rVector As ACR.ccVector
    '    Dim ds As DataSet
    '    Dim noExiste As Boolean

    '    codeError = 0                                'PAR_ADMINISTRADORAS

    '    seqMes = INEClasificacion.ObtenerSeqMes(dbc, idAdm, fecCaja)
    '    ds = Vec.traer(dbc, idAdm, IdCliente, "AFP", tipoProducto, tipoFondo, seqMes, 0)
    '    If ds.Tables(0).Rows.Count > 0 Then
    '        noExiste = False
    '        rVector = New ACR.ccVector(ds)
    '    Else
    '        noExiste = True
    '        rVector = New ACR.ccVector(ds.Tables(0).NewRow)

    '        rVector.idCliente = IdCliente
    '        rVector.tipoProducto = tipoProducto
    '        rVector.tipoFondo = tipoFondo
    '        rVector.categoria = Nothing
    '        rVector.tipoVector = "AFP"
    '        rVector.seqMes = seqMes
    '        rVector.perCaja = fecCaja

    '        rVector.valCuoAbo = 0
    '        rVector.valCuoCar = 0

    '        rVector.valMlAbo = 0
    '        rVector.valMlCar = 0

    '        rVector.valCuoRen = 0
    '        rVector.valMlRen = 0

    '        rVector.valCuoTotal = 0
    '        rVector.valMlTotal = 0

    '        rVector.valMlValCuo = valCuotaIni
    '        rVector.fecValCuo = fecValCuotaIni

    '        rVector.indLey = 2

    '    End If

    '    With rVector
    '        .valMlAbo = .valMlAbo + montoPesos
    '        .valCuoAbo = .valCuoAbo + montoCuotas
    '        .valMlTotal = .valMlTotal + montoPesos
    '        .valCuoTotal = .valCuoTotal + montoCuotas

    '        If noExiste Then
    '            Vec.crear(dbc, idAdm, .idCliente, .tipoVector, .tipoProducto, .tipoFondo, .seqMes, 0, .categoria, .perCaja, "CRE", "ACR", .valMlAbo, .valMlCar, .valMlRen, .valMlTotal, .valCuoAbo, .valCuoCar, .valCuoRen, .valCuoTotal, .indLey, .fecValCuo, .valMlValCuo, usu, fun)
    '        Else
    '            Vec.modificar(dbc, idAdm, .idCliente, .tipoVector, .tipoProducto, .tipoFondo, .seqMes, 0, .categoria, .perCaja, .estadoVector, .indTipoOrigen, .valMlAbo, .valMlCar, .valMlRen, .valMlTotal, .valCuoAbo, .valCuoCar, .valCuoRen, .valCuoTotal, .indLey, .fecValCuo, .valMlValCuo, usu, fun)
    '        End If

    '    End With

       
    'End Sub

    'Public Shared Sub RealizarCargos(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal idCliente As Integer, ByVal tipoProducto As String, ByVal tipoFondo As String, ByVal montoPesos As Decimal, ByVal montoCuotas As Decimal, ByVal ConTraspaso As Boolean, ByVal codAdmDestino As Integer, ByVal usu As String, ByVal fun As String, ByRef pesosMenor48 As Decimal, ByRef cuotasMenor48 As Decimal, ByRef codeError As Integer)

    '    Dim seqMes As Integer
    '    Dim rVec As ACR.ccVector
    '    Dim ds As DataSet
    '    Dim i As Integer
    '    Dim largoVector As Integer
    '    Dim montoFaltanteCuo As Decimal = montoCuotas
    '    Dim montoFaltantePes As Decimal = montoPesos
    '    Dim pesosCargadosPeriodo As Decimal
    '    Dim cuotasCargadasPeriodo As Decimal
    '    Dim mlTotalCar, mlAbonosCar, mlCargosCar, mlRentaCar As Decimal
    '    Dim cuoTotalCar, cuoAbonosCar, cuoCargosCar, cuoRentaCar As Decimal
    '    Dim parAcr As New ParametrosINE()
    '    Dim dslargo As DataSet

    '    pesosMenor48 = 0 : cuotasMenor48 = 0 : codeError = 0

    '    dslargo = parAcr.LargoVector.traer(dbc, idAdm)
    '    If dslargo.Tables(0).Rows.Count <> 1 Then
    '        codeError = 15301 '"Largo del vector no ha sido determinado
    '        Exit Sub
    '    End If
    '    largoVector = dslargo.Tables(0).Rows(0).Item("LARGO_VECTOR")

    '    ds = Vector.buscarTodasLasSecuencias(dbc, idAdm, idCliente, "AFP", tipoProducto, tipoFondo, 0)
    '    If ds.Tables(0).Rows.Count < 1 Then
    '        codeError = 15300 'El vector no ha sido encontrado")
    '        Exit Sub
    '    End If

    '    i = ds.Tables(0).Rows.Count - 1
    '    While i >= 0 And montoFaltantePes > 0

    '        pesosCargadosPeriodo = 0

    '        rVec = New ACR.ccVector(ds.Tables(0).Rows(i))

    '        With rVec

    '            If .valMlTotal <= montoFaltantePes Then ' le sacamos todo lo del periodo

    '                mlTotalCar = .valMlTotal
    '                mlAbonosCar = .valMlAbo
    '                mlCargosCar = .valMlCar
    '                mlRentaCar = .valMlRen

    '                cuoTotalCar = .valCuoTotal
    '                cuoAbonosCar = .valCuoAbo
    '                cuoCargosCar = .valCuoCar
    '                cuoRentaCar = .valCuoRen

    '                .valMlCar += .valMlAbo + .valMlRen
    '                .valCuoCar += .valCuoAbo + .valCuoRen
    '                .valMlTotal = 0
    '                .valCuoTotal = 0

    '            Else 'le sacamos lo proporcional al periodo
    '                Dim factor As Decimal = montoFaltantePes / .valMlTotal

    '                mlTotalCar = montoFaltantePes
    '                mlAbonosCar = Mat.Redondear(.valMlAbo * factor)
    '                mlCargosCar = Mat.Redondear(.valMlCar * factor)
    '                mlRentaCar = Mat.Redondear(.valMlRen * factor)

    '                cuoTotalCar = montoFaltanteCuo
    '                cuoAbonosCar = Mat.Redondear(.valCuoAbo * factor)
    '                cuoCargosCar = Mat.Redondear(.valCuoCar * factor)
    '                cuoRentaCar = Mat.Redondear(.valCuoRen * factor)

    '                .valMlTotal -= mlTotalCar
    '                .valMlAbo -= mlAbonosCar
    '                .valMlCar += mlCargosCar
    '                .valMlRen -= mlRentaCar

    '                .valCuoTotal -= cuoTotalCar
    '                .valCuoAbo -= cuoAbonosCar
    '                .valCuoCar += cuoCargosCar
    '                .valCuoRen -= cuoRentaCar

    '            End If

    '            montoFaltantePes = montoFaltantePes - mlTotalCar
    '            montoFaltanteCuo = montoFaltanteCuo - cuoTotalCar
    '            .valMlCar = .valMlCar + mlTotalCar
    '            .valMlCar = .valCuoCar - cuoTotalCar

    '            Vector.modificar(dbc, idAdm, .idCliente, .tipoVector, .tipoProducto, .tipoFondo, .seqMes, 0, .categoria, .perCaja, .estadoVector, .indTipoOrigen, .valMlAbo, .valMlCar, .valMlRen, .valMlTotal, .valCuoAbo, .valCuoCar, .valCuoRen, .valCuoTotal, .indLey, .fecValCuo, .valMlValCuo, usu, fun)
    '            If ConTraspaso Then
    '                'si es con traspaso creamos el vector para traspaso 
    '                'con los valores cargados para el periodo
    '                Vector.sumar(dbc, idAdm, .idCliente, "TRA", .tipoProducto, .tipoFondo, .seqMes, codAdmDestino, .categoria, .perCaja, .estadoVector, .indTipoOrigen, mlAbonosCar, mlCargosCar, mlRentaCar, mlTotalCar, cuoAbonosCar, cuoCargosCar, cuoRentaCar, cuoTotalCar, .indLey, .fecValCuo, .valMlValCuo, usu, fun)
    '            End If

    '            If .seqMes < largoVector Then
    '                pesosMenor48 = pesosMenor48 + pesosCargadosPeriodo
    '                'cuotasMenor48 = cuotasMenor48 + cuotasCargadasPeriodo
    '            End If

    '        End With
    '        i = i - 1
    '    End While

    '    'If montoFaltanteCuo <> 0 Or montoFaltantePes <> 0 Then
    '    '    codeError = 15302 'Se ha detectado diferencias al agotar un periódo del vector")
    '    '    Exit Sub
    '    'End If

    'End Sub

    'Public Shared Sub CrearVectorTraspIngreso(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal idCliente As Integer, ByVal tipoProducto As String, ByVal codAdmOrigen As Integer, ByVal fondo1 As String, ByVal valMlMonto1 As Decimal, ByVal fondo2 As String, ByVal valMlMonto2 As Decimal, ByVal USU As String, ByVal fun As String, ByRef coderror As Integer)
    '    Dim dsVecAfp, dsVecIng, Ds As DataSet
    '    Dim ccprod As AAA.ccProductos
    '    Dim ccVecIng As ACR.ccVector
    '    Dim ccVecAfp As ACR.ccVector
    '    Dim noExiste As Boolean
    '    Dim i As Integer
    '    Dim D1, D2, p1, p2 As Decimal
    '    Dim ajuste1, ajuste2 As Decimal
    '    Dim aboFondo1, renFondo1, totFondo1, aboFondo2, renFondo2, totFondo2 As Decimal
    '    Dim ValCuo1, ValCuo2 As Decimal
    '    Dim fecValorCuota As Date


    '    coderror = 15293
    '    aboFondo1 = renFondo1 = totFondo1 = aboFondo2 = renFondo2 = totFondo2 = 0

    '    p1 = Mat.Redondear((100 * valMlMonto1) / (valMlMonto1 + IIf(IsNothing(fondo2), 0, valMlMonto2)), 2)
    '    p2 = 100 - p1

    '    fecValorCuota = Sys.Kernel.Parametros.FechaAcreditacion.obtenerFechaValorCuota(dbc, idAdm, "ACR")

    '    Ds = Parametro.obtenerValorCuotasFondos(dbc, idAdm, fecValorCuota, fondo1, 0, 1)
    '    If Ds.Tables(0).Rows.Count = 0 Then
    '        coderror = 15316 '"No existen todos los valores cuotas
    '        Exit Sub
    '    End If
    '    ValCuo1 = Ds.Tables(0).Rows(0).Item("VAL_CUOTA")

    '    If Not IsNothing(fondo2) Then
    '        Ds = Parametro.obtenerValorCuotasFondos(dbc, idAdm, fecValorCuota, fondo2, 0, 1)
    '        If Ds.Tables(0).Rows.Count = 0 Then
    '            coderror = 15316 '"No existen todos los valores cuotas
    '            Exit Sub
    '        End If
    '        ValCuo2 = Ds.Tables(0).Rows(0).Item("VAL_CUOTA")
    '    End If

    '    'Buscamos todas las secuencias del vector ING
    '    dsVecIng = Vector.buscarTodasLasSecuencias(dbc, idAdm, idCliente, "ING", tipoProducto, "C", codAdmOrigen)

    '    For i = 0 To dsVecIng.Tables(0).Rows.Count - 1

    '        ccVecIng = New ACR.ccVector(dsVecIng.Tables(0).Rows(i))

    '        If ccVecIng.estadoVector = "ING" Then

    '            ccVecIng.fecValCuo = fecValorCuota

    '            Ds = Vector.traer(dbc, idAdm, ccVecIng.idCliente, ccVecIng.tipoVector, ccVecIng.tipoProducto, ccVecIng.tipoFondo, ccVecIng.seqMes, 0)

    '            'Vector ingreso pasa a historico                  
    '            Vector.modificar(dbc, idAdm, ccVecIng.idCliente, ccVecIng.tipoVector, ccVecIng.tipoProducto, ccVecIng.tipoFondo, ccVecIng.seqMes, ccVecIng.codAdmOrigen, ccVecIng.categoria, ccVecIng.perCaja, "INH", ccVecIng.indTipoOrigen, ccVecIng.valMlAbo, ccVecIng.valMlCar, ccVecIng.valMlRen, ccVecIng.valMlTotal, ccVecIng.valCuoAbo, ccVecIng.valCuoCar, ccVecIng.valCuoRen, ccVecIng.valCuoTotal, ccVecIng.indLey, ccVecIng.fecValCuo, ccVecIng.valMlValCuo, USU, fun)

    '            If Not IsNothing(fondo2) Then ' hay dos fondos

    '                'buscamos el fondo2 como vector "AFP"
    '                dsVecAfp = Vector.traer(dbc, idAdm, idCliente, "AFP", tipoProducto, fondo2, ccVecIng.seqMes, 0)
    '                If dsVecAfp.Tables(0).Rows.Count = 0 Then
    '                    ccVecAfp = New ACR.ccVector(dsVecAfp.Tables(0).NewRow)
    '                    noExiste = True
    '                Else
    '                    ccVecAfp = New ACR.ccVector(dsVecAfp)
    '                    noExiste = False
    '                End If

    '                Distribucion(ccVecIng.valMlAbo, D1, D2, p1, 0)
    '                ccVecIng.valMlAbo = D1
    '                ccVecAfp.valMlAbo = D2
    '                aboFondo1 = aboFondo1 + D1
    '                aboFondo2 = aboFondo2 + D2

    '                Distribucion(ccVecIng.valMlRen, D1, D2, p1, 0)
    '                ccVecIng.valMlRen = D1
    '                ccVecAfp.valMlRen = D2
    '                renFondo1 = renFondo1 + D1
    '                renFondo2 = renFondo2 + D2

    '                ccVecIng.valMlTotal = ccVecIng.valMlAbo + ccVecIng.valMlRen
    '                ccVecAfp.valMlTotal = ccVecAfp.valMlAbo + ccVecAfp.valMlRen

    '                If i = dsVecIng.Tables(0).Rows.Count - 1 Then
    '                    ajuste1 = valMlMonto1 - aboFondo1 - renFondo1
    '                    ajuste2 = valMlMonto2 - aboFondo2 - renFondo2
    '                    ccVecIng.valMlTotal = ccVecIng.valMlTotal + ajuste1
    '                    ccVecAfp.valMlTotal = ccVecAfp.valMlTotal + ajuste2
    '                    ccVecIng.valMlAbo = ccVecIng.valMlAbo + ajuste1
    '                    ccVecAfp.valMlAbo = ccVecAfp.valMlAbo + ajuste2
    '                End If

    '                ccVecAfp.tipoVector = "AFP"
    '                ccVecAfp.tipoFondo = fondo2
    '                ccVecAfp.fecValCuo = fecValorCuota
    '                ccVecAfp.valMlValCuo = ValCuo2
    '                valoriza(ccVecAfp)
    '                If noExiste Then 'se crea el fondo2 como AFP
    '                    Vector.crear(dbc, idAdm, ccVecIng.idCliente, ccVecAfp.tipoVector, ccVecIng.tipoProducto, ccVecAfp.tipoFondo, ccVecIng.seqMes, 0, ccVecIng.categoria, ccVecIng.perCaja, "CRE", ccVecIng.indTipoOrigen, ccVecAfp.valMlAbo, ccVecAfp.valMlCar, ccVecAfp.valMlRen, ccVecAfp.valMlTotal, ccVecAfp.valCuoAbo, ccVecAfp.valCuoCar, ccVecAfp.valCuoRen, ccVecAfp.valCuoTotal, ccVecIng.indLey, ccVecAfp.fecValCuo, ccVecAfp.valMlValCuo, USU, fun)
    '                Else
    '                    Vector.modificar(dbc, idAdm, ccVecIng.idCliente, ccVecAfp.tipoVector, ccVecIng.tipoProducto, ccVecAfp.tipoFondo, ccVecIng.seqMes, 0, ccVecAfp.categoria, ccVecIng.perCaja, ccVecAfp.estadoVector, ccVecAfp.indTipoOrigen, ccVecAfp.valMlAbo, ccVecAfp.valMlCar, ccVecAfp.valMlRen, ccVecAfp.valMlTotal, ccVecAfp.valCuoAbo, ccVecAfp.valCuoCar, ccVecAfp.valCuoRen, ccVecAfp.valCuoTotal, ccVecIng.indLey, ccVecAfp.fecValCuo, ccVecAfp.valMlValCuo, USU, fun)
    '                End If
    '            End If
    '            ccVecIng.tipoVector = "AFP"
    '            ccVecIng.tipoFondo = fondo1
    '            ccVecIng.fecValCuo = fecValorCuota
    '            ccVecIng.valMlValCuo = ValCuo1
    '            valoriza(ccVecIng)

    '            dsVecAfp = Vector.traer(dbc, idAdm, idCliente, "AFP", tipoProducto, fondo1, ccVecIng.seqMes, 0)
    '            If dsVecAfp.Tables(0).Rows.Count = 0 Then
    '                Vector.crear(dbc, idAdm, ccVecIng.idCliente, ccVecIng.tipoVector, ccVecIng.tipoProducto, ccVecIng.tipoFondo, ccVecIng.seqMes, codAdmOrigen, ccVecIng.categoria, ccVecIng.perCaja, "CRE", ccVecIng.indTipoOrigen, ccVecIng.valMlAbo, ccVecIng.valMlCar, ccVecIng.valMlRen, ccVecIng.valMlTotal, ccVecIng.valCuoAbo, ccVecIng.valCuoCar, ccVecIng.valCuoRen, ccVecIng.valCuoTotal, ccVecIng.indLey, ccVecIng.fecValCuo, ccVecIng.valMlValCuo, USU, fun)
    '            Else
    '                ccVecAfp = New ACR.ccVector(dsVecAfp)
    '                Vector.modificar(dbc, idAdm, ccVecAfp.idCliente, ccVecAfp.tipoVector, ccVecAfp.tipoProducto, ccVecAfp.tipoFondo, ccVecAfp.seqMes, ccVecAfp.codAdmOrigen, ccVecAfp.categoria, ccVecIng.perCaja, ccVecAfp.estadoVector, ccVecAfp.indTipoOrigen, ccVecIng.valMlAbo, ccVecIng.valMlCar, ccVecIng.valMlRen, ccVecIng.valMlTotal, ccVecIng.valCuoAbo, ccVecIng.valCuoCar, ccVecIng.valCuoRen, ccVecIng.valCuoTotal, ccVecIng.indLey, ccVecIng.fecValCuo, ccVecIng.valMlValCuo, USU, fun)
    '            End If
    '        End If
    '    Next
    '    coderror = 0

    'End Sub

    'Public Shared Sub CambiarDeFondo(ByVal dbc As OraConn, ByVal idAdm As Integer, ByVal idCliente As Integer, ByVal tipoProducto As String, ByVal tipoFondoOrigen As String, ByVal tipoFondoDestino As String, ByVal porcentaje As Decimal, ByVal valCuoDestino As Decimal, ByVal fecValCuoDestino As Date, ByVal USU As String, ByVal fun As String, ByRef codError As Integer)

    '    Dim dsVec As DataSet
    '    Dim ds As DataSet
    '    Dim ccVecOri As ACR.ccVector
    '    Dim ccVecDes As ACR.ccVector
    '    Dim i As Integer
    '    Dim D1, D2, valorCuota As Decimal



    '    'traemos todas las secuencias del vector
    '    dsVec = Vector.buscarTodasLasSecuencias(dbc, idAdm, idCliente, "AFP", tipoProducto, tipoFondoOrigen, 0)

    '    If dsVec.Tables(0).Rows.Count = 0 Then
    '        codError = 15300 ' no se ha encontrado el Vector
    '        Exit Sub
    '    End If

    '    ccVecOri = New ACR.ccVector(dsVec.Tables(0).Rows(0))
    '    'ds = Parametro.obtenerValorCuotasFondos(dbc, idAdm, ccVecOri.fecValCuo, tipoFondoDestino, 0, 1)
    '    'If ds.Tables(0).Rows.Count = 0 Then
    '    '    codError = 15316 'No existe el valor cuota
    '    '    Exit Sub
    '    'End If

    '    valorCuota = valCuoDestino

    '    For i = 0 To dsVec.Tables(0).Rows.Count - 1

    '        ccVecOri = New ACR.ccVector(dsVec.Tables(0).Rows(i))
    '        ccVecDes = New ACR.ccVector(dsVec.Tables(0).Rows(i))

    '        Distribucion(ccVecOri.valCuoAbo, D1, D2, porcentaje, 2)
    '        ccVecOri.valCuoAbo = D1

    '        Distribucion(ccVecOri.valCuoCar, D1, D2, porcentaje, 2)
    '        ccVecOri.valCuoCar = D1

    '        Distribucion(ccVecOri.valCuoRen, D1, D2, porcentaje, 2)
    '        ccVecOri.valCuoRen = D1

    '        Distribucion(ccVecOri.valCuoTotal, D1, D2, porcentaje, 2)
    '        ccVecOri.valCuoTotal = D1

    '        Distribucion(ccVecOri.valMlAbo, D1, D2, porcentaje, 2)
    '        ccVecOri.valMlAbo = D1 : ccVecDes.valMlAbo = D2

    '        Distribucion(ccVecOri.valMlCar, D1, D2, porcentaje, 2)
    '        ccVecOri.valMlCar = D1 : ccVecDes.valMlCar = D2

    '        Distribucion(ccVecOri.valMlRen, D1, D2, porcentaje, 2)
    '        ccVecOri.valMlRen = D1 : ccVecDes.valMlRen = D2

    '        Distribucion(ccVecOri.valMlTotal, D1, D2, porcentaje, 2)
    '        ccVecOri.valMlTotal = D1 : ccVecDes.valMlTotal = D2

    '        ccVecDes.tipoFondo = tipoFondoDestino
    '        ccVecDes.fecValCuo = fecValCuoDestino
    '        ccVecDes.valMlValCuo = valorCuota
    '        ccVecDes.categoria = Nothing

    '        valoriza(ccVecDes)

    '        'rebajamos el vector origen    
    '        If porcentaje = 100 Then
    '            Vector.eliminar(dbc, idAdm, ccVecOri.idCliente, ccVecOri.tipoVector, ccVecOri.tipoProducto, ccVecOri.tipoFondo, ccVecOri.seqMes, ccVecOri.codAdmOrigen)
    '        Else
    '            Vector.modificar(dbc, idAdm, ccVecOri.idCliente, ccVecOri.tipoVector, ccVecOri.tipoProducto, ccVecOri.tipoFondo, ccVecOri.seqMes, ccVecOri.codAdmOrigen, ccVecOri.categoria, ccVecOri.perCaja, ccVecOri.estadoVector, ccVecOri.indTipoOrigen, ccVecOri.valMlAbo, ccVecOri.valMlCar, ccVecOri.valMlRen, ccVecOri.valMlTotal, ccVecOri.valCuoAbo, ccVecOri.valCuoCar, ccVecOri.valCuoRen, ccVecOri.valCuoTotal, ccVecOri.indLey, ccVecOri.fecValCuo, ccVecOri.valMlValCuo, USU, fun)
    '        End If

    '        'creamos o modificamos el vector destino
    '        ds = Vector.traer(dbc, idAdm, ccVecDes.idCliente, ccVecDes.tipoVector, ccVecDes.tipoProducto, ccVecDes.tipoFondo, ccVecDes.seqMes, ccVecDes.codAdmOrigen)

    '        If ds.Tables(0).Rows.Count = 0 Then
    '            Vector.crear(dbc, idAdm, ccVecDes.idCliente, ccVecDes.tipoVector, ccVecDes.tipoProducto, ccVecDes.tipoFondo, ccVecDes.seqMes, ccVecDes.codAdmOrigen, ccVecDes.categoria, ccVecDes.perCaja, ccVecDes.estadoVector, ccVecDes.indTipoOrigen, ccVecDes.valMlAbo, ccVecDes.valMlCar, ccVecDes.valMlRen, ccVecDes.valMlTotal, ccVecDes.valCuoAbo, ccVecDes.valCuoCar, ccVecDes.valCuoRen, ccVecDes.valCuoTotal, ccVecDes.indLey, ccVecDes.fecValCuo, ccVecDes.valMlValCuo, USU, fun)
    '        Else
    '            Vector.modificar(dbc, idAdm, ccVecDes.idCliente, ccVecDes.tipoVector, ccVecDes.tipoProducto, ccVecDes.tipoFondo, ccVecDes.seqMes, ccVecDes.codAdmOrigen, ccVecDes.categoria, ccVecDes.perCaja, ccVecDes.estadoVector, ccVecDes.indTipoOrigen, ccVecDes.valMlAbo, ccVecDes.valMlCar, ccVecDes.valMlRen, ccVecDes.valMlTotal, ccVecDes.valCuoAbo, ccVecDes.valCuoCar, ccVecDes.valCuoRen, ccVecDes.valCuoTotal, ccVecDes.indLey, ccVecDes.fecValCuo, ccVecDes.valMlValCuo, USU, fun)
    '        End If
    '    Next
    '    codError = 0


    'End Sub

    'Private Shared Sub Distribucion(ByVal Monto As Decimal, ByRef D1 As Decimal, ByRef D2 As Decimal, ByVal P1 As Decimal, ByVal nDec As Integer)

    '    D1 = Mat.Redondear(P1 * Monto / 100, nDec)
    '    D2 = Monto - D1

    'End Sub
    'Private Shared Sub valoriza(ByRef rVec As ACR.ccVector)
    '    rVec.valCuoAbo = Mat.Redondear(rVec.valMlAbo / rVec.valMlValCuo, 2)
    '    rVec.valCuoRen = Mat.Redondear(rVec.valMlRen / rVec.valMlValCuo, 2)
    '    rVec.valCuoTotal = Mat.Redondear(rVec.valMlTotal / rVec.valMlValCuo, 2)
    'End Sub
End Class
