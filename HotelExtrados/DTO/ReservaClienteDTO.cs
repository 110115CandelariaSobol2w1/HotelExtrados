using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelExtrados.DTO
{
    public class ReservaClienteDTO
    {
        public int Nro_habitacion { get; set; }
        public int cant_camas { get; set; }
        public bool Cochera { get; set; }
        public float precio { get; set; }
        public bool television { get; set; }
        public bool Desayuno { get; set; }
        public int idEstado { get; set; }

        public DateTime Check_out { get; set; }
    }
}
