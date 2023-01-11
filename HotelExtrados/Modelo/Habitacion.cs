using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelExtrados.Modelo
{
    public class Habitacion
    {
        public int idHabitacion { get; set; } 
        public int Nro_Habitacion { get; set; }
        public int idTipo { get; set; } 
        public int cant_camas { get; set; }
        public bool Cochera { get; set; }   
        public float precio { get; set; }
        public bool television { get; set; }
        public bool Desayuno { get; set; }
        public bool Servicio { get; set; }
        public bool Hidromasaje { get; set; }   
        public int idEstado { get; set; }

  
    }
}
