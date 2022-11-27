using AgendaDigital.Views;
using AgendaDigitaldDesconectado.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaDigitaldDesconectado.CRUD
{
    public class AgendaCRUD
    {

        agendaContext contenedor = new();

        public IEnumerable<Amigos> ObtenerTodosLosAmigos()
        {
            return contenedor.Amigos.OrderBy(amigo => amigo.Nombre);
        }
        public void Crear(Amigos amigo)
        {
          
            
            contenedor.Amigos.Add(amigo);
            // save the changes to the database
            contenedor.SaveChanges();
            
        }

        

        public void Eliminar(Amigos amigo)
        {
            contenedor.Amigos.Remove(amigo);
            contenedor.SaveChanges();
        }
        public void Actualizar(Amigos amigo)
        {
            contenedor.Amigos.Update(amigo);
            contenedor.SaveChanges();
            //Preguntar
            contenedor.Entry(amigo).Reload();
        }

        public bool Validar(Amigos a, out List<string> errores)
        {
            errores = new();
            if (string.IsNullOrEmpty(a.Nombre))
                errores.Add("El nombre es obligatorio");
            if (string.IsNullOrWhiteSpace(a.CorreoElectronico))
                errores.Add("El correo electrónico es obligatorio");
            if (string.IsNullOrWhiteSpace(a.Telefono))
                errores.Add("El telefono es obligatorio");
            if (string.IsNullOrWhiteSpace(a.Fechanacimiento.ToString()))
                errores.Add("La fecha de nacimiento es obligatoria");
            if (a.FechaNacimiento > DateTime.Now)
                errores.Add("La fecha de nacimiento no puede ser mayor a la fecha actual");
            if (contenedor.Amigos.Any(amigo => amigo.Telefono == a.Telefono) && contenedor.Amigos.Any(amigo => amigo.Id != a.Id))
                errores.Add("Ese numero de telefono ya existe");
            if (contenedor.Amigos.Any(amigo => amigo.CorreoElectronico == a.CorreoElectronico) && contenedor.Amigos.Any(amigo => amigo.Id != a.Id))
                errores.Add("Ese correo electronico ya existe");
            if (a.Telefono != null && a.Telefono.Length > 10)
                errores.Add("El tamaño maximo del numero telefono es de 10 digitos");

            //Make a regex to validate an email

   


            return errores.Count == 0;
        }

        public Amigos? ObtenerAmigo(Amigos amigo)
        {
            Amigos? existe = contenedor.Amigos.Find(amigo.Id);
            if (existe != null)
            {
                return existe;

            }
            return null;
        }
    }
}
