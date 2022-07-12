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
/// Summary description for Utility
/// </summary>
public class Utility
{
    public Utility()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string cifrado(string texto)
    {
        string result = null;
        try
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(texto);
            bs = x.ComputeHash(bs);
            string password;
            System.Text.StringBuilder smd = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                smd.Append(b.ToString("x2").ToLower());
            }
            result = smd.ToString();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
        return result;

    }
    /** Metodo que genera el codigo de la partida por pais
     * recibe de parametro un entero que significa el pais, y un entero que es el correlativo
     **/
    public static string GeneroPartida(string paisISO, int partidaNo, int tipoconta)
    {
        string result = "";
        try
        {
            result = paisISO + tipoconta + DateTime.Now.Year.ToString().Substring(2, 2);
            int mes = int.Parse(DateTime.Now.Month.ToString());
            if (mes < 10) result += "0" + mes;
            else result += mes;
            if (partidaNo < 10) result += "0000" + partidaNo;
            else if ((partidaNo >= 10) && (partidaNo < 100)) result += "000" + partidaNo;
            else if ((partidaNo >= 100) && (partidaNo < 1000)) result += "00" + partidaNo;
            else if ((partidaNo >= 1000) && (partidaNo < 10000)) result += "0" + partidaNo;
            else if (partidaNo >= 10000) result += partidaNo;
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog("Utility.GeneroPartida " + e.Message);
            return null;
        }
        return result;
    }
    /**
     * Metodo que obtiene las cuentas contables y las devuelve en un arreglo
     * Parametros recibe un string con los tipos de cuentas que se desea obtener, ejemplo 1,3,5 (activo, venta, capital)
     **/
    public static ArrayList getCuentasContables(string tipo, string criterio)
    {
        ArrayList result = null;
        string where = "";
        try
        {
            if (tipo.Equals("XClasificacion"))
            {
                where = " and cue_clasificacion in (" + criterio + ")";
                result = (ArrayList)DB.getCuenta(where);
            }
            else if (tipo.Equals("XTipoClasificacion"))
            {
                where = criterio;
                result = (ArrayList)DB.getCuenta(where);
            }
            else if (tipo.Equals("Bancarias"))
            {
                where = " and cue_tipo=2";
                result = (ArrayList)DB.getCuenta(where);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
        return result;
    }
    /**
     * Metodo que obtiene todos los provisiones para editarlas, aceptarlas, etc
     */
    public static ArrayList getCuentasProvisiones(string criterio)
    {
        ArrayList result = null;
        string where = "";
        try
        {
            result = (ArrayList)DB.getProvision(criterio);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
        return result;
    }
    /**
     * Metodo que obtiene los proveedores segun el tipo y el criterio y las devuelve en un arreglo
     * Parametros recibe un string con los tipos de busqueda que se desea obtener, y el criterio
     **/
    public static ArrayList getProveedor(string tipo, string criterio)
    {
        ArrayList result = null;
        string where = "";
        try
        {
            if (tipo.Equals("XNitNombre"))
            {
                where = criterio;
                result = (ArrayList)DB.getProveedor(where, "");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
        return result;
    }
    /**
     * Metodo que obtiene los agentes segun el tipo y el criterio y las devuelve en un arreglo
     * Parametros recibe un string con los tipos de busqueda que se desea obtener, y el criterio
     **/
    public static ArrayList getAgentes(string tipo, string criterio)
    {
        ArrayList result = null;
        string where = "";
        try
        {
            if (tipo.Equals("XNitNombre"))
            {
                where = criterio;
                result = (ArrayList)DB.getAgentes(where);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
        return result;
    }
    /**
     * Metodo que llena un gridvies a partir de un arraylist
     * Parametros, cantidad de columnas
     */
    public static DataTable fillGridView(string tipo, ArrayList arr)
    {
        DataTable dt = null;
        try
        {
            dt = new DataTable("Generico");
            if (tipo.Equals("chequesanticipo"))
            {
                dt.Columns.Add("id");
                dt.Columns.Add("Cuenta No.");
                dt.Columns.Add("Numero");
                dt.Columns.Add("Valor");
                dt.Columns.Add("Fecha");
                dt.Columns.Add("Moneda");
                dt.Columns.Add("Valor Equivalente");

                foreach (RE_GenericBean rgb in arr)
                {
                    object[] objArr = { rgb.intC1, rgb.strC1, rgb.intC2, rgb.decC1, rgb.strC2, rgb.strC3, rgb.decC2 };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("retenciontoprint"))
            {
                dt.Columns.Add("retencionID");
                dt.Columns.Add("chequeID");
                dt.Columns.Add("corteID");
                dt.Columns.Add("cheque");
                dt.Columns.Add("corte");
                dt.Columns.Add("provision");
                dt.Columns.Add("referencia");
                dt.Columns.Add("retencion");
                dt.Columns.Add("monto");
                dt.Columns.Add("moneda");
                dt.Columns.Add("proveedorID");
                dt.Columns.Add("proveedor");
                foreach (RE_GenericBean rgb in arr)
                {
                    object[] objArr = { rgb.intC4, rgb.intC1, rgb.intC2, rgb.intC5, rgb.strC2 + "-" + rgb.strC3, rgb.strC6 + "-" + rgb.strC7, rgb.strC4, rgb.strC5, rgb.decC1, Utility.TraducirMonedaInt(rgb.intC6), rgb.intC7, rgb.strC8 };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("chequesgenerados"))
            {
                dt.Columns.Add("id");
                dt.Columns.Add("cuenta");
                dt.Columns.Add("chequeno");
                dt.Columns.Add("provid");
                dt.Columns.Add("banconombre");
                dt.Columns.Add("monto");
                dt.Columns.Add("moneda");

                foreach (RE_GenericBean rgb in arr)
                {
                    object[] objArr = { rgb.intC3, rgb.strC1, rgb.intC1, rgb.strC4, rgb.strC2, rgb.decC1, rgb.strC3 };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("transfereciasgeneradas"))
            {
                dt.Columns.Add("id");
                dt.Columns.Add("cuenta");
                dt.Columns.Add("chequeno");
                dt.Columns.Add("provid");
                dt.Columns.Add("banconombre");
                dt.Columns.Add("monto");
                dt.Columns.Add("moneda");

                foreach (RE_GenericBean rgb in arr)
                {
                    object[] objArr = { rgb.intC3, rgb.strC1, rgb.strC5, rgb.strC4, rgb.strC2, rgb.decC1, rgb.strC3 };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("Cuenta"))
            {
                dt.Columns.Add("Cuenta ID");
                dt.Columns.Add("Descripción");
                foreach (RE_GenericBean rgb in arr)
                {
                    object[] objArr = { rgb.strC1, rgb.strC2 };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("detallecorte"))
            {
                dt.Columns.Add("Id");
                dt.Columns.Add("Correlativo");
                dt.Columns.Add("Documento");
                dt.Columns.Add("Fecha");
                dt.Columns.Add("Moneda");
                dt.Columns.Add("Total");
                dt.Columns.Add("Total Eq.");
                dt.Columns.Add("Tipo Documento");
                foreach (RE_GenericBean rgb in arr)
                {
                    object[] objArr = { rgb.intC1, rgb.intC2, rgb.strC2, rgb.strC1, Utility.TraducirMonedaInt(rgb.intC4), rgb.decC1.ToString("#,#.00"), rgb.decC2.ToString("#,#.00"), Utility.TraducirOperaciontoSTR(rgb.intC3) };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("Naviera"))
            {
                dt.Columns.Add("Naviera ID");
                dt.Columns.Add("Nombre");
                foreach (RE_GenericBean rgb in arr)
                {
                    object[] objArr = { rgb.intC1, rgb.strC1 };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("LineasAereas"))
            {
                dt.Columns.Add("Linea aerea ID");
                dt.Columns.Add("Nombre");
                foreach (RE_GenericBean rgb in arr)
                {
                    object[] objArr = { rgb.intC1, rgb.strC1 };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("Provision"))
            {
                dt.Columns.Add("Provision ID");
                dt.Columns.Add("Fecha");
                dt.Columns.Add("Fecha maximo de pago");
                dt.Columns.Add("Valor");
                dt.Columns.Add("Observaciones");
                dt.Columns.Add("Usuario");
                foreach (RE_GenericBean rgb in arr)
                {
                    object[] objArr = { rgb.intC1, rgb.strC1, rgb.strC2, rgb.decC1.ToString("#,#.00"), rgb.strC3, rgb.strC4 };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("Proveedor"))
            {
                dt.Columns.Add("Proveedor ID");
                dt.Columns.Add("Nit");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("Descripcion");
                dt.Columns.Add("Observaciones");
                dt.Columns.Add("Correo Electrónico");
                dt.Columns.Add("Dirección");
                dt.Columns.Add("Teléfono");
                dt.Columns.Add("Dias de credito");
                dt.Columns.Add("Tipo Regimen");
                foreach (RE_GenericBean rgb in arr)
                {
                    //object[] objArr = { rgb.intC1, rgb.strC1, rgb.strC2, rgb.strC4, rgb.strC10, rgb.strC8, rgb.strC5, rgb.strC6, rgb.intC4, rgb.intC7 };
                    object[] objArr = { rgb.intC1, rgb.strC1, rgb.strC2, rgb.strC3, rgb.strC10, rgb.strC9, rgb.strC5, rgb.strC7, rgb.intC9, rgb.intC7 };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("Intercompany"))
            {
                dt.Columns.Add("Intercompany ID");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("Nit");
                dt.Columns.Add("Tipo Regimen");
                dt.Columns.Add("Direccion");
                dt.Columns.Add("Nombre_Comercial");
                dt.Columns.Add("Ruc");
                foreach (RE_GenericBean rgb in arr)
                {
                    //object[] objArr = { rgb.intC1, rgb.strC1, rgb.strC2, rgb.strC4, rgb.strC10, rgb.strC8, rgb.strC5, rgb.strC6, rgb.intC4, rgb.intC7 };
                    object[] objArr = { rgb.intC1, rgb.strC5, rgb.strC2, rgb.intC2, rgb.strC3, rgb.strC1, rgb.strC6 };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("ProveedorforOC"))
            {
                dt.Columns.Add("Proveedor ID");
                dt.Columns.Add("Nit");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("Descripcion");
                dt.Columns.Add("Dirección");
                dt.Columns.Add("Teléfono");
                dt.Columns.Add("Email");
                foreach (RE_GenericBean rgb in arr)
                {
                    object[] objArr = { rgb.intC1, rgb.strC1, rgb.strC2, rgb.strC3, rgb.strC5, rgb.strC7, rgb.strC9 };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("Agente"))
            {
                dt.Columns.Add("Agente ID");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("Dirección");
                dt.Columns.Add("Teléfono");
                dt.Columns.Add("Correo Electrónico");
                dt.Columns.Add("Contacto");
                dt.Columns.Add("Tipo");
                dt.Columns.Add("Nit");
                dt.Columns.Add("Ruc");
                foreach (RE_GenericBean rgb in arr)
                {
                    object[] objArr = { rgb.intC1, rgb.strC1, rgb.strC2, rgb.strC3, rgb.strC4, rgb.strC5, rgb.strC7, rgb.strC8, rgb.strC9 };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("FactPendProveedor"))
            {
                dt.Columns.Add("ID");
                dt.Columns.Add("Factura");
                dt.Columns.Add("Fecha");
                dt.Columns.Add("HBL");
                dt.Columns.Add("MBL");
                ///
                dt.Columns.Add("PROVISION");
                dt.Columns.Add("OC");
                ///
                dt.Columns.Add("Valor Retencion");
                dt.Columns.Add("Valor Provision");
                dt.Columns.Add("Total a Pagar");
                dt.Columns.Add("Moneda");
                dt.Columns.Add("Valor Retencion Equivalente");
                dt.Columns.Add("Total Equivalente");
                foreach (RE_GenericBean rgb in arr)
                {

                    //object[] objArr = { rgb.intC1, rgb.strC1, rgb.strC2, rgb.strC5, rgb.strC6, rgb.decC2.ToString("#,#.00"), rgb.decC1.ToString("#,#.00"), (rgb.decC1 - rgb.decC2).ToString("#.#.00"), Utility.TraducirMonedaInt(rgb.intC2) };
                    object[] objArr = { rgb.intC1, rgb.strC1, rgb.strC2, rgb.strC5, rgb.strC6, rgb.strC7, rgb.strC8, rgb.decC2.ToString("#,#.00"), rgb.decC1.ToString("#,#.00"), (rgb.decC1 - rgb.decC2).ToString("#.#.00"), Utility.TraducirMonedaInt(rgb.intC2), rgb.decC4.ToString("#.#.00"), (rgb.decC3 - rgb.decC4).ToString("#.#.00") };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("NDPendProveedor"))
            {
                dt.Columns.Add("ID");
                dt.Columns.Add("Nota Credito");
                dt.Columns.Add("Fecha");
                dt.Columns.Add("Usuario");
                dt.Columns.Add("HBL");
                dt.Columns.Add("MBL");
                dt.Columns.Add("Valor");
                dt.Columns.Add("Moneda");
                dt.Columns.Add("Valor Equivalente");
                foreach (RE_GenericBean rgb in arr)
                {
                    object[] objArr = { rgb.intC1, rgb.strC1 + rgb.intC2, rgb.strC2, rgb.strC4, rgb.strC7, rgb.strC8, rgb.strC6, Utility.TraducirMonedaInt(rgb.intC3), rgb.strC9 };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("FAIntercompany"))
            {
                //dt.Columns.Add("ID");
                //dt.Columns.Add("Factura");
                //dt.Columns.Add("Fecha");
                //dt.Columns.Add("Usuario");
                //dt.Columns.Add("HBL");
                //dt.Columns.Add("MBL");
                //dt.Columns.Add("Valor");
                //dt.Columns.Add("Moneda");
                //dt.Columns.Add("Valor Equivalente");

                dt.Columns.Add("ID");
                dt.Columns.Add("Factura");
                dt.Columns.Add("Fecha");
                dt.Columns.Add("Usuario");
                dt.Columns.Add("HBL");
                dt.Columns.Add("MBL");
                dt.Columns.Add("Abonos (NC)");
                dt.Columns.Add("Total Factura");
                dt.Columns.Add("Total a Cobrar");
                dt.Columns.Add("Total Equivalente");
                dt.Columns.Add("Moneda");
                foreach (RE_GenericBean rgb in arr)
                {
                    //object[] objArr = { rgb.intC1, rgb.strC1 + " - " + rgb.intC2, rgb.strC2, rgb.strC4, rgb.strC7, rgb.strC8, rgb.strC6, Utility.TraducirMonedaInt(rgb.intC3), rgb.strC9 };
                    object[] objArr = { rgb.intC1, rgb.strC1 + " - " + rgb.intC2, rgb.strC2, rgb.strC4, rgb.strC7, rgb.strC8, rgb.strC10, rgb.strC6, Math.Round((Convert.ToDouble(rgb.strC6)) - (Convert.ToDouble(rgb.strC10)), 2), rgb.strC9, Utility.TraducirMonedaInt(rgb.intC3) };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("NCPendProveedor"))
            {
                dt.Columns.Add("ID");
                dt.Columns.Add("Nota Credito");
                dt.Columns.Add("Fecha");
                dt.Columns.Add("Usuario");
                dt.Columns.Add("HBL");
                dt.Columns.Add("MBL");
                dt.Columns.Add("Valor");
                dt.Columns.Add("Moneda");
                dt.Columns.Add("Valor Equivalente");
                dt.Columns.Add("ttt");
                foreach (RE_GenericBean rgb in arr)
                {
                    object[] objArr = { rgb.intC1, rgb.strC1 + rgb.intC2, rgb.strC2, rgb.strC4, rgb.strC6, rgb.strC7, rgb.decC1.ToString("#,#.00"), Utility.TraducirMonedaInt(rgb.intC3), rgb.decC2.ToString("#,#.00"), rgb.intC4.ToString() };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("corteproveedor"))
            {
                dt.Columns.Add("CID");
                dt.Columns.Add("Serie");
                dt.Columns.Add("Correlativo");
                //dt.Columns.Add("Cheque");
                dt.Columns.Add("Fecha");
                dt.Columns.Add("Usuario creador");
                dt.Columns.Add("Moneda");
                dt.Columns.Add("Total");
                dt.Columns.Add("Total Equivalente");

                foreach (RE_GenericBean rgb in arr)
                {
                    object[] objArr = { rgb.intC1, rgb.strC1, rgb.intC2, rgb.strC2, rgb.strC3, rgb.strC4, rgb.decC1.ToString("#,#.00"), rgb.decC2.ToString("#,#.00") };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("recibosclientes_cheques"))
            {
                dt.Columns.Add("CID");
                dt.Columns.Add("Serie");
                dt.Columns.Add("Correlativo");
                //dt.Columns.Add("Cheque");
                dt.Columns.Add("Fecha");
                dt.Columns.Add("Usuario creador");
                dt.Columns.Add("Moneda");
                dt.Columns.Add("Total");
                dt.Columns.Add("Total Equivalente");

                foreach (RE_GenericBean rgb in arr)
                {
                    object[] objArr = { rgb.intC1, rgb.strC2, rgb.strC1, rgb.strC3, rgb.strC4, rgb.strC5, rgb.decC1.ToString("#,#.00"), rgb.decC2.ToString("#,#.00") };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("CortesProveedorPagados"))
            {
                dt.Columns.Add("CID");
                dt.Columns.Add("Cheque");
                dt.Columns.Add("Serie");
                dt.Columns.Add("Fecha");
                dt.Columns.Add("Usuario creador");
                dt.Columns.Add("Moneda");
                dt.Columns.Add("Total");
                //Dennis
                //dt.Columns.Add("Banco");
                //dt.Columns.Add("No.Cuenta");
                //dt.Columns.Add("No.Cheque");
                //Fin
                foreach (RE_GenericBean rgb in arr)
                {
                    object[] objArr = { rgb.intC1, rgb.intC2, rgb.strC1, rgb.strC2, rgb.strC3, rgb.strC4, rgb.decC1.ToString("#,#.00") };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("getListaCortesPagados"))
            {
                decimal saldo = 0;
                dt.Columns.Add("CID");
                dt.Columns.Add("Serie");
                dt.Columns.Add("Corte");
                dt.Columns.Add("Fecha");
                dt.Columns.Add("Usuario creador");
                dt.Columns.Add("Moneda");
                dt.Columns.Add("Total");
                dt.Columns.Add("Banco");
                dt.Columns.Add("No.Cuenta");
                dt.Columns.Add("No.");
                dt.Columns.Add("id_cheque");
                dt.Columns.Add("serieR");
                dt.Columns.Add("Abono");
                dt.Columns.Add("Saldo");
                dt.Columns.Add("Cli_id");
                foreach (RE_GenericBean rgb in arr)
                {
                    saldo = rgb.decC1 + rgb.decC2;
                    object[] objArr = { rgb.intC1, rgb.strC1, rgb.intC3, rgb.strC2, rgb.strC3, rgb.strC4, rgb.decC1.ToString("#,#.00"), rgb.strC5, rgb.strC6, rgb.strC7, rgb.intC2, rgb.strC8, rgb.decC2, saldo, rgb.intC4 };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("provisioneslist"))//Aca
            {
                dt.Columns.Add("PVNID");
                dt.Columns.Add("Proveedor");
                dt.Columns.Add("ID");
                dt.Columns.Add("Fecha Creacion");
                dt.Columns.Add("Usuario creador");
                dt.Columns.Add("Total");
                dt.Columns.Add("Moneda");
                dt.Columns.Add("OC");
                dt.Columns.Add("MBL");
                dt.Columns.Add("PROVISION");
                dt.Columns.Add("Factura Proveedor");
                string ordencompra = "";
                foreach (RE_GenericBean rgb in arr)
                {
                    if (!rgb.strC5.Equals("")) ordencompra = rgb.strC5 + rgb.intC3; else ordencompra = "";
                    string provision = "";
                    provision = rgb.strC7.ToString() + rgb.intC4.ToString();
                    string fact_corr_id = rgb.strC8 + "-" + rgb.strC9;
                    object[] objArr = { rgb.intC1, rgb.strC3, rgb.intC2, rgb.strC1, rgb.strC2, rgb.decC1.ToString("#,#.00"), rgb.strC4, ordencompra, rgb.strC6, provision, fact_corr_id };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("CajaChica"))
            {
                dt.Columns.Add("Proveedor ID");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("Trafiquer");

                foreach (RE_GenericBean rgb in arr)
                {
                    object[] objArr = { rgb.intC1, rgb.strC1, rgb.strC2 };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("cliente"))
            {
                dt.Columns.Add("ID");
                dt.Columns.Add("Nombre");
                //Dennis
                dt.Columns.Add("Direccion");
                //Fin
                foreach (RE_GenericBean rgb in arr)
                {
                    object[] objArr = { rgb.douC1, rgb.strC1, rgb.strC4 };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("retencionproveedor"))
            {
                dt.Columns.Add("ID");
                dt.Columns.Add("Factura");
                dt.Columns.Add("Monto");

                foreach (RE_GenericBean rgb in arr)
                {
                    object[] objArr = { rgb.intC1, rgb.strC1, rgb.decC1.ToString("#,#.00") };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("recibosclientes"))
            {
                dt.Columns.Add("ID");
                dt.Columns.Add("Fecha");
                dt.Columns.Add("Tipo");
                dt.Columns.Add("Monto");

                foreach (RE_GenericBean rgb in arr)
                {
                    object[] objArr = { rgb.intC1, rgb.strC1, rgb.strC2, rgb.douC1 };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("Retencion"))
            {
                dt.Columns.Add("Id");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("Tipo");
                dt.Columns.Add("Mínimo");
                dt.Columns.Add("Porcentaje");
                dt.Columns.Add("PaisID");
                foreach (RE_GenericBean rgb in arr)
                {
                    object[] objArr = { rgb.intC1, rgb.strC1, rgb.intC2, rgb.decC1, rgb.intC3, rgb.intC4 };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("Cobrador"))
            {
                dt.Columns.Add("Cobrador ID");
                dt.Columns.Add("Usuario");

                foreach (RE_GenericBean rgb in arr)
                {
                    object[] objArr = { rgb.douC1, rgb.strC1 };
                    dt.Rows.Add(objArr);
                }
            }
            else if (tipo.Equals("Intercompany"))
            {
                dt.Columns.Add("ID");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("Nit");
                dt.Columns.Add("Direccion");
                dt.Columns.Add("Pais");
                dt.Columns.Add("Tipo Regimen");
                foreach (RE_GenericBean rgb in arr)
                {
                    object[] objArr = { rgb.intC1.ToString(), rgb.strC1, rgb.strC2, rgb.strC3, rgb.strC4, rgb.intC2.ToString() };
                    dt.Rows.Add(objArr);
                }
            }

        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return dt;
    }
    /**
     * Metodo que obtiene el detalle de una provision para editarla o bien informativo para aceptarla.
     */
    public static RE_GenericBean getDetalleProvision(int provID)
    {
        RE_GenericBean result = null;
        string where = "";
        try
        {
            where = " and a.tpr_prov_id=" + provID;
            result = (RE_GenericBean)DB.getDetalleProvision(where);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return result;
    }

    public static ArrayList getFactProvision(string tipo, string criterio, UsuarioBean user)
    {
        ArrayList result = null;
        string where = "";
        try
        {
            if (tipo.Equals("XproveedorID"))
            {
                where = criterio;
                result = (ArrayList)DB.getFactProvision(where, user);
            }
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return result;
    }

    public static int TraducirMonedaStr(string ISOMoneda)
    {
        int result = 8;
        try
        {
            if (ISOMoneda.Equals("USD")) result = 8;
            if (ISOMoneda.Equals("GTQ")) result = 1;
            if (ISOMoneda.Equals("CRC")) result = 5;
            if (ISOMoneda.Equals("HNL")) result = 3;
            if (ISOMoneda.Equals("LPS")) result = 3;
            if (ISOMoneda.Equals("BZD")) result = 7;
            if (ISOMoneda.Equals("NIO")) result = 4;
            if (ISOMoneda.Equals("NIC")) result = 4;
            if (ISOMoneda.Equals("C$")) result = 4;
            if (ISOMoneda.Equals("EUR")) result = 9;
            if (ISOMoneda.Equals("PAB")) result = 6;
            if (ISOMoneda.Equals("MXN")) result = 10;
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -1;
        }
        return result;
    }

    public static string TraducirMonedaInt(int monID)
    {
        string result = "";
        try
        {
            if (monID == 1) result = "GTQ";
            if (monID == 2) result = "USD";
            if (monID == 3) result = "HNL";
            //if (monID == 4) result = "NIO";
            if (monID == 4) result = "C$";
            if (monID == 5) result = "CRC";
            if (monID == 6) result = "PAB";
            //if (monID == 7) result = "BZ$";
            if (monID == 7) result = "BZD";
            if (monID == 8) result = "USD";

        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return "";
        }
        return result;
    }
    public static string  CodigoDistribucionPicking(int paiID)
    {
        string result = "";
        #region Backup
        //try
        //{
        //    if (paiID == 1) result = "1,2";//Guatemala
        //    if (paiID == 2) result = "5,6,10";//SV
        //    if (paiID == 9) result = "5,6,10";//SV2
        //    if (paiID == 3) result = "0,7";//HN
        //    if (paiID == 4) result = "3,4";//NI
        //    if (paiID == 5) result = "8";//CR
        //    if (paiID == 6) result = "";//PA
        //    if (paiID == 7) result = "";//BZ
        //    if (paiID == 11) result = "3,4";//GRH
        //    if (paiID == 12) result = "3,4";//ISI
        //    if (paiID == 13) result = "3,4";//MAYAN
        //    if (paiID == 14) result = "1,2";//APROA
        //}
        //catch (Exception e)
        //{
        //    log4net ErrLog = new log4net();
        //    ErrLog.ErrorLog(e.Message);
        //    return "";
        //}
        #endregion
        result = DB.Obtener_Codigo_Regimen_Por_Pais(paiID);
        return result;
    }

    public static int ISOPaistoInt(string ISOPais)
    {
        int result = 0;
        try
        {
            if (ISOPais.Equals("GT")) result = 1;
            if (ISOPais.Equals("SV")) result = 2;
            if (ISOPais.Equals("HN")) result = 3;
            if (ISOPais.Equals("NI")) result = 4;
            if (ISOPais.Equals("N1")) result = 11;
            if (ISOPais.Equals("CR")) result = 5;
            if (ISOPais.Equals("PA")) result = 6;
            if (ISOPais.Equals("BZ")) result = 7;
            if (ISOPais.Equals("US")) result = 8;
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -1;
        }
        return result;
    }

    public static string InttoISOPais(int paiID)
    {
        string result = "";
        try
        {
            if (paiID == 1) result = "GT";
            if (paiID == 2) result = "SV";
            if (paiID == 3) result = "HN";
            if (paiID == 4) result = "NI";
            if (paiID == 5) result = "CR";
            if (paiID == 6) result = "PA";
            if (paiID == 7) result = "BZ";
            if (paiID == 8) result = "US";
            if (paiID == 14) result = "GT";
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return result;
    }

    public static string TraducirServiciotoSTR(int servID)
    {
        string result = "";
        try
        {
            if (servID == 1) result = "FCL";
            if (servID == 2) result = "LCL";
            if (servID == 3) result = "AEREO";
            if (servID == 4) result = "APL";
            if (servID == 5) result = "TRANSPORTE LOCAL";
            if (servID == 6) result = "SEGUROS";
            if (servID == 7) result = "PUERTOS";
            if (servID == 8) result = "APL LOGISTICS";
            if (servID == 9) result = "ADUANAS";
            if (servID == 10) result = "ALMACENADORA";
            if (servID == 11) result = "INSPECTOR";
            if (servID == 12) result = "PO BOX";
            if (servID == 13) result = "ADMINISTRATIVO";
            if (servID == 14) result = "TERCEROS";
            if (servID == 15) result = "INTERMODAL";
            if (servID == 16) result = "FCL ECONO";
            if (servID == 17) result = "LCL ECONO";
            if (servID == 18) result = "FACTURACION APL";
            if (servID == 19) result = "ZONA LIBRE";
            if (servID == 20) result = "LOGISTICA IOR / EOR";
            if (servID == 21) result = "INTERMODAL FTL";
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return result;
    }

    public static int TraducirServiciotoINT(string servID)
    {
        int result = 0;
        try
        {
            if (servID.Equals("FCL")) result = 1;
            if (servID.Equals("LCL")) result = 2;
            if (servID.Equals("AEREO")) result = 3;
            if (servID.Equals("APL")) result = 4;
            if (servID.Equals("TRANSPORTE LOCAL")) result = 5;
            if (servID.Equals("SEGUROS")) result = 6;
            if (servID.Equals("PUERTOS")) result = 7;
            if (servID.Equals("APL LOGISTICS")) result = 8;
            if (servID.Equals("ADUANAS")) result = 9;
            if (servID.Equals("ALMACENADORA")) result = 10;
            if (servID.Equals("INSPECTOR")) result = 11;
            if (servID.Equals("PO BOX")) result = 12;
            if (servID.Equals("ADMINISTRATIVO")) result = 13;
            if (servID.Equals("TERCEROS")) result = 14;
            if (servID.Equals("INTERMODAL")) result = 15;
            if (servID.Equals("FCL ECONO")) result = 16;
            if (servID.Equals("LCL ECONO")) result = 17;
            if (servID.Equals("FACTURACION APL")) result = 18;
            if (servID.Equals("ZONA LIBRE")) result = 19;
            if (servID.Equals("LOGISTICA IOR / EOR")) result = 20;
            if (servID.Equals("INTERMODAL FTL")) result = 21;

        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -1;
        }
        return result;
    }

    public static string TraducirOperaciontoSTR(int servID)
    {
        string result = "";
        try
        {
            if (servID == 1) result = "FACTURA";
            if (servID == 2) result = "RECIBO";
            if (servID == 3) result = "NOTA CREDITO";
            if (servID == 4) result = "NOTA DEBITO";
            if (servID == 5) result = "PROVISION";
            if (servID == 6) result = "CHEQUE";
            if (servID == 7) result = "ORDEN COMPRA";
            if (servID == 8) result = "CONTRASEÑA";
            if (servID == 9) result = "RETENCION";
            if (servID == 10) result = "PROVISION";
            if (servID == 11) result = "CORTE";
            if (servID == 12) result = "NOTA CREDITO";
            if (servID == 13) result = "CHEQUE";
            if (servID == 14) result = "PROFORMA";
            if (servID == 31) result = "NOTA CREDITO A NOTA DEBITO";
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return result;
    }
    public static string MesfromIntToStr_english(int mes)
    {
        string result = "";
        try
        {
            if (mes == 1) result = "January";
            if (mes == 2) result = "February";
            if (mes == 3) result = "March";
            if (mes == 4) result = "April";
            if (mes == 5) result = "May";
            if (mes == 6) result = "June";
            if (mes == 7) result = "July";
            if (mes == 8) result = "August";
            if (mes == 9) result = "September";
            if (mes == 10) result = "October";
            if (mes == 11) result = "November";
            if (mes == 12) result = "December";
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return result;
    }

    public static string MesfromIntToStr(int mes)
    {
        string result = "";
        try
        {
            if (mes == 1) result = "Enero";
            if (mes == 2) result = "Febrero";
            if (mes == 3) result = "Marzo";
            if (mes == 4) result = "Abril";
            if (mes == 5) result = "Mayo";
            if (mes == 6) result = "Junio";
            if (mes == 7) result = "Julio";
            if (mes == 8) result = "Agosto";
            if (mes == 9) result = "Septiembre";
            if (mes == 10) result = "Octubre";
            if (mes == 11) result = "Noviembre";
            if (mes == 12) result = "Diciembre";
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return result;
    }

    public static int TraducirOperaciontoint(string servID)
    {
        int result = 0;
        try
        {
            if (servID.Equals("FACTURA")) result = 1;
            if (servID.Equals("RECIBO")) result = 2;
            // if (servID.Equals("NOTA CREDITO")) result = 3;
            if (servID.Equals("NOTA DEBITO")) result = 4;
            // if (servID.Equals("FACTURA")) result = 5;
            if (servID.Equals("CHEQUE")) result = 6;
            if (servID.Equals("ORDEN COMPRA")) result = 7;
            if (servID.Equals("CONTRASEÑA")) result = 8;
            if (servID.Equals("RETENCION")) result = 9;
            if (servID.Equals("PROVISION")) result = 5;
            if (servID.Equals("CORTE")) result = 11;
            if (servID.Equals("NOTA CREDITO")) result = 12;
            if (servID.Equals("CHEQUE")) result = 13;
            if (servID.Equals("PROFORMA")) result = 14;
            if (servID.Equals("NOTA CREDITO A NOTA DEBITO")) result = 31;
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -1;
        }
        return result;
    }

    public static string GetReferenciaLibroDiario(int tranID, int refNo)
    {
        string result = "";
        try
        {
            result = "";//DB.getSegunReferecia(tranID, refNo);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return "";
        }
        return result;
    }
}