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
    /// Interaction logic for CreateNote.xaml
    /// </summary>
    public partial class CreateNote : Window
    {
        private NpgsqlConnection conn;
        string connstring = "Host=20.24.68.238;Port=5432;Username=postgres;Password=Planify123Junpro;Database=planify";
        public static NpgsqlCommand cmd;
        public DataTable dt;
        public string sql = null;
        private DataGrid r;
        public int userId;
        public NotesPage thisIsPage;

   
        public CreateNote()
        {
            conn = new NpgsqlConnection(connstring);
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

       

        private void Cancel_Button(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Close_addNote_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddNote_Button(object sender, RoutedEventArgs e)
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
                    thisIsPage.btnLoad_Click(thisIsPage.btnLoad, null);
                    Close();

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
    }
}
