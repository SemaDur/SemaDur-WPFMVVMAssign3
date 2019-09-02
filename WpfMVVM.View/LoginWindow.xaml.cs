using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using WpfMVVM.ViewModel;

namespace WpfMVVM.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private LoginWindowViewModel loginModel;

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT COUNT(1) FROM Users WHERE UserName=@userName AND UserPass=@userPass", conn);

                command.CommandType = CommandType.Text;
                SqlParameter userNameParam = new SqlParameter("@userName", SqlDbType.NVarChar);
                userNameParam.Value = UserNameTxt.Text;

                SqlParameter userPassParam = new SqlParameter("@userPass", SqlDbType.NVarChar);
                userPassParam.Value = UserPassTxt.Text;

                command.Parameters.Add(userNameParam);
                command.Parameters.Add(userPassParam);

                int rows = Convert.ToInt32(command.ExecuteScalar());

                if (rows == 1)
                {
                    MainWindow dashbord = new MainWindow();
                    dashbord.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong User or Password.");
                }
            }
        }

        
    }
}
