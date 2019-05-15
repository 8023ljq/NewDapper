using System;
                             using System.Text;
                             using System.Collections.Generic;
                             using System.Data;
                             using Newtonsoft.Json;

                             namespace DapperModel.DBModel.SysModel{
                                /// <summary>
                                /// ��̨����Ա
                                /// </summary>
                                public class Sys_Manager
                                {
                            /// <summary>
                            /// ����Id
                            /// </summary>
                            public  string  Id { get; set; }

                            /// <summary>
                            /// ��ɫID
                            /// </summary>
                            public  string  RoleId { get; set; }

                            /// <summary>
                            /// �û���
                            /// </summary>
                            public  string  Name { get; set; }

                            /// <summary>
                            /// ��¼����
                            /// </summary>
                            public  string  Password { get; set; }

                            /// <summary>
                            /// �û�ͷ��
                            /// </summary>
                            public  string  Avatar { get; set; }

                            /// <summary>
                            /// �û��ǳ�
                            /// </summary>
                            public  string  Nickname { get; set; }

                            /// <summary>
                            /// �ֻ���
                            /// </summary>
                            public  string  Phone { get; set; }

                            /// <summary>
                            /// �����ַ
                            /// </summary>
                            public  string  Email { get; set; }

                            /// <summary>
                            /// ��¼����
                            /// </summary>
                            public  int  LoginTimes { get; set; }

                            /// <summary>
                            /// ���һ�ε�¼IP
                            /// </summary>
                            public  string  LastLoginIP { get; set; }

                            /// <summary>
                            /// ���һ�ε�¼ʱ��
                            /// </summary>
                            public  DateTime  LastLoginTime { get; set; }

                            /// <summary>
                            /// �����
                            /// </summary>
                            public  string  AddUserId { get; set; }

                            /// <summary>
                            /// ���ʱ��
                            /// </summary>
                            public  DateTime  AddTime { get; set; }

                            /// <summary>
                            /// �޸���
                            /// </summary>
                            public  string  UpdateUserId { get; set; }

                            /// <summary>
                            /// �޸�ʱ��
                            /// </summary>
                            public  DateTime  UpdateTime { get; set; }

                            /// <summary>
                            /// �Ƿ�����(0:��1:��)
                            /// </summary>
                            public  bool  IsLocking { get; set; }

                            /// <summary>
                            /// �Ƿ�ɾ��(0:��1:��)
                            /// </summary>
                            public  bool  IsDelete { get; set; }

                            /// <summary>
                            /// ��ע
                            /// </summary>
                            public  string  Remarks { get; set; }
 }
                           }