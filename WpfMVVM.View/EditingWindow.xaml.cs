using System.Windows;
using WpfMVVM.ViewModel;

namespace WpfMVVM.View
{
    /// <summary>
    /// Interaction logic for EditingWindow.xaml
    /// </summary>
    public partial class EditingWindow : Window
    {
        public EditingWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EditingWindowViewModel viewModel = (EditingWindowViewModel)DataContext;
            viewModel.Done += ViewModel_Done;
        }

        private void ViewModel_Done(object sender, EditingWindowViewModel.DoneEventArgs e)
        {

            MessageBox.Show(e.Message);
        }
  
    }
}
