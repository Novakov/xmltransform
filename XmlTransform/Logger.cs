using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Web.XmlTransform;

namespace XmlTransform
{
    class Logger : IXmlTransformationLogger
    {
        private TextWriter output;

        public Logger(TextWriter textWriter)
        {
            this.output = textWriter;
        }

        public void EndSection(MessageType type, string message, params object[] messageArgs)
        {
            output.Write("EndSection:");
            output.WriteLine(message, messageArgs);
        }

        public void EndSection(string message, params object[] messageArgs)
        {
            this.EndSection(MessageType.Verbose, message, messageArgs);
        }

        public void LogError(string file, int lineNumber, int linePosition, string message, params object[] messageArgs)
        {
            output.Write("ERROR: {0}({1},{2}):", file, lineNumber, linePosition);
            output.WriteLine(message, messageArgs);
        }

        public void LogError(string file, string message, params object[] messageArgs)
        {
            output.Write("ERROR: {0}:", file);
            output.WriteLine(message, messageArgs);
        }

        public void LogError(string message, params object[] messageArgs)
        {
            output.Write("ERROR:");
            output.WriteLine(message, messageArgs);
        }

        public void LogErrorFromException(Exception ex, string file, int lineNumber, int linePosition)
        {
            output.WriteLine("ERROR: {0}({1},{2}): {3}", file, lineNumber, linePosition, ex);
        }

        public void LogErrorFromException(Exception ex, string file)
        {
            output.WriteLine("ERROR: {0}: {1}", file, ex);
        }

        public void LogErrorFromException(Exception ex)
        {
            output.WriteLine("ERROR: {0}", ex);
        }

        public void LogMessage(MessageType type, string message, params object[] messageArgs)
        {
            output.WriteLine(message, messageArgs);
        }

        public void LogMessage(string message, params object[] messageArgs)
        {
            this.LogMessage(MessageType.Verbose, message, messageArgs);
        }

        public void LogWarning(string file, int lineNumber, int linePosition, string message, params object[] messageArgs)
        {
            output.Write("WARN: {0}({1},{2}):", file, lineNumber, linePosition);
            output.WriteLine(message, messageArgs);
        }

        public void LogWarning(string file, string message, params object[] messageArgs)
        {
            output.Write("WARN: {0}:", file);
            output.WriteLine(message, messageArgs);
        }

        public void LogWarning(string message, params object[] messageArgs)
        {
            output.Write("WARN:");
            output.WriteLine(message, messageArgs);
        }

        public void StartSection(MessageType type, string message, params object[] messageArgs)
        {
            output.Write("StartSection:");
            output.WriteLine(message, messageArgs);
        }

        public void StartSection(string message, params object[] messageArgs)
        {
            this.StartSection(MessageType.Verbose, message, messageArgs);
        }
    }
}
