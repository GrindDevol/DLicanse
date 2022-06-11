using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DLicense
{
    public partial class CheckLicense : Form
    {
        public CheckLicense()
        {
            InitializeComponent();
        }

        private void labelURL_Click(object sender, EventArgs e)
        {
            AuthorizationManagement.OpenURL("http://google.com");
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            //String email = textBoxEmail.Text;
            //String licanseKey = textBoxLicanseKey.Text;
            //String physicalId = AuthorizationManagement.GetAllPhysicalID();

            //AuthorizationManagement.NewRecordIntoDataBase(licanseKey, physicalId);

            //Generator.GenerateLicense(email, licanseKey);
            
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            //Application.Exit();
            //if (Verify.VerifyLicanse())
            //{
            //    label1.Text = "Вы авторизованы";
            //}
            //else
            //{
            //    label1.Text = "Вы не авторизованы";
            //}
        }
    }
}
