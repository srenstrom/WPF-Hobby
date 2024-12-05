using MVVM.Models;
using MVVM.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HobbyViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new HobbyViewModel();
            Loaded += HobbyView_Loaded;
            DataContext = viewModel; // Hela main window kopplas till innehållet i viewmodel
        }
        private async void HobbyView_Loaded(object sender, RoutedEventArgs e)
        {
            await viewModel.LoadAsync();
        }

        
    }
}