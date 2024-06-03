using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Firebase.Database.Query;
using EccobankRecolector.Modelo;
using EccobankRecolector.Conexiones;

namespace EccobankRecolector.Datos
{
   public class Dsolicitudesrecojo
    {
        public async Task<List<Msolicitudesrecojo>> MostrarsolicitRecojo(Msolicitudesrecojo parametrosPedir)
        {
            return (await Constantes.firebase
                .Child("Solicitudesrecojo")
                .OrderByKey()
                .OnceAsync<Msolicitudesrecojo>()).Where(a => a.Object.Estado == "Asignado").Where(b=>b.Key==parametrosPedir.Idsolicitud).Select(item => new Msolicitudesrecojo
                {
                    Idcliente = item.Object.Idcliente,
                    Fecha = item.Object.Fecha,
                    Estado = item.Object.Estado,
                    Idturno = item.Object.Idturno
                }).ToList();
        }
        public async Task Eliminarsolicitud(Msolicitudesrecojo parametros)
        {
            var dataEliminar = (await Constantes.firebase
                .Child("Solicitudesrecojo")
                .OnceAsync<Msolicitudesrecojo>())
                .Where(a => a.Key == parametros.Idsolicitud)
                .FirstOrDefault();
            await Constantes.firebase.Child("Solicitudesrecojo")
                .Child(dataEliminar.Key).DeleteAsync();
        }
    }
}
