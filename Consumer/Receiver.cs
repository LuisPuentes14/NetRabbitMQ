
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "alejandro",
    Password = "12345",
};


using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{

    channel.QueueDeclare("VaxiQueue", false, false, false, null);

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.Span;
        var message = Encoding.UTF8.GetString(body);

        Console.WriteLine("Mensaje recibido {0}",message);      
       
    };
    // una ves recibido se quita del Queue, simpre y cuando 
    // el segundo parametro este en true, si esta en falso no lo quita
    channel.BasicConsume("VaxiQueue", true, consumer);

}

Console.WriteLine("Presiona [Enter] para salir de la aplicacion");
Console.ReadLine();
