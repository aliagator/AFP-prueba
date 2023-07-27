<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ucFiltroAdicionalMant.ascx.vb" Inherits="Sonda.Net.Nxt.Workflow.WEB.ucFiltroAdicionalMant" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" align="center" border="0">
	<TR>
		<TD colSpan="3">
			<TABLE class="tablatit" id="Table2" cellSpacing="0" cellPadding="0" width="95%">
				<TR>
					<TD class="titppal"></TD>
					<TD class="titppal2"><snt:label id="Label2" runat="server">Mantención Filtros</snt:label></TD>
					<TD class="titppal3"></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
	<TR>
		<TD class="C_left" width="13"></TD>
		<TD class="C_center" width="424"></TD>
		<TD class="C_right" width="13"></TD>
	</TR>
	<TR>
		<TD class="C_left2" width="13"></TD>
		<TD>
			<TABLE class="TableContenido1" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD>
						<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="100%" border="0">
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
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="Table3" cellSpacing="1" cellPadding="1" border="0">
				<TR>
					<TD vAlign="top" width="300">
						<TABLE id="Table4" style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="200">
							<TR>
								<TD class="tit_left"></TD>
								<TD class="tit_center"><asp:label id="Label10" runat="server">Filtro No Asociado</asp:label></TD>
								<TD class="tit_right"></TD>
							</TR>
						</TABLE>
						<TABLE class="TableContenido1" height="265" cellSpacing="0" cellPadding="0" width="100%"
							border="0">
							<TR>
								<TD vAlign="top"><snt:tablapaginada id="tpFiltroNoAsociado" runat="server" NombreCampoColumnaCheck="Sel" MostrarColumnaCheck="True">
										<snt:TablaPaginadaColumna Ancho="" Titulo="Campo" ColId="NOMBRECAMPO" NombreCampo="NOMBRECAMPO" NombreParametro="NOMBRECAMPO"
											EsLlave="True" EsParametro="True" />
										<snt:TablaPaginadaColumna Ancho="" Titulo="Tipo Dato" ColId="TIPODATO" NombreCampo="TIPODATO" NombreParametro="TIPODATO"
											EsLlave="True" EsParametro="True" />
									</snt:tablapaginada></TD>
							</TR>
						</TABLE>
					</TD>
					<TD style="WIDTH: 91px">
						<P><ASP:BUTTON id="btnAsociar" runat="server" Width="100%" Text="  Agregar >" CssClass="btnSinTexto"
								DESIGNTIMEDRAGDROP="117"></ASP:BUTTON></P>
						<P><asp:button id="btnDesasociar" runat="server" Width="100%" Text="  Eliminar <" CssClass="btnSinTexto"
								DESIGNTIMEDRAGDROP="118"></asp:button></P>
					</TD>
					<TD vAlign="top" colSpan="3">
						<TABLE id="Table4" style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="200">
							<TR>
								<TD class="tit_left"></TD>
								<TD class="tit_center"><asp:label id="Label1" runat="server">Filtro Asociado</asp:label></TD>
								<TD class="tit_right"></TD>
							</TR>
						</TABLE>
						<TABLE class="TableContenido1" height="265" cellSpacing="0" cellPadding="0" width="100%"
							border="0">
							<TR>
								<TD vAlign="top"><snt:tablapaginada id="tpFiltroAsociado" runat="server" NombreCampoColumnaCheck="Sel" MostrarColumnaCheck="True">
										<snt:TablaPaginadaColumna Ancho="" Titulo="Atributo" ColId="ATRIBUTO" NombreCampo="ATRIBUTO" NombreParametro="ATRIBUTO"
											EsLlave="True" EsParametro="True" EsHyperlink="True" hlnk_AltoVentana="0" hlnk_AnchoVentana="0" hlnk_AutoPostback="True"
											hlnk_ID="ATRIBUTO" />
										<snt:TablaPaginadaColumna Ancho="" Titulo="Titulo" ColId="TITULO" NombreCampo="TITULO" NombreParametro="TITULO"
											EsLlave="True" EsParametro="True" />
										<snt:TablaPaginadaColumna Ancho="" Titulo="Tipo" ColId="TIPO" NombreCampo="TIPO" NombreParametro="TIPO" EsLlave="True"
											EsParametro="True" />
									</snt:tablapaginada></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD></TD>
					<TD style="WIDTH: 81px"></TD>
					<TD class="label" style="WIDTH: 76px"><snt:label id="Label3" runat="server" Tipo="NombreCampo">Titulo</snt:label></TD>
					<TD colSpan="1"><snt:textboxadv id="Titulo" runat="server" Width="148px" Tipo="NombreCampo" TipoCase="TextoLibre"></snt:textboxadv></TD>
					<TD class="casilla"><snt:checkbox id="chkTipo" runat="server" Tipo="Titulo" Text="Tipo Lista" Enabled="False"></snt:checkbox></TD>
				<TR>
					<TD></TD>
					<TD></TD>
					<TD style="WIDTH: 76px"></TD>
					<TD align="right">
						<snt:button id="btnGuardar" runat="server" Tipo="Guardar" CssClass="btnGuardar"></snt:button></TD>
					<TD align="right"><snt:button id="btnVolver" runat="server" Tipo="Volver" CssClass="btnVolver"></snt:button></TD>
				</TR>
			</TABLE>
			<TABLE id="Table6" cellSpacing="1" cellPadding="1" width="100%" border="0">
				<TR>
					<TD></TD>
					<TD align="right"></TD>
				</TR>
			</TABLE>
		</TD>
		<TD class="C_right2" width="13"></TD>
	</TR>
	<TR>
		<TD class="C_left3" width="13"></TD>
		<TD class="C_bottom" width="424"></TD>
		<TD class="C_right3" width="13"></TD>
	</TR>
</TABLE>
<P>&nbsp;</P>
