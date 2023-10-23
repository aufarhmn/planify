using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planify
{
    internal class Pomodoro
    {
        private DateTime _startTime;
        private DateTime _endTime;
        private int _interval;
        private int _timeRest;

        public DateTime startTime
        {
            get { return _startTime; }
        }
        public DateTime endTime
        {
            get { return _endTime; }
        }
        public int interval
        {
            get { return _interval; }
        }
        public int timeRest
        {
            get { return _timeRest; }
        }
    }
}
