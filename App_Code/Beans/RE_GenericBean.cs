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
/// Summary description for RE_GenericBean
/// </summary>
public class RE_GenericBean
{
    private double doubleCampo1 = 0;
    private double doubleCampo2 = 0;
    private double doubleCampo3 = 0;
    private double doubleCampo4 = 0;
    private double doubleCampo5 = 0;
    private double doubleCampo6 = 0;
    private double doubleCampo7 = 0;
    private double doubleCampo8 = 0;
    private double doubleCampo9 = 0;
    private double doubleCampo10 = 0;
    private double doubleCampo11 = 0;
    private double doubleCampo12 = 0;
    private long longCampo1 = 0;
    private long longCampo2 = 0;
    private long longCampo3 = 0;
    private long longCampo4 = 0;
    private long longCampo5 = 0;
    private long longCampo6 = 0;
    private long longCampo7 = 0;
    private long longCampo8 = 0;
    private long longCampo9 = 0;
    private long longCampo10 = 0;
    private long longCampo11 = 0;
    private long longCampo12 = 0;
    private int intCampo1 = 0;
    private int intCampo2 = 0;
    private int intCampo3 = 0;
    private int intCampo4 = 0;
    private int intCampo5 = 0;
    private int intCampo6 = 0;
    private int intCampo7 = 0;
    private int intCampo8 = 0;
    private int intCampo9 = 0;
    private int intCampo10 = 0;
    private int intCampo11 = 0;
    private int intCampo12 = 0;
    private int intCampo13 = 0;
    private int intCampo14 = 0;
    private int intCampo15 = 0;
    private int intCampo16 = 0;
    private int intestado = 0;
    private string strCampo1 = "";
    private string strCampo2 = "";
    private string strCampo3 = "";
    private string strCampo4 = "";
    private string strCampo5 = "";
    private string strCampo6 = "";
    private string strCampo7 = "";
    private string strCampo8 = "";
    private string strCampo9 = "";
    private string strCampo10 = "";
    private string strCampo11 = "";
    private string strCampo12 = "";
    private string strCampo13 = "";
    private string strCampo14 = "";
    private string strCampo15 = "";
    private string strCampo16 = "";
    private string strCampo17 = "";
    private string strCampo18 = "";
    private string strCampo19 = "";
    private string strCampo20 = "";
    private string strCampo21 = "";
    private string strCampo22 = "";
    private string strCampo23 = "";
    private string strCampo24 = "";
    private string strCampo25 = "";
    private string strCampo26 = "";
    private string strCampo27 = "";
    private string strCampo28 = "";
    private string strCampo29 = "";
    private string strCampo30 = "";
    private string strCampo31 = "";
    private string strCampo32 = "";
    private string strCampo33 = "";
    private string strCampo34 = "";
    private string strCampo35 = "";
    private string strCampo36 = "";
    private string strCampo37 = "";
    private string strCampo38 = "";
    private string strCampo39 = "";
    private string strCampo40 = "";
    private string strCampo41 = "";
    private string fecha_hora = "";
    private string strCampo42 = "";
    private string strCampo43 = "";
    private string strCampo44 = "";
    private string strCampo45 = "";
    private string strCampo46 = "";
    private string strCampo47 = "";
    private string strCampo48 = "";
    private string strCampo49 = "";
    private string strCampo50 = "";
    private string strCampo51 = "";
    private string strCampo52 = "";
    private string strCampo53 = "";
    private string strCampo54 = "";
    private string strCampo55 = "";
    private string strCampo56 = "";
    private string strCampo57 = "";
    private string strCampo58 = "";
    private string strCampo59 = "";
    private string strCampo60 = "";
    private string strCampo61 = "";
    private string strCampo62 = "";
    private string strCampo63 = "";
    private string strCampo64 = "";
    private string fecha_emision = "";
    private decimal decCampo1 = 0;
    private decimal decCampo2 = 0;
    private decimal decCampo3 = 0;
    private decimal decCampo4 = 0;
    private decimal decCampo5 = 0;
    private decimal decCampo6 = 0;
    private decimal decCampo7 = 0;
    private decimal decCampo8 = 0;
    private decimal decCampo9 = 0;
    private decimal decCampo10 = 0;
    private decimal decCampo11 = 0;
    private decimal decCampo12 = 0;
    private decimal decCampo13 = 0;
    private decimal decCampo14 = 0;
    private decimal decCampo15 = 0;
    private decimal decCampo16 = 0;
    private decimal decCampo17 = 0;
    private decimal decCampo18 = 0;
    private decimal decCampo19 = 0;
    private decimal decCampo20 = 0;
    private decimal decCampo21 = 0;
    private decimal decCampo22 = 0;
    private decimal decCampo23 = 0;
    private decimal decCampo24 = 0;
    private decimal decCampo25 = 0;
    private decimal decCampo26 = 0;
    private decimal decCampo27 = 0;
    private decimal decCampo28 = 0;
    private decimal decCampo29 = 0;
    private decimal decCampo30 = 0;
    private decimal decCampo31 = 0;
    private decimal decCampo32 = 0;
    private decimal decCampo33 = 0;
    private decimal decCampo34 = 0;
    private decimal decCampo35 = 0;
    private decimal decCampo36 = 0;
    private decimal decCampo37 = 0;
    private decimal decCampo38 = 0;
    private decimal decCampo39 = 0;
    private decimal decCampo40 = 0;
    private bool boolCampo1 = false;
    private bool boolCampo2 = false;
    private bool boolCampo3 = false;
    private bool boolCampo4 = false;
    private bool boolCampo5 = false;
    private bool boolCampo6 = false;
    private bool boolCampo7 = false;
    private bool boolCampo8 = false;
    private ArrayList arreglo1 = new ArrayList();
    private ArrayList arreglo2 = new ArrayList();
    private ArrayList arreglo3 = new ArrayList();
    private ArrayList arreglo4 = new ArrayList();
    private Hashtable _ht1 = null;

