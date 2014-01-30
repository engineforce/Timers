using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Threading;

namespace CustomTimers.Model
{
    public class SystemTimer : IPrimitiveTimer
    {
        //-----------------------------------------------
        // Private data members
        //-----------------------------------------------
        private System.Timers.Timer _Timer = new System.Timers.Timer ();

        //-----------------------------------------------
        // Public properties
        //-----------------------------------------------
        public double Interval
        {
            get
            {
                return _Timer.Interval;
            }
            set
            {
                _Timer.Interval = value;
            }
        }

        //-----------------------------------------------
        // Public member function
        //-----------------------------------------------
        public SystemTimer ()
        {
            _Timer.Elapsed += new ElapsedEventHandler (_Timer_Elapsed);
        }
       
        public virtual void Start ()
        {
            Console.WriteLine ("System Timer started");
            _Timer.Start ();
        }

        public virtual void Stop ()
        {
            _Timer.Stop ();
        }

        //-----------------------------------------------
        // Generated Events
        //-----------------------------------------------
        public event EventHandler Elapsed;
        protected virtual void OnElapsed (EventArgs e)
        {
            Elapsed (this, e);
        }

        //-----------------------------------------------
        // Event Handler
        //-----------------------------------------------
        private int _Timer_ElapsedRunning = 0;
        void _Timer_Elapsed (object sender, ElapsedEventArgs e)
        {            
            // Prevent Reentry
            if (Interlocked.Exchange (ref _Timer_ElapsedRunning, 1) == 0)
            {
                OnElapsed (new EventArgs ());
                Interlocked.Exchange (ref _Timer_ElapsedRunning, 0);
            }
            //else
            //{
            //    Console.WriteLine (string.Format (
            //        "{0}: {1} _Timer_Elapsed was denied",
            //        DateTime.Now, Thread.CurrentThread.ManagedThreadId));
            //}
        }
    }
}
