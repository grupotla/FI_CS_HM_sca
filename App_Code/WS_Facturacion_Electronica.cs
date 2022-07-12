using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Collections;
using System.Net.Mail;
using System.Net;

/// <summary>
/// Summary description for WS_Facturacion_Electronica
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WS_Facturacion_Electronica : System.Web.Services.WebService 
{
    string Documentos_Exitosos = "";
    string Documentos_Fallidos = "";
    int Total_Documentos_Exitosos = 0;
    int Total_Documentos_Fallidos = 0;
    int Total_Documentos = 0;
    int bandera = 0;
    public WS_Facturacion_Electronica () 
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod]
    public bool Generar_Proceso_Batch(int paiID, int sessionID)
    {
        bool resultado = false;
        UsuarioBean user = new UsuarioBean();
        user.PaisID = paiID;
        string Guid = "";
        XmlDocument ExmlDoc = new XmlDocument();
        string Signature = "";
        Documentos_Exitosos = "";
        Documentos_Fallidos = "";
        ArrayList Arr_Documentos = (ArrayList)Contabilizacion_Automatica_CAD.Get_Transacciones_SCA_Pendientes_Firma(paiID, sessionID);
        if ((paiID == 1) || (paiID == 15))
        {
            #region Facturacion Electronica de Guatemala
            foreach (XML_Bean Bean_Documentos in Arr_Documentos)
            {
                Total_Documentos++;

                if (DB.isFELDate(paiID)) { //2019-07-29
                    
                    #region FEL 2019-05-03
                    try
                    {
                        string facide = Bean_Documentos.intC1.ToString();
                        //2019-04-23
                        //http://10.10.1.7:9191/WebService1.asmx
                        fel_101017.WebService1 proceso = new fel_101017.WebService1();
                        var resultado2 = proceso.Proceso_07_Completo("", "", facide, user.ID, Bean_Documentos.stC1, paiID.ToString());  
                        if (resultado2[3] == "" && resultado2[0] != "")
                        {
                            //resultado2[3] = "Key : " + resultado2[0] + " Serie : " + resultado2[1] + " Correlativo :" + resultado2[2];                        
                            Documentos_Exitosos += "-ACTUALIZACION DE FIRMA EXITOSA.: " + Bean_Documentos.stC1 + " , " + Bean_Documentos.intC1.ToString() + " , " + Bean_Documentos.strC6 + "-" + Bean_Documentos.stC5 + " , " + " , " + Signature + "\n";
                            Total_Documentos_Exitosos++;
                            resultado = true;
                            bandera++;
                        }
                        else
                        {
                            resultado = false;
                            Documentos_Fallidos += "Existio un error al Transmitir Factura (" + Bean_Documentos.intC1 + "), " + Bean_Documentos.strC6 + "-" + Bean_Documentos.stC5 + " , " + resultado2[3] + ", \n";
                            Total_Documentos_Fallidos++;
                        }
                    }
                    catch (Exception ex)
                    {
                        Documentos_Fallidos += "Existio un error al Transmitir Factura (" + Bean_Documentos.intC1 + "), " + Bean_Documentos.strC6 + "-" + Bean_Documentos.stC5 + " , " + ex.Message + ", \n";
                        Total_Documentos_Fallidos++;
                        resultado = false;
                    }
                    #endregion 
                    
                } else { 

                    Guid = DB.Validar_Referencia_Interna_GFACE(Bean_Documentos.stC2, paiID);
                    if (Guid == "-100")
                    {
                                    #region Error al Validar con GFACE
                    resultado = false;
                    return resultado;
                    #endregion
                    }
                    else if (Guid != "0")
                    {
                                                                                                            #region Documento Procesado por el GFACE pero no recibido por Aimar
                    GFACEWEBSERVICE.TransactionTag Tag = new GFACEWEBSERVICE.TransactionTag();
                    Tag = (GFACEWEBSERVICE.TransactionTag)DB.ObtenerDocumentoTransmitido(user, Guid, Bean_Documentos.stC2);
                    ExmlDoc.InnerXml = DB.Base64String_String(Tag.ResponseData.ResponseData1);
                    Signature = DB.Get_Signature(user, ExmlDoc);
                    #region Actualizar Datos de Transmision
                    int result_signature = 0;
                    ArrayList EArr = new ArrayList();
                    EArr.Add(Bean_Documentos.stC1);
                    EArr.Add(Signature);
                    EArr.Add(Bean_Documentos.intC2);
                    EArr.Add(Bean_Documentos.intC1);
                    EArr.Add(Bean_Documentos.stC2);
                    EArr.Add(Guid);
                    EArr.Add(Bean_Documentos.stC5);
                    EArr.Add(ExmlDoc);
                    result_signature = DB.Update_Signature(user, EArr);
                    Documentos_Exitosos += "-ACTUALIZACION DE FIRMA EXITOSA.: " + Bean_Documentos.stC1 + " , " + Bean_Documentos.intC1.ToString() + " , " + Signature + "\n";
                    Total_Documentos_Exitosos++;
                    #endregion
                    resultado = true;
                    #endregion
                    }
                    else if (Guid == "0")
                    {
                        #region Documento no Transmitido al GFACE
                        ExmlDoc = (XmlDocument)DB.Generar_XMLNativo(int.Parse(Bean_Documentos.stC1), Bean_Documentos.intC1);
                        Bean_Documentos.stC4 = ExmlDoc.InnerXml;
                        resultado = Transmitir_Documento(Bean_Documentos, paiID);
                        bandera++;
                        #endregion
                    }
                }

            }
            #endregion
        }
        else if ((paiID == 5) || (paiID == 21))
        {
            #region Facturacion Electronica de Costa Rica
            ArrayList Arr_Transmision_CR = null;
            EInvoice_CR EInvoice = null;
            Total_Documentos = 0;
            Total_Documentos_Exitosos = 0;
            foreach (XML_Bean Bean_Documentos in Arr_Documentos)
            {
                Total_Documentos++;
                EInvoice = new EInvoice_CR(paiID);
                Arr_Transmision_CR = new ArrayList();
                Arr_Transmision_CR = EInvoice.Generar_Firma_Electronica(user, int.Parse(Bean_Documentos.stC1), Bean_Documentos.intC1, null);
                if (Arr_Transmision_CR[0].ToString() == "0")
                {
                    Total_Documentos_Fallidos++;
                    if (Bean_Documentos.stC1 == "1")
                    {
                        Documentos_Fallidos += "Exitio un error al Transmitir Factura (" + Bean_Documentos.intC1 + ") .: " + Arr_Transmision_CR[1].ToString() + ", ";
                    }
                    else if (Bean_Documentos.stC1 == "4")
                    {
                        Documentos_Fallidos += "Exitio un error al Transmitir Nota de Debito (" + Bean_Documentos.intC1 + ") .: " + Arr_Transmision_CR[1].ToString() + ", ";
                    }
                }
                else if (Arr_Transmision_CR[0].ToString() == "1")
                {
                    #region Actualizar Datos de Factura Electronica
                    int resultado_actualizar_datos = 0;
                    EInvoice = new EInvoice_CR(paiID);
                    resultado_actualizar_datos = EInvoice.Actualizar_Datos_Documento_Electronico(int.Parse(Bean_Documentos.stC1), Bean_Documentos.intC1, Arr_Transmision_CR[2].ToString(), Arr_Transmision_CR[3].ToString(), Arr_Transmision_CR[4].ToString());
                    EInvoice = null;
                    #endregion
                    Total_Documentos_Exitosos++;
                    if (Bean_Documentos.stC1 == "1")
                    {
                        Documentos_Exitosos += "Factura transmitida exitosamente .: " + Arr_Transmision_CR[2].ToString() + ", ";
                    }
                    else if (Bean_Documentos.stC1 == "4")
                    {
                        Documentos_Exitosos += "Nota de Debito transmitida exitosamente .: " + Arr_Transmision_CR[2].ToString() + ", ";
                    }

                    //2019-08-29 Correccion. Actualizacion al correlativo en sistema sca, del resultado de firma hhmm
                    try
                    {
                        string iCorr = Arr_Transmision_CR[2].ToString();
                        DB.setCorrGTI(iCorr, Bean_Documentos.stC1, Bean_Documentos.intC1.ToString());
                    } catch(Exception e) {
                        string ex = e.Message;
                    }

                }
            }
            #endregion
        }
        Enviar_Reporte();
        return resultado;
    }
    protected bool Transmitir_Documento(XML_Bean Bean_Documentos, int paiID)
    {
        UsuarioBean user = new UsuarioBean();
        user.PaisID = paiID;
        bool resultado = false;
        XmlDocument ExmlDoc = new XmlDocument();
        string Signature = "";
        ExmlDoc.InnerXml = Bean_Documentos.stC4;
        int intentos_transmision = 0;
        GFACEWEBSERVICE.TransactionTag Tag = new GFACEWEBSERVICE.TransactionTag();
        do
        {
            Tag = (GFACEWEBSERVICE.TransactionTag)DB.Transmitir_Documento_Electronico(user, ExmlDoc, Bean_Documentos.stC2);
            intentos_transmision += 1;
        } while (((Tag == null)) && (intentos_transmision <= 3));
        if (Tag == null)
        {
            #region Transmision Fallida
            Documentos_Fallidos += "-TRANSMISION FALLIDA.: " + Bean_Documentos.stC1 + " , " + Bean_Documentos.intC1.ToString() + "\n";
            Total_Documentos_Fallidos++;
            resultado = false;
            return resultado;
            #endregion
        }
        else if (Tag.Response.Result == false)
        {
            #region XML Invalido
            Documentos_Fallidos += "-XML INVALIDO.: " + Bean_Documentos.stC1 + " , " + Bean_Documentos.intC1.ToString() + " , " + Tag.Response.Hint.ToString() + " , " + Tag.Response.Description.ToString() + " \n";
            Total_Documentos_Fallidos++;
            resultado = false;
            return resultado;
            #endregion
        }
        else if (Tag.Response.Result == true)
        {
            #region Transmision Exitosa
            int result_signature = 0;
            ArrayList EArr = new ArrayList();
            ExmlDoc.InnerXml = DB.Base64String_String(Tag.ResponseData.ResponseData1);
            Signature = DB.Get_Signature(user, ExmlDoc);
            EArr.Add(Bean_Documentos.stC1);
            EArr.Add(Signature);
            EArr.Add(Bean_Documentos.intC2);
            EArr.Add(Bean_Documentos.intC1);
            EArr.Add(Bean_Documentos.stC2);
            EArr.Add(Tag.Response.Identifier.DocumentGUID);
            EArr.Add(Bean_Documentos.stC5);
            ExmlDoc.InnerXml = Bean_Documentos.stC4;
            EArr.Add(ExmlDoc);
            result_signature = DB.Update_Signature(user, EArr);
            Documentos_Exitosos += "-TRANSMISION EXITOSA.: " + Bean_Documentos.stC1 + " , " + Bean_Documentos.intC1.ToString() + " , " + Signature + "\n";
            Total_Documentos_Exitosos++;
            resultado = true;
            #endregion
        }
        return resultado;
    }
    protected void Enviar_Reporte()
    {
        #region Enviar Reporte por Correo
        System.Net.Mail.MailMessage Email = new System.Net.Mail.MailMessage();
        string Server = "mail.aimargroup.com";
        Email.From = new System.Net.Mail.MailAddress("soporte6@aimargroup.com");
        Email.To.Add("soporte6@aimargroup.com");
        Email.To.Add("soporte5@aimargroup.com");
        Email.Subject = "Facturacion Electronica SCA - AIMAR - " + DateTime.Now.ToString();
        Email.Body += " \n";
        Email.Body += string.Concat("Fecha-Hora de ejecución: ", DateTime.Now.ToString());
        Email.Body += " \n";
        Email.Body += " \n";
        Email.Body += "Total de Documentos Procesados.: " + Total_Documentos.ToString() + "  \n";
        Email.Body += "Total de Documentos Exitosos.: " + Total_Documentos_Exitosos.ToString() + "  \n";
        Email.Body += "Total de Documentos Fallidos.: " + Total_Documentos_Fallidos.ToString() + "  \n";
        Email.Body += " \n";
        Email.Body += "Listado de documentos exitosos: \n";
        Email.Body += Documentos_Exitosos + "  \n";
        Email.Body += " \n";
        Email.Body += "Listado de Documentos Fallidos.: \n";
        Email.Body += Documentos_Fallidos + "  \n";
        SmtpClient Cliente_Smtp = new SmtpClient(Server);
        Cliente_Smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
        try
        {
            Cliente_Smtp.Send(Email);
        }
        catch (Exception ex)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(ex.Message);
        }
        #endregion
    }
    [WebMethod]
    public XmlDocument Generar_XMLNativo(int ttrID, int docID)
    {
        #region Generar XML Nativo
        XmlDocument ExmlDoc = new XmlDocument();
        UsuarioBean user = new UsuarioBean();
        ExmlDoc = (XmlDocument)DB.Generar_XMLNativo(ttrID, docID);
        return ExmlDoc;
        #endregion
    }
}
