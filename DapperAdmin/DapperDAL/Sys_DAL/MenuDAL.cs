using DapperModel.DataModel;
using DapperSql.Sys_Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDAL
{
    /// <summary>
    /// 菜单数据处理层
    /// </summary>
    public class MenuDAL: BaseDALS
    {
        /// <summary>
        /// 获取所有菜单数据(添加之前检查是否存在)
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        public List<Sys_Menu> GetMenuIsExistList(Sys_Menu menuModel)
        {
            return GetList<Sys_Menu>(Sys_MenuSql.selectMenuIsExist, null, menuModel);
        }

        /// <summary>
        /// 添加菜单数据
        /// </summary>
        /// <param name="MenuModel"></param>
        /// <param name="operateLogModel"></param>
        public void AddMenuThing(Sys_Menu MenuModel, L_AdminOperateLog operateLogModel)
        {
            using (var tran = dapperHelps.GetOpenConnection().BeginTransaction())
            {
                dapperHelps.ExecuteInsert<Sys_Menu>(MenuModel, tran);

                dapperHelps.ExecuteInsertGuid<L_AdminOperateLog>(operateLogModel, tran);

                tran.Commit();
            }
        }

    }
}
