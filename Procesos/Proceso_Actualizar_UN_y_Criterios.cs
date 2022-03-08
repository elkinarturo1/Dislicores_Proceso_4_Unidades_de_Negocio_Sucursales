using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidades_de_Negocio_Sucursales.Clases;
using System.IO;
using System.Data;
using Unidades_de_Negocio_Sucursales.Modelos;
using System.Xml;

namespace Unidades_de_Negocio_Sucursales.Procesos
{
    public class Proceso_Actualizar_UN_y_Criterios
    {
        public void ejecutarProceso(string idTercero = "-1", string idSucursal = "-1")
        {
            DataSet ds = new DataSet();
            bool bitError = false;
            LogModel objLog = new LogModel();
            string rowIdTercero = "-1";
            string Nielsen_D = "";
            string subsegmento = "";
            string equivalencia_UN = "";
            string equivalencia_criterioUN_CCD = "";


            //Pasos
            //Consultar clientes con sus criterios incluyendo el Nielsen D
            //Cuando el Nielsen D  es store o digital se coloca la unidad de negocio correspondiente a store y digital		
            //Actualizar unidad de negocio a nivel del cliente en la pestaña ventas
            //Actualizar el plan CCD del cliente con el correspondiente criterio


            //===================================================================================
            //Consultar Datos a actualizar
            try
            {
                ds = OperacionesSQL.sp_Unidades_Sucursales_Select(idTercero, idSucursal);
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
                                subsegmento = registro["idCriterio_SubSegmento"].ToString();                                
                                Nielsen_D = registro["cod_equivalencia_Nielsen_D"].ToString();
                                rowIdTercero = registro["f201_rowid_tercero"].ToString();
                                rowIdTercero = registro["f201_rowid_tercero"].ToString();
                                idTercero = registro["f200_id"].ToString();
                                idSucursal = registro["f201_id_sucursal"].ToString();


                                //Si Nielsen D es Store o Digital se coloca la unidad de negocio
                                //correspondiente a Store o Digital
                                //700 - Store
                                //701 - Digital
                                //900 - Administrativos
                                if (Nielsen_D == "700")//700 - Store
                                {
                                    equivalencia_UN = "9070";
                                    equivalencia_criterioUN_CCD = "003";
                                }
                                else if (Nielsen_D == "701")//701 - Digital Rappy
                                {
                                    equivalencia_UN = "9071";
                                    equivalencia_criterioUN_CCD = "005";
                                }
                                else if (Nielsen_D == "900")//900 - Administrativos
                                {
                                    equivalencia_UN = "9099";
                                    equivalencia_criterioUN_CCD = "006";
                                }
                                else
                                {
                                    equivalencia_UN = registro["cod_equivalencia_UN"].ToString();
                                    equivalencia_criterioUN_CCD = registro["cod_equivalencia_CCD"].ToString();
                                }
                                //=====================================================================
                                

                                //=====================================================================
                                //Actualizo la unidad de negocio en la pestala de ventas
                                try
                                {
                                    OperacionesODBC.DSP_ACTUALIZAR_UN_CLIENTE(rowIdTercero, idSucursal, equivalencia_UN);
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                //=====================================================================



                                //=====================================================================
                                //Envio el criterio de clasificacion a Siesa

                                if ((Nielsen_D != "700") || (Nielsen_D != "701") || (Nielsen_D != "900"))
                                {
                                    string strXMLGT = "";

                                    strXMLGT += "<MyDataSet>" + Environment.NewLine;

                                    //Criterio Canal TI1
                                    strXMLGT += "<Criterio>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_PLAN_CRITERIOS>" + registro["idPlan_Canal"].ToString() + "</F207_ID_PLAN_CRITERIOS>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_TERCERO>" + registro["f200_id"].ToString() + "</F207_ID_TERCERO>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_SUCURSAL>" + registro["f201_id_sucursal"].ToString() + "</F207_ID_SUCURSAL>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_CRITERIO_MAYOR>" + registro["idCriterio_Canal"].ToString() + "</F207_ID_CRITERIO_MAYOR>" + Environment.NewLine;
                                    strXMLGT += "</Criterio>" + Environment.NewLine;


                                    //Criterio SubCanal TI2
                                    strXMLGT += "<Criterio>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_PLAN_CRITERIOS>" + registro["idPlan_SubCanal"].ToString() + "</F207_ID_PLAN_CRITERIOS>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_TERCERO>" + registro["f200_id"].ToString() + "</F207_ID_TERCERO>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_SUCURSAL>" + registro["f201_id_sucursal"].ToString() + "</F207_ID_SUCURSAL>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_CRITERIO_MAYOR>" + registro["idCriterio_SubCanal"].ToString() + "</F207_ID_CRITERIO_MAYOR>" + Environment.NewLine;
                                    strXMLGT += "</Criterio>" + Environment.NewLine;


                                    //Criterio SubCanal TI3
                                    strXMLGT += "<Criterio>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_PLAN_CRITERIOS>" + registro["idPlan_Segmento"].ToString() + "</F207_ID_PLAN_CRITERIOS>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_TERCERO>" + registro["f200_id"].ToString() + "</F207_ID_TERCERO>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_SUCURSAL>" + registro["f201_id_sucursal"].ToString() + "</F207_ID_SUCURSAL>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_CRITERIO_MAYOR>" + registro["idCriterio_Segmento"].ToString() + "</F207_ID_CRITERIO_MAYOR>" + Environment.NewLine;
                                    strXMLGT += "</Criterio>" + Environment.NewLine;


                                    //Criterio Canal Diageo 007
                                    strXMLGT += "<Criterio>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_PLAN_CRITERIOS>" + registro["idPlan_Canal_Diageo"].ToString() + "</F207_ID_PLAN_CRITERIOS>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_TERCERO>" + registro["f200_id"].ToString() + "</F207_ID_TERCERO>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_SUCURSAL>" + registro["f201_id_sucursal"].ToString() + "</F207_ID_SUCURSAL>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_CRITERIO_MAYOR>" + registro["idCriterio_Canal_Diageo"].ToString() + "</F207_ID_CRITERIO_MAYOR>" + Environment.NewLine;
                                    strXMLGT += "</Criterio>" + Environment.NewLine;

                                    //Criterio SubCanal Diageo 006
                                    strXMLGT += "<Criterio>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_PLAN_CRITERIOS>" + registro["idPlan_SubCanal_Diageo"].ToString() + "</F207_ID_PLAN_CRITERIOS>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_TERCERO>" + registro["f200_id"].ToString() + "</F207_ID_TERCERO>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_SUCURSAL>" + registro["f201_id_sucursal"].ToString() + "</F207_ID_SUCURSAL>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_CRITERIO_MAYOR>" + registro["idCriterio_SubCanal_Diageo"].ToString() + "</F207_ID_CRITERIO_MAYOR>" + Environment.NewLine;
                                    strXMLGT += "</Criterio>" + Environment.NewLine;

                                    //Criterio Segmento Diageo 012
                                    strXMLGT += "<Criterio>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_PLAN_CRITERIOS>" + registro["idPlan_Segmento_Diageo"].ToString() + "</F207_ID_PLAN_CRITERIOS>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_TERCERO>" + registro["f200_id"].ToString() + "</F207_ID_TERCERO>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_SUCURSAL>" + registro["f201_id_sucursal"].ToString() + "</F207_ID_SUCURSAL>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_CRITERIO_MAYOR>" + registro["idCriterio_Segmento_Diageo"].ToString() + "</F207_ID_CRITERIO_MAYOR>" + Environment.NewLine;
                                    strXMLGT += "</Criterio>" + Environment.NewLine;

                                    //Criterio SubSegmento 013
                                    strXMLGT += "<Criterio>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_PLAN_CRITERIOS>" + registro["idPlan_SubSegmento_Diageo"].ToString() + "</F207_ID_PLAN_CRITERIOS>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_TERCERO>" + registro["f200_id"].ToString() + "</F207_ID_TERCERO>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_SUCURSAL>" + registro["f201_id_sucursal"].ToString() + "</F207_ID_SUCURSAL>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_CRITERIO_MAYOR>" + registro["idCriterio_SubSegmento_Diageo"].ToString() + "</F207_ID_CRITERIO_MAYOR>" + Environment.NewLine;
                                    strXMLGT += "</Criterio>" + Environment.NewLine;


                                    //Criterio Canal Cliente CCD
                                    strXMLGT += "<Criterio>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_PLAN_CRITERIOS>CCD</F207_ID_PLAN_CRITERIOS>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_TERCERO>" + registro["f200_id"].ToString() + "</F207_ID_TERCERO>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_SUCURSAL>" + registro["f201_id_sucursal"].ToString() + "</F207_ID_SUCURSAL>" + Environment.NewLine;
                                    strXMLGT += "<F207_ID_CRITERIO_MAYOR>" + equivalencia_criterioUN_CCD + "</F207_ID_CRITERIO_MAYOR>" + Environment.NewLine;
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
                                            OperacionesSQL.sp_UN_Control_Clientes_Guardar(idTercero, idSucursal, subsegmento,equivalencia_criterioUN_CCD, Nielsen_D, "3");
                                        }

                                    }
                                    else
                                    {
                                        objLog.bitError = true;
                                    }
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
