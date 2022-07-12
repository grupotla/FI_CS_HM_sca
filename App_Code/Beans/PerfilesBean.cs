using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;

/// <summary>
/// Summary description for PerfilesBean
/// </summary>
public class PerfilesBean
{
    private int per_id = 0;
    private string per_nombre = "";
    private string per_descripcion = "";
    private ArrayList per_app;
	public PerfilesBean()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int ID
    {
        get { return per_id; }
        set { per_id = value; }
    }
    public string Nombre
    {
        get { return per_nombre; }
        set { per_nombre = value; }
    }
    public string Descripcion
    {
        get { return per_descripcion; }
        set { per_descripcion = value; }
    }
    public ArrayList Apparr
    {
        get { return per_app; }
        set { per_app = value; }
    }
}
