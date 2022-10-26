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

        //bool Validar(Amigo a, out ObservableCollection<string> errores)
        //{


        //    errores = Errores;
        //    errores.Clear();
        //    var numeroExiste = AmigosControlador.ListaAmigos.Any(amigo => amigo.Telefono == a.Telefono);
        //    var correoExiste = AmigosControlador.ListaAmigos.Any(amigo => amigo.CorreoElectronico == a.CorreoElectronico);

        //    if (string.IsNullOrWhiteSpace(a.Nombre))
        //        errores.Add("El nombre es obligatorio");
        //    if (string.IsNullOrWhiteSpace(a.CorreoElectronico))
        //        errores.Add("El correo electrónico es obligatorio");
        //    if (string.IsNullOrWhiteSpace(a.Telefono))
        //        errores.Add("El telefono es obligatorio");
        //    if  (string.IsNullOrEmpty(a.FechaNacimiento.ToString()))
        //        errores.Add("La fecha de nacimiento es obligatoria");
        //    if (a.FechaNacimiento > DateTime.Now)
        //        errores.Add("La fecha de nacimiento no puede ser mayor a la fecha actual");
        //    if (numeroExiste)
        //        errores.Add("Ese numero de telefono ya existe");
        //    if (correoExiste)
        //        errores.Add("Ese correo electronico ya existe");
        //    if (a.Telefono!=null && a.Telefono.Length > 10)
        //        errores.Add("El tamaño maximo del numero telefono es de 10 digitos");

        //    PropertyChange();
        //    return errores.Count == 0;
        //}

        void CambiarVista(string Vista)
        {
            this.Vista = Vista;

            if (Vista == "agregar")
            {
                Amigo = new Amigo();
            }


            PropertyChange();
        }


        void DetallesAmigo(Amigo amigo)
        {
            if (amigo != null)
            {
                this.Amigo = amigo;
                CambiarVista("detalles");
            }
        }

        public void Agregar(Amigo a)
        {

            if (AmigosControlador.Validar(a, out List<string> errores))
            {

                foreach (var item in errores)
                {
                    Error += $"{item}\n";
                }
                AmigosControlador.Crear(a);
                CambiarVista("amigos");
            }
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
                foreach (var item in errores)
                {
                    Error += $"{item}\n";
                }
                AmigosControlador.Editar(a);
                CambiarVista("amigos");
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
