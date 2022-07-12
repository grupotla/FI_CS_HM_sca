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
/// Summary description for opciones_perfil
/// </summary>
public class opciones_perfil
{
    private int pap_per_id = 0;
    private int pap_apl_id = 0;
    private int pap_decimal = 0;

	public opciones_perfil()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int pap_perID
    {
        get { return pap_per_id; }
        set { pap_per_id = value; }
    }
    public int pap_aplID
    {
        get { return pap_apl_id; }
        set { pap_apl_id = value; }
    }
    public int pap_Dec
    {
        get { return pap_decimal; }
        set { pap_decimal = value; }
    }
}
