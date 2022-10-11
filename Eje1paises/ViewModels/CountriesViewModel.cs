using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eje1paises.Models;

namespace Eje1paises.ViewModels
{
    public class CountriesViewModel
    {
        public CountryController Countries {get;set;} = new CountryController();
        
    }
}
