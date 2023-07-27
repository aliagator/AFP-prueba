<%@ Control Language="vb" AutoEventWireup="false" Inherits="Sonda.Net.Nxt.Web.ucListaProcesosLargos" TargetSchema="http://schemas.microsoft.com/intellisense/ie3-2nav3-0" %>
<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<table cellSpacing="0" cellPadding="0" width="95%" align="center" border="0">
	<tr>
		<td colSpan="3">
			<table class="tablatit" cellSpacing="0" cellPadding="0" width="95%">
				<tr>
					<td class="titppal"></td>
					<td class="titppal2"><snt:label id="Label2" runat="server" cssclass="titppal2" Tipo="NombreCampo">Lista de procesos</snt:label></td>
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
<snt:Label id="Label1" runat="server" DESIGNTIMEDRAGDROP="7" Tipo="NombreCampo">Seleccione el dia:</snt:Label>
<snt:DropDownList id="DropDownList1" runat="server" AutoPostBack="True" Tipo="NombreCampo"></snt:DropDownList>
<snt:Button id="btnActualizar" Tipo="Actualizar" runat="server" cssclass="btnactualizar"></snt:Button><BR>
<BR>
<snt:TablaPaginada id="TablaPaginada1" runat="server" NroRegistros="20">
	<snt:TablaPaginadaColumna Ancho="" Titulo="Id" ColId="IdUnico" NombreCampo="IdUnico" EsLlave="True" EsParametro="True" />
	<snt:TablaPaginadaColumna Ancho="" Titulo="Descripcion" ColId="Descripcion" NombreCampo="Descripcion" />
	<snt:TablaPaginadaColumna Ancho="" Titulo="Estado" ColId="Estado" NombreCampo="Estado" />
	<snt:TablaPaginadaColumna Ancho="" Titulo="Inicio" ColId="FechaHoraInicio" NombreCampo="FechaHoraInicio" Formato="Fecha"
		FormatoFecha="DDMMAAAAhhmmss" />
	<snt:TablaPaginadaColumna Ancho="" Titulo="Fin" ColId="FechaHoraFin" NombreCampo="FechaHoraFin" Formato="Fecha"
		FormatoFecha="DDMMAAAAhhmmss" />
	<snt:TablaPaginadaColumna Ancho="" Titulo="" ColId="VerDetalle" NombreCampo="" EsHyperlink="True" Formato="Personalizado"
		FormatoPersonalizado="Ver Detalle" hlnk_AutoPostback="True" hlnk_ID="VerDetalle" />
	<snt:TablaPaginadaColumna Ancho="" Titulo="" ColId="Cancelar" NombreCampo="" EsHyperlink="True" Formato="Personalizado"
		FormatoPersonalizado="Cancelar" hlnk_AutoPostback="True" hlnk_ID="Cancelar" />
</snt:TablaPaginada>
		</td>
		<td class="C_right2" width="13"></td>
	</tr>
	<tr>
		<td class="C_left3" width="13"></td>
		<td class="C_bottom" width="424"></td>
		<td class="C_right3" width="13"></td>
	</tr>
</table>
