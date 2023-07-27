<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ucDespachar.ascx.vb" Inherits="Sonda.Net.Nxt.Workflow.WEB.ucDespachar" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
	<tr>
		<td colSpan="3">
			<table class="tablatit" cellSpacing="0" cellPadding="0" width="95%">
				<tr>
					<td class="titppal"></td>
					<td class="titppal2"><snt:label id="Label4" Tipo="NombreCampo" runat="server" cssclass="titppal2"> Despacho:</snt:label></td>
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
			<table style="HEIGHT: 20px" cellSpacing="0" cellPadding="0" width="300">
				<tr>
					<td class="tit_left"></td>
					<td class="tit_center"><asp:label id="Label10" runat="server">Seleccione una flecha:</asp:label></td>
					<td class="tit_right"></td>
				</tr>
			</table>
			<TABLE class="TableContenido1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD align="left"><snt:listbox id="ddlFlechasSalida" Tipo="NombreCampo" runat="server" AutoPostBack="True" Rows="8"
							Width="95%"></snt:listbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="ddlFlechasSalida"
							ErrorMessage="Seleccione una flecha de salida">*</asp:requiredfieldvalidator></TD>
				</TR>
			</TABLE>
			<table style="HEIGHT: 20px" cellSpacing="0" cellPadding="0" width="300">
				<tr>
					<td class="tit_left"></td>
					<td class="tit_center"><asp:label id="Label1" runat="server">Ingrese una observacion:</asp:label></td>
					<td class="tit_right"></td>
				</tr>
			</table>
			<TABLE class="TableContenido1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD align="left"><snt:textboxadv id="txtObs" Tipo="NombreCampo" runat="server" Width="95%" TextMode="MultiLine" TipoCase="TextoLibre"
							Height="80px"></snt:textboxadv></TD>
				</TR>
			</TABLE>
			<table id="tblPersona" cellSpacing="0" cellPadding="0" width="100%" runat="server">
				<tr>
					<td>
						<TABLE id="Table1" style="HEIGHT: 20px" cellSpacing="0" cellPadding="0" width="300">
							<TR>
								<TD class="tit_left"></TD>
								<TD class="tit_center">
									<asp:label id="Label" runat="server">Seleccione la persona asignada:</asp:label></TD>
								<TD class="tit_right"></TD>
							</TR>
						</TABLE>
						<snt:RadioButtonList id="rblPersonaAsignada" runat="server" Tipo="NombreCampo" DataTextField="NOMBRE"
							DataValueField="IdPersona"></snt:RadioButtonList>
					</td>
				</tr>
			</table>
			<table id="tblPersonas" cellSpacing="0" cellPadding="0" width="100%" runat="server">
				<tr>
					<td>
						<TABLE id="Table2" style="HEIGHT: 20px" cellSpacing="0" cellPadding="0" width="300">
							<TR>
								<TD class="tit_left"></TD>
								<TD class="tit_center">
									<asp:label id="Label2" runat="server">Seleccione las personas asignadas:</asp:label></TD>
								<TD class="tit_right"></TD>
							</TR>
						</TABLE>
						<snt:CheckBoxList id="chkPersonasAsignadas" runat="server" Tipo="NombreCampo" DataTextField="NOMBRE"
							DataValueField="IdPersona"></snt:CheckBoxList>
					</td>
				</tr>
			</table>
			<P align="right"><snt:button id="btnDespachar" Tipo="Despachar" runat="server"></snt:button><snt:button id="btnVolver" Tipo="Volver" runat="server" CausesValidation="False"></snt:button><asp:validationsummary id="ValidationSummary1" runat="server" Width="450px" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></P>
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
