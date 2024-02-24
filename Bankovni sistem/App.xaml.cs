using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace Bankovni_sistem
{
    public partial class App : Application
    {
       
        public SqlConnection YourSqlConnection { get; private set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            
            string connectionString = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;

           
            YourSqlConnection = new SqlConnection(connectionString);

            try
            {
                YourSqlConnection.Open();

               
            }
            catch (Exception ex)
            {
                
                MessageBox.Show($"Error opening database connection: {ex.Message}");
                Shutdown(); 
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            
            YourSqlConnection?.Close();
        }

        private void Application_Startup_1(object sender, StartupEventArgs e)
        {

        }
    }
}

