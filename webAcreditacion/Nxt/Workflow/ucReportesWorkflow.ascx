<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ucReportesWorkflow.ascx.vb" Inherits="Sonda.Net.Nxt.Workflow.WEB.ucReportesWorkflow" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="snt" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<style type="text/css">
    .style1 { HEIGHT: 16px; WIDTH: 30% }
</style>
	<TABLE style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="100%">

	     <TR>
			<TD class="Titulo" align="left">Reportes Workflow</TD>
		</TR>
		<TR>
			<TD class="casilla"> </TD>
		</TR>


	</TABLE>


<asp:panel id="pnlReportes" runat="server" Width="100%">
	<TABLE style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="100%">
		<TR>
			<TD class="tit_left"></TD>
			<TD class="tit_center">
				<asp:label id="lblReportes" runat="server">Reportes</asp:label></TD>
			<TD class="tit_right"></TD>
		</TR>
	</TABLE>
<TABLE id="Table1" class="TableContenido1" cellSpacing="0" cellPadding="0" width="100%"
		border="0" runat="server">
		<TR>
			<TD class="casilla" width="70%">
				<snt:RadioButtonList id="RadioButtonReportes" runat="server" Tipo="NombreCampo" RepeatDirection="Vertical"  AutoPostBack="True">
					<asp:ListItem Value="REPORTES_CASOS" Selected="True">Casos</asp:ListItem>
					<asp:ListItem Value="REPORTES_CASOS_POR_TAREA">Casos por Tarea</asp:ListItem>
					<asp:ListItem Value="REPORTES_CASOS_POR_DIAGRAMA">Casos por Diagrama</asp:ListItem>
					<asp:ListItem Value="REPORTES_HISTORICO">Histórico</asp:ListItem>
					<asp:ListItem Value="REPORTES_HISTORICO_POR_TAREA">Histórico por Tarea</asp:ListItem>
				</snt:RadioButtonList></TD>
			<TD align="right"></TD>
		</TR>
   		<TR>
			<TD class="casilla" width="70%"></TD>
			<TD align="right"/>
		</TR>

	</TABLE>
	
</asp:panel>

<asp:panel id="pnlFiltroUsuario" runat="server" Width="100%">

	<TABLE style="HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="100%">
		<TR>
			<TD class="tit_left"></TD>
			<TD class="tit_center">
				<asp:label id="lblFiltroUsuario" runat="server">Filtros</asp:label></TD>
			<TD class="tit_right"></TD>
		</TR>
	</TABLE>
	<TABLE class="TableContenido1" cellSpacing="0" cellPadding="0" width="100%" border="0">
		<TR>
			<TD>
				<TABLE id="Table2" style="HEIGHT: 20px" cellSpacing="1" cellPadding="1" width="100%" border="0">
					<TR>
						<TD class="label" style="HEIGHT: 24px; WIDTH: 76px">
							<snt:label id="Label3" runat="server" Tipo="NombreCampo">Id.Usuario</snt:label></TD>
						<TD class="casilla" style="HEIGHT: 24px; WIDTH: 150px">
							<snt:dropdownlist id="ddlUsuario" runat="server" Tipo="NombreCampo" GuardarEnHistoria="True" AutoPostBack="True"
								AgregarDatoVacio="True" TextoDatoVacio="(Todos)"></snt:dropdownlist></TD>
						<TD class="label" style="HEIGHT: 24px; WIDTH: 75px">
							<snt:label id="Label5" runat="server" Tipo="NombreCampo">Nombre</snt:label></TD>
						<TD class="casilla" style="HEIGHT: 24px">
							<snt:label id="lblNombre" runat="server" Tipo="NombreCampo">Administrador</snt:label></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
	</TABLE>
</asp:panel>
<TABLE style="HEIGHT: 20px; WIDTH: 100%" class="TableContenido1" border="0" cellSpacing="0"
	cellPadding="0">
	<TR>
		<TD style="HEIGHT: 20px">
			<TABLE style="HEIGHT: 20px; WIDTH: 100%" border="0" cellSpacing="0" cellPadding="0" align="center">
				<TBODY>
					<TR>
						<TD class="style1">
							<snt:label id="Label15" runat="server" Tipo="NombreCampo">Proyecto:</snt:label></TD>
						<TD class="style1" align="left">&nbsp;
							<snt:label id="Label4" runat="server" Tipo="NombreCampo">Diagrama:</snt:label></TD>
						<TD class="style1" align="left">&nbsp;
							<snt:label id="Label11" runat="server" Tipo="NombreCampo">Tarea:</snt:label></TD>
					</TR>
					<TR>
						<TD style=" HEIGHT: 16px" class="casilla">
							<snt:dropdownlist id="ddlProyecto" runat="server" Tipo="NombreCampo" GuardarEnHistoria="True" AutoPostBack="True"
								AgregarDatoVacio="True" TextoDatoVacio="(Seleccione un Proyecto)" Width="250px"></snt:dropdownlist></TD>
						<TD style=" HEIGHT: 16px" class="casilla">
							<snt:dropdownlist id="ddlDiagrama" runat="server" Tipo="NombreCampo" GuardarEnHistoria="True" AutoPostBack="True"
								AgregarDatoVacio="True" TextoDatoVacio="(Seleccione un Diagrama)" Width="250px"></snt:dropdownlist></TD>
						<TD style=" HEIGHT: 16px" class="casilla">
							<snt:dropdownlist id="ddlTarea" runat="server" Tipo="NombreCampo" GuardarEnHistoria="True" AutoPostBack="True"
								AgregarDatoVacio="True" TextoDatoVacio="(Seleccione una Tarea)" Width="250px"></snt:dropdownlist></TD>
					</TR>
				</TBODY>
			</TABLE>
			<TABLE class="TableContenido1" style="HEIGHT: 20px; WIDTH: 100%" cellSpacing="0" cellPadding="0"
				border="0">
				<TR>
					<TD style="HEIGHT: 20px">
						<TABLE style="HEIGHT: 20px; WIDTH: 100%" cellSpacing="0" cellPadding="0" align="center"
							border="0">
							<TR>
								<TD class="label" style="WIDTH: 339px">
								    Desde:</TD>
								<TD class="label" style="WIDTH: 331px" align="left">
									<snt:label id="Label13" runat="server" Tipo="NombreCampo">Hasta:</snt:label></TD>
							</TR>
							<TR>
								<TD class="casilla" style="HEIGHT: 17px; WIDTH: 339px">
									<snt:fechaadv id="FechaDesde" runat="server" GuardarEnHistoria="True" Enabled="False"></snt:fechaadv></TD>
								<TD class="casilla" style="HEIGHT: 17px; WIDTH: 331px" align="left">
									<snt:fechaadv id="FechaHasta" runat="server" GuardarEnHistoria="True" Enabled="False"></snt:fechaadv></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
		<TR>
			<TD align="right"></TD>
		</TR>
   		<TR>
			<TD align="right">
				<snt:button id="btnGenerar" runat="server" Tipo="Generar" CssClass="btnGenerar"></snt:button>
				<snt:button id="btnVolver" runat="server" Tipo="Volver" CssClass="btnVolver" Visible="True"></snt:button></TD>
		</TR>

</TABLE>
