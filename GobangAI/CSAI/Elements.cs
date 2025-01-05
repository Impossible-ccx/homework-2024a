using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WpfApp;

namespace CSAI
{
    internal static class Fun
    {
        public const int MaxDepth = 24;
        public const long MaxSolveSpace = (long)1e8;
        //public const long MaxSolveSpace = (long)1e6;
        //意外，因为在draw中决定第一步，因此depth从2开始
        public static ABNode DFS(WMap map, in PriQueueST space, in ABNode parent, in PlayerSide op, long solveSpace, in int depth = 2)
        {
            DrawType pen;
            if(depth%2 == 1)
            {
                pen = op == PlayerSide.Player1 ? DrawType.Player1 : DrawType.Player2;
            }
            else
            {
                pen = op != PlayerSide.Player1 ? DrawType.Player1 : DrawType.Player2;
            }
            ABNode res = parent;
            if ((solveSpace > MaxSolveSpace || depth > MaxDepth))
            {
                res.Value = ComputeValue(map, op);
            }
            else
            {
                for(int i = 0; i < space.Count; i++)
                {
                    var node = space[i];
                    map.map[node.X, node.Y] = pen;
                    bool isEnd;
                    var nextSpace = GetAvailableDraws(map,
                        pen == DrawType.Player1 ? PlayerSide.Player2 : PlayerSide.Player1, out isEnd);
                    if (isEnd)
                    {
                        if (depth % 2 == 1)
                        {
                            res.Value = 10000000;
                        }
                        else
                        {
                            res.Value = -10000000;
                        }
                        map.map[node.X, node.Y] = DrawType.Empty;
                        break;
                    }
                    else
                    {
                        ABNode child = DFS(map, nextSpace, res, op, solveSpace * nextSpace.Count, depth + 1);
                        if (depth % 2 == 1)
                        {
                            if (child.Value > res.Value)
                            {
                                res.Value = child.Value;
                                res.Alpha = child.Value;
                            }
                        }
                        else
                        {
                            if (child.Value < res.Value)
                            {
                                res.Value = child.Value;
                                res.Beta = child.Value;
                            }
                        }
                        if (res.Alpha >= res.Beta)
                        {
                            map.map[node.X, node.Y] = DrawType.Empty;
                            break;
                        }
                    }
                    map.map[node.X, node.Y] = DrawType.Empty;
                }
            }
            return res;
        }
        public static PriQueueST GetAvailableDraws(in WMap map, in PlayerSide forwho, out bool isEnd)
        {
            var anti = forwho == PlayerSide.Player1 ? PlayerSide.Player2 :PlayerSide.Player1;
            
            var cutoff = new PriQueueST();
            //var cutoff = new PriorityQueue<Node, int>();
            isEnd = false;
            //击杀剪枝
            for(int i = 0;  i < map.maxx; i++)
            {
                for(int j = 0; j < map.maxy; j++)
                {
                    if(map.map[i, j] == DrawType.Empty)
                    {
                        Node node = new Node
                        {
                            X = i,
                            Y = j
                        };
                        int prio = Math.Max((int)CheckType(map, node, forwho) * 2 + 1
                            , (int)CheckType(map, node, anti) * 2);
                        cutoff.Enqueue(node, prio);
                    }
                }
            }
            cutoff.Dequeue(out Node n, out int u);
            var res = new PriQueueST();
            if(u == 12)
            {
                isEnd = true;
                res.Clear();
                res.Enqueue(n, u);
                return res;
            }
            else if(u > 10)
            {
                res.Clear();
                res.Enqueue(n, u);
                return res;
            }
            else
            {
                return cutoff;
            }
        }
        private static int ComputeValue(in WMap map, in PlayerSide op)
        {
            int p1 = 0;
            int p2 = 0;
            PlayerSide anti = op == PlayerSide.Player1 ? PlayerSide.Player2 : PlayerSide.Player1;
            for (int i = 0; i < map.maxx; i++)
            {
                for (int j = 0; j < map.maxy; j++)
                {
                    if (map.map[i, j] == DrawType.Empty)
                    {
                        Node node = new Node();
                        node.X = i;
                        node.Y = j;
                        int sp1 = (int)CheckType(map,node, op);
                        int sp2 = (int)CheckType(map, node, anti);
                        p1 += sp1 * sp1 * sp1 * sp1;
                        p2 += sp2 * sp2 * sp2 * sp2;
                    }
                }
            }
            return p2 - p1;
        }
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ptype PCheckType(in WMap map, in Node target, in PlayerSide op)
        {
            DrawType opd = op == PlayerSide.Player1 ? DrawType.Player1 : DrawType.Player2;
            Ptype res = Ptype.p1;
            //x+
            {
                Ptype the = Ptype.p1;
                short count = 0;
                int live = 0;
                for (int i = 0; i < 4; i++)
                {
                    if(target.X + i + 1 >= map.maxx)
                    {
                        break;
                    }
                    else
                    {
                        var t = map.map[target.X + i + 1 , target.Y];
                        if(t == opd)
                        {
                            count++;
                        }
                        else
                        {
                            if(t == DrawType.Empty) live++;
                            break;
                        }
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    if (target.X - i - 1 < 0)
                    {
                        break;
                    }
                    else
                    {
                        var t = map.map[target.X - i - 1, target.Y];
                        if (t == opd)
                        {
                            count++;
                        }
                        else
                        {
                            if (t == DrawType.Empty) live++;
                            break;
                        }
                    }
                }
                switch (count)
                {
                    case 4:
                        the = Ptype.five; 
                        break;
                    case 3:
                        if (live == 2) the = Ptype.live4;
                        else the = Ptype.dead4; 
                        break;
                    case 2:
                        if (live == 2) the = Ptype.live3;
                        else the = Ptype.dead3;
                        break;
                    case 1:
                        the = Ptype.p2; break;
                    case 0:
                        the = Ptype.p1; break;
                }
                res = EvaUp(res, the);
            }
            //y+
            {
                Ptype the = Ptype.p1;
                short count = 0;
                int live = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (target.Y + i + 1 >= map.maxy)
                    {
                        break;
                    }
                    else
                    {
                        var t = map.map[target.X, target.Y + i + 1];
                        if (t == opd)
                        {
                            count++;
                        }
                        else
                        {
                            if(t == DrawType.Empty) live++;
                            break;
                        }
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    if (target.Y - i - 1 < 0)
                    {
                        break;
                    }
                    else
                    {
                        var t = map.map[target.X, target.Y - i - 1];
                        if (t == opd)
                        {
                            count++;
                        }
                        else
                        {
                            if (t == DrawType.Empty) live++;
                            break;
                        }
                    }
                }
                switch (count)
                {
                    case 4:
                        the = Ptype.five;
                        break;
                    case 3:
                        if (live == 2) the = Ptype.live4;
                        else the = Ptype.dead4;
                        break;
                    case 2:
                        if (live == 2) the = Ptype.live3;
                        else the = Ptype.dead3;
                        break;
                    case 1:
                        the = Ptype.p2; break;
                    case 0:
                        the = Ptype.p1; break;
                }
                res = EvaUp(res, the);
            }
            //x+y+
            {
                Ptype the = Ptype.p1;
                short count = 0;
                int live = 0;   
                for (int i = 0; i < 4; i++)
                {
                    if (target.X + i + 1 >= map.maxx || target.Y + i + 1 >= map.maxy)
                    {
                        break;
                    }
                    else
                    {
                        var t = map.map[target.X + i + 1 , target.Y + i + 1];
                        if (t == opd)
                        {
                            count++;
                        }
                        else
                        {
                            if (t == DrawType.Empty) live++;
                            break;
                        }
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    if (target.X - i - 1 < 0 || target.Y - i - 1 < 0)
                    {
                        break;
                    }
                    else
                    {
                        var t = map.map[target.X - i - 1, target.Y - i - 1];
                        if (t == opd)
                        {
                            count++;
                        }
                        else
                        {
                            if (t == DrawType.Empty) live++;
                            break;
                        }
                    }
                }
                switch (count)
                {
                    case 4:
                        the = Ptype.five;
                        break;
                    case 3:
                        if (live == 2) the = Ptype.live4;
                        else the = Ptype.dead4;
                        break;
                    case 2:
                        if (live == 2) the = Ptype.live3;
                        else the = Ptype.dead3;
                        break;
                    case 1:
                        the = Ptype.p2; break;
                    case 0:
                        the = Ptype.p1; break;
                }
                res = EvaUp(res, the);
            }
            //x+y-
            {
                Ptype the = Ptype.p1;
                short count = 0;
                int live = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (target.X + i + 1 >= map.maxx || target.Y - i - 1 < 0)
                    {
                        break;
                    }
                    else
                    {
                        var t = map.map[target.X + i + 1, target.Y - i - 1];
                        if (t == opd)
                        {
                            count++;
                        }
                        else
                        {
                            if(t == DrawType.Empty) live++;
                            break;
                        }
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    if (target.X - i - 1 < 0 || target.Y + i + 1 >= map.maxy)
                    {
                        break;
                    }
                    else
                    {
                        var t = map.map[target.X - i - 1, target.Y + i + 1];
                        if (t == opd)
                        {
                            count++;
                        }
                        else
                        {
                            if (t == DrawType.Empty) live++;
                            break;
                        }
                    }
                }
                switch (count)
                {
                    case 4:
                        the = Ptype.five;
                        break;
                    case 3:
                        if (live == 2) the = Ptype.live4;
                        else the = Ptype.dead4;
                        break;
                    case 2:
                        if (live == 2) the = Ptype.live3;
                        else the = Ptype.dead3;
                        break;
                    case 1:
                        the = Ptype.p2; break;
                    case 0:
                        the = Ptype.p1; break;
                }
                res = EvaUp(res, the);
            }
            return res;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Ptype CheckType(in WMap map, in Node target, in PlayerSide op)
        {
            return HType.HCheckType(map, target, op);
            //return PCheckType(map, target, op);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Ptype EvaUp(Ptype p1, Ptype p2)
        {
            if (p1 < p2)
            {
                var temp = p1;
                p1 = p2;
                p2 = temp;
            }
            if (p1 == Ptype.dead4)
            {
                if (p2 == Ptype.dead4) return Ptype.live4;
                else if (p2 == Ptype.live3) return Ptype.live4;
                else return Ptype.dead4;
            }
            else if (p1 == Ptype.live3 && p2 == Ptype.live3) return Ptype.live4;
            else return p1;

        }
    }
    
    internal enum Ptype{
        p1,
        p2,
        dead3,
        live3,
        dead4,
        live4,
        five
    }
    internal struct ABNode
    {
        public int Alpha;
        public int Beta;
        public int Value;
    }
    public class WMap
    {
        public DrawType[,] map;
        public readonly int maxx;
        public readonly int maxy;
        public WMap(DMap p)
        {
            this.maxx = p.maxx;
            this.maxy = p.maxy;
            map = new DrawType[this.maxx, this.maxy];
            for(int i = 0;i< maxx;i++)
            {
                for(int j = 0; j < maxy; j++)
                {
                    map[i,j] = p.map[i,j];
                }
            }
        }
    }
}