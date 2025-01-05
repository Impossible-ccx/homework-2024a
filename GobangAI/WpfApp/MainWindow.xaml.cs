using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
    public class MainWindowView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        Uri _OnlinePageUri = new Uri("StartPage.xaml", UriKind.Relative);
        public Uri OnlinePageUri
        {
            get { return _OnlinePageUri; }
            set { _OnlinePageUri = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null)); }
        }
        public string Player1 { get; set; } = string.Empty;
        public string Player2 { get; set; } = string.Empty;
        public static MainWindowView? TopView { get; private set; }
        public MainWindowView()
        {
            TopView = this;
        }
    }
}