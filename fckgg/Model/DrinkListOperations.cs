using fckgg.DAL;
using System.Collections.ObjectModel;
using fckgg.Patterns;

namespace fckgg.Model
{
    public class DrinkListOperations : OnPropertyChangedClass
    {
        static DrinkListOperations _model = new DrinkListOperations();
        DrinkList vals = new DrinkService().GetDrinks();
        public ObservableCollection<Drink> _drinks;

        public DrinkListOperations()
        {
            _drinks = new ObservableCollection<Drink>();
            foreach (var d in vals.Drinks)
                _drinks.Add(d);
        }

        public static DrinkListOperations Model => _model ?? (_model = new DrinkListOperations());

        public void CreateDrink(int price, string title)
        {
            Drink drink = new Drink
            {
                Price = price,
                Title = title
            };
            XML.CreateDrink(drink);
            _drinks.Clear();
            DrinkList tmpvals = new DrinkService().GetDrinks();
            foreach (var d in tmpvals.Drinks)
                _drinks.Add(d);
        }

        public void DeleteDrink(Drink drink)
        {
            XML.DeleteDrink(drink.Id);
            _drinks.Remove(drink);
            NotifyPropertyChanged("ResultChange");
        }

        public void UpDateDrink(Drink drink)
        {
            XML.UpDateDrink(drink);
        }
    }
}
