namespace OfdSharp.Primitives.Text
{
    /// <summary>
    /// 文字定位，文字对象使用严格的文字定位信息进行定位
    /// 11.3 文字定位 图 61 表 46
    /// </summary>
    public class TextCode
    {
        /// <summary>
        /// 第一个文字的字形在对象坐标系下的 X 坐标
        /// 当 X 不出现，则采用上一个 TextCode 的 X 值，文字对象中的一个
        /// TextCode 的属性必选
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// 第一个文字的字形在对象坐标系下的 Y 坐标
        /// 当 X 不出现，则采用上一个 TextCode 的 Y 值，文字对象中的一个
        ///  TextCode 的属性必选
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// 文字之间在 X 方向上的偏移值
        /// double 型数值队列，列表中的每个值代表一个文字与前一个
        /// 文字之间在 X 方向的偏移值
        /// DeltaX 不出现时，表示文字的绘制点在 X 方向不做偏移。
        /// </summary>
        public Array DeltaX { get; set; }

        /// <summary>
        /// 字之间在 Y 方向上的偏移
        /// double 型数值队列，列表中的每个值代表一个文字与前一个
        /// 文字之间在 Y 方向的偏移值
        /// DeltaY 不出现时，表示文字的绘制点在 Y 方向不做偏移。
        /// </summary>
        public Array DeltaY { get; set; }

        /// <summary>
        /// 文字内容
        /// </summary>
        public string Value { get; set; }
    }
}
