using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperModel.ViewModel.RequestModel
{
    /// <summary>
    /// 添加按钮请求类
    /// </summary>
    public class AddMenuPowerRequest
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string GuId { get; set; }

        /// <summary>
        /// 父级ID(或者自身ID)
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 按钮名称
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 按钮标识
        /// </summary>
        public string Purview { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestUrl { get; set; }
    }
}
