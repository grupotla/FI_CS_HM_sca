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
/// Summary description for Conv
/// </summary>
public class Conv
{
	public Conv()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string enletras(string num) {
        return enletras(num, 1);
    }

    public string enletras(string num, int monID)
    {
        string res, dec = "";
        Int64 entero;
        int decimales;
        double nro;
        try
        {
            nro = Convert.ToDouble(num);
        }
        catch
        {
            return "";
        }
        entero = Convert.ToInt64(Math.Truncate(nro));
        decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
        if (monID == 8)
        {
            if ((decimales >= 0) && (decimales < 10))
            {
                dec = " WITH 0" + decimales.ToString() + "/100";
            }
            else if ((decimales >= 0) && (decimales >9))
            {
                dec = " WITH " + decimales.ToString() + "/100";
            }
            res = toTextEng(Convert.ToDouble(entero)) + dec;
        }
        else
        {
            if ((decimales >= 0)&&(decimales<10))
            {
                dec = " CON 0" + decimales.ToString() + "/100";
            }
            else if ((decimales >= 0) && (decimales > 9))
            {
                dec = " CON " + decimales.ToString() + "/100";
            }

            res = toText(Convert.ToDouble(entero)) + dec;
        }
        return res;
    }

    private string toText(double value)
    {
        string Num2Text = "";
        value = Math.Truncate(value);
        if (value == 0) Num2Text = "CERO";
        else if (value == 1) Num2Text = "UNO";
        else if (value == 2) Num2Text = "DOS";
        else if (value == 3) Num2Text = "TRES";
        else if (value == 4) Num2Text = "CUATRO";
        else if (value == 5) Num2Text = "CINCO";
        else if (value == 6) Num2Text = "SEIS";
        else if (value == 7) Num2Text = "SIETE";
        else if (value == 8) Num2Text = "OCHO";
        else if (value == 9) Num2Text = "NUEVE";
        else if (value == 10) Num2Text = "DIEZ";
        else if (value == 11) Num2Text = "ONCE";
        else if (value == 12) Num2Text = "DOCE";
        else if (value == 13) Num2Text = "TRECE";
        else if (value == 14) Num2Text = "CATORCE";
        else if (value == 15) Num2Text = "QUINCE";
        else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
        else if (value == 20) Num2Text = "VEINTE";
        else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
        else if (value == 30) Num2Text = "TREINTA";
        else if (value == 40) Num2Text = "CUARENTA";
        else if (value == 50) Num2Text = "CINCUENTA";
        else if (value == 60) Num2Text = "SESENTA";
        else if (value == 70) Num2Text = "SETENTA";
        else if (value == 80) Num2Text = "OCHENTA";
        else if (value == 90) Num2Text = "NOVENTA";
        else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
        else if (value == 100) Num2Text = "CIEN";
        else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
        else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
        else if (value == 500) Num2Text = "QUINIENTOS";
        else if (value == 700) Num2Text = "SETECIENTOS";
        else if (value == 900) Num2Text = "NOVECIENTOS";
        else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
        else if (value == 1000) Num2Text = "MIL";
        else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
        else if (value < 1000000)
        {
            Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
            if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
        }
        else if (value == 1000000) Num2Text = "UN MILLON";
        else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
        else if (value < 1000000000000)
        {

            Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";

            if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
        }
        else if (value == 1000000000000) Num2Text = "UN BILLON";
        else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
        else
        {
            Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
            if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
        }
        return Num2Text;
    }

    private string toTextEng(double value)
    {
        string Num2Text = "";
        value = Math.Truncate(value);
        if (value == 0) Num2Text = "ZERO";
        else if (value == 1) Num2Text = "ONE";
        else if (value == 2) Num2Text = "TWO";
        else if (value == 3) Num2Text = "THREE";
        else if (value == 4) Num2Text = "FOUR";
        else if (value == 5) Num2Text = "FIVE";
        else if (value == 6) Num2Text = "SIX";
        else if (value == 7) Num2Text = "SEVEN";
        else if (value == 8) Num2Text = "EIGHT";
        else if (value == 9) Num2Text = "NINE";
        else if (value == 10) Num2Text = "TEN";
        else if (value == 11) Num2Text = "ELEVEN";
        else if (value == 12) Num2Text = "TWELVE";
        else if (value == 13) Num2Text = "THIRTEEN";
        else if (value == 14) Num2Text = "FOURTEEN";
        else if (value == 15) Num2Text = "FIFTEEN";
        else if (value == 16) Num2Text = "SIXTEEN";
        else if (value == 17) Num2Text = "SEVENTEEN";
        else if (value == 18) Num2Text = "EIGHTEEN";
        else if (value == 19) Num2Text = "NINETEEN";
        else if (value == 20) Num2Text = "TWENTY";
        else if (value == 30) Num2Text = "THIRTY";
        else if (value == 40) Num2Text = "FORTY";
        else if (value == 50) Num2Text = "FIFTY";
        else if (value == 60) Num2Text = "SIXTY";
        else if (value == 70) Num2Text = "SEVENTY";
        else if (value == 80) Num2Text = "EIGHTY";
        else if (value == 90) Num2Text = "NINETY";
        else if (value < 100) Num2Text = toTextEng(Math.Truncate(value / 10) * 10) + " " + toTextEng(value % 10);
        else if (value < 1000) {
            Num2Text = toTextEng(Math.Truncate(value / 100)) + " HUNDRED ";
            if ((value % 100) > 0) Num2Text = Num2Text + " " + toTextEng(value % 100);
        }
        else if (value < 1000000)
        {
            Num2Text = toTextEng(Math.Truncate(value / 1000)) + " THOUSAND ";
            if ((value % 1000) > 0) Num2Text = Num2Text + " " + toTextEng(value % 1000);
        }
        else if (value == 1000000) Num2Text = " ONE MILLION ";
        else if (value < 2000000) Num2Text = "ONE MILLION " + toTextEng(value % 1000000);
        else if (value < 1000000000000)
        {

            Num2Text = toTextEng(Math.Truncate(value / 1000000)) + " MILLIONS ";

            if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toTextEng(value - Math.Truncate(value / 1000000) * 1000000);
        }
        else if (value == 1000000000000) Num2Text = " ONE BILLION ";
        else if (value < 2000000000000) Num2Text = "ONE BILLION " + toTextEng(value - Math.Truncate(value / 1000000000000) * 1000000000000);
        else
        {
            Num2Text = toTextEng(Math.Truncate(value / 1000000000000)) + " BILLIONS ";
            if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toTextEng(value - Math.Truncate(value / 1000000000000) * 1000000000000);
        }
        return Num2Text;
    }
}
