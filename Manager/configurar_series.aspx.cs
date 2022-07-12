using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class Manager_configurar_series : System.Web.UI.Page
{
    UsuarioBean user = null;
    ArrayList arr = null;
    ListItem item = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userSCA"] == null)
        {
            Response.Redirect("../Default.aspx");
        }
        user = (UsuarioBean)Session["userSCA"];
        int opcion = DB.Validar_Opcion_Usuario(user, 17);
        if (opcion == 0)
        {
            Response.Redirect("~/Home.aspx");
        }
        if (!Page.IsPostBack)
        {
            Obtengo_listas();
        }
    }
    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        MultiView1.ActiveViewIndex = int.Parse(Menu1.SelectedValue);
        if (Menu1.SelectedValue == "0")
        {
            gv_series_configuradas.DataSource = Obtener_Detalle_Configuracion_Series_Contabilizacion_Automatica("");
            gv_series_configuradas.DataBind();
        }
        else if (Menu1.SelectedValue == "2")
        {
            gv_series_configuradas2.DataSource = Obtener_Detalle_Configuracion_Series_Contabilizacion_Automatica("");
            gv_series_configuradas2.DataBind();
        }
    }
    protected void Obtengo_listas()
    {
        arr = (ArrayList)DB.getPaises("");
        drp_empresa.Items.Clear();
        item = new ListItem("Seleccione...", "0");
        drp_empresa.Items.Add(item);
        foreach (PaisBean pais in arr)
        {
            item = new ListItem(pais.Nombre_Sistema, pais.ID.ToString());
            drp_empresa.Items.Add(item);
        }
        drp_empresa.SelectedIndex = 0;

        arr = null;
        arr = (ArrayList)DB.GetDocumentosBySYS_TipoTeferencia();
        drp_tipo_documento.Items.Clear();
        item = new ListItem("Seleccione...", "0");
        drp_tipo_documento.Items.Add(item);
        foreach (RE_GenericBean Bean in arr)
        {
            item = new ListItem(Bean.strC1, Bean.intC1.ToString());
            drp_tipo_documento.Items.Add(item);
        }
        drp_tipo_documento.SelectedIndex = 0;

        arr = null;
        drp_sistema.Items.Clear();
        item = new ListItem("Seleccione...", "0");
        drp_sistema.Items.Add(item);
        arr = (ArrayList)DB.getSistemas();
        foreach (RE_GenericBean Bean in arr)
        {
            item = new ListItem(Bean.strC1, Bean.intC1.ToString());
            drp_sistema.Items.Add(item);
        }
        drp_sistema.SelectedIndex = 0;

        arr = null;
        drp_linea_servicio.Items.Clear();
        item = new ListItem("Seleccione...", "0");
        drp_linea_servicio.Items.Add(item);
        arr = (ArrayList)DB.getTipo_Operacion();
        foreach (RE_GenericBean Bean in arr)
        {
            item = new ListItem(Bean.strC1, Bean.intC1.ToString());
            drp_linea_servicio.Items.Add(item);
        }
        drp_linea_servicio.SelectedIndex = 0;

        item = new ListItem("Seleccione...", "0");
        drp_sucursal.Items.Clear();
        drp_moneda.Items.Clear();
        drp_serie.Items.Clear();
        drp_sucursal.Items.Add(item);
        drp_moneda.Items.Add(item);
        drp_serie.Items.Add(item);
        drp_sucursal.SelectedIndex = 0;
        drp_moneda.SelectedIndex = 0;
        drp_serie.SelectedIndex = 0;
    }
    protected void drp_contabilidad_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drp_sistema.SelectedValue == "0")
        {
            WebMsgBox.Show("Debe seleccionar el Sistema a Configurar");
            drp_sistema.SelectedIndex = 0;
            drp_contabilidad.SelectedValue = "0";
            return;
        }
        if (drp_linea_servicio.SelectedValue == "0")
        {
            WebMsgBox.Show("Debe seleccionar la Linea de Servicio a Configurar");
            drp_linea_servicio.SelectedIndex = 0;
            drp_contabilidad.SelectedValue = "0";
            return;
        }
        if (drp_empresa.SelectedValue == "0")
        {
            WebMsgBox.Show("Debe seleccionar la Empresa a Configurar");
            drp_empresa.SelectedIndex = 0;
            drp_contabilidad.SelectedValue = "0";
            return;
        }
        if (drp_tipo_documento.SelectedValue == "0")
        {
            WebMsgBox.Show("Debe seleccionar el Tipo de Documento a Configurar");
            drp_contabilidad.SelectedIndex = 0;
            drp_contabilidad.SelectedValue = "0";
            return;
        }
        if (drp_contabilidad.SelectedValue != "0")
        {
            arr = null;
            arr = (ArrayList)DB.getMonedasbyPais(int.Parse(drp_empresa.SelectedValue), int.Parse(drp_contabilidad.SelectedValue));
            drp_moneda.Items.Clear();
            item = new ListItem("Seleccione...", "0");
            drp_moneda.Items.Add(item);
            foreach (RE_GenericBean rgb in arr)
            {
                item = new ListItem(rgb.strC1, rgb.intC1.ToString());
                drp_moneda.Items.Add(item);
            }
            drp_moneda.SelectedIndex = 0;
            if (drp_tipo_documento.SelectedValue == "5")
            {
                ArrayList Arr_Sucursales = (ArrayList)DB.getSucursales(" and  suc_pai_id=" + int.Parse(drp_empresa.SelectedValue) + " and suc_nombre='SISTEMAS' ");
                if (Arr_Sucursales.Count == 0)
                {
                    #region Crear Sucursal
                    SucursalBean sucbean = new SucursalBean();
                    sucbean.Nombre = "SISTEMAS";
                    sucbean.paisID = int.Parse(drp_empresa.SelectedValue);
                    int result = DB.InsertUpdateSucursal(sucbean);
                    #endregion
                }

                drp_sucursal.Items.Clear();
                arr = null;
                arr = (ArrayList)DB.getSucursales(" and suc_pai_id=" + int.Parse(drp_empresa.SelectedValue) + " and suc_nombre ilike '%SISTEMAS%' ");
                item = new ListItem("Seleccione...", "0");
                drp_sucursal.Items.Add(item);
                foreach (SucursalBean Bean in arr)
                {
                    item = new ListItem(Bean.Nombre, Bean.ID.ToString());
                    drp_sucursal.Items.Add(item);
                }
                drp_sucursal.SelectedIndex = 0;
            }
            else
            {
                drp_sucursal.Items.Clear();
                arr = null;
                arr = (ArrayList)DB.getSucursales(" and suc_pai_id=" + int.Parse(drp_empresa.SelectedValue) + "  ");
                item = new ListItem("Seleccione...", "0");
                drp_sucursal.Items.Add(item);
                foreach (SucursalBean Bean in arr)
                {
                    item = new ListItem(Bean.Nombre, Bean.ID.ToString());
                    drp_sucursal.Items.Add(item);
                }
                drp_sucursal.SelectedIndex = 0;
            }
        }
    }
    protected void drp_tipo_operacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drp_sistema.SelectedValue == "0")
        {
            WebMsgBox.Show("Debe seleccionar el Sistema a Configurar");
            drp_sistema.SelectedIndex = 0;
            drp_tipo_operacion.SelectedValue = "0";
            return;
        }
        if (drp_linea_servicio.SelectedValue == "0")
        {
            WebMsgBox.Show("Debe seleccionar la Linea de Servicio a Configurar");
            drp_linea_servicio.SelectedIndex = 0;
            drp_tipo_operacion.SelectedValue = "0";
            return;
        }
        if (drp_empresa.SelectedValue == "0")
        {
            WebMsgBox.Show("Debe seleccionar la Empresa a Configurar");
            drp_empresa.SelectedIndex = 0;
            drp_tipo_operacion.SelectedValue = "0";
            return;
        }
        if (drp_tipo_documento.SelectedValue == "0")
        {
            WebMsgBox.Show("Debe seleccionar el Tipo de Documento a Configurar");
            drp_contabilidad.SelectedIndex = 0;
            drp_tipo_operacion.SelectedValue = "0";
            return;
        }
        if (drp_contabilidad.SelectedValue == "0")
        {
            WebMsgBox.Show("Debe seleccionar la Contabilidad a Configurar");
            drp_contabilidad.SelectedIndex = 0;
            drp_tipo_operacion.SelectedValue = "0";
            return;
        }
        if (drp_moneda.SelectedValue == "0")
        {
            WebMsgBox.Show("Debe seleccionar la Moneda a Configurar");
            drp_moneda.SelectedIndex = 0;
            drp_tipo_operacion.SelectedValue = "0";
            return;
        }
        if (drp_sucursal.SelectedValue == "0")
        {
            WebMsgBox.Show("Debe seleccionar la Sucursal a Configurar");
            drp_sucursal.SelectedIndex = 0;
            drp_tipo_operacion.SelectedValue = "0";
            return;
        }
        if (drp_tipo_operacion.SelectedValue == "0")
        {
            WebMsgBox.Show("Por Favor seleccione el Tipo de Operacion Configurar");
            drp_tipo_operacion.SelectedIndex = 0;
            return;
        }
        if (drp_tipo_documento.SelectedValue == "5")
        {
            drp_serie.Items.Clear();
            item = new ListItem("Seleccione...", "0");
            drp_serie.Items.Add(item);
            if (drp_contabilidad.SelectedValue == "1")
            {
                if (drp_moneda.SelectedValue == "8")
                {
                    if ((drp_empresa.SelectedValue == "2") || (drp_empresa.SelectedValue == "6") || (drp_empresa.SelectedValue == "26") || (drp_empresa.SelectedValue == "25"))
                    {
                        item = new ListItem("PFSA", "1");
                        drp_serie.Items.Add(item);
                    }
                    else
                    {
                        item = new ListItem("PFSAD", "3");
                        drp_serie.Items.Add(item);
                    }
                }
                else
                {
                    item = new ListItem("PFSA", "1");
                    drp_serie.Items.Add(item);
                }
            }
            else if (drp_contabilidad.SelectedValue == "2")
            {
                item = new ListItem("PFNA", "2");
                drp_serie.Items.Add(item);
            }
        }
        else
        {
            string sql = "";
            sql = "  fac_suc_id=" + drp_sucursal.SelectedValue + "  and fac_tipo=" + drp_tipo_documento.SelectedValue + " and fac_conta_id=" + drp_contabilidad.SelectedValue + " and fac_operacion_id=" + drp_tipo_operacion.SelectedValue + " and fac_mon_id="+drp_moneda.SelectedValue+" ";
            arr = null;
            drp_serie.Items.Clear();
            item = new ListItem("Seleccione...", "0");
            drp_serie.Items.Add(item);
            arr = (ArrayList)DB.getSeriesByCriterio(user, sql);
            foreach (RE_GenericBean bean in arr)
            {
                item = new ListItem(bean.strC2, bean.strC2);
                drp_serie.Items.Add(item);
            }
            drp_serie.SelectedIndex = 0;
        }
    }
    protected void btn_limpiar_Click(object sender, EventArgs e)
    {
        drp_sistema.SelectedValue = "0";
        drp_linea_servicio.SelectedValue = "0";
        drp_empresa.SelectedValue = "0";
        drp_tipo_documento.SelectedValue = "0";
        drp_contabilidad.SelectedValue = "0";
        drp_moneda.SelectedValue = "0";
        drp_sucursal.SelectedValue = "0";
        drp_tipo_operacion.SelectedValue = "0";
        drp_serie.SelectedValue = "0";
    }
    protected void btn_guardar_configuracion_Click(object sender, EventArgs e)
    {
        if (drp_sistema.SelectedValue == "0")
        {
            WebMsgBox.Show("Debe seleccionar el Sistema a Configurar");
            drp_sistema.SelectedIndex = 0;
            return;
        }
        if (drp_linea_servicio.SelectedValue == "0")
        {
            WebMsgBox.Show("Debe seleccionar la Linea de Servicio a Configurar");
            drp_linea_servicio.SelectedIndex = 0;
            return;
        }
        if (drp_empresa.SelectedValue == "0")
        {
            WebMsgBox.Show("Debe seleccionar la Empresa a Configurar");
            drp_empresa.SelectedIndex = 0;
            return;
        }
        if (drp_tipo_documento.SelectedValue == "0")
        {
            WebMsgBox.Show("Debe seleccionar el Tipo de Documento a Configurar");
            drp_contabilidad.SelectedIndex = 0;
            return;
        }
        if (drp_contabilidad.SelectedValue == "0")
        {
            WebMsgBox.Show("Debe seleccionar la Contabilidad a Configurar");
            drp_contabilidad.SelectedIndex = 0;
            return;
        }
        if (drp_moneda.SelectedValue == "0")
        {
            WebMsgBox.Show("Debe seleccionar la Moneda a Configurar");
            drp_moneda.SelectedIndex = 0;
            return;
        }
        if (drp_sucursal.SelectedValue == "0")
        {
            WebMsgBox.Show("Debe seleccionar la Sucursal a Configurar");
            drp_sucursal.SelectedIndex = 0;
            return;
        }
        if (drp_tipo_operacion.SelectedValue == "0")
        {
            WebMsgBox.Show("Por Favor seleccione el Tipo de Operacion Configurar");
            return;
        }
        if (drp_serie.SelectedValue == "0")
        {
            WebMsgBox.Show("Por Favor seleccione la Serie a Configurar");
            return;
        }
        RE_GenericBean Bean_Configuracion = new RE_GenericBean();
        Bean_Configuracion.intC1 = int.Parse(drp_sistema.SelectedValue);
        Bean_Configuracion.intC2 = int.Parse(drp_linea_servicio.SelectedValue);
        Bean_Configuracion.intC3 = int.Parse(drp_empresa.SelectedValue);
        Bean_Configuracion.intC4 = int.Parse(drp_tipo_documento.SelectedValue);
        Bean_Configuracion.intC5 = int.Parse(drp_contabilidad.SelectedValue);
        Bean_Configuracion.intC6 = int.Parse(drp_moneda.SelectedValue);
        Bean_Configuracion.intC7 = int.Parse(drp_sucursal.SelectedValue);
        Bean_Configuracion.intC8 = int.Parse(drp_tipo_operacion.SelectedValue);
        Bean_Configuracion.strC1 = drp_serie.SelectedItem.Text;
        Bean_Configuracion.strC2 = user.ID;
        int bandera_existencia = 0;
        string sql = "";
        if ((Bean_Configuracion.intC3 != 6) && (Bean_Configuracion.intC3 != 25))
        {
            //Todas las Empresas que no sean de Panama
            sql = " and trcs_sis_id=" + Bean_Configuracion.intC1 + " and trcs_tto_id=" + Bean_Configuracion.intC2 + " and trcs_empresa_id=" + Bean_Configuracion.intC3 + " and trcs_ttr_id=" + Bean_Configuracion.intC4 + " and trcs_conta_id=" + Bean_Configuracion.intC5 + " and trcs_moneda_id=" + Bean_Configuracion.intC6 + " and trcs_tipo_operacion=" + Bean_Configuracion.intC8 + " ";
        }
        else
        {
            //Empresas de Panama
            sql = " and trcs_sis_id=" + Bean_Configuracion.intC1 + " and trcs_tto_id=" + Bean_Configuracion.intC2 + " and trcs_empresa_id=" + Bean_Configuracion.intC3 + " and trcs_ttr_id=" + Bean_Configuracion.intC4 + " and trcs_conta_id=" + Bean_Configuracion.intC5 + " and trcs_moneda_id=" + Bean_Configuracion.intC6 + " and trcs_tipo_operacion=" + Bean_Configuracion.intC8 + " and trcs_sucursal_id=" + Bean_Configuracion.intC7 + " ";
        }
        bandera_existencia = Contabilizacion_Automatica_CAD.Validar_Existencia_Configuracion_Series_Contabilizacion_Automatica(sql);
        if (bandera_existencia > 0)
        {
            WebMsgBox.Show("Configuracion Existente");
            return;
        }
        int result = 0;
        result = Contabilizacion_Automatica_CAD.Insertar_Configuracion_Series_Contabilizacion_Automatica(Bean_Configuracion);
        if (result == -100)
        {
            WebMsgBox.Show("Existio un erro al momento de Insertar la Configuracion");
            return;
        }
        else
        {
            WebMsgBox.Show("La Configuracion de la Serie fue guardada exitosamente");
            drp_sistema.SelectedValue = "0";
            drp_linea_servicio.SelectedValue = "0";
            drp_empresa.SelectedValue = "0";
            drp_tipo_documento.SelectedValue = "0";
            drp_contabilidad.SelectedValue = "0";
            drp_moneda.SelectedValue = "0";
            drp_sucursal.SelectedValue = "0";
            drp_tipo_operacion.SelectedValue = "0";
            drp_serie.SelectedValue = "0";
            return;
        }
    }
    protected void drp_moneda_SelectedIndexChanged(object sender, EventArgs e)
    {
        drp_sucursal.SelectedValue = "0";
        drp_tipo_operacion.SelectedValue = "0";
        drp_serie.Items.Clear();
        item = new ListItem("Seleccione...", "0");
        drp_serie.Items.Add(item);
        drp_serie.SelectedValue = "0";
    }
    public static DataTable Obtener_Detalle_Configuracion_Series_Contabilizacion_Automatica(string sql)
    {
        int correlativo = 1;
        DataTable dt = new DataTable();
        dt.Columns.Add("NO");
        dt.Columns.Add("ID");
        dt.Columns.Add("SISTEMA");
        dt.Columns.Add("TTO");
        dt.Columns.Add("EMPRESA");
        dt.Columns.Add("DOCUMENTO");
        dt.Columns.Add("CONTABILIDAD");
        dt.Columns.Add("MONEDA");
        dt.Columns.Add("SUCURSAL");
        dt.Columns.Add("SERIE");
        dt.Columns.Add("OPERACION");
        ArrayList Arr = Contabilizacion_Automatica_CAD.Obtener_Detalle_Configuracion_Series_Contabilizacion_Automatica(sql);
        if (Arr != null)
        {
            foreach (RE_GenericBean Bean in Arr)
            {
                object[] Obj_Arr = { correlativo.ToString(), Bean.strC1, Bean.strC3, Bean.strC5, Bean.strC7, Bean.strC9, Bean.strC11, Bean.strC13, Bean.strC15, Bean.strC17, Bean.strC18 };
                correlativo++;
                dt.Rows.Add(Obj_Arr);
            }
        }
        return dt;
    }
    protected void gv_series_configuradas2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int indice = e.RowIndex;
        int ID = int.Parse(gv_series_configuradas2.Rows[indice].Cells[2].Text.ToString());
        int resultado = Contabilizacion_Automatica_CAD.Eliminar_Configuracion_Series_Contabilizacion_Automatica(user, ID);
        if (resultado == -100)
        {
            WebMsgBox.Show("Existio un error al tratar de eliminar la configuracion");
            return;
        }
        else
        {
            gv_series_configuradas2.DataSource = Obtener_Detalle_Configuracion_Series_Contabilizacion_Automatica("");
            gv_series_configuradas2.DataBind();
            WebMsgBox.Show("Configuracion Eliminada Exitosamente");
            return;
        }
    }
    protected void btn_buscar_series_Click(object sender, EventArgs e)
    {

    }
}
