namespace DapperSql
{
    /// <summary>
    /// 公用sql处理
    /// </summary>
    public class BaseSql
    {
        /// <summary>
        /// 拼接分页查询语句
        /// </summary>
        /// <param name="Field"></param>
        /// <param name="SortType"></param>
        /// <returns></returns>
        public static string GetRowNum(string Field, string SortType)
        {
           return $@", ROW_NUMBER() OVER (ORDER BY {Field} {SortType}) rownum ";
        }
    }
}
