Option Strict Off

Imports System.Web.Services
Imports Sonda.Net.DB
Imports Sonda.Gestion.Adm.Sys.CodeCompletion
Imports Sonda.Gestion.Adm.Sys.IngresoEgreso
Imports Sonda.Net
Imports Sonda.Gestion.Adm.Sys.Soporte

<WebService(Namespace:="http://tempuri.org/")> _
Public Class wsINEControlProcesos
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

	<WebMethod()> Public Function wmObtenerEtapaPorPeriodo(ByVal idadm As Integer, ByVal idProceso As String, ByVal idEtapa As String, ByVal periodo As Date) As DataSet
		Dim dbc As OraConn
		Dim cp As New controlProcesos()
		Try
			dbc = New OraConn()
			wmObtenerEtapaPorPeriodo = cp.obtenerEtapaPorPeriodo(dbc, idadm, idProceso, idEtapa, periodo)
		Catch e As SondaException
			Dim sm As New SondaExceptionManager(e)
		Catch e As Exception
			Dim sm As New SondaExceptionManager(e)
		Finally
			dbc.Close()
		End Try
	End Function
	<WebMethod()> Public Function wmObtenerUltSeqEtapa(ByVal idadm As Integer, ByVal periodo As Date, ByVal idProceso As String, ByVal idEtapa As String) As DataSet
		Dim dbc As OraConn
		Dim cp As New controlProcesos()
		Try
			dbc = New OraConn()
			wmObtenerUltSeqEtapa = cp.obtenerUltSeqEtapa(dbc, idadm, periodo, idProceso, idEtapa)
		Catch e As SondaException
			Dim sm As New SondaExceptionManager(e)
		Catch e As Exception
			Dim sm As New SondaExceptionManager(e)
		Finally
			dbc.Close()
		End Try
	End Function
	<WebMethod()> Public Function wmObtenerProcesoPorPeriodo(ByVal idadm As Integer, ByVal idProceso As String, ByVal periodo As Date) As DataSet
		Dim dbc As OraConn
		Dim cp As New controlProcesos()
		Try
			dbc = New OraConn()
			wmObtenerProcesoPorPeriodo = cp.obtenerProcesoPorPeriodo(dbc, idadm, idProceso, periodo)
		Catch e As SondaException
			Dim sm As New SondaExceptionManager(e)
		Catch e As Exception
			Dim sm As New SondaExceptionManager(e)
		Finally
			dbc.Close()
		End Try
	End Function
	<WebMethod()> Public Function wmObtenerUltimoPeriodoOk(ByVal idadm As Integer, ByVal idProceso As String) As DataSet
		Dim dbc As OraConn
		Dim cp As New controlProcesos()
		Try
			dbc = New OraConn()
			wmObtenerUltimoPeriodoOk = cp.obtenerUltimoPeriodoOk(dbc, idadm, idProceso)
		Catch e As SondaException
			Dim sm As New SondaExceptionManager(e)
		Catch e As Exception
			Dim sm As New SondaExceptionManager(e)
		Finally
			dbc.Close()
		End Try
	End Function
	<WebMethod()> Public Function wmValidacionParaControDeProceso(ByVal idAdm As Integer, ByVal idProceso As String, ByVal idEtapa As String, ByVal periodo As Date, ByVal Frecuencia As String)
		Dim cp As controlProcesos
		Dim ccp As PRO.ccProcesos
		Dim cce As PRO.ccEtapas
		Dim ds As DataSet
		Dim dbc As OraConn
		Dim fecAnterior As Date
		Dim retorno As Integer
		Try

			wmValidacionParaControDeProceso = 0
			dbc = New OraConn()
			'VALIDACION DEL PROCESO PERIODO ACTUAL
			ds = cp.obtenerProcesoPorPeriodo(dbc, idAdm, idProceso, periodo)
			If ds.Tables(0).Rows.Count > 0 Then
				ccp = New PRO.ccProcesos(ds)
				If ccp.estado = 5 Then
					wmValidacionParaControDeProceso = -4
					Exit Function
				End If
			End If

			'VALIDACION DEL PROCESO PERIODO ANTERIOR
			Select Case Frecuencia
				Case "M" : fecAnterior = periodo.AddMonths(-1)
				Case "D" : fecAnterior = Fecha.contarhabiles(dbc, periodo, -2)
			End Select

			If Frecuencia <> "N" Then
				ds = cp.obtenerUltimoPeriodoOk(dbc, idAdm, idProceso)
				If ds.Tables(0).Rows.Count > 0 Then
					ccp = New PRO.ccProcesos(ds)
					If fecAnterior <> ccp.perPeriodo Then
						wmValidacionParaControDeProceso = -3				   ' Proceso anterior no ha sido cerrado
						Exit Function
					End If
				End If
			End If

			'VALIDACION DE LA ETAPA ANTERIOR DEL PROCESO (SI ES QUE TIENE UNA ANTERIOR)

			If idEtapa <> "ETAPA1" Then
				Dim idEtapaAnt As String
				idEtapaAnt = "ETAPA" & CStr(Val(idEtapa.Substring(idEtapa.Length - 1)) - 1)
				ds = cp.obtenerEtapaPorPeriodo(dbc, idAdm, idProceso, idEtapaAnt, periodo)
				If ds.Tables(0).Rows.Count > 0 Then
					cce = New PRO.ccEtapas(ds)
					If cce.estado <> 5 Then
						wmValidacionParaControDeProceso = -2				   'Etapa anterior no está concluida
						Exit Function
					End If
				Else
					wmValidacionParaControDeProceso = -2				'Etapa anterior no está concluida
					Exit Function
				End If
			End If

			'VALIDACION DEL ESTADO ACTUAL DE LA ETAPA
			ds = cp.obtenerEtapaPorPeriodo(dbc, idAdm, idProceso, idEtapa, periodo)

			If ds.Tables(0).Rows.Count > 0 Then
				cce = New PRO.ccEtapas(ds)
				wmValidacionParaControDeProceso = cce.estado
			Else
				wmValidacionParaControDeProceso = -1
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
	End Function

	'DAB 23/10/2019 CA-3341518 PLI-2019035878 INI
	<WebMethod()> Public Function wmObtenerProcesosTodos(ByVal idadm As Integer, ByVal idProceso As String, ByVal idEtapa As String, ByVal periodo As Object) As DataSet
		Dim dbc As OraConn
		Dim cp As New controlProcesos()
		Try
			dbc = New OraConn()
			wmObtenerProcesosTodos = cp.wmObtenerProcesosTodos(dbc, idadm, idProceso, idEtapa, periodo)
		Catch e As SondaException
			Dim sm As New SondaExceptionManager(e)
		Catch e As Exception
			Dim sm As New SondaExceptionManager(e)
		Finally
			dbc.Close()
		End Try
	End Function
	'DAB 23/10/2019 CA-3341518 PLI-2019035878 FIN

	Public Function wmObtenerProcesoComision(ByVal idadm As Integer, ByVal idProceso As String, ByVal periodo As Date) As DataSet
		Dim dbc As OraConn
		Dim cp As New controlProcesos()
		Try
			dbc = New OraConn()
			wmObtenerProcesoComision = cp.obtenerProcesosComision(dbc, idadm, idProceso, periodo)

		Catch e As SondaException
			Dim sm As New SondaExceptionManager(e)
		Catch e As Exception
			Dim sm As New SondaExceptionManager(e)
		Finally
			dbc.Close()
		End Try
	End Function

End Class
