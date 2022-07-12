using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class Tarifas_Rebates : System.Web.UI.Page
{
    UsuarioBean user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userSCA"] == null)
        {
            Response.Redirect("../Default.aspx");
        }
        user = (UsuarioBean)Session["userSCA"];
        int opcion = DB.Validar_Opcion_Usuario(user, 13);
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
        drp_empresa.Items.Add(item);
        foreach (PaisBean pais in arr)
        {
            item = new ListItem(pais.Nombre, pais.ID.ToString());
            drp_empresa.Items.Add(item);
        }
        drp_empresa.SelectedIndex = 0;
        item = new ListItem("Seleccione...", "0");
        arr = DB.getTipo_Operacion();
        drp_linea_servicio.Items.Clear();
        drp_linea_servicio.Items.Add(item);
        foreach (RE_GenericBean Bean_Tipo_Operacion in arr)
        {
            item = new ListItem(Bean_Tipo_Operacion.strC1, Bean_Tipo_Operacion.intC1.ToString());
            drp_linea_servicio.Items.Add(item);
        }
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        if (drp_empresa.SelectedValue == "0")
        {
            WebMsgBox.Show("Por favor seleccione una Empresa");
            return;
        }

        if (drp_tipo_persona.SelectedValue == "0")
        {
            WebMsgBox.Show("Por favor seleccione el Tipo de Persona");
            return;
        }
        Obtener_Tarifas();
    }
    protected void Obtener_Tarifas()
    {
        string sql = " and trct_empresa_id=" + drp_empresa.SelectedValue + " and trct_trctt_id in (3,8,9) ";
        if (drp_tipo_persona.SelectedValue != "0")
        {
            sql += " and trct_tpi_id=" + drp_tipo_persona.SelectedValue + " ";
        }
        if ((tb_persona_id.Text.Trim() != "") && (tb_persona_id.Text.Trim() != "0"))
        {
            sql += " and trct_persona_id=" + tb_persona_id.Text + " ";
        }
        if (drp_linea_servicio.SelectedValue != "0")
        {
            sql += " and trct_linea_servicio=" + drp_linea_servicio.SelectedValue + " ";
        }
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
        dt.Columns.Add("TTOID");
        dt.Columns.Add("LINEA_SERVICIO");
        #endregion
        ArrayList Arr_Tarifas = Contabilizacion_Automatica_CAD.Get_Tarifas_Contabilizacion_Automatica_Por_Criterio(sql);
        foreach (RE_GenericBean Bean_Tarifa in Arr_Tarifas)
        {
            object[] Obj = { Bean_Tarifa.strC1, Bean_Tarifa.strC2, Bean_Tarifa.strC3, Bean_Tarifa.strC4, Bean_Tarifa.strC5, Bean_Tarifa.strC8, Bean_Tarifa.strC11, Bean_Tarifa.strC12, Bean_Tarifa.strC4, Bean_Tarifa.strC13, Bean_Tarifa.strC15, Bean_Tarifa.strC14, Bean_Tarifa.strC6, Bean_Tarifa.strC7, Bean_Tarifa.strC16, Bean_Tarifa.strC17 };
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
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[14].Visible = false;
        }
    }
}