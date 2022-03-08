using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidades_de_Negocio_Sucursales.Modelos;

namespace Unidades_de_Negocio_Sucursales.Clases
{
    public static class OperacionesSQL
    {


        public static DataSet sp_Unidades_Sucursales_Nielsen_D_Select(string f200_id, string F201_ID_SUCURSAL)
        {
            SqlConnection conexionSQL = new SqlConnection(Properties.Settings.Default.strConexionSQL);
            SqlCommand comandoSQL = new SqlCommand();
            SqlDataAdapter adaptadorSQL = new SqlDataAdapter();
            DataSet ds = new DataSet();

            comandoSQL.Connection = conexionSQL;
            comandoSQL.CommandType = CommandType.StoredProcedure;
            comandoSQL.CommandText = "sp_Unidades_Sucursales_Nielsen_D_Select";

            comandoSQL.Parameters.AddWithValue("@f200_id", f200_id);
            comandoSQL.Parameters.AddWithValue("@F201_ID_SUCURSAL", F201_ID_SUCURSAL);

            try
            {

                adaptadorSQL.SelectCommand = comandoSQL;
                adaptadorSQL.Fill(ds);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                comandoSQL.Parameters.Clear();
                comandoSQL.Connection.Close();
            }

            return ds;

        }


        public static DataSet sp_Unidades_Sucursales_Select(string f200_id, string F201_ID_SUCURSAL)
        {
            SqlConnection conexionSQL = new SqlConnection(Properties.Settings.Default.strConexionSQL);
            SqlCommand comandoSQL = new SqlCommand();
            SqlDataAdapter adaptadorSQL = new SqlDataAdapter();
            DataSet ds = new DataSet();

            comandoSQL.Connection = conexionSQL;
            comandoSQL.CommandType = CommandType.StoredProcedure;
            comandoSQL.CommandText = "sp_Unidades_Sucursales_Select";

            comandoSQL.Parameters.AddWithValue("@f200_id", f200_id);
            comandoSQL.Parameters.AddWithValue("@F201_ID_SUCURSAL", F201_ID_SUCURSAL);

            try
            {

                adaptadorSQL.SelectCommand = comandoSQL;
                adaptadorSQL.Fill(ds);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                comandoSQL.Parameters.Clear();
                comandoSQL.Connection.Close();
            }

            return ds;

        }

        public static void sp_Cliente_Relanzar(string idTercero, string idSucursal)
        {
            SqlConnection conexionSQL = new SqlConnection(Properties.Settings.Default.strConexionSQL);
            SqlCommand comandoSQL = new SqlCommand();

            comandoSQL.Connection = conexionSQL;
            comandoSQL.CommandType = CommandType.StoredProcedure;
            comandoSQL.CommandText = "sp_Cliente_Relanzar";

            comandoSQL.Parameters.AddWithValue("@idTercero", idTercero);
            comandoSQL.Parameters.AddWithValue("@idSucursal", idSucursal);

            try
            {
                comandoSQL.Connection.Open();
                comandoSQL.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                comandoSQL.Parameters.Clear();
                comandoSQL.Connection.Close();
            }

        }

        public static void sp_UN_Control_Clientes_Guardar(string idTercero, string idSucursal, string Crit_Subsegmento, string Crit_CCD, string Crit_Vendedor, string proceso = "-1")
        {
            SqlConnection conexionSQL = new SqlConnection(Properties.Settings.Default.strConexionSQL);
            SqlCommand comandoSQL = new SqlCommand();

            comandoSQL.Connection = conexionSQL;
            comandoSQL.CommandType = CommandType.StoredProcedure;
            comandoSQL.CommandText = "sp_UN_Control_Clientes_Guardar";

            comandoSQL.Parameters.AddWithValue("@idTercero", idTercero);
            comandoSQL.Parameters.AddWithValue("@idSucursal", idSucursal);
            comandoSQL.Parameters.AddWithValue("@Crit_Subsegmento", Crit_Subsegmento);
            comandoSQL.Parameters.AddWithValue("@Crit_CCD", Crit_CCD);
            comandoSQL.Parameters.AddWithValue("@Crit_Vendedor", Crit_Vendedor);
            comandoSQL.Parameters.AddWithValue("@proceso", proceso);

            try
            {
                comandoSQL.Connection.Open();
                comandoSQL.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                comandoSQL.Connection.Close();
            }

        }

        public static void sp_Log_Guardar(LogModel modelo)
        {
            SqlConnection conexionSQL = new SqlConnection(Properties.Settings.Default.strConexionSQL);
            SqlCommand comandoSQL = new SqlCommand();

            comandoSQL.Connection = conexionSQL;
            comandoSQL.CommandType = CommandType.StoredProcedure;
            comandoSQL.CommandText = "sp_Log_Guardar";

            comandoSQL.Parameters.AddWithValue("@idTercero", modelo.idTercero);
            comandoSQL.Parameters.AddWithValue("@idSucursal", modelo.idSucursal);
            comandoSQL.Parameters.AddWithValue("@proceso", modelo.proceso);
            comandoSQL.Parameters.AddWithValue("@rowIdTercero", modelo.rowIdTercero);
            comandoSQL.Parameters.AddWithValue("@direccion", modelo.direccion);
            comandoSQL.Parameters.AddWithValue("@codigoPostal", modelo.codigoPostal);
            comandoSQL.Parameters.AddWithValue("@latitud", modelo.latitud);
            comandoSQL.Parameters.AddWithValue("@longitud", modelo.longitud);
            comandoSQL.Parameters.AddWithValue("@estrato", modelo.estratoNvlSocioEconomico);
            comandoSQL.Parameters.AddWithValue("@barrio", modelo.barrio);
            comandoSQL.Parameters.AddWithValue("@comunaLocalidad", modelo.comunaLocalidad);
            comandoSQL.Parameters.AddWithValue("@Zona1", modelo.Zona1);
            comandoSQL.Parameters.AddWithValue("@Zona2", modelo.Zona2);
            comandoSQL.Parameters.AddWithValue("@Zona3", modelo.Zona3);
            comandoSQL.Parameters.AddWithValue("@Zona4", modelo.Zona4);
            comandoSQL.Parameters.AddWithValue("@resultadoSitiData", modelo.resultadoSitiData);
            comandoSQL.Parameters.AddWithValue("@xml_GT", modelo.xml_GT);
            comandoSQL.Parameters.AddWithValue("@resultado_GT", modelo.resultadoGT);
            comandoSQL.Parameters.AddWithValue("@xml_UNOEE", modelo.xml_UNOEE);
            comandoSQL.Parameters.AddWithValue("@resultado_UNOEE", modelo.resultado_UNOEE);
            comandoSQL.Parameters.AddWithValue("@detalle", modelo.detalle);


            try
            {
                comandoSQL.Connection.Open();
                comandoSQL.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                comandoSQL.Connection.Close();
            }

        }


    }
}
