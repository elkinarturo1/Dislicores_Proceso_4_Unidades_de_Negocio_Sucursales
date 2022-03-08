using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidades_de_Negocio_Sucursales.Modelos
{
    public class LogModel
    {
        public bool bitError { get; set; }
        public string idTercero { get; set; }
        public string idSucursal { get; set; }
        public string proceso { get; set; }
        public int rowIdTercero { get; set; }
        public string direccion { get; set; }
        public string codigoPostal { get; set; }
        public string zonaLogistica { get; set; }
        public string diasEntrega { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string ciudad { get; set; }
        public string idCanalVend { get; set; }
        public string estratoNvlSocioEconomico { get; set; }
        public string barrio { get; set; }
        public string comunaLocalidad { get; set; }
        public string Zona1 { get; set; }
        public string Zona2 { get; set; }
        public string Zona3 { get; set; }
        public string Zona4 { get; set; }
        public string Zona5 { get; set; }
        public string datosEnviadosSitiData { get; set; }
        public string resultadoSitiData { get; set; }
        public string xml_GT { get; set; }
        public string planoGT { get; set; }
        public string resultadoGT { get; set; }
        public string xml_UNOEE { get; set; }
        public short resultado_UNOEE { get; set; }
        public string detalle { get; set; }

        public string Crit_Subsegmento { get; set; }

        public string Crit_CCD { get; set; }

        public string Crit_Vendedor { get; set; }

        public string procesoUN { get; set; }

    }
}
