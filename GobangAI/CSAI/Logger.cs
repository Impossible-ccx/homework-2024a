using System.IO;
using System.Text;
namespace CSAI
{
    internal class Logger
    {
        private string path = "./log.txt";
        int aveDep = 0;
        Int128 record = 0;
        Int128 rep = 0;
        public void NewDepth(int depth)
        {
            rep += depth;
            record++;
            aveDep = (int)(rep / record);
        }
        public Logger()
        {
            File.Create(path);
        }
        public void OutputAveDep()
        {
            using (FileStream fs = File.Open(path, FileMode.Open))
            {
                AddText(fs, aveDep + Environment.NewLine);
                fs.Flush();
            }
        }
        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
    }
}
