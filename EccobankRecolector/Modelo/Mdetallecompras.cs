﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EccobankRecolector.Modelo
{
   public class Mdetallecompras
    {
        public string Iddetallecompra { get; set; }

        public string fecha { get; set; }
        public string Ganancia { get; set; }
        public string Idcliente { get; set; }
        public string Idproducto { get; set; }
        public string Cantidad { get; set; }
        public string Preciocompra { get; set; }
        public string PrecioVenta { get; set; }
        public string Total { get; set; }
        public string Und { get; set; }
        public string Puntos { get; set; }
        public string Estado { get; set; }
        //Campos para mostrar detalle compras con productos
        public decimal Suma { get; set; }
        public string Producto { get; set; }
        public string ProducIcono { get; set; }
        public string Color { get; set; }
    }
}