using System;
using RabbitMQ.Client;
using System.Text;

namespace Send
{
    class Send
    {
		public static void Main()
		{
			var factory = new ConnectionFactory() { HostName = "localhost" };
            factory.Endpoint.Port = 1111;//5672
            using (var connection = factory.CreateConnection())
			using(var channel = connection.CreateModel())
			{
				channel.QueueDeclare(queue: "hello",
									 durable: false,
									 exclusive: false,
									 autoDelete: false,
									 arguments: null);
                Console.WriteLine(" Press text to send .");
                //string message = "Hello World!";
                string message = Console.ReadLine(); 
				var body = Encoding.UTF8.GetBytes(message);
                Console.WriteLine(" Press [enter] to send message.");
                Console.ReadLine();
                channel.BasicPublish(exchange: "",
									 routingKey: "hello",
									 basicProperties: null,
									 body: body);
				Console.WriteLine(" [x] Sent {0}", message);
			}

			Console.WriteLine(" Press [enter] to exit.");
			Console.ReadLine();
		}
    }
}
