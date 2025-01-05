using System.Data;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
namespace WpfApp
{
    public static class PlayerDIService
    {
        public static Dictionary<string, Type> PlayersDict { get; private set; } = [];
        public static List<PlayerInfo> AvailablePlayers { get; private set; } = [];
        static PlayerDIService()
        {
            var dlls = Directory.GetFiles("./Players").Where(f => Regex.IsMatch(f, ".*dll")).ToArray();
            foreach (var dll in dlls)
            {
                try
                {
                    Assembly playerAssembly = Assembly.LoadFrom(dll);
                    foreach (var playerT in playerAssembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IPlayer))))
                    {
                        PlayersDict[(string)playerT.GetProperties().Single(p => p.Name == "Name").GetValue(null)!] = playerT;
                        AvailablePlayers.Add(new PlayerInfo((string)playerT.GetProperties().Single(p => p.Name == "Name").GetValue(null)!,
                            (string)playerT.GetProperties().Single(p => p.Name == "Description").GetValue(null)!));
                    }
                }
                catch
                {
                    continue;
                }
            }
            DefaultReg();
        }
        static private void DefaultReg()
        {
            PlayersDict[HumanPlayer.Name] = typeof(HumanPlayer);
            AvailablePlayers.Add(new PlayerInfo(HumanPlayer.Name, HumanPlayer.Description));
            PlayersDict[CSAI.CSAI.Name] = typeof(CSAI.CSAI);
            AvailablePlayers.Add(new PlayerInfo(CSAI.CSAI.Name, CSAI.CSAI.Description));
        }
    }
    public class GameManager
    {
        public bool FlagConfirmInput = false;
        public bool IsGameEnded = false;
        public IPlayer Player1 { get; }
        public IPlayer Player2 { get; }
        public PlayerSide OnlinePlayer { get; private set; }
        public int Maxx { get; }
        public int Maxy { get; }
        public GobangMap Map { get; private set; }
        public GameManager(string playerName1, string playerName2, int maxx, int maxy)
        {
            if (!(PlayerDIService.PlayersDict.ContainsKey(playerName1)
                && PlayerDIService.PlayersDict.ContainsKey(playerName2)))
            {
                throw new Exception("No such a player");
            }
            Player1 = (IPlayer)Activator.CreateInstance(PlayerDIService.PlayersDict[playerName1])!;

            Player2 = (IPlayer)Activator.CreateInstance(PlayerDIService.PlayersDict[playerName2])!;
            this.Maxx = maxx;
            this.Maxy = maxy;
            Map = new GobangMap(maxx, maxy);
            OnlinePlayer = PlayerSide.Player1;
        }
        public event EventHandler? MapFlush;
        public event EventHandler? PlayerChange;
        public async void Start()
        {
            Task chrono;
            while (true)
            {
                chrono = new Task(() => Thread.Sleep(200));
                chrono.Start();

                IPlayer player;
                PlayerSide next;
                DrawType drawType;
                if (OnlinePlayer == PlayerSide.Player1)
                {
                    player = Player1;
                    drawType = DrawType.Player1;
                    next = PlayerSide.Player2;
                }
                else
                {
                    player = Player2;
                    drawType = DrawType.Player2;
                    next = PlayerSide.Player1;
                }

                Node p = await Task.Run(() => player.Draw((new DMap(Map.Map, Maxx, Maxy)), OnlinePlayer));
                               
                if (IsGameEnded)
                {
                    break;
                }
                DrawNode tar; 
                tar.x = p.X; tar.y = p.Y;
                tar.drawType = drawType;

                if (Map.Draw(tar))
                {
                    MapFlush?.Invoke(this, EventArgs.Empty);
                    switch (Map.CheckWinner())
                    {
                        case null:
                            OnlinePlayer = next;
                            PlayerChange?.Invoke(this, EventArgs.Empty);
                            break;
                        case PlayerSide.Player1:
                            MessageBox.Show($"Red won!{Environment.NewLine}Click \"Quit\" to quit");
                            IsGameEnded = true;
                            break;
                        case PlayerSide.Player2:
                            MessageBox.Show($"Bule won!{Environment.NewLine}Click \"Quit\" to quit");
                            IsGameEnded = true;
                            break;
                    }
                }
                else
                {
                    player.OnFailToDraw(Map.Map, p);
                    continue;
                }
                chrono.Wait();
            }
        }
    }
    public static class RTIO
    {
        public static Node? OnTouchedNode {  get; set; }
    }
}
