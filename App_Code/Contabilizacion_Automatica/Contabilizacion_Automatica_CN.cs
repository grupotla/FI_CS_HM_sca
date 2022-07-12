using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using Npgsql;
using System.Data;
using System.Xml;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Net.Mime;
using System.Threading;

public class Contabilizacion_Automatica_CN
{
    public static string mensaje = "";
    public static UsuarioBean user = null;
    public static PaisBean Pais_Bean = null;
    public static string Tipo_Contabilizacion = "Master";
    public Contabilizacion_Automatica_CN()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static ArrayList Generar_Contabilizacion_Automatica(bool _MBL, bool _HBL, bool _Routing, bool _Imp_Exp, int _paisID, int _sisID, int _ttoID, int _blID, string _usuID)
    {   
        ArrayList resultado = new ArrayList();
        int casoID = 0;
        int cantidad_costos_sin_transaccion_asociada = 0;
        int alertaID = 0;

        ArrayList Arr_Transacciones_Asociadas = new ArrayList();
        ArrayList Arr_Grupo_Costos = new ArrayList();
        Bean_Costos Bean_I = new Bean_Costos();
        Bean_Costos Bean_J = new Bean_Costos();

        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlTransaction transaction;
        conn = DB.OpenConnection();
        comm = new NpgsqlCommand();
        comm.Connection = conn;
        transaction = conn.BeginTransaction();
        comm.Transaction = transaction;
        comm.CommandType = CommandType.Text;
        try
        {
            #region 0. Definir Parametros Iniciales
            XmlDocument XMLContabilizacion_Automatica = null;
            XmlElement NDContabilizacion_Automatica = null;
            XMLContabilizacion_Automatica = new XmlDocument();
            NDContabilizacion_Automatica = XMLContabilizacion_Automatica.CreateElement("Contabilizacion_Automatica");
            Pais_Bean = Contabilizacion_Automatica_CAD.Cargar_Pais(_paisID.ToString());
            #endregion
            #region 1. Determinar Caso
            casoID = Determinar_Caso(_MBL, _HBL, _Routing, _Imp_Exp);
            if (casoID == 0)
            {
                alertaID = 11;
                mensaje = "No Existe ningun Caso de Contabilizacion Automatica con la Configuracion MASTER.: " + Traducir_Variables("MBL", _MBL) + ", HOUSE.: " + Traducir_Variables("HBL", _HBL) + ", " + Traducir_Variables("ROUTING", _Routing) + ", " + Traducir_Variables("IMPORT_EXPORT", _Imp_Exp) + " .";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            else if (casoID == -100)
            {
                alertaID = 11;
                mensaje = "Existio un error al Tratar de Determinar el Caso de Contabilizacion Automatica";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            #endregion
            #region Identificar Tipos de Personas
            RE_GenericBean Bean_Personas_BL = DB.Get_Tipos_Personas_X_Sistema_Tipo_Operacion_BLID(Pais_Bean, _sisID, _ttoID, _blID);
            if (Bean_Personas_BL == null)
            {
                alertaID = 10;
                mensaje = "Error al momento de Generar Contabilizacion Automatica";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            #endregion
            #region Verificar si se Contabiliza por Master o House
            RE_GenericBean Bean_Grupo_Agentes = (RE_GenericBean)DB.Get_Grupo_Agentes_X_ID(int.Parse(Bean_Personas_BL.strC1));
            if (Bean_Grupo_Agentes == null)
            {
                alertaID = 10;
                mensaje = "Error al momento de Generar Contabilizacion Automatica";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            Tipo_Contabilizacion = DB.Contabilizacion_Automatica_Validar_Agente_Contabiliza_House(_paisID, _sisID, _ttoID, Bean_Grupo_Agentes.intC1);
            if (Tipo_Contabilizacion == "-100")
            {
                alertaID = 10;
                mensaje = "Error al momento de Generar Contabilizacion Automatica";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            #endregion
            #region 2. Obtener_Costos
            ArrayList Arr_Costos = Contabilizacion_Automatica_CAD.Get_CostosBL_X_Traficos(Pais_Bean, _sisID, _ttoID, _blID, Tipo_Contabilizacion);
            if (Arr_Costos == null)
            {
                alertaID = 13;
                mensaje = "Existio un error al Tratar de Obtener los Costos";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            if (Arr_Costos.Count == 0)
            {
                alertaID = 2;
                mensaje = "Documento sin Costos";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            #endregion
            #region 3. Determinar Transacciones Asociadas al Caso
            Arr_Transacciones_Asociadas = Obtener_Transacciones_Asociadas(casoID, _paisID, _sisID, _ttoID);
            if (Arr_Transacciones_Asociadas.Count == 0)
            {
                string Sistema = "";
                string Tipo_Operacion = "";
                ArrayList Arr_Sistemas = DB.Get_Detalle_Sistemas(" and b.tto_id=" + _ttoID + "");
                foreach (RE_GenericBean Bean in Arr_Sistemas)
                {
                    Sistema = Bean.strC1;
                    Tipo_Operacion = Bean.strC2;
                }
                alertaID = 12;
                mensaje = "No existen Transacciones Asociadas para Contabilizar en: " + Pais_Bean.Nombre + ", " + Sistema + " " + Tipo_Operacion + ", la Configuracion MASTER.: " + Traducir_Variables("MBL", _MBL) + ", HOUSE.: " + Traducir_Variables("HBL", _HBL) + ", " + Traducir_Variables("ROUTING", _Routing) + ", " + Traducir_Variables("IMPORT_EXPORT", _Imp_Exp) + " .";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            #endregion
            #region 4. Verificar que cada Costo Tenga una Transaccion Asociada
            bool _match = false;
            foreach (Bean_Costos Bean_Costos in Arr_Costos)
            {
                _match = false;
                foreach (RE_GenericBean Bean_Transaccion in Arr_Transacciones_Asociadas)
                {
                    #region Validar Transaccion Asociada
                    if (DB.Traducir_Tipo_Persona_MasterToBAW(Bean_Costos.Costo_Tipo_Proveedor_ID.ToString()) == Bean_Transaccion.intC5)
                    {
                        #region Validar Tipo de Servicio Terceros
                        if (Bean_Transaccion.boolC1 == true)
                        {
                            if (Bean_Costos.Costo_Servicio_ID == 14)
                            {
                                _match = true;
                            }
                            else
                            {
                                _match = false;
                                alertaID = 14;
                                mensaje += "Error en la Clasificacion del Tipo de Servicio del Rubro.: " + DB.Traducir_Rubro_STR(Bean_Costos.Costo_Rub_ID) + " Monto.: " + Bean_Costos.Costo_Monto.ToString() + " Tipo Persona.: " + DB.Traducir_Tipo_Persona_STR(DB.Traducir_Tipo_Persona_MasterToBAW(Bean_Costos.Costo_Tipo_Proveedor_ID.ToString())) + " Nombre.: " + Bean_Costos.Costo_Nombre_Proveedor + ", Debe utilizar Tipo de Servicio Terceros.";
                            }
                        }
                        else if (Bean_Transaccion.boolC1 == false)
                        {
                            if (Bean_Costos.Costo_Servicio_ID == 14)
                            {
                                _match = false;
                                alertaID = 14;
                                mensaje += "Error en la Clasificacion del Tipo de Servicio del Rubro.: " + DB.Traducir_Rubro_STR(Bean_Costos.Costo_Rub_ID) + " Monto.: " + Bean_Costos.Costo_Monto.ToString() + " Tipo Persona.: " + DB.Traducir_Tipo_Persona_STR(DB.Traducir_Tipo_Persona_MasterToBAW(Bean_Costos.Costo_Tipo_Proveedor_ID.ToString())) + " Nombre.: " + Bean_Costos.Costo_Nombre_Proveedor + ", Debe utilizar Tipo de Servicio Terceros.";
                            }
                            else
                            {
                                _match = true;
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
                if (_match == false)
                {
                    #region Validar Transaccion Default
                    if ((DB.Traducir_Tipo_Persona_MasterToBAW(Bean_Costos.Costo_Tipo_Proveedor_ID.ToString()) == 4) || (DB.Traducir_Tipo_Persona_MasterToBAW(Bean_Costos.Costo_Tipo_Proveedor_ID.ToString()) == 6))
                    {
                        _match = true;
                    }
                    #endregion
                }
                if (_match == false)
                {
                    #region Validar Costos sin Transaccion Asociada
                    cantidad_costos_sin_transaccion_asociada++;
                    mensaje += "Transaccion No Definida para el Sistema Rubro.: " + DB.Traducir_Rubro_STR(Bean_Costos.Costo_Rub_ID) + " Monto.: " + Bean_Costos.Costo_Monto.ToString() + " Tipo Persona.: " + DB.Traducir_Tipo_Persona_STR(DB.Traducir_Tipo_Persona_MasterToBAW(Bean_Costos.Costo_Tipo_Proveedor_ID.ToString())) + " Nombre.: " + Bean_Costos.Costo_Nombre_Proveedor + ", Debe utilizar Tipo de Servicio Terceros.";
                    #endregion
                }
            }
            if (cantidad_costos_sin_transaccion_asociada > 0)
            {
                #region Detener Contabilizacion y Retornar Mensaje
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
                #endregion
            }
            #endregion
            #region 5. Generar Nodos
            NDContabilizacion_Automatica.AppendChild(Crear_Nodo_Caso(XMLContabilizacion_Automatica, casoID, _MBL, _HBL, _Routing, _Imp_Exp));
            NDContabilizacion_Automatica.AppendChild(Crear_Nodo_Configuracion(XMLContabilizacion_Automatica, _paisID, _sisID, _ttoID, _blID, _usuID));
            NDContabilizacion_Automatica.AppendChild(Crear_Nodo_Costos(XMLContabilizacion_Automatica, Arr_Costos));
            NDContabilizacion_Automatica.AppendChild(Crear_Nodo_Transacciones(XMLContabilizacion_Automatica, Arr_Transacciones_Asociadas));
            XMLContabilizacion_Automatica.AppendChild(NDContabilizacion_Automatica);
            #endregion
            #region 6. Asignar Costos a su respectiva Transaccion
            foreach (RE_GenericBean Bean_Transaccion in Arr_Transacciones_Asociadas)
            {
                foreach (Bean_Costos Bean_Costos in Arr_Costos)
                {
                    if (DB.Traducir_Tipo_Persona_MasterToBAW(Bean_Costos.Costo_Tipo_Proveedor_ID.ToString()) == Bean_Transaccion.intC5)
                    {
                        XMLContabilizacion_Automatica = Asignar_Costos_Transaccion(XMLContabilizacion_Automatica, Bean_Transaccion, Bean_Costos);
                    }
                }
            }
            #endregion
            #region 7. Obtener y Asignar Detalle de Transacciones
            XMLContabilizacion_Automatica = Asignar_Detalle_Transaccion(XMLContabilizacion_Automatica);
            if (XMLContabilizacion_Automatica == null)
            {
                alertaID = 16;
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            #endregion
            #region 8. Liberar Arreglos
            Arr_Costos = null;
            Arr_Transacciones_Asociadas = null;
            #endregion
            #region 9. Convertir Costos a Moneda Destino
            XMLContabilizacion_Automatica = Convertir_Costos_Moneda_Destino(XMLContabilizacion_Automatica, Pais_Bean);
            #endregion
            #region 10. Generar Contabilizacion Automatica
            foreach (XmlNode NDNivel1 in XMLContabilizacion_Automatica.ChildNodes)
            {
                if (NDNivel1.Name == "Contabilizacion_Automatica")
                {
                    foreach (XmlNode NDNivel2 in NDNivel1.ChildNodes)
                    {
                        if (NDNivel2.Name == "Transacciones")
                        {
                            foreach (XmlNode NDNivel3 in NDNivel2.ChildNodes)
                            {
                                if (NDNivel3.Name == "Transaccion")
                                {
                                    foreach (XmlNode NDNivel4 in NDNivel3.ChildNodes)
                                    {
                                        if (NDNivel4.Name == "tcatr_ttr_id")
                                        {
                                            int ttrID = 0;
                                            ttrID = int.Parse(NDNivel4.InnerText);
                                            switch (ttrID)
                                            {
                                                case 5:
                                                    #region Generar Provision Automatica
                                                    #endregion
                                                    break;
                                                case 12:
                                                    #region Genera Nota de Credito a Proveedores Automatica
                                                    #endregion
                                                    break;
                                                case 25:
                                                    #region Generar Nota de Debito a Proveedores Automatica
                                                    #endregion
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion

        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            transaction.Rollback();
            return null;
        }
        return resultado;
    }
    public static bool Existe_Pais(string _paisID)
    {
        #region Validar que exista el Pais
        bool resultado = false;
        resultado = Contabilizacion_Automatica_CAD.Existe_Pais(_paisID.ToString());
        return resultado;
        #endregion
    }
    public static bool Existe_Tipo_Cambio(string _paisID)
    {
        #region Validar que exista Tipo de Cambio
        bool resultado = false;
        resultado = Contabilizacion_Automatica_CAD.Existe_Tipo_Cambio(_paisID.ToString());
        return resultado;
        #endregion
    }
    private static string Traducir_Variables(string variable, bool valor)
    {
        #region Traducir_Variables
        string resultado = "";
        if ((variable.ToUpper() == "MBL") || (variable.ToUpper() == "HBL"))
        {
            if (valor == true)
            {
                resultado = "Prepaid";
            }
            else
            {
                resultado = "Collect";
            }
        }
        if (variable.ToUpper() == "ROUTING")
        {
            if (valor == true)
            {
                resultado = "CARGA RUTEADA";
            }
            else
            {
                resultado = "CARGA NO RUTEADA";
            }
        }
        if (variable.ToUpper() == "IMPORT_EXPORT")
        {
            if (valor == true)
            {
                resultado = "Importacion";
            }
            else
            {
                resultado = "Exportacion";
            }
        }
        return resultado;
        #endregion
    }
    public static int Determinar_Caso(bool _MBL, bool _HBL, bool _Routing, bool _Imp_Exp)
    {
        #region Determinar_Caso
        int casoID = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select tcaca_id from tbl_contabilizacion_automatica_casos where tcaca_tipo_mbl=" + _MBL + " and tcaca_tipo_hbl=" + _HBL + " and tcaca_ruteado=" + _Routing + " and tcaca_import_export=" + _Imp_Exp + " and tcaca_estado=1";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                casoID = int.Parse(reader.GetValue(0).ToString());
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return casoID;
        #endregion
    }
    private static ArrayList Obtener_Transacciones_Asociadas(int casoID, int _paisID, int _sisID, int _ttoID)
    {
        #region Obtener_Transacciones_Asociadas
        ArrayList Arr = new ArrayList();
        RE_GenericBean Bean = new RE_GenericBean();
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select a.tca_id, b.tcaco_id, c.tcatr_id, c.tcatr_ttr_id, c.tcatr_tpi_origen_id, c.tcatr_tpi_destino_id, a.tca_tcatt_id, a.tca_tcato_id, b.tcaco_terceros " +
            "from tbl_contabilizacion_automatica a, tbl_contabilizacion_automatica_configuracion b, tbl_contabilizacion_automatica_transacciones c " +
            "where a.tca_id=b.tcaco_tca_id and a.tca_tcatr_id=c.tcatr_id " +
            "and a.tca_estado=1 and b.tcaco_estado=1  and c.tcatr_estado=1 " +
            "and a.tca_tcaca_id=" + casoID + " " +
            "and b.tcaco_pai_id=" + _paisID + "" +
            "and b.tcaco_sis_id=" + _sisID + "" +
            "and b.tcaco_tto_id=" + _ttoID + "" +
            "and b.tcaco_automatizar=true ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.intC1 = int.Parse(reader.GetValue(0).ToString());//tca_id
                Bean.intC2 = int.Parse(reader.GetValue(1).ToString());//tcaco_id
                Bean.intC3 = int.Parse(reader.GetValue(2).ToString());//tcatr_id
                Bean.intC4 = int.Parse(reader.GetValue(3).ToString());//tcatr_ttr_id
                Bean.intC5 = int.Parse(reader.GetValue(4).ToString());//tcatr_tpi_origen_id
                Bean.intC6 = int.Parse(reader.GetValue(5).ToString());//tcatr_tpi_destino_id
                Bean.intC7 = int.Parse(reader.GetValue(6).ToString());//tca_tcatt_id
                Bean.intC8 = int.Parse(reader.GetValue(7).ToString());//tca_tcato_id
                Bean.boolC1 = bool.Parse(reader.GetValue(8).ToString());//tcaco_terceros
                Arr.Add(Bean);
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Arr;
        #endregion
    }
    private static ArrayList Obtener_Detalle_Configuracion_Transaccion_Asociada(int tcacoID)
    {
        #region Obtener Detalles por Transaccion Asociada
        ArrayList Arr = new ArrayList();
        RE_GenericBean Bean = new RE_GenericBean();
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select tcad_id, tcad_contabilidad_destino_id, tcad_moneda_origen_id, tcad_moneda_destino_id, tcad_suc_id, tcad_operacion_id, tcad_serie, tcad_genera_partida " +
            "from tbl_contabilizacion_automatica_detalle where tcad_tcaco_id=" + tcacoID + " and tcad_estado=1 ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.intC1 = int.Parse(reader.GetValue(0).ToString());//tcad_id
                Bean.intC2 = int.Parse(reader.GetValue(1).ToString());//tcad_contabilidad_destino_id
                Bean.intC3 = int.Parse(reader.GetValue(2).ToString());//tcad_moneda_origen_id
                Bean.intC4 = int.Parse(reader.GetValue(3).ToString());//tcad_moneda_destino_id
                Bean.intC5 = int.Parse(reader.GetValue(4).ToString());//tcad_suc_id
                Bean.intC6 = int.Parse(reader.GetValue(5).ToString());//tcad_operacion_id
                Bean.strC1 = reader.GetValue(6).ToString();//tcad_serie
                Bean.boolC1 = bool.Parse(reader.GetValue(7).ToString());//tcad_genera_partida
                Arr.Add(Bean);
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Arr;
        #endregion
    }
    private static XmlElement Crear_Nodo_Caso(XmlDocument XMLContabilizacion_Automatica, int casoID, bool _MBL, bool _HBL, bool _Routing, bool _Imp_Exp)
    {
        #region Crear Nodo Caso
        XmlElement NDCaso = null;
        XmlElement ND_Hijo = null;
        NDCaso = XMLContabilizacion_Automatica.CreateElement("Caso");
        ND_Hijo = XMLContabilizacion_Automatica.CreateElement("tcaca_id");
        ND_Hijo.InnerText = casoID.ToString();
        NDCaso.AppendChild(ND_Hijo);
        ND_Hijo = null;
        ND_Hijo = XMLContabilizacion_Automatica.CreateElement("tcaca_tipo_mbl");
        ND_Hijo.InnerText = _MBL.ToString();
        NDCaso.AppendChild(ND_Hijo);
        ND_Hijo = null;
        ND_Hijo = XMLContabilizacion_Automatica.CreateElement("tcaca_tipo_hbl");
        ND_Hijo.InnerText = _HBL.ToString();
        NDCaso.AppendChild(ND_Hijo);
        ND_Hijo = null;
        ND_Hijo = XMLContabilizacion_Automatica.CreateElement("tcaca_ruteado");
        ND_Hijo.InnerText = _Routing.ToString();
        NDCaso.AppendChild(ND_Hijo);
        ND_Hijo = null;
        ND_Hijo = XMLContabilizacion_Automatica.CreateElement("tcaca_import_export");
        ND_Hijo.InnerText = _Imp_Exp.ToString();
        NDCaso.AppendChild(ND_Hijo);
        ND_Hijo = null;
        return NDCaso;
        #endregion
    }
    private static XmlElement Crear_Nodo_Configuracion(XmlDocument XMLContabilizacion_Automatica, int _paisID, int _sisID, int _ttoID, int _blID, string _usuID)
    {
        #region Crear Nodo Configuracion
        XmlElement NDConfiguracion = null;
        XmlElement ND_Hijo = null;
        NDConfiguracion = XMLContabilizacion_Automatica.CreateElement("Configuracion");
        ND_Hijo = XMLContabilizacion_Automatica.CreateElement("tcaco_pai_id");
        ND_Hijo.InnerText = _paisID.ToString();
        NDConfiguracion.AppendChild(ND_Hijo);
        ND_Hijo = null;
        ND_Hijo = XMLContabilizacion_Automatica.CreateElement("tcaco_sis_id");
        ND_Hijo.InnerText = _sisID.ToString();
        NDConfiguracion.AppendChild(ND_Hijo);
        ND_Hijo = null;
        ND_Hijo = XMLContabilizacion_Automatica.CreateElement("tcaco_tto_id");
        ND_Hijo.InnerText = _ttoID.ToString();
        NDConfiguracion.AppendChild(ND_Hijo);
        ND_Hijo = null;
        ND_Hijo = XMLContabilizacion_Automatica.CreateElement("tcaco_bl_id");
        ND_Hijo.InnerText = _blID.ToString();
        NDConfiguracion.AppendChild(ND_Hijo);
        ND_Hijo = null;
        ND_Hijo = XMLContabilizacion_Automatica.CreateElement("tcaco_usu_id");
        ND_Hijo.InnerText = _usuID.ToString();
        NDConfiguracion.AppendChild(ND_Hijo);
        ND_Hijo = null;
        ND_Hijo = XMLContabilizacion_Automatica.CreateElement("tcaco_tipo_contabilizacion");
        ND_Hijo.InnerText = Tipo_Contabilizacion;
        NDConfiguracion.AppendChild(ND_Hijo);
        ND_Hijo = null;
        return NDConfiguracion;
        #endregion
    }
    private static XmlElement Crear_Nodo_Costos(XmlDocument XMLContabilizacion_Automatica, ArrayList Arr_Costos)
    {
        #region Crear Nodo Costos
        XmlElement NDCostos = null;
        XmlElement SNDCosto = null;
        XmlElement ND_Hijo = null;
        NDCostos = XMLContabilizacion_Automatica.CreateElement("Costos");
        foreach (Bean_Costos Bean_Costos in Arr_Costos)
        {
            SNDCosto = XMLContabilizacion_Automatica.CreateElement("Costo");
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_ID");
            ND_Hijo.InnerText = Bean_Costos.Costo_ID.ToString();
            SNDCosto.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Tipo_Proveedor_ID");
            ND_Hijo.InnerText = Bean_Costos.Costo_Tipo_Proveedor_ID.ToString();
            SNDCosto.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Proveedor_ID");
            ND_Hijo.InnerText = Bean_Costos.Costo_Proveedor_ID.ToString();
            SNDCosto.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Rub_ID");
            ND_Hijo.InnerText = Bean_Costos.Costo_Rub_ID.ToString();
            SNDCosto.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Moneda_ID");
            ND_Hijo.InnerText = Bean_Costos.Costo_Moneda_ID.ToString();
            SNDCosto.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Moneda");
            ND_Hijo.InnerText = Bean_Costos.Costo_Moneda.ToString();
            SNDCosto.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Monto");
            ND_Hijo.InnerText = Bean_Costos.Costo_Monto.ToString();
            SNDCosto.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Orden_Compra");
            ND_Hijo.InnerText = Bean_Costos.Costo_Orden_Compra.ToString();
            SNDCosto.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Referencia");
            ND_Hijo.InnerText = Bean_Costos.Costo_Referencia.ToString();
            SNDCosto.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Provisionado");
            ND_Hijo.InnerText = Bean_Costos.Costo_Provisionado.ToString();
            SNDCosto.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Nombre_Proveedor");
            ND_Hijo.InnerText = Bean_Costos.Costo_Nombre_Proveedor;
            SNDCosto.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Servicio_ID");
            ND_Hijo.InnerText = Bean_Costos.Costo_Servicio_ID.ToString();
            SNDCosto.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Pago_Terceros");
            ND_Hijo.InnerText = Bean_Costos.Costo_Pago_Terceros.ToString();
            SNDCosto.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Es_Afecto");
            ND_Hijo.InnerText = Bean_Costos.Costo_Es_Afecto.ToString();
            SNDCosto.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Master");
            ND_Hijo.InnerText = Bean_Costos.Costo_Documento_Master;
            SNDCosto.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_House");
            ND_Hijo.InnerText = Bean_Costos.Costo_Documento_House;
            SNDCosto.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Routing");
            ND_Hijo.InnerText = Bean_Costos.Costo_Routing;
            SNDCosto.AppendChild(ND_Hijo);
            ND_Hijo = null;
            NDCostos.AppendChild(SNDCosto);
        }
        return NDCostos;
        #endregion
    }
    private static XmlElement Crear_Nodo_Transacciones(XmlDocument XMLContabilizacion_Automatica, ArrayList Arr_Transacciones)
    {
        #region Crear Nodo Costos
        XmlElement NDTransacciones = null;
        XmlElement SNDTransaccion = null;
        XmlElement ND_Hijo = null;
        NDTransacciones = XMLContabilizacion_Automatica.CreateElement("Transacciones");
        foreach (RE_GenericBean Bean_Transaccion in Arr_Transacciones)
        {
            SNDTransaccion = XMLContabilizacion_Automatica.CreateElement("Transaccion");
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("tca_id");
            ND_Hijo.InnerText = Bean_Transaccion.intC1.ToString();
            SNDTransaccion.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("tcaco_id");
            ND_Hijo.InnerText = Bean_Transaccion.intC2.ToString();
            SNDTransaccion.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("tcatr_id");
            ND_Hijo.InnerText = Bean_Transaccion.intC3.ToString();
            SNDTransaccion.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("tcatr_ttr_id");
            ND_Hijo.InnerText = Bean_Transaccion.intC4.ToString();
            SNDTransaccion.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("tcatr_tpi_origen_id");
            ND_Hijo.InnerText = Bean_Transaccion.intC5.ToString();
            SNDTransaccion.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("tcatr_tpi_destino_id");
            ND_Hijo.InnerText = Bean_Transaccion.intC6.ToString();
            SNDTransaccion.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("tca_tcatt_id");
            ND_Hijo.InnerText = Bean_Transaccion.intC7.ToString();
            SNDTransaccion.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("tca_tcato_id");
            ND_Hijo.InnerText = Bean_Transaccion.intC8.ToString();
            SNDTransaccion.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("tcaco_terceros");
            ND_Hijo.InnerText = Bean_Transaccion.boolC1.ToString();
            SNDTransaccion.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("CostosAsignados");
            SNDTransaccion.AppendChild(ND_Hijo);
            ND_Hijo = null;
            ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Configuraciones");
            SNDTransaccion.AppendChild(ND_Hijo);
            ND_Hijo = null;
            NDTransacciones.AppendChild(SNDTransaccion);
        }
        return NDTransacciones;
        #endregion
    }
    private static XmlDocument Asignar_Costos_Transaccion(XmlDocument XMLContabilizacion_Automatica, RE_GenericBean Bean_Transaccion, Bean_Costos Bean_Costos)
    {
        #region Asignar Costos a Cada Transaccion
        foreach (XmlNode NDNivel1 in XMLContabilizacion_Automatica.ChildNodes)
        {
            if (NDNivel1.Name == "Contabilizacion_Automatica")
            {
                foreach (XmlNode NDNivel2 in NDNivel1.ChildNodes)
                {
                    if (NDNivel2.Name == "Transacciones")
                    {
                        foreach (XmlNode NDNivel3 in NDNivel2.ChildNodes)
                        {
                            if (NDNivel3.Name == "Transaccion")
                            {
                                foreach (XmlNode NDNivel4 in NDNivel3.ChildNodes)
                                {
                                    if (NDNivel4.Name == "tcatr_id")
                                    {
                                        if (NDNivel4.InnerText == Bean_Transaccion.intC3.ToString())
                                        {
                                            foreach (XmlNode NDTemp in NDNivel3.ChildNodes)
                                            {
                                                if (NDTemp.Name == "CostosAsignados")
                                                {
                                                    XmlElement NDCosto = XMLContabilizacion_Automatica.CreateElement("Costo");

                                                    XmlElement ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_ID");
                                                    ND_Hijo.InnerText = Bean_Costos.Costo_ID.ToString();
                                                    NDCosto.AppendChild(ND_Hijo);
                                                    ND_Hijo = null;
                                                    ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Tipo_Proveedor_ID");
                                                    ND_Hijo.InnerText = Bean_Costos.Costo_Tipo_Proveedor_ID.ToString();
                                                    NDCosto.AppendChild(ND_Hijo);
                                                    ND_Hijo = null;
                                                    ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Proveedor_ID");
                                                    ND_Hijo.InnerText = Bean_Costos.Costo_Proveedor_ID.ToString();
                                                    NDCosto.AppendChild(ND_Hijo);
                                                    ND_Hijo = null;
                                                    ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Rub_ID");
                                                    ND_Hijo.InnerText = Bean_Costos.Costo_Rub_ID.ToString();
                                                    NDCosto.AppendChild(ND_Hijo);
                                                    ND_Hijo = null;
                                                    ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Moneda_ID");
                                                    ND_Hijo.InnerText = DB.Traducir_Moneda_MASTER_TO_BAW(Bean_Costos.Costo_Moneda).ToString();
                                                    NDCosto.AppendChild(ND_Hijo);
                                                    ND_Hijo = null;
                                                    ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Monto");
                                                    ND_Hijo.InnerText = Bean_Costos.Costo_Monto.ToString();
                                                    NDCosto.AppendChild(ND_Hijo);
                                                    ND_Hijo = null;
                                                    ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Orden_Compra");
                                                    ND_Hijo.InnerText = Bean_Costos.Costo_Orden_Compra.ToString();
                                                    NDCosto.AppendChild(ND_Hijo);
                                                    ND_Hijo = null;
                                                    ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Referencia");
                                                    ND_Hijo.InnerText = Bean_Costos.Costo_Referencia.ToString();
                                                    NDCosto.AppendChild(ND_Hijo);
                                                    ND_Hijo = null;
                                                    ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Provisionado");
                                                    ND_Hijo.InnerText = Bean_Costos.Costo_Provisionado.ToString();
                                                    NDCosto.AppendChild(ND_Hijo);
                                                    ND_Hijo = null;
                                                    ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Nombre_Proveedor");
                                                    ND_Hijo.InnerText = Bean_Costos.Costo_Nombre_Proveedor;
                                                    NDCosto.AppendChild(ND_Hijo);
                                                    ND_Hijo = null;
                                                    ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Servicio_ID");
                                                    ND_Hijo.InnerText = Bean_Costos.Costo_Servicio_ID.ToString();
                                                    NDCosto.AppendChild(ND_Hijo);
                                                    ND_Hijo = null;
                                                    ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Pago_Terceros");
                                                    ND_Hijo.InnerText = Bean_Costos.Costo_Pago_Terceros.ToString();
                                                    NDCosto.AppendChild(ND_Hijo);
                                                    ND_Hijo = null;
                                                    ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Es_Afecto");
                                                    ND_Hijo.InnerText = Bean_Costos.Costo_Es_Afecto.ToString();
                                                    NDCosto.AppendChild(ND_Hijo);
                                                    ND_Hijo = null;
                                                    ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Master");
                                                    ND_Hijo.InnerText = Bean_Costos.Costo_Documento_Master; ;
                                                    NDCosto.AppendChild(ND_Hijo);
                                                    ND_Hijo = null;
                                                    ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_House");
                                                    ND_Hijo.InnerText = Bean_Costos.Costo_Documento_House; ;
                                                    NDCosto.AppendChild(ND_Hijo);
                                                    ND_Hijo = null;
                                                    ND_Hijo = XMLContabilizacion_Automatica.CreateElement("Costo_Routing");
                                                    ND_Hijo.InnerText = Bean_Costos.Costo_Routing; ;
                                                    NDCosto.AppendChild(ND_Hijo);
                                                    ND_Hijo = null;

                                                    NDTemp.AppendChild(NDCosto);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return XMLContabilizacion_Automatica;
        #endregion
    }
    private static XmlDocument Asignar_Detalle_Transaccion(XmlDocument XMLContabilizacion_Automatica)
    {
        #region Asignar Detalle Configuracion a cada Transaccion
        XmlNode NDTCACO_ID = null;
        foreach (XmlNode NDNivel1 in XMLContabilizacion_Automatica.ChildNodes)
        {
            if (NDNivel1.Name == "Contabilizacion_Automatica")
            {
                foreach (XmlNode NDNivel2 in NDNivel1.ChildNodes)
                {
                    if (NDNivel2.Name == "Transacciones")
                    {
                        foreach (XmlNode NDNivel3 in NDNivel2.ChildNodes)
                        {
                            if (NDNivel3.Name == "Transaccion")
                            {
                                foreach (XmlNode NDNivel4 in NDNivel3.ChildNodes)
                                {
                                    if (NDNivel4.Name == "tcaco_id")
                                    {
                                        NDTCACO_ID = NDNivel4;
                                        foreach (XmlNode NDTemp in NDNivel3.ChildNodes)
                                        {
                                            if (NDTemp.Name == "Configuraciones")
                                            {
                                                UsuarioBean user_temp = null;
                                                ArrayList Arr_Detalle_Transacciones = (ArrayList)DB.Contabilizacion_Automatica_Get_Detalles_Configuracion(user_temp, int.Parse(NDTCACO_ID.InnerText));
                                                if ((Arr_Detalle_Transacciones == null) || (Arr_Detalle_Transacciones.Count == 0))
                                                {
                                                    mensaje += "(" + NDTCACO_ID.InnerText + ") Transaccion sin Configuracion";
                                                    XMLContabilizacion_Automatica = null;
                                                    return XMLContabilizacion_Automatica;
                                                }
                                                foreach (RE_GenericBean Bean_Detalle in Arr_Detalle_Transacciones)
                                                {
                                                    XmlElement SNConfiguracionBAW = XMLContabilizacion_Automatica.CreateElement("ConfiguracionBAW");
                                                    XmlElement SNDHijo = XMLContabilizacion_Automatica.CreateElement("tcad_id");
                                                    SNDHijo.InnerText = Bean_Detalle.intC1.ToString();
                                                    SNConfiguracionBAW.AppendChild(SNDHijo);
                                                    SNDHijo = null;
                                                    SNDHijo = XMLContabilizacion_Automatica.CreateElement("tcad_pai_id");
                                                    SNDHijo.InnerText = Bean_Detalle.intC3.ToString();
                                                    SNConfiguracionBAW.AppendChild(SNDHijo);
                                                    SNDHijo = null;
                                                    SNDHijo = XMLContabilizacion_Automatica.CreateElement("tcad_contabilidad_destino_id");
                                                    SNDHijo.InnerText = Bean_Detalle.intC4.ToString();
                                                    SNConfiguracionBAW.AppendChild(SNDHijo);
                                                    SNDHijo = null;
                                                    SNDHijo = XMLContabilizacion_Automatica.CreateElement("tcad_moneda_origen_id");
                                                    SNDHijo.InnerText = Bean_Detalle.intC5.ToString();
                                                    SNConfiguracionBAW.AppendChild(SNDHijo);
                                                    SNDHijo = null;
                                                    SNDHijo = XMLContabilizacion_Automatica.CreateElement("tcad_moneda_destino_id");
                                                    SNDHijo.InnerText = Bean_Detalle.intC6.ToString();
                                                    SNConfiguracionBAW.AppendChild(SNDHijo);
                                                    SNDHijo = null;
                                                    SNDHijo = XMLContabilizacion_Automatica.CreateElement("tcad_suc_id");
                                                    SNDHijo.InnerText = Bean_Detalle.intC7.ToString();
                                                    SNConfiguracionBAW.AppendChild(SNDHijo);
                                                    SNDHijo = null;
                                                    SNDHijo = XMLContabilizacion_Automatica.CreateElement("tcad_operacion_id");
                                                    SNDHijo.InnerText = Bean_Detalle.intC2.ToString();
                                                    SNConfiguracionBAW.AppendChild(SNDHijo);
                                                    SNDHijo = null;
                                                    SNDHijo = XMLContabilizacion_Automatica.CreateElement("tcad_serie");
                                                    SNDHijo.InnerText = Bean_Detalle.strC6;
                                                    SNConfiguracionBAW.AppendChild(SNDHijo);
                                                    SNDHijo = null;
                                                    SNDHijo = XMLContabilizacion_Automatica.CreateElement("tcad_genera_partida");
                                                    SNDHijo.InnerText = Bean_Detalle.boolC1.ToString();
                                                    SNConfiguracionBAW.AppendChild(SNDHijo);
                                                    SNDHijo = null;
                                                    NDTemp.AppendChild(SNConfiguracionBAW);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return XMLContabilizacion_Automatica;
        #endregion
    }
    private static XmlDocument Convertir_Costos_Moneda_Destino(XmlDocument XMLContabilizacion_Automatica, PaisBean _Pais_Bean)
    {
        #region Convertir los Costos Asociados a Cada Transaccion a la Moneda Destino
        int monedaID_Origen_Costo = 0;
        int monedaID_Origen = 0;
        int monedaID_Destino = 0;
        double monto_Origen_Costo = 0;
        double monto_Destino = 0;
        foreach (XmlNode NDNivel1 in XMLContabilizacion_Automatica.ChildNodes)
        {
            if (NDNivel1.Name == "Contabilizacion_Automatica")
            {
                foreach (XmlNode NDNivel2 in NDNivel1.ChildNodes)
                {
                    if (NDNivel2.Name == "Transacciones")
                    {
                        foreach (XmlNode NDNivel3 in NDNivel2.ChildNodes)
                        {
                            if (NDNivel3.Name == "Transaccion")
                            {
                                foreach (XmlNode NDNivel4 in NDNivel3.ChildNodes)
                                {
                                    if (NDNivel4.Name == "CostosAsignados")
                                    {
                                        foreach (XmlNode NDNivel5 in NDNivel4.ChildNodes)
                                        {
                                            if (NDNivel5.Name == "Costo")
                                            {
                                                #region Capturar Datos de Origen
                                                foreach (XmlNode NDNivel6 in NDNivel5)
                                                {
                                                    if (NDNivel6.Name == "Costo_Moneda_ID")
                                                    {
                                                        monedaID_Origen_Costo = int.Parse(NDNivel6.InnerText);
                                                    }
                                                    if (NDNivel6.Name == "Costo_Monto")
                                                    {
                                                        monto_Origen_Costo = double.Parse(NDNivel6.InnerText);
                                                    }
                                                }
                                                #endregion
                                                #region Evaluar Configuracion
                                                int ban_monedas = 0;
                                                foreach (XmlNode NDTEMPNivel4 in NDNivel3.ChildNodes)
                                                {
                                                    if (NDTEMPNivel4.Name == "Configuraciones")
                                                    {
                                                        foreach (XmlNode NDTEMPNivel5 in NDTEMPNivel4)
                                                        {
                                                            if (NDTEMPNivel5.Name == "ConfiguracionBAW")
                                                            {
                                                                #region Capturar Configuracion Moneda Destino
                                                                foreach (XmlNode NDTEMPNivel6 in NDTEMPNivel5)
                                                                {
                                                                    if (NDTEMPNivel6.Name == "tcad_moneda_origen_id")
                                                                    {
                                                                        monedaID_Origen = int.Parse(NDTEMPNivel6.InnerText);
                                                                    }
                                                                    if (NDTEMPNivel6.Name == "tcad_moneda_destino_id")
                                                                    {
                                                                        monedaID_Destino = int.Parse(NDTEMPNivel6.InnerText);
                                                                    }
                                                                }
                                                                #endregion
                                                                #region Convertir a Moneda Destino
                                                                if (monedaID_Origen_Costo == monedaID_Origen)
                                                                {
                                                                    if (monedaID_Origen_Costo == monedaID_Destino)
                                                                    {
                                                                        ban_monedas++;
                                                                        monto_Destino = monto_Origen_Costo;
                                                                    }
                                                                    else if (monedaID_Origen_Costo != monedaID_Destino)
                                                                    {
                                                                        ban_monedas++;
                                                                        if (monedaID_Destino == 8)
                                                                        {
                                                                            monto_Destino = Math.Round(monto_Origen_Costo / (double)_Pais_Bean.TipoCambio, 2);
                                                                        }
                                                                        else
                                                                        {
                                                                            monto_Destino = Math.Round(monto_Origen_Costo * (double)_Pais_Bean.TipoCambio, 2);
                                                                        }
                                                                    }
                                                                }
                                                                #endregion
                                                            }
                                                        }
                                                    }
                                                }
                                                #region No Existe Configuracion
                                                if (ban_monedas == 0)
                                                {
                                                    mensaje += "Transaccion sin Configuracion de Monedas";
                                                    XMLContabilizacion_Automatica = null;
                                                    return XMLContabilizacion_Automatica;
                                                }
                                                #endregion
                                                #endregion
                                                #region Asignar Monto y Moneda Convertida
                                                foreach (XmlNode SNDNivel6 in NDNivel5)
                                                {
                                                    if (SNDNivel6.Name == "Costo_Moneda_ID")
                                                    {
                                                        SNDNivel6.InnerText = monedaID_Destino.ToString();
                                                    }
                                                    if (SNDNivel6.Name == "Costo_Monto")
                                                    {
                                                        SNDNivel6.InnerText = monto_Destino.ToString();
                                                    }
                                                }
                                                #endregion
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return XMLContabilizacion_Automatica;
        #endregion
    }
    public static ArrayList Generar_Contabilizacion_Automatica_Intercompanys_Administrativo(NpgsqlConnection conn, NpgsqlCommand comm, NpgsqlTransaction Transaction, ArrayList Arr_Documento, int ttrID, UsuarioBean user)
    {
        #region Generar Contabilizacion Automatica Intercompanys Administrativo
        ArrayList Arr_result = null;
        Arr_result = Contabilizacion_Automatica_CAD.Construir_Provision_A_Partir_Otro_Documento(Arr_Documento, ttrID, user);
        if (Arr_result == null)
        {
            Arr_result = new ArrayList();
            Arr_result.Add("0");
            Arr_result.Add("Existio un error al Tratar de Construir el Documento");
            return Arr_result;
        }
        if (Arr_result[0].ToString() == "0")
        {
            return Arr_result;
        }
        Bean_Provision_Automatica Provision_Automatica = (Bean_Provision_Automatica)Arr_result[1];
        PaisBean Pais_Bean = DB.getPais(Provision_Automatica.tpr_pai_id);
        Arr_result = new ArrayList();
        Arr_result = Contabilizacion_Automatica_CAD.Insertar_Provision(conn, comm, Transaction, Pais_Bean, Provision_Automatica.tpr_tcon_id, Provision_Automatica.tpr_suc_id, Provision_Automatica);
        if (Arr_result == null)
        {
            Arr_result = new ArrayList();
            Arr_result.Add("0");
            Arr_result.Add("Existio un error al Insertar Provision Automatica");
            return Arr_result;
        }
        if (Arr_result[0].ToString() == "0")
        {
            return Arr_result;
        }
        #region Insertar Log Transacciones Encadenadas
        FacturaBean Factura = (FacturaBean)Arr_Documento[0];
        Provision_Automatica = (Bean_Provision_Automatica)Arr_result[1];
        RE_GenericBean Bean_Log = new RE_GenericBean();
        Bean_Log.intC1 = ttrID;//ttel_padre_ttr_id
        Bean_Log.intC2 = Factura.tfa_ID;//ttel_padre_ref_id
        Bean_Log.intC3 = 5;//ttel_hijo_ttr_id
        Bean_Log.intC4 = Provision_Automatica.tpr_prov_id;//ttel_hijo_ref_id
        Bean_Log.intC5 = 1;//ttel_tta_id
        Bean_Log.strC1 = user.ID;//ttel_usuario_emision
        Bean_Log.intC6 = user.PaisID;//ttel_padre_empresa_id
        Bean_Log.intC7 = Provision_Automatica.tpr_pai_id;//ttel_hijo_empresa_id
        int resutaldo_log_transacciones = 0;
        resutaldo_log_transacciones = Contabilizacion_Automatica_CAD.Insertar_Log_Transacciones_Encadenadas(Bean_Log);
        #endregion
        return Arr_result;
        #endregion
    }
    public static RE_GenericBean Obtener_Configuracion_Intercompany_Administrativo(int intercompanyID, int idpaisORIGEN, int idcontaORIGEN, int idmonedaORIGEN, int operacionID)
    {
        #region Obtener Configuracion de Intercompany Administrativo
        RE_GenericBean Bean = null;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select PAIS,a.tia_contabilidad_destino, a.tia_moneda_destino, a.tia_ttr_id_destino, a.tia_suc_id, a.tia_operacion_id, a.tia_moneda_impresion_destino " +
            "from tbl_intercompany_administrativo a inner join dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select id_intercompany, nombre_intercompany, id_empresa_baw from intercompanys where activo=true') " +
            "Intercompanys_Result(ID int, NOMBRE varchar, PAIS int) on (a.tia_id_intercompany=ID) " +
            "where a.tia_pais_origen=" + idpaisORIGEN + " and a.tia_contabilidad_origen=" + idcontaORIGEN + " and a.tia_moneda_origen=" + idmonedaORIGEN + " and a.tia_tipo_operacion=" + operacionID + " and a.tia_id_intercompany=" + intercompanyID + " and a.tia_estado=1 ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.intC1 = int.Parse(reader.GetValue(0).ToString());//Empresa del Intercompany
                Bean.intC2 = int.Parse(reader.GetValue(1).ToString());//tia_contabilidad_destino
                Bean.intC3 = int.Parse(reader.GetValue(2).ToString());//tia_moneda_destino
                Bean.intC4 = int.Parse(reader.GetValue(3).ToString());//tia_ttr_id_destino
                Bean.intC5 = int.Parse(reader.GetValue(4).ToString());//tia_suc_id
                Bean.intC6 = int.Parse(reader.GetValue(5).ToString());//tia_operacion_id
                Bean.intC7 = int.Parse(reader.GetValue(6).ToString());//tia_moneda_impresion_destino
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Bean;
        #endregion
    }
    public static double Convertir_Divisas_Intercompanys(int idpaisORIGEN, int idmonedaORIGEN, double monto_original, int idpaisDESTINO, int idmonedaDESTINO)
    {
        #region Convertir Divisas en Base a la configuracion del Intercompany
        double monto_convertido = 0;
        decimal Tipo_Cambio_Origen = 0;
        decimal Tipo_Cambio_Destino = 0;
        Tipo_Cambio_Origen = DB.getTipoCambioHoy(idpaisORIGEN);
        Tipo_Cambio_Destino = DB.getTipoCambioHoy(idpaisDESTINO);
        if (idmonedaORIGEN == idmonedaDESTINO)
        {
            monto_convertido = monto_original;
        }
        else
        {
            if ((idmonedaORIGEN == 8) && (idmonedaDESTINO != 8))
            {
                monto_convertido = Math.Round(monto_original * (double)Tipo_Cambio_Destino, 2);
            }
            else if ((idmonedaORIGEN != 8) && (idmonedaDESTINO == 8))
            {
                monto_convertido = Math.Round(monto_original / (double)Tipo_Cambio_Origen, 2);
            }
            else if ((idmonedaORIGEN != 8) && (idmonedaDESTINO != 8))
            {
                monto_convertido = Math.Round(monto_original / (double)Tipo_Cambio_Origen, 2);
                monto_convertido = Math.Round(monto_convertido * (double)Tipo_Cambio_Destino, 2);
            }
        }
        return monto_convertido;
        #endregion
    }
    public static ArrayList Generar_Contabilizacion_Automatica_Intercompany_Operativo(int _empresaORIGENID, int _intercompanyDESTINOID, int _sisID, int _ttoID, int _blID, string _usuID)
    {
        ArrayList resultado = new ArrayList();
        ArrayList Arr_Cargos_Intercompany = null;
        ArrayList Arr_Configuracion_Intercompany_Operativo = null;
        Bean_Datos_BL Datos_BL = null;
        RE_GenericBean Bean_AUX = null;
        RE_GenericBean Configuracion_Cobro_Intercompany = null;
        RE_GenericBean Configuracion_Pago_Intercompany = null;
        RE_GenericBean Configuracion_Cobro_Por_Terceros = null;
        int alertaID = 0;
        string mensaje = "";
        int intercompanyORIGEN_ID = 0;
        int intercompanyDESTINO_ID = 0;
        int empresaORIGEN_ID = 0;
        int empresaDESTINO_ID = 0;
        int bandera_cobro_intercompany = 0;
        int bandera_pago_intercompany = 0;
        int bandera_cobro_por_terceros = 0;
        int bandera_cobro_profit = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader = null;
        NpgsqlTransaction transaccion;
        conn = DB.OpenConnection();
        transaccion = conn.BeginTransaction();
        comm = new NpgsqlCommand();
        comm.Connection = conn;
        comm.Transaction = transaccion;
        comm.CommandType = CommandType.Text;
        try
        {
            #region 1.  Validar Existencia de Configuracion Intercompanys Operativos

            empresaORIGEN_ID = _empresaORIGENID;
            Bean_AUX = (RE_GenericBean)DB.Get_Intercompany_Data_By_Empresa(empresaORIGEN_ID);
            intercompanyORIGEN_ID = Bean_AUX.intC1;
            intercompanyDESTINO_ID = _intercompanyDESTINOID;
            Bean_AUX = (RE_GenericBean)DB.Get_Intercompany_Data(intercompanyDESTINO_ID);
            empresaDESTINO_ID = Bean_AUX.intC3;

            string sql = " and tio_id_intercompany=" + intercompanyORIGEN_ID + " and tio_tiott_id=" + 1 + " and tio_tsis_id=" + _sisID + " ";
            bandera_cobro_intercompany = Contabilizacion_Automatica_CAD.Validar_Existencia_Intercompany_Operativo(user, sql);
            sql = " and tio_id_intercompany=" + intercompanyDESTINO_ID + " and tio_tiott_id=" + 2 + " and tio_tsis_id=" + _sisID + " ";
            bandera_pago_intercompany = Contabilizacion_Automatica_CAD.Validar_Existencia_Intercompany_Operativo(user, sql);
            sql = " and tio_id_intercompany=" + intercompanyDESTINO_ID + " and tio_tiott_id=" + 3 + " and tio_tsis_id=" + _sisID + " ";
            bandera_cobro_por_terceros = Contabilizacion_Automatica_CAD.Validar_Existencia_Intercompany_Operativo(user, sql);

            if (bandera_cobro_intercompany == 0)
            {
                alertaID = 16;
                mensaje = "No existe configuracion para generar la Contabilizacion Automatica de Cobro Intercompany";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            if (bandera_pago_intercompany == 0)
            {
                alertaID = 16;
                mensaje = "No existe configuracion para generar la Contabilizacion Automatica de Pago Intercompany";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            if (bandera_cobro_por_terceros == 0)
            {
                alertaID = 16;
                mensaje = "No existe configuracion para generar la Contabilizacion Automatica del Cobro al Cliente por Cuenta de Terceros";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            RE_GenericBean Bean_Intercompany_Origen = (RE_GenericBean)DB.Get_Intercompany_Data(intercompanyORIGEN_ID);
            RE_GenericBean Bean_Intercompany_Destino = (RE_GenericBean)DB.Get_Intercompany_Data(intercompanyDESTINO_ID);
            if (empresaORIGEN_ID==empresaDESTINO_ID)
            {
                alertaID = 0;
                resultado.Add(alertaID);
                resultado.Add("No se puede generar la Contabilizacion Automatica del Intercompany Operativo porque: El Intercompany Origen.: " + Bean_Intercompany_Origen.strC5 + " es igual al Intercompany Destino.: " + Bean_Intercompany_Destino.strC5 + " ");
                return resultado;
            }
            #endregion
            #region 2.  Validacion Tipos de Cambio
            if (Existe_Pais(empresaORIGEN_ID.ToString()) == false)
            {
                alertaID = 4;
                mensaje = "Empresa Origen no configurada dentro de Sistema BAW";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            if (Existe_Tipo_Cambio(empresaORIGEN_ID.ToString()) == false)
            {
                alertaID = 3;
                mensaje = "No Existe Tipo de Cambio en Empresa Origen";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            if (Existe_Pais(empresaDESTINO_ID.ToString()) == false)
            {
                alertaID = 4;
                mensaje = "Empresa Destino no configurada dentro de Sistema BAW";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            if (Existe_Tipo_Cambio(empresaDESTINO_ID.ToString()) == false)
            {
                alertaID = 3;
                mensaje = "No Existe Tipo de Cambio en Empresa Destino";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            #endregion
            #region 3.  Obtener Cargos Intercompany
            PaisBean Pais_Origen = new PaisBean();
            Pais_Origen = DB.getPais(empresaORIGEN_ID);
            PaisBean Pais_Destino = new PaisBean();
            Pais_Destino = DB.getPais(empresaDESTINO_ID);
            Arr_Cargos_Intercompany = (ArrayList)Contabilizacion_Automatica_CAD.Get_CargosIntercompany_X_Traficos(Pais_Origen, _sisID, _ttoID, _blID);
            if (Arr_Cargos_Intercompany == null)
            {
                alertaID = 7;
                mensaje = "Error al momento de Obtener Cargos Intercompany";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            else if (Arr_Cargos_Intercompany.Count == 0)
            {
                alertaID = 6;
                mensaje = "Documento sin cargos o cargos libres para generar Contabilizacion Automatica de Intercompany Operativo";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            #endregion
            #region 4.  Obtener Datos de la Carga
            Datos_BL = Contabilizacion_Automatica_CAD.Get_DatosBL_X_Traficos(_sisID, _ttoID, _blID, Pais_Origen);
            if (Datos_BL == null)
            {
                alertaID = 10;
                mensaje = "Existio un error al tratar de Obtener los Datos de la Carga";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            if ((Datos_BL.Mbl.Trim() == "") || (Datos_BL.Mbl.ToUpper().Trim() == "PENDIENTE"))
            {
                alertaID = 0;
                if (_sisID == 1)
                {
                    mensaje = "No se puede generar la Contabilizacion Automatica del Intercompany Operativo porque la carga aun no tiene Master BL asignado.";
                }
                else if (_sisID == 2)
                {
                    mensaje = "No se puede generar la Contabilizacion Automatica del Intercompany Operativo porque la carga aun no tiene Guia Master asignada.";
                }
                else if (_sisID == 3)
                {
                    mensaje = "No se puede generar la Contabilizacion Automatica del Intercompany Operativo porque la carga aun no tiene Carta de Porte Master asignada.";
                }
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            #endregion
            #region 5.  Crear Grupo
            int ID_GRUPO = 0;
            comm.CommandText = "select nextval('id_grupo_intercompany')";
            ID_GRUPO = int.Parse(comm.ExecuteScalar().ToString());
            #endregion
            #region 6.  Obtener Configuracion de Cobro Intercompany
            sql = " and tio_id_intercompany=" + intercompanyORIGEN_ID + " and tio_tiott_id=" + 1 + " and tio_tsis_id=" + _sisID + " ";
            Arr_Configuracion_Intercompany_Operativo = (ArrayList)Contabilizacion_Automatica_CAD.Obtener_Configuraciones_Intercompany_Operativo(null, sql);
            if (Arr_Configuracion_Intercompany_Operativo == null)
            {
                alertaID = 10;
                mensaje = "Existio un error al tratar de Obtener la Configuracion de Cobro al Intercompany Destino";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            if (Arr_Configuracion_Intercompany_Operativo.Count == 0)
            {
                alertaID = 10;
                mensaje = "No existe la Configuracion de Cobro al Intercompany Destino";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            Configuracion_Cobro_Intercompany = (RE_GenericBean)Arr_Configuracion_Intercompany_Operativo[0];
            #endregion
            #region 7.  Construir Cobro Intercompany Origen a Destino
            resultado = (ArrayList)Contabilizacion_Automatica_CAD.Construir_Factura_Intercompany_Operativo(Configuracion_Cobro_Intercompany, Datos_BL, Arr_Cargos_Intercompany, intercompanyORIGEN_ID, intercompanyDESTINO_ID, _usuID);
            if ((resultado[0].ToString() == "0")||(resultado[0].ToString() == "-100"))
            {
                transaccion.Rollback();
                return resultado;
            }
            Bean_Factura_Automatica Factura_Automatica = (Bean_Factura_Automatica)resultado[1];
            resultado = new ArrayList();
            resultado = Contabilizacion_Automatica_CAD.Insertar_Factura(conn, comm, transaccion, Pais_Origen, Factura_Automatica.tfa_conta_id, Factura_Automatica.tfa_suc_id, Factura_Automatica);
            if (resultado[0].ToString()  == "0")
            {
                transaccion.Rollback();
                return resultado;
            }
            resultado = new ArrayList();
            resultado = Contabilizacion_Automatica_CAD.Asignar_Grupo_Cargos_Intercompany(Datos_BL, Arr_Cargos_Intercompany, Pais_Origen, _sisID, ID_GRUPO);
            if (resultado[0].ToString() == "0")
            {
                transaccion.Rollback();
                return resultado;
            }
            #endregion
            #region 8.  Obtener Configuracion de Pago Intercompany Destino a Intercompany Origen
            sql = "";
            sql = " and tio_id_intercompany=" + intercompanyDESTINO_ID + " and tio_tiott_id=" + 2 + " and tio_tsis_id=" + _sisID + "  ";
            //sql = " and tio_id_intercompany=" + intercompanyDESTINO_ID + " and tio_tiott_id=" + 2 + " ";

            Arr_Configuracion_Intercompany_Operativo = (ArrayList)Contabilizacion_Automatica_CAD.Obtener_Configuraciones_Intercompany_Operativo(null, sql);
            if (Arr_Configuracion_Intercompany_Operativo == null)
            {
                alertaID = 10;
                mensaje = "Existio un error al tratar de Obtener la Configuracion de Pago del Intercompany Destino al Intercompany Origen";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            if (Arr_Configuracion_Intercompany_Operativo.Count == 0)
            {
                alertaID = 10;
                mensaje = "No existe la Configuracion de Pago al Intercompany Origen";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            Configuracion_Pago_Intercompany = (RE_GenericBean)Arr_Configuracion_Intercompany_Operativo[0];
            #endregion
            #region 9.  Construir Pago Intercompany Destino a Origen
            resultado = (ArrayList)Contabilizacion_Automatica_CAD.Construir_Provision_Intercompany_Operativo(Configuracion_Pago_Intercompany, Datos_BL, Arr_Cargos_Intercompany, intercompanyORIGEN_ID, intercompanyDESTINO_ID, _usuID);
            if ((resultado[0].ToString() == "0") || (resultado[0].ToString() == "-100"))
            {
                #region Desmarcar Cargos Asociados a la Factura
                UsuarioBean user_liberar_factura = new UsuarioBean();
                user_liberar_factura.ID = Factura_Automatica.tfa_usu_id;
                user_liberar_factura.pais = Pais_Origen;
                int update_result_liberar_factura = 0;
                foreach (Bean_Detalle_Rubros Cargos in Factura_Automatica.Arr_Detalle_Facturacion)
                {
                    if (Factura_Automatica.tfa_tto_id == 1)//FCL
                    {
                        update_result_liberar_factura = DB.Update_Cargos_Traficos(user_liberar_factura, Factura_Automatica.tfa_tto_id, 0, Cargos.tdf_cargo_id, Factura_Automatica.tfa_blid, Factura_Automatica.tfa_id, 1, "F", 1);
                    }
                    else if (Factura_Automatica.tfa_tto_id == 2)//LCL
                    {
                        update_result_liberar_factura = DB.Update_Cargos_Traficos(user_liberar_factura, Factura_Automatica.tfa_tto_id, 0, Cargos.tdf_cargo_id, Factura_Automatica.tfa_blid, Factura_Automatica.tfa_id, 1, "L", 1);
                    }
                    else//AEREO - TERRESTRE
                    {
                        update_result_liberar_factura = DB.Update_Cargos_Traficos(user_liberar_factura, Factura_Automatica.tfa_tto_id, 0, Cargos.tdf_cargo_id, Factura_Automatica.tfa_blid, Factura_Automatica.tfa_id, 1, "", 1);
                    }
                }
                #endregion
                transaccion.Rollback();
                return resultado;
            }
            Bean_Provision_Automatica Provision_Automatica = (Bean_Provision_Automatica)resultado[1];
            Provision_Automatica.tpr_fact_id = Factura_Automatica.tfa_serie;
            Provision_Automatica.tpr_fact_corr = Factura_Automatica.tfa_correlativo;
            resultado = new ArrayList();
            resultado = Contabilizacion_Automatica_CAD.Insertar_Provision(conn, comm, transaccion, Pais_Destino, Provision_Automatica.tpr_tcon_id, Provision_Automatica.tpr_suc_id, Provision_Automatica);
            if (resultado[0].ToString() == "0")
            {
                transaccion.Rollback();
                return resultado;
            }
            Provision_Automatica = (Bean_Provision_Automatica)resultado[1];
            resultado = new ArrayList();
            resultado = Contabilizacion_Automatica_CAD.Insertar_Costos_Intercompany_Operativo(_sisID, Datos_BL, Arr_Cargos_Intercompany, Pais_Origen, Pais_Destino, 5, Provision_Automatica.tpr_prov_id, ID_GRUPO, intercompanyORIGEN_ID, _usuID);
            if (resultado[0].ToString() == "0")
            {
                transaccion.Rollback();
                return resultado;
            }
            else if (resultado[0].ToString() == "1")
            {
                Arr_Cargos_Intercompany = (ArrayList)resultado[1];
            }
            resultado = new ArrayList();
            resultado = Contabilizacion_Automatica_CAD.Amarrar_Cargos_Costos_Intercompany_Operativo(conn, comm, transaccion, Arr_Cargos_Intercompany, Provision_Automatica.Arr_Detalle_Provision, "COSTO");
            if (resultado[0].ToString() == "0")
            {
                transaccion.Rollback();
                return resultado;
            }
            #endregion
            #region 10.  Obtener Configuracion de Cobro por Cuenta de Terceros en Intercompany Destino
            sql = "";
            sql = " and tio_id_intercompany=" + intercompanyDESTINO_ID + " and tio_tiott_id=" + 3 + " and tio_tsis_id=" + _sisID + " ";
            //sql = " and tio_id_intercompany=" + intercompanyDESTINO_ID + " and tio_tiott_id=" + 3 + " ";
            Arr_Configuracion_Intercompany_Operativo = (ArrayList)Contabilizacion_Automatica_CAD.Obtener_Configuraciones_Intercompany_Operativo(null, sql);
            if (Arr_Configuracion_Intercompany_Operativo == null)
            {
                alertaID = 10;
                mensaje = "Existio un error al tratar de Obtener la Configuracion de Cobro por Cuenta de Terceros del Intercompany Destino";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            if (Arr_Configuracion_Intercompany_Operativo.Count == 0)
            {
                alertaID = 10;
                mensaje = "No existe la Configuracion de Cobro por Cuenta de Terceros del Intercompany Destino";
                resultado.Add(alertaID);
                resultado.Add(mensaje);
                return resultado;
            }
            Configuracion_Cobro_Por_Terceros = (RE_GenericBean)Arr_Configuracion_Intercompany_Operativo[0];
            #endregion
            #region 11.  Construir Cobro Intercompany Destino a Cliente
            resultado = (ArrayList)Contabilizacion_Automatica_CAD.Construir_Nota_Debito_Intercompany_Operativo(Configuracion_Cobro_Por_Terceros, Datos_BL, Arr_Cargos_Intercompany, intercompanyORIGEN_ID, intercompanyDESTINO_ID, _usuID);
            if ((resultado[0].ToString() == "0") || (resultado[0].ToString() == "-100"))
            {
                #region Desmarcar Cargos Asociados a la Factura
                UsuarioBean user_liberar_factura = new UsuarioBean();
                user_liberar_factura.ID = Factura_Automatica.tfa_usu_id;
                user_liberar_factura.pais = Pais_Origen;
                int update_result_liberar_factura = 0;
                foreach (Bean_Detalle_Rubros Cargos in Factura_Automatica.Arr_Detalle_Facturacion)
                {
                    if (Factura_Automatica.tfa_tto_id == 1)//FCL
                    {
                        update_result_liberar_factura = DB.Update_Cargos_Traficos(user_liberar_factura, Factura_Automatica.tfa_tto_id, 0, Cargos.tdf_cargo_id, Factura_Automatica.tfa_blid, Factura_Automatica.tfa_id, 1, "F", 1);
                    }
                    else if (Factura_Automatica.tfa_tto_id == 2)//LCL
                    {
                        update_result_liberar_factura = DB.Update_Cargos_Traficos(user_liberar_factura, Factura_Automatica.tfa_tto_id, 0, Cargos.tdf_cargo_id, Factura_Automatica.tfa_blid, Factura_Automatica.tfa_id, 1, "L", 1);
                    }
                    else//AEREO - TERRESTRE
                    {
                        update_result_liberar_factura = DB.Update_Cargos_Traficos(user_liberar_factura, Factura_Automatica.tfa_tto_id, 0, Cargos.tdf_cargo_id, Factura_Automatica.tfa_blid, Factura_Automatica.tfa_id, 1, "", 1);
                    }
                }
                #endregion
                #region Desactivar Costos Asociados a la Provision
                int update_result_liberar_provision = 0;
                UsuarioBean user_liberar_provision = new UsuarioBean();
                user_liberar_provision.ID = Provision_Automatica.tpr_usu_creacion;
                user_liberar_provision.pais = Pais_Origen;
                foreach (Bean_Cargos Costo in Arr_Cargos_Intercompany)
                {
                    if (Provision_Automatica.tpr_tto_id == 1)//FCL
                    {
                        update_result_liberar_provision = DB.Update_Costos_Traficos(user_liberar_provision.pais, Provision_Automatica.tpr_tto_id, 0, Costo.Costo_Terceros_ID, Provision_Automatica.tpr_blid, Provision_Automatica.tpr_prov_id, 5, "F", 3);
                    }
                    else if (Provision_Automatica.tpr_tto_id == 2)//LCL
                    {
                        update_result_liberar_provision = DB.Update_Costos_Traficos(user_liberar_provision.pais, Provision_Automatica.tpr_tto_id, 0, Costo.Costo_Terceros_ID, Provision_Automatica.tpr_blid, Provision_Automatica.tpr_prov_id, 5, "L", 3);
                    }
                    else//AEREO - TERRESTRE
                    {
                        update_result_liberar_provision = DB.Update_Costos_Traficos(user_liberar_provision.pais, Provision_Automatica.tpr_tto_id, 0, Costo.Costo_Terceros_ID, Provision_Automatica.tpr_blid, Provision_Automatica.tpr_prov_id, 5, "", 3);
                    }
                }
                #endregion
                transaccion.Rollback();
                return resultado;
            }
            Bean_Nota_Debito_Automatica Nota_Debito_Automatica = null;
            if (resultado[0].ToString() != "17")
            {
                Nota_Debito_Automatica = (Bean_Nota_Debito_Automatica)resultado[1];
                resultado = new ArrayList();
                resultado = Contabilizacion_Automatica_CAD.Insertar_Nota_Debito(conn, comm, transaccion, Pais_Destino, Nota_Debito_Automatica.tnd_tcon_id, Nota_Debito_Automatica.tnd_suc_id, Nota_Debito_Automatica);
                if (resultado[0].ToString() == "0")
                {
                    transaccion.Rollback();
                    return resultado;
                }
                Nota_Debito_Automatica = (Bean_Nota_Debito_Automatica)resultado[2];
                resultado = new ArrayList();
                resultado = Contabilizacion_Automatica_CAD.Insertar_Cargos_Intercompany_Operativo(_sisID, Datos_BL, Arr_Cargos_Intercompany, Pais_Origen, 4, Nota_Debito_Automatica.tnd_id, ID_GRUPO);
                if (resultado[0].ToString() == "0")
                {
                    transaccion.Rollback();
                    return resultado;
                }
                else if (resultado[0].ToString() == "1")
                {
                    Arr_Cargos_Intercompany = (ArrayList)resultado[1];
                }
                resultado = new ArrayList();
                resultado = Contabilizacion_Automatica_CAD.Amarrar_Cargos_Costos_Intercompany_Operativo(conn, comm, transaccion, Arr_Cargos_Intercompany, Nota_Debito_Automatica.Arr_Detalle_Facturacion, "CARGO");
                if (resultado[0].ToString() == "0")
                {
                    transaccion.Rollback();
                    return resultado;
                }
            }
            else
            {
                bandera_cobro_profit++;
            }
            #endregion
            #region 12.  Insertar Log Transacciones Encadenadas
            RE_GenericBean Bean_Log = new RE_GenericBean();
            Bean_Log.intC1 = 1;//ttel_padre_ttr_id
            Bean_Log.intC2 = Factura_Automatica.tfa_id;//ttel_padre_ref_id
            Bean_Log.intC3 = 5;//ttel_hijo_ttr_id
            Bean_Log.intC4 = Provision_Automatica.tpr_prov_id;//ttel_hijo_ref_id
            Bean_Log.intC5 = 2;//ttel_tta_id
            Bean_Log.strC1 = _usuID;//ttel_usuario_emision
            Bean_Log.intC6 = Factura_Automatica.tfa_pai_id;//ttel_padre_empresa_id
            Bean_Log.intC7 = Provision_Automatica.tpr_pai_id;//ttel_hijo_empresa_id
            Bean_Log.intC8 = ID_GRUPO;
            int resutaldo_log_transacciones = 0;
            resutaldo_log_transacciones = Contabilizacion_Automatica_CAD.Insertar_Log_Transacciones_Encadenadas(Bean_Log);
            if (bandera_cobro_profit == 0)
            {
                Bean_Log = new RE_GenericBean();
                Bean_Log.intC1 = 1;//ttel_padre_ttr_id
                Bean_Log.intC2 = Factura_Automatica.tfa_id;//ttel_padre_ref_id
                Bean_Log.intC3 = 4;//ttel_hijo_ttr_id
                Bean_Log.intC4 = Nota_Debito_Automatica.tnd_id;//ttel_hijo_ref_id
                Bean_Log.intC5 = 2;//ttel_tta_id
                Bean_Log.strC1 = _usuID;//ttel_usuario_emision
                Bean_Log.intC6 = Factura_Automatica.tfa_pai_id;//ttel_padre_empresa_id
                Bean_Log.intC7 = Nota_Debito_Automatica.tnd_pai_id;//ttel_hijo_empresa_id
                Bean_Log.intC8 = ID_GRUPO;
                resutaldo_log_transacciones = 0;
                resutaldo_log_transacciones = Contabilizacion_Automatica_CAD.Insertar_Log_Transacciones_Encadenadas(Bean_Log);
            }
            #endregion
            #region 13.  Generar Notificacion Automatica de Intercompanys
            transaccion.Commit();
            bool resultado_notificacion = false;
            resultado_notificacion = Contabilizacion_Automatica_CN.Generar_Notificacion_Automatica_Intercompany(Bean_Log.intC1, Bean_Log.intC2, 2);
            #endregion
            #region 14.  Finalizar Transaccion
            resultado = new ArrayList();
            resultado.Add("1");
            if (bandera_cobro_profit == 0)
            {
                resultado.Add("Intercompany Operativo Contabilizado Exitosamente, Se generaron automaticamente las siguientes Transacciones: el Cobro del Intercompany Origen.: " + Bean_Intercompany_Origen.strC5 + " al Intercompany Destino.: " + Bean_Intercompany_Destino.strC5 + " con Serie.:  " + Factura_Automatica.tfa_serie + " y Correlativo.: " + Factura_Automatica.tfa_correlativo + ". Se genero el Pago de.: " + Bean_Intercompany_Destino.strC5 + " A.: " + Bean_Intercompany_Origen.strC5 + " con Serie.: " + Provision_Automatica.tpr_serie + " y Correlativo.:  " + Provision_Automatica.tpr_correlativo + " y el Cobro al Cliente por Cuenta de Terceros en SISTEMA BAW " + Pais_Destino.Nombre_Sistema + " con Serie.: " + Nota_Debito_Automatica.tnd_serie + " y Correlativo.: " + Nota_Debito_Automatica.tnd_correlativo + " ");
            }
            else
            {
                resultado.Add("Intercompany Operativo Contabilizado Exitosamente, Se generaron automaticamente las siguientes Transacciones: el Cobro del Intercompany Origen.: " + Bean_Intercompany_Origen.strC5 + " al Intercompany Destino.: " + Bean_Intercompany_Destino.strC5 + " con Serie.:  " + Factura_Automatica.tfa_serie + " y Correlativo.: " + Factura_Automatica.tfa_correlativo + ". Se genero el Pago de.: " + Bean_Intercompany_Destino.strC5 + " A.: " + Bean_Intercompany_Origen.strC5 + " con Serie.: " + Provision_Automatica.tpr_serie + " y Correlativo.:  " + Provision_Automatica.tpr_correlativo + " ");
            }
            Arr_Cargos_Intercompany = null;
            Arr_Configuracion_Intercompany_Operativo = null;
            Datos_BL = null;
            Bean_AUX = null;
            Configuracion_Cobro_Intercompany = null;
            Configuracion_Pago_Intercompany = null;
            Configuracion_Cobro_Por_Terceros = null;
            Pais_Origen = null;
            Bean_Intercompany_Origen = null;
            Pais_Destino = null;
            Bean_Intercompany_Destino = null;
            Factura_Automatica = null;
            Provision_Automatica = null;
            Nota_Debito_Automatica = null;
            #endregion
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            transaccion.Rollback();
            return null;
        }
        finally
        {
            DB.CloseObj(reader, comm, conn);
        }
        return resultado;
    }
    public static Rubros Calcular_Impuestos(int empresaID, int ContaID, int monedaID, int servicioID, long RubroID, double Monto, int Impuesto_Proveedor, PaisBean Pais_Bean)
    {
        #region Realizar Calculo de Impuestos
        Rubros Rubros_Bean = null;
        Rubros Rubro = null;
        Rubro = new Rubros();
        Rubro.rubroID = RubroID;
        Rubros_Bean = (Rubros)DB.ExistRubroPais(Rubro, empresaID);
        Rubros_Bean.rubroTypeID = servicioID;
        if (Rubros_Bean == null)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog("Error, El BL no tiene todos los rubros registrados en contabilidad para este pais.");
            return null;
        }
        if ((Impuesto_Proveedor != 1))//si debe cobrar iva y el rubro no esta en dolares y no es excento
        {
            if (Rubros_Bean.CobIva == 1)
            {
                if (Rubros_Bean.IvaInc == 1)
                {
                    Rubros_Bean.rubroTot = Monto;
                    Rubros_Bean.rubroSubTot = Math.Round(Monto * (double)(1 / (1 + Pais_Bean.Impuesto)), 2);
                    Rubros_Bean.rubroImpuesto = Math.Round(Monto - Rubros_Bean.rubroSubTot, 2);
                }
                else
                {
                    Rubros_Bean.rubroImpuesto = Math.Round(Monto * (double)Pais_Bean.Impuesto, 2);
                    Rubros_Bean.rubroSubTot = Math.Round(Monto, 2);
                    Rubros_Bean.rubroTot = Math.Round(Rubros_Bean.rubroSubTot + Rubros_Bean.rubroImpuesto, 2);
                }
            }
            else if (Rubros_Bean.CobIva == 0)
            {
                Rubros_Bean.rubroTot = Monto;
                Rubros_Bean.rubroSubTot = Monto;
                Rubros_Bean.rubroImpuesto = 0;
            }
        }
        else
        {
            Rubros_Bean.rubroTot = Monto;
            Rubros_Bean.rubroSubTot = Monto;
            Rubros_Bean.rubroImpuesto = 0;
        }
        if (monedaID == 8)
        {
            Rubros_Bean.rubroTotD = Math.Round((double)Rubros_Bean.rubroTot * (double)Pais_Bean.TipoCambio, 2);
        }
        else
        {
            Rubros_Bean.rubroTotD = Math.Round((double)Rubros_Bean.rubroTot / (double)Pais_Bean.TipoCambio, 2);
        }
        return Rubros_Bean;
        #endregion
    }
    public static int Traducir_Moneda_Master_To_BAW_X_ID(int monedaID)
    {
        #region Traducir Moneda de la Master al BAW por ID
        int monedaIDBAW = 0;
        string simboloMONEDA = "";
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenMasterConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select moneda_id, simbolo from monedas where moneda_id=" + monedaID + "";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                simboloMONEDA = reader.GetValue(1).ToString();
            }
            DB.CloseObj(reader, comm, conn);
            monedaIDBAW = Utility.TraducirMonedaStr(simboloMONEDA);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return monedaIDBAW;
        #endregion
    }
    public static bool Generar_Notificacion_Automatica_Intercompany(int ttrID_Padre, int refID_Padre, int Tipo_Intercompany)
    {
        bool resultado = false;
        RE_GenericBean Bean_Detalle_Transaccion = null;
        try
        {
            Bean_Detalle_Transaccion = Contabilizacion_Automatica_CAD.Obtener_Detalle_Notificacion_Automatica_Intercompany(ttrID_Padre, refID_Padre, Tipo_Intercompany);
            #region Enviar Reporte por Correo
            System.Net.Mail.MailMessage Email = new System.Net.Mail.MailMessage();
            string Server = "mail.aimargroup.com";
            string body = "";
            string lista_de_distribucion = "";
            int empresaDESTINO_ID = 0;
            empresaDESTINO_ID = int.Parse(Bean_Detalle_Transaccion.strC35);
            #region Definir Lista de Distribucion
            switch (empresaDESTINO_ID)
            {
                case 1:
                    lista_de_distribucion = "maria-zacarias@aimargroup.com, esdras-mendez@aimargroup.com, gt-traffic10@aimargroup.com, prealertgt@aimargroup.com, luis-solares@aimargroup.com, luis-sosa@aimargroup.com";
                    break;
                case 2:
                    lista_de_distribucion = "maria-zacarias@aimargroup.com, alex-zelada@aimargroup.com, brenda-villalta@aimargroup.com";
                    break;
                case 3:
                    lista_de_distribucion = "maria-zacarias@aimargroup.com, suyapa-bueso@aimargroup.com, hn-contabilidad@aimargroup.com, roberto-cerrato@aimargroup.com";
                    break;
                case 4:
                    lista_de_distribucion = "maria-zacarias@aimargroup.com, maria-lopez@aimargroup.com, giovanni-fonseca@aimargroup.com, nic-traffic01@aimargroup.com, nic-traffic03@aimargroup.com, nic-traffic05@aimargroup.com, wilder-perez@aimargroup.com";
                    break;
                case 5:
                    lista_de_distribucion = "maria-zacarias@aimargroup.com, carol-morales@aimargroup.com, marcela-palma@aimargroup.com, luis-sterling@aimargroup.com, cr-traffic03@aimargroup.com, cr-intermodaltransport@aimargroup.com ";
                    break;
                case 6:
                    lista_de_distribucion = "maria-zacarias@aimargroup.com, lorena-arcia@aimargroup.com, jessie-girado@aimargroup.com, ingrid-jimenez@aimargroup.com, isis-rodriguez@aimargroup.com ";
                    break;
                case 7:
                    lista_de_distribucion = "maria-zacarias@aimargroup.com, licel-velasquez@aimargroup.com, letticia-calderon@aimargroup.com";
                    break;
                case 9:
                    lista_de_distribucion = "maria-zacarias@aimargroup.com, claudia-ayala@aimargroup.com";
                    break;
                case 11:
                    lista_de_distribucion = "maria-zacarias@aimargroup.com, omar-pizzi1@grhlogistics.com, ricardo-gonzalez@grhlogistics.com, juan-moraga@grhlogistics.com, nic-transportelocal@grhlogistics.com, lilliam-mercado@grhlogistics.com, nic-gerenciaops@grhlogistics.com ";
                    break;
                case 12:
                    lista_de_distribucion = "maria-zacarias@aimargroup.com, giovanni-fonseca@aimargroup.com, rolando-pineda@isisurveyor.com";
                    break;
                case 13:
                    lista_de_distribucion = "maria-zacarias@aimargroup.com, rodolfo-gonzales@aimargroup.com, edgard-campos@aimargroup.com";
                    break;
                case 15:
                    lista_de_distribucion = "maria-zacarias@aimargroup.com, esdras-mendez@aimargroup.com, yashira-shutuc@aimargroup.com, prealertgt@aimargroup.com, luis-solares@aimargroup.com, luis-sosa@aimargroup.com";
                    break;
                case 18:
                    lista_de_distribucion = "maria-zacarias@aimargroup.com, elder-osoy@aimargroup.com, esdras-mendez@aimargroup.com";
                    break;
                case 21:
                    lista_de_distribucion = "maria-zacarias@aimargroup.com, carol-morales@aimargroup.com, luis-sterling@aimargroup.com, cr-traffic03@aimargroup.com, cr-intermodaltransport@aimargroup.com";
                    break;
                case 22:
                    lista_de_distribucion = "maria-zacarias@aimargroup.com, licel-velasquez@aimargroup.com, letticia-calderon@aimargroup.com";
                    break;
                case 23:
                    lista_de_distribucion = "maria-zacarias@aimargroup.com, suyapa-bueso@aimargroup.com, roberto-cerrato@aimargroup.com";
                    break;
                case 24:
                    lista_de_distribucion = "maria-zacarias@aimargroup.com, noelia-gonzalez@aimargroup.com, nic-traffic01@aimargroup.com, nic-traffic03@aimargroup.com, nic-traffic05@aimargroup.com, wilder-perez@aimargroup.com";
                    break;
                case 25:
                    lista_de_distribucion = "maria-zacarias@aimargroup.com, lorena-arcia@aimargroup.com, jessie-girado@aimargroup.com, ingrid-jimenez@aimargroup.com, isis-rodriguez@aimargroup.com";
                    break;
                case 26:
                    //lista_de_distribucion = "maria-zacarias@aimargroup.com, alex-zelada@aimargroup.com, brenda-villalta@aimargroup.com";
                    //Ticket#2019072931000341 — CORRECCION CORREOS NOTIFICACION AUTOMATICA FACTURACION INTERCOMPANY / LF 
                    lista_de_distribucion = "maria-zacarias@aimargroup.com, operaciones.sv1@latinfreightneutral.com, customer.sv1@latinfreightneutral.com, customer.sv2@latinfreightneutral.com, contabilidad.sv1@latinfreightneutral.com";
                    break;
                default:
                    lista_de_distribucion = "";
                    break;
            }
            #endregion
            Email.From = new System.Net.Mail.MailAddress("sistema-contable@aimargroup.com");
            //Email.To.Add(lista_de_distribucion);
            Email.To.Add("soporte2@aimargroup.com");
            Email.Bcc.Add("soporte2@aimargroup.com");
            Email.Subject = "Notificacion Automatica de Cobro - " + Bean_Detalle_Transaccion.strC30 + "  (" + Bean_Detalle_Transaccion.strC3 + ")";
            body = Generar_HTML_Notificacion_Intercompany(Tipo_Intercompany, Bean_Detalle_Transaccion);
            #region Validar Lista de Distribucion y HTML
            if ((body == "") || (lista_de_distribucion == ""))
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog("Error al Generar Notificacion Automatica Intercompany (ttrID_Padre=" + ttrID_Padre + " , refID_Padre=" + refID_Padre + " , Tipo_Intercompany=" + Tipo_Intercompany + " ), ");
                resultado = false;
                return resultado;
            }
            #endregion
            AlternateView viewprint = AlternateView.CreateAlternateViewFromString(body, Encoding.UTF8, MediaTypeNames.Text.Html);
            Email.AlternateViews.Add(viewprint);
            SmtpClient Cliente_Smtp = new SmtpClient(Server);
            Cliente_Smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
            try
            {
                Cliente_Smtp.Send(Email);
                Contabilizacion_Automatica_CAD.Actualizar_Estado_Notificacion_Intercompany(ttrID_Padre, refID_Padre);
                resultado = true;
            }
            catch (Exception ex)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(ex.Message);
                ErrLog.ErrorLog("Error al Enviar Notificacion Automatica Intercompany (ttrID_Padre=" + ttrID_Padre + " , refID_Padre=" + refID_Padre + " , Tipo_Intercompany=" + Tipo_Intercompany + " ), " + ex.Message);
                resultado = false;
            }
            #endregion
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog("Error al Generar Notificacion Automatica Intercompany (ttrID_Padre=" + ttrID_Padre + " , refID_Padre=" + refID_Padre + " , Tipo_Intercompany=" + Tipo_Intercompany + " ), " + e.Message);
            return false;
        }
        return resultado;
    }
    public static string Generar_HTML_Notificacion_Intercompany(int Tipo_Intercompany, RE_GenericBean Bean_Detalle_Intercompany)
    {
        string _HTML = "";
        string _pathLOGO = "http://www.aimargroup.com/img/aimar.jpg";
        #region Obtener Path del Logotipo
        //RE_GenericBean Bean_AUX = (RE_GenericBean)DB.Get_Intercompany_Data(int.Parse(Bean_Detalle_Intercompany.strC34));
        PaisBean Pais_AUX = null;
        Pais_AUX = DB.getPais(int.Parse(Bean_Detalle_Intercompany.strC34));
        _pathLOGO = Pais_AUX.Imagepath.Replace("~/img/", "http://www.aimargroup.com/logos/BAW/");
        #endregion
        _HTML = "<html>" +
        "<body>" +
        "<table align='center' cellpadding='0' cellspacing='0'>" +
        "<tr>" +
            "<td align='center'>" +

                "<img alt=Logo' src='" + _pathLOGO + "' height='100' width='300' />" +
                "<br>" +
                "<br>" +
            "</td>" +
        "</tr>" +
        "<tr>" +
            "<td>" +
                "<table cellpadding='0' cellspacing='0' " +
                    "style='font-family:Verdana;font-size:12pt;' width='600px'>" +
                    "<tr>" +
                        "<td>" +
                            "Estimado Intercompany, <b>" + Bean_Detalle_Intercompany.strC31 + " (Nit. " + Bean_Detalle_Intercompany.strC33 + ")</b></td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td style='border-bottom:1px dotted #2525E2;padding-bottom:20px;'>" +
                            "<br />" +
                            "<b>" + Bean_Detalle_Intercompany.strC30 + " (Nit. " + Bean_Detalle_Intercompany.strC32 + ") </b>Ha emitido para usted un " +
                            "documento de cobro. A continuación podrá encontrar mas " +
                            "información.</td>" +
                    "</tr>" +
                "</table>" +
            "</td>" +
        "</tr>" +
        "<tr>" +
            "<td>" +
                "<table cellpadding='0' cellspacing='0' " +
                    "style='font-family:Verdana;font-size:12pt;' width='600px'>" +
                    "<tr>" +
                        "<td>" +
                            "<b>Información del Cobro en la Contabilidad de " + Bean_Detalle_Intercompany.strC30 + "</b></td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td style='border-bottom:1px dotted #2525E2;padding-bottom:20px;'>" +
                            "<br />" +
                            "<ul>" +
                                "<li>Documento: " + Bean_Detalle_Intercompany.strC1 + "</li>" +
                                "<li>Fecha de emisión: " + Bean_Detalle_Intercompany.strC2 + "</li>" +
                                "<li>Serie y número: " + Bean_Detalle_Intercompany.strC3 + "</li>" +
                                "<li>Contabilidad: " + Bean_Detalle_Intercompany.strC4 + "</li>" +
                                "<li>Moneda: " + Bean_Detalle_Intercompany.strC5 + "</li>" +
                                "<li>Monto: " + Bean_Detalle_Intercompany.strC6 + "</li>" +
                                "<li>Master: " + Bean_Detalle_Intercompany.strC7 + "</li>" +
                                "<li>House: " + Bean_Detalle_Intercompany.strC8 + "</li>" +
                                "<li>Observaciones: " + Bean_Detalle_Intercompany.strC9 + "</li>" +
                            "</ul>" +
                        "</td>" +
                    "</tr>" +
                "</table>" +
            "</td>" +
        "</tr>" +
        "<tr>" +
            "<td>" +
                "<table cellpadding='0' cellspacing='0' " +
                    "style='font-family:Verdana;font-size:12pt;' width='600px'>" +
                    "<tr>" +
                        "<td>" +
                            "<b>Información del Pago en la Contabilidad de " + Bean_Detalle_Intercompany.strC31 + "</b></td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td style='border-bottom:1px dotted #2525E2;padding-bottom:20px;'>" +
                            "<br />" +
                            "<ul>" +
                                "<li>Documento: " + Bean_Detalle_Intercompany.strC10 + "</li>" +
                                "<li>Pagar a: " + Bean_Detalle_Intercompany.strC11 + "</li>" +
                                "<li>Fecha de emisión: " + Bean_Detalle_Intercompany.strC12 + "</li>" +
                                "<li>Serie y número: " + Bean_Detalle_Intercompany.strC13 + "</li>" +
                                "<li>Contabilidad: " + Bean_Detalle_Intercompany.strC14 + "</li>" +
                                "<li>Moneda: " + Bean_Detalle_Intercompany.strC15 + "</li>" +
                                "<li>Monto: " + Bean_Detalle_Intercompany.strC16 + "</li>" +
                                "<li>Master: " + Bean_Detalle_Intercompany.strC17 + "</li>" +
                                "<li>House: " + Bean_Detalle_Intercompany.strC18 + "</li>" +
                                "<li>Observaciones: " + Bean_Detalle_Intercompany.strC19 + "</li>" +
                            "</ul>" +
                        "</td>" +
                    "</tr>" +
                "</table>" +
            "</td>" +
        "</tr>";
        if (Tipo_Intercompany == 2)
        {
            _HTML += "<tr>" +
                "<td>" +
                    "<table cellpadding='0' cellspacing='0' " +
                        "style='font-family:Verdana;font-size:12pt;' width='600px'>" +
                        "<tr>" +
                            "<td>" +
                                "<b>Información del Cobro al Cliente por Cuenta de Terceros en la Contabilidad de " + Bean_Detalle_Intercompany.strC31 + "</b></td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td style='border-bottom:1px dotted #2525E2;padding-bottom:20px;'>" +
                                "<br />" +
                                "<ul>" +
                                    "<li>Documento: " + Bean_Detalle_Intercompany.strC20 + "</li>" +
                                    "<li>Cliente: " + Bean_Detalle_Intercompany.strC21 + "</li>" +
                                    "<li>Fecha de emisión: " + Bean_Detalle_Intercompany.strC22 + "</li>" +
                                    "<li>Serie y número: " + Bean_Detalle_Intercompany.strC23 + "</li>" +
                                    "<li>Contabilidad: " + Bean_Detalle_Intercompany.strC24 + "</li>" +
                                    "<li>Moneda: " + Bean_Detalle_Intercompany.strC25 + "</li>" +
                                    "<li>Monto: " + Bean_Detalle_Intercompany.strC26 + "</li>" +
                                    "<li>Master: " + Bean_Detalle_Intercompany.strC27 + "</li>" +
                                    "<li>House: " + Bean_Detalle_Intercompany.strC28 + "</li>" +
                                    "<li>Observaciones: " + Bean_Detalle_Intercompany.strC29 + "</li>" +
                                "</ul>" +
                            "</td>" +
                        "</tr>" +
                    "</table>" +
                "</td>" +
            "</tr>";
        }
        _HTML += "</table>" +
        "</body>" +
        "</html>";
        return _HTML;
    }
    public static bool Generate_Intercompany_Notification_Forwarding()
    {
        bool resultado = true;
        //OBTENER NOTIFICACIONES INTERCOMPANY PENDIENTES DE ENVIAR
        ArrayList Arr_Notificaciones_Pendientes_Enviar = null;
        Arr_Notificaciones_Pendientes_Enviar = Contabilizacion_Automatica_CAD.Obtener_Transacciones_Intercompany_Pendientes_Notificar();
        foreach (RE_GenericBean Bean in Arr_Notificaciones_Pendientes_Enviar)
        {
            resultado = Generar_Notificacion_Automatica_Intercompany(int.Parse(Bean.strC3), int.Parse(Bean.strC4), int.Parse(Bean.strC5));
            int milliseconds = 20000;//REALIZAR ENVIO CADA 20 SEGUNDOS
            Thread.Sleep(milliseconds);
        }
        return resultado;
    }
    public static ArrayList Generar_Contabilizacion_Embarque_SCA(int sesionID)
    {
        ArrayList resultado = new ArrayList();
        ArrayList Arr_Facturas_Cliente_Generadas = new ArrayList();
        ArrayList Arr_Notas_Debito_Cliente_Generadas = new ArrayList();
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader = null;
        NpgsqlTransaction transaccion;
        conn = DB.OpenConnection();
        transaccion = conn.BeginTransaction();
        comm = new NpgsqlCommand();
        comm.Connection = conn;
        comm.Transaction = transaccion;
        comm.CommandType = CommandType.Text;
        PaisBean Empresa_Temporal = null;
        try
        {
            #region Obtener Detalle de Transacciones de la Sesion
            ArrayList Arr_Detalle_Transacciones_Sesion = Contabilizacion_Automatica_CAD.Obtener_Detalle_Transacciones_Sesion_SCA(sesionID);
            if (Arr_Detalle_Transacciones_Sesion == null)
            {
                resultado = new ArrayList();
                resultado.Add("0");
                resultado.Add("Existio un error al momento de Obtener el Detalle de Transacciones de la Sesion");
                return resultado;
            }
            #endregion
            ArrayList Arr_Grupo_X_Contabilizar = null;
            for (int a = 0; a < (Arr_Detalle_Transacciones_Sesion.Count); a++)
            {
                #region Generar Grupos de Transacciones a Contabilizar
                Arr_Grupo_X_Contabilizar = new ArrayList();
                RE_GenericBean Bean_Transaccion_A = (RE_GenericBean)Arr_Detalle_Transacciones_Sesion[a];
                if (Bean_Transaccion_A.strC56 == "FALSE")//No a sido agrupado para Contabilizar
                {
                    int grupo_A = 0;
                    int ttrID_A = 0;
                    int monID_A = 0;
                    int tpiID_A = 0;
                    int personaID_A = 0;
                    string allIN_A = "";
                    string tipoFACTURA_A = "";
                    string Factura_PROVEEDOR_A = "";
                    int empresa_A = 0;

                    grupo_A = int.Parse(Bean_Transaccion_A.strC43);//trt_grupo_id
                    ttrID_A = int.Parse(Bean_Transaccion_A.strC8);//trt_ttr_id
                    monID_A = int.Parse(Bean_Transaccion_A.strC20);//trt_ttm_id
                    tpiID_A = int.Parse(Bean_Transaccion_A.strC10);//trt_tpi_id
                    personaID_A = int.Parse(Bean_Transaccion_A.strC12);//trt_persona_id
                    allIN_A = Bean_Transaccion_A.strC42;//All In
                    tipoFACTURA_A = Bean_Transaccion_A.strC63;//trt_ttf_id
                    Factura_PROVEEDOR_A = Bean_Transaccion_A.strC60 + "-" + Bean_Transaccion_A.strC61;//trt_proveedor_serie - trt_proveedor_correlativo
                    empresa_A = int.Parse(Bean_Transaccion_A.strC50);//trt_empresa_id

                    Bean_Transaccion_A.strC56 = "TRUE";//Actualizo el Bean para indicar que esta Transaccion ya ha sido agregada para Contabilizar con su grupo
                    Arr_Detalle_Transacciones_Sesion[a] = Bean_Transaccion_A;
                    Arr_Grupo_X_Contabilizar.Add(Bean_Transaccion_A);//Agrego al Arreglo de Transacciones a Contabilizar del Grupo en Curso

                    #region Validar Transaccion B
                    for (int b = a + 1; b < (Arr_Detalle_Transacciones_Sesion.Count); b++)
                    {
                        RE_GenericBean Bean_Transaccion_B = (RE_GenericBean)Arr_Detalle_Transacciones_Sesion[b];
                        if (Bean_Transaccion_B.strC56 == "FALSE")//No a sido agrupado para Contabilizar
                        {

                            int grupo_B = 0;
                            int ttrID_B = 0;
                            int monID_B = 0;
                            int tpiID_B = 0;
                            int personaID_B = 0;
                            string allIN_B = "";
                            string tipoFACTURA_B = "";
                            string Factura_PROVEEDOR_B = "";
                            int empresa_B = 0;

                            grupo_B = int.Parse(Bean_Transaccion_B.strC43);//trt_grupo_id
                            ttrID_B = int.Parse(Bean_Transaccion_B.strC8);//trt_ttr_id
                            monID_B = int.Parse(Bean_Transaccion_B.strC20);//trt_ttm_id
                            tpiID_B = int.Parse(Bean_Transaccion_B.strC10);//trt_tpi_id
                            personaID_B = int.Parse(Bean_Transaccion_B.strC12);//trt_persona_id
                            allIN_B = Bean_Transaccion_B.strC42;//All In
                            tipoFACTURA_B = Bean_Transaccion_B.strC63;//trt_ttf_id
                            Factura_PROVEEDOR_B = Bean_Transaccion_B.strC60 + "-" + Bean_Transaccion_B.strC61;//trt_proveedor_serie - trt_proveedor_correlativo
                            empresa_B = int.Parse(Bean_Transaccion_B.strC50);//trt_empresa_id

                            if (ttrID_A == 1)
                            {
                                if ((allIN_A == allIN_B) && (grupo_A == grupo_B) && (ttrID_A == ttrID_B) && (monID_A == monID_B) && (tpiID_A == tpiID_B) && (personaID_A == personaID_B) && (tipoFACTURA_A == tipoFACTURA_B) && (empresa_A == empresa_B))
                                {
                                    //Si la Transaccion A y B son del mismo Grupo, Son Facturas con el Mismo All IN, en la misma Moneda, con el mismo Tipo de Persona y Codigo las agrupo para contabilizar
                                    Bean_Transaccion_B.strC56 = "TRUE";//Actualizo el Bean para indicar que esta Transaccion ya ha sido agregada para Contabilizar con su grupo
                                    Arr_Detalle_Transacciones_Sesion[b] = Bean_Transaccion_B;
                                    Arr_Grupo_X_Contabilizar.Add(Bean_Transaccion_B);//Agrego al Arreglo de Transacciones a Contabilizar del Grupo en Curso
                                }
                            }
                            else if (ttrID_A == 5)
                            {
                                if ((Factura_PROVEEDOR_A == Factura_PROVEEDOR_B) && (grupo_A == grupo_B) && (ttrID_A == ttrID_B) && (monID_A == monID_B) && (tpiID_A == tpiID_B) && (personaID_A == personaID_B) && (empresa_A == empresa_B))
                                {
                                    //Si la Transaccion A y B son del mismo Grupo, Tienen la Misma Factura de Proveedor, en la misma Moneda, con el mismo Tipo de Persona y Codigo las agrupo para contabilizar
                                    Bean_Transaccion_B.strC56 = "TRUE";//Actualizo el Bean para indicar que esta Transaccion ya ha sido agregada para Contabilizar con su grupo
                                    Arr_Detalle_Transacciones_Sesion[b] = Bean_Transaccion_B;
                                    Arr_Grupo_X_Contabilizar.Add(Bean_Transaccion_B);//Agrego al Arreglo de Transacciones a Contabilizar del Grupo en Curso
                                }
                            }
                            else
                            {
                                if ((grupo_A == grupo_B) && (ttrID_A == ttrID_B) && (monID_A == monID_B) && (tpiID_A == tpiID_B) && (personaID_A == personaID_B))
                                {
                                    //Si la Transaccion A y B son del mismo Grupo, Documento, en la misma Moneda, con el mismo Tipo de Persona y Codigo las agrupo para contabilizar
                                    Bean_Transaccion_B.strC56 = "TRUE";//Actualizo el Bean para indicar que esta Transaccion ya ha sido agregada para Contabilizar con su grupo
                                    Arr_Detalle_Transacciones_Sesion[b] = Bean_Transaccion_B;
                                    Arr_Grupo_X_Contabilizar.Add(Bean_Transaccion_B);//Agrego al Arreglo de Transacciones a Contabilizar del Grupo en Curso
                                }
                            }
                        }
                    }
                    #endregion
                    #region Enviar a Contabilizar cada Tipo de Transaccion
                    if (ttrID_A == 1)
                    {
                        #region Construir Factura
                        resultado = new ArrayList();
                        resultado = Contabilizacion_Automatica_CAD.Construir_Factura_SCA(sesionID, Bean_Transaccion_A, Arr_Grupo_X_Contabilizar);
                        if ((resultado[0].ToString() == "0") || (resultado[0].ToString() == "-100"))
                        {
                            transaccion.Rollback();
                            return resultado;
                        }
                        #endregion
                        #region Insertar_Factura
                        Bean_Factura_Automatica Factura_Automatica = (Bean_Factura_Automatica)resultado[1];
                        Empresa_Temporal = null;
                        Empresa_Temporal = DB.getPais(Factura_Automatica.tfa_pai_id);
                        resultado = new ArrayList();
                        resultado = Contabilizacion_Automatica_CAD.Insertar_Factura(conn, comm, transaccion, Empresa_Temporal, Factura_Automatica.tfa_conta_id, Factura_Automatica.tfa_suc_id, Factura_Automatica);
                        if (resultado[0].ToString() == "0")
                        {
                            transaccion.Rollback();
                            return resultado;
                        }
                        #endregion
                        #region Agregar Facturas a marcar en Trafico
                        if (Factura_Automatica.tfa_tpi_id == 3)
                        {
                            #region Factura Cliente
                            Arr_Facturas_Cliente_Generadas.Add(Factura_Automatica);
                            #endregion
                        }
                        #endregion
                        #region Marcar Transaccion con su REF_ID
                        resultado = Contabilizacion_Automatica_CAD.Marcar_Transaccion_SCA_Con_REFID(sesionID, resultado, Arr_Grupo_X_Contabilizar, 1);
                        if (resultado[0].ToString() == "0")
                        {
                            transaccion.Rollback();
                            return resultado;
                        }
                        #endregion
                    }
                    else if (ttrID_A == 4)
                    {
                        #region Construir Nota de Debito
                        resultado = new ArrayList();
                        resultado = Contabilizacion_Automatica_CAD.Construir_Nota_Debito_SCA(sesionID, Bean_Transaccion_A, Arr_Grupo_X_Contabilizar);
                        if ((resultado[0].ToString() == "0") || (resultado[0].ToString() == "-100"))
                        {
                            transaccion.Rollback();
                            return resultado;
                        }
                        #endregion
                        #region Insertar_Nota_Debito
                        Bean_Nota_Debito_Automatica Nota_Debito_Automatica = (Bean_Nota_Debito_Automatica)resultado[1];
                        Empresa_Temporal = null;
                        Empresa_Temporal = DB.getPais(Nota_Debito_Automatica.tnd_pai_id);
                        resultado = new ArrayList();
                        resultado = Contabilizacion_Automatica_CAD.Insertar_Nota_Debito(conn, comm, transaccion, Empresa_Temporal, Nota_Debito_Automatica.tnd_tcon_id, Nota_Debito_Automatica.tnd_suc_id, Nota_Debito_Automatica);
                        if (resultado[0].ToString() == "0")
                        {
                            transaccion.Rollback();
                            return resultado;
                        }
                        #endregion
                        #region Agregar Notas de Debito a marcar en Trafico
                        if (Nota_Debito_Automatica.tnd_tpi_id == 3)
                        {
                            #region Notas de Debito Cliente Cliente
                            Arr_Notas_Debito_Cliente_Generadas.Add(Nota_Debito_Automatica);
                            #endregion
                        }
                        #endregion
                        #region Marcar Transaccion con su REF_ID
                        resultado = Contabilizacion_Automatica_CAD.Marcar_Transaccion_SCA_Con_REFID(sesionID, resultado, Arr_Grupo_X_Contabilizar, 4);
                        if (resultado[0].ToString() == "0")
                        {
                            transaccion.Rollback();
                            return resultado;
                        }
                        #endregion
                    }
                    else if (ttrID_A == 5)
                    {
                        #region Construir Provision
                        resultado = new ArrayList();
                        if (Bean_Transaccion_A.strC10 != "10")//trt_tpi_id
                        {
                            //Provision Agente, Naviera, Proveedores
                            resultado = (ArrayList)Contabilizacion_Automatica_CAD.Construir_Provision_SCA(sesionID, Bean_Transaccion_A, Arr_Grupo_X_Contabilizar);
                        }
                        else if (Bean_Transaccion_A.strC10 == "10")//trt_tpi_id
                        {
                            //Provision Intercompany
                            resultado = (ArrayList)Contabilizacion_Automatica_CAD.Construir_Provision_Intercompany_SCA(sesionID, Bean_Transaccion_A, Arr_Grupo_X_Contabilizar);
                        }
                        if ((resultado[0].ToString() == "0") || (resultado[0].ToString() == "-100"))
                        {
                            transaccion.Rollback();
                            return resultado;
                        }
                        #endregion
                        #region Insertar_Provision
                        Bean_Provision_Automatica Provision_Automatica = (Bean_Provision_Automatica)resultado[1];
                        Empresa_Temporal = null;
                        Empresa_Temporal = DB.getPais(Provision_Automatica.tpr_pai_id);
                        resultado = new ArrayList();
                        resultado = Contabilizacion_Automatica_CAD.Insertar_Provision(conn, comm, transaccion, Empresa_Temporal, Provision_Automatica.tpr_tcon_id, Provision_Automatica.tpr_suc_id, Provision_Automatica);
                        if (resultado[0].ToString() == "0")
                        {
                            transaccion.Rollback();
                            return resultado;
                        }
                        #endregion
                        #region Marcar Transaccion con su REF_ID
                        resultado = Contabilizacion_Automatica_CAD.Marcar_Transaccion_SCA_Con_REFID(sesionID, resultado, Arr_Grupo_X_Contabilizar, 5);
                        if (resultado[0].ToString() == "0")
                        {
                            transaccion.Rollback();
                            return resultado;
                        }
                        #endregion
                        #region Validar Provision Intercompany Encadenada
                        ArrayList Arr_Provision_Automatica_Temporal = null;
                        Arr_Provision_Automatica_Temporal = (ArrayList)resultado[2];
                        Bean_Provision_Automatica Provision_Automatica_Temporal = (Bean_Provision_Automatica)Arr_Provision_Automatica_Temporal[2];
                        if (Provision_Automatica_Temporal.tpr_tpi_id == 10)
                        {
                            int grupo_Temporal = int.Parse(Bean_Transaccion_A.strC43);//trt_grupo_id
                            RE_GenericBean Transaccion_Padre_Temporal = Contabilizacion_Automatica_CAD.Obtener_Transaccion_Padre_SCA_Intercompany_Administrativo(sesionID, grupo_Temporal, 1);
                            #region Insertar Log Transacciones Encadenadas
                            RE_GenericBean Bean_Log = new RE_GenericBean();
                            Bean_Log.intC1 = Transaccion_Padre_Temporal.intC1;//ttel_padre_ttr_id
                            Bean_Log.intC2 = Transaccion_Padre_Temporal.intC2;//ttel_padre_ref_id
                            Bean_Log.intC3 = 5;//ttel_hijo_ttr_id
                            Bean_Log.intC4 = Provision_Automatica_Temporal.tpr_prov_id;//ttel_hijo_ref_id
                            Bean_Log.intC5 = 1;//ttel_tta_id
                            Bean_Log.strC1 = Provision_Automatica_Temporal.tpr_usu_acepta;//ttel_usuario_emision
                            Bean_Log.intC6 = Transaccion_Padre_Temporal.intC3;//ttel_padre_empresa_id
                            Bean_Log.intC7 = Provision_Automatica.tpr_pai_id;//ttel_hijo_empresa_id
                            int resutaldo_log_transacciones = 0;
                            resutaldo_log_transacciones = Contabilizacion_Automatica_CAD.Insertar_Log_Transacciones_Encadenadas(Bean_Log);
                            #endregion
                            Provision_Automatica_Temporal = null;
                            Transaccion_Padre_Temporal = null;
                            grupo_Temporal = 0;
                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion
            }
            #region Finalizar Contabilizacion
            #region Marcar Traficos
            //Comentado para que las Pruebas no Marquen los Rubros en los Sistemas de Trafico
            #region Facturas
            foreach (Bean_Factura_Automatica Factura_Automatica in Arr_Facturas_Cliente_Generadas)
            {
                #region Marcar Traficos con Facturas
                PaisBean Pais_Temporal = new PaisBean();
                Pais_Temporal = DB.getPais(Factura_Automatica.tfa_pai_id);
                UsuarioBean user_facturas = new UsuarioBean();
                user_facturas.ID = Factura_Automatica.tfa_usu_id;
                user_facturas.pais = Pais_Temporal;
                int update_result = 0;
                foreach (Bean_Detalle_Rubros Cargos in Factura_Automatica.Arr_Detalle_Facturacion)
                {
                    if (Factura_Automatica.tfa_tto_id == 1)//FCL
                    {
                        update_result = DB.Update_Cargos_Traficos(user_facturas, Factura_Automatica.tfa_tto_id, 1, Cargos.tdf_cargo_id, Factura_Automatica.tfa_blid, Factura_Automatica.tfa_id, 1, "F", 1);
                    }
                    else if (Factura_Automatica.tfa_tto_id == 2)//LCL
                    {
                        update_result = DB.Update_Cargos_Traficos(user_facturas, Factura_Automatica.tfa_tto_id, 1, Cargos.tdf_cargo_id, Factura_Automatica.tfa_blid, Factura_Automatica.tfa_id, 1, "L", 1);
                    }
                    else//AEREO - TERRESTRE
                    {
                        update_result = DB.Update_Cargos_Traficos(user_facturas, Factura_Automatica.tfa_tto_id, 1, Cargos.tdf_cargo_id, Factura_Automatica.tfa_blid, Factura_Automatica.tfa_id, 1, "", 1);
                    }
                    if (update_result == -100)
                    {
                        transaccion.Rollback();
                        return null;
                    }
                }
                #endregion
            }
            #endregion
            #region Notas de Debito
            foreach (Bean_Nota_Debito_Automatica Nota_Debito_Automatica in Arr_Notas_Debito_Cliente_Generadas)
            {
                #region Marcar Traficos con Notas de Debito
                PaisBean Pais_Temporal2 = new PaisBean();
                Pais_Temporal2 = DB.getPais(Nota_Debito_Automatica.tnd_pai_id);
                UsuarioBean user_nota_debito = new UsuarioBean();
                user_nota_debito.ID = Nota_Debito_Automatica.tnd_usu_id;
                user_nota_debito.pais = Pais_Temporal2;
                int update_result = 0;
                foreach (Bean_Detalle_Rubros Cargos in Nota_Debito_Automatica.Arr_Detalle_Facturacion)
                {
                    if (Nota_Debito_Automatica.tnd_tto_id == 1)//FCL
                    {
                        update_result = DB.Update_Cargos_Traficos(user_nota_debito, Nota_Debito_Automatica.tnd_tto_id, 1, Cargos.tdf_cargo_id, Nota_Debito_Automatica.tnd_blid, Nota_Debito_Automatica.tnd_id, 4, "F", 1);
                    }
                    else if (Nota_Debito_Automatica.tnd_tto_id == 2)//LCL
                    {
                        update_result = DB.Update_Cargos_Traficos(user_nota_debito, Nota_Debito_Automatica.tnd_tto_id, 1, Cargos.tdf_cargo_id, Nota_Debito_Automatica.tnd_blid, Nota_Debito_Automatica.tnd_id, 4, "L", 1);
                    }
                    else//AEREO - TERRESTRE
                    {
                        update_result = DB.Update_Cargos_Traficos(user_nota_debito, Nota_Debito_Automatica.tnd_tto_id, 1, Cargos.tdf_cargo_id, Nota_Debito_Automatica.tnd_blid, Nota_Debito_Automatica.tnd_id, 4, "", 1);
                    }
                    if (update_result == -100)
                    {
                        transaccion.Rollback();
                        return null;
                    }
                }
                #endregion
            }
            #endregion
            #endregion
            transaccion.Commit();
            #region Asociar Transacciones Intercompany
            ArrayList Arr_Transacciones_Intercompany = (ArrayList)Contabilizacion_Automatica_CAD.Get_Cobros_Intercompany_Sesion_Contabilizacion_Automatica(sesionID);
            foreach (RE_GenericBean Bean_Transaccion_Intercompany in Arr_Transacciones_Intercompany)
            {
                int provID_AUX = 0;
                provID_AUX = (int)Contabilizacion_Automatica_CAD.Get_Transaccion_Intercompany_Hija_Sesion_Contabilizacion_Automatica(Bean_Transaccion_Intercompany.intC1, Bean_Transaccion_Intercompany.intC2);
                if (provID_AUX > 0)
                {
                    resultado = (ArrayList)Contabilizacion_Automatica_CAD.Marcar_Pago_Intercompany_SCA_Con_Documento_Cobro(provID_AUX, Bean_Transaccion_Intercompany.strC1, Bean_Transaccion_Intercompany.strC2);
                    if (resultado[0].ToString() == "0")
                    {
                        transaccion.Rollback();
                        return resultado;
                    }
                }
            }
            #endregion
            resultado = new ArrayList();
            resultado.Add("1");
            resultado.Add("Todo el Embarque fue Contabilizado Exitosamente");
            #endregion
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            transaccion.Rollback();
            return null;
        }
        finally
        {
            DB.CloseObj(reader, comm, conn);
        }
        return resultado;
    }
    //INICIO
    public static ArrayList Validar_Cambios_Al_Embarque_SCA(int sesionID)
    {
        ArrayList Arr_Result = new ArrayList();
        int resultado = 1;
        string mensaje = "";
        try
        {
            RE_GenericBean Bean_Sesion = Contabilizacion_Automatica_CAD.Obtener_Detalle_Sesion_Reconciliacon_Carga(sesionID);
            PaisBean Empresa_Bean = DB.getPais(int.Parse(Bean_Sesion.strC2));
            ArrayList Arr_BLs_Sesion = Contabilizacion_Automatica_CAD.Obtener_Detalle_BLs_Reconciliacion_Carga(sesionID);
            ArrayList Arr_BLs_Trafico = Contabilizacion_Automatica_CAD.Get_BLs_X_Traficos_X_Master(int.Parse(Bean_Sesion.strC4), int.Parse(Bean_Sesion.strC6), int.Parse(Bean_Sesion.strC8), Empresa_Bean, Bean_Sesion.strC10);
            ArrayList Arr_BLs_Trafico_Temporal = new ArrayList();

            #region Generar Detalle de BL's con informacion Actual de Trafico
            RE_GenericBean Bean_Temporal = null;
            Bean_Temporal = new RE_GenericBean();
            Bean_Temporal.strC1 = "M";//trb_tipo_bl
            Bean_Temporal.strC2 = Bean_Sesion.strC8;//trb_bl_id
            Bean_Temporal.strC3 = Bean_Sesion.strC10;//trb_bl
            Bean_Temporal.strC4 = "0";//trb_routing_id
            Bean_Temporal.strC5 = "";//trb_routing
            Bean_Temporal.strC6 = "0";//trb_contenedor_id
            Bean_Temporal.strC7 = "";//trb_contenedor
            Bean_Temporal.strC8 = "0";//trb_peso
            Bean_Temporal.strC9 = "0";//trb_volumen
            Bean_Temporal.strC10 = "0";//trb_cli_id
            Bean_Temporal.strC11 = "";//trb_destino
            Bean_Temporal.strC12 = sesionID.ToString();//trb_trs_id
            Bean_Temporal.strC13 = "FALSE";//trb_to_order
            Bean_Temporal.strC14 = "0";//trb_to_order_id
            Arr_BLs_Trafico_Temporal.Add(Bean_Temporal);
            foreach (Bean_Datos_BL BL in Arr_BLs_Trafico)
            {
                Bean_Temporal = new RE_GenericBean();
                Bean_Temporal.strC1 = "H";//trb_tipo_bl
                Bean_Temporal.strC2 = BL.BLID.ToString();//trb_bl_id
                Bean_Temporal.strC3 = BL.Hbl;//trb_bl
                Bean_Temporal.strC4 = BL.RoutingID.ToString();//trb_routing_id
                Bean_Temporal.strC5 = BL.Routing.Trim();//trb_routing
                Bean_Temporal.strC6 = BL.ContenedorID.ToString();//trb_contenedor_id
                Bean_Temporal.strC7 = BL.Contenedor.Trim();//trb_contenedor
                Bean_Temporal.strC8 = BL.Peso;//trb_peso
                Bean_Temporal.strC9 = BL.Volumen;//trb_volumen
                Bean_Temporal.strC10 = BL.Cliente.ToString();//trb_cli_id
                Bean_Temporal.strC13 = "FALSE";
                Bean_Temporal.strC14 = "0";//trb_to_order_id
                Bean_Temporal.strC11 = BL.Destino_Final.Trim();//trb_destino
                Bean_Temporal.strC12 = sesionID.ToString();//trb_trs_id
                Arr_BLs_Trafico_Temporal.Add(Bean_Temporal);
            }
            #endregion
            #region Definicion de Variables de Validacion
            string tipo_BL_Trafico = "";
            string tipo_BL_Sesion = "";
            string blID_Trafico = "";
            string blID_Sesion = "";
            string noBL_Trafico = "";
            string noBL_Sesion = "";
            string routingID_Trafico = "";
            string routingID_Sesion = "";
            string contenedorID_Trafico = "";
            string contenedorID_Sesion = "";
            string peso_Trafico = "";
            string peso_Sesion = "";
            string volumen_Trafico = "";
            string volumen_Sesion = "";
            string cliID_Trafico = "";
            string cliID_Sesion = "";
            string destino_Trafico = "";
            string destino_Sesion = "";
            string agenteID_Trafico = "";
            string agenteID_Sesion = "";
            string navieraID_Trafico = "";
            string navieraID_Sesion = "";
            string to_ORDER_Sesion = "";
            string to_ORDERID_Sesion = "";
            #endregion

            foreach (RE_GenericBean Bean_BL_Trafico in Arr_BLs_Trafico_Temporal)
            {
                tipo_BL_Trafico = Bean_BL_Trafico.strC1;
                blID_Trafico = Bean_BL_Trafico.strC2;
                noBL_Trafico = Bean_BL_Trafico.strC3;

                if (tipo_BL_Trafico == "M")
                {
                    #region Validar Datos del Master
                    Bean_Datos_BL Datos_Trafico = Contabilizacion_Automatica_CAD.Get_DatosBL_X_Traficos(int.Parse(Bean_Sesion.strC4), int.Parse(Bean_Sesion.strC6), int.Parse(Bean_Sesion.strC8), Empresa_Bean);
                    if (Datos_Trafico.BLID.ToString() == Bean_Sesion.strC8)
                    {
                        agenteID_Trafico = Datos_Trafico.Agente.ToString();
                        agenteID_Sesion = Bean_Sesion.strC11;
                        if (agenteID_Trafico != agenteID_Sesion)
                        {
                            resultado = 0;
                            mensaje += "El Agente del embarque ha sido modificado (T=" + agenteID_Trafico + " - S=" + agenteID_Sesion + ")  <br>";
                        }

                        navieraID_Trafico = Datos_Trafico.Naviera.ToString();
                        navieraID_Sesion = Bean_Sesion.strC12;
                        if (navieraID_Trafico != navieraID_Sesion)
                        {
                            resultado = 0;
                            mensaje += "La Naviera del embarque ha sido modificada (T=" + navieraID_Trafico + " - S=" + navieraID_Sesion + ")  <br>";
                        }
                    }
                    #endregion
                }

                foreach (RE_GenericBean Bean_BL_Sesion in Arr_BLs_Sesion)
                {
                    tipo_BL_Sesion = Bean_BL_Sesion.strC2;
                    blID_Sesion = Bean_BL_Sesion.strC3;
                    noBL_Sesion = Bean_BL_Sesion.strC4;

                    if ((tipo_BL_Trafico == "H") && (tipo_BL_Sesion == "H"))
                    {
                        #region Validar Datos de los Houses
                        if (blID_Trafico == blID_Sesion)
                        {

                            routingID_Trafico = Bean_BL_Trafico.strC4;
                            routingID_Sesion = Bean_BL_Sesion.strC5;
                            if (routingID_Trafico != routingID_Sesion)
                            {
                                resultado = 0;
                                mensaje += noBL_Sesion + ".: El Routing ha sido modificado <br>";
                            }

                            contenedorID_Trafico = Bean_BL_Trafico.strC6;
                            contenedorID_Sesion = Bean_BL_Sesion.strC7;
                            if (contenedorID_Trafico != contenedorID_Sesion)
                            {
                                resultado = 0;
                                mensaje += noBL_Sesion + ".: El contenedor ha sido modificado <br>";
                            }

                            peso_Trafico = Bean_BL_Trafico.strC8;
                            peso_Sesion = Bean_BL_Sesion.strC9;
                            if (peso_Trafico != peso_Sesion)
                            {
                                resultado = 0;
                                mensaje += noBL_Sesion + ".: El peso ha sido modificado (T=" + peso_Trafico + " - S=" + peso_Sesion + ") <br>";
                            }

                            volumen_Trafico = Bean_BL_Trafico.strC9;
                            volumen_Sesion = Bean_BL_Sesion.strC10;
                            if (volumen_Trafico != volumen_Sesion)
                            {
                                resultado = 0;
                                mensaje += noBL_Sesion + ".: El volumen ha sido modificado (T= " + volumen_Trafico + " - S=" + volumen_Sesion + ") <br>";
                            }

                            cliID_Trafico = Bean_BL_Trafico.strC10;
                            cliID_Sesion = Bean_BL_Sesion.strC11;
                            to_ORDER_Sesion = Bean_BL_Sesion.strC15;
                            to_ORDERID_Sesion = Bean_BL_Sesion.strC16;
                            if (to_ORDER_Sesion == "True")
                            {
                                if (cliID_Trafico != to_ORDERID_Sesion)
                                {
                                    resultado = 0;
                                    mensaje += noBL_Sesion + ".: El cliente ha sido modificado (T= " + cliID_Trafico + " - S=" + to_ORDERID_Sesion + ") <br>";
                                }
                            }
                            else
                            {
                                if (cliID_Trafico != cliID_Sesion)
                                {
                                    resultado = 0;
                                    mensaje += noBL_Sesion + ".: El cliente ha sido modificado (T= " + cliID_Trafico + " - S=" + cliID_Sesion + ") <br>";
                                }

                            }
                            destino_Trafico = Bean_BL_Trafico.strC11;
                            destino_Sesion = Bean_BL_Sesion.strC12;
                            if (destino_Trafico != destino_Sesion)
                            {
                                resultado = 0;
                                mensaje += noBL_Sesion + ".: El destino final ha sido modificado (T=" + destino_Trafico + " - S=" + destino_Sesion + ") <br>";
                            }

                        }
                        #endregion
                    }
                }
            }
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        Arr_Result.Add(resultado.ToString());
        Arr_Result.Add(mensaje);
        return Arr_Result;
    }
}