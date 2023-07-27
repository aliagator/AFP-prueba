<%@ Register TagPrefix="cc1" Namespace="Sonda.Net.Validator" Assembly="SondaNetWebUI" %>
<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="inePlanillaTesoreria.ascx.vb" Inherits="Sonda.Gestion.Adm.WEB.IngresoEgreso.Acreditacion.inePlanillaTesoreria" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<%@ Register TagPrefix="cc2" Namespace="Sonda.Net.Validator" Assembly="SondaNetWebUI" %>
<style type="text/css">
    .auto-style1 {
        width: 209px;
    }
    .auto-style2 {
        width: 209px;
        height: 25px;
    }
</style>
<p align="left">
    <table class="TablaContenedora" id="Table1" bordercolor="#006699" cellspacing="1" cellpadding="1" width="98%" border="0">
        <tr>
            <td>
                <div align="left">
                    <table class="TablaTitulo" id="Table7" cellspacing="1" cellpadding="1" width="100%" align="left" border="0">
                        <tr>
                            <td class="ttFilaTitulo">
                                <p align="center">
                                    <cc1:Label ID="Label2" Height="57px" runat="server" Width="640px" Tipo="Titulo">Carga Planilla Tesoreria</cc1:Label>
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
                        <td class="auto-style2">
                            <p align="right">&nbsp;</p>
                        </td>
                        <%--<td class="tfFilaEtiqueta" style="width: 2px">
                            <label id="lblId" runat="server" width="55px" tipo="NotaMediana">Fecha Inicial</label>
                        </td>
                        <td class="tfFilaEtiqueta" style="width: 48px">
                            <label id="lblPeriodoIncioCaja" runat="server" width="55px" tipo="NotaMediana">Fecha Final</label>
                        </td>--%>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <cc1:Label ID="lblTimbreCaja" runat="server" Width="267px" Tipo="NotaMediana" Etiqueta="Vacío">Seleccionar Plantilla Excel</cc1:Label>
                        </td>
                        <td class="tfFilaDato" style="width: 48px; height: 26px">
                            <p align="left">
                                <input id="fileCargar" type="file" size="42" name="fileCargar" runat="server">
                            </p>
                        </td>
                        <td class="tfFilaDato" style="width: 48px; height: 26px">
                            <p align="left">
                                <cc1:Button ID="btnImportarArchivo" runat="server" Tipo="TextoLibre" Etiqueta="Abrir Planilla Excel" ConfirmarAccion="False"></cc1:Button>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <cc1:Label ID="lblFechaArch" runat="server" Width="267px" Tipo="NotaMediana" Etiqueta="Vacío">Fecha Archivo Seleccionado</cc1:Label>
                        </td>
                        <td class="tfFilaDato" style="width: 48px; height: 26px">
                            <p align="left">
                                <cc1:Label ID="lblFecha" runat="server" Width="267px" Tipo="NotaMediana" Etiqueta="Vacío"></cc1:Label>
                            </p>
                        </td>
                        <td class="tfFilaDato" style="width: 48px; height: 26px">
                            <p align="left">
                                <cc1:Button ID="btnGrabarPlanilla" runat="server" Tipo="TextoLibre" Etiqueta="Grabar Planilla" ConfirmarAccion="False" OnClick="btnGrabarPlanilla_Click"></cc1:Button>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <p align="left">
                                <p align="right">&nbsp;</p>
                            </p>
                        </td>
                        <td>
                            <p align="left">
                                <p align="right">&nbsp;</p>
                            </p>
                        </td>
                        <td>
                            <p align="left">
                                <cc1:Button ID="btnInformeCargaPlanilla" runat="server" Tipo="TextoLibre" Etiqueta="Informe Carga Planilla" ConfirmarAccion="False" OnClick="btnInformeCargaPlanilla_Click"></cc1:Button>
                            </p>
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
        
    </table>
    <table class="TablaContenedora" id="Table2" bordercolor="#006699" cellspacing="1" cellpadding="1" width="98%" border="0">
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
