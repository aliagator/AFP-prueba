Option Explicit On 
Option Strict Off

Imports Sonda.Net.DB
Imports Sonda.Net
Imports Sonda.Gestion.Adm.Sys.IngresoEgreso
Imports Sonda.Gestion.Adm.Sys.CodeCompletion
Imports System.IO

Public Class INEProcesosExceso


    Public Sub wmProcesoBatch(ByVal idAdm As Integer, ByVal seqProceso As Integer, ByVal seqEtapa As Integer, ByVal ds As DataSet, ByVal usu As String, ByVal fun As String)
        Dim dbc As New OraConn()
        Dim etapa As String
        Dim perProceso As Date
        Dim tipoProducto As String

        Try
            dbc.BeginTrans()

            etapa = ds.Tables(0).Rows(0).Item("ID_ETAPA")
            perProceso = ds.Tables(0).Rows(0).Item("PER_PROCESO")
            tipoProducto = ds.Tables(0).Rows(0).Item("TIPO_PRODUCTO")


            If etapa = "ETAPA1" Then
                sysExcesos.controlExcesoEmpleador(dbc, idAdm, perProceso, tipoProducto, seqProceso, seqEtapa, usu, fun)
            ElseIf etapa = "VALORACION" Then

            Else
                Throw New Exception("Etapa No definida")
            End If

            dbc.Commit()


        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Sub


    'CA-5285674 - RRS - 04/02/20 - Informe Excesos 
    Public Function wmGeneraExcelExcesoEmpleador(ByVal idadm As Integer, ByVal perProceso As Date, ByVal seqProceso As Integer, ByVal fun As String) As String
        Dim dbc As OraConn
        Dim cp As New controlProcesos()
        Dim dsDatos As New DataSet()
        Dim rutaArchivo As String
        Dim nombreArchivo As String
        Dim nombreRuta As String = ""
        Dim dsFile As New DataSet()

        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            If fun = "MANUAL" Then
                rutaArchivo = "C:\aaa\"
            Else
                dsFile = Sys.Soporte.Archivo.traer(dbc, idadm, "FAMRESOUT")

                If dsFile.Tables(0).Rows.Count > 0 Then
                    rutaArchivo = dsFile.Tables(0).Rows(0).Item("RUTA")
                    If Not rutaArchivo.EndsWith("\") Then rutaArchivo = rutaArchivo & "\"

                Else
                    nombreRuta = "No existe el directorio para la generación del archivo"
                End If

            End If

            'Nombre Archivo
            nombreArchivo = "EXCESOS_" & seqProceso & "_" & dateToString(Now.Date) & ".csv"

            'obtener los datos del reporte
            dsDatos = sysExcesos.generarInformeExcesosEmpl(dbc, idadm, perProceso)
            If nombreRuta = "" Then
                If dsDatos.Tables(0).Rows.Count > 0 Then
                    'crearArchivo con la respuesta
                    Dim nombreFile As String = rutaArchivo & nombreArchivo
                    Dim esc As New StreamWriter(nombreFile, False)
                    Dim i As Integer
                    Dim linea As String

                    esc.WriteLine("RUT AFILIADO; NOMBRE AFILIADO; PERIODO EXCESO; MONTO TOTAL EXCESO; CANTIDAD EMPLEADORES")

                    For i = 0 To dsDatos.Tables(0).Rows.Count - 1
                        linea = Nothing
                        With dsDatos.Tables(0).Rows(i)
                            linea = strNr(.Item("RUT")) & ";" & _
                                    strNr(.Item("NOMBRE")) & ";" & _
                                    strNr(.Item("PER_COTIZACION")) & ";" & _
                                    strNr(.Item("VALOR_EXCESO")) & ";" & _
                                    strNr(.Item("CANT_EMPLEADORES")) & ";"
                        End With
                        esc.WriteLine(linea)
                    Next
                    esc.Flush()
                    esc.Close()



                    nombreRuta = rutaArchivo & nombreArchivo
                Else
                    nombreRuta = "No existen Excesos procesados para la generación del archivo"
                End If
            End If
            Return nombreRuta

        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
        Finally
            dbc.Close()
        End Try
    End Function

    Public Shared Function strNr(ByVal valor As Object) As String
        Return IIf(IsDBNull(valor), "", valor)
    End Function

    Public Shared Function dateToString(ByVal fecha As Date, Optional ByVal guion As String = Nothing) As String
        Return fecha.Year.ToString & guion & fecha.Month.ToString.PadLeft(2, "0") & guion & fecha.Day.ToString.PadLeft(2, "0")
    End Function

    'Private Sub crearXlsExceso(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal ds As DataSet, ByVal rutaArchivo As String, ByVal nombreArchivo As String)
    '    Dim nombFile As String
    '    Dim cntReg As Integer
    '    Dim hojaAct As Integer = 1
    '    Dim fila As Integer = 0
    '    Dim rowIndex As Integer = 0
    '    Dim i, k As Integer
    '    Dim vid_persona, vap_paterno, vap_materno, vnombre As String

    '    Dim titulo() As String = {"RUT AFILIADO", _
    '                                "NOMBRE AFILIADO", _
    '                                "PERIODO EXCESO", _
    '                                "MONTO TOTAL EXCESO", _
    '                                "CANTIDAD EMPLEADORES"}

    '    nombFile = rutaArchivo & nombreArchivo

    '    'Creación del archivo XLS
    '    Dim xls As New XlsFile(True)

    '    xls.NewFile(1)

    '    xls.ActiveSheet = 1

    '    xls.SheetName = "Nómina de Excesos"

    '    For i = 0 To titulo.GetUpperBound(0)
    '        xls.SetCellValue(1, i + 1, titulo(i))
    '    Next i
    '    If ds.Tables(0).Rows.Count > 0 Then
    '        Dim obj As String
    '        Dim j As Integer
    '        For j = 0 To ds.Tables(0).Rows.Count - 1
    '            With ds.Tables(0).Rows(j)
    '                For i = 1 To titulo.GetUpperBound(0) + 1
    '                    asignarValor(xls, j + 2, i, IIf(IsDBNull(.Item(i)), Nothing, .Item(i)))
    '                Next i
    '            End With
    '        Next j
    '    End If

    '    xls.ActiveSheet = 1
    '    xls.SelectCell(1, 1, False)
    '    xls.Save(nombFile, TFileFormats.Xls) 'Guardamos Archivo y lo enviamos a destino  
    '    ' -----------------------------------------------------------------------------------
    'End Sub

    'Private Function asignarValor(ByRef xls As XlsFile, ByVal fila As Integer, ByVal columna As Integer, ByVal valor As Object)
    '    Dim tipoDato As String
    '    If valor Is Nothing Then
    '    Else
    '        tipoDato = valor.GetType().ToString.ToUpper
    '        tipoDato = tipoDato.Replace("SYSTEM.", "")
    '        Select Case tipoDato
    '            Case "INT32", "INT64"
    '                xls.SetCellValue(fila, columna, CType(valor, Integer))
    '            Case "DECIMAL"
    '                xls.SetCellValue(fila, columna, CType(valor, Decimal))
    '            Case "DATETIME", "DATE"
    '                xls.SetCellValue(fila, columna, CType(dateToString(valor, "-"), String))
    '            Case Else
    '                xls.SetCellValue(fila, columna, CType(valor, String))
    '        End Select
    '    End If
    'End Function

    'Public Shared Function dateToString(ByVal fecha As Date, Optional ByVal guion As String = Nothing) As String
    '    Return fecha.Year.ToString & guion & fecha.Month.ToString.PadLeft(2, "0") & guion & fecha.Day.ToString.PadLeft(2, "0")
    'End Function

    ' FIN - CA-5285674 - RRS - 04/02/20 - Informe Excesos 


End Class
