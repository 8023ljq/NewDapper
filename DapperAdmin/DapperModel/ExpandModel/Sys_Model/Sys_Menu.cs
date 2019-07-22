using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperModel
{
    public partial class Sys_MenuS
    {
        /// <summary>
        /// 下级菜单
        /// </summary>
        [NotMapped]
        public List<Sys_Menu> children { get; set; }
    }
}
