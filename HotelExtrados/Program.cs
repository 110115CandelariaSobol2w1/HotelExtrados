using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using HotelExtrados.Controlador;
using HotelExtrados.Modelo;
using HotelExtrados.DTO;

namespace HotelExtrados
{
    internal class Program
    {

        static void Main(string[] args)
        {

            login();
        }

        public static void login()
        {
            UsuarioController userController = new UsuarioController();

            Console.WriteLine("Ingrese el usuario");
            string usuario = Console.ReadLine();
            Console.WriteLine("Ingrese su pass");
            string pass = Console.ReadLine();
            int credenciales = userController.Login(usuario, pass);

            if (credenciales == 1)
            {              
                Console.WriteLine("Bienvenido administrador");
                //aca abririamos el menu de adminsitrador
                menuAdmin();
            }
            else if (credenciales == 2)
            {
                Console.WriteLine("Bienvenido APP");

                menuApp();
            }
            else 
            {
                Console.WriteLine("El usuario ingresado no existe");
                login();
            }

            Console.Read();
        }

        public static void menuApp()
        {
            bool salir = false;

            while (!salir)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("BIENVENIDO AL PANEL DE ADMINISTRADOR");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("1. Ingrese 1 para ver el listado de todos los cuartos");
                Console.WriteLine("2. Ingrese 2 para ver el listado de cuartos disponibles");
                Console.WriteLine("3. Ingrese 3 para cargar un nuevo cliente");
                Console.WriteLine("4. Ingrese 4 para realizar una nueva reserva");
                Console.WriteLine("5. Ingrese 5 para cambiar el estado de un cuarto");
                Console.WriteLine("6. Ingrese 6 para salir de la aplicacion");
                Console.WriteLine("Elige una de las opciones");

                int opcion = Convert.ToInt32(Console.ReadLine());

                switch(opcion)
                {
                    case 1:
                        Console.WriteLine("Has elegido la opción 1");
                        getHabitaciones();

                        break;
                    case 2:
                         Console.WriteLine("Has elegido la opción 2");
                        getHabitacionesDesocupadas();
                        break;
                    case 3:
                        Console.WriteLine("Has elegido la opción 3");
                        agregarCliente();
                        break;
                    case 4:
                        Console.WriteLine("Has elegido la opción 4");
                        agregarReserva();
                        break;
                    case 5:
                        Console.WriteLine("Has elegido la opción 5");
                        getHabitacionesDesocupadasLimpieza();
                        break;
                    case 6:
                        Console.WriteLine("Has elegido salir de la app");
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Elige una opcion entre 1 y 7");
                        break;
                }
            }
        }

