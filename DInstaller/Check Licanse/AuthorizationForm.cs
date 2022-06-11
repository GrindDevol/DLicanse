using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DLicense.Generator;

namespace DLicense
{
    public partial class AuthorizationForm : Form
    {
        public AuthorizationForm()
        {
            if (AuthorizationManagement.CheckLicanseFile())
            {
                if (Verify.VerifyLicanse())
                {
                    AuthorizationManagement.OpenForm(new Form());
                }
                else
                {
                    AuthorizationManagement.OpenForm(new ErrorForm());
                }
            }
            else
            {
                InitializeComponent();
            }

            //InitializeComponent();

        }
        private void urlLable_Click(object sender, EventArgs e)
        {
            AuthorizationManagement.OpenURL("http://google.com");
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void buttonNext_Click(object sender, EventArgs e)
        {
            String email = textBoxEmail.Text;
            String licanseKey = textBoxLicense.Text;

            if (AuthorizationManagement.NewRecordIntoDataBase(licanseKey, email))
            {
  
                //Opne new form
                Application.Exit();
            }
            else
            {
                this.Hide();
                AuthorizationManagement.OpenForm(new ErrorForm());
            }
        }

    }
}
