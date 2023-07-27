<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ucDiagrama.ascx.vb" Inherits="Sonda.Net.Nxt.Workflow.WEB.ucDiagrama" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="300">
	<TR>
		<TD class="tit_left"></TD>
		<TD class="tit_center">
			<snt:label id="lbltitulo" runat="server">Diagrama</snt:label></TD>
		<TD class="tit_right"></TD>
	</TR>
</TABLE>
<TABLE class="TableContenido1" cellSpacing="0" cellPadding="0" width="98%" border="0">
	<TR>
		<td>
			<asp:PlaceHolder id="PlaceHolder1" runat="server"></asp:PlaceHolder>
		</td>
	</TR>
</TABLE>
