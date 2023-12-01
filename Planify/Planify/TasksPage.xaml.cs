using MaterialDesignThemes.Wpf;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
                List<TaskLoad> taskList = dt.AsEnumerable().Select(row => {
                    taskId = row.Field<int>("_taskid");
                    return new TaskLoad
                    {

                        TaskId = row.Field<int>("_taskid"),
                        TaskTitle = row.Field<string>("_taskname"),
                        TaskCategory = row.Field<string>("_categoryname"),
                        TaskDescription = row.Field<string>("_taskdescription"),
                        TaskCreateDate = row.Field<DateTime>("_taskcreatedate"),
                        TaskDateEnd = row.Field<DateTime>("_taskdeadline"),
                        TaskIsDone = row.Field<bool>("_taskisdone"),
                        // Map other properties accordingly
                    };
                }).ToList();
                /* List<Task> taskList = dt.AsEnumerable().Select(row =>
         new Task("", "", "", "", "", DateTime.Now)
         {

             Title = row.Field<string>("number"),
             Description = row.Field<string>("description"),
             DateCreated = row.Field<DateTime>("dateCreate"),
             DateTimeEnd = row.Field<DateTime>("dateEnd"),
             TaskIsDone = row.Field<Boolean>("done")

             // Map other properties accordingly
         }).ToList();*/
                dgTask.ItemsSource = taskList;
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
            AddTask addTask = new AddTask();
            addTask.userId = userId;
            addTask.thisIsPage = this;
            addTask.ShowDialog();
            /*try
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
            }*/
        }

        private void dgTask_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
         
        }

        private void dgTask_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
           
        }
       

        private void dgTask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SeeTask seeTask = new SeeTask();
            



            if (dgTask.SelectedItem != null)

            {

               
                TaskLoad selectedItem = dgTask.SelectedItem as TaskLoad;

                if (selectedItem != null)
                {


                    // Use the correct column names based on the output
                    // Replace "taskname" and "taskdescription" with the correct column names
                    /* tbTitle.Text = row["TaskTitle"].ToString();
                     taskId = selectedItem.TaskId;

                     tbTitle.Text = selectedItem.TaskTitle;
                     tbDesc.Text = selectedItem.TaskDescription;
                     datePickerDeadline.SelectedDate = selectedItem.TaskDateEnd;
                     taskCreate = selectedItem.TaskCreateDate;
                     bool isTaskDone = selectedItem.TaskIsDone;

                     if (isTaskDone)
                     {
                         rbDone.IsChecked = true;

                     }
                     else
                     {

                         rbOngoing.IsChecked = true;
                     }*/
                    
                  

                    seeTask.title.Text = selectedItem.TaskTitle;
                    seeTask.description.Text = selectedItem.TaskDescription;
                    seeTask.deadline.Content = selectedItem.TaskDateEnd.ToString("d MMMM yyyy");
                    seeTask.category.Content = $"Category : {selectedItem.TaskCategory}";
                   
                    IconTaskClass iconTaskClass = new IconTaskClass { IconTaskIsDone = selectedItem.TaskIsDone };
                    seeTask.DataContext = iconTaskClass;


                    seeTask.ShowDialog();
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
            EditTask editTaskPage = new EditTask();
            editTaskPage.userId = userId;
          
            editTaskPage.thisIsPage = this;

            if (dgTask.SelectedItem != null)

            {

               
                TaskLoad selectedItem = dgTask.SelectedItem as TaskLoad;

                if (selectedItem != null)
                {


                    // Use the correct column names based on the output
                    // Replace "taskname" and "taskdescription" with the correct column names
                    /*tbTitle.Text = row["TaskTitle"].ToString();*/
                   

                    editTaskPage.taskId = selectedItem.TaskId;

                    editTaskPage.tbTitle.Text = selectedItem.TaskTitle;
                    editTaskPage.tbCategory.Text = selectedItem.TaskCategory;
                    editTaskPage.tbDesc.Text = selectedItem.TaskDescription;

                    editTaskPage.datePickerDeadline.SelectedDate = selectedItem.TaskDateEnd;
                    editTaskPage.taskCreate = selectedItem.TaskCreateDate;


                    if (selectedItem.TaskIsDone)
                    {
                        editTaskPage.rbDone.IsChecked = true;

                    }
                    else
                    {

                        editTaskPage.rbOngoing.IsChecked = true;
                    }

                    editTaskPage.ShowDialog();
                    // Update other text boxes as needed for different columns
                }
            }
            /*try
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
            }*/
        }

        

        private void btnNote_Click(object sender, RoutedEventArgs e)
        {
            NotesPage newPage = new NotesPage();
            newPage.userId = userId;
            newPage.btnLoad_Click(newPage.btnLoad, null);
            this.NavigationService.Navigate(newPage);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginPage newPage = new LoginPage();
            this.NavigationService.Navigate(newPage);
        }

        private void Pomodoro_Click(object sender, RoutedEventArgs e)
        {
            PomodoroPage newPage = new PomodoroPage();
            newPage.userId = userId;
            this.NavigationService.Navigate(newPage);
          
        }
    }
}

public class TaskLoad
{
    public int TaskId { get; set; }
   
    public string TaskTitle { get; set; }
    public string TaskCategory { get; set; }
    public string TaskDescription{ get; set; }
    public DateTime TaskCreateDate { get; set; }
    public DateTime TaskDateEnd { get; set; }
    public bool TaskIsDone { get; set; }
    // Other properties for your task class
}
