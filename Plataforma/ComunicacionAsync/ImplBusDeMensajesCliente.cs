using Microsoft.EntityFrameworkCore.Metadata;
using Plataforma.DTO;
using RabbitMQ.Client;
using System;

namespace Plataforma.ComunicacionAsync
{
    public class ImplBusDeMensajesCliente : IBusDeMensajesCliente
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        public ImplBusDeMensajesCliente(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = _configuration["Host_RabbitMQ"],
                Port = int.Parse(_configuration["Puerto_RabbitMQ"]),
            };
            try {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(
                    exchange: "mi_exchange",
                    type: ExchangeType.Fanout
                );
            }
            catch (Exception e) {
                Console.WriteLine($"Error al conectar a RabbitMQ: {e.Message}");
            }
        }
        public void PublicarNuevoEstudiante(EstudiantePublisherDTO estudiantePublisherDTO)
        {
            Console.WriteLine($"Publicando nuevo estudiante: {estudiantePublisherDTO.nombre} {estudiantePublisherDTO.apellido}");
            string mensaje = System.Text.Json.JsonSerializer.Serialize(estudiantePublisherDTO);
            if (_connection.IsOpen) 
                Enviar(mensaje);
            else
                Console.WriteLine("No se puede enviar el mensaje, la conexión está cerrada.");
        }

        private void Enviar(string mensaje)
        {
            var cuerpo = System.Text.Encoding.UTF8.GetBytes(mensaje);
            _channel.BasicPublish(
                exchange: "mi_exchange",
                routingKey: "",
                basicProperties: null,
                body: cuerpo
            );
        }
        private void Finalizar() { 
            if(_channel != null)
            {
                _channel.Close();
                _connection.Close();
            }
        }
    }
}
