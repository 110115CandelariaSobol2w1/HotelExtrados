using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelExtrados.Modelo
{
    public class Reserva
    {
        public int idReserva { get; set; }
        public int Nro_habitacion { get; set; }  
        public long Dni_cliente { get; set; }
        public DateTime Check_in { get; set; }
        public DateTime Check_out { get; set; }  
        public string Estado { get; set; }



    }
}
