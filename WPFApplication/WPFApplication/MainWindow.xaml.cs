using Npgsql;
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

namespace WPFApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            loadUsers();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Test Clicked");
        }

        private void loadUsers()
        {
            NpgsqlConnection con = new NpgsqlConnection("Server=localhost;Port=5432;Database=Test;Username=postgres;Password=Password");
            con.Open();

            NpgsqlCommand command = new NpgsqlCommand("select * from \"UserDetails\"", con);
            NpgsqlDataReader reader = command.ExecuteReader();

            Paragraph paragraph = new Paragraph();
            while (reader.Read())
            {
                paragraph.Inlines.Add(reader["Username"].ToString());
            }
            richTextBox.Document = new FlowDocument(paragraph);
            // Need to update it to the MVVVM template
        }
    }
}
