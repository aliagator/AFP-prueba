<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ucPendientesIncrustado.ascx.vb" Inherits="Sonda.Net.Nxt.Workflow.WEB.ucPendientesIncrustado" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<asp:panel id="pnlFiltroUsuario" runat="server">
	<TABLE style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="300">
		<TR>
			<TD class="tit_left"></TD>
			<TD class="tit_center">
				<asp:label id="lblFiltroUsuario" runat="server">Filtro Usuario</asp:label></TD>
			<TD class="tit_right"></TD>
		</TR>
	</TABLE>
	<TABLE id="TableUsuario"  class="TableContenido1" border="0" cellSpacing="0" cellPadding="0" width="100%">
		<TR>
			<TD>
				<TABLE style="HEIGHT: 20px" id="Table2" border="0" cellSpacing="1" cellPadding="1" width="100%">
					<TR>
						<TD style="WIDTH: 76px; HEIGHT: 24px" class="label">
							<snt:label id="Label3" runat="server" Tipo="NombreCampo">Id.Usuario</snt:label></TD>
						<TD style="WIDTH: 150px; HEIGHT: 24px" class="casilla">
						
							<snt:dropdownlist id="ddlUsuario" runat="server" Tipo="NombreCampo" GuardarEnHistoria="True" AutoPostBack="True"
								AgregarDatoVacio="True" TextoDatoVacio="(Todos)"></snt:dropdownlist>
							<snt:hyperlinkbusqueda id="HyperlinkBusqueda1" runat="server" PaginaContenedora="defaultblank.aspx" AnchoVentana="800"
	AltoVentana="500" AutoPostBackOnClose="True" Controles="txtIdUsuario" UserControl="WebControlAccesoUsuario/usuBVSeleccionarUsuario"
	ImageUrl="../../img/busqueda.gif"></snt:hyperlinkbusqueda>
						<INPUT id="txtIdUsuario" style="WIDTH: 32px; HEIGHT: 22px" type="hidden" size="1" name="txtIdUsuario"
							runat="server">
								
						</TD>
						<TD style="WIDTH: 75px; HEIGHT: 24px" class="label">
							<snt:label id="Label5" runat="server" Tipo="NombreCampo">Nombre</snt:label></TD>
						<TD style="HEIGHT: 24px" class="casilla">
							<snt:label id="lblNombre" runat="server" Tipo="NombreCampo">Administrador</snt:label></TD>
						<TD style="WIDTH: 120px; HEIGHT: 24px" class="label">
							<snt:label id="Label4" runat="server" Tipo="NombreCampo">Nro. de Pendientes</snt:label></TD>
						<TD style="WIDTH: 30px; HEIGHT: 24px" class="casilla">
							<snt:label id="lblNroCopiasPendientes" runat="server" Tipo="NombreCampo">25</snt:label></TD>
					</TR>
					<TR id="rowIncHistoria" runat="server">
						<TD style="WIDTH: 76px; HEIGHT: 24px" class="label"></TD>
						<TD colSpan="5">
							<snt:checkbox id="chkIncHistoria" runat="server" Tipo="NombreCampo" GuardarEnHistoria="True" Autopostback="True"
								ToolTip="Incluír todos los expedientes en los que el usuario seleccionado ha participado, aunque no los tenga asignados actualmente."
								Text="Incluír expedientes en los que ha participado."></snt:checkbox></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
	</TABLE>
