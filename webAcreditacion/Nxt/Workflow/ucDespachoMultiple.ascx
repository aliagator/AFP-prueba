<%@ Register TagPrefix="uc1" TagName="ucPendientesIncrustado" Src="ucPendientesIncrustado.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ucDespachoMultiple.ascx.vb" Inherits="Sonda.Net.Nxt.Workflow.WEB.ucDespachoMultiple" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<uc1:ucpendientesincrustado id="UcPendientesIncrustado1" runat="server"></uc1:ucpendientesincrustado><BR>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
	<TR>
		<TD align="right"><snt:button id="btnDespachar" runat="server" Tipo="Despachar" ConfirmarAccion="True" MensajeConfirmacion="Va a realizar un despacho múltiple ¿Esta usted seguro?"></snt:button></TD>
	</TR>
</TABLE>
<snt:HyperLink id="hlnkDespachar" runat="server" UserControl="nxt/workflow/ucDespachoMultiplePopup"
	AutoPostBackOnClose="True" NombreVentana="ucDespachoMultiplePopup" NuevaVentana="True" Modal="True"
	PaginaContenedora="defaultblank.aspx" AltoVentana="600" AnchoVentana="500"></snt:HyperLink>
