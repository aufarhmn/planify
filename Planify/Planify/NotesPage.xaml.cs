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

namespace Planify
{
    /// <summary>
    /// Interaction logic for NotesPage.xaml
    /// </summary>
    public partial class NotesPage : Page
    {
        private NpgsqlConnection conn;
        string connstring = "Host=20.24.68.238;Port=5432;Username=postgres;Password=Planify123Junpro;Database=planify";
        public static NpgsqlCommand cmd;
        public DataTable dt;
        public string sql = null;
        private DataGrid r;
        public int userId;
        public int noteId;

        public NotesPage()
        {
            conn = new NpgsqlConnection(connstring);
            InitializeComponent();
        }

        

        public void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                dgNote.ItemsSource = null;
                LoginPage loginPage = new LoginPage();

                /* dt.Columns.Add("Title");
                 dt.Columns.Add("Description");*/

                /*dgTask.ItemsSource = dt;*/

                sql = $"select * from get_notes({userId})";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                NpgsqlDataReader rd = cmd.ExecuteReader();
                dt.Load(rd);
                List<NotesLoad> noteList = dt.AsEnumerable().Select(row =>{
                    noteId = row.Field<int>("_noteid");
                   return  new NotesLoad
                    {
                        NoteId = row.Field<int>("_noteid"),
                        NoteTitle = row.Field<string>("_notename"),
                        NoteCategory = row.Field<string>("_categoryname"),
                        NoteDescription = row.Field<string>("_notedescription"),

                        NoteIsFavorite = row.Field<bool>("_noteisfavorite"),
                        // Map other properties accordingly
                    }; }).ToList();
                dgNote.ItemsSource = noteList;
                conn.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Mendapatkan note Fail!!", MessageBoxButton.OK, MessageBoxImage.Error);
                conn.Close();
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateNote createNote = new CreateNote();
            createNote.userId = userId;
            createNote.thisIsPage = this;
            createNote.ShowDialog();
            
        }

        

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            {
                conn.Open();
                sql = $"select * from delete_notes({noteId})";
                cmd = new NpgsqlCommand(sql, conn);
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Berhasil menghapus note", "Sukses menghapus Note", MessageBoxButton.OK, MessageBoxImage.Information);
                    conn.Close();
                    btnLoad_Click(btnLoad, null);

                }
                else
                {
                    MessageBox.Show("Error gagal menghapus note", "Menghapus Fail!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    conn.Close();
                }

            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SeeNote seeNote = new SeeNote();
            if (dgNote.SelectedItem != null)
            {
                
                NotesLoad selectedItem = dgNote.SelectedItem as NotesLoad;
                if (selectedItem != null)
                {


                    // Use the correct column names based on the output
                    // Replace "taskname" and "taskdescription" with the correct column names
                    /*noteId = selectedItem.NoteId;
                    tbTitle.Text = selectedItem.NoteTitle;
                    tbDesc.Text = selectedItem.NoteDescription;
                    if (selectedItem.NoteIsFavorite)
                    {
                        rbYes.IsChecked = true;

                    }
                    else
                    {

                        rbNo.IsChecked = true;
                    }*/

                    seeNote.title.Text = selectedItem.NoteTitle;
                    seeNote.category.Content = $"Category : {selectedItem.NoteCategory}";
                    seeNote.description.Text = selectedItem.NoteDescription;
                    IconNoteClass iconTaskClass = new IconNoteClass { IconNoteFavorite = selectedItem.NoteIsFavorite };
                    seeNote.DataContext = iconTaskClass;
                    seeNote.ShowDialog();

                    // Update other text boxes as needed for different columns
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTask_Click(object sender, RoutedEventArgs e)
        {
            TasksPage newPage = new TasksPage();
            newPage.userId = userId;
            newPage.btnLoad_Click(newPage.btnLoad, null);
            this.NavigationService.Navigate(newPage);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            EditNote editNote = new EditNote();
            editNote.userId = userId;
            editNote.thisIsPage = this;
           
            if (dgNote.SelectedItem != null)
            {

                NotesLoad selectedItem = dgNote.SelectedItem as NotesLoad;
                if (selectedItem != null)
                {


                    // Use the correct column names based on the output
                    // Replace "taskname" and "taskdescription" with the correct column names

                    editNote.noteId = selectedItem.NoteId;
                    editNote.tbTitle.Text = selectedItem.NoteTitle;
                    editNote.tbCategory.Text = selectedItem.NoteCategory;
                    editNote.tbDesc.Text = selectedItem.NoteDescription;
                    if (selectedItem.NoteIsFavorite)
                    {
                        editNote.rbYes.IsChecked = true;

                    }
                    else
                    {

                        editNote.rbNo.IsChecked = true;
                    }

                    editNote.ShowDialog();
                    // Update other text boxes as needed for different columns
                }
            }

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

public class NotesLoad
{
    public int NoteId { get; set; }
    public string NoteTitle { get; set; }

    public string NoteCategory { get; set; }
    public string NoteDescription { get; set; }
    public bool NoteIsFavorite { get; set; }
    // Other properties for your task class
}
