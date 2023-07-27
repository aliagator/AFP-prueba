<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ucDibujaProyecto.ascx.vb" Inherits="Sonda.Net.Nxt.Workflow.WEB.ucDibujaProyecto" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<TABLE id="Table1" style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="300">
	<TR>
		<TD class="tit_left"></TD>
		<TD class="tit_center">
			<snt:label id="lbltitulo" runat="server">Proyecto</snt:label></TD>
		<TD class="tit_right"></TD>
	</TR>
</TABLE>
<TABLE class="TableContenido1" cellSpacing="0" cellPadding="0" width="98%" border="0">
	<TR>
		<td>
			<snt:tabstrip id="TabStrip1" TabDefaultStyle="background-color:#000000;font-family:verdana;font-weight:bold;font-size:8pt;color:#ffffff;width:79;height:21;text-align:center"
				TabHoverStyle="background-color:#777777" TabSelectedStyle="background-color:#ffffff;color:#000000"
				runat="server" Tipo="NombreCampo"></snt:tabstrip>
			<snt:Multipage id="Multipage1" runat="server" Tipo="NombreCampo"></snt:Multipage>
		</td>
	</TR>
</TABLE>
