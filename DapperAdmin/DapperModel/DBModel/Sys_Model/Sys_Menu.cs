using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace DapperModel.DBModel.SysModel
{
    /// <summary>
    /// ϵͳ�˵�
    /// </summary>
    public class Sys_Menu
    {
        /// <summary>
        /// ����Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ����Id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// �˵�����
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// �˵��㼶
        /// </summary>
        public int Layers { get; set; }

        /// <summary>
        /// ͼ���ַ
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// ���ӵ�ַ
        /// </summary>
        public string AddressUrl { get; set; }

        /// <summary>
        /// �����ֶ�
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        public string Purview { get; set; }

        /// <summary>
        /// �Ƿ���ʾ
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// �Ƿ�Ĭ��
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// �����
        /// </summary>
        public string AddUserId { get; set; }

        /// <summary>
        /// ���ʱ��
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// �޸���
        /// </summary>
        public string UpdateUserId { get; set; }

        /// <summary>
        /// �޸�ʱ��
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// �Ƿ�ɾ��(0:��1:��)
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// ��ע
        /// </summary>
        public string Remarks { get; set; }
    }
}