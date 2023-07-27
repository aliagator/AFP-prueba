Option Strict Off

Imports Sonda.Gestion.Adm.Sys.CodeCompletion

Imports Sonda.Net.DB
Imports Sonda.Net
Imports System.IO
Public Class INESaldo

    Public Class saldo
        Public numSaldo As Integer
        Public valCuoSaldo As Decimal
        Public comisionFijaCobrada As Boolean = False
        'Public rentaUfAcumulada As Decimal = 0
    End Class

    Public saldos() As saldo

    Public Sub New()
        ReDim saldos(-1)
        count = 0
    End Sub

    Public count As Integer

    Public Sub add(ByVal numSaldo As Integer, ByVal valCuoSaldo As Decimal)

        Dim s As saldo
        Dim blExiste As Boolean = False
        count += 1
        For Each s In saldos
            blExiste = (s.numSaldo = numSaldo)
            If blExiste Then Exit For
        Next
        If Not blExiste Then
            ReDim Preserve saldos(saldos.Length)
            saldos(saldos.Length - 1) = New saldo()
            saldos(saldos.Length - 1).numSaldo = numSaldo
            saldos(saldos.Length - 1).valCuoSaldo = valCuoSaldo
        End If


    End Sub

    Public Sub clear()
        ReDim saldos(-1)
        count = 0
    End Sub

    Public Sub imputar(ByVal numSaldo As Integer, ByVal valCuoMonto As Decimal, ByVal tipoImputacion As String)
        Dim s As saldo
        For Each s In saldos
            If s.numSaldo = numSaldo Then
                If tipoImputacion = "ABO" Then
                    s.valCuoSaldo += valCuoMonto
                Else
                    s.valCuoSaldo -= valCuoMonto
                End If
                Exit For
            End If
        Next

    End Sub
    'Public Sub SumarRentaUF(ByVal numSaldo As Integer, ByVal valUFMonto As Decimal, ByVal tipoImputacion As String)
    '    Dim s As saldo
    '    For Each s In saldos
    '        If s.numSaldo = numSaldo Then
    '            If tipoImputacion = "ABO" Then
    '                s.rentaUfAcumulada += valUFMonto
    '            Else
    '                s.rentaUfAcumulada -= valUFMonto
    '            End If
    '            Exit For
    '        End If
    '    Next
    'End Sub

    Public Function sobregirado() As Boolean
        Dim s As saldo
        Dim b As Boolean = False
        For Each s In saldos
            b = s.valCuoSaldo < 0
            If b Then Exit For
        Next
        sobregirado = b
    End Function

    Public Function ValorSobregiro() As Decimal
        Dim s As saldo
        For Each s In saldos
            ValorSobregiro = s.valCuoSaldo
            Exit For
        Next
    End Function

End Class
