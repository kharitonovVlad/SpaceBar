using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using fckgg.Model;

namespace fckgg
{
    public class ViewModel : OnPropertyChangedClass
    {
        private Drink upDataDrink;
        private Order model;
        private OrderList orderList;
        private DrinkListOperations drinkListOperations;
        private Drink _drink;

        public Drink createDrink;
        public Order order;

        public int Price { get; set; }
        public string Title { get; set; }
        public int? Result => model.Total;
        public Drink drink { get => _drink; set { _drink = value; NotifyPropertyChanged("drink"); } }
        public ObservableCollection<Order> Orders { get; set; }
        public ObservableCollection<TitleAndCount> Names { get; set; }
        public DateTime DateTimes { get; set; }
        public int Total { get; set; }
        public ObservableCollection<Drink> Drinks { get; set; }
        public ObservableCollection<TitleAndCount> Titles { get; set; }

        private RelayCommand _upCommand;
        private RelayCommand _delCommand;
        private RelayCommand _savelCommand;
        private RelayCommand _openCommand;
        private RelayCommand _delDrinkCommand;
        private RelayCommand _upDateWindowOpenCommand;
        private RelayCommand _createDrinkCommand;
        private RelayCommand _excelCommand;
        private RelayCommand _excelOrdersCommand;

        public RelayCommand UpCommand => _upCommand ?? (_upCommand = new RelayCommand(UpMetod));       
        public RelayCommand DelCommand => _delCommand ?? (_delCommand = new RelayCommand(DelMetod));        
        public RelayCommand OpenCommand => _openCommand ?? (_openCommand = new RelayCommand(OpenMetodAsync));        
        public RelayCommand DelDrinkCommand => _delDrinkCommand ?? (_delDrinkCommand = new RelayCommand(DelDrinkMetod));       
        public RelayCommand UpDateWindowOpenCommand => _upDateWindowOpenCommand ?? (_upDateWindowOpenCommand = new RelayCommand(UpDateWindowOpenMetod));              
        public RelayCommand CreateDrinkCommand => _createDrinkCommand ?? (_createDrinkCommand = new RelayCommand(CreateDrinkMetod));        
        public RelayCommand ExcelCommand => _excelCommand ?? (_excelCommand = new RelayCommand(ExcelMetod));        
        public RelayCommand ExcelOrdersCommand => _excelOrdersCommand ?? (_excelOrdersCommand = new RelayCommand(ExcelOrdersMetod));
        public RelayCommand SaveCommand => _savelCommand ?? (_savelCommand = new RelayCommand(SaveMetod));

        private void ExcelMetod(object parameter) => orderList.ExcelOrder(DateTimes, Total, Names);

        private void UpDateWindowOpenMetod(object parameter) => drinkListOperations.UpDateDrink((Drink)parameter);

        private void DelDrinkMetod(object parameter) => drinkListOperations.DeleteDrink((Drink)parameter);

        private void DelMetod(object parameter) => model.Del((TitleAndCount)parameter);

        private void UpMetod(object parameter) => model.Add((Drink)parameter);

        private void ExcelOrdersMetod(object parameter)
        {
            if (Orders.Count == 0)
            {
                new Exception("Список заказов пуст.");
                return;
            }
            orderList.ExcelAllOrders(Orders);
        }
       
        private void CreateDrinkMetod(object parameter)
        {
            if (Title == "")
            {
                new Exception("Поле \"Наименование\" не заполнено!");
                return;
            }
            drinkListOperations.CreateDrink(Price, Title);
        }
        
        private async void OpenMetodAsync(object parameter)
        {
            order = new Order();
            order = (Order)parameter;
            orderList.OpenOrder((Order)parameter);
            var displayRootRegistry = (Application.Current as App).displayRootRegistry;
            var otherWindowViewModel = new OrderWindowViewModel();
            await displayRootRegistry.ShowModalPresentation(otherWindowViewModel);
        }

        private void SaveMetod(object parameter)
        {
            if (model.names.Count == 0)
            {
                new Exception("Напитки не добавлены!");
                return;
            }

            orderList.SaveOrder(model);
        }

        public ViewModel()
        {
            PrepareViewModel();
        }

        protected void PrepareViewModel()
        {
            Drinks = new ObservableCollection<Drink> { };
            Orders = new ObservableCollection<Order>() { };
            Titles = new ObservableCollection<TitleAndCount>() { };
            Names = new ObservableCollection<TitleAndCount>() { };
            _drink = new Drink();
            createDrink = new Drink();
            Price = 0;
            Title = "";
            upDataDrink = new Drink();
            model = Order.Model;
            drink = model.Drink;
            orderList = OrderList.Model;           
            drinkListOperations = DrinkListOperations.Model;           
            model.PropertyChanged += Model_PropertyChanged;
            Titles = model.names;
            Orders = orderList.orders;
            Drinks = drinkListOperations._drinks;
            Names = orderList.names;
            DateTimes = orderList.DT;
            Total = orderList.Total;
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ResultChange")
            {
                NotifyPropertyChanged("Result");
            }
        }
    }
}
