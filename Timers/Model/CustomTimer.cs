using System;
using System.ComponentModel;
using System.Diagnostics;

namespace CustomTimers.Model
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CustomTimer
    {
        //-----------------------------------------------
        // Private data members
        //-----------------------------------------------
        public enum IntervalUnits { Millisecond, Second, Minute, Hour };
        public enum TimerStates { Stopped, Running, Paused };

        protected int _Hour = 0;
        protected int _Minute = 0;
        protected int _Second = 0;
        protected int _Millisecond = 0;
        protected int _Interval = 1000;
        protected IntervalUnits _IntervalUnit = IntervalUnits.Millisecond;
        protected TimerStates _State = TimerStates.Stopped;
        protected ISynchronizeInvoke _SynchronizingObject = null;
        protected bool _UseHighResolution = true;

        protected IPrimitiveTimer _Timer = null;
        protected bool _IsCompleted = false;
        //protected DateTime _SystemTimeLatched = DateTime.Now;
        protected Stopwatch _DiagnosticsStopWatch = new Stopwatch ();

        //-----------------------------------------------
        // Public properties
        //-----------------------------------------------
        public int Hour
        {
            get { return _Hour; }
            set { _Hour = value; }
        }

        public int Minute
        {
            get { return _Minute; }
            set { _Minute = value; }
        }

        public int Second
        {
            get { return _Second; }
            set { _Second = value; }
        }

        public int Millisecond
        {
            get { return _Millisecond; }
            set { _Millisecond = value; }
        }

        public int Interval
        {
            get { return _Interval; }
            set
            {
                if (value < 1)
                {
                    throw new Exception (string.Format (
                        "Invalid CustomTimer interval {0}, must be positive",
                        value));
                }
                _Interval = value;
                UpdateTimerProperty ();
            }
        }

        public IntervalUnits IntervalUnit
        {
            get { return _IntervalUnit; }
            set
            {
                _IntervalUnit = value;
                UpdateTimerProperty ();
            }
        }

        public TimerStates State
        {
            get { return _State; }
            set
            {
                _State = value;
                var e = new EventArgs ();
                OnStateChanged (e);
            }
        }

        public ISynchronizeInvoke SynchronizingObject
        {
            get { return _SynchronizingObject; }
            set { _SynchronizingObject = value; }
        }

        public bool UseHighResolution
        {
            get { return _UseHighResolution; }
            set { _UseHighResolution = value; }
        }


        //-----------------------------------------------
        // Private member functions
        //-----------------------------------------------
        private void UpdateTimerProperty ()
        {
            if (_Timer == null)
                return;

            long intervalInMs = GetIntervalInMilliseconds ();

            _Timer.Interval = intervalInMs;
        }

        //private void Init ()
        //{
        //    _Timer = CreateTimer ();
        //    _Timer.Elapsed += new EventHandler (_Timer_Elapsed);             
        //}

        private IPrimitiveTimer CreateTimer ()
        {
            if (UseHighResolution)
                return new QueueTimer ();

            return new SystemTimer ();
        }

        //-----------------------------------------------
        // Protected member function
        //-----------------------------------------------
        // Utility function for firing an event through the target.
        // It uses C#'s variable length parameter list support
        // to build the parameter list.
        // This functions presumes that the caller holds the object lock.
        // (This is because the event list is typically modified on the UI
        // thread, but events are usually raised on the worker thread.)
        protected void FireAsync (Delegate dlg, params object[] pList)
        {
            if (dlg != null)
            {
                _SynchronizingObject.BeginInvoke (dlg, pList);
            }
            else
            {
                throw new Exception (
                    "No synchronize object has be assigned. \n" +
                    "Cannot generate events");
            }
        }

        protected long GetIntervalInMilliseconds ()
        {
            long intervalInMs;

            switch (_IntervalUnit)
            {
                case IntervalUnits.Hour:
                    intervalInMs = _Interval * 60 * 60 * 1000;
                    break;
                case IntervalUnits.Minute:
                    intervalInMs = _Interval * 60 * 1000;
                    break;
                case IntervalUnits.Second:
                    intervalInMs = _Interval * 1000;
                    break;
                case IntervalUnits.Millisecond:
                    intervalInMs = _Interval;
                    break;
                default:
                    throw new Exception (string.Format (
                        "Unknown interval unit ({0})", _IntervalUnit));
            }

            return intervalInMs;
        }

        //-----------------------------------------------
        // Public member function
        //-----------------------------------------------
        public CustomTimer ()
        {
            //Init ();            
        }

        public CustomTimer (
            int hour, int minute, int second, int millisecond, int interval,
            IntervalUnits intervalUnit, bool useHighResolution)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
            Millisecond = millisecond;
            Interval = interval;
            IntervalUnit = intervalUnit;
            UseHighResolution = useHighResolution;

            //Init ();
        }

        public virtual void Reset ()
        {
            Hour = 0;
            Minute = 0;
            Second = 0;
            Millisecond = 0;
            Interval = 10;
            IntervalUnit = IntervalUnits.Millisecond;
        }

        public virtual void Start ()
        {
            if (State != TimerStates.Running)
            {
                _Timer = CreateTimer ();
                _Timer.Elapsed += TimerElapsed;

                UpdateTimerProperty ();               

                _Timer.Start ();

                //_SystemTimeLatched = DateTime.Now;

                if (State != TimerStates.Paused)
                    _DiagnosticsStopWatch.Reset ();

                _DiagnosticsStopWatch.Start ();

                _IsCompleted = false;
                State = TimerStates.Running;
            }
        }

        public virtual void Pause ()
        {
            if (_Timer != null)
                _Timer.Stop ();

            _DiagnosticsStopWatch.Stop ();
            State = TimerStates.Paused;
        }

        public virtual void Stop ()
        {
            if (_Timer != null)
                _Timer.Stop ();

            _DiagnosticsStopWatch.Stop ();
            State = TimerStates.Stopped;
            Reset ();
        }

        public virtual void FindEffectivTime ()
        {
            while (true)
            {
                if (_Millisecond >= 1000)
                {
                    _Millisecond -= 1000;
                    ++_Second;
                }

                if (_Second >= 60)
                {
                    _Second -= 60;
                    ++_Minute;
                }

                if (_Minute >= 60)
                {
                    _Minute -= 60;
                    ++_Hour;
                }

                // Break from the loop if all time values are with valid range
                if (_Millisecond < 1000 && _Second < 60 && _Minute < 60)
                {
                    break;
                }
            }

            while (true)
            {
                if (_Millisecond < 0)
                {
                    _Millisecond += 1000;
                    --_Second;
                }

                if (_Second < 0)
                {
                    _Second += 60;
                    --_Minute;
                }

                if (_Minute < 0)
                {
                    _Minute += 60;
                    --_Hour;
                }

                if (_Hour < 0)
                {
                    _Hour = 0;
                    _Minute = 0;
                    _Second = 0;
                    _Millisecond = 0;
                }

                // Break from the loop if all time values are positive
                if (_Millisecond >= 0 && _Second >= 0 &&
                    _Minute >= 0 && _Hour >= 0)
                {
                    break;
                }
            }
        }

        //-----------------------------------------------
        // Generated Events
        //-----------------------------------------------
        public event EventHandler Elapsed;
        protected virtual void OnElapsed (EventArgs e)
        {
            FireAsync (Elapsed, this, e);
        }

        public event EventHandler Completed;
        protected virtual void OnCompleted (EventArgs e)
        {
            State = TimerStates.Stopped;
            FireAsync (Completed, this, e);
        }

        public event EventHandler StateChanged;
        protected virtual void OnStateChanged (EventArgs e)
        {
            FireAsync (StateChanged, this, e);
        }

        //-----------------------------------------------
        // Event Handler
        //-----------------------------------------------
        private void TimerElapsed (object sender, EventArgs e)
        {
            OnElapsed (new EventArgs ());
        }


    }
}
