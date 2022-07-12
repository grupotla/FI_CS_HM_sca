<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="configurar_series.aspx.cs" Inherits="Manager_configurar_series" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style3
        {
            height: 17px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div align="center">
    <table align="center" cellpadding="0" cellspacing="0" style=" width:1000px; height: 420px; background-color:White;">
            <tr>
                <td align="center" valign="top">
                <br />
                    <table align="center" cellpadding="0" cellspacing="0" style=" width:700px; background-color:White;">
                        <tr>
                            <td>
                                <strong>CONFIGURACION DE SERIES</strong></td>
                        </tr>
                        <tr>
                            <td align="center" height="20px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Menu ID="Menu1" runat="server" BorderColor="#3366CC" BorderStyle="Double" 
                                    BorderWidth="2px" Orientation="Horizontal" BackColor="White" 
                                    Width="300px" onmenuitemclick="Menu1_MenuItemClick">
                                    <Items>
                                        <asp:MenuItem Text="Configuraciones" Value="0"></asp:MenuItem>
                                        <asp:MenuItem Text="Ingresar" Value="1"></asp:MenuItem>
                                        <asp:MenuItem Text="Eliminar" Value="2"></asp:MenuItem>
                                    </Items>
                                </asp:Menu>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" height="20px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:MultiView ID="MultiView1" runat="server">
                                    <asp:View ID="View1" runat="server">
                                        <table align="center" cellpadding="0" cellspacing="0" style="width: 80%">
                                            <tr>
                                                <td align="center" class="style3" style="color: black;">
                                                    <strong>Series Configuradas</strong></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:GridView ID="gv_series_configuradas" runat="server" Font-Size="XX-Small">
                                                    </asp:GridView>
                                                    &nbsp;&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                    <asp:View ID="View2" runat="server">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 80%">
                                    <tr>
                                        <td align="center" colspan="2" 
                                            style="color: black; " class="style3">
                                            <strong>Ingresar Configuracion de Series</strong></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Sistema</td>
                                        <td>
                                            <asp:DropDownList ID="drp_sistema" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                        <tr>
                                            <td>
                                                Linea de Servicio</td>
                                            <td>
                                                <asp:DropDownList ID="drp_linea_servicio" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    <tr>
                                        <td>
                                            Empresa</td>
                                        <td>
                                            <asp:DropDownList ID="drp_empresa" runat="server" Height="16px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                        <tr>
                                            <td>
                                                Tipo de Documento</td>
                                            <td>
                                                <asp:DropDownList ID="drp_tipo_documento" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Tipo de Contabilidad</td>
                                            <td>
                                                <asp:DropDownList ID="drp_contabilidad" runat="server" AutoPostBack="True" 
                                                    onselectedindexchanged="drp_contabilidad_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Seleccione...</asp:ListItem>
                                                    <asp:ListItem Value="1">Fiscal</asp:ListItem>
                                                    <asp:ListItem Value="2">Financiera</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    <tr>
                                        <td>
                                            Moneda</td>
                                        <td>
                                            <asp:DropDownList ID="drp_moneda" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="drp_moneda_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Sucursal</td>
                                        <td>
                                            <asp:DropDownList ID="drp_sucursal" runat="server" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Operacion</td>
                                        <td>
                                            <asp:DropDownList ID="drp_tipo_operacion" runat="server" AutoPostBack="True" 
                                                onselectedindexchanged="drp_tipo_operacion_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Seleccione</asp:ListItem>
                                                <asp:ListItem Value="1">Facturacion</asp:ListItem>
                                                <asp:ListItem Value="2">Operaciones</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Serie</td>
                                        <td>
                                            <asp:DropDownList ID="drp_serie" runat="server">
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
                                        <td align="center" colspan="2">
                                            <asp:Button ID="btn_guardar_configuracion" runat="server" Text="Guardar" 
                                                onclick="btn_guardar_configuracion_Click" />
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btn_limpiar" runat="server" 
                                                Text="Limpiar" onclick="btn_limpiar_Click" />
                                        </td>
                                    </tr>
                                        <tr>
                                            <td align="center" colspan="2">
                                                &nbsp;</td>
                                        </tr>
                                </table>
                                    </asp:View>
                                    <asp:View ID="View3" runat="server">
                                        <table align="center" cellpadding="0" cellspacing="0" style="width: 80%">
                                            <tr>
                                                <td align="center" class="style3" style="color: black;">
                                                    <strong>Eliminar Configuracion</strong></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    <asp:GridView ID="gv_series_configuradas2" runat="server" Font-Size="XX-Small" 
                                                        onrowdeleting="gv_series_configuradas2_RowDeleting">
                                                        <Columns>
                                                            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                                                        </Columns>
                                                    </asp:GridView>
                                                    &nbsp;&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                </asp:MultiView>
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
</div>
</asp:Content>

