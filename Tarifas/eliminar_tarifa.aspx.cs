using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class Tarifas_eliminar_tarifa : System.Web.UI.Page
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
        int opcion = DB.Validar_Opcion_Usuario(user, 9);
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
        arr = null;
        drp_tipo_tarifa.Items.Clear();
        item = new ListItem("Seleccione...", "0");
        drp_tipo_tarifa.Items.Add(item);
        arr = Contabilizacion_Automatica_CAD.Get_Tipo_Tarifa_Contabilizacion_Automatica();
        foreach (RE_GenericBean Bean_Tarifa in arr)
        {
            item = new ListItem(Bean_Tarifa.strC2, Bean_Tarifa.strC1);
            drp_tipo_tarifa.Items.Add(item);
        }
        drp_tipo_tarifa.SelectedIndex = 0;
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
        if (drp_tipo_tarifa.SelectedValue == "0")
        {
            WebMsgBox.Show("Por favor seleccione el Tipo de Tarifa");
            return;
        }
        Obtener_Tarifas();
    }
    protected void Obtener_Tarifas()
    {
        string sql = " and trct_empresa_id=" + drp_empresa.SelectedValue + " and trct_trctt_id=" + drp_tipo_tarifa.SelectedValue + " ";
        if (drp_tipo_persona.SelectedValue != "0")
        {
            sql += " and trct_tpi_id=" + drp_tipo_persona.SelectedValue + " ";
        }
        if ((tb_persona_id.Text.Trim()!="")&&(tb_persona_id.Text.Trim()!="0"))
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
            if (drp_tipo_tarifa.SelectedValue == "1")
            {
                //COLOADING RATE
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;
            }
            else if ((drp_tipo_tarifa.SelectedValue == "4") || (drp_tipo_tarifa.SelectedValue == "6"))
            {
                //TRANSFERENCIAS A TERCEROS Y CONVERSION A USD
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;
            }
            else if (drp_tipo_tarifa.SelectedValue == "7")
            {
                //GARANTIAS
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;
            }
            else if ((drp_tipo_tarifa.SelectedValue == "2") || (drp_tipo_tarifa.SelectedValue == "3") || (drp_tipo_tarifa.SelectedValue == "5") || (drp_tipo_tarifa.SelectedValue == "8") || (drp_tipo_tarifa.SelectedValue == "9"))
            {
                // 3,8,9 Rebates - 2 Inland - 5 Intermodal
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[2].Visible = false;
                e.Row.Cells[3].Visible = false;
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[15].Visible = false;
            }
        }
    }
    protected void gv_tarifas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            foreach (Button btn in e.Row.Cells[0].Controls.OfType<Button>())
            {
                if (btn.Text == "Eliminar")
                {
                    btn.Attributes["onclick"] = "if(!confirm('Esta seguro de querer Eliminar esta Tarifa ?')){ return false; };";
                }
            }
        }
    }
    protected void gv_tarifas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Eliminar")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int ID = int.Parse(gv_tarifas.Rows[index].Cells[1].Text);
            int result_update = 0;
            result_update = Contabilizacion_Automatica_CAD.Eliminar_Tarifa_Contabilizacion_Automatica(user, ID);
            if ((result_update == 0) || (result_update == -100))
            {
                WebMsgBox.Show("Existio un error al momento de Eliminar la Tarifa");
                return;
            }
            else
            {
                WebMsgBox.Show("Tarifa Eliminada exitosamente");
                drp_empresa.SelectedValue = "0";
                drp_tipo_tarifa.SelectedValue = "0";
                drp_tipo_persona.SelectedValue = "0";
                tb_persona_id.Text = "0";
                gv_tarifas.DataBind();
                return;
            }
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
    protected void drp_tipo_tarifa_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drp_tipo_tarifa.SelectedValue != "0")
        {
            int Tipo_Tarifa = int.Parse(drp_tipo_tarifa.SelectedValue);
            switch (Tipo_Tarifa)
            {
                case 1:
                    //COLOADING RATE
                    pnl_tipo_persona.Visible = true;
                    pnl_nombre.Visible = true;
                    pnl_codigo.Visible = true;
                    gv_tarifas.DataBind();
                    pnl_linea_servicio.Visible = false;
                    break;
                case 4:
                case 6:
                    //TRANSFERENCIAS A TERCEROS Y CONVERSION A USD
                    pnl_tipo_persona.Visible = false;
                    pnl_nombre.Visible = false;
                    pnl_codigo.Visible = false;
                    gv_tarifas.DataBind();
                    pnl_linea_servicio.Visible = false;
                    break;
                case 7:
                    pnl_tipo_persona.Visible = true;
                    pnl_nombre.Visible = true;
                    pnl_codigo.Visible = true;
                    gv_tarifas.DataBind();
                    pnl_linea_servicio.Visible = false;
                    break;
                case 2:
                case 3:
                case 5:
                case 8:
                case 9:
                    pnl_tipo_persona.Visible = true;
                    pnl_nombre.Visible = true;
                    pnl_codigo.Visible = true;
                    gv_tarifas.DataBind();
                    pnl_linea_servicio.Visible = true;
                    break;
            }
            drp_empresa.SelectedValue = "0";
            drp_linea_servicio.SelectedValue = "0";
            drp_tipo_persona.SelectedValue = "0";
            tb_persona_nombre.Text = "";
            tb_persona_id.Text = "0";
        }
        else
        {
            drp_empresa.SelectedValue = "0";
            drp_linea_servicio.SelectedValue = "0";
            drp_tipo_persona.SelectedValue = "0";
            tb_persona_nombre.Text = "";
            tb_persona_id.Text = "0";
            gv_tarifas.DataBind();
        }
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