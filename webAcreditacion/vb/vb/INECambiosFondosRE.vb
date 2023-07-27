Imports System.Web.Services
Imports Sonda.Net.DB
Imports Sonda.Gestion.Adm.Sys.CodeCompletion
Imports Sonda.Gestion.Adm.Sys.CodeCompletion.ACR
Imports Sonda.Gestion.Adm.Sys.CodeCompletion.AAA
Imports Sonda.Gestion.Adm.Sys
Imports Sonda.Gestion.Adm.Sys.Soporte
Imports Sonda.Gestion.Adm.Sys.IngresoEgreso.CambiosFondosRE
Imports Sonda.Net
Imports Sonda.Net.Reports
Imports System.io


Public Class INECambiosFondosRE

    Public Shared Function wmBuscarProcesosCambioFondo(ByVal idAdm As Integer, ByVal idProceso As String) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()
            wmBuscarProcesosCambioFondo = buscarProcesosCambioFondo(idAdm, idProceso)
            dbc.Commit()
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function

    Public Shared Function wmBuscarProcesosCambioFondo(ByVal idAdm As Integer) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()
            wmBuscarProcesosCambioFondo = buscarProcesosCambioFondo(idAdm)
            dbc.Commit()
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function


    Public Function GeneraExcelAuditor(ByVal Afp As String, ByVal Id_adm As Integer, ByVal fecProceso As Date, ByVal seqProceso As Integer) As String

        Dim dbc As OraConn
        Dim dsDatos1 As New DataSet()
        Dim dsDatos2 As New DataSet()
        Dim rutaArchivo As String
        Dim nombreArchivo As String
        Dim dataSetResultado As New DataSet()
        Dim nombreRuta As String = "err"
        Try
            dbc = New OraConn()
            dbc.BeginTrans()
            Dim dsFile As New DataSet()

            dsFile = Sys.Soporte.Archivo.traer(dbc, Id_adm, "AUDIETARIO")

            If dsFile.Tables(0).Rows.Count > 0 Then
                rutaArchivo = dsFile.Tables(0).Rows(0).Item("RUTA")
                'cambia la ruta del server a la local
                'rutaArchivo = "C:\ARCHIVOS\"
            Else
                GeneraExcelAuditor = "err"
            End If

            'Nombre Archivo
            nombreArchivo = "INFAUDITORETARIO_" & fecProceso.Month.ToString() & fecProceso.Year.ToString() & ".xls"

            'obtener los datos del reporte
            dsDatos1 = obtenerEtariosAuditados(dbc, Id_adm, fecProceso, seqProceso)

            If dsDatos1.Tables(0).Rows.Count > 0 Then
                'crearArchivo con la respuesta
                Dim titulo1() As String = {"RUT", _
                                            "CLASE COTIZANTE", _
                                            "SEXO", _
                                            "EDAD", _
                                            "TIPO PRODUCTO", _
                                            "FONDO ORIGEN", _
                                            "% ORIGEN", _
                                            "ACCION REGULARIZADORA", _
                                            "FONDO DESTINO", _
                                            "% DESTINO", _
                                            "OBSERVACIONES", _
                                            "FECHA PROCESO"}

                crearXls(Id_adm, titulo1, dsDatos1, rutaArchivo, nombreArchivo, "Informe Auditor Etario")

                nombreRuta = rutaArchivo & nombreArchivo
            Else
                GeneraExcelAuditor = "err"
            End If
            Return nombreRuta
        Catch e As SondaException
            If Not IsNothing(dbc) Then dbc.Rollback()
            FileClose(1)
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            If Not IsNothing(dbc) Then dbc.Rollback()
            FileClose(1)
            Dim sm As New SondaExceptionManager(e)
        Finally
            If Not IsNothing(dbc) Then dbc.Close()
        End Try
    End Function


    Public Function GeneraExcelProceso(ByVal Afp As String, ByVal Usuario As String, ByVal Id_adm As Integer, ByVal Seq_proceso As Integer, ByVal Seq_etapa As Integer) As String

        Dim dbc As OraConn
        Dim log As New Sys.Soporte.Procesos.logEtapa(Id_adm, Seq_proceso, Seq_etapa)
        Dim dsDatos1 As New DataSet()
        Dim dsDatos2 As New DataSet()
        Dim rutaArchivo As String
        Dim nombreArchivoInc As String
        Dim nombreArchivoExc As String
        Dim dataSetResultado As New DataSet()
        Dim nombreRuta As String = "err"
        Try
            dbc = New OraConn()
            dbc.BeginTrans()
            Dim dsFileInc As New DataSet()
            Dim dsFileExc As New DataSet()

            dsFileInc = Sys.Soporte.Archivo.traer(dbc, Id_adm, "INCLETARIO")
            dsFileExc = Sys.Soporte.Archivo.traer(dbc, Id_adm, "EXCLETARIO")

            If dsFileInc.Tables(0).Rows.Count > 0 And dsFileExc.Tables(0).Rows.Count > 0 Then
                rutaArchivo = dsFileInc.Tables(0).Rows(0).Item("RUTA")
                'cambia la ruta del server a la local
                'rutaArchivo = "C:\ARCHIVOS\"
            Else
                Throw New Exception("No existe el directorio para el archivo 'APAROLRUT'")
            End If

            'Nombre Archivo
            nombreArchivoInc = "INFINCLUIDOSETARIO_" & dateToString(Now.Date) & ".xls"
            nombreArchivoExc = "INFEXCLUIDOSETARIO_" & dateToString(Now.Date) & ".xls"

            'obtener los datos del reporte
            dsDatos1 = obtenerEtariosExcluidos(dbc, Id_adm, Seq_proceso)
            dsDatos2 = obtenerEtariosIncluidos(dbc, Id_adm, Seq_proceso)

            If dsDatos1.Tables(0).Rows.Count > 0 Or dsDatos2.Tables(0).Rows.Count > 0 Then
                'crearArchivo con la respuesta
                Dim titulo1() As String = {"RUT", _
                                            "CLASE COTIZANTE", _
                                            "SEXO", _
                                            "EDAD", _
                                            "TIPO CAUSAL", _
                                            "FECHA PROCESO"}

                crearXls(Id_adm, titulo1, dsDatos1, rutaArchivo, nombreArchivoExc, "Informe Excluidos Etario")

                Dim titulo2() As String = {"RUT CLIENTE", _
                                            "N° SOLICITUD", _
                                            "CLASE COTIZANTE", _
                                            "SEXO", _
                                            "EDAD", _
                                            "FECHA DE CAMBIO", _
                                            "TIPO PRODUCTO", _
                                            "FONDO ORIGEN", _
                                            "PORCENT. NOMINAL", _
                                            "PORCENT. REAL", _
                                            "NUMERO DE CAMBIO", _
                                            "FONDO DESTINO", _
                                            "FECHA PROCESO"}
                crearXls(Id_adm, titulo2, dsDatos2, rutaArchivo, nombreArchivoInc, "Informe Incluidos Etario")

                nombreRuta = rutaArchivo & nombreArchivoExc & " y " & nombreArchivoInc
            Else
                Throw New Exception("No fue posible crear los archivos .xls, por favor verifique los parametros")
            End If
            Return nombreRuta
        Catch e As SondaException
            If Not IsNothing(dbc) Then dbc.Rollback()
            FileClose(1)
            log.codError = e.codigo
            log.descripcionError = e.message
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            If Not IsNothing(dbc) Then dbc.Rollback()
            FileClose(1)
            log.descripcionError = e.message
            Dim sm As New SondaExceptionManager(e)
        Finally
            If Not IsNothing(dbc) Then dbc.Close()
            log.fecHoraFin = Now
            log.Save()
        End Try

    End Function

    Private Function dateToString(ByVal fecha As Date) As String
        Return fecha.Day.ToString.PadLeft(2, "0") & fecha.Month.ToString.PadLeft(2, "0") & fecha.Year.ToString
    End Function


End Class
