using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Net;

public partial class Operaciones_contabilizar_terrestre : System.Web.UI.Page
{
    int empresaID = 0;
    string usuID = "";
    ListItem item = null;
    ArrayList arr = null;
    DataTable dt1;
    int sID = 0;
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt;
    UsuarioBean user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userSCA"] == null)
        {
            Response.Redirect("../Default.aspx");
        }
        user = (UsuarioBean)Session["userSCA"];
        if (!IsPostBack)
        {
            #region Obtener Variables
            if (Request.QueryString["usuID"] == null)
            {
                lbl_error.Text = "Error de Autenticacion";
                lbl_error.Visible = true;
                return;
            }
            if (Request.QueryString["sID"] == null)
            {
                lbl_error.Text = "Sesion Invalida";
                lbl_error.Visible = true;
                return;
            }
            sID = int.Parse(Request.QueryString["sID"].ToString());
            #region Verificar si la Sesion es Valida
            int ban_validar_sesion = 0;
            ban_validar_sesion = Contabilizacion_Automatica_CAD.Validar_Existencia_Sesion_Reconciliacion_X_ID(sID);
            if (ban_validar_sesion == -100)
            {
                WebMsgBox.Show("Existio un error al momento de verificar la Validez de la Sesion");
                Response.Redirect("~/Home.aspx");
            }
            if (ban_validar_sesion == 0)
            {
                WebMsgBox.Show("Sesion invalida");
                Response.Redirect("~/Home.aspx");
            }
            #endregion
            usuID = Request.QueryString["usuID"].ToString();
            lbl_sesion_id.Text = sID.ToString();
            RE_GenericBean Bean_Sesion = Contabilizacion_Automatica_CAD.Obtener_Detalle_Sesion_Reconciliacon_Carga(sID);
            lbl_empresa.Text = Bean_Sesion.strC3;
            lbl_empresaID.Text = Bean_Sesion.strC2;
            lbl_sistema.Text = Bean_Sesion.strC5;
            lbl_sistemaID.Text = Bean_Sesion.strC4;
            lbl_linea_servicio.Text = Bean_Sesion.strC7;
            lbl_mbl.Text = Bean_Sesion.strC10;
            //lbl_blID.Text = Bean_Sesion.strC8;
            lbl_ttoID.Text = Bean_Sesion.strC6;
            lbl_tipo.Text = Bean_Sesion.strC14;
            lbl_usuario.Text = usuID;
            lbl_viaje_no.Text = Bean_Sesion.strC17;
            string sql = "";
            sql = "select nombre from navieras where activo=true and id_naviera=" + Bean_Sesion.strC12;
            lbl_naviera.Text = DB.getName(sql);
            lbl_naviera_id.Text = Bean_Sesion.strC12;
            sql = "select agente from agentes where activo=true and agente_id=" + Bean_Sesion.strC11;
            lbl_agente.Text = DB.getName(sql);
            lbl_agente_id.Text = Bean_Sesion.strC11;
            //lbl_viajeID.Text = Bean_Sesion.strC9;
            #endregion
        }
        Obtener_Estado_Cuenta_Sesion(int.Parse(lbl_sesion_id.Text));
        #region Validar estado de la Sesion
        if (Request.QueryString["sID"] == null)
        {
            lbl_error.Text = "Sesion Invalida";
            lbl_error.Visible = true;
            return;
        }
        sID = int.Parse(Request.QueryString["sID"].ToString());
        if (sID > 0)
        {
            RE_GenericBean Bean_Sesion = Contabilizacion_Automatica_CAD.Obtener_Detalle_Sesion_Reconciliacon_Carga(sID);
            if (Bean_Sesion.strC24 == "4")
            {
                pnl_contabilizacion.Visible = true;
                lbl_confirmacion.Text = "El Embarque ya ha sido Contabilizado.";
                lbl_confirmacion.ForeColor = System.Drawing.Color.Black;
                btn_siguiente.Visible = true;
                btn_contabilizar.Visible = false;
                btn_regresar.Visible = false;
            }
        }
        #endregion
    }
    protected void Obtener_Estado_Cuenta_Sesion(int ID)
    {
        LibroDiarioDS ds = new LibroDiarioDS();
        ArrayList Arr_Transacciones_Estado_Cuenta = (ArrayList)Contabilizacion_Automatica_CAD.Obtener_Detalle_Transacciones_Reconciliacion_Carga(int.Parse(lbl_sesion_id.Text));
        foreach (RE_GenericBean Bean in Arr_Transacciones_Estado_Cuenta)
        {
            object[] objArr = { Bean.strC5, Bean.strC6, Bean.strC7, Bean.strC8, Bean.strC9, Bean.strC10, Bean.strC11, Bean.strC12, Bean.strC13, Bean.strC14, Bean.strC15, Bean.strC16, Bean.strC17, Bean.strC18, Bean.strC19, Bean.strC20, Bean.strC21, Bean.strC22, Bean.strC23, Bean.strC24, Convert.ToDouble(Bean.strC25), Convert.ToDouble(Bean.strC26), Convert.ToDouble(Bean.strC27), Convert.ToDouble(Bean.strC28), Convert.ToDouble(Bean.strC29), Convert.ToDouble(Bean.strC30), Convert.ToDouble(Bean.strC31), Convert.ToDouble(Bean.strC32), Convert.ToDouble(Bean.strC33), Convert.ToDouble(Bean.strC34), Convert.ToDouble(Bean.strC35), Convert.ToDouble(Bean.strC36), Bean.strC49 };
            ds.Tables["DS_Reconciliacion"].Rows.Add(objArr);
        }
        rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        rpt.Load(Server.MapPath("~/Reportes/CR_CA2.rpt"));
        rpt.SetDataSource(ds.Tables["DS_Reconciliacion"]);
        double TC = 8.0;
        TC = Convert.ToDouble(DB.getTipoCambioHoy(int.Parse(lbl_empresaID.Text)).ToString("#,#.00#;(#,#.00#)"));
        rpt.SetParameterValue("Tipo_Cambio", TC);
        rpt.SetParameterValue("Master", lbl_mbl.Text);
        rpt.SetParameterValue("No_Viaje", lbl_viaje_no.Text);
        rpt.SetParameterValue("Empresa", lbl_empresa.Text);
        rpt.SetParameterValue("Sistema", lbl_sistema.Text);
        rpt.SetParameterValue("Linea_Servicio", lbl_linea_servicio.Text);
        rpt.SetParameterValue("Logotipo", Server.MapPath("~/img/aimar_en.jpg"));
        CrystalReportViewer1.ReportSource = rpt;
        CrystalReportViewer1.DataBind();
    }
    protected void btn_regresar_Click(object sender, EventArgs e)
    {
        int sesionID = 0;
        sesionID = int.Parse(lbl_sesion_id.Text);
        RE_GenericBean Bean_Sesion = Contabilizacion_Automatica_CAD.Obtener_Detalle_Sesion_Reconciliacon_Carga(sesionID);
        Response.Redirect("~/Operaciones/quiz_terrestre.aspx?empresaID=" + Bean_Sesion.strC2 + "&sistemaID=" + Bean_Sesion.strC4 + "&ttoID=" + Bean_Sesion.strC6 + "&blID=" + Bean_Sesion.strC8 + "&usuID=" + user.ID + "&sID=" + sesionID + "");
    }
    protected void btn_contabilizar_Click(object sender, EventArgs e)
    {
        int _empresaID = 0;
        int _sesionID = 0;
        bool resultado_transmision = false;
        _empresaID = int.Parse(lbl_empresaID.Text);
        _sesionID = int.Parse(lbl_sesion_id.Text);
        ArrayList resultado = new ArrayList();

        #region Validaciones Facturacion Electronica de Costa Rica
        if ((_empresaID == 5) || (_empresaID == 21))
        {
            int ban_cliente = 0;
            RE_GenericBean Bean_Sesion_Aux = Contabilizacion_Automatica_CAD.Obtener_Detalle_Sesion_Reconciliacon_Carga(_sesionID);
            ArrayList Arr_BLs_Sesion = Contabilizacion_Automatica_CAD.Obtener_Detalle_BLs_Reconciliacion_Carga(_sesionID);
            foreach (RE_GenericBean Bean_BL_Sesion in Arr_BLs_Sesion)
            {
                if (Bean_BL_Sesion.strC2 == "H")
                {
                    if (Bean_BL_Sesion.strC12 == user.pais.ISO)
                    {
                        int cliID_Sesion = int.Parse(Bean_BL_Sesion.strC11);
                        string mensaje_cliente = "";
                        mensaje_cliente = "El Embarque.: " + Bean_Sesion_Aux.strC10 + " no puede ser Contabilizado porque El Cliente con ID.: " + cliID_Sesion + " del HBL.: " + Bean_BL_Sesion.strC4 + " ";
                        RE_GenericBean Bean_Cliente_Aux = DB.getDataClient(Convert.ToDouble(cliID_Sesion));
                        if ((Bean_Cliente_Aux.strC1.Trim() == "") || (Bean_Cliente_Aux.strC1.Trim() == ".") || (Bean_Cliente_Aux.strC1.Trim() == "0"))//NIT
                        {
                            ban_cliente++;
                            mensaje_cliente += "tiene un NIT invalido (" + Bean_Cliente_Aux.strC1.Trim() + "), por favor corregir en el Catalogo de Clientes, ";
                        }
                        if ((Bean_Cliente_Aux.strC3.Trim() == "") || (Bean_Cliente_Aux.strC3.Trim() == ".") || (Bean_Cliente_Aux.strC3.Trim() == "-"))//NOMBRE
                        {
                            ban_cliente++;
                            mensaje_cliente += "tiene un nombre invalido (" + Bean_Cliente_Aux.strC3.Trim() + "), por favor corregir en el Catalogo de Clientes, ";
                        }
                        if ((Bean_Cliente_Aux.strC4.Trim() == "") || (Bean_Cliente_Aux.strC4.Trim() == ".") || (Bean_Cliente_Aux.strC4.Trim() == "-"))//DIRECCION
                        {
                            ban_cliente++;
                            mensaje_cliente += "tiene una direccion invalida (" + Bean_Cliente_Aux.strC3.Trim() + "), por favor corregir en el Catalogo de Clientes, ";
                        }
                        if ((Bean_Cliente_Aux.strC9.Trim() == "") || (Bean_Cliente_Aux.strC9.Trim() == "0"))//TIPO IDENTIFICACION TRIBUTARIA
                        {
                            ban_cliente++;
                            mensaje_cliente += "no tiene Tipo de Identificacion Tributaria asignada, por favor corregir en el Catalogo de Clientes para poder Contabilizar. ";
                        }
                        if (ban_cliente > 0)
                        {
                            pnl_contabilizacion.Visible = true;
                            lbl_confirmacion.Text = mensaje_cliente;
                            lbl_confirmacion.ForeColor = System.Drawing.Color.Red;
                            btn_siguiente.Visible = true;
                            btn_contabilizar.Visible = false;
                            btn_siguiente.Visible = false;
                            return;
                        }
                    }
                }
            }
        }
        #endregion

        resultado = Contabilizacion_Automatica_CN.Generar_Contabilizacion_Embarque_SCA(int.Parse(lbl_sesion_id.Text));
        if (resultado == null)
        {
            pnl_contabilizacion.Visible = true;
            lbl_confirmacion.Text = "Existio un error al momento de Generar la Contabilizacion del Embarque";
            lbl_confirmacion.ForeColor = System.Drawing.Color.Black;
            btn_siguiente.Visible = false;
        }
        else if (resultado[0].ToString() == "0")
        {
            pnl_contabilizacion.Visible = true;
            lbl_confirmacion.Text = resultado[1].ToString();
            lbl_confirmacion.ForeColor = System.Drawing.Color.Red;
            btn_siguiente.Visible = false;
        }
        else if (resultado[0].ToString() == "1")
        {
            #region Actualizar Parametros de la Sesion
            string sql = "";
            sql = "update tbl_reconciliacion_carga_sesiones set trs_estado_sesion=4, " +
            "trs_ip_address_contabilizacion='" + Request.ServerVariables["REMOTE_ADDR"].ToString() + "', " +
            "trs_usu_contabilizacion='" + user.ID + "', trs_fecha_contabilizacion=now(), " +
            "trs_tipo_cambio_contabilizacion=" + user.pais.TipoCambio.ToString() + " where trs_id=" + int.Parse(lbl_sesion_id.Text) + " and trs_estado=1";
            Contabilizacion_Automatica_CAD.Actualizar_Informacion_Sesion_Cuestionario_Contabilizacion_Automatica(int.Parse(lbl_sesion_id.Text), sql);
            #endregion
            #region Generar Facturas Electronicas
            //Comentado para que se puedan hacer pruebas y no transmitir las Facturas Electronicas al GFACE
            if ((_empresaID == 1) || (_empresaID == 15) || (_empresaID == 5) || (_empresaID == 21)) //2019-07-25
            {
                WS_Facturacion_Electronica WS_Facturacion_Electronica = new WS_Facturacion_Electronica();
                resultado_transmision = WS_Facturacion_Electronica.Generar_Proceso_Batch(_empresaID, _sesionID);
            }
            #endregion
            pnl_contabilizacion.Visible = true;
            lbl_confirmacion.Text = resultado[1].ToString();
            lbl_confirmacion.ForeColor = System.Drawing.Color.Black;
            btn_siguiente.Visible = true;
            btn_contabilizar.Visible = false;
            btn_regresar.Visible = false;

            int sesionID = 0;
            sesionID = int.Parse(lbl_sesion_id.Text);
            RE_GenericBean Bean_Sesion = Contabilizacion_Automatica_CAD.Obtener_Detalle_Sesion_Reconciliacon_Carga(sesionID);
            Response.Redirect("~/Operaciones/detalle_contabilizacion.aspx?empresaID=" + Bean_Sesion.strC2 + "&sistemaID=" + Bean_Sesion.strC4 + "&ttoID=" + Bean_Sesion.strC6 + "&blID=" + Bean_Sesion.strC8 + "&usuID=" + user.ID + "&sID=" + sesionID + "");
        }
    }
    protected void btn_siguiente_Click(object sender, EventArgs e)
    {
        int sesionID = 0;
        sesionID = int.Parse(lbl_sesion_id.Text);
        RE_GenericBean Bean_Sesion = Contabilizacion_Automatica_CAD.Obtener_Detalle_Sesion_Reconciliacon_Carga(sesionID);
        Response.Redirect("~/Operaciones/detalle_contabilizacion.aspx?empresaID=" + Bean_Sesion.strC2 + "&sistemaID=" + Bean_Sesion.strC4 + "&ttoID=" + Bean_Sesion.strC6 + "&blID=" + Bean_Sesion.strC8 + "&usuID=" + user.ID + "&sID=" + sesionID + "");
    }
    private void Page_Unload(object sender, EventArgs e)
    {
        #region Clear Report Objects
        if (rpt != null)
        {
            rpt.Close();
            rpt.Dispose();
            GC.Collect();
        }
        #endregion
    }
    
}