using System;
using System.Collections.Generic;

namespace Pintores.Models
{
    public partial class Pintores
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Pais { get; set; } = null!;
        public string Ciudad { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public DateTime? FechaFallecimiento { get; set; }
    }
}
