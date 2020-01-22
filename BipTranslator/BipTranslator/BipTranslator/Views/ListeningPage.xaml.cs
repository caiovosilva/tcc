using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.ComponentModel;
using Xamarin.Forms.Xaml;
using Plugin.AudioRecorder;
using BipTranslator.Interfaces;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using PCLStorage;
using MediaManager;

namespace BipTranslator.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListeningPage : ContentPage
    {
        AudioRecorderService recorder;
        public ListeningPage()
        {
            InitializeComponent();
			recorder = DependencyService.Get<IAudioRecorder>().GetAudioRecorder();
        }

		async void Record_Clicked(object sender, EventArgs e)
		{
			await RecordIfPermissionsGranted();	
		}

		async Task RecordIfPermissionsGranted()
		{
			var statusStorage = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
			var statusMicrophone = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Microphone);
			if (statusStorage == PermissionStatus.Granted && statusMicrophone == PermissionStatus.Granted)
			{
				await RecordAudio();
			}
			else
			{
				await TryToGetPermissionsAsync();
				await RecordIfPermissionsGranted();

			}
		}

		async Task RecordAudio()
		{
			try
			{
				if (!recorder.IsRecording) //Record button clicked
				{
					recorder.StopRecordingOnSilence = false;

					RecordButton.IsEnabled = false;
					PlayButton.IsEnabled = false;

					//start recording audio
					var audioRecordTask = await recorder.StartRecording();

					RecordButton.Text = "Stop Recording";
					RecordButton.IsEnabled = true;

					var file = await audioRecordTask;

					RecordButton.Text = "Record";
					PlayButton.IsEnabled = true;
				}
				else //Stop button clicked
				{
					RecordButton.IsEnabled = false;

					//stop the recording...
					await recorder.StopRecording();

					RecordButton.IsEnabled = true;
				}
			}
			catch (Exception ex)
			{
				//blow up the app!
				throw ex;
			}
		}

		void Play_Clicked(object sender, EventArgs e)
		{
			PlayAudio();
		}

		async void PlayAudio()
		{
			try
			{
				var filePath = recorder.GetAudioFilePath();
				if (filePath != null)
				{
					PlayButton.IsEnabled = false;
					RecordButton.IsEnabled = false;
					await CrossMediaManager.Current.Play(filePath);
					PlayButton.IsEnabled = true;
					RecordButton.IsEnabled = true;
				}
			}
			catch (Exception ex)
			{
				//blow up the app!
				throw ex;
			}
		}

		private async Task<bool> TryToGetPermissionsAsync()
		{
			bool permissionsGranted = true;

			var permissionsStartList = new List<Permission>()
			{
				Permission.Storage,
				Permission.Microphone,
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
			return permissionsGranted;
		}

		//private async Task CopyAudioAsync(string audioFile)
		//{
		//	IFileSystem fileSystem = FileSystem.Current;
		//	IFolder rootFolder = fileSystem.RoamingStorage;
		//	try
		//	{
		//		IFile pickedAudio = await fileSystem.GetFileFromPathAsync(audioFile);
		//		await pickedAudio.MoveAsync(rootFolder.Path, NameCollisionOption.ReplaceExisting);
		//	}
		//	catch (Exception ex)
		//	{
		//		await DisplayAlert("Copy Audio", ex.Message, "OK");
		//	}
		//}
	}
}