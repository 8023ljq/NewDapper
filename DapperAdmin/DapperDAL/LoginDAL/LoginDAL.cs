using DapperModel.DataModel;

namespace DapperDAL
{
    /// <summary>
    /// 后台登录数据处理层
    /// </summary>
    public class LoginDAL: BaseDALS
    {
        /// <summary>
        /// 通过用户名检查用户是否存在
        /// </summary>
        /// <returns></returns>
        public Sys_Manager GetModelByName(string Name)
        {
           return GetModelAll<Sys_Manager>("Name=@Name", new { Name = Name });
        }
    }
}
