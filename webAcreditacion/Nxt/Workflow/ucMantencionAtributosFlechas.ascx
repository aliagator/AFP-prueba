<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ucMantencionAtributosFlechas.ascx.vb" Inherits="Sonda.Net.Nxt.Workflow.WEB.ucMantencionAtributosFlechas" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
	<tr>
		<td colSpan="3">
			<table class="tablatit" cellSpacing="0" cellPadding="0" width="95%">
				<tr>
					<td class="titppal"></td>
					<td class="titppal2"><snt:label id="Label1" runat="server" cssclass="titppal2">Mantención de atributos de una Tarea / Flecha</snt:label></td>
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
			<table style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="300">
				<tr>
					<td class="tit_left"></td>
					<td class="tit_center"><asp:label id="lblCriterios" runat="server">Seleccione una Tarea</asp:label></td>
					<td class="tit_right"></td>
				</tr>
			</table>
			<TABLE class="TableContenido1" style="WIDTH: 100%; HEIGHT: 20px" cellSpacing="0" cellPadding="0"
				border="0">
				<TR>
					<TD style="HEIGHT: 20px">
						<TABLE id="Table3" style="WIDTH: 100%; HEIGHT: 20px" cellSpacing="0" cellPadding="0" align="center"
							border="0">
							<TR>
								<TD class="label" style="WIDTH: 339px"><snt:label id="lblEmpresa" runat="server" Tipo="NombreCampo">Proyecto</snt:label></TD>
								<TD class="label" style="WIDTH: 331px"><snt:label id="lblVersión" runat="server" Tipo="NombreCampo">Versión</snt:label></TD>
							</TR>
							<TR>
								<TD class="casilla" style="WIDTH: 339px; HEIGHT: 13px"><snt:dropdownlist id="ddlProyecto" runat="server" Tipo="NombreCampo" 
										AutoPostBack="True" TextoDatoVacio="(Seleccione un Proyecto)" AgregarDatoVacio="True" GuardarEnHistoria="True" Width="274px" Requerido="True"></snt:dropdownlist></TD>
								<TD class="casilla" style="WIDTH: 331px; HEIGHT: 13px" align="left"><snt:dropdownlist id="ddlVersion" runat="server" Tipo="NombreCampo" 
										AutoPostBack="True" TextoDatoVacio="(Seleccione una Versión)" AgregarDatoVacio="True" GuardarEnHistoria="True" Width="274px" Requerido="True"></snt:dropdownlist></TD>
							</TR>
							<TR>
								<TD class="label" style="WIDTH: 339px"><snt:label id="Label11" runat="server" Tipo="NombreCampo">Tarea:</snt:label></TD>
								<TD class="label" style="WIDTH: 331px" align="left"></TD>
							</TR>
							<TR>
								<TD class="casilla" style="WIDTH: 100%; HEIGHT: 2px" colSpan="2"><snt:dropdownlist id="ddlTarea" runat="server" Tipo="NombreCampo" 
										AutoPostBack="True" TextoDatoVacio="(Seleccione una Tarea)" AgregarDatoVacio="True" GuardarEnHistoria="True" Width="50%" Requerido="True"></snt:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100%" colSpan="2">
									<P align="right">
										<snt:HyperLink id="hlnkEditarAtributos" runat="server" MostrarComoBoton="True" EtiquetaBoton="Atributos"
											TipoBoton="Editar" AutoPostBack="True" NuevaVentana="True" Modal="True" UserControl="Nxt/Workflow/ucEditarAtributos"
											AltoVentana="400" AnchoVentana="600"></snt:HyperLink><snt:button id="btnBuscar" runat="server" Tipo="Buscar" CssClass="btnBuscar" Visible="False"></snt:button><snt:button id="btnVolver" runat="server" Tipo="Volver" CssClass="btnVolver" Visible="False"></snt:button></P>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<table style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="300">
				<tr>
					<td class="tit_left"></td>
					<td class="tit_center"><asp:label id="Label2" runat="server">Flechas de Salida</asp:label></td>
					<td class="tit_right"></td>
				</tr>
			</table>
			<TABLE class="TableContenido1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD><snt:tablapaginada id="TablaPaginada1" runat="server">
							<snt:TablaPaginadaColumna Ancho="" Titulo="Id.Flecha" ColId="FLECHAID" NombreCampo="FLECHAID" EsLlave="True"
								EsParametro="True" />
							<snt:TablaPaginadaColumna Ancho="" Titulo="Nombre" ColId="NOMBREFLECHA" NombreCampo="NOMBREFLECHA" />
							<snt:TablaPaginadaColumna Ancho="" Titulo="Tarea Destino" ColId="TITULOTAREADESTINO" NombreCampo="TITULOTAREADESTINO" />
							<snt:TablaPaginadaColumna Ancho="" Titulo="Editar" ColId="EDITAR" NombreCampo="" EsHyperlink="True" Formato="Personalizado"
								FormatoPersonalizado="Editar atributos" hlnk_AltoVentana="400" hlnk_AnchoVentana="600" hlnk_Modal="True"
								hlnk_ScrollVentana="True" hlnk_NuevaVentana="True" hlnk_UserControl="Nxt/Workflow/ucEditarAtributos" hlnk_ID="EDITAR" />
							<snt:TablaPaginadaColumna Ancho="" Visible="False" Titulo="DIAGID" ColId="DIAGID" NombreCampo="DIAGID" EsLlave="True"
								EsParametro="True" />
							<snt:TablaPaginadaColumna Ancho="" Visible="False" Titulo="PROYID" ColId="PROYID" NombreCampo="PROYID" EsLlave="True"
								EsParametro="True" />
							<snt:TablaPaginadaColumna Ancho="" Visible="False" Titulo="VERSIONID" ColId="VERSIONID" NombreCampo="VERSIONID"
								EsLlave="True" EsParametro="True" />
							<snt:TablaPaginadaColumna Ancho="" Visible="False" Titulo="" ColId="CARGARENLOAD" NombreCampo="" EsParametro="True" />
							<snt:TablaPaginadaColumna Ancho="" Visible="False" Titulo="" ColId="EDITABLE" NombreCampo="" EsParametro="True" />
						</snt:tablapaginada></TD>
				</TR>
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
