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
                MessageBox.Show("Error" + ex.Message, "Login Fail!!", MessageBoxButton.OK, MessageBoxImage.Error);
                conn.Close();
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"select * from create_notes(:_categoryname, :_userid, :_notename, :_notedescription, :_notecreatedate)";
                cmd = new NpgsqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("_categoryname", tbCategory.Text);
                cmd.Parameters.Add(new NpgsqlParameter("_userid", NpgsqlTypes.NpgsqlDbType.Integer)).Value = userId;
                cmd.Parameters.AddWithValue("_notename", tbTitle.Text);
                cmd.Parameters.AddWithValue("_notedescription", tbDesc.Text);
                cmd.Parameters.Add(new NpgsqlParameter("_notecreatedate", NpgsqlTypes.NpgsqlDbType.Date)).Value = DateTime.Now;

                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Berhasil membuat note", "Sukses membuat Bite", MessageBoxButton.OK, MessageBoxImage.Information);
                    conn.Close();
                    btnLoad_Click(btnLoad, null);

                }
                else
                {
                    MessageBox.Show("Error", "Login Fail!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Login Fail!!", MessageBoxButton.OK, MessageBoxImage.Error);
                conn.Close();
            }
        }

        private void dgNote_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                    noteId = (int)row["_noteid"];
                    // Update other text boxes as needed for different columns
                }
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
                    MessageBox.Show("Error", "Login Fail!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    conn.Close();
                }

            }
        }
    }
}
