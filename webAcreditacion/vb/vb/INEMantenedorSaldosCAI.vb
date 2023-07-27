Imports Sonda.Net.DB
Imports Sonda.Net
Imports System.IO
Imports Sonda.Gestion.Adm.Sys.IngresoEgreso
Imports Sonda.Gestion.Adm.Sys
Imports Sonda.Gestion.Adm.Sys.Kernel
Imports Sonda.Gestion.Adm.Sys.Soporte
Public Class INEMantenedorSaldosCAI
    Public Sub wmProcesoBatch(ByVal idAdm As Integer, ByVal seqProceso As Integer, ByVal seqEtapa As Integer, ByVal ds As DataSet, ByVal usu As String, ByVal fun As String)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()

            If seqEtapa = 1 Then
                dbc.BeginTrans()
                MantenedorSaldosCAI.ValidarDatosMasivo(dbc, idAdm, seqProceso, seqEtapa, usu, fun)
                dbc.Commit()
            End If
            If seqEtapa = 2 Then
                dbc.BeginTrans()
                creaLoteAcreditacionSaldosCAI(dbc, idAdm, seqProceso, seqEtapa, ds, usu, fun)
                'MantenedorSaldosCAI.creaLoteAcreditacionSaldosCAI(dbc, idAdm, seqProceso, seqEtapa)
                dbc.Commit()
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
    Public Sub creaLoteAcreditacionSaldosCAI(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal seqProceso As Integer, ByVal seqEtapa As Integer, ByVal ds As DataSet, ByVal usu As String, ByVal fun As String)
        Dim log As New Procesos.logEtapa(idAdm, seqProceso, seqEtapa)
        Dim acr As New IngresoEgreso.INEControlAcr()
        Dim fecAcr As Date
        Dim numeroId As Integer
        Dim fecCreacion As Date
        Dim SeqRegistro As Integer
        Dim CodError As Integer
        Dim indErrorCarga As Boolean
        Dim codMvto As String
        Dim perProceso As Date
        Dim i As Integer
        Dim j As Integer
        Dim totRegProc As Integer
        Dim fecCierre As Date
        Dim tipoProceso As String = "M"
        Try
            If fun <> "MANUAL" Then
                log.AddEvento("***INICIO PROCESO DE CREACIÓN DE LOTE DE ACREDITACIÓN***")
                log.Save()
            End If

            indErrorCarga = False
            fecAcr = Parametros.FechaAcreditacion.obtenerFechaAcreditacion(dbc, idAdm, "ACR")
            If fun <> "MANUAL" Then
                log.AddEvento("Se obtiene la fecha acreditacion=" & CStr(fecAcr))
                log.Save()
            End If

            acr.crearProcesoAcreditacion(dbc, idAdm, "AJUMASIV", usu, seqProceso, 0, "ENL", usu, fun, numeroId, fecCreacion, CodError)
            If fun <> "MANUAL" Then
                log.AddEvento("Se crea el proceso de Acreditacion Id=" & CStr(numeroId))
                log.Save()
            End If
            If CodError <> 0 Then
                log.AddEvento("Se generó el error " & CodError & " al crear el proceso de acreditación...")
                indErrorCarga = True
            End If
            perProceso = CType("01-" & fecAcr.Month & "-" & fecAcr.Year, Date)
            If CodError = 0 Then
                'Cargamos transacciones
                Dim blnErrDes As Boolean
                blnErrDes = cargaTransaccionesSaldosCAI(dbc, idAdm, ds, numeroId, perProceso, fecAcr, usu, fun, log)
                If blnErrDes = True Then
                    indErrorCarga = True
                End If
                acr.CerrarProcesoAcreditacion(dbc, idAdm, "AJUMASIV", usu, numeroId, usu, fun, totRegProc, fecCierre, CodError)
                If CodError <> 0 Then
                    indErrorCarga = True
                    If fun <> "MANUAL" Then
                        log.AddEvento("Se cierra proceso de acreditacion, error=" & CodError)
                        log.Save()
                    End If
                Else
                    If fun <> "MANUAL" Then
                        log.AddEvento("Se cierra proceso de acreditacion sin errores")
                        log.Save()
                    End If
                End If
                If fun <> "MANUAL" Then
                    log.AddEvento("Hora de termino:" & CStr(Sys.Soporte.Fecha.ahora(dbc)))
                    log.Save()
                    log.AddEvento("<<<<< FIN DEL PROCESO DE CARGA DE LOTE DE ACREDITACIÓN PARA CUENTAS CAI >>>>>")
                    log.Save()
                End If
            Else
                If fun <> "MANUAL" Then
                    log.AddEvento("error al iniciar proceso de acreditacion")
                    log.Save()
                End If
            End If
            If fun <> "MANUAL" Then
                If indErrorCarga = False Then
                    'Iniciar actualizacion en tabla detalle de registros con numero_id
                    actualizarDatosSaldosCAI(dbc, idAdm, ds, numeroId, seqProceso, tipoProceso, usu, fun)
                    log.estado = Procesos.Estado.Exitoso
                    log.Save()
                    dbc.Commit()
                Else
                    log.estado = Procesos.Estado.ConError
                    log.Save()
                    dbc.Rollback()
                End If
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
    Public Shared Sub actualizarDatosSaldosCAI(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal ds As DataSet, ByVal numeroId As Integer, ByVal seqProceso As Integer, ByVal tipoProceso As String, ByVal usu As String, ByVal fun As String)
        Dim i As Integer
        Dim seqDetalle As Long
        Try
            For i = 0 To ds.Tables(0).Rows.Count - 1
                seqDetalle = ds.Tables(0).Rows(i).Item("SEQ_DETALLE")
                MantenedorSaldosCAI.actualizaDetalleSaldosCAI(dbc, idAdm, numeroId, seqProceso, seqDetalle, usu, fun)
            Next
            MantenedorSaldosCAI.actualizaCabeceraSaldosCAI(dbc, idAdm, numeroId, seqProceso, tipoProceso, usu, fun)
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
            dbc.Rollback()
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
            dbc.Rollback()
        End Try
    End Sub
    Public Shared Function cargaTransaccionesSaldosCAI(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal ds As DataSet, ByVal numeroId As Integer, ByVal perProceso As Date, ByVal fecAcr As Date, ByVal usu As String, ByVal fun As String) As Integer
        Dim acr As New IngresoEgreso.INEControlAcr()
        Dim objdate As Object
        Dim SeqRegistro As Integer
        Dim CodError As Integer
        Dim i, j As Integer
        cargaTransaccionesSaldosCAI = False
        For i = 0 To 1
            If i = 0 Then
                'crear transacciones con cargos
                For j = 0 To ds.Tables(0).Rows.Count - 1
                    With ds.Tables(0).Rows(j)
                        acr.crearTransacciones(dbc, idAdm, "AJUSELEC", usu, numeroId, .Item("ID_PERSONA"), _
                            .Item("ID_CLIENTE"), "", "", "", "", "", perProceso, .Item("SEQ_CARGA"), .Item("SEQ_DETALLE"), 0, 0, 0, 0, "", "", "", "", "", objdate, _
                            objdate, 0, fecAcr, objdate, objdate, "", 0, 0, 0, 0, 0, "CAI", .Item("TIPO_FONDO"), .Item("TIPO_FONDO"), _
                            .Item("CATEGORIA_ORIGEN"), .Item("SUB_CATEGORIA_ORIGEN"), "", "CAR", "CTA", "CTA", "", 0, 0, "AJUSELEC", "320960", "", "", "", "", "", 0, 0, "", Nothing, "", objdate, _
                            0, objdate, objdate, objdate, 0, 0, 0, 0, 0, 0, 0, "", 0, .Item("VAL_ML_SALDO_DESTINO"), 0, 0, .Item("VAL_CUO_DESTINO"), _
                            0, 0, 0, 0, 0, 0, 0, 0, "", 0, 0, 0, "", 0, 0, "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "CUO", "S", "N", "S", objdate, 0, "", _
                            Nothing, Nothing, Nothing, 0, 0, 0, 0, 0, 0, 0, 0, usu, fun, SeqRegistro, CodError)

                        If CodError <> 0 Then
                            cargaTransaccionesSaldosCAI = CodError
                        End If
                    End With
                Next
            End If
            If i = 1 Then
                'crear transacciones con abonos
                For j = 0 To ds.Tables(0).Rows.Count - 1
                    With ds.Tables(0).Rows(j)
                        acr.crearTransacciones(dbc, idAdm, "AJUSELEC", usu, numeroId, .Item("ID_PERSONA"), _
                            .Item("ID_CLIENTE"), "", "", "", "", "", perProceso, .Item("SEQ_CARGA"), .Item("SEQ_DETALLE"), 0, 0, 0, 0, "", "", "", "", "", objdate, _
                            objdate, 0, fecAcr, objdate, objdate, "", 0, 0, 0, 0, 0, "CAI", .Item("TIPO_FONDO"), .Item("TIPO_FONDO"), _
                            .Item("CATEGORIA_DESTINO"), .Item("SUB_CATEGORIA_DESTINO"), "", "ABO", "CTA", "CTA", "", 0, 0, "AJUSELEC", "310960", "", "", "", "", "", 0, 0, "", Nothing, "", objdate, _
                            0, objdate, objdate, objdate, 0, 0, 0, 0, 0, 0, 0, "", 0, .Item("VAL_ML_SALDO_DESTINO"), 0, 0, .Item("VAL_CUO_DESTINO"), _
                            0, 0, 0, 0, 0, 0, 0, 0, "", 0, 0, 0, "", 0, 0, "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "CUO", "S", "N", "S", objdate, 0, "", _
                            Nothing, Nothing, Nothing, 0, 0, 0, 0, 0, 0, 0, 0, usu, fun, SeqRegistro, CodError)
                        If CodError <> 0 Then
                            cargaTransaccionesSaldosCAI = CodError
                        End If
                    End With
                Next
            End If
        Next
    End Function
    Public Shared Function cargaTransaccionesSaldosCAI(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal ds As DataSet, ByVal numeroId As Integer, ByVal perProceso As Date, ByVal fecAcr As Date, ByVal usu As String, ByVal fun As String, ByVal log As Procesos.logEtapa) As Boolean
        Dim acr As New IngresoEgreso.INEControlAcr()
        Dim objdate As Object
        Dim SeqRegistro As Integer
        Dim CodError As Integer
        Dim i, j As Integer
        cargaTransaccionesSaldosCAI = False
        For i = 0 To 1
            If i = 0 Then
                'crear transacciones con cargos
                For j = 0 To ds.Tables(0).Rows.Count - 1
                    With ds.Tables(0).Rows(j)
                        acr.crearTransacciones(dbc, idAdm, "AJUMASIV", usu, numeroId, .Item("ID_PERSONA"), _
                            .Item("ID_CLIENTE"), "", "", "", "", "", perProceso, .Item("SEQ_CARGA"), .Item("SEQ_DETALLE"), 0, 0, 0, 0, "", "", "", "", "", objdate, _
                            objdate, 0, fecAcr, objdate, objdate, "", 0, 0, 0, 0, 0, "CAI", .Item("TIPO_FONDO"), .Item("TIPO_FONDO"), _
                            .Item("CATEGORIA_ORIGEN"), .Item("SUB_CATEGORIA_ORIGEN"), "", "CAR", "CTA", "CTA", "", 0, 0, "AJUMASIV", "320960", "", "", "", "", "", 0, 0, "", Nothing, "", objdate, _
                            0, objdate, objdate, objdate, 0, 0, 0, 0, 0, 0, 0, "", 0, .Item("VAL_ML_SALDO_DESTINO"), 0, 0, .Item("VAL_CUO_DESTINO"), _
                            0, 0, 0, 0, 0, 0, 0, 0, "", 0, 0, 0, "", 0, 0, "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "CUO", "S", "N", "S", objdate, 0, "", _
                            Nothing, Nothing, Nothing, 0, 0, 0, 0, 0, 0, 0, 0, usu, fun, SeqRegistro, CodError)

                        If CodError <> 0 Then
                            log.AddEvento("Error al cargar transacciones de cargos codigo " & CodError & " cliente " & .Item("ID_PERSONA"))
                            cargaTransaccionesSaldosCAI = True
                        End If
                    End With
                Next
            End If
            If i = 1 Then
                'crear transacciones con abonos
                For j = 0 To ds.Tables(0).Rows.Count - 1
                    With ds.Tables(0).Rows(j)
                        acr.crearTransacciones(dbc, idAdm, "AJUMASIV", usu, numeroId, .Item("ID_PERSONA"), _
                            .Item("ID_CLIENTE"), "", "", "", "", "", perProceso, .Item("SEQ_CARGA"), .Item("SEQ_DETALLE"), 0, 0, 0, 0, "", "", "", "", "", objdate, _
                            objdate, 0, fecAcr, objdate, objdate, "", 0, 0, 0, 0, 0, "CAI", .Item("TIPO_FONDO"), .Item("TIPO_FONDO"), _
                            .Item("CATEGORIA_DESTINO"), .Item("SUB_CATEGORIA_DESTINO"), "", "ABO", "CTA", "CTA", "", 0, 0, "AJUMASIV", "310960", "", "", "", "", "", 0, 0, "", Nothing, "", objdate, _
                            0, objdate, objdate, objdate, 0, 0, 0, 0, 0, 0, 0, "", 0, .Item("VAL_ML_SALDO_DESTINO"), 0, 0, .Item("VAL_CUO_DESTINO"), _
                            0, 0, 0, 0, 0, 0, 0, 0, "", 0, 0, 0, "", 0, 0, "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, "CUO", "S", "N", "S", objdate, 0, "", _
                            Nothing, Nothing, Nothing, 0, 0, 0, 0, 0, 0, 0, 0, usu, fun, SeqRegistro, CodError)
                        If CodError <> 0 Then
                            log.AddEvento("Error al cargar transacciones de abonos codigo " & CodError & " cliente " & .Item("ID_PERSONA"))
                            cargaTransaccionesSaldosCAI = True
                        End If
                    End With
                Next
            End If
        Next
    End Function
    Public Shared Sub insertarDatosTablaTempXls(ByVal ds As DataSet, ByVal idAdm As Integer)
        Dim dbc As OraConn
        Dim i As Integer
        Dim rutAfiliado As String
        Dim tipoFondo As String
        Dim categoriaOrigen As String
        Dim cuotasOrigen As Decimal
        Dim categoriaDestino As String
        Dim cuotasDestino As Decimal
        Dim subCategoriaOrigen As Integer = 0
        Dim subCategoriaDestino As Integer = 0
        Try
            dbc = New OraConn()
            dbc.BeginTrans()
            For i = 0 To ds.Tables(0).Rows.Count - 1
                rutAfiliado = ds.Tables(0).Rows(i).Item("Rut Afiliado")
                tipoFondo = ds.Tables(0).Rows(i).Item("Tipo de Fondo")
                categoriaOrigen = ds.Tables(0).Rows(i).Item("Categoría Origen")
                If ds.Tables(0).Columns.Contains("Subcategoria Origen") Then
                    subCategoriaOrigen = ds.Tables(0).Rows(i).Item("Subcategoria Origen")
                End If
                cuotasOrigen = IIf(IsDBNull(ds.Tables(0).Rows(i).Item("Cuotas Origen")), -99, ds.Tables(0).Rows(i).Item("Cuotas Origen")) '
                categoriaDestino = ds.Tables(0).Rows(i).Item("Categoría Destino")
                If ds.Tables(0).Columns.Contains("Subcategoria Destino") Then
                    subCategoriaDestino = ds.Tables(0).Rows(i).Item("Subcategoria Destino")
                End If
                cuotasDestino = IIf(IsDBNull(ds.Tables(0).Rows(i).Item("Cuotas Destino")), -99, ds.Tables(0).Rows(i).Item("Cuotas Destino"))
                MantenedorSaldosCAI.insertaDatosTablaTempXls(dbc, idAdm, rutAfiliado, tipoFondo, categoriaOrigen, cuotasOrigen, _
                                         categoriaDestino, cuotasDestino, i, subCategoriaOrigen, subCategoriaDestino)
            Next i
            dbc.Commit()
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
            dbc.Rollback()
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
            dbc.Rollback()
        Finally
            dbc.Close()
        End Try
    End Sub

    Public Shared Function validarDatosArchivoXls(ByVal idAdm As Integer, ByVal rutaServer As String, ByVal archivo As String, ByVal usu As String, ByVal fun As String) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()
            validarDatosArchivoXls = MantenedorSaldosCAI.validaDatosArchivoXls(dbc, idAdm, rutaServer, archivo, usu, fun)
            If validarDatosArchivoXls.Tables(0).Rows.Count > 0 Then
                dbc.Commit()
            Else
                dbc.Rollback()
            End If
            'dbc.Commit()
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

    Public Shared Function traerSaldosAfiliado(ByVal idAdm As Integer, ByVal idCliente As Integer) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()
            traerSaldosAfiliado = MantenedorSaldosCAI.traeSaldosAfiliado(dbc, idAdm, idCliente, "CAI")
            dbc.Commit()
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
    Public Shared Function traerPorRut(ByVal idAdm As Integer, ByVal idPersona As String) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            traerPorRut = Kernel.Cliente.traerPorRut(dbc, idAdm, idPersona)
            dbc.Commit()
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
    Public Shared Sub procesarSaldosCAISelectivo(ByVal idAdm As Integer, ByVal ds As DataSet, ByVal usu As String, ByVal fun As String, ByRef codErrorGeneral As Integer, ByRef numeroId As Integer)
        Dim dbc As OraConn
        dbc = New OraConn()
        Dim i As Integer
        Dim dsContrato As DataSet
        Dim idCliente As Integer = ds.Tables(0).Rows(0).Item("ID_CLIENTE")
        Dim idPersona As String = ds.Tables(0).Rows(0).Item("ID_PERSONA")
        Dim dsDatosAInsertar As New DataSet()
        Dim dsDetSaldosCai As DataSet
        Dim dtDatosAInsertar As New DataTable()

        Dim ValidarDatosSelectivo As Integer = 0 'Sin Errores
        Dim sumValCuoOrigenA As Decimal
        Dim sumValCuoDestinoA As Decimal

        Dim sumValCuoOrigenB As Decimal
        Dim sumValCuoDestinoB As Decimal

        Dim sumValCuoOrigenC As Decimal
        Dim sumValCuoDestinoC As Decimal

        Dim sumValCuoOrigenD As Decimal
        Dim sumValCuoDestinoD As Decimal

        Dim sumValCuoOrigenE As Decimal
        Dim sumValCuoDestinoE As Decimal

        Dim valCuoSaldo As Decimal
        Dim valCuoDestino As Decimal
        Dim valMlSaldo As Integer
        Dim valMlSaldoDestino As Integer
        Dim tipoFondo As String
        Dim categoriaOrigen As String
        Dim categoriaDestino As String 'Puede venir con o sin formato
        Dim subCategoriaOrigen As Integer
        Dim subCategoriaDestino As Integer
        Dim seqCarga As Integer

        dsDatosAInsertar.Tables.Add(dtDatosAInsertar)
        dsDatosAInsertar.Tables(0).Columns.Add("ID_CLIENTE", GetType(Integer))
        'dsDatosAInsertar.Tables(0).Columns.Add("ID_PERSONA", GetType(String))
        dsDatosAInsertar.Tables(0).Columns.Add("TIPO_FONDO", GetType(String))
        dsDatosAInsertar.Tables(0).Columns.Add("CATEGORIA_ORIGEN", GetType(String))
        dsDatosAInsertar.Tables(0).Columns.Add("SUB_CATEGORIA_ORIGEN", GetType(Integer))
        dsDatosAInsertar.Tables(0).Columns.Add("VAL_ML_SALDO", GetType(Integer))
        dsDatosAInsertar.Tables(0).Columns.Add("VAL_CUO_SALDO", GetType(Decimal))
        dsDatosAInsertar.Tables(0).Columns.Add("CATEGORIA_DESTINO", GetType(String))
        dsDatosAInsertar.Tables(0).Columns.Add("SUB_CATEGORIA_DESTINO", GetType(Integer))
        dsDatosAInsertar.Tables(0).Columns.Add("VAL_ML_SALDO_DESTINO", GetType(Integer))
        dsDatosAInsertar.Tables(0).Columns.Add("VAL_CUO_DESTINO", GetType(Decimal))

        For i = 0 To ds.Tables(0).Rows.Count - 1
            If Not IsDBNull(ds.Tables(0).Rows(i).Item("CATEGORIA_DESTINO")) And _
               (Not IsDBNull(ds.Tables(0).Rows(i).Item("VAL_CUO_DESTINO")) Or Not IsDBNull(ds.Tables(0).Rows(i).Item("VAL_ML_SALDO_DESTINO"))) Then
                valCuoSaldo = ds.Tables(0).Rows(i).Item("VAL_CUO_SALDO")
                valCuoDestino = CType(ds.Tables(0).Rows(i).Item("VAL_CUO_DESTINO"), String).Replace(".", ",")
                If IsDBNull(ds.Tables(0).Rows(i).Item("VAL_ML_SALDO_DESTINO")) Then 'Si no se ingreso nada en el campo monto destino
                    valMlSaldoDestino = 0
                Else
                    valMlSaldoDestino = CType(ds.Tables(0).Rows(i).Item("VAL_ML_SALDO_DESTINO"), Integer)
                End If
                tipoFondo = ds.Tables(0).Rows(i).Item("TIPO_FONDO")
                categoriaOrigen = String_To_Rut(CType(ds.Tables(0).Rows(i).Item("CATEGORIA_ORIGEN"), String))
                subCategoriaOrigen = ds.Tables(0).Rows(i).Item("SUB_CATEGORIA_ORIGEN")
                categoriaDestino = String_To_Rut(CType(ds.Tables(0).Rows(i).Item("CATEGORIA_DESTINO"), String))
                subCategoriaDestino = 0 'Se obtiene en PKG
                valMlSaldo = ds.Tables(0).Rows(i).Item("VAL_ML_SALDO")
                If valCuoSaldo < valCuoDestino Then
                    codErrorGeneral = 1 'Por registro monto origen no puede ser menor que monto destino
                    Exit For
                End If
                If tipoFondo = "A" Then
                    sumValCuoOrigenA = sumValCuoOrigenA + valCuoSaldo
                    sumValCuoDestinoA = sumValCuoDestinoA + valCuoDestino
                ElseIf tipoFondo = "B" Then
                    sumValCuoOrigenB = sumValCuoOrigenB + valCuoSaldo
                    sumValCuoDestinoB = sumValCuoDestinoB + valCuoDestino
                ElseIf tipoFondo = "C" Then
                    sumValCuoOrigenC = sumValCuoOrigenC + valCuoSaldo
                    sumValCuoDestinoC = sumValCuoDestinoC + valCuoDestino
                ElseIf tipoFondo = "D" Then
                    sumValCuoOrigenD = sumValCuoOrigenD + valCuoSaldo
                    sumValCuoDestinoD = sumValCuoDestinoD + valCuoDestino
                ElseIf tipoFondo = "E" Then
                    sumValCuoOrigenE = sumValCuoOrigenE + valCuoSaldo
                    sumValCuoDestinoE = sumValCuoDestinoE + valCuoDestino
                End If
                dsContrato = Sys.Kernel.TipoProducto.traerEmpleadorPorRut(dbc, idAdm, idCliente, categoriaDestino, "CAI", "V")
                If dsContrato.Tables(0).Rows.Count = 0 Then
                    codErrorGeneral = 2 'Afiliado no tiene contrato con el empleador(categoria destino)
                    Exit For
                End If
                If valCuoDestino = 0 And valMlSaldoDestino = 0 Then
                    codErrorGeneral = 3 'Monto de cuotas y monto saldo es cero
                    Exit For
                End If
                'Si se ingresa monto en pesos pero no en cuotas
                If valCuoDestino = 0 And valMlSaldoDestino <> 0 Then
                    valCuoDestino = MantenedorSaldosCAI.obtenerCuotaPorSaldo(dbc, idAdm, tipoFondo, valMlSaldoDestino)
                End If
                dsDatosAInsertar.Tables(0).Rows.Add(New Object() {idCliente, tipoFondo, _
                                                                               categoriaOrigen, subCategoriaOrigen, _
                                                                               valMlSaldo, valCuoSaldo, categoriaDestino, _
                                                                               subCategoriaDestino, valMlSaldoDestino, valCuoDestino})
            End If
        Next
        ds.AcceptChanges()
        dsDatosAInsertar.AcceptChanges()
        If codErrorGeneral = 0 Then 'Si no hubo errores por registro
            If sumValCuoDestinoA > sumValCuoOrigenA Or sumValCuoDestinoB > sumValCuoOrigenB Or _
                sumValCuoDestinoC > sumValCuoOrigenC Or sumValCuoDestinoD > sumValCuoOrigenD Or _
                sumValCuoDestinoE > sumValCuoOrigenE Then
                codErrorGeneral = 4 'Suma monto destino por tipo de fondo para el cliente no puede ser mayor que el origen
            Else
                'Grabar ds en tabla cabecera y detalle como selectivo
                grabarDatosSelectivosCAI(dbc, idAdm, dsDatosAInsertar, codErrorGeneral, ValidarDatosSelectivo, usu, fun, seqCarga)
                If codErrorGeneral = 0 Then
                    dsDetSaldosCai = MantenedorSaldosCAI.traerDetSaldosCai(dbc, idAdm, seqCarga)
                    If dsDetSaldosCai.Tables(0).Rows.Count > 0 Then
                        numeroId = creaLoteAcreditacionSaldosCAISelectivo(dbc, idAdm, dsDetSaldosCai, usu, fun, codErrorGeneral) 'Se realiza commit o rollback de los cambios anteriores
                    End If
                End If

            End If
        End If
    End Sub
    Public Shared Function creaLoteAcreditacionSaldosCAISelectivo(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal ds As DataSet, ByVal usu As String, ByVal fun As String, ByRef codErrorGeneral As Integer) As Integer
        Dim indErrorCarga As Boolean
        Dim CodError As Integer
        Dim fecAcr As Date
        Dim fecCreacion As Date
        Dim numeroId As Integer
        Dim acr As New IngresoEgreso.INEControlAcr()
        Dim perProceso As Date
        Dim totRegProc As Integer
        Dim fecCierre As Date
        Dim tipoProceso As String = "S"
        Try
            indErrorCarga = False
            fecAcr = Parametros.FechaAcreditacion.obtenerFechaAcreditacion(dbc, idAdm, "ACR")
            acr.crearProcesoAcreditacion(dbc, idAdm, "AJUSELEC", usu, 0, 0, "ENL", usu, fun, numeroId, fecCreacion, CodError)
            If CodError <> 0 Then
                'indErrorCarga = True
                Throw New SondaException(CodError)
            End If
            perProceso = CType("01-" & fecAcr.Month & "-" & fecAcr.Year, Date)
            If CodError = 0 Then
                Dim valorCodigoError As Integer
                valorCodigoError = cargaTransaccionesSaldosCAI(dbc, idAdm, ds, numeroId, perProceso, fecAcr, usu, fun)
                If valorCodigoError <> 0 Then 'Si hubo un error al ingresar las transacciones
                    Throw New SondaException(valorCodigoError)
                Else
                    acr.CerrarProcesoAcreditacion(dbc, idAdm, "AJUSELEC", usu, numeroId, usu, fun, totRegProc, fecCierre, CodError)
                    If CodError <> 0 Then
                        Throw New SondaException(CodError)
                    Else
                        actualizarDatosSaldosCAI(dbc, idAdm, ds, numeroId, 0, tipoProceso, usu, fun)
                        dbc.Commit()
                        creaLoteAcreditacionSaldosCAISelectivo = numeroId
                    End If
                End If
            Else
                dbc.Rollback()
            End If
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
            dbc.Rollback()
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
            dbc.Rollback()
        End Try
    End Function

    Public Shared Sub grabarDatosSelectivosCAI(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal ds As DataSet, ByRef codError As Integer, ByRef ValidarDatosSelectivo As Integer, ByVal usu As String, ByVal fun As String, ByRef seqCarga As Integer)
        Dim i As Integer
        Dim fecCarga As Date = DateTime.Now
        Try
            seqCarga = MantenedorSaldosCAI.insertaEncMantSaldosCAI(dbc, idAdm, fecCarga, "SELECTIVO", "SELECTIVO", "V", fecCarga, usu, fun, "N", "S")
            If Not IsNothing(seqCarga) Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    With ds.Tables(0).Rows(i)
                        MantenedorSaldosCAI.insertaDetMantSaldosCAI(dbc, idAdm, fecCarga, seqCarga, .Item("ID_CLIENTE"), _
                                                                   .Item("TIPO_FONDO"), .Item("CATEGORIA_ORIGEN"), _
                                                                   .Item("VAL_CUO_SALDO"), .Item("CATEGORIA_DESTINO"), .Item("VAL_CUO_DESTINO"), _
                                                                   codError, "DI", .Item("SUB_CATEGORIA_ORIGEN"), "N", usu, fun, (i + 1))
                    End With

                Next
                'dbc.Commit()
            Else
                codError = 5 ' Error al insertar en cabecera
                dbc.Rollback()
            End If
        Catch e As SondaException
            Dim sm As New SondaExceptionManager(e)
            dbc.Rollback()
        Catch e As Exception
            Dim sm As New SondaExceptionManager(e)
            dbc.Rollback()
        End Try
    End Sub
    Public Shared Function String_To_Rut(ByVal rut As String) As String
        'Transforma Rut en formato de base de datos ej: 11.111.111-1 --> 000000111111111
        Return StrDup(15 - Len(Replace(Replace(Trim(rut), "-", ""), ".", "")), "0") & UCase(Replace(Replace(Trim(rut), "-", ""), ".", ""))
    End Function
    Public Shared Function blTraerEncSaldosCAI(ByVal idAdm As Integer) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            blTraerEncSaldosCAI = MantenedorSaldosCAI.traerEncSaldosCAI(dbc, idAdm)
            dbc.Commit()
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
End Class
