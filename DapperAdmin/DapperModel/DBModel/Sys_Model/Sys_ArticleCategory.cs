using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace DapperModel.DBModel.SysModel
{
    /// <summary>
    /// �������
    /// </summary>
    public class Sys_ArticleCategory
    {
        /// <summary>
        /// ����
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// �������
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// ������ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// ���ID�б�(���ŷָ���)
        /// </summary>
        public string ClassList { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public int ClassLayer { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// ����ͼ��
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// ����SEO����
        /// </summary>
        public string SeoTitle { get; set; }

        /// <summary>
        /// ����SEO�ؼ���
        /// </summary>
        public string SeoKeywords { get; set; }

        /// <summary>
        /// ����SEO����
        /// </summary>
        public string SeoDescription { get; set; }

        /// <summary>
        /// �Ƿ�ɾ��
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}