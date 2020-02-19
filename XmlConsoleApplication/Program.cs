using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;

namespace XmlConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            string myConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string currentfilepath = ConfigurationManager.AppSettings["DirectoryPath"].ToString();
            string backupfilePath = ConfigurationManager.AppSettings["BackupPath"].ToString();

            try
            {
                while (true)
                {
                    if (File.Exists(currentfilepath))
                    {
                        XmlService xmlService = new XmlService();
                        //string convertedXml = xmlService.ConvertXmlToString(currentfilepath);
                        XmlTextReader xmlreader = new XmlTextReader(currentfilepath);
                        DataSet ds = new DataSet();
                        ds.ReadXml(xmlreader);
                        xmlreader.Close();

                        SqlConnection con = new SqlConnection(myConnectionString);

                        //SqlCommand cmd = new SqlCommand("spCustomerXmlData", con);

                        SqlCommand cmd = new SqlCommand("spUserXmlData", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@UserXmlData", SqlDbType.Xml).Value = ds.GetXml();

                        //cmd.Parameters.AddWithValue("@CustomerXmlData", convertedXml);
                        //cmd.Parameters.AddWithValue("@UserXmlData", convertedXml);


                        con.Open();

                        cmd.ExecuteNonQuery();

                        con.Close();

                        xmlService.CreateBackupFile(backupfilePath, xmlreader.ToString());
                        File.Delete(currentfilepath);
                        Console.WriteLine("New record added successfully || " + DateTime.Now.ToString("dddd, dd MMMM yyyy"));
                        
                    }

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}


