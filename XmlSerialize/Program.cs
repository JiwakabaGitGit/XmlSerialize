using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace XmlSerialize
{
    [XmlRoot("book")]
    public class Book
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("publised")]
        public DateTime Published { get; set; }

        [XmlArray("authors")]
        [XmlArrayItem("author")]
        public List<Author> Authors { get; set; }
    }

    public class Author
    {
        [XmlElement("authorName")]
        public string AuthorName { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string path = "test.xml";

            var book = new Book
            {
                Title =
                    "The Art of Readable Code: Simple and Practical Techniques for Writing Better Code (Theory in Practice)",
                Published = new DateTime(2011, 11, 3),
                Authors = new List<Author>
                {
                    new Author {AuthorName = "Dustin Boswell"},
                    new Author {AuthorName = "Trevor Foucher"}
                }
            };

            // シリアライズ
            using (StreamWriter writer = new StreamWriter(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Book)); // Bookクラスのシリアライザを定義
                serializer.Serialize(writer, book);
            }

            // デシリアライズ
            Book deserializedBook;
            using (StreamReader reader = new StreamReader(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Book));
                deserializedBook = (Book)serializer.Deserialize(reader);
            }

            Console.WriteLine($"Title: {deserializedBook.Title}");
            Console.WriteLine($"Published: {deserializedBook.Published}");
            foreach (var author in deserializedBook.Authors)
            {
                Console.WriteLine($"AuthorName: {author.AuthorName}");
            }
        }

    }
}
