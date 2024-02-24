using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bankovni_sistem
{
  
    public partial class MainWindow : Window
    {
        public static int zaposlenid;
        public MainWindow()
        {
            InitializeComponent();
            Uplatabtn.Visibility = Visibility.Collapsed;
            Podizanjebtn.Visibility = Visibility.Collapsed;
            Korisniklbl.Visibility = Visibility.Collapsed;
            Listaprikaza.Visibility = Visibility.Collapsed;
            Otvaranjebtn.Visibility= Visibility.Collapsed; 
            Placanjebtn.Visibility = Visibility.Collapsed;  
            Proverabtn.Visibility = Visibility.Collapsed;
           
        }

        private void Prijavabtn_Click(object sender, RoutedEventArgs e)
        {
            string ime = Imetb.Text;
            string password = Lozinkatb.Text;
           
            
            
            SqlConnection connection = ((App)Application.Current).YourSqlConnection;

            
            if (connection == null || connection.State != ConnectionState.Open)
            {
                
                return;
            }
            
            string query = "SELECT * FROM TblEmploye WHERE EmployeEmail = @ime";

            
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ime", ime);

                
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                   
                    string storedPassword = reader["EmployeJMBG"].ToString();

                    if (password == storedPassword)
                    {
                        zaposlenid = reader.GetInt32("EmployeID");
                        Unesilbl.Visibility = Visibility.Collapsed;
                        Imetb.Visibility = Visibility.Collapsed;
                        Lozinkatb.Visibility = Visibility.Collapsed;
                        Prijavabtn.Visibility = Visibility.Collapsed;
                        Uplatabtn.Visibility = Visibility.Visible;
                        Podizanjebtn.Visibility = Visibility.Visible;
                        Korisniklbl.Visibility = Visibility.Visible;
                        Listaprikaza.Visibility = Visibility.Visible;
                        Otvaranjebtn.Visibility = Visibility.Visible;
                        Placanjebtn.Visibility = Visibility.Visible;
                        Proverabtn.Visibility = Visibility.Visible;
                        Korisniklbl.Content = reader["EmployeName"].ToString() +" "+ reader["EmployeSurn"].ToString()+"-"+ reader["Position"].ToString();
                    }
                    else
                    {
                       
                        MessageBox.Show("Netačna lozinka!");
                        
                    }
                }
                else
                {
                    
                    MessageBox.Show("Korisnik nije pronađen!");
                    
                }
                reader.Close();
            }



        }

        private void Otvaranjebtn_Click(object sender, RoutedEventArgs e)
        {
            OtvaranjeRacuna ORprozor = new OtvaranjeRacuna();
            ORprozor.Show();
        }

        private void Uplatabtn_Click(object sender, RoutedEventArgs e)
        {
            Deposit Dprozor = new Deposit();
            Dprozor.Show();
        }

        private void Podizanjebtn_Click(object sender, RoutedEventArgs e)
        {
            Withdrawal withdrawal = new Withdrawal();
            withdrawal.Show();
        }

        private void Placanjebtn_Click(object sender, RoutedEventArgs e)
        {
            Transfer transfer = new Transfer();
            transfer.Show();
        }

        private void Proverabtn_Click(object sender, RoutedEventArgs e)
        {
            Proverastanja proverastanja = new Proverastanja(this);
            proverastanja.Show();
        }
    }
}