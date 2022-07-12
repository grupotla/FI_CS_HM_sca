using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class Operaciones_detalle_carga_terrestre : System.Web.UI.Page
{
    int empresaID = 0;
    int sistemaID = 0;
    int ttoID = 0;
    int blID = 0;
    int sID = 0;
    string usuID = "";
    string MBL = "";
    PaisBean Empresa_Bean = null;
    Bean_Datos_BL Datos_Carga = null;
    RE_GenericBean Sistema_Bean = null;
    RE_GenericBean Tipo_Operacion_Bean = null;
    UsuarioBean user;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userSCA"] == null)
        {
            Response.Redirect("../Default.aspx");
        }
        user = (UsuarioBean)Session["userSCA"];
        int opcion = DB.Validar_Opcion_Usuario(user, 6);
        if (opcion == 0)
        {
            Response.Redirect("~/Home.aspx");
        }

        if (!IsPostBack)
        {
            if (Request.QueryString["usuID"] == null)
            {
                btn_siguiente.Visible = false;
                lbl_error.Text = "Error de Autenticacion";
                lbl_error.Visible = true;
                return;
            }
            if (Request.QueryString["sID"] == null)
            {
                #region Sesion Inexistente
                #region Obtener Variables
                if (Request.QueryString["empresaID"] == null)
                {
                    btn_siguiente.Visible = false;
                    lbl_error.Text = "Error de Autenticacion";
                    lbl_error.Visible = true;
                    return;
                }
                if (Request.QueryString["sistemaID"] == null)
                {
                    btn_siguiente.Visible = false;
                    lbl_error.Text = "Error de Autenticacion";
                    lbl_error.Visible = true;
                    return;
                }
                if (Request.QueryString["ttoID"] == null)
                {
                    btn_siguiente.Visible = false;
                    lbl_error.Text = "Error de Autenticacion";
                    lbl_error.Visible = true;
                    return;
                }
                if (Request.QueryString["blID"] == null)
                {
                    btn_siguiente.Visible = false;
                    lbl_error.Text = "Error de Autenticacion";
                    lbl_error.Visible = true;
                    return;
                }
                if (Request.QueryString["usuID"] == null)
                {
                    btn_siguiente.Visible = false;
                    lbl_error.Text = "Error de Autenticacion";
                    lbl_error.Visible = true;
                    return;
                }
                if (Request.QueryString["mbl"] == null)
                {
                    btn_siguiente.Visible = false;
                    lbl_error.Text = "Error de Autenticacion";
                    lbl_error.Visible = true;
                    return;
                }
                empresaID = int.Parse(Request.QueryString["empresaID"].ToString());
                sistemaID = int.Parse(Request.QueryString["sistemaID"].ToString());
                ttoID = int.Parse(Request.QueryString["ttoID"].ToString());
                blID = int.Parse(Request.QueryString["blID"].ToString());
                usuID = Request.QueryString["usuID"].ToString();
                MBL = Request.QueryString["mbl"].ToString().Trim();
                #endregion
                int Sesion = Contabilizacion_Automatica_CAD.Validar_Existencia_Sesion_Reconciliacion_Carga(empresaID, sistemaID, ttoID, blID, MBL);
                if (Sesion == -100)
                {
                    #region Error al validar existencia
                    lbl_error.Text = "Existio un error al tratar de validar la existencia de la Sesion";
                    lbl_error.Visible = true;
                    return;
                    #endregion
                }
                if (Sesion == 0)
                {
                    #region Crear Sesion
                    //Cargar Parametros de Forma Normal
                    Empresa_Bean = DB.getPais(empresaID);
                    Sistema_Bean = DB.getSistema_By_ID(sistemaID.ToString());
                    Tipo_Operacion_Bean = DB.getTipo_Operacion_By_ID(ttoID.ToString());
                    Datos_Carga = Contabilizacion_Automatica_CAD.Get_DatosBL_X_Traficos(sistemaID, ttoID, blID, Empresa_Bean);
                    lbl_tipo.Text = "";
                    if (Datos_Carga.Import_Export == 1) { lbl_tipo.Text = "IMPORTACION"; } else if (Datos_Carga.Import_Export == 2) { lbl_tipo.Text = "EXPORTACION"; }
                    lbl_mbl.Text = Datos_Carga.Mbl;
                    lbl_usuario.Text = usuID;
                    //Obtener Parametros para Crear Sesion
                    RE_GenericBean Bean_Temporal = new RE_GenericBean();
                    Bean_Temporal.strC1 = empresaID.ToString();
                    Bean_Temporal.strC2 = Empresa_Bean.Nombre_Sistema.ToUpper();
                    Bean_Temporal.strC3 = sistemaID.ToString();
                    Bean_Temporal.strC4 = Sistema_Bean.strC1.ToUpper();
                    Bean_Temporal.strC5 = ttoID.ToString();
                    Bean_Temporal.strC6 = Tipo_Operacion_Bean.strC1.ToUpper();
                    Bean_Temporal.strC7 = blID.ToString();
                    Bean_Temporal.strC8 = Datos_Carga.viajeID.ToString();
                    Bean_Temporal.strC9 = Datos_Carga.Mbl.Trim();
                    Bean_Temporal.strC10 = Datos_Carga.Agente.ToString();
                    Bean_Temporal.strC11 = Datos_Carga.Naviera.ToString();
                    Bean_Temporal.strC12 = Datos_Carga.Import_Export.ToString();
                    Bean_Temporal.strC13 = lbl_tipo.Text;
                    Bean_Temporal.strC14 = usuID;
                    Bean_Temporal.strC15 = Datos_Carga.No_Viaje;
                    Bean_Temporal.strC16 = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    Bean_Temporal.strC17 = Datos_Carga.Fecha_Arribo;
                    #region Validar que la Sesion tenga Agente y Naviera
                    if ((Bean_Temporal.strC10 == "") || (Bean_Temporal.strC10 == "0"))
                    {
                        WebMsgBox.Show("La Carga no tiene asignado un Agente valido, por favor asignelo en el Sistema de Trafico para poder Contabilizar");
                    }
                    if ((Bean_Temporal.strC11 == "") || (Bean_Temporal.strC11 == "0"))
                    {
                        WebMsgBox.Show("La Carga no tiene Naviera asignada, por favor asignela en el Sistema de Trafico para poder Contabilizar");
                    }
                    #endregion
                    //Insertar Sesion
                    int resultado_crear_sesion = 0;
                    resultado_crear_sesion = Contabilizacion_Automatica_CAD.Crear_Sesion_Reconciliacion_Carga(Bean_Temporal);
                    if (resultado_crear_sesion == -100)
                    {
                        lbl_error.Text = "Existio un error al momento de tratar de crear la Sesion.";
                        lbl_error.Visible = true;
                        return;
                    }
                    #endregion
                }
                #region Cargar Sesion
                Sesion = Contabilizacion_Automatica_CAD.Validar_Existencia_Sesion_Reconciliacion_Carga(empresaID, sistemaID, ttoID, blID, MBL);
                lbl_sesion_id.Text = Sesion.ToString();
                RE_GenericBean Bean_Sesion = Contabilizacion_Automatica_CAD.Obtener_Detalle_Sesion_Reconciliacon_Carga(Sesion);
                lbl_empresa.Text = Bean_Sesion.strC3;
                lbl_sistema.Text = Bean_Sesion.strC5;
                lbl_tipo_operacion.Text = Bean_Sesion.strC7;
                lbl_mbl.Text = Bean_Sesion.strC10;
                lbl_tipo.Text = Bean_Sesion.strC14;
                lbl_usuario.Text = usuID;
                Empresa_Bean = DB.getPais(int.Parse(Bean_Sesion.strC2));
                Datos_Carga = Contabilizacion_Automatica_CAD.Get_DatosBL_X_Traficos(int.Parse(Bean_Sesion.strC4), int.Parse(Bean_Sesion.strC6), int.Parse(Bean_Sesion.strC8), Empresa_Bean);
                string sql = "";
                sql = "select agente from agentes where activo=true and agente_id=" + Bean_Sesion.strC12;
                lbl_naviera.Text = DB.getName(sql);
                sql = "select agente from agentes where activo=true and agente_id=" + Bean_Sesion.strC11;
                lbl_agente.Text = DB.getName(sql);
                lbl_fecha_arribo.Text = Datos_Carga.Fecha_Arribo;
                lbl_fecha_ingreso.Text = Datos_Carga.Fecha_Ingreso_Sistema;
                //lbl_no_viaje.Text = Bean_Sesion.strC17;
                btn_siguiente.Visible = true;
                Get_BLs(Sesion);
                #endregion
                #endregion
            }
            else
            {
                #region Cargar Sesion Existente

                sID = int.Parse(Request.QueryString["sID"].ToString());
                usuID = Request.QueryString["usuID"].ToString();
                lbl_sesion_id.Text = sID.ToString();
                RE_GenericBean Bean_Sesion = Contabilizacion_Automatica_CAD.Obtener_Detalle_Sesion_Reconciliacon_Carga(sID);

                lbl_empresa.Text = Bean_Sesion.strC3;
                lbl_sistema.Text = Bean_Sesion.strC5;
                lbl_tipo_operacion.Text = Bean_Sesion.strC7;
                lbl_mbl.Text = Bean_Sesion.strC9;
                lbl_tipo.Text = Bean_Sesion.strC11;
                lbl_usuario.Text = usuID;
                Empresa_Bean = DB.getPais(int.Parse(Bean_Sesion.strC2));
                Datos_Carga = Contabilizacion_Automatica_CAD.Get_DatosBL_X_Traficos(int.Parse(Bean_Sesion.strC4), int.Parse(Bean_Sesion.strC6), int.Parse(Bean_Sesion.strC8), Empresa_Bean);
                string sql = "";
                sql = "select agente from agentes where activo=true and agente_id=" + Bean_Sesion.strC12;
                lbl_naviera.Text = DB.getName(sql);
                sql = "select agente from agentes where activo=true and agente_id=" + Bean_Sesion.strC11;
                lbl_agente.Text = DB.getName(sql);
                lbl_fecha_arribo.Text = Datos_Carga.Fecha_Arribo;
                lbl_fecha_ingreso.Text = Datos_Carga.Fecha_Ingreso_Sistema;
                //lbl_no_viaje.Text = Bean_Sesion.strC17;
                btn_siguiente.Visible = true;
                Get_BLs(sID);
                #endregion
            }
        }
    }
    protected void gv_carga_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count > 1)
        {
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Operaciones/nueva.aspx");
    }
    protected void Get_BLs(int sesionID)
    {
        #region Cargar Listado de BLs
        RE_GenericBean Bean_Sesion = Contabilizacion_Automatica_CAD.Obtener_Detalle_Sesion_Reconciliacon_Carga(sesionID);
        int correlativo = 1;
        int sisID = 0;
        int ttoID = 0;
        int blID = 0;
        string MBL = "";
        PaisBean Empresa_Bean = null;
        sisID = int.Parse(Bean_Sesion.strC4);
        ttoID = int.Parse(Bean_Sesion.strC6);
        Empresa_Bean = DB.getPais(int.Parse(Bean_Sesion.strC2));
        blID = int.Parse(Bean_Sesion.strC8);
        MBL = Bean_Sesion.strC10;
        ArrayList Arr_BLs = Contabilizacion_Automatica_CAD.Get_BLs_X_Traficos_X_Master(sisID, ttoID, blID, Empresa_Bean, MBL);
        DataTable dt_bls = new DataTable();
        dt_bls.Columns.Add("NO.");
        dt_bls.Columns.Add("BLID");
        dt_bls.Columns.Add("CONTENEDORID");
        dt_bls.Columns.Add("MBL");
        dt_bls.Columns.Add("HBL");
        dt_bls.Columns.Add("ROUTING");
        dt_bls.Columns.Add("PESO");
        dt_bls.Columns.Add("VOLUMEN");
        dt_bls.Columns.Add("ID");
        dt_bls.Columns.Add("CONSIGNATARIO");
        foreach (Bean_Datos_BL BL in Arr_BLs)
        {
            string Nombre_Cliente = "";
            string sql = "";
            sql = "select nombre_cliente from clientes where id_cliente=" + BL.Cliente;
            Nombre_Cliente = DB.getName(sql);
            RE_GenericBean Bean_Puerto_Origen = new RE_GenericBean();
            Bean_Puerto_Origen = Contabilizacion_Automatica_CAD.Obtener_Puerto_Origen(BL.Puerto_Embarque_ID);
            //object[] Obj = { correlativo.ToString(), BL.BLID.ToString(), BL.ContenedorID.ToString(), BL.Mbl, BL.Hbl, BL.Routing, BL.Contenedor, BL.Peso.ToString(), BL.Volumen.ToString(), BL.Cliente.ToString(), Nombre_Cliente, Bean_Puerto_Origen.strC2 + " , " + Bean_Puerto_Origen.strC3 + " , " + Bean_Puerto_Origen.strC4 };
            object[] Obj = { correlativo.ToString(), BL.BLID.ToString(), BL.ContenedorID.ToString(), BL.Mbl, BL.Hbl, BL.Routing, BL.Peso.ToString(), BL.Volumen.ToString(), BL.Cliente.ToString(), Nombre_Cliente};
            correlativo++;
            dt_bls.Rows.Add(Obj);
        }
        gv_carga.DataSource = dt_bls;
        gv_carga.DataBind();
        #endregion
    }
    protected void btn_siguiente_Click(object sender, EventArgs e)
    {
        RE_GenericBean Bean_Sesion = Contabilizacion_Automatica_CAD.Obtener_Detalle_Sesion_Reconciliacon_Carga(int.Parse(lbl_sesion_id.Text));
        if (Bean_Sesion.strC24 == "4")
        {
            Response.Redirect("~/Operaciones/detalle_contabilizacion.aspx?empresaID=" + Bean_Sesion.strC2 + "&sistemaID=" + Bean_Sesion.strC4 + "&ttoID=" + Bean_Sesion.strC6 + "&blID=" + Bean_Sesion.strC8 + "&usuID=" + user.ID + "&sID=" + Bean_Sesion.strC1 + "");
        }
        PaisBean Empresa_Bean = DB.getPais(int.Parse(Bean_Sesion.strC2));
        ArrayList Arr_BLs = Contabilizacion_Automatica_CAD.Get_BLs_X_Traficos_X_Master(int.Parse(Bean_Sesion.strC4), int.Parse(Bean_Sesion.strC6), blID, Empresa_Bean, Bean_Sesion.strC10);
        #region Validar Existencia de Detalle de BL's
        int Detalle_BLs = Contabilizacion_Automatica_CAD.Validar_Existencia_Detalle_BLs_Reconciliacion_Carga(int.Parse(lbl_sesion_id.Text));
        if (Detalle_BLs == -100)
        {
            lbl_error.Text = "Existio un error al tratar de validar la existencia de los BL's de la Carga";
            lbl_error.Visible = true;
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
            Bean_Temporal.strC12 = lbl_sesion_id.Text;//trb_trs_id
            Bean_Temporal.strC13 = "FALSE";//trb_to_order
            Bean_Temporal.strC14 = "0";//trb_to_order_id
            Bean_Temporal.strC15 = "0";//trb_puerto_origen_id
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
                if (lbl_tipo_operacion.Text == "LCL")
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
                Bean_Temporal.strC12 = lbl_sesion_id.Text;//trb_trs_id
                Bean_Temporal.strC15 = BL.Puerto_Embarque_ID.ToString();//trb_puerto_origen_id
                Arr_Detalle_BLs.Add(Bean_Temporal);
            }
            Detalle_BLs = 0;
            Detalle_BLs = Contabilizacion_Automatica_CAD.Insertar_Detalle_BLs_Reconciliacion_Carga(Arr_Detalle_BLs);
            if (Detalle_BLs == -100)
            {
                lbl_error.Text = "Existio un error al Insertar el Detalle de los BL's de la Carga";
                lbl_error.Visible = true;
                return;
            }
            #endregion
        }
        #endregion
        Response.Redirect("~/Operaciones/quiz_terrestre.aspx?empresaID=" + Bean_Sesion.strC2 + "&sistemaID=" + Bean_Sesion.strC4 + "&ttoID=" + Bean_Sesion.strC6 + "&blID=" + Bean_Sesion.strC8 + "&usuID=" + lbl_usuario.Text + "&sID=" + lbl_sesion_id.Text + "");
    }
}