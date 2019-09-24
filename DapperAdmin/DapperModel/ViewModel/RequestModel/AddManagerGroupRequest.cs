using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperModel.ViewModel.RequestModel
{
    /// <summary>
    /// 添加用户组请求实体
    /// </summary>
    public class AddManagerGroupRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 组名
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 添加类型
        /// </summary>
        public int AddType { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>
        public string AddUserId { get; set; }
    }
}