	public RE_GenericBean()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public long lonC1 {
        get { return longCampo1; }
        set { longCampo1 = value; }
    }
    public long lonC2
    {
        get { return longCampo2; }
        set { longCampo2 = value; }
    }
    public long lonC3
    {
        get { return longCampo3; }
        set { longCampo3 = value; }
    }
    public long lonC4
    {
        get { return longCampo4; }
        set { longCampo4 = value; }
    }
    public long lonC5
    {
        get { return longCampo5; }
        set { longCampo5 = value; }
    }
    public long lonC6
    {
        get { return longCampo6; }
        set { longCampo6 = value; }
    }
    public long lonC7
    {
        get { return longCampo7; }
        set { longCampo7 = value; }
    }
    public long lonC8
    {
        get { return longCampo8; }
        set { longCampo8 = value; }
    }
    public long lonC9
    {
        get { return longCampo9; }
        set { longCampo9 = value; }
    }
    public long lonC10
    {
        get { return longCampo10; }
        set { longCampo10 = value; }
    }
    public long lonC11
    {
        get { return longCampo11; }
        set { longCampo11 = value; }
    }
    public long lonC12
    {
        get { return longCampo12; }
        set { longCampo12 = value; }
    }
    public ArrayList arr1
    {
        get { return arreglo1; }
        set { arreglo1 = value; }
    }
    public ArrayList arr2
    {
        get { return arreglo2; }
        set { arreglo2 = value; }
    }
    public ArrayList arr3
    {
        get { return arreglo3; }
        set { arreglo3 = value; }
    }
    public ArrayList arr4
    {
        get { return arreglo4; }
        set { arreglo4 = value; }
    }
    public Hashtable ht1
    {
        get { return _ht1; }
        set { _ht1 = value; }
    }
    public double douC1
    {
        get { return doubleCampo1; }
        set { doubleCampo1 = value; }
    }
    public double douC2
    {
        get { return doubleCampo2; }
        set { doubleCampo2 = value; }
    }
    public double douC3
    {
        get { return doubleCampo3; }
        set { doubleCampo3 = value; }
    }
    public double douC4
    {
        get { return doubleCampo4; }
        set { doubleCampo4 = value; }
    }
    public double douC5
    {
        get { return doubleCampo5; }
        set { doubleCampo5 = value; }
    }
    public double douC6
    {
        get { return doubleCampo6; }
        set { doubleCampo6 = value; }
    }
    public double douC7
    {
        get { return doubleCampo7; }
        set { doubleCampo7 = value; }
    }
    public double douC8
    {
        get { return doubleCampo8; }
        set { doubleCampo8 = value; }
    }
    public double douC9
    {
        get { return doubleCampo9; }
        set { doubleCampo9 = value; }
    }
    public double douC10
    {
        get { return doubleCampo10; }
        set { doubleCampo10 = value; }
    }
    public double douC11
    {
        get { return doubleCampo11; }
        set { doubleCampo11 = value; }
    }
    public double douC12
    {
        get { return doubleCampo12; }
        set { doubleCampo12 = value; }
    }


