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
/// Summary description for UsuarioBean
/// </summary>
public class UsuarioBean
{
    private string usu_id = "";
    private string usu_pss = "";
    private string usu_nombre = "";
    private int usu_paisID = 0;
    private int usu_no = 0;
    private int usu_sucursalID = 0;
    private int usu_estado = 0;
    private string usu_fechacreacion = "";
    private string usu_paisNombre = "";
    private string usu_sucursalNombre = "";
    private string usu_printerNanem = "";
    private ArrayList usu_perfil = new ArrayList();
    private ArrayList usu_depto = new ArrayList();
    private Hashtable usu_aplicaciones = new Hashtable();
    private Hashtable usu_operaciones = new Hashtable();
    private PaisBean usu_pais = new PaisBean();
    private Impresion_Bean usu_impresion = new Impresion_Bean();
    private int usu_contaID = 0;
    private int usu_Operacion = 0;
    private int usu_Moneda = 0;
    private int usu_Idioma = 0;
    private string usu_fecha_corte_gt;
    private string usu_fecha_corte_ni;
    private string usu_fecha_corte_cr;
    private string usu_fecha_corte_sv;
    private string usu_fecha_corte_hn;
    private string usu_fecha_corte_pa;
    private string usu_fecha_corte_bz;
    private string usu_fecha_corte_sv2;
    private bool usu_suc_es_apl = false;
	public UsuarioBean()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string ID
    {
        get { return usu_id; }
        set { usu_id = value; }
    }
    public string Contrasena
    {
        get { return usu_pss; }
        set { usu_pss = value; }
    }
    public string Nombre
    {
        get { return usu_nombre; }
        set { usu_nombre = value; }
    }
    public int No
    {
        get { return usu_no; }
        set { usu_no = value; }
    }
    public int PaisID
    {
        get { return usu_paisID; }
        set { usu_paisID = value; }
    }
    public int SucursalID
    {
        get { return usu_sucursalID; }
        set { usu_sucursalID = value; }
    }
    public int Estado
    {
        get { return usu_estado; }
        set { usu_estado = value; }
    }
    public string Fecha
    {
        get { return usu_fechacreacion; }
        set { usu_fechacreacion = value; }
    }
    public string PaisNombre
    {
        get { return usu_paisNombre; }
        set { usu_paisNombre = value; }
    }
    public string SucursalNombre
    {
        get { return usu_sucursalNombre; }
        set { usu_sucursalNombre = value; }
    }
    public ArrayList Perfil
    {
        get { return usu_perfil; }
        set { usu_perfil = value; }
    }
    public ArrayList Departamento
    {
        get { return usu_depto; }
        set { usu_depto = value; }
    }
    public Hashtable Aplicaciones
    {
        get { return usu_aplicaciones; }
        set { usu_aplicaciones = value; }
    }

    public PaisBean pais
    {
        get { return usu_pais; }
        set { usu_pais = value; }
    }
    public Impresion_Bean ImpresionBean
    {
        get { return usu_impresion; }
        set { usu_impresion = value; }
    }
    public int contaID
    {
        get { return usu_contaID; }
        set { usu_contaID = value; }
    }
    public int Operacion
    {
        get { return usu_Operacion; }
        set { usu_Operacion = value; }
    }
    public string PrinterName
    {
        get { return usu_printerNanem; }
        set { usu_printerNanem = value; }
    }
    public int Moneda
    {
        get { return usu_Moneda; }
        set { usu_Moneda = value; }
    }
    public int Idioma
    {
        get { return usu_Idioma; }
        set { usu_Idioma = value; }
    }
    public string fecha_corte_gt
    {
        get { return usu_fecha_corte_gt; }
        set { usu_fecha_corte_gt = value; }
    }
    public string fecha_corte_ni
    {
        get { return usu_fecha_corte_ni; }
        set { usu_fecha_corte_ni = value; }
    }
    public string fecha_corte_sv
    {
        get { return usu_fecha_corte_sv; }
        set { usu_fecha_corte_sv = value; }
    }
    public string fecha_corte_hn
    {
        get { return usu_fecha_corte_hn; }
        set { usu_fecha_corte_hn = value; }
    }
    public string fecha_corte_cr
    {
        get { return usu_fecha_corte_cr; }
        set { usu_fecha_corte_cr = value; }
    }
    public string fecha_corte_pa
    {
        get { return usu_fecha_corte_pa; }
        set { usu_fecha_corte_pa = value; }
    }
    public string fecha_corte_bz
    {
        get { return usu_fecha_corte_bz; }
        set { usu_fecha_corte_bz = value; }
    }
    public string fecha_corte_sv2
    {
        get { return usu_fecha_corte_sv2; }
        set { usu_fecha_corte_sv2 = value; }
    }
    public bool Sucursal_Es_APL
    {
        get { return usu_suc_es_apl; }
        set { usu_suc_es_apl = value; }
    }
}
