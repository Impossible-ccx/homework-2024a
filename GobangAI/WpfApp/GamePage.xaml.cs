using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp
{
    /// <summary>
    /// GamePage.xaml 的交互逻辑
    /// </summary>
    internal class SquareButton : Button
    {
        public int x;
        public int y;
    }
    public partial class GamePage : Page
    {
        
        public GamePage()
        {
            InitializeComponent();
            IniCanvas();
            var gm = ((GamePageView)DataContext)._TheGameManager;
            gm.MapFlush += (sender, e) => MapFlush(sender, e);
            gm.PlayerChange += (sender, e) => PlayerChange(sender, e);
            gm.Start();
        }
        private void PlayerChange(object? sender, EventArgs e)
        {
            var gm = ((GamePageView)DataContext)._TheGameManager;
            if(gm.OnlinePlayer == PlayerSide.Player1)
            {
                P1L.Visibility = Visibility.Visible;
                P2L.Visibility = Visibility.Hidden;
            }
            else
            {
                P1L.Visibility = Visibility.Hidden;
                P2L.Visibility = Visibility.Visible;
            }
        }
        private void SquareDown(SquareButton sender)
        {
            Node p = new Node();
            p.X = sender.x; p.Y = sender.y;
            RTIO.OnTouchedNode = p;
        }
        private void MapFlush(object? sender, EventArgs e)
        {
            var gm = ((GamePageView)DataContext)._TheGameManager;
            foreach (var bt in theMap.Children)
            {
                var p = bt as SquareButton;
                if(p == null)
                {
                    continue;
                }
                switch(gm.Map.Map[p.x, p.y])
                {
                    case DrawType.Empty:
                        p.Background = new SolidColorBrush(Colors.WhiteSmoke);
                        break;
                    case DrawType.Wall:
                        p.Background = new SolidColorBrush(Colors.Black);
                        break;
                    case DrawType.Player1:
                        p.Background = new SolidColorBrush(Colors.Red);
                        break;
                    case DrawType.Player2:
                        p.Background = new SolidColorBrush(Colors.Blue);
                        break;
                }

            }
        }
        private void IniCanvas()
        {
            var gm = ((GamePageView)DataContext)._TheGameManager;
            int bx = 400 / gm.Maxx;
            int by = 400 / gm.Maxy;
            for (int i = 0;i < gm.Maxx;i++)
            {
                for(int j = 0;j < gm.Maxy; j++)
                {
                    var bt = new SquareButton() { Width = bx,Height = by};
                    bt.x = i;
                    bt.y = j;
                    Canvas.SetTop(bt, j * by);
                    Canvas.SetLeft(bt, i * bx);
                    bt.Click += (sender, e) => { SquareDown((SquareButton)sender); };
                    theMap.Children.Add(bt);
                }
            }
        }
        private void Button_Quit(object sender, RoutedEventArgs e)
        {
            var top = (MainWindow)Window.GetWindow(this);
            top.topFrame.Navigate(new Uri("StartPage.xaml",UriKind.Relative));
            var gm = ((GamePageView)DataContext)._TheGameManager;
            gm.IsGameEnded = true;
        }
    }
    public class GamePageView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(null));
        }
        string _Player1;
        public string Player1
        {
            get { return _Player1!; }
            set { _Player1 = value; OnPropertyChanged(); }
        }
        string _Player2;
        public string Player2
        {
            get { return _Player2!; }
            set { _Player2 = value; OnPropertyChanged(); }
        }
        readonly internal GameManager _TheGameManager;
        
        public GamePageView()
        {
            _Player1 = MainWindowView.TopView!.Player1!;
            _Player2 = MainWindowView.TopView!.Player2!;
            _TheGameManager = new GameManager(Player1, Player2, 12, 12);
        }
    }
}
