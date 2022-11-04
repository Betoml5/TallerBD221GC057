using System;
using System.Collections.Generic;

namespace Productos.Models
{
    public partial class Productos
    {
        public uint Sku { get; set; }
        public string Marca { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public uint IdSeccion { get; set; }

        public virtual Secciones IdSeccionNavigation { get; set; } = null!;
    }
}
