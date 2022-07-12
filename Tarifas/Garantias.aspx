<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Garantias.aspx.cs" Inherits="Tarifas_Garantias" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table align="center" cellpadding="0" cellspacing="0" style=" width:1000px; height: 420px; background-color:White;">
            <tr>
                <td align="center" valign="middle">
                    <table align="center" cellpadding="0" cellspacing="0" class="style3">
                        <tr>
                            <td colspan="2">
                                <strong>GARANTIAS DE MOVILIZACION DE CARGA</strong>
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
                            <td height="25px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Tipo de Persona</td>
                            <td height="25px">
                                <asp:DropDownList ID="drp_tipo_persona" runat="server">
                                    <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                                    <asp:ListItem Value="2">Agente</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td height="25px">
                                Nombre</td>
                            <td>
                                <asp:TextBox ID="tb_persona_nombre" runat="server" Height="16px" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="25px">
                                Codigo</td>
                            <td>
                                <asp:TextBox ID="tb_persona_id" runat="server" Height="16px" Width="50px">0</asp:TextBox>
                                <cc1:ModalPopupExtender ID="modalcliente" runat="server" 
                                    BackgroundCssClass="FondoAplicacion" CancelControlID="btnProveedorCancelar" 
                                    DropShadow="True" OnCancelScript="mpeClienteOnCancel()" 
                                    PopupControlID="pnlProveedor" TargetControlID="tb_persona_id" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <asp:Button ID="btn_buscar" runat="server" Text="Buscar" 
                                    onclick="btn_buscar_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" height="200px" valign="top">
                                <asp:GridView ID="gv_tarifas" runat="server" Font-Size="XX-Small" 
                                    onrowcreated="gv_tarifas_RowCreated">
                                </asp:GridView>
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

