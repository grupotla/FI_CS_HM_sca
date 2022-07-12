using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class Definir_Empresa : System.Web.UI.Page
{
    UsuarioBean user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userSCA"] == null)
        {
            //Response.Redirect("../Default.aspx");
            Response.Redirect("Default.aspx");
        }
        user = (UsuarioBean)Session["userSCA"];
        if (!Page.IsPostBack)
        {
            Obtengo_Listas();
        }
    }
    protected void Obtengo_Listas()
    {
        ArrayList arr = (ArrayList)DB.getPaises("");
        ArrayList Arr_Empresas_Configuradas = (ArrayList)DB.Get_Empresas_Configuradas(user.ID);
        drp_empresas.Items.Clear();
        ListItem item = new ListItem("Seleccione...", "0");
        drp_empresas.Items.Add(item);
        foreach (PaisBean pais in arr)
        {

            for (int a = 0; a < Arr_Empresas_Configuradas.Count; a++)
            {
                int empresaID = int.Parse(Arr_Empresas_Configuradas[a].ToString());
                if (empresaID == pais.ID)
                {
                    item = new ListItem(pais.Nombre_Sistema, pais.ID.ToString());
                    drp_empresas.Items.Add(item);
                }
            }
        }
        drp_empresas.SelectedIndex = 0;
    }
    protected void btn_siguiente_Click(object sender, EventArgs e)
    {
        if (drp_empresas.Items.Count == 1)
        {
            WebMsgBox.Show("Su usuario no tiene Empresas configuradas en el SCA");
            return;
        }
        if (drp_empresas.SelectedValue != "0")
        {
            int paisID = int.Parse(drp_empresas.SelectedValue);
            user = (UsuarioBean)Session["userSCA"];
            user.PaisID = paisID;
            user.pais = (PaisBean)DB.getPais(paisID);
            decimal TipoCambio = DB.getTipoCambioHoy(paisID);
            if (TipoCambio == 0)
            {
                WebMsgBox.Show("No se ha ingresado el Tipo de Cambio para el dia de hoy en la Empresa.: " + drp_empresas.SelectedItem.Text + ", por favor solicite su ingreso al Departamento de Contabilidad");
                return;
            }
            Session["userSCA"] = user;
            Response.Redirect("~/Home.aspx");
        }
        else if (drp_empresas.SelectedValue == "0")
        {
            WebMsgBox.Show("Por Favor seleccione la Empresa a la que desea ingresar");
            return;
        }
    }
    protected void btn_salir_Click(object sender, EventArgs e)
    {
        Session["usuario"] = null;
        Response.Redirect("~/Default.aspx");
    }
}