    public bool boolC1
    {
        get { return boolCampo1; }
        set { boolCampo1 = value; }
    }
    public bool boolC2
    {
        get { return boolCampo2; }
        set { boolCampo2 = value; }
    }
    public bool boolC3
    {
        get { return boolCampo3; }
        set { boolCampo3 = value; }
    }
    public bool boolC4
    {
        get { return boolCampo4; }
        set { boolCampo4 = value; }
    }
    public bool boolC5
    {
        get { return boolCampo5; }
        set { boolCampo5 = value; }
    }
    public bool boolC6
    {
        get { return boolCampo6; }
        set { boolCampo6 = value; }
    }
    public bool boolC7
    {
        get { return boolCampo7; }
        set { boolCampo7 = value; }
    }
    public bool boolC8
    {
        get { return boolCampo8; }
        set { boolCampo8 = value; }
    }
    public decimal decC1
    {
        get { return decCampo1; }
        set { decCampo1 = value; }
    }
    public decimal decC2
    {
        get { return decCampo2; }
        set { decCampo2 = value; }
    }
    public decimal decC3
    {
        get { return decCampo3; }
        set { decCampo3 = value; }
    }
    public decimal decC4
    {
        get { return decCampo4; }
        set { decCampo4 = value; }
    }
    public decimal decC5
    {
        get { return decCampo5; }
        set { decCampo5 = value; }
    }
    public decimal decC6
    {
        get { return decCampo6; }
        set { decCampo6 = value; }
    }
    public decimal decC7
    {
        get { return decCampo7; }
        set { decCampo7 = value; }
    }
    public decimal decC8
    {
        get { return decCampo8; }
        set { decCampo8 = value; }
    }
    public decimal decC9
    {
        get { return decCampo9; }
        set { decCampo9 = value; }
    }
    public decimal decC10
    {
        get { return decCampo10; }
        set { decCampo10 = value; }
    }
    public decimal decC11
    {
        get { return decCampo11; }
        set { decCampo11 = value; }
    }
    public decimal decC12
    {
        get { return decCampo12; }
        set { decCampo12 = value; }
    }
    public decimal decC13
    {
        get { return decCampo13; }
        set { decCampo13 = value; }
    }
    public decimal decC14
    {
        get { return decCampo14; }
        set { decCampo14 = value; }
    }
    public decimal decC15
    {
        get { return decCampo15; }
        set { decCampo15 = value; }
    }
    public decimal decC16
    {
        get { return decCampo16; }
        set { decCampo16 = value; }
    }
    public decimal decC17
    {
        get { return decCampo17; }
        set { decCampo17 = value; }
    }
    public decimal decC18
    {
        get { return decCampo18; }
        set { decCampo18 = value; }
    }
    public decimal decC19
    {
        get { return decCampo19; }
        set { decCampo19 = value; }
    }
    public decimal decC20
    {
        get { return decCampo20; }
        set { decCampo20 = value; }
    }
    public decimal decC21
    {
        get { return decCampo21; }
        set { decCampo21 = value; }
    }
    public decimal decC22
    {
        get { return decCampo22; }
        set { decCampo22 = value; }
    }
    public decimal decC23
    {
        get { return decCampo23; }
        set { decCampo23 = value; }
    }
    public decimal decC24
    {
        get { return decCampo24; }
        set { decCampo24 = value; }
    }


