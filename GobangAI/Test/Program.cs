using WpfApp;
using CSAI;
using System.Runtime.InteropServices;
using System.Diagnostics;
namespace Testu
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            test2();
        }
        internal static void test()
        {
            DMap m = new DMap(new DrawType[12, 12], 12, 12);
            WMap map = new WMap(m);
            Random rand = new Random();
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    switch (rand.Next(4))
                    {
                        case 0:
                            map.map[i, j] = DrawType.Player1;
                            break;
                        case 1:
                            map.map[i, j] = DrawType.Player2;
                            break;
                        case 2:
                            map.map[i, j] = DrawType.Empty;
                            break;
                        case 3:
                            map.map[i, j] = DrawType.Wall;
                            break;
                    }
                }
            }
            Node tar = new Node();
            PlayerSide op = PlayerSide.Player1;

            var watch = new Stopwatch();
            Test.HC(map, tar, op);
            watch.Start();
            for (int j = 0; j < 10000000; j++)
            {
                Test.PC(map, tar, op);
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedTicks);
            watch.Restart();
            for (int j = 0; j < 10000000; j++)
            {
                Test.HC(map, tar, op);
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedTicks);
        }
        internal static void test2()
        {
            var watch = new Stopwatch();
            int[] nums = new int[1000]; 
            watch.Start();
            for(int i = 0; i < 10000; i++)
            {
                t1(nums);
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedTicks);
            watch.Start();
            for (int i = 0; i < 10000; i++)
            {
                t2(nums);
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedTicks);
        }
        internal static void t1(int[] nums)
        {
            Random rnd = new Random();
            for(int i = 0; i < 1000; i++)
            {
                var p = nums[rnd.Next(1000)];
            }
        }
        unsafe internal static void t2(int[] nums)
        {
            int* p = stackalloc int[1000];
            Random rnd = new Random();
            fixed (int* n = &nums[0])
            {
                Buffer.MemoryCopy(n, p, 1000 * 4, 1000 * 4);
            }
            for(int i = 0; i < 1000; i++)
            {
                var l = p[rnd.Next(1000)];
            }
        }
    }
}
