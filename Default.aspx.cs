using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["userSCA"] = null;
        //Response.Redirect("http://10.10.1.10/login/login.aspx");
    }
    protected void btn_ingresar_Click(object sender, EventArgs e)
    {
        string user = tb_usuario.Text.Trim();
        string pass = Utility.cifrado(tb_password.Text.Trim());
        UsuarioBean usuario = (UsuarioBean)DB.ValidaCliente(user, tb_password.Text.Trim());
        if (usuario != null)
        {
            lbl_error.Visible = false;
            Session["userSCA"] = usuario;
            Response.Redirect("~/definir_empresa.aspx");
        }
        else
        {
            lbl_error.Visible = true;
            lbl_error.Text = "Usuario Invalido";
            return;
        }
        tb_usuario.Focus();
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