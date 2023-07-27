Option Strict Off

Imports Sonda.Gestion.Adm.Sys.CodeCompletion.ACR

Imports Sonda.Net.DB
Imports Sonda.Net
Imports System.IO
Public Class INEMovimiento


    Public Class clsMov

        Public mov As ccSaldosMovimientos
        Public tipoMvto As Integer
        Public valMlAbonos As Decimal
        Public valCuoAbonos As Decimal
        Public valMlCargo As Decimal
        Public valCuoCargo As Decimal
        Public Sub New(ByVal dsMov As DataSet, ByVal tipoImputacion As String)
            mov = New ccSaldosMovimientos(dsMov.Tables(0).NewRow)
            mov.tipoImputacion = tipoImputacion
            valMlAbonos = 0
            valCuoAbonos = 0
            valMlCargo = 0
            valCuoCargo = 0
        End Sub

    End Class

    Public item As clsMov()
    Public count As Integer
    Public valMlSaldoInicial As Decimal
    Public valCuoSaldoInicial As Decimal
    Public valMlSaldoFinal As Decimal
    Public valCuoSaldoFinal As Decimal

    Public Sub New(ByVal SaldoInicialPesos As Decimal, ByVal SaldoInicialCuotas As Decimal)
        valMlSaldoInicial = SaldoInicialPesos
        valCuoSaldoInicial = SaldoInicialCuotas
        valMlSaldoFinal = valMlSaldoInicial
        valCuoSaldoFinal = valCuoSaldoInicial
    End Sub


    Public Sub add(ByVal dsaux As DataSet, ByVal tipoImputacion As String, ByVal tipoMvto As String)
        ReDim Preserve item(count)
        item(count) = New clsMov(dsaux, tipoImputacion)
        item(count).tipoMvto = tipoMvto
        item(count).mov.valMlSaldo = valMlSaldoFinal
        item(count).mov.valCuoSaldo = valCuoSaldoFinal
        count = count + 1
    End Sub

    Public Sub clear()
        Array.Clear(item, 0, count)
        valMlSaldoFinal = 0
        valMlSaldoInicial = 0
        valCuoSaldoFinal = 0
        valCuoSaldoInicial = 0
        count = 0

    End Sub

    Public Sub Abonar(ByVal index As Integer, ByVal valMlAbo As Decimal, ByVal valCuoAbo As Decimal)
        Dim i As Integer
        item(index).valMlAbonos += valMlAbo
        item(index).valCuoAbonos += valCuoAbo
        valMlSaldoFinal += valMlAbo
        valCuoSaldoFinal += valCuoAbo

        For i = index To count - 1

            item(i).mov.valMlSaldo += valMlAbo
            item(i).mov.valCuoSaldo += valCuoAbo

        Next

    End Sub
    Public Sub Cargar(ByVal index As Integer, ByVal valMlCar As Decimal, ByVal valCuoCar As Decimal)
        Dim i As Integer
        item(index).valMlCargo += valMlCar
        item(index).valCuoCargo += valCuoCar
        valMlSaldoFinal -= valMlCar
        valCuoSaldoFinal -= valCuoCar

        For i = index To count - 1

            item(i).mov.valMlSaldo -= valMlCar
            item(i).mov.valCuoSaldo -= valCuoCar

        Next

    End Sub

End Class


