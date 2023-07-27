<%@ Register TagPrefix="cc1" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="MisReportes.ascx.vb" Inherits="Sonda.Gestion.Adm.WEB.MisReportes" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<cc1:DataGridSnd id="dgsMisReportes" runat="server" Tipo="NombreCampo" PageSize="15" Width="100%"></cc1:DataGridSnd>
<cc1:HyperLink id="hlnkAdminFormatoImpresion" runat="server" UserControl="AdminFormatoImpresion" Visible="False">Administrar formatos predeterminados de reportes.</cc1:HyperLink>