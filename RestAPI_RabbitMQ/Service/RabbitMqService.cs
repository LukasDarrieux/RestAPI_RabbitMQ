using RabbitMQ.Client;
using RestAPI_RabbitMQ.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RestAPI_RabbitMQ.Service
{
    public class RabbitMqService : IDisposable
    {
        private IModel channel;
        private IConnection connection;
        public RabbitMqService()
        {
            
        }

        public bool SendMessage(Message message)
        {
            try
            {
                InicializeRabbitMQClient();

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

                channel.BasicPublish("", "Message", null, body);

                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public List<Message>? GetMessages()
        {
            var messages = new List<Message>();
            try
            {
                InicializeRabbitMQClient();
                while (true)
                {
                    var result = channel.BasicGet("Message", true);
                    if (result is null) break;

                    var body = result.Body.ToArray();
                    var json = Encoding.UTF8.GetString(body);

                    var message = JsonSerializer.Deserialize<Message>(json);
                    if (message != null)
                    {
                        messages.Add(message);
                    }
                }
            }
            catch(Exception e)
            {
                messages.Add(new Message()
                {
                    Title = "Erro Message Exception",
                    Body = $"Erro: {e.Message}"
                });
            }

            return messages;
            
        }

        private void InicializeRabbitMQClient()
        {
            var factory = new ConnectionFactory { HostName = "localhost", Port = 5672, UserName = "guest", Password = "guest" };
            this.connection = factory.CreateConnection();
            this.channel = connection.CreateModel();
            this.channel.QueueDeclare("Message", false, false, false, null);
        }

        public void Dispose()
        {
            if (!(channel is null)) channel.Dispose();
            if (!(connection is null)) connection.Dispose();
        }
    }
}
