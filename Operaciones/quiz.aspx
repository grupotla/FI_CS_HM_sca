<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="quiz.aspx.cs" Inherits="reconciliacion_carga_quiz" enableEventValidation="true" %>
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
        .style3
        {
            font-size: small;
        }
        .style4
        {
            font-size: small;
        }
        .style5
        {
            width: 100%;
        }
        .style6
        {
            width: 400px;
        }
        .style9
        {
            width: 90%;
            height: 34px;
        }
        .style10
        {
            width: 164px;
        }
        .style11
        {
            width: 1100px;
        }
        .style12
        {
            font-size: small;
            height: 17px;
        }
        .style13
        {
            height: 17px;
        }
        .style14
        {
            font-size: small;
            width: 350px;
        }
        .style17
        {
            font-weight: bold;
            font-style: italic;
        }
        .style20
        {
            font-size: x-small;
            color: #3366FF;
            font-weight:bold;
        }
        .style22
        {
            width: 60%;
        }
        .style23
        {
            width: 255px;
        }
        .style24
        {
            height: 250px;
        }
        .style25
        {
            width: 85%;
        }
        .style27
        {
            width: 50%;
        }
        .style28
        {
            width: 70%;
        }
        .style29
        {
            width: 75%;
        }
        .style30
        {
            font-size: medium;
        }
        .style31
        {
            height: 22px;
        }
        .style32
        {
            height: 21px;
        }
        .style33
        {
            width: 80%;
        }
        .style34
        {
            text-decoration: underline;
        }
        .style35
        {
            width: 94%;
        }
        .style36
        {
            font-size: xx-small;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table align="center" cellpadding="0" cellspacing="0" class="style1">
                <tr>
                    <td align="center">
                        <table align="center" cellpadding="0" cellspacing="15" class="style2" 
                            bgcolor="White">
                            <tr>
                                <td bgcolor="White" colspan="3" align="center">
                                    <table align="center" cellpadding="0" cellspacing="0" class="style1" >
                                        <tr>
                                            <td align="center" valign="middle">
                                                <table align="center" cellpadding="0" cellspacing="0" class="style25" border="1px">
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
                                                        <td class="style20">
                                                            &nbsp;</td>
                                                        <td class="style20">
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td class="style20">
                                                            &nbsp;</td>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td class="style20">
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
                                        </table>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="White" colspan="3" align="center">
                                                            <span class="style30" style="color: #3366FF; font-weight: bold;">TIPO CAMBIO.:</span>&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lbl_tipo_cambio" runat="server" 
                                                    
                                                                
                                                                
                                                                style="font-weight: 700; color: #000000; font-size: medium; background-color: #FFFFFF;">0.00</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="350px" align="center" valign="top">
                                    <table align="center" bgcolor="#E5E5E5" cellpadding="0" cellspacing="0" 
                                        class="style5" border="1px">
                                        <tr>
                                            <td class="style10">
                                    <table align="center" cellpadding="0" cellspacing="0" class="style2" 
                                        bgcolor="#E5E5E5">
                                        <tr>
                                            <td align="center" width="10px">
                                                &nbsp;</td>
                                            <td class="style3" align="center">
                                                <strong>BL&#39;s</strong></td>
                                            <td align="center" width="10px">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                &nbsp;</td>
                                            <td align="left">
                                                <strong><em>Master</em></strong></td>
                                            <td align="left">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="center" height="25">
                                                &nbsp;</td>
                                            <td align="center" height="25">
                                                <asp:Label ID="lbl_mbl" runat="server"></asp:Label>
                                                <asp:Label ID="lbl_blID" runat="server" Text="0" Visible="False"></asp:Label>
                                                <asp:Label ID="lbl_viajeID" runat="server" Text="0" Visible="False"></asp:Label>
                                                <asp:Label ID="lbl_mbl_trb_id" runat="server" Text="0" Visible="False"></asp:Label>
                                                <asp:Label ID="lbl_mbl_estado" runat="server" Text="1" Visible="False"></asp:Label>
                                            </td>
                                            <td align="center" height="25">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="center" height="25">
                                                &nbsp;</td>
                                            <td align="left" height="17px">
                                                <strong><em>Viaje No.</em></strong></td>
                                            <td align="center" height="25">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="center" height="25">
                                                &nbsp;</td>
                                            <td align="center" height="20px">
                                                <asp:Label ID="lbl_viaje_no" runat="server"></asp:Label>
                                            </td>
                                            <td align="center" height="25">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="left" height="25">
                                                &nbsp;</td>
                                            <td align="left" height="25">
                                                <strong><em>Houses<asp:Label ID="lbl_hbl_id" runat="server" Text="0" Visible="False"></asp:Label>
                                                <asp:Label ID="lbl_hbl_correlativo" runat="server" Text="0" Visible="False"></asp:Label>
                                                <asp:Label ID="lbl_hbl" runat="server" Text="0" Visible="False"></asp:Label>
                                                <asp:Label ID="lbl_hbl_trb_id" runat="server" Text="0" Visible="False"></asp:Label></em></strong>
                                                <asp:Label ID="lbl_hbl_tipo_carga" runat="server" 
                                                    style="font-weight: 700; font-style: italic" Text="0" Visible="False"></asp:Label>
                                                <asp:Label ID="lbl_hbl_destino_final" runat="server" style="font-weight: 700; font-style: italic" Text="0" Visible="False"></asp:Label>
                                                <asp:Label ID="lbl_hbl_cliente" runat="server" style="font-weight: 700; font-style: italic" Text="0" Visible="False"></asp:Label>
                                                <asp:Label ID="lbl_hbl_tipo_rebate" runat="server" 
                                                    style="font-weight: 700; font-style: italic" Text="0" Visible="False" 
                                                    Font-Bold="False"></asp:Label>
                                            </td>
                                            <td align="left" height="25">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                &nbsp;</td>
                                            <td align="center">
                                                <asp:GridView ID="gv_houses" runat="server" Font-Size="XX-Small" 
                                                    onrowcreated="gv_houses_RowCreated" onrowdeleting="gv_houses_RowDeleting">
                                                    <Columns>
                                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/img/icons/delete.png" 
                                                            ShowDeleteButton="True" Visible="False" />
                                                    </Columns>
                                                    <HeaderStyle BackColor="#99CCFF" ForeColor="Black" />
                                                </asp:GridView>
                                            </td>
                                            <td align="center">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                &nbsp;</td>
                                            <td align="center">
                                                &nbsp;</td>
                                            <td align="center">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                &nbsp;</td>
                                            <td align="center" colspan="0">
                                                <table align="center" cellpadding="0" cellspacing="0" class="style22">
                                                    <tr>
                                                        <td>
        
                <asp:Label ID="lbl_estado1" runat="server" BackColor="#33CC33" Height="10px" 
                    Width="10px" ToolTip="Respondido"></asp:Label>
                                                        </td>
                                                        <td>
        
                <asp:Label ID="lbl_estado2" runat="server" BackColor="#0033CC" Height="10px" 
                    Width="10px" ToolTip="Respondiendo"></asp:Label>
                                                        </td>
                                                        <td>
        
                <asp:Label ID="lbl_estado3" runat="server" BackColor="Black" Height="10px" 
                    Width="10px" ToolTip="Sin Responder"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="center">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                &nbsp;</td>
                                            <td align="center">
                                                &nbsp;</td>
                                            <td align="center">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td bgcolor="#99CCFF" height="300px" align="center" valign="top" style=" border:solid 2px;" 
                                    class="style11">
                                    <table align="center" cellpadding="0" cellspacing="0" class="style2" >
                                        <tr>
                                            <td>
                                                <h2>&nbsp;
                                                    <asp:Label ID="lbl_no_pregunta" runat="server" Text="-" Visible="False"></asp:Label>
                                                </h2>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnl_detalle_hbl" runat="server" Visible="False">
                                                    <table align="center" cellpadding="0" cellspacing="0" 
    class="style22">
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td width="55px">
                                                                <strong><em>Consignatario:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </em></strong></td>
                                                            <td>
                                                                <asp:Label ID="lbl_nombre_cliente" runat="server" 
                                                                    style="font-size: x-small; font-weight: 700; font-style: italic"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="100px">
                                                                <strong><em>Puerto Salida:</em></strong></td>
                                                            <td>
                                                                <asp:Label ID="lbl_puerto_origen_id" runat="server" 
                                                                    style="font-size: x-small; font-weight: 700; font-style: italic" 
                                                                    Visible="False"></asp:Label>
                                                                <asp:Label ID="lbl_puerto_origen" runat="server" 
                                                                    style="font-size: x-small; font-weight: 700; font-style: italic"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:MultiView ID="MultiView1" runat="server">
                                                    <asp:View ID="View1" runat="server">
                                                        <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                            <tr>
                                                                <td align="left">
                                                                    <h2>
                                                                        Bienvenido</h2>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left">
                                                                    <br />
                                                                    Para iniciar el proceso de Contabilizacion de clic en boton Iniciar</td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" height="40" valign="middle">
                                                                    <asp:Button ID="btn_iniciar" runat="server" Text="Iniciar" 
                                                                        onclick="btn_iniciar_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:View>
                                                    <asp:View ID="View2" runat="server">
                                                        <asp:Panel ID="pnl_pregunta1_0" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style1">
                                                                <tr>
                                                                    <td align="left" height="20" 
                                                                        style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                                                                        <strong>SOA&#39;S</strong>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="20">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style35">
                                                                            <tr>
                                                                                <td align="left" class="style34" width="600px">
                                                                                    <strong><em>SOA DEL AGENTE</em></strong></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    Por favor ingrese los valores totales por cobrar y pagar al Agente:</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Panel ID="pnl_totales_agente" runat="server">
                                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style33">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Panel ID="pnl_agente_cobrar_local" runat="server" Visible="False">
                                                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style9">
                                                                                                            <tr>
                                                                                                                <td width="300px" align="right">
                                                                                                                    Cobro en Moneda Local</td>
                                                                                                                <td align="center" width="300px">
                                                                                                                    <asp:TextBox ID="tb_agente_cobrar_local" runat="server" Height="16px" style="text-align:right"
                                                                                                                        Width="100px">0.00</asp:TextBox>
                                                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator35" 
                                                                                                                        runat="server" ControlToValidate="tb_agente_cobrar_local" 
                                                                                                                        ErrorMessage="Error ###.##" SetFocusOnError="True" 
                                                                                                                        ValidationExpression="\d+.\d{2}">
                                                                                                                    </asp:RegularExpressionValidator>
                                                                                                                    <cc1:FilteredTextBoxExtender ID="tb_agente_cobrar_local_FilteredTextBoxExtender" 
                                                                                                                        runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                                                                        TargetControlID="tb_agente_cobrar_local" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                    <asp:Panel ID="pnl_agente_pagar_local" runat="server" Visible="False">
                                                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style9">
                                                                                                            <tr>
                                                                                                                <td width="300px" align="right">
                                                                                                                    Pago en Moneda Local</td>
                                                                                                                <td align="center" width="300px">
                                                                                                                    <asp:TextBox ID="tb_agente_pagar_local" runat="server" Height="16px" style="text-align:right"
                                                                                                                        Width="100px">0.00</asp:TextBox>
                                                                                                                    <cc1:FilteredTextBoxExtender ID="tb_agente_pagar_local_FilteredTextBoxExtender" 
                                                                                                                        runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                                                                        TargetControlID="tb_agente_pagar_local" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator36" 
                                                                                                                        runat="server" ControlToValidate="tb_agente_pagar_local" 
                                                                                                                        ErrorMessage="Error ###.##" SetFocusOnError="True" 
                                                                                                                        ValidationExpression="\d+.\d{2}">
                                                                                                                    </asp:RegularExpressionValidator>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                    <asp:Panel ID="pnl_agente_cobrar_usd" runat="server" Visible="False">
                                                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style9">
                                                                                                            <tr>
                                                                                                                <td width="300px" align="right">
                                                                                                                    Cobro en USD</td>
                                                                                                                <td align="center" width="300px">
                                                                                                                    <asp:TextBox ID="tb_agente_cobrar_usd" runat="server" Height="16px" style="text-align:right"
                                                                                                                        Width="100px">0.00</asp:TextBox>
                                                                                                                    <cc1:FilteredTextBoxExtender ID="tb_agente_cobrar_usd_FilteredTextBoxExtender" 
                                                                                                                        runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                                                                        TargetControlID="tb_agente_cobrar_usd" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator37" 
                                                                                                                        runat="server" ControlToValidate="tb_agente_cobrar_usd" 
                                                                                                                        ErrorMessage="Error ###.##" SetFocusOnError="True" 
                                                                                                                        ValidationExpression="\d+.\d{2}">
                                                                                                                    </asp:RegularExpressionValidator>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                    <asp:Panel ID="pnl_agente_pagar_usd" runat="server" Visible="False">
                                                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style9">
                                                                                                            <tr>
                                                                                                                <td width="300px" align="right">
                                                                                                                    Pago en USD</td>
                                                                                                                <td align="center" width="300px">
                                                                                                                    <asp:TextBox ID="tb_agente_pagar_usd" runat="server" Height="16px" style="text-align:right"
                                                                                                                        Width="100px">0.00</asp:TextBox>
                                                                                                                    <cc1:FilteredTextBoxExtender ID="tb_agente_pagar_usd_FilteredTextBoxExtender" 
                                                                                                                        runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                                                                        TargetControlID="tb_agente_pagar_usd" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator38" 
                                                                                                                        runat="server" ControlToValidate="tb_agente_pagar_usd" 
                                                                                                                        ErrorMessage="Error ###.##" SetFocusOnError="True" 
                                                                                                                        ValidationExpression="\d+.\d{2}">
                                                                                                                    </asp:RegularExpressionValidator>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" class="style34" width="600px">
                                                                                    <strong><em>SOA DE LA NAVIERA</em></strong></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    Por favor ingrese los valores totales por pagar a la Naviera:</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <asp:Panel ID="pnl_totales_naviera" runat="server">
                                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style33">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Panel ID="pnl_naviera_cobrar_local" runat="server" Visible="False">
                                                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style9">
                                                                                                            <tr>
                                                                                                                <td width="300px" align="right">
                                                                                                                    Cobro en Moneda Local</td>
                                                                                                                <td align="center" width="300px">
                                                                                                                    <asp:TextBox ID="tb_naviera_cobrar_local" runat="server" Height="16px" style="text-align:right"
                                                                                                                        Width="100px">0.00</asp:TextBox>
                                                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator39" 
                                                                                                                        runat="server" ControlToValidate="tb_naviera_cobrar_local" 
                                                                                                                        ErrorMessage="Error ###.##" SetFocusOnError="True" 
                                                                                                                        ValidationExpression="\d+.\d{2}">
                                                                                                                    </asp:RegularExpressionValidator>
                                                                                                                    <cc1:FilteredTextBoxExtender ID="tb_naviera_cobrar_local_FilteredTextBoxExtender" 
                                                                                                                        runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                                                                        TargetControlID="tb_naviera_cobrar_local" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                    <asp:Panel ID="pnl_naviera_pagar_local" runat="server" Visible="False">
                                                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style9">
                                                                                                            <tr>
                                                                                                                <td width="300px" align="right">
                                                                                                                    Pago en Moneda Local</td>
                                                                                                                <td align="center" width="300px">
                                                                                                                    <asp:TextBox ID="tb_naviera_pagar_local" runat="server" Height="16px" style="text-align:right"
                                                                                                                        Width="100px">0.00</asp:TextBox>
                                                                                                                    <cc1:FilteredTextBoxExtender ID="tb_naviera_pagar_local_FilteredTextBoxExtender" 
                                                                                                                        runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                                                                        TargetControlID="tb_naviera_pagar_local" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator40" 
                                                                                                                        runat="server" ControlToValidate="tb_naviera_pagar_local" 
                                                                                                                        ErrorMessage="Error ###.##" SetFocusOnError="True" 
                                                                                                                        ValidationExpression="\d+.\d{2}">
                                                                                                                    </asp:RegularExpressionValidator>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                    <asp:Panel ID="pnl_naviera_cobrar_usd" runat="server" Visible="False">
                                                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style9">
                                                                                                            <tr>
                                                                                                                <td width="300px" align="right">
                                                                                                                    Cobro en USD</td>
                                                                                                                <td align="center" width="300px">
                                                                                                                    <asp:TextBox ID="tb_naviera_cobrar_usd" runat="server" Height="16px" style="text-align:right"
                                                                                                                        Width="100px">0.00</asp:TextBox>
                                                                                                                    <cc1:FilteredTextBoxExtender ID="tb_naviera_cobrar_usd_FilteredTextBoxExtender0" 
                                                                                                                        runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                                                                        TargetControlID="tb_naviera_cobrar_usd" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator41" 
                                                                                                                        runat="server" ControlToValidate="tb_naviera_cobrar_usd" 
                                                                                                                        ErrorMessage="Error ###.##" SetFocusOnError="True" 
                                                                                                                        ValidationExpression="\d+.\d{2}">
                                                                                                                    </asp:RegularExpressionValidator>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                    <asp:Panel ID="pnl_naviera_pagar_usd" runat="server" Visible="False">
                                                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style9">
                                                                                                            <tr>
                                                                                                                <td width="300px" align="right">
                                                                                                                    Pago en USD</td>
                                                                                                                <td align="center" width="300px">
                                                                                                                    <asp:TextBox ID="tb_naviera_pagar_usd" runat="server" Height="16px" style="text-align:right"
                                                                                                                        Width="100px">0.00</asp:TextBox>
                                                                                                                    <cc1:FilteredTextBoxExtender ID="tb_naviera_pagar_usd_FilteredTextBoxExtender0" 
                                                                                                                        runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                                                                        TargetControlID="tb_naviera_pagar_usd" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator42" 
                                                                                                                        runat="server" ControlToValidate="tb_naviera_pagar_usd" 
                                                                                                                        ErrorMessage="Error ###.##" SetFocusOnError="True" 
                                                                                                                        ValidationExpression="\d+.\d{2}">
                                                                                                                    </asp:RegularExpressionValidator>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </asp:Panel>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente34" runat="server" 
                                                                            onclick="btn_siguiente34_Click" style="height: 26px" 
                                                                            Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="4" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_1" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                    <h2>Master</h2>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <br />
                                                                        Cómo es el Documento Master?
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_1" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl_pregunta1" runat="server" 
                                                                            RepeatDirection="Horizontal">
                                                                            <asp:ListItem>Prepagado</asp:ListItem>
                                                                            <asp:ListItem>Collect</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar48" runat="server" onclick="btn_regresar48_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente_pregunta1" runat="server" 
                                                                            onclick="btn_siguiente_pregunta1_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_2" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_20" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <br />
                                                                        Master prepagado, no se contabiliza<br /> nada relacionado al Ocean Freight del 
                                                                        Master.</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente8_pregunta1" runat="server" Text="Regresar" 
                                                                            onclick="btn_siguiente8_pregunta1_Click" />
                                                                        &nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente2_pregunta1" runat="server" 
                                                                            onclick="btn_siguiente2_pregunta1_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_3" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_2" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        <strong>Ingrese el monto del Flete Maritimo de la Factura de la Naviera (MBL)</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="30px">
                                                                        Monto</td>
                                                                    <td>
                                                                        <asp:TextBox ID="tb_monto_factura_naviera" runat="server" Height="16px" 
                                                                            Width="100px">0.00</asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="tb_monto_factura_naviera_FilteredTextBoxExtender1" 
                                                                            runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                            TargetControlID="tb_monto_factura_naviera" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                                                            ControlToValidate="tb_monto_factura_naviera" ErrorMessage="Error ###.##" 
                                                                            SetFocusOnError="True" ValidationExpression="\d+.\d{2}">
                                                                                    </asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Moneda</td>
                                                                    <td>
                                                                        <asp:DropDownList ID="drp_p1_moneda" runat="server">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        <strong>Datos Factura de la Naviera</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2" height="45px">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style9">
                                                                            <tr>
                                                                                <td>
                                                                                    Serie</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_serie_proveedor2" runat="server" Height="16px" Width="75px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    Correlativo</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_correlativo_proveedor2" runat="server" Height="16px" 
                                                                                        Width="75px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    Fecha</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_fecha_proveedor2" runat="server" Height="16px" 
                                                                                        Width="128px"></asp:TextBox>
                                                                                    <cc1:MaskedEditExtender ID="tb_fecha_proveedor2_MaskedEditExtender" 
                                                                                        runat="server" Enabled="True" Mask="99/99/9999" MaskType="Date" 
                                                                                        TargetControlID="tb_fecha_proveedor2"></cc1:MaskedEditExtender>
                                                                                    <cc1:CalendarExtender ID="tb_fecha_proveedor2_CalendarExtender" runat="server" 
                                                                                        Enabled="True" Format="MM/dd/yyyy" TargetControlID="tb_fecha_proveedor2"></cc1:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle" colspan="2">
                                                                        <asp:Button ID="btn_regresar6" runat="server" onclick="btn_regresar6_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente3_pregunta1" runat="server" 
                                                                            onclick="btn_siguiente3_pregunta1_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_3_0" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        <asp:Label ID="lbl_fechahora_73" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Quien pagara el Ocean Freight?</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl35" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="10">Nosotros</asp:ListItem>
                                                                            <asp:ListItem Value="2">Agente</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar42" runat="server" onclick="btn_regresar42_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente18" runat="server" onclick="btn_siguiente18_Click" 
                                                                            style="height: 26px" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_4" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        <asp:Label ID="lbl_fechahora_3" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        <strong>Ingrese el monto del Flete Maritimo de la Factura&nbsp; del&nbsp; Agente (MBL)</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2" width="200px">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" height="30px">
                                                                        Monto</td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="tb_monto_factura_agente" runat="server" Height="16px" 
                                                                            Width="100px">0.00</asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="tb_monto_factura_agente_FilteredTextBoxExtender1" 
                                                                            runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                            TargetControlID="tb_monto_factura_agente" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                                                                            ControlToValidate="tb_monto_factura_agente" ErrorMessage="Error ###.##" 
                                                                            SetFocusOnError="True" ValidationExpression="\d+.\d{2}">
                                                                            </asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Moneda</td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="drp_p1_moneda2" runat="server">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2" valign="middle">
                                                                        <strong>Datos Factura del Agente</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2" height="45px" valign="middle">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style9">
                                                                            <tr>
                                                                                <td>
                                                                                    Serie</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_serie_proveedor3" runat="server" Height="16px" Width="75px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    Correlativo</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_correlativo_proveedor3" runat="server" Height="16px" 
                                                                                        Width="75px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    Fecha</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_fecha_proveedor3" runat="server" Height="16px" 
                                                                                        Width="128px"></asp:TextBox>
                                                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" 
                                                                                        runat="server" Enabled="True" Mask="99/99/9999" MaskType="Date" 
                                                                                        TargetControlID="tb_fecha_proveedor3"></cc1:MaskedEditExtender>
                                                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                                                                        Enabled="True" Format="MM/dd/yyyy" TargetControlID="tb_fecha_proveedor3"></cc1:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle" colspan="2">
                                                                        <asp:Button ID="btn_regresar7" runat="server" onclick="btn_regresar7_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente4_pregunta1" runat="server" 
                                                                            onclick="btn_siguiente4_pregunta1_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_5" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        <asp:Label ID="lbl_fechahora_4" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        <asp:Label ID="lbl_preguntaID_diferencia_master" runat="server" Text="0" 
                                                                            Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Es igual el monto del Ocean Freight de la Factura del Agente al de la Factura de 
                                                                        la Naviera?</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_diferencia_ocean_freight" runat="server" Text="0.00" 
                                                                            Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style2">
                                                                            <tr>
                                                                                <td align="center" class="style17" width="80">
                                                                                    Naviera</td>
                                                                                <td align="left" width="100">
                                                                                    <b><i>
                                                                                    <asp:Label ID="lbl_moneda_factura_naviera1" runat="server" Text="-"></asp:Label>
                                                                                    &nbsp;<asp:Label ID="lbl_monto_factura_naviera" runat="server" Text="0.00"></asp:Label>
                                                                                    </i></b>
                                                                                </td>
                                                                                <td align="center" width="50px">
                                                                                    <b><i>=</i></b></td>
                                                                                <td align="center" width="80">
                                                                                    <b><i>Agente</i></b></td>
                                                                                <td align="left" width="100">
                                                                                    <b><i>
                                                                                    <asp:Label ID="lbl_moneda_factura_naviera2" runat="server" Text="-"></asp:Label>
                                                                                    &nbsp;<asp:Label ID="lbl_monto_factura_agente" runat="server" Text="0.00"></asp:Label>
                                                                                    </i></b>
                                                                                </td>
                                                                                <td width="50px">
                                                                                    <b><i>??</i></b></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl2_pregunta1" runat="server" 
                                                                            RepeatDirection="Horizontal" Enabled="False">
                                                                            <asp:ListItem Value="True">Si</asp:ListItem>
                                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar8" runat="server" onclick="btn_regresar8_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente5_pregunta1" runat="server" Text="Siguiente" 
                                                                            onclick="btn_siguiente5_pregunta1_Click" style="height: 26px" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_5_1" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        <asp:Label ID="lbl_fechahora_19" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        La Diferencia sera a cargo de:</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl8_pregunta1" runat="server" 
                                                                            RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="0">Todos</asp:ListItem>
                                                                            <asp:ListItem Value="2">Agente</asp:ListItem>
                                                                            <asp:ListItem Value="10">Nosotros</asp:ListItem>
                                                                            <asp:ListItem Value="3">Cliente</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar25" runat="server" onclick="btn_regresar25_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente17_pregunta1" runat="server" Text="Siguiente" 
                                                                            onclick="btn_siguiente17_pregunta1_Click" style="height: 26px" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_6" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <asp:Label ID="lbl_fechahora_5" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_campo1" runat="server" Font-Bold="True"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style14" valign="middle">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style6">
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    A Cargo de&nbsp;</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p1_agente_naviera" runat="server" AutoPostBack="True" 
                                                                                        onselectedindexchanged="drp_p1_agente_naviera_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Servicio</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p1_servicio" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Rubro</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p1_rubro" runat="server">
                                                                                        <asp:ListItem Selected="True" Value="28">OCEAN FREIGHT</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Moneda&nbsp;</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p1_moneda3" runat="server" Enabled="False">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Monto</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_p1_monto_recargos" runat="server" Height="16px" Width="100px">0.00</asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="tb_p1_monto_recargos_FilteredTextBoxExtender" 
                                                                                        runat="server" Enabled="True" TargetControlID="tb_p1_monto_recargos" FilterType="Numbers,Custom" ValidChars="." ></cc1:FilteredTextBoxExtender>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                                                                        ControlToValidate="tb_p1_monto_recargos" ErrorMessage="Error ###.##" SetFocusOnError="True" 
                                                                                        ValidationExpression="\d+.\d{2}">
                                                                                    </asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:Panel ID="pnl_cliente1" runat="server" Visible="False">
                                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style5">
                                                                                            <tr>
                                                                                                <td align="left" colspan="2" width="110px" height="30px" 
                                                                                                    style="border-style: solid none none none; border-width: 1px 0px 0px 0px; border-color: #000000">
                                                                                                    <strong>Datos del Cliente</strong></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td width="110px">
                                                                                                    Codigo</td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="tb_codigo_cliente1" runat="server" Height="16px" 
                                                                                                        ReadOnly="True" Width="50px">0</asp:TextBox>
                                                                                                        <cc1:ModalPopupExtender ID="modalcliente2" runat="server" 
                                                                                        BackgroundCssClass="FondoAplicacion" CancelControlID="btnClienteCancelar" 
                                                                                        DropShadow="True" OnCancelScript="mpeClienteOnCancel()" 
                                                                                        PopupControlID="pnlCliente1" TargetControlID="tb_codigo_cliente1" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Nombre</td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="tb_nombre_cliente1" runat="server" Height="16px" 
                                                                                                        ReadOnly="True" Width="300px"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:Button ID="btn_p1_agregar_recargo" runat="server" 
                                                                                        onclick="btn_p1_agregar_recargo_Click" Text="Agregar" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:GridView ID="gv_cargos_master" runat="server" Font-Size="X-Small" 
                                                                                        onrowdeleting="gv_cargos_master_RowDeleting" 
                                                                                        onrowcreated="gv_cargos_master_RowCreated">
                                                                                        <Columns>
                                                                                            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle" class="style6">
                                                                        <asp:Button ID="btn_siguiente6_pregunta1" runat="server" 
                                                                            onclick="btn_siguiente6_pregunta1_Click" Text="Siguiente" 
                                                                            style="height: 26px" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_7" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style13">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Transacciones Generadas por el Ocean Freight del Master:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_6" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:GridView ID="gv_transacciones_master" runat="server" Font-Size="X-Small" 
                                                                            onrowcreated="gv_transacciones_master_RowCreated">
                                                                            <Columns>
                                                                                <asp:TemplateField><ItemTemplate><asp:DropDownList ID="drp_afecto_excento1" runat="server" Visible="False"><asp:ListItem Value="0">SELECCIONE...</asp:ListItem><asp:ListItem Value="1">EXCENTO</asp:ListItem><asp:ListItem Value="2">AFECTO</asp:ListItem></asp:DropDownList></ItemTemplate></asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style2">
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:RadioButtonList ID="rbl3_pregunta1" runat="server" 
                                                                                        RepeatDirection="Horizontal" Visible="False">
                                                                                        <asp:ListItem Value="True">Si</asp:ListItem>
                                                                                        <asp:ListItem Value="False" Selected="True">No</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:RadioButtonList ID="rbl4_pregunta1" runat="server" 
                                                                                        RepeatDirection="Horizontal" Visible="False">
                                                                                        <asp:ListItem Value="True">Si</asp:ListItem>
                                                                                        <asp:ListItem Value="False" Selected="True">No</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar_pregunta1" runat="server" Text="Regresar" 
                                                                            onclick="btn_regresar_pregunta1_Click" Visible="False" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente7_pregunta1" runat="server" Text="Siguiente" 
                                                                            onclick="btn_siguiente7_pregunta1_Click" 
                                                                            style="width: 82px; height: 26px;" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_8" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        <asp:Label ID="lbl_fechahora_7" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Existen Gastos Locales del Master, que sean indicados<br /> en el <strong>AVISO 
                                                                        DE ARRIBO</strong> de la Naviera?</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl5_pregunta1" runat="server" 
                                                                            RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="True">Si</asp:ListItem>
                                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar18" runat="server" Text="Regresar" 
                                                                            Visible="False" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente9_pregunta1" runat="server" 
                                                                            Text="Siguiente" onclick="btn_siguiente9_pregunta1_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_9" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <asp:Label ID="lbl_fechahora_8" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <strong>Por favor ingrese cada uno de los Gastos Locales del Master:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style14" valign="middle">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style6">
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    A Cargo de&nbsp;</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p1_agente_naviera2" runat="server" 
                                                                                        AutoPostBack="True" 
                                                                                        onselectedindexchanged="drp_p1_agente_naviera2_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Servicio</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p1_servicio2" runat="server" AutoPostBack="True" 
                                                                                        onselectedindexchanged="drp_p1_servicio2_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Rubro</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p1_rubro2" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Moneda&nbsp;</td>
                                                                                <td align="left">
                                                                                    <asp:DropDownList ID="drp_p1_moneda5" runat="server" 
                                                                                        ToolTip="Moneda en que sera emitido el Pago a la Naviera" Width="100px">
                                                                                    </asp:DropDownList>
                                                                                    &nbsp; <span class="style36"><strong>* Moneda en que sera emitido el Pago a la 
                                                                                    Naviera</strong></span></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Impuestos</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_afecto_excento2" runat="server">
                                                                                        <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Excento</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Afecto</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Monto</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_p1_monto_recargos_master" runat="server" Height="16px" 
                                                                                        Width="100px">0.00</asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="tb_p1_monto_recargos_master_FilteredTextBoxExtender" 
                                                                                        runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                                        TargetControlID="tb_p1_monto_recargos_master" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator24" 
                                                                                        runat="server" ControlToValidate="tb_p1_monto_recargos_master" 
                                                                                        ErrorMessage="Error ###.##" SetFocusOnError="True" 
                                                                                        ValidationExpression="\d+.\d{2}">
                                                                                    </asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Observaciones</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_observaciones1" runat="server" Height="16px" MaxLength="99" 
                                                                                        Width="350px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" height="10px">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:Panel ID="pnl_cliente2" runat="server" Visible="False">
                                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style5">
                                                                                            <tr>
                                                                                                <td align="left" colspan="2" width="110px" height="30px" 
                                                                                                    style="border-style: solid none none none; border-width: 1px 0px 0px 0px; border-color: #000000" 
                                                                                                    valign="middle">
                                                                                                    <strong>Datos del Cliente</strong></td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td width="110px">
                                                                                                    Codigo</td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="tb_codigo_cliente2" runat="server" Height="16px" 
                                                                                                        ReadOnly="True" Width="50px">0</asp:TextBox>
                                                                                                    <cc1:ModalPopupExtender ID="modalcliente3" runat="server" 
                                                                                                    BackgroundCssClass="FondoAplicacion" CancelControlID="btnClienteCancelar2" 
                                                                                                    DropShadow="True" OnCancelScript="mpeClienteOnCancel()" 
                                                                                                    PopupControlID="pnlCliente2" TargetControlID="tb_codigo_cliente2" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    Nombre</td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="tb_nombre_cliente2" runat="server" Height="16px" 
                                                                                                        ReadOnly="True" Width="400px"></asp:TextBox>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td colspan="2">
                                                                                                    &nbsp;</td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="left" colspan="2" style="font-weight: bold">
                                                                                                    El Gasto Local esta incluido dentro de los cargos del HBL por cobrar al Cliente?</td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="center" colspan="2">
                                                                                                    &nbsp;</td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td align="center" colspan="2">
                                                                                                    <asp:RadioButtonList ID="rbl40" runat="server" RepeatDirection="Horizontal">
                                                                                                        <asp:ListItem Value="True">Si</asp:ListItem>
                                                                                                        <asp:ListItem Value="False">No</asp:ListItem>
                                                                                                    </asp:RadioButtonList>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" colspan="2" height="25px" 
                                                                                    style="border-style: solid none none none; border-width: 1px 0px 0px 0px; border-color: #000000">
                                                                                    <strong>Datos Factura de la Naviera</strong></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2" height="45px" valign="middle">
                                                                                    <table align="center" cellpadding="0" cellspacing="0" class="style9">
                                                                                        <tr>
                                                                                            <td>
                                                                                                Serie</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="tb_serie_proveedor1" runat="server" Height="16px" Width="75px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                Correlativo</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="tb_correlativo_proveedor1" runat="server" Height="16px" 
                                                                                                    Width="75px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                Fecha</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="tb_fecha_proveedor1" runat="server" Height="16px" Width="128px"></asp:TextBox>
                                                                                                <cc1:MaskedEditExtender ID="tb_fecha_proveedor1_MaskedEditExtender" runat="server" 
                                                                                                    Enabled="True" Mask="99/99/9999" MaskType="Date" TargetControlID="tb_fecha_proveedor1"></cc1:MaskedEditExtender>
                                                                                                <cc1:CalendarExtender ID="tb_fecha_proveedor1_CalendarExtender" runat="server" 
                                                                                                    Enabled="True" Format="MM/dd/yyyy" TargetControlID="tb_fecha_proveedor1"></cc1:CalendarExtender>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2" height="35px" valign="middle">
                                                                                    <asp:Button ID="btn_p1_agregar_recargo13" runat="server" Text="Agregar" 
                                                                                        onclick="btn_p1_agregar_recargo13_Click" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2" 
                                                                                    style="border-style: solid none none none; border-width: 1px 0px 0px 0px; border-color: #000000">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2" style="margin-left: 40px">
                                                                                    <asp:GridView ID="gv_cargos_locales_master" runat="server" Font-Size="X-Small" 
                                                                                        onrowdeleting="gv_cargos_locales_master_RowDeleting" 
                                                                                        onrowcreated="gv_cargos_locales_master_RowCreated">
                                                                                        <Columns>
                                                                                            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style6" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente10_pregunta1" runat="server" 
                                                                            onclick="btn_siguiente10_pregunta1_Click" style="height: 26px" 
                                                                            Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_10" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style13">
                                                                        <asp:Label ID="lbl_fechahora_9" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Transacciones Generadas por Gastos Locales del Master:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:GridView ID="gv_transacciones_gastos_locales_master" runat="server" 
                                                                            Font-Size="X-Small" 
                                                                            onrowcreated="gv_transacciones_gastos_locales_master_RowCreated">
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar19" runat="server" onclick="btn_regresar19_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente11_pregunta1" runat="server" 
                                                                            onclick="btn_siguiente11_pregunta1_Click" style="width: 82px" 
                                                                            Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_24" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        <asp:Label ID="lbl_fechahora_75" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Existen Costos Adicionales por pagar al <strong>Agente?</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl37" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="True">Si</asp:ListItem>
                                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente20" runat="server" onclick="btn_siguiente20_Click" 
                                                                            style="height: 26px" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_25" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <asp:Label ID="lbl_fechahora_76" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <strong>Por favor ingrese cada uno de los Costos Adicionales por pagar al 
                                                                        Agente:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style14" valign="middle">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style6">
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Servicio</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p1_servicio4" runat="server" AutoPostBack="True" 
                                                                                        onselectedindexchanged="drp_p1_servicio4_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Rubro</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p1_rubro4" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Moneda&nbsp;</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p1_moneda7" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="170px">
                                                                                    Contabilizar como:</td>
                                                                                <td>
                                                                                    <asp:RadioButtonList ID="rbl38" runat="server" RepeatDirection="Horizontal">
                                                                                        <asp:ListItem Value="1">Excento de Impuestos</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Afecto a Impuestos</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Monto</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_monto_costo_proveedor2" runat="server" Height="16px" 
                                                                                        Width="100px">0.00</asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="tb_monto_costo_proveedor2_FilteredTextBoxExtender" 
                                                                                        runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                                        TargetControlID="tb_monto_costo_proveedor2" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator31" 
                                                                                        runat="server" ControlToValidate="tb_monto_costo_proveedor2" 
                                                                                        ErrorMessage="Error ###.##" SetFocusOnError="True" 
                                                                                        ValidationExpression="\d+.\d{2}">
                                                                                    </asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Observaciones</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_observaciones4" runat="server" Height="16px" MaxLength="99" 
                                                                                        Width="300px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" height="10px">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" colspan="2" height="25px" 
                                                                                    style="border-style: solid none none none; border-width: 1px 0px 0px 0px; border-color: #000000">
                                                                                    <strong>Datos Factura del Agente</strong></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2" height="45px" valign="middle">
                                                                                    <table align="center" cellpadding="0" cellspacing="0" class="style9">
                                                                                        <tr>
                                                                                            <td>
                                                                                                Serie</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="tb_serie_proveedor5" runat="server" Height="16px" Width="75px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                Correlativo</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="tb_correlativo_proveedor5" runat="server" Height="16px" 
                                                                                                    Width="75px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                Fecha</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="tb_fecha_proveedor5" runat="server" Height="16px" 
                                                                                                    Width="128px"></asp:TextBox>
                                                                                                <cc1:MaskedEditExtender ID="tb_fecha_proveedor5_MaskedEditExtender" 
                                                                                                    runat="server" Enabled="True" Mask="99/99/9999" MaskType="Date" 
                                                                                                    TargetControlID="tb_fecha_proveedor5"></cc1:MaskedEditExtender>
                                                                                                <cc1:CalendarExtender ID="tb_fecha_proveedor5_CalendarExtender" runat="server" 
                                                                                                    Enabled="True" Format="MM/dd/yyyy" TargetControlID="tb_fecha_proveedor5"></cc1:CalendarExtender>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2" height="35px" valign="middle">
                                                                                    <asp:Button ID="btn_agregar2" runat="server" onclick="btn_agregar2_Click" 
                                                                                        Text="Agregar" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2" 
                                                                                    style="border-style: solid none none none; border-width: 1px 0px 0px 0px; border-color: #000000">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:GridView ID="gv_costos_proveedores2" runat="server" Font-Size="X-Small" 
                                                                                        onrowcreated="gv_costos_proveedores2_RowCreated" 
                                                                                        onrowdeleting="gv_costos_proveedores2_RowDeleting">
                                                                                        <Columns>
                                                                                            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style6" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente21" runat="server" style="height: 26px" 
                                                                            Text="Siguiente" onclick="btn_siguiente21_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_26" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style13">
                                                                        <asp:Label ID="lbl_fechahora_77" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Transacciones Generadas por Costos del Agente:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:GridView ID="gv_transacciones_costos_proveedores2" runat="server" 
                                                                            Font-Size="X-Small" 
                                                                            onrowcreated="gv_transacciones_costos_proveedores2_RowCreated">
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar44" runat="server" onclick="btn_regresar44_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente22" runat="server" onclick="btn_siguiente22_Click" 
                                                                            style="width: 82px" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_21" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        <asp:Label ID="lbl_fechahora_70" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Existen Costos Adicionales por pagar a otros <strong>Proveedores?</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl34" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="True">Si</asp:ListItem>
                                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar37" runat="server" Text="Regresar" 
                                                                            Visible="False" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente8" runat="server" Text="Siguiente" 
                                                                            onclick="btn_siguiente8_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_22" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <asp:Label ID="lbl_fechahora_71" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <strong>Por favor ingrese cada uno de los Costos Adicionales por pagar a otros 
                                                                        Proveedores:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style14" valign="middle">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style6">
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" colspan="2" height="30px" valign="middle">
                                                                                    <strong>Proveedor</strong></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Codigo</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_proveedor_codigo3" runat="server" Height="16px" 
                                                                                        ReadOnly="True" Width="50px">0</asp:TextBox>
                                                                                    <cc1:ModalPopupExtender ID="modalproveedor4" 
                                                                                        runat="server" BackgroundCssClass="FondoAplicacion" 
                                                                                        CancelControlID="btnClienteCancelar2" DropShadow="True" 
                                                                                        OnCancelScript="mpeClienteOnCancel()" PopupControlID="pnlProveedor4" 
                                                                                        TargetControlID="tb_proveedor_codigo3" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Nombre</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_proveedor_nombre3" runat="server" Height="16px" 
                                                                                        ReadOnly="True" Width="250px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" colspan="2">
                                                                                    <strong>Costos</strong></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Servicio</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p1_servicio3" runat="server" AutoPostBack="True" 
                                                                                        onselectedindexchanged="drp_p1_servicio3_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Rubro</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p1_rubro3" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Moneda&nbsp;</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p1_moneda6" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Impuestos</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_afecto_excento3" runat="server">
                                                                                        <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Excento</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Afecto</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Monto</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_monto_costo_proveedor1" runat="server" Height="16px" 
                                                                                        Width="100px">0.00</asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="tb_monto_costo_proveedor1_FilteredTextBoxExtender" 
                                                                                        runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                                        TargetControlID="tb_monto_costo_proveedor1" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator30" 
                                                                                        runat="server" ControlToValidate="tb_monto_costo_proveedor1" 
                                                                                        ErrorMessage="Error ###.##" SetFocusOnError="True" 
                                                                                        ValidationExpression="\d+.\d{2}">
                                                                                    </asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Observaciones</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_observaciones3" runat="server" Height="16px" MaxLength="99" 
                                                                                        Width="250px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" height="10px">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" colspan="2" height="25px" 
                                                                                    style="border-style: solid none none none; border-width: 1px 0px 0px 0px; border-color: #000000">
                                                                                    <strong>Datos Factura Proveedor</strong></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2" height="45px" valign="middle">
                                                                                    <table align="center" cellpadding="0" cellspacing="0" class="style9">
                                                                                        <tr>
                                                                                            <td>
                                                                                                Serie</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="tb_serie_proveedor4" runat="server" Height="16px" Width="75px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                Correlativo</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="tb_correlativo_proveedor4" runat="server" Height="16px" 
                                                                                                    Width="75px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                Fecha</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="tb_fecha_proveedor4" runat="server" Height="16px" 
                                                                                                    Width="128px"></asp:TextBox>
                                                                                                <cc1:MaskedEditExtender ID="tb_fecha_proveedor4_MaskedEditExtender" 
                                                                                                    runat="server" Enabled="True" Mask="99/99/9999" MaskType="Date" 
                                                                                                    TargetControlID="tb_fecha_proveedor4"></cc1:MaskedEditExtender>
                                                                                                <cc1:CalendarExtender ID="tb_fecha_proveedor4_CalendarExtender" runat="server" 
                                                                                                    Enabled="True" Format="MM/dd/yyyy" TargetControlID="tb_fecha_proveedor4"></cc1:CalendarExtender>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2" height="35px" valign="middle">
                                                                                    <asp:Button ID="btn_agregar1" runat="server" 
                                                                                        onclick="btn_agregar1_Click" Text="Agregar" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2" 
                                                                                    style="border-style: solid none none none; border-width: 1px 0px 0px 0px; border-color: #000000">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:GridView ID="gv_costos_proveedores1" runat="server" Font-Size="X-Small" 
                                                                                        onrowcreated="gv_costos_proveedores1_RowCreated" 
                                                                                        onrowdeleting="gv_costos_proveedores1_RowDeleting">
                                                                                        <Columns>
                                                                                            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style6" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente14" runat="server" style="height: 26px" 
                                                                            Text="Siguiente" onclick="btn_siguiente14_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_23" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style13">
                                                                        <asp:Label ID="lbl_fechahora_72" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Transacciones Generadas por Costos de Proveedores:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:GridView ID="gv_transacciones_costos_proveedores" runat="server" 
                                                                            Font-Size="X-Small" 
                                                                            onrowcreated="gv_transacciones_costos_proveedores_RowCreated">
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar38" runat="server" onclick="btn_regresar38_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente15" runat="server" onclick="btn_siguiente15_Click" 
                                                                            style="width: 82px" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_11" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        <asp:Label ID="lbl_fechahora_10" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        El Master es cortado a:</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl6_pregunta1" runat="server" 
                                                                            RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="1">Puerto</asp:ListItem>
                                                                            <asp:ListItem Value="2">Ciudad</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar20" runat="server" Text="Regresar" 
                                                                            Visible="False" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente12_pregunta1" runat="server" style="height: 26px" 
                                                                            Text="Siguiente" onclick="btn_siguiente12_pregunta1_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_20" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        <asp:Label ID="lbl_fechahora_68" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Quien se hara cargo del InLand?</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl33" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="3">El Cliente</asp:ListItem>
                                                                            <asp:ListItem Value="10">Nosotros</asp:ListItem>
                                                                            <asp:ListItem Value="2">Agente</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar36" runat="server" onclick="btn_regresar36_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente12" runat="server" onclick="btn_siguiente12_Click" 
                                                                            Text="Siguiente" Width="82px" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_12" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <asp:Label ID="lbl_fechahora_11" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Por Favor seleccione el Transportista a utilizar:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style14" valign="middle">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style6">
                                                                            <tr>
                                                                                <td>
                                                                                    Codigo</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_codigo_transportista1" runat="server" Height="16px" 
                                                                                        Width="50px" ReadOnly="True">0</asp:TextBox>
                                                                                        <cc1:ModalPopupExtender ID="modalcliente" runat="server" 
                                                                                        BackgroundCssClass="FondoAplicacion" CancelControlID="btnProveedorCancelar" 
                                                                                        DropShadow="True" OnCancelScript="mpeClienteOnCancel()" 
                                                                                        PopupControlID="pnlProveedor" TargetControlID="tb_codigo_transportista1" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Nombre</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_nombre_transportista1" runat="server" Height="16px" 
                                                                                        Width="300px" ReadOnly="True"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" align="left">
                                                                                    <strong>Por favor Ingrese la Tarifa del InLand correspondiente al Master:</strong></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Moneda</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_moneda1" runat="server" Enabled="False">
                                                                                        <asp:ListItem Value="8">USD</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Impuestos</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_afecto_excento4" runat="server">
                                                                                        <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Excento</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Afecto</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Monto</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_monto_inland_master" runat="server" Height="16px" 
                                                                                        ReadOnly="True">0.00</asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" 
                                                                                        Enabled="True" FilterType="Numbers,Custom" TargetControlID="tb_monto_inland_master" 
                                                                                        ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator27" 
                                                                                        runat="server" ControlToValidate="tb_monto_inland_master" ErrorMessage="Error ###.##" 
                                                                                        SetFocusOnError="True" ValidationExpression="\d+.\d{2}">
                                                                                    </asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Observaciones</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_observaciones2" runat="server" Height="16px" MaxLength="99" 
                                                                                        Width="400px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" colspan="2">
                                                                                    <strong>Datos Factura Proveedor</strong></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <table align="center" cellpadding="0" cellspacing="0" class="style9">
                                                                                        <tr>
                                                                                            <td>
                                                                                                Serie</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="tb_serie_proveedor7" runat="server" Height="16px" Width="75px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                Correlativo</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="tb_correlativo_proveedor7" runat="server" Height="16px" 
                                                                                                    Width="75px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                Fecha</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="tb_fecha_proveedor7" runat="server" Height="16px" 
                                                                                                    Width="128px"></asp:TextBox>
                                                                                                <cc1:MaskedEditExtender ID="tb_fecha_proveedor7_MaskedEditExtender" 
                                                                                                    runat="server" Enabled="True" Mask="99/99/9999" MaskType="Date" 
                                                                                                    TargetControlID="tb_fecha_proveedor7"></cc1:MaskedEditExtender>
                                                                                                <cc1:CalendarExtender ID="tb_fecha_proveedor7_CalendarExtender" runat="server" 
                                                                                                    Enabled="True" Format="MM/dd/yyyy" TargetControlID="tb_fecha_proveedor7"></cc1:CalendarExtender>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style6" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar26" runat="server" onclick="btn_regresar26_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;<asp:Button ID="btn_siguiente13_pregunta1" runat="server" 
                                                                            onclick="btn_siguiente13_pregunta1_Click" style="height: 26px" 
                                                                            Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_13" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style13">
                                                                        <asp:Label ID="lbl_fechahora_12" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Transacciones Generadas por InLand:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:GridView ID="gv_transacciones_inland_master" runat="server" 
                                                                            Font-Size="X-Small" 
                                                                            onrowcreated="gv_transacciones_inland_master_RowCreated">
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar21" runat="server" onclick="btn_regresar21_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente14_pregunta1" runat="server" 
                                                                            onclick="btn_siguiente14_pregunta1_Click" style="width: 82px" 
                                                                            Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_14" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        <asp:Label ID="lbl_fechahora_13" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Existen Retenciones en el Pais por realizar<br /> Transferencias hacia el 
                                                                        exterior?</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl7_pregunta1" runat="server" 
                                                                            RepeatDirection="Horizontal" Enabled="False">
                                                                            <asp:ListItem Value="True">Si</asp:ListItem>
                                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar22" runat="server" Text="Regresar" 
                                                                            Visible="False" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente15_pregunta1" runat="server" style="height: 26px" 
                                                                            Text="Siguiente" onclick="btn_siguiente15_pregunta1_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_15" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        <asp:Label ID="lbl_fechahora_14" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Porcentaje de Retencion por Transferencias a Terceros en
                                                                        <br />
                                                                        el Exterior:</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style28">
                                                                            <tr>
                                                                                <td>
                                                                                    Porcentaje Base</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td align="right">
                                                                                    <asp:Label ID="lbl_tarifa_base_transferencias_terceros" runat="server" 
                                                                                        Text="0.00"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Porcentaje Adicional</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000" 
                                                                                    align="right">
                                                                                    <asp:Label ID="lbl_tarifa_adicional_transferencias_terceros" runat="server" 
                                                                                        Text="0.00"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td height="30PX" valign="middle" width="300px">
                                                                                    <strong>Porcentaje Total a Retener</strong></td>
                                                                                <td width="30px">
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:Label ID="lbl_tarifa_total_transferencias_terceros" runat="server" 
                                                                                        Font-Bold="True" Text="0.00"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar23" runat="server" Text="Regresar" 
                                                                            Visible="False" onclick="btn_regresar23_Click" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente16_pregunta1" runat="server" 
                                                                            Text="Siguiente" onclick="btn_siguiente16_pregunta1_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_16" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_15" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Existen Garantias sobre la cantidad de CBM&#39;s a movilizar con el Agente?</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl8_pregunta2" runat="server" 
                                                                            RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="True">Si</asp:ListItem>
                                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" style="margin-left: 40px" valign="middle">
                                                                        <asp:Button ID="btn_siguiente11_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente11_pregunta2_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_17" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_16" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_campo2" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:TextBox ID="tb_total_cbms_garantia" runat="server" Height="16px" 
                                                                            Visible="False" Width="100px">0.00</asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" 
                                                                                        runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                                        TargetControlID="tb_total_cbms_garantia" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_campo3" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:TextBox ID="tb_coloading_rate" runat="server" Height="16px" 
                                                                            Visible="False" Width="100px">0.00</asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" 
                                                                                        runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                                        TargetControlID="tb_coloading_rate" ValidChars="."></cc1:FilteredTextBoxExtender>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Button ID="btn_regresar2" runat="server" onclick="btn_regresar2_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente12_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente12_pregunta2_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_18" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_17" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style29">
                                                                            <tr>
                                                                                <td>
                                                                                    Total CBM&#39;s Pactados</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_total_cbms_garantia2" runat="server" Height="16px" 
                                                                                        ReadOnly="True" Width="100px">0.00</asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Coloading Rate</td>
                                                                                <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                                                                                    <asp:TextBox ID="tb_coloading_rate2" runat="server" Height="16px" 
                                                                                        ReadOnly="True" Width="100px">0.00</asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Moneda&nbsp;</td>
                                                                                <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                                                                                    <asp:DropDownList ID="drp_moneda2" runat="server" Enabled="False">
                                                                                        <asp:ListItem Value="8">USD</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <strong>Total a Pagar</strong></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_total_garantias_pagar" runat="server" Font-Bold="True" 
                                                                                        Height="16px" ReadOnly="True" Width="100px">0.00</asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Del total de CBM&#39;s pactados, ingrese la cantidad
                                                                        <br />
                                                                        pactada por pais:</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:GridView ID="gv_cbms_pactados_pais" runat="server" Font-Size="XX-Small" 
                                                                            onrowcreated="gv_cbms_pactados_pais_RowCreated">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="CBMs"><ItemTemplate><asp:TextBox ID="tb_cbms_garantia_pais" runat="server" AutoPostBack="True" 
                                                                                            Height="16px" ontextchanged="tb_cbms_garantia_pais_TextChanged" Width="70px">0</asp:TextBox><cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" 
                                                                                            Enabled="True" FilterType="Numbers,Custom" 
                                                                                            TargetControlID="tb_cbms_garantia_pais" ValidChars="."></cc1:FilteredTextBoxExtender></ItemTemplate></asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="TOTAL"><ItemTemplate><asp:TextBox ID="tb_garantia_total_empresa" runat="server" Height="16px" 
                                                                                            ReadOnly="true" Width="70px">0</asp:TextBox></ItemTemplate></asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:Button ID="btn_regresar3" runat="server" onclick="btn_regresar3_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente13_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente13_pregunta2_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" style="margin-left: 40px" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta1_19" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style13">
                                                                        <asp:Label ID="lbl_fechahora_18" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Transacciones Generadas por Garantias:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:GridView ID="gv_transacciones_garantias" runat="server" 
                                                                            Font-Size="X-Small" onrowcreated="gv_transacciones_garantias_RowCreated">
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style12" valign="middle">
                                                                        <asp:Button ID="btn_regresar24" runat="server" onclick="btn_regresar24_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente15_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente15_pregunta2_Click" Text="Siguiente" 
                                                                            style="height: 26px" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </asp:View>
                                                    <asp:View ID="View3" runat="server">
                                                        <asp:Panel ID="pnl_pregunta2_1" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_21" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <h2>
                                                                            Houses</h2>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <br />
                                                                        Validar por cada House!!<br />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente_pregunta2_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta2_1_1" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        <asp:Label ID="lbl_fechahora_69" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        La Carga esta consignada a TO ORDER, por favor seleccione
                                                                        <br />
                                                                        el Cliente a utilizar:</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2" height="30px" valign="middle">
                                                                        <strong>Datos del Cliente</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        Codigo</td>
                                                                    <td align="left" valign="middle">
                                                                        <asp:TextBox ID="tb_codigo_cliente3" runat="server" Height="16px" 
                                                                            ReadOnly="True" Width="50px">0</asp:TextBox>
                                                                        <cc1:ModalPopupExtender ID="modalcliente4" 
                                                                            runat="server" BackgroundCssClass="FondoAplicacion" 
                                                                            CancelControlID="btnClienteCancelar3" DropShadow="True" 
                                                                            OnCancelScript="mpeClienteOnCancel()" PopupControlID="pnlCliente3" 
                                                                            TargetControlID="tb_codigo_cliente3" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle" width="120px">
                                                                        Nombre</td>
                                                                    <td align="left" valign="middle">
                                                                        <asp:TextBox ID="tb_nombre_cliente3" runat="server" Height="16px" 
                                                                            ReadOnly="True" Width="400px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente13" runat="server" Text="Siguiente" 
                                                                            onclick="btn_siguiente13_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta2_2" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_22" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        El House es FreeHand Cargo o Routing Order?</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl1_pregunta2" runat="server" 
                                                                            RepeatDirection="Horizontal" Enabled="False">
                                                                            <asp:ListItem Value="1">FreeHand Cargo</asp:ListItem>
                                                                            <asp:ListItem Value="2">Routing Order</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente2_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente2_pregunta2_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta2_3" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_23" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        El House es Prepagado o Collect?</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl2_pregunta2" runat="server" 
                                                                            RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="1">Prepagado</asp:ListItem>
                                                                            <asp:ListItem Value="2">Collect</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente3_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente3_pregunta2_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta2_4" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_24" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        El Destino del House es?</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl3_pregunta2" runat="server" 
                                                                            RepeatDirection="Horizontal" Enabled="False">
                                                                            <asp:ListItem Value="1">Al mismo Pais</asp:ListItem>
                                                                            <asp:ListItem Value="2">A otro Pais</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente4_pregunta2" runat="server" Text="Siguiente" 
                                                                            onclick="btn_siguiente4_pregunta2_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta2_11" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_84" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        El Consignatario de este House es un Intercompany?</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl42" runat="server" 
                                                                            RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="True">Si</asp:ListItem>
                                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente29" runat="server" Text="Siguiente" 
                                                                            onclick="btn_siguiente29_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta2_12" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_85" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Existen Costos o Comisiones Intercompany por Cobrar?</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl43" runat="server" 
                                                                            RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="True">Si</asp:ListItem>
                                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente30" runat="server" Text="Siguiente" 
                                                                            onclick="btn_siguiente30_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta2_13" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <asp:Label ID="lbl_fechahora_86" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <strong>Intercompany</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style14" valign="middle">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style6">
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" colspan="2">
                                                                                    Por Favor seleccione el Intercompany a operar:</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_intercompanys1" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" colspan="2">
                                                                                    <strong>Costos o Comisiones</strong></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" colspan="2">
                                                                                    Por Favor ingrese cada uno de los Costos o Comisiones por cobrar al 
                                                                                    Intercompany:</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style32">
                                                                                    <asp:Label ID="lbl_tipo_factura1" runat="server" Visible="False">Tipo de Factura</asp:Label>
                                                                                </td>
                                                                                <td class="style32">
                                                                                    <asp:DropDownList ID="drp_tipo_factura1" runat="server" Visible="False">
                                                                                        <asp:ListItem Value="1">Seleccione...</asp:ListItem>
                                                                                        <asp:ListItem Value="2">FACTURA CONSUMIDOR FINAL</asp:ListItem>
                                                                                        <asp:ListItem Value="3">FACTURA CREDITO FISCAL</asp:ListItem>
                                                                                        <asp:ListItem Value="4">FACTURA EXPORTACION</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style31">
                                                                                    Servicio</td>
                                                                                <td class="style31">
                                                                                    <asp:DropDownList ID="drp_p1_servicio7" runat="server" AutoPostBack="True" 
                                                                                        onselectedindexchanged="drp_p1_servicio7_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Rubro</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p1_rubro7" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Moneda&nbsp;</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p1_moneda10" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Monto</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_monto_costo_proveedor5" runat="server" Height="16px" 
                                                                                        Width="100px">0.00</asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="tb_monto_costo_proveedor5_FilteredTextBoxExtender" 
                                                                                        runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                                        TargetControlID="tb_monto_costo_proveedor5" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator34" 
                                                                                        runat="server" ControlToValidate="tb_monto_costo_proveedor5" 
                                                                                        ErrorMessage="Error ###.##" SetFocusOnError="True" 
                                                                                        ValidationExpression="\d+.\d{2}">
                                                                                    </asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Observaciones</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_observaciones13" runat="server" Height="16px" 
                                                                                        MaxLength="99" Width="300px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" height="10px">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2" height="35px" valign="middle">
                                                                                    <asp:Button ID="btn_agregar5" runat="server" Text="Agregar" 
                                                                                        onclick="btn_agregar5_Click" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2" 
                                                                                    style="border-style: solid none none none; border-width: 1px 0px 0px 0px; border-color: #000000">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:GridView ID="gv_costos_proveedores5" runat="server" Font-Size="X-Small" 
                                                                                        onrowdeleting="gv_costos_proveedores5_RowDeleting" 
                                                                                        onrowcreated="gv_costos_proveedores5_RowCreated">
                                                                                        <Columns>
                                                                                            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style6" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente31" runat="server" Text="Siguiente" 
                                                                            onclick="btn_siguiente31_Click" style="height: 26px" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta2_14" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style13">
                                                                        <asp:Label ID="lbl_fechahora_87" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Transacciones Generadas por Costos o Comisiones por cobrar&nbsp; 
                                                                        Intercompany:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:GridView ID="gv_transacciones_costos_proveedores5" runat="server" 
                                                                            Font-Size="X-Small" 
                                                                            onrowcreated="gv_transacciones_costos_proveedores5_RowCreated">
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar47" runat="server" onclick="btn_regresar47_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente32" runat="server" onclick="btn_siguiente32_Click" 
                                                                            style="width: 82px" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta2_5" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        <asp:Label ID="lbl_fechahora_78" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Existen Costos Adicionales por pagar al <strong>Agente?</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl39" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="True">Si</asp:ListItem>
                                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente23" runat="server" onclick="btn_siguiente23_Click" 
                                                                            style="height: 26px" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta2_6" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <asp:Label ID="lbl_fechahora_79" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <strong>Por favor ingrese cada uno de los Costos Adicionales por pagar al 
                                                                        Agente:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style14" valign="middle">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style6">
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" colspan="2" 
                                                                                    style="font-weight: bold; color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                                                                                    <asp:RadioButtonList ID="rbl_agentes1" runat="server" AutoPostBack="True" 
                                                                                        Font-Bold="True" onselectedindexchanged="rbl_agentes1_SelectedIndexChanged" 
                                                                                        RepeatDirection="Horizontal">
                                                                                        <asp:ListItem Value="True">Agente del Embarque</asp:ListItem>
                                                                                        <asp:ListItem Value="False">Otro Agente</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    Codigo</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_agenteID1" runat="server" Height="16px" ReadOnly="True" 
                                                                                        Width="50px">
                                                                                    </asp:TextBox>
                                                                                    <cc1:ModalPopupExtender ID="modalagente1" 
                                                                                        runat="server" BackgroundCssClass="FondoAplicacion" 
                                                                                        CancelControlID="btnClienteCancelar2" DropShadow="True" 
                                                                                        OnCancelScript="mpeClienteOnCancel()" PopupControlID="pnlAgente1" 
                                                                                        TargetControlID="tb_agenteID1" />

                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    Nombre</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_agente_nombre1" runat="server" Height="16px" Width="400px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" colspan="2" 
                                                                                    style="font-weight: bold; color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                                                                                    Costos</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Servicio</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p1_servicio5" runat="server" AutoPostBack="True" 
                                                                                        onselectedindexchanged="drp_p1_servicio5_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Rubro</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p1_rubro5" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Moneda&nbsp;</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p1_moneda8" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Impuestos</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_afecto_excento5" runat="server">
                                                                                        <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Excento</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Afecto</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Monto</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_monto_costo_proveedor3" runat="server" Height="16px" 
                                                                                        Width="100px">0.00</asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="tb_monto_costo_proveedor3_FilteredTextBoxExtender" 
                                                                                        runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                                        TargetControlID="tb_monto_costo_proveedor3" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator32" 
                                                                                        runat="server" ControlToValidate="tb_monto_costo_proveedor3" 
                                                                                        ErrorMessage="Error ###.##" SetFocusOnError="True" 
                                                                                        ValidationExpression="\d+.\d{2}">
                                                                                    </asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Observaciones</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_observaciones5" runat="server" Height="16px" MaxLength="99" 
                                                                                        Width="400px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" height="10px" align="left" 
                                                                                    style="font-weight: bold; color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                                                                                    <strong>Datos Factura del Agente</strong></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2" height="45px" valign="middle">
                                                                                    <table align="center" cellpadding="0" cellspacing="0" class="style9">
                                                                                        <tr>
                                                                                            <td>
                                                                                                Serie</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="tb_serie_proveedor6" runat="server" Height="16px" Width="75px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                Correlativo</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="tb_correlativo_proveedor6" runat="server" Height="16px" 
                                                                                                    Width="75px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                Fecha</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="tb_fecha_proveedor6" runat="server" Height="16px" 
                                                                                                    Width="128px"></asp:TextBox>
                                                                                                <cc1:MaskedEditExtender ID="tb_fecha_proveedor6_MaskedEditExtender" 
                                                                                                    runat="server" Enabled="True" Mask="99/99/9999" MaskType="Date" 
                                                                                                    TargetControlID="tb_fecha_proveedor6"></cc1:MaskedEditExtender>
                                                                                                <cc1:CalendarExtender ID="tb_fecha_proveedor6_CalendarExtender" runat="server" 
                                                                                                    Enabled="True" Format="MM/dd/yyyy" TargetControlID="tb_fecha_proveedor6"></cc1:CalendarExtender>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2" height="35px" valign="middle">
                                                                                    <asp:Button ID="btn_agregar3" runat="server" Text="Agregar" 
                                                                                        onclick="btn_agregar3_Click" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2" 
                                                                                    style="border-style: solid none none none; border-width: 1px 0px 0px 0px; border-color: #000000">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:GridView ID="gv_costos_proveedores3" runat="server" Font-Size="X-Small" 
                                                                                        onrowcreated="gv_costos_proveedores3_RowCreated" 
                                                                                        onrowdeleting="gv_costos_proveedores3_RowDeleting">
                                                                                        <Columns>
                                                                                            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style6" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente24" runat="server" 
                                                                            Text="Siguiente" onclick="btn_siguiente24_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta2_7" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style13">
                                                                        <asp:Label ID="lbl_fechahora_80" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Transacciones Generadas por Pagos del Agente:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:GridView ID="gv_transacciones_costos_proveedores3" runat="server" 
                                                                            Font-Size="X-Small" 
                                                                            onrowcreated="gv_transacciones_costos_proveedores3_RowCreated">
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar45" runat="server" onclick="btn_regresar45_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente25" runat="server" onclick="btn_siguiente25_Click" 
                                                                            style="width: 82px" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta2_8" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        <asp:Label ID="lbl_fechahora_81" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Existen Cargos Adicionales por cobrar al <strong>Agente?</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl41" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="True">Si</asp:ListItem>
                                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente26" runat="server" style="height: 26px" 
                                                                            Text="Siguiente" onclick="btn_siguiente26_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta2_9" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <asp:Label ID="lbl_fechahora_82" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <strong>Por favor ingrese cada uno de los Cargos Adicionales por cobrar al 
                                                                        Agente:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style14" valign="middle">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style6">
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="200px">
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_tipo_cobro1" runat="server" Enabled="False" 
                                                                                        Visible="False">
                                                                                        <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                                                                                        <asp:ListItem Value="1">FACTURA</asp:ListItem>
                                                                                        <asp:ListItem Value="4" Selected="True">NOTA DEBITO</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Servicio</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p1_servicio6" runat="server" AutoPostBack="True" 
                                                                                        onselectedindexchanged="drp_p1_servicio6_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Rubro</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p1_rubro6" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Moneda&nbsp;</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p1_moneda9" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Monto</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_monto_costo_proveedor4" runat="server" Height="16px" 
                                                                                        Width="100px">0.00</asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="tb_monto_costo_proveedor4_FilteredTextBoxExtender" 
                                                                                        runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                                        TargetControlID="tb_monto_costo_proveedor4" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator33" 
                                                                                        runat="server" ControlToValidate="tb_monto_costo_proveedor4" 
                                                                                        ErrorMessage="Error ###.##" SetFocusOnError="True" 
                                                                                        ValidationExpression="\d+.\d{2}">
                                                                                    </asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Observaciones</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_observaciones12" runat="server" Height="16px" 
                                                                                        MaxLength="99" Width="300px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" height="10px">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:Button ID="btn_agregar4" runat="server" Text="Agregar" 
                                                                                        onclick="btn_agregar4_Click" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:GridView ID="gv_costos_proveedores4" runat="server" Font-Size="X-Small" 
                                                                                        onrowcreated="gv_costos_proveedores4_RowCreated" 
                                                                                        onrowdeleting="gv_costos_proveedores4_RowDeleting">
                                                                                        <Columns>
                                                                                            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                                                                                            <asp:TemplateField HeaderText="All In"><ItemTemplate><asp:TextBox ID="tb_all_in1" runat="server" Height="16px" Width="75px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style6" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente27" runat="server" onclick="btn_siguiente27_Click" 
                                                                            Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_pregunta2_10" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style13">
                                                                        <asp:Label ID="lbl_fechahora_83" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Transacciones Generadas por Cobros al Agente:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:GridView ID="gv_transacciones_costos_proveedores4" runat="server" 
                                                                            Font-Size="X-Small" 
                                                                            onrowcreated="gv_transacciones_costos_proveedores4_RowCreated">
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar46" runat="server" onclick="btn_regresar46_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente28" runat="server" onclick="btn_siguiente28_Click" 
                                                                            style="width: 82px" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </asp:View>
                                                    <asp:View ID="View4" runat="server">
                                                    <asp:Panel ID="pnl_fhc_prepagado_mp0" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_45" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_comentario_personal4" runat="server" Text="FHC-PREPAID-MP" 
                                                                            Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Existen Cargos </strong>por servicios DAP o DDP por Cobrar al Agente?</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl_29" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="True">Si</asp:ListItem>
                                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente60_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente60_pregunta2_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_prepagado_mp" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_41" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Como es el Servicio:</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:DropDownList ID="drp_incoterms" runat="server">
                                                                            <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                                                                            <asp:ListItem Value="4">DDP</asp:ListItem>
                                                                            <asp:ListItem Value="9">DAP</asp:ListItem>
                                                                            <asp:ListItem Value="50">OTRO</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente5_pregunta2" runat="server" Text="Siguiente" 
                                                                            onclick="btn_siguiente5_pregunta2_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_prepagado_mp_dap_ddp" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <asp:Label ID="lbl_fechahora_43" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <asp:Label ID="lbl_campo6" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style14" valign="middle">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style6">
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Servicio</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p2_servicio" runat="server" AutoPostBack="True" 
                                                                                        onselectedindexchanged="drp_p2_servicio_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Rubro</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p2_rubro" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Moneda&nbsp;</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p2_moneda" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Monto</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_p2_monto_recargo" runat="server" Height="16px" 
                                                                                        Width="100px">0.00</asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="tb_p2_monto_recargo_FilteredTextBoxExtender" 
                                                                                        runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                                        TargetControlID="tb_p2_monto_recargo" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" 
                                                                                        ControlToValidate="tb_p1_monto_recargos" ErrorMessage="Error ###.##" 
                                                                                        SetFocusOnError="True" ValidationExpression="\d+.\d{2}">
                                                                                    </asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:Button ID="btn_p1_agregar_recargo2" runat="server" 
                                                                                        onclick="btn_p1_agregar_recargo2_Click" Text="Agregar" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:GridView ID="gv_cargos_ddp_dap" runat="server" Font-Size="X-Small" 
                                                                                        onrowcreated="gv_cargos_ddp_dap_RowCreated" 
                                                                                        onrowdeleting="gv_cargos_ddp_dap_RowDeleting">
                                                                                        <Columns>
                                                                                            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style6" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente16_pregunta2" runat="server" Text="Siguiente" 
                                                                            onclick="btn_siguiente16_pregunta2_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_prepagado_mp_dap_ddp2" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style13">
                                                                        <asp:Label ID="lbl_fechahora_44" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style13">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Transacciones Generadas por Servicio DAP O DDP:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:GridView ID="gv_transacciones_ddp_dap" runat="server" Font-Size="X-Small" 
                                                                            onrowcreated="gv_transacciones_ddp_dap_RowCreated">
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar_pregunta2" runat="server" 
                                                                            onclick="btn_regresar_pregunta2_Click" Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente17_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente17_pregunta2_Click" style="width: 82px" 
                                                                            Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_prepagado_mp2" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_42" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Hay Rebate?</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl5_pregunta2" runat="server" 
                                                                            RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="True">Si</asp:ListItem>
                                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente6_pregunta2" runat="server" Text="Siguiente" 
                                                                            onclick="btn_siguiente6_pregunta2_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_prepagado_mp3" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        <asp:Label ID="lbl_fechahora_46" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        <asp:Label ID="lbl_campo7" runat="server" Font-Bold="True"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle" colspan="2">
                                                                        <asp:RadioButtonList ID="rbl6_pregunta2" runat="server" Height="25px" 
                                                                            RepeatDirection="Horizontal" Width="274px">
                                                                            <asp:ListItem Value="3">Estandard</asp:ListItem>
                                                                            <asp:ListItem Value="8">Por House</asp:ListItem>
                                                                            <asp:ListItem Value="9">Por CBM</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle" colspan="2">
                                                                        <asp:Label ID="lbl_campo8" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle">
                                                                        Moneda</td>
                                                                    <td align="left" valign="middle">
                                                                        <asp:DropDownList ID="drp_moneda3" runat="server" Enabled="False">
                                                                            <asp:ListItem Value="8">USD</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle" width="100px">
                                                                        Impuestos</td>
                                                                    <td align="left" valign="middle">
                                                                        <asp:DropDownList ID="drp_afecto_excento6" runat="server">
                                                                            <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                                                                            <asp:ListItem Value="1">Excento</asp:ListItem>
                                                                            <asp:ListItem Value="2">Afecto</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle">
                                                                        Monto</td>
                                                                    <td align="left" valign="middle">
                                                                        <asp:TextBox ID="tb_monto_rebate3" runat="server" Height="16px" Width="100px">0.00</asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="tb_monto_rebate_FilteredTextBoxExtender2" 
                                                                            runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                            TargetControlID="tb_monto_rebate3" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" 
                                                                            ControlToValidate="tb_monto_rebate3" ErrorMessage="Error ###.##" 
                                                                            SetFocusOnError="True" ValidationExpression="\d+.\d{2}">
                                                                        </asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle">
                                                                        Observaciones</td>
                                                                    <td align="left" valign="middle">
                                                                        <asp:TextBox ID="tb_observaciones6" runat="server" Height="16px" MaxLength="99" 
                                                                            Width="300px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle" colspan="2">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2" valign="middle">
                                                                        <strong>Datos Factura del Agente</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2" valign="middle">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style9">
                                                                            <tr>
                                                                                <td>
                                                                                    Serie</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_serie_proveedor8" runat="server" Height="16px" Width="75px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    Correlativo</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_correlativo_proveedor8" runat="server" Height="16px" 
                                                                                        Width="75px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    Fecha</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_fecha_proveedor8" runat="server" Height="16px" 
                                                                                        Width="128px"></asp:TextBox>
                                                                                    <cc1:MaskedEditExtender ID="tb_fecha_proveedor8_MaskedEditExtender" 
                                                                                        runat="server" Enabled="True" Mask="99/99/9999" MaskType="Date" 
                                                                                        TargetControlID="tb_fecha_proveedor8"></cc1:MaskedEditExtender>
                                                                                    <cc1:CalendarExtender ID="tb_fecha_proveedor8_CalendarExtender" runat="server" 
                                                                                        Enabled="True" Format="MM/dd/yyyy" TargetControlID="tb_fecha_proveedor8"></cc1:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle" colspan="2">
                                                                        <asp:Button ID="btn_regresar27" runat="server" onclick="btn_regresar27_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente7_pregunta2" runat="server" Text="Siguiente" 
                                                                            onclick="btn_siguiente7_pregunta2_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2" height="4" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_prepagado_mp7" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_47" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style13">
                                                                        <strong>Transacciones Generadas por Rebate, Carga Prepagada al mismo Pais:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:GridView ID="gv_rebates" runat="server" Font-Size="X-Small" 
                                                                            onrowcreated="gv_rebates_RowCreated">
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar28" runat="server" onclick="btn_regresar28_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente10_pregunta2" runat="server" Text="Siguiente" 
                                                                            onclick="btn_siguiente10_pregunta2_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_prepagado_mp9" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style6" colspan="2">
                                                                        <asp:Label ID="lbl_fechahora_48" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style6" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <strong>Cargos al Cliente : HBL FHC PREPAID</strong></td>
                                                                    <td align="right" class="style6">
                                                                        <asp:Button ID="btn_actualizar_cargos2" runat="server" 
                                                                            onclick="btn_actualizar_cargos2_Click" Text="Actualizar Cargos" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style14" valign="middle" colspan="2">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style6">
                                                                            <tr>
                                                                                <td align="center">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center">
                                                                                    <asp:GridView ID="gv_cargos_locales" runat="server" Font-Size="XX-Small" 
                                                                                        EmptyDataText="No existe ningun cargo asociado en el Sistema de Trafico" 
                                                                                        onrowcreated="gv_cargos_locales_RowCreated">
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="NO."><ItemTemplate><asp:Label ID="lbl_cargos_house_correlativo" runat="server" Text="0"></asp:Label></ItemTemplate></asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="SERVICIO"><ItemTemplate><asp:DropDownList ID="drp_cargos_house_servicio" runat="server" 
                                                                                                        AutoPostBack="True" Font-Size="X-Small" 
                                                                                                        Width="70px"></asp:DropDownList></ItemTemplate></asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="CARGO"><ItemTemplate><asp:DropDownList ID="drp_cargos_house_rubro" runat="server" 
                                                                                                        Font-Size="XX-Small" Width="105px"></asp:DropDownList></ItemTemplate></asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="MONEDA"><ItemTemplate><asp:DropDownList ID="drp_cargos_house_moneda" runat="server" 
                                                                                                        Font-Size="XX-Small" Width="46px"></asp:DropDownList></ItemTemplate></asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="MONTO"><ItemTemplate><asp:TextBox ID="tb_cargos_house_monto" runat="server" Font-Size="XX-Small" 
                                                                                                        Height="12px" Width="55px" style=" text-align:right">0.00</asp:TextBox></ItemTemplate></asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="CLASE"><ItemTemplate><asp:RadioButtonList ID="rbl_cargos_house_prep_coll" runat="server" 
                                                                                                        RepeatDirection="Horizontal"><asp:ListItem Value="1">Prepaid</asp:ListItem><asp:ListItem Value="2">Collect</asp:ListItem></asp:RadioButtonList></ItemTemplate></asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="TIPO"><ItemTemplate><asp:RadioButtonList ID="rbl_cargos_house_loc_int" runat="server" 
                                                                                                        RepeatDirection="Horizontal"><asp:ListItem Value="1">Loc</asp:ListItem><asp:ListItem Value="2">Int</asp:ListItem></asp:RadioButtonList></ItemTemplate></asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="CONTA"><ItemTemplate><asp:DropDownList ID="drp_cargos_house_conta" runat="server" 
                                                                                                        Font-Size="XX-Small" Width="60px"><asp:ListItem Value="0">Seleccione...</asp:ListItem><asp:ListItem Value="1">Fiscal</asp:ListItem><asp:ListItem Value="2">Financiera</asp:ListItem></asp:DropDownList></ItemTemplate></asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="BL"><ItemTemplate><asp:RadioButtonList ID="rbl_cargos_house_conocimiento" runat="server" 
                                                                                                        Height="25px" RepeatDirection="Horizontal"><asp:ListItem Value="True">Si</asp:ListItem><asp:ListItem Value="False">No</asp:ListItem></asp:RadioButtonList></ItemTemplate></asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="NOTAS"><ItemTemplate><asp:TextBox ID="tb_cargos_house_observaciones" runat="server" 
                                                                                                        Font-Size="XX-Small" Height="12px" Width="90px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="ALL IN"><ItemTemplate><asp:TextBox ID="tb_cargos_house_allin" runat="server" Font-Size="XX-Small" 
                                                                                                        Height="12px" Width="80px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                                                                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/img/icons/delete.png" 
                                                                                                ShowDeleteButton="True" Visible="False" />
                                                                                            <asp:ButtonField ButtonType="Image" CommandName="Editar" 
                                                                                                ImageUrl="~/img/icons/edit.png" Text="Edit" Visible="False" />
                                                                                            <asp:ButtonField ButtonType="Image" CommandName="Actualizar" 
                                                                                                ImageUrl="~/img/icons/save.png" Text="Update" Visible="False" />
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center">
                                                                                    <asp:Button ID="btn_siguiente20_pregunta2" runat="server" 
                                                                                        onclick="btn_siguiente20_pregunta2_Click" Text="Siguiente" />
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
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_prepagado_tipo_factura1" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        <strong>Definir Tipo de Factura - Carga Prepagada FHC:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <em><strong>Marcar todas como:</strong></em></td>
                                                                    <td align="center" width="500PX">
                                                                        <asp:RadioButtonList ID="rbl_tipo_factura" runat="server" AutoPostBack="True" 
                                                                            Font-Size="XX-Small" 
                                                                            onselectedindexchanged="rbl_tipo_factura_SelectedIndexChanged" 
                                                                            RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="2">FACTURA CONSUMIDOR FINAL</asp:ListItem>
                                                                            <asp:ListItem Value="3">FACTURA CREDITO FISCAL</asp:ListItem>
                                                                            <asp:ListItem Value="4">FACTURA EXPORTACION</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2">
                                                                        <asp:GridView ID="gv_transacciones_tipo_factura1" runat="server" 
                                                                            Font-Size="X-Small" onrowcreated="gv_transacciones_tipo_factura1_RowCreated">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="FACTURA"><ItemTemplate><asp:DropDownList ID="drp_tipo_factura1" runat="server" Font-Size="XX-Small" 
                                                                                            Width="150px"><asp:ListItem Value="1">Seleccione...</asp:ListItem><asp:ListItem Value="2">FACTURA CONSUMIDOR FINAL</asp:ListItem><asp:ListItem Value="3">FACTURA CREDITO FISCAL</asp:ListItem><asp:ListItem Value="4">FACTURA EXPORTACION</asp:ListItem></asp:DropDownList></ItemTemplate></asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2" height="40" valign="middle">
                                                                        &nbsp;<asp:Button ID="btn_regresar39" runat="server" onclick="btn_regresar39_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;
                                                                        <asp:Button ID="btn_siguiente9" runat="server" onclick="btn_siguiente9_Click" 
                                                                            Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2" height="40" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_prepagado_mp10" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_49" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Transacciones generadas por Cargos BL FHC Prepagado:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:GridView ID="gv_transacciones_cargos_locales_hbls" runat="server" Font-Size="X-Small" onrowcreated="gv_transacciones_cargos_locales_hbls_RowCreated">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="IMPUESTOS"><ItemTemplate><asp:DropDownList ID="drp_afecto_excento11" runat="server" Font-Size="XX-Small" 
                                                                                            Visible="False" Width="60px"><asp:ListItem Value="0">Seleccione...</asp:ListItem><asp:ListItem Value="1">Excento</asp:ListItem><asp:ListItem Value="2">Afecto</asp:ListItem></asp:DropDownList></ItemTemplate></asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="SERIE"><ItemTemplate><asp:TextBox ID="tb_serie_proveedor13" runat="server" Height="12px" 
                                                                                            Width="60px" ToolTip="Serie de Factura del Agente" Font-Size="XX-Small" 
                                                                                            Visible="False" MaxLength="50"></asp:TextBox></ItemTemplate></asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="CORR"><ItemTemplate><asp:TextBox ID="tb_correlativo_proveedor13" runat="server" Height="12px" 
                                                                                            Width="60px" ToolTip="Correlativo de Factura del Agente" 
                                                                                            Font-Size="XX-Small" Visible="False" MaxLength="50"></asp:TextBox></ItemTemplate></asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="FECHA"><ItemTemplate><asp:TextBox ID="tb_fecha_proveedor13" runat="server" Height="12px" 
                                                                                            Width="50px" Font-Size="XX-Small" ToolTip="Fecha de la Factura del Agente" 
                                                                                            Visible="False" MaxLength="50"></asp:TextBox><cc1:MaskedEditExtender ID="tb_fecha_proveedor13_MaskedEditExtender" 
                                                                                            runat="server" Enabled="True" Mask="99/99/9999" MaskType="Date" 
                                                                                            TargetControlID="tb_fecha_proveedor13"></cc1:MaskedEditExtender><cc1:CalendarExtender ID="tb_fecha_proveedor13_CalendarExtender" runat="server" 
                                                                                            Enabled="True" Format="MM/dd/yyyy" TargetControlID="tb_fecha_proveedor13"></cc1:CalendarExtender></ItemTemplate></asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        &nbsp;<asp:Button ID="btn_regresar5" runat="server" onclick="btn_regresar5_Click" 
                                                                            Text="Regresar" />
&nbsp;
                                                                        <asp:Button ID="btn_siguiente21_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente21_pregunta2_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_prepagado_op" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" style="margin-left: 40px">
                                                                        <asp:Label ID="lbl_fechahora_50" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_comentario_personal3" runat="server" Text="FHC-PREPAID-OP" 
                                                                            Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Hay Rebate?</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl1" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="True">Si</asp:ListItem>
                                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente1" runat="server" onclick="btn_siguiente1_Click" 
                                                                            Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_prepagado_op1" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        <asp:Label ID="lbl_fechahora_51" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        <asp:Label ID="lbl_campo9" runat="server" Font-Bold="True"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle" colspan="2">
                                                                        <asp:RadioButtonList ID="rbl11_pregunta2" runat="server" 
                                                                            RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="3">Estandard</asp:ListItem>
                                                                            <asp:ListItem Value="8">Por House</asp:ListItem>
                                                                            <asp:ListItem Value="9">Por CBM</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle" colspan="2">
                                                                        <asp:Label ID="lbl_campo10" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        Moneda</td>
                                                                    <td align="left" valign="middle">
                                                                        <asp:DropDownList ID="drp_moneda4" runat="server" Enabled="False">
                                                                            <asp:ListItem Value="8">USD</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle" width="100px">
                                                                        Impuestos</td>
                                                                    <td align="left" valign="middle">
                                                                        <asp:DropDownList ID="drp_afecto_excento7" runat="server">
                                                                            <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                                                                            <asp:ListItem Value="1">Excento</asp:ListItem>
                                                                            <asp:ListItem Value="2">Afecto</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        Monto</td>
                                                                    <td align="left" valign="middle">
                                                                        <asp:TextBox ID="tb_monto_rebate4" runat="server" Height="16px" Width="100px">0.00</asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="tb_monto_rebate_FilteredTextBoxExtender4" 
                                                                            runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                            TargetControlID="tb_monto_rebate4" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" 
                                                                            ControlToValidate="tb_monto_rebate4" ErrorMessage="Error ###.##" 
                                                                            SetFocusOnError="True" ValidationExpression="\d+.\d{2}">
                                                                            </asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        Observaciones</td>
                                                                    <td align="left" valign="middle">
                                                                        <asp:TextBox ID="tb_observaciones7" runat="server" Height="16px" MaxLength="99" 
                                                                            Width="300px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" colspan="2" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2" valign="middle">
                                                                        <strong>Datos Factura del Agente</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2" valign="middle">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style9">
                                                                            <tr>
                                                                                <td>
                                                                                    Serie</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_serie_proveedor9" runat="server" Height="16px" Width="75px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    Correlativo</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_correlativo_proveedor9" runat="server" Height="16px" 
                                                                                        Width="75px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    Fecha</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_fecha_proveedor9" runat="server" Height="16px" 
                                                                                        Width="128px"></asp:TextBox>
                                                                                    <cc1:MaskedEditExtender ID="tb_fecha_proveedor9_MaskedEditExtender" 
                                                                                        runat="server" Enabled="True" Mask="99/99/9999" MaskType="Date" 
                                                                                        TargetControlID="tb_fecha_proveedor9"></cc1:MaskedEditExtender>
                                                                                    <cc1:CalendarExtender ID="tb_fecha_proveedor9_CalendarExtender" runat="server" 
                                                                                        Enabled="True" Format="MM/dd/yyyy" TargetControlID="tb_fecha_proveedor9"></cc1:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle" colspan="2">
                                                                        <asp:Button ID="btn_regresar29" runat="server" onclick="btn_regresar29_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btn_siguiente22_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente22_pregunta2_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_prepagado_op2" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_52" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Transacciones Generadas por Rebate, Carga Prepagada a otro Pais:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:GridView ID="gv_rebates2" runat="server" Font-Size="X-Small" 
                                                                            onrowcreated="gv_rebates2_RowCreated">
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        &nbsp;<asp:Button ID="btn_regresar30" runat="server" onclick="btn_regresar30_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente27_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente27_pregunta2_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_prepagado_op3" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_53" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Existen Cargos Adicionales</strong> por Transporte a Otro Pais?</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl_30" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="True">Si</asp:ListItem>
                                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente2" runat="server" onclick="btn_siguiente2_Click" 
                                                                            Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_prepagado_op4" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <asp:Label ID="lbl_fechahora_54" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        Cargo por Transporte a otro Pais: (FHC-P-OP)</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style14" valign="middle">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style6">
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_codigo_transportista4" runat="server" Height="16px" 
                                                                                        ReadOnly="True" Visible="False" Width="50px">20</asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="150px">
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_nombre_transportista4" runat="server" Height="16px" 
                                                                                        ReadOnly="True" Width="300px" Visible="False">MAYAN LOGISTICS NICARAGUA</asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_routing_terrestre3" runat="server" Height="16px" 
                                                                                        Width="200px" Visible="False"></asp:TextBox>
                                                                                    <cc1:ModalPopupExtender ID="modalrouting5" 
                                                                                        runat="server" BackgroundCssClass="FondoAplicacion" 
                                                                                        CancelControlID="btn_routing_cancelar" DropShadow="True" 
                                                                                        OnCancelScript="mpeClienteOnCancel()" PopupControlID="pnlRouting5" 
                                                                                        TargetControlID="tb_routing_terrestre3" />
                                                                                    <asp:TextBox ID="tb_routing_terrestre_ID3" runat="server" Height="16px" 
                                                                                        Visible="False" Width="40px">0</asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Servicio</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p2_servicio12" runat="server" Enabled="False">
                                                                                        <asp:ListItem Value="15">INTERMODAL</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Rubro</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p2_rubro12" runat="server" Enabled="False" 
                                                                                        Height="17px">
                                                                                        <asp:ListItem Value="62">FLETE TERRESTRE</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Moneda&nbsp;</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p2_moneda13" runat="server" Enabled="False">
                                                                                        <asp:ListItem Value="8">USD</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:Label ID="lbl_campo11" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:TextBox ID="tb_tarifa_intermodal2" runat="server" Height="16px" 
                                                                                        Width="100px">0.00</asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="tb_tarifa_intermodal2_FilteredTextBoxExtender" 
                                                                                        runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                                        TargetControlID="tb_tarifa_intermodal2" ValidChars="."></cc1:FilteredTextBoxExtender>
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
                                                                    <td align="center" class="style6" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente48_pregunta3" runat="server" 
                                                                            onclick="btn_siguiente48_pregunta3_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_prepagado_op5" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_55" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Transacciones generadas por Transporte hacia otro Pais, Carga FHC 
                                                                        Prepagada:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:GridView ID="gv_transacciones_routing8" runat="server" Font-Size="X-Small" 
                                                                            onrowcreated="gv_transacciones_routing8_RowCreated">
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar31" runat="server" onclick="btn_regresar31_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente55_pregunta4" runat="server" 
                                                                            onclick="btn_siguiente55_pregunta4_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_prepagado_op8" runat="server" Visible="False">
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_collect_mp" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_56" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_comentario_personal2" runat="server" Text="FHC-COLLECT-MP" 
                                                                            Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Hay Rebate?</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl13_pregunta2" runat="server" 
                                                                            RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="True">Si</asp:ListItem>
                                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente28_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente28_pregunta2_Click" Text="Siguiente" 
                                                                            style="height: 26px" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_collect_mp2" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        <asp:Label ID="lbl_fechahora_57" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        <asp:Label ID="lbl_campo12" runat="server" Font-Bold="True"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle" colspan="2">
                                                                        <asp:RadioButtonList ID="rbl31" runat="server" Height="25px" 
                                                                            RepeatDirection="Horizontal" Width="274px">
                                                                            <asp:ListItem Value="3">Estandard</asp:ListItem>
                                                                            <asp:ListItem Value="8">Por House</asp:ListItem>
                                                                            <asp:ListItem Value="9">Por CBM</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle" colspan="2">
                                                                        <asp:Label ID="lbl_campo13" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle">
                                                                        Moneda</td>
                                                                    <td align="left" valign="middle">
                                                                        <asp:DropDownList ID="drp_moneda5" runat="server" Enabled="False">
                                                                            <asp:ListItem Value="8">USD</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle" width="100px">
                                                                        Impuestos</td>
                                                                    <td align="left" valign="middle">
                                                                        <asp:DropDownList ID="drp_afecto_excento8" runat="server">
                                                                            <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                                                                            <asp:ListItem Value="1">Excento</asp:ListItem>
                                                                            <asp:ListItem Value="2">Afecto</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle">
                                                                        Monto
                                                                    </td>
                                                                    <td align="left" valign="middle">
                                                                        <asp:TextBox ID="tb_monto_rebate13" runat="server" Height="16px" Width="100px">0.00</asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="tb_monto_rebate13_FilteredTextBoxExtender" 
                                                                            runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                            TargetControlID="tb_monto_rebate13" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator28" 
                                                                            runat="server" ControlToValidate="tb_monto_rebate13" 
                                                                            ErrorMessage="Error ###.##" SetFocusOnError="True" 
                                                                            ValidationExpression="\d+.\d{2}">&nbsp; 
                                                                            
                                                                            </asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle">
                                                                        Observaciones</td>
                                                                    <td align="left" valign="middle">
                                                                        <asp:TextBox ID="tb_observaciones8" runat="server" Height="16px" MaxLength="99" 
                                                                            Width="300px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle" colspan="2">
                                                                        
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2" valign="middle">
                                                                        <strong>Datos Factura del Agente</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2" valign="middle">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style9">
                                                                            <tr>
                                                                                <td>
                                                                                    Serie</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_serie_proveedor10" runat="server" Height="16px" 
                                                                                        Width="75px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    Correlativo</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_correlativo_proveedor10" runat="server" Height="16px" 
                                                                                        Width="75px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    Fecha</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_fecha_proveedor10" runat="server" Height="16px" 
                                                                                        Width="128px"></asp:TextBox>
                                                                                    <cc1:MaskedEditExtender ID="tb_fecha_proveedor10_MaskedEditExtender" 
                                                                                        runat="server" Enabled="True" Mask="99/99/9999" MaskType="Date" 
                                                                                        TargetControlID="tb_fecha_proveedor10"></cc1:MaskedEditExtender>
                                                                                    <cc1:CalendarExtender ID="tb_fecha_proveedor10_CalendarExtender" runat="server" 
                                                                                        Enabled="True" Format="MM/dd/yyyy" TargetControlID="tb_fecha_proveedor10"></cc1:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle" colspan="2">
                                                                        <asp:Button ID="btn_regresar32" runat="server" onclick="btn_regresar32_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente3" runat="server" 
                                                                            onclick="btn_siguiente3_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_collect_mp3" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_58" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Transacciones Generadas por Rebate, Carga Collect al mismo Pais:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:GridView ID="gv_rebates3" runat="server" Font-Size="X-Small" 
                                                                            onrowcreated="gv_rebates3_RowCreated">
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        &nbsp;<asp:Button ID="btn_regresar33" runat="server" onclick="btn_regresar33_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente33_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente33_pregunta2_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_collect_mp4" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="center" class="style14" valign="middle">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style6">
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:Label ID="lbl_fechahora_59" runat="server" Visible="False"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:Label ID="lbl_comentario_personal1" runat="server" Text="FHC-COLLECT-MP" 
                                                                                        Visible="False"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <strong>Cargos al Cliente : HBL FHC COLLECT</strong></td>
                                                                                <td align="right">
                                                                                    <asp:Button ID="btn_actualizar_cargos3" runat="server" 
                                                                                        onclick="btn_actualizar_cargos3_Click" Text="Actualizar Cargos" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:GridView ID="gv_cargos_locales2" runat="server" Font-Size="XX-Small" 
                                                                                        onrowcommand="gv_cargos_locales2_RowCommand" 
                                                                                        onrowcreated="gv_cargos_locales2_RowCreated" 
                                                                                        onrowdeleting="gv_cargos_locales2_RowDeleting">
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="NO."><ItemTemplate><asp:Label ID="lbl_cargos_house_correlativo2" runat="server" Text="0"></asp:Label></ItemTemplate></asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="SERVICIO"><ItemTemplate><asp:DropDownList ID="drp_cargos_house_servicio2" runat="server" 
                                                                                                        AutoPostBack="True" Font-Size="X-Small" 
                                                                                                        onselectedindexchanged="drp_cargos_house_servicio2_SelectedIndexChanged" 
                                                                                                        Width="70px"></asp:DropDownList></ItemTemplate></asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="CARGO"><ItemTemplate><asp:DropDownList ID="drp_cargos_house_rubro2" runat="server" 
                                                                                                        Font-Size="XX-Small" Width="105px"></asp:DropDownList></ItemTemplate></asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="MONEDA"><ItemTemplate><asp:DropDownList ID="drp_cargos_house_moneda2" runat="server" 
                                                                                                        Font-Size="XX-Small" Width="46px"></asp:DropDownList></ItemTemplate></asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="MONTO"><ItemTemplate><asp:TextBox ID="tb_cargos_house_monto2" runat="server" Font-Size="XX-Small" 
                                                                                                        Height="12px" Width="55px" style=" text-align:right">0.00</asp:TextBox></ItemTemplate></asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="CLASE"><ItemTemplate><asp:RadioButtonList ID="rbl_cargos_house_prep_coll2" runat="server" 
                                                                                                        RepeatDirection="Horizontal"><asp:ListItem Value="1">Prepaid</asp:ListItem><asp:ListItem Value="2">Collect</asp:ListItem></asp:RadioButtonList></ItemTemplate></asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="TIPO"><ItemTemplate><asp:RadioButtonList ID="rbl_cargos_house_loc_int2" runat="server" 
                                                                                                        RepeatDirection="Horizontal"><asp:ListItem Value="1">Loc</asp:ListItem><asp:ListItem Value="2">Int</asp:ListItem></asp:RadioButtonList></ItemTemplate></asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="CONTA"><ItemTemplate><asp:DropDownList ID="drp_cargos_house_conta2" runat="server" 
                                                                                                        Font-Size="XX-Small" Width="60px"><asp:ListItem Value="0">Seleccione...</asp:ListItem><asp:ListItem Value="1">Fiscal</asp:ListItem><asp:ListItem Value="2">Financiera</asp:ListItem></asp:DropDownList></ItemTemplate></asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="BL"><ItemTemplate><asp:RadioButtonList ID="rbl_cargos_house_conocimiento2" runat="server" 
                                                                                                        Height="25px" RepeatDirection="Horizontal"><asp:ListItem Value="True">Si</asp:ListItem><asp:ListItem Value="False">No</asp:ListItem></asp:RadioButtonList></ItemTemplate></asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="NOTAS"><ItemTemplate><asp:TextBox ID="tb_cargos_house_observaciones2" runat="server" 
                                                                                                        Font-Size="XX-Small" Height="12px" Width="90px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="ALL IN"><ItemTemplate><asp:TextBox ID="tb_cargos_house_allin2" runat="server" Font-Size="XX-Small" 
                                                                                                        Height="12px" Width="80px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                                                                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="~/img/icons/delete.png" 
                                                                                                ShowDeleteButton="True" Visible="False" />
                                                                                            <asp:ButtonField ButtonType="Image" CommandName="Editar" 
                                                                                                ImageUrl="~/img/icons/edit.png" Text="Edit" Visible="False" />
                                                                                            <asp:ButtonField ButtonType="Image" CommandName="Actualizar" 
                                                                                                ImageUrl="~/img/icons/save.png" Text="Update" Visible="False" />
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:Button ID="btn_siguiente35_pregunta2" runat="server" 
                                                                                        onclick="btn_siguiente35_pregunta2_Click" Text="Siguiente" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_collect_tipo_factura2" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        <strong>Definir Tipo de Factura - Carga Collect FHC:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <em><strong>Marcar todas como:</strong></em></td>
                                                                    <td align="center" width="500PX">
                                                                        <asp:RadioButtonList ID="rbl_tipo_factura2" runat="server" AutoPostBack="True" 
                                                                            Font-Size="XX-Small" 
                                                                            onselectedindexchanged="rbl_tipo_factura2_SelectedIndexChanged" 
                                                                            RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="2">FACTURA CONSUMIDOR FINAL</asp:ListItem>
                                                                            <asp:ListItem Value="3">FACTURA CREDITO FISCAL</asp:ListItem>
                                                                            <asp:ListItem Value="4">FACTURA EXPORTACION</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2">
                                                                        <asp:GridView ID="gv_transacciones_tipo_factura2" runat="server" 
                                                                            Font-Size="X-Small" onrowcreated="gv_transacciones_tipo_factura2_RowCreated">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="FACTURA"><ItemTemplate><asp:DropDownList ID="drp_tipo_factura2" runat="server" Font-Size="XX-Small" 
                                                                                            Width="150px"><asp:ListItem Value="1">Seleccione...</asp:ListItem><asp:ListItem Value="2">FACTURA CONSUMIDOR FINAL</asp:ListItem><asp:ListItem Value="3">FACTURA CREDITO FISCAL</asp:ListItem><asp:ListItem Value="4">FACTURA EXPORTACION</asp:ListItem></asp:DropDownList></ItemTemplate></asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2" height="40" valign="middle">
                                                                        &nbsp;<asp:Button ID="btn_regresar40" runat="server" onclick="btn_regresar40_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;
                                                                        <asp:Button ID="btn_siguiente16" runat="server" onclick="btn_siguiente16_Click" 
                                                                            Text="Siguiente" style="height: 26px" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2" height="40" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_collect_mp5" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_60" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Transacciones generadas por Cargos BL FHC Collect:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:GridView ID="gv_transacciones_cargos_locales_hbls2" runat="server" Font-Size="X-Small" onrowcreated="gv_transacciones_cargos_locales_hbls2_RowCreated">
                                                                                                                                                        <Columns>
                                                                                <asp:TemplateField HeaderText="IMPUESTOS"><ItemTemplate><asp:DropDownList ID="drp_afecto_excento12" runat="server" Font-Size="XX-Small" 
                                                                                            Visible="False" Width="60px"><asp:ListItem Value="0">Seleccione...</asp:ListItem><asp:ListItem Value="1">Excento</asp:ListItem><asp:ListItem Value="2">Afecto</asp:ListItem></asp:DropDownList></ItemTemplate></asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="SERIE"><ItemTemplate><asp:TextBox ID="tb_serie_proveedor14" runat="server" Height="12px" 
                                                                                            Width="60px" ToolTip="Serie de Factura del Agente" Font-Size="XX-Small" 
                                                                                            Visible="False" MaxLength="50"></asp:TextBox></ItemTemplate></asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="CORR"><ItemTemplate><asp:TextBox ID="tb_correlativo_proveedor14" runat="server" Height="12px" 
                                                                                            Width="60px" ToolTip="Correlativo de Factura del Agente" 
                                                                                            Font-Size="XX-Small" Visible="False" MaxLength="50"></asp:TextBox></ItemTemplate></asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="FECHA"><ItemTemplate><asp:TextBox ID="tb_fecha_proveedor14" runat="server" Height="12px" 
                                                                                            Width="50px" Font-Size="XX-Small" ToolTip="Fecha de la Factura del Agente" 
                                                                                            Visible="False" MaxLength="50"></asp:TextBox><cc1:MaskedEditExtender ID="tb_fecha_proveedor14_MaskedEditExtender" 
                                                                                            runat="server" Enabled="True" Mask="99/99/9999" MaskType="Date" 
                                                                                            TargetControlID="tb_fecha_proveedor14"></cc1:MaskedEditExtender><cc1:CalendarExtender ID="tb_fecha_proveedor14_CalendarExtender" runat="server" 
                                                                                            Enabled="True" Format="MM/dd/yyyy" TargetControlID="tb_fecha_proveedor14"></cc1:CalendarExtender></ItemTemplate></asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        &nbsp;<asp:Button ID="btn_regresar9" runat="server" onclick="btn_regresar9_Click1" 
                                                                            Text="Regresar" />
                                                                        &nbsp;
                                                                        <asp:Button ID="btn_siguiente36_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente36_pregunta2_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_collect_mp10" runat="server" Visible="False">
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_collect_op1" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_comentario_personal5" runat="server" Text="FHC-COLLECT-OP" 
                                                                            Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_61" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Hay Rebate?</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl19_pregunta2" runat="server" 
                                                                            RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="True">Si</asp:ListItem>
                                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente37_pregunta2" runat="server" Text="Siguiente" 
                                                                            onclick="btn_siguiente37_pregunta2_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_collect_op2" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        <asp:Label ID="lbl_fechahora_62" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        <asp:Label ID="lbl_campo14" runat="server" Font-Bold="True"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle" colspan="2">
                                                                        <asp:RadioButtonList ID="rbl32" runat="server" Height="25px" 
                                                                            RepeatDirection="Horizontal" Width="274px">
                                                                            <asp:ListItem Value="3">Estandard</asp:ListItem>
                                                                            <asp:ListItem Value="8">Por House</asp:ListItem>
                                                                            <asp:ListItem Value="9">Por CBM</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle" colspan="2">
                                                                        <asp:Label ID="lbl_campo15" runat="server"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle">
                                                                        Moneda</td>
                                                                    <td align="left" valign="middle">
                                                                        <asp:DropDownList ID="drp_moneda6" runat="server" Enabled="False">
                                                                            <asp:ListItem Value="8">USD</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle" width="100px">
                                                                        Impuestos</td>
                                                                    <td align="left" valign="middle">
                                                                        <asp:DropDownList ID="drp_afecto_excento9" runat="server">
                                                                            <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                                                                            <asp:ListItem Value="1">Excento</asp:ListItem>
                                                                            <asp:ListItem Value="2">Afecto</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle">
                                                                        Monto
                                                                    </td>
                                                                    <td align="left" valign="middle">
                                                                        <asp:TextBox ID="tb_monto_rebate14" runat="server" Height="16px" Width="100px">0.00</asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="tb_monto_rebate14_FilteredTextBoxExtender" 
                                                                            runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                            TargetControlID="tb_monto_rebate14" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator29" 
                                                                            runat="server" ControlToValidate="tb_monto_rebate13" 
                                                                            ErrorMessage="Error ###.##" SetFocusOnError="True" 
                                                                            ValidationExpression="\d+.\d{2}">

                                                                        </asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" valign="middle">
                                                                        Observaciones</td>
                                                                    <td align="left" valign="middle">
                                                                        <asp:TextBox ID="tb_observaciones9" runat="server" Height="16px" MaxLength="99" 
                                                                            Width="300px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        <strong>Datos Factura del Agente</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style9">
                                                                            <tr>
                                                                                <td>
                                                                                    Serie</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_serie_proveedor11" runat="server" Height="16px" 
                                                                                        Width="75px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    Correlativo</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_correlativo_proveedor11" runat="server" Height="16px" 
                                                                                        Width="75px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    Fecha</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_fecha_proveedor11" runat="server" Height="16px" 
                                                                                        Width="128px"></asp:TextBox>
                                                                                    <cc1:MaskedEditExtender ID="tb_fecha_proveedor11_MaskedEditExtender" 
                                                                                        runat="server" Enabled="True" Mask="99/99/9999" MaskType="Date" 
                                                                                        TargetControlID="tb_fecha_proveedor11"></cc1:MaskedEditExtender>
                                                                                    <cc1:CalendarExtender ID="tb_fecha_proveedor11_CalendarExtender" runat="server" 
                                                                                        Enabled="True" Format="MM/dd/yyyy" TargetControlID="tb_fecha_proveedor11"></cc1:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle" colspan="2">
                                                                        <asp:Button ID="btn_regresar34" runat="server" onclick="btn_regresar34_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente4" runat="server" onclick="btn_siguiente4_Click" 
                                                                            Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_collect_op3" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_63" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Transacciones Generadas por Rebate, Carga Collect a otro Pais:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:GridView ID="gv_rebates4" runat="server" Font-Size="X-Small" 
                                                                            onrowcreated="gv_rebates4_RowCreated">
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        &nbsp;<asp:Button ID="btn_regresar35" runat="server" onclick="btn_regresar35_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente41_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente41_pregunta2_Click" Text="Siguiente" 
                                                                            style="height: 26px" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_collect_op4" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_64" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Existen Cargos Adicionales</strong> por Transporte a Otro Pais?
                                                                        <br />
                                                                        FHC-C-OP</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl_31" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="True">Si</asp:ListItem>
                                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente5" runat="server" onclick="btn_siguiente5_Click" 
                                                                            Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_collect_op5" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <asp:Label ID="lbl_fechahora_65" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        Cargo por Transporte a otro Pais: (FHC-C-OP)</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style14" valign="middle">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style6">
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_codigo_transportista5" runat="server" Height="16px" 
                                                                                        ReadOnly="True" Visible="False" Width="50px">20</asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="150px">
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_nombre_transportista5" runat="server" Height="16px" 
                                                                                        ReadOnly="True" Width="300px" Visible="False">MAYAN LOGISTICS NICARAGUA</asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_routing_terrestre4" runat="server" Height="16px" 
                                                                                        Width="200px" Visible="False"></asp:TextBox>
                                                                                    <cc1:ModalPopupExtender ID="modalrouting6" 
                                                                                        runat="server" BackgroundCssClass="FondoAplicacion" 
                                                                                        CancelControlID="btn_routing_cancelar" DropShadow="True" 
                                                                                        OnCancelScript="mpeClienteOnCancel()" PopupControlID="pnlRouting6" 
                                                                                        TargetControlID="tb_routing_terrestre4" />
                                                                                    <asp:TextBox ID="tb_routing_terrestre_ID4" runat="server" Height="16px" 
                                                                                        Visible="False" Width="40px">0</asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Servicio</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p2_servicio13" runat="server" Enabled="False">
                                                                                        <asp:ListItem Value="15">INTERMODAL</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Rubro</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p2_rubro13" runat="server" Enabled="False" 
                                                                                        Height="17px">
                                                                                        <asp:ListItem Value="62">FLETE TERRESTRE</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Moneda&nbsp;</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p2_moneda14" runat="server" Enabled="False">
                                                                                        <asp:ListItem Value="8">USD</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:Label ID="lbl_campo16" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:TextBox ID="tb_tarifa_intermodal3" runat="server" Height="16px" 
                                                                                        Width="100px">0.00</asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="tb_tarifa_intermodal3_FilteredTextBoxExtender" 
                                                                                        runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                                        TargetControlID="tb_tarifa_intermodal3" ValidChars="."></cc1:FilteredTextBoxExtender>
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
                                                                    <td align="center" class="style6" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente6" runat="server" 
                                                                            onclick="btn_siguiente6_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_collect_op6" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_66" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Transacciones generadas por Transporte hacia otro Pais, Carga FHC 
                                                                        Collect:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:GridView ID="gv_cargos_fhc_collect_op" runat="server" Font-Size="X-Small" 
                                                                            onrowcreated="gv_cargos_fhc_collect_op_RowCreated">
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar10" runat="server" onclick="btn_regresar10_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente43_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente43_pregunta2_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_fhc_collect_op7" runat="server" Visible="False">
                                                        </asp:Panel>
                                                    </asp:View>
                                                    <asp:View ID="View5" runat="server">
                                                        <asp:Panel ID="pnl_routing_order1_0" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" height="20">
                                                                        <asp:Label ID="lbl_fechahora_74" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        Existe Coloading Rate por pagar al Agente?</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl36" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="TRUE">Si</asp:ListItem>
                                                                            <asp:ListItem Value="FALSE">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente19" runat="server" onclick="btn_siguiente19_Click" 
                                                                            Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_routing_order1" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style27">
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lbl_fechahora_25" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <asp:Label ID="lbl_campo4" runat="server" Font-Bold="True"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Moneda</td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="drp_moneda7" runat="server" Enabled="False">
                                                                            <asp:ListItem Value="8">USD</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td width="100px">
                                                                        Impuestos</td>
                                                                    <td align="left">
                                                                        <asp:DropDownList ID="drp_afecto_excento10" runat="server">
                                                                            <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                                                                            <asp:ListItem Value="1">Excento</asp:ListItem>
                                                                            <asp:ListItem Value="2">Afecto</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Monto</td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="tb_coloading_rate1" runat="server" Height="16px" Width="100px">0.00</asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="tb_coloading_rate1_FilteredTextBoxExtender" 
                                                                            runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                            TargetControlID="tb_coloading_rate1" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Observaciones</td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="tb_observaciones10" runat="server" Height="16px" 
                                                                            MaxLength="99" Width="300px"></asp:TextBox>
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
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        <strong>Datos Factura del Agente</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style9">
                                                                            <tr>
                                                                                <td>
                                                                                    Serie</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_serie_proveedor12" runat="server" Height="16px" 
                                                                                        Width="75px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    Correlativo</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_correlativo_proveedor12" runat="server" Height="16px" 
                                                                                        Width="75px"></asp:TextBox>
                                                                                </td>
                                                                                <td>
                                                                                    Fecha</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_fecha_proveedor12" runat="server" Height="16px" 
                                                                                        Width="128px"></asp:TextBox>
                                                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" 
                                                                                        runat="server" Enabled="True" Mask="99/99/9999" MaskType="Date" 
                                                                                        TargetControlID="tb_fecha_proveedor12"></cc1:MaskedEditExtender>
                                                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                                                                                        Enabled="True" Format="MM/dd/yyyy" TargetControlID="tb_fecha_proveedor12"></cc1:CalendarExtender>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2">
                                                                        <asp:Button ID="btn_regresar43" runat="server" onclick="btn_regresar43_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente45_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente45_pregunta2_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_routing_collect_mp" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style6">
                                                                <tr>
                                                                    <td align="center" colspan="2">
                                                                        <asp:Label ID="lbl_fechahora_27" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Cargos al Cliente : HBL RUTEADO</strong></td>
                                                                    <td align="right">
                                                                        <asp:Button ID="btn_actualizar_cargos1" runat="server" 
                                                                            onclick="btn_actualizar_cargos1_Click" Text="Actualizar Cargos" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        <asp:Label ID="lbl_empresa_a" runat="server" Font-Bold="True" 
                                                                            Font-Italic="True" Font-Size="Small" Font-Underline="True" 
                                                                            Text="Aimar Guatemala"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2">
                                                                        <asp:GridView ID="gv_cargos_locales3" runat="server" Font-Size="XX-Small" 
                                                                            onrowcreated="gv_cargos_locales3_RowCreated" 
                                                                            onrowdeleting="gv_cargos_locales3_RowDeleting">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="NO."><ItemTemplate><asp:Label ID="lbl_cargos_house_correlativo3" runat="server" Text="0"></asp:Label></ItemTemplate></asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="SERVICIO"><ItemTemplate><asp:DropDownList ID="drp_cargos_house_servicio3" runat="server" 
                                                                                            AutoPostBack="True" Font-Size="X-Small" 
                                                                                            onselectedindexchanged="drp_cargos_house_servicio2_SelectedIndexChanged" 
                                                                                            Width="70px"></asp:DropDownList></ItemTemplate></asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="CARGO"><ItemTemplate><asp:DropDownList ID="drp_cargos_house_rubro3" runat="server" 
                                                                                            Font-Size="XX-Small" Width="105px"></asp:DropDownList></ItemTemplate></asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="MONEDA"><ItemTemplate><asp:DropDownList ID="drp_cargos_house_moneda3" runat="server" 
                                                                                            Font-Size="XX-Small" Width="46px"></asp:DropDownList></ItemTemplate></asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="MONTO"><ItemTemplate><asp:TextBox ID="tb_cargos_house_monto3" runat="server" Font-Size="XX-Small" 
                                                                                            Height="12px" Width="55px" style=" text-align:right">0.00</asp:TextBox></ItemTemplate></asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="CLASE"><ItemTemplate><asp:RadioButtonList ID="rbl_cargos_house_prep_coll3" runat="server" 
                                                                                            RepeatDirection="Horizontal"><asp:ListItem Value="1">Prepaid</asp:ListItem><asp:ListItem Value="2">Collect</asp:ListItem></asp:RadioButtonList></ItemTemplate></asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="TIPO"><ItemTemplate><asp:RadioButtonList ID="rbl_cargos_house_loc_int3" runat="server" 
                                                                                            RepeatDirection="Horizontal"><asp:ListItem Value="1">Loc</asp:ListItem><asp:ListItem Value="2">Int</asp:ListItem></asp:RadioButtonList></ItemTemplate></asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="CONTA" Visible="False"><ItemTemplate><asp:DropDownList ID="drp_cargos_house_conta3" runat="server" 
                                                                                            Font-Size="XX-Small" Width="60px"><asp:ListItem Value="0">Seleccione...</asp:ListItem><asp:ListItem Value="1">Fiscal</asp:ListItem><asp:ListItem Value="2">Financiera</asp:ListItem></asp:DropDownList></ItemTemplate></asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="C.E." Visible="False"><ItemTemplate><asp:RadioButtonList ID="rbl_cargos_house_conocimiento3" runat="server" 
                                                                                            Height="25px" RepeatDirection="Horizontal"><asp:ListItem Value="True">Si</asp:ListItem><asp:ListItem Value="False">No</asp:ListItem></asp:RadioButtonList></ItemTemplate></asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="NOTAS"><ItemTemplate><asp:TextBox ID="tb_cargos_house_observaciones3" runat="server" 
                                                                                            Font-Size="XX-Small" Height="12px" Width="90px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="ALL IN"><ItemTemplate><asp:TextBox ID="tb_cargos_house_allin3" runat="server" Font-Size="XX-Small" 
                                                                                            Height="12px" Width="80px"></asp:TextBox></ItemTemplate></asp:TemplateField>
                                                                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/img/icons/delete.png" 
                                                                                    ShowDeleteButton="True" Visible="False" />
                                                                                <asp:ButtonField ButtonType="Image" CommandName="Editar" 
                                                                                    ImageUrl="~/img/icons/edit.png" Text="Edit" Visible="False" />
                                                                                <asp:ButtonField ButtonType="Image" CommandName="Actualizar" 
                                                                                    ImageUrl="~/img/icons/save.png" Text="Update" Visible="False" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        <asp:Label ID="lbl_empresa_b" runat="server" Font-Bold="True" 
                                                                            Font-Italic="True" Font-Size="Small" Font-Underline="True">World Maritime Transport (WMT)</asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2">
                                                                        <asp:GridView ID="gv_cargos_locales5" runat="server" Font-Size="XX-Small" 
                                                                            onrowcreated="gv_cargos_locales3_RowCreated" 
                                                                            onrowdeleting="gv_cargos_locales3_RowDeleting">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="NO.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl_cargos_house_correlativo5" runat="server" Text="0"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="SERVICIO">
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="drp_cargos_house_servicio5" runat="server" 
                                                                                            AutoPostBack="True" Font-Size="X-Small" 
                                                                                            onselectedindexchanged="drp_cargos_house_servicio2_SelectedIndexChanged" 
                                                                                            Width="70px">
                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="CARGO">
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="drp_cargos_house_rubro5" runat="server" 
                                                                                            Font-Size="XX-Small" Width="105px">
                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="MONEDA">
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="drp_cargos_house_moneda5" runat="server" 
                                                                                            Font-Size="XX-Small" Width="46px">
                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="MONTO">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="tb_cargos_house_monto5" runat="server" Font-Size="XX-Small" 
                                                                                            Height="12px" style=" text-align:right" Width="55px">0.00</asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="CLASE">
                                                                                    <ItemTemplate>
                                                                                        <asp:RadioButtonList ID="rbl_cargos_house_prep_coll5" runat="server" 
                                                                                            RepeatDirection="Horizontal">
                                                                                            <asp:ListItem Value="1">Prepaid</asp:ListItem>
                                                                                            <asp:ListItem Value="2">Collect</asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="TIPO">
                                                                                    <ItemTemplate>
                                                                                        <asp:RadioButtonList ID="rbl_cargos_house_loc_int5" runat="server" 
                                                                                            RepeatDirection="Horizontal">
                                                                                            <asp:ListItem Value="1">Loc</asp:ListItem>
                                                                                            <asp:ListItem Value="2">Int</asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="CONTA" Visible="False">
                                                                                    <ItemTemplate>
                                                                                        <asp:DropDownList ID="drp_cargos_house_conta5" runat="server" 
                                                                                            Font-Size="XX-Small" Width="60px">
                                                                                            <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                                                                                            <asp:ListItem Value="1">Fiscal</asp:ListItem>
                                                                                            <asp:ListItem Value="2">Financiera</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="C.E." Visible="False">
                                                                                    <ItemTemplate>
                                                                                        <asp:RadioButtonList ID="rbl_cargos_house_conocimiento5" runat="server" 
                                                                                            Height="25px" RepeatDirection="Horizontal">
                                                                                            <asp:ListItem Value="True">Si</asp:ListItem>
                                                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                                                        </asp:RadioButtonList>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="NOTAS">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="tb_cargos_house_observaciones5" runat="server" 
                                                                                            Font-Size="XX-Small" Height="12px" Width="90px"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="ALL IN">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="tb_cargos_house_allin5" runat="server" Font-Size="XX-Small" 
                                                                                            Height="12px" Width="80px"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/img/icons/delete.png" 
                                                                                    ShowDeleteButton="True" Visible="False" />
                                                                                <asp:ButtonField ButtonType="Image" CommandName="Editar" 
                                                                                    ImageUrl="~/img/icons/edit.png" Text="Edit" Visible="False" />
                                                                                <asp:ButtonField ButtonType="Image" CommandName="Actualizar" 
                                                                                    ImageUrl="~/img/icons/save.png" Text="Update" Visible="False" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2">
                                                                        <asp:Button ID="btn_siguiente35_pregunta3" runat="server" 
                                                                            onclick="btn_siguiente35_pregunta3_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_routing_collect_tipo_factura3" runat="server" 
                                                            Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        <strong>Definir Tipo de Factura - Carga RO:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <em><strong>Marcar todas como:</strong></em></td>
                                                                    <td align="center" width="500PX">
                                                                        <asp:RadioButtonList ID="rbl_tipo_factura3" runat="server" AutoPostBack="True" 
                                                                            Font-Size="XX-Small" 
                                                                            onselectedindexchanged="rbl_tipo_factura3_SelectedIndexChanged" 
                                                                            RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="2">FACTURA CONSUMIDOR FINAL</asp:ListItem>
                                                                            <asp:ListItem Value="3">FACTURA CREDITO FISCAL</asp:ListItem>
                                                                            <asp:ListItem Value="4">FACTURA EXPORTACION</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2">
                                                                        <asp:GridView ID="gv_transacciones_tipo_factura3" runat="server" 
                                                                            Font-Size="X-Small" onrowcreated="gv_transacciones_tipo_factura3_RowCreated">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="FACTURA"><ItemTemplate><asp:DropDownList ID="drp_tipo_factura3" runat="server" Font-Size="XX-Small" 
                                                                                            Width="150px"><asp:ListItem Value="1">Seleccione...</asp:ListItem><asp:ListItem Value="2">FACTURA CONSUMIDOR FINAL</asp:ListItem><asp:ListItem Value="3">FACTURA CREDITO FISCAL</asp:ListItem><asp:ListItem Value="4">FACTURA EXPORTACION</asp:ListItem></asp:DropDownList></ItemTemplate></asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" colspan="2">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2" height="40" valign="middle">
                                                                        &nbsp;<asp:Button ID="btn_regresar41" runat="server" onclick="btn_regresar41_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;
                                                                        <asp:Button ID="btn_siguiente17" runat="server" onclick="btn_siguiente17_Click" 
                                                                            Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" colspan="2" height="40" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_routing_order3_0" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_29" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Existen Cargos Adicionales</strong> por Transporte Local?</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl_25" runat="server" 
                                                                            RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="True">Si</asp:ListItem>
                                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente57_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente57_pregunta2_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_routing_order3" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <asp:Label ID="lbl_fechahora_30" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <strong>Cargos Adicionales</strong> por Transporte Local:</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style14" valign="middle">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style6">
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Codigo</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_codigo_transportista2" runat="server" Height="16px" 
                                                                                        ReadOnly="True" Width="50px">0</asp:TextBox>
                                                                                    <cc1:ModalPopupExtender ID="modalproveedor2" 
                                                                                        runat="server" BackgroundCssClass="FondoAplicacion" 
                                                                                        CancelControlID="btn_proveedor_cancelar2" DropShadow="True" 
                                                                                        OnCancelScript="mpeClienteOnCancel()" PopupControlID="pnlProveedor2" 
                                                                                        TargetControlID="tb_codigo_transportista2" />
                                                                                    </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="150px">
                                                                                    Transportista</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_nombre_transportista2" runat="server" Height="16px" 
                                                                                        ReadOnly="True" Width="300px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Routing Terrestre</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_routing_terrestre" runat="server" Height="16px" 
                                                                                        Width="200px"></asp:TextBox>
                                                                                        <cc1:ModalPopupExtender ID="modalrouting1" 
                                                                                        runat="server" BackgroundCssClass="FondoAplicacion" 
                                                                                        CancelControlID="btn_routing_cancelar" DropShadow="True" 
                                                                                        OnCancelScript="mpeClienteOnCancel()" PopupControlID="pnlRouting1" 
                                                                                        TargetControlID="tb_routing_terrestre" />
                                                                                    <asp:TextBox ID="tb_routing_terrestre_ID" runat="server" Height="16px" 
                                                                                        Width="40px" Visible="False">0</asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Servicio</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p2_servicio7" runat="server" AutoPostBack="True" 
                                                                                        onselectedindexchanged="drp_p2_servicio7_SelectedIndexChanged">
                                                                                        <asp:ListItem Value="5">TRANSPORTE LOCAL</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Rubro</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p2_rubro7" runat="server" Height="17px">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Moneda&nbsp;</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p2_moneda8" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Impuestos</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_afecto_excento13" runat="server">
                                                                                        <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                                                                                        <asp:ListItem Value="1">Excento</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Afecto</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Monto</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_p2_monto_recargo7" runat="server" Height="16px" 
                                                                                        Width="100px">0.00</asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="tb_p2_monto_recargo7_FilteredTextBoxExtender" 
                                                                                        runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                                        TargetControlID="tb_p2_monto_recargo7" ValidChars="."></cc1:FilteredTextBoxExtender>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator19" 
                                                                                        runat="server" ControlToValidate="tb_p2_monto_recargo7" 
                                                                                        ErrorMessage="Error ###.##" SetFocusOnError="True" 
                                                                                        ValidationExpression="\d+.\d{2}">
                                                                                    </asp:RegularExpressionValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Observaciones</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_observaciones11" runat="server" Height="16px" 
                                                                                        MaxLength="99" Width="300px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left" colspan="2">
                                                                                    <strong>Datos Factura del Proveedor</strong></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <table align="center" cellpadding="0" cellspacing="0" class="style9">
                                                                                        <tr>
                                                                                            <td>
                                                                                                Serie</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="tb_serie_proveedor15" runat="server" Height="16px" 
                                                                                                    Width="75px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                Correlativo</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="tb_correlativo_proveedor15" runat="server" Height="16px" 
                                                                                                    Width="75px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                Fecha</td>
                                                                                            <td>
                                                                                                <asp:TextBox ID="tb_fecha_proveedor15" runat="server" Height="16px" 
                                                                                                    Width="128px"></asp:TextBox>
                                                                                                <cc1:MaskedEditExtender ID="tb_fecha_proveedor15_MaskedEditExtender" 
                                                                                                    runat="server" Enabled="True" Mask="99/99/9999" MaskType="Date" 
                                                                                                    TargetControlID="tb_fecha_proveedor15"></cc1:MaskedEditExtender>
                                                                                                <cc1:CalendarExtender ID="tb_fecha_proveedor15_CalendarExtender" runat="server" 
                                                                                                    Enabled="True" Format="MM/dd/yyyy" TargetControlID="tb_fecha_proveedor15"></cc1:CalendarExtender>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2" style="margin-left: 40px">
                                                                                    <asp:Button ID="btn_p1_agregar_recargo8" runat="server" Text="Agregar" 
                                                                                        onclick="btn_p1_agregar_recargo8_Click" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:GridView ID="gv_routing_transporte_local" runat="server" 
                                                                                        Font-Size="X-Small" onrowcreated="gv_routing_transporte_local_RowCreated" 
                                                                                        onrowdeleting="gv_routing_transporte_local_RowDeleting">
                                                                                        <Columns>
                                                                                            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style6" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente46_pregunta2" runat="server" Text="Siguiente" 
                                                                            onclick="btn_siguiente46_pregunta2_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_routing_order4_0" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_35" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Existen Cargos Adicionales</strong> de Aduanas?</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl_27" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="True">Si</asp:ListItem>
                                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente58_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente58_pregunta2_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_routing_order4" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <asp:Label ID="lbl_fechahora_34" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <strong>Cargos Adicionales</strong> por Aduanas:</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style14" valign="middle">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style6">
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Routing Aduanas</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_routing_aduanas1" runat="server" Height="16px" 
                                                                                        Width="200px" ReadOnly="True"></asp:TextBox>
                                                                                    <cc1:ModalPopupExtender ID="modalrouting2" 
                                                                                        runat="server" BackgroundCssClass="FondoAplicacion" 
                                                                                        CancelControlID="btn_routing_cancelar" DropShadow="True" 
                                                                                        OnCancelScript="mpeClienteOnCancel()" PopupControlID="pnlRouting2" 
                                                                                        TargetControlID="tb_routing_aduanas1" />
                                                                                    <asp:TextBox ID="tb_routing_aduanas_ID1" runat="server" Height="16px" 
                                                                                        Visible="False" Width="40px">0</asp:TextBox>
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
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:GridView ID="gv_routing_aduanas" runat="server" Font-Size="X-Small" 
                                                                                        onrowcreated="gv_routing_aduanas_RowCreated" 
                                                                                        onrowdeleting="gv_routing_aduanas_RowDeleting" 
                                                                                        EmptyDataText="No Existen Cargos de Aduanas disponibles">
                                                                                        <Columns>
                                                                                            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" Visible="False" />
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style6" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente47_pregunta2" runat="server" Text="Siguiente" 
                                                                            onclick="btn_siguiente47_pregunta2_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_routing_order5_0" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_31" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Existen Cargos Adicionales</strong> por Transporte a Otro Pais?</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl_26" runat="server" 
                                                                            RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="True">Si</asp:ListItem>
                                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente57_pregunta3" runat="server" 
                                                                            onclick="btn_siguiente57_pregunta3_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_routing_order5" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <asp:Label ID="lbl_fechahora_32" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        Cargo por Transporte a otro Pais, Carga RO</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style14" valign="middle">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style6">
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_codigo_transportista3" runat="server" Height="16px" 
                                                                                        ReadOnly="True" Visible="False" Width="50px">20</asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td width="150px" colspan="2">
                                                                                    <asp:TextBox ID="tb_nombre_transportista3" runat="server" Height="16px" 
                                                                                        ReadOnly="True" Visible="False" Width="300px">MAYAN LOGISTICS NICARAGUA</asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_routing_terrestre2" runat="server" Height="16px" 
                                                                                        Width="200px" Visible="False"></asp:TextBox>
                                                                                    <cc1:ModalPopupExtender ID="modalrouting4" 
                                                                                        runat="server" BackgroundCssClass="FondoAplicacion" 
                                                                                        CancelControlID="btn_routing_cancelar" DropShadow="True" 
                                                                                        OnCancelScript="mpeClienteOnCancel()" PopupControlID="pnlRouting4" 
                                                                                        TargetControlID="tb_routing_terrestre2" />
                                                                                    <asp:TextBox ID="tb_routing_terrestre_ID2" runat="server" Height="16px" 
                                                                                        Width="40px" Visible="False">0</asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Servicio</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p2_servicio9" runat="server" Enabled="False">
                                                                                        <asp:ListItem Value="15">INTERMODAL</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Rubro</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p2_rubro9" runat="server" Height="17px" 
                                                                                        Enabled="False">
                                                                                        <asp:ListItem Value="62">FLETE TERRESTRE</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Moneda&nbsp;</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_p2_moneda10" runat="server" Enabled="False">
                                                                                        <asp:ListItem Value="8">USD</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:Label ID="lbl_campo5" runat="server"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:TextBox ID="tb_tarifa_intermodal1" runat="server" Height="16px" 
                                                                                        Width="100px">0.00</asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="tb_tarifa_intermodal1_FilteredTextBoxExtender" 
                                                                                        runat="server" Enabled="True" FilterType="Numbers,Custom" 
                                                                                        TargetControlID="tb_tarifa_intermodal1" ValidChars="."></cc1:FilteredTextBoxExtender>
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
                                                                    <td align="center" class="style6" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente48_pregunta2" runat="server" Text="Siguiente" 
                                                                            onclick="btn_siguiente48_pregunta2_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_routing_order6" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_26" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Transaccion generada por Coloading Rate:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:GridView ID="gv_transacciones_routing" runat="server" Font-Size="X-Small" 
                                                                            onrowcreated="gv_transacciones_routing_RowCreated">
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar11" runat="server" onclick="btn_regresar11_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente49_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente49_pregunta2_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_routing_order7" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_28" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Transacciones generadas por Cargos del HBL RUTEADO:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:GridView ID="gv_transacciones_routing2" runat="server" Font-Size="X-Small" onrowcreated="gv_transacciones_routing2_RowCreated">
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar12" runat="server" onclick="btn_regresar12_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente50_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente50_pregunta2_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_routing_order8" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Transacciones&nbsp; generadas por Transporte Local:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_33" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:GridView ID="gv_transacciones_routing3" runat="server" Font-Size="X-Small" 
                                                                            onrowcreated="gv_transacciones_routing3_RowCreated">
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle" style="margin-left: 40px">
                                                                        <asp:Button ID="btn_regresar13" runat="server" onclick="btn_regresar13_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente51_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente51_pregunta2_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_routing_order9" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_36" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Transacciones del RO de Aduanas generadas:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:GridView ID="gv_transacciones_routing4" runat="server" Font-Size="X-Small" 
                                                                            onrowcreated="gv_transacciones_routing4_RowCreated">
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar14" runat="server" onclick="btn_regresar14_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente52_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente52_pregunta2_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_routing_order10_0" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_37" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Existen Cargos Adicionales</strong> de Seguros?</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style4" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl_28" runat="server" RepeatDirection="Horizontal">
                                                                            <asp:ListItem Value="True">Si</asp:ListItem>
                                                                            <asp:ListItem Value="False">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente59_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente59_pregunta2_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_routing_order10" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <asp:Label ID="lbl_fechahora_38" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left" class="style6">
                                                                        <strong>Cargos Adicionales</strong> por Seguros:</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style14" valign="middle">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style6">
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    Routing de Seguros</td>
                                                                                <td>
                                                                                    <asp:TextBox ID="tb_routing_seguros1" runat="server" Height="16px" 
                                                                                        ReadOnly="True" Width="200px"></asp:TextBox>
                                                                                    <cc1:ModalPopupExtender ID="modalrouting3" 
                                                                                        runat="server" BackgroundCssClass="FondoAplicacion" 
                                                                                        CancelControlID="btn_routing_cancelar3" DropShadow="True" 
                                                                                        OnCancelScript="mpeClienteOnCancel()" PopupControlID="pnlRouting3" 
                                                                                        TargetControlID="tb_routing_seguros1" />
                                                                                    <asp:TextBox ID="tb_routing_seguros_ID1" runat="server" Height="16px" 
                                                                                        Visible="False" Width="40px">0</asp:TextBox>
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
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <asp:GridView ID="gv_routing_seguros" runat="server" Font-Size="X-Small" 
                                                                                        onrowcreated="gv_routing_seguros_RowCreated" 
                                                                                        onrowdeleting="gv_routing_seguros_RowDeleting" 
                                                                                        EmptyDataText="No Existen Cargos de Seguros Disponibles">
                                                                                        <Columns>
                                                                                            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" Visible="False" />
                                                                                        </Columns>
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" class="style6" height="40" valign="middle">
                                                                        <asp:Button ID="btn_siguiente53_pregunta2" runat="server" Text="Siguiente" 
                                                                            onclick="btn_siguiente53_pregunta2_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_routing_order11" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_39" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Transacciones del RO de Seguros generadas:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:GridView ID="gv_transacciones_routing5" runat="server" Font-Size="X-Small" 
                                                                            onrowcreated="gv_transacciones_routing5_RowCreated">
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar15" runat="server" onclick="btn_regresar15_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente54_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente54_pregunta2_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_routing_order12" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_40" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <strong>Transacciones generadas por Transporte hacia otro Pais Carga RO:</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <asp:GridView ID="gv_transacciones_routing6" runat="server" Font-Size="X-Small" 
                                                                            onrowcreated="gv_transacciones_routing6_RowCreated">
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        <asp:Button ID="btn_regresar16" runat="server" onclick="btn_regresar16_Click" 
                                                                            Text="Regresar" />
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente55_pregunta2" runat="server" 
                                                                            onclick="btn_siguiente55_pregunta2_Click" Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </asp:View>
                                                    <asp:View ID="View6" runat="server">
                                                        <asp:Panel ID="pnl_contabilizar1" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="lbl_fechahora_67" runat="server" Visible="False"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center">
                                                                        <br />
                                                                        <strong>El Cuestionario ha sido Completado!! Desea generar la Contabilizacion?</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        <asp:RadioButtonList ID="rbl_32" runat="server" RepeatDirection="Horizontal" 
                                                                            AutoPostBack="True" onselectedindexchanged="rbl_32_SelectedIndexChanged">
                                                                            <asp:ListItem Value="1">Ahora</asp:ListItem>
                                                                            <asp:ListItem Value="2">Luego</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        <asp:Panel ID="pnl_validacion_soas" runat="server" Visible="False">
                                                                            <table align="center" cellpadding="0" cellspacing="0" class="style1">
                                                                                <tr>
                                                                                    <td align="left" colspan="4" 
                                                                                        style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000">
                                                                                        <strong>VALIDACION DE SOA&#39;S</strong></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        &nbsp;</td>
                                                                                    <td>
                                                                                        &nbsp;</td>
                                                                                    <td>
                                                                                        &nbsp;</td>
                                                                                    <td>
                                                                                        &nbsp;</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center" width="200px">
                                                                                        <strong><em>AGENTE</em></strong></td>
                                                                                    <td width="50px">
                                                                                        &nbsp;</td>
                                                                                    <td align="center" width="100px">
                                                                                        <strong><em>SOA</em></strong></td>
                                                                                    <td align="center" width="100px">
                                                                                        <strong><em>SCA</em></strong></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4">
                                                                                        <asp:Panel ID="pnl_agente_cobrar_local2" runat="server" Visible="False">
                                                                                            <table align="center" cellpadding="0" cellspacing="0" class="style5">
                                                                                                <tr>
                                                                                                    <td align="center" valign="middle" width="50px">
                                                                                                        &nbsp;</td>
                                                                                                    <td align="right" width="200px">
                                                                                                        <asp:Label ID="lbl_campo_requerido1" runat="server" Font-Bold="True" Text="(*)"></asp:Label>
                                                                                                        Cobro en Moneda Local&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                                                                                    <td align="center" width="100px">
                                                                                                        <asp:TextBox ID="tb_agente_cobrar_local2" runat="server" Height="16px" style="text-align:right"
                                                                                                            Width="80px" ReadOnly="True">0.00</asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="center" width="100px">
                                                                                                        <asp:TextBox ID="tb_agente_cobrar_local3" runat="server" Height="16px" style="text-align:right"
                                                                                                            Width="80px" ReadOnly="True">0.00</asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4">
                                                                                        <asp:Panel ID="pnl_agente_pagar_local2" runat="server" Visible="False">
                                                                                            <table align="center" cellpadding="0" cellspacing="0" class="style5">
                                                                                                <tr>
                                                                                                    <td align="center" width="50px">
                                                                                                        &nbsp;</td>
                                                                                                    <td align="right" width="200px">
                                                                                                        <asp:Label ID="lbl_campo_requerido2" runat="server" Font-Bold="True" Text="(*)"></asp:Label>
                                                                                                        Pago en Moneda Local&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                                                                                    <td align="center" width="100px">
                                                                                                        <asp:TextBox ID="tb_agente_pagar_local2" runat="server" Height="16px" style="text-align:right"
                                                                                                            Width="80px" ReadOnly="True">0.00</asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="center" width="100px">
                                                                                                        <asp:TextBox ID="tb_agente_pagar_local3" runat="server" Height="16px" style="text-align:right"
                                                                                                            Width="80px" ReadOnly="True">0.00</asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4">
                                                                                        <asp:Panel ID="pnl_agente_cobrar_usd2" runat="server" Visible="False">
                                                                                            <table align="center" cellpadding="0" cellspacing="0" class="style5">
                                                                                                <tr>
                                                                                                    <td align="center" width="50px">
                                                                                                        &nbsp;</td>
                                                                                                    <td align="right" width="200px">
                                                                                                        <asp:Label ID="lbl_campo_requerido3" runat="server" Font-Bold="True" Text="(*)"></asp:Label>
                                                                                                        Cobro en USD&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                                                                                    <td align="center" width="100px">
                                                                                                        <asp:TextBox ID="tb_agente_cobrar_usd2" runat="server" Height="16px" style="text-align:right"
                                                                                                            Width="80px" ReadOnly="True">0.00</asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="center" 
                                                                                                        width="100px">
                                                                                                        <asp:TextBox ID="tb_agente_cobrar_usd3" runat="server" Height="16px" style="text-align:right"
                                                                                                            Width="80px" ReadOnly="True">0.00</asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4">
                                                                                        <asp:Panel ID="pnl_agente_pagar_usd2" runat="server" Visible="False">
                                                                                            <table align="center" cellpadding="0" cellspacing="0" class="style5">
                                                                                                <tr>
                                                                                                    <td align="center" width="50px">
                                                                                                        &nbsp;</td>
                                                                                                    <td align="right" width="200px">
                                                                                                        <asp:Label ID="lbl_campo_requerido4" runat="server" Font-Bold="True" Text="(*)"></asp:Label>
                                                                                                        Pago en USD&nbsp;&nbsp;&nbsp;&nbsp; </td>
                                                                                                    <td align="center" 
                                                                                                        width="100px">
                                                                                                        <asp:TextBox ID="tb_agente_pagar_usd2" runat="server" Height="16px" style="text-align:right"
                                                                                                            Width="80px" ReadOnly="True">0.00</asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="center" width="100px">
                                                                                                        <asp:TextBox ID="tb_agente_pagar_usd3" runat="server" Height="16px" style="text-align:right"
                                                                                                            Width="80px" ReadOnly="True">0.00</asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="center">
                                                                                        <strong><em>NAVIERA</em></strong></td>
                                                                                    <td>
                                                                                        &nbsp;</td>
                                                                                    <td align="center">
                                                                                        <strong><em>SOA</em></strong></td>
                                                                                    <td align="center">
                                                                                        <strong><em>SCA</em></strong></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4">
                                                                                        <asp:Panel ID="pnl_naviera_cobrar_local2" runat="server" Visible="False">
                                                                                            <table align="center" cellpadding="0" cellspacing="0" class="style5">
                                                                                                <tr>
                                                                                                    <td align="center" width="50px">
                                                                                                        &nbsp;</td>
                                                                                                    <td align="right" width="200px">
                                                                                                        <asp:Label ID="lbl_campo_requerido5" runat="server" Font-Bold="True" Text="(*)"></asp:Label>
                                                                                                        Cobro en Moneda Local&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                    </td>
                                                                                                    <td align="center" 
                                                                                                        width="100px">
                                                                                                        <asp:TextBox ID="tb_naviera_cobrar_local2" runat="server" Height="16px" style="text-align:right"
                                                                                                            Width="80px" ReadOnly="True">0.00</asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="center" 
                                                                                                        width="100px">
                                                                                                        <asp:TextBox ID="tb_naviera_cobrar_local3" runat="server" Height="16px" style="text-align:right"
                                                                                                            Width="80px" ReadOnly="True">0.00</asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4">
                                                                                        <asp:Panel ID="pnl_naviera_pagar_local2" runat="server" Visible="False">
                                                                                            <table align="center" cellpadding="0" cellspacing="0" class="style5">
                                                                                                <tr>
                                                                                                    <td align="center" width="50px">
                                                                                                        &nbsp;</td>
                                                                                                    <td align="right" width="200px">
                                                                                                        <asp:Label ID="lbl_campo_requerido6" runat="server" Font-Bold="True" Text="(*)"></asp:Label>
                                                                                                        Pago en Moneda Local&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                    </td>
                                                                                                    <td align="center" 
                                                                                                        width="100px">
                                                                                                        <asp:TextBox ID="tb_naviera_pagar_local2" runat="server" Height="16px" style="text-align:right"
                                                                                                            Width="80px" ReadOnly="True">0.00</asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="center" 
                                                                                                        width="100px">
                                                                                                        <asp:TextBox ID="tb_naviera_pagar_local3" runat="server" Height="16px" style="text-align:right"
                                                                                                            Width="80px" ReadOnly="True">0.00</asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4">
                                                                                        <asp:Panel ID="pnl_naviera_cobrar_usd2" runat="server" Visible="False">
                                                                                            <table align="center" cellpadding="0" cellspacing="0" class="style5">
                                                                                                <tr>
                                                                                                    <td align="center" width="50px">
                                                                                                        &nbsp;</td>
                                                                                                    <td align="right" width="200px">
                                                                                                        <asp:Label ID="lbl_campo_requerido7" runat="server" Font-Bold="True" Text="(*)"></asp:Label>
                                                                                                        Cobro en USD&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                    </td>
                                                                                                    <td align="center" 
                                                                                                        width="100px">
                                                                                                        <asp:TextBox ID="tb_naviera_cobrar_usd2" runat="server" Height="16px" style="text-align:right"
                                                                                                            Width="80px" ReadOnly="True">0.00</asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="center" 
                                                                                                        width="100px">
                                                                                                        <asp:TextBox ID="tb_naviera_cobrar_usd3" runat="server" Height="16px" style="text-align:right"
                                                                                                            Width="80px" ReadOnly="True">0.00</asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4">
                                                                                        <asp:Panel ID="pnl_totales_naviera2" runat="server">
                                                                                            <table align="center" cellpadding="0" cellspacing="0" class="style5">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Panel ID="pnl_naviera_pagar_usd2" runat="server" Visible="False">
                                                                                                            <table align="center" cellpadding="0" cellspacing="0" class="style5">
                                                                                                                <tr>
                                                                                                                    <td align="center" width="50px">
                                                                                                                        &nbsp;</td>
                                                                                                                    <td align="right" width="200px">
                                                                                                                        <asp:Label ID="lbl_campo_requerido8" runat="server" Font-Bold="True" Text="(*)"></asp:Label>
                                                                                                                        Pago en USD&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                                    </td>
                                                                                                                    <td align="center" 
                                                                                                                        width="100px">
                                                                                                                        <asp:TextBox ID="tb_naviera_pagar_usd2" runat="server" Height="16px" style="text-align:right"
                                                                                                                            Width="80px" ReadOnly="True">0.00</asp:TextBox>
                                                                                                                    </td>
                                                                                                                    <td align="center" 
                                                                                                                        width="100px">
                                                                                                                        <asp:TextBox ID="tb_naviera_pagar_usd3" runat="server" Height="16px" style="text-align:right"
                                                                                                                            Width="80px" ReadOnly="True">0.00</asp:TextBox>
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </table>
                                                                                                        </asp:Panel>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </asp:Panel>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </asp:Panel>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        <asp:Label ID="lbl_mensaje" runat="server" Font-Bold="True"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        &nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente7" runat="server" onclick="btn_siguiente7_Click" 
                                                                            Text="Siguiente" Visible="False" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_contabilizar2" runat="server" Visible="False">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                                <tr>
                                                                    <td align="left">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="left">
                                                                        <br />
                                                                        <strong>Por Favor seleccione el Departamento donde desea generar la 
                                                                        Contabilizacion?</strong></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        <table align="center" cellpadding="0" cellspacing="0" class="style22">
                                                                            <tr>
                                                                                <td>
                                                                                    Departamento</td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="drp_departamentos1" runat="server">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" height="40" valign="middle">
                                                                        &nbsp;&nbsp;&nbsp;
                                                                        <asp:Button ID="btn_siguiente33" runat="server" onclick="btn_siguiente33_Click" 
                                                                            Text="Siguiente" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnl_contabilizar3" runat="server" Visible="False">
                                                            
                                                        </asp:Panel>
                                                    </asp:View>
                                                    <asp:View ID="View7" runat="server">
                                                        <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                                            <tr>
                                                                <td align="left">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <br />
                                                                    <strong>EXISTEN CAMBIOS EN LA INFORMACION DEL EMBARQUE</strong></td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" valign="middle">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="middle">
                                                                    El SCA ha detectado que se realizaron cambios en los datos del Embarque, a 
                                                                    continuacion se presenta el detalle:</td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" valign="middle">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" valign="middle">
                                                                    <asp:Label ID="lbl_cambios1" runat="server" Font-Bold="True"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" valign="middle">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" valign="middle">
                                                                    <ul>
                                                                        <li>Los cambios realizados en el Master o los Houses desde Trafico, pueden afectar 
                                                                            en el monto de las Transacciones ya generadas. </li>
                                                                        <li>Las Tarifas, los Ingresos o Costos pueden haber variado, es necesario <strong>
                                                                            REINICIAR EL EMBARQUE</strong> para contabilizar los valores correctos.</li>
                                                                    </ul>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" valign="middle">
                                                                    &nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" height="40" valign="middle">
                                                                    &nbsp;&nbsp;&nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:View>
                                                </asp:MultiView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                    <asp:Label ID="lbl_error" runat="server" Font-Bold="True" ForeColor="Red" 
                                        Visible="False"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="center" valign="top" class="style23">
                                    <table align="center" bgcolor="#E5E5E5" cellpadding="0" cellspacing="0" 
                                        class="style5" border="1px">
                                        <tr>
                                            <td>
                                    <table align="center" cellpadding="0" cellspacing="0" class="style2" 
                                        bgcolor="#E5E5E5">                                        
                                        <tr>
                                            <td class="style4" align="center">
                                                <strong>Cuestionario</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btn_reiniciar" runat="server" BackColor="#E5E5E5" 
                                                    BorderStyle="None" Font-Italic="True" ForeColor="Black" 
                                                    onclick="btn_reiniciar_Click" Text="Reiniciar" Font-Bold="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btn_finalizar" runat="server" BackColor="#E5E5E5" 
                                                    BorderStyle="None" Font-Italic="True" ForeColor="Black" Text="Finalizar" 
                                                    Font-Bold="True" PostBackUrl="~/Home.aspx" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btn_salir" runat="server" BackColor="#E5E5E5" 
                                                    BorderStyle="None" Font-Italic="True" ForeColor="#000099" Text="Salir" 
                                                    Font-Bold="True" PostBackUrl="~/Home.aspx" />
                                            </td>
                                        </tr>
                                    </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center" valign="top" class="style24">
                                    <table align="center" cellpadding="0" cellspacing="0" class="style2" 
                                        bgcolor="White" style="border: 1px solid #000000; margin-top:15px; margin-bottom:15px">
                                        <tr>
                                            <td align="center" class="style3" bgcolor="Red" 
                                                style="background-color: #000000; color: #FFFFFF; font-weight: bold" 
                                                height="35px">
                                                <strong>ESTADO DE CUENTA DE TODA LA OPERACION DEL EMBARQUE</strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Panel ID="pnl_estado_cuenta" runat="server" Height="600px" 
                                                    HorizontalAlign="Center" ScrollBars="Vertical">
                                                    <br />
                                                    <div id="dv_estado_cuenta" runat="server">
                                                    </div>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <table align="center" cellpadding="0" cellspacing="0" class="style9">
                                                    <tr>
                                                        <td align="center" valign="top">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="top">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style35" 
                                                                style="border: 1px solid #000000; display:none">
                                                                <tr>
                                                                    <td align="center" bgcolor="Gray" rowspan="2" 
                                                                        style="font-weight: bold; color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                                                        valign="middle">
                                                                        NO</td>
                                                                    <td align="center" bgcolor="#E6E6E6" colspan="5" 
                                                                        
                                                                        style="font-weight: bold; color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-bottom-style: solid; border-bottom-color: #000000; border-bottom-width: 1px;">
                                                                        DETALLE DE TRANSACCIONES</td>
                                                                    <td align="center" bgcolor="#FF80FF" colspan="2" 
                                                                        
                                                                        style="font-weight: bold; color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-bottom-style: solid; border-bottom-color: #000000; border-bottom-width: 1px;">
                                                                        AGENTE</td>
                                                                    <td align="center" bgcolor="#0080FF" colspan="2" 
                                                                        
                                                                        style="font-weight: bold; color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-bottom-style: solid; border-bottom-color: #000000; border-bottom-width: 1px;">
                                                                        NAVIERA</td>
                                                                    <td align="center" bgcolor="#FFFF80" colspan="2" 
                                                                        
                                                                        style="font-weight: bold; color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-bottom-style: solid; border-bottom-color: #000000; border-bottom-width: 1px;">
                                                                        CLIENTE</td>
                                                                    <td align="center" bgcolor="#FF8000" colspan="2" 
                                                                        
                                                                        style="font-weight: bold; color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-bottom-style: solid; border-bottom-color: #000000; border-bottom-width: 1px;">
                                                                        PROVEEDORES</td>
                                                                    <td align="center" bgcolor="#6666FF" colspan="2" 
                                                                        
                                                                        style="font-weight: bold; color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-bottom-style: solid; border-bottom-color: #000000; border-bottom-width: 1px;">
                                                                        INTERCOMPANY<br />
                                                                        DESTINO</td>
                                                                    <td align="center" bgcolor="Lime" colspan="2" 
                                                                        style="font-weight: bold; color: #000000; border-bottom-style: solid; border-bottom-color: #000000; border-bottom-width: 1px;">
                                                                        INGRESOS &amp;<br />
                                                                        COSTOS</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" bgcolor="#E6E6E6" 
                                                                        
                                                                        style="font-weight: bold; color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-size: x-small;" 
                                                                        width="60px">
                                                                        TRANS</td>
                                                                    <td align="center" bgcolor="#E6E6E6" 
                                                                        
                                                                        style="font-weight: bold; color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-size: x-small;" 
                                                                        width="120px">
                                                                        CONCEPTO</td>
                                                                    <td align="center" bgcolor="#E6E6E6" 
                                                                        
                                                                        style="font-weight: bold; color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-size: x-small;" 
                                                                        width="160px">
                                                                        BL</td>
                                                                    <td align="center" bgcolor="#E6E6E6" 
                                                                        
                                                                        style="font-weight: bold; color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-size: x-small;" 
                                                                        width="120px">
                                                                        RUBRO</td>
                                                                    <td align="center" bgcolor="#E6E6E6" 
                                                                        
                                                                        style="font-weight: bold; color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-size: x-small;" 
                                                                        width="60px">
                                                                        MONEDA</td>
                                                                    <td align="center" bgcolor="#FF80FF" 
                                                                        
                                                                        style="font-weight: bold; color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-size: x-small; font-style: italic;" 
                                                                        width="60px">
                                                                        Cobrar</td>
                                                                    <td align="center" bgcolor="#FF80FF" 
                                                                        
                                                                        style="font-weight: bold; color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-size: x-small; font-style: italic;" 
                                                                        width="60px">
                                                                        Pagar</td>
                                                                    <td align="center" bgcolor="#0080FF" 
                                                                        
                                                                        style="font-weight: bold; color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-size: x-small; font-style: italic;" 
                                                                        width="60px">
                                                                        Cobrar</td>
                                                                    <td align="center" bgcolor="#0080FF" 
                                                                        
                                                                        style="font-weight: bold; color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-size: x-small; font-style: italic;" 
                                                                        width="60px">
                                                                        Pagar</td>
                                                                    <td align="center" bgcolor="#FFFF80" 
                                                                        
                                                                        style="font-weight: bold; color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-size: x-small; font-style: italic;" 
                                                                        width="60px">
                                                                        Cobrar</td>
                                                                    <td align="center" bgcolor="#FFFF80" 
                                                                        
                                                                        style="font-weight: bold; color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-size: x-small; font-style: italic;" 
                                                                        width="60px">
                                                                        Pagar</td>
                                                                    <td align="center" bgcolor="#FF8000" 
                                                                        
                                                                        style="font-weight: bold; color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-size: x-small; font-style: italic;" 
                                                                        width="60px">
                                                                        Cobrar</td>
                                                                    <td align="center" bgcolor="#FF8000" 
                                                                        
                                                                        style="font-weight: bold; color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-size: x-small; font-style: italic;" 
                                                                        width="60px">
                                                                        Pagar</td>
                                                                    <td align="center" bgcolor="#6666FF" 
                                                                        
                                                                        style="font-weight: bold; color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-size: x-small; font-style: italic;" 
                                                                        width="60px">
                                                                        Cobrar</td>
                                                                    <td align="center" bgcolor="#6666FF" 
                                                                        
                                                                        style="font-weight: bold; color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-size: x-small; font-style: italic;" 
                                                                        width="60px">
                                                                        Pagar</td>
                                                                    <td align="center" bgcolor="Lime" 
                                                                        
                                                                        style="font-weight: bold; color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-size: x-small; font-style: italic;" 
                                                                        width="60px">
                                                                        Ingresos</td>
                                                                    <td align="center" bgcolor="Lime" 
                                                                        
                                                                        style="font-weight: bold; color: #000000; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; font-size: x-small; font-style: italic;" 
                                                                        width="60px">
                                                                        Costos</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="center" bgcolor="Gray" 
                                                                        
                                                                        style="color: #000000; border-color: #000000; border-right-style: solid; font-weight: bold; font-size: x-small; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px;">
                                                                        1</td>
                                                                    <td align="left" bgcolor="#E6E6E6" 
                                                                        style="color: #000000; font-weight: bold; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; border-left-color: #000000; font-size: x-small;">
                                                                        CXP</td>
                                                                    <td align="center" bgcolor="#E6E6E6" 
                                                                        style="color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small;">
                                                                        COSTO DEL AGENTE</td>
                                                                    <td align="left" bgcolor="#E6E6E6" 
                                                                        style="color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small;">
                                                                        CCFNGB1613701</td>
                                                                    <td align="center" bgcolor="#E6E6E6" 
                                                                        style="color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small;">
                                                                        PROFIT SHARED</td>
                                                                    <td align="center" bgcolor="#E6E6E6" 
                                                                        style="color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small;">
                                                                        USD</td>
                                                                    <td align="right" bgcolor="#FF80FF" 
                                                                        style="color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small;">
                                                                        0.00</td>
                                                                    <td align="right" bgcolor="#FF80FF" 
                                                                        style="color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small;">
                                                                        250.00</td>
                                                                    <td align="right" bgcolor="#0080FF" 
                                                                        style="color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small;">
                                                                        12750.00</td>
                                                                    <td align="right" bgcolor="#0080FF" 
                                                                        style="color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small;">
                                                                        0.00</td>
                                                                    <td align="right" bgcolor="#FFFF80" 
                                                                        style="color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small;">
                                                                        8900.00</td>
                                                                    <td align="right" bgcolor="#FFFF80" 
                                                                        style="color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small;">
                                                                        0.00</td>
                                                                    <td align="right" bgcolor="#FF8000" 
                                                                        style="color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small;">
                                                                        0.00</td>
                                                                    <td align="right" bgcolor="#FF8000" 
                                                                        style="color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small;">
                                                                        0.00</td>
                                                                    <td align="right" bgcolor="#6666FF" 
                                                                        style="color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small;">
                                                                        0.00</td>
                                                                    <td align="right" bgcolor="#6666FF" 
                                                                        style="color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small;">
                                                                        0.00</td>
                                                                    <td align="right" bgcolor="Lime" 
                                                                        style="color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small;">
                                                                        15789.00</td>
                                                                    <td align="right" bgcolor="Lime" 
                                                                        style="color: #000000; font-size: x-small;">
                                                                        7689.00</td>
                                                                </tr>
                                                                <%= HTML_Transacciones %>
                                                                <tr>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        
                                                                        style="color: #FFFFFF; border-color: #000000; border-right-style: solid; font-weight: bold; font-size: small; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px; font-style: italic;" 
                                                                        colspan="5">
                                                                        TOTAL LOCAL.:</td>
                                                                    <td align="center" bgcolor="Black" 
                                                                        
                                                                        style="color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: #FF80FF; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: #FF80FF;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: #FF80FF; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: #FF80FF;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: #0080FF; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: #0080FF;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: #0080FF; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: #0080FF;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: #FFFF80; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: #FFFF80;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: #FFFF80; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: #FFFF80;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: #FF8000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: #FF8000;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: #FF8000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: #FF8000;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: #6666FF; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: #6666FF;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: #6666FF; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: #6666FF;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: Lime; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: Lime;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: Lime; font-size: x-small; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: Lime;">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        
                                                                        style="color: #FFFFFF; border-color: #000000; border-right-style: solid; font-weight: bold; font-size: small; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px; font-style: italic;" 
                                                                        colspan="5">
                                                                        TOTAL USD.:</td>
                                                                    <td align="center" bgcolor="Black" 
                                                                        
                                                                        style="color: #000000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: #FF80FF; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: #FF80FF;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: #FF80FF; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: #FF80FF;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: #0080FF; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: #0080FF;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: #0080FF; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: #0080FF;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: #FFFF80; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: #FFFF80;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: #FFFF80; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: #FFFF80;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: #FF8000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: #FF8000;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: #FF8000; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: #FF8000;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: #6666FF; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: #6666FF;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: #6666FF; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: #6666FF;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: Lime; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-size: x-small; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: Lime;">
                                                                        &nbsp;</td>
                                                                    <td align="right" bgcolor="Black" 
                                                                        
                                                                        style="color: Lime; font-size: x-small; border-right-style: solid; border-right-width: 1px; border-right-color: #000000; font-weight: bold; border-left-style: solid; border-left-width: 1px; border-left-color: Lime;">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" colspan="5">
                                                                        &nbsp;</td>
                                                                    <td align="center">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" colspan="5">
                                                                        &nbsp;</td>
                                                                    <td align="center">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="left" 
                                                                        style="color: #000000; font-weight: bold; font-style: italic;" colspan="5">
                                                                        PROFIT TRANSACCIONES LOCAL.:</td>
                                                                    <td align="right" 
                                                                        
                                                                        style="color: #000000; font-weight: bold; font-style: italic; padding-right: 10px;" 
                                                                        colspan="3">
                                                                        afsd</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" colspan="5">
                                                                        &nbsp;</td>
                                                                    <td align="center">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="left" 
                                                                        
                                                                        style="color: #000000; font-weight: bold; font-style: italic; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000;" 
                                                                        colspan="5">
                                                                        PROFIT TRANSACCIONES USD.:</td>
                                                                    <td align="right" 
                                                                        
                                                                        style="color: #000000; font-weight: bold; font-style: italic; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000; padding-right: 10px;" 
                                                                        colspan="3">
                                                                        asdf</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" colspan="5">
                                                                        &nbsp;</td>
                                                                    <td align="center">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right" colspan="5">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" colspan="5">
                                                                        &nbsp;</td>
                                                                    <td align="center">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="left" 
                                                                        
                                                                        style="color: #000000; font-size: medium; font-weight: bold; font-style: italic;" 
                                                                        colspan="5">
                                                                        PROFIT DEL EMBARQUE USD.:</td>
                                                                    <td align="right" 
                                                                        
                                                                        style="color: #000000; font-size: medium; font-weight: bold; font-style: italic; padding-right: 10px;" 
                                                                        colspan="3">
                                                                        asdf</td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" colspan="5">
                                                                        &nbsp;</td>
                                                                    <td align="center">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                    <td align="right">
                                                                        &nbsp;</td>
                                                                </tr>
                                                                </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="top">
                                                            </td>
                                                    </tr>
                                                    </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center" height="200px" valign="top">
                                    <table align="center" cellpadding="0" cellspacing="0" class="style2" 
                                        bgcolor="White" style="border: 1px solid #000000; margin-top:15px; margin-bottom:15px">
                                        <tr>
                                            <td align="center" class="style3" bgcolor="Red" 
                                                style="background-color: #000000; color: #FFFFFF; font-weight: bold" 
                                                height="35px">
                                                <strong>RESUMEN DE PREGUNTAS</strong></td>
                                        </tr>
                                        <tr>
                                            <td align="center" bgcolor="#3366FF">
                                                <table align="center" cellpadding="0" cellspacing="0" class="style9">
                                                    <tr>
                                                        <td align="center" valign="top">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="top">
                                                            <asp:Panel ID="pnl_cuestionario" runat="server" Height="300px" 
                                                                ScrollBars="Vertical" Width="800px" BackColor="White">
                                                                <br />
                                                                <br />
                                                                <asp:GridView ID="gv_resumen_preguntas" runat="server" Font-Size="XX-Small" onrowcreated="gv_resumen_preguntas_RowCreated">
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" valign="top">
                                                            &nbsp;</td>
                                                    </tr>
                                                    </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center" valign="top">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>

            <asp:Panel ID="pnlProveedor" runat="server" CssClass="CajaDialogo" style="display:none;" Width="800px" BackColor="#CCCCCC">
            <div align="center">
            <table>
                <tr><td align="center">Filtrar por</td></tr>
                <tr>
                    <td>Nit:<asp:TextBox ID="tb_nitb" runat="server" Height="16px" 
                            Width="131px" /></td>
                </tr>
                <tr>
                    <td>Nombre:<asp:TextBox ID="tb_nombreb" runat="server" Height="16px" 
                            Width="293px" /></td>
                </tr>
                <tr>
                    <td>Codigo:<asp:TextBox ID="tb_codigo" runat="server" Height="16px" 
                            Width="293px" /></td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="bt_buscar_proveedor" runat="server" Text="Buscar" 
                            onclick="bt_buscar_proveedor_Click" /></td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table></div>
            <div style="padding: 10px; background-color: #0033CC; color: #FFFFFF;" 
                align="center">
                <asp:Label ID="Label9" runat="server" Text="Seleccionar" />
            </div>
            <div align="center">
                <asp:GridView ID="gv_clientes" runat="server" AllowPaging="True" 
                    AutoGenerateSelectButton="True" 
                    onpageindexchanging="gv_clientes_PageIndexChanging" 
                    onselectedindexchanged="gv_clientes_SelectedIndexChanged" 
                    onpageindexchanged="gv_clientes_PageIndexChanged"
                    PageSize="5" onrowcreated="gv_clientes_RowCreated">
                </asp:GridView>
            </div>
            <div align="center">
                &nbsp;&nbsp;
                <br />
                <asp:Button ID="btnProveedorCancelar" runat="server" Text="Cancelar" />
                <br />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlProveedor2" runat="server" CssClass="CajaDialogo" style="display:none;" Width="800px" BackColor="#CCCCCC">
            <div align="center">
            <table>
                <tr><td align="center">Filtrar por</td></tr>
                <tr>
                    <td>Nit:<asp:TextBox ID="tb_proveedor_nit2" runat="server" Height="16px" 
                            Width="131px" /></td>
                </tr>
                <tr>
                    <td>Nombre:<asp:TextBox ID="tb_proveedor_nombre2" runat="server" Height="16px" 
                            Width="293px" /></td>
                </tr>
                <tr>
                    <td>Codigo:<asp:TextBox ID="tb_proveedor_codigo2" runat="server" Height="16px" 
                            Width="293px" /></td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btn_buscar_proveedor2" runat="server" Text="Buscar" 
                            onclick="btn_buscar_proveedor2_Click" /></td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table></div>
            <div style="padding: 10px; background-color: #0033CC; color: #FFFFFF;" 
                align="center">
                <asp:Label ID="Label1" runat="server" Text="Seleccionar" />
            </div>
            <div align="center">
                <asp:GridView ID="gv_proveedor2" runat="server" AllowPaging="True" 
                    AutoGenerateSelectButton="True" 
                    onpageindexchanging="gv_proveedor2_PageIndexChanging" 
                    onselectedindexchanged="gv_proveedor2_SelectedIndexChanged" 
                    onpageindexchanged="gv_proveedor2_PageIndexChanged"
                    PageSize="5" onrowcreated="gv_proveedor2_RowCreated">
                </asp:GridView>
            </div>
            <div align="center">
                &nbsp;&nbsp;
                <br />
                <asp:Button ID="btn_proveedor_cancelar2" runat="server" Text="Cancelar" />
                <br />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlRouting1" runat="server" CssClass="CajaDialogo" style="display:none;" Width="800px" BackColor="#CCCCCC">
            <div align="center">
            <table>
                <tr><td align="center">Buscar RO Terrestre</td></tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>No:&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="tb_criterio1" runat="server" Height="16px" 
                            Width="293px" /></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btn_buscar_routing1" runat="server" Text="Buscar" 
                            onclick="btn_buscar_routing1_Click" /></td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table></div>
            <div style="padding: 10px; background-color: #0033CC; color: #FFFFFF;" 
                align="center">
                <asp:Label ID="Label2" runat="server" Text="Seleccionar" />
            </div>
            <div align="center">
                <asp:GridView ID="gv_routings1" runat="server" AllowPaging="True" 
                    AutoGenerateSelectButton="True" 
                    onpageindexchanging="gv_routings1_PageIndexChanging" 
                    onselectedindexchanged="gv_routings1_SelectedIndexChanged" 
                    onpageindexchanged="gv_routings1_PageIndexChanged"
                    PageSize="5" Font-Size="X-Small" onrowcreated="gv_routings1_RowCreated">
                </asp:GridView>
            </div>
            <div align="center">
                &nbsp;&nbsp;
                <br />
                <asp:Button ID="btn_routing_cancelar" runat="server" Text="Cancelar" />
                <br />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlRouting2" runat="server" CssClass="CajaDialogo" style="display:none;" Width="800px" BackColor="#CCCCCC">
            <div align="center">
            <table>
                <tr><td align="center">Buscar RO de Aduanas</td></tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>No:&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="tb_criterio2" runat="server" Height="16px" 
                            Width="293px" /></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btn_buscar_routing2" runat="server" Text="Buscar" 
                            onclick="btn_buscar_routing2_Click" /></td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table></div>
            <div style="padding: 10px; background-color: #0033CC; color: #FFFFFF;" 
                align="center">
                <asp:Label ID="Label3" runat="server" Text="Seleccionar" />
            </div>
            <div align="center">
                <asp:GridView ID="gv_routings2" runat="server" AllowPaging="True" 
                    AutoGenerateSelectButton="True" 
                    onpageindexchanging="gv_routings2_PageIndexChanging" 
                    onselectedindexchanged="gv_routings2_SelectedIndexChanged" 
                    onpageindexchanged="gv_routings2_PageIndexChanged"
                    PageSize="5" Font-Size="X-Small" onrowcreated="gv_routings2_RowCreated">
                </asp:GridView>
            </div>
            <div align="center">
                &nbsp;&nbsp;
                <br />
                <asp:Button ID="btn_routing_cancelar2" runat="server" Text="Cancelar" />
                <br />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlRouting3" runat="server" CssClass="CajaDialogo" style="display:none;" Width="800px" BackColor="#CCCCCC">
            <div align="center">
            <table>
                <tr><td align="center">Buscar RO de Seguros</td></tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>No:&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="tb_criterio3" runat="server" Height="16px" 
                            Width="293px" /></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btn_buscar_routing3" runat="server" Text="Buscar" 
                            onclick="btn_buscar_routing3_Click" /></td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table></div>
            <div style="padding: 10px; background-color: #0033CC; color: #FFFFFF;" 
                align="center">
                <asp:Label ID="Label4" runat="server" Text="Seleccionar" />
            </div>
            <div align="center">
                <asp:GridView ID="gv_routings3" runat="server" AllowPaging="True" 
                    AutoGenerateSelectButton="True" 
                    onpageindexchanging="gv_routings3_PageIndexChanging" 
                    onselectedindexchanged="gv_routings3_SelectedIndexChanged" 
                    onpageindexchanged="gv_routings3_PageIndexChanged"
                    PageSize="5" Font-Size="X-Small" onrowcreated="gv_routings3_RowCreated">
                </asp:GridView>
            </div>
            <div align="center">
                &nbsp;&nbsp;
                <br />
                <asp:Button ID="btn_routing_cancelar3" runat="server" Text="Cancelar" />
                <br />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlRouting4" runat="server" CssClass="CajaDialogo" style="display:none;" Width="800px" BackColor="#CCCCCC">
            <div align="center">
            <table>
                <tr><td align="center">Buscar RO Terrestre4</td></tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>No:&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="tb_criterio4" runat="server" Height="16px" 
                            Width="293px" /></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btn_buscar_routing4" runat="server" Text="Buscar" 
                            onclick="btn_buscar_routing4_Click" /></td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table></div>
            <div style="padding: 10px; background-color: #0033CC; color: #FFFFFF;" 
                align="center">
                <asp:Label ID="Label5" runat="server" Text="Seleccionar" />
            </div>
            <div align="center">
                <asp:GridView ID="gv_routings4" runat="server" AllowPaging="True" 
                    AutoGenerateSelectButton="True" 
                    onpageindexchanging="gv_routings4_PageIndexChanging" 
                    onselectedindexchanged="gv_routings4_SelectedIndexChanged" 
                    onpageindexchanged="gv_routings4_PageIndexChanged"
                    PageSize="5" Font-Size="X-Small" onrowcreated="gv_routings4_RowCreated">
                </asp:GridView>
            </div>
            <div align="center">
                &nbsp;&nbsp;
                <br />
                <asp:Button ID="btn_routing_cancelar4" runat="server" Text="Cancelar" />
                <br />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlRouting5" runat="server" CssClass="CajaDialogo" style="display:none;" Width="800px" BackColor="#CCCCCC">
            <div align="center">
            <table>
                <tr><td align="center">Buscar RO Terrestre5</td></tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>No:&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="tb_criterio5" runat="server" Height="16px" 
                            Width="293px" /></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btn_buscar_routing5" runat="server" Text="Buscar" onclick="btn_buscar_routing5_Click" 
                             /></td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table></div>
            <div style="padding: 10px; background-color: #0033CC; color: #FFFFFF;" 
                align="center">
                <asp:Label ID="Label6" runat="server" Text="Seleccionar" />
            </div>
            <div align="center">
                <asp:GridView ID="gv_routings5" runat="server" AllowPaging="True" 
                    AutoGenerateSelectButton="True" 
                    onpageindexchanging="gv_routings5_PageIndexChanging" 
                    onselectedindexchanged="gv_routings5_SelectedIndexChanged" 
                    onpageindexchanged="gv_routings5_PageIndexChanged"
                    PageSize="5" Font-Size="X-Small" onrowcreated="gv_routings5_RowCreated">
                </asp:GridView>
            </div>
            <div align="center">
                &nbsp;&nbsp;
                <br />
                <asp:Button ID="Button2" runat="server" Text="Cancelar" />
                <br />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlRouting6" runat="server" CssClass="CajaDialogo" style="display:none;" Width="800px" BackColor="#CCCCCC">
            <div align="center">
            <table>
                <tr><td align="center">Buscar RO Terrestre6</td></tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>No:&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="tb_criterio6" runat="server" Height="16px" 
                            Width="293px" /></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btn_buscar_routing6" runat="server" Text="Buscar" onclick="btn_buscar_routing6_Click" 
                             /></td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table></div>
            <div style="padding: 10px; background-color: #0033CC; color: #FFFFFF;" 
                align="center">
                <asp:Label ID="Label7" runat="server" Text="Seleccionar" />
            </div>
            <div align="center">
                <asp:GridView ID="gv_routings6" runat="server" AllowPaging="True" 
                    AutoGenerateSelectButton="True" 
                    onpageindexchanging="gv_routings6_PageIndexChanging" 
                    onselectedindexchanged="gv_routings6_SelectedIndexChanged" 
                    onpageindexchanged="gv_routings6_PageIndexChanged"
                    PageSize="5" Font-Size="X-Small" onrowcreated="gv_routings6_RowCreated">
                </asp:GridView>
            </div>
            <div align="center">
                &nbsp;&nbsp;
                <br />
                <asp:Button ID="Button3" runat="server" Text="Cancelar" />
                <br />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlCliente1" runat="server" CssClass="CajaDialogo" style="display:none;" Width="800px" BackColor="#CCCCCC">
            <div align="center">
            <table>
                <tr><td align="center" colspan="2">Buscar Cliente</td></tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        Codigo</td>
                    <td>
                        <asp:TextBox ID="tb_criterio7" runat="server" Height="16px" Width="120px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Nombre</td>
                    <td>
                        <asp:TextBox ID="tb_criterio8" runat="server" Height="16px" Width="120px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btn_buscar_cliente1" runat="server" Text="Buscar" 
                            onclick="btn_buscar_cliente1_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                </tr>
            </table></div>
            <div style="padding: 10px; background-color: #0033CC; color: #FFFFFF;" 
                align="center">
                <asp:Label ID="Label8" runat="server" Text="Seleccionar" />
            </div>
            <div align="center">
                <asp:GridView ID="gv_clientes1" runat="server" AllowPaging="True" 
                    AutoGenerateSelectButton="True" 
                    onpageindexchanging="gv_clientes1_PageIndexChanging" 
                    onselectedindexchanged="gv_clientes1_SelectedIndexChanged" 
                    onpageindexchanged="gv_clientes1_PageIndexChanged"
                    PageSize="5" Font-Size="X-Small" onrowcreated="gv_clientes1_RowCreated">
                </asp:GridView>
            </div>
            <div align="center">
                &nbsp;&nbsp;
                <br />
                <asp:Button ID="btnClienteCancelar" runat="server" Text="Cancelar" />
                <br />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlCliente2" runat="server" CssClass="CajaDialogo" style="display:none;" Width="800px" BackColor="#CCCCCC">
            <div align="center">
            <table>
                <tr><td align="center" colspan="2">Buscar Cliente</td></tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        Codigo</td>
                    <td>
                        <asp:TextBox ID="tb_criterio9" runat="server" Height="16px" Width="120px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Nombre</td>
                    <td>
                        <asp:TextBox ID="tb_criterio10" runat="server" Height="16px" Width="120px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btn_buscar_cliente2" runat="server" Text="Buscar" 
                            onclick="btn_buscar_cliente2_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                </tr>
            </table></div>
            <div style="padding: 10px; background-color: #0033CC; color: #FFFFFF;" 
                align="center">
                <asp:Label ID="Label10" runat="server" Text="Seleccionar" />
            </div>
            <div align="center">
                <asp:GridView ID="gv_clientes2" runat="server" AllowPaging="True" 
                    AutoGenerateSelectButton="True" 
                    onpageindexchanging="gv_clientes2_PageIndexChanging" 
                    onselectedindexchanged="gv_clientes2_SelectedIndexChanged" 
                    onpageindexchanged="gv_clientes2_PageIndexChanged"
                    PageSize="5" Font-Size="X-Small" onrowcreated="gv_clientes2_RowCreated">
                </asp:GridView>
            </div>
            <div align="center">
                &nbsp;&nbsp;
                <br />
                <asp:Button ID="btnClienteCancelar2" runat="server" Text="Cancelar" />
                <br />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlCliente3" runat="server" CssClass="CajaDialogo" style="display:none;" Width="800px" BackColor="#CCCCCC">
            <div align="center">
            <table>
                <tr><td align="center" colspan="2">Asignar Cliente</td></tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        Codigo</td>
                    <td>
                        <asp:TextBox ID="tb_criterio11" runat="server" Height="16px" Width="120px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Nombre</td>
                    <td>
                        <asp:TextBox ID="tb_criterio12" runat="server" Height="16px" Width="120px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btn_buscar_cliente3" runat="server" Text="Buscar" 
                            onclick="btn_buscar_cliente3_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                </tr>
            </table></div>
            <div style="padding: 10px; background-color: #0033CC; color: #FFFFFF;" 
                align="center">
                <asp:Label ID="Label11" runat="server" Text="Seleccionar" />
            </div>
            <div align="center">
                <asp:GridView ID="gv_clientes3" runat="server" AllowPaging="True" 
                    AutoGenerateSelectButton="True" 
                    onpageindexchanging="gv_clientes3_PageIndexChanging" 
                    onselectedindexchanged="gv_clientes3_SelectedIndexChanged" 
                    onpageindexchanged="gv_clientes3_PageIndexChanged"
                    PageSize="5" Font-Size="X-Small" onrowcreated="gv_clientes3_RowCreated">
                </asp:GridView>
            </div>
            <div align="center">
                &nbsp;&nbsp;
                <br />
                <asp:Button ID="btnClienteCancelar3" runat="server" Text="Cancelar" />
                <br />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlProveedor4" runat="server" CssClass="CajaDialogo" style="display:none;" Width="800px" BackColor="#CCCCCC">
            <div align="center">
            <table>
                <tr><td align="center">Filtrar por</td></tr>
                <tr>
                    <td>Nit:<asp:TextBox ID="tb_proveedor_nit4" runat="server" Height="16px" 
                            Width="131px" /></td>
                </tr>
                <tr>
                    <td>Nombre:<asp:TextBox ID="tb_proveedor_nombre4" runat="server" Height="16px" 
                            Width="293px" /></td>
                </tr>
                <tr>
                    <td>Codigo:<asp:TextBox ID="tb_proveedor_codigo4" runat="server" Height="16px" 
                            Width="293px" /></td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btn_buscar_proveedor4" runat="server" Text="Buscar" 
                            onclick="btn_buscar_proveedor4_Click" /></td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table></div>
            <div style="padding: 10px; background-color: #0033CC; color: #FFFFFF;" 
                align="center">
                <asp:Label ID="Label12" runat="server" Text="Seleccionar Proveedor" />
            </div>
            <div align="center">
                <asp:GridView ID="gv_proveedor4" runat="server" AllowPaging="True" 
                    AutoGenerateSelectButton="True" 
                    onpageindexchanging="gv_proveedor4_PageIndexChanging" 
                    onselectedindexchanged="gv_proveedor4_SelectedIndexChanged" 
                    onpageindexchanged="gv_proveedor4_PageIndexChanged"
                    PageSize="5" onrowcreated="gv_proveedor4_RowCreated">
                </asp:GridView>
            </div>
            <div align="center">
                &nbsp;&nbsp;
                <br />
                <asp:Button ID="btn_proveedor_cancelar4" runat="server" Text="Cancelar" />
                <br />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlAgente1" runat="server" CssClass="CajaDialogo" style="display:none;" Width="800px" BackColor="#CCCCCC">
            <div align="center">
            <table>
                <tr><td align="center">Filtrar por</td></tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td>Nombre:<asp:TextBox ID="tb_agente_nombre2" runat="server" Height="16px" 
                            Width="293px" /></td>
                </tr>
                <tr>
                    <td>Codigo:<asp:TextBox ID="tb_agente_codigo2" runat="server" Height="16px" 
                            Width="293px" /></td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btn_buscar_agente1" runat="server" Text="Buscar" 
                            onclick="btn_buscar_agente1_Click" /></td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table></div>
            <div style="padding: 10px; background-color: #0033CC; color: #FFFFFF;" 
                align="center">
                <asp:Label ID="Label13" runat="server" Text="Seleccionar Agente" />
            </div>
            <div align="center">
                <asp:GridView ID="gv_agentes1" runat="server" AllowPaging="True" 
                    AutoGenerateSelectButton="True" 
                    onpageindexchanging="gv_agentes1_PageIndexChanging" 
                    onselectedindexchanged="gv_agentes1_SelectedIndexChanged" 
                    onpageindexchanged="gv_agentes1_PageIndexChanged"
                    PageSize="5" onrowcreated="gv_agentes1_RowCreated">
                </asp:GridView>
            </div>
            <div align="center">
                &nbsp;&nbsp;
                <br />
                <asp:Button ID="btn_agente_cancelar1" runat="server" Text="Cancelar" />
                <br />
                <br />
            </div>
        </asp:Panel>
</asp:Content>

