using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfMVVM.Model;
using System.Data;
using System.Windows;
using WpfMVVM.ViewModel;

namespace WpfMVVM.ViewModel
{
    public class LoginWindowViewModel
    {  
        private string userName;
        private string userPass;
        
        public LoginWindowViewModel(string user, string pass)
        {
            this.userName = user;
            this.userPass = pass;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT COUNT(1) FROM Users WHERE UserName=@userName AND UserPass=@userPass", conn);

                command.CommandType = CommandType.Text;
                SqlParameter userNameParam = new SqlParameter("@userName", SqlDbType.NVarChar);
                userNameParam.Value = this.userName;

                SqlParameter userPassParam = new SqlParameter("@userPass", SqlDbType.NVarChar);
                userPassParam.Value = this.userPass;

                command.Parameters.Add(userNameParam);
                command.Parameters.Add(userPassParam);

                int rows = Convert.ToInt32(command.ExecuteScalar());

                if (rows == 1)
                {
                    //MainWindow dashbord = new MainWindow();
                    //dashbord.Show();
                    //this.Close();
                }
                else
                {
                    MessageBox.Show("Wrong User or Password.");
                }
            }

        }
    }
}
