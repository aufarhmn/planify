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
    /// Interaction logic for CreateTask.xaml
    /// </summary>
    public partial class CreateTask : Page
    {
        public CreateTask()
        {
            InitializeComponent();
        }

        private NpgsqlConnection conn;
        string connstring = "Host=20.24.68.238;Port=5432;Username=planify-admin;Password=Planify123Junpro;Database=planify";
        public static NpgsqlCommand cmd;
        public string sql;
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try {
            conn.Open();
                sql = @"select ";
            
            }catch (Exception ex) { }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
