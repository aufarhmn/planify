using Npgsql;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Planify
{
    /// <summary>
    /// Interaction logic for PomodoroPage.xaml
    /// </summary>
    public partial class PomodoroPage : Page
    {
        public int userId;
        public PomodoroPage()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginPage newPage = new LoginPage();
            this.NavigationService.Navigate(newPage);
        }

        private void btnNote_Click(object sender, RoutedEventArgs e)
        {
            NotesPage newPage = new NotesPage();
            newPage.userId = userId;
            newPage.btnLoad_Click(newPage.btnLoad, null);
            this.NavigationService.Navigate(newPage);
        }

        private void btnTask_Click(object sender, RoutedEventArgs e)
        {
            TasksPage newPage = new TasksPage();
            newPage.userId = userId;
            newPage.btnLoad_Click(newPage.btnLoad, null);
            this.NavigationService.Navigate(newPage);
        }

        private void ShowPomodoro_Click(object sender, RoutedEventArgs e)
        {
            int studyMinutes;
            int breakMinutes;
            if (int.TryParse(tbStudyMinutes.Text, out studyMinutes) && int.TryParse(tbBreakMinutes.Text, out breakMinutes))
            {
                // Both inputs are valid integers
                if (studyMinutes > 0 && breakMinutes > 0)
                {
                    PomodorView pomodorView = new PomodorView();
                    pomodorView.studyMinutes = studyMinutes;
                    pomodorView.breakMinutes = breakMinutes;
                    pomodorView.Show();
                }
                else
                {
                    MessageBox.Show("Study minutes and break minutes must be greater than 0", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Input must be number", "Pomodoro Fail!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
           
        
    }
   
}
