<%@ Register TagPrefix="cc1" Namespace="Sonda.Net.Validator" Assembly="SondaNetWebUI" %>
<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ineDetallePlanillaTesoreria.ascx.vb" Inherits="Sonda.Gestion.Adm.WEB.IngresoEgreso.Acreditacion.ineDetallePlanillaTesoreria" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<%@ Register TagPrefix="cc2" Namespace="Sonda.Net.Validator" Assembly="SondaNetWebUI" %>
<p align="left">
    <table class="TablaContenedora" id="Table1" bordercolor="#006699" cellspacing="1" cellpadding="1" width="98%" border="0">
        <tr>
            <td>
                <div align="left">
                    <table class="TablaTitulo" id="Table7" cellspacing="1" cellpadding="1" width="100%" align="left" border="0">
                        <tr>
                            <td class="ttFilaTitulo">
                                <p align="center">
                                    <cc1:Label ID="Label2" Height="57px" runat="server" Width="640px" Tipo="Titulo">Detalle Planilla Tesorería</cc1:Label>
                                </p>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table class="TablaGrupoDato" id="Table8" style="height: 80px" cellspacing="1" cellpadding="1" width="100%" border="0" runat="server">
                    <tr>
                        <td class="tfFilaEtiqueta" style="width: 45px">
                            <p align="right">&nbsp;</p>
                        </td>
                        <td class="tfFilaEtiqueta" style="width: 2px">&nbsp;</td>
                        <td class="tfFilaEtiqueta" style="width: 48px">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="tfFilaEtiqueta" style="width: 2px">
                            <cc1:Label ID="lblTimbreCaja" runat="server" Width="55px" Tipo="NotaMediana" Etiqueta="Vacío">Fecha Acreditación</cc1:Label>
                        </td>
                        <td class="tfFilaDato" style="width: 48px; height: 26px">
                            <p align="left">
                                <cc1:FechaAdv ID="fechaPlanilla" runat="server" Tipo="NombreCampo" AutoPostBack="True" Despliegue="DDMMAAAA" Requerido="True" GuardarEnHistoria="True"></cc1:FechaAdv>
                            </p>
                        </td>
                        <td class="tfFilaDato" style="width: 48px; height: 26px">
                            <p align="left">
                                <cc1:Button ID="btnNuevo" runat="server" Tipo="Cargar" Etiqueta="Información" ConfirmarAccion="False" OnClick="btnNuevo_Click"></cc1:Button>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td class="tfFilaEtiqueta" style="width: 2px">&nbsp;</td>
                        <td class="tfFilaDato" style="width: 48px; height: 26px">
                            <p align="left">
                                &nbsp;
                            </p>
                        </td>
                        <td class="tfFilaDato" style="width: 48px; height: 26px">
                            <p align="left">
                                &nbsp;
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 129px"></td>
                        <td style="width: 430px">&nbsp;</td>
                        <td>
                            <cc1:Button ID="btnImprimir" runat="server" Tipo="Imprimir"></cc1:Button>
                            <cc1:Button ID="btnDescarga" runat="server" Tipo="Descargar"></cc1:Button>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td class="tfFilaDato">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <p align="right">
                    <p align="right">&nbsp;</p>
                </p>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="height: 41px">
                <p align="left">
                    <cc1:DataGridSnd ID="DataGridSnd2" runat="server" Width="100%" Tipo="NotaMediana"></cc1:DataGridSnd>
                </p>
            </td>
        </tr>

    </table>
</p>
<p align="left">
    <cc1:MsgBoxSnd ID="MsgBoxSnd1" runat="server"></cc1:MsgBoxSnd>
</p>
<p align="right">&nbsp;</p>
<p align="right">&nbsp;</p>
