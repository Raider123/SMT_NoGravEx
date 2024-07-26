using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waveplus_Daq_Example
{
    public class MovingAverage
    {
        private readonly int _k;
        private readonly double[] _values;

        private int _index = 0;
        private double _sum = 0;

        /// <summary>
        /// A very simple implementation of a moving average class.
        //  This class is not thread safe, does not handle overflow (summation) and 
        //  the current Moving-Average value can only be fetched when calling Update().
        /// </summary>
        /// <param name="k"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public MovingAverage(int k)
        {
            if (k <= 0) throw new ArgumentOutOfRangeException(nameof(k), "Must be greater than 0");

            _k = k;
            _values = new double[k];
        }

        public double Update(double nextInput)
        {
            // calculate the new sum
            _sum = _sum - _values[_index] + nextInput;

            // overwrite the old value with the new one
            _values[_index] = nextInput;

            // increment the index (wrapping back to 0) // Ringbuffered
            _index = (_index + 1) % _k;

            // calculate the average
            return _sum / _k;
        }
    }
}
