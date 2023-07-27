Option Explicit On 
Option Strict Off
Imports Sonda.Gestion.Adm.Sys.CodeCompletion
Imports Sonda.Gestion.Adm.Sys.IngresoEgreso
Imports Sonda.Gestion.Adm.Sys.Kernel
Imports Sonda.Gestion.Adm.Sys

Imports Sonda.Net.DB
Imports Sonda.Net


Public Class INEClasificacion

    'Public Shared Function ObtenerFondoRecaudador(ByRef dbc As OraConn, ByVal IdAdm As Integer, ByVal IdCliente As Integer, ByVal TipoProducto As String) As String
    '    Dim ds As DataSet
    '    Dim fondo As String
    '    ds = Kernel.TipoProducto.TRtraerTipoProductoPorcliente(dbc, IdAdm, IdCliente, TipoProducto)
    '    If ds.Tables(0).Rows.Count = 1 Then
    '        fondo = ds.Tables(0).Rows(0).Item("TIPO_FONDO_RECAUDACION")
    '    Else
    '        Throw New SondaException(15304)   'No se puede obtener el Fondo Recaudador
    '    End If
    '    ObtenerFondoRecaudador = fondo

    'End Function

    'Public Shared Function ObtenerTopePeriodo(ByVal IdAdm As Integer, ByVal FechaCaja As Date, ByRef FechaTope As Date) As String
    '    Dim dbc As OraConn
    '    Try
    '        dbc = New OraConn()
    '        FechaTope = Vector.obtenerTopePeriodo(dbc, IdAdm, FechaCaja)
    '    Catch e As SondaException
    '        dbc.Rollback()
    '        Dim sm As New SondaExceptionManager(e)
    '    Catch e As Exception
    '        dbc.Rollback()
    '        Dim sm As New SondaExceptionManager(e)
    '    Finally
    '        dbc.Close()
    '    End Try
    'End Function


    Public Shared Function ObtenerSeqMes(ByRef dbc As OraConn, ByVal idadm As Integer, ByVal FechaCaja As Date) As Integer
        Dim ds As DataSet
        Dim hoy As Date
        Dim numMeses, largoVector As Integer

        hoy = Sys.Soporte.Fecha.ahora(dbc) ' PARAMETRO ULTIMO PERIODO DEL VECTOR
        numMeses = DateDiff(DateInterval.Month, FechaCaja, hoy) + 1

        ds = ParametrosINE.LargoVector.traer(dbc, idadm)
        If ds.Tables(0).Rows.Count <> 1 Then
            Throw New SondaException(15301)  '"Largo del vector no ha sido determinado
        Else
            largoVector = ds.Tables(0).Rows(0).Item("LARGO_VECTOR")
        End If

        If numMeses > largoVector Then
            numMeses = largoVector
        End If

        ObtenerSeqMes = numMeses

    End Function


End Class
