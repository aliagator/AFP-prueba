<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ucMigraAttribXmlATablas.ascx.vb" Inherits="Sonda.Net.Nxt.Workflow.WEB.ucMigraAttribXmlATablas" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<table border="0" cellSpacing="0" cellPadding="0" width="95%" align="center">
	<tr>
		<td colSpan="3">
			<table class="tablatit" cellSpacing="0" cellPadding="0" width="95%">
				<tr>
					<td class="titppal"></td>
					<td class="titppal2"><snt:label id="Label1" runat="server" cssclass="titppal2">Migracion de Atributos en Xml a Tablas</snt:label></td>
					<td class="titppal3"></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td class="C_left" width="13"></td>
		<td width="13"></td>
		<td class="C_right" width="13"></td>
	</tr>
	<tr>
		<td class="C_left2" style="HEIGHT: 18px" width="13"></td>
		<td style="HEIGHT: 18px" width="13"></td>
		<td class="C_right2" style="HEIGHT: 18px" width="13"></td>
	</tr>
	<tr>
		<td class="C_left2" width="13"></td>
		<td width="424" style="FONT-WEIGHT: bold; TEXT-DECORATION: underline">Resumen de 
			Proceso</td>
		<td class="C_right2" width="13"></td>
	</tr>
	<tr>
		<td class="C_left2" style="HEIGHT: 18px" width="13"></td>
		<td style="HEIGHT: 18px" width="424">Tablas Procesadas</td>
		<td class="C_right2" style="HEIGHT: 18px" width="13"></td>
	</tr>
	<tr>
		<td class="C_left2" style="HEIGHT: 18px" width="13"></td>
		<td style="HEIGHT: 18px" width="424">
			<asp:TextBox id="txtTablasProcesadas" runat="server" Width="603px" TextMode="MultiLine" Wrap="False"
				Height="104px"></asp:TextBox></td>
		<td class="C_right2" style="HEIGHT: 18px" width="13"></td>
	</tr>
	<tr>
		<td class="C_left2" style="HEIGHT: 22px" width="13"></td>
		<td style="HEIGHT: 22px" width="424">Total de Tablas Procesadas:
			<asp:Label id="lblTotalTablasProcesadas" runat="server" Width="144px" Height="20px"></asp:Label></td>
		<td class="C_right2" style="HEIGHT: 22px" width="13"></td>
	</tr>
	<tr>
		<td class="C_left2" style="HEIGHT: 18px" width="13"></td>
		<td style="HEIGHT: 18px" width="424">Total Registros Actualizados:
			<asp:Label id="lblTotalRegistrosActualizados" runat="server"></asp:Label></td>
		<td class="C_right2" style="HEIGHT: 18px" width="13"></td>
	</tr>
	<tr>
		<td class="C_left2" style="HEIGHT: 18px" width="13"></td>
		<td style="HEIGHT: 18px" width="424">Total de Tablas Procesadas Erroneas:
			<asp:Label id="lblTotalTablasProcesadasErroneas" runat="server"></asp:Label></td>
		
		<td class="C_right2" style="HEIGHT: 18px" width="13"></td>
	</tr>
	<tr>
		<td class="C_left2" width="13">
			<P align="right">&nbsp;</P>
		</td>
		<td>
			<P align="right">&nbsp;</P>
			<DIV align="right">
				<P align="right"><snt:button id="btnProcesar" runat="server" Tipo="Procesar"></snt:button><snt:button id="btnVolver" runat="server" Tipo="Volver" CausesValidation="False"></snt:button><asp:validationsummary id="Validationsummary2" runat="server" ShowMessageBox="True" ShowSummary="False"
						Width="450px"></asp:validationsummary></P>
			</DIV>
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
