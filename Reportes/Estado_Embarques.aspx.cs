using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class Reportes_Estado_Embarques : System.Web.UI.Page
{
    CrystalDecisions.CrystalReports.Engine.ReportDocument rpt;
    UsuarioBean user;
    LibroDiarioDS ds = new LibroDiarioDS();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userSCA"] == null)
        {
            Response.Redirect("../Default.aspx");
        }
        user = (UsuarioBean)Session["userSCA"];
        int opcion = DB.Validar_Opcion_Usuario(user, 19);
        if (opcion == 0)
        {
            Response.Redirect("~/Home.aspx");
        }
        if (!Page.IsPostBack)
        {
            Obtengo_listas();
        }
        ds = (LibroDiarioDS)ViewState["VS_Estado_Embarques"];
        if (ds != null)
        {
            rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            rpt.Load(Server.MapPath("~/Reportes/CR_Estado_Embarques.rpt"));
            rpt.SetDataSource(ds.Tables["R_Estado_Embarques"]);
            rpt.SetParameterValue("Empresa", user.pais.Nombre_Sistema);
            rpt.SetParameterValue("Sistema", "MARITIMO");
            rpt.SetParameterValue("Linea_Servicio", drp_linea_servicio.SelectedItem.Text);
            rpt.SetParameterValue("Logotipo", Server.MapPath(user.pais.Imagepath));
            CrystalReportViewer1.ReportSource = rpt;
            CrystalReportViewer1.DataBind();
        }
    }
    protected void Obtengo_listas()
    {
        ArrayList arr = null;
        ListItem item = null;
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
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        if (drp_linea_servicio.SelectedValue == "0")
        {
            WebMsgBox.Show("Por Favor seleccione la Linea de Servicio");
            return;
        }
        if (tb_fecha_inicial.Text == "")
        {
            WebMsgBox.Show("Por Favor seleccione la Fecha Inicial");
            return;
        }
        if (tb_fecha_final.Text == "")
        {
            WebMsgBox.Show("Por Favor seleccione la Fecha Final");
            return;
        }
        Generar_Reporte();
    }
    protected void Generar_Reporte()
    {
        int sisID = 0;
        int ttoID = 0;
        PaisBean Empresa_Bean = (PaisBean)DB.getPais(user.PaisID);
        ds = new LibroDiarioDS();
        string sql = "";
        string Fecha_Inicial = "";
        string Fecha_Final = "";

        if (drp_linea_servicio.SelectedValue == "0")
        {
            WebMsgBox.Show("Por Favor seleccione la Linea de Servicio");
            return;
        }
        if (tb_fecha_inicial.Text == "")
        {
            WebMsgBox.Show("Por Favor seleccione la Fecha Inicial");
            return;
        }
        if (tb_fecha_final.Text == "")
        {
            WebMsgBox.Show("Por Favor seleccione la Fecha Final");
            return;
        }



        #region Definir Parametros
        ttoID = int.Parse(drp_linea_servicio.SelectedValue);
        Fecha_Inicial = DB.DateFormat(tb_fecha_inicial.Text.Trim());
        Fecha_Final = DB.DateFormat(tb_fecha_final.Text.Trim());
        #region Maritimo
        if ((ttoID == 1) || (ttoID == 2))
        {
            sisID = 1;
        }
        if (ttoID == 1)
        {
            #region FCL
            sql = "";
            #endregion
        }
        else if (ttoID == 2)
        {
            #region LCL
            sql = "";
            #endregion
        }
        #endregion
        #endregion
        ArrayList Arr_Embarques = (ArrayList)Contabilizacion_Automatica_CAD.Get_Reporte_Estado_Embarques(Empresa_Bean, sisID, ttoID, Fecha_Inicial, Fecha_Final, sql);
        foreach (RE_GenericBean Bean in Arr_Embarques)
        {
            object[] objArr = { Bean.strC1, Bean.strC2, Bean.strC3, Bean.strC4, Bean.strC5, Bean.strC6, Bean.strC7, Bean.strC8, Bean.strC9, Bean.strC10, Bean.strC11, Bean.strC12, Bean.strC13, Bean.strC14, Bean.strC15, Bean.strC16, Bean.strC17 };
            ds.Tables["R_Estado_Embarques"].Rows.Add(objArr);
        }
        ViewState["VS_Estado_Embarques"] = ds;//Definir ViewState
        rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        rpt.Load(Server.MapPath("~/Reportes/CR_Estado_Embarques.rpt"));
        rpt.SetDataSource(ds.Tables["R_Estado_Embarques"]);
        rpt.SetParameterValue("Empresa", user.pais.Nombre_Sistema);
        rpt.SetParameterValue("Sistema", "MARITIMO");
        rpt.SetParameterValue("Linea_Servicio", drp_linea_servicio.SelectedItem.Text);
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