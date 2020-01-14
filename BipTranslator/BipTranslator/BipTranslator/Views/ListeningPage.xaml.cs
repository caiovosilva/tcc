using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.ComponentModel;
using Xamarin.Forms.Xaml;

namespace BipTranslator.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListeningPage : ContentPage
    {
        public ListeningPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("clicou hein!", "iniciando gravação agorinha", "OK");
        }
    }
}