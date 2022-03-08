using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidades_de_Negocio_Sucursales.Modelos;

namespace Unidades_de_Negocio_Sucursales.Clases
{
    public class Proceso_Importar_Criterios
    {

        public void ejecutarProceso(ref LogModel objLog)
        {

            string strXMLGT = "";

            try
            {

                //===========================================================================================
                //Enviar Criterios Clasificacion                         

                objLog.proceso = "Criterios";

                strXMLGT += "<MyDataSet>" + Environment.NewLine;

                //============================================================================
                if (objLog.zonaLogistica != "")
                {
                    strXMLGT += "<Criterio>" + Environment.NewLine;
                    strXMLGT += "<F207_ID_PLAN_CRITERIOS>CCD</F207_ID_PLAN_CRITERIOS>" + Environment.NewLine;
                    strXMLGT += "<F207_ID_TERCERO>" + objLog.idTercero + "</F207_ID_TERCERO>" + Environment.NewLine;
                    strXMLGT += "<F207_ID_SUCURSAL>" + objLog.idSucursal + "</F207_ID_SUCURSAL>" + Environment.NewLine;
                    strXMLGT += "<F207_ID_CRITERIO_MAYOR>" + objLog.zonaLogistica + "</F207_ID_CRITERIO_MAYOR>" + Environment.NewLine;
                    strXMLGT += "</Criterio>" + Environment.NewLine;
                }
                else
                {
                    objLog.bitError = true;
                    objLog.detalle += "Error campo zonalogistica vacio - " + Environment.NewLine;
                }
                //============================================================================
                               
                strXMLGT += "</MyDataSet>" + Environment.NewLine;


                ModuloConsumosWS objWS = new ModuloConsumosWS();
                objWS.importarDatos_UNOEE(107184, "Criterios Clientes", strXMLGT, ref objLog);

            }
            catch (Exception ex)
            {
                objLog.bitError = true;
                objLog.detalle = ex.Message;
                //ModuloSQL.sp_Log_Guardar(objLog);
            }
            //===========================================================================================

        }

    }
}
