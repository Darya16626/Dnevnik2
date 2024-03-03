using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace Dnevnik3
{
    public class Stream
    {
        string path = "C:\\Users\\Andre\\source\\repos\\Dnevnik3\\Dnevnik3\\Dnevnik.xml";
        
        public void Serial(List<Inc> Text)
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<Inc>));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                xml.Serialize(fs, Text);
            }
        }
        public List<Inc> Deserial()
        {
            List<Inc> Text;
            XmlSerializer xml = new XmlSerializer(typeof(List<Inc>));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                Text = (List<Inc>)xml.Deserialize(fs);
            }
            return Text;
        }
    }
}
