using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for SucursalBean
/// </summary>
public class SucursalBean
{
    private int suc_id = 0;
    private int suc_pai_id = 0;
    private string suc_nombre = "";
    private string suc_nombre_comercial = "";
    private string suc_departamento = "";
    private string suc_municipio = "";
    private string suc_direccion = "";
    private string suc_codigo_postal = "";
    private string suc_codigo_establecimiento = "";
    private string suc_dispositivo_electronico = "";
    private string suc_pais_nombre = "";
    private bool suc_es_apl = false;
    private ArrayList factura_arr = null;
	public SucursalBean()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int ID
    {
        get { return suc_id; }
        set { suc_id = value; }
    }
    public int paisID
    {
        get { return suc_pai_id; }
        set { suc_pai_id = value; }
    }
    public string paisNombre
    {
        get { return suc_pais_nombre; }
        set { suc_pais_nombre = value; }
    }
    public string Nombre
    {
        get { return suc_nombre; }
        set { suc_nombre = value; }
    }
    public ArrayList fac_arr
    {
        get { return factura_arr; }
        set { factura_arr = value; }
    }
    public string Nombre_Comercial
    {
        get { return suc_nombre_comercial; }
        set { suc_nombre_comercial = value; }
    }
    public string Departamento
    {
        get { return suc_departamento; }
        set { suc_departamento = value; }
    }
    public string Municipio
    {
        get { return suc_municipio; }
        set { suc_municipio = value; }
    }
    public string Direccion
    {
        get { return suc_direccion; }
        set { suc_direccion = value; }
    }
    public string Codigo_Postal
    {
        get { return suc_codigo_postal; }
        set { suc_codigo_postal = value; }
    }
    public string Codigo_Establecimiento
    {
        get { return suc_codigo_establecimiento; }
        set { suc_codigo_establecimiento = value; }
    }
    public string Dispositivo_Electronico
    {
        get { return suc_dispositivo_electronico; }
        set { suc_dispositivo_electronico    = value; }
    }
    public bool Es_APL
    {
        get { return suc_es_apl; }
        set { suc_es_apl = value; }
    }
}
