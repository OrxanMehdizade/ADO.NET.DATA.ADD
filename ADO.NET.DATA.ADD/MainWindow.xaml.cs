using System.Data.SqlClient;
using System.Text;
using System.Windows;

namespace ADO.NET.DATA.ADD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetDataFromDatabase();
        }

        private void GetDataFromDatabase()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string query = "SELECT FirstName, LastName FROM Authors";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    StringBuilder fullNameBuilder = new StringBuilder();

                    while (reader.Read())
                    {
                        string name = reader.GetString(reader.GetOrdinal("FirstName"));
                        string surname = reader.GetString(reader.GetOrdinal("LastName"));
                        string fullName = $"{name + " " + surname}\n";
                        fullNameBuilder.AppendLine(fullName);

                    }

                    textBlock.Text = fullNameBuilder.ToString();
                }

                reader.Close();
            }
        }
        private void AddDataToDatabase(string name, string surname)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string query = "INSERT INTO Authors (FirstName, LastName) VALUES (@Name, @Surname)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Surname", surname);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("ADD.OK.");
                    GetDataFromDatabase();
                }
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string addname1 = addname.Text;
            string addsurname1 = addsurname.Text;
            AddDataToDatabase(addname1, addsurname1);

        }
    }
}

