<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ucHistoria.ascx.vb" Inherits="Sonda.Net.Nxt.Workflow.WEB.ucHistoria" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<%@ Register TagPrefix="cc1" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<TABLE id="Table1" style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="300">
	<TR>
		<TD class="tit_left"></TD>
		<TD class="tit_center">
			<snt:label id="Label2" runat="server">Historial del Expediente</snt:label></TD>
		<TD class="tit_right"></TD>
	</TR>
</TABLE>
<TABLE class="TableContenido1" cellSpacing="0" cellPadding="0" width="98%" border="0">
	<TR>
		<td>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD width="25%" class="label">
						<cc1:Label id="lblLlaveExpId" Tipo="NombreCampo" runat="server"> Id.Expediente:</cc1:Label></TD>
					<TD width="75%" class="casilla">
						<cc1:Label id="lblLlaveExpIdText" Tipo="NombreCampo" runat="server"></cc1:Label></TD>
					<TD width="0%" class="label" >
						<cc1:Label id="lblCopiaExpId" Tipo="NombreCampo" runat="server" visible=False> Id.Copia:</cc1:Label></TD>
					<TD width="0%" class="casilla">
						<cc1:Label id="lblCopiaExpIdText" Tipo="NombreCampo" runat="server" visible=False></cc1:Label></TD>
				</TR>
			</TABLE>
			<P></P>
			<cc1:TablaPaginada id="tblHistoria" runat="server" Width="100%">
				<snt:TablaPaginadaColumna Ancho="" Titulo="Per.Origen" ColId="PERORIGEN" NombreCampo="PERORIGEN" />
				<snt:TablaPaginadaColumna Ancho="" Titulo="Rol.Origen" ColId="ROLORIGEN" NombreCampo="ROLORIGEN" />
				<snt:TablaPaginadaColumna Ancho="" Titulo="Titulo Tarea Origen" ColId="TITULOTAREAORIGEN" NombreCampo="TITULOTAREAORIGEN" />
				<snt:TablaPaginadaColumna Ancho="" Titulo="Diag" ColId="DIAGID" NombreCampo="DIAGID" />
				<snt:TablaPaginadaColumna Ancho="" Titulo="Flecha" ColId="FLECHAID" NombreCampo="FLECHAID" />
				<snt:TablaPaginadaColumna Ancho="" Titulo="Per.Destino" ColId="PERDESTINO" NombreCampo="PERDESTINO" />
				<snt:TablaPaginadaColumna Ancho="" Titulo="Rol.Destino" ColId="ROLDESTINO" NombreCampo="ROLDESTINO" />
				<snt:TablaPaginadaColumna Ancho="" Titulo="Titulo Tarea Destino" ColId="TITULOTAREADESTINO" NombreCampo="TITULOTAREADESTINO" />
				<snt:TablaPaginadaColumna Ancho="" Titulo="Fecha" ColId="DFECHA" NombreCampo="DFECHA" Formato="Fecha" FormatoFecha="DDMMAAAAhhmmss" />
				<snt:TablaPaginadaColumna Ancho="" Titulo="Obs." ColId="OBS" NombreCampo="OBS" />
			</cc1:TablaPaginada>
			<P>
				<TABLE cellSpacing="1" cellPadding="1" width="100%" border="0">
					<TR>
						<TD style="WIDTH: 434px">
							<cc1:Label id="lblMensaje" Tipo="NotaMediana" runat="server"></cc1:Label>
						<TD align="right">
							<cc1:Button id="BtnVolver" Tipo="Volver" CssClass="btnVolver" runat="server"></cc1:Button>
							<cc1:Button id="btnCerrar" Tipo="CerrarVentana" runat="server" JavaScriptOnClick="windows.close();return true;"
								CssClass="btnCerrar"></cc1:Button>
						</TD>
		</td>
	</TR>
</TABLE>
</P> </td> </TR> </TABLE>
