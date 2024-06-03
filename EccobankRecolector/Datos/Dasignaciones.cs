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
   public class Dasignaciones
    {
        public async Task <List<Masignaciones>> Mostrarasignaciones(Masignaciones parametrosPedir)
        {
            return (await Constantes.firebase
                .Child("Asignaciones")
                .OrderByKey()
                .OnceAsync<Masignaciones>()).Where(a => a.Object.idrecolector == parametrosPedir.idrecolector).Select(item => new Masignaciones
                {
                    idsolicitud = item.Object.idsolicitud
                }).ToList();
        }
        public async Task<List<Msolicitudesrecojo>> MostrarclientesAsignados(Masignaciones parametrosPedir)
        {
            var Listasolicitudes = new List<Msolicitudesrecojo>();
            var funcionasignaciones = new Dasignaciones();
            var parametrosasignaciones = new Masignaciones();
            parametrosasignaciones.idrecolector = parametrosPedir.idrecolector;
            var listaasignaciones = await funcionasignaciones.Mostrarasignaciones(parametrosasignaciones);
            foreach(var itemasig in listaasignaciones)
            {
                var funcion = new Dsolicitudesrecojo();
                var parametros = new Msolicitudesrecojo();
                parametros.Idsolicitud = itemasig.idsolicitud;
                #region cargar datos clientes turnos
              
                var listasolicitudes = await funcion.MostrarsolicitRecojo(parametros);
                string Nombrecliente = "";
                string Direccion = "";
                string Fecha = "";
                string Geolocalizacion = "";
                string Turno = "";
                string Idturno = "";
                string Estado = "";
                var fclientes = new Dclientes();
                var pclientes = new Mclientes();
                foreach (var itemsoli in listasolicitudes)
                {
                    Estado = itemsoli.Estado;
                    Fecha = itemsoli.Fecha;
                    Idturno = itemsoli.Idturno;
                    pclientes.Idcliente = itemsoli.Idcliente;
                }
                var fturno = new Dturnosrecojos();
                var pturno = new Mturnosrecojo();
                pturno.Idturno = Idturno;
                var listaturnos = await fturno.Mostrarturnosrecojo(pturno);
                foreach (var itemturno in listaturnos)
                {
                    Turno = itemturno.Turno;
                }
                var listaclientes = await fclientes.MostrarclientesXid(pclientes);
                foreach (var datacliente in listaclientes)
                {
                    Geolocalizacion = datacliente.Geo;
                    Nombrecliente = datacliente.NombresApe;
                    Direccion = datacliente.Direccion;
                }
                #endregion
                parametros.Nombrecliente = Nombrecliente;
                parametros.Estado = Estado;
                parametros.Direccion = Direccion;
                parametros.Fecha = Fecha;
                parametros.Geolocalizacion = Geolocalizacion;
                parametros.Turno = Turno;
                parametros.Idcliente = pclientes.Idcliente;
                Listasolicitudes.Add(parametros);
            }
            return Listasolicitudes;
        }
        public async Task Eliminarasignacion(Masignaciones parametros)
        {
            var dataEliminar = (await Constantes.firebase
                .Child("Asignaciones")
                .OnceAsync<Masignaciones>())
                .Where(a => a.Object.idsolicitud == parametros.idsolicitud)
                .FirstOrDefault();
            await Constantes.firebase
                .Child("Asignaciones")
                .Child(dataEliminar.Key)
                .DeleteAsync();
        }

    }
}
