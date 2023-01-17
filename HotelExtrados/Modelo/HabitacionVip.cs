using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelExtrados.Modelo
{
    public class HabitacionVip : Habitacion
    {
        public bool Servicio { get; set; }
        public bool Hidromasaje { get; set; }
    }
}
