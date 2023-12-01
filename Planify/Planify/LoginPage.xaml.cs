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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        
        private NpgsqlConnection conn;
        string connstring = "Host=20.24.68.238;Port=5432;Username=postgres;Password=Planify123Junpro;Database=planify";
        public static NpgsqlCommand cmd;
        public string sql;
        User user = new User();
        public LoginPage()
        {
            conn = new NpgsqlConnection(connstring);
            InitializeComponent();
            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();
        }

      

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterPage newPage = new RegisterPage();
            this.NavigationService.Navigate(newPage);
        }

       
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                conn.Open();
                sql = @"select * from user_login(:_name, :_password)";
                cmd = new NpgsqlCommand(sql, conn);
                
                cmd.Parameters.AddWithValue("_name", txtName.Text);
                cmd.Parameters.AddWithValue("_password", txtPassword.Password);

                if ((int)cmd.ExecuteScalar() != 0)
                {
                    
                   
                    TasksPage newPage = new TasksPage();
                    newPage.userId = (int)cmd.ExecuteScalar();
                   newPage.btnLoad_Click(newPage.btnLoad, null);
                    this.NavigationService.Navigate(newPage);

                    conn.Close();
                    txtName.Text = txtPassword.Password = null;

                    if (this.NavigationService.CanGoBack)
                    {
                        this.NavigationService.RemoveBackEntry();
                    }
                }
                else
                {
                    MessageBox.Show("Username atau Password salah", "Login Fail!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    conn.Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Login Fail!!", MessageBoxButton.OK, MessageBoxImage.Error);
                conn.Close();
            }
        }

        
    }
}
