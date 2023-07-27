Option Explicit On 
Option Strict Off

Imports Sonda.Gestion.Adm.Sys.IngresoEgreso
Imports Sonda.Gestion.Adm.Sys
Imports Sonda.Gestion.Adm.Sys.CodeCompletion
Imports Sonda.Net.DB
Imports Sonda.Net

Public Class INEControlRecaudacion
    Public Class Encabezado
        
        Public Shared Function imputar(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal folioCaja As Long, ByVal fecOperacion As Date, ByVal tipoImputacion As String, ByVal idEntRecaudadora As Integer, ByVal fecCaja As Date, ByVal valMlRecaudDocinc As Decimal, ByVal valMlRecaudDocIncSob As Decimal, ByVal valMlRecaudSindoc As Decimal, ByVal valMlDescMenorPos As Decimal, ByVal valMlDescMenorNeg As Decimal, ByVal valMlRentabDistrib As Decimal, ByVal usu As String, ByVal fun As String) As Decimal

            imputar = ControlRecaudacion.Encabezado.imputar(dbc, idAdm, folioCaja, fecOperacion, tipoImputacion, idEntRecaudadora, fecCaja, valMlRecaudDocinc, valMlRecaudDocIncSob, valMlRecaudSindoc, valMlDescMenorPos, valMlDescMenorNeg, valMlRentabDistrib, usu, fun)

        End Function
    End Class

    Public Class Detalle
        
        Public Shared Function imputar(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal folioCaja As Long, ByVal fecOperacion As Date, ByVal tipoProducto As String, ByVal tipoFondo As String, ByVal tipoImputacion As String, ByVal valMlAcrCuenta As Decimal, ByVal valMlAcrRezago As Decimal, ByVal valMlAcrTransferencia As Decimal, ByVal valMlAcrCuentaDocinc As Decimal, ByVal valMlAcrRezagoDocinc As Decimal, ByVal valMlAcrTransfDocinc As Decimal, ByVal valMlAcrCuentaDocincSob As Decimal, ByVal valMlAcrRezagoDocincSob As Decimal, ByVal valMlAcrTransfDocincSob As Decimal, ByVal valMlAcrCuentaSindoc As Decimal, ByVal valMlAcrRezagoSindoc As Decimal, ByVal valMlAcrTransfSindoc As Decimal, ByVal valMlAcrCuentaRezag As Decimal, ByVal usu As String, ByVal fun As String) As Decimal
            Dim Objcontrol As New ControlRecaudacion.Detalle()

            imputar = ControlRecaudacion.Detalle.imputar(dbc, idAdm, folioCaja, fecOperacion, tipoProducto, tipoFondo, tipoImputacion, valMlAcrCuenta, valMlAcrRezago, valMlAcrTransferencia, valMlAcrCuentaDocinc, valMlAcrRezagoDocinc, valMlAcrTransfDocinc, valMlAcrCuentaDocincSob, valMlAcrRezagoDocincSob, valMlAcrTransfDocincSob, valMlAcrCuentaSindoc, valMlAcrRezagoSindoc, valMlAcrTransfSindoc, valMlAcrCuentaRezag, usu, fun)

        End Function
    End Class
End Class
