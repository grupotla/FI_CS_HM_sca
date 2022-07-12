using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Collections;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for Rubros
/// </summary>
public class Poliza
{
    private string no_poliza = null;
    private string cuenta = null;
    private string desc_cuenta = null;
    private double cuentas_debe = 0;
    private double cuentas_haber = 0;
    
	public Poliza()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string partida
    {
        get { return no_poliza; }
        set { no_poliza = value; }
    }
    public string cta
    {
        get { return cuenta; }
        set { cuenta = value; }
    }
    public string desc_cta
    {
        get { return desc_cuenta; }
        set { desc_cuenta = value; }
    }
    public double cta_debe
    {
        get { return cuentas_debe; }
        set { cuentas_debe = value; }
    }
    public double cta_haber
    {
        get { return cuentas_haber; }
        set { cuentas_haber = value; }
    }
}