</asp:panel>
<TABLE id="tblFiltro" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	<TR>
		<TD width="100%"><asp:panel id="pnlFiltroBasico" runat="server">
				<TABLE style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="300">
					<TR>
						<TD class="tit_left"></TD>
						<TD class="tit_center">
							<asp:label id="Label14" runat="server">Filtro Básico</asp:label></TD>
						<TD class="tit_right"></TD>
					</TR>
				</TABLE>
				<TABLE style="WIDTH: 100%; HEIGHT: 20px" class="TableContenido1" border="0" cellSpacing="0"
					cellPadding="0">
					<TR>
						<TD style="HEIGHT: 20px">
							<TABLE style="WIDTH: 100%; HEIGHT: 20px" border="0" cellSpacing="0" cellPadding="0" align="center">
								<TBODY>
									<TR>
										<TD style="WIDTH: 30%" class="label">
											<snt:label id="Label15" runat="server" Tipo="NombreCampo">Proyecto:</snt:label></TD>
										<TD style="WIDTH: 30%" class="label" align="left">
											<snt:label id="Label16" runat="server" Tipo="NombreCampo">Tipo de Expediente:</snt:label></TD>
										<TD style="WIDTH: 30%" class="label" align="left">
											<snt:label id="Label11" runat="server" Tipo="NombreCampo">Tarea:</snt:label>
										</TD>
									</TR>
									<TR>
										<TD style=" HEIGHT: 16px" class="casilla">
											<snt:dropdownlist id="ddlProyecto" runat="server" Tipo="NombreCampo" GuardarEnHistoria="True" AutoPostBack="True"
												AgregarDatoVacio="True" TextoDatoVacio="(Seleccione un Proyecto)" Width="250px"></snt:dropdownlist></TD>
										<TD style="HEIGHT: 2px" class="casilla">
											<snt:dropdownlist id="ddlTipoExped" runat="server" Tipo="NombreCampo" GuardarEnHistoria="True" AgregarDatoVacio="True"
												TextoDatoVacio="(Seleccione un tipo de expediente)" Width="250px"></snt:dropdownlist></TD>
						</TD>
						<TD style=" HEIGHT: 16px" class="casilla">
							<snt:dropdownlist id="ddlTarea" runat="server" Tipo="NombreCampo" GuardarEnHistoria="True" AgregarDatoVacio="True"
								TextoDatoVacio="(Seleccione una Tarea)" Width="250px"></snt:dropdownlist>
						</TD>
					</TR>
				</TABLE>
				<asp:PlaceHolder id="PHFiltroAdicional" runat="server"></asp:PlaceHolder></TD>
		</TD>
	</TR>
