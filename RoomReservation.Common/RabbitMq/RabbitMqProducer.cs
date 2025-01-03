namespace RoomReservation.Common.RabbitMq;

using RabbitMQ.Client;
using RoomReservation.Domain.Entities;
using System;
using System.Text;
using System.Text.Json;

public interface IRabbitMqProducer : IDisposable
{

}

public class RabbitMqProducer : IRabbitMqProducer
{
    private readonly string _queueName = "booking_queue";
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMqProducer()
    {
        // Initialize the connection factory
        var factory = new ConnectionFactory
        {
            HostName = "localhost", // RabbitMQ server hostname
            Port = 5672,            // Default RabbitMQ port
            UserName = "guest",     // Default username
            Password = "guest",     // Default password
            DispatchConsumersAsync = false // For non-async consumers (default)
        };

        // Create a connection and channel
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        // Declare the queue (ensure it exists)
        _channel.QueueDeclare(
            queue: _queueName,
            durable: true,  // The queue will survive broker restarts
            exclusive: false, // The queue can be used by multiple connections
            autoDelete: false, // The queue won't be deleted automatically
            arguments: null // No extra arguments
        );
    }

    /// <summary>
    /// Sends a booking message to the RabbitMQ queue.
    /// </summary>
    /// <param name="booking">The booking object to send.</param>
    public void SendBooking(Booking booking)
    {
        // Serialize the booking object to JSON
        var message = JsonSerializer.Serialize(booking);

        // Convert the JSON string to a byte array
        var body = Encoding.UTF8.GetBytes(message);

        // Publish the message to the default exchange with the queue as the routing key
        _channel.BasicPublish(
            exchange: "",              // Default exchange
            routingKey: _queueName,    // Queue name (routing key)
            basicProperties: null,     // No additional properties
            body: body                 // Message body
        );

        Console.WriteLine($"[Producer] Sent booking: {booking.Id}");
    }

    /// <summary>
    /// Dispose of the RabbitMQ connection and channel.
    /// </summary>
    public void Dispose()
    {
        // Clean up resources
        _channel?.Close();
        _connection?.Close();
    }
}
