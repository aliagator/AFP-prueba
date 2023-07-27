Imports System.Web.Services
Imports Sonda.Net.DB
Imports Sonda.Net
Imports Sonda.Gestion.Adm.Sys.IngresoEgreso
Imports System.IO


Public Class wsModificacionMasivaCF

    Public Shared Function buscarResumen(ByVal idAdm As Integer, ByVal numProceso As Integer, ByVal codError As Integer) As DataSet
        Dim dbc As OraConn
        Dim ds As New DataSet()
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            Return sysModificacionMasivaCF.buscarResumen(dbc, idAdm, numProceso, codError)

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

    'procesos realizados
    Public Shared Function buscarProcActualizacion(ByVal idAdm As Integer, ByVal numProceso As Integer) As DataSet
        Dim dbc As OraConn
        Dim ds As New DataSet()
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            Return sysModificacionMasivaCF.buscarProcActualizacion(dbc, idAdm, numProceso)

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

    'procesa archivo
    Public Shared Function procesarArchivo(ByVal idAdm As Integer, ByVal nombreArchivo As String, ByVal usu As String, ByVal fun As String, ByRef numProceso As Long, ByRef nombArchivoErro As String) As Boolean
        Dim dbc As OraConn
        Dim ds As New DataSet()
        Dim lec As StreamReader
        Dim esc As StreamWriter
        Dim linea As String
        Dim cantSinDatos As Integer = 0
        Dim arr() As String
        Dim cantReg As Integer = 1
        Dim numSolicitud As Long
        Dim tipoProducto As String
        Dim fecCambio As Date
        Dim nombreFileResul As String
        Dim estado As String
        Dim nS, tP, fC As String
        Dim fecproceso As Date = Now.Date
        Dim nomArchivoResp As String

        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            procesarArchivo = True 'sin errores

            If Not File.Exists(nombreArchivo) Then Throw New Exception("Archivo " & nombreArchivo & " no se encuentra, por favor verifique")
            If Not nombreArchivo.EndsWith("CSV") Then Throw New Exception("Archivo incorrecto (*.CSV), por favor verifique")

            numProceso = sysModificacionMasivaCF.obtenerNumProceso(dbc, idAdm)


            lec = New StreamReader(nombreArchivo, System.Text.Encoding.Default)

            nombreFileResul = Path.GetDirectoryName(nombreArchivo)
            If Not nombreFileResul.EndsWith("\") Then nombreFileResul = nombreFileResul & "\"
            nombreFileResul = nombreFileResul & numProceso & "_" & Path.GetFileName(nombreArchivo)

            esc = New StreamWriter(nombreFileResul, False)
            nomArchivoResp = Path.GetFileName(nombreFileResul)

            Do
                linea = lec.ReadLine
                estado = Nothing

                If Not linea Is Nothing Then
                    arr = linea.Split(";")

                    Try : nS = arr(0) : Catch : nS = 0 : End Try
                    Try : tP = arr(1) : Catch : tP = Nothing : End Try
                    Try : fC = arr(2) : Catch : fC = Nothing : End Try


                    Try : numSolicitud = arr(0) : Catch : estado = "Num Solicitud Incorrecto" : End Try
                    Try : tipoProducto = arr(1) : Catch : estado = "Tipo Producto Incorrecto" : End Try
                    Try : fecCambio = arr(2) : Catch : estado = "Fecha Incorrecta" : End Try

                    If cantReg = 1 Then
                        estado = Nothing
                    Else


                        If estado Is Nothing Then
                            estado = sysModificacionMasivaCF.validarGrabar(dbc, idAdm, numProceso, numSolicitud, tipoProducto, fecCambio, cantReg, fecproceso, nomArchivoResp, usu, fun)
                            If estado.ToUpper = "OK" Then estado = Nothing
                        End If
                    End If
                    If cantReg = 1 Then
                        esc.WriteLine(nS & ";" & tP & ";" & fC & ";" & "Observacion;")
                    Else
                        esc.WriteLine(nS & ";" & tP & ";" & fC & ";" & IIf(estado Is Nothing, "OK", estado))
                    End If
                    cantReg += 1

                Else
                        cantSinDatos += 1
                        If cantSinDatos > 10 Then Exit Do
                    End If

                    If Not estado Is Nothing Then procesarArchivo = False
            Loop

            esc.Flush()
            esc.Close()
            lec.Close()

            dbc.Commit()

        Catch e As SondaException
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            dbc.Rollback()
            Dim sm As New SondaExceptionManager(e)
        Finally
            Try : lec.Close() : Catch : End Try
            Try : esc.Close() : Catch : End Try
            dbc.Close()
        End Try
    End Function

    Public Shared Sub actualizacionMasiva(ByVal idAdm As Integer, ByVal numProceso As Integer, ByVal usu As String, ByVal fun As String)
        Dim dbc As OraConn
        Dim ds As New DataSet()
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            sysModificacionMasivaCF.actualizaMasiva(dbc, idAdm, numProceso, usu, fun)

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
