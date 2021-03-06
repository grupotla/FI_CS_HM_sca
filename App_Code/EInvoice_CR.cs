using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;
using System.Data;
using Npgsql;
using System.IO;

/// <summary>
/// Summary description for EInvoice_CR
/// </summary>
public class EInvoice_CR
{

    XmlDocument Invoice = null;
    XDocument XML_Invoice = null;
    XElement Nodo_root = null;
    XElement Nodo_FacturaElectronicaXML = null;
    XElement Nodo_Encabezado = null;
    XElement Nodo_Detalle = null;
    XElement Nodo_Linea = null;
    XElement Nodo_Impuesto = null;
    XElement Nodo_InformacionDeReferencia = null;
    XElement Nodo_Totales = null;
    XElement Nodo_Otros = null;
    decimal total_servicios_gravados = 0;
    decimal total_servicios_exentos = 0;
    decimal total_mercancias_gravadas = 0;
    decimal total_mercancias_exentas = 0;
    decimal total_gravado = 0;
    decimal total_exento = 0;
    decimal total_venta = 0;
    decimal total_descuentos = 0;
    decimal total_venta_neta = 0;
    decimal total_impuesto = 0;
    decimal total_comprobante = 0;

    string _NumCuenta = string.Empty;
    string _emailUSER = string.Empty;
    string _passUSER = string.Empty;

