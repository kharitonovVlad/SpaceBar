using fckgg.Model;
using fckgg.Patterns;

namespace fckgg.DAL
{
    public class DrinkService 
    {
        public DrinkList GetDrinks()
        {
            var path = "./Content/bar.xml";
            return XML.LoadObjectFromFile<DrinkList>(path);
        }
    }
}
