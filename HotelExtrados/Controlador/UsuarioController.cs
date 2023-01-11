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
    public class UsuarioController
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["MyDbConnection"].ToString();


        //LOGIN. 
        public int Login(string username, string password)
        {
            using (IDbConnection db = new SqlConnection(cadenaConexion))
            {
                db.Open();
                var parameters = new DynamicParameters();
                parameters.Add("username", username);
                parameters.Add("password", password);

                string query = "SELECT * FROM Usuarios WHERE Usuario = @Username AND Password = @Password";

                var user = db.QueryFirstOrDefault<Usuario>(query, parameters);


                if(user != null && user.idRol == 1)
                {
                    return 1;
    
                }
                else if (user != null && user.idRol == 2)
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
         
            }
        }
    }
}
