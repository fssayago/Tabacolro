using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EccobankRecolector.Conexiones;
using EccobankRecolector.Datos;
using EccobankRecolector.Modelo;
using Firebase.Database.Query;
using EccobankRecolector.VistaModelo;

namespace EccobankRecolector.Datos
{
   public class Drecolectores
    {
        public async Task<List<Mrecolectores>>Mostrarrecolectores(Mrecolectores parametrosPedir)
        {
            return (await Constantes.firebase
                .Child("Recolectores")
                .OnceAsync<Mrecolectores>()).Where(a => a.Object.Correo == parametrosPedir.Correo).Select(item => new Mrecolectores
                {
                    Nombre = item.Object.Nombre,
                    Perfil = item.Object.Perfil,
                    Idrecolector =item.Key
                }).ToList();
        }
    
        public async Task<bool> InsertarRecolectores(Mrecolectores parametros)
        {
        await Constantes.firebase
            .Child("Recolectores")
            .PostAsync(new Mrecolectores()
            {
                Correo = parametros.Correo,
                Estado = parametros.Estado,
                Identificacion = parametros.Identificacion,
                Nombre = parametros.Nombre
            });
        return true;
        }
        public async Task<List<Mrecolectores>> BuscarRecolectores(Mrecolectores parametrosPedir)
        {
        return (await Constantes.firebase
            .Child("Recolectores")
            .OrderByKey()
            .OnceAsync<Mrecolectores>())
            .Where(a => a.Object.Identificacion == parametrosPedir.Identificacion)
            .Where(b => b.Object.Estado == "Activo")
            .Select(item => new Mrecolectores
            {
                Idrecolectores = item.Key,
                Nombre = item.Object.Nombre
            }).ToList();
        }
    }
}
