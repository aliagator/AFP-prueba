Imports System.Web.Services
Imports Sonda.Net.DB
Imports Sonda.Gestion.Adm.Sys.CodeCompletion
Imports Sonda.Gestion.Adm.Sys.IngresoEgreso
Imports Sonda.Net


<WebService(Namespace := "http://tempuri.org/")> _
Public Class wsINEReintegro
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

    '-----------------------------------------------------------------------
    ' Archivo generado automáticamente.
    ' Ruta Archivo  : C:\TEMP\sysIngresoEgreso.dll_705547
    ' Full Namespace: Sonda.Gestion.Adm.Sys.IngresoEgreso.Reintegro
    '
    ' Fecha: 24/01/2005 19:33:37
    ' Generador de código implementado por Luis Lillo Armijo.
    ' Versión del generador: 1.0.735 (Beta)
    '-----------------------------------------------------------------------


    <WebMethod()> Public Function wmbuscar(ByVal idAdm As Integer, ByVal seqCheque As Integer) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            wmbuscar = RequerimientoPago.buscar(dbc, idAdm, seqCheque)

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
    <WebMethod()> Public Function wmbuscarCheques(ByVal idAdm As Integer, ByVal codOrigenProceso As String, ByVal estadoCheque As String, ByVal dias As Decimal) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            wmbuscarCheques = RequerimientoPago.buscarCheques(dbc, idAdm, codOrigenProceso, estadoCheque, dias)

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
    <WebMethod()> Public Sub wmcrear(ByVal idAdm As Integer, ByVal seqCheque As Integer, ByVal tipoContabilidad As String, ByVal idCliente As Integer, ByVal numReferenciaOrigen1 As Integer, ByVal numReferenciaOrigen2 As Integer, ByVal codOrigenProceso As String, ByVal fecOpeContab As Date, ByVal codCuentaPasivo As Integer, ByVal codCuentaBanco As Integer, ByVal numCheque As Integer, ByVal nomBanco As String, ByVal tipoBeneficiario As String, ByVal idBeneficiarioCheque As String, ByVal nombreBeneficiarioCheque As String, ByVal valMlFondo As Decimal, ByVal valMlAdministradora As Decimal, ByVal valMlOtrosAportes As Decimal, ByVal tipoPago As String, ByVal fecEmisionCheque As Date, ByVal fecVctoCheque As Date, ByVal fecCaducidadCheque As Date, ByVal fecReintegroCheque As Date, ByVal idUsuarioIngCheque As String, ByVal codAgencia As Integer, ByVal estadoCheque As String, ByVal indFin700 As String, ByVal fecEstadoCheque As Date, ByVal usu As String, ByVal fun As String)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            RequerimientoPago.crear(dbc, idAdm, seqCheque, tipoContabilidad, idCliente, numReferenciaOrigen1, numReferenciaOrigen2, codOrigenProceso, fecOpeContab, codCuentaPasivo, codCuentaBanco, numCheque, nomBanco, tipoBeneficiario, idBeneficiarioCheque, nombreBeneficiarioCheque, valMlFondo, valMlAdministradora, valMlOtrosAportes, tipoPago, fecEmisionCheque, fecVctoCheque, fecCaducidadCheque, fecReintegroCheque, idUsuarioIngCheque, codAgencia, estadoCheque, indFin700, fecEstadoCheque, usu, fun)

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
    <WebMethod()> Public Function wmcrearConSecuencia(ByVal idAdm As Integer, ByVal tipoContabilidad As String, ByVal idCliente As Integer, ByVal numReferenciaOrigen1 As Integer, ByVal numReferenciaOrigen2 As Integer, ByVal codOrigenProceso As String, ByVal fecOpeContab As Date, ByVal codCuentaPasivo As Integer, ByVal codCuentaBanco As Integer, ByVal numCheque As Integer, ByVal nomBanco As String, ByVal tipoBeneficiario As String, ByVal idBeneficiarioCheque As String, ByVal nombreBeneficiarioCheque As String, ByVal valMlFondo As Decimal, ByVal valMlAdministradora As Decimal, ByVal valMlOtrosAportes As Decimal, ByVal tipoPago As String, ByVal fecEmisionCheque As Date, ByVal fecVctoCheque As Date, ByVal fecCaducidadCheque As Date, ByVal fecReintegroCheque As Date, ByVal idUsuarioIngCheque As String, ByVal codAgencia As Integer, ByVal estadoCheque As String, ByVal indFin700 As String, ByVal fecEstadoCheque As Date, ByVal usu As String, ByVal fun As String) As Integer
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            wmcrearConSecuencia = RequerimientoPago.crearConSecuencia(dbc, idAdm, tipoContabilidad, idCliente, numReferenciaOrigen1, numReferenciaOrigen2, codOrigenProceso, fecOpeContab, codCuentaPasivo, codCuentaBanco, numCheque, nomBanco, tipoBeneficiario, idBeneficiarioCheque, nombreBeneficiarioCheque, valMlFondo, valMlAdministradora, valMlOtrosAportes, tipoPago, fecEmisionCheque, fecVctoCheque, fecCaducidadCheque, fecReintegroCheque, idUsuarioIngCheque, codAgencia, estadoCheque, indFin700, fecEstadoCheque, usu, fun)

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
    <WebMethod()> Public Sub wmeliminar(ByVal idAdm As Integer, ByVal seqCheque As Integer)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            RequerimientoPago.eliminar(dbc, idAdm, seqCheque)

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
    <WebMethod()> Public Sub wmmodEstado(ByVal idAdm As Integer, ByVal seqCheque As Integer, ByVal estadoReg As String, ByVal usu As String, ByVal fun As String)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            RequerimientoPago.modEstado(dbc, idAdm, seqCheque, estadoReg, usu, fun)

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
    <WebMethod()> Public Sub wmmodEstadoCheque(ByVal idAdm As Integer, ByVal seqCheque As Integer, ByVal estadoCheque As String, ByVal usu As String, ByVal fun As String)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            RequerimientoPago.modEstadoCheque(dbc, idAdm, seqCheque, estadoCheque, usu, fun)

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
    <WebMethod()> Public Sub wmmodificar(ByVal idAdm As Integer, ByVal seqCheque As Integer, ByVal tipoContabilidad As String, ByVal idCliente As Integer, ByVal numReferenciaOrigen1 As Integer, ByVal numReferenciaOrigen2 As Integer, ByVal codOrigenProceso As String, ByVal fecOpeContab As Date, ByVal codCuentaPasivo As Integer, ByVal codCuentaBanco As Integer, ByVal numCheque As Integer, ByVal nomBanco As String, ByVal tipoBeneficiario As String, ByVal idBeneficiarioCheque As String, ByVal nombreBeneficiarioCheque As String, ByVal valMlFondo As Decimal, ByVal valMlAdministradora As Decimal, ByVal valMlOtrosAportes As Decimal, ByVal tipoPago As String, ByVal fecEmisionCheque As Date, ByVal fecVctoCheque As Date, ByVal fecCaducidadCheque As Date, ByVal fecReintegroCheque As Date, ByVal idUsuarioIngCheque As String, ByVal codAgencia As Integer, ByVal estadoCheque As String, ByVal indFin700 As String, ByVal fecEstadoCheque As Date, ByVal usu As String, ByVal fun As String)
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            RequerimientoPago.modificar(dbc, idAdm, seqCheque, tipoContabilidad, idCliente, numReferenciaOrigen1, numReferenciaOrigen2, codOrigenProceso, fecOpeContab, codCuentaPasivo, codCuentaBanco, numCheque, nomBanco, tipoBeneficiario, idBeneficiarioCheque, nombreBeneficiarioCheque, valMlFondo, valMlAdministradora, valMlOtrosAportes, tipoPago, fecEmisionCheque, fecVctoCheque, fecCaducidadCheque, fecReintegroCheque, idUsuarioIngCheque, codAgencia, estadoCheque, indFin700, fecEstadoCheque, usu, fun)

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
    <WebMethod()> Public Function wmtraer(ByVal idAdm As Integer, ByVal seqCheque As Integer) As DataSet
        Dim dbc As OraConn
        Try
            dbc = New OraConn()
            dbc.BeginTrans()

            wmtraer = RequerimientoPago.traer(dbc, idAdm, seqCheque)

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


    <WebMethod()> Public Sub wmCrearProcesoAcreditacion(ByVal idAdm As Integer, ByVal ds As DataSet, ByVal usu As String, ByVal fun As String, ByRef numeroId As Integer, ByRef codOrigenProceso As String)
        Dim dbc As OraConn
        Dim i As Integer
        Dim codError, totReg As Integer
        Dim fecCreacion As Date
        Dim dsPar As DataSet

        Dim IndDevComision As String
        Dim IndDevImpuesto As String


        Try
            dbc = New OraConn()
            dbc.BeginTrans()
            dsPar = Sys.Soporte.Parametro.traerGlobal(dbc, "PAR_ACR_ORIGEN_REINTEGRO", New Object() {idAdm, ds.Tables(0).Rows(0).Item("COD_ORIGEN_PROCESO")})

            IndDevComision = dsPar.Tables(0).Rows(0).Item("IND_DEV_COMISION")
            IndDevImpuesto = dsPar.Tables(0).Rows(0).Item("IND_DEV_IMPUESTO")
            codOrigenProceso = dsPar.Tables(0).Rows(0).Item("COD_ORIGEN_REINTEGRO")

            INEControlAcr.crearProcesoAcreditacion(idAdm, codOrigenProceso, usu, 0, 0, "MSI", usu, fun, numeroId, fecCreacion, codError)
            If codError <> 0 Then
                Throw New SondaException(codError)
            End If
            For i = 0 To ds.Tables(0).Rows.Count - 1

                crearTransaccionesReintegro(dbc, idAdm, numeroId, usu, codOrigenProceso, ds.Tables(0).Rows(i).Item("NUMERO_ID"), ds.Tables(0).Rows(i).Item("COD_ORIGEN_PROCESO"), ds.Tables(0).Rows(i).Item("NUM_REFERENCIA_ORIGEN1"), ds.Tables(0).Rows(i).Item("NUM_REFERENCIA_ORIGEN2"), ds.Tables(0).Rows(i).Item("TIPO_CONTABILIDAD"), ds.Tables(0).Rows(i).Item("VAL_ML_CHEQUE"), IndDevComision, IndDevImpuesto, usu, fun, codError)
                If codError <> 0 Then
                    RequerimientoPago.modEstadoCheque(dbc, idAdm, ds.Tables(0).Rows(i).Item("SEQ_CHEQUE"), "ERE", usu, fun)
                    codError = 0
                    dbc.Rollback()
                    dbc.BeginTrans()
                Else
                    RequerimientoPago.modEstadoCheque(dbc, idAdm, ds.Tables(0).Rows(i).Item("SEQ_CHEQUE"), "PRE", usu, fun)
                    dbc.Commit()
                    dbc.BeginTrans()
                End If

            Next
            dbc.Commit()

            INEControlAcr.CerrarProcesoAcreditacion(idAdm, codOrigenProceso, usu, numeroId, usu, fun, totReg, fecCreacion, codError)
            If codError <> 0 Then
                Throw New SondaException(codError)
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
    Private Sub crearTransaccionesReintegro(ByVal dbc As OraConn, ByVal idAdm As Integer, ByVal numeroId As Integer, ByVal idUsuario As String, ByVal codOrigenProceso As String, ByVal numeroIdOrigen As Integer, ByVal codOrigenProcesoCheque As String, ByVal numReferenciaOrigen1 As Long, ByVal numReferenciaOrigen2 As Long, ByVal tipoFondo As String, ByVal valMlMontoCheque As Decimal, ByVal IndDevComision As String, ByVal IndDevImpuesto As String, ByVal usu As String, ByVal fun As String, ByRef codError As String)
        Dim ds, dsAux As DataSet
        Dim i As Integer
        Dim rTrn As ACR.ccTransacciones
        Dim seqRegistro As Integer
        Dim totalReintegro As Decimal = 0
        Dim codMvtoReversa As String

        ds = Transacciones.buscarParaReintegro(dbc, idAdm, codOrigenProcesoCheque, numeroIdOrigen, numReferenciaOrigen1, numReferenciaOrigen2, tipoFondo)
        If ds.Tables(0).Rows.Count = 0 Then
            codError = -1
            Throw New Exception("NO SE ENCONTRARON DATOS PARA REALIZAR EL REINTEGRO")
        End If

        For i = 0 To ds.Tables(0).Rows.Count - 1

            rTrn = New ACR.ccTransacciones(ds.Tables(0).Rows(i))
            If rTrn.codOrigenProceso = "DEVEXEMP" Then ' Abono a RND

                If rTrn.valCuoMvtoCal > 0 Then
                    '--.--cn2
                    INEControlAcr.crearTransacciones(dbc, idAdm, codOrigenProceso, usu, numeroId, rTrn.idPersona, rTrn.idCliente, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.numReferenciaOrigen1, rTrn.numReferenciaOrigen2, 0, 0, 0, 0, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, rTrn.numReferenciaOrigen1, 0, 0, Nothing, rTrn.tipoProducto, rTrn.tipoFondoDestinoCal, rTrn.tipoFondoDestinoCal, rTrn.categoria, rTrn.subCategoria, rTrn.codRegTributario, "ABO", "RND", "RND", Nothing, 0, Nothing, codOrigenProceso, rTrn.codMvto, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.tipoRezago, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.valMlMontoNominal, rTrn.valMlMvtoCal, Nothing, Nothing, rTrn.valCuoMvtoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.tipoComisionPorcentual, 0, 0, 0, rTrn.tipoComisionFija, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, "CUO", "N", "N", "S", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, 0, 0, 0, 0, 0, 0, usu, fun, seqRegistro, codError)
                    If codError <> 0 Then
                        Exit Sub
                    End If
                End If

            Else

                DeterminarTipoMovimiento(dbc, idAdm, rTrn, IndDevImpuesto, IndDevComision, totalReintegro, codMvtoReversa)

                dsAux = InformacionCliente.traerTipoProductoVig(dbc, idAdm, rTrn.idCliente, rTrn.tipoProducto)

                If dsAux.Tables(0).Rows.Count = 0 Then
                    rTrn.codDestinoTransaccion = "REZ"
                    rTrn.codCausalRezago = "25" ' POR AHORA
                Else
                    rTrn.codDestinoTransaccion = "CTA"
                    rTrn.codCausalRezago = Nothing
                End If

                If rTrn.valCuoMvtoCal > 0 Then
                    rTrn.codMvto = codMvtoReversa
                    INEControlAcr.crearTransacciones(dbc, idAdm, codOrigenProceso, usu, numeroId, rTrn.idPersona, rTrn.idCliente, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.numReferenciaOrigen1, rTrn.numReferenciaOrigen2, 0, 0, 0, 0, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, rTrn.numReferenciaOrigen1, 0, 0, Nothing, rTrn.tipoProducto, rTrn.tipoFondoDestinoCal, rTrn.tipoFondoDestinoCal, rTrn.categoria, rTrn.subCategoria, rTrn.codRegTributario, "ABO", rTrn.codDestinoTransaccion, rTrn.codDestinoTransaccion, Nothing, 0, Nothing, codOrigenProceso, rTrn.codMvto, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.codCausalRezago, rTrn.tipoRezago, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.valMlMontoNominal, rTrn.valMlMvtoCal, Nothing, Nothing, rTrn.valCuoMvtoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.tipoComisionPorcentual, 0, 0, 0, rTrn.tipoComisionFija, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, "CUO", "N", "N", "S", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, 0, 0, 0, 0, 0, 0, usu, fun, seqRegistro, codError)
                    If codError <> 0 Then
                        Exit Sub
                    End If
                End If


                If rTrn.valCuoComisPorcentualCal > 0 And IndDevComision = "S" Then

                    rTrn.codMvto = codMvtoReversa
                    rTrn.valCuoMvtoCal = rTrn.valCuoComisPorcentualCal
                    INEControlAcr.crearTransacciones(dbc, idAdm, codOrigenProceso, usu, numeroId, rTrn.idPersona, rTrn.idCliente, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.numReferenciaOrigen1, rTrn.numReferenciaOrigen2, 0, 0, 0, 0, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, rTrn.numReferenciaOrigen1, 0, 0, Nothing, rTrn.tipoProducto, rTrn.tipoFondoDestinoCal, rTrn.tipoFondoDestinoCal, rTrn.categoria, rTrn.subCategoria, rTrn.codRegTributario, "ABO", rTrn.codDestinoTransaccion, rTrn.codDestinoTransaccion, Nothing, 0, Nothing, codOrigenProceso, rTrn.codMvto, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.codCausalRezago, rTrn.tipoRezago, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.valMlMontoNominal, rTrn.valMlMvtoCal, Nothing, Nothing, rTrn.valCuoMvtoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.tipoComisionPorcentual, 0, 0, 0, rTrn.tipoComisionFija, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, "CUO", "N", "N", "S", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, 0, 0, 0, 0, 0, 0, usu, fun, seqRegistro, codError)
                    If codError <> 0 Then
                        Exit Sub
                    End If

                    INEControlAcr.crearTransacciones(dbc, idAdm, codOrigenProceso, usu, numeroId, rTrn.idPersona, rTrn.idCliente, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.numReferenciaOrigen1, rTrn.numReferenciaOrigen2, 0, 0, 0, 0, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, rTrn.numReferenciaOrigen1, 0, 0, Nothing, "AD01", rTrn.tipoFondoDestinoCal, rTrn.tipoFondoDestinoCal, "AD01", rTrn.subCategoria, rTrn.codRegTributario, "CAR", "AD01", "CTA", Nothing, 0, Nothing, codOrigenProceso, rTrn.codMvto, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.tipoRezago, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.valMlMontoNominal, rTrn.valMlMvtoCal, Nothing, Nothing, rTrn.valCuoMvtoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.tipoComisionPorcentual, 0, 0, 0, rTrn.tipoComisionFija, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, "CUO", "N", "N", "S", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, 0, 0, 0, 0, 0, 0, usu, fun, seqRegistro, codError)
                    If codError <> 0 Then
                        Exit Sub
                    End If
                End If


                If rTrn.valCuoComisFijaCal > 0 And IndDevComision = "S" Then
                    rTrn.codMvto = codMvtoReversa
                    rTrn.valCuoMvtoCal = rTrn.valCuoComisFijaCal
                    INEControlAcr.crearTransacciones(dbc, idAdm, codOrigenProceso, usu, numeroId, rTrn.idPersona, rTrn.idCliente, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.numReferenciaOrigen1, rTrn.numReferenciaOrigen2, 0, 0, 0, 0, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, rTrn.numReferenciaOrigen1, 0, 0, Nothing, rTrn.tipoProducto, rTrn.tipoFondoDestinoCal, rTrn.tipoFondoDestinoCal, rTrn.categoria, rTrn.subCategoria, rTrn.codRegTributario, "ABO", rTrn.codDestinoTransaccion, rTrn.codDestinoTransaccion, Nothing, 0, Nothing, codOrigenProceso, rTrn.codMvto, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.codCausalRezago, rTrn.tipoRezago, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.valMlMontoNominal, rTrn.valMlMvtoCal, Nothing, Nothing, rTrn.valCuoMvtoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.tipoComisionPorcentual, 0, 0, 0, rTrn.tipoComisionFija, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, "CUO", "N", "N", "S", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, 0, 0, 0, 0, 0, 0, usu, fun, seqRegistro, codError)
                    If codError <> 0 Then
                        Exit Sub
                    End If
                    INEControlAcr.crearTransacciones(dbc, idAdm, codOrigenProceso, usu, numeroId, rTrn.idPersona, rTrn.idCliente, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.numReferenciaOrigen1, rTrn.numReferenciaOrigen2, 0, 0, 0, 0, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, rTrn.numReferenciaOrigen1, 0, 0, Nothing, "AD01", rTrn.tipoFondoDestinoCal, rTrn.tipoFondoDestinoCal, "AD01", rTrn.subCategoria, rTrn.codRegTributario, "CAR", "AD01", "CTA", Nothing, 0, Nothing, codOrigenProceso, rTrn.codMvto, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.tipoRezago, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.valMlMontoNominal, rTrn.valMlMvtoCal, Nothing, Nothing, rTrn.valCuoMvtoCal, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, rTrn.tipoComisionPorcentual, 0, 0, 0, rTrn.tipoComisionFija, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, Nothing, Nothing, Nothing, "CUO", "N", "N", "S", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, 0, 0, 0, 0, 0, 0, 0, 0, usu, fun, seqRegistro, codError)
                    If codError <> 0 Then
                        Exit Sub
                    End If
                End If
            End If

        Next
        If totalReintegro <> valMlMontoCheque Then
            codError = 15354
        End If


    End Sub
    Private Function obtenerCodMvtoReversa(ByVal codMvto As String, ByVal tipoMvto As String) As String
        Dim st1, st2, st3 As String

        st1 = codMvto.Substring(0, 1)
        st2 = codMvto.Substring(1, 1)
        st3 = codMvto.Substring(2)

        st2 = IIf(st2 = "1", "2", "1")

        obtenerCodMvtoReversa = st1 & st2 & st3

    End Function

    Private Sub DeterminarTipoMovimiento(ByRef dbc As OraConn, ByVal idAdm As Integer, ByVal rTrn As ACR.ccTransacciones, ByVal IndDevImpuesto As String, ByVal IndDevcomision As String, ByRef totalReintegro As Decimal, ByRef codMvtoReversa As String)
        Dim parMov As New INEParametrosDS.ParametroGeneral("PAR_ACR_MVTO_ACREDITACION")
        Dim dsPar As DataSet
        Dim rTmov As PAR.ccAcrMvtoAcreditacion

        dsPar = parMov.traer(dbc, New Object() {"VID_ADM", "VCOD_MVTO"}, New Object() {idAdm, rTrn.codMvto}, New Object() {"INTEGER", "STRING"})
        rTmov = New PAR.ccAcrMvtoAcreditacion(dsPar)

        Select Case rTmov.tipoMvto
            Case "IMP"
                If IndDevImpuesto = "N" Then
                    rTrn.valCuoMvtoCal = 0
                End If
            Case "COM"
                If IndDevcomision = "N" Then
                    rTrn.valCuoMvtoCal = 0
                End If
            Case Else
                totalReintegro += rTrn.valMlMvtoCal

        End Select
        codMvtoReversa = obtenerCodMvtoReversa(rTrn.codMvto, rTmov.tipoMvto)
    End Sub

End Class
