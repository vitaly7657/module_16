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

namespace module_16
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
        }

        public AddWindow(DataRow row):this()
        {
            cancel_add_clien_button.Click += delegate
            {
                this.DialogResult = false;
            };
            add_client_button.Click += delegate
            {
                row["Client_surname"] = txt_Client_surname.Text;
                row["Client_name"] = txt_Client_name.Text;
                row["Client_patronymic"] = txt_Client_patronymic.Text;
                row["Phone_number"] = txt_Phone_number.Text;
                row["Email"] = txt_Email.Text;
                this.DialogResult = !false;
            };
        }
        
    }
}
