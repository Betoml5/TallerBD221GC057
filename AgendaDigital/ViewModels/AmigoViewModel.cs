using AgendaDigital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AgendaDigital.ViewModels
{
    public class AmigoViewModel
    {

        public AmigosControlador AmigosControlador { get; set; }  = new AmigosControlador();

        public Amigo Amigo { get; set; }
        public string Error { get; set; } = "";
        public string Mensaje { get; set; } = "";

        public void Crear(Amigo a)
        {

            if (string.IsNullOrWhiteSpace(a.Nombre)
                || string.IsNullOrWhiteSpace(a.Telefono)
                || string.IsNullOrWhiteSpace(a.CorreoElectronico))  
            {
                Error = "Faltan datos por rellenar";
                return;
            }

            AmigosControlador.Crear(a);
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
            int filasAfectadas = AmigosControlador.Borrar(a);
            if (filasAfectadas > 0)
            {
                Mensaje = "¡Eliminado Satisfactoriamente!";
            }
            else
            {
                Mensaje = "¡Hubo un error al intentar eliminar el amigo";
            }
        }

        public void Editar(Amigo a)
        {
            if (string.IsNullOrWhiteSpace(a.Nombre) || 
                string.IsNullOrWhiteSpace(a.Telefono) ||
                string.IsNullOrWhiteSpace(a.CorreoElectronico))
            {
                Error = "¡Hay campos vacios!";
                return;
            }
            int filasAfectadas = AmigosControlador.Editar(a);
            if (filasAfectadas > 0)
            {
                Mensaje = "¡Amigo editado correctamente!";
            }
            else
            {
                Error = "¡Hubi un error a la hora de editar el amigo!";
            }
        }
        
    }
}
