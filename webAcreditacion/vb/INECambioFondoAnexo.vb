Imports System
Imports System.IO
Imports Sonda.Gestion.Adm.Sys.Soporte
Imports Sonda.Gestion.Adm.Sys.IngresoEgreso
Imports Sonda.Net.DB
Imports Sonda.Net

Imports Sonda.Gestion.Adm.Sys.CodeCompletion


Public Class INECambioFondoAnexo
    Public Shared lineaParam As Integer
    Public Shared lect As StreamReader
    Public Shared escr As StreamWriter


    'call-generacion de anexos (1,2,3)**************************************
    Public Shared Function generarAnexos(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal fecDesde As Date, ByVal fecHasta As Date, ByVal nomAfp As String) As String
        Dim ds As New DataSet()
        Dim viernesAnterior As Date
        Dim fecCorteAnexo3 As Date
        Dim fecInicioAnexo3 As Date
        Dim fechaActual As Date

        fechaActual = Sonda.Gestion.Adm.Sys.Soporte.Fecha.ahora(dbc)

        Dim nomArchivo As String = "ANEXOSCAMBFDO_" & Format(fechaActual, "ddMMyyyyHHmmss")

        Dim ruta As String = obtenerRutas(dbc, idAdm, "XML") & "\"

        abrirArchivo(ruta, "PLANTILLA_ANEXOS", "XML")
        abrirArchivo(ruta, nomArchivo, "XLS")


        iniciaNumLineas(1) '*************************************************anexo_1
        ds = obtenerData(dbc, idAdm, fecDesde, fecHasta, 1)
        genAnexo1(ds, fecDesde, fecHasta, nomAfp)


        iniciaNumLineas(2) '*************************************************anexo_1
        'calcula el viernes anterior (si no es habil...jueves..mier...)
        viernesAnterior = calcularViernesAnterior(dbc, fecDesde)
        ds = obtenerData(dbc, idAdm, fecDesde, viernesAnterior, 2)
        genAnexo2(ds, fecDesde, fecHasta, nomAfp)


        iniciaNumLineas(3)'*************************************************anexo_1
        '''''If fecHasta.DayOfWeek = DayOfWeek.Friday Then
        '''''    fecCorteAnexo3 = calcularDomingoProx(dbc, fecHasta)
        '''''Else
        '''''    fecCorteAnexo3 = fecHasta
        '''''End If
        '''''ds = obtenerData(dbc, idAdm, fecDesde, fecCorteAnexo3, 3)
        '''''genAnexo3(ds, fecDesde, fecCorteAnexo3, nomAfp)

        If fecHasta.DayOfWeek = DayOfWeek.Friday Then
            fecInicioAnexo3 = calcularSabadoAnterior(dbc, fecDesde)
        Else
            fecInicioAnexo3 = fecDesde
        End If

        ds = obtenerData(dbc, idAdm, fecInicioAnexo3, fecHasta, 3)
        genAnexo3(ds, fecInicioAnexo3, fecHasta, nomAfp)

        cerrarArchivo("XML")
        cerrarArchivo("XLS")

        Return ruta & nomArchivo & ".xls"

    End Function
    Public Shared Function calcularViernesAnterior(ByRef dbc As OraConn, ByVal fechaDesde As Date) As Date
        Dim habil As Integer
        'buscar el día sabado anterior a la fecha de lunes
        Do
            fechaDesde = fechaDesde.AddDays(-1)
        Loop While fechaDesde.DayOfWeek <> DayOfWeek.Saturday

        Do
            fechaDesde = fechaDesde.AddDays(-1) 'aqui será viernes, 
            habil = Sonda.Gestion.Adm.Sys.Soporte.Fecha.esHabil(dbc, fechaDesde)
        Loop While habil <> 1

        'verifica si es festivo
        Return fechaDesde
    End Function

    Public Shared Function calcularDomingoProx(ByRef dbc As OraConn, ByVal fechaHasta As Date) As Date
        Dim fechaCorte As Date = fechaHasta
        'buscar el día domingo siguiente para cortar anexo 3
        Do
            fechaCorte = fechaCorte.AddDays(+1)
            'si fin de mes a 1/2 semana--- finalizar
            If fechaCorte.Month <> fechaHasta.Month Then
                fechaCorte = fechaCorte.AddDays(-1)
                Exit Do
            End If

        Loop While fechaCorte.DayOfWeek <> DayOfWeek.Sunday

        Return fechaCorte
    End Function

    '--.-- buscar el sabado + domingo + lunes=total solicitudes subscritas...el lunes anexo3
    Public Shared Function calcularSabadoAnterior(ByRef dbc As OraConn, ByVal fechaDesde As Date) As Date
        Dim fechaCorte As Date = fechaDesde
        'buscar el día Sabado Anterior para sumar las solicitudes añ dia lunes- anexo 3
        Do
            fechaCorte = fechaCorte.AddDays(-1)
            'si fin de mes a 1/2 semana--- finalizar
            If fechaCorte.Month <> fechaDesde.Month Then
                fechaCorte = fechaCorte.AddDays(+1)
                Exit Do
            End If

        Loop While fechaCorte.DayOfWeek <> DayOfWeek.Saturday

        Return fechaCorte
    End Function


    '*****************PUBLICOS**************************************
    Public Shared Function obtenerRutas(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal codArchivo As String) As String
        Dim ruta As String = ""

        If codArchivo = "XML" Then
            ruta = WS.Soporte.TRNComun.traerRutaArchivoSalida(dbc, idAdm, "ACRANEXOCF", "CA")
            If ruta <> "" Then ruta = ruta & "\"
        Else
            ruta = WS.Soporte.TRNComun.traerRutaArchivoSalida(dbc, idAdm, "ACRANEXOCF", "CA")
        End If
        Return ruta

    End Function
    Public Shared Function abrirArchivo(ByVal ruta As String, ByVal nombreArchivo As String, ByVal codArchivo As String)
        Try
            If codArchivo = "XML" Then
                lect = New StreamReader(ruta & "IN\" & nombreArchivo & "." & codArchivo)
            Else
                escr = New StreamWriter(ruta & nombreArchivo & "." & codArchivo)
            End If
        Catch EX As Exception
            Throw New Exception(EX.Message)
        End Try
    End Function
    Public Shared Function obtenerData(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal fecDesde As Date, ByVal fecHasta As Date, ByVal numAnexo As Integer) As DataSet
        Try
            Return CambioFondo.Archivos.anexosCambioFondo(dbc, idAdm, numAnexo, fecDesde, fecHasta)

        Catch EX As Exception
            Throw New Exception(EX.Message)
        End Try
    End Function
    Public Shared Function inicioAnexos(ByVal numAnexo As Integer) As Integer
        Try
            'editar el xml y buscar anexo1
            If numAnexo = 1 Then
                Return 1253
            ElseIf numAnexo = 2 Then
                Return 2002
            ElseIf numAnexo = 3 Then
                Return 2416
            Else
                Throw New Exception("Numero de Anexo no se encuentra")
            End If

        Catch EX As Exception
            Throw New Exception(EX.Message)
        End Try
    End Function
    Public Shared Function cerrarArchivo(ByVal codArchivo As String)
        Try
            If codArchivo = "XML" Then
                lect.Close()
            Else
                escr.Flush()
                escr.Close()
            End If
        Catch EX As Exception
            Throw New Exception(EX.Message)
        End Try
    End Function
    Public Shared Function iniciaNumLineas(ByVal numAnexo As Integer)
        If numAnexo = 1 Then
            lineaParam = 1294
        ElseIf numAnexo = 2 Then
            lineaParam = 2029
            '   lineaNumCta = 2089 'num_cuentas por fondo
        Else
            lineaParam = 2428
            '  lineaNumCta = 2484
        End If
    End Function


    '*****************ANEXO 1**************************************
    Public Shared Sub genAnexo1(ByVal ds As DataSet, ByVal fechadesde As Date, ByVal fechahasta As Date, ByVal administradora As String)
        Dim linea As String = " "
        Dim i As Integer = 1
        Dim inicio As String = "><Data ss:Type=" & Chr(34) & "Number" & Chr(34) & ">"
        Dim fin As String = "</Data></Cell>"
        Dim top As Integer
        Dim fondoOri As String
        Dim fondoDest(4) As String
        Dim totalFondo(4) As Long


        Do
            fondoOri = ""
            fondoDest(0) = "" : fondoDest(1) = "" : fondoDest(2) = "" : fondoDest(3) = "" : fondoDest(4) = ""

            linea = lect.ReadLine
            If i = lineaParam Then
                Dim f1 As String = fechadesde.Day.ToString.PadLeft(2, "0") & fechadesde.Month.ToString.PadLeft(2, "0") & fechadesde.Year
                Dim f2 As String = fechahasta.Day.ToString.PadLeft(2, "0") & fechahasta.Month.ToString.PadLeft(2, "0") & fechahasta.Year
                linea = linea.Replace("FECHA1 - FECHA2", f1 & "-" & f2)
            End If
            If i = lineaParam + 8 Then linea = linea.Replace("ADMINISTRADORA", administradora.ToUpper)



            If i >= lineaParam + 47 And i < lineaParam + 144 Then 'A
                top = lineaParam + 47
                fondoOri = "A" : fondoDest(0) = "B" : fondoDest(1) = "C" : fondoDest(2) = "D" : fondoDest(3) = "E"

            ElseIf i >= lineaParam + 144 And i < lineaParam + 241 Then 'B
                top = lineaParam + 144
                fondoOri = "B" : fondoDest(0) = "A" : fondoDest(1) = "C" : fondoDest(2) = "D" : fondoDest(3) = "E"

            ElseIf i >= lineaParam + 241 And i < lineaParam + 338 Then 'C
                top = lineaParam + 241
                fondoOri = "C" : fondoDest(0) = "A" : fondoDest(1) = "B" : fondoDest(2) = "D" : fondoDest(3) = "E"

            ElseIf i >= lineaParam + 338 And i < lineaParam + 435 Then 'D
                top = lineaParam + 338
                fondoOri = "D" : fondoDest(0) = "A" : fondoDest(1) = "B" : fondoDest(2) = "C" : fondoDest(3) = "E"

            ElseIf i >= lineaParam + 435 And i < lineaParam + 532 Then 'E
                top = lineaParam + 435
                fondoOri = "E" : fondoDest(0) = "A" : fondoDest(1) = "B" : fondoDest(2) = "C" : fondoDest(3) = "D"

            ElseIf i >= lineaParam + 532 And i < lineaParam + 607 Then 'Z
                top = lineaParam + 532
                fondoOri = "Z" : fondoDest(0) = "A" : fondoDest(1) = "B" : fondoDest(2) = "C" : fondoDest(3) = "D"

            ElseIf i >= lineaParam + 608 And i <= lineaParam + 650 Then 'ZZ
                'top = lineaParam + 608
                top = 1902
                fondoOri = "Z" : fondoDest(0) = "E" : fondoDest(1) = "Z" : fondoDest(2) = "Z" : fondoDest(3) = "Z" : fondoDest(4) = "Z"
            End If

            If top <> 0 Then
                If (i >= top And i <= top + 15) Then linea = porFondo(ds, linea, i, fondoOri, fondoDest(0), inicio, fin, totalFondo)

                If (i >= top + 19 And i <= top + 35) Then linea = porFondo(ds, linea, i, fondoOri, fondoDest(1), inicio, fin, totalFondo)

                If Not fondoDest(2) = "Z" Then
                    If (i >= top + 38 And i <= top + 53) Then linea = porFondo(ds, linea, i, fondoOri, fondoDest(2), inicio, fin, totalFondo)
                End If

                If Not fondoDest(3) = "Z" Then
                    If (i >= top + 57 And i <= top + 72) Then linea = porFondo(ds, linea, i, fondoOri, fondoDest(3), inicio, fin, totalFondo)
                End If

                If Not (fondoOri = "Z" And fondoDest(4) = "Z") Then
                    If (i >= top + 76 And i <= top + 96) Then linea = porFondo(ds, linea, i, fondoOri, "Z", inicio, fin, totalFondo)
                End If

            End If

            i = i + 1
            escr.WriteLine(linea)

            If i = inicioAnexos(2) Then
                escr.Flush()
                Exit Do
            End If

            If (i Mod 100) = 0 Then
                escr.Flush()
            End If
        Loop While linea <> ""

    End Sub
    Public Shared Function porFondo(ByVal DS As DataSet, ByRef linea As String, ByVal numlinea As Integer, ByVal fondoOri As String, ByVal fondoDest As String, ByVal inicio As Object, ByVal fin As Object, ByRef totalFondo() As Long)
        Dim num As Integer

        Select Case fondoOri

            Case "A"
                num = lineaParam + 47
                Select Case fondoDest
                    Case "B" : num = num
                    Case "C" : num = num + 19
                    Case "D" : num = num + 38
                    Case "E" : num = num + 57
                    Case "Z" : num = num + 77
                End Select
            Case "B"
                num = lineaParam + 144
                Select Case fondoDest
                    Case "A" : num = num
                    Case "C" : num = num + 19
                    Case "D" : num = num + 38
                    Case "E" : num = num + 57
                    Case "Z" : num = num + 77
                End Select
            Case "C"
                num = lineaParam + 241
                Select Case fondoDest
                    Case "A" : num = num
                    Case "B" : num = num + 19
                    Case "D" : num = num + 38
                    Case "E" : num = num + 57
                    Case "Z" : num = num + 77
                End Select
            Case "D"
                num = lineaParam + 338
                Select Case fondoDest
                    Case "A" : num = num
                    Case "B" : num = num + 19
                    Case "C" : num = num + 38
                    Case "E" : num = num + 57
                    Case "Z" : num = num + 77
                End Select
            Case "E"
                num = lineaParam + 435
                Select Case fondoDest
                    Case "A" : num = num
                    Case "B" : num = num + 19
                    Case "C" : num = num + 38
                    Case "D" : num = num + 57
                    Case "Z" : num = num + 77
                End Select
            Case "Z"
                num = lineaParam + 532
                Select Case fondoDest
                    Case "A" : num = num
                    Case "B" : num = num + 19
                    Case "C" : num = num + 38
                    Case "D" : num = num + 57
                    Case "E" : num = num + 76
                    Case "Z" : num = num + 96
                End Select


        End Select

        If num + 0 = numlinea Then linea = linea.Replace("/>", inicio & Valor(DS, fondoOri, fondoDest, 3) & fin) 'n°cuentas
        If num + 1 = numlinea Then linea = linea.Replace("/>", inicio & Valor(DS, fondoOri, fondoDest, 2) & fin) 'pesos
        If num + 2 = numlinea Then linea = linea.Replace("/>", inicio & Valor(DS, fondoOri, fondoDest, 5) & fin)
        If num + 3 = numlinea Then linea = linea.Replace("/>", inicio & Valor(DS, fondoOri, fondoDest, 4) & fin)
        If num + 4 = numlinea Then linea = linea.Replace("/>", inicio & Valor(DS, fondoOri, fondoDest, 7) & fin)
        If num + 5 = numlinea Then linea = linea.Replace("/>", inicio & Valor(DS, fondoOri, fondoDest, 6) & fin)
        If num + 6 = numlinea Then linea = linea.Replace("/>", inicio & Valor(DS, fondoOri, fondoDest, 9) & fin)
        If num + 7 = numlinea Then linea = linea.Replace("/>", inicio & Valor(DS, fondoOri, fondoDest, 8) & fin)
        If num + 8 = numlinea Then linea = linea.Replace("/>", inicio & Valor(DS, fondoOri, fondoDest, 11) & fin)
        If num + 9 = numlinea Then linea = linea.Replace("/>", inicio & Valor(DS, fondoOri, fondoDest, 10) & fin)
        If num + 10 = numlinea Then linea = linea.Replace("/>", inicio & Valor(DS, fondoOri, fondoDest, 13) & fin)
        If num + 11 = numlinea Then linea = linea.Replace("/>", inicio & Valor(DS, fondoOri, fondoDest, 12) & fin)
        If num + 12 = numlinea Then linea = linea.Replace("/>", inicio & Valor(DS, fondoOri, fondoDest, 15) & fin)
        If num + 13 = numlinea Then linea = linea.Replace("/>", inicio & Valor(DS, fondoOri, fondoDest, 14) & fin)
        If num + 14 = numlinea Then linea = linea.Replace("/>", inicio & Valor(DS, fondoOri, fondoDest, 17) & fin)
        If num + 15 = numlinea Then
            linea = linea.Replace("/>", inicio & Valor(DS, fondoOri, fondoDest, 16) & fin)

            If fondoDest = "Z" Then
                Select Case fondoOri
                    Case "A" : totalFondo(0) = Valor(DS, fondoOri, fondoDest, 16)
                    Case "B" : totalFondo(1) = Valor(DS, fondoOri, fondoDest, 16)
                    Case "C" : totalFondo(2) = Valor(DS, fondoOri, fondoDest, 16)
                    Case "D" : totalFondo(3) = Valor(DS, fondoOri, fondoDest, 16)
                    Case "E" : totalFondo(4) = Valor(DS, fondoOri, fondoDest, 16)
                End Select
            End If

        End If

        Return linea
    End Function
    Public Shared Function Valor(ByVal ds As DataSet, ByVal fondoOri As String, ByVal fondoDest As String, ByVal items As Integer) As Long
        Valor = 0
        Dim i As Integer
        For i = 0 To ds.Tables(0).Rows.Count - 1
            With ds.Tables(0).Rows(i)
                If .Item("TIPO_FONDO_ORIGEN") = fondoOri And .Item("TIPO_FONDO_DESTINO") = fondoDest Then
                    Try
                        Valor = .Item(items)
                    Catch
                        Valor = 0
                    End Try
                    Exit For
                End If
            End With
        Next i
    End Function


    '*****************ANEXO 2**************************************
    Public Shared Function Valor(ByVal ds As DataSet, ByVal fondoOri As String, ByVal items As Integer) As Long
        Valor = 0
        Dim i As Integer
        For i = 0 To ds.Tables(0).Rows.Count - 1
            With ds.Tables(0).Rows(i)
                If .Item("TIPO_FONDO") = fondoOri Then
                    Try
                        Valor = IIf(items = 1, .Item("NUM_REGISTROS"), .Item("VAL_ML_SALDO"))
                    Catch
                        Valor = 0
                    End Try
                    Exit For
                End If
            End With
        Next i
    End Function
    Public Shared Sub genAnexo2(ByVal ds As DataSet, ByVal fechadesde As Date, ByVal fechahasta As Date, ByVal administradora As String)
        Dim i As Integer = inicioAnexos(2)
        Dim linea As String = Nothing
        Dim inicio As String = "><Data ss:Type=" & Chr(34) & "Number" & Chr(34) & ">"
        Dim fin As String = "</Data></Cell>"

        Do
            linea = lect.ReadLine


            If i = lineaParam Then
                Dim f1 As String = fechadesde.Day.ToString.PadLeft(2, "0") & fechadesde.Month.ToString.PadLeft(2, "0") & fechadesde.Year
                Dim f2 As String = fechahasta.Day.ToString.PadLeft(2, "0") & fechahasta.Month.ToString.PadLeft(2, "0") & fechahasta.Year
                linea = linea.Replace("FECHA1 - FECHA2", f1 & "-" & f2)
            End If
            If i = lineaParam + 2 Then linea = linea.Replace("ADMINISTRADORA", administradora.ToUpper)



            If i = lineaParam + 46 Then linea = linea.Replace("/>", inicio & Valor(ds, "A", 1) & fin)
            If i = lineaParam + 48 Then linea = linea.Replace("/>", inicio & Valor(ds, "A", 2) & fin)

            If i = lineaParam + 60 Then linea = linea.Replace("/>", inicio & Valor(ds, "B", 1) & fin)
            If i = lineaParam + 62 Then linea = linea.Replace("/>", inicio & Valor(ds, "B", 2) & fin)

            If i = lineaParam + 74 Then linea = linea.Replace("/>", inicio & Valor(ds, "C", 1) & fin)
            If i = lineaParam + 76 Then linea = linea.Replace("/>", inicio & Valor(ds, "C", 2) & fin)

            If i = lineaParam + 88 Then linea = linea.Replace("/>", inicio & Valor(ds, "D", 1) & fin)
            If i = lineaParam + 90 Then linea = linea.Replace("/>", inicio & Valor(ds, "D", 2) & fin)

            If i = lineaParam + 102 Then linea = linea.Replace("/>", inicio & Valor(ds, "E", 1) & fin)
            If i = lineaParam + 104 Then linea = linea.Replace("/>", inicio & Valor(ds, "E", 2) & fin)




            i = i + 1
            escr.WriteLine(linea)

            If i = inicioAnexos(3) Then
                escr.Flush()
                Exit Do
            End If
        Loop While linea <> ""

    End Sub


    '*****************ANEXO 3**************************************
    Public Shared Function genAnexo3(ByVal ds As DataSet, ByVal fechadesde As Date, ByVal fechahasta As Date, ByVal administradora As String)
        Dim linea As String = ""
        Dim i, j As Integer

        Dim t(35) As camposAnexo3
        For i = 0 To 35
            t(i) = New camposAnexo3()
        Next i

        preparaVector(ds, t, fechadesde)

        Dim numCCO As Integer = lineaParam + 56
        Dim numCCV As Integer = lineaParam + 215
        Dim numCDC As Integer = lineaParam + 373
        Dim numCAF As Integer = lineaParam + 531
        Dim numCVC As Integer = lineaParam + 689
        Dim numCAV As Integer = lineaParam + 847
        Dim numCAI As Integer = lineaParam + 1005
        j = 0
        i = inicioAnexos(3)
        Do
            linea = lect.ReadLine

            If i = lineaParam Then
                Dim f1 As String = fechadesde.Day.ToString.PadLeft(2, "0") & fechadesde.Month.ToString.PadLeft(2, "0") & fechadesde.Year
                Dim f2 As String = fechahasta.Day.ToString.PadLeft(2, "0") & fechahasta.Month.ToString.PadLeft(2, "0") & fechahasta.Year
                linea = linea.Replace("FECHA1 - FECHA2", f1 & "-" & f2)
            End If
            If i = lineaParam + 2 Then linea = linea.Replace("ADMINISTRADORA", administradora.ToUpper)

            If i >= numCCO And i < numCCO + 82 And j <= 4 Then modificaLinea(numCCO, i, t, j, linea)
            If i >= numCCV And i < numCCV + 82 And j > 4 And j <= 9 Then modificaLinea(numCCV, i, t, j, linea)
            If i >= numCDC And i < numCDC + 82 And j > 9 And j <= 14 Then modificaLinea(numCDC, i, t, j, linea)
            If i >= numCAF And i < numCAF + 82 And j > 14 And j <= 19 Then modificaLinea(numCAF, i, t, j, linea)
            If i >= numCVC And i < numCVC + 82 And j > 19 And j <= 24 Then modificaLinea(numCVC, i, t, j, linea)
            If i >= numCAV And i < numCAV + 82 And j > 24 And j <= 29 Then modificaLinea(numCAV, i, t, j, linea)
            If i >= numCAI And i < numCAI + 82 And j > 29 And j <= 34 Then modificaLinea(numCAI, i, t, j, linea)

            i = i + 1
            escr.WriteLine(linea)
        Loop While linea <> ""

    End Function
    Public Shared Function obtenerIndex(ByVal indexProd As Integer, ByVal fondo As String) As Integer
        Dim val As Integer
        Select Case fondo
            Case "A" : val = indexProd + 0
            Case "B" : val = indexProd + 1
            Case "C" : val = indexProd + 2
            Case "D" : val = indexProd + 3
            Case "E" : val = indexProd + 4
            Case Else : val = 35
        End Select
        Return val
    End Function
    Public Shared Function preparaVector(ByVal ds As DataSet, ByRef t() As camposAnexo3, ByVal fechaDesde As Date)
        Dim i As Integer
        Dim fecha As Date
        Dim producto As String
        Dim numReg As Integer
        Dim fondoOri As String
        Dim fondoDes As String
        Dim indexProd As Integer


        For i = 0 To ds.Tables(0).Rows.Count - 1
            With ds.Tables(0).Rows(i)
                fecha = IIf(IsDBNull(.Item("fec_cambio")), Nothing, .Item("fec_cambio"))
                producto = IIf(IsDBNull(.Item("tipo_producto")), Nothing, .Item("tipo_producto"))
                numReg = IIf(IsDBNull(.Item("num_registros")), Nothing, .Item("num_registros"))
                fondoOri = IIf(IsDBNull(.Item("tipo_fondo_origen")), "x", .Item("tipo_fondo_origen"))
                fondoDes = IIf(IsDBNull(.Item("tipo_fondo_destino")), "x", .Item("tipo_fondo_destino"))


                Select Case producto
                    Case "CCO" : indexProd = 0 : t(0).fecha = fecha
                    Case "CCV" : indexProd = 5 : t(5).fecha = fecha
                    Case "CDC" : indexProd = 10 : t(10).fecha = fecha
                    Case "CAF" : indexProd = 15 : t(15).fecha = fecha
                    Case "CVC" : indexProd = 20 : t(20).fecha = fecha
                    Case "CAV" : indexProd = 25 : t(25).fecha = fecha
                    Case "CAI" : indexProd = 30 : t(30).fecha = fecha
                End Select



                ''''If fecha = fechaDesde Then ' DIA LUNES
                ''''    t(obtenerIndex(indexProd, fondoOri)).LunesO += numReg
                ''''    t(obtenerIndex(indexProd, fondoDes)).LunesD += numReg
                ''''ElseIf fecha = fechaDesde.AddDays(1) Then ' DIA martes
                ''''    t(obtenerIndex(indexProd, fondoOri)).MartesO += numReg
                ''''    t(obtenerIndex(indexProd, fondoDes)).MartesD += numReg
                ''''ElseIf fecha = fechaDesde.AddDays(2) Then  ' DIA miercoles
                ''''    t(obtenerIndex(indexProd, fondoOri)).MiercolesO += numReg
                ''''    t(obtenerIndex(indexProd, fondoDes)).MiercolesD += numReg
                ''''ElseIf fecha = fechaDesde.AddDays(3) Then  ' DIA jueves
                ''''    t(obtenerIndex(indexProd, fondoOri)).JuevesO += numReg
                ''''    t(obtenerIndex(indexProd, fondoDes)).JuevesD += numReg
                ''''ElseIf fecha = fechaDesde.AddDays(4) Then  ' DIA viernes
                ''''    t(obtenerIndex(indexProd, fondoOri)).ViernesO += numReg
                ''''    t(obtenerIndex(indexProd, fondoDes)).ViernesD += numReg


                ''''ElseIf fecha = fechaDesde.AddDays(5) Then  ' DIA sabado
                ''''    t(obtenerIndex(indexProd, fondoOri)).LunesO += numReg
                ''''    t(obtenerIndex(indexProd, fondoDes)).LunesD += numReg
                ''''ElseIf fecha = fechaDesde.AddDays(6) Then  ' DIA domingo
                ''''    t(obtenerIndex(indexProd, fondoOri)).LunesO += numReg
                ''''    t(obtenerIndex(indexProd, fondoDes)).LunesD += numReg
                ''''End If


                If fecha.DayOfWeek = DayOfWeek.Monday Then ' DIA LUNES
                    t(obtenerIndex(indexProd, fondoOri)).LunesO += numReg
                    t(obtenerIndex(indexProd, fondoDes)).LunesD += numReg

                ElseIf fecha.DayOfWeek = DayOfWeek.Tuesday Then ' DIA martes
                    t(obtenerIndex(indexProd, fondoOri)).MartesO += numReg
                    t(obtenerIndex(indexProd, fondoDes)).MartesD += numReg

                ElseIf fecha.DayOfWeek = DayOfWeek.Wednesday Then  ' DIA miercoles
                    t(obtenerIndex(indexProd, fondoOri)).MiercolesO += numReg
                    t(obtenerIndex(indexProd, fondoDes)).MiercolesD += numReg

                ElseIf fecha.DayOfWeek = DayOfWeek.Thursday Then  ' DIA jueves
                    t(obtenerIndex(indexProd, fondoOri)).JuevesO += numReg
                    t(obtenerIndex(indexProd, fondoDes)).JuevesD += numReg

                ElseIf fecha.DayOfWeek = DayOfWeek.Friday Then  ' DIA viernes
                    t(obtenerIndex(indexProd, fondoOri)).ViernesO += numReg
                    t(obtenerIndex(indexProd, fondoDes)).ViernesD += numReg


                ElseIf fecha.DayOfWeek = DayOfWeek.Saturday Then  ' DIA sabado
                    t(obtenerIndex(indexProd, fondoOri)).LunesO += numReg
                    t(obtenerIndex(indexProd, fondoDes)).LunesD += numReg

                ElseIf fecha.DayOfWeek = DayOfWeek.Sunday Then  ' DIA domingo
                    t(obtenerIndex(indexProd, fondoOri)).LunesO += numReg
                    t(obtenerIndex(indexProd, fondoDes)).LunesD += numReg
                End If


            End With
        Next i




        '///GENERA ARCHIVO CON EL VECTOR ////////////////////////////////////////////////////////
        ''''''Dim aa As New StreamWriter("c:\aer.txt")
        ''''''Dim b As String = ""
        ''''''For i = 0 To 35
        ''''''    b = t(i).fecha & vbTab & t(i).fondo & vbTab & t(i).LunesO & vbTab & t(i).LunesD & vbTab & t(i).MartesO & vbTab & t(i).MartesD & vbTab & t(i).MiercolesO & vbTab & t(i).MiercolesD & vbTab & t(i).JuevesO & vbTab & t(i).JuevesD & vbTab & t(i).ViernesO & vbTab & t(i).ViernesD & vbCrLf

        ''''''    If i = 4 Or i = 9 Or i = 14 Or i = 19 Or i = 24 Or i = 29 Or i = 34 Then
        ''''''        b = b & vbCrLf
        ''''''    End If
        ''''''    aa.Write(b)


        ''''''Next i
        ''''''aa.Flush()
        ''''''aa.Close()
        '/////////////////////////////////////////////////////////////////////////////////////////////////////////



    End Function
    Public Shared Function modificaLinea(ByRef numProducto As Integer, ByVal numLinea As Integer, ByRef t() As camposAnexo3, ByRef index As Integer, ByRef linea As String)

        If numLinea = numProducto + 0 Then linea = linea.Replace(">0</Data></Cell>", ">" & t(index).LunesO & "</Data></Cell>")
        If numLinea = numProducto + 1 Then linea = linea.Replace(">0</Data></Cell>", ">" & t(index).LunesD & "</Data></Cell>")

        If numLinea = numProducto + 2 Then linea = linea.Replace(">0</Data></Cell>", ">" & t(index).MartesO & "</Data></Cell>")
        If numLinea = numProducto + 3 Then linea = linea.Replace(">0</Data></Cell>", ">" & t(index).MartesD & "</Data></Cell>")

        If numLinea = numProducto + 4 Then linea = linea.Replace(">0</Data></Cell>", ">" & t(index).MiercolesO & "</Data></Cell>")
        If numLinea = numProducto + 5 Then linea = linea.Replace(">0</Data></Cell>", ">" & t(index).MiercolesD & "</Data></Cell>")

        If numLinea = numProducto + 6 Then linea = linea.Replace(">0</Data></Cell>", ">" & t(index).JuevesO & "</Data></Cell>")
        If numLinea = numProducto + 7 Then linea = linea.Replace(">0</Data></Cell>", ">" & t(index).JuevesD & "</Data></Cell>")

        If numLinea = numProducto + 8 Then linea = linea.Replace(">0</Data></Cell>", ">" & t(index).ViernesO & "</Data></Cell>")
        If numLinea = numProducto + 9 Then
            linea = linea.Replace(">0</Data></Cell>", ">" & t(index).ViernesD & "</Data></Cell>")
            numProducto += 18
            index += 1
        End If

    End Function


    Public Shared Function traerRutaArchivoSalida(ByVal dbc As OraConn, ByVal idAdm As Integer, ByVal codArchivo As String, ByVal codEntidad As String) As String
        Dim dsArc, dsEnt As DataSet

        dsArc = Archivo.traer(dbc, idAdm, codArchivo)
        dsEnt = Archivo.buscarEntidad(dbc, idAdm, codArchivo, codEntidad)

        If dsEnt.Tables(0).Rows.Count > 0 Then

            If dsArc.Tables(0).Rows.Count > 0 Then
                Dim arc As New TRN.ccArchivo(dsArc)

                If Not IsNothing(arc.ruta) AndAlso arc.ruta <> "" Then

                    If System.IO.Directory.Exists(arc.ruta) Then
                        Return arc.ruta
                    Else
                        'El directorio no existe
                        Throw New SondaException(12509, New Exception("Archivo: " & arc.codArchivo & " Directorio: " & arc.ruta))
                    End If

                Else
                    Dim par As New par.ccTrnParametros(Parametro.traerGlobal(dbc, "PAR_TRN_PARAMETROS", New Object() {idAdm}))
                    Dim dir As String
                    par.uncTempDir = par.uncTempDir.ToString.Replace("/", "\")
                    par.uncTempDir = par.uncTempDir.ToString.TrimEnd("\")
                    dir = par.uncTempDir.ToString + "\" + arc.idProcesoNegocio + "\" + arc.idSubProcesoNegocio + "\" + codEntidad + "\OUT\"


                    If Not System.IO.Directory.Exists(dir) Then
                        Try
                            System.IO.Directory.CreateDirectory(dir)
                        Catch e As System.IO.IOException
                            'Imposible crear el directorio, verifique los parametros de configuración del sistema de archivos
                            Throw New SondaException(12509, New Exception("Archivo: " & arc.codArchivo & " Directorio: " & dir))
                        End Try
                    End If

                    Return dir
                End If

            Else
                'Error al rescatar la informacion del archivo
                Throw New SondaException(12503, New Exception("CODARCHIVO: " & codArchivo & " CODENTIDAD: " & codEntidad))
            End If
        Else
            Throw New SondaException(12501, New Exception("CODARCHIVO: " & codArchivo & " CODENTIDAD: " & codEntidad)) 'La relación Archivo-Entidad no existe
        End If

    End Function


End Class

Public Class camposAnexo3
    Public fecha As Date
    Public tipoProducto As String
    Public fondo As String
    Public LunesO As Integer
    Public LunesD As Integer
    Public MartesO As Integer
    Public MartesD As Integer
    Public MiercolesO As Integer
    Public MiercolesD As Integer
    Public JuevesO As Integer
    Public JuevesD As Integer
    Public ViernesO As Integer
    Public ViernesD As Integer

    'Public SabadoO As Integer
    'Public SabadoD As Integer

    'Public DomingoO As Integer
    'Public DomingoD As Integer

    Public Sub New()
        fecha = Nothing
        tipoProducto = Nothing
        LunesO = Nothing
        LunesD = Nothing
        MartesO = Nothing
        MartesD = Nothing
        MiercolesO = Nothing
        MiercolesD = Nothing
        JuevesO = Nothing
        JuevesD = Nothing
        ViernesO = Nothing
        ViernesD = Nothing

        'SabadoO = Nothing
        'SabadoO = Nothing

        'DomingoO = Nothing
        'DomingoD = Nothing
    End Sub




End Class