</TABLE>
</asp:panel><asp:panel id="pnlFiltroAvanzado" runat="server" Visible="False">
	<TABLE style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="300">
		<TR>
			<TD class="tit_left"></TD>
			<TD class="tit_center">
				<asp:label id="lblCriterios" runat="server">Filtro Avanzado</asp:label></TD>
			<TD class="tit_right"></TD>
		</TR>
	</TABLE>
	<TABLE style="WIDTH: 100%; HEIGHT: 20px" class="TableContenido1" border="0" cellSpacing="0"
		cellPadding="0">
		<TR>
			<TD style="HEIGHT: 20px">
				<TABLE style="WIDTH: 100%; HEIGHT: 20px" border="0" cellSpacing="0" cellPadding="0" align="center">
					<TR>
						<TD style="WIDTH: 339px" class="label">
							<snt:label id="Label12" runat="server" Tipo="NombreCampo">Desde:</snt:label></TD>
						<TD style="WIDTH: 331px" class="label" align="left">
							<snt:label id="Label13" runat="server" Tipo="NombreCampo">Hasta:</snt:label></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 339px; HEIGHT: 17px" class="casilla">
							<snt:fechaadv id="FechaDesde" runat="server" GuardarEnHistoria="True"></snt:fechaadv></TD>
						<TD style="WIDTH: 331px; HEIGHT: 17px" class="casilla" align="left">
							<snt:fechaadv id="FechaHasta" runat="server" GuardarEnHistoria="True"></snt:fechaadv></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 339px; HEIGHT: 11px" class="label">
							<snt:label id="lblVersión" runat="server" Tipo="NombreCampo">Versión</snt:label></TD>
						<TD style="WIDTH: 331px" class="label">
							<snt:label id="Label6" runat="server" Tipo="NombreCampo">Id.Expediente:</snt:label></TD>
					</TR>
					<TR>
						<TD>
							<snt:dropdownlist id="ddlVersion" runat="server" Tipo="NombreCampo" GuardarEnHistoria="True" AutoPostBack="True"
								AgregarDatoVacio="True" TextoDatoVacio="(Seleccione una Versión)" Width="274px"></snt:dropdownlist></TD>
						<TD style="WIDTH: 331px; HEIGHT: 16px" class="casilla" align="left">
							<snt:textbox id="txtLlaveExpId" runat="server" Tipo="NombreCampo" GuardarEnHistoria="True"></snt:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 339px" class="label">
							<snt:label id="Label8" runat="server" Tipo="NombreCampo">Proceso de Negocio:</snt:label></TD>
						<TD style="WIDTH: 331px" class="label" align="left">
							<snt:label id="Label9" runat="server" Tipo="NombreCampo">Módulo:</snt:label></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 339px; HEIGHT: 17px" class="casilla">
							<snt:textboxadv id="txtProcesoNegocio" runat="server" Tipo="NombreCampo" GuardarEnHistoria="True"></snt:textboxadv></TD>
						<TD style="WIDTH: 331px; HEIGHT: 17px" class="casilla" align="left">
							<snt:textbox id="txtModulo" runat="server" Tipo="NombreCampo" GuardarEnHistoria="True"></snt:textbox></TD>
					</TR>
					<TR>
						<TD style="WIDTH: 50%; HEIGHT: 20px" class="casilla">
							<snt:checkbox id="chkExpedNoAsignados" runat="server" Tipo="NombreCampo" GuardarEnHistoria="True"
								Text="Incluír expedientes no asignados."></snt:checkbox></TD>
						<TD style="WIDTH: 50%; HEIGHT: 20px" class="casilla">
							<snt:checkbox id="chkExpedFinalizados" runat="server" Tipo="NombreCampo" GuardarEnHistoria="True"
								Text="Incluír expedientes finalizados."></snt:checkbox></TD>
					</TR>
					<TR>
					</TR>
				</TABLE>
			</TD>
		</TR>
	</TABLE>
</asp:panel></TD></TR></TBODY></TABLE>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	<TR>
		<TD align="right" width="100%"><snt:button id="btnVerFiltro" runat="server" Tipo="TextoLibre" CssClass="btnVerDetalle2" Etiqueta="Ver Filtro Avanzado"></snt:button><snt:button id="btnNuevo" runat="server" Tipo="Nuevo" CssClass="btnNuevo"></snt:button></TD>
	</TR>
</TABLE>
<TABLE style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="100%">
	<TR>
		<TD class="tit_left" width="20"></TD>
		<TD class="tit_center" width="300"><asp:label id="Label2" runat="server">Pendientes</asp:label></TD>
		<TD class="tit_right" width="20"></TD>
		<td align="right"><snt:button id="btnActualizar" runat="server" Tipo="Actualizar" CssClass="btnActualizar"></snt:button><snt:button id="btnVolver" runat="server" Tipo="Volver" Visible="False" CssClass="btnVolver"></snt:button></td>
	</TR>
