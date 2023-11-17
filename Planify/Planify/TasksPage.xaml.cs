using Npgsql;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        public int taskId;
        public DateTime taskCreate;

      



        public TasksPage()
        {
            
            conn = new NpgsqlConnection(connstring);
            InitializeComponent();
            datePickerDeadline.DisplayDateStart = DateTime.Now;
            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();

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
                MessageBox.Show("Error" + ex.Message, "Mendapatkan task Fail!!", MessageBoxButton.OK, MessageBoxImage.Error);
                conn.Close();
            }

        }
       

        public void btnCreate_Click(object sender, RoutedEventArgs e)
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
                 
                if((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Berhasil membuat task","Sukses membuat Task", MessageBoxButton.OK, MessageBoxImage.Information);
                    conn.Close();
                    btnLoad_Click(btnLoad, null);

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

        private void dgTask_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
         
        }

        private void dgTask_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
           
        }
       

        private void dgTask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgTask.SelectedItem != null)
            {
                DataRowView row = dgTask.SelectedItem as DataRowView;
                if (row != null)
                {
                   

                    // Use the correct column names based on the output
                    // Replace "taskname" and "taskdescription" with the correct column names
                    tbTitle.Text = row["_taskname"].ToString();
                   tbDesc.Text = row["_taskdescription"].ToString();
                    tbCategory.Text = row["_categoryname"].ToString();
                    taskId = (int)row["_taskid"];

                    if (DateTime.TryParse(row["_taskdeadline"].ToString(), out DateTime taskDeadline))
                    {
                        datePickerDeadline.SelectedDate = taskDeadline;
                    }
                    else
                    {
                        datePickerDeadline.SelectedDate = DateTime.Now;
                    }

                    if (DateTime.TryParse(row["_taskcreatedate"].ToString(), out DateTime taskcreatedate))
                    {
                        taskCreate = taskcreatedate;
                    }
                    else
                    {
                        taskCreate = DateTime.Now;
                    }




                    bool isTaskDone = Convert.ToBoolean(row["_taskisdone"]);

                    if (isTaskDone)
                    {
                        rbDone.IsChecked = true;
                       
                    }
                    else
                    {
                        
                        rbOngoing.IsChecked = true;
                    }
                    // Update other text boxes as needed for different columns
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();
            sql = $"select * from delete_tasks({taskId})";
            cmd = new NpgsqlCommand(sql, conn);
            if ((int)cmd.ExecuteScalar() == 1)
            {
                MessageBox.Show("Berhasil menghapus task", "Sukses menghapus Task", MessageBoxButton.OK, MessageBoxImage.Information);
                conn.Close();
                btnLoad_Click(btnLoad, null);

            }
            else
            {
                MessageBox.Show("Error gagal menghapus task", "Menghapus Fail!!", MessageBoxButton.OK, MessageBoxImage.Error);
                conn.Close();
            }

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
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
                if(rbDone.IsChecked == false && rbOngoing.IsChecked == false)
                {
                    MessageBox.Show("Silahkan mengisi status task", "Gagal update Task", MessageBoxButton.OK, MessageBoxImage.Information);
                    conn.Close();
                }

                cmd.Parameters.Add(new NpgsqlParameter("_taskisdone", NpgsqlTypes.NpgsqlDbType.Boolean)).Value = rbDone.IsChecked;




                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Berhasil mengupdate task", "Sukses mengupdate Task", MessageBoxButton.OK, MessageBoxImage.Information);
                    conn.Close();
                    btnLoad_Click(btnLoad, null);

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

        

        private void btnNote_Click(object sender, RoutedEventArgs e)
        {
            NotesPage newPage = new NotesPage();
            newPage.userId = userId;
            this.NavigationService.Navigate(newPage);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginPage newPage = new LoginPage();
            this.NavigationService.Navigate(newPage);
        }
    }
}
