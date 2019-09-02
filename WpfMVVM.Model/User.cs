using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace WpfMVVM.Model
{
    public class User : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private int _id;
        private string _userName;
        private string _userPass;
        private int _isAdmin;
        private Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id == value)
                {
                    return;
                }
                _id = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Id"));

            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName == value)
                {
                    return;
                }
                _userName = value;

                List<string> errors = new List<string>();
                bool valid = true;

                if (value == null || value == "")
                {
                    errors.Add("User name can't be empty.");
                    SetErrors("UserName", errors);
                    valid = false;
                }

                if (!Regex.Match(value, @"^[a-zA-Z]+$").Success)
                {
                    errors.Add("User Name can only contain letters.");
                    SetErrors("UserName", errors);
                    valid = false;
                }

                if (valid)
                {
                    ClearErrors("UserName");
                }

                OnPropertyChanged(new PropertyChangedEventArgs("UserName"));
            }
        }

        public string UserPass
        {
            get { return _userPass; }
            set
            {
                if (_userPass == value)
                {
                    return;
                }
                _userPass = value;

                List<string> errors = new List<string>();
                bool valid = true;

                if (value == null || value == "")
                {
                    errors.Add("Password can't be empty.");
                    SetErrors("UserPass", errors);
                    valid = false;
                }
                
                if (valid)
                {
                    ClearErrors("UserPass");
                }

                OnPropertyChanged(new PropertyChangedEventArgs("UserPass"));
            }
        }

        public int IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                if (_isAdmin == value)
                {
                    return;
                }
                _isAdmin = value;

                List<string> errors = new List<string>();
                bool valid = true;

                if (value < 0 || value > 1)
                {
                    errors.Add("Can only be 0 or 1.");
                    SetErrors("IsAdmin", errors);
                    valid = false;
                }

                if (valid)
                {
                    ClearErrors("IsAdmin");
                }

                OnPropertyChanged(new PropertyChangedEventArgs("IsAdmin"));

            }
        }

        public bool HasErrors
        {
            get
            {
                return (errors.Count > 0);
            }
        }

        public User(int Id, string UserName, string UserPass, int IsAdmin)
        {
            this.Id = Id;
            this.UserName = UserName;
            this.UserPass = UserPass;
            this.IsAdmin = IsAdmin;
        }

        public User(string UserName, string UserPass, int IsAdmin)
        {
            this.UserName = UserName;
            this.UserPass = UserPass;
            this.IsAdmin = IsAdmin;
        }

        public User()
        {
            UserName = "";
            UserPass = "";
            IsAdmin = 0;
        }

        public static User GetUserFromResultSet(SqlDataReader reader)
        {
            User user = new User((int)reader["Id"], (string)reader["UserName"], (string)reader["UserPass"], (int)reader["IsAdmin"]);
            return user;
        }

        public void DeleteUser()
        {
            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("DELETE FROM Users WHERE Id=@Id", conn);

                SqlParameter myParam = new SqlParameter("@Id", SqlDbType.Int, 11);
                myParam.Value = this.Id;

                command.Parameters.Add(myParam);

                int rows = command.ExecuteNonQuery();

            }
        }

        public void UpdateUser()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("UPDATE Users SET UserName=@userName, UserPass=@userPass, IsAdmin=@isAdmin WHERE Id=@Id", conn);

                SqlParameter userNameParam = new SqlParameter("@userName", SqlDbType.NVarChar);
                userNameParam.Value = this.UserName;

                SqlParameter userPassParam = new SqlParameter("@userPass", SqlDbType.NVarChar);
                userPassParam.Value = this.UserPass;

                SqlParameter isAdminParam = new SqlParameter("@isAdmin", SqlDbType.Int, 11);
                isAdminParam.Value = this.IsAdmin;

                SqlParameter myParam = new SqlParameter("@Id", SqlDbType.Int, 11);
                myParam.Value = this.Id;

                command.Parameters.Add(userNameParam);
                command.Parameters.Add(userPassParam);
                command.Parameters.Add(isAdminParam);
                command.Parameters.Add(myParam);

                int rows = command.ExecuteNonQuery();

            }
        }

        public void Insert()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                conn.Open();

                SqlCommand command = new SqlCommand("INSERT INTO Users(UserName, UserPass, IsAdmin) VALUES(@userName, @userPass, @isAdmin); SELECT IDENT_CURRENT('Users');", conn);

                SqlParameter userNameParam = new SqlParameter("@userName", SqlDbType.NVarChar);
                userNameParam.Value = this.UserName;

                SqlParameter userPassParam = new SqlParameter("@userPass", SqlDbType.NVarChar);
                userPassParam.Value = this.UserPass;

                SqlParameter isAdminParam = new SqlParameter("@isAdmin", SqlDbType.Int, 11);
                isAdminParam.Value = this.IsAdmin;

                command.Parameters.Add(userNameParam);
                command.Parameters.Add(userPassParam);
                command.Parameters.Add(isAdminParam);

                var id = command.ExecuteScalar();

                if (id != null)
                {
                    this.Id = Convert.ToInt32(id);
                }

            }
        }

        public void Save()
        {
            if (Id == 0)
            {
                Insert();
            }
            else
            {
                UpdateUser();
            }
        }

        private void SetErrors(string propertyName, List<string> propertyErrors)
        {
            // Clear any errors that already exist for this property.
            errors.Remove(propertyName);
            // Add the list collection for the specified property.
            errors.Add(propertyName, propertyErrors);
            // Raise the error-notification event.
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void ClearErrors(string propertyName)
        {
            // Remove the error list for this property.
            errors.Remove(propertyName);
            // Raise the error-notification event.
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                // Provide all the error collections.
                return (errors.Values);
            }
            else
            {
                // Provice the error collection for the requested property
                // (if it has errors).
                if (errors.ContainsKey(propertyName))
                {
                    return (errors[propertyName]);
                }
                else
                {
                    return null;
                }
            }
        }

        public User Clone()
        {
            User clonedUser = new User();
            clonedUser.UserName = this.UserName;
            clonedUser.UserPass = this.UserPass;
            clonedUser.IsAdmin = this.IsAdmin;
            clonedUser.Id = this.Id;

            return clonedUser;
        }

        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            User objUser = (User)obj;

            if (objUser.Id == this.Id) return true;

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }
}
