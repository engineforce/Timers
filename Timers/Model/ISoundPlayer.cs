using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomTimers.Model
{
    public interface ISoundPlayer
    {
        string FileName { get; set; }
        void Play();
        void Stop();
    }
}
