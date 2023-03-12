using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace module_16
{
    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        SqlConnection connection;
        SqlDataAdapter dataAdapter;
        DataTable dataTable;
        DataRowView rowView;

        OleDbConnection connectionAccess;
        OleDbDataAdapter dataAdapterAccess;
        DataTable dataTableAccess;
        DataRowView rowViewAccess;
        public MainWindow()
        {
            InitializeComponent();

            

            add_product_button.IsEnabled = false;
            delete_product_button.IsEnabled = false;

            LocalDB_Connection();

            //AccessDB_Connection();
        }

        public void LocalDB_Connection()
        {
            try
            {
                //INIT---------------------
                //строка подключения к базе
                var connectionString = new SqlConnectionStringBuilder()
                {
                    DataSource = @"(localdb)\MSSQLLocalDB",
                    InitialCatalog = "LocalDB",
                    IntegratedSecurity = true,
                    Pooling = false
                };

                //создание подключения к базе
                connection = new SqlConnection(connectionString.ConnectionString);

                //запись новой таблицы в память
                dataTable = new DataTable();

                //создание нового адаптера с командами для подключения к базе
                dataAdapter = new SqlDataAdapter();


                //SELECT---------------------
                var sql = @"select * from Clients"; //скрипт заполнения таблицы
                dataAdapter.SelectCommand = new SqlCommand(sql, connection); //команда select через адаптер

                //INSERT---------------------
                sql = @"insert into Clients(Client_surname,Client_name,Client_patronymic,Phone_number,Email)
                    values(@Client_surname,@Client_name,@Client_patronymic,@Phone_number,@Email);
                    set @Id = @@identity;";
                dataAdapter.InsertCommand = new SqlCommand(sql, connection);

                dataAdapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 4, "Id").Direction = ParameterDirection.Output;
                dataAdapter.InsertCommand.Parameters.Add("@Client_surname", SqlDbType.NVarChar, 50, "Client_surname");
                dataAdapter.InsertCommand.Parameters.Add("@Client_name", SqlDbType.NVarChar, 50, "Client_name");
                dataAdapter.InsertCommand.Parameters.Add("@Client_patronymic", SqlDbType.NVarChar, 50, "Client_patronymic");
                dataAdapter.InsertCommand.Parameters.Add("@Phone_number", SqlDbType.NVarChar, 50, "Phone_number");
                dataAdapter.InsertCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 50, "Email");

                //UPDATE---------------------
                sql = @"update Clients set		
		            Client_surname = @Client_surname,
		            Client_name = @Client_name,
		            Client_patronymic = @Client_patronymic,
		            Phone_number = @Phone_number,
		            Email = @Email

	                where Id=@Id";

                dataAdapter.UpdateCommand = new SqlCommand(sql, connection);                
                dataAdapter.UpdateCommand.Parameters.Add("@Id", SqlDbType.Int, 4, "Id").SourceVersion = DataRowVersion.Original;
                dataAdapter.UpdateCommand.Parameters.Add("@Client_surname", SqlDbType.NVarChar, 50, "Client_surname");
                dataAdapter.UpdateCommand.Parameters.Add("@Client_name", SqlDbType.NVarChar, 50, "Client_name");
                dataAdapter.UpdateCommand.Parameters.Add("@Client_patronymic", SqlDbType.NVarChar, 50, "Client_patronymic");
                dataAdapter.UpdateCommand.Parameters.Add("@Phone_number", SqlDbType.NVarChar, 50, "Phone_number");
                dataAdapter.UpdateCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 50, "Email");

                //DELETE---------------------
                sql = "delete from Clients where Id = @Id";
                dataAdapter.DeleteCommand = new SqlCommand(sql, connection);
                dataAdapter.DeleteCommand.Parameters.Add("@Id", SqlDbType.Int, 4, "Id");

                //заполнение методом Fill таблицы dataTable из базы
                dataAdapter.Fill(dataTable);

                //заполнение таблицы на форме
                gridView.DataContext = dataTable.DefaultView;
            }

            catch (Exception ex)
            {
                MessageBox.Show($"LocalDB error: {ex.Message}");
            }

            finally
            {
                if (connection != null)
                    MessageBox.Show("LocalDB connection OK");
            }
        }

        string loginDB;
        string passwordDB;

        private void update_accessDB_Click(object sender, RoutedEventArgs e)
        {
            if (login_tb.Text == "Admin" && pass_tb.Text == "123")
            {
                add_product_button.IsEnabled = true;
                delete_product_button.IsEnabled = true;
                loginDB = login_tb.Text.ToString();
                passwordDB = pass_tb.Text.ToString();
                AccessDB_Connection();
            }
            else
            {
                MessageBox.Show("Неверные данные");
            }
        }

        public void AccessDB_Connection()
        {
            
            try
            {
                //строка подключения к базе
                var connectionStringAccess = new OleDbConnectionStringBuilder()
                {
                    Provider = "Microsoft.ACE.OLEDB.12.0",
                    DataSource = @"C:\Users\Saske\Desktop\_C SHARP\Практические работы\Модуль 16\module_16\bin\Debug\AccessDB.accdb"
                };

                //ввод логина
                connectionStringAccess.Add("User ID", loginDB);

                //ввод пароля
                connectionStringAccess.Add("Jet OLEDB:Database Password", passwordDB);

                //создание подключения к базе
                connectionAccess = new OleDbConnection(connectionStringAccess.ConnectionString);

                //запись новой таблицы в память
                dataTableAccess = new DataTable();

                //создание нового адаптера с командами для подключения к базе Access
                dataAdapterAccess = new OleDbDataAdapter();

                //SELECT---------------------
                var sqlAccess = @"select * from Orders";
                dataAdapterAccess.SelectCommand = new OleDbCommand(sqlAccess, connectionAccess);

                //INSERT---------------------
                sqlAccess = @"INSERT INTO Orders(Email_access,Product_code,Product_name)
                            VALUES (@Email_access,@Product_code,@Product_name);";

                dataAdapterAccess.InsertCommand = new OleDbCommand(sqlAccess, connectionAccess);
                dataAdapterAccess.InsertCommand.Parameters.Add("@Email_access", OleDbType.WChar, 50, "Email_access");
                dataAdapterAccess.InsertCommand.Parameters.Add("@Product_code", OleDbType.WChar, 50, "Product_code");
                dataAdapterAccess.InsertCommand.Parameters.Add("@Product_name", OleDbType.WChar, 50, "Product_name");

                //DELETE---------------------
                sqlAccess = "delete from Orders where Id_access = @Id_access";
                dataAdapterAccess.DeleteCommand = new OleDbCommand(sqlAccess, connectionAccess);
                dataAdapterAccess.DeleteCommand.Parameters.Add("Id_access", OleDbType.Integer, 4, "Id_access");

                //заполнение методом Fill таблицы dataTableAccess из базы
                dataAdapterAccess.Fill(dataTableAccess);

                //заполнение таблицы на форме
                gridViewAccess.DataContext = dataTableAccess.DefaultView;
            }

            catch (Exception ex)
            {
                MessageBox.Show($"AccessDB error: {ex.Message}");
            }

            finally
            {
                if (connectionAccess != null)
                    MessageBox.Show("AccessDB connection OK");
            }

        }
        private void delete_client_button_Click(object sender, RoutedEventArgs e)
        {
            rowView = (DataRowView)gridView.SelectedItem;
            rowView.Row.Delete();
            dataAdapter.Update(dataTable);
        }

        private void gridView_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            rowView = (DataRowView)gridView.SelectedItem;
            rowView.BeginEdit();
        }

        private void gridView_CurrentCellChanged(object sender, EventArgs e)
        {
            if (rowView == null) return;
            rowView.EndEdit();
            dataAdapter.Update(dataTable);
        }

        private void open_AddWindow_button_Click(object sender, RoutedEventArgs e)
        {
            DataRow r = dataTable.NewRow();
            AddWindow add = new AddWindow(r);
            add.ShowDialog();

            if(add.DialogResult.Value)
            {
                dataTable.Rows.Add(r);
                dataAdapter.Update(dataTable);
            }
            dataAdapter.Update(dataTable);
        }

        private void add_product_button_Click(object sender, RoutedEventArgs e)
        {
            DataRow r = dataTableAccess.NewRow();
            AddWindowAccess add = new AddWindowAccess(r);
            add.ShowDialog();

            if (add.DialogResult.Value)
            {
                dataTableAccess.Rows.Add(r);
                dataAdapterAccess.Update(dataTableAccess);
            }
            dataAdapterAccess.Update(dataTableAccess);
            dataTableAccess.Clear();
            dataAdapterAccess.Fill(dataTableAccess);
        }

        private void delete_product_button_Click(object sender, RoutedEventArgs e)
        {
            rowViewAccess = (DataRowView)gridViewAccess.SelectedItem;
            rowViewAccess.Row.Delete();
            dataAdapterAccess.Update(dataTableAccess);
            dataTableAccess.Clear();
            dataAdapterAccess.Fill(dataTableAccess);
        }        

        private void help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Логин Admin, пароль 123");
        }
    }
}
