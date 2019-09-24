using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperModel.CommonModel
{
    /// <summary>
    /// 分页查询实体类
    /// </summary>
    public class PageModel
    {
        /// <summary>
        /// 每页大小
        /// </summary>
        public int pageSize { get; set; }

        /// <summary>
        /// 第几页
        /// </summary>
        public int curPage { get; set; }

        /// <summary>
        /// 总行数
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 从多少行开始
        /// </summary>
        public int start { get; set; }

        /// <summary>
        /// 到多少行结束
        /// </summary>
        public int end { get; set; }
    }
}
