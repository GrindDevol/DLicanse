using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace DLicense
{
    static internal partial class Generator
    {
        public static class Verify
        {
            public static bool VerifyLicanse()
            {
                string filePath = "license.xml";
                //if (!System.IO.File.Exists(filePath))
                //{
                //    return false;
                //}
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);
                string Signature = xmlDoc.ChildNodes[0].SelectSingleNode(@"/license/Signature", null).InnerText;
                if (Signature == Generator.GetHexString())
                {
                    MessageBox.Show("Ви успішно авторизовані у системі", "Успішно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    MessageBox.Show("Помилка при аторизації", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    AuthorizationManagement.OpenForm(new ErrorForm());
                    return false;
                }
                
            }

            //public static bool SeconVerifyLicanse()
            //{
            //    string filePath = "license.xml";
            //    if (!System.IO.File.Exists(filePath))
            //    {
            //        return false;
            //    }
            //    else
            //    {
            //        XmlDocument xmlDoc = new XmlDocument();
            //        xmlDoc.Load(filePath);
            //        string Key = xmlDoc.ChildNodes[0].SelectSingleNode(@"/license/LicenseKey", null).InnerText;
            //        string Email = xmlDoc.ChildNodes[0].SelectSingleNode(@"/license/Email", null).InnerText;
            //        string Signature = xmlDoc.ChildNodes[0].SelectSingleNode(@"/license/Signature", null).InnerText;

            //        string verifyKey = Generator.GetHexString(Email, Key);
            //        AuthorizationManagement.Authorization(Key, Email);
            //        if (verifyKey == Signature)
            //        {
            //            return true;
            //        }
            //        else
            //        {
            //            return false;
            //        }
            //    }
            //}
        }
    }
}
