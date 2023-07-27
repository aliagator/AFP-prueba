<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ucIniciarExpediente.ascx.vb" Inherits="Sonda.Net.Nxt.Workflow.WEB.ucIniciarExpediente" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
	<tr>
		<td colSpan="3">
			<table class="tablatit" cellSpacing="0" cellPadding="0" width="95%">
				<tr>
					<td class="titppal"></td>
					<td class="titppal2"><snt:label id="Label1" runat="server" cssclass="titppal2">Iniciar un expediente</snt:label></td>
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
			<TABLE id="tblFiltro" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<TBODY>
					<TR>
						<TD width="100%">
							<TABLE style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="300">
								<TBODY>
									<TR>
										<TD class="tit_left"></TD>
										<TD class="tit_center"><asp:label id="lblSeleccioneTarea" runat="server">Seleccione la tarea de inicio</asp:label></TD>
										<TD class="tit_right"></TD>
									</TR>
								</TBODY>
							</TABLE>
							<TABLE class="TableContenido1" style="WIDTH: 100%; HEIGHT: 20px" cellSpacing="0" cellPadding="0"
								border="0">
								<TBODY>
									<TR>
										<TD style="HEIGHT: 20px">
											<TABLE style="WIDTH: 100%; HEIGHT: 20px" cellSpacing="0" cellPadding="0" align="center"
												border="0">
												<TBODY>
													<TR>
														<TD class="label" style="WIDTH: 339px"><snt:label id="Label11" runat="server" Tipo="NombreCampo">Tarea:</snt:label></TD>
														<TD class="label" style="WIDTH: 331px" align="left"></TD>
													</TR>
													<TR>
														<TD class="casilla" style="HEIGHT: 16px" colSpan="2"><snt:dropdownlist id="ddlTarea" runat="server" Tipo="NombreCampo" AutoPostBack="True" Requerido="True"
																Width="95%" GuardarEnHistoria="True" ></snt:dropdownlist></TD>
													</TR>
													<TR>
														<TD class="label" style="WIDTH: 339px; HEIGHT: 19px" colSpan="2"><snt:label id="Label7" runat="server" Tipo="NombreCampo">Tipo Expediente:</snt:label></TD>
													</TR>
													<TR>
														<TD class="casilla" style="WIDTH: 339px; HEIGHT: 12px" colSpan="2"><snt:dropdownlist id="ddlTipoExped" runat="server" Tipo="NombreCampo" Requerido="True" Width="274px"
																GuardarEnHistoria="True" AutoPostBack="True"></snt:dropdownlist></TD>
													</TR>
													<tr>
														<td colSpan="2">
															<TABLE id="TablaDatosDespacho" style="WIDTH: 100%; HEIGHT: 20px" cellSpacing="0" cellPadding="0"
																align="center" border="0" runat="server">
																<TR>
																	<TD class="label" style="WIDTH: 331px; HEIGHT: 19px" align="left" colSpan="2"><snt:label id="Label6" runat="server" Tipo="NombreCampo">Id.Expediente:</snt:label></TD>
																</TR>
																<TR>
																	<TD class="casilla" style="WIDTH: 331px; HEIGHT: 2px" align="left" colSpan="2"><snt:textboxadv id="txtllaveexpid" runat="server" Tipo="NombreCampo" Requerido="True" GuardarEnHistoria="True"
																			MaxLength="16" TipoCase="TextoLibre"></snt:textboxadv></TD>
																</TR>
																<TR>
																	<TD class="label" style="WIDTH: 339px; HEIGHT: 18px"><snt:label id="Label2" runat="server" Tipo="NombreCampo">Descripción:</snt:label></TD>
																	<TD class="label" style="WIDTH: 331px; HEIGHT: 18px" align="left"></TD>
																</TR>
																<TR>
																	<TD class="casilla" colSpan="2"><snt:textboxadv id="txtDescripcion" runat="server" Tipo="NombreCampo" Requerido="True" Width="95%"
																			GuardarEnHistoria="True" MaxLength="50" TipoCase="TextoLibre"></snt:textboxadv></TD>
																</TR>
																<TR>
																	<TD class="label" style="WIDTH: 339px"><snt:label id="Label3" runat="server" Tipo="NombreCampo">Observación:</snt:label></TD>
																	<TD class="label" style="WIDTH: 331px" align="left"></TD>
																</TR>
																<TR>
																	<TD class="casilla" colSpan="2"><snt:textboxadv id="txtObs" runat="server" Tipo="NombreCampo" Width="95%" GuardarEnHistoria="True"
																			MaxLength="255" TipoCase="TextoLibre"></snt:textboxadv></TD>
																</TR>
															</TABLE>
														</td>
													</tr>
												</TBODY>
											</TABLE>
										</TD>
									</TR>
									<tr>
										<td align="right"><snt:button id="btnIniciar" runat="server" Tipo="TextoLibre" Etiqueta="Iniciar" CssClass="btnNuevo"></snt:button><snt:button id="btnVolver" runat="server" Tipo="Volver" CssClass="btnVolver" Visible="False"></snt:button></td>
									</tr>
								</TBODY>
							</TABLE>
						</TD>
					</TR>
				</TBODY>
			</TABLE>
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
