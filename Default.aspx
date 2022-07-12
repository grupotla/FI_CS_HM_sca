<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 40%;
        }
        .style2
        {
            width: 95%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table align="center" cellpadding="0" cellspacing="0" style=" width:1000px; height: 420px; background-color: White;">
            <tr>
                <td align="center" valign="middle">
                <h2>
                    Bienvenido
                </h2>
                <p>
                    Por favor 
                    ingrese sus datos para iniciar la Sesion.....
                </p>
                    <br />
                    <table align="center" cellpadding="0" cellspacing="0" class="style2">
                        <tr>
                            <td>
                    <table align="center" cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td align="center">
                                <strong>Usuario</strong></td>
                            <td align="center" height="30px">
                                <asp:TextBox ID="tb_usuario" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <strong>Password</strong></td>
                            <td align="center" height="30px">
                                <asp:TextBox ID="tb_password" runat="server" TextMode="Password"></asp:TextBox>
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
                                <asp:Button ID="btn_ingresar" runat="server" onclick="btn_ingresar_Click" 
                                    Text="Ingresar" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" height="50px" valign="middle">
                                <asp:Label ID="lbl_error" runat="server" Font-Bold="True" ForeColor="Red" 
                                    Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <strong><em>Descargar Tutorial&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btn_descargar_tutorial2" runat="server" BackColor="White" 
                                    BorderStyle="None" Font-Bold="True" ForeColor="#0000CC" 
                                    onclick="btn_descargar_tutorial2_Click" Text="Descargar" />
                                </em>
                                </strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <strong><em>Descargar Video Tutorial</em>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btn_descargar_tutorial" runat="server" BackColor="White" 
                                    BorderStyle="None" Font-Bold="True" ForeColor="#0000CC" 
                                    onclick="btn_descargar_tutorial_Click" Text="Descargar" />
                                </strong>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" valign="middle">
                    &nbsp;</td>
            </tr>
    </table>
</asp:Content>

