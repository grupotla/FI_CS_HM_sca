using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_aceptar_Click(object sender, EventArgs e)
    {
        Session["userSCA"] = null;
        Session.Abandon();
        Response.Redirect("~/Default.aspx");
    }
    protected void btn_cancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Home.aspx");
    }
}