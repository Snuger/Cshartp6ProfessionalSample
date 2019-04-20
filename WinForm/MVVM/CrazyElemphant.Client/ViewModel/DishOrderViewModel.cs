using CrazyElemphant.Client.Command;
using CrazyElemphant.Client.Contracts.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrazyElemphant.Client.Bridge;
using System.Windows.Input;
using CrazyElemphant.Client.Model;

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
            UserCiresDevice.UserChooseChanged = new Action(SelectItem);
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
                if (totalCount > 0)
                   this.OrderStateOption = OrderStateType.PadingSubmition;
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

        /// <summary>
        /// DishPushOrder
        /// </summary>    
        public ICommand DishPushOrder => new DelegateCommand(() =>
        {
           bool result=_dishService.DishPushOrder(this.DishOrders.Where(c=>c.IsSelected==true).Select(c=>new DishOrder { Name=c.Dish.Name, Categray=c.Dish.Categray, Count=c.DishCount, UnitPrice=c.Dish.UnitPrice}).ToList());
            if(result)
                this.OrderStateOption = OrderStateType.Submitted;
        });


        private string orderStateDiscription;

        public string OrderStateDiscription
        {
            get { return orderStateDiscription; }
            set
            {
                orderStateDiscription = value;
                this.RaisePropertyChanged("OrderStateDiscription");
            }
        }


        private OrderStateType orderState;

        public OrderStateType OrderStateOption
        {
            get { return orderState; }
            set
            {
                orderState = value;
                switch (orderState)
                {
                    case OrderStateType.None:
                        this.OrderStateDiscription = "你好，欢迎你来本店,请选择你喜欢的菜";
                        break;
                    case OrderStateType.PadingSubmition:
                        this.OrderStateDiscription = "选择好之后，点击下单告诉我们，马上为你准备。";
                        break;
                    case OrderStateType.Submitted:
                        this.OrderStateDiscription = "你的订单已经成功提交。";
                        break;                    
                    case OrderStateType.Cooking:
                        this.OrderStateDiscription = "你的订单正在烹饪。";
                        break;
                    case OrderStateType.Plated:
                        this.OrderStateDiscription = "你的订单已经完成烹饪，马上为你上菜。";
                        break;
                }
                this.RaisePropertyChanged("OrderStateOption");
            }
        }

    }
}
