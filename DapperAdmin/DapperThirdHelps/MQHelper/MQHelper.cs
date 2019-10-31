using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperThirdHelps.MQHelper
{
    /// <summary>
    /// Author：Geek Dog  Content：消息队列帮助类 AddTime：2019-5-8 10:41:27  
    /// </summary>
    public static class MQHelper
    {
        //1:实例化连接工厂(建立私有只能在本类中调用)
        private static ConnectionFactory factory = new ConnectionFactory()
        {
            HostName = ConfigurationManager.AppSettings["HostName"],
            UserName = ConfigurationManager.AppSettings["UserName"],
            Password = ConfigurationManager.AppSettings["Password"],
            VirtualHost = "/",
        };
        #region 发布消息

        /// <summary>
        /// Author：Geek Dog  Content：发布消息(发送内容为实体对象) AddTime：2019-5-8 11:43:49  
        /// </summary>
        /// <param name="QueueName">队列名称</param>
        /// <param name="Model">发送实体对象</param>
        public static void PublishEntityObject<T>(string QueueName, T MessageModel)
        {

            //2. 建立连接
            using (var connection = factory.CreateConnection())
            {
                //3. 创建信道
                using (var channel = connection.CreateModel())
                {
                    //4. 申明队列(queue:队列名,durable:是否持久化,exclusive:是否唯一,autoDelete:是否自动删除)
                    channel.QueueDeclare(QueueName, true, false, false, null);
                    //5. 构建byte消息数据包
                    string message = ObjectToJson(MessageModel);

                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2;

                    var body = Encoding.UTF8.GetBytes(message);
                    //6. 发送数据包
                    channel.BasicPublish("", QueueName, properties, body);
                }
            }
        }

        /// <summary>
        /// Author：Geek Dog  Content：发布消息(发送内容为实体对象) AddTime：2019-5-8 11:43:49  
        /// </summary>
        /// <param name="QueueName">队列名称</param>
        /// <param name="Model">发送实体对象</param>
        public static void PublishString(string QueueName, string Message)
        {
            //2. 建立连接
            using (var connection = factory.CreateConnection())
            {
                //3. 创建信道
                using (var channel = connection.CreateModel())
                {
                    //4. 申明队列(queue:队列名,durable:是否持久化,exclusive:是否唯一,autoDelete:是否自动删除)
                    channel.QueueDeclare(QueueName, false, false, false, null);
                    //5. 构建byte消息数据包
                    string message = Message;

                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2;

                    var body = Encoding.UTF8.GetBytes(message);
                    //6. 发送数据包
                    channel.BasicPublish("", QueueName, properties, body);
                }
            }
        }
        #endregion

        #region 接收消息
        /// <summary>
        /// Author：Geek Dog  Content：接收队列消息(接收内容为实体对象) AddTime：2019-5-8 13:58:19  
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="QueueName">队列名称</param>
        /// <returns></returns>
        public static T ReceiveEntityObject<T>(string QueueName)
        {
            var message = String.Empty;
            List<T> List = new List<T>();
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(QueueName, true, false, false, null);

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(QueueName, false, consumer);

                    //while (true)
                    //{
                    var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                    var body = ea.Body;
                    message = Encoding.UTF8.GetString(body);
                    return JsonToObject<T>(message);

                    channel.BasicAck(ea.DeliveryTag, false);
                    //}
                }
            }

        }

        #endregion

        #region 数据处理单独放到当前项目不引用其他项目方法,防止循环引用
        /// <summary>
        /// 序列化实体对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 序列化JSON
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
        #endregion
    }
}
