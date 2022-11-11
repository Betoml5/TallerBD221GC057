using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Productos.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace Productos.ViewModels
{
    public class ProductosViewModel : INotifyPropertyChanged
    {
    
        public ObservableCollection<Models.Productos> ListaProductos { get; set; } = new ObservableCollection<Productos.Models.Productos>();
        public ObservableCollection<Secciones> ListaSecciones { get; set; } = new ObservableCollection<Secciones>();
        ferreteraContext contenedor = new ferreteraContext();
        public ulong TotalProductos { get; set; }
        public decimal PrecioPromedio { get; set; }

        
        public ICommand ProductosPorSeccionCommand { get; set; }

        public ProductosViewModel()
        {
            //Data context es el contenedor de mi base de datos;
            //Primero instanciar un objeto de esa clase para poder acceder a las entidades (Tablas de mysql)

            ProductosPorSeccionCommand = new RelayCommand<Secciones>(ProductosPorSeccion);

            var productos = contenedor.Productos.OrderBy(producto => producto.Nombre);
            var secciones = contenedor.Secciones.OrderBy(seccion => seccion.Nombre);
            ListaProductos.Clear();
            ListaSecciones.Clear();

            foreach (var producto in productos)
            {
                ListaProductos.Add(producto);
            }

            foreach (var seccion in secciones)
            {
                ListaSecciones.Add(seccion);
            }

            TotalProductos = (ulong)contenedor.Productos.Count();
            PrecioPromedio = contenedor.Productos.Average(producto => producto.Precio);
            PropertyChange();

        }

        void ProductosPorSeccion(Secciones seccion)
        {
            ListaProductos.Clear();
            var productos = contenedor.Productos.Where(producto => producto.IdSeccion == seccion.Id);
            foreach (var producto in productos)
            {
                ListaProductos.Add(producto);
            }
            TotalProductos = 0; PrecioPromedio = 0;
            TotalProductos = (ulong)ListaProductos.Count();
            PrecioPromedio = (TotalProductos == 0) ? 0 :  ListaProductos.Average(producto => producto.Precio);
        }


        void PropertyChange(string? prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}   
