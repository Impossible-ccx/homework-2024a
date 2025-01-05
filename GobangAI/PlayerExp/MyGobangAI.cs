using WpfApp;
using CppCliClass;
namespace PlayerExp
{
    public class MyGobangAI : IPlayer
    {
        private bool flagEnd = false;
        private AIPlayer _player;
        public MyGobangAI()
        {
            _player = new();
        }
        public static string Name { get; } = "CPP AI";

        public static string Description { get; } = "CPP AI 2.0";
        public Node Draw(DMap map, PlayerSide u)
        {
            return _player.Draw(map.map, u, map.maxx, map.maxy);
        }

        public void OnFailToDraw(DrawType[,] map, Node preDraw)
        {
            ;
        }
    }
}
