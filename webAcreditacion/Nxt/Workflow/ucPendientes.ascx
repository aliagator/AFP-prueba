<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ucPendientes.ascx.vb" Inherits="Sonda.Net.Nxt.Workflow.WEB.ucPendientes" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="ucPendientesIncrustado" Src="ucPendientesIncrustado.ascx" %>
<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<table cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
	<tr>
		<td colSpan="3">
			<table class="tablatit" cellSpacing="0" cellPadding="0" width="95%">
				<tr>
					<td class="titppal"></td>
					<td class="titppal2"><snt:label id="Label1" cssclass="titppal2" runat="server">Consulta de Pendientes por Usuario</snt:label></td>
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
		<uc1:ucPendientesIncrustado id="UcPendientesIncrustado1" runat="server"></uc1:ucPendientesIncrustado>
		</td>
		<td class="C_right2" width="13"></td>
	</tr>
	<tr>
		<td class="C_left3" width="13"></td>
		<td class="C_bottom" width="424"></td>
		<td class="C_right3" width="13"></td>
	</tr>
</table>
</TD></TR></TBODY></TABLE>
