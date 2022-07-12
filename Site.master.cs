using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    UsuarioBean user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userSCA"] == null)
        {
            Response.Redirect("../Default.aspx");
        }
        user = (UsuarioBean)Session["userSCA"];
        lbl_nombre_empresa.Text = user.pais.Nombre_Sistema + "  -  TC:   " + user.pais.TipoCambio.ToString();
        Cargar_Menu();
    }
    protected void Cargar_Menu()
    {
        NavigationMenu.Items.Clear();
        ArrayList Arr_Opciones = DB.Get_Opciones_Padre_Menu();
        foreach (RE_GenericBean Opcion_Padre in Arr_Opciones)
        {
            MenuItem Item_Padre = new MenuItem(Opcion_Padre.strC2, Opcion_Padre.strC1, string.Empty, Opcion_Padre.strC4);
            MenuItem Item_Hijo = null;
            ArrayList Arr_Opciones_Hijas = DB.Get_Opciones_Hijas_Menu(user.PaisID, user.ID, int.Parse(Opcion_Padre.strC1));
            foreach (RE_GenericBean Opcion_Hija in Arr_Opciones_Hijas)
            {
                Item_Hijo = new MenuItem(Opcion_Hija.strC2, Opcion_Hija.strC1, string.Empty, Opcion_Hija.strC4);
                Item_Padre.ChildItems.Add(Item_Hijo);
            }
            if ((Opcion_Padre.strC1 == "1") ||(Item_Padre.ChildItems.Count > 0))
            {
                NavigationMenu.Items.Add(Item_Padre);
            }
        }
    }
    protected void btn_descargar_tutorial_Click(object sender, EventArgs e)
    {
        string FilePath = "D:\\VIDEO_TUTORIALES\\SCA\\VIDEO TUTORIAL - SCA.mp4";
        if (File.Exists(FilePath) == true)
        {
            #region Descargar Archivo
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=VIDEO TUTORIAL - SCA.mp4");
            Response.Flush();
            Response.WriteFile(FilePath);
            Response.End();
            #endregion
        }
    }
    protected void btn_descargar_tutorial2_Click(object sender, EventArgs e)
    {
        string FilePath = "D:\\VIDEO_TUTORIALES\\SCA\\MANUAL SCA.pdf";
        if (File.Exists(FilePath) == true)
        {
            #region Descargar Archivo
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment; filename=MANUAL SCA.pdf");
            Response.Flush();
            Response.WriteFile(FilePath);
            Response.End();
            #endregion
        }
    }
}
