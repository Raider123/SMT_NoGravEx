using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LSL;
using Waveplus.DaqExample;

namespace Waveplus_Daq_Example
{
    internal class Sender : Form
    {
        // The EMG example program
        private WaveplusDaqExampleForm wdaq;

        // LSL related fields
        private StreamOutlet outlet;
        private Button startButton;

        // Cancellation token to stop the sending loop
        private CancellationTokenSource cancellationTokenSource;

        public Sender()
        {
            wdaq = new WaveplusDaqExampleForm();
            InitializeLSL();
            InitializeComponent();
        }

        private void InitializeLSL()
        {
            // Erstellen von StreamInfo für den Stream mit mehrkanaligem Sample
            StreamInfo streamInfo = new StreamInfo("ExampleStream", "EMG", 8, 100, channel_format_t.cf_double64, "myuid34234");

            // Erstellen eines StreamOutlet
            outlet = new StreamOutlet(streamInfo);
        }

        private void InitializeComponent()
        {
            // Initialize the form components
            this.startButton = new Button();
            this.SuspendLayout();

            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(100, 50);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(100, 50);
            this.startButton.Text = "Senden Beginnen";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new EventHandler(StartButton_Click);

            // 
            // Sender
            // 
            this.ClientSize = new System.Drawing.Size(300, 150);
            this.Controls.Add(this.startButton);
            this.Name = "Sender";
            this.Text = "EMG Sender";
            this.FormClosing += new FormClosingEventHandler(Sender_FormClosing);
            this.ResumeLayout(false);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (cancellationTokenSource == null || cancellationTokenSource.IsCancellationRequested)
            {
                cancellationTokenSource = new CancellationTokenSource();
                Task.Run(() => Run(cancellationTokenSource.Token));
            }
        }

        private void Run(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    Update();

                    // Kurze Pause, um CPU-Überlastung zu vermeiden (z.B. 10ms)
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Senden der Daten: {ex.Message}", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Update()
        {
            if (wdaq != null)
            {
                double[] current_emg_samples = wdaq.rec_emg_samples; // Angenommen, wdaq liefert nun ein Double-Array

                Debug.Write(current_emg_samples.ToString());

                SendEMGData(current_emg_samples);
            }
        }

        private void SendEMGData(double[] current_emg_samples)
        {
            // Senden der EMG-Daten als Array
            outlet.push_sample(current_emg_samples);
        }

        private void Sender_FormClosing(object sender, FormClosingEventArgs e)
        {
            cancellationTokenSource?.Cancel();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Sender());
        }
    }
}
