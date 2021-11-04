using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace PrototypePattern
{
    public  class Shape:ICloneable
    {
        public string ID { get; set; }

        public string Type { get; set; }

        public virtual string GetID() => ID;

        public virtual void SetId(string id) => ID = id;

        public virtual string Draw()=>$"{JsonSerializer.Serialize(this)}";

        public object Clone()
        {
            return MemberwiseClone();
        }
        public object DeepClone() {
            using MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Seek(0, SeekOrigin.Begin);
            return formatter.Deserialize(stream);
        }
    }
}
