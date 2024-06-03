using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Firebase.Database.Query;
using EccobankRecolector.Modelo;
using EccobankRecolector.Conexiones;
using System.IO;
using Firebase.Storage;
namespace EccobankRecolector.Datos
{
    public class Ddetallecompras
    {
        List<Mdetallecompras> Listacompras = new List<Mdetallecompras>();
        public async Task InsertarDetallecompra(Mdetallecompras parametros)
        {
            await Constantes.firebase
                .Child("Detallecompra")
                .PostAsync(new Mdetallecompras()

                {
                    Estado = parametros.Estado,
                    fecha = DateTime.Now.ToString(),
                    Ganancia = parametros.Ganancia,
                    Idcliente = parametros.Idcliente,
                    Idproducto = parametros.Idproducto,
                    Cantidad = parametros.Cantidad,
                    Preciocompra = parametros.Preciocompra,
                    PrecioVenta = parametros.PrecioVenta,
                    Total = parametros.Total,
                    Und = parametros.Und,
                    Puntos = parametros.Puntos
                });
        }
        public async Task<List<Mdetallecompras>> MostrarTotal(Mdetallecompras parametrosPedir)
        {
            return (await Constantes.firebase
                .Child("Detallecompra")
                .OnceAsync<Mdetallecompras>()).Where(a => a.Object.Idcliente == parametrosPedir.Idcliente).Where(b => b.Object.Estado == "SIN CONFIRMAR").Select(item => new Mdetallecompras
                {
                    Total = item.Object.Total
                }).ToList();
        }
        public async Task<string> SumarTotal(Mdetallecompras parametrosPedir)
        {
            var funcion = new Ddetallecompras();
            var parametros = new Mdetallecompras();
            parametros.Idcliente = parametrosPedir.Idcliente;
            decimal total = 0;
            var listatotal = await funcion.MostrarTotal(parametros);
            foreach (var hobit in listatotal)
            {
                total += Convert.ToDecimal(hobit.Total);
            }
            return total.ToString();
        }
        public async Task EliDcompraSinconfirmar(Mdetallecompras parametros)
        {
            var dataEliminar = (await Constantes.firebase
                .Child("Detallecompra")
                .OnceAsync<Mdetallecompras>())
                .Where(a => a.Object.Idcliente == parametros.Idcliente)
                .Where(b => b.Object.Estado == "SIN CONFIRMAR")
                .Select(item => new Mdetallecompras
                {
                    Iddetallecompra = item.Key
                }
                ).ToList();
            foreach (var data in dataEliminar)
            {
                string id = data.Iddetallecompra;
                await Constantes.firebase
                             .Child("Detallecompra")
                             .Child(id).DeleteAsync();
            }

        }
        public async Task Confirmardetallecompra(Mdetallecompras parametros)
        {
            var data = (await Constantes.firebase
                .Child("Detallecompra")
                .OnceAsync<Mdetallecompras>())
                .Where(a => a.Object.Idcliente == parametros.Idcliente)
                .Where(b => b.Object.Estado == "SIN CONFIRMAR");

            foreach (var hobit in data)
            {
                hobit.Object.Estado = "PAGADO";
                await Constantes.firebase
                    .Child("Detallecompra")
                    .Child(hobit.Key)
                    .PutAsync(hobit.Object);

            }

        }
        public async Task<List<Mdetallecompras>> MostrarDcompra(string Idcliente)
        {
            decimal total = 0;
            var data = (await Constantes.firebase
       .Child("Detallecompra")
       .OrderByKey()
       .OnceAsync<Mdetallecompras>())
       .Where(a => a.Object.Idcliente == Idcliente)
       .Where(b=>b.Object.Estado=="SIN CONFIRMAR");
            foreach(var hobit in data)
            {
                var parametros = new Mdetallecompras();
                parametros.Preciocompra = "Precio de compra por " + hobit.Object.Und + " = S/." + hobit.Object.Preciocompra;
                parametros.Cantidad = hobit.Object.Cantidad;
                parametros.Total = "S/. " + hobit.Object.Total;
                parametros.Und = hobit.Object.Und;
                parametros.fecha = hobit.Object.fecha;
                parametros.Iddetallecompra = hobit.Key;
                total += Convert.ToDecimal(hobit.Object.Total);
                parametros.Suma = total;
                var funcionProductos = new Dproductos();
                var parametrosProduc = new Mproductos();
                parametrosProduc.Idproducto = hobit.Object.Idproducto;
                var dt = await funcionProductos.MostrarproductosXid(parametrosProduc);
                foreach (var dtpro in dt)
                {
                    parametros.Producto = dtpro.Descripcion + " (" + parametros.Cantidad + " " + parametros.Und + ")";
                    parametros.ProducIcono = dtpro.Icono;
                    parametros.Color = dtpro.Color;
                }
                Listacompras.Add(parametros);
            }
            return Listacompras.ToList();
        }
        public async Task EliminarDcompra(Mdetallecompras parametros)
        {
            var dataEliminar = (await Constantes.firebase
                .Child("Detallecompra")
                .OnceAsync<Mdetallecompras>())
                .Where(a => a.Key == parametros.Iddetallecompra)
                .FirstOrDefault();
            await Constantes.firebase.Child("Detallecompra")
                .Child(dataEliminar.Key).DeleteAsync();
        }
    }
}
