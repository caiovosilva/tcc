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
            this.TryToGetPermissionsAsync();

        }

        protected IUserDialogs Dialogs { get; }

        private async Task TryToGetPermissionsAsync()
        {
            bool permissionsGranted = true;

            var permissionsStartList = new List<Permission>()
            {
                Permission.Storage,
                Permission.Microphone
            };

            var permissionsNeededList = new List<Permission>();
            try
            {
                foreach (var permission in permissionsStartList)
                {
                    var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
                    if (status != PermissionStatus.Granted)
                    {
                        permissionsNeededList.Add(permission);
                    }
                }
            }
            catch (Exception ex)
            {
            }

            var results = await CrossPermissions.Current.RequestPermissionsAsync(permissionsNeededList.ToArray());

            try
            {
                foreach (var permission in permissionsNeededList)
                {
                    var status = PermissionStatus.Unknown;
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(permission))
                        status = results[permission];
                    if (status == PermissionStatus.Granted || status == PermissionStatus.Unknown)
                    {
                        permissionsGranted = true;
                    }
                    else
                    {
                        permissionsGranted = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            //return permissionsGranted;
        }
    }
}
