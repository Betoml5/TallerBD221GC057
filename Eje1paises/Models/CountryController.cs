using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eje1paises.Models
{
    public class CountryController
    {
        public List<Country> CountryList { get; set; } = new List<Country>();

        MySqlConnection connection;
        MySqlCommand sqlCommand;
        MySqlDataReader reader;
        
        public CountryController()
        {

            // 1 Establecer los pasos de la conexion
            connection = new MySqlConnection("server=localhost;user=root;database=world;password=root");
            //connection.ConnectionString = "server=localhost;user=root;database=world;password=root";
            // 2 Abrir la conexion
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            // 3 Ejecutar el comando, antes definir el comando

            sqlCommand = new MySqlCommand();
            sqlCommand.Connection = connection;
            sqlCommand.CommandText = "SELECT * FROM country order by name";
            reader = sqlCommand.ExecuteReader();

            // 4 Leer los datos
            while (reader.Read())
            {
                Country c = new Country()
                {
                    Code = (string)reader["code"],
                    Name = (string)reader["Name"],
                    Continent = (string)reader["Continent"],
                    Region = (string)reader["region"],
                    SurfaceArea = (float)reader["surfacearea"],
                    IndepYear = Convert.IsDBNull(reader["indepyear"]) ? (short) 0 : (short)reader["indepyear"],
                    Population = (int)reader["population"],
                    LifeExpectancy = Convert.IsDBNull(reader["LifeExpectancy"]) ? 0 : (float)reader["LifeExpectancy"],
                    LocalName = (string)reader["localname"],
                    GovernmentForm = (string)reader["GovernmentForm"],
                    HeadOfState = Convert.IsDBNull(reader["headofstate"]) ? "No HAY" : (string)reader["headofstate"],
                };

                CountryList.Add(c);
            }

            reader.Close();

        }

        ~CountryController()
        {

            // Cerrar la conexion

            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
            

        }
    }
}
