using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Waveplus_Daq_Example;

namespace Waveplus.DaqExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Sender sender = new Sender();
            sender.Run();
        }
    }

}

