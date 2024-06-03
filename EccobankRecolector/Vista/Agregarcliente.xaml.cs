using EccobankRecolector.VistaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EccobankRecolector.Vista
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Agregarcliente : ContentPage
    {
        public Agregarcliente()
        {
            InitializeComponent();
            BindingContext = new VMclientes(Navigation);
        }
    }
}