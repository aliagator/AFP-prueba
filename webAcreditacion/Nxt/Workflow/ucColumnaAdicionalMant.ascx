<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ucColumnaAdicionalMant.ascx.vb" Inherits="Sonda.Net.Nxt.Workflow.WEB.ucColumnaAdicionalMant" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" align="center" border="0">
	<TR>
		<TD colSpan="3">
			<TABLE id="Table2" class="tablatit" cellSpacing="0" cellPadding="0" width="95%">
				<TR>
					<TD class="titppal"></TD>
					<TD class="titppal2"><snt:label id="Label2" runat="server">Columna</snt:label></TD>
					<TD class="titppal3"></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD class="C_left" width="13"></TD>
		<TD class="C_center" width="640"></TD>
		<TD class="C_right" width="13"></TD>
	</TR>
	<TR>
		<TD class="C_left2" width="13"></TD>
		<TD>
			<TABLE class="TableContenido1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
							<TR>
								<TD class="label"><snt:label id="Label15" runat="server" Tipo="NombreCampo">Proyecto:</snt:label></TD>
								<TD class="label"><snt:label id="Label16" runat="server" Tipo="NombreCampo">Tipo de Expediente:</snt:label></TD>
							</TR>
							<TR>
								<TD class="casilla"><snt:dropdownlist id="ddlProyecto" runat="server" Tipo="NombreCampo" TextoDatoVacio="(Seleccione un Proyecto)"
										AgregarDatoVacio="True" GuardarEnHistoria="True" Width="250px" AutoPostBack="True"></snt:dropdownlist></TD>
								<TD class="casilla"><snt:dropdownlist id="ddlTipoExped" runat="server" Tipo="NombreCampo" TextoDatoVacio="(Seleccione un tipo de expediente)"
										AgregarDatoVacio="True" GuardarEnHistoria="True" Width="250px" AutoPostBack="True"></snt:dropdownlist></TD>
							</TR>
							<TR>
								<TD style="WIDTH: 100%" colSpan="2">
									<P align="right"><snt:button id="btnBuscar" runat="server" Tipo="Buscar" Visible="False" CssClass="btnBuscar"></snt:button></P>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE class="TableContenido1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD align="right"><snt:hyperlink id="HypExportExcelLog" runat="server" AutoPostBack="True" ImageUrl="../../img/excel.gif"></snt:hyperlink><snt:hyperlink id="HypExportWordLog" runat="server" AutoPostBack="True" ImageUrl="../../Img/Word.gif"></snt:hyperlink></TD>
				</TR>
				<TR>
					<TD><snt:tablapaginada id="TpColumna" runat="server">
						<snt:TablaPaginadaColumna Ancho="" Visible="False" Titulo="ATCOLUMNAID" ColId="ATCOLUMNAID" NombreCampo="ATCOLUMNAID" EsLlave="True" EsParametro="True" />
						<snt:TablaPaginadaColumna Ancho="" Titulo="Título" ColId="TITULO" NombreCampo="TITULO" EsLlave="True" EsParametro="True" EsHyperlink="True" hlnk_AltoVentana="0" hlnk_AnchoVentana="0" hlnk_AutoPostback="True" hlnk_ID="TITULO" />
						<snt:TablaPaginadaColumna Ancho="" Titulo="Proyecto" ColId="ATPROYID" NombreCampo="ATPROYID" EsLlave="True" EsParametro="True" />
						<snt:TablaPaginadaColumna Ancho="" Titulo="Tipo Exp" ColId="ATTIPOEXPID" NombreCampo="ATTIPOEXPID" EsLlave="True" EsParametro="True" />
						<snt:TablaPaginadaColumna Ancho="" Titulo="Orden" ColId="ORDEN" NombreCampo="ORDEN" EsLlave="True" EsParametro="True" />
						<snt:TablaPaginadaColumna Ancho="" Titulo="Tipo" ColId="TIPO" NombreCampo="TIPO" EsLlave="True" EsParametro="True" />
						<snt:TablaPaginadaColumna Ancho="" Visible="False" Titulo="Tipo Link" ColId="LINK" NombreCampo="LINK" EsLlave="True" EsParametro="True" />
						<snt:TablaPaginadaColumna Ancho="" Visible="False" Titulo="OPCIONES" ColId="OPCIONES" NombreCampo="OPCIONES" EsLlave="True" EsParametro="True" />
						</snt:tablapaginada>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table4" style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="200">
				<TR>
					<TD class="tit_left"></TD>
					<TD class="tit_center"><asp:label id="Label10" runat="server">Columna</asp:label></TD>
					<TD class="tit_right"></TD>
				</TR>
			</TABLE>
			<TABLE class="TableContenido1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
			<TABLE class="Table7" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD class="label"><snt:label id="Label1" runat="server" Tipo="NombreCampo">Proyecto:</snt:label></TD>
					<TD class="label"><snt:label id="Label4" runat="server" Tipo="NombreCampo">Tipo de Expediente:</snt:label></TD>
					<TD class="label"><snt:label id="Label5" runat="server" Tipo="NombreCampo">Título</snt:label></TD>
				</TR>
				<TR>
					<TD align="left"><snt:textboxadv id="TxtProyecto" runat="server" Tipo="NombreCampo" Width="148px" enabled="false"
							TipoCase="TextoLibre"></snt:textboxadv></TD>
					<TD align="left"><snt:textboxadv id="TxtTipoExp" runat="server" Tipo="NombreCampo" Width="200px" enabled="false"
							TipoCase="TextoLibre"></snt:textboxadv></TD>
					<TD align="left"><snt:textboxadv id="TxtTitulo" runat="server" Tipo="NombreCampo" Width="148px" enabled="true" TipoCase="TextoLibre"></snt:textboxadv></TD>
				</TR>
			</TABLE>
			<TABLE id="Table5" cellSpacing="0" cellPadding="0" width="100%">
				<TR>
					<TD class="label" Width="25%"><snt:label id="Label6" runat="server" Tipo="NombreCampo">Orden:</snt:label></TD>
					<TD class="label" Width="25%"><snt:label id="Label7" runat="server" Tipo="NombreCampo">Tipo Link</snt:label></TD>
					<TD class="label" Width="16%"><snt:label id="lblEsPopup" runat="server" Tipo="NombreCampo" visible="false">Es PopUp</snt:label></TD>
					<TD class="label" Width="17%"><snt:label id="lblAncho" runat="server" Tipo="NombreCampo" visible="false">Ancho</snt:label></TD>
					<TD class="label" Width="17%"><snt:label id="lblAlto" runat="server" Tipo="NombreCampo" visible="false">Alto</snt:label></TD>
				</TR>
				<TR>
					<TD Width="25%"><snt:textboxadv id="txtOrden" runat="server" Tipo="NombreCampo" Width="100px" enabled="true" TipoCase="TextoLibre"></snt:textboxadv></TD>
					<TD class="casilla" Width="25%"><snt:dropdownlist id="ddlTipoLink" runat="server" Tipo="NombreCampo" TextoDatoVacio="(Seleccione Tipo Link)"
							AgregarDatoVacio="True" GuardarEnHistoria="True" Width="180px" AutoPostBack="True"></snt:dropdownlist></TD>
					<TD Width="16%"><snt:checkbox id="ChkEsPopUp" runat="server" Tipo="Titulo" visible="false" Enabled="true" Text=""></snt:checkbox></TD>
					<TD Width="17%"><snt:textboxadv id="TxtAncho" runat="server" Tipo="NombreCampo" Width="100px" enabled="true" TipoCase="TextoLibre"
							visible="false"></snt:textboxadv></TD>
					<TD Width="17%"><snt:textboxadv id="TxtAlto" runat="server" Tipo="NombreCampo" Width="100px" enabled="true" TipoCase="TextoLibre"
							visible="false"></snt:textboxadv></TD>
				</TR>
			</TABLE>
			<TABLE id="Table6" cellSpacing="0" cellPadding="0" width="100%">
				<TR>
					<TD class="label"><snt:label id="lblLink" runat="server" Tipo="NombreCampo">Link:</snt:label></TD>
				</TR>
				<TR>
					<TD><snt:textboxadv id="txtLink" runat="server" Tipo="NombreCampo" enabled="true" TextMode="MultiLine"
							Width="100%"></snt:textboxadv></TD>
				</TR>
			</TABLE></TD>
			</TR>
			</TABLE>
			<TABLE class="TableContenido1" cellSpacing="0" cellPadding="0" width="100%">
				<TR>
					<TD align="left"><snt:button id="btnGuardar" runat="server" Tipo="Guardar" CssClass="btnGrabar"></snt:button></TD>
					<TD align="left"><snt:button id="btnNuevo" runat="server" Tipo="Nuevo" CssClass="btnNuevo" CausesValidation="False"></snt:button></TD>
					<TD align="left"><snt:button id="btnEliminar" runat="server" Tipo="Eliminar" CssClass="btnEliminar"></snt:button></TD>
					<TD align="right"><snt:button id="btnVolver" runat="server" Tipo="Volver" CssClass="btnVolver"></snt:button></TD>
				</TR>
			</TABLE>
		</TD>
		<TD class="C_right2" width="13"></TD>
	</TR>
	<TR>
		<TD class="C_left3" width="13"></TD>
		<TD class="C_bottom" width="640"></TD>
		<TD class="C_right3" width="13"></TD>
	</TR>
</TABLE>
<snt:textboxadv id="TxtColumnaId" runat="server" Tipo="NombreCampo" Width="148px" enabled="false"
	TipoCase="TextoLibre" visible="false"></snt:textboxadv>
