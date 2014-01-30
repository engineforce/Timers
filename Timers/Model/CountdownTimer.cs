using System;
using System.Threading;

namespace CustomTimers.Model
{
    public class CountdownTimer : CustomTimer
    {
        private int _hourLatch;
        private int _minuteLatch;
        private int _secondLatch;
        private int _millisecondLatch;

        //-----------------------------------------------
        // Private member functions
        //-----------------------------------------------
        private int _updateInternalTimeRunning;
        private void UpdateInternalTime ()
        {
            // Prevent Reentry
            if (Interlocked.Exchange (ref _updateInternalTimeRunning, 1) == 0)
            {
                //DateTime currentTime = DateTime.Now;

                //_Millisecond -= (currentTime.Millisecond - _SystemTimeLatched.Millisecond);
                //_Second -= (currentTime.Second - _SystemTimeLatched.Second);
                //_Minute -= (currentTime.Minute - _SystemTimeLatched.Minute);
                //_Hour -= (currentTime.Hour - _SystemTimeLatched.Hour);

                //_SystemTimeLatched = DateTime.Now;

                _DiagnosticsStopWatch.Stop ();
                TimeSpan timeElapsed = _DiagnosticsStopWatch.Elapsed;

                long totalMilliseconds = Convert.ToInt64 (timeElapsed.TotalMilliseconds);
                long intervalInMs = GetIntervalInMilliseconds ();

                long totalTime = Convert.ToInt64 (
                    (double) totalMilliseconds / (double) intervalInMs)
                    * intervalInMs;

                _Millisecond = _millisecondLatch - (int) (totalTime % 1000);
                totalTime = totalTime / 1000;

                _Second = _secondLatch - (int) (totalTime % 60);
                totalTime = totalTime / 60;

                _Minute = _minuteLatch - (int) (totalTime % 60);
                totalTime = totalTime / 60;

                _Hour = _hourLatch - (int) (totalTime % 60);

                FindEffectivTime ();

                if (_Millisecond == 0 && _Second == 0 &&
                        _Minute == 0 && _Hour == 0)
                {
                    if (State != TimerStates.Stopped)
                    {
                        _Timer.Stop ();
                        _IsCompleted = true;
                    }
                }
                else
                {
                    _DiagnosticsStopWatch.Start ();
                }

                Interlocked.Exchange (ref _updateInternalTimeRunning, 0);
            }
        }

        //-----------------------------------------------
        // Public member function
        //-----------------------------------------------
        public CountdownTimer ()
        {
            // This will automatically call the default constructor 
            // of parent class first
        }

        public CountdownTimer (
            int hour, int minute, int second, int millisecond, int interval,
            IntervalUnits intervalUnit, bool useHighResolution)
            :
            base (hour, minute, second, millisecond, interval,
            intervalUnit, useHighResolution)
        {

        }

        public override void Start ()
        {
            if (_Millisecond > 0 || _Second > 0 ||
                    _Minute > 0 || _Hour > 0)
            {
                if (State != TimerStates.Paused)
                {
                    _hourLatch = _Hour;
                    _minuteLatch = _Minute;
                    _secondLatch = _Second;
                    _millisecondLatch = _Millisecond;
                }

                base.Start ();
            }
        }

        //-----------------------------------------------
        // Generated Events
        //-----------------------------------------------
        protected override void OnElapsed (EventArgs e)
        {
            UpdateInternalTime ();
            base.OnElapsed (e);

            if (_IsCompleted)
            {
                OnCompleted (e);
            }
        }
    }
}
