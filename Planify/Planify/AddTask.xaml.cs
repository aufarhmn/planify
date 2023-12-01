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
using System.Windows.Shapes;

namespace Planify
{
    /// <summary>
    /// Interaction logic for AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        private NpgsqlConnection conn;
        string connstring = "Host=20.24.68.238;Port=5432;Username=postgres;Password=Planify123Junpro;Database=planify";
        public static NpgsqlCommand cmd;
        public DataTable dt;
        public string sql = null;
        private DataGrid r;
        public int userId;
        public TasksPage thisIsPage;
       
        public DateTime taskCreate;
        public AddTask()
        {
            conn = new NpgsqlConnection(connstring);
            InitializeComponent();
            datePickerDeadline.DisplayDateStart = DateTime.Now;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }
        


        private void AddTask_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"select * from create_tasks(:_categoryname, :_userid, :_taskname, :_taskdescription, :_taskcreatedate, :_taskdeadline, :_taskisdone)";
                cmd = new NpgsqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("_categoryname", tbCategory.Text);
                cmd.Parameters.Add(new NpgsqlParameter("_userid", NpgsqlTypes.NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.AddWithValue("_taskname", tbTitle.Text);
                cmd.Parameters.AddWithValue("_taskdescription", tbDesc.Text);
                cmd.Parameters.Add(new NpgsqlParameter("_taskcreatedate", NpgsqlTypes.NpgsqlDbType.Date)).Value = DateTime.Now;
                cmd.Parameters.Add(new NpgsqlParameter("_taskdeadline", NpgsqlTypes.NpgsqlDbType.Date)).Value = datePickerDeadline.SelectedDate;
                cmd.Parameters.Add(new NpgsqlParameter("_taskisdone", NpgsqlTypes.NpgsqlDbType.Boolean)).Value = false;

                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Berhasil membuat task", "Sukses membuat Task", MessageBoxButton.OK, MessageBoxImage.Information);
                    conn.Close();
                    
                   
                    thisIsPage.btnLoad_Click(thisIsPage.btnLoad, null);
                    Close();

                }
                else
                {
                    MessageBox.Show("Error", "Create Fail!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Create Fail!!", MessageBoxButton.OK, MessageBoxImage.Error);
                conn.Close();
            }
        }

        private void Close_addTask_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void Cancel_Button(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
