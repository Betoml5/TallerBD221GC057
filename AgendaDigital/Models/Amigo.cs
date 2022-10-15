using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AgendaDigital.Models
{
    public class Amigo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }

        public string CorreoElectronico { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }

   

}
