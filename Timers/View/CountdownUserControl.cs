using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using CustomTimers.Common;
using CustomTimers.Model;
using System.Linq;

namespace CustomTimers.View
{
    public partial class CountdownUserControl : UserControl, IMementoContainer
    {
        //-----------------------------------------------
        // Private data members
        //-----------------------------------------------

        private readonly CustomTimer _countdownTimer = new CountdownTimer();
        private readonly WindowsSoundPlayer _player = new WindowsSoundPlayer();
        private bool _isStopClicked;

        //-----------------------------------------------
        // Public properties
        //-----------------------------------------------
        public CustomTimer.TimerStates TimerState
        {
            get { return _countdownTimer.State; }
        }

        public string Message
        {
            get { return txtMessage.Text; }
        }

        public IStatusReporter StatusReporter { get; set; }

        //-----------------------------------------------
        // Private member functions
        //-----------------------------------------------
        private void Init ()
        {
            _countdownTimer.Elapsed += CustomTimerElapsed;
            _countdownTimer.Completed += CustomTimerCompleted;
            _countdownTimer.StateChanged += CountdownTimerStateChanged;

            _countdownTimer.SynchronizingObject = this;
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

        private void UpdateLabels ()
        {
            if (!_isStopClicked)
            {
                lblHour.Text =
                    string.Format("{0:00}", _countdownTimer.Hour);
                lblMinute.Text =
                    string.Format("{0:00}", _countdownTimer.Minute);
                lblSecond.Text =
                    string.Format("{0:00}", _countdownTimer.Second);
                lblMillisecond.Text =
                    string.Format("{0:000}", _countdownTimer.Millisecond);
            }
        }

        private void UpdateInputs ()
        {
            cbxIntervalUnit.DataSource = Enum.GetValues(
                typeof(CustomTimer.IntervalUnits));
            txtInterval.Text = string.Format("{0}", _countdownTimer.Interval);
            txtHour.Text = string.Format("{0:00}", _countdownTimer.Hour);
            txtMinute.Text = string.Format("{0:00}", _countdownTimer.Minute);
            txtSecond.Text = string.Format("{0:00}", _countdownTimer.Second);
            txtMillisecond.Text = string.Format(
                "{0:000}", _countdownTimer.Millisecond);
            cbxHighResolution.Checked = _countdownTimer.UseHighResolution;
        }

        private void UpdateButtons ()
        {
            btnStart.Enabled = true;
            btnStop.Enabled = true;
            btnPause.Enabled = true;
            btnReset.Enabled = true;
            switch (_countdownTimer.State)
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

        private void PlaySimpleSound ()
        {
            if (txtSound.Text == null || !cbxSoundEnabled.Checked)
                return;

            try
            {
                _player.FileName = txtSound.Text;
                _player.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Reset ()
        {
            _countdownTimer.Reset();
            UpdateStatus();
            UpdateLabels();
            UpdateInputs();
            UpdateButtons();
        }

        private void Cleanup ()
        {
            _player.Stop();
        }

        //-----------------------------------------------
        // Public member functions
        //-----------------------------------------------
        public CountdownUserControl ()
        {
            InitializeComponent();
            Init();
            Reset();
        }

        public object CreateMemento ()
        {
            var memento =
                new List<KeyedElement>
                {
                    new KeyedElement("IntervalUnit", cbxIntervalUnit.SelectedValue.ToString()),
                    new KeyedElement("Interval", txtInterval.Text),
                    new KeyedElement("Hour", txtHour.Text),
                    new KeyedElement("Minute", txtMinute.Text),
                    new KeyedElement("Second", txtSecond.Text),
                    new KeyedElement("Millisecond", txtMillisecond.Text),
                    new KeyedElement("HighResolution", cbxHighResolution.Checked.ToString()),
                    new KeyedElement("Message", txtMessage.Text),
                    new KeyedElement("Sound", txtSound.Text),
                    new KeyedElement("SoundEnabled", cbxSoundEnabled.Checked.ToString())
                };

            return memento;
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
                    txtHour.Text = memento["Hour"];
                    txtMinute.Text = memento["Minute"];
                    txtSecond.Text = memento["Second"];
                    txtMillisecond.Text = memento["Millisecond"];
                    cbxHighResolution.Checked = Convert.ToBoolean(memento["HighResolution"]);
                    txtMessage.Text = memento["Message"];
                    txtSound.Text = memento["Sound"];
                    cbxSoundEnabled.Checked = Convert.ToBoolean(memento["SoundEnabled"]);

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
        public event EventHandler TimerCompleted;
        protected virtual void OnTimerCompleted (EventArgs e)
        {
            TimerCompleted(this, e);
        }

        public event EventHandler TimerStarted;
        protected virtual void OnTimerStarted (EventArgs e)
        {
            TimerStarted(this, e);
        }

        //-----------------------------------------------
        // Event Handlers
        //-----------------------------------------------
        private void CustomTimerElapsed (object sender, EventArgs e)
        {
            UpdateLabels();
        }

        private void CustomTimerCompleted (object sender, EventArgs e)
        {
            PlaySimpleSound();
            OnTimerCompleted(e);
        }

        private void CountdownTimerStateChanged (object sender, EventArgs e)
        {
            UpdateButtons();
        }


        private void BtnStartClick (object sender, EventArgs e)
        {
            try
            {
                if (_countdownTimer.State != CustomTimer.TimerStates.Paused)
                {
                    _countdownTimer.Hour = Convert.ToInt32(txtHour.Text);
                    _countdownTimer.Minute = Convert.ToInt32(txtMinute.Text);
                    _countdownTimer.Second = Convert.ToInt32(txtSecond.Text);
                    _countdownTimer.Millisecond =
                        Convert.ToInt32(txtMillisecond.Text);
                    _countdownTimer.Interval =
                        Convert.ToInt32(txtInterval.Text);
                    _countdownTimer.IntervalUnit =
                        (CustomTimer.IntervalUnits)Enum.Parse(
                        typeof(CustomTimer.IntervalUnits),
                        cbxIntervalUnit.Text);

                    _countdownTimer.FindEffectivTime();

                    UpdateStatus();
                    UpdateLabels();
                }

                _isStopClicked = false;
                _countdownTimer.Start();
                OnTimerStarted(new EventArgs());
            }
            catch (Exception ex)
            {
                ReportStatus(ex.Message);
            }
        }

        private void BtnPauseClick (object sender, EventArgs e)
        {
            _countdownTimer.Pause();
        }

        private void BtnStopClick (object sender, EventArgs e)
        {
            _isStopClicked = true;
            _player.Stop();
            _countdownTimer.Stop();
        }

        private void BtnResetClick (object sender, EventArgs e)
        {
            _isStopClicked = false;
            _player.Stop();
            Reset();
        }

        private void CbxHighResolutionCheckedChanged (object sender, EventArgs e)
        {
            _countdownTimer.UseHighResolution = cbxHighResolution.Checked;
        }

        private void BtnBrowseSoundClick (object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtSound.Text))
            {
                dlgSound.InitialDirectory = Path.GetDirectoryName(txtSound.Text);
            }

            var dialogResult = dlgSound.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                txtSound.Text = dlgSound.FileName;
            }
        }

        protected override void OnCreateControl ()
        {
            base.OnCreateControl();

            if (ParentForm != null)
            {
                ParentForm.FormClosing += ParentFormFormClosing;
            }
        }

        private void ParentFormFormClosing (object sender, FormClosingEventArgs e)
        {
            Cleanup();
        }

        private void cbxSoundEnabled_CheckedChanged (object sender, EventArgs e)
        {
            txtSound.Enabled = cbxSoundEnabled.Checked;
        }

    }
}
