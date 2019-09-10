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
            return textXML;
        }

        public void CreateBackupFile(string path,string file)
        {
            string dateTime = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")).ToString();
            //string fileName =dateTime + "CustomerBackup.xml";
            string fileName = dateTime + "UserBackup.xml";
            string fullPath = path + fileName;
            File.WriteAllText(fullPath, file);           
        }
    }
}
