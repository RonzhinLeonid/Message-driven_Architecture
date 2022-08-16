﻿using RabbitMQ.Client;
using SettingRabbit;
using System.Net.Security;
using System.Text;

namespace Messaging
{
    public class Producer
    {
        private readonly ConnectionFactory _connectionFactory;

        public Producer()
        {
            _connectionFactory = new ConnectionFactory
            {
                HostName = Setting.HostName,
                VirtualHost = Setting.VirtualHost,
                UserName = Setting.UserName, 
                Password = Setting.Password, 
                Port = Setting.Port,
                RequestedHeartbeat = TimeSpan.FromSeconds(10),
                Ssl = new SslOption
                {
                    Enabled = true,
                    AcceptablePolicyErrors = SslPolicyErrors.RemoteCertificateNameMismatch |
                                             SslPolicyErrors.RemoteCertificateChainErrors,
                    Version = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls11
                }
            };
        }

        /// <summary>
        /// Отправить сообщение в очередь
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="message"></param>
        public void SendToQueue(string queueName, string message)
        {
            var data = Encoding.UTF8.GetBytes(message);
            if (data.Length > 0)
            {
                try
                {
                    using IConnection connection = _connectionFactory.CreateConnection();
                    using (StreamReader reader = new StreamReader(new MemoryStream(data)))
                    {
                        using (var channel = connection.CreateModel())
                        {
                            channel.BasicPublish(exchange: "",
                                routingKey: queueName,
                                body: data);
                            channel.Close();
                        }
                        Console.WriteLine($"Сообщение отправлено в очередь {queueName}");
                        reader.Close();
                        connection.Close();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка отправки сообщения в очередь {queueName}. " +
                                      $"Сведения о сообщении: '{Encoding.UTF8.GetString(data, 0, data.Length)}' " +
                                      $"Сведения об ошибке:" + e.Message + "/" + e?.InnerException);
                }
            }
        }
    }
}
