using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EccobankRecolector.Modelo;
using EccobankRecolector.VistaModelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EccobankRecolector.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Agregarcompra : ContentPage
    {
        public Agregarcompra(Mproductos productos)
        {
            InitializeComponent();
            BindingContext = new VMagregarcompra(Navigation, productos);
        }

    }
}