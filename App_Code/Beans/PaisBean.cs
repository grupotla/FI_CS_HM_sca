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
/// Summary description for PaisBean
/// </summary>
public class PaisBean
{
    private int pai_id = 0;
    private string pai_nombre = "";
    private int pai_diasimpuesto = 0;
    private int pai_mesesimpuesto = 0;
    private int pai_diasaholgura = 0;
    private int pai_grupo_empresas = 0;
    private decimal pai_porcinteres = 0;
    private decimal pai_momentoretencion = 0;
    private string pai_lista_correos = "";
    private decimal pai_tipocambio = 0;
    private decimal pai_impuesto = 0;
    private string pai_cobrar = "";//cuentas por cobrar
    private string pai_paga = "";//cuentas por pagar
    private string pai_recibo = "";
    //private string pai_agente = "";//cuent agentes
    //private string pai_banco = ""; 
    private string pai_anticipo_clientes = ""; // cuenta asociada a los anticipos de clientes
    private string pai_iva_cobra = "";//iva por cobrar
    private string pai_iva_paga = "";//iva por pagar
    private bool pai_nit = true;
    private bool pai_agente = true;
    private string pai_dif_cambiario = "";//cuenta contable para intercompany
    private string pai_intercompany = "";//cuenta contable para intercompany
    private string pai_intercompanysv = "";//cuenta contable para intercompany
    private string pai_intercompanyhn = "";//cuenta contable para intercompany
    private string pai_intercompanyni = "";//cuenta contable para intercompany
    private string pai_intercompanycr = "";//cuenta contable para intercompany
    private string pai_intercompanypa = "";//cuenta contable para intercompany
    private string pai_intercompanybz = "";//cuenta contable para intercompany
    private string pai_intercompanygrh = "";//cuenta contable para intercompany
    private string pai_intercompanymayanl = "";//cuenta contable para intercompany
    private string pai_schema = "";//esquema a donde va a consultar
    private string pai_iso = "";
    private string usu_imagepath = "";
    private string usu_direccion_empresa = "";
    private string usu_formas_pago = "";
    private string pai_numero_identificacion_tributaria = "";
    private string pai_cod_regimen = "";
    private string pai_nombre_sistema = "";
    private string pai_schema_apl = "";
	public PaisBean()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int diasimpuesto
    {
        get { return pai_diasimpuesto; }
        set { pai_diasimpuesto = value; }
    }
    public int diasholgura
    {
        get { return pai_diasaholgura; }
        set { pai_diasaholgura = value; }
    }
    public int Grupo_Empresas
    {
        get { return pai_grupo_empresas; }
        set { pai_grupo_empresas = value; }
    }
    public decimal porcinteres
    {
        get { return pai_porcinteres; }
        set { pai_porcinteres = value; }
    }
    public decimal momentoretencion
    {
        get { return pai_momentoretencion; }
        set { pai_momentoretencion = value; }
    }
    public string lista_correos
    {
        get { return pai_lista_correos; }
        set { pai_lista_correos = value; }
    }    
    public int ID
    {
        get { return pai_id; }
        set { pai_id = value; }
    }
    public string ISO
    {
        get { return pai_iso; }
        set { pai_iso = value; }
    }
    public string Nombre
    {
        get { return pai_nombre; }
        set { pai_nombre = value; }
    }
    public string ctaCobra
    {
        get { return pai_cobrar; }
        set { pai_cobrar = value; }
    }
    public string ctaPaga
    {
        get { return pai_paga; }
        set { pai_paga = value; }
    }
    public string ivaCobra
    {
        get { return pai_iva_cobra; }
        set { pai_iva_cobra = value; }
    }
    public string ivaPaga
    {
        get { return pai_iva_paga; }
        set { pai_iva_paga = value; }
    }
    public string Recibo
    {
        get { return pai_recibo; }
        set { pai_recibo = value; }
    }
    //public string ctaAgente
    //{
    //    get { return pai_agente; }
    //    set { pai_agente = value; }
    //}
    //public string ctaBanco
    //{
    //    get { return pai_banco; }
    //    set { pai_banco = value; }
    //}
    public string ctaAnticipoCli
    {
        get { return pai_anticipo_clientes; }
        set { pai_anticipo_clientes = value; }
    }
    public string ctaDifCambiario
    {
        get { return pai_dif_cambiario; }
        set { pai_dif_cambiario = value; }
    }
    public bool ctaNit
    {
        get { return pai_nit; }
        set { pai_nit = value; }
    }
    public string ctaIntercompany
    {
        get { return pai_intercompany; }
        set { pai_intercompany = value; }
    }
    public string schema
    {
        get { return pai_schema; }
        set { pai_schema = value; }
    }
    public string Pai_Cod_Regimen
    {
        get { return pai_cod_regimen; }
        set { pai_cod_regimen = value; }
    }
    public string Nombre_Sistema
    {
        get { return pai_nombre_sistema; }
        set { pai_nombre_sistema = value; }
    }
    public decimal TipoCambio
    {
        get { return pai_tipocambio; }
        set { pai_tipocambio = value; }
    }
    public decimal Impuesto
    {
        get { return pai_impuesto; }
        set { pai_impuesto = value; }
    }
    public string ctaIntercompanySV
    {
        get { return pai_intercompanysv; }
        set { pai_intercompanysv = value; }
    }
    public string ctaIntercompanyHN
    {
        get { return pai_intercompanyhn; }
        set { pai_intercompanyhn = value; }
    }
    public string ctaIntercompanyNI
    {
        get { return pai_intercompanyni; }
        set { pai_intercompanyni = value; }
    }
    public string ctaIntercompanyCR
    {
        get { return pai_intercompanycr; }
        set { pai_intercompanycr = value; }
    }
    public string ctaIntercompanyPA
    {
        get { return pai_intercompanypa; }
        set { pai_intercompanypa = value; }
    }
    public string ctaIntercompanyBZ
    {
        get { return pai_intercompanybz; }
        set { pai_intercompanybz = value; }
    }
    public string ctaIntercompanyGRH
    {
        get { return pai_intercompanygrh; }
        set { pai_intercompanygrh = value; }
    }
    public string ctaIntercompanyMayanL
    {
        get { return pai_intercompanymayanl; }
        set { pai_intercompanymayanl = value; }
    }
    public string Imagepath
    {
        get { return usu_imagepath; }
        set { usu_imagepath = value; }
    }
    public string Direccion_Empresa
    {
        get { return usu_direccion_empresa; }
        set { usu_direccion_empresa = value; }
    }
    public string Formas_Pago
    {
        get { return usu_formas_pago; }
        set { usu_formas_pago = value; }
    }
    public bool Pai_Agente
    {
        get { return pai_agente; }
        set { pai_agente = value; }
    }
    public string Pai_Numero_Identificacion_Tributaria
    {
        get { return pai_numero_identificacion_tributaria; }
        set { pai_numero_identificacion_tributaria = value; }
    }
    public int MesesImpuesto
    {
        get { return pai_mesesimpuesto; }
        set { pai_mesesimpuesto = value; }
    }
    public string schema_apl
    {
        get { return pai_schema_apl; }
        set { pai_schema_apl = value; }
    }
}
