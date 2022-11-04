using AgendaDigital.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AgendaDigital.ViewModels
{
    public class AmigoViewModel : INotifyPropertyChanged
    {

        public AmigosControlador AmigosControlador { get; set; } = new AmigosControlador();
        public List<string> Errores { get; set; } = new List<string>();
        public int PosicionSeleccionada { get; set; }
        public Amigo Amigo { get; set; }
        public string Error { get; set; } = "";

        public string Mensaje { get; set; } = "";
        public string Vista { get; set; } = "amigos";

        int posicionInicial;

        public ICommand CambiarVistaCommand { get; set; }
        public ICommand AgregarAmigoCommand { get; set; }
        public ICommand EliminarAmigoCommand { get; set; }

        public ICommand EditarAmigoCommand { get; set; }
        public ICommand DetallesAmigoCommand { get; set; }
        public ICommand CancelarCommand { get; set; }
        

        public AmigoViewModel()
        {
            CambiarVistaCommand = new RelayCommand<string>(CambiarVista);
            AgregarAmigoCommand = new RelayCommand<Amigo>(Agregar);
            EliminarAmigoCommand = new RelayCommand<Amigo>(Eliminar);
            EditarAmigoCommand = new RelayCommand<Amigo>(Editar);
            DetallesAmigoCommand = new RelayCommand<Amigo>(DetallesAmigo);
            CancelarCommand = new  RelayCommand(Cancelar);
        }

        void CambiarVista(string Vista)
        {
            Error = "";
            this.Vista = Vista;

            if (Vista == "agregar")
            {
                Amigo = new Amigo();
            }

            if (Vista == "detalles")
            {
                var clon = new Amigo()
                {
                    Id = Amigo.Id,
                    Nombre = Amigo.Nombre,
                    FechaNacimiento = Amigo.FechaNacimiento,
                    Telefono = Amigo.Telefono,
                    CorreoElectronico = Amigo.CorreoElectronico
                };

                Amigo = clon;
            }


            PropertyChange();
        }


        void DetallesAmigo(Amigo amigo)
        {
            if (amigo != null)
            {
                Error = "";
                this.Amigo = amigo;
                CambiarVista("detalles");
            }
        }

        public void Agregar(Amigo a)
        {
            Error = "";
            if (AmigosControlador.Validar(a, out List<string> errores))
            {

                
                AmigosControlador.Crear(a);
                CambiarVista("amigos");
            }

            foreach (var item in errores)
            {
                Error += $"{item}\n";
            }

            PropertyChange();
        }
        public void BuscarPorId(Amigo a)
        {
            Amigo = AmigosControlador.BuscarPorId(a);
            if (Amigo != null)
            {
                Mensaje = "¡Encontrado!";
            }
            else
            {
                Mensaje = "¡No se encontro el amigo!";
            }
        }
        public void Eliminar(Amigo a)
        {
            if (a != null)
            {
                int filasAfectadas = AmigosControlador.Borrar(a);

                if (filasAfectadas > 0)
                {
                    Mensaje = "¡Eliminado Satisfactoriamente!";
                }
                
            }
        }
        public void Editar(Amigo a)
        {
            if (AmigosControlador.Validar(a, out List<string> errores))
            {
              
                AmigosControlador.Editar(a);
                CambiarVista("amigos");
            }

            foreach (var item in errores)
            {
               
                Error += $"{item}\n";
            }

            PropertyChange();
        }

            void Cancelar()
            {
                this.Amigo = null;
                CambiarVista("amigos");
                
            }

            void PropertyChange(string? prop = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
