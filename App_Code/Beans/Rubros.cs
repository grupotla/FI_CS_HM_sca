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
public class Rubros
{
    private long rubro_id = 0;
    private string rubro_name = "";
    private double total = 0;
    private double totalD = 0;
    private double valor_prepaid = 0;
    private double valor_collect = 0;
    private int cobraiva = 0;
    private int ivaincluido = 0;
    private int nosujeto = 0;
    private double impuesto = 0;
    private double subtotal = 0;
    private double equivalente = 0;
    private long tipo_moneda = 0;
    private string Tipo = "";
    private int tipo_serv = 0;
    private ArrayList cuentas_debe = null;
    private ArrayList cuentas_haber = null;
    private string rubro_commentario="";
    private int operacion = 0;
    private int sistema = 0;
    private int costoid = 0;
    private int bl_id = 0;
    private int Tipo_Cargo = 0;// Cargo Local=0 Cargo Internacional=1
    private string Ruteado = "";
    private int Routing_ID;
    private string Numero_Routing;
    private int IncotermID;
    private string Incoterm;
    private int FacturaID;
    private int TipoDocumeto;
    private double CargoID = 0;
    private double CostoID = 0;
    private string import_export;
    private int tipo_contribuyente = 0;
	public Rubros()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public ArrayList cta_debe {
        get { return cuentas_debe; }
        set { cuentas_debe = value; }
    }
    public ArrayList cta_haber
    {
        get { return cuentas_haber; }
        set { cuentas_haber = value; }
    }

    public long rubroID {
        get { return rubro_id; }
        set { rubro_id = value; }
    }
    public string rubroName {
        get { return rubro_name; }
        set { rubro_name = value; }
    }
    public string rubroCommentario {
        get { return rubro_commentario; }
        set { rubro_commentario = value; }
    }
    public string rubtoType
    {
        get { return Tipo; }
        set { Tipo = value; }
    }
    public int rubroTypeID
    {
        get { return tipo_serv; }
        set { tipo_serv = value; }
    }
    public double rubroTot
    {
        get { return total; }
        set { total = value; }
    }
    public double rubroTotD
    {
        get { return totalD; }
        set { totalD = value; }
    }
    public double rubroImpuesto
    {
        get { return impuesto; }
        set { impuesto = value; }
    }
    public double rubroSubTot
    {
        get { return subtotal; }
        set { subtotal = value; }
    }
    public double rubroEquivalente
    {
        get { return equivalente; }
        set { equivalente = value; }
    }

    public int CobIva
    {
        get { return cobraiva; }
        set { cobraiva = value; }
    }
    public int IvaInc
    {
        get { return ivaincluido; }
        set { ivaincluido = value; }
    }
    public long rubroMoneda
    {
        get { return tipo_moneda; }
        set { tipo_moneda = value; }
    }
    public int NoSujeto
    {
        get { return nosujeto; }
        set { nosujeto = value; }
    }
    public int rubroOperacion
    {
        get { return operacion; }
        set { operacion = value; }
    }
    public int rubroSistema
    {
        get { return sistema; }
        set { sistema = value; }
    }
    public int rubroCostoID
    {
        get { return costoid; }
        set { costoid = value; }
    }
    public int rubroBlID
    {
        get { return bl_id; }
        set { bl_id = value; }
    }
    public int rubroTipoCargo
    {
        get { return Tipo_Cargo; }
        set { Tipo_Cargo = value; }
    }
    public string rubroRuteado
    {
        get { return Ruteado; }
        set { Ruteado = value; }
    }
    public int rubroRoutingID
    {
        get { return Routing_ID; }
        set { Routing_ID = value; }
    }
    public string rubroNumeroRouting
    {
        get { return Numero_Routing; }
        set { Numero_Routing = value; }
    }
    public int rubroIncotermID
    {
        get { return IncotermID; }
        set { IncotermID = value; }
    }
    public string rubroIncoterm
    {
        get { return Incoterm; }
        set { Incoterm = value; }
    }
    public int rubroFacturaID
    {
        get { return FacturaID; }
        set { FacturaID = value; }
    }
    public int rubroTipoDocumento
    {
        get { return TipoDocumeto; }
        set { TipoDocumeto = value; }
    }
    public double rubroCargoID
    {
        get { return CargoID; }
        set { CargoID = value; }
    }
    public double RubroCostoID
    {
        get { return CostoID; }
        set { CostoID = value; }
    }
    public double Valor_Prepaid
    {
        get { return valor_prepaid; }
        set { valor_prepaid = value; }
    }
    public double Valor_Collect
    {
        get { return valor_collect; }
        set { valor_collect = value; }
    }
    public string Impor_Export
    {
        get { return import_export; }
        set { import_export = value; }
    }
    public int rubroTipo_Contribuyente
    {
        get { return tipo_contribuyente; }
        set { tipo_contribuyente = value; }
    }
}
