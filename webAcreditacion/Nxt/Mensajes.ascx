<%@ Control Language="vb" AutoEventWireup="false" Codebehind="Mensajes.ascx.vb" Inherits="Sonda.Net.Nxt.Web.Mensajes" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False"%>
<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<table id="table1" cellSpacing="0" cellPadding="0" width="500" style="PADDING-LEFT: 10px; PADDING-TOP: 10px"
	runat="server">
	<tr>
		<td>
			<table id="tblCabecera" style="HEIGHT: 20px" cellSpacing="0" cellPadding="0" width="300"
				runat="server">
				<tr>
					<td class="tit_left"></td>
					<td class="tit_center">
						<asp:Label id="Label10" runat="server">Mensajes</asp:Label></td>
					<td class="tit_right"></td>
				</tr>
			</table>
			<asp:DataGrid id="DataGrid1" runat="server" Width="480px" AutoGenerateColumns="False" CssClass="NombreCampo"
				EnableViewState="False">
				<Columns>
					<asp:BoundColumn DataField="Severidad" DataFormatString="&lt;img src='img\severidad\sev{0}s.gif'/&gt;">
						<HeaderStyle Width="5%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Codigo" ReadOnly="True" HeaderText="C&#243;digo">
						<HeaderStyle Width="5%"></HeaderStyle>
					</asp:BoundColumn>
					<asp:BoundColumn DataField="Message" HeaderText="Descripci&#243;n">
						<ItemStyle Width="400px"></ItemStyle>
					</asp:BoundColumn>
				</Columns>
			</asp:DataGrid>
			<snt:HyperLink id="HyperLink1" runat="server" Modal="True" NuevaVentana="True" visible="False"
				AnchoVentana="500" AltoVentana="300" ScrollVentana="True" NombreVentana="VentanaPopupError"
				PaginaContenedora="defaultblank.aspx"></snt:HyperLink>
			<p align="center">
				<snt:Button id="btnCerrar" runat="server" JavaScriptOnClick="window.close();return true;" Tipo="CerrarVentana"
					Visible="False"></snt:Button>
			</p>
		</td>
	</tr>
</table>
