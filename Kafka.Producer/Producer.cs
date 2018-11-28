using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kafka.Producer
{
    class Producer
    {
        static void Main(string[] args)
        {
            var config = new Dictionary<string, object>{
                { "bootstrap.servers","localhost:9092"}
            };

            using (var producer = new Producer<Null, string>(config, null, new StringSerializer(encoding: Encoding.UTF8)))
            {
                string text = null;

                while (text != "exit")
                {
                    text = Console.ReadLine();
                    producer.ProduceAsync("philosophize-this", null, text);
                }

                producer.Flush(100);
            }
        }
    }
}