</TABLE>
<table class="TableContenido1" cellSpacing="0" cellPadding="0" width="99%" border="0">
	<TR>
		<TD>
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD align="left">
						<asp:panel id="pnlFiltroColumnas" runat="server" Visible="false">
							<snt:Label id="lblColumnas" runat="server" Tipo="NotaChica">lblColumnas</snt:Label>
							<asp:PlaceHolder id="phFiltroColumnas" runat="server"></asp:PlaceHolder></TD>
					</asp:panel>
					<td style="WIDTH:30px">
						<snt:hyperlink id="HypExportExcel" runat="server" ImageUrl="~/Img/excel.gif" AutoPostBack="True"></snt:hyperlink>
					</td>
				</TR>
			</TABLE>
			<snt:tablapaginada id="TablaPaginada1" runat="server" GuardarEnHistoria="True" ConservarPaginaActual="True"
				EnableColumnViewState="True">
				<snt:TablaPaginadaColumna Ancho="" Visible="False" Titulo="PROYID" ColId="PROYID" NombreCampo="PROYID" EsLlave="True"
					EsParametro="True" />
				<snt:TablaPaginadaColumna Ancho="" Visible="False" Titulo="VERSIONID" ColId="VERSIONID" NombreCampo="VERSIONID"
					EsLlave="True" EsParametro="True" />
				<snt:TablaPaginadaColumna Width="34px" Ancho="34px" Titulo="" ColId="ALARMADA" NombreCampo="ALARMADA" Formato="Personalizado"
					FormatoPersonalizado="<p align=center><img src='img/wfl/a{ALARMADA}.gif'></img></p>" ExpresionOrden="ALARMADA"
					Ordenar="True" />
				<snt:TablaPaginadaColumna Width="34px" Ancho="34px" Titulo="" ColId="VENCIDA" NombreCampo="VENCIDA" Formato="Personalizado"
					FormatoPersonalizado="<p align=center><img src='img/wfl/c{VENCIDA}.gif'></img></p>" ExpresionOrden="VENCIDA"
					Ordenar="True" />
				<snt:TablaPaginadaColumna Ancho="" Titulo="Proy." ColId="NOMBREPROY" NombreCampo="NOMBREPROY" ExpresionOrden="P.ATNOMBREPROY"
					Ordenar="True" />
				<snt:TablaPaginadaColumna Ancho="" Titulo="Exp.Id" ColId="LLAVEEXPID" NombreCampo="LLAVEEXPID" EsLlave="True"
					EsParametro="True" ExpresionOrden="C.ATLLAVEEXPID" Ordenar="True" />
				<snt:TablaPaginadaColumna Ancho="" Titulo="Descripción" ColId="DESCRIPCION" NombreCampo="DESCRIPCION" ExpresionOrden="C.ATDESCRIPCION"
					Ordenar="True" />
				<snt:TablaPaginadaColumna Ancho="" Titulo="Copia" ColId="COPIAEXPID" NombreCampo="COPIAEXPID" EsLlave="True"
					EsParametro="True" />
				<snt:TablaPaginadaColumna Ancho="" Titulo="Tarea" ColId="TITULOTAREA" NombreCampo="TITULOTAREA" ExpresionOrden="T.ATTITULOTAREA"
					Ordenar="True" />
				<snt:TablaPaginadaColumna Ancho="" Titulo="Persona" ColId="PERSONAASIGNADA" NombreCampo="PERSONAASIGNADA"
					EsLlave="True" EsParametro="True" ExpresionOrden="C.ATPERSONAASIGNADA" Ordenar="True" />
				<snt:TablaPaginadaColumna Ancho="" Titulo="Fecha" ColId="Fecha" NombreCampo="DFecha" ExpresionOrden="C.ATFECHA"
					Ordenar="True" />
				<snt:TablaPaginadaColumna Ancho="" Titulo="" ColId="PROCESAR" NombreCampo="" EsHyperlink="True" Formato="Personalizado"
					FormatoPersonalizado="Procesar" hlnk_AltoVentana="0" hlnk_AnchoVentana="0" hlnk_AutoPostback="True" hlnk_ID="PROCESAR" />
				<snt:TablaPaginadaColumna Width="4px" Ancho="4px" Titulo="" ColId="ESPACIO" NombreCampo="" Formato="Personalizado"
					FormatoPersonalizado=" " />
				<snt:TablaPaginadaColumna Ancho="" Titulo="" ColId="VERDETALLE" NombreCampo="" EsHyperlink="True" Formato="Personalizado"
					FormatoPersonalizado="Ver Detalle" hlnk_AltoVentana="200" hlnk_AnchoVentana="800" hlnk_Modal="True" hlnk_ScrollVentana="True"
					hlnk_NuevaVentana="True" hlnk_UserControl="Nxt/Workflow/ucDetalleCopia" hlnk_AutoPostbackOnClose="True"
					hlnk_ID="VERDETALLE" />
				<snt:TablaPaginadaColumna Width="4px" Ancho="4px" Titulo="" ColId="ESPACIO2" NombreCampo="" Formato="Personalizado"
					FormatoPersonalizado=" " />
				<snt:TablaPaginadaColumna Ancho="" Titulo="" ColId="Diagrama" NombreCampo="" EsHyperlink="True" Formato="Personalizado"
					FormatoPersonalizado="Diagrama" hlnk_AltoVentana="500" hlnk_AnchoVentana="800" hlnk_Modal="True" hlnk_ScrollVentana="True"
					hlnk_NuevaVentana="True" hlnk_UserControl="nxt/workflow/ucDiagrama" hlnk_ID="Diagrama" />
				<snt:TablaPaginadaColumna Ancho="" Visible="False" Titulo="TAREAACTUAL" ColId="TAREAACTUAL" NombreCampo="TAREAACTUAL"
					EsLlave="True" EsParametro="True" />
				<snt:TablaPaginadaColumna Ancho="" Visible="False" Titulo="DIAGACTUAL" ColId="DIAGACTUAL" NombreCampo="DIAGACTUAL"
					EsLlave="True" EsParametro="True" />
			</snt:tablapaginada></TD>
	</TR>
