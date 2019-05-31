using DapperCommonMethod.CommonJson;
using DapperModel.TextModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DapperAdmin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Hemo
        public ActionResult Index()
        {
            var str = JosnHelp.Readjson("1001", "zh-CN");

            return View();
        }

        [HttpPost]
        public ActionResult IndexAct(Text Text)
        {
            string tit = Text.title;
            string name = Text.username;
            return Success("测试通过");
        }

        public ActionResult List()
        {
            return View();
        }
    }
}