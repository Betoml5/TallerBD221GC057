using Ferretera.Models;
using GalaSoft.MvvmLight.Command;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ferretera.ViewModels
{
    public class ProductosViewModel: INotifyPropertyChanged
    {
        public Productos Productos { get; set; } = new Productos();
        
        public ICommand ProductosPorSeccionCommand { get; set; }   

        public event PropertyChangedEventHandler? PropertyChanged;

        public ProductosViewModel()
        {
            ProductosPorSeccionCommand = new RelayCommand<Seccion>(ProductosPorSeccion);
        }

        private void ProductosPorSeccion(Seccion s)
        {
            Productos.ProductosPorSeccion(s);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}
