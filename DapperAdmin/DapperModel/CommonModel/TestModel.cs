using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperModel.CommonModel
{
    public class TestModel
    {
        public string str { get; set; }

        public List<SqlTableModel> tableModels { get; set; }
    }
}
