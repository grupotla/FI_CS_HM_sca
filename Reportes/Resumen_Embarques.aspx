<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Resumen_Embarques.aspx.cs" Inherits="Reportes_Resumen_Embarques" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<table align="center" cellpadding="0" cellspacing="0" style=" width:1100px; height: 420px; background-color:White;">
            <tr>
                <td>
                    <br />
                    <table align="center" cellpadding="0" cellspacing="0" class="style3">
                        <tr>
                            <td align="left">
                                <strong>EMBARQUES CONTABILIZADOS</strong></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center">
                    <table align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" colspan="2">
                                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                                </asp:ScriptManager>
                            </td>
                    </tr>
                    <tr>
                        <td align="left" width="150px">
                            Linea de Servicio
                        </td>
                        <td height="30px">
                            <asp:DropDownList ID="drp_linea_servicio" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="middle">
                            Fecha de Contabilizacion</td>
                        <td height="80px" width="200px">
                            <table align="center" cellpadding="0" cellspacing="0" class="style4">
                                <tr>
                                    <td>
                                        Desde</td>
                                    <td width="30px">
                                        &nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="tb_fecha_inicial" runat="server" Height="16px" Width="80px"></asp:TextBox>
                                        <ajaxToolkit:MaskedEditExtender ID="tb_fecha_inicial_MaskedEditExtender" 
                                            runat="server" Enabled="True" Mask="99/99/9999" MaskType="Date" 
                                            TargetControlID="tb_fecha_inicial">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <ajaxToolkit:CalendarExtender ID="tb_fecha_inicial_CalendarExtender" 
                                            runat="server" Enabled="True" Format="MM/dd/yyyy" 
                                            TargetControlID="tb_fecha_inicial">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Hasta</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="tb_fecha_final" runat="server" Height="16px" Width="80px"></asp:TextBox>
                                        <ajaxToolkit:MaskedEditExtender ID="tb_fecha_final_MaskedEditExtender" 
                                            runat="server" Enabled="True" Mask="99/99/9999" MaskType="Date" 
                                            TargetControlID="tb_fecha_final">
                                        </ajaxToolkit:MaskedEditExtender>
                                        <ajaxToolkit:CalendarExtender ID="tb_fecha_final_CalendarExtender" 
                                            runat="server" Enabled="True" Format="MM/dd/yyyy" 
                                            TargetControlID="tb_fecha_final">
                                        </ajaxToolkit:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btn_buscar" runat="server" Text="Buscar" 
                                onclick="btn_buscar_Click" Font-Bold="True" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btn_regresar" runat="server" PostBackUrl="~/Home.aspx" 
                                Text="Salir" Font-Bold="True" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            &nbsp;</td>
                    </tr>
                    </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                                            <cr:crystalreportviewer ID="CrystalReportViewer1" runat="server" 
                                                                AutoDataBind="true" HasToggleGroupTreeButton="False" 
                                                                PageZoomFactor="90" ToolPanelView="None" DisplayStatusbar="False" 
                                                                HasDrilldownTabs="False" ToolPanelWidth="200px" HasPrintButton="False" 
                                                                HasZoomFactorList="False" />
                                                        </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
</table>
</asp:Content>

