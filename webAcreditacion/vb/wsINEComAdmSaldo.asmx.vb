Option Strict Off

Imports System.Web.Services
Imports Sonda.Net.DB
Imports Sonda.Gestion.Adm.Sys.CodeCompletion
Imports Sonda.Gestion.Adm.Sys.IngresoEgreso
Imports Sonda.Net
Imports Sonda.Gestion.Adm.Sys.Soporte
Imports System.IO

<WebService(Namespace:="http://tempuri.org/")> _
Public Class wsINEComAdmSaldo
	Inherits System.Web.Services.WebService

#Region " Web Services Designer Generated Code "

	Public Sub New()
		MyBase.New()

		'This call is required by the Web Services Designer.
		InitializeComponent()

		'Add your own initialization code after the InitializeComponent() call

	End Sub

	'Required by the Web Services Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Web Services Designer
	'It can be modified using the Web Services Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		components = New System.ComponentModel.Container()
	End Sub

	Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
		'CODEGEN: This procedure is required by the Web Services Designer
		'Do not modify it using the code editor.
		If disposing Then
			If Not (components Is Nothing) Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub

#End Region



	'''''''''<WebMethod()> Public Sub calcularComAdmSaldo(ByVal idAdm As Integer, ByVal periodo As Date, ByVal USU As String, ByVal FUN As String, ByVal seqProceso As Integer, ByVal seqEtapa As Integer)
	'''''''''    Dim dbc As OraConn

	'''''''''    'Dim ds As DataSet
	'''''''''    Dim fecValorCuota As Date
	'''''''''    'Dim codError As Integer
	'''''''''    Dim numeroId As Integer
	'''''''''    Dim numRegPe, numRegNc As Integer
	'''''''''    Dim ccFecAcr As PAR.ccFechaAcreditacion
	'''''''''    Dim tipoCuenta As String
	'''''''''    Dim ds As DataSet
	'''''''''    Dim rPro As PRO.ccProcesos

	'''''''''    Try

	'''''''''        dbc = New OraConn()
	'''''''''        dbc.BeginTrans()

	'''''''''        fecValorCuota = Sys.Kernel.Parametros.FechaAcreditacion.obtenerFechaValorCuota(dbc, idAdm, "ACR")

	'''''''''        ds = Sys.Soporte.Procesos.traerProcesos(dbc, idAdm, seqProceso)

	'''''''''        rPro = New PRO.ccProcesos(ds)
	'''''''''        Select Case rPro.idProceso
	'''''''''            Case "ACRCOMADMSAL_APV" : tipoCuenta = "APV"
	'''''''''            Case "ACRCOMADMSAL_CAV" : tipoCuenta = "CAV"
	'''''''''            Case "ACRCOMADMSAL_CVC" : tipoCuenta = "CVC"
	'''''''''        End Select
	'''''''''        Comisiones.ComisionAdmSaldo.calcular(dbc, idAdm, periodo, fecValorCuota, Nothing, tipoCuenta, seqProceso, seqEtapa, USU, FUN)

	'''''''''        dbc.Commit()

	'''''''''    Catch e As SondaException
	'''''''''        dbc.Rollback()
	'''''''''        Dim sm As New SondaExceptionManager(e)
	'''''''''    Catch e As Exception
	'''''''''        dbc.Rollback()
	'''''''''        Dim sm As New SondaExceptionManager(e)
	'''''''''    Finally
	'''''''''        dbc.Close()
	'''''''''        'log.Save()
	'''''''''    End Try
	'''''''''End Sub


	<WebMethod()> Public Function buscarComAdmSaldo(ByVal idAdm As Integer, ByVal idCliente As Object, ByVal tipoProducto As String, ByVal tipoFondo As String, ByVal categoria As String, ByVal codRegTributario As String, ByVal periodo As Date) As DataSet
		Dim comision As New Comisiones.ComisionAdmSaldo()
		Dim dbc As OraConn
		dbc = New OraConn()
		Try
			buscarComAdmSaldo = comision.buscar(dbc, idAdm, idCliente, tipoProducto, tipoFondo, categoria, codRegTributario, periodo)

		Catch e As SondaException
			Dim sm As New SondaExceptionManager(e)
		Catch e As Exception
			Dim sm As New SondaExceptionManager(e)
		Finally
			dbc.Close()
		End Try
	End Function
	<WebMethod()> Public Function eliminarComAdmSaldo(ByVal idAdm As Integer, ByVal idCliente As Object, ByVal tipoProducto As String, ByVal tipoFondo As String, ByVal categoria As String, ByVal periodo As Date) As DataSet
		Dim comision As New Comisiones.ComisionAdmSaldo()
		Dim dbc As OraConn
		dbc = New OraConn()
		Try

			comision.eliminar(dbc, idAdm, idCliente, tipoProducto, tipoFondo, categoria, periodo)

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
	''''''''<WebMethod()> Public Function wmProcesoBatch(ByVal idAdm As Integer, ByVal seqProceso As Integer, ByVal seqEtapa As Integer, ByVal ds As DataSet, ByVal usu As String, ByVal fun As String)

	''''''''    Dim log As New Procesos.logEtapa()
	''''''''    Dim i As Integer
	''''''''    Try
	''''''''        'log = New Procesos.logEtapa(idAdm, seqProceso, seqEtapa)
	''''''''        'log.estado = Procesos.Estado.Ejecucion
	''''''''        'log.AddEvento("Inicio de proceso" & Chr(13))
	''''''''        'log.FecHoraInicio = Now
	''''''''        'log.Save()

	''''''''        Select Case ds.Tables(0).Rows(0).Item("ID_ETAPA")
	''''''''            Case "ETAPA1"  'CALCULO
	''''''''                log.AddEvento("calculo de comision" & Chr(13))
	''''''''                log.Save()
	''''''''                calcularComAdmSaldo(idAdm, ds.Tables(0).Rows(0).Item("PERIODO"), usu, fun, seqProceso, seqEtapa)

	''''''''                'Case "ETAPA2" 'SIMULACION
	''''''''                '    log.AddEvento("Envio a Simulación")
	''''''''                '    log.Save() '
	''''''''                '    INEControlAcr.ProcesarAcreditacion(idAdm, "COMADMSA", ds.Tables(0).Rows(0).Item("ID_USUARIO_PROCESO"), ds.Tables(0).Rows(0).Item("NUMERO_ID"), "SIM", usu, fun, log)

	''''''''                'Case "ETAPA3" 'ACREDITACION
	''''''''                '    log.AddEvento("Envio a Acreditación")
	''''''''                '    log.Save() '
	''''''''                '    INEControlAcr.ProcesarAcreditacion(idAdm, "COMADMSA", ds.Tables(0).Rows(0).Item("ID_USUARIO_PROCESO"), ds.Tables(0).Rows(0).Item("NUMERO_ID"), "ACR", usu, fun, log)

	''''''''        End Select
	''''''''        'log.estado = Procesos.Estado.Exitoso
	''''''''        'log.AddEvento("finalizacion exitosa del proceso" & Chr(13))
	''''''''        'log.fecHoraFin = Now
	''''''''        'log.Save()


	''''''''    Catch e As SondaException
	''''''''        Dim sm As New SondaExceptionManager(e)
	''''''''        log.AddEvento("ha ocurrido un error" & Chr(13))
	''''''''        log.estado = Procesos.Estado.ConError
	''''''''    Catch e As Exception
	''''''''        Dim sm As New SondaExceptionManager(e)
	''''''''        log.AddEvento("ha ocurrido un error" & Chr(13))
	''''''''        log.estado = Procesos.Estado.ConError
	''''''''    Finally
	''''''''        log.Save()

	''''''''    End Try

	''''''''End Function

	Private Function ObtenerCategoriaCargo(ByVal dbc As OraConn, ByVal idAdm As Integer, ByVal idCliente As Integer, ByVal tipoProducto As String, ByVal tipoFondo As String) As String
		Dim ds As DataSet
		Dim i As Integer
		'Dim dbc As New OraConn()
		Dim rSal As AAA.ccSaldos
		Dim categoria As String = Nothing

		ds = Sys.Kernel.Producto.obtenerSaldosFondo(dbc, idAdm, idCliente, tipoProducto, tipoFondo, "V")
		If ds.Tables(0).Rows.Count = 0 Then
			Throw New Exception("No se encontraron saldos vigentes para cargar")
		End If
		If ds.Tables(0).Rows.Count = 1 Then
			rSal = New AAA.ccSaldos(ds)
			categoria = rSal.categoria
		Else
			For i = 0 To ds.Tables(0).Rows.Count - 1
				rSal = New AAA.ccSaldos(ds.Tables(0).Rows(i))
				Select Case rSal.categoria
					Case "ANTIGUO"
						ObtenerCategoriaCargo = rSal.categoria
						Exit Function
					Case "NUEVO"
						categoria = rSal.categoria
				End Select
			Next

		End If
		ObtenerCategoriaCargo = categoria
		If Not (categoria = "ANTIGUO" Or categoria = "NUEVO") Then
			Throw New Exception("No se encontraron saldos vigentes para cargar")
		End If
		'dbc.Close()
	End Function
	Private Sub ValorizarComisiones(ByVal idAdm As Integer, ByRef rCom As ACR.ccComisionAdmSaldo)
		Dim valorCuota As Decimal
		Dim ds As DataSet
		ds = INEControlAcr.obtenerCuotaAcreditacion(idAdm, rCom.tipoFondo)
		If ds.Tables(0).Rows.Count = 0 Then
			Throw New SondaException(15316)
		End If
		valorCuota = ds.Tables(0).Rows(0).Item("VAL_CUOTA")
		rCom.valCuoComision = Math.Round(rCom.valMlComision / valorCuota, 2)
	End Sub


	'--.--################################################################################
	' metodos copiados de wsINEComAdmSaldo, no se puede catalogar el acr
	'--.-07-01-2008---
	'--.--################################################################################

	<WebMethod()> Public Function wmProcesoBatch(ByVal idAdm As Integer, ByVal seqProceso As Integer, ByVal seqEtapa As Integer, ByVal ds As DataSet, ByVal usu As String, ByVal fun As String)

		Dim log As New Procesos.logEtapa()
		Dim i As Integer
		Try
			'log = New Procesos.logEtapa(idAdm, seqProceso, seqEtapa)
			'log.AddEvento("Inicio de proceso(*)")
			'log.FecHoraInicio = Now
			'log.Save()

			Select Case ds.Tables(0).Rows(0).Item("ID_ETAPA")
				Case "ETAPA1"			  'CALCULO
					log.AddEvento("Calculo de comision..." & Chr(13))
					log.Save()
					calcularComAdmSaldo(idAdm, ds.Tables(0).Rows(0).Item("PERIODO"), usu, fun, seqProceso, seqEtapa)

					'Case "ETAPA2" 'SIMULACION
					'    log.AddEvento("Envio a Simulación")
					'    log.Save() '
					'    INEControlAcr.ProcesarAcreditacion(idAdm, "COMADMSA", ds.Tables(0).Rows(0).Item("ID_USUARIO_PROCESO"), ds.Tables(0).Rows(0).Item("NUMERO_ID"), "SIM", usu, fun, log)

					'Case "ETAPA3" 'ACREDITACION
					'    log.AddEvento("Envio a Acreditación")
					'    log.Save() '
					'    INEControlAcr.ProcesarAcreditacion(idAdm, "COMADMSA", ds.Tables(0).Rows(0).Item("ID_USUARIO_PROCESO"), ds.Tables(0).Rows(0).Item("NUMERO_ID"), "ACR", usu, fun, log)

			End Select
			'log.estado = Procesos.Estado.Exitoso
			'log.AddEvento("finalizacion exitosa del proceso" & Chr(13))
			'log.fecHoraFin = Now
			'log.Save()


		Catch e As SondaException
			Dim sm As New SondaExceptionManager(e)
			log.AddEvento("ha ocurrido un error" & Chr(13))
			log.estado = Procesos.Estado.ConError
		Catch e As Exception
			Dim sm As New SondaExceptionManager(e)
			log.AddEvento("ha ocurrido un error" & Chr(13))
			log.estado = Procesos.Estado.ConError
		Finally
			log.Save()

		End Try

	End Function


	<WebMethod()> Public Sub calcularComAdmSaldo(ByVal idAdm As Integer, ByVal periodo As Date, ByVal USU As String, ByVal FUN As String, ByVal seqProceso As Integer, ByVal seqEtapa As Integer)
		Dim dbc As OraConn

		'Dim ds As DataSet
		Dim fecValorCuota As Date
		'Dim codError As Integer
		Dim numeroId As Integer
		' Dim numRegPe, numRegNc As Integer
		' Dim ccFecAcr As PAR.ccFechaAcreditacion
		Dim tipoCuenta As String
		Dim ds As DataSet
		Dim rPro As PRO.ccProcesos

		Try

			dbc = New OraConn()
			dbc.BeginTrans()

			fecValorCuota = Sys.Kernel.Parametros.FechaAcreditacion.obtenerFechaValorCuota(dbc, idAdm, "ACR")

			ds = Sys.Soporte.Procesos.traerProcesos(dbc, idAdm, seqProceso)

			rPro = New PRO.ccProcesos(ds)
			Select Case rPro.idProceso
				Case "ACRCOMADMSAL_APV" : tipoCuenta = "APV"
				Case "ACRCOMADMSAL_CAV" : tipoCuenta = "CAV"
				Case "ACRCOMADMSAL_CVC" : tipoCuenta = "CVC"
			End Select

			Comisiones.ComisionAdmSaldo.calcular(dbc, idAdm, periodo, fecValorCuota, Nothing, tipoCuenta, seqProceso, seqEtapa, USU, FUN)

			dbc.Commit()

		Catch e As SondaException
			dbc.Rollback()
			Dim sm As New SondaExceptionManager(e)
		Catch e As Exception
			dbc.Rollback()
			Dim sm As New SondaExceptionManager(e)
		Finally
			dbc.Close()
			'log.Save()
		End Try
	End Sub
	'--.--################################################################################






	<WebMethod()> Public Function testProcesoBatch()
		Dim idAdm As Integer = 1
		Dim seqProceso As Integer = 133488
		Dim seqEtapa As Integer = 7
		Dim ds As New DataSet()
		Dim usu As String = "USU"
		Dim fun As String = "FUN"

		ds.Tables.Add(0)
		ds.AcceptChanges()
		ds.Tables(0).Columns.Add("ID_ETAPA", GetType(String))
		ds.Tables(0).Columns.Add("PERIODO", GetType(Date))

		ds.AcceptChanges()

		ds.Tables(0).Rows.Add(New Object() {"ETAPA1", "01/12/2008"})
		ds.AcceptChanges()



		wmProcesoBatch(idAdm, seqProceso, seqEtapa, ds, usu, fun)

	End Function

	Private Function fechaHoraStr() As String
		Dim f As Date = Now.Date
		Return f.Year.ToString & f.Month.ToString & f.Hour.ToString & f.Minute.ToString & f.Second.ToString
	End Function


	'DAB 17/10/2019 CA-3341518 PLI-2019035878 INI
	<WebMethod()> Public Sub generarCSV(ByVal idAdm As Integer, ByVal seqProceso As Integer, ByVal usu As String, ByVal fun As String, ByRef ruta As String)
		Dim ds As New DataSet()
		Dim dsExcel As New DataSet()
		Dim linea As String = Nothing
		Dim es As StreamWriter
		Dim le As StreamReader
		Dim nomArchivo As String
		Dim rutaArchivo As String
		Dim comision As New Comisiones.ComisionAdmSaldo()
		Dim nombreAFP As String
		Dim dbc As OraConn
		Dim i, j, k As Integer
		Dim corte As Integer
		Dim largoArchivo1 As Integer
		Dim largoArchivo2 As Integer
		Dim ind As Integer = 0
		Dim rutaAux As String
		Try
			dbc = New OraConn()
			dbc.BeginTrans()
			ds = comision.genRepCalculoComisionesVol(dbc, idAdm, seqProceso, usu, fun)
			If ds.Tables(0).Rows.Count > 0 Then
				If fun <> "MANUAL" Then
					rutaArchivo = obtenerRutaArchivo(dbc, idAdm, "ACRCOMADMS")
				Else
					rutaArchivo = "C:\aaa\"
				End If

				If rutaArchivo Is Nothing Then
					Throw New Exception("Ruta de Archivo no encontrada")
				Else
					If Not rutaArchivo.ToUpper.EndsWith("\") Then rutaArchivo = rutaArchivo & "\"
				End If

				If ds.Tables(0).Rows.Count <= 1000000 Then			 'Menor o igual que numero grande se crea 1 archivo
					nomArchivo = "ComAdmSaldo_Detalle_" & seqProceso.ToString & "_" & fechaHoraStr() & ".csv"
					generaArchivoCSV(ds, 0, ds.Tables(0).Rows.Count, linea, rutaArchivo, nomArchivo, es, le, usu, ruta)
				Else			 'Se van a crear 2 archivos
					'Se divide la muestra
					largoArchivo1 = Math.Round(ds.Tables(0).Rows.Count / 2)
					largoArchivo2 = Math.Floor(ds.Tables(0).Rows.Count / 2)
					For k = largoArchivo1 To ds.Tables(0).Rows.Count				'Desde el corte hasta el fin
						If ds.Tables(0).Rows(k).Item("RUT") = ds.Tables(0).Rows(k + 1).Item("RUT") Then
							ind = ind + 1
						Else
							Exit For
						End If
					Next
					largoArchivo1 = largoArchivo1 + ind
					largoArchivo2 = ds.Tables(0).Rows.Count - largoArchivo1

					nomArchivo = "ComAdmSaldo_Detalle_" & seqProceso.ToString & "_" & fechaHoraStr() & "_1-2.csv"
					generaArchivoCSV(ds, 0, largoArchivo1, linea, rutaArchivo, nomArchivo, es, le, usu, ruta)
					rutaAux = ruta

					nomArchivo = "ComAdmSaldo_Detalle_" & seqProceso.ToString & "_" & fechaHoraStr() & "_2-2.csv"
					generaArchivoCSV(ds, largoArchivo1, ds.Tables(0).Rows.Count, linea, rutaArchivo, nomArchivo, es, le, usu, ruta)
					ruta = rutaAux & ", y " & ruta

				End If

			Else
				ruta = "N"
			End If
		Catch e As SondaException
			dbc.Rollback()
			Dim sm As New SondaExceptionManager(e)
		Catch e As Exception
			dbc.Rollback()
			Dim sm As New SondaExceptionManager(e)
		Finally
			dbc.Close()
			Try : es.Close() : Catch : End Try
		End Try
	End Sub
	Private Sub generaArchivoCSV(ByVal ds As DataSet, ByVal inicial As Integer, ByVal largoArchivo As Integer, ByVal linea As String, ByVal rutaArchivo As String, ByVal nomArchivo As String, ByVal es As StreamWriter, ByVal le As StreamReader, ByVal usu As String, ByRef ruta As String)
		Dim i, j As Integer
		Dim fechaActual As Date = DateTime.Now()

		'Creamos archivo
		es = New StreamWriter(rutaArchivo & nomArchivo, True)
		es.WriteLine(";;;;;;;;;;;;;;;;;")
		'encabezado
		With ds.Tables(0).Rows(0)
			es.WriteLine("NOMBRE AFP:;" & .Item("AFP") & ";;;;;;;;;;;;;;FECHA ACTUAL:;" & fechaActual.ToString("dd/MM/yyyy"))
			es.WriteLine("SEQ PROCESO:;" & .Item("seq_proceso") & ";;;;;;;;;;;;;;FECHA CUOTA:;" & .Item("fec_valor_cuota"))
			es.WriteLine("USUARIO:;" & usu & ";;REPORTE CALCULO COMISIONES;;;;;;;;;;;;TASA MENSUAL:;" & .Item("tasa"))
			es.WriteLine("PERIODO PROCESO:;" & .Item("per_proceso") & ";;;;;;;;;;;;;;DÍAS MES:;" & .Item("cant_dias_mes"))
			es.WriteLine(";;;;;;;;;;;;;;;;;")
			es.WriteLine("RUT;NOMBRE;NUM SALDO;FECHA APERTURA;TIPO PRODUCTO;TIPO FONDO;TIPO IMPUTACION;CONCEPTO;FECHA ACREDITACION;MONTO;MONTO CUO;MONTO SALDO;MONTO SALDO CUO;PROMEDIO CUO;DIAS PERMANENCIA;FACTOR;MONTO COMISION;MONTO COMISION CUO")
			For i = inicial To largoArchivo - 1
				With ds.Tables(0).Rows(i)
					For j = 7 To 24
						linea = linea & .Item(j) & ";"
					Next
					es.WriteLine(linea)
					linea = Nothing
				End With
			Next
		End With
		es.Flush()
		es.Close()
		ruta = rutaArchivo & nomArchivo
	End Sub
	Private Function obtenerRutaArchivo(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal codArchivo As String) As String
		Dim ds As New DataSet()
		ds = Sys.Soporte.Archivo.traer(dbc, idAdm, codArchivo)
		If ds.Tables(0).Rows.Count > 0 Then
			obtenerRutaArchivo = IIf(IsDBNull(ds.Tables(0).Rows(0).Item("RUTA")), Nothing, ds.Tables(0).Rows(0).Item("RUTA"))
		Else
			Throw New Exception("Error: Ruta de Archivo no encontrada")
		End If
	End Function
	'DAB 17/10/2019 CA-3341518 PLI-2019035878 FIN

End Class
