namespace DataCenterCollector.Api.Dtos
{
    public class CreateTopicDto
    {    
        /// <summary>
        ///     The name of the topic to be created (required).
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The number of partitions for the new topic or -1 (the default) if a 
        ///     replica assignment is specified.
        /// </summary>
        public int NumPartitions { get; set; } = -1;     

        /// <summary>
        ///     The replication factor for the new topic or -1 (the default) if a 
        ///     replica assignment is specified instead.
        /// </summary>
        public short ReplicationFactor { get; set; } = -1;
    }
}