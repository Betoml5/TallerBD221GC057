using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferretera.Models
{
    public class Producto
    {
        public uint Sku { get; set; }
        public string Marca { get; set; }
        public string NombreProducto { get; set; }
        //public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Seccion { get; set; }

       
    }

}
