using System;
using System.Xml;
using System.Xml.Linq;

namespace FileXML
{
    class Book
    {
        public string Title { get; set; }
        public float Price { get; set; }
    }

    class ReadWriteFileXML
    {
        public static bool WriteToFile()
        {
            Book book = new Book();
            book.Title = "Đắc Nhân Tâm";
            book.Price = 123.5f;
            try
            {
                XDocument xDoc;
                if (System.IO.File.Exists("books.xml"))
                {
                    xDoc = XDocument.Load("books.xml");
                    xDoc.Element("bookstore").Add(new XElement("book",
                        new XElement("title", book.Title),
                        new XElement("price", book.Price.ToString())));
                }
                else
                {
                    xDoc = new XDocument(new XElement("bookstore",
                        new XElement("book",
                            new XElement("title", book.Title),
                            new XElement("price", book.Price.ToString()))));
                }

                xDoc.Save("books.xml");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        static void ReadFromFile()
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;

            using (XmlReader reader = XmlReader.Create("books.xml", settings))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            Console.Write("<" + reader.Name);
                            Console.WriteLine(">");
                            break;
                        case XmlNodeType.Text:
                            Console.WriteLine(reader.Value);
                            break;
                        case XmlNodeType.EndElement:
                            Console.Write("</" + reader.Name);
                            Console.WriteLine(">");
                            break;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            bool isWritten = WriteToFile();
            if (isWritten)
            {
                ReadFromFile();
            }
            else
            {
                Console.WriteLine("Writing data to file encountered an error. Please try again!");
            }
            Console.ReadLine();
        }
    }
}