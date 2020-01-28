using System;
using System.Collections.ObjectModel;

namespace fckgg.Model
{
    public class Order : OnPropertyChangedClass
    {
        public ObservableCollection<TitleAndCount> names;
        
        private Drink drink;
        static Order _model;
        private int total;
       
        public Drink Drink { get => drink; set { drink = value; NotifyPropertyChanged("Drink"); } }
        public int Total { get { return total; } set { total = value; NotifyPropertyChanged("Total"); } }
        private TitleAndCount tNc { get; set; }
        public DateTime DateTime { get; set; }
        private int Id { get; set; }

        public Order()
        {
            names = new ObservableCollection<TitleAndCount>();
        }

        public static Order Model => _model ?? (_model = new Order());

        public void GetDrinkToUpDate(Drink drink)
        {
            Drink = drink;
        }

        public void Add(Drink drink)
        {
            Total += drink.Price;
            if (names.Count == 0)
            {
                tNc = new TitleAndCount(drink);
                names.Add(tNc);
                NotifyPropertyChanged("ResultChange");
                return;
            }
            foreach (var j in names)
                if (j.Title.Equals(drink.Title))
                {
                    j.Count++;
                    NotifyPropertyChanged("ResultChange");
                    return;
                }
            tNc = new TitleAndCount(drink);
            names.Add(tNc);
            NotifyPropertyChanged("ResultChange");
        }

        public void Clear(Order order)
        {
            Total = 0;
            names.Clear();
            NotifyPropertyChanged("ResultChange");
        }

        public void Del(TitleAndCount tNc)
        {
            Total -= tNc.Price;
            tNc.Count--;
            if (tNc.Count == 0) names.Remove(tNc);
            NotifyPropertyChanged("ResultChange");
        }
    }
}
