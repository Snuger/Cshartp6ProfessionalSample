using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BasicBuilderDesigin
{
    abstract  class CarBuilder
    {
        public Car Car { get; set; } = new Car();

        public abstract void ConfigEngine();

        public abstract void ConfigWheel();

        public abstract void DesignBrand();

        public virtual Car Delivery() {
            //
            ConfigEngine();

            ConfigWheel();

            DesignBrand();

            if (Car is null)
                throw new ArgumentNullException(nameof(Car));
            if (string.IsNullOrWhiteSpace(Car.Brand))
                throw new ArgumentNullException(nameof(Car.Brand));
            if (Car.Engine==null)
                throw new ArgumentNullException(nameof(Car.Engine));
            if (Car.Wheels.GroupBy(c => c.Size).Count() > 1)
                throw new Exception("wheel Size inconsistent");
            if (Car.Wheels.Any(c => string.IsNullOrWhiteSpace(c.Tires.Brand)))
                throw new Exception("Tires not installed");
            return Car;        
        }
    }
}
