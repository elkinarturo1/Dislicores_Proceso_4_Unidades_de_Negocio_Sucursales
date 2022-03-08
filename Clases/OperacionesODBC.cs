using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidades_de_Negocio_Sucursales.Clases
{
    public static class OperacionesODBC
    {

        public static void DSP_ACTUALIZAR_UN_CLIENTE(string p_RowIdTercero, string p_idSucursal, string p_UN)
        {

            OdbcConnection conexionOracle = new OdbcConnection(Properties.Settings.Default.strConexionODBC);
            OdbcCommand ComandoODBC = new OdbcCommand();

            ComandoODBC.Connection = conexionOracle;
            ComandoODBC.CommandType = CommandType.StoredProcedure;
            ComandoODBC.CommandText = "{call DSP_ACTUALIZAR_UN_CLIENTE(?,?,?)}";
            ComandoODBC.CommandTimeout = 0;

            //ComandoODBC.CommandText = "DSP_GEO_ACT_DIRECCION";

            OdbcParameter P1 = new OdbcParameter("p_RowIdTercero", ParameterDirection.Input);
            OdbcParameter P2 = new OdbcParameter("p_idSucursal", ParameterDirection.Input);
            OdbcParameter P3 = new OdbcParameter("p_UN", ParameterDirection.Input);

            try
            {

                P1.Value = p_RowIdTercero;
                P2.Value = p_idSucursal;
                P3.Value = p_UN;

                ComandoODBC.Parameters.Add(P1);
                ComandoODBC.Parameters.Add(P2);
                ComandoODBC.Parameters.Add(P3);

                ComandoODBC.Connection.Open();
                ComandoODBC.ExecuteNonQuery();

                //objLog.detalle = "Actualizacion Direccion OK";

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ComandoODBC.Parameters.Clear();
                ComandoODBC.Connection.Close();
            }

        }

    }
}
