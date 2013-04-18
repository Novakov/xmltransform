using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using Microsoft.Web.XmlTransform;

namespace XmlTransform
{
    class Program
    {
        static void Main(string[] args)
        {            
            TransformDocument transform;            

            using (var fs = new FileStream("transform.xml", FileMode.Open, FileAccess.Read))
            {
                transform = TransformDocument.LoadFrom(fs);
            }           

            var dict = new Dictionary<string, string>
            {
                {"name", "testing"}
            };

            transform.ApplyArguments(dict);          

            var doc = new XmlTransformableDocument();
            doc.Load("input.xml");

            transform.ApplyTo(doc);

            using (var writer = XmlWriter.Create(Console.Out, new XmlWriterSettings { Indent = true }))
            {
                doc.WriteTo(writer);
            }

            Console.ReadLine();
        }
    }       
}
