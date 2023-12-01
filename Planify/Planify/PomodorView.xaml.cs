using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Timers;

namespace Planify
{
    /// <summary>
    /// Interaction logic for PomodorView.xaml
    /// </summary>
    public partial class PomodorView : Window
    {
        Timer timer = new Timer();
        private int setStudy;
        private int setBreak;
        public int studyMinutes;
        public int breakMinutes;   
        
        public int h, m = 0 , s = 0, ms = 0;

       

        public PomodorView()
        {
            InitializeComponent();
            setStudy = studyMinutes;
            setBreak = breakMinutes;
           
        }

        private void Close_Pomodoro(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            timer.Interval = 1;
            setStudy = studyMinutes;
            setBreak = breakMinutes;
            timer.Elapsed += onTimeEvent;
            timer.Start();
        }

        void onTimeEvent(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                
                if(setStudy >= 0)
                {
                    Title.Content = "Let's Study";
                    Title.Foreground = Brushes.Teal;
                    borderTimer.BorderBrush = Brushes.Teal;
                    m = setStudy;
                    if (s == 0)
                    {
                        s = 59;
                        setStudy -= 1;
                    }

                    if (ms == 0)
                    {
                        ms = 99;
                        s -= 1;
                    }



                    /* if (m == 60)
                     {
                         m = 0;
                         h += 1;
                     }*/
                    ms -= 1;
                }
                else
                {
                    Title.Content = "Let's Take a Break";
                    Title.Foreground = Brushes.DimGray;
                    borderTimer.BorderBrush = Brushes.DimGray;
                    m = setBreak - 1;

                    if (s == 0)
                    {
                        s = 59;
                        setBreak -= 1;
                    }

                    if (ms == 0)
                    {
                        ms = 99;
                        s -= 1;
                    }




                    ms -= 1;
                    if (m < 0)
                    {
                        setStudy = studyMinutes -1;
                        setBreak = breakMinutes;

                    }

                }
                


                timerView.Content = string.Format("{0}:{1}:{2}:{3}", h.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'), ms.ToString().PadLeft(2, '0'));
            });
        }




        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }
    }
}
