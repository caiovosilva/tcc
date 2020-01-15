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

namespace BipTranslator.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListeningPage : ContentPage
    {
        AudioRecorderService recorder;
        AudioPlayer player;
        public ListeningPage()
        {
            InitializeComponent();

			recorder = DependencyService.Get<IAudioRecorder>().GetAudioRecorder();

            player = new AudioPlayer();
            player.FinishedPlaying += Player_FinishedPlaying;
        }

		async void Record_Clicked(object sender, EventArgs e)
		{
			await RecordAudio();
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

		void PlayAudio()
		{
			try
			{
				var filePath = recorder.GetAudioFilePath();
				var streampath = recorder.GetAudioFileStream();
				if (filePath != null)
				{
					PlayButton.IsEnabled = false;
					RecordButton.IsEnabled = false;

					player.Play(filePath);
				}
			}
			catch (Exception ex)
			{
				//blow up the app!
				throw ex;
			}
		}

		void Player_FinishedPlaying(object sender, EventArgs e)
        {
            PlayButton.IsEnabled = true;
            RecordButton.IsEnabled = true;
        }
    }
}