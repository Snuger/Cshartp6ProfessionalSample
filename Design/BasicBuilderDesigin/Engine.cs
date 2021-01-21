using System;
using System.Collections.Generic;
using System.Text;

namespace BasicBuilderDesigin
{
    public class Engine
    {

        public Guid EngineNum { get; set; } = System.Guid.NewGuid();

        public string Displacement { get; set; }

        public DateTime FactoryTime { get; set; } = System.DateTime.Now;

    }
}
