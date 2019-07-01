using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperModel.CommonModel
{
    /// <summary>
    /// 查询条件实体类（带分页）
    /// </summary>
    public class SelectModel : PageModel
    {
        /// <summary>
        /// 关键字查询
        /// </summary>
        public string Keyword { get; set; }
    }
}
