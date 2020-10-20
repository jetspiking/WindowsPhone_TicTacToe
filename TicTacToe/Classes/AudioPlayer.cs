using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace TicTacToe.Classes
{
    public class AudioPlayer
    {
        public static MediaPlayer mediaPlayer = new MediaPlayer();
        public static void PlayClick()
        {
            if (AppDesign.enableGameAudio)
                PlayAudio("Click.wav");
        }

        private static void PlayAudio(String audioFile)
        {
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri($"{AppDesign.APP_AUDIO_URI_PATH}/{audioFile}", UriKind.RelativeOrAbsolute));
            mediaPlayer.Play();
        }
    }
}
