using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPatternDesign
{
    public  class MealBuilder
    {
        /// <summary>
        /// 饱汉子
        /// </summary>
        /// <returns></returns>
        public Meal Hanger() { 
            Meal meal = new Meal();
            meal.AddItem(new Coke());
            return meal;
        }

        public Meal Hungry() {
            Meal meal = new Meal();
            meal.AddItem(new VegBurger());
            meal.AddItem(new ChickenBurger());
            meal.AddItem(new Pepsi());
            return meal;
        }

    }
}
