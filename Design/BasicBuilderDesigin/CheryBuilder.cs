using System;
using System.Collections.Generic;
using System.Text;

namespace BasicBuilderDesigin
{
    class CheryBuilder : CarBuilder
    {      
        public override void ConfigEngine()
        {
            Car.Engine = new Engine()
            {
                Displacement = "1.5T"
            };
        }

        public override void ConfigWheel()
        {
            Car.Wheels[0] = new Wheel() { Position = "left-befor", Size = 18, Tires = new Tires() { Brand = "马吉斯" } };
            Car.Wheels[1] = new Wheel() { Position = "right-befor", Size = 18, Tires = new Tires() { Brand = "马吉斯" } };
            Car.Wheels[2] = new Wheel() { Position = "left-after", Size = 18, Tires = new Tires() { Brand = "马吉斯" } };
            Car.Wheels[3] = new Wheel() { Position = "right-after", Size = 18, Tires = new Tires() { Brand = "马吉斯" } };
        }

        public override void DesignBrand()
        {
            Car.Brand = "QQ";           
        }
    }
}
