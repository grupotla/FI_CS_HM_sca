using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class Operaciones_continuar : System.Web.UI.Page
{
    UsuarioBean user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userSCA"] == null)
        {
            Response.Redirect("../Default.aspx");
        }
        user = (UsuarioBean)Session["userSCA"];
        int opcion = DB.Validar_Opcion_Usuario(user, 7);
        if (opcion == 0)
        {
            Response.Redirect("~/Home.aspx");
        }
        if (!Page.IsPostBack)
        {
            Obtengo_listas();
        }
    }
    protected void Obtengo_listas()
    {
        ArrayList arr = (ArrayList)DB.getPaises("");
        ListItem item = new ListItem("Seleccione...", "0");
        drp_empresa.Items.Clear();
        foreach (PaisBean pais in arr)
        {
            item = new ListItem(pais.Nombre_Sistema, pais.ID.ToString());
            drp_empresa.Items.Add(item);
        }
        drp_empresa.SelectedValue = user.PaisID.ToString();

        arr = null;
        drp_linea_servicio.Items.Clear();
        item = new ListItem("Seleccione...", "0");
        drp_linea_servicio.Items.Add(item);
        arr = DB.getTipo_Operacion();
        ArrayList Arr_Lineas_Servicio = (ArrayList)DB.Get_Lineas_Servicio_Configuradas(user.PaisID, user.ID);

        foreach (RE_GenericBean Bean_Tipo_Operacion in arr)
        {
            foreach (string LineaID in Arr_Lineas_Servicio)
            {
                if (LineaID == Bean_Tipo_Operacion.intC1.ToString())
                {
                    item = new ListItem(Bean_Tipo_Operacion.strC1, Bean_Tipo_Operacion.intC1.ToString());
                    drp_linea_servicio.Items.Add(item);
                }
            }
        }
    }
    protected void dgw1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DataTable dt1;
        dt1 = (DataTable)ViewState["dt"];
        dgw1.DataSource = dt1;
        dgw1.PageIndex = e.NewPageIndex;
        dgw1.DataBind();
    }
    protected void dgw1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count > 1)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[10].Visible = false;
        }
    }
    protected void dgw1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Seleccionar")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int sesionID = 0;
            int estado_Sesion = 0;
            sesionID = int.Parse(dgw1.Rows[index].Cells[1].Text);
            estado_Sesion = int.Parse(dgw1.Rows[index].Cells[10].Text);
            RE_GenericBean Bean_Sesion = Contabilizacion_Automatica_CAD.Obtener_Detalle_Sesion_Reconciliacon_Carga(sesionID);
            if (estado_Sesion == 4)
            {
                Response.Redirect("~/Operaciones/detalle_contabilizacion.aspx?empresaID=" + Bean_Sesion.strC2 + "&sistemaID=" + Bean_Sesion.strC4 + "&ttoID=" + Bean_Sesion.strC6 + "&blID=" + Bean_Sesion.strC8 + "&usuID=" + user.ID + "&sID=" + sesionID + "");
            }
            else
            {
                #region Validar Existencia de Detalle de BL's
                PaisBean Empresa_Bean = DB.getPais(int.Parse(Bean_Sesion.strC2));
                ArrayList Arr_BLs = Contabilizacion_Automatica_CAD.Get_BLs_X_Traficos_X_Master(int.Parse(Bean_Sesion.strC4), int.Parse(Bean_Sesion.strC6), int.Parse(Bean_Sesion.strC8), Empresa_Bean, Bean_Sesion.strC10);
                int Detalle_BLs = Contabilizacion_Automatica_CAD.Validar_Existencia_Detalle_BLs_Reconciliacion_Carga(sesionID);
                if (Detalle_BLs == -100)
                {
                    WebMsgBox.Show("Existio un error al tratar de validar la existencia de los BL's de la Carga");
                    return;
                }
                else if (Detalle_BLs == 0)
                {
                    #region Insertar Detalle de BL's
                    ArrayList Arr_Detalle_BLs = new ArrayList();
                    RE_GenericBean Bean_Temporal = null;
                    Bean_Temporal = new RE_GenericBean();
                    Bean_Temporal.strC1 = "M";//trb_tipo_bl
                    Bean_Temporal.strC2 = Bean_Sesion.strC8;//trb_bl_id
                    Bean_Temporal.strC3 = Bean_Sesion.strC10;//trb_bl
                    Bean_Temporal.strC4 = "0";//trb_routing_id
                    Bean_Temporal.strC5 = "";//trb_routing
                    Bean_Temporal.strC6 = "0";//trb_contenedor_id
                    Bean_Temporal.strC7 = "";//trb_contenedor
                    Bean_Temporal.strC8 = "0";//trb_peso
                    Bean_Temporal.strC9 = "0";//trb_volumen
                    Bean_Temporal.strC10 = "0";//trb_cli_id
                    Bean_Temporal.strC11 = "";//trb_destino
                    Bean_Temporal.strC12 = sesionID.ToString();//trb_trs_id
                    Bean_Temporal.strC13 = "FALSE";//trb_to_order
                    Bean_Temporal.strC14 = "0";//trb_to_order_id
                    Arr_Detalle_BLs.Add(Bean_Temporal);
                    foreach (Bean_Datos_BL BL in Arr_BLs)
                    {
                        Bean_Temporal = new RE_GenericBean();
                        Bean_Temporal.strC1 = "H";//trb_tipo_bl
                        Bean_Temporal.strC2 = BL.BLID.ToString();//trb_bl_id
                        Bean_Temporal.strC3 = BL.Hbl;//trb_bl
                        Bean_Temporal.strC4 = BL.RoutingID.ToString();//trb_routing_id
                        Bean_Temporal.strC5 = BL.Routing.Trim();//trb_routing
                        Bean_Temporal.strC6 = BL.ContenedorID.ToString();//trb_contenedor_id
                        Bean_Temporal.strC7 = BL.Contenedor.Trim();//trb_contenedor
                        Bean_Temporal.strC8 = BL.Peso;//trb_peso
                        #region PESO
                        if ((Bean_Temporal.strC8 == "") || (Bean_Temporal.strC8 == "0") || (Bean_Temporal.strC8 == "0.00"))
                        {
                            WebMsgBox.Show("El HBL.: " + Bean_Temporal.strC3 + " tiene un Peso invalido.: " + Bean_Temporal.strC8 + ", por favor asigne un Peso valido en Trafico para poder Contabilizar ");
                            return;
                        }
                        #endregion
                        Bean_Temporal.strC9 = BL.Volumen;//trb_volumen
                        if (Bean_Sesion.strC7 == "LCL")
                        {
                            #region VOLUMEN
                            if ((Bean_Temporal.strC9 == "") || (Bean_Temporal.strC9 == "0") || (Bean_Temporal.strC9 == "0.00"))
                            {
                                WebMsgBox.Show("El HBL.: " + Bean_Temporal.strC3 + " tiene un Volumen invalido.: " + Bean_Temporal.strC9 + ", por favor asigne un Volumen valido en Trafico para poder Contabilizar ");
                                return;
                            }
                            #endregion
                        }
                        Bean_Temporal.strC10 = BL.Cliente.ToString();//trb_cli_id
                        Bean_Temporal.strC13 = "FALSE";
                        Bean_Temporal.strC14 = "0";//trb_to_order_id
                        #region CLIENTE
                        if ((Bean_Temporal.strC10 == "") || (Bean_Temporal.strC10 == "0") || (Bean_Temporal.strC10 == "0.00"))
                        {
                            WebMsgBox.Show("El HBL.: " + Bean_Temporal.strC3 + " no tiene un Cliente valido asignado.: " + Bean_Temporal.strC10 + ", por favor asigne uno valido en Trafico para poder Contabilizar ");
                            return;
                        }
                        #endregion
                        #region Validar Consignatario TO ORDER
                        int codigo_cliente = 0;
                        string nombre_cliente = "";
                        string sql_temporal = "";
                        codigo_cliente = int.Parse(BL.Cliente.ToString());
                        sql_temporal = "select nombre_cliente from clientes where id_cliente=" + codigo_cliente;
                        nombre_cliente = DB.getName(sql_temporal).Trim().ToUpper();
                        if ((codigo_cliente == 31621) || (codigo_cliente == 65402) || (nombre_cliente == "TO ORDER"))
                        {
                            Bean_Temporal.strC10 = "0";//trb_cli_id
                            Bean_Temporal.strC13 = "TRUE";//EL BL viene Consignado TO ORDER
                            Bean_Temporal.strC14 = codigo_cliente.ToString();//trb_to_order_id
                        }
                        #endregion
                        Bean_Temporal.strC11 = BL.Destino_Final.Trim();//trb_destino
                        Bean_Temporal.strC12 = sesionID.ToString();//trb_trs_id
                        Arr_Detalle_BLs.Add(Bean_Temporal);
                    }
                    Detalle_BLs = 0;
                    Detalle_BLs = Contabilizacion_Automatica_CAD.Insertar_Detalle_BLs_Reconciliacion_Carga(Arr_Detalle_BLs);
                    if (Detalle_BLs == -100)
                    {
                        WebMsgBox.Show("Existio un error al Insertar el Detalle de los BL's de la Carga");
                        return;
                    }
                    #endregion
                }
                #endregion
                Response.Redirect("~/Operaciones/quiz.aspx?empresaID=" + Bean_Sesion.strC2 + "&sistemaID=" + Bean_Sesion.strC4 + "&ttoID=" + Bean_Sesion.strC6 + "&blID=" + Bean_Sesion.strC8 + "&usuID=" + user.ID + "&sID=" + sesionID + "");
            }
        }
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        if (drp_empresa.SelectedValue == "0")
        {
            WebMsgBox.Show("Por Favor seleccione la Empresa a utilizar");
            return;
        }
        if (drp_linea_servicio.SelectedValue == "0")
        {
            WebMsgBox.Show("Por Favor seleccione la Linea de Servicio a utilizar");
            return;
        }
        if (tb_master.Text.Trim() == "")
        {
            WebMsgBox.Show("Por Favor Ingrese el Master a Contabilizar");
            return;
        }
        string sql = "";
        sql = " and trs_empresa_id=" + drp_empresa.SelectedValue + " and trs_tto_id=" + drp_linea_servicio.SelectedValue + " and trs_bl ilike '%" + tb_master.Text.Trim() + "%' ";
        ArrayList Arr_Sesiones = Contabilizacion_Automatica_CAD.Buscar_Sesiones_Contabilizacion_Automatica(sql);
        DataTable dt = new DataTable();
        dt.Columns.Add("SID");
        dt.Columns.Add("EMPRESA");
        dt.Columns.Add("SISTEMA");
        dt.Columns.Add("LINEA_SERVICIO");
        dt.Columns.Add("MASTER");
        dt.Columns.Add("VIAJE NO");
        dt.Columns.Add("TIPO");
        dt.Columns.Add("USUARIO_CREACION");
        dt.Columns.Add("FECHA_CREACION");
        dt.Columns.Add("ESTADO_ID");
        dt.Columns.Add("ESTADO");
        foreach (RE_GenericBean Bean in Arr_Sesiones)
        {
            object[] Ojb_Sesiones = { Bean.strC1, Bean.strC2, Bean.strC3, Bean.strC4, Bean.strC5, Bean.strC9, Bean.strC6, Bean.strC7, Bean.strC8, Bean.strC10, Bean.strC11 };
            dt.Rows.Add(Ojb_Sesiones);
        }
        dgw1.DataSource = dt;
        dgw1.DataBind();
    }
    protected void btn_nueva_Click(object sender, EventArgs e)
    {
        drp_linea_servicio.SelectedValue = "0";
        tb_master.Text = "";
        tb_master.Focus();
        dgw1.DataBind();
    }
}