using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace CustomTimers.Model
{
    public class QueueTimer : IDisposable, IPrimitiveTimer
    {
        //-----------------------------------------------
        // PInvoke methods and struct
        //-----------------------------------------------
        public delegate void WaitOrTimerDelegate (
            IntPtr Parameter, Int32 TimerOrWaitFired);


        public enum WtFlags
        {
            WT_EXECUTEDEFAULT = 0x00000000,
            WT_EXECUTEINTIMERTHREAD = 0x00000020,
            WT_EXECUTEINIOTHREAD = 0x00000001,
            WT_EXECUTEINPERSISTENTTHREAD = 0x00000080,
            WT_EXECUTELONGFUNCTION = 0x00000010,
            WT_EXECUTEONLYONCE = 0x00000008,
            WT_TRANSFER_IMPERSONATION = 0x00000100
        };

        [DllImport ("kernel32.dll")]
        static extern IntPtr CreateTimerQueue ();

        [DllImport ("kernel32.dll")]
        static extern bool CreateTimerQueueTimer (
            out IntPtr phNewTimer, IntPtr TimerQueue,
            WaitOrTimerDelegate Callback, IntPtr Parameter, uint DueTime,
            uint Interval, uint Flags);

        [DllImport ("kernel32.dll")]
        static extern bool ChangeTimerQueueTimer (
            IntPtr TimerQueue, IntPtr Timer, uint DueTime, uint Interval);

        [DllImport ("kernel32.dll")]
        static extern bool DeleteTimerQueueTimer (
            IntPtr TimerQueue, IntPtr Timer, IntPtr CompletionEvent);

        [DllImport ("kernel32.dll")]
        static extern bool DeleteTimerQueueEx (
            IntPtr TimerQueue, IntPtr CompletionEvent);

        

        //-----------------------------------------------
        // Private data members
        //-----------------------------------------------
        private IntPtr _HTimerQueue = IntPtr.Zero;
        private IntPtr _HQueueTimer = IntPtr.Zero;
        // Track whether Dispose has been called.
        private bool _IsDisposed = false;
        private WaitOrTimerDelegate _WaitOrTimerDelegate;
        private double _Interval = 1000.0;

        //-----------------------------------------------
        // Public properties
        //-----------------------------------------------
        public double Interval
        {
            get { return _Interval; }
            set { _Interval = value; }
        }

        //-----------------------------------------------
        // Public member function
        //-----------------------------------------------
        public QueueTimer ()
        {
            _HTimerQueue = CreateTimerQueue ();

            if (_HTimerQueue == IntPtr.Zero)
            {
                throw new Exception ("Cannot create Timer Queue");
            }

            _WaitOrTimerDelegate = new WaitOrTimerDelegate (_QueueTimer_Elapsed);
        }

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose ()
        {
            Dispose (true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize (this);
        }

        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        private void Dispose (bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this._IsDisposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.                
                DeleteTimerQueueEx (_HTimerQueue, IntPtr.Zero);

                // Note disposing has been done.
                _IsDisposed = true;

            }
        }

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        ~QueueTimer ()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose (false);
        }


        public void Start ()
        {
            if (_HQueueTimer == IntPtr.Zero)
            {
                uint flags = (uint) WtFlags.WT_EXECUTEDEFAULT;

                Console.WriteLine ("Queue Timer started");

                CreateTimerQueueTimer (out _HQueueTimer,
                    _HTimerQueue, _WaitOrTimerDelegate, IntPtr.Zero,
                    (uint) _Interval, (uint) _Interval, flags);
            }
        }

        public void Stop ()
        {
            if (_HQueueTimer != IntPtr.Zero)
            {
                DeleteTimerQueueTimer (_HTimerQueue, _HQueueTimer, IntPtr.Zero);
                _HQueueTimer = IntPtr.Zero;
            }
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
        private int _QueueTimer_ElapsedRunning = 0;
        private void _QueueTimer_Elapsed (IntPtr Parameter, Int32 TimerOrWaitFired)
        {
            // Prevent Reentry
            if (Interlocked.Exchange (ref _QueueTimer_ElapsedRunning, 1) == 0)
            {
                OnElapsed (new EventArgs ());
                Interlocked.Exchange (ref _QueueTimer_ElapsedRunning, 0);
            }
            //else
            //{
            //    Console.WriteLine (string.Format (
            //        "{0}: {1} _QueueTimer_Elapsed is denied",
            //        DateTime.Now, Thread.CurrentThread.ManagedThreadId));
            //}
        }
    }
}
