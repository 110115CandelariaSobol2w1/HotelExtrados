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

        public int CambiarEstadoHabitacion(int Nro_habitacion)
        {
            string query = "update Habitaciones set IdEstado = CASE IdEstado" +
                " when 1 then 2 " +
                "when 2 then 2 " +
                "when 3 then 2 " +
                "when 4 then 2 " +
                "else null " +
                "end " +
                "where Nro_habitacion = @Nro_Habitacion";

            using (IDbConnection db = new SqlConnection(cadenaConexion))
            {
                db.Open();

                var estadoNuevo = db.Execute(query, new { Nro_habitacion = Nro_habitacion});

                return estadoNuevo;

            }
        }

        public int estadoReserva(int Nro_habitacion)
        {

            string query = "select Estado from Reserva where Nro_habitacion = @Nro_habitacion";
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var estado = conexion.QueryFirstOrDefault<int>(query, new { Nro_habitacion = Nro_habitacion});
                return estado;
            }
        }





    }
}
