<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Inlands.aspx.cs" Inherits="Tarifas_Inlands" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table align="center" cellpadding="0" cellspacing="0" style=" width:1000px; height: 420px; background-color:White;">
            <tr>
                <td align="center" valign="middle">
                    <table align="center" cellpadding="0" cellspacing="0" class="style3">
                        <tr>
                            <td colspan="2">
                                <strong>INLANDS</strong>
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
                                Empresa</td>
                            <td height="25px">
                                <asp:DropDownList ID="drp_empresa" runat="server">
                                </asp:DropDownList>
                            </td>
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
                            <td>
                                Codigo</td>
                            <td>
                                <asp:TextBox ID="tb_persona_id" runat="server" Height="16px" Width="50px">0</asp:TextBox>
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
</asp:Content>

