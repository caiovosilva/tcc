using Plugin.AudioRecorder;
using System;
using System.Collections.Generic;
using System.Text;

namespace BipTranslator.Interfaces
{
    public interface IAudioRecorder
    {
        AudioRecorderService GetAudioRecorder();
    }
}
