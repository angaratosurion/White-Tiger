using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
namespace White_Tiger_Managment
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {

                this.Close();
            }
            catch (Exception ex)
            {
                System.Net.WebException webex = new System.Net.WebException();
                if (ex.GetType() != webex.GetType())
                {
                    Program.errorreport(ex);
                }
                else
                {

                    MessageBox.Show(ex.Message);

                }

            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                MainWindow.username = txtUsername.Text;
                MainWindow.pass = txtPassword.Text;
                NetworkCredential cred = new NetworkCredential(MainWindow.username, MainWindow.pass,Environment.UserDomainName);
                //frmMain.Program.client.Program.clientCredentials.Windows.Program.clientCredential.UserName = frmMain.username;
                //frmMain.Program.client.Program.clientCredentials.Windows.Program.clientCredential.Password = frmMain.pass;
                //frmMain.Program.client.Program.clientCredentials.Windows.Program.clientCredential = cred;
                
                Program.client.Login(MainWindow.username, MainWindow.pass);
                this.Close();
            }
            catch (Exception ex)
            {
                System.Net.WebException webex = new System.Net.WebException();
               // MessageBox.Show(ex.ToString());
                if (ex.GetType() != webex.GetType())
                {
                    Program.errorreport(ex);
                }
                else
                {

                    MessageBox.Show(ex.Message);

                }

            }
        }
    }
}
