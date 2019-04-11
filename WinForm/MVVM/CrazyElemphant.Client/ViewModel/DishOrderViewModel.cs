using CrazyElemphant.Client.Command;
using CrazyElemphant.Client.Contracts.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyElemphant.Client.ViewModel
{
    public class DishOrderViewModel:NotifiCationObject
    {
       
        private IDishService _dishService;
        private List<DishViewModel> dishOrders;

        public DelegateCommand SelectCommand { get; set; }

        public DishOrderViewModel(IDishService dishService)
        {           
            _dishService = dishService;
            loadDishs();
            SelectCommand = new DelegateCommand(new Action(SelectItem));
        }

        public List<DishViewModel> DishOrders
        {
            get { return dishOrders; }
            set
            {
                dishOrders = value;  
                this.RaisePropertyChanged("DishOrders");
            }
        }

        protected void loadDishs()
        {         
            var models = _dishService.GetItems();
            this.DishOrders = (from a in models select new DishViewModel() { Dish=a, IsSelected=false }).ToList();
        }

        private void SelectItem() {
           this.TotalCount=this.DishOrders.Where(i => i.IsSelected == true).Sum(c => c.DishCount);
            this.TotalPrice = this.DishOrders.Where(i => i.IsSelected == true).Sum(c => c.DishPrice);
        }

        private int totalCount;
        public int TotalCount
        {
            get { return totalCount; }
            set
            {
                totalCount = value;
                this.RaisePropertyChanged("TotalCount");
            }
        }

        private double totalPrice;
        public double TotalPrice
        {
            get { return totalPrice; }
            set
            {
                totalPrice = value;
                this.RaisePropertyChanged("TotalPrice");
            }
        }

    }
}
