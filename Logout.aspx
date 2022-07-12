<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="Logout.aspx.cs" Inherits="Logout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<table align="center" cellpadding="0" cellspacing="0" style=" width:1000px; height: 420px; background-color:White;">
            <tr>
                <td align="center" valign="middle">
                <br />
                    <table align="center" cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td align="center" width="300px">
                                <strong>FINALIZAR SESION</strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center">
                                Esta seguro de querer cerrar la Sesion?</td>
                        </tr>
                        <tr>
                            <td align="center">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center" height="50px" valign="middle">
                                <asp:Button ID="btn_aceptar" runat="server" onclick="btn_aceptar_Click" 
                                    Text="Si" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btn_cancelar" runat="server" onclick="btn_cancelar_Click" 
                                    Text="No" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
    </table>
</asp:Content>

