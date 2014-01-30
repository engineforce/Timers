using System.IO;
using WMPLib;

namespace CustomTimers.Model
{
    public class WindowsSoundPlayer : ISoundPlayer
    {
        private WindowsMediaPlayer _player = new WindowsMediaPlayer();

        public string FileName { get; set; }

        public void Play ()
        {
            if (File.Exists(FileName))
            {
                _player.URL = FileName;
                _player.controls.play();
            }
        }

        public void Stop()
        {
            _player.controls.stop();
        }
    }
}