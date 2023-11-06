using Planify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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


/*class Program
{
    static void Main()
    {
        // Create an instance of the Dog class
        Task mytask =new Task("ijo", "", "", "", "", DateTime.Now);
        mytask.CreateItem(mytask);
        Console.WriteLine(mytask.Title);
    }
}*/
