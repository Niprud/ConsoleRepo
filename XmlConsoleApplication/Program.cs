using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace XmlConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string myConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string path = ConfigurationManager.AppSettings["DirectoryPath"].ToString();
            string backupPath = ConfigurationManager.AppSettings["BackupPath"].ToString();


            XmlService xmlService = new XmlService();
            string convertedXml = xmlService.ConvertXmlToString(path);
            xmlService.CreateBackupFile(backupPath, convertedXml);

            SqlConnection con = new SqlConnection(myConnectionString);

            SqlCommand cmd = new SqlCommand("spAddCustomer", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param = cmd.Parameters.Add("@xmlText", SqlDbType.NVarChar);
            param.Value = convertedXml;

            con.Open();

            cmd.ExecuteNonQuery();

            con.Close();

        }
    }
}


