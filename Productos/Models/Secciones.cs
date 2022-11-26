using System;
using System.Collections.Generic;

namespace Productos.Models
{
    public partial class Secciones
    {
        public Secciones()
        {
            Productos = new HashSet<Productos>();
        }

        public uint Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Productos> Productos { get; set; }

        public override string ToString()
        {
            return Nombre.ToString();
        }
    }
}
