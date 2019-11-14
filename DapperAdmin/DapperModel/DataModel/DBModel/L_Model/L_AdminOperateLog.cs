using System;

namespace DapperModel.DataModel
{
    /// <summary>
    /// 管理员操作日志
    /// </summary>
    public class L_AdminOperateLog
    {
        /// <summary>
        /// 主键Id
        /// </summary>	
        public string Id { get; set; }
 
        /// <summary>
        /// 管理员ID
        /// </summary>	
        public string AdminId { get; set; }
 
        /// <summary>
        /// 管理员名称
        /// </summary>	
        public string AdminName { get; set; }
 
        /// <summary>
        /// 操作时间
        /// </summary>	
        public DateTime OperateTime { get; set; }
 
        /// <summary>
        /// 操作类型
        /// </summary>	
        public int? OperateType { get; set; }
 
        /// <summary>
        /// 操作描述
        /// </summary>	
        public string OperateDepict { get; set; }

    }
}


