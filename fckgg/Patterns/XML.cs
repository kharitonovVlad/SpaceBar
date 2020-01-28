using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Linq;
using fckgg.Model;
using fckgg.DAL;

namespace fckgg.Patterns
{
    public class XML
    {
        public static ObservableCollection<Drink> DeleteDrink(int id)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("./Content/bar.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            ObservableCollection<Drink> drinks = new ObservableCollection<Drink>();
            foreach (XmlNode xnode in xRoot)
            {
                XmlNode attr = xnode.Attributes.GetNamedItem("id");
                if (attr.Value == id.ToString()) xRoot.RemoveChild(xnode);
            }
            xDoc.Save("./Content/bar.xml");
            var vals = new DrinkService().GetDrinks();
            foreach (var d in vals.Drinks)
                drinks.Add(d);
            return drinks;
        }

        public static void CreateDrink(Drink drink)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("./Content/bar.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            var value = XDocument.Load("./Content/bar.xml")
                      .Descendants("Drink")
                      .Last()
                      .Attribute("id").Value;
            drink.Id = int.Parse(value) + 1;
            XmlElement drinkElem = xDoc.CreateElement("Drink");
            XmlAttribute drinkAttr = xDoc.CreateAttribute("id");
            XmlText drinkText = xDoc.CreateTextNode(drink.Id.ToString());
            drinkAttr.AppendChild(drinkText);
            XmlElement picElem = xDoc.CreateElement("Pic");
            XmlText picText = xDoc.CreateTextNode("Resources/Assets/SpaceBar.png");
            picElem.AppendChild(picText);
            XmlElement priceElem = xDoc.CreateElement("Price");
            XmlText priceText = xDoc.CreateTextNode(drink.Price.ToString());
            priceElem.AppendChild(priceText);
            XmlElement titleElem = xDoc.CreateElement("Title");
            XmlText titleText = xDoc.CreateTextNode(drink.Title);
            titleElem.AppendChild(titleText);
            XmlElement idElem = xDoc.CreateElement("Id");
            XmlText idText = xDoc.CreateTextNode(drink.Id.ToString());
            idElem.AppendChild(idText);
            drinkElem.Attributes.Append(drinkAttr);
            drinkElem.AppendChild(picElem);
            drinkElem.AppendChild(priceElem);
            drinkElem.AppendChild(titleElem);
            drinkElem.AppendChild(idElem);
            xRoot.AppendChild(drinkElem);
            xDoc.Save("./Content/bar.xml");
        }

        public static void UpDateDrink(Drink drink)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("./Content/bar.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            XmlElement countElemPrice;
            XmlElement countElemTitle;
            XmlElement countElemPic;
            XmlElement countElemId;
            XmlText countTextPrice;           
            XmlText countTextTitle;
            XmlText countTextPic = xDoc.CreateTextNode("");
            XmlText countTextId = xDoc.CreateTextNode("");
            XmlText attrIdText = xDoc.CreateTextNode(drink.Id.ToString());
            XmlAttribute attrId = xDoc.CreateAttribute("id");
                       
            foreach (XmlNode xnode in xRoot)
            {
                XmlNode attr = xnode.Attributes.GetNamedItem("id");
                if (attr.Value == drink.Id.ToString())
                {
                    countElemPrice = xDoc.CreateElement("Price");
                    countElemTitle = xDoc.CreateElement("Title");
                    countElemPic = xDoc.CreateElement("Pic");
                    countElemId = xDoc.CreateElement("Id");

                    countTextPrice = xDoc.CreateTextNode(drink.Price.ToString());
                    countTextTitle = xDoc.CreateTextNode(drink.Title);
                    countTextId = xDoc.CreateTextNode(drink.Id.ToString());
                    countTextPic = xDoc.CreateTextNode(drink.Pic);

                    countElemPrice.AppendChild(countTextPrice);
                    countElemTitle.AppendChild(countTextTitle);                   

                    attrId.AppendChild(attrIdText);

                    countElemId.AppendChild(countTextId);
                    countElemPic.AppendChild(countTextPic);

                    xnode.RemoveAll();

                    xnode.Attributes.Append(attrId);

                    xnode.AppendChild(countElemPrice);                   
                    xnode.AppendChild(countElemTitle);
                    xnode.AppendChild(countElemId);
                    xnode.AppendChild(countElemPic);
                }
            }
            xDoc.Save("./Content/bar.xml");

        }

        public static ObservableCollection<Order> GetOrders()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("orders.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            ObservableCollection<Order> orders = new ObservableCollection<Order>();
            foreach (XmlNode xnode in xRoot)
            {
                Order order = new Order();
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "total")
                    {
                        order.Total = Int32.Parse(childnode.InnerText);
                    }
                    if (childnode.Name == "data")
                    {
                        order.DateTime = DateTime.Parse(childnode.InnerText);
                    }
                    if (childnode.Name == "titles")
                    {
                        foreach (XmlNode titl in childnode.ChildNodes)
                        {
                            TitleAndCount tNc = new TitleAndCount();
                            XmlNode attr = titl.Attributes.GetNamedItem("titleName");
                            tNc.Title = attr.Value;
                            foreach (XmlNode n in titl)
                            {
                                if (n.Name == "count") tNc.Count = Int32.Parse(n.InnerText);
                            }
                            order.names.Add(tNc);
                        }
                    }
                }
                orders.Add(order);
            }
            return orders;
        }

        public static void SaveXML(Order order)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("orders.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            XmlElement orderElem = xDoc.CreateElement("order");
            XmlElement titlesElem = xDoc.CreateElement("titles");
            foreach (var o in order.names)
            {
                XmlElement titleElem = xDoc.CreateElement("title");
                XmlAttribute titleAttr = xDoc.CreateAttribute("titleName");
                XmlElement countElem = xDoc.CreateElement("count");
                XmlText titleText = xDoc.CreateTextNode(o.Title);
                XmlText countText = xDoc.CreateTextNode(o.Count.ToString());
                titleAttr.AppendChild(titleText);
                countElem.AppendChild(countText);
                titleElem.Attributes.Append(titleAttr);
                titleElem.AppendChild(countElem);
                titlesElem.AppendChild(titleElem);
            }
            XmlElement totalElem = xDoc.CreateElement("total");
            XmlText totalText = xDoc.CreateTextNode(order.Total.ToString());
            XmlElement dataElem = xDoc.CreateElement("data");
            XmlText dataText = xDoc.CreateTextNode(order.DateTime.ToString());
            totalElem.AppendChild(totalText);
            orderElem.AppendChild(totalElem);
            dataElem.AppendChild(dataText);
            orderElem.AppendChild(dataElem);
            orderElem.AppendChild(titlesElem);
            xRoot.AppendChild(orderElem);
            xDoc.Save("orders.xml");
        }

        public static XDocument Serelialize<T>(T source)
        {
            var stream = new MemoryStream();
            var ser = new XmlSerializer(typeof(T));
            ser.Serialize(stream, source);
            stream.Position = 0;
            return XDocument.Load(stream);
        }
        public static T Deserelialize<T>(XDocument source)
        {
            var stream = source.CreateReader();
            var ser = new XmlSerializer(typeof(T));
            return (T)ser.Deserialize(stream);
        }
        public static T LoadObjectFromFile<T>(string path)
        {
            var xml = XDocument.Load(path);
            return Deserelialize<T>(xml);
        }
        public static T LoadObjectFromString<T>(string textXml)
        {
            var xml = XDocument.Parse(textXml);
            return Deserelialize<T>(xml);
        }
    }
}
