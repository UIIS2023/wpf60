using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Bankovni_sistem
{
    
    public partial class Transfer : Window
    {
        public Transfer()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string uplatioc = Dtb1.Text;
            string account = Dtb2.Text;
            string suma = Dtb3.Text;
            string account2 = Dtb4.Text;
            string pozivni = Dtb5.Text; 
            int accid = 0;
            double balance;
            int tid;
            SqlConnection connection = ((App)Application.Current).YourSqlConnection;
            string q1 = $"SELECT AccID FROM tblAccount WHERE AccNum =@1";
            using (SqlCommand command = new SqlCommand(q1, connection))

            {
                command.Parameters.AddWithValue("@1", account);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {

                    accid = reader.GetInt32(0);

                }
                else
                {

                    MessageBox.Show("Ovaj nalog ne posotji");


                }


            }
            if (accid != 0)
            {
                string q2 = $"SELECT AccBal FROM TblAccount where AccID =@1";
                using (SqlCommand command = new SqlCommand(q2, connection))

                {
                    command.Parameters.AddWithValue("@1", accid);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    balance = reader.GetDouble(0);




                }
                string q3 = $"UPDATE TblAccount SET AccBal=@1 WHERE AccID=@2";
                if (int.TryParse(suma, out int isuma))

                {
                    balance -= isuma;
                    using (SqlCommand command = new SqlCommand(q3, connection))
                    {
                        command.Parameters.AddWithValue("@1", balance);
                        command.Parameters.AddWithValue("@2", accid);
                        command.ExecuteNonQuery();

                    }

                    string q4 = $"INSERT INTO TblServices (TransactionDate,Amount,PayerName,EmployeeID) OUTPUT INSERTED.TransactionID VALUES(GETDATE(),@1,@2,@3)";
                    using (SqlCommand command = new SqlCommand(q4, connection))
                    {

                        command.Parameters.AddWithValue("@1", "-"+suma);
                        command.Parameters.AddWithValue("@2", uplatioc);
                        command.Parameters.AddWithValue("@3", MainWindow.zaposlenid);
                        tid = Convert.ToInt32(command.ExecuteScalar());

                    }
                    string q5 = $"INSERT INTO Transfer(TransactionID,AccountID,ToAccountID,CallNumber) VALUES(@1,@2,@3,@4)";
                    using (SqlCommand command = new SqlCommand(q5, connection))
                    {

                        command.Parameters.AddWithValue("@1", tid);
                        command.Parameters.AddWithValue("@2", accid);
                        command.Parameters.AddWithValue("@3", account2);
                        command.Parameters.AddWithValue("@4", pozivni);
                        command.ExecuteNonQuery();



                    }
                    MessageBox.Show("Transakcija zavrsena");
                    Close();


                }
                else
                {
                    MessageBox.Show("unesite pravilnu vrednost");

                }
            }
        }
    }
}
