<%@ Control Language="vb" AutoEventWireup="false" Codebehind="Home.ascx.vb" Inherits="Sonda.Gestion.Adm.WEB.Home" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<%@ Register TagPrefix="cc2" Namespace="Sonda.Net.Validator" Assembly="SondaNetWebUI" %>
<P align="center">&nbsp;</P>
<P align="center">&nbsp;</P>
<P align="center">
<cc1:Label id="Label1" runat="server" Tipo="Titulo" visible="False"></cc1:Label></P>
<!-- ESTE CODIGO REDIRECCIONA SEGUN LOS PARAMETROS PASADOS AL FRAMESET !-->
<!-- PARA PODER ENVIAR UN URL CON UN LINK AL SITIO, INCLUSO PASANDO PARÁMETROS MEDIANTE PARURL !-->
<script language="javascript" src="js\2_9_9_1\FramesetParameters.js"></script>
<script language="javascript">
	
	var aux = "";
	
	if (getParameter(framesetQuery,"url") ) aux = aux + "&url=" + getParameter(framesetQuery,"url") ;
	if (getParameter(framesetQuery,"PARURL") ) aux = aux + "&PARURL=" + getParameter(framesetQuery,"PARURL") ;
	if (getParameter(framesetQuery,"FUN") ) aux = aux + "&FUN=" + getParameter(framesetQuery,"FUN") ;
	if (getParameter(framesetQuery,"MODO") ) aux = aux + "&MODO=" + getParameter(framesetQuery,"MODO") ;
	
		
	if (aux != "") {
		this.location.replace("default.aspx?" + aux.substring(1) );
	}

</script>
<!-- FIN : ESTE CODIGO REDIRECCIONA SEGUN LOS PARAMETROS PASADOS AL FRAMESET !-->