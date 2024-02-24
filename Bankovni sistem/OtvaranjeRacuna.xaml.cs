using System.Data.SqlClient;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Bankovni_sistem
{
  
    public partial class OtvaranjeRacuna : Window
    {
       

        public OtvaranjeRacuna()
        {
            InitializeComponent();
            ORRB1.IsChecked = true;

        }


        private void ORRB2_Checked(object sender, RoutedEventArgs e)
        {
            ORG1.IsEnabled = false;
            ORG2.IsEnabled = true;
        }

        private void ORRB1_Checked(object sender, RoutedEventArgs e)
        {
            ORG1.IsEnabled = true;
            ORG2.IsEnabled = false;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ORG1.IsEnabled = true;
            ORG2.IsEnabled = false;

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {


        }

        private void ORbtn1_Click(object sender, RoutedEventArgs e)
        {
            string ime = ORtb1.Text;
            string prezime = ORtb2.Text;
            string jmbg;
            DateTime rodjenje;
            char pol;
            string adresa = ORtb6.Text;
            string telefon = ORtb7.Text;
            string mail = ORtb8.Text;
            bool check = true;
            int clientid;
            int accid;
            int accnum;
            Random random = new Random();
            SqlConnection connection = ((App)Application.Current).YourSqlConnection;

            if (ORtb3.Text.Length != 13)
            {
                MessageBox.Show("unesite pravilan JMBG");
            }
            else if (long.TryParse(ORtb3.Text, out long outic))
            {
                jmbg = ORtb3.Text;
                string dateFormat = "dd/MM/yyyy";
                if (DateTime.TryParseExact(ORtb4.Text, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                {
                    rodjenje = result.Date;

                    if (char.TryParse(ORtb5.Text, out pol))
                    {
                        if (ORRB1.IsChecked == true)
                        {

                            string q1 = $"SELECT  ClientJMBG FROM TblClient";
                            using (SqlCommand command = new SqlCommand(q1, connection))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                       
                                        string columnValue = reader.GetString(0); 

                                      
                                        if (columnValue.Equals(jmbg, StringComparison.OrdinalIgnoreCase))
                                        {
                                            MessageBox.Show("ovaj klijent vec postoji");
                                            check = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (check)
                            {
                                string q2 = $"INSERT INTO TblClient (Clientname,ClientSname,ClientJMBG,DateBirth,Sex,Adress,Telefon,Email)  VALUES(@1,@2,@3,@4,@5,@6,@7,@8)";
                                using (SqlCommand command2 = new SqlCommand(q2, connection))
                                {
                                    command2.Parameters.AddWithValue("@1", ime);
                                    command2.Parameters.AddWithValue("@2", prezime);
                                    command2.Parameters.AddWithValue("@3", jmbg);
                                    command2.Parameters.AddWithValue("@4", rodjenje);
                                    command2.Parameters.AddWithValue("@5", pol);
                                    command2.Parameters.AddWithValue("@6", adresa);
                                    command2.Parameters.AddWithValue("@7", telefon);
                                    command2.Parameters.AddWithValue("@8", mail);
                                    command2.ExecuteNonQuery();


                                }
                                string q3 = $"SELECT ClientID  FROM TblClient WHERE ClientJMBG=@1";
                                using (SqlCommand command = new SqlCommand(q3, connection))
                                {
                                    command.Parameters.AddWithValue("@1", jmbg);
                                    using (SqlDataReader reader = command.ExecuteReader())
                                    {
                                        reader.Read();
                                        clientid = reader.GetInt32(0);


                                    }



                                }
                                string q4 = $"INSERT INTO TblAccount (AccNum,AccBal,AccDate,ClientID)  OUTPUT INSERTED.AccID VALUES(@1,@2,@3,@4)";
                                using (SqlCommand command = new SqlCommand(q4, connection))
                                {
                                    command.Parameters.AddWithValue("@1", random.Next(100000000,1000000000));
                                    command.Parameters.AddWithValue("@2", BalTbt.Text);
                                    command.Parameters.AddWithValue("@3", DateTime.Today);
                                    command.Parameters.AddWithValue("@4", clientid);
                                    accid = Convert.ToInt32(command.ExecuteScalar());


                                }
                            
                                string q6 = $"INSERT INTO TblCard(CardNum,CardHolder,ExpDate,CVV,PIN,AccID) VALUES(@1,@2,@3,@4,@5,@6)";
                                using (SqlCommand command = new SqlCommand(q6, connection))
                                {
                                    command.Parameters.AddWithValue("@1", random.Next(100000000, 1000000000));
                                    command.Parameters.AddWithValue("@2", ime +" "+ prezime);
                                    command.Parameters.AddWithValue("@3", DateTime.Today.AddYears(4));
                                    command.Parameters.AddWithValue("@4", random.Next(100, 1000));
                                    command.Parameters.AddWithValue("@5", random.Next(1000, 10000));
                                    command.Parameters.AddWithValue("@6", accid);
                                    command.ExecuteNonQuery();


                                }
                                string q7 = $"SELECT AccNum  FROM TblAccount WHERE AccID=@1";
                                using (SqlCommand command = new SqlCommand(q7, connection))
                                {
                                    command.Parameters.AddWithValue("@1", accid);
                                    using (SqlDataReader reader = command.ExecuteReader())
                                    {
                                        reader.Read();
                                        accnum = reader.GetInt32(0);


                                    }



                                }
                                MessageBox.Show(string.Format("Nalog uspešno otvoren, broj naloga je {0}", accnum));

                                Close();



                            }














                        }
                        else
                        {
                            jmbg = jmbgtbt2.Text;
                            string q3 = $"SELECT ClientID  FROM TblClient WHERE ClientJMBG=@1";
                            using (SqlCommand command = new SqlCommand(q3, connection))
                            {
                                command.Parameters.AddWithValue("@1", jmbg);
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    reader.Read(); 
                                    clientid = reader.GetInt32(0);


                                }



                            }
                            string q4 = $"INSERT INTO TblAccount(AccNum,AccBal,AccDate,ClientID)  OUTPUT INSERTED.AccID VALUES(@1,@2,@3,@4)";
                            using (SqlCommand command = new SqlCommand(q4, connection))
                            {
                                command.Parameters.AddWithValue("@1", random.Next(100000000, 1000000000));
                                command.Parameters.AddWithValue("@2", BalTbt.Text);
                                command.Parameters.AddWithValue("@3", DateTime.Today);
                                command.Parameters.AddWithValue("@4", clientid);
                                accid = Convert.ToInt32(command.ExecuteScalar());


                            }
                       
                            string q6 = $"INSERT INTO TblCard(CardNum,CardHolder,ExpDate,CVV,PIN,AccID) VALUES(@1,@2,@3,@4,@5,@6)";
                            using (SqlCommand command = new SqlCommand(q6, connection))
                            {
                                command.Parameters.AddWithValue("@1", random.Next(100000000, 1000000000));
                                command.Parameters.AddWithValue("@2", ime + " " + prezime);
                                command.Parameters.AddWithValue("@3", DateTime.Today.AddYears(4));
                                command.Parameters.AddWithValue("@4", random.Next(100, 1000));
                                command.Parameters.AddWithValue("@5", random.Next(1000, 10000));
                                command.Parameters.AddWithValue("@6", accid);
                                command.ExecuteNonQuery();


                            }
                            string q7 = $"SELECT AccNum  FROM TblAccount WHERE AccID=@1";
                            using (SqlCommand command = new SqlCommand(q7, connection))
                            {
                                command.Parameters.AddWithValue("@1", accid);
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    reader.Read();
                                    accnum = reader.GetInt32(0);


                                }



                            }
                            MessageBox.Show(string.Format("Nalog uspešno otvoren, broj naloga je {0}", accnum));


                            Close();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Pogresan unos pola");
                    }
                }
                else
                {
                    MessageBox.Show("pogresan unos datuma");
                }
            }
            else
            {
                MessageBox.Show("Unesite pravilan jmbg");
            }







        }

        private void Findbtn_Click(object sender, RoutedEventArgs e)
        {
            bool check = false;
            string jmbg = jmbgtbt2.Text;
            SqlConnection connection = ((App)Application.Current).YourSqlConnection;
            string q1 = $"SELECT  ClientJMBG FROM TblClient";
            using (SqlCommand command = new SqlCommand(q1, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                       
                        string columnValue = reader.GetString(0); 

                        
                        if (columnValue.Equals(jmbg, StringComparison.OrdinalIgnoreCase))
                        {
                            
                            check = true;
                            break;
                        }
                    }
                }
            }
            if(check)
            {
                string q2 = $"SELECT ClientName, ClientSname, DateBirth, Sex, Adress, Telefon, Email FROM TblClient WHERE ClientJMBG = @1";
                using (SqlCommand command = new SqlCommand(q2, connection))
                {
                    command.Parameters.AddWithValue("@1", jmbg);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                       ORtb1.Text = reader.GetString(0);
                        ORtb2.Text = reader.GetString(1);
                        ORtb3.Text = jmbg;
                        ORtb4.Text = reader.GetDateTime(2).ToString("dd/MM/yyyy");

                        ORtb5.Text = reader.GetString(3);
                        ORtb6.Text = reader.GetString(4);
                        ORtb7.Text = reader.GetString(5);
                        ORtb8.Text = reader.GetString(6);


                    }



                }






            }
            else
            {
                MessageBox.Show("Ovaj klijent ne postoji");



            }


        }
    }
}
