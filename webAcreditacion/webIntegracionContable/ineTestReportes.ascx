<%@ Register TagPrefix="cc1" Namespace="Sonda.Net.Validator" Assembly="SondaNetWebUI" %>
<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ineTestReportes.ascx.vb" Inherits="Sonda.Gestion.Adm.WEB.IngresoEgreso.Acreditacion.ineTestReportes" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
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
                                    <cc1:Label ID="Label2" Height="57px" runat="server" Width="640px" Tipo="Titulo">Generacion Informe Recaudacion y Detalle</cc1:Label>
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
                        <td class="tfFilaEtiqueta" style="width: 2px">
                            <label id="lblId" runat="server" width="55px" tipo="NotaMediana">Fecha Inicial</label>
                        </td>
                        <td class="tfFilaEtiqueta" style="width: 48px">
                            <label id="lblPeriodoIncioCaja" runat="server" width="55px" tipo="NotaMediana">Fecha Final</label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tfFilaEtiqueta" style="width: 2px">
                            <cc1:Label ID="lblTimbreCaja" runat="server" Width="55px" Tipo="NotaMediana">Timbre Caja</cc1:Label>
                        </td>
                        <td class="tfFilaDato" style="width: 48px; height: 26px">
                            <p align="left">
                                <cc1:FechaAdv ID="fechaIniCaja" runat="server" Tipo="NombreCampo" AutoPostBack="True" Despliegue="DDMMAAAA" Requerido="True" GuardarEnHistoria="True"></cc1:FechaAdv>
                            </p>
                        </td>
                        <td class="tfFilaDato" style="width: 48px; height: 26px">
                            <p align="left">
                                <cc1:FechaAdv ID="fechaFinCaja" runat="server" Tipo="NombreCampo" AutoPostBack="True" Despliegue="DDMMAAAA" Requerido="True" GuardarEnHistoria="True"></cc1:FechaAdv>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td class="tfFilaEtiqueta" style="width: 2px">
                            <cc1:Label ID="lblFechaAcre" runat="server" Width="55px" Tipo="NotaMediana">Fecha Acreditacion</cc1:Label>
                        </td>
                        <td class="tfFilaDato" style="width: 48px; height: 26px">
                            <p align="left">
                                <cc1:FechaAdv ID="fechaIniRecaudacion" runat="server" Tipo="NombreCampo" AutoPostBack="True" Despliegue="DDMMAAAA" Requerido="True" GuardarEnHistoria="True"></cc1:FechaAdv>
                            </p>
                        </td>
                        <td class="tfFilaDato" style="width: 48px; height: 26px">
                            <p align="left">
                                <cc1:FechaAdv ID="fechaFinRecaudacion" runat="server" Tipo="NombreCampo" AutoPostBack="True" Despliegue="DDMMAAAA" Requerido="True" GuardarEnHistoria="True"></cc1:FechaAdv>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 129px"></td>
                        <td style="width: 430px">
                            <cc1:RadioButtonList ID="rblTipo" Tipo="NombreCampo" runat="server" AutoPostBack="True" GuardarEnHistoria="True" Height="29px">
                                <asp:ListItem Value="1" Selected="True">Normal</asp:ListItem>
                                <asp:ListItem Value="2" Selected="True">Azul 1</asp:ListItem>
                                <asp:ListItem Value="3">Azul 2</asp:ListItem>
                                <asp:ListItem Value="4">Sin Documento</asp:ListItem>
                                <asp:ListItem Value="5">Sin Documento. Rojo 1</asp:ListItem>
                                <asp:ListItem Value="6">Sin Documento. Rojo 2</asp:ListItem>
                            </cc1:RadioButtonList>

                        </td>
                        <td>
                            <cc1:Button ID="btnNuevo" runat="server" Tipo="Nuevo" Etiqueta="Proceso" ConfirmarAccion="False" OnClick="btnNuevo_Click"></cc1:Button>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p align="right">
                                <p align="right">&nbsp;</p>
                            </p>
                        </td>
                        <td>
                            <p align="right">
                                <p align="right">&nbsp;</p>
                            </p>
                        </td>
                        <td class="tfFilaDato">
                            <cc1:Button ID="btnImprimir" runat="server" Tipo="Imprimir"></cc1:Button>
                            <cc1:Button ID="btnDescarga" runat="server" Tipo="Descargar"></cc1:Button>
                        </td>
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
        <tr>
            <td style="height: 41px">
                <p align="left">
                    <cc1:DataGridSnd ID="DataGridSnd1" runat="server" Width="100%" Tipo="NotaMediana"></cc1:DataGridSnd>
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
