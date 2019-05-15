using System;
using System.Text;
using System.Collections.Generic;
using System.Data;

namespace DapperModel.DBModel.SysModel
{
    /// <summary>
    /// ��̨����Ա��ɫ��
    /// </summary>
    public class Sys_ManagerRole
    {
        /// <summary>
        /// ����Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ��ɫ����
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// ��ɫ����(1:����2:ϵ��[����鿴��Ŀö��])
        /// </summary>
        public int RoleType { get; set; }

        /// <summary>
        /// �Ƿ�Ĭ��(ֻ�������ݽ����޸�)
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