    public decimal decC25
    {
        get { return decCampo25; }
        set { decCampo25 = value; }
    }
    public decimal decC26
    {
        get { return decCampo26; }
        set { decCampo26 = value; }
    }
    public decimal decC27
    {
        get { return decCampo27; }
        set { decCampo27 = value; }
    }
    public decimal decC28
    {
        get { return decCampo28; }
        set { decCampo28 = value; }
    }
    public decimal decC29
    {
        get { return decCampo29; }
        set { decCampo29 = value; }
    }
    public decimal decC30
    {
        get { return decCampo30; }
        set { decCampo30 = value; }
    }
    public decimal decC31
    {
        get { return decCampo31; }
        set { decCampo31 = value; }
    }
    public decimal decC32
    {
        get { return decCampo32; }
        set { decCampo32 = value; }
    }
    public decimal decC33
    {
        get { return decCampo33; }
        set { decCampo33 = value; }
    }
    public decimal decC34
    {
        get { return decCampo34; }
        set { decCampo34 = value; }
    }
    public decimal decC35
    {
        get { return decCampo35; }
        set { decCampo35 = value; }
    }
    public decimal decC36
    {
        get { return decCampo36; }
        set { decCampo36 = value; }
    }
    public decimal decC37
    {
        get { return decCampo37; }
        set { decCampo37 = value; }
    }
    public decimal decC38
    {
        get { return decCampo38; }
        set { decCampo38 = value; }
    }
    public decimal decC39
    {
        get { return decCampo39; }
        set { decCampo39 = value; }
    }
    public decimal decC40
    {
        get { return decCampo40; }
        set { decCampo40 = value; }
    }


