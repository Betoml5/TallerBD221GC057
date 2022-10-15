using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferretera.Models
{
    public class Productos
    {
        public ObservableCollection<Producto> ListaProductos { get; set; } = new ObservableCollection<Producto>();
        public List<Seccion> ListaSecciones { get; set; } = new List<Seccion>();

        public decimal PromedioPrecio { get; set; } = 0;
        public decimal PromedioSeccion { get; set; } = 0;


        MySqlConnection conexion { get; set; }
         MySqlCommand commandoSQL { get; set; }
         MySqlDataReader lector { get; set; }
        public Productos()
        {
            //Establecer los pasos de la conexion
            conexion = new MySqlConnection("server=localhost;user=root;database=ferretera;password=root");

            //Abrimos la conexion
            if (conexion.State != System.Data.ConnectionState.Open)
            {
                conexion.Open();
            }

            // Ejecutamos el comando

            commandoSQL = new MySqlCommand();
            commandoSQL.Connection = conexion;
            commandoSQL.CommandText = "SELECT SKU, MARCA, P.NOMBRE AS NOMBREPRODUCTO, PRECIO, S.NOMBRE AS SECCION FROM PRODUCTOS AS P JOIN SECCIONES AS S ON P.IdSeccion = S.ID";
            lector = commandoSQL.ExecuteReader();


            //Leer datos
            while (lector.Read())
            {
                Producto p = new Producto()
                {
                    Sku = (uint)lector["sku"],
                    Marca = Convert.IsDBNull(lector["marca"]) ? "Sin marca" : (string)lector["marca"],
                    NombreProducto = (string)lector["nombreproducto"],
                    //Descripcion = (string)lector["descripcion"],
                    Precio = (decimal)lector["precio"],
                    Seccion = (string)lector["seccion"]

                };

                ListaProductos.Add(p);
            }


            lector.Close();

            
            commandoSQL.CommandText = "SELECT * FROM SECCIONES";
            lector = commandoSQL.ExecuteReader();

            while (lector.Read())
            {
                Seccion s = new Seccion()
                {
                    Id = (uint)lector["id"],
                    Nombre = (string)lector["nombre"]

                };

                ListaSecciones.Add(s);
            }
            lector.Close();

            commandoSQL.CommandText = "SELECT AVG(PRECIO) as PromedioPrecio FROM PRODUCTOS;";
            lector = commandoSQL.ExecuteReader();
            while (lector.Read())
            {
                PromedioPrecio = (decimal)lector["PromedioPrecio"];
            }

            lector.Close();
            
        }

        ~Productos()
        {
            // Cerrar la conexion
            if (conexion.State == System.Data.ConnectionState.Open)
                conexion.Close();
        }

        public void ProductosPorSeccion(Seccion s)
        {
            if (s == null)
            {
                return;
            }
            conexion = new MySqlConnection("server=localhost;user=root;database=ferretera;password=root");

            if (conexion.State != System.Data.ConnectionState.Open)
            {
                conexion.Open();
            }
            
            commandoSQL.CommandText = $"SELECT SKU, MARCA, P.NOMBRE AS NOMBREPRODUCTO, PRECIO, S.NOMBRE AS SECCION FROM PRODUCTOS AS P JOIN SECCIONES AS S ON P.IdSeccion = S.ID WHERE S.ID = {s.Id}";
            lector = commandoSQL.ExecuteReader();
            var clonProductos = ListaProductos;
            ListaProductos.Clear();

            while (lector.Read())
            {
                Producto p = new Producto()
                {
                    Sku = (uint)lector["sku"],
                    Marca = Convert.IsDBNull(lector["marca"]) ? "Sin marca" : (string)lector["marca"],
                    NombreProducto = (string)lector["nombreproducto"],
                    //Descripcion = (string)lector["descripcion"],
                    Precio = (decimal)lector["precio"],
                    Seccion = (string)lector["seccion"]
                };
                ListaProductos.Add(p);
            }

            lector.Close();
            if (ListaProductos.Count == 0)
            {
                ListaProductos = clonProductos;
            }

            commandoSQL.CommandText = $"SELECT AVG(PRECIO) AS PROMEDIOPRECIO FROM PRODUCTOS WHERE PRODUCTOS.IDSECCION = {s.Id}";
            lector = commandoSQL.ExecuteReader();


            while (lector.Read())
            {
                PromedioSeccion = Convert.IsDBNull(lector["PROMEDIOPRECIO"]) ? 0 : (decimal)lector["PROMEDIOPRECIO"];
            }

            lector.Close();
           

           

        }
    }
}
