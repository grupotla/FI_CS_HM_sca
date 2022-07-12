using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class Operaciones_nueva : System.Web.UI.Page
{
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
        if (!Page.IsPostBack)
        {
            Obtengo_listas();
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
        int bandera_parametros = 0;
        if (tb_documento.Text.Trim() != "")
        {
            bandera_parametros++;
        }
        if (tb_house.Text.Trim() != "")
        {
            bandera_parametros++;
        }
        if (tb_routing.Text.Trim() != "")
        {
            bandera_parametros++;
        }
        if (tb_contenedor.Text.Trim() != "")
        {
            bandera_parametros++;
        }
        if (bandera_parametros == 0)
        {
            dgw1.DataBind();
            WebMsgBox.Show("Por Favor ingrese al menos un Criterio de busqueda (Master, House, Routing o Contenedor)");
            return;
        }
        int lista = 0;
        UsuarioBean user = new UsuarioBean();
        PaisBean Pais_Bean = new PaisBean();
        Pais_Bean = DB.getPais(int.Parse(drp_empresa.SelectedValue));
        user.pais = Pais_Bean;
        string sql = "";
        if (drp_linea_servicio.SelectedValue.Equals("2"))
        {
            sql = "";
            if (tb_documento.Text.Trim() != "")
            {
                sql += " and b.mbl='" + tb_documento.Text.Trim() + "' ";
            }
            if (tb_house.Text.Trim() != "")
            {
                sql += " and a.no_bl='" + tb_house.Text.Trim() + "' ";
            }
            if (tb_routing.Text.Trim() != "")
            {
                sql += " and numero_routing='" + tb_routing.Text.Trim() + "' ";
            }
            if (tb_contenedor.Text.Trim() != "")
            {
                sql += " and b.no_contenedor='" + tb_contenedor.Text.Trim() + "' ";
            }
            DataSet ds = DB.getBL_LCL_invoice(lista, tb_documento.Text.Trim(), user, sql);
            this.dgw1.DataSource = ds.Tables["bl_list"];
            this.dgw1.DataBind();
            ViewState["dt"] = ds.Tables["bl_list"];
        }
        else if (drp_linea_servicio.SelectedValue.Equals("1"))
        {
            sql = "";
            if (tb_documento.Text.Trim() != "")
            {
                sql += " and a.mbl='" + tb_documento.Text.Trim() + "' ";
            }
            if (tb_house.Text.Trim() != "")
            {
                sql += " and a.no_bl='" + tb_house.Text.Trim() + "' ";
            }
            if (tb_routing.Text.Trim() != "")
            {
                sql += " and numero_routing='" + tb_routing.Text.Trim() + "' ";
            }
            if (tb_contenedor.Text.Trim() != "")
            {
                sql += " and b.no_contenedor='" + tb_contenedor.Text.Trim() + "' ";
            }
            DataSet ds = DB.getBL_FCL_invoice(lista, tb_documento.Text.Trim(), user, sql);
            this.dgw1.DataSource = ds.Tables["bl_list"];
            this.dgw1.DataBind();
            ViewState["dt"] = ds.Tables["bl_list"];
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
            e.Row.Cells[6].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[8].Visible = false;
            e.Row.Cells[9].Visible = false;
        }
    }
    protected void dgw1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Seleccionar")
        {
            int index = Convert.ToInt32(e.CommandArgument);

            #region Prueba
            
            //Datos_Carga = Contabilizacion_Automatica_CAD.Get_DatosBL_X_Traficos(sistemaID, ttoID, blID, Empresa_Bean);
            //lbl_tipo.Text = "";
            //if (Datos_Carga.Import_Export == 1) { lbl_tipo.Text = "IMPORTACION"; } else if (Datos_Carga.Import_Export == 2) { lbl_tipo.Text = "EXPORTACION"; }
            #endregion

            if (dgw1.Rows[index].Cells[3].Text.ToUpper() == "PENDIENTE")
            {
                WebMsgBox.Show("El Master aun se encuentra PENDIENTE, por favor asigne el numero de Master correcto en el Sistema de Trafico para poder Contabilizar");
                return;
            }
            else if (dgw1.Rows[index].Cells[3].Text.ToUpper() == "0")
            {
                WebMsgBox.Show("El Master de la Carga es invalido, no puede ser.: " + 0 + " por favor asigne el numero de Master correcto en el Sistema de Trafico para poder Contabilizar");
                return;
            }
            else if ((dgw1.Rows[index].Cells[3].Text.ToUpper() == " ") || (dgw1.Rows[index].Cells[3].Text.ToUpper() == ""))
            {
                WebMsgBox.Show("El Master de la Carga es invalido, por favor asigne el numero de Master correcto en el Sistema de Trafico para poder Contabilizar");
                return;
            }
            else
            {
                int empresaID = int.Parse(drp_empresa.SelectedValue);
                RE_GenericBean Bean_Tipo_Operacion = DB.getTipo_Operacion_By_ID(drp_linea_servicio.SelectedValue);
                int sisID = int.Parse(Bean_Tipo_Operacion.strC2);
                int ttoID = Bean_Tipo_Operacion.intC1;
                int blID = int.Parse(dgw1.Rows[index].Cells[7].Text);
                string usuario = user.ID;
                string Import_Export = "";
                PaisBean Empresa_Bean = null;
                Empresa_Bean = DB.getPais(empresaID);
                Bean_Datos_BL Datos_Carga = null;
                Datos_Carga = Contabilizacion_Automatica_CAD.Get_DatosBL_X_Traficos(sisID, ttoID, blID, Empresa_Bean);
                if (Datos_Carga.Import_Export == 1) { Import_Export = "IMPORTACION"; } else if (Datos_Carga.Import_Export == 2) { Import_Export = "EXPORTACION"; }
                if (Import_Export == "EXPORTACION")
                {
                    WebMsgBox.Show("El Embarque " + dgw1.Rows[index].Cells[3].Text.ToUpper() + " no puede ser contabilizado porque es una Exportacion, el SCA solo opera Importaciones.");
                    return;
                }
                else if (Import_Export == "IMPORTACION")
                {
                    Response.Redirect("~/Operaciones/detalle_carga.aspx?empresaID=" + empresaID + "&sistemaID=" + sisID + "&ttoID=" + ttoID + "&blID=" + blID + "&usuID=" + usuario + "&mbl=" + dgw1.Rows[index].Cells[3].Text.Trim() + "");
                }
            }
        }
    }
    protected void btn_nueva_Click(object sender, EventArgs e)
    {
        tb_documento.Text = "";
        tb_house.Text = "";
        tb_routing.Text = "";
        tb_contenedor.Text = "";
        dgw1.DataBind();
        tb_documento.Focus();
    }
}