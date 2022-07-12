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
/// Summary description for Impresion_Bean
/// </summary>
public class Impresion_Bean
{
	public Impresion_Bean()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string operacion = "";//1 Impresion, 2 Reimpresion
    public string Operacion
    {
        get { return operacion; }
        set { operacion = value; }
    }
    private string tipo_documento = "";//sys_tipo_referencia
    public string Tipo_Documento
    {
        get { return tipo_documento; }
        set { tipo_documento = value; }
    }
    private string id = "";//ID_Documento
    public string Id
    {
        get { return id; }
        set { id = value; }
    }
    private bool impreso;//True or False
    public bool Impreso
    {
        get { return impreso; }
        set { impreso = value; }
    }
    private string printername = "";//Nombre de la Impresora
    public string PrinterName
    {
        get { return printername; }
        set { printername = value; }
    }
}
