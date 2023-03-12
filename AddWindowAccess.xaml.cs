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
    /// Логика взаимодействия для AddWindowAccess.xaml
    /// </summary>
    public partial class AddWindowAccess : Window
    {
        public AddWindowAccess()
        {
            InitializeComponent();
        }

        public AddWindowAccess(DataRow row) : this()
        {
            cancel_add_product_button.Click += delegate
            {
                this.DialogResult = false;
            };
            add_product_button.Click += delegate
            {
                row["Email_access"] = txt_Email_Access.Text;
                row["Product_code"] = txt_Product_Code.Text;
                row["Product_name"] = txt_Product_Name.Text;                
                this.DialogResult = !false;
            };
        }
    }
}
