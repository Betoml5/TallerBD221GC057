using MySql.Data.MySqlClient;
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

        public int Crear(Amigo a)
        {
            comandoSQL.CommandText = $"INSERT INTO AMIGOS (Nombre, FechaNacimiento, Telefono, CorreoElectronico) VALUES ({a.Nombre}, {a.FechaNacimiento.ToString("YYYY-MM-DD")}, {a.Telefono}, {a.CorreoElectronico})";
            var rowsAffected = comandoSQL.ExecuteNonQuery();
            return rowsAffected;  
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
            comandoSQL.CommandText = $"DELETE * FROM AMIGOS WHERE ID = {a.Id}";
            var rowsAffected = comandoSQL.ExecuteNonQuery();

            return rowsAffected;
            
        }

        public int Editar(Amigo a)
        {
            comandoSQL.CommandText = $"UPDATE AMIGOS SET NOMBRE = {a.Nombre} TELEFONO = {a.Telefono} CORREOELECTRONICO = {a.CorreoElectronico} WHERE ID = {a.Id}";
            var rowsAffected = comandoSQL.ExecuteNonQuery();

            return rowsAffected;
        }
    }
}