        public static void menuAdmin()
        {
            bool salir = false;

            while (!salir)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("BIENVENIDO AL PANEL DE APP");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("1. Ingrese 1 para agregar un nuevo cuarto");
                Console.WriteLine("2. Ingrese 2 para cambiar el estado de un cuarto");
                Console.WriteLine("3. Ingrese 3 para volver al estado anterior");
                Console.WriteLine("4. Ingrese 4 para cambiar cuarto de renovacion a disponible");
                Console.WriteLine("6. Ingrese 5 para salir de la aplicacion");
                Console.WriteLine("Elige una de las opciones");

                int opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Has elegido la opción 1");
                        agregarHabitacion();
                        break;
                    case 2:
                        Console.WriteLine("Has elegido la opción 2");
                        updateLimpiezaOrRenovacion();
                        break;
                    case 3:
                        Console.WriteLine("Has elegido la opción 3");
                        //llamamos a la funcion que vuelve al estado anterior
                        break;
                    case 4:
                        Console.WriteLine("Has elegido la opción 4");
                        updateRenovacionDisponbile();
                        break;
                    case 5:
                        Console.WriteLine("Has elegido salir de la app");
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Elige una opcion entre 1 y 5");
                        break;
                }
            }
        }

        public static void getHabitaciones()
        {
            HabitacionController habController = new HabitacionController();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Habitaciones comunes");
            Console.ForegroundColor = ConsoleColor.Gray;
            IEnumerable<Habitacion> habitaciones = habController.obtenerHabitacionesNormales();
            foreach (Habitacion habitacion in habitaciones)
            {
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("El nro de habitacion es: " + habitacion.Nro_Habitacion);
                Console.WriteLine(" Cantidad de camas: " + habitacion.cant_camas);

                if(habitacion.Cochera == true)
                {
                    Console.WriteLine("Cochera: Incluida");
                }
                else
                {
                    Console.WriteLine("Cochera: No incluida");
                }

                Console.WriteLine(" Precio: " + habitacion.precio);

                if(habitacion.television == true)
                {
                    Console.WriteLine("Televisor: Si");
                }
                else
                {
                    Console.WriteLine("Televisor: No posee");
                }

                if (habitacion.Desayuno == true)
                {
                    Console.WriteLine("Desayuno: Incluido");
                }
                else
                {
                    Console.WriteLine("Desayuno: No incluido");
                }

                Console.WriteLine("----------------------------------------------");

            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("///////////////////////////////");
            Console.WriteLine("Habitaciones VIP");
            Console.ForegroundColor = ConsoleColor.Gray;
            IEnumerable<Habitacion> habitacionesVip = habController.obtenerHabitacionesVip();
            foreach (Habitacion habitacion in habitacionesVip)
            {
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("El nro de habitacion es: " + habitacion.Nro_Habitacion);
                Console.WriteLine(" Cantidad de camas: " + habitacion.cant_camas);
                //Console.WriteLine(" Cochera: " + habitacion.Cochera);
                if (habitacion.Cochera == true)
                {
                    Console.WriteLine("Cochera: Incluida");
                }
                else
                {
                    Console.WriteLine("Cochera: No incluida");
                }
                Console.WriteLine(" Precio: " + habitacion.precio);
                //Console.WriteLine(" Servicio: " + habitacion.Servicio);
                if (habitacion.Servicio == true)
                {
                    Console.WriteLine("Servicio a la habitacion: Incluido");
                }
                else
                {
                    Console.WriteLine("Servicio a la habitacion: No incluido");
                }
               // Console.WriteLine(" Hidromasaje: " + habitacion.Hidromasaje);
                if (habitacion.Hidromasaje == true)
                {
                    Console.WriteLine("Hidromasaje: Si");
                }
                else
                {
                    Console.WriteLine("Hidromasaje: No posee");
                }
                Console.WriteLine("----------------------------------------------");

            }


        }

        public static void getHabitacionesDesocupadas()
        {
            HabitacionController habController = new HabitacionController();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Habitaciones comunes");
            Console.ForegroundColor = ConsoleColor.Gray;
            IEnumerable<Habitacion> habitaciones = habController.obtenerHabitacionesNormalesDesocupadas();
            foreach (Habitacion habitacion in habitaciones)
            {
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("El nro de habitacion es: " + habitacion.Nro_Habitacion);
                Console.WriteLine(" Cantidad de camas: " + habitacion.cant_camas);
                //Console.WriteLine(" Cochera: " + habitacion.Cochera);
                if (habitacion.Cochera == true)
                {
                    Console.WriteLine("Cochera: Incluida");
                }
                else
                {
                    Console.WriteLine("Cochera: No incluida");
                }
                Console.WriteLine(" Precio: " + habitacion.precio);
                //Console.WriteLine(" Television: " + habitacion.television);

                if (habitacion.television == true)
                {
                    Console.WriteLine("Televisor: Si");
                }
                else
                {
                    Console.WriteLine("Televisor: No posee");
                }

                //Console.WriteLine(" Desayuno: " + habitacion.Desayuno);
                if (habitacion.Desayuno == true)
                {
                    Console.WriteLine("Desayuno: Incluido");
                }
                else
                {
                    Console.WriteLine("Desayuno: No incluido");
                }
                Console.WriteLine("----------------------------------------------");

            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("///////////////////////////////");
            Console.WriteLine("Habitaciones VIP");
            Console.ForegroundColor = ConsoleColor.Gray;
            IEnumerable<Habitacion> habitacionesVip = habController.obtenerHabitacionesVipDesocupadas();
            foreach (Habitacion habitacion in habitacionesVip)
            {
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("El nro de habitacion es: " + habitacion.Nro_Habitacion);
                Console.WriteLine(" Cantidad de camas: " + habitacion.cant_camas);
               // Console.WriteLine(" Cochera: " + habitacion.Cochera);
                if (habitacion.Cochera == true)
                {
                    Console.WriteLine("Cochera: Incluida");
                }
                else
                {
                    Console.WriteLine("Cochera: No incluida");
                }
                Console.WriteLine(" Precio: " + habitacion.precio);
                //Console.WriteLine(" Servicio: " + habitacion.Servicio);
                if (habitacion.Servicio == true)
                {
                    Console.WriteLine("Servicio a la habitacion: Incluido");
                }
                else
                {
                    Console.WriteLine("Servicio a la habitacion: No incluido");
                }
                //Console.WriteLine(" Hidromasaje: " + habitacion.Hidromasaje);
                if (habitacion.Hidromasaje == true)
                {
                    Console.WriteLine("Hidromasaje: Si");
                }
                else
                {
                    Console.WriteLine("Hidromasaje: No posee");
                }
                Console.WriteLine("----------------------------------------------");

            }


        }

        public static void agregarCliente()
        {
            Cliente nuevo = new Cliente();
            ClienteController controller = new ClienteController();

            Console.WriteLine("Ingrese el DNI del cliente");
            nuevo.Dni = Int64.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el nombre del cliente");
            nuevo.Nombre = Console.ReadLine();
            Console.WriteLine("Ingrese el apellido del cliente");
            nuevo.Apellido = Console.ReadLine();
            Console.WriteLine("Ingrese el email del cliente");
            nuevo.Email = Console.ReadLine();

            controller.agregarCliente(nuevo);
        }

        public static void agregarReserva()
        {
            Reserva nueva = new Reserva();
            ReservaController  controller = new ReservaController();

            Console.WriteLine("Ingrese el numero de habitacion");
            nueva.Nro_habitacion = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ingrese el DNI del cliente");
            nueva.Dni_cliente = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Ingrese la fecha de ingreso");
            nueva.Check_in = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Ingrese la fecha de salida");
            nueva.Check_out = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Ingrese el estado de la reserva: ACTIVA - INACTIVA");
            nueva.Estado = Console.ReadLine();


            controller.agregarReserva(nueva);

           
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Reserva realizada con exito");
            Console.ForegroundColor = ConsoleColor.Gray;

        }

        public static void agregarHabitacion()
        {
            HabitacionController controller = new HabitacionController();
            Habitacion nueva = new Habitacion();

            Console.WriteLine("Ingrese el numero de habitacion");
            nueva.Nro_Habitacion = Convert.ToInt32(Console.ReadLine());


            Console.WriteLine("Ingrese el tipo de habitacion:");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Comun: 1  VIP: 2");
            Console.ForegroundColor = ConsoleColor.Gray;
            nueva.idTipo = Convert.ToInt32(Console.ReadLine());


            Console.WriteLine("Ingrese la cantidad de camas");
            nueva.cant_camas = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Posee cochera?");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Si tiene: TRUE   -   Si no tiene: FALSE");
            Console.ForegroundColor = ConsoleColor.Gray;
            nueva.Cochera = Convert.ToBoolean(Console.ReadLine());


            Console.WriteLine("Ingrese el precio por noche");
            nueva.precio = Convert.ToInt32(Console.ReadLine());


            Console.WriteLine("Posee televisor?");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Si tiene: TRUE   -   Si no tiene: FALSE");
            Console.ForegroundColor = ConsoleColor.Gray;
            nueva.television = Convert.ToBoolean(Console.ReadLine());


            Console.WriteLine("Incluye desayuno?");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Si tiene: TRUE   -   Si no tiene: FALSE");
            Console.ForegroundColor = ConsoleColor.Gray;
            nueva.Desayuno = Convert.ToBoolean(Console.ReadLine());


            Console.WriteLine("Posee servicio al cuarto?");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Si tiene: TRUE   -   Si no tiene: FALSE");
            Console.ForegroundColor = ConsoleColor.Gray;
            nueva.Servicio = Convert.ToBoolean(Console.ReadLine());


            Console.WriteLine("Posee hidromasajes?");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Si tiene: TRUE   -   Si no tiene: FALSE");
            Console.ForegroundColor = ConsoleColor.Gray;
            nueva.Hidromasaje = Convert.ToBoolean(Console.ReadLine());

            Console.WriteLine("Ingrese estado del cuarto");
            Console.WriteLine("Disponible: 1");
            Console.WriteLine("Ocupado: 2");
            Console.WriteLine("Limpieza: 3");
            Console.WriteLine("Renovacion: 4");
            nueva.idEstado = Convert.ToInt32(Console.ReadLine());

            controller.agregarHabitacion(nueva);

        }

        public static void getHabitacionesDesocupadasLimpieza()
        {
            HabitacionController controller = new HabitacionController();

            IEnumerable<habitacionesDTO> habitaciones = controller.obtenerHabitacionesDesocupadasLimpieza();
            foreach (habitacionesDTO habitacion in habitaciones)
            {
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("El nro de habitacion es: " + habitacion.Nro_Habitacion);
                Console.WriteLine("El estado de la habitacion es: " + habitacion.descripcion);


            }

            Habitacion nueva = new Habitacion();
            Console.WriteLine("Ingrese el numero de habitacion que quiere modificar el estado:");
            nueva.Nro_Habitacion = Convert.ToInt32(Console.ReadLine());
            controller.CambiarEstado(nueva);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("MODIFICACION EXITOSA");
            Console.ForegroundColor = ConsoleColor.Gray;

            IEnumerable<habitacionesDTO> habitaciones2 = controller.obtenerHabitacionesDesocupadasLimpieza();
            foreach (habitacionesDTO habitacion in habitaciones2)
            {
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("El nro de habitacion es: " + habitacion.Nro_Habitacion);
                Console.WriteLine("El estado de la habitacion es: " + habitacion.descripcion);


            }



        }

        public static void updateRenovacionDisponbile()
        {
            HabitacionController controller = new HabitacionController();

            IEnumerable<habitacionesDTO> habitaciones = controller.obtenerHabitacionesRenovacion();
            foreach (habitacionesDTO habitacion in habitaciones)
            {
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("El nro de habitacion es: " + habitacion.Nro_Habitacion);
                Console.WriteLine("El estado de la habitacion es: " + habitacion.descripcion);

            }

            Habitacion nueva = new Habitacion();
            Console.WriteLine("Ingrese el numero de habitacion que pasa a estar disponible:");
            nueva.Nro_Habitacion = Convert.ToInt32(Console.ReadLine());
            controller.RenovacionADisponible(nueva);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("MODIFICACION EXITOSA");
            Console.ForegroundColor = ConsoleColor.Gray;

            IEnumerable<habitacionesDTO> habitaciones2 = controller.obtenerHabitacionesDesocupadasLimpieza();
            foreach (habitacionesDTO habitacion in habitaciones2)
            {
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("El nro de habitacion es: " + habitacion.Nro_Habitacion);
                Console.WriteLine("El estado de la habitacion es: " + habitacion.descripcion);


            }
        }

        public static void updateLimpiezaOrRenovacion()
        {
            HabitacionController controller = new HabitacionController();

            Habitacion habitacion = new Habitacion();

            Console.WriteLine("Ingrese el Nro de habitacion que desea modificar el estado");
            habitacion.Nro_Habitacion = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Ingrese 1 si desea poner la habitacion en estado de Limpieza");
            Console.WriteLine("Ingrese 2 si desea poner la habitacion en estado de Renovacion");
            int estadoNuevo = Convert.ToInt32(Console.ReadLine());

            if(estadoNuevo == 1)
            {
                controller.estadoALimpieza(habitacion);
            }
            else if(estadoNuevo == 2)
            {
                controller.estadoARenovacion(habitacion);
            }
            else
            {
                Console.WriteLine("Opcion ingresada no valida");
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("MODIFICACION EXITOSA");
            Console.ForegroundColor = ConsoleColor.Gray;
        }


    }
    
}
