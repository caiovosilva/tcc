using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Acr.UserDialogs;
using System.Threading.Tasks;

namespace BipTranslator.ViewModels
{
    class ListeningViewModel : BaseViewModel
    {
        public ListeningViewModel(/*IUserDialogs dialogs*/)
        {
            TryToGetPermissionsAsync();
            //this.Dialogs = dialogs;
            Title = "Clica aê cabra!";
        }

        protected IUserDialogs Dialogs { get; }

        private async Task TryToGetPermissionsAsync()
        {
            //try
            //{
            //    var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            //    if (status != PermissionStatus.Granted)
            //    {
            //        if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
            //        {
            //            await this.Dialogs.AlertAsync("need permissions");
            //        }

            //        var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
            //        status = results[Permission.Location];
            //    }

            //    if (status == PermissionStatus.Granted)
            //    {
            //        var results = await CrossGeolocator.Current.GetPositionAsync(10000);
            //        LabelGeolocation.Text = "Lat: " + results.Latitude + " Long: " + results.Longitude;
            //    }
            //    else if (status != PermissionStatus.Unknown)
            //    {
            //        await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
            //    }
            //}
            //catch (Exception ex)
            //{

            //    LabelGeolocation.Text = "Error: " + ex;
            //}
            bool permissionsGranted = true;

            var permissionsStartList = new List<Permission>()
        {
            Permission.Location,
            Permission.LocationAlways,
            Permission.LocationWhenInUse,
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
