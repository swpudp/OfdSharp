namespace OfdSharp.Primitives.Graph.Tight
{
    /// <summary>
    /// 移动节点，用于表示到新的绘制点指令
    /// </summary>
    public class Move
    {
        /// <summary>
        /// 移动后新的当前绘制点
        /// </summary>
        public Position Point1 { get; set; }
    }
}
