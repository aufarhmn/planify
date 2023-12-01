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

namespace Planify
{
    /// <summary>
    /// Interaction logic for SeeTask.xaml
    /// </summary>
    public partial class SeeTask : Window
    {
        public bool taskIsDone;
        
        public SeeTask()
        {
            InitializeComponent();
            
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        private void Close_Task_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

public class IconTaskClass
{
    
    public bool IconTaskIsDone { get; set; }
    // Other properties for your task class
}
