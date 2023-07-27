<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ucAtributo.ascx.vb" Inherits="Sonda.Net.Nxt.Workflow.WEB.ucAtributo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	<TR>
		<TD width="140"><snt:textboxadv id="txtNombre" width="140px" Tipo="NombreCampo" runat="server" Requerido="True"></snt:textboxadv></TD>
		<TD width="100"><snt:dropdownlist id="ddlTipo" width="100px" Tipo="NombreCampo" runat="server" AutoPostBack="True">
				<asp:ListItem Value="1">Texto</asp:ListItem>
				<asp:ListItem Value="2">Numerico</asp:ListItem>
				<asp:ListItem Value="3">Fecha</asp:ListItem>
			</snt:dropdownlist></TD>
		<TD width="15"></TD>
		<TD align="left"><snt:fechaadv id="fecha" Tipo="NombreCampo" runat="server" Requerido="True" Visible="False" Width="100%"></snt:fechaadv><snt:numero id="numerico" Tipo="NombreCampo" runat="server" Visible="False" Width="90%"></snt:numero><snt:textboxadv id="texto" Tipo="NombreCampo" runat="server" Requerido="True" TipoCase="TextoLibre"
				Width="90%"></snt:textboxadv></TD>
		<TD width="80"><snt:button id="Button1" Tipo="Eliminar" runat="server" CssClass="btnEliminar" CausesValidation="False"></snt:button></TD>
	</TR>
</TABLE>
