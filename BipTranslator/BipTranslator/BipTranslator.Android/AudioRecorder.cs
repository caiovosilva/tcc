using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BipTranslator.Interfaces;
using Plugin.AudioRecorder;
using Xamarin.Essentials;

[assembly: Xamarin.Forms.Dependency(typeof(BipTranslator.Droid.AudioRecorder))]
namespace BipTranslator.Droid
{
    class AudioRecorder : IAudioRecorder
    {
        public AudioRecorderService GetAudioRecorder()
        {
            return new AudioRecorderService
            {
                StopRecordingAfterTimeout = false,
                StopRecordingOnSilence = false,
                TotalAudioTimeout = TimeSpan.FromSeconds(15),
                AudioSilenceTimeout = TimeSpan.FromSeconds(2)
            };
        }
    }
}