<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="contabilizar.aspx.cs" Inherits="Operaciones_contabilizar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

    <style type="text/css">
        .style1
        {
            width: 95%;
        }
        .style2
        {
            width: 92%;
        }
        .style4
        {
            font-size: small;
        }
        .style5
        {
            width: 100%;
        }
        .style9
        {
            width: 90%;
        }
        .style20
    {
        font-size: x-small;
        color: #3366FF;
        font-weight:bold;
    }
        .style21
        {
            width: 777px;
        }
        .style23
        {
            width: 250px;
        }
        .style25
        {
            width: 85%;
        }
        .style26
        {
            width: 70%;
        }
        </style>
        <script  type="text/javascript">
            function BloquearPantalla() {
                $.blockUI({ message: '<h1>Procesando...</h1>' });
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_endRequest(function () {
                    $.unblockUI();
                });
            }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table align="center" cellpadding="0" cellspacing="0" class="style1">
                <tr>
                    <td align="center">
                        <table align="center" cellpadding="0" cellspacing="15" class="style2" 
                            bgcolor="White">
                            <tr>
                                <td bgcolor="White" colspan="2" align="center">
                                    <table align="center" cellpadding="0" cellspacing="0" class="style1" 
                                        bgcolor="#3366FF" style=" display:none;" >
                                        <tr>
                                            <td align="center" valign="middle" class="style21">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="middle">
                                                <table align="center" cellpadding="0" cellspacing="0" class="style25">
                                                    <tr>
                                                        <td align="center" bgcolor="Black" height="30px" valign="middle">
                                                <asp:Label ID="lbl_empresa" runat="server" 
                                                    style="font-weight: 700; font-size: medium; color: #FFFFFF;"></asp:Label>
                                                <asp:Label ID="lbl_empresaID" runat="server" 
                                                    style="font-weight: 700; color: #FFFFFF; font-size: medium;" 
                                                    Visible="False"></asp:Label>
                                                <asp:Label ID="lbl_shipper" runat="server" 
                                                    style="font-weight: 700; color: #FFFFFF" Visible="False"></asp:Label>
                                                <asp:Label ID="lbl_cliente" runat="server" 
                                                    style="font-weight: 700; color: #FFFFFF" Visible="False"></asp:Label>
                                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                                <Scripts>
                                                    <asp:ScriptReference Path="~/Scripts/jquery-1.4.1.min.js" />
                                                    <asp:ScriptReference Path="~/Scripts/jquery.blockUI.js" />
                                                </Scripts>
                                                </asp:ScriptManager>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td bgcolor="White">
                                                <table align="center" cellpadding="0" cellspacing="0" class="style9" 
                                                    bgcolor="Black">
                                                    <tr bgcolor="White">
                                                        <td width="15px">
                                                            &nbsp;</td>
                                                        <td class="style20">
                                                            SISTEMA</td>
                                                        <td>
                                                <asp:Label ID="lbl_sistema" runat="server" 
                                                    style="font-weight: 700; color: #000000; font-size: x-small;"></asp:Label>
                                                <asp:Label ID="lbl_sistemaID" runat="server" 
                                                    style="font-weight: 700; color: #000000; font-size: x-small;" Visible="False"></asp:Label>
                                                            <asp:Label ID="lbl_sesion_id" runat="server" Text="0" Visible="False"></asp:Label>
                                                        </td>
                                                        <td class="style20">
                                                            LINEA DE SERVICIO</td>
                                                        <td>
                                                <asp:Label ID="lbl_linea_servicio" runat="server" 
                                                    style="font-weight: 700; color: #000000; font-size: x-small;"></asp:Label>
                                                <asp:Label ID="lbl_ttoID" runat="server" 
                                                    style="font-weight: 700; color: #000000; font-size: x-small;" Visible="False"></asp:Label>
                                                        </td>
                                                        <td class="style20">
                                                            TIPO</td>
                                                        <td>
                                                <asp:Label ID="lbl_tipo" runat="server" 
                                                    style="font-weight: 700; color: #000000; font-size: x-small;"></asp:Label>
                                                        </td>
                                                        <td width="15px">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr bgcolor="White">
                                                        <td class="style20">
                                                            &nbsp;</td>
                                                        <td class="style20">
                                                            USUARIO</td>
                                                        <td>
                                                <asp:Label ID="lbl_usuario" runat="server" 
                                                    style="font-weight: 700; color: #000000; font-size: x-small;"></asp:Label>
                                                        </td>
                                                        <td class="style20">
                                                            NAVIERA</td>
                                                        <td>
                                                <asp:Label ID="lbl_naviera" runat="server" 
                                                    
                                                                style="font-weight: 700; color: #000000; font-size: x-small; background-color: #FFFFFF;"></asp:Label>
                                                            <asp:Label ID="lbl_naviera_id" runat="server" Text="0" Visible="False"></asp:Label>
                                                        </td>
                                                        <td class="style20">
                                                            AGENTE</td>
                                                        <td>
                                                <asp:Label ID="lbl_agente" runat="server" 
                                                    style="font-weight: 700; color: #000000; font-size: x-small;"></asp:Label>
                                                            <asp:Label ID="lbl_agente_id" runat="server" Text="0" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr bgcolor="White">
                                                        <td>
                                                            &nbsp;</td>
                                                        <td height="5px">
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                <asp:Label ID="lbl_mbl" runat="server" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                <asp:Label ID="lbl_viaje_no" runat="server" Visible="False"></asp:Label>
                                                        </td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="middle" class="style21">
                                                &nbsp;</td>
                                        </tr>
                                        </table>
                                    <asp:Panel ID="pnl_contabilizacion" runat="server" Visible="False">
                                        <table cellpadding="0" cellspacing="0" class="style26">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lbl_confirmacion" runat="server" Font-Bold="True" 
                                                        Font-Italic="True" Font-Size="Medium"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:Button ID="btn_siguiente" runat="server" Font-Bold="True" 
                                                        Font-Italic="True" Text="Siguiente" onclick="btn_siguiente_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td width="1550px" align="center" valign="top">
                                                            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                                                                AutoDataBind="true" HasToggleGroupTreeButton="False" 
                                                                PageZoomFactor="90" ToolPanelView="None" DisplayStatusbar="False" 
                                                                HasDrilldownTabs="False" ToolPanelWidth="200px" HasPrintButton="False" 
                                                                HasZoomFactorList="False" />
                                                        </td>
                                <td align="center" valign="top" class="style23">
                                    <table align="center" bgcolor="White" cellpadding="0" cellspacing="0" 
                                        class="style5" border="1px">
                                        <tr>
                                            <td>
                                    <table align="center" cellpadding="0" cellspacing="0" class="style2" 
                                        bgcolor="White">                                        
                                        <tr>
                                            <td class="style4" align="center">
                                                <strong>Cuestionario                                             </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btn_regresar" runat="server" BackColor="White" 
                                                    BorderStyle="None" Font-Italic="True" ForeColor="Black" 
                                                    onclick="btn_regresar_Click" Text="Regresar" Font-Bold="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btn_finalizar" runat="server" BackColor="White" 
                                                    BorderStyle="None" Font-Italic="True" ForeColor="Black" Text="Finalizar" 
                                                    Font-Bold="True" PostBackUrl="~/Home.aspx" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btn_contabilizar" runat="server" BackColor="White" 
                                                    BorderStyle="None" Font-Italic="False" ForeColor="#000099" 
                                                    Text="Contabilizar BAW" Font-Bold="True" OnClientClick="javascript:BloquearPantalla();" UseSubmitBehavior="false" 
                                                    onclick="btn_contabilizar_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="1550px" align="center" valign="top" colspan="2">
                                    <asp:Label ID="lbl_error" runat="server" Font-Bold="True" ForeColor="Red" 
                                        Visible="False"></asp:Label>
                                            </td>
                            </tr>
                            </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
</asp:Content>

