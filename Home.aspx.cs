using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class reconciliacion_carga_Default : System.Web.UI.Page
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
    }
}