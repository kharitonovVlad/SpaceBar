using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;

namespace fckgg.Model
{
    [XmlRoot("DrinkList")]
    public class DrinkList
    {      
        [XmlElement("Drink")]
        public List<Drink> Drinks { get; set; }


    }
}
