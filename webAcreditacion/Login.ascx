<%@ Control AutoEventWireup="false" Inherits="Sonda.Gestion.Adm.WEB.Login" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" CodeBehind="Login.ascx.vb" Language="vb" %>
<%@ Register TagPrefix="cc2" Namespace="Sonda.Net.Validator" Assembly="SondaNetWebUI" %>
<%@ Register TagPrefix="cc1" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>

<!-- Script para para slider - http://jquery.malsup.com/cycle2/ -->
<script src="https://malsup.github.io/min/jquery.cycle2.min.js"></script>

<asp:panel id="Panel1" Visible="False" runat="server">

<div id="login">
	<figure class="logo-sonda">
		<img src="img/2018/logo_sonda_login.png" alt="Sonda S.A"/>
	</figure>

	<div id="form-login">
		<div id="login-titulo">
			<h2>Ingreso al Sistema</h2>
		</div>
		<div id="login-credenciales">
			<label for="TxtIdPersona">
				<cc1:textbox id="TxtIdPersona" runat="server" Tipo="NombreCampo" tabindex="1"></cc1:textbox>
				<cc2:requiredvalidator id="RequiredValidator2" runat="server" ControlToValidate="TxtIdPersona"></cc2:requiredvalidator>
			</label>
			<label for="TxtClave">
				<cc1:textbox id="TxtClave" runat="server" Tipo="NombreCampo" TextMode="Password" tabindex="2"></cc1:textbox>
				<cc2:RequiredValidator id="RequiredValidator1" runat="server" ControlToValidate="TxtClave"></cc2:RequiredValidator>
			</label>
		</div>
		<div id="errores">
			<cc1:Label id="lblmsgNoExiste" runat="server" Tipo="NotaMediana"></cc1:Label>
		</div>
		<div id="login-botones">
			<cc1:button id="Btnsiguiente" runat="server" Tipo="Siguiente" Text="Iniciar Sesión" tabindex="4"></cc1:button>
		</div>
		<a id="btnRecuperarClave" href="default.aspx?url=webControlAccesoUsuario/usuRecuperarPassword&amp;FUN=USULOGIN">
			<cc1:label id="lblRecuperarContrasena" runat="server" Tipo="NotaMediana">Haga click aquí para reestablecer su contraseña.</cc1:label>
		</a>
	</div>
	
</div>

<!-- Slider -->
<div id="bg-login">
	<span class="bg-img pag-1-2"></span>
</div>

</asp:panel>

<asp:panel id="Panel2" Visible="False" runat="server">
	<div id="logout">
		<p>¿Está Seguro que desea terminar la sesión de usuario?</p>

		<div class="botones">
			<cc1:button id="Btnterminar" runat="server" Tipo="Terminar" Etiqueta="Sesión" tabindex="6" CssClass="btnSinEstilo"></cc1:button>
			<cc1:HyperLink id="hlnkCancelar" runat="server" MostrarComoBoton="True" TipoBoton="Cancelar" UserControl="Home" CssClass="btnSinEstilo"></cc1:HyperLink>
		</div>
	</div>
</asp:panel>

<P align="left"><asp:panel id="Panel3" Visible="False" Height="89px" Width="343px" runat="server"></P>
<P align="left"><cc1:label id="Label5" runat="server" Tipo="Titulo">Password Expirada debe Cambiarla</cc1:label></P>
<P align="left">
	<TABLE id="Table3" style="HEIGHT: 75px; WIDTH: 347px" cellSpacing="1" cellPadding="1" width="347"
		border="0">
		<TR>
			<TD colspan=2  align=left><cc1:label id="lblTituloClaveAntigua" runat="server" Tipo="NotaMediana">Password Antigua</cc1:label></TD>
		</TR>			
		<TR>
			<TD height="1" colspan = 2 align=left>
				<P align="left"><cc1:textbox id="TxtClaveAntigua" runat="server" Tipo="NombreCampo" TextMode="Password" tabindex="7"></cc1:textbox></P>
			</TD>	
       </TR>		
		<TR>
			<TD><cc1:label id="Label4" runat="server" Tipo="NotaMediana">Nueva Password</cc1:label></TD>
			<TD><cc1:label id="Label3" runat="server" Tipo="NotaMediana">Repita Nueva Password</cc1:label></TD>
		</TR>
		<TR>
			<TD height="1">
				<P align="left"><cc1:textbox id="Txtnewpass" Width="149px" runat="server" Tipo="NombreCampo" TextMode="Password"
						tabindex="8"></cc1:textbox>
					<cc2:CompareValidator id="CompareValidator1" runat="server" ControlToValidate="Txtnewpass" TextValidator="Asterisco"
						ErrorMessage="Las claves no coinciden" ControlToCompare="Txtnewpass2">*</cc2:CompareValidator></P>
			</TD>
			<TD height="1">
				<P align="left"><cc1:textbox id="Txtnewpass2" runat="server" Tipo="NombreCampo" TextMode="Password" tabindex="9"></cc1:textbox></P>
			</TD>
		</TR>
		<TR>
			<TD><cc2:requiredvalidator id="RequiredValidator4" runat="server" ControlToValidate="Txtnewpass"></cc2:requiredvalidator></TD>
			<TD><cc2:requiredvalidator id="RequiredValidator3" runat="server" ControlToValidate="Txtnewpass2"></cc2:requiredvalidator></TD>
		</TR>
		<TR>
			<TD></TD>
			<TD>
				<P align="right"><cc1:button id="Btnguardar" runat="server" Tipo="Guardar"></cc1:button></P>
			</TD>
		</TR>
	</TABLE>
</P>
<P align="left"><cc1:label id="Lblmsg" runat="server" Tipo="SubTitulo"></cc1:label></P>
<P align="left"></asp:panel></P>
<cc1:MsgBoxSnd id="msgContrasenaExpiraraEnNDias" runat="server" tipo="YesNoDlg"></cc1:MsgBoxSnd>