//using RabbitMQ.Client.Events;
//using RabbitMQ.Client;
//using RoomReservation.Domain.Entities;
//using System.Text;
//using System.Text.Json;
//using RoomReservation.Domain.Repositories;
//using Microsoft.Extensions.DependencyInjection;

//namespace RoomReservation.Common.RabbitMq
//{
//    public interface IRabbitMqConsumer : IDisposable
//    {
//        void StartListening();
//    }

//    public class RabbitMqConsumer : IRabbitMqConsumer
//    {
//        private readonly IConnection _connection;
//        private readonly IModel _channel;
//        private readonly string _queueName = "booking_queue";
//        private readonly IBookingRepository _bookingRepository;

//        public RabbitMqConsumer(IBookingRepository bookingRepository)
//        {
//            var factory = new ConnectionFactory
//            {
//                HostName = "localhost",
//                Port = 5672,
//                UserName = "guest",
//                Password = "guest"
//            };

//            _connection = factory.CreateConnection();
//            _channel = _connection.CreateModel();
//            _bookingRepository = bookingRepository;

//            _channel.QueueDeclare(
//                queue: _queueName,
//                durable: true,
//                exclusive: false,
//                autoDelete: false,
//                arguments: null
//            );
//        }

//        public void StartListening()
//        {
//            var consumer = new EventingBasicConsumer(_channel);

//            consumer.Received += async (model, ea) =>
//            {
//                var body = ea.Body.ToArray();
//                var message = Encoding.UTF8.GetString(body);
//                var booking = JsonSerializer.Deserialize<Booking>(message);

//                Console.WriteLine($"[Consumer] Received message for booking: {booking?.ClientId}. ");

//                // Save the booking to the database using Entity Framework
//                //Task.WaitAll(_bookingRepository.CreateForRabbitMq(booking));
//                //await SaveBookingToDatabaseAsync(booking);
//            };

//            _channel.BasicConsume(
//                queue: _queueName,
//                autoAck: true,
//                consumer: consumer
//            );

//            Console.WriteLine("RabbitMQ Consumer is listening for messages...");
//        }

//        public void Dispose()
//        {
//            _channel?.Dispose();
//            _connection?.Dispose();
//        }
//    }
//}
