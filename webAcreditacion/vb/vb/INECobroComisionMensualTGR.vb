Imports Sonda.Net.DB
Imports Sonda.Net
Imports Sonda.Gestion.Adm.Sys.IngresoEgreso
Imports Sonda.Gestion.Adm.Sys.Soporte

Public Class INECobroComisionMensualTGR
    Public Sub wmProcesoBatch(ByVal idAdm As Integer, ByVal seqProceso As Integer, ByVal seqEtapa As Integer, ByVal ds As DataSet, ByVal usu As String, ByVal fun As String)
        Dim dbc As OraConn
        Dim dsEtapas As DataSet
        Dim perProceso As Date = ds.Tables(0).Rows(0).Item("PER_PROCESO")
        Dim perCotizacion As Date = ds.Tables(0).Rows(0).Item("PER_COTIZACION")
        'Dim idEtapa As String = ds.Tables(0).Rows(0).Item("ID_ETAPA")
        Dim fecProceso As Date = ds.Tables(0).Rows(0).Item("FEC_PROCESO")

        Try
            dbc = New OraConn()
            If (seqEtapa Mod 2 <> 0) Then
                dbc.BeginTrans()
                CobroComisionMensualTGR.procesoExtraccionTGR(dbc, idAdm, seqProceso, seqEtapa, perProceso, perCotizacion, fecProceso, usu, fun)
                dbc.Commit()
                'se ejecuta etapa 1 
            Else
                dbc.BeginTrans()
                CobroComisionMensualTGR.generarTransacciones(dbc, idAdm, seqProceso, seqEtapa, perProceso, perCotizacion, fecProceso, usu, fun) '; procesoGeneracionLote(dbc, idAdm, seqProceso, seqEtapa, ds, usu, fun)
                dbc.Commit()
                'se ejecuta etapa 2
            End If

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

    Public Shared Function obtienePeriodoCobroTGR(ByVal perProceso As Date) As Date
        obtienePeriodoCobroTGR = perProceso.AddMonths(-17)
    End Function

    Public Shared Function traerValidacionProcAntTGR(ByVal idAdm As Integer, ByVal perProceso As Date, ByVal usu As String, ByVal fun As String) As DataSet
        Dim dbc As OraConn
        dbc = New OraConn()
        Try
            dbc.BeginTrans()
            traerValidacionProcAntTGR = CobroComisionMensualTGR.traerValidacionProcAntTGR(dbc, idAdm, perProceso, usu, fun)
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
            dbc.Rollback()
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
            dbc.Rollback()
        Finally
            dbc.Close()
        End Try
    End Function
    Public Shared Function traerPeriodoContable(ByVal idAdm As Integer) As Date
        Dim dbc As OraConn
        Dim ds As DataSet
        Try
            dbc = New OraConn()
            ds = ParametrosINE.PeriodoContable.traer(dbc, idAdm)
            traerPeriodoContable = CType(ds.Tables(0).Rows(0).Item("PER_CONTABLE"), Date)
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
            dbc.Rollback()
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
            dbc.Rollback()
        Finally
            dbc.Close()
        End Try
    End Function
    Public Shared Function generaEncabezadoTGR(ByVal idAdm As Integer, ByVal seqProceso As Integer, ByVal perProceso As Date, ByVal perCotizacion As Date, ByVal usu As String, ByVal fun As String) As DataSet
        Dim dbc As New OraConn()
        Try
            dbc.BeginTrans()
            generaEncabezadoTGR = CobroComisionMensualTGR.generaEncabezadoTGR(dbc, idAdm, seqProceso, perProceso, perCotizacion, usu, fun)
            dbc.Commit()
        Catch e As SondaException
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function
    Public Shared Function enlazaProProcesosxEncTGR(ByVal idAdm As Integer, ByVal ds As DataSet) As DataSet
        Dim dbc As New OraConn()
        Dim row As DataRow

        Dim perProceso As Date
        Dim perCotizacion As Date
        Dim numeroId As Integer
        Dim dsTGR As DataSet
        Try
            dbc.BeginTrans()
            ds.Tables(0).Columns.Add(New DataColumn("PER_PROCESO"))
            ds.Tables(0).Columns.Add(New DataColumn("PER_COTIZACION"))
            ds.Tables(0).Columns.Add(New DataColumn("NUMERO_ID"))
            ds.Tables(0).Columns.Add(New DataColumn("DESCRIPCION_LOTE"))

            For Each row In ds.Tables(0).Rows
                dsTGR = CobroComisionMensualTGR.traerEncabezadoTGR(dbc, idAdm, Int(row("SEQ_PROCESO")))
                If dsTGR.Tables(0).Rows.Count > 0 Then
                    row("PER_PROCESO") = dsTGR.Tables(0).Rows(0).Item("PER_PROCESO")
                    row("PER_COTIZACION") = dsTGR.Tables(0).Rows(0).Item("PER_COTIZACION")
                    row("NUMERO_ID") = dsTGR.Tables(0).Rows(0).Item("NUMERO_ID")
                    row("DESCRIPCION_LOTE") = dsTGR.Tables(0).Rows(0).Item("DESCRIPCION_LOTE")
                Else
                    row.Delete()
                End If
            Next
            ds.AcceptChanges()
            enlazaProProcesosxEncTGR = ds
        Catch e As SondaException
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function
    Public Shared Function buscarProcesos(ByVal idAdm As Integer, ByVal idProceso As String) As DataSet
        Dim dbc As New OraConn()
        Try
            dbc.BeginTrans()
            buscarProcesos = Sys.Soporte.Procesos.buscarProcesos(dbc, idAdm, idProceso, "ACR", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
            dbc.Commit()
        Catch e As SondaException
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try


    End Function

    Public Shared Function traerEstadoProcesoAcr(ByVal idAdm As Integer, ByVal fechaProceso As Date) As String
        Dim dbc As New OraConn()
        Try
            dbc.BeginTrans()
            traerEstadoProcesoAcr = CobroComisionMensualTGR.traerEstadoProcesoAcr(dbc, idAdm, fechaProceso)
            dbc.Commit()
        Catch e As SondaException
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function



    Public Shared Function verificaRpt(ByVal idAdm As Integer, ByVal seqProceso As Integer, ByVal estadoCom As String) As DataSet
        Dim dbc As New OraConn()
        Try
            dbc.BeginTrans()
            verificaRpt = CobroComisionMensualTGR.verificaRpt(dbc, idAdm, seqProceso, estadoCom)
            dbc.Commit()
        Catch e As SondaException
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function

    Public Shared Sub actualizaEncTgrTransaccion(ByVal idAdm As Integer, ByVal seqProceso As Integer, ByVal perProceso As Date, ByVal codOrigenProceso As String, ByVal usu As String, ByVal fun As String)
        Dim dbc As New OraConn()
        Try
            dbc.BeginTrans()
            CobroComisionMensualTGR.actualizaEncTgrTransaccion(dbc, idAdm, seqProceso, perProceso, codOrigenProceso, usu, fun)
            dbc.Commit()
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
