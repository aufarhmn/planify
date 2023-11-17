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

        

        private void btnLoad_Click(object sender, RoutedEventArgs e)
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
                dgNote.ItemsSource = dt.DefaultView;
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
            try
            {
                conn.Open();
                sql = @"select * from create_notes(:_categoryname, :_userid, :_notename, :_notedescription, :_noteisfavorite)";
                cmd = new NpgsqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("_categoryname", tbCategory.Text);
                cmd.Parameters.Add(new NpgsqlParameter("_userid", NpgsqlTypes.NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.AddWithValue("_notename", tbTitle.Text);
                cmd.Parameters.AddWithValue("_notedescription", tbDesc.Text);
                

                if (rbYes.IsChecked == false && rbNo.IsChecked == false)
                {
                    MessageBox.Show("Silahkan pilih nilai favorite", "Gagal membuat Note", MessageBoxButton.OK, MessageBoxImage.Information);
                    conn.Close();
                }

                cmd.Parameters.Add(new NpgsqlParameter("_noteisfavorite", NpgsqlTypes.NpgsqlDbType.Boolean)).Value = rbYes.IsChecked;


                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Berhasil membuat note", "Sukses membuat Note", MessageBoxButton.OK, MessageBoxImage.Information);
                    conn.Close();
                    btnLoad_Click(btnLoad, null);

                }
                else
                {
                    MessageBox.Show("Error Gagal membuat note", "Fail!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Membuat note Fail!!", MessageBoxButton.OK, MessageBoxImage.Error);
                conn.Close();
            }
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
            if (dgNote.SelectedItem != null)
            {
                DataRowView row = dgNote.SelectedItem as DataRowView;
                if (row != null)
                {


                    // Use the correct column names based on the output
                    // Replace "taskname" and "taskdescription" with the correct column names
                    tbTitle.Text = row["_notename"].ToString();
                    tbDesc.Text = row["_notedescription"].ToString();
                    tbCategory.Text = row["_categoryname"].ToString();
                    bool isFavorite = Convert.ToBoolean(row["_noteisfavorite"]);

                    if (isFavorite)
                    {
                        rbYes.IsChecked = true;

                    }
                    else
                    {

                        rbNo.IsChecked = true;
                    }
                    noteId = (int)row["_noteid"];
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
            this.NavigationService.Navigate(newPage);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"select * from note_update(:_noteid, :_notename, :_notedescription, :_noteisfavorite)";
                cmd = new NpgsqlCommand(sql, conn);

                cmd.Parameters.Add(new NpgsqlParameter("_noteid", NpgsqlTypes.NpgsqlDbType.Integer)).Value = noteId;
                cmd.Parameters.AddWithValue("_notename", tbTitle.Text);
                cmd.Parameters.AddWithValue("_notedescription", tbDesc.Text);


                if (rbYes.IsChecked == false && rbNo.IsChecked == false)
                {
                    MessageBox.Show("Silahkan pilih nilai favorite", "Gagal mengupdate Note", MessageBoxButton.OK, MessageBoxImage.Information);
                    conn.Close();
                }

                cmd.Parameters.Add(new NpgsqlParameter("_noteisfavorite", NpgsqlTypes.NpgsqlDbType.Boolean)).Value = rbYes.IsChecked;


                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Berhasil mengupdate note", "Sukses mengupdate Note", MessageBoxButton.OK, MessageBoxImage.Information);
                    conn.Close();
                    btnLoad_Click(btnLoad, null);

                }
                else
                {
                    MessageBox.Show("Error gagal mengupdate note", "Mengupdate note Fail!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Mengupdate note Fail!!", MessageBoxButton.OK, MessageBoxImage.Error);
                conn.Close();
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginPage newPage = new LoginPage();
            this.NavigationService.Navigate(newPage);
        }
    }
}
