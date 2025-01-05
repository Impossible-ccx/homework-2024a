using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WpfApp;

namespace CSAI
{
    unsafe internal static class HType
    {
        public static Ptype HCheckType(in WMap map, in Node target, in PlayerSide op)
        {
            DrawType opd = op == PlayerSide.Player2 ? DrawType.Player1 : DrawType.Player2;
            Ptype res = Ptype.p1;
            DrawType* vect = stackalloc DrawType[9];
            int tx = target.X;
            int ty = target.Y;
            int mx = map.maxx;
            int my = map.maxy;
            var tm = map.map;
            //x
            {
                for (int i = -4; i <= 4; i++)
                {
                    if (tx + i < 0 || tx + i >= mx)
                    {
                        vect[i + 4] = DrawType.Wall;
                    }
                    else
                    {
                        DrawType temp = tm[tx + i, ty];
                        if (temp == opd)
                        {
                            vect[i + 4] = DrawType.Wall;
                        }
                        else
                        {
                            vect[i + 4] = temp;
                        }
                    }
                }
                res = EvaUp(res, table[GetIndex(vect)]);
            }
            //y
            {
                for (int i = -4; i <= 4; i++)
                {
                    if (ty + i < 0 || ty + i >= my)
                    {
                        vect[i + 4] = DrawType.Wall;
                    }
                    else
                    {
                        var temp = tm[tx, ty + i];
                        if (temp == opd)
                        {
                            vect[i + 4] = DrawType.Wall;
                        }
                        else
                        {
                            vect[i + 4] = temp;
                        }
                    }
                }
                res = EvaUp(res, table[GetIndex(vect)]);
            }
            //xy
            {
                for (int i = -4; i <= 4; i++)
                {
                    int nx = tx + i;
                    int ny = ty + i;
                    if (nx < 0 || ny < 0 || nx >= mx || ny >= my)
                    {
                        vect[i + 4] = DrawType.Wall;
                    }
                    else
                    {
                        var temp = tm[nx, ny];
                        if (temp == opd)
                        {
                            vect[i + 4] = DrawType.Wall;
                        }
                        else
                        {
                            vect[i + 4] = temp;
                        }
                    }
                }
                res = EvaUp(res, table[GetIndex(vect)]);
            }
            //yx
            {
                for (int i = -4; i <= 4; i++)
                {
                    int nx = tx - i;
                    int ny = ty + i;
                    if (nx < 0 || ny < 0 || nx >= mx || ny >= my)
                    {
                        vect[i + 4] = DrawType.Wall;
                    }
                    else
                    {
                        var temp = tm[nx, ny];
                        if (temp == opd)
                        {
                            vect[i + 4] = DrawType.Wall;
                        }
                        else
                        {
                            vect[i + 4] = temp;
                        }
                    }
                }
                res = EvaUp(res, table[GetIndex(vect)]);
            }
            return res;
        }
        static HType()
        {
            fpow3Ini();
            HCheckTypeIni();
        }
        private static Ptype* table = (Ptype*)Marshal.AllocHGlobal(sizeof(Ptype) * fpow(3, 9));
        private static void HCheckTypeIni()
        {
            DrawType* vect = stackalloc DrawType[9];
            HCheckTypeIniPart(vect);
        }
        private static void HCheckTypeIniPart(DrawType* vect, int dep = 0)
        {
            if (dep == 9)
            {
                table[GetIndex(vect)] = HCheckTypeIniEva(vect);
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    switch (i)
                    {
                        case 0:
                            vect[dep] = DrawType.Wall;
                            break;
                        case 1:
                            vect[dep] = DrawType.Player1;
                            break;
                        case 2:
                            vect[dep] = DrawType.Empty;
                            break;
                    }
                    HCheckTypeIniPart(vect, dep + 1);
                }
            }
        }
        private static Ptype HCheckTypeIniEva(DrawType* vect)
        {
            int count = 1;
            int leftbouns = 0;
            int rightbouns = 0;
            for (int i = 1; i <= 4; i++)
            {
                if (vect[4 - i] == DrawType.Player1)
                {
                    count++;
                }
                else if (vect[4 - i] == DrawType.Wall)
                {
                    leftbouns = 0;
                    break;
                }
                else if (vect[4 - i] == DrawType.Empty)
                {
                    for (int j = i + 1; j <= 4; j++)
                    {
                        if (vect[j] == DrawType.Player1)
                        {
                            leftbouns++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;
                }
            }
            for (int i = 1; i <= 4; i++)
            {
                if (vect[4 + i] == DrawType.Player1)
                {
                    count++;
                }
                else if (vect[4 + i] == DrawType.Wall)
                {
                    rightbouns = 0;
                    break;
                }
                else if (vect[4 + i] == DrawType.Empty)
                {
                    for (int j = i + 1; j <= 4; j++)
                    {
                        if (vect[4 + i] == DrawType.Player1)
                        {
                            rightbouns++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;
                }
            }
            int normalCount = count + Math.Min(leftbouns, rightbouns) + 1;
            int bestCount = count + Math.Max(leftbouns, rightbouns) + 19;
            if (count >= 5)
            {
                return Ptype.five;
            }
            else if (count == 4)
            {
                if (normalCount >= 5)
                {
                    return Ptype.live4;
                }
                else if (bestCount == 4)
                {
                    return Ptype.p1;
                }
                else
                {
                    return Ptype.dead4;
                }
            }
            else if (count == 3)
            {
                if (normalCount >= 5)
                {
                    return Ptype.live4;
                }
                else if (normalCount == 4)
                {
                    return Ptype.live3;
                }
                else if (bestCount == 3)
                {
                    return Ptype.p1;
                }
                else
                {
                    return Ptype.dead3;
                }
            }
            else if (count == 2)
            {
                if (normalCount >= 5)
                {
                    return Ptype.live4;
                }
                else if (normalCount == 4)
                {
                    return Ptype.live3;
                }
                else if (bestCount == 2)
                {
                    return Ptype.p1;
                }
                else
                {
                    return Ptype.p2;
                }
            }
            else
            {
                return Ptype.p1;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetIndex(DrawType* fir)
        {
            int index = 0;
            for (int i = 0; i < 9; i++)
            {
                switch (fir[i])
                {
                    case DrawType.Wall:
                        break;
                    case DrawType.Empty:
                        index += fpow3[8 - i];
                        break;
                    default:
                        index += 2 * fpow3[8 - i];
                        break;
                }
            }
            return index;
        }
        private static int* fpow3 = (int*)Marshal.AllocHGlobal(sizeof(int) * 9);
        private static void fpow3Ini()
        {
            for (int i = 0; i < 9; i++)
            {
                fpow3[i] = fpow(3, i);
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int fpow(int input, int pow)
        {
            switch (pow)
            {
                case 0:
                    return 1;
                case 1:
                    return 3;
                case 2:
                    return 9;
                case 3:
                    return 27;
                case 4:
                    return 81;
                case 5:
                    return 243;
                case 6:
                    return 729;
                case 7:
                    return 2187;
                case 8:
                    return 6561;
                case 9:
                    return 19683;
                default: throw new Exception();
            }
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
    unsafe public ref struct PriQueueST
    {
        private fixed byte listSpan[6000];
        private Span<NodeQ> list;
        private int size = 0;
        public Node this[int index]
        {
            get
            {
                return list[index].node;
            }
        }
        public int Count { get { return size; } }
        public PriQueueST()
        {
            fixed (byte* ptr = &listSpan[0])
            {
                list = new Span<NodeQ>(ptr, 300);
            }
        }
        public void Enqueue(Node node, int p)
        {
            list[size] = new NodeQ(node, p);
            Up(size);
            ++size;
        }
        public bool Dequeue(out Node target, out int priority)
        {
            if (size > 0)
            {
                target = list[0].node;
                priority = list[0].p;
                list[0] = list[--size];
                Down(0);
                return true;
            }
            else
            {
                target = new Node();
                priority = int.MinValue;
                return false;
            }
        }
        public void Clear()
        {
            while (Dequeue(out var t, out var u)) ;
        }
        private void Up(int target)
        {
            int parent = (target - 1) / 2;
            if (target == 0)
            {
                return;
            }
            else
            {
                if (list[parent].p < list[target].p)
                {
                    var temp = list[parent];
                    list[parent] = list[target];
                    list[target] = temp;
                    Up(parent);
                    return;
                }
                else
                {
                    return;
                }
            }
        }
        private void Down(int target)
        {
            int leftChild = target * 2 + 1;
            int rightChild = target * 2 + 2;
            if (leftChild < size)
            {
                if (rightChild < size)
                {
                    int pl = list[leftChild].p;
                    int pr = list[rightChild].p;
                    if (pl > pr)
                    {
                        if (pl > list[target].p)
                        {
                            var temp = list[leftChild];
                            list[leftChild] = list[target];
                            list[target] = temp;
                            Down(leftChild);
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (pr > list[target].p)
                        {
                            var temp = list[rightChild];
                            list[rightChild] = list[target];
                            list[target] = temp;
                            Down(rightChild);
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    if (list[leftChild].p > list[target].p)
                    {
                        var temp = list[leftChild];
                        list[leftChild] = list[target];
                        list[target] = temp;
                        return;
                    }
                }
            }
            else
            {
                return;
            }
        }
    }
    internal struct NodeQ
    {
        public Node node;
        public int p;
        public NodeQ(Node node, int p)
        {
            this.node = node;
            this.p = p;
        }

    }
}
