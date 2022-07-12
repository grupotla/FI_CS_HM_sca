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
/// Summary description for ReportBean
/// </summary>
public class ReportBean
{
    private string cue_id = "";
    private string nombre = "";
    private string cue_madre = "";
    private string nivel1 = "";
    private string nivel2 = "";
    private string nivel3 = "";
    private string nivel4 = "";
    private string nivel5 = "";
    private int cue_nivel = 0;
    private int cue_clasificacion = 0;
    private decimal debe = 0;
    private decimal haber = 0;
    private decimal debe1 = 0;
    private decimal haber1 = 0;
    private decimal debegt = 0;
    private decimal habergt = 0;
    private decimal debegtLT = 0;
    private decimal habergtLT = 0;
    private decimal debegtISI = 0;
    private decimal habergtISI = 0;
    private decimal debeEs = 0;
    private decimal haberEs = 0;
    private decimal debeEsLT = 0;
    private decimal haberEsLT = 0;
    private decimal debeEs2 = 0;
    private decimal haberEs2 = 0;
    private decimal debeHn = 0;
    private decimal haberHn = 0;
    private decimal debeHnLT = 0;
    private decimal haberHnLT = 0;
    private decimal debeCr = 0;
    private decimal haberCr = 0;
    private decimal debeCrLT = 0;
    private decimal haberCrLT = 0;
    private decimal debenic = 0;
    private decimal habernic = 0;
    private decimal debenicLT = 0;
    private decimal habernicLT = 0;
    private decimal debegrh = 0;
    private decimal habergrh = 0;
    private decimal debemayan = 0;
    private decimal habermayan = 0;
    private decimal debeisi = 0;
    private decimal haberisi = 0;
    private decimal debePr = 0;
    private decimal haberPr = 0;
    private decimal debePrLT = 0;
    private decimal haberPrLT = 0;
    private decimal debebc = 0;
    private decimal haberbc = 0;
    private decimal debebcLT = 0;
    private decimal haberbcLT = 0;
    private decimal saldo_inicial_debe = 0;
    private decimal saldo_inicial_haber = 0;
	public ReportBean()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string Cue_Id
    {
        get { return cue_id; }
        set { cue_id = value; }
    }
    public string Nombre
    {
        get { return nombre; }
        set { nombre = value; }
    }
    public string Cue_Madre
    {
        get { return cue_madre; }
        set { cue_madre = value; }
    }
    public int Cue_Nivel
    {
        get { return cue_nivel; }
        set { cue_nivel = value; }
    }
    public int Cue_Clasificacion
    {
        get { return cue_clasificacion; }
        set { cue_clasificacion = value; }
    }
    public decimal Debe
    {
        get { return debe; }
        set { debe = value; }
    }
    public decimal Haber
    {
        get { return haber; }
        set { haber = value; }
    }
    public string Nivel1
    {
        get { return nivel1; }
        set { nivel1 = value; }
    }
    public string Nivel2
    {
        get { return nivel2; }
        set { nivel2 = value; }
    }
    public string Nivel3
    {
        get { return nivel3; }
        set { nivel3 = value; }
    }
    public string Nivel4
    {
        get { return nivel4; }
        set { nivel4 = value; }
    }
    public string Nivel5
    {
        get { return nivel5; }
        set { nivel5 = value; }
    }
    public decimal Saldo_Inicial_Debe
    {
        get { return saldo_inicial_debe ; }
        set { saldo_inicial_debe = value; }
    }
    public decimal Saldo_Inicial_Haber
    {
        get { return saldo_inicial_haber ; }
        set { saldo_inicial_haber = value; }
    }
    public decimal Debegt
    {
        get { return debegt; }
        set { debegt = value; }
    }
    public decimal Habergt
    {
        get { return habergt; }
        set { habergt = value; }
    }
    public decimal DebegtLT
    {
        get { return debegtLT; }
        set { debegtLT = value; }
    }
    public decimal HabergtLT
    {
        get { return habergtLT; }
        set { habergtLT = value; }
    }
    public decimal DebegtISI
    {
        get { return debegtISI; }
        set { debegtISI = value; }
    }
    public decimal HabergtISI
    {
        get { return habergtISI; }
        set { habergtISI = value; }
    }
    public decimal Debe1
    {
        get { return debe1; }
        set { debe1 = value; }
    }
    public decimal Haber1
    {
        get { return haber1; }
        set { haber1 = value; }
    }
    public decimal DebeEs
    {
        get { return debeEs; }
        set { debeEs = value; }
    }
    public decimal HaberEs
    {
        get { return haberEs; }
        set { haberEs = value; }
    }
    public decimal DebeESLT
    {
        get { return debeEsLT; }
        set { debeEsLT = value; }
    }
    public decimal HaberEsLT
    {
        get { return haberEsLT; }
        set { haberEsLT = value; }
    }
    public decimal DebeEs2
    {
        get { return debeEs2; }
        set { debeEs2 = value; }
    }
    public decimal HaberEs2
    {
        get { return haberEs2; }
        set { haberEs2 = value; }
    }
    public decimal DebeCr
    {
        get { return debeCr; }
        set { debeCr = value; }
    }
    public decimal HaberCr
    {
        get { return haberCr; }
        set { haberCr = value; }
    }
    public decimal DebeCrLT
    {
        get { return debeCrLT; }
        set { debeCrLT = value; }
    }
    public decimal HaberCrLT
    {
        get { return haberCrLT; }
        set { haberCrLT = value; }
    }
    public decimal DebeHn
    {
        get { return debeHn; }
        set { debeHn = value; }
    }
    public decimal HaberHn
    {
        get { return haberHn; }
        set { haberHn = value; }
    }
    public decimal DebeHnLT
    {
        get { return debeHnLT; }
        set { debeHnLT = value; }
    }
    public decimal HaberHnLT
    {
        get { return haberHnLT; }
        set { haberHnLT = value; }
    }
    public decimal DebePr
    {
        get { return debePr; }
        set { debePr = value; }
    }
    public decimal HaberPr
    {
        get { return haberPr; }
        set { haberPr = value; }
    }
    public decimal DebePrLT
    {
        get { return debePrLT; }
        set { debePrLT = value; }
    }
    public decimal HaberPrLT
    {
        get { return haberPrLT; }
        set { haberPrLT = value; }
    }
    public decimal Debenic
    {
        get { return debenic; }
        set { debenic = value; }
    }
    public decimal Habernic
    {
        get { return habernic; }
        set { habernic = value; }
    }
    public decimal DebenicLT
    {
        get { return debenicLT; }
        set { debenicLT = value; }
    }
    public decimal HabernicLT
    {
        get { return habernicLT; }
        set { habernicLT = value; }
    }
    public decimal Debegrh
    {
        get { return debegrh; }
        set { debegrh = value; }
    }
    public decimal Habergrh
    {
        get { return habergrh; }
        set { habergrh = value; }
    }
    public decimal Debeisi
    {
        get { return debeisi; }
        set { debeisi = value; }
    }
    public decimal Haberisi
    {
        get { return haberisi; }
        set { haberisi = value; }
    }
    public decimal Debemayan
    {
        get { return debemayan; }
        set { debemayan = value; }
    }
    public decimal Habermayan
    {
        get { return habermayan; }
        set { habermayan = value; }
    }
    public decimal Debebc
    {
        get { return debebc; }
        set { debebc = value; }
    }
    public decimal Haberbc
    {
        get { return haberbc; }
        set { haberbc = value; }
    }
    public decimal DebebcLT
    {
        get { return debebcLT; }
        set { debebcLT = value; }
    }
    public decimal HaberbcLT
    {
        get { return haberbcLT; }
        set { haberbcLT = value; }
    }
}
