using System.Windows;

namespace WpfApp
{
    public struct DrawNode
    {
        public int x;
        public int y;
        public DrawType drawType;
    }
    public class PlayerInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public PlayerInfo(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
    public class GobangMap
    {
        public readonly int maxx;
        public readonly int maxy;
        public DrawType[,] Map { get; private set; }
        public GobangMap(int maxx, int maxy)
        {
            Map = new DrawType[maxx, maxy];
            this.maxx = maxx;
            this.maxy = maxy;
            for(int i = 0; i < maxx; i++)
            {
                for(int j = 0; j < maxy; j++)
                {
                    Map[i, j] = DrawType.Empty;
                }
            }
        }
        public bool Draw(DrawNode target)
        {
            if (Map[target.x, target.y] == DrawType.Empty)
            {
                Map[target.x, target.y] = target.drawType;
                return true;
            }
            else
            {
                return false;
            }
        }
        public PlayerSide? CheckWinner()
        {
            //x+y- 判定有问题
            {
                int count = 1;
                DrawType last = DrawType.Empty;
                for (int i = 0; i < maxx; i++)
                {
                    for (int j = 0; j < maxy; j++)
                    {
                        if (Map[i, j] == last && Map[i, j] != DrawType.Empty)
                        {
                            count++;
                        }
                        else
                        {
                            count = 1;
                        }
                        last = Map[i, j];
                        if(count == 5)
                        {
                            if(last == DrawType.Player1)
                            {
                                return PlayerSide.Player1;
                            }
                            else
                            {
                                return PlayerSide.Player2;
                            }
                        }
                    }
                }
            }
            {
                int count = 1;
                DrawType last = DrawType.Empty;
                for (int i = 0; i < maxy; i++)
                {
                    for (int j = 0; j < maxx; j++)
                    {
                        if (Map[j, i] == last && Map[j, i] != DrawType.Empty)
                        {
                            count++;
                        }
                        else
                        {
                            count = 1;
                        }
                        last = Map[j, i];
                        if (count == 5)
                        {
                            if (last == DrawType.Player1)
                            {
                                return PlayerSide.Player1;
                            }
                            else
                            {
                                return PlayerSide.Player2;
                            }
                        }
                    }
                }
            }
            {
                int count = 1;
                DrawType last = DrawType.Empty;
                for (int i = -maxy + 1; i <= maxx - 1; i++)
                {
                    for (int j = Math.Max(0, -i); j <= maxy - 1 && i + j <= maxx - 1; j++)
                    {

                        if (Map[i + j, j] == last && Map[i + j, j] != DrawType.Empty)
                        {
                            count++;
                        }
                        else
                        {
                            count = 1;
                        }
                        last = Map[i + j, j];
                        if (count == 5)
                        {
                            if (last == DrawType.Player1)
                            {
                                return PlayerSide.Player1;
                            }
                            else
                            {
                                return PlayerSide.Player2;
                            }
                        }
                    }
                }
            }
            {
                int count = 1;
                DrawType last = DrawType.Empty;
                for (int i = 0; i <= maxx + maxy - 2; i++)
                {
                    for (int j = Math.Max(0, i - maxx + 1); j <= maxy - 1 && i - j >= 0; j++)
                    {
                        if (Map[i - j, j] == last && Map[i - j, j] != DrawType.Empty)
                        {
                            count++;
                        }
                        else
                        {
                            count = 1;
                        }
                        last = Map[i - j, j];
                        if (count == 5)
                        {
                            if (last == DrawType.Player1)
                            {
                                return PlayerSide.Player1;
                            }
                            else
                            {
                                return PlayerSide.Player2;
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
    public class HumanPlayer : IPlayer
    {
        public static string Name { get; } = "Local Human Player";

        public static string Description { get; } = "Local human player ( You ).";

        public Node Draw(DMap map, PlayerSide u)
        {
            RTIO.OnTouchedNode = null;
            while (true)
            {
                if (RTIO.OnTouchedNode != null)
                {
                    return (Node)RTIO.OnTouchedNode;
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }

        public void OnFailToDraw(DrawType[,] map, Node preDraw)
        {
            MessageBox.Show("Select a rigth place to draw");
        }
    }
}
