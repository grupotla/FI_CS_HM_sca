<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="detalle_carga_terrestre.aspx.cs" Inherits="Operaciones_detalle_carga_terrestre" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table align="center" cellpadding="0" cellspacing="0" style=" width:1000px; height: 420px; background-color:White; ">
            <tr>
                <td align="center" valign="middle">
                    <h3><strong>Detalle de la Carga</strong></h3>
                </td>
            </tr>
            <tr>
                <td align="center" valign="middle">
                <br />
                <br />
                    <table align="center" cellpadding="0" cellspacing="0" style=" vertical-align:top; " >
                        <tr>
                            <td>
                                Empresa</td>
                            <td>
                                <asp:Label ID="lbl_empresa" runat="server" style="font-weight: 700"></asp:Label>
                                <asp:Label ID="lbl_sesion_id" runat="server" Text="0" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Sistema</td>
                            <td>
                                <asp:Label ID="lbl_sistema" runat="server" style="font-weight: 700"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="185px">
                                Linea de Servicio</td>
                            <td>
                                <asp:Label ID="lbl_tipo_operacion" runat="server" style="font-weight: 700"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tipo</td>
                            <td>
                                <asp:Label ID="lbl_tipo" runat="server" style="font-weight: 700"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Master</td>
                            <td>
                                <asp:Label ID="lbl_mbl" runat="server" style="font-weight: 700"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Usuario</td>
                            <td>
                                <asp:Label ID="lbl_usuario" runat="server" style="font-weight: 700"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Agente</td>
                            <td>
                                <asp:Label ID="lbl_agente" runat="server" style="font-weight: 700"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Transportista</td>
                            <td>
                                <asp:Label ID="lbl_naviera" runat="server" style="font-weight: 700"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fecha de Ingreso</td>
                            <td>
                                <asp:Label ID="lbl_fecha_ingreso" runat="server" style="font-weight: 700"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fecha de Arribo</td>
                            <td>
                                <asp:Label ID="lbl_fecha_arribo" runat="server" style="font-weight: 700"></asp:Label>
                            </td>
                        </tr>
          
                        </table>
                </td>
            </tr>
            <tr>
                <td align="center" style="vertical-align:top;">
                <br />
                <br />
                    <asp:GridView ID="gv_carga" runat="server" onrowcreated="gv_carga_RowCreated" 
                        Font-Size="X-Small">
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="center" style="vertical-align:top;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" style="vertical-align:top;">
                    <asp:Button ID="btn_regresar" runat="server" onclick="Button1_Click" 
                        Text="Regresar" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_siguiente" runat="server" Text="Siguiente" Visible="False" 
                        onclick="btn_siguiente_Click" />
                </td>
            </tr>
            <tr>
                <td align="center" style="vertical-align:top;">
                                <asp:Label ID="lbl_error" runat="server" ForeColor="Red" Visible="False" 
                                    Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" style="vertical-align:top;">
                                &nbsp;</td>
            </tr>
        </table>
</asp:Content>

