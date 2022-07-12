<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="configurar_usuarios.aspx.cs" Inherits="Manager_configurar_usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">

        .style3
        {
            width: 70%;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div align="center">
    <table align="center" cellpadding="0" cellspacing="0" style=" width:1000px; height: 420px; background-color:White;">
        <tr>
            <td height="400px">
                <table align="center" cellpadding="0" cellspacing="0" class="style3">
                    <tr>
                        <td align="center" colspan="4">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <strong>CONFIGURACION DE USUARIOS</strong></td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td colspan="3">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left" height="30px" width="150px">
                            <strong>USUARIOS</strong></td>
                        <td colspan="3">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <table align="center" cellpadding="0" cellspacing="0" class="style3">
                                <tr>
                                    <td height="30px">
                                        Usuario</td>
                                    <td>
                            <asp:DropDownList ID="drp_usuarios" runat="server">
                            </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="30px">
                                        Empresa</td>
                                    <td>
                            <asp:DropDownList ID="drp_empresas" runat="server">
                            </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2" height="35px">
                            <asp:Button ID="btn_buscar" runat="server" Text="Buscar" 
                                onclick="btn_buscar_Click" />
&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btn_nueva_busqueda" runat="server" 
                                onclick="btn_nueva_busqueda_Click" Text="Nueva Busqueda" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" 
                            style="border-top-style: solid; border-width: 1px; border-top-color: #000000">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <strong>ESTADO</strong></td>
                        <td colspan="3">
                            <asp:RadioButtonList ID="rbl_estados" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">ACTIVAR</asp:ListItem>
                                <asp:ListItem Value="0">ELIMINAR</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" 
                            style="border-top-style: solid; border-width: 1px; border-top-color: #000000">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <strong>LINEAS DE SERVICIO</strong></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:CheckBoxList ID="chkbl_lineas_servicio" runat="server" RepeatColumns="4" 
                                RepeatDirection="Horizontal" Font-Size="XX-Small">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4" 
                            style="border-top-style: solid; border-width: 1px; border-top-color: #000000">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <strong>OPCIONES MENU</strong></td>
                        <td colspan="3">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td align="left" colspan="3">
                            CONTABILIZACION</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td align="center" colspan="3">
                            <asp:CheckBoxList ID="chkbl_opcion2" runat="server" Font-Size="XX-Small">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td align="left" colspan="3">
                            TARIFARIO</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td align="center" colspan="3">
                            <asp:CheckBoxList ID="chkbl_opcion3" runat="server" Font-Size="XX-Small">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td align="left" colspan="3">
                            ADMINISTRACION</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td align="center" colspan="3">
                            <asp:CheckBoxList ID="chkbl_opcion4" runat="server" Font-Size="XX-Small">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td align="left" colspan="3">
                            REPORTES</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td align="center" colspan="3">
                            <asp:CheckBoxList ID="chkbl_opcion5" runat="server" Font-Size="XX-Small">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td colspan="3">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Button ID="btn_guardar" runat="server" onclick="btn_guardar_Click" 
                                Text="Guardar" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
</asp:Content>

