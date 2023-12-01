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
    /// Interaction logic for EditTask.xaml
    /// </summary>
    public partial class EditTask : Window
    {
        private NpgsqlConnection conn;
        string connstring = "Host=20.24.68.238;Port=5432;Username=postgres;Password=Planify123Junpro;Database=planify";
        public static NpgsqlCommand cmd;
        public DataTable dt;
        public string sql = null;
        private DataGrid r;
        public int userId;
        public int taskId;
        public DateTime taskCreate;
        public TasksPage thisIsPage;
        public EditTask()
        {
            conn = new NpgsqlConnection(connstring);
            InitializeComponent();
        }

        private void Cancel_Button(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateTask_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"select * from task_update(:_taskid, :_taskname, :_taskdescription, :_taskcreatedate, :_taskdeadline, :_taskisdone)";
                cmd = new NpgsqlCommand(sql, conn);

                cmd.Parameters.Add(new NpgsqlParameter("_taskid", NpgsqlTypes.NpgsqlDbType.Integer)).Value = taskId;
                cmd.Parameters.AddWithValue("_taskname", tbTitle.Text);
                cmd.Parameters.AddWithValue("_taskdescription", tbDesc.Text);
                cmd.Parameters.Add(new NpgsqlParameter("_taskcreatedate", NpgsqlTypes.NpgsqlDbType.Date)).Value = taskCreate;
                cmd.Parameters.Add(new NpgsqlParameter("_taskdeadline", NpgsqlTypes.NpgsqlDbType.Date)).Value = datePickerDeadline.SelectedDate;
                if (rbDone.IsChecked == false && rbOngoing.IsChecked == false)
                {
                    MessageBox.Show("Silahkan mengisi status task", "Gagal update Task", MessageBoxButton.OK, MessageBoxImage.Information);
                    conn.Close();
                }

                cmd.Parameters.Add(new NpgsqlParameter("_taskisdone", NpgsqlTypes.NpgsqlDbType.Boolean)).Value = rbDone.IsChecked;




                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Berhasil mengupdate task", "Sukses mengupdate Task", MessageBoxButton.OK, MessageBoxImage.Information);
                    conn.Close();
                    thisIsPage.btnLoad_Click(thisIsPage.btnLoad, null);
                    Close();

                }
                else
                {
                    MessageBox.Show("Error gagal mengupdate task", "Update Fail!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Mengupdate task Fail!!", MessageBoxButton.OK, MessageBoxImage.Error);
                conn.Close();
            }
        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        private void Close_Page(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
