using System;
using System.IO;
using System.Xml.Linq;

namespace XmlConsoleApplication
{
    class XmlService
    {
        public string ConvertXmlToString(string path)
        {
            string textXML = File.ReadAllText(path);
            XElement rootElement = XDocument.Parse(textXML).Root;
            string customerXml = rootElement.ToString();
            return customerXml;
        }

        public void CreateBackupFile(string path,string file)
        {  
            
            string fileName = "(" + DateTime.Now.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss") + ")" + "CustomerBackup"  + ".xml";
            string fullPath = path + fileName;
            File.WriteAllText(fullPath, file);
            string readText = File.ReadAllText(fullPath);
            Console.WriteLine(readText);
        }
    }
}
