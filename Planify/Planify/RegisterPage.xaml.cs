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

namespace Planify
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        private NpgsqlConnection conn;
        string connstring = "Host=20.24.68.238;Port=5432;Username=postgres;Password=Planify123Junpro;Database=planify";
        public static NpgsqlCommand cmd;
        public string sql;
        public RegisterPage()
        {
            conn = new NpgsqlConnection(connstring);
            InitializeComponent();
          
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginPage newPage = new LoginPage();
            this.NavigationService.Navigate(newPage);
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"select * from user_register(:_name, :_email, :_password)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_name", txtName.Text);
                cmd.Parameters.AddWithValue("_email", txtEmail.Text);
                cmd.Parameters.AddWithValue("_password", txtPassword.Password);
                
                if((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Successfully Registered", "Well Done", MessageBoxButton.OK, MessageBoxImage.Information);
                    conn.Close();
                    txtEmail.Text = txtName.Text = txtPassword.Password = txtConfirmPassword.Password = null;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Register Fail!!", MessageBoxButton.OK, MessageBoxImage.Error);
                conn.Close();
            }
        }

       
    }
}
