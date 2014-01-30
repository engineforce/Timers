using System;
using System.Threading;

namespace CustomTimers.Model
{
    class StopWatchTimer : CustomTimer
    {
        //-----------------------------------------------
        // Private member functions
        //-----------------------------------------------
        private int UpdateInternalTimeRunning = 0;
        private void UpdateInternalTime ()
        {
            // Prevent Reentry
            if (Interlocked.Exchange (ref UpdateInternalTimeRunning, 1) == 0)
            {
                //DateTime currentTime = DateTime.Now;

                //_Millisecond += (currentTime.Millisecond - _SystemTimeLatched.Millisecond);
                //_Second += (currentTime.Second - _SystemTimeLatched.Second);
                //_Minute += (currentTime.Minute - _SystemTimeLatched.Minute);
                //_Hour += (currentTime.Hour - _SystemTimeLatched.Hour);

                //_SystemTimeLatched = DateTime.Now;

                _DiagnosticsStopWatch.Stop ();

                TimeSpan timeElapsed = _DiagnosticsStopWatch.Elapsed;

                long totalMilliseconds = Convert.ToInt64 (timeElapsed.TotalMilliseconds);
                long intervalInMs = GetIntervalInMilliseconds ();

                long totalTime = Convert.ToInt64 (
                    (double) totalMilliseconds / (double) intervalInMs)
                    * intervalInMs;

                _Millisecond = (int) (totalTime % 1000);
                totalTime = totalTime / 1000;

                _Second = (int) (totalTime % 60);
                totalTime = totalTime / 60;

                _Minute = (int) (totalTime % 60);
                totalTime = totalTime / 60;

                _Hour = (int) totalTime;

                //FindEffectivTime ();

                _DiagnosticsStopWatch.Start ();

                Interlocked.Exchange (ref UpdateInternalTimeRunning, 0);
            }
        }

        //-----------------------------------------------
        // Public member function
        //-----------------------------------------------
        public StopWatchTimer ()
        {
            // This will automatically call the default constructor 
            // of parent class first
        }

        //-----------------------------------------------
        // Generated Events
        //-----------------------------------------------
        protected override void OnElapsed (EventArgs e)
        {
            UpdateInternalTime ();
            base.OnElapsed (e);
        }
    }
}
