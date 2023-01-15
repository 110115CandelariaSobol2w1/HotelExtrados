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
                Console.WriteLine("BIENVENIDO AL PANEL ATENCION AL PUBLICO");
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
                Console.WriteLine("BIENVENIDO AL PANEL DE ADMINISTRADOR");
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
                        volverEstadoAnterior();
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

                if(habitacion.idEstado == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    DateTime checkOut = habController.obtenerCheckOut(habitacion.Nro_Habitacion);
                    Console.WriteLine("La habitacion se encuenta ocupada hasta el dia {0}", checkOut);
                    Console.ForegroundColor = ConsoleColor.Gray;
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

                if (habitacion.idEstado == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    DateTime checkOut = habController.obtenerCheckOut(habitacion.Nro_Habitacion);
                    Console.WriteLine("La habitacion se encuenta ocupada hasta el dia {0}", checkOut);
                    Console.ForegroundColor = ConsoleColor.Gray;
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
            long dni = Convert.ToInt64(Console.ReadLine());
            nuevo.Dni = dni;

            bool clienteReserva = controller.verificarCliente(dni);
            if (clienteReserva)
            {
                Console.WriteLine("El cliente con el dni {0} ya se encuentra registrado", dni);
            }
            else
            {
                Console.WriteLine("Ingrese el nombre del cliente");
                nuevo.Nombre = Console.ReadLine();
                Console.WriteLine("Ingrese el apellido del cliente");
                nuevo.Apellido = Console.ReadLine();
                Console.WriteLine("Ingrese el email del cliente");
                nuevo.Email = Console.ReadLine();

                controller.agregarCliente(nuevo);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Cliente registrado con exito!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            
        }

        public static void agregarReserva()
        {
            Reserva nueva = new Reserva();
            ReservaController  controller = new ReservaController();
            ClienteController controllerCliente = new ClienteController();
            HabitacionController habController = new HabitacionController();

            Console.WriteLine("Ingrese el numero de habitacion");
            int nroHabitacion = Convert.ToInt32(Console.ReadLine());
            nueva.Nro_habitacion = nroHabitacion;
            Console.WriteLine("Ingrese el DNI del cliente");
            long dniReserva = Convert.ToInt64(Console.ReadLine());
            nueva.Dni_cliente = dniReserva;

            //Deberia verificar si el cliente esta cargado, y si no ir al menu de carga
            bool clienteReserva = controllerCliente.verificarCliente(dniReserva);
            if (clienteReserva == false)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("ERROR. Cliente no encontrado!!. Primero debe registrar al cliente");
                Console.ForegroundColor = ConsoleColor.Gray;
                menuApp();
     
            }
            else 
            {

                Console.WriteLine("Ingrese la fecha de ingreso");
                DateTime Check_in = Convert.ToDateTime(Console.ReadLine());
                nueva.Check_in = Check_in;
                Console.WriteLine("Ingrese la fecha de salida");
                DateTime Check_out = Convert.ToDateTime(Console.ReadLine());
                nueva.Check_out = Check_out;

                bool verificacion = habController.verificarHabitacionFecha(nroHabitacion, Check_in, Check_out);

                if (verificacion == false)
                {
                    nueva.Estado = 1;

                    controller.agregarReserva(nueva);

                    //Update para cambiar el estado de la habitacion en la tabla habitaciones
                    controller.CambiarEstadoHabitacion(nroHabitacion);

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Reserva realizada con exito");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else if (verificacion)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("La habitacion no se encuentra disponbile en las fechas seleccionadas");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }

           

        }

        public static void agregarHabitacion()
        {
            HabitacionController controller = new HabitacionController();
            Habitacion nueva = new Habitacion();

            Console.WriteLine("Ingrese el numero de habitacion");
            int nroHabitacion = Convert.ToInt32(Console.ReadLine());
            nueva.Nro_Habitacion = nroHabitacion;
            bool habitacionNueva = controller.verificarHabitacion(nroHabitacion);
            if (habitacionNueva)
            {
                Console.WriteLine("La habitacion numero {0} ya se encuentra registrada", nroHabitacion);
            }
            else
            {
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

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Habitacion registrada con exito!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }


            

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

            Console.ForegroundColor = ConsoleColor.Yellow;
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

            Reserva reserva = new Reserva();

            Console.WriteLine("Ingrese el Nro de habitacion que desea modificar el estado");
            int nroHabitacion = Convert.ToInt32(Console.ReadLine());
            habitacion.Nro_Habitacion = nroHabitacion;
            reserva.Nro_habitacion = nroHabitacion;


            Console.WriteLine("Ingrese 1 si desea poner la habitacion en estado de Limpieza");
            Console.WriteLine("Ingrese 2 si desea poner la habitacion en estado de Renovacion");

            int estadoNuevo = Convert.ToInt32(Console.ReadLine());

            if (estadoNuevo == 1)
            {
                controller.estadoALimpieza(habitacion);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("MODIFICACION EXITOSA");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (estadoNuevo == 2 && reserva.Estado == 0)
            {
                controller.estadoARenovacion(habitacion);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("MODIFICACION EXITOSA");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if(estadoNuevo == 2 && reserva.Estado == 1)
            {
                controller.estadoARenovacion(habitacion);
                controller.cancelarReserva(reserva);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("MODIFICACION EXITOSA. RESERVA CANCELADA POR REMODELACIONES");
                Console.ForegroundColor = ConsoleColor.Gray;

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Opcion ingresada no valida");
                Console.ForegroundColor = ConsoleColor.Gray;
            }

           
        }

        public static void volverEstadoAnterior()
        {
            HabitacionController controller = new HabitacionController();
            Habitacion habitacion = new Habitacion();

            Console.WriteLine("Ingrese el Nro de habitacion que desea modificar el estado");
            int nroHabitacion = Convert.ToInt32(Console.ReadLine());
            habitacion.Nro_Habitacion = nroHabitacion;

           int estadoHabitacion =  controller.EstadoHabitacion(habitacion);

            if(estadoHabitacion == 1)
            {
                controller.LimpiezaAOcupado(habitacion);
                long dniCliente = controller.obtenerClienteReserva(habitacion);
                Console.WriteLine("La habitacion se encuentra ocupada para el cliente cuyo dni es:  {0}" , dniCliente);
            }
            else if(estadoHabitacion == 0)
            {
                controller.LimpiezaADisponible(habitacion);
                
                Console.WriteLine("La habitacion se encuentra disponbile");
            }
            else
            {
                Console.WriteLine("Error");
            }
        }


    }
    
}
