using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class Tarifas_Garantias : System.Web.UI.Page
{
    UsuarioBean user;
    DataTable dt1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userSCA"] == null)
        {
            Response.Redirect("../Default.aspx");
        }
        user = (UsuarioBean)Session["userSCA"];
        int opcion = DB.Validar_Opcion_Usuario(user, 12);
        if (opcion == 0)
        {
            Response.Redirect("~/Home.aspx");
        }
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        if (drp_tipo_persona.SelectedValue == "0")
        {
            WebMsgBox.Show("Por favor seleccione el Tipo de Persona");
            return;
        }
        if ((tb_persona_id.Text == "") || (tb_persona_id.Text == "0"))
        {
            WebMsgBox.Show("Por favor seleccione un Proveedor");
            return;
        }
        Obtener_Tarifas();
    }
    protected void Obtener_Tarifas()
    {
        string sql = " and trct_trctt_id=7 ";
        if (drp_tipo_persona.SelectedValue != "0")
        {
            sql += " and trct_tpi_id=" + drp_tipo_persona.SelectedValue + " ";
        }
        if ((tb_persona_id.Text.Trim() != "") && (tb_persona_id.Text.Trim() != "0"))
        {
            sql += " and trct_persona_id=" + tb_persona_id.Text + " ";
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
            e.Row.Cells[11].Visible = false;
        }
    }
    protected void gv_clientes_PageIndexChanged(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            dt1 = (DataTable)ViewState["dt"];
        }
    }
    protected void gv_clientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dt1 = (DataTable)ViewState["dt"];
        gv_clientes.DataSource = dt1;
        gv_clientes.PageIndex = e.NewPageIndex;
        gv_clientes.DataBind();
        modalcliente.Show();
    }
    protected void gv_clientes_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = gv_clientes.SelectedRow;
        if (drp_tipo_persona.SelectedValue.Equals("4")) //proveedor
        {
            tb_persona_id.Text = Page.Server.HtmlDecode(row.Cells[1].Text);
            tb_persona_nombre.Text = Page.Server.HtmlDecode(row.Cells[3].Text);
        }
        else if (drp_tipo_persona.SelectedValue.Equals("2")) //agente
        {
            tb_persona_id.Text = Page.Server.HtmlDecode(row.Cells[1].Text);
            tb_persona_nombre.Text = Page.Server.HtmlDecode(row.Cells[2].Text);
        }
        else if (drp_tipo_persona.SelectedValue.Equals("5"))//naviera
        {
            tb_persona_id.Text = Page.Server.HtmlDecode(row.Cells[1].Text);
            tb_persona_nombre.Text = Page.Server.HtmlDecode(row.Cells[2].Text);
        }
        else if (drp_tipo_persona.SelectedValue.Equals("6"))//Lineas aereas
        {
            tb_persona_id.Text = Page.Server.HtmlDecode(row.Cells[1].Text);
            tb_persona_nombre.Text = Page.Server.HtmlDecode(row.Cells[2].Text);
        }
        else if (drp_tipo_persona.SelectedValue.Equals("10"))//Intercompanys
        {
            tb_persona_id.Text = Page.Server.HtmlDecode(row.Cells[1].Text);
            tb_persona_nombre.Text = Page.Server.HtmlDecode(row.Cells[6].Text);
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        DataTable dt = null;
        ArrayList Arr = null;
        string where = "";
        if (tb_codigo.Text.Equals("") && tb_nombreb.Text.Equals("") && tb_nitb.Text.Equals(""))
        {
            WebMsgBox.Show("Debe ingresar al menos un criterio de búsqueda");
            return;
        }
        if (drp_tipo_persona.SelectedValue.Equals("4"))
        {
            if (!tb_nombreb.Text.Trim().Equals("") && tb_nombreb.Text != null) where += " upper(rtrim(nombre)) like '%" + tb_nombreb.Text.Trim().ToUpper() + "%'";
            if (!tb_codigo.Text.Trim().Equals("") && tb_codigo.Text != null)
                if (!where.Equals("")) where += " and numero=" + tb_codigo.Text; else where += "numero=" + tb_codigo.Text;
            if (!tb_nitb.Text.Trim().Equals("") && tb_nitb.Text != null)
                if (!where.Equals("")) where += " and nit='" + tb_nitb.Text + "'"; else where += "nit='" + tb_nitb.Text + "'";
            Arr = (ArrayList)DB.getProveedor(where, ""); //Proveedor
            dt = (DataTable)Utility.fillGridView("Proveedor", Arr);
        }
        else if (drp_tipo_persona.SelectedValue.Equals("2"))
        {
            if (!tb_nitb.Text.Equals(""))
            {
                WebMsgBox.Show("No se puede buscar por nit a un agente");
                return;
            }
            if (!tb_nombreb.Text.Trim().Equals("") && tb_nombreb.Text != null) where += " upper(rtrim(agente)) like '%" + tb_nombreb.Text.Trim().ToUpper() + "%'";
            if (!tb_codigo.Text.Trim().Equals("") && tb_codigo.Text != null)
                if (!where.Equals("")) where += " and agente_id=" + tb_codigo.Text; else where += "agente_id=" + tb_codigo.Text;
            Arr = (ArrayList)DB.getAgente(where, ""); //Agente
            dt = (DataTable)Utility.fillGridView("Agente", Arr);
        }
        else if (drp_tipo_persona.SelectedValue.Equals("5"))
        {
            if (!tb_nitb.Text.Equals(""))
            {
                WebMsgBox.Show("No se puede buscar por nit una naviera");
                return;
            }
            if (!tb_nombreb.Text.Trim().Equals("") && tb_nombreb.Text != null) where += " upper(rtrim(nombre)) like '%" + tb_nombreb.Text.Trim().ToUpper() + "%'";
            if (!tb_codigo.Text.Trim().Equals("") && tb_codigo.Text != null)
                if (!where.Equals("")) where += " and id_naviera=" + tb_codigo.Text; else where += "id_naviera=" + tb_codigo.Text;
            Arr = (ArrayList)DB.getNavieras(where, ""); //Naviera
            dt = (DataTable)Utility.fillGridView("Naviera", Arr);
        }
        else if (drp_tipo_persona.SelectedValue.Equals("6"))
        {
            if (!tb_nitb.Text.Equals(""))
            {
                WebMsgBox.Show("No se puede buscar por nit una línea aerea");
                return;
            }
            if (!tb_nombreb.Text.Trim().Equals("") && tb_nombreb.Text != null) where += " and upper(rtrim(name)) ilike '%" + tb_nombreb.Text.Trim().ToUpper() + "%'";
            if (!tb_codigo.Text.Trim().Equals("") && tb_codigo.Text != null)
                if (!where.Equals("")) where += " and carrier_id=" + tb_codigo.Text; else where += " and carrier_id=" + tb_codigo.Text;

            Arr = (ArrayList)DB.getCarriers(where); //Lineas aereas
            dt = (DataTable)Utility.fillGridView("LineasAereas", Arr);
        }
        else if (drp_tipo_persona.SelectedValue.Equals("10"))
        {
            if (!tb_nombreb.Text.Trim().Equals("") && tb_nombreb.Text != null) where += " and upper(rtrim(nombre_comercial)) like '%" + tb_nombreb.Text.Trim().ToUpper() + "%'";
            if (!tb_codigo.Text.Trim().Equals("") && tb_codigo.Text != null)
                if (!where.Equals("")) where += " and id_intercompany=" + tb_codigo.Text; else where += " and id_intercompany=" + tb_codigo.Text;
            if (!tb_nitb.Text.Trim().Equals("") && tb_nitb.Text != null)
                if (!where.Equals("")) where += " and nit='" + tb_nitb.Text + "'"; else where += " and nit='" + tb_nitb.Text + "'";
            Arr = (ArrayList)DB.Get_Intercompanys(where);//Intercompany
            dt = (DataTable)Utility.fillGridView("Intercompany", Arr);
        }

        gv_clientes.DataSource = dt;
        gv_clientes.DataBind();
        ViewState["dt"] = dt;
        modalcliente.Show();
    }
    protected void gv_clientes_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.Cells.Count > 1)
        {
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
        }
    }
}