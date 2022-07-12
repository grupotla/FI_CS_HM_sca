<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="continuar.aspx.cs" Inherits="Operaciones_continuar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table align="center" cellpadding="0" cellspacing="0" style=" width:1000px; height: 420px; background-color:White;">
            <tr>
                <td>
                    <table align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" colspan="2">
                            <strong>CONTINUAR CONTABILIZACION DE DOCUMENTOS</strong></td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center">
                            Empresa</td>
                        <td height="30px">
                            <asp:DropDownList ID="drp_empresa" runat="server" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            Linea de Servicio
                        </td>
                        <td height="30px">
                            <asp:DropDownList ID="drp_linea_servicio" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="150px">
                            Master</td>
                        <td height="30px">
                            <asp:TextBox ID="tb_master" runat="server" Height="16px" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btn_buscar" runat="server" Text="Buscar" 
                                onclick="btn_buscar_Click" Height="26px" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btn_nueva" runat="server" Height="26px" 
                                onclick="btn_nueva_Click" Text="Nueva" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:GridView ID="dgw1" runat="server" 
                            AllowPaging="True" onpageindexchanging="dgw1_PageIndexChanging" 
                            PageSize="10" 
                            EmptyDataText="No existen datos con este criterio, por favor verifique" 
                            onrowcreated="dgw1_RowCreated" onrowcommand="dgw1_RowCommand" 
                                Font-Size="X-Small">
                            <Columns>
                                <asp:ButtonField HeaderText="Seleccionar" Text="Seleccionar" 
                                    CommandName="Seleccionar" />
                            </Columns>
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

