namespace DapperModel.CommonModel
{
    /// <summary>
    /// 要记录修改前后数据操作实体类
    /// </summary>
    public class UpdateInfo
    {
        /// <summary>
        /// 属性名
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 原始值
        /// </summary>
        public string OldValue { get; set; }
        /// <summary>
        /// 新值
        /// </summary>
        public string NewValue { get; set; }
    }
}
