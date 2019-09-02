using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMVVM.Model
{
    public class UserCollection : ObservableCollection<User>
    {
        public static UserCollection GetAllUser()
        {
            UserCollection users = new UserCollection();
            User user = null;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("SELECT Id, UserName, UserPass, IsAdmin FROM Users", conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = User.GetUserFromResultSet(reader);

                        users.Add(user);
                    }
                }

            }
            return users;
        }
    }
}
