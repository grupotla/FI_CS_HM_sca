<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ingresar_tarifa.aspx.cs" Inherits="Tarifas_ingresar_tarifa" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style3
        {
            width: 50%;
            background-color: White;
        }
        .style4
        {
            height: 22px;
        }
    .style5
    {
        width: 100%;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table align="center" cellpadding="0" cellspacing="0" style=" width:1000px; height: 420px; background-color:White;">
            <tr>
                <td align="center" valign="middle">
    <table align="center" cellpadding="0" cellspacing="0" class="style3">
        <tr>
            <td colspan="2">
                <strong>INGRESAR TARIFA</strong>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
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
                <strong>Tipo de Tarifa</strong></td>
            <td height="25px">
                <asp:DropDownList ID="drp_tipo_tarifa" runat="server" AutoPostBack="True" 
                    Font-Bold="True" onselectedindexchanged="drp_tipo_tarifa_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Empresa</td>
            <td height="25px">
                <asp:DropDownList ID="drp_empresa" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="drp_empresa_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="pnl_linea_servicio" runat="server" Visible="False">
                    <table align="center" cellpadding="0" cellspacing="0" 
    class="style5">
                        <tr>
                            <td width="122px">
                                Linea Servicio</td>
                            <td>
                                <asp:DropDownList ID="drp_linea_servicio" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="pnl_tipo_persona" runat="server" Visible="False">
                    <table align="center" cellpadding="0" cellspacing="0" 
    class="style5">
                        <tr>
                            <td width="122px">
                                <asp:Label ID="lbl_tipo_persona" runat="server" Text="Tipo Persona"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drp_tipo_persona" runat="server">
                                    <asp:ListItem Value="0">Seleccione..</asp:ListItem>
                                    <asp:ListItem Value="2">Agente</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="pnl_nombre" runat="server" Visible="False">
                    <table align="center" cellpadding="0" cellspacing="0" 
    class="style5">
                        <tr>
                            <td width="122px">
                                <asp:Label ID="lbl_nombre" runat="server" Text="Nombre"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tb_persona_nombre" runat="server" Height="16px" Width="300px" 
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td class="style4" colspan="2">
                <asp:Panel ID="pnl_codigo" runat="server" Visible="False">
                    <table align="center" cellpadding="0" cellspacing="0" 
    class="style5">
                        <tr>
                            <td width="122px">
                                <asp:Label ID="lbl_codigo" runat="server" Text="Codigo"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="tb_persona_id" runat="server" Height="16px" ReadOnly="True" 
                                    Width="82px">0</asp:TextBox>
                                <cc1:ModalPopupExtender ID="modalcliente" runat="server" 
                                    BackgroundCssClass="FondoAplicacion" CancelControlID="btnProveedorCancelar" 
                                    DropShadow="True" OnCancelScript="mpeClienteOnCancel()" 
                                    PopupControlID="pnlProveedor" TargetControlID="tb_persona_id" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="pnl_moneda" runat="server" Visible="False">
                    <table align="center" cellpadding="0" cellspacing="0" 
    class="style5">
                        <tr>
                            <td width="122px">
                                <asp:Label ID="lbl_moneda" runat="server" Text="Moneda"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="drp_moneda" runat="server" Enabled="False">
                                    <asp:ListItem Selected="True" Value="0">Seleccione...</asp:ListItem>
                                    <asp:ListItem Value="8">USD</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                Tarifa Base</td>
            <td height="25px">
                <asp:TextBox ID="tb_tarifa_base" runat="server" Height="16px" Width="150px">0.00</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Tarifa Adicional</td>
            <td height="25px">
                <asp:TextBox ID="tb_tarifa_adicional" runat="server" Height="16px" 
                    Width="150px">0.00</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btn_guardar" runat="server" Text="Guardar" 
                    onclick="btn_guardar_Click" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
                </td>
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
                    <td align="center"><asp:Button ID="bt_buscar" runat="server" Text="Buscar" 
                            onclick="Button4_Click" /></td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table></div>
            <div style="padding: 10px; background-color: #0033CC; color: #FFFFFF;" 
                align="center">
                <asp:Label ID="Label9" runat="server" Text="Seleccionar Agente" />
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
</asp:Content>

