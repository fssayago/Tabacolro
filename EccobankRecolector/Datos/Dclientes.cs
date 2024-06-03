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
   public class Dclientes
    {
        string rutafoto;
        public async Task Insertarclientes(Mclientes parametros)
        {
            await Constantes.firebase
                .Child("Clientes")
                .PostAsync(new Mclientes()

                {
                    Direccion = parametros.Direccion,
                    FotoFachada = parametros.FotoFachada,
                    Geo = parametros.Geo,
                    IdDepa = parametros.IdDepa,
                    IdDis = parametros.IdDis,
                    IdPais = parametros.IdPais,
                    IdPro = parametros.IdPro,
                    IdZona = parametros.IdZona,
                    Identificacion = parametros.Identificacion,
                    NombresApe = parametros.NombresApe,
                    Kgacumulados = parametros.Kgacumulados,
                    Puntos = parametros.Puntos,
                    Totalcobrado = parametros.Totalcobrado,
                    Totalporcobrar = parametros.Totalporcobrar
                });
        }
        public async Task<string> Subirfotofachada(Stream imageStream, string Identificacion)
        {
            var stroageImage = await new FirebaseStorage("ecob-3563a.appspot.com")
                .Child("Fachadasclientes")
                .Child(Identificacion + ".jpg")
                .PutAsync(imageStream);
            rutafoto = stroageImage;
            return rutafoto;
        }
        public async Task<List<Mclientes>> MostrarclientesXid(Mclientes parametrosPedir)
        {

            return (await Constantes.firebase
              .Child("Clientes")
              .OnceAsync<Mclientes>()).Where(a => a.Key == parametrosPedir.Idcliente).Select(item => new Mclientes
              {
                  Identificacion = item.Object.Identificacion,
                  NombresApe = item.Object.NombresApe,
                  FotoFachada = item.Object.FotoFachada,
                  Idcliente = item.Key,
                  Geo = item.Object.Geo,
                  Direccion = item.Object.Direccion

              }).ToList();
        }
    }
}
