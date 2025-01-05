using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfApp
{
    /// <summary>
    /// StartPage.xaml 的交互逻辑
    /// </summary>
    public partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void Start_Button(object sender, RoutedEventArgs e)
        {
            var p = (StartPageView)DataContext;
            if (p.Player1 != null && p.Player2 != null)
            {
                var top = (MainWindow)Window.GetWindow(this);
                top.topFrame.Navigate(p.GameUri);
                var t = (MainWindowView)top.DataContext;
                t.Player1 = p.Player1.Name;
                t.Player2 = p.Player2.Name;
            }
            else
            {
                MessageBox.Show("!!!");
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StartPageView p = (StartPageView)DataContext;
            p.PlayerOnClicked = (PlayerInfo)((TextBlock)sender).DataContext;
        }

        private void SelectP1(object sender, RoutedEventArgs e)
        {
            StartPageView p = (StartPageView)DataContext;
            p.Player1 = p.PlayerOnClicked;
        }

        private void SelectP2(object sender, RoutedEventArgs e)
        {
            StartPageView p = (StartPageView)DataContext;
            p.Player2 = p.PlayerOnClicked;
        }
    }

    public class StartPageView : INotifyPropertyChanged
    {
        PlayerInfo? _Player1;
        public PlayerInfo? Player1
        {
            get { return _Player1; }
            set
            {
                _Player1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
            }
        }
        PlayerInfo? _Player2;
        public PlayerInfo? Player2
        {
            get { return _Player2; }
            set { _Player2 = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null)); }
        }
        PlayerInfo? _PlayerOnClicked = new PlayerInfo(string.Empty, string.Empty);
        public PlayerInfo? PlayerOnClicked
        {
            get { return _PlayerOnClicked; }
            set { _PlayerOnClicked = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null)); }
        }
        public static List<PlayerInfo> AvailabelPlayers
        {
            get
            {
                return PlayerDIService.AvailablePlayers;
            }
        }
        public Uri GameUri { get; set; } = new Uri("GamePage.xaml", UriKind.Relative);

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
