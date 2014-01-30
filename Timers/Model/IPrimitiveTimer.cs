using System;
using System.Collections.Generic;
using System.Text;

namespace CustomTimers.Model
{
    public interface IPrimitiveTimer
    {
        double Interval
        {
            get;
            set;
        }

        void Start ();
        void Stop ();

        event EventHandler Elapsed;
    }
}
