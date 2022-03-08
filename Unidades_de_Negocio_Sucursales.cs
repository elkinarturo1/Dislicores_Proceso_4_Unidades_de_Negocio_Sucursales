using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unidades_de_Negocio_Sucursales.Clases;
using Unidades_de_Negocio_Sucursales.Modelos;
using Unidades_de_Negocio_Sucursales.Procesos;

namespace Unidades_de_Negocio_Sucursales
{
    public partial class Unidades_de_Negocio_Sucursales : Form
    {
        public Unidades_de_Negocio_Sucursales()
        {
            InitializeComponent();
        }

        private void Unidades_de_Negocio_Sucursales_Load(object sender, EventArgs e)
        {
            Proceso_Actualizar_Vendedor_Nielsen_D objActualizarNielsenD = new Proceso_Actualizar_Vendedor_Nielsen_D();
            Proceso_Actualizar_UN_y_Criterios objActualizarUN = new Proceso_Actualizar_UN_y_Criterios();

            string idTercero = "-1";
            string idSucursal = "-1";

            idTercero = "11407555";
            idSucursal = "001";

            try
            {
                objActualizarNielsenD.ejecutarProceso(idTercero, idSucursal);
                objActualizarUN.ejecutarProceso(idTercero, idSucursal);
            }
            catch (Exception ex)
            {
                string strError = ex.Message;
            }         

            this.Close();

        }
    }
}
