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

/// <summary>
/// Summary description for OpAppBean
/// </summary>
public class OpAppBean
{
    private int op_id = 0;
    private string op_nombre = "";
    private string op_descripcion = "";
    private int op_decimal = 0;

	public OpAppBean()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int opID
    {
        get { return op_id; }
        set { op_id = value; }
    }
    public string opnombre
    {
        get { return op_nombre; }
        set { op_nombre = value; }
    }
    public string opdescripcion
    {
        get { return op_descripcion; }
        set { op_descripcion = value; }
    }
    public int opdecimal
    {
        get { return op_decimal; }
        set { op_decimal = value; }
    }
}
