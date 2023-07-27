Imports Sonda.Net.DB
Imports Sonda.Gestion.Adm.Sys.IngresoEgreso


Public Class INEParametrosDS

    Public Structure Comision

        Public UltimoValor As String
        Public UltimoPeriodo As Date
        Public ds As DataSet
        Public count As Integer
        Public Function traer(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal perComision As Date, ByVal tipoProducto As String, ByVal tipoComision As String, ByVal tipoCliente As String, ByVal idAdmComision As Integer, ByVal tipoCobroComision As String) As DataSet

            Dim dsAux As DataSet
            If IsNothing(perComision) Or IsNothing(tipoProducto) Or IsNothing(tipoComision) Or IsNothing(tipoCliente) Or IsNothing(idAdmComision) Then
                traer = dsVacio()
                Exit Function
            End If

            If IsNothing(ds) Then
                ds = Comisiones.traerUltimoValor(dbc, idAdm, perComision, tipoProducto, tipoComision, tipoCliente, idAdmComision, tipoCobroComision)
                count = ds.Tables(0).Rows.Count
                If count > 0 Then UltimoValor = ds.Tables(0).Rows(0).Item("VAL_MONTO")

            Else
                If ds.Tables(0).Rows.Count = 0 Or UltimoPeriodo > perComision Then
                    ds = Comisiones.traerUltimoValor(dbc, idAdm, perComision, tipoProducto, tipoComision, tipoCliente, idAdmComision, tipoCobroComision)
                    count = ds.Tables(0).Rows.Count
                    If count > 0 Then
                        UltimoValor = ds.Tables(0).Rows(0).Item("VAL_MONTO")
                        UltimoPeriodo = ds.Tables(0).Rows(0).Item("PER_COMISION")
                    End If
                Else
                    Dim dr As DataRow() = ds.Tables(0).Select("TIPO_PRODUCTO='" & tipoProducto & "' AND TIPO_COMISION='" & tipoComision & "' AND TIPO_CLIENTE='" & tipoCliente & "' AND ID_ADM_COMISION=" & idAdmComision & " AND TIPO_COBRO_COMISION='" & tipoCobroComision & "'")
                    If UBound(dr) < 0 Then
                        dsAux = Comisiones.traerUltimoValor(dbc, idAdm, perComision, tipoProducto, tipoComision, tipoCliente, idAdmComision, tipoCobroComision)
                        If dsAux.Tables(0).Rows.Count > 0 Then
                            ds.Tables(0).Rows.Add(dsAux.Tables(0).Rows(0).ItemArray)
                            count = ds.Tables(0).Rows.Count
                            If count > 0 Then UltimoValor = ds.Tables(0).Rows(0).Item("VAL_MONTO")
                        End If
                        traer = dsAux
                        Exit Function
                    Else
                        dsAux = ds.Clone
                        dsAux.Tables(0).Rows.Add(dr(0).ItemArray)
                        traer = dsAux
                        Exit Function
                    End If

                End If
            End If
            traer = ds

        End Function

    End Structure

    Public Structure ParametroGeneral

        Public ds As DataSet

        Private tabla As String

        Public count As Integer

        Public Sub New(ByVal NombreTabla As String)
            tabla = NombreTabla
            count = 0
            ds = Nothing
        End Sub

        Public Function traer(ByRef dbc As OraConn, ByVal paramsNames() As Object, ByVal values() As Object, ByVal tiposDatos() As Object) As DataSet

            Select Case True

                Case IsNothing(ds)
                    ds = traerGlobal(dbc, tabla, ParamsNames, values, tiposDatos)
                    traer = ds
                Case ds.Tables(0).Rows.Count = 0
                    ds = traerGlobal(dbc, tabla, ParamsNames, values, tiposDatos)
                    traer = ds
                Case Else
                    traer = obtenerDesdeDataset(dbc, ds, tabla, ParamsNames, values, tiposDatos)

            End Select

        End Function

    End Structure

    Private Shared Function dsVacio() As DataSet
        Dim T As New DataTable()
        Dim ds As DataSet
        ds = New DataSet()
        ds.Tables.Add(T)
        dsVacio = ds
    End Function

    Private Shared Function traerGlobal(ByRef dbc As OraConn, ByVal tabla As String, ByVal panamNames() As Object, ByVal values() As Object, ByVal tipoDato() As Object) As DataSet

        Dim PROCEDIMIENTO As String = "P" & tabla & ".TRAER"
        Dim PARAMETROS As String
        Dim SENTIDO As String
        Dim TIPOS As String
        Dim I As Integer
        Dim DS As DataSet

        For I = 0 To UBound(panamNames)

            PARAMETROS &= panamNames(I) & ","
            SENTIDO &= "in,"

            Select Case UCase(tipoDato(I))
                Case "INTEGER" : TIPOS &= "int,"
                Case "STRING" : TIPOS &= "string,"
                Case "DATE" : TIPOS &= "date,"
                Case "DECIMAL" : TIPOS &= "decimal,"

            End Select
        Next
        PARAMETROS &= "THISCURSOR"
        SENTIDO &= "out"
        TIPOS &= "cursor"


        Return dbc.ExecProc(PROCEDIMIENTO, PARAMETROS, values, SENTIDO, TIPOS)

    End Function

    Private Shared Function obtenerDesdeDataset(ByRef dbc As OraConn, ByRef ds As DataSet, ByVal tabla As String, ByVal param() As Object, ByVal values() As Object, ByVal tipos() As Object)

        Dim dsAux As DataSet
        Dim dr As DataRow()
        Dim i As Integer
        Dim sql As String

        For i = 0 To UBound(param)
            If param(i) <> "VID_ADM" Then
                If IsNothing(values(i)) Then
                    values(i) = ""
                End If
                sql &= CStr(param(i)).Substring(1) & "="
                Select Case UCase(tipos(i))

                    Case "INTEGER" : sql &= values(i)
                    Case "STRING" : sql &= "'" & values(i) & "'"
                    Case "DATE" : sql &= "# " & values(i).Month & "/" & values(i).Day & "/" & values(i).Year & " #"
                    Case "DECIMAL" : sql &= values(i)

                End Select

                If i <> UBound(param) Then
                    sql &= " And "
                End If
            End If
        Next

        dr = ds.Tables(0).Select(sql)
        If UBound(dr) < 0 Then
            dsAux = traerGlobal(dbc, tabla, param, values, tipos)
            If dsAux.Tables(0).Rows.Count > 0 Then
                ds.Tables(0).Rows.Add(dsAux.Tables(0).Rows(0).ItemArray)
            End If
        Else
            dsAux = ds.Clone
            dsAux.Tables(0).Rows.Add(dr(0).ItemArray)
        End If
        obtenerDesdeDataset = dsAux
    End Function

End Class
