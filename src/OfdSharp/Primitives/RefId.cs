namespace OfdSharp.Primitives
{
    /// <summary>
    /// 标识引用，无符号位整数，此标识应为文档内已定义的标识
    /// </summary>
    public class RefId
    {
        public Id Id { get; set; }

        public override string ToString()
        {
            return Id.ToString("D", null);
        }
    }
}
