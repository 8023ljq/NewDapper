using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace DapperModel.DBModel.SysModel
{
    /// <summary>
    /// 文章类别
    /// </summary>
    public class Sys_ArticleCategory
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 分类标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 父分类ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 类别ID列表(逗号分隔开)
        /// </summary>
        public string ClassList { get; set; }

        /// <summary>
        /// 类别深度
        /// </summary>
        public int ClassLayer { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 分类图标
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 分类SEO标题
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
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}