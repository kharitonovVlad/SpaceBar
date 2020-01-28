using System.Xml.Serialization;

namespace fckgg.Model
{
    [XmlRoot("DrinkList")]
    public class Drink : OnPropertyChangedClass
    {
        private string pic;
        private int price;
        private string title;
        private int id;

        public string Pic { get { return pic; } set { pic = value; NotifyPropertyChanged("Pic"); } }
        public int Price { get { return price; } set { price = value; NotifyPropertyChanged("Price"); } }
        public string Title { get { return title; } set { title = value; NotifyPropertyChanged("Title"); } }
        public int Id { get { return id; } set { id = value; NotifyPropertyChanged("Id"); } }

    }
}
