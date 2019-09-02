using System.Windows;
using WpfMVVM.ViewModel;

namespace WpfMVVM.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainWindowViewModel mainViewModel = new MainWindowViewModel(Mediator.Instance);
            this.DataContext = mainViewModel;
        }

        private void NewBtn_Click(object sender, RoutedEventArgs e)
        {
            EditingWindow newWindow = new EditingWindow();
            newWindow.DataContext = new EditingWindowViewModel(Mediator.Instance);
            newWindow.ShowDialog();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindowViewModel viewModel = (MainWindowViewModel)DataContext;
            EditingWindow editWindow = new EditingWindow();
            editWindow.DataContext = new EditingWindowViewModel(viewModel.CurrentUser.Clone(), Mediator.Instance);
            editWindow.ShowDialog();
        }
    }
}
