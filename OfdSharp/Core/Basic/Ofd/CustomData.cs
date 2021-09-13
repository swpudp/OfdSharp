namespace OfdSharp.Core.Basic.Ofd
{
    /// <summary>
    /// 用户自定义元数据，可以指定一个名称及其对应的值
    /// </summary>
    public class CustomData
    {
        /// <summary>
        /// 元数据名称(Name)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 元数据值
        /// </summary>
        public string Value { get; set; }
    }
}
