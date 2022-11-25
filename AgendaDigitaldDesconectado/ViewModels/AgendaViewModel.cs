using AgendaDigitaldDesconectado.CRUD;
using AgendaDigitaldDesconectado.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AgendaDigitaldDesconectado.ViewModels
{
    public class AgendaViewModel : INotifyPropertyChanged
    {
        public string Vista { get; set; } = "amigos"; 
        public ObservableCollection<Amigos> Amigos { get; set; } = new ObservableCollection<Amigos>();
        public Amigos Amigo { get; set; }
        
        AgendaCRUD CRUD = new();

        public int PosicionSeleccionada { get; set; }

        public ICommand CrearCommand { get; set; }
        public ICommand CambiarVistaCommand { get; set; } 
        public ICommand DetallesAmigosCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand ActualizarCommand { get; set; }

        public AgendaViewModel()
        {

            CrearCommand = new RelayCommand<Amigos>(Crear);
            CambiarVistaCommand = new RelayCommand<string>(CambiarVista);
            DetallesAmigosCommand = new RelayCommand<Amigos>(DetallesAmigo);
            EliminarCommand = new RelayCommand<Amigos>(Eliminar);
            ActualizarCommand = new RelayCommand<Amigos>(Actualizar);
            CancelarCommand = new RelayCommand(Cancelar);

            //Cargar informacion de la tabla
            IEnumerable<Amigos> amigos = CRUD.ObtenerTodosLosAmigos();

            foreach (Amigos amigo in amigos)
            {
                Amigos.Add(amigo);
            }
        }
        public void Crear(Amigos amigo)
        {
            CRUD.Crear(amigo);
            CambiarVista("amigos");
            ActualizarBD();
        }

        public void Actualizar(Amigos amigo)
        {
            CRUD.Actualizar(amigo);
            
            ActualizarBD();
        }

        public void Eliminar(Amigos amigo)
        {
            CRUD.Eliminar(amigo);
            ActualizarBD();
        }
        void DetallesAmigo(Amigos amigo)
        {
            if (amigo != null)
            {
                this.Amigo = amigo;
                CambiarVista("detalles");
            }
        }

        void Cancelar()
        {
            this.Amigo = null;
            CambiarVista("amigos");
        }

        public void ActualizarBD()
        {
            Amigos.Clear();
            foreach (var item in CRUD.ObtenerTodosLosAmigos())
            {
                Amigos.Add(item);
            }

            PropertyChange();
        }


        void CambiarVista(string vista)
        {
            this.Vista = vista;

            if (vista == "crear")
            {
                Amigo = new Amigos();
            }

            if (Vista == "detalles")
            {
                var clon = new Amigos()
                {
                    Id = Amigo.Id,
                    Nombre = Amigo.Nombre,
                    Fechanacimiento = Amigo.Fechanacimiento,
                    Telefono = Amigo.Telefono,
                    CorreoElectronico = Amigo.CorreoElectronico
                };

                Amigo = clon;
            }

            PropertyChange();
        }

        void PropertyChange(string? prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
