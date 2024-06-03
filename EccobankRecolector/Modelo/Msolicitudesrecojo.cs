using System;
using System.Collections.Generic;
using System.Text;

namespace EccobankRecolector.Modelo
{
   public class Msolicitudesrecojo
    {
        public string Idsolicitud { get; set; }
        public string Fecha { get; set; }
        public string Idcliente { get; set; }
        public string Idturno { get; set; }
        public string Estado { get; set; }
        //Parametros unicamente para mostrar lista
        public string Nombrecliente { get; set; }
        public string Direccion { get; set; }
        public string Geolocalizacion { get; set; }
        public string Turno { get; set; }
    }
}
