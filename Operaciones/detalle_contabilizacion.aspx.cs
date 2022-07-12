using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class Operaciones_detalle_contabilizacion : System.Web.UI.Page
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
            lbl_tipo_cambio.Text = Bean_Sesion.strC20;
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
    }
    protected void Obtener_Estado_Cuenta_Sesion(int ID)
    {
        //Transacciones del Embarque
        //LibroDiarioDS ds = new LibroDiarioDS();
        //ArrayList Arr_Transacciones_Estado_Cuenta = (ArrayList)Contabilizacion_Automatica_CAD.Obtener_Detalle_Transacciones_Contabilizadas_Sesion_SCA(int.Parse(lbl_sesion_id.Text));
        //foreach (RE_GenericBean Bean in Arr_Transacciones_Estado_Cuenta)
        //{
        //    object[] objArr = { Bean.strC5, Bean.strC6, Bean.strC7, Bean.strC8, Bean.strC9, Bean.strC10, Bean.strC11, Bean.strC12, Bean.strC13, Bean.strC14, Bean.strC15, Bean.strC16, Bean.strC17, Bean.strC18, Bean.strC19, Bean.strC20, Bean.strC21, Bean.strC22, Bean.strC23, Bean.strC24, Convert.ToDouble(Bean.strC25), Convert.ToDouble(Bean.strC26), Convert.ToDouble(Bean.strC27), Convert.ToDouble(Bean.strC28), Convert.ToDouble(Bean.strC29), Convert.ToDouble(Bean.strC30), Convert.ToDouble(Bean.strC31), Convert.ToDouble(Bean.strC32), Convert.ToDouble(Bean.strC33), Convert.ToDouble(Bean.strC34), Convert.ToDouble(Bean.strC35), Convert.ToDouble(Bean.strC36), Bean.strC49, Bean.strC58, Bean.strC59 };
        //    ds.Tables["DS_Reconciliacion"].Rows.Add(objArr);
        //}
        //rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        //rpt.Load(Server.MapPath("~/Reportes/CR_Transacciones_Embarque.rpt"));
        //rpt.SetDataSource(ds.Tables["DS_Reconciliacion"]);
        //CrystalReportViewer1.ReportSource = rpt;
        //CrystalReportViewer1.DataBind();

        //Afectacion Contable
        //LibroDiarioDS ds = new LibroDiarioDS();
        //ArrayList Arr_Transacciones_Estado_Cuenta = (ArrayList)Contabilizacion_Automatica_CAD.Obtener_Afectacion_Contable_Transacciones_Sesion_SCA(int.Parse(lbl_sesion_id.Text));
        //foreach (RE_GenericBean Bean in Arr_Transacciones_Estado_Cuenta)
        //{
        //    object[] objArr = { Bean.strC5, Bean.strC6, Bean.strC7, Bean.strC8, Bean.strC9, Bean.strC10, Bean.strC11, Bean.strC12, Bean.strC13, Bean.strC14, Bean.strC15, Bean.strC16, Bean.strC17, Bean.strC18, Bean.strC19, Bean.strC20, Bean.strC21, Bean.strC22, Bean.strC23, Bean.strC24, Convert.ToDouble(Bean.strC25), Convert.ToDouble(Bean.strC26), Convert.ToDouble(Bean.strC27), Convert.ToDouble(Bean.strC28), Convert.ToDouble(Bean.strC29), Convert.ToDouble(Bean.strC30), Convert.ToDouble(Bean.strC31), Convert.ToDouble(Bean.strC32), Convert.ToDouble(Bean.strC33), Convert.ToDouble(Bean.strC34), Convert.ToDouble(Bean.strC35), Convert.ToDouble(Bean.strC36), Bean.strC49, Bean.strC58, Bean.strC59, Bean.strC60, Bean.strC61, Bean.strC62, Bean.strC63, Bean.strC64 };
        //    ds.Tables["DS_Reconciliacion"].Rows.Add(objArr);
        //}
        //rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        //rpt.Load(Server.MapPath("~/Reportes/CR_Afectacion_Contable.rpt"));
        //rpt.SetDataSource(ds.Tables["DS_Reconciliacion"]);
        //CrystalReportViewer1.ReportSource = rpt;
        //CrystalReportViewer1.DataBind();

        LibroDiarioDS ds1 = new LibroDiarioDS();
        LibroDiarioDS ds2 = new LibroDiarioDS();
        LibroDiarioDS ds3 = new LibroDiarioDS();
        LibroDiarioDS ds4 = new LibroDiarioDS();
        LibroDiarioDS ds5 = new LibroDiarioDS();
        LibroDiarioDS ds6 = new LibroDiarioDS();

        //Transacciones del Embarque
        #region Obtener Detalle de Transacciones Contabilizadas del Embarque
        ArrayList Arr_Transacciones_Contabilizadas = (ArrayList)Contabilizacion_Automatica_CAD.Obtener_Detalle_Transacciones_Contabilizadas_Sesion_SCA(int.Parse(lbl_sesion_id.Text));
        foreach (RE_GenericBean Bean in Arr_Transacciones_Contabilizadas)
        {
            object[] objArr = { Bean.strC5, Bean.strC6, Bean.strC7, Bean.strC8, Bean.strC9, Bean.strC10, Bean.strC11, Bean.strC12, Bean.strC13, Bean.strC14, Bean.strC15, Bean.strC16, Bean.strC17, Bean.strC18, Bean.strC19, Bean.strC20, Bean.strC21, Bean.strC22, Bean.strC23, Bean.strC24, Convert.ToDouble(Bean.strC25), Convert.ToDouble(Bean.strC26), Convert.ToDouble(Bean.strC27), Convert.ToDouble(Bean.strC28), Convert.ToDouble(Bean.strC29), Convert.ToDouble(Bean.strC30), Convert.ToDouble(Bean.strC31), Convert.ToDouble(Bean.strC32), Convert.ToDouble(Bean.strC33), Convert.ToDouble(Bean.strC34), Convert.ToDouble(Bean.strC35), Convert.ToDouble(Bean.strC36), Bean.strC49, Bean.strC58, Bean.strC59 };
            ds2.Tables["DS_Reconciliacion"].Rows.Add(objArr);
        }
        #endregion
        //Estado de Cuenta de todo el Embarque
        #region Obtener Estado de Cuenta de todo el Embarque
        ArrayList Arr_Transacciones_Estado_Cuenta = (ArrayList)Contabilizacion_Automatica_CAD.Obtener_Detalle_Transacciones_Reconciliacion_Carga(int.Parse(lbl_sesion_id.Text));
        foreach (RE_GenericBean Bean in Arr_Transacciones_Estado_Cuenta)
        {
            object[] objArr = { Bean.strC5, Bean.strC6, Bean.strC7, Bean.strC8, Bean.strC9, Bean.strC10, Bean.strC11, Bean.strC12, Bean.strC13, Bean.strC14, Bean.strC15, Bean.strC16, Bean.strC17, Bean.strC18, Bean.strC19, Bean.strC20, Bean.strC21, Bean.strC22, Bean.strC23, Bean.strC24, Convert.ToDouble(Bean.strC25), Convert.ToDouble(Bean.strC26), Convert.ToDouble(Bean.strC27), Convert.ToDouble(Bean.strC28), Convert.ToDouble(Bean.strC29), Convert.ToDouble(Bean.strC30), Convert.ToDouble(Bean.strC31), Convert.ToDouble(Bean.strC32), Convert.ToDouble(Bean.strC33), Convert.ToDouble(Bean.strC34), Convert.ToDouble(Bean.strC35), Convert.ToDouble(Bean.strC36), Bean.strC49 };
            ds3.Tables["DS_Reconciliacion"].Rows.Add(objArr);
        }       
        #endregion
        //Resumen de Cuentas Contables de las Transacciones del Embarque
        #region Obtener Resumen Contable de las Transacciones del Embarque
        ArrayList Arr_Resumen_Contable_Transacciones = (ArrayList)Contabilizacion_Automatica_CAD.Obtener_Resumen_Contable_Transacciones_Sesion_SCA(int.Parse(lbl_sesion_id.Text));
        foreach (RE_GenericBean Bean in Arr_Resumen_Contable_Transacciones)
        {
            object[] objArr = { Bean.strC5, Bean.strC6, Bean.strC7, Bean.strC8, Bean.strC9, Bean.strC10, Bean.strC11, Bean.strC12, Bean.strC13, Bean.strC14, Bean.strC15, Bean.strC16, Bean.strC17, Bean.strC18, Bean.strC19, Bean.strC20, Bean.strC21, Bean.strC22, Bean.strC23, Bean.strC24, Convert.ToDouble(Bean.strC25), Convert.ToDouble(Bean.strC26), Convert.ToDouble(Bean.strC27), Convert.ToDouble(Bean.strC28), Convert.ToDouble(Bean.strC29), Convert.ToDouble(Bean.strC30), Convert.ToDouble(Bean.strC31), Convert.ToDouble(Bean.strC32), Convert.ToDouble(Bean.strC33), Convert.ToDouble(Bean.strC34), Convert.ToDouble(Bean.strC35), Convert.ToDouble(Bean.strC36), Bean.strC49, Bean.strC58, Bean.strC59, Bean.strC60, Bean.strC61, Bean.strC62, Bean.strC63, Bean.strC64 };
            ds4.Tables["DS_Reconciliacion"].Rows.Add(objArr);
        }
        #endregion
        //Detalle Contable de las Transacciones del Embarque
        //Estado de Cuenta del Agente
        #region Obtener Estado de Cuenta del Agente
        ArrayList Arr_Transacciones_Agente = (ArrayList)Contabilizacion_Automatica_CAD.Obtener_SOA_By_Tipo_Persona_Sesion_Reconciliacion_Carga(int.Parse(lbl_sesion_id.Text), 2);
        foreach (RE_GenericBean Bean in Arr_Transacciones_Agente)
        {
            object[] objArr = { Bean.strC5, Bean.strC6, Bean.strC7, Bean.strC8, Bean.strC9, Bean.strC10, Bean.strC11, Bean.strC12, Bean.strC13, Bean.strC14, Bean.strC15, Bean.strC16, Bean.strC17, Bean.strC18, Bean.strC19, Bean.strC20, Bean.strC21, Bean.strC22, Bean.strC23, Bean.strC24, Convert.ToDouble(Bean.strC25), Convert.ToDouble(Bean.strC26), Convert.ToDouble(Bean.strC27), Convert.ToDouble(Bean.strC28), Convert.ToDouble(Bean.strC29), Convert.ToDouble(Bean.strC30), Convert.ToDouble(Bean.strC31), Convert.ToDouble(Bean.strC32), Convert.ToDouble(Bean.strC33), Convert.ToDouble(Bean.strC34), Convert.ToDouble(Bean.strC35), Convert.ToDouble(Bean.strC36), Bean.strC49, Bean.strC58, Bean.strC59, Bean.strC60, Bean.strC61, Bean.strC62, Bean.strC63, Bean.strC64 };
            ds5.Tables["DS_Reconciliacion"].Rows.Add(objArr);
        }
        #endregion
        //Estado de Cuenta de la Naviera
        #region Obtener Estado de Cuenta del Agente
        ArrayList Arr_Transacciones_Naviera = (ArrayList)Contabilizacion_Automatica_CAD.Obtener_SOA_By_Tipo_Persona_Sesion_Reconciliacion_Carga(int.Parse(lbl_sesion_id.Text), 5);
        foreach (RE_GenericBean Bean in Arr_Transacciones_Naviera)
        {
            object[] objArr = { Bean.strC5, Bean.strC6, Bean.strC7, Bean.strC8, Bean.strC9, Bean.strC10, Bean.strC11, Bean.strC12, Bean.strC13, Bean.strC14, Bean.strC15, Bean.strC16, Bean.strC17, Bean.strC18, Bean.strC19, Bean.strC20, Bean.strC21, Bean.strC22, Bean.strC23, Bean.strC24, Convert.ToDouble(Bean.strC25), Convert.ToDouble(Bean.strC26), Convert.ToDouble(Bean.strC27), Convert.ToDouble(Bean.strC28), Convert.ToDouble(Bean.strC29), Convert.ToDouble(Bean.strC30), Convert.ToDouble(Bean.strC31), Convert.ToDouble(Bean.strC32), Convert.ToDouble(Bean.strC33), Convert.ToDouble(Bean.strC34), Convert.ToDouble(Bean.strC35), Convert.ToDouble(Bean.strC36), Bean.strC49, Bean.strC58, Bean.strC59, Bean.strC60, Bean.strC61, Bean.strC62, Bean.strC63, Bean.strC64 };
            ds6.Tables["DS_Reconciliacion"].Rows.Add(objArr);
        }
        #endregion
        rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        rpt.Load(Server.MapPath("~/Reportes/CR_EC_Consolidado.rpt"));
        rpt.SetDataSource(ds1.Tables["DS_Reconciliacion"]);
        rpt.Subreports["CR_Transacciones_Embarque.rpt"].SetDataSource(ds2);
        rpt.Subreports["CR_CA3.rpt"].SetDataSource(ds3);
        //double TC = 0;
        //TC = Convert.ToDouble(lbl_tipo_cambio.Text);
        //rpt.SetParameterValue("Tipo_Cambio", TC, rpt.Subreports[0].Name.ToString());
        rpt.Subreports["CR_Resumen_Contable.rpt"].SetDataSource(ds4);
        //rpt.Subreports["CR_Detalle_Contable.rpt"].SetDataSource(ds5);
        rpt.Subreports["CR_SOA_Agente.rpt"].SetDataSource(ds5);
        rpt.Subreports["CR_SOA_Naviera.rpt"].SetDataSource(ds6);
        rpt.SetParameterValue("Master", lbl_mbl.Text);
        rpt.SetParameterValue("No_Viaje", lbl_viaje_no.Text);
        rpt.SetParameterValue("Empresa", lbl_empresa.Text);
        rpt.SetParameterValue("Sistema", lbl_sistema.Text);
        rpt.SetParameterValue("Linea_Servicio", lbl_linea_servicio.Text);
        //rpt.SetParameterValue("Logotipo", Server.MapPath("~/img/aimar_en.jpg"));
        rpt.SetParameterValue("Logotipo", Server.MapPath(user.pais.Imagepath));
        CrystalReportViewer1.ReportSource = rpt;
        CrystalReportViewer1.DataBind();

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