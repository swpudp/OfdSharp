namespace OfdSharp.Primitives
{
    /// <summary>
    /// 标识引用，无符号位整数，此标识应为文档内已定义的标识
    /// </summary>
    public class CtRefId
    {
        public CtRefId(int id)
        {
            Id = new CtId(id);
        }

        public CtRefId(CtId id)
        {
            Id = id;
        }

        public CtRefId(string id)
        {
            Id = new CtId(id);
        }

        public CtId Id { get; }

        public override string ToString()
        {
            return Id.ToString("D", null);
        }
    }
}