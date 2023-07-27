<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ucDespachoMultiplePopup.ascx.vb" Inherits="Sonda.Net.Nxt.Workflow.WEB.ucDespachoMultiplePopup" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<P>
	<asp:PlaceHolder id="PlaceHolder1" runat="server"></asp:PlaceHolder></P>
<P>
	<asp:Panel id="pnlOK" runat="server" Visible="False">
		<P align="center">
			<snt:Label id="Label1" runat="server" Tipo="NombreCampo" ></snt:Label><BR>
			<snt:Button id="btnCerrar" runat="server" Tipo="CerrarVentana" JavaScriptOnClick="window.close()"></snt:Button></P>
	</asp:Panel>
<P></P>
<P></P>
<P>&nbsp;</P>
