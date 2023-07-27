<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ucEspereUnMomento.ascx.vb" Inherits="Sonda.Net.Nxt.Web.ucEspereUnMomento" TargetSchema="http://schemas.microsoft.com/intellisense/ie3-2nav3-0" %>
<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<table cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
	<tr>
		<td colSpan="3">
			<table class="tablatit" cellSpacing="0" cellPadding="0" width="95%">
				<tr>
					<td class="titppal"></td>
					<td class="titppal2"><snt:label id="Label2" runat="server" cssclass="titppal2" Tipo="NombreCampo">Estado del proceso</snt:label></td>
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
			<TABLE id="tblEspere" cellSpacing="0" cellPadding="0" border="0" align="center" runat="server">
				<TR valign="middle">
					<TD><snt:Image id="Image1" Tipo="NombreCampo" runat="server" ImageUrl="~/img/sondanet/processing.gif"></snt:Image></TD>
					<TD><snt:label id="Label1" Tipo="SubTitulo" runat="server">Espere un momento mientras se ejecuta el proceso...</snt:label></TD>
				</TR>
			</TABLE>
			<TABLE id="tblError" cellSpacing="0" cellPadding="0" align="center" border="0" runat="server">
				<TR vAlign="middle">
					<TD><snt:Image id="Image2" Tipo="NombreCampo" runat="server" ImageUrl="~/img/severidad/sev3.gif"></snt:Image></TD>
					<TD><snt:label id="Label3" runat="server" Tipo="SubTitulo">El proceso no ha finalizado correctamente, revise los mensajes para determinar el error.</snt:label></TD>
				</TR>
			</TABLE>
			<P align="center">
				<snt:timer id="Timer1" runat="server" Segundos="8"></snt:timer>
				<asp:PlaceHolder id="MENSAJESWEBNXTPLACEHOLDER" runat="server"></asp:PlaceHolder></P>
		</td>
		<td class="C_right2" width="13"></td>
	</tr>
	<tr>
		<td class="C_left3" width="13"></td>
		<td class="C_bottom" width="424"></td>
		<td class="C_right3" width="13"></td>
	</tr>
</table>
<snt:TextBox id="txtVolver" Tipo="NombreCampo" runat="server" GuardarEnHistoria="True" Visible="False"></snt:TextBox>
