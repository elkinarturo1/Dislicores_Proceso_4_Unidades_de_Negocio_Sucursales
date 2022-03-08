using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Unidades_de_Negocio_Sucursales.Modelos;

namespace Unidades_de_Negocio_Sucursales.Clases
{
    public class ModuloConsumosWS
    {


        public void importarDatos_UNOEE(int idConector, string nombreConector, string strXML, ref LogModel objLog)
        {

            string rutaPlanos = @"C:\inetpub\wwwroot\GTIntegrationVTEX\Planos\";
            string strResultado = "";

            try
            {

                //Valida la estructura del XML convirtiendolo a DataSet
                DataSet dsDatos = new DataSet();
                TextReader txtReader1 = new StringReader(strXML);
                XmlReader reader1 = new XmlTextReader(txtReader1);
                dsDatos.ReadXml(reader1);

                objLog.xml_GT = strXML;

                if (dsDatos.Tables.Count > 0)
                {
                    if (dsDatos.Tables[0].Rows.Count > 0)
                    {

                        WSGT.wsGenerarPlano objGT = new WSGT.wsGenerarPlano();
                        objLog.planoGT = objGT.GenerarPlanoXML(idConector, nombreConector, 2, "2", "gt", "gt", dsDatos, ref rutaPlanos, ref strResultado);
                        objLog.resultadoGT = strResultado;

                        if (objLog.resultadoGT == "Se genero el plano correctamente")
                        {
                            ModuloConsumosWS objUNOEE = new ModuloConsumosWS();
                            objUNOEE.cargarConector(ref objLog);
                        }
                        else
                        {
                            objLog.bitError = true;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                objLog.resultadoGT = "Ha ocurrido una excepcion al generar el plano " + ex.Message;
            }

        }


        public void cargarConector(ref LogModel objLog)
        {

            string strXML = "";
            short resultSiesa = 123;
            DataSet ds = new DataSet();
            string strMensaje = "";

            try
            {

                strXML += "<Importar>" + Environment.NewLine;
                strXML += $"<NombreConexion>{Properties.Settings.Default.ConexionUNOEE}</NombreConexion>" + Environment.NewLine;
                strXML += $"<IdCia>{Properties.Settings.Default.CiaUNOEE}</IdCia>" + Environment.NewLine;
                strXML += $"<Usuario>{Properties.Settings.Default.UsuarioUNOEE}</Usuario>" + Environment.NewLine;
                strXML += $"<Clave>{Properties.Settings.Default.ClaveUNOEE}</Clave>" + Environment.NewLine;
                //strXML += $"<Usuario>juan.puerta</Usuario>" + Environment.NewLine;
                //strXML += $"<Clave>Jessi25</Clave>" + Environment.NewLine;
                strXML += objLog.planoGT;
                strXML += "</Importar>" + Environment.NewLine;

                objLog.xml_UNOEE = strXML;

                WSUNOEE.WSUNOEE objUNOEE = new WSUNOEE.WSUNOEE();

                objUNOEE.Timeout = 18000000;
                //objUNOEE.Url = "http://172.16.100.24:9091/WSUNOEE_EUCLIDES_VTEX/WSUNOEE.asmx";
                ds = objUNOEE.ImportarXML(strXML, ref resultSiesa);

         objLog.resultado_UNOEE = resultSiesa;

                switch (resultSiesa)
                {
                    case 0:
                        strMensaje = "Importacion Exitosa ";                       
                        break;
                    case 1:
                        strMensaje = "Error : 1 - Error de datos al cargar la informacion a siesa a Siesa " + ds.GetXml() + Environment.NewLine + strXML;
                        break;
                    case 2:
                        strMensaje = "Error : 2 - El impodatos no envio algun parametro " + ds.GetXml() + Environment.NewLine + strXML;
                        break;
                    case 3:
                        strMensaje = "Error :  3 - El usuario o la contraseña que ingreso no son validos " + ds.GetXml() + Environment.NewLine + strXML;
                        break;
                    case 4:
                        strMensaje = "Error : 4 - La version del impodatos no se corresponde con la version del ERP o el impodatos esta en una maquina que no tiene cliente Siesa " + ds.GetXml() + Environment.NewLine + strXML;
                        break;
                    case 5:
                        strMensaje = "Error :  5 - La base de datos no existe o están ingresándole un parámetro erróneo a la hora de especificar la conexión. " + ds.GetXml() + Environment.NewLine + strXML;
                        break;
                    case 6:
                        strMensaje = "Error : 6 - El archivo que se está especificando en la ruta de los parámetros del .BAT no existe " + ds.GetXml() + Environment.NewLine + strXML;
                        break;
                    case 7:
                        strMensaje = "Error :  7 - El archivo que se está especificando en la ruta de los parámetros del .BAT no es valido " + ds.GetXml() + Environment.NewLine + strXML;
                        break;
                    case 8:
                        strMensaje = "Error : 8 - Hay un problema con la tabla en la base de datos donde se ingresaran los archivos " + ds.GetXml() + Environment.NewLine + strXML;
                        break;
                    case 9:
                        strMensaje = "Error :  9 - La compañía que se ingresó en los parámetros del .BAT no es valida " + ds.GetXml() + Environment.NewLine + strXML;
                        break;
                    case 10:
                        strMensaje = "Error : 10 - Error desconocido " + ds.GetXml() + Environment.NewLine + strXML;
                        break;
                    case 99:
                        strMensaje = "Error : 99 - Error de tipo diferente a los anteriores, normalmente de permisos a nivel del ERP " + ds.GetXml() + Environment.NewLine + strXML;
                        break;
                }

                //if (objLog.resultado_UNOEE != 0)
                //{
                //    objLog.bitError = true;
                //    objLog.detalle = strMensaje;
                //    ModuloSQL.sp_Log_Guardar(objLog);
                //}

                objLog.detalle = strMensaje;

            }
            catch (Exception ex)
            {
                objLog.bitError = true;
                objLog.detalle = ex.Message;
            }

        }



        public void generarPlano(int idConector, string nombreConector, string strXML, ref LogModel objLog)
        {

            string rutaPlanos = @"C:\inetpub\wwwroot\GTIntegrationVTEX\Planos\";
            string strResultado = "";

            try
            {

                //Valida la estructura del XML convirtiendolo a DataSet
                DataSet dsDatos = new DataSet();
                TextReader txtReader1 = new StringReader(strXML);
                XmlReader reader1 = new XmlTextReader(txtReader1);
                dsDatos.ReadXml(reader1);

                objLog.xml_GT = strXML;

                if (dsDatos.Tables.Count > 0)
                {
                    if (dsDatos.Tables[0].Rows.Count > 0)
                    {

                        WSGT.wsGenerarPlano objGT = new WSGT.wsGenerarPlano();
                        objLog.planoGT = objGT.GenerarPlanoXML(idConector, nombreConector, 2, "2", "gt", "gt", dsDatos, ref rutaPlanos, ref strResultado);
                        objLog.resultadoGT = strResultado;

                        //if (objLog.resultadoGT == "Se genero el plano correctamente")
                        //{
                        //    ModuloConsumosWS objUNOEE = new ModuloConsumosWS();
                        //    objUNOEE.cargarConector(ref objLog);
                        //}

                    }
                }

            }
            catch (Exception ex)
            {
                objLog.resultadoGT = "Ha ocurrido una excepcion al generar el plano " + ex.Message;
            }

        }



    }
}