	public EInvoice_CR(int paisID)
	{
        switch (paisID)
        {
            case 5:
                {
                    //Produccion
                    _NumCuenta = "4620";
                    _emailUSER = "3101123041";
                    _passUSER = "Aimar@123";

                    //Pruebas
                    //_NumCuenta = "3112";
                    //_emailUSER = "robin-sanchez@aimargroup.com";
                    //_passUSER = "PaSs@123";
                    break;
                }
            case 21:
                {
                    //Produccion
                    _NumCuenta = "37957";
                    _emailUSER = "111080396";
                    _passUSER = "Latin#2018";

                    //Pruebas
                    //_NumCuenta = "4515";
                    ////_emailUSER = "111080396";
                    //_emailUSER = "carol-morales@aimargroup.com";                    
                    //_passUSER = "Prueba#2018";
                    break;
                }
            default:
                break;
        }
	}
    public ArrayList Crear_Factura(int tfaID)
    {
        ArrayList Arr_XML = new ArrayList();
        int ttrID = 1;
        decimal tipo_cambio = 1;
        UsuarioBean user = new UsuarioBean();
        Invoice = new XmlDocument();
        XML_Invoice = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
        try
        {
            #region Capturar Data Factura
            #region Encabezado Factura

            RE_GenericBean Factura_BD = (RE_GenericBean)DB.getFacturaData(tfaID);
            FacturaBean FacturaElectronica = new FacturaBean();
            #region Construir Usuario
            user.ID = Factura_BD.strC8;
            user.Contrasena = "";
            user.PaisID = Factura_BD.intC8;
            user.contaID = Factura_BD.intC9;
            user.pais = (PaisBean)DB.getPais(user.PaisID);
            user.Moneda = DB.getMonedaByPaisConta(user);
            user.Idioma = DB.getIdiomaByPaisConta(user);
            user.Aplicaciones = (Hashtable)DB.getPerfilesbyUser(user.ID);
            user.Departamento = (ArrayList)DB.getUsuarioDepartamentoAimar(user.ID, user.PaisID);
            user.SucursalID = Factura_BD.intC2;
            #endregion
            FacturaElectronica.Fecha_Emision = Factura_BD.strC5.Replace("/", "-");
            FacturaElectronica.Fecha_Pago = Factura_BD.strC6.Replace("/", "-");
            FacturaElectronica.Nit = Factura_BD.strC2.Replace("-", "");
            FacturaElectronica.Nombre = Factura_BD.strC3;
            FacturaElectronica.Direccion = Factura_BD.strC4;
            FacturaElectronica.allIN = Factura_BD.strC30;
            FacturaElectronica.ReciboAduanal = Factura_BD.strC31;
            FacturaElectronica.Recibo_Agencia = Factura_BD.strC42;
            FacturaElectronica.Valor_Aduanero = Factura_BD.strC43;
            FacturaElectronica.SubTot = Convert.ToDouble(Factura_BD.decC1);
            FacturaElectronica.Impuesto = Convert.ToDouble(Factura_BD.decC2);
            FacturaElectronica.Total = Convert.ToDouble(Factura_BD.decC3);
            FacturaElectronica.MonedaID = Factura_BD.intC4;
            if (FacturaElectronica.MonedaID == 8)
            {
                tipo_cambio = DB.getTipoCambioByDay(user, FacturaElectronica.Fecha_Emision);
            }
            FacturaElectronica.Observaciones = Factura_BD.strC7;
            FacturaElectronica.HBL = Factura_BD.strC9;
            FacturaElectronica.MBL = Factura_BD.strC10;
            FacturaElectronica.Contenedor = Factura_BD.strC11;
            FacturaElectronica.Routing = Factura_BD.strC12;
            FacturaElectronica.Naviera = Factura_BD.strC13;
            FacturaElectronica.Vapor = Factura_BD.strC14;
            FacturaElectronica.Shipper = Factura_BD.strC15;
            FacturaElectronica.OrdenPO = Factura_BD.strC16;
            FacturaElectronica.Consignee = Factura_BD.strC17;
            FacturaElectronica.Comodity = Factura_BD.strC18;
            FacturaElectronica.Paquetes = Factura_BD.strC32;
            FacturaElectronica.cantPaquetes = Factura_BD.strC19;
            FacturaElectronica.Peso = Factura_BD.strC20;
            FacturaElectronica.Volumen = Factura_BD.strC21;
            FacturaElectronica.Dua_Ingreso = Factura_BD.strC22;
            FacturaElectronica.Dua_Salida = Factura_BD.strC23;
            FacturaElectronica.Vendedor1 = Factura_BD.strC24;
            FacturaElectronica.Vendedor2 = Factura_BD.strC25;
            FacturaElectronica.Razon = Factura_BD.strC26;
            FacturaElectronica.Serie = Factura_BD.strC28;
            FacturaElectronica.Nombre_Agente = Factura_BD.strC41;
            FacturaElectronica.Regimen_Aduanero = Factura_BD.intC11;
            int Serie_ID = DB.getDocumentoID(user.SucursalID, FacturaElectronica.Serie, ttrID, user);
            FacturaElectronica.serieID = Serie_ID;
            if (Factura_BD.strC58 == "3")
            {
                RE_GenericBean Data_Cliente = DB.getDataClient(Convert.ToDouble(Factura_BD.intC3));
                FacturaElectronica.Correo_Electronico = Data_Cliente.strC8;
            }
            else if (Factura_BD.strC58 == "10")
            {
                FacturaElectronica.Correo_Electronico = "";
            }
            FacturaElectronica.Factura_Electronica = int.Parse(Factura_BD.strC50);
            FacturaElectronica.Fecha_Emision = DateTime.Parse((Factura_BD.strC5)).AddMinutes(-2).ToString("yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            FacturaElectronica.Fecha_Pago = DateTime.Parse((Factura_BD.strC6)).AddYears(1).ToString("yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            FacturaElectronica.Tipo_Persona = int.Parse(Factura_BD.strC58);
            TimeSpan ts = DateTime.Parse(FacturaElectronica.Fecha_Pago) - DateTime.Parse(FacturaElectronica.Fecha_Emision);
            int dias_credito = ts.Days; 
            #endregion
            #region Construir Detalle Factura
            GridView gv_detalle = new GridView();
            gv_detalle.DataSource = (DataTable)DB.getRubbyFact(Factura_BD.intC1, ttrID);
            gv_detalle.DataBind();
            Rubros rubro;
            XML_Bean Bean_IVA = new XML_Bean();
            XML_Bean Bean_IVACERO = new XML_Bean();
            foreach (GridViewRow row in gv_detalle.Rows)
            {
                rubro = new Rubros();
                rubro.rubroID = long.Parse(row.Cells[0].Text);
                rubro.rubroName = row.Cells[1].Text;
                rubro.rubtoType = row.Cells[2].Text;
                rubro.rubroSubTot = double.Parse(row.Cells[5].Text);
                rubro.rubroImpuesto = double.Parse(row.Cells[6].Text);
                rubro.rubroTot = double.Parse(row.Cells[7].Text);
                rubro.rubroCommentario = System.Net.WebUtility.HtmlDecode(row.Cells[8].Text);
                if (FacturaElectronica.RubrosArr == null) FacturaElectronica.RubrosArr = new ArrayList();
                FacturaElectronica.RubrosArr.Add(rubro);
            }
            #endregion
            #endregion

            #region Observaciones Electronica
            string Observaciones = "";
            if (FacturaElectronica.HBL.Trim() != "")
            {
                Observaciones += "HBL:" + FacturaElectronica.HBL + " ";
            }
            if (FacturaElectronica.Routing.Trim() != "")
            {
                Observaciones += "ROUTING:" + FacturaElectronica.Routing + " ";
            }
            if (FacturaElectronica.Contenedor.Trim() != "")
            {
                Observaciones += "CONTENEDOR:" + FacturaElectronica.Contenedor + " ";
            }
            if (FacturaElectronica.Shipper.Trim() != "")
            {
                Observaciones += "SHIPPER:" + FacturaElectronica.Shipper + " ";
            }
            if (FacturaElectronica.Paquetes.Trim() != "")
            {
                Observaciones += "PAQUETES: " + FacturaElectronica.cantPaquetes.ToString() + " - " + FacturaElectronica.Paquetes + " ";
            }
            if ((FacturaElectronica.Peso.Trim() != "") || (FacturaElectronica.Volumen.Trim() != ""))
            {
                Observaciones += "PESO:" + FacturaElectronica.Peso + " VOLUMEN.: " + FacturaElectronica.Volumen + " ";
            }
            if ((FacturaElectronica.Dua_Ingreso.Trim() != "") || (FacturaElectronica.Dua_Salida.Trim() != ""))
            {
                Observaciones += "DUA INGRESO:" + FacturaElectronica.Dua_Ingreso + " DUA SALIDA.: " + FacturaElectronica.Dua_Salida + " ";
            }
            if (FacturaElectronica.OrdenPO.Trim() != "")
            {
                Observaciones += "POLIZA:" + FacturaElectronica.OrdenPO + " ";
            }
            if (FacturaElectronica.Regimen_Aduanero.ToString() != "0")
            {
                Observaciones += "REGIMEN ADUANERO:" + DB.Get_Regimen_Aduanero_XID(1, FacturaElectronica.Regimen_Aduanero).ToString() + " ";
            }
            if ((Factura_BD.intC3.ToString().Trim() != "") && (Factura_BD.intC3.ToString().Trim() != "0"))
            {
                Observaciones += "CODIGO DE CLIENTE:" + Factura_BD.intC3.ToString().Trim() + " ";
            }
            if (FacturaElectronica.Observaciones.Trim() != "")
            {
                Observaciones += "OBSERVACIONES:" + FacturaElectronica.Observaciones + " ";
            }
            Observaciones += "Estimado Cliente, a partir de la fecha de emision de la factura tiene 15 dias calendario para realizar cualquier reclamo sobre la misma ";
            #endregion

            #region Determinar Tipo de Identificacion Tributaria
            string _TipoIdentificacion = "";
            if (Factura_BD.strC58 == "3")
            {
                string criterio = "";
                ArrayList clientearr = null;
                RE_GenericBean clienteBean = null;
                criterio = " a.id_cliente=" + Factura_BD.intC3.ToString();
                clientearr = (ArrayList)DB.getClientes(criterio, user, "");
                if ((clientearr != null) && (clientearr.Count > 0))
                {
                    clienteBean = (RE_GenericBean)clientearr[0];
                    _TipoIdentificacion = clienteBean.strC10;
                }
            }
            else if (Factura_BD.strC58 == "10")
            {
                _TipoIdentificacion = "10";
            }
            #endregion
            #region Filtrar Identificacion Tributaria
            if (FacturaElectronica.Tipo_Persona == 3)
            {
                FacturaElectronica.Nit = FacturaElectronica.Nit.Trim();
                FacturaElectronica.Nit = FacturaElectronica.Nit.Replace("-", "");
                if (FacturaElectronica.Nit.Length > 10)
                {
                    FacturaElectronica.Nit = FacturaElectronica.Nit.Substring(0, 10);
                }
            }
            else
            {
                FacturaElectronica.Nit = FacturaElectronica.Nit.Trim();
            }
            #endregion
            #region Copia Electronica de Cortesia
            string _CopiaCortesia = "";
            if ((FacturaElectronica.Correo_Electronico.Trim().Length > 0) && (FacturaElectronica.Correo_Electronico.Trim() != "-"))
            {
                FacturaElectronica.Correo_Electronico = FacturaElectronica.Correo_Electronico.Trim();
                FacturaElectronica.Correo_Electronico = FacturaElectronica.Correo_Electronico.Replace(",", ";");
                //_CopiaCortesia = FacturaElectronica.Correo_Electronico + " ; " + user.ID + "@aimargroup.com";
                _CopiaCortesia = FacturaElectronica.Correo_Electronico;
            }
            else
            {
                _CopiaCortesia = user.ID + "@aimargroup.com";
            }
            #endregion

            #region Construir XML
            var condicionVenta = Traducir_Condiciones_Venta_Hacienda(user, Convert.ToInt32(FacturaElectronica.CliID), FacturaElectronica.Tipo_Persona);

            Nodo_Encabezado = new XElement("Encabezado",
                new XElement("NumeroFactura", tfaID),
                new XElement("FechaFactura", FacturaElectronica.Fecha_Emision),
                new XElement("Emisor",
                    new XElement("NumCuenta", _NumCuenta)
                    ),
				new XElement("CodigoActividad", "630101"),
                new XElement("TipoCambio", tipo_cambio),
                new XElement("TipoDoc", Traducir_Tipo_Documento_Hacienda(ttrID, _TipoIdentificacion)),
                //new XElement("NumConsecutivoCompr", ""),
                new XElement("CondicionVenta", condicionVenta),
                //new XElement("NumOrdenCompra", ""),
                new XElement("Moneda", Traducir_Moneda_Hacienda(FacturaElectronica.MonedaID)),
                new XElement("idMedioPago", "99"),
                condicionVenta == "02" ? new XElement("DiasCredito", dias_credito) : null,
                new XElement("Sucursal", "1"),
                new XElement("Terminal", "1"),
                new XElement("FechaVencimiento", FacturaElectronica.Fecha_Pago),
                new XElement("SituacionEnvio", "1"),
                new XElement("Receptor",
                    new XElement("TipoIdentificacion", _TipoIdentificacion),
                    new XElement("IdentificacionReceptor", FacturaElectronica.Nit),
                    new XElement("NombreReceptor", FacturaElectronica.Nombre),
                    new XElement("idProvincia", "1"),
                    new XElement("idCanton", "1"),
                    new XElement("idDistrito", "1"),
                    new XElement("idBarrio", "1"),
                    new XElement("DireccionReceptor", FacturaElectronica.Direccion),
                    //new XElement("NumeroAreaTelReceptor", "506"),
                    //new XElement("NumeroTelReceptor", "12345678"),
                    new XElement("CorreoElectronicoReceptor", _CopiaCortesia),
                    new XElement("CopiaCortesia", "cr-felectronica@aimargroup.com")
                    //new XElement("CorreoElectronicoReceptor", "soporte7@aimargroup.com"),
                    //new XElement("CopiaCortesia", "soporte7@aimargroup.com")
                    )
                );

        Nodo_Detalle = new XElement("Detalle");
        foreach (Rubros Rubro in FacturaElectronica.RubrosArr)
        {
            Nodo_Linea = new XElement("Linea",
                //new XElement("Tipo", "S"), //2019-07-01
                new XElement("CodigoProducto", Rubro.rubroID),
                new XElement("Cantidad", "1"),
                new XElement("UnidadMedida", "24"), //2019-07-01
                new XElement("DetalleMerc", Rubro.rubroName),
                new XElement("PrecioUnitario", Rubro.rubroSubTot.ToString("##.00000"))
                //new XElement("MontoDescuento", (0).ToString("##.00000")),
                //new XElement("NaturalezaDescuento", "")
            );

            if (Rubro.rubroImpuesto > 0)
            {
                var _CodigoImpuesto_Temporal = "01";
                double valor_impuesto = 0;
                valor_impuesto = Rubro.rubroSubTot * 0.13;
                valor_impuesto = double.Parse(valor_impuesto.ToString("F"));
                Rubro.rubroImpuesto = valor_impuesto;
                Nodo_Impuesto = new XElement("Impuestos",
                     new XElement("Impuesto",
                         new XElement("CodigoImpuesto", _CodigoImpuesto_Temporal),
						 new XElement("CodigoTarifa", "08"), //2019-07-01
                         new XElement("PorcentajeImpuesto", "13.00"),
                         new XElement("MontoImpuesto", Rubro.rubroImpuesto.ToString("##.00000"))
                     )
                 );
                Nodo_Linea.Add(Nodo_Impuesto);
                total_servicios_gravados += decimal.Parse(Rubro.rubroSubTot.ToString());
                total_impuesto += decimal.Parse(Rubro.rubroImpuesto.ToString());
            }
            else
            {
                total_servicios_exentos += decimal.Parse(Rubro.rubroSubTot.ToString());
            }
            Nodo_Detalle.Add(Nodo_Linea);
        }

        //Nodo_InformacionDeReferencia = new XElement("InformacionDeReferencia",
        //    new XElement("Referencia",
        //        new XElement("TpoDocRef", "0"),
        //        new XElement("NumeroReferencia", "0"),
        //        new XElement("CodigoReferencia", "0")
        //    )
        //);
        total_gravado = total_servicios_gravados;
        total_exento = total_servicios_exentos;
        total_venta = total_gravado + total_exento;
        total_venta_neta = total_venta;
        total_comprobante = total_venta_neta + total_impuesto;
        Nodo_Totales = new XElement("Totales",
			new XElement("TotalServGravados", total_servicios_gravados.ToString("##0.00000")),
			new XElement("TotalServExentos", total_servicios_exentos.ToString("##0.00000")),
			new XElement("TotalServExonerados", "0.00000"), //2019-07-01
			new XElement("TotalMercanciasGravadas", total_mercancias_gravadas.ToString("##0.00000")),
			new XElement("TotalMercanciasExentas", total_mercancias_exentas.ToString("##0.00000")),
			new XElement("TotalMercanciasExoneradas", "0.00000"), //2019-07-01                
			new XElement("TotalGravado", total_gravado.ToString("##0.00000")),
			new XElement("TotalExento", total_exento.ToString("##0.00000")),
			new XElement("TotalExonerado", "0.00000"), //2019-07-01                
			new XElement("TotalOtrosCargos", "0.00000"), //2019-07-01                
			new XElement("TotalVenta", total_venta.ToString("##0.00000")),
			new XElement("TotalDescuentos", total_descuentos.ToString("##0.00000")),
			new XElement("TotalIVADevuelto", "0.00000"), //2019-07-01                
			new XElement("TotalVentaNeta", total_venta_neta.ToString("##0.00000")),
			new XElement("TotalImpuesto", total_impuesto.ToString("##0.00000")),
			new XElement("TotalComprobante", total_comprobante.ToString("##0.00000"))
        );

        Nodo_Otros = new XElement("Otros", Observaciones);

        Nodo_FacturaElectronicaXML = new XElement("FacturaElectronicaXML");
        Nodo_FacturaElectronicaXML.Add(Nodo_Encabezado);
        Nodo_FacturaElectronicaXML.Add(Nodo_Detalle);
        //Nodo_FacturaElectronicaXML.Add(Nodo_InformacionDeReferencia);
        Nodo_FacturaElectronicaXML.Add(Nodo_Totales);
        Nodo_FacturaElectronicaXML.Add(Nodo_Otros);

        Nodo_root = new XElement("root");
        Nodo_root.Add(Nodo_FacturaElectronicaXML);
        XML_Invoice.Add(Nodo_root);
        Invoice.InnerXml = XML_Invoice.ToString();

        string Path = "D:\\\\FACTURA_CR" + tfaID + ".xml";
        XmlTextWriter Writer = new XmlTextWriter(Path, new UTF8Encoding(false));
        Invoice.Save(Writer);
        Writer.Flush();
        Writer.Close();
        XML_Invoice = null;
        
        #endregion

            Arr_XML = new ArrayList();
            Arr_XML.Add("1");
            Arr_XML.Add(Invoice);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            Arr_XML = new ArrayList();
            Arr_XML.Add("0");
            Arr_XML.Add("Existio un error al crear XML de la Factura.: " + e.Message + " ");
        }
        return Arr_XML;
    }
    public ArrayList Crear_Nota_Debito(int tndID)
    {
        ArrayList Arr_XML = new ArrayList();
        int ttrID = 4;
        decimal tipo_cambio = 1;
        UsuarioBean user = new UsuarioBean();
        Invoice = new XmlDocument();
        XML_Invoice = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
        try
        {
            #region Capturar Data Nota de Debito
            #region Encabezado Nota Debito
            RE_GenericBean NotaDebito_BD = (RE_GenericBean)DB.getNotaDebitoData(tndID);
            RE_GenericBean NotaDebitoElectronica = new RE_GenericBean();
            #region Construir Usuario
            user.ID = NotaDebito_BD.strC5;
            user.Contrasena = "";
            user.PaisID = NotaDebito_BD.intC2;
            user.contaID = NotaDebito_BD.intC10;
            user.pais = (PaisBean)DB.getPais(user.PaisID);
            user.Moneda = DB.getMonedaByPaisConta(user);
            user.Idioma = DB.getIdiomaByPaisConta(user);
            user.Aplicaciones = (Hashtable)DB.getPerfilesbyUser(user.ID);
            user.Departamento = (ArrayList)DB.getUsuarioDepartamentoAimar(user.ID, user.PaisID);
            user.SucursalID = NotaDebito_BD.intC5;
            #endregion
            NotaDebitoElectronica.intC1 = NotaDebito_BD.intC4;//MonedaID
            if (NotaDebitoElectronica.intC1 == 8)
            {
                tipo_cambio = DB.getTipoCambioByDay(user, DateTime.Parse(NotaDebito_BD.strC3).ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
            }
            NotaDebitoElectronica.intC3 = user.contaID;//ContaID
            NotaDebitoElectronica.intC4 = 0;//Factura ID;
            NotaDebitoElectronica.intC5 = 0;//tipo de transaccion
            NotaDebitoElectronica.intC6 = NotaDebito_BD.intC7;//tipo de persona
            NotaDebitoElectronica.intC7 = Convert.ToInt32(NotaDebito_BD.douC1);//Codigo Cliente
            NotaDebitoElectronica.strC1 = NotaDebito_BD.strC4;//Observaciones
            NotaDebitoElectronica.strC2 = "";//Serie Factura
            NotaDebitoElectronica.strC3 = "";//Correlativo Factura
            NotaDebitoElectronica.strC20 = NotaDebito_BD.strC28;//Serie ND
            NotaDebitoElectronica.strC35 = NotaDebito_BD.strC1.Replace("-", "");
            NotaDebitoElectronica.strC36 = NotaDebito_BD.strC53;//tnd_correo_documento_electronico
            NotaDebitoElectronica.strC37 = NotaDebito_BD.strC54;//tnc_referencia_correo
            NotaDebitoElectronica.strC38 = NotaDebito_BD.strC30;//All in
            NotaDebitoElectronica.strC9 = NotaDebito_BD.strC7;//HBL
            NotaDebitoElectronica.strC10 = NotaDebito_BD.strC8;//MBL
            NotaDebitoElectronica.strC11 = NotaDebito_BD.strC9;//CONTENEDOR
            NotaDebitoElectronica.strC12 = NotaDebito_BD.strC12;//ROUTING
            NotaDebitoElectronica.strC13 = NotaDebito_BD.strC33;//Agente
            NotaDebitoElectronica.strC14 = NotaDebito_BD.strC13;//Naviera
            NotaDebitoElectronica.strC15 = NotaDebito_BD.strC15;//Shipper
            NotaDebitoElectronica.strC16 = NotaDebito_BD.strC17;//Consignatario
            NotaDebitoElectronica.strC17 = NotaDebito_BD.strC14;//Vapor
            NotaDebitoElectronica.strC18 = NotaDebito_BD.strC20;//Peso
            NotaDebitoElectronica.strC19 = NotaDebito_BD.strC21;//Volumen
            NotaDebitoElectronica.strC21 = NotaDebito_BD.strC22;//Dua Ingreso
            NotaDebitoElectronica.strC22 = NotaDebito_BD.strC23;//Dua Salida

            NotaDebitoElectronica.decC1 = NotaDebito_BD.decC1;//tnd_total
            NotaDebitoElectronica.decC3 = NotaDebito_BD.decC3;//tnd_impuesto                
            NotaDebitoElectronica.decC4 = NotaDebito_BD.decC2;//tnd_subtotal

            int Doc_ID = DB.getDocumentoID(user.SucursalID, NotaDebitoElectronica.strC20, 4, user);
            NotaDebitoElectronica.intC8 = Doc_ID;//Serie ID
            NotaDebitoElectronica.Nombre_Cliente = NotaDebito_BD.strC2;//Nombre del Cliente
            NotaDebitoElectronica.Direccion = NotaDebito_BD.strC6;//Direccion
            NotaDebitoElectronica.Estado = 0;//Estado de Factura
            NotaDebitoElectronica.Factura_Ref_ID = NotaDebito_BD.intC12;
            NotaDebitoElectronica.Factura_Ref_Serie = NotaDebito_BD.strC57;
            NotaDebitoElectronica.Factura_Ref_Correlativo = NotaDebito_BD.intC13;
            NotaDebitoElectronica.Factura_Ref_Fecha = NotaDebito_BD.strC58;
            NotaDebitoElectronica.Factura_Ref_Doc = NotaDebito_BD.strC59;

            #endregion
            #region Contruir Detalle Nota Debito
            GridView gv_detalle = new GridView();
            gv_detalle.DataSource = (DataTable)DB.getRubbyFact(tndID, 4);
            gv_detalle.DataBind();
            Rubros rubro;
            XML_Bean Bean_IVA = new XML_Bean();
            XML_Bean Bean_IVACERO = new XML_Bean();
            foreach (GridViewRow row in gv_detalle.Rows)
            {
                rubro = new Rubros();
                rubro.rubroID = long.Parse(row.Cells[0].Text);
                rubro.rubroName = row.Cells[1].Text;
                rubro.rubroTot = double.Parse(row.Cells[7].Text);
                rubro.rubroImpuesto = double.Parse(row.Cells[6].Text);
                rubro.rubroSubTot = double.Parse(row.Cells[5].Text);
                if (NotaDebitoElectronica.arr1 == null) NotaDebitoElectronica.arr1 = new ArrayList();
                if (rubro.rubroTot > 0)
                    NotaDebitoElectronica.arr1.Add(rubro);
            }
            #endregion
            #region Validacion ALL-IN
            if (NotaDebitoElectronica.strC38.Trim() != "")
            {
                rubro = new Rubros();
                rubro.rubroID = long.Parse("0");
                rubro.rubroName = NotaDebitoElectronica.strC38;
                rubro.rubtoType = "Servicio";
                rubro.rubroSubTot = Convert.ToDouble(NotaDebitoElectronica.decC4);
                rubro.rubroImpuesto = Convert.ToDouble(NotaDebitoElectronica.decC3);
                rubro.rubroTot = Convert.ToDouble(NotaDebitoElectronica.decC1);
                rubro.rubroTotD = 0;
                rubro.rubroCommentario = "";
                rubro.rubroTypeID = 0;
                if (NotaDebitoElectronica.arr3 == null) NotaDebitoElectronica.arr3 = new ArrayList();
                NotaDebitoElectronica.arr3.Add(rubro);
            }
            #endregion
            NotaDebitoElectronica.Factura_Electronica = int.Parse(NotaDebito_BD.strC50);
            NotaDebitoElectronica.intC10 = 1; //Factura
            NotaDebitoElectronica.intC11 = NotaDebito_BD.intC7;//Tipo de Persona
            NotaDebitoElectronica.Fecha_Emision = DateTime.Parse((NotaDebito_BD.strC3)).AddMinutes(-2).ToString("yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            string fecha_pago = DateTime.Parse((NotaDebito_BD.strC45)).AddYears(1).ToString("yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan ts = DateTime.Parse(fecha_pago) - DateTime.Parse(NotaDebitoElectronica.Fecha_Emision);
            int dias_credito = ts.Days;
            #endregion

            #region Observaciones Electronicas
            string Observaciones = "";
            if (NotaDebitoElectronica.strC9.Trim() != "")
            {
                Observaciones += "HBL:" + NotaDebitoElectronica.strC9 + " ";
            }
            if (NotaDebitoElectronica.strC12.Trim() != "")
            {
                Observaciones += "ROUTING:" + NotaDebitoElectronica.strC12 + " ";
            }
            if (NotaDebitoElectronica.strC11.Trim() != "")
            {
                Observaciones += "CONTENEDOR:" + NotaDebitoElectronica.strC11 + " ";
            }
            if (NotaDebitoElectronica.strC15.Trim() != "")
            {
                Observaciones += "SHIPPER:" + NotaDebitoElectronica.strC15 + " ";
            }
            if ((NotaDebitoElectronica.strC18.Trim() != "") || (NotaDebitoElectronica.strC19.Trim() != ""))
            {
                Observaciones += "PESO:" + NotaDebitoElectronica.strC18 + " VOLUMEN.: " + NotaDebitoElectronica.strC19 + " ";
            }
            if ((NotaDebitoElectronica.strC21.Trim() != "") || (NotaDebitoElectronica.strC22.Trim() != ""))
            {
                Observaciones += "DUA INGRESO:" + NotaDebitoElectronica.strC21 + " DUA SALIDA.: " + NotaDebitoElectronica.strC22 + " ";
            }
            if ((NotaDebitoElectronica.intC7.ToString().Trim() != "") && (NotaDebitoElectronica.intC7.ToString().Trim() != "0"))
            {
                Observaciones += "CODIGO DE CLIENTE:" + NotaDebitoElectronica.intC7.ToString().Trim() + " ";
            }
            if (NotaDebitoElectronica.strC1.Trim() != "")
            {
                Observaciones += "OBSERVACIONES:" + NotaDebitoElectronica.strC1 + " ";
            }
            Observaciones += "Estimado Cliente, a partir de la fecha de emision de la nota de debito tiene 15 dias calendario para realizar cualquier reclamo sobre la misma ";
            #endregion

            #region Determinar Tipo de Identificacion Tributaria
            string _TipoIdentificacion = "";
            if (NotaDebitoElectronica.intC11 == 3)
            {
                string criterio = "";
                ArrayList clientearr = null;
                RE_GenericBean clienteBean = null;
                criterio = " a.id_cliente=" + NotaDebitoElectronica.intC7.ToString();
                clientearr = (ArrayList)DB.getClientes(criterio, user, "");
                if ((clientearr != null) && (clientearr.Count > 0))
                {
                    clienteBean = (RE_GenericBean)clientearr[0];
                    _TipoIdentificacion = clienteBean.strC10;
                }
            }
            else if (NotaDebitoElectronica.intC11 == 10)
            {
                _TipoIdentificacion = "10";
            }
            #endregion
            #region Filtrar Identificacion Tributaria
            if (NotaDebitoElectronica.intC11 == 3)
            {
                NotaDebitoElectronica.strC35 = NotaDebitoElectronica.strC35.Trim();
                NotaDebitoElectronica.strC35 = NotaDebitoElectronica.strC35.Replace("-", "");
                if (NotaDebitoElectronica.strC35.Length > 10)
                {
                    NotaDebitoElectronica.strC35 = NotaDebitoElectronica.strC35.Substring(0, 10);
                }
            }
            else
            {
                NotaDebitoElectronica.strC35 = NotaDebitoElectronica.strC35.Trim();
            }
            #endregion
            #region Copia Electronica de Cortesia
            string _CopiaCortesia = "";
            if ((NotaDebitoElectronica.strC36.Trim().Length > 0) && (NotaDebitoElectronica.strC36.Trim() != "-"))
            {
                NotaDebitoElectronica.strC36 = NotaDebitoElectronica.strC36.Trim();
                NotaDebitoElectronica.strC36 = NotaDebitoElectronica.strC36.Replace(",", ";");
                //_CopiaCortesia = NotaDebitoElectronica.strC36 + " ; " + user.ID + "@aimargroup.com";
                _CopiaCortesia = NotaDebitoElectronica.strC36;
            }
            else
            {
                _CopiaCortesia = user.ID + "@aimargroup.com";
            }
            #endregion
            #region Construir XML

            var condicionVenta = Traducir_Condiciones_Venta_Hacienda(user, Convert.ToInt32(NotaDebitoElectronica.intC7), NotaDebitoElectronica.intC6);

            Nodo_Encabezado = new XElement("Encabezado",
                new XElement("NumeroFactura", tndID),
                new XElement("FechaFactura", NotaDebitoElectronica.Fecha_Emision),
                new XElement("Emisor",
                    new XElement("NumCuenta", _NumCuenta)
                    ),
				new XElement("CodigoActividad", "630101"),
                new XElement("TipoCambio", tipo_cambio),
                new XElement("TipoDoc", Traducir_Tipo_Documento_Hacienda(ttrID, _TipoIdentificacion)),
                //new XElement("NumConsecutivoCompr", ""),
                new XElement("CondicionVenta", condicionVenta),
                //new XElement("NumOrdenCompra", ""),
                new XElement("Moneda", Traducir_Moneda_Hacienda(NotaDebitoElectronica.intC1)),
                new XElement("idMedioPago", "99"),
                condicionVenta == "02" ? new XElement("DiasCredito", dias_credito) : null,
                new XElement("Sucursal", "1"),
                new XElement("Terminal", "1"),
                new XElement("FechaVencimiento", fecha_pago),
                new XElement("SituacionEnvio", "1"),
                new XElement("Receptor",
                    new XElement("TipoIdentificacion", _TipoIdentificacion),
                    new XElement("IdentificacionReceptor", NotaDebitoElectronica.strC35),
                    new XElement("NombreReceptor", NotaDebitoElectronica.Nombre_Cliente),
                    new XElement("idProvincia", "1"),
                    new XElement("idCanton", "1"),
                    new XElement("idDistrito", "1"),
                    new XElement("idBarrio", "1"),
                    new XElement("DireccionReceptor", NotaDebitoElectronica.Direccion),
                    //new XElement("NumeroAreaTelReceptor", "506"),
                    //new XElement("NumeroTelReceptor", "12345678"),
                    new XElement("CorreoElectronicoReceptor", _CopiaCortesia),
                    new XElement("CopiaCortesia", "cr-felectronica@aimargroup.com")
                    //new XElement("CorreoElectronicoReceptor", "soporte7@aimargroup.com"),
                    //new XElement("CopiaCortesia", "soporte7@aimargroup.com")
                    )
                );

            Nodo_Detalle = new XElement("Detalle");
            foreach (Rubros Rubro in NotaDebitoElectronica.arr1)
            {
                Nodo_Linea = new XElement("Linea",
                    //new XElement("Tipo", "S"), //2019-07-01
                    new XElement("CodigoProducto", Rubro.rubroID),
                    new XElement("Cantidad", "1"),
                    new XElement("UnidadMedida", "24"), //2019-07-01
                    new XElement("DetalleMerc", Rubro.rubroName),
                    new XElement("PrecioUnitario", Rubro.rubroSubTot.ToString("##.00000"))
                   // new XElement("MontoDescuento", (0).ToString("##.00000")),
                    //new XElement("NaturalezaDescuento", "")
                );

                if (Rubro.rubroImpuesto > 0)
                {
                    string _CodigoImpuesto_Temporal = "01";
                    double valor_impuesto = 0;
                    valor_impuesto = Rubro.rubroSubTot * 0.13;
                    valor_impuesto = double.Parse(valor_impuesto.ToString("F"));
                    Rubro.rubroImpuesto = valor_impuesto;
                    Nodo_Impuesto = new XElement("Impuestos",
                         new XElement("Impuesto",
                             new XElement("CodigoImpuesto", _CodigoImpuesto_Temporal),
							 new XElement("CodigoTarifa", "08"), //2019-07-01
                             new XElement("PorcentajeImpuesto", "13.00"),
                             new XElement("MontoImpuesto", Rubro.rubroImpuesto.ToString("##.00000"))
                         )
                     );
                    Nodo_Linea.Add(Nodo_Impuesto);
                    total_servicios_gravados += decimal.Parse(Rubro.rubroSubTot.ToString());
                    total_impuesto += decimal.Parse(Rubro.rubroImpuesto.ToString());
                }
                else
                {
                    total_servicios_exentos += decimal.Parse(Rubro.rubroSubTot.ToString());
                }

                Nodo_Detalle.Add(Nodo_Linea);
            }

            var _NumeroReferencia = string.Empty;
            if (NotaDebitoElectronica.Factura_Ref_Doc != "")
                _NumeroReferencia = NotaDebitoElectronica.Factura_Ref_Doc; //2019-07-11 trae segmento de esignature pos 21,22
            else {
                if (user.PaisID == 21) 
                {
                    _NumeroReferencia = NotaDebitoElectronica.Factura_Ref_Serie.Substring(6, 10) + NotaDebitoElectronica.Factura_Ref_Correlativo.ToString("0000000000.##");
                }
                else
                {
                    _NumeroReferencia = NotaDebitoElectronica.Factura_Ref_Serie.Substring(2, 10) + NotaDebitoElectronica.Factura_Ref_Correlativo.ToString("0000000000.##");
                }
                //_NumeroReferencia = NotaCreditoElectronica.strC2.Substring(2, 10);
                //_NumeroReferencia = _NumeroReferencia + int.Parse(NotaCreditoElectronica.strC3).ToString("0000000000.##");
            }

            Nodo_InformacionDeReferencia = new XElement("InformacionDeReferencia",
                new XElement("Referencia",
                    //new XElement("TpoDocRef", "01"), //01 factura 02 debito 03 credito                    
                    new XElement("TpoDocRef", FechaGTI43(NotaDebitoElectronica.Factura_Ref_Fecha, _TipoIdentificacion)),
                    new XElement("NumeroReferencia", _NumeroReferencia),
                    new XElement("CodigoReferencia", "04") //referencia a otro documento
                )
            );
            total_gravado = total_servicios_gravados;
            total_exento = total_servicios_exentos;
            total_venta = total_gravado + total_exento;
            total_venta_neta = total_venta;
            total_comprobante = total_venta_neta + total_impuesto;
            Nodo_Totales = new XElement("Totales",
				new XElement("TotalServGravados", total_servicios_gravados.ToString("##0.00000")),
				new XElement("TotalServExentos", total_servicios_exentos.ToString("##0.00000")),
				new XElement("TotalServExonerados", "0.00000"), //2019-07-01
				new XElement("TotalMercanciasGravadas", total_mercancias_gravadas.ToString("##0.00000")),
				new XElement("TotalMercanciasExentas", total_mercancias_exentas.ToString("##0.00000")),
				new XElement("TotalMercanciasExoneradas", "0.00000"), //2019-07-01                
				new XElement("TotalGravado", total_gravado.ToString("##0.00000")),
				new XElement("TotalExento", total_exento.ToString("##0.00000")),
				new XElement("TotalExonerado", "0.00000"), //2019-07-01                
				new XElement("TotalOtrosCargos", "0.00000"), //2019-07-01                
				new XElement("TotalVenta", total_venta.ToString("##0.00000")),
				new XElement("TotalDescuentos", total_descuentos.ToString("##0.00000")),
				new XElement("TotalIVADevuelto", "0.00000"), //2019-07-01                
				new XElement("TotalVentaNeta", total_venta_neta.ToString("##0.00000")),
				new XElement("TotalImpuesto", total_impuesto.ToString("##0.00000")),
				new XElement("TotalComprobante", total_comprobante.ToString("##0.00000"))
            );

            Nodo_Otros = new XElement("Otros", Observaciones);

            Nodo_FacturaElectronicaXML = new XElement("FacturaElectronicaXML");
            Nodo_FacturaElectronicaXML.Add(Nodo_Encabezado);
            Nodo_FacturaElectronicaXML.Add(Nodo_Detalle);
            Nodo_FacturaElectronicaXML.Add(Nodo_InformacionDeReferencia);
            Nodo_FacturaElectronicaXML.Add(Nodo_Totales);
            Nodo_FacturaElectronicaXML.Add(Nodo_Otros);

            Nodo_root = new XElement("root");
            Nodo_root.Add(Nodo_FacturaElectronicaXML);
            XML_Invoice.Add(Nodo_root);
            Invoice.InnerXml = XML_Invoice.ToString();

            string Path = "D:\\\\PRUEBA_ND_CR" + tndID + ".xml";
            XmlTextWriter Writer = new XmlTextWriter(Path, new UTF8Encoding(false));
            Invoice.Save(Writer);
            Writer.Flush();
            Writer.Close();
            XML_Invoice = null;
            #endregion

            Arr_XML = new ArrayList();
            Arr_XML.Add("1");
            Arr_XML.Add(Invoice);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            Arr_XML = new ArrayList();
            Arr_XML.Add("0");
            Arr_XML.Add("Existio un error al crear XML de la Nota de Debito.: " + e.Message + " ");
        }
        return Arr_XML;
    }
    public ArrayList Crear_Nota_Credito(int tncID)
    {
        ArrayList Arr_XML = new ArrayList();
        int ttrID = 3;
        decimal tipo_cambio = 1;
        UsuarioBean user = new UsuarioBean();
        Invoice = new XmlDocument();
        XML_Invoice = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
        try
        {
            #region Capturar Data Nota Credito
            #region Encabezado Nota Credito
            RE_GenericBean NotaCredito_BD = (RE_GenericBean)DB.getNotaCreditoData(tncID);
            RE_GenericBean NotaCreditoElectronica = new RE_GenericBean();
            #region Construir Usuario
            user.ID = NotaCredito_BD.strC8;
            user.Contrasena = "";
            user.PaisID = NotaCredito_BD.intC8;
            user.contaID = NotaCredito_BD.intC9;
            user.pais = (PaisBean)DB.getPais(user.PaisID);
            user.Moneda = DB.getMonedaByPaisConta(user);
            user.Idioma = DB.getIdiomaByPaisConta(user);
            user.Aplicaciones = (Hashtable)DB.getPerfilesbyUser(user.ID);
            user.Departamento = (ArrayList)DB.getUsuarioDepartamentoAimar(user.ID, user.PaisID);
            user.SucursalID = NotaCredito_BD.intC2;
            #endregion
            NotaCreditoElectronica.intC1 = NotaCredito_BD.intC4;//MonedaID
            NotaCreditoElectronica.intC3 = user.contaID;
            NotaCreditoElectronica.intC4 = 0;//Factura ID;
            NotaCreditoElectronica.intC5 = 3;//tipo de transaccion
            NotaCreditoElectronica.intC6 = int.Parse(NotaCredito_BD.strC56);//tipo de persona
            NotaCreditoElectronica.intC7 = NotaCredito_BD.intC3;//Codigo Cliente
            NotaCreditoElectronica.strC1 = NotaCredito_BD.strC7;//Observaciones
            NotaCreditoElectronica.strC2 = NotaCredito_BD.strC28;//Serie Factura
            NotaCreditoElectronica.strC3 = NotaCredito_BD.strC1;//Correlativo Factura
            //NotaCreditoElectronica.strC39 = NotaCredito_BD.strC30;//fecha factura 2019-07-09
            NotaCreditoElectronica.strC20 = NotaCredito_BD.strC32;
            NotaCreditoElectronica.strC35 = NotaCredito_BD.strC2;//nit
            NotaCreditoElectronica.strC36 = NotaCredito_BD.strC53;//tnc_correo_documento_electronico
            NotaCreditoElectronica.strC37 = NotaCredito_BD.strC54;//tnc_referencia_correo
            NotaCreditoElectronica.strC38 = NotaCredito_BD.strC30;//All in
            NotaCreditoElectronica.strC9 = NotaCredito_BD.strC9;//HBL
            NotaCreditoElectronica.strC10 = NotaCredito_BD.strC10;//MBL
            NotaCreditoElectronica.strC11 = NotaCredito_BD.strC11;//CONTENEDOR
            NotaCreditoElectronica.strC12 = NotaCredito_BD.strC12;//ROUTING
            NotaCreditoElectronica.strC13 = NotaCredito_BD.strC16;//POLIZA
            NotaCreditoElectronica.Fecha_Emision = DateTime.Parse((NotaCredito_BD.strC5)).AddMinutes(-2).ToString("yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            string fecha_pago = DateTime.Parse((NotaCredito_BD.strC6)).AddYears(1).ToString("yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            TimeSpan ts = DateTime.Parse(fecha_pago) - DateTime.Parse(NotaCreditoElectronica.Fecha_Emision);
            int dias_credito = ts.Days;
            

            NotaCreditoElectronica.decC1 = NotaCredito_BD.decC3;//tnc_total
            NotaCreditoElectronica.decC3 = NotaCredito_BD.decC5;//tnc_impuesto                
            NotaCreditoElectronica.decC4 = NotaCredito_BD.decC4;//tnc_subtotal
            NotaCreditoElectronica.decC5 = NotaCredito_BD.decC3;//tfa_subtotal
            NotaCreditoElectronica.decC6 = NotaCredito_BD.decC2;//tfa_impuesto
            NotaCreditoElectronica.decC7 = NotaCredito_BD.decC5 + NotaCredito_BD.decC6;//tfa_total

            int Doc_ID = DB.getDocumentoID(user.SucursalID, NotaCredito_BD.strC32, 3, user);
            NotaCreditoElectronica.intC8 = Doc_ID;//Serie ID
            NotaCreditoElectronica.Nombre_Cliente = NotaCredito_BD.strC3;//Nombre del Cliente
            NotaCreditoElectronica.Direccion = NotaCredito_BD.strC4;//Direccion
            NotaCreditoElectronica.Estado = NotaCredito_BD.intC10;//Estado de Factura
            NotaCreditoElectronica.intC11 = int.Parse(NotaCredito_BD.strC56);//tnc_tpi_id
            #endregion
            #region Contruir Detalle Nota Credito
            GridView gv_detalle = new GridView();
            gv_detalle.DataSource = (DataTable)DB.getRubbyFact_For_Print(tncID, 3, user);
            gv_detalle.DataBind();
            Rubros rubro;
            XML_Bean Bean_IVA = new XML_Bean();
            XML_Bean Bean_IVACERO = new XML_Bean();
            foreach (GridViewRow row in gv_detalle.Rows)
            {
                rubro = new Rubros();
                rubro.rubroID = long.Parse(row.Cells[0].Text);
                rubro.rubroName = row.Cells[1].Text;
                rubro.rubroTot = double.Parse(row.Cells[7].Text);
                rubro.rubroImpuesto = double.Parse(row.Cells[6].Text);
                rubro.rubroSubTot = double.Parse(row.Cells[5].Text);

                if (NotaCreditoElectronica.arr1 == null) NotaCreditoElectronica.arr1 = new ArrayList();
                if (rubro.rubroTot > 0)
                    NotaCreditoElectronica.arr1.Add(rubro);
                if (NotaCreditoElectronica.arr2 == null) NotaCreditoElectronica.arr2 = new ArrayList();
            }
            #endregion
            #region Validacion ALL-IN
            if (NotaCreditoElectronica.strC38.Trim() != "")
            {
                rubro = new Rubros();
                rubro.rubroID = long.Parse("0");
                rubro.rubroName = NotaCreditoElectronica.strC38;
                rubro.rubtoType = "Servicio";
                rubro.rubroSubTot = Convert.ToDouble(NotaCreditoElectronica.decC4);
                rubro.rubroImpuesto = Convert.ToDouble(NotaCreditoElectronica.decC3);
                rubro.rubroTot = Convert.ToDouble(NotaCreditoElectronica.decC1);
                rubro.rubroTotD = 0;
                rubro.rubroCommentario = "";
                rubro.rubroTypeID = 0;
                if (NotaCreditoElectronica.arr3 == null) NotaCreditoElectronica.arr3 = new ArrayList();
                NotaCreditoElectronica.arr3.Add(rubro);
            }
            #endregion
            NotaCreditoElectronica.Factura_Electronica = int.Parse(NotaCredito_BD.strC50);
            NotaCreditoElectronica.intC10 = 1; //Factura
            #endregion

            #region Definir Data Nodo de Referencia
            string _TpoDocRef = "";
            string _NumeroReferencia = "";
            string _CodigoReferencia = "";
            string Observacion_Tipo_Documento = "";
            ArrayList Arr_Tipo_NC = DB.Determinar_Tipo_Nota_Credito(tncID);
            if (Arr_Tipo_NC[0].ToString() == "0")
            {
                return null;
            }
            else if (Arr_Tipo_NC[0].ToString() == "1")
            {
                #region Tipo de Nota de Credito
                switch (Arr_Tipo_NC[1].ToString())
                {
                    case "2": _CodigoReferencia = "04"; break;    //total es menor al saldo
                    case "3": _CodigoReferencia = "01"; break;    //anula
                    //case "4": _CodigoReferencia = "99"; break;    //total igual al saldo
                    default: _CodigoReferencia = "99"; break;       //otros
                }
                /*if (Arr_Tipo_NC[1].ToString() == "3")
                {
                    //Nota de Credito que Anula
                    _CodigoReferencia = "01";    //1 
                }
                else if (Arr_Tipo_NC[1].ToString() == "2")
                {
                    //Nota de Credito que Descuenta
                    _CodigoReferencia = "04";   //3
                }*/
                #endregion
                #region Tipo de Documento al que se aplico la Nota de Credito
                //_TpoDocRef = Traducir_Tipo_Documento_Hacienda(int.Parse(Arr_Tipo_NC[2].ToString())).ToString();  2019-07-08
                if (Arr_Tipo_NC[2].ToString() == "1")
                {
                    Observacion_Tipo_Documento = "Nota de Credito aplicada a Factura No.:" + NotaCreditoElectronica.strC3 + " ";
                }
                else if (Arr_Tipo_NC[2].ToString() == "4")
                {
                    Observacion_Tipo_Documento = "Nota de Credito aplicada a Nota de Debito No.:" + NotaCreditoElectronica.strC3 + " ";
                }
                #endregion
                #region Numero de Referencia
                if (NotaCreditoElectronica.Factura_Ref_Doc != "")
                    _NumeroReferencia = NotaCreditoElectronica.Factura_Ref_Doc; //2019-07-11 trae segmento de esignature pos 20,22
                else {
                    if (user.PaisID == 21)
                        _NumeroReferencia = NotaCreditoElectronica.strC2.Substring(6, 10);
                    else
                        _NumeroReferencia = NotaCreditoElectronica.strC2.Substring(2, 10);

                    _NumeroReferencia = _NumeroReferencia + int.Parse(NotaCreditoElectronica.strC3).ToString("0000000000.##");
                }                
                //_NumeroReferencia = _NumeroReferencia;                
                #endregion
            }
            #endregion

            #region Observaciones Electronicas
            string Observaciones = "";
            if (NotaCreditoElectronica.strC9.Trim() != "")
            {
                Observaciones += "HBL.: " + NotaCreditoElectronica.strC9 + " ,";
            }
            if (NotaCreditoElectronica.strC12.Trim() != "")
            {
                Observaciones += "ROUTING.: " + NotaCreditoElectronica.strC12 + " ,";
            }
            if (NotaCreditoElectronica.strC11.Trim() != "")
            {
                Observaciones += "CONTENEDOR.: " + NotaCreditoElectronica.strC11 + " ,";
            }
            if ((NotaCreditoElectronica.intC7.ToString().Trim() != "") && (NotaCreditoElectronica.intC7.ToString().Trim() != "0"))
            {
                Observaciones += "CODIGO DE CLIENTE.: " + NotaCreditoElectronica.intC7.ToString().Trim() + " ,";
            }
            if (NotaCreditoElectronica.strC1.Trim() != "")
            {
                Observaciones += "OBSERVACIONES.: " + NotaCreditoElectronica.strC1 + " ,";
            }

            Observaciones += "OBSERVACIONES.: " + NotaCreditoElectronica.strC1 + " ,";

            #endregion

            #region Determinar Tipo de Identificacion Tributaria
            string _TipoIdentificacion = "";
            string criterio = "";
            ArrayList clientearr = null;
            RE_GenericBean clienteBean = null;
            criterio = " a.id_cliente=" + NotaCreditoElectronica.intC7.ToString();
            clientearr = (ArrayList)DB.getClientes(criterio, user, "");
            if ((clientearr != null) && (clientearr.Count > 0))
            {
                clienteBean = (RE_GenericBean)clientearr[0];
                _TipoIdentificacion = clienteBean.strC10;
            }
            #endregion

            _TpoDocRef = Traducir_Tipo_Documento_Hacienda(int.Parse(Arr_Tipo_NC[2].ToString()), _TipoIdentificacion).ToString();  //2019-07-08

            if (_TpoDocRef == "02")
            {
                _TpoDocRef = "01"; //2019-07-17 NC no puede referenciar a la ND trae los datos de factura
                if (_CodigoReferencia == "01") _CodigoReferencia = "99";
            }

            #region Filtrar Identificacion Tributaria
            if (NotaCreditoElectronica.intC11 == 3)
            {
                NotaCreditoElectronica.strC35 = NotaCreditoElectronica.strC35.Trim();
                NotaCreditoElectronica.strC35 = NotaCreditoElectronica.strC35.Replace("-", "");
                if (NotaCreditoElectronica.strC35.Length > 10)
                {
                    NotaCreditoElectronica.strC35 = NotaCreditoElectronica.strC35.Substring(0, 10);
                }
            }
            else
            {
                NotaCreditoElectronica.strC35 = NotaCreditoElectronica.strC35.Trim();
            }
            #endregion
            #region Copia Electronica de Cortesia
            string _CopiaCortesia = "";
            if ((NotaCreditoElectronica.strC36.Trim().Length > 0) && (NotaCreditoElectronica.strC36.Trim() != "-"))
            {
                NotaCreditoElectronica.strC36 = NotaCreditoElectronica.strC36.Trim();
                NotaCreditoElectronica.strC36 = NotaCreditoElectronica.strC36.Replace(",", ";");
                //_CopiaCortesia = NotaCreditoElectronica.strC36 + " ; " + user.ID + "@aimargroup.com";
                _CopiaCortesia = NotaCreditoElectronica.strC36;
            }
            else
            {
                _CopiaCortesia = user.ID + "@aimargroup.com";
            }
            #endregion

            #region Construir XML

            var condicionVenta = Traducir_Condiciones_Venta_Hacienda(user, Convert.ToInt32(NotaCreditoElectronica.intC7), NotaCreditoElectronica.intC6);

            Nodo_Encabezado = new XElement("Encabezado",
                new XElement("NumeroFactura", tncID),
                new XElement("FechaFactura", NotaCreditoElectronica.Fecha_Emision),
                new XElement("Emisor",
                    new XElement("NumCuenta", _NumCuenta)
                    ),
				new XElement("CodigoActividad", "630101"),
                new XElement("TipoCambio", tipo_cambio),
                new XElement("TipoDoc", Traducir_Tipo_Documento_Hacienda(ttrID, _TipoIdentificacion)),
                //new XElement("NumConsecutivoCompr", ""),
                new XElement("CondicionVenta", condicionVenta),
                //new XElement("NumOrdenCompra", ""),
                new XElement("Moneda", Traducir_Moneda_Hacienda(NotaCreditoElectronica.intC1)),
                new XElement("idMedioPago", "99"),
                condicionVenta == "02" ? new XElement("DiasCredito", dias_credito) : null,
                new XElement("Sucursal", "1"),
                new XElement("Terminal", "1"),
                //new XElement("FechaVencimiento", fecha_pago),
                new XElement("SituacionEnvio", "1"),
                new XElement("Receptor",
                    new XElement("TipoIdentificacion", _TipoIdentificacion),
                    new XElement("IdentificacionReceptor", NotaCreditoElectronica.strC35),
                    new XElement("NombreReceptor", NotaCreditoElectronica.Nombre_Cliente),
                    new XElement("idProvincia", "1"),
                    new XElement("idCanton", "1"),
                    new XElement("idDistrito", "1"),
                    new XElement("idBarrio", "1"),
                    new XElement("DireccionReceptor", NotaCreditoElectronica.Direccion),
                    //new XElement("NumeroAreaTelReceptor", "506"),
                    //new XElement("NumeroTelReceptor", "12345678"),
                    new XElement("CorreoElectronicoReceptor", _CopiaCortesia),
                    new XElement("CopiaCortesia", "cr-felectronica@aimargroup.com")
                    //new XElement("CorreoElectronicoReceptor", "soporte7@aimargroup.com"),
                    //new XElement("CopiaCortesia", "soporte7@aimargroup.com")
                    )
                );

            Nodo_Detalle = new XElement("Detalle");
            foreach (Rubros Rubro in NotaCreditoElectronica.arr1)
            {
                #region Determinar Codigo de Impuesto
                string _CodigoImpuesto_Temporal = "01";
                if (Rubro.rubroImpuesto == 0)
                {
                    _CodigoImpuesto_Temporal = "0";
                }
                #endregion
                Nodo_Linea = new XElement("Linea",
                    //new XElement("Tipo", "S"), //2019-07-01
                    new XElement("CodigoProducto", Rubro.rubroID),
                    new XElement("Cantidad", "1"),
                    new XElement("UnidadMedida", "24"), //2019-07-01
                    new XElement("DetalleMerc", Rubro.rubroName),
                    new XElement("PrecioUnitario", Rubro.rubroSubTot.ToString("##.00000"))
                    //new XElement("MontoDescuento", (0).ToString("##.00000")),
                    //new XElement("NaturalezaDescuento", "")
                );
                if (Rubro.rubroImpuesto > 0)
                {
                    double valor_impuesto = 0;
                    valor_impuesto = Rubro.rubroSubTot * 0.13;
                    valor_impuesto = double.Parse(valor_impuesto.ToString("F"));
                    Rubro.rubroImpuesto = valor_impuesto;
                    Nodo_Impuesto = new XElement("Impuestos",
                         new XElement("Impuesto",
                             new XElement("CodigoImpuesto", _CodigoImpuesto_Temporal),
							 new XElement("CodigoTarifa", "08"), //2019-07-01
                             new XElement("PorcentajeImpuesto", "13.00"),
                             new XElement("MontoImpuesto", Rubro.rubroImpuesto.ToString("##.00000"))
                         )
                     );
                    Nodo_Linea.Add(Nodo_Impuesto);
                    total_servicios_gravados += decimal.Parse(Rubro.rubroSubTot.ToString());
                    total_impuesto += decimal.Parse(Rubro.rubroImpuesto.ToString());
                }
                else
                {
                    total_servicios_exentos += decimal.Parse(Rubro.rubroSubTot.ToString());
                }

                Nodo_Detalle.Add(Nodo_Linea);
            }            

            Nodo_InformacionDeReferencia = new XElement("InformacionDeReferencia",
                new XElement("Referencia",
                    //new XElement("TpoDocRef", _TpoDocRef),
                    new XElement("TpoDocRef", FechaGTI43_NC(NotaCreditoElectronica.Factura_Ref_Fecha, _TipoIdentificacion, _TpoDocRef)),                     
                    new XElement("NumeroReferencia", _NumeroReferencia),
                    new XElement("CodigoReferencia", _CodigoReferencia)
                )
            );
            total_gravado = total_servicios_gravados;
            total_exento = total_servicios_exentos;
            total_venta = total_gravado + total_exento;
            total_venta_neta = total_venta;
            total_comprobante = total_venta_neta + total_impuesto;
            Nodo_Totales = new XElement("Totales",
				new XElement("TotalServGravados", total_servicios_gravados.ToString("##0.00000")),
				new XElement("TotalServExentos", total_servicios_exentos.ToString("##0.00000")),
				new XElement("TotalServExonerados", "0.00000"), //2019-07-01
				new XElement("TotalMercanciasGravadas", total_mercancias_gravadas.ToString("##0.00000")),
				new XElement("TotalMercanciasExentas", total_mercancias_exentas.ToString("##0.00000")),
				new XElement("TotalMercanciasExoneradas", "0.00000"), //2019-07-01                
				new XElement("TotalGravado", total_gravado.ToString("##0.00000")),
				new XElement("TotalExento", total_exento.ToString("##0.00000")),
				new XElement("TotalExonerado", "0.00000"), //2019-07-01                
				new XElement("TotalOtrosCargos", "0.00000"), //2019-07-01                
				new XElement("TotalVenta", total_venta.ToString("##0.00000")),
				new XElement("TotalDescuentos", total_descuentos.ToString("##0.00000")),
				new XElement("TotalIVADevuelto", "0.00000"), //2019-07-01                
				new XElement("TotalVentaNeta", total_venta_neta.ToString("##0.00000")),
				new XElement("TotalImpuesto", total_impuesto.ToString("##0.00000")),
				new XElement("TotalComprobante", total_comprobante.ToString("##0.00000"))
            );


            Nodo_Otros = new XElement("Otros", Observaciones);

            Nodo_FacturaElectronicaXML = new XElement("FacturaElectronicaXML");
            Nodo_FacturaElectronicaXML.Add(Nodo_Encabezado);
            Nodo_FacturaElectronicaXML.Add(Nodo_Detalle);
            Nodo_FacturaElectronicaXML.Add(Nodo_InformacionDeReferencia);
            Nodo_FacturaElectronicaXML.Add(Nodo_Totales);
            Nodo_FacturaElectronicaXML.Add(Nodo_Otros);

            Nodo_root = new XElement("root");
            Nodo_root.Add(Nodo_FacturaElectronicaXML);
            XML_Invoice.Add(Nodo_root);
            Invoice.InnerXml = XML_Invoice.ToString();

            #endregion

            Arr_XML = new ArrayList();
            Arr_XML.Add("1");
            Arr_XML.Add(Invoice);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            Arr_XML = new ArrayList();
            Arr_XML.Add("0");
            Arr_XML.Add("Existio un error al crear XML de la Nota de Debito.: " + e.Message + " ");
        }
        return Arr_XML;
    }
    public string Send_Document()
    {
        string Document = "";
        return Document;
    }
    private static string Traducir_Moneda_Hacienda(int monedaID)
    {
        #region Traducir a Codigo de Moneda de la Hacienda
        string Moneda_Hacienda = "";
        if (monedaID == 5)
        {
            Moneda_Hacienda = "01";
        }
        else if (monedaID == 8)
        {
            Moneda_Hacienda = "02";
        }
        return Moneda_Hacienda;
        #endregion
    }
    private static string Traducir_Condiciones_Venta_Hacienda(UsuarioBean user, int cliID, int tpiID)
    {
        #region Traducir a Codigo de Condiciones de Venta de la Hacienda
        string Tipo_Cliente_Hacienda = "";
        if (tpiID == 3)
        {
            RE_GenericBean Credito_Bean = (RE_GenericBean)DB.getCredito_Cliente(user.PaisID, cliID);
            if (Credito_Bean == null)
            {
                Tipo_Cliente_Hacienda = "01";
            }
            else
            {
                Tipo_Cliente_Hacienda = "02";
            }
        }
        else if (tpiID == 10)
        {
            Tipo_Cliente_Hacienda = "01";
        }
        return Tipo_Cliente_Hacienda;
        #endregion
    }
    private string Traducir_Tipo_Documento_Hacienda(int ttrID, string _TipoIdentificacion)
    {
        #region Traducir a Tipo de Documento de Hacienda
        string Tipo_Documento_Hacienda = "0";
        if (ttrID == 1)
        {
            Tipo_Documento_Hacienda = "01";

            if (_TipoIdentificacion == "10") Tipo_Documento_Hacienda = "04";
        }
        else if (ttrID == 3)
        {
            Tipo_Documento_Hacienda = "03";
        }
        else if (ttrID == 4)
        {
            Tipo_Documento_Hacienda = "02";
        }
        return Tipo_Documento_Hacienda;
        #endregion
    }
    public string Transmitir_Documento(string XML)
    {
        string resultado = "";
        WS_GFACE_CR.GTICargaFactura WS = new WS_GFACE_CR.GTICargaFactura();
        resultado = WS.InsertarDocumentos(XML, _emailUSER, _passUSER);
        return resultado;
    }
    public string Extraer_Nodo_XML(XmlDocument XML, string Nodo)
    {
        string Valor = "";
        XmlNodeList Nodo_Correlativo = XML.GetElementsByTagName(Nodo);
        Valor = Nodo_Correlativo[0].InnerText;
        return Valor;
    }
    public string Obtener_Documento_Firmado(string Correlativo, int ttrID)
    {
        string resultado = "";
        WS_GFACE_CR.GTICargaFactura WS = new WS_GFACE_CR.GTICargaFactura();
        resultado = WS.ConsultaDocumento(int.Parse(_NumCuenta), Correlativo, _emailUSER, _passUSER);
        return resultado;
    }
    public string Extraer_Firma(string XML_Firmado)
    {
        string Firma_Electronica = "";
        int Posicion_Inicio_Firma = 0;
        int Posicion_Fin_Firma = 0;
        Posicion_Inicio_Firma = XML_Firmado.IndexOf("<firma>");
        Posicion_Fin_Firma = XML_Firmado.IndexOf("</firma>");
        Firma_Electronica = XML_Firmado.Substring(Posicion_Inicio_Firma + 7, Posicion_Fin_Firma - Posicion_Inicio_Firma);
        return Firma_Electronica;
    }
    public ArrayList Generar_Firma_Electronica(UsuarioBean user, int ttrID, int docID, ArrayList Arr_Documento)
    {
        ArrayList Arr_Resultado = new ArrayList();
        try
        {
            string XML_Respuesta = "";
            XmlDocument EInvoice_Document = null;
            EInvoice_CR EInvoice = new EInvoice_CR(user.PaisID);
            ArrayList Arr_XML = new ArrayList();
            #region Crear XML
            if (ttrID == 1)
            {
                #region Crear XML de Factura
                if (docID > 0)
                {
                    Arr_XML = (ArrayList)EInvoice.Crear_Factura(docID);
                }
                else
                {
                    Arr_XML = (ArrayList)EInvoice.Crear_Factura_desde_Memoria(user, Arr_Documento);
                }
                #endregion
            }
            else if (ttrID == 3)
            {
                #region Crear XML de Nota de Credito
                if (docID > 0)
                {
                    Arr_XML = (ArrayList)EInvoice.Crear_Nota_Credito(docID);
                }
                else
                {
                    Arr_XML = (ArrayList)EInvoice.Crear_Nota_Credito_desde_Memoria(user, Arr_Documento);
                }
                #endregion
            }
            else if (ttrID == 4)
            {
                #region Crear XML de Nota de Debito
                if (docID > 0)
                {
                    Arr_XML = (ArrayList)EInvoice.Crear_Nota_Debito(docID);
                }
                else
                {
                    Arr_XML = (ArrayList)EInvoice.Crear_Nota_Debito_desde_Memoria(user, Arr_Documento);
                }
                #endregion
            }
            if (Arr_XML[0].ToString() == "0")
            {
                Arr_Resultado = new ArrayList();
                Arr_Resultado.Add("0");
                Arr_Resultado.Add(Arr_XML[1].ToString());
                return Arr_Resultado;
            }
            else if (Arr_XML[0].ToString() == "1")
            {
                EInvoice_Document = (XmlDocument)Arr_XML[1];
            }
            #endregion
            XML_Respuesta = EInvoice.Transmitir_Documento(EInvoice_Document.InnerXml);
            EInvoice_Document = new XmlDocument();
            EInvoice_Document.InnerXml = XML_Respuesta;
            string Codigo_Error = EInvoice.Extraer_Nodo_XML(EInvoice_Document, "CodigoError");
            if (Codigo_Error == "CodError:00" || Codigo_Error == "0") //se agrego Codigo_Error == "0" 2019-07-02
            {
                #region Transmision Exitosa
                string Correlativo_Electronico = "";
                string Correlativo = "";
                Correlativo = EInvoice.Extraer_Nodo_XML(EInvoice_Document, "NumConsecutivoCompr");
                Correlativo_Electronico = Correlativo;
                #region Filtrar Correlativo
                if (Correlativo.Length == 20)
                {
                    Correlativo = Correlativo.Substring(10, 10);
                    Correlativo = Convert.ToInt32(Correlativo).ToString();
                   }
                #endregion
                string Referencia_Interna = EInvoice.Extraer_Nodo_XML(EInvoice_Document, "ClaveNumerica");
                string XML_Firmado = EInvoice.Obtener_Documento_Firmado(Correlativo_Electronico, 1);
                string Firma_Electronica = EInvoice.Extraer_Firma(XML_Firmado);
                Firma_Electronica = Referencia_Interna;

                Arr_Resultado = new ArrayList();
                Arr_Resultado.Add("1");
                if (ttrID == 1)
                {
                    Arr_Resultado.Add("La Factura fue guardada exitosamente con el Correlativo: " + Correlativo + " y Código Único de Consulta: " + Firma_Electronica + "");
                }
                else if (ttrID == 4)
                {
                    Arr_Resultado.Add("La Nota de Debito fue guardada exitosamente con el Correlativo: " + Correlativo + " y Código Único de Consulta: " + Firma_Electronica + "");
                }
                else if (ttrID == 3)
                {
                    Arr_Resultado.Add("La Nota de Credito fue guardada exitosamente con el Correlativo: " + Correlativo + " y Código Único de Consulta: " + Firma_Electronica + "");
                }
                Arr_Resultado.Add(Correlativo);
                Arr_Resultado.Add(Referencia_Interna);
                Arr_Resultado.Add(Firma_Electronica);
                #endregion
            }
            else if (Codigo_Error != "CodError:00")
            {
                #region Errores de Hacienda
                string mensaje = "";
                //mensaje = Traducir_Tipo_Error(Codigo_Error);
                mensaje = EInvoice.Extraer_Nodo_XML(EInvoice_Document, "DescripcionError");
                Arr_Resultado = new ArrayList();
                Arr_Resultado.Add("0");
                if (ttrID == 1)
                {
                    Arr_Resultado.Add("La Factura no ha sido guardada ni procesada por el Ministerio de Hacienda por el siguiente motivo:\\n\\n" + mensaje + "\\n\\nComuniquese con el personal Administrativo de "+user.pais.Nombre_Sistema+" para que actualizen y corrijan.\\n\\nPosterior a ello genere nuevamente la Factura.");
                }
                else if (ttrID == 4)
                {
                    Arr_Resultado.Add("La Nota de Debito no ha sido guardada ni procesada por el Ministerio de Hacienda por el siguiente motivo:\\n\\n" + mensaje + "\\n\\nComuniquese con el personal Administrativo de " + user.pais.Nombre_Sistema + " para que actualizen y corrijan.\\n\\nPosterior a ello genere nuevamente la Nota de Debito.");
                }
                else if (ttrID == 3)
                {
                    Arr_Resultado.Add("La Nota de Credito no ha sido guardada ni procesada por el Ministerio de Hacienda por el siguiente motivo:\\n\\n" + mensaje + "\\n\\nComuniquese con el personal Administrativo de " + user.pais.Nombre_Sistema + " para que actualizen y corrijan.\\n\\nPosterior a ello genere nuevamente la Nota de Credito.");
                }
                #endregion
            }
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            Arr_Resultado = new ArrayList();
            Arr_Resultado.Add("0");
            Arr_Resultado.Add(e.Message);
            return Arr_Resultado;
        }
        return Arr_Resultado;
    }
    public int Actualizar_Datos_Documento_Electronico(int ttrID, int refID, string correlativo, string referencia_interna, string firma_electronica)
    {
        int resultado = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            if (ttrID == 1)
            {
                comm.CommandText = "update tbl_facturacion set tfa_correlativo='" + correlativo + "', tfa_internal_reference='" + referencia_interna + "', tfa_esignature='" + firma_electronica + "' where tfa_id=" + refID + " ";
                resultado = comm.ExecuteNonQuery();
                comm.Parameters.Clear();
                comm.CommandText = "update tbl_libro_diario set tdi_correlativo='" + correlativo + "' where tdi_ttr_id=" + ttrID + " and tdi_ref_id=" + refID + " ";
                resultado = comm.ExecuteNonQuery();
                comm.Parameters.Clear();
            }
            else if (ttrID == 3)
            {
                comm.CommandText = "update tbl_nota_credito set tnc_correlativo=" + correlativo + ", tnc_internal_reference='" + referencia_interna + "', tnc_esignature='" + firma_electronica + "' where tnc_id=" + refID + " ";
                resultado = comm.ExecuteNonQuery();
                comm.Parameters.Clear();
                comm.CommandText = "update tbl_libro_diario set tdi_correlativo='" + correlativo + "' where tdi_ttr_id=" + ttrID + " and tdi_ref_id=" + refID + " ";
                resultado = comm.ExecuteNonQuery();
                comm.Parameters.Clear();
            }
            else if (ttrID == 4)
            {
                comm.CommandText = "update tbl_nota_debito set tnd_correlativo=" + correlativo + ", tnd_internal_reference='" + referencia_interna + "', tnd_esignature='" + firma_electronica + "' where tnd_id=" + refID + " ";
                resultado = comm.ExecuteNonQuery();
                comm.Parameters.Clear();
                comm.CommandText = "update tbl_libro_diario set tdi_correlativo='" + correlativo + "' where tdi_ttr_id=" + ttrID + " and tdi_ref_id=" + refID + " ";
                resultado = comm.ExecuteNonQuery();
                comm.Parameters.Clear();
            }
            DB.CloseObj_insert(comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return resultado;
    }
    private static string Traducir_Tipo_Error(string CodigoError)
    {
        #region Traduccion de Errores GTI
        string mensaje = "";
        string[] separator = { "," };
        string[] errores = CodigoError.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        foreach (string error in errores)
        {
            switch (error)
            {
                case "CodError:01":
                    mensaje += "Codigo de moneda invalido (CodError:01) ,";
                    break;
                case "CodError:02":
                    mensaje += "Cuenta emisora no existe (CodError:02) ,";
                    break;
                case "CodError:03":
                    mensaje += "Identificador del receptor invalido (CodError:03) ,";
                    break;
                case "CodError:04":
                    mensaje += "Email del receptor invalido (CodError:04) ,";
                    break;
                case "CodError:05":
                    mensaje += "Condicion de venta invalida (CodError:05) ,";
                    break;
                case "CodError:06":
                    mensaje += "Copias de cortesia invalidas (CodError:06) ,";
                    break;
                case "CodError:07":
                    mensaje += "Informacion de referencia invalida (CodError:07) ,";
                    break;
                case "CodError:08":
                    mensaje += "Telefono o area del receptor invalida (CodError:08) ,";
                    break;
                case "CodError:09":
                    mensaje += "Tipo de documento invalido (CodError:09) ,";
                    break;
                case "CodError:10":
                    mensaje += "Tipo de identificacion del receptor invalida (CodError:10) ,";
                    break;
                case "CodError:11":
                    mensaje += "Cantidad de etiquetas de referencia invalida (CodError:11) ,";
                    break;
                case "CodError:12":
                    mensaje += "Cantidad de etiquetas de detalle invalida (CodError:12) ,";
                    break;
                case "CodError:13":
                    mensaje += "Factura referenciada posee 10 Notas. No se puede agregar la nota (CodError:13) ,";
                    break;
                case "CodError:14":
                    mensaje += "Nombre del receptor invalido (CodError:14) ,";
                    break;
                case "CodError:15":
                    mensaje += "Cantidad del producto o servicio invalida (CodError:15) ,";
                    break;
                case "CodError:16":
                    mensaje += "Codigo unidad no existe (CodError:16) ,";
                    break;
                case "CodError:17":
                    mensaje += "Precio unitario invalido (CodError:17) ,";
                    break;
                case "CodError:18":
                    mensaje += "Descuento invalido (CodError:18) ,";
                    break;
                case "CodError:19":
                    mensaje += "Longitud del codigo invalida (CodError:19) ,";
                    break;
                case "CodError:21"://CodError:20 No esta tipificado por GTI
                    mensaje += "Dirección del receptor invalida (CodError:21) ,";
                    break;
                case "CodError:22":
                    mensaje += "Tipo de moneda distinta a la factura referenciada (CodError:22) ,";
                    break;
                case "CodError:23":
                    mensaje += "Detalle de mercancia invalido (CodError:23) ,";
                    break;
                case "CodError:24":
                    mensaje += "Codigo de impuesto invalido (CodError:24) ,";
                    break;
                case "CodError:25":
                    mensaje += "Cantidad de etiquetas de codigo de impuesto mayor a dos (CodError:25) ,";
                    break;
                case "CodError:26":
                    mensaje += "Codigos de impuesto repetidos (CodError:26) ,";
                    break;
                case "CodError:27":
                    mensaje += "Monto de la Nota de credito no coincide con el monto total de la factura a anular (CodError:27) ,";
                    break;
                case "CodError:28":
                    mensaje += "Cantidad de rubros invalido (mayor o menor a uno) en nota de credito que anula (CodError:28) ,";
                    break;
                case "CodError:29":
                    mensaje += "Servicio fuera de linea (CodError:29) ,";
                    break;
                case "CodError:30":
                    mensaje += "Codigo de producto repetido en una o más lineas (CodError:30) ,";
                    break;
                case "CodError:31":
                    mensaje += "Documento al que se le va a aplicar una nota no existe (CodError:31) ,";
                    break;
                case "CodError:32":
                    mensaje += "Identificacion extranjera requerida (CodError:32) ,";
                    break;
                case "CodError:33":
                    mensaje += "Fecha de vencimiento menor a la fecha actual (CodError:33) ,";
                    break;
                case "CodError:34":
                    mensaje += "Formato de fecha incorrecto (CodError:34) ,";
                    break;
                case "CodError:35":
                    mensaje += "Naturaleza de descuento invalida o vacia (CodError:35) ,";
                    break;
                case "CodError:36":
                    mensaje += "Falta la etiqueta <Total> o Total de documento no coincide (CodError:36) ,";
                    break;
                case "CodError:37":
                    mensaje += "El monto del impuesto es Incorrecto (CodError:37) ,";
                    break;
                case "CodError:38":
                    mensaje += "El porcentaje del Impuesto es incorrecto (CodError:38) ,";
                    break;
                case "CodError:39":
                    mensaje += "La fecha de la factura es menor a 30 días o mayor a la fecha actual (CodError:39) ,";
                    break;
                case "CodError:40":
                    mensaje += "Sobrepasa la cantidad maxima de lineas (CodError:40) ,";
                    break;
                case "CodError:41":
                    mensaje += "Error al obtener el tipo cambio (No fue posible conectarse con el servicio del Banco Central de Costa Rica) (CodError:41) ,";
                    break;
                case "CodError:998":
                    mensaje += "Fallo la autenticación del usuario. Datos incorrectos (CodError:998) ,";
                    break;
                case "CodError:999":
                    mensaje += "Ocurrio un error inesperado (Comuníquese con el administrador) (CodError:999) ,";
                    break;
                case "CodError:P01":
                    mensaje += "Tipo de Cambio Personalizado (CodError:P01) ,";
                    break;
                case "CodError:P02":
                    mensaje += "Numero de Factura interno del Cliente (CodError:P02) ,";
                    break;
                case "CodError:P03":
                    mensaje += "Fecha Factura del Cliente (CodError:P03) ,";
                    break;
                case "CodError:46":
                    mensaje += "Provincia requerida (CodError:46) ,";
                    break;
                case "CodError:47":
                    mensaje += "Cantón requerido (CodError:47) ,";
                    break;
                case "CodError:48":
                    mensaje += "Distrito requerido (CodError:48) ,";
                    break;
                case "CodError:49":
                    mensaje += "Otras señas requeridas (CodError:49) ,";
                    break;
                case "CodError:50":
                    mensaje += "Número de área de receptor requerido (CodError:50) ,";
                    break;
                case "CodError:51":
                    mensaje += "Número de teléfono del receptor requerido (CodError:51) ,";
                    break;
                case "CodError:52":
                    mensaje += "Nombre del receptor requerido (CodError:52) ,";
                    break;
                case "CodError:53":
                    mensaje += "El monto Total de la factura no coincide con los cálculos (CodError:53) ,";
                    break;
                case "CodError:54":
                    mensaje += "Tipo de detalle inválido, debe ser M (Mercancía) o S (Servicio) (CodError:54) ,";
                    break;
                case "CodError:55":
                    mensaje += "Número de factura requerido (CodError:55) ,";
                    break;
                case "CodError:56":
                    mensaje += "Fecha de emisión de factura requerida (CodError:56) ,";
                    break;
                case "CodError:57":
                    mensaje += "No requiere datos de referencia (CodError:57) ,";
                    break;
                case "CodError:58":
                    mensaje += "Tipo de referencia inválido (CodError:58) ,";
                    break;
                case "CodError:59":
                    mensaje += "Código de referencia inválido (CodError:59) ,";
                    break;
                case "CodError:60":
                    mensaje += "Se requiere tipo de cabio (CodError:60) ,";
                    break;
                case "CodError:61":
                    mensaje += "Total inválido (CodError:61) ,";
                    break;
                case "CodError:62":
                    mensaje += "Situación de envió inválida (CodError:62) ,";
                    break;
                case "CodError:63":
                    mensaje += "Si existe código de área debe existir teléfono (CodError:63) ,";
                    break;
                case "CodError:64":
                    mensaje += "Si existe teléfono debe existir código de área (CodError:64) ,";
                    break;
                case "CodError:65":
                    mensaje += "Debe existir solo un correo para el receptor (CodError:65) ,";
                    break;
            }
        }
        return mensaje;
        #endregion
    }
    public ArrayList Crear_Factura_desde_Memoria(UsuarioBean user, ArrayList Arr_Documento)
    {
        ArrayList Arr_XML = new ArrayList();
        int ttrID = 1;
        decimal tipo_cambio = 1;
        string fechaEmision = "";
        Invoice = new XmlDocument();
        XML_Invoice = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
        try
        {
            FacturaBean FacturaElectronica = (FacturaBean)Arr_Documento[0];
            #region Definir Observaciones Electronica
            string Observaciones = "";
            if (FacturaElectronica.HBL.Trim() != "")
            {
                Observaciones += "HBL:" + FacturaElectronica.HBL + " ";
            }
            if (FacturaElectronica.Routing.Trim() != "")
            {
                Observaciones += "ROUTING:" + FacturaElectronica.Routing + " ";
            }
            if (FacturaElectronica.Contenedor.Trim() != "")
            {
                Observaciones += "CONTENEDOR:" + FacturaElectronica.Contenedor + " ";
            }
            if (FacturaElectronica.Shipper.Trim() != "")
            {
                Observaciones += "SHIPPER:" + FacturaElectronica.Shipper + " ";
            }
            if (FacturaElectronica.Paquetes.Trim() != "")
            {
                Observaciones += "PAQUETES:" + FacturaElectronica.cantPaquetes.ToString() + " - " + FacturaElectronica.Paquetes + " ";
            }
            if ((FacturaElectronica.Peso.Trim() != "") || (FacturaElectronica.Volumen.Trim() != ""))
            {
                Observaciones += "PESO:" + FacturaElectronica.Peso + " VOLUMEN.: " + FacturaElectronica.Volumen + " ";
            }
            if ((FacturaElectronica.Dua_Ingreso.Trim() != "") || (FacturaElectronica.Dua_Salida.Trim() != ""))
            {
                Observaciones += "DUA INGRESO:" + FacturaElectronica.Dua_Ingreso + " DUA SALIDA.: " + FacturaElectronica.Dua_Salida + " ";
            }
            if (FacturaElectronica.OrdenPO.Trim() != "")
            {
                Observaciones += "POLIZA:" + FacturaElectronica.OrdenPO + " ,";
            }
            if (FacturaElectronica.Regimen_Aduanero.ToString() != "0")
            {
                Observaciones += "REGIMEN ADUANERO:" + DB.Get_Regimen_Aduanero_XID(1, FacturaElectronica.Regimen_Aduanero).ToString() + " ";
            }
            if ((FacturaElectronica.CliID.ToString().Trim() != "") && (FacturaElectronica.CliID.ToString().Trim() != "0"))
            {
                Observaciones += "CODIGO DE CLIENTE:" + FacturaElectronica.CliID.ToString().Trim() + " ";
            }
            if (FacturaElectronica.Observaciones.Trim() != "")
            {
                Observaciones += "OBSERVACIONES:" + FacturaElectronica.Observaciones + " ";
            }
            Observaciones += "Estimado Cliente, a partir de la fecha de emision de la factura tiene 15 dias calendario para realizar cualquier reclamo sobre la misma ";
            #endregion
            //FacturaElectronica.Fecha_Emision = DateTime.Parse((FacturaElectronica.Fecha_Hora + " " + DateTime.Now.TimeOfDay.ToString())).ToString("yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            FacturaElectronica.Fecha_Emision = DateTime.Parse((FacturaElectronica.Fecha_Hora)).AddMinutes(-2).ToString("yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            fechaEmision = DateTime.Parse(FacturaElectronica.Fecha_Hora).ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            if (FacturaElectronica.MonedaID == 8)
            {
                tipo_cambio = DB.getTipoCambioByDay(user, fechaEmision);
            }
            DateTime aux_fecha_emision = DateTime.Parse(FacturaElectronica.Fecha_Hora.Substring(0, 10));
            double dias_credito = DB.getFechaMaxPago(int.Parse(FacturaElectronica.CliID.ToString()), 3, user);
            DateTime aux_fecha_pago = aux_fecha_emision.AddDays(dias_credito);
            string fecha_pago_formateada = aux_fecha_pago.ToString();
            FacturaElectronica.Fecha_Pago = DateTime.Parse((fecha_pago_formateada)).AddYears(1).ToString("yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);//yyyy-MM-ddTHH:mm:ss
            #region Determinar Tipo de Identificacion Tributaria
            string _TipoIdentificacion = "";
            if (FacturaElectronica.Tipo_Persona == 3)
            {
                string criterio = "";
                ArrayList clientearr = null;
                RE_GenericBean clienteBean = null;
                criterio = " a.id_cliente=" + FacturaElectronica.CliID.ToString();
                clientearr = (ArrayList)DB.getClientes(criterio, user, "");
                if ((clientearr != null) && (clientearr.Count > 0))
                {
                    clienteBean = (RE_GenericBean)clientearr[0];
                    _TipoIdentificacion = clienteBean.strC10;
                }
            }
            else
            {
                _TipoIdentificacion = "10";
            }
            #endregion
            #region Filtrar Identificacion Tributaria
            if (FacturaElectronica.Tipo_Persona == 3)
            {
                FacturaElectronica.Nit = FacturaElectronica.Nit.Trim();
                FacturaElectronica.Nit = FacturaElectronica.Nit.Replace("-", "");
                if (FacturaElectronica.Nit.Length > 10)
                {
                    if (_TipoIdentificacion != "3")
                    {
                        FacturaElectronica.Nit = FacturaElectronica.Nit.Substring(0, 10);
                    }
                }
            }
            else
            {
                FacturaElectronica.Nit = FacturaElectronica.Nit.Trim();
            }
            #endregion
            #region Copia Electronica de Cortesia
            string _CopiaCortesia = "";
            if ((FacturaElectronica.Correo_Electronico.Trim().Length > 0) && (FacturaElectronica.Correo_Electronico.Trim() != "-"))
            {
                FacturaElectronica.Correo_Electronico = FacturaElectronica.Correo_Electronico.Trim();
                FacturaElectronica.Correo_Electronico = FacturaElectronica.Correo_Electronico.Replace(",", ";");
                //_CopiaCortesia = FacturaElectronica.Correo_Electronico + " ; " + user.ID + "@aimargroup.com";
                _CopiaCortesia = FacturaElectronica.Correo_Electronico;
            }
            else
            {
                _CopiaCortesia = user.ID + "@aimargroup.com";
            }
            #endregion

            #region Construir XML

            var condicionVenta = Traducir_Condiciones_Venta_Hacienda(user, Convert.ToInt32(FacturaElectronica.CliID), FacturaElectronica.Tipo_Persona);

            Nodo_Encabezado = new XElement("Encabezado",
                new XElement("NumeroFactura", FacturaElectronica.tfa_ID),
                new XElement("FechaFactura", FacturaElectronica.Fecha_Emision),
                new XElement("Emisor",
                    new XElement("NumCuenta", _NumCuenta)
                    ),
				new XElement("CodigoActividad", "630101"),
                new XElement("TipoCambio", tipo_cambio),
                new XElement("TipoDoc", Traducir_Tipo_Documento_Hacienda(ttrID, _TipoIdentificacion)),
                //new XElement("NumConsecutivoCompr", ""),
                new XElement("CondicionVenta", condicionVenta),
                //new XElement("NumOrdenCompra", ""),
                new XElement("Moneda", Traducir_Moneda_Hacienda(FacturaElectronica.MonedaID)),
                new XElement("idMedioPago", "99"),
                condicionVenta == "02" ? new XElement("DiasCredito", dias_credito) : null,
                new XElement("Sucursal", "1"),
                new XElement("Terminal", "1"),
                new XElement("FechaVencimiento", FacturaElectronica.Fecha_Pago),
                new XElement("SituacionEnvio", "1"),
                new XElement("Receptor",
                    new XElement("TipoIdentificacion", _TipoIdentificacion),
                    new XElement("IdentificacionReceptor", FacturaElectronica.Nit),
                    new XElement("NombreReceptor", FacturaElectronica.Nombre),
                    new XElement("idProvincia", "1"),
                    new XElement("idCanton", "1"),
                    new XElement("idDistrito", "1"),
                    new XElement("idBarrio", "1"),
                    new XElement("DireccionReceptor", FacturaElectronica.Direccion),
                    //new XElement("NumeroAreaTelReceptor", "506"),
                    //new XElement("NumeroTelReceptor", "12345678"),
                    new XElement("CorreoElectronicoReceptor", _CopiaCortesia),
                    new XElement("CopiaCortesia", "cr-felectronica@aimargroup.com")
                    //new XElement("CorreoElectronicoReceptor", "soporte7@aimargroup.com"),
                    //new XElement("CopiaCortesia", "soporte7@aimargroup.com")
                    )
                );

            Nodo_Detalle = new XElement("Detalle");
            foreach (Rubros Rubro in FacturaElectronica.RubrosArr)
            {
                #region Determinar Codigo de Impuesto
                string _CodigoImpuesto_Temporal = "01";
                if (Rubro.rubroImpuesto == 0)
                {
                    _CodigoImpuesto_Temporal = "0";
                }
                #endregion
                Nodo_Linea = new XElement("Linea",
                    //new XElement("Tipo", "S"), //2019-07-01
                    new XElement("CodigoProducto", Rubro.rubroID),
                    new XElement("Cantidad", "1"),
                    new XElement("UnidadMedida", "24"), //2019-07-01
                    new XElement("DetalleMerc", Rubro.rubroName),
                    new XElement("PrecioUnitario", Rubro.rubroSubTot.ToString("##.00000"))
                    //new XElement("MontoDescuento", (0).ToString("##.00000")),
                    //new XElement("NaturalezaDescuento", "")
                );

                if (Rubro.rubroImpuesto > 0)
                {
                    double valor_impuesto = 0;
                    valor_impuesto = Rubro.rubroSubTot * 0.13;
                    valor_impuesto = double.Parse(valor_impuesto.ToString("F"));
                    Rubro.rubroImpuesto = valor_impuesto;
                    Nodo_Impuesto = new XElement("Impuestos",
                         new XElement("Impuesto",
                             new XElement("CodigoImpuesto", _CodigoImpuesto_Temporal),
							 new XElement("CodigoTarifa", "08"), //2019-07-01
                             new XElement("PorcentajeImpuesto", "13.00"),
                             new XElement("MontoImpuesto", Rubro.rubroImpuesto.ToString("##.00000"))
                         )
                     );
                    Nodo_Linea.Add(Nodo_Impuesto);
                    total_servicios_gravados += decimal.Parse(Rubro.rubroSubTot.ToString());
                    total_impuesto += decimal.Parse(Rubro.rubroImpuesto.ToString());
                }
                else
                {
                    total_servicios_exentos += decimal.Parse(Rubro.rubroSubTot.ToString());
                }
                Nodo_Detalle.Add(Nodo_Linea);
            }

            //Nodo_InformacionDeReferencia = new XElement("InformacionDeReferencia",
            //    new XElement("Referencia",
            //        new XElement("TpoDocRef", "0"),
            //        new XElement("NumeroReferencia", "0"),
            //        new XElement("CodigoReferencia", "0")
            //    )
            //);
            total_gravado = total_servicios_gravados;
            total_exento = total_servicios_exentos;
            total_venta = total_gravado + total_exento;
            total_venta_neta = total_venta;
            total_comprobante = total_venta_neta + total_impuesto;
            Nodo_Totales = new XElement("Totales",
				new XElement("TotalServGravados", total_servicios_gravados.ToString("##0.00000")),
				new XElement("TotalServExentos", total_servicios_exentos.ToString("##0.00000")),
				new XElement("TotalServExonerados", "0.00000"), //2019-07-01
				new XElement("TotalMercanciasGravadas", total_mercancias_gravadas.ToString("##0.00000")),
				new XElement("TotalMercanciasExentas", total_mercancias_exentas.ToString("##0.00000")),
				new XElement("TotalMercanciasExoneradas", "0.00000"), //2019-07-01                
				new XElement("TotalGravado", total_gravado.ToString("##0.00000")),
				new XElement("TotalExento", total_exento.ToString("##0.00000")),
				new XElement("TotalExonerado", "0.00000"), //2019-07-01                
				new XElement("TotalOtrosCargos", "0.00000"), //2019-07-01                
				new XElement("TotalVenta", total_venta.ToString("##0.00000")),
				new XElement("TotalDescuentos", total_descuentos.ToString("##0.00000")),
				new XElement("TotalIVADevuelto", "0.00000"), //2019-07-01                
				new XElement("TotalVentaNeta", total_venta_neta.ToString("##0.00000")),
				new XElement("TotalImpuesto", total_impuesto.ToString("##0.00000")),
				new XElement("TotalComprobante", total_comprobante.ToString("##0.00000"))
            );


            Nodo_Otros = new XElement("Otros", Observaciones);

            Nodo_FacturaElectronicaXML = new XElement("FacturaElectronicaXML");
            Nodo_FacturaElectronicaXML.Add(Nodo_Encabezado);
            Nodo_FacturaElectronicaXML.Add(Nodo_Detalle);
            Nodo_FacturaElectronicaXML.Add(Nodo_InformacionDeReferencia);
            Nodo_FacturaElectronicaXML.Add(Nodo_Totales);
            Nodo_FacturaElectronicaXML.Add(Nodo_Otros);

            Nodo_root = new XElement("root");
            Nodo_root.Add(Nodo_FacturaElectronicaXML);
            XML_Invoice.Add(Nodo_root);
            Invoice.InnerXml = XML_Invoice.ToString();

            string Path = "D:\\\\PRUEBA_FA_CR.xml";
            XmlTextWriter Writer = new XmlTextWriter(Path, new UTF8Encoding(false));
            Invoice.Save(Writer);
            Writer.Flush();
            Writer.Close();
            XML_Invoice = null;

            #endregion
            Arr_XML = new ArrayList();
            Arr_XML.Add("1");
            Arr_XML.Add(Invoice);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            Arr_XML = new ArrayList();
            Arr_XML.Add("0");
            Arr_XML.Add("Existio un error al crear XML de la Factura.: " + e.Message + " ");
        }
        return Arr_XML;
    }
    public ArrayList Crear_Nota_Debito_desde_Memoria(UsuarioBean user, ArrayList Arr_Documento)
    {
        ArrayList Arr_XML = new ArrayList();
        int ttrID = 4;
        decimal tipo_cambio = 1;
        string fechaEmision = "";
        Invoice = new XmlDocument();
        XML_Invoice = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
        try
        {
            FacturaBean NotaDebitoElectronica = (FacturaBean)Arr_Documento[0];

            #region Observaciones Electronicas
            string Observaciones = "";
            if (NotaDebitoElectronica.HBL.Trim() != "")
            {
                Observaciones += "HBL:" + NotaDebitoElectronica.HBL + " ";
            }
            if (NotaDebitoElectronica.Routing.Trim() != "")
            {
                Observaciones += "ROUTING:" + NotaDebitoElectronica.Routing + " ";
            }
            if (NotaDebitoElectronica.Contenedor.Trim() != "")
            {
                Observaciones += "CONTENEDOR:" + NotaDebitoElectronica.Contenedor + " ";
            }
            if (NotaDebitoElectronica.Shipper.Trim() != "")
            {
                Observaciones += "SHIPPER:" + NotaDebitoElectronica.Shipper + " ";
            }
            if ((NotaDebitoElectronica.Peso.Trim() != "") || (NotaDebitoElectronica.Volumen.Trim() != ""))
            {
                Observaciones += "PESO:" + NotaDebitoElectronica.Peso + " VOLUMEN.: " + NotaDebitoElectronica.Volumen + " ";
            }
            if ((NotaDebitoElectronica.Dua_Ingreso.Trim() != "") || (NotaDebitoElectronica.Dua_Salida.Trim() != ""))
            {
                Observaciones += "DUA INGRESO:" + NotaDebitoElectronica.Dua_Ingreso + " DUA SALIDA.: " + NotaDebitoElectronica.Dua_Salida + " ";
            }
            if ((NotaDebitoElectronica.CliID.ToString().Trim() != "") && (NotaDebitoElectronica.CliID.ToString().Trim() != "0"))
            {
                Observaciones += "CODIGO DE CLIENTE:" + NotaDebitoElectronica.CliID.ToString().Trim() + " ";
            }
            if (NotaDebitoElectronica.Observaciones.Trim() != "")
            {
                Observaciones += "OBSERVACIONES:" + NotaDebitoElectronica.Observaciones + " ";
            }
            Observaciones += "Estimado Cliente, a partir de la fecha de emision de la nota de debito tiene 15 dias calendario para realizar cualquier reclamo sobre la misma ";
            #endregion

            NotaDebitoElectronica.Fecha_Emision = DateTime.Parse((NotaDebitoElectronica.Fecha_Hora)).AddMinutes(-2).ToString("yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            fechaEmision = DateTime.Parse(NotaDebitoElectronica.Fecha_Hora).ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            if (NotaDebitoElectronica.MonedaID == 8)
            {
                tipo_cambio = DB.getTipoCambioByDay(user, fechaEmision);
            }
            DateTime aux_fecha_emision = DateTime.Parse(NotaDebitoElectronica.Fecha_Hora.Substring(0, 10));
            double dias_credito = DB.getFechaMaxPago(int.Parse(NotaDebitoElectronica.CliID.ToString()), 3, user);
            DateTime aux_fecha_pago = aux_fecha_emision.AddDays(dias_credito);
            string fecha_pago_formateada = aux_fecha_pago.ToString();
            NotaDebitoElectronica.Fecha_Pago = DateTime.Parse((fecha_pago_formateada)).AddYears(1).ToString("yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            string _TipoIdentificacion = "";
            string criterio = "";
            if (NotaDebitoElectronica.Tipo_Persona == 3)
            {
                #region Clientes
                #region Determinar Tipo de Identificacion Tributaria
                _TipoIdentificacion = "";
                criterio = "";
                ArrayList clientearr = null;
                RE_GenericBean clienteBean = null;
                criterio = " a.id_cliente=" + NotaDebitoElectronica.CliID.ToString();
                clientearr = (ArrayList)DB.getClientes(criterio, user, "");
                if ((clientearr != null) && (clientearr.Count > 0))
                {
                    clienteBean = (RE_GenericBean)clientearr[0];
                    _TipoIdentificacion = clienteBean.strC10;
                }
                #endregion
                #region Filtrar Identificacion Tributaria
                NotaDebitoElectronica.Nit = NotaDebitoElectronica.Nit.Trim();
                NotaDebitoElectronica.Nit = NotaDebitoElectronica.Nit.Replace("-", "");
                if (NotaDebitoElectronica.Nit.Length > 10)
                {
                    NotaDebitoElectronica.Nit = NotaDebitoElectronica.Nit.Substring(0, 10);
                }
                #endregion
                #endregion
            }
            else if (NotaDebitoElectronica.Tipo_Persona == 10)
            {
                #region Intercompanys
                #region Determinar Tipo de Identificacion Tributaria
                _TipoIdentificacion = "10";
                #endregion
                #region Filtrar Identificacion Tributaria
                NotaDebitoElectronica.Nit = NotaDebitoElectronica.Nit.Trim();
                #endregion
                #endregion
            }
            NotaDebitoElectronica.Fecha_Emision = DateTime.Parse((NotaDebitoElectronica.Fecha_Hora)).AddMinutes(-2).ToString("yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            #region Copia Electronica de Cortesia
            string _CopiaCortesia = "";
            if ((NotaDebitoElectronica.Correo_Electronico.Trim().Length > 0) && (NotaDebitoElectronica.Correo_Electronico.Trim() != "-"))
            {
                NotaDebitoElectronica.Correo_Electronico = NotaDebitoElectronica.Correo_Electronico.Trim();
                NotaDebitoElectronica.Correo_Electronico = NotaDebitoElectronica.Correo_Electronico.Replace(",", ";");
                //_CopiaCortesia = NotaDebitoElectronica.Correo_Electronico + " ; " + user.ID + "@aimargroup.com";
                _CopiaCortesia = NotaDebitoElectronica.Correo_Electronico;
            }
            else
            {
                _CopiaCortesia = user.ID + "@aimargroup.com";
            }
            #endregion
            #region Construir XML

            var condicionVenta = Traducir_Condiciones_Venta_Hacienda(user, Convert.ToInt32(NotaDebitoElectronica.CliID), NotaDebitoElectronica.Tipo_Persona);

            Nodo_Encabezado = new XElement("Encabezado",
                new XElement("NumeroFactura", ""),
                new XElement("FechaFactura", NotaDebitoElectronica.Fecha_Emision),
                new XElement("Emisor",
                    new XElement("NumCuenta", _NumCuenta)
                    ),
				new XElement("CodigoActividad", "630101"),
                new XElement("TipoCambio", tipo_cambio),
                new XElement("TipoDoc", Traducir_Tipo_Documento_Hacienda(ttrID, _TipoIdentificacion)),
                //new XElement("NumConsecutivoCompr", ""),
                new XElement("CondicionVenta", condicionVenta),
                //new XElement("NumOrdenCompra", ""),
                new XElement("Moneda", Traducir_Moneda_Hacienda(NotaDebitoElectronica.MonedaID)),
                new XElement("idMedioPago", "99"),
                condicionVenta == "02" ? new XElement("DiasCredito", dias_credito) : null,
                new XElement("Sucursal", "1"),
                new XElement("Terminal", "1"),
                new XElement("FechaVencimiento", NotaDebitoElectronica.Fecha_Pago),
                new XElement("SituacionEnvio", "1"),
                new XElement("Receptor",
                    new XElement("TipoIdentificacion", _TipoIdentificacion),
                    new XElement("IdentificacionReceptor", NotaDebitoElectronica.Nit),
                    new XElement("NombreReceptor", NotaDebitoElectronica.Nombre),
                    new XElement("idProvincia", "1"),
                    new XElement("idCanton", "1"),
                    new XElement("idDistrito", "1"),
                    new XElement("idBarrio", "1"),
                    new XElement("DireccionReceptor", NotaDebitoElectronica.Direccion),
                    //new XElement("NumeroAreaTelReceptor", "506"),
                    //new XElement("NumeroTelReceptor", "12345678"),
                    new XElement("CorreoElectronicoReceptor", _CopiaCortesia),
                    new XElement("CopiaCortesia", "cr-felectronica@aimargroup.com")
                    //new XElement("CorreoElectronicoReceptor", "soporte7@aimargroup.com"),
                    //new XElement("CopiaCortesia", "soporte7@aimargroup.com")
                    )
                );

            Nodo_Detalle = new XElement("Detalle");
            foreach (Rubros Rubro in NotaDebitoElectronica.RubrosArr)
            {
                #region Determinar Codigo de Impuesto
                string _CodigoImpuesto_Temporal = "01";
                if (Rubro.rubroImpuesto == 0)
                {
                    _CodigoImpuesto_Temporal = "0";
                }
                #endregion
                Nodo_Linea = new XElement("Linea",
                    //new XElement("Tipo", "S"), //2019-07-01
                    new XElement("CodigoProducto", Rubro.rubroID),
                    new XElement("Cantidad", "1"),
                    new XElement("UnidadMedida", "24"), //2019-07-01
                    new XElement("DetalleMerc", Rubro.rubroName),
                    new XElement("PrecioUnitario", Rubro.rubroSubTot.ToString("##.00000"))
                    //new XElement("MontoDescuento", (0).ToString("##.00000")),
                    //new XElement("NaturalezaDescuento", "")
                );

                if (Rubro.rubroImpuesto > 0)
                {
                    double valor_impuesto = 0;
                    valor_impuesto = Rubro.rubroSubTot * 0.13;
                    valor_impuesto = double.Parse(valor_impuesto.ToString("F"));
                    Rubro.rubroImpuesto = valor_impuesto;
                    Nodo_Impuesto = new XElement("Impuestos",
                         new XElement("Impuesto",
                             new XElement("CodigoImpuesto", _CodigoImpuesto_Temporal),
							 new XElement("CodigoTarifa", "08"), //2019-07-01
                             new XElement("PorcentajeImpuesto", "13.00"),
                             new XElement("MontoImpuesto", Rubro.rubroImpuesto.ToString("##.00000"))
                         )
                     );
                    Nodo_Linea.Add(Nodo_Impuesto);
                    total_servicios_gravados += decimal.Parse(Rubro.rubroSubTot.ToString());
                    total_impuesto += decimal.Parse(Rubro.rubroImpuesto.ToString());
                }
                else
                {
                    total_servicios_exentos += decimal.Parse(Rubro.rubroSubTot.ToString());
                }

                Nodo_Detalle.Add(Nodo_Linea);
            }

            var _NumeroReferencia = string.Empty;
            if (NotaDebitoElectronica.Factura_Ref_Doc != "")
                _NumeroReferencia = NotaDebitoElectronica.Factura_Ref_Doc; //2019-07-10
            else {
                if (user.PaisID == 21)
                {
                    _NumeroReferencia = NotaDebitoElectronica.Factura_Ref_Serie.Substring(6, 10) + NotaDebitoElectronica.Factura_Ref_Correlativo.ToString("0000000000.##");
                }
                else
                {
                    _NumeroReferencia = NotaDebitoElectronica.Factura_Ref_Serie.Substring(2, 10) + NotaDebitoElectronica.Factura_Ref_Correlativo.ToString("0000000000.##");
                }            
            }

            Nodo_InformacionDeReferencia = new XElement("InformacionDeReferencia",
                new XElement("Referencia",
                    new XElement("TpoDocRef", FechaGTI43(NotaDebitoElectronica.Factura_Ref_Fecha, _TipoIdentificacion)),                    
                    new XElement("NumeroReferencia", _NumeroReferencia),                    
                    new XElement("CodigoReferencia", "04") //referencia a otro documento //antes tenia 3 2019-07-08
                )
            );

            total_gravado = total_servicios_gravados;
            total_exento = total_servicios_exentos;
            total_venta = total_gravado + total_exento;
            total_venta_neta = total_venta;
            total_comprobante = total_venta_neta + total_impuesto;
            Nodo_Totales = new XElement("Totales",
				new XElement("TotalServGravados", total_servicios_gravados.ToString("##0.00000")),
				new XElement("TotalServExentos", total_servicios_exentos.ToString("##0.00000")),
				new XElement("TotalServExonerados", "0.00000"), //2019-07-01
				new XElement("TotalMercanciasGravadas", total_mercancias_gravadas.ToString("##0.00000")),
				new XElement("TotalMercanciasExentas", total_mercancias_exentas.ToString("##0.00000")),
				new XElement("TotalMercanciasExoneradas", "0.00000"), //2019-07-01                
				new XElement("TotalGravado", total_gravado.ToString("##0.00000")),
				new XElement("TotalExento", total_exento.ToString("##0.00000")),
				new XElement("TotalExonerado", "0.00000"), //2019-07-01                
				new XElement("TotalOtrosCargos", "0.00000"), //2019-07-01                
				new XElement("TotalVenta", total_venta.ToString("##0.00000")),
				new XElement("TotalDescuentos", total_descuentos.ToString("##0.00000")),
				new XElement("TotalIVADevuelto", "0.00000"), //2019-07-01                
				new XElement("TotalVentaNeta", total_venta_neta.ToString("##0.00000")),
				new XElement("TotalImpuesto", total_impuesto.ToString("##0.00000")),
				new XElement("TotalComprobante", total_comprobante.ToString("##0.00000"))
            );


            Nodo_Otros = new XElement("Otros", Observaciones);

            Nodo_FacturaElectronicaXML = new XElement("FacturaElectronicaXML");
            Nodo_FacturaElectronicaXML.Add(Nodo_Encabezado);
            Nodo_FacturaElectronicaXML.Add(Nodo_Detalle);
            Nodo_FacturaElectronicaXML.Add(Nodo_InformacionDeReferencia);
            Nodo_FacturaElectronicaXML.Add(Nodo_Totales);
            Nodo_FacturaElectronicaXML.Add(Nodo_Otros);

            Nodo_root = new XElement("root");
            Nodo_root.Add(Nodo_FacturaElectronicaXML);
            XML_Invoice.Add(Nodo_root);
            Invoice.InnerXml = XML_Invoice.ToString();

			string Path = "D:\\\\PRUEBA_ND_CR.xml";
            XmlTextWriter Writer = new XmlTextWriter(Path, new UTF8Encoding(false));
            Invoice.Save(Writer);
            Writer.Flush();
            Writer.Close();
            XML_Invoice = null;
            #endregion
            
			
            Arr_XML = new ArrayList();
            Arr_XML.Add("1");
            Arr_XML.Add(Invoice);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            Arr_XML = new ArrayList();
            Arr_XML.Add("0");
            Arr_XML.Add("Existio un error al crear XML de la Nota de Debito.: " + e.Message + " ");
        }
        return Arr_XML;
    }
    public ArrayList Crear_Nota_Credito_desde_Memoria(UsuarioBean user, ArrayList Arr_Documento)
    {
        ArrayList Arr_XML = new ArrayList();
        int ttrID = 3;
        decimal tipo_cambio = 1;
        string fechaEmision = "";
        string fechaPago = "";
        Invoice = new XmlDocument();
        XML_Invoice = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
        try
        {
            RE_GenericBean NotaCreditoElectronica = (RE_GenericBean)Arr_Documento[0];

            NotaCreditoElectronica.Fecha_Emision = DateTime.Parse((NotaCreditoElectronica.Fecha_Hora)).AddMinutes(-2).ToString("yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            fechaEmision = DateTime.Parse(NotaCreditoElectronica.Fecha_Hora).ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            if (NotaCreditoElectronica.intC2 == 8)
            {
                tipo_cambio = DB.getTipoCambioByDay(user, fechaEmision);
            }
            DateTime aux_fecha_emision = DateTime.Parse(NotaCreditoElectronica.Fecha_Hora.Substring(0, 10));
            double dias_credito = DB.getFechaMaxPago(int.Parse(NotaCreditoElectronica.intC7.ToString()), 3, user);
            DateTime aux_fecha_pago = aux_fecha_emision.AddDays(dias_credito);
            string fecha_pago_formateada = aux_fecha_pago.ToString();
            fechaPago = DateTime.Parse((fecha_pago_formateada)).AddYears(1).ToString("yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            #region Definir Data Nodo de Referencia
            string _TpoDocRef = "";
            string _NumeroReferencia = "";
            string _CodigoReferencia = "";
            string Observacion_Tipo_Documento = "";
            #region Tipo de Nota de Credito

            switch (NotaCreditoElectronica.Estado.ToString())
            {
                case "2": _CodigoReferencia = "04"; break;    //total es menor al saldo
                case "3": _CodigoReferencia = "01"; break;    //anula
                //case "4": _CodigoReferencia = "99"; break;    //total igual al saldo
                default: _CodigoReferencia = "99"; break;       //otros
            }
            /*if (NotaCreditoElectronica.Estado == 3)
            {
                //Nota de Credito que Anula
                _CodigoReferencia = "01";
            }
            else if (NotaCreditoElectronica.Estado == 2)
            {
                //Nota de Credito que Descuenta
                _CodigoReferencia = "04";   //03
            }*/
            #endregion
            #region Tipo de Documento al que se aplico la Nota de Credito
            //_TpoDocRef = Traducir_Tipo_Documento_Hacienda(NotaCreditoElectronica.intC10).ToString(); 2019-07-08
            if (NotaCreditoElectronica.intC10 == 1)
            {
                Observacion_Tipo_Documento = "Nota de Credito aplicada a Factura No.:" + NotaCreditoElectronica.strC3 + " ";
            }
            else if (NotaCreditoElectronica.intC10 == 4)
            {
                Observacion_Tipo_Documento = "Nota de Credito aplicada a Nota de Debito No.:" + NotaCreditoElectronica.strC3 + " ";
            }
            #endregion
            #region Numero de Referencia
            if (NotaCreditoElectronica.Factura_Ref_Doc != "")
                _NumeroReferencia = NotaCreditoElectronica.Factura_Ref_Doc; //2019-07-11
            else { 
                if (user.PaisID == 21) 
                    _NumeroReferencia = NotaCreditoElectronica.strC2.Substring(6, 10);               
                else
                    _NumeroReferencia = NotaCreditoElectronica.strC2.Substring(2, 10);
                _NumeroReferencia = _NumeroReferencia + int.Parse(NotaCreditoElectronica.strC3).ToString("0000000000.##");
            }                        
            #endregion
			#endregion

            #region Observaciones Electronicas
            string Observaciones = "";
            if (NotaCreditoElectronica.strC9.Trim() != "")
            {
                Observaciones += "HBL.: " + NotaCreditoElectronica.strC9 + " ,";
            }
            if (NotaCreditoElectronica.strC12.Trim() != "")
            {
                Observaciones += "ROUTING.: " + NotaCreditoElectronica.strC12 + " ,";
            }
            if (NotaCreditoElectronica.strC11.Trim() != "")
            {
                Observaciones += "CONTENEDOR.: " + NotaCreditoElectronica.strC11 + " ,";
            }
            if ((NotaCreditoElectronica.intC7.ToString().Trim() != "") && (NotaCreditoElectronica.intC7.ToString().Trim() != "0"))
            {
                Observaciones += "CODIGO DE CLIENTE.: " + NotaCreditoElectronica.intC7.ToString().Trim() + " ,";
            }
            if (NotaCreditoElectronica.strC1.Trim() != "")
            {
                Observaciones += "OBSERVACIONES.: " + NotaCreditoElectronica.strC1 + " ,";
            }

            Observaciones += "OBSERVACIONES.: " + NotaCreditoElectronica.strC1 + " ,";
            Observaciones += Observacion_Tipo_Documento;
            #endregion

            #region Determinar Tipo de Identificacion Tributaria
            string _TipoIdentificacion = "";
            if (NotaCreditoElectronica.intC6 == 3)
            {
                string criterio = "";
                ArrayList clientearr = null;
                RE_GenericBean clienteBean = null;
                criterio = " a.id_cliente=" + NotaCreditoElectronica.intC7.ToString();
                clientearr = (ArrayList)DB.getClientes(criterio, user, "");
                if ((clientearr != null) && (clientearr.Count > 0))
                {
                    clienteBean = (RE_GenericBean)clientearr[0];
                    _TipoIdentificacion = clienteBean.strC10;
                }
            }
            else
            {
                _TipoIdentificacion = "10";
            }
            #endregion

            _TpoDocRef = Traducir_Tipo_Documento_Hacienda(NotaCreditoElectronica.intC10, _TipoIdentificacion).ToString(); //2019-07-08

            if (_TpoDocRef == "02")
            {
                _TpoDocRef = "01"; //2019-07-17 NC no puede referenciar a la ND trae los datos de factura
                if (_CodigoReferencia == "01") _CodigoReferencia = "99";
            }

            #region Filtrar Identificacion Tributaria
            if (NotaCreditoElectronica.intC6 == 3)
            {
                NotaCreditoElectronica.strC35 = NotaCreditoElectronica.strC35.Trim();
                NotaCreditoElectronica.strC35 = NotaCreditoElectronica.strC35.Replace("-", "");
                if (NotaCreditoElectronica.strC35.Length > 10)
                {
                    NotaCreditoElectronica.strC35 = NotaCreditoElectronica.strC35.Substring(0, 10);
                }
            }
            else
            {
                NotaCreditoElectronica.strC35 = NotaCreditoElectronica.strC35.Trim();
            }
            #endregion
            #region Copia Electronica de Cortesia
            string _CopiaCortesia = "";
            if ((NotaCreditoElectronica.strC36.Trim().Length > 0) && (NotaCreditoElectronica.strC36.Trim() != "-"))
            {
                NotaCreditoElectronica.strC36 = NotaCreditoElectronica.strC36.Trim();
                NotaCreditoElectronica.strC36 = NotaCreditoElectronica.strC36.Replace(",", ";");
                //_CopiaCortesia = NotaCreditoElectronica.strC36 + " ; " + user.ID + "@aimargroup.com";
                _CopiaCortesia = NotaCreditoElectronica.strC36;
            }
            else
            {
                _CopiaCortesia = user.ID + "@aimargroup.com";
            }
            #endregion
            #region Construir XML

            var condicionVenta = Traducir_Condiciones_Venta_Hacienda(user, Convert.ToInt32(NotaCreditoElectronica.intC7), NotaCreditoElectronica.intC6);

            Nodo_Encabezado = new XElement("Encabezado",
                new XElement("NumeroFactura", ""),
                new XElement("FechaFactura", NotaCreditoElectronica.Fecha_Emision),
                new XElement("Emisor",
                    new XElement("NumCuenta", _NumCuenta)
                    ),
				new XElement("CodigoActividad", "630101"),
                new XElement("TipoCambio", tipo_cambio),
                new XElement("TipoDoc", Traducir_Tipo_Documento_Hacienda(ttrID, _TipoIdentificacion)),
                //new XElement("NumConsecutivoCompr", ""),
                new XElement("CondicionVenta", condicionVenta),
                //new XElement("NumOrdenCompra", ""),
                new XElement("Moneda", Traducir_Moneda_Hacienda(NotaCreditoElectronica.intC2)),
                new XElement("idMedioPago", "99"),
                condicionVenta == "02" ? new XElement("DiasCredito", dias_credito) : null,
                new XElement("Sucursal", "1"),
                new XElement("Terminal", "1"),
                //new XElement("FechaVencimiento", fechaPago),
                new XElement("SituacionEnvio", "1"),
                new XElement("Receptor",
                    new XElement("TipoIdentificacion", _TipoIdentificacion),
                    new XElement("IdentificacionReceptor", NotaCreditoElectronica.strC35),
                    new XElement("NombreReceptor", NotaCreditoElectronica.Nombre_Cliente),
                    new XElement("idProvincia", "1"),
                    new XElement("idCanton", "1"),
                    new XElement("idDistrito", "1"),
                    new XElement("idBarrio", "1"),
                    new XElement("DireccionReceptor", NotaCreditoElectronica.Direccion),
                    //new XElement("NumeroAreaTelReceptor", "506"),
                    //new XElement("NumeroTelReceptor", "12345678"),
                    new XElement("CorreoElectronicoReceptor", _CopiaCortesia),
                    new XElement("CopiaCortesia", "cr-felectronica@aimargroup.com")
                    //new XElement("CorreoElectronicoReceptor", "soporte7@aimargroup.com"),
                    //new XElement("CopiaCortesia", "soporte7@aimargroup.com")
                    )
                );

            Nodo_Detalle = new XElement("Detalle");
            foreach (Rubros Rubro in NotaCreditoElectronica.arr1)
            {
                #region Determinar Codigo de Impuesto
                string _CodigoImpuesto_Temporal = "01";
                if (Rubro.rubroImpuesto == 0)
                {
                    _CodigoImpuesto_Temporal = "0";
                }
                #endregion
                Nodo_Linea = new XElement("Linea",
                    //new XElement("Tipo", "S"), //2019-07-01
                    new XElement("CodigoProducto", Rubro.rubroID),
                    new XElement("Cantidad", "1"),
                    new XElement("UnidadMedida", "24"), //2019-07-01
                    new XElement("DetalleMerc", Rubro.rubroName),
                    new XElement("PrecioUnitario", Rubro.rubroSubTot.ToString("##.00000"))
                    //new XElement("MontoDescuento", (0).ToString("##.00000")),
                    //new XElement("NaturalezaDescuento", "")
                );
                if (Rubro.rubroImpuesto > 0)
                {
                    double valor_impuesto = 0;
                    valor_impuesto = Rubro.rubroSubTot * 0.13;
                    valor_impuesto = double.Parse(valor_impuesto.ToString("F"));
                    Rubro.rubroImpuesto = valor_impuesto;
                    Nodo_Impuesto = new XElement("Impuestos",
                         new XElement("Impuesto",
                             new XElement("CodigoImpuesto", _CodigoImpuesto_Temporal),
							 new XElement("CodigoTarifa", "08"), //2019-07-01
                             new XElement("PorcentajeImpuesto", "13.00"),
                             new XElement("MontoImpuesto", Rubro.rubroImpuesto.ToString("##.00000"))
                         )
                     );
                    Nodo_Linea.Add(Nodo_Impuesto);
                    total_servicios_gravados += decimal.Parse(Rubro.rubroSubTot.ToString());
                    total_impuesto += decimal.Parse(Rubro.rubroImpuesto.ToString());
                }
                else
                {
                    total_servicios_exentos += decimal.Parse(Rubro.rubroSubTot.ToString());
                }

                Nodo_Detalle.Add(Nodo_Linea);
            }

            Nodo_InformacionDeReferencia = new XElement("InformacionDeReferencia",
                new XElement("Referencia",
                    //new XElement("TpoDocRef", _TpoDocRef),
                    new XElement("TpoDocRef", FechaGTI43_NC(NotaCreditoElectronica.Factura_Ref_Fecha, _TipoIdentificacion, _TpoDocRef)),                     
                    new XElement("NumeroReferencia", _NumeroReferencia),
                    new XElement("CodigoReferencia", _CodigoReferencia)
                )
            );
            total_gravado = total_servicios_gravados;
            total_exento = total_servicios_exentos;
            total_venta = total_gravado + total_exento;
            total_venta_neta = total_venta;
            total_comprobante = total_venta_neta + total_impuesto;
            Nodo_Totales = new XElement("Totales",
				new XElement("TotalServGravados", total_servicios_gravados.ToString("##0.00000")),
				new XElement("TotalServExentos", total_servicios_exentos.ToString("##0.00000")),
				new XElement("TotalServExonerados", "0.00000"), //2019-07-01
				new XElement("TotalMercanciasGravadas", total_mercancias_gravadas.ToString("##0.00000")),
				new XElement("TotalMercanciasExentas", total_mercancias_exentas.ToString("##0.00000")),
				new XElement("TotalMercanciasExoneradas", "0.00000"), //2019-07-01                
				new XElement("TotalGravado", total_gravado.ToString("##0.00000")),
				new XElement("TotalExento", total_exento.ToString("##0.00000")),
				new XElement("TotalExonerado", "0.00000"), //2019-07-01                
				new XElement("TotalOtrosCargos", "0.00000"), //2019-07-01                
				new XElement("TotalVenta", total_venta.ToString("##0.00000")),
				new XElement("TotalDescuentos", total_descuentos.ToString("##0.00000")),
				new XElement("TotalIVADevuelto", "0.00000"), //2019-07-01                
				new XElement("TotalVentaNeta", total_venta_neta.ToString("##0.00000")),
				new XElement("TotalImpuesto", total_impuesto.ToString("##0.00000")),
				new XElement("TotalComprobante", total_comprobante.ToString("##0.00000"))
            );

            Nodo_Otros = new XElement("Otros", Observaciones);

            Nodo_FacturaElectronicaXML = new XElement("FacturaElectronicaXML");
            Nodo_FacturaElectronicaXML.Add(Nodo_Encabezado);
            Nodo_FacturaElectronicaXML.Add(Nodo_Detalle);
            Nodo_FacturaElectronicaXML.Add(Nodo_InformacionDeReferencia);
            Nodo_FacturaElectronicaXML.Add(Nodo_Totales);
            Nodo_FacturaElectronicaXML.Add(Nodo_Otros);

            Nodo_root = new XElement("root");
            Nodo_root.Add(Nodo_FacturaElectronicaXML);
            XML_Invoice.Add(Nodo_root);
            Invoice.InnerXml = XML_Invoice.ToString();

            string Path = "D:\\\\PRUEBA_NC_CR.xml";
            XmlTextWriter Writer = new XmlTextWriter(Path, new UTF8Encoding(false));
            Invoice.Save(Writer);
            Writer.Flush();
            Writer.Close();
            XML_Invoice = null;

            #endregion

            Arr_XML = new ArrayList();
            Arr_XML.Add("1");
            Arr_XML.Add(Invoice);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            Arr_XML = new ArrayList();
            Arr_XML.Add("0");
            Arr_XML.Add("Existio un error al crear XML de la Nota de Debito.: " + e.Message + " ");
        }
        return Arr_XML;
    }


    public static string FechaGTI43(string fecha, string _TipoIdentificacion)
    {
        string valor = "";

        DateTime oDate1 = Convert.ToDateTime("01/07/2019");
        DateTime oDate2 = Convert.ToDateTime(fecha);
        int result = DateTime.Compare(oDate1, oDate2);        

        //si es mayor que cero es antes del 1ro de julio y solo se referenciaba a facturas 01
        valor = result > 0 ? "01" : (_TipoIdentificacion == "10" ? "04" : "01");
        //si fecha es mayor o igual al 1ro de julio valida tipo identificacion, si es extranjero asigna 04 tiquete sino es 01 factura
        //por el momento solo hace validacion para nota de debito

        return valor;
    }


    public static string FechaGTI43_NC(string fecha, string _TipoIdentificacion, string _TpoDocRef)
    {   //fecha yyyy/mm/dd este formato trae la nota de credito

        //DateTime ahora = DateTime.Now;
        string[] f = fecha.Split('/');
        //DateTime fecha2 = new DateTime(int.Parse(f[2]), int.Parse(f[1]), int.Parse(f[0]));
        string fecha2 = f[2] + "/" + f[1] + "/" + f[0];

        string valor = "";        
        if (_TpoDocRef == "01") //factura
            valor = FechaGTI43(fecha2, _TipoIdentificacion);                    
        //if (_TpoDocRef == "02") //nota debito
        else
            valor = _TpoDocRef;
        return valor;
    }
}
