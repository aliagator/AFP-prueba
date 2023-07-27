<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ucIniciarProceso.ascx.vb" Inherits="Sonda.Net.Nxt.Web.ucIniciarProceso" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<P>Ejecución de procesos largos mediante URL</P>
<P>Para ejecutar un proceso largo mediante una URL se deben especificar los 
	siguientes parámetros</P>
<TABLE id="Table1" border="1" cellSpacing="1" cellPadding="1" width="100%">
	<TR>
		<TD style="WIDTH: 141px">Parametro</TD>
		<TD style="WIDTH: 384px">Descripción</TD>
		<TD>Ejemplo</TD>
		<TD>Requerido</TD>
		<TD>Valor predeterminado</TD>
	</TR>
	<TR>
		<TD style="WIDTH: 141px">IDUNICO</TD>
		<TD style="WIDTH: 384px">Identificador único del proceso, debe ser un identificador 
			que no se repita.</TD>
		<TD>emifactura0021</TD>
		<TD>SI</TD>
		<TD>&nbsp;</TD>
	</TR>
	<TR>
		<TD style="WIDTH: 141px">DESC</TD>
		<TD style="WIDTH: 384px">Glosa descriptiva del proceso</TD>
		<TD>Proceso de emisión de factura #21</TD>
		<TD>NO</TD>
		<TD>"Proceso "&nbsp;+ Idunico</TD>
	</TR>
	<TR>
		<TD style="WIDTH: 141px">CLASE</TD>
		<TD style="WIDTH: 384px">Clase que implementa el proceso largo</TD>
		<TD>Sonda.Gestion.Fin700.BL.RhuRemMovMensuales</TD>
		<TD>SI</TD>
		<TD>&nbsp;</TD>
	</TR>
	<TR>
		<TD style="WIDTH: 141px">METODO</TD>
		<TD style="WIDTH: 384px">Método que implementa el proceso largo</TD>
		<TD>RutinaMuyMuyLarga</TD>
		<TD>SI</TD>
		<TD>&nbsp;</TD>
	</TR>
	<TR>
		<TD style="WIDTH: 141px">PARAMNAMES</TD>
		<TD style="WIDTH: 384px">Nombre de los parámetros que recibe el método separados 
			por el caracter '|'</TD>
		<TD>PARAM1|PARAM2|PARAM3</TD>
		<TD>NO</TD>
		<TD>(Vacio)</TD>
	</TR>
	<TR>
		<TD style="WIDTH: 141px">PARAMVALUES</TD>
		<TD style="WIDTH: 384px">Valores de los parámetros que recibe el método separados 
			por el caracter '|', debe coincidir con la cantidad de parametros recibidos en 
			PARAMVALUES</TD>
		<TD>1|hola|10/10/2013</TD>
		<TD>NO</TD>
		<TD>(Vacio)</TD>
	</TR>
	<TR>
		<TD style="WIDTH: 141px">USU</TD>
		<TD style="WIDTH: 384px">Usuario que ejecuta el proceso</TD>
		<TD>Usuario del sistema</TD>
		<TD>SI</TD>
		<TD>&nbsp;</TD>
	</TR>
	<TR>
		<TD style="WIDTH: 141px">PWD</TD>
		<TD style="WIDTH: 384px">Password del usuario que ejecuta el proceso</TD>
		<TD>Password del usuario USU</TD>
		<TD>SI</TD>
		<TD>&nbsp;</TD>
	</TR>
	<TR>
		<TD style="WIDTH: 141px">FUN</TD>
		<TD style="WIDTH: 384px">Funcionalidad desde la que se ejecuta el proceso</TD>
		<TD>Funcionalidad desde la que se ejecuta el proceso</TD>
		<TD>NO</TD>
		<TD>PNLUSERCONTROL</TD>
	</TR>
	<TR>
		<TD style="WIDTH: 141px">USERCONTROL</TD>
		<TD style="WIDTH: 384px">User control donde se redirige al usuario para ver el 
			resultado del proceso</TD>
		<TD>WebRhuRem/ucResultadoRutinaMuyMuyLarga</TD>
		<TD>SI</TD>
		<TD>&nbsp;</TD>
	</TR>
	<TR>
		<TD style="WIDTH: 141px">CONCUUSU</TD>
		<TD style="WIDTH: 384px">Concurrencia del proceso por usuario</TD>
		<TD>1</TD>
		<TD>NO</TD>
		<TD>1</TD>
	</TR>
	<TR>
		<TD style="WIDTH: 141px">CONCTOTAL</TD>
		<TD style="WIDTH: 384px">Concurrencia del proceso total</TD>
		<TD>1</TD>
		<TD>NO</TD>
		<TD>1</TD>
	</TR>
</TABLE>
<P>Revise la documentación de procesos largos en nuestro <a href="http://herramientas/wiki/index.php?title=Procesos_Largos#Utilizaci.C3.B3n">
		wiki</a></P>
<P>&nbsp;</P>
