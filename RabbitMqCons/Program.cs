using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
namespace RabbitMqCons
{
    static class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"Welcome {message}");
                };
                channel.BasicConsume(queue: "NameQueue",
                    autoAck: true,
                    consumer: consumer);
                Console.ReadLine();
            }
        }
    }
}
