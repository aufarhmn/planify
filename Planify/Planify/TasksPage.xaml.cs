using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for TasksPage.xaml
    /// </summary>
    public partial class TasksPage : Page
    {
        private NpgsqlConnection conn;
        string connstring = "Host=20.24.68.238;Port=5432;Username=postgres;Password=Planify123Junpro;Database=planify";
        public static NpgsqlCommand cmd;
        public DataTable dt;
        public string sql = null;
        private DataGrid r;
        public int userId;
      



        public TasksPage()
        {
            conn = new NpgsqlConnection(connstring);
            InitializeComponent();
            datePickerDeadline.DisplayDateStart = DateTime.Now;
           
        }

        public void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               conn.Open();
                dgTask.ItemsSource = null;
                LoginPage loginPage = new LoginPage();

               /* dt.Columns.Add("Title");
                dt.Columns.Add("Description");*/

                /*dgTask.ItemsSource = dt;*/

                sql = $"select * from get_tasks({userId})";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                NpgsqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd); 
                dgTask.ItemsSource = dt.DefaultView;
                conn.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Login Fail!!", MessageBoxButton.OK, MessageBoxImage.Error);
                conn.Close();
            }

        }
        public void Button_Click(object sender,RoutedEventArgs e) { }

        public void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"select * from create_task(:_categoryname, :_userid, :_taskname, :_taskdescription, :_taskcreatedate, :_taskdeadline, :_taskisdone";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_categoryname", tbCategory.Text);
                cmd.Parameters.AddWithValue("_userid", userId);
                cmd.Parameters.AddWithValue("_taskname", tbTitle);
                cmd.Parameters.AddWithValue("_taskdescription", tbDesc);
                cmd.Parameters.AddWithValue("_taskcreatedate", DateTime.Now);
                cmd.Parameters.AddWithValue("_taskdeadline", datePickerDeadline);
                cmd.Parameters.AddWithValue("_taskisdone", false);
                 
                if((int)cmd.ExecuteScalar() == 0)
                {
                    MessageBox.Show("","Sukses membuat Task", MessageBoxButton.OK, MessageBoxImage.Information);
                    conn.Close();
                    btnLoad_Click(btnLoad, null);

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
