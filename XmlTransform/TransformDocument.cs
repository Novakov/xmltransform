using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Web.XmlTransform;

namespace XmlTransform
{
    public class TransformDocument
    {
        private readonly XDocument document;

        public IXmlTransformationLogger Logger { get; set; }

        private TransformDocument(XDocument document)
        {
            this.document = document;
        }

        public static TransformDocument LoadFrom(Stream stream)
        {
            return new TransformDocument(XDocument.Load(stream));
        }

        public void ApplyArguments(IDictionary<string, string> args)
        {
            Traverse(this.document.Root, args);
        }

        public void WriteTo(XmlWriter writer)
        {
            this.document.WriteTo(writer);
        }

        public void ApplyTo(XmlTransformableDocument target)
        {
            using (var ms = new MemoryStream())
            {
                this.document.Save(ms);
                
                ms.Seek(0, SeekOrigin.Begin);

                var transformation = new XmlTransformation(ms, this.Logger);

                transformation.Apply(target);
            }
        }

        private void Traverse(XElement element, IDictionary<string, string> args)
        {
            foreach (var attribute in element.Attributes())
            {
                attribute.Value = Substitute(attribute.Value, args);
            }

            foreach (var item in element.Elements())
            {
                Traverse(item, args);
            }
        }

        private string Substitute(string value, IDictionary<string, string> args)
        {
            foreach (var item in args)
            {
                value = value.Replace("{" + item.Key + "}", item.Value);
            }

            return value;
        }
    }
}
