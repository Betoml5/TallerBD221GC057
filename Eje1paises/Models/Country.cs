using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eje1paises.Models
{
    public class Country
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public string Continent { get; set; }
        public string Region { get; set; }
        public float SurfaceArea { get; set; }
        public short  IndepYear { get; set; }
        public int Population { get; set; }
        public float LifeExpectancy { get; set; }
        public string LocalName { get; set; }
        public string GovernmentForm { get; set; }

        public string HeadOfState { get; set; }

        public override string ToString()
        {
            return Name;
        }

       
    }
}