</table>
<snt:label id="lblMaximoRegistros" runat="server" Tipo="NotaGrande">Esta consulta retorna solo los primeros 100 registros. Utilice el filtro o el orden si los registros que busca no se encuentran en el resultado.</snt:label>
<asp:panel id="Panel1" runat="server" HorizontalAlign="Right" style="TEXT-ALIGN: left" visible="False">
	<snt:Button id="btnSeleccionarTodo" runat="server" Tipo="Seleccionar" Etiqueta="Todos" CssClass="SinTexto150"></snt:Button>
	<snt:HyperLink id="HyperLink1" runat="server" AutoPostBack="True" CssClass="btnSinTexto" visible="False"
		AltoVentana="500" AnchoVentana="400" AutoPostBackOnClose="True" LeftVentana="100" Modal="True"
		MostrarComoBoton="True" NuevaVentana="True" PaginaContenedora=" " TipoBoton="TextoLibre" TopVentana="50"
		UserControl="Nxt/Workflow/ucSeleccionarPersona" EtiquetaBoton="ReasignarLink">[HyperLink1]</snt:HyperLink>
	<snt:HyperLink id="HyperLink2" runat="server" AutoPostBack="True" CssClass="btnSinTexto" visible="False"
		AltoVentana="500" AnchoVentana="400" AutoPostBackOnClose="True" LeftVentana="100" Modal="True"
		MostrarComoBoton="True" NuevaVentana="True" PaginaContenedora=" " TipoBoton="TextoLibre" TopVentana="50"
		UserControl="Nxt/Workflow/ucSeleccionarPersona" EtiquetaBoton="DespacharLink">[HyperLink2]</snt:HyperLink>
	<snt:Button id="btnReasignar" runat="server" Tipo="Reasignar" CssClass="btnSinTexto"></snt:Button>
	<snt:Button id="btnDespachar" runat="server" Tipo="Despachar" CssClass="btnSinTexto"></snt:Button>
	<snt:Button id="btnLimpiarSeleccion" runat="server" Tipo="Limpiar" Etiqueta="Seleccion" CssClass="SinTexto150"></snt:Button>
</asp:panel><asp:panel id="Panel2" Runat="server"></asp:panel>
