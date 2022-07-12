using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class Tarifas_ingresar_tarifa : System.Web.UI.Page
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
        int opcion = DB.Validar_Opcion_Usuario(user, 8);
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
        //drp_moneda.Items.Clear();
        drp_empresa.Items.Add(item);
        //drp_moneda.Items.Add(item);
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
            item = new ListItem(Bean_Tarifa.strC2 + " - " + Bean_Tarifa.strC4, Bean_Tarifa.strC1);
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
    protected void drp_empresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (drp_empresa.SelectedValue != "0")
        //{
        //    ArrayList arr = null;
        //    ListItem item = new ListItem("Seleccione...", "0");
        //    drp_moneda.Items.Clear();
        //    drp_moneda.Items.Add(item);
        //    arr = (ArrayList)DB.getMonedasbyCriterio(" and tpm_pai_id=" + drp_empresa.SelectedValue + " ");
        //    foreach (RE_GenericBean rgb in arr)
        //    {
        //        item = new ListItem(rgb.strC2, rgb.strC1);
        //        drp_moneda.Items.Add(item);
        //    }
        //}
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
        tb_nombreb.Text = "";
        tb_codigo.Text = "";
        tb_nitb.Text = "";
        gv_clientes.DataBind();
    }
    protected void btn_guardar_Click(object sender, EventArgs e)
    {
        if (drp_tipo_tarifa.SelectedValue == "0")
        {
            WebMsgBox.Show("Por Favor seleccione el Tipo de Tarifa");
            return;
        }
        #region Validaciones
        RE_GenericBean Bean = null;
        int Tipo_Tarifa = int.Parse(drp_tipo_tarifa.SelectedValue);
        switch (Tipo_Tarifa)
        {
            case 1://Coloading Rate
                if (drp_empresa.SelectedValue == "0")
                {
                    WebMsgBox.Show("Por favor seleccione la Empresa");
                    return;
                }
                if (drp_tipo_persona.SelectedValue == "0")
                {
                    WebMsgBox.Show("Por favor seleccione el Tipo de Proveedor");
                    return;
                }
                if (tb_persona_id.Text == "0")
                {
                    WebMsgBox.Show("Por favor seleccione un Proveedor");
                    return;
                }
                if ((tb_tarifa_base.Text == "0.00") || (tb_tarifa_base.Text == "0") || (tb_tarifa_base.Text == ""))
                {
                    WebMsgBox.Show("Por favor Ingrese el valor la Tarifa Base");
                    return;
                }
                Bean = new RE_GenericBean();
                Bean.intC1 = int.Parse(drp_empresa.SelectedValue);
                Bean.intC2 = int.Parse(drp_tipo_persona.SelectedValue);
                Bean.intC3 = int.Parse(tb_persona_id.Text);
                Bean.intC4 = 8;
                Bean.douC1 = double.Parse(tb_tarifa_base.Text);
                Bean.douC2 = double.Parse(tb_tarifa_adicional.Text);
                Bean.intC5 = int.Parse(drp_tipo_tarifa.SelectedValue);
                Bean.strC1 = user.ID;
                Bean.intC6 = 0;
                break;
            case 4:
            case 6:
                if (drp_empresa.SelectedValue == "0")
                {
                    WebMsgBox.Show("Por favor seleccione la Empresa");
                    return;
                }
                if ((tb_tarifa_base.Text == "0.00") || (tb_tarifa_base.Text == "0") || (tb_tarifa_base.Text == ""))
                {
                    WebMsgBox.Show("Por favor Ingrese el valor la Tarifa Base");
                    return;
                }
                Bean = new RE_GenericBean();
                Bean.intC1 = int.Parse(drp_empresa.SelectedValue);
                Bean.intC2 = 0;
                Bean.intC3 = 0;
                Bean.intC4 = 0;
                Bean.douC1 = double.Parse(tb_tarifa_base.Text);
                Bean.douC2 = double.Parse(tb_tarifa_adicional.Text);
                Bean.intC5 = int.Parse(drp_tipo_tarifa.SelectedValue);
                Bean.strC1 = user.ID;
                Bean.intC6 = 0;
                break;
            case 7:
                if (drp_empresa.SelectedValue == "0")
                {
                    WebMsgBox.Show("Por favor seleccione la Empresa");
                    return;
                }
                if (drp_tipo_persona.SelectedValue == "0")
                {
                    WebMsgBox.Show("Por favor seleccione el Tipo de Proveedor");
                    return;
                }
                if (tb_persona_id.Text == "0")
                {
                    WebMsgBox.Show("Por favor seleccione un Proveedor");
                    return;
                }
                if ((tb_tarifa_base.Text == "0.00") || (tb_tarifa_base.Text == "0") || (tb_tarifa_base.Text == ""))
                {
                    WebMsgBox.Show("Por favor Ingrese el valor la Tarifa Base");
                    return;
                }
                Bean = new RE_GenericBean();
                Bean.intC1 = int.Parse(drp_empresa.SelectedValue);
                Bean.intC2 = int.Parse(drp_tipo_persona.SelectedValue);
                Bean.intC3 = int.Parse(tb_persona_id.Text);
                Bean.intC4 = 0;
                Bean.douC1 = double.Parse(tb_tarifa_base.Text);
                Bean.douC2 = double.Parse(tb_tarifa_adicional.Text);
                Bean.intC5 = int.Parse(drp_tipo_tarifa.SelectedValue);
                Bean.strC1 = user.ID;
                Bean.intC6 = 0;
                break;
            case 2:
            case 3:
            case 5:
            case 8:
            case 9:// 3,8,9 Rebates - 2 Inland - 5 Intermodal
                if (drp_empresa.SelectedValue == "0")
                {
                    WebMsgBox.Show("Por favor seleccione la Empresa");
                    return;
                }
                if (drp_linea_servicio.SelectedValue == "0")
                {
                    WebMsgBox.Show("Por favor seleccione la Linea de Servicio");
                    return;
                }
                if (drp_tipo_persona.SelectedValue == "0")
                {
                    WebMsgBox.Show("Por favor seleccione el Tipo de Proveedor");
                    return;
                }
                if (tb_persona_id.Text == "0")
                {
                    WebMsgBox.Show("Por favor seleccione un Proveedor");
                    return;
                }
                if ((tb_tarifa_base.Text == "0.00") || (tb_tarifa_base.Text == "0") || (tb_tarifa_base.Text == ""))
                {
                    WebMsgBox.Show("Por favor Ingrese el valor la Tarifa Base");
                    return;
                }
                Bean = new RE_GenericBean();
                Bean.intC1 = int.Parse(drp_empresa.SelectedValue);
                Bean.intC2 = int.Parse(drp_tipo_persona.SelectedValue);
                Bean.intC3 = int.Parse(tb_persona_id.Text);
                Bean.intC4 = 8;
                Bean.douC1 = double.Parse(tb_tarifa_base.Text);
                Bean.douC2 = double.Parse(tb_tarifa_adicional.Text);
                Bean.intC5 = int.Parse(drp_tipo_tarifa.SelectedValue);
                Bean.strC1 = user.ID;
                Bean.intC6 = int.Parse(drp_linea_servicio.SelectedValue);
                break;
        }
        #endregion
        int bandera_existencia = 0;
        bandera_existencia = Contabilizacion_Automatica_CAD.Validar_Existencia_Tarifa_Contabilizacion_Automatica(Bean.intC1, Bean.intC2, Bean.intC3, Bean.intC5, Bean.intC6);
        if (bandera_existencia == -100)
        {
            WebMsgBox.Show("Existio un error al momento de Validar la existencia de la Tarifa");
            return;
        }
        else if (bandera_existencia > 0)
        {
            WebMsgBox.Show("Tarifa Existente, para poder ingresar esta Tarifa primero debe eliminar la anterior");
            return;
        }
        int resultado = Contabilizacion_Automatica_CAD.Insertar_Tarifa_Contabilizacion_Automatica(Bean);
        if ((resultado == 0) || (resultado == -100))
        {
            WebMsgBox.Show("Existio un error al momento de Ingresar la Tarifa, por favor intente mas tarde");
            return;
        }
        else
        {
            drp_tipo_tarifa.SelectedValue = "0";
            drp_empresa.SelectedValue = "0";
            drp_tipo_persona.SelectedValue = "0";
            drp_linea_servicio.SelectedValue = "0";
            tb_persona_nombre.Text = "";
            tb_persona_id.Text = "0";
            drp_moneda.Items.Clear();
            ListItem item = new ListItem("Seleccione...", "0");
            drp_moneda.Items.Add(item);
            tb_tarifa_base.Text = "0.00";
            tb_tarifa_adicional.Text = "0.00";
            WebMsgBox.Show("Tarifa Ingresada exitosamente");
            return;
        }

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
                    pnl_moneda.Visible = false;
                    pnl_linea_servicio.Visible = false;
                    break;
                case 4:
                case 6:
                    //TRANSFERENCIAS A TERCEROS Y CONVERSION A USD
                    pnl_tipo_persona.Visible = false;
                    pnl_nombre.Visible = false;
                    pnl_codigo.Visible = false;
                    pnl_moneda.Visible = false;
                    pnl_linea_servicio.Visible = false;
                    break;
                case 7:
                    //GARANTIAS
                    pnl_tipo_persona.Visible = true;
                    pnl_nombre.Visible = true;
                    pnl_codigo.Visible = true;
                    pnl_moneda.Visible = false;
                    pnl_linea_servicio.Visible = false;
                    break;
                case 2:
                case 3:
                case 5:
                case 8:
                case 9:
                    // 3,8,9 Rebates - 2 Inland - 5 Intermodal
                    pnl_tipo_persona.Visible = true;
                    pnl_nombre.Visible = true;
                    pnl_codigo.Visible = true;
                    pnl_moneda.Visible = false;
                    pnl_linea_servicio.Visible = true;
                    break;
            }
            drp_empresa.SelectedValue = "0";
            drp_linea_servicio.SelectedValue = "0";
            drp_tipo_persona.SelectedValue = "0";
            tb_persona_nombre.Text = "";
            tb_persona_id.Text = "0";
            tb_tarifa_base.Text = "0.00";
            tb_tarifa_adicional.Text = "0.00";
        }
        else
        {
            lbl_tipo_persona.Visible = false;
            drp_tipo_persona.Visible = false;
            lbl_nombre.Visible = false;
            tb_persona_nombre.Visible = false;
            lbl_codigo.Visible = false;
            tb_persona_id.Visible = false;
            lbl_moneda.Visible = false;
            drp_moneda.Visible = false;
            drp_moneda.SelectedValue = "0";
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