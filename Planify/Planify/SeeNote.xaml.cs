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
    /// Interaction logic for SeeNote.xaml
    /// </summary>
    public partial class SeeNote : Window
    {
        public SeeNote()
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

public class IconNoteClass
{

    public bool IconNoteFavorite { get; set; }
    // Other properties for your task class
}
