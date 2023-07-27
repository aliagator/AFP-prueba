<%@ Control Language="vb" AutoEventWireup="false" Codebehind="UpLoadWF.ascx.vb" Inherits="Sonda.Net.Nxt.Workflow.WEB.UpLoadWF" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<P>&nbsp;</P>
<P>
	<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
		<TR>
			<td colSpan="3">
				<table class="tablatit" cellSpacing="0" cellPadding="0" width="95%">
					<tr>
						<td class="titppal"></td>
						<td class="titppal2"><snt:label id="Label1" runat="server" cssclass="titppal2" Tipo="NombreCampo"> Proceso de carga de un nuevo proyecto</snt:label></td>
						<td class="titppal3"></td>
					</tr>
				</table>
			</td>
		</TR>
		<tr>
			<td class="C_left" style="WIDTH: 15px" width="15"></td>
			<td class="C_center" width="424"></td>
			<td class="C_right" width="13"></td>
		</tr>
		<TR>
			<td class="C_left2" width="13"></td>
			<td>
				<TABLE cellSpacing="0" cellPadding="0">
					<tr>
						<td>
							<table style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="300">
								<tr>
									<td class="tit_left"></td>
									<td class="tit_center"><asp:label id="Label2" runat="server">Seleccione un proyecto</asp:label></td>
									<td class="tit_right"></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td>
							<TABLE cellSpacing="0" cellPadding="0" width="100%">
								<TBODY>
									<TR>
										<td>
											<asp:RadioButton id="rbExistente" runat="server" Checked="True" GroupName="existenteONuevo" Text="Existente"
												AutoPostBack="True"></asp:RadioButton>
											<asp:RadioButton id="rbNuevo" runat="server" GroupName="existenteONuevo" Text="Nuevo" AutoPostBack="True"></asp:RadioButton>
										</td>
									</TR>
									<TR>
										<td>
											<asp:DropDownList id="ddlProyecto" runat="server" Width="100%"></asp:DropDownList>
											<asp:TextBox id="txtNombreProy" runat="server" Width="100%" MaxLength="64" Visible="False"></asp:TextBox>
										</td>
									<tr>
									</tr>
								</TBODY>
							</TABLE>
						</td>
					</tr>
				</TABLE>
			</td>
			<td class="C_right2" width="13"></td>
		</TR>
		<TR>
			<td class="C_left2" width="13"></td>
			<td>
				<table style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="300">
					<tr>
						<td class="tit_left"></td>
						<td class="tit_center"><asp:label id="lblCriterios" runat="server">Seleccione archivo a cargar</asp:label></td>
						<td class="tit_right"></td>
					</tr>
				</table>
			</td>
			<td class="C_right2" width="13"></td>
		</TR>
		<TR>
			<td class="C_left2" width="13"></td>
			<TD style="WIDTH: 468px"><INPUT id="getFile" style="WIDTH: 557px; HEIGHT: 22px" type="file" size="73" name="getFile"
					runat="server"></TD>
			<td class="C_right2" width="13"></td>
		</TR>
		<TR id="trTituloObservaciones" runat="server">
			<td class="C_left2" width="13"></td>
			<td>
				<table style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="300">
					<tr>
						<td class="tit_left"></td>
						<td class="tit_center"><asp:label id="Lblobserva" runat="server">Observaciones</asp:label></td>
						<td class="tit_right"></td>
					</tr>
				</table>
			</td>
			<td class="C_right2" width="13"></td>
		</TR>
		<TR id="trObservaciones" runat="server">
			<td class="C_left2" width="13"></td>
			<TD><snt:textboxadv id="observaciones" runat="server" Tipo="NombreCampo" Width="556px" TextMode="MultiLine"
					Height="104px" TipoCase="minusculas"></snt:textboxadv></TD>
			<td class="C_right2" width="13"></td>
		</TR>
		<tr>
			<td class="C_left2" width="13"></td>
			<TD>
				<P><snt:button id="cmdUpload" runat="server" Tipo="Cargar" Etiqueta="Proyecto"></snt:button>
					<snt:Button id="btnVerDiagrama" Tipo="Ver" runat="server" Etiqueta="Diagrama" Enabled="False"></snt:Button></P>
			</TD>
			<td class="C_right2" width="13"></td>
		</tr>
		<tr>
			<td class="C_left3" width="13"></td>
			<td class="C_bottom" width="424"></td>
			<td class="C_right3" width="13"></td>
		</tr>
	</TABLE>
</P>
