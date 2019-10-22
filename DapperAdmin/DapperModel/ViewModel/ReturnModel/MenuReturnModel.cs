using System.Collections.Generic;

namespace DapperModel.ViewModel
{
    /// <summary>
    /// 单个菜单信息的返回参数
    /// </summary>
    public class MenuReturnModel
    {
        /// <summary>
        /// 菜单数据
        /// </summary>
        public Sys_Menu MenuModel { get; set; }

        /// <summary>
        ///  菜单按钮数据集合
        /// </summary>
        public List<Sys_Menu> MenuPowerList { get; set; }
    }
}