    public int intC1
    {
        get { return intCampo1; }
        set { intCampo1 = value; }
    }
    public int intC2
    {
        get { return intCampo2; }
        set { intCampo2 = value; }
    }
    public int intC3
    {
        get { return intCampo3; }
        set { intCampo3 = value; }
    }
    public int intC4
    {
        get { return intCampo4; }
        set { intCampo4 = value; }
    }
    public int intC5
    {
        get { return intCampo5; }
        set { intCampo5 = value; }
    }
    public int intC6
    {
        get { return intCampo6; }
        set { intCampo6 = value; }
    }
    public int intC7
    {
        get { return intCampo7; }
        set { intCampo7 = value; }
    }
    public int intC8
    {
        get { return intCampo8; }
        set { intCampo8 = value; }
    }
    public int intC9
    {
        get { return intCampo9; }
        set { intCampo9 = value; }
    }
    public int intC10
    {
        get { return intCampo10; }
        set { intCampo10 = value; }
    }
    public int intC11
    {
        get { return intCampo11; }
        set { intCampo11 = value; }
    }
    public string strC1
    {
        get { return strCampo1; }
        set { strCampo1 = value; }
    }
    public string strC2
    {
        get { return strCampo2; }
        set { strCampo2 = value; }
    }
    public string strC3
    {
        get { return strCampo3; }
        set { strCampo3 = value; }
    }
    public string strC4
    {
        get { return strCampo4; }
        set { strCampo4 = value; }
    }
    public string strC5
    {
        get { return strCampo5; }
        set { strCampo5 = value; }
    }
    public string strC6
    {
        get { return strCampo6; }
        set { strCampo6 = value; }
    }
    public string strC7
    {
        get { return strCampo7; }
        set { strCampo7 = value; }
    }
    public string strC8
    {
        get { return strCampo8; }
        set { strCampo8 = value; }
    }
    public string strC9
    {
        get { return strCampo9; }
        set { strCampo9 = value; }
    }
    public string strC10
    {
        get { return strCampo10; }
        set { strCampo10 = value; }
    }
    public string strC11
    {
        get { return strCampo11; }
        set { strCampo11 = value; }
    }
    public string strC12
    {
        get { return strCampo12; }
        set { strCampo12 = value; }
    }
    public string strC13
    {
        get { return strCampo13; }
        set { strCampo13 = value; }
    }
    public string strC14
    {
        get { return strCampo14; }
        set { strCampo14 = value; }
    }
    public string strC15
    {
        get { return strCampo15; }
        set { strCampo15 = value; }
    }
    public string strC16
    {
        get { return strCampo16; }
        set { strCampo16 = value; }
    }
    public string strC17
    {
        get { return strCampo17; }
        set { strCampo17 = value; }
    }
    public string strC18
    {
        get { return strCampo18; }
        set { strCampo18 = value; }
    }
    public string strC19
    {
        get { return strCampo19; }
        set { strCampo19 = value; }
    }
    public string strC20
    {
        get { return strCampo20; }
        set { strCampo20 = value; }
    }
    public string strC21
    {
        get { return strCampo21; }
        set { strCampo21 = value; }
    }
    public string strC22
    {
        get { return strCampo22; }
        set { strCampo22 = value; }
    }
    public string strC23
    {
        get { return strCampo23; }
        set { strCampo23 = value; }
    }
    public string strC24
    {
        get { return strCampo24; }
        set { strCampo24 = value; }
    }
    public string strC25
    {
        get { return strCampo25; }
        set { strCampo25 = value; }
    }
    public string strC26
    {
        get { return strCampo26; }
        set { strCampo26 = value; }
    }
    public string strC27
    {
        get { return strCampo27; }
        set { strCampo27 = value; }
    }
    public string strC28
    {
        get { return strCampo28; }
        set { strCampo28 = value; }
    }
    public string strC29
    {
        get { return strCampo29; }
        set { strCampo29 = value; }
    }
    public string strC30
    {
        get { return strCampo30; }
        set { strCampo30 = value; }
    }
    public string strC31
    {
        get { return strCampo31; }
        set { strCampo31 = value; }
    }
    //Dennis
    public string strC32
    {
        get { return strCampo32; }
        set { strCampo32 = value; }
    }
    public string strC33
    {
        get { return strCampo33; }
        set { strCampo33 = value; }
    }
    public string strC34
    {
        get { return strCampo34; }
        set { strCampo34 = value; }
    }
    public string strC35
    {
        get { return strCampo35; }
        set { strCampo35 = value; }
    }
    public string strC36
    {
        get { return strCampo36; }
        set { strCampo36 = value; }
    }
    public string strC37
    {
        get { return strCampo37; }
        set { strCampo37 = value; }
    }
    public string strC38
    {
        get { return strCampo38; }
        set { strCampo38 = value; }
    }
    public string strC39
    {
        get { return strCampo39; }
        set { strCampo39 = value; }
    }
    public string strC40
    {
        get { return strCampo40; }
        set { strCampo40 = value; }
    }
    public string strC41
    {
        get { return strCampo41; }
        set { strCampo41 = value; }
    }
    public string strC42
    {
        get { return strCampo42; }
        set { strCampo42 = value; }
    }
    public string strC43
    {
        get { return strCampo43; }
        set { strCampo43 = value; }
    }
    public string strC44
    {
        get { return strCampo44; }
        set { strCampo44 = value; }
    }
    public string strC45
    {
        get { return strCampo45; }
        set { strCampo45 = value; }
    }
    public string strC46
    {
        get { return strCampo46; }
        set { strCampo46 = value; }
    }
    public string strC47
    {
        get { return strCampo47; }
        set { strCampo47 = value; }
    }
    public string strC48
    {
        get { return strCampo48; }
        set { strCampo48 = value; }
    }
    public string strC49
    {
        get { return strCampo49; }
        set { strCampo49 = value; }
    }
    public string strC50
    {
        get { return strCampo50; }
        set { strCampo50 = value; }
    }
    public string strC51
    {
        get { return strCampo51; }
        set { strCampo51 = value; }
    }
    public string strC52
    {
        get { return strCampo52; }
        set { strCampo52 = value; }
    }
    public int intC12
    {
        get { return intCampo12; }
        set { intCampo12 = value; }
    }
    public int intC13
    {
        get { return intCampo13; }
        set { intCampo13 = value; }
    }
    public int intC14
    {
        get { return intCampo14; }
        set { intCampo14 = value; }
    }
    public int intC15
    {
        get { return intCampo15; }
        set { intCampo15 = value; }
    }
    public int intC16
    {
        get { return intCampo16; }
        set { intCampo16 = value; }
    }
    public string Fecha_Hora
    {
        get { return fecha_hora; }
        set { fecha_hora = value; }
    }
    public int Estado
    {
        get { return intestado; }
        set { intestado = value; }
    }
    private string nombre_cliente = "";
    public string Nombre_Cliente
    {
        get { return nombre_cliente; }
        set { nombre_cliente = value; }
    }
    private bool fiscal_no_fiscal = true;
    public bool Fiscal_No_Fiscal
    {
        get { return fiscal_no_fiscal; }
        set { fiscal_no_fiscal = value; }
    }
    private string total_letras = "";
    public string Total_Letras
    {
        get { return total_letras; }
        set { total_letras = value; }
    }
    private string total_letras_equivalente = "";
    public string Total_Letras_Equivalente
    {
        get { return total_letras_equivalente; }
        set { total_letras_equivalente = value; }
    }
    private int blid = 0;
    public int Blid
    {
        get { return blid; }
        set { blid = value; }
    }
    private int tipo_operacion = 0;
    public int Tipo_Operacion
    {
        get { return tipo_operacion; }
        set { tipo_operacion = value; }
    }
    private string modifica_regimen = "";
    public string Modifica_Regimen
    {
        get { return modifica_regimen; }
        set { modifica_regimen = value; }
    }
    private int tipo_contribuyente = 0;
    public int Tipo_Contribuyente
    {
        get { return tipo_contribuyente; }
        set { tipo_contribuyente = value; }
    }
    private string referencia_interna = "";
    public string Referencia_Interna
    {
        get { return referencia_interna; }
        set { referencia_interna = value; }
    }
    private int factura_electronica = 0;
    public int Factura_Electronica
    {
        get { return factura_electronica; }
        set { factura_electronica = value; }
    }
    private string correlativo = "";
    public string Correlativo
    {
        get { return correlativo; }
        set { correlativo = value; }
    }
    private string direccion = "";
    public string Direccion
    {
        get { return direccion; }
        set { direccion = value; }
    }
    private double totalingresosng;
    public double TotalIngresosNG
    {
        get { return totalingresosng; }
        set { totalingresosng = value; }
    }
    public string strC53
    {
        get { return strCampo53; }
        set { strCampo53 = value; }
    }
    public string strC54
    {
        get { return strCampo54; }
        set { strCampo54 = value; }
    }
    public string strC55
    {
        get { return strCampo55; }
        set { strCampo55 = value; }
    }
    public string strC56
    {
        get { return strCampo56; }
        set { strCampo56 = value; }
    }
    public string strC57
    {
        get { return strCampo57; }
        set { strCampo57 = value; }
    }
    public string Fecha_Emision
    {
        get { return fecha_emision; }
        set { fecha_emision = value; }
    }
    public string strC58
    {
        get { return strCampo58; }
        set { strCampo58 = value; }
    }
    public string strC59
    {
        get { return strCampo59; }
        set { strCampo59 = value; }
    }
    public string strC60
    {
        get { return strCampo60; }
        set { strCampo60 = value; }
    }
    public string strC61
    {
        get { return strCampo61; }
        set { strCampo61 = value; }
    }
    public string strC62
    {
        get { return strCampo62; }
        set { strCampo62 = value; }
    }
    public string strC63
    {
        get { return strCampo63; }
        set { strCampo63 = value; }
    }
    public string strC64
    {
        get { return strCampo64; }
        set { strCampo64 = value; }
    }
    private string exportacion_nombre_comercial = "";
    public string Exportacion_Nombre_Comercial
    {
        get { return exportacion_nombre_comercial; }
        set { exportacion_nombre_comercial = value; }
    }
    private string exportacion_identificacion_tributaria = "";
    public string Exportacion_Identificacion_Tributaria
    {
        get { return exportacion_identificacion_tributaria; }
        set { exportacion_identificacion_tributaria = value; }
    }

    //Se agregaron para nota debito electronica
    private int factura_ref_id = 0;
    public int Factura_Ref_ID
    {
        get { return factura_ref_id; }
        set { factura_ref_id = value; }
    }
    private string factura_ref_serie = "";
    public string Factura_Ref_Serie
    {
        get { return factura_ref_serie; }
        set { factura_ref_serie = value; }
    }
    private int factura_ref_correlativo = 0;
    public int Factura_Ref_Correlativo
    {
        get { return factura_ref_correlativo; }
        set { factura_ref_correlativo = value; }
    }

    private string factura_ref_fecha = "";
    public string Factura_Ref_Fecha
    {
        get { return factura_ref_fecha; }
        set { factura_ref_fecha = value; }
    }

    private string factura_ref_doc = "";
    public string Factura_Ref_Doc
    {
        get { return factura_ref_doc; }
        set { factura_ref_doc = value; }
    }

}



