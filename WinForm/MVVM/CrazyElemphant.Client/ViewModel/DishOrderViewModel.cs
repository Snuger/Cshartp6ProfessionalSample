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


        public DelegateCommand SelectItemCommand { get; set; }


        private int count;

        public int Count
        {
            get { return count; }
            set
            {
                count = value;
                this.RaisePropertyChanged("Count");
            }
        }

        private List<DishViewModel> dishOrders;
  

        public DishOrderViewModel(IDishService dishService)
        {
            _dishService = dishService;
            loadDishs();
           // this.SelectItemCommand = new DelegateCommand(new Action(this.SelectItemExecute));
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

        private void SelectItemExecute() {
            this.Count = this.DishOrders.Count(i => i.IsSelected == true);
        }


        protected void loadDishs()
        {         
            var models = _dishService.GetItems();
            this.DishOrders = (from a in models select new DishViewModel() { Dish=a, IsSelected=false }).ToList();

        }


    }
}
