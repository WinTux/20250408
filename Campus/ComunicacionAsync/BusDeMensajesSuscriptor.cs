
using Campus.Eventos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Campus.ComunicacionAsync
{
    public class BusDeMensajesSuscriptor : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IProcessadorDeEventos _processadorDeEventos;
        private IConnection _connection;
        private IModel _channel;
        private string cola;
        public BusDeMensajesSuscriptor(IConfiguration configuration, IProcessadorDeEventos processadorDeEventos)
        {
            _configuration = configuration;
            _processadorDeEventos = processadorDeEventos;
            IniciarRabbitMQ();
        }

        private void IniciarRabbitMQ()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["Host_RabbitMQ"],
                Port = int.Parse(_configuration["Puerto_RabbitMQ"])
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(
                exchange: "mi_exchange",
                type: ExchangeType.Fanout
            );
            cola = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(
                queue: cola,
                exchange: "mi_exchange",
                routingKey: ""
            );
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                Console.WriteLine("Recibido un mensaje.");
                var cuerpo = ea.Body.ToArray();
                var mensaje = System.Text.Encoding.UTF8.GetString(cuerpo);
                _processadorDeEventos.ProcesarEvento(mensaje);
            };
            _channel.BasicConsume(
                    queue: cola,
                    autoAck: true,
                    consumer: consumer);
            return Task.CompletedTask;
        }
    }
}
