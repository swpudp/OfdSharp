namespace OfdSharp.Primitives
{
    /// <summary>
    /// 标识引用，无符号位整数，此标识应为文档内已定义的标识
    /// </summary>
    public class RefId
    {
        public RefId(int id)
        {
            Id = new Id(id);
        }

        public RefId(Id id)
        {
            Id = id;
        }

        public RefId(string id)
        {
            Id = new Id(id);
        }

        public Id Id { get; }

        public override string ToString()
        {
            return Id.ToString("D", null);
        }
    }
}