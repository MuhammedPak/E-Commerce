
using MyEcom.Common.RabbitmqDto;
using MyEcom.Common.UserDto;
using MyEcom.Core.Service.Interfaces;
using Nest;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEcom.Core.Service
{
    public class RabbitmqService : IRabbitMQService
    {

        public void SendDataToAdmin(RabbitmqDto rabbitmqDto)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (RabbitMQ.Client.IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: rabbitmqDto.otelid.ToString(),
                                       durable: false,
                                       exclusive: false,
                                       autoDelete: false,
                                       arguments: null);

                string message = JsonConvert.SerializeObject(rabbitmqDto);
                var body = Encoding.UTF8.GetBytes(message);
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: "",
                                     routingKey: rabbitmqDto.otelid.ToString(),
                                     basicProperties: null,                                     
                                     body: body);
                    
            }

            List<RabbitmqDto> rabbitmqDtos = new List<RabbitmqDto>();
            rabbitmqDtos.Add(rabbitmqDto);
            Services.ReservationService.AddReservation(rabbitmqDtos);


        }
        public void TakeDataToUser(int otelid)
        {
           
            List<RabbitmqDto> reservations = new List<RabbitmqDto>();
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (RabbitMQ.Client.IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue:otelid.ToString(),
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                var consumer = new QueueingBasicConsumer(channel);

                uint h = channel.MessageCount(otelid.ToString());

                channel.BasicConsume(queue:otelid.ToString(),
                    autoAck: true,           
                    consumer: consumer
                );

                for (int j = 0; j < h; j++)
                {

                    var datas = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                    var data = Encoding.UTF8.GetString(datas.Body);
                    RabbitmqDto mdata = JsonConvert.DeserializeObject<RabbitmqDto>(data);
                    reservations.Add(mdata);
                }
                

            

             
            }
        }

    }
}
