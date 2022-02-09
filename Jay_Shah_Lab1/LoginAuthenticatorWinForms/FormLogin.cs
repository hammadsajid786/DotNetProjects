using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginAuthenticatorWinForms
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool isSuccess = LibLoginAuthenticator.LoginAuthenticator.Authenticate(txtUsername.Text, txtPassword.Text);
            if (isSuccess)
            {
                MessageBox.Show("Hurray, Your credentials are correct!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid credentials.\n");
            }
        }
    }
}
