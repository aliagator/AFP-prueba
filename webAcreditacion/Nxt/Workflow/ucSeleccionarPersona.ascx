<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ucSeleccionarPersona.ascx.vb" Inherits="Sonda.Net.Nxt.Workflow.WEB.ucSeleccionarPersona" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="400">
	<TR>
		<TD class="tit_left"></TD>
		<TD class="tit_center">
			<snt:label id="Label2" runat="server">Seleccione el usuario al cual asignará la tarea:</snt:label></TD>
		<TD class="tit_right"></TD>
	</TR>
</TABLE>
<TABLE id="TablaBusqueda" style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="400">
	<TR>
		<TD class="Label">Usuario</TD>
		<TD class="Casilla" width="160">
			<snt:TextBoxAdv id="txtNombre" Tipo="NombreCampo" runat="server" TipoCase="TextoLibre" Width="100%"></snt:TextBoxAdv></TD>
		</TD>
		<TD>
			<snt:button id="BtnBuscar" Tipo="Buscar" runat="server"></snt:button>
		</TD>
	</TR>
</TABLE>
<TABLE class="TableContenido1" id="Table2" cellSpacing="0" cellPadding="0" width="98%"
	border="0">
	<TR>
		<TD>
			<snt:TablaPaginada id="TablaPaginada1" MostrarColumnaRadio="True" runat="server">
				<snt:TablaPaginadaColumna Ancho="" Visible="False" Titulo="Id." ColId="ID" NombreCampo="ID" EsLlave="True"
					EsParametro="True" />
				<snt:TablaPaginadaColumna Ancho="" Titulo="Nombre" ColId="NOMBRE" NombreCampo="NOMBRE" EsLlave="True" EsParametro="True" />
				<snt:TablaPaginadaColumna Ancho="" Visible="False" Titulo="ID_PERSONA" ColId="ID_PERSONA" NombreCampo="ID_PERSONA"
					EsLlave="True" EsParametro="True" />
			</snt:TablaPaginada><snt:Button id="btnSeleccionar" runat="server" Tipo="Seleccionar" CssClass="btnSinTexto" MensajeConfirmacion="¿Esta seguro que desea asignar al usuario seleccionado?"
				ConfirmarAccion="True"></snt:Button>
			<snt:Button id="Button2" runat="server" Tipo="Cancelar" JavaScriptOnClick="window.close(); return false"
				CssClass="btnSinTexto"></snt:Button>
		</TD>
	</TR>
</TABLE>
