using System;
                             using System.Text;
                             using System.Collections.Generic;
                             using System.Data;
                             using Newtonsoft.Json;

                             namespace DapperModel.DBModel.SysModel{
                                /// <summary>
                                /// �������ݱ�
                                /// </summary>
                                public class Sys_Article
                                {
                            /// <summary>
                            /// ����Id
                            /// </summary>
                            public  string  Id { get; set; }

                            /// <summary>
                            /// ����ID
                            /// </summary>
                            public  string  CategoryId { get; set; }

                            /// <summary>
                            /// ���±���
                            /// </summary>
                            public  string  Title { get; set; }

                            /// <summary>
                            /// ���¸�����
                            /// </summary>
                            public  string  SubTitle { get; set; }

                            /// <summary>
                            /// ��������
                            /// </summary>
                            public  string  Content { get; set; }

                            /// <summary>
                            /// �������
                            /// </summary>
                            public  int  ViewCount { get; set; }

                            /// <summary>
                            /// ͼƬ��ַ
                            /// </summary>
                            public  string  ImageUrl { get; set; }

                            /// <summary>
                            /// ����
                            /// </summary>
                            public  int  Sort { get; set; }

                            /// <summary>
                            /// SEO����
                            /// </summary>
                            public  string  SeoTitle { get; set; }

                            /// <summary>
                            /// ����SEO�ؼ���
                            /// </summary>
                            public  string  SeoKeywords { get; set; }

                            /// <summary>
                            /// ����SEO����
                            /// </summary>
                            public  string  SeoDescription { get; set; }

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
                            /// �Ƿ��ö�
                            /// </summary>
                            public  bool  IsTop { get; set; }

                            /// <summary>
                            /// �Ƿ�����
                            /// </summary>
                            public  bool  IsRed { get; set; }

                            /// <summary>
                            /// �Ƿ񷢲�
                            /// </summary>
                            public  bool  IsPublish { get; set; }

                            /// <summary>
                            /// �Ƿ�ɾ��
                            /// </summary>
                            public  bool  IsDeleted { get; set; }
 }
                           }