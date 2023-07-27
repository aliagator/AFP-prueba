<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ucSeleccionarFlecha.ascx.vb" Inherits="Sonda.Net.Nxt.Workflow.WEB.ucSeleccionarFlecha" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<P>
	<TABLE id="Table1" style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="400">
		<TR>
			<TD class="tit_left"></TD>
			<TD class="tit_center">
				<snt:label id="Label2" runat="server">Seleccione la flecha por la que se despachará el expediente:</snt:label></TD>
			<TD class="tit_right"></TD>
		</TR>
	</TABLE>
	<TABLE class="TableContenido1" id="Table2" cellSpacing="0" cellPadding="0" width="98%"
		border="0">
		<TR>
			<TD>
				<snt:TablaPaginada id="TablaPaginada1" MostrarColumnaRadio="True" runat="server">
					<snt:TablaPaginadaColumna Ancho="" Visible="False" Titulo="Id." ColId="ID" NombreCampo="ID" EsLlave="True"
						EsParametro="True" />
					<snt:TablaPaginadaColumna Ancho="" Titulo="Titulo" ColId="TITULO" NombreCampo="TITULO" EsLlave="True" EsParametro="True" />
				</snt:TablaPaginada>
				<snt:Button id="btnSeleccionar" runat="server" Tipo="Seleccionar" CssClass="btnSinTexto"></snt:Button>
				<snt:Button id="Button2" runat="server" Tipo="Cancelar" JavaScriptOnClick="window.close();return false;" CssClass="btnSinTexto"></snt:Button></TD>
		</TR>
	</TABLE>
</P>
