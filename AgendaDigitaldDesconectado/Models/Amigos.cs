using System;
using System.Collections.Generic;

namespace AgendaDigitaldDesconectado.Models
{
    public partial class Amigos
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime? Fechanacimiento { get; set; }
        public string Telefono { get; set; } = null!;
        public string CorreoElectronico { get; set; } = null!;

        public override string ToString()
        {
            return Nombre.ToString();
        }
    }
}
