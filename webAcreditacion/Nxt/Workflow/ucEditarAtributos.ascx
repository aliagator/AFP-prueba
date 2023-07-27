<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ucEditarAtributos.ascx.vb" Inherits="Sonda.Net.Nxt.Workflow.WEB.ucEditarAtributos" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="300">
	<TR>
		<TD class="tit_left"></TD>
		<TD class="tit_center">
			<asp:label id="Label2" runat="server">Atributos</asp:label></TD>
		<TD class="tit_right"></TD>
	</TR>
</TABLE>
<TABLE class="TableContenido1" id="Table2" cellSpacing="0" cellPadding="0" width="98%"
	border="0">
	<TR>
		<td>
			<P align="left">
				<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
					<TR>
						<TD width="140">
							<snt:Label id="Label1" runat="server" Tipo="NombreCampo">Nombre</snt:Label></TD>
						<TD width="100">
							<snt:Label id="Label3" runat="server" Tipo="NombreCampo">Tipo</snt:Label></TD>
						<TD width="15"></TD>
						<TD align="left">
							<snt:Label id="Label4" runat="server" Tipo="NombreCampo">Valor</snt:Label></TD>
						<TD width="80"></TD>
					</TR>
				</TABLE>
				<asp:PlaceHolder id="PHAtributos" runat="server"></asp:PlaceHolder></P>
		</td>
	</TR>
</TABLE>
<P align="right">
	<snt:Button id="btnGuardar" runat="server" Tipo="Guardar" CssClass="btnGrabar"></snt:Button>
	<snt:Button id="btnNuevo" runat="server" Tipo="Nuevo" CausesValidation="False" CssClass="btnNuevo"></snt:Button>
	<snt:Button id="btnCerrarVentana" runat="server" Tipo="CerrarVentana" JavaScriptOnClick="windows.close();return false;"
		CausesValidation="False" CssClass="btnCerrar"></snt:Button></P>
