using System;


namespace DapperModel.ViewModel.DBViewModel
{
    /// <summary>
    /// 文章内容表
    /// </summary>
    public class Sys_ArticleViewModel
    {
	     
        /// <summary>
        /// 主键Id
        /// </summary>	
        public string Id { get; set; }
 
        /// <summary>
        /// 分类ID
        /// </summary>	
        public string CategoryId { get; set; }
 
        /// <summary>
        /// 文章标题
        /// </summary>	
        public string Title { get; set; }
 
        /// <summary>
        /// 文章副标题
        /// </summary>	
        public string SubTitle { get; set; }
 
        /// <summary>
        /// 文章内容
        /// </summary>	
        public string Content { get; set; }
 
        /// <summary>
        /// 浏览次数
        /// </summary>	
        public int? ViewCount { get; set; }
 
        /// <summary>
        /// 图片地址
        /// </summary>	
        public string ImageUrl { get; set; }
 
        /// <summary>
        /// 排序
        /// </summary>	
        public int? Sort { get; set; }
 
        /// <summary>
        /// SEO标题
        /// </summary>	
        public string SeoTitle { get; set; }
 
        /// <summary>
        /// 分类SEO关键字
        /// </summary>	
        public string SeoKeywords { get; set; }
 
        /// <summary>
        /// 分类SEO描述
        /// </summary>	
        public string SeoDescription { get; set; }
 
        /// <summary>
        /// 添加人
        /// </summary>	
        public string AddUserId { get; set; }
 
        /// <summary>
        /// 添加时间
        /// </summary>	
        public DateTime AddTime { get; set; }
 
        /// <summary>
        /// 修改人
        /// </summary>	
        public string UpdateUserId { get; set; }
 
        /// <summary>
        /// 修改时间
        /// </summary>	
        public DateTime UpdateTime { get; set; }
 
        /// <summary>
        /// 是否置顶
        /// </summary>	
        public bool? IsTop { get; set; }
 
        /// <summary>
        /// 是否热门
        /// </summary>	
        public bool? IsRed { get; set; }
 
        /// <summary>
        /// 是否发布
        /// </summary>	
        public bool? IsPublish { get; set; }
 
        /// <summary>
        /// 是否删除
        /// </summary>	
        public bool? IsDeleted { get; set; }

    }
}


