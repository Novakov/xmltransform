using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using CommandLine;
using CommandLine.Text;
using Microsoft.Web.XmlTransform;

[assembly:AssemblyUsage
(
    "xmltransform -i input.xml -t transform.xml -o output.xml key1=value1 key2=value2"
)]

namespace XmlTransform
{
    class Options
    {
        [Option('i', "input", Required = true, HelpText = "Input file")]
        public string InputFile { get; set; }

        [Option('t', "transform", Required = true, HelpText = "Transform file")]
        public string TransformFile { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output file")]
        public string OutputFile { get; set; }

        [ValueList(typeof(List<string>))]
        public List<string> Arguments { get; set; }

        [HelpOption('h', "help")]
        public string GetHelp()
        {
            var text = HelpText.AutoBuild(this, (t) => HelpText.DefaultParsingErrorsHandler(this, t));
            text.Copyright = " ";
            text.Heading = " ";
            text.AddPostOptionsLine("You can add any number of key=value pair. They will be used to replace {key} placeholders in transform file.\n");

            return text.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var opts = new Options();

            if (!CommandLine.Parser.Default.ParseArguments(args, opts))
            {               
                return;
            }

            var dict = new Dictionary<string, string>();

            if (!File.Exists(opts.InputFile))
            {
                Console.WriteLine("Input file {0} doesn't exists", opts.InputFile);
                return;
            }

            if (!File.Exists(opts.TransformFile))
            {
                Console.WriteLine("Transform file {0} doesn't exists", opts.TransformFile);
                return;
            }

            foreach (var item in opts.Arguments)
            {
                if (item.IndexOf("=") < 1)
                {
                    Console.WriteLine("Invalid key-value pair: {0}", item);

                    Console.WriteLine(opts.GetHelp());
                    return;
                }
                else
                {
                    var index = item.IndexOf("=");
                    var key = item.Substring(0, index);
                    var value = item.Substring(index + 1);

                    dict.Add(key, value);
                }
            }
            
            TransformDocument transform;

            using (var fs = new FileStream(opts.TransformFile, FileMode.Open, FileAccess.Read))
            {
                transform = TransformDocument.LoadFrom(fs);
            }            

            transform.ApplyArguments(dict);

            var doc = new XmlTransformableDocument();
            doc.Load(opts.InputFile);

            transform.ApplyTo(doc);

            using (var writer = XmlWriter.Create(opts.OutputFile, new XmlWriterSettings { Indent = true }))
            {
                doc.WriteTo(writer);
            }
        }
    }
}
