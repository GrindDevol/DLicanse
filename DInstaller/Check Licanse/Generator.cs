using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DLicense
{
    static partial class Generator
    {
        private static string GetBaseBoardID()
        {
            ManagementClass mc = new ManagementClass("Win32_BaseBoard");
            ManagementObjectCollection moc = mc.GetInstances();
            string strID = null;
            foreach (ManagementObject mo in moc)
            {
                strID = mo.Properties["SerialNumber"].Value.ToString();
                break;
            }
            return strID;
        }
        private static string GetProcessorID()
        {
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();
            string strID = null;
            foreach (ManagementObject mo in moc)
            {
                strID = mo.Properties["ProcessorId"].Value.ToString();
                break;
            }
            return strID;
        }
        private static string GetBiosID()
        {
            ManagementClass mc = new ManagementClass("Win32_PhysicalMedia");
            ManagementObjectCollection moc = mc.GetInstances();
            string strID = null;
            foreach (ManagementObject mo in moc)
            {
                strID = mo.Properties["SerialNumber"].Value.ToString();
                break;
            }
            return strID;
        }
        public static string GetAllPhysicalID()
        {
            return (GetBiosID() + "/" + GetBaseBoardID() + "/" + GetProcessorID()).Trim();
        }
        public static void GenerateLicense(string Email, string Key)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(@"<license>
            <LicenseKey></LicenseKey>
            <Email></Email>
            <PhysicalID></PhysicalID>
            <Signature></Signature>
            </license>");
            doc.ChildNodes[0].SelectSingleNode(@"/license/LicenseKey", null).InnerText = Key;
            doc.ChildNodes[0].SelectSingleNode(@"/license/Email", null).InnerText = Email;
            doc.ChildNodes[0].SelectSingleNode(@"/license/PhysicalID", null).InnerText = GetAllPhysicalID();
            doc.ChildNodes[0].SelectSingleNode(@"/license/Signature", null).InnerText = GetHexString();
            doc.Save(System.IO.Path.Combine("license.xml"));
        }

        private static string GetHexString()
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(GetAllPhysicalID() + GetSecret());
            byte[] hash = md5.ComputeHash(data);
            return Convert.ToBase64String(hash);
        }

        public static string GetSecret() 
        {
            DataBace db = new DataBace();
            db.openConnection();
            MySqlCommand commandGetSecretWord = new MySqlCommand("SELECT `SecretWord` FROM `walidkey` WHERE `PhysicalID` = @physicalId", db.getConnection());
            commandGetSecretWord.Parameters.Add("@physicalId", MySqlDbType.VarChar).Value = Generator.GetAllPhysicalID();
            string secretWord = commandGetSecretWord.ExecuteReader().ToString();
            db.closeConnection();
            return secretWord;
        }
    }
}
