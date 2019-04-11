using CrazyElemphant.Client.Command;
using CrazyElemphant.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyElemphant.Client.ViewModel
{
    public class DishViewModel:NotifiCationObject
    {

        public DelegateCommand Add { get; set; }

        public DelegateCommand Remove { get; set; }

        public Dish Dish { get; set; }

        private int dishcount;

        public int DishCount
        {
            get { return dishcount; }
            set
            {
                dishcount = value;
                this.DishPrice = dishcount * Dish.UnitPrice;                
                this.RaisePropertyChanged("DishCount");
            }
        }

        private double dishPrice;

        public double DishPrice
        {
            get { return dishPrice; }
            set
            {
                dishPrice = value;
                this.RaisePropertyChanged("DishPrice");
            }
        }


        private bool isSelected;
        public DishViewModel()
        {
            this.Add = new DelegateCommand(new Action(DishAdd));
            this.Remove = new DelegateCommand(new Action(DishRemove));

        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                if (isSelected)
                {
                    if(this.DishCount==0)
                         this.DishCount += 1;
                }
                else {
                    this.DishCount = 0;
                }                 
                this.RaisePropertyChanged("IsSelected");
            }
        }

        private void DishAdd()
        {
            this.DishCount += 1;
            if (this.DishCount > 0)
                this.IsSelected = true;
        }

        private void DishRemove()
        {
            if (this.DishCount > 0)            
                this.DishCount -= 1;
            if (this.DishCount == 0)
                this.IsSelected = false;
        }
    }
}
