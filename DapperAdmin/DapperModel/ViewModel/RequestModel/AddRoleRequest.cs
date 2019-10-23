using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DapperModel.ViewModel.RequestModel
{
    /// <summary>
    /// 添加角色实体
    /// </summary>
    public class AddRoleRequest
    {
        /// <summary>
        /// 角色主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Required(ErrorMessage = "3004")]
        public string RoleName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        [StringLength(80, ErrorMessage = "6000")]
        public string Remarks { get; set; }

        /// <summary>
        /// 选中权限的主键ID
        /// </summary>
        public List<string> SelectedArray { get; set; }

        /// <summary>
        /// 父级权限主键
        /// </summary>
        public List<string> FatherArray { get; set; }

        /// <summary>
        /// 选中按钮权限的主键ID
        /// </summary>
        public List<string> MenuPowerArry { get; set; }
    }
}
