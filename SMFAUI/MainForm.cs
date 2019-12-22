using STOTP;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace SMFAUI
{
    public partial class MainForm : Form
    {
        private TOTP _totp;
        private Timer _updateOtpTimer;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _totp = new TOTP(
                ConfigurationManager.AppSettings.Get("TotpKey"),
                Int32.Parse(ConfigurationManager.AppSettings.Get("OtpDigits") ?? TOTP.DefaultLength.ToString()),
                Int32.Parse(ConfigurationManager.AppSettings.Get("OtpWindow") ?? TOTP.DefaultTimeInterval.ToString()));
            this.Text = ConfigurationManager.AppSettings.Get("TotpAccountName") ?? "TOTP OTP";

            _updateOtpTimer = new Timer();
            _updateOtpTimer.Interval = 100;
            _updateOtpTimer.Tick += new EventHandler(UpdateOtp_Tick);
            _updateOtpTimer.Enabled = true;

            progressOtp.Minimum = 0;
            progressOtp.Maximum = _totp.Window;
        }

        private void UpdateOtp_Tick(object Sender, EventArgs e)
        {
            // Set the caption to the current time.
            textBoxOtp.Text = _totp.Otp;
            progressOtp.Value = _totp.SecondsRemaining;
        }

        private void buttonCopyOtp_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_totp.Otp);
        }
    }
}
