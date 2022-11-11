using Pintores.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pintores.ViewModels
{
    public class PintoresViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Models.Pintores> Pintores { get; set; } = new ObservableCollection<Models.Pintores>();
        PinacotecaContext contenedor = new();


        public PintoresViewModel()
        {
            var pintores = contenedor.Pintores.OrderBy(pintor => pintor.Nombre);
            foreach (var pintor in pintores)
            {
                Pintores.Add(pintor);
            }

        }


        void PropertyChange(string? prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
