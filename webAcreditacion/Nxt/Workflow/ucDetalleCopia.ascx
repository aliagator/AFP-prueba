<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ucDetalleCopia.ascx.vb" Inherits="Sonda.Net.Nxt.Workflow.WEB.ucDetalleCopia" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<table cellSpacing="0" cellPadding="0" align="center" border="0" width="100%">
	<tr>
		<td colSpan="3">
			<table class="tablatit" cellSpacing="0" cellPadding="0" width="95%">
				<tr>
					<td class="titppal"></td>
					<td class="titppal2">
						<snt:Label id="Label1" runat="server" cssclass="titppal2" Tipo="Titulo">Detalle de la Copia</snt:Label></td>
					<td class="titppal3"></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td class="C_left" width="13"></td>
		<td class="C_center" width="424"></td>
		<td class="C_right" width="13"></td>
	</tr>
	<tr>
		<td class="C_left2" width="13"></td>
		<td>
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD width="30%" class="label">
						<snt:Label id="Label2" runat="server" Tipo="NombreCampo">Proy.Id</snt:Label></TD>
					<TD width="70%" class="casilla">
						<snt:Label id="lblProyId" runat="server" Tipo="NombreCampo"></snt:Label></TD>
				</TR>
				<TR>
					<TD class="label">
						<snt:Label id="Label3" runat="server" Tipo="NombreCampo">Versión</snt:Label></TD>
					<TD class="casilla">
						<snt:Label id="lblVersionId" runat="server" Tipo="NombreCampo"></snt:Label></TD>
				</TR>
				<TR>
					<TD class="label">
						<snt:Label id="Label4" runat="server" Tipo="NombreCampo">Llave</snt:Label></TD>
					<TD class="casilla">
						<snt:Label id="lblLlaveexpid" runat="server" Tipo="NombreCampo"></snt:Label></TD>
				</TR>
				<TR>
					<TD class="label">
						<snt:Label id="Label5" Tipo="NombreCampo" runat="server">Copia</snt:Label></TD>
					<TD class="casilla">
						<snt:Label id="lblCopiaExpId" runat="server" Tipo="NombreCampo"></snt:Label></TD>
				</TR>
				<TR>
					<TD class="label">
						<snt:Label id="Label6" Tipo="NombreCampo" runat="server">Descripción</snt:Label></TD>
					<TD class="casilla">
						<snt:Label id="lblDescripcion" runat="server" Tipo="NombreCampo"></snt:Label></TD>
				</TR>
				<TR>
					<TD class="label">
						<snt:Label id="Label7" Tipo="NombreCampo" runat="server">Carpeta</snt:Label></TD>
					<TD class="casilla">
						<snt:Label id="lblCarpetaActual" runat="server" Tipo="NombreCampo"></snt:Label></TD>
				</TR>
				<TR>
					<TD class="label">
						<snt:Label id="Label8" Tipo="NombreCampo" runat="server">Tarea</snt:Label></TD>
					<TD class="casilla">
						<snt:Label id="lblTareaId" runat="server" Tipo="NombreCampo" width="30%"></snt:Label><snt:Label id="lblTituloTarea" runat="server" Tipo="NombreCampo"></snt:Label></TD>
				</TR>
				<TR>
					<TD class="label" style="HEIGHT: 18px">
						<snt:Label id="Label9" Tipo="NombreCampo" runat="server">Asignada</snt:Label></TD>
					<TD class="casilla" style="HEIGHT: 18px">
						<asp:CheckBox id="chkAsignada" runat="server" Enabled="False"></asp:CheckBox></TD>
				</TR>
				<TR>
					<TD class="label">
						<snt:Label id="Label10" Tipo="NombreCampo" runat="server">Despachable</snt:Label></TD>
					<TD class="casilla">
						<asp:CheckBox id="chkdespachable" runat="server" Enabled="False"></asp:CheckBox></TD>
				</TR>
				<TR>
					<TD class="label">
						<snt:Label id="Label11" Tipo="NombreCampo" runat="server">Fecha</snt:Label></TD>
					<TD class="casilla">
						<snt:Label id="lblFecha" runat="server" Tipo="NombreCampo"></snt:Label></TD>
				</TR>
				<TR>
					<TD class="label">
						<snt:Label id="Label12" Tipo="NombreCampo" runat="server">Pendiente por Despachar</snt:Label></TD>
					<TD class="casilla">
						<asp:CheckBox id="chkPendPorDesp" runat="server" Enabled="False"></asp:CheckBox></TD>
				</TR>
				<TR>
					<TD class="label">
						<snt:Label id="Label13" Tipo="NombreCampo" runat="server">Observacion</snt:Label></TD>
					<TD class="casilla">
						<snt:Label id="lblObs" runat="server" Tipo="NombreCampo"></snt:Label></TD>
				</TR>
				<TR>
					<TD class="label"></TD>
					<TD class="casilla"></TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<P align="right">
							<snt:Button id="btnDespachar" Tipo="Despachar" runat="server" CssClass="btnDespachar" Visible="False"></snt:Button>
							<snt:Button id="btnVerAtributos" Tipo="Ver" runat="server" CssClass="btnVer" Etiqueta="Atributos"></snt:Button>
							<snt:Button id="btnVerHistoria" Tipo="Ver" runat="server" Etiqueta="Historia" CssClass="btnVer"></snt:Button></P>
					</TD>
				</TR>
			</TABLE>
		</td>
		<td class="C_right2" width="13"></td>
	</tr>
	<tr>
		<td class="C_left3" width="13"></td>
		<td class="C_bottom" width="424"></td>
		<td class="C_right3" width="13"></td>
	</tr>
</table>
<snt:HyperLink id="HyperLink1" runat="server" NuevaVentana="True" PaginaContenedora="defaultblank.aspx"
	Modal="True" AltoVentana="200" AnchoVentana="500" ScrollVentana="True"></snt:HyperLink>
