namespace WpfApp
{
    public enum PlayerSide
    {
        Player1, Player2
    }
    public enum DrawType
    {
        Player1,
        Player2,
        Empty,
        Wall
    }
    public struct Node
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    public interface IPlayer
    {
        public static abstract string Name { get; }
        public static abstract string Description { get; }
        public Node Draw(DMap map, PlayerSide u);
        //直接抛出来就行，实际上没有实现
        public void OnFailToDraw(DrawType[,] map, Node preDraw);
    }
    public class DMap
    {
        public readonly DrawType[,] map;
        public readonly int maxx;
        public readonly int maxy;
        public DMap(DrawType[,] map, int maxx, int maxy)
        {
            this.map = map;
            this.maxx = maxx;
            this.maxy = maxy;
        }
    }
}
