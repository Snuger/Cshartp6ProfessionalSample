using System;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;

namespace Sampke.Serialization.Productor.Buillder
{
    public class SampkeProducterBuilder<TValue> : ProducerBuilder<Null, TValue> where TValue : class, new()
    {

        public SampkeProducterBuilder(ISchemaRegistryClient cachedSchemaRegistryClient, ProducerConfig producerConfig) : base(producerConfig)
        {
            if (cachedSchemaRegistryClient is null)
                throw new ArgumentNullException(nameof(cachedSchemaRegistryClient));
            if (producerConfig is null)
                throw new ArgumentNullException(nameof(producerConfig));
            base.SetValueSerializer(new JsonSerializer<TValue>(cachedSchemaRegistryClient, new JsonSerializerConfig() { BufferBytes = 100 }, new NJsonSchema.Generation.JsonSchemaGeneratorSettings() { AllowReferencesWithProperties = true, AlwaysAllowAdditionalObjectProperties = true }));

        }
    }
}