using System.IO;

namespace OfdSharp.Container
{
    /// <summary>
    /// 资源目录
    /// </summary>
    public class ResDir : VirtualContainer
    {
        public ResDir(DirectoryInfo fullDir) : base(fullDir)
        {
        }
    }
}
