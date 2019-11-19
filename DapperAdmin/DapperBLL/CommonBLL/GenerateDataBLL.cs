using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperDAL;
using DapperModel.CommonModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace DapperBLL
{
    /// <summary>
    /// 生成数据业务逻辑层
    /// </summary>
    public class GenerateDataBLL
    {
        GenerateDataDAL dataDAL = new GenerateDataDAL();

        /// <summary>
        /// 获取当前数据库所有表名
        /// </summary>
        /// <returns></returns>
        public ResultMsg GetNowTableName()
        {
            List<SqlTableModel> TableNameList = dataDAL.GetNowTableName();
            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = TableNameList });
        }

        /// <summary>
        /// 生成数据文件
        /// </summary>
        /// <param name="TableNameList">所选表名</param>
        /// <returns></returns>
        public ResultMsg GenerateFile(List<SqlTableModel> TableNameList)
        {
            var templeturl = HostingEnvironment.MapPath("~/") + "\\File\\DBModel.txt";//模板文件路径

            if (!File.Exists(templeturl))
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1029);
            }

            StreamReader reader = new StreamReader(templeturl, Encoding.UTF8);
            string Readrtxt = reader.ReadToEnd();

            List<SqlColumnModel> ColumnModelList = dataDAL.GetTableInfo("");

            //循环表名
            foreach (var TableName in TableNameList)
            {
                List<SqlColumnModel> TableColumnModelList = ColumnModelList.Where(p => p.ColumnTableName == TableName.TableName).ToList();

                string classStr = string.Empty;

                //循环表字段
                foreach (var ColumnName in TableColumnModelList)
                {
                    if (ColumnName.ColumnName == "Id")
                    {
                        classStr += $@"
        /// <summary>
        /// {ColumnName.ColumnRemark}
        /// </summary>
        [ExplicitKey]
        public {CommonMethod.DBTypeToCSharpType(ColumnName.ColumnType)} {ColumnName.ColumnName} {{ get; set; }}" + "\r\n";
                    }
                    else
                    {
                        classStr += $@"
        /// <summary>
        /// {ColumnName.ColumnRemark}
        /// </summary>
        public {CommonMethod.DBTypeToCSharpType(ColumnName.ColumnType)} {ColumnName.ColumnName} {{ get; set; }}" + "\r\n";
                    }
                }
                Readrtxt = Readrtxt.Replace("#Cite#", "").Replace("#Namespaces#", "").Replace("#ClassRemarks#", TableName.TableRemark)
                          .Replace("#ClassDBName#", TableName.TableName).Replace("#ClassName#", TableName.TableName).Replace("#ColumnInfo#", classStr);

                string prefix = TableName.TableName.Split('_')[0];

                var ModelFileName = HostingEnvironment.MapPath("~/") + "\\CreaterFile\\DBModel\\"+ prefix + "_Model\\"+ TableName.TableName + ".cs";//实体类文件路径

                CommonMethod.CreateFile(ModelFileName, Readrtxt);
            }

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200);
        }
    }
}
