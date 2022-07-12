<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Conversiones.aspx.cs" Inherits="Tarifas_Conversiones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table align="center" cellpadding="0" cellspacing="0" style=" width:1000px; height: 420px; background-color:White;">
            <tr>
                <td align="center" valign="middle">
                    <table align="center" cellpadding="0" cellspacing="0" class="style3">
                        <tr>
                            <td colspan="2">
                                <strong>TARIFAS DE CONVERSION A DOLARES</strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                &nbsp;</td>
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

