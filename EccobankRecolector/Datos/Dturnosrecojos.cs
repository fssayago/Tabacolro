using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Firebase.Database.Query;
using EccobankRecolector.Modelo;
using EccobankRecolector.Conexiones;
using System.IO;

namespace EccobankRecolector.Datos
{
  public  class Dturnosrecojos
    {
        public async Task<List<Mturnosrecojo>> Mostrarturnosrecojo(Mturnosrecojo parametrosPedir)
        {

            return (await Constantes.firebase
              .Child("Turnosrecojo")
              .OnceAsync<Mturnosrecojo>()).Where(a => a.Key == parametrosPedir.Idturno).Select(item => new Mturnosrecojo
              {
                  Idturno = item.Key,
                  Turno = item.Object.Turno
              }).ToList();

        }
    }
}
