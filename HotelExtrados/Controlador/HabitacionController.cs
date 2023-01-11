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

            string query = "select nro_habitacion, cant_camas, cochera, precio, television, desayuno" +
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

            string query = "select Nro_Habitacion, Cant_camas,Cochera,Precio,Servicio,Hidromasaje " +
                "from Habitaciones where IdTipo = 2";


            using (IDbConnection db = new SqlConnection(cadenaConexion))
            {
                db.Open();
                var habitacionesVip = db.Query<Habitacion>(query).ToList();


                return habitacionesVip;
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

        //ADMIN PUNTO 3

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


    }
}
