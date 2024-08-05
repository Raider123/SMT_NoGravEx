using System;
using System.Diagnostics;
using System.Threading;
using LSL;
using Waveplus.DaqExample;

namespace Waveplus_Daq_Example
{
    internal class Sender
    {
        // The EMG example program
        private WaveplusDaqExampleForm wdaq;

        // LSL related fields
        private StreamOutlet outlet;

        public Sender()
        {
            wdaq = new WaveplusDaqExampleForm();
            InitializeLSL();
        }

        private void InitializeLSL()
        {
            // Erstellen von StreamInfo für den Stream
            StreamInfo streamInfo = new StreamInfo("ExampleStream", "EMG", 1, 100, channel_format_t.cf_double64, "myuid34234");

            // Erstellen eines StreamOutlet
            outlet = new StreamOutlet(streamInfo);
        }

        private void SendEMGData(double current_emg_samples)
        {
            // Erstellen eines Arrays für den zu sendenden Wert
            double[] sample = new double[1] { current_emg_samples };

            // Senden der EMG-Daten
            outlet.push_sample(sample);
        }

        private void Update()
        {
            double current_emg_samples = wdaq.current_emg_sample;
            Debug.Write(current_emg_samples.ToString());
            SendEMGData(current_emg_samples);
        }

        public void Run()
        {
            // Starte die Endlosschleife
            while (true)
            {
                Update();

                // Kurze Pause, um CPU-Überlastung zu vermeiden (z.B. 10ms)
                Thread.Sleep(10);
            }
        }
    }
}
