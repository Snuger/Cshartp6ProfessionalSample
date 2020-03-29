using CrazyElemphant.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyElemphant.Client.ViewModel
{
    public  class RestaurantViewModel:NotifiCationObject
    {
        private Restaurant restaurant;

        public RestaurantViewModel()
        {
            this.restaurant = new Restaurant() {
                 Name= "Crazy大象",
                 Address= "北京市海淀区万泉河路紫金庄园1号楼 1层 113室（亲们：这地儿特难找！）",
                 PhoneNumber= "15210365423 or 82650336"
            };
            
        }

        public Restaurant Restaurant
        {
            get { return restaurant; }
            set
            {
                restaurant = value;
                this.RaisePropertyChanged("Restaurant");
            }
        }

    }
}
