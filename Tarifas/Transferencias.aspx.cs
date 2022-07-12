using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class Tarifas_Transferencias : System.Web.UI.Page
{
    UsuarioBean user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userSCA"] == null)
        {
            Response.Redirect("../Default.aspx");
        }
        user = (UsuarioBean)Session["userSCA"];
        int opcion = DB.Validar_Opcion_Usuario(user, 15);
        if (opcion == 0)
        {
            Response.Redirect("~/Home.aspx");
        }
        if (!Page.IsPostBack)
        {
            Obtener_Tarifas();
        }
    }
    protected void Obtener_Tarifas()
    {
        string sql = " and trct_trctt_id=4 ";
        #region Definir Data Table
        DataTable dt = new DataTable();
        dt.Columns.Add("ID");
        dt.Columns.Add("EMPRESA_ID");
        dt.Columns.Add("TPI_ID");
        dt.Columns.Add("PERSONA_ID");
        dt.Columns.Add("MONEDA_ID");
        dt.Columns.Add("TIPO_TARIFA_ID");
        dt.Columns.Add("EMPRESA");
        dt.Columns.Add("TIPO_PERSONA");
        dt.Columns.Add("CODIGO");
        dt.Columns.Add("NOMBRE");
        dt.Columns.Add("TIPO");
        dt.Columns.Add("MONEDA");
        dt.Columns.Add("TARIFA_BASE");
        dt.Columns.Add("TARIFA_ADICIONAL");
        #endregion
        ArrayList Arr_Tarifas = Contabilizacion_Automatica_CAD.Get_Tarifas_Contabilizacion_Automatica_Por_Criterio(sql);
        foreach (RE_GenericBean Bean_Tarifa in Arr_Tarifas)
        {
            object[] Obj = { Bean_Tarifa.strC1, Bean_Tarifa.strC2, Bean_Tarifa.strC3, Bean_Tarifa.strC4, Bean_Tarifa.strC5, Bean_Tarifa.strC8, Bean_Tarifa.strC11, Bean_Tarifa.strC12, Bean_Tarifa.strC4, Bean_Tarifa.strC13, Bean_Tarifa.strC15, Bean_Tarifa.strC14, Bean_Tarifa.strC6, Bean_Tarifa.strC7 };
            dt.Rows.Add(Obj);
        }
        gv_tarifas.DataSource = dt;
        gv_tarifas.DataBind();
    }
    protected void gv_tarifas_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count > 1)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[11].Visible = false;
        }
    }
}