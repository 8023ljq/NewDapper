using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperModel.TextModel
{
    public class TextModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string province { get; set; }
        
        /// <summary>
        /// 市
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public string district { get; set; }
    }
}
