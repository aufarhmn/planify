﻿using Npgsql;
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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {

        private NpgsqlConnection conn;
        string connstring = "Host=20.24.68.238;Port=5432;Username=planify-admin;Password=Planify123Junpro;Database=planify";
        public LoginPage()
        {
            conn = new NpgsqlConnection(connstring);
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterPage newPage = new RegisterPage();
            this.NavigationService.Navigate(newPage);
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
           

           /* CreateTask newPage = new CreateTask();
            this.NavigationService.Navigate(newPage);*/
        }
    }
}
