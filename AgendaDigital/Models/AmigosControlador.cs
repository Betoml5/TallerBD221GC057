using MySql.Data.MySqlClient;
using Org.BouncyCastle.Math.Field;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AgendaDigital.Models
{
    public class AmigosControlador
    {
        public ObservableCollection<Amigo> ListaAmigos { get; set; } = new ObservableCollection<Amigo>();

        MySqlConnection conexion { get; set; }
        MySqlCommand comandoSQL { get; set; }
        MySqlDataReader lector { get; set; }

        public AmigosControlador()
        {
            conexion = new MySqlConnection("server=localhost;user=root;database=agenda;password=root");

            if (conexion.State != System.Data.ConnectionState.Open)
            {
                conexion.Open();
            }

            comandoSQL = new MySqlCommand();
            comandoSQL.Connection = conexion;
            comandoSQL.CommandText = "SELECT * FROM AMIGOS";
            lector = comandoSQL.ExecuteReader();

            while (lector.Read())
            {
                Amigo a = new()
                {
                    Id = (int)lector["ID"],
                    Nombre = (string)lector["Nombre"],
                    FechaNacimiento = (DateTime)lector["fechanacimiento"],
                    Telefono = (string)lector["telefono"],
                    CorreoElectronico = (string)lector["correoelectronico"]
                };

                ListaAmigos.Add(a);
            }

            lector.Close();
            
        }

        public void Crear(Amigo a)
        {
            //comandoSQL.CommandText = $"INSERT INTO AMIGOS (Nombre, FechaNacimiento, Telefono, CorreoElectronico) VALUES ({a.Nombre}, {a.FechaNacimiento:YYYY-MM-DD}, {a.Telefono}, {a.CorreoElectronico})";
            comandoSQL.CommandText = string.Format("INSERT INTO AMIGOS (Nombre, FechaNacimiento, Telefono, CorreoElectronico) VALUES ('{0}', '{1}', '{2}', '{3}')", a.Nombre, a.FechaNacimiento.ToString("yyyy-MM-dd"), a.Telefono, a.CorreoElectronico);
            var rowsAffected = comandoSQL.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                ListaAmigos.Add(a);
            }
     
        }

        public Amigo BuscarPorId(Amigo a)
        {
            Amigo amigo = new();
            comandoSQL.CommandText = $"SELECT * FROM AMIGOS WHERE Id = {a.Id}";
            lector = comandoSQL.ExecuteReader();

            while (lector.Read())
            {
                amigo.Id = (int)lector["ID"];
                amigo.Nombre = (string)lector["NOMBRE"];
                amigo.FechaNacimiento = (DateTime)lector["FECHANACIMIENTO"];
                amigo.CorreoElectronico = (string)lector["CORREOELECTRONICO"];
                amigo.Telefono = (string)lector["TELEFONO"];
            }

            return amigo;
            
        }

        public int Borrar(Amigo a)
        {
            comandoSQL.CommandText = $"DELETE FROM AMIGOS WHERE ID = {a.Id}";
            var rowsAffected = comandoSQL.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                ListaAmigos.Remove(a);
                return rowsAffected;
            }

            return rowsAffected;
            
        }

        public void Editar(Amigo a)
        {

            if (a != null)
            {
                comandoSQL.CommandText = $"UPDATE AMIGOS SET NOMBRE = '{a.Nombre}', TELEFONO = '{a.Telefono}', CORREOELECTRONICO = '{a.CorreoElectronico}', FECHANACIMIENTO = '{a.FechaNacimiento.ToString("yyyy-MM-dd")}' WHERE ID = {a.Id}";

                if (comandoSQL.ExecuteNonQuery() != 0)
                {
                    var temporal = ListaAmigos.FirstOrDefault(amigo => amigo.Id == a.Id);
                    if (temporal != null)
                    {
                        temporal.Nombre = a.Nombre;
                        temporal.CorreoElectronico = a.CorreoElectronico;
                        temporal.Telefono = a.Telefono;
                        temporal.FechaNacimiento = a.FechaNacimiento;
                    }
                }
            }
            
         
        }

        public  bool Validar(Amigo a, out List<string> errores)
        {
            errores = new();
            if (string.IsNullOrWhiteSpace(a.Nombre))
                    errores.Add("El nombre es obligatorio");
                if (string.IsNullOrWhiteSpace(a.CorreoElectronico))
                    errores.Add("El correo electrónico es obligatorio");
                if (string.IsNullOrWhiteSpace(a.Telefono))
                    errores.Add("El telefono es obligatorio");
                if (string.IsNullOrWhiteSpace(a.FechaNacimiento.ToString()))
                    errores.Add("La fecha de nacimiento es obligatoria");
                if (a.FechaNacimiento > DateTime.Now)
                    errores.Add("La fecha de nacimiento no puede ser mayor a la fecha actual");
                if (ListaAmigos.Any(amigo => amigo.Telefono == a.Telefono) && ListaAmigos.Any(amigo => amigo.Id != a.Id))
                    errores.Add("Ese numero de telefono ya existe");
                if (ListaAmigos.Any(amigo => amigo.CorreoElectronico == a.CorreoElectronico) && ListaAmigos.Any(amigo => amigo.Id != a.Id))
                    errores.Add("Ese correo electronico ya existe");
                if (a.Telefono != null && a.Telefono.Length > 10)
                    errores.Add("El tamaño maximo del numero telefono es de 10 digitos");
           
            return errores.Count == 0;
        }
    }
}
