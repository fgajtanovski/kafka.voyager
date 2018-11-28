using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace Kafka.Consumer
{
    class Consumer
    {
        static void Main(string[] args)
        {
            var config = new Dictionary<string, object> {
                { "group.id","thoughts-consumer"},
                { "bootstrap.servers","localhost:9092"},
                { "enable.auto.commit", "false"}
            };

            using (var consumer = new Consumer<Null, string>(config, null, new StringDeserializer(encoding: Encoding.UTF8)))
            {
                consumer.Subscribe(new string[] { "philosophize-this" });

                consumer.OnMessage += (_, msg) =>
                {
                    Console.WriteLine($"Topic: {msg.Topic} Partion: {msg.Partition} Offset: {msg.Offset} {msg.Value}");
                    consumer.CommitAsync(msg);
                };

                while (true)
                {
                    consumer.Poll(100);
                }
            }
        }
    }
}
