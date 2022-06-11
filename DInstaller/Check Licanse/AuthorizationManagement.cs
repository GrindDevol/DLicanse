using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLicense
{
    static internal class AuthorizationManagement
    {
        public static bool CheckLicanseFile()
        {
            if (System.IO.File.Exists("license.xml"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool Authorization(string licanseKey, string email)
        {
            DataBace db = new DataBace();
            DataTable dataTable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand commandFill = new MySqlCommand("SELECT * FROM `walidkey` WHERE `KeyWord` = @licanseKey AND `Email` = @email AND `PhysicalID` = @physicalId", db.getConnection());
            commandFill.Parameters.Add("@licanseKey", MySqlDbType.VarChar).Value = licanseKey;
            commandFill.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;
            commandFill.Parameters.Add("@physicalId", MySqlDbType.VarChar).Value = Generator.GetAllPhysicalID();

            adapter.SelectCommand = commandFill;
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool NewRecordIntoDataBase(string licanseKey, string email)
        {
            DataBace db = new DataBace();
            DataTable dataTable = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            
            MySqlCommand commandInsert = new MySqlCommand("UPDATE `walidkey` SET `PhysicalID` = @physicalId WHERE `KeyWord` = @licanseKey AND `Email` = @email AND `PhysicalID` = @empty", db.getConnection());
            commandInsert.Parameters.Add("@physicalId", MySqlDbType.VarChar).Value = Generator.GetAllPhysicalID();
            commandInsert.Parameters.Add("@licanseKey", MySqlDbType.VarChar).Value = licanseKey;
            commandInsert.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;
            commandInsert.Parameters.Add("@empty", MySqlDbType.VarChar).Value = "";

            adapter.SelectCommand = commandInsert;
            adapter.Fill(dataTable);

            //MySqlCommand commandGetSecretWord = new MySqlCommand("SELECT `SecretWord` FROM `walidkey` WHERE `KeyWord` = @licanseKey AND `PhysicalID` = @physicalId", db.getConnection());
            //commandGetSecretWord.Parameters.Add("@licanseKey", MySqlDbType.VarChar).Value = licanseKey;
            //commandGetSecretWord.Parameters.Add("@physicalId", MySqlDbType.VarChar).Value = Generator.GetAllPhysicalID();
            //string secretWord = commandGetSecretWord.ExecuteScalar().ToString();
            
            if (Authorization(licanseKey, email))
            {
                MessageBox.Show("Ви успішно авторизовані у системі", "Успішно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Generator.GenerateLicense(licanseKey, email);
                return true;
            }
            else
            {
                MessageBox.Show("Помилка при аторизації", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public static void OpenURL(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw new Exception("Не вдалося відкрити сторінку");
                }
            }
        }
        public static void OpenForm(Form form)
        {
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();//Возможно решить проблему при открытии окна ошибки в новом окне
        }
        //public static void OpenExeFile(string path)
        //{
        //    Process p = new Process();
        //    ProcessStartInfo psi = new ProcessStartInfo();
        //    psi.FileName = path;
        //    p.StartInfo = psi;
        //    //DlicanseAuthorization.CloseCurrentWindow();
        //    Application.Exit();
        //    p.Start();
        //}
        
        //public static void CheckLicanse()
        //{
        //    string licansePath = "./licanseFileInfo.txt";
        //    string filePath = "D:\\2_s_4_c\\WinEnc\\bin\\Debug\\net5.0-windows\\WinEnc.exe";
        //    string errorMassage = String.Empty;

        //    //if (!IsEmpty(licansePath))
        //    //{
        //    //    List<string> list = ReadFile(licansePath);
        //    //    if (ConnectionWhithDataBase(list[0], GetAllPhysicalID()))
        //    //    {
        //    //        //DlicanseAuthorization.CloseCurrentWindow();
        //    //        OpenExeFile(filePath);
        //    //    }
        //    //    else
        //    //    {
        //    //        ErrorForm errorForm = new ErrorForm();
        //    //        errorForm.StartPosition = FormStartPosition.CenterScreen;
        //    //        Application.Run(errorForm);
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    //запуск страницы ввода лицензии
        //    //    DlicanseAuthorization dlicanseAuthorization = new DlicanseAuthorization();
        //    //    dlicanseAuthorization.StartPosition = FormStartPosition.CenterScreen;
        //    //    Application.Run(dlicanseAuthorization);
        //    //}
        //}

    }
}
