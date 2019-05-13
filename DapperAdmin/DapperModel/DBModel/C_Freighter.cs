using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperModel.DBModel
{
    public class C_Freighter
    {
        /// <summary>
        /// 主键
        /// </summary>	
        public string Id { get; set; }

        /// <summary>
        /// 承运商资质Id
        /// </summary>
        public string FreighterCertificationId { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>	
        public string Phone { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>	
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>	
        public string PassWord { get; set; }

        /// <summary>
        /// 加密随机码
        /// </summary>	
        public string PassWordRandNum { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// 联系人手机号
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// 联系人邮箱
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        /// 父级主账号Id
        /// </summary>	
        public string ParentId { get; set; }

        /// <summary>
        /// 税率
        /// </summary>	
        public string TaxRate { get; set; }

        /// <summary>
        /// 是否停用(0[false]:启用1[true]:停用)
        /// </summary>
        public bool? IsDisable { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime? RegisterTime { get; set; }

        /// <summary>
        /// 删除标记
        /// </summary>	
        public bool? DeleteMark { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>	
        public string CreatorUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatorTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>	
        public string LastModifyUserId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 删除人
        /// </summary>	
        public string DeleteUserId { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
    }
}
