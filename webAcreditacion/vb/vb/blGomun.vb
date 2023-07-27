Imports System
Imports System.IO
Imports Sonda.Net
Imports Sonda.Net.DB

Public Class blGomun

	Public Function traerProcesos(ByVal idAdm As Integer, ByVal idProceso As String) As DataSet
		Dim dbc As OraConn
		Dim ds As New DataSet()
		Dim dsAux As DataSet
		Dim dr() As DataRow
		Dim I As Integer = 0

		Dim DT As DataTable


		Try
			dbc = New OraConn()
			dbc.BeginTrans()

			ds = Sys.Soporte.Procesos.buscarProcesos(dbc, idAdm, idProceso.ToString, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

			If ds.Tables(0).Rows.Count < 10 Then
				traerProcesos = ds
			Else

				Try
					DT = ds.Tables(0)
					DT.DefaultView.Sort = "SEQ_PROCESO DESC"
					For I = 0 To DT.DefaultView.Table.Rows.Count - 1
						If I >= 10 Then
							DT.DefaultView.Table.Rows(I).Delete()
						End If
					Next

					ds.Tables.RemoveAt(0)
					ds.Tables.Add(DT.DefaultView.Table)
					ds.AcceptChanges()
					traerProcesos = ds
				Catch
					traerProcesos = ds
				End Try
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

End Class
