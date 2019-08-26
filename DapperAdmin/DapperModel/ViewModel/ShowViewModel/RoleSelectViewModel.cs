namespace DapperModel.ViewModel
{
    /// <summary>
    /// 角色列表下拉框展示数据类
    /// </summary>
    public class RoleSelectViewModel
    {
        /// <summary>
        /// 对应主键ID
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 对应显示数据
        /// </summary>
        public string label { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public bool disabled { get; set; }
    }
}
