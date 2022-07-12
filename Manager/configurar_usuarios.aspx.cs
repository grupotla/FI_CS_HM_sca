using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class Manager_configurar_usuarios : System.Web.UI.Page
{
    UsuarioBean user;
    ListItem item = null;
    ArrayList arr = new ArrayList();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userSCA"] == null)
        {
            Response.Redirect("../Default.aspx");
        }
        user = (UsuarioBean)Session["userSCA"];
        int opcion = DB.Validar_Opcion_Usuario(user, 18);
        if (opcion == 0)
        {
            Response.Redirect("~/Home.aspx");
        }
        if (!Page.IsPostBack)
        {
            Obtengo_Listas();
        }
    }
    protected void Obtengo_Listas()
    {
        drp_usuarios.Items.Clear();
        arr = (ArrayList)DB.getUsuarios("");
        item = null;
        item = new ListItem("Seleccione...", "0");
        drp_usuarios.Items.Add(item);
        foreach (UsuarioBean user in arr)
        {
            item = new ListItem(user.ID, user.ID);
            drp_usuarios.Items.Add(item);
        }
        drp_usuarios.SelectedValue = "0";

        arr = (ArrayList)DB.getPaises("");
        item = null;
        item = new ListItem("Seleccione...", "0");
        drp_empresas.Items.Add(item);
        foreach (PaisBean pais in arr)
        {
            item = new ListItem(pais.Nombre_Sistema, pais.ID.ToString());
            drp_empresas.Items.Add(item);
        }
        drp_empresas.SelectedValue = "0";

        arr = DB.Get_Tipos_Operacion_Con_Sistema();
        foreach (RE_GenericBean Bean_Tipo_Operacion in arr)
        {
            item = new ListItem(Bean_Tipo_Operacion.strC2 + " - " + Bean_Tipo_Operacion.strC1, Bean_Tipo_Operacion.intC1.ToString());
            chkbl_lineas_servicio.Items.Add(item);
        }
        arr = DB.Get_Opciones_Menu_By_Opcion(2);//Menu Contabilizacion
        foreach (RE_GenericBean Bean_Opcion in arr)
        {
            item = new ListItem(Bean_Opcion.strC2, Bean_Opcion.strC1);
            chkbl_opcion2.Items.Add(item);
        }
        arr = DB.Get_Opciones_Menu_By_Opcion(3);//Menu Tarifario
        foreach (RE_GenericBean Bean_Opcion in arr)
        {
            item = new ListItem(Bean_Opcion.strC2, Bean_Opcion.strC1);
            chkbl_opcion3.Items.Add(item);
        }
        arr = DB.Get_Opciones_Menu_By_Opcion(4);//Menu Administracion
        foreach (RE_GenericBean Bean_Opcion in arr)
        {
            item = new ListItem(Bean_Opcion.strC2, Bean_Opcion.strC1);
            chkbl_opcion4.Items.Add(item);
        }
        arr = DB.Get_Opciones_Menu_By_Opcion(5);//Menu Reportes
        foreach (RE_GenericBean Bean_Opcion in arr)
        {
            item = new ListItem(Bean_Opcion.strC2, Bean_Opcion.strC1);
            chkbl_opcion5.Items.Add(item);
        }
    }
    protected void btn_nueva_busqueda_Click(object sender, EventArgs e)
    {
        Limpiar_Pantalla();
    }
    protected void btn_guardar_Click(object sender, EventArgs e)
    {
        if (drp_usuarios.SelectedValue == "0")
        {
            WebMsgBox.Show("Por Favor Seleccione el usuario a configurar");
            return;
        }
        if (drp_empresas.SelectedValue == "0")
        {
            WebMsgBox.Show("Por Favor Seleccione la Empresa donde desea configurar el acceso");
            return;
        }
        if (rbl_estados.SelectedValue == "")
        {
            WebMsgBox.Show("Por Favor Seleccione el Estado del Usuario");
            return;
        }
        if (rbl_estados.SelectedValue == "1")
        {
            #region Activar Usuario
            int ban_existe = 0;
            ban_existe = DB.Validar_Usuario_Activo(drp_usuarios.SelectedItem.Text.Trim(), int.Parse(drp_empresas.SelectedValue));
            if (ban_existe == -100)
            {
                WebMsgBox.Show("Existio un error al Tratar de Validar la Existencia del Usuario");
                return;
            }
            else  if (ban_existe == 0)
            {
                int ban_crear = 0;
                ban_crear = DB.Crear_Usuario(drp_usuarios.SelectedItem.Text.Trim(), int.Parse(drp_empresas.SelectedValue), user);
                if (ban_crear == -100)
                {
                    WebMsgBox.Show("Existio un error al Tratar de Crear el Usuario");
                    return;
                }
                else
                {
                    ArrayList Arr_Lineas_Servicio = new ArrayList();
                    for (int a = 0; a < chkbl_lineas_servicio.Items.Count; a++)
                    {
                        if (chkbl_lineas_servicio.Items[a].Selected == true)
                        {
                            Arr_Lineas_Servicio.Add(chkbl_lineas_servicio.Items[a].Value);
                        }
                    }
                    DB.Insertar_Lineas_Servicio_Usuario(int.Parse(drp_empresas.SelectedValue), drp_usuarios.SelectedItem.Text, Arr_Lineas_Servicio, user);
                    ArrayList Arr_Opciones = new ArrayList();
                    for (int a = 0; a < chkbl_opcion2.Items.Count; a++)
                    {
                        if (chkbl_opcion2.Items[a].Selected == true)
                        {
                            Arr_Opciones.Add(chkbl_opcion2.Items[a].Value);
                        }
                    }
                    for (int b = 0; b < chkbl_opcion3.Items.Count; b++)
                    {
                        if (chkbl_opcion3.Items[b].Selected == true)
                        {
                            Arr_Opciones.Add(chkbl_opcion3.Items[b].Value);
                        }
                    }
                    for (int c = 0; c < chkbl_opcion4.Items.Count; c++)
                    {
                        if (chkbl_opcion4.Items[c].Selected == true)
                        {
                            Arr_Opciones.Add(chkbl_opcion4.Items[c].Value);
                        }
                    }
                    for (int d = 0; d < chkbl_opcion5.Items.Count; d++)
                    {
                        if (chkbl_opcion5.Items[d].Selected == true)
                        {
                            Arr_Opciones.Add(chkbl_opcion5.Items[d].Value);
                        }
                    }
                    DB.Insertar_Opciones_Usuario(int.Parse(drp_empresas.SelectedValue), drp_usuarios.SelectedItem.Text, Arr_Opciones, user);
                    Limpiar_Pantalla();
                    WebMsgBox.Show("El Usuario fue configurado exitosamente");
                    return;
                }
            }
            else if (ban_existe > 0)
            {
                ArrayList Arr_Lineas_Servicio = new ArrayList();
                for (int a = 0; a < chkbl_lineas_servicio.Items.Count; a++)
                {
                    if (chkbl_lineas_servicio.Items[a].Selected == true)
                    {
                        Arr_Lineas_Servicio.Add(chkbl_lineas_servicio.Items[a].Value);
                    }
                }
                DB.Insertar_Lineas_Servicio_Usuario(int.Parse(drp_empresas.SelectedValue), drp_usuarios.SelectedItem.Text, Arr_Lineas_Servicio, user);
                ArrayList Arr_Opciones = new ArrayList();
                for (int a = 0; a < chkbl_opcion2.Items.Count; a++)
                {
                    if (chkbl_opcion2.Items[a].Selected == true)
                    {
                        Arr_Opciones.Add(chkbl_opcion2.Items[a].Value);
                    }
                }
                for (int b = 0; b < chkbl_opcion3.Items.Count; b++)
                {
                    if (chkbl_opcion3.Items[b].Selected == true)
                    {
                        Arr_Opciones.Add(chkbl_opcion3.Items[b].Value);
                    }
                }
                for (int c = 0; c < chkbl_opcion4.Items.Count; c++)
                {
                    if (chkbl_opcion4.Items[c].Selected == true)
                    {
                        Arr_Opciones.Add(chkbl_opcion4.Items[c].Value);
                    }
                }
                for (int d = 0; d < chkbl_opcion5.Items.Count; d++)
                {
                    if (chkbl_opcion5.Items[d].Selected == true)
                    {
                        Arr_Opciones.Add(chkbl_opcion5.Items[d].Value);
                    }
                }
                DB.Insertar_Opciones_Usuario(int.Parse(drp_empresas.SelectedValue), drp_usuarios.SelectedItem.Text, Arr_Opciones, user);
                Limpiar_Pantalla();
                WebMsgBox.Show("El Usuario fue configurado exitosamente");
                return;
            }
            #endregion
        }
        else if (rbl_estados.SelectedValue == "0")
        {
            #region InActivar Usuario
            int ban_existe = 0;
            ban_existe = DB.Validar_Usuario_Activo(drp_usuarios.SelectedItem.Text.Trim(), int.Parse(drp_empresas.SelectedValue));
            if (ban_existe == -100)
            {
                WebMsgBox.Show("Existio un error al Tratar de Validar la Existencia del Usuario");
                return;
            }
            else if (ban_existe == 0)
            {
                WebMsgBox.Show("Usuario inexistente, no se puede eliminar");
                return;
            }
            else if (ban_existe > 0)
            {
                ArrayList Arr_Lineas_Servicio = new ArrayList();
                DB.Insertar_Lineas_Servicio_Usuario(int.Parse(drp_empresas.SelectedValue), drp_usuarios.SelectedItem.Text, Arr_Lineas_Servicio, user);
                ArrayList Arr_Opciones = new ArrayList();
                DB.Insertar_Opciones_Usuario(int.Parse(drp_empresas.SelectedValue), drp_usuarios.SelectedItem.Text, Arr_Opciones, user);
                int ban_eliminar = 0;
                ban_eliminar = DB.Eliminar_Usuario(drp_usuarios.SelectedItem.Text.Trim(), int.Parse(drp_empresas.SelectedValue), user);
                if (ban_eliminar == -100)
                {
                    WebMsgBox.Show("Existio un error al Tratar de Eliminar el Usuario");
                    return;
                }
                else if (ban_eliminar > 0)
                {
                    WebMsgBox.Show("El usuario fue Eliminado Exitosamente");
                    Limpiar_Pantalla();
                    return;
                }
            }
            #endregion
        }
    }
    protected void btn_buscar_Click(object sender, EventArgs e)
    {
        Limpiar_CheckBox_Lits();
        if (drp_usuarios.SelectedValue == "0")
        {
            WebMsgBox.Show("Por Favor Seleccione el usuario a configurar");
            return;
        }
        if (drp_empresas.SelectedValue == "0")
        {
            WebMsgBox.Show("Por Favor Seleccione la Empresa donde desea configurar el acceso");
            return;
        }
        int ban_existe = 0;
        ban_existe = DB.Validar_Usuario_Activo(drp_usuarios.SelectedItem.Text.Trim(), int.Parse(drp_empresas.SelectedValue));
        if (ban_existe == -100)
        {
            WebMsgBox.Show("Existio un error al Tratar de Validar la Existencia del Usuario");
            return;
        }
        else if (ban_existe > 0)
        {
            rbl_estados.SelectedValue = "1";
            string ttoID = "";
            ArrayList Arr_Lineas_Servicio = (ArrayList)DB.Get_Lineas_Servicio_Configuradas(int.Parse(drp_empresas.SelectedValue), drp_usuarios.SelectedItem.Text);
            for (int a = 0; a < chkbl_lineas_servicio.Items.Count; a++)
            {
                chkbl_lineas_servicio.Items[a].Selected = false;
                ttoID = chkbl_lineas_servicio.Items[a].Value;
                foreach (string LineaID in Arr_Lineas_Servicio)
                {
                    if (ttoID == LineaID)
                    {
                        chkbl_lineas_servicio.Items[a].Selected = true;
                    }
                }
            }

            ArrayList Arr_Opciones = new ArrayList();
            string opcionID = "";
            Arr_Opciones = DB.Get_Opciones_Usuario(int.Parse(drp_empresas.SelectedValue), drp_usuarios.SelectedItem.Text, 2);
            for (int a = 0; a < chkbl_opcion2.Items.Count; a++)
            {
                chkbl_opcion2.Items[a].Selected = false;
                opcionID = chkbl_opcion2.Items[a].Value;
                foreach (string _opcion in Arr_Opciones)
                {
                    if (opcionID == _opcion)
                    {
                        chkbl_opcion2.Items[a].Selected = true;
                    }
                }
            }
            Arr_Opciones = null;
            opcionID = "";
            Arr_Opciones = DB.Get_Opciones_Usuario(int.Parse(drp_empresas.SelectedValue), drp_usuarios.SelectedItem.Text, 3);
            for (int a = 0; a < chkbl_opcion3.Items.Count; a++)
            {
                chkbl_opcion3.Items[a].Selected = false;
                opcionID = chkbl_opcion3.Items[a].Value;
                foreach (string _opcion in Arr_Opciones)
                {
                    if (opcionID == _opcion)
                    {
                        chkbl_opcion3.Items[a].Selected = true;
                    }
                }
            }
            Arr_Opciones = null;
            opcionID = "";
            Arr_Opciones = DB.Get_Opciones_Usuario(int.Parse(drp_empresas.SelectedValue), drp_usuarios.SelectedItem.Text, 4);
            for (int a = 0; a < chkbl_opcion4.Items.Count; a++)
            {
                chkbl_opcion4.Items[a].Selected = false;
                opcionID = chkbl_opcion4.Items[a].Value;
                foreach (string _opcion in Arr_Opciones)
                {
                    if (opcionID == _opcion)
                    {
                        chkbl_opcion4.Items[a].Selected = true;
                    }
                }
            }
            Arr_Opciones = null;
            opcionID = "";
            Arr_Opciones = DB.Get_Opciones_Usuario(int.Parse(drp_empresas.SelectedValue), drp_usuarios.SelectedItem.Text, 5);
            for (int a = 0; a < chkbl_opcion5.Items.Count; a++)
            {
                chkbl_opcion5.Items[a].Selected = false;
                opcionID = chkbl_opcion5.Items[a].Value;
                foreach (string _opcion in Arr_Opciones)
                {
                    if (opcionID == _opcion)
                    {
                        chkbl_opcion5.Items[a].Selected = true;
                    }
                }
            }

        }
    }
    protected void Limpiar_Pantalla()
    {
        drp_usuarios.SelectedValue = "0";
        drp_empresas.SelectedValue = "0";
        rbl_estados.SelectedIndex = -1;
        for (int a = 0; a < chkbl_lineas_servicio.Items.Count; a++)
        {
            if (chkbl_lineas_servicio.Items[a].Selected == true)
            {
                chkbl_lineas_servicio.Items[a].Selected = false;
            }
        }
        for (int a = 0; a < chkbl_opcion2.Items.Count; a++)
        {
            if (chkbl_opcion2.Items[a].Selected == true)
            {
                chkbl_opcion2.Items[a].Selected = false;
            }
        }
        for (int b = 0; b < chkbl_opcion3.Items.Count; b++)
        {
            if (chkbl_opcion3.Items[b].Selected == true)
            {
                chkbl_opcion3.Items[b].Selected = false;
            }
        }
        for (int c = 0; c < chkbl_opcion4.Items.Count; c++)
        {
            if (chkbl_opcion4.Items[c].Selected == true)
            {
                chkbl_opcion4.Items[c].Selected = false;
            }
        }
        for (int d = 0; d < chkbl_opcion5.Items.Count; d++)
        {
            if (chkbl_opcion5.Items[d].Selected == true)
            {
                chkbl_opcion5.Items[d].Selected = false;
            }
        }
    }
    protected void Limpiar_CheckBox_Lits()
    {
        rbl_estados.SelectedIndex = -1;
        for (int a = 0; a < chkbl_lineas_servicio.Items.Count; a++)
        {
            if (chkbl_lineas_servicio.Items[a].Selected == true)
            {
                chkbl_lineas_servicio.Items[a].Selected = false;
            }
        }
        for (int a = 0; a < chkbl_opcion2.Items.Count; a++)
        {
            if (chkbl_opcion2.Items[a].Selected == true)
            {
                chkbl_opcion2.Items[a].Selected = false;
            }
        }
        for (int b = 0; b < chkbl_opcion3.Items.Count; b++)
        {
            if (chkbl_opcion3.Items[b].Selected == true)
            {
                chkbl_opcion3.Items[b].Selected = false;
            }
        }
        for (int c = 0; c < chkbl_opcion4.Items.Count; c++)
        {
            if (chkbl_opcion4.Items[c].Selected == true)
            {
                chkbl_opcion4.Items[c].Selected = false;
            }
        }
        for (int d = 0; d < chkbl_opcion5.Items.Count; d++)
        {
            if (chkbl_opcion5.Items[d].Selected == true)
            {
                chkbl_opcion5.Items[d].Selected = false;
            }
        }
    }
}

