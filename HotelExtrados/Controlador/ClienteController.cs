using Dapper;
using HotelExtrados.Modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelExtrados.Controlador
{
    public class ClienteController
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["MyDbConnection"].ToString();
       
        //APP PUNTO 3
        public int agregarCliente(Cliente cliente)
        {

            string query = "insert into Clientes (Dni,Nombre,Apellido,Email) values (@Dni,@Nombre,@Apellido,@Email)";
            using (IDbConnection db = new SqlConnection(cadenaConexion))
            {
                db.Open();

                var clienteNuevo = db.Execute(query, new { Dni = cliente.Dni, Nombre = cliente.Nombre, Apellido = cliente.Apellido, Email = cliente.Email });

                return clienteNuevo;

            }
        }
    }
}
