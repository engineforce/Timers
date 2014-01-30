using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using CustomTimers.Common;
using CustomTimers.Properties;
using CustomTimers.View;

namespace CustomTimers
{
    public partial class TimerForm : Form, IStatusReporter
    {
        //-----------------------------------------------
        // Private data members
        //-----------------------------------------------
        private const string STATE_FILE_PATH = "State.xml";
        private readonly MessageBoxForm _messageBoxForm = new MessageBoxForm();

        //-----------------------------------------------
        // Public member functions
        //-----------------------------------------------
        public TimerForm ()
        {
            InitializeComponent();

            _messageBoxForm.StartPosition = FormStartPosition.CenterParent;

            ucCountdown.TimerCompleted += new EventHandler(UcCountdownTimerCompleted);
            ucCountdown.TimerStarted += new EventHandler(UcCountdownTimerStarted);
            ucCountdown.StatusReporter = this;
            ucStopWatch.StatusReporter = this;
        }

        private void SaveState ()
        {
            try
            {
                var state = new TimeFormState();

                state.CountDownTimerState = ucCountdown.CreateMemento();
                state.StopWatchTimerState = ucStopWatch.CreateMemento();
                state.TopMost = cbxIsTopMost.Checked;
                state.SelectedTabIndex = tabTimer.SelectedIndex;

                using (var streamWriter = new StreamWriter(STATE_FILE_PATH))
                {
                    var xmlSerializer = new XmlSerializer(typeof(TimeFormState));
                    xmlSerializer.Serialize(streamWriter, state);
                }
                
                ReportStatus(string.Format("Current state has been saved successfully to {0}.", STATE_FILE_PATH));
            }
            catch (Exception ex)
            {
                ReportStatus(ex.Message);
            }
        }
         
        private void LoadState (bool reportError = true)
        {
            try
            {
                TimeFormState state = null;

                using (var streamReader = new StreamReader(STATE_FILE_PATH))
                {
                    var xmlSerializer = new XmlSerializer(typeof(TimeFormState));
                    state = xmlSerializer.Deserialize(streamReader) as TimeFormState;
                }

                if (state != null)
                {
                    ucCountdown.SetMemento(state.CountDownTimerState);
                    ucStopWatch.SetMemento(state.StopWatchTimerState);
                    cbxIsTopMost.Checked = (bool)state.TopMost;
                    tabTimer.SelectedIndex = state.SelectedTabIndex;
                }
            }
            catch (Exception ex)
            {
                if (reportError)
                {
                    ReportStatus(ex.Message);
                }
            }
        }

        //-----------------------------------------------
        // Event Handlers
        //-----------------------------------------------

        private void TimerFormResize (object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                this.Hide();
                nicoMain.Text = string.Format(
                    "{0}: {1}", ucCountdown.TimerState, ucCountdown.Message);
            }
        }

        private void NicoMainDoubleClick (object sender, EventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal;
        }

        private void AboutToolStripMenuItemClick (object sender, EventArgs e)
        {
            // Show Message
            _messageBoxForm.Message = string.Format(
                "\n" +
                "Author: Paul Li\n" +
                "Date Updated: 1st May 2012 \n" +
                "Email: pong_ho80@hotmail.com \n");
            _messageBoxForm.ShowDialog(this);
        }

        private void UcCountdownTimerCompleted (object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.TopMost = true;

            // Update Tray Icon message
            nicoMain.Text = string.Format(
                "{0}: {1}", ucCountdown.TimerState, ucCountdown.Message);

            // Show Message
            _messageBoxForm.Message = string.Format("{0}", ucCountdown.Message);
            _messageBoxForm.ShowDialog(this);
            this.TopMost = false;
        }

        private void UcCountdownTimerStarted (object sender, EventArgs e)
        {
            nicoMain.Text = string.Format(
                "{0}: {1}", ucCountdown.TimerState, ucCountdown.Message);
        }

        private void CbxIsTopMostCheckedChanged (object sender, EventArgs e)
        {
            this.TopMost = cbxIsTopMost.Checked;
        }

        private void TimerFormFormClosed (object sender, FormClosedEventArgs e)
        {
            nicoMain.Dispose();
        }

        private void TimerFormLoad (object sender, EventArgs e)
        {
            // tabTimer.SelectedIndex = 1;
            this.TopMost = cbxIsTopMost.Checked;

            if (Settings.Default.LoadSettingsAtStart)
            {
                LoadState(false);
            }
        }

        private void ExitToolStripMenuItemClick (object sender, EventArgs e)
        {
            Close();
        }

        private void saveToolStripMenuItem_Click (object sender, EventArgs e)
        {
            SaveState();
        }

        private void loadToolStripMenuItem_Click (object sender, EventArgs e)
        {
            LoadState();
        }

        private void resetStateToolStripMenuItem_Click (object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(STATE_FILE_PATH))
                {
                    File.Delete(STATE_FILE_PATH);
                }

                ReportStatus("Saved state has been removed successfully.");
            }
            catch (Exception ex)
            {
                ReportStatus(ex.Message);
            }
        }

        public void ReportStatus (string status)
        {
            lblStatus.Text = status;
        }
    }

    [XmlInclude(typeof(List<KeyedElement>))]
    public class TimeFormState
    {
        public object CountDownTimerState { get; set; }
        public object StopWatchTimerState { get; set; }

        public bool TopMost { get; set; }
        public int SelectedTabIndex { get; set; }
    }
}
