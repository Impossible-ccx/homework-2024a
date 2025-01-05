
using System.Collections.Generic;
using System.Xml;
using WpfApp;

namespace CSAI
{
    public class CSAI : IPlayer
    {
        internal static Logger logger = new Logger();
        public static string Name { get; } = "CSAI";

        public static string Description { get; } = "C# AI ver = 3.1";

        public Node Draw(DMap map, PlayerSide u)
        {
            //logger.OutputAveDep();

            WMap p = new WMap(map);
            ABNode parent = new ABNode
            {
                Alpha = int.MinValue,
                Beta = int.MaxValue
            };
            var nodes = Fun.GetAvailableDraws(p, u, out bool trush);
            if(nodes.Count == 1)
            {
                return nodes[0];
            }
            var res = new ABNode[map.maxx*map.maxy];
            int index = 0;
            int taskCount = 0;
            object locker = new object();
            for(int i = 0; i < nodes.Count; i++)
            {
                var n = nodes[i];
                Task.Run(() => {
                    var space = new PriQueueST();
                    space.Enqueue(n, 0);
                    res[index] = Fun.DFS(p, space, parent, u, space.Count); 
                    lock (locker) { taskCount++; } 
                });
                index++;
            }
            while(taskCount != index)
            {
                Thread.Sleep(200);
            }
            int maxv = int.MinValue;
            int resi = -1;
            for(int i = 0; i < index; i++)
            {
                if(maxv < res[i].Value)
                {
                    resi = i;
                    maxv = res[i].Value;
                }
            }
            return nodes[resi];
        }

        public void OnFailToDraw(DrawType[,] map, Node preDraw)
        {
            throw new Exception();
        }
    }
    public static class Test
    {
        public static void HC(in WMap map, in Node target, in PlayerSide op)
        {
            HType.HCheckType(map, target, op);
        }
        public static void PC(in WMap map, in Node target, in PlayerSide op)
        {
            Fun.PCheckType(map, target, op);
        }
        public static void HTC(in WMap map, in Node target, in PlayerSide op)
        {
            //HType.TestCheckType(map, target, op);
        }
    }
}