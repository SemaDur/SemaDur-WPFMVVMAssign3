using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using WpfMVVM.Model;

namespace WpfMVVM.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private User currentUser;
        private UserCollection userList;
        private ListCollectionView userListView;

        private string filterText;

        private Mediator mediator;

        public User CurrentUser
        {
            get { return currentUser; }
            set
            {
                if (currentUser == value)
                {
                    return;
                }
                currentUser = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CurrentUser"));
            }
        }

        public UserCollection UserList
        {
            get { return userList; }
            set
            {
                if (userList == value)
                {
                    return;
                }
                userList = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UserList"));
            }
        }

        public ListCollectionView UserListView
        {
            get { return userListView; }
            set
            {
                if (userListView == value)
                {
                    return;
                }
                userListView = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UserListView"));
            }
        }

        public String FilterText
        {
            get { return filterText; }
            set
            {
                if (filterText == value)
                {
                    return;
                }
                filterText = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FilterText"));
            }
        }

        private ICommand deleteCommand;

        public ICommand DeleteCommand
        {
            get { return deleteCommand; }
            set
            {
                if (deleteCommand == value)
                {
                    return;
                }
                deleteCommand = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DeleteCommand"));
            }
        }

        void DeleteExecute(object obj)
        {
            CurrentUser.DeleteUser();
            UserList.Remove(CurrentUser);
        }

        bool CanDelete(object obj)
        {

            if (CurrentUser == null) return false;

            return true;
        }

        public MainWindowViewModel(Mediator mediator)
        {

            this.mediator = mediator;

            DeleteCommand = new RelayCommand(DeleteExecute, CanDelete);

            this.PropertyChanged += MainWindowViewModel_PropertyChanged;

            UserList = UserCollection.GetAllUser();
            UserListView = new ListCollectionView(UserList);
            UserListView.Filter = UserFilter;
            CurrentUser = new User();

            mediator.Register("UserChange", UserChanged);

        }

        private void UserChanged(object obj)
        {
            User user = (User)obj;

            int index = UserList.IndexOf(user);

            if (index != -1)
            {
                UserList.RemoveAt(index);
                UserList.Insert(index, user);
            }
            else
            {
                UserList.Add(user);
            }
        }

        private void MainWindowViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("FilterText"))
            {
                UserListView.Refresh();
            }
        }

        private bool UserFilter(object obj)
        {
            if (FilterText == null) return true;
            if (FilterText.Equals("")) return true;

            User user = obj as User;
            return (user.UserName.ToLower().StartsWith(FilterText.ToLower()));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }
}
