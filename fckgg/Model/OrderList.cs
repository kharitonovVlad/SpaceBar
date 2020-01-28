using System;
using System.Collections.ObjectModel;
using fckgg.Patterns;
using fckgg.Controllers;

namespace fckgg.Model
{
    public class OrderList : OnPropertyChangedClass
    {
        static OrderList _model;
        public Drink Drink;
        private int total;
        private DateTime dT;
        public ObservableCollection<Order> orders;
        public ObservableCollection<TitleAndCount> names;       
        public ObservableCollection<Drink> _drinks;       

        public int Total { get { return total; } set { total = value; NotifyPropertyChanged("Total"); } }

        public DateTime DT { get => dT; set { dT = value; NotifyPropertyChanged("DT"); } }

        public static OrderList Model => _model ?? (_model = new OrderList());      

        public OrderList()
        {
            orders = new ObservableCollection<Order>();
            orders = XML.GetOrders();
        }
        public void SaveOrder(Order order)
        {
            Order tmpOrder = new Order
            {
                Total = order.Total
            };
            foreach (var n in order.names) tmpOrder.names.Add(n);
            tmpOrder.DateTime = DateTime.Now;
            orders.Add(tmpOrder);
            XML.SaveXML(tmpOrder);
            order.Clear(order);
        }

        public void ExcelOrder(DateTime date, int price, ObservableCollection<TitleAndCount> titles)
        {
            Order order = new Order
            {
                DateTime = date,
                Total = price,
                names = titles
            };
            ReportController.ExportOrderToExel(order);
        }

        public void ExcelAllOrders(ObservableCollection<Order> orders)
        {
            ReportController.ExportAllOrdersToExcel(orders);
        }

        public void OpenOrder(Order order)
        {
            names = new ObservableCollection<TitleAndCount>();
            names = order.names;
            Total = order.Total;
            DT = order.DateTime;
        }
    }
}
