using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Acr.UserDialogs;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BipTranslator.ViewModels
{
    class ListeningViewModel : BaseViewModel
    {
        public ListeningViewModel(/*IUserDialogs dialogs*/)
        {
            //this.Dialogs = dialogs;
            Title = "Clica aê cabra!";
        }

        // protected IUserDialogs Dialogs { get; }
    }
}
