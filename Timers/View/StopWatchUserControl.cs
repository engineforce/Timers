using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CustomTimers.Common;
using CustomTimers.Model;

namespace CustomTimers.View
{
    public partial class StopWatchUserControl : UserControl, IMementoContainer
    {
        //-----------------------------------------------
        // Private data members
        //-----------------------------------------------
        private StopWatchTimer _StopWatch = new StopWatchTimer ();
        private bool _IsStopClicked = false;

        //-----------------------------------------------
        // Public properties
        //-----------------------------------------------

        public IStatusReporter StatusReporter { get; set; }

        //-----------------------------------------------
        // Private member functions
        //-----------------------------------------------
        private void Init ()
        {
            _StopWatch.SynchronizingObject = this;
            _StopWatch.Elapsed += new
                EventHandler (_StopWatch_Elapsed);

            _StopWatch.StateChanged += new
                EventHandler (_StopWatch_StateChanged);
        }

        private void UpdateStatus ()
        {
            ReportStatus("");
        }

        private void ReportStatus (string status)
        {
            if (StatusReporter != null)
            {
                StatusReporter.ReportStatus(status);
            }
        }

        private void Reset ()
        {
            _StopWatch.Reset ();
            UpdateStatus();
            UpdateLabels ();
            UpdateInputs ();
            UpdateButtons ();
        }

        private void UpdateLabels ()
        {
            if (!_IsStopClicked)
            {
                lblHour.Text =
                    string.Format ("{0:00}", _StopWatch.Hour);
                lblMinute.Text =
                    string.Format ("{0:00}", _StopWatch.Minute);
                lblSecond.Text =
                    string.Format ("{0:00}", _StopWatch.Second);
                lblMillisecond.Text =
                    string.Format ("{0:000}", _StopWatch.Millisecond);
            }
        }

        private void UpdateInputs ()
        {
            cbxIntervalUnit.DataSource = System.Enum.GetValues (
                typeof (CustomTimer.IntervalUnits));
            txtInterval.Text = string.Format ("{0}", _StopWatch.Interval);
            cbxHighResolution.Checked = _StopWatch.UseHighResolution;
        }

        private void UpdateButtons ()
        {
            btnStart.Enabled = true;
            btnStop.Enabled = true;
            btnPause.Enabled = true;
            btnReset.Enabled = true;
            switch (_StopWatch.State)
            {
                case CustomTimer.TimerStates.Stopped:
                    btnPause.Enabled = false;
                    break;
                case CustomTimer.TimerStates.Paused:
                    btnPause.Enabled = false;
                    btnReset.Enabled = false;
                    break;
                case CustomTimer.TimerStates.Running:
                    btnStart.Enabled = false;
                    btnReset.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        //-----------------------------------------------
        // Protected member function
        //-----------------------------------------------

        //-----------------------------------------------
        // Public member functions
        //-----------------------------------------------
        public StopWatchUserControl ()
        {
            InitializeComponent ();
            Init ();
            Reset ();
        }

        public object CreateMemento ()
        {
            var memento =
                new List<KeyedElement>
                {
                    new KeyedElement("IntervalUnit", cbxIntervalUnit.SelectedValue.ToString()),
                    new KeyedElement("Interval", txtInterval.Text),
                    new KeyedElement("HighResolution", cbxHighResolution.Checked.ToString()),
                    new KeyedElement("Message", txtMessage.Text)
                };

            return memento.ToList();
        }

        public void SetMemento (object mementoObject)
        {
            try
            {
                var mementoList = mementoObject as List<KeyedElement>;
                if (mementoList != null)
                {
                    var memento = mementoList.ToDictionary(k => k.Key, v => v.Value);

                    cbxIntervalUnit.SelectedItem = memento["IntervalUnit"];
                    txtInterval.Text = memento["Interval"];
                    cbxHighResolution.Checked = Convert.ToBoolean(memento["HighResolution"]);
                    txtMessage.Text = memento["Message"];

                    ReportStatus("Saved state has been restored successfully.");
                }
                else
                {
                    ReportStatus("Cannot load saved state.");
                }
            }
            catch (Exception ex)
            {
                ReportStatus(ex.Message);
            }
        }

        //-----------------------------------------------
        // Generated Events
        //-----------------------------------------------

        //-----------------------------------------------
        // Event Handlers
        //-----------------------------------------------       
        private void _StopWatch_Elapsed (
            object sender, EventArgs e)
        {
            UpdateLabels ();
        }

        private void _StopWatch_StateChanged (object sender, EventArgs e)
        {
            UpdateButtons ();
        }

        private void btnStart_Click (object sender, EventArgs e)
        {
            try
            {
                if (_StopWatch.State != CustomTimer.TimerStates.Paused)
                {
                    _StopWatch.Interval =
                            Convert.ToInt32 (txtInterval.Text);
                    _StopWatch.IntervalUnit =
                        (CustomTimer.IntervalUnits) Enum.Parse (
                        typeof (CustomTimer.IntervalUnits),
                        cbxIntervalUnit.Text);

                    UpdateStatus();
                    UpdateLabels ();
                }

                _IsStopClicked = false;
                _StopWatch.Start ();
            }
            catch (Exception ex)
            {
                ReportStatus(ex.Message);
            }
        }

        private void btnPause_Click (object sender, EventArgs e)
        {
            _StopWatch.Pause ();
        }
                
        private void btnStop_Click (object sender, EventArgs e)
        {
            _IsStopClicked = true;
            _StopWatch.Stop ();
        }

        private void btnReset_Click (object sender, EventArgs e)
        {
            _IsStopClicked = false;
            Reset ();
        }

        private void cbxHighResolution_CheckedChanged (object sender, EventArgs e)
        {
            _StopWatch.UseHighResolution = cbxHighResolution.Checked;
        }
        
    }
}
