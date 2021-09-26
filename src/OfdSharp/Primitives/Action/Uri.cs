namespace OfdSharp.Primitives.Action
{
    /// <summary>
    /// 目标URI的位置
    /// </summary>
    public class Uri
    {
        /// <summary>
        /// 目标URI的位置
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Base URI，用于相对地址
        /// </summary>
        public string Base { get; }
    }
}
