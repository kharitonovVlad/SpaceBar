
namespace fckgg.Model
{
    public class TitleAndCount : OnPropertyChangedClass
    {
        private string title;
        private int count;
        private int price;

        public string Title { get { return title; } set { title = value; NotifyPropertyChanged("Title"); } }
        public int Count { get { return count; } set { count = value; NotifyPropertyChanged("Count"); } }
        public int Price { get { return price; } set { price = value; NotifyPropertyChanged("Price"); } }

        public TitleAndCount() { }


        public TitleAndCount(Drink drink)
        {
            Title = drink.Title;
            Count = 1;
            Price = drink.Price;
        }
    }
}
