Option Explicit On 
Option Strict Off
Imports Sonda.Gestion.Adm.Sys.CodeCompletion
Imports Sonda.Gestion.Adm.Sys.IngresoEgreso
Imports Sonda.Gestion.Adm.Sys.Kernel
Imports Sonda.Gestion.Adm.Sys
Imports Sonda.Net.DB
Imports Sonda.Net
Public Class INEComisiones
    Public Class AdmSaldoPorRetiro

        Public Shared Sub calcular(ByVal IdAdm As Integer, ByVal montoRetiroPesos As Decimal, ByVal TipoProducto As String, ByVal tipoFondo As String, ByVal tipoCliente As String, ByVal fecOperacion As Date, ByVal valCuotaFondo As Decimal, ByVal codPlan As String, ByRef montoComisionPesos As Decimal, ByRef montoComisionCuotas As Decimal, ByRef codeError As Integer)
            calcular(IdAdm, "PRO_ANT", 0, TipoProducto, tipoFondo, Nothing, 0, Nothing, 0, tipoCliente, fecOperacion, valCuotaFondo, montoRetiroPesos, codPlan, montoComisionPesos, montoComisionCuotas, codeError)
        End Sub

        Public Shared Sub calcular(ByVal IdAdm As Integer, ByVal codOrigenProceso As String, ByVal idCliente As Integer, ByVal TipoProducto As String, ByVal tipoFondo As String, ByVal categoria As String, ByVal subCategoria As Long, ByVal codRegTributario As String, ByVal numSaldo As Long, ByVal tipoCliente As String, ByVal fecOperacion As Date, ByVal valMlValorCuota As Decimal, ByVal valMlMontoTraspaso As Decimal, ByVal codPlan As String, ByRef montoComisionPesos As Decimal, ByRef montoComisionCuotas As Decimal, ByRef codeError As Integer)
            Dim dbc As OraConn
            Dim ds As DataSet

            Try

                dbc = New OraConn()
                codeError = 0 : montoComisionPesos = 0 : montoComisionCuotas = 0

                If codOrigenProceso = "PRO_ANT" Then
                    ds = Comisiones.ComisionAdmSaldo.calcularParaTraspaso(dbc, IdAdm, valMlMontoTraspaso, TipoProducto, tipoFondo, tipoCliente, fecOperacion, valMlValorCuota, codPlan)
                Else
                    'ds = Comisiones.ComisionAdmSaldo.calcularParaTraspaso(dbc, IdAdm, idCliente, TipoProducto, tipoFondo, categoria, subCategoria, codRegTributario, numSaldo, tipoCliente, fecOperacion, valCuotaFondo, codPlan)
                    ds = Comisiones.ComisionAdmSaldo.calcularParaTraspaso(dbc, IdAdm, codOrigenProceso, idCliente, TipoProducto, tipoFondo, categoria, subCategoria, codRegTributario, numSaldo, tipoCliente, fecOperacion, valMlValorCuota, valMlMontoTraspaso, codPlan)
                End If

                If ds.Tables(0).Rows.Count = 0 Then Exit Try

                montoComisionPesos = IIf(IsDBNull(ds.Tables(0).Rows(0).Item("VAL_ML_MONTO_COMISION")), 0, ds.Tables(0).Rows(0).Item("VAL_ML_MONTO_COMISION"))
                montoComisionCuotas = IIf(IsDBNull(ds.Tables(0).Rows(0).Item("VAL_CUO_MONTO_COMISION")), 0, ds.Tables(0).Rows(0).Item("VAL_CUO_MONTO_COMISION"))
                codeError = IIf(IsDBNull(ds.Tables(0).Rows(0).Item("CODE_ERROR")), 0, ds.Tables(0).Rows(0).Item("CODE_ERROR"))


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

    End Class

End Class
