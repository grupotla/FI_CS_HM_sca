<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="definir_empresa.aspx.cs" Inherits="Definir_Empresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<table align="center" cellpadding="0" cellspacing="0" style=" width:1000px; height: 420px; background-color:White;">
            <tr>
                <td align="center" valign="middle">
                <p>
                    Por favor seleccione la Empresa que desea operar:</p>
                <br />
                    <table align="center" cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td align="center" width="150px">
                                <strong>EMPRESA</strong></td>
                            <td align="center" height="30px">
                                <asp:DropDownList ID="drp_empresas" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" height="50px" valign="middle">
                                <asp:Button ID="btn_siguiente" runat="server" onclick="btn_siguiente_Click" 
                                    Text="Siguiente" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btn_salir" runat="server" onclick="btn_salir_Click" 
                                    Text="Salir" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
    </table>
</asp:Content>

