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
/// Summary description for MatOpBean
/// </summary>
public class MatOpBean
{
    private int _paiID = 0;
    private int _tranID = 0;
    private int _servID = 0;
    private int _rubID = 0;
    private int _contriID = 0;
    private int _monID = 0;
    private int _cobroID = 0;
    private bool _cobIva = true;
    private bool _Ivainc = true;
    private int _contaID = 0;
    private int _impexpID = 0;

	public MatOpBean()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int paiID
    {
        get { return _paiID; }
        set { _paiID = value; }
    }
    public int tranID
    {
        get { return _tranID; }
        set { _tranID = value; }
    }
    public int servID
    {
        get { return _servID; }
        set { _servID = value; }
    }
    public int rubID
    {
        get { return _rubID; }
        set { _rubID = value; }
    }
    public int contriID
    {
        get { return _contriID; }
        set { _contriID = value; }
    }
    public int monID
    {
        get { return _monID; }
        set { _monID = value; }
    }
    public int cobroID
    {
        get { return _cobroID; }
        set { _cobroID = value; }
    }
    public int contaID
    {
        get { return _contaID; }
        set { _contaID = value; }
    }
    public int impexpID
    {
        get { return _impexpID; }
        set { _impexpID = value; }
    }
    public bool cobIva
    {
        get { return _cobIva; }
        set { _cobIva = value; }
    }
    public bool ivaInc
    {
        get { return _Ivainc; }
        set { _Ivainc = value; }
    }
}
