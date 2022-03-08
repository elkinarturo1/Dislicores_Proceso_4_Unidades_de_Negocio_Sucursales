using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Unidades_de_Negocio_Sucursales.Clases;
using Unidades_de_Negocio_Sucursales.Modelos;

namespace Unidades_de_Negocio_Sucursales.Procesos
{
    public class Proceso_Actualizar_Vendedor_Nielsen_D
    {

        public void ejecutarProceso(string idTercero = "-1", string idSucursal = "-1")
        {

            DataSet ds = new DataSet();
            bool bitError = false;
            LogModel objLog = new LogModel();
            //string rowIdTercero = "-1";
            //string Nielsen_D = "";
            //string equivalencia_UN = "";
            //string equivalencia_criterioUN_CCD = "";
            string equivalencia_criterio_Nielse_D = "";


            //Pasos
            //Actualizar el plan T14 con el correspondiente criterio

            //===================================================================================
            //Consultar Datos a actualizar
            try
            {
                ds = OperacionesSQL.sp_Unidades_Sucursales_Nielsen_D_Select(idTercero, idSucursal);
            }
            catch (Exception ex)
            {
                bitError = true;
                objLog.bitError = true;
                objLog.idTercero = idTercero;
                objLog.idSucursal = idSucursal;
                objLog.resultadoSitiData = "Error al consultar sp_Clientes_Select " + ex.Message;
                OperacionesSQL.sp_Log_Guardar(objLog);
            }
            //===================================================================================


            //===================================================================================
            //Armar XML
            if (bitError == false)
            {
                try
                {

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow registro in ds.Tables[0].Rows)
                            {

                                //============================================================================
                                //Capturos la variables de acuerdo a la logica
                                idTercero = registro["f200_nit"].ToString();
                                idSucursal = registro["f201_id_sucursal"].ToString();
                                //Criterio de Nielsen D T14:
                                equivalencia_criterio_Nielse_D = registro["cod_equivalencia_Nielsen_D"].ToString();
                                objLog.Crit_Vendedor = equivalencia_criterio_Nielse_D;                                
                                //=====================================================================



                                //=====================================================================
                                //Envio el criterio de clasificacion a Siesa

                                string strXMLGT = "";

                                strXMLGT += "<MyDataSet>" + Environment.NewLine;

                                //Criterio Nilsen D
                                strXMLGT += "<Criterio>" + Environment.NewLine;
                                strXMLGT += "<F207_ID_PLAN_CRITERIOS>TI4</F207_ID_PLAN_CRITERIOS>" + Environment.NewLine;
                                strXMLGT += "<F207_ID_TERCERO>" + registro["f200_id"].ToString() + "</F207_ID_TERCERO>" + Environment.NewLine;
                                strXMLGT += "<F207_ID_SUCURSAL>" + registro["f201_id_sucursal"].ToString() + "</F207_ID_SUCURSAL>" + Environment.NewLine;
                                strXMLGT += "<F207_ID_CRITERIO_MAYOR>" + equivalencia_criterio_Nielse_D + "</F207_ID_CRITERIO_MAYOR>" + Environment.NewLine;
                                strXMLGT += "</Criterio>" + Environment.NewLine;

                                strXMLGT += "</MyDataSet>" + Environment.NewLine;

                                string rutaPlanos = @"C:\inetpub\wwwroot\GTIntegrationVTEX\Planos\";
                                string strResultado = "";

                                //Valida la estructura del XML convirtiendolo a DataSet
                                DataSet dsDatos = new DataSet();
                                TextReader txtReader1 = new StringReader(strXMLGT);
                                XmlReader reader1 = new XmlTextReader(txtReader1);
                                dsDatos.ReadXml(reader1);

                                WSGT.wsGenerarPlano objGT = new WSGT.wsGenerarPlano();
                                objLog.planoGT = objGT.GenerarPlanoXML(119653, "Servi_Criterios_Clientes", 2, "2", "gt", "gt", dsDatos, ref rutaPlanos, ref strResultado);
                                objLog.resultadoGT = strResultado;

                                if (objLog.resultadoGT == "Se genero el plano correctamente")
                                {
                                    ModuloConsumosWS objUNOEE = new ModuloConsumosWS();
                                    objUNOEE.cargarConector(ref objLog);                                    

                                    if (objLog.resultado_UNOEE == 0)
                                    {
                                        OperacionesSQL.sp_UN_Control_Clientes_Guardar(idTercero, idSucursal, "", "", equivalencia_criterio_Nielse_D, "3");
                                    }

                                }
                                else
                                {
                                    objLog.bitError = true;
                                }

                                //=====================================================================

                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    bitError = true;
                    objLog.bitError = true;
                    objLog.idTercero = idTercero;
                    objLog.idSucursal = idSucursal;
                    objLog.resultadoSitiData = "Error al consultar sp_Clientes_Select " + ex.Message;
                    OperacionesSQL.sp_Log_Guardar(objLog);
                }

            }
            //===================================================================================


        }

    }
}
