using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
 
    public partial class Proverastanja : Window
    {
        private MainWindow mainWindowInstance;
        public Proverastanja(MainWindow mainWindow)
        {
            InitializeComponent();
            mainWindowInstance = mainWindow;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int account;
            int accid = 0;
            bool check=false;
            List<ResultModel> resultList = new List<ResultModel>();
            if (int.TryParse(Tb1.Text, out account))
            {
                SqlConnection connection = ((App)Application.Current).YourSqlConnection;
                string q1 = $"SELECT  AccId FROM TblAccount WHERE AccNum=@1";
                using (SqlCommand command = new SqlCommand(q1, connection))
                {
                    command.Parameters.AddWithValue("@1", account);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            accid = reader.GetInt32(0);
                            check = true;
                        }
                        else
                        {
                            MessageBox.Show("Nepostojeci racun");
                        }



                    }





                }
                if(check)
                {
                    string q2 = "SELECT\r\n    S.TransactionDate,\r\n    S.Amount\r\nFROM\r\n    TblServices AS S\r\nINNER JOIN\r\n    Transfer AS T ON S.TransactionID = T.TransactionID\r\nWHERE\r\n    T.AccountID = @1\r\n\r\nUNION ALL\r\n\r\nSELECT\r\n    S.TransactionDate,\r\n    S.Amount\r\nFROM\r\n    TblServices AS S\r\nINNER JOIN\r\n    CashWithdrawal AS W ON S.TransactionID = W.TransactionID\r\nWHERE\r\n    W.AccountID = @1\r\n\r\nUNION ALL\r\n\r\nSELECT\r\n    S.TransactionDate,\r\n    S.Amount\r\nFROM\r\n    TblServices AS S\r\nINNER JOIN\r\n    CashDeposit AS D ON S.TransactionID = D.TransactionID\r\nWHERE\r\n    D.AccountID = @1;\r\n";

                    using (SqlCommand command = new SqlCommand(q2, connection))

                    {
                         command.Parameters.AddWithValue("@1", accid);
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                          while(reader.Read())
                            {
                                resultList.Add(new ResultModel
                                {
                                    TransactionDate = reader.GetDateTime(0),
                                    Amount = reader.GetDecimal(1)
                                });




                            }



                        }



                    }

                    string q3 = $"SELECT AccBal FROM TblAccount WHERE AccID =@1";
                    using (SqlCommand command = new SqlCommand(q3, connection))

                    {
                        command.Parameters.AddWithValue("@1", accid);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            resultList.Add(new ResultModel
                            {
                                TransactionDate = DateTime.Now,
                                Amount =Convert.ToDecimal(reader.GetDouble(0))
                            });



                        }



                    }
                    mainWindowInstance.Listaprikaza.ItemsSource = resultList;

                    Close();


                }





            }

            else
            {
                MessageBox.Show("nepravilan unos naloga");
            }
        }
    }
}
public class ResultModel
{
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
   
}
