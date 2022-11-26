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
    }
}
