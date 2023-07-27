<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ucPendientesPorProyecto.ascx.vb" Inherits="Sonda.Net.Nxt.Workflow.WEB.ucPendientesPorProyecto" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<table cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
	<tr>
		<td colSpan="3">
			<table class="tablatit" cellSpacing="0" cellPadding="0" width="95%">
				<tr>
					<td class="titppal"></td>
					<td class="titppal2"><snt:label id="Label1" runat="server" cssclass="titppal2">Consulta de pendientes agrupados por Proyecto</snt:label></td>
					<td class="titppal3"></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td class="C_left" width="13"></td>
		<td class="C_center" ></td>
		<td class="C_right" width="13"></td>
	</tr>
	<tr> 
		<td class="C_left2" width="13"></td>
		<td>
			<table class="tablatit" cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td width=25%><iframe name="iframe_treeview" HEIGHT=500  width=100% src="defaultblank.aspx?url=Nxt/Workflow/ucPendientesPorProyectoTreeview"></iframe>
					</td>
					<td>
					<iframe name="iframe_contenido" HEIGHT=500  width=100% src="defaultblank.aspx"></iframe>
					</td>
				</tr>
			</table>
		</td>
		<td class="C_right2" width="13"></td>
	<tr>
		<td class="C_left3" width="13"></td>
		<td class="C_bottom" ></td>
		<td class="C_right3" width="13"></td>
	</tr>
</table>
</TD></TR></TBODY></TABLE>
