using Dapper;
using HotelExtrados.Modelo;
using HotelExtrados.DTO;
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
    public class HabitacionController
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["MyDbConnection"].ToString();

        //APP PUNTO 1
        public IEnumerable<Habitacion> obtenerHabitacionesNormales()
        {

            string query = "select nro_habitacion, cant_camas, cochera, precio, television, desayuno, IdEstado" +
                " from habitaciones where idTipo = 1 ";


            using (IDbConnection db = new SqlConnection(cadenaConexion))
            {
                db.Open();
                var habitacionesComunes = db.Query<Habitacion>(query).ToList();


                return habitacionesComunes;
            }


        }

        public IEnumerable<Habitacion> obtenerHabitacionesVip()
        {

            string query = "select Nro_Habitacion, Cant_camas,Cochera,Precio,Servicio,Hidromasaje, IdEstado " +
                "from Habitaciones where IdTipo = 2";


            using (IDbConnection db = new SqlConnection(cadenaConexion))
            {
                db.Open();
                var habitacionesVip = db.Query<Habitacion>(query).ToList();


                return habitacionesVip;
            }


        }
        public DateTime obtenerCheckOut(int Nro_habitacion)
        {
            string query = "select Check_out from Reserva where Nro_habitacion = @Nro_habitacion";

            using (IDbConnection db = new SqlConnection(cadenaConexion))
            {
                db.Open();
                return db.QueryFirstOrDefault<DateTime>(query, new { Nro_habitacion = Nro_habitacion});
            }
        }
       
        //APP PUNTO 2
        public IEnumerable<Habitacion> obtenerHabitacionesVipDesocupadas()
        {

            string query = "select Nro_Habitacion, Cant_camas,Cochera,Precio,Servicio,Hidromasaje " +
                "from Habitaciones where IdTipo = 2 and IdEstado = 1";


            using (IDbConnection db = new SqlConnection(cadenaConexion))
            {
                db.Open();
                var habitacionesVip = db.Query<Habitacion>(query).ToList();


                return habitacionesVip;
            }


        }

        public IEnumerable<Habitacion> obtenerHabitacionesNormalesDesocupadas()
        {

            string query = "select nro_habitacion, cant_camas, cochera, precio, television, desayuno " +
                "from habitaciones where idTipo = 1 and IdEstado = 1";


            using (IDbConnection db = new SqlConnection(cadenaConexion))
            {
                db.Open();
                var habitacionesVip = db.Query<Habitacion>(query).ToList();


                return habitacionesVip;
            }


        }

        //APP PUNTO 3 - ClienteController

        //APP PUNTO 4 - ReservaController

        //APP PUNTO 5
        public IEnumerable<habitacionesDTO> obtenerHabitacionesDesocupadasLimpieza()
        {
            string query = "select Nro_habitacion, h.IdEstado, e.Descripcion " +
                "from habitaciones h join Estado_Habitacion e on h.IdEstado = e.IdEstado " +
                "where h.IdEstado = 1 or h.IdEstado =3";

            using (IDbConnection db = new SqlConnection(cadenaConexion))
            {
                db.Open();

                var estadoHabitacion = db.Query<habitacionesDTO>(query).ToList();

                return estadoHabitacion;

            }
        }

        public int CambiarEstado(Habitacion nuevoEstado)
        {
            string query = "update Habitaciones set IdEstado = CASE IdEstado" +
                " when 1 then 3 " +
                "when 3 then 1 " +
                "else null " +
                "end " +
                "where Nro_habitacion = @Nro_Habitacion";

            using (IDbConnection db = new SqlConnection(cadenaConexion))
            {
                db.Open();

                var estadoNuevo = db.Execute(query, new { Nro_habitacion = nuevoEstado.Nro_Habitacion });

                return estadoNuevo;

            }
        }


        //-----------------------------------------------------------------------------------------------//


        //ADMIN PUNTO 1
        public int agregarHabitacion(Habitacion habitacion)
        {

            string query = "insert into Habitaciones (Nro_habitacion,IdTipo,Cant_camas,Cochera,Precio,Television,Desayuno,Servicio,Hidromasaje,IdEstado) values (@Nro_habitacion,@IdTipo,@Cant_camas,@Cochera,@Precio,@Television,@Desayuno,@Servicio,@Hidromasaje,@IdEstado)";
            using (IDbConnection db = new SqlConnection(cadenaConexion))
            {
                db.Open();

                var habitacionNueva = db.Execute(query, new { Nro_habitacion = habitacion.Nro_Habitacion, IdTipo = habitacion.idTipo,Cant_camas = habitacion.cant_camas, Cochera = habitacion.Cochera,Precio = habitacion.precio,Television = habitacion.television, Desayuno = habitacion.Desayuno, Servicio = habitacion.Servicio, Hidromasaje = habitacion.Hidromasaje, IdEstado = habitacion.idEstado});

                return habitacionNueva;

            }
        }


        //VERIFICAMOS QUE NO EXISTA EL NUMERO DE CUARTO QUE VAMOS A REGISTRAR

        public bool verificarHabitacion(int Nro_habitacion)
        {
            using (var connection = new SqlConnection(cadenaConexion))
            {
                string query = "select count(*) from Habitaciones where Nro_habitacion = @Nro_habitacion";


                var count = connection.ExecuteScalar<int>(query, new { Nro_habitacion = Nro_habitacion });

                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        //ADMIN PUNTO 2

        public int estadoALimpieza(Habitacion habitacion)
        {
            string query = "update Habitaciones set IdEstado = CASE IdEstado " +
                "when 1 then 3 " +
                "when 2 then 3 " +
                "when 3 then 3 " +
                "when 4 then 3 " +
                "end " +
                "where Nro_Habitacion = @Nro_Habitacion";

            using (IDbConnection db = new SqlConnection(cadenaConexion))
            {
                db.Open();

                var estadoNuevo = db.Execute(query, new { Nro_habitacion = habitacion.Nro_Habitacion });

                return estadoNuevo;

            }
        }

        public int estadoARenovacion(Habitacion habitacion)
        {
            string query = "update Habitaciones set IdEstado = CASE IdEstado " +
                "when 1 then 4 " +
                "when 2 then 4 " +
                "when 3 then 4 " +
                "when 4 then 4 " +
                "end " +
                "where Nro_Habitacion = @Nro_Habitacion";

            using (IDbConnection db = new SqlConnection(cadenaConexion))
            {
                db.Open();

                var estadoNuevo = db.Execute(query, new { Nro_habitacion = habitacion.Nro_Habitacion });

                return estadoNuevo;

            }
        }

        public int cancelarReserva(Reserva reserva)
        {
            string query = "update Reserva set Estado = CASE Estado " +
                "when 1 then 0 " +
                "when 0 then 0 " +
                "end " +
                "where Nro_Habitacion = @Nro_Habitacion";

            using (IDbConnection db = new SqlConnection(cadenaConexion))
            {
                db.Open();

                var estadoNuevo = db.Execute(query, new { Nro_habitacion = reserva.Nro_habitacion});

                return estadoNuevo;

            }
        }

        public int ObtenerEstadoHabitacion(Habitacion habitacion)
        {
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var dias = conexion.QueryFirstOrDefault<int>("select IdEstado from Habitaciones where Nro_habitacion = @Nro_habitacion", new { id_habitacion = habitacion.Nro_Habitacion});
                return dias;
            }
        }


        //ADMIN PUNTO 3

        public int EstadoHabitacion(Habitacion habitacion)
        {
            string query = "select count(*) from Reserva" +
                " where Nro_habitacion = @Nro_habitacion" +
                " and GETDATE() >= Check_in and GETDATE()<= Check_out";
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                var estadoNuevo = conexion.QueryFirstOrDefault<int>(query, new { Nro_habitacion = habitacion.Nro_Habitacion});
                return estadoNuevo;
            }
        }

        public int LimpiezaADisponible(Habitacion habitacion)
        {

            string query = "UPDATE Habitaciones SET IdEstado = CASE IdEstado " +
                "WHEN '3' THEN '1'  " +
                "WHEN '1' THEN '1'" +
                " WHEN '2' THEN '2'" +
                " WHEN '4' THEN '4'" +
                " END WHERE Nro_habitacion = @Nro_habitacion";
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                return conexion.Execute(query, new { Nro_habitacion = habitacion.Nro_Habitacion});

            }
        }

        public int LimpiezaAOcupado(Habitacion habitacion)
        {

            string query = "UPDATE Habitaciones SET IdEstado = CASE IdEstado " +
                "WHEN '3' THEN '2'  " +
                "WHEN '1' THEN '1'" +
                " WHEN '2' THEN '2'" +
                " WHEN '4' THEN '4'" +
                " END WHERE Nro_habitacion = @Nro_habitacion";
            using (IDbConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                return conexion.Execute(query, new { Nro_habitacion = habitacion.Nro_Habitacion });

            }
        }

        public long obtenerClienteReserva(Habitacion habitacion)
        {
            string query = "select dni as 'cliente' " +
                "from Clientes c join Reserva r on c.Dni = r.Dni_cliente " +
                "where Nro_habitacion = @Nro_habitacion and GETDATE() between Check_in and Check_out";

            using (IDbConnection db = new SqlConnection(cadenaConexion))
            {
                db.Open();
                return db.QueryFirstOrDefault<long>(query, new { Nro_habitacion = habitacion.Nro_Habitacion});
            }
        }


        //ADMIN PUNTO 4
        public IEnumerable<habitacionesDTO> obtenerHabitacionesRenovacion()
        {
            string query = "select Nro_habitacion, h.IdEstado, e.Descripcion " +
               "from habitaciones h join Estado_Habitacion e on h.IdEstado = e.IdEstado " +
               "where h.IdEstado = 4";

            using (IDbConnection db = new SqlConnection(cadenaConexion))
            {
                db.Open();

                var habitacionesRenovacion = db.Query<habitacionesDTO>(query).ToList();

                return habitacionesRenovacion;

            }
        }

        public int RenovacionADisponible(Habitacion habitacion)
        {

            string query = "update Habitaciones" +
                " set IdEstado = CASE IdEstado " +
                "when 4 then 1" +
                " else null " +
                "end " +
                "where Nro_Habitacion = @Nro_habitacion";

            using (IDbConnection db = new SqlConnection(cadenaConexion))
            {
                db.Open();

                var estadoNuevo = db.Execute(query, new { Nro_habitacion = habitacion.Nro_Habitacion });

                return estadoNuevo;

            }
        }


        //Verificamos que la habitacion no se encuentre ocupada entre el check in y checkout

        public bool verificarHabitacionFecha(int Nro_habitacion,DateTime Check_in, DateTime Check_out)
        {
            string query = "SELECT count(*) from Habitaciones " +
                "where Nro_habitacion = @Nro_habitacion and Nro_habitacion IN " +
                "(SELECT Nro_habitacion FROM Reserva WHERE (check_in <= @Check_out AND check_out > @Check_in))";

            using (IDbConnection db = new SqlConnection(cadenaConexion))
            {
                db.Open();

                var count = db.ExecuteScalar<int>(query, new { Nro_habitacion = Nro_habitacion, Check_in = Check_in, Check_out = Check_out });

                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

     

        }

    }
}
