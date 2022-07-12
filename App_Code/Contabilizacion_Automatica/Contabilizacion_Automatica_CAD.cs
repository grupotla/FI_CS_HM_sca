using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Collections;
using System.Data;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for Contabilizacion_Automatica_CAD
/// </summary>
public class Contabilizacion_Automatica_CAD
{
    public static string[,] M_Monedas = new string[6, 2] { { "1", "GTQ" }, { "3", "LPS" }, { "4", "NIO" }, { "5", "CRC" }, { "7", "BZD" }, { "8", "USD" } };
    public static string[,] M_Tipo_Persona = new string[4, 3] { { "Linea Aerea", "0", "6" }, { "Agente", "1", "2" }, { "Naviera", "2", "5" }, { "Proveedor", "3", "4" } };//Tipo-ID_Master,ID_BAW
    public static string[,] M_Pais_Impuestos = new string[16, 3] { { "1", "1", "2" }, { "1", "2", "1" }, { "2", "1", "2" }, { "3", "1", "2" }, { "3", "2", "1" }, { "4", "1", "2" }, { "5", "1", "2" }, { "5", "2", "2" }, { "6", "1", "2" }, { "11", "1", "2" }, { "11", "2", "1" }, { "7", "1", "2" }, { "7", "2", "1" }, { "15", "1", "2" }, { "15", "2", "1" }, { "9", "1", "2" } };//PAISID-CONTABILIDAD-ImpuestoProveedor: 1=EXCENCETO 2=CONTRIBUYENTE
    public static string[,] M_Contabilidad = new string[16, 3] { { "1", "GTQ", "1" }, { "1", "USD", "2" }, { "2", "USD", "1" }, { "3", "LPS", "1" }, { "3", "USD", "2" }, { "4", "USD", "1" }, { "5", "CRC", "1" }, { "5", "USD", "2" }, { "6", "USD", "1" }, { "11", "NIO", "1" }, { "11", "USD", "2" }, { "7", "BZD", "1" }, { "7", "USD", "2" }, { "15", "GTQ", "1" }, { "15", "USD", "2" }, { "9", "USD", "1" } };//PAISID-MONEDA-CONTA
	public Contabilizacion_Automatica_CAD()
	{
	}
    protected static int Obtener_Llave_Primaria(NpgsqlConnection conn, NpgsqlCommand comm, NpgsqlTransaction Transaction, int ttrID)
    {
        #region Obtener Llave Primaria
        int PK = 0;
        if (ttrID == 1)
        {
            comm.CommandText = "select nextval('id_no_factura')";
            PK = int.Parse(comm.ExecuteScalar().ToString());
        }
        else if (ttrID == 3)
        {
            comm.CommandText = "select nextval('id_nota_credito')";
            PK = int.Parse(comm.ExecuteScalar().ToString());
        }
        else if (ttrID == 4)
        {
            comm.CommandText = "select nextval('id_nota_debito')";
            PK = int.Parse(comm.ExecuteScalar().ToString());
        }
        else if (ttrID == 5)
        {
            comm.CommandText = "select nextval('id_provision')";
            PK = int.Parse(comm.ExecuteScalar().ToString());
        }
        return PK;
        #endregion
    }
    protected static string Generar_Numero_Partida(NpgsqlConnection conn, NpgsqlCommand comm, NpgsqlTransaction Transaction, int paiID, string paisISO, int contaID)
    {
        #region Generar Numero de Partida
        string Numero_Partida = "";
        int Correlativo_Partida = 0;
        //Obtener Correlativo Partida
        comm.Parameters.Add("@paiID", NpgsqlTypes.NpgsqlDbType.Integer).Value = paiID;
        comm.Parameters.Add("@tconID", NpgsqlTypes.NpgsqlDbType.Integer).Value = contaID;
        Correlativo_Partida = int.Parse(comm.ExecuteScalar().ToString());
        //Generar Numero de Partida
        Numero_Partida = Utility.GeneroPartida(paisISO, Correlativo_Partida, contaID);
        Correlativo_Partida += 1;
        //Actualizar el correlativo de la Partida
        comm.CommandText = "update sys_partidas_value set spv_value=" + Correlativo_Partida + " where spv_pai_id=" + paiID + " and spv_tcon_id=" + contaID + "";
        comm.ExecuteNonQuery();
        comm.Parameters.Clear();
        return Numero_Partida;
        #endregion
    }
    protected static int Actualizar_Correlativo_Partida(NpgsqlConnection conn, NpgsqlCommand comm, NpgsqlTransaction Transaction, int Numero_Partida, int paiID, int contaID)
    {
        #region Actualizo el correlativo de la partida
        int result = 0;
        comm.CommandText = "update sys_partidas_value set spv_value=" + Numero_Partida + " where spv_pai_id=" + paiID + " and spv_tcon_id=" + contaID + "";
        result = comm.ExecuteNonQuery();
        comm.Parameters.Clear();
        return result;
        #endregion
    }
    protected static string Obtener_Fecha_Arranque(string schema)
    {
        #region Obtener Fecha de Arranque
        string Fecha_Arranque = "";
        if (schema == "ventas_gt")
        {
            Fecha_Arranque = "2011/11/07";
        }
        else if (schema == "ventas_cr")
        {
            Fecha_Arranque = "2011/11/13";
        }
        else if (schema == "ventas_pa")
        {
            Fecha_Arranque = "2012/01/30";
        }
        else if (schema == "ventas_hn")
        {
            Fecha_Arranque = "2012/02/08";
        }
        else if (schema == "ventas_sv")
        {
            Fecha_Arranque = "2012/03/28";
        }
        else if (schema == "ventas_ni")
        {
            Fecha_Arranque = "2012/11/14";
        }
        else if (schema == "ventas_ni_grh")
        {
            Fecha_Arranque = "2012/11/14";
        }
        else if (schema == "ventas_bz")
        {
            Fecha_Arranque = "2012/12/07";
        }
        else if (schema == "ventas_gtltf")
        {
            Fecha_Arranque = "2013/01/01";
        }
        else if (schema == "ventas_crltf")
        {
            Fecha_Arranque = "2013/08/01";
        }
        else if (schema == "ventas_paltf")
        {
            Fecha_Arranque = "2013/08/01";
        }
        else if (schema == "ventas_gtapl")
        {
            Fecha_Arranque = "2013/09/01";
        }
        else if (schema == "ventas_niapl")
        {
            Fecha_Arranque = "2013/09/01";
        }
        else if (schema == "ventas_niltf")
        {
            Fecha_Arranque = "2013/10/22";
        }
        else if (schema == "ventas_svapl")
        {
            Fecha_Arranque = "2013/11/23";
        }
        else if (schema == "ventas_niapl")
        {
            Fecha_Arranque = "2013/11/23";
        }
        else if (schema == "ventas_svltf")
        {
            Fecha_Arranque = "2014/01/01";
        }
        else if (schema == "ventas_hnltf")
        {
            Fecha_Arranque = "2014/05/01";
        }
        return Fecha_Arranque;
        #endregion
    }
    public static bool Existe_Pais(string ID)
    {
        #region Validar que exista el Pais
        ArrayList Arr = (ArrayList)DB.getPaises("");
        bool Resultado = false;
        foreach (PaisBean Pais in Arr)
        {
            if (Pais.ID.ToString() == ID)
            {
                Resultado = true;
            }
        }
        return Resultado;
        #endregion
    }
    public static PaisBean Cargar_Pais(string ID)
    {
        #region Cargar Datos del Pais
        PaisBean Pais_Bean = new PaisBean();
        ArrayList Arr = (ArrayList)DB.getPaises("");
        foreach (PaisBean Pais in Arr)
        {
            if (Pais.ID.ToString() == ID)
            {
                Pais_Bean = Pais;
                Pais_Bean.TipoCambio = DB.getTipoCambioHoy(Pais_Bean.ID);
            }
        }
        return Pais_Bean;
        #endregion
    }
    public static bool Existe_Tipo_Cambio(string ID)
    {
        #region Validar que exista Tipo de Cambio
        bool Resultado = false;
        decimal Tipo_Cambio = 0;
        Tipo_Cambio = DB.getTipoCambioHoy(int.Parse(ID));
        if (Tipo_Cambio > 0)
        {
            Resultado = true;
        }
        return Resultado;
        #endregion
    }
    protected int Traducir_Tipo_Moneda(string Moneda)
    {
        #region Traducir Moneda
        int MonID = 0;
        for (int i = 0; i < 6; i++)
        {
            if (M_Monedas[i, 1] == Moneda)
            {
                MonID = int.Parse(M_Monedas[i, 0]);
            }
        }
        return MonID;
        #endregion
    }
    protected int Traducir_Tipo_Persona(string ID)
    {
        #region Traducir Tipo de Persona
        int TPI = 0;
        for (int i = 0; i < 4; i++)
        {
            if (M_Tipo_Persona[i, 1] == ID)
            {
                TPI = int.Parse(M_Tipo_Persona[i, 2]);
            }
        }
        return TPI;
        #endregion
    }
    private static int Definir_Cobro_Impuesto(string paiID, int contaID, int ImpuestoProveedor)
    {
        #region Definir si se Cobra o no Impuesto Dependiendo de la Contabilidad
        int resultado = 0;
        for (int i = 0; i < 15; i++)
        {
            if ((paiID == M_Pais_Impuestos[i, 0]) && (contaID.ToString() == M_Pais_Impuestos[i, 1]))
            {
                if (contaID == 1)
                {
                    resultado = ImpuestoProveedor;
                }
                else if (contaID == 2)
                {
                    if (paiID == "5")
                    {
                        resultado = ImpuestoProveedor;
                    }
                    else
                    {
                        resultado = int.Parse(M_Pais_Impuestos[i, 2]);
                    }
                }
            }
        }
        return resultado;
        #endregion
    }
    public static ArrayList Insertar_Factura(NpgsqlConnection conn, NpgsqlCommand comm, NpgsqlTransaction Transaction, PaisBean PaisBean, int contaID, int sucID, Bean_Factura_Automatica Factura_Automatica)
    {
        #region Insertar Factura
        ArrayList Arr_Result = new ArrayList();
        int result = 0;
        try
        {
            #region Definicion de Variales y parametros Iniciales
            int tfaID = 0;
            string Correlativo_STR = "";
            int Correlativo_INT = 0;
            comm.CommandText = "select nextval('id_no_factura')";
            tfaID = int.Parse(comm.ExecuteScalar().ToString());
            Factura_Automatica.tfa_id = tfaID;

            #region Obtener Correlativo
            comm.CommandText = "select max(fac_numero_inicial) from tbl_factura where fac_suc_id=" + sucID + " and fac_tipo=1 and fac_serie='" + Factura_Automatica.tfa_serie + "' ";
            Correlativo_INT = Convert.ToInt32(comm.ExecuteScalar());
            Correlativo_STR = Correlativo_INT.ToString();
            Factura_Automatica.tfa_correlativo = Correlativo_STR;
            comm.CommandText = "";
            comm.Parameters.Clear();
            #endregion

            #region Validacion Tipo de Transmision
            if (((Factura_Automatica.tfa_pai_id == 5) || (Factura_Automatica.tfa_pai_id == 21)) && (Factura_Automatica.tfa_fac_electronica == 1))
            {
                Correlativo_INT = 0;
                Factura_Automatica.tfa_correlativo = "0";
            }
            #endregion

            #endregion
            comm.CommandText = "insert into tbl_facturacion (tfa_id, tfa_correlativo, tfa_nit, tfa_nombre, tfa_direccion, tfa_fecha_emision, tfa_fecha_pago, tfa_sub_total, tfa_impuesto, tfa_total, tfa_observacion, tfa_suc_id, tfa_cli_id, tfa_moneda, tfa_ted_id, tfa_usu_id, tfa_hbl, tfa_mbl, tfa_contenedor, tfa_routing, tfa_naviera, tfa_vapor, tfa_shipper, tfa_ordenpo, tfa_consignee, tfa_comodity, tfa_paquetes, tfa_peso, tfa_volumen, tfa_dua_ingreso, tfa_dua_salida, tfa_vendedor1, tfa_vendedor2, tfa_razon, tfa_referencia, tfa_serie, tfa_id_shipper, tfa_id_consignee, tfa_pai_id, tfa_conta_id, tfa_sub_total_eq, tfa_impuesto_eq, tfa_total_eq, tfa_tie_id, tfa_ttc_id, tfa_allin, tfa_reciboaduana, tfa_cant_paquetes, tfa_agent_id, tfa_agente, tfa_recibo_agencia, tfa_valor_aduanero, tfa_ruc, tfa_giro, tfa_ttf_id, tfa_ruta_pais, tfa_ruta, tfa_observacion2, tfa_tra_id, tfa_blid, tfa_tto_id, tfa_esignature, tfa_fac_electronica, tfa_internal_reference, tfa_guid, tfa_correo_documento_electronico, tfa_referencia_correo, tfa_innerxml, tfa_fecha_batch, tfa_tti_id, tfa_correlativo_electronico, tfa_no_factura_aduana, tfa_no_embarque, tfa_tpi_id)";
            comm.CommandText += " values  (@tfa_id, @tfa_correlativo, @tfa_nit, @tfa_nombre, @tfa_direccion, @tfa_fecha_emision, @tfa_fecha_pago, @tfa_sub_total, @tfa_impuesto, @tfa_total, @tfa_observacion, @tfa_suc_id, @tfa_cli_id, @tfa_moneda, @tfa_ted_id, @tfa_usu_id, @tfa_hbl, @tfa_mbl, @tfa_contenedor, @tfa_routing, @tfa_naviera, @tfa_vapor, @tfa_shipper, @tfa_ordenpo, @tfa_consignee, @tfa_comodity, @tfa_paquetes, @tfa_peso, @tfa_volumen, @tfa_dua_ingreso, @tfa_dua_salida, @tfa_vendedor1, @tfa_vendedor2, @tfa_razon, @tfa_referencia, @tfa_serie, @tfa_id_shipper, @tfa_id_consignee, @tfa_pai_id, @tfa_conta_id, @tfa_sub_total_eq, @tfa_impuesto_eq, @tfa_total_eq, @tfa_tie_id, @tfa_ttc_id, @tfa_allin, @tfa_reciboaduana, @tfa_cant_paquetes, @tfa_agent_id, @tfa_agente, @tfa_recibo_agencia, @tfa_valor_aduanero, @tfa_ruc, @tfa_giro, @tfa_ttf_id, @tfa_ruta_pais, @tfa_ruta, @tfa_observacion2, @tfa_tra_id, @tfa_blid, @tfa_tto_id, @tfa_esignature, @tfa_fac_electronica, @tfa_internal_reference, @tfa_guid, @tfa_correo_documento_electronico, @tfa_referencia_correo, @tfa_innerxml, current_date, @tfa_tti_id, @tfa_correlativo_electronico, @tfa_no_factura_aduana, @tfa_no_embarque, @tfa_tpi_id)";
            comm.Parameters.Add("@tfa_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_id;
            comm.Parameters.Add("@tfa_correlativo", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_correlativo;
            comm.Parameters.Add("@tfa_nit", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_nit;
            comm.Parameters.Add("@tfa_nombre", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_nombre.Trim();
            comm.Parameters.Add("@tfa_direccion", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_direccion.Trim();
            comm.Parameters.Add("@tfa_fecha_emision", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = Factura_Automatica.tfa_fecha_emision;
            comm.Parameters.Add("@tfa_fecha_pago", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = Factura_Automatica.tfa_fecha_pago;
            comm.Parameters.Add("@tfa_sub_total", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Factura_Automatica.tfa_sub_total;
            comm.Parameters.Add("@tfa_impuesto", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Factura_Automatica.tfa_impuesto;
            comm.Parameters.Add("@tfa_total", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Factura_Automatica.tfa_total;
            comm.Parameters.Add("@tfa_observacion", NpgsqlTypes.NpgsqlDbType.Text).Value = Factura_Automatica.tfa_observacion;
            comm.Parameters.Add("@tfa_suc_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_suc_id;
            comm.Parameters.Add("@tfa_cli_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_cli_id;
            comm.Parameters.Add("@tfa_moneda", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_moneda;
            comm.Parameters.Add("@tfa_ted_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_ted_id;
            comm.Parameters.Add("@tfa_usu_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_usu_id;
            comm.Parameters.Add("@tfa_hbl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_hbl;
            comm.Parameters.Add("@tfa_mbl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_mbl;
            comm.Parameters.Add("@tfa_contenedor", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_contenedor;
            comm.Parameters.Add("@tfa_routing", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_routing;
            comm.Parameters.Add("@tfa_naviera", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_naviera;
            comm.Parameters.Add("@tfa_vapor", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_vapor;
            comm.Parameters.Add("@tfa_shipper", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_shipper;
            comm.Parameters.Add("@tfa_ordenpo", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_ordenpo;
            comm.Parameters.Add("@tfa_consignee", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_consignee;
            comm.Parameters.Add("@tfa_comodity", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_comodity;
            comm.Parameters.Add("@tfa_paquetes", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_paquetes;
            comm.Parameters.Add("@tfa_peso", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_peso;
            comm.Parameters.Add("@tfa_volumen", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_volumen;
            comm.Parameters.Add("@tfa_dua_ingreso", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_dua_ingreso;
            comm.Parameters.Add("@tfa_dua_salida", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_dua_salida;
            comm.Parameters.Add("@tfa_vendedor1", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_vendedor1;
            comm.Parameters.Add("@tfa_vendedor2", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_vendedor2;
            comm.Parameters.Add("@tfa_razon", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_razon;
            comm.Parameters.Add("@tfa_referencia", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_referencia;
            comm.Parameters.Add("@tfa_serie", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_serie;
            comm.Parameters.Add("@tfa_id_shipper", NpgsqlTypes.NpgsqlDbType.Double).Value = Factura_Automatica.tfa_id_shipper;
            comm.Parameters.Add("@tfa_id_consignee", NpgsqlTypes.NpgsqlDbType.Double).Value = Factura_Automatica.tfa_id_consignee;
            comm.Parameters.Add("@tfa_pai_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_pai_id;
            comm.Parameters.Add("@tfa_conta_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_conta_id;
            comm.Parameters.Add("@tfa_sub_total_eq", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Factura_Automatica.tfa_sub_total_eq;
            comm.Parameters.Add("@tfa_impuesto_eq ", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Factura_Automatica.tfa_impuesto_eq;
            comm.Parameters.Add("@tfa_total_eq ", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Factura_Automatica.tfa_total_eq;
            comm.Parameters.Add("@tfa_tie_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_tie_id;
            comm.Parameters.Add("@tfa_ttc_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_ttc_id;
            comm.Parameters.Add("@tfa_allin", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_allin;
            comm.Parameters.Add("@tfa_reciboaduana", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_reciboaduana;
            comm.Parameters.Add("@tfa_cant_paquetes", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_cant_paquetes;
            comm.Parameters.Add("@tfa_agent_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_agent_id;
            comm.Parameters.Add("@tfa_agente", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_agente;
            comm.Parameters.Add("@tfa_recibo_agencia", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_recibo_agencia;
            comm.Parameters.Add("@tfa_valor_aduanero", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_valor_aduanero;
            comm.Parameters.Add("@tfa_ruc", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_ruc;
            comm.Parameters.Add("@tfa_giro", NpgsqlTypes.NpgsqlDbType.Text).Value = Factura_Automatica.tfa_giro;
            comm.Parameters.Add("@tfa_ttf_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_ttf_id;
            comm.Parameters.Add("@tfa_ruta_pais", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_ruta_pais;
            comm.Parameters.Add("@tfa_ruta", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_ruta;
            comm.Parameters.Add("@tfa_observacion2", NpgsqlTypes.NpgsqlDbType.Text).Value = Factura_Automatica.tfa_observacion2;
            comm.Parameters.Add("@tfa_tra_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_tra_id;
            comm.Parameters.Add("@tfa_blid", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_blid;
            comm.Parameters.Add("@tfa_tto_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_tto_id;
            comm.Parameters.Add("@tfa_esignature", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_esignature;
            comm.Parameters.Add("@tfa_fac_electronica", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_fac_electronica;
            #region Generar Referencia Interna
            if ((Factura_Automatica.tfa_fac_electronica == 1) || (Factura_Automatica.tfa_fac_electronica == 2))
            {
                Factura_Automatica.tfa_internal_reference = Factura_Automatica.tfa_cli_id.ToString() + "0001" + Factura_Automatica.tfa_id.ToString();
            }
            else
            {
                Factura_Automatica.tfa_internal_reference = "0";
            }
            #endregion
            comm.Parameters.Add("@tfa_internal_reference", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_internal_reference;
            comm.Parameters.Add("@tfa_guid", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_guid;
            comm.Parameters.Add("@tfa_correo_documento_electronico", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_correo_documento_electronico;
            comm.Parameters.Add("@tfa_referencia_correo", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_referencia_correo;
            comm.Parameters.Add("@tfa_innerxml", NpgsqlTypes.NpgsqlDbType.Text).Value = Factura_Automatica.tfa_innerxml;
            comm.Parameters.Add("@tfa_tti_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_tti_id;
            comm.Parameters.Add("@tfa_correlativo_electronico", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_correlativo_electronico;
            comm.Parameters.Add("@tfa_no_factura_aduana", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_no_factura_aduana;
            comm.Parameters.Add("@tfa_no_embarque", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_no_embarque;
            comm.Parameters.Add("@tfa_tpi_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_tpi_id;
            result = comm.ExecuteNonQuery();
            comm.Parameters.Clear();
            comm.CommandText = "";;

            #region Actualizar Correlativo
            if (((Factura_Automatica.tfa_pai_id == 5) || (Factura_Automatica.tfa_pai_id == 21)) && (Factura_Automatica.tfa_fac_electronica == 1))
            {
            }
            else
            {
                Correlativo_INT = Correlativo_INT + 1;
                comm.CommandText = "update tbl_factura set fac_numero_inicial=" + Correlativo_INT + " where fac_suc_id=" + sucID + " and fac_serie='" + Factura_Automatica.tfa_serie + "' and fac_tipo=1";
                result = comm.ExecuteNonQuery();
                comm.CommandText = "";
                comm.Parameters.Clear();
            }
            #endregion
            #region Insertar Detalle de Rubros
            Arr_Result = new ArrayList();
            Arr_Result = Insertar_Detalle_Rubros(conn, comm, Transaction, PaisBean, contaID, sucID, 1, tfaID, Factura_Automatica.Arr_Detalle_Facturacion);
            if (Arr_Result[0].ToString() == "0")
            {
                Arr_Result = new ArrayList();
                Arr_Result.Add("0");
                Arr_Result.Add("Error al Insertar Detalle de Rubros de la Factura");
                return Arr_Result;
            }
            else if (Arr_Result[0].ToString() == "1")
            {
                Factura_Automatica.Arr_Detalle_Facturacion = (ArrayList)Arr_Result[1];
            }
            #endregion
            #region Insertar Libro Diario
            ArrayList Arr_Documentos = new ArrayList();
            Arr_Documentos.Add(Factura_Automatica);
            result = Insertar_Libro_Diario(conn, comm, Transaction, PaisBean, Factura_Automatica.tfa_conta_id, Factura_Automatica.tfa_suc_id, 1, Factura_Automatica.tfa_id, Arr_Documentos);
            if (result == -100)
            {
                Arr_Result = new ArrayList();
                Arr_Result.Add("0");
                Arr_Result.Add("Error al Insertar Partida Contable");
                return Arr_Result;
            }
            #endregion
            #region Marcar Traficos
            UsuarioBean user = new UsuarioBean();
            user.ID = Factura_Automatica.tfa_usu_id;
            user.pais = PaisBean;
            int update_result = 0;
            //foreach (Bean_Detalle_Rubros Cargos in Factura_Automatica.Arr_Detalle_Facturacion)
            //{
            //    if (Factura_Automatica.tfa_tto_id == 1)//FCL
            //    {
            //        update_result = DB.Update_Cargos_Traficos(user, Factura_Automatica.tfa_tto_id, 1, Cargos.tdf_cargo_id, Factura_Automatica.tfa_blid, Factura_Automatica.tfa_id, 1, "F", 1);
            //    }
            //    else if (Factura_Automatica.tfa_tto_id == 2)//LCL
            //    {
            //        update_result = DB.Update_Cargos_Traficos(user, Factura_Automatica.tfa_tto_id, 1, Cargos.tdf_cargo_id, Factura_Automatica.tfa_blid, Factura_Automatica.tfa_id, 1, "L", 1);
            //    }
            //    else//AEREO - TERRESTRE
            //    {
            //        update_result = DB.Update_Cargos_Traficos(user, Factura_Automatica.tfa_tto_id, 1, Cargos.tdf_cargo_id, Factura_Automatica.tfa_blid, Factura_Automatica.tfa_id, 1, "", 1);
            //    }
            //    if (update_result == -100)
            //    {
            //        Transaction.Rollback();
            //        return null;
            //    }
            //}
            #endregion
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            Arr_Result = new ArrayList();
            Arr_Result.Add("0");
            Arr_Result.Add("Error al Insertar Cobro Automatico de Intercompany");
            return Arr_Result;
        }
        Arr_Result = new ArrayList();
        Arr_Result.Add("1");
        Arr_Result.Add("Factura Automatica Generada Exitosamente");
        Arr_Result.Add(Factura_Automatica);
        return Arr_Result;
        #endregion
    }
    public static ArrayList Insertar_Nota_Debito(NpgsqlConnection conn, NpgsqlCommand comm, NpgsqlTransaction Transaction, PaisBean PaisBean, int contaID, int sucID, Bean_Nota_Debito_Automatica Nota_Debito_Automatica)
    {
        #region Insertar Nota de Debito
        ArrayList Arr_Result = new ArrayList();
        int result = 0;
        try
        {
            #region Definicion de Variales y parametros Iniciales
            int tndID = 0;
            string Correlativo_STR = "";
            int Correlativo_INT = 0;

            tndID = Obtener_Llave_Primaria(conn, comm, Transaction, 4);
            Nota_Debito_Automatica.tnd_id = tndID;

            #region Obtener Correlativo
            comm.CommandText = "select max(fac_numero_inicial) from tbl_factura where fac_suc_id=" + sucID + " and fac_tipo=4 and fac_serie='" + Nota_Debito_Automatica.tnd_serie + "' ";
            Correlativo_INT = Convert.ToInt32(comm.ExecuteScalar());
            Correlativo_STR = Correlativo_INT.ToString();
            Nota_Debito_Automatica.tnd_correlativo = Correlativo_INT;
            comm.CommandText = "";
            comm.Parameters.Clear();
            #endregion

            #region Validacion Tipo de Transmision
            if (((Nota_Debito_Automatica.tnd_pai_id == 5) || (Nota_Debito_Automatica.tnd_pai_id == 21)) && (Nota_Debito_Automatica.tnd_fac_electronica == 1) && (Nota_Debito_Automatica.tnd_tpi_id == 3))
            {
                Correlativo_INT = 0;
                Nota_Debito_Automatica.tnd_correlativo = 0;
            }
            #endregion

            #endregion
            comm.CommandText = "insert into tbl_nota_debito (tnd_id, tnd_nit, tnd_nombre, tnd_cli_id, tnd_fecha_emision, tnd_total, tnd_pai_id, tnd_ted_id, tnd_observacion, tnd_usu_id, tnd_direccion, tnd_moneda, tnd_hbl, tnd_mbl, tnd_contenedor, tnd_routing, tnd_referencia, tnd_serie, tnd_suc_id, tnd_correlativo, tnd_tpi_id, tnd_tcon_id, tnd_fecha_pago, tnd_sub_total, tnd_impuesto, tnd_naviera, tnd_vapor, tnd_shipper, tnd_ordenpo, tnd_consignee, tnd_comodity, tnd_paquetes, tnd_peso, tnd_dua_salida, tnd_vendedor1, tnd_vendedor2, tnd_razon, tnd_id_shipper, tnd_id_consignee, tnd_sub_total_eq, tnd_impuesto_eq, tnd_total_eq, tnd_tie_id, tnd_ttc_id, tnd_allin, tnd_reciboaduana, tnd_volumen, tnd_dua_ingreso, tnd_cant_paquetes, tnd_agente_id, tnd_agente, tnd_fiscal, tnd_fecha_libro_compras, tnd_blid, tnd_tto_id, tnd_bien_serv, tnd_esignature, tnd_fac_electronica, tnd_internal_reference, tnd_guid, tnd_correo_documento_electronico, tnd_referencia_correo, tnd_innerxml, tnd_fecha_batch, tnd_tti_id, tnd_ttd_id) ";
            comm.CommandText += "values (@tnd_id, @tnd_nit, @tnd_nombre, @tnd_cli_id, @tnd_fecha_emision, @tnd_total, @tnd_pai_id, @tnd_ted_id, @tnd_observacion, @tnd_usu_id, @tnd_direccion, @tnd_moneda, @tnd_hbl, @tnd_mbl, @tnd_contenedor, @tnd_routing, @tnd_referencia, @tnd_serie, @tnd_suc_id, @tnd_correlativo, @tnd_tpi_id, @tnd_tcon_id, @tnd_fecha_pago, @tnd_sub_total, @tnd_impuesto, @tnd_naviera, @tnd_vapor, @tnd_shipper, @tnd_ordenpo, @tnd_consignee, @tnd_comodity, @tnd_paquetes, @tnd_peso, @tnd_dua_salida, @tnd_vendedor1, @tnd_vendedor2, @tnd_razon, @tnd_id_shipper, @tnd_id_consignee, @tnd_sub_total_eq, @tnd_impuesto_eq, @tnd_total_eq, @tnd_tie_id, @tnd_ttc_id, @tnd_allin, @tnd_reciboaduana, @tnd_volumen, @tnd_dua_ingreso, @tnd_cant_paquetes, @tnd_agente_id, @tnd_agente, @tnd_fiscal, @tnd_fecha_libro_compras, @tnd_blid, @tnd_tto_id, @tnd_bien_serv, @tnd_esignature, @tnd_fac_electronica, @tnd_internal_reference, @tnd_guid, @tnd_correo_documento_electronico, @tnd_referencia_correo, @tnd_innerxml, @tnd_fecha_batch, @tnd_tti_id, @tnd_ttd_id)";
            comm.Parameters.Add("@tnd_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_id;
            comm.Parameters.Add("@tnd_nit", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_nit;
            comm.Parameters.Add("@tnd_nombre", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_nombre.Trim();
            comm.Parameters.Add("@tnd_cli_id", NpgsqlTypes.NpgsqlDbType.Double).Value = Nota_Debito_Automatica.tnd_cli_id;
            comm.Parameters.Add("@tnd_fecha_emision", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = Nota_Debito_Automatica.tnd_fecha_emision;
            comm.Parameters.Add("@tnd_total", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Nota_Debito_Automatica.tnd_total;
            comm.Parameters.Add("@tnd_pai_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_pai_id;
            comm.Parameters.Add("@tnd_ted_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_ted_id;
            comm.Parameters.Add("@tnd_observacion", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_observacion;
            comm.Parameters.Add("@tnd_usu_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_usu_id;
            comm.Parameters.Add("@tnd_direccion", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_direccion.Trim();
            comm.Parameters.Add("@tnd_moneda", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_moneda;
            comm.Parameters.Add("@tnd_hbl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_hbl;
            comm.Parameters.Add("@tnd_mbl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_mbl;
            comm.Parameters.Add("@tnd_contenedor", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_contenedor;
            comm.Parameters.Add("@tnd_routing", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_routing;
            comm.Parameters.Add("@tnd_referencia", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_referencia;
            comm.Parameters.Add("@tnd_serie", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_serie;
            comm.Parameters.Add("@tnd_suc_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_suc_id;
            comm.Parameters.Add("@tnd_correlativo", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_correlativo;
            comm.Parameters.Add("@tnd_tpi_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_tpi_id;
            comm.Parameters.Add("@tnd_tcon_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_tcon_id;
            comm.Parameters.Add("@tnd_fecha_pago", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = Nota_Debito_Automatica.tnd_fecha_pago;
            comm.Parameters.Add("@tnd_sub_total", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Nota_Debito_Automatica.tnd_sub_total;
            comm.Parameters.Add("@tnd_impuesto", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Nota_Debito_Automatica.tnd_impuesto;
            comm.Parameters.Add("@tnd_naviera", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_naviera;
            comm.Parameters.Add("@tnd_vapor", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_vapor;
            comm.Parameters.Add("@tnd_shipper", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_shipper;
            comm.Parameters.Add("@tnd_ordenpo", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_ordenpo;
            comm.Parameters.Add("@tnd_consignee", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_consignee;
            comm.Parameters.Add("@tnd_comodity", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_comodity;
            comm.Parameters.Add("@tnd_paquetes", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_paquetes;
            comm.Parameters.Add("@tnd_peso", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_peso;
            comm.Parameters.Add("@tnd_dua_salida", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_dua_salida;
            comm.Parameters.Add("@tnd_vendedor1", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_vendedor1;
            comm.Parameters.Add("@tnd_vendedor2", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_vendedor2;
            comm.Parameters.Add("@tnd_razon", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_razon;
            comm.Parameters.Add("@tnd_id_shipper", NpgsqlTypes.NpgsqlDbType.Double).Value = Nota_Debito_Automatica.tnd_id_shipper;
            comm.Parameters.Add("@tnd_id_consignee", NpgsqlTypes.NpgsqlDbType.Double).Value = Nota_Debito_Automatica.tnd_id_consignee;
            comm.Parameters.Add("@tnd_sub_total_eq", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Nota_Debito_Automatica.tnd_sub_total_eq;
            comm.Parameters.Add("@tnd_impuesto_eq", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Nota_Debito_Automatica.tnd_impuesto_eq;
            comm.Parameters.Add("@tnd_total_eq", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Nota_Debito_Automatica.tnd_total_eq;
            comm.Parameters.Add("@tnd_tie_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_tie_id;
            comm.Parameters.Add("@tnd_ttc_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_ttc_id;
            comm.Parameters.Add("@tnd_allin", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_allin;
            comm.Parameters.Add("@tnd_reciboaduana", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_reciboaduana;
            comm.Parameters.Add("@tnd_volumen", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_volumen;
            comm.Parameters.Add("@tnd_dua_ingreso", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_dua_ingreso;
            comm.Parameters.Add("@tnd_cant_paquetes", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_cant_paquetes;
            comm.Parameters.Add("@tnd_agente_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_agente_id;
            comm.Parameters.Add("@tnd_agente", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_agente;
            comm.Parameters.Add("@tnd_fiscal", NpgsqlTypes.NpgsqlDbType.Boolean).Value = Nota_Debito_Automatica.tnd_fiscal;
            comm.Parameters.Add("@tnd_fecha_libro_compras", NpgsqlTypes.NpgsqlDbType.Date).Value = Nota_Debito_Automatica.tnd_fecha_libro_compras;
            comm.Parameters.Add("@tnd_blid", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_blid;
            comm.Parameters.Add("@tnd_tto_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_tto_id;
            comm.Parameters.Add("@tnd_bien_serv", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_bien_serv;
            comm.Parameters.Add("@tnd_esignature", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_esignature;
            comm.Parameters.Add("@tnd_fac_electronica", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_fac_electronica;
            comm.Parameters.Add("@tnd_internal_reference", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_internal_reference;
            comm.Parameters.Add("@tnd_guid", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_guid;
            comm.Parameters.Add("@tnd_correo_documento_electronico", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_correo_documento_electronico;
            comm.Parameters.Add("@tnd_referencia_correo", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_referencia_correo;
            comm.Parameters.Add("@tnd_innerxml", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_innerxml;
            comm.Parameters.Add("@tnd_fecha_batch", NpgsqlTypes.NpgsqlDbType.Date).Value = Nota_Debito_Automatica.tnd_fecha_batch;
            comm.Parameters.Add("@tnd_tti_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_bien_serv;
            comm.Parameters.Add("@tnd_ttd_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_ttd_id;
            result = comm.ExecuteNonQuery();
            comm.Parameters.Clear();
            comm.CommandText = "";

            #region Actualizar Correlativo
            if (((Nota_Debito_Automatica.tnd_pai_id == 5) || (Nota_Debito_Automatica.tnd_pai_id == 21)) && (Nota_Debito_Automatica.tnd_fac_electronica == 1) && (Nota_Debito_Automatica.tnd_tpi_id == 3))
            {
            }
            else
            {
                Correlativo_INT = Correlativo_INT + 1;
                comm.CommandText = "update tbl_factura set fac_numero_inicial=" + Correlativo_INT + " where fac_suc_id=" + sucID + " and fac_serie='" + Nota_Debito_Automatica.tnd_serie + "' and fac_tipo=4";
                result = comm.ExecuteNonQuery();
                comm.CommandText = "";
                comm.Parameters.Clear();
            }
            #endregion
            #region Insertar Detalle de Rubros
            Arr_Result = new ArrayList();
            Arr_Result = Insertar_Detalle_Rubros(conn, comm, Transaction, PaisBean, contaID, sucID, 4, tndID, Nota_Debito_Automatica.Arr_Detalle_Facturacion);
            if (Arr_Result[0].ToString() == "0")
            {
                Arr_Result = new ArrayList();
                Arr_Result.Add("0");
                Arr_Result.Add("Error al Insertar Detalle de Rubros de la Factura");
                return Arr_Result;
            }
            else if (Arr_Result[0].ToString() == "1")
            {
                Nota_Debito_Automatica.Arr_Detalle_Facturacion = (ArrayList)Arr_Result[1];
            }
            #endregion
            #region Insertar Libro Diario
            ArrayList Arr_Documentos = new ArrayList();
            Arr_Documentos.Add(Nota_Debito_Automatica);
            result = Insertar_Libro_Diario(conn, comm, Transaction, PaisBean, Nota_Debito_Automatica.tnd_tcon_id, Nota_Debito_Automatica.tnd_suc_id, 4, Nota_Debito_Automatica.tnd_id, Arr_Documentos);
            if (result == -100)
            {
                Arr_Result = new ArrayList();
                Arr_Result.Add("0");
                Arr_Result.Add("Error al Insertar Partida Contable");
                return Arr_Result;
            }
            #endregion
            #region Marcar Traficos
            UsuarioBean user = new UsuarioBean();
            user.ID = Nota_Debito_Automatica.tnd_usu_id;
            user.pais = PaisBean;
            int update_result = 0;
            foreach (Bean_Detalle_Rubros Cargos in Nota_Debito_Automatica.Arr_Detalle_Facturacion)
            {
                if (Nota_Debito_Automatica.tnd_tto_id == 1)//FCL
                {
                    update_result = DB.Update_Cargos_Traficos(user, Nota_Debito_Automatica.tnd_tto_id, 1, Cargos.tdf_cargo_id, Nota_Debito_Automatica.tnd_blid, Nota_Debito_Automatica.tnd_id, 4, "F", 1);
                }
                else if (Nota_Debito_Automatica.tnd_tto_id == 2)//LCL
                {
                    update_result = DB.Update_Cargos_Traficos(user, Nota_Debito_Automatica.tnd_tto_id, 1, Cargos.tdf_cargo_id, Nota_Debito_Automatica.tnd_blid, Nota_Debito_Automatica.tnd_id, 4, "L", 1);
                }
                else//AEREO - TERRESTRE
                {
                    update_result = DB.Update_Cargos_Traficos(user, Nota_Debito_Automatica.tnd_tto_id, 1, Cargos.tdf_cargo_id, Nota_Debito_Automatica.tnd_blid, Nota_Debito_Automatica.tnd_id, 4, "", 1);
                }
                if (update_result == -100)
                {
                    Transaction.Rollback();
                    return null;
                }
            }
            #endregion
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            Arr_Result = new ArrayList();
            Arr_Result.Add("0");
            Arr_Result.Add("Error al Insertar Cobro Automatico por Cuenta de Terceros");
            return Arr_Result;
        }
        Arr_Result = new ArrayList();
        Arr_Result.Add("1");
        Arr_Result.Add("Nota de Debito Automatica Generada Exitosamente");
        Arr_Result.Add(Nota_Debito_Automatica);
        return Arr_Result;
        #endregion
    }
    public static int Insertar_Nota_Credito(NpgsqlConnection conn, NpgsqlCommand comm, NpgsqlTransaction Transaction, PaisBean PaisBean, int contaID, int sucID, Bean_Nota_Credito_Automatica Nota_Credito_Automatica)
    {
        #region Insertar Nota de Credito
        int result = 0;
        #region Definicion de Variales y parametros Iniciales
        int tncID = 0;
        int Numero_Partida_INT = 0;
        string Numero_Partida_STR = "";
        string Correlativo_STR = "";
        int Correlativo_INT = 0;
        tncID = Obtener_Llave_Primaria(conn, comm, Transaction, 3);
        Correlativo_INT = DB.GetCorr(sucID, 3, Nota_Credito_Automatica.tnc_serie);
        Correlativo_STR = Correlativo_STR.ToString();
        #endregion
        comm.CommandText = "insert into tbl_nota_credito (tnc_id, tnc_usu_id, tnc_pai_id, tnc_fecha, tnc_hora, tnc_suc_id, tnc_ted_id, tnc_monto, tnc_observaciones, tnc_cli_id, tnc_monto_equivalente, tnc_mon_id, tnc_serie, tnc_correlativo, tnc_tpi_id, tnc_tcon_id, tnc_ttr_id, tnc_hbl, tnc_mbl, tnc_routing, tnc_contenedor, tnc_fecha_emision, tnc_nombre, tnc_referencia, tnc_nit, tnc_poliza, tnc_fiscal, tnc_fecha_libro_compras, tnc_montosinimpuesto, tnc_impuesto, tnc_esignature, tnc_fac_electronica, tnc_internal_reference, tnc_guid, tnc_correo_documento_electronico, tnc_referencia_correo, tnc_innerxml, tnc_fecha_batch) ";
        comm.CommandText += "values (@tnc_id, @tnc_usu_id, @tnc_pai_id, @tnc_fecha, @tnc_hora, @tnc_suc_id, @tnc_ted_id, @tnc_monto, @tnc_observaciones, @tnc_cli_id, @tnc_monto_equivalente, @tnc_mon_id, @tnc_serie, @tnc_correlativo, @tnc_tpi_id, @tnc_tcon_id, @tnc_ttr_id, @tnc_hbl, @tnc_mbl, @tnc_routing, @tnc_contenedor, @tnc_fecha_emision, @tnc_nombre, @tnc_referencia, @tnc_nit, @tnc_poliza, @tnc_fiscal, @tnc_fecha_libro_compras, @tnc_montosinimpuesto, @tnc_impuesto, @tnc_esignature, @tnc_fac_electronica, @tnc_internal_reference, @tnc_guid, @tnc_correo_documento_electronico, @tnc_referencia_correo, @tnc_innerxml, @tnc_fecha_batch)";
        comm.Parameters.Add("@tnc_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = tncID;
        comm.Parameters.Add("@tnc_usu_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Credito_Automatica.tnc_usu_id;
        comm.Parameters.Add("@tnc_pai_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Credito_Automatica.tnc_pai_id;
        comm.Parameters.Add("@tnc_fecha", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Credito_Automatica.tnc_fecha;
        comm.Parameters.Add("@tnc_hora", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Credito_Automatica.tnc_hora;
        comm.Parameters.Add("@tnc_suc_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Credito_Automatica.tnc_suc_id;
        comm.Parameters.Add("@tnc_ted_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Credito_Automatica.tnc_ted_id;
        comm.Parameters.Add("@tnc_monto", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Nota_Credito_Automatica.tnc_monto;
        comm.Parameters.Add("@tnc_observaciones", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Credito_Automatica.tnc_observaciones;
        comm.Parameters.Add("@tnc_cli_id", NpgsqlTypes.NpgsqlDbType.Double).Value = Nota_Credito_Automatica.tnc_cli_id;
        comm.Parameters.Add("@tnc_monto_equivalente", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Nota_Credito_Automatica.tnc_monto_equivalente;
        comm.Parameters.Add("@tnc_mon_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Credito_Automatica.tnc_mon_id;
        comm.Parameters.Add("@tnc_serie", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Credito_Automatica.tnc_serie;
        comm.Parameters.Add("@tnc_correlativo", NpgsqlTypes.NpgsqlDbType.Integer).Value = Correlativo_INT;
        comm.Parameters.Add("@tnc_tpi_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Credito_Automatica.tnc_tpi_id;
        comm.Parameters.Add("@tnc_tcon_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Credito_Automatica.tnc_tcon_id;
        comm.Parameters.Add("@tnc_ttr_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Credito_Automatica.tnc_ttr_id;
        comm.Parameters.Add("@tnc_hbl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Credito_Automatica.tnc_hbl;
        comm.Parameters.Add("@tnc_mbl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Credito_Automatica.tnc_mbl;
        comm.Parameters.Add("@tnc_routing", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Credito_Automatica.tnc_routing;
        comm.Parameters.Add("@tnc_contenedor", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Credito_Automatica.tnc_contenedor;
        comm.Parameters.Add("@tnc_fecha_emision", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Credito_Automatica.tnc_fecha_emision;
        comm.Parameters.Add("@tnc_nombre", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Credito_Automatica.tnc_nombre;
        comm.Parameters.Add("@tnc_referencia", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Credito_Automatica.tnc_referencia;
        comm.Parameters.Add("@tnc_nit", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Credito_Automatica.tnc_nit;
        comm.Parameters.Add("@tnc_poliza", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Credito_Automatica.tnc_poliza;
        comm.Parameters.Add("@tnc_fiscal", NpgsqlTypes.NpgsqlDbType.Boolean).Value = Nota_Credito_Automatica.tnc_fiscal;
        comm.Parameters.Add("@tnc_fecha_libro_compras", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Credito_Automatica.tnc_fecha_libro_compras;
        comm.Parameters.Add("@tnc_montosinimpuesto", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Nota_Credito_Automatica.tnc_montosinimpuesto;
        comm.Parameters.Add("@tnc_impuesto", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Nota_Credito_Automatica.tnc_impuesto;
        comm.Parameters.Add("@tnc_esignature", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Credito_Automatica.tnc_esignature;
        comm.Parameters.Add("@tnc_fac_electronica", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Credito_Automatica.tnc_fac_electronica;
        comm.Parameters.Add("@tnc_internal_reference", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Credito_Automatica.tnc_internal_reference;
        comm.Parameters.Add("@tnc_guid", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Credito_Automatica.tnc_guid;
        comm.Parameters.Add("@tnc_correo_documento_electronico", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Credito_Automatica.tnc_correo_documento_electronico;
        comm.Parameters.Add("@tnc_referencia_correo", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Credito_Automatica.tnc_referencia_correo;
        comm.Parameters.Add("@tnc_innerxml", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Credito_Automatica.tnc_innerxml;
        comm.Parameters.Add("@tnc_fecha_batch", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Credito_Automatica.tnc_fecha_batch;
        result = comm.ExecuteNonQuery();
        comm.Parameters.Clear();
        comm.CommandText = "";
        #region Actualizaciones
        DB.updateCorr(Correlativo_INT + 1, sucID, 3, Nota_Credito_Automatica.tnc_serie);
        #endregion
        return result;
        #endregion
    }
    public static ArrayList Insertar_Provision(NpgsqlConnection conn, NpgsqlCommand comm, NpgsqlTransaction Transaction, PaisBean PaisBean, int contaID, int sucID, Bean_Provision_Automatica Provision_Automatica)
    {
        #region Insertar Provision
        ArrayList Arr_Result = new ArrayList();
        int result = 0;
        try
        {
            #region Definicion de Variales y parametros Iniciales
            int tprID = 0;
            tprID = Obtener_Llave_Primaria(conn, comm, Transaction, 5);
            Provision_Automatica.tpr_prov_id = tprID;
            #endregion
            #region Obtener Serie y Secuencia de la Provision
            RE_GenericBean Datos_Bean = DB.Get_Serie_CorrelativoBy_Traficos(Provision_Automatica.tpr_suc_id, Provision_Automatica.tpr_tcon_id, Provision_Automatica.tpr_mon_id);
            if (Datos_Bean == null)
            {
                int resultado_secuencia = DB.Crear_Series_Provisiones_Automaticas(Provision_Automatica.tpr_suc_id, PaisBean, Provision_Automatica.tpr_tto_id, Provision_Automatica.tpr_tcon_id, Provision_Automatica.tpr_mon_id);
                if (resultado_secuencia == -100)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    Arr_Result.Add("Existio un error al Momento de Crear la Secuencia de la Serie de Provisiones Automaticas");
                    return Arr_Result;
                }
            }
            Datos_Bean = DB.Get_Serie_CorrelativoBy_Traficos(Provision_Automatica.tpr_suc_id, Provision_Automatica.tpr_tcon_id, Provision_Automatica.tpr_mon_id);
            comm.CommandText = "select nextval('" + Datos_Bean.strC2 + "')";
            Provision_Automatica.tpr_correlativo = int.Parse(comm.ExecuteScalar().ToString());
            comm.Parameters.Clear();
            #endregion
            comm.CommandText = "insert into tbl_provisiones (tpr_prov_id, tpr_proveedor_id, tpr_fact_id, tpr_fact_fecha, tpr_fecha_maxpago, tpr_valor, tpr_afecto, tpr_noafecto, tpr_iva, tpr_observacion, tpr_suc_id, tpr_pai_id, tpr_usu_creacion, tpr_fecha_creacion, tpr_usu_acepta, tpr_fecha_acepta, tpr_departamento, tpr_ted_id, tpr_serie, tpr_serie_oc, tpr_correlativo_oc, tpr_tts_id, tpr_hbl, tpr_mbl, tpr_routing, tpr_contenedor, tpr_tpi_id, tpr_correlativo, tpr_mon_id, tpr_serie_contrasena, tpr_contrasena_correlativo, tpr_valor_equivalente, tpr_fact_corr, tpr_proveedor_cajachica, tpr_imp_exp_id, tpr_bien_serv, tpr_tcon_id, tpr_nombre, tpr_proveedor_cajachica_id, tpr_poliza, tpr_fiscal, tpr_fecha_libro_compras, tpr_tto_id, tpr_ruta_pais, tpr_ruta, tpr_blid, tpr_tti_id, tpr_usu_modifica_regimen, tpr_usu_anula, tpr_fecha_anula, tpr_toc_id, tpr_observacion_contrasena, tpr_fecha_recibo_factura, tpr_ttd_id) ";
            comm.CommandText += "values (@tpr_prov_id, @tpr_proveedor_id, @tpr_fact_id, @tpr_fact_fecha, @tpr_fecha_maxpago, @tpr_valor, @tpr_afecto, @tpr_noafecto, @tpr_iva, @tpr_observacion, @tpr_suc_id, @tpr_pai_id, @tpr_usu_creacion, @tpr_fecha_creacion, @tpr_usu_acepta, @tpr_fecha_acepta, @tpr_departamento, @tpr_ted_id, @tpr_serie, @tpr_serie_oc, @tpr_correlativo_oc, @tpr_tts_id, @tpr_hbl, @tpr_mbl, @tpr_routing, @tpr_contenedor, @tpr_tpi_id, @tpr_correlativo, @tpr_mon_id, @tpr_serie_contrasena, @tpr_contrasena_correlativo, @tpr_valor_equivalente, @tpr_fact_corr, @tpr_proveedor_cajachica, @tpr_imp_exp_id, @tpr_bien_serv, @tpr_tcon_id, @tpr_nombre, @tpr_proveedor_cajachica_id, @tpr_poliza, @tpr_fiscal, @tpr_fecha_libro_compras, @tpr_tto_id, @tpr_ruta_pais, @tpr_ruta, @tpr_blid, @tpr_tti_id, @tpr_usu_modifica_regimen, @tpr_usu_anula, @tpr_fecha_anula, @tpr_toc_id, @tpr_observacion_contrasena, @tpr_fecha_recibo_factura, @tpr_ttd_id)";
            comm.Parameters.Add("@tpr_prov_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_prov_id;
            comm.Parameters.Add("@tpr_proveedor_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_proveedor_id;
            comm.Parameters.Add("@tpr_fact_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Provision_Automatica.tpr_fact_id;
            #region Fechas de Proveedor
            if (Provision_Automatica.tpr_fact_fecha == "")
            {
                comm.Parameters.Add("@tpr_fact_fecha", NpgsqlTypes.NpgsqlDbType.Date).Value = null;
            }
            else
            {
                comm.Parameters.Add("@tpr_fact_fecha", NpgsqlTypes.NpgsqlDbType.Date).Value = Provision_Automatica.tpr_fact_fecha;
            }
            if (Provision_Automatica.tpr_fecha_maxpago == "")
            {
                comm.Parameters.Add("@tpr_fecha_maxpago", NpgsqlTypes.NpgsqlDbType.Date).Value = null;
            }
            else
            {
                comm.Parameters.Add("@tpr_fecha_maxpago", NpgsqlTypes.NpgsqlDbType.Date).Value = Provision_Automatica.tpr_fecha_maxpago;
            }
            #endregion
            comm.Parameters.Add("@tpr_valor", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Provision_Automatica.tpr_valor;
            comm.Parameters.Add("@tpr_afecto", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Provision_Automatica.tpr_afecto;
            comm.Parameters.Add("@tpr_noafecto", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Provision_Automatica.tpr_noafecto;
            comm.Parameters.Add("@tpr_iva", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Provision_Automatica.tpr_iva;
            comm.Parameters.Add("@tpr_observacion", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Provision_Automatica.tpr_observacion;
            comm.Parameters.Add("@tpr_suc_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_suc_id;
            comm.Parameters.Add("@tpr_pai_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_pai_id;
            comm.Parameters.Add("@tpr_usu_creacion", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Provision_Automatica.tpr_usu_creacion;
            comm.Parameters.Add("@tpr_fecha_creacion", NpgsqlTypes.NpgsqlDbType.Date).Value = Provision_Automatica.tpr_fecha_creacion;
            comm.Parameters.Add("@tpr_usu_acepta", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Provision_Automatica.tpr_usu_acepta;
            comm.Parameters.Add("@tpr_fecha_acepta", NpgsqlTypes.NpgsqlDbType.Date).Value = Provision_Automatica.tpr_fecha_acepta;
            comm.Parameters.Add("@tpr_departamento", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_departamento;
            comm.Parameters.Add("@tpr_ted_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_ted_id;
            comm.Parameters.Add("@tpr_serie", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Provision_Automatica.tpr_serie;
            comm.Parameters.Add("@tpr_serie_oc", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Provision_Automatica.tpr_serie_oc;
            comm.Parameters.Add("@tpr_correlativo_oc", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_correlativo_oc;
            comm.Parameters.Add("@tpr_tts_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_tts_id;
            comm.Parameters.Add("@tpr_hbl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Provision_Automatica.tpr_hbl;
            comm.Parameters.Add("@tpr_mbl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Provision_Automatica.tpr_mbl;
            comm.Parameters.Add("@tpr_routing", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Provision_Automatica.tpr_routing;
            comm.Parameters.Add("@tpr_contenedor", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Provision_Automatica.tpr_contenedor;
            comm.Parameters.Add("@tpr_tpi_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_tpi_id;
            comm.Parameters.Add("@tpr_correlativo", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_correlativo;
            comm.Parameters.Add("@tpr_mon_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_mon_id;
            comm.Parameters.Add("@tpr_serie_contrasena", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Provision_Automatica.tpr_serie_contrasena;
            comm.Parameters.Add("@tpr_contrasena_correlativo", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_contrasena_correlativo;
            comm.Parameters.Add("@tpr_valor_equivalente", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Provision_Automatica.tpr_valor_equivalente;
            comm.Parameters.Add("@tpr_fact_corr", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Provision_Automatica.tpr_fact_corr;
            comm.Parameters.Add("@tpr_proveedor_cajachica", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Provision_Automatica.tpr_proveedor_cajachica;
            comm.Parameters.Add("@tpr_imp_exp_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_imp_exp_id;
            comm.Parameters.Add("@tpr_bien_serv", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_bien_serv;
            comm.Parameters.Add("@tpr_tcon_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_tcon_id;
            comm.Parameters.Add("@tpr_nombre", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Provision_Automatica.tpr_nombre.Trim();
            comm.Parameters.Add("@tpr_proveedor_cajachica_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_proveedor_cajachica_id;
            comm.Parameters.Add("@tpr_poliza", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Provision_Automatica.tpr_poliza;
            comm.Parameters.Add("@tpr_fiscal", NpgsqlTypes.NpgsqlDbType.Boolean).Value = Provision_Automatica.tpr_fiscal;
            comm.Parameters.Add("@tpr_fecha_libro_compras", NpgsqlTypes.NpgsqlDbType.Date).Value = Provision_Automatica.tpr_fecha_libro_compras;
            comm.Parameters.Add("@tpr_tto_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_tto_id;
            comm.Parameters.Add("@tpr_ruta_pais", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Provision_Automatica.tpr_ruta_pais;
            comm.Parameters.Add("@tpr_ruta", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Provision_Automatica.tpr_ruta;
            comm.Parameters.Add("@tpr_blid", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_blid;
            comm.Parameters.Add("@tpr_tti_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_tti_id;
            comm.Parameters.Add("@tpr_usu_modifica_regimen", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Provision_Automatica.tpr_usu_modifica_regimen;
            comm.Parameters.Add("@tpr_usu_anula", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Provision_Automatica.tpr_usu_anula;
            comm.Parameters.Add("@tpr_fecha_anula", NpgsqlTypes.NpgsqlDbType.Date).Value = null;
            comm.Parameters.Add("@tpr_toc_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_toc_id;
            comm.Parameters.Add("@tpr_observacion_contrasena", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Provision_Automatica.tpr_observacion_contrasena;
            comm.Parameters.Add("@tpr_fecha_recibo_factura", NpgsqlTypes.NpgsqlDbType.Date).Value = null;
            comm.Parameters.Add("@tpr_ttd_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_ttd_id;
            result = comm.ExecuteNonQuery();
            comm.Parameters.Clear();
            comm.CommandText = "";
            #region Insertar Detalle de Rubros
            Arr_Result = new ArrayList();
            Arr_Result = Insertar_Detalle_Rubros(conn, comm, Transaction, PaisBean, contaID, sucID, 5, tprID, Provision_Automatica.Arr_Detalle_Provision);
            if (Arr_Result[0].ToString() == "0")
            {
                Arr_Result = new ArrayList();
                Arr_Result.Add("0");
                Arr_Result.Add("Error al Insertar Detalle de Rubros Provision");
                return Arr_Result;
            }
            else if (Arr_Result[0].ToString() == "1")
            {
                Provision_Automatica.Arr_Detalle_Provision = (ArrayList)Arr_Result[1];
            }
            #endregion
            #region Insertar Libro Diario
            ArrayList Arr_Documentos = new ArrayList();
            Arr_Documentos.Add(Provision_Automatica);
            result = Insertar_Libro_Diario(conn, comm, Transaction, PaisBean, contaID, sucID, 5, tprID, Arr_Documentos);
            if (result == -100)
            {
                Arr_Result = new ArrayList();
                Arr_Result.Add("0");
                Arr_Result.Add("Error al Insertar Partida Contable");
                return Arr_Result;
            }
            #endregion
            #region Marcar Traficos
            //int update_result = 0;
            //foreach (Bean_Costos Costo in Provision_Automatica.Arr_Costos_Provision)
            //{
            //    if (Provision_Automatica.tpr_tto_id == 1)//FCL
            //    {
            //        update_result = DB.Update_Costos_Traficos(PaisBean.schema, Provision_Automatica.tpr_tto_id, 1, rubro.RubroCostoID, Provision_Bean.intC10, prov_id, 5, "F");
            //    }
            //    else if (Provision_Automatica.tpr_tto_id == 2)//LCL
            //    {
            //        update_result = DB.Update_Costos_Traficos(PaisBean.schema, Provision_Automatica.tpr_tto_id, 1, rubro.RubroCostoID, Provision_Bean.intC10, prov_id, 5, "L");
            //    }
            //    else//AEREO - TERRESTRE
            //    {
            //        update_result = DB.Update_Costos_Traficos(PaisBean.schema, Provision_Automatica.tpr_tto_id, 1, rubro.RubroCostoID, Provision_Bean.intC10, prov_id, 5, "");
            //    }
            //    if (update_result == -100)
            //    {
            //        Transaction.Rollback();
            //        return null;
            //    }
            //}
            #endregion
            Arr_Result = new ArrayList();
            Arr_Result.Add("1");
            Arr_Result.Add(Provision_Automatica);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            Arr_Result = new ArrayList();
            Arr_Result.Add("0");
            Arr_Result.Add("Error al Insertar Pago Automatico al Intercompany");
            return Arr_Result;
        }
        Arr_Result = new ArrayList();
        Arr_Result.Add("1");
        Arr_Result.Add("Provision Automatica Generada Exitosamente");
        Arr_Result.Add(Provision_Automatica);
        return Arr_Result;
        #endregion
    }
    public static ArrayList Insertar_Detalle_Rubros(NpgsqlConnection conn, NpgsqlCommand comm, NpgsqlTransaction Transaction, PaisBean PaisBean, int contaID, int sucID, int ttrID, int refID, ArrayList Arr_Rubros)
    {
        #region Insertar Detalle de Rubros
        ArrayList Arr_Result = new ArrayList();
        int result = 0;
        try
        {
            if ((ttrID == 1) || (ttrID == 4) || (ttrID == 5))
            {
                for (int i = 0; i < Arr_Rubros.Count; i++ )
                {
                    Bean_Detalle_Rubros Rubros = (Bean_Detalle_Rubros)Arr_Rubros[i];
                    comm.CommandText = "insert into tbl_detalle_facturacion (tdf_rub_id, tdf_montosinimpuesto, tdf_impuesto, tdf_monto, tdf_tfa_id, tdf_ttr_id, tdf_tts_id, tdf_total_equivalente, tdf_ttm_id, tdf_comentarios, tdf_cargo_id) ";
                    comm.CommandText += "values (@tdf_rub_id, @tdf_montosinimpuesto, @tdf_impuesto, @tdf_monto, @tdf_tfa_id, @tdf_ttr_id, @tdf_tts_id, @tdf_total_equivalente, @tdf_ttm_id, @tdf_comentarios, @tdf_cargo_id)";
                    comm.Parameters.Add("@tdf_rub_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Rubros.tdf_rub_id;
                    comm.Parameters.Add("@tdf_montosinimpuesto", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Rubros.tdf_montosinimpuesto;
                    comm.Parameters.Add("@tdf_impuesto", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Rubros.tdf_impuesto;
                    comm.Parameters.Add("@tdf_monto", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Rubros.tdf_monto;
                    comm.Parameters.Add("@tdf_tfa_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = refID;
                    comm.Parameters.Add("@tdf_ttr_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = ttrID;
                    comm.Parameters.Add("@tdf_tts_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Rubros.tdf_tts_id;
                    comm.Parameters.Add("@tdf_total_equivalente", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Rubros.tdf_total_equivalente;
                    comm.Parameters.Add("@tdf_ttm_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Rubros.tdf_ttm_id;
                    comm.Parameters.Add("@tdf_comentarios", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Rubros.tdf_comentarios;
                    comm.Parameters.Add("@tdf_cargo_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Rubros.tdf_cargo_id;
                    comm.CommandText += " returning tdf_id";
                    result = int.Parse(comm.ExecuteScalar().ToString());
                    Rubros.tdf_id = result;
                    Arr_Rubros[i] = Rubros;
                    comm.Parameters.Clear();
                    comm.CommandText = "";
                }
            }
            else if (ttrID == 3)
            {
                foreach (Bean_Detalle_Nota_Credito Rubros in Arr_Rubros)
                {
                    comm.CommandText = "insert into tbl_detalle_notacredito (dnc_monto, dnc_rub_id, dnc_monto_equivalente, dnc_moneda_id, dnc_ted_id, dnc_tre_id, dnc_str_id, dnc_tts_id, dnc_montosinimpuesto, dnc_impuesto) ";
                    comm.CommandText += "values (@dnc_monto, @dnc_rub_id, @dnc_monto_equivalente, @dnc_moneda_id, @dnc_ted_id, @dnc_tre_id, @dnc_str_id, @dnc_tts_id, @dnc_montosinimpuesto, @dnc_impuesto)";
                    comm.Parameters.Add("@dnc_monto", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Rubros.dnc_monto;
                    comm.Parameters.Add("@dnc_rub_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Rubros.dnc_rub_id;
                    comm.Parameters.Add("@dnc_monto_equivalente", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Rubros.dnc_monto_equivalente;
                    comm.Parameters.Add("@dnc_moneda_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Rubros.dnc_moneda_id;
                    comm.Parameters.Add("@dnc_ted_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Rubros.dnc_ted_id;
                    comm.Parameters.Add("@dnc_tre_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = refID;
                    comm.Parameters.Add("@dnc_str_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Rubros.dnc_str_id;
                    comm.Parameters.Add("@dnc_tts_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Rubros.dnc_tts_id;
                    comm.Parameters.Add("@dnc_montosinimpuesto", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Rubros.dnc_montosinimpuesto;
                    comm.Parameters.Add("@dnc_impuesto", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Rubros.dnc_impuesto;
                    result = comm.ExecuteNonQuery();
                    comm.Parameters.Clear();
                    comm.CommandText = "";
                }
            }
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            Arr_Result = new ArrayList();
            Arr_Result.Add("0");
            Arr_Result.Add("Error al Insertar Detalle de Rubros");
            return Arr_Result;
        }
        Arr_Result = new ArrayList();
        Arr_Result.Add("1");
        Arr_Result.Add(Arr_Rubros);
        return Arr_Result;
        #endregion
    }
    public static int Insertar_Libro_Diario(NpgsqlConnection conn, NpgsqlCommand comm, NpgsqlTransaction Transaction, PaisBean PaisBean, int contaID, int sucID, int ttrID, int refID, ArrayList Arr_Documento)
    {
        int result = 0;
        int Numero_Partida_INT = 0;
        string Numero_Partida_STR = "";
        try
        {
            #region Generar Numero de Partida
            //Halo el correlativo de la partida y genero su codigo
            comm.CommandText = "select spv_value from sys_partidas_value where spv_pai_id =@paiID and spv_tcon_id=@tconID";
            comm.Parameters.Add("@paiID", NpgsqlTypes.NpgsqlDbType.Integer).Value = PaisBean.ID;
            comm.Parameters.Add("@tconID", NpgsqlTypes.NpgsqlDbType.Integer).Value = contaID;
            Numero_Partida_INT = int.Parse(comm.ExecuteScalar().ToString());
            comm.Parameters.Clear();
            Numero_Partida_STR = Utility.GeneroPartida(PaisBean.ISO, Numero_Partida_INT, contaID);
            Numero_Partida_INT += 1;
            //Actualizo el correlativo de la partida
            comm.CommandText = "update sys_partidas_value set spv_value=@value where spv_pai_id=@paiID and spv_tcon_id=@tconID";
            comm.Parameters.Add("@paiID", NpgsqlTypes.NpgsqlDbType.Integer).Value = PaisBean.ID;
            comm.Parameters.Add("@value", NpgsqlTypes.NpgsqlDbType.Integer).Value = Numero_Partida_INT;
            comm.Parameters.Add("@tconID", NpgsqlTypes.NpgsqlDbType.Integer).Value = contaID;
            comm.ExecuteNonQuery();
            comm.Parameters.Clear();
            #endregion
            if (ttrID == 1)
            {
                Bean_Factura_Automatica Factura_Automatica = (Bean_Factura_Automatica)Arr_Documento[0];
                foreach (Bean_Detalle_Rubros Rubros in Factura_Automatica.Arr_Detalle_Facturacion)
                {
                    foreach (RE_GenericBean Bean in Rubros.cta_haber)
                    {
                        comm.CommandText = "insert into tbl_libro_diario (tdi_cue_id, tdi_ref_id, tdi_fecha, tdi_hora, tdi_usu_id, tdi_haber, tdi_ttr_id, tdi_pai_id, tdi_suc_id, tdi_tipo_cambio, tdi_moneda_id, tdi_no_partida, tdi_fecha_documento, tdi_tcon_id, tdi_ttt_id, tdi_persona_id, tdi_tpi_id)";
                        comm.CommandText += " values (@tdi_cue_id, @tdi_ref_id, current_date, current_time, @tdi_usu_id, @tdi_haber, @tdi_ttr_id, @tdi_pai_id, @tdi_suc_id, @tdi_tipo_cambio, @tdi_moneda_id, @tdi_no_partida, current_date, @tdi_tcon_id, @tdi_ttt_id, @tdi_persona_id, @tdi_tpi_id)";
                        comm.Parameters.Add("@tdi_cue_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC1;
                        comm.Parameters.Add("@tdi_ref_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_id;
                        comm.Parameters.Add("@tdi_usu_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_usu_id;
                        comm.Parameters.Add("@tdi_haber", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Rubros.tdf_montosinimpuesto;
                        comm.Parameters.Add("@tdi_ttr_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = 1;
                        comm.Parameters.Add("@tdi_pai_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_pai_id;
                        comm.Parameters.Add("@tdi_suc_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_suc_id;
                        comm.Parameters.Add("@tdi_tipo_cambio", NpgsqlTypes.NpgsqlDbType.Real).Value = DB.getTipoCambioHoy(Factura_Automatica.tfa_pai_id);
                        comm.Parameters.Add("@tdi_moneda_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_moneda;
                        comm.Parameters.Add("@tdi_no_partida", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Numero_Partida_STR;
                        comm.Parameters.Add("@tdi_tcon_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_conta_id;
                        comm.Parameters.Add("@tdi_ttt_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_ttt_id;
                        comm.Parameters.Add("@tdi_persona_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_cli_id;
                        comm.Parameters.Add("@tdi_tpi_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_tpi_id;
                        result = comm.ExecuteNonQuery();
                        comm.Parameters.Clear();
                    }
                }
                foreach (RE_GenericBean Bean in Factura_Automatica.ctas_abono)
                {
                    comm.CommandText = "insert into tbl_libro_diario (tdi_cue_id, tdi_ref_id, tdi_fecha, tdi_hora, tdi_usu_id, tdi_debe, tdi_ttr_id, tdi_pai_id, tdi_suc_id, tdi_tipo_cambio, tdi_moneda_id, tdi_no_partida, tdi_fecha_documento, tdi_tcon_id, tdi_ttt_id, tdi_persona_id, tdi_tpi_id)";
                    comm.CommandText += " values (@tdi_cue_id, @tdi_ref_id, current_date, current_time, @tdi_usu_id, @tdi_debe, @tdi_ttr_id, @tdi_pai_id, @tdi_suc_id, @tdi_tipo_cambio, @tdi_moneda_id, @tdi_no_partida, current_date, @tdi_tcon_id, @tdi_ttt_id, @tdi_persona_id, @tdi_tpi_id)";
                    comm.Parameters.Add("@tdi_cue_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC1;
                    comm.Parameters.Add("@tdi_ref_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_id;
                    comm.Parameters.Add("@tdi_usu_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_usu_id;
                    comm.Parameters.Add("@tdi_debe", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Factura_Automatica.tfa_total;
                    comm.Parameters.Add("@tdi_ttr_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = 1;
                    comm.Parameters.Add("@tdi_pai_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_pai_id;
                    comm.Parameters.Add("@tdi_suc_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_suc_id;
                    comm.Parameters.Add("@tdi_tipo_cambio", NpgsqlTypes.NpgsqlDbType.Real).Value = DB.getTipoCambioHoy(Factura_Automatica.tfa_pai_id);
                    comm.Parameters.Add("@tdi_moneda_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_moneda;
                    comm.Parameters.Add("@tdi_no_partida", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Numero_Partida_STR;
                    comm.Parameters.Add("@tdi_tcon_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_conta_id;
                    comm.Parameters.Add("@tdi_ttt_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_ttt_id;
                    comm.Parameters.Add("@tdi_persona_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_cli_id;
                    comm.Parameters.Add("@tdi_tpi_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_tpi_id;
                    comm.Parameters.Add("", NpgsqlTypes.NpgsqlDbType.Box).Value = 
                    result = comm.ExecuteNonQuery();
                    comm.Parameters.Clear();
                }
                if (Factura_Automatica.tfa_impuesto > 0)
                {
                    comm.CommandText = "insert into tbl_libro_diario (tdi_cue_id, tdi_ref_id, tdi_fecha, tdi_hora, tdi_usu_id, tdi_haber, tdi_ttr_id, tdi_pai_id, tdi_suc_id, tdi_tipo_cambio, tdi_moneda_id, tdi_no_partida, tdi_fecha_documento, tdi_tcon_id, tdi_ttt_id, tdi_persona_id, tdi_tpi_id)";
                    comm.CommandText += " values (@tdi_cue_id, @tdi_ref_id, current_date, current_time, @tdi_usu_id, @tdi_haber, @tdi_ttr_id, @tdi_pai_id, @tdi_suc_id, @tdi_tipo_cambio, @tdi_moneda_id, @tdi_no_partida, current_date, @tdi_tcon_id, @tdi_ttt_id, @tdi_persona_id, @tdi_tpi_id)";
                    comm.Parameters.Add("@tdi_cue_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = PaisBean.ivaPaga;
                    comm.Parameters.Add("@tdi_ref_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_id;
                    comm.Parameters.Add("@tdi_usu_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Factura_Automatica.tfa_usu_id;
                    comm.Parameters.Add("@tdi_haber", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Factura_Automatica.tfa_impuesto;
                    comm.Parameters.Add("@tdi_ttr_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = 1;
                    comm.Parameters.Add("@tdi_pai_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_pai_id;
                    comm.Parameters.Add("@tdi_suc_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_suc_id;
                    comm.Parameters.Add("@tdi_tipo_cambio", NpgsqlTypes.NpgsqlDbType.Real).Value = DB.getTipoCambioHoy(Factura_Automatica.tfa_pai_id);
                    comm.Parameters.Add("@tdi_moneda_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_moneda;
                    comm.Parameters.Add("@tdi_no_partida", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Numero_Partida_STR;
                    comm.Parameters.Add("@tdi_tcon_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_conta_id;
                    comm.Parameters.Add("@tdi_ttt_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_ttt_id;
                    comm.Parameters.Add("@tdi_persona_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_cli_id;
                    comm.Parameters.Add("@tdi_tpi_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Factura_Automatica.tfa_tpi_id;
                    result = comm.ExecuteNonQuery();
                    comm.Parameters.Clear();
                }
            }
            else if (ttrID == 4)
            {
                Bean_Nota_Debito_Automatica Nota_Debito_Automatica = (Bean_Nota_Debito_Automatica)Arr_Documento[0];
                foreach (Bean_Detalle_Rubros Rubros in Nota_Debito_Automatica.Arr_Detalle_Facturacion)
                {
                    foreach (RE_GenericBean Bean in Rubros.cta_haber)
                    {
                        comm.CommandText = "insert into tbl_libro_diario (tdi_cue_id, tdi_ref_id, tdi_fecha, tdi_hora, tdi_usu_id, tdi_haber, tdi_ttr_id, tdi_pai_id, tdi_suc_id, tdi_tipo_cambio, tdi_moneda_id, tdi_no_partida, tdi_fecha_documento, tdi_tcon_id, tdi_ttt_id, tdi_persona_id, tdi_tpi_id)";
                        comm.CommandText += " values (@tdi_cue_id, @tdi_ref_id, current_date, current_time, @tdi_usu_id, @tdi_haber, @tdi_ttr_id, @tdi_pai_id, @tdi_suc_id, @tdi_tipo_cambio, @tdi_moneda_id, @tdi_no_partida, current_date, @tdi_tcon_id, @tdi_ttt_id, @tdi_persona_id, @tdi_tpi_id)";
                        comm.Parameters.Add("@tdi_cue_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC1;
                        comm.Parameters.Add("@tdi_ref_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_id;
                        comm.Parameters.Add("@tdi_usu_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_usu_id;
                        comm.Parameters.Add("@tdi_haber", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Rubros.tdf_montosinimpuesto;
                        comm.Parameters.Add("@tdi_ttr_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = 4;
                        comm.Parameters.Add("@tdi_pai_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_pai_id;
                        comm.Parameters.Add("@tdi_suc_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_suc_id;
                        comm.Parameters.Add("@tdi_tipo_cambio", NpgsqlTypes.NpgsqlDbType.Real).Value = DB.getTipoCambioHoy(Nota_Debito_Automatica.tnd_pai_id);
                        comm.Parameters.Add("@tdi_moneda_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_moneda;
                        comm.Parameters.Add("@tdi_no_partida", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Numero_Partida_STR;
                        comm.Parameters.Add("@tdi_tcon_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_tcon_id;
                        comm.Parameters.Add("@tdi_ttt_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_ttt_id;
                        comm.Parameters.Add("@tdi_persona_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_cli_id;
                        comm.Parameters.Add("@tdi_tpi_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_tpi_id;
                        result = comm.ExecuteNonQuery();
                        comm.Parameters.Clear();
                    }
                }
                foreach (RE_GenericBean Bean in Nota_Debito_Automatica.ctas_abono)
                {
                    comm.CommandText = "insert into tbl_libro_diario (tdi_cue_id, tdi_ref_id, tdi_fecha, tdi_hora, tdi_usu_id, tdi_debe, tdi_ttr_id, tdi_pai_id, tdi_suc_id, tdi_tipo_cambio, tdi_moneda_id, tdi_no_partida, tdi_fecha_documento, tdi_tcon_id, tdi_ttt_id, tdi_persona_id, tdi_tpi_id)";
                    comm.CommandText += " values (@tdi_cue_id, @tdi_ref_id, current_date, current_time, @tdi_usu_id, @tdi_debe, @tdi_ttr_id, @tdi_pai_id, @tdi_suc_id, @tdi_tipo_cambio, @tdi_moneda_id, @tdi_no_partida, current_date, @tdi_tcon_id, @tdi_ttt_id, @tdi_persona_id, @tdi_tpi_id)";
                    comm.Parameters.Add("@tdi_cue_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC1;
                    comm.Parameters.Add("@tdi_ref_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_id;
                    comm.Parameters.Add("@tdi_usu_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_usu_id;
                    comm.Parameters.Add("@tdi_debe", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Nota_Debito_Automatica.tnd_total;
                    comm.Parameters.Add("@tdi_ttr_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = 4;
                    comm.Parameters.Add("@tdi_pai_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_pai_id;
                    comm.Parameters.Add("@tdi_suc_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_suc_id;
                    comm.Parameters.Add("@tdi_tipo_cambio", NpgsqlTypes.NpgsqlDbType.Real).Value = DB.getTipoCambioHoy(Nota_Debito_Automatica.tnd_pai_id);
                    comm.Parameters.Add("@tdi_moneda_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_moneda;
                    comm.Parameters.Add("@tdi_no_partida", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Numero_Partida_STR;
                    comm.Parameters.Add("@tdi_tcon_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_tcon_id;
                    comm.Parameters.Add("@tdi_ttt_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_ttt_id;
                    comm.Parameters.Add("@tdi_persona_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_cli_id;
                    comm.Parameters.Add("@tdi_tpi_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_tpi_id;
                    result = comm.ExecuteNonQuery();
                    comm.Parameters.Clear();
                }
                if (Nota_Debito_Automatica.tnd_impuesto > 0)
                {
                    comm.CommandText = "insert into tbl_libro_diario (tdi_cue_id, tdi_ref_id, tdi_fecha, tdi_hora, tdi_usu_id, tdi_haber, tdi_ttr_id, tdi_pai_id, tdi_suc_id, tdi_tipo_cambio, tdi_moneda_id, tdi_no_partida, tdi_fecha_documento, tdi_tcon_id, tdi_ttt_id, tdi_persona_id, tdi_tpi_id)";
                    comm.CommandText += " values (@tdi_cue_id, @tdi_ref_id, current_date, current_time, @tdi_usu_id, @tdi_haber, @tdi_ttr_id, @tdi_pai_id, @tdi_suc_id, @tdi_tipo_cambio, @tdi_moneda_id, @tdi_no_partida, current_date, @tdi_tcon_id, @tdi_ttt_id, @tdi_persona_id, @tdi_tpi_id)";
                    comm.Parameters.Add("@tdi_cue_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = PaisBean.ivaCobra;
                    comm.Parameters.Add("@tdi_ref_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_id;
                    comm.Parameters.Add("@tdi_usu_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Nota_Debito_Automatica.tnd_usu_id;
                    comm.Parameters.Add("@tdi_haber", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Nota_Debito_Automatica.tnd_impuesto;
                    comm.Parameters.Add("@tdi_ttr_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = 4;
                    comm.Parameters.Add("@tdi_pai_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_pai_id;
                    comm.Parameters.Add("@tdi_suc_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_suc_id;
                    comm.Parameters.Add("@tdi_tipo_cambio", NpgsqlTypes.NpgsqlDbType.Real).Value = DB.getTipoCambioHoy(Nota_Debito_Automatica.tnd_pai_id);
                    comm.Parameters.Add("@tdi_moneda_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_moneda;
                    comm.Parameters.Add("@tdi_no_partida", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Numero_Partida_STR;
                    comm.Parameters.Add("@tdi_tcon_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_tcon_id;
                    comm.Parameters.Add("@tdi_ttt_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_ttt_id;
                    comm.Parameters.Add("@tdi_persona_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_cli_id;
                    comm.Parameters.Add("@tdi_tpi_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Nota_Debito_Automatica.tnd_tpi_id;
                    result = comm.ExecuteNonQuery();
                    comm.Parameters.Clear();
                }
            }
            else if (ttrID == 5)
            {
                Bean_Provision_Automatica Provision_Automatica = (Bean_Provision_Automatica)Arr_Documento[0];
                foreach (Bean_Detalle_Rubros Rubros in Provision_Automatica.Arr_Detalle_Provision)
                {
                    foreach (RE_GenericBean Bean in Rubros.cta_debe)
                    {
                        comm.CommandText = "insert into tbl_libro_diario (tdi_cue_id, tdi_ref_id, tdi_fecha, tdi_hora, tdi_usu_id, tdi_debe, tdi_ttr_id, tdi_pai_id, tdi_suc_id, tdi_tipo_cambio, tdi_moneda_id, tdi_no_partida, tdi_fecha_documento, tdi_tcon_id, tdi_ttt_id, tdi_persona_id, tdi_tpi_id)";
                        comm.CommandText += " values (@tdi_cue_id, @tdi_ref_id, current_date, current_time, @tdi_usu_id, @tdi_debe, @tdi_ttr_id, @tdi_pai_id, @tdi_suc_id, @tdi_tipo_cambio, @tdi_moneda_id, @tdi_no_partida, current_date, @tdi_tcon_id, @tdi_ttt_id, @tdi_persona_id, @tdi_tpi_id)";
                        comm.Parameters.Add("@tdi_cue_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC1;
                        comm.Parameters.Add("@tdi_ref_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_prov_id;
                        comm.Parameters.Add("@tdi_usu_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Provision_Automatica.tpr_usu_creacion;
                        comm.Parameters.Add("@tdi_debe", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Rubros.tdf_montosinimpuesto;
                        comm.Parameters.Add("@tdi_ttr_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = 5;
                        comm.Parameters.Add("@tdi_pai_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_pai_id;
                        comm.Parameters.Add("@tdi_suc_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_suc_id;
                        comm.Parameters.Add("@tdi_tipo_cambio", NpgsqlTypes.NpgsqlDbType.Real).Value = DB.getTipoCambioHoy(Provision_Automatica.tpr_pai_id);
                        comm.Parameters.Add("@tdi_moneda_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_mon_id;
                        comm.Parameters.Add("@tdi_no_partida", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Numero_Partida_STR;
                        comm.Parameters.Add("@tdi_tcon_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_tcon_id;
                        comm.Parameters.Add("@tdi_ttt_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_ttt_id;
                        comm.Parameters.Add("@tdi_persona_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_proveedor_id;
                        comm.Parameters.Add("@tdi_tpi_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_tpi_id;
                        result = comm.ExecuteNonQuery();
                        comm.Parameters.Clear();
                    }
                }
                foreach (RE_GenericBean Bean in Provision_Automatica.ctas_cargo)
                {
                    comm.CommandText = "insert into tbl_libro_diario (tdi_cue_id, tdi_ref_id, tdi_fecha, tdi_hora, tdi_usu_id, tdi_haber, tdi_ttr_id, tdi_pai_id, tdi_suc_id, tdi_tipo_cambio, tdi_moneda_id, tdi_no_partida, tdi_fecha_documento, tdi_tcon_id, tdi_ttt_id, tdi_persona_id, tdi_tpi_id)";
                    comm.CommandText += " values (@tdi_cue_id, @tdi_ref_id, current_date, current_time, @tdi_usu_id, @tdi_haber, @tdi_ttr_id, @tdi_pai_id, @tdi_suc_id, @tdi_tipo_cambio, @tdi_moneda_id, @tdi_no_partida, current_date, @tdi_tcon_id, @tdi_ttt_id, @tdi_persona_id, @tdi_tpi_id)";
                    comm.Parameters.Add("@tdi_cue_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC1;
                    comm.Parameters.Add("@tdi_ref_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_prov_id;
                    comm.Parameters.Add("@tdi_usu_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Provision_Automatica.tpr_usu_creacion;
                    comm.Parameters.Add("@tdi_haber", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Provision_Automatica.tpr_valor;
                    comm.Parameters.Add("@tdi_ttr_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = 5;
                    comm.Parameters.Add("@tdi_pai_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_pai_id;
                    comm.Parameters.Add("@tdi_suc_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_suc_id;
                    comm.Parameters.Add("@tdi_tipo_cambio", NpgsqlTypes.NpgsqlDbType.Real).Value = DB.getTipoCambioHoy(Provision_Automatica.tpr_pai_id);
                    comm.Parameters.Add("@tdi_moneda_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_mon_id;
                    comm.Parameters.Add("@tdi_no_partida", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Numero_Partida_STR;
                    comm.Parameters.Add("@tdi_tcon_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_tcon_id;
                    comm.Parameters.Add("@tdi_ttt_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_ttt_id;
                    comm.Parameters.Add("@tdi_persona_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_proveedor_id;
                    comm.Parameters.Add("@tdi_tpi_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_tpi_id;
                    result = comm.ExecuteNonQuery();
                    comm.Parameters.Clear();
                }
                if (Provision_Automatica.tpr_iva > 0)
                {
                    comm.CommandText = "insert into tbl_libro_diario (tdi_cue_id, tdi_ref_id, tdi_fecha, tdi_hora, tdi_usu_id, tdi_debe, tdi_ttr_id, tdi_pai_id, tdi_suc_id, tdi_tipo_cambio, tdi_moneda_id, tdi_no_partida, tdi_fecha_documento, tdi_tcon_id, tdi_ttt_id, tdi_persona_id, tdi_tpi_id)";
                    comm.CommandText += " values (@tdi_cue_id, @tdi_ref_id, current_date, current_time, @tdi_usu_id, @tdi_debe, @tdi_ttr_id, @tdi_pai_id, @tdi_suc_id, @tdi_tipo_cambio, @tdi_moneda_id, @tdi_no_partida, current_date, @tdi_tcon_id, @tdi_ttt_id, @tdi_persona_id, @tdi_tpi_id)";
                    comm.Parameters.Add("@tdi_cue_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = PaisBean.ivaCobra;
                    comm.Parameters.Add("@tdi_ref_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_prov_id;
                    comm.Parameters.Add("@tdi_usu_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Provision_Automatica.tpr_usu_creacion;
                    comm.Parameters.Add("@tdi_debe", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Provision_Automatica.tpr_iva;
                    comm.Parameters.Add("@tdi_ttr_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = 5;
                    comm.Parameters.Add("@tdi_pai_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_pai_id;
                    comm.Parameters.Add("@tdi_suc_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_suc_id;
                    comm.Parameters.Add("@tdi_tipo_cambio", NpgsqlTypes.NpgsqlDbType.Real).Value = DB.getTipoCambioHoy(Provision_Automatica.tpr_pai_id);
                    comm.Parameters.Add("@tdi_moneda_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_mon_id;
                    comm.Parameters.Add("@tdi_no_partida", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Numero_Partida_STR;
                    comm.Parameters.Add("@tdi_tcon_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_tcon_id;
                    comm.Parameters.Add("@tdi_ttt_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_ttt_id;
                    comm.Parameters.Add("@tdi_persona_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_proveedor_id;
                    comm.Parameters.Add("@tdi_tpi_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Provision_Automatica.tpr_tpi_id;
                    result = comm.ExecuteNonQuery();
                    comm.Parameters.Clear();
                }
            }
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return result;
    }
    public static Bean_Datos_BL Get_DatosBL_X_Traficos(int SisID, int ttoID, int blID, PaisBean Pais_Bean)
    {
        #region Get Datos BL por Cada Trafico
        Bean_Datos_BL Datos_BL = null;
        string Fecha_Arranque = "";
        Fecha_Arranque = Obtener_Fecha_Arranque(Pais_Bean.schema);
        if (SisID == 1)
        {
            #region Cargar Datos Sistema Maritimo
            NpgsqlConnection con_Maritimo;
            NpgsqlCommand com_Maritimo;
            NpgsqlDataReader reader_Maritimo;
            try
            {
                con_Maritimo = DB.OpenVentasConnection(Pais_Bean.schema);
                com_Maritimo = new NpgsqlCommand();
                com_Maritimo.Connection = con_Maritimo;
                com_Maritimo.CommandType = CommandType.Text;
                if (ttoID == 1)
                {
                    #region FCL BK
                    //com_Maritimo.CommandText = "select a.mbl, a.no_bl, numero_routing, b.no_contenedor, a.id_cliente as cliente, a.id_naviera, a.agente_id, a.id_cliente as consignatario, a.id_shipper, a.vapor, " +
                    //"b.peso, b.volumen, b.comodity_id, b.no_piezas, a.import_export, b.id_tipo_paquete, vendedor_id, a.user_id, COALESCE(a.id_colectar, 0),  " +
                    //"to_char(a.fecha_arribo, 'yyyy-mm-dd'), to_char(a.fecha_ingreso_sistema, 'yyyy-mm-dd'), 0 as viajeID, a.no_viaje " +
                    //"from bl_completo a inner join contenedor_completo b on b.bl_id = (case when a.bl_id_ref>0 then a.bl_id_ref else a.bl_id end) " +
                    //"left join dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select id_routing, routing, vendedor_id from routings') " +
                    //"Routings_Result(routing_id bigint, numero_routing varchar, vendedor_id varchar) on (a.id_routing=routing_id) " +
                    //"where a.activo=true and b.activo=true " +
                    //"and a.bl_id=" + blID + " ";
                    #endregion
                    #region FCL
                    com_Maritimo.CommandText = "select a.mbl, a.no_bl, numero_routing, b.no_contenedor, a.id_cliente as cliente, a.id_naviera_representante, a.agente_id, a.id_cliente as consignatario, a.id_shipper, a.vapor, " +
                    "b.peso, b.volumen, b.comodity_id, b.no_piezas, a.import_export, b.id_tipo_paquete, vendedor_id, a.user_id, COALESCE(a.id_colectar, 0),  " +
                    "to_char(a.fecha_arribo, 'yyyy-mm-dd'), to_char(a.fecha_ingreso_sistema, 'yyyy-mm-dd'), 0 as viajeID, a.no_viaje, a.id_puerto_origen " +
                    "from bl_completo a inner join contenedor_completo b on b.bl_id = (case when a.bl_id_ref>0 then a.bl_id_ref else a.bl_id end) " +
                    "left join dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select id_routing, routing, vendedor_id from routings') " +
                    "Routings_Result(routing_id bigint, numero_routing varchar, vendedor_id varchar) on (a.id_routing=routing_id) " +
                    "where a.activo=true and b.activo=true " +
                    "and a.bl_id=" + blID + " ";
                    #endregion
                }
                else if (ttoID == 2)
                {
                    #region LCL BK
                    //com_Maritimo.CommandText = "select b.mbl, a.no_bl, numero_routing, b.no_contenedor, a.id_cliente as cliente, c.id_naviera, c.agente_id, a.id_cliente as consignatario, a.id_shipper, c.vapor, a.peso, a.volumen, a.comodity_id, a.no_piezas, c.import_export, a.id_tipo_paquete, vendedor_id, c.user_id, COALESCE(a.id_colectar, 0), to_char(c.fecha_arribo, 'yyyy-mm-dd'), to_char(a.fecha_ingreso_sistema, 'yyyy-mm-dd'), c.viaje_id, c.no_viaje " +
                    //"from bill_of_lading as a inner join viaje_contenedor as b on (b.viaje_contenedor_id=a.viaje_contenedor_id and b.activo=true) " +
                    //"inner join viajes as c on (c.viaje_id=b.viaje_id and c.activo=true) " +
                    //"left join dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select id_routing, routing, vendedor_id from routings') " +
                    //"Routings_Result(routing_id bigint, numero_routing varchar, vendedor_id varchar) on (a.id_routing=routing_id) " +
                    //"where a.activo=true and a.bl_id=" + blID + " ";
                    #endregion
                    #region LCL
                    com_Maritimo.CommandText = "select b.mbl, a.no_bl, numero_routing, b.no_contenedor, a.id_cliente as cliente, c.id_naviera_representante, c.agente_id, a.id_cliente as consignatario, a.id_shipper, c.vapor, a.peso, a.volumen, a.comodity_id, a.no_piezas, c.import_export, a.id_tipo_paquete, vendedor_id, c.user_id, COALESCE(a.id_colectar, 0), to_char(c.fecha_arribo, 'yyyy-mm-dd'), to_char(a.fecha_ingreso_sistema, 'yyyy-mm-dd'), c.viaje_id, c.no_viaje, c.id_puerto_origen " +
                    "from bill_of_lading as a inner join viaje_contenedor as b on (b.viaje_contenedor_id=a.viaje_contenedor_id and b.activo=true) " +
                    "inner join viajes as c on (c.viaje_id=b.viaje_id and c.activo=true) " +
                    "left join dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select id_routing, routing, vendedor_id from routings') " +
                    "Routings_Result(routing_id bigint, numero_routing varchar, vendedor_id varchar) on (a.id_routing=routing_id) " +
                    "where a.activo=true and a.bl_id=" + blID + " ";
                    #endregion
                }
                reader_Maritimo = com_Maritimo.ExecuteReader();
                while (reader_Maritimo.Read())
                {
                    Datos_BL = new Bean_Datos_BL();
                    Datos_BL.Mbl = reader_Maritimo.GetValue(0).ToString().Trim();
                    Datos_BL.Hbl = reader_Maritimo.GetValue(1).ToString().Trim();
                    Datos_BL.Routing = reader_Maritimo.GetValue(2).ToString();
                    Datos_BL.Contenedor = reader_Maritimo.GetValue(3).ToString();
                    Datos_BL.Cliente = int.Parse(reader_Maritimo.GetValue(4).ToString());
                    Datos_BL.Naviera = int.Parse(reader_Maritimo.GetValue(5).ToString());
                    Datos_BL.Agente = int.Parse(reader_Maritimo.GetValue(6).ToString());
                    Datos_BL.Consignatario = int.Parse(reader_Maritimo.GetValue(7).ToString());
                    Datos_BL.Shipper = int.Parse(reader_Maritimo.GetValue(8).ToString());
                    Datos_BL.Vapor = reader_Maritimo.GetValue(9).ToString();
                    Datos_BL.Peso = reader_Maritimo.GetValue(10).ToString();
                    Datos_BL.Volumen = reader_Maritimo.GetValue(11).ToString();
                    Datos_BL.Comodity = int.Parse(reader_Maritimo.GetValue(12).ToString());
                    Datos_BL.No_Piezas = int.Parse(reader_Maritimo.GetValue(13).ToString());
                    if (bool.Parse(reader_Maritimo.GetValue(14).ToString()) == true)
                    {
                        Datos_BL.Import_Export = 1;
                    }
                    else
                    {
                        Datos_BL.Import_Export = 2;
                    }
                    Datos_BL.Tipo_Paquete = int.Parse(reader_Maritimo.GetValue(15).ToString());
                    Datos_BL.ttoID = ttoID;
                    Datos_BL.BLID = blID;
                    Datos_BL.Vendedor1 = DB.GetVendedorByRouting(Datos_BL.Routing);
                    string sql = "select pw_name from usuarios_empresas where id_usuario=" + reader_Maritimo.GetValue(17).ToString();
                    Datos_BL.Vendedor2 = DB.getName(sql);
                    Datos_BL.Cliente = int.Parse(reader_Maritimo.GetValue(18).ToString());
                    Datos_BL.Fecha_Arribo = reader_Maritimo.GetValue(19).ToString();
                    Datos_BL.Fecha_Ingreso_Sistema = reader_Maritimo.GetValue(20).ToString();
                    Datos_BL.viajeID = int.Parse(reader_Maritimo.GetValue(21).ToString());
                    Datos_BL.No_Viaje = reader_Maritimo.GetValue(22).ToString();
                    Datos_BL.Puerto_Embarque_ID = int.Parse(reader_Maritimo.GetValue(23).ToString());
                }
                DB.CloseObj(reader_Maritimo, com_Maritimo, con_Maritimo);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                return null;
            }
            #endregion
        }
        else if (SisID == 2)
        {
            #region Cargar Datos Sistema Aereo
            MySqlConnection con_Aereo;
            MySqlCommand com_Aereo;
            MySqlDataReader reader_Aereo;
            try
            {
                con_Aereo = DB.OpenAereoConnection();
                com_Aereo = new MySqlCommand();
                com_Aereo.Connection = con_Aereo;
                com_Aereo.CommandType = CommandType.Text;
                if (ttoID == 3)
                {
                    #region Importacion
                    com_Aereo.CommandText = "select a.AWBNumber, a.HAWBNumber, 'Numero_Routing', 'Numero_Contenedor', a.ConsignerID as Cliente, " +
                    "a.CarrierID as Linea_Aerea, a.AgentID, a.ConsignerID, a.ShipperID, 'Vapor', " +
                    "CAST(a.TotWeight AS DECIMAL) as Peso, CAST(a.TotWeightChargeable AS DECIMAL) as Volumen, a.Commodities, " +
                    "a.TotNoOfPieces, 'Import', 'Bultos', a.RoutingID, a.UserID, a.ClientCollectID from Awbi a where a.AWBID=" + blID + ";";
                    #endregion
                }
                else if (ttoID == 4)
                {
                    #region Exportacion
                    com_Aereo.CommandText = "select a.AWBNumber, a.HAWBNumber, 'Numero_Routing', 'Numero_Contenedor', a.ConsignerID as Cliente, " +
                    "a.CarrierID as Linea_Aerea, a.AgentID, a.ConsignerID, a.ShipperID, 'Vapor', " +
                    "CAST(a.TotWeight AS DECIMAL) as Peso, CAST(a.TotWeightChargeable AS DECIMAL) as Volumen, a.Commodities, " +
                    "a.TotNoOfPieces, 'Import', 'Bultos', a.RoutingID, a.UserID, a.ClientCollectID from Awb a where a.AWBID=" + blID + ";";
                    #endregion
                }
                reader_Aereo = com_Aereo.ExecuteReader();
                while (reader_Aereo.Read())
                {
                    Datos_BL = new Bean_Datos_BL();
                    Datos_BL.Mbl = reader_Aereo.GetValue(0).ToString();
                    Datos_BL.Hbl = reader_Aereo.GetValue(1).ToString();
                    Datos_BL.Routing = reader_Aereo.GetValue(2).ToString();
                    Datos_BL.Contenedor = reader_Aereo.GetValue(3).ToString();
                    Datos_BL.Cliente = int.Parse(reader_Aereo.GetValue(4).ToString());
                    Datos_BL.Naviera = int.Parse(reader_Aereo.GetValue(5).ToString());
                    Datos_BL.Agente = int.Parse(reader_Aereo.GetValue(6).ToString());
                    Datos_BL.Consignatario = int.Parse(reader_Aereo.GetValue(7).ToString());
                    Datos_BL.Shipper = int.Parse(reader_Aereo.GetValue(8).ToString());
                    Datos_BL.Vapor = reader_Aereo.GetValue(9).ToString();
                    Datos_BL.Peso = reader_Aereo.GetValue(10).ToString();
                    Datos_BL.Volumen = reader_Aereo.GetValue(11).ToString();
                    Datos_BL.Comodity = int.Parse(reader_Aereo.GetValue(12).ToString());
                    if (ttoID == 3)
                    {
                        Datos_BL.Import_Export = 1;
                    }
                    else
                    {
                        Datos_BL.Import_Export = 2;
                    }
                    //Datos_BL.No_Piezas = int.Parse(reader_Aereo.GetValue(13).ToString());
                    double variable = double.Parse(reader_Aereo.GetValue(13).ToString());
                    Datos_BL.No_Piezas = Convert.ToInt32(double.Parse(reader_Aereo.GetValue(13).ToString()));
                    Datos_BL.Tipo_Paquete = 4;//4 = Bultos
                    Datos_BL.ttoID = ttoID;
                    Datos_BL.BLID = blID;
                    string routingID = "";
                    string userID = "";
                    routingID = reader_Aereo.GetValue(16).ToString();
                    userID = reader_Aereo.GetValue(17).ToString();
                    if ((routingID != "") && (routingID != "0"))
                    {
                        Datos_BL.Routing = DB.getRouting(int.Parse(routingID));
                        Datos_BL.Vendedor1 = DB.GetVendedorByRouting(Datos_BL.Routing);
                    }
                    if ((userID != "") && (userID != "0"))
                    {
                        string sql = "select pw_name from usuarios_empresas where id_usuario=" + userID;
                        Datos_BL.Vendedor2 = DB.getName(sql);
                    }
                    Datos_BL.Cliente = int.Parse(reader_Aereo.GetValue(18).ToString());
                }
                DB.CloseMySQLObj(reader_Aereo, com_Aereo, con_Aereo);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                return null;
            }
            #endregion
        }
        else if (SisID == 3)
        {
            #region Cargar Datos Sistema Terrestre
            MySqlConnection con_Terrestre;
            MySqlCommand com_Terrestre;
            MySqlDataReader reader_Terrestre;
            try
            {
                con_Terrestre = DB.OpenTerrestreConnection();
                com_Terrestre = new MySqlCommand();
                com_Terrestre.Connection = con_Terrestre;
                com_Terrestre.CommandType = CommandType.Text;
                if ((ttoID == 5) || (ttoID == 6) || (ttoID == 7))
                {
                    #region Express-Consolidado-Local
                    string paisISO = "";
                    #region Definir Pais ISO
                    if (Pais_Bean.ID == 11)
                    {
                        paisISO = "N1";
                    }
                    else
                    {
                        paisISO = Pais_Bean.ISO;
                    }
                    #endregion
                    #region bkp
                    /*com_Terrestre.CommandText = "select b.BLNumber, a.HBLNumber, " +
                    "(select BLs from BLDetail where ExType in (4,5,6,7) and BLDetailID=" + blID + " " +
                    "union select '' from BLDetail where ExType in (8) and BLDetailID=" + blID + " " +
                    "and (MBLs REGEXP '^LL' or MBLs REGEXP '^LE')) as Routing, " +
                    "b.ContainerDep, a.ClientsID, c.ProviderID, a.AgentsID, a.ClientsID as Consignatario, " +
                    "a.ShippersID, c.TruckNo, a.Weights, a.Volumes, a.CommoditiesID, a.NoOfPieces, " +
                        "(" +
                            "select 2 as tipo from BLDetail where BLDetailID=" + blID + " and Countries='" + Pais_Bean.ISO + "' and CountriesFinalDes<>'" + Pais_Bean.ISO + "' and BLType in (0,1) " +
                            "union " +
                            "select 1 as tipo from BLDetail where BLDetailID=" + blID + " and Intransit=2 and CountriesFinalDes ='" + Pais_Bean.ISO + "' and BLType in (0,1) " +
                            "union " +
                            "select 1 as tipo from BLDetail where BLDetailID=" + blID + " and Countries='" + Pais_Bean.ISO + "' and CountriesFinalDes ='" + Pais_Bean.ISO + "' and BLType in (2) " +
                        "), " +
                    "a.ClassNoOfPieces, " +
                    "(select EXType from BLDetail where BLID=b.BLID limit 1) as EXType, " +
                    "b.UserID, a.ClientCollectID " +
                    "from BLDetail as a left join BLs as b on (b.BLID=a.BLID) " +
                    "left join Trucks as c on (b.TruckID=c.TruckID) " +
                    "where a.Expired=0 and a.BLDetailID=" + blID + ";";*/
                    #endregion
                    com_Terrestre.CommandText = "select b.BLNumber, a.HBLNumber, " +
                    "(select BLs from BLDetail where ExType in (4,5,6,7) and BLDetailID=" + blID + " " +
                    "union select '' from BLDetail where ExType in (8) and BLDetailID=" + blID + " " +
                    "and (MBLs REGEXP '^LL' or MBLs REGEXP '^LE')) as Routing, " +
                    "b.ContainerDep, a.ClientsID, c.ProviderID, a.AgentsID, a.ClientsID as Consignatario, " +
                    "a.ShippersID, c.TruckNo, a.Weights, a.Volumes, a.CommoditiesID, a.NoOfPieces, " +
                        "(" +
                            "select 1 " +
                        "), " +
                    "a.ClassNoOfPieces, " +
                    "(select EXType from BLDetail where BLID=b.BLID limit 1) as EXType, " +
                    "b.UserID, a.ClientCollectID, b.CreatedDate, b.BLArrivalDate " +
                    "from BLDetail as a left join BLs as b on (b.BLID=a.BLID) " +
                    "left join Trucks as c on (b.TruckID=c.TruckID) " +
                    "where a.Expired=0 and a.BLDetailID=" + blID + ";";
                    #endregion
                }
                reader_Terrestre = com_Terrestre.ExecuteReader();
                while (reader_Terrestre.Read())
                {
                    Datos_BL = new Bean_Datos_BL();
                    Datos_BL.Mbl = reader_Terrestre.GetValue(0).ToString();
                    Datos_BL.Hbl = reader_Terrestre.GetValue(1).ToString();
                    Datos_BL.Routing = reader_Terrestre.GetValue(2).ToString();
                    Datos_BL.Contenedor = reader_Terrestre.GetValue(3).ToString();
                    Datos_BL.Cliente = int.Parse(reader_Terrestre.GetValue(4).ToString());
                    Datos_BL.Naviera = int.Parse(reader_Terrestre.GetValue(5).ToString());
                    Datos_BL.Agente = int.Parse(reader_Terrestre.GetValue(8).ToString());
                    Datos_BL.Consignatario = int.Parse(reader_Terrestre.GetValue(7).ToString());
                    Datos_BL.Shipper = int.Parse(reader_Terrestre.GetValue(6).ToString());
                    Datos_BL.Vapor = reader_Terrestre.GetValue(9).ToString();
                    Datos_BL.Peso = reader_Terrestre.GetValue(10).ToString();
                    Datos_BL.Volumen = reader_Terrestre.GetValue(11).ToString();
                    Datos_BL.Comodity = int.Parse(reader_Terrestre.GetValue(12).ToString());
                    Datos_BL.No_Piezas = int.Parse(reader_Terrestre.GetValue(13).ToString());
                    Datos_BL.Import_Export = int.Parse(reader_Terrestre.GetValue(14).ToString());
                    Datos_BL.Paquetes = reader_Terrestre.GetValue(15).ToString();
                    Datos_BL.ttoID = ttoID;
                    Datos_BL.BLID = blID;
                    Datos_BL.Fecha_Arribo = reader_Terrestre.GetValue(20).ToString();
                    Datos_BL.Fecha_Ingreso_Sistema = reader_Terrestre.GetValue(19).ToString();
                    string EXType = reader_Terrestre.GetValue(16).ToString();
                    if ((EXType == "4") || (EXType == "5") || (EXType == "6"))
                    {
                        Datos_BL.Routing = Datos_BL.Routing;
                    }
                    else
                    {
                        Datos_BL.Routing = "";
                    }
                    if ((Datos_BL.Routing != "") && (Datos_BL.Routing != "0"))
                    {
                        Datos_BL.Vendedor1 = DB.GetVendedorByRouting(Datos_BL.Routing);
                    }
                    string userID = "";
                    userID = reader_Terrestre.GetValue(17).ToString();
                    if ((userID != "") && (userID != "0"))
                    {
                        string sql = "select pw_name from usuarios_empresas where id_usuario=" + userID;
                        Datos_BL.Vendedor2 = DB.getName(sql);
                    }
                    Datos_BL.Cliente = int.Parse(reader_Terrestre.GetValue(18).ToString());
                }
                DB.CloseMySQLObj(reader_Terrestre, com_Terrestre, con_Terrestre);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                return null;
            }
            #endregion
        }
        return Datos_BL;
        #endregion
    }
    public static ArrayList Get_BLs_X_Traficos_X_Master(int SisID, int ttoID, int blID, PaisBean Pais_Bean, string MBL)
    {
        #region Get Detalle de Carga por Cada Trafico
        ArrayList Arr_BLs = new ArrayList();
        Bean_Datos_BL Datos_BL = null;
        string Fecha_Arranque = "";
        string en_transito = "";
        string en_intermodal = "";
        string id_pais_final = "";
        string id_pais_final2 = "";
        Fecha_Arranque = Obtener_Fecha_Arranque(Pais_Bean.schema);
        if (SisID == 1)
        {
            #region Cargar Datos Sistema Maritimo
            NpgsqlConnection con_Maritimo;
            NpgsqlCommand com_Maritimo;
            NpgsqlDataReader reader_Maritimo;
            try
            {
                con_Maritimo = DB.OpenVentasConnection(Pais_Bean.schema);
                com_Maritimo = new NpgsqlCommand();
                com_Maritimo.Connection = con_Maritimo;
                com_Maritimo.CommandType = CommandType.Text;
                if (ttoID == 1)
                {
                    com_Maritimo.CommandText = "select distinct on (a.no_bl) a.bl_id, b.contenedor_id, a.mbl, a.no_bl, numero_routing, b.no_contenedor, a.id_cliente as cliente, a.id_naviera_representante, a.agente_id, a.id_cliente as consignatario, a.id_shipper, a.vapor, b.peso, b.volumen, b.comodity_id, b.no_piezas, a.import_export, b.id_tipo_paquete, vendedor_id, a.user_id, a.id_colectar, coalesce(routing_id,0), '' as id_pais_final, false as en_transito, a.id_puerto_origen  " +
                    "from bl_completo a inner join contenedor_completo b on b.bl_id = (case when a.bl_id_ref>0 then a.bl_id_ref else a.bl_id end) " +
                    "left join dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select id_routing, routing, vendedor_id from routings') " +
                    "Routings_Result(routing_id bigint, numero_routing varchar, vendedor_id varchar) on (a.id_routing=routing_id) " +
                    "where a.activo=true and b.activo=true " +
                    "and trim(a.mbl)='" + MBL + "' ";
                }
                else if (ttoID == 2)
                {
                    com_Maritimo.CommandText = "select  a.bl_id, b.viaje_contenedor_id, b.mbl, a.no_bl, numero_routing, b.no_contenedor, a.id_cliente as cliente, c.id_naviera_representante, c.agente_id, a.id_cliente as consignatario, a.id_shipper, c.vapor, a.peso, a.volumen, a.comodity_id, a.no_piezas, c.import_export, a.id_tipo_paquete, vendedor_id, c.user_id, a.id_colectar, coalesce(routing_id,0), en_transito, id_pais_final, en_intermodal, id_pais_final2, c.id_puerto_origen " +
                    "from bill_of_lading as a inner join viaje_contenedor as b on (b.viaje_contenedor_id=a.viaje_contenedor_id and b.activo=true) " +
                    "inner join viajes as c on (c.viaje_id=b.viaje_id and c.activo=true) " +
                    "left join dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select id_routing, routing, vendedor_id from routings') " +
                    "Routings_Result(routing_id bigint, numero_routing varchar, vendedor_id varchar) on (a.id_routing=routing_id) " +
                    "where a.activo=true and trim(b.mbl)='" + MBL + "' ";
                }
                reader_Maritimo = com_Maritimo.ExecuteReader();
                while (reader_Maritimo.Read())
                {
                    Datos_BL = new Bean_Datos_BL();
                    Datos_BL.BLID = int.Parse(reader_Maritimo.GetValue(0).ToString());
                    Datos_BL.ContenedorID = int.Parse(reader_Maritimo.GetValue(1).ToString());
                    Datos_BL.Mbl = reader_Maritimo.GetValue(2).ToString();
                    Datos_BL.Hbl = reader_Maritimo.GetValue(3).ToString();
                    Datos_BL.Routing = reader_Maritimo.GetValue(4).ToString();
                    Datos_BL.Contenedor = reader_Maritimo.GetValue(5).ToString();
                    Datos_BL.RoutingID = int.Parse(reader_Maritimo.GetValue(21).ToString());
                    #region Definir Destino Final
                    if (ttoID == 1)
                    {
                        Datos_BL.Destino_Final = Pais_Bean.ISO;
                        Datos_BL.Puerto_Embarque_ID = int.Parse(reader_Maritimo.GetValue(24).ToString());
                    }
                    else if (ttoID == 2)
                    {

                        en_transito = reader_Maritimo.GetValue(22).ToString();
                        en_intermodal = reader_Maritimo.GetValue(24).ToString();

                        id_pais_final = reader_Maritimo.GetValue(23).ToString();
                        id_pais_final2 = reader_Maritimo.GetValue(25).ToString();

                        Datos_BL.Puerto_Embarque_ID = int.Parse(reader_Maritimo.GetValue(26).ToString());

                        if ((en_transito.ToUpper() == "TRUE") && (en_intermodal.ToUpper() == "TRUE"))
                        {
                            Datos_BL.Destino_Final = id_pais_final;// o Datos_BL.Destino_Final = id_pais_final2
                        }
                        else if ((en_transito.ToUpper() == "TRUE") && (en_intermodal.ToUpper() == "FALSE"))
                        {
                            Datos_BL.Destino_Final = id_pais_final;
                        }
                        else if ((en_transito.ToUpper() == "FALSE") && (en_intermodal.ToUpper() == "TRUE"))
                        {
                            Datos_BL.Destino_Final = id_pais_final2;
                        }
                        else if ((en_transito.ToUpper() == "FALSE") && (en_intermodal.ToUpper() == "FALSE"))
                        {
                            Datos_BL.Destino_Final = Pais_Bean.ISO;
                        }
                        else if (en_transito.ToUpper() == "")
                        {
                            Datos_BL.Destino_Final = Pais_Bean.ISO;
                        }
                    }
                    #endregion
                    Datos_BL.Cliente = int.Parse(reader_Maritimo.GetValue(6).ToString());
                    Datos_BL.Shipper = int.Parse(reader_Maritimo.GetValue(10).ToString());
                    Datos_BL.Peso = reader_Maritimo.GetValue(12).ToString();
                    Datos_BL.Volumen = reader_Maritimo.GetValue(13).ToString();
                    if (bool.Parse(reader_Maritimo.GetValue(16).ToString()) == true)
                    {
                        Datos_BL.Import_Export = 1;
                    }
                    else
                    {
                        Datos_BL.Import_Export = 2;
                        int aux = Datos_BL.Cliente;
                        Datos_BL.Cliente = Datos_BL.Shipper;
                        Datos_BL.Shipper = aux;
                    }
                    Arr_BLs.Add(Datos_BL);
                }
                DB.CloseObj(reader_Maritimo, com_Maritimo, con_Maritimo);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                return null;
            }
            #endregion
        }
        if (SisID == 3)
        {
            #region Cargar Datos Sistema Terrestre
            MySqlConnection con_Terrestre;
            MySqlCommand com_Terrestre;
            MySqlDataReader reader_Terrestre;
            try
            {
                con_Terrestre = DB.OpenTerrestreConnection();
                com_Terrestre = new MySqlCommand();
                com_Terrestre.Connection = con_Terrestre;
                com_Terrestre.CommandType = CommandType.Text;
                if (ttoID == 5 || ttoID == 6  || ttoID == 7)
                {
                    /*com_Terrestre.CommandText = "select  a.bl_id, b.viaje_contenedor_id, b.mbl, a.no_bl, numero_routing, b.no_contenedor, a.id_cliente as cliente, c.id_naviera, c.agente_id, a.id_cliente as consignatario, a.id_shipper, c.vapor, a.peso, a.volumen, a.comodity_id, a.no_piezas, c.import_export, a.id_tipo_paquete, vendedor_id, c.user_id, a.id_colectar, coalesce(routing_id,0), en_transito, id_pais_final, en_intermodal, id_pais_final2, c.id_puerto_origen " +
                    "from bill_of_lading as a inner join viaje_contenedor as b on (b.viaje_contenedor_id=a.viaje_contenedor_id and b.activo=true) " +
                    "inner join viajes as c on (c.viaje_id=b.viaje_id and c.activo=true) " +
                    "left join dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select id_routing, routing, vendedor_id from routings') " +
                    "Routings_Result(routing_id bigint, numero_routing varchar, vendedor_id varchar) on (a.id_routing=routing_id) " +
                    "where a.activo=true and trim(b.mbl)='" + MBL + "' ";*/


                    com_Terrestre.CommandText = "select DISTINCT a.BLDetailID, b.BLID, b.BLNumber, a.HBLNumber, " +
                               "case EXType " +
                                   "when 4 then a.BLs " +
                                   "when 5 then a.BLs " +
                                   "when 6 then a.BLs " +
                                   "when 7 then a.BLs " +
                                   "else '' " +
                                "end as Routings, " +
                               "'',a.ClientsID as cliente, c.ProviderID, a.AgentsID, a.ClientsID as consignatario, a.ShippersID, '', a.Weights, a.Volumes, a.CommoditiesID, a.NoOfPieces, 1, 0, 0, a.UserID, 0, coalesce(a.EXID,0), a.InTransit, a.CountriesFinalDes, 0,  a.CountriesFinalDes, 0 " +
                               "from BLDetail as a " +
                               "inner join BLs as b on (b.BLID=a.BLID) " +
                               "inner join Trucks as c on (b.TruckID=c.TruckID) " +
                               "where a.Expired=0 and a.BLID not in (-1) and b.BLNumber = '" + MBL + "'";


                }
                
                reader_Terrestre = com_Terrestre.ExecuteReader();
                while (reader_Terrestre.Read())
                {
                    Datos_BL = new Bean_Datos_BL();
                    Datos_BL.BLID = int.Parse(reader_Terrestre.GetValue(0).ToString());
                    Datos_BL.ContenedorID = int.Parse(reader_Terrestre.GetValue(1).ToString());
                    Datos_BL.Mbl = reader_Terrestre.GetValue(2).ToString();
                    Datos_BL.Hbl = reader_Terrestre.GetValue(3).ToString();
                    Datos_BL.Routing = reader_Terrestre.GetValue(4).ToString();
                    Datos_BL.Contenedor = reader_Terrestre.GetValue(5).ToString();
                    Datos_BL.RoutingID = int.Parse(reader_Terrestre.GetValue(21).ToString());
                    #region Definir Destino Final
                    /*if (ttoID == 1)
                    {*/
                        Datos_BL.Destino_Final = Pais_Bean.ISO;
                        Datos_BL.Puerto_Embarque_ID = int.Parse(reader_Terrestre.GetValue(24).ToString());
                    /*}
                    else if (ttoID == 2)
                    {*/

                        en_transito = reader_Terrestre.GetValue(22).ToString();
                        en_intermodal = reader_Terrestre.GetValue(24).ToString();

                        id_pais_final = reader_Terrestre.GetValue(23).ToString();
                        id_pais_final2 = reader_Terrestre.GetValue(25).ToString();

                        Datos_BL.Puerto_Embarque_ID = int.Parse(reader_Terrestre.GetValue(26).ToString());

                        Datos_BL.Destino_Final = id_pais_final;

                        /*if ((en_transito.ToUpper() == "TRUE") && (en_intermodal.ToUpper() == "TRUE"))
                        {
                            Datos_BL.Destino_Final = id_pais_final;// o Datos_BL.Destino_Final = id_pais_final2
                        }
                        else if ((en_transito.ToUpper() == "TRUE") && (en_intermodal.ToUpper() == "FALSE"))
                        {
                            Datos_BL.Destino_Final = id_pais_final;
                        }
                        else if ((en_transito.ToUpper() == "FALSE") && (en_intermodal.ToUpper() == "TRUE"))
                        {
                            Datos_BL.Destino_Final = id_pais_final2;
                        }
                        else if ((en_transito.ToUpper() == "FALSE") && (en_intermodal.ToUpper() == "FALSE"))
                        {
                            Datos_BL.Destino_Final = Pais_Bean.ISO;
                        }
                        else if (en_transito.ToUpper() == "")
                        {
                            Datos_BL.Destino_Final = Pais_Bean.ISO;
                        }*/
                   /* } */
                    #endregion
                    Datos_BL.Cliente = int.Parse(reader_Terrestre.GetValue(6).ToString());
                    Datos_BL.Shipper = int.Parse(reader_Terrestre.GetValue(10).ToString());
                    Datos_BL.Peso = reader_Terrestre.GetValue(12).ToString();
                    Datos_BL.Volumen = reader_Terrestre.GetValue(13).ToString();
                    if (int.Parse(reader_Terrestre.GetValue(16).ToString()) == 1)
                    {
                        Datos_BL.Import_Export = 1;
                    }
                    else
                    {
                        Datos_BL.Import_Export = 2;
                        int aux = Datos_BL.Cliente;
                        Datos_BL.Cliente = Datos_BL.Shipper;
                        Datos_BL.Shipper = aux;
                    }
                    Arr_BLs.Add(Datos_BL);
                }
                DB.CloseMySQLObj(reader_Terrestre, com_Terrestre, con_Terrestre);
                
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                return null;
            }
            #endregion
        }
        return Arr_BLs;
        #endregion
    }
    public static ArrayList Get_CostosBL_X_Traficos(PaisBean Pais_Bean, int SisID, int Tipo, int blID, string Tipo_Contabilizacion)
    {
        #region Get Costos por Traficos
        string schema = "";
        ArrayList Arr_Costos = new ArrayList();
        Bean_Costos Bean_Costos = null;
        #region Definir Schema
        if ((Tipo == 17) || (Tipo == 18))
        {
            schema = Pais_Bean.schema_apl;
        }
        else
        {
            schema = Pais_Bean.schema;
        }
        #endregion
        string Fecha_Arranque = "";
        #region Fecha Arranque
        if (schema == "ventas_gt")
        {
            Fecha_Arranque = "2011/11/07";
        }
        else if (schema == "ventas_cr")
        {
            Fecha_Arranque = "2011/11/13";
        }
        else if (schema == "ventas_pa")
        {
            Fecha_Arranque = "2012/01/30";
        }
        else if (schema == "ventas_hn")
        {
            Fecha_Arranque = "2012/02/08";
        }
        else if (schema == "ventas_sv")
        {
            Fecha_Arranque = "2012/03/28";
        }
        else if (schema == "ventas_ni")
        {
            Fecha_Arranque = "2012/11/14";
        }
        else if (schema == "ventas_ni_grh")
        {
            Fecha_Arranque = "2012/11/14";
        }
        else if (schema == "ventas_bz")
        {
            Fecha_Arranque = "2012/12/07";
        }
        else if (schema == "ventas_gtltf")
        {
            Fecha_Arranque = "2013/08/01";
        }
        else if (schema == "ventas_crltf")
        {
            Fecha_Arranque = "2013/08/01";
        }
        else if (schema == "ventas_paltf")
        {
            Fecha_Arranque = "2013/08/01";
        }
        else if (schema == "ventas_gtapl")
        {
            Fecha_Arranque = "2013/09/01";
        }
        else if (schema == "ventas_niapl")
        {
            Fecha_Arranque = "2013/09/01";
        }
        else if (schema == "ventas_niltf")
        {
            Fecha_Arranque = "2013/10/22";
        }
        else if (schema == "ventas_svapl")
        {
            Fecha_Arranque = "2013/11/23";
        }
        else if (schema == "ventas_niapl")
        {
            Fecha_Arranque = "2013/11/23";
        }
        else if (schema == "ventas_svltf")
        {
            Fecha_Arranque = "2014/01/01";
        }
        #endregion
        if (SisID == 1)
        {
            #region Cargar Costos Sistema Maritimo
            NpgsqlConnection con_Maritimo;
            NpgsqlCommand com_Maritimo;
            NpgsqlDataReader reader_Maritimo;
            try
            {
                con_Maritimo = DB.OpenVentasConnection(schema);
                com_Maritimo = new NpgsqlCommand();
                com_Maritimo.Connection = con_Maritimo;
                com_Maritimo.CommandType = CommandType.Text;
                if ((Tipo == 1) || (Tipo == 17))
                {
                    #region FCL
                    #region Produccion
                    com_Maritimo.CommandText = "select a.bl_id, a.mbl, a.no_bl, b.id_costo_master, b.id_rubro, rubro_name, b.id_moneda, b.costo, " +
                    "b.id_tipo_prorrateo, tipo_prorrateo, b.id_tipo_proveedor, tipo_proveedor, b.id_proveedor, nombre_persona, b.orden_compra, b.referencia, b.tipo_bl, simbolo_moneda, a.import_export, " +
                    "(select x.no_contenedor from contenedor_completo as x where x.bl_id=a.bl_id limit 1 ) as no_contenedor , " +
                    "numero_routing, b.id_servicio, b.pago_terceros, (b.es_afecto + 1)  " +
                    "from ((((((((bl_completo a LEFT JOIN costos_master b on (a.bl_id=b.bl_id)) " +
                    "LEFT JOIN dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select desc_rubro_es, id_rubro from rubros') " +
                    "Rubros_Result (rubro_name varchar, rubro_id bigint) on (id_rubro=b.id_rubro)) " +
                    "LEFT JOIN dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select descripcion, id_tipo_prorrateo  from tipo_prorrateo') " +
                    "Prorrateo_Result (tipo_prorrateo varchar, id_tipoprorrateo integer) on (b.id_tipo_prorrateo=id_tipo_prorrateo)) " +
                    "LEFT JOIN dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select descripcion, id_tipo_proveedor from tipo_proveedor') " +
                    "Proveedor_Result (tipo_proveedor varchar , id_tipoproveedor integer) on (b.id_tipo_proveedor=id_tipo_proveedor)) " +
                    "LEFT JOIN dblink ('dbname=master-aimar host=10.10.1.20 port=5432  user=dbmaster password=aimargt', ' select personas.id, personas.nombre, personas.tipo from personas') " +
                    "Personas_Result (id_persona bigint, nombre_persona varchar, tipo_persona integer ) on (id_persona=b.id_proveedor)) " +
                    "LEFT JOIN dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select moneda_id, simbolo from monedas')" +
                    "Monedas_Result (moneda_id bigint, simbolo_moneda varchar) on (b.id_moneda=moneda_id))" +
                    "LEFT JOIN dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select id_routing, routing from routings')" +
                    "Routings_Result (routing_id bigint, numero_routing varchar) on (a.id_routing=routing_id)))" +
                    "where a.bl_id=" + blID + " and b.tipo_bl='F' and rubro_id=b.id_rubro and id_tipoprorrateo=b.id_tipo_prorrateo and id_tipoproveedor=b.id_tipo_proveedor and id_persona=b.id_proveedor and tipo_persona=b.id_tipo_proveedor and b.id_moneda=moneda_id and b.id_provision=0 and b.orden_compra='' and b.billingdate>='" + Fecha_Arranque + " 00:00:00' and b.activo=true order by rubro_name ";
                    #endregion
                    #endregion
                }
                else if ((Tipo == 2) || (Tipo == 18))
                {
                    #region LCL
                    #region Produccion
                    com_Maritimo.CommandText = "select b.viaje_contenedor_id, b.mbl, '', c.id_costo_master, c.id_rubro, rubro_name, c.id_moneda, c.costo, c.id_tipo_prorrateo, tipo_prorrateo, c.id_tipo_proveedor, " +
                    "tipo_proveedor, c.id_proveedor, nombre_persona, c.orden_compra, c.referencia, c.tipo_bl, simbolo_moneda, a.import_export, b.no_contenedor, '', c.id_servicio, c.pago_terceros, (c.es_afecto + 1) " +
                    "from ((((((((viajes a LEFT JOIN viaje_contenedor b on (a.viaje_id=b.viaje_id)) " +
                    "LEFT JOIN costos_master c on (b.viaje_contenedor_id=c.bl_id))) " +
                    "LEFT JOIN dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select desc_rubro_es, id_rubro from rubros') " +
                    "Rubros_Result (rubro_name varchar, rubro_id bigint) on (id_rubro=c.id_rubro)) " +
                    "LEFT JOIN dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select descripcion, id_tipo_prorrateo  from tipo_prorrateo') " +
                    "Prorrateo_Result (tipo_prorrateo varchar, id_tipoprorrateo integer) on (c.id_tipo_prorrateo=id_tipo_prorrateo)) " +
                    "LEFT JOIN dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select descripcion, id_tipo_proveedor from tipo_proveedor') " +
                    "Proveedor_Result (tipo_proveedor varchar , id_tipoproveedor integer) on (c.id_tipo_proveedor=id_tipo_proveedor)) " +
                    "LEFT JOIN dblink ('dbname=master-aimar host=10.10.1.20 port=5432  user=dbmaster password=aimargt', ' select personas.id, personas.nombre, personas.tipo from personas') " +
                    "Personas_Result (id_persona bigint, nombre_persona varchar, tipo_persona integer ) on (id_persona=c.id_proveedor)) " +
                    "LEFT JOIN dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select moneda_id, simbolo from monedas') " +
                    "Monedas_Result (moneda_id bigint, simbolo_moneda varchar) on (c.id_moneda=moneda_id)) " +
                    "where b.viaje_contenedor_id=" + blID + " and c.tipo_bl='L' and rubro_id=c.id_rubro and id_tipoprorrateo=c.id_tipo_prorrateo " +
                     "and id_tipoproveedor=c.id_tipo_proveedor and id_persona=c.id_proveedor and tipo_persona=c.id_tipo_proveedor and c.id_moneda=moneda_id and c.orden_compra='' and c.id_provision=0 and c.billingdate>='" + Fecha_Arranque + " 00:00:00' and c.activo=true order by rubro_name ";
                    #endregion
                    #endregion
                }
                reader_Maritimo = com_Maritimo.ExecuteReader();
                while (reader_Maritimo.Read())
                {
                    Bean_Costos = new Bean_Costos();
                    Bean_Costos.Costo_Tipo_Proveedor_ID = int.Parse(reader_Maritimo.GetValue(10).ToString());
                    Bean_Costos.Costo_Proveedor_ID = int.Parse(reader_Maritimo.GetValue(12).ToString());
                    Bean_Costos.Costo_Rub_ID = int.Parse(reader_Maritimo.GetValue(4).ToString());
                    Bean_Costos.Costo_Moneda_ID = int.Parse(reader_Maritimo.GetValue(6).ToString());
                    Bean_Costos.Costo_Moneda = reader_Maritimo.GetValue(17).ToString();
                    Bean_Costos.Costo_Monto = double.Parse(reader_Maritimo.GetValue(7).ToString());
                    Bean_Costos.Costo_Orden_Compra = reader_Maritimo.GetValue(14).ToString();
                    Bean_Costos.Costo_Referencia = reader_Maritimo.GetValue(15).ToString();
                    Bean_Costos.Costo_Provisionado = false;
                    Bean_Costos.Costo_ID = int.Parse(reader_Maritimo.GetValue(3).ToString());
                    Bean_Costos.Costo_Nombre_Proveedor = reader_Maritimo.GetValue(13).ToString();
                    Bean_Costos.Costo_Servicio_ID = int.Parse(reader_Maritimo.GetValue(21).ToString());
                    Bean_Costos.Costo_Pago_Terceros = int.Parse(reader_Maritimo.GetValue(22).ToString());
                    Bean_Costos.Costo_Es_Afecto = int.Parse(reader_Maritimo.GetValue(23).ToString());
                    Arr_Costos.Add(Bean_Costos);
                }
                DB.CloseObj(reader_Maritimo, com_Maritimo, con_Maritimo);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                return null;
            }
            #endregion
        }
        else if (SisID == 2)
        {
            #region Cargar Costos Sistema Aereo
            MySqlConnection con_Aereo;
            MySqlCommand com_Aereo;
            MySqlDataReader reader_Aereo;
            try
            {
                con_Aereo = DB.OpenAereoConnection();
                com_Aereo = new MySqlCommand();
                com_Aereo.Connection = con_Aereo;
                com_Aereo.CommandType = CommandType.Text;
                if (Tipo == 3)
                {
                    #region Importacion
                    if (Tipo_Contabilizacion == "Master")
                    {
                        #region Obtener Costos por Master
                        com_Aereo.CommandText = "select A.AwbID, A.AwbNumber, C.CostID, C.ItemID, C.Currency, C.Cost, C.Distribution, C.SupplierType, " +
                        "C.SupplierID, C.SupplierName, C.PurchaseOrder, C.Reference, 'True', C.ServiceID, A.HAwbNumber, A.Routing, C.ThirdParties, (C.IsAffected + 1) " +
                        "from Awbi A,Costs C where A.AwbID=C.BLID and A.AwbID=" + blID + " and C.Expired=0 and C.DocType=2 and C.ProvisionID=0 and C.PurchaseOrder='' and  C.BillingDate>='" + Fecha_Arranque + " 00:00:00' and C.Countries='" + Pais_Bean.ISO + "' ";
                        #endregion
                    }
                    else if (Tipo_Contabilizacion == "House")
                    {
                        #region Obtener Costos por House
                        com_Aereo.CommandText = "select  a.AwbID, a.AwbNumber, b.CostID, b.ItemID, b.Currency, b.Cost, b.Distribution, b.SupplierType, " +
                        "b.SupplierID, b.SupplierName, b.PurchaseOrder, b.Reference, 'True', b.ServiceID, d.HAWBNumber, a.Routing, b.ThirdParties, (b.IsAffected + 1) " +
                        "from Awbi a, Costs b, CostsDetail c, Awbi d " +
                        "where a.AwbID=b.BLID and b.Expired=0 and b.CostID=c.CostID and b.PurchaseOrder='' and  b.BillingDate>='" + Fecha_Arranque + " 00:00:00' and b.Countries='" + Pais_Bean.ISO + "' " +
                        "and c.SBLID=d.AwbID " +
                        "and b.DocType=2 " +
                        "and a.AwbID=" + blID + "";
                        #endregion
                    }
                    #endregion
                }
                else if (Tipo == 4)
                {
                    #region Exportacion
                    if (Tipo_Contabilizacion == "Master")
                    {
                        #region Obtener Costos por Master
                        com_Aereo.CommandText = "select A.AwbID, A.AwbNumber, C.CostID, C.ItemID, C.Currency, C.Cost, C.Distribution, C.SupplierType, " +
                        "C.SupplierID, C.SupplierName, C.PurchaseOrder, C.Reference, 'False', C.ServiceID, A.HAwbNumber, A.Routing, C.ThirdParties, (C.IsAffected + 1)  " +
                        "from Awb A,Costs C where A.AwbID=C.BLID and A.AwbID=" + blID + " and C.Expired=0 and C.DocType=1 and C.ProvisionID=0 and C.PurchaseOrder='' and  C.BillingDate>='" + Fecha_Arranque + " 00:00:00' and C.Countries='" + Pais_Bean.ISO + "' ";
                        #endregion
                    }
                    else if (Tipo_Contabilizacion == "House")
                    {
                        #region Obtener Costos por House
                        com_Aereo.CommandText = "select  a.AwbID, a.AwbNumber, b.CostID, b.ItemID, b.Currency, b.Cost, b.Distribution, b.SupplierType, " +
                        "b.SupplierID, b.SupplierName, b.PurchaseOrder, b.Reference, 'False', b.ServiceID, d.HAWBNumber, a.Routing, b.ThirdParties, (b.IsAffected + 1) " +
                        "from Awb a, Costs b, CostsDetail c, Awb d " +
                        "where a.AwbID=b.BLID and b.Expired=0 and b.CostID=c.CostID and b.PurchaseOrder='' and  b.BillingDate>='" + Fecha_Arranque + " 00:00:00' and b.Countries='" + Pais_Bean.ISO + "' " +
                        "and c.SBLID=d.AwbID " +
                        "and b.DocType=1 " +
                        "and a.AwbID=" + blID + "";
                        #endregion
                    }
                    #endregion
                }
                reader_Aereo = com_Aereo.ExecuteReader();
                while (reader_Aereo.Read())
                {
                    Bean_Costos = new Bean_Costos();
                    Bean_Costos.Costo_Tipo_Proveedor_ID = int.Parse(reader_Aereo.GetValue(7).ToString());
                    Bean_Costos.Costo_Proveedor_ID = int.Parse(reader_Aereo.GetValue(8).ToString());
                    Bean_Costos.Costo_Rub_ID = int.Parse(reader_Aereo.GetValue(3).ToString());
                    Bean_Costos.Costo_Moneda_ID = 0;
                    Bean_Costos.Costo_Moneda = reader_Aereo.GetValue(4).ToString();
                    Bean_Costos.Costo_Monto = double.Parse(reader_Aereo.GetValue(5).ToString());
                    Bean_Costos.Costo_Orden_Compra = reader_Aereo.GetValue(10).ToString();
                    Bean_Costos.Costo_Referencia = reader_Aereo.GetValue(11).ToString();
                    Bean_Costos.Costo_Provisionado = false;
                    Bean_Costos.Costo_ID = int.Parse(reader_Aereo.GetValue(2).ToString());
                    Bean_Costos.Costo_Nombre_Proveedor = reader_Aereo.GetValue(9).ToString();
                    Bean_Costos.Costo_Servicio_ID = int.Parse(reader_Aereo.GetValue(13).ToString());
                    Bean_Costos.Costo_Pago_Terceros = int.Parse(reader_Aereo.GetValue(16).ToString());
                    Bean_Costos.Costo_Es_Afecto = int.Parse(reader_Aereo.GetValue(17).ToString());
                    Bean_Costos.Costo_Documento_Master = reader_Aereo.GetValue(1).ToString();
                    Bean_Costos.Costo_Documento_House = reader_Aereo.GetValue(14).ToString();
                    Bean_Costos.Costo_Routing = reader_Aereo.GetValue(15).ToString();
                    Arr_Costos.Add(Bean_Costos);
                }
                DB.CloseMySQLObj(reader_Aereo, com_Aereo, con_Aereo);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                return null;
            }
            #endregion
        }
        else if (SisID == 3)
        {
            #region Cargar Costos Sistema Terrestre
            MySqlConnection con_Terrestre;
            MySqlCommand com_Terrestre;
            MySqlDataReader reader_Terrestre;
            try
            {
                con_Terrestre = DB.OpenTerrestreConnection();
                com_Terrestre = new MySqlCommand();
                com_Terrestre.Connection = con_Terrestre;
                com_Terrestre.CommandType = CommandType.Text;
                if ((Tipo == 5) || (Tipo == 6) || (Tipo == 7))
                {
                    #region Express-Consolidado-Local
                    if (Tipo_Contabilizacion == "Master")
                    {
                        com_Terrestre.CommandText = "select B.BLID, B.BLNumber, C.CostID, C.ItemID, C.Currency,C.Cost, C.Distribution, C.SupplierType, " +
                        "C.SupplierID, C.SupplierName, C.PurchaseOrder, C.Reference, 'False', C.ServiceID, " +
                        "(select HBLNumber from BLDetail where BLID=B.BLID limit 1) as HBL, " +
                        "(select BLs from BLDetail where BLID=B.BLID limit 1) as BLs, " +
                        "(select Container from BLDetail where BLID=B.BLID limit 1) as CONTENEDOR, " +
                        "(select EXType from BLDetail where BLID=B.BLID limit 1) as EXType, C.ThirdParties, (C.IsAffected + 1) " +
                        "from BLs B, Costs C where B.BLID=C.BLID " +
                        "and B.BLID=" + blID + " and C.Expired=0 and C.ProvisionID=0 and C.PurchaseOrder='' and C.BillingDate>='" + Fecha_Arranque + " 00:00:00' and C.Countries='" + Pais_Bean.ISO + "' ";
                    }
                    else if (Tipo_Contabilizacion == "House")
                    {
                        com_Terrestre.CommandText = "select  a.BLID, a.BLNumber, b.CostID, b.ItemID, b.Currency, b.Cost, b.Distribution, b.SupplierType, " +
                        "b.SupplierID, b.SupplierName, b.PurchaseOrder, b.Reference, 'False', b.ServiceID, d.HBLNumber, " +
                        "(select BLs from BLDetail where BLID=a.BLID limit 1) as Routing, " +
                        "(select Container from BLDetail where BLID=a.BLID limit 1) as CONTENEDOR, " +
                        "(select EXType from BLDetail where BLID=a.BLID limit 1) as EXType, b.ThirdParties, (b.IsAffected + 1) " +
                        "from BLs a, Costs b, CostsDetail c, BLDetail d " +
                        "where a.BLID=b.BLID and b.Expired=0 and b.CostID=c.CostID " +
                        "and c.SBLID=d.BLDetailID " +
                        "and a.BLID=" + blID + " and b.PurchaseOrder='' and b.BillingDate>='" + Fecha_Arranque + " 00:00:00' and b.Countries='" + Pais_Bean.ISO + "'";
                    }
                    #endregion
                }
                reader_Terrestre = com_Terrestre.ExecuteReader();
                while (reader_Terrestre.Read())
                {
                    Bean_Costos = new Bean_Costos();
                    Bean_Costos.Costo_Tipo_Proveedor_ID = int.Parse(reader_Terrestre.GetValue(7).ToString());
                    Bean_Costos.Costo_Proveedor_ID = int.Parse(reader_Terrestre.GetValue(8).ToString());
                    Bean_Costos.Costo_Rub_ID = int.Parse(reader_Terrestre.GetValue(3).ToString());
                    Bean_Costos.Costo_Moneda_ID = 0;
                    Bean_Costos.Costo_Moneda = reader_Terrestre.GetValue(4).ToString();
                    Bean_Costos.Costo_Monto = double.Parse(reader_Terrestre.GetValue(5).ToString());
                    Bean_Costos.Costo_Orden_Compra = reader_Terrestre.GetValue(10).ToString();
                    Bean_Costos.Costo_Referencia = reader_Terrestre.GetValue(11).ToString();
                    Bean_Costos.Costo_Provisionado = false;
                    Bean_Costos.Costo_ID = int.Parse(reader_Terrestre.GetValue(2).ToString());
                    Bean_Costos.Costo_Nombre_Proveedor = reader_Terrestre.GetValue(9).ToString();
                    Bean_Costos.Costo_Servicio_ID = int.Parse(reader_Terrestre.GetValue(13).ToString());
                    Bean_Costos.Costo_Pago_Terceros = int.Parse(reader_Terrestre.GetValue(18).ToString());
                    Bean_Costos.Costo_Es_Afecto = int.Parse(reader_Terrestre.GetValue(19).ToString());
                    Bean_Costos.Costo_Documento_Master = reader_Terrestre.GetValue(1).ToString();
                    Bean_Costos.Costo_Documento_House = reader_Terrestre.GetValue(14).ToString();
                    Bean_Costos.Costo_Routing = reader_Terrestre.GetValue(15).ToString();
                    Arr_Costos.Add(Bean_Costos);
                }
                DB.CloseMySQLObj(reader_Terrestre, com_Terrestre, con_Terrestre);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                return null;
            }
            #endregion
        }
        return Arr_Costos;
        #endregion
    }
    protected static ArrayList Generar_Detalle_Rubros(PaisBean Pais_Bean, Bean_Contabilizacion_Automatica_Detalle Configuracion, ArrayList Arr_Costos, int Impuesto_Proveedor)
    {
        #region Generar Detalle Subtotal, Impuesto y Total por cada Rubro
        ArrayList Arr = new ArrayList();
        ArrayList Arr_Detalle_Rubros = new ArrayList();
        Bean_Totales Totales = new Bean_Totales();
        foreach (Bean_Costos Bean_Costo in Arr_Costos)
        {
            Bean_Detalle_Rubros Detalle_Rubros = null;
            Bean_Detalle_Nota_Credito Detalle_Rubros_NC = null;
            Rubros Rubros_Bean = null;
            Rubros Rubro = null;
            Rubro = new Rubros();
            Rubro.rubroID = Bean_Costo.Costo_Rub_ID;
            Rubros_Bean = (Rubros)DB.ExistRubroPais(Rubro, Pais_Bean.ID);
            if (Rubros_Bean == null)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog("Error, El BL no tiene todos los rubros registrados en contabilidad para este pais.");
                return null;
            }
            if (Configuracion.tcad_ttr_id != 3)
            {
                #region Generar Detalle Facturacion, Notas de Debito y Provisiones
                Detalle_Rubros = new Bean_Detalle_Rubros();
                if ((Impuesto_Proveedor != 1))//si debe cobrar iva y el rubro no esta en dolares y no es excento
                {
                    if (Rubros_Bean.CobIva == 1)
                    {
                        if (Rubros_Bean.IvaInc == 1)
                        {
                            Detalle_Rubros.tdf_monto = Bean_Costo.Costo_Monto;
                            Detalle_Rubros.tdf_montosinimpuesto = Math.Round(Bean_Costo.Costo_Monto * (double)(1 / (1 + Pais_Bean.Impuesto)), 2);
                            Detalle_Rubros.tdf_impuesto = Math.Round(Bean_Costo.Costo_Monto - Detalle_Rubros.tdf_montosinimpuesto, 2);
                        }
                        else
                        {
                            Detalle_Rubros.tdf_impuesto = Math.Round(Bean_Costo.Costo_Monto * (double)Pais_Bean.Impuesto, 2);
                            Detalle_Rubros.tdf_montosinimpuesto = Math.Round(Bean_Costo.Costo_Monto, 2);
                            Detalle_Rubros.tdf_monto = Math.Round(Detalle_Rubros.tdf_montosinimpuesto + Detalle_Rubros.tdf_impuesto, 2);
                        }
                        Totales.Afecto += Detalle_Rubros.tdf_montosinimpuesto;
                    }
                    else if (Rubros_Bean.CobIva == 0)
                    {
                        Detalle_Rubros.tdf_monto = Bean_Costo.Costo_Monto;
                        Detalle_Rubros.tdf_montosinimpuesto = Bean_Costo.Costo_Monto;
                        Detalle_Rubros.tdf_impuesto = 0;
                        Totales.No_Afecto += Detalle_Rubros.tdf_montosinimpuesto;
                    }
                }
                else
                {
                    Detalle_Rubros.tdf_monto = Bean_Costo.Costo_Monto;
                    Detalle_Rubros.tdf_montosinimpuesto = Bean_Costo.Costo_Monto;
                    Detalle_Rubros.tdf_impuesto = 0;
                    Totales.No_Afecto += Detalle_Rubros.tdf_montosinimpuesto;
                }
                if (Bean_Costo.Costo_Moneda_ID == 8)
                {
                    Detalle_Rubros.tdf_total_equivalente = Math.Round((double)Detalle_Rubros.tdf_monto * (double)Pais_Bean.TipoCambio, 2);
                    Detalle_Rubros.tdf_montosinimpuesto_equivalente = Math.Round((double)Detalle_Rubros.tdf_montosinimpuesto * (double)Pais_Bean.TipoCambio, 2);
                    Detalle_Rubros.tdf_impuesto_equivalente = Math.Round((double)Detalle_Rubros.tdf_impuesto * (double)Pais_Bean.TipoCambio, 2);
                }
                else
                {
                    Detalle_Rubros.tdf_total_equivalente = Math.Round((double)Detalle_Rubros.tdf_monto / (double)Pais_Bean.TipoCambio, 2);
                    Detalle_Rubros.tdf_montosinimpuesto_equivalente = Math.Round((double)Detalle_Rubros.tdf_montosinimpuesto / (double)Pais_Bean.TipoCambio, 2);
                    Detalle_Rubros.tdf_impuesto_equivalente = Math.Round((double)Detalle_Rubros.tdf_impuesto / (double)Pais_Bean.TipoCambio, 2);
                }
                Detalle_Rubros.tdf_rub_id = Bean_Costo.Costo_Rub_ID;
                Detalle_Rubros.tdf_tfa_id = 0;
                Detalle_Rubros.tdf_ttr_id = Configuracion.tcad_ttr_id;
                Detalle_Rubros.tdf_tts_id = Bean_Costo.Costo_Servicio_ID;
                Detalle_Rubros.tdf_ttm_id = Configuracion.tcad_moneda_id_destino;
                Detalle_Rubros.tdf_cargo_id = Bean_Costo.Costo_ID;
                Arr_Detalle_Rubros.Add(Detalle_Rubros);
                Totales.Total += Detalle_Rubros.tdf_monto;
                Totales.SubTotal += Detalle_Rubros.tdf_montosinimpuesto;
                Totales.Impuesto += Detalle_Rubros.tdf_impuesto;
                Totales.Total_Equivalente += Detalle_Rubros.tdf_total_equivalente;
                Totales.SubTotal_Equivalente += Detalle_Rubros.tdf_montosinimpuesto_equivalente;
                Totales.Impuesto_Equivalente += Detalle_Rubros.tdf_impuesto_equivalente;

                #endregion
            }
            else if (Configuracion.tcad_ttr_id == 3)
            {
                #region Generar Detalle Nota Credito
                Detalle_Rubros_NC = new Bean_Detalle_Nota_Credito();
                if ((Impuesto_Proveedor != 1))//si debe cobrar iva y el rubro no esta en dolares y no es excento
                {
                    if (Rubros_Bean.CobIva == 1)
                    {
                        if (Rubros_Bean.IvaInc == 1)
                        {
                            Detalle_Rubros_NC.dnc_monto = Bean_Costo.Costo_Monto;
                            Detalle_Rubros_NC.dnc_montosinimpuesto = Math.Round(Bean_Costo.Costo_Monto * (double)(1 / (1 + Pais_Bean.Impuesto)), 2);
                            Detalle_Rubros_NC.dnc_impuesto = Math.Round(Bean_Costo.Costo_Monto - Detalle_Rubros_NC.dnc_montosinimpuesto, 2);
                        }
                        else
                        {
                            Detalle_Rubros_NC.dnc_impuesto = Math.Round(Bean_Costo.Costo_Monto * (double)Pais_Bean.Impuesto, 2);
                            Detalle_Rubros_NC.dnc_montosinimpuesto = Math.Round(Bean_Costo.Costo_Monto, 2);
                            Detalle_Rubros_NC.dnc_monto = Math.Round(Detalle_Rubros_NC.dnc_montosinimpuesto + Detalle_Rubros_NC.dnc_impuesto, 2);
                        }
                        Totales.Afecto += Detalle_Rubros_NC.dnc_montosinimpuesto;
                    }
                    else if (Rubros_Bean.CobIva == 0)
                    {
                        Detalle_Rubros_NC.dnc_monto = Bean_Costo.Costo_Monto;
                        Detalle_Rubros_NC.dnc_montosinimpuesto = Bean_Costo.Costo_Monto;
                        Detalle_Rubros_NC.dnc_impuesto = 0;
                        Totales.No_Afecto += Detalle_Rubros_NC.dnc_montosinimpuesto;
                    }
                }
                else
                {
                    Detalle_Rubros_NC.dnc_monto = Bean_Costo.Costo_Monto;
                    Detalle_Rubros_NC.dnc_montosinimpuesto = Bean_Costo.Costo_Monto;
                    Detalle_Rubros_NC.dnc_impuesto = 0;
                    Totales.No_Afecto += Detalle_Rubros_NC.dnc_montosinimpuesto;
                }
                if (Bean_Costo.Costo_Moneda_ID == 8)
                {
                    Detalle_Rubros_NC.dnc_monto_equivalente = Math.Round((double)Detalle_Rubros_NC.dnc_monto * (double)Pais_Bean.TipoCambio, 2);
                    Detalle_Rubros_NC.dnc_montosinimpuesto_equivalente = Math.Round((double)Detalle_Rubros_NC.dnc_montosinimpuesto * (double)Pais_Bean.TipoCambio, 2);
                    Detalle_Rubros_NC.dnc_impuesto_equivalente = Math.Round((double)Detalle_Rubros_NC.dnc_impuesto_equivalente * (double)Pais_Bean.TipoCambio, 2);
                }
                else
                {
                    Detalle_Rubros_NC.dnc_monto_equivalente = Math.Round((double)Detalle_Rubros_NC.dnc_monto / (double)Pais_Bean.TipoCambio, 2);
                    Detalle_Rubros_NC.dnc_montosinimpuesto_equivalente = Math.Round((double)Detalle_Rubros_NC.dnc_montosinimpuesto / (double)Pais_Bean.TipoCambio, 2);
                    Detalle_Rubros_NC.dnc_impuesto_equivalente = Math.Round((double)Detalle_Rubros_NC.dnc_impuesto_equivalente / (double)Pais_Bean.TipoCambio, 2);
                }

                Detalle_Rubros_NC.dnc_rub_id = Bean_Costo.Costo_Rub_ID;
                Detalle_Rubros_NC.dnc_moneda_id = Bean_Costo.Costo_Moneda_ID;
                Detalle_Rubros_NC.dnc_ted_id = 1;
                Detalle_Rubros_NC.dnc_tre_id = 0;
                Detalle_Rubros_NC.dnc_str_id = Configuracion.tcad_ttr_id;
                Arr_Detalle_Rubros.Add(Detalle_Rubros_NC);
                Totales.Total += Detalle_Rubros_NC.dnc_monto;
                Totales.SubTotal += Detalle_Rubros_NC.dnc_montosinimpuesto;
                Totales.Impuesto += Detalle_Rubros_NC.dnc_impuesto;
                Totales.Total_Equivalente += Detalle_Rubros_NC.dnc_monto_equivalente;
                Totales.SubTotal_Equivalente += Detalle_Rubros_NC.dnc_montosinimpuesto_equivalente;
                Totales.Impuesto_Equivalente += Detalle_Rubros_NC.dnc_impuesto_equivalente;
                #endregion
            }
        }
        Arr.Add(Totales);
        Arr.Add(Arr_Detalle_Rubros);
        return Arr;
        #endregion
    }
    public static int Get_ContabilizacionID_X_Parametros(string _MBL_Tipo, string _HBL_Tipo, string _ES_Ruteado, string _Imp_Exp, int _paisID, int _ttoID, PaisBean _Pais_Bean)
    {
        #region Get Contabilizacion ID
        int resultado = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = System.Data.CommandType.Text;
            comm.CommandText = "select tca_id from tbl_contabilizacion_automatica where tca_tipo_mbl=" + _MBL_Tipo + " and tca_tipo_hbl=" + _HBL_Tipo + " and tca_ruteado=" + _ES_Ruteado + " and tca_import_export=" + _Imp_Exp + " and tca_pai_id=" + _paisID + " and tca_tto_id=" + _ttoID + " and tca_estado=1";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                resultado = int.Parse(reader.GetValue(0).ToString());
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return resultado;
        #endregion
    }
    public static ArrayList Get_Configuracion_Transacciones_Contabilizacion_Automatica(int ID)
    {
        #region Obtener la Configuracion de cada Transaccion asociada a una Contabilizacion Automatica
        ArrayList Arr_Transacciones = new ArrayList();
        Bean_Contabilizacion_Automatica_Detalle Bean_Configuracion_Transaccion = null;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = System.Data.CommandType.Text;
            comm.CommandText = "select tcad_id, tcad_ttr_id, tcad_tpi_id, tcad_contabilidad_id, tcad_moneda_id_origen, tcad_moneda_id_destino, tcad_tiene_hijos, tcad_id_padre, tcad_estado, tcad_tca_id, tcad_automatizar, tcad_descripcion, tcad_tct_id, tcad_terceros, tcad_suc_id, tcad_operacion_id, tcad_serie, tcad_genera_partida from tbl_contabilizacion_automatica_detalle where tcad_tca_id=" + ID + " and tcad_estado=1 ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean_Configuracion_Transaccion = new Bean_Contabilizacion_Automatica_Detalle();
                Bean_Configuracion_Transaccion.tcad_id = int.Parse(reader.GetValue(0).ToString());
                Bean_Configuracion_Transaccion.tcad_ttr_id = int.Parse(reader.GetValue(1).ToString());
                Bean_Configuracion_Transaccion.tcad_tpi_id = int.Parse(reader.GetValue(2).ToString());
                Bean_Configuracion_Transaccion.tcad_contabilidad_id = int.Parse(reader.GetValue(3).ToString());
                Bean_Configuracion_Transaccion.tcad_moneda_id_origen = int.Parse(reader.GetValue(4).ToString());
                Bean_Configuracion_Transaccion.tcad_moneda_id_destino = int.Parse(reader.GetValue(5).ToString());
                Bean_Configuracion_Transaccion.tcad_tiene_hijos = bool.Parse(reader.GetValue(6).ToString());
                Bean_Configuracion_Transaccion.tcad_id_padre = int.Parse(reader.GetValue(7).ToString());
                Bean_Configuracion_Transaccion.tcad_estado = int.Parse(reader.GetValue(8).ToString());
                Bean_Configuracion_Transaccion.tcad_tca_id = int.Parse(reader.GetValue(9).ToString());
                Bean_Configuracion_Transaccion.tcad_automatizar = bool.Parse(reader.GetValue(10).ToString());
                Bean_Configuracion_Transaccion.tcad_descripcion = reader.GetValue(11).ToString();
                Bean_Configuracion_Transaccion.tcad_tct_id = int.Parse(reader.GetValue(12).ToString());
                Bean_Configuracion_Transaccion.tcad_terceros = bool.Parse(reader.GetValue(13).ToString());
                Bean_Configuracion_Transaccion.tcad_suc_id = int.Parse(reader.GetValue(14).ToString());
                Bean_Configuracion_Transaccion.tcad_operacion_id = int.Parse(reader.GetValue(15).ToString());
                Bean_Configuracion_Transaccion.tcad_serie = reader.GetValue(16).ToString();
                Bean_Configuracion_Transaccion.tcad_genera_partida = bool.Parse(reader.GetValue(17).ToString());
                Arr_Transacciones.Add(Bean_Configuracion_Transaccion);
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Arr_Transacciones;
        #endregion
    }
    public static Bean_Provision_Automatica Construir_Provision(NpgsqlConnection conn, NpgsqlCommand comm, NpgsqlTransaction Transaction, Bean_Contabilizacion_Automatica_Detalle Bean_Configuracion_Transaccion, PaisBean Pais_Bean, ArrayList Arr_Costos,int sisID, int ttoID, int blID, string usuID)
    {
        #region Construir Provision Automatica
        Bean_Provision_Automatica Provision_Automatica = null;
        try
        {
            int Correlativo = 0;
            Correlativo = DB.GetCorr(Bean_Configuracion_Transaccion.tcad_suc_id, 5, Bean_Configuracion_Transaccion.tcad_serie);
            Provision_Automatica = new Bean_Provision_Automatica();
            Bean_Datos_BL Datos_BL = Get_DatosBL_X_Traficos(sisID, ttoID, blID, Pais_Bean);
            Bean_Costos Bean_Costo = (Bean_Costos)Arr_Costos[0];
            int ImpuestoProveedor = Definir_Cobro_Impuesto(Pais_Bean.ID.ToString(), Bean_Configuracion_Transaccion.tcad_contabilidad_id, Bean_Costo.Costo_Es_Afecto);
            ArrayList Arr_Detalle=  Generar_Detalle_Rubros(Pais_Bean, Bean_Configuracion_Transaccion, Arr_Costos, ImpuestoProveedor);
            ArrayList Arr_Detalle_Provision = (ArrayList)Arr_Detalle[1];
            Bean_Totales Totales = (Bean_Totales)Arr_Detalle[0];
            Provision_Automatica.tpr_prov_id = Obtener_Llave_Primaria(conn, comm, Transaction, 5);
            Provision_Automatica.tpr_fact_id = "";
            Provision_Automatica.tpr_fact_fecha = "";
            Provision_Automatica.tpr_fecha_maxpago = "";
            Provision_Automatica.tpr_valor = Totales.Total;
            Provision_Automatica.tpr_afecto = Totales.Afecto;
            Provision_Automatica.tpr_noafecto = Totales.No_Afecto;
            Provision_Automatica.tpr_iva = Totales.Impuesto;
            Provision_Automatica.tpr_observacion = "";
            Provision_Automatica.tpr_suc_id = Bean_Configuracion_Transaccion.tcad_suc_id;
            Provision_Automatica.tpr_pai_id = Pais_Bean.ID;
            Provision_Automatica.tpr_usu_creacion = usuID;
            Provision_Automatica.tpr_fecha_creacion = "";
            Provision_Automatica.tpr_usu_acepta = "";
            Provision_Automatica.tpr_fecha_acepta = "";
            Provision_Automatica.tpr_departamento = 0;
            Provision_Automatica.tpr_ted_id = 5;
            Provision_Automatica.tpr_serie = Bean_Configuracion_Transaccion.tcad_serie;
            Provision_Automatica.tpr_serie_oc = "";
            Provision_Automatica.tpr_correlativo_oc = 0;
            Provision_Automatica.tpr_tts_id = 0;
            Provision_Automatica.tpr_hbl = Datos_BL.Hbl;
            Provision_Automatica.tpr_mbl = Datos_BL.Mbl;
            Provision_Automatica.tpr_routing = Datos_BL.Routing;
            Provision_Automatica.tpr_contenedor = Datos_BL.Contenedor;
            Provision_Automatica.tpr_tpi_id = Bean_Costo.Costo_Tipo_Proveedor_ID;
            Provision_Automatica.tpr_correlativo = Correlativo;
            Provision_Automatica.tpr_mon_id = Bean_Configuracion_Transaccion.tcad_moneda_id_destino;
            Provision_Automatica.tpr_serie_contrasena = "";
            Provision_Automatica.tpr_contrasena_correlativo = 0;
            Provision_Automatica.tpr_valor_equivalente = Totales.Total_Equivalente;
            Provision_Automatica.tpr_fact_corr = Bean_Costo.Costo_Referencia;
            Provision_Automatica.tpr_proveedor_cajachica = "";
            Provision_Automatica.tpr_imp_exp_id = Datos_BL.Import_Export;
            Provision_Automatica.tpr_bien_serv = 2;
            Provision_Automatica.tpr_tcon_id = Bean_Configuracion_Transaccion.tcad_contabilidad_id;
            Provision_Automatica.tpr_fecha_emision = "";
            Provision_Automatica.tpr_nombre = Bean_Costo.Costo_Nombre_Proveedor;
            Provision_Automatica.tpr_proveedor_cajachica_id = 0;
            Provision_Automatica.tpr_poliza = "";
            Provision_Automatica.tpr_fiscal = true;
            Provision_Automatica.tpr_fecha_libro_compras = "";
            Provision_Automatica.tpr_tto_id = Datos_BL.BLID;
            Provision_Automatica.tpr_ruta_pais = "";
            Provision_Automatica.tpr_ruta = "";
            Provision_Automatica.tpr_blid = Datos_BL.BLID;
            Provision_Automatica.tpr_tti_id = ImpuestoProveedor;
            Provision_Automatica.tpr_usu_modifica_regimen = "";
            Provision_Automatica.tpr_usu_anula = "";
            Provision_Automatica.tpr_fecha_anula = "";
            Provision_Automatica.tpr_toc_id = 0;
            Provision_Automatica.tpr_observacion_contrasena = "-";
            Provision_Automatica.tpr_fecha_recibo_factura = "";
            Provision_Automatica.Arr_Detalle_Provision = Arr_Detalle_Provision;
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Provision_Automatica;
        #endregion
    }
    public static ArrayList Construir_Provision_A_Partir_Otro_Documento(ArrayList Arr_Documento, int ttrID, UsuarioBean user)
    {

        ArrayList Arr_Result = new ArrayList();
        #region Construir Provision Automatica A Partir de Otro Documento
        Bean_Provision_Automatica Provision_Automatica = null;
        try
        {
            if (ttrID == 1)
            {
                #region Construir Provision Automatica a Partir de una Factura
                FacturaBean Factura = (FacturaBean)Arr_Documento[0];
                #region Configuracion Intercompany
                int intercompanyID = 0;
                int idpaisORIGEN = 0;
                int idcontaORIGEN = 0;
                int idmonedaORIGEN = 0;
                int idOPERACION = 0;
                int idpaisDESTINO = 0;
                int idcontaDESTINO = 0;
                int idmonedaDESTINO = 0;
                RE_GenericBean Intercompany_Origen = null;
                intercompanyID = int.Parse(Factura.CliID.ToString());
                idpaisORIGEN = user.PaisID;
                idcontaORIGEN = user.contaID;
                idmonedaORIGEN = Factura.MonedaID;
                idOPERACION = 2;
                RE_GenericBean Bean_Configuracion_Intercompany = Contabilizacion_Automatica_CN.Obtener_Configuracion_Intercompany_Administrativo(intercompanyID, idpaisORIGEN, idcontaORIGEN, idmonedaORIGEN, idOPERACION);
                idpaisDESTINO = Bean_Configuracion_Intercompany.intC1;
                idcontaDESTINO = Bean_Configuracion_Intercompany.intC2;
                idmonedaDESTINO = Bean_Configuracion_Intercompany.intC3;
                Intercompany_Origen = (RE_GenericBean)DB.Get_Intercompany_Data_By_Empresa(idpaisORIGEN);
                #endregion
                if (Factura != null)
                {
                    RE_GenericBean Intercompany_Bean = (RE_GenericBean)DB.Get_Intercompany_Data(Convert.ToInt32(Factura.CliID));
                    if (Intercompany_Bean == null)
                    {
                        Arr_Result = new ArrayList();
                        Arr_Result.Add("0");
                        Arr_Result.Add("Existio un error al Tratar de Obtener la Informacion del Intercompany");
                        return Arr_Result;
                    }
                    int transID = 105;//Provision Intercompanys
                    int tipo_cobro = 1;//prepaid, collect
                    Provision_Automatica = new Bean_Provision_Automatica();
                    Provision_Automatica.tpr_prov_id = 0;
                    Provision_Automatica.tpr_proveedor_id = Intercompany_Origen.intC1;
                    Provision_Automatica.tpr_fact_id = Factura.Serie;
                    Provision_Automatica.tpr_fact_fecha = DateTime.Now.ToString("yyyy-MM-dd");
                    Provision_Automatica.tpr_fecha_maxpago = DateTime.Now.ToString("yyyy-MM-dd");
                    Provision_Automatica.tpr_valor = 0;
                    Provision_Automatica.tpr_afecto = 0;
                    Provision_Automatica.tpr_noafecto = 0;
                    Provision_Automatica.tpr_iva = 0;
                    Provision_Automatica.tpr_observacion = Factura.Observaciones;
                    Provision_Automatica.tpr_suc_id = Bean_Configuracion_Intercompany.intC5;
                    Provision_Automatica.tpr_pai_id = idpaisDESTINO;
                    Provision_Automatica.tpr_usu_creacion = user.ID;
                    Provision_Automatica.tpr_fecha_creacion = DateTime.Now.ToString("yyyy-MM-dd");
                    Provision_Automatica.tpr_usu_acepta = user.ID;
                    Provision_Automatica.tpr_fecha_acepta = DateTime.Now.ToString("yyyy-MM-dd");
                    Provision_Automatica.tpr_departamento = 0;
                    Provision_Automatica.tpr_mon_id = idmonedaDESTINO;
                    Provision_Automatica.tpr_ted_id = 5;
                    RE_GenericBean Datos_Bean = (RE_GenericBean)DB.Get_Serie_CorrelativoBy_Traficos(Provision_Automatica.tpr_suc_id, idcontaDESTINO, Provision_Automatica.tpr_mon_id);
                    if (Datos_Bean == null)
                    {
                        PaisBean Pais_Temp = null;
                        Pais_Temp = (PaisBean)DB.getPais(idpaisDESTINO);
                        int Resultado = DB.Crear_Series_Provisiones_Automaticas(Provision_Automatica.tpr_suc_id, Pais_Temp, 8, idcontaDESTINO, Provision_Automatica.tpr_mon_id);
                        Datos_Bean = (RE_GenericBean)DB.Get_Serie_CorrelativoBy_Traficos(Provision_Automatica.tpr_suc_id, user.contaID, Provision_Automatica.tpr_mon_id);
                    }
                    Provision_Automatica.tpr_serie = Datos_Bean.strC1;
                    Provision_Automatica.tpr_serie_oc = "";
                    Provision_Automatica.tpr_correlativo_oc = 0;
                    Provision_Automatica.tpr_tts_id = 0;
                    Provision_Automatica.tpr_hbl = Factura.HBL;
                    Provision_Automatica.tpr_mbl = Factura.MBL;
                    Provision_Automatica.tpr_routing = Factura.Routing;
                    Provision_Automatica.tpr_contenedor = Factura.Contenedor;
                    Provision_Automatica.tpr_tpi_id = Factura.Tipo_Persona;
                    Provision_Automatica.tpr_correlativo = 0;
                    //Provision_Automatica.tpr_mon_id = idmonedaDESTINO;
                    Provision_Automatica.tpr_serie_contrasena = "";
                    Provision_Automatica.tpr_contrasena_correlativo = 0;
                    Provision_Automatica.tpr_valor_equivalente = 0;
                    Provision_Automatica.tpr_fact_corr = Factura.Correlativo;
                    Provision_Automatica.tpr_proveedor_cajachica = "";
                    Provision_Automatica.tpr_imp_exp_id = Factura.imp_exp;
                    Provision_Automatica.tpr_bien_serv = 2;
                    Provision_Automatica.tpr_tcon_id = idcontaDESTINO;
                    Provision_Automatica.tpr_nombre = Intercompany_Origen.strC1;
                    Provision_Automatica.tpr_proveedor_cajachica_id = 0;
                    Provision_Automatica.tpr_poliza = Factura.OrdenPO;

                    if ((Provision_Automatica.tpr_pai_id == 2) || (Provision_Automatica.tpr_pai_id == 9) || (Provision_Automatica.tpr_pai_id == 26))
                    {
                        Provision_Automatica.tpr_fiscal = false;
                    }
                    else
                    {
                        Provision_Automatica.tpr_fiscal = true;
                    }

                    Provision_Automatica.tpr_fecha_libro_compras = DateTime.Now.ToString("yyyy-MM-dd");
                    Provision_Automatica.tpr_tto_id = Factura.Tipo_Operacion;
                    Provision_Automatica.tpr_ruta_pais = Factura.Ruta_Pais;
                    Provision_Automatica.tpr_ruta = Factura.Ruta;
                    Provision_Automatica.tpr_blid = Factura.BlId;
                    Provision_Automatica.tpr_tti_id = Factura.Contribuyente;
                    Provision_Automatica.tpr_usu_modifica_regimen = "";
                    Provision_Automatica.tpr_usu_anula = "";
                    Provision_Automatica.tpr_fecha_anula = null;
                    Provision_Automatica.tpr_toc_id = 0;
                    Provision_Automatica.tpr_observacion_contrasena = "";
                    Provision_Automatica.tpr_fecha_recibo_factura = null;
                    Provision_Automatica.tpr_mbl_modificado = false;
                    Provision_Automatica.tpr_ttd_id = 1;
                    Provision_Automatica.tpr_tds_id = 0;
                    #region Generar Detalle de Rubros
                    Bean_Detalle_Rubros Detalle_Rubros = null;
                    ArrayList Arr_Rubros = null;
                    if (Factura.RubrosArr != null)
                    {
                        Arr_Rubros = new ArrayList();
                        foreach (Rubros rub in Factura.RubrosArr)
                        {
                            Detalle_Rubros = new Bean_Detalle_Rubros();
                            Detalle_Rubros.tdf_rub_id = Convert.ToInt32(rub.rubroID);
                            Detalle_Rubros.tdf_montosinimpuesto = rub.rubroTot;
                            Detalle_Rubros.tdf_montosinimpuesto = Contabilizacion_Automatica_CN.Convertir_Divisas_Intercompanys(idpaisORIGEN, idmonedaORIGEN, Detalle_Rubros.tdf_montosinimpuesto, idpaisDESTINO, idmonedaDESTINO);
                            Detalle_Rubros.tdf_impuesto = 0;
                            Detalle_Rubros.tdf_monto = rub.rubroTot;
                            Detalle_Rubros.tdf_monto = Contabilizacion_Automatica_CN.Convertir_Divisas_Intercompanys(idpaisORIGEN, idmonedaORIGEN, Detalle_Rubros.tdf_monto, idpaisDESTINO, idmonedaDESTINO);
                            Detalle_Rubros.tdf_tfa_id = 0;
                            Detalle_Rubros.tdf_ttr_id = 5;
                            //Detalle_Rubros.tdf_tts_id = rub.rubroTypeID;
                            Detalle_Rubros.tdf_tts_id = 14;//TECEROS Modificado el 24-09-2015 por solicitud de Mr Holbik
                            Detalle_Rubros.tdf_total_equivalente = rub.rubroTotD;
                            #region Calcular Total Equivalente
                            if (Provision_Automatica.tpr_mon_id == 8)
                            {
                                decimal Tipo_Cambio_Temporal = DB.getTipoCambioHoy(Provision_Automatica.tpr_pai_id);
                                Detalle_Rubros.tdf_total_equivalente = Math.Round(Detalle_Rubros.tdf_monto * (double)Tipo_Cambio_Temporal, 2);
                            }
                            else
                            {
                                decimal Tipo_Cambio_Temporal = DB.getTipoCambioHoy(Provision_Automatica.tpr_pai_id);
                                Detalle_Rubros.tdf_total_equivalente = Math.Round(Detalle_Rubros.tdf_monto / (double)Tipo_Cambio_Temporal, 2);
                            }
                            #endregion
                            Detalle_Rubros.tdf_ttm_id = Provision_Automatica.tpr_mon_id;
                            Detalle_Rubros.tdf_comentarios = "";
                            Detalle_Rubros.tdf_cargo_id = 0;
                            if (Detalle_Rubros.tdf_impuesto == 0)
                            {
                                Provision_Automatica.tpr_noafecto += Detalle_Rubros.tdf_montosinimpuesto;
                            }
                            else
                            {
                                Provision_Automatica.tpr_afecto += Detalle_Rubros.tdf_montosinimpuesto;
                            }
                            int tttID = 15;
                            Detalle_Rubros.cta_debe = (ArrayList)DB.getCtaContablebyRubro("debe", (int)Detalle_Rubros.tdf_rub_id, idpaisDESTINO, tttID, Provision_Automatica.tpr_tti_id, Provision_Automatica.tpr_mon_id, Provision_Automatica.tpr_imp_exp_id, tipo_cobro, Provision_Automatica.tpr_tcon_id, Detalle_Rubros.tdf_tts_id);
                            if (Detalle_Rubros.cta_debe == null)
                            {
                                Arr_Result = new ArrayList();
                                Arr_Result.Add("0");
                                Arr_Result.Add("No existe combinacion de cuentas contables para el Servicio " + Detalle_Rubros.tdf_tts_id.ToString() + " y Rubro " + Detalle_Rubros.tdf_rub_id);
                                return Arr_Result;
                            }
                            if (Provision_Automatica.Arr_Detalle_Provision == null) Provision_Automatica.Arr_Detalle_Provision = new ArrayList();
                            Provision_Automatica.Arr_Detalle_Provision.Add(Detalle_Rubros);
                        }
                    }
                    #endregion
                    Provision_Automatica.tpr_valor = Factura.Total;
                    Provision_Automatica.tpr_valor = Contabilizacion_Automatica_CN.Convertir_Divisas_Intercompanys(idpaisORIGEN, idmonedaORIGEN, Provision_Automatica.tpr_valor, idpaisDESTINO, idmonedaDESTINO);
                    Provision_Automatica.tpr_valor_equivalente = Factura.TotalDol;
                    #region Calcular Total Equivalente
                    if (Provision_Automatica.tpr_mon_id == 8)
                    {
                        decimal Tipo_Cambio_Temporal = DB.getTipoCambioHoy(Provision_Automatica.tpr_pai_id);
                        Provision_Automatica.tpr_valor_equivalente = Math.Round(Provision_Automatica.tpr_valor * (double)Tipo_Cambio_Temporal, 2);
                    }
                    else
                    {
                        decimal Tipo_Cambio_Temporal = DB.getTipoCambioHoy(Provision_Automatica.tpr_pai_id);
                        Provision_Automatica.tpr_valor_equivalente = Math.Round(Provision_Automatica.tpr_valor / (double)Tipo_Cambio_Temporal, 2);
                    }
                    #endregion
                    Provision_Automatica.tpr_iva = 0;
                    Provision_Automatica.tpr_ttt_id = transID;
                    int matOpID = DB.getMatrizOperacionID(transID, Provision_Automatica.tpr_mon_id, idpaisDESTINO, Provision_Automatica.tpr_tcon_id);
                    ArrayList ctas_cargo = (ArrayList)DB.getMatrizConfiguracion_ingreso_egreso(matOpID, "Abono");
                    if (ctas_cargo == null)
                    {
                        Arr_Result = new ArrayList();
                        Arr_Result.Add("0");
                        Arr_Result.Add("No existe combinacion de cuentas contables de Abono para la Transaccion");
                        return Arr_Result;
                    }
                    Provision_Automatica.ctas_cargo = ctas_cargo;
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("1");
                    Arr_Result.Add(Provision_Automatica);
                }
                #endregion
            }
            else if (ttrID == 4)
            {
                #region Construir Provision Automatica a Partir de una Nota de Debito
                FacturaBean Nota_Debito = (FacturaBean)Arr_Documento[0];
                #region Configuracion Intercompany
                int intercompanyID = 0;
                int idpaisORIGEN = 0;
                int idcontaORIGEN = 0;
                int idmonedaORIGEN = 0;
                int idOPERACION = 0;
                int idpaisDESTINO = 0;
                int idcontaDESTINO = 0;
                int idmonedaDESTINO = 0;
                RE_GenericBean Intercompany_Origen = null;
                intercompanyID = int.Parse(Nota_Debito.CliID.ToString());
                idpaisORIGEN = user.PaisID;
                idcontaORIGEN = user.contaID;
                idmonedaORIGEN = Nota_Debito.MonedaID;
                idOPERACION = 2;
                RE_GenericBean Bean_Configuracion_Intercompany = Contabilizacion_Automatica_CN.Obtener_Configuracion_Intercompany_Administrativo(intercompanyID, idpaisORIGEN, idcontaORIGEN, idmonedaORIGEN, idOPERACION);
                idpaisDESTINO = Bean_Configuracion_Intercompany.intC1;
                idcontaDESTINO = Bean_Configuracion_Intercompany.intC2;
                idmonedaDESTINO = Bean_Configuracion_Intercompany.intC3;
                Intercompany_Origen = (RE_GenericBean)DB.Get_Intercompany_Data_By_Empresa(idpaisORIGEN);
                #endregion
                if (Nota_Debito != null)
                {
                    RE_GenericBean Intercompany_Bean = (RE_GenericBean)DB.Get_Intercompany_Data(Convert.ToInt32(Nota_Debito.CliID));
                    if (Intercompany_Bean == null)
                    {
                        Arr_Result = new ArrayList();
                        Arr_Result.Add("0");
                        Arr_Result.Add("Existio un error al Tratar de Obtener la Informacion del Intercompany");
                        return Arr_Result;
                    }
                    int transID = 105;//Provision Intercompanys
                    int tipo_cobro = 1;//prepaid, collect
                    Provision_Automatica = new Bean_Provision_Automatica();
                    Provision_Automatica.tpr_prov_id = 0;
                    Provision_Automatica.tpr_proveedor_id = Intercompany_Origen.intC1;
                    Provision_Automatica.tpr_fact_id = Nota_Debito.Serie;
                    Provision_Automatica.tpr_fact_fecha = DateTime.Now.ToString("yyyy-MM-dd");
                    Provision_Automatica.tpr_fecha_maxpago = DateTime.Now.ToString("yyyy-MM-dd");
                    Provision_Automatica.tpr_valor = 0;
                    Provision_Automatica.tpr_afecto = 0;
                    Provision_Automatica.tpr_noafecto = 0;
                    Provision_Automatica.tpr_iva = 0;
                    Provision_Automatica.tpr_observacion = Nota_Debito.Observaciones;
                    Provision_Automatica.tpr_suc_id = Bean_Configuracion_Intercompany.intC5;
                    Provision_Automatica.tpr_pai_id = idpaisDESTINO;
                    Provision_Automatica.tpr_usu_creacion = user.ID;
                    Provision_Automatica.tpr_fecha_creacion = DateTime.Now.ToString("yyyy-MM-dd");
                    Provision_Automatica.tpr_usu_acepta = user.ID;
                    Provision_Automatica.tpr_fecha_acepta = DateTime.Now.ToString("yyyy-MM-dd");
                    Provision_Automatica.tpr_departamento = 0;
                    Provision_Automatica.tpr_mon_id = idmonedaDESTINO;
                    Provision_Automatica.tpr_ted_id = 5;
                    RE_GenericBean Datos_Bean = (RE_GenericBean)DB.Get_Serie_CorrelativoBy_Traficos(Provision_Automatica.tpr_suc_id, idcontaDESTINO, Provision_Automatica.tpr_mon_id);
                    if (Datos_Bean == null)
                    {
                        PaisBean Pais_Temp = null;
                        Pais_Temp = (PaisBean)DB.getPais(idpaisDESTINO);
                        int Resultado = DB.Crear_Series_Provisiones_Automaticas(Provision_Automatica.tpr_suc_id, Pais_Temp, 8, idcontaDESTINO, Provision_Automatica.tpr_mon_id);
                        Datos_Bean = (RE_GenericBean)DB.Get_Serie_CorrelativoBy_Traficos(Provision_Automatica.tpr_suc_id, user.contaID, Provision_Automatica.tpr_mon_id);
                    }
                    Provision_Automatica.tpr_serie = Datos_Bean.strC1;
                    Provision_Automatica.tpr_serie_oc = "";
                    Provision_Automatica.tpr_correlativo_oc = 0;
                    Provision_Automatica.tpr_tts_id = 0;
                    Provision_Automatica.tpr_hbl = Nota_Debito.HBL;
                    Provision_Automatica.tpr_mbl = Nota_Debito.MBL;
                    Provision_Automatica.tpr_routing = Nota_Debito.Routing;
                    Provision_Automatica.tpr_contenedor = Nota_Debito.Contenedor;
                    Provision_Automatica.tpr_tpi_id = Nota_Debito.Tipo_Persona;
                    Provision_Automatica.tpr_correlativo = 0;
                    //Provision_Automatica.tpr_mon_id = idmonedaDESTINO;
                    Provision_Automatica.tpr_serie_contrasena = "";
                    Provision_Automatica.tpr_contrasena_correlativo = 0;
                    Provision_Automatica.tpr_valor_equivalente = 0;
                    Provision_Automatica.tpr_fact_corr = Nota_Debito.Correlativo;
                    Provision_Automatica.tpr_proveedor_cajachica = "";
                    Provision_Automatica.tpr_imp_exp_id = Nota_Debito.imp_exp;
                    Provision_Automatica.tpr_bien_serv = 2;
                    Provision_Automatica.tpr_tcon_id = idcontaDESTINO;
                    Provision_Automatica.tpr_nombre = Intercompany_Origen.strC1;
                    Provision_Automatica.tpr_proveedor_cajachica_id = 0;
                    Provision_Automatica.tpr_poliza = "";

                    if ((Provision_Automatica.tpr_pai_id == 2) || (Provision_Automatica.tpr_pai_id == 9) || (Provision_Automatica.tpr_pai_id == 26))
                    {
                        Provision_Automatica.tpr_fiscal = false;
                    }
                    else
                    {
                        Provision_Automatica.tpr_fiscal = true;
                    }

                    Provision_Automatica.tpr_fecha_libro_compras = DateTime.Now.ToString("yyyy-MM-dd");
                    Provision_Automatica.tpr_tto_id = Nota_Debito.ttoID;
                    Provision_Automatica.tpr_ruta_pais = "";
                    Provision_Automatica.tpr_ruta = "";
                    Provision_Automatica.tpr_blid = Nota_Debito.BlId;
                    Provision_Automatica.tpr_tti_id = Nota_Debito.tttID;
                    Provision_Automatica.tpr_usu_modifica_regimen = "";
                    Provision_Automatica.tpr_usu_anula = "";
                    Provision_Automatica.tpr_fecha_anula = null;
                    Provision_Automatica.tpr_toc_id = 0;
                    Provision_Automatica.tpr_observacion_contrasena = "";
                    Provision_Automatica.tpr_fecha_recibo_factura = null;
                    Provision_Automatica.tpr_mbl_modificado = false;
                    Provision_Automatica.tpr_ttd_id = 1;
                    Provision_Automatica.tpr_tds_id = 0;
                    #region Generar Detalle de Rubros
                    Bean_Detalle_Rubros Detalle_Rubros = null;
                    ArrayList Arr_Rubros = null;
                    if (Nota_Debito.RubrosArr != null)
                    {
                        Arr_Rubros = new ArrayList();
                        foreach (Rubros rub in Nota_Debito.RubrosArr)
                        {
                            Detalle_Rubros = new Bean_Detalle_Rubros();
                            Detalle_Rubros.tdf_rub_id = Convert.ToInt32(rub.rubroID);
                            Detalle_Rubros.tdf_montosinimpuesto = rub.rubroTot;
                            Detalle_Rubros.tdf_montosinimpuesto = Contabilizacion_Automatica_CN.Convertir_Divisas_Intercompanys(idpaisORIGEN, idmonedaORIGEN, Detalle_Rubros.tdf_montosinimpuesto, idpaisDESTINO, idmonedaDESTINO);
                            Detalle_Rubros.tdf_impuesto = 0;
                            Detalle_Rubros.tdf_monto = rub.rubroTot;
                            Detalle_Rubros.tdf_monto = Contabilizacion_Automatica_CN.Convertir_Divisas_Intercompanys(idpaisORIGEN, idmonedaORIGEN, Detalle_Rubros.tdf_monto, idpaisDESTINO, idmonedaDESTINO);
                            Detalle_Rubros.tdf_tfa_id = 0;
                            Detalle_Rubros.tdf_ttr_id = 5;
                            Detalle_Rubros.tdf_tts_id = rub.rubroTypeID;
                            #region Calcular Total Equivalente
                            if (Provision_Automatica.tpr_mon_id == 8)
                            {
                                decimal Tipo_Cambio_Temporal = DB.getTipoCambioHoy(Provision_Automatica.tpr_pai_id);
                                Detalle_Rubros.tdf_total_equivalente = Math.Round(Detalle_Rubros.tdf_monto * (double)Tipo_Cambio_Temporal, 2);
                            }
                            else
                            {
                                decimal Tipo_Cambio_Temporal = DB.getTipoCambioHoy(Provision_Automatica.tpr_pai_id);
                                Detalle_Rubros.tdf_total_equivalente = Math.Round(Detalle_Rubros.tdf_monto / (double)Tipo_Cambio_Temporal, 2);
                            }
                            #endregion
                            Detalle_Rubros.tdf_ttm_id = Provision_Automatica.tpr_mon_id;
                            Detalle_Rubros.tdf_comentarios = "";
                            Detalle_Rubros.tdf_cargo_id = 0;
                            if (Detalle_Rubros.tdf_impuesto == 0)
                            {
                                Provision_Automatica.tpr_noafecto += Detalle_Rubros.tdf_montosinimpuesto;
                            }
                            else
                            {
                                Provision_Automatica.tpr_afecto += Detalle_Rubros.tdf_montosinimpuesto;
                            }
                            int tttID = 15;
                            Detalle_Rubros.cta_debe = (ArrayList)DB.getCtaContablebyRubro("debe", (int)Detalle_Rubros.tdf_rub_id, user.PaisID, tttID, Provision_Automatica.tpr_tti_id, Provision_Automatica.tpr_mon_id, Provision_Automatica.tpr_imp_exp_id, tipo_cobro, Provision_Automatica.tpr_tcon_id, Detalle_Rubros.tdf_tts_id);
                            if (Detalle_Rubros.cta_debe == null)
                            {
                                Arr_Result = new ArrayList();
                                Arr_Result.Add("0");
                                Arr_Result.Add("No existe combinacion de cuentas contables para el Servicio " + Detalle_Rubros.tdf_tts_id.ToString() + " y Rubro " + Detalle_Rubros.tdf_rub_id);
                                return Arr_Result;
                            }
                            if (Provision_Automatica.Arr_Detalle_Provision == null) Provision_Automatica.Arr_Detalle_Provision = new ArrayList();
                            Provision_Automatica.Arr_Detalle_Provision.Add(Detalle_Rubros);
                        }
                    }
                    #endregion
                    Provision_Automatica.tpr_valor = Nota_Debito.Total;
                    Provision_Automatica.tpr_valor = Contabilizacion_Automatica_CN.Convertir_Divisas_Intercompanys(idpaisORIGEN, idmonedaORIGEN, Provision_Automatica.tpr_valor, idpaisDESTINO, idmonedaDESTINO);
                    #region Calcular Total Equivalente
                    if (Provision_Automatica.tpr_mon_id == 8)
                    {
                        decimal Tipo_Cambio_Temporal = DB.getTipoCambioHoy(Provision_Automatica.tpr_pai_id);
                        Provision_Automatica.tpr_valor_equivalente = Math.Round(Provision_Automatica.tpr_valor * (double)Tipo_Cambio_Temporal, 2);
                    }
                    else
                    {
                        decimal Tipo_Cambio_Temporal = DB.getTipoCambioHoy(Provision_Automatica.tpr_pai_id);
                        Provision_Automatica.tpr_valor_equivalente = Math.Round(Provision_Automatica.tpr_valor / (double)Tipo_Cambio_Temporal, 2);
                    }
                    #endregion
                    Provision_Automatica.tpr_iva = 0;
                    Provision_Automatica.tpr_ttt_id = transID;
                    int matOpID = DB.getMatrizOperacionID(transID, Provision_Automatica.tpr_mon_id, user.PaisID, Provision_Automatica.tpr_tcon_id);
                    ArrayList ctas_cargo = (ArrayList)DB.getMatrizConfiguracion_ingreso_egreso(matOpID, "Abono");
                    if (ctas_cargo == null)
                    {
                        Arr_Result = new ArrayList();
                        Arr_Result.Add("0");
                        Arr_Result.Add("No existe combinacion de cuentas contables de Abono para la Transaccion");
                        return Arr_Result;
                    }
                    Provision_Automatica.ctas_cargo = ctas_cargo;
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("1");
                    Arr_Result.Add(Provision_Automatica);
                }
                #endregion
            }
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Arr_Result;
        #endregion
    }
    public static int Insertar_Configuracion_Intercompany_Administrativo(UsuarioBean user, RE_GenericBean Bean)
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
            comm.CommandText = "insert into tbl_intercompany_administrativo (tia_pais_origen, tia_contabilidad_origen, tia_moneda_origen, tia_tipo_operacion, tia_id_intercompany, tia_contabilidad_destino, tia_moneda_destino, tia_ttr_id_destino, tia_suc_id, tia_operacion_id, tia_moneda_impresion_destino, tia_usuario_creacion)";
            comm.CommandText += " values (@tia_pais_origen, @tia_contabilidad_origen, @tia_moneda_origen, @tia_tipo_operacion, @tia_id_intercompany, @tia_contabilidad_destino, @tia_moneda_destino, @tia_ttr_id_destino, @tia_suc_id, @tia_operacion_id, @tia_moneda_impresion_destino, @tia_usuario_creacion)";
            comm.Parameters.Add("@tia_pais_origen", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC1;
            comm.Parameters.Add("@tia_contabilidad_origen", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC2;
            comm.Parameters.Add("@tia_moneda_origen", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC3;
            comm.Parameters.Add("@tia_tipo_operacion", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC4;
            comm.Parameters.Add("@tia_id_intercompany", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC5;
            comm.Parameters.Add("@tia_contabilidad_destino", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC6;
            comm.Parameters.Add("@tia_moneda_destino", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC7;
            comm.Parameters.Add("@tia_ttr_id_destino", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC8;
            comm.Parameters.Add("@tia_suc_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC9;
            comm.Parameters.Add("@tia_operacion_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC10;
            comm.Parameters.Add("@tia_moneda_impresion_destino", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC11;
            comm.Parameters.Add("@tia_usuario_creacion", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.ID;
            resultado = comm.ExecuteNonQuery();
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
    public static ArrayList Obtener_Configuraciones_Intercompany_Administrativo(UsuarioBean user, string sql)
    {
        ArrayList Arr_Result = new ArrayList();
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
            comm.CommandText = "select a.tia_id, b.pai_nombre, c.tcon_nombre, substring( upper(d.ttm_nombre) from 1 for 3), a.tia_tipo_operacion, NOMBRE_INTERCOMAPNY, e.tcon_nombre, " +
            "substring (upper(f.ttm_nombre) from 1 for 3), g.ttr_nombre, h.suc_nombre, a.tia_operacion_id, substring (upper(i.ttm_nombre) from 1 for 3), ID_INTERCOMPANY " +
            "FROM tbl_intercompany_administrativo a " +
            "INNER JOIN tbl_pais b on (a.tia_pais_origen=b.pai_id) " +
            "INNER JOIN tbl_tipo_conta c on (a.tia_contabilidad_origen=c.tcon_id) " +
            "INNER JOIN tbl_tipo_moneda d on (a.tia_moneda_origen=d.ttm_id) " +
            "INNER JOIN dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select  id_intercompany, nombre_comercial from intercompanys') " +
            "Intercompany_Result (ID_INTERCOMPANY integer, NOMBRE_INTERCOMAPNY varchar) on (ID_INTERCOMPANY=a.tia_id_intercompany) " +
            "INNER JOIN tbl_tipo_conta e on (a.tia_contabilidad_destino=e.tcon_id) " +
            "INNER JOIN tbl_tipo_moneda f on (a.tia_moneda_destino=f.ttm_id) " +
            "INNER JOIN sys_tipo_referencia g on (a.tia_ttr_id_destino=g.ttr_id) " +
            "INNER JOIN tbl_sucursal h on (a.tia_suc_id=h.suc_id) " +
            "INNER JOIN tbl_tipo_moneda i on (a.tia_moneda_impresion_destino=i.ttm_id) " +
            "WHERE A.tia_estado=1 " + sql + "  order by b.pai_nombre, NOMBRE_INTERCOMAPNY";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.strC1 = reader.GetValue(0).ToString();//tia_id
                Bean.strC2 = reader.GetValue(1).ToString();//tia_pais_origen
                Bean.strC3 = reader.GetValue(2).ToString();//tia_contabilidad_origen
                Bean.strC4 = reader.GetValue(3).ToString();//tia_moneda_origen
                Bean.strC5 = reader.GetValue(4).ToString();//tia_tipo_operacion
                if (Bean.strC5 == "1")
                {
                    Bean.strC5 = "PAGO";
                }
                else if (Bean.strC5 == "2")
                {
                    Bean.strC6 = "COBRO";
                }
                Bean.strC6 = reader.GetValue(5).ToString();//NOMBRE_INTERCOMAPNY
                Bean.strC7 = reader.GetValue(6).ToString();//tia_contabilidad_destino
                Bean.strC8 = reader.GetValue(7).ToString();//tia_moneda_destino
                Bean.strC9 = reader.GetValue(8).ToString();//tia_ttr_id_destino
                Bean.strC10 = reader.GetValue(9).ToString();//tia_suc_id
                Bean.strC11 = reader.GetValue(10).ToString();//tia_operacion_id
                Bean.strC12 = reader.GetValue(11).ToString();//tia_moneda_impresion_destino
                Bean.strC13 = reader.GetValue(12).ToString();//ID_INTERCOMPANY
                Arr_Result.Add(Bean);
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Arr_Result;
    }
    public static int Eliminar_Configuracion_Intercompany_Administrativo(UsuarioBean user, int configuracionID)
    {
        int result = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "update tbl_intercompany_administrativo set tia_estado=0, tia_fecha_eliminacion=now(), tia_usuario_eliminacion='" + user.ID + "' where tia_id=" + configuracionID + "";
            result = comm.ExecuteNonQuery();
            DB.CloseObj_insert(comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return result;
    }
    public static int Validar_Existencia_Intercompany_Administrativo(UsuarioBean user, string sql)
    {
        int resultado = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select tia_id from tbl_intercompany_administrativo where tia_estado=1 " + sql + "";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                resultado = int.Parse(reader.GetValue(0).ToString());
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return resultado;
    }
    public static int Insertar_Log_Transacciones_Encadenadas(RE_GenericBean Bean)
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
            comm.CommandText = "insert into tbl_transacciones_encadenadas_log (ttel_padre_ttr_id, ttel_padre_ref_id, ttel_hijo_ttr_id, ttel_hijo_ref_id, ttel_tta_id, ttel_usuario_emision, ttel_padre_empresa_id, ttel_hijo_empresa_id, ttel_grupo_id) ";
            comm.CommandText += "values (@ttel_padre_ttr_id, @ttel_padre_ref_id, @ttel_hijo_ttr_id, @ttel_hijo_ref_id, @ttel_tta_id, @ttel_usuario_emision, @ttel_padre_empresa_id, @ttel_hijo_empresa_id, @ttel_grupo_id)";
            comm.Parameters.Add("@ttel_padre_ttr_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC1;
            comm.Parameters.Add("@ttel_padre_ref_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC2;
            comm.Parameters.Add("@ttel_hijo_ttr_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC3;
            comm.Parameters.Add("@ttel_hijo_ref_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC4;
            comm.Parameters.Add("@ttel_tta_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC5;
            comm.Parameters.Add("@ttel_usuario_emision", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC1;
            comm.Parameters.Add("@ttel_padre_empresa_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC6;
            comm.Parameters.Add("@ttel_hijo_empresa_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC7;
            comm.Parameters.Add("@ttel_grupo_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC8;
            resultado = comm.ExecuteNonQuery();
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
    public static ArrayList Get_CargosIntercompany_X_Traficos(PaisBean Pais_Bean, int SisID, int Tipo, int blID)
    {
        #region Get Cargos por Traficos
        string schema = "";
        ArrayList Arr_Cargos = new ArrayList();
        Bean_Cargos Bean_Cargos = null;
        #region Definir Schema
        if ((Tipo == 17) || (Tipo == 18))
        {
            schema = Pais_Bean.schema_apl;
        }
        else
        {
            schema = Pais_Bean.schema;
        }
        #endregion
        if (SisID == 1)
        {
            #region Cargar Cargos Sistema Maritimo
            NpgsqlConnection con_Maritimo;
            NpgsqlCommand com_Maritimo;
            NpgsqlDataReader reader_Maritimo;
            try
            {
                con_Maritimo = DB.OpenVentasConnection(schema);
                com_Maritimo = new NpgsqlCommand();
                com_Maritimo.Connection = con_Maritimo;
                com_Maritimo.CommandType = CommandType.Text;
                if ((Tipo == 1) || (Tipo == 17))
                {
                    #region FCL
                    com_Maritimo.CommandText = "select cargo_id, bl_id, id_rubro, id_servicio, id_moneda, coalesce(valor_collect,0) + coalesce(valor_sobreventa,0), factura_id, " +
                    "tipo_bl, tipo_documento, tipo_cargo, tipo_cobro, inter_company, id_grupo, id_tipo_persona " +
                    "from cargos_bl " +
                    "where bl_id=" + blID + " and tipo_bl='F' and factura_id=0 and activo=TRUE and tipo_cargo=2 and id_tipo_persona=5 ";
                    #endregion
                }
                else if ((Tipo == 2) || (Tipo == 18))
                {
                    #region LCL
                    com_Maritimo.CommandText = "select cargo_id, bl_id, id_rubro, id_servicio, id_moneda, coalesce(valor_collect,0) + coalesce(valor_sobreventa,0), factura_id, " +
                    "tipo_bl, tipo_documento, tipo_cargo, tipo_cobro, inter_company, id_grupo, id_tipo_persona " +
                    "from cargos_bl " +
                    "where bl_id=" + blID + " and tipo_bl='L' and factura_id=0 and activo=TRUE and tipo_cargo=2 and id_tipo_persona=5 ";
                    #endregion
                }
                reader_Maritimo = com_Maritimo.ExecuteReader();
                while (reader_Maritimo.Read())
                {
                    //Revisar Traduccion ID de Monedas
                    Bean_Cargos = new Bean_Cargos();
                    Bean_Cargos.Cargo_ID = int.Parse(reader_Maritimo.GetValue(0).ToString());
                    Bean_Cargos.Cargo_BLID = int.Parse(reader_Maritimo.GetValue(1).ToString());
                    Bean_Cargos.Cargo_Rub_ID = int.Parse(reader_Maritimo.GetValue(2).ToString());
                    Bean_Cargos.Cargo_Servicio_ID = int.Parse(reader_Maritimo.GetValue(3).ToString());
                    Bean_Cargos.Cargo_Moneda_Trafico_ID = int.Parse(reader_Maritimo.GetValue(4).ToString());
                    Bean_Cargos.Cargo_Moneda_ID = Contabilizacion_Automatica_CN.Traducir_Moneda_Master_To_BAW_X_ID(Bean_Cargos.Cargo_Moneda_Trafico_ID);
                    Bean_Cargos.Cargo_Monto = double.Parse(reader_Maritimo.GetValue(5).ToString());
                    Bean_Cargos.Factura_ID = int.Parse(reader_Maritimo.GetValue(6).ToString());
                    Bean_Cargos.Cargo_Tipo_BL = reader_Maritimo.GetValue(7).ToString();
                    Bean_Cargos.Tipo_Documento = int.Parse(reader_Maritimo.GetValue(8).ToString());
                    Bean_Cargos.Tipo_Cargo = int.Parse(reader_Maritimo.GetValue(9).ToString());
                    Bean_Cargos.Tipo_Cobro = int.Parse(reader_Maritimo.GetValue(10).ToString());
                    Bean_Cargos.ID_Intercompany = int.Parse(reader_Maritimo.GetValue(11).ToString());
                    Bean_Cargos.ID_Grupo = int.Parse(reader_Maritimo.GetValue(12).ToString());
                    Bean_Cargos.ID_Tipo_Persona = int.Parse(reader_Maritimo.GetValue(13).ToString());
                    Arr_Cargos.Add(Bean_Cargos);
                }
                DB.CloseObj(reader_Maritimo, com_Maritimo, con_Maritimo);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                return null;
            }
            #endregion
        }
        else if (SisID == 2)
        {
            #region Cargar Costos Sistema Aereo
            MySqlConnection con_Aereo;
            MySqlCommand com_Aereo;
            MySqlDataReader reader_Aereo;
            try
            {
                con_Aereo = DB.OpenAereoConnection();
                com_Aereo = new MySqlCommand();
                com_Aereo.Connection = con_Aereo;
                com_Aereo.CommandType = CommandType.Text;
                if (Tipo == 3)
                {
                    #region Importacion
                    com_Aereo.CommandText = "select ChargeID, AWBID, ItemID, ServiceID, CurrencyID, (Value+ OverSold), InvoiceID, 'Import', DocType, " +
                    "InterChargeType, Local, InterCompanyID, InterGroupID, InterProviderType " +
                    "from ChargeItems " +
                    "where AWBID=" + blID + " and DocTyp=1 and InvoiceID=0 and Expired=0 and InterChargeType=2 and InterProviderType=5;";
                    #endregion
                }
                else if (Tipo == 4)
                {
                    #region Exportacion
                    com_Aereo.CommandText = "select ChargeID, AWBID, ItemID, ServiceID, CurrencyID, (Value+ OverSold), InvoiceID, 'Import', DocType, " +
                    "InterChargeType, Local, InterCompanyID, InterGroupID, InterProviderType " +
                    "from ChargeItems " +
                    "where AWBID=" + blID + " and DocTyp=0 and InvoiceID=0 and Expired=0 and InterChargeType=2 and InterProviderType=5;";
                    #endregion
                }
                reader_Aereo = com_Aereo.ExecuteReader();
                while (reader_Aereo.Read())
                {
                    Bean_Cargos = new Bean_Cargos();
                    Bean_Cargos.Cargo_ID = int.Parse(reader_Aereo.GetValue(0).ToString());
                    Bean_Cargos.Cargo_BLID = int.Parse(reader_Aereo.GetValue(1).ToString());
                    Bean_Cargos.Cargo_Rub_ID = int.Parse(reader_Aereo.GetValue(2).ToString());
                    Bean_Cargos.Cargo_Servicio_ID = int.Parse(reader_Aereo.GetValue(3).ToString());
                    Bean_Cargos.Cargo_Moneda_Simbolo = reader_Aereo.GetValue(4).ToString();
                    Bean_Cargos.Cargo_Moneda_ID = Utility.TraducirMonedaStr(Bean_Cargos.Cargo_Moneda_Simbolo);
                    Bean_Cargos.Cargo_Monto = double.Parse(reader_Aereo.GetValue(5).ToString());
                    Bean_Cargos.Factura_ID = int.Parse(reader_Aereo.GetValue(6).ToString());
                    Bean_Cargos.Cargo_Tipo_BL = "";
                    Bean_Cargos.Tipo_Documento = int.Parse(reader_Aereo.GetValue(8).ToString());
                    Bean_Cargos.Tipo_Cargo = int.Parse(reader_Aereo.GetValue(9).ToString());
                    Bean_Cargos.Tipo_Cobro = int.Parse(reader_Aereo.GetValue(10).ToString());
                    Bean_Cargos.ID_Intercompany = int.Parse(reader_Aereo.GetValue(11).ToString());
                    Bean_Cargos.ID_Grupo = int.Parse(reader_Aereo.GetValue(12).ToString());
                    Bean_Cargos.ID_Tipo_Persona = int.Parse(reader_Aereo.GetValue(13).ToString());
                    Arr_Cargos.Add(Bean_Cargos);
                }
                DB.CloseMySQLObj(reader_Aereo, com_Aereo, con_Aereo);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                return null;
            }
            #endregion
        }
        else if (SisID == 3)
        {
            #region Cargar Costos Sistema Terrestre
            MySqlConnection con_Terrestre;
            MySqlCommand com_Terrestre;
            MySqlDataReader reader_Terrestre;
            try
            {
                con_Terrestre = DB.OpenTerrestreConnection();
                com_Terrestre = new MySqlCommand();
                com_Terrestre.Connection = con_Terrestre;
                com_Terrestre.CommandType = CommandType.Text;
                if ((Tipo == 5) || (Tipo == 6) || (Tipo == 7))
                {
                    #region Express-Consolidado-Local
                    com_Terrestre.CommandText = "select ChargeID, SBLID, ItemID, ServiceID, Currency, (Value+ OverSold), InvoiceID, 'Import', DocType, " +
                    "InterChargeType, Local, InterCompanyID, InterGroupID, InterProviderType " +
                    "from ChargeItems " +
                    "where SBLID=" + blID + " and InvoiceID=0 and Expired=0 and InterChargeType=2 and InterProviderType=5; ";
                    #endregion
                }
                reader_Terrestre = com_Terrestre.ExecuteReader();
                while (reader_Terrestre.Read())
                {
                    Bean_Cargos = new Bean_Cargos();
                    Bean_Cargos.Cargo_ID = int.Parse(reader_Terrestre.GetValue(0).ToString());
                    Bean_Cargos.Cargo_BLID = int.Parse(reader_Terrestre.GetValue(1).ToString());
                    Bean_Cargos.Cargo_Rub_ID = int.Parse(reader_Terrestre.GetValue(2).ToString());
                    Bean_Cargos.Cargo_Servicio_ID = int.Parse(reader_Terrestre.GetValue(3).ToString());
                    Bean_Cargos.Cargo_Moneda_Simbolo = reader_Terrestre.GetValue(4).ToString();
                    Bean_Cargos.Cargo_Moneda_ID = Utility.TraducirMonedaStr(Bean_Cargos.Cargo_Moneda_Simbolo);
                    Bean_Cargos.Cargo_Monto = double.Parse(reader_Terrestre.GetValue(5).ToString());
                    Bean_Cargos.Factura_ID = int.Parse(reader_Terrestre.GetValue(6).ToString());
                    Bean_Cargos.Cargo_Tipo_BL = "";
                    Bean_Cargos.Tipo_Documento = int.Parse(reader_Terrestre.GetValue(8).ToString());
                    Bean_Cargos.Tipo_Cargo = int.Parse(reader_Terrestre.GetValue(9).ToString());
                    Bean_Cargos.Tipo_Cobro = int.Parse(reader_Terrestre.GetValue(10).ToString());
                    Bean_Cargos.ID_Intercompany = int.Parse(reader_Terrestre.GetValue(11).ToString());
                    Bean_Cargos.ID_Grupo = int.Parse(reader_Terrestre.GetValue(12).ToString());
                    Bean_Cargos.ID_Tipo_Persona = int.Parse(reader_Terrestre.GetValue(13).ToString());
                    Arr_Cargos.Add(Bean_Cargos);
                }
                DB.CloseMySQLObj(reader_Terrestre, com_Terrestre, con_Terrestre);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                return null;
            }
            #endregion
        }
        return Arr_Cargos;
        #endregion
    }
    public static ArrayList Get_All_Cargos_X_Traficos(PaisBean Pais_Bean, int SisID, int Tipo, int blID)
    {
        //Aqui me quede 
        #region Get Cargos por Traficos
        string schema = "";
        int tipo_cargoID = 0;
        int tipo_personaID = 0;
        ArrayList Arr_Cargos = new ArrayList();
        Bean_Cargos Bean_Cargos = null;
        #region Definir Schema
        if ((Tipo == 17) || (Tipo == 18))
        {
            schema = Pais_Bean.schema_apl;
        }
        else
        {
            schema = Pais_Bean.schema;
        }
        #endregion
        if (SisID == 1)
        {
            #region Cargar Cargos Sistema Maritimo
            NpgsqlConnection con_Maritimo;
            NpgsqlCommand com_Maritimo;
            NpgsqlDataReader reader_Maritimo;
            try
            {
                con_Maritimo = DB.OpenVentasConnection(schema);
                com_Maritimo = new NpgsqlCommand();
                com_Maritimo.Connection = con_Maritimo;
                com_Maritimo.CommandType = CommandType.Text;
                if ((Tipo == 1) || (Tipo == 17))
                {
                    #region FCL
                    com_Maritimo.CommandText = "select cargo_id, bl_id, id_rubro, id_servicio, id_moneda, coalesce(valor_collect,0) + coalesce(valor_sobreventa,0), factura_id, " +
                    "tipo_bl, tipo_documento, tipo_cargo, tipo_cobro, inter_company, id_grupo, id_tipo_persona " +
                    "from cargos_bl " +
                    //"where bl_id=" + blID + " and tipo_bl='F' and activo=TRUE and tipo_cargo="+tipo_cargoID+" and id_tipo_persona="++" ";
                    "where bl_id=" + blID + " and tipo_bl='F' and activo=TRUE and tipo_cargo=2 and id_tipo_persona=5 ";
                    #endregion
                }
                else if ((Tipo == 2) || (Tipo == 18))
                {
                    #region LCL
                    com_Maritimo.CommandText = "select cargo_id, bl_id, id_rubro, id_servicio, id_moneda, coalesce(valor_collect,0) + coalesce(valor_sobreventa,0), factura_id, " +
                    "tipo_bl, tipo_documento, tipo_cargo, tipo_cobro, inter_company, id_grupo, id_tipo_persona " +
                    "from cargos_bl " +
                    "where bl_id=" + blID + " and tipo_bl='L' and activo=TRUE and tipo_cargo=2 and id_tipo_persona=5 ";
                    #endregion
                }
                reader_Maritimo = com_Maritimo.ExecuteReader();
                while (reader_Maritimo.Read())
                {
                    //Revisar Traduccion ID de Monedas
                    Bean_Cargos = new Bean_Cargos();
                    Bean_Cargos.Cargo_ID = int.Parse(reader_Maritimo.GetValue(0).ToString());
                    Bean_Cargos.Cargo_BLID = int.Parse(reader_Maritimo.GetValue(1).ToString());
                    Bean_Cargos.Cargo_Rub_ID = int.Parse(reader_Maritimo.GetValue(2).ToString());
                    Bean_Cargos.Cargo_Servicio_ID = int.Parse(reader_Maritimo.GetValue(3).ToString());
                    Bean_Cargos.Cargo_Moneda_Trafico_ID = int.Parse(reader_Maritimo.GetValue(4).ToString());
                    Bean_Cargos.Cargo_Moneda_ID = Contabilizacion_Automatica_CN.Traducir_Moneda_Master_To_BAW_X_ID(Bean_Cargos.Cargo_Moneda_Trafico_ID);
                    Bean_Cargos.Cargo_Monto = double.Parse(reader_Maritimo.GetValue(5).ToString());
                    Bean_Cargos.Factura_ID = int.Parse(reader_Maritimo.GetValue(6).ToString());
                    Bean_Cargos.Cargo_Tipo_BL = reader_Maritimo.GetValue(7).ToString();
                    Bean_Cargos.Tipo_Documento = int.Parse(reader_Maritimo.GetValue(8).ToString());
                    Bean_Cargos.Tipo_Cargo = int.Parse(reader_Maritimo.GetValue(9).ToString());
                    Bean_Cargos.Tipo_Cobro = int.Parse(reader_Maritimo.GetValue(10).ToString());
                    Bean_Cargos.ID_Intercompany = int.Parse(reader_Maritimo.GetValue(11).ToString());
                    Bean_Cargos.ID_Grupo = int.Parse(reader_Maritimo.GetValue(12).ToString());
                    Bean_Cargos.ID_Tipo_Persona = int.Parse(reader_Maritimo.GetValue(13).ToString());
                    Arr_Cargos.Add(Bean_Cargos);
                }
                DB.CloseObj(reader_Maritimo, com_Maritimo, con_Maritimo);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                return null;
            }
            #endregion
        }
        else if (SisID == 2)
        {
            #region Cargar Costos Sistema Aereo
            MySqlConnection con_Aereo;
            MySqlCommand com_Aereo;
            MySqlDataReader reader_Aereo;
            try
            {
                con_Aereo = DB.OpenAereoConnection();
                com_Aereo = new MySqlCommand();
                com_Aereo.Connection = con_Aereo;
                com_Aereo.CommandType = CommandType.Text;
                if (Tipo == 3)
                {
                    #region Importacion
                    com_Aereo.CommandText = "select ChargeID, AWBID, ItemID, ServiceID, CurrencyID, (Value+ OverSold), InvoiceID, 'Import', DocType, " +
                    "InterChargeType, Local, InterCompanyID, InterGroupID, InterProviderType " +
                    "from ChargeItems " +
                    "where AWBID=" + blID + " and DocTyp=1 and InvoiceID=0 and Expired=0 and InterChargeType=2 and InterProviderType=5;";
                    #endregion
                }
                else if (Tipo == 4)
                {
                    #region Exportacion
                    com_Aereo.CommandText = "select ChargeID, AWBID, ItemID, ServiceID, CurrencyID, (Value+ OverSold), InvoiceID, 'Import', DocType, " +
                    "InterChargeType, Local, InterCompanyID, InterGroupID, InterProviderType " +
                    "from ChargeItems " +
                    "where AWBID=" + blID + " and DocTyp=0 and InvoiceID=0 and Expired=0 and InterChargeType=2 and InterProviderType=5;";
                    #endregion
                }
                reader_Aereo = com_Aereo.ExecuteReader();
                while (reader_Aereo.Read())
                {
                    Bean_Cargos = new Bean_Cargos();
                    Bean_Cargos.Cargo_ID = int.Parse(reader_Aereo.GetValue(0).ToString());
                    Bean_Cargos.Cargo_BLID = int.Parse(reader_Aereo.GetValue(1).ToString());
                    Bean_Cargos.Cargo_Rub_ID = int.Parse(reader_Aereo.GetValue(2).ToString());
                    Bean_Cargos.Cargo_Servicio_ID = int.Parse(reader_Aereo.GetValue(3).ToString());
                    Bean_Cargos.Cargo_Moneda_Simbolo = reader_Aereo.GetValue(4).ToString();
                    Bean_Cargos.Cargo_Moneda_ID = Utility.TraducirMonedaStr(Bean_Cargos.Cargo_Moneda_Simbolo);
                    Bean_Cargos.Cargo_Monto = double.Parse(reader_Aereo.GetValue(5).ToString());
                    Bean_Cargos.Factura_ID = int.Parse(reader_Aereo.GetValue(6).ToString());
                    Bean_Cargos.Cargo_Tipo_BL = "";
                    Bean_Cargos.Tipo_Documento = int.Parse(reader_Aereo.GetValue(8).ToString());
                    Bean_Cargos.Tipo_Cargo = int.Parse(reader_Aereo.GetValue(9).ToString());
                    Bean_Cargos.Tipo_Cobro = int.Parse(reader_Aereo.GetValue(10).ToString());
                    Bean_Cargos.ID_Intercompany = int.Parse(reader_Aereo.GetValue(11).ToString());
                    Bean_Cargos.ID_Grupo = int.Parse(reader_Aereo.GetValue(12).ToString());
                    Bean_Cargos.ID_Tipo_Persona = int.Parse(reader_Aereo.GetValue(13).ToString());
                    Arr_Cargos.Add(Bean_Cargos);
                }
                DB.CloseMySQLObj(reader_Aereo, com_Aereo, con_Aereo);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                return null;
            }
            #endregion
        }
        else if (SisID == 3)
        {
            #region Cargar Costos Sistema Terrestre
            MySqlConnection con_Terrestre;
            MySqlCommand com_Terrestre;
            MySqlDataReader reader_Terrestre;
            try
            {
                con_Terrestre = DB.OpenTerrestreConnection();
                com_Terrestre = new MySqlCommand();
                com_Terrestre.Connection = con_Terrestre;
                com_Terrestre.CommandType = CommandType.Text;
                if ((Tipo == 5) || (Tipo == 6) || (Tipo == 7))
                {
                    #region Express-Consolidado-Local
                    com_Terrestre.CommandText = "select ChargeID, SBLID, ItemID, ServiceID, Currency, (Value+ OverSold), InvoiceID, 'Import', DocType, " +
                    "InterChargeType, Local, InterCompanyID, InterGroupID, InterProviderType " +
                    "from ChargeItems " +
                    "where SBLID=" + blID + " and InvoiceID=0 and Expired=0 and InterChargeType=2 and InterProviderType=5; ";
                    #endregion
                }
                reader_Terrestre = com_Terrestre.ExecuteReader();
                while (reader_Terrestre.Read())
                {
                    Bean_Cargos = new Bean_Cargos();
                    Bean_Cargos.Cargo_ID = int.Parse(reader_Terrestre.GetValue(0).ToString());
                    Bean_Cargos.Cargo_BLID = int.Parse(reader_Terrestre.GetValue(1).ToString());
                    Bean_Cargos.Cargo_Rub_ID = int.Parse(reader_Terrestre.GetValue(2).ToString());
                    Bean_Cargos.Cargo_Servicio_ID = int.Parse(reader_Terrestre.GetValue(3).ToString());
                    Bean_Cargos.Cargo_Moneda_Simbolo = reader_Terrestre.GetValue(4).ToString();
                    Bean_Cargos.Cargo_Moneda_ID = Utility.TraducirMonedaStr(Bean_Cargos.Cargo_Moneda_Simbolo);
                    Bean_Cargos.Cargo_Monto = double.Parse(reader_Terrestre.GetValue(5).ToString());
                    Bean_Cargos.Factura_ID = int.Parse(reader_Terrestre.GetValue(6).ToString());
                    Bean_Cargos.Cargo_Tipo_BL = "";
                    Bean_Cargos.Tipo_Documento = int.Parse(reader_Terrestre.GetValue(8).ToString());
                    Bean_Cargos.Tipo_Cargo = int.Parse(reader_Terrestre.GetValue(9).ToString());
                    Bean_Cargos.Tipo_Cobro = int.Parse(reader_Terrestre.GetValue(10).ToString());
                    Bean_Cargos.ID_Intercompany = int.Parse(reader_Terrestre.GetValue(11).ToString());
                    Bean_Cargos.ID_Grupo = int.Parse(reader_Terrestre.GetValue(12).ToString());
                    Bean_Cargos.ID_Tipo_Persona = int.Parse(reader_Terrestre.GetValue(13).ToString());
                    Arr_Cargos.Add(Bean_Cargos);
                }
                DB.CloseMySQLObj(reader_Terrestre, com_Terrestre, con_Terrestre);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                return null;
            }
            #endregion
        }
        return Arr_Cargos;
        #endregion
    }
    public static int Insertar_Configuracion_Intercompany_Operativo(UsuarioBean user, RE_GenericBean Bean)
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
            comm.CommandText = "insert into tbl_intercompany_operativo (tio_id_intercompany, tio_tiott_id, tio_ttr_id, tio_contabilidad_id, tio_moneda_id, tio_suc_id, tio_operacion_id, tio_serie, tio_usuario_creacion, tio_tsis_id)";
            comm.CommandText += " values (@tio_id_intercompany, @tio_tiott_id, @tio_ttr_id, @tio_contabilidad_id, @tio_moneda_id, @tio_suc_id, @tio_operacion_id, @tio_serie, @tio_usuario_creacion, @tio_tsis_id)";
            comm.Parameters.Add("@tio_id_intercompany", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC1;
            comm.Parameters.Add("@tio_tiott_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC2;
            comm.Parameters.Add("@tio_ttr_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC3;
            comm.Parameters.Add("@tio_contabilidad_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC4;
            comm.Parameters.Add("@tio_moneda_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC5;
            comm.Parameters.Add("@tio_suc_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC6;
            comm.Parameters.Add("@tio_operacion_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC7;
            comm.Parameters.Add("@tio_serie", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC1;
            comm.Parameters.Add("@tio_usuario_creacion", NpgsqlTypes.NpgsqlDbType.Varchar).Value = user.ID;
            comm.Parameters.Add("@tio_tsis_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC8;
            resultado = comm.ExecuteNonQuery();
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
    public static ArrayList Obtener_Configuraciones_Intercompany_Operativo(UsuarioBean user, string sql)
    {
        ArrayList Arr_Result = new ArrayList();
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
            comm.CommandText = "select a.tio_id, a.tio_id_intercompany, a.tio_tiott_id, a.tio_ttr_id, a.tio_contabilidad_id, a.tio_moneda_id, a.tio_suc_id, a.tio_operacion_id, a.tio_serie, " +
            "NOMBRE_INTERCOMAPNY, b.tiott_nombre, upper(c.ttr_nombre), d.tcon_nombre, substring(e.ttm_nombre from 1 for 3), f.suc_nombre, a.tio_tsis_id, g.tsis_nombre " +
            "from tbl_intercompany_operativo a " +
            "inner join dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select  id_intercompany, nombre_comercial from intercompanys')" +
            "Intercompany_Result (ID_INTERCOMPANY integer, NOMBRE_INTERCOMAPNY varchar) on (a.tio_id_intercompany=ID_INTERCOMPANY)" +
            "inner join tbl_intercompany_operativo_tipo_transaccion b on (a.tio_tiott_id=b.tiott_id)" +
            "inner join sys_tipo_referencia c on (a.tio_ttr_id=c.ttr_id)" +
            "inner join tbl_tipo_conta d on (a.tio_contabilidad_id=d.tcon_id)" +
            "inner join tbl_tipo_moneda e on (a.tio_moneda_id=e.ttm_id)" +
            "inner join tbl_sucursal f on (a.tio_suc_id=f.suc_id)" +
            "inner join tbl_sistemas g on (a.tio_tsis_id=g.tsis_id)" +
            "where a.tio_estado=1 " + sql + " ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.intC1 = int.Parse(reader.GetValue(0).ToString());//tio_id
                Bean.intC2 = int.Parse(reader.GetValue(1).ToString());//tio_id_intercompany
                Bean.intC3 = int.Parse(reader.GetValue(2).ToString());//tio_tiott_id
                Bean.intC4 = int.Parse(reader.GetValue(3).ToString());//tio_ttr_id
                Bean.intC5 = int.Parse(reader.GetValue(4).ToString());//tio_contabilidad_id
                Bean.intC6 = int.Parse(reader.GetValue(5).ToString());//tio_moneda_id
                Bean.intC7 = int.Parse(reader.GetValue(6).ToString());//tio_suc_id
                Bean.intC8 = int.Parse(reader.GetValue(7).ToString());//tio_operacion_id
                Bean.strC1 = reader.GetValue(9).ToString();//NOMBRE_INTERCOMAPNY
                Bean.strC2 = reader.GetValue(10).ToString();//tiott_nombre
                Bean.strC3 = reader.GetValue(11).ToString();//ttr_nombre
                Bean.strC4 = reader.GetValue(12).ToString();//tcon_nombre
                Bean.strC5 = reader.GetValue(13).ToString();//ttm_nombre
                Bean.strC6 = reader.GetValue(14).ToString();//suc_nombre
                Bean.strC7 = reader.GetValue(8).ToString();//tio_serie
                Bean.intC9 = int.Parse(reader.GetValue(15).ToString());//tio_tsis_id
                Bean.strC8 = reader.GetValue(16).ToString();//tsis_nombre
                Arr_Result.Add(Bean);
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Arr_Result;
    }
    public static int Eliminar_Configuracion_Intercompany_Operativo(UsuarioBean user, int configuracionID)
    {
        int result = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "update tbl_intercompany_operativo set tio_estado=0, tio_fecha_eliminacion=now(), tio_usuario_eliminacion='" + user.ID + "' where tio_id=" + configuracionID + "";
            result = comm.ExecuteNonQuery();
            DB.CloseObj_insert(comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return result;
    }
    public static int Validar_Existencia_Intercompany_Operativo(UsuarioBean user, string sql)
    {
        int resultado = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select tio_id from tbl_intercompany_operativo where tio_estado=1 " + sql + "";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                resultado = int.Parse(reader.GetValue(0).ToString());
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return resultado;
    }
    public static ArrayList Construir_Factura_Intercompany_Operativo(RE_GenericBean Bean_Configuracion_Transaccion, Bean_Datos_BL Datos_BL, ArrayList Arr_Cargos_BL, int intercompanyORIGEN_ID, int intercompanyDESTINO_ID, string usuID)
    {
        #region Construir Factura Automatica Intercompany Operativo
        ArrayList Arr_Result = null;
        int Correlativo = 0;
        int tipo_cobro = 1;//prepaid, collect
        string sql = "";
        Rubros Rubro_Bean = null;
        try
        {
            RE_GenericBean Bean_Intercompany_Origen = (RE_GenericBean)DB.Get_Intercompany_Data(intercompanyORIGEN_ID);
            RE_GenericBean Bean_Intercompany_Destino = (RE_GenericBean)DB.Get_Intercompany_Data(intercompanyDESTINO_ID);
            PaisBean Pais_Bean_Origen = DB.getPais(Bean_Intercompany_Origen.intC3);
            PaisBean Pais_Bean_Destino = DB.getPais(Bean_Intercompany_Destino.intC3);
            Correlativo = DB.GetCorr(Bean_Configuracion_Transaccion.intC7, 1, Bean_Configuracion_Transaccion.strC7);
            Bean_Factura_Automatica Factura_Automatica = new Bean_Factura_Automatica();
            Factura_Automatica.tfa_id = 0;
            Factura_Automatica.tfa_correlativo = Correlativo.ToString();
            Factura_Automatica.tfa_nit = Bean_Intercompany_Destino.strC2;
            Factura_Automatica.tfa_nombre = Bean_Intercompany_Destino.strC1;
            Factura_Automatica.tfa_direccion = Bean_Intercompany_Destino.strC3;
            Factura_Automatica.tfa_fecha_emision = DB.getDateTimeNow().Substring(0, 19);
            Factura_Automatica.tfa_fecha_pago = DB.getDateTimeNow().Substring(0, 19);
            Factura_Automatica.tfa_sub_total = 0;
            Factura_Automatica.tfa_impuesto = 0;
            Factura_Automatica.tfa_total = 0;
            Factura_Automatica.tfa_observacion = "";
            Factura_Automatica.tfa_suc_id = Bean_Configuracion_Transaccion.intC7;
            Factura_Automatica.tfa_cli_id = intercompanyDESTINO_ID;
            Factura_Automatica.tfa_moneda = Bean_Configuracion_Transaccion.intC6;
            Factura_Automatica.tfa_ted_id = 1;
            Factura_Automatica.tfa_usu_id = usuID;
            Factura_Automatica.tfa_hbl = Datos_BL.Hbl;
            Factura_Automatica.tfa_mbl = Datos_BL.Mbl;
            Factura_Automatica.tfa_contenedor = Datos_BL.Contenedor;
            Factura_Automatica.tfa_routing = Datos_BL.Routing;
            sql = "select nombre from navieras where activo=true and id_naviera=" + Datos_BL.Naviera;
            Factura_Automatica.tfa_naviera = DB.getName(sql);
            Factura_Automatica.tfa_vapor = Datos_BL.Vapor;
            sql = "select nombre_cliente from clientes where id_cliente=" + Datos_BL.Shipper;
            Factura_Automatica.tfa_shipper = DB.getName(sql);
            Factura_Automatica.tfa_ordenpo = "";
            sql = "select nombre_cliente from clientes where id_cliente=" + Datos_BL.Consignatario;
            Factura_Automatica.tfa_consignee = DB.getName(sql);
            sql = "select namees from commodities where commodityid =" + Datos_BL.Comodity;
            Factura_Automatica.tfa_comodity = DB.getName(sql);
            Factura_Automatica.tfa_paquetes = (string)DB.GetNombreTipoBulto(Datos_BL.Tipo_Paquete);
            Factura_Automatica.tfa_peso = Datos_BL.Peso;
            Factura_Automatica.tfa_volumen = Datos_BL.Volumen;
            Factura_Automatica.tfa_dua_ingreso = "";
            Factura_Automatica.tfa_dua_salida = "";
            Factura_Automatica.tfa_vendedor1 = Datos_BL.Vendedor1;
            Factura_Automatica.tfa_vendedor2 = Datos_BL.Vendedor2;
            Factura_Automatica.tfa_razon = Bean_Intercompany_Destino.strC1;
            Factura_Automatica.tfa_referencia = "";
            Factura_Automatica.tfa_serie = Bean_Configuracion_Transaccion.strC7;
            Factura_Automatica.tfa_id_shipper = Datos_BL.Shipper;
            Factura_Automatica.tfa_consignee = Datos_BL.Consignatario.ToString();
            Factura_Automatica.tfa_pai_id = Bean_Intercompany_Origen.intC3;
            Factura_Automatica.tfa_conta_id = Bean_Configuracion_Transaccion.intC5;
            Factura_Automatica.tfa_sub_total_eq = 0;
            Factura_Automatica.tfa_impuesto_eq = 0;
            Factura_Automatica.tfa_total_eq = 0;
            Factura_Automatica.tfa_tie_id = Datos_BL.Import_Export;
            Factura_Automatica.tfa_ttc_id = 1;
            Factura_Automatica.tfa_allin = "";
            Factura_Automatica.tfa_reciboaduana = "";
            Factura_Automatica.tfa_cant_paquetes = Datos_BL.No_Piezas.ToString();
            Factura_Automatica.tfa_agent_id = Datos_BL.Agente;
            sql = "select agente from agentes where activo=true and agente_id=" + Datos_BL.Agente;
            Factura_Automatica.tfa_agente = DB.getName(sql);
            Factura_Automatica.tfa_recibo_agencia = "";
            Factura_Automatica.tfa_valor_aduanero = "";
            Factura_Automatica.tfa_ruc = Bean_Intercompany_Destino.strC6;
            Factura_Automatica.tfa_giro = "";
            Factura_Automatica.tfa_ttf_id = 1;
            Factura_Automatica.tfa_ruta_pais = "";
            Factura_Automatica.tfa_ruta = "";
            Factura_Automatica.tfa_observacion2 = "";
            Factura_Automatica.tfa_tra_id = 0;
            Factura_Automatica.tfa_blid = Datos_BL.BLID;
            Factura_Automatica.tfa_tto_id = Datos_BL.ttoID;
            Factura_Automatica.tfa_esignature = "-";
            Factura_Automatica.tfa_fac_electronica = 0;
            Factura_Automatica.tfa_internal_reference = "0";
            Factura_Automatica.tfa_guid = "0";
            Factura_Automatica.tfa_correo_documento_electronico = "-";
            Factura_Automatica.tfa_referencia_correo = "-";
            Factura_Automatica.tfa_innerxml = "";
            Factura_Automatica.tfa_fecha_batch = DB.getDateTimeNow().Substring(0, 10);
            if (Factura_Automatica.tfa_conta_id == 1)
            {
                Factura_Automatica.tfa_tti_id = 2;
            }
            else if (Factura_Automatica.tfa_conta_id == 2)
            {
                Factura_Automatica.tfa_tti_id = 1;
            }
            Factura_Automatica.tfa_correlativo_electronico = "0";
            Factura_Automatica.tfa_no_factura_aduana = "-";
            Factura_Automatica.tfa_no_embarque = "-";
            Factura_Automatica.tfa_tpi_id = 10;
            #region Generar Detalle de Rubros
            Bean_Detalle_Rubros Detalle_Rubros = null;
            foreach (Bean_Cargos Cargo_BL in Arr_Cargos_BL)
            {
                Detalle_Rubros = new Bean_Detalle_Rubros();
                Detalle_Rubros.tdf_cargo_id = Cargo_BL.Cargo_ID;
                Detalle_Rubros.tdf_tts_id = Cargo_BL.Cargo_Servicio_ID;
                Detalle_Rubros.tdf_rub_id = Cargo_BL.Cargo_Rub_ID;
                Detalle_Rubros.tdf_montosinimpuesto = Cargo_BL.Cargo_Monto;

                //BK
                //Detalle_Rubros.tdf_montosinimpuesto = Contabilizacion_Automatica_CN.Convertir_Divisas_Intercompanys(Factura_Automatica.tfa_pai_id, Cargo_BL.Cargo_Moneda_ID, Detalle_Rubros.tdf_montosinimpuesto, Bean_Intercompany_Origen.intC3, Bean_Configuracion_Transaccion.intC6);
                //Rubro_Bean = Contabilizacion_Automatica_CN.Calcular_Impuestos(Factura_Automatica.tfa_pai_id, Factura_Automatica.tfa_conta_id, Factura_Automatica.tfa_moneda, Detalle_Rubros.tdf_tts_id, Detalle_Rubros.tdf_rub_id, Detalle_Rubros.tdf_montosinimpuesto, Factura_Automatica.tfa_tti_id, Pais_Bean_Origen);

                Detalle_Rubros.tdf_montosinimpuesto = Contabilizacion_Automatica_CN.Convertir_Divisas_Intercompanys(Factura_Automatica.tfa_pai_id, Cargo_BL.Cargo_Moneda_ID, Detalle_Rubros.tdf_montosinimpuesto, Bean_Intercompany_Origen.intC3, Bean_Configuracion_Transaccion.intC6);
                if (Detalle_Rubros.tdf_tts_id == 14)
                {
                    //Si es un Cargo por Terceros enviarlo como excento
                    Rubro_Bean = Contabilizacion_Automatica_CN.Calcular_Impuestos(Factura_Automatica.tfa_pai_id, Factura_Automatica.tfa_conta_id, Factura_Automatica.tfa_moneda, Detalle_Rubros.tdf_tts_id, Detalle_Rubros.tdf_rub_id, Detalle_Rubros.tdf_montosinimpuesto, 1, Pais_Bean_Origen);
                }
                else
                {
                    //Si no es un Cargo por Terceros enviarlo como afecto o excento en base a Master Aimar
                    Rubro_Bean = Contabilizacion_Automatica_CN.Calcular_Impuestos(Factura_Automatica.tfa_pai_id, Factura_Automatica.tfa_conta_id, Factura_Automatica.tfa_moneda, Detalle_Rubros.tdf_tts_id, Detalle_Rubros.tdf_rub_id, Detalle_Rubros.tdf_montosinimpuesto, Factura_Automatica.tfa_tti_id, Pais_Bean_Origen);
                }

                Detalle_Rubros.tdf_ttm_id = Bean_Configuracion_Transaccion.intC6;
                Detalle_Rubros.tdf_montosinimpuesto = Rubro_Bean.rubroSubTot;
                Detalle_Rubros.tdf_impuesto = Rubro_Bean.rubroImpuesto;
                Detalle_Rubros.tdf_monto = Rubro_Bean.rubroTot;
                Detalle_Rubros.tdf_total_equivalente = Rubro_Bean.rubroTotD;

                Factura_Automatica.tfa_sub_total += Detalle_Rubros.tdf_montosinimpuesto;
                Factura_Automatica.tfa_impuesto += Detalle_Rubros.tdf_impuesto;
                Factura_Automatica.tfa_total += Detalle_Rubros.tdf_monto;
                Factura_Automatica.tfa_total_eq += Detalle_Rubros.tdf_total_equivalente;

                Detalle_Rubros.tdf_tfa_id = 0;
                Detalle_Rubros.tdf_ttr_id = 1;
                #region Calcular Total Equivalente
                if (Factura_Automatica.tfa_moneda == 8)
                {
                    decimal Tipo_Cambio_Temporal = DB.getTipoCambioHoy(Factura_Automatica.tfa_pai_id);
                    Factura_Automatica.tfa_impuesto_eq += Math.Round(Detalle_Rubros.tdf_impuesto * (double)Tipo_Cambio_Temporal, 2);
                    Factura_Automatica.tfa_sub_total_eq += Math.Round(Detalle_Rubros.tdf_montosinimpuesto * (double)Tipo_Cambio_Temporal, 2);
                }
                else
                {
                    decimal Tipo_Cambio_Temporal = DB.getTipoCambioHoy(Factura_Automatica.tfa_pai_id);
                    Factura_Automatica.tfa_impuesto_eq += Math.Round(Detalle_Rubros.tdf_impuesto / (double)Tipo_Cambio_Temporal, 2);
                    Factura_Automatica.tfa_sub_total_eq += Math.Round(Detalle_Rubros.tdf_montosinimpuesto / (double)Tipo_Cambio_Temporal, 2);
                }
                #endregion
                Detalle_Rubros.tdf_comentarios = "";
                int transID = 108;
                int tttID = 0;
                if (Factura_Automatica.tfa_conta_id == 2)
                {
                    tttID = 7;
                }
                else
                {
                    tttID = 1;
                }
                Detalle_Rubros.cta_haber = (ArrayList)DB.getCtaContablebyRubro("haber", (int)Detalle_Rubros.tdf_rub_id, Factura_Automatica.tfa_pai_id, tttID, Factura_Automatica.tfa_tti_id, Factura_Automatica.tfa_moneda, Factura_Automatica.tfa_tie_id, tipo_cobro, Factura_Automatica.tfa_conta_id, Detalle_Rubros.tdf_tts_id);
                if (Detalle_Rubros.cta_haber == null)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    string nombre_servicio = "";
                    string nombre_rubro = "";
                    RE_GenericBean Bean_Rubro_Aux = null;
                    nombre_servicio = Utility.TraducirServiciotoSTR(Detalle_Rubros.tdf_tts_id);
                    Bean_Rubro_Aux = DB.getRubro(Detalle_Rubros.tdf_rub_id);
                    nombre_rubro = Bean_Rubro_Aux.strC1;
                    Arr_Result.Add("No existe combinacion de cuentas contables para el Servicio " + nombre_servicio + " y Rubro " + nombre_rubro + " con ID (" + Detalle_Rubros.tdf_rub_id + "), en la Empresa " + Bean_Intercompany_Origen.strC5 + " para emitir Factura.");
                    return Arr_Result;
                }
                if (Detalle_Rubros.cta_haber.Count == 0)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    string nombre_servicio = "";
                    string nombre_rubro = "";
                    RE_GenericBean Bean_Rubro_Aux = null;
                    nombre_servicio = Utility.TraducirServiciotoSTR(Detalle_Rubros.tdf_tts_id);
                    Bean_Rubro_Aux = DB.getRubro(Detalle_Rubros.tdf_rub_id);
                    nombre_rubro = Bean_Rubro_Aux.strC1;
                    Arr_Result.Add("No existe combinacion de cuentas contables para el Servicio " + nombre_servicio + " y Rubro " + nombre_rubro + " con ID (" + Detalle_Rubros.tdf_rub_id + "), en la Empresa " + Bean_Intercompany_Origen.strC5 + " para emitir Factura.");
                    return Arr_Result;
                }
                Factura_Automatica.tfa_ttt_id = transID;
                int _matOpID_Destino = DB.getMatrizOperacionID(transID, Factura_Automatica.tfa_moneda, Factura_Automatica.tfa_pai_id, Factura_Automatica.tfa_conta_id);
                ArrayList ctas_cargo_Destino = (ArrayList)DB.getMatrizConfiguracion_ingreso_egreso(_matOpID_Destino, "Cargo");
                if ((ctas_cargo_Destino == null) || (ctas_cargo_Destino.Count == 0))
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    Arr_Result.Add("No Existe configuracion contable para la Provision en Destino , por favor pongase en contacto con el Contador");
                    return Arr_Result;
                }
                Factura_Automatica.ctas_abono = ctas_cargo_Destino;
                if (Factura_Automatica.Arr_Detalle_Facturacion == null) Factura_Automatica.Arr_Detalle_Facturacion = new ArrayList();
                Factura_Automatica.Arr_Detalle_Facturacion.Add(Detalle_Rubros);
                Arr_Result = new ArrayList();
                Arr_Result.Add("1");
                Arr_Result.Add(Factura_Automatica);
            }
            #endregion
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            Arr_Result = new ArrayList();
            Arr_Result.Add("Existio un error al tratar de Construir el documento de Cobro al Intercompany Destino");
            Arr_Result.Add(null);
            return Arr_Result;
        }
        return Arr_Result;
        #endregion
    }
    public static ArrayList Construir_Provision_Intercompany_Operativo(RE_GenericBean Bean_Configuracion_Transaccion, Bean_Datos_BL Datos_BL, ArrayList Arr_Cargos_BL, int intercompanyORIGEN_ID, int intercompanyDESTINO_ID, string usuID)
    {
        #region Construir Provision Automatica Intercompany Operativo
        ArrayList Arr_Result = null;
        int tipo_cobro = 1;//prepaid, collect
        Rubros Rubro_Bean = null;
        try
        {
            RE_GenericBean Bean_Intercompany_Origen = (RE_GenericBean)DB.Get_Intercompany_Data(intercompanyORIGEN_ID);
            RE_GenericBean Bean_Intercompany_Destino = (RE_GenericBean)DB.Get_Intercompany_Data(intercompanyDESTINO_ID);
            PaisBean Pais_Bean_Origen = DB.getPais(Bean_Intercompany_Origen.intC3);
            PaisBean Pais_Bean_Destino = DB.getPais(Bean_Intercompany_Destino.intC3);
            Bean_Provision_Automatica Provision_Automatica = new Bean_Provision_Automatica();
            Provision_Automatica.tpr_prov_id = 0;
            Provision_Automatica.tpr_proveedor_id = intercompanyORIGEN_ID;
            Provision_Automatica.tpr_fact_id = "";
            Provision_Automatica.tpr_fact_fecha = DateTime.Now.ToString("yyyy-MM-dd");
            Provision_Automatica.tpr_fecha_maxpago = DateTime.Now.ToString("yyyy-MM-dd"); ;
            Provision_Automatica.tpr_valor = 0;
            Provision_Automatica.tpr_afecto = 0;
            Provision_Automatica.tpr_noafecto = 0;
            Provision_Automatica.tpr_iva = 0;
            Provision_Automatica.tpr_observacion = "";
            Provision_Automatica.tpr_suc_id = Bean_Configuracion_Transaccion.intC7;
            Provision_Automatica.tpr_pai_id = Pais_Bean_Destino.ID;
            Provision_Automatica.tpr_usu_creacion = usuID;
            Provision_Automatica.tpr_fecha_creacion = DateTime.Now.ToString("yyyy-MM-dd");
            Provision_Automatica.tpr_usu_acepta = usuID;
            Provision_Automatica.tpr_fecha_acepta = DateTime.Now.ToString("yyyy-MM-dd");
            Provision_Automatica.tpr_departamento = 0;
            Provision_Automatica.tpr_ted_id = 5;
            Provision_Automatica.tpr_serie = Bean_Configuracion_Transaccion.strC7;
            Provision_Automatica.tpr_serie_oc = "";
            Provision_Automatica.tpr_correlativo_oc = 0;
            Provision_Automatica.tpr_tts_id = 0;
            Provision_Automatica.tpr_hbl = Datos_BL.Hbl;
            Provision_Automatica.tpr_mbl = Datos_BL.Mbl;
            Provision_Automatica.tpr_routing = Datos_BL.Routing;
            Provision_Automatica.tpr_contenedor = Datos_BL.Contenedor;
            Provision_Automatica.tpr_tpi_id = 10;
            Provision_Automatica.tpr_correlativo = 0;
            Provision_Automatica.tpr_mon_id = Bean_Configuracion_Transaccion.intC6;
            Provision_Automatica.tpr_serie_contrasena = "";
            Provision_Automatica.tpr_contrasena_correlativo = 0;
            Provision_Automatica.tpr_valor_equivalente = 0;
            Provision_Automatica.tpr_fact_corr = "";
            Provision_Automatica.tpr_proveedor_cajachica = "";
            Provision_Automatica.tpr_imp_exp_id = Datos_BL.Import_Export;
            Provision_Automatica.tpr_imp_exp_id = 1;//Toda la Carga enviada a Collectar se vuelve Importacion
            Provision_Automatica.tpr_bien_serv = 2;
            Provision_Automatica.tpr_tcon_id = Bean_Configuracion_Transaccion.intC5;
            Provision_Automatica.tpr_nombre = Bean_Intercompany_Origen.strC1;
            Provision_Automatica.tpr_proveedor_cajachica_id = 0;
            Provision_Automatica.tpr_poliza = "";
            Provision_Automatica.tpr_fecha_libro_compras = DateTime.Now.ToString("yyyy-MM-dd");
            Provision_Automatica.tpr_tto_id = Datos_BL.ttoID;
            Provision_Automatica.tpr_ruta_pais = "";
            Provision_Automatica.tpr_ruta = "";
            Provision_Automatica.tpr_blid = Datos_BL.BLID;
            Provision_Automatica.tpr_tti_id = 1;// Excento
            Provision_Automatica.tpr_usu_modifica_regimen = "";
            Provision_Automatica.tpr_usu_anula = "";
            Provision_Automatica.tpr_fecha_anula = null;
            Provision_Automatica.tpr_toc_id = 0;
            Provision_Automatica.tpr_observacion_contrasena = "";
            Provision_Automatica.tpr_fecha_recibo_factura = null;
            Provision_Automatica.tpr_mbl_modificado = false;
            Provision_Automatica.tpr_ttd_id = 3;
            Provision_Automatica.tpr_tds_id = 0;
            #region Generar Detalle de Rubros
            Bean_Detalle_Rubros Detalle_Rubros = null;
            foreach (Bean_Cargos Cargo_BL in Arr_Cargos_BL)
            {
                Detalle_Rubros = new Bean_Detalle_Rubros();
                Detalle_Rubros.tdf_cargo_id = Cargo_BL.Cargo_ID;
                Detalle_Rubros.tdf_tts_id = 14;//Terceros
                Detalle_Rubros.tdf_rub_id = Cargo_BL.Cargo_Rub_ID;
                Detalle_Rubros.tdf_montosinimpuesto = Cargo_BL.Cargo_Monto;
                //Detalle_Rubros.tdf_montosinimpuesto = Contabilizacion_Automatica_CN.Convertir_Divisas_Intercompanys(Provision_Automatica.tpr_pai_id, Cargo_BL.Cargo_Moneda_ID, Detalle_Rubros.tdf_montosinimpuesto, Bean_Intercompany_Origen.intC3, Bean_Configuracion_Transaccion.intC6);
                Detalle_Rubros.tdf_montosinimpuesto = Contabilizacion_Automatica_CN.Convertir_Divisas_Intercompanys(Provision_Automatica.tpr_pai_id, Cargo_BL.Cargo_Moneda_ID, Detalle_Rubros.tdf_montosinimpuesto, Bean_Intercompany_Destino.intC3, Bean_Configuracion_Transaccion.intC6);
                Rubro_Bean = Contabilizacion_Automatica_CN.Calcular_Impuestos(Provision_Automatica.tpr_pai_id, Provision_Automatica.tpr_tcon_id, Provision_Automatica.tpr_mon_id, Detalle_Rubros.tdf_tts_id, Detalle_Rubros.tdf_rub_id, Detalle_Rubros.tdf_montosinimpuesto, Provision_Automatica.tpr_tti_id, Pais_Bean_Destino);
                Detalle_Rubros.tdf_ttm_id = Bean_Configuracion_Transaccion.intC6;
                Detalle_Rubros.tdf_montosinimpuesto = Rubro_Bean.rubroSubTot;
                Detalle_Rubros.tdf_impuesto = Rubro_Bean.rubroImpuesto;
                Detalle_Rubros.tdf_monto = Rubro_Bean.rubroTot;
                Detalle_Rubros.tdf_total_equivalente = Rubro_Bean.rubroTotD;
                Detalle_Rubros.tdf_tfa_id = 0;
                Detalle_Rubros.tdf_ttr_id = 5;
                Detalle_Rubros.tdf_comentarios = "";
                if (Detalle_Rubros.tdf_impuesto == 0)
                {
                    Provision_Automatica.tpr_noafecto += Detalle_Rubros.tdf_montosinimpuesto;
                }
                else
                {
                    Provision_Automatica.tpr_afecto += Detalle_Rubros.tdf_montosinimpuesto;
                }
                Provision_Automatica.tpr_valor += Detalle_Rubros.tdf_monto;
                Provision_Automatica.tpr_iva = Detalle_Rubros.tdf_impuesto;
                Provision_Automatica.tpr_valor_equivalente += Detalle_Rubros.tdf_total_equivalente;
                int transID = 105;
                int tttID = 15;
                Detalle_Rubros.cta_debe = (ArrayList)DB.getCtaContablebyRubro("debe", (int)Detalle_Rubros.tdf_rub_id, Provision_Automatica.tpr_pai_id, tttID, Provision_Automatica.tpr_tti_id, Provision_Automatica.tpr_mon_id, Provision_Automatica.tpr_imp_exp_id, tipo_cobro, Provision_Automatica.tpr_tcon_id, Detalle_Rubros.tdf_tts_id);
                if (Detalle_Rubros.cta_debe == null)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    string nombre_servicio = "";
                    string nombre_rubro = "";
                    RE_GenericBean Bean_Rubro_Aux = null;
                    nombre_servicio = Utility.TraducirServiciotoSTR(Detalle_Rubros.tdf_tts_id);
                    Bean_Rubro_Aux = DB.getRubro(Detalle_Rubros.tdf_rub_id);
                    nombre_rubro = Bean_Rubro_Aux.strC1;
                    Arr_Result.Add("No existe combinacion de cuentas contables para el Servicio " + nombre_servicio + " y Rubro " + nombre_rubro + " con ID (" + Detalle_Rubros.tdf_rub_id + "), en la Empresa " + Bean_Intercompany_Destino.strC5 + " para emitir Provision.");
                    return Arr_Result;
                }
                if (Detalle_Rubros.cta_debe.Count == 0)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    string nombre_servicio = "";
                    string nombre_rubro = "";
                    RE_GenericBean Bean_Rubro_Aux = null;
                    nombre_servicio = Utility.TraducirServiciotoSTR(Detalle_Rubros.tdf_tts_id);
                    Bean_Rubro_Aux = DB.getRubro(Detalle_Rubros.tdf_rub_id);
                    nombre_rubro = Bean_Rubro_Aux.strC1;
                    Arr_Result.Add("No existe combinacion de cuentas contables para el Servicio " + nombre_servicio + " y Rubro " + nombre_rubro + " con ID (" + Detalle_Rubros.tdf_rub_id + "), en la Empresa " + Bean_Intercompany_Destino.strC5 + " para emitir Provision.");
                    return Arr_Result;
                }
                if (Provision_Automatica.Arr_Detalle_Provision == null) Provision_Automatica.Arr_Detalle_Provision = new ArrayList();
                Provision_Automatica.Arr_Detalle_Provision.Add(Detalle_Rubros);

                Provision_Automatica.tpr_ttt_id = transID;
                int _matOpID_Destino = DB.getMatrizOperacionID(transID, Provision_Automatica.tpr_mon_id, Provision_Automatica.tpr_pai_id, Provision_Automatica.tpr_tcon_id);

                ArrayList ctas_cargo_Destino = (ArrayList)DB.getMatrizConfiguracion_ingreso_egreso(_matOpID_Destino, "Abono");
                if ((ctas_cargo_Destino == null) || (ctas_cargo_Destino.Count == 0))
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    Arr_Result.Add("No Existe configuracion contable para la Provision en Destino , por favor pongase en contacto con el Contador");
                    return Arr_Result;
                }
                Provision_Automatica.ctas_cargo = ctas_cargo_Destino;

                Arr_Result = new ArrayList();
                Arr_Result.Add("1");
                Arr_Result.Add(Provision_Automatica);
            }
            #endregion
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            Arr_Result = new ArrayList();
            Arr_Result.Add("0");
            Arr_Result.Add("Existio un error al tratar de Construir el documento de Pago al Intercompany Origen");
            Arr_Result.Add(null);
            return Arr_Result;
        }
        return Arr_Result;
        #endregion
    }
    public static ArrayList Construir_Nota_Debito_Intercompany_Operativo(RE_GenericBean Bean_Configuracion_Transaccion, Bean_Datos_BL Datos_BL, ArrayList Arr_Cargos_BL, int intercompanyORIGEN_ID, int intercompanyDESTINO_ID, string usuID)
    {
        #region Construir Nota de Debito Automatica Intercompany Operativo
        ArrayList Arr_Result = null;
        int tipo_cobro = 1;//prepaid, collect
        string sql = "";
        Rubros Rubro_Bean = null;
        try
        {
            RE_GenericBean Bean_Intercompany_Origen = (RE_GenericBean)DB.Get_Intercompany_Data(intercompanyORIGEN_ID);
            RE_GenericBean Bean_Intercompany_Destino = (RE_GenericBean)DB.Get_Intercompany_Data(intercompanyDESTINO_ID);
            PaisBean Pais_Bean_Origen = DB.getPais(Bean_Intercompany_Origen.intC3);
            PaisBean Pais_Bean_Destino = DB.getPais(Bean_Intercompany_Destino.intC3);
            Bean_Nota_Debito_Automatica Nota_Debito_Automatica = new Bean_Nota_Debito_Automatica();
            #region Obtener Datos del Cliente Final
            RE_GenericBean Bean_Cliente = null;
            Bean_Cliente = DB.getDataClient(double.Parse(Datos_BL.Cliente.ToString()));
            if (Bean_Cliente == null)
            {
                Arr_Result = new ArrayList();
                Arr_Result.Add("0");
                Arr_Result.Add("El cliente con ID.: " + Datos_BL.Cliente.ToString() + " es invalido ");
                return Arr_Result;
            }
            #endregion

            Nota_Debito_Automatica.tnd_id = 0;
            Nota_Debito_Automatica.tnd_nit = Bean_Cliente.strC1;
            Nota_Debito_Automatica.tnd_nombre = Bean_Cliente.strC2;
            Nota_Debito_Automatica.tnd_cli_id = Datos_BL.Cliente;
            Nota_Debito_Automatica.tnd_fecha_emision = DB.getDateTimeNow().Substring(0, 19);
            Nota_Debito_Automatica.tnd_total = 0;
            Nota_Debito_Automatica.tnd_pai_id = Pais_Bean_Destino.ID;
            Nota_Debito_Automatica.tnd_ted_id = 1;
            Nota_Debito_Automatica.tnd_observacion = "";
            Nota_Debito_Automatica.tnd_usu_id = usuID;
            Nota_Debito_Automatica.tnd_direccion = Bean_Cliente.strC4;
            Nota_Debito_Automatica.tnd_moneda = Bean_Configuracion_Transaccion.intC6;
            Nota_Debito_Automatica.tnd_hbl = Datos_BL.Hbl;
            Nota_Debito_Automatica.tnd_mbl = Datos_BL.Mbl;
            Nota_Debito_Automatica.tnd_contenedor = Datos_BL.Contenedor;
            Nota_Debito_Automatica.tnd_routing = Datos_BL.Routing;
            Nota_Debito_Automatica.tnd_referencia = "";
            Nota_Debito_Automatica.tnd_serie = Bean_Configuracion_Transaccion.strC7;
            Nota_Debito_Automatica.tnd_suc_id = Bean_Configuracion_Transaccion.intC7;
            Nota_Debito_Automatica.tnd_correlativo = 0;
            Nota_Debito_Automatica.tnd_tpi_id = 3;
            Nota_Debito_Automatica.tnd_tcon_id = Bean_Configuracion_Transaccion.intC5;
            Nota_Debito_Automatica.tnd_fecha_pago = DateTime.Now.ToString("yyyy-MM-dd");
            Nota_Debito_Automatica.tnd_sub_total = 0;
            Nota_Debito_Automatica.tnd_impuesto = 0;
            sql = "select nombre from navieras where activo=true and id_naviera=" + Datos_BL.Naviera;
            Nota_Debito_Automatica.tnd_naviera = DB.getName(sql);
            Nota_Debito_Automatica.tnd_vapor = Datos_BL.Vapor;
            sql = "select nombre_cliente from clientes where id_cliente=" + Datos_BL.Shipper;
            Nota_Debito_Automatica.tnd_shipper = DB.getName(sql);
            Nota_Debito_Automatica.tnd_ordenpo = "";
            sql = "select nombre_cliente from clientes where id_cliente=" + Datos_BL.Consignatario;
            Nota_Debito_Automatica.tnd_consignee = DB.getName(sql);
            sql = "select namees from commodities where commodityid =" + Datos_BL.Comodity;
            Nota_Debito_Automatica.tnd_comodity = DB.getName(sql);
            Nota_Debito_Automatica.tnd_paquetes = (string)DB.GetNombreTipoBulto(Datos_BL.Tipo_Paquete);
            Nota_Debito_Automatica.tnd_peso = Datos_BL.Peso;
            Nota_Debito_Automatica.tnd_dua_salida = "";
            Nota_Debito_Automatica.tnd_vendedor1 = Datos_BL.Vendedor1;
            Nota_Debito_Automatica.tnd_vendedor2 = Datos_BL.Vendedor2;
            Nota_Debito_Automatica.tnd_razon = Bean_Cliente.strC2;
            Nota_Debito_Automatica.tnd_id_shipper = Datos_BL.Shipper;
            Nota_Debito_Automatica.tnd_id_consignee = Datos_BL.Consignatario;
            Nota_Debito_Automatica.tnd_sub_total_eq = 0;
            Nota_Debito_Automatica.tnd_impuesto_eq = 0;
            Nota_Debito_Automatica.tnd_total_eq = 0;
            Nota_Debito_Automatica.tnd_tie_id = Datos_BL.Import_Export;
            Nota_Debito_Automatica.tnd_tie_id = 1;//Toda la Carga enviada a Colectar se vuelve Import
            Nota_Debito_Automatica.tnd_ttc_id = tipo_cobro;
            Nota_Debito_Automatica.tnd_reciboaduana = "";
            Nota_Debito_Automatica.tnd_volumen = Datos_BL.Volumen;
            Nota_Debito_Automatica.tnd_dua_ingreso = "";
            Nota_Debito_Automatica.tnd_cant_paquetes = Datos_BL.Paquetes;
            Nota_Debito_Automatica.tnd_agente_id = Datos_BL.Agente;
            sql = "select agente from agentes where activo=true and agente_id=" + Datos_BL.Agente;
            Nota_Debito_Automatica.tnd_agente = DB.getName(sql);
            Nota_Debito_Automatica.tnd_fiscal = true;
            Nota_Debito_Automatica.tnd_fecha_libro_compras = DateTime.Now.ToString("yyyy-MM-dd");
            Nota_Debito_Automatica.tnd_blid = Datos_BL.BLID;
            Nota_Debito_Automatica.tnd_tto_id = Datos_BL.ttoID;
            Nota_Debito_Automatica.tnd_bien_serv = 2;
            Nota_Debito_Automatica.tnd_esignature = "-";
            Nota_Debito_Automatica.tnd_fac_electronica = 0;
            Nota_Debito_Automatica.tnd_internal_reference = "0";
            Nota_Debito_Automatica.tnd_guid = "0";
            Nota_Debito_Automatica.tnd_correo_documento_electronico = "-";
            Nota_Debito_Automatica.tnd_referencia_correo = "-";
            Nota_Debito_Automatica.tnd_innerxml = "";
            Nota_Debito_Automatica.tnd_fecha_batch = DB.getDateTimeNow().Substring(0, 10);
            Nota_Debito_Automatica.tnd_tti_id = 1;//Excento
            Nota_Debito_Automatica.tnd_ttd_id = 3;
            #region Generar Detalle de Rubros
            Bean_Detalle_Rubros Detalle_Rubros = null;
            foreach (Bean_Cargos Cargo_BL in Arr_Cargos_BL)
            {
                if (Cargo_BL.Cargo_Rub_ID == 129)
                {
                    #region Profit - El Rubro Profit no genera cobro al cliente en destino porque la oficina origen solo se queda con el dinero del Profit y no con todos los cargos
                    if (Arr_Cargos_BL.Count == 1)
                    {
                        Arr_Result = new ArrayList();
                        Arr_Result.Add("17");
                        Arr_Result.Add("Cobro de Profit, el Profit no se cobra al cliente final en destino");
                        Arr_Result.Add(null);
                    }
                    #endregion
                }
                else
                {
                    Detalle_Rubros = new Bean_Detalle_Rubros();
                    Detalle_Rubros.tdf_cargo_id = Cargo_BL.Cargo_ID;
                    Detalle_Rubros.tdf_tts_id = Cargo_BL.Cargo_Servicio_ID;
                    Detalle_Rubros.tdf_tts_id = 14;//Terceros
                    Detalle_Rubros.tdf_rub_id = Cargo_BL.Cargo_Rub_ID;
                    Detalle_Rubros.tdf_montosinimpuesto = Cargo_BL.Cargo_Monto;

                    Detalle_Rubros.tdf_montosinimpuesto = Contabilizacion_Automatica_CN.Convertir_Divisas_Intercompanys(Nota_Debito_Automatica.tnd_pai_id, Cargo_BL.Cargo_Moneda_ID, Detalle_Rubros.tdf_montosinimpuesto, Bean_Intercompany_Destino.intC3, Bean_Configuracion_Transaccion.intC6);
                    Rubro_Bean = Contabilizacion_Automatica_CN.Calcular_Impuestos(Nota_Debito_Automatica.tnd_pai_id, Nota_Debito_Automatica.tnd_tcon_id, Nota_Debito_Automatica.tnd_moneda, Detalle_Rubros.tdf_tts_id, Detalle_Rubros.tdf_rub_id, Detalle_Rubros.tdf_montosinimpuesto, Nota_Debito_Automatica.tnd_tti_id, Pais_Bean_Destino);

                    Detalle_Rubros.tdf_ttm_id = Bean_Configuracion_Transaccion.intC6;
                    Detalle_Rubros.tdf_montosinimpuesto = Rubro_Bean.rubroSubTot;
                    Detalle_Rubros.tdf_impuesto = Rubro_Bean.rubroImpuesto;
                    Detalle_Rubros.tdf_monto = Rubro_Bean.rubroTot;
                    Detalle_Rubros.tdf_total_equivalente = Rubro_Bean.rubroTotD;

                    Nota_Debito_Automatica.tnd_sub_total += Detalle_Rubros.tdf_montosinimpuesto;
                    Nota_Debito_Automatica.tnd_impuesto += Detalle_Rubros.tdf_impuesto;
                    Nota_Debito_Automatica.tnd_total += Detalle_Rubros.tdf_monto;
                    Nota_Debito_Automatica.tnd_total_eq += Detalle_Rubros.tdf_total_equivalente;

                    Detalle_Rubros.tdf_tfa_id = 0;
                    Detalle_Rubros.tdf_ttr_id = 4;
                    #region Calcular Total Equivalente
                    if (Nota_Debito_Automatica.tnd_moneda == 8)
                    {
                        decimal Tipo_Cambio_Temporal = DB.getTipoCambioHoy(Nota_Debito_Automatica.tnd_pai_id);
                        Nota_Debito_Automatica.tnd_impuesto_eq += Math.Round(Detalle_Rubros.tdf_impuesto * (double)Tipo_Cambio_Temporal, 2);
                        Nota_Debito_Automatica.tnd_sub_total_eq += Math.Round(Detalle_Rubros.tdf_montosinimpuesto * (double)Tipo_Cambio_Temporal, 2);
                    }
                    else
                    {
                        decimal Tipo_Cambio_Temporal = DB.getTipoCambioHoy(Nota_Debito_Automatica.tnd_pai_id);
                        Nota_Debito_Automatica.tnd_impuesto_eq += Math.Round(Detalle_Rubros.tdf_impuesto / (double)Tipo_Cambio_Temporal, 2);
                        Nota_Debito_Automatica.tnd_sub_total_eq += Math.Round(Detalle_Rubros.tdf_montosinimpuesto / (double)Tipo_Cambio_Temporal, 2);
                    }
                    #endregion
                    Detalle_Rubros.tdf_comentarios = "";
                    int transID = 6;
                    int tttID = 6;
                    if (Nota_Debito_Automatica.tnd_tcon_id == 1)
                    {
                        tttID = 1;
                    }
                    else
                    {
                        tttID = 7;
                    }
                    Detalle_Rubros.cta_haber = (ArrayList)DB.getCtaContablebyRubro("haber", (int)Detalle_Rubros.tdf_rub_id, Nota_Debito_Automatica.tnd_pai_id, tttID, Nota_Debito_Automatica.tnd_tti_id, Nota_Debito_Automatica.tnd_moneda, Nota_Debito_Automatica.tnd_tie_id, tipo_cobro, Nota_Debito_Automatica.tnd_tcon_id, Detalle_Rubros.tdf_tts_id);
                    if (Detalle_Rubros.cta_haber == null)
                    {
                        Arr_Result = new ArrayList();
                        Arr_Result.Add("0");
                        string nombre_servicio = "";
                        string nombre_rubro = "";
                        RE_GenericBean Bean_Rubro_Aux = null;
                        nombre_servicio = Utility.TraducirServiciotoSTR(Detalle_Rubros.tdf_tts_id);
                        Bean_Rubro_Aux = DB.getRubro(Detalle_Rubros.tdf_rub_id);
                        nombre_rubro = Bean_Rubro_Aux.strC1;
                        Arr_Result.Add("No existe combinacion de cuentas contables para el Servicio " + nombre_servicio + " y Rubro " + nombre_rubro + " con ID (" + Detalle_Rubros.tdf_rub_id + "), en la Empresa " + Bean_Intercompany_Destino.strC5 + " para emitir Nota de Debito.");
                        return Arr_Result;
                    }
                    if (Detalle_Rubros.cta_haber.Count == 0)
                    {
                        Arr_Result = new ArrayList();
                        Arr_Result.Add("0");
                        string nombre_servicio = "";
                        string nombre_rubro = "";
                        RE_GenericBean Bean_Rubro_Aux = null;
                        nombre_servicio = Utility.TraducirServiciotoSTR(Detalle_Rubros.tdf_tts_id);
                        Bean_Rubro_Aux = DB.getRubro(Detalle_Rubros.tdf_rub_id);
                        nombre_rubro = Bean_Rubro_Aux.strC1;
                        Arr_Result.Add("No existe combinacion de cuentas contables para el Servicio " + nombre_servicio + " y Rubro " + nombre_rubro + " con ID (" + Detalle_Rubros.tdf_rub_id + "), en la Empresa " + Bean_Intercompany_Destino.strC5 + " para emitir Nota de Debito.");
                        return Arr_Result;
                    }
                    int _matOpID_Destino = DB.getMatrizOperacionID(tttID, Nota_Debito_Automatica.tnd_moneda, Nota_Debito_Automatica.tnd_pai_id, Nota_Debito_Automatica.tnd_tcon_id);
                    ArrayList ctas_cargo_Destino = (ArrayList)DB.getMatrizConfiguracion_ingreso_egreso(_matOpID_Destino, "Cargo");
                    if ((ctas_cargo_Destino == null) || (ctas_cargo_Destino.Count == 0))
                    {
                        Arr_Result = new ArrayList();
                        Arr_Result.Add("0");
                        Arr_Result.Add("No Existe configuracion contable para la Provision en Destino , por favor pongase en contacto con el Contador");
                        return Arr_Result;
                    }
                    Nota_Debito_Automatica.ctas_abono = ctas_cargo_Destino;
                    Nota_Debito_Automatica.tnd_ttt_id = transID;
                    if (Nota_Debito_Automatica.Arr_Detalle_Facturacion == null) Nota_Debito_Automatica.Arr_Detalle_Facturacion = new ArrayList();
                    Nota_Debito_Automatica.Arr_Detalle_Facturacion.Add(Detalle_Rubros);
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("1");
                    Arr_Result.Add(Nota_Debito_Automatica);
                }
            }
            #endregion  
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            Arr_Result = new ArrayList();
            Arr_Result.Add("0");
            Arr_Result.Add("Existio un error al tratar de Construir el documento de Cobro Automatico por Cuenta de Terceros");
            Arr_Result.Add(null);
            return Arr_Result;
        }
        return Arr_Result;
        #endregion
    }
    public static ArrayList Insertar_Cargos_Intercompany_Operativo(int _sisID, Bean_Datos_BL Datos_BL, ArrayList Arr_Cargos_BL, PaisBean Pais_Origen, int ttrID, int docID, int grupoID)
    {
        ArrayList Arr_result = null;
        int result = 0;
        #region Insertar Cargos por cuenta de Terceros generados en Intercompany Operativo
        string schema = "";
        string tipo_bl = "";
        #region Definir Schema
        if ((Datos_BL.ttoID == 17) || (Datos_BL.ttoID == 18))
        {
            schema = Pais_Origen.schema_apl;
        }
        else
        {
            schema = Pais_Origen.schema;
        }
        #endregion
        for (int i = 0; i < Arr_Cargos_BL.Count; i++)
        {
            Bean_Cargos Cargo_BL = (Bean_Cargos)Arr_Cargos_BL[i];
            if (Cargo_BL.Cargo_Rub_ID == 129)
            {
                #region Profit - El Rubro Profit no genera cobro al cliente en destino porque la oficina origen solo se queda con el dinero del Profit y no con todos los cargos
                #endregion
            }
            else
            {
                if (_sisID == 1)
                {
                    #region Cargos Sistema Maritimo
                    NpgsqlConnection con_Maritimo;
                    NpgsqlCommand com_Maritimo;
                    try
                    {
                        con_Maritimo = DB.OpenVentasConnection(schema);
                        com_Maritimo = new NpgsqlCommand();
                        com_Maritimo.Connection = con_Maritimo;
                        com_Maritimo.CommandType = CommandType.Text;
                        #region Definir Tipo BL
                        if ((Datos_BL.ttoID == 1) || (Datos_BL.ttoID == 17))
                        {
                            #region FCL
                            tipo_bl = "F";
                            #endregion
                        }
                        else if ((Datos_BL.ttoID == 2) || (Datos_BL.ttoID == 18))
                        {
                            #region LCL
                            tipo_bl = "L";
                            #endregion
                        }
                        #endregion
                        com_Maritimo.CommandText = "insert into cargos_bl (bl_id, tipo_bl, id_moneda, id_rubro, detalle, valor_prepaid, valor_collect, basis, rate, right_hand_note, valor_sobreventa, local, id_servicio, factura_id, tipo_conta, tipo_cobro, tipo_documento, activo, id_usuario, valor, sobreventa, tipo_cargo, inter_company, id_grupo, id_tipo_persona) ";
                        com_Maritimo.CommandText += " values (@bl_id, @tipo_bl, @id_moneda, @id_rubro, @detalle, @valor_prepaid, @valor_collect, @basis, @rate, @right_hand_note, @valor_sobreventa, @local, @id_servicio, @factura_id, @tipo_conta, @tipo_cobro, @tipo_documento, @activo, @id_usuario, @valor, @sobreventa, @tipo_cargo, @inter_company, @id_grupo, @id_tipo_persona)";
                        com_Maritimo.Parameters.Add("@bl_id", NpgsqlTypes.NpgsqlDbType.Bigint).Value = Cargo_BL.Cargo_BLID;
                        com_Maritimo.Parameters.Add("@tipo_bl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tipo_bl;
                        com_Maritimo.Parameters.Add("@id_moneda", NpgsqlTypes.NpgsqlDbType.Bigint).Value = Cargo_BL.Cargo_Moneda_Trafico_ID;
                        com_Maritimo.Parameters.Add("@id_rubro", NpgsqlTypes.NpgsqlDbType.Bigint).Value = Cargo_BL.Cargo_Rub_ID;
                        com_Maritimo.Parameters.Add("@detalle", NpgsqlTypes.NpgsqlDbType.Varchar).Value = "";
                        com_Maritimo.Parameters.Add("@valor_prepaid", NpgsqlTypes.NpgsqlDbType.Numeric).Value = 0;
                        com_Maritimo.Parameters.Add("@valor_collect", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Cargo_BL.Cargo_Monto;
                        com_Maritimo.Parameters.Add("@basis", NpgsqlTypes.NpgsqlDbType.Numeric).Value = 0;
                        com_Maritimo.Parameters.Add("@rate", NpgsqlTypes.NpgsqlDbType.Numeric).Value = 0;
                        com_Maritimo.Parameters.Add("@right_hand_note", NpgsqlTypes.NpgsqlDbType.Varchar).Value = "";
                        com_Maritimo.Parameters.Add("@valor_sobreventa", NpgsqlTypes.NpgsqlDbType.Real).Value = 0;
                        com_Maritimo.Parameters.Add("@local", NpgsqlTypes.NpgsqlDbType.Boolean).Value = false;
                        com_Maritimo.Parameters.Add("@id_servicio", NpgsqlTypes.NpgsqlDbType.Bigint).Value = 14;
                        com_Maritimo.Parameters.Add("@factura_id", NpgsqlTypes.NpgsqlDbType.Bigint).Value = docID;
                        com_Maritimo.Parameters.Add("@tipo_conta", NpgsqlTypes.NpgsqlDbType.Bigint).Value = 0;
                        com_Maritimo.Parameters.Add("@tipo_cobro", NpgsqlTypes.NpgsqlDbType.Integer).Value = 0;//Internacional
                        com_Maritimo.Parameters.Add("@tipo_documento", NpgsqlTypes.NpgsqlDbType.Integer).Value = ttrID;
                        com_Maritimo.Parameters.Add("@activo", NpgsqlTypes.NpgsqlDbType.Boolean).Value = true;
                        com_Maritimo.Parameters.Add("@id_usuario", NpgsqlTypes.NpgsqlDbType.Integer).Value = 0;
                        com_Maritimo.Parameters.Add("@valor", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Cargo_BL.Cargo_Monto;
                        com_Maritimo.Parameters.Add("@sobreventa", NpgsqlTypes.NpgsqlDbType.Numeric).Value = 0;
                        com_Maritimo.Parameters.Add("@tipo_cargo", NpgsqlTypes.NpgsqlDbType.Integer).Value = 2;//Normal 1, Intercompany 2
                        com_Maritimo.Parameters.Add("@inter_company", NpgsqlTypes.NpgsqlDbType.Integer).Value = Datos_BL.Cliente;
                        com_Maritimo.Parameters.Add("@id_grupo", NpgsqlTypes.NpgsqlDbType.Integer).Value = grupoID;
                        com_Maritimo.Parameters.Add("@id_tipo_persona", NpgsqlTypes.NpgsqlDbType.Integer).Value = 4;//Cliente
                        com_Maritimo.CommandText += " returning cargo_id";
                        result = int.Parse(com_Maritimo.ExecuteScalar().ToString());
                        Cargo_BL.Cargo_Terceros_ID = result;
                        Arr_Cargos_BL[i] = Cargo_BL;
                        DB.CloseObj_insert(com_Maritimo, con_Maritimo);
                    }
                    catch (Exception e)
                    {
                        log4net ErrLog = new log4net();
                        ErrLog.ErrorLog(e.Message);
                        Arr_result = new ArrayList();
                        Arr_result.Add("0");
                        Arr_result.Add("Existio un error al tratar Insertar los Cargos en Trafico");
                        return Arr_result;
                    }
                    #endregion
                }
                else if (_sisID == 2)
                {
                    #region Cargos Sistema Aereo
                    MySqlConnection con_Aereo;
                    MySqlCommand com_Aereo;
                    try
                    {
                        con_Aereo = DB.OpenAereoConnection();
                        com_Aereo = new MySqlCommand();
                        com_Aereo.Connection = con_Aereo;
                        com_Aereo.CommandType = CommandType.Text;
                        com_Aereo.CommandText = "insert into ChargeItems (AWBID,CurrencyID,ItemID,Value,Local, AgentTyp, DocTyp, ItemName, CreatedDate, CreatedTime, UserID, Expired, OverSold, ServiceID, ServiceName, PrepaidCollect, InvoiceID, AccountType, DocType, CalcInBL, InterChargeType, InterCompanyID, InterGroupID, InterProviderType) ";
                        com_Aereo.CommandText += "values (@AWBID,@CurrencyID,@ItemID,@Value,@Local, @AgentTyp, @DocTyp, @ItemName, CURDATE(), @CreatedTime, @UserID, @Expired, @OverSold, @ServiceID, @ServiceName, @PrepaidCollect, @InvoiceID, @AccountType, @DocType, @CalcInBL, @InterChargeType, @InterCompanyID, @InterGroupID, @InterProviderType)";
                        com_Aereo.Parameters.Add("@AWBID", MySqlDbType.Double).Value = Datos_BL.BLID;
                        com_Aereo.Parameters.Add("@CurrencyID", MySqlDbType.VarChar).Value = Cargo_BL.Cargo_Moneda_Simbolo;
                        com_Aereo.Parameters.Add("@ItemID", MySqlDbType.Double).Value = Cargo_BL.Cargo_Rub_ID;
                        com_Aereo.Parameters.Add("@Value", MySqlDbType.Double).Value = Cargo_BL.Cargo_Monto;
                        com_Aereo.Parameters.Add("@Local", MySqlDbType.Double).Value = 0;//Internacional
                        com_Aereo.Parameters.Add("@AgentTyp", MySqlDbType.Double).Value = 2;//0=Carrier, 1=Agent, 2=Otros
                        if (Datos_BL.ttoID == 3)
                        {
                            #region Importacion
                            com_Aereo.Parameters.Add("@DocTyp", MySqlDbType.Double).Value = 1;
                            #endregion
                        }
                        else if (Datos_BL.ttoID == 4)
                        {
                            #region Exportacion
                            com_Aereo.Parameters.Add("@DocTyp", MySqlDbType.Double).Value = 0;
                            #endregion
                        }
                        RE_GenericBean Bean_Rubro = DB.getRubro(Cargo_BL.Cargo_Rub_ID);
                        com_Aereo.Parameters.Add("@ItemName", MySqlDbType.VarChar).Value = Bean_Rubro.strC1.ToUpper();
                        com_Aereo.Parameters.Add("@CreatedTime", MySqlDbType.Double).Value = double.Parse(DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString());
                        com_Aereo.Parameters.Add("@UserID", MySqlDbType.Double).Value = 0;
                        com_Aereo.Parameters.Add("@Expired", MySqlDbType.Double).Value = 0;
                        com_Aereo.Parameters.Add("@OverSold", MySqlDbType.Double).Value = 0;
                        com_Aereo.Parameters.Add("@ServiceID", MySqlDbType.Double).Value = 14;
                        com_Aereo.Parameters.Add("@ServiceName", MySqlDbType.VarChar).Value = Utility.TraducirServiciotoSTR(14);//Terceros  
                        com_Aereo.Parameters.Add("@PrepaidCollect", MySqlDbType.Double).Value = 1;//Collect
                        com_Aereo.Parameters.Add("@InvoiceID", MySqlDbType.Double).Value = docID;
                        com_Aereo.Parameters.Add("@AccountType", MySqlDbType.Int16).Value = 0;//No se usa, era par indicar fiscal/financiero
                        com_Aereo.Parameters.Add("@DocType", MySqlDbType.Double).Value = ttrID;
                        com_Aereo.Parameters.Add("@CalcInBL", MySqlDbType.Double).Value = 0;//Esta bandera es para indicar si los valores deben salir impresos en la CP/Guia, 1=Si.              0=No Sale, Poner que no salga Aereo y Terrestre
                        com_Aereo.Parameters.Add("@InterChargeType", MySqlDbType.Double).Value = 2;//Normal 1, Intercompany 2
                        com_Aereo.Parameters.Add("@InterCompanyID", MySqlDbType.Double).Value = Convert.ToDouble(Datos_BL.Cliente);
                        com_Aereo.Parameters.Add("@InterGroupID", MySqlDbType.Double).Value = grupoID;
                        com_Aereo.Parameters.Add("@InterProviderType", MySqlDbType.Double).Value = 4;//Cliente
                        result = com_Aereo.ExecuteNonQuery();
                        com_Aereo.Parameters.Clear();
                        com_Aereo.CommandText = "select last_insert_id();";
                        result = int.Parse(com_Aereo.ExecuteScalar().ToString());
                        com_Aereo.Parameters.Clear();
                        Cargo_BL.Cargo_Terceros_ID = result;
                        Arr_Cargos_BL[i] = Cargo_BL;
                        DB.CloseMySQLObj_insert(com_Aereo, con_Aereo);
                    }
                    catch (Exception e)
                    {
                        log4net ErrLog = new log4net();
                        ErrLog.ErrorLog(e.Message);
                        Arr_result = new ArrayList();
                        Arr_result.Add("0");
                        Arr_result.Add("Existio un error al tratar Insertar los Cargos en Trafico");
                        return Arr_result;
                    }
                    #endregion
                }
                else if (_sisID == 3)
                {
                    #region Cargos Sistema Terrestre
                    MySqlConnection con_Terrestre;
                    MySqlCommand com_Terrestre;
                    try
                    {
                        con_Terrestre = DB.OpenTerrestreConnection();
                        com_Terrestre = new MySqlCommand();
                        com_Terrestre.Connection = con_Terrestre;
                        com_Terrestre.CommandType = CommandType.Text;
                        com_Terrestre.CommandText = "insert into ChargeItems (SBLID, Currency, ItemID, Value, Local, AgentTyp, ItemName, CreatedDate, CreatedTime, UserID, Expired, PrepaidCollect, OverSold, ServiceID, ServiceName, InvoiceID, AccountType, DocType, CalcInBL, InterChargeType, InterCompanyID, InterGroupID, InterProviderType) ";
                        com_Terrestre.CommandText += "values (@SBLID, @Currency, @ItemID, @Value, @Local, @AgentTyp, @ItemName, CURDATE(), @CreatedTime, @UserID, @Expired, @PrepaidCollect, @OverSold, @ServiceID, @ServiceName, @InvoiceID, @AccountType, @DocType, @CalcInBL, @InterChargeType, @InterCompanyID, @InterGroupID, @InterProviderType)";
                        com_Terrestre.Parameters.Add("@SBLID", MySqlDbType.Double).Value = Datos_BL.BLID;
                        com_Terrestre.Parameters.Add("@Currency", MySqlDbType.VarChar).Value = Cargo_BL.Cargo_Moneda_Simbolo;
                        com_Terrestre.Parameters.Add("@ItemID", MySqlDbType.Double).Value = Cargo_BL.Cargo_Rub_ID;
                        com_Terrestre.Parameters.Add("@Value", MySqlDbType.Double).Value = Cargo_BL.Cargo_Monto;
                        com_Terrestre.Parameters.Add("@Local", MySqlDbType.Double).Value = 0;//Internacional
                        com_Terrestre.Parameters.Add("@AgentTyp", MySqlDbType.Double).Value = 2;//0=Carrier, 1=Agent, 2=Otros
                        RE_GenericBean Bean_Rubro = DB.getRubro(Cargo_BL.Cargo_Rub_ID);
                        com_Terrestre.Parameters.Add("@ItemName", MySqlDbType.VarChar).Value = Bean_Rubro.strC1.ToUpper();
                        com_Terrestre.Parameters.Add("@CreatedTime", MySqlDbType.Double).Value = double.Parse(DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString());
                        com_Terrestre.Parameters.Add("@UserID", MySqlDbType.Double).Value = 0;
                        com_Terrestre.Parameters.Add("@Expired", MySqlDbType.Double).Value = 0;
                        com_Terrestre.Parameters.Add("@PrepaidCollect", MySqlDbType.Double).Value = 1;//Collect
                        com_Terrestre.Parameters.Add("@OverSold", MySqlDbType.Double).Value = 0;
                        com_Terrestre.Parameters.Add("@ServiceID", MySqlDbType.Double).Value = 14;
                        com_Terrestre.Parameters.Add("@ServiceName", MySqlDbType.VarChar).Value = Utility.TraducirServiciotoSTR(14);
                        com_Terrestre.Parameters.Add("@InvoiceID", MySqlDbType.Double).Value = docID;
                        com_Terrestre.Parameters.Add("@AccountType", MySqlDbType.Int16).Value = 0;//No se usa, era par indicar fiscal/financiero
                        com_Terrestre.Parameters.Add("@DocType", MySqlDbType.Double).Value = ttrID;
                        com_Terrestre.Parameters.Add("@CalcInBL", MySqlDbType.Double).Value = 0;//Esta bandera es para indicar si los valores deben salir impresos en la CP/Guia, 1=Si 0=No
                        com_Terrestre.Parameters.Add("@InterChargeType", MySqlDbType.Double).Value = 2;//Normal 1, Intercompany 2
                        com_Terrestre.Parameters.Add("@InterCompanyID", MySqlDbType.Double).Value = Convert.ToDouble(Datos_BL.Cliente);
                        com_Terrestre.Parameters.Add("@InterGroupID", MySqlDbType.Double).Value = grupoID;
                        com_Terrestre.Parameters.Add("@InterProviderType", MySqlDbType.Double).Value = 4;//Cliente
                        result = com_Terrestre.ExecuteNonQuery();
                        com_Terrestre.Parameters.Clear();
                        com_Terrestre.CommandText = "select last_insert_id();";
                        result = int.Parse(com_Terrestre.ExecuteScalar().ToString());
                        com_Terrestre.Parameters.Clear();
                        Cargo_BL.Cargo_Terceros_ID = result;
                        Arr_Cargos_BL[i] = Cargo_BL;
                        DB.CloseMySQLObj_insert(com_Terrestre, con_Terrestre);
                    }
                    catch (Exception e)
                    {
                        log4net ErrLog = new log4net();
                        ErrLog.ErrorLog(e.Message);
                        Arr_result = new ArrayList();
                        Arr_result.Add("0");
                        Arr_result.Add("Existio un error al tratar Insertar los Cargos en Trafico");
                        return Arr_result;
                    }
                    #endregion
                }
            }
        }
        #endregion
        Arr_result = new ArrayList();
        Arr_result.Add("1");
        Arr_result.Add(Arr_Cargos_BL);
        return Arr_result;
    }
    public static ArrayList Amarrar_Cargos_Costos_Intercompany_Operativo(NpgsqlConnection conn, NpgsqlCommand comm, NpgsqlTransaction Transaction, ArrayList Arr_Cargos_BL, ArrayList Arr_Rubros, string Tipo)
    {
        #region Amarrar Cargos Intercompany Operativo con su respectivo Detalle Facturacion
        ArrayList Arr_result = null;
        int result = 0;
        try
        {
            for (int i = 0; i < Arr_Cargos_BL.Count; i++)
            {
                //Bean_Detalle_Rubros Rubros = (Bean_Detalle_Rubros)Arr_Rubros[i];
                Bean_Cargos Cargo_BL = (Bean_Cargos)Arr_Cargos_BL[i];
                if ((Cargo_BL.Cargo_Rub_ID == 129) && (Tipo == "CARGO"))
                {
                    #region Profit - El Rubro Profit no genera cobro al cliente en destino porque la oficina origen solo se queda con el dinero del Profit y no con todos los cargos
                    #endregion
                }
                else
                {
                    Bean_Detalle_Rubros Rubros = (Bean_Detalle_Rubros)Arr_Rubros[i];
                    if (Tipo == "CARGO")
                    {
                        comm.CommandText = "update tbl_detalle_facturacion set tdf_cargo_id=" + Cargo_BL.Cargo_Terceros_ID + " where tdf_id=" + Rubros.tdf_id + " ";
                    }
                    else if (Tipo == "COSTO")
                    {
                        comm.CommandText = "update tbl_detalle_facturacion set tdf_cargo_id=" + Cargo_BL.Costo_Terceros_ID + " where tdf_id=" + Rubros.tdf_id + " ";
                    }
                    result = comm.ExecuteNonQuery();
                    comm.CommandText = "";
                    comm.Parameters.Clear();
                }
            }
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            Arr_result = new ArrayList();
            Arr_result.Add("0");
            Arr_result.Add("Existio un error al tratar de amarar los Cargos del Intercompany Operativo");
            return Arr_result;
        }
        Arr_result = new ArrayList();
        Arr_result.Add("1");
        Arr_result.Add("Cargos Amarrados Exitosamente");
        return Arr_result;
        #endregion
    }
    public static ArrayList Insertar_Costos_Intercompany_Operativo(int _sisID, Bean_Datos_BL Datos_BL, ArrayList Arr_Cargos_BL, PaisBean Pais_Origen, PaisBean Pais_Destino, int ttrID, int docID, int grupoID, int intercompanyORIGEN_ID, string usuID)
    {
        ArrayList Arr_result = null;
        int result = 0;
        #region Insertar Costos generados en Intercompany Operativo
        string schema = "";
        string tipo_bl = "";
        #region Definir Schema
        if ((Datos_BL.ttoID == 17) || (Datos_BL.ttoID == 18))
        {
            schema = Pais_Origen.schema_apl;
        }
        else
        {
            schema = Pais_Origen.schema;
        }
        #endregion
        for (int i = 0; i < Arr_Cargos_BL.Count; i++)
        {
            Bean_Cargos Cargo_BL = (Bean_Cargos)Arr_Cargos_BL[i];
            if (_sisID == 1)
            {
                #region Costos Sistema Maritimo
                NpgsqlConnection con_Maritimo;
                NpgsqlCommand com_Maritimo;
                try
                {
                    con_Maritimo = DB.OpenVentasConnection(schema);
                    com_Maritimo = new NpgsqlCommand();
                    com_Maritimo.Connection = con_Maritimo;
                    com_Maritimo.CommandType = CommandType.Text;
                    #region Definir Tipo BL
                    if ((Datos_BL.ttoID == 1) || (Datos_BL.ttoID == 17))
                    {
                        #region FCL
                        tipo_bl = "F";
                        #endregion
                    }
                    else if ((Datos_BL.ttoID == 2) || (Datos_BL.ttoID == 18))
                    {
                        #region LCL
                        tipo_bl = "L";
                        #endregion
                    }
                    #endregion
                    com_Maritimo.CommandText = "insert into costos_master (id_tipo_prorrateo, id_tipo_proveedor, id_proveedor, tipo_bl, id_rubro, id_moneda, costo, bl_id, activo, id_provision, orden_compra, moneda, id_servicio, tipo_conta, referencia, pago_terceros, billingdate, user_id, user_ip, es_afecto, id_grupo) ";
                    com_Maritimo.CommandText += " values (@id_tipo_prorrateo, @id_tipo_proveedor, @id_proveedor, @tipo_bl, @id_rubro, @id_moneda, @costo, @bl_id, @activo, @id_provision, @orden_compra, @moneda, @id_servicio, @tipo_conta, @referencia, @pago_terceros, now(), @user_id, @user_ip, @es_afecto, @id_grupo)";
                    com_Maritimo.Parameters.Add("@id_tipo_prorrateo", NpgsqlTypes.NpgsqlDbType.Bigint).Value = 4;//Directo
                    com_Maritimo.Parameters.Add("@id_tipo_proveedor", NpgsqlTypes.NpgsqlDbType.Bigint).Value = 5;//Intercompany
                    com_Maritimo.Parameters.Add("@id_proveedor", NpgsqlTypes.NpgsqlDbType.Bigint).Value = intercompanyORIGEN_ID;
                    com_Maritimo.Parameters.Add("@tipo_bl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tipo_bl;
                    com_Maritimo.Parameters.Add("@id_rubro", NpgsqlTypes.NpgsqlDbType.Bigint).Value = Cargo_BL.Cargo_Rub_ID;
                    com_Maritimo.Parameters.Add("@id_moneda", NpgsqlTypes.NpgsqlDbType.Bigint).Value = Cargo_BL.Cargo_Moneda_Trafico_ID;
                    com_Maritimo.Parameters.Add("@costo", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Cargo_BL.Cargo_Monto;
                    com_Maritimo.Parameters.Add("@bl_id", NpgsqlTypes.NpgsqlDbType.Bigint).Value = Datos_BL.BLID;
                    com_Maritimo.Parameters.Add("@activo", NpgsqlTypes.NpgsqlDbType.Boolean).Value = true;
                    com_Maritimo.Parameters.Add("@id_provision", NpgsqlTypes.NpgsqlDbType.Integer).Value = docID;
                    com_Maritimo.Parameters.Add("@orden_compra", NpgsqlTypes.NpgsqlDbType.Varchar).Value = "";
                    com_Maritimo.Parameters.Add("@moneda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = DB.Get_Simbolo_Moneda_Master(Cargo_BL.Cargo_Moneda_Trafico_ID);
                    com_Maritimo.Parameters.Add("@id_servicio", NpgsqlTypes.NpgsqlDbType.Bigint).Value = 14;
                    com_Maritimo.Parameters.Add("@tipo_conta", NpgsqlTypes.NpgsqlDbType.Bigint).Value = 0;
                    com_Maritimo.Parameters.Add("@referencia", NpgsqlTypes.NpgsqlDbType.Varchar).Value = "";
                    com_Maritimo.Parameters.Add("@pago_terceros", NpgsqlTypes.NpgsqlDbType.Integer).Value = 0;
                    com_Maritimo.Parameters.Add("@user_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = usuID;
                    com_Maritimo.Parameters.Add("@user_ip", NpgsqlTypes.NpgsqlDbType.Varchar).Value = "10.10.1.7";
                    com_Maritimo.Parameters.Add("@es_afecto", NpgsqlTypes.NpgsqlDbType.Smallint).Value = 1;
                    com_Maritimo.Parameters.Add("@id_grupo", NpgsqlTypes.NpgsqlDbType.Integer).Value = grupoID;
                    com_Maritimo.CommandText += " returning id_costo_master";
                    result = int.Parse(com_Maritimo.ExecuteScalar().ToString());
                    Cargo_BL.Costo_Terceros_ID = result;
                    Arr_Cargos_BL[i] = Cargo_BL;
                    DB.CloseObj_insert(com_Maritimo, con_Maritimo);
                }
                catch (Exception e)
                {
                    log4net ErrLog = new log4net();
                    ErrLog.ErrorLog(e.Message);
                    Arr_result = new ArrayList();
                    Arr_result.Add("0");
                    Arr_result.Add("Existio un error al tratar Insertar los Costos en Trafico");
                    return Arr_result;
                }
                #endregion
            }
            else if (_sisID == 2)
            {
                #region Costos Sistema Aereo
                MySqlConnection con_Aereo;
                MySqlCommand com_Aereo;
                try
                {
                    con_Aereo = DB.OpenAereoConnection();
                    com_Aereo = new MySqlCommand();
                    com_Aereo.Connection = con_Aereo;
                    com_Aereo.CommandType = CommandType.Text;
                    com_Aereo.CommandText = "insert into Costs (Distribution, SupplierType, SupplierID, SupplierName, ItemID, ItemName, Currency, Cost, BLID, Expired, CreatedDate, CreatedTime, UserID, DocType, ProvisionID, PurchaseOrder, ServiceID, ServiceName, AccountType, Reference, Countries, ThirdParties, BillingDate, IsAffected, SupplierNeutral, InterGroupID) ";
                    com_Aereo.CommandText += "values (@Distribution, @SupplierType, @SupplierID, @SupplierName, @ItemID, @ItemName, @Currency, @Cost, @BLID, @Expired, CURDATE(), @CreatedTime, @UserID, @DocType, @ProvisionID, @PurchaseOrder, @ServiceID, @ServiceName, @AccountType, @Reference, @Countries, @ThirdParties, CURDATE(), @IsAffected, @SupplierNeutral, @InterGroupID) ";
                    com_Aereo.Parameters.Add("@Distribution", MySqlDbType.Double).Value = 4;//Directo
                    com_Aereo.Parameters.Add("@SupplierType", MySqlDbType.Double).Value = 5;//Intercompany
                    com_Aereo.Parameters.Add("@SupplierID", MySqlDbType.Double).Value = intercompanyORIGEN_ID;
                    RE_GenericBean Bean_Intercompany_Origen = (RE_GenericBean)DB.Get_Intercompany_Data(intercompanyORIGEN_ID);
                    com_Aereo.Parameters.Add("@SupplierName", MySqlDbType.VarChar).Value = Bean_Intercompany_Origen.strC1;
                    com_Aereo.Parameters.Add("@ItemID", MySqlDbType.Double).Value = Cargo_BL.Cargo_Rub_ID;
                    RE_GenericBean Bean_Rubro = DB.getRubro(Cargo_BL.Cargo_Rub_ID);
                    com_Aereo.Parameters.Add("@ItemName", MySqlDbType.VarChar).Value = Bean_Rubro.strC1.ToUpper();
                    com_Aereo.Parameters.Add("@Currency", MySqlDbType.VarChar).Value = Cargo_BL.Cargo_Moneda_Simbolo;
                    com_Aereo.Parameters.Add("@Cost", MySqlDbType.Double).Value = Cargo_BL.Cargo_Monto;
                    com_Aereo.Parameters.Add("@BLID", MySqlDbType.Double).Value = Datos_BL.BLID;
                    com_Aereo.Parameters.Add("@Expired", MySqlDbType.Double).Value = 0;
                    com_Aereo.Parameters.Add("@CreatedTime", MySqlDbType.Double).Value = double.Parse(DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString());
                    com_Aereo.Parameters.Add("@UserID", MySqlDbType.Double).Value = 0;
                    if (Datos_BL.ttoID == 3)
                    {
                        #region Importacion
                        com_Aereo.Parameters.Add("@DocType", MySqlDbType.Double).Value = 2;
                        #endregion
                    }
                    else if (Datos_BL.ttoID == 4)
                    {
                        #region Exportacion
                        com_Aereo.Parameters.Add("DocType", MySqlDbType.Double).Value = 1;
                        #endregion
                    }
                    com_Aereo.Parameters.Add("ProvisionID", MySqlDbType.Double).Value = docID;
                    com_Aereo.Parameters.Add("PurchaseOrder", MySqlDbType.VarChar).Value = "";
                    com_Aereo.Parameters.Add("ServiceID", MySqlDbType.Double).Value = 14;
                    com_Aereo.Parameters.Add("ServiceName", MySqlDbType.VarChar).Value = Utility.TraducirServiciotoSTR(14);
                    com_Aereo.Parameters.Add("AccountType", MySqlDbType.Double).Value = 0;//No se usa, era par indicar fiscal/financiero
                    com_Aereo.Parameters.Add("Reference", MySqlDbType.VarChar).Value = "";
                    com_Aereo.Parameters.Add("Countries", MySqlDbType.VarChar).Value = Pais_Destino.ISO;
                    com_Aereo.Parameters.Add("ThirdParties", MySqlDbType.Double).Value = 0;
                    com_Aereo.Parameters.Add("IsAffected", MySqlDbType.Double).Value = 0;
                    com_Aereo.Parameters.Add("SupplierNeutral", MySqlDbType.Double).Value = 0;//0=No Neutral, 1=Neutral
                    com_Aereo.Parameters.Add("InterGroupID", MySqlDbType.Double).Value = grupoID;
                    result = com_Aereo.ExecuteNonQuery();
                    com_Aereo.Parameters.Clear();
                    com_Aereo.CommandText = "select last_insert_id();";
                    result = int.Parse(com_Aereo.ExecuteScalar().ToString());
                    com_Aereo.Parameters.Clear();
                    Cargo_BL.Costo_Terceros_ID = result;
                    Arr_Cargos_BL[i] = Cargo_BL;
                    DB.CloseMySQLObj_insert(com_Aereo, con_Aereo);
                }
                catch (Exception e)
                {
                    log4net ErrLog = new log4net();
                    ErrLog.ErrorLog(e.Message);
                    Arr_result = new ArrayList();
                    Arr_result.Add("0");
                    Arr_result.Add("Existio un error al tratar Insertar los Costos en Trafico");
                    return Arr_result;
                }
                #endregion
            }
            else if (_sisID == 3)
            {
                #region Costos Sistema Terrestre
                MySqlConnection con_Terrestre;
                MySqlCommand com_Terrestre;
                try
                {
                    con_Terrestre = DB.OpenTerrestreConnection();
                    com_Terrestre = new MySqlCommand();
                    com_Terrestre.Connection = con_Terrestre;
                    com_Terrestre.CommandType = CommandType.Text;
                    com_Terrestre.CommandText = "insert into Costs (Distribution, SupplierType, SupplierID, SupplierName, ItemID, ItemName, Currency, Cost, BLID, Expired, CreatedDate, CreatedTime, UserID, ProvisionID, PurchaseOrder, ServiceID, ServiceName, AccountType, Reference, Countries, ThirdParties, BillingDate, IsAffected, SupplierNeutral, InterGroupID) ";
                    com_Terrestre.CommandText += "values (@Distribution, @SupplierType, @SupplierID, @SupplierName, @ItemID, @ItemName, @Currency, @Cost, @BLID, @Expired, CURDATE(), @CreatedTime, @UserID, @ProvisionID, @PurchaseOrder, @ServiceID, @ServiceName, @AccountType, @Reference, @Countries, @ThirdParties, now(), @IsAffected, @SupplierNeutral, @InterGroupID)";
                    com_Terrestre.Parameters.Add("@Distribution", MySqlDbType.Double).Value = 4;//Directo
                    com_Terrestre.Parameters.Add("@SupplierType", MySqlDbType.Double).Value = 5;//Intercompany
                    com_Terrestre.Parameters.Add("@SupplierID", MySqlDbType.Double).Value = intercompanyORIGEN_ID;
                    RE_GenericBean Bean_Intercompany_Destino = (RE_GenericBean)DB.Get_Intercompany_Data(intercompanyORIGEN_ID);
                    com_Terrestre.Parameters.Add("@SupplierName", MySqlDbType.VarChar).Value = Bean_Intercompany_Destino.strC1;
                    com_Terrestre.Parameters.Add("@ItemID", MySqlDbType.Double).Value = Cargo_BL.Cargo_Rub_ID;
                    RE_GenericBean Bean_Rubro = DB.getRubro(Cargo_BL.Cargo_Rub_ID);
                    com_Terrestre.Parameters.Add("@ItemName", MySqlDbType.VarChar).Value = Bean_Rubro.strC1.ToUpper();
                    com_Terrestre.Parameters.Add("@Currency", MySqlDbType.VarChar).Value = Cargo_BL.Cargo_Moneda_Simbolo;
                    com_Terrestre.Parameters.Add("@Cost", MySqlDbType.Double).Value = Cargo_BL.Cargo_Monto;
                    com_Terrestre.Parameters.Add("@BLID", MySqlDbType.Double).Value = Datos_BL.BLID;
                    com_Terrestre.Parameters.Add("@Expired", MySqlDbType.Double).Value = 0;
                    com_Terrestre.Parameters.Add("@CreatedTime", MySqlDbType.Double).Value = double.Parse(DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString());
                    com_Terrestre.Parameters.Add("@UserID", MySqlDbType.Double).Value = 0;
                    com_Terrestre.Parameters.Add("@ProvisionID", MySqlDbType.Double).Value = docID;
                    com_Terrestre.Parameters.Add("@PurchaseOrder", MySqlDbType.VarChar).Value = "";
                    com_Terrestre.Parameters.Add("@ServiceID", MySqlDbType.Double).Value = 14;
                    com_Terrestre.Parameters.Add("@ServiceName", MySqlDbType.VarChar).Value = Utility.TraducirServiciotoSTR(14);
                    com_Terrestre.Parameters.Add("@AccountType", MySqlDbType.Int32).Value = 0;//No se usa, era par indicar fiscal/financiero
                    com_Terrestre.Parameters.Add("@Reference", MySqlDbType.VarChar).Value = "";
                    com_Terrestre.Parameters.Add("@Countries", MySqlDbType.VarChar).Value = Pais_Destino.ISO;
                    com_Terrestre.Parameters.Add("@ThirdParties", MySqlDbType.Int32).Value = 0;
                    com_Terrestre.Parameters.Add("@IsAffected", MySqlDbType.Int32).Value = 0;
                    com_Terrestre.Parameters.Add("@SupplierNeutral", MySqlDbType.Double).Value = 0;//0=No Neutral, 1=Neutral
                    com_Terrestre.Parameters.Add("@InterGroupID", MySqlDbType.Double).Value = grupoID;
                    result = com_Terrestre.ExecuteNonQuery();
                    com_Terrestre.Parameters.Clear();
                    com_Terrestre.CommandText = "select last_insert_id();";
                    result = int.Parse(com_Terrestre.ExecuteScalar().ToString());
                    com_Terrestre.Parameters.Clear();
                    Cargo_BL.Costo_Terceros_ID = result;
                    Arr_Cargos_BL[i] = Cargo_BL;
                    DB.CloseMySQLObj_insert(com_Terrestre, con_Terrestre);
                }
                catch (Exception e)
                {
                    log4net ErrLog = new log4net();
                    ErrLog.ErrorLog(e.Message);
                    Arr_result = new ArrayList();
                    Arr_result.Add("0");
                    Arr_result.Add("Existio un error al tratar Insertar los Costos en Trafico");
                    return Arr_result;
                }
                #endregion
            }
        }
        #endregion
        Arr_result = new ArrayList();
        Arr_result.Add("1");
        Arr_result.Add(Arr_Cargos_BL);
        return Arr_result;
    }
    public static ArrayList Asignar_Grupo_Cargos_Intercompany(Bean_Datos_BL Datos_BL, ArrayList Arr_Cargos_BL, PaisBean Pais_Origen, int _sisID, int grupoID)
    {
        ArrayList Arr_result = null;
        int result = 0;
        #region Asigar Grupo a Cargos de Intercompany Operativo
        string schema = "";
        string tipo_bl = "";
        #region Definir Schema
        if ((Datos_BL.ttoID == 17) || (Datos_BL.ttoID == 18))
        {
            schema = Pais_Origen.schema_apl;
        }
        else
        {
            schema = Pais_Origen.schema;
        }
        #endregion
        for (int i = 0; i < Arr_Cargos_BL.Count; i++)
        {
            Bean_Cargos Cargo_BL = (Bean_Cargos)Arr_Cargos_BL[i];
            if (_sisID == 1)
            {
                #region Cargos Sistema Maritimo
                NpgsqlConnection con_Maritimo;
                NpgsqlCommand com_Maritimo;
                try
                {
                    con_Maritimo = DB.OpenVentasConnection(schema);
                    com_Maritimo = new NpgsqlCommand();
                    com_Maritimo.Connection = con_Maritimo;
                    com_Maritimo.CommandType = CommandType.Text;
                    #region Definir Tipo BL
                    if ((Datos_BL.ttoID == 1) || (Datos_BL.ttoID == 17))
                    {
                        #region FCL
                        tipo_bl = "F";
                        #endregion
                    }
                    else if ((Datos_BL.ttoID == 2) || (Datos_BL.ttoID == 18))
                    {
                        #region LCL
                        tipo_bl = "L";
                        #endregion
                    }
                    #endregion
                    com_Maritimo.CommandText = "update cargos_bl set id_grupo=" + grupoID + " where bl_id=" + Datos_BL.BLID + " and tipo_bl='" + tipo_bl + "' and cargo_id=" + Cargo_BL.Cargo_ID + ";";
                    result = com_Maritimo.ExecuteNonQuery();
                    DB.CloseObj_insert(com_Maritimo, con_Maritimo);
                }
                catch (Exception e)
                {
                    log4net ErrLog = new log4net();
                    ErrLog.ErrorLog(e.Message);
                    Arr_result = new ArrayList();
                    Arr_result.Add("0");
                    Arr_result.Add("Existio un error al Asignar Grupo");
                    return Arr_result;
                }
                #endregion
            }
            else if (_sisID == 2)
            {
                #region Cargos Sistema Aereo
                MySqlConnection con_Aereo;
                MySqlCommand com_Aereo;
                try
                {
                    con_Aereo = DB.OpenAereoConnection();
                    com_Aereo = new MySqlCommand();
                    com_Aereo.Connection = con_Aereo;
                    com_Aereo.CommandType = CommandType.Text;
                    if (Datos_BL.ttoID == 3)//Importacion
                    {
                        com_Aereo.CommandText = "update ChargeItems set InterGroupID=" + grupoID + " where AWBID=" + Datos_BL.BLID + " and ChargeID=" + Cargo_BL.Cargo_ID + " and DocTyp=1;";
                    }
                    else if (Datos_BL.ttoID == 4)//Exportacion
                    {
                        com_Aereo.CommandText = "update ChargeItems set InterGroupID=" + grupoID + " where AWBID=" + Datos_BL.BLID + " and ChargeID=" + Cargo_BL.Cargo_ID + " and DocTyp=0;";
                    }
                    result = com_Aereo.ExecuteNonQuery();
                    DB.CloseMySQLObj_insert(com_Aereo, con_Aereo);
                }
                catch (Exception e)
                {
                    log4net ErrLog = new log4net();
                    ErrLog.ErrorLog(e.Message);
                    Arr_result = new ArrayList();
                    Arr_result.Add("0");
                    Arr_result.Add("Existio un error al Asignar Grupo");
                    return Arr_result;
                }
                #endregion
            }
            else if (_sisID == 3)
            {
                #region Cargos Sistema Terrestre
                MySqlConnection con_Terrestre;
                MySqlCommand com_Terrestre;
                MySqlDataReader reader_Terrestre;
                try
                {
                    con_Terrestre = DB.OpenTerrestreConnection();
                    com_Terrestre = new MySqlCommand();
                    com_Terrestre.Connection = con_Terrestre;
                    com_Terrestre.CommandType = CommandType.Text;
                    if ((Datos_BL.ttoID == 5) || (Datos_BL.ttoID == 6) || (Datos_BL.ttoID == 7))
                    {
                        #region Express-Consolidado-Local
                        com_Terrestre.CommandText = "update ChargeItems set InterGroupID=" + grupoID + " where SBLID=" + Datos_BL.BLID + " and ChargeID=" + Cargo_BL.Cargo_ID + ";";
                        #endregion
                    }
                    reader_Terrestre = com_Terrestre.ExecuteReader();
                    DB.CloseMySQLObj(reader_Terrestre, com_Terrestre, con_Terrestre);
                }
                catch (Exception e)
                {
                    log4net ErrLog = new log4net();
                    ErrLog.ErrorLog(e.Message);
                    Arr_result = new ArrayList();
                    Arr_result.Add("0");
                    Arr_result.Add("Existio un error al Asignar Grupo");
                    return Arr_result;
                }
                #endregion
            }
        }
        #endregion
        Arr_result = new ArrayList();
        Arr_result.Add("1");
        Arr_result.Add("Grupo asignado correctamente");
        return Arr_result;
    }
    public static ArrayList Eliminar_Cargos_Costos_Intercompany(UsuarioBean user, int _sisID, int ttoID, int blID, int ID, string Tipo)
    {
        ArrayList Arr_result = null;
        int result = 0;
        #region Asigar Grupo a Cargos de Intercompany Operativo
        string schema = "";
        string tipo_bl = "";
        #region Definir Schema
        if ((ttoID == 17) || (ttoID == 18))
        {
            schema = user.pais.schema_apl;
        }
        else
        {
            schema = user.pais.schema;
        }
        #endregion
        if (_sisID == 1)
        {
            #region Cargos Sistema Maritimo
            NpgsqlConnection con_Maritimo;
            NpgsqlCommand com_Maritimo;
            try
            {
                con_Maritimo = DB.OpenVentasConnection(schema);
                com_Maritimo = new NpgsqlCommand();
                com_Maritimo.Connection = con_Maritimo;
                com_Maritimo.CommandType = CommandType.Text;
                #region Definir Tipo BL
                if ((ttoID == 1) || (ttoID == 17))
                {
                    #region FCL
                    tipo_bl = "F";
                    #endregion
                }
                else if ((ttoID == 2) || (ttoID == 18))
                {
                    #region LCL
                    tipo_bl = "L";
                    #endregion
                }
                #endregion
                if (Tipo == "CARGO")
                {
                    com_Maritimo.CommandText = "update cargos_bl set activo=false where bl_id=" + blID + " and tipo_bl='" + tipo_bl + "' and cargo_id=" + ID + ";";
                }
                else if (Tipo == "COSTO")
                {
                    com_Maritimo.CommandText = "update costos_master set activo=false where bl_id=" + blID + " and tipo_bl='" + tipo_bl + "' and id_costo_master=" + ID + ";";
                }
                result = com_Maritimo.ExecuteNonQuery();
                DB.CloseObj_insert(com_Maritimo, con_Maritimo);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                Arr_result = new ArrayList();
                Arr_result.Add("0");
                Arr_result.Add("Existio un error al Anular " + Tipo + "");
                return Arr_result;
            }
            #endregion
        }
        else if (_sisID == 2)
        {
            #region Cargos Sistema Aereo
            MySqlConnection con_Aereo;
            MySqlCommand com_Aereo;
            MySqlDataReader reader_Aereo;
            try
            {
                con_Aereo = DB.OpenAereoConnection();
                com_Aereo = new MySqlCommand();
                com_Aereo.Connection = con_Aereo;
                com_Aereo.CommandType = CommandType.Text;
                if (ttoID == 3)
                {
                    #region Importacion
                    com_Aereo.CommandText = "";
                    #endregion
                }
                else if (ttoID == 4)
                {
                    #region Exportacion
                    com_Aereo.CommandText = "";
                    #endregion
                }
                reader_Aereo = com_Aereo.ExecuteReader();
                DB.CloseMySQLObj(reader_Aereo, com_Aereo, con_Aereo);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                Arr_result = new ArrayList();
                Arr_result.Add("0");
                Arr_result.Add("Existio un error al Asignar Grupo");
                return Arr_result;
            }
            #endregion
        }
        else if (_sisID == 3)
        {
            #region Cargos Sistema Terrestre
            MySqlConnection con_Terrestre;
            MySqlCommand com_Terrestre;
            MySqlDataReader reader_Terrestre;
            try
            {
                con_Terrestre = DB.OpenTerrestreConnection();
                com_Terrestre = new MySqlCommand();
                com_Terrestre.Connection = con_Terrestre;
                com_Terrestre.CommandType = CommandType.Text;
                if ((ttoID == 5) || (ttoID == 6) || (ttoID == 7))
                {
                    #region Express-Consolidado-Local
                    com_Terrestre.CommandText = "";
                    #endregion
                }
                reader_Terrestre = com_Terrestre.ExecuteReader();
                DB.CloseMySQLObj(reader_Terrestre, com_Terrestre, con_Terrestre);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                Arr_result = new ArrayList();
                Arr_result.Add("0");
                Arr_result.Add("Existio un error al Asignar Grupo");
                return Arr_result;
            }
            #endregion
        }
        #endregion
        Arr_result = new ArrayList();
        Arr_result.Add("1");
        Arr_result.Add("Grupo asignado correctamente");
        return Arr_result;
    }
    public static ArrayList Obtener_Log_Intercompany_Operativo_X_Empresa(int empresaID)
    {
        ArrayList Arr = new ArrayList();
        ArrayList Arr_Result = new ArrayList();
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
            comm.CommandText = "select a.ttel_padre_ref_id, INTERCOMPANY_HIJO, 'FACTURA', b.tfa_serie, b.tfa_correlativo, b.tfa_ted_id, 'PROVISION', c.tpr_serie, c.tpr_correlativo, c.tpr_ted_id, 'NOTA DEBITO', d.tnd_serie, d.tnd_correlativo, d.tnd_ted_id, b.tfa_hbl, d.tnd_nombre, upper(e.ted_nombre) as estado_factura, upper(f.ted_nombre) as estado_provision, upper(g.ted_nombre) as estado_nota_debito " +
            "from tbl_transacciones_encadenadas_log a " +
            "LEFT JOIN dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select id_intercompany, nombre_comercial, id_empresa_baw from intercompanys where activo=true') Intercompanys_Result_Hijo (ID_INTERCOMPANY_HIJO bigint, INTERCOMPANY_HIJO varchar, ID_EMPRESA_BAW_HIJA bigint) ON (a.ttel_hijo_empresa_id=ID_EMPRESA_BAW_HIJA) " +
            "LEFT JOIN dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select id_intercompany, nombre_comercial, id_empresa_baw from intercompanys where activo=true') Intercompanys_Result_Padre (ID_INTERCOMPANY_PADRE bigint, INTERCOMPANY_PADRE varchar, ID_EMPRESA_BAW_PADRE bigint) ON (a.ttel_padre_empresa_id=ID_EMPRESA_BAW_PADRE) " +
            "LEFT JOIN tbl_facturacion b on (a.ttel_padre_ttr_id=1 and a.ttel_padre_ref_id=b.tfa_id) " +
            "LEFT JOIN tbl_provisiones c on (a.ttel_hijo_ttr_id=5 and a.ttel_hijo_ref_id=c.tpr_prov_id) " +
            "LEFT JOIN tbl_nota_debito d on (a.ttel_hijo_ttr_id=4 and a.ttel_hijo_ref_id=d.tnd_id) " +
            "LEFT JOIN tbl_estado_documento e on (b.tfa_ted_id=e.ted_id) " +
            "LEFT JOIN tbl_estado_documento f on (c.tpr_ted_id=f.ted_id) " +
            "LEFT JOIN tbl_estado_documento g on (d.tnd_ted_id=g.ted_id) " +
            "where a.ttel_tta_id=2 and a.ttel_estado=1 " +
            "and a.ttel_padre_empresa_id=" + empresaID + " " +
            "order by a.ttel_id desc ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.strC1 = reader.GetValue(0).ToString();//ttel_padre_ref_id
                Bean.strC2 = reader.GetValue(1).ToString();//intercompany_hijo
                Bean.strC3 = reader.GetValue(2).ToString();//FA
                Bean.strC4 = reader.GetValue(3).ToString();//tfa_serie
                Bean.strC5 = reader.GetValue(4).ToString();//tfa_correlativo
                Bean.strC6 = reader.GetValue(5).ToString();//tfa_ted_id
                Bean.strC7 = reader.GetValue(6).ToString();//PRV
                Bean.strC8 = reader.GetValue(7).ToString();//tpr_serie
                Bean.strC9 = reader.GetValue(8).ToString();//tpr_correlativo
                Bean.strC10 = reader.GetValue(9).ToString();//tpr_ted_id
                Bean.strC11 = reader.GetValue(10).ToString();//ND
                Bean.strC12 = reader.GetValue(11).ToString();//tnd_serie
                Bean.strC13 = reader.GetValue(12).ToString();//tnd_correlativo
                Bean.strC14 = reader.GetValue(13).ToString();//tnd_ted_id
                Bean.strC15 = reader.GetValue(14).ToString();//tfa_hbl
                Bean.strC16 = reader.GetValue(15).ToString();//tnd_nombre
                Bean.strC17 = reader.GetValue(16).ToString();//estado_factura
                Bean.strC18 = reader.GetValue(17).ToString();//estado_provision
                Bean.strC19 = reader.GetValue(18).ToString();//estado_nota_debito
                Arr.Add(Bean);
            }
            DB.CloseObj(reader, comm, conn);
            Bean = null;
            RE_GenericBean Bean_Temporal = null;
            RE_GenericBean Bean_Auxiliar = null;
            string serie_provision = "";
            string correlativo_provision = "";
            string estado_provision = "";
            string serie_nota_debito = "";
            string correlativo_nota_debito = "";
            string estado_nota_debito = "";
            string nombre_nota_debito = "";
            for (int i = 0; i < Arr.Count; i++)
            {
                for (int j = i + 1; j < Arr.Count; j++)
                {
                    Bean = (RE_GenericBean)Arr[i];
                    Bean_Temporal = (RE_GenericBean)Arr[j];
                    if (Bean.strC1 == Bean_Temporal.strC1)
                    {
                        if (Bean.strC8 != "")
                        {
                            serie_provision = Bean.strC8;
                            correlativo_provision = Bean.strC9;
                            estado_provision = Bean.strC18;
                            serie_nota_debito = Bean_Temporal.strC12;
                            correlativo_nota_debito = Bean_Temporal.strC13;
                            estado_nota_debito = Bean_Temporal.strC19;
                            nombre_nota_debito = Bean_Temporal.strC16;
                        }
                        else
                        {
                            serie_provision = Bean_Temporal.strC8;
                            correlativo_provision = Bean_Temporal.strC9;
                            estado_provision = Bean_Temporal.strC18;
                            serie_nota_debito = Bean.strC12;
                            correlativo_nota_debito = Bean.strC13;
                            estado_nota_debito = Bean.strC19;
                            nombre_nota_debito = Bean.strC16;
                        }
                        Bean_Auxiliar = new RE_GenericBean();
                        Bean_Auxiliar.strC1 = Bean.strC1;
                        Bean_Auxiliar.strC2 = Bean.strC2;
                        Bean_Auxiliar.strC3 = Bean.strC3;
                        Bean_Auxiliar.strC4 = Bean.strC4;
                        Bean_Auxiliar.strC5 = Bean.strC5;
                        Bean_Auxiliar.strC6 = Bean.strC17;
                        Bean_Auxiliar.strC7 = Bean.strC7;
                        Bean_Auxiliar.strC8 = serie_provision;
                        Bean_Auxiliar.strC9 = correlativo_provision;
                        Bean_Auxiliar.strC10 = estado_provision;
                        Bean_Auxiliar.strC11 = Bean.strC11;
                        Bean_Auxiliar.strC12 = serie_nota_debito;
                        Bean_Auxiliar.strC13 = correlativo_nota_debito;
                        Bean_Auxiliar.strC14 = estado_nota_debito;
                        Bean_Auxiliar.strC15 = Bean.strC15;
                        Bean_Auxiliar.strC16 = nombre_nota_debito;
                        Arr_Result.Add(Bean_Auxiliar);
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
        return Arr_Result;
    }
    public static ArrayList Obtener_Log_Intercompany_Administrativo_X_Empresa(int empresaID)
    {
        ArrayList Arr = new ArrayList();
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
            comm.CommandText = "select a.ttel_padre_ref_id, INTERCOMPANY_PADRE, " +
            "(CASE WHEN a.ttel_padre_ttr_id=1 THEN 'FACTURA' ELSE 'NOTA DEBITO' END) as TTR_PADRE, " +
            "(CASE WHEN a.ttel_padre_ttr_id=1 THEN b.tfa_serie ELSE d.tnd_serie END) as SERIE_PADRE, " +
            "(CASE WHEN a.ttel_padre_ttr_id=1 THEN (cast(b.tfa_correlativo as text)) ELSE (cast (d.tnd_correlativo as text)) END) as CORRELATIVO_PADRE, " +
            "(CASE WHEN a.ttel_padre_ttr_id=1 THEN upper(e.ted_nombre) ELSE upper(g.ted_nombre)  END) as ESTADO_PADRE, " +
            "INTERCOMPANY_HIJO, 'PROVISION', c.tpr_serie, c.tpr_correlativo, upper(f.ted_nombre) as estado_provision, b.tfa_hbl " +
            "from tbl_transacciones_encadenadas_log a " +
            "LEFT JOIN dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select id_intercompany, nombre_comercial, id_empresa_baw from intercompanys where activo=true') Intercompanys_Result_Hijo (ID_INTERCOMPANY_HIJO bigint, INTERCOMPANY_HIJO varchar, ID_EMPRESA_BAW_HIJA bigint) ON (a.ttel_hijo_empresa_id=ID_EMPRESA_BAW_HIJA) " +
            "LEFT JOIN dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select id_intercompany, nombre_comercial, id_empresa_baw from intercompanys where activo=true') Intercompanys_Result_Padre (ID_INTERCOMPANY_PADRE bigint, INTERCOMPANY_PADRE varchar, ID_EMPRESA_BAW_PADRE bigint) ON (a.ttel_padre_empresa_id=ID_EMPRESA_BAW_PADRE) " +
            "LEFT JOIN tbl_facturacion b on (a.ttel_padre_ttr_id=1 and a.ttel_padre_ref_id=b.tfa_id) " +
            "LEFT JOIN tbl_nota_debito d on (a.ttel_padre_ttr_id=4 and a.ttel_padre_ref_id=d.tnd_id) " +
            "LEFT JOIN tbl_provisiones c on (a.ttel_hijo_ttr_id=5 and a.ttel_hijo_ref_id=c.tpr_prov_id) " +
            "LEFT JOIN tbl_estado_documento e on (b.tfa_ted_id=e.ted_id) " +
            "LEFT JOIN tbl_estado_documento f on (c.tpr_ted_id=f.ted_id) " +
            "LEFT JOIN tbl_estado_documento g on (d.tnd_ted_id=g.ted_id) " +
            "where a.ttel_tta_id=1 and a.ttel_estado=1 " +
            "and a.ttel_padre_empresa_id=" + empresaID + " " +
            "order by a.ttel_id desc ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.strC1 = reader.GetValue(0).ToString();//ttel_padre_ref_id
                Bean.strC2 = reader.GetValue(1).ToString();//INTERCOMPANY_PADRE
                Bean.strC3 = reader.GetValue(2).ToString();//TTR_PADRE
                Bean.strC4 = reader.GetValue(3).ToString();//SERIE_PADRE
                Bean.strC5 = reader.GetValue(4).ToString();//CORRELATIVO_PADRE
                Bean.strC6 = reader.GetValue(5).ToString();//ESTADO_PADRE
                Bean.strC7 = reader.GetValue(6).ToString();//INTERCOMPANY_HIJO
                Bean.strC8 = reader.GetValue(7).ToString();//PROVISION
                Bean.strC9 = reader.GetValue(8).ToString();//tpr_serie
                Bean.strC10 = reader.GetValue(9).ToString();//tpr_correlativo
                Bean.strC11 = reader.GetValue(10).ToString();//estado_provision
                Bean.strC12 = reader.GetValue(11).ToString();//tfa_hbl
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
    }
    public static RE_GenericBean Obtener_Detalle_Notificacion_Automatica_Intercompany(int ttrID_Padre, int refID_Padre, int Tipo_Intercompany)
    {
        ArrayList Arr = new ArrayList();
        RE_GenericBean Bean = null;
        RE_GenericBean Bean_Detalle = new RE_GenericBean();
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select ttel_padre_ttr_id, ttel_padre_ref_id, ttel_hijo_ttr_id, ttel_hijo_ref_id, ttel_padre_empresa_id, ttel_hijo_empresa_id from tbl_transacciones_encadenadas_log where ttel_estado=1 and ttel_tta_id=" + Tipo_Intercompany + " and ttel_padre_ttr_id=" + ttrID_Padre + " and ttel_padre_ref_id=" + refID_Padre + "";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.strC1 = reader.GetValue(0).ToString();//ttel_padre_ttr_id
                Bean.strC2 = reader.GetValue(1).ToString();//ttel_padre_ref_id
                Bean.strC3 = reader.GetValue(2).ToString();//ttel_hijo_ttr_id
                Bean.strC4 = reader.GetValue(3).ToString();//ttel_hijo_ref_id
                Bean.strC5 = reader.GetValue(4).ToString();//ttel_padre_empresa_id
                Bean.strC6 = reader.GetValue(5).ToString();//ttel_hijo_empresa_id
                Arr.Add(Bean);
            }
            DB.CloseObj(reader, comm, conn);
            #region Definir Nombre de Intercompanys
            int empresaORIGEN_ID = 0;
            int empresaDESTINO_ID = 0;
            string empresaORIGEN_Nombre = "";
            string empresaDESTINO_Nombre = "";
            string empresaORIGEN_NIT = "";
            string empresaDESTINO_NIT = "";
            empresaORIGEN_ID = int.Parse(Bean.strC5);
            empresaDESTINO_ID = int.Parse(Bean.strC6);
            RE_GenericBean Bean_Intercompany_Temporal = null;
            Bean_Intercompany_Temporal = (RE_GenericBean)DB.Get_Intercompany_Data_By_Empresa(empresaORIGEN_ID);
            empresaORIGEN_Nombre = Bean_Intercompany_Temporal.strC5 + "   (" + Bean_Intercompany_Temporal.strC1 + ")";
            empresaORIGEN_Nombre = Bean_Intercompany_Temporal.strC5;
            empresaORIGEN_NIT = Bean_Intercompany_Temporal.strC2;
            Bean_Intercompany_Temporal = null;
            Bean_Intercompany_Temporal = (RE_GenericBean)DB.Get_Intercompany_Data_By_Empresa(empresaDESTINO_ID);
            empresaDESTINO_Nombre = Bean_Intercompany_Temporal.strC5 + "   (" + Bean_Intercompany_Temporal.strC1 + ")";
            empresaDESTINO_Nombre = Bean_Intercompany_Temporal.strC5;
            empresaDESTINO_NIT = Bean_Intercompany_Temporal.strC2;
            Bean_Detalle.strC30 = empresaORIGEN_Nombre;
            Bean_Detalle.strC31 = empresaDESTINO_Nombre;
            Bean_Detalle.strC32 = empresaORIGEN_NIT;
            Bean_Detalle.strC33 = empresaDESTINO_NIT;
            Bean_Detalle.strC34 = empresaORIGEN_ID.ToString();
            Bean_Detalle.strC35 = empresaDESTINO_ID.ToString();
            #endregion
            #region Generar Detalle de Transacciones Intercompanys
            foreach (RE_GenericBean Bean_Temporal in Arr)
            {
                if (Bean_Temporal.strC1 == "1")//Transaccion Padre
                { 
                    #region El documento Padre es Factura
                    RE_GenericBean Factura_Bean = (RE_GenericBean)DB.getFacturaData(int.Parse(Bean_Temporal.strC2));
                    Bean_Detalle.strC1 = "FACTURA";
                    Bean_Detalle.strC2 = Factura_Bean.strC5;//Fecha Emision
                    Bean_Detalle.strC3 = Factura_Bean.strC28 +" - "+Factura_Bean.strC1;//Serie y Correlativo
                    if (Factura_Bean.intC9 == 1) { Bean_Detalle.strC4 = "FISCAL"; } else { Bean_Detalle.strC4 = "FINANCIERA"; }//Contabilidad
                    Bean_Detalle.strC5 = Utility.TraducirMonedaInt(Factura_Bean.intC4);//Moneda
                    Bean_Detalle.strC6 = Factura_Bean.decC3.ToString("#,#.00#;(#,#.00#)");//Monto
                    Bean_Detalle.strC7 = Factura_Bean.strC10;//Master
                    Bean_Detalle.strC8 = Factura_Bean.strC9;//House
                    Bean_Detalle.strC9 = Factura_Bean.strC7;//Observaciones
                    #endregion
                }
                else if (Bean_Temporal.strC1 == "4")
                {
                    #region El documento Padre es Nota de Debito
                    RE_GenericBean Nota_Debito_Bean = (RE_GenericBean)DB.getNotaDebitoData(int.Parse(Bean_Temporal.strC2));
                    Bean_Detalle.strC1 = "NOTA DEBITO";
                    Bean_Detalle.strC2 = Nota_Debito_Bean.strC3;//Fecha Emision
                    Bean_Detalle.strC3 = Nota_Debito_Bean.strC28 + " - " + Nota_Debito_Bean.intC6.ToString();//Serie y Correlativo
                    if (Nota_Debito_Bean.intC10 == 1) { Bean_Detalle.strC4 = "FISCAL"; } else { Bean_Detalle.strC4 = "FINANCIERA"; }//Contabilidad
                    Bean_Detalle.strC5 = Utility.TraducirMonedaInt(Nota_Debito_Bean.intC4);//Moneda
                    Bean_Detalle.strC6 = Nota_Debito_Bean.decC1.ToString("#,#.00#;(#,#.00#)");//Monto
                    Bean_Detalle.strC7 = Nota_Debito_Bean.strC8;//Master
                    Bean_Detalle.strC8 = Nota_Debito_Bean.strC7;//House
                    Bean_Detalle.strC9 = Nota_Debito_Bean.strC4;//Observaciones
                    #endregion
                }
                if (Bean_Temporal.strC3 == "5")//Transaccion Hijo
                {
                    #region El documento Hijo es una Provision
                    RE_GenericBean Provision_Bean = (RE_GenericBean)Utility.getDetalleProvision(int.Parse(Bean_Temporal.strC4));
                    Bean_Detalle.strC10 = "PROVISION";
                    Bean_Detalle.strC11 = empresaORIGEN_Nombre;//Nombre Intercompany
                    Bean_Detalle.strC12 = Provision_Bean.strC12;//Fecha Emision
                    Bean_Detalle.strC13 = Provision_Bean.strC17 + " - " + Provision_Bean.intC6.ToString();//Serie y Correlativo
                    if (Provision_Bean.intC14 == 1) { Bean_Detalle.strC14 = "FISCAL"; } else { Bean_Detalle.strC14 = "FINANCIERA"; }//Contabilidad
                    Bean_Detalle.strC15 = Utility.TraducirMonedaInt(Provision_Bean.intC5);//Moneda
                    Bean_Detalle.strC16 = Provision_Bean.decC1.ToString("#,#.00#;(#,#.00#)");//Monto
                    Bean_Detalle.strC17 = Provision_Bean.strC14;//Master
                    Bean_Detalle.strC18 = Provision_Bean.strC13;//House
                    Bean_Detalle.strC19 = Provision_Bean.strC6;//Observaciones
                    #endregion
                }
                else if (Bean_Temporal.strC3 == "4")
                {
                    #region El documento Hijo es Nota de Debito al Cliente
                    RE_GenericBean Nota_Debito_Bean = (RE_GenericBean)DB.getNotaDebitoData(int.Parse(Bean_Temporal.strC4));
                    Bean_Detalle.strC20 = "NOTA DEBITO";
                    Bean_Detalle.strC21 = Nota_Debito_Bean.strC2;//Cliente
                    Bean_Detalle.strC22 = Nota_Debito_Bean.strC3;//Fecha Emision
                    Bean_Detalle.strC23 = Nota_Debito_Bean.strC28 + " - " + Nota_Debito_Bean.intC6.ToString();//Serie y Correlativo
                    if (Nota_Debito_Bean.intC10 == 1) { Bean_Detalle.strC24 = "FISCAL"; } else { Bean_Detalle.strC24 = "FINANCIERA"; }//Contabilidad
                    Bean_Detalle.strC25 = Utility.TraducirMonedaInt(Nota_Debito_Bean.intC4);//Moneda
                    Bean_Detalle.strC26 = Nota_Debito_Bean.decC1.ToString("#,#.00#;(#,#.00#)");//Monto
                    Bean_Detalle.strC27 = Nota_Debito_Bean.strC8;//Master
                    Bean_Detalle.strC28 = Nota_Debito_Bean.strC7;//House
                    Bean_Detalle.strC29 = Nota_Debito_Bean.strC4;//Observaciones
                    #endregion
                }
            }
            #endregion
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Bean_Detalle;
    }
    //INICIO
    public static int Validar_Existencia_Sesion_Reconciliacion_Carga(int EmpresaID, int SisID, int ttoID, int blID, string MBL)
    {
        int resultado = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select trs_id from tbl_reconciliacion_carga_sesiones where trs_empresa_id=" + EmpresaID + " and trs_sis_id=" + SisID + " and trs_tto_id=" + ttoID + " and trs_bl='" + MBL + "' and trs_estado=1 ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                resultado = int.Parse(reader.GetValue(0).ToString());
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return resultado;
    }
    public static int Validar_Existencia_Sesion_Reconciliacion_X_ID(int sessionID)
    {
        int resultado = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select trs_id from tbl_reconciliacion_carga_sesiones where trs_id=" + sessionID + " and trs_estado=1 ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                resultado = int.Parse(reader.GetValue(0).ToString());
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return resultado;
    }
    public static RE_GenericBean Obtener_Detalle_Sesion_Reconciliacon_Carga(int ID)
    {
        RE_GenericBean Bean_Result = null;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select trs_id, trs_empresa_id, trs_empresa, trs_sis_id, trs_sistema, trs_tto_id, trs_tipo_operacion, trs_bl_id, trs_viaje_id, trs_bl, trs_agente_id, trs_naviera_id, trs_imp_exp_id, trs_imp_exp, trs_usu_id, trs_fecha_creacion, trs_viaje_no, trs_usu_contabilizacion, trs_fecha_contabilizacion, trs_tipo_cambio_contabilizacion, trs_fecha_arribo, trs_sucursal_id, trs_estado, trs_estado_sesion " +
            "from tbl_reconciliacion_carga_sesiones where trs_id=" + ID + " and trs_estado=1 ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean_Result = new RE_GenericBean();
                Bean_Result.strC1 = reader.GetValue(0).ToString();//trs_id
                Bean_Result.strC2 = reader.GetValue(1).ToString();//trs_empresa_id
                Bean_Result.strC3 = reader.GetValue(2).ToString();//trs_empresa
                Bean_Result.strC4 = reader.GetValue(3).ToString();//trs_sis_id
                Bean_Result.strC5 = reader.GetValue(4).ToString();//trs_sistema
                Bean_Result.strC6 = reader.GetValue(5).ToString();//trs_tto_id
                Bean_Result.strC7 = reader.GetValue(6).ToString();//trs_tipo_operacion
                Bean_Result.strC8 = reader.GetValue(7).ToString();//trs_bl_id
                Bean_Result.strC9 = reader.GetValue(8).ToString();//trs_viaje_id
                Bean_Result.strC10 = reader.GetValue(9).ToString();//trs_bl
                Bean_Result.strC11 = reader.GetValue(10).ToString();//trs_agente_id
                Bean_Result.strC12 = reader.GetValue(11).ToString();//trs_naviera_id
                Bean_Result.strC13 = reader.GetValue(12).ToString();//trs_imp_exp_id
                Bean_Result.strC14 = reader.GetValue(13).ToString();//trs_imp_exp
                Bean_Result.strC15 = reader.GetValue(14).ToString();//trs_usu_id
                Bean_Result.strC16 = reader.GetValue(15).ToString();//trs_fecha_creacion
                Bean_Result.strC17 = reader.GetValue(16).ToString();//trs_viaje_no
                Bean_Result.strC18 = reader.GetValue(17).ToString();//trs_usu_contabilizacion
                Bean_Result.strC19 = reader.GetValue(18).ToString();//trs_fecha_contabilizacion
                Bean_Result.strC20 = reader.GetValue(19).ToString();//trs_tipo_cambio_contabilizacion
                Bean_Result.strC21 = reader.GetValue(20).ToString();//trs_fecha_arribo
                Bean_Result.strC22 = reader.GetValue(21).ToString();//trs_sucursal_id
                Bean_Result.strC23 = reader.GetValue(22).ToString();//trs_estado
                Bean_Result.strC24 = reader.GetValue(23).ToString();//trs_estado_sesion
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Bean_Result;
    }
    public static int Crear_Sesion_Reconciliacion_Carga(RE_GenericBean Bean)
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
            comm.CommandText = "insert into tbl_reconciliacion_carga_sesiones (trs_empresa_id, trs_empresa, trs_sis_id, trs_sistema, trs_tto_id, trs_tipo_operacion, trs_bl_id, trs_viaje_id, trs_bl, trs_agente_id, trs_naviera_id, trs_imp_exp_id, trs_imp_exp, trs_usu_id, trs_viaje_no, trs_ip_address_creacion, trs_fecha_arribo)";
            comm.CommandText += " values (@trs_empresa_id, @trs_empresa, @trs_sis_id, @trs_sistema, @trs_tto_id, @trs_tipo_operacion, @trs_bl_id, @trs_viaje_id, @trs_bl, @trs_agente_id, @trs_naviera_id, @trs_imp_exp_id, @trs_imp_exp, @trs_usu_id, @trs_viaje_no, @trs_ip_address_creacion, @trs_fecha_arribo)";
            comm.Parameters.Add("@trs_empresa_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC1);
            comm.Parameters.Add("@trs_empresa", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC2;
            comm.Parameters.Add("@trs_sis_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC3);
            comm.Parameters.Add("@trs_sistema", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC4;
            comm.Parameters.Add("@trs_tto_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC5);
            comm.Parameters.Add("@trs_tipo_operacion", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC6;
            comm.Parameters.Add("@trs_bl_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC7);
            comm.Parameters.Add("@trs_viaje_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC8);
            comm.Parameters.Add("@trs_bl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC9;
            comm.Parameters.Add("@trs_agente_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC10);
            comm.Parameters.Add("@trs_naviera_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC11);
            comm.Parameters.Add("@trs_imp_exp_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC12);
            comm.Parameters.Add("@trs_imp_exp", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC13;
            comm.Parameters.Add("@trs_usu_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC14;
            comm.Parameters.Add("@trs_viaje_no", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC15;
            comm.Parameters.Add("@trs_ip_address_creacion", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC16;
            comm.Parameters.Add("@trs_fecha_arribo", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC17;
            resultado = comm.ExecuteNonQuery();
            comm.Parameters.Clear();
            comm.CommandText = "";
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
    public static int Validar_Existencia_Detalle_BLs_Reconciliacion_Carga(int sesionID)
    {
        int resultado = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select trb_id from tbl_reconciliacion_carga_bls where trb_trs_id=" + sesionID + " and trb_estado=1";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                resultado = int.Parse(reader.GetValue(0).ToString());
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return resultado;
    }
    public static int Insertar_Detalle_BLs_Reconciliacion_Carga(ArrayList Arr)
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
            foreach (RE_GenericBean Bean in Arr)
            {
                comm.CommandText = "insert into tbl_reconciliacion_carga_bls (trb_tipo_bl, trb_bl_id, trb_bl, trb_routing_id, trb_routing, trb_contenedor_id, trb_contenedor, trb_peso, trb_volumen, trb_cli_id, trb_destino, trb_trs_id, trb_to_order, trb_to_order_id, trb_puerto_origen_id)";
                comm.CommandText += " values (@trb_tipo_bl, @trb_bl_id, @trb_bl, @trb_routing_id, @trb_routing, @trb_contenedor_id, @trb_contenedor, @trb_peso, @trb_volumen, @trb_cli_id, @trb_destino, @trb_trs_id, @trb_to_order, @trb_to_order_id, @trb_puerto_origen_id)";
                comm.Parameters.Add("@trb_tipo_bl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC1;
                comm.Parameters.Add("@trb_bl_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC2);
                comm.Parameters.Add("@trb_bl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC3;
                comm.Parameters.Add("@trb_routing_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC4);
                comm.Parameters.Add("@trb_routing", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC5;
                comm.Parameters.Add("@trb_contenedor_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC6);
                comm.Parameters.Add("@trb_contenedor", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC7;
                comm.Parameters.Add("@trb_peso", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC8;
                comm.Parameters.Add("@trb_volumen", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC9;
                comm.Parameters.Add("@trb_cli_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC10);
                comm.Parameters.Add("@trb_destino", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC11;
                comm.Parameters.Add("@trb_trs_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC12);
                comm.Parameters.Add("@trb_to_order", NpgsqlTypes.NpgsqlDbType.Boolean).Value = Convert.ToBoolean(Bean.strC13);
                comm.Parameters.Add("@trb_to_order_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC14);
                comm.Parameters.Add("@trb_puerto_origen_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC15);
                resultado = comm.ExecuteNonQuery();
                comm.Parameters.Clear();
                comm.CommandText = "";
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
    public static ArrayList Obtener_Detalle_BLs_Reconciliacion_Carga(int ID)
    {
        ArrayList Arr_Result = new ArrayList();
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
            comm.CommandText = "select trb_id, trb_tipo_bl, trb_bl_id, trb_bl, trb_routing_id, trb_routing, trb_contenedor_id, trb_contenedor, trb_peso, trb_volumen, trb_cli_id, trb_destino, trb_estado_bl, trb_estado, trb_to_order, trb_to_order_id, trb_puerto_origen_id " +
            "from tbl_reconciliacion_carga_bls where trb_trs_id=" + ID + " and trb_estado>0 order by trb_id, trb_routing desc";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.strC1 = reader.GetValue(0).ToString();//trb_id
                Bean.strC2 = reader.GetValue(1).ToString();//trb_tipo_bl
                Bean.strC3 = reader.GetValue(2).ToString();//trb_bl_id
                Bean.strC4 = reader.GetValue(3).ToString();//trb_bl
                Bean.strC5 = reader.GetValue(4).ToString();//trb_routing_id
                Bean.strC6 = reader.GetValue(5).ToString();//trb_routing
                Bean.strC7 = reader.GetValue(6).ToString();//trb_contenedor_id
                Bean.strC8 = reader.GetValue(7).ToString();//trb_contenedor
                Bean.strC9 = reader.GetValue(8).ToString();//trb_peso
                Bean.strC10 = reader.GetValue(9).ToString();//trb_volumen
                Bean.strC11 = reader.GetValue(10).ToString();//trb_cli_id
                Bean.strC12 = reader.GetValue(11).ToString();//trb_destino
                Bean.strC13 = reader.GetValue(12).ToString();//trb_estado_bl
                Bean.strC14 = reader.GetValue(13).ToString();//trb_estado
                Bean.strC15 = reader.GetValue(14).ToString();//trb_to_order
                Bean.strC16 = reader.GetValue(15).ToString();//trb_to_order_id
                Bean.strC17 = reader.GetValue(16).ToString();//trb_puerto_origen_id
                Arr_Result.Add(Bean);
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Arr_Result;
    }
    public static int Insertar_Preguntas_Respuestas_Reconciliacion_Carga(string trbID, string Pregunta, string Respuesta, string usuID, string Fecha_Hora)
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
            comm.CommandText = "insert into tbl_reconciliacion_carga_cuestionario (trc_pregunta, trc_respuesta, trc_usu_id, trc_trb_id, trc_fecha_creacion)";
            comm.CommandText += " values (@trc_pregunta, @trc_respuesta, @trc_usu_id, @trc_trb_id, @trc_fecha_creacion)";
            comm.Parameters.Add("@trc_pregunta", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Pregunta;
            comm.Parameters.Add("@trc_respuesta", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Respuesta;
            comm.Parameters.Add("@trc_usu_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = usuID;
            comm.Parameters.Add("@trc_trb_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(trbID);
            comm.Parameters.Add("@trc_fecha_creacion", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = Fecha_Hora;
            comm.CommandText += " returning trc_id";
            resultado = int.Parse(comm.ExecuteScalar().ToString());
            comm.Parameters.Clear();
            comm.CommandText = "";
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
    public static int Existe_Pregunta_Reconciliacion_Carga(string Fecha_Hora)
    {
        int resultado = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select coalesce(trc_id,0) from tbl_reconciliacion_carga_cuestionario where trc_fecha_creacion='" + Fecha_Hora + "' ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                resultado = int.Parse(reader.GetValue(0).ToString());
            }
            comm.CommandText = "";
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return resultado;
    }
    public static int Existe_Transaccion_Reconciliacion_Carga(string Fecha_Hora)
    {
        int resultado = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select coalesce(count(trt_id),0) from tbl_reconciliacion_carga_transacciones where trt_fecha_creacion='" + Fecha_Hora + "' and trt_estado=1 ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                resultado = int.Parse(reader.GetValue(0).ToString());
            }
            comm.CommandText = "";
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return resultado;
    }
    public static int Inactivar_Pregunta_Respuesta_Reconciliacion_Carga_X_Fecha(string Fecha_Hora)
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
            comm.CommandText = "update tbl_reconciliacion_carga_cuestionario set trc_estado=0 where trc_fecha_creacion='" + Fecha_Hora + "' ";
            resultado = comm.ExecuteNonQuery();
            comm.CommandText = "";
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
    public static int Inactivar_Pregunta_Respuesta_Reconciliacion_Carga_X_ID(string ID)
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
            comm.CommandText = "update tbl_reconciliacion_carga_cuestionario set trc_estado=0 where trc_id=" + ID + " ";
            resultado = comm.ExecuteNonQuery();
            comm.CommandText = "";
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
    public static ArrayList Get_Cargos_X_Traficos_Para_Reconciliacion(PaisBean Pais_Bean, int SisID, int Tipo, int blID)
    {
        #region Get Cargos por Traficos
        string schema = "";
        ArrayList Arr_Cargos = new ArrayList();
        Bean_Cargos Bean_Cargos = null;
        #region Definir Schema
        if ((Tipo == 17) || (Tipo == 18))
        {
            schema = Pais_Bean.schema_apl;
        }
        else
        {
            schema = Pais_Bean.schema;
        }
        #endregion
        if (SisID == 1)
        {
            #region Cargar Cargos Sistema Maritimo
            NpgsqlConnection con_Maritimo;
            NpgsqlCommand com_Maritimo;
            NpgsqlDataReader reader_Maritimo;
            try
            {
                con_Maritimo = DB.OpenVentasConnection(schema);
                com_Maritimo = new NpgsqlCommand();
                com_Maritimo.Connection = con_Maritimo;
                com_Maritimo.CommandType = CommandType.Text;
                if ((Tipo == 1) || (Tipo == 17))
                {
                    #region FCL
                    com_Maritimo.CommandText = "select cargo_id, bl_id, id_rubro, id_servicio, id_moneda, coalesce(valor_collect,0) + coalesce(valor_sobreventa,0), factura_id, " +
                    "tipo_bl, tipo_documento, tipo_cargo, tipo_cobro, inter_company, id_grupo, id_tipo_persona, tipo_cobro, (coalesce(valor_collect,0) + coalesce(valor_sobreventa,0)) as Monto_Collect, (coalesce(valor_prepaid,0) + coalesce(valor_sobreventa,0)) as Monto_Prepaid " +
                    "from cargos_bl " +
                    //"where bl_id=" + blID + " and tipo_bl='F' and activo=TRUE and tipo_cargo=1 and id_tipo_persona=4 ";
                    "where bl_id=" + blID + " and tipo_bl='F' and factura_id=0 and activo=TRUE and tipo_cargo=1 and id_tipo_persona=4 ";
                    #endregion
                }
                else if ((Tipo == 2) || (Tipo == 18))
                {
                    #region LCL
                    com_Maritimo.CommandText = "select cargo_id, bl_id, id_rubro, id_servicio, id_moneda, coalesce(valor_collect,0) + coalesce(valor_sobreventa,0), factura_id, " +
                    "tipo_bl, tipo_documento, tipo_cargo, tipo_cobro, inter_company, id_grupo, id_tipo_persona, tipo_cobro, (coalesce(valor_collect,0) + coalesce(valor_sobreventa,0)) as Monto_Collect, (coalesce(valor_prepaid,0) + coalesce(valor_sobreventa,0)) as Monto_Prepaid " +
                    "from cargos_bl " +
                    //"where bl_id=" + blID + " and tipo_bl='L' and activo=TRUE and tipo_cargo=1 and id_tipo_persona=4 ";
                    "where bl_id=" + blID + " and tipo_bl='L' and factura_id=0 and activo=TRUE and tipo_cargo=1 and id_tipo_persona=4 ";
                    #endregion
                }
                reader_Maritimo = com_Maritimo.ExecuteReader();
                while (reader_Maritimo.Read())
                {
                    //Revisar Traduccion ID de Monedas
                    Bean_Cargos = new Bean_Cargos();
                    Bean_Cargos.Cargo_ID = int.Parse(reader_Maritimo.GetValue(0).ToString());
                    Bean_Cargos.Cargo_BLID = int.Parse(reader_Maritimo.GetValue(1).ToString());
                    Bean_Cargos.Cargo_Rub_ID = int.Parse(reader_Maritimo.GetValue(2).ToString());
                    Bean_Cargos.Cargo_Servicio_ID = int.Parse(reader_Maritimo.GetValue(3).ToString());
                    Bean_Cargos.Cargo_Moneda_Trafico_ID = int.Parse(reader_Maritimo.GetValue(4).ToString());
                    Bean_Cargos.Cargo_Moneda_ID = Contabilizacion_Automatica_CN.Traducir_Moneda_Master_To_BAW_X_ID(Bean_Cargos.Cargo_Moneda_Trafico_ID);
                    Bean_Cargos.Cargo_Monto = double.Parse(reader_Maritimo.GetValue(5).ToString());
                    Bean_Cargos.Factura_ID = int.Parse(reader_Maritimo.GetValue(6).ToString());
                    Bean_Cargos.Cargo_Tipo_BL = reader_Maritimo.GetValue(7).ToString();
                    Bean_Cargos.Tipo_Documento = int.Parse(reader_Maritimo.GetValue(8).ToString());
                    Bean_Cargos.Tipo_Cargo = int.Parse(reader_Maritimo.GetValue(9).ToString());
                    Bean_Cargos.Tipo_Cobro = int.Parse(reader_Maritimo.GetValue(10).ToString());
                    Bean_Cargos.ID_Intercompany = int.Parse(reader_Maritimo.GetValue(11).ToString());
                    Bean_Cargos.ID_Grupo = int.Parse(reader_Maritimo.GetValue(12).ToString());
                    Bean_Cargos.ID_Tipo_Persona = int.Parse(reader_Maritimo.GetValue(13).ToString());
                    //Nuevos Campos
                    Bean_Cargos.ID_Local_Internacional = int.Parse(reader_Maritimo.GetValue(14).ToString());
                    Bean_Cargos.Valor_Collect = double.Parse(reader_Maritimo.GetValue(15).ToString());
                    Bean_Cargos.Valor_Prepaid = double.Parse(reader_Maritimo.GetValue(16).ToString());
                    Arr_Cargos.Add(Bean_Cargos);
                }
                DB.CloseObj(reader_Maritimo, com_Maritimo, con_Maritimo);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                return null;
            }
            #endregion
        }
        else if (SisID == 2)
        {
            #region Cargar Costos Sistema Aereo
            MySqlConnection con_Aereo;
            MySqlCommand com_Aereo;
            MySqlDataReader reader_Aereo;
            try
            {
                con_Aereo = DB.OpenAereoConnection();
                com_Aereo = new MySqlCommand();
                com_Aereo.Connection = con_Aereo;
                com_Aereo.CommandType = CommandType.Text;
                if (Tipo == 3)
                {
                    #region Importacion
                    com_Aereo.CommandText = "select ChargeID, AWBID, ItemID, ServiceID, CurrencyID, (Value+ OverSold), InvoiceID, 'Import', DocType, " +
                    "InterChargeType, Local, InterCompanyID, InterGroupID, InterProviderType " +
                    "from ChargeItems " +
                    "where AWBID=" + blID + " and DocTyp=1 and InvoiceID=0 and Expired=0 and InterChargeType=1 and InterProviderType=4;";
                    #endregion
                }
                else if (Tipo == 4)
                {
                    #region Exportacion
                    com_Aereo.CommandText = "select ChargeID, AWBID, ItemID, ServiceID, CurrencyID, (Value+ OverSold), InvoiceID, 'Import', DocType, " +
                    "InterChargeType, Local, InterCompanyID, InterGroupID, InterProviderType " +
                    "from ChargeItems " +
                    "where AWBID=" + blID + " and DocTyp=0 and InvoiceID=0 and Expired=0 and InterChargeType=1 and InterProviderType=4;";
                    #endregion
                }
                reader_Aereo = com_Aereo.ExecuteReader();
                while (reader_Aereo.Read())
                {
                    Bean_Cargos = new Bean_Cargos();
                    Bean_Cargos.Cargo_ID = int.Parse(reader_Aereo.GetValue(0).ToString());
                    Bean_Cargos.Cargo_BLID = int.Parse(reader_Aereo.GetValue(1).ToString());
                    Bean_Cargos.Cargo_Rub_ID = int.Parse(reader_Aereo.GetValue(2).ToString());
                    Bean_Cargos.Cargo_Servicio_ID = int.Parse(reader_Aereo.GetValue(3).ToString());
                    Bean_Cargos.Cargo_Moneda_Simbolo = reader_Aereo.GetValue(4).ToString();
                    Bean_Cargos.Cargo_Moneda_ID = Utility.TraducirMonedaStr(Bean_Cargos.Cargo_Moneda_Simbolo);
                    Bean_Cargos.Cargo_Monto = double.Parse(reader_Aereo.GetValue(5).ToString());
                    Bean_Cargos.Factura_ID = int.Parse(reader_Aereo.GetValue(6).ToString());
                    Bean_Cargos.Cargo_Tipo_BL = "";
                    Bean_Cargos.Tipo_Documento = int.Parse(reader_Aereo.GetValue(8).ToString());
                    Bean_Cargos.Tipo_Cargo = int.Parse(reader_Aereo.GetValue(9).ToString());
                    Bean_Cargos.Tipo_Cobro = int.Parse(reader_Aereo.GetValue(10).ToString());
                    Bean_Cargos.ID_Intercompany = int.Parse(reader_Aereo.GetValue(11).ToString());
                    Bean_Cargos.ID_Grupo = int.Parse(reader_Aereo.GetValue(12).ToString());
                    Bean_Cargos.ID_Tipo_Persona = int.Parse(reader_Aereo.GetValue(13).ToString());
                    Arr_Cargos.Add(Bean_Cargos);
                }
                DB.CloseMySQLObj(reader_Aereo, com_Aereo, con_Aereo);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                return null;
            }
            #endregion
        }
        else if (SisID == 3)
        {
            #region Cargar Costos Sistema Terrestre
            MySqlConnection con_Terrestre;
            MySqlCommand com_Terrestre;
            MySqlDataReader reader_Terrestre;
            try
            {
                con_Terrestre = DB.OpenTerrestreConnection();
                com_Terrestre = new MySqlCommand();
                com_Terrestre.Connection = con_Terrestre;
                com_Terrestre.CommandType = CommandType.Text;
                if ((Tipo == 5) || (Tipo == 6) || (Tipo == 7))
                {
                    #region Express-Consolidado-Local
                    com_Terrestre.CommandText = "select ChargeID, SBLID, ItemID, ServiceID, Currency, (Value+ OverSold), InvoiceID, 'Import', DocType, " +
                    "InterChargeType, Local, InterCompanyID, InterGroupID, InterProviderType, Local, PrepaidCollect " +
                    "from ChargeItems " +
                    "where SBLID=" + blID + " and InvoiceID=0 and Expired=0 and InterChargeType=1 and InterProviderType=4; ";
                    #endregion
                }
                reader_Terrestre = com_Terrestre.ExecuteReader();
                while (reader_Terrestre.Read())
                {
                    Bean_Cargos = new Bean_Cargos();
                    Bean_Cargos.Cargo_ID = int.Parse(reader_Terrestre.GetValue(0).ToString());
                    Bean_Cargos.Cargo_BLID = int.Parse(reader_Terrestre.GetValue(1).ToString());
                    Bean_Cargos.Cargo_Rub_ID = int.Parse(reader_Terrestre.GetValue(2).ToString());
                    Bean_Cargos.Cargo_Servicio_ID = int.Parse(reader_Terrestre.GetValue(3).ToString());
                    Bean_Cargos.Cargo_Moneda_Simbolo = reader_Terrestre.GetValue(4).ToString();
                    Bean_Cargos.Cargo_Moneda_ID = Utility.TraducirMonedaStr(Bean_Cargos.Cargo_Moneda_Simbolo);
                    Bean_Cargos.Cargo_Monto = double.Parse(reader_Terrestre.GetValue(5).ToString());
                    Bean_Cargos.Factura_ID = int.Parse(reader_Terrestre.GetValue(6).ToString());
                    Bean_Cargos.Cargo_Tipo_BL = "";
                    Bean_Cargos.Tipo_Documento = int.Parse(reader_Terrestre.GetValue(8).ToString());
                    Bean_Cargos.Tipo_Cargo = int.Parse(reader_Terrestre.GetValue(9).ToString());
                    Bean_Cargos.Tipo_Cobro = int.Parse(reader_Terrestre.GetValue(10).ToString());
                    Bean_Cargos.ID_Intercompany = int.Parse(reader_Terrestre.GetValue(11).ToString());
                    Bean_Cargos.ID_Grupo = int.Parse(reader_Terrestre.GetValue(12).ToString());
                    Bean_Cargos.ID_Tipo_Persona = int.Parse(reader_Terrestre.GetValue(13).ToString());
                    Bean_Cargos.ID_Local_Internacional = int.Parse(reader_Terrestre.GetValue(14).ToString());
                    Bean_Cargos.ID_prepaid_collect = int.Parse(reader_Terrestre.GetValue(15).ToString());
                    Arr_Cargos.Add(Bean_Cargos);
                }
                DB.CloseMySQLObj(reader_Terrestre, com_Terrestre, con_Terrestre);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                return null;
            }
            #endregion
        }
        return Arr_Cargos;
        #endregion
    }
    public static int Insertar_Transacciones_Cuestionario_Reconciliacion_Carga(ArrayList Arr)
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
            foreach (RE_GenericBean Bean in Arr)
            {
                comm.CommandText = "insert into tbl_reconciliacion_carga_transacciones (trt_tipo_bl, trt_tipo_carga, trt_destino_final, trt_ttr_id, trt_tipo_documento, trt_tpi_id, trt_tipo_persona, trt_persona_id, trt_nombre, trt_tts_id, trt_tipo_servicio, trt_rub_id, trt_rubro, trt_conta_id, trt_contabilidad, trt_ttm_id, trt_moneda, trt_monto, trt_concepto, trt_prepaid_collect_id, trt_prepaid_collect, trt_local_internacional_id, trt_local_internacional, trt_tiene_conocimiento_embarque, trt_observaciones, trt_all_in, trt_agente_cobrar, trt_agente_pagar, trt_naviera_cobrar, trt_naviera_pagar, trt_cliente_cobrar, trt_cliente_pagar, trt_intermodal_cobrar, trt_intermodal_pagar, trt_intercompany_destino_cobrar, trt_intercompany_destino_pagar, trt_ingresos, trt_costos, trt_grupo_id, trt_contabilizar, trt_usu_id, trt_trc_id, trt_fecha_creacion, trt_empresa_id, trt_cargo_id, trt_tiene_referencia_adicional, trt_id_routing_adicional, trt_routing_adicional, trt_tto_id_adicional, trt_proveedor_serie, trt_proveedor_correlativo, trt_proveedor_fecha, trt_ttf_id, trt_tra_id, tfa_ordenpo, trt_afecto_excento) ";
                comm.CommandText += "values (@trt_tipo_bl, @trt_tipo_carga, @trt_destino_final, @trt_ttr_id, @trt_tipo_documento, @trt_tpi_id, @trt_tipo_persona, @trt_persona_id, @trt_nombre, @trt_tts_id, @trt_tipo_servicio, @trt_rub_id, @trt_rubro, @trt_conta_id, @trt_contabilidad, @trt_ttm_id, @trt_moneda, @trt_monto, @trt_concepto, @trt_prepaid_collect_id, @trt_prepaid_collect, @trt_local_internacional_id, @trt_local_internacional, @trt_tiene_conocimiento_embarque, @trt_observaciones, @trt_all_in, @trt_agente_cobrar, @trt_agente_pagar, @trt_naviera_cobrar, @trt_naviera_pagar, @trt_cliente_cobrar, @trt_cliente_pagar, @trt_intermodal_cobrar, @trt_intermodal_pagar, @trt_intercompany_destino_cobrar, @trt_intercompany_destino_pagar, @trt_ingresos, @trt_costos, @trt_grupo_id, @trt_contabilizar, @trt_usu_id, @trt_trc_id, @trt_fecha_creacion, @trt_empresa_id, @trt_cargo_id, @trt_tiene_referencia_adicional, @trt_id_routing_adicional, @trt_routing_adicional, @trt_tto_id_adicional, @trt_proveedor_serie, @trt_proveedor_correlativo, @trt_proveedor_fecha, @trt_ttf_id, @trt_tra_id, @tfa_ordenpo, @trt_afecto_excento)";
                comm.Parameters.Add("@trt_tipo_bl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC1;
                comm.Parameters.Add("@trt_tipo_carga", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC2;
                comm.Parameters.Add("@trt_destino_final", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC3;
                comm.Parameters.Add("@trt_ttr_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC4);
                comm.Parameters.Add("@trt_tipo_documento", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC5;
                comm.Parameters.Add("@trt_tpi_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC6);
                comm.Parameters.Add("@trt_tipo_persona", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC7;
                comm.Parameters.Add("@trt_persona_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC8);
                comm.Parameters.Add("@trt_nombre", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC9;
                comm.Parameters.Add("@trt_tts_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC10);
                comm.Parameters.Add("@trt_tipo_servicio", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC11;
                comm.Parameters.Add("@trt_rub_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC12);
                comm.Parameters.Add("@trt_rubro", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC13;
                comm.Parameters.Add("@trt_conta_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC14);
                comm.Parameters.Add("@trt_contabilidad", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC15;
                comm.Parameters.Add("@trt_ttm_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC16);
                comm.Parameters.Add("@trt_moneda", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC17;
                comm.Parameters.Add("@trt_monto", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Convert.ToDouble(Bean.strC18);
                comm.Parameters.Add("@trt_concepto", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC19;
                comm.Parameters.Add("@trt_prepaid_collect_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC20);
                comm.Parameters.Add("@trt_prepaid_collect", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC21;
                comm.Parameters.Add("@trt_local_internacional_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC22);
                comm.Parameters.Add("@trt_local_internacional", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC23;
                comm.Parameters.Add("@trt_tiene_conocimiento_embarque", NpgsqlTypes.NpgsqlDbType.Boolean).Value = Convert.ToBoolean(Bean.strC24);
                comm.Parameters.Add("@trt_observaciones", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC25;
                comm.Parameters.Add("@trt_all_in", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC26;
                comm.Parameters.Add("@trt_agente_cobrar", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Convert.ToDouble(Bean.strC27);
                comm.Parameters.Add("@trt_agente_pagar", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Convert.ToDouble(Bean.strC28);
                comm.Parameters.Add("@trt_naviera_cobrar", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Convert.ToDouble(Bean.strC29);
                comm.Parameters.Add("@trt_naviera_pagar", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Convert.ToDouble(Bean.strC30);
                comm.Parameters.Add("@trt_cliente_cobrar", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Convert.ToDouble(Bean.strC31);
                comm.Parameters.Add("@trt_cliente_pagar", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Convert.ToDouble(Bean.strC32);
                comm.Parameters.Add("@trt_intermodal_cobrar", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Convert.ToDouble(Bean.strC33);
                comm.Parameters.Add("@trt_intermodal_pagar", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Convert.ToDouble(Bean.strC34);
                comm.Parameters.Add("@trt_intercompany_destino_cobrar", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Convert.ToDouble(Bean.strC35);
                comm.Parameters.Add("@trt_intercompany_destino_pagar", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Convert.ToDouble(Bean.strC36);
                comm.Parameters.Add("@trt_ingresos", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Convert.ToDouble(Bean.strC37);
                comm.Parameters.Add("@trt_costos", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Convert.ToDouble(Bean.strC38);
                comm.Parameters.Add("@trt_grupo_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC39);
                comm.Parameters.Add("@trt_contabilizar", NpgsqlTypes.NpgsqlDbType.Boolean).Value = Convert.ToBoolean(Bean.strC40);
                comm.Parameters.Add("@trt_usu_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC41;
                comm.Parameters.Add("@trt_trc_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC42);
                comm.Parameters.Add("@trt_fecha_creacion", NpgsqlTypes.NpgsqlDbType.Timestamp).Value = Bean.strC43;
                #region Definir Valor por Default
                if (Bean.strC44.Trim() == "") { Bean.strC44 = "0"; }
                if (Bean.strC45.Trim() == "") { Bean.strC45 = "0"; }
                if (Bean.strC46.Trim() == "") { Bean.strC46 = "False"; }
                if (Bean.strC47.Trim() == "") { Bean.strC47 = "0"; }
                if (Bean.strC48.Trim() == "") { Bean.strC48 = "-"; }
                if (Bean.strC49.Trim() == "") { Bean.strC49 = "0"; }
                #endregion
                comm.Parameters.Add("@trt_empresa_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC44);
                comm.Parameters.Add("@trt_cargo_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC45);
                comm.Parameters.Add("@trt_tiene_referencia_adicional", NpgsqlTypes.NpgsqlDbType.Boolean).Value = bool.Parse(Bean.strC46);
                comm.Parameters.Add("@trt_id_routing_adicional", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC47);
                comm.Parameters.Add("@trt_routing_adicional", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC48;
                comm.Parameters.Add("@trt_tto_id_adicional", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC49);
                comm.Parameters.Add("@trt_proveedor_serie", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC50;
                comm.Parameters.Add("@trt_proveedor_correlativo", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC51;
                comm.Parameters.Add("@trt_proveedor_fecha", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC52;
                #region Definir Valor por Default
                if (Bean.strC53 == "")
                {
                    Bean.strC53 = "0";
                }
                if (Bean.strC54 == "")
                {
                    Bean.strC54 = "0";
                }
                if (Bean.strC56 == "")
                {
                    Bean.strC56 = "0";
                }
                #endregion
                comm.Parameters.Add("@trt_ttf_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC53);
                comm.Parameters.Add("@trt_tra_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC54);
                comm.Parameters.Add("@tfa_ordenpo", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC55;
                comm.Parameters.Add("@trt_afecto_excento", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(Bean.strC56);
                resultado = comm.ExecuteNonQuery();
                comm.Parameters.Clear();
                comm.CommandText = "";
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
    public static ArrayList Obtener_Preguntas_Respuestas_Reconciliacion_Carga(int ID)
    {
        ArrayList Arr_Result = new ArrayList();
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
            comm.CommandText = "select trs_id, trb_id, trc_id, trb_bl, trc_pregunta, trc_respuesta, trc_usu_id, to_char(trc_fecha_creacion, 'yyyy-mm-dd'), trc_estado " +
            "from tbl_reconciliacion_carga_sesiones, tbl_reconciliacion_carga_bls, tbl_reconciliacion_carga_cuestionario " +
            "where trs_id=trb_trs_id and trs_estado=1 and trb_estado=1 " +
            "and trb_id=trc_trb_id and trc_estado=1 " +
            "and trs_id=" + ID + " order by 3 asc ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.strC1 = reader.GetValue(0).ToString();//trs_id
                Bean.strC2 = reader.GetValue(1).ToString();//trb_id
                Bean.strC3 = reader.GetValue(2).ToString();//trc_id
                Bean.strC4 = reader.GetValue(3).ToString();//trb_bl
                Bean.strC5 = reader.GetValue(4).ToString();//trc_pregunta
                Bean.strC6 = reader.GetValue(5).ToString();//trc_respuesta
                Bean.strC7 = reader.GetValue(6).ToString();//trc_usu_id
                Bean.strC8 = reader.GetValue(7).ToString();//trc_fecha_creacion
                Bean.strC9 = reader.GetValue(8).ToString();//trc_estado
                Arr_Result.Add(Bean);
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Arr_Result;
    }
    public static ArrayList Obtener_Detalle_Transacciones_Reconciliacion_Carga(int ID)
    {
        ArrayList Arr_Result = new ArrayList();
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
            comm.CommandText = "select  trs_id, trb_id, trc_id, trt_id, " +
            "trt_tipo_bl, trt_tipo_carga, trt_destino_final, " +
            "trt_ttr_id, trt_tipo_documento, trt_tpi_id, trt_tipo_persona, trt_persona_id, trt_nombre, " +
            "trt_tts_id, trt_tipo_servicio, trt_rub_id, trt_rubro, trt_conta_id, trt_contabilidad, trt_ttm_id, trt_moneda, trt_monto, trt_concepto, trt_observaciones, " +
            "trt_agente_cobrar, trt_agente_pagar, trt_naviera_cobrar, trt_naviera_pagar, trt_cliente_cobrar, trt_cliente_pagar, " +
            "trt_intermodal_cobrar, trt_intermodal_pagar, trt_intercompany_destino_cobrar, trt_intercompany_destino_pagar, trt_ingresos, trt_costos, " +
            "trt_prepaid_collect_id, trt_prepaid_collect, trt_local_internacional_id, trt_local_internacional, trt_tiene_conocimiento_embarque, trt_all_in, " +
            "trt_grupo_id, trt_contabilizar, trt_fecha_creacion, trt_usu_id, trt_trc_id, trt_estado, trb_bl,  " +
            "trt_empresa_id, trt_cargo_id, trt_tiene_referencia_adicional, trt_id_routing_adicional, trt_routing_adicional, trt_tto_id_adicional " +
            "from tbl_reconciliacion_carga_sesiones, tbl_reconciliacion_carga_bls, tbl_reconciliacion_carga_cuestionario, tbl_reconciliacion_carga_transacciones " +
            "where trs_id=trb_trs_id and trs_estado=1 and trb_estado=1 " +
            "and trb_id=trc_trb_id and trc_estado=1 " +
            "and trc_id=trt_trc_id and trt_estado=1 " +
            "and trs_id=" + ID + " order by 4 asc ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.strC1 = reader.GetValue(0).ToString();//trs_id
                Bean.strC2 = reader.GetValue(1).ToString();//trb_id
                Bean.strC3 = reader.GetValue(2).ToString();//trc_id
                Bean.strC4 = reader.GetValue(3).ToString();//trt_id
                Bean.strC5 = reader.GetValue(4).ToString();//trt_tipo_bl
                Bean.strC6 = reader.GetValue(5).ToString();//trt_tipo_carga
                Bean.strC7 = reader.GetValue(6).ToString();//trt_destino_final
                Bean.strC8 = reader.GetValue(7).ToString();//trt_ttr_id
                Bean.strC9 = reader.GetValue(8).ToString();//trt_tipo_documento
                Bean.strC10 = reader.GetValue(9).ToString();//trt_tpi_id
                Bean.strC11 = reader.GetValue(10).ToString();//trt_tipo_persona
                Bean.strC12 = reader.GetValue(11).ToString();//trt_persona_id
                Bean.strC13 = reader.GetValue(12).ToString();//trt_nombre
                Bean.strC14 = reader.GetValue(13).ToString();//trt_tts_id
                Bean.strC15 = reader.GetValue(14).ToString();//trt_tipo_servicio
                Bean.strC16 = reader.GetValue(15).ToString();//trt_rub_id
                Bean.strC17 = reader.GetValue(16).ToString();//trt_rubro
                Bean.strC18 = reader.GetValue(17).ToString();//trt_conta_id
                Bean.strC19 = reader.GetValue(18).ToString();//trt_contabilidad
                Bean.strC20 = reader.GetValue(19).ToString();//trt_ttm_id
                Bean.strC21 = reader.GetValue(20).ToString();//trt_moneda
                Bean.strC22 = reader.GetValue(21).ToString();//trt_monto
                Bean.strC23 = reader.GetValue(22).ToString();//trt_concepto
                Bean.strC24 = reader.GetValue(23).ToString();//trt_observaciones
                Bean.strC25 = reader.GetValue(24).ToString();//trt_agente_cobrar
                Bean.strC26 = reader.GetValue(25).ToString();//trt_agente_pagar
                Bean.strC27 = reader.GetValue(26).ToString();//trt_naviera_cobrar
                Bean.strC28 = reader.GetValue(27).ToString();//trt_naviera_pagar
                Bean.strC29 = reader.GetValue(28).ToString();//trt_cliente_cobrar
                Bean.strC30 = reader.GetValue(29).ToString();//trt_cliente_pagar
                Bean.strC31 = reader.GetValue(30).ToString();//trt_intermodal_cobrar
                Bean.strC32 = reader.GetValue(31).ToString();//trt_intermodal_pagar
                Bean.strC33 = reader.GetValue(32).ToString();//trt_intercompany_destino_cobrar
                Bean.strC34 = reader.GetValue(33).ToString();//trt_intercompany_destino_pagar
                Bean.strC35 = reader.GetValue(34).ToString();//trt_ingresos
                Bean.strC36 = reader.GetValue(35).ToString();//trt_costos
                Bean.strC37 = reader.GetValue(36).ToString();//trt_prepaid_collect_id
                Bean.strC38 = reader.GetValue(37).ToString();//trt_prepaid_collect
                Bean.strC39 = reader.GetValue(38).ToString();//trt_local_internacional_id
                Bean.strC40 = reader.GetValue(39).ToString();//trt_local_internacional
                Bean.strC41 = reader.GetValue(40).ToString();//trt_tiene_conocimiento_embarque
                Bean.strC42 = reader.GetValue(41).ToString();//trt_all_in
                Bean.strC43 = reader.GetValue(42).ToString();//trt_grupo_id
                Bean.strC44 = reader.GetValue(43).ToString();//trt_contabilizar
                Bean.strC45 = reader.GetValue(44).ToString();//trt_fecha_creacion
                Bean.strC46 = reader.GetValue(45).ToString();//trt_usu_id
                Bean.strC47 = reader.GetValue(46).ToString();//trt_trc_id
                Bean.strC48 = reader.GetValue(47).ToString();//trt_estado
                Bean.strC49 = reader.GetValue(48).ToString();//trb_bl
                Bean.strC50 = reader.GetValue(49).ToString();//trt_empresa_id
                Bean.strC51 = reader.GetValue(50).ToString();//trt_cargo_id
                Bean.strC52 = reader.GetValue(51).ToString();//trt_tiene_referencia_adicional
                Bean.strC53 = reader.GetValue(52).ToString();//trt_id_routing_adicional
                Bean.strC54 = reader.GetValue(53).ToString();//trt_routing_adicional
                Bean.strC55 = reader.GetValue(54).ToString();//trt_tto_id_adicional

                Arr_Result.Add(Bean);
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Arr_Result;
    }
    public static ArrayList Obtener_Transacciones_Intercompany_Pendientes_Notificar()
    {
        ArrayList Arr_Result = new ArrayList();
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
            comm.CommandText = "select distinct on (ttel_padre_ttr_id, ttel_padre_ref_id) ttel_id, ttel_padre_empresa_id, ttel_padre_ttr_id, ttel_padre_ref_id, ttel_tta_id, ttel_fecha_emision, ttel_fecha_anulacion " +
            "from tbl_transacciones_encadenadas_log " +
            "where ttel_estado=1 and ttel_estado_notificacion=0 ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.strC1 = reader.GetValue(0).ToString();//ttel_id
                Bean.strC2 = reader.GetValue(1).ToString();//ttel_padre_empresa_id
                Bean.strC3 = reader.GetValue(2).ToString();//ttel_padre_ttr_id
                Bean.strC4 = reader.GetValue(3).ToString();//ttel_padre_ref_id
                Bean.strC5 = reader.GetValue(4).ToString();//ttel_tta_id
                Bean.strC6 = reader.GetValue(5).ToString();//ttel_fecha_emision
                Bean.strC7 = reader.GetValue(6).ToString();//ttel_fecha_anulacion
                Arr_Result.Add(Bean);
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Arr_Result;
    }
    public static int Actualizar_Estado_Notificacion_Intercompany(int ttrID_Padre, int refID_Padre)
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
            comm.CommandText = "update tbl_transacciones_encadenadas_log set ttel_estado_notificacion=1, ttel_fecha_notificacion=now() where ttel_padre_ttr_id=" + ttrID_Padre + " and ttel_padre_ref_id=" + refID_Padre + " ";
            resultado = comm.ExecuteNonQuery();
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
    public static ArrayList Get_Tipo_Tarifa_Contabilizacion_Automatica()
    {
        ArrayList result = new ArrayList();
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
            comm.CommandText = "select  trctt_id, trctt_tipo, trctt_fecha_creacion, trctt_unidad_medida from tbl_reconciliacion_carga_tipo_tarifa where trctt_estado=1";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.strC1 = reader.GetValue(0).ToString();//trctt_id
                Bean.strC2 = reader.GetValue(1).ToString();//trctt_tipo
                Bean.strC3 = reader.GetValue(2).ToString();//trctt_fecha_creacion
                Bean.strC4 = reader.GetValue(3).ToString();//trctt_unidad_medida
                result.Add(Bean);
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return result;
    }
    public static int Insertar_Tarifa_Contabilizacion_Automatica(RE_GenericBean Bean)
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
            comm.CommandText = "insert into tbl_reconciliacion_carga_tarifario (trct_empresa_id, trct_tpi_id, trct_persona_id, trct_ttm_id, trct_tarifa_base, trct_tarifa_adicional, trct_trctt_id, trct_usu_id_creacion, trct_linea_servicio) ";
            comm.CommandText += " values (@trct_empresa_id, @trct_tpi_id, @trct_persona_id, @trct_ttm_id, @trct_tarifa_base, @trct_tarifa_adicional, @trct_trctt_id, @trct_usu_id_creacion, @trct_linea_servicio)";
            comm.Parameters.Add("@trct_empresa_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC1;
            comm.Parameters.Add("@trct_tpi_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC2;
            comm.Parameters.Add("@trct_persona_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC3;
            comm.Parameters.Add("@trct_ttm_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC4;
            comm.Parameters.Add("@trct_tarifa_base", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Bean.douC1;
            comm.Parameters.Add("@trct_tarifa_adicional", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Bean.douC2;
            comm.Parameters.Add("@trct_trctt_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC5;
            comm.Parameters.Add("@trct_usu_id_creacion", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC1;
            comm.Parameters.Add("@trct_linea_servicio", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC6;
            resultado = comm.ExecuteNonQuery();
            comm.Parameters.Clear();
            comm.CommandText = "";
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
    public static ArrayList Get_Tarifas_Contabilizacion_Automatica_Por_Criterio(string sql)
    {
        ArrayList result = new ArrayList();
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
            comm.CommandText = "select trct_id, trct_empresa_id, trct_tpi_id, trct_persona_id, trct_ttm_id, trct_tarifa_base, trct_tarifa_adicional, trct_trctt_id, trct_usu_id_creacion, trct_fecha_creacion, " +
            "pai_nombre_sistema, upper(tpi_nombre), substr(ttm_nombre,1,3), upper(trctt_tipo||' - '||trctt_unidad_medida), trct_linea_servicio, tto_nombre " +
            "from tbl_reconciliacion_carga_tarifario " +
            "INNER JOIN tbl_pais on (trct_empresa_id=pai_id) " +
            "LEFT JOIN tbl_tipo_persona on (trct_tpi_id=tpi_id) " +
            "LEFT JOIN tbl_tipo_moneda on (trct_ttm_id=ttm_id) " +
            "INNER JOIN tbl_reconciliacion_carga_tipo_tarifa on (trct_trctt_id=trctt_id) " +
            "LEFT JOIN tbl_tipo_operacion on (trct_linea_servicio=tto_id) " +
            "where trct_estado=1 ";
            comm.CommandText += sql;
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.strC1 = reader.GetValue(0).ToString();//trct_id
                Bean.strC2 = reader.GetValue(1).ToString();//trct_empresa_id
                Bean.strC3 = reader.GetValue(2).ToString();//trct_tpi_id
                Bean.strC4 = reader.GetValue(3).ToString();//trct_persona_id
                Bean.strC5 = reader.GetValue(4).ToString();//trct_ttm_id
                Bean.strC6 = reader.GetValue(5).ToString();//trct_tarifa_base
                Bean.strC7 = reader.GetValue(6).ToString();//trct_tarifa_adicional
                Bean.strC8 = reader.GetValue(7).ToString();//trct_trctt_id
                Bean.strC9 = reader.GetValue(8).ToString();//trct_usu_id_creacion
                Bean.strC10 = reader.GetValue(9).ToString();//trct_fecha_creacion
                Bean.strC11 = reader.GetValue(10).ToString();//NOMBRE EMPRESA
                Bean.strC12 = reader.GetValue(11).ToString();//TIPO DE PERSONA
                Bean.strC13 = "";//NOMBRE DE LA PERSONA
                #region Obtener Nombre
                string Nombre = "";
                RE_GenericBean Bean_Aux = null;
                if (Bean.strC3 == "2")
                {
                    Bean_Aux = DB.getAgenteData(int.Parse(Bean.strC4), "");
                    Nombre = Bean_Aux.strC1;
                }
                else if (Bean.strC3 == "4")
                {
                    Bean_Aux = DB.getProveedorData(int.Parse(Bean.strC4), "");
                    Nombre = Bean_Aux.strC1;
                }
                else if (Bean.strC3 == "5")
                {
                    Bean_Aux = DB.getNavieraData(int.Parse(Bean.strC4));
                    Nombre = Bean_Aux.strC1;
                }
                else if (Bean.strC3 == "6")
                {
                    Bean_Aux = DB.getCarriersData(int.Parse(Bean.strC4));
                    Nombre = Bean_Aux.strC1;
                }
                Bean.strC13 = Nombre;
                #endregion
                Bean.strC14 = reader.GetValue(12).ToString();//TIPO MONEDA
                Bean.strC15 = reader.GetValue(13).ToString();//TIPO TARIFA
                Bean.strC16 = reader.GetValue(14).ToString();//trct_linea_servicio ID
                Bean.strC17 = reader.GetValue(15).ToString();//trct_linea_servicio
                result.Add(Bean);
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return result;
    }
    public static int Eliminar_Tarifa_Contabilizacion_Automatica(UsuarioBean user, int ID)
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
            comm.CommandText = "update tbl_reconciliacion_carga_tarifario set trct_usu_id_eliminacion='" + user.ID + "', trct_fecha_eliminacion=now(), trct_estado=0 where trct_id=" + ID + " ";
            resultado = comm.ExecuteNonQuery();
            comm.Parameters.Clear();
            comm.CommandText = "";
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
    public static int Validar_Existencia_Tarifa_Contabilizacion_Automatica(int Empresa_ID, int Tipo_Persona_ID, int Persona_ID, int Tipo_Tarifa_ID, int ttoID)
    {
        int resultado = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            if ((Tipo_Tarifa_ID == 3) || (Tipo_Tarifa_ID == 8) || (Tipo_Tarifa_ID == 9))
            {
                //REBATES
                comm.CommandText = "select count(trct_id) from tbl_reconciliacion_carga_tarifario where trct_empresa_id=" + Empresa_ID + " and trct_tpi_id=" + Tipo_Persona_ID + " and trct_persona_id=" + Persona_ID + " and trct_trctt_id in (3,8,9) and trct_linea_servicio in (" + ttoID + ") and trct_estado=1";
            }
            else if ((Tipo_Tarifa_ID == 2) || (Tipo_Tarifa_ID == 5))
            {
                //INLAD E INTERMODAL
                comm.CommandText = "select count(trct_id) from tbl_reconciliacion_carga_tarifario where trct_empresa_id=" + Empresa_ID + " and trct_tpi_id=" + Tipo_Persona_ID + " and trct_persona_id=" + Persona_ID + " and trct_trctt_id in (" + Tipo_Tarifa_ID + ") and trct_linea_servicio in (" + ttoID + ") and trct_estado=1";
            }   
            else
            {
                comm.CommandText = "select count(trct_id) from tbl_reconciliacion_carga_tarifario where trct_empresa_id=" + Empresa_ID + " and trct_tpi_id=" + Tipo_Persona_ID + " and trct_persona_id=" + Persona_ID + " and trct_trctt_id=" + Tipo_Tarifa_ID + " and trct_estado=1";
            }
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                resultado = int.Parse(reader.GetValue(0).ToString());
            }
            comm.Parameters.Clear();
            comm.CommandText = "";
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return resultado;
    }
    public static int Insertar_Posicion_Contabilizacion_Automatica(RE_GenericBean Bean)
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
            comm.CommandText = "insert into tbl_reconciliacion_carga_posicion (trcp_trs_id, trcp_trb_id, trcp_objeto, trcp_usu_id, trcp_tipo_bl) ";
            comm.CommandText += " values (@trcp_trs_id, @trcp_trb_id, @trcp_objeto, @trcp_usu_id, @trcp_tipo_bl)";
            comm.Parameters.Add("@trcp_trs_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC1;
            comm.Parameters.Add("@trcp_trb_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC2;
            comm.Parameters.Add("@trcp_objeto", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC1;
            comm.Parameters.Add("@trcp_usu_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC2;
            comm.Parameters.Add("@trcp_tipo_bl", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC3;
            resultado = comm.ExecuteNonQuery();
            comm.Parameters.Clear();
            comm.CommandText = "";
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
    public static RE_GenericBean Obtener_Posicion_Contabilizacion_Automatica(int sesionID)
    {
        RE_GenericBean Bean_Result = new RE_GenericBean();
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            #region Definir Session Inicial
            Bean_Result.strC1 = "0";
            Bean_Result.strC2 = "0";
            Bean_Result.strC3 = "0";
            Bean_Result.strC4 = "pnl_pregunta1_1";
            Bean_Result.strC5 = "System";
            Bean_Result.strC6 = "-";
            #endregion
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select trcp_id, trcp_trs_id, trcp_trb_id, trcp_objeto, trcp_usu_id, trcp_fecha_creacion " +
            "from tbl_reconciliacion_carga_posicion where trcp_trs_id=" + sesionID + " and trcp_estado=1";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean_Result = new RE_GenericBean();
                Bean_Result.strC1 = reader.GetValue(0).ToString();//trcp_id
                Bean_Result.strC2 = reader.GetValue(1).ToString();//trcp_trs_id
                Bean_Result.strC3 = reader.GetValue(2).ToString();//trcp_trb_id
                Bean_Result.strC4 = reader.GetValue(3).ToString();//trcp_objeto
                Bean_Result.strC5 = reader.GetValue(4).ToString();//trcp_usu_id
                Bean_Result.strC6 = reader.GetValue(5).ToString();//trcp_fecha_creacion
            }
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Bean_Result;
    }
    public static RE_GenericBean Get_Tarifa_Contabilizacion_Automatica_Por_Parametros(int empresaID, int tpiID, int personaID, int Tipo_Tarifa)
    {
        RE_GenericBean Bean_Result = new RE_GenericBean();
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select trct_id, trct_empresa_id, trct_tpi_id, trct_persona_id, trct_ttm_id, trct_tarifa_base, trct_tarifa_adicional, trct_trctt_id from tbl_reconciliacion_carga_tarifario where trct_empresa_id=" + empresaID + " and trct_tpi_id=" + tpiID + " and trct_persona_id=" + personaID + " and trct_trctt_id=" + Tipo_Tarifa + " and trct_estado=1 ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean_Result.strC1 = reader.GetValue(0).ToString();//trct_id
                Bean_Result.strC2 = reader.GetValue(1).ToString();//trct_empresa_id
                Bean_Result.strC3 = reader.GetValue(2).ToString();//trct_tpi_id
                Bean_Result.strC4 = reader.GetValue(3).ToString();//trct_persona_id
                Bean_Result.strC5 = reader.GetValue(4).ToString();//trct_ttm_id
                Bean_Result.douC1 = Convert.ToDouble(reader.GetValue(5).ToString());//trct_tarifa_base
                Bean_Result.douC2 = Convert.ToDouble(reader.GetValue(6).ToString());//trct_tarifa_adicional
                Bean_Result.strC6 = reader.GetValue(7).ToString();//trct_trctt_id
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Bean_Result;
    }
    public static RE_GenericBean Get_Tarifa_Contabilizacion_Automatica_Por_SQL(string sql)
    {
        RE_GenericBean Bean_Result = new RE_GenericBean();
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select trct_id, trct_empresa_id, trct_tpi_id, trct_persona_id, trct_ttm_id, trct_tarifa_base, trct_tarifa_adicional, trct_trctt_id from tbl_reconciliacion_carga_tarifario where trct_estado=1 ";
            comm.CommandText += sql;
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean_Result.strC1 = reader.GetValue(0).ToString();//trct_id
                Bean_Result.strC2 = reader.GetValue(1).ToString();//trct_empresa_id
                Bean_Result.strC3 = reader.GetValue(2).ToString();//trct_tpi_id
                Bean_Result.strC4 = reader.GetValue(3).ToString();//trct_persona_id
                Bean_Result.strC5 = reader.GetValue(4).ToString();//trct_ttm_id
                Bean_Result.douC1 = Convert.ToDouble(reader.GetValue(5).ToString());//trct_tarifa_base
                Bean_Result.douC2 = Convert.ToDouble(reader.GetValue(6).ToString());//trct_tarifa_adicional
                Bean_Result.strC6 = reader.GetValue(7).ToString();//trct_trctt_id
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Bean_Result;
    }
    public static double Get_Tarifa_Total_Garantias_Por_Agente(int tpiID, int personaID)
    {
        double result = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select coalesce(sum(trct_tarifa_base+trct_tarifa_adicional),0) from tbl_reconciliacion_carga_tarifario where trct_tpi_id=" + tpiID + " and trct_persona_id=" + personaID + " and trct_trctt_id=7 and trct_estado=1 ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                result = double.Parse(reader.GetValue(0).ToString());
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return result;
    }
    public static RE_GenericBean Get_Posicion_Sesion_Cuestionario_Contabilizacion_Automatica(int sessionID)
    {
        RE_GenericBean Bean_Result = new RE_GenericBean();
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select b.trcp_id, b.trcp_trs_id, b.trcp_trb_id, b.trcp_objeto, b.trcp_tipo_bl, c.trb_estado_bl, b.trcp_usu_id, b.trcp_fecha_creacion " +
            "from tbl_reconciliacion_carga_sesiones a, tbl_reconciliacion_carga_posicion b, tbl_reconciliacion_carga_bls c " +
            "where a.trs_id=b.trcp_trs_id and a.trs_estado=1 and b.trcp_estado=1 " +
            "and c.trb_trs_id=a.trs_id and c.trb_estado=1 " +
            "and b.trcp_trs_id=" + sessionID + " order by b.trcp_fecha_creacion desc limit 1 ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean_Result.strC1 = reader.GetValue(0).ToString();//trcp_id
                Bean_Result.strC2 = reader.GetValue(1).ToString();//trcp_trs_id
                Bean_Result.strC3 = reader.GetValue(2).ToString();//trcp_trb_id
                Bean_Result.strC4 = reader.GetValue(3).ToString();//trcp_objeto
                Bean_Result.strC5 = reader.GetValue(4).ToString();//trcp_tipo_bl
                Bean_Result.strC6 = reader.GetValue(5).ToString();//trb_estado_bl
                Bean_Result.strC7 = reader.GetValue(6).ToString();//trcp_usu_id
                Bean_Result.strC8 = reader.GetValue(7).ToString();//trcp_fecha_creacion
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Bean_Result;
    }
    public static int Actualizar_Estado_Documento_Sesion_Cuestionario_Contabilizacion_Automatica(int trbID, int estado)
    {
        int result = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "update tbl_reconciliacion_carga_bls set trb_estado_bl=" + estado + " where trb_id=" + trbID + " and trb_estado=1";
            result = comm.ExecuteNonQuery();
            DB.CloseObj_insert(comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return result;
    }
    public static string Get_Respuesta_Cuestionario_Contabilizacion_Automatica_X_Pregunta(int trbID, string pregunta)
    {
        string result = "";
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select trc_id, trc_respuesta, trc_usu_id, trc_fecha_creacion " +
            "from tbl_reconciliacion_carga_cuestionario where trc_estado=1 and trc_trb_id=" + trbID + " and trc_pregunta='" + pregunta + "' ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                result = reader.GetValue(1).ToString();
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return "-100";
        }
        return result;
    }
    public static DataSet getRO_TERRESTRE(PaisBean Pais_Bean, string routing, string criterio)
    {
        //Terrestre LTL = 4
        //Terrestre Express LE = 5
        //Terrestre Local LC = 6
        //comm.CommandText += " and a.id_transporte=8 ";

        DataSet ds = new DataSet();
        ds.Tables.Add("bl_list");
        ds.Tables["bl_list"].Columns.Add("ID");
        ds.Tables["bl_list"].Columns.Add("NUMERO_ROUTING");
        ds.Tables["bl_list"].Columns.Add("CLI_ID");
        ds.Tables["bl_list"].Columns.Add("CLIENTE");
        ds.Tables["bl_list"].Columns.Add("ID_FACT");
        ds.Tables["bl_list"].Columns.Add("FACTURAR");
        ds.Tables["bl_list"].Columns.Add("NAVIERA");
        ds.Tables["bl_list"].Columns.Add("AGENTE");
        ds.Tables["bl_list"].Columns.Add("PESO");
        ds.Tables["bl_list"].Columns.Add("VOLUMEN");
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenMasterConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select a.id_routing, a.routing, coalesce(a.id_usuario_creacion), coalesce(a.id_cliente,0), coalesce(a.id_facturar,0), coalesce(a.id_naviera,0), coalesce(a.agente_id,0), coalesce(a.peso, 0),coalesce(a.volumen,0) " +
            "from routings a where routing ilike '%" + routing + "%' and a.id_pais='" + Pais_Bean.ISO + "' and a.routing_secc=0 and a.id_routing_type=2 and a.borrado=false ";
            comm.CommandText += criterio;
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                string Nombre_Cliente = "";
                string Nombre_Facturar = "";
                string Naviera = "";
                string Agente = "";
                string Usuario = "";
                string sql = "";
                sql = "select pw_name from usuarios_empresas where id_usuario=" + reader.GetValue(2).ToString();
                Usuario = DB.getName(sql);
                sql = "select nombre_cliente from clientes where id_cliente=" + reader.GetValue(3).ToString();
                Nombre_Cliente = DB.getName(sql);
                sql = "select nombre_cliente from clientes where id_cliente=" + reader.GetValue(4).ToString();
                Nombre_Facturar = DB.getName(sql);
                sql = "select agente from agentes where activo=true and agente_id=" + reader.GetValue(5).ToString();
                Agente = DB.getName(sql);
                sql = "select nombre from navieras where activo=true and id_naviera=" + reader.GetValue(6).ToString();
                Naviera = DB.getName(sql);
                object[] objArr = { reader.GetValue(0).ToString(), reader.GetValue(1).ToString(), reader.GetValue(3).ToString(), Nombre_Cliente, reader.GetValue(4).ToString(), Nombre_Facturar, Naviera, Agente, reader.GetValue(7).ToString(), reader.GetValue(8).ToString() };
                ds.Tables["bl_list"].Rows.Add(objArr);
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return ds;
    }
    public static DataSet getRO_Aduanas(PaisBean Pais_Bean, string routing)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("bl_list");
        ds.Tables["bl_list"].Columns.Add("bl_house");
        ds.Tables["bl_list"].Columns.Add("bl_master");
        ds.Tables["bl_list"].Columns.Add("NUMERO_ROUTING");
        ds.Tables["bl_list"].Columns.Add("Contenedor");
        ds.Tables["bl_list"].Columns.Add("tipo_operacion");
        ds.Tables["bl_list"].Columns.Add("ROID");
        ds.Tables["bl_list"].Columns.Add("CLI_ID");
        ds.Tables["bl_list"].Columns.Add("CLIENTE");
        ds.Tables["bl_list"].Columns.Add("ID_FACT");
        ds.Tables["bl_list"].Columns.Add("FACTURAR");
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenMasterConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select a.id_routing, 'HBL', 'MBL', a.routing, coalesce(a.id_usuario_creacion), coalesce(a.id_shipper,0), coalesce(a.id_cliente,0), coalesce(a.id_naviera,0), coalesce(a.agente_id,0), 'VAPOR', 'CONTENEDOR', coalesce(a.peso, 0),coalesce(a.volumen,0), coalesce(a.comodity_id,0), coalesce(a.no_piezas), coalesce(a.id_tipo_paquete), 13, coalesce(a.id_usuario_creacion), coalesce(a.id_cliente,0), coalesce(a.id_facturar,0) " +
            "from routings a where routing ilike '%" + routing + "%' and a.id_pais='" + Pais_Bean.ISO + "' and a.routing_secc=0 and a.id_routing_type=2 and a.id_transporte=8 and a.borrado=false ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                string Nombre_Cliente = "";
                string Nombre_Facturar = "";
                string sql = "";
                sql = "select nombre_cliente from clientes where id_cliente=" + reader.GetValue(18).ToString();
                Nombre_Cliente = DB.getName(sql);
                sql = "select nombre_cliente from clientes where id_cliente=" + reader.GetValue(19).ToString();
                Nombre_Facturar = DB.getName(sql);
                object[] objArr = { reader.GetValue(3).ToString(), "", reader.GetValue(3).ToString(), "", reader.GetValue(16).ToString(), reader.GetValue(0).ToString(), reader.GetValue(18).ToString(), Nombre_Cliente, reader.GetValue(19).ToString(), Nombre_Facturar };
                ds.Tables["bl_list"].Rows.Add(objArr);
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return ds;
    }
    public static ArrayList getRubrosbyRO_ADUANAS(int routingID)
    {
        ArrayList result = new ArrayList();
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenMasterConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select  a.id_rubro, (a.valor + a.sobreventa), a.id_moneda, a.id_servicio, a.id_routing, a.local, a.factura_id, a.tipo_documento, a.id_cargos_routing, a.observacion " +
            "from cargos_routing a where a.id_routing=" + routingID + " and a.activo=true and a.factura_id=0 ";
            reader = comm.ExecuteReader();
            Rubros rubbean = null;
            while (reader.Read())
            {
                rubbean = new Rubros();
                if (!reader.IsDBNull(0)) rubbean.rubroID = reader.GetInt64(0);
                if (!reader.IsDBNull(1)) rubbean.rubroTot = double.Parse(reader.GetValue(1).ToString());
                rubbean.rubroName = DB.getRubro(int.Parse(rubbean.rubroID.ToString())).strC1;
                if (!reader.IsDBNull(2))
                {
                    if ((reader.GetInt64(2) == 1) || (reader.GetInt64(2) == 4) || (reader.GetInt64(2) == 5) || (reader.GetInt64(2) == 7) || (reader.GetInt64(2) == 9) || (reader.GetInt64(2) == 12) || (reader.GetInt64(2) == 14) || (reader.GetInt64(2) == 17) || (reader.GetInt64(2) == 19) || (reader.GetInt64(2) == 20)) rubbean.rubroMoneda = 8;
                    else if ((reader.GetInt64(2) == 2) || (reader.GetInt64(2) == 16) || (reader.GetInt64(2) == 18)) { rubbean.rubroMoneda = 1; }
                    else if (reader.GetInt64(2) == 6) { rubbean.rubroMoneda = 3; }
                    else if ((reader.GetInt64(2) == 8) || (reader.GetInt64(2) == 21)) { rubbean.rubroMoneda = 4; }
                    else if (reader.GetInt64(2) == 10) { rubbean.rubroMoneda = 5; }
                    else if (reader.GetInt64(2) == 13) { rubbean.rubroMoneda = 7; }
                }
                if (!reader.IsDBNull(3)) rubbean.rubroTypeID = int.Parse(reader.GetValue(3).ToString());
                if (!reader.IsDBNull(3)) rubbean.rubtoType = Utility.TraducirServiciotoSTR(int.Parse(reader.GetValue(3).ToString()));
                if (!reader.IsDBNull(4)) rubbean.rubroBlID = int.Parse(reader.GetValue(4).ToString());
                if (!reader.IsDBNull(5))
                {
                    if (reader.GetValue(5).ToString() == "True")
                    {
                        rubbean.rubroTipoCargo = 1;//Local = True = 1
                    }
                    else if (reader.GetValue(5).ToString() == "False")
                    {
                        rubbean.rubroTipoCargo = 0;//Internacional = False = 0
                    }
                }
                if (!reader.IsDBNull(4)) rubbean.rubroRoutingID = int.Parse(reader.GetValue(4).ToString());
                if (!reader.IsDBNull(6)) rubbean.rubroFacturaID = int.Parse(reader.GetValue(6).ToString());
                if (!reader.IsDBNull(7)) rubbean.rubroTipoDocumento = int.Parse(reader.GetValue(7).ToString());//Factura o Nota de Debito
                if (!reader.IsDBNull(8)) rubbean.rubroCargoID = double.Parse(reader.GetValue(8).ToString());//CargoID
                if (!reader.IsDBNull(9)) rubbean.rubroCommentario = reader.GetValue(9).ToString();//Observacion
                if (rubbean.rubroTot > 0)
                {
                    result.Add(rubbean);
                }
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return result;
    }
    public static DataSet getRO_Seguros(PaisBean Pais_Bean, string routing)
    {
        DataSet ds = new DataSet();
        ds.Tables.Add("bl_list");
        ds.Tables["bl_list"].Columns.Add("bl_house");
        ds.Tables["bl_list"].Columns.Add("bl_master");
        ds.Tables["bl_list"].Columns.Add("NUMERO_ROUTING");
        ds.Tables["bl_list"].Columns.Add("Contenedor");
        ds.Tables["bl_list"].Columns.Add("tipo_operacion");
        ds.Tables["bl_list"].Columns.Add("ROID");
        ds.Tables["bl_list"].Columns.Add("CLI_ID");
        ds.Tables["bl_list"].Columns.Add("CLIENTE");
        ds.Tables["bl_list"].Columns.Add("ID_FACT");
        ds.Tables["bl_list"].Columns.Add("FACTURAR");
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenMasterConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select a.id_routing, 'HBL', 'MBL', a.routing, coalesce(a.id_usuario_creacion), coalesce(a.id_shipper,0), coalesce(a.id_cliente,0), coalesce(a.id_naviera,0), coalesce(a.agente_id,0), 'VAPOR', 'CONTENEDOR', coalesce(a.peso, 0),coalesce(a.volumen,0), coalesce(a.comodity_id,0), coalesce(a.no_piezas), coalesce(a.id_tipo_paquete), 13, coalesce(a.id_usuario_creacion), coalesce(a.id_cliente,0), coalesce(a.id_facturar,0) " +
            "from routings a where routing ilike '%" + routing + "%' and a.id_pais='" + Pais_Bean.ISO + "' and a.routing_secc=0 and a.id_routing_type=2 and a.id_transporte=9 and a.borrado=false ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                string Nombre_Cliente = "";
                string Nombre_Facturar = "";
                string sql = "";
                sql = "select nombre_cliente from clientes where id_cliente=" + reader.GetValue(18).ToString();
                Nombre_Cliente = DB.getName(sql);
                sql = "select nombre_cliente from clientes where id_cliente=" + reader.GetValue(19).ToString();
                Nombre_Facturar = DB.getName(sql);
                object[] objArr = { reader.GetValue(3).ToString(), "", reader.GetValue(3).ToString(), "", reader.GetValue(16).ToString(), reader.GetValue(0).ToString(), reader.GetValue(18).ToString(), Nombre_Cliente, reader.GetValue(19).ToString(), Nombre_Facturar };
                ds.Tables["bl_list"].Rows.Add(objArr);
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return ds;
    }
    public static ArrayList getRubrosbyRO_SEGUROS(int routingID)
    {
        ArrayList result = new ArrayList();
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenMasterConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select  a.id_rubro, (a.valor + a.sobreventa), a.id_moneda, a.id_servicio, a.id_routing, a.local, a.factura_id, a.tipo_documento, a.id_cargos_routing, a.observacion " +
            "from cargos_routing a where a.id_routing=" + routingID + " and a.activo=true and a.factura_id=0 ";
            reader = comm.ExecuteReader();
            Rubros rubbean = null;
            while (reader.Read())
            {
                rubbean = new Rubros();
                if (!reader.IsDBNull(0)) rubbean.rubroID = reader.GetInt64(0);
                if (!reader.IsDBNull(1)) rubbean.rubroTot = double.Parse(reader.GetValue(1).ToString());
                rubbean.rubroName = DB.getRubro(int.Parse(rubbean.rubroID.ToString())).strC1;
                if (!reader.IsDBNull(2))
                {
                    if ((reader.GetInt64(2) == 1) || (reader.GetInt64(2) == 4) || (reader.GetInt64(2) == 5) || (reader.GetInt64(2) == 7) || (reader.GetInt64(2) == 9) || (reader.GetInt64(2) == 12) || (reader.GetInt64(2) == 14) || (reader.GetInt64(2) == 17) || (reader.GetInt64(2) == 19) || (reader.GetInt64(2) == 20)) rubbean.rubroMoneda = 8;
                    else if ((reader.GetInt64(2) == 2) || (reader.GetInt64(2) == 16) || (reader.GetInt64(2) == 18)) { rubbean.rubroMoneda = 1; }
                    else if (reader.GetInt64(2) == 6) { rubbean.rubroMoneda = 3; }
                    else if ((reader.GetInt64(2) == 8) || (reader.GetInt64(2) == 21)) { rubbean.rubroMoneda = 4; }
                    else if (reader.GetInt64(2) == 10) { rubbean.rubroMoneda = 5; }
                    else if (reader.GetInt64(2) == 13) { rubbean.rubroMoneda = 7; }
                }
                if (!reader.IsDBNull(3)) rubbean.rubroTypeID = int.Parse(reader.GetValue(3).ToString());
                if (!reader.IsDBNull(3)) rubbean.rubtoType = Utility.TraducirServiciotoSTR(int.Parse(reader.GetValue(3).ToString()));
                if (!reader.IsDBNull(4)) rubbean.rubroBlID = int.Parse(reader.GetValue(4).ToString());
                if (!reader.IsDBNull(5))
                {
                    if (reader.GetValue(5).ToString() == "True")
                    {
                        rubbean.rubroTipoCargo = 1;//Local = True = 1
                    }
                    else if (reader.GetValue(5).ToString() == "False")
                    {
                        rubbean.rubroTipoCargo = 0;//Internacional = False = 0
                    }
                }
                if (!reader.IsDBNull(4)) rubbean.rubroRoutingID = int.Parse(reader.GetValue(4).ToString());
                if (!reader.IsDBNull(6)) rubbean.rubroFacturaID = int.Parse(reader.GetValue(6).ToString());
                if (!reader.IsDBNull(7)) rubbean.rubroTipoDocumento = int.Parse(reader.GetValue(7).ToString());//Factura o Nota de Debito
                if (!reader.IsDBNull(8)) rubbean.rubroCargoID = double.Parse(reader.GetValue(8).ToString());//CargoID
                if (!reader.IsDBNull(9)) rubbean.rubroCommentario = reader.GetValue(9).ToString();//Observacion
                if (rubbean.rubroTot > 0)
                {
                    result.Add(rubbean);
                }
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return result;
    }
    public static int ReIniciar_Cuestionario_Reconciliacion_Carga(int sesionID, string usuario)
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
            comm.CommandText = "";
            //Inactivar Ultima Posicion
            comm.CommandText = "update tbl_reconciliacion_carga_posicion set trcp_estado=0 where trcp_trs_id=" + sesionID + " and trcp_estado=1 ";
            resultado = comm.ExecuteNonQuery();
            comm.Parameters.Clear();
            comm.CommandText = "";
            //Inactivar Transacciones
            comm.CommandText = "update tbl_reconciliacion_carga_transacciones set trt_estado=0 where trt_estado=1 and trt_trc_id in " +
            "(select c.trc_id from tbl_reconciliacion_carga_sesiones a, tbl_reconciliacion_carga_bls b, tbl_reconciliacion_carga_cuestionario c, tbl_reconciliacion_carga_transacciones d " +
            "where a.trs_id=b.trb_trs_id and b.trb_id=c.trc_trb_id and c.trc_id=d.trt_trc_id " +
            "and a.trs_estado=1 and b.trb_estado=1 and c.trc_estado=1 and d.trt_estado=1 " +
            "and a.trs_id=" + sesionID + ") ";
            resultado = comm.ExecuteNonQuery();
            comm.Parameters.Clear();
            comm.CommandText = "";
            //Inactivar Cuestionario
            comm.CommandText = "update tbl_reconciliacion_carga_cuestionario set trc_estado=0, trc_usu_eliminacion_id='" + usuario + "', trc_fecha_eliminacion=now() where trc_estado=1 and trc_trb_id in " +
            "(select b.trb_id from tbl_reconciliacion_carga_sesiones a, tbl_reconciliacion_carga_bls b " +
            "where a.trs_id=b.trb_trs_id " +
            "and a.trs_estado=1 and b.trb_estado=1 " +
            "and a.trs_id=" + sesionID + ") ";
            resultado = comm.ExecuteNonQuery();
            comm.Parameters.Clear();
            comm.CommandText = "";
            //Reestablecer estado de BL's
            //comm.CommandText = "update tbl_reconciliacion_carga_bls set trb_estado_bl=1 where trb_trs_id=" + sesionID + " and trb_estado=1";
            //InActivar Listado y Detalle de BL's importar nuevamente la informacion de Trafico para que este actualizada y los Houses regresen a estado=1
            comm.CommandText = "update tbl_reconciliacion_carga_bls set trb_estado=0 where trb_trs_id=" + sesionID + " and trb_estado=1";
            resultado = comm.ExecuteNonQuery();
            comm.Parameters.Clear();
            comm.CommandText = "";
            //Reestablecer Numero de Grupo de la Sesion al # 1
            //Se comenta para que la Sesion sea InActivada para que genere un nuevo numero de Sesion y se importe nuevamente la informacion de Trafico
            //comm.CommandText = "update tbl_reconciliacion_carga_sesiones set trs_numero_grupo=1 where trs_id=" + sesionID + " and trs_estado=1";
            //resultado = comm.ExecuteNonQuery();
            //comm.Parameters.Clear();
            //comm.CommandText = "";
            //Reestablecer Estado de la Sesion al # 1
            //Se comenta para que la Sesion sea InActivada para que genere un nuevo numero de Sesion y se importe nuevamente la informacion de Trafico
            //comm.CommandText = "update tbl_reconciliacion_carga_sesiones set trs_estado_sesion=1 where trs_id=" + sesionID + " and trs_estado=1";
            //resultado = comm.ExecuteNonQuery();
            //comm.Parameters.Clear();
            //comm.CommandText = "";
            //InActivar Sesion
            comm.CommandText = "update tbl_reconciliacion_carga_sesiones set trs_estado=0 where trs_id=" + sesionID + " and trs_estado=1";
            resultado = comm.ExecuteNonQuery();
            comm.Parameters.Clear();
            comm.CommandText = "";
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
    public static ArrayList Buscar_Sesiones_Contabilizacion_Automatica(string sql)
    {
        ArrayList result = new ArrayList();
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
            comm.CommandText = "select trs_id, trs_empresa, trs_sistema, trs_tipo_operacion, trs_bl, trs_imp_exp, trs_usu_id, to_char(trs_fecha_creacion, 'yyyy-mm-dd'), trs_viaje_no, trs_estado_sesion " +
            "from tbl_reconciliacion_carga_sesiones where trs_estado=1 ";
            comm.CommandText += sql;
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.strC1 = reader.GetValue(0).ToString();//trs_id
                Bean.strC2 = reader.GetValue(1).ToString();//trs_empresa
                Bean.strC3 = reader.GetValue(2).ToString();//trs_sistema
                Bean.strC4 = reader.GetValue(3).ToString();//trs_tipo_operacion
                Bean.strC5 = reader.GetValue(4).ToString();//trs_bl
                Bean.strC6 = reader.GetValue(5).ToString();//trs_imp_exp
                Bean.strC7 = reader.GetValue(6).ToString();//trs_usu_id
                Bean.strC8 = reader.GetValue(7).ToString();//trs_fecha_creacion
                Bean.strC9 = reader.GetValue(8).ToString();//trs_viaje_no
                Bean.strC10 = reader.GetValue(9).ToString();//trs_estado_sesion
                Bean.strC11 = "";//Nombre del Estado
                #region Asignar Nombre de Estado
                if (Bean.strC10 == "1")
                {
                    Bean.strC11 = "SIN RESPONDER";
                }
                else if (Bean.strC10 == "2")
                {
                    Bean.strC11 = "RESPONDIDA";
                }
                else if (Bean.strC10 == "3")
                {
                    Bean.strC11 = "RESPONDIENDO";
                }
                else if (Bean.strC10 == "4")
                {
                    Bean.strC11 = "CONTABILIZADA";
                }
                #endregion
                result.Add(Bean);
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return result;
    }
    public static int Obtener_Numero_Grupo_Sesion_Contabilizacion_Automatica(int sesionID)
    {
        int resultado = 0;
        int numero_grupo = 0;
        int nuevo_numero_grupo = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select coalesce(trs_numero_grupo,0) from tbl_reconciliacion_carga_sesiones where trs_estado=1 and trs_id=" + sesionID + " ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                numero_grupo = int.Parse(reader.GetValue(0).ToString());
            }
            comm.Parameters.Clear();
            if (numero_grupo > 0)
            {
                nuevo_numero_grupo = numero_grupo + 1;
                comm.CommandText = "update tbl_reconciliacion_carga_sesiones set trs_numero_grupo=" + nuevo_numero_grupo + " where trs_estado=1 and trs_id=" + sesionID + " ";
                resultado = comm.ExecuteNonQuery();
                comm.Parameters.Clear();
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return numero_grupo;
    }
    public static int Insertar_Configuracion_Series_Contabilizacion_Automatica(RE_GenericBean Bean)
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
            comm.CommandText = "insert into tbl_reconciliacion_carga_series (trcs_sis_id, trcs_tto_id, trcs_empresa_id, trcs_ttr_id, trcs_conta_id, trcs_moneda_id, trcs_sucursal_id, trcs_tipo_operacion, trcs_serie, trcs_usu_id) ";
            comm.CommandText += " values (@trcs_sis_id, @trcs_tto_id, @trcs_empresa_id, @trcs_ttr_id, @trcs_conta_id, @trcs_moneda_id, @trcs_sucursal_id, @trcs_tipo_operacion, @trcs_serie, @trcs_usu_id)";
            comm.Parameters.Add("@trcs_sis_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC1;
            comm.Parameters.Add("@trcs_tto_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC2;
            comm.Parameters.Add("@trcs_empresa_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC3;
            comm.Parameters.Add("@trcs_ttr_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC4;
            comm.Parameters.Add("@trcs_conta_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC5;
            comm.Parameters.Add("@trcs_moneda_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC6;
            comm.Parameters.Add("@trcs_sucursal_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC7;
            comm.Parameters.Add("@trcs_tipo_operacion", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC8;
            comm.Parameters.Add("@trcs_serie", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC1;
            comm.Parameters.Add("@trcs_usu_id", NpgsqlTypes.NpgsqlDbType.Varchar).Value = Bean.strC2;
            resultado = comm.ExecuteNonQuery();
            comm.Parameters.Clear();
            comm.CommandText = "";
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
    public static int Validar_Existencia_Configuracion_Series_Contabilizacion_Automatica(string sql)
    {
        int resultado = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select coalesce(trcs_id,0) from tbl_reconciliacion_carga_series where trcs_estado=1 " + sql + " ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                resultado = int.Parse(reader.GetValue(0).ToString());
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return resultado;
    }
    public static ArrayList Obtener_Detalle_Configuracion_Series_Contabilizacion_Automatica(string sql)
    {
        ArrayList Arr_Result = new ArrayList();
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
            comm.CommandText = "select trcs_id, trcs_sis_id, tsis_nombre, trcs_tto_id, tto_nombre, trcs_empresa_id, pai_nombre_sistema, trcs_ttr_id, upper(ttr_nombre), trcs_conta_id, tcon_nombre, " +
            "trcs_moneda_id, substr(ttm_nombre,1,3), trcs_sucursal_id, suc_nombre, trcs_tipo_operacion, trcs_serie, trcs_tipo_operacion " +
            "from tbl_reconciliacion_carga_series " +
            "inner join tbl_sistemas on (trcs_sis_id=tsis_id) " +
            "inner join tbl_tipo_operacion on (trcs_tto_id=tto_id) " +
            "inner join tbl_pais on (trcs_empresa_id=pai_id) " +
            "inner join sys_tipo_referencia on (trcs_ttr_id=ttr_id) " +
            "inner join tbl_tipo_conta on (trcs_conta_id=tcon_id) " +
            "inner join tbl_tipo_moneda on (trcs_moneda_id=ttm_id) " +
            "inner join tbl_sucursal on (trcs_sucursal_id=suc_id) " +
            "where trcs_estado=1 ";
            //comm.CommandText += " order by trcs_id ";
            comm.CommandText += " order by trcs_empresa_id, trcs_sis_id, trcs_tto_id, trcs_ttr_id, trcs_tipo_operacion, trcs_moneda_id, trcs_id ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.strC1 = reader.GetValue(0).ToString();//trcs_id
                Bean.strC2 = reader.GetValue(1).ToString();//trcs_sis_id
                Bean.strC3 = reader.GetValue(2).ToString();//tsis_nombre
                Bean.strC4 = reader.GetValue(3).ToString();//trcs_tto_id
                Bean.strC5 = reader.GetValue(4).ToString();//tto_nombre
                Bean.strC6 = reader.GetValue(5).ToString();//trcs_empresa_id
                Bean.strC7 = reader.GetValue(6).ToString();//pai_nombre_sistema
                Bean.strC8 = reader.GetValue(7).ToString();//trcs_ttr_id
                Bean.strC9 = reader.GetValue(8).ToString();//ttr_nombre
                Bean.strC10 = reader.GetValue(9).ToString();//trcs_conta_id
                Bean.strC11 = reader.GetValue(10).ToString();//tcon_nombre
                Bean.strC12 = reader.GetValue(11).ToString();//trcs_moneda_id
                Bean.strC13 = reader.GetValue(12).ToString();//ttm_nombre
                Bean.strC14 = reader.GetValue(13).ToString();//trcs_sucursal_id
                Bean.strC15 = reader.GetValue(14).ToString();//suc_nombre
                Bean.strC16 = reader.GetValue(15).ToString();//trcs_tipo_operacion
                Bean.strC17 = reader.GetValue(16).ToString();//trcs_serie
                Bean.strC18 = reader.GetValue(17).ToString();//trcs_tipo_operacion
                if (Bean.strC18 == "1")
                {
                    Bean.strC18 = "FACTURACION";
                }
                else if (Bean.strC18 == "2")
                {
                    Bean.strC18 = "OPERACIONES";
                }
                Arr_Result.Add(Bean);
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Arr_Result;
    }
    public static RE_GenericBean Obtener_Configuracion_Serie_Contabilizacion_Automatica(string sql)
    {
        RE_GenericBean Bean_Result = new RE_GenericBean();
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select trcs_id, trcs_sis_id, tsis_nombre, trcs_tto_id, tto_nombre, trcs_empresa_id, pai_nombre_sistema, trcs_ttr_id, upper(ttr_nombre), trcs_conta_id, tcon_nombre, " +
            "trcs_moneda_id, substr(ttm_nombre,1,3), trcs_sucursal_id, suc_nombre, trcs_tipo_operacion, trcs_serie " +
            "from tbl_reconciliacion_carga_series " +
            "inner join tbl_sistemas on (trcs_sis_id=tsis_id) " +
            "inner join tbl_tipo_operacion on (trcs_tto_id=tto_id) " +
            "inner join tbl_pais on (trcs_empresa_id=pai_id) " +
            "inner join sys_tipo_referencia on (trcs_ttr_id=ttr_id) " +
            "inner join tbl_tipo_conta on (trcs_conta_id=tcon_id) " +
            "inner join tbl_tipo_moneda on (trcs_moneda_id=ttm_id) " +
            "inner join tbl_sucursal on (trcs_sucursal_id=suc_id) " +
            "where trcs_estado=1 ";
            comm.CommandText += sql;
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean_Result = new RE_GenericBean();
                Bean_Result.strC1 = reader.GetValue(0).ToString();//trcs_id
                Bean_Result.strC2 = reader.GetValue(1).ToString();//trcs_sis_id
                Bean_Result.strC3 = reader.GetValue(2).ToString();//tsis_nombre
                Bean_Result.strC4 = reader.GetValue(3).ToString();//trcs_tto_id
                Bean_Result.strC5 = reader.GetValue(4).ToString();//tto_nombre
                Bean_Result.strC6 = reader.GetValue(5).ToString();//trcs_empresa_id
                Bean_Result.strC7 = reader.GetValue(6).ToString();//pai_nombre_sistema
                Bean_Result.strC8 = reader.GetValue(7).ToString();//trcs_ttr_id
                Bean_Result.strC9 = reader.GetValue(8).ToString();//ttr_nombre
                Bean_Result.strC10 = reader.GetValue(9).ToString();//trcs_conta_id
                Bean_Result.strC11 = reader.GetValue(10).ToString();//tcon_nombre
                Bean_Result.strC12 = reader.GetValue(11).ToString();//trcs_moneda_id
                Bean_Result.strC13 = reader.GetValue(12).ToString();//ttm_nombre
                Bean_Result.strC14 = reader.GetValue(13).ToString();//trcs_sucursal_id
                Bean_Result.strC15 = reader.GetValue(14).ToString();//suc_nombre
                Bean_Result.strC16 = reader.GetValue(15).ToString();//trcs_tipo_operacion
                Bean_Result.strC17 = reader.GetValue(16).ToString();//trcs_serie
                #region Validar Serie Electronica
                if ((Bean_Result.strC8 == "1") || (Bean_Result.strC8 == "4"))
                {
                    UsuarioBean Usuario_temporal = new UsuarioBean();
                    Usuario_temporal.contaID = int.Parse(Bean_Result.strC10);
                    int Doc_ID = DB.getDocumentoID(int.Parse(Bean_Result.strC14), Bean_Result.strC17, int.Parse(Bean_Result.strC8), Usuario_temporal);
                    RE_GenericBean Bean_Serie = (RE_GenericBean)DB.getFactura(Doc_ID, int.Parse(Bean_Result.strC8));
                    Bean_Result.intC1 = Bean_Serie.intC14;
                }
                else
                {
                    Bean_Result.intC1 = 0;
                }
                #endregion
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Bean_Result;
    }
    public static int Eliminar_Configuracion_Series_Contabilizacion_Automatica(UsuarioBean user, int configuracionID)
    {
        int result = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "update tbl_reconciliacion_carga_series set trcs_estado=0, trcs_usu_id_eliminacion='" + user.ID + "', trcs_fecha_eliminacion=now() where trcs_id=" + configuracionID + " and trcs_estado=1 ";
            result = comm.ExecuteNonQuery();
            DB.CloseObj_insert(comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return result;
    }
    public static ArrayList Obtener_Detalle_Transacciones_Sesion_SCA(int sesionID)
    {
        ArrayList Arr_Result = new ArrayList();
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
            comm.CommandText = "select  trs_id, trb_id, trc_id, trt_id, " +
            "trt_tipo_bl, trt_tipo_carga, trt_destino_final, " +
            "trt_ttr_id, trt_tipo_documento, trt_tpi_id, trt_tipo_persona, trt_persona_id, trt_nombre, " +
            "trt_tts_id, trt_tipo_servicio, trt_rub_id, trt_rubro, trt_conta_id, trt_contabilidad, trt_ttm_id, trt_moneda, trt_monto, trt_concepto, trt_observaciones, " +
            "trt_agente_cobrar, trt_agente_pagar, trt_naviera_cobrar, trt_naviera_pagar, trt_cliente_cobrar, trt_cliente_pagar, " +
            "trt_intermodal_cobrar, trt_intermodal_pagar, trt_intercompany_destino_cobrar, trt_intercompany_destino_pagar, trt_ingresos, trt_costos, " +
            "trt_prepaid_collect_id, trt_prepaid_collect, trt_local_internacional_id, trt_local_internacional, trt_tiene_conocimiento_embarque, trt_all_in, " +
            "trt_grupo_id, trt_contabilizar, trt_fecha_creacion, trt_usu_id, trt_trc_id, trt_estado, trb_bl,  " +
            "trt_empresa_id, trt_cargo_id, trt_tiene_referencia_adicional, trt_id_routing_adicional, trt_routing_adicional, trt_tto_id_adicional, trb_bl_id, trt_serie, trt_correlativo, " +
            "trt_proveedor_serie, trt_proveedor_correlativo, trt_proveedor_fecha, " +
            "trt_ttf_id, trt_afecto_excento " +
            "from tbl_reconciliacion_carga_sesiones, tbl_reconciliacion_carga_bls, tbl_reconciliacion_carga_cuestionario, tbl_reconciliacion_carga_transacciones " +
            "where trs_id=trb_trs_id and trs_estado=1 and trb_estado=1 " +
            "and trb_id=trc_trb_id and trc_estado=1 " +
            "and trc_id=trt_trc_id and trt_estado=1 " +
            "and trs_id=" + sesionID + " order by 4 asc ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.strC1 = reader.GetValue(0).ToString();//trs_id
                Bean.strC2 = reader.GetValue(1).ToString();//trb_id
                Bean.strC3 = reader.GetValue(2).ToString();//trc_id
                Bean.strC4 = reader.GetValue(3).ToString();//trt_id
                Bean.strC5 = reader.GetValue(4).ToString();//trt_tipo_bl
                Bean.strC6 = reader.GetValue(5).ToString();//trt_tipo_carga
                Bean.strC7 = reader.GetValue(6).ToString();//trt_destino_final
                Bean.strC8 = reader.GetValue(7).ToString();//trt_ttr_id
                Bean.strC9 = reader.GetValue(8).ToString();//trt_tipo_documento
                Bean.strC10 = reader.GetValue(9).ToString();//trt_tpi_id
                Bean.strC11 = reader.GetValue(10).ToString();//trt_tipo_persona
                Bean.strC12 = reader.GetValue(11).ToString();//trt_persona_id
                Bean.strC13 = reader.GetValue(12).ToString();//trt_nombre
                Bean.strC14 = reader.GetValue(13).ToString();//trt_tts_id
                Bean.strC15 = reader.GetValue(14).ToString();//trt_tipo_servicio
                Bean.strC16 = reader.GetValue(15).ToString();//trt_rub_id
                Bean.strC17 = reader.GetValue(16).ToString();//trt_rubro
                Bean.strC18 = reader.GetValue(17).ToString();//trt_conta_id
                Bean.strC19 = reader.GetValue(18).ToString();//trt_contabilidad
                Bean.strC20 = reader.GetValue(19).ToString();//trt_ttm_id
                Bean.strC21 = reader.GetValue(20).ToString();//trt_moneda
                Bean.strC22 = reader.GetValue(21).ToString();//trt_monto
                Bean.strC23 = reader.GetValue(22).ToString();//trt_concepto
                Bean.strC24 = reader.GetValue(23).ToString();//trt_observaciones
                Bean.strC25 = reader.GetValue(24).ToString();//trt_agente_cobrar
                Bean.strC26 = reader.GetValue(25).ToString();//trt_agente_pagar
                Bean.strC27 = reader.GetValue(26).ToString();//trt_naviera_cobrar
                Bean.strC28 = reader.GetValue(27).ToString();//trt_naviera_pagar
                Bean.strC29 = reader.GetValue(28).ToString();//trt_cliente_cobrar
                Bean.strC30 = reader.GetValue(29).ToString();//trt_cliente_pagar
                Bean.strC31 = reader.GetValue(30).ToString();//trt_intermodal_cobrar
                Bean.strC32 = reader.GetValue(31).ToString();//trt_intermodal_pagar
                Bean.strC33 = reader.GetValue(32).ToString();//trt_intercompany_destino_cobrar
                Bean.strC34 = reader.GetValue(33).ToString();//trt_intercompany_destino_pagar
                Bean.strC35 = reader.GetValue(34).ToString();//trt_ingresos
                Bean.strC36 = reader.GetValue(35).ToString();//trt_costos
                Bean.strC37 = reader.GetValue(36).ToString();//trt_prepaid_collect_id
                Bean.strC38 = reader.GetValue(37).ToString();//trt_prepaid_collect
                Bean.strC39 = reader.GetValue(38).ToString();//trt_local_internacional_id
                Bean.strC40 = reader.GetValue(39).ToString();//trt_local_internacional
                Bean.strC41 = reader.GetValue(40).ToString();//trt_tiene_conocimiento_embarque
                Bean.strC42 = reader.GetValue(41).ToString();//trt_all_in
                Bean.strC43 = reader.GetValue(42).ToString();//trt_grupo_id
                Bean.strC44 = reader.GetValue(43).ToString();//trt_contabilizar
                Bean.strC45 = reader.GetValue(44).ToString();//trt_fecha_creacion
                Bean.strC46 = reader.GetValue(45).ToString();//trt_usu_id
                Bean.strC47 = reader.GetValue(46).ToString();//trt_trc_id
                Bean.strC48 = reader.GetValue(47).ToString();//trt_estado
                Bean.strC49 = reader.GetValue(48).ToString();//trb_bl
                Bean.strC50 = reader.GetValue(49).ToString();//trt_empresa_id
                Bean.strC51 = reader.GetValue(50).ToString();//trt_cargo_id
                Bean.strC52 = reader.GetValue(51).ToString();//trt_tiene_referencia_adicional
                Bean.strC53 = reader.GetValue(52).ToString();//trt_id_routing_adicional
                Bean.strC54 = reader.GetValue(53).ToString();//trt_routing_adicional
                Bean.strC55 = reader.GetValue(54).ToString();//trt_tto_id_adicional
                Bean.strC56 = "FALSE";//CONTABILIZADO - Esto Sirve para hacer los Grupos
                Bean.strC57 = reader.GetValue(55).ToString();//trb_bl_id
                Bean.strC58 = reader.GetValue(56).ToString();//trt_serie
                Bean.strC59 = reader.GetValue(57).ToString();//trt_correlativo
                Bean.strC60 = reader.GetValue(58).ToString();//trt_proveedor_serie
                Bean.strC61 = reader.GetValue(59).ToString();//trt_proveedor_correlativo
                Bean.strC62 = reader.GetValue(60).ToString();//trt_proveedor_fecha
                Bean.strC63 = reader.GetValue(61).ToString();//trt_ttf_id
                Bean.strC64 = reader.GetValue(62).ToString();//trt_afecto_excento
                Arr_Result.Add(Bean);
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Arr_Result;
    }
    public static ArrayList Construir_Factura_SCA(int SesionID, RE_GenericBean Bean_Transaccion, ArrayList Arr_Detalle_Transacciones)
    {
        ArrayList Arr_Result = null;
        #region Construir Factura Automatica Sistema de Contabilizacion Automatica
        int tipo_cobro = 1;//prepaid, collect
        string sql = "";
        Rubros Rubro_Bean = null;
        try
        {

            PaisBean Empresa_Bean = DB.getPais(int.Parse(Bean_Transaccion.strC50));
            RE_GenericBean Bean_Sesion = Contabilizacion_Automatica_CAD.Obtener_Detalle_Sesion_Reconciliacon_Carga(SesionID);

            Bean_Datos_BL Datos_BL = null;
            if (Bean_Transaccion.strC52.ToUpper() == "FALSE")
            {
                PaisBean Empresa_Origen_Bean = DB.getPais(int.Parse(Bean_Sesion.strC2));
                Datos_BL = Contabilizacion_Automatica_CAD.Get_DatosBL_X_Traficos(int.Parse(Bean_Sesion.strC4), int.Parse(Bean_Sesion.strC6), int.Parse(Bean_Transaccion.strC57), Empresa_Origen_Bean);
                //Datos_BL = Contabilizacion_Automatica_CAD.Get_DatosBL_X_Traficos(int.Parse(Bean_Sesion.strC4), int.Parse(Bean_Sesion.strC6), int.Parse(Bean_Transaccion.strC57), Empresa_Bean);
            }
            else
            {
                #region Obtener Datos RO Adicional
                Datos_BL = new Bean_Datos_BL();
                Datos_BL.BLID = int.Parse(Bean_Transaccion.strC53);
                Datos_BL.Routing = Bean_Transaccion.strC54;
                Datos_BL.ttoID = int.Parse(Bean_Transaccion.strC55);
                Datos_BL.Cliente = int.Parse(Bean_Transaccion.strC12);
                Datos_BL.Import_Export = 1;//Import
                #endregion
            }
            
            sql = "";
            if ((Bean_Sesion.strC2 != "6") && (Bean_Sesion.strC2 != "25"))
            {
                //Todas las Empresas que no sean de Panama
                sql = "and trcs_sis_id=" + Bean_Sesion.strC4 + " and trcs_tto_id=" + Bean_Sesion.strC6 + " and trcs_empresa_id=" + Empresa_Bean.ID + " and trcs_ttr_id=" + Bean_Transaccion.strC8 + " and trcs_conta_id=" + Bean_Transaccion.strC18 + " and trcs_moneda_id=" + Bean_Transaccion.strC20 + " and trcs_tipo_operacion=1 ";
            }
            else
            {
                //Solo Empresas de Panama
                sql = "and trcs_sis_id=" + Bean_Sesion.strC4 + " and trcs_tto_id=" + Bean_Sesion.strC6 + " and trcs_empresa_id=" + Empresa_Bean.ID + " and trcs_ttr_id=" + Bean_Transaccion.strC8 + " and trcs_conta_id=" + Bean_Transaccion.strC18 + " and trcs_moneda_id=" + Bean_Transaccion.strC20 + " and trcs_tipo_operacion=1 and trcs_sucursal_id=" + Bean_Sesion.strC22 + " ";
            }
            RE_GenericBean Bean_Configuracion_Serie = Contabilizacion_Automatica_CAD.Obtener_Configuracion_Serie_Contabilizacion_Automatica(sql);
            if (Bean_Configuracion_Serie == null)
            {
                Arr_Result = new ArrayList();
                Arr_Result.Add("0");
                Arr_Result.Add("Existio un error al Tratar de Obtener la Serie de la Factura");
                return Arr_Result;
            }
            else if (Bean_Configuracion_Serie.strC17 == "")
            {
                Arr_Result = new ArrayList();
                Arr_Result.Add("0");
                if ((Bean_Sesion.strC2 != "6") && (Bean_Sesion.strC2 != "25"))
                {
                    //Todas las Empresas que no sean de Panama
                    Arr_Result.Add("No existe Serie Configurada para Contabilizar la Factura.: (" + Bean_Transaccion.strC1 + " , " + Bean_Transaccion.strC4 + ")");
                }
                else
                {
                    //Solo Empresas de Panama
                    SucursalBean Suc_Temporal = null;
                    Suc_Temporal = DB.getSucursal(int.Parse(Bean_Sesion.strC22));
                    Arr_Result.Add("No existe Serie Configurada para Contabilizar la Factura.: (" + Bean_Transaccion.strC1 + " , " + Bean_Transaccion.strC4 + ") en el Departamento.: "+Suc_Temporal.Nombre+" ");
                    Suc_Temporal = null;
                }
                return Arr_Result;
            }

            #region Definir Serie El Salvador - Credito Fiscal, Factura de Exportacion, Factura de Consumidor Final
            if (Empresa_Bean.ID == 1 && Bean_Transaccion.strC10 == "10" && Bean_Transaccion.strC12 == "26")
            {
                Bean_Configuracion_Serie.strC17 = "EXP";
                Bean_Configuracion_Serie.strC14 = "87";
                Bean_Configuracion_Serie.intC1 = 1;
                Bean_Configuracion_Serie.strC12 = "1";
            }
            if (Empresa_Bean.ID == 2)
            {
                string _Tipo_Factura = "";
                _Tipo_Factura = Bean_Transaccion.strC63;
                if (_Tipo_Factura == "2")
                {
                    //Factura de Consumidor Final
                    Bean_Configuracion_Serie.strC17 = "FC112";
                }
                else if (_Tipo_Factura == "3")
                {
                    //Credito Fiscal
                    Bean_Configuracion_Serie.strC17 = "CF115";
                }
                else if (_Tipo_Factura == "4")
                {
                    //Factura de Exportacion
                    Bean_Configuracion_Serie.strC17 = "FX112";
                }
            }
            if (Empresa_Bean.ID == 9)
            {
                string _Tipo_Factura = "";
                _Tipo_Factura = Bean_Transaccion.strC63;
                if (_Tipo_Factura == "2")
                {
                    //Factura de Consumidor Final
                    Bean_Configuracion_Serie.strC17 = "FC113";
                }
                else if (_Tipo_Factura == "3")
                {
                    //Credito Fiscal
                    Bean_Configuracion_Serie.strC17 = "CF130";
                }
                else if (_Tipo_Factura == "4")
                {
                    //Factura de Exportacion
                    Bean_Configuracion_Serie.strC17 = "FX115";
                }
            }
            if (Empresa_Bean.ID == 26)
            {
                string _Tipo_Factura = "";
                _Tipo_Factura = Bean_Transaccion.strC63;
                if (_Tipo_Factura == "2")
                {
                    //Factura de Consumidor Final
                    Bean_Configuracion_Serie.strC17 = "FC111";
                }
                else if (_Tipo_Factura == "3")
                {
                    //Credito Fiscal
                    Bean_Configuracion_Serie.strC17 = "CF111";
                }
                else if (_Tipo_Factura == "4")
                {
                    //Factura de Exportacion
                    Bean_Configuracion_Serie.strC17 = "FX111";
                }
            }
            #endregion

            Bean_Factura_Automatica Factura_Automatica = new Bean_Factura_Automatica();
            Factura_Automatica.tfa_id = 0;
            Factura_Automatica.tfa_correlativo = "0";
            Factura_Automatica.tfa_nit = "";
            Factura_Automatica.tfa_nombre = "";
            Factura_Automatica.tfa_direccion = "";
            Factura_Automatica.tfa_conta_id = int.Parse(Bean_Transaccion.strC18);
            if (Bean_Transaccion.strC10 == "3")
            {
                #region Cargar Cliente
                RE_GenericBean Bean_Cliente_Aux = DB.getDataClient(Convert.ToDouble(Bean_Transaccion.strC12));
                if (Bean_Cliente_Aux == null)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    Arr_Result.Add("El cliente con ID.: " + Bean_Transaccion.strC12.ToString() + " es invalido ");
                    return Arr_Result;
                }
                Factura_Automatica.tfa_nit = Bean_Cliente_Aux.strC1;
                #region Validar Nit
                if (Factura_Automatica.tfa_nit.Trim() == "")
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    Arr_Result.Add("El NIT del cliente con ID.: " + Bean_Transaccion.strC12.ToString() + " es invalido ");
                    return Arr_Result;
                }
                #endregion
                Factura_Automatica.tfa_nombre = Bean_Cliente_Aux.strC3;
                #region Validar Nombre
                if (Factura_Automatica.tfa_nombre.Trim() == "")
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    Arr_Result.Add("El Nombre para Facturar del cliente con ID.: " + Bean_Transaccion.strC12.ToString() + " es invalido ");
                    return Arr_Result;
                }
                #endregion
                Factura_Automatica.tfa_direccion = Bean_Cliente_Aux.strC4;
                Factura_Automatica.tfa_razon = Bean_Cliente_Aux.strC2;
                Factura_Automatica.tfa_ruc = Bean_Cliente_Aux.strC5;
                Factura_Automatica.tfa_tpi_id = int.Parse(Bean_Transaccion.strC10);
                Factura_Automatica.tfa_correo_documento_electronico = Bean_Cliente_Aux.strC8;
                Factura_Automatica.tfa_moneda = int.Parse(Bean_Transaccion.strC20);
                Factura_Automatica.tfa_giro = Bean_Cliente_Aux.strC6;
                #region Definir Tipo de Contribuyente
                if (Factura_Automatica.tfa_conta_id == 1)
                {
                    #region Contabilidad Fiscal
                    if (Factura_Automatica.tfa_moneda == 8)
                    {
                        #region Moneda USD
                        if ((Empresa_Bean.ID == 2) || (Empresa_Bean.ID == 9) || (Empresa_Bean.ID == 26))
                        {
                            #region Empresas de El Salvador
                            //Empresas de El Salvador
                            string _Tipo_Factura_Aux = "";
                            _Tipo_Factura_Aux = Bean_Transaccion.strC63;
                            if ((_Tipo_Factura_Aux == "2") || (_Tipo_Factura_Aux == "3"))
                            {
                                //Facturas de Consumidor Final y Credito Fiscal si cobran impuestos en base al Catalogo
                                Factura_Automatica.tfa_tti_id = Bean_Cliente_Aux.intC1;
                            }
                            else if (_Tipo_Factura_Aux == "4")
                            {
                                //Factura de Exportacion es Excenta
                                Factura_Automatica.tfa_tti_id = 1;
                            }
                            #endregion
                        }
                        else if ((Empresa_Bean.ID == 6) || (Empresa_Bean.ID == 25))
                        {
                            #region Empresas de Panama
                            if ((Bean_Configuracion_Serie.strC14 == "79") || (Bean_Configuracion_Serie.strC14 == "81"))
                            {
                                //Empresas de Panama en Zona Libre
                                Factura_Automatica.tfa_tti_id = 1;
                            }
                            else
                            {
                                //Empresas de Panama que no estan en Zona Libre
                                Factura_Automatica.tfa_tti_id = Bean_Cliente_Aux.intC1;
                            }
                            #endregion
                        }
                        else
                        {
                            #region Contabilidad Fiscal USD - Todas las Empresas que no estan en USD
                            //Excento  - Todas las Empresas que no estan en USD
                            if ((Empresa_Bean.ID == 1) || (Empresa_Bean.ID == 15))
                            {
                                //En Guatemala Fiscal USD Exenta
                                Factura_Automatica.tfa_tti_id = 1;
                            }
                            else
                            {
                                //Afecto en Base a Master Aimar en todas las Empresas que no estan en USD ni son Guatemala
                                Factura_Automatica.tfa_tti_id = Bean_Cliente_Aux.intC1;
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        #region Moneda Local
                        //Contribuyente en Base al Catalogo
                        Factura_Automatica.tfa_tti_id = Bean_Cliente_Aux.intC1;
                        #endregion
                    }
                    #endregion
                }
                else if (Factura_Automatica.tfa_conta_id == 2)
                {
                    #region Contabilidad Financiera
                    //Excento
                    Factura_Automatica.tfa_tti_id = 1;
                    #endregion
                }
                #endregion
                #endregion
            }
            else if (Bean_Transaccion.strC10 == "10")
            {
                #region Cargar Intercompany
                RE_GenericBean Bean_Intercompany_Aux = DB.getIntercompanyData(Convert.ToInt32(Bean_Transaccion.strC12));
                if (Bean_Intercompany_Aux == null)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    Arr_Result.Add("El Intercompany con ID.: " + Datos_BL.Cliente.ToString() + " es invalido ");
                    return Arr_Result;
                }
                Factura_Automatica.tfa_nit = Bean_Intercompany_Aux.strC2;
                Factura_Automatica.tfa_nombre = Bean_Intercompany_Aux.strC1;
                Factura_Automatica.tfa_direccion = Bean_Intercompany_Aux.strC4;
                Factura_Automatica.tfa_razon = Bean_Intercompany_Aux.strC1;
                Factura_Automatica.tfa_ruc = "";
                Factura_Automatica.tfa_tpi_id = int.Parse(Bean_Transaccion.strC10);
                Factura_Automatica.tfa_correo_documento_electronico = "";
                Factura_Automatica.tfa_moneda = int.Parse(Bean_Transaccion.strC20);
                Factura_Automatica.tfa_giro = "";
                Factura_Automatica.tfa_tti_id = 1;
                #endregion
            }

            Factura_Automatica.tfa_fecha_emision = DB.getDateTimeNow().Substring(0, 10);
            Factura_Automatica.tfa_fecha_pago = DB.getDateTimeNow().Substring(0, 10);
            #region Definir Fecha de Pago
            string Fecha_Emision = "";
            string Fecha_Pago = "";

            Fecha_Emision = Factura_Automatica.tfa_fecha_emision;
            Fecha_Pago = Factura_Automatica.tfa_fecha_pago;

            DateTime _Fecha_Emision = DateTime.Parse(Fecha_Emision);
            DateTime _Fecha_Pago = DateTime.Parse(Fecha_Emision);
            //Pablo Aguilar: Validar que cuando la empresa para la transasaccion sea WMT, busque los dias de credito en la empresa de la sesion.
            int empresa_id = Empresa_Bean.ID;
            if (Empresa_Bean.ID == 30)
            {
                empresa_id = int.Parse(Bean_Sesion.strC2);
            }
            ////
            double _Dias_Credito = DB.Get_Dias_Credito_X_Tipo_Persona_Empresa(Convert.ToInt32(Bean_Transaccion.strC12), 3, empresa_id);
            _Fecha_Pago = _Fecha_Pago.AddDays(_Dias_Credito);

            Factura_Automatica.tfa_fecha_emision = _Fecha_Emision.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture) + " " + DateTime.Now.TimeOfDay.ToString().Substring(0, 8);
            Factura_Automatica.tfa_fecha_pago = _Fecha_Pago.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture) + " " + DateTime.Now.TimeOfDay.ToString().Substring(0, 8);
            #endregion

            Factura_Automatica.tfa_sub_total = 0;
            Factura_Automatica.tfa_impuesto = 0;
            Factura_Automatica.tfa_total = 0;
            Factura_Automatica.tfa_observacion = "";
            Factura_Automatica.tfa_suc_id = int.Parse(Bean_Configuracion_Serie.strC14);
            Factura_Automatica.tfa_cli_id = Convert.ToInt32(Bean_Transaccion.strC12);
            Factura_Automatica.tfa_moneda = int.Parse(Bean_Transaccion.strC20);
            Factura_Automatica.tfa_ted_id = 1;
            Factura_Automatica.tfa_usu_id = Bean_Transaccion.strC46;
            Factura_Automatica.tfa_hbl = Datos_BL.Hbl;
            Factura_Automatica.tfa_mbl = Datos_BL.Mbl;
            Factura_Automatica.tfa_contenedor = Datos_BL.Contenedor;
            Factura_Automatica.tfa_routing = Datos_BL.Routing;
            sql = "select nombre from navieras where activo=true and id_naviera=" + Datos_BL.Naviera;
            Factura_Automatica.tfa_naviera = DB.getName(sql);
            Factura_Automatica.tfa_vapor = Datos_BL.Vapor;
            sql = "select nombre_cliente from clientes where id_cliente=" + Datos_BL.Shipper;
            Factura_Automatica.tfa_shipper = DB.getName(sql);
            Factura_Automatica.tfa_ordenpo = "";
            sql = "select nombre_cliente from clientes where id_cliente=" + Datos_BL.Consignatario;
            Factura_Automatica.tfa_consignee = DB.getName(sql);
            sql = "select namees from commodities where commodityid =" + Datos_BL.Comodity;
            Factura_Automatica.tfa_comodity = DB.getName(sql);
            Factura_Automatica.tfa_paquetes = (string)DB.GetNombreTipoBulto(Datos_BL.Tipo_Paquete);
            Factura_Automatica.tfa_peso = Datos_BL.Peso;
            Factura_Automatica.tfa_volumen = Datos_BL.Volumen;
            Factura_Automatica.tfa_dua_ingreso = "";
            Factura_Automatica.tfa_dua_salida = "";
            Factura_Automatica.tfa_vendedor1 = Datos_BL.Vendedor1;
            Factura_Automatica.tfa_vendedor2 = Datos_BL.Vendedor2;
            Factura_Automatica.tfa_referencia = "";
            Factura_Automatica.tfa_serie = Bean_Configuracion_Serie.strC17;
            Factura_Automatica.tfa_id_shipper = Datos_BL.Shipper;
            Factura_Automatica.tfa_id_consignee = Datos_BL.Consignatario;
            Factura_Automatica.tfa_pai_id = Empresa_Bean.ID;
            Factura_Automatica.tfa_sub_total_eq = 0;
            Factura_Automatica.tfa_impuesto_eq = 0;
            Factura_Automatica.tfa_total_eq = 0;
            Factura_Automatica.tfa_tie_id = Datos_BL.Import_Export;
            Factura_Automatica.tfa_ttc_id = 1;
            Factura_Automatica.tfa_allin = Bean_Transaccion.strC42;
            Factura_Automatica.tfa_reciboaduana = "";
            Factura_Automatica.tfa_cant_paquetes = Datos_BL.No_Piezas.ToString();
            Factura_Automatica.tfa_agent_id = Datos_BL.Agente;
            sql = "select agente from agentes where activo=true and agente_id=" + Datos_BL.Agente;
            Factura_Automatica.tfa_agente = DB.getName(sql);
            Factura_Automatica.tfa_recibo_agencia = "";
            Factura_Automatica.tfa_valor_aduanero = "";
            Factura_Automatica.tfa_ttf_id = int.Parse(Bean_Transaccion.strC63);
            Factura_Automatica.tfa_ruta_pais = "";
            Factura_Automatica.tfa_ruta = "";
            Factura_Automatica.tfa_observacion2 = "";
            Factura_Automatica.tfa_tra_id = 0;
            Factura_Automatica.tfa_blid = Datos_BL.BLID;
            Factura_Automatica.tfa_blid = int.Parse(Bean_Transaccion.strC57);
            Factura_Automatica.tfa_tto_id = Datos_BL.ttoID;
            Factura_Automatica.tfa_esignature = "-";
            Factura_Automatica.tfa_fac_electronica = Bean_Configuracion_Serie.intC1;
            Factura_Automatica.tfa_internal_reference = "0";
            Factura_Automatica.tfa_guid = "0";
            Factura_Automatica.tfa_referencia_correo = "-";
            Factura_Automatica.tfa_innerxml = "";
            Factura_Automatica.tfa_fecha_batch = DB.getDateTimeNow().Substring(0, 10);
            Factura_Automatica.tfa_correlativo_electronico = "0";
            Factura_Automatica.tfa_no_factura_aduana = "-";
            Factura_Automatica.tfa_no_embarque = "-";
            #region Generar Detalle de Rubros
            Bean_Detalle_Rubros Detalle_Rubros = null;
            foreach (RE_GenericBean Bean_Cargo in Arr_Detalle_Transacciones)
            {
                int Tipo_Contribuyente_Temporal = 0;
                int Tipo_Cargo_Temporal = 0;
                Tipo_Contribuyente_Temporal = Factura_Automatica.tfa_tti_id;
                Tipo_Cargo_Temporal = int.Parse(Bean_Cargo.strC39);//Local(1) o Internacional(2)

                Detalle_Rubros = new Bean_Detalle_Rubros();
                Detalle_Rubros.tdf_cargo_id = int.Parse(Bean_Cargo.strC51);
                Detalle_Rubros.tdf_tts_id = int.Parse(Bean_Cargo.strC14);
                Detalle_Rubros.tdf_rub_id = int.Parse(Bean_Cargo.strC16);
                Detalle_Rubros.tdf_montosinimpuesto = Convert.ToDouble(Bean_Cargo.strC22);
                if (Detalle_Rubros.tdf_tts_id == 14)
                {
                    //Si es un Cargo por Terceros enviarlo como excento
                    Tipo_Contribuyente_Temporal = 1;//Exento
                    Rubro_Bean = Contabilizacion_Automatica_CN.Calcular_Impuestos(Factura_Automatica.tfa_pai_id, Factura_Automatica.tfa_conta_id, Factura_Automatica.tfa_moneda, Detalle_Rubros.tdf_tts_id, Detalle_Rubros.tdf_rub_id, Detalle_Rubros.tdf_montosinimpuesto, 1, Empresa_Bean);
                }
                else
                {
                    //Si no es un Cargo por Terceros enviarlo como afecto o excento en base a Master Aimar
                    if (Tipo_Cargo_Temporal == 1)
                    {
                        //Cargo Local
                        Tipo_Contribuyente_Temporal = Tipo_Contribuyente_Temporal;//Afecto a impuestos en Base a Master Aimar
                    }
                    else if (Tipo_Cargo_Temporal == 2)
                    {
                        //Cargo Internacional
                        Tipo_Contribuyente_Temporal = 1;//Exento de Impuestos
                    }
                    Rubro_Bean = Contabilizacion_Automatica_CN.Calcular_Impuestos(Factura_Automatica.tfa_pai_id, Factura_Automatica.tfa_conta_id, Factura_Automatica.tfa_moneda, Detalle_Rubros.tdf_tts_id, Detalle_Rubros.tdf_rub_id, Detalle_Rubros.tdf_montosinimpuesto, Tipo_Contribuyente_Temporal, Empresa_Bean);
                }
                Detalle_Rubros.tdf_ttm_id = int.Parse(Bean_Cargo.strC20);
                Detalle_Rubros.tdf_montosinimpuesto = Rubro_Bean.rubroSubTot;
                Detalle_Rubros.tdf_impuesto = Rubro_Bean.rubroImpuesto;
                Detalle_Rubros.tdf_monto = Rubro_Bean.rubroTot;
                Detalle_Rubros.tdf_total_equivalente = Rubro_Bean.rubroTotD;

                Factura_Automatica.tfa_sub_total += Detalle_Rubros.tdf_montosinimpuesto;
                Factura_Automatica.tfa_impuesto += Detalle_Rubros.tdf_impuesto;
                Factura_Automatica.tfa_total += Detalle_Rubros.tdf_monto;
                Factura_Automatica.tfa_total_eq += Detalle_Rubros.tdf_total_equivalente;

                Detalle_Rubros.tdf_tfa_id = 0;
                Detalle_Rubros.tdf_ttr_id = 1;
                #region Calcular Total Equivalente
                if (Factura_Automatica.tfa_moneda == 8)
                {
                    decimal Tipo_Cambio_Temporal = DB.getTipoCambioHoy(Factura_Automatica.tfa_pai_id);
                    Factura_Automatica.tfa_impuesto_eq += Math.Round(Detalle_Rubros.tdf_impuesto * (double)Tipo_Cambio_Temporal, 2);
                    Factura_Automatica.tfa_sub_total_eq += Math.Round(Detalle_Rubros.tdf_montosinimpuesto * (double)Tipo_Cambio_Temporal, 2);
                }
                else
                {
                    decimal Tipo_Cambio_Temporal = DB.getTipoCambioHoy(Factura_Automatica.tfa_pai_id);
                    Factura_Automatica.tfa_impuesto_eq += Math.Round(Detalle_Rubros.tdf_impuesto / (double)Tipo_Cambio_Temporal, 2);
                    Factura_Automatica.tfa_sub_total_eq += Math.Round(Detalle_Rubros.tdf_montosinimpuesto / (double)Tipo_Cambio_Temporal, 2);
                }
                #endregion
                Detalle_Rubros.tdf_comentarios = "";
                int transID = 0;
                #region Definir TTT_ID = Tipo Transaccion para cuenta de concentracion
                if (Factura_Automatica.tfa_tpi_id == 10)
                {
                    transID = 108;
                }
                else
                {
                    if (Factura_Automatica.tfa_conta_id == 2)
                    {
                        transID = 7;
                    }
                    else
                    {
                        transID = 1;
                    }
                }
                #endregion
                int tttID = 0;
                if (Factura_Automatica.tfa_conta_id == 2)
                {
                    tttID = 7;
                }
                else
                {
                    tttID = 1;
                }
                Detalle_Rubros.cta_haber = (ArrayList)DB.getCtaContablebyRubro("haber", (int)Detalle_Rubros.tdf_rub_id, Factura_Automatica.tfa_pai_id, tttID, Factura_Automatica.tfa_tti_id, Factura_Automatica.tfa_moneda, Factura_Automatica.tfa_tie_id, tipo_cobro, Factura_Automatica.tfa_conta_id, Detalle_Rubros.tdf_tts_id);
                if (Detalle_Rubros.cta_haber == null)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    string nombre_servicio = "";
                    string nombre_rubro = "";
                    RE_GenericBean Bean_Rubro_Aux = null;
                    nombre_servicio = Utility.TraducirServiciotoSTR(Detalle_Rubros.tdf_tts_id);
                    Bean_Rubro_Aux = DB.getRubro(Detalle_Rubros.tdf_rub_id);
                    nombre_rubro = Bean_Rubro_Aux.strC1;
                    Arr_Result.Add("No existe combinacion de cuentas contables para el Servicio " + nombre_servicio + " y Rubro " + nombre_rubro + " con ID (" + Detalle_Rubros.tdf_rub_id + "), en la Empresa " + Empresa_Bean.Nombre_Sistema + " para emitir Factura.");
                    return Arr_Result;
                }
                if (Detalle_Rubros.cta_haber.Count == 0)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    string nombre_servicio = "";
                    string nombre_rubro = "";
                    RE_GenericBean Bean_Rubro_Aux = null;
                    nombre_servicio = Utility.TraducirServiciotoSTR(Detalle_Rubros.tdf_tts_id);
                    Bean_Rubro_Aux = DB.getRubro(Detalle_Rubros.tdf_rub_id);
                    nombre_rubro = Bean_Rubro_Aux.strC1;
                    Arr_Result.Add("No existe combinacion de cuentas contables para el Servicio " + nombre_servicio + " y Rubro " + nombre_rubro + " con ID (" + Detalle_Rubros.tdf_rub_id + "), en la Empresa " + Empresa_Bean.Nombre_Sistema + " para emitir Factura.");
                    return Arr_Result;
                }
                Factura_Automatica.tfa_ttt_id = transID;
                int _matOpID_Factura = DB.getMatrizOperacionID(transID, Factura_Automatica.tfa_moneda, Factura_Automatica.tfa_pai_id, Factura_Automatica.tfa_conta_id);
                ArrayList ctas_cargo_Factura = (ArrayList)DB.getMatrizConfiguracion_ingreso_egreso(_matOpID_Factura, "Cargo");
                if ((ctas_cargo_Factura == null) || (ctas_cargo_Factura.Count == 0))
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    Arr_Result.Add("No Existe configuracion contable para emitir la Factura , por favor pongase en contacto con el Contador");
                    return Arr_Result;
                }
                Factura_Automatica.ctas_abono = ctas_cargo_Factura;
                if (Factura_Automatica.Arr_Detalle_Facturacion == null) Factura_Automatica.Arr_Detalle_Facturacion = new ArrayList();
                Factura_Automatica.Arr_Detalle_Facturacion.Add(Detalle_Rubros);
                Arr_Result = new ArrayList();
                Arr_Result.Add("1");
                Arr_Result.Add(Factura_Automatica);
            }
            #endregion
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            Arr_Result = new ArrayList();
            Arr_Result.Add("Existio un error al tratar de Construir la Factura Automatica");
            Arr_Result.Add(null);
            return Arr_Result;
        }
        #endregion
        return Arr_Result;
    }
    public static ArrayList Construir_Nota_Debito_SCA(int SesionID, RE_GenericBean Bean_Transaccion, ArrayList Arr_Detalle_Transacciones)
    {
        #region Construir Nota de Debito Sistema de Contabilizacion Automatica
        ArrayList Arr_Result = null;
        int tipo_cobro = 1;//prepaid, collect
        string sql = "";
        Rubros Rubro_Bean = null;
        try
        {
            PaisBean Empresa_Bean = DB.getPais(int.Parse(Bean_Transaccion.strC50));
            RE_GenericBean Bean_Sesion = Contabilizacion_Automatica_CAD.Obtener_Detalle_Sesion_Reconciliacon_Carga(SesionID);

            Bean_Datos_BL Datos_BL = null;
            if (Bean_Transaccion.strC52.ToUpper() == "FALSE")
            {
                Datos_BL = Contabilizacion_Automatica_CAD.Get_DatosBL_X_Traficos(int.Parse(Bean_Sesion.strC4), int.Parse(Bean_Sesion.strC6), int.Parse(Bean_Transaccion.strC57), Empresa_Bean);
            }
            else
            {
                #region Obtener Datos RO Adicional
                Datos_BL = new Bean_Datos_BL();
                Datos_BL.BLID = int.Parse(Bean_Transaccion.strC53);
                Datos_BL.Routing = Bean_Transaccion.strC54;
                Datos_BL.ttoID = int.Parse(Bean_Transaccion.strC55);
                Datos_BL.Cliente = int.Parse(Bean_Transaccion.strC12);
                Datos_BL.Import_Export = 1;//Import
                #endregion
            }
            
            sql = "";

            if ((Bean_Sesion.strC2 != "6") && (Bean_Sesion.strC2 != "25"))
            {
                if (Bean_Transaccion.strC10 == "3")
                {
                    //Todas las Empresas que no sean de Panama
                    sql = "and trcs_sis_id=" + Bean_Sesion.strC4 + " and trcs_tto_id=" + Bean_Sesion.strC6 + " and trcs_empresa_id=" + Empresa_Bean.ID + " and trcs_ttr_id=" + Bean_Transaccion.strC8 + " and trcs_conta_id=" + Bean_Transaccion.strC18 + " and trcs_moneda_id=" + Bean_Transaccion.strC20 + " and trcs_tipo_operacion=1 ";
                }
                else
                {
                    //Solo Empresas de Panama
                    sql = "and trcs_sis_id=" + Bean_Sesion.strC4 + " and trcs_tto_id=" + Bean_Sesion.strC6 + " and trcs_empresa_id=" + Empresa_Bean.ID + " and trcs_ttr_id=" + Bean_Transaccion.strC8 + " and trcs_conta_id=" + Bean_Transaccion.strC18 + " and trcs_moneda_id=" + Bean_Transaccion.strC20 + " and trcs_tipo_operacion=2 ";
                }
            }
            else
            {
                if (Bean_Transaccion.strC10 == "3")
                {
                    //Todas las Empresas que no sean de Panama
                    sql = "and trcs_sis_id=" + Bean_Sesion.strC4 + " and trcs_tto_id=" + Bean_Sesion.strC6 + " and trcs_empresa_id=" + Empresa_Bean.ID + " and trcs_ttr_id=" + Bean_Transaccion.strC8 + " and trcs_conta_id=" + Bean_Transaccion.strC18 + " and trcs_moneda_id=" + Bean_Transaccion.strC20 + " and trcs_tipo_operacion=1 and trcs_sucursal_id=" + Bean_Sesion.strC22 + " ";
                }
                else
                {
                    //Solo Empresas de Panama
                    sql = "and trcs_sis_id=" + Bean_Sesion.strC4 + " and trcs_tto_id=" + Bean_Sesion.strC6 + " and trcs_empresa_id=" + Empresa_Bean.ID + " and trcs_ttr_id=" + Bean_Transaccion.strC8 + " and trcs_conta_id=" + Bean_Transaccion.strC18 + " and trcs_moneda_id=" + Bean_Transaccion.strC20 + " and trcs_tipo_operacion=2 and trcs_sucursal_id=" + Bean_Sesion.strC22 + " ";
                }
            }

            RE_GenericBean Bean_Configuracion_Serie = Contabilizacion_Automatica_CAD.Obtener_Configuracion_Serie_Contabilizacion_Automatica(sql);
            if (Bean_Configuracion_Serie == null)
            {
                Arr_Result = new ArrayList();
                Arr_Result.Add("0");
                Arr_Result.Add("Existio un error al Tratar de Obtener la Serie de la Nota de Debito");
                return Arr_Result;
            }
            else if (Bean_Configuracion_Serie.strC17 == "")
            {
                Arr_Result = new ArrayList();
                Arr_Result.Add("0");
                if ((Bean_Sesion.strC2 != "6") && (Bean_Sesion.strC2 != "25"))
                {
                    //Todas las Empresas que no sean de Panama
                    Arr_Result.Add("No existe Serie Configurada para Contabilizar la Nota de Debito.: (" + Bean_Transaccion.strC1 + " , " + Bean_Transaccion.strC4 + ")");
                }
                else
                {
                    //Solo Empresas de Panama
                    SucursalBean Suc_Temporal = null;
                    Suc_Temporal = DB.getSucursal(int.Parse(Bean_Sesion.strC22));
                    Arr_Result.Add("No existe Serie Configurada para Contabilizar la Nota de Debito.: (" + Bean_Transaccion.strC1 + " , " + Bean_Transaccion.strC4 + ") en el Departamento.: " + Suc_Temporal.Nombre + " ");
                    Suc_Temporal = null;
                }
                return Arr_Result;
            }

            #region Validacion Notas de Debito Intercompanys Costa Rica
            if (Bean_Transaccion.strC10 == "10")//Intercompany
            {
                if (Bean_Configuracion_Serie.strC6 == "5")//Aimar Costa Rica
                {
                    if (Bean_Configuracion_Serie.strC8 == "4")//Notas de Debito
                    {
                        if (Bean_Configuracion_Serie.strC16 == "2")//Operaciones
                        {
                            Bean_Configuracion_Serie.strC1 = Bean_Configuracion_Serie.strC1;//trcs_id
                            Bean_Configuracion_Serie.strC2 = Bean_Configuracion_Serie.strC2;//trcs_sis_id
                            Bean_Configuracion_Serie.strC3 = Bean_Configuracion_Serie.strC3;//tsis_nombre
                            Bean_Configuracion_Serie.strC4 = Bean_Configuracion_Serie.strC4;//trcs_tto_id
                            Bean_Configuracion_Serie.strC5 = Bean_Configuracion_Serie.strC5;//tto_nombre
                            Bean_Configuracion_Serie.strC6 = Bean_Configuracion_Serie.strC6;//trcs_empresa_id
                            Bean_Configuracion_Serie.strC7 = Bean_Configuracion_Serie.strC7;//pai_nombre_sistema
                            Bean_Configuracion_Serie.strC8 = Bean_Configuracion_Serie.strC8;//trcs_ttr_id
                            Bean_Configuracion_Serie.strC9 = Bean_Configuracion_Serie.strC9;//ttr_nombre
                            Bean_Configuracion_Serie.strC10 = Bean_Configuracion_Serie.strC10;//trcs_conta_id
                            Bean_Configuracion_Serie.strC11 = Bean_Configuracion_Serie.strC11;//tcon_nombre
                            Bean_Configuracion_Serie.strC12 = Bean_Configuracion_Serie.strC12;//trcs_moneda_id
                            Bean_Configuracion_Serie.strC13 = Bean_Configuracion_Serie.strC13;//ttm_nombre
                            Bean_Configuracion_Serie.strC14 = "117";//trcs_sucursal_id - Contabilidad
                            Bean_Configuracion_Serie.strC15 = "CONTABILIDAD";//suc_nombre
                            Bean_Configuracion_Serie.strC16 = Bean_Configuracion_Serie.strC16;//trcs_tipo_operacion
                            Bean_Configuracion_Serie.intC1 = 1;//Electronica
                            if (Bean_Configuracion_Serie.strC12 == "5")//Colones
                            {
                                //Bean_Configuracion_Serie.strC17 = "GFE-OP-ND";//trcs_serie
                                Bean_Configuracion_Serie.strC17 = "OP-C-0010000102";//trcs_serie
                            }
                            else if (Bean_Configuracion_Serie.strC12 == "8")//Dolares
                            {
                                //Bean_Configuracion_Serie.strC17 = "GFE-OP-ND-$";//trcs_serie
                                Bean_Configuracion_Serie.strC17 = "OP-$-0010000102";//trcs_serie
                            }
                        }
                    }
                }
            }
            #endregion

            Bean_Nota_Debito_Automatica Nota_Debito_Automatica = new Bean_Nota_Debito_Automatica();
            Nota_Debito_Automatica.tnd_id = 0;
            Nota_Debito_Automatica.tnd_nit = "";
            Nota_Debito_Automatica.tnd_nombre = "";
            Nota_Debito_Automatica.tnd_cli_id = Convert.ToDouble(Bean_Transaccion.strC12);
            if (Bean_Transaccion.strC10 == "3")
            {
                #region Cargar Cliente
                //Cliente
                RE_GenericBean Bean_Cliente_Aux = DB.getDataClient(Convert.ToDouble(Bean_Transaccion.strC12));
                if (Bean_Cliente_Aux == null)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    Arr_Result.Add("El cliente con ID.: " + Datos_BL.Cliente.ToString() + " es invalido ");
                    return Arr_Result;
                }
                Nota_Debito_Automatica.tnd_nit = Bean_Cliente_Aux.strC1;
                Nota_Debito_Automatica.tnd_nombre = Bean_Cliente_Aux.strC2;
                Nota_Debito_Automatica.tnd_direccion = Bean_Cliente_Aux.strC4;
                Nota_Debito_Automatica.tnd_razon = Bean_Cliente_Aux.strC3;
                Nota_Debito_Automatica.tnd_tti_id = 1;
                Nota_Debito_Automatica.tnd_correo_documento_electronico = Bean_Cliente_Aux.strC8;
                #endregion
            }
            else if (Bean_Transaccion.strC10 == "2")
            {
                #region Cargar Agente
                RE_GenericBean Bean_Agente_Aux = DB.getAgenteData(Convert.ToInt32(Bean_Transaccion.strC12), "");
                if (Bean_Agente_Aux == null)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    Arr_Result.Add("El Agente con ID.: " + Datos_BL.Cliente.ToString() + " es invalido ");
                    return Arr_Result;
                }
                Nota_Debito_Automatica.tnd_nit = Bean_Agente_Aux.strC6;
                Nota_Debito_Automatica.tnd_nombre = Bean_Agente_Aux.strC1;
                Nota_Debito_Automatica.tnd_direccion = Bean_Agente_Aux.strC2;
                Nota_Debito_Automatica.tnd_razon = Bean_Agente_Aux.strC1;
                Nota_Debito_Automatica.tnd_tti_id = 1;
                #endregion
            }
            else if (Bean_Transaccion.strC10 == "4")
            {
                #region Cargar Proveedor
                RE_GenericBean Bean_Proveedor_Aux = DB.getProveedorData(Convert.ToInt32(Bean_Transaccion.strC12), "");
                if (Bean_Proveedor_Aux == null)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    Arr_Result.Add("El Proveedor con ID.: " + Datos_BL.Cliente.ToString() + " es invalido ");
                    return Arr_Result;
                }
                Nota_Debito_Automatica.tnd_nit = Bean_Proveedor_Aux.strC1;
                Nota_Debito_Automatica.tnd_nombre = Bean_Proveedor_Aux.strC2;
                Nota_Debito_Automatica.tnd_direccion = Bean_Proveedor_Aux.strC5;
                Nota_Debito_Automatica.tnd_razon = Bean_Proveedor_Aux.strC2;
                Nota_Debito_Automatica.tnd_tti_id = 1;
                #endregion
            }
            else if (Bean_Transaccion.strC10 == "5")
            {
                #region Cargar Naviera
                RE_GenericBean Bean_Naviera_Aux = DB.getNavieraData(Convert.ToInt32(Bean_Transaccion.strC12));
                if (Bean_Naviera_Aux == null)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    Arr_Result.Add("La Naviera con ID.: " + Datos_BL.Cliente.ToString() + " es invalida ");
                    return Arr_Result;
                }
                Nota_Debito_Automatica.tnd_nit = Bean_Naviera_Aux.strC2;
                Nota_Debito_Automatica.tnd_nombre = Bean_Naviera_Aux.strC1;
                Nota_Debito_Automatica.tnd_direccion = "";
                Nota_Debito_Automatica.tnd_razon = Bean_Naviera_Aux.strC1;
                Nota_Debito_Automatica.tnd_tti_id = 1;
                #endregion
            }
            else if (Bean_Transaccion.strC10 == "5")
            {
                #region Cargar Linea Aerea
                RE_GenericBean Bean_Linea_Aerea_Aux = DB.getCarriersData(Convert.ToInt32(Bean_Transaccion.strC12));
                if (Bean_Linea_Aerea_Aux == null)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    Arr_Result.Add("La Linea Aerea con ID.: " + Datos_BL.Cliente.ToString() + " es invalida ");
                    return Arr_Result;
                }
                Nota_Debito_Automatica.tnd_nit = Bean_Linea_Aerea_Aux.strC2;
                Nota_Debito_Automatica.tnd_nombre = Bean_Linea_Aerea_Aux.strC1;
                Nota_Debito_Automatica.tnd_direccion = "";
                Nota_Debito_Automatica.tnd_razon = Bean_Linea_Aerea_Aux.strC1;
                Nota_Debito_Automatica.tnd_tti_id = 1;
                #endregion
            }
            else if (Bean_Transaccion.strC10 == "10")
            {
                #region Cargar Intercompany
                RE_GenericBean Bean_Intercompany_Aux = DB.getIntercompanyData(Convert.ToInt32(Bean_Transaccion.strC12));
                if (Bean_Intercompany_Aux == null)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    Arr_Result.Add("El Intercompany con ID.: " + Datos_BL.Cliente.ToString() + " es invalido ");
                    return Arr_Result;
                }
                Nota_Debito_Automatica.tnd_nit = Bean_Intercompany_Aux.strC2;
                Nota_Debito_Automatica.tnd_nombre = Bean_Intercompany_Aux.strC7;
                Nota_Debito_Automatica.tnd_direccion = Bean_Intercompany_Aux.strC4;
                Nota_Debito_Automatica.tnd_razon = Bean_Intercompany_Aux.strC7;
                Nota_Debito_Automatica.tnd_tti_id = 1;
                #endregion
            }
            Nota_Debito_Automatica.tnd_fecha_emision = DB.getDateTimeNow().Substring(0, 19);
            Nota_Debito_Automatica.tnd_total = 0;
            Nota_Debito_Automatica.tnd_pai_id = Empresa_Bean.ID;
            Nota_Debito_Automatica.tnd_ted_id = 1;
            Nota_Debito_Automatica.tnd_observacion = "";
            Nota_Debito_Automatica.tnd_usu_id = Bean_Transaccion.strC46;
            Nota_Debito_Automatica.tnd_moneda = int.Parse(Bean_Transaccion.strC20);
            Nota_Debito_Automatica.tnd_hbl = Datos_BL.Hbl;
            Nota_Debito_Automatica.tnd_mbl = Datos_BL.Mbl;
            Nota_Debito_Automatica.tnd_contenedor = Datos_BL.Contenedor;
            Nota_Debito_Automatica.tnd_routing = Datos_BL.Routing;
            Nota_Debito_Automatica.tnd_referencia = "";
            Nota_Debito_Automatica.tnd_serie = Bean_Configuracion_Serie.strC17;
            Nota_Debito_Automatica.tnd_suc_id = int.Parse(Bean_Configuracion_Serie.strC14);
            Nota_Debito_Automatica.tnd_correlativo = 0;
            Nota_Debito_Automatica.tnd_tpi_id = int.Parse(Bean_Transaccion.strC10);
            Nota_Debito_Automatica.tnd_tcon_id = int.Parse(Bean_Transaccion.strC18);
            Nota_Debito_Automatica.tnd_fecha_pago = DateTime.Now.ToString("yyyy-MM-dd");
            Nota_Debito_Automatica.tnd_sub_total = 0;
            Nota_Debito_Automatica.tnd_impuesto = 0;
            sql = "select nombre from navieras where activo=true and id_naviera=" + Datos_BL.Naviera;
            Nota_Debito_Automatica.tnd_naviera = DB.getName(sql);
            Nota_Debito_Automatica.tnd_vapor = Datos_BL.Vapor;
            sql = "select nombre_cliente from clientes where id_cliente=" + Datos_BL.Shipper;
            Nota_Debito_Automatica.tnd_shipper = DB.getName(sql);
            Nota_Debito_Automatica.tnd_ordenpo = "";
            sql = "select nombre_cliente from clientes where id_cliente=" + Datos_BL.Consignatario;
            Nota_Debito_Automatica.tnd_consignee = DB.getName(sql);
            sql = "select namees from commodities where commodityid =" + Datos_BL.Comodity;
            Nota_Debito_Automatica.tnd_comodity = DB.getName(sql);
            Nota_Debito_Automatica.tnd_paquetes = (string)DB.GetNombreTipoBulto(Datos_BL.Tipo_Paquete);
            Nota_Debito_Automatica.tnd_peso = Datos_BL.Peso;
            Nota_Debito_Automatica.tnd_dua_salida = "";
            Nota_Debito_Automatica.tnd_vendedor1 = Datos_BL.Vendedor1;
            Nota_Debito_Automatica.tnd_vendedor2 = Datos_BL.Vendedor2;
            Nota_Debito_Automatica.tnd_id_shipper = Datos_BL.Shipper;
            Nota_Debito_Automatica.tnd_id_consignee = Datos_BL.Consignatario;
            Nota_Debito_Automatica.tnd_sub_total_eq = 0;
            Nota_Debito_Automatica.tnd_impuesto_eq = 0;
            Nota_Debito_Automatica.tnd_total_eq = 0;
            Nota_Debito_Automatica.tnd_tie_id = Datos_BL.Import_Export;
            Nota_Debito_Automatica.tnd_tie_id = 1;//Toda la Carga enviada a Colectar se vuelve Import
            Nota_Debito_Automatica.tnd_ttc_id = tipo_cobro;
            Nota_Debito_Automatica.tnd_reciboaduana = "";
            Nota_Debito_Automatica.tnd_volumen = Datos_BL.Volumen;
            Nota_Debito_Automatica.tnd_dua_ingreso = "";
            Nota_Debito_Automatica.tnd_cant_paquetes = Datos_BL.Paquetes;
            Nota_Debito_Automatica.tnd_agente_id = Datos_BL.Agente;
            sql = "select agente from agentes where activo=true and agente_id=" + Datos_BL.Agente;
            Nota_Debito_Automatica.tnd_agente = DB.getName(sql);
            Nota_Debito_Automatica.tnd_fiscal = true;
            Nota_Debito_Automatica.tnd_fecha_libro_compras = DateTime.Now.ToString("yyyy-MM-dd");
            Nota_Debito_Automatica.tnd_blid = Datos_BL.BLID;
            Nota_Debito_Automatica.tnd_blid = int.Parse(Bean_Transaccion.strC57);
            Nota_Debito_Automatica.tnd_tto_id = Datos_BL.ttoID;
            Nota_Debito_Automatica.tnd_bien_serv = 2;
            Nota_Debito_Automatica.tnd_esignature = "-";
            Nota_Debito_Automatica.tnd_fac_electronica = Bean_Configuracion_Serie.intC1;
            Nota_Debito_Automatica.tnd_internal_reference = "0";
            Nota_Debito_Automatica.tnd_guid = "0";
            Nota_Debito_Automatica.tnd_referencia_correo = "-";
            Nota_Debito_Automatica.tnd_innerxml = "";
            Nota_Debito_Automatica.tnd_fecha_batch = DB.getDateTimeNow().Substring(0, 10);
            Nota_Debito_Automatica.tnd_tti_id = 1;//Excento
            Nota_Debito_Automatica.tnd_ttd_id = 3;
            #region Generar Detalle de Rubros
            Bean_Detalle_Rubros Detalle_Rubros = null;
            foreach (RE_GenericBean Bean_Cargo in Arr_Detalle_Transacciones)
            {
                Detalle_Rubros = new Bean_Detalle_Rubros();
                Detalle_Rubros.tdf_cargo_id = int.Parse(Bean_Cargo.strC51);
                Detalle_Rubros.tdf_tts_id = int.Parse(Bean_Cargo.strC14);
                Detalle_Rubros.tdf_rub_id = int.Parse(Bean_Cargo.strC16);
                Detalle_Rubros.tdf_montosinimpuesto = Convert.ToDouble(Bean_Cargo.strC22);
                Rubro_Bean = Contabilizacion_Automatica_CN.Calcular_Impuestos(Nota_Debito_Automatica.tnd_pai_id, Nota_Debito_Automatica.tnd_tcon_id, Nota_Debito_Automatica.tnd_moneda, Detalle_Rubros.tdf_tts_id, Detalle_Rubros.tdf_rub_id, Detalle_Rubros.tdf_montosinimpuesto, Nota_Debito_Automatica.tnd_tti_id, Empresa_Bean);
                Detalle_Rubros.tdf_ttm_id = int.Parse(Bean_Cargo.strC20);
                Detalle_Rubros.tdf_montosinimpuesto = Rubro_Bean.rubroSubTot;
                Detalle_Rubros.tdf_impuesto = Rubro_Bean.rubroImpuesto;
                Detalle_Rubros.tdf_monto = Rubro_Bean.rubroTot;
                Detalle_Rubros.tdf_total_equivalente = Rubro_Bean.rubroTotD;

                Nota_Debito_Automatica.tnd_sub_total += Detalle_Rubros.tdf_montosinimpuesto;
                Nota_Debito_Automatica.tnd_impuesto += Detalle_Rubros.tdf_impuesto;
                Nota_Debito_Automatica.tnd_total += Detalle_Rubros.tdf_monto;
                Nota_Debito_Automatica.tnd_total_eq += Detalle_Rubros.tdf_total_equivalente;

                Detalle_Rubros.tdf_tfa_id = 0;
                Detalle_Rubros.tdf_ttr_id = 4;
                #region Calcular Total Equivalente
                if (Nota_Debito_Automatica.tnd_moneda == 8)
                {
                    decimal Tipo_Cambio_Temporal = DB.getTipoCambioHoy(Nota_Debito_Automatica.tnd_pai_id);
                    Nota_Debito_Automatica.tnd_impuesto_eq += Math.Round(Detalle_Rubros.tdf_impuesto * (double)Tipo_Cambio_Temporal, 2);
                    Nota_Debito_Automatica.tnd_sub_total_eq += Math.Round(Detalle_Rubros.tdf_montosinimpuesto * (double)Tipo_Cambio_Temporal, 2);
                }
                else
                {
                    decimal Tipo_Cambio_Temporal = DB.getTipoCambioHoy(Nota_Debito_Automatica.tnd_pai_id);
                    Nota_Debito_Automatica.tnd_impuesto_eq += Math.Round(Detalle_Rubros.tdf_impuesto / (double)Tipo_Cambio_Temporal, 2);
                    Nota_Debito_Automatica.tnd_sub_total_eq += Math.Round(Detalle_Rubros.tdf_montosinimpuesto / (double)Tipo_Cambio_Temporal, 2);
                }
                #endregion
                Detalle_Rubros.tdf_comentarios = "";
                int transID = 6;
                #region Definir TTT_ID = Tipo de Transaccion Cuenta de Concentracion
                if (Nota_Debito_Automatica.tnd_tpi_id == 3)
                {
                    if (Nota_Debito_Automatica.tnd_tcon_id == 1)
                    {
                        transID = 1;
                    }
                    else
                    {
                        transID = 7;
                    }
                }
                else if (Nota_Debito_Automatica.tnd_tpi_id == 2)
                {
                    transID = 59;
                }
                else if (Nota_Debito_Automatica.tnd_tpi_id == 4)
                {
                    transID = 52;
                }
                else if (Nota_Debito_Automatica.tnd_tpi_id == 5)
                {
                    transID = 60;
                }
                else if (Nota_Debito_Automatica.tnd_tpi_id == 6)
                {
                    transID = 61;
                }
                #endregion
                int tttID = 6;
                if (Nota_Debito_Automatica.tnd_tcon_id == 1)
                {
                    tttID = 1;
                }
                else
                {
                    tttID = 7;
                }
                Detalle_Rubros.cta_haber = (ArrayList)DB.getCtaContablebyRubro("haber", (int)Detalle_Rubros.tdf_rub_id, Nota_Debito_Automatica.tnd_pai_id, tttID, Nota_Debito_Automatica.tnd_tti_id, Nota_Debito_Automatica.tnd_moneda, Nota_Debito_Automatica.tnd_tie_id, tipo_cobro, Nota_Debito_Automatica.tnd_tcon_id, Detalle_Rubros.tdf_tts_id);
                if (Detalle_Rubros.cta_haber == null)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    string nombre_servicio = "";
                    string nombre_rubro = "";
                    RE_GenericBean Bean_Rubro_Aux = null;
                    nombre_servicio = Utility.TraducirServiciotoSTR(Detalle_Rubros.tdf_tts_id);
                    Bean_Rubro_Aux = DB.getRubro(Detalle_Rubros.tdf_rub_id);
                    nombre_rubro = Bean_Rubro_Aux.strC1;
                    Arr_Result.Add("No existe combinacion de cuentas contables para el Servicio " + nombre_servicio + " y Rubro " + nombre_rubro + " con ID (" + Detalle_Rubros.tdf_rub_id + "), en la Empresa " + Empresa_Bean.Nombre_Sistema + " para emitir Nota de Debito.");
                    return Arr_Result;
                }
                if (Detalle_Rubros.cta_haber.Count == 0)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    string nombre_servicio = "";
                    string nombre_rubro = "";
                    RE_GenericBean Bean_Rubro_Aux = null;
                    nombre_servicio = Utility.TraducirServiciotoSTR(Detalle_Rubros.tdf_tts_id);
                    Bean_Rubro_Aux = DB.getRubro(Detalle_Rubros.tdf_rub_id);
                    nombre_rubro = Bean_Rubro_Aux.strC1;
                    Arr_Result.Add("No existe combinacion de cuentas contables para el Servicio " + nombre_servicio + " y Rubro " + nombre_rubro + " con ID (" + Detalle_Rubros.tdf_rub_id + "), en la Empresa " + Empresa_Bean.Nombre_Sistema + " para emitir Nota de Debito.");
                    return Arr_Result;
                }
                //int _matOpID_Destino = DB.getMatrizOperacionID(tttID, Nota_Debito_Automatica.tnd_moneda, Nota_Debito_Automatica.tnd_pai_id, Nota_Debito_Automatica.tnd_tcon_id);
                int _matOpID_Destino = DB.getMatrizOperacionID(transID, Nota_Debito_Automatica.tnd_moneda, Nota_Debito_Automatica.tnd_pai_id, Nota_Debito_Automatica.tnd_tcon_id);
                ArrayList ctas_cargo_Destino = (ArrayList)DB.getMatrizConfiguracion_ingreso_egreso(_matOpID_Destino, "Cargo");
                if ((ctas_cargo_Destino == null) || (ctas_cargo_Destino.Count == 0))
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    Arr_Result.Add("No Existe configuracion contable para la Nota de Debito , por favor pongase en contacto con el Contador");
                    return Arr_Result;
                }
                Nota_Debito_Automatica.ctas_abono = ctas_cargo_Destino;
                Nota_Debito_Automatica.tnd_ttt_id = transID;
                if (Nota_Debito_Automatica.Arr_Detalle_Facturacion == null) Nota_Debito_Automatica.Arr_Detalle_Facturacion = new ArrayList();
                Nota_Debito_Automatica.Arr_Detalle_Facturacion.Add(Detalle_Rubros);
                Arr_Result = new ArrayList();
                Arr_Result.Add("1");
                Arr_Result.Add(Nota_Debito_Automatica);
            }
            #endregion
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            Arr_Result = new ArrayList();
            Arr_Result.Add("0");
            Arr_Result.Add("Existio un error al tratar de Construir Nota de Debito Automatica");
            Arr_Result.Add(null);
            return Arr_Result;
        }
        return Arr_Result;
        #endregion
    }
    public static ArrayList Construir_Provision_SCA(int SesionID, RE_GenericBean Bean_Transaccion, ArrayList Arr_Detalle_Transacciones)
    {
        #region Construir Provision Sistema de Contabilizacion Automatica
        ArrayList Arr_Result = null;
        int tipo_cobro = 1;//prepaid, collect
        string sql = "";
        Rubros Rubro_Bean = null;
        try
        {
            PaisBean Empresa_Bean = DB.getPais(int.Parse(Bean_Transaccion.strC50));
            RE_GenericBean Bean_Sesion = Contabilizacion_Automatica_CAD.Obtener_Detalle_Sesion_Reconciliacon_Carga(SesionID);

            Bean_Datos_BL Datos_BL = null;
            if (Bean_Transaccion.strC52.ToUpper() == "FALSE")
            {
                Datos_BL = Contabilizacion_Automatica_CAD.Get_DatosBL_X_Traficos(int.Parse(Bean_Sesion.strC4), int.Parse(Bean_Sesion.strC6), int.Parse(Bean_Transaccion.strC57), Empresa_Bean);
            }
            else
            {
                #region Obtener Datos RO Adicional
                Datos_BL = new Bean_Datos_BL();
                Datos_BL.BLID = int.Parse(Bean_Transaccion.strC53);
                Datos_BL.Routing = Bean_Transaccion.strC54;
                Datos_BL.ttoID = int.Parse(Bean_Transaccion.strC55);
                Datos_BL.Cliente = int.Parse(Bean_Transaccion.strC12);
                Datos_BL.Import_Export = 1;//Import
                #endregion
            }

            sql = "";
            sql = "and trcs_sis_id=" + Bean_Sesion.strC4 + " and trcs_tto_id=" + Bean_Sesion.strC6 + " and trcs_empresa_id=" + Empresa_Bean.ID + " and trcs_ttr_id=" + Bean_Transaccion.strC8 + " and trcs_conta_id=" + Bean_Transaccion.strC18 + " and trcs_moneda_id=" + Bean_Transaccion.strC20 + " and trcs_tipo_operacion=2 ";
            RE_GenericBean Bean_Configuracion_Serie = Contabilizacion_Automatica_CAD.Obtener_Configuracion_Serie_Contabilizacion_Automatica(sql);
            if (Bean_Configuracion_Serie == null)
            {
                Arr_Result = new ArrayList();
                Arr_Result.Add("0");
                Arr_Result.Add("Existio un error al Tratar de Obtener la Serie de la Provision");
                return Arr_Result;
            }
            else if (Bean_Configuracion_Serie.strC17 == "")
            {
                Arr_Result = new ArrayList();
                Arr_Result.Add("0");
                Arr_Result.Add("No existe Serie Configurada para Contabilizar la Provision.: (" + Bean_Transaccion.strC1 + " , " + Bean_Transaccion.strC4 + ")");
                return Arr_Result;
            }

            Bean_Provision_Automatica Provision_Automatica = new Bean_Provision_Automatica();
            Provision_Automatica.tpr_prov_id = 0;
            Provision_Automatica.tpr_tcon_id = int.Parse(Bean_Transaccion.strC18);
            Provision_Automatica.tpr_mon_id = int.Parse(Bean_Transaccion.strC20);

            if (Bean_Transaccion.strC10 == "2")
            {
                #region Cargar Agente
                RE_GenericBean Bean_Agente_Aux = DB.getAgenteData(Convert.ToInt32(Bean_Transaccion.strC12), "");
                if (Bean_Agente_Aux == null)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    Arr_Result.Add("El Agente con ID.: " + Datos_BL.Cliente.ToString() + " es invalido ");
                    return Arr_Result;
                }
                Provision_Automatica.tpr_proveedor_id = int.Parse(Bean_Transaccion.strC12);
                Provision_Automatica.tpr_tpi_id = int.Parse(Bean_Transaccion.strC10);
                Provision_Automatica.tpr_nombre = Bean_Agente_Aux.strC1;
                //if ((Provision_Automatica.tpr_tcon_id == 2) || ((Provision_Automatica.tpr_tcon_id == 1) && (Provision_Automatica.tpr_mon_id == 8)))
                //{
                //    Provision_Automatica.tpr_tti_id = 1;//Excento
                //}
                //else
                //{
                //    Provision_Automatica.tpr_tti_id = DB.getProveedorRegimen(Provision_Automatica.tpr_tpi_id, Provision_Automatica.tpr_proveedor_id.ToString());
                //}
                #region Definir Tipo de Contribuyente
                if (Provision_Automatica.tpr_tcon_id == 2)
                {
                    #region Contabilidad Financiera
                    Provision_Automatica.tpr_tti_id = 1;//Excento
                    #endregion
                }
                else if (Provision_Automatica.tpr_tcon_id == 1)
                {
                    if ((Empresa_Bean.ID == 2) || (Empresa_Bean.ID == 9) || (Empresa_Bean.ID == 26) || (Empresa_Bean.ID == 6) || (Empresa_Bean.ID == 25))
                    {
                        #region Empresas con Contabilidad USD
                        Provision_Automatica.tpr_tti_id = DB.getProveedorRegimen(Provision_Automatica.tpr_tpi_id, Provision_Automatica.tpr_proveedor_id.ToString());
                        #endregion
                    }
                    else
                    {
                        if (Provision_Automatica.tpr_mon_id == 8)
                        {
                            #region Empresas con Contabilidad Fiscal USD
                            Provision_Automatica.tpr_tti_id = 1;//Excento        
                            #endregion
                        }
                        else if (Provision_Automatica.tpr_mon_id != 8)
                        {
                            #region Contabilidad Fiscal en Moneda Local
                            Provision_Automatica.tpr_tti_id = DB.getProveedorRegimen(Provision_Automatica.tpr_tpi_id, Provision_Automatica.tpr_proveedor_id.ToString());
                            #endregion
                        }
                    }
                }
                #endregion
                #endregion
            }
            else if (Bean_Transaccion.strC10 == "4")
            {
                #region Cargar Proveedor
                RE_GenericBean Bean_Proveedor_Aux = DB.getProveedorData(Convert.ToInt32(Bean_Transaccion.strC12), "");
                if (Bean_Proveedor_Aux == null)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    Arr_Result.Add("El Proveedor con ID.: " + Datos_BL.Cliente.ToString() + " es invalido ");
                    return Arr_Result;
                }
                Provision_Automatica.tpr_proveedor_id = int.Parse(Bean_Transaccion.strC12);
                Provision_Automatica.tpr_tpi_id = int.Parse(Bean_Transaccion.strC10);
                Provision_Automatica.tpr_nombre = Bean_Proveedor_Aux.strC2;
                //if ((Provision_Automatica.tpr_tcon_id == 2) || ((Provision_Automatica.tpr_tcon_id == 1) && (Provision_Automatica.tpr_mon_id == 8)))
                //{
                //    Provision_Automatica.tpr_tti_id = 1;//Excento
                //}
                //else
                //{
                //    Provision_Automatica.tpr_tti_id = DB.getProveedorRegimen(Provision_Automatica.tpr_tpi_id, Provision_Automatica.tpr_proveedor_id.ToString());
                //}
                #region Definir Tipo de Contribuyente
                if (Provision_Automatica.tpr_tcon_id == 2)
                {
                    #region Contabilidad Financiera
                    Provision_Automatica.tpr_tti_id = 1;//Excento
                    #endregion
                }
                else if (Provision_Automatica.tpr_tcon_id == 1)
                {
                    if ((Empresa_Bean.ID == 2) || (Empresa_Bean.ID == 9) || (Empresa_Bean.ID == 26) || (Empresa_Bean.ID == 6) || (Empresa_Bean.ID == 25))
                    {
                        #region Empresas con Contabilidad USD
                        Provision_Automatica.tpr_tti_id = DB.getProveedorRegimen(Provision_Automatica.tpr_tpi_id, Provision_Automatica.tpr_proveedor_id.ToString());
                        #endregion
                    }
                    else
                    {
                        if (Provision_Automatica.tpr_mon_id == 8)
                        {
                            #region Empresas con Contabilidad Fiscal USD
                            Provision_Automatica.tpr_tti_id = 1;//Excento        
                            #endregion
                        }
                        else if (Provision_Automatica.tpr_mon_id != 8)
                        {
                            #region Contabilidad Fiscal en Moneda Local
                            Provision_Automatica.tpr_tti_id = DB.getProveedorRegimen(Provision_Automatica.tpr_tpi_id, Provision_Automatica.tpr_proveedor_id.ToString());
                            #endregion
                        }
                    }
                }
                #endregion
                #endregion
            }
            else if (Bean_Transaccion.strC10 == "5")
            {
                #region Cargar Naviera
                RE_GenericBean Bean_Naviera_Aux = DB.getNavieraData(Convert.ToInt32(Bean_Transaccion.strC12));
                if (Bean_Naviera_Aux == null)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    Arr_Result.Add("La Naviera con ID.: " + Datos_BL.Cliente.ToString() + " es invalida ");
                    return Arr_Result;
                }
                Provision_Automatica.tpr_proveedor_id = int.Parse(Bean_Transaccion.strC12);
                Provision_Automatica.tpr_tpi_id = int.Parse(Bean_Transaccion.strC10);
                Provision_Automatica.tpr_nombre = Bean_Naviera_Aux.strC1;
                //if ((Provision_Automatica.tpr_tcon_id == 2) || ((Provision_Automatica.tpr_tcon_id == 1) && (Provision_Automatica.tpr_mon_id == 8)))
                //{
                //    Provision_Automatica.tpr_tti_id = 1;//Excento
                //}
                //else
                //{
                //    Provision_Automatica.tpr_tti_id = DB.getProveedorRegimen(Provision_Automatica.tpr_tpi_id, Provision_Automatica.tpr_proveedor_id.ToString());
                //}
                #region Definir Tipo de Contribuyente
                if (Provision_Automatica.tpr_tcon_id == 2)
                {
                    #region Contabilidad Financiera
                    Provision_Automatica.tpr_tti_id = 1;//Excento
                    #endregion
                }
                else if (Provision_Automatica.tpr_tcon_id == 1)
                {
                    if ((Empresa_Bean.ID == 2) || (Empresa_Bean.ID == 9) || (Empresa_Bean.ID == 26) || (Empresa_Bean.ID == 6) || (Empresa_Bean.ID == 25))
                    {
                        #region Empresas con Contabilidad USD
                        Provision_Automatica.tpr_tti_id = DB.getProveedorRegimen(Provision_Automatica.tpr_tpi_id, Provision_Automatica.tpr_proveedor_id.ToString());
                        #endregion
                    }
                    else
                    {
                        if (Provision_Automatica.tpr_mon_id == 8)
                        {
                            #region Empresas con Contabilidad Fiscal USD
                            Provision_Automatica.tpr_tti_id = 1;//Excento        
                            #endregion
                        }
                        else if (Provision_Automatica.tpr_mon_id != 8)
                        {
                            #region Contabilidad Fiscal en Moneda Local
                            Provision_Automatica.tpr_tti_id = DB.getProveedorRegimen(Provision_Automatica.tpr_tpi_id, Provision_Automatica.tpr_proveedor_id.ToString());
                            #endregion
                        }
                    }
                }
                #endregion
                #endregion
            }
            else if (Bean_Transaccion.strC10 == "6")
            {
                #region Cargar Linea Aerea
                RE_GenericBean Bean_Linea_Aerea_Aux = DB.getCarriersData(Convert.ToInt32(Bean_Transaccion.strC12));
                if (Bean_Linea_Aerea_Aux == null)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    Arr_Result.Add("La Linea Aerea con ID.: " + Datos_BL.Cliente.ToString() + " es invalida ");
                    return Arr_Result;
                }
                Provision_Automatica.tpr_proveedor_id = int.Parse(Bean_Transaccion.strC12);
                Provision_Automatica.tpr_tpi_id = int.Parse(Bean_Transaccion.strC10);
                Provision_Automatica.tpr_nombre = Bean_Linea_Aerea_Aux.strC1;
                //if ((Provision_Automatica.tpr_tcon_id == 2) || ((Provision_Automatica.tpr_tcon_id == 1) && (Provision_Automatica.tpr_mon_id == 8)))
                //{
                //    Provision_Automatica.tpr_tti_id = 1;//Excento
                //}
                //else
                //{
                //    Provision_Automatica.tpr_tti_id = DB.getProveedorRegimen(Provision_Automatica.tpr_tpi_id, Provision_Automatica.tpr_proveedor_id.ToString());
                //}
                #region Definir Tipo de Contribuyente
                if (Provision_Automatica.tpr_tcon_id == 2)
                {
                    #region Contabilidad Financiera
                    Provision_Automatica.tpr_tti_id = 1;//Excento
                    #endregion
                }
                else if (Provision_Automatica.tpr_tcon_id == 1)
                {
                    if ((Empresa_Bean.ID == 2) || (Empresa_Bean.ID == 9) || (Empresa_Bean.ID == 26) || (Empresa_Bean.ID == 6) || (Empresa_Bean.ID == 25))
                    {
                        #region Empresas con Contabilidad USD
                        Provision_Automatica.tpr_tti_id = DB.getProveedorRegimen(Provision_Automatica.tpr_tpi_id, Provision_Automatica.tpr_proveedor_id.ToString());
                        #endregion
                    }
                    else
                    {
                        if (Provision_Automatica.tpr_mon_id == 8)
                        {
                            #region Empresas con Contabilidad Fiscal USD
                            Provision_Automatica.tpr_tti_id = 1;//Excento        
                            #endregion
                        }
                        else if (Provision_Automatica.tpr_mon_id != 8)
                        {
                            #region Contabilidad Fiscal en Moneda Local
                            Provision_Automatica.tpr_tti_id = DB.getProveedorRegimen(Provision_Automatica.tpr_tpi_id, Provision_Automatica.tpr_proveedor_id.ToString());
                            #endregion
                        }
                    }
                }
                #endregion
                #endregion
            }
            else if (Bean_Transaccion.strC10 == "10")
            {
                #region Cargar Intercompany
                RE_GenericBean Bean_Intercompany_Aux = DB.getIntercompanyData(Convert.ToInt32(Bean_Transaccion.strC12));
                if (Bean_Intercompany_Aux == null)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    Arr_Result.Add("El Intercompany con ID.: " + Datos_BL.Cliente.ToString() + " es invalido ");
                    return Arr_Result;
                }
                Provision_Automatica.tpr_proveedor_id = int.Parse(Bean_Transaccion.strC12);
                Provision_Automatica.tpr_tpi_id = int.Parse(Bean_Transaccion.strC10);
                Provision_Automatica.tpr_nombre = Bean_Intercompany_Aux.strC7;
                //if ((Provision_Automatica.tpr_tcon_id == 2) || ((Provision_Automatica.tpr_tcon_id == 1) && (Provision_Automatica.tpr_mon_id == 8)))
                //{
                //    Provision_Automatica.tpr_tti_id = 1;//Excento
                //}
                //else
                //{
                //    Provision_Automatica.tpr_tti_id = DB.getProveedorRegimen(Provision_Automatica.tpr_tpi_id, Provision_Automatica.tpr_proveedor_id.ToString());
                //}
                #region Definir Tipo de Contribuyente
                if (Provision_Automatica.tpr_tcon_id == 2)
                {
                    #region Contabilidad Financiera
                    Provision_Automatica.tpr_tti_id = 1;//Excento
                    #endregion
                }
                else if (Provision_Automatica.tpr_tcon_id == 1)
                {
                    if ((Empresa_Bean.ID == 2) || (Empresa_Bean.ID == 9) || (Empresa_Bean.ID == 26) || (Empresa_Bean.ID == 6) || (Empresa_Bean.ID == 25))
                    {
                        #region Empresas con Contabilidad USD
                        Provision_Automatica.tpr_tti_id = DB.getProveedorRegimen(Provision_Automatica.tpr_tpi_id, Provision_Automatica.tpr_proveedor_id.ToString());
                        #endregion
                    }
                    else
                    {
                        if (Provision_Automatica.tpr_mon_id == 8)
                        {
                            #region Empresas con Contabilidad Fiscal USD
                            Provision_Automatica.tpr_tti_id = 1;//Excento        
                            #endregion
                        }
                        else if (Provision_Automatica.tpr_mon_id != 8)
                        {
                            #region Contabilidad Fiscal en Moneda Local
                            Provision_Automatica.tpr_tti_id = DB.getProveedorRegimen(Provision_Automatica.tpr_tpi_id, Provision_Automatica.tpr_proveedor_id.ToString());
                            #endregion
                        }
                    }
                }
                #endregion
                #endregion
            }
            Provision_Automatica.tpr_fact_id = Bean_Transaccion.strC60;//trt_proveedor_serie
            Provision_Automatica.tpr_fact_fecha = Bean_Transaccion.strC62;//trt_proveedor_fecha
            Provision_Automatica.tpr_fecha_maxpago = Bean_Transaccion.strC62;//trt_proveedor_fecha
            #region Definir Fecha de Pago
            if (Provision_Automatica.tpr_fact_fecha != "")
            {
                string Fecha_Emision = "";
                string Fecha_Pago = "";

                Fecha_Emision = Provision_Automatica.tpr_fact_fecha;
                Fecha_Pago = Provision_Automatica.tpr_fecha_maxpago;

                DateTime _Fecha_Emision = DateTime.Parse(Fecha_Emision);
                DateTime _Fecha_Pago = DateTime.Parse(Fecha_Emision);
                //Pablo Aguilar: Validar que cuando la empresa para la transasaccion sea WMT, busque los dias de credito en la empresa de la sesion.
                int empresa_id = Empresa_Bean.ID;
                if (Empresa_Bean.ID == 30)
                {
                    empresa_id = int.Parse(Bean_Sesion.strC2);
                }
                ////
                double _Dias_Credito = DB.Get_Dias_Credito_X_Tipo_Persona_Empresa(Convert.ToInt32(Bean_Transaccion.strC12), int.Parse(Bean_Transaccion.strC10), empresa_id);
                _Fecha_Pago = _Fecha_Pago.AddDays(_Dias_Credito);

                Provision_Automatica.tpr_fact_fecha = _Fecha_Emision.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                Provision_Automatica.tpr_fecha_maxpago = _Fecha_Pago.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            #endregion
            Provision_Automatica.tpr_valor = 0;
            Provision_Automatica.tpr_afecto = 0;
            Provision_Automatica.tpr_noafecto = 0;
            Provision_Automatica.tpr_iva = 0;
            Provision_Automatica.tpr_observacion = Bean_Transaccion.strC24;
            Provision_Automatica.tpr_suc_id = int.Parse(Bean_Configuracion_Serie.strC14);
            Provision_Automatica.tpr_pai_id = Empresa_Bean.ID;
            Provision_Automatica.tpr_usu_creacion = Bean_Transaccion.strC46;
            Provision_Automatica.tpr_fecha_creacion = DateTime.Now.ToString("yyyy-MM-dd");
            Provision_Automatica.tpr_usu_acepta = Bean_Transaccion.strC46;
            Provision_Automatica.tpr_fecha_acepta = DateTime.Now.ToString("yyyy-MM-dd");
            Provision_Automatica.tpr_departamento = 0;
            Provision_Automatica.tpr_ted_id = 5;
            Provision_Automatica.tpr_serie = Bean_Configuracion_Serie.strC17;
            Provision_Automatica.tpr_serie_oc = "";
            Provision_Automatica.tpr_correlativo_oc = 0;
            Provision_Automatica.tpr_tts_id = 0;
            Provision_Automatica.tpr_hbl = Datos_BL.Hbl;
            Provision_Automatica.tpr_mbl = Datos_BL.Mbl;
            Provision_Automatica.tpr_routing = Datos_BL.Routing;
            Provision_Automatica.tpr_contenedor = Datos_BL.Contenedor;
            Provision_Automatica.tpr_correlativo = 0;
            Provision_Automatica.tpr_serie_contrasena = "";
            Provision_Automatica.tpr_contrasena_correlativo = 0;
            Provision_Automatica.tpr_valor_equivalente = 0;
            Provision_Automatica.tpr_fact_corr = Bean_Transaccion.strC61;//trt_proveedor_correlativo
            Provision_Automatica.tpr_proveedor_cajachica = "";
            Provision_Automatica.tpr_imp_exp_id = Datos_BL.Import_Export;
            Provision_Automatica.tpr_imp_exp_id = 1;//Toda la Carga enviada a Collectar se vuelve Importacion
            Provision_Automatica.tpr_bien_serv = 2;
            Provision_Automatica.tpr_proveedor_cajachica_id = 0;
            Provision_Automatica.tpr_poliza = "";
            Provision_Automatica.tpr_fecha_libro_compras = DateTime.Now.ToString("yyyy-MM-dd");
            Provision_Automatica.tpr_tto_id = Datos_BL.ttoID;
            Provision_Automatica.tpr_ruta_pais = "";
            Provision_Automatica.tpr_ruta = "";
            Provision_Automatica.tpr_blid = Datos_BL.BLID;
            Provision_Automatica.tpr_usu_modifica_regimen = "";
            Provision_Automatica.tpr_usu_anula = "";
            Provision_Automatica.tpr_fecha_anula = null;
            Provision_Automatica.tpr_toc_id = 0;
            Provision_Automatica.tpr_observacion_contrasena = "";
            Provision_Automatica.tpr_fecha_recibo_factura = null;
            Provision_Automatica.tpr_mbl_modificado = false;
            Provision_Automatica.tpr_ttd_id = 3;
            Provision_Automatica.tpr_tds_id = 0;
            #region Generar Detalle de Rubros
            Bean_Detalle_Rubros Detalle_Rubros = null;
            foreach (RE_GenericBean Bean_Cargo in Arr_Detalle_Transacciones)
            {
                Detalle_Rubros = new Bean_Detalle_Rubros();
                Detalle_Rubros.tdf_cargo_id = int.Parse(Bean_Cargo.strC51);
                Detalle_Rubros.tdf_tts_id = int.Parse(Bean_Cargo.strC14);
                Detalle_Rubros.tdf_rub_id = int.Parse(Bean_Cargo.strC16);
                Detalle_Rubros.tdf_montosinimpuesto = Convert.ToDouble(Bean_Cargo.strC22);
                #region Definir Tipo de Contribuyente en Base a Seleccion del Usuario de Trafico
                int _Tipo_CONTRIBUYENTE = 0;
                if (Provision_Automatica.tpr_tpi_id == 10)
                {
                    #region Provision Intercompanys
                    _Tipo_CONTRIBUYENTE = 1;//Excento
                    #endregion
                }
                else
                {
                    #region Contabilidad Fiscal
                    if (Provision_Automatica.tpr_tcon_id == 1)
                    {
                        #region Contabilidad Fiscal
                        _Tipo_CONTRIBUYENTE = int.Parse(Bean_Cargo.strC64);//trt_afecto_excento
                        #endregion
                    }
                    else if (Provision_Automatica.tpr_tcon_id == 2)
                    {
                        #region Contabilidad Financiera
                        if ((Provision_Automatica.tpr_pai_id == 5) || (Provision_Automatica.tpr_pai_id == 21))
                        {
                            _Tipo_CONTRIBUYENTE = int.Parse(Bean_Cargo.strC64);//trt_afecto_excento
                        }
                        else
                        {
                            _Tipo_CONTRIBUYENTE = 1;//Excento
                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion

                //Rubro_Bean = Contabilizacion_Automatica_CN.Calcular_Impuestos(Provision_Automatica.tpr_pai_id, Provision_Automatica.tpr_tcon_id, Provision_Automatica.tpr_mon_id, Detalle_Rubros.tdf_tts_id, Detalle_Rubros.tdf_rub_id, Detalle_Rubros.tdf_montosinimpuesto, Provision_Automatica.tpr_tti_id, Empresa_Bean);
                Rubro_Bean = Contabilizacion_Automatica_CN.Calcular_Impuestos(Provision_Automatica.tpr_pai_id, Provision_Automatica.tpr_tcon_id, Provision_Automatica.tpr_mon_id, Detalle_Rubros.tdf_tts_id, Detalle_Rubros.tdf_rub_id, Detalle_Rubros.tdf_montosinimpuesto, _Tipo_CONTRIBUYENTE, Empresa_Bean);

                Detalle_Rubros.tdf_ttm_id = int.Parse(Bean_Cargo.strC20);
                Detalle_Rubros.tdf_montosinimpuesto = Rubro_Bean.rubroSubTot;
                Detalle_Rubros.tdf_impuesto = Rubro_Bean.rubroImpuesto;
                Detalle_Rubros.tdf_monto = Rubro_Bean.rubroTot;
                Detalle_Rubros.tdf_total_equivalente = Rubro_Bean.rubroTotD;
                Detalle_Rubros.tdf_tfa_id = 0;
                Detalle_Rubros.tdf_ttr_id = 5;
                Detalle_Rubros.tdf_comentarios = "";
                if (Detalle_Rubros.tdf_impuesto == 0)
                {
                    Provision_Automatica.tpr_noafecto += Detalle_Rubros.tdf_montosinimpuesto;
                }
                else
                {
                    Provision_Automatica.tpr_afecto += Detalle_Rubros.tdf_montosinimpuesto;
                }
                Provision_Automatica.tpr_valor += Detalle_Rubros.tdf_monto;
                Provision_Automatica.tpr_iva += Detalle_Rubros.tdf_impuesto;
                Provision_Automatica.tpr_valor_equivalente += Detalle_Rubros.tdf_total_equivalente;
                int transID = 105;
                #region Definir TTT_ID = Tipo Transaccion de Cuenta de Concentracion
                if (Provision_Automatica.tpr_tpi_id == 2)
                {
                    transID = 15;
                }
                else if (Provision_Automatica.tpr_tpi_id == 4)
                {
                    transID = 8;
                }
                else if (Provision_Automatica.tpr_tpi_id == 5)
                {
                    transID = 17;
                }
                else if (Provision_Automatica.tpr_tpi_id == 6)
                {
                    transID = 18;
                }
                else if (Provision_Automatica.tpr_tpi_id == 10)
                {
                    transID = 105;
                }
                #endregion
                int tttID = 15;
                //Aca se validan las Cuentas Contables de los Rubros
                Detalle_Rubros.cta_debe = (ArrayList)DB.getCtaContablebyRubro("debe", (int)Detalle_Rubros.tdf_rub_id, Provision_Automatica.tpr_pai_id, tttID, Provision_Automatica.tpr_tti_id, Provision_Automatica.tpr_mon_id, Provision_Automatica.tpr_imp_exp_id, tipo_cobro, Provision_Automatica.tpr_tcon_id, Detalle_Rubros.tdf_tts_id);
                if (Detalle_Rubros.cta_debe == null)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    string nombre_servicio = "";
                    string nombre_rubro = "";
                    RE_GenericBean Bean_Rubro_Aux = null;
                    nombre_servicio = Utility.TraducirServiciotoSTR(Detalle_Rubros.tdf_tts_id);
                    Bean_Rubro_Aux = DB.getRubro(Detalle_Rubros.tdf_rub_id);
                    nombre_rubro = Bean_Rubro_Aux.strC1;
                    Arr_Result.Add("No existe combinacion de cuentas contables para el Servicio " + nombre_servicio + " y Rubro " + nombre_rubro + " con ID (" + Detalle_Rubros.tdf_rub_id + "), en la Empresa " + Empresa_Bean.Nombre_Sistema + " para emitir Provision.");
                    return Arr_Result;
                }
                if (Detalle_Rubros.cta_debe.Count == 0)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    string nombre_servicio = "";
                    string nombre_rubro = "";
                    RE_GenericBean Bean_Rubro_Aux = null;
                    nombre_servicio = Utility.TraducirServiciotoSTR(Detalle_Rubros.tdf_tts_id);
                    Bean_Rubro_Aux = DB.getRubro(Detalle_Rubros.tdf_rub_id);
                    nombre_rubro = Bean_Rubro_Aux.strC1;
                    Arr_Result.Add("No existe combinacion de cuentas contables para el Servicio " + nombre_servicio + " y Rubro " + nombre_rubro + " con ID (" + Detalle_Rubros.tdf_rub_id + "), en la Empresa " + Empresa_Bean.Nombre_Sistema + " para emitir Provision.");
                    return Arr_Result;
                }
                if (Provision_Automatica.Arr_Detalle_Provision == null) Provision_Automatica.Arr_Detalle_Provision = new ArrayList();
                Provision_Automatica.Arr_Detalle_Provision.Add(Detalle_Rubros);

                Provision_Automatica.tpr_ttt_id = transID;
                int _matOpID_Destino = DB.getMatrizOperacionID(transID, Provision_Automatica.tpr_mon_id, Provision_Automatica.tpr_pai_id, Provision_Automatica.tpr_tcon_id);

                ArrayList ctas_cargo_Destino = (ArrayList)DB.getMatrizConfiguracion_ingreso_egreso(_matOpID_Destino, "Abono");
                if ((ctas_cargo_Destino == null) || (ctas_cargo_Destino.Count == 0))
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    Arr_Result.Add("No Existe configuracion contable para la Provision, por favor pongase en contacto con el Contador");
                    return Arr_Result;
                }
                Provision_Automatica.ctas_cargo = ctas_cargo_Destino;

                Arr_Result = new ArrayList();
                Arr_Result.Add("1");
                Arr_Result.Add(Provision_Automatica);
            }
            #endregion
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            Arr_Result = new ArrayList();
            Arr_Result.Add("0");
            Arr_Result.Add("Existio un error al tratar de Construir la Provision");
            Arr_Result.Add(null);
            return Arr_Result;
        }
        return Arr_Result;
        #endregion
    }
    public static ArrayList Construir_Provision_Intercompany_SCA(int SesionID, RE_GenericBean Bean_Transaccion, ArrayList Arr_Detalle_Transacciones)
    {
        #region Construir Provision Intercompany Sistema de Contabilizacion Automatica
        ArrayList Arr_Result = null;
        int tipo_cobro = 1;//prepaid, collect
        Rubros Rubro_Bean = null;
        try
        {

            int intercompany_ORIGEN_ID = 0;
            int intercompany_DESTINO_ID = 0;
            RE_GenericBean Intercompany_Origen = null;
            RE_GenericBean Intercompany_Destino = null;
            int idpaisORIGEN = 0;
            int idcontaORIGEN = 0;
            int idmonedaORIGEN = 0;
            int idOPERACION = 0;
            int idpaisDESTINO = 0;
            int idcontaDESTINO = 0;
            int idmonedaDESTINO = 0;
            #region Cargar Intercompany Origen y Destino
            Intercompany_Origen = (RE_GenericBean)DB.Get_Intercompany_Data(int.Parse(Bean_Transaccion.strC12));
            if (Intercompany_Origen == null)
            {
                Arr_Result = new ArrayList();
                Arr_Result.Add("0");
                Arr_Result.Add("El Intercompany Origen es invalido ");
                return Arr_Result;
            }
            intercompany_ORIGEN_ID = Intercompany_Origen.intC1;

            Intercompany_Destino = (RE_GenericBean)DB.Get_Intercompany_Data_By_Empresa(int.Parse(Bean_Transaccion.strC50));
            intercompany_DESTINO_ID = Intercompany_Destino.intC1;
            if (Intercompany_Destino == null)
            {
                Arr_Result = new ArrayList();
                Arr_Result.Add("0");
                Arr_Result.Add("El Intercompany Destino es invalido ");
                return Arr_Result;
            }
            #endregion
            idpaisORIGEN = Intercompany_Origen.intC3;
            idcontaORIGEN = int.Parse(Bean_Transaccion.strC18);
            idmonedaORIGEN = int.Parse(Bean_Transaccion.strC20);
            idOPERACION = 2;
            #region Validaciones Intercompany
            int existe_configuracion = 0;
            string sql_existencia = " and tia_pais_origen=" + idpaisORIGEN + " and tia_contabilidad_origen=" + idcontaORIGEN + " and tia_moneda_origen=" + idmonedaORIGEN + " and tia_tipo_operacion=" + 2 + " and tia_id_intercompany=" + intercompany_DESTINO_ID + " ";
            existe_configuracion = Contabilizacion_Automatica_CAD.Validar_Existencia_Intercompany_Administrativo(null, sql_existencia);
            if (existe_configuracion == -100)
            {
                Arr_Result = new ArrayList();
                Arr_Result.Add("0");
                Arr_Result.Add("Existio un Error al Tratar de validar la configuracion del Intercompany Administrativo");
                return Arr_Result;
            }
            else if (existe_configuracion > 0)
            {
                decimal Tipo_Cambio_Destino = 0;
                Tipo_Cambio_Destino = DB.getTipoCambioHoy(Intercompany_Destino.intC3);
                if (Tipo_Cambio_Destino == 0)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    Arr_Result.Add("No Existe Tipo de Cambio para realizar la Automatizacion en Sistema BAW - " + Intercompany_Destino.strC5 + ", favor notificar al personal de Contabilidad");
                    return Arr_Result;
                }
            }
            else if (existe_configuracion == 0)
            {
                Arr_Result = new ArrayList();
                Arr_Result.Add("0");
                Arr_Result.Add("No Existe Configuracion para realizar la Automatizacion del Intercompany Administrativo.: " + Intercompany_Destino.strC5 + ", porfavor pongase en contacto con el Contador");
                return Arr_Result;
            }
            #endregion
            RE_GenericBean Bean_Configuracion_Intercompany = Contabilizacion_Automatica_CN.Obtener_Configuracion_Intercompany_Administrativo(intercompany_DESTINO_ID, idpaisORIGEN, idcontaORIGEN, idmonedaORIGEN, idOPERACION);
            idpaisDESTINO = Bean_Configuracion_Intercompany.intC1;
            idcontaDESTINO = Bean_Configuracion_Intercompany.intC2;
            idmonedaDESTINO = Bean_Configuracion_Intercompany.intC3;
            PaisBean Empresa_Bean = null;
            Empresa_Bean = DB.getPais(idpaisDESTINO);

            RE_GenericBean Bean_Sesion = Contabilizacion_Automatica_CAD.Obtener_Detalle_Sesion_Reconciliacon_Carga(SesionID);
            Bean_Datos_BL Datos_BL = null;
            PaisBean Empresa_Origen_Bean = DB.getPais(idpaisORIGEN);
            Datos_BL = Contabilizacion_Automatica_CAD.Get_DatosBL_X_Traficos(int.Parse(Bean_Sesion.strC4), int.Parse(Bean_Sesion.strC6), int.Parse(Bean_Transaccion.strC57), Empresa_Origen_Bean);
            Bean_Provision_Automatica Provision_Automatica = new Bean_Provision_Automatica();
            Provision_Automatica.tpr_prov_id = 0;
            Provision_Automatica.tpr_suc_id = Bean_Configuracion_Intercompany.intC5;
            Provision_Automatica.tpr_tcon_id = idcontaDESTINO;
            Provision_Automatica.tpr_mon_id = idmonedaDESTINO;
            Provision_Automatica.tpr_proveedor_id = intercompany_ORIGEN_ID;
            Provision_Automatica.tpr_tpi_id = 10;
            Provision_Automatica.tpr_nombre = Intercompany_Origen.strC1;
            Provision_Automatica.tpr_tti_id = 1;//Excento
            Provision_Automatica.tpr_fact_id = Bean_Transaccion.strC60;//trt_proveedor_serie
            Provision_Automatica.tpr_fact_fecha = Bean_Transaccion.strC62;//trt_proveedor_fecha
            Provision_Automatica.tpr_fecha_maxpago = Bean_Transaccion.strC62;//trt_proveedor_fecha
            #region Definir Fecha de Pago
            if (Provision_Automatica.tpr_fact_fecha != "")
            {
                string Fecha_Emision = "";
                string Fecha_Pago = "";

                Fecha_Emision = Provision_Automatica.tpr_fact_fecha;
                Fecha_Pago = Provision_Automatica.tpr_fecha_maxpago;

                DateTime _Fecha_Emision = DateTime.Parse(Fecha_Emision);
                DateTime _Fecha_Pago = DateTime.Parse(Fecha_Emision);
                double _Dias_Credito = DB.Get_Dias_Credito_X_Tipo_Persona_Empresa(Convert.ToInt32(Bean_Transaccion.strC12), int.Parse(Bean_Transaccion.strC10), Empresa_Bean.ID);
                _Fecha_Pago = _Fecha_Pago.AddDays(_Dias_Credito);

                Provision_Automatica.tpr_fact_fecha = _Fecha_Emision.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                Provision_Automatica.tpr_fecha_maxpago = _Fecha_Pago.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            #endregion
            Provision_Automatica.tpr_valor = 0;
            Provision_Automatica.tpr_afecto = 0;
            Provision_Automatica.tpr_noafecto = 0;
            Provision_Automatica.tpr_iva = 0;
            Provision_Automatica.tpr_observacion = Bean_Transaccion.strC24;

            RE_GenericBean Datos_Serie_Bean = (RE_GenericBean)DB.Get_Serie_CorrelativoBy_Traficos(Provision_Automatica.tpr_suc_id, Provision_Automatica.tpr_tcon_id, Provision_Automatica.tpr_mon_id);
            if (Datos_Serie_Bean == null)
            {
                PaisBean Pais_Temp = null;
                Pais_Temp = (PaisBean)DB.getPais(idpaisDESTINO);
                int Resultado = DB.Crear_Series_Provisiones_Automaticas(Provision_Automatica.tpr_suc_id, Pais_Temp, 8, Provision_Automatica.tpr_tcon_id, Provision_Automatica.tpr_mon_id);
                Datos_Serie_Bean = (RE_GenericBean)DB.Get_Serie_CorrelativoBy_Traficos(Provision_Automatica.tpr_suc_id, Provision_Automatica.tpr_tcon_id, Provision_Automatica.tpr_mon_id);
            }

            Provision_Automatica.tpr_pai_id = Empresa_Bean.ID;
            Provision_Automatica.tpr_usu_creacion = Bean_Transaccion.strC46;
            Provision_Automatica.tpr_fecha_creacion = DateTime.Now.ToString("yyyy-MM-dd");
            Provision_Automatica.tpr_usu_acepta = Bean_Transaccion.strC46;
            Provision_Automatica.tpr_fecha_acepta = DateTime.Now.ToString("yyyy-MM-dd");
            Provision_Automatica.tpr_departamento = 0;
            Provision_Automatica.tpr_ted_id = 5;
            Provision_Automatica.tpr_serie = Datos_Serie_Bean.strC1;
            Provision_Automatica.tpr_serie_oc = "";
            Provision_Automatica.tpr_correlativo_oc = 0;
            Provision_Automatica.tpr_tts_id = 0;
            Provision_Automatica.tpr_hbl = Datos_BL.Hbl;
            Provision_Automatica.tpr_mbl = Datos_BL.Mbl;
            Provision_Automatica.tpr_routing = Datos_BL.Routing;
            Provision_Automatica.tpr_contenedor = Datos_BL.Contenedor;
            Provision_Automatica.tpr_correlativo = 0;
            Provision_Automatica.tpr_serie_contrasena = "";
            Provision_Automatica.tpr_contrasena_correlativo = 0;
            Provision_Automatica.tpr_valor_equivalente = 0;
            Provision_Automatica.tpr_fact_corr = Bean_Transaccion.strC61;//trt_proveedor_correlativo
            Provision_Automatica.tpr_proveedor_cajachica = "";
            Provision_Automatica.tpr_imp_exp_id = Datos_BL.Import_Export;
            Provision_Automatica.tpr_imp_exp_id = 1;//Toda la Carga enviada a Collectar se vuelve Importacion
            Provision_Automatica.tpr_bien_serv = 2;
            Provision_Automatica.tpr_proveedor_cajachica_id = 0;
            Provision_Automatica.tpr_poliza = "";
            Provision_Automatica.tpr_fecha_libro_compras = DateTime.Now.ToString("yyyy-MM-dd");
            Provision_Automatica.tpr_tto_id = Datos_BL.ttoID;
            Provision_Automatica.tpr_ruta_pais = "";
            Provision_Automatica.tpr_ruta = "";
            Provision_Automatica.tpr_blid = Datos_BL.BLID;
            Provision_Automatica.tpr_usu_modifica_regimen = "";
            Provision_Automatica.tpr_usu_anula = "";
            Provision_Automatica.tpr_fecha_anula = null;
            Provision_Automatica.tpr_toc_id = 0;
            Provision_Automatica.tpr_observacion_contrasena = "";
            Provision_Automatica.tpr_fecha_recibo_factura = null;
            Provision_Automatica.tpr_mbl_modificado = false;
            Provision_Automatica.tpr_ttd_id = 3;
            Provision_Automatica.tpr_tds_id = 0;
            #region Generar Detalle de Rubros
            Bean_Detalle_Rubros Detalle_Rubros = null;
            foreach (RE_GenericBean Bean_Cargo in Arr_Detalle_Transacciones)
            {
                Detalle_Rubros = new Bean_Detalle_Rubros();
                Detalle_Rubros.tdf_cargo_id = int.Parse(Bean_Cargo.strC51);
                Detalle_Rubros.tdf_tts_id = int.Parse(Bean_Cargo.strC14);
                Detalle_Rubros.tdf_rub_id = int.Parse(Bean_Cargo.strC16);
                Detalle_Rubros.tdf_montosinimpuesto = Convert.ToDouble(Bean_Cargo.strC22);
                Detalle_Rubros.tdf_montosinimpuesto = Contabilizacion_Automatica_CN.Convertir_Divisas_Intercompanys(idpaisORIGEN, idmonedaORIGEN, Detalle_Rubros.tdf_montosinimpuesto, idpaisDESTINO, idmonedaDESTINO);
                Detalle_Rubros.tdf_impuesto = 0;
                Detalle_Rubros.tdf_monto = Convert.ToDouble(Bean_Cargo.strC22);
                Detalle_Rubros.tdf_monto = Contabilizacion_Automatica_CN.Convertir_Divisas_Intercompanys(idpaisORIGEN, idmonedaORIGEN, Detalle_Rubros.tdf_monto, idpaisDESTINO, idmonedaDESTINO);
                Detalle_Rubros.tdf_total_equivalente = Detalle_Rubros.tdf_monto;
                #region Calcular Total Equivalente
                if (Provision_Automatica.tpr_mon_id == 8)
                {
                    decimal Tipo_Cambio_Temporal = DB.getTipoCambioHoy(Provision_Automatica.tpr_pai_id);
                    Detalle_Rubros.tdf_total_equivalente = Math.Round(Detalle_Rubros.tdf_monto * (double)Tipo_Cambio_Temporal, 2);
                }
                else
                {
                    decimal Tipo_Cambio_Temporal = DB.getTipoCambioHoy(Provision_Automatica.tpr_pai_id);
                    Detalle_Rubros.tdf_total_equivalente = Math.Round(Detalle_Rubros.tdf_monto / (double)Tipo_Cambio_Temporal, 2);
                }
                #endregion
                Detalle_Rubros.tdf_ttm_id = int.Parse(Bean_Cargo.strC20);
                Detalle_Rubros.tdf_tfa_id = 0;
                Detalle_Rubros.tdf_ttr_id = 5;
                Detalle_Rubros.tdf_comentarios = "";
                if (Detalle_Rubros.tdf_impuesto == 0)
                {
                    Provision_Automatica.tpr_noafecto += Detalle_Rubros.tdf_montosinimpuesto;
                }
                else
                {
                    Provision_Automatica.tpr_afecto += Detalle_Rubros.tdf_montosinimpuesto;
                }
                Provision_Automatica.tpr_valor += Detalle_Rubros.tdf_monto;
                Provision_Automatica.tpr_iva = Detalle_Rubros.tdf_impuesto;
                Provision_Automatica.tpr_valor_equivalente += Detalle_Rubros.tdf_total_equivalente;
                int transID = 105;
                int tttID = 15;
                Detalle_Rubros.cta_debe = (ArrayList)DB.getCtaContablebyRubro("debe", (int)Detalle_Rubros.tdf_rub_id, Provision_Automatica.tpr_pai_id, tttID, Provision_Automatica.tpr_tti_id, Provision_Automatica.tpr_mon_id, Provision_Automatica.tpr_imp_exp_id, tipo_cobro, Provision_Automatica.tpr_tcon_id, Detalle_Rubros.tdf_tts_id);
                if (Detalle_Rubros.cta_debe == null)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    string nombre_servicio = "";
                    string nombre_rubro = "";
                    RE_GenericBean Bean_Rubro_Aux = null;
                    nombre_servicio = Utility.TraducirServiciotoSTR(Detalle_Rubros.tdf_tts_id);
                    Bean_Rubro_Aux = DB.getRubro(Detalle_Rubros.tdf_rub_id);
                    nombre_rubro = Bean_Rubro_Aux.strC1;
                    Arr_Result.Add("No existe combinacion de cuentas contables para el Servicio " + nombre_servicio + " y Rubro " + nombre_rubro + " con ID (" + Detalle_Rubros.tdf_rub_id + "), en la Empresa " + Empresa_Bean.Nombre_Sistema + " para emitir Provision.");
                    return Arr_Result;
                }
                if (Detalle_Rubros.cta_debe.Count == 0)
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    string nombre_servicio = "";
                    string nombre_rubro = "";
                    RE_GenericBean Bean_Rubro_Aux = null;
                    nombre_servicio = Utility.TraducirServiciotoSTR(Detalle_Rubros.tdf_tts_id);
                    Bean_Rubro_Aux = DB.getRubro(Detalle_Rubros.tdf_rub_id);
                    nombre_rubro = Bean_Rubro_Aux.strC1;
                    Arr_Result.Add("No existe combinacion de cuentas contables para el Servicio " + nombre_servicio + " y Rubro " + nombre_rubro + " con ID (" + Detalle_Rubros.tdf_rub_id + "), en la Empresa " + Empresa_Bean.Nombre_Sistema + " para emitir Provision.");
                    return Arr_Result;
                }
                if (Provision_Automatica.Arr_Detalle_Provision == null) Provision_Automatica.Arr_Detalle_Provision = new ArrayList();
                Provision_Automatica.Arr_Detalle_Provision.Add(Detalle_Rubros);

                Provision_Automatica.tpr_ttt_id = transID;
                int _matOpID_Destino = DB.getMatrizOperacionID(transID, Provision_Automatica.tpr_mon_id, Provision_Automatica.tpr_pai_id, Provision_Automatica.tpr_tcon_id);

                ArrayList ctas_cargo_Destino = (ArrayList)DB.getMatrizConfiguracion_ingreso_egreso(_matOpID_Destino, "Abono");
                if ((ctas_cargo_Destino == null) || (ctas_cargo_Destino.Count == 0))
                {
                    Arr_Result = new ArrayList();
                    Arr_Result.Add("0");
                    Arr_Result.Add("No Existe configuracion contable para la Provision, por favor pongase en contacto con el Contador");
                    return Arr_Result;
                }
                Provision_Automatica.ctas_cargo = ctas_cargo_Destino;

                Arr_Result = new ArrayList();
                Arr_Result.Add("1");
                Arr_Result.Add(Provision_Automatica);
            }
            #endregion
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            Arr_Result = new ArrayList();
            Arr_Result.Add("0");
            Arr_Result.Add("Existio un error al tratar de Construir la Provision");
            Arr_Result.Add(null);
            return Arr_Result;
        }
        return Arr_Result;
        #endregion
    }
    public static ArrayList Marcar_Transaccion_SCA_Con_REFID(int SesionID, ArrayList Arr_Documento, ArrayList Arr_Detalle_Transacciones, int ttrID)
    {
        ArrayList Arr_Result = new ArrayList();
        int result = 0;
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
                Bean_Factura_Automatica Factura_Automatica = (Bean_Factura_Automatica)Arr_Documento[2];
                foreach (RE_GenericBean Bean_Transaccion in Arr_Detalle_Transacciones)
                {
                    int TRS_ID = 0;
                    int TRB_ID = 0;
                    int TRC_ID = 0;
                    int TRT_ID = 0;
                    TRS_ID = int.Parse(Bean_Transaccion.strC1);
                    TRB_ID = int.Parse(Bean_Transaccion.strC2);
                    TRC_ID = int.Parse(Bean_Transaccion.strC3);
                    TRT_ID = int.Parse(Bean_Transaccion.strC4);
                    comm.CommandText = "update tbl_reconciliacion_carga_transacciones set trt_ttr_final_id=" + ttrID + ", trt_ref_id=" + Factura_Automatica.tfa_id + ", trt_serie='" + Factura_Automatica.tfa_serie + "', trt_correlativo='" + Factura_Automatica.tfa_correlativo + "' where trt_id=" + TRT_ID + " and trt_estado=1 ";
                    result = comm.ExecuteNonQuery();
                }
            }
            else if (ttrID == 4)
            {
                Bean_Nota_Debito_Automatica Nota_Debito_Automatica = (Bean_Nota_Debito_Automatica)Arr_Documento[2];
                foreach (RE_GenericBean Bean_Transaccion in Arr_Detalle_Transacciones)
                {
                    int TRS_ID = 0;
                    int TRB_ID = 0;
                    int TRC_ID = 0;
                    int TRT_ID = 0;
                    TRS_ID = int.Parse(Bean_Transaccion.strC1);
                    TRB_ID = int.Parse(Bean_Transaccion.strC2);
                    TRC_ID = int.Parse(Bean_Transaccion.strC3);
                    TRT_ID = int.Parse(Bean_Transaccion.strC4);
                    comm.CommandText = "update tbl_reconciliacion_carga_transacciones set trt_ttr_final_id=" + ttrID + ", trt_ref_id=" + Nota_Debito_Automatica.tnd_id + ", trt_serie='" + Nota_Debito_Automatica.tnd_serie + "', trt_correlativo='" + Nota_Debito_Automatica.tnd_correlativo + "' where trt_id=" + TRT_ID + " and trt_estado=1 ";
                    result = comm.ExecuteNonQuery();
                }
            }
            else if (ttrID == 5)
            {
                Bean_Provision_Automatica Provision_Automatica = (Bean_Provision_Automatica)Arr_Documento[2];
                foreach (RE_GenericBean Bean_Transaccion in Arr_Detalle_Transacciones)
                {
                    int TRS_ID = 0;
                    int TRB_ID = 0;
                    int TRC_ID = 0;
                    int TRT_ID = 0;
                    TRS_ID = int.Parse(Bean_Transaccion.strC1);
                    TRB_ID = int.Parse(Bean_Transaccion.strC2);
                    TRC_ID = int.Parse(Bean_Transaccion.strC3);
                    TRT_ID = int.Parse(Bean_Transaccion.strC4);
                    comm.CommandText = "update tbl_reconciliacion_carga_transacciones set trt_ttr_final_id=" + ttrID + ", trt_ref_id=" + Provision_Automatica.tpr_prov_id + ", trt_serie='" + Provision_Automatica.tpr_serie + "', trt_correlativo='" + Provision_Automatica.tpr_correlativo + "' where trt_id=" + TRT_ID + " and trt_estado=1 ";
                    result = comm.ExecuteNonQuery();
                }
            }
            comm.Parameters.Clear();
            DB.CloseObj_insert(comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            Arr_Result = new ArrayList();
            Arr_Result.Add("0");
            Arr_Result.Add("Existio un error al tratar de Marcar la Transaccion con Referencia");
            Arr_Result.Add(Arr_Documento);
            return Arr_Result;
        }
        Arr_Result = new ArrayList();
        Arr_Result.Add("1");
        Arr_Result.Add("Transaccion Marcada Exitosamente");
        Arr_Result.Add(Arr_Documento);
        return Arr_Result;
    }
    public static ArrayList Obtener_Detalle_Transacciones_Contabilizadas_Sesion_SCA(int sesionID)
    {
        ArrayList Arr_Result = new ArrayList();
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
            comm.CommandText = "select distinct on(trt_ref_id, trt_ttr_final_id) trs_id, trb_id, trc_id, trt_id, " +
            "trt_tipo_bl, trt_tipo_carga, trt_destino_final, " +
            "trt_ttr_id, trt_tipo_documento, trt_tpi_id, trt_tipo_persona, trt_persona_id, trt_nombre, " +
            "trt_tts_id, trt_tipo_servicio, trt_rub_id, trt_rubro, trt_conta_id, trt_contabilidad, trt_ttm_id, trt_moneda, trt_monto, trt_concepto, trt_observaciones, " +
            "trt_agente_cobrar, trt_agente_pagar, trt_naviera_cobrar, trt_naviera_pagar, trt_cliente_cobrar, trt_cliente_pagar, " +
            "trt_intermodal_cobrar, trt_intermodal_pagar, trt_intercompany_destino_cobrar, trt_intercompany_destino_pagar, trt_ingresos, trt_costos, " +
            "trt_prepaid_collect_id, trt_prepaid_collect, trt_local_internacional_id, trt_local_internacional, trt_tiene_conocimiento_embarque, trt_all_in, " +
            "trt_grupo_id, trt_contabilizar, trt_fecha_creacion, trt_usu_id, trt_trc_id, trt_estado, trb_bl,  " +
            "trt_empresa_id, trt_cargo_id, trt_tiene_referencia_adicional, trt_id_routing_adicional, trt_routing_adicional, trt_tto_id_adicional, trb_bl_id, " +
            "trt_serie, trt_correlativo, trt_ttr_final_id, trt_ref_id " +
            "from tbl_reconciliacion_carga_sesiones, tbl_reconciliacion_carga_bls, tbl_reconciliacion_carga_cuestionario, tbl_reconciliacion_carga_transacciones " +
            "where trs_id=trb_trs_id and trs_estado=1 and trb_estado=1 " +
            "and trb_id=trc_trb_id and trc_estado=1 " +
            "and trc_id=trt_trc_id and trt_estado=1 " +
            "and trs_id=" + sesionID + " order by trt_ref_id, trt_ttr_final_id, trt_id  asc  ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.strC1 = reader.GetValue(0).ToString();//trs_id
                Bean.strC2 = reader.GetValue(1).ToString();//trb_id
                Bean.strC3 = reader.GetValue(2).ToString();//trc_id
                Bean.strC4 = reader.GetValue(3).ToString();//trt_id
                Bean.strC5 = reader.GetValue(4).ToString();//trt_tipo_bl
                Bean.strC6 = reader.GetValue(5).ToString();//trt_tipo_carga
                Bean.strC7 = reader.GetValue(6).ToString();//trt_destino_final
                Bean.strC8 = reader.GetValue(7).ToString();//trt_ttr_id
                Bean.strC9 = reader.GetValue(8).ToString();//trt_tipo_documento
                Bean.strC10 = reader.GetValue(9).ToString();//trt_tpi_id
                Bean.strC11 = reader.GetValue(10).ToString();//trt_tipo_persona
                Bean.strC12 = reader.GetValue(11).ToString();//trt_persona_id
                Bean.strC13 = reader.GetValue(12).ToString();//trt_nombre
                Bean.strC14 = reader.GetValue(13).ToString();//trt_tts_id
                Bean.strC15 = reader.GetValue(14).ToString();//trt_tipo_servicio
                Bean.strC16 = reader.GetValue(15).ToString();//trt_rub_id
                Bean.strC17 = reader.GetValue(16).ToString();//trt_rubro
                Bean.strC18 = reader.GetValue(17).ToString();//trt_conta_id
                Bean.strC19 = reader.GetValue(18).ToString();//trt_contabilidad
                Bean.strC20 = reader.GetValue(19).ToString();//trt_ttm_id
                Bean.strC21 = reader.GetValue(20).ToString();//trt_moneda
                Bean.strC22 = reader.GetValue(21).ToString();//trt_monto
                Bean.strC23 = reader.GetValue(22).ToString();//trt_concepto
                Bean.strC24 = reader.GetValue(23).ToString();//trt_observaciones
                Bean.strC25 = reader.GetValue(24).ToString();//trt_agente_cobrar
                Bean.strC26 = reader.GetValue(25).ToString();//trt_agente_pagar
                Bean.strC27 = reader.GetValue(26).ToString();//trt_naviera_cobrar
                Bean.strC28 = reader.GetValue(27).ToString();//trt_naviera_pagar
                Bean.strC29 = reader.GetValue(28).ToString();//trt_cliente_cobrar
                Bean.strC30 = reader.GetValue(29).ToString();//trt_cliente_pagar
                Bean.strC31 = reader.GetValue(30).ToString();//trt_intermodal_cobrar
                Bean.strC32 = reader.GetValue(31).ToString();//trt_intermodal_pagar
                Bean.strC33 = reader.GetValue(32).ToString();//trt_intercompany_destino_cobrar
                Bean.strC34 = reader.GetValue(33).ToString();//trt_intercompany_destino_pagar
                Bean.strC35 = reader.GetValue(34).ToString();//trt_ingresos
                Bean.strC36 = reader.GetValue(35).ToString();//trt_costos
                Bean.strC37 = reader.GetValue(36).ToString();//trt_prepaid_collect_id
                Bean.strC38 = reader.GetValue(37).ToString();//trt_prepaid_collect
                Bean.strC39 = reader.GetValue(38).ToString();//trt_local_internacional_id
                Bean.strC40 = reader.GetValue(39).ToString();//trt_local_internacional
                Bean.strC41 = reader.GetValue(40).ToString();//trt_tiene_conocimiento_embarque
                Bean.strC42 = reader.GetValue(41).ToString();//trt_all_in
                Bean.strC43 = reader.GetValue(42).ToString();//trt_grupo_id
                Bean.strC44 = reader.GetValue(43).ToString();//trt_contabilizar
                Bean.strC45 = reader.GetValue(44).ToString();//trt_fecha_creacion
                Bean.strC46 = reader.GetValue(45).ToString();//trt_usu_id
                Bean.strC47 = reader.GetValue(46).ToString();//trt_trc_id
                Bean.strC48 = reader.GetValue(47).ToString();//trt_estado
                Bean.strC49 = reader.GetValue(48).ToString();//trb_bl
                Bean.strC50 = reader.GetValue(49).ToString();//trt_empresa_id
                Bean.strC51 = reader.GetValue(50).ToString();//trt_cargo_id
                Bean.strC52 = reader.GetValue(51).ToString();//trt_tiene_referencia_adicional
                Bean.strC53 = reader.GetValue(52).ToString();//trt_id_routing_adicional
                Bean.strC54 = reader.GetValue(53).ToString();//trt_routing_adicional
                Bean.strC55 = reader.GetValue(54).ToString();//trt_tto_id_adicional
                Bean.strC56 = "FALSE";//CONTABILIZADO - Esto Sirve para hacer los Grupos
                Bean.strC57 = reader.GetValue(55).ToString();//trb_bl_id
                Bean.strC58 = reader.GetValue(56).ToString();//trt_serie
                Bean.strC59 = reader.GetValue(57).ToString();//trt_correlativo
                Bean.strC60 = reader.GetValue(58).ToString();//trt_ttr_final_id
                Bean.strC61 = reader.GetValue(59).ToString();//trt_ref_id
                #region Obtener Monto Total de cada una de las Transacciones
                int ttrID = 0;
                int refID = 0;
                ttrID = int.Parse(Bean.strC60);
                refID = int.Parse(Bean.strC61);
                if (ttrID == 1)
                {
                    #region Factura o Invoice
                    RE_GenericBean Factura_Bean = (RE_GenericBean)DB.getFacturaData(refID);
                    Bean.strC22 = Factura_Bean.decC3.ToString();
                    #endregion
                }
                else if (ttrID == 4)
                {
                    #region Nota de Debito o Recordatorio de Pago a Cliente y Agentes
                    RE_GenericBean Nota_Debito_Bean = (RE_GenericBean)DB.getNotaDebitoData(refID);
                    Bean.strC22 = Nota_Debito_Bean.decC1.ToString();
                    #endregion
                }
                else if (ttrID == 5)
                {
                    #region Provisiones
                    RE_GenericBean Provision_Bean = (RE_GenericBean)Utility.getDetalleProvision(refID);
                    Bean.strC22 = Provision_Bean.decC1.ToString();
                    if (Provision_Bean.intC11 == 10)
                    {
                        Bean.strC20 = Provision_Bean.intC5.ToString();//trt_ttm_id
                        Bean.strC21 = Utility.TraducirMonedaInt(Provision_Bean.intC5);//trt_moneda
                    }
                    #endregion
                }
                #endregion
                Arr_Result.Add(Bean);
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Arr_Result;
    }
    public static ArrayList Obtener_Afectacion_Contable_Transacciones_Sesion_SCA(int sesionID)
    {
        ArrayList Arr_Result = new ArrayList();
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
            comm.CommandText = "select trs_id, trb_id, trc_id, trt_id, trt_tipo_bl, trt_tipo_carga, trt_destino_final, trt_ttr_id, trt_tipo_documento, " +
            "trt_tpi_id, trt_tipo_persona, trt_persona_id, trt_nombre, trt_tts_id, trt_tipo_servicio, trt_rub_id, trt_rubro, trt_conta_id, trt_contabilidad, " +
            "trt_ttm_id, trt_moneda, trt_monto, trt_concepto, trt_observaciones, trt_agente_cobrar, trt_agente_pagar, trt_naviera_cobrar, trt_naviera_pagar, " +
            "trt_cliente_cobrar, trt_cliente_pagar, trt_intermodal_cobrar, trt_intermodal_pagar, trt_intercompany_destino_cobrar, trt_intercompany_destino_pagar, " +
            "trt_ingresos, trt_costos, trt_prepaid_collect_id, trt_prepaid_collect, trt_local_internacional_id, trt_local_internacional, trt_tiene_conocimiento_embarque, " +
            "trt_all_in, trt_grupo_id, trt_contabilizar, trt_fecha_creacion, trt_usu_id, trt_trc_id, trt_estado, trb_bl,  trt_empresa_id, trt_cargo_id, trt_tiene_referencia_adicional, " +
            "trt_id_routing_adicional, trt_routing_adicional, trt_tto_id_adicional, trb_bl_id, trt_serie, trt_correlativo, cue_id, cue_nombre, tdi_debe, tdi_haber, cue_clasificacion " +
            "from tbl_reconciliacion_carga_sesiones, tbl_reconciliacion_carga_bls, tbl_reconciliacion_carga_cuestionario, tbl_reconciliacion_carga_transacciones, " +
            "tbl_libro_diario, tbl_cuenta " +
            "where trs_id=trb_trs_id and trs_estado=1 and trb_estado=1 and trb_id=trc_trb_id and trc_estado=1 and trc_id=trt_trc_id " +
            "and trt_ttr_final_id=tdi_ttr_id and trt_ref_id=tdi_ref_id and tdi_cue_id=cue_id " +
            "and trt_estado=1 and trs_id=" + sesionID + " " +
            "order by trt_id, trt_ttr_final_id, trt_ref_id asc ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.strC1 = reader.GetValue(0).ToString();//trs_id
                Bean.strC2 = reader.GetValue(1).ToString();//trb_id
                Bean.strC3 = reader.GetValue(2).ToString();//trc_id
                Bean.strC4 = reader.GetValue(3).ToString();//trt_id
                Bean.strC5 = reader.GetValue(4).ToString();//trt_tipo_bl
                Bean.strC6 = reader.GetValue(5).ToString();//trt_tipo_carga
                Bean.strC7 = reader.GetValue(6).ToString();//trt_destino_final
                Bean.strC8 = reader.GetValue(7).ToString();//trt_ttr_id
                Bean.strC9 = reader.GetValue(8).ToString();//trt_tipo_documento
                Bean.strC10 = reader.GetValue(9).ToString();//trt_tpi_id
                Bean.strC11 = reader.GetValue(10).ToString();//trt_tipo_persona
                Bean.strC12 = reader.GetValue(11).ToString();//trt_persona_id
                Bean.strC13 = reader.GetValue(12).ToString();//trt_nombre
                Bean.strC14 = reader.GetValue(13).ToString();//trt_tts_id
                Bean.strC15 = reader.GetValue(14).ToString();//trt_tipo_servicio
                Bean.strC16 = reader.GetValue(15).ToString();//trt_rub_id
                Bean.strC17 = reader.GetValue(16).ToString();//trt_rubro
                Bean.strC18 = reader.GetValue(17).ToString();//trt_conta_id
                Bean.strC19 = reader.GetValue(18).ToString();//trt_contabilidad
                Bean.strC20 = reader.GetValue(19).ToString();//trt_ttm_id
                Bean.strC21 = reader.GetValue(20).ToString();//trt_moneda
                Bean.strC22 = reader.GetValue(21).ToString();//trt_monto
                Bean.strC23 = reader.GetValue(22).ToString();//trt_concepto
                Bean.strC24 = reader.GetValue(23).ToString();//trt_observaciones
                Bean.strC25 = reader.GetValue(24).ToString();//trt_agente_cobrar
                Bean.strC26 = reader.GetValue(25).ToString();//trt_agente_pagar
                Bean.strC27 = reader.GetValue(26).ToString();//trt_naviera_cobrar
                Bean.strC28 = reader.GetValue(27).ToString();//trt_naviera_pagar
                Bean.strC29 = reader.GetValue(28).ToString();//trt_cliente_cobrar
                Bean.strC30 = reader.GetValue(29).ToString();//trt_cliente_pagar
                Bean.strC31 = reader.GetValue(30).ToString();//trt_intermodal_cobrar
                Bean.strC32 = reader.GetValue(31).ToString();//trt_intermodal_pagar
                Bean.strC33 = reader.GetValue(32).ToString();//trt_intercompany_destino_cobrar
                Bean.strC34 = reader.GetValue(33).ToString();//trt_intercompany_destino_pagar
                Bean.strC35 = reader.GetValue(34).ToString();//trt_ingresos
                Bean.strC36 = reader.GetValue(35).ToString();//trt_costos
                Bean.strC37 = reader.GetValue(36).ToString();//trt_prepaid_collect_id
                Bean.strC38 = reader.GetValue(37).ToString();//trt_prepaid_collect
                Bean.strC39 = reader.GetValue(38).ToString();//trt_local_internacional_id
                Bean.strC40 = reader.GetValue(39).ToString();//trt_local_internacional
                Bean.strC41 = reader.GetValue(40).ToString();//trt_tiene_conocimiento_embarque
                Bean.strC42 = reader.GetValue(41).ToString();//trt_all_in
                Bean.strC43 = reader.GetValue(42).ToString();//trt_grupo_id
                Bean.strC44 = reader.GetValue(43).ToString();//trt_contabilizar
                Bean.strC45 = reader.GetValue(44).ToString();//trt_fecha_creacion
                Bean.strC46 = reader.GetValue(45).ToString();//trt_usu_id
                Bean.strC47 = reader.GetValue(46).ToString();//trt_trc_id
                Bean.strC48 = reader.GetValue(47).ToString();//trt_estado
                Bean.strC49 = reader.GetValue(48).ToString();//trb_bl
                Bean.strC50 = reader.GetValue(49).ToString();//trt_empresa_id
                Bean.strC51 = reader.GetValue(50).ToString();//trt_cargo_id
                Bean.strC52 = reader.GetValue(51).ToString();//trt_tiene_referencia_adicional
                Bean.strC53 = reader.GetValue(52).ToString();//trt_id_routing_adicional
                Bean.strC54 = reader.GetValue(53).ToString();//trt_routing_adicional
                Bean.strC55 = reader.GetValue(54).ToString();//trt_tto_id_adicional
                Bean.strC56 = "FALSE";//CONTABILIZADO - Esto Sirve para hacer los Grupos
                Bean.strC57 = reader.GetValue(55).ToString();//trb_bl_id
                Bean.strC58 = reader.GetValue(56).ToString();//trt_serie
                Bean.strC59 = reader.GetValue(57).ToString();//trt_correlativo

                Bean.strC60 = reader.GetValue(58).ToString();//cue_id
                Bean.strC61 = reader.GetValue(59).ToString();//cue_nombre
                Bean.strC62 = reader.GetValue(60).ToString();//tdi_debe
                Bean.strC63 = reader.GetValue(61).ToString();//tdi_haber
                Bean.strC64 = reader.GetValue(62).ToString();//cue_clasificacion
                #region Definir Tipo de Cuenta
                if (Bean.strC64 == "1")
                {
                    Bean.strC64 = "ACTIVO";
                }
                else if (Bean.strC64 == "2")
                {
                    Bean.strC64 = "PASIVO";
                }
                else if (Bean.strC64 == "3")
                {
                    Bean.strC64 = "INGRESOS";
                }
                else if (Bean.strC64 == "4")
                {
                    Bean.strC64 = "EGRESOS";
                }
                else if (Bean.strC64 == "5")
                {
                    Bean.strC64 = "CAPITAL";
                }
                #endregion
                Arr_Result.Add(Bean);
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Arr_Result;
    }
    public static ArrayList Obtener_Resumen_Contable_Transacciones_Sesion_SCA(int sesionID)
    {
        ArrayList Arr_Result = new ArrayList();
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
            #region Backup
            //comm.CommandText = "select cue_id, cue_nombre, cue_clasificacion, trt_moneda, sum (tdi_debe), sum(tdi_haber) " +
            //"from " +
            //"( " +
            //"select trs_id, trb_id, trc_id, trt_id, trt_tipo_bl, trt_tipo_carga, trt_destino_final, trt_ttr_id, trt_tipo_documento, " +
            //"trt_tpi_id, trt_tipo_persona, trt_persona_id, trt_nombre, trt_tts_id, trt_tipo_servicio, trt_rub_id, trt_rubro, trt_conta_id, " +
            //"trt_contabilidad, trt_ttm_id, trt_moneda, trt_monto, trt_concepto, trt_observaciones, trt_agente_cobrar, trt_agente_pagar, " +
            //"trt_naviera_cobrar, trt_naviera_pagar, trt_cliente_cobrar, trt_cliente_pagar, trt_intermodal_cobrar, trt_intermodal_pagar, " +
            //"trt_intercompany_destino_cobrar, trt_intercompany_destino_pagar, trt_ingresos, trt_costos, trt_prepaid_collect_id, " +
            //"trt_prepaid_collect, trt_local_internacional_id, trt_local_internacional, trt_tiene_conocimiento_embarque, trt_all_in, " +
            //"trt_grupo_id, trt_contabilizar, trt_fecha_creacion, trt_usu_id, trt_trc_id, trt_estado, trb_bl,  trt_empresa_id, trt_cargo_id, " +
            //"trt_tiene_referencia_adicional, trt_id_routing_adicional, trt_routing_adicional, trt_tto_id_adicional, trb_bl_id, trt_serie, " +
            //"trt_correlativo, cue_id, cue_nombre, tdi_debe, tdi_haber, cue_clasificacion " +
            //"from tbl_reconciliacion_carga_sesiones, tbl_reconciliacion_carga_bls, tbl_reconciliacion_carga_cuestionario, " +
            //"tbl_reconciliacion_carga_transacciones, tbl_libro_diario, tbl_cuenta " +
            //"where trs_id=trb_trs_id and trs_estado=1 and trb_estado=1 and trb_id=trc_trb_id and trc_estado=1 " +
            //"and trc_id=trt_trc_id and trt_ttr_final_id=tdi_ttr_id and trt_ref_id=tdi_ref_id and tdi_cue_id=cue_id " +
            //"and trt_estado=1 and trs_id=" + sesionID + " " +
            //") " +
            //"as X " +
            //"group by cue_id, cue_nombre, cue_clasificacion, trt_moneda " +
            //"order by cue_id asc ";
            #endregion
            comm.CommandText = "select cue_id, cue_nombre, cue_clasificacion, moneda, sum(tdi_debe), sum(tdi_haber) from " +
            "( " +
            "    select cue_id, cue_nombre, cue_clasificacion, substr(ttm_nombre, 1,3) as moneda, tdi_debe, tdi_haber " +
            "    from tbl_libro_diario, tbl_cuenta, tbl_tipo_moneda " +
            "    where tdi_cue_id=cue_id " +
            "    and ttm_id=tdi_moneda_id " +
            "    and tdi_ttr_id=1 " +
            "    and tdi_ref_id in (select distinct trt_ref_id " +
            "    from tbl_reconciliacion_carga_sesiones, tbl_reconciliacion_carga_bls, tbl_reconciliacion_carga_cuestionario, tbl_reconciliacion_carga_transacciones " +
            "    where trs_id=trb_trs_id and trs_estado=1 and trb_estado=1 and trb_id=trc_trb_id and trc_estado=1 and trc_id=trt_trc_id and trt_estado=1 and trs_id=" + sesionID + " and trt_ttr_final_id=1) " +
            "    union all " +
            "    select cue_id, cue_nombre, cue_clasificacion, substr(ttm_nombre, 1,3) as moneda, tdi_debe, tdi_haber " +
            "    from tbl_libro_diario, tbl_cuenta, tbl_tipo_moneda " +
            "    where tdi_cue_id=cue_id " +
            "    and ttm_id=tdi_moneda_id " +
            "    and tdi_ttr_id=4 " +
            "    and tdi_ref_id in (select distinct trt_ref_id " +
            "    from tbl_reconciliacion_carga_sesiones, tbl_reconciliacion_carga_bls, tbl_reconciliacion_carga_cuestionario, tbl_reconciliacion_carga_transacciones " +
            "    where trs_id=trb_trs_id and trs_estado=1 and trb_estado=1 and trb_id=trc_trb_id and trc_estado=1 and trc_id=trt_trc_id and trt_estado=1 and trs_id=" + sesionID + " and trt_ttr_final_id=4) " +
            "    union all " +
            "    select cue_id, cue_nombre, cue_clasificacion, substr(ttm_nombre, 1,3) as moneda, tdi_debe, tdi_haber " +
            "    from tbl_libro_diario, tbl_cuenta, tbl_tipo_moneda " +
            "    where tdi_cue_id=cue_id " +
            "    and ttm_id=tdi_moneda_id " +
            "    and tdi_ttr_id=5 " +
            "    and tdi_ref_id in (select distinct trt_ref_id " +
            "    from tbl_reconciliacion_carga_sesiones, tbl_reconciliacion_carga_bls, tbl_reconciliacion_carga_cuestionario, tbl_reconciliacion_carga_transacciones " +
            "    where trs_id=trb_trs_id and trs_estado=1 and trb_estado=1 and trb_id=trc_trb_id and trc_estado=1 and trc_id=trt_trc_id and trt_estado=1 and trs_id=" + sesionID + " and trt_ttr_final_id=5) " +
            ") as Resumen_Contable " +
            "group by cue_id, cue_nombre, cue_clasificacion, moneda order by cue_id asc ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.strC60 = reader.GetValue(0).ToString();//cue_id
                Bean.strC61 = reader.GetValue(1).ToString();//cue_nombre
                Bean.strC64 = reader.GetValue(2).ToString();//cue_clasificacion
                Bean.strC21 = reader.GetValue(3).ToString();//trt_moneda
                Bean.strC62 = reader.GetValue(4).ToString();//Debe
                Bean.strC63 = reader.GetValue(5).ToString();//Haber

                Bean.strC22 = "0.00";
                Bean.strC25 = "0.00";
                Bean.strC26 = "0.00";
                Bean.strC27 = "0.00";
                Bean.strC28 = "0.00";
                Bean.strC29 = "0.00";
                Bean.strC30 = "0.00";
                Bean.strC31 = "0.00";
                Bean.strC32 = "0.00";
                Bean.strC33 = "0.00";
                Bean.strC34 = "0.00";
                Bean.strC35 = "0.00";
                Bean.strC36 = "0.00";


                #region Definir Tipo de Cuenta
                if (Bean.strC64 == "1")
                {
                    Bean.strC64 = "ACTIVO";
                }
                else if (Bean.strC64 == "2")
                {
                    Bean.strC64 = "PASIVO";
                }
                else if (Bean.strC64 == "3")
                {
                    Bean.strC64 = "INGRESOS";
                }
                else if (Bean.strC64 == "4")
                {
                    Bean.strC64 = "EGRESOS";
                }
                else if (Bean.strC64 == "5")
                {
                    Bean.strC64 = "CAPITAL";
                }
                #endregion
                Arr_Result.Add(Bean);
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Arr_Result;
    }
    public static ArrayList Obtener_SOA_By_Tipo_Persona_Sesion_Reconciliacion_Carga(int ID, int tpiID)
    {
        ArrayList Arr_Result = new ArrayList();
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
            comm.CommandText = "select  trs_id, trb_id, trc_id, trt_id, " +
            "trt_tipo_bl, trt_tipo_carga, trt_destino_final, " +
            "trt_ttr_id, trt_tipo_documento, trt_tpi_id, trt_tipo_persona, trt_persona_id, trt_nombre, " +
            "trt_tts_id, trt_tipo_servicio, trt_rub_id, trt_rubro, trt_conta_id, trt_contabilidad, trt_ttm_id, trt_moneda, trt_monto, trt_concepto, trt_observaciones, " +
            "trt_agente_cobrar, trt_agente_pagar, trt_naviera_cobrar, trt_naviera_pagar, trt_cliente_cobrar, trt_cliente_pagar, " +
            "trt_intermodal_cobrar, trt_intermodal_pagar, trt_intercompany_destino_cobrar, trt_intercompany_destino_pagar, trt_ingresos, trt_costos, " +
            "trt_prepaid_collect_id, trt_prepaid_collect, trt_local_internacional_id, trt_local_internacional, trt_tiene_conocimiento_embarque, trt_all_in, " +
            "trt_grupo_id, trt_contabilizar, trt_fecha_creacion, trt_usu_id, trt_trc_id, trt_estado, trb_bl,  " +
            "trt_empresa_id, trt_cargo_id, trt_tiene_referencia_adicional, trt_id_routing_adicional, trt_routing_adicional, trt_tto_id_adicional, " +
            "trt_serie, trt_correlativo " +
            "from tbl_reconciliacion_carga_sesiones, tbl_reconciliacion_carga_bls, tbl_reconciliacion_carga_cuestionario, tbl_reconciliacion_carga_transacciones " +
            "where trs_id=trb_trs_id and trs_estado=1 and trb_estado=1 " +
            "and trb_id=trc_trb_id and trc_estado=1 " +
            "and trc_id=trt_trc_id and trt_estado=1 " +
            "and trs_id=" + ID + " and trt_tpi_id=" + tpiID + " order by 4 asc ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.strC1 = reader.GetValue(0).ToString();//trs_id
                Bean.strC2 = reader.GetValue(1).ToString();//trb_id
                Bean.strC3 = reader.GetValue(2).ToString();//trc_id
                Bean.strC4 = reader.GetValue(3).ToString();//trt_id
                Bean.strC5 = reader.GetValue(4).ToString();//trt_tipo_bl
                Bean.strC6 = reader.GetValue(5).ToString();//trt_tipo_carga
                Bean.strC7 = reader.GetValue(6).ToString();//trt_destino_final
                Bean.strC8 = reader.GetValue(7).ToString();//trt_ttr_id
                Bean.strC9 = reader.GetValue(8).ToString();//trt_tipo_documento
                Bean.strC10 = reader.GetValue(9).ToString();//trt_tpi_id
                Bean.strC11 = reader.GetValue(10).ToString();//trt_tipo_persona
                Bean.strC12 = reader.GetValue(11).ToString();//trt_persona_id
                Bean.strC13 = reader.GetValue(12).ToString();//trt_nombre
                Bean.strC14 = reader.GetValue(13).ToString();//trt_tts_id
                Bean.strC15 = reader.GetValue(14).ToString();//trt_tipo_servicio
                Bean.strC16 = reader.GetValue(15).ToString();//trt_rub_id
                Bean.strC17 = reader.GetValue(16).ToString();//trt_rubro
                Bean.strC18 = reader.GetValue(17).ToString();//trt_conta_id
                Bean.strC19 = reader.GetValue(18).ToString();//trt_contabilidad
                Bean.strC20 = reader.GetValue(19).ToString();//trt_ttm_id
                Bean.strC21 = reader.GetValue(20).ToString();//trt_moneda
                Bean.strC22 = reader.GetValue(21).ToString();//trt_monto
                Bean.strC23 = reader.GetValue(22).ToString();//trt_concepto
                Bean.strC24 = reader.GetValue(23).ToString();//trt_observaciones
                Bean.strC25 = reader.GetValue(24).ToString();//trt_agente_cobrar
                Bean.strC26 = reader.GetValue(25).ToString();//trt_agente_pagar
                Bean.strC27 = reader.GetValue(26).ToString();//trt_naviera_cobrar
                Bean.strC28 = reader.GetValue(27).ToString();//trt_naviera_pagar
                Bean.strC29 = reader.GetValue(28).ToString();//trt_cliente_cobrar
                Bean.strC30 = reader.GetValue(29).ToString();//trt_cliente_pagar
                Bean.strC31 = reader.GetValue(30).ToString();//trt_intermodal_cobrar
                Bean.strC32 = reader.GetValue(31).ToString();//trt_intermodal_pagar
                Bean.strC33 = reader.GetValue(32).ToString();//trt_intercompany_destino_cobrar
                Bean.strC34 = reader.GetValue(33).ToString();//trt_intercompany_destino_pagar
                Bean.strC35 = reader.GetValue(34).ToString();//trt_ingresos
                Bean.strC36 = reader.GetValue(35).ToString();//trt_costos
                Bean.strC37 = reader.GetValue(36).ToString();//trt_prepaid_collect_id
                Bean.strC38 = reader.GetValue(37).ToString();//trt_prepaid_collect
                Bean.strC39 = reader.GetValue(38).ToString();//trt_local_internacional_id
                Bean.strC40 = reader.GetValue(39).ToString();//trt_local_internacional
                Bean.strC41 = reader.GetValue(40).ToString();//trt_tiene_conocimiento_embarque
                Bean.strC42 = reader.GetValue(41).ToString();//trt_all_in
                Bean.strC43 = reader.GetValue(42).ToString();//trt_grupo_id
                Bean.strC44 = reader.GetValue(43).ToString();//trt_contabilizar
                Bean.strC45 = reader.GetValue(44).ToString();//trt_fecha_creacion
                Bean.strC46 = reader.GetValue(45).ToString();//trt_usu_id
                Bean.strC47 = reader.GetValue(46).ToString();//trt_trc_id
                Bean.strC48 = reader.GetValue(47).ToString();//trt_estado
                Bean.strC49 = reader.GetValue(48).ToString();//trb_bl
                Bean.strC50 = reader.GetValue(49).ToString();//trt_empresa_id
                Bean.strC51 = reader.GetValue(50).ToString();//trt_cargo_id
                Bean.strC52 = reader.GetValue(51).ToString();//trt_tiene_referencia_adicional
                Bean.strC53 = reader.GetValue(52).ToString();//trt_id_routing_adicional
                Bean.strC54 = reader.GetValue(53).ToString();//trt_routing_adicional
                Bean.strC55 = reader.GetValue(54).ToString();//trt_tto_id_adicional
                Bean.strC58 = reader.GetValue(55).ToString();//trt_serie
                Bean.strC59 = reader.GetValue(56).ToString();//trt_correlativo

                Bean.strC62 = "0.00";//Debe
                Bean.strC63 = "0.00";//Haber

                Arr_Result.Add(Bean);
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Arr_Result;
    }
    public static ArrayList Get_Transacciones_SCA_Pendientes_Firma(int paiID, int sessionID)
    {
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        XML_Bean Bean = null;
        ArrayList resultado = new ArrayList();
        try
        {
            conn = DB.OpenConnectionBigTimeOut();
            comm = new NpgsqlCommand();
            comm.CommandType = CommandType.Text;
            comm.Connection = conn;
            if ((paiID == 1) || (paiID == 15))
            {
                comm.CommandText = "select '1', tfa_id, tfa_fac_electronica, tfa_internal_reference, tfa_guid, tfa_innerxml, tfa_correlativo, tfa_serie " +
                "from tbl_facturacion " +
                "where tfa_id in (select distinct on(trt_ref_id, trt_ttr_final_id) trt_ref_id " +
                "from tbl_reconciliacion_carga_sesiones, tbl_reconciliacion_carga_bls, tbl_reconciliacion_carga_cuestionario, tbl_reconciliacion_carga_transacciones " +
                "where trs_id=trb_trs_id and trs_estado=1 and trb_estado=1 and trb_id=trc_trb_id and trc_estado=1 and trc_id=trt_trc_id and trt_estado=1 " +
                "and trt_ttr_final_id=1 and trt_empresa_id=" + paiID + " and trt_ttm_id=1 and trs_id=" + sessionID + " ) " +
                "and tfa_fac_electronica in (1,2) and tfa_esignature='-'";
            }
            else if ((paiID == 5) || (paiID == 21))
            {
                comm.CommandText = "select '1', tfa_id, tfa_fac_electronica, tfa_internal_reference, tfa_guid, tfa_innerxml, cast(tfa_correlativo as text), tfa_serie " +
                "from tbl_facturacion " +
                "where tfa_id in (select distinct on(trt_ref_id, trt_ttr_final_id) trt_ref_id " +
                "from tbl_reconciliacion_carga_sesiones, tbl_reconciliacion_carga_bls, tbl_reconciliacion_carga_cuestionario, tbl_reconciliacion_carga_transacciones " +
                "where trs_id=trb_trs_id and trs_estado=1 and trb_estado=1 and trb_id=trc_trb_id and trc_estado=1 and trc_id=trt_trc_id and trt_estado=1 " +
                "and trt_ttr_final_id in (1) and trt_empresa_id=" + paiID + " and trt_ttm_id in (5,8) and trs_id=" + sessionID + " ) " +
                "and tfa_fac_electronica in (1) and tfa_esignature='-' " +
                "union all " +
                "select '4', tnd_id, tnd_fac_electronica, tnd_internal_reference, tnd_guid, tnd_innerxml, cast(tnd_correlativo as text), tnd_serie " +
                "from tbl_nota_debito " +
                "where tnd_id in (select distinct on(trt_ref_id, trt_ttr_final_id) trt_ref_id " +
                "from tbl_reconciliacion_carga_sesiones, tbl_reconciliacion_carga_bls, tbl_reconciliacion_carga_cuestionario, tbl_reconciliacion_carga_transacciones " +
                "where trs_id=trb_trs_id and trs_estado=1 and trb_estado=1 and trb_id=trc_trb_id and trc_estado=1 and trc_id=trt_trc_id and trt_estado=1 " +
                "and trt_ttr_final_id in (4) and trt_empresa_id=" + paiID + " and trt_ttm_id in (5,8) and trs_id=" + sessionID + " ) " +
                "and tnd_fac_electronica in (1) and tnd_esignature='-' ";
            }
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new XML_Bean();
                Bean.stC1 = reader.GetValue(0).ToString();//TTRID
                Bean.intC1 = int.Parse(reader.GetValue(1).ToString());//ID
                Bean.intC2 = int.Parse(reader.GetValue(2).ToString());//TIPOSERIE
                Bean.stC2 = reader.GetValue(3).ToString();//INTERNALREFERENCE
                Bean.stC3 = reader.GetValue(4).ToString();//GUID
                Bean.stC4 = reader.GetValue(5).ToString();//INNERXML
                Bean.stC5 = reader.GetValue(6).ToString();//FOLIO
                resultado.Add(Bean);
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return resultado;
    }
    public static int Actualizar_Estado_Sesion_Cuestionario_Contabilizacion_Automatica(int sesionID, int estado)
    {
        int result = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "update tbl_reconciliacion_carga_sesiones set trs_estado_sesion=" + estado + " where trs_id=" + sesionID + " and trs_estado=1";
            result = comm.ExecuteNonQuery();
            DB.CloseObj_insert(comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return result;
    }
    public static int Get_Posicion_Houses_Cuestionario_Contabilizacion_Automatica(int sessionID)
    {
        int TRB_ID = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select min(trb_id) from " +
            "( " +
            "select trb_id, trb_estado_bl, trb_bl " +
            "from tbl_reconciliacion_carga_bls " +
            "where trb_estado=1 and trb_trs_id=" + sessionID + " and trb_tipo_bl not in ('M') " +
            "and trb_estado_bl not in (2,4) " +
            "order by trb_id asc, trb_estado_bl desc " +
            ") as HOUSE_SIGUIENTE ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                TRB_ID = int.Parse(reader.GetValue(0).ToString());//trb_id
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return TRB_ID;
    }
    public static int Actualizar_Cliente_BL_Sesion_Contabilizacion_Automatica(int trbID, int cliID)
    {
        int result = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "update tbl_reconciliacion_carga_bls set trb_cli_id=" + cliID + " where trb_id=" + trbID + " and trb_estado=1";
            result = comm.ExecuteNonQuery();
            DB.CloseObj_insert(comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return result;
    }
    public static int Actualizar_Informacion_Sesion_Cuestionario_Contabilizacion_Automatica(int sesionID, string sql)
    {
        int result = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = sql;
            result = comm.ExecuteNonQuery();
            DB.CloseObj_insert(comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return result;
    }
    public static ArrayList Get_Reporte_Estado_Embarques(PaisBean Empresa_Bean, int sisID, int ttoID, string Fecha_Inicial, string Fecha_Final, string sql)
    {
        #region Get Datos BL por Cada Trafico
        ArrayList result = new ArrayList();
        RE_GenericBean Bean = null;
        if (sisID == 1)
        {
            #region Cargar Datos Sistema Maritimo
            NpgsqlConnection con_Maritimo;
            NpgsqlCommand com_Maritimo;
            NpgsqlDataReader reader_Maritimo;
            try
            {
                con_Maritimo = DB.OpenVentasConnection(Empresa_Bean.schema);
                com_Maritimo = new NpgsqlCommand();
                com_Maritimo.Connection = con_Maritimo;
                com_Maritimo.CommandType = CommandType.Text;
                if (ttoID == 1)
                {
                    #region FCL
                    com_Maritimo.CommandText = "select a.mbl, a.no_viaje, a.agente_id, a.id_naviera_representante, a.vapor, " +
                    "to_char(a.fecha_arribo, 'yyyy-mm-dd'), to_char(a.fecha_ingreso_sistema, 'yyyy-mm-dd'), " +
                    "a.bl_id, 0 as viaje_id, " +
                    "nombre_AGENTE, NAVIERA_nombre, " +
                    "coalesce(TRS_FECHA, '-'), coalesce(TRS_USUARIO,'-') , coalesce(TRS_ESTADO, '0'), " +
                    "coalesce(USUARIO_INGRESO, '-'), coalesce(TRS_USUARIO_CONTABILIZA, '-'), coalesce(TRS_FECHA_CONTABILIZACION, '-') " +
                    "from bl_completo a inner join contenedor_completo b on (a.bl_id=b.bl_id and a.import_export=true) " +
                    "left join dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select id_routing, routing, vendedor_id from routings') " +
                    "Routings_Result(routing_id bigint, numero_routing varchar, vendedor_id varchar) on (a.id_routing=routing_id) " +
                    "left join dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select agente_id, agente from agentes where activo=true') " +
                    "Agentes_Result(id_AGENTE bigint, nombre_AGENTE varchar) on (a.agente_id=id_AGENTE) " +
                    "left join dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select id_naviera, nombre from navieras where activo=true') " +
                    "Navieras_Result(NAVIERA_id bigint, NAVIERA_nombre varchar) on (a.id_naviera_representante=NAVIERA_id) " +
                    "left join dblink ('dbname=aimar_baw host=10.10.1.18 port=5432 user=dbmaster password=aimargt', 'select trs_id, trs_empresa_id, trs_sis_id, trs_tto_id, trs_bl, trs_viaje_no, trs_fecha_creacion, trs_usu_id, trs_estado_sesion, trs_usu_contabilizacion, trs_fecha_contabilizacion  from tbl_reconciliacion_carga_sesiones where trs_estado=1') " +
                    "SCA_Result(TRS_ID int, TRS_EMPRESA_ID int, TRS_SISTEMA_ID int, TRS_TTO_ID int, TRS_BL varchar, TRS_VIAJE varchar, TRS_FECHA varchar, TRS_USUARIO varchar, TRS_ESTADO int, TRS_USUARIO_CONTABILIZA varchar, TRS_FECHA_CONTABILIZACION varchar) " +
                    "on (a.mbl=TRS_BL " + sql + " ) " +
                    "left join dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select id_usuario, pw_name from usuarios_empresas') " +
                    "Usuarios_Result(USUARIO_INGRESO_ID bigint, USUARIO_INGRESO varchar) on (a.user_id=USUARIO_INGRESO_ID) " +
                    "where a.activo=true and a.fecha_arribo>='" + Fecha_Inicial + "' and a.fecha_arribo<='" + Fecha_Final + "' " +
                    "group by 8, 9, 6, 1, 2, 7, 5, 3, 4, 10, 11, 12, 13, 14, 15, 16, 17 ";
                    #endregion
                }
                else if (ttoID == 2)
                {
                    #region LCL
                    com_Maritimo.CommandText = "select b.mbl, c.no_viaje, c.agente_id, c.id_naviera_representante, c.vapor, " +
                    "to_char(c.fecha_arribo, 'yyyy-mm-dd'), to_char(a.fecha_ingreso_sistema, 'yyyy-mm-dd'), " +
                    "a.viaje_contenedor_id, b.viaje_id, " +
                    "nombre_AGENTE, NAVIERA_nombre, " +
                    "coalesce(TRS_FECHA, '-'), coalesce(TRS_USUARIO,'-') , coalesce(TRS_ESTADO, '0'), " +
                    "coalesce(USUARIO_INGRESO, '-'), coalesce(TRS_USUARIO_CONTABILIZA, '-'), coalesce(TRS_FECHA_CONTABILIZACION, '-') " +
                    "from bill_of_lading as a inner join viaje_contenedor as b on (b.viaje_contenedor_id=a.viaje_contenedor_id and b.activo=true) " +
                    "inner join viajes as c on (c.viaje_id=b.viaje_id and c.activo=true and c.import_export=true) " +
                    "left join dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select id_routing, routing, vendedor_id from routings') " +
                    "Routings_Result(routing_id bigint, numero_routing varchar, vendedor_id varchar) on (a.id_routing=routing_id) " +
                    "left join dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select agente_id, agente from agentes where activo=true') " +
                    "Agentes_Result(id_AGENTE bigint, nombre_AGENTE varchar) on (c.agente_id=id_AGENTE) " +
                    "left join dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select id_naviera, nombre from navieras where activo=true') " +
                    "Navieras_Result(NAVIERA_id bigint, NAVIERA_nombre varchar) on (c.id_naviera_representante=NAVIERA_id) " +
                    "left join dblink ('dbname=aimar_baw host=10.10.1.18 port=5432 user=dbmaster password=aimargt', 'select trs_id, trs_empresa_id, trs_sis_id, trs_tto_id, trs_bl, trs_viaje_no, trs_fecha_creacion, trs_usu_id, trs_estado_sesion, trs_usu_contabilizacion, trs_fecha_contabilizacion  from tbl_reconciliacion_carga_sesiones where trs_estado=1') " +
                    "SCA_Result(TRS_ID int, TRS_EMPRESA_ID int, TRS_SISTEMA_ID int, TRS_TTO_ID int, TRS_BL varchar, TRS_VIAJE varchar, TRS_FECHA varchar, TRS_USUARIO varchar, TRS_ESTADO int, TRS_USUARIO_CONTABILIZA varchar, TRS_FECHA_CONTABILIZACION varchar) " +
                    "on (b.mbl=TRS_BL " + sql + " ) " +
                    "left join dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select id_usuario, pw_name from usuarios_empresas') " +
                    "Usuarios_Result(USUARIO_INGRESO_ID bigint, USUARIO_INGRESO varchar) on (c.user_id=USUARIO_INGRESO_ID) " +
                    "where a.activo=true and c.fecha_arribo>='" + Fecha_Inicial + "' and c.fecha_arribo<='" + Fecha_Final + "' " +
                    "group by 8, 9, 6, 1, 2, 7, 5, 3, 4, 10, 11, 12, 13, 14, 15, 16, 17 ";
                    #endregion
                }
                reader_Maritimo = com_Maritimo.ExecuteReader();
                while (reader_Maritimo.Read())
                {
                    Bean = new RE_GenericBean();
                    Bean.strC1 = reader_Maritimo.GetValue(0).ToString();//MBL
                    Bean.strC2 = reader_Maritimo.GetValue(1).ToString();//NO VIAJE
                    Bean.strC3 = reader_Maritimo.GetValue(2).ToString();//AGENTE ID
                    Bean.strC4 = reader_Maritimo.GetValue(3).ToString();//NAVIERA ID
                    Bean.strC5 = reader_Maritimo.GetValue(4).ToString();//VAPOR
                    Bean.strC6 = reader_Maritimo.GetValue(5).ToString();//FECHA ARRIBO
                    Bean.strC7 = reader_Maritimo.GetValue(6).ToString();//FECHA INGRESO
                    Bean.strC8 = reader_Maritimo.GetValue(7).ToString();//VIAJE CONTENEDOR ID
                    Bean.strC9 = reader_Maritimo.GetValue(8).ToString();//VIAJE ID
                    Bean.strC10 = reader_Maritimo.GetValue(9).ToString();//NOMBRE AGENTE
                    Bean.strC11 = reader_Maritimo.GetValue(10).ToString();//NOMBRE NAVIERA
                    Bean.strC12 = reader_Maritimo.GetValue(11).ToString();//FECHA DE INICIO DE CONTABILIZACION
                    Bean.strC13 = reader_Maritimo.GetValue(12).ToString();//USUARIO DE INICIO DE CONTABILIZACION
                    Bean.strC14 = reader_Maritimo.GetValue(13).ToString();//ESTADO DE CONTABILIZACION
                    Bean.strC15 = reader_Maritimo.GetValue(14).ToString();//USUARIO DE INGRESO
                    Bean.strC16 = reader_Maritimo.GetValue(15).ToString();//USUARIO CONTABILIZA
                    Bean.strC17 = reader_Maritimo.GetValue(16).ToString();//FECHA CONTABILIZACION
                    #region Formatear Fecha de Contabilizacion
                    if (Bean.strC17 != "-")
                    {
                        Bean.strC17 = Bean.strC17.Substring(0, 10);
                    }
                    #endregion
                    #region Traducion de Estados de Contabilizacion
                    //1 Sin Responder
                    //2 Respondida
                    //3 Respondiendo
                    //4 Contabilizada
                    if ((Bean.strC14 == "0") || (Bean.strC14 == "1"))
                    {
                        Bean.strC14 = "SIN CONTABILIZAR";
                    }
                    else if (Bean.strC14 == "2")
                    {
                        Bean.strC14 = "RESPONDIDO";
                    }
                    else if (Bean.strC14 == "3")
                    {
                        Bean.strC14 = "CONTABILIZANDO";
                    }
                    else if (Bean.strC14 == "4")
                    {
                        Bean.strC14 = "CONTABILIZADO";
                    }
                    #endregion
                    result.Add(Bean);
                }
                DB.CloseObj(reader_Maritimo, com_Maritimo, con_Maritimo);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                return null;
            }
            #endregion
        }
        else if (sisID == 2)
        {
            #region Cargar Datos Sistema Aereo
            MySqlConnection con_Aereo;
            MySqlCommand com_Aereo;
            MySqlDataReader reader_Aereo;
            try
            {
                con_Aereo = DB.OpenAereoConnection();
                com_Aereo = new MySqlCommand();
                com_Aereo.Connection = con_Aereo;
                com_Aereo.CommandType = CommandType.Text;
                if (ttoID == 3)
                {
                    #region Importacion
                    com_Aereo.CommandText = "";
                    #endregion
                }
                else if (ttoID == 4)
                {
                    #region Exportacion
                    com_Aereo.CommandText = "";
                    #endregion
                }
                reader_Aereo = com_Aereo.ExecuteReader();
                while (reader_Aereo.Read())
                {
                }
                DB.CloseMySQLObj(reader_Aereo, com_Aereo, con_Aereo);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                return null;
            }
            #endregion
        }
        else if (sisID == 3)
        {
            #region Cargar Datos Sistema Terrestre
            MySqlConnection con_Terrestre;
            MySqlCommand com_Terrestre;
            MySqlDataReader reader_Terrestre;
            try
            {
                con_Terrestre = DB.OpenTerrestreConnection();
                com_Terrestre = new MySqlCommand();
                com_Terrestre.Connection = con_Terrestre;
                com_Terrestre.CommandType = CommandType.Text;
                if ((ttoID == 5) || (ttoID == 6) || (ttoID == 7))
                {
                    #region Express-Consolidado-Local
                    string paisISO = "";
                    com_Terrestre.CommandText = "";
                    #endregion
                }
                reader_Terrestre = com_Terrestre.ExecuteReader();
                while (reader_Terrestre.Read())
                {   
                }
                DB.CloseMySQLObj(reader_Terrestre, com_Terrestre, con_Terrestre);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                return null;
            }
            #endregion
        }
        return result;
        #endregion
    }
    public static ArrayList Get_Resumen_Embarques(int empresaID, int sisID, int ttoID, string Fecha_Inicial, string Fecha_Final)
    {
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        RE_GenericBean Bean = null;
        ArrayList resultado = new ArrayList();
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.CommandType = CommandType.Text;
            comm.Connection = conn;
            comm.CommandText = "select  trs_id, " +
            "sum( round (cast((case when trt_ttm_id=8 then trt_agente_cobrar else trt_agente_cobrar/trs_tipo_cambio_contabilizacion end ) as numeric),2)), " +
            "sum( round (cast((case when trt_ttm_id=8 then trt_agente_pagar else trt_agente_pagar/trs_tipo_cambio_contabilizacion end ) as numeric),2)), " +
            "sum( round (cast((case when trt_ttm_id=8 then trt_naviera_cobrar else trt_naviera_cobrar/trs_tipo_cambio_contabilizacion end ) as numeric),2)), " +
            "sum( round (cast((case when trt_ttm_id=8 then trt_naviera_pagar else trt_naviera_pagar/trs_tipo_cambio_contabilizacion end ) as numeric),2)), " +
            "sum( round (cast((case when trt_ttm_id=8 then trt_cliente_cobrar else trt_cliente_cobrar/trs_tipo_cambio_contabilizacion end ) as numeric),2)), " +
            "sum( round (cast((case when trt_ttm_id=8 then trt_cliente_pagar else trt_cliente_pagar/trs_tipo_cambio_contabilizacion end ) as numeric),2)), " +
            "sum( round (cast((case when trt_ttm_id=8 then trt_intermodal_cobrar else trt_intermodal_cobrar/trs_tipo_cambio_contabilizacion end ) as numeric),2)), " +
            "sum( round (cast((case when trt_ttm_id=8 then trt_intermodal_pagar else trt_intermodal_pagar/trs_tipo_cambio_contabilizacion end ) as numeric),2)), " +
            "sum( round (cast((case when trt_ttm_id=8 then trt_intercompany_destino_cobrar else trt_intercompany_destino_cobrar/trs_tipo_cambio_contabilizacion end ) as numeric),2)), " +
            "sum( round (cast((case when trt_ttm_id=8 then trt_intercompany_destino_pagar else trt_intercompany_destino_pagar/trs_tipo_cambio_contabilizacion end ) as numeric),2)), " +
            "sum( round (cast((case when trt_ttm_id=8 then trt_ingresos else trt_ingresos/trs_tipo_cambio_contabilizacion end ) as numeric),2)), " +
            "sum( round (cast((case when trt_ttm_id=8 then trt_costos else trt_costos/trs_tipo_cambio_contabilizacion end ) as numeric),2)), " +
            "trs_usu_contabilizacion, trs_fecha_arribo, to_char(trs_fecha_contabilizacion, 'yyyy-mm-dd'), trs_tipo_cambio_contabilizacion, " +
            "trs_bl, trs_viaje_no, " +
            "trs_agente_id, trs_naviera_id, " +
            "nombre_AGENTE, NAVIERA_nombre " +
            "from tbl_reconciliacion_carga_sesiones " +
            "inner join tbl_reconciliacion_carga_bls on (trs_id=trb_trs_id and trs_estado=1 and trb_estado=1) " +
            "inner join tbl_reconciliacion_carga_cuestionario on (trb_id=trc_trb_id and trc_estado=1) " +
            "inner join tbl_reconciliacion_carga_transacciones on (trc_id=trt_trc_id and trt_estado=1) " +
            "left join dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select agente_id, agente from agentes where activo=true') " +
            "Agentes_Result(id_AGENTE bigint, nombre_AGENTE varchar) on (trs_agente_id=id_AGENTE) " +
            "left join dblink ('dbname=master-aimar host=10.10.1.20 port=5432 user=dbmaster password=aimargt', 'select id_naviera, nombre from navieras where activo=true') " +
            "Navieras_Result(NAVIERA_id bigint, NAVIERA_nombre varchar) on (trs_naviera_id=NAVIERA_id) " +
            "where trs_empresa_id=" + empresaID + " and trs_sis_id=" + sisID + " and trs_tto_id=" + ttoID + " and trs_estado_sesion=4 and trs_fecha_contabilizacion>='" + Fecha_Inicial + " 00:00:00' and trs_fecha_contabilizacion<='" + Fecha_Final + " 23:59:59' " +
            "group by trs_id, trs_fecha_contabilizacion, nombre_AGENTE, NAVIERA_nombre order by trs_fecha_contabilizacion asc";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.intC1 = int.Parse(reader.GetValue(0).ToString());//trs_id
                Bean.douC1 = double.Parse(reader.GetValue(1).ToString());//trt_agente_cobrar
                Bean.douC2 = double.Parse(reader.GetValue(2).ToString());//trt_agente_pagar
                Bean.douC3 = double.Parse(reader.GetValue(3).ToString());//trt_naviera_cobrar
                Bean.douC4 = double.Parse(reader.GetValue(4).ToString());//trt_naviera_pagar
                Bean.douC5 = double.Parse(reader.GetValue(5).ToString());//trt_cliente_cobrar
                Bean.douC6 = double.Parse(reader.GetValue(6).ToString());//trt_cliente_pagar
                Bean.douC7 = double.Parse(reader.GetValue(7).ToString());//trt_intermodal_cobrar
                Bean.douC8 = double.Parse(reader.GetValue(8).ToString());//trt_intermodal_pagar
                Bean.douC9 = double.Parse(reader.GetValue(9).ToString());//trt_intercompany_destino_cobrar
                Bean.douC10 = double.Parse(reader.GetValue(10).ToString());//trt_intercompany_destino_pagar
                Bean.douC11 = double.Parse(reader.GetValue(11).ToString());//trt_ingresos
                Bean.douC12 = double.Parse(reader.GetValue(12).ToString());//trt_costos
                Bean.strC1 = reader.GetValue(13).ToString();//trs_usu_contabilizacion
                Bean.strC2 = reader.GetValue(14).ToString();//trs_fecha_arribo
                Bean.strC3 = reader.GetValue(15).ToString();//trs_fecha_contabilizacion
                Bean.strC4 = reader.GetValue(16).ToString();//trs_tipo_cambio_contabilizacion
                Bean.strC5 = reader.GetValue(17).ToString();//trs_bl
                Bean.strC6 = reader.GetValue(18).ToString();//trs_viaje_no
                Bean.strC7 = reader.GetValue(19).ToString();//trs_agente_id
                Bean.strC8 = reader.GetValue(20).ToString();//trs_naviera_id
                Bean.strC9 = reader.GetValue(21).ToString();//nombre_AGENTE
                Bean.strC10 = reader.GetValue(22).ToString();//NAVIERA_nombre
                resultado.Add(Bean);

            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return resultado;
    }
    //public static RE_GenericBean Obtener_Transaccion_Padre_SCA_Intercompany_Administrativo(int SesionID, int grupoID, int ttr_finalID)
    public static RE_GenericBean Obtener_Transaccion_Padre_SCA_Intercompany_Administrativo(int SesionID, int grupoID, int ttr_finalID)
    {
        RE_GenericBean Result = null;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            #region Backup
            //comm.CommandText = "select  trt_ttr_final_id, trt_ref_id, trt_empresa_id " +
            //"from tbl_reconciliacion_carga_sesiones, tbl_reconciliacion_carga_bls, tbl_reconciliacion_carga_cuestionario, tbl_reconciliacion_carga_transacciones " +
            //"where trs_id=trb_trs_id and trs_estado=1 and trb_estado=1 and trb_id=trc_trb_id and trc_estado=1 and trc_id=trt_trc_id and trt_estado=1 " +
            //"and trs_id=" + SesionID + " and trt_grupo_id=" + grupoID + " and trt_ttr_final_id=" + ttr_finalID + " ";
            #endregion
            comm.CommandText = "select  trt_ttr_final_id, trt_ref_id, trt_empresa_id " +
            "from tbl_reconciliacion_carga_sesiones, tbl_reconciliacion_carga_bls, tbl_reconciliacion_carga_cuestionario, tbl_reconciliacion_carga_transacciones " +
            "where trs_id=trb_trs_id and trs_estado=1 and trb_estado=1 and trb_id=trc_trb_id and trc_estado=1 and trc_id=trt_trc_id and trt_estado=1 " +
            "and trs_id=" + SesionID + " and trt_grupo_id=" + grupoID + " and trt_ttr_final_id not in (5) ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Result = new RE_GenericBean();
                Result.intC1 = int.Parse(reader.GetValue(0).ToString());//TTRID
                Result.intC2 = int.Parse(reader.GetValue(1).ToString());//REFIF
                Result.intC3 = int.Parse(reader.GetValue(2).ToString());//EMPRESAID
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Result;
    }
    public static int Actualizar_Sucursal_Sesion_Contabilizacion_Automatica(int sesionID, int sucID)
    {
        int result = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "update tbl_reconciliacion_carga_sesiones set trs_sucursal_id=" + sucID + " where trs_id=" + sesionID + " and trs_estado=1";
            result = comm.ExecuteNonQuery();
            DB.CloseObj_insert(comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return result;
    }
    public static int Insertar_Totales_Sesion_Contabilizacion_Automatica(RE_GenericBean Bean)
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
            comm.CommandText = "insert into tbl_reconciliacion_carga_totales (trcto_trs_id, trcto_agente_cobrar_local, trcto_agente_pagar_local, trcto_agente_cobrar_usd, trcto_agente_pagar_usd, trcto_naviera_cobrar_local, trcto_naviera_pagar_local, trcto_naviera_cobrar_usd, trcto_naviera_pagar_usd) ";
            comm.CommandText += "values (@trcto_trs_id, @trcto_agente_cobrar_local, @trcto_agente_pagar_local, @trcto_agente_cobrar_usd, @trcto_agente_pagar_usd, @trcto_naviera_cobrar_local, @trcto_naviera_pagar_local, @trcto_naviera_cobrar_usd, @trcto_naviera_pagar_usd) ";
            comm.Parameters.Add("@trcto_trs_id", NpgsqlTypes.NpgsqlDbType.Integer).Value = Bean.intC1;
            comm.Parameters.Add("@trcto_agente_cobrar_local", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Bean.douC1;
            comm.Parameters.Add("@trcto_agente_pagar_local", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Bean.douC2;
            comm.Parameters.Add("@trcto_agente_cobrar_usd", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Bean.douC3;
            comm.Parameters.Add("@trcto_agente_pagar_usd", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Bean.douC4;
            comm.Parameters.Add("@trcto_naviera_cobrar_local", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Bean.douC5;
            comm.Parameters.Add("@trcto_naviera_pagar_local", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Bean.douC6;
            comm.Parameters.Add("@trcto_naviera_cobrar_usd", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Bean.douC7;
            comm.Parameters.Add("@trcto_naviera_pagar_usd", NpgsqlTypes.NpgsqlDbType.Numeric).Value = Bean.douC8;
            resultado = comm.ExecuteNonQuery();
            comm.Parameters.Clear();
            comm.CommandText = "";
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
    public static int Validar_Existencia_Totales_Sesion_Contabilizacion_Automatica(int sesionID)
    {
        int resultado = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select trcto_id, trcto_trs_id, " +
            "trcto_agente_cobrar_local, trcto_agente_pagar_local, trcto_agente_cobrar_usd, trcto_agente_pagar_usd, " +
            "trcto_naviera_cobrar_local, trcto_naviera_pagar_local, trcto_naviera_cobrar_usd, trcto_naviera_pagar_usd " +
            "from tbl_reconciliacion_carga_totales " +
            "where trcto_trs_id=" + sesionID + " and trcto_estado=1 ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                resultado = int.Parse(reader.GetValue(0).ToString());
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return resultado;
    }
    public static int Actualizar_Totales_Sesion_Contabilizacion_Automatica(RE_GenericBean Bean)
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
            comm.CommandText = "update tbl_reconciliacion_carga_totales set trcto_agente_cobrar_local="+Bean.douC1+", trcto_agente_pagar_local="+Bean.douC2+", trcto_agente_cobrar_usd="+Bean.douC3+", trcto_agente_pagar_usd="+Bean.douC4+", ";
            comm.CommandText += "trcto_naviera_cobrar_local=" + Bean.douC5 + ", trcto_naviera_pagar_local=" + Bean.douC6 + ", trcto_naviera_cobrar_usd=" + Bean.douC7 + ", trcto_naviera_pagar_usd=" + Bean.douC8 + " ";
            comm.CommandText += "where trcto_trs_id=" + Bean.intC1 + " and trcto_estado=1";
            resultado = comm.ExecuteNonQuery();
            comm.Parameters.Clear();
            comm.CommandText = "";
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
    public static ArrayList Validar_Total_SOAS_VS_Total_Sesion_Contabilizacion_Automatica(int sesionID)
    {
        ArrayList Arr_Result = null;
        RE_GenericBean Bean_Total_SOAS = null;
        RE_GenericBean Bean_Totales_Sesion = null;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select trcto_agente_cobrar_local, trcto_agente_pagar_local, " +
            "trcto_agente_cobrar_usd, trcto_agente_pagar_usd, " +
            "trcto_naviera_cobrar_local, trcto_naviera_pagar_local, " +
            "trcto_naviera_cobrar_usd, trcto_naviera_pagar_usd " +
            "from tbl_reconciliacion_carga_totales where trcto_trs_id=" + sesionID + " and trcto_estado=1 ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean_Total_SOAS = new RE_GenericBean();
                Bean_Total_SOAS.douC1 = Convert.ToDouble(reader.GetValue(0).ToString());//trcto_agente_cobrar_local
                Bean_Total_SOAS.douC2 = Convert.ToDouble(reader.GetValue(1).ToString());//trcto_agente_pagar_local
                Bean_Total_SOAS.douC3 = Convert.ToDouble(reader.GetValue(2).ToString());//trcto_agente_cobrar_usd
                Bean_Total_SOAS.douC4 = Convert.ToDouble(reader.GetValue(3).ToString());//trcto_agente_pagar_usd
                Bean_Total_SOAS.douC5 = Convert.ToDouble(reader.GetValue(4).ToString());//trcto_naviera_cobrar_local
                Bean_Total_SOAS.douC6 = Convert.ToDouble(reader.GetValue(5).ToString());//trcto_naviera_pagar_local
                Bean_Total_SOAS.douC7 = Convert.ToDouble(reader.GetValue(6).ToString());//trcto_naviera_cobrar_usd
                Bean_Total_SOAS.douC8 = Convert.ToDouble(reader.GetValue(7).ToString());//trcto_naviera_pagar_usd
            }
            comm.CommandText = "select  trt_ttm_id, " +
            "sum(trt_agente_cobrar) as Agente_Cobrar, sum(trt_agente_pagar) as Agente_Pagar, " +
            "sum(trt_naviera_cobrar) as Naviera_Cobrar, sum(trt_naviera_pagar) as Naviera_Pagar " +
            "from tbl_reconciliacion_carga_sesiones, tbl_reconciliacion_carga_bls, " +
            "tbl_reconciliacion_carga_cuestionario, tbl_reconciliacion_carga_transacciones " +
            "where trs_id=trb_trs_id and trs_estado=1 and trb_estado=1 and trb_id=trc_trb_id and trc_estado=1 and trc_id=trt_trc_id and trt_estado=1 " +
            "and trs_id=" + sesionID + " " +
            "group by trt_ttm_id ";
            reader = comm.ExecuteReader();
            Bean_Totales_Sesion = new RE_GenericBean();
            while (reader.Read())
            {
                Bean_Totales_Sesion.intC1 = Convert.ToInt32(reader.GetValue(0).ToString());//trt_ttm_id
                if (Bean_Totales_Sesion.intC1 != 8)
                {
                    Bean_Totales_Sesion.douC1 = Convert.ToDouble(reader.GetValue(1).ToString());//Agente_Cobrar Local
                    Bean_Totales_Sesion.douC2 = Convert.ToDouble(reader.GetValue(2).ToString());//Agente_Pagar Local
                    Bean_Totales_Sesion.douC5 = Convert.ToDouble(reader.GetValue(3).ToString());//Naviera_Cobrar LocaL
                    Bean_Totales_Sesion.douC6 = Convert.ToDouble(reader.GetValue(4).ToString());//Naviera_Pagar Local
                }
                else if (Bean_Totales_Sesion.intC1 == 8)
                {
                    Bean_Totales_Sesion.douC3 = Convert.ToDouble(reader.GetValue(1).ToString());//Agente_Cobrar USD
                    Bean_Totales_Sesion.douC4 = Convert.ToDouble(reader.GetValue(2).ToString());//Agente_Pagar USD
                    Bean_Totales_Sesion.douC7 = Convert.ToDouble(reader.GetValue(3).ToString());//Naviera_Cobrar USD
                    Bean_Totales_Sesion.douC8 = Convert.ToDouble(reader.GetValue(4).ToString());//Naviera_Pagar USD
                }
            }
            DB.CloseObj(reader, comm, conn);

            Arr_Result = new ArrayList();
            if ((Bean_Total_SOAS != null) && (Bean_Totales_Sesion != null))
            {
                Arr_Result.Add("1");
                Arr_Result.Add(Bean_Total_SOAS);
                Arr_Result.Add(Bean_Totales_Sesion);
            }
            else
            {
                Arr_Result.Add("0");
                Arr_Result.Add(Bean_Total_SOAS);
                Arr_Result.Add(Bean_Totales_Sesion);
            }
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Arr_Result;
    }
    public static ArrayList Get_Cobros_Intercompany_Sesion_Contabilizacion_Automatica(int sesionID)
    {
        ArrayList Arr_Result = new ArrayList();
        RE_GenericBean Bean_Cobro = null;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select trt_ttr_final_id, trt_ref_id, trt_serie, trt_correlativo " +
            "from tbl_reconciliacion_carga_sesiones, tbl_reconciliacion_carga_bls, " +
            "tbl_reconciliacion_carga_cuestionario, tbl_reconciliacion_carga_transacciones " +
            "where trs_id=trb_trs_id and trs_estado=1 and trb_estado=1 and trb_id=trc_trb_id " +
            "and trc_estado=1 and trc_id=trt_trc_id and trt_estado=1 and trs_id=" + sesionID + " " +
            "and trt_tpi_id=10 " +
            "and trt_ttr_final_id not in (5) ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean_Cobro = new RE_GenericBean();
                Bean_Cobro.intC1 = int.Parse(reader.GetValue(0).ToString());//trt_ttr_final_id
                Bean_Cobro.intC2 = int.Parse(reader.GetValue(1).ToString());//trt_ref_id
                Bean_Cobro.strC1 = reader.GetValue(2).ToString();//trt_serie
                Bean_Cobro.strC2 = reader.GetValue(3).ToString();//trt_correlativo
                Arr_Result.Add(Bean_Cobro);
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return null;
        }
        return Arr_Result;
    }
    public static int Get_Transaccion_Intercompany_Hija_Sesion_Contabilizacion_Automatica(int ttrID, int refID)
    {
        int resultado = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select ttel_hijo_ref_id  " +
            "from tbl_transacciones_encadenadas_log " +
            "where ttel_padre_ttr_id=" + ttrID + " and ttel_padre_ref_id=" + refID + " " +
            "and ttel_estado=1 and ttel_hijo_ttr_id=5 ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                resultado = int.Parse(reader.GetValue(0).ToString());//ttel_hijo_ref_id
            }
            DB.CloseObj(reader, comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            return -100;
        }
        return resultado;
    }
    public static ArrayList Marcar_Pago_Intercompany_SCA_Con_Documento_Cobro(int provID, string serieINTERCOMPANY, string correlativoINTERCOMPANY)
    {
        ArrayList Arr_Result = new ArrayList();
        int result = 0;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        try
        {
            conn = DB.OpenConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "update tbl_provisiones set tpr_fact_id='" + serieINTERCOMPANY + "', tpr_fact_corr='" + correlativoINTERCOMPANY + "' where tpr_prov_id=" + provID + " ";
            result = comm.ExecuteNonQuery();
            comm.Parameters.Clear();
            DB.CloseObj_insert(comm, conn);
        }
        catch (Exception e)
        {
            log4net ErrLog = new log4net();
            ErrLog.ErrorLog(e.Message);
            Arr_Result = new ArrayList();
            Arr_Result.Add("0");
            Arr_Result.Add("Existio un error al tratar de asignar los Datos del Documento de Cobro a la Provision");
            return Arr_Result;
        }
        Arr_Result = new ArrayList();
        Arr_Result.Add("1");
        Arr_Result.Add("Transaccion Marcada Exitosamente");
        return Arr_Result;
    }
    public static RE_GenericBean Obtener_Puerto_Origen(int puerto_origen_id)
    {
        RE_GenericBean Bean = null;
        NpgsqlConnection conn;
        NpgsqlCommand comm;
        NpgsqlDataReader reader;
        try
        {
            conn = DB.OpenMasterConnection();
            comm = new NpgsqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "select c.id_region, a.codigo, b.unlocode_id, c.descripcion, a.descripcion, b.nombre " +
            "from paises a inner join unlocode b on (a.codigo=b.pais) " +
            "inner join region c on (a.id_region=c.id_region) " +
            "where b.unlocode_id=" + puerto_origen_id + " ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.intC1 = int.Parse(reader.GetValue(0).ToString());//id_region
                Bean.strC1 = reader.GetValue(1).ToString();//codigo pais
                Bean.intC2 = int.Parse(reader.GetValue(2).ToString());//unlocode_id
                Bean.strC2 = reader.GetValue(3).ToString();//Region Nombre
                Bean.strC3 = reader.GetValue(4).ToString();//Pais Nombre
                Bean.strC4 = reader.GetValue(5).ToString();//Puerto Origen
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
    }
    public static RE_GenericBean Get_HBL_Data_Reconciliacion_Carga(int trb_id)
    {
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
            comm.CommandText = "select trb_id, trb_tipo_bl, trb_bl_id, trb_bl, trb_routing_id, trb_routing, trb_contenedor_id, trb_contenedor, " +
            "trb_peso, trb_volumen, trb_cli_id, trb_destino, trb_trs_id, trb_estado_bl, trb_to_order, trb_to_order_id, trb_puerto_origen_id " +
            "from tbl_reconciliacion_carga_bls " +
            "where trb_estado=1 and trb_id=" + trb_id + " ";
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                Bean = new RE_GenericBean();
                Bean.strC1 = reader.GetValue(0).ToString();//trb_id
                Bean.strC2 = reader.GetValue(1).ToString();//trb_tipo_bl
                Bean.strC3 = reader.GetValue(2).ToString();//trb_bl_id
                Bean.strC4 = reader.GetValue(3).ToString();//trb_bl
                Bean.strC5 = reader.GetValue(4).ToString();//trb_routing_id
                Bean.strC6 = reader.GetValue(5).ToString();//trb_routing
                Bean.strC7 = reader.GetValue(6).ToString();//trb_contenedor_id
                Bean.strC8 = reader.GetValue(7).ToString();//trb_contenedor
                Bean.strC9 = reader.GetValue(8).ToString();//trb_peso
                Bean.strC10 = reader.GetValue(9).ToString();//trb_volumen
                Bean.strC11 = reader.GetValue(10).ToString();//trb_cli_id
                Bean.strC12 = reader.GetValue(11).ToString();//trb_destino
                Bean.strC13 = reader.GetValue(12).ToString();//trb_trs_id
                Bean.strC14 = reader.GetValue(13).ToString();//trb_estado_bl
                Bean.strC15 = reader.GetValue(14).ToString();//trb_to_order
                Bean.strC16 = reader.GetValue(15).ToString();//trb_to_order_id
                Bean.strC17 = reader.GetValue(16).ToString();//trb_puerto_origen_id
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
    }

    public static ArrayList Get_Cargos_Intercompany_Trafico(PaisBean Pais_Bean, int SisID, int Tipo, int blID)
    {
        #region Get Cargos por Traficos
        string schema = "";
        ArrayList Arr_Cargos = new ArrayList();
        Bean_Cargos Bean_Cargos = null;
        #region Definir Schema
        if ((Tipo == 17) || (Tipo == 18))
        {
            schema = Pais_Bean.schema_apl;
        }
        else
        {
            schema = Pais_Bean.schema;
        }
        #endregion
        
        if (SisID == 3)
        {
            #region Cargar Costos Sistema Terrestre
            MySqlConnection con_Terrestre;
            MySqlCommand com_Terrestre;
            MySqlDataReader reader_Terrestre;
            try
            {
                con_Terrestre = DB.OpenTerrestreConnection();
                com_Terrestre = new MySqlCommand();
                com_Terrestre.Connection = con_Terrestre;
                com_Terrestre.CommandType = CommandType.Text;
                if ((Tipo == 5) || (Tipo == 6) || (Tipo == 7))
                {
                    #region Express-Consolidado-Local
                    com_Terrestre.CommandText = "select ChargeID, SBLID, ItemID, ServiceID, Currency, (Value+ OverSold), InvoiceID, 'Import', DocType, " +
                    "InterChargeType, Local, InterCompanyID, InterGroupID, InterProviderType, Local, PrepaidCollect " +
                    "from ChargeItems " +
                    "where SBLID=" + blID + " and InvoiceID=0 and Expired=0 and InterChargeType=2 and InterProviderType=5; ";
                    #endregion
                }
                reader_Terrestre = com_Terrestre.ExecuteReader();
                while (reader_Terrestre.Read())
                {
                    Bean_Cargos = new Bean_Cargos();
                    Bean_Cargos.Cargo_ID = int.Parse(reader_Terrestre.GetValue(0).ToString());
                    Bean_Cargos.Cargo_BLID = int.Parse(reader_Terrestre.GetValue(1).ToString());
                    Bean_Cargos.Cargo_Rub_ID = int.Parse(reader_Terrestre.GetValue(2).ToString());
                    Bean_Cargos.Cargo_Servicio_ID = int.Parse(reader_Terrestre.GetValue(3).ToString());
                    Bean_Cargos.Cargo_Moneda_Simbolo = reader_Terrestre.GetValue(4).ToString();
                    Bean_Cargos.Cargo_Moneda_ID = Utility.TraducirMonedaStr(Bean_Cargos.Cargo_Moneda_Simbolo);
                    Bean_Cargos.Cargo_Monto = double.Parse(reader_Terrestre.GetValue(5).ToString());
                    Bean_Cargos.Factura_ID = int.Parse(reader_Terrestre.GetValue(6).ToString());
                    Bean_Cargos.Cargo_Tipo_BL = "";
                    Bean_Cargos.Tipo_Documento = int.Parse(reader_Terrestre.GetValue(8).ToString());
                    Bean_Cargos.Tipo_Cargo = int.Parse(reader_Terrestre.GetValue(9).ToString());
                    Bean_Cargos.Tipo_Cobro = int.Parse(reader_Terrestre.GetValue(10).ToString());
                    Bean_Cargos.ID_Intercompany = int.Parse(reader_Terrestre.GetValue(11).ToString());
                    Bean_Cargos.ID_Grupo = int.Parse(reader_Terrestre.GetValue(12).ToString());
                    Bean_Cargos.ID_Tipo_Persona = int.Parse(reader_Terrestre.GetValue(13).ToString());
                    Bean_Cargos.ID_Local_Internacional = int.Parse(reader_Terrestre.GetValue(14).ToString());
                    Bean_Cargos.ID_prepaid_collect = int.Parse(reader_Terrestre.GetValue(15).ToString());
                    Arr_Cargos.Add(Bean_Cargos);
                }
                DB.CloseMySQLObj(reader_Terrestre, com_Terrestre, con_Terrestre);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                return null;
            }
            #endregion
        }
        return Arr_Cargos;
        #endregion
    }

    public static ArrayList Validar_Cargos_Collect(PaisBean Pais_Bean, int SisID, int Tipo, int blID)
    {
        #region Get Cargos por Traficos
        string schema = "";
        ArrayList Arr_Cargos = new ArrayList();
        Bean_Cargos Bean_Cargos = null;
        #region Definir Schema
        if ((Tipo == 17) || (Tipo == 18))
        {
            schema = Pais_Bean.schema_apl;
        }
        else
        {
            schema = Pais_Bean.schema;
        }
        #endregion

        if (SisID == 3)
        {
            #region Cargar Costos Sistema Terrestre
            MySqlConnection con_Terrestre;
            MySqlCommand com_Terrestre;
            MySqlDataReader reader_Terrestre;
            try
            {
                con_Terrestre = DB.OpenTerrestreConnection();
                com_Terrestre = new MySqlCommand();
                com_Terrestre.Connection = con_Terrestre;
                com_Terrestre.CommandType = CommandType.Text;
                if ((Tipo == 5) || (Tipo == 6) || (Tipo == 7))
                {
                    #region Express-Consolidado-Local
                    com_Terrestre.CommandText = "select ChargeID, SBLID, ItemID, ServiceID, Currency, (Value+ OverSold), InvoiceID, 'Import', DocType, " +
                    "InterChargeType, Local, InterCompanyID, InterGroupID, InterProviderType, Local, PrepaidCollect " +
                    "from ChargeItems " +
                    "where SBLID=" + blID + " and InvoiceID=0 and Expired=0 and InterChargeType=1 and InterProviderType=4 and PrepaidCollect=2; ";
                    #endregion
                }
                reader_Terrestre = com_Terrestre.ExecuteReader();
                while (reader_Terrestre.Read())
                {
                    Bean_Cargos = new Bean_Cargos();
                    Bean_Cargos.Cargo_ID = int.Parse(reader_Terrestre.GetValue(0).ToString());
                    Bean_Cargos.Cargo_BLID = int.Parse(reader_Terrestre.GetValue(1).ToString());
                    Bean_Cargos.Cargo_Rub_ID = int.Parse(reader_Terrestre.GetValue(2).ToString());
                    Bean_Cargos.Cargo_Servicio_ID = int.Parse(reader_Terrestre.GetValue(3).ToString());
                    Bean_Cargos.Cargo_Moneda_Simbolo = reader_Terrestre.GetValue(4).ToString();
                    Bean_Cargos.Cargo_Moneda_ID = Utility.TraducirMonedaStr(Bean_Cargos.Cargo_Moneda_Simbolo);
                    Bean_Cargos.Cargo_Monto = double.Parse(reader_Terrestre.GetValue(5).ToString());
                    Bean_Cargos.Factura_ID = int.Parse(reader_Terrestre.GetValue(6).ToString());
                    Bean_Cargos.Cargo_Tipo_BL = "";
                    Bean_Cargos.Tipo_Documento = int.Parse(reader_Terrestre.GetValue(8).ToString());
                    Bean_Cargos.Tipo_Cargo = int.Parse(reader_Terrestre.GetValue(9).ToString());
                    Bean_Cargos.Tipo_Cobro = int.Parse(reader_Terrestre.GetValue(10).ToString());
                    Bean_Cargos.ID_Intercompany = int.Parse(reader_Terrestre.GetValue(11).ToString());
                    Bean_Cargos.ID_Grupo = int.Parse(reader_Terrestre.GetValue(12).ToString());
                    Bean_Cargos.ID_Tipo_Persona = int.Parse(reader_Terrestre.GetValue(13).ToString());
                    Bean_Cargos.ID_Local_Internacional = int.Parse(reader_Terrestre.GetValue(14).ToString());
                    Bean_Cargos.ID_prepaid_collect = int.Parse(reader_Terrestre.GetValue(15).ToString());
                    Arr_Cargos.Add(Bean_Cargos);
                }
                DB.CloseMySQLObj(reader_Terrestre, com_Terrestre, con_Terrestre);
            }
            catch (Exception e)
            {
                log4net ErrLog = new log4net();
                ErrLog.ErrorLog(e.Message);
                return null;
            }
            #endregion
        }
        return Arr_Cargos;
        #endregion
    }

}
