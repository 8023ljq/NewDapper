using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperModel
{
    [NotMapped]
    public partial class Sys_Menu
    {
        /// <summary>
        /// 下级菜单
        /// </summary>
        [NotMapped]
        public  List<Sys_Menu> children { get; set; }
    }
}
