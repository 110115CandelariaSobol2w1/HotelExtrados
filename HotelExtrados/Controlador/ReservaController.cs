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
    public class ReservaController
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["MyDbConnection"].ToString();

        //APP PUNTO 4 
        public int agregarReserva(Reserva reserva)
        {

            string query = "insert into Reserva (Nro_habitacion,Dni_cliente,Check_in,Check_out,Estado) values (@Nro_habitacion,@Dni_cliente,@Check_in, @Check_out,@Estado)";
            using (IDbConnection db = new SqlConnection(cadenaConexion))
            {
                db.Open();

                var reservaNueva = db.Execute(query, new { Nro_habitacion = reserva.Nro_habitacion, Dni_cliente = reserva.Dni_cliente,Check_in = reserva.Check_in, Check_out = reserva.Check_out, Estado = reserva.Estado });

                return reservaNueva;

            }
        }
    }
